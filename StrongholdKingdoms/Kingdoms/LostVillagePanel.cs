namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class LostVillagePanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDExtendingPanel background = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDArea backgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage bottomImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton btnHallOfLegends = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnLogout = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lostMessageLabel = new CustomSelfDrawPanel.CSDLabel();
        private int m_cardsMode = -1;
        private LostVillageWindow m_parent;
        private int m_secondAgeMessage;
        private CustomSelfDrawPanel.CSDImage topImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDFill transparentBackground = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDLabel wallOfText = new CustomSelfDrawPanel.CSDLabel();

        public LostVillagePanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void closePopup()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void hallOfLegendsClick()
        {
            string str = "";
            switch (Program.mySettings.LanguageIdent)
            {
                case "fr":
                    str = "http://fr.strongholdkingdoms.com/glory/HoH.php?worldid=2500&age=1";
                    break;

                case "de":
                    str = "http://de.strongholdkingdoms.com/glory/HoH.php?worldid=2500&age=1";
                    break;

                case "ru":
                    str = "http://ru.strongholdkingdoms.com/glory/HoH.php?worldid=2500&age=1";
                    break;

                case "es":
                    str = "http://es.strongholdkingdoms.com/glory/HoH.php?worldid=2500&age=1";
                    break;

                case "pl":
                    str = "http://pl.strongholdkingdoms.com/glory/HoH.php?worldid=2500&age=1";
                    break;

                case "tr":
                    str = "http://tr.strongholdkingdoms.com/glory/HoH.php?worldid=2500&age=1";
                    break;

                case "it":
                    str = "http://it.strongholdkingdoms.com/glory/HoH.php?worldid=2500&age=1";
                    break;

                case "pt":
                    str = "http://pt.strongholdkingdoms.com/glory/HoH.php?worldid=2500&age=1";
                    break;

                default:
                    str = "http://www.strongholdkingdoms.com/glory/HoH.php?worldid=2500&age=1";
                    break;
            }
            new Process { StartInfo = { FileName = str } }.Start();
        }

        public void init(LostVillageWindow parent, int age, int cardsMode)
        {
            this.m_secondAgeMessage = age;
            this.m_parent = parent;
            this.m_cardsMode = cardsMode;
            base.clearControls();
            this.transparentBackground.Size = base.Size;
            this.transparentBackground.FillColor = Color.FromArgb(0xff, 0, 0xff);
            base.addControl(this.transparentBackground);
            if (age != 0x3e8)
            {
                this.background.Position = new Point(0, 70);
                this.background.Size = new Size(base.Width, 0x1be);
                base.addControl(this.background);
                this.background.Create((Image) GFXLibrary._9sclice_fancy_top_left, (Image) GFXLibrary._9sclice_fancy_top_mid, (Image) GFXLibrary._9sclice_fancy_top_right, (Image) GFXLibrary._9sclice_fancy_mid_left, (Image) GFXLibrary._9sclice_fancy_mid_mid, (Image) GFXLibrary._9sclice_fancy_mid_right, (Image) GFXLibrary._9sclice_fancy_bottom_left, (Image) GFXLibrary._9sclice_fancy_bottom_mid, (Image) GFXLibrary._9sclice_fancy_bottom_right);
                this.background.ForceTiling();
                this.topImage.Image = (Image) GFXLibrary._9sclice_fancy_top_mid_over_01;
                this.topImage.Position = new Point((base.Width - this.topImage.Image.Width) / 2, 0);
                base.addControl(this.topImage);
                this.bottomImage.Image = (Image) GFXLibrary._9sclice_fancy_bottom_mid_over;
                this.bottomImage.Position = new Point((base.Width - this.bottomImage.Image.Width) / 2, (base.Height - this.bottomImage.Image.Height) - 5);
                base.addControl(this.bottomImage);
                this.backgroundArea.Position = new Point(0xab, 0x86);
                this.backgroundArea.Size = new Size(0x202, 340);
                base.addControl(this.backgroundArea);
            }
            else
            {
                this.backgroundImage.Position = new Point(0, 0);
                this.backgroundImage.Image = (Image) GFXLibrary.dominationEnd;
                base.addControl(this.backgroundImage);
                this.backgroundArea.Position = new Point(0, 0);
                this.backgroundArea.Size = this.backgroundImage.Size;
                base.addControl(this.backgroundArea);
            }
            this.btnLogout.ImageNorm = (Image) GFXLibrary.worldSelect_swap_norm;
            this.btnLogout.ImageOver = (Image) GFXLibrary.worldSelect_swap_over;
            this.btnLogout.ImageClick = (Image) GFXLibrary.worldSelect_swap_pushed;
            if (age != 0x3e8)
            {
                this.btnLogout.Position = new Point(260 - (this.btnLogout.ImageNorm.Width / 2), 0x147);
            }
            else
            {
                this.btnLogout.Position = new Point((base.Width / 2) - (this.btnLogout.ImageNorm.Width / 2), 0x23d);
            }
            this.btnLogout.Text.Text = SK.Text("GENERIC_Continue", "Continue");
            this.btnLogout.TextYOffset = -2;
            this.btnLogout.Text.Color = ARGBColors.White;
            this.btnLogout.Text.DropShadowColor = ARGBColors.Black;
            this.btnLogout.Text.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
            this.btnLogout.Text.Position = new Point(-3, 0);
            this.btnLogout.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.logoutClick));
            this.btnLogout.Enabled = true;
            this.backgroundArea.addControl(this.btnLogout);
            if (cardsMode < 0)
            {
                if (this.m_secondAgeMessage == 0)
                {
                    this.headerLabel.Text = SK.Text("WorldSelect_Villages_Lost", "Your village was lost due to") + " :";
                    this.headerLabel.Position = new Point(-50, 12);
                    this.headerLabel.Size = new Size(this.backgroundArea.Width + 100, 150);
                    this.headerLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
                    this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                    this.headerLabel.Color = ARGBColors.Black;
                    this.headerLabel.DropShadowColor = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.headerLabel);
                    if (GameEngine.Instance.World.lastAttacker.Length == 0)
                    {
                        this.lostMessageLabel.Text = SK.Text("WorldSelect_Inactivity", "Inactivity");
                    }
                    else if (GameEngine.Instance.World.lastAttacker == RemoteServices.Instance.UserName)
                    {
                        this.lostMessageLabel.Text = SK.Text("WorldSelect_Abandoning", "You Abandoned Your Village");
                    }
                    else
                    {
                        this.lostMessageLabel.Text = SK.Text("WorldSelect_Attacking_Player", "An Attacking Player") + " (" + GameEngine.Instance.World.lastAttacker + ")";
                    }
                    this.lostMessageLabel.Position = new Point(0, 0x2f);
                    this.lostMessageLabel.Size = new Size(this.backgroundArea.Width, 240);
                    this.lostMessageLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                    this.lostMessageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                    this.lostMessageLabel.Color = ARGBColors.Black;
                    this.lostMessageLabel.DropShadowColor = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.lostMessageLabel);
                    this.wallOfText.Text = SK.Text("WorldSelect_wall_of_text1", "Losing your village is all part of the game and this is only the beginning!") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_wall_of_text2", "There are no penalties for losing a village. You have the same amount of Gold, Honour and Faith Points as before and any unused Cards, Card Packs or Premium Tokens remain in your account. Quests, Research and Achievements are all unaffected and your Rank remains the same.") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_wall_of_text3", "With all your resources and abilities intact all that remains is for you to rebuild and rise to fight again!");
                    this.wallOfText.Position = new Point(0, 0x40);
                    this.wallOfText.Size = new Size(this.backgroundArea.Width, 260);
                    this.wallOfText.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                    this.wallOfText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.wallOfText.Color = ARGBColors.Black;
                    this.wallOfText.DropShadowColor = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.wallOfText);
                }
                else if (this.m_secondAgeMessage == 2)
                {
                    this.headerLabel.Text = SK.Text("WorldSelect_2ndAge_Header", "Welcome to the Second Age!");
                    this.headerLabel.Position = new Point(-50, 12);
                    this.headerLabel.Size = new Size(this.backgroundArea.Width + 100, 150);
                    this.headerLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
                    this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                    this.headerLabel.Color = ARGBColors.Black;
                    this.headerLabel.DropShadowColor = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.headerLabel);
                    this.wallOfText.Text = SK.Text("WorldSelect_2ndAge_body1", "A new rule set is now in effect:") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_2ndAge_body2", "1. Armies move across the world map at double speed.") + Environment.NewLine + SK.Text("WorldSelect_2ndAge_body3", "2. Three times as much Glory is earned for holding territory.") + Environment.NewLine + SK.Text("WorldSelect_2ndAge_body4", "3. Monks can influence voting at county level, with high voting costs and caps at county, province and country level.") + Environment.NewLine + SK.Text("WorldSelect_2ndAge_body5", "4. Interdicting and excommunicating villages costs twice the usual amount.") + Environment.NewLine + SK.Text("WorldSelect_2ndAge_body6", "5. At the end of the Second Age Glory Race players will be given unique treasures for their victory and a 'Champion Pack'.") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_2ndAge_body7", "For more information on the Second Age please check your mail.");
                    this.wallOfText.Position = new Point(0, 0x31);
                    this.wallOfText.Size = new Size(this.backgroundArea.Width, 260);
                    if ((Program.mySettings.LanguageIdent == "fr") || (Program.mySettings.LanguageIdent == "ru"))
                    {
                        this.wallOfText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    }
                    else
                    {
                        this.wallOfText.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
                    }
                    this.wallOfText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.wallOfText.Color = ARGBColors.Black;
                    this.wallOfText.DropShadowColor = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.wallOfText);
                    CustomSelfDrawPanel.WikiLinkControl.init(this.backgroundArea, 0x2d, new Point(0x201, -6));
                }
                else if (this.m_secondAgeMessage == 3)
                {
                    this.headerLabel.Text = SK.Text("WorldSelect_3rdAge_Header", "Welcome to the Third Age!");
                    this.headerLabel.Position = new Point(-50, 12);
                    this.headerLabel.Size = new Size(this.backgroundArea.Width + 100, 150);
                    this.headerLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
                    this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                    this.headerLabel.Color = ARGBColors.Black;
                    this.headerLabel.DropShadowColor = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.headerLabel);
                    this.wallOfText.Text = SK.Text("WorldSelect_2ndAge_body1", "A new rule set is now in effect:") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_3rdAge_body2", "1. Crown Princes may now access up to 30 villages") + Environment.NewLine + SK.Text("WorldSelect_3rdAge_body3", "2. Honour production has been increased by 900% for Banqueting and 300% for Popularity") + Environment.NewLine + SK.Text("WorldSelect_3rdAge_body4", "3. Goods have been cleared from all Markets, with prices reset to their starting levels") + Environment.NewLine + SK.Text("WorldSelect_3rdAge_body5", "4. All in-game Factions and Houses have been disbanded") + Environment.NewLine + SK.Text("WorldSelect_3rdAge_body6", "5. Certain upgradeable parish buildings can now gain additional levels") + Environment.NewLine + SK.Text("WorldSelect_3rdAge_body7", "6. The rate at which Glory is gained has been rebalanced") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_3rdAge_body8", "For more information on the Third Age please check your mail");
                    this.wallOfText.Position = new Point(0, 0x31);
                    this.wallOfText.Size = new Size(this.backgroundArea.Width, 260);
                    this.wallOfText.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
                    this.wallOfText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.wallOfText.Color = ARGBColors.Black;
                    this.wallOfText.DropShadowColor = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.wallOfText);
                    CustomSelfDrawPanel.WikiLinkControl.init(this.backgroundArea, 0x2e, new Point(0x201, -6));
                }
                else if (this.m_secondAgeMessage == 4)
                {
                    this.headerLabel.Text = SK.Text("WorldSelect_4thAge_Header", "Welcome to the Fourth Age!");
                    this.headerLabel.Position = new Point(-50, 12);
                    this.headerLabel.Size = new Size(this.backgroundArea.Width + 100, 150);
                    this.headerLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
                    this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                    this.headerLabel.Color = ARGBColors.Black;
                    this.headerLabel.DropShadowColor = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.headerLabel);
                    this.wallOfText.Text = SK.Text("WorldSelect_2ndAge_body1", "A new rule set is now in effect:") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_4thAge_body2", "1. Crown Princes may now own up to 40 villages.") + Environment.NewLine + SK.Text("WorldSelect_4thAge_body3", "2. Army and Scout movement speeds are three times faster than in the First Age.") + Environment.NewLine + SK.Text("WorldSelect_4thAge_body4", "3. Weapons can no longer be sold or purchased at Markets.") + Environment.NewLine + SK.Text("WorldSelect_4thAge_body5", "4. Goods have been cleared from all Markets, with prices reset to their starting level.") + Environment.NewLine + SK.Text("WorldSelect_4thAge_body6", "5. The Faith Point cost for Interdiction has been increased.") + Environment.NewLine + SK.Text("WorldSelect_4thAge_body7", "6. A Military School can be built in a parish, which gives access to Bombards.") + Environment.NewLine + SK.Text("WorldSelect_4thAge_body8", "7. Players who own more than 10 villages will no longer have to pay the extra honour cost to regain a village, if one of their villages is captured.") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_4thAge_body9", "For more information on the Fourth Age please check your mail.");
                    this.wallOfText.Position = new Point(0, 0x31);
                    this.wallOfText.Size = new Size(this.backgroundArea.Width, 260);
                    this.wallOfText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.wallOfText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.wallOfText.Color = ARGBColors.Black;
                    this.wallOfText.DropShadowColor = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.wallOfText);
                    CustomSelfDrawPanel.WikiLinkControl.init(this.backgroundArea, 0x30, new Point(0x201, -6));
                }
                else if (this.m_secondAgeMessage == 5)
                {
                    this.headerLabel.Text = SK.Text("WorldSelect_5thAge_Header", "Welcome to the Fifth Age!");
                    this.headerLabel.Position = new Point(-50, 12);
                    this.headerLabel.Size = new Size(this.backgroundArea.Width + 100, 150);
                    this.headerLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
                    this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                    this.headerLabel.Color = ARGBColors.Black;
                    this.headerLabel.DropShadowColor = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.headerLabel);
                    this.wallOfText.Text = SK.Text("WorldSelect_2ndAge_body1", "A new rule set is now in effect:") + Environment.NewLine + Environment.NewLine + SK.Text("FifthAge_Mail_03", "1. Military Schools can be upgraded to level 5.") + Environment.NewLine + SK.Text("FifthAge_Mail_04", "2. Treasure Castles are twice as likely to appear.") + Environment.NewLine + SK.Text("FifthAge_Mail_05", "3. All factions and houses have been disbanded.") + Environment.NewLine + SK.Text("FifthAge_Mail_06", "4. All capital forums and walls have been cleared.") + Environment.NewLine + SK.Text("FifthAge_Mail_07", "5. Only members of a House can be candidates for County elections.") + Environment.NewLine + SK.Text("FifthAge_Mail_08", "6. To be eligible for the office of sheriff, a candidate must belong to a House which controls at least 30% of the parishes in the county.") + Environment.NewLine + SK.Text("WorldSelect_5thAge_body8", "7. Large Houses gain Glory more slowly than small Houses.") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_5thAge_body9", "For more information on the Fifth Age please check your mail.");
                    this.wallOfText.Position = new Point(0, 0x31);
                    this.wallOfText.Size = new Size(this.backgroundArea.Width, 260);
                    this.wallOfText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.wallOfText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.wallOfText.Color = ARGBColors.Black;
                    this.wallOfText.DropShadowColor = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.wallOfText);
                    CustomSelfDrawPanel.WikiLinkControl.init(this.backgroundArea, 0x33, new Point(0x201, -6));
                }
                else if (this.m_secondAgeMessage == 10)
                {
                    this.headerLabel.Text = SK.Text("WorldSelect_Dom_Heading1", "Welcome to Domination World!");
                    this.headerLabel.Position = new Point(-50, 0);
                    this.headerLabel.Size = new Size(this.backgroundArea.Width + 100, 150);
                    this.headerLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
                    this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                    this.headerLabel.Color = ARGBColors.Black;
                    this.headerLabel.DropShadowColor = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.headerLabel);
                    this.wallOfText.Text = SK.Text("WorldSelect_Dom_body2", "New gameplay rules are in effect") + ":" + Environment.NewLine + Environment.NewLine + "1. " + SK.Text("WorldSelect_Dom_body3", "Interdiction cannot be used.") + Environment.NewLine + "2. " + SK.Text("WorldSelect_Dom_body4", "Build speeds quadrupled for buildings and castles in villages and capitals.") + Environment.NewLine + "3. " + SK.Text("WorldSelect_Dom_body5", "Research times reduced by half.") + Environment.NewLine + "4. " + SK.Text("WorldSelect_Dom_body6", "Time limit of 60 days, after which the world ends.") + Environment.NewLine + "5. " + SK.Text("WorldSelect_Dom_body7", "No Glory Rounds or Ages. The House with the most Glory when the world ends wins.") + Environment.NewLine + "6. " + SK.Text("WorldSelect_Dom_body8", "Unique Platinum Card Pack with rare cards for players in the winning House, as well as Ultimate, Super and Random Card Packs.") + Environment.NewLine + "7. " + SK.Text("WorldSelect_Dom_body9", "Other prizes for players who reach the top of each Leaderboard.") + Environment.NewLine + "8. " + SK.Text("WorldSelect_Dom_body10", "New Anglo-Saxon era map borders.") + Environment.NewLine + Environment.NewLine + SK.Text("WorldSelect_Dom_body11", "For more information on Domination World please check your mail.");
                    this.wallOfText.Position = new Point(0, 0x20);
                    this.wallOfText.Size = new Size(this.backgroundArea.Width, 0x11d);
                    if ((Program.mySettings.LanguageIdent == "de") || (Program.mySettings.LanguageIdent == "ru"))
                    {
                        this.wallOfText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    }
                    else
                    {
                        this.wallOfText.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
                    }
                    this.wallOfText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.wallOfText.Color = ARGBColors.Black;
                    this.wallOfText.DropShadowColor = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.wallOfText);
                    CustomSelfDrawPanel.WikiLinkControl.init(this.backgroundArea, 0x2f, new Point(0x201, -6));
                }
                else if (this.m_secondAgeMessage == 0x3e8)
                {
                    this.headerLabel.Text = SK.Text("PT_TUT_header1", "Congratulations!");
                    this.headerLabel.Position = new Point(0, 0x19);
                    this.headerLabel.Size = new Size(this.backgroundArea.Width, 150);
                    this.headerLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
                    this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                    this.headerLabel.DropShadowColor = ARGBColors.Black;
                    this.headerLabel.Color = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.headerLabel);
                    this.wallOfText.Text = SK.Text("DOMINATION_END_MESSAGE", "Goodness my lord! The end of days is now upon us! My how you've grown from that stumbling bumpkin I tutored long ago! It seems that the final reckoning has come and that this world is at its end!  Will your name also be recorded amongst the storied warriors in the Hall of Legends to be remembered for all eternity? This world has been a true test of your mettle and you are to be congratulated for having achieved so much here!");
                    this.wallOfText.Position = new Point(0x70, 250);
                    this.wallOfText.Size = new Size(this.backgroundArea.Width - 0xe0, 300);
                    this.wallOfText.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
                    this.wallOfText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                    this.wallOfText.DropShadowColor = ARGBColors.Black;
                    this.wallOfText.Color = ARGBColors.LightGray;
                    this.backgroundArea.addControl(this.wallOfText);
                    this.btnLogout.Text.Text = SK.Text("LogoutPanel_Swap_Worlds", "Swap Worlds");
                    this.btnHallOfLegends.ImageNorm = (Image) GFXLibrary.HOLlink;
                    this.btnHallOfLegends.OverBrighten = true;
                    this.btnHallOfLegends.MoveOnClick = true;
                    this.btnHallOfLegends.Position = new Point((base.Width / 2) - (this.btnHallOfLegends.ImageNorm.Width / 2), 0x1e3);
                    this.btnHallOfLegends.Text.Text = SK.Text("HALL_OF_LEGENDS", "Hall of Legends");
                    this.btnHallOfLegends.TextYOffset = -2;
                    this.btnHallOfLegends.Text.Color = ARGBColors.White;
                    this.btnHallOfLegends.Text.DropShadowColor = ARGBColors.Black;
                    this.btnHallOfLegends.Text.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
                    this.btnHallOfLegends.Text.Position = new Point(-3, 0);
                    this.btnHallOfLegends.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.hallOfLegendsClick));
                    this.btnHallOfLegends.Enabled = true;
                    this.backgroundArea.addControl(this.btnHallOfLegends);
                }
            }
            else
            {
                if (cardsMode == 0)
                {
                    this.headerLabel.Text = SK.Text("CARD_OFFERS_Super_Random_Pack", "Super Random Pack");
                }
                else
                {
                    this.headerLabel.Text = SK.Text("CARD_OFFERS_Ultimate_Random_Pack", "Ultimate Random Pack");
                }
                this.headerLabel.Position = new Point(-50, 12);
                this.headerLabel.Size = new Size(this.backgroundArea.Width + 100, 150);
                this.headerLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
                this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.headerLabel.Color = ARGBColors.Black;
                this.headerLabel.DropShadowColor = ARGBColors.LightGray;
                this.backgroundArea.addControl(this.headerLabel);
                if (cardsMode == 0)
                {
                    this.lostMessageLabel.Text = SK.Text("Cards_Super_Explanation", "Super random packs generally contain less silver ranked cards and give you a much greater chance of rare diamond and double diamond cards.");
                }
                else
                {
                    this.lostMessageLabel.Text = SK.Text("Cards_Ultimate_Explanation", "Ultimate random packs generally contain far less silver ranked cards and give you a much greater chance of super rare triple diamond and Sapphire cards.");
                }
                this.lostMessageLabel.Position = new Point(0, 0x2f);
                this.lostMessageLabel.Size = new Size(this.backgroundArea.Width, 240);
                this.lostMessageLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.lostMessageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.lostMessageLabel.Color = ARGBColors.Black;
                this.lostMessageLabel.DropShadowColor = ARGBColors.LightGray;
                this.backgroundArea.addControl(this.lostMessageLabel);
                this.wallOfText.Text = SK.Text("Cards_NewPacks", "While one can expect better cards across all ranks, some of the cards in this pack may be suitable only for higher ranked players.");
                this.wallOfText.Position = new Point(0x19, 250);
                this.wallOfText.Size = new Size(this.backgroundArea.Width - 50, 60);
                this.wallOfText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                this.wallOfText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
                this.wallOfText.Color = ARGBColors.Black;
                this.wallOfText.DropShadowColor = ARGBColors.LightGray;
                this.backgroundArea.addControl(this.wallOfText);
                CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage();
                if (cardsMode == 0)
                {
                    control.Image = (Image) GFXLibrary.SuperFan;
                }
                else
                {
                    control.Image = (Image) GFXLibrary.UltimateFan;
                }
                control.Position = new Point(120, 0x69);
                this.backgroundArea.addControl(control);
            }
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.Name = "LostVillagePanel";
            base.Size = new Size(600, 0x37);
            base.ResumeLayout(false);
        }

        private void logoutClick()
        {
            GameEngine.Instance.playInterfaceSound("AutoSelectVillageAreaPopup_logout");
            this.m_parent.closing = true;
            GameEngine.Instance.closeNoVillagePopup(false);
            if ((this.m_secondAgeMessage == 0) && (this.m_cardsMode < 0))
            {
                GameEngine.Instance.openSimpleSelectVillage();
            }
        }

        public void update()
        {
        }
    }
}

