namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class WheelPanel : CustomSelfDrawPanel
    {
        private RewardImage centreRewardImage = new RewardImage();
        private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private const double DEG2RAD = 0.0174533;
        private CustomSelfDrawPanel.CSDExtendingPanel greenArea = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel helpLabel = new CustomSelfDrawPanel.CSDLabel();
        private static WheelPanel Instance;
        private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelTitle2 = new CustomSelfDrawPanel.CSDLabel();
        private int lastGlowSegment = -1;
        private float lastRotate = -1000f;
        private int m_cardAdded = -1;
        private WheelReward m_storedReward;
        private int m_wheelType = -1;
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDExtendingPanel MainPanel = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel numTicketsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage pegImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage pointerImage = new CustomSelfDrawPanel.CSDImage();
        private float pointerRotate;
        private float pointerRotateSpeed;
        private CustomSelfDrawPanel.CSDImage pointerShadowImage = new CustomSelfDrawPanel.CSDImage();
        private RewardImage prizeRewardImage = new RewardImage();
        private int pullbackCount;
        private CustomSelfDrawPanel.CSDLabel rewardDescription = new CustomSelfDrawPanel.CSDLabel();
        private RewardImage[] rewardImages = new RewardImage[20];
        private bool rewardImagesCreated;
        private List<WheelReward> rewards;
        private bool royal = true;
        private CustomSelfDrawPanel.CSDButton spinButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage spinGlow = new CustomSelfDrawPanel.CSDImage();
        private int spinMode;
        private float spinStopExtra;
        private CustomSelfDrawPanel.CSDImage starImage = new CustomSelfDrawPanel.CSDImage();
        private float starRotate;
        private float starRotateSpeed;
        private int starSpinCount;
        private int starSpinMode;
        private int targetSegment = 5;
        private CustomSelfDrawPanel.CSDImage wheelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDArea wheelLayer1 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea wheelLayer2 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea wheelLayer3 = new CustomSelfDrawPanel.CSDArea();

        public WheelPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void addCardPack(int packType, int amount)
        {
            if (GameEngine.Instance.World.ProfileUserCardPacks.ContainsKey(packType))
            {
                CardTypes.UserCardPack pack = GameEngine.Instance.World.ProfileUserCardPacks[packType];
                pack.Count += amount;
            }
            else
            {
                CardTypes.UserCardPack pack2 = new CardTypes.UserCardPack {
                    PackID = packType,
                    Count = amount
                };
                GameEngine.Instance.World.ProfileUserCardPacks[packType] = pack2;
            }
        }

        public static void ClearInstance()
        {
            Instance = null;
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.closeWheelPopup();
            InterfaceMgr.Instance.ParentForm.TopMost = true;
            InterfaceMgr.Instance.ParentForm.TopMost = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void giveReward()
        {
            switch (this.m_storedReward.rewardType)
            {
                case 200:
                    GameEngine.Instance.World.addGold((double) this.m_storedReward.rewardAmount);
                    break;

                case 0xca:
                    GameEngine.Instance.World.addFaithPoints((double) this.m_storedReward.rewardAmount);
                    break;

                case 0xcb:
                {
                    WorldMap world = GameEngine.Instance.World;
                    world.ProfileCardpoints += this.m_storedReward.rewardAmount;
                    break;
                }
                case 0xcc:
                    this.addCardPack(1, this.m_storedReward.rewardAmount);
                    break;

                case 0xcd:
                    this.addCardPack(4, this.m_storedReward.rewardAmount);
                    break;

                case 0xce:
                    this.addCardPack(0x1b, this.m_storedReward.rewardAmount);
                    break;

                case 0xcf:
                    this.addCardPack(3, this.m_storedReward.rewardAmount);
                    break;

                case 0xd0:
                    this.addCardPack(0x1c, this.m_storedReward.rewardAmount);
                    break;

                case 0xd1:
                    GameEngine.Instance.World.addResearchPoints(this.m_storedReward.rewardAmount);
                    break;

                case 210:
                    GameEngine.Instance.World.addTickets(this.m_wheelType, this.m_storedReward.rewardAmount);
                    this.numTicketsLabel.Text = SK.Text("WheelPanel_Spins", "Spins") + ": " + GameEngine.Instance.World.getTickets(this.m_wheelType).ToString();
                    break;

                case 0xd3:
                    this.addCardPack(1, this.m_storedReward.rewardAmount);
                    this.addCardPack(3, this.m_storedReward.rewardAmount);
                    this.addCardPack(4, this.m_storedReward.rewardAmount);
                    this.addCardPack(0x1b, this.m_storedReward.rewardAmount);
                    this.addCardPack(0x1c, this.m_storedReward.rewardAmount);
                    break;

                case 0xd4:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc05));
                    }
                    break;

                case 0xd5:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc2a));
                    }
                    break;

                case 0xd6:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc0e));
                    }
                    break;

                case 0xd7:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc59));
                    }
                    break;

                case 0xd8:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc12));
                    }
                    break;

                case 0xd9:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc2e));
                    }
                    break;

                case 0xda:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc61));
                    }
                    break;

                case 0xdb:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc5d));
                    }
                    break;

                case 220:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc06));
                    }
                    break;

                case 0xdd:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc07));
                    }
                    break;

                case 0xdf:
                    this.addCardPack(0x31, this.m_storedReward.rewardAmount);
                    break;

                case 0xe0:
                    this.addCardPack(50, this.m_storedReward.rewardAmount);
                    break;

                case 0xe1:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xcc0));
                    }
                    break;

                case 0xe2:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc81));
                    }
                    break;

                case 0xe3:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc2b));
                    }
                    break;

                case 0xe4:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc13));
                    }
                    break;

                case 0xe5:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc31));
                    }
                    break;

                case 230:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0x101));
                    }
                    break;

                case 0xe7:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc2f));
                    }
                    break;

                case 0xe8:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xcc5));
                    }
                    break;

                case 0xe9:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0x102));
                    }
                    break;

                case 0xea:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc82));
                    }
                    break;

                case 0xeb:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xcc6));
                    }
                    break;

                case 0xec:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0x903));
                    }
                    break;

                case 0xed:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xcc9));
                    }
                    break;

                case 0xee:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xccd));
                    }
                    break;

                case 0xef:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0x103));
                    }
                    break;

                case 240:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xcc7));
                    }
                    break;

                case 0xf1:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xc83));
                    }
                    break;

                case 0xf2:
                    if (this.m_cardAdded >= 0)
                    {
                        GameEngine.Instance.World.addProfileCard(this.m_cardAdded, CardTypes.getStringFromCard(0xcca));
                    }
                    break;
            }
            this.m_storedReward = null;
            this.updateSpinButton();
        }

        public void init(bool initialCall, int wheelType)
        {
            CustomSelfDrawPanel.CSDImage image2;
            this.m_wheelType = wheelType;
            Instance = this;
            base.clearControls();
            if (!this.rewardImagesCreated)
            {
                this.rewardImagesCreated = true;
                for (int i = 0; i < 20; i++)
                {
                    this.rewardImages[i] = new RewardImage();
                }
            }
            this.mainBackgroundImage.Image = GFXLibrary.dummy;
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = base.Size;
            this.mainBackgroundImage.Tile = true;
            base.addControl(this.mainBackgroundImage);
            this.MainPanel.Size = base.Size;
            this.MainPanel.Position = new Point(0, 0);
            this.mainBackgroundImage.addControl(this.MainPanel);
            this.MainPanel.Create((Image) GFXLibrary.cardpanel_panel_back_top_left, (Image) GFXLibrary.cardpanel_panel_back_top_mid, (Image) GFXLibrary.cardpanel_panel_back_top_right, (Image) GFXLibrary.cardpanel_panel_back_mid_left, (Image) GFXLibrary.cardpanel_panel_back_mid_mid, (Image) GFXLibrary.cardpanel_panel_back_mid_right, (Image) GFXLibrary.cardpanel_panel_back_bottom_left, (Image) GFXLibrary.cardpanel_panel_back_bottom_mid, (Image) GFXLibrary.cardpanel_panel_back_bottom_right);
            CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.cardpanel_panel_gradient_top_left,
                Size = GFXLibrary.cardpanel_panel_gradient_top_left.Size,
                Position = new Point(0, 0)
            };
            this.MainPanel.addControl(control);
            image2 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.cardpanel_panel_gradient_bottom_right,
                Size = GFXLibrary.cardpanel_panel_gradient_bottom_right.Size,
                Position = new Point((this.MainPanel.Width - ((Image) GFXLibrary.cardpanel_panel_gradient_bottom_right).Width) - 6, (this.MainPanel.Height - ((Image) GFXLibrary.cardpanel_panel_gradient_bottom_right).Height) - 6)
            };
            this.MainPanel.addControl(image2);
            this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
            this.closeImage.Size = this.closeImage.Image.Size;
            this.closeImage.setMouseOverDelegate(() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over, () => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal);
            this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
            this.closeImage.Position = new Point((base.Width - 14) - 0x11, 10);
            this.closeImage.CustomTooltipID = 0x2774;
            this.mainBackgroundImage.addControl(this.closeImage);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundImage, 0x20, new Point((base.Width - 40) - 40, 2));
            CustomSelfDrawPanel.CSDFill fill = new CustomSelfDrawPanel.CSDFill {
                FillColor = Color.FromArgb(0xff, 130, 0x81, 0x7e),
                Size = new Size(base.Width - 10, 1),
                Position = new Point(5, 0x22)
            };
            this.mainBackgroundImage.addControl(fill);
            this.labelTitle.Position = new Point(0x1b, 5);
            this.labelTitle.Size = new Size(600, 0x40);
            switch (this.m_wheelType)
            {
                case -1:
                    this.labelTitle.Text = SK.Text("WheelPanel_Royal_Wheel", "Quest Wheel");
                    break;

                case 0:
                    this.labelTitle.Text = SK.Text("WheelPanel_Treasure_Wheel_1", "Treasure Wheel Tier 1");
                    break;

                case 1:
                    this.labelTitle.Text = SK.Text("WheelPanel_Treasure_Wheel_2", "Treasure Wheel Tier 2");
                    break;

                case 2:
                    this.labelTitle.Text = SK.Text("WheelPanel_Treasure_Wheel_3", "Treasure Wheel Tier 3");
                    break;

                case 3:
                    this.labelTitle.Text = SK.Text("WheelPanel_Treasure_Wheel_4", "Treasure Wheel Tier 4");
                    break;

                case 4:
                    this.labelTitle.Text = SK.Text("WheelPanel_Treasure_Wheel_5", "Treasure Wheel Tier 5");
                    break;
            }
            this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelTitle.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
            this.labelTitle.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelTitle);
            if (this.m_wheelType == -1)
            {
                if (Program.mySettings.LanguageIdent == "it")
                {
                    this.labelTitle2.Position = new Point(300, 10);
                }
                else
                {
                    this.labelTitle2.Position = new Point(250, 10);
                }
                this.labelTitle2.Size = new Size(600, 0x40);
                string[] strArray = new string[] { "(", SK.Text("WheelPanel_Level", "Level"), " ", (Wheel.getWheelLevel(GameEngine.Instance.World.getRank()) + 1).ToString(), ")" };
                this.labelTitle2.Text = string.Concat(strArray);
                this.labelTitle2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.labelTitle2.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
                this.labelTitle2.Color = ARGBColors.Black;
                this.mainBackgroundImage.addControl(this.labelTitle2);
            }
            this.wheelImage.Image = (Image) GFXLibrary.wheel_wheel_royal;
            this.wheelImage.Position = new Point(3, 0x23);
            this.mainBackgroundImage.addControl(this.wheelImage);
            this.numTicketsLabel.Position = new Point(0x2d5, 0x2a);
            this.numTicketsLabel.Size = new Size(600, 0x36);
            this.numTicketsLabel.Text = SK.Text("WheelPanel_Spins", "Spins") + ": " + GameEngine.Instance.World.getTickets(this.m_wheelType).ToString();
            this.numTicketsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.numTicketsLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
            this.numTicketsLabel.Color = ARGBColors.Black;
            this.wheelImage.addControl(this.numTicketsLabel);
            this.starImage.Image = (Image) GFXLibrary.wheel_star[0];
            this.starImage.Position = new Point(0x22d, 0x2f);
            this.starImage.RotateCentre = new PointF(128f, 128f);
            this.starImage.Visible = false;
            this.starSpinMode = 0;
            this.wheelImage.addControl(this.starImage);
            if (this.royal)
            {
                this.pegImage.Image = (Image) GFXLibrary.wheel_icons[12];
            }
            else
            {
                this.pegImage.Image = (Image) GFXLibrary.wheel_icons[13];
            }
            this.pegImage.Position = new Point(0x26e, 0x73);
            this.pegImage.Visible = true;
            this.wheelImage.addControl(this.pegImage);
            this.rewardDescription.Position = new Point(0x2dd, 0x8a);
            this.rewardDescription.Size = new Size(0x9b, 80);
            this.rewardDescription.Text = "";
            this.rewardDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.rewardDescription.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
            this.rewardDescription.Color = ARGBColors.Black;
            this.wheelImage.addControl(this.rewardDescription);
            this.wheelLayer1.Position = new Point(0, 0);
            this.wheelLayer1.Size = this.wheelImage.Size;
            this.wheelImage.addControl(this.wheelLayer1);
            this.wheelLayer2.Position = new Point(0, 0);
            this.wheelLayer2.Size = this.wheelImage.Size;
            this.wheelImage.addControl(this.wheelLayer2);
            this.wheelLayer3.Position = new Point(0, 0);
            this.wheelLayer3.Size = this.wheelImage.Size;
            this.wheelImage.addControl(this.wheelLayer3);
            this.spinGlow.Image = (Image) GFXLibrary.wheel_icons[0];
            this.spinGlow.Position = new Point(0x205, 0x1a7);
            this.wheelImage.addControl(this.spinGlow);
            this.spinButton.ImageNorm = (Image) GFXLibrary.wheel_spinButton_royal[0];
            this.spinButton.ImageOver = (Image) GFXLibrary.wheel_spinButton_royal[1];
            this.spinButton.MoveOnClick = false;
            this.spinButton.Position = new Point(0x202, 0x1ba);
            this.spinButton.Text.Text = SK.Text("Wheel_Spin", "Spin");
            this.spinButton.TextYOffset = 0x20;
            this.spinButton.Text.Color = ARGBColors.Black;
            this.spinButton.Text.DropShadowColor = Color.FromArgb(160, 160, 160);
            this.spinButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.spinButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.spinCard));
            this.wheelImage.addControl(this.spinButton);
            int num2 = 5;
            this.pointerShadowImage.Image = (Image) GFXLibrary.wheel_arrowBlurShadow[0];
            this.pointerShadowImage.Position = new Point(0xd7 + num2, 0x6c + num2);
            this.pointerShadowImage.RotateCentre = new PointF(76.5f, 172.5f);
            this.pointerShadowImage.Alpha = 0.5f;
            this.wheelImage.addControl(this.pointerShadowImage);
            this.pointerImage.Image = (Image) GFXLibrary.wheel_arrowBlur_royal[0];
            this.pointerImage.Position = new Point(-num2, -num2);
            this.pointerImage.RotateCentre = new PointF(76.5f, 172.5f);
            this.pointerShadowImage.addControl(this.pointerImage);
            this.centreRewardImage.init(null, new Point(0x125, 0x11b));
            this.wheelImage.addControl(this.centreRewardImage);
            this.prizeRewardImage.init(null, new Point(690, 0xb7));
            this.wheelImage.addControl(this.prizeRewardImage);
            this.rewards = Wheel.getRewardWheel(GameEngine.Instance.World.getRank(), this.m_wheelType, GameEngine.Instance.LocalWorldData.AIWorld);
            if (this.rewards.Count == 20)
            {
                Random random = new Random();
                for (int j = 0; j < 20; j++)
                {
                    int num4 = random.Next(20);
                    WheelReward reward = this.rewards[j];
                    this.rewards[j] = this.rewards[num4];
                    this.rewards[num4] = reward;
                }
                for (int k = 0; k < 20; k++)
                {
                    Point point = this.rotatePoint(new Point(0, 230), (float) ((k * 0x12) + 9));
                    this.rewardImages[k].init(this.rewards[k], new Point(0x125 + point.X, 0x11b - point.Y), this.wheelLayer1, this.wheelLayer2, this.wheelLayer3);
                    this.wheelImage.addControl(this.rewardImages[k]);
                }
            }
            if (GameEngine.Instance.World.getTickets(this.m_wheelType) > 0)
            {
                this.helpLabel.Text = SK.Text("WheelPanel_spin_help", "Spin the wheel to win a prize");
            }
            else
            {
                this.helpLabel.Text = SK.Text("WheelPanel_nospin_help", "You have no spins available");
            }
            this.helpLabel.Position = new Point(0x2ae, 260);
            this.helpLabel.Size = new Size(0xc7, 180);
            this.helpLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.helpLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.helpLabel.Color = ARGBColors.Black;
            this.wheelImage.addControl(this.helpLabel);
            this.pointerRotate = 0f;
            this.pointerRotateSpeed = 0f;
            this.spinMode = -2;
            this.lastRotate = -1000f;
            this.updateSpinButton();
            base.Invalidate();
            this.update();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private bool isPointerInAngle(float testAngle, float minAngle, float maxAngle)
        {
            return (((testAngle >= minAngle) && (testAngle < maxAngle)) || ((((minAngle < 0f) && ((testAngle - 360f) >= minAngle)) && ((testAngle - 360f) < maxAngle)) || (((maxAngle >= 360f) && ((testAngle + 360f) >= minAngle)) && ((testAngle + 360f) < maxAngle))));
        }

        public Point rotatePoint(Point pos, float angle)
        {
            double d = angle;
            d *= -0.0174533;
            double num2 = Math.Cos(d);
            double num3 = Math.Sin(d);
            double x = pos.X;
            double y = pos.Y;
            double num6 = (x * num2) - (y * num3);
            double num7 = (x * num3) + (y * num2);
            return new Point((int) num6, (int) num7);
        }

        private static void s_SpinTheWheelCallback(SpinTheWheel_ReturnType returnData)
        {
            if (Instance != null)
            {
                Instance.spinTheWheelCallback(returnData);
            }
        }

        private void spinCard()
        {
            if (this.spinMode < 0)
            {
                GameEngine.Instance.playInterfaceSound("Wheel_start");
                this.startWheelSpinning();
                this.m_storedReward = null;
                this.prizeRewardImage.updateImage(null);
                this.rewardDescription.Text = "";
                this.updateSpinButton();
                GameEngine.Instance.World.useTickets(this.m_wheelType, 1);
                this.numTicketsLabel.Text = SK.Text("WheelPanel_Spins", "Spins") + ": " + GameEngine.Instance.World.getTickets(this.m_wheelType).ToString();
            }
        }

        private void spinTheWheelCallback(SpinTheWheel_ReturnType returnData)
        {
            if (!returnData.Success || (returnData.reward == null))
            {
                this.spinMode = -1;
                this.pointerRotateSpeed = 0f;
                GameEngine.Instance.World.addTickets(this.m_wheelType, 1);
                this.numTicketsLabel.Text = SK.Text("WheelPanel_Spins", "Spins") + ": " + GameEngine.Instance.World.getTickets(this.m_wheelType).ToString();
                this.updateSpinButton();
            }
            else
            {
                this.targetSegment = 0;
                this.m_storedReward = returnData.reward;
                for (int i = 0; i < 20; i++)
                {
                    if ((this.m_storedReward.rewardType == this.rewards[i].rewardType) && (this.m_storedReward.rewardAmount == this.rewards[i].rewardAmount))
                    {
                        this.targetSegment = i;
                        break;
                    }
                }
                this.spinMode = 2;
                this.m_cardAdded = returnData.cardAdded;
            }
        }

        private void startWheelSpinning()
        {
            this.lastGlowSegment = -1;
            this.pointerRotateSpeed = -2f;
            this.pullbackCount = 10;
            this.spinMode = 50;
            this.spinStopExtra = new Random().Next(0x12);
            this.starImage.Visible = false;
            this.starImage.Alpha = 0.01f;
            this.starSpinMode = 0;
        }

        public void update()
        {
            float num8;
            for (int i = 0; i < 20; i++)
            {
                this.rewardImages[i].update();
            }
            if ((this.pointerRotateSpeed != 0f) || (this.lastRotate != this.pointerRotate))
            {
                float pointerRotate = this.pointerRotate;
                if ((pointerRotate >= 179.9f) && (pointerRotate <= 180f))
                {
                    pointerRotate = 179.9f;
                }
                else if ((pointerRotate > 180f) && (pointerRotate <= 180.1f))
                {
                    pointerRotate = 180.1f;
                }
                this.pointerShadowImage.Rotate = pointerRotate;
                this.pointerImage.Rotate = pointerRotate;
                this.pointerRotate += this.pointerRotateSpeed;
                if (this.pointerRotate >= 360f)
                {
                    this.pointerRotate -= 360f;
                }
                this.lastRotate = this.pointerRotate;
                if (this.pointerRotateSpeed < 7f)
                {
                    this.pointerShadowImage.Image = (Image) GFXLibrary.wheel_arrowBlurShadow[0];
                    this.pointerImage.Image = (Image) GFXLibrary.wheel_arrowBlur_royal[0];
                }
                else if (this.pointerRotateSpeed < 15f)
                {
                    this.pointerShadowImage.Image = (Image) GFXLibrary.wheel_arrowBlurShadow[1];
                    this.pointerImage.Image = (Image) GFXLibrary.wheel_arrowBlur_royal[1];
                }
                else
                {
                    this.pointerShadowImage.Image = (Image) GFXLibrary.wheel_arrowBlurShadow[2];
                    this.pointerImage.Image = (Image) GFXLibrary.wheel_arrowBlur_royal[2];
                }
                if (((this.spinMode > 1) && (this.spinMode < 50)) && (this.pointerRotateSpeed < 18f))
                {
                    int index = (int) (this.pointerRotate / 18f);
                    if (((index >= 0) && (index < 20)) && (index != this.lastGlowSegment))
                    {
                        this.lastGlowSegment = index;
                        GameEngine.Instance.playInterfaceSound("Wheel_individual_segment_" + index.ToString());
                        this.rewardImages[index].highlight();
                    }
                }
                if (this.spinMode >= -1)
                {
                    int num4 = (int) (this.pointerRotate / 18f);
                    if ((num4 >= 0) && (num4 < 20))
                    {
                        this.centreRewardImage.fixedHighlight();
                        this.centreRewardImage.updateImage(this.rewards[num4]);
                    }
                }
                base.Invalidate(new Rectangle(0x6c, 130, 370, 370));
            }
            switch (this.spinMode)
            {
                case 0:
                    if (this.pointerRotateSpeed >= 30f)
                    {
                        this.pointerRotateSpeed = 30f;
                        this.spinMode = 1;
                        this.m_storedReward = null;
                        this.m_cardAdded = -1;
                        RemoteServices.Instance.set_SpinTheWheel_UserCallBack(new RemoteServices.SpinTheWheel_UserCallBack(WheelPanel.s_SpinTheWheelCallback));
                        RemoteServices.Instance.SpinTheRoyalWheel(-1, this.m_wheelType);
                        break;
                    }
                    this.pointerRotateSpeed += 0.4f;
                    break;

                case 2:
                {
                    float minAngle = ((((this.targetSegment * 0x12) + 9) - 100) - 0x4e) - this.spinStopExtra;
                    if (this.isPointerInAngle(this.pointerRotate, minAngle, minAngle + 90f))
                    {
                        this.spinMode++;
                    }
                    break;
                }
                case 3:
                {
                    float maxAngle = ((((this.targetSegment * 0x12) + 9) - 0xc4) - 0x4e) - this.spinStopExtra;
                    if (this.isPointerInAngle(this.pointerRotate, maxAngle - 30f, maxAngle))
                    {
                        this.spinMode++;
                    }
                    break;
                }
                case 4:
                    if (this.pointerRotateSpeed <= 5f)
                    {
                        if (this.pointerRotateSpeed > 1.4f)
                        {
                            this.pointerRotateSpeed -= 0.1f;
                        }
                        else
                        {
                            this.pointerRotateSpeed = 1.4f;
                            this.spinMode++;
                        }
                        break;
                    }
                    this.pointerRotateSpeed -= 0.2f;
                    break;

                case 5:
                {
                    float num7 = this.targetSegment * 0x12;
                    if (this.isPointerInAngle(this.pointerRotate, num7 - (this.spinStopExtra / 3f), num7 + 18f))
                    {
                        if (this.pointerRotateSpeed <= 0f)
                        {
                            this.pointerRotateSpeed = 0f;
                            this.prizeRewardImage.updateImage(this.m_storedReward);
                            this.pegImage.Visible = false;
                            this.rewardDescription.Text = Wheel.getRewardText(this.m_storedReward.rewardType, this.m_storedReward.rewardAmount, GameEngine.NFI);
                            this.giveReward();
                            this.spinMode = -1;
                            this.updateSpinButton();
                            this.starSpinMode = 1;
                            break;
                        }
                        if ((this.pointerRotateSpeed >= 0.5f) || this.isPointerInAngle(this.pointerRotate, num7, num7 + 18f))
                        {
                            this.pointerRotateSpeed -= 0.1f;
                        }
                    }
                    break;
                }
                case 50:
                    this.pullbackCount--;
                    if (this.pullbackCount == 0)
                    {
                        this.pointerRotateSpeed = 1f;
                        this.spinMode = 0x33;
                    }
                    break;

                case 0x33:
                    this.pointerRotateSpeed += 4f;
                    if (this.pointerRotateSpeed >= 30f)
                    {
                        this.spinMode = 1;
                        this.m_storedReward = null;
                        this.m_cardAdded = -1;
                        RemoteServices.Instance.set_SpinTheWheel_UserCallBack(new RemoteServices.SpinTheWheel_UserCallBack(WheelPanel.s_SpinTheWheelCallback));
                        RemoteServices.Instance.SpinTheRoyalWheel(-1, this.m_wheelType);
                    }
                    break;
            }
            if (this.starSpinMode <= 0)
            {
                return;
            }
            switch (this.starSpinMode)
            {
                case 1:
                    this.starImage.Image = (Image) GFXLibrary.wheel_star[2];
                    this.starImage.Visible = true;
                    if (this.prizeRewardImage.iconImage.Image != null)
                    {
                        GameEngine.Instance.playInterfaceSound("Wheel_star_start");
                        break;
                    }
                    this.starImage.Visible = false;
                    break;

                case 2:
                    this.starImage.Alpha += 0.2f;
                    if (this.starImage.Alpha > 1f)
                    {
                        this.starImage.Alpha = 1f;
                        this.starSpinMode++;
                    }
                    goto Label_06F0;

                case 3:
                    this.starSpinCount--;
                    if (this.starSpinCount == 0)
                    {
                        this.starSpinMode++;
                    }
                    goto Label_06F0;

                case 4:
                    this.starImage.Alpha -= 0.1f;
                    if (this.starImage.Alpha < 0f)
                    {
                        this.starImage.Alpha = 0f;
                        this.starImage.Visible = false;
                        this.starSpinMode = 0;
                        this.starRotateSpeed = 0f;
                    }
                    goto Label_06F0;

                default:
                    goto Label_06F0;
            }
            this.starImage.Alpha = 0.01f;
            this.starSpinMode++;
            this.starRotate = 0f;
            this.starRotateSpeed = 8f;
            this.starSpinCount = 90;
        Label_06F0:
            num8 = this.starRotate;
            if ((num8 >= 179.9f) && (num8 <= 180f))
            {
                num8 = 179.9f;
            }
            else if ((num8 > 180f) && (num8 <= 180.1f))
            {
                num8 = 180.1f;
            }
            this.starImage.Rotate = num8;
            this.starRotate += this.starRotateSpeed;
            if (this.starRotate >= 360f)
            {
                this.starRotate -= 360f;
            }
            if (this.starRotateSpeed <= 8f)
            {
                this.starImage.Image = (Image) GFXLibrary.wheel_star[0];
            }
            else if (this.starRotateSpeed < 15f)
            {
                this.starImage.Image = (Image) GFXLibrary.wheel_star[1];
            }
            else
            {
                this.starImage.Image = (Image) GFXLibrary.wheel_star[2];
            }
            this.starImage.invalidate();
        }

        private void updateSpinButton()
        {
            if ((GameEngine.Instance.World.getTickets(this.m_wheelType) == 0) || (this.spinMode >= 0))
            {
                this.spinButton.Enabled = false;
                this.spinGlow.Visible = false;
            }
            else
            {
                this.spinButton.Enabled = true;
                this.spinGlow.Visible = true;
            }
        }

        public class RewardImage : CustomSelfDrawPanel.CSDControl
        {
            private const float FADE_MAX = 10f;
            private float fadeValue;
            private CustomSelfDrawPanel.CSDImage glowImage = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage iconImage = new CustomSelfDrawPanel.CSDImage();
            private WheelReward m_reward;
            private CustomSelfDrawPanel.CSDImage numberImage = new CustomSelfDrawPanel.CSDImage();

            public void fixedHighlight()
            {
                this.fadeValue = 0f;
                this.glowImage.Alpha = 1f;
                base.invalidate();
            }

            public void highlight()
            {
                this.fadeValue = 10f;
                this.glowImage.Alpha = 1f;
                base.invalidate();
            }

            public void init(WheelReward reward, Point position)
            {
                this.init(reward, position, null, null, null);
            }

            public void init(WheelReward reward, Point position, CustomSelfDrawPanel.CSDArea layer1, CustomSelfDrawPanel.CSDArea layer2, CustomSelfDrawPanel.CSDArea layer3)
            {
                this.fadeValue = 0f;
                this.m_reward = reward;
                this.Position = new Point(position.X - 0x40, position.Y - 0x40);
                this.Size = new Size(0x80, 0x80);
                this.glowImage.Image = (Image) GFXLibrary.wheel_icons[0];
                if (layer1 != null)
                {
                    this.glowImage.Position = this.Position;
                    this.glowImage.Alpha = 0f;
                    layer1.addControl(this.glowImage);
                    this.updateImage(reward);
                    this.iconImage.Position = new Point(this.Position.X + 2, this.Position.Y + 2);
                    layer2.addControl(this.iconImage);
                    this.numberImage.Position = new Point(this.Position.X + 13, this.Position.Y + 0x4b);
                    layer3.addControl(this.numberImage);
                }
                else
                {
                    this.glowImage.Alpha = 0f;
                    base.addControl(this.glowImage);
                    this.updateImage(reward);
                    this.iconImage.Position = new Point(2, 2);
                    base.addControl(this.iconImage);
                    this.numberImage.Position = new Point(13, 0x4b);
                    base.addControl(this.numberImage);
                }
            }

            public bool update()
            {
                if (this.fadeValue > 0f)
                {
                    this.fadeValue--;
                    if (this.fadeValue < 0f)
                    {
                        this.fadeValue = 0f;
                    }
                    this.glowImage.Alpha = this.fadeValue / 10f;
                    base.invalidate();
                }
                return true;
            }

            public void updateImage(WheelReward reward)
            {
                this.m_reward = reward;
                this.numberImage.Visible = false;
                if (reward == null)
                {
                    this.iconImage.Image = null;
                    this.numberImage.Visible = false;
                }
                else
                {
                    bool flag = false;
                    bool flag2 = true;
                    switch (reward.rewardType)
                    {
                        case 200:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[5];
                            break;

                        case 0xca:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[4];
                            break;

                        case 0xcb:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[1];
                            break;

                        case 0xcc:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[10];
                            flag = true;
                            break;

                        case 0xcd:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[8];
                            flag = true;
                            break;

                        case 0xce:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[6];
                            flag = true;
                            break;

                        case 0xcf:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[9];
                            flag = true;
                            break;

                        case 0xd0:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[7];
                            flag = true;
                            break;

                        case 0xd1:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[2];
                            break;

                        case 210:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[3];
                            flag2 = false;
                            break;

                        case 0xd3:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[11];
                            flag = true;
                            break;

                        case 0xd4:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x1a];
                            flag2 = false;
                            break;

                        case 0xd5:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x11];
                            flag2 = false;
                            break;

                        case 0xd6:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x13];
                            flag2 = false;
                            break;

                        case 0xd7:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x10];
                            flag2 = false;
                            break;

                        case 0xd8:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[20];
                            flag2 = false;
                            break;

                        case 0xd9:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x12];
                            flag2 = false;
                            break;

                        case 0xda:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x15];
                            flag2 = false;
                            break;

                        case 0xdb:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x16];
                            flag2 = false;
                            break;

                        case 220:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x1b];
                            flag2 = false;
                            break;

                        case 0xdd:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x1c];
                            flag2 = false;
                            break;

                        case 0xde:
                            this.iconImage.Image = null;
                            this.numberImage.Visible = false;
                            flag2 = false;
                            break;

                        case 0xdf:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[30];
                            flag = true;
                            break;

                        case 0xe0:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x1d];
                            flag = true;
                            break;

                        case 0xe1:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x1f];
                            flag2 = false;
                            break;

                        case 0xe2:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x20];
                            flag2 = false;
                            break;

                        case 0xe3:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x21];
                            flag2 = false;
                            break;

                        case 0xe4:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x22];
                            flag2 = false;
                            break;

                        case 0xe5:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x23];
                            flag2 = false;
                            break;

                        case 230:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x24];
                            flag2 = false;
                            break;

                        case 0xe7:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x25];
                            flag2 = false;
                            break;

                        case 0xe8:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x26];
                            flag2 = false;
                            break;

                        case 0xe9:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x27];
                            flag2 = false;
                            break;

                        case 0xea:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[40];
                            flag2 = false;
                            break;

                        case 0xeb:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x29];
                            flag2 = false;
                            break;

                        case 0xec:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x2a];
                            flag2 = false;
                            break;

                        case 0xed:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x2b];
                            flag2 = false;
                            break;

                        case 0xee:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x2c];
                            flag2 = false;
                            break;

                        case 0xef:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x2d];
                            flag2 = false;
                            break;

                        case 240:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x2e];
                            flag2 = false;
                            break;

                        case 0xf1:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x2f];
                            flag2 = false;
                            break;

                        case 0xf2:
                            this.iconImage.Image = (Image) GFXLibrary.wheel_icons[0x30];
                            flag2 = false;
                            break;
                    }
                    if (flag2)
                    {
                        int index = -1;
                        if (flag)
                        {
                            switch (reward.rewardAmount)
                            {
                                case 1:
                                    index = 0x1c;
                                    break;

                                case 2:
                                    index = 0x1b;
                                    break;

                                case 3:
                                    index = 0x1a;
                                    break;

                                case 4:
                                    index = 0x19;
                                    break;

                                case 5:
                                    index = 0x18;
                                    break;

                                case 10:
                                    index = 0x17;
                                    break;
                            }
                        }
                        else
                        {
                            switch (reward.rewardAmount)
                            {
                                case 1:
                                    index = 0x16;
                                    break;

                                case 2:
                                    index = 0x15;
                                    break;

                                case 10:
                                    index = 20;
                                    break;

                                case 20:
                                    index = 0x1f;
                                    break;

                                case 40:
                                    index = 0x20;
                                    break;

                                case 50:
                                    index = 0x11;
                                    break;

                                case 0x19:
                                    index = 0x13;
                                    break;

                                case 0x23:
                                    index = 0x12;
                                    break;

                                case 0x4b:
                                    index = 0x10;
                                    break;

                                case 80:
                                    index = 0x21;
                                    break;

                                case 100:
                                    index = 15;
                                    break;

                                case 250:
                                    index = 13;
                                    break;

                                case 400:
                                    index = 11;
                                    break;

                                case 150:
                                    index = 14;
                                    break;

                                case 200:
                                    index = 12;
                                    break;

                                case 500:
                                    index = 10;
                                    break;

                                case 0x3e8:
                                    index = 9;
                                    break;

                                case 0x7d0:
                                    index = 8;
                                    break;

                                case 0x3a98:
                                    index = 0x1d;
                                    break;

                                case 0x4e20:
                                    index = 5;
                                    break;

                                case 0x1388:
                                    index = 7;
                                    break;

                                case 0x2710:
                                    index = 6;
                                    break;

                                case 0x61a8:
                                    index = 4;
                                    break;

                                case 0x7530:
                                    index = 30;
                                    break;

                                case 0x9c40:
                                    index = 0x22;
                                    break;

                                case 0x3d090:
                                    index = 1;
                                    break;

                                case 0x7a120:
                                    index = 0;
                                    break;

                                case 0xc350:
                                    index = 3;
                                    break;

                                case 0x186a0:
                                    index = 2;
                                    break;
                            }
                        }
                        if (index >= 0)
                        {
                            this.numberImage.Visible = true;
                            this.numberImage.Image = (Image) GFXLibrary.wheel_numbers[index];
                        }
                        else
                        {
                            this.numberImage.Visible = false;
                        }
                    }
                }
                base.invalidate();
            }
        }
    }
}

