namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ReportCapturePanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDCheckBox achievementsCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox attackCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDCheckBox buyVillagesCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDCheckBox capitalAttackCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDLabel captureLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDCheckBox cardsCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private IContainer components;
        private CustomSelfDrawPanel.CSDCheckBox defenceCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox electionsCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox enemyCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox factionsCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox foragingCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox forwardedOnlyCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox houseCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private int m_mode;
        private CustomSelfDrawPanel.CSDButton okButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDCheckBox questsCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox readMessagesCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox reinforceCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox religionCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox researchCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox scoutingCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox spinsCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox tradeCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox vassalsCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox villageLostCheck = new CustomSelfDrawPanel.CSDCheckBox();

        public ReportCapturePanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void checkToggled()
        {
            if (this.m_mode == 1)
            {
                CustomSelfDrawPanel.CSDControl clickedControl = base.ClickedControl;
                if (clickedControl != null)
                {
                    switch (clickedControl.Data)
                    {
                        case -4:
                            ReportsPanel.Instance.ShowVillageLost = !ReportsPanel.Instance.ShowVillageLost;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case -3:
                            ReportsPanel.Instance.ShowForwardedMessagesOnly = !ReportsPanel.Instance.ShowForwardedMessagesOnly;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case -2:
                            ReportsPanel.Instance.ShowParishAttacks = !ReportsPanel.Instance.ShowParishAttacks;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case -1:
                            ReportsPanel.Instance.ShowReadMessages = !ReportsPanel.Instance.ShowReadMessages;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 0:
                            ReportsPanel.Instance.Filters.attacks = this.attackCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 1:
                            ReportsPanel.Instance.Filters.defense = this.defenceCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 2:
                            ReportsPanel.Instance.Filters.enemyWarnings = this.enemyCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 3:
                            ReportsPanel.Instance.Filters.reinforcements = this.reinforceCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 4:
                            ReportsPanel.Instance.Filters.scouting = this.scoutingCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 5:
                            ReportsPanel.Instance.Filters.foraging = this.foragingCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 6:
                            ReportsPanel.Instance.Filters.trade = this.tradeCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 7:
                            ReportsPanel.Instance.Filters.vassals = this.vassalsCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 8:
                            ReportsPanel.Instance.Filters.religion = this.religionCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 9:
                            ReportsPanel.Instance.Filters.research = this.researchCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 10:
                            ReportsPanel.Instance.Filters.elections = this.electionsCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 11:
                            ReportsPanel.Instance.Filters.factions = this.factionsCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 12:
                            ReportsPanel.Instance.Filters.cards = this.cardsCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 13:
                            ReportsPanel.Instance.Filters.achievements = this.achievementsCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 14:
                            ReportsPanel.Instance.Filters.buyVillages = this.buyVillagesCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 15:
                            ReportsPanel.Instance.Filters.quests = this.questsCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 0x10:
                            ReportsPanel.Instance.Filters.spins = this.spinsCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            return;

                        case 0x11:
                            ReportsPanel.Instance.Filters.house = this.houseCheck.Checked;
                            ReportsPanel.Instance.updateFilters();
                            break;

                        default:
                            return;
                    }
                }
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

        public void init(int mode, ReportCapturePopup parent)
        {
            this.m_mode = mode;
            base.clearControls();
            this.backgroundImage.Image = (Image) GFXLibrary.popup_background_01;
            this.backgroundImage.Position = new Point(0, 0);
            base.addControl(this.backgroundImage);
            bool flag = false;
            if (mode == 0)
            {
                this.captureLabel.Text = SK.Text("Report_Capturing", "Report Capturing");
                flag = true;
            }
            else
            {
                this.captureLabel.Text = SK.Text("Report_Filtering", "Report Filtering");
            }
            this.captureLabel.Color = ARGBColors.White;
            this.captureLabel.Position = new Point(13, 7);
            this.captureLabel.Size = new Size(0x14f, 20);
            this.captureLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.captureLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.backgroundImage.addControl(this.captureLabel);
            this.okButton.ImageNorm = (Image) GFXLibrary.button_blue_01_normal;
            this.okButton.ImageOver = (Image) GFXLibrary.button_blue_01_over;
            this.okButton.ImageClick = (Image) GFXLibrary.button_blue_01_in;
            this.okButton.Position = new Point(240, 0x145);
            this.okButton.Text.Text = SK.Text("GENERIC_OK", "OK");
            this.okButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.okButton.Text.Color = ARGBColors.Black;
            this.okButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.okClicked), "ReportCapturePanel_ok");
            this.backgroundImage.addControl(this.okButton);
            this.cancelButton.ImageNorm = (Image) GFXLibrary.button_blue_01_normal;
            this.cancelButton.ImageOver = (Image) GFXLibrary.button_blue_01_over;
            this.cancelButton.ImageClick = (Image) GFXLibrary.button_blue_01_in;
            this.cancelButton.Position = new Point(0x7c, 0x145);
            this.cancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.cancelButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.cancelButton.Text.Color = ARGBColors.Black;
            this.cancelButton.setClickDelegate(delegate {
                InterfaceMgr.Instance.closeReportCaptureWindow();
                InterfaceMgr.Instance.ParentForm.TopMost = true;
                InterfaceMgr.Instance.ParentForm.TopMost = false;
            }, "ReportCapturePanel_cancel");
            this.cancelButton.Visible = flag;
            this.backgroundImage.addControl(this.cancelButton);
            ReportFilterList reportFilters = null;
            if (flag)
            {
                reportFilters = RemoteServices.Instance.ReportFilters;
            }
            else
            {
                reportFilters = ReportsPanel.Instance.Filters;
            }
            int num = 0x19;
            int y = 0x37;
            int x = 0x2d;
            int num4 = 210;
            if (!flag)
            {
                y -= 12;
                num = 0x16;
            }
            if (Program.mySettings.LanguageIdent == "de")
            {
                x -= 20;
                num4 += 20;
            }
            this.attackCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.attackCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.attackCheck.Position = new Point(x, y);
            this.attackCheck.Checked = reportFilters.attacks;
            this.attackCheck.CBLabel.Text = SK.Text("ReportFilter_Attacks", "Attacks");
            this.attackCheck.CBLabel.Color = ARGBColors.Black;
            this.attackCheck.CBLabel.Position = new Point(20, -1);
            this.attackCheck.CBLabel.Size = new Size(170, 0x19);
            this.attackCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.attackCheck.Data = 0;
            this.attackCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.attackCheck);
            this.defenceCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.defenceCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.defenceCheck.Position = new Point(x, y + num);
            this.defenceCheck.Checked = reportFilters.defense;
            this.defenceCheck.CBLabel.Text = SK.Text("ReportFilter_Defense", "Defense");
            this.defenceCheck.CBLabel.Color = ARGBColors.Black;
            this.defenceCheck.CBLabel.Position = new Point(20, -1);
            this.defenceCheck.CBLabel.Size = new Size(170, 0x19);
            this.defenceCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.defenceCheck.Data = 1;
            this.defenceCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.defenceCheck);
            this.enemyCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.enemyCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.enemyCheck.Position = new Point(x, y + (2 * num));
            this.enemyCheck.Checked = reportFilters.enemyWarnings;
            this.enemyCheck.CBLabel.Text = SK.Text("ReportFilter_Enemy_Attacks", "Enemy Attacks");
            this.enemyCheck.CBLabel.Color = ARGBColors.Black;
            this.enemyCheck.CBLabel.Position = new Point(20, -1);
            this.enemyCheck.CBLabel.Size = new Size(170, 0x19);
            this.enemyCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.enemyCheck.Data = 2;
            this.enemyCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.enemyCheck);
            this.reinforceCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.reinforceCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.reinforceCheck.Position = new Point(x, y + (3 * num));
            this.reinforceCheck.Checked = reportFilters.reinforcements;
            this.reinforceCheck.CBLabel.Text = SK.Text("ReportFilter_Reinforcements", "Reinforcements");
            this.reinforceCheck.CBLabel.Color = ARGBColors.Black;
            this.reinforceCheck.CBLabel.Position = new Point(20, -1);
            this.reinforceCheck.CBLabel.Size = new Size(190, 0x19);
            this.reinforceCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.reinforceCheck.Data = 3;
            this.reinforceCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.reinforceCheck);
            this.scoutingCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.scoutingCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.scoutingCheck.Position = new Point(x, y + (4 * num));
            this.scoutingCheck.Checked = reportFilters.scouting;
            this.scoutingCheck.CBLabel.Text = SK.Text("ReportFilter_Scouting", "Scouting");
            this.scoutingCheck.CBLabel.Color = ARGBColors.Black;
            this.scoutingCheck.CBLabel.Position = new Point(20, -1);
            this.scoutingCheck.CBLabel.Size = new Size(170, 0x19);
            if (Program.mySettings.LanguageIdent == "pt")
            {
                this.scoutingCheck.CBLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            }
            else
            {
                this.scoutingCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            }
            this.scoutingCheck.Data = 4;
            this.scoutingCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.scoutingCheck);
            this.foragingCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.foragingCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.foragingCheck.Position = new Point(x, y + (5 * num));
            this.foragingCheck.Checked = reportFilters.foraging;
            this.foragingCheck.CBLabel.Text = SK.Text("ReportFilter_Foraging", "Foraging");
            this.foragingCheck.CBLabel.Color = ARGBColors.Black;
            this.foragingCheck.CBLabel.Position = new Point(20, -1);
            this.foragingCheck.CBLabel.Size = new Size(170, 0x19);
            this.foragingCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.foragingCheck.Data = 5;
            this.foragingCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.foragingCheck);
            this.tradeCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.tradeCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.tradeCheck.Position = new Point(x, y + (6 * num));
            this.tradeCheck.Checked = reportFilters.trade;
            this.tradeCheck.CBLabel.Text = SK.Text("ReportFilter_Trade", "Trade");
            this.tradeCheck.CBLabel.Color = ARGBColors.Black;
            this.tradeCheck.CBLabel.Position = new Point(20, -1);
            this.tradeCheck.CBLabel.Size = new Size(170, 0x19);
            this.tradeCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tradeCheck.Data = 6;
            this.tradeCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.tradeCheck);
            this.vassalsCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.vassalsCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.vassalsCheck.Position = new Point(num4, y);
            this.vassalsCheck.Checked = reportFilters.vassals;
            this.vassalsCheck.CBLabel.Text = SK.Text("ReportFilter_Vassals", "Vassals");
            this.vassalsCheck.CBLabel.Color = ARGBColors.Black;
            this.vassalsCheck.CBLabel.Position = new Point(20, -1);
            this.vassalsCheck.CBLabel.Size = new Size(170, 0x19);
            this.vassalsCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.vassalsCheck.Data = 7;
            this.vassalsCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.vassalsCheck);
            this.religionCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.religionCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.religionCheck.Position = new Point(num4, y + num);
            this.religionCheck.Checked = reportFilters.religion;
            this.religionCheck.CBLabel.Text = SK.Text("ReportFilter_Religion", "Religion");
            this.religionCheck.CBLabel.Color = ARGBColors.Black;
            this.religionCheck.CBLabel.Position = new Point(20, -1);
            this.religionCheck.CBLabel.Size = new Size(170, 0x19);
            this.religionCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.religionCheck.Data = 8;
            this.religionCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.religionCheck);
            this.researchCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.researchCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.researchCheck.Position = new Point(num4, y + (2 * num));
            this.researchCheck.Checked = reportFilters.research;
            this.researchCheck.CBLabel.Text = SK.Text("ReportFilter_Research", "Research");
            this.researchCheck.CBLabel.Color = ARGBColors.Black;
            this.researchCheck.CBLabel.Position = new Point(20, -1);
            this.researchCheck.CBLabel.Size = new Size(170, 0x19);
            this.researchCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.researchCheck.Data = 9;
            this.researchCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.researchCheck);
            this.electionsCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.electionsCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.electionsCheck.Position = new Point(num4, y + (3 * num));
            this.electionsCheck.Checked = reportFilters.elections;
            this.electionsCheck.CBLabel.Text = SK.Text("ReportFilter_Elections", "Elections");
            this.electionsCheck.CBLabel.Color = ARGBColors.Black;
            this.electionsCheck.CBLabel.Position = new Point(20, -1);
            this.electionsCheck.CBLabel.Size = new Size(170, 0x19);
            this.electionsCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.electionsCheck.Data = 10;
            this.electionsCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.electionsCheck);
            this.factionsCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.factionsCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.factionsCheck.Position = new Point(num4, y + (4 * num));
            this.factionsCheck.Checked = reportFilters.factions;
            this.factionsCheck.CBLabel.Text = SK.Text("ReportFilter_Factions", "Factions");
            this.factionsCheck.CBLabel.Color = ARGBColors.Black;
            this.factionsCheck.CBLabel.Position = new Point(20, -1);
            this.factionsCheck.CBLabel.Size = new Size(170, 0x19);
            this.factionsCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.factionsCheck.Data = 11;
            this.factionsCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.factionsCheck);
            this.cardsCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.cardsCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.cardsCheck.Position = new Point(num4, y + (5 * num));
            this.cardsCheck.Checked = reportFilters.cards;
            this.cardsCheck.CBLabel.Text = SK.Text("ReportFilter_Cards", "Cards");
            this.cardsCheck.CBLabel.Color = ARGBColors.Black;
            this.cardsCheck.CBLabel.Position = new Point(20, -1);
            this.cardsCheck.CBLabel.Size = new Size(170, 0x19);
            this.cardsCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.cardsCheck.Data = 12;
            this.cardsCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.cardsCheck);
            this.achievementsCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.achievementsCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.achievementsCheck.Position = new Point(num4, y + (6 * num));
            this.achievementsCheck.Checked = reportFilters.achievements;
            this.achievementsCheck.CBLabel.Text = SK.Text("GENERIC_Achievements", "Achievements");
            this.achievementsCheck.CBLabel.Color = ARGBColors.Black;
            this.achievementsCheck.CBLabel.Position = new Point(20, -1);
            this.achievementsCheck.CBLabel.Size = new Size(170, 0x19);
            this.achievementsCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.achievementsCheck.Data = 13;
            this.achievementsCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.achievementsCheck);
            this.buyVillagesCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.buyVillagesCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.buyVillagesCheck.Position = new Point(x, y + (7 * num));
            this.buyVillagesCheck.Checked = reportFilters.buyVillages;
            this.buyVillagesCheck.CBLabel.Text = SK.Text("ReportFilter_Village_Charter", "Village Charter");
            this.buyVillagesCheck.CBLabel.Color = ARGBColors.Black;
            this.buyVillagesCheck.CBLabel.Position = new Point(20, -1);
            this.buyVillagesCheck.CBLabel.Size = new Size(170, 0x19);
            if (Program.mySettings.LanguageIdent == "it")
            {
                this.buyVillagesCheck.CBLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            }
            else
            {
                this.buyVillagesCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            }
            this.buyVillagesCheck.Data = 14;
            this.buyVillagesCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.buyVillagesCheck);
            this.questsCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.questsCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.questsCheck.Position = new Point(num4, y + (7 * num));
            this.questsCheck.Checked = reportFilters.quests;
            this.questsCheck.CBLabel.Text = SK.Text("GENERIC_Quests", "Quests");
            this.questsCheck.CBLabel.Color = ARGBColors.Black;
            this.questsCheck.CBLabel.Position = new Point(20, -1);
            this.questsCheck.CBLabel.Size = new Size(170, 0x19);
            this.questsCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.questsCheck.Data = 15;
            this.questsCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.questsCheck);
            this.capitalAttackCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.capitalAttackCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.capitalAttackCheck.Position = new Point(x, y + (8 * num));
            this.capitalAttackCheck.Checked = ReportsPanel.Instance.ShowParishAttacks;
            this.capitalAttackCheck.CBLabel.Text = SK.Text("ReportFilter_Capital_Attacks", "Capital Attacks");
            this.capitalAttackCheck.CBLabel.Color = ARGBColors.Black;
            this.capitalAttackCheck.CBLabel.Position = new Point(20, -1);
            this.capitalAttackCheck.CBLabel.Size = new Size(170, 0x19);
            this.capitalAttackCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.capitalAttackCheck.Data = -2;
            this.capitalAttackCheck.Visible = !flag;
            this.capitalAttackCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.capitalAttackCheck);
            this.spinsCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.spinsCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.spinsCheck.Position = new Point(num4, y + (8 * num));
            this.spinsCheck.Checked = reportFilters.spins;
            this.spinsCheck.CBLabel.Text = SK.Text("GENERIC_Wheel_Spins", "Wheel Spins");
            this.spinsCheck.CBLabel.Color = ARGBColors.Black;
            this.spinsCheck.CBLabel.Position = new Point(20, -1);
            this.spinsCheck.CBLabel.Size = new Size(170, 0x19);
            this.spinsCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.spinsCheck.Data = 0x10;
            this.spinsCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.spinsCheck);
            int num5 = flag ? 8 : 9;
            this.houseCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.houseCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.houseCheck.Position = new Point(x, y + (num5 * num));
            this.houseCheck.Checked = reportFilters.house;
            this.houseCheck.CBLabel.Text = SK.Text("ReportFilter_House", "House");
            this.houseCheck.CBLabel.Color = ARGBColors.Black;
            this.houseCheck.CBLabel.Position = new Point(20, -1);
            this.houseCheck.CBLabel.Size = new Size(170, 0x19);
            this.houseCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.houseCheck.Data = 0x11;
            this.houseCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.houseCheck);
            this.villageLostCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.villageLostCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.villageLostCheck.Position = new Point(num4, y + (9 * num));
            this.villageLostCheck.Checked = ReportsPanel.Instance.ShowVillageLost;
            this.villageLostCheck.CBLabel.Text = SK.Text("Reports_VillageLost", "Village Lost");
            this.villageLostCheck.CBLabel.Color = ARGBColors.Black;
            this.villageLostCheck.CBLabel.Position = new Point(20, -1);
            this.villageLostCheck.CBLabel.Size = new Size(170, 0x19);
            this.villageLostCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.villageLostCheck.Data = -4;
            this.villageLostCheck.Visible = !flag;
            this.villageLostCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundImage.addControl(this.villageLostCheck);
            CustomSelfDrawPanel.CSDButton control = new CustomSelfDrawPanel.CSDButton {
                ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal,
                ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over,
                ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed,
                Position = new Point(30, 270)
            };
            control.Text.Text = SK.Text("ReportFilter_Select_All", "Select All");
            control.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            control.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            control.TextYOffset = -3;
            control.Text.Color = ARGBColors.Black;
            control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectAllClicked), "ReportCapturePanel_select_all");
            control.Visible = !flag;
            this.backgroundImage.addControl(control);
            CustomSelfDrawPanel.CSDButton button2 = new CustomSelfDrawPanel.CSDButton {
                ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal,
                ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over,
                ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed,
                Position = new Point(0xc0, 270)
            };
            button2.Text.Text = SK.Text("ReportFilter_Select_None", "Select None");
            button2.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            button2.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            button2.TextYOffset = -3;
            button2.Text.Color = ARGBColors.Black;
            button2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectNoneClicked), "ReportCapturePanel_select_none");
            button2.Visible = !flag;
            this.backgroundImage.addControl(button2);
            this.readMessagesCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.readMessagesCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            if (Program.mySettings.LanguageIdent == "pl")
            {
                this.readMessagesCheck.Position = new Point(x - 20, 330);
            }
            else
            {
                this.readMessagesCheck.Position = new Point(x, 330);
            }
            this.readMessagesCheck.Checked = ReportsPanel.Instance.ShowReadMessages;
            this.readMessagesCheck.CBLabel.Text = SK.Text("ReportFilter_Show_Read_Messages", "Show Read Messages");
            this.readMessagesCheck.CBLabel.Color = ARGBColors.Black;
            if (Program.mySettings.LanguageIdent == "de")
            {
                this.readMessagesCheck.CBLabel.Position = new Point(10, -1);
            }
            else
            {
                this.readMessagesCheck.CBLabel.Position = new Point(20, -1);
            }
            this.readMessagesCheck.CBLabel.Size = new Size(310, 0x19);
            this.readMessagesCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.readMessagesCheck.Data = -1;
            this.readMessagesCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.readMessagesCheck.Visible = !flag;
            this.backgroundImage.addControl(this.readMessagesCheck);
            this.forwardedOnlyCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.forwardedOnlyCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            if (Program.mySettings.LanguageIdent == "pl")
            {
                this.forwardedOnlyCheck.Position = new Point(x - 20, 0x131);
            }
            else
            {
                this.forwardedOnlyCheck.Position = new Point(x, 0x131);
            }
            this.forwardedOnlyCheck.Checked = ReportsPanel.Instance.ShowForwardedMessagesOnly;
            this.forwardedOnlyCheck.CBLabel.Text = SK.Text("ReportFilter_Show_Forwarded_Only_Messages", "Show Forwarded Messages Only");
            this.forwardedOnlyCheck.CBLabel.Color = ARGBColors.Black;
            this.forwardedOnlyCheck.CBLabel.Position = new Point(20, -1);
            this.forwardedOnlyCheck.CBLabel.Size = new Size(310, 0x19);
            this.forwardedOnlyCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.forwardedOnlyCheck.Data = -3;
            this.forwardedOnlyCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.forwardedOnlyCheck.Visible = !flag;
            this.backgroundImage.addControl(this.forwardedOnlyCheck);
            parent.Size = this.backgroundImage.Size;
            base.Invalidate();
            parent.Invalidate();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.White;
            base.Name = "ReportCapturePanel";
            base.Size = new Size(600, 0x37);
            base.ResumeLayout(false);
        }

        public void okClicked()
        {
            if (this.m_mode == 1)
            {
                InterfaceMgr.Instance.closeReportCaptureWindow();
                InterfaceMgr.Instance.ParentForm.TopMost = true;
                InterfaceMgr.Instance.ParentForm.TopMost = false;
            }
            else
            {
                ReportFilterList reportFilters = RemoteServices.Instance.ReportFilters;
                bool flag = false;
                if (reportFilters.attacks != this.attackCheck.Checked)
                {
                    flag = true;
                    reportFilters.attacks = this.attackCheck.Checked;
                }
                if (reportFilters.defense != this.defenceCheck.Checked)
                {
                    flag = true;
                    reportFilters.defense = this.defenceCheck.Checked;
                }
                if (reportFilters.vassals != this.vassalsCheck.Checked)
                {
                    flag = true;
                    reportFilters.vassals = this.vassalsCheck.Checked;
                }
                if (reportFilters.reinforcements != this.reinforceCheck.Checked)
                {
                    flag = true;
                    reportFilters.reinforcements = this.reinforceCheck.Checked;
                }
                if (reportFilters.research != this.researchCheck.Checked)
                {
                    flag = true;
                    reportFilters.research = this.researchCheck.Checked;
                }
                if (reportFilters.scouting != this.scoutingCheck.Checked)
                {
                    flag = true;
                    reportFilters.scouting = this.scoutingCheck.Checked;
                }
                if (reportFilters.foraging != this.foragingCheck.Checked)
                {
                    flag = true;
                    reportFilters.foraging = this.foragingCheck.Checked;
                }
                if (reportFilters.elections != this.electionsCheck.Checked)
                {
                    flag = true;
                    reportFilters.elections = this.electionsCheck.Checked;
                }
                if (reportFilters.factions != this.factionsCheck.Checked)
                {
                    flag = true;
                    reportFilters.factions = this.factionsCheck.Checked;
                }
                if (reportFilters.religion != this.religionCheck.Checked)
                {
                    flag = true;
                    reportFilters.religion = this.religionCheck.Checked;
                }
                if (reportFilters.trade != this.tradeCheck.Checked)
                {
                    flag = true;
                    reportFilters.trade = this.tradeCheck.Checked;
                }
                if (reportFilters.cards != this.cardsCheck.Checked)
                {
                    flag = true;
                    reportFilters.cards = this.cardsCheck.Checked;
                }
                if (reportFilters.achievements != this.achievementsCheck.Checked)
                {
                    flag = true;
                    reportFilters.achievements = this.achievementsCheck.Checked;
                }
                if (reportFilters.buyVillages != this.buyVillagesCheck.Checked)
                {
                    flag = true;
                    reportFilters.buyVillages = this.buyVillagesCheck.Checked;
                }
                if (reportFilters.enemyWarnings != this.enemyCheck.Checked)
                {
                    flag = true;
                    reportFilters.enemyWarnings = this.enemyCheck.Checked;
                }
                if (reportFilters.quests != this.questsCheck.Checked)
                {
                    flag = true;
                    reportFilters.quests = this.questsCheck.Checked;
                }
                if (reportFilters.spins != this.spinsCheck.Checked)
                {
                    flag = true;
                    reportFilters.spins = this.spinsCheck.Checked;
                }
                if (reportFilters.house != this.houseCheck.Checked)
                {
                    flag = true;
                    reportFilters.house = this.houseCheck.Checked;
                }
                if (flag)
                {
                    RemoteServices.Instance.UpdateReportFilters(reportFilters);
                }
                InterfaceMgr.Instance.closeReportCaptureWindow();
                InterfaceMgr.Instance.ParentForm.TopMost = true;
                InterfaceMgr.Instance.ParentForm.TopMost = false;
            }
        }

        public void selectAllClicked()
        {
            if (this.m_mode == 1)
            {
                ReportsPanel.Instance.Filters.attacks = true;
                ReportsPanel.Instance.Filters.defense = true;
                ReportsPanel.Instance.Filters.enemyWarnings = true;
                ReportsPanel.Instance.Filters.reinforcements = true;
                ReportsPanel.Instance.Filters.scouting = true;
                ReportsPanel.Instance.Filters.foraging = true;
                ReportsPanel.Instance.Filters.trade = true;
                ReportsPanel.Instance.Filters.vassals = true;
                ReportsPanel.Instance.Filters.religion = true;
                ReportsPanel.Instance.Filters.research = true;
                ReportsPanel.Instance.Filters.elections = true;
                ReportsPanel.Instance.Filters.factions = true;
                ReportsPanel.Instance.Filters.cards = true;
                ReportsPanel.Instance.Filters.achievements = true;
                ReportsPanel.Instance.Filters.buyVillages = true;
                ReportsPanel.Instance.ShowParishAttacks = true;
                ReportsPanel.Instance.ShowForwardedMessagesOnly = false;
                ReportsPanel.Instance.ShowVillageLost = true;
                ReportsPanel.Instance.Filters.quests = true;
                ReportsPanel.Instance.Filters.spins = true;
                ReportsPanel.Instance.Filters.house = true;
                ReportsPanel.Instance.updateFilters();
                this.attackCheck.Checked = true;
                this.defenceCheck.Checked = true;
                this.enemyCheck.Checked = true;
                this.reinforceCheck.Checked = true;
                this.scoutingCheck.Checked = true;
                this.foragingCheck.Checked = true;
                this.tradeCheck.Checked = true;
                this.vassalsCheck.Checked = true;
                this.religionCheck.Checked = true;
                this.researchCheck.Checked = true;
                this.electionsCheck.Checked = true;
                this.factionsCheck.Checked = true;
                this.cardsCheck.Checked = true;
                this.achievementsCheck.Checked = true;
                this.buyVillagesCheck.Checked = true;
                this.capitalAttackCheck.Checked = true;
                this.forwardedOnlyCheck.Checked = false;
                this.villageLostCheck.Checked = true;
                this.questsCheck.Checked = true;
                this.spinsCheck.Checked = true;
                this.houseCheck.Checked = true;
            }
        }

        public void selectNoneClicked()
        {
            if (this.m_mode == 1)
            {
                ReportsPanel.Instance.Filters.attacks = false;
                ReportsPanel.Instance.Filters.defense = false;
                ReportsPanel.Instance.Filters.enemyWarnings = false;
                ReportsPanel.Instance.Filters.reinforcements = false;
                ReportsPanel.Instance.Filters.scouting = false;
                ReportsPanel.Instance.Filters.foraging = false;
                ReportsPanel.Instance.Filters.trade = false;
                ReportsPanel.Instance.Filters.vassals = false;
                ReportsPanel.Instance.Filters.religion = false;
                ReportsPanel.Instance.Filters.research = false;
                ReportsPanel.Instance.Filters.elections = false;
                ReportsPanel.Instance.Filters.factions = false;
                ReportsPanel.Instance.Filters.cards = false;
                ReportsPanel.Instance.Filters.achievements = false;
                ReportsPanel.Instance.Filters.buyVillages = false;
                ReportsPanel.Instance.ShowParishAttacks = false;
                ReportsPanel.Instance.ShowForwardedMessagesOnly = false;
                ReportsPanel.Instance.ShowVillageLost = false;
                ReportsPanel.Instance.Filters.quests = false;
                ReportsPanel.Instance.Filters.spins = false;
                ReportsPanel.Instance.Filters.house = false;
                ReportsPanel.Instance.updateFilters();
                this.attackCheck.Checked = false;
                this.defenceCheck.Checked = false;
                this.enemyCheck.Checked = false;
                this.reinforceCheck.Checked = false;
                this.scoutingCheck.Checked = false;
                this.foragingCheck.Checked = false;
                this.tradeCheck.Checked = false;
                this.vassalsCheck.Checked = false;
                this.religionCheck.Checked = false;
                this.researchCheck.Checked = false;
                this.electionsCheck.Checked = false;
                this.factionsCheck.Checked = false;
                this.cardsCheck.Checked = false;
                this.achievementsCheck.Checked = false;
                this.buyVillagesCheck.Checked = false;
                this.capitalAttackCheck.Checked = false;
                this.forwardedOnlyCheck.Checked = false;
                this.villageLostCheck.Checked = false;
                this.questsCheck.Checked = false;
                this.spinsCheck.Checked = false;
                this.houseCheck.Checked = false;
            }
        }

        public void update()
        {
        }
    }
}

