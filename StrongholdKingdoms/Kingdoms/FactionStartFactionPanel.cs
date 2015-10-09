namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public class FactionStartFactionPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDLabel abbrvLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel abbrvLabelInfo = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage bar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage bar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage bar3 = new CustomSelfDrawPanel.CSDImage();
        private bool clicksActive = true;
        private CustomSelfDrawPanel.CSDLabel colour1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel colour2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel colour3Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel colour4Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDFill[] colours1 = new CustomSelfDrawPanel.CSDFill[0x20];
        private CustomSelfDrawPanel.CSDFill[] colours2 = new CustomSelfDrawPanel.CSDFill[0x20];
        private CustomSelfDrawPanel.CSDFill[] colours3 = new CustomSelfDrawPanel.CSDFill[0x20];
        private CustomSelfDrawPanel.CSDFill[] colours4 = new CustomSelfDrawPanel.CSDFill[0x20];
        private IContainer components;
        private CustomSelfDrawPanel.CSDButton createButton = new CustomSelfDrawPanel.CSDButton();
        private DockableControl dockableControl;
        private int factionFlagData;
        private CustomSelfDrawPanel.CSDFactionFlagImage flagMinus1 = new CustomSelfDrawPanel.CSDFactionFlagImage();
        private CustomSelfDrawPanel.CSDFactionFlagImage flagMinus2 = new CustomSelfDrawPanel.CSDFactionFlagImage();
        private CustomSelfDrawPanel.CSDButton flagMinusButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDFactionFlagImage flagPlus1 = new CustomSelfDrawPanel.CSDFactionFlagImage();
        private CustomSelfDrawPanel.CSDFactionFlagImage flagPlus2 = new CustomSelfDrawPanel.CSDFactionFlagImage();
        private CustomSelfDrawPanel.CSDButton flagPlusButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage inset1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage inset2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage inset3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage inset4 = new CustomSelfDrawPanel.CSDImage();
        public static FactionStartFactionPanel instance = null;
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDLabel mottoLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel mottoLabelInfo = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel nameLabelInfo = new CustomSelfDrawPanel.CSDLabel();
        public const int PANEL_ID = 0x2f;
        private CustomSelfDrawPanel.CSDLabel rankNeededLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel rankNeededLabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDRectangle selectedColour1 = new CustomSelfDrawPanel.CSDRectangle();
        private CustomSelfDrawPanel.CSDRectangle selectedColour2 = new CustomSelfDrawPanel.CSDRectangle();
        private CustomSelfDrawPanel.CSDRectangle selectedColour3 = new CustomSelfDrawPanel.CSDRectangle();
        private CustomSelfDrawPanel.CSDRectangle selectedColour4 = new CustomSelfDrawPanel.CSDRectangle();
        private CustomSelfDrawPanel.CSDFactionFlagImage selectedFlag = new CustomSelfDrawPanel.CSDFactionFlagImage();
        private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
        public static bool StartFaction = true;
        private TextBox tbFactionName;
        private TextBox tbFactionShortName;
        private TextBox tbMotto;

        public FactionStartFactionPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void changeFactionMottoCallback(ChangeFactionMotto_ReturnType returnData)
        {
            if (returnData.yourFaction != null)
            {
                GameEngine.Instance.World.YourFaction = returnData.yourFaction;
            }
            if (returnData.Success)
            {
                if (returnData.yourFaction != null)
                {
                    InterfaceMgr.Instance.setVillageTabSubMode(0x2e, false);
                }
            }
            else
            {
                MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("FactionsPanel_Faction_Edit_Error", "Faction Edit Error"));
                this.createButton.Enabled = true;
            }
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
            base.clearControls();
            this.closing();
        }

        public void closing()
        {
        }

        private void colours1clicked()
        {
            if (this.clicksActive)
            {
                this.createButton.Enabled = true;
                int data = base.ClickedControl.Data;
                int flag = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                FactionData.getFlagData(this.factionFlagData, ref flag, ref num3, ref num4, ref num5, ref num6);
                this.factionFlagData = FactionData.createFlagData(flag, data, num4, num5, num6);
                this.updateFlags(this.colours1[data], 1);
            }
        }

        private void colours2clicked()
        {
            if (this.clicksActive)
            {
                this.createButton.Enabled = true;
                int data = base.ClickedControl.Data;
                int flag = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                FactionData.getFlagData(this.factionFlagData, ref flag, ref num3, ref num4, ref num5, ref num6);
                this.factionFlagData = FactionData.createFlagData(flag, num3, data, num5, num6);
                this.updateFlags(this.colours2[data], 2);
            }
        }

        private void colours3clicked()
        {
            if (this.clicksActive)
            {
                this.createButton.Enabled = true;
                int data = base.ClickedControl.Data;
                int flag = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                FactionData.getFlagData(this.factionFlagData, ref flag, ref num3, ref num4, ref num5, ref num6);
                this.factionFlagData = FactionData.createFlagData(flag, num3, num4, data, num6);
                this.updateFlags(this.colours3[data], 3);
            }
        }

        private void colours4clicked()
        {
            if (this.clicksActive)
            {
                this.createButton.Enabled = true;
                int data = base.ClickedControl.Data;
                int flag = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                FactionData.getFlagData(this.factionFlagData, ref flag, ref num3, ref num4, ref num5, ref num6);
                this.factionFlagData = FactionData.createFlagData(flag, num3, num4, num5, data);
                this.updateFlags(this.colours4[data], 4);
            }
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        public void createClick()
        {
            if ((((this.tbFactionShortName.Text.Length > 3) && (this.tbFactionName.Text.Length > 3)) && ((this.tbMotto.Text.Length > 3) && StringValidation.isValidGameString(this.tbFactionShortName.Text))) && ((StringValidation.notAllSpaces(this.tbFactionShortName.Text) && StringValidation.isValidGameString(this.tbFactionName.Text)) && StringValidation.notAllSpaces(this.tbFactionName.Text)))
            {
                this.createFaction(this.tbFactionName.Text, this.tbFactionShortName.Text, this.tbMotto.Text);
            }
        }

        public void createFaction(string factionName, string factionNameAbrv, string factionMotto)
        {
            this.createButton.Enabled = false;
            if (StartFaction)
            {
                RemoteServices.Instance.set_CreateFaction_UserCallBack(new RemoteServices.CreateFaction_UserCallBack(this.createFactionCallback));
                RemoteServices.Instance.CreateFaction(factionName, factionNameAbrv, factionMotto, this.factionFlagData);
            }
            else
            {
                RemoteServices.Instance.set_ChangeFactionMotto_UserCallBack(new RemoteServices.ChangeFactionMotto_UserCallBack(this.changeFactionMottoCallback));
                RemoteServices.Instance.ChangeFactionMotto(factionName, factionNameAbrv, factionMotto, this.factionFlagData);
            }
        }

        public void createFactionCallback(CreateFaction_ReturnType returnData)
        {
            if (returnData.Success && (returnData.yourFaction != null))
            {
                RemoteServices.Instance.UserFactionID = returnData.yourFaction.factionID;
                GameEngine.Instance.World.YourFaction = returnData.yourFaction;
                GameEngine.Instance.World.FactionMembers = returnData.members;
                GameEngine.Instance.World.FactionAllies = null;
                GameEngine.Instance.World.FactionEnemies = null;
                GameEngine.Instance.World.HouseAllies = null;
                GameEngine.Instance.World.HouseEnemies = null;
                InterfaceMgr.Instance.getFactionTabBar().forceChangeTab(1);
                GameEngine.Instance.World.LastUpdatedCrowns = DateTime.MinValue;
            }
            else
            {
                MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("FactionsPanel_Faction_Create_Error", "Faction Create Error"));
                this.createButton.Enabled = true;
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

        private void flagDec()
        {
            if (this.clicksActive)
            {
                this.createButton.Enabled = true;
                int flag = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                FactionData.getFlagData(this.factionFlagData, ref flag, ref num2, ref num3, ref num4, ref num5);
                if (flag == 0)
                {
                    flag = 1;
                }
                flag--;
                if (flag < 1)
                {
                    flag = 0x3f;
                }
                this.factionFlagData = FactionData.createFlagData(flag, num2, num3, num4, num5);
                this.updateFlags(null, 0);
            }
        }

        private void flagDec2()
        {
            if (this.clicksActive)
            {
                this.createButton.Enabled = true;
                int flag = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                FactionData.getFlagData(this.factionFlagData, ref flag, ref num2, ref num3, ref num4, ref num5);
                if (flag == 0)
                {
                    flag = 1;
                }
                flag -= 2;
                if (flag < 1)
                {
                    flag += 0x3f;
                }
                this.factionFlagData = FactionData.createFlagData(flag, num2, num3, num4, num5);
                this.updateFlags(null, 0);
            }
        }

        private void flagInc()
        {
            if (this.clicksActive)
            {
                this.createButton.Enabled = true;
                int flag = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                FactionData.getFlagData(this.factionFlagData, ref flag, ref num2, ref num3, ref num4, ref num5);
                if (flag == 0)
                {
                    flag = 1;
                }
                flag++;
                if (flag >= 0x40)
                {
                    flag = 1;
                }
                this.factionFlagData = FactionData.createFlagData(flag, num2, num3, num4, num5);
                this.updateFlags(null, 0);
            }
        }

        private void flagInc2()
        {
            if (this.clicksActive)
            {
                this.createButton.Enabled = true;
                int flag = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                FactionData.getFlagData(this.factionFlagData, ref flag, ref num2, ref num3, ref num4, ref num5);
                if (flag == 0)
                {
                    flag = 1;
                }
                flag += 2;
                if (flag >= 0x40)
                {
                    flag -= 0x3f;
                }
                this.factionFlagData = FactionData.createFlagData(flag, num2, num3, num4, num5);
                this.updateFlags(null, 0);
            }
        }

        public void init(bool resized)
        {
            int height = base.Height;
            instance = this;
            base.clearControls();
            this.sidebar.addSideBar(5, this);
            this.mainBackgroundImage.FillColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = new Size(base.Width - 200, height);
            base.addControl(this.mainBackgroundImage);
            this.backgroundFade.Image = (Image) GFXLibrary.background_top;
            this.backgroundFade.Position = new Point(0, 0);
            this.backgroundFade.Size = new Size(base.Width - 200, this.backgroundFade.Image.Height);
            this.mainBackgroundImage.addControl(this.backgroundFade);
            this.bar1.Image = (Image) GFXLibrary.lineitem_strip_01_dark;
            this.bar1.Position = new Point(30, 20);
            this.mainBackgroundImage.addControl(this.bar1);
            this.bar2.Image = (Image) GFXLibrary.lineitem_strip_01_light;
            this.bar2.Position = new Point(30, 80);
            this.mainBackgroundImage.addControl(this.bar2);
            this.bar3.Image = (Image) GFXLibrary.lineitem_strip_01_dark;
            this.bar3.Position = new Point(30, 140);
            this.mainBackgroundImage.addControl(this.bar3);
            this.nameLabel.Text = SK.Text("CreateFactionPopup_Faction_Name", "Faction Name");
            this.nameLabel.Color = ARGBColors.Black;
            this.nameLabel.Position = new Point(20, 0);
            this.nameLabel.Size = new Size(600, this.bar1.Height);
            this.nameLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.bar1.addControl(this.nameLabel);
            this.nameLabelInfo.Text = SK.Text("CreateFactionPopup_Faction_Name_Length", "(between 4 - 49 characters)");
            this.nameLabelInfo.Color = Color.FromArgb(0x40, 0x40, 0x40);
            this.nameLabelInfo.Position = new Point(0xe1, 0x1a);
            this.nameLabelInfo.Size = new Size(600, 40);
            this.nameLabelInfo.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.nameLabelInfo.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.bar1.addControl(this.nameLabelInfo);
            this.abbrvLabel.Text = SK.Text("CreateFactionPopup_Faction_Short_Name", "Faction Short Name");
            this.abbrvLabel.Color = ARGBColors.Black;
            this.abbrvLabel.Position = new Point(20, 0);
            this.abbrvLabel.Size = new Size(600, this.bar1.Height);
            this.abbrvLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.abbrvLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.bar2.addControl(this.abbrvLabel);
            this.abbrvLabelInfo.Text = SK.Text("CreateFactionPopup_Faction_Short_Name_Length", "(between 4 - 10 characters)");
            this.abbrvLabelInfo.Color = Color.FromArgb(0x40, 0x40, 0x40);
            this.abbrvLabelInfo.Position = new Point(0xe1, 0x1a);
            this.abbrvLabelInfo.Size = new Size(600, 40);
            this.abbrvLabelInfo.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.abbrvLabelInfo.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.bar2.addControl(this.abbrvLabelInfo);
            this.mottoLabel.Text = SK.Text("CreateFactionPopup_Faction_Motto", "Faction Motto");
            this.mottoLabel.Color = ARGBColors.Black;
            this.mottoLabel.Position = new Point(20, 0);
            this.mottoLabel.Size = new Size(600, this.bar3.Height);
            this.mottoLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.mottoLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.bar3.addControl(this.mottoLabel);
            this.mottoLabelInfo.Text = SK.Text("CreateFactionPopup_Faction_Motto_Length", "(between 4 - 49 characters)");
            this.mottoLabelInfo.Color = Color.FromArgb(0x40, 0x40, 0x40);
            this.mottoLabelInfo.Position = new Point(0xe1, 0x1a);
            this.mottoLabelInfo.Size = new Size(600, 40);
            this.mottoLabelInfo.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.mottoLabelInfo.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.bar3.addControl(this.mottoLabelInfo);
            if (StartFaction)
            {
                InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionInvites_Start_Faction", "Start New Faction"));
            }
            else
            {
                InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionInvites_Edit_Faction", "Edit Faction Details"));
            }
            this.createButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
            this.createButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
            this.createButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
            this.createButton.Position = new Point(0x123, 520);
            if (StartFaction)
            {
                this.createButton.Text.Text = SK.Text("CreateFactionPopup_Create", "Create");
            }
            else
            {
                this.createButton.Text.Text = SK.Text("FactionInvites_Apply_Changes", "Apply Changes");
            }
            this.createButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.createButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.createButton.TextYOffset = -3;
            this.createButton.Text.Color = ARGBColors.Black;
            if (!resized)
            {
                this.createButton.Enabled = false;
                this.createButton.Visible = true;
            }
            this.createButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.createClick), "FactionStartFactionPanel_create");
            this.mainBackgroundImage.addControl(this.createButton);
            this.selectedFlag.Position = new Point(0x114, 230);
            this.mainBackgroundImage.addControl(this.selectedFlag);
            this.flagMinus1.Position = new Point(0xa6, 260);
            this.flagMinus1.Scale = 0.5;
            this.flagMinus1.ClickArea = new Rectangle(0, 0, GFXLibrary.factionFlags[0].Width / 2, GFXLibrary.factionFlags[0].Height / 2);
            this.flagMinus1.Visible = false;
            this.flagMinus1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.flagDec), "FactionStartFactionPanel_change");
            this.mainBackgroundImage.addControl(this.flagMinus1);
            this.flagMinus2.Position = new Point(0x2e, 260);
            this.flagMinus2.Scale = 0.5;
            this.flagMinus2.ClickArea = new Rectangle(0, 0, GFXLibrary.factionFlags[0].Width / 2, GFXLibrary.factionFlags[0].Height / 2);
            this.flagMinus2.Visible = false;
            this.flagMinus2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.flagDec2), "FactionStartFactionPanel_change");
            this.mainBackgroundImage.addControl(this.flagMinus2);
            this.flagPlus1.Position = new Point(0x1fa, 260);
            this.flagPlus1.Scale = 0.5;
            this.flagPlus1.ClickArea = new Rectangle(0, 0, GFXLibrary.factionFlags[0].Width / 2, GFXLibrary.factionFlags[0].Height / 2);
            this.flagPlus1.Visible = false;
            this.flagPlus1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.flagInc), "FactionStartFactionPanel_change");
            this.mainBackgroundImage.addControl(this.flagPlus1);
            this.flagPlus2.Position = new Point(0x272, 260);
            this.flagPlus2.Scale = 0.5;
            this.flagPlus2.ClickArea = new Rectangle(0, 0, GFXLibrary.factionFlags[0].Width / 2, GFXLibrary.factionFlags[0].Height / 2);
            this.flagPlus2.Visible = false;
            this.flagPlus2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.flagInc2), "FactionStartFactionPanel_change");
            this.mainBackgroundImage.addControl(this.flagPlus2);
            this.flagPlusButton.ImageNorm = (Image) GFXLibrary.arrow_button_right_normal;
            this.flagPlusButton.ImageOver = (Image) GFXLibrary.arrow_button_right_over;
            this.flagPlusButton.ImageClick = (Image) GFXLibrary.arrow_button_right_pushed;
            this.flagPlusButton.Position = new Point(0x2ea, 0x10d);
            this.flagPlusButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.flagInc), "FactionStartFactionPanel_change");
            this.mainBackgroundImage.addControl(this.flagPlusButton);
            this.flagMinusButton.ImageNorm = (Image) GFXLibrary.arrow_button_left_normal;
            this.flagMinusButton.ImageOver = (Image) GFXLibrary.arrow_button_left_over;
            this.flagMinusButton.ImageClick = (Image) GFXLibrary.arrow_button_left_pushed;
            this.flagMinusButton.Position = new Point(2, 0x10d);
            this.flagMinusButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.flagDec), "FactionStartFactionPanel_change");
            this.mainBackgroundImage.addControl(this.flagMinusButton);
            this.inset1.Image = (Image) GFXLibrary.faction_inset;
            this.inset1.Position = new Point(7, 0x176);
            this.mainBackgroundImage.addControl(this.inset1);
            this.inset2.Image = (Image) GFXLibrary.faction_inset;
            this.inset2.Position = new Point(0xcf, 0x176);
            this.mainBackgroundImage.addControl(this.inset2);
            this.inset3.Image = (Image) GFXLibrary.faction_inset;
            this.inset3.Position = new Point(0x197, 0x176);
            this.mainBackgroundImage.addControl(this.inset3);
            this.inset4.Image = (Image) GFXLibrary.faction_inset;
            this.inset4.Position = new Point(0x25f, 0x176);
            this.mainBackgroundImage.addControl(this.inset4);
            for (int i = 0; i < 0x20; i++)
            {
                this.colours1[i] = new CustomSelfDrawPanel.CSDFill();
                this.colours1[i].Position = new Point(0x11 + ((i % 8) * 20), 400 + ((i / 8) * 20));
                this.colours1[i].FillColor = FactionData.getColour(i);
                this.colours1[i].Size = new Size(20, 20);
                this.colours1[i].Data = i;
                this.colours1[i].setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.colours1clicked), "FactionStartFactionPanel_colours");
                this.mainBackgroundImage.addControl(this.colours1[i]);
            }
            for (int j = 0; j < 0x20; j++)
            {
                this.colours2[j] = new CustomSelfDrawPanel.CSDFill();
                this.colours2[j].Position = new Point(0xd9 + ((j % 8) * 20), 400 + ((j / 8) * 20));
                this.colours2[j].FillColor = FactionData.getColour(j);
                this.colours2[j].Size = new Size(20, 20);
                this.colours2[j].Data = j;
                this.colours2[j].setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.colours2clicked), "FactionStartFactionPanel_colours");
                this.mainBackgroundImage.addControl(this.colours2[j]);
            }
            for (int k = 0; k < 0x20; k++)
            {
                this.colours3[k] = new CustomSelfDrawPanel.CSDFill();
                this.colours3[k].Position = new Point(0x1a1 + ((k % 8) * 20), 400 + ((k / 8) * 20));
                this.colours3[k].FillColor = FactionData.getColour(k);
                this.colours3[k].Size = new Size(20, 20);
                this.colours3[k].Data = k;
                this.colours3[k].setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.colours3clicked), "FactionStartFactionPanel_colours");
                this.mainBackgroundImage.addControl(this.colours3[k]);
            }
            for (int m = 0; m < 0x20; m++)
            {
                this.colours4[m] = new CustomSelfDrawPanel.CSDFill();
                this.colours4[m].Position = new Point(0x269 + ((m % 8) * 20), 400 + ((m / 8) * 20));
                this.colours4[m].FillColor = FactionData.getColour(m);
                this.colours4[m].Size = new Size(20, 20);
                this.colours4[m].Data = m;
                this.colours4[m].setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.colours4clicked), "FactionStartFactionPanel_colours");
                this.mainBackgroundImage.addControl(this.colours4[m]);
            }
            this.selectedColour1.LineColor = ARGBColors.Black;
            this.selectedColour1.Size = new Size(20, 20);
            this.mainBackgroundImage.addControl(this.selectedColour1);
            this.selectedColour2.LineColor = ARGBColors.Black;
            this.selectedColour2.Size = new Size(20, 20);
            this.mainBackgroundImage.addControl(this.selectedColour2);
            this.selectedColour3.LineColor = ARGBColors.Black;
            this.selectedColour3.Size = new Size(20, 20);
            this.mainBackgroundImage.addControl(this.selectedColour3);
            this.selectedColour4.LineColor = ARGBColors.Black;
            this.selectedColour4.Size = new Size(20, 20);
            this.mainBackgroundImage.addControl(this.selectedColour4);
            this.colour1Label.Text = SK.Text("FactionFlags_colour1", "Colour 1");
            this.colour1Label.Color = ARGBColors.Black;
            this.colour1Label.Position = new Point(0x11, 0x177);
            this.colour1Label.Size = new Size(160, 0x19);
            this.colour1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.colour1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.mainBackgroundImage.addControl(this.colour1Label);
            this.colour2Label.Text = SK.Text("FactionFlags_colour2", "Colour 2");
            this.colour2Label.Color = ARGBColors.Black;
            this.colour2Label.Position = new Point(0xd9, 0x177);
            this.colour2Label.Size = new Size(160, 0x19);
            this.colour2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.colour2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.mainBackgroundImage.addControl(this.colour2Label);
            this.colour3Label.Text = SK.Text("FactionFlags_colour3", "Colour 3");
            this.colour3Label.Color = ARGBColors.Black;
            this.colour3Label.Position = new Point(0x1a1, 0x177);
            this.colour3Label.Size = new Size(160, 0x19);
            this.colour3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.colour3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.mainBackgroundImage.addControl(this.colour3Label);
            this.colour4Label.Text = SK.Text("FactionFlags_colour4", "Colour 4");
            this.colour4Label.Color = ARGBColors.Black;
            this.colour4Label.Position = new Point(0x269, 0x177);
            this.colour4Label.Size = new Size(160, 0x19);
            this.colour4Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.colour4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.mainBackgroundImage.addControl(this.colour4Label);
            if (!resized)
            {
                if (StartFaction)
                {
                    this.tbFactionName.Text = "";
                    this.tbFactionShortName.Text = "";
                    this.tbMotto.Text = "";
                    this.factionFlagData = FactionData.createFlagData(1, 9, 15, 4, 0x1c);
                }
                else
                {
                    this.tbFactionName.Text = GameEngine.Instance.World.YourFaction.factionName;
                    this.tbFactionShortName.Text = GameEngine.Instance.World.YourFaction.factionNameAbrv;
                    this.tbMotto.Text = GameEngine.Instance.World.YourFaction.factionMotto;
                    this.factionFlagData = GameEngine.Instance.World.YourFaction.flagData;
                    if (this.factionFlagData <= 0)
                    {
                        this.factionFlagData = FactionData.createFlagData(1, 9, 15, 4, 0x1c);
                    }
                }
            }
            this.updateFlags(null, 0);
            if (GameEngine.Instance.World.getRank() < (GameEngine.Instance.LocalWorldData.Faction_CreateAtLevel - 1))
            {
                if (GameEngine.Instance.LocalWorldData.Faction_CreateAtLevel == 12)
                {
                    this.rankNeededLabel.Text = SK.Text("FactionsPanel_Rank_Needed_12", "You don't currently have the required Rank (12) to create a Faction.");
                }
                else
                {
                    this.rankNeededLabel.Text = SK.Text("FactionsPanel_Rank_Needed", "You don't currently have the required Rank (14) to create a Faction.");
                }
                this.rankNeededLabel.Color = ARGBColors.Black;
                this.rankNeededLabel.Position = new Point(0, 190);
                this.rankNeededLabel.Size = new Size(this.mainBackgroundImage.Size.Width, 40);
                this.rankNeededLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.rankNeededLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.mainBackgroundImage.addControl(this.rankNeededLabel);
                this.createButton.Visible = false;
                this.tbFactionName.Enabled = false;
                this.tbFactionShortName.Enabled = false;
                this.tbMotto.Enabled = false;
                this.flagPlusButton.Enabled = false;
                this.flagMinusButton.Enabled = false;
                this.clicksActive = false;
                for (int n = 0; n < 0x20; n++)
                {
                    this.colours1[n].FillColor = Color.FromArgb(0x80, this.colours1[n].FillColor);
                    this.colours2[n].FillColor = Color.FromArgb(0x80, this.colours2[n].FillColor);
                    this.colours3[n].FillColor = Color.FromArgb(0x80, this.colours3[n].FillColor);
                    this.colours4[n].FillColor = Color.FromArgb(0x80, this.colours4[n].FillColor);
                }
                this.inset1.Alpha = 0.3f;
                this.inset2.Alpha = 0.3f;
                this.inset3.Alpha = 0.3f;
                this.inset4.Alpha = 0.3f;
                this.flagMinus1.Alpha = 0.3f;
                this.flagMinus2.Alpha = 0.3f;
                this.flagPlus1.Alpha = 0.3f;
                this.flagPlus2.Alpha = 0.3f;
                int flag = 0;
                int num8 = 0;
                int num9 = 0;
                int num10 = 0;
                int num11 = 0;
                FactionData.getFlagData(this.factionFlagData, ref flag, ref num8, ref num9, ref num10, ref num11);
                ColorMap[] mapArray = FactionData.getColourMap(num8, num9, num10, num11, 100);
                this.selectedFlag.ColourMap = mapArray;
                this.flagMinus2.ColourMap = mapArray;
                this.flagMinus1.ColourMap = mapArray;
                this.flagPlus1.ColourMap = mapArray;
                this.flagPlus2.ColourMap = mapArray;
            }
            else
            {
                this.createButton.Visible = true;
                this.tbFactionName.Enabled = true;
                this.tbFactionShortName.Enabled = true;
                this.tbMotto.Enabled = true;
                this.flagPlusButton.Enabled = true;
                this.flagMinusButton.Enabled = true;
                this.clicksActive = true;
                this.inset1.Alpha = 1f;
                this.inset2.Alpha = 1f;
                this.inset3.Alpha = 1f;
                this.inset4.Alpha = 1f;
                this.flagMinus1.Alpha = 1f;
                this.flagMinus2.Alpha = 1f;
                this.flagPlus1.Alpha = 1f;
                this.flagPlus2.Alpha = 1f;
            }
            if (!resized)
            {
                CustomSelfDrawPanel.FactionPanelSideBar.downloadCurrentFactionInfo();
            }
        }

        private void InitializeComponent()
        {
            this.tbMotto = new TextBox();
            this.tbFactionShortName = new TextBox();
            this.tbFactionName = new TextBox();
            base.SuspendLayout();
            this.tbMotto.Location = new Point(0x102, 0x92);
            this.tbMotto.MaxLength = 0x31;
            this.tbMotto.Name = "tbMotto";
            this.tbMotto.Size = new Size(0xed, 20);
            this.tbMotto.TabIndex = 5;
            this.tbMotto.TextChanged += new EventHandler(this.tbFactionName_TextChanged);
            this.tbFactionShortName.Location = new Point(0x102, 0x56);
            this.tbFactionShortName.MaxLength = 10;
            this.tbFactionShortName.Name = "tbFactionShortName";
            this.tbFactionShortName.Size = new Size(0x79, 20);
            this.tbFactionShortName.TabIndex = 4;
            this.tbFactionShortName.TextChanged += new EventHandler(this.tbFactionName_TextChanged);
            this.tbFactionName.Location = new Point(0x102, 0x1a);
            this.tbFactionName.MaxLength = 0x31;
            this.tbFactionName.Name = "tbFactionName";
            this.tbFactionName.Size = new Size(0xed, 20);
            this.tbFactionName.TabIndex = 3;
            this.tbFactionName.TextChanged += new EventHandler(this.tbFactionName_TextChanged);
            base.AutoScaleMode = AutoScaleMode.None;
            base.Controls.Add(this.tbMotto);
            base.Controls.Add(this.tbFactionShortName);
            base.Controls.Add(this.tbFactionName);
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "FactionStartFactionPanel";
            base.Size = new Size(0x3e0, 0x236);
            base.ResumeLayout(false);
            base.PerformLayout();
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

        public void logout()
        {
        }

        private void tbFactionName_TextChanged(object sender, EventArgs e)
        {
            if ((((this.tbFactionShortName.Text.Length > 3) && (this.tbFactionName.Text.Length > 3)) && ((this.tbMotto.Text.Length > 3) && StringValidation.isValidGameString(this.tbFactionShortName.Text))) && ((StringValidation.notAllSpaces(this.tbFactionShortName.Text) && StringValidation.isValidGameString(this.tbFactionName.Text)) && StringValidation.notAllSpaces(this.tbFactionName.Text)))
            {
                this.createButton.Enabled = true;
            }
            else
            {
                this.createButton.Enabled = false;
            }
        }

        public void update()
        {
            this.sidebar.update();
            if ((((this.tbFactionShortName.Text.Length > 3) && (this.tbFactionName.Text.Length > 3)) && ((this.tbMotto.Text.Length > 3) && StringValidation.isValidGameString(this.tbFactionShortName.Text))) && ((StringValidation.notAllSpaces(this.tbFactionShortName.Text) && StringValidation.isValidGameString(this.tbFactionName.Text)) && StringValidation.notAllSpaces(this.tbFactionName.Text)))
            {
                this.createButton.Enabled = true;
            }
            else
            {
                this.createButton.Enabled = false;
            }
        }

        public void updateFlags(CustomSelfDrawPanel.CSDFill fill, int fillBoxNumber)
        {
            int flag = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            FactionData.getFlagData(this.factionFlagData, ref flag, ref num2, ref num3, ref num4, ref num5);
            if (flag == 0)
            {
                flag = 1;
            }
            if ((flag - 2) >= 1)
            {
                this.flagMinus2.Visible = true;
                this.flagMinus2.Image = (Image) GFXLibrary.factionFlags[flag - 2];
            }
            else
            {
                this.flagMinus2.Visible = true;
                this.flagMinus2.Image = (Image) GFXLibrary.factionFlags[(flag - 2) + 0x3f];
            }
            if ((flag - 1) >= 1)
            {
                this.flagMinus1.Visible = true;
                this.flagMinus1.Image = (Image) GFXLibrary.factionFlags[flag - 1];
            }
            else
            {
                this.flagMinus1.Visible = true;
                this.flagMinus1.Image = (Image) GFXLibrary.factionFlags[(flag - 1) + 0x3f];
            }
            if ((flag + 1) < GFXLibrary.factionFlags.Length)
            {
                this.flagPlus1.Visible = true;
                this.flagPlus1.Image = (Image) GFXLibrary.factionFlags[flag + 1];
            }
            else
            {
                this.flagPlus1.Visible = true;
                this.flagPlus1.Image = (Image) GFXLibrary.factionFlags[(flag + 1) - 0x3f];
            }
            if ((flag + 2) < GFXLibrary.factionFlags.Length)
            {
                this.flagPlus2.Visible = true;
                this.flagPlus2.Image = (Image) GFXLibrary.factionFlags[flag + 2];
            }
            else
            {
                this.flagPlus2.Visible = true;
                this.flagPlus2.Image = (Image) GFXLibrary.factionFlags[(flag + 2) - 0x3f];
            }
            this.selectedFlag.Image = (Image) GFXLibrary.factionFlags[flag];
            ColorMap[] mapArray = FactionData.getColourMap(num2, num3, num4, num5, 0xff);
            this.selectedFlag.ColourMap = mapArray;
            this.flagMinus2.ColourMap = mapArray;
            this.flagMinus1.ColourMap = mapArray;
            this.flagPlus1.ColourMap = mapArray;
            this.flagPlus2.ColourMap = mapArray;
            this.selectedColour1.Position = this.colours1[num2].Position;
            this.selectedColour2.Position = this.colours2[num3].Position;
            this.selectedColour3.Position = this.colours3[num4].Position;
            this.selectedColour4.Position = this.colours4[num5].Position;
            this.selectedColour1.LineColor = ARGBColors.Black;
            this.selectedColour2.LineColor = ARGBColors.Black;
            this.selectedColour3.LineColor = ARGBColors.Black;
            this.selectedColour4.LineColor = ARGBColors.Black;
            switch (num2)
            {
                case 11:
                case 14:
                case 15:
                case 1:
                case 0x16:
                case 0x19:
                case 0x1a:
                case 0x1b:
                    this.selectedColour1.LineColor = ARGBColors.White;
                    break;
            }
            switch (num3)
            {
                case 11:
                case 14:
                case 15:
                case 1:
                case 0x16:
                case 0x19:
                case 0x1a:
                case 0x1b:
                    this.selectedColour2.LineColor = ARGBColors.White;
                    break;
            }
            switch (num4)
            {
                case 11:
                case 14:
                case 15:
                case 1:
                case 0x16:
                case 0x19:
                case 0x1a:
                case 0x1b:
                    this.selectedColour3.LineColor = ARGBColors.White;
                    break;
            }
            switch (num5)
            {
                case 11:
                case 14:
                case 15:
                case 1:
                case 0x16:
                case 0x19:
                case 0x1a:
                case 0x1b:
                    this.selectedColour4.LineColor = ARGBColors.White;
                    break;
            }
            base.Invalidate();
        }
    }
}

