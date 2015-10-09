namespace Kingdoms
{
    using CommonTypes;
    using Dotnetrix_Samples;
    using Kingdoms.Properties;
    using StatTracking;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class OptionsPopup : MyFormBase
    {
        private BitmapButton btnApply;
        private BitmapButton btnCancel;
        private BitmapButton btnDebugInfo;
        private BitmapButton btnDownloadTranslations;
        private BitmapButton btnOK;
        private BitmapButton btnRestoreDefaultVolumes;
        private BitmapButton btnResumeTutorial;
        private CheckBox cbBattleSFX;
        private CheckBox cbCapitalIDs;
        private CheckBox cbConfirmBuyMultiple;
        private CheckBox cbConfirmCards;
        private CheckBox cbConfirmOpenMultiple;
        private CheckBox cbEnvironmentals;
        private CheckBox cbFlashingTaskbarAttack;
        private CheckBox cbGraphicsCompatibility;
        private CheckBox cbInstantTooltips;
        private CheckBox cbMusic;
        private CheckBox cbProductionInfo;
        private CheckBox cbProfanityFilter;
        private CheckBox cbSeasonalFX;
        private CheckBox cbSFX;
        private CheckBox cbTooltips;
        private CheckBox cbVillageIDs;
        private CheckBox cbWhiteTextBox;
        private CheckBox cbWinterLandscape;
        private IContainer components;
        private bool environmentalVolumeChanged;
        private int initialLanguageIndex = -1;
        private Label label1;
        private List<string> languageIDs = new List<string>();
        private Label lblAdvanced;
        private Label lblVolumes;
        private ListBox listBoxLanguages;
        private bool musicVolumeChanged;
        private bool playSounds;
        private Panel pnlWikiHelp;
        private static OptionsPopup popup;
        private static ResolutionChangeCallback resolutionChangeCallback;
        public const int SETTINGS_REPORTS_DISPLAY = 1;
        public const int SETTINGS_TAB_DISPLAY = 0;
        private bool soundfxVolumeChanged;
        private TabControlEx tabOptions;
        private TabPage tabPage1;
        private TabPage tabPage3;
        private TabPage tpageDisplay;
        private TrackBar trackBarEnvironmentals;
        private TrackBar trackBarMusicVolume;
        private TrackBar trackBarSFX;

        public OptionsPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void addLanguage(string name, string id)
        {
            this.listBoxLanguages.Items.Add(name);
            this.languageIDs.Add(id);
            if (((id == Program.mySettings.LanguageIdent) || ((id == "en") && (Program.mySettings.LanguageIdent.Length == 0))) && (this.initialLanguageIndex == -1))
            {
                this.initialLanguageIndex = this.languageIDs.Count - 1;
                this.listBoxLanguages.SelectedIndex = this.initialLanguageIndex;
            }
        }

        private void applySettings()
        {
            bool flag = false;
            if (RemoteServices.Instance.UserOptions.profanityFilter != this.cbProfanityFilter.Checked)
            {
                flag = true;
                RemoteServices.Instance.UserOptions.profanityFilter = this.cbProfanityFilter.Checked;
            }
            if (this.initialLanguageIndex != this.listBoxLanguages.SelectedIndex)
            {
                this.initialLanguageIndex = this.listBoxLanguages.SelectedIndex;
                string str = this.languageIDs[this.initialLanguageIndex];
                Program.mySettings.LanguageIdent = str;
                SKLocalization.LoadLocalization(Application.StartupPath + @"\Localization\", Program.mySettings.LanguageIdent);
                if (!SKLocalization.Instance.valid)
                {
                    MyMessageBox.Show(SK.Text("OptionsPopup_Community_Language_main", "You have selected a language that was created by members of the Stronghold Kingdoms community and is not directly supported by Firefly, therefore we cannot guarantee the accuracy of any translations."), SK.Text("OptionsPopup_CommunityLanguage", "Community Translation"));
                    SKLocalization.LoadLocalization(GameEngine.getLangsPath(), Program.mySettings.LanguageIdent);
                }
                this.initLabels();
                MyMessageBox.Show(SK.Text("OptionsPopup_ChangeLanguage_Restart", "It is recommended that you reload the client after changing the language."), SK.Text("OptionsPopup_ChangeLanguage", "Change Language"));
            }
            if (flag)
            {
                RemoteServices.Instance.UpdateUserOptions(RemoteServices.Instance.UserOptions);
            }
            Program.mySettings.SETTINGS_instantTooltips = this.cbInstantTooltips.Checked;
            Program.mySettings.ConfirmPlayCard = this.cbConfirmCards.Checked;
            Program.mySettings.SETTINGS_showTooltips = this.cbTooltips.Checked;
            Program.mySettings.OpenMultipleCardPacks = this.cbConfirmOpenMultiple.Checked;
            Program.mySettings.BuyMultipleCardPacks = this.cbConfirmBuyMultiple.Checked;
            Program.mySettings.viewVillageIDs = this.cbVillageIDs.Checked;
            Program.mySettings.viewCapitalIDs = this.cbCapitalIDs.Checked;
            Program.mySettings.Music = this.cbMusic.Checked;
            Program.mySettings.MusicVolume = this.trackBarMusicVolume.Value;
            Sound.setMusicState(Program.mySettings.Music);
            Program.mySettings.SFX = this.cbSFX.Checked;
            Program.mySettings.SFXVolume = this.trackBarSFX.Value;
            Sound.setSFXState(Program.mySettings.SFX);
            Program.mySettings.BattleSFX = this.cbBattleSFX.Checked;
            Sound.setBattleSFXState(Program.mySettings.BattleSFX);
            Program.mySettings.Environmentals = this.cbEnvironmentals.Checked;
            Program.mySettings.EnvironmentalVolume = this.trackBarEnvironmentals.Value;
            Sound.setEnvironmentalState(Program.mySettings.Environmentals);
            GameEngine.Instance.AudioEngine.setMP3MasterVolume(((float) Program.mySettings.MusicVolume) / 100f, 0);
            GameEngine.Instance.AudioEngine.setSFXMasterVolume(((float) Program.mySettings.SFXVolume) / 100f);
            GameEngine.Instance.AudioEngine.setEnvironmentalMasterVolume(((float) Program.mySettings.EnvironmentalVolume) / 100f);
            int num = 2;
            if (this.cbGraphicsCompatibility.Checked)
            {
                num = 1;
            }
            if (num != Program.mySettings.AAMode)
            {
                Program.mySettings.AAMode = num;
                MyMessageBox.Show(SK.Text("OptionsPopup_Restart_Required_Main", "You need to restart Stronghold Kingdoms for these changes to take place."), SK.Text("OptionsPopup_Restart_Required", "Restart Required"));
            }
            Program.mySettings.SeasonalSpecialFX = this.cbSeasonalFX.Checked;
            Program.mySettings.SeasonalWinterLandscape = this.cbWinterLandscape.Checked;
            Program.mySettings.FlashingTaskbarAttack = this.cbFlashingTaskbarAttack.Checked;
            Program.mySettings.ShowProductionInfo = this.cbProductionInfo.Checked;
            Program.mySettings.UseMapTextBorders = this.cbWhiteTextBox.Checked;
            Program.mySettings.Save();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("OptionsPopup_apply");
            bool flag = false;
            if (Program.mySettings.SeasonalWinterLandscape != this.cbWinterLandscape.Checked)
            {
                flag = true;
            }
            this.applySettings();
            this.btnApply.Enabled = false;
            if (flag)
            {
                GFXLibrary.Instance.flushSnowGFX();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("OptionsPopup_cancel");
            if (this.musicVolumeChanged)
            {
                GameEngine.Instance.AudioEngine.setMP3MasterVolume(((float) Program.mySettings.MusicVolume) / 100f, 0);
            }
            if (this.soundfxVolumeChanged)
            {
                GameEngine.Instance.AudioEngine.setSFXMasterVolume(((float) Program.mySettings.SFXVolume) / 100f);
            }
            if (this.environmentalVolumeChanged)
            {
                GameEngine.Instance.AudioEngine.setEnvironmentalMasterVolume(((float) Program.mySettings.EnvironmentalVolume) / 100f);
            }
            base.Close();
            InterfaceMgr.Instance.reactiveMainWindow();
        }

        private void btnDebugInfo_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.toggleDebugPopup();
        }

        private void btnDownloadTranslations_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("OptionsPopup_download");
            try
            {
                new Process { StartInfo = { FileName = URLs.CommunityTranslationPage } }.Start();
            }
            catch (Exception)
            {
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("OptionsPopup_ok");
            bool flag = false;
            if (Program.mySettings.SeasonalWinterLandscape != this.cbWinterLandscape.Checked)
            {
                flag = true;
            }
            this.applySettings();
            StatTrackingClient.Instance().ActivateTrigger(0x1d, Program.mySettings.UseMapTextBorders);
            base.Close();
            InterfaceMgr.Instance.reactiveMainWindow();
            if (flag)
            {
                GFXLibrary.Instance.flushSnowGFX();
            }
        }

        private void btnRestoreDefaultVolumes_Click(object sender, EventArgs e)
        {
            this.trackBarMusicVolume.Value = 13;
            this.trackBarSFX.Value = 100;
            this.trackBarEnvironmentals.Value = 0x22;
        }

        private void btnResumeTutorial_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("Options_resume_tutorial");
            GameEngine.Instance.World.resumeTutorial();
            this.btnResumeTutorial.Visible = false;
        }

        private void cbBattleSFX_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbCapitalIDs_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbConfirmBuyMultiple_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbConfirmCards_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbConfirmOpenMultiple_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbEnvironmentals_CheckedChanged(object sender, EventArgs e)
        {
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
            this.btnApply.Enabled = true;
            this.environmentalVolumeChanged = true;
        }

        private void cbFlashingTaskbarAttack_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbGraphicsCompatibility_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbInstantTooltips_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbMusic_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbProductionInfo_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbProfanityFilter_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbReportFilter_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbSeasonalFX_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbSFX_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbTooltips_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbVillageIDs_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbWhiteTextBox_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
            }
        }

        private void cbWinterLandscape_CheckedChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
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

        private void InitializeComponent()
        {
            this.tabOptions = new TabControlEx();
            this.tpageDisplay = new TabPage();
            this.cbCapitalIDs = new CheckBox();
            this.cbProductionInfo = new CheckBox();
            this.cbFlashingTaskbarAttack = new CheckBox();
            this.lblAdvanced = new Label();
            this.cbVillageIDs = new CheckBox();
            this.cbConfirmOpenMultiple = new CheckBox();
            this.cbConfirmBuyMultiple = new CheckBox();
            this.btnResumeTutorial = new BitmapButton();
            this.btnDebugInfo = new BitmapButton();
            this.cbTooltips = new CheckBox();
            this.cbInstantTooltips = new CheckBox();
            this.cbConfirmCards = new CheckBox();
            this.cbProfanityFilter = new CheckBox();
            this.cbWhiteTextBox = new CheckBox();
            this.tabPage3 = new TabPage();
            this.cbWinterLandscape = new CheckBox();
            this.cbSeasonalFX = new CheckBox();
            this.cbBattleSFX = new CheckBox();
            this.btnRestoreDefaultVolumes = new BitmapButton();
            this.lblVolumes = new Label();
            this.trackBarEnvironmentals = new TrackBar();
            this.cbEnvironmentals = new CheckBox();
            this.trackBarSFX = new TrackBar();
            this.cbSFX = new CheckBox();
            this.cbGraphicsCompatibility = new CheckBox();
            this.trackBarMusicVolume = new TrackBar();
            this.cbMusic = new CheckBox();
            this.tabPage1 = new TabPage();
            this.btnDownloadTranslations = new BitmapButton();
            this.label1 = new Label();
            this.listBoxLanguages = new ListBox();
            this.btnApply = new BitmapButton();
            this.btnCancel = new BitmapButton();
            this.btnOK = new BitmapButton();
            this.pnlWikiHelp = new Panel();
            this.tabOptions.SuspendLayout();
            this.tpageDisplay.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.trackBarEnvironmentals.BeginInit();
            this.trackBarSFX.BeginInit();
            this.trackBarMusicVolume.BeginInit();
            this.tabPage1.SuspendLayout();
            base.SuspendLayout();
            this.tabOptions.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tabOptions.BackColor = Color.FromArgb(0x60, 0x6d, 0x76);
            this.tabOptions.Controls.Add(this.tpageDisplay);
            this.tabOptions.Controls.Add(this.tabPage3);
            this.tabOptions.Controls.Add(this.tabPage1);
            this.tabOptions.ItemSize = new Size(110, 0x15);
            this.tabOptions.Location = new Point(9, 0x25);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.SelectedIndex = 0;
            this.tabOptions.Size = new Size(0x14f, 0x135);
            this.tabOptions.SizeMode = TabSizeMode.Fixed;
            this.tabOptions.TabIndex = 0;
            this.tabOptions.SelectedIndexChanged += new EventHandler(this.tabOptions_SelectedIndexChanged);
            this.tpageDisplay.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            this.tpageDisplay.Controls.Add(this.cbCapitalIDs);
            this.tpageDisplay.Controls.Add(this.cbProductionInfo);
            this.tpageDisplay.Controls.Add(this.cbFlashingTaskbarAttack);
            this.tpageDisplay.Controls.Add(this.lblAdvanced);
            this.tpageDisplay.Controls.Add(this.cbVillageIDs);
            this.tpageDisplay.Controls.Add(this.cbConfirmOpenMultiple);
            this.tpageDisplay.Controls.Add(this.cbConfirmBuyMultiple);
            this.tpageDisplay.Controls.Add(this.btnResumeTutorial);
            this.tpageDisplay.Controls.Add(this.btnDebugInfo);
            this.tpageDisplay.Controls.Add(this.cbTooltips);
            this.tpageDisplay.Controls.Add(this.cbInstantTooltips);
            this.tpageDisplay.Controls.Add(this.cbConfirmCards);
            this.tpageDisplay.Controls.Add(this.cbProfanityFilter);
            this.tpageDisplay.Controls.Add(this.cbWhiteTextBox);
            this.tpageDisplay.ForeColor = ARGBColors.Black;
            this.tpageDisplay.Location = new Point(4, 0x19);
            this.tpageDisplay.Name = "tpageDisplay";
            this.tpageDisplay.Padding = new Padding(3);
            this.tpageDisplay.Size = new Size(0x147, 280);
            this.tpageDisplay.TabIndex = 0;
            this.tpageDisplay.Text = "Settings";
            this.cbCapitalIDs.AutoSize = true;
            this.cbCapitalIDs.Location = new Point(0x17, 0x101);
            this.cbCapitalIDs.Name = "cbCapitalIDs";
            this.cbCapitalIDs.Size = new Size(0x67, 0x11);
            this.cbCapitalIDs.TabIndex = 0x19;
            this.cbCapitalIDs.Text = "View Capital IDs";
            this.cbCapitalIDs.UseVisualStyleBackColor = true;
            this.cbCapitalIDs.CheckedChanged += new EventHandler(this.cbCapitalIDs_CheckedChanged);
            this.cbProductionInfo.AutoSize = true;
            this.cbProductionInfo.Location = new Point(0x17, 0xab);
            this.cbProductionInfo.Name = "cbProductionInfo";
            this.cbProductionInfo.Size = new Size(0xd5, 0x11);
            this.cbProductionInfo.TabIndex = 0x18;
            this.cbProductionInfo.Text = "Production Indicators in the Village Map";
            this.cbProductionInfo.UseVisualStyleBackColor = true;
            this.cbProductionInfo.CheckedChanged += new EventHandler(this.cbProductionInfo_CheckedChanged);
            this.cbFlashingTaskbarAttack.AutoSize = true;
            this.cbFlashingTaskbarAttack.Location = new Point(0x17, 0x95);
            this.cbFlashingTaskbarAttack.Name = "cbFlashingTaskbarAttack";
            this.cbFlashingTaskbarAttack.Size = new Size(0xc3, 0x11);
            this.cbFlashingTaskbarAttack.TabIndex = 0x17;
            this.cbFlashingTaskbarAttack.Text = "Flash Taskbar Icon When Attacked";
            this.cbFlashingTaskbarAttack.UseVisualStyleBackColor = true;
            this.cbFlashingTaskbarAttack.CheckedChanged += new EventHandler(this.cbFlashingTaskbarAttack_CheckedChanged);
            this.lblAdvanced.AutoSize = true;
            this.lblAdvanced.Location = new Point(6, 0xd6);
            this.lblAdvanced.Name = "lblAdvanced";
            this.lblAdvanced.Size = new Size(0x5f, 13);
            this.lblAdvanced.TabIndex = 0x16;
            this.lblAdvanced.Text = "Advanced Options";
            this.cbVillageIDs.AutoSize = true;
            this.cbVillageIDs.Location = new Point(0x17, 0xea);
            this.cbVillageIDs.Name = "cbVillageIDs";
            this.cbVillageIDs.Size = new Size(0x66, 0x11);
            this.cbVillageIDs.TabIndex = 0x15;
            this.cbVillageIDs.Text = "View Village IDs";
            this.cbVillageIDs.UseVisualStyleBackColor = true;
            this.cbVillageIDs.CheckedChanged += new EventHandler(this.cbVillageIDs_CheckedChanged);
            this.cbConfirmOpenMultiple.AutoSize = true;
            this.cbConfirmOpenMultiple.Location = new Point(0x17, 0x7f);
            this.cbConfirmOpenMultiple.Name = "cbConfirmOpenMultiple";
            this.cbConfirmOpenMultiple.Size = new Size(0x95, 0x11);
            this.cbConfirmOpenMultiple.TabIndex = 20;
            this.cbConfirmOpenMultiple.Text = "Open Multiple Card Packs";
            this.cbConfirmOpenMultiple.UseVisualStyleBackColor = true;
            this.cbConfirmOpenMultiple.CheckedChanged += new EventHandler(this.cbConfirmOpenMultiple_CheckedChanged);
            this.cbConfirmBuyMultiple.AutoSize = true;
            this.cbConfirmBuyMultiple.Location = new Point(0x17, 0x69);
            this.cbConfirmBuyMultiple.Name = "cbConfirmBuyMultiple";
            this.cbConfirmBuyMultiple.Size = new Size(0x8d, 0x11);
            this.cbConfirmBuyMultiple.TabIndex = 0x13;
            this.cbConfirmBuyMultiple.Text = "Buy Multiple Card Packs";
            this.cbConfirmBuyMultiple.UseVisualStyleBackColor = true;
            this.cbConfirmBuyMultiple.CheckedChanged += new EventHandler(this.cbConfirmBuyMultiple_CheckedChanged);
            this.btnResumeTutorial.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnResumeTutorial.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnResumeTutorial.BorderDrawing = true;
            this.btnResumeTutorial.FocusRectangleEnabled = false;
            this.btnResumeTutorial.Image = null;
            this.btnResumeTutorial.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnResumeTutorial.ImageBorderEnabled = true;
            this.btnResumeTutorial.ImageDropShadow = true;
            this.btnResumeTutorial.ImageFocused = null;
            this.btnResumeTutorial.ImageInactive = null;
            this.btnResumeTutorial.ImageMouseOver = null;
            this.btnResumeTutorial.ImageNormal = null;
            this.btnResumeTutorial.ImagePressed = null;
            this.btnResumeTutorial.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnResumeTutorial.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnResumeTutorial.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnResumeTutorial.Location = new Point(0xc2, 0xec);
            this.btnResumeTutorial.Name = "btnResumeTutorial";
            this.btnResumeTutorial.OffsetPressedContent = true;
            this.btnResumeTutorial.Padding2 = 5;
            this.btnResumeTutorial.Size = new Size(0x76, 0x17);
            this.btnResumeTutorial.StretchImage = false;
            this.btnResumeTutorial.TabIndex = 0x12;
            this.btnResumeTutorial.Text = "Resume Tutorial";
            this.btnResumeTutorial.TextDropShadow = false;
            this.btnResumeTutorial.UseVisualStyleBackColor = false;
            this.btnResumeTutorial.Visible = false;
            this.btnResumeTutorial.Click += new EventHandler(this.btnResumeTutorial_Click);
            this.btnDebugInfo.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnDebugInfo.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnDebugInfo.BorderDrawing = true;
            this.btnDebugInfo.FocusRectangleEnabled = false;
            this.btnDebugInfo.Image = null;
            this.btnDebugInfo.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnDebugInfo.ImageBorderEnabled = true;
            this.btnDebugInfo.ImageDropShadow = true;
            this.btnDebugInfo.ImageFocused = null;
            this.btnDebugInfo.ImageInactive = null;
            this.btnDebugInfo.ImageMouseOver = null;
            this.btnDebugInfo.ImageNormal = null;
            this.btnDebugInfo.ImagePressed = null;
            this.btnDebugInfo.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnDebugInfo.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnDebugInfo.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnDebugInfo.Location = new Point(0xc2, 0xd9);
            this.btnDebugInfo.Name = "btnDebugInfo";
            this.btnDebugInfo.OffsetPressedContent = true;
            this.btnDebugInfo.Padding2 = 5;
            this.btnDebugInfo.Size = new Size(0x76, 0x17);
            this.btnDebugInfo.StretchImage = false;
            this.btnDebugInfo.TabIndex = 0x11;
            this.btnDebugInfo.Text = "Debug Info";
            this.btnDebugInfo.TextDropShadow = false;
            this.btnDebugInfo.UseVisualStyleBackColor = false;
            this.btnDebugInfo.Click += new EventHandler(this.btnDebugInfo_Click);
            this.cbTooltips.AutoSize = true;
            this.cbTooltips.Location = new Point(0x17, 0x27);
            this.cbTooltips.Name = "cbTooltips";
            this.cbTooltips.Size = new Size(0x3f, 0x11);
            this.cbTooltips.TabIndex = 0x10;
            this.cbTooltips.Text = "Tooltips";
            this.cbTooltips.UseVisualStyleBackColor = true;
            this.cbTooltips.CheckedChanged += new EventHandler(this.cbTooltips_CheckedChanged);
            this.cbInstantTooltips.AutoSize = true;
            this.cbInstantTooltips.Location = new Point(0x17, 0x3d);
            this.cbInstantTooltips.Name = "cbInstantTooltips";
            this.cbInstantTooltips.Size = new Size(0x62, 0x11);
            this.cbInstantTooltips.TabIndex = 15;
            this.cbInstantTooltips.Text = "Instant Tooltips";
            this.cbInstantTooltips.UseVisualStyleBackColor = true;
            this.cbInstantTooltips.CheckedChanged += new EventHandler(this.cbInstantTooltips_CheckedChanged);
            this.cbConfirmCards.AutoSize = true;
            this.cbConfirmCards.Location = new Point(0x17, 0x53);
            this.cbConfirmCards.Name = "cbConfirmCards";
            this.cbConfirmCards.Size = new Size(0x80, 0x11);
            this.cbConfirmCards.TabIndex = 14;
            this.cbConfirmCards.Text = "Confirm Playing Cards";
            this.cbConfirmCards.UseVisualStyleBackColor = true;
            this.cbConfirmCards.CheckedChanged += new EventHandler(this.cbConfirmCards_CheckedChanged);
            this.cbProfanityFilter.AutoSize = true;
            this.cbProfanityFilter.Location = new Point(0x17, 0x11);
            this.cbProfanityFilter.Name = "cbProfanityFilter";
            this.cbProfanityFilter.Size = new Size(0x9f, 0x11);
            this.cbProfanityFilter.TabIndex = 13;
            this.cbProfanityFilter.Text = "Profanity Filter (English Only)";
            this.cbProfanityFilter.UseVisualStyleBackColor = true;
            this.cbProfanityFilter.CheckedChanged += new EventHandler(this.cbProfanityFilter_CheckedChanged);
            this.cbWhiteTextBox.AutoSize = true;
            this.cbWhiteTextBox.Location = new Point(0x17, 0xc1);
            this.cbWhiteTextBox.Name = "cbWhiteTextBox";
            this.cbWhiteTextBox.Size = new Size(0xe4, 0x11);
            this.cbWhiteTextBox.TabIndex = 0x18;
            this.cbWhiteTextBox.Text = "Show White Background on Parish Names";
            this.cbWhiteTextBox.UseVisualStyleBackColor = true;
            this.cbWhiteTextBox.CheckedChanged += new EventHandler(this.cbWhiteTextBox_CheckedChanged);
            this.tabPage3.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            this.tabPage3.Controls.Add(this.cbWinterLandscape);
            this.tabPage3.Controls.Add(this.cbSeasonalFX);
            this.tabPage3.Controls.Add(this.cbBattleSFX);
            this.tabPage3.Controls.Add(this.btnRestoreDefaultVolumes);
            this.tabPage3.Controls.Add(this.lblVolumes);
            this.tabPage3.Controls.Add(this.trackBarEnvironmentals);
            this.tabPage3.Controls.Add(this.cbEnvironmentals);
            this.tabPage3.Controls.Add(this.trackBarSFX);
            this.tabPage3.Controls.Add(this.cbSFX);
            this.tabPage3.Controls.Add(this.cbGraphicsCompatibility);
            this.tabPage3.Controls.Add(this.trackBarMusicVolume);
            this.tabPage3.Controls.Add(this.cbMusic);
            this.tabPage3.Location = new Point(4, 0x19);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(3);
            this.tabPage3.Size = new Size(0x147, 280);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Audio";
            this.cbWinterLandscape.AutoSize = true;
            this.cbWinterLandscape.ForeColor = ARGBColors.Black;
            this.cbWinterLandscape.Location = new Point(20, 0xdf);
            this.cbWinterLandscape.Name = "cbWinterLandscape";
            this.cbWinterLandscape.Size = new Size(0x8f, 0x11);
            this.cbWinterLandscape.TabIndex = 0x19;
            this.cbWinterLandscape.Text = "Show Winter Landscape";
            this.cbWinterLandscape.UseVisualStyleBackColor = true;
            this.cbWinterLandscape.CheckedChanged += new EventHandler(this.cbWinterLandscape_CheckedChanged);
            this.cbSeasonalFX.AutoSize = true;
            this.cbSeasonalFX.ForeColor = ARGBColors.Black;
            this.cbSeasonalFX.Location = new Point(20, 0xcb);
            this.cbSeasonalFX.Name = "cbSeasonalFX";
            this.cbSeasonalFX.Size = new Size(0x74, 0x11);
            this.cbSeasonalFX.TabIndex = 0x18;
            this.cbSeasonalFX.Text = "Show Seasonal FX";
            this.cbSeasonalFX.UseVisualStyleBackColor = true;
            this.cbSeasonalFX.CheckedChanged += new EventHandler(this.cbSeasonalFX_CheckedChanged);
            this.cbBattleSFX.AutoSize = true;
            this.cbBattleSFX.ForeColor = ARGBColors.Black;
            this.cbBattleSFX.Location = new Point(20, 0x63);
            this.cbBattleSFX.Name = "cbBattleSFX";
            this.cbBattleSFX.Size = new Size(0x67, 0x11);
            this.cbBattleSFX.TabIndex = 11;
            this.cbBattleSFX.Text = "Battle Sound FX";
            this.cbBattleSFX.UseVisualStyleBackColor = true;
            this.cbBattleSFX.CheckedChanged += new EventHandler(this.cbBattleSFX_CheckedChanged);
            this.btnRestoreDefaultVolumes.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnRestoreDefaultVolumes.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnRestoreDefaultVolumes.BorderDrawing = true;
            this.btnRestoreDefaultVolumes.FocusRectangleEnabled = false;
            this.btnRestoreDefaultVolumes.Image = null;
            this.btnRestoreDefaultVolumes.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnRestoreDefaultVolumes.ImageBorderEnabled = true;
            this.btnRestoreDefaultVolumes.ImageDropShadow = true;
            this.btnRestoreDefaultVolumes.ImageFocused = null;
            this.btnRestoreDefaultVolumes.ImageInactive = null;
            this.btnRestoreDefaultVolumes.ImageMouseOver = null;
            this.btnRestoreDefaultVolumes.ImageNormal = null;
            this.btnRestoreDefaultVolumes.ImagePressed = null;
            this.btnRestoreDefaultVolumes.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnRestoreDefaultVolumes.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnRestoreDefaultVolumes.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnRestoreDefaultVolumes.Location = new Point(0x86, 0xa7);
            this.btnRestoreDefaultVolumes.Name = "btnRestoreDefaultVolumes";
            this.btnRestoreDefaultVolumes.OffsetPressedContent = true;
            this.btnRestoreDefaultVolumes.Padding2 = 5;
            this.btnRestoreDefaultVolumes.Size = new Size(0xa7, 0x17);
            this.btnRestoreDefaultVolumes.StretchImage = false;
            this.btnRestoreDefaultVolumes.TabIndex = 10;
            this.btnRestoreDefaultVolumes.Text = "Restore Defaults";
            this.btnRestoreDefaultVolumes.TextDropShadow = false;
            this.btnRestoreDefaultVolumes.UseVisualStyleBackColor = false;
            this.btnRestoreDefaultVolumes.Click += new EventHandler(this.btnRestoreDefaultVolumes_Click);
            this.lblVolumes.ForeColor = ARGBColors.Black;
            this.lblVolumes.Location = new Point(0x83, 10);
            this.lblVolumes.Name = "lblVolumes";
            this.lblVolumes.Size = new Size(170, 13);
            this.lblVolumes.TabIndex = 9;
            this.lblVolumes.Text = "Volume";
            this.lblVolumes.TextAlign = ContentAlignment.TopCenter;
            this.trackBarEnvironmentals.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            this.trackBarEnvironmentals.Location = new Point(0x83, 0x7e);
            this.trackBarEnvironmentals.Maximum = 100;
            this.trackBarEnvironmentals.Minimum = 1;
            this.trackBarEnvironmentals.Name = "trackBarEnvironmentals";
            this.trackBarEnvironmentals.Size = new Size(170, 0x2d);
            this.trackBarEnvironmentals.TabIndex = 8;
            this.trackBarEnvironmentals.TickFrequency = 5;
            this.trackBarEnvironmentals.Value = 1;
            this.trackBarEnvironmentals.ValueChanged += new EventHandler(this.trackBarEnvironmentals_ValueChanged);
            this.cbEnvironmentals.AutoSize = true;
            this.cbEnvironmentals.ForeColor = ARGBColors.Black;
            this.cbEnvironmentals.Location = new Point(20, 0x84);
            this.cbEnvironmentals.Name = "cbEnvironmentals";
            this.cbEnvironmentals.Size = new Size(0x62, 0x11);
            this.cbEnvironmentals.TabIndex = 7;
            this.cbEnvironmentals.Text = "Environmentals";
            this.cbEnvironmentals.UseVisualStyleBackColor = true;
            this.cbEnvironmentals.CheckedChanged += new EventHandler(this.cbEnvironmentals_CheckedChanged);
            this.trackBarSFX.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            this.trackBarSFX.Location = new Point(0x83, 0x4c);
            this.trackBarSFX.Maximum = 100;
            this.trackBarSFX.Minimum = 1;
            this.trackBarSFX.Name = "trackBarSFX";
            this.trackBarSFX.Size = new Size(170, 0x2d);
            this.trackBarSFX.TabIndex = 5;
            this.trackBarSFX.TickFrequency = 5;
            this.trackBarSFX.Value = 1;
            this.trackBarSFX.ValueChanged += new EventHandler(this.trackBarSFX_ValueChanged);
            this.cbSFX.AutoSize = true;
            this.cbSFX.ForeColor = ARGBColors.Black;
            this.cbSFX.Location = new Point(20, 0x4c);
            this.cbSFX.Name = "cbSFX";
            this.cbSFX.Size = new Size(0x49, 0x11);
            this.cbSFX.TabIndex = 4;
            this.cbSFX.Text = "Sound FX";
            this.cbSFX.UseVisualStyleBackColor = true;
            this.cbSFX.CheckedChanged += new EventHandler(this.cbSFX_CheckedChanged);
            this.cbGraphicsCompatibility.AutoSize = true;
            this.cbGraphicsCompatibility.ForeColor = ARGBColors.Black;
            this.cbGraphicsCompatibility.Location = new Point(20, 0xf4);
            this.cbGraphicsCompatibility.Name = "cbGraphicsCompatibility";
            this.cbGraphicsCompatibility.Size = new Size(0x9f, 0x11);
            this.cbGraphicsCompatibility.TabIndex = 3;
            this.cbGraphicsCompatibility.Text = "Graphics Compatibility Mode";
            this.cbGraphicsCompatibility.UseVisualStyleBackColor = true;
            this.cbGraphicsCompatibility.CheckedChanged += new EventHandler(this.cbGraphicsCompatibility_CheckedChanged);
            this.trackBarMusicVolume.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            this.trackBarMusicVolume.Location = new Point(0x83, 0x1a);
            this.trackBarMusicVolume.Maximum = 100;
            this.trackBarMusicVolume.Minimum = 1;
            this.trackBarMusicVolume.Name = "trackBarMusicVolume";
            this.trackBarMusicVolume.Size = new Size(170, 0x2d);
            this.trackBarMusicVolume.TabIndex = 1;
            this.trackBarMusicVolume.TickFrequency = 5;
            this.trackBarMusicVolume.Value = 1;
            this.trackBarMusicVolume.ValueChanged += new EventHandler(this.trackBarMusicVolume_ValueChanged);
            this.cbMusic.AutoSize = true;
            this.cbMusic.ForeColor = ARGBColors.Black;
            this.cbMusic.Location = new Point(20, 0x20);
            this.cbMusic.Name = "cbMusic";
            this.cbMusic.Size = new Size(0x36, 0x11);
            this.cbMusic.TabIndex = 0;
            this.cbMusic.Text = "Music";
            this.cbMusic.UseVisualStyleBackColor = true;
            this.cbMusic.CheckedChanged += new EventHandler(this.cbMusic_CheckedChanged);
            this.tabPage1.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            this.tabPage1.Controls.Add(this.btnDownloadTranslations);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.listBoxLanguages);
            this.tabPage1.ForeColor = ARGBColors.Black;
            this.tabPage1.Location = new Point(4, 0x19);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x147, 280);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Languages";
            this.btnDownloadTranslations.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnDownloadTranslations.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnDownloadTranslations.BorderDrawing = true;
            this.btnDownloadTranslations.FocusRectangleEnabled = false;
            this.btnDownloadTranslations.Image = null;
            this.btnDownloadTranslations.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnDownloadTranslations.ImageBorderEnabled = true;
            this.btnDownloadTranslations.ImageDropShadow = true;
            this.btnDownloadTranslations.ImageFocused = null;
            this.btnDownloadTranslations.ImageInactive = null;
            this.btnDownloadTranslations.ImageMouseOver = null;
            this.btnDownloadTranslations.ImageNormal = null;
            this.btnDownloadTranslations.ImagePressed = null;
            this.btnDownloadTranslations.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnDownloadTranslations.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnDownloadTranslations.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnDownloadTranslations.Location = new Point(0x1f, 0xe3);
            this.btnDownloadTranslations.Name = "btnDownloadTranslations";
            this.btnDownloadTranslations.OffsetPressedContent = true;
            this.btnDownloadTranslations.Padding2 = 5;
            this.btnDownloadTranslations.Size = new Size(0x10a, 0x17);
            this.btnDownloadTranslations.StretchImage = false;
            this.btnDownloadTranslations.TabIndex = 2;
            this.btnDownloadTranslations.Text = "Download Community Translations";
            this.btnDownloadTranslations.TextDropShadow = false;
            this.btnDownloadTranslations.UseVisualStyleBackColor = false;
            this.btnDownloadTranslations.Click += new EventHandler(this.btnDownloadTranslations_Click);
            this.label1.AutoSize = true;
            this.label1.ForeColor = ARGBColors.Black;
            this.label1.Location = new Point(0x69, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x6a, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Available Languages";
            this.listBoxLanguages.BackColor = ARGBColors.White;
            this.listBoxLanguages.ForeColor = ARGBColors.Black;
            this.listBoxLanguages.FormattingEnabled = true;
            this.listBoxLanguages.Location = new Point(60, 0x23);
            this.listBoxLanguages.Name = "listBoxLanguages";
            this.listBoxLanguages.Size = new Size(0xc9, 0xad);
            this.listBoxLanguages.TabIndex = 0;
            this.listBoxLanguages.SelectedIndexChanged += new EventHandler(this.listBoxLanguages_SelectedIndexChanged);
            this.btnApply.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnApply.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnApply.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnApply.BorderDrawing = true;
            this.btnApply.Enabled = false;
            this.btnApply.FocusRectangleEnabled = false;
            this.btnApply.Image = null;
            this.btnApply.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnApply.ImageBorderEnabled = true;
            this.btnApply.ImageDropShadow = true;
            this.btnApply.ImageFocused = null;
            this.btnApply.ImageInactive = null;
            this.btnApply.ImageMouseOver = null;
            this.btnApply.ImageNormal = null;
            this.btnApply.ImagePressed = null;
            this.btnApply.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnApply.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnApply.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnApply.Location = new Point(0xfb, 0x160);
            this.btnApply.Name = "btnApply";
            this.btnApply.OffsetPressedContent = true;
            this.btnApply.Padding2 = 5;
            this.btnApply.Size = new Size(90, 0x1a);
            this.btnApply.StretchImage = false;
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.TextDropShadow = false;
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnCancel.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnCancel.BorderDrawing = true;
            this.btnCancel.FocusRectangleEnabled = false;
            this.btnCancel.Image = null;
            this.btnCancel.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnCancel.ImageBorderEnabled = true;
            this.btnCancel.ImageDropShadow = true;
            this.btnCancel.ImageFocused = null;
            this.btnCancel.ImageInactive = null;
            this.btnCancel.ImageMouseOver = null;
            this.btnCancel.ImageNormal = null;
            this.btnCancel.ImagePressed = null;
            this.btnCancel.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnCancel.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnCancel.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnCancel.Location = new Point(0x9b, 0x160);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OffsetPressedContent = true;
            this.btnCancel.Padding2 = 5;
            this.btnCancel.Size = new Size(90, 0x1a);
            this.btnCancel.StretchImage = false;
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextDropShadow = false;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnOK.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnOK.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnOK.BorderDrawing = true;
            this.btnOK.FocusRectangleEnabled = false;
            this.btnOK.Image = null;
            this.btnOK.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnOK.ImageBorderEnabled = true;
            this.btnOK.ImageDropShadow = true;
            this.btnOK.ImageFocused = null;
            this.btnOK.ImageInactive = null;
            this.btnOK.ImageMouseOver = null;
            this.btnOK.ImageNormal = null;
            this.btnOK.ImagePressed = null;
            this.btnOK.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnOK.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnOK.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnOK.Location = new Point(0x3b, 0x160);
            this.btnOK.Name = "btnOK";
            this.btnOK.OffsetPressedContent = true;
            this.btnOK.Padding2 = 5;
            this.btnOK.Size = new Size(90, 0x1a);
            this.btnOK.StretchImage = false;
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.TextDropShadow = false;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.pnlWikiHelp.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.pnlWikiHelp.Location = new Point(14, 0x15c);
            this.pnlWikiHelp.Name = "pnlWikiHelp";
            this.pnlWikiHelp.Size = new Size(0x23, 0x23);
            this.pnlWikiHelp.TabIndex = 14;
            this.pnlWikiHelp.MouseLeave += new EventHandler(this.pnlWikiHelp_MouseLeave);
            this.pnlWikiHelp.MouseClick += new MouseEventHandler(this.pnlWikiHelp_MouseClick);
            this.pnlWikiHelp.MouseEnter += new EventHandler(this.pnlWikiHelp_MouseEnter);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x161, 0x182);
            base.Controls.Add(this.pnlWikiHelp);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.tabOptions);
            base.Icon = Resources.shk_icon;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "OptionsPopup";
            base.ShowBar = true;
            base.ShowClose = true;
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Settings";
            base.Controls.SetChildIndex(this.tabOptions, 0);
            base.Controls.SetChildIndex(this.btnApply, 0);
            base.Controls.SetChildIndex(this.btnCancel, 0);
            base.Controls.SetChildIndex(this.btnOK, 0);
            base.Controls.SetChildIndex(this.pnlWikiHelp, 0);
            this.tabOptions.ResumeLayout(false);
            this.tpageDisplay.ResumeLayout(false);
            this.tpageDisplay.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.trackBarEnvironmentals.EndInit();
            this.trackBarSFX.EndInit();
            this.trackBarMusicVolume.EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            base.ResumeLayout(false);
        }

        public void initLabels()
        {
            this.tpageDisplay.Text = SK.Text("Options_Settings", "Settings");
            this.btnResumeTutorial.Text = SK.Text("Options_Resume_Tutorial", "Resume Tutorial");
            this.btnDebugInfo.Text = SK.Text("Options_Debug_Info", "Debug Info");
            this.cbTooltips.Text = SK.Text("Options_Tooltips", "Tooltips");
            this.cbInstantTooltips.Text = SK.Text("Options_Instant_Tooltips", "Instant Tooltips");
            this.cbConfirmCards.Text = SK.Text("Options_ConfirmCards", "Confirm Playing Cards");
            this.cbConfirmBuyMultiple.Text = SK.Text("Options_ConfirmCardsBuy", "Buy Multiple Card Packs");
            this.cbConfirmOpenMultiple.Text = SK.Text("Options_ConfirmCardsOpen", "Open Multiple Card Packs");
            this.cbProfanityFilter.Text = SK.Text("Options_Profanity_Filter", "Profanity Filter");
            this.btnApply.Text = SK.Text("Options_Apply", "Apply");
            this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.Text = base.Title = SK.Text("Options_Settings", "Settings");
            this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
            this.label1.Text = SK.Text("Options_Available_Languages", "Available Languages");
            this.tabPage3.Text = SK.Text("Options_Audio", "Audio / Visual");
            this.cbMusic.Text = SK.Text("Options_Music", "Music");
            this.cbGraphicsCompatibility.Text = SK.Text("Options_Graphics_Compatibility", "Graphics Compatibility Mode");
            this.tabPage1.Text = SK.Text("Options_Languages", "Languages");
            this.btnDownloadTranslations.Text = SK.Text("Options_Download_Languages", "Download Community Translations");
            this.cbSFX.Text = SK.Text("Options_SFX", "Sound FX");
            this.cbEnvironmentals.Text = SK.Text("Options_Environmentals", "Ambient Sounds");
            this.cbBattleSFX.Text = SK.Text("Options_BattleSFX", "Battle Sound FX");
            this.lblVolumes.Text = SK.Text("Options_Volumne", "Volume");
            this.btnRestoreDefaultVolumes.Text = SK.Text("Options_RestoreDefaultVolume", "Restore Defaults");
            this.lblAdvanced.Text = SK.Text("Options_AdvancedOptions", "Advanced Options");
            this.cbVillageIDs.Text = SK.Text("Options_VillageIDs", "View Village IDs");
            this.cbCapitalIDs.Text = SK.Text("Options_CapitalIDs", "View Capital IDs");
            this.cbSeasonalFX.Text = SK.Text("Options_show_Seasonal_FX2", "Show Snow Effect");
            this.cbWinterLandscape.Text = SK.Text("Options_show_Winter", "Show Winter Landscape");
            this.cbFlashingTaskbarAttack.Text = SK.Text("Options_Flashing_Taskbar_Attack", "Flash Taskbar Icon When Attacked");
            this.cbProductionInfo.Text = SK.Text("Options_Production_Info", "Production Indicators in the Village Map");
            this.cbWhiteTextBox.Text = SK.Text("Options_White_Backgrounds", "Show White Background on Parish Names");
        }

        private void listBoxLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_language_selected");
            }
            this.btnApply.Enabled = true;
        }

        public static void openSettings()
        {
            if ((popup == null) || !popup.Created)
            {
                popup = new OptionsPopup();
            }
            popup.setup(0);
            popup.Show(InterfaceMgr.Instance.ParentForm);
        }

        public static void openSettings(int tabID)
        {
            if ((popup == null) || !popup.Created)
            {
                popup = new OptionsPopup();
            }
            popup.setup(tabID);
            popup.Show(InterfaceMgr.Instance.ParentForm);
        }

        public static void openSettingsLogin()
        {
            if ((popup == null) || !popup.Created)
            {
                popup = new OptionsPopup();
            }
            popup.setup(0);
            popup.ShowDialog(Program.profileLogin);
            popup.Dispose();
        }

        private void pnlWikiHelp_MouseClick(object sender, MouseEventArgs e)
        {
            CustomSelfDrawPanel.WikiLinkControl.openHelpLink(30);
        }

        private void pnlWikiHelp_MouseEnter(object sender, EventArgs e)
        {
            this.pnlWikiHelp.BackgroundImage = (Image) GFXLibrary.int_button_Q_over;
        }

        private void pnlWikiHelp_MouseLeave(object sender, EventArgs e)
        {
            this.pnlWikiHelp.BackgroundImage = (Image) GFXLibrary.int_button_Q_normal;
        }

        public static void registerCallback(ResolutionChangeCallback newResolutionChangeCallback)
        {
            resolutionChangeCallback = newResolutionChangeCallback;
        }

        public void setup(int tabID)
        {
            this.playSounds = false;
            this.initLabels();
            this.initialLanguageIndex = -1;
            this.listBoxLanguages.Items.Clear();
            this.languageIDs.Clear();
            this.addLanguage("English", "en");
            this.addLanguage("Deutsch", "de");
            this.addLanguage("Fran\x00e7ais", "fr");
            this.addLanguage("Русский", "ru");
            this.addLanguage("Espa\x00f1ol", "es");
            this.addLanguage("Polski", "pl");
            this.addLanguage("T\x00fcrk\x00e7e", "tr");
            this.addLanguage("Italiano", "it");
            this.addLanguage("Portugu\x00eas do Brasil", "pt");
            foreach (SKLang lang in Program.communityLangs)
            {
                bool flag = true;
                if ((((lang.id == "en") || (lang.id == "de")) || ((lang.id == "fr") || (lang.id == "ru"))) || (((lang.id == "es") || (lang.id == "pl")) || (((lang.id == "tr") || (lang.id == "it")) || (lang.id == "pt"))))
                {
                    flag = false;
                }
                if (flag)
                {
                    this.addLanguage(lang.name + "   (" + SK.Text("OptionsPopup_CommunityLanguage", "Community Translation") + ")", lang.id);
                }
            }
            this.cbSeasonalFX.Visible = Program.ShowSeasonalFXOption;
            this.cbWinterLandscape.Visible = Program.ShowSeasonalFXOption;
            this.pnlWikiHelp.BackgroundImage = (Image) GFXLibrary.int_button_Q_normal;
            CustomTooltipManager.addTooltipToSystemControl(this.pnlWikiHelp, 0x1132);
            this.btnResumeTutorial.Visible = false;
            RemoteServices.Instance.set_UpdateReportFilters_UserCallBack(new RemoteServices.UpdateReportFilters_UserCallBack(this.updateReportFiltersCallback));
            this.cbProfanityFilter.Checked = RemoteServices.Instance.UserOptions.profanityFilter;
            this.cbConfirmCards.Checked = Program.mySettings.ConfirmPlayCard;
            this.cbConfirmOpenMultiple.Checked = Program.mySettings.OpenMultipleCardPacks;
            this.cbConfirmBuyMultiple.Checked = Program.mySettings.BuyMultipleCardPacks;
            this.cbInstantTooltips.Checked = Program.mySettings.SETTINGS_instantTooltips;
            this.cbTooltips.Checked = Program.mySettings.SETTINGS_showTooltips;
            this.cbVillageIDs.Checked = Program.mySettings.viewVillageIDs;
            this.cbCapitalIDs.Checked = Program.mySettings.viewCapitalIDs;
            this.cbSeasonalFX.Checked = Program.mySettings.SeasonalSpecialFX;
            this.cbWinterLandscape.Checked = Program.mySettings.SeasonalWinterLandscape;
            this.cbFlashingTaskbarAttack.Checked = Program.mySettings.FlashingTaskbarAttack;
            this.cbProductionInfo.Checked = Program.mySettings.ShowProductionInfo;
            this.cbWhiteTextBox.Checked = Program.mySettings.UseMapTextBorders;
            this.cbMusic.Checked = Program.mySettings.Music;
            int musicVolume = Program.mySettings.MusicVolume;
            if (musicVolume < this.trackBarMusicVolume.Minimum)
            {
                musicVolume = this.trackBarMusicVolume.Minimum;
            }
            if (musicVolume > this.trackBarMusicVolume.Maximum)
            {
                musicVolume = this.trackBarMusicVolume.Maximum;
            }
            this.trackBarMusicVolume.Value = musicVolume;
            this.cbSFX.Checked = Program.mySettings.SFX;
            this.cbBattleSFX.Checked = Program.mySettings.BattleSFX;
            int sFXVolume = Program.mySettings.SFXVolume;
            if (sFXVolume < this.trackBarSFX.Minimum)
            {
                sFXVolume = this.trackBarSFX.Minimum;
            }
            if (sFXVolume > this.trackBarSFX.Maximum)
            {
                sFXVolume = this.trackBarSFX.Maximum;
            }
            this.trackBarSFX.Value = sFXVolume;
            this.cbEnvironmentals.Checked = Program.mySettings.Environmentals;
            int environmentalVolume = Program.mySettings.EnvironmentalVolume;
            if (environmentalVolume < this.trackBarEnvironmentals.Minimum)
            {
                environmentalVolume = this.trackBarEnvironmentals.Minimum;
            }
            if (environmentalVolume > this.trackBarEnvironmentals.Maximum)
            {
                environmentalVolume = this.trackBarEnvironmentals.Maximum;
            }
            this.trackBarEnvironmentals.Value = environmentalVolume;
            if (Program.mySettings.AAMode == 1)
            {
                this.cbGraphicsCompatibility.Checked = true;
            }
            else
            {
                this.cbGraphicsCompatibility.Checked = false;
            }
            this.musicVolumeChanged = false;
            this.soundfxVolumeChanged = false;
            this.environmentalVolumeChanged = false;
            switch (tabID)
            {
                case 0:
                    this.tabOptions.SelectTab("tpageDisplay");
                    break;

                case 1:
                    this.tabOptions.SelectTab("tpageReports");
                    break;
            }
            this.btnApply.Enabled = false;
            this.playSounds = true;
            StatTrackingClient.Instance().ActivateTrigger(0x1c, Program.mySettings.UseMapTextBorders);
        }

        private void tabOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.playSounds)
            {
                GameEngine.Instance.playInterfaceSound("Options_tab_changed");
            }
        }

        private void trackBarEnvironmentals_ValueChanged(object sender, EventArgs e)
        {
            GameEngine.Instance.AudioEngine.setEnvironmentalMasterVolume(((float) this.trackBarEnvironmentals.Value) / 100f);
            this.btnApply.Enabled = true;
            this.environmentalVolumeChanged = true;
        }

        private void trackBarMusicVolume_ValueChanged(object sender, EventArgs e)
        {
            GameEngine.Instance.AudioEngine.setMP3MasterVolume(((float) this.trackBarMusicVolume.Value) / 100f, 0);
            this.btnApply.Enabled = true;
            this.musicVolumeChanged = true;
        }

        private void trackBarSFX_ValueChanged(object sender, EventArgs e)
        {
            GameEngine.Instance.AudioEngine.setSFXMasterVolume(((float) this.trackBarSFX.Value) / 100f);
            this.btnApply.Enabled = true;
            this.soundfxVolumeChanged = true;
        }

        private void updateReportFiltersCallback(UpdateReportFilters_ReturnType returnData)
        {
            bool success = returnData.Success;
        }

        public delegate void ResolutionChangeCallback(int newRes);
    }
}

