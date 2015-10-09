namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class WorldSelectPopupPanel : CustomSelfDrawPanel
    {
        public static Image closedImage;
        public static Image closeImage;
        public static Image closeImageOver;
        private const int columnExtra = 50;
        private IContainer components;
        public static int defaultHeight = 0x25d;
        public static int defaultWidth = 0x338;
        private const int extraHeight = 160;
        private const int extraWidth = 0x7c;
        private CustomSelfDrawPanel.CSDFill infoOverlay = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDLabel infoOverlayActivePlayers = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel infoOverlayActivePlayersValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton infoOverlayClose = new CustomSelfDrawPanel.CSDButton();
        private static SparseArray InfoOverlayData = new SparseArray();
        private CustomSelfDrawPanel.CSDLabel infoOverlayDuration = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel infoOverlayDurationValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel infoOverlayGameAge = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel infoOverlayGameAgeValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage infoOverlayHeading = new CustomSelfDrawPanel.CSDImage();
        private static SparseArray InfoOverlayHeadings = new SparseArray();
        private CustomSelfDrawPanel.CSDLabel infoOverlayHouses = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel infoOverlayHousesValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDFill infoOverlayPanel = new CustomSelfDrawPanel.CSDFill();
        public static Image joinImage;
        public static Image joinImageOver;
        private CustomSelfDrawPanel.CSDArea languageArea = new CustomSelfDrawPanel.CSDArea();
        private string lastLang = "";
        public List<CustomSelfDrawPanel.CSDControl> loggedInWorldControls = new List<CustomSelfDrawPanel.CSDControl>();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        public static Image playImage;
        public static Image playImageOver;
        public int pulse;
        private bool pulseTitleButton;
        private int scrHeight = 480;
        private int scrWidth = 0x29d;
        private int scrX = 0x4b;
        private int scrY = 80;
        public static Image selectAIImage;
        public static Image selectAIImageOver;
        public static Image selectAIImageSelected;
        private CustomSelfDrawPanel.CSDFill selectedWorldRect = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDFill selectedWorldRect2 = new CustomSelfDrawPanel.CSDFill();
        public static Image selectImage;
        public static Image selectImageOver;
        public static Image selectImageSelected;
        public static Image selectSpecialImage;
        public static Image selectSpecialImageOver;
        public static Image selectSpecialImageSelected;
        private CustomSelfDrawPanel.CSDCheckBox showOwnWorlds = new CustomSelfDrawPanel.CSDCheckBox();
        private static bool showOwnWorldsStatus = true;
        private static int showSpecialWorlds = -1;
        private string strAIWorlds = SK.Text("WORLD_Special_AI", "AI Worlds");
        private string strClose = SK.Text("GENERIC_Close", "Close");
        private string strClosed = SK.Text("FactionInvites_Membership_closed", "Closed");
        private string strJoin = SK.Text("WORLD_Join", "Join");
        private string strOffline = SK.Text("WORLD_Offline", "Offline");
        private string strOnline = SK.Text("WORLD_Online", "Online");
        private string strPlay = SK.Text("WORLD_Play", "Play");
        private string strSelect = SK.Text("WORLD_Select_Standard", "Select Standard Worlds");
        private string strSelectAI = SK.Text("WORLD_Select_AI", "Select AI Worlds");
        private string strSelectSpecial = SK.Text("WORLD_Select_Special", "Select Special Worlds");
        private string strSpecialWorlds = SK.Text("WORLD_Special_Worlds", "Special Worlds");
        private string strStandardWorlds = SK.Text("WORLD_Standard_Worlds", "Standard Worlds");
        private string strWorldEnded = SK.Text("WorldEnded", "This World has ended.");
        private CustomSelfDrawPanel.CSDLabel supportLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton titleButton;
        private CustomSelfDrawPanel.CSDButton titleButton2;
        private CustomSelfDrawPanel.CSDImage titleImage;
        private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private Color WebButtonblue = Color.FromArgb(0x55, 0x91, 0xcb);
        private Color WebButtonGrey = Color.FromArgb(0xe1, 0xe1, 0xe1);
        private int WebButtonheight = 0x16;
        private int WebButtonRadius = 10;
        private Color WebButtonRed = Color.FromArgb(160, 0, 0);
        private Color WebButtonRedFaded = Color.FromArgb(160, 0x60, 0x60);
        private int WebButtonWidth = 120;
        private Color WebButtonYellow = Color.FromArgb(0xe1, 0xe1, 0);
        private Color WebButtonYellow2 = Color.FromArgb(0xff, 0xee, 8);
        private Font WebTextFontBold = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-Bold.ttf", 10f, FontStyle.Bold);
        private Font WebTextFontBoldCond = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 10f, FontStyle.Bold);
        private int worldControlHeight = 0x18;
        private int worldControlWidth = 80;

        public WorldSelectPopupPanel()
        {
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void AddControlToPanel(CustomSelfDrawPanel.CSDControl c)
        {
            base.addControl(c);
        }

        private void addTitleButtons()
        {
            this.pulseTitleButton = false;
            this.wallScrollBar.Value = 0;
            this.wallScrollBarMoved();
            if (this.titleImage != null)
            {
                this.removeControlFromPanel(this.titleImage);
            }
            if (this.titleButton != null)
            {
                this.removeControlFromPanel(this.titleButton);
            }
            if (this.titleButton2 != null)
            {
                this.removeControlFromPanel(this.titleButton2);
            }
            if (showSpecialWorlds == -1)
            {
                if (this.areThereSpecialWorlds())
                {
                    this.titleButton = new CustomSelfDrawPanel.CSDButton();
                    this.titleButton.ImageNorm = this.SelectSpecialImage;
                    this.titleButton.ImageOver = this.SelectSpecialImageOver;
                    this.titleButton.Position = new Point(0x119, 10);
                    this.titleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.specialWorldsClick));
                    this.AddControlToPanel(this.titleButton);
                }
                else
                {
                    this.titleButton = null;
                }
                if (this.areThereAIWorlds())
                {
                    this.titleButton2 = new CustomSelfDrawPanel.CSDButton();
                    this.titleButton2.ImageNorm = this.SelectAIImage;
                    this.titleButton2.ImageOver = this.SelectAIImageOver;
                    this.titleButton2.Position = new Point(0x227, 10);
                    this.titleButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.aiWorldsClick));
                    this.AddControlToPanel(this.titleButton2);
                    if (!this.areTherePlayedAIWorlds())
                    {
                        this.pulseTitleButton = true;
                    }
                }
                else
                {
                    this.titleButton2 = null;
                }
                this.titleImage = new CustomSelfDrawPanel.CSDImage();
                this.titleImage.Image = this.SelectImageSelected;
                this.titleImage.Position = new Point(11, 8);
                this.AddControlToPanel(this.titleImage);
                this.supportLabel.Visible = true;
                this.languageArea.Visible = true;
            }
            else if (showSpecialWorlds == 1)
            {
                this.titleButton = new CustomSelfDrawPanel.CSDButton();
                this.titleButton.ImageNorm = this.SelectImage;
                this.titleButton.ImageOver = this.SelectImageOver;
                this.titleButton.Position = new Point(11, 10);
                this.titleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.standardWorldsClick));
                this.AddControlToPanel(this.titleButton);
                this.titleImage = new CustomSelfDrawPanel.CSDImage();
                this.titleImage.Image = this.SelectSpecialImageSelected;
                this.titleImage.Position = new Point(0x119, 8);
                this.AddControlToPanel(this.titleImage);
                if (this.areThereAIWorlds())
                {
                    this.titleButton2 = new CustomSelfDrawPanel.CSDButton();
                    this.titleButton2.ImageNorm = this.SelectAIImage;
                    this.titleButton2.ImageOver = this.SelectAIImageOver;
                    this.titleButton2.Position = new Point(0x227, 10);
                    this.titleButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.aiWorldsClick));
                    this.AddControlToPanel(this.titleButton2);
                    if (!this.areTherePlayedAIWorlds())
                    {
                        this.pulseTitleButton = true;
                    }
                }
                else
                {
                    this.titleButton2 = null;
                }
                this.supportLabel.Visible = false;
                this.languageArea.Visible = false;
            }
            else if (showSpecialWorlds == 2)
            {
                this.titleButton = new CustomSelfDrawPanel.CSDButton();
                this.titleButton.ImageNorm = this.SelectImage;
                this.titleButton.ImageOver = this.SelectImageOver;
                this.titleButton.Position = new Point(11, 10);
                this.titleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.standardWorldsClick));
                this.AddControlToPanel(this.titleButton);
                this.titleImage = new CustomSelfDrawPanel.CSDImage();
                this.titleImage.Image = this.SelectAIImageSelected;
                this.titleImage.Position = new Point(0x227, 8);
                this.AddControlToPanel(this.titleImage);
                if (this.areThereSpecialWorlds())
                {
                    this.titleButton = new CustomSelfDrawPanel.CSDButton();
                    this.titleButton.ImageNorm = this.SelectSpecialImage;
                    this.titleButton.ImageOver = this.SelectSpecialImageOver;
                    this.titleButton.Position = new Point(0x119, 10);
                    this.titleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.specialWorldsClick));
                    this.AddControlToPanel(this.titleButton);
                }
                else
                {
                    this.titleButton = null;
                }
                this.supportLabel.Visible = false;
                this.languageArea.Visible = false;
            }
        }

        private void aiWorldsClick()
        {
            showSpecialWorlds = 2;
            List<WorldInfo> list = Program.profileLogin.GetWorldsBySupportCulture("", showOwnWorldsStatus, showSpecialWorlds);
            this.BuildOnlineWorldList(list);
            this.addTitleButtons();
        }

        private bool areThereAIWorlds()
        {
            foreach (WorldInfo info in Program.profileLogin.GetWorldsBySupportCulture("", true, 0))
            {
                if (ProfileLoginWindow.isAIWorld(info.KingdomsWorldID))
                {
                    return true;
                }
            }
            return false;
        }

        private bool areTherePlayedAIWorlds()
        {
            foreach (WorldInfo info in Program.profileLogin.GetWorldsBySupportCulture("", true, 0))
            {
                if (ProfileLoginWindow.isAIWorld(info.KingdomsWorldID) && info.Playing)
                {
                    return true;
                }
            }
            return false;
        }

        private bool areThereSpecialWorlds()
        {
            foreach (WorldInfo info in Program.profileLogin.GetWorldsBySupportCulture("", true, 0))
            {
                if (ProfileLoginWindow.isSpecialWorld(info.KingdomsWorldID))
                {
                    return true;
                }
            }
            return false;
        }

        private void btnWorldAction_Click()
        {
            WorldInfo tag = (WorldInfo) base.ClickedControl.Tag;
            this.closeClick();
            Program.profileLogin.btnWorldAction_Click(tag);
        }

        public void BuildOnlineWorldList(List<WorldInfo> list)
        {
            this.loggedInWorldControls.Clear();
            this.wallScrollArea.clearControls();
            if (list.Count > 0)
            {
                DateTime time = new DateTime(0x7de, 2, 20, 0, 0, 0);
                bool playing = list[0].Playing;
                int y = 0;
                int height = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    CustomSelfDrawPanel.CSDLabel item = new CustomSelfDrawPanel.CSDLabel();
                    CustomSelfDrawPanel.CSDImage image = new CustomSelfDrawPanel.CSDImage();
                    CustomSelfDrawPanel.CSDImage image2 = new CustomSelfDrawPanel.CSDImage();
                    CustomSelfDrawPanel.CSDLabel label2 = new CustomSelfDrawPanel.CSDLabel();
                    CustomSelfDrawPanel.CSDImage image3 = new CustomSelfDrawPanel.CSDImage();
                    CustomSelfDrawPanel.CSDImage image4 = new CustomSelfDrawPanel.CSDImage();
                    if (list[i].Playing != playing)
                    {
                        playing = list[i].Playing;
                        y += 20;
                    }
                    if ((i & 1) == 0)
                    {
                        image3.Image = (Image) GFXLibrary.lineitem_strip_02_dark;
                    }
                    else
                    {
                        image3.Image = (Image) GFXLibrary.lineitem_strip_02_light;
                    }
                    image3.Position = new Point(0, y);
                    this.loggedInWorldControls.Add(image3);
                    height = y + 40;
                    image.Y = y + 7;
                    image2.Y = y + 4;
                    item.Y = y + 9;
                    label2.Y = y + 9;
                    item.Width = 0x90;
                    item.Height = this.worldControlHeight;
                    label2.Width = 0x69;
                    label2.Height = this.worldControlHeight;
                    item.Text = ProfileLoginWindow.getWorldShortDesc(list[i]);
                    image.Image = (Image) GFXLibrary.getLoginWorldFlag(list[i].Supportculture);
                    image.Width = image.Image.Width;
                    image.Height = image.Image.Height;
                    image2.Image = (Image) GFXLibrary.getLoginWorldMap(list[i].MapCulture);
                    image2.Width = image2.Image.Width;
                    image2.Height = image2.Image.Height;
                    switch (list[i].Supportculture)
                    {
                        case "en":
                            image.CustomTooltipID = 0xfa1;
                            break;

                        case "de":
                            image.CustomTooltipID = 0xfa2;
                            break;

                        case "fr":
                            image.CustomTooltipID = 0xfa3;
                            break;

                        case "ru":
                            image.CustomTooltipID = 0xfa4;
                            break;

                        case "es":
                            image.CustomTooltipID = 0xfb0;
                            break;

                        case "pl=":
                            image.CustomTooltipID = 0xfb4;
                            break;

                        case "tr":
                            image.CustomTooltipID = 0xfb7;
                            break;

                        case "it":
                            image.CustomTooltipID = 0xfbb;
                            break;

                        case "pt":
                            image.CustomTooltipID = 0xfc3;
                            break;

                        case "eu":
                            image.CustomTooltipID = 0xfbf;
                            break;
                    }
                    switch (list[i].MapCulture)
                    {
                        case "en":
                            image2.CustomTooltipID = 0xfa5;
                            break;

                        case "de":
                            image2.CustomTooltipID = 0xfa6;
                            break;

                        case "fr":
                            image2.CustomTooltipID = 0xfa7;
                            break;

                        case "ru":
                            image2.CustomTooltipID = 0xfa8;
                            break;

                        case "es":
                            image2.CustomTooltipID = 0xfb1;
                            break;

                        case "pl":
                            image2.CustomTooltipID = 0xfb5;
                            break;

                        case "tr":
                            image2.CustomTooltipID = 0xfb8;
                            break;

                        case "it":
                            image2.CustomTooltipID = 0xfbc;
                            break;

                        case "us":
                            image2.CustomTooltipID = 0xfbe;
                            break;

                        case "eu":
                            image2.CustomTooltipID = 0xfc0;
                            break;

                        case "pt":
                            image2.CustomTooltipID = 0xfc4;
                            break;
                    }
                    item.X = 0x18;
                    image.X = (((((item.X - 20) - 0x39) + item.Width) + 8) + 0x4b) + 30;
                    image2.X = ((image.X + image.Width) + 8) + 0x4b;
                    label2.X = (((image2.X + image2.Width) + 8) + 0x4b) - 40;
                    if (list[i].ShortDesc.Contains("****"))
                    {
                        image4.Image = (Image) GFXLibrary.age_fifth_age_28x16;
                        image4.Position = new Point(image.X - 80, (y + 7) - 5);
                        image4.CustomTooltipID = 0xfc7;
                        this.loggedInWorldControls.Add(image4);
                    }
                    else if (list[i].ShortDesc.Contains("***"))
                    {
                        image4.Image = (Image) GFXLibrary.age_fourth_age_28x16;
                        image4.Position = new Point(image.X - 80, (y + 7) - 5);
                        image4.CustomTooltipID = 0xfc2;
                        this.loggedInWorldControls.Add(image4);
                    }
                    else if (list[i].ShortDesc.Contains("**"))
                    {
                        image4.Image = (Image) GFXLibrary.age_third_age_28x16;
                        image4.Position = new Point(image.X - 80, (y + 7) - 5);
                        image4.CustomTooltipID = 0xfba;
                        this.loggedInWorldControls.Add(image4);
                    }
                    else if (list[i].ShortDesc.Contains("*"))
                    {
                        image4.Image = (Image) GFXLibrary.age_second_age_28x16;
                        image4.Position = new Point(image.X - 80, (y + 7) - 5);
                        image4.CustomTooltipID = 0xfb3;
                        this.loggedInWorldControls.Add(image4);
                    }
                    else if (!ProfileLoginWindow.isAIWorld(list[i].KingdomsWorldID) && !ProfileLoginWindow.isSpecialWorld(list[i].KingdomsWorldID))
                    {
                        image4.Image = (Image) GFXLibrary.age_first_age_28x16;
                        image4.Position = new Point(image.X - 80, (y + 7) - 5);
                        image4.CustomTooltipID = 0xfc6;
                        this.loggedInWorldControls.Add(image4);
                    }
                    if (list[i].Online)
                    {
                        label2.Text = this.strOnline;
                        label2.Color = ARGBColors.Green;
                        CustomSelfDrawPanel.CSDButton button = new CustomSelfDrawPanel.CSDButton {
                            Width = this.worldControlWidth,
                            Height = this.worldControlHeight,
                            Y = y + 5,
                            Tag = list[i]
                        };
                        button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnWorldAction_Click), "WorldSelectPopupPanel_world_select");
                        if (list[i].Playing)
                        {
                            button.ImageNorm = this.PlayImage;
                            button.ImageOver = this.PlayImageOver;
                        }
                        else if (list[i].AvailableToJoin)
                        {
                            button.ImageNorm = this.JoinImage;
                            button.ImageOver = this.JoinImageOver;
                        }
                        else
                        {
                            button.ImageNorm = this.ClosedImage;
                            button.ImageOver = this.ClosedImage;
                            button.setClickDelegate(null);
                            button.Active = false;
                        }
                        button.Width = button.ImageNorm.Width;
                        button.Height = button.ImageNorm.Height;
                        button.X = 0x254 - button.Width;
                        this.loggedInWorldControls.Add(button);
                        if (button.Active)
                        {
                            CustomSelfDrawPanel.CSDButton button2 = new CustomSelfDrawPanel.CSDButton {
                                ImageNorm = (Image) GFXLibrary.help_normal,
                                ImageOver = (Image) GFXLibrary.help_over,
                                ImageClick = (Image) GFXLibrary.help_pushed,
                                Position = new Point(0x260, y + 8),
                                Data = list[i].KingdomsWorldID
                            };
                            button2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoOverlayOpenedClick));
                            this.loggedInWorldControls.Add(button2);
                        }
                        label2.CustomTooltipID = 0xfaa;
                    }
                    else
                    {
                        if ((list[i].KingdomsWorldID == 0x9c4) && (DateTime.UtcNow > time))
                        {
                            label2.Text = this.strWorldEnded;
                            label2.Width = 300;
                        }
                        else
                        {
                            label2.Text = this.strOffline;
                            label2.Color = ARGBColors.Red;
                        }
                        label2.CustomTooltipID = 0xfa9;
                    }
                    if (showSpecialWorlds <= 0)
                    {
                        this.loggedInWorldControls.Add(image);
                    }
                    this.loggedInWorldControls.Add(image2);
                    this.loggedInWorldControls.Add(item);
                    this.loggedInWorldControls.Add(label2);
                    y += 40;
                }
                foreach (CustomSelfDrawPanel.CSDControl control in this.loggedInWorldControls)
                {
                    this.wallScrollArea.addControl(control);
                }
                this.wallScrollArea.Size = new Size(this.wallScrollArea.Width, height);
                if (height < this.wallScrollBar.Height)
                {
                    this.wallScrollBar.Visible = false;
                }
                else
                {
                    this.wallScrollBar.Visible = true;
                    this.wallScrollBar.NumVisibleLines = this.wallScrollBar.Height;
                    this.wallScrollBar.Max = height - this.wallScrollBar.Height;
                }
                this.wallScrollArea.invalidate();
                this.wallScrollBar.invalidate();
                base.Invalidate();
            }
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.closeWorldSelectPopupWindow();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void infoOverlayCloseClicked()
        {
            this.infoOverlay.Visible = false;
        }

        private void infoOverlayFillinData(WorldInfoData data)
        {
            if ((data == null) || (data.worldID == 0))
            {
                this.infoOverlay.Visible = false;
            }
            else
            {
                NumberFormatInfo nFI = GameEngine.NFI;
                this.infoOverlayDurationValue.Text = data.daysOld.ToString("N", nFI);
                if (ProfileLoginWindow.isSpecialWorld(data.worldID))
                {
                    this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_Domination", "Domination");
                }
                else if (ProfileLoginWindow.isAIWorld(data.worldID))
                {
                    this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_AI", "AI");
                }
                else
                {
                    switch (data.age)
                    {
                        case 0:
                            this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_1stAge", "1st Age");
                            break;

                        case 1:
                            this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_2ndAge", "2nd Age");
                            break;

                        case 2:
                            this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_3rdAge", "3rd Age");
                            break;

                        case 3:
                            this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_4thAge", "4th Age");
                            break;

                        case 4:
                            this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_5thAge", "5th Age");
                            break;

                        case 5:
                            this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_6thAge", "6th Age");
                            break;

                        case 6:
                            this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_7thAge", "7th Age");
                            break;
                    }
                }
                this.infoOverlayHousesValue.Text = data.housesInGlory.ToString("N", nFI);
                this.infoOverlayActivePlayersValue.Text = data.activePlayers.ToString("N", nFI);
            }
        }

        private void infoOverlayOpenedClick()
        {
            CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
            int data = clickedControl.Data;
            foreach (WorldInfo info in Program.profileLogin.GetWorldsBySupportCulture(this.lastLang, showOwnWorldsStatus, showSpecialWorlds))
            {
                if (info.KingdomsWorldID == data)
                {
                    this.openInfoOverlay(info);
                    break;
                }
            }
        }

        public void init(int villageID, bool reset)
        {
            base.clearControls();
            this.languageArea.Size = base.Size;
            this.AddControlToPanel(this.languageArea);
            this.BackColor = ARGBColors.White;
            this.addTitleButtons();
            CustomSelfDrawPanel.CSDButton c = new CustomSelfDrawPanel.CSDButton {
                ImageNorm = this.CloseImage,
                ImageOver = this.CloseImageOver,
                Position = new Point(0x25c, 570)
            };
            c.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "WorldSelectPopupPanel_close");
            this.AddControlToPanel(c);
            CustomSelfDrawPanel.CSDLabel label = new CustomSelfDrawPanel.CSDLabel {
                Text = SK.Text("WORLD_SELECT_Name", "Name"),
                Position = new Point(0x5c, 0x3f),
                Size = new Size(300, 30),
                Color = ARGBColors.Black,
                Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold)
            };
            this.AddControlToPanel(label);
            this.supportLabel.Text = SK.Text("WORLD_SELECT_Support", "Support Language");
            this.supportLabel.Position = new Point(0x91, 0x3f);
            this.supportLabel.Size = new Size(300, 30);
            this.supportLabel.Color = ARGBColors.Black;
            this.supportLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.supportLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.AddControlToPanel(this.supportLabel);
            CustomSelfDrawPanel.CSDLabel label2 = new CustomSelfDrawPanel.CSDLabel {
                Text = SK.Text("WORLD_SELECT_Map", "Map"),
                Position = new Point(0x131, 0x3f),
                Size = new Size(200, 30),
                Color = ARGBColors.Black,
                Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER
            };
            this.AddControlToPanel(label2);
            CustomSelfDrawPanel.CSDLabel label3 = new CustomSelfDrawPanel.CSDLabel {
                Text = SK.Text("WORLD_SELECT_Status", "Status"),
                Position = new Point(0x1c8, 0x3f),
                Size = new Size(300, 30),
                Color = ARGBColors.Black,
                Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold)
            };
            this.AddControlToPanel(label3);
            this.wallScrollArea.Position = new Point(this.scrX, this.scrY);
            this.wallScrollArea.Size = new Size(this.scrWidth, this.scrHeight);
            this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(this.scrWidth, this.scrHeight));
            this.AddControlToPanel(this.wallScrollArea);
            this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
            this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.AddControlToPanel(this.mouseWheelOverlay);
            int num1 = this.wallScrollBar.Value;
            this.wallScrollBar.Position = new Point(this.scrWidth + this.scrX, this.scrY);
            this.wallScrollBar.Size = new Size(0x18, this.scrHeight);
            this.AddControlToPanel(this.wallScrollBar);
            this.wallScrollBar.Value = 0;
            this.wallScrollBar.Max = 100;
            this.wallScrollBar.NumVisibleLines = 0x19;
            this.wallScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            this.showOwnWorlds.CheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[0];
            this.showOwnWorlds.UncheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[1];
            this.showOwnWorlds.Position = new Point(15, 570);
            this.showOwnWorlds.Checked = showOwnWorldsStatus;
            this.showOwnWorlds.CBLabel.Text = SK.Text("WORLD_Always_Show_Your_Worlds", "Always show worlds you are playing.");
            this.showOwnWorlds.CBLabel.Color = ARGBColors.Black;
            this.showOwnWorlds.CBLabel.Position = new Point(20, -1);
            this.showOwnWorlds.CBLabel.Size = new Size(400, 0x19);
            this.showOwnWorlds.CBLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.showOwnWorlds.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.ownToggled));
            this.AddControlToPanel(this.showOwnWorlds);
            Dictionary<string, LocalizationLanguage> dictionary = new Dictionary<string, LocalizationLanguage>();
            this.selectedWorldRect.Position = new Point();
            this.selectedWorldRect.FillColor = Color.FromArgb(0xc0, 0xc0, 0xc0);
            this.selectedWorldRect.Size = new Size(0x22, 0x16);
            this.languageArea.addControl(this.selectedWorldRect);
            this.selectedWorldRect2.Position = new Point();
            this.selectedWorldRect2.FillColor = ARGBColors.Black;
            this.selectedWorldRect2.Size = new Size(0x20, 20);
            this.languageArea.addControl(this.selectedWorldRect2);
            bool flag = false;
            foreach (WorldInfo info in ProfileLoginWindow.WorldList)
            {
                if (dictionary.ContainsKey(info.Supportculture))
                {
                    continue;
                }
                if (info.Supportculture == "eu")
                {
                    flag = true;
                    continue;
                }
                LocalizationLanguage language = new LocalizationLanguage {
                    CultureCode = info.Supportculture
                };
                dictionary.Add(info.Supportculture, language);
                CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage();
                string cultureCode = language.CultureCode;
                switch (cultureCode)
                {
                    case "pt":
                    case "br":
                        cultureCode = "br";
                        break;
                }
                control.Image = (Image) GFXLibrary.getLoginWorldFlag(cultureCode);
                control.Width = control.Image.Width;
                control.Height = control.Image.Height;
                switch (language.CultureCode)
                {
                    case "en":
                        control.CustomTooltipID = 0xfab;
                        break;

                    case "de":
                        control.CustomTooltipID = 0xfac;
                        break;

                    case "fr":
                        control.CustomTooltipID = 0xfad;
                        break;

                    case "ru":
                        control.CustomTooltipID = 0xfae;
                        break;

                    case "es":
                        control.CustomTooltipID = 0xfb2;
                        break;

                    case "pl":
                        control.CustomTooltipID = 0xfb6;
                        break;

                    case "tr":
                        control.CustomTooltipID = 0xfb9;
                        break;

                    case "it":
                        control.CustomTooltipID = 0xfbd;
                        break;

                    case "eu":
                        control.CustomTooltipID = 0xfc1;
                        break;

                    case "pt":
                        control.CustomTooltipID = 0xfc5;
                        break;
                }
                control.Position = new Point((((this.scrWidth + 0x35) + 90) - (dictionary.Count * (control.Width + 2))) - 4, 0x2d);
                this.languageArea.addControl(control);
                control.Tag = language;
                control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.language_Click), "WorldSelectPopupPanel_language_flags");
            }
            if (flag)
            {
                CustomSelfDrawPanel.CSDImage image2;
                LocalizationLanguage language2 = new LocalizationLanguage {
                    CultureCode = "eu"
                };
                dictionary.Add("eu", language2);
                image2 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.getLoginWorldFlag("eu"),
                    Width = ((Image)GFXLibrary.getLoginWorldFlag("eu")).Width,
                    Height = ((Image) GFXLibrary.getLoginWorldFlag("eu")).Height,
                    CustomTooltipID = 0xfc1,
                    Position = new Point((((this.scrWidth + 0x35) + 90) - (dictionary.Count * (((Image) GFXLibrary.getLoginWorldFlag("eu")).Width + 2))) - 4, 0x2d)
                };
                this.languageArea.addControl(image2);
                image2.Tag = language2;
                image2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.language_Click), "WorldSelectPopupPanel_language_flags");
            }
            this.lastLang = ProfileLoginWindow.LastSelectedSupportCulture;
            this.updateFlagAlpha();
            this.infoOverlay.Position = new Point(0, 0);
            this.infoOverlay.Size = base.Size;
            this.infoOverlay.FillColor = Color.FromArgb(0x80, 0, 0, 0);
            this.infoOverlay.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoOverlayCloseClicked));
            this.infoOverlay.Visible = false;
            this.AddControlToPanel(this.infoOverlay);
            this.infoOverlayPanel.Position = new Point(200, 150);
            this.infoOverlayPanel.Size = new Size(base.Width - 400, base.Height - 300);
            this.infoOverlayPanel.FillColor = ARGBColors.White;
            this.infoOverlay.addControl(this.infoOverlayPanel);
            this.infoOverlayClose.ImageNorm = this.CloseImage;
            this.infoOverlayClose.ImageOver = this.CloseImageOver;
            this.infoOverlayClose.Position = new Point(200, 270);
            this.infoOverlayClose.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoOverlayCloseClicked), "WorldSelectPopupPanel_close");
            this.infoOverlayPanel.addControl(this.infoOverlayClose);
            this.infoOverlayHeading.Position = new Point(0x51, 10);
            this.infoOverlayPanel.addControl(this.infoOverlayHeading);
            this.infoOverlayDuration.Text = SK.Text("WorldSelect_WorldDuration", "Days since World Start");
            this.infoOverlayDuration.Position = new Point(40, 70);
            this.infoOverlayDuration.Size = new Size(240, 50);
            this.infoOverlayDuration.Color = ARGBColors.Black;
            this.infoOverlayDuration.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.infoOverlayDuration.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.infoOverlayPanel.addControl(this.infoOverlayDuration);
            this.infoOverlayDurationValue.Text = "?";
            this.infoOverlayDurationValue.Position = new Point(0xb9, 70);
            this.infoOverlayDurationValue.Size = new Size(200, 50);
            this.infoOverlayDurationValue.Color = ARGBColors.Black;
            this.infoOverlayDurationValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.infoOverlayDurationValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.infoOverlayPanel.addControl(this.infoOverlayDurationValue);
            this.infoOverlayGameAge.Text = SK.Text("WorldSelect_GameAge", "Game Type");
            this.infoOverlayGameAge.Position = new Point(40, 110);
            this.infoOverlayGameAge.Size = new Size(240, 50);
            this.infoOverlayGameAge.Color = ARGBColors.Black;
            this.infoOverlayGameAge.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.infoOverlayGameAge.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.infoOverlayPanel.addControl(this.infoOverlayGameAge);
            this.infoOverlayGameAgeValue.Text = "?";
            this.infoOverlayGameAgeValue.Position = new Point(0xb9, 110);
            this.infoOverlayGameAgeValue.Size = new Size(200, 50);
            this.infoOverlayGameAgeValue.Color = ARGBColors.Black;
            this.infoOverlayGameAgeValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.infoOverlayGameAgeValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.infoOverlayPanel.addControl(this.infoOverlayGameAgeValue);
            this.infoOverlayHouses.Text = SK.Text("WorldSelect_RemainingHouses", "Houses Left in Glory Race");
            this.infoOverlayHouses.Position = new Point(40, 150);
            this.infoOverlayHouses.Size = new Size(240, 50);
            this.infoOverlayHouses.Color = ARGBColors.Black;
            this.infoOverlayHouses.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.infoOverlayHouses.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.infoOverlayPanel.addControl(this.infoOverlayHouses);
            this.infoOverlayHousesValue.Text = "?";
            this.infoOverlayHousesValue.Position = new Point(0xb9, 150);
            this.infoOverlayHousesValue.Size = new Size(200, 50);
            this.infoOverlayHousesValue.Color = ARGBColors.Black;
            this.infoOverlayHousesValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.infoOverlayHousesValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.infoOverlayPanel.addControl(this.infoOverlayHousesValue);
            this.infoOverlayActivePlayers.Text = SK.Text("WorldSelect_ActivePlayer", "Active Players");
            this.infoOverlayActivePlayers.Position = new Point(40, 190);
            this.infoOverlayActivePlayers.Size = new Size(240, 50);
            this.infoOverlayActivePlayers.Color = ARGBColors.Black;
            this.infoOverlayActivePlayers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.infoOverlayActivePlayers.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.infoOverlayPanel.addControl(this.infoOverlayActivePlayers);
            this.infoOverlayActivePlayersValue.Text = "?";
            this.infoOverlayActivePlayersValue.Position = new Point(0xb9, 190);
            this.infoOverlayActivePlayersValue.Size = new Size(200, 50);
            this.infoOverlayActivePlayersValue.Color = ARGBColors.Black;
            this.infoOverlayActivePlayersValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.infoOverlayActivePlayersValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.infoOverlayPanel.addControl(this.infoOverlayActivePlayersValue);
            List<WorldInfo> list = Program.profileLogin.GetWorldsBySupportCulture(ProfileLoginWindow.LastSelectedSupportCulture, showOwnWorldsStatus, showSpecialWorlds);
            this.BuildOnlineWorldList(list);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void language_Click()
        {
            this.lastLang = ((LocalizationLanguage) base.ClickedControl.Tag).CultureCode;
            this.updateFlagAlpha();
            List<WorldInfo> list = Program.profileLogin.GetWorldsBySupportCulture(((LocalizationLanguage) base.ClickedControl.Tag).CultureCode, showOwnWorldsStatus, showSpecialWorlds);
            this.wallScrollBar.Value = 0;
            this.wallScrollBarMoved();
            this.BuildOnlineWorldList(list);
        }

        private void mouseWheelMoved(int delta)
        {
            if (this.wallScrollBar.Visible)
            {
                if (delta < 0)
                {
                    this.wallScrollBar.scrollDown(40);
                }
                else if (delta > 0)
                {
                    this.wallScrollBar.scrollUp(40);
                }
            }
        }

        private void openInfoOverlay(WorldInfo info)
        {
            string text = ProfileLoginWindow.getWorldShortDesc(info);
            this.infoOverlay.Visible = true;
            if (InfoOverlayHeadings[info.KingdomsWorldID] == null)
            {
                Image image = WebStyleButtonImage.Generate(260, this.WebButtonheight + 4, text, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonRed, this.WebButtonRadius);
                InfoOverlayHeadings[info.KingdomsWorldID] = image;
            }
            this.infoOverlayHeading.Image = (Image) InfoOverlayHeadings[info.KingdomsWorldID];
            this.infoOverlayDurationValue.Text = "?";
            this.infoOverlayGameAgeValue.Text = "?";
            this.infoOverlayHousesValue.Text = "?";
            this.infoOverlayActivePlayersValue.Text = "?";
            if (InfoOverlayData[info.KingdomsWorldID] == null)
            {
                URLs.GameRPCAddress = info.HostExt;
                RemoteServices.Instance.init(URLs.GameRPC);
                RemoteServices.Instance.set_WorldInfo_UserCallBack(new RemoteServices.WorldInfo_UserCallBack(this.WorldInfoCallback));
                RemoteServices.Instance.WorldInfo();
            }
            else
            {
                this.infoOverlayFillinData((WorldInfoData) InfoOverlayData[info.KingdomsWorldID]);
            }
        }

        private void ownToggled()
        {
            showOwnWorldsStatus = this.showOwnWorlds.Checked;
            List<WorldInfo> list = Program.profileLogin.GetWorldsBySupportCulture(this.lastLang, showOwnWorldsStatus, showSpecialWorlds);
            this.BuildOnlineWorldList(list);
        }

        private void removeControlFromPanel(CustomSelfDrawPanel.CSDControl c)
        {
            base.removeControl(c);
        }

        private void specialWorldsClick()
        {
            showSpecialWorlds = 1;
            List<WorldInfo> list = Program.profileLogin.GetWorldsBySupportCulture("", showOwnWorldsStatus, showSpecialWorlds);
            this.BuildOnlineWorldList(list);
            this.addTitleButtons();
        }

        private void standardWorldsClick()
        {
            showSpecialWorlds = -1;
            List<WorldInfo> list = Program.profileLogin.GetWorldsBySupportCulture(this.lastLang, showOwnWorldsStatus, showSpecialWorlds);
            this.BuildOnlineWorldList(list);
            this.addTitleButtons();
        }

        public void update()
        {
            if (this.pulseTitleButton && (this.titleButton2 != null))
            {
                this.pulse++;
                if (this.pulse == 0x300)
                {
                    this.pulse = 0;
                }
                float num = this.pulse / 3;
                if (num > 128f)
                {
                    num = 256f - num;
                }
                num = (num + 128f) / 255f;
                this.titleButton2.Alpha = num;
                this.titleButton2.invalidate();
            }
        }

        private void updateFlagAlpha()
        {
            foreach (CustomSelfDrawPanel.CSDControl control in this.languageArea.Controls)
            {
                try
                {
                    if (control.Tag != null)
                    {
                        CustomSelfDrawPanel.CSDImage image = (CustomSelfDrawPanel.CSDImage) control;
                        if (((LocalizationLanguage) image.Tag).CultureCode == this.lastLang)
                        {
                            image.Colorise = ARGBColors.White;
                            this.selectedWorldRect.Position = new Point(image.Position.X - 2, image.Position.Y - 2);
                            this.selectedWorldRect2.Position = new Point(image.Position.X - 1, image.Position.Y - 1);
                        }
                        else
                        {
                            image.Colorise = Color.FromArgb(0xc0, 0xc0, 0xc0, 0xc0);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void wallScrollBarMoved()
        {
            int y = this.wallScrollBar.Value;
            this.wallScrollArea.Position = new Point(this.wallScrollArea.X, this.scrY - y);
            this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
        }

        private void WorldInfoCallback(WorldInfo_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if ((returnData.worldInfo != null) && (returnData.worldInfo.worldID != 0))
                {
                    InfoOverlayData[returnData.worldInfo.worldID] = returnData.worldInfo;
                    this.infoOverlayFillinData(returnData.worldInfo);
                }
                else
                {
                    this.infoOverlay.Visible = false;
                }
            }
        }

        public Image ClosedImage
        {
            get
            {
                if (closedImage == null)
                {
                    closedImage = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strClosed, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonRed, this.WebButtonRadius);
                }
                return closedImage;
            }
        }

        public Image CloseImage
        {
            get
            {
                if (closeImage == null)
                {
                    closeImage = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.strClose, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
                }
                return closeImage;
            }
        }

        public Image CloseImageOver
        {
            get
            {
                if (closeImageOver == null)
                {
                    closeImageOver = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.strClose, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return closeImageOver;
            }
        }

        public Image JoinImage
        {
            get
            {
                if (joinImage == null)
                {
                    joinImage = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strJoin, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return joinImage;
            }
        }

        public Image JoinImageOver
        {
            get
            {
                if (joinImageOver == null)
                {
                    joinImageOver = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strJoin, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return joinImageOver;
            }
        }

        public Image PlayImage
        {
            get
            {
                if (playImage == null)
                {
                    playImage = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strPlay, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return playImage;
            }
        }

        public Image PlayImageOver
        {
            get
            {
                if (playImageOver == null)
                {
                    playImageOver = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strPlay, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return playImageOver;
            }
        }

        public Image SelectAIImage
        {
            get
            {
                if (selectAIImage == null)
                {
                    selectAIImage = WebStyleButtonImage.Generate(260, this.WebButtonheight, this.strSelectAI, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonRedFaded, this.WebButtonRadius);
                }
                return selectAIImage;
            }
        }

        public Image SelectAIImageOver
        {
            get
            {
                if (selectAIImageOver == null)
                {
                    selectAIImageOver = WebStyleButtonImage.Generate(260, this.WebButtonheight, this.strSelectAI, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return selectAIImageOver;
            }
        }

        public Image SelectAIImageSelected
        {
            get
            {
                if (selectAIImageSelected == null)
                {
                    selectAIImageSelected = WebStyleButtonImage.Generate(260, this.WebButtonheight + 4, this.strAIWorlds, this.WebTextFontBoldCond, this.WebButtonYellow2, this.WebButtonRed, this.WebButtonRadius);
                }
                return selectAIImageSelected;
            }
        }

        public Image SelectImage
        {
            get
            {
                if (selectImage == null)
                {
                    selectImage = WebStyleButtonImage.Generate(260, this.WebButtonheight, this.strSelect, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonRedFaded, this.WebButtonRadius);
                }
                return selectImage;
            }
        }

        public Image SelectImageOver
        {
            get
            {
                if (selectImageOver == null)
                {
                    selectImageOver = WebStyleButtonImage.Generate(260, this.WebButtonheight, this.strSelect, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return selectImageOver;
            }
        }

        public Image SelectImageSelected
        {
            get
            {
                if (selectImageSelected == null)
                {
                    selectImageSelected = WebStyleButtonImage.Generate(260, this.WebButtonheight + 4, this.strStandardWorlds, this.WebTextFontBoldCond, this.WebButtonYellow2, this.WebButtonRed, this.WebButtonRadius);
                }
                return selectImageSelected;
            }
        }

        public Image SelectSpecialImage
        {
            get
            {
                if (selectSpecialImage == null)
                {
                    selectSpecialImage = WebStyleButtonImage.Generate(260, this.WebButtonheight, this.strSelectSpecial, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonRedFaded, this.WebButtonRadius);
                }
                return selectSpecialImage;
            }
        }

        public Image SelectSpecialImageOver
        {
            get
            {
                if (selectSpecialImageOver == null)
                {
                    selectSpecialImageOver = WebStyleButtonImage.Generate(260, this.WebButtonheight, this.strSelectSpecial, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return selectSpecialImageOver;
            }
        }

        public Image SelectSpecialImageSelected
        {
            get
            {
                if (selectSpecialImageSelected == null)
                {
                    selectSpecialImageSelected = WebStyleButtonImage.Generate(260, this.WebButtonheight + 4, this.strSpecialWorlds, this.WebTextFontBoldCond, this.WebButtonYellow2, this.WebButtonRed, this.WebButtonRadius);
                }
                return selectSpecialImageSelected;
            }
        }
    }
}

