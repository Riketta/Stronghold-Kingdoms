namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class CastleMapBattlePanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton adminExportAllButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel attackTypeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea backgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage backPanelImage = new CustomSelfDrawPanel.CSDImage();
        private BattleResultPopup battleResultPopup;
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private DockableControl dockableControl;
        private static bool fast = false;
        public static bool fromReports = true;
        private CustomSelfDrawPanel.CSDButton heightButton = new CustomSelfDrawPanel.CSDButton();
        private static bool high = false;
        private static CastleMapBattlePanel2 Instance = null;
        private bool m_aiAttack;
        private bool m_attackerVictory;
        private int m_attackType = -1;
        private BattleTroopNumbers m_endingTroops;
        private GetReport_ReturnType m_reportReturnData;
        private BattleTroopNumbers m_startingTroops;
        private int m_villageID = -1;
        private CustomSelfDrawPanel.CSDButton pauseButton = new CustomSelfDrawPanel.CSDButton();
        private static bool paused = false;
        private CustomSelfDrawPanel.CSDColorBar pillageBar = new CustomSelfDrawPanel.CSDColorBar();
        private CustomSelfDrawPanel.CSDLabel pillageClockLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDColorBar reportBar = new CustomSelfDrawPanel.CSDColorBar();
        private CustomSelfDrawPanel.CSDLabel reportClockLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel reportHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private bool resultsMode;
        private CustomSelfDrawPanel.CSDButton speedButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel viewCastleHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel viewCastleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel viewTroopsHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel viewTroopsLabel = new CustomSelfDrawPanel.CSDLabel();

        public CastleMapBattlePanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
            Instance = this;
        }

        public void battleMode(bool realBattle, int attackType, bool aiAttack)
        {
            this.m_aiAttack = aiAttack;
            this.m_attackType = attackType;
            fromReports = true;
            paused = false;
            fast = false;
            high = false;
            this.resultsMode = false;
            this.updateButtons();
            this.speedButton.Visible = realBattle;
            this.pauseButton.Visible = realBattle;
            if (realBattle)
            {
                this.attackTypeLabel.Text = CastlesCommon.getAttackTypeLabel(attackType);
                if (this.attackTypeLabel.Text.Length == 0)
                {
                    this.attackTypeLabel.Text = SK.Text("GENERIC_Attacking", "Attacking");
                }
                if (MainMenuBar2.CastleCopyMode)
                {
                    this.adminExportAllButton.Visible = true;
                    paused = true;
                    this.pauseButton.Text.Text = SK.Text("CastleMapBattle_Resume", "Resume");
                    GameEngine.Instance.CastleBattle.pauseBattle();
                }
            }
            else if (attackType >= 0)
            {
                this.attackTypeLabel.Text = GameEngine.Instance.World.getVillageName(attackType);
            }
            else
            {
                switch (attackType)
                {
                    case -11:
                        this.attackTypeLabel.Text = SK.Text("GENERIC_Treasure_Castle", "Treasure Castle");
                        goto Label_0273;

                    case -10:
                        this.attackTypeLabel.Text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
                        goto Label_0273;

                    case -9:
                        this.attackTypeLabel.Text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
                        goto Label_0273;

                    case -8:
                        this.attackTypeLabel.Text = SK.Text("GENERIC_Wolfs_Castle", "Wolf's Castle");
                        goto Label_0273;

                    case -7:
                        this.attackTypeLabel.Text = SK.Text("GENERIC_Pigs_Castle", "Pig's Castle");
                        goto Label_0273;

                    case -6:
                        this.attackTypeLabel.Text = SK.Text("GENERIC_Snakes_Castle", "Snake's Castle");
                        goto Label_0273;

                    case -5:
                        this.attackTypeLabel.Text = SK.Text("GENERIC_Rats_Castle", "Rat's Castle");
                        goto Label_0273;

                    case -4:
                        this.attackTypeLabel.Text = SK.Text("GENERIC_An_Empty_Village", "An empty village");
                        goto Label_0273;

                    case -3:
                        this.attackTypeLabel.Text = SK.Text("GENERIC_Wolf_Camp", "Wolf Lair");
                        goto Label_0273;

                    case -2:
                        this.attackTypeLabel.Text = SK.Text("GENERIC_Bandit_Camp", "Bandit Camp");
                        goto Label_0273;
                }
                this.attackTypeLabel.Text = "";
            }
        Label_0273:
            this.viewCastleHeadingLabel.Visible = !realBattle;
            this.viewCastleLabel.Visible = !realBattle;
            this.viewTroopsHeadingLabel.Visible = !realBattle;
            this.viewTroopsLabel.Visible = !realBattle;
            if (!realBattle)
            {
                this.pillageClockLabel.Visible = false;
                this.pillageBar.Visible = false;
                this.reportBar.Visible = false;
                this.reportClockLabel.Visible = false;
                this.reportHeadingLabel.Visible = false;
            }
        }

        private void closeClick()
        {
            if (fromReports)
            {
                InterfaceMgr.Instance.getMainTabBar().changeTab(7);
            }
            else
            {
                InterfaceMgr.Instance.getMainTabBar().changeTab(0);
            }
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
        }

        public void closePopup(bool exit, bool tutorial)
        {
            if (exit)
            {
                this.closeClick();
            }
            else
            {
                this.resultsMode = true;
                this.speedButton.Visible = false;
                this.updateButtons();
            }
            if (tutorial)
            {
                PostTutorialWindow.CreatePostTutorialWindow(true);
            }
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        public void create()
        {
            this.initCastlePlacePanel();
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

        private void exportClick()
        {
            SaveFileDialog dialog = new SaveFileDialog {
                DefaultExt = "",
                Filter = "All Save Types (*.*)|*.*",
                Title = "Save Castle and attackers Data"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                GameEngine.Instance.CastleBattle.DEBUG_SaveCastleMap(dialog.FileName + ".cmap");
                GameEngine.Instance.CastleBattle.DEBUG_SaveAIWorldSetup(dialog.FileName + ".txt");
                GameEngine.Instance.CastleBattle.saveCamp(dialog.FileName + ".camp");
                GameEngine.Instance.CastleBattle.memoriseAttackSetup("Export_" + Path.GetFileNameWithoutExtension(dialog.FileName));
            }
        }

        public static void fromWorld()
        {
            Instance.fromWorldInst();
        }

        private void fromWorldInst()
        {
            fromReports = false;
            paused = false;
            fast = false;
            high = false;
            this.resultsMode = false;
            this.updateButtons();
        }

        public void initCastlePlacePanel()
        {
            this.backgroundArea.Position = new Point(0, 0);
            this.backgroundArea.Size = base.Size;
            base.addControl(this.backgroundArea);
            this.backPanelImage.Image = (Image) GFXLibrary.castlescreen_panelback_A;
            this.backPanelImage.Position = new Point(0, 0);
            this.backgroundArea.addControl(this.backPanelImage);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x99, 6);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CastleMapBattlePanel2_close");
            this.backgroundArea.addControl(this.closeButton);
            this.attackTypeLabel.Color = ARGBColors.Black;
            this.attackTypeLabel.Position = new Point(0, 0x21);
            this.attackTypeLabel.Size = new Size(this.backPanelImage.Width - 2, 0x18);
            this.attackTypeLabel.Text = SK.Text("GENERIC_Attack_Type", "Attack Type");
            this.attackTypeLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.attackTypeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.backgroundArea.addControl(this.attackTypeLabel);
            this.pillageClockLabel.Color = ARGBColors.Black;
            this.pillageClockLabel.Position = new Point(0, 30);
            this.pillageClockLabel.Size = new Size(this.backPanelImage.Width, 80);
            this.pillageClockLabel.Text = "0";
            this.pillageClockLabel.Visible = false;
            this.pillageClockLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
            this.pillageClockLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.backgroundArea.addControl(this.pillageClockLabel);
            this.pillageBar.setImages((Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right, (Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right);
            this.pillageBar.Number = 0.0;
            this.pillageBar.MaxValue = 1.0;
            this.pillageBar.Visible = false;
            this.pillageBar.SetMargin(2, 2, 2, 3);
            this.pillageBar.Position = new Point(0x15, 0x55);
            this.backgroundArea.addControl(this.pillageBar);
            this.pauseButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.pauseButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.pauseButton.Position = new Point(0x15, 0xc3);
            this.pauseButton.Text.Text = SK.Text("CastleMapBattle_Pause", "Pause");
            this.pauseButton.TextYOffset = 0;
            this.pauseButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.pauseButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.togglePauseClick), "CastleMapBattlePanel2_pause");
            this.backPanelImage.addControl(this.pauseButton);
            this.speedButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.speedButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.speedButton.Position = new Point(0x15, 0xf5);
            this.speedButton.Text.Text = SK.Text("CastleMapBattle_Fast", "Fast");
            this.speedButton.TextYOffset = 0;
            this.speedButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.speedButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggleSpeedClick), "CastleMapBattlePanel2_speed");
            this.backPanelImage.addControl(this.speedButton);
            this.heightButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_viewmode_normal;
            this.heightButton.ImageOver = (Image) GFXLibrary.r_building_miltary_viewmode_over;
            this.heightButton.ImageClick = (Image) GFXLibrary.r_building_miltary_viewmode_pushed;
            this.heightButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggleHeightClick));
            this.heightButton.Position = new Point(0x3a, 0x127);
            this.backPanelImage.addControl(this.heightButton);
            this.adminExportAllButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.adminExportAllButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.adminExportAllButton.Position = new Point(0x15, 0xaf);
            this.adminExportAllButton.Text.Text = "Export";
            this.adminExportAllButton.TextYOffset = 0;
            this.adminExportAllButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.adminExportAllButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.exportClick));
            this.adminExportAllButton.Visible = false;
            this.backPanelImage.addControl(this.adminExportAllButton);
            this.viewCastleHeadingLabel.Color = ARGBColors.Black;
            this.viewCastleHeadingLabel.Position = new Point(0, 0x49);
            this.viewCastleHeadingLabel.Size = new Size(this.backPanelImage.Width - 2, 0x2c);
            this.viewCastleHeadingLabel.Text = SK.Text("CastleMapBattle_Castle_Last_Update", "Castle Last Update");
            this.viewCastleHeadingLabel.Visible = false;
            this.viewCastleHeadingLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.viewCastleHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.backgroundArea.addControl(this.viewCastleHeadingLabel);
            this.viewCastleLabel.Color = ARGBColors.Black;
            this.viewCastleLabel.Position = new Point(0, 0x6c);
            this.viewCastleLabel.Size = new Size(this.backPanelImage.Width - 2, 0x18);
            this.viewCastleLabel.Text = "...";
            this.viewCastleLabel.Visible = false;
            this.viewCastleLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.viewCastleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.backgroundArea.addControl(this.viewCastleLabel);
            this.viewTroopsHeadingLabel.Color = ARGBColors.Black;
            this.viewTroopsHeadingLabel.Position = new Point(0, 0x85);
            this.viewTroopsHeadingLabel.Size = new Size(this.backPanelImage.Width - 2, 0x2c);
            this.viewTroopsHeadingLabel.Text = SK.Text("CastleMapBattle_Troops_Last_Update", "Troops Last Update");
            this.viewTroopsHeadingLabel.Visible = false;
            this.viewTroopsHeadingLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.viewTroopsHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.backgroundArea.addControl(this.viewTroopsHeadingLabel);
            this.viewTroopsLabel.Color = ARGBColors.Black;
            this.viewTroopsLabel.Position = new Point(0, 0xa8);
            this.viewTroopsLabel.Size = new Size(this.backPanelImage.Width - 2, 0x18);
            this.viewTroopsLabel.Text = "...";
            this.viewTroopsLabel.Visible = false;
            this.viewTroopsLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.viewTroopsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.backgroundArea.addControl(this.viewTroopsLabel);
            this.reportHeadingLabel.Color = ARGBColors.Black;
            this.reportHeadingLabel.Position = new Point(20, 0x65);
            this.reportHeadingLabel.Size = new Size((this.backPanelImage.Width - 2) - 40, 0x22);
            this.reportHeadingLabel.Text = SK.Text("CastleMapBattle_Report_Unavailable", "Report Unavailable To Attacker");
            this.reportHeadingLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.reportHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.backgroundArea.addControl(this.reportHeadingLabel);
            this.reportClockLabel.Color = ARGBColors.Black;
            this.reportClockLabel.Position = new Point(0, 0x6b);
            this.reportClockLabel.Size = new Size(this.backPanelImage.Width, 80);
            this.reportClockLabel.Text = "0";
            this.reportClockLabel.Visible = false;
            this.reportClockLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
            this.reportClockLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.backgroundArea.addControl(this.reportClockLabel);
            this.reportBar.setImages((Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right, (Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right);
            this.reportBar.Number = 0.0;
            this.reportBar.MaxValue = 1.0;
            this.reportBar.Visible = false;
            this.reportBar.SetMargin(2, 2, 2, 3);
            this.reportBar.Position = new Point(0x15, 160);
            this.backgroundArea.addControl(this.reportBar);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "CastleMapBattlePanel2";
            base.Size = new Size(0xc4, 0x236);
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

        public void setCastleReportClock(int reportClock, int reportClockMax)
        {
            if (this.m_aiAttack)
            {
                this.reportBar.Visible = false;
                this.reportClockLabel.Visible = false;
                this.reportHeadingLabel.Visible = false;
            }
            else
            {
                this.reportHeadingLabel.Visible = true;
                if (reportClock < 0)
                {
                    this.reportBar.Visible = false;
                    this.reportClockLabel.Visible = false;
                    this.reportHeadingLabel.Text = SK.Text("CastleMapBattle_Report_Available", "Report Available To Attacker");
                }
                else
                {
                    this.reportHeadingLabel.Text = SK.Text("CastleMapBattle_Report_Unavailable", "Report Unavailable To Attacker");
                    this.reportBar.Visible = true;
                    this.reportClockLabel.Visible = true;
                    this.reportBar.Number = reportClock;
                    this.reportClockLabel.Text = (reportClock / 10).ToString();
                    this.reportBar.MaxValue = reportClockMax;
                }
            }
        }

        public void setPillageClock(int pillageClock, int pillageClockMax)
        {
            if (pillageClock < 0)
            {
                this.pillageBar.Visible = false;
                this.pillageClockLabel.Visible = false;
            }
            else
            {
                this.pillageBar.Visible = true;
                this.pillageClockLabel.Visible = true;
                this.pillageBar.Number = pillageClock;
                this.pillageClockLabel.Text = (pillageClock / 10).ToString();
                this.pillageBar.MaxValue = pillageClockMax;
            }
        }

        public void setTimes(DateTime castleTime, DateTime troopTime)
        {
            this.reportHeadingLabel.Visible = false;
            this.pillageBar.Visible = false;
            this.pillageClockLabel.Visible = false;
            if (castleTime == DateTime.MaxValue)
            {
                this.viewCastleLabel.Visible = false;
                this.viewCastleHeadingLabel.Visible = false;
            }
            else if (castleTime == DateTime.MinValue)
            {
                this.viewCastleLabel.Text = SK.Text("CastleMapBattle_None_Available", "None Available");
            }
            else
            {
                this.viewCastleLabel.Text = castleTime.ToShortDateString() + ":" + castleTime.ToShortTimeString();
            }
            if (troopTime == DateTime.MaxValue)
            {
                this.viewTroopsLabel.Visible = false;
                this.viewTroopsHeadingLabel.Visible = false;
            }
            else if (troopTime == DateTime.MinValue)
            {
                this.viewTroopsLabel.Text = SK.Text("CastleMapBattle_None_Available", "None Available");
            }
            else
            {
                this.viewTroopsLabel.Text = troopTime.ToShortDateString() + ":" + troopTime.ToShortTimeString();
            }
        }

        public void ShowViewBattleResults(bool attackerVictory, BattleTroopNumbers startingTroops, BattleTroopNumbers endingTroops, int villageID, GetReport_ReturnType reportReturnData)
        {
            this.m_attackerVictory = attackerVictory;
            this.m_startingTroops = startingTroops;
            this.m_endingTroops = endingTroops;
            this.m_villageID = villageID;
            this.m_reportReturnData = reportReturnData;
            if (this.battleResultPopup != null)
            {
                if (this.battleResultPopup.Created)
                {
                    this.battleResultPopup.Close();
                }
                this.battleResultPopup = null;
            }
            this.battleResultPopup = new BattleResultPopup();
            this.battleResultPopup.init(attackerVictory, startingTroops, endingTroops, this.m_attackType, villageID, reportReturnData, this);
            if (attackerVictory)
            {
                if (GameEngine.Instance.World.isUserVillage(villageID))
                {
                    Sound.playBattleEndDefeatMusic();
                }
                else
                {
                    Sound.playBattleEndVictoryMusic();
                }
            }
            else if (GameEngine.Instance.World.isUserVillage(villageID))
            {
                Sound.playBattleEndVictoryMusic();
            }
            else
            {
                Sound.playBattleEndDefeatMusic();
            }
            Form parentForm = InterfaceMgr.Instance.ParentForm;
            Size size = parentForm.Size;
            size.Width -= this.battleResultPopup.Width;
            size.Height -= this.battleResultPopup.Height;
            Point location = parentForm.Location;
            this.battleResultPopup.Location = new Point(location.X + (size.Width / 2), location.Y + (size.Height / 2));
            this.battleResultPopup.Show(InterfaceMgr.Instance.ParentForm);
        }

        private void toggleHeightClick()
        {
            high = !high;
            if (GameEngine.Instance.CastleBattle != null)
            {
                GameEngine.Instance.CastleBattle.toggleHeight(high);
                if (high)
                {
                    GameEngine.Instance.playInterfaceSound("CastleMapBattlePanel2_height_high");
                }
                else
                {
                    GameEngine.Instance.playInterfaceSound("CastleMapBattlePanel2_height_low");
                }
            }
            this.updateButtons();
        }

        private void togglePauseClick()
        {
            if (!this.resultsMode)
            {
                paused = !paused;
                if (GameEngine.Instance.CastleBattle != null)
                {
                    if (paused)
                    {
                        GameEngine.Instance.CastleBattle.pauseBattle();
                    }
                    else
                    {
                        GameEngine.Instance.CastleBattle.unpauseBattle();
                    }
                }
            }
            else
            {
                this.ShowViewBattleResults(this.m_attackerVictory, this.m_startingTroops, this.m_endingTroops, this.m_villageID, this.m_reportReturnData);
            }
            this.updateButtons();
        }

        private void toggleSpeedClick()
        {
            if (!this.resultsMode)
            {
                fast = !fast;
                if (GameEngine.Instance.CastleBattle != null)
                {
                    GameEngine.Instance.CastleBattle.setFastPlayback(fast);
                }
            }
            this.updateButtons();
        }

        private void updateButtons()
        {
            if (!this.resultsMode)
            {
                if (paused)
                {
                    this.pauseButton.Text.Text = SK.Text("CastleMapBattle_Resume", "Resume");
                }
                else
                {
                    this.pauseButton.Text.Text = SK.Text("CastleMapBattle_Pause", "Pause");
                }
                if (!fast)
                {
                    this.speedButton.Text.Text = SK.Text("CastleMapBattle_Fast_Speed", "Fast Speed");
                }
                else
                {
                    this.speedButton.Text.Text = SK.Text("CastleMapBattle_Normal_Speed", "Normal Speed");
                }
            }
            else
            {
                this.pauseButton.Text.Text = SK.Text("CastleMapBattle_View_Report", "View Report");
            }
        }
    }
}

