namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class NewAutoSelectVillagePanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDImage background = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDArea backgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton btnAdvanced = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnEnterGame = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnLogout = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private DateTime delayedRetry = DateTime.MinValue;
        private CustomSelfDrawPanel.CSDLabel header1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel header2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel header3Label = new CustomSelfDrawPanel.CSDLabel();
        private NewAutoSelectVillageWindow m_parent;
        private JoiningWorldPopup m_popup;
        private int retries;
        private CustomSelfDrawPanel.CSDFill transparentBackground = new CustomSelfDrawPanel.CSDFill();

        public NewAutoSelectVillagePanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void advancedClicked()
        {
            GameEngine.Instance.playInterfaceSound("AutoSelectVillageAreaPopup_manual");
            this.closePopup();
            this.m_parent.closing = true;
            GameEngine.Instance.closeNoVillagePopup(false);
            GameEngine.Instance.openAdvancedSelectVillage();
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

        private void enterGameClicked()
        {
            GameEngine.Instance.playInterfaceSound("AutoSelectVillageAreaPopup_random");
            this.closePopup();
            this.m_popup = new JoiningWorldPopup();
            this.m_popup.init(-1, "");
            this.m_popup.Show(this);
            this.btnEnterGame.Enabled = false;
            this.btnAdvanced.Enabled = false;
            this.retries = 0;
            RemoteServices.Instance.set_SetStartingCounty_UserCallBack(new RemoteServices.SetStartingCounty_UserCallBack(this.SetStartingCountyCallback));
            RemoteServices.Instance.SetStartingCounty(0x270f);
        }

        public void init(int tryingToJoinCounty, NewAutoSelectVillageWindow parent)
        {
            this.m_parent = parent;
            base.clearControls();
            this.transparentBackground.Size = base.Size;
            this.transparentBackground.FillColor = Color.FromArgb(0xff, 0, 0xff);
            base.addControl(this.transparentBackground);
            this.background.Position = new Point(0, 0);
            this.background.Image = (Image) GFXLibrary.worldSelect_Background;
            this.background.Size = new Size(this.background.Image.Width, this.background.Image.Height);
            base.addControl(this.background);
            this.backgroundArea.Position = new Point(0, 0);
            this.backgroundArea.Size = new Size(0x271, 0x29c);
            this.background.addControl(this.backgroundArea);
            if (((Program.mySettings.LanguageIdent == "en") || (Program.mySettings.LanguageIdent == "fr")) || (Program.mySettings.LanguageIdent == "de"))
            {
                this.header1Label.Text = SK.Text("WorldSelect_Place_Your_Village1", "Place");
                this.header1Label.Position = new Point(0x6c, 170);
                this.header1Label.Size = new Size(this.backgroundArea.Width - 200, 150);
                this.header1Label.Font = FontManager.GetFont("Arial", 24f, FontStyle.Regular);
                this.header1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.header1Label.Color = ARGBColors.Black;
                this.header1Label.DropShadowColor = ARGBColors.LightGray;
                this.backgroundArea.addControl(this.header1Label);
            }
            this.header2Label.Text = SK.Text("WorldSelect_Place_Your_Village2", "Your");
            this.header2Label.Position = new Point(0x6c, 0xd7);
            this.header2Label.Size = new Size(this.backgroundArea.Width - 200, 150);
            this.header2Label.Font = FontManager.GetFont("Arial", 24f, FontStyle.Regular);
            this.header2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.header2Label.Color = ARGBColors.Black;
            this.header2Label.DropShadowColor = ARGBColors.LightGray;
            this.backgroundArea.addControl(this.header2Label);
            this.header3Label.Text = SK.Text("WorldSelect_Place_Your_Village3", "Village");
            this.header3Label.Position = new Point(0x6c, 260);
            this.header3Label.Size = new Size(this.backgroundArea.Width - 200, 150);
            this.header3Label.Font = FontManager.GetFont("Arial", 24f, FontStyle.Regular);
            this.header3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.header3Label.Color = ARGBColors.Black;
            this.header3Label.DropShadowColor = ARGBColors.LightGray;
            this.backgroundArea.addControl(this.header3Label);
            this.btnEnterGame.ImageNorm = (Image) GFXLibrary.worldSelect_random_norm;
            this.btnEnterGame.ImageOver = (Image) GFXLibrary.worldSelect_random_over;
            this.btnEnterGame.ImageClick = (Image) GFXLibrary.worldSelect_random_pushed;
            this.btnEnterGame.Position = new Point(0xc1, 0x13c);
            this.btnEnterGame.Text.Text = SK.Text("WorldSelect_Random", "Random");
            this.btnEnterGame.TextYOffset = -2;
            this.btnEnterGame.Text.Color = ARGBColors.White;
            this.btnEnterGame.Text.DropShadowColor = ARGBColors.Black;
            this.btnEnterGame.Text.Font = FontManager.GetFont("Arial", 20f, FontStyle.Regular);
            this.btnEnterGame.Text.Position = new Point(-3, 0);
            this.btnEnterGame.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.enterGameClicked));
            this.btnEnterGame.Enabled = true;
            this.btnEnterGame.CustomTooltipID = 0x708;
            this.backgroundArea.addControl(this.btnEnterGame);
            this.btnAdvanced.ImageNorm = (Image) GFXLibrary.worldSelect_manual_norm;
            this.btnAdvanced.ImageOver = (Image) GFXLibrary.worldSelect_manual_over;
            this.btnAdvanced.ImageClick = (Image) GFXLibrary.worldSelect_manual_pushed;
            this.btnAdvanced.Position = new Point(0xc1, 0x1a0);
            this.btnAdvanced.Text.Text = SK.Text("WorldSelect_Manual", "Manual");
            this.btnAdvanced.TextYOffset = -2;
            this.btnAdvanced.Text.Color = ARGBColors.White;
            this.btnAdvanced.Text.DropShadowColor = ARGBColors.Black;
            this.btnAdvanced.Text.Font = FontManager.GetFont("Arial", 20f, FontStyle.Regular);
            this.btnAdvanced.Text.Position = new Point(-3, 0);
            this.btnAdvanced.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.advancedClicked));
            this.btnAdvanced.Enabled = true;
            this.btnAdvanced.CustomTooltipID = 0x709;
            this.backgroundArea.addControl(this.btnAdvanced);
            if (GameEngine.Instance.LocalWorldData.AIWorld)
            {
                this.btnAdvanced.Visible = false;
            }
            else
            {
                this.btnAdvanced.Visible = true;
            }
            this.btnLogout.ImageNorm = (Image) GFXLibrary.worldSelect_swap_norm;
            this.btnLogout.ImageOver = (Image) GFXLibrary.worldSelect_swap_over;
            this.btnLogout.ImageClick = (Image) GFXLibrary.worldSelect_swap_pushed;
            this.btnLogout.Position = new Point(0xf5, 0x204);
            this.btnLogout.Text.Text = SK.Text("LogoutPanel_Swap_Worlds", "Swap Worlds");
            this.btnLogout.TextYOffset = -2;
            this.btnLogout.Text.Color = ARGBColors.White;
            this.btnLogout.Text.DropShadowColor = ARGBColors.Black;
            this.btnLogout.Text.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
            this.btnLogout.Text.Position = new Point(-3, 0);
            this.btnLogout.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.logoutClick));
            this.btnLogout.Enabled = true;
            this.btnLogout.CustomTooltipID = 0x70a;
            this.backgroundArea.addControl(this.btnLogout);
            this.delayedRetry = DateTime.MinValue;
            if (tryingToJoinCounty >= -1)
            {
                this.closePopup();
                this.m_popup = new JoiningWorldPopup();
                this.m_popup.init(-1, "");
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
            base.Name = "NewAutoSelectVillagePanel";
            base.Size = new Size(600, 0x37);
            base.ResumeLayout(false);
        }

        private void logoutClick()
        {
            GameEngine.Instance.playInterfaceSound("AutoSelectVillageAreaPopup_logout");
            this.m_parent.closing = true;
            GameEngine.Instance.closeNoVillagePopup(false);
            LoggingOutPopup.open(true, false, false, false, false, false, false, 0, 100, false, false, false, false, false, false, 500, 500, 500, 500, 250);
        }

        private void SetStartingCountyCallback(SetStartingCounty_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.availableCounties != null)
                {
                    this.retries++;
                    if (this.retries >= 2)
                    {
                        this.btnEnterGame.Enabled = true;
                        this.btnAdvanced.Enabled = true;
                        this.closePopup();
                        MyMessageBox.Show(SK.Text("SelectVillageAreaPopup_Village_Placement_Error_Message", "The server failed to find you a village, please try again."), SK.Text("SelectVillageAreaPopup_Village_Placement_Error", "Village Placement Error"));
                    }
                    else
                    {
                        Thread.Sleep(0x7d0);
                        RemoteServices.Instance.set_SetStartingCounty_UserCallBack(new RemoteServices.SetStartingCounty_UserCallBack(this.SetStartingCountyCallback));
                        RemoteServices.Instance.SetStartingCounty(0x270f);
                    }
                }
                else if (returnData.villageID >= 0)
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

