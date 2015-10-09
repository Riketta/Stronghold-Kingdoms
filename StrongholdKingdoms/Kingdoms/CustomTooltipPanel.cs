namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class CustomTooltipPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDExtendingPanel background = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel cardTooltipDescription = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel cardTooltipEffect = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage cardTooltipImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage cardTooltipImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel cardTooltipName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel cardTooltipTimeLeft = new CustomSelfDrawPanel.CSDLabel();
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel housingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel housingValue = new CustomSelfDrawPanel.CSDLabel();
        private int lastData = -1;
        private string lastText = "";
        private int lastTooltip = -1;
        public const int MAX_TOOLTIP_WIDTH = 350;
        private CustomSelfDrawPanel.CSDLabel peasantsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel peasantsValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel spareWorkersLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel spareWorkersValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage timeImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel tooltipLabel = new CustomSelfDrawPanel.CSDLabel();

        public CustomTooltipPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void createCardTooltip(int tooltipID, int data, Form parent, bool force)
        {
            int totalSeconds;
            if (((this.lastTooltip == tooltipID) && (this.lastData == data)) && !force)
            {
                WorldData localWorldData = GameEngine.Instance.LocalWorldData;
                DateTime time3 = VillageMap.getCurrentServerTime();
                CardData userCardData = GameEngine.Instance.World.UserCardData;
                DateTime minValue = DateTime.MinValue;
                totalSeconds = 0;
                int length = userCardData.cards.Length;
                for (int i = 0; i < length; i++)
                {
                    int cardType = userCardData.cards[i];
                    if (cardType == data)
                    {
                        minValue = userCardData.cardsExpiry[i];
                        TimeSpan span2 = (TimeSpan) (minValue - time3);
                        CardTypes.getCardDuration(cardType);
                        totalSeconds = (int) span2.TotalSeconds;
                        if (totalSeconds < 0)
                        {
                            totalSeconds = 0;
                        }
                        if (span2.TotalDays > 100.0)
                        {
                            totalSeconds = -1;
                        }
                        break;
                    }
                }
            }
            else
            {
                this.lastText = "x";
                this.lastData = data;
                this.lastTooltip = tooltipID;
                parent.Size = new Size(300, 240);
                base.clearControls();
                this.background.Size = parent.Size;
                this.background.Position = new Point(0, 0);
                base.addControl(this.background);
                this.background.Create((Image) GFXLibrary.cardpanel_grey_9slice_left_top, (Image) GFXLibrary.cardpanel_grey_9slice_middle_top, (Image) GFXLibrary.cardpanel_grey_9slice_right_top, (Image) GFXLibrary.cardpanel_grey_9slice_left_middle, (Image) GFXLibrary.cardpanel_grey_9slice_middle_middle, (Image) GFXLibrary.cardpanel_grey_9slice_right_middle, (Image) GFXLibrary.cardpanel_grey_9slice_left_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_middle_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_right_bottom);
                this.cardTooltipName.Text = CardTypes.getDescriptionFromCard(data);
                this.cardTooltipName.Color = ARGBColors.Black;
                this.cardTooltipName.Position = new Point(100, 4);
                this.cardTooltipName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.cardTooltipName.Size = new Size(190, 40);
                this.cardTooltipName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.background.addControl(this.cardTooltipName);
                this.cardTooltipDescription.Text = CardTypes.getEffectTextFromCard(data);
                this.cardTooltipDescription.Color = ARGBColors.Black;
                this.cardTooltipDescription.Position = new Point(100, 50);
                this.cardTooltipDescription.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                this.cardTooltipDescription.Size = new Size(190, 100);
                this.cardTooltipDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.background.addControl(this.cardTooltipDescription);
                this.cardTooltipImage.Image = (Image) GFXLibrary.Instance.getCardImageBig(data);
                GFXLibrary.Instance.closeBigCardsLoader();
                this.cardTooltipImage.Position = new Point(4, 4);
                this.cardTooltipImage.Size = new Size(0x5c, 0x83);
                this.background.addControl(this.cardTooltipImage);
                switch (CardTypes.getColourFromCard(data))
                {
                    case 1:
                        this.cardTooltipImage2.Image = (Image) GFXLibrary.BlueCardOverlayBig;
                        break;

                    case 2:
                        this.cardTooltipImage2.Image = (Image) GFXLibrary.GreenCardOverlayBig;
                        break;

                    case 3:
                        this.cardTooltipImage2.Image = (Image) GFXLibrary.PurpleCardOverlayBig;
                        break;

                    case 4:
                        this.cardTooltipImage2.Image = (Image) GFXLibrary.RedCardOverlayBig;
                        break;

                    case 5:
                        this.cardTooltipImage2.Image = (Image) GFXLibrary.YellowCardOverlayBig;
                        break;
                }
                this.cardTooltipImage2.Size = this.cardTooltipImage.Size;
                this.cardTooltipImage.addControl(this.cardTooltipImage2);
                if (tooltipID != 0x2710)
                {
                    if (CardTypes.getCardSubType(data) == 0xc00)
                    {
                        this.cardTooltipTimeLeft.Text = SK.Text("TOOLTIP_CARD_INSTANT", "Instant Card");
                    }
                    else
                    {
                        int num5 = CardTypes.getCardDuration(data);
                        if ((num5 > 0x474a) || (num5 == 0))
                        {
                            this.cardTooltipTimeLeft.Text = SK.Text("TOOLTIP_CARD_EXPIRES", "Expires when used");
                        }
                        else
                        {
                            int secsLeft = (num5 * 60) * 60;
                            string str2 = VillageMap.createBuildTimeString(secsLeft);
                            this.cardTooltipTimeLeft.Text = SK.Text("TOOLTIP_CARD_DURATION", "Duration") + " : " + str2;
                        }
                    }
                }
                else
                {
                    WorldData data1 = GameEngine.Instance.LocalWorldData;
                    DateTime time = VillageMap.getCurrentServerTime();
                    CardData data2 = GameEngine.Instance.World.UserCardData;
                    DateTime time2 = DateTime.MinValue;
                    int num = 0;
                    int num2 = data2.cards.Length;
                    for (int j = 0; j < num2; j++)
                    {
                        int num4 = data2.cards[j];
                        if (num4 == data)
                        {
                            time2 = data2.cardsExpiry[j];
                            TimeSpan span = (TimeSpan) (time2 - time);
                            CardTypes.getCardDuration(num4);
                            num = (int) span.TotalSeconds;
                            if (num < 0)
                            {
                                num = 0;
                            }
                            if (span.TotalDays > 100.0)
                            {
                                num = -1;
                            }
                            break;
                        }
                    }
                    if (num < 0)
                    {
                        this.cardTooltipTimeLeft.Text = SK.Text("TOOLTIP_CARD_EXPIRES", "Expires when used");
                    }
                    else
                    {
                        string str = VillageMap.createBuildTimeString(num);
                        this.cardTooltipTimeLeft.Text = SK.Text("TOOLTIP_CARD_EXPIRES_IN", "Expires In") + " : " + str;
                    }
                }
                this.timeImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_time;
                this.timeImage.Position = new Point(10, 0x9e);
                this.background.addControl(this.timeImage);
                this.cardTooltipTimeLeft.Color = ARGBColors.Black;
                this.cardTooltipTimeLeft.Position = new Point(40, 160);
                this.cardTooltipTimeLeft.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                this.cardTooltipTimeLeft.Size = new Size(250, 40);
                this.cardTooltipTimeLeft.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.background.addControl(this.cardTooltipTimeLeft);
                string str3 = "";
                double num7 = CardTypes.getCardEffectValue(data);
                int num8 = (int) num7;
                NumberFormatInfo provider = null;
                if (num8 == num7)
                {
                    provider = GameEngine.NFI;
                }
                else if (CardTypes.getCardType(data) == 0x80d)
                {
                    provider = GameEngine.NFI_D2;
                }
                else
                {
                    provider = GameEngine.NFI_D1;
                }
                if (CardBarGDI.addX(data))
                {
                    str3 = "x" + num7.ToString("N", provider);
                }
                else if (CardBarGDI.addPlus(data))
                {
                    str3 = "+" + num7.ToString("N", provider);
                }
                else if (num7 != 0.0)
                {
                    str3 = num7.ToString("N", provider);
                }
                if (CardBarGDI.addPercent(data))
                {
                    str3 = str3 + "%";
                }
                if (str3.Length <= 0)
                {
                    if (tooltipID == 0x2775)
                    {
                        this.cardTooltipEffect.Text = getCardEffectString(data);
                        this.cardTooltipEffect.Color = ARGBColors.Black;
                        this.cardTooltipEffect.Position = new Point(10, 190);
                        this.cardTooltipEffect.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                        this.cardTooltipEffect.Size = new Size(290, 60);
                        this.cardTooltipEffect.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                        this.background.addControl(this.cardTooltipEffect);
                    }
                    else
                    {
                        this.cardTooltipEffect.Text = "";
                    }
                }
                else
                {
                    switch (CardTypes.getCardType(data))
                    {
                        case 0xbc0:
                        case 0xbc1:
                        case 0xbc2:
                            str3 = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BASIC_DIPLOMACY", "50% Chance of Averting Enemy Attacks");
                            break;

                        case 0xc05:
                        case 0xc06:
                        case 0xc07:
                        {
                            int index = GameEngine.Instance.World.getRank();
                            double num10 = GameEngine.Instance.LocalWorldData.ranks_HonourPerLevel[index];
                            num10 *= num7;
                            str3 = ((int) num10).ToString("N", GameEngine.NFI);
                            break;
                        }
                    }
                    this.cardTooltipEffect.Text = str3 + " " + getCardEffectString(data);
                    this.cardTooltipEffect.Color = ARGBColors.Black;
                    this.cardTooltipEffect.Position = new Point(10, 190);
                    this.cardTooltipEffect.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.cardTooltipEffect.Size = new Size(290, 60);
                    this.cardTooltipEffect.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                    this.background.addControl(this.cardTooltipEffect);
                }
                base.Invalidate();
                parent.Invalidate();
                return;
            }
            if (totalSeconds < 0)
            {
                this.cardTooltipTimeLeft.Text = SK.Text("TOOLTIP_CARD_EXPIRES", "Expires when used");
            }
            else
            {
                string str4 = VillageMap.createBuildTimeString(totalSeconds);
                this.cardTooltipTimeLeft.Text = SK.Text("TOOLTIP_CARD_EXPIRES_IN", "Expires In") + " : " + str4;
            }
        }

        public void createVillagePeasant(int tooltipID, int data, Form parent, bool force)
        {
            if (((this.lastTooltip != tooltipID) || (this.lastData != data)) || force)
            {
                this.lastText = "x";
                this.lastData = data;
                this.lastTooltip = tooltipID;
                Graphics graphics = base.CreateGraphics();
                Font font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                Size size = graphics.MeasureString(SK.Text("TOOLTIP_VILAGEMAP_TOTAL_PEASANTS", "Total Peasants"), font, 800).ToSize();
                Size size2 = graphics.MeasureString(SK.Text("TOOLTIP_VILAGEMAP_UNEMPLOYEED_PEASANTS", "Unemployed Peasants"), font, 800).ToSize();
                Size size3 = graphics.MeasureString(SK.Text("TOOLTIP_VILAGEMAP_HOUSING_CAPACITY", "Housing Capacity"), font, 800).ToSize();
                int width = size.Width;
                if (size2.Width > width)
                {
                    width = size2.Width;
                }
                if (size3.Width > width)
                {
                    width = size3.Width;
                }
                width += 60;
                graphics.Dispose();
                parent.Size = new Size(width, 100);
                base.clearControls();
                this.background.Size = parent.Size;
                this.background.Position = new Point(0, 0);
                base.addControl(this.background);
                this.background.Create((Image) GFXLibrary.cardpanel_grey_9slice_left_top, (Image) GFXLibrary.cardpanel_grey_9slice_middle_top, (Image) GFXLibrary.cardpanel_grey_9slice_right_top, (Image) GFXLibrary.cardpanel_grey_9slice_left_middle, (Image) GFXLibrary.cardpanel_grey_9slice_middle_middle, (Image) GFXLibrary.cardpanel_grey_9slice_right_middle, (Image) GFXLibrary.cardpanel_grey_9slice_left_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_middle_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_right_bottom);
                this.peasantsLabel.Text = SK.Text("TOOLTIP_VILAGEMAP_TOTAL_PEASANTS", "Total Peasants");
                this.peasantsLabel.Color = ARGBColors.Black;
                this.peasantsLabel.Position = new Point(10, 10);
                this.peasantsLabel.Font = font;
                this.peasantsLabel.Size = new Size(width - 20, 30);
                this.background.addControl(this.peasantsLabel);
                this.peasantsValue.Text = "0";
                this.peasantsValue.Color = ARGBColors.Black;
                this.peasantsValue.Position = new Point(10, 10);
                this.peasantsValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                this.peasantsValue.Size = new Size(width - 20, 30);
                this.peasantsValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.background.addControl(this.peasantsValue);
                this.spareWorkersLabel.Text = SK.Text("TOOLTIP_VILAGEMAP_UNEMPLOYEED_PEASANTS", "Unemployed Peasants");
                this.spareWorkersLabel.Color = ARGBColors.Black;
                this.spareWorkersLabel.Position = new Point(10, 40);
                this.spareWorkersLabel.Font = font;
                this.spareWorkersLabel.Size = new Size(width - 20, 30);
                this.background.addControl(this.spareWorkersLabel);
                this.spareWorkersValue.Text = "0";
                this.spareWorkersValue.Color = ARGBColors.Black;
                this.spareWorkersValue.Position = new Point(10, 40);
                this.spareWorkersValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                this.spareWorkersValue.Size = new Size(width - 20, 30);
                this.spareWorkersValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.background.addControl(this.spareWorkersValue);
                this.housingLabel.Text = SK.Text("TOOLTIP_VILAGEMAP_HOUSING_CAPACITY", "Housing Capacity");
                this.housingLabel.Color = ARGBColors.Black;
                this.housingLabel.Position = new Point(10, 70);
                this.housingLabel.Font = font;
                this.housingLabel.Size = new Size(width - 20, 30);
                this.background.addControl(this.housingLabel);
                this.housingValue.Text = "0";
                this.housingValue.Color = ARGBColors.Black;
                this.housingValue.Position = new Point(10, 70);
                this.housingValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                this.housingValue.Size = new Size(width - 20, 30);
                this.housingValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.background.addControl(this.housingValue);
                base.Invalidate();
                parent.Invalidate();
            }
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                this.peasantsValue.Text = village.m_totalPeople.ToString();
                this.spareWorkersValue.Text = village.m_spareWorkers.ToString();
                this.housingValue.Text = village.m_housingCapacity.ToString();
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

        public static string getCardEffectString(int card)
        {
            string str = "";
            int num = CardTypes.getCardType(card);
            if (num <= 0x70a)
            {
                if (num <= 0x30f)
                {
                    switch (num)
                    {
                        case 0x101:
                        case 0x102:
                        case 0x103:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MASTER_MASON", "Castle build speed");

                        case 260:
                        case 0x105:
                        case 0x106:
                            return str;

                        case 0x107:
                            return "";

                        case 0x108:
                        case 0x10b:
                        case 0x10c:
                            return "";

                        case 0x109:
                        case 0x10d:
                        case 270:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SURPRISE_ATTACK", "Knights Charge");

                        case 0x10a:
                        case 0x10f:
                        case 0x110:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_THE_LAST_STAND", "Knights Charge");

                        case 0x201:
                        case 0x202:
                        case 0x203:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ORCHARD_MANAGEMENT", "Increase in Apple Production");

                        case 0x204:
                        case 0x205:
                        case 0x206:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MILK_MAIDS", "Increase in Cheese Production");

                        case 0x207:
                        case 520:
                        case 0x209:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_PIG_BREEDING", "Increase in Meat Production");

                        case 0x20a:
                        case 0x20b:
                        case 0x20c:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_HARVESTING", "Increase in Bread Production");

                        case 0x20d:
                        case 0x20e:
                        case 0x20f:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_VETERAN_FARMER", "Increase in All Food Production");

                        case 0x210:
                        case 0x211:
                        case 530:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CROPPING", "Increase in Vegetable Production");

                        case 0x213:
                        case 0x214:
                        case 0x215:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FISHING", "Increase in Fish Production");

                        case 0x216:
                        case 0x217:
                        case 0x218:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SIMPLE_FEAST", "Increase in Popularity from Rations");

                        case 0x219:
                        case 0x21a:
                        case 0x21b:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_HOPS_TENDING", "Increase in Ale Production");

                        case 540:
                        case 0x21d:
                        case 0x21e:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BAR_KEEPING", "Increase in Popularity from Ale Consumption");

                        case 0x301:
                        case 770:
                        case 0x303:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_WOODSMAN_SHIP", "Increase in Wood Production");

                        case 0x304:
                        case 0x305:
                        case 0x306:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_STONE_CRAFT", "Increase in Stone Production");

                        case 0x307:
                        case 0x308:
                        case 0x309:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IRON_SMELTING", "Increase in Iron Production");

                        case 0x30a:
                        case 0x30b:
                        case 780:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_PITCH_EXTRACTION", "Increase in Pitch Production");

                        case 0x30d:
                        case 0x30e:
                        case 0x30f:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_HAULAGE", "Increase in All Raw Material Production");
                    }
                    return str;
                }
                if (num <= 0x51b)
                {
                    switch (num)
                    {
                        case 0x401:
                        case 0x402:
                        case 0x403:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BODKIN_CASTING", "Increase in Bow Production");

                        case 0x404:
                        case 0x405:
                        case 0x406:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_PIKE_CRAFT", "Increase in Pike Production");

                        case 0x407:
                        case 0x408:
                        case 0x409:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SWORD_CRAFT", "Increase in Sword Production");

                        case 0x40a:
                        case 0x40b:
                        case 0x40c:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ARMOUR_WORKING", "Increase in Armour Production");

                        case 0x40d:
                        case 0x40e:
                        case 0x40f:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SIEGE_ENGINEERS", "Increase in Catapult Production");

                        case 0x501:
                        case 0x502:
                        case 0x503:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_LAVISH_BANQUETING", "Increase in Honour When Holding a Banquet");

                        case 0x504:
                        case 0x505:
                        case 0x506:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_DEER_STALKING", "Increase in Venison Production");

                        case 0x507:
                        case 0x508:
                        case 0x509:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FURNITURE_MAKING", "Increase in Furniture Production");

                        case 0x50a:
                        case 0x50b:
                        case 0x50c:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_METAL_CRAFTS", "Increase in Metalware Production");

                        case 0x50d:
                        case 0x50e:
                        case 0x50f:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_TAILORING", "Increase in Clothes Production");

                        case 0x510:
                        case 0x511:
                        case 0x512:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_VINTNERS", "Increase in Wine Production");

                        case 0x513:
                        case 0x514:
                        case 0x515:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SALT_WORKING", "Increase in Salt Production");

                        case 0x516:
                        case 0x517:
                        case 0x518:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CULINARY_SKILLS", "Increase in Spice Production");

                        case 0x519:
                        case 0x51a:
                        case 0x51b:
                            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FINE_ATTIRE", "Increase in Silk Production");
                    }
                    return str;
                }
                switch (num)
                {
                    case 0x601:
                    case 0x602:
                    case 0x603:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CARTERS", "Increase in Merchant Speed");

                    case 0x604:
                        return str;

                    case 0x605:
                    case 0x606:
                    case 0x607:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_TRADE_CARAVANS", "Increase in Merchant Carrying Capacity");

                    case 0x708:
                    case 0x709:
                    case 0x70a:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CONSTRUCTION_TECHNIQUES", "Increase in Village Building Build Speed");
                }
                return str;
            }
            if (num <= 0xa06)
            {
                switch (num)
                {
                    case 0x801:
                    case 0x802:
                    case 0x803:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_POLITICS", "Increase in Monk Votes");

                    case 0x804:
                    case 0x805:
                    case 0x806:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_WEDDINGS", "Increase in Blessing Duration");

                    case 0x807:
                    case 0x808:
                    case 0x809:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_INQUISITION_TECHNIQUES", "Increase in Inquisition Duration");

                    case 0x80a:
                    case 0x80b:
                    case 0x80c:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_HEALING", "Increase in Healing");

                    case 0x80d:
                    case 0x80e:
                    case 0x80f:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_PROTECTION", "Increase in duration of interdiction given by monks");

                    case 0x810:
                    case 0x811:
                    case 0x812:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_EXCOMMUNICATION", "Increase in Excommunication Duration");

                    case 0x813:
                    case 0x814:
                    case 0x815:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ABSOLUTION", "Increase in Excommunication Duration Reduction");

                    case 0x816:
                    case 0x817:
                    case 0x818:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ENVOY", "Increase in Monks Speed");

                    case 0x901:
                    case 0x902:
                    case 0x903:
                        goto Label_0D00;

                    case 0x904:
                    case 0x905:
                    case 0x906:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BASIC_SCAVENGING", "Increase in Scout Carrying Capacity");

                    case 0xa01:
                    case 0xa02:
                    case 0xa03:
                        goto Label_0D3F;

                    case 0xa04:
                    case 0xa05:
                    case 0xa06:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BASIC_RECRUITMENT", "Cost of Troop Recruitment");
                }
                return str;
            }
            if (num <= 0xb0f)
            {
                switch (num)
                {
                    case 0xa81:
                    case 0xa82:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SCOUTING_RANGE", "Increase in your Honour Range");

                    case 0xa83:
                    case 0xa84:
                    case 0xa85:
                        goto Label_0D00;

                    case 0xa86:
                    case 0xa87:
                    case 0xa88:
                        goto Label_0D3F;

                    case 0xb01:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ADVANCED_HOUSEKEEPING", "Increase in House Capacity");

                    case 0xb02:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ADVANCED_GRANARIES", "Increase in Granary Capacity");

                    case 0xb03:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ADVANCED_STOCKPILING", "Increase in Stockpile Capacity");

                    case 0xb04:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ADVANCED_CELLARING", "Increase in Inn Capacity");

                    case 0xb05:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ADVANCED_ARMOURIES", "Increase in Armoury Capacity");

                    case 0xb06:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_EXPANDED_KEEP_STORAGE", "Increase in Village Hall Capacity");

                    case 0xb07:
                    case 0xb08:
                    case 0xb09:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BASIC_CULTURE", "Increase in Popularity To Honour Multiplier");

                    case 0xb0a:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FAIRER_JUSTICE", "Reduction in Negative Popularity from Justice Buildings");

                    case 0xb0b:
                    case 0xb0c:
                    case 0xb0d:
                    case 0xb0e:
                        return "????";

                    case 0xb0f:
                        return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FESTIVAL", "Popularity Boost");
                }
                return str;
            }
            switch (num)
            {
                case 0xb41:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_COMPLETED_CONTRACT", "Trade Completed Immediately");

                case 0xb42:
                case 0xb43:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ADVANCED_COMPLETED_CONTRACT", "Trades Completed Immediately");

                case 0xb44:
                case 0xb45:
                case 0xb46:
                    return str;

                case 0xb47:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_COMPLETED_DELIVERY", "Delivery Completed Immediately");

                case 0xb48:
                case 0xb49:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ADVANCED_COMPLETED_DELIVERY", "Deliveries Completed Immediately");

                case 0xb81:
                case 0xb82:
                case 0xb83:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IMPROVED_GUARD_HOUSES", "Extra Spaces for Troops in your Castle");

                case 0xb84:
                case 0xb85:
                case 0xb86:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IMPROVED_WOODEN_DEFENCES", "Stronger Wooden Castle Structures");

                case 0xb87:
                case 0xb88:
                case 0xb89:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IMPROVED_STONE_WALLS", "Stronger Stone Walls");

                case 0xb8a:
                case 0xb8b:
                case 0xb8c:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IMPROVED_STONE_STRUCTURES", "Stronger Stone Structures");

                case 0xb8d:
                case 0xb8e:
                case 0xb8f:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IMPROVED_MOATS", "Deeper Moats");

                case 0xb90:
                case 0xb91:
                case 0xb92:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IMPROVED_PITS", "More Damage from Pits");

                case 0xb93:
                case 0xb94:
                case 0xb95:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IMPROVED_OIL_POTS", "Extra Range of Oil Pots");

                case 0xb96:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_KNIGHTS_CHARGE", "Increase in Knights Speed");

                case 0xb97:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_EXPERT_TURRETS", "Turret Firing Speed");

                case 0xb98:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_EXPERT_TUNNELLING", "Troops From a Tunnel");

                case 0xb99:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_EXPERT_BALLISTAE", "Ballistae Firing Speed");

                case 0xb9a:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SUPER_TAX", "Extra Tax Band");

                case 0xb9b:
                case 0xb9c:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SUPER_TAX_ADVANCED", "Extra Tax Bands");

                case 0xb9d:
                case 0xb9e:
                case 0xb9f:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_EXTRA_TAX", "Increase in Tax Collected");

                case 0xba0:
                case 0xba1:
                case 0xba2:
                case 0xba3:
                case 0xba4:
                case 0xba5:
                case 0xba6:
                case 0xba7:
                case 0xba8:
                case 0xba9:
                case 0xbaa:
                case 0xbab:
                case 0xbac:
                case 0xbad:
                case 0xbae:
                case 0xbaf:
                case 0xbb0:
                case 0xbb1:
                case 0xbb2:
                case 0xbb3:
                case 0xbb4:
                case 0xbb5:
                case 0xbb6:
                case 0xbb7:
                case 0xbb8:
                case 0xbb9:
                case 0xbba:
                case 0xbbb:
                case 0xbbc:
                case 0xbbd:
                case 0xbbe:
                case 0xbbf:
                case 0xbc0:
                case 0xbc1:
                case 0xbc2:
                case 0xbc3:
                case 0xbc4:
                case 0xbc5:
                case 0xbc6:
                case 0xbc7:
                case 0xbc8:
                case 0xbc9:
                case 0xbca:
                case 0xbcb:
                case 0xbcc:
                case 0xbcd:
                case 0xbce:
                case 0xbcf:
                case 0xbd0:
                case 0xbd1:
                case 0xbd2:
                case 0xbd3:
                case 0xbd4:
                case 0xbd5:
                case 0xbd6:
                case 0xbd7:
                case 0xbd8:
                case 0xbd9:
                case 0xbda:
                case 0xbdb:
                case 0xbdc:
                case 0xbdd:
                case 0xbde:
                case 0xbdf:
                case 0xbe0:
                case 0xbe1:
                case 0xbe2:
                case 0xbe3:
                case 0xbe4:
                case 0xbe5:
                case 0xbe6:
                case 0xbe7:
                case 0xbe8:
                case 0xbe9:
                case 0xbea:
                case 0xbeb:
                case 0xbec:
                case 0xbed:
                case 0xbee:
                case 0xbef:
                case 0xbf0:
                case 0xbf1:
                case 0xbf2:
                case 0xbf3:
                case 0xbf4:
                case 0xbf5:
                case 0xbf6:
                case 0xbf7:
                case 0xbf8:
                case 0xbf9:
                case 0xbfa:
                case 0xbfb:
                case 0xbfc:
                case 0xbfd:
                case 0xbfe:
                case 0xbff:
                case 0xc00:
                case 0xc6d:
                case 0xc6e:
                case 0xc6f:
                case 0xc70:
                case 0xc71:
                case 0xc72:
                case 0xc73:
                case 0xc74:
                case 0xc75:
                case 0xc76:
                case 0xc77:
                case 0xc78:
                case 0xc79:
                case 0xc7a:
                case 0xc7b:
                case 0xc7c:
                case 0xc7d:
                case 0xc7e:
                case 0xc7f:
                case 0xc80:
                case 0xc84:
                case 0xc85:
                case 0xc86:
                case 0xc87:
                case 0xc88:
                case 0xc89:
                case 0xc8a:
                case 0xc8b:
                case 0xc8c:
                case 0xc8d:
                case 0xc8e:
                case 0xc8f:
                case 0xc90:
                case 0xc91:
                case 0xc92:
                case 0xc93:
                case 0xc94:
                case 0xc95:
                case 0xc96:
                case 0xc97:
                case 0xc98:
                case 0xc99:
                case 0xc9a:
                case 0xc9b:
                case 0xc9c:
                case 0xc9d:
                case 0xc9e:
                case 0xc9f:
                case 0xca0:
                case 0xca1:
                case 0xca2:
                case 0xca3:
                case 0xca4:
                case 0xca5:
                case 0xca6:
                case 0xca7:
                case 0xca8:
                case 0xca9:
                case 0xcaa:
                case 0xcab:
                case 0xcac:
                case 0xcad:
                case 0xcae:
                case 0xcaf:
                case 0xcb0:
                case 0xcb5:
                case 0xcb6:
                case 0xcb7:
                case 0xcb8:
                case 0xcb9:
                case 0xcba:
                case 0xcbb:
                case 0xcbc:
                case 0xcbd:
                case 0xcbe:
                case 0xcbf:
                    return str;

                case 0xc01:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_RESEARCH_POINT", "Research Point Added");

                case 0xc02:
                case 0xc03:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_RESEARCH_POINTS_2", "Research Points Added");

                case 0xc04:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FLAG", "Flag Added To Parish");

                case 0xc05:
                case 0xc06:
                case 0xc07:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BASIC_CHIVALRY", "Honour (Based on your Current Rank)");

                case 0xc08:
                case 0xc09:
                case 0xc0a:
                case 0xc0b:
                case 0xc0c:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_GOLD_HORDE", "Gold Added To Your Treasury");

                case 0xc0d:
                case 0xc0e:
                case 0xc0f:
                case 0xc10:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_APPLE_HAUL", "Apples Added To Your Granary");

                case 0xc11:
                case 0xc12:
                case 0xc13:
                case 0xc14:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CHEESE_HAUL", "Cheese Added To Your Granary");

                case 0xc15:
                case 0xc16:
                case 0xc17:
                case 0xc18:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MEAT_HAUL", "Meat Added To Your Granary");

                case 0xc19:
                case 0xc1a:
                case 0xc1b:
                case 0xc1c:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BREAD_HAUL", "Bread Added To Your Granary");

                case 0xc1d:
                case 0xc1e:
                case 0xc1f:
                case 0xc20:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_VEGETABLES_HAUL", "Vegetables Added To Your Granary");

                case 0xc21:
                case 0xc22:
                case 0xc23:
                case 0xc24:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FISH_HAUL", "Fish Added To Your Granary");

                case 0xc25:
                case 0xc26:
                case 0xc27:
                case 0xc28:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ALE_HAUL", "Ale Added To Your Inn");

                case 0xc29:
                case 0xc2a:
                case 0xc2b:
                case 0xc2c:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_WOOD_HAUL", "Wood Added To Your Stockpile");

                case 0xc2d:
                case 0xc2e:
                case 0xc2f:
                case 0xc30:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_STONE_HAUL", "Stone Added To Your Stockpile");

                case 0xc31:
                case 0xc32:
                case 0xc33:
                case 0xc34:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IRON_HAUL", "Iron Added To Your Stockpile");

                case 0xc35:
                case 0xc36:
                case 0xc37:
                case 0xc38:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_PITCH_HAUL", "Pitch Added To Your Stockpile");

                case 0xc39:
                case 0xc3a:
                case 0xc3b:
                case 0xc3c:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_VENISON_HAUL", "Venison Added To Your Keep");

                case 0xc3d:
                case 0xc3e:
                case 0xc3f:
                case 0xc40:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FURNITURE_HAUL", "Furniture Added To Your Keep");

                case 0xc41:
                case 0xc42:
                case 0xc43:
                case 0xc44:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_METALWARE_HAUL", "Metalware Added To Your Keep");

                case 0xc45:
                case 0xc46:
                case 0xc47:
                case 0xc48:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CLOTHES_HAUL", "Clothes Added To Your Keep");

                case 0xc49:
                case 0xc4a:
                case 0xc4b:
                case 0xc4c:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_WINE_HAUL", "Wine Added To Your Keep");

                case 0xc4d:
                case 0xc4e:
                case 0xc4f:
                case 0xc50:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SALT_HAUL", "Salt Added To Your Keep");

                case 0xc51:
                case 0xc52:
                case 0xc53:
                case 0xc54:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SPICES_HAUL", "Spices Added To Your Keep");

                case 0xc55:
                case 0xc56:
                case 0xc57:
                case 0xc58:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SILK_HAUL", "Silk Added To Your Keep");

                case 0xc59:
                case 0xc5a:
                case 0xc5b:
                case 0xc5c:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BOWS_HAUL", "Bows Added To Your Armoury");

                case 0xc5d:
                case 0xc5e:
                case 0xc5f:
                case 0xc60:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_PIKES_HAUL", "Pikes Added To Your Armoury");

                case 0xc61:
                case 0xc62:
                case 0xc63:
                case 0xc64:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ARMOUR_HAUL", "Armour Added To Your Armoury");

                case 0xc65:
                case 0xc66:
                case 0xc67:
                case 0xc68:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SWORDS_HAUL", "Swords Added To Your Armoury");

                case 0xc69:
                case 0xc6a:
                case 0xc6b:
                case 0xc6c:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CATAPULTS_HAUL", "Catapults Added To Your Armoury");

                case 0xc81:
                case 0xc82:
                case 0xc83:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CASTLE_DESIGNER", "Hours of ongoing Castle Construction Completed");

                case 0xcb1:
                case 0xcb2:
                case 0xcb3:
                case 0xcb4:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ACADEMIC_STUDY", "Hours of Research Completed");

                case 0xcc0:
                case 0xcc1:
                case 0xcc2:
                case 0xcc3:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_PEASANTS_SMALL", "Peasants");

                case 0xcc4:
                case 0xcc5:
                case 0xcc6:
                case 0xcc7:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_ARCHERS_SMALL", "Archers");

                case 0xcc8:
                case 0xcc9:
                case 0xcca:
                case 0xccb:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_PIKEMEN_SMALL", "Pikemen");

                case 0xccc:
                case 0xccd:
                case 0xcce:
                case 0xccf:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_SWORDSMEN_SMALL", "Swordsmen");

                case 0xcd0:
                case 0xcd1:
                case 0xcd2:
                case 0xcd3:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_CATAPULTS_SMALL", "Catapults");

                case 0xcd4:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_WALL_CONSTRUCTION_TEAM", "Hours of Castle Wall Construction Completed");

                case 0xcd5:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MOAT_DIGGING_TEAM", "Hours of Moat Construction Completed");

                case 0xcd6:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_PIT_CONSTRUCTION_TEAM", "Hours of Castle Pits Construction Completed");

                case 0xcd7:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_SCOUTS_SMALL", "Scout");

                case 0xcd8:
                case 0xcd9:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_SCOUTS_MEDIUM", "Scouts");

                case 0xcda:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_MONKS_SMALL", "Monk");

                case 0xcdb:
                case 0xcdc:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_MONKS_MEDIUM", "Monks");

                case 0xcdd:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_MERCHANTS_SMALL", "Merchant");

                case 0xcde:
                case 0xcdf:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_MERCHANTS_MEDIUM", "Merchants");

                case 0xce0:
                case 0xce1:
                case 0xce2:
                    return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_VILLAGERS_SMALL", "Villagers");

                default:
                    return str;
            }
        Label_0D00:
            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_HORSE_BREEDING", "Increase in Scout Speed");
        Label_0D3F:
            return SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BASIC_DISCIPLINE", "Increase in Army Speed");
        }

        public void hidingTooltip()
        {
            this.lastText = "";
            this.lastTooltip = -1;
            this.lastData = -1;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.Name = "CustomTooltipPanel";
            base.Size = new Size(600, 0x37);
            base.ResumeLayout(false);
        }

        public void setText(string text, int tooltipID, int data, Form parent, bool force)
        {
            if ((tooltipID == 0x2710) || (tooltipID == 0x2775))
            {
                this.createCardTooltip(tooltipID, data, parent, force);
            }
            else if (tooltipID == 0x8d)
            {
                this.createVillagePeasant(tooltipID, data, parent, force);
            }
            else if ((this.lastText != text) || force)
            {
                this.lastText = text;
                this.lastTooltip = tooltipID;
                base.clearControls();
                this.tooltipLabel.Text = text;
                this.tooltipLabel.Color = ARGBColors.Black;
                this.tooltipLabel.Position = new Point(2, 2);
                this.tooltipLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                Graphics graphics = base.CreateGraphics();
                Size size = graphics.MeasureString(text, this.tooltipLabel.Font, 350).ToSize();
                graphics.Dispose();
                this.tooltipLabel.Size = new Size(size.Width + 1, size.Height + 1);
                Size size2 = new Size((size.Width + 4) + 1, (size.Height + 4) + 1);
                if (!size2.Equals(parent.Size))
                {
                    parent.Size = size2;
                }
                this.tooltipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.background.Size = size2;
                this.background.Position = new Point(0, 0);
                base.addControl(this.background);
                this.background.Create((Image) GFXLibrary.cardpanel_grey_9slice_left_top, (Image) GFXLibrary.cardpanel_grey_9slice_middle_top, (Image) GFXLibrary.cardpanel_grey_9slice_right_top, (Image) GFXLibrary.cardpanel_grey_9slice_left_middle, (Image) GFXLibrary.cardpanel_grey_9slice_middle_middle, (Image) GFXLibrary.cardpanel_grey_9slice_right_middle, (Image) GFXLibrary.cardpanel_grey_9slice_left_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_middle_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_right_bottom);
                this.background.addControl(this.tooltipLabel);
                base.Invalidate();
                parent.Invalidate();
            }
        }

        public void update()
        {
        }
    }
}

