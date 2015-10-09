namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class FactionMyFactionPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDImage backImage1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage barImage1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage barImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage barImage3 = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private CustomSelfDrawPanel.CSDButton diplomacyAllyButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDExtendingPanel diplomacyBackgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDButton diplomacyButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton diplomacyCancelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel diplomacyCurrentLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel diplomacyCurrentLabelHeader = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton diplomacyEnemyButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel diplomacyFactionLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel diplomacyHeaderImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel diplomacyHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel diplomacyLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton diplomacyNeutralButton = new CustomSelfDrawPanel.CSDButton();
        private bool diplomacyOverlayVisible;
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDLabel factionMottoLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel factionNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDFactionFlagImage flagimage = new CustomSelfDrawPanel.CSDFactionFlagImage();
        private CustomSelfDrawPanel.CSDFill greyOverlay = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel houseLabel = new CustomSelfDrawPanel.CSDLabel();
        public static FactionMyFactionPanel instance = null;
        private List<FactionMemberLine> lineList = new List<FactionMemberLine>();
        private static int m_selectedFaction = -1;
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDLabel membersLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel membersLabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        public const int PANEL_ID = 0x2a;
        private CustomSelfDrawPanel.CSDLabel playerNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel pointsHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel pointsHeaderLabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel rankHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel rankHeaderLabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel rankLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel villagesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDExtendingPanel wallInfoImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();

        public FactionMyFactionPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void addDiplomacyOverlay()
        {
            FactionData data = GameEngine.Instance.World.getFaction(m_selectedFaction);
            if (data != null)
            {
                this.removeOverlay();
                this.diplomacyOverlayVisible = true;
                this.greyOverlay.Size = base.Size;
                this.greyOverlay.FillColor = Color.FromArgb(0x80, 0, 0, 0);
                this.greyOverlay.setClickDelegate(delegate {
                });
                this.mainBackgroundImage.addControl(this.greyOverlay);
                this.diplomacyHeaderImage.Size = new Size(400, 40);
                this.diplomacyHeaderImage.Position = new Point(((base.Width - 200) - 400) / 2, 100);
                this.greyOverlay.addControl(this.diplomacyHeaderImage);
                this.diplomacyHeaderImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
                this.diplomacyBackgroundImage.Size = new Size(400, 300);
                this.diplomacyBackgroundImage.Position = new Point(((base.Width - 200) - 400) / 2, 140);
                this.greyOverlay.addControl(this.diplomacyBackgroundImage);
                this.diplomacyBackgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
                this.diplomacyHeadingLabel.Text = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy");
                this.diplomacyHeadingLabel.Color = ARGBColors.White;
                this.diplomacyHeadingLabel.Position = new Point(0, 0);
                this.diplomacyHeadingLabel.Size = this.diplomacyHeaderImage.Size;
                this.diplomacyHeadingLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
                this.diplomacyHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.diplomacyHeaderImage.addControl(this.diplomacyHeadingLabel);
                this.diplomacyFactionLabel.Text = data.factionName;
                this.diplomacyFactionLabel.Color = ARGBColors.Black;
                this.diplomacyFactionLabel.Position = new Point(0, 8);
                this.diplomacyFactionLabel.Size = new Size(this.diplomacyBackgroundImage.Width, 30);
                this.diplomacyFactionLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.diplomacyFactionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.diplomacyBackgroundImage.addControl(this.diplomacyFactionLabel);
                this.diplomacyCurrentLabelHeader.Text = SK.Text("GENERIC_Current_Relationship", "Current Relationship");
                this.diplomacyCurrentLabelHeader.Color = ARGBColors.Black;
                this.diplomacyCurrentLabelHeader.Position = new Point(0, 40);
                this.diplomacyCurrentLabelHeader.Size = new Size(this.diplomacyBackgroundImage.Width, 30);
                this.diplomacyCurrentLabelHeader.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.diplomacyCurrentLabelHeader.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.diplomacyBackgroundImage.addControl(this.diplomacyCurrentLabelHeader);
                string str = "";
                int num = GameEngine.Instance.World.getYourFactionRelation(m_selectedFaction);
                if (num == 0)
                {
                    str = str + SK.Text("GENERIC_Neutral", "Neutral");
                }
                else if (num > 0)
                {
                    str = str + SK.Text("GENERIC_Ally", "Ally");
                }
                else if (num < 0)
                {
                    str = str + SK.Text("GENERIC_Enemy", "Enemy");
                }
                this.diplomacyCurrentLabel.Text = str;
                this.diplomacyCurrentLabel.Color = ARGBColors.Black;
                this.diplomacyCurrentLabel.Position = new Point(0, 0x41);
                this.diplomacyCurrentLabel.Size = new Size(this.diplomacyBackgroundImage.Width, 30);
                this.diplomacyCurrentLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.diplomacyCurrentLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.diplomacyBackgroundImage.addControl(this.diplomacyCurrentLabel);
                this.diplomacyNeutralButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                this.diplomacyNeutralButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                this.diplomacyNeutralButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                this.diplomacyNeutralButton.Position = new Point(0x5f, 100);
                this.diplomacyNeutralButton.Text.Text = SK.Text("FactionsDiplomacy_Set_as_neutral", "Set As Neutral");
                this.diplomacyNeutralButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.diplomacyNeutralButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.diplomacyNeutralButton.TextYOffset = -3;
                this.diplomacyNeutralButton.Text.Color = ARGBColors.Black;
                this.diplomacyNeutralButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnBreakAlliance_Click), "FactionMyFactionPanel_neutral_clicked");
                this.diplomacyBackgroundImage.addControl(this.diplomacyNeutralButton);
                this.diplomacyAllyButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                this.diplomacyAllyButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                this.diplomacyAllyButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                this.diplomacyAllyButton.Position = new Point(0x5f, 150);
                this.diplomacyAllyButton.Text.Text = SK.Text("FactionsDiplomacy_Set_as_ally", "Set As Ally");
                this.diplomacyAllyButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.diplomacyAllyButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.diplomacyAllyButton.TextYOffset = -3;
                this.diplomacyAllyButton.Text.Color = ARGBColors.Black;
                this.diplomacyAllyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAlly_Click), "FactionMyFactionPanel_ally_clicked");
                this.diplomacyBackgroundImage.addControl(this.diplomacyAllyButton);
                this.diplomacyEnemyButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                this.diplomacyEnemyButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                this.diplomacyEnemyButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                this.diplomacyEnemyButton.Position = new Point(0x5f, 200);
                this.diplomacyEnemyButton.Text.Text = SK.Text("FactionsDiplomacy_Set_as_enemy", "Set As Enemy");
                this.diplomacyEnemyButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.diplomacyEnemyButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.diplomacyEnemyButton.TextYOffset = -3;
                this.diplomacyEnemyButton.Text.Color = ARGBColors.Black;
                this.diplomacyEnemyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnMakeEnemy_Click), "FactionMyFactionPanel_enemy_clicked");
                this.diplomacyBackgroundImage.addControl(this.diplomacyEnemyButton);
                this.diplomacyCancelButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                this.diplomacyCancelButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                this.diplomacyCancelButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                this.diplomacyCancelButton.Position = new Point(130, 250);
                this.diplomacyCancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
                this.diplomacyCancelButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.diplomacyCancelButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.diplomacyCancelButton.TextYOffset = -3;
                this.diplomacyCancelButton.Text.Color = ARGBColors.Black;
                this.diplomacyCancelButton.setClickDelegate(() => this.removeOverlay(), "FactionMyFactionPanel_dipomacy_close");
                this.diplomacyBackgroundImage.addControl(this.diplomacyCancelButton);
                this.diplomacyEnemyButton.Enabled = true;
                this.diplomacyAllyButton.Enabled = true;
                this.diplomacyNeutralButton.Enabled = true;
            }
        }

        public void addPlayers(FactionMemberData[] fmd)
        {
            this.wallScrollArea.clearControls();
            int y = 0;
            this.lineList.Clear();
            int position = 0;
            if (fmd != null)
            {
                if (m_selectedFaction != RemoteServices.Instance.UserFactionID)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int num4 = 1;
                        switch (i)
                        {
                            case 1:
                                num4 = 2;
                                break;

                            case 2:
                                num4 = 0;
                                break;
                        }
                        foreach (FactionMemberData data in fmd)
                        {
                            if (data.status == num4)
                            {
                                FactionMemberLine control = new FactionMemberLine();
                                if (y != 0)
                                {
                                    y += 5;
                                }
                                control.Position = new Point(0, y);
                                control.init(data, position, this, false);
                                this.wallScrollArea.addControl(control);
                                y += control.Height;
                                this.lineList.Add(control);
                                position++;
                            }
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int num6 = 1;
                        switch (j)
                        {
                            case 1:
                                num6 = 2;
                                break;

                            case 2:
                                num6 = 0;
                                break;

                            case 3:
                                num6 = -1;
                                break;
                        }
                        foreach (FactionMemberData data2 in fmd)
                        {
                            if (data2.status == num6)
                            {
                                FactionMemberLine line2 = new FactionMemberLine();
                                if (y != 0)
                                {
                                    y += 5;
                                }
                                line2.Position = new Point(0, y);
                                line2.init(data2, position, this, true);
                                this.wallScrollArea.addControl(line2);
                                y += line2.Height;
                                this.lineList.Add(line2);
                                position++;
                            }
                        }
                    }
                }
            }
            this.wallScrollArea.Size = new Size(this.wallScrollArea.Width, y);
            if (y < this.wallScrollBar.Height)
            {
                this.wallScrollBar.Visible = false;
            }
            else
            {
                this.wallScrollBar.Visible = true;
                this.wallScrollBar.NumVisibleLines = this.wallScrollBar.Height;
                this.wallScrollBar.Max = y - this.wallScrollBar.Height;
            }
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
            this.updateRelationshipText();
            this.update();
            base.Invalidate();
        }

        public void applyClicked()
        {
            this.diplomacyButton.Enabled = false;
            RemoteServices.Instance.set_FactionApplication_UserCallBack(new RemoteServices.FactionApplication_UserCallBack(this.factionApplicationCallback));
            RemoteServices.Instance.FactionApplication(SelectedFaction);
        }

        private void btnAlly_Click()
        {
            this.diplomacyEnemyButton.Enabled = false;
            this.diplomacyAllyButton.Enabled = false;
            this.diplomacyNeutralButton.Enabled = false;
            RemoteServices.Instance.set_CreateFactionRelationship_UserCallBack(new RemoteServices.CreateFactionRelationship_UserCallBack(this.createFactionRelationshipCallback));
            RemoteServices.Instance.CreateFactionRelationship(m_selectedFaction, 1);
        }

        private void btnBreakAlliance_Click()
        {
            this.diplomacyEnemyButton.Enabled = false;
            this.diplomacyAllyButton.Enabled = false;
            this.diplomacyNeutralButton.Enabled = false;
            RemoteServices.Instance.set_CreateFactionRelationship_UserCallBack(new RemoteServices.CreateFactionRelationship_UserCallBack(this.createFactionRelationshipCallback));
            RemoteServices.Instance.CreateFactionRelationship(m_selectedFaction, 0);
        }

        private void btnMakeEnemy_Click()
        {
            this.diplomacyEnemyButton.Enabled = false;
            this.diplomacyAllyButton.Enabled = false;
            this.diplomacyNeutralButton.Enabled = false;
            RemoteServices.Instance.set_CreateFactionRelationship_UserCallBack(new RemoteServices.CreateFactionRelationship_UserCallBack(this.createFactionRelationshipCallback));
            RemoteServices.Instance.CreateFactionRelationship(m_selectedFaction, -1);
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

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        private void createFactionRelationshipCallback(CreateFactionRelationship_ReturnType returnData)
        {
            if (returnData.Success)
            {
                GameEngine.Instance.World.FactionAllies = returnData.yourAllies;
                GameEngine.Instance.World.FactionEnemies = returnData.yourEnemies;
                this.diplomacyOverlayVisible = false;
                this.init(false);
            }
            else
            {
                this.diplomacyEnemyButton.Enabled = true;
                this.diplomacyAllyButton.Enabled = true;
                this.diplomacyNeutralButton.Enabled = true;
            }
        }

        private void diplomacyClicked()
        {
            this.addDiplomacyOverlay();
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

        public void factionApplicationCallback(FactionApplication_ReturnType returnData)
        {
            this.diplomacyButton.Enabled = true;
            if (returnData.Success)
            {
                GameEngine.Instance.World.FactionInvites = returnData.invites;
                GameEngine.Instance.World.FactionApplications = returnData.applications;
                this.diplomacyButton.Visible = false;
                this.diplomacyLabel.Text = SK.Text("FactionInvites_Application Pending", "Application Pending");
                this.diplomacyLabel.Color = ARGBColors.Black;
                this.diplomacyLabel.Position = new Point(0x18, 0x7e);
                this.diplomacyLabel.Size = new Size(240, 40);
                this.diplomacyLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.diplomacyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.mainBackgroundImage.addControl(this.diplomacyLabel);
                this.diplomacyLabel.Visible = true;
                MyMessageBox.Show(SK.Text("FactionInvites_Have_Applied", "You have now applied to join a faction.  Click on the Invites tab to view your current applications."), SK.Text("FactionInvites_Faction_Application", "Faction Application"));
            }
        }

        private void getViewFactionDataCallback(GetViewFactionData_ReturnType returnData)
        {
            if (returnData.Success)
            {
                NumberFormatInfo nFI = GameEngine.NFI;
                this.addPlayers(returnData.members);
                if (returnData.factionData != null)
                {
                    GameEngine.Instance.World.setFactionMemberData(returnData.factionData.factionID, returnData.members);
                }
                GameEngine.Instance.World.setFactionData(returnData.factionData);
                GameEngine.Instance.World.FactionAllies = returnData.yourAllies;
                GameEngine.Instance.World.FactionEnemies = returnData.yourEnemies;
            }
        }

        public void houseClicked()
        {
            FactionData data = GameEngine.Instance.World.getFaction(m_selectedFaction);
            if ((data != null) && (data.houseID > 0))
            {
                InterfaceMgr.Instance.showHousePanel(data.houseID);
            }
        }

        public void init(bool resized)
        {
            int height = base.Height;
            instance = this;
            base.clearControls();
            NumberFormatInfo nFI = GameEngine.NFI;
            this.sidebar.addSideBar(1, this);
            FactionData data = GameEngine.Instance.World.getFaction(m_selectedFaction);
            if (data == null)
            {
                data = new FactionData();
            }
            this.greyOverlay.Size = base.Size;
            this.mainBackgroundImage.FillColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = new Size(base.Width - 200, height);
            base.addControl(this.mainBackgroundImage);
            this.backgroundFade.Image = (Image) GFXLibrary.background_top;
            this.backgroundFade.Position = new Point(0, 0);
            this.backgroundFade.Size = new Size(base.Width - 200, this.backgroundFade.Image.Height);
            this.mainBackgroundImage.addControl(this.backgroundFade);
            this.backImage1.Image = (Image) GFXLibrary.faction_tanback;
            this.backImage1.Position = new Point((this.mainBackgroundImage.Size.Width - this.backImage1.Size.Width) - 0x19, 12);
            this.mainBackgroundImage.addControl(this.backImage1);
            this.backImage2.Image = (Image) GFXLibrary.faction_title_band;
            this.backImage2.Position = new Point(20, 20);
            this.mainBackgroundImage.addControl(this.backImage2);
            this.barImage1.Image = (Image) GFXLibrary.faction_bar_tan_1_heavier;
            this.barImage1.Position = new Point(0x114, 70);
            this.mainBackgroundImage.addControl(this.barImage1);
            this.barImage2.Image = (Image) GFXLibrary.faction_bar_tan_1_lighter;
            this.barImage2.Position = new Point(0x114, 0x5e);
            this.mainBackgroundImage.addControl(this.barImage2);
            this.barImage3.Image = (Image) GFXLibrary.faction_bar_tan_1_heavier;
            this.barImage3.Position = new Point(0x114, 0x76);
            this.mainBackgroundImage.addControl(this.barImage3);
            this.factionNameLabel.Text = data.factionName;
            this.factionNameLabel.Color = ARGBColors.Black;
            this.factionNameLabel.Position = new Point(0xcd, 10);
            this.factionNameLabel.Size = new Size(600, 40);
            this.factionNameLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Regular);
            this.factionNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.factionNameLabel);
            this.factionMottoLabel.Text = "\"" + data.factionMotto + "\"";
            this.factionMottoLabel.Color = ARGBColors.Black;
            this.factionMottoLabel.Position = new Point(0xcd, 0x29);
            this.factionMottoLabel.Size = new Size(600, 40);
            this.factionMottoLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.factionMottoLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.factionMottoLabel);
            if (data.houseID > 0)
            {
                this.houseLabel.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + data.houseID.ToString();
                this.houseLabel.Color = ARGBColors.Black;
                this.houseLabel.Position = new Point(0x23f, 110);
                this.houseLabel.Size = new Size(200, 50);
                this.houseLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.houseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.houseLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "FactionMyFactionPanel_house");
                this.mainBackgroundImage.addControl(this.houseLabel);
                this.houseImage.Image = (Image) GFXLibrary.house_circles_large[data.houseID - 1];
                this.houseImage.Position = new Point(0x2a3 - (this.houseImage.Image.Width / 2), (0x41 - (this.houseImage.Image.Height / 2)) + 8);
                this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "FactionMyFactionPanel_house");
                this.mainBackgroundImage.addControl(this.houseImage);
            }
            this.membersLabel.Text = SK.Text("FactionInvites_Members", "Members");
            this.membersLabel.Color = ARGBColors.Black;
            this.membersLabel.Position = new Point(0x11c, 0x49);
            this.membersLabel.Size = new Size(600, 40);
            this.membersLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.membersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.membersLabel);
            this.membersLabelValue.Text = data.numMembers.ToString();
            this.membersLabelValue.Color = ARGBColors.Black;
            this.membersLabelValue.Position = new Point(30, 0x49);
            this.membersLabelValue.Size = new Size(0x1e2, 40);
            this.membersLabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.membersLabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.membersLabelValue);
            this.rankHeaderLabel.Text = SK.Text("STATS_CATEGORY_TITLE_RANK", "Rank");
            this.rankHeaderLabel.Color = ARGBColors.Black;
            this.rankHeaderLabel.Position = new Point(0x11c, 0x79);
            this.rankHeaderLabel.Size = new Size(600, 40);
            this.rankHeaderLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.rankHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.rankHeaderLabel);
            this.rankHeaderLabelValue.Text = (GameEngine.Instance.World.getFactionRank(m_selectedFaction) + 1).ToString("N", nFI);
            this.rankHeaderLabelValue.Color = ARGBColors.Black;
            this.rankHeaderLabelValue.Position = new Point(30, 0x79);
            this.rankHeaderLabelValue.Size = new Size(0x1e2, 40);
            this.rankHeaderLabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.rankHeaderLabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.rankHeaderLabelValue);
            this.pointsHeaderLabel.Text = SK.Text("FactionsPanel_Points", "Points");
            this.pointsHeaderLabel.Color = ARGBColors.Black;
            this.pointsHeaderLabel.Position = new Point(0x11c, 0x61);
            this.pointsHeaderLabel.Size = new Size(600, 40);
            this.pointsHeaderLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.pointsHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.pointsHeaderLabel);
            this.pointsHeaderLabelValue.Text = data.points.ToString("N", nFI);
            this.pointsHeaderLabelValue.Color = ARGBColors.Black;
            this.pointsHeaderLabelValue.Position = new Point(30, 0x61);
            this.pointsHeaderLabelValue.Size = new Size(0x1e2, 40);
            this.pointsHeaderLabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.pointsHeaderLabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.pointsHeaderLabelValue);
            this.headerLabelsImage.Size = new Size(((base.Width - 0x19) - 0x17) - 200, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 0x9f);
            this.mainBackgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider1Image.Position = new Point(290, 0);
            this.headerLabelsImage.addControl(this.divider1Image);
            this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider2Image.Position = new Point(440, 0);
            this.headerLabelsImage.addControl(this.divider2Image);
            this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider3Image.Position = new Point(610, 0);
            this.headerLabelsImage.addControl(this.divider3Image);
            this.playerNameLabel.Text = SK.Text("UserInfoPanel_", "Player Name");
            this.playerNameLabel.Color = ARGBColors.Black;
            this.playerNameLabel.Position = new Point(9, -2);
            this.playerNameLabel.Size = new Size(0x143, this.headerLabelsImage.Height);
            this.playerNameLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.playerNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.playerNameLabel);
            this.pointsLabel.Text = SK.Text("FactionsPanel_Points", "Points");
            this.pointsLabel.Color = ARGBColors.Black;
            this.pointsLabel.Position = new Point(0x127, -2);
            this.pointsLabel.Size = new Size(140, this.headerLabelsImage.Height);
            this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.headerLabelsImage.addControl(this.pointsLabel);
            this.rankLabel.Text = SK.Text("STATS_CATEGORY_TITLE_RANK", "Rank");
            this.rankLabel.Color = ARGBColors.Black;
            this.rankLabel.Position = new Point(0x1bd, -2);
            this.rankLabel.Size = new Size(0xdf, this.headerLabelsImage.Height);
            this.rankLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.rankLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.rankLabel);
            this.villagesLabel.Text = SK.Text("UserInfoPanel_Villages", "Villages");
            this.villagesLabel.Color = ARGBColors.Black;
            this.villagesLabel.Position = new Point(0x267, -2);
            this.villagesLabel.Size = new Size(110, this.headerLabelsImage.Height);
            this.villagesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.villagesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.headerLabelsImage.addControl(this.villagesLabel);
            this.flagimage.createFromFlagData(data.flagData);
            this.flagimage.Position = new Point(0x23, 6);
            this.flagimage.Scale = 0.5;
            this.mainBackgroundImage.addControl(this.flagimage);
            if (data.factionID == RemoteServices.Instance.UserFactionID)
            {
                InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionInvites_My_Faction_Details", "My Faction Details"));
            }
            else
            {
                InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionInvites_Faction_Details", "Faction Details"));
            }
            if ((RemoteServices.Instance.UserFactionID < 0) && GameEngine.Instance.World.alreadyGotFactionApplication(data.factionID))
            {
                this.diplomacyLabel.Text = SK.Text("FactionInvites_Application Pending", "Application Pending");
                this.diplomacyLabel.Color = ARGBColors.Black;
                this.diplomacyLabel.Position = new Point(0x18, 0x7e);
                this.diplomacyLabel.Size = new Size(240, 40);
                this.diplomacyLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.diplomacyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.mainBackgroundImage.addControl(this.diplomacyLabel);
            }
            else if (((RemoteServices.Instance.UserFactionID < 0) && data.openForApplications) && !GameEngine.Instance.World.alreadyGotFactionApplication(data.factionID))
            {
                if (GameEngine.Instance.World.getRank() >= 6)
                {
                    this.diplomacyButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                    this.diplomacyButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                    this.diplomacyButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                    this.diplomacyButton.Position = new Point(0x18, 0x7e);
                    this.diplomacyButton.Text.Text = SK.Text("FactionInvites_Apply", "Apply To Join");
                    this.diplomacyButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.diplomacyButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    this.diplomacyButton.TextYOffset = -3;
                    this.diplomacyButton.Text.Color = ARGBColors.Black;
                    this.diplomacyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.applyClicked), "FactionMyFactionPanel_diplomacy");
                    this.mainBackgroundImage.addControl(this.diplomacyButton);
                }
            }
            else if (data.factionID != RemoteServices.Instance.UserFactionID)
            {
                if (GameEngine.Instance.World.getYourFactionRank() == 1)
                {
                    this.diplomacyButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                    this.diplomacyButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                    this.diplomacyButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                    this.diplomacyButton.Position = new Point(0x18, 0x7e);
                    this.diplomacyButton.Text.Text = "";
                    this.diplomacyButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.diplomacyButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    this.diplomacyButton.TextYOffset = -3;
                    this.diplomacyButton.Text.Color = ARGBColors.Black;
                    this.diplomacyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.diplomacyClicked), "FactionMyFactionPanel_diplomacy");
                    this.mainBackgroundImage.addControl(this.diplomacyButton);
                }
                else
                {
                    this.diplomacyLabel.Text = "";
                    this.diplomacyLabel.Color = ARGBColors.Black;
                    this.diplomacyLabel.Position = new Point(0x18, 0x7e);
                    this.diplomacyLabel.Size = new Size(240, 40);
                    this.diplomacyLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                    this.diplomacyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                    this.mainBackgroundImage.addControl(this.diplomacyLabel);
                }
            }
            this.wallScrollArea.Position = new Point(0x19, 0xbc);
            this.wallScrollArea.Size = new Size(0x2c1, (height - 0x26) - 150);
            this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x2c1, (height - 0x26) - 150));
            this.mainBackgroundImage.addControl(this.wallScrollArea);
            this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
            this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
            int num1 = this.wallScrollBar.Value;
            this.wallScrollBar.Position = new Point(0x2dd, 0xbc);
            this.wallScrollBar.Size = new Size(0x18, (height - 0x26) - 150);
            this.mainBackgroundImage.addControl(this.wallScrollBar);
            this.wallScrollBar.Value = 0;
            this.wallScrollBar.Max = 100;
            this.wallScrollBar.NumVisibleLines = 0x19;
            this.wallScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            bool uptodate = false;
            FactionMemberData[] fmd = GameEngine.Instance.World.getFactionMemberData(m_selectedFaction, ref uptodate);
            if (!resized)
            {
                if ((GameEngine.Instance.LocalWorldData.AIWorld && (data.factionID >= 1)) && (data.factionID <= 4))
                {
                    uptodate = true;
                    fmd = new FactionMemberData[] { new FactionMemberData() };
                    switch (data.factionID)
                    {
                        case 1:
                            fmd[0].userID = 1;
                            fmd[0].userName = "The Rat";
                            fmd[0].status = 1;
                            fmd[0].numVillages = GameEngine.Instance.World.countRatsCastles();
                            break;

                        case 2:
                            fmd[0].userID = 2;
                            fmd[0].userName = "The Snake";
                            fmd[0].status = 1;
                            fmd[0].numVillages = GameEngine.Instance.World.countSnakesCastles();
                            break;

                        case 3:
                            fmd[0].userID = 3;
                            fmd[0].userName = "The Pig";
                            fmd[0].status = 1;
                            fmd[0].numVillages = GameEngine.Instance.World.countPigsCastles();
                            break;

                        case 4:
                            fmd[0].userID = 4;
                            fmd[0].userName = "The Wolf";
                            fmd[0].status = 1;
                            fmd[0].numVillages = GameEngine.Instance.World.countWolfsCastles();
                            break;
                    }
                }
                if (!uptodate)
                {
                    RemoteServices.Instance.set_GetViewFactionData_UserCallBack(new RemoteServices.GetViewFactionData_UserCallBack(this.getViewFactionDataCallback));
                    RemoteServices.Instance.GetViewFactionData(m_selectedFaction);
                }
                this.diplomacyOverlayVisible = false;
            }
            this.addPlayers(fmd);
            if (resized && this.diplomacyOverlayVisible)
            {
                this.addDiplomacyOverlay();
            }
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "FactionMyFactionPanel";
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

        public void logout()
        {
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

        public void removeOverlay()
        {
            this.mainBackgroundImage.removeControl(this.greyOverlay);
            this.greyOverlay.clearControls();
            base.Invalidate();
            this.diplomacyOverlayVisible = false;
        }

        public void update()
        {
            this.sidebar.update();
        }

        public void updateRelationshipText()
        {
            if ((RemoteServices.Instance.UserFactionID < 0) || (m_selectedFaction == RemoteServices.Instance.UserFactionID))
            {
                FactionData data = GameEngine.Instance.World.getFaction(m_selectedFaction);
                if (((RemoteServices.Instance.UserFactionID < 0) && (data != null)) && GameEngine.Instance.World.alreadyGotFactionApplication(data.factionID))
                {
                    this.diplomacyButton.Visible = false;
                    this.diplomacyLabel.Visible = true;
                }
                else if (((RemoteServices.Instance.UserFactionID < 0) && (data != null)) && (data.openForApplications && !GameEngine.Instance.World.alreadyGotFactionApplication(data.factionID)))
                {
                    if (GameEngine.Instance.World.getRank() >= 6)
                    {
                        this.diplomacyButton.Visible = true;
                    }
                    else
                    {
                        this.diplomacyButton.Visible = false;
                    }
                    this.diplomacyLabel.Visible = false;
                }
                else
                {
                    this.diplomacyButton.Visible = false;
                    this.diplomacyLabel.Visible = false;
                }
            }
            else
            {
                string str = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy") + " : ";
                this.diplomacyButton.Visible = true;
                this.diplomacyLabel.Visible = true;
                int num = 0;
                if (m_selectedFaction >= 0)
                {
                    num = GameEngine.Instance.World.getYourFactionRelation(m_selectedFaction);
                }
                if (num == 0)
                {
                    str = str + SK.Text("GENERIC_Neutral", "Neutral");
                }
                else if (num > 0)
                {
                    str = str + SK.Text("GENERIC_Ally", "Ally");
                }
                else if (num < 0)
                {
                    str = str + SK.Text("GENERIC_Enemy", "Enemy");
                }
                this.diplomacyLabel.Text = str;
                this.diplomacyButton.Text.Text = str;
            }
        }

        private void wallScrollBarMoved()
        {
            int y = this.wallScrollBar.Value;
            this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 0xbc - y);
            this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
        }

        public static int SelectedFaction
        {
            get
            {
                return m_selectedFaction;
            }
            set
            {
                m_selectedFaction = value;
            }
        }

        public class FactionMemberLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDButton declineButton = new CustomSelfDrawPanel.CSDButton();
            private FactionMemberData m_factionMemberData;
            private FactionMyFactionPanel m_parent;
            private int m_position = -1000;
            private CustomSelfDrawPanel.CSDImage officerImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage onlineImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel playerName = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel rankName = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel villageLabel = new CustomSelfDrawPanel.CSDLabel();

            public void clickedLine()
            {
                if (this.m_factionMemberData.userID >= 0)
                {
                    GameEngine.Instance.playInterfaceSound("FactionMyFactionPanel_user_clicked");
                    WorldMap.CachedUserInfo userInfo = new WorldMap.CachedUserInfo {
                        userID = this.m_factionMemberData.userID
                    };
                    InterfaceMgr.Instance.showUserInfoScreen(userInfo);
                }
            }

            public void declineClicked()
            {
                this.declineButton.Enabled = false;
                RemoteServices.Instance.set_FactionWithdrawInvite_UserCallBack(new RemoteServices.FactionWithdrawInvite_UserCallBack(this.factionWithdrawInviteCallback));
                RemoteServices.Instance.FactionWithdrawInvite(this.m_factionMemberData.userID);
            }

            public void factionWithdrawInviteCallback(FactionWithdrawInvite_ReturnType returnData)
            {
                this.declineButton.Enabled = true;
                if (returnData.members != null)
                {
                    GameEngine.Instance.World.FactionMembers = returnData.members;
                    this.m_parent.init(false);
                }
            }

            public void init(FactionMemberData factionData, int position, FactionMyFactionPanel parent, bool ownFaction)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.m_factionMemberData = factionData;
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_light;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_dark;
                }
                this.backgroundImage.Position = new Point(0, 0);
                base.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                base.addControl(this.backgroundImage);
                this.Size = this.backgroundImage.Size;
                NumberFormatInfo nFI = GameEngine.NFI;
                if ((factionData.status == 1) || (factionData.status == 2))
                {
                    if (factionData.status == 1)
                    {
                        this.officerImage.Image = (Image) GFXLibrary.faction_leaders[1];
                        this.officerImage.CustomTooltipID = 0x901;
                    }
                    else
                    {
                        this.officerImage.Image = (Image) GFXLibrary.faction_leaders[0];
                        this.officerImage.CustomTooltipID = 0x902;
                    }
                    this.officerImage.Position = new Point(9, 2);
                    this.officerImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                    this.backgroundImage.addControl(this.officerImage);
                }
                this.playerName.Text = factionData.userName;
                if (factionData.status != -1)
                {
                    this.playerName.Color = ARGBColors.Black;
                }
                else
                {
                    this.playerName.Color = ARGBColors.DarkRed;
                }
                this.playerName.Position = new Point(0x45, 0);
                this.playerName.Size = new Size(250, this.backgroundImage.Height);
                this.playerName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.playerName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.playerName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                this.backgroundImage.addControl(this.playerName);
                if (factionData.status != -1)
                {
                    this.pointsLabel.Text = factionData.totalPoints.ToString("N", nFI);
                    this.pointsLabel.Color = ARGBColors.Black;
                    this.pointsLabel.Position = new Point(300, 0);
                    this.pointsLabel.Size = new Size(0x55, this.backgroundImage.Height);
                    this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                    this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    this.pointsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                    this.backgroundImage.addControl(this.pointsLabel);
                    this.rankName.Text = Rankings.getRankingName(factionData.rank, factionData.male);
                    this.rankName.Color = ARGBColors.Black;
                    this.rankName.Position = new Point(450, 0);
                    this.rankName.Size = new Size(150, this.backgroundImage.Height);
                    this.rankName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                    this.rankName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.rankName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                    this.backgroundImage.addControl(this.rankName);
                    this.villageLabel.Text = factionData.numVillages.ToString("N", nFI);
                    this.villageLabel.Color = ARGBColors.Black;
                    this.villageLabel.Position = new Point(620, 0);
                    this.villageLabel.Size = new Size(0x37, this.backgroundImage.Height);
                    this.villageLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                    this.villageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    this.villageLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                    this.backgroundImage.addControl(this.villageLabel);
                    if (factionData.online)
                    {
                        this.onlineImage.Image = (Image) GFXLibrary.radio_green[0];
                        this.onlineImage.Position = new Point(280, 5);
                        this.backgroundImage.addControl(this.onlineImage);
                    }
                }
                else
                {
                    this.pointsLabel.Text = SK.Text("FactionsInvites_Invite_Pending", "Invitation Pending");
                    this.pointsLabel.Color = ARGBColors.DarkRed;
                    this.pointsLabel.Position = new Point(300, 0);
                    this.pointsLabel.Size = new Size(500, this.backgroundImage.Height);
                    this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                    this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.pointsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                    this.backgroundImage.addControl(this.pointsLabel);
                    switch (GameEngine.Instance.World.getYourFactionRank())
                    {
                        case 1:
                        case 2:
                            this.declineButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                            this.declineButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                            this.declineButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                            this.declineButton.Position = new Point(560, 0);
                            this.declineButton.Text.Text = SK.Text("FactionMemberLine_Cancel_Invite", "Cancel Invite");
                            this.declineButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                            this.declineButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                            this.declineButton.TextYOffset = -3;
                            this.declineButton.Text.Color = ARGBColors.Black;
                            this.declineButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.declineClicked), "FactionMyFactionPanel_declined_clicked");
                            this.backgroundImage.addControl(this.declineButton);
                            break;
                    }
                }
                this.shieldImage.Image = GameEngine.Instance.World.getWorldShield(factionData.userID, 0x19, 0x1c);
                if (this.shieldImage.Image != null)
                {
                    this.shieldImage.Position = new Point(0x27, 1);
                    this.shieldImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                    this.shieldImage.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                    this.backgroundImage.addControl(this.shieldImage);
                }
                base.invalidate();
            }

            public void update()
            {
            }
        }
    }
}

