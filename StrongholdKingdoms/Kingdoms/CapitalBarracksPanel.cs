namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class CapitalBarracksPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton attackInfoButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel attackingArchersLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel attackingCaptainsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel attackingCatapultsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel AttackingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel attackingPeasantsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel attackingPikmenLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel attackingSwordsmenLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel barracksLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel BottomHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel CurrentTroopsLabel = new CustomSelfDrawPanel.CSDLabel();
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDColorBar fullBar = new CustomSelfDrawPanel.CSDColorBar();
        private CustomSelfDrawPanel.CSDImage goldImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel goldLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inCastleArchersLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inCastleCaptainsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inCastleCatapultsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel InCastleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inCastlePeasantsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inCastlePikmenLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inCastleSwordsmenLabel = new CustomSelfDrawPanel.CSDLabel();
        public static CapitalBarracksPanel instance;
        private CustomSelfDrawPanel.CSDLabel localArchersLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel localCaptainsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel localCatapultsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel LocalLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel localPeasantsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel localPikmenLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel localSwordsmenLabel = new CustomSelfDrawPanel.CSDLabel();
        private DisbandTroopsPopup m_disbandPopup;
        private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private int makeArchers = 10;
        private int makeCatapults = 10;
        private int makePeasants = 10;
        private int makePikemen = 10;
        private int makeSwordsmen = 10;
        private CustomSelfDrawPanel.CSDLabel MaxLabelLabel = new CustomSelfDrawPanel.CSDLabel();
        private int mouseOver = -1;
        private CustomSelfDrawPanel.CSDButton reinforcementInfoButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel reinforcingArchersLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel reinforcingCaptainsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel reinforcingCatapultsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel ReinforcingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel reinforcingPeasantsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel reinforcingPikmenLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel reinforcingSwordsmenLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage smallPeasantImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel SpaceLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage troop1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troop2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troop3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troop4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troop5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel troopCtrl1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel troopCtrl2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel troopCtrl3Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel troopCtrl4Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel troopCtrl5Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage troopGold1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troopGold2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troopGold3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troopGold4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troopGold5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel troopGoldCost1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel troopGoldCost2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel troopGoldCost3Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel troopGoldCost4Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel troopGoldCost5Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton troopMake1Button1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake1Button20 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake1Button5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake2Button1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake2Button20 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake2Button5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake3Button1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake3Button20 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake3Button5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake4Button1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake4Button20 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake4Button5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake5Button1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake5Button20 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake5Button5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopsMade1Disband = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopsMade2Disband = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopsMade3Disband = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopsMade4Disband = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopsMade5Disband = new CustomSelfDrawPanel.CSDButton();

        public CapitalBarracksPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void attackInfoClick()
        {
        }

        private int calcMidSize(int numTroops)
        {
            if (numTroops < 6)
            {
                return 0xf423f;
            }
            if (numTroops < 30)
            {
                return 5;
            }
            return 10;
        }

        public void closeClick()
        {
            InterfaceMgr.Instance.setVillageTabSubMode(-1);
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
            base.clearControls();
            this.closeDisbandPopup();
        }

        public void closeDisbandPopup()
        {
            if (this.m_disbandPopup != null)
            {
                if (this.m_disbandPopup.Created)
                {
                    this.m_disbandPopup.Close();
                }
                this.m_disbandPopup = null;
            }
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        private void disbandTroopsClick()
        {
            if (base.OverControl != null)
            {
                int data = base.OverControl.Data;
                if (GameEngine.Instance.Village != null)
                {
                    this.closeDisbandPopup();
                    this.m_disbandPopup = new DisbandTroopsPopup();
                    this.m_disbandPopup.init(data);
                    this.m_disbandPopup.Show();
                }
            }
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

        public void init()
        {
            instance = this;
            base.clearControls();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.barracks_background;
            this.mainBackgroundImage.Position = new Point(0, 0);
            base.addControl(this.mainBackgroundImage);
            this.mainBackgroundArea.Position = new Point(0, 0);
            this.mainBackgroundArea.Size = new Size(0x3e0, 0x236);
            this.mainBackgroundImage.addControl(this.mainBackgroundArea);
            InterfaceMgr.Instance.setVillageHeading(SK.Text("CapitalBarracksPanel_Mercenaries", "Mercenaries"));
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x3b4, 10);
            this.closeButton.CustomTooltipID = 0x259;
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CapitalBarracksPanel_close");
            this.mainBackgroundArea.addControl(this.closeButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundArea, 13, new Point(0x382, 10));
            this.troopsMade1Disband.ImageNorm = (Image) GFXLibrary.barracks_little_button_normal;
            this.troopsMade1Disband.ImageOver = (Image) GFXLibrary.barracks_little_button_over;
            this.troopsMade1Disband.Position = new Point(0x6d, 0x2e);
            this.troopsMade1Disband.Text.Text = "0";
            this.troopsMade1Disband.Text.Color = ARGBColors.Black;
            this.troopsMade1Disband.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopsMade1Disband.TextYOffset = 0;
            this.troopsMade1Disband.Data = 70;
            this.troopsMade1Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "CapitalBarracksPanel_disband_peasants");
            this.troopsMade1Disband.CustomTooltipID = 600;
            this.mainBackgroundArea.addControl(this.troopsMade1Disband);
            this.troop1Image.Image = (Image) GFXLibrary.barracks_unit_peasant;
            this.troop1Image.Position = new Point(12, 12);
            this.mainBackgroundArea.addControl(this.troop1Image);
            this.troopGoldCost1Label.Text = "0";
            this.troopGoldCost1Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopGoldCost1Label.Position = new Point(0x45, 0xa5);
            this.troopGoldCost1Label.Size = new Size(0x2c, 0x2f);
            this.troopGoldCost1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopGoldCost1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop1Image.addControl(this.troopGoldCost1Label);
            int y = 0xe7;
            this.troopMake1Button1.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
            this.troopMake1Button1.ImageOver = (Image) GFXLibrary.button3comp_left_over;
            this.troopMake1Button1.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
            this.troopMake1Button1.Position = new Point(10, y);
            this.troopMake1Button1.Text.Text = "1";
            this.troopMake1Button1.TextYOffset = 1;
            this.troopMake1Button1.Text.Size = new Size(this.troopMake1Button1.Width - 5, this.troopMake1Button1.Height);
            this.troopMake1Button1.Text.Position = new Point(5, 0);
            this.troopMake1Button1.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake1Button1.Text.Color = ARGBColors.Black;
            this.troopMake1Button1.Data = 70;
            this.troopMake1Button1.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake1Button1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_peasants");
            this.troop1Image.addControl(this.troopMake1Button1);
            this.troopMake1Button5.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
            this.troopMake1Button5.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
            this.troopMake1Button5.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
            this.troopMake1Button5.Position = new Point(60, y);
            this.troopMake1Button5.Text.Text = "5";
            this.troopMake1Button5.TextYOffset = 1;
            this.troopMake1Button5.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake1Button5.Text.Color = ARGBColors.Black;
            this.troopMake1Button5.Data = 0x42e;
            this.troopMake1Button5.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake1Button5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_peasants");
            this.troop1Image.addControl(this.troopMake1Button5);
            this.troopMake1Button20.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
            this.troopMake1Button20.ImageOver = (Image) GFXLibrary.button3comp_right_over;
            this.troopMake1Button20.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
            this.troopMake1Button20.Position = new Point(0x6c, y);
            this.troopMake1Button20.Text.Text = "20";
            this.troopMake1Button20.TextYOffset = 1;
            this.troopMake1Button20.Text.Size = new Size(this.troopMake1Button20.Width - 5, this.troopMake1Button20.Height);
            this.troopMake1Button20.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake1Button20.Text.Color = ARGBColors.Black;
            this.troopMake1Button20.Data = 0x816;
            this.troopMake1Button20.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake1Button20.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_peasants");
            this.troop1Image.addControl(this.troopMake1Button20);
            this.troopGold1Image.Image = (Image) GFXLibrary.com_32_money;
            this.troopGold1Image.Position = new Point(0x75, 0xad);
            this.troop1Image.addControl(this.troopGold1Image);
            this.troopsMade2Disband.ImageNorm = (Image) GFXLibrary.barracks_little_button_normal;
            this.troopsMade2Disband.ImageOver = (Image) GFXLibrary.barracks_little_button_over;
            this.troopsMade2Disband.Position = new Point(270, 0x2e);
            this.troopsMade2Disband.Text.Text = "0";
            this.troopsMade2Disband.Text.Color = ARGBColors.Black;
            this.troopsMade2Disband.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopsMade2Disband.Data = 0x48;
            this.troopsMade2Disband.TextYOffset = 0;
            this.troopsMade2Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "CapitalBarracksPanel_disband_archers");
            this.troopsMade2Disband.CustomTooltipID = 600;
            this.mainBackgroundArea.addControl(this.troopsMade2Disband);
            this.troop2Image.Image = (Image) GFXLibrary.barracks_unit_archer;
            this.troop2Image.Position = new Point(0xad, 12);
            this.mainBackgroundArea.addControl(this.troop2Image);
            this.troopGoldCost2Label.Text = "0";
            this.troopGoldCost2Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopGoldCost2Label.Position = new Point(0x45, 0xa5);
            this.troopGoldCost2Label.Size = new Size(0x2c, 0x2f);
            this.troopGoldCost2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopGoldCost2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop2Image.addControl(this.troopGoldCost2Label);
            this.troopMake2Button1.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
            this.troopMake2Button1.ImageOver = (Image) GFXLibrary.button3comp_left_over;
            this.troopMake2Button1.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
            this.troopMake2Button1.Position = new Point(10, y);
            this.troopMake2Button1.Text.Text = "1";
            this.troopMake2Button1.TextYOffset = 1;
            this.troopMake2Button1.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake2Button1.Text.Color = ARGBColors.Black;
            this.troopMake2Button1.Text.Size = new Size(this.troopMake2Button1.Width - 5, this.troopMake2Button1.Height);
            this.troopMake2Button1.Text.Position = new Point(5, 0);
            this.troopMake2Button1.Data = 0x48;
            this.troopMake2Button1.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake2Button1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_archers");
            this.troop2Image.addControl(this.troopMake2Button1);
            this.troopMake2Button5.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
            this.troopMake2Button5.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
            this.troopMake2Button5.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
            this.troopMake2Button5.Position = new Point(60, y);
            this.troopMake2Button5.Text.Text = "5";
            this.troopMake2Button5.TextYOffset = 1;
            this.troopMake2Button5.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake2Button5.Text.Color = ARGBColors.Black;
            this.troopMake2Button5.Data = 0x430;
            this.troopMake2Button5.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake2Button5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_archers");
            this.troop2Image.addControl(this.troopMake2Button5);
            this.troopMake2Button20.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
            this.troopMake2Button20.ImageOver = (Image) GFXLibrary.button3comp_right_over;
            this.troopMake2Button20.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
            this.troopMake2Button20.Position = new Point(0x6c, y);
            this.troopMake2Button20.Text.Text = "20";
            this.troopMake2Button20.TextYOffset = 1;
            this.troopMake2Button20.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake2Button20.Text.Color = ARGBColors.Black;
            this.troopMake2Button20.Text.Size = new Size(this.troopMake2Button20.Width - 5, this.troopMake2Button20.Height);
            this.troopMake2Button20.Data = 0x818;
            this.troopMake2Button20.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake2Button20.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_archers");
            this.troop2Image.addControl(this.troopMake2Button20);
            this.troopGold2Image.Image = (Image) GFXLibrary.com_32_money;
            this.troopGold2Image.Position = new Point(0x75, 0xad);
            this.troop2Image.addControl(this.troopGold2Image);
            this.troopsMade3Disband.ImageNorm = (Image) GFXLibrary.barracks_little_button_normal;
            this.troopsMade3Disband.ImageOver = (Image) GFXLibrary.barracks_little_button_over;
            this.troopsMade3Disband.Position = new Point(0x1af, 0x2e);
            this.troopsMade3Disband.Text.Text = "0";
            this.troopsMade3Disband.Text.Color = ARGBColors.Black;
            this.troopsMade3Disband.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopsMade3Disband.TextYOffset = 0;
            this.troopsMade3Disband.Data = 0x49;
            this.troopsMade3Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "CapitalBarracksPanel_disband_pikemen");
            this.troopsMade3Disband.CustomTooltipID = 600;
            this.mainBackgroundArea.addControl(this.troopsMade3Disband);
            this.troop3Image.Image = (Image) GFXLibrary.barracks_unit_pikemen;
            this.troop3Image.Position = new Point(0x14e, 12);
            this.mainBackgroundArea.addControl(this.troop3Image);
            this.troopGoldCost3Label.Text = "0";
            this.troopGoldCost3Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopGoldCost3Label.Position = new Point(0x45, 0xa5);
            this.troopGoldCost3Label.Size = new Size(0x2c, 0x2f);
            this.troopGoldCost3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopGoldCost3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop3Image.addControl(this.troopGoldCost3Label);
            this.troopMake3Button1.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
            this.troopMake3Button1.ImageOver = (Image) GFXLibrary.button3comp_left_over;
            this.troopMake3Button1.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
            this.troopMake3Button1.Position = new Point(10, y);
            this.troopMake3Button1.Text.Text = "1";
            this.troopMake3Button1.TextYOffset = 1;
            this.troopMake3Button1.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake3Button1.Text.Color = ARGBColors.Black;
            this.troopMake3Button1.Text.Size = new Size(this.troopMake3Button1.Width - 5, this.troopMake3Button1.Height);
            this.troopMake3Button1.Text.Position = new Point(5, 0);
            this.troopMake3Button1.Data = 0x49;
            this.troopMake3Button1.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake3Button1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_pikemen");
            this.troop3Image.addControl(this.troopMake3Button1);
            this.troopMake3Button5.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
            this.troopMake3Button5.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
            this.troopMake3Button5.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
            this.troopMake3Button5.Position = new Point(60, y);
            this.troopMake3Button5.Text.Text = "5";
            this.troopMake3Button5.TextYOffset = 1;
            this.troopMake3Button5.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake3Button5.Text.Color = ARGBColors.Black;
            this.troopMake3Button5.Data = 0x431;
            this.troopMake3Button5.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake3Button5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_pikemen");
            this.troop3Image.addControl(this.troopMake3Button5);
            this.troopMake3Button20.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
            this.troopMake3Button20.ImageOver = (Image) GFXLibrary.button3comp_right_over;
            this.troopMake3Button20.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
            this.troopMake3Button20.Position = new Point(0x6c, y);
            this.troopMake3Button20.Text.Text = "20";
            this.troopMake3Button20.TextYOffset = 1;
            this.troopMake3Button20.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake3Button20.Text.Color = ARGBColors.Black;
            this.troopMake3Button20.Text.Size = new Size(this.troopMake3Button20.Width - 5, this.troopMake3Button20.Height);
            this.troopMake3Button20.Data = 0x819;
            this.troopMake3Button20.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake3Button20.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_pikemen");
            this.troop3Image.addControl(this.troopMake3Button20);
            this.troopGold3Image.Image = (Image) GFXLibrary.com_32_money;
            this.troopGold3Image.Position = new Point(0x75, 0xad);
            this.troop3Image.addControl(this.troopGold3Image);
            this.troopsMade4Disband.ImageNorm = (Image) GFXLibrary.barracks_little_button_normal;
            this.troopsMade4Disband.ImageOver = (Image) GFXLibrary.barracks_little_button_over;
            this.troopsMade4Disband.Position = new Point(0x250, 0x2e);
            this.troopsMade4Disband.Text.Text = "0";
            this.troopsMade4Disband.Text.Color = ARGBColors.Black;
            this.troopsMade4Disband.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopsMade4Disband.TextYOffset = 0;
            this.troopsMade4Disband.Data = 0x47;
            this.troopsMade4Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "CapitalBarracksPanel_disband_swordsmen");
            this.troopsMade4Disband.CustomTooltipID = 600;
            this.mainBackgroundArea.addControl(this.troopsMade4Disband);
            this.troop4Image.Image = (Image) GFXLibrary.barracks_unit_swordsman;
            this.troop4Image.Position = new Point(0x1ef, 12);
            this.mainBackgroundArea.addControl(this.troop4Image);
            this.troopGoldCost4Label.Text = "0";
            this.troopGoldCost4Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopGoldCost4Label.Position = new Point(0x45, 0xa5);
            this.troopGoldCost4Label.Size = new Size(0x2c, 0x2f);
            this.troopGoldCost4Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopGoldCost4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop4Image.addControl(this.troopGoldCost4Label);
            this.troopMake4Button1.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
            this.troopMake4Button1.ImageOver = (Image) GFXLibrary.button3comp_left_over;
            this.troopMake4Button1.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
            this.troopMake4Button1.Position = new Point(10, y);
            this.troopMake4Button1.Text.Text = "1";
            this.troopMake4Button1.TextYOffset = 1;
            this.troopMake4Button1.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake4Button1.Text.Color = ARGBColors.Black;
            this.troopMake4Button1.Text.Size = new Size(this.troopMake4Button1.Width - 5, this.troopMake4Button1.Height);
            this.troopMake4Button1.Text.Position = new Point(5, 0);
            this.troopMake4Button1.Data = 0x47;
            this.troopMake4Button1.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake4Button1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_swordsmen");
            this.troop4Image.addControl(this.troopMake4Button1);
            this.troopMake4Button5.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
            this.troopMake4Button5.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
            this.troopMake4Button5.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
            this.troopMake4Button5.Position = new Point(60, y);
            this.troopMake4Button5.Text.Text = "5";
            this.troopMake4Button5.TextYOffset = 1;
            this.troopMake4Button5.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake4Button5.Text.Color = ARGBColors.Black;
            this.troopMake4Button5.Data = 0x42f;
            this.troopMake4Button5.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake4Button5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_swordsmen");
            this.troop4Image.addControl(this.troopMake4Button5);
            this.troopMake4Button20.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
            this.troopMake4Button20.ImageOver = (Image) GFXLibrary.button3comp_right_over;
            this.troopMake4Button20.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
            this.troopMake4Button20.Position = new Point(0x6c, y);
            this.troopMake4Button20.Text.Text = "20";
            this.troopMake4Button20.TextYOffset = 1;
            this.troopMake4Button20.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake4Button20.Text.Color = ARGBColors.Black;
            this.troopMake4Button20.Text.Size = new Size(this.troopMake4Button20.Width - 5, this.troopMake4Button20.Height);
            this.troopMake4Button20.Data = 0x817;
            this.troopMake4Button20.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake4Button20.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_swordsmen");
            this.troop4Image.addControl(this.troopMake4Button20);
            this.troopGold4Image.Image = (Image) GFXLibrary.com_32_money;
            this.troopGold4Image.Position = new Point(0x75, 0xad);
            this.troop4Image.addControl(this.troopGold4Image);
            this.troopsMade5Disband.ImageNorm = (Image) GFXLibrary.barracks_little_button_normal;
            this.troopsMade5Disband.ImageOver = (Image) GFXLibrary.barracks_little_button_over;
            this.troopsMade5Disband.Position = new Point(0x2f1, 0x2e);
            this.troopsMade5Disband.Text.Text = "0";
            this.troopsMade5Disband.Text.Color = ARGBColors.Black;
            this.troopsMade5Disband.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopsMade5Disband.TextYOffset = 0;
            this.troopsMade5Disband.Data = 0x4a;
            this.troopsMade5Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "CapitalBarracksPanel_disband_catapults");
            this.troopsMade5Disband.CustomTooltipID = 600;
            this.mainBackgroundArea.addControl(this.troopsMade5Disband);
            this.troop5Image.Image = (Image) GFXLibrary.barracks_unit_catapult;
            this.troop5Image.Position = new Point(0x290, 12);
            this.mainBackgroundArea.addControl(this.troop5Image);
            this.troopGoldCost5Label.Text = "0";
            this.troopGoldCost5Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopGoldCost5Label.Position = new Point(0x45, 0xa5);
            this.troopGoldCost5Label.Size = new Size(0x2c, 0x2f);
            this.troopGoldCost5Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopGoldCost5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop5Image.addControl(this.troopGoldCost5Label);
            this.troopMake5Button1.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
            this.troopMake5Button1.ImageOver = (Image) GFXLibrary.button3comp_left_over;
            this.troopMake5Button1.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
            this.troopMake5Button1.Position = new Point(10, y);
            this.troopMake5Button1.Text.Text = "1";
            this.troopMake5Button1.TextYOffset = 1;
            this.troopMake5Button1.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake5Button1.Text.Color = ARGBColors.Black;
            this.troopMake5Button1.Text.Size = new Size(this.troopMake5Button1.Width - 5, this.troopMake5Button1.Height);
            this.troopMake5Button1.Text.Position = new Point(5, 0);
            this.troopMake5Button1.Data = 0x4a;
            this.troopMake5Button1.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake5Button1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_catapults");
            this.troop5Image.addControl(this.troopMake5Button1);
            this.troopMake5Button5.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
            this.troopMake5Button5.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
            this.troopMake5Button5.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
            this.troopMake5Button5.Position = new Point(60, y);
            this.troopMake5Button5.Text.Text = "5";
            this.troopMake5Button5.TextYOffset = 1;
            this.troopMake5Button5.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake5Button5.Text.Color = ARGBColors.Black;
            this.troopMake5Button5.Data = 0x432;
            this.troopMake5Button5.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake5Button5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_catapults");
            this.troop5Image.addControl(this.troopMake5Button5);
            this.troopMake5Button20.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
            this.troopMake5Button20.ImageOver = (Image) GFXLibrary.button3comp_right_over;
            this.troopMake5Button20.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
            this.troopMake5Button20.Position = new Point(0x6c, y);
            this.troopMake5Button20.Text.Text = "20";
            this.troopMake5Button20.TextYOffset = 1;
            this.troopMake5Button20.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake5Button20.Text.Color = ARGBColors.Black;
            this.troopMake5Button20.Text.Size = new Size(this.troopMake5Button20.Width - 5, this.troopMake5Button20.Height);
            this.troopMake5Button20.Data = 0x81a;
            this.troopMake5Button20.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake5Button20.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick), "CapitalBarracksPanel_make_catapults");
            this.troop5Image.addControl(this.troopMake5Button20);
            this.troopGold5Image.Image = (Image) GFXLibrary.com_32_money;
            this.troopGold5Image.Position = new Point(0x75, 0xad);
            this.troop5Image.addControl(this.troopGold5Image);
            int num2 = 0x49;
            this.goldImage.Image = (Image) GFXLibrary.com_32_money;
            this.goldImage.Position = new Point(num2 + 7, 340);
            this.mainBackgroundArea.addControl(this.goldImage);
            this.goldLabel.Text = "0";
            this.goldLabel.Color = ARGBColors.Black;
            this.goldLabel.Position = new Point((num2 + 7) + 0x20, 340);
            this.goldLabel.Size = new Size(300, this.goldImage.Image.Height);
            this.goldLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.goldLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mainBackgroundArea.addControl(this.goldLabel);
            this.CurrentTroopsLabel.Text = SK.Text("BARRACKS_Troops", "Troops") + ": 0";
            this.CurrentTroopsLabel.Color = ARGBColors.Black;
            this.CurrentTroopsLabel.Position = new Point(560, 0x15b);
            this.CurrentTroopsLabel.Size = new Size(200, 20);
            this.CurrentTroopsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.CurrentTroopsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mainBackgroundArea.addControl(this.CurrentTroopsLabel);
            this.MaxLabelLabel.Text = SK.Text("CapitalBarracksPanel_Max_Army_Size", "Max Army Size") + ": 0";
            this.MaxLabelLabel.Color = ARGBColors.Black;
            this.MaxLabelLabel.Position = new Point(770, 0x165);
            this.MaxLabelLabel.Size = new Size(200, 20);
            this.MaxLabelLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.MaxLabelLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mainBackgroundArea.addControl(this.MaxLabelLabel);
            this.fullBar.setImages((Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right, (Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right);
            this.fullBar.Number = 0.0;
            this.fullBar.MaxValue = 9.0;
            this.fullBar.SetMargin(2, 2, 2, 3);
            this.fullBar.Position = new Point(770, 0x153);
            this.mainBackgroundArea.addControl(this.fullBar);
            int num3 = 0x1ba;
            this.BottomHeaderLabel.Text = SK.Text("BARRACKS_Troop_Information", "Troop Information");
            this.BottomHeaderLabel.Color = Color.FromArgb(0xf2, 0xca, 0x76);
            this.BottomHeaderLabel.DropShadowColor = ARGBColors.Black;
            this.BottomHeaderLabel.Position = new Point(0x36, 0x192);
            this.BottomHeaderLabel.Size = new Size(0xe2, 0x1b);
            this.BottomHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.BottomHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.mainBackgroundArea.addControl(this.BottomHeaderLabel);
            int num4 = 0x19;
            this.LocalLabel.Text = SK.Text("BARRACKS_In_Barracks", "In Barracks");
            this.LocalLabel.Color = ARGBColors.Black;
            this.LocalLabel.Position = new Point(0x4f, num3);
            this.LocalLabel.Size = new Size(0xe2, 0x1b);
            this.LocalLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.LocalLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mainBackgroundArea.addControl(this.LocalLabel);
            this.InCastleLabel.Text = SK.Text("BARRACKS_In_Castle", "In Castle");
            this.InCastleLabel.Color = ARGBColors.Black;
            this.InCastleLabel.Position = new Point(0x4f, num3 + num4);
            this.InCastleLabel.Size = new Size(0xe2, 0x1b);
            this.InCastleLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.InCastleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mainBackgroundArea.addControl(this.InCastleLabel);
            this.AttackingLabel.Text = SK.Text("GENERIC_Attacking", "Attacking");
            this.AttackingLabel.Color = ARGBColors.Black;
            this.AttackingLabel.Position = new Point(0x4f, num3 + (2 * num4));
            this.AttackingLabel.Size = new Size(0xe2, 0x1b);
            this.AttackingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.AttackingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mainBackgroundArea.addControl(this.AttackingLabel);
            this.ReinforcingLabel.Text = SK.Text("BARRACKS_Reinforcing", "Reinforcing");
            this.ReinforcingLabel.Color = ARGBColors.Black;
            this.ReinforcingLabel.Position = new Point(0x4f, num3 + (3 * num4));
            this.ReinforcingLabel.Size = new Size(0xe2, 0x1b);
            this.ReinforcingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.ReinforcingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mainBackgroundArea.addControl(this.ReinforcingLabel);
            this.smallPeasantImage.Image = (Image) GFXLibrary.barracks_screen_bottom_units;
            this.smallPeasantImage.Position = new Point(370, 0x18a);
            this.mainBackgroundArea.addControl(this.smallPeasantImage);
            int num5 = 0x55;
            int x = 0x158;
            this.localPeasantsLabel.Text = "0";
            this.localPeasantsLabel.Color = ARGBColors.Black;
            this.localPeasantsLabel.Position = new Point(x, num3);
            this.localPeasantsLabel.Size = new Size(50, 0x1b);
            this.localPeasantsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.localPeasantsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.localPeasantsLabel);
            this.localArchersLabel.Text = "0";
            this.localArchersLabel.Color = ARGBColors.Black;
            this.localArchersLabel.Position = new Point(x + num5, num3);
            this.localArchersLabel.Size = new Size(50, 0x1b);
            this.localArchersLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.localArchersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.localArchersLabel);
            this.localPikmenLabel.Text = "0";
            this.localPikmenLabel.Color = ARGBColors.Black;
            this.localPikmenLabel.Position = new Point(x + (2 * num5), num3);
            this.localPikmenLabel.Size = new Size(50, 0x1b);
            this.localPikmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.localPikmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.localPikmenLabel);
            this.localSwordsmenLabel.Text = "0";
            this.localSwordsmenLabel.Color = ARGBColors.Black;
            this.localSwordsmenLabel.Position = new Point(x + (3 * num5), num3);
            this.localSwordsmenLabel.Size = new Size(50, 0x1b);
            this.localSwordsmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.localSwordsmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.localSwordsmenLabel);
            this.localCatapultsLabel.Text = "0";
            this.localCatapultsLabel.Color = ARGBColors.Black;
            this.localCatapultsLabel.Position = new Point(x + (4 * num5), num3);
            this.localCatapultsLabel.Size = new Size(50, 0x1b);
            this.localCatapultsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.localCatapultsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.localCatapultsLabel);
            this.localCaptainsLabel.Text = "0";
            this.localCaptainsLabel.Color = ARGBColors.Black;
            this.localCaptainsLabel.Position = new Point(x + (5 * num5), num3);
            this.localCaptainsLabel.Size = new Size(50, 0x1b);
            this.localCaptainsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.localCaptainsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.localCaptainsLabel);
            this.inCastlePeasantsLabel.Text = "0";
            this.inCastlePeasantsLabel.Color = ARGBColors.Black;
            this.inCastlePeasantsLabel.Position = new Point(x, num3 + num4);
            this.inCastlePeasantsLabel.Size = new Size(50, 0x1b);
            this.inCastlePeasantsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.inCastlePeasantsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.inCastlePeasantsLabel);
            this.inCastleArchersLabel.Text = "0";
            this.inCastleArchersLabel.Color = ARGBColors.Black;
            this.inCastleArchersLabel.Position = new Point(x + num5, num3 + num4);
            this.inCastleArchersLabel.Size = new Size(50, 0x1b);
            this.inCastleArchersLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.inCastleArchersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.inCastleArchersLabel);
            this.inCastlePikmenLabel.Text = "0";
            this.inCastlePikmenLabel.Color = ARGBColors.Black;
            this.inCastlePikmenLabel.Position = new Point(x + (2 * num5), num3 + num4);
            this.inCastlePikmenLabel.Size = new Size(50, 0x1b);
            this.inCastlePikmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.inCastlePikmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.inCastlePikmenLabel);
            this.inCastleSwordsmenLabel.Text = "0";
            this.inCastleSwordsmenLabel.Color = ARGBColors.Black;
            this.inCastleSwordsmenLabel.Position = new Point(x + (3 * num5), num3 + num4);
            this.inCastleSwordsmenLabel.Size = new Size(50, 0x1b);
            this.inCastleSwordsmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.inCastleSwordsmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.inCastleSwordsmenLabel);
            this.inCastleCatapultsLabel.Text = "0";
            this.inCastleCatapultsLabel.Color = ARGBColors.Black;
            this.inCastleCatapultsLabel.Position = new Point(x + (4 * num5), num3 + num4);
            this.inCastleCatapultsLabel.Size = new Size(50, 0x1b);
            this.inCastleCatapultsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.inCastleCatapultsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.inCastleCatapultsLabel);
            this.inCastleCaptainsLabel.Text = "0";
            this.inCastleCaptainsLabel.Color = ARGBColors.Black;
            this.inCastleCaptainsLabel.Position = new Point(x + (5 * num5), num3 + num4);
            this.inCastleCaptainsLabel.Size = new Size(50, 0x1b);
            this.inCastleCaptainsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.inCastleCaptainsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.inCastleCaptainsLabel);
            this.attackingPeasantsLabel.Text = "0";
            this.attackingPeasantsLabel.Color = ARGBColors.Black;
            this.attackingPeasantsLabel.Position = new Point(x, num3 + (2 * num4));
            this.attackingPeasantsLabel.Size = new Size(50, 0x1b);
            this.attackingPeasantsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.attackingPeasantsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.attackingPeasantsLabel);
            this.attackingArchersLabel.Text = "0";
            this.attackingArchersLabel.Color = ARGBColors.Black;
            this.attackingArchersLabel.Position = new Point(x + num5, num3 + (2 * num4));
            this.attackingArchersLabel.Size = new Size(50, 0x1b);
            this.attackingArchersLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.attackingArchersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.attackingArchersLabel);
            this.attackingPikmenLabel.Text = "0";
            this.attackingPikmenLabel.Color = ARGBColors.Black;
            this.attackingPikmenLabel.Position = new Point(x + (2 * num5), num3 + (2 * num4));
            this.attackingPikmenLabel.Size = new Size(50, 0x1b);
            this.attackingPikmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.attackingPikmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.attackingPikmenLabel);
            this.attackingSwordsmenLabel.Text = "0";
            this.attackingSwordsmenLabel.Color = ARGBColors.Black;
            this.attackingSwordsmenLabel.Position = new Point(x + (3 * num5), num3 + (2 * num4));
            this.attackingSwordsmenLabel.Size = new Size(50, 0x1b);
            this.attackingSwordsmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.attackingSwordsmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.attackingSwordsmenLabel);
            this.attackingCatapultsLabel.Text = "0";
            this.attackingCatapultsLabel.Color = ARGBColors.Black;
            this.attackingCatapultsLabel.Position = new Point(x + (4 * num5), num3 + (2 * num4));
            this.attackingCatapultsLabel.Size = new Size(50, 0x1b);
            this.attackingCatapultsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.attackingCatapultsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.attackingCatapultsLabel);
            this.attackingCaptainsLabel.Text = "0";
            this.attackingCaptainsLabel.Color = ARGBColors.Black;
            this.attackingCaptainsLabel.Position = new Point(x + (5 * num5), num3 + (2 * num4));
            this.attackingCaptainsLabel.Size = new Size(50, 0x1b);
            this.attackingCaptainsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.attackingCaptainsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.attackingCaptainsLabel);
            this.reinforcingPeasantsLabel.Text = "0";
            this.reinforcingPeasantsLabel.Color = ARGBColors.Black;
            this.reinforcingPeasantsLabel.Position = new Point(x, num3 + (3 * num4));
            this.reinforcingPeasantsLabel.Size = new Size(50, 0x1b);
            this.reinforcingPeasantsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.reinforcingPeasantsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.reinforcingPeasantsLabel);
            this.reinforcingArchersLabel.Text = "0";
            this.reinforcingArchersLabel.Color = ARGBColors.Black;
            this.reinforcingArchersLabel.Position = new Point(x + num5, num3 + (3 * num4));
            this.reinforcingArchersLabel.Size = new Size(50, 0x1b);
            this.reinforcingArchersLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.reinforcingArchersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.reinforcingArchersLabel);
            this.reinforcingPikmenLabel.Text = "0";
            this.reinforcingPikmenLabel.Color = ARGBColors.Black;
            this.reinforcingPikmenLabel.Position = new Point(x + (2 * num5), num3 + (3 * num4));
            this.reinforcingPikmenLabel.Size = new Size(50, 0x1b);
            this.reinforcingPikmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.reinforcingPikmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.reinforcingPikmenLabel);
            this.reinforcingSwordsmenLabel.Text = "0";
            this.reinforcingSwordsmenLabel.Color = ARGBColors.Black;
            this.reinforcingSwordsmenLabel.Position = new Point(x + (3 * num5), num3 + (3 * num4));
            this.reinforcingSwordsmenLabel.Size = new Size(50, 0x1b);
            this.reinforcingSwordsmenLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.reinforcingSwordsmenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.reinforcingSwordsmenLabel);
            this.reinforcingCatapultsLabel.Text = "0";
            this.reinforcingCatapultsLabel.Color = ARGBColors.Black;
            this.reinforcingCatapultsLabel.Position = new Point(x + (4 * num5), num3 + (3 * num4));
            this.reinforcingCatapultsLabel.Size = new Size(50, 0x1b);
            this.reinforcingCatapultsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.reinforcingCatapultsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.reinforcingCatapultsLabel);
            this.reinforcingCaptainsLabel.Text = "0";
            this.reinforcingCaptainsLabel.Color = ARGBColors.Black;
            this.reinforcingCaptainsLabel.Position = new Point(x + (5 * num5), num3 + (3 * num4));
            this.reinforcingCaptainsLabel.Size = new Size(50, 0x1b);
            this.reinforcingCaptainsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.reinforcingCaptainsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundArea.addControl(this.reinforcingCaptainsLabel);
            this.reinforcementInfoButton.ImageNorm = (Image) GFXLibrary.barracks_i_button_normal;
            this.reinforcementInfoButton.ImageOver = (Image) GFXLibrary.barracks_i_button_over;
            this.reinforcementInfoButton.Position = new Point(0x361, (num3 + (3 * num4)) - 1);
            this.reinforcementInfoButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.reinforcementInfoClick), "CapitalBarracksPanel_view_reinforcements");
            this.mainBackgroundArea.addControl(this.reinforcementInfoButton);
            this.update();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x236);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "CapitalBarracksPanel";
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

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        private void makeTroopClick()
        {
            if (base.OverControl != null)
            {
                try
                {
                    CustomSelfDrawPanel.CSDButton overControl = (CustomSelfDrawPanel.CSDButton) base.OverControl;
                    if (overControl.Active && overControl.Visible)
                    {
                        int data = overControl.Data;
                        VillageMap village = GameEngine.Instance.Village;
                        if (village != null)
                        {
                            int amount = 1;
                            int numTroops = 0;
                            switch ((data % 0x3e8))
                            {
                                case 70:
                                    numTroops = this.makePeasants;
                                    break;

                                case 0x47:
                                    numTroops = this.makeSwordsmen;
                                    break;

                                case 0x48:
                                    numTroops = this.makeArchers;
                                    break;

                                case 0x49:
                                    numTroops = this.makePikemen;
                                    break;

                                case 0x4a:
                                    numTroops = this.makeCatapults;
                                    break;
                            }
                            if (data >= 0x7d0)
                            {
                                amount = numTroops;
                                data -= 0x7d0;
                            }
                            else if (data >= 0x3e8)
                            {
                                amount = this.calcMidSize(numTroops);
                                if (amount > 0x2710)
                                {
                                    return;
                                }
                                data -= 0x3e8;
                            }
                            bool quickSend = false;
                            if (amount == numTroops)
                            {
                                quickSend = true;
                            }
                            village.makeTroops(data, amount, quickSend);
                            this.updateValues();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void makeTroopLeave()
        {
            this.mouseOver = -1;
        }

        private void makeTroopOver()
        {
            if (base.OverControl != null)
            {
                int data = base.OverControl.Data;
                this.mouseOver = data % 0x3e8;
            }
        }

        private void reinforcementInfoClick()
        {
            InterfaceMgr.Instance.setVillageTabSubMode(6);
        }

        private void swipeLeft()
        {
            InterfaceMgr.Instance.getVillageTabBar().changeTabLeft();
        }

        private void swiperight()
        {
            InterfaceMgr.Instance.getVillageTabBar().changeTabRight();
        }

        public void update()
        {
            this.updateValues();
        }

        public void updateValues()
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            WorldData localWorldData = GameEngine.Instance.LocalWorldData;
            VillageMap village = GameEngine.Instance.Village;
            CastleMap castle = GameEngine.Instance.Castle;
            if ((village != null) && (castle != null))
            {
                int num = village.LocallyMade_Peasants;
                int num2 = village.LocallyMade_Archers;
                int num3 = village.LocallyMade_Pikemen;
                int num4 = village.LocallyMade_Swordsmen;
                int num5 = village.LocallyMade_Catapults;
                int num6 = (((num4 + num3) + num) + num5) + num2;
                VillageMap.ArmouryLevels levels = new VillageMap.ArmouryLevels();
                village.getArmouryLevels(levels);
                int capitalGold = (int) village.m_capitalGold;
                capitalGold -= (num * localWorldData.Barracks_GoldCost_Peasant) * localWorldData.MercenaryCostMultiplier;
                capitalGold -= (num2 * localWorldData.Barracks_GoldCost_Archer) * localWorldData.MercenaryCostMultiplier;
                capitalGold -= (num3 * localWorldData.Barracks_GoldCost_Pikeman) * localWorldData.MercenaryCostMultiplier;
                capitalGold -= (num4 * localWorldData.Barracks_GoldCost_Swordsman) * localWorldData.MercenaryCostMultiplier;
                capitalGold -= (num5 * localWorldData.Barracks_GoldCost_Catapult) * localWorldData.MercenaryCostMultiplier;
                int num8 = localWorldData.Barracks_GoldCost_Peasant * localWorldData.MercenaryCostMultiplier;
                int num9 = localWorldData.Barracks_GoldCost_Archer * localWorldData.MercenaryCostMultiplier;
                int num10 = localWorldData.Barracks_GoldCost_Pikeman * localWorldData.MercenaryCostMultiplier;
                int num11 = localWorldData.Barracks_GoldCost_Swordsman * localWorldData.MercenaryCostMultiplier;
                int num12 = localWorldData.Barracks_GoldCost_Catapult * localWorldData.MercenaryCostMultiplier;
                int numTroops = capitalGold / num8;
                int num14 = capitalGold / num9;
                int num15 = capitalGold / num10;
                int num16 = capitalGold / num11;
                int num17 = capitalGold / num12;
                int num18 = 0;
                if (GameEngine.Instance.World.isCapital(village.VillageID))
                {
                    num18 = (village.m_parishCapitalResearchData.Research_Command + 1) * 0x19;
                }
                int num19 = village.calcTotalTroops() + num6;
                int num20 = num19;
                this.goldLabel.Text = capitalGold.ToString();
                this.CurrentTroopsLabel.Text = SK.Text("BARRACKS_Troops", "Troops") + ": " + num19.ToString("N", nFI);
                this.MaxLabelLabel.Text = SK.Text("BARRACKS_Max_Army_Size", "Max Army Size") + ": " + num18.ToString("N", nFI);
                int num21 = num18 - num19;
                if (numTroops > num21)
                {
                    numTroops = num21;
                }
                if (num14 > num21)
                {
                    num14 = num21;
                }
                if (num15 > num21)
                {
                    num15 = num21;
                }
                if (num16 > num21)
                {
                    num16 = num21;
                }
                if (num17 > num21)
                {
                    num17 = num21;
                }
                if (num19 >= num18)
                {
                    numTroops = 0;
                    num14 = 0;
                    num15 = 0;
                    num16 = 0;
                    num17 = 0;
                }
                ResearchData researchDataForCurrentVillage = GameEngine.Instance.World.GetResearchDataForCurrentVillage();
                if (researchDataForCurrentVillage.Research_Conscription == 0)
                {
                    numTroops = 0;
                    this.troop1Image.Visible = false;
                }
                else
                {
                    this.troop1Image.Visible = true;
                }
                if (researchDataForCurrentVillage.Research_LongBow == 0)
                {
                    num14 = 0;
                    this.troop2Image.Visible = false;
                }
                else
                {
                    this.troop2Image.Visible = true;
                }
                if (researchDataForCurrentVillage.Research_Pike == 0)
                {
                    num15 = 0;
                    this.troop3Image.Visible = false;
                }
                else
                {
                    this.troop3Image.Visible = true;
                }
                if (researchDataForCurrentVillage.Research_Sword == 0)
                {
                    num16 = 0;
                    this.troop4Image.Visible = false;
                }
                else
                {
                    this.troop4Image.Visible = true;
                }
                if (researchDataForCurrentVillage.Research_Catapult == 0)
                {
                    num17 = 0;
                    this.troop5Image.Visible = false;
                }
                else
                {
                    this.troop5Image.Visible = true;
                }
                this.troopsMade1Disband.Visible = this.troop1Image.Visible;
                this.troopsMade2Disband.Visible = this.troop2Image.Visible;
                this.troopsMade3Disband.Visible = this.troop3Image.Visible;
                this.troopsMade4Disband.Visible = this.troop4Image.Visible;
                this.troopsMade5Disband.Visible = this.troop5Image.Visible;
                this.makePeasants = numTroops;
                this.makeArchers = num14;
                this.makePikemen = num15;
                this.makeSwordsmen = num16;
                this.makeCatapults = num17;
                bool flag = true;
                if (!GameEngine.Instance.World.isUserVillage(InterfaceMgr.Instance.getSelectedMenuVillage()))
                {
                    flag = false;
                }
                this.troopMake1Button1.invalidate();
                this.troopMake1Button1.Active = false;
                this.troopMake1Button1.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                this.troopMake1Button5.Active = false;
                this.troopMake1Button5.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                this.troopMake1Button20.Active = false;
                this.troopMake1Button20.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                int num22 = this.calcMidSize(numTroops);
                if (numTroops > 0)
                {
                    if (flag)
                    {
                        this.troopMake1Button1.Active = true;
                        this.troopMake1Button1.Text.Color = ARGBColors.Black;
                    }
                    if (numTroops >= num22)
                    {
                        this.troopMake1Button5.Text.Text = num22.ToString();
                        if (flag)
                        {
                            this.troopMake1Button5.Active = true;
                            this.troopMake1Button5.Text.Color = ARGBColors.Black;
                        }
                    }
                    if (numTroops > 1)
                    {
                        this.troopMake1Button20.Text.Text = numTroops.ToString();
                        if (flag)
                        {
                            this.troopMake1Button20.Active = true;
                            this.troopMake1Button20.Text.Color = ARGBColors.Black;
                        }
                    }
                }
                this.troopMake2Button1.invalidate();
                this.troopMake2Button1.Active = false;
                this.troopMake2Button1.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                this.troopMake2Button5.Active = false;
                this.troopMake2Button5.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                this.troopMake2Button20.Active = false;
                this.troopMake2Button20.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                int num23 = this.calcMidSize(num14);
                if (num14 > 0)
                {
                    if (flag)
                    {
                        this.troopMake2Button1.Active = true;
                        this.troopMake2Button1.Text.Color = ARGBColors.Black;
                    }
                    if (num14 >= num23)
                    {
                        this.troopMake2Button5.Text.Text = num23.ToString();
                        if (flag)
                        {
                            this.troopMake2Button5.Active = true;
                            this.troopMake2Button5.Text.Color = ARGBColors.Black;
                        }
                    }
                    if (num14 > 1)
                    {
                        this.troopMake2Button20.Text.Text = num14.ToString();
                        if (flag)
                        {
                            this.troopMake2Button20.Active = true;
                            this.troopMake2Button20.Text.Color = ARGBColors.Black;
                        }
                    }
                }
                this.troopMake3Button1.invalidate();
                this.troopMake3Button1.Active = false;
                this.troopMake3Button1.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                this.troopMake3Button5.Active = false;
                this.troopMake3Button5.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                this.troopMake3Button20.Active = false;
                this.troopMake3Button20.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                int num24 = this.calcMidSize(num15);
                if (num15 > 0)
                {
                    if (flag)
                    {
                        this.troopMake3Button1.Active = true;
                        this.troopMake3Button1.Text.Color = ARGBColors.Black;
                    }
                    if (num15 >= num24)
                    {
                        this.troopMake3Button5.Text.Text = num24.ToString();
                        if (flag)
                        {
                            this.troopMake3Button5.Active = true;
                            this.troopMake3Button5.Text.Color = ARGBColors.Black;
                        }
                    }
                    if (num15 > 1)
                    {
                        this.troopMake3Button20.Text.Text = num15.ToString();
                        if (flag)
                        {
                            this.troopMake3Button20.Active = true;
                            this.troopMake3Button20.Text.Color = ARGBColors.Black;
                        }
                    }
                }
                this.troopMake4Button1.invalidate();
                this.troopMake4Button1.Active = false;
                this.troopMake4Button1.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                this.troopMake4Button5.Active = false;
                this.troopMake4Button5.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                this.troopMake4Button20.Active = false;
                this.troopMake4Button20.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                int num25 = this.calcMidSize(num16);
                if ((num16 > 0) && flag)
                {
                    if (flag)
                    {
                        this.troopMake4Button1.Active = true;
                        this.troopMake4Button1.Text.Color = ARGBColors.Black;
                    }
                    if (num16 >= num25)
                    {
                        this.troopMake4Button5.Text.Text = num25.ToString();
                        if (flag)
                        {
                            this.troopMake4Button5.Active = true;
                            this.troopMake4Button5.Text.Color = ARGBColors.Black;
                        }
                    }
                    if (num16 > 1)
                    {
                        this.troopMake4Button20.Text.Text = num16.ToString();
                        if (flag)
                        {
                            this.troopMake4Button20.Active = true;
                            this.troopMake4Button20.Text.Color = ARGBColors.Black;
                        }
                    }
                }
                this.troopMake5Button1.invalidate();
                this.troopMake5Button1.Active = false;
                this.troopMake5Button1.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                this.troopMake5Button5.Active = false;
                this.troopMake5Button5.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                this.troopMake5Button20.Active = false;
                this.troopMake5Button20.Text.Color = Color.FromArgb(0x97, 0x86, 0x6c);
                int num26 = this.calcMidSize(num17);
                if ((num17 > 0) && flag)
                {
                    if (flag)
                    {
                        this.troopMake5Button1.Active = true;
                        this.troopMake5Button1.Text.Color = ARGBColors.Black;
                    }
                    if (num17 >= num26)
                    {
                        this.troopMake5Button5.Text.Text = num26.ToString();
                        if (flag)
                        {
                            this.troopMake5Button5.Active = true;
                            this.troopMake5Button5.Text.Color = ARGBColors.Black;
                        }
                    }
                    if (num17 > 1)
                    {
                        this.troopMake5Button20.Text.Text = num17.ToString();
                        if (flag)
                        {
                            this.troopMake5Button20.Active = true;
                            this.troopMake5Button20.Text.Color = ARGBColors.Black;
                        }
                    }
                }
                int numPeasants = 0;
                int numArchers = 0;
                int numPikemen = 0;
                int numSwordsmen = 0;
                int numCatapults = 0;
                int numCaptains = 0;
                int numReinfPeasants = 0;
                int numReinfArchers = 0;
                int numReinfPikemen = 0;
                int numReinfSwordsmen = 0;
                int numReinfCatapults = 0;
                int numReinfCaptains = 0;
                GameEngine.Instance.World.getTotalTroopsOutOfVillage(village.VillageID, ref numPeasants, ref numArchers, ref numPikemen, ref numSwordsmen, ref numCatapults, ref numCaptains, ref numReinfPeasants, ref numReinfArchers, ref numReinfPikemen, ref numReinfSwordsmen, ref numReinfCatapults, ref numReinfCaptains);
                int num39 = 0;
                int num40 = 0;
                int num41 = 0;
                int num42 = 0;
                int num43 = 0;
                castle.countOwnPlacedTroopTypes(ref num39, ref num40, ref num41, ref num42, ref num43);
                this.inCastlePeasantsLabel.Text = num39.ToString("N", nFI);
                this.inCastleArchersLabel.Text = num40.ToString("N", nFI);
                this.inCastlePikmenLabel.Text = num41.ToString("N", nFI);
                this.inCastleSwordsmenLabel.Text = num42.ToString("N", nFI);
                this.attackingPeasantsLabel.Text = numPeasants.ToString("N", nFI);
                this.attackingArchersLabel.Text = numArchers.ToString("N", nFI);
                this.attackingPikmenLabel.Text = numPikemen.ToString("N", nFI);
                this.attackingSwordsmenLabel.Text = numSwordsmen.ToString("N", nFI);
                this.attackingCatapultsLabel.Text = numCatapults.ToString("N", nFI);
                this.reinforcingPeasantsLabel.Text = numReinfPeasants.ToString("N", nFI);
                this.reinforcingArchersLabel.Text = numReinfArchers.ToString("N", nFI);
                this.reinforcingPikmenLabel.Text = numReinfPikemen.ToString("N", nFI);
                this.reinforcingSwordsmenLabel.Text = numReinfSwordsmen.ToString("N", nFI);
                this.reinforcingCatapultsLabel.Text = numReinfCatapults.ToString("N", nFI);
                numPeasants += village.m_numPeasants + num;
                numPeasants += num39;
                numPeasants += numReinfPeasants;
                numArchers += village.m_numArchers + num2;
                numArchers += num40;
                numArchers += numReinfArchers;
                numPikemen += village.m_numPikemen + num3;
                numPikemen += num41;
                numPikemen += numReinfPikemen;
                numSwordsmen += village.m_numSwordsmen + num4;
                numSwordsmen += num42;
                numSwordsmen += numReinfSwordsmen;
                numCatapults += village.m_numCatapults + num5;
                numCatapults += numReinfCatapults;
                this.troopsMade1Disband.Text.Text = numPeasants.ToString("N", nFI);
                this.troopsMade2Disband.Text.Text = numArchers.ToString("N", nFI);
                this.troopsMade3Disband.Text.Text = numPikemen.ToString("N", nFI);
                this.troopsMade4Disband.Text.Text = numSwordsmen.ToString("N", nFI);
                this.troopsMade5Disband.Text.Text = numCatapults.ToString("N", nFI);
                this.localPeasantsLabel.Text = (village.m_numPeasants + num).ToString("N", nFI);
                this.localArchersLabel.Text = (village.m_numArchers + num2).ToString("N", nFI);
                this.localPikmenLabel.Text = (village.m_numPikemen + num3).ToString("N", nFI);
                this.localSwordsmenLabel.Text = (village.m_numSwordsmen + num4).ToString("N", nFI);
                this.localCatapultsLabel.Text = (village.m_numCatapults + num5).ToString("N", nFI);
                this.troopGoldCost1Label.Text = num8.ToString();
                this.troopGoldCost2Label.Text = num9.ToString();
                this.troopGoldCost3Label.Text = num10.ToString();
                this.troopGoldCost4Label.Text = num11.ToString();
                this.troopGoldCost5Label.Text = num12.ToString();
                if (capitalGold < num8)
                {
                    this.troopGold1Image.Colorise = Color.FromArgb(0xff, 0x80, 0x80);
                }
                else
                {
                    this.troopGold1Image.Colorise = ARGBColors.White;
                }
                if (capitalGold < num9)
                {
                    this.troopGold2Image.Colorise = Color.FromArgb(0xff, 0x80, 0x80);
                }
                else
                {
                    this.troopGold2Image.Colorise = ARGBColors.White;
                }
                if (capitalGold < num10)
                {
                    this.troopGold3Image.Colorise = Color.FromArgb(0xff, 0x80, 0x80);
                }
                else
                {
                    this.troopGold3Image.Colorise = ARGBColors.White;
                }
                if (capitalGold < num11)
                {
                    this.troopGold4Image.Colorise = Color.FromArgb(0xff, 0x80, 0x80);
                }
                else
                {
                    this.troopGold4Image.Colorise = ARGBColors.White;
                }
                if (capitalGold < num12)
                {
                    this.troopGold5Image.Colorise = Color.FromArgb(0xff, 0x80, 0x80);
                }
                else
                {
                    this.troopGold5Image.Colorise = ARGBColors.White;
                }
                this.fullBar.MaxValue = GameEngine.Instance.LocalWorldData.Village_UnitCapacity;
                if (num20 < GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
                {
                    this.fullBar.Number = num20;
                }
                else
                {
                    this.fullBar.Number = GameEngine.Instance.LocalWorldData.Village_UnitCapacity;
                }
            }
        }
    }
}

