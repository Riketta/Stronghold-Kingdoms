namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class NewSelectVillageAreaPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDExtendingPanel background = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDArea backgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton btnBack = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnEnterGame = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnLogout = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private DateTime delayedRetry = DateTime.MinValue;
        private float divider = 5f;
        private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage highImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel highLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel loadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lostMessageLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage lowImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel lowLabel = new CustomSelfDrawPanel.CSDLabel();
        private NewSelectVillageAreaWindow m_parent;
        private JoiningWorldPopup m_popup;
        private CustomSelfDrawPanel.CSDRectangle mapBorder = new CustomSelfDrawPanel.CSDRectangle();
        private CustomSelfDrawPanel.CSDImage mapImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage medImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel medLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel populationLabel = new CustomSelfDrawPanel.CSDLabel();
        private int retries;
        private int selectedCounty = -1;
        private CustomSelfDrawPanel.CSDFill transparentBackground = new CustomSelfDrawPanel.CSDFill();

        public NewSelectVillageAreaPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void btnBack_Click()
        {
            GameEngine.Instance.playInterfaceSound("SelectVillageAreaPopup_back");
            this.m_parent.closing = true;
            this.closePopup();
            GameEngine.Instance.closeNoVillagePopup(false);
            GameEngine.Instance.openSimpleSelectVillage();
        }

        private void btnEnterGame_Click()
        {
            if (this.selectedCounty >= 0)
            {
                GameEngine.Instance.playInterfaceSound("SelectVillageAreaPopup_enter_game");
                this.closePopup();
                this.m_popup = new JoiningWorldPopup();
                this.m_popup.init(this.selectedCounty, "");
                this.m_popup.Show(this);
                this.btnEnterGame.Enabled = false;
                this.retries = 0;
                RemoteServices.Instance.set_SetStartingCounty_UserCallBack(new RemoteServices.SetStartingCounty_UserCallBack(this.SetStartingCountyCallback));
                RemoteServices.Instance.SetStartingCounty(this.selectedCounty);
            }
        }

        public void closePopup()
        {
            if (this.m_popup != null)
            {
                this.m_popup.Close();
                this.m_popup = null;
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

        public void GetVillageStartLocationsCallback(GetVillageStartLocations_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.loadingLabel.Visible = false;
                this.importCounties(returnData.availableCounties);
            }
        }

        private void iconOver_Click()
        {
            GameEngine.Instance.playInterfaceSound("SelectVillageAreaPopup_select_county");
            if (this.m_popup == null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                if (clickedControl != null)
                {
                    foreach (CustomSelfDrawPanel.CSDControl control in this.mapImage.Controls)
                    {
                        if (control.GetType() == typeof(CustomSelfDrawPanel.CSDButton))
                        {
                            CustomSelfDrawPanel.CSDButton button2 = (CustomSelfDrawPanel.CSDButton) control;
                            if (button2.ImageClick == (Image) GFXLibrary.selector_square_pressed)
                            {
                                button2.ImageNorm = (Image) GFXLibrary.selector_square_normal;
                                button2.ImageOver = (Image) GFXLibrary.selector_square_over;
                            }
                            else if (button2.ImageClick == (Image) GFXLibrary.selector_square_orange_pressed)
                            {
                                button2.ImageNorm = (Image) GFXLibrary.selector_square_orange_normal;
                                button2.ImageOver = (Image) GFXLibrary.selector_square_orange_over;
                            }
                            else if (button2.ImageClick == (Image) GFXLibrary.selector_square_red_pressed)
                            {
                                button2.ImageNorm = (Image) GFXLibrary.selector_square_red_normal;
                                button2.ImageOver = (Image) GFXLibrary.selector_square_red_over;
                            }
                        }
                    }
                    if (clickedControl.ImageClick == (Image) GFXLibrary.selector_square_pressed)
                    {
                        clickedControl.ImageNorm = (Image) GFXLibrary.selector_square_pressed;
                        clickedControl.ImageOver = (Image) GFXLibrary.selector_square_pressed;
                    }
                    else if (clickedControl.ImageClick == (Image) GFXLibrary.selector_square_orange_pressed)
                    {
                        clickedControl.ImageNorm = (Image) GFXLibrary.selector_square_orange_pressed;
                        clickedControl.ImageOver = (Image) GFXLibrary.selector_square_orange_pressed;
                    }
                    else if (clickedControl.ImageClick == (Image) GFXLibrary.selector_square_red_pressed)
                    {
                        clickedControl.ImageNorm = (Image) GFXLibrary.selector_square_red_pressed;
                        clickedControl.ImageOver = (Image) GFXLibrary.selector_square_red_pressed;
                    }
                    this.selectedCounty = clickedControl.Data;
                    this.btnEnterGame.Enabled = true;
                    this.loadingLabel.Text = GameEngine.Instance.World.getCountyName(this.selectedCounty);
                    this.loadingLabel.Visible = true;
                    this.mapImage.invalidate();
                }
            }
        }

        private void importCounties(List<int> counties)
        {
            this.selectedCounty = -1;
            if (counties != null)
            {
                this.mapImage.clearControls();
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < counties.Count; j += 4)
                    {
                        int countyID = counties[j];
                        int num4 = counties[j + 1];
                        int num5 = counties[j + 2];
                        int local1 = counties[j + 3];
                        Point point = GameEngine.Instance.World.getCountyMarkerLocation(countyID);
                        if (num4 == -1000)
                        {
                            if (i == 0)
                            {
                                CustomSelfDrawPanel.CSDImage image;
                                image = new CustomSelfDrawPanel.CSDImage {
                                    Image = (Image) GFXLibrary.selector_square_red_normal,
                                    Size = new Size(((Image)GFXLibrary.selector_square_red_normal).Size.Width / 2, ((Image)GFXLibrary.selector_square_red_normal).Size.Height / 2),
                                    CustomTooltipID = 0x44d,
                                    Position = new Point(((int) (((float) point.X) / this.divider)) - 4, ((int) (((float) point.Y) / this.divider)) - 4),
                                    Colorise = Color.FromArgb(0xff, 0x80, 0x80, 0x80)
                                };
                                this.mapImage.addControl(image);
                            }
                        }
                        else if (i == 1)
                        {
                            CustomSelfDrawPanel.CSDButton control = new CustomSelfDrawPanel.CSDButton {
                                Position = new Point(((int) (((float) point.X) / this.divider)) - 8, ((int) (((float) point.Y) / this.divider)) - 8)
                            };
                            control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconOver_Click));
                            control.Data = countyID;
                            control.CustomTooltipID = 0x44c;
                            this.mapImage.addControl(control);
                            if (num4 > (num5 / 2))
                            {
                                control.ImageNorm = (Image) GFXLibrary.selector_square_normal;
                                control.ImageOver = (Image) GFXLibrary.selector_square_over;
                                control.ImageClick = (Image) GFXLibrary.selector_square_pressed;
                            }
                            else if (num4 > (num5 / 7))
                            {
                                control.ImageNorm = (Image) GFXLibrary.selector_square_orange_normal;
                                control.ImageOver = (Image) GFXLibrary.selector_square_orange_over;
                                control.ImageClick = (Image) GFXLibrary.selector_square_orange_pressed;
                            }
                            else
                            {
                                control.ImageNorm = (Image) GFXLibrary.selector_square_red_normal;
                                control.ImageOver = (Image) GFXLibrary.selector_square_red_over;
                                control.ImageClick = (Image) GFXLibrary.selector_square_red_pressed;
                            }
                        }
                    }
                }
                this.mapImage.invalidate();
            }
        }

        public void init(int tryingToJoinCounty, NewSelectVillageAreaWindow parent)
        {
            this.m_parent = parent;
            base.clearControls();
            this.transparentBackground.Size = base.Size;
            this.transparentBackground.FillColor = Color.FromArgb(0xff, 0, 0xff);
            base.addControl(this.transparentBackground);
            this.background.Position = new Point(0, 0);
            this.background.Size = new Size(base.Width, base.Height);
            base.addControl(this.background);
            this.background.Create((Image) GFXLibrary._9sclice_fancy_top_left, (Image) GFXLibrary._9sclice_fancy_top_mid, (Image) GFXLibrary._9sclice_fancy_top_right, (Image) GFXLibrary._9sclice_fancy_mid_left, (Image) GFXLibrary._9sclice_fancy_mid_mid, (Image) GFXLibrary._9sclice_fancy_mid_right, (Image) GFXLibrary._9sclice_fancy_bottom_left, (Image) GFXLibrary._9sclice_fancy_bottom_mid, (Image) GFXLibrary._9sclice_fancy_bottom_right);
            this.background.ForceTiling();
            this.backgroundArea.Position = new Point(0xce, 0x35);
            this.backgroundArea.Size = new Size(0x202, 340);
            base.addControl(this.backgroundArea);
            int y = 0;
            this.divider = 5f;
            switch (GameEngine.Instance.World.WorldMapType)
            {
                case 1:
                    this.mapImage.Image = (Image) GFXLibrary.world_select_map_de;
                    break;

                case 3:
                    this.mapImage.Image = (Image) GFXLibrary.world_select_map_fr;
                    break;

                case 4:
                    this.mapImage.Image = (Image) GFXLibrary.world_select_map_ru;
                    break;

                case 5:
                    this.mapImage.Image = (Image) GFXLibrary.world_select_map_sa;
                    this.divider = 6f;
                    y = 10;
                    break;

                case 6:
                    this.mapImage.Image = (Image) GFXLibrary.world_select_map_es;
                    this.divider = 5.5f;
                    y = 0x68;
                    break;

                case 7:
                    this.mapImage.Image = (Image) GFXLibrary.world_select_map_pl;
                    y = 50;
                    break;

                case 8:
                    this.mapImage.Image = (Image) GFXLibrary.world_select_map_eu;
                    y = 0x42;
                    this.divider = 5.5f;
                    break;

                case 9:
                    this.mapImage.Image = (Image) GFXLibrary.world_select_map_tr;
                    this.divider = 5.5f;
                    y = 190;
                    break;

                case 10:
                    this.mapImage.Image = (Image) GFXLibrary.world_select_map_us;
                    this.divider = 5.5f;
                    y = 130;
                    break;

                case 11:
                    this.mapImage.Image = (Image) GFXLibrary.world_select_map_it;
                    y = 0x55;
                    break;

                default:
                    this.mapImage.Image = (Image) GFXLibrary.world_select_map_en;
                    break;
            }
            this.mapImage.Position = new Point(0, y);
            this.backgroundArea.addControl(this.mapImage);
            this.mapBorder.Position = new Point(0, y);
            this.mapBorder.Size = new Size(this.mapImage.Width, this.mapImage.Height);
            this.mapBorder.LineColor = ARGBColors.Black;
            this.backgroundArea.addControl(this.mapBorder);
            this.btnEnterGame.ImageNorm = (Image) GFXLibrary.worldSelect_swap_norm;
            this.btnEnterGame.ImageOver = (Image) GFXLibrary.worldSelect_swap_over;
            this.btnEnterGame.ImageClick = (Image) GFXLibrary.worldSelect_swap_pushed;
            this.btnEnterGame.Position = new Point(0x235, 60);
            this.btnEnterGame.Text.Text = SK.Text("SelectVillageAreaPopup_Enter_Game", "Enter Game");
            this.btnEnterGame.TextYOffset = -2;
            this.btnEnterGame.Text.Color = ARGBColors.White;
            this.btnEnterGame.Text.DropShadowColor = ARGBColors.Black;
            this.btnEnterGame.Text.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
            this.btnEnterGame.Text.Position = new Point(-3, 0);
            this.btnEnterGame.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnEnterGame_Click));
            this.btnEnterGame.Enabled = false;
            this.backgroundArea.addControl(this.btnEnterGame);
            this.btnBack.ImageNorm = (Image) GFXLibrary.worldSelect_swap_norm;
            this.btnBack.ImageOver = (Image) GFXLibrary.worldSelect_swap_over;
            this.btnBack.ImageClick = (Image) GFXLibrary.worldSelect_swap_pushed;
            this.btnBack.Position = new Point(0x235, 540);
            this.btnBack.Text.Text = SK.Text("FORUMS_Back", "Back");
            this.btnBack.TextYOffset = -2;
            this.btnBack.Text.Color = ARGBColors.White;
            this.btnBack.Text.DropShadowColor = ARGBColors.Black;
            this.btnBack.Text.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
            this.btnBack.Text.Position = new Point(-3, 0);
            this.btnBack.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnBack_Click));
            this.btnBack.Enabled = true;
            this.backgroundArea.addControl(this.btnBack);
            this.btnLogout.ImageNorm = (Image) GFXLibrary.worldSelect_swap_norm;
            this.btnLogout.ImageOver = (Image) GFXLibrary.worldSelect_swap_over;
            this.btnLogout.ImageClick = (Image) GFXLibrary.worldSelect_swap_pushed;
            this.btnLogout.Position = new Point(0x235, 500);
            this.btnLogout.Text.Text = SK.Text("LogoutPanel_Swap_Worlds", "Swap Worlds");
            this.btnLogout.TextYOffset = -2;
            this.btnLogout.Text.Color = ARGBColors.White;
            this.btnLogout.Text.DropShadowColor = ARGBColors.Black;
            this.btnLogout.Text.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
            this.btnLogout.Text.Position = new Point(-3, 0);
            this.btnLogout.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.logoutClick));
            this.btnLogout.Enabled = true;
            this.backgroundArea.addControl(this.btnLogout);
            this.headerLabel.Text = SK.Text("SelectVillageAreaPopup_Select_Village_Location", "Select Village Location");
            this.headerLabel.Position = new Point(0, 1);
            this.headerLabel.Size = new Size(this.background.Width, 150);
            this.headerLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.headerLabel.Color = ARGBColors.Black;
            this.headerLabel.DropShadowColor = ARGBColors.LightGray;
            this.background.addControl(this.headerLabel);
            this.loadingLabel.Text = SK.Text("SelectVillageAreaPopup_Downloading", "Downloading") + " .....";
            this.loadingLabel.Position = new Point((this.btnEnterGame.Position.X + (this.btnEnterGame.Width / 2)) - 100, this.btnEnterGame.Position.Y + 50);
            this.loadingLabel.Size = new Size(200, 200);
            this.loadingLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.loadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.loadingLabel.Color = ARGBColors.Black;
            this.loadingLabel.DropShadowColor = ARGBColors.LightGray;
            this.backgroundArea.addControl(this.loadingLabel);
            this.populationLabel.Text = SK.Text("SelectVillagePopup_Population", "Population");
            this.populationLabel.Position = new Point(0x23e, 0xf5);
            this.populationLabel.Size = new Size(150, 30);
            this.populationLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.populationLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.populationLabel.Color = ARGBColors.Black;
            this.populationLabel.DropShadowColor = ARGBColors.LightGray;
            this.backgroundArea.addControl(this.populationLabel);
            this.lowImage.Image = (Image) GFXLibrary.selector_square_normal;
            this.lowImage.Position = new Point(0x23e, 270);
            this.backgroundArea.addControl(this.lowImage);
            this.lowLabel.Text = SK.Text("SelectVillagePopup_Low", "Low");
            this.lowLabel.Position = new Point(0x252, 270);
            this.lowLabel.Size = new Size(150, 30);
            this.lowLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
            this.lowLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.lowLabel.Color = ARGBColors.Black;
            this.lowLabel.DropShadowColor = ARGBColors.LightGray;
            this.backgroundArea.addControl(this.lowLabel);
            this.medImage.Image = (Image) GFXLibrary.selector_square_orange_normal;
            this.medImage.Position = new Point(0x23e, 0x127);
            this.backgroundArea.addControl(this.medImage);
            this.medLabel.Text = SK.Text("SelectVillagePopup_Medium", "Medium");
            this.medLabel.Position = new Point(0x252, 0x127);
            this.medLabel.Size = new Size(150, 30);
            this.medLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
            this.medLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.medLabel.Color = ARGBColors.Black;
            this.medLabel.DropShadowColor = ARGBColors.LightGray;
            this.backgroundArea.addControl(this.medLabel);
            this.highImage.Image = (Image) GFXLibrary.selector_square_red_normal;
            this.highImage.Position = new Point(0x23e, 320);
            this.backgroundArea.addControl(this.highImage);
            this.highLabel.Text = SK.Text("SelectVillagePopup_High", "High");
            this.highLabel.Position = new Point(0x252, 320);
            this.highLabel.Size = new Size(150, 30);
            this.highLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
            this.highLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.highLabel.Color = ARGBColors.Black;
            this.highLabel.DropShadowColor = ARGBColors.LightGray;
            this.backgroundArea.addControl(this.highLabel);
            RemoteServices.Instance.set_GetVillageStartLocations_UserCallBack(new RemoteServices.GetVillageStartLocations_UserCallBack(this.GetVillageStartLocationsCallback));
            RemoteServices.Instance.GetVillageStartLocations();
            if (tryingToJoinCounty >= 0)
            {
                this.closePopup();
                this.m_popup = new JoiningWorldPopup();
                this.m_popup.init(tryingToJoinCounty, "");
                this.m_popup.Show(this);
                this.btnEnterGame.Enabled = false;
                this.delayedRetry = DateTime.Now.AddSeconds(-25.0);
                GameEngine.Instance.tryingToJoinCounty = -2;
            }
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.Name = "NewSelectVillageAreaPanel";
            base.Size = new Size(600, 0x37);
            base.ResumeLayout(false);
        }

        private void logoutClick()
        {
            GameEngine.Instance.playInterfaceSound("SelectVillageAreaPopup_logout");
            this.m_parent.closing = true;
            GameEngine.Instance.closeNoVillagePopup(false);
            LoggingOutPopup.open(true, false, false, false, false, false, false, 0, 100, false, false, false, false, false, false, 500, 500, 500, 500, 250);
        }

        private void SetStartingCountyCallback(SetStartingCounty_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.availableCounties == null)
                {
                    if (returnData.villageID >= 0)
                    {
                        GameEngine.Instance.World.setVillageName(returnData.villageID, returnData.villageName);
                        GameEngine.Instance.World.addUserVillage(returnData.villageID);
                        GameEngine.Instance.World.updateWorldMapOwnership();
                        this.m_parent.closing = true;
                        GameEngine.Instance.closeNoVillagePopup(true);
                        GameEngine.Instance.World.setResearchData(returnData.m_researchData);
                        InterfaceMgr.Instance.selectUserVillage(returnData.villageID, false);
                    }
                    else
                    {
                        this.delayedRetry = DateTime.Now;
                    }
                }
                else
                {
                    bool flag = false;
                    for (int i = 0; i < returnData.availableCounties.Count; i += 4)
                    {
                        if (returnData.availableCounties[i] == this.selectedCounty)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        this.retries++;
                        if (this.retries < 2)
                        {
                            Thread.Sleep(0x7d0);
                            RemoteServices.Instance.set_SetStartingCounty_UserCallBack(new RemoteServices.SetStartingCounty_UserCallBack(this.SetStartingCountyCallback));
                            RemoteServices.Instance.SetStartingCounty(this.selectedCounty);
                            return;
                        }
                    }
                    this.importCounties(returnData.availableCounties);
                    this.btnEnterGame.Enabled = true;
                    this.closePopup();
                    MyMessageBox.Show(SK.Text("SelectVillageAreaPopup_Village_Placement_Error_Message", "The server failed to find you a village, please try again."), SK.Text("SelectVillageAreaPopup_Village_Placement_Error", "Village Placement Error"));
                }
            }
        }

        public void update()
        {
            if (this.delayedRetry != DateTime.MinValue)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.delayedRetry);
                if (span.TotalSeconds > 30.0)
                {
                    this.delayedRetry = DateTime.MinValue;
                    RemoteServices.Instance.set_SetStartingCounty_UserCallBack(new RemoteServices.SetStartingCounty_UserCallBack(this.SetStartingCountyCallback));
                    RemoteServices.Instance.SetStartingCounty(-1);
                }
            }
        }
    }
}

