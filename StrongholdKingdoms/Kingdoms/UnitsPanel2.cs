namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class UnitsPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDLabel barracksLabel = new CustomSelfDrawPanel.CSDLabel();
        private CardBarGDI cardbar = new CardBarGDI();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private int disbandOver = -1;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDColorBar fullBar = new CustomSelfDrawPanel.CSDColorBar();
        public static UnitsPanel2 instance;
        private CustomSelfDrawPanel.CSDLabel item1AmountLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage item1Image = new CustomSelfDrawPanel.CSDImage();
        private DisbandUnitsPopup m_disbandPopup;
        private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private int mouseOver = -1;
        private CustomSelfDrawPanel.CSDLabel noResearchText = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDExtendingPanel noResearchWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private int numMonk;
        private int numScouts;
        private int numSpies;
        private int numTrader;
        private CustomSelfDrawPanel.CSDLabel SpaceLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage troop1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troop1WeaponImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troop2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troop2WeaponImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troop3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troop3WeaponImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troop4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troop4WeaponImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troop5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage troop5WeaponImage = new CustomSelfDrawPanel.CSDImage();
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
        private CustomSelfDrawPanel.CSDButton troopMake1Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake1ButtonA = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake1ButtonB = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake1ButtonX = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake2Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake2ButtonA = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake2ButtonB = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake2ButtonX = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake3Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake4Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake4ButtonA = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake4ButtonB = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake4ButtonX = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopMake5Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea troopsMade1Disband = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel troopsMade1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea troopsMade2Disband = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel troopsMade2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea troopsMade3Disband = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel troopsMade3Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea troopsMade4Disband = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel troopsMade4Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea troopsMade5Disband = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel troopsMade5Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel troopUnitSize1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel troopUnitSize2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel troopUnitSize3Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel troopUnitSize4Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel troopUnitSize5Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel typeLabel = new CustomSelfDrawPanel.CSDLabel();

        public UnitsPanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void attackInfoClick()
        {
            InterfaceMgr.Instance.setVillageTabSubMode(7);
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

        private void disbandTroopLeave()
        {
            switch (this.disbandOver)
            {
                case 1:
                    this.troopsMade1Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
                    break;

                case 2:
                    this.troopsMade2Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
                    break;

                case 3:
                    this.troopsMade3Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
                    break;

                case 4:
                    this.troopsMade4Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
                    break;
            }
            this.disbandOver = -1;
        }

        private void disbandTroopOver()
        {
            if (base.OverControl != null)
            {
                int data = base.OverControl.Data;
                this.disbandOver = data;
                switch (this.disbandOver)
                {
                    case 1:
                        this.troopsMade1Label.Color = ARGBColors.Red;
                        return;

                    case 2:
                        this.troopsMade2Label.Color = ARGBColors.Red;
                        return;

                    case 3:
                        this.troopsMade3Label.Color = ARGBColors.Red;
                        return;

                    case 4:
                        this.troopsMade4Label.Color = ARGBColors.Red;
                        break;

                    default:
                        return;
                }
            }
        }

        private void disbandTroopsClick()
        {
            if (base.OverControl != null)
            {
                int data = base.OverControl.Data;
                if (GameEngine.Instance.Village != null)
                {
                    this.closeDisbandPopup();
                    this.m_disbandPopup = new DisbandUnitsPopup();
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
            this.mainBackgroundImage.Image = (Image) GFXLibrary.people_background;
            this.mainBackgroundImage.Position = new Point(0, 0);
            base.addControl(this.mainBackgroundImage);
            this.mainBackgroundArea.Position = new Point(0, 0);
            this.mainBackgroundArea.Size = new Size(0x3e0, 0x236);
            this.mainBackgroundImage.addControl(this.mainBackgroundArea);
            InterfaceMgr.Instance.setVillageHeading(SK.Text("UnitsPanel_Units", "Units"));
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x3b4, 10);
            this.closeButton.CustomTooltipID = 0x2bd;
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "UnitsPanel2_close");
            this.mainBackgroundImage.addControl(this.closeButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundArea, 6, new Point(0x382, 10));
            this.troop1Image.Image = (Image) GFXLibrary.people_01;
            this.troop1Image.Position = new Point(140, 0x30);
            this.mainBackgroundArea.addControl(this.troop1Image);
            this.troopsMade1Label.Text = "0";
            this.troopsMade1Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopsMade1Label.Position = new Point(0x43, 5);
            this.troopsMade1Label.Size = new Size(0x57, 0x1c);
            this.troopsMade1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopsMade1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.troop1Image.addControl(this.troopsMade1Label);
            this.troopsMade1Disband.Position = new Point(0, 0);
            this.troopsMade1Disband.Size = this.troopsMade1Label.Size;
            this.troopsMade1Disband.Data = 1;
            this.troopsMade1Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "UnitsPanel2_disband_monks");
            this.troopsMade1Disband.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopLeave));
            this.troopsMade1Disband.CustomTooltipID = 600;
            this.troopsMade1Label.addControl(this.troopsMade1Disband);
            this.troopGoldCost1Label.Text = "0";
            this.troopGoldCost1Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopGoldCost1Label.Position = new Point(0x3b, 0x89);
            this.troopGoldCost1Label.Size = new Size(0x2c, 0x2f);
            this.troopGoldCost1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopGoldCost1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop1Image.addControl(this.troopGoldCost1Label);
            this.troopMake1Button.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.troopMake1Button.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.troopMake1Button.Position = new Point(10, 0xbd);
            this.troopMake1Button.Text.Text = "0";
            this.troopMake1Button.TextYOffset = 1;
            this.troopMake1Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopMake1Button.Text.Color = ARGBColors.Black;
            this.troopMake1Button.Data = 1;
            this.troopMake1Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
            this.troopMake1ButtonA.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
            this.troopMake1ButtonA.ImageOver = (Image) GFXLibrary.button3comp_left_over;
            this.troopMake1ButtonA.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
            this.troopMake1ButtonA.Position = new Point(10, 0xc3);
            this.troopMake1ButtonA.Text.Text = "0";
            this.troopMake1ButtonA.TextYOffset = 1;
            this.troopMake1ButtonA.Text.Size = new Size(this.troopMake1ButtonA.Width - 5, this.troopMake1ButtonA.Height);
            this.troopMake1ButtonA.Text.Position = new Point(5, 0);
            this.troopMake1ButtonA.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake1ButtonA.Text.Color = ARGBColors.Black;
            this.troopMake1ButtonA.Data = 1;
            this.troopMake1ButtonA.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake1ButtonA.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
            this.troop1Image.addControl(this.troopMake1ButtonA);
            this.troopMake1ButtonX.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
            this.troopMake1ButtonX.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
            this.troopMake1ButtonX.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
            this.troopMake1ButtonX.Position = new Point(60, 0xc3);
            this.troopMake1ButtonX.Text.Text = "0";
            this.troopMake1ButtonX.TextYOffset = 1;
            this.troopMake1ButtonX.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake1ButtonX.Text.Color = ARGBColors.Black;
            this.troopMake1ButtonX.Data = 11;
            this.troopMake1ButtonX.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake1ButtonX.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
            this.troop1Image.addControl(this.troopMake1ButtonX);
            this.troopMake1ButtonB.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
            this.troopMake1ButtonB.ImageOver = (Image) GFXLibrary.button3comp_right_over;
            this.troopMake1ButtonB.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
            this.troopMake1ButtonB.Position = new Point(0x6c, 0xc3);
            this.troopMake1ButtonB.Text.Text = "0";
            this.troopMake1ButtonB.TextYOffset = 1;
            this.troopMake1ButtonB.Text.Size = new Size(this.troopMake1ButtonB.Width - 5, this.troopMake1ButtonB.Height);
            this.troopMake1ButtonB.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake1ButtonB.Text.Color = ARGBColors.Black;
            this.troopMake1ButtonB.Data = 0x15;
            this.troopMake1ButtonB.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake1ButtonB.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
            this.troop1Image.addControl(this.troopMake1ButtonB);
            this.troopGold1Image.Image = (Image) GFXLibrary.com_32_money;
            this.troopGold1Image.Position = new Point(0x6b, 0x91);
            this.troop1Image.addControl(this.troopGold1Image);
            this.troop1WeaponImage.Image = (Image) GFXLibrary.people_unitspace_icon_01;
            this.troop1WeaponImage.Position = new Point(0x6b, 0x71);
            this.troop1WeaponImage.CustomTooltipID = 0x2be;
            this.troop1Image.addControl(this.troop1WeaponImage);
            this.troopUnitSize1Label.Text = "0";
            this.troopUnitSize1Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopUnitSize1Label.Position = new Point(0x3b, 0x6b);
            this.troopUnitSize1Label.Size = new Size(0x2c, 0x2f);
            this.troopUnitSize1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopUnitSize1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop1Image.addControl(this.troopUnitSize1Label);
            this.troop2Image.Image = (Image) GFXLibrary.people_02;
            this.troop2Image.Position = new Point(410, 0x30);
            this.mainBackgroundArea.addControl(this.troop2Image);
            this.troopsMade2Label.Text = "0";
            this.troopsMade2Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopsMade2Label.Position = new Point(0x43, 5);
            this.troopsMade2Label.Size = new Size(0x57, 0x1c);
            this.troopsMade2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopsMade2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.troop2Image.addControl(this.troopsMade2Label);
            this.troopsMade2Disband.Position = new Point(0, 0);
            this.troopsMade2Disband.Size = this.troopsMade2Label.Size;
            this.troopsMade2Disband.Data = 2;
            this.troopsMade2Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "UnitsPanel2_disband_traders");
            this.troopsMade2Disband.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopLeave));
            this.troopsMade2Disband.CustomTooltipID = 600;
            this.troopsMade2Label.addControl(this.troopsMade2Disband);
            this.troopGoldCost2Label.Text = "0";
            this.troopGoldCost2Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopGoldCost2Label.Position = new Point(0x3b, 0x89);
            this.troopGoldCost2Label.Size = new Size(0x2c, 0x2f);
            this.troopGoldCost2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopGoldCost2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop2Image.addControl(this.troopGoldCost2Label);
            this.troopMake2Button.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.troopMake2Button.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.troopMake2Button.Position = new Point(10, 0xbd);
            this.troopMake2Button.Text.Text = "0";
            this.troopMake2Button.TextYOffset = 1;
            this.troopMake2Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopMake2Button.Text.Color = ARGBColors.Black;
            this.troopMake2Button.Data = 2;
            this.troopMake2Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
            this.troopMake2ButtonA.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
            this.troopMake2ButtonA.ImageOver = (Image) GFXLibrary.button3comp_left_over;
            this.troopMake2ButtonA.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
            this.troopMake2ButtonA.Position = new Point(10, 0xc3);
            this.troopMake2ButtonA.Text.Text = "0";
            this.troopMake2ButtonA.TextYOffset = 1;
            this.troopMake2ButtonA.Text.Size = new Size(this.troopMake2ButtonA.Width - 5, this.troopMake2ButtonA.Height);
            this.troopMake2ButtonA.Text.Position = new Point(5, 0);
            this.troopMake2ButtonA.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake2ButtonA.Text.Color = ARGBColors.Black;
            this.troopMake2ButtonA.Data = 2;
            this.troopMake2ButtonA.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake2ButtonA.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
            this.troop2Image.addControl(this.troopMake2ButtonA);
            this.troopMake2ButtonX.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
            this.troopMake2ButtonX.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
            this.troopMake2ButtonX.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
            this.troopMake2ButtonX.Position = new Point(60, 0xc3);
            this.troopMake2ButtonX.Text.Text = "0";
            this.troopMake2ButtonX.TextYOffset = 1;
            this.troopMake2ButtonX.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake2ButtonX.Text.Color = ARGBColors.Black;
            this.troopMake2ButtonX.Data = 12;
            this.troopMake2ButtonX.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake2ButtonX.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
            this.troop2Image.addControl(this.troopMake2ButtonX);
            this.troopMake2ButtonB.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
            this.troopMake2ButtonB.ImageOver = (Image) GFXLibrary.button3comp_right_over;
            this.troopMake2ButtonB.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
            this.troopMake2ButtonB.Position = new Point(0x6c, 0xc3);
            this.troopMake2ButtonB.Text.Text = "0";
            this.troopMake2ButtonB.TextYOffset = 1;
            this.troopMake2ButtonB.Text.Size = new Size(this.troopMake2ButtonB.Width - 5, this.troopMake2ButtonB.Height);
            this.troopMake2ButtonB.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake2ButtonB.Text.Color = ARGBColors.Black;
            this.troopMake2ButtonB.Data = 0x16;
            this.troopMake2ButtonB.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake2ButtonB.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
            this.troop2Image.addControl(this.troopMake2ButtonB);
            this.troopGold2Image.Image = (Image) GFXLibrary.com_32_money;
            this.troopGold2Image.Position = new Point(0x6b, 0x91);
            this.troop2Image.addControl(this.troopGold2Image);
            this.troop2WeaponImage.Image = (Image) GFXLibrary.people_unitspace_icon_02;
            this.troop2WeaponImage.Position = new Point(0x6b, 0x71);
            this.troop2WeaponImage.CustomTooltipID = 0x2be;
            this.troop2Image.addControl(this.troop2WeaponImage);
            this.troopUnitSize2Label.Text = "0";
            this.troopUnitSize2Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopUnitSize2Label.Position = new Point(0x3b, 0x6b);
            this.troopUnitSize2Label.Size = new Size(0x2c, 0x2f);
            this.troopUnitSize2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopUnitSize2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop2Image.addControl(this.troopUnitSize2Label);
            this.troop3Image.Image = (Image) GFXLibrary.people_03;
            this.troop3Image.Position = new Point(410, 0x30);
            this.mainBackgroundArea.addControl(this.troop3Image);
            this.troopsMade3Label.Text = "0";
            this.troopsMade3Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopsMade3Label.Position = new Point(0x43, 5);
            this.troopsMade3Label.Size = new Size(0x57, 0x1c);
            this.troopsMade3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopsMade3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.troop3Image.addControl(this.troopsMade3Label);
            this.troopsMade3Disband.Position = new Point(0, 0);
            this.troopsMade3Disband.Size = this.troopsMade3Label.Size;
            this.troopsMade3Disband.Data = 3;
            this.troopsMade3Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick));
            this.troopsMade3Disband.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopLeave));
            this.troopsMade3Disband.CustomTooltipID = 600;
            this.troopsMade3Label.addControl(this.troopsMade3Disband);
            this.troopGoldCost3Label.Text = "0";
            this.troopGoldCost3Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopGoldCost3Label.Position = new Point(0x3b, 0x89);
            this.troopGoldCost3Label.Size = new Size(0x2c, 0x2f);
            this.troopGoldCost3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopGoldCost3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop3Image.addControl(this.troopGoldCost3Label);
            this.troopMake3Button.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.troopMake3Button.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.troopMake3Button.Position = new Point(10, 0xbd);
            this.troopMake3Button.Text.Text = "0";
            this.troopMake3Button.TextYOffset = 1;
            this.troopMake3Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopMake3Button.Text.Color = ARGBColors.Black;
            this.troopMake3Button.Data = 3;
            this.troopMake3Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
            this.troop3Image.addControl(this.troopMake3Button);
            this.troopGold3Image.Image = (Image) GFXLibrary.com_32_money;
            this.troopGold3Image.Position = new Point(0x6b, 0x91);
            this.troop3Image.addControl(this.troopGold3Image);
            this.troop3WeaponImage.Image = (Image) GFXLibrary.people_unitspace_icon_03;
            this.troop3WeaponImage.Position = new Point(0x6b, 0x71);
            this.troop3WeaponImage.CustomTooltipID = 0x2be;
            this.troop3Image.addControl(this.troop3WeaponImage);
            this.troopUnitSize3Label.Text = "0";
            this.troopUnitSize3Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopUnitSize3Label.Position = new Point(0x3b, 0x6b);
            this.troopUnitSize3Label.Size = new Size(0x2c, 0x2f);
            this.troopUnitSize3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopUnitSize3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop3Image.addControl(this.troopUnitSize3Label);
            this.troop4Image.Image = (Image) GFXLibrary.people_04;
            this.troop4Image.Position = new Point(680, 0x30);
            this.mainBackgroundArea.addControl(this.troop4Image);
            this.troopsMade4Label.Text = "0";
            this.troopsMade4Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopsMade4Label.Position = new Point(0x43, 5);
            this.troopsMade4Label.Size = new Size(0x57, 0x1c);
            this.troopsMade4Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopsMade4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.troop4Image.addControl(this.troopsMade4Label);
            this.troopsMade4Disband.Position = new Point(0, 0);
            this.troopsMade4Disband.Size = this.troopsMade4Label.Size;
            this.troopsMade4Disband.Data = 4;
            this.troopsMade4Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick), "UnitsPanel2_disband_scouts");
            this.troopsMade4Disband.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopLeave));
            this.troopsMade4Disband.CustomTooltipID = 600;
            this.troopsMade4Label.addControl(this.troopsMade4Disband);
            this.troopGoldCost4Label.Text = "0";
            this.troopGoldCost4Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopGoldCost4Label.Position = new Point(0x3b, 0x89);
            this.troopGoldCost4Label.Size = new Size(0x2c, 0x2f);
            this.troopGoldCost4Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopGoldCost4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop4Image.addControl(this.troopGoldCost4Label);
            this.troopMake4Button.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.troopMake4Button.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.troopMake4Button.Position = new Point(10, 0xc1);
            this.troopMake4Button.Text.Text = "0";
            this.troopMake4Button.TextYOffset = 1;
            this.troopMake4Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopMake4Button.Text.Color = ARGBColors.Black;
            this.troopMake4Button.Data = 4;
            this.troopMake4Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
            this.troopMake4ButtonA.ImageNorm = (Image) GFXLibrary.button3comp_left_normal;
            this.troopMake4ButtonA.ImageOver = (Image) GFXLibrary.button3comp_left_over;
            this.troopMake4ButtonA.ImageClick = (Image) GFXLibrary.button3comp_left_pressed;
            this.troopMake4ButtonA.Position = new Point(10, 0xc3);
            this.troopMake4ButtonA.Text.Text = "0";
            this.troopMake4ButtonA.TextYOffset = 1;
            this.troopMake4ButtonA.Text.Size = new Size(this.troopMake4ButtonA.Width - 5, this.troopMake4ButtonA.Height);
            this.troopMake4ButtonA.Text.Position = new Point(5, 0);
            this.troopMake4ButtonA.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake4ButtonA.Text.Color = ARGBColors.Black;
            this.troopMake4ButtonA.Data = 4;
            this.troopMake4ButtonA.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake4ButtonA.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
            this.troop4Image.addControl(this.troopMake4ButtonA);
            this.troopMake4ButtonX.ImageNorm = (Image) GFXLibrary.button3comp_mid_normal;
            this.troopMake4ButtonX.ImageOver = (Image) GFXLibrary.button3comp_mid_over;
            this.troopMake4ButtonX.ImageClick = (Image) GFXLibrary.button3comp_mid_pushed;
            this.troopMake4ButtonX.Position = new Point(60, 0xc3);
            this.troopMake4ButtonX.Text.Text = "0";
            this.troopMake4ButtonX.TextYOffset = 1;
            this.troopMake4ButtonX.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake4ButtonX.Text.Color = ARGBColors.Black;
            this.troopMake4ButtonX.Data = 14;
            this.troopMake4ButtonX.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake4ButtonX.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
            this.troop4Image.addControl(this.troopMake4ButtonX);
            this.troopMake4ButtonB.ImageNorm = (Image) GFXLibrary.button3comp_right_normal;
            this.troopMake4ButtonB.ImageOver = (Image) GFXLibrary.button3comp_right_over;
            this.troopMake4ButtonB.ImageClick = (Image) GFXLibrary.button3comp_right_pushed;
            this.troopMake4ButtonB.Position = new Point(0x6c, 0xc3);
            this.troopMake4ButtonB.Text.Text = "0";
            this.troopMake4ButtonB.TextYOffset = 1;
            this.troopMake4ButtonB.Text.Size = new Size(this.troopMake4ButtonB.Width - 5, this.troopMake4ButtonB.Height);
            this.troopMake4ButtonB.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.troopMake4ButtonB.Text.Color = ARGBColors.Black;
            this.troopMake4ButtonB.Data = 0x18;
            this.troopMake4ButtonB.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake4ButtonB.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
            this.troop4Image.addControl(this.troopMake4ButtonB);
            this.troopGold4Image.Image = (Image) GFXLibrary.com_32_money;
            this.troopGold4Image.Position = new Point(0x6b, 0x91);
            this.troop4Image.addControl(this.troopGold4Image);
            this.troop4WeaponImage.Image = (Image) GFXLibrary.people_unitspace_icon_04;
            this.troop4WeaponImage.Position = new Point(0x6b, 0x71);
            this.troop4WeaponImage.CustomTooltipID = 0x2be;
            this.troop4Image.addControl(this.troop4WeaponImage);
            this.troopUnitSize4Label.Text = "0";
            this.troopUnitSize4Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopUnitSize4Label.Position = new Point(0x3b, 0x6b);
            this.troopUnitSize4Label.Size = new Size(0x2c, 0x2f);
            this.troopUnitSize4Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopUnitSize4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop4Image.addControl(this.troopUnitSize4Label);
            this.troop5Image.Image = (Image) GFXLibrary.people_05;
            this.troop5Image.Position = new Point(770, 0x30);
            this.mainBackgroundArea.addControl(this.troop5Image);
            this.troopsMade5Label.Text = "0";
            this.troopsMade5Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopsMade5Label.Position = new Point(0x43, 5);
            this.troopsMade5Label.Size = new Size(0x57, 0x1c);
            this.troopsMade5Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopsMade5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.troop5Image.addControl(this.troopsMade5Label);
            this.troopsMade5Disband.Position = new Point(0, 0);
            this.troopsMade5Disband.Size = this.troopsMade5Label.Size;
            this.troopsMade5Disband.Data = 5;
            this.troopsMade5Disband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandTroopsClick));
            this.troopsMade5Disband.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.disbandTroopLeave));
            this.troopsMade5Disband.CustomTooltipID = 600;
            this.troopsMade5Label.addControl(this.troopsMade5Disband);
            this.troopGoldCost5Label.Text = "0";
            this.troopGoldCost5Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopGoldCost5Label.Position = new Point(0x3b, 0x89);
            this.troopGoldCost5Label.Size = new Size(0x2c, 0x2f);
            this.troopGoldCost5Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopGoldCost5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop5Image.addControl(this.troopGoldCost5Label);
            this.troopMake5Button.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.troopMake5Button.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.troopMake5Button.Position = new Point(10, 0xbd);
            this.troopMake5Button.Text.Text = "0";
            this.troopMake5Button.TextYOffset = 1;
            this.troopMake5Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopMake5Button.Text.Color = ARGBColors.Black;
            this.troopMake5Button.Data = 5;
            this.troopMake5Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.makeTroopLeave));
            this.troopMake5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.makeTroopClick));
            this.troop5Image.addControl(this.troopMake5Button);
            this.troopGold5Image.Image = (Image) GFXLibrary.com_32_money;
            this.troopGold5Image.Position = new Point(0x6b, 0x91);
            this.troop5Image.addControl(this.troopGold5Image);
            this.troop5WeaponImage.Image = (Image) GFXLibrary.people_unitspace_icon_05;
            this.troop5WeaponImage.Position = new Point(0x6b, 0x71);
            this.troop5WeaponImage.CustomTooltipID = 0x2be;
            this.troop5Image.addControl(this.troop5WeaponImage);
            this.troopUnitSize5Label.Text = "0";
            this.troopUnitSize5Label.Color = Color.FromArgb(0xd0, 0xa5, 0x66);
            this.troopUnitSize5Label.Position = new Point(0x3b, 0x6b);
            this.troopUnitSize5Label.Size = new Size(0x2c, 0x2f);
            this.troopUnitSize5Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopUnitSize5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.troop5Image.addControl(this.troopUnitSize5Label);
            int num = 0x49;
            this.item1Image.Image = (Image) GFXLibrary.com_32_people;
            this.item1Image.Position = new Point(num + 7, 310);
            this.mainBackgroundArea.addControl(this.item1Image);
            this.item1AmountLabel.Text = "0";
            this.item1AmountLabel.Color = ARGBColors.Black;
            this.item1AmountLabel.Position = new Point(num - 3, 0x160);
            this.item1AmountLabel.Size = new Size(50, 20);
            this.item1AmountLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.item1AmountLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.mainBackgroundArea.addControl(this.item1AmountLabel);
            this.typeLabel.Text = "";
            this.typeLabel.Color = ARGBColors.Black;
            this.typeLabel.Position = new Point(num + 80, 0x15c);
            this.typeLabel.Size = new Size(250, 20);
            this.typeLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.typeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.mainBackgroundArea.addControl(this.typeLabel);
            this.SpaceLabel.Text = SK.Text("BARRACKS_Spare_Unit_Space", "Spare Unit Space") + ": 0";
            this.SpaceLabel.Color = ARGBColors.Black;
            this.SpaceLabel.Position = new Point(560, 0x14e);
            if (((Program.mySettings.LanguageIdent == "pl") || (Program.mySettings.LanguageIdent == "it")) || (Program.mySettings.LanguageIdent == "pt"))
            {
                this.SpaceLabel.Size = new Size(420, 20);
            }
            else
            {
                this.SpaceLabel.Size = new Size(220, 20);
            }
            this.SpaceLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.SpaceLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mainBackgroundArea.addControl(this.SpaceLabel);
            this.fullBar.setImages((Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right, (Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right);
            this.fullBar.Number = 0.0;
            this.fullBar.MaxValue = 9.0;
            this.fullBar.SetMargin(2, 2, 2, 3);
            if (((Program.mySettings.LanguageIdent == "pl") || (Program.mySettings.LanguageIdent == "it")) || (Program.mySettings.LanguageIdent == "pt"))
            {
                this.fullBar.Position = new Point(770, 0x167);
            }
            else
            {
                this.fullBar.Position = new Point(770, 0x153);
            }
            this.mainBackgroundArea.addControl(this.fullBar);
            this.troop3Image.Visible = false;
            this.troop5Image.Visible = false;
            this.troop1Image.Alpha = 0.3f;
            this.troop2Image.Alpha = 0.3f;
            this.troop4Image.Alpha = 0.3f;
            this.cardbar.Position = new Point(0, 0);
            this.mainBackgroundImage.addControl(this.cardbar);
            this.cardbar.init(5);
            byte num1 = GameEngine.Instance.World.UserResearchData.Research_Ordination;
            byte num2 = GameEngine.Instance.World.UserResearchData.Research_Merchant_Guilds;
            byte num3 = GameEngine.Instance.World.UserResearchData.Research_Scouts;
            this.update();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x236);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "UnitsPanel2";
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
                    int data = overControl.Data;
                    VillageMap village = GameEngine.Instance.Village;
                    if ((village == null) || !overControl.Active)
                    {
                        goto Label_02A0;
                    }
                    switch (data)
                    {
                        case 1:
                            GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_monks");
                            village.makePeople(4);
                            return;

                        case 2:
                            GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_traders");
                            village.makeTroops(-5);
                            return;

                        case 3:
                        case 13:
                        case 0x17:
                            return;

                        case 4:
                            GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_scouts");
                            village.makeTroops(0x4c, 1, false);
                            return;

                        case 11:
                            if (this.numMonk <= 0)
                            {
                                goto Label_00EE;
                            }
                            GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_monks");
                            if (this.numMonk < 4)
                            {
                                break;
                            }
                            village.makePeople(0x3ec);
                            return;

                        case 12:
                            if (this.numTrader <= 0)
                            {
                                goto Label_01A5;
                            }
                            GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_traders");
                            if (this.numTrader < 4)
                            {
                                goto Label_0191;
                            }
                            village.makeTroops(-5, 4, false);
                            return;

                        case 14:
                            if (this.numScouts <= 0)
                            {
                                goto Label_0255;
                            }
                            GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_scouts");
                            if (this.numScouts < 4)
                            {
                                goto Label_0244;
                            }
                            village.makeTroops(0x4c, 4, false);
                            return;

                        case 0x15:
                            if (this.numMonk <= 0)
                            {
                                goto Label_0131;
                            }
                            GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_monks");
                            village.makePeople(0x3e8 + this.numMonk);
                            return;

                        case 0x16:
                            if (this.numTrader <= 0)
                            {
                                goto Label_01E5;
                            }
                            GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_traders");
                            village.makeTroops(-5, this.numTrader, false);
                            return;

                        case 0x18:
                            if (this.numScouts <= 0)
                            {
                                goto Label_028F;
                            }
                            GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_scouts");
                            village.makeTroops(0x4c, this.numScouts, false);
                            return;

                        default:
                            return;
                    }
                    village.makePeople(0x3e8 + this.numMonk);
                    return;
                Label_00EE:
                    GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_failed");
                    return;
                Label_0131:
                    GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_failed");
                    return;
                Label_0191:
                    village.makeTroops(-5, this.numTrader, false);
                    return;
                Label_01A5:
                    GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_failed");
                    return;
                Label_01E5:
                    GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_failed");
                    return;
                Label_0244:
                    village.makeTroops(0x4c, this.numScouts, false);
                    return;
                Label_0255:
                    GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_failed");
                    return;
                Label_028F:
                    GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_failed");
                    return;
                Label_02A0:
                    GameEngine.Instance.playInterfaceSound("UnitsPanel2_make_failed");
                }
                catch (Exception)
                {
                }
            }
        }

        private void makeTroopLeave()
        {
            this.mouseOver = -1;
            this.typeLabel.Text = "";
        }

        private void makeTroopOver()
        {
            if (base.OverControl != null)
            {
                int data = base.OverControl.Data;
                this.mouseOver = data;
                switch (data)
                {
                    case 1:
                    case 11:
                    case 0x15:
                        this.typeLabel.Text = SK.Text("UnitsPanel_Monks", "Monks");
                        return;

                    case 2:
                    case 12:
                    case 0x16:
                        this.typeLabel.Text = SK.Text("UnitsPanel_Traders", "Traders");
                        return;

                    case 3:
                        this.typeLabel.Text = SK.Text("UnitsPanel_Spies", "Spies");
                        return;

                    case 4:
                    case 14:
                    case 0x18:
                        this.typeLabel.Text = SK.Text("UnitsPanel_Scouts", "Scouts");
                        return;

                    case 13:
                    case 0x17:
                        return;
                }
            }
        }

        private void reinforcementInfoClick()
        {
            InterfaceMgr.Instance.setVillageTabSubMode(6);
        }

        public void update()
        {
            this.updateValues();
            this.cardbar.update();
        }

        public void updateValues()
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            WorldData localWorldData = GameEngine.Instance.LocalWorldData;
            VillageMap village = GameEngine.Instance.Village;
            CastleMap castle = GameEngine.Instance.Castle;
            if ((village != null) && (castle != null))
            {
                int num = village.LocallyMade_Scouts;
                int num2 = village.m_spareWorkers - num;
                this.item1AmountLabel.Text = num2.ToString("N", nFI);
                if ((num2 == 0) && (this.mouseOver >= 0))
                {
                    this.item1Image.Colorise = Color.FromArgb(0xff, 0x80, 0x80);
                }
                else
                {
                    this.item1Image.Colorise = ARGBColors.White;
                }
                int num3 = (int) GameEngine.Instance.World.getCurrentGold();
                num3 -= num * localWorldData.ScoutGoldCost;
                village.calcTotalTroops();
                int num4 = village.calcUnitUsages() + (num * GameEngine.Instance.LocalWorldData.UnitSize_Scout);
                int num5 = village.calcTotalTraders();
                int numMonks = village.calcTotalMonks();
                int num7 = village.calcTotalScouts() + num;
                this.numMonk = Math.Min(num2, num3 / localWorldData.getMonkCost(numMonks));
                this.numTrader = Math.Min(num2, num3 / localWorldData.TraderGoldCost);
                this.numScouts = Math.Min(num2, num3 / localWorldData.ScoutGoldCost);
                this.numSpies = 0;
                int num8 = ResearchData.scoutResearchScoutsLevels[GameEngine.Instance.World.userResearchData.Research_Scouts];
                int num9 = village.countWorkingMarkets() * ResearchData.numMerchantGuildsTraders[GameEngine.Instance.World.userResearchData.Research_Merchant_Guilds];
                int num10 = ResearchData.ordinationResearchMonkLevels[GameEngine.Instance.World.userResearchData.Research_Ordination];
                if (this.numScouts > num8)
                {
                    this.numScouts = num8;
                }
                if (this.numTrader > num9)
                {
                    this.numTrader = num9;
                }
                if (this.numMonk > num10)
                {
                    this.numMonk = num10;
                }
                if ((num9 - num5) < this.numTrader)
                {
                    this.numTrader = num9 - num5;
                }
                if ((num8 - num7) < this.numScouts)
                {
                    this.numScouts = num8 - num7;
                }
                if ((num10 - numMonks) < this.numMonk)
                {
                    this.numMonk = num10 - numMonks;
                }
                if (this.numTrader < 0)
                {
                    this.numTrader = 0;
                }
                if (this.numScouts < 0)
                {
                    this.numScouts = 0;
                }
                if (this.numMonk < 0)
                {
                    this.numMonk = 0;
                }
                this.SpaceLabel.Text = SK.Text("BARRACKS_Spare_Unit_Space", "Spare Unit Space") + ": " + Math.Max(GameEngine.Instance.LocalWorldData.Village_UnitCapacity - num4, 0).ToString("N", nFI);
                this.troopUnitSize1Label.Text = GameEngine.Instance.LocalWorldData.UnitSize_Priests.ToString();
                this.troopUnitSize2Label.Text = GameEngine.Instance.LocalWorldData.UnitSize_Trader.ToString();
                this.troopUnitSize3Label.Text = GameEngine.Instance.LocalWorldData.UnitSize_Spies.ToString();
                this.troopUnitSize4Label.Text = GameEngine.Instance.LocalWorldData.UnitSize_Scout.ToString();
                this.troop1WeaponImage.CustomTooltipData = GameEngine.Instance.LocalWorldData.UnitSize_Priests;
                this.troop2WeaponImage.CustomTooltipData = GameEngine.Instance.LocalWorldData.UnitSize_Trader;
                this.troop3WeaponImage.CustomTooltipData = GameEngine.Instance.LocalWorldData.UnitSize_Spies;
                this.troop4WeaponImage.CustomTooltipData = GameEngine.Instance.LocalWorldData.UnitSize_Scout;
                while (this.numMonk > 0)
                {
                    if ((num4 + (GameEngine.Instance.LocalWorldData.UnitSize_Priests * this.numMonk)) <= GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
                    {
                        break;
                    }
                    this.numMonk--;
                }
                while (this.numTrader > 0)
                {
                    if ((num4 + (GameEngine.Instance.LocalWorldData.UnitSize_Trader * this.numTrader)) <= GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
                    {
                        break;
                    }
                    this.numTrader--;
                }
                if ((num4 + GameEngine.Instance.LocalWorldData.UnitSize_Spies) > GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
                {
                    this.numSpies = 0;
                }
                while (this.numScouts > 0)
                {
                    if ((num4 + (GameEngine.Instance.LocalWorldData.UnitSize_Scout * this.numScouts)) <= GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
                    {
                        break;
                    }
                    this.numScouts--;
                }
                if (num4 >= GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
                {
                    this.numMonk = 0;
                    this.numScouts = 0;
                    this.numSpies = 0;
                    this.numTrader = 0;
                }
                if (GameEngine.Instance.World.UserResearchData.Research_Ordination == 0)
                {
                    this.numMonk = 0;
                    this.troop1Image.Alpha = 0.3f;
                    this.troopMake1ButtonA.Visible = false;
                    this.troopMake1ButtonB.Visible = false;
                    this.troopMake1ButtonX.Visible = false;
                    this.troop1Image.CustomTooltipID = 0x2c3;
                    this.troopsMade1Disband.Visible = false;
                }
                else
                {
                    this.troop1Image.Alpha = 1f;
                    this.troopMake1ButtonA.Visible = true;
                    this.troopMake1ButtonB.Visible = true;
                    this.troopMake1ButtonX.Visible = true;
                    this.troop1Image.CustomTooltipID = 0x2c0;
                    this.troopsMade1Disband.Visible = true;
                }
                if (GameEngine.Instance.World.UserResearchData.Research_Merchant_Guilds == 0)
                {
                    this.numTrader = 0;
                    this.troop2Image.Alpha = 0.3f;
                    this.troopMake2ButtonA.Visible = false;
                    this.troopMake2ButtonB.Visible = false;
                    this.troopMake2ButtonX.Visible = false;
                    this.troop2Image.CustomTooltipID = 0x2c2;
                    this.troopsMade2Disband.Visible = false;
                }
                else
                {
                    this.troop2Image.Alpha = 1f;
                    this.troopMake2ButtonA.Visible = true;
                    this.troopMake2ButtonB.Visible = true;
                    this.troopMake2ButtonX.Visible = true;
                    this.troop2Image.CustomTooltipID = 0x2bf;
                    this.troopsMade2Disband.Visible = true;
                }
                this.numSpies = 0;
                this.troop3Image.Visible = false;
                if (GameEngine.Instance.World.UserResearchData.Research_Scouts == 0)
                {
                    this.numScouts = 0;
                    this.troop4Image.Alpha = 0.3f;
                    this.troopMake4ButtonA.Visible = false;
                    this.troopMake4ButtonB.Visible = false;
                    this.troopMake4ButtonX.Visible = false;
                    this.troop4Image.CustomTooltipID = 0x2c4;
                    this.troopsMade4Disband.Visible = false;
                }
                else
                {
                    this.troop4Image.Alpha = 1f;
                    this.troopMake4ButtonA.Visible = true;
                    this.troopMake4ButtonB.Visible = true;
                    this.troopMake4ButtonX.Visible = true;
                    this.troop4Image.CustomTooltipID = 0x2c1;
                    this.troopsMade4Disband.Visible = true;
                }
                this.troop5Image.Visible = false;
                if (this.numMonk > 0)
                {
                    this.troopMake1Button.Active = true;
                    this.troopMake1Button.Alpha = 1f;
                    this.troopMake1ButtonA.Active = true;
                    this.troopMake1ButtonA.Alpha = 1f;
                    this.troopMake1ButtonB.Active = true;
                    this.troopMake1ButtonB.Alpha = 1f;
                    this.troopMake1ButtonX.Active = true;
                    this.troopMake1ButtonX.Alpha = 1f;
                    this.troopMake1Button.CustomTooltipID = 0;
                    this.troopMake1ButtonA.CustomTooltipID = 0;
                    this.troopMake1ButtonB.CustomTooltipID = 0;
                    this.troopMake1ButtonX.CustomTooltipID = 0;
                }
                else
                {
                    this.troopMake1Button.Active = false;
                    this.troopMake1Button.Alpha = 0.5f;
                    this.troopMake1ButtonA.Active = false;
                    this.troopMake1ButtonA.Alpha = 0.5f;
                    this.troopMake1ButtonB.Active = false;
                    this.troopMake1ButtonB.Alpha = 0.5f;
                    this.troopMake1ButtonX.Active = false;
                    this.troopMake1ButtonX.Alpha = 0.5f;
                    int num11 = 0;
                    if (num2 == 0)
                    {
                        num11 |= 1;
                    }
                    if (num3 < localWorldData.getMonkCost(numMonks))
                    {
                        num11 |= 2;
                    }
                    if ((num10 - numMonks) <= 0)
                    {
                        num11 |= 8;
                    }
                    if ((num4 + GameEngine.Instance.LocalWorldData.UnitSize_Priests) > GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
                    {
                        num11 |= 4;
                    }
                    this.troopMake1Button.CustomTooltipID = 0xa8c;
                    this.troopMake1ButtonA.CustomTooltipID = 0xa8c;
                    this.troopMake1ButtonB.CustomTooltipID = 0xa8c;
                    this.troopMake1ButtonX.CustomTooltipID = 0xa8c;
                    this.troopMake1Button.CustomTooltipData = num11;
                    this.troopMake1ButtonA.CustomTooltipData = num11;
                    this.troopMake1ButtonB.CustomTooltipData = num11;
                    this.troopMake1ButtonX.CustomTooltipData = num11;
                }
                if (this.numTrader > 0)
                {
                    this.troopMake2Button.Active = true;
                    this.troopMake2Button.Alpha = 1f;
                    this.troopMake2ButtonA.Active = true;
                    this.troopMake2ButtonA.Alpha = 1f;
                    this.troopMake2ButtonB.Active = true;
                    this.troopMake2ButtonB.Alpha = 1f;
                    this.troopMake2ButtonX.Active = true;
                    this.troopMake2ButtonX.Alpha = 1f;
                    this.troopMake2Button.CustomTooltipID = 0;
                    this.troopMake2ButtonA.CustomTooltipID = 0;
                    this.troopMake2ButtonB.CustomTooltipID = 0;
                    this.troopMake2ButtonX.CustomTooltipID = 0;
                }
                else
                {
                    this.troopMake2Button.Active = false;
                    this.troopMake2Button.Alpha = 0.5f;
                    this.troopMake2ButtonA.Active = false;
                    this.troopMake2ButtonA.Alpha = 0.5f;
                    this.troopMake2ButtonB.Active = false;
                    this.troopMake2ButtonB.Alpha = 0.5f;
                    this.troopMake2ButtonX.Active = false;
                    this.troopMake2ButtonX.Alpha = 0.5f;
                    int num12 = 0;
                    if (num2 == 0)
                    {
                        num12 |= 1;
                    }
                    if (num3 < localWorldData.TraderGoldCost)
                    {
                        num12 |= 2;
                    }
                    if ((num9 - num5) <= 0)
                    {
                        num12 |= 8;
                    }
                    if ((num4 + GameEngine.Instance.LocalWorldData.UnitSize_Trader) > GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
                    {
                        num12 |= 4;
                    }
                    this.troopMake2Button.CustomTooltipID = 0xa8c;
                    this.troopMake2ButtonA.CustomTooltipID = 0xa8c;
                    this.troopMake2ButtonB.CustomTooltipID = 0xa8c;
                    this.troopMake2ButtonX.CustomTooltipID = 0xa8c;
                    this.troopMake2Button.CustomTooltipData = num12;
                    this.troopMake2ButtonA.CustomTooltipData = num12;
                    this.troopMake2ButtonB.CustomTooltipData = num12;
                    this.troopMake2ButtonX.CustomTooltipData = num12;
                }
                if (this.numSpies > 0)
                {
                    this.troopMake3Button.Active = true;
                    this.troopMake3Button.Alpha = 1f;
                }
                else
                {
                    this.troopMake3Button.Active = false;
                    this.troopMake3Button.Alpha = 0.5f;
                }
                if (this.numScouts > 0)
                {
                    this.troopMake4Button.Active = true;
                    this.troopMake4Button.Alpha = 1f;
                    this.troopMake4ButtonA.Active = true;
                    this.troopMake4ButtonA.Alpha = 1f;
                    this.troopMake4ButtonB.Active = true;
                    this.troopMake4ButtonB.Alpha = 1f;
                    this.troopMake4ButtonX.Active = true;
                    this.troopMake4ButtonX.Alpha = 1f;
                    this.troopMake4Button.CustomTooltipID = 0;
                    this.troopMake4ButtonA.CustomTooltipID = 0;
                    this.troopMake4ButtonB.CustomTooltipID = 0;
                    this.troopMake4ButtonX.CustomTooltipID = 0;
                }
                else
                {
                    this.troopMake4Button.Active = false;
                    this.troopMake4Button.Alpha = 0.5f;
                    this.troopMake4ButtonA.Active = false;
                    this.troopMake4ButtonA.Alpha = 0.5f;
                    this.troopMake4ButtonB.Active = false;
                    this.troopMake4ButtonB.Alpha = 0.5f;
                    this.troopMake4ButtonX.Active = false;
                    this.troopMake4ButtonX.Alpha = 0.5f;
                    int num13 = 0;
                    if (num2 == 0)
                    {
                        num13 |= 1;
                    }
                    if (num3 < localWorldData.ScoutGoldCost)
                    {
                        num13 |= 2;
                    }
                    if ((num8 - num7) <= 0)
                    {
                        num13 |= 8;
                    }
                    if ((num4 + GameEngine.Instance.LocalWorldData.UnitSize_Scout) > GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
                    {
                        num13 |= 4;
                    }
                    this.troopMake4Button.CustomTooltipID = 0xa8c;
                    this.troopMake4ButtonA.CustomTooltipID = 0xa8c;
                    this.troopMake4ButtonB.CustomTooltipID = 0xa8c;
                    this.troopMake4ButtonX.CustomTooltipID = 0xa8c;
                    this.troopMake4Button.CustomTooltipData = num13;
                    this.troopMake4ButtonA.CustomTooltipData = num13;
                    this.troopMake4ButtonB.CustomTooltipData = num13;
                    this.troopMake4ButtonX.CustomTooltipData = num13;
                }
                this.troopsMade1Label.Text = numMonks.ToString("N", nFI) + " / " + num10.ToString("N", nFI);
                this.troopsMade2Label.Text = num5.ToString("N", nFI) + " / " + num9.ToString("N", nFI);
                this.troopsMade3Label.Text = "0";
                this.troopsMade4Label.Text = num7.ToString("N", nFI) + " / " + num8.ToString("N", nFI);
                this.troopMake1Button.Text.Text = this.numMonk.ToString("N", nFI);
                this.troopMake1ButtonB.Text.Text = this.numMonk.ToString("N", nFI);
                if (this.numMonk > 0)
                {
                    this.troopMake1ButtonA.Text.Text = "1";
                    if (this.numMonk >= 4)
                    {
                        this.troopMake1ButtonX.Text.Text = "4";
                    }
                    else
                    {
                        this.troopMake1ButtonX.Text.Text = this.numMonk.ToString("N", nFI);
                    }
                }
                else
                {
                    this.troopMake1ButtonA.Text.Text = "0";
                    this.troopMake1ButtonX.Text.Text = "0";
                }
                this.troopMake2Button.Text.Text = this.numTrader.ToString("N", nFI);
                this.troopMake2ButtonB.Text.Text = this.numTrader.ToString("N", nFI);
                if (this.numTrader > 0)
                {
                    this.troopMake2ButtonA.Text.Text = "1";
                    if (this.numTrader >= 4)
                    {
                        this.troopMake2ButtonX.Text.Text = "4";
                    }
                    else
                    {
                        this.troopMake2ButtonX.Text.Text = this.numTrader.ToString("N", nFI);
                    }
                }
                else
                {
                    this.troopMake2ButtonA.Text.Text = "0";
                    this.troopMake2ButtonX.Text.Text = "0";
                }
                this.troopMake3Button.Text.Text = this.numSpies.ToString("N", nFI);
                this.troopMake4Button.Text.Text = this.numScouts.ToString("N", nFI);
                this.troopMake4ButtonB.Text.Text = this.numScouts.ToString("N", nFI);
                if (this.numScouts > 0)
                {
                    this.troopMake4ButtonA.Text.Text = "1";
                    if (this.numScouts >= 4)
                    {
                        this.troopMake4ButtonX.Text.Text = "4";
                    }
                    else
                    {
                        this.troopMake4ButtonX.Text.Text = this.numScouts.ToString("N", nFI);
                    }
                }
                else
                {
                    this.troopMake4ButtonA.Text.Text = "0";
                    this.troopMake4ButtonX.Text.Text = "0";
                }
                this.troopGoldCost1Label.Text = localWorldData.getMonkCost(numMonks).ToString();
                this.troopGoldCost2Label.Text = localWorldData.TraderGoldCost.ToString();
                this.troopGoldCost3Label.Text = "0";
                this.troopGoldCost4Label.Text = localWorldData.ScoutGoldCost.ToString();
                if (num3 < localWorldData.getMonkCost(numMonks))
                {
                    this.troopGold1Image.Colorise = Color.FromArgb(0xff, 0x80, 0x80);
                }
                else
                {
                    this.troopGold1Image.Colorise = ARGBColors.White;
                }
                if (num3 < localWorldData.TraderGoldCost)
                {
                    this.troopGold2Image.Colorise = Color.FromArgb(0xff, 0x80, 0x80);
                }
                else
                {
                    this.troopGold2Image.Colorise = ARGBColors.White;
                }
                if (num3 < 0)
                {
                    this.troopGold3Image.Colorise = Color.FromArgb(0xff, 0x80, 0x80);
                }
                else
                {
                    this.troopGold3Image.Colorise = ARGBColors.White;
                }
                if (num3 < localWorldData.ScoutGoldCost)
                {
                    this.troopGold4Image.Colorise = Color.FromArgb(0xff, 0x80, 0x80);
                }
                else
                {
                    this.troopGold4Image.Colorise = ARGBColors.White;
                }
                this.fullBar.MaxValue = GameEngine.Instance.LocalWorldData.Village_UnitCapacity;
                if (num4 < GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
                {
                    this.fullBar.Number = num4;
                }
                else
                {
                    this.fullBar.Number = GameEngine.Instance.LocalWorldData.Village_UnitCapacity;
                }
                if ((this.mouseOver >= 0) && ((GameEngine.Instance.LocalWorldData.Village_UnitCapacity - num4) <= 0))
                {
                    this.SpaceLabel.Color = Color.FromArgb(0xff, 0x40, 0x40);
                }
                else
                {
                    this.SpaceLabel.Color = ARGBColors.Black;
                }
            }
        }
    }
}

