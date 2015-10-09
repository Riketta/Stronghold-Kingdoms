namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class InterfaceMgr
    {
        private bool advancedCastleOptionsPopupClosing;
        private int attackTargetHomeVillage = -1;
        private AttackTargetSidePanel2 attackTargetSidePanel = new AttackTargetSidePanel2();
        private bool AttackTargetsPopupClosing;
        public static bool bgdBlurEnabled = false;
        private bool BPPopupWindowClosing;
        public int BuyOfferMultiple;
        private bool buyVillageWindowClosing;
        private CardBarDX cardBarDX = new CardBarDX();
        private CastleInfoBar2 castleInfoBar = new CastleInfoBar2();
        private CastleMapAttackerSetupPanel castleMapAttackerSetupPanel = new CastleMapAttackerSetupPanel();
        private CastleMapBattlePanel2 castleMapBattlePanel = new CastleMapBattlePanel2();
        private CastleMapPanel castleMapPanel = new CastleMapPanel();
        private ChatScreenManager chatScreenManager = new ChatScreenManager();
        private bool confirmBuyOfferPopupClosing;
        private bool confirmOpenPackPopupClosing;
        private bool confirmPlayCardPopupClosing;
        private bool connectionErrorWindowClosing;
        private CountryCapitalVillagePanel2 countryCapitalVillagePanel = new CountryCapitalVillagePanel2();
        private CountyCapitalVillagePanel2 countyCapitalVillagePanel = new CountyCapitalVillagePanel2();
        private int courtierHomeVillage = -1;
        private bool createPopupWindowClosing;
        private DominationWindow dominationWindow;
        private bool doUserInfoUpdate;
        private EmptyVillagePanel2 emptyVillagePanel = new EmptyVillagePanel2();
        private bool firstVillageBackgroundCall = true;
        private bool formationPopupClosing;
        private bool freeCardsPopupClosing;
        private GloryVictoryWindow gloryVictoryWindow;
        public bool ignoreStopDraw;
        private static InterfaceMgr instance = null;
        private DateTime lastStopDrawTime = DateTime.MinValue;
        private int lastTimeChangedMode = -1;
        private int lastViewedVillage = -1;
        private int lastVillageTab = -1;
        private bool launchAttackPopupClosing;
        private bool logoutWindowClosing;
        private AchievementPopup m_achievementPopup;
        private AdvancedCastleOptionsPopup m_advancedCastleOptionsPopup;
        private AttackTargetsPopup m_AttackTargetsPopup;
        private BPPopupWindow m_BPPopupWindow;
        private BuyVillagePopupWindow m_buyVillageWindow;
        private ConfirmBuyOfferPopup m_confirmBuyOfferPopup;
        private ConfirmOpenPackPopup m_confirmOpenPackPopup;
        private ConfirmPlayCardPopup m_confirmPlayCardPopup;
        private ConnectionErrorWindow m_connectionErrorWindow;
        private CreatePopupWindow m_createPopupWindow;
        private CustomTooltip m_currentCustomTooltip;
        private DonatePopup m_currentDonatePopup;
        private MenuPopup m_currentMenuPopup;
        private TutorialArrowWindow m_currentTutorialArrowWindow;
        private TutorialWindow m_currentTutorialWindow;
        private Size m_expandedMainSize;
        private string m_floatingInputString = "";
        private int m_floatingInputValue;
        private int m_forcedMenuVillage = -1;
        private FormationPopup m_formationPopup;
        private FreeCardsPopup m_freeCardsPopup;
        private bool m_greyLogin;
        private GreyOutWindow m_greyOutWindow;
        private SendArmyWindow m_launchAttackPopup;
        private LogoutOptionsWindow2 m_logoutOptionsWindow;
        private DateTime m_menuPopupClosedLastTime = DateTime.MinValue;
        private NewQuestRewardPopup m_newQuestRewardPopup;
        private int m_ownSelectedVillage = -1;
        private PlayCardsWindow m_playCardsWindow;
        private int m_reallySelectedVillage = -1;
        private ReportCapturePopup m_reportCapturePopup;
        private ScoutPopupWindow m_scoutPopupWindow;
        private int m_selectedMenuVillage = -1;
        private int m_selectedVassalVillage = -1;
        private SendMonkWindow m_sendMonkWindow;
        private VacationCancelPopupWindow m_VacationCancelPopupWindow;
        private WheelPopup m_WheelPopup;
        private WheelSelectPopup m_WheelSelectPopup;
        private WorldSelectPopupWindow m_worldSelectPopupWindow;
        private MailScreenManager mailScreenManager = new MailScreenManager();
        private MainWindowPanel mainWindowPanel = new MainWindowPanel();
        private MapFilterPanel2 mapFilterPanel = new MapFilterPanel2();
        private MapFilterSelectPanel mapFilterSelectPanel = new MapFilterSelectPanel();
        public long MapSelectedArmy = -1L;
        public long MapSelectedPerson = -1L;
        public long MapSelectedReinforcement = -1L;
        public long MapSelectedTrader = -1L;
        private MedalsPopupWindow medalsPopupPanel;
        private int monkSelectHomeVillage = -1;
        private MonkTargetSidePanel2 monkTargetSidePanel = new MonkTargetSidePanel2();
        private const int MRHP_POS = 6;
        private List<int> newAchievements = new List<int>();
        private bool newQuestRewardPopupClosing;
        private NewQuestsCompletedWindow newQuestsCompletedWindow;
        private List<int> nextAchievementIDs = new List<int>();
        public int OpenPackMultiple;
        private OtherVillagePanel2 otherVillagePanel = new OtherVillagePanel2();
        private const int OVERLAY_CONTEXT_HEIGHT = 0x1c;
        private OwnCountryCapitalPanel2 ownCountryCapitalPanel = new OwnCountryCapitalPanel2();
        private OwnCountyCapitalPanel2 ownCountyCapitalPanel = new OwnCountyCapitalPanel2();
        private OwnParishCapitalPanel2 ownParishCapitalPanel = new OwnParishCapitalPanel2();
        private OwnProvinceCapitalPanel2 ownProvinceCapitalPanel = new OwnProvinceCapitalPanel2();
        private OwnVillagePanel2 ownVillagePanel = new OwnVillagePanel2();
        private Form parentForm;
        private MainWindow parentMainWindow;
        private ParishCapitalVillagePanel2 parishCapitalVillagePanel = new ParishCapitalVillagePanel2();
        private PersonInfoPanel2 personInfoPanel = new PersonInfoPanel2();
        private bool playCardsWindowClosing;
        private ProvinceCapitalVillagePanel2 provinceCapitalVillagePanel = new ProvinceCapitalVillagePanel2();
        private ReinforcementTargetSidePanel2 reinforcementTargetSidePanel = new ReinforcementTargetSidePanel2();
        private bool reportCaptureWindowClosing;
        private ResearchPanel researchPanel = new ResearchPanel();
        private bool scoutPopupWindowClosing;
        private ScoutTargetSidePanel2 scoutTargetSidePanel = new ScoutTargetSidePanel2();
        private SelectArmyPanel2 selectArmyPanel = new SelectArmyPanel2();
        private SelectReinforcementPanel2 selectReinforcementPanel = new SelectReinforcementPanel2();
        private FloatingValueSent sendDelegate;
        private bool sendMonkWindowClosing;
        private FloatingTextSent sendTextDelegate;
        private int stockExchangeBuyingVillage = -1;
        private StockExchangeSidePanel2 stockExchangeSidePanel = new StockExchangeSidePanel2();
        private DateTime timeChangedToMode1 = DateTime.MinValue;
        private TraderInfoPanel2 traderInfoPanel = new TraderInfoPanel2();
        private TradeWithPanel2 tradeWithPanel = new TradeWithPanel2();
        public static int UIScale = 1;
        private UserInfoPanel2 userInfoPanel = new UserInfoPanel2();
        private int userInfoRefreshCountdown;
        private UserInfoScreen userInfoScreen = new UserInfoScreen();
        private bool VacationCancelPopupWindowClosing;
        private VassalAttackVillagePanel2 vassalAttackVillagePanel = new VassalAttackVillagePanel2();
        private int vassalSelectHomeVillage = -1;
        private VassalSelectSidePanel2 vassalSelectSidePanel = new VassalSelectSidePanel2();
        private VassalVillagePanel2 vassalVillagePanel = new VassalVillagePanel2();
        private VillageInfoBar2 villageInfoBar = new VillageInfoBar2();
        private VillageMapPanel villageMapPanel = new VillageMapPanel();
        private VillageReportBackgroundPanel villageReportBackgroundPanel = new VillageReportBackgroundPanel();
        private bool WheelPopupClosing;
        private bool WheelSelectPopupClosing;
        private int worldMapMode;
        private bool worldSelectPopupWindowClosing;

        public void activateAchievementPopup(int id)
        {
            if (this.m_achievementPopup != null)
            {
                if (this.m_achievementPopup.isActive())
                {
                    this.nextAchievementIDs.Add(id);
                    return;
                }
                this.m_achievementPopup = null;
            }
            this.m_achievementPopup = new AchievementPopup();
            this.m_achievementPopup.activate(id);
        }

        public void addMainMiniWindow(bool firstCall)
        {
            this.addMainMiniWindow(firstCall, false);
        }

        public void addMainMiniWindow(bool firstCall, bool overlayTabBar)
        {
            int num = 0;
            if (overlayTabBar)
            {
                num = 0x1c;
            }
            Size size = new Size(this.parentMainWindow.getDXBasePanel().Width, (this.parentForm.ClientSize.Height - 120) + num);
            this.mainWindowPanel.Size = size;
            this.getTopLeftMenu().Height = 120 - num;
            this.getTopRightMenu().Height = 120 - num;
            this.getTopLeftMenu().setContextBarVisible(!overlayTabBar);
            this.mainWindowPanel.Location = new Point(0, 120 - num);
            this.mainWindowPanel.doDraw(firstCall);
            if (!this.parentForm.Controls.Contains(this.mainWindowPanel))
            {
                this.parentForm.SuspendLayout();
                this.parentForm.Controls.Add(this.mainWindowPanel);
                this.mainWindowPanel.ResumeLayout(false);
                this.parentForm.ResumeLayout(false);
            }
            this.parentMainWindow.setMainWindowAreaVisible(false);
        }

        public void addMainWindow(bool allowBackgroundDraw, bool overlayTabBar)
        {
            int num = 0;
            if (overlayTabBar)
            {
                num = 0x1c;
            }
            Size size = new Size(this.m_expandedMainSize.Width, this.m_expandedMainSize.Height + num);
            this.getTopLeftMenu().Height = 120 - num;
            this.getTopRightMenu().Height = 120 - num;
            this.mainWindowPanel.Size = size;
            this.getTopLeftMenu().setContextBarVisible(!overlayTabBar);
            this.mainWindowPanel.Location = new Point(0, 120 - num);
            this.mainWindowPanel.BringToFront();
            this.mainWindowPanel.doDraw(allowBackgroundDraw);
            if (!this.parentForm.Controls.Contains(this.mainWindowPanel))
            {
                this.parentForm.SuspendLayout();
                this.parentForm.Controls.Add(this.mainWindowPanel);
                this.parentForm.ResumeLayout(false);
            }
            this.parentMainWindow.setMainAreaVisible(false);
        }

        public bool allowDrawCircles()
        {
            if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_WORLD)
            {
                int villageID = this.getSelectedMenuVillage();
                if (villageID < 0)
                {
                    return false;
                }
                if (!GameEngine.Instance.World.isUserVillage(villageID))
                {
                    return false;
                }
                if (GameEngine.Instance.World.isCapital(villageID))
                {
                    return false;
                }
            }
            return true;
        }

        public void capitalDonateResourcesInit(int villageID, VillageMapBuilding selectedBuilding)
        {
            this.villageReportBackgroundPanel.capitalDonateResourcesInit(villageID, selectedBuilding);
        }

        public void castle_ClearSelectedTroop()
        {
            this.castleMapPanel.clearSelectedTroop();
            this.castleMapAttackerSetupPanel.clearSelectedTroop();
        }

        public void castle_SetSelectedTroop(int numPeasants, int peasantsState, int numArchers, int archersState, int numPikemen, int pikemenState, int numSwordsmen, int swordsmenState, int numCaptains, int captainState)
        {
            this.castleMapPanel.setSelectedTroop(numPeasants, peasantsState, numArchers, archersState, numPikemen, pikemenState, numSwordsmen, swordsmenState, numCaptains, captainState);
        }

        public void castleAttack_SetSelectedTroop(int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numCaptains, int captainsCommand, int captainsData)
        {
            this.castleMapAttackerSetupPanel.setSelectedTroop(numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numCaptains, captainsCommand, captainsData);
        }

        public void castleAttackShowAttackReady(bool state)
        {
            this.castleMapAttackerSetupPanel.showAttackReady(state);
        }

        public void castleAttackShowRealAttack(bool state)
        {
            this.castleMapAttackerSetupPanel.showRealAttack(state);
        }

        public void castleBattleTimes(DateTime castleTime, DateTime troopTime)
        {
            this.castleMapBattlePanel.setTimes(castleTime, troopTime);
        }

        public void castleChanged()
        {
            if (GameEngine.Instance.Castle != null)
            {
                GameEngine.Instance.Castle.leaveMap();
            }
        }

        public void castleCommitReturn()
        {
            this.castleMapPanel.castleCommitReturn();
        }

        public void castleEndBuilderMode()
        {
            this.castleMapPanel.castleEndBuilderMode();
        }

        public void castleMapResizeWindow()
        {
        }

        public void castleShowPlacedAttackers(int numPlacedPeasants, int numPlacedArchers, int numPlacedPikemen, int numPlacedSwordsmen, int numPlacedCatapults, int maxPeasants, int maxArchers, int maxPikemen, int maxSwordsmen, int maxCatapults, int numCaptains, int maxCaptains, int captainsCommand, int numPeasantsInCastle, int numArchersInCastle, int numPikemenInCastle, int numSwordsmenInCastle)
        {
            this.castleMapAttackerSetupPanel.setStats(numPlacedArchers, numPlacedPikemen, numPlacedSwordsmen, numPlacedPeasants, numPlacedCatapults, maxPeasants, maxArchers, maxPikemen, maxSwordsmen, maxCatapults, numCaptains, maxCaptains, captainsCommand, numPeasantsInCastle, numArchersInCastle, numPikemenInCastle, numSwordsmenInCastle);
        }

        public void castleStartBuilderMode()
        {
            this.castleMapPanel.castleStartBuilderMode();
        }

        public void castleStopPlacing()
        {
            this.castleMapPanel.clearCastlePlaceInfo();
            this.castleMapPanel.stopDeleting();
        }

        public void centerOnVillage()
        {
            if (this.m_selectedMenuVillage >= 0)
            {
                this.selectUserVillage(this.m_selectedMenuVillage, true);
            }
        }

        public void changeTab(int tabID)
        {
            this.getMainTabBar().changeTab(tabID);
        }

        public void chatClose()
        {
            this.chatScreenManager.close(true, true);
            this.chatScreenManager.logout();
        }

        public void chatLogin()
        {
            this.chatScreenManager.login();
        }

        public void chatLogout()
        {
            this.chatScreenManager.logout();
        }

        public void chatSetBan(bool banned)
        {
            this.chatScreenManager.setChatBan(banned);
        }

        public void chatUpdate()
        {
            this.chatScreenManager.chatUpdate();
        }

        public void clearAndCloseUserInfo()
        {
            this.lastViewedVillage = -1;
            this.closeUserInfo();
        }

        public void clearControls()
        {
            this.clearControls(true, true, true, true);
        }

        public void clearControls(bool removeMainWindowPanel, bool removeVillageReportBackground, bool removePolitics, bool removeRightHandPanel)
        {
            this.doUserInfoUpdate = false;
            if (removeRightHandPanel)
            {
                this.userInfoPanel.closeControl(true);
                this.closeTraderInfoPanel();
                this.closeArmySelectedPanel();
                this.closeSelectedVillagePanel();
                this.closePersonInfoPanel();
                this.closeReinforcementSelectedPanel();
                this.getMainRightHandPanel().Controls.Clear();
            }
            this.mapFilterPanel.closeControl(true);
            this.researchPanel.closeControl(true);
            this.mailScreenManager.closeControl(true);
            this.chatScreenManager.closeControl(true);
            this.userInfoScreen.closeControl(true);
            this.mapFilterSelectPanel.closeControl(true);
            this.lastVillageTab = -1;
            this.villageReportBackgroundPanel.showPanel(-1);
            this.closeSendMonkWindow();
            this.m_selectedVassalVillage = -1;
            if (removeVillageReportBackground)
            {
                this.villageReportBackgroundPanel.closeControl(true);
            }
            if (removeMainWindowPanel)
            {
                this.getDXBasePanel().Controls.Remove(this.mainWindowPanel);
                this.parentForm.Controls.Remove(this.mainWindowPanel);
            }
            this.villageInfoBar.hide();
            this.castleInfoBar.hide();
            this.closeVillageTab();
            this.closeCastleTab();
            CapitalHelpBox.closeHelpBox();
            this.closeMedalsPopup();
            this.closeNewQuestsCompletedPopup();
            this.closeGloryVictoryWindowPopup();
        }

        public void clearControlsBetweenPolitics()
        {
            this.clearControls(false, true, false, true);
        }

        public void clearControlsLeaveRightHandPanel()
        {
            this.clearControls(true, true, true, false);
        }

        public void clearRightHandPanel_Special()
        {
            this.vassalVillagePanel.closeControl(true);
            this.vassalAttackVillagePanel.closeControl(true);
            this.monkTargetSidePanel.closeControl(true);
            this.attackTargetSidePanel.closeControl(true);
            this.reinforcementTargetSidePanel.closeControl(true);
            this.scoutTargetSidePanel.closeControl(true);
            this.selectArmyPanel.closeControl(true);
            this.selectReinforcementPanel.closeControl(true);
            this.traderInfoPanel.closeControl(true);
            this.tradeWithPanel.closeControl(true);
            this.vassalAttackVillagePanel.closeControl(true);
            this.vassalSelectSidePanel.closeControl(true);
            this.mapFilterPanel.closeControl(true);
        }

        public void clearStoredMail()
        {
            this.mailScreenManager.clearStoredMail();
        }

        public void clearVillageBuildingInfo()
        {
            this.villageMapPanel.clearBuildingInfo();
        }

        public bool clickDXCardBar(Point mousePos)
        {
            return this.cardBarDX.click(mousePos);
        }

        public void Close()
        {
            this.villageReportBackgroundPanel.clearAllReports();
            this.mailScreenManager.clearAllMail();
            this.chatScreenManager.close(true, true);
            this.clearControls();
            this.parentMainWindow = null;
            this.parentForm = null;
        }

        public void closeAchievementPopup()
        {
            if (this.m_achievementPopup != null)
            {
                this.closePopupWindow(this.m_achievementPopup);
                this.m_achievementPopup = null;
            }
        }

        public void closeAdvancedCastleOptionsPopup()
        {
            if (!this.advancedCastleOptionsPopupClosing)
            {
                this.advancedCastleOptionsPopupClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_advancedCastleOptionsPopup))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                }
                this.m_advancedCastleOptionsPopup = null;
                this.advancedCastleOptionsPopupClosing = false;
            }
        }

        public void closeAllPopups()
        {
            this.closeLaunchAttackPopup();
            this.closePlayCardsWindow();
            this.closeMenuPopup();
            this.closeCustomTooltip();
            this.closeTutorialWindow();
            this.closeAchievementPopup();
            this.closeScoutPopupWindow();
            this.closeSendMonkWindow();
            this.closeBuyVillagePopupWindow();
            this.closeDonatePopup();
            this.closeLogoutWindow();
            this.closeReportCaptureWindow();
            this.closeNewQuestRewardPopup();
            this.closeNewQuestsCompletedPopup();
            this.closeGloryVictoryWindowPopup();
            this.closeAdvancedCastleOptionsPopup();
            this.closeFreeCardsPopup();
            this.closeWheelPopup();
            this.closeWheelSelectPopup();
            this.closeConfirmPlayCardPopup();
            this.closeTutorialArrowWindow();
            this.closeMedalsPopup();
            this.closeConnectionErrorWindow();
            this.closeDominatonWindow();
            this.closeFormationPopup();
            this.closeAttackTargetsPopup();
        }

        public void closeArmySelectedPanel()
        {
            this.selectArmyPanel.closeControl(true);
            this.MapSelectedArmy = -1L;
        }

        public void closeAttackTargetsPopup()
        {
            if (!this.AttackTargetsPopupClosing)
            {
                this.AttackTargetsPopupClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_AttackTargetsPopup))
                {
                    GameEngine.Instance.EnableMouseClicks();
                }
                this.m_AttackTargetsPopup = null;
                this.AttackTargetsPopupClosing = false;
            }
        }

        public void closeBPPopupWindow()
        {
            if (!this.BPPopupWindowClosing)
            {
                this.BPPopupWindowClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_BPPopupWindow))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                    this.showDXCardBar(9);
                    Instance.closeParishPanel();
                }
                this.m_BPPopupWindow = null;
                this.BPPopupWindowClosing = false;
                Program.profileLogin.TopMost = true;
                Program.profileLogin.BringToFront();
                Program.profileLogin.TopMost = false;
            }
        }

        public void closeBuyVillagePopupWindow()
        {
            if (!this.buyVillageWindowClosing)
            {
                this.buyVillageWindowClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_buyVillageWindow))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                    this.showDXCardBar(9);
                }
                this.m_buyVillageWindow = null;
                this.buyVillageWindowClosing = false;
            }
        }

        public void closeCastleTab()
        {
            if (GameEngine.Instance.Castle != null)
            {
                GameEngine.Instance.Castle.leaveMap();
            }
        }

        public void closeConfirmBuyOfferPopup()
        {
            if (!this.confirmBuyOfferPopupClosing)
            {
                this.confirmBuyOfferPopupClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_confirmBuyOfferPopup))
                {
                    ((PlayCardsWindow) this.getCardWindow()).reactivatePanel();
                }
                this.m_confirmBuyOfferPopup = null;
                this.confirmBuyOfferPopupClosing = false;
            }
        }

        public void closeConfirmOpenPackPopup()
        {
            if (!this.confirmOpenPackPopupClosing)
            {
                this.confirmOpenPackPopupClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_confirmOpenPackPopup))
                {
                    ((PlayCardsWindow) this.getCardWindow()).reactivatePanel();
                }
                this.m_confirmOpenPackPopup = null;
                this.confirmOpenPackPopupClosing = false;
            }
        }

        public void closeConfirmPlayCardPopup()
        {
            if (!this.confirmPlayCardPopupClosing)
            {
                this.confirmPlayCardPopupClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_confirmPlayCardPopup) && (this.getCardWindow() != null))
                {
                    ((PlayCardsWindow) this.getCardWindow()).reactivatePanel();
                }
                this.m_confirmPlayCardPopup = null;
                this.confirmPlayCardPopupClosing = false;
            }
        }

        public void closeConnectionErrorWindow()
        {
            if (!this.connectionErrorWindowClosing)
            {
                this.connectionErrorWindowClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_connectionErrorWindow))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                    this.showDXCardBar(9);
                }
                this.m_connectionErrorWindow = null;
                this.connectionErrorWindowClosing = false;
            }
        }

        public void closeCreatePopupWindow()
        {
            if (!this.createPopupWindowClosing)
            {
                this.createPopupWindowClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_createPopupWindow))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                    this.showDXCardBar(9);
                    Instance.closeParishPanel();
                }
                this.m_createPopupWindow = null;
                this.createPopupWindowClosing = false;
                Program.profileLogin.TopMost = true;
                Program.profileLogin.BringToFront();
                Program.profileLogin.TopMost = false;
            }
        }

        public void closeCustomTooltip()
        {
            if (this.isPopupWindowOpen(this.m_currentCustomTooltip))
            {
                this.m_currentCustomTooltip.closing();
                this.m_currentCustomTooltip.Hide();
            }
        }

        public void closeDominatonWindow()
        {
            if (this.dominationWindow != null)
            {
                this.dominationWindow.Close();
                this.dominationWindow = null;
            }
        }

        public void closeDonatePopup()
        {
            if (this.isPopupWindowOpen(this.m_currentDonatePopup))
            {
                this.m_currentDonatePopup.Hide();
            }
        }

        public void closeFilterPanel()
        {
            this.mapFilterPanel.closeControl(true);
            this.showMapFilterSelectPanel(true, true);
        }

        public void closeFormationPopup()
        {
            if (!this.formationPopupClosing)
            {
                this.formationPopupClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_formationPopup))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                }
                this.m_formationPopup = null;
                this.formationPopupClosing = false;
            }
        }

        public void closeFreeCardsPopup()
        {
            if (!this.freeCardsPopupClosing)
            {
                this.freeCardsPopupClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_freeCardsPopup))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                }
                this.m_freeCardsPopup = null;
                this.freeCardsPopupClosing = false;
            }
        }

        public void closeGloryVictoryWindowPopup()
        {
            if (this.gloryVictoryWindow != null)
            {
                this.gloryVictoryWindow.Close();
                this.gloryVictoryWindow = null;
            }
        }

        public void closeGreyOut()
        {
            this.closePopupWindow(this.m_greyOutWindow);
            this.m_greyOutWindow = null;
        }

        public void closeLaunchAttackPopup()
        {
            if (!this.launchAttackPopupClosing)
            {
                this.launchAttackPopupClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_launchAttackPopup))
                {
                    this.closeGreyOut();
                }
                this.m_launchAttackPopup = null;
                this.launchAttackPopupClosing = false;
                if (this.parentForm != null)
                {
                    this.parentForm.TopMost = true;
                    this.parentForm.Focus();
                    this.parentForm.BringToFront();
                    this.parentForm.Focus();
                    this.parentForm.TopMost = false;
                }
            }
        }

        public void closeLogoutWindow()
        {
            if (!this.logoutWindowClosing)
            {
                this.logoutWindowClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_logoutOptionsWindow))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                }
                this.m_logoutOptionsWindow = null;
                this.logoutWindowClosing = false;
            }
        }

        public void closeMedalsPopup()
        {
            if (this.medalsPopupPanel != null)
            {
                this.medalsPopupPanel.Close();
                this.medalsPopupPanel = null;
            }
        }

        public void closeMenuPopup()
        {
            MainWindow.captureCloseMenuEvent = false;
            if (this.m_currentMenuPopup != null)
            {
                this.closePopupWindow(this.m_currentMenuPopup);
                this.m_currentMenuPopup = null;
                this.m_menuPopupClosedLastTime = DateTime.Now;
            }
        }

        public void closeMonksPanel()
        {
        }

        public void closeNewQuestRewardPopup()
        {
            if (!this.newQuestRewardPopupClosing)
            {
                this.newQuestRewardPopupClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_newQuestRewardPopup))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                }
                this.m_newQuestRewardPopup = null;
                this.newQuestRewardPopupClosing = false;
            }
        }

        public void closeNewQuestsCompletedPopup()
        {
            if (this.newQuestsCompletedWindow != null)
            {
                this.newQuestsCompletedWindow.Close();
                this.newQuestsCompletedWindow = null;
            }
        }

        public void closeParishPanel()
        {
            this.clearControlsLeaveRightHandPanel();
            this.getTopRightMenu().showVillageTab(false);
            this.getTopRightMenu().showFactionTabBar(false);
            this.worldMapMode = 0;
            this.parentMainWindow.setMainAreaVisible(true);
            this.showMapFilterSelectPanel(true, true);
        }

        public void closePersonInfoPanel()
        {
            this.personInfoPanel.closeControl(true);
            this.MapSelectedPerson = -1L;
        }

        public void closePlayCardsWindow()
        {
            if (!this.playCardsWindowClosing)
            {
                this.playCardsWindowClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_playCardsWindow))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    if (((this.getScoutPopupWindow() == null) && (this.getSendMonkWindow() == null)) && ((this.getBuyVillageWindow() == null) && (this.m_launchAttackPopup == null)))
                    {
                        this.closeGreyOut();
                    }
                }
                this.m_playCardsWindow = null;
                this.playCardsWindowClosing = false;
            }
        }

        private void closePopupWindow(Form window)
        {
            if ((window != null) && window.Created)
            {
                window.Close();
            }
        }

        public void closeReinforcementSelectedPanel()
        {
            this.selectReinforcementPanel.closeControl(true);
            this.MapSelectedReinforcement = -1L;
        }

        public void closeReportCaptureWindow()
        {
            if (!this.reportCaptureWindowClosing)
            {
                this.reportCaptureWindowClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_reportCapturePopup))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                }
                this.m_reportCapturePopup = null;
                this.reportCaptureWindowClosing = false;
            }
        }

        public void closeScoutPopupWindow()
        {
            if (!this.scoutPopupWindowClosing)
            {
                this.scoutPopupWindowClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_scoutPopupWindow))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                    this.showDXCardBar(9);
                    Instance.closeParishPanel();
                }
                this.m_scoutPopupWindow = null;
                this.scoutPopupWindowClosing = false;
            }
        }

        public void closeSelectedVillagePanel()
        {
            this.closeUserInfo();
            this.emptyVillagePanel.closeControl(true);
            this.ownVillagePanel.closeControl(true);
            this.ownParishCapitalPanel.closeControl(true);
            this.ownCountyCapitalPanel.closeControl(true);
            this.ownProvinceCapitalPanel.closeControl(true);
            this.ownCountryCapitalPanel.closeControl(true);
            this.otherVillagePanel.closeControl(true);
            this.parishCapitalVillagePanel.closeControl(true);
            this.countyCapitalVillagePanel.closeControl(true);
            this.provinceCapitalVillagePanel.closeControl(true);
            this.countryCapitalVillagePanel.closeControl(true);
            this.vassalVillagePanel.closeControl(true);
            this.vassalAttackVillagePanel.closeControl(true);
            this.monkTargetSidePanel.closeControl(true);
            this.attackTargetSidePanel.closeControl(true);
            this.reinforcementTargetSidePanel.closeControl(true);
            this.scoutTargetSidePanel.closeControl(true);
            this.selectArmyPanel.closeControl(true);
            this.selectReinforcementPanel.closeControl(true);
            this.traderInfoPanel.closeControl(true);
            this.tradeWithPanel.closeControl(true);
            this.vassalAttackVillagePanel.closeControl(true);
            this.vassalSelectSidePanel.closeControl(true);
            this.m_reallySelectedVillage = -1;
        }

        public void closeSelectedVillagePanelButNotSelect()
        {
            this.closeUserInfo();
            this.emptyVillagePanel.closeControl(true);
            this.ownVillagePanel.closeControl(true);
            this.ownParishCapitalPanel.closeControl(true);
            this.ownCountyCapitalPanel.closeControl(true);
            this.ownProvinceCapitalPanel.closeControl(true);
            this.ownCountryCapitalPanel.closeControl(true);
            this.otherVillagePanel.closeControl(true);
            this.parishCapitalVillagePanel.closeControl(true);
            this.countyCapitalVillagePanel.closeControl(true);
            this.provinceCapitalVillagePanel.closeControl(true);
            this.countryCapitalVillagePanel.closeControl(true);
            this.vassalVillagePanel.closeControl(true);
            this.vassalAttackVillagePanel.closeControl(true);
            this.selectArmyPanel.closeControl(true);
            this.selectReinforcementPanel.closeControl(true);
            this.traderInfoPanel.closeControl(true);
            this.vassalAttackVillagePanel.closeControl(true);
        }

        public void closeSendMonkWindow()
        {
            if (!this.sendMonkWindowClosing)
            {
                this.sendMonkWindowClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_sendMonkWindow))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                    this.showDXCardBar(9);
                    Instance.closeParishPanel();
                }
                this.m_sendMonkWindow = null;
                this.sendMonkWindowClosing = false;
            }
        }

        public void closeTextInput(int inputValue)
        {
            this.m_floatingInputValue = inputValue;
            FloatingInput.close();
            if (this.sendDelegate != null)
            {
                this.sendDelegate(this.m_floatingInputValue);
                this.sendDelegate = null;
            }
        }

        public void closeTextStringInput(string inputValue)
        {
            this.m_floatingInputString = inputValue;
            FloatingInputText.close();
            if (this.sendTextDelegate != null)
            {
                this.sendTextDelegate(this.m_floatingInputString);
                this.sendTextDelegate = null;
            }
        }

        public void closeTraderInfoPanel()
        {
            this.traderInfoPanel.closeControl(true);
            this.MapSelectedTrader = -1L;
        }

        public void closeTutorialArrowWindow()
        {
            if (this.isPopupWindowOpen(this.m_currentTutorialArrowWindow))
            {
                this.m_currentTutorialArrowWindow.Hide();
            }
        }

        public void closeTutorialWindow()
        {
            if (this.isPopupWindowOpen(this.m_currentTutorialWindow))
            {
                this.m_currentTutorialWindow.closing();
                this.m_currentTutorialWindow.Hide();
            }
        }

        public void closeUserInfo()
        {
            this.userInfoPanel.closeControl(true);
        }

        public void closeVacationCancelPopupWindow()
        {
            if (!this.VacationCancelPopupWindowClosing)
            {
                this.VacationCancelPopupWindowClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_VacationCancelPopupWindow))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                    this.showDXCardBar(9);
                    Instance.closeParishPanel();
                }
                this.m_VacationCancelPopupWindow = null;
                this.VacationCancelPopupWindowClosing = false;
                Program.profileLogin.TopMost = true;
                Program.profileLogin.BringToFront();
                Program.profileLogin.TopMost = false;
            }
        }

        public void closeVillageTab()
        {
            if (GameEngine.Instance.Village != null)
            {
                GameEngine.Instance.Village.leaveMap();
            }
            VillageMap.closePopups();
        }

        public void closeWheelPopup()
        {
            if (!this.WheelPopupClosing)
            {
                WheelPanel.ClearInstance();
                this.WheelPopupClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_WheelPopup))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                }
                this.m_WheelPopup = null;
                this.WheelPopupClosing = false;
            }
        }

        public void closeWheelSelectPopup()
        {
            if (!this.WheelSelectPopupClosing)
            {
                WheelPanel.ClearInstance();
                this.WheelSelectPopupClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_WheelSelectPopup))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                }
                this.m_WheelSelectPopup = null;
                this.WheelSelectPopupClosing = false;
            }
        }

        public void closeWorldSelectPopupWindow()
        {
            if (!this.worldSelectPopupWindowClosing)
            {
                this.worldSelectPopupWindowClosing = true;
                if (this.isPopupWindowOpenAndClose(this.m_worldSelectPopupWindow))
                {
                    GameEngine.Instance.EnableMouseClicks();
                    this.closeGreyOut();
                    this.showDXCardBar(9);
                    Instance.closeParishPanel();
                }
                this.m_worldSelectPopupWindow = null;
                this.worldSelectPopupWindowClosing = false;
                Program.profileLogin.TopMost = true;
                Program.profileLogin.BringToFront();
                Program.profileLogin.TopMost = false;
            }
        }

        public void completeQuest(int quest)
        {
            this.villageReportBackgroundPanel.questPanelCompleteQuest(quest);
        }

        public bool deleteReport(long reportID)
        {
            return this.villageReportBackgroundPanel.queryDeleteReport(reportID);
        }

        public void deleteReportFolder(string folderName, int mode)
        {
            this.villageReportBackgroundPanel.deleteReportFolder(folderName, mode);
        }

        public void displayArmySelectPanel(long armyID)
        {
            this.MapSelectedArmy = armyID;
            if (!this.selectArmyPanel.isVisible())
            {
                this.selectArmyPanel.initProperties(true, "", null);
                this.selectArmyPanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                this.selectArmyPanel.init();
            }
            this.selectArmyPanel.armySelected(armyID);
        }

        public void displayAttackTargetSidepanel()
        {
            this.attackTargetSidePanel.initProperties(true, "", null);
            this.attackTargetSidePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
            this.attackTargetSidePanel.init();
        }

        public void displayMonkSelectSidePanel()
        {
            this.monkTargetSidePanel.initProperties(true, "", null);
            this.monkTargetSidePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
            this.monkTargetSidePanel.init();
        }

        public void displayPersonInfoPanel(long personID)
        {
            this.MapSelectedPerson = personID;
            if (!this.personInfoPanel.isVisible())
            {
                this.personInfoPanel.initProperties(true, "", null);
                this.personInfoPanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                this.personInfoPanel.init();
            }
            this.personInfoPanel.setPerson(personID);
        }

        public void displayReinforcementSelectPanel(long armyID)
        {
            this.MapSelectedReinforcement = armyID;
            if (!this.selectReinforcementPanel.isVisible())
            {
                this.selectReinforcementPanel.initProperties(true, "", null);
                this.selectReinforcementPanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                this.selectReinforcementPanel.init();
            }
            this.selectReinforcementPanel.reinforcementSelected(armyID);
        }

        public void displayReinforcementTargetSidepanel()
        {
            this.reinforcementTargetSidePanel.initProperties(true, "", null);
            this.reinforcementTargetSidePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
            this.reinforcementTargetSidePanel.init();
        }

        public void displayScoutTargetSidepanel()
        {
            this.scoutTargetSidePanel.initProperties(true, "", null);
            this.scoutTargetSidePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
            this.scoutTargetSidePanel.init();
        }

        public void displaySelectedVillagePanel(int villageID, bool doubleClick, bool doShowUserInfo, bool forceSelfClick, bool forceInactiveNonPlayer)
        {
            this.showMapFilterSelectPanel(true, true, true, false);
            this.clearRightHandPanel_Special();
            this.doUserInfoUpdate = false;
            this.userInfoRefreshCountdown = 5;
            bool flag = this.emptyVillagePanel.isVisible();
            bool flag2 = this.ownVillagePanel.isVisible();
            bool flag3 = this.ownParishCapitalPanel.isVisible();
            bool flag4 = this.ownCountyCapitalPanel.isVisible();
            bool flag5 = this.ownProvinceCapitalPanel.isVisible();
            bool flag6 = this.ownCountryCapitalPanel.isVisible();
            bool flag7 = this.otherVillagePanel.isVisible();
            bool flag8 = this.parishCapitalVillagePanel.isVisible();
            bool flag9 = this.countyCapitalVillagePanel.isVisible();
            bool flag10 = this.provinceCapitalVillagePanel.isVisible();
            bool flag11 = this.countryCapitalVillagePanel.isVisible();
            bool flag12 = this.vassalVillagePanel.isVisible();
            bool flag13 = this.vassalAttackVillagePanel.isVisible();
            bool flag14 = false;
            bool flag15 = false;
            bool flag16 = false;
            bool flag17 = false;
            bool flag18 = false;
            bool flag19 = false;
            bool flag20 = false;
            bool flag21 = false;
            bool flag22 = false;
            bool flag23 = false;
            bool flag24 = false;
            bool flag25 = false;
            bool flag26 = false;
            WorldMap.SpecialVillageCache specialData = null;
            if (!forceSelfClick && (villageID == this.getSelectedMenuVillage()))
            {
                forceSelfClick = true;
            }
            this.m_forcedMenuVillage = -1;
            bool flag27 = false;
            if ((RemoteServices.Instance.Admin && GameEngine.shiftPressed) && GameEngine.Instance.World.isCapital(villageID))
            {
                flag27 = true;
            }
            if (((forceSelfClick || doubleClick) && (GameEngine.Instance.World.isUserVillage(villageID) || flag27)) && !forceInactiveNonPlayer)
            {
                this.setVillageNameBar(villageID);
                if (!GameEngine.Instance.World.isCapital(villageID))
                {
                    GameEngine.Instance.MovedFromVillageID = villageID;
                    flag15 = true;
                    if (!this.ownVillagePanel.isVisible())
                    {
                        this.ownVillagePanel.initProperties(true, "", null);
                        this.ownVillagePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                        this.ownVillagePanel.init();
                    }
                    this.ownVillagePanel.updateOwnVillageText(villageID);
                    if (doubleClick)
                    {
                        Instance.getMainTabBar().changeTab(1);
                    }
                }
                else if (GameEngine.Instance.World.isRegionCapital(villageID))
                {
                    flag16 = true;
                    if (!this.ownParishCapitalPanel.isVisible())
                    {
                        this.ownParishCapitalPanel.initProperties(true, "", null);
                        this.ownParishCapitalPanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                        this.ownParishCapitalPanel.init();
                    }
                    this.ownParishCapitalPanel.updateOwnVillageText(villageID);
                    if (doubleClick)
                    {
                        if (flag27)
                        {
                            this.m_forcedMenuVillage = villageID;
                        }
                        this.m_reallySelectedVillage = villageID;
                        Instance.getMainTabBar().changeTab(2);
                    }
                }
                else if (GameEngine.Instance.World.isCountyCapital(villageID))
                {
                    flag17 = true;
                    if (!this.ownCountyCapitalPanel.isVisible())
                    {
                        this.ownCountyCapitalPanel.initProperties(true, "", null);
                        this.ownCountyCapitalPanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                        this.ownCountyCapitalPanel.init();
                    }
                    this.ownCountyCapitalPanel.updateOwnVillageText(villageID);
                    if (doubleClick)
                    {
                        if (flag27)
                        {
                            this.m_forcedMenuVillage = villageID;
                        }
                        this.m_reallySelectedVillage = villageID;
                        Instance.getMainTabBar().changeTab(2);
                    }
                }
                else if (GameEngine.Instance.World.isProvinceCapital(villageID))
                {
                    flag18 = true;
                    if (!this.ownProvinceCapitalPanel.isVisible())
                    {
                        this.ownProvinceCapitalPanel.initProperties(true, "", null);
                        this.ownProvinceCapitalPanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                        this.ownProvinceCapitalPanel.init();
                    }
                    this.ownProvinceCapitalPanel.updateOwnVillageText(villageID);
                    if (doubleClick)
                    {
                        if (flag27)
                        {
                            this.m_forcedMenuVillage = villageID;
                        }
                        this.m_reallySelectedVillage = villageID;
                        Instance.getMainTabBar().changeTab(2);
                    }
                }
                else if (GameEngine.Instance.World.isCountryCapital(villageID))
                {
                    flag19 = true;
                    if (!this.ownCountryCapitalPanel.isVisible())
                    {
                        this.ownCountryCapitalPanel.initProperties(true, "", null);
                        this.ownCountryCapitalPanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                        this.ownCountryCapitalPanel.init();
                    }
                    this.ownCountryCapitalPanel.updateOwnVillageText(villageID);
                    if (doubleClick)
                    {
                        if (flag27)
                        {
                            this.m_forcedMenuVillage = villageID;
                        }
                        this.m_reallySelectedVillage = villageID;
                        Instance.getMainTabBar().changeTab(2);
                    }
                }
                this.m_selectedVassalVillage = -1;
            }
            else if (GameEngine.Instance.World.isCapital(villageID) && (this.m_selectedVassalVillage < 0))
            {
                if (GameEngine.Instance.World.isRegionCapital(villageID))
                {
                    flag21 = true;
                    if (!this.parishCapitalVillagePanel.isVisible())
                    {
                        this.parishCapitalVillagePanel.initProperties(true, "", null);
                        this.parishCapitalVillagePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                        this.parishCapitalVillagePanel.init();
                    }
                    this.parishCapitalVillagePanel.updateParishCapitalVillageText(villageID, this.OwnSelectedVillage);
                    if ((forceSelfClick || doubleClick) && GameEngine.Instance.World.isUserRelatedVillage(villageID))
                    {
                        this.m_ownSelectedVillage = -1;
                        this.setVillageNameBar(villageID);
                        GameEngine.Instance.MovedFromVillageID = villageID;
                        this.parishCapitalVillagePanel.updateParishCapitalVillageText(villageID, this.OwnSelectedVillage);
                        if (doubleClick)
                        {
                            this.m_reallySelectedVillage = villageID;
                            Instance.getMainTabBar().changeTab(2);
                        }
                    }
                }
                else if (GameEngine.Instance.World.isCountyCapital(villageID))
                {
                    flag22 = true;
                    if (!this.countyCapitalVillagePanel.isVisible())
                    {
                        this.countyCapitalVillagePanel.initProperties(true, "", null);
                        this.countyCapitalVillagePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                        this.countyCapitalVillagePanel.init();
                    }
                    this.countyCapitalVillagePanel.updateCountyCapitalVillageText(villageID, this.OwnSelectedVillage);
                    if ((forceSelfClick || doubleClick) && GameEngine.Instance.World.isUserRelatedVillage(villageID))
                    {
                        this.m_ownSelectedVillage = -1;
                        this.setVillageNameBar(villageID);
                        GameEngine.Instance.MovedFromVillageID = villageID;
                        this.countyCapitalVillagePanel.updateCountyCapitalVillageText(villageID, this.OwnSelectedVillage);
                        if (doubleClick)
                        {
                            this.m_reallySelectedVillage = villageID;
                            Instance.getMainTabBar().changeTab(2);
                        }
                    }
                }
                else if (GameEngine.Instance.World.isProvinceCapital(villageID))
                {
                    flag23 = true;
                    if (!this.provinceCapitalVillagePanel.isVisible())
                    {
                        this.provinceCapitalVillagePanel.initProperties(true, "", null);
                        this.provinceCapitalVillagePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                        this.provinceCapitalVillagePanel.init();
                    }
                    this.provinceCapitalVillagePanel.updateProvinceCapitalVillageText(villageID, this.OwnSelectedVillage);
                    if ((forceSelfClick || doubleClick) && GameEngine.Instance.World.isUserRelatedVillage(villageID))
                    {
                        this.m_ownSelectedVillage = -1;
                        this.setVillageNameBar(villageID);
                        GameEngine.Instance.MovedFromVillageID = villageID;
                        this.provinceCapitalVillagePanel.updateProvinceCapitalVillageText(villageID, this.OwnSelectedVillage);
                        if (doubleClick)
                        {
                            this.m_reallySelectedVillage = villageID;
                            Instance.getMainTabBar().changeTab(2);
                        }
                    }
                }
                else if (GameEngine.Instance.World.isCountryCapital(villageID))
                {
                    flag24 = true;
                    if (!this.countryCapitalVillagePanel.isVisible())
                    {
                        this.countryCapitalVillagePanel.initProperties(true, "", null);
                        this.countryCapitalVillagePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                        this.countryCapitalVillagePanel.init();
                    }
                    this.countryCapitalVillagePanel.updateCountryCapitalVillageText(villageID, this.OwnSelectedVillage);
                    if ((forceSelfClick || doubleClick) && GameEngine.Instance.World.isUserRelatedVillage(villageID))
                    {
                        this.m_ownSelectedVillage = -1;
                        this.setVillageNameBar(villageID);
                        GameEngine.Instance.MovedFromVillageID = villageID;
                        this.countryCapitalVillagePanel.updateCountryCapitalVillageText(villageID, this.OwnSelectedVillage);
                        if (doubleClick)
                        {
                            this.m_reallySelectedVillage = villageID;
                            Instance.getMainTabBar().changeTab(2);
                        }
                    }
                }
                this.m_reallySelectedVillage = villageID;
            }
            else
            {
                int num = GameEngine.Instance.World.getVillageUserID(villageID);
                if (GameEngine.Instance.LocalWorldData.AIWorld)
                {
                    switch (num)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            num = -1;
                            break;
                    }
                }
                if (num < 0)
                {
                    if (!forceInactiveNonPlayer && (this.m_selectedVassalVillage >= 0))
                    {
                        flag26 = true;
                        if (!this.vassalAttackVillagePanel.isVisible())
                        {
                            this.vassalAttackVillagePanel.initProperties(true, "", null);
                            this.vassalAttackVillagePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                            this.vassalAttackVillagePanel.init(villageID);
                        }
                        this.vassalAttackVillagePanel.updateOtherVillageText(villageID);
                    }
                    else
                    {
                        bool flag28 = false;
                        if (GameEngine.Instance.World.isSpecial(villageID))
                        {
                            flag28 = true;
                            specialData = GameEngine.Instance.World.getSpecialVillageData(villageID, true);
                        }
                        flag14 = true;
                        if (!this.emptyVillagePanel.isVisible())
                        {
                            this.emptyVillagePanel.initProperties(true, "", null);
                            this.emptyVillagePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                            this.emptyVillagePanel.init(villageID);
                        }
                        this.emptyVillagePanel.updateEmptyVillageText(villageID);
                        this.emptyVillagePanel.updateSpecialData(specialData);
                        if (forceInactiveNonPlayer && flag28)
                        {
                            this.emptyVillagePanel.forceDisable();
                        }
                    }
                }
                else if (!flag15)
                {
                    if (!forceInactiveNonPlayer && GameEngine.Instance.World.isVassal(this.m_ownSelectedVillage, villageID))
                    {
                        flag25 = true;
                        if (!this.vassalVillagePanel.isVisible())
                        {
                            this.vassalVillagePanel.initProperties(true, "", null);
                            this.vassalVillagePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                            this.vassalVillagePanel.init();
                        }
                        this.vassalVillagePanel.updateVassalVillageText(villageID);
                        if (this.m_selectedVassalVillage != villageID)
                        {
                            this.m_selectedVassalVillage = -1;
                        }
                    }
                    else if (!forceInactiveNonPlayer && (this.m_selectedVassalVillage >= 0))
                    {
                        flag26 = true;
                        if (!this.vassalAttackVillagePanel.isVisible())
                        {
                            this.vassalAttackVillagePanel.initProperties(true, "", null);
                            this.vassalAttackVillagePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                            this.vassalAttackVillagePanel.init(villageID);
                        }
                        this.vassalAttackVillagePanel.updateOtherVillageText(villageID);
                    }
                    else
                    {
                        flag20 = true;
                        if (!this.otherVillagePanel.isVisible())
                        {
                            this.otherVillagePanel.initProperties(true, "", null);
                            this.otherVillagePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                            this.otherVillagePanel.init();
                        }
                        this.otherVillagePanel.updateOtherVillageText(villageID);
                        if (forceInactiveNonPlayer)
                        {
                            this.otherVillagePanel.forceDisable();
                        }
                    }
                }
            }
            if (GameEngine.Instance.World.isSpecial(villageID))
            {
                if (!GameEngine.Instance.LocalWorldData.AIWorld)
                {
                    doShowUserInfo = false;
                    this.closeUserInfo();
                }
                else
                {
                    switch (GameEngine.Instance.World.getSpecial(villageID))
                    {
                        case 7:
                        case 9:
                        case 11:
                        case 13:
                            goto Label_0AC3;
                    }
                    doShowUserInfo = false;
                    this.closeUserInfo();
                }
            }
        Label_0AC3:
            if (doShowUserInfo)
            {
                this.doUserInfoUpdate = true;
                WorldMap.VillageRolloverInfo villageInfo = null;
                WorldMap.CachedUserInfo userInfo = null;
                if (villageID != this.lastViewedVillage)
                {
                    this.lastViewedVillage = villageID;
                    GameEngine.Instance.World.retrieveUserData(villageID, -1, ref villageInfo, ref userInfo, true, false);
                }
                else
                {
                    GameEngine.Instance.World.retrieveUserData(villageID, -1, ref villageInfo, ref userInfo, false, false);
                }
                if (userInfo != null)
                {
                    this.showUserInfo();
                    this.userInfoPanel.updateVillageInfo(villageInfo, userInfo);
                }
                else
                {
                    this.closeUserInfo();
                }
            }
            else
            {
                GameEngine.Instance.World.clearCachedVillageUserInfo();
            }
            if (flag && !flag14)
            {
                this.emptyVillagePanel.closeControl(true);
            }
            if (flag7 && !flag20)
            {
                this.otherVillagePanel.closeControl(true);
            }
            if (flag2 && !flag15)
            {
                this.ownVillagePanel.closeControl(true);
            }
            if (flag3 && !flag16)
            {
                this.ownParishCapitalPanel.closeControl(true);
            }
            if (flag4 && !flag17)
            {
                this.ownCountyCapitalPanel.closeControl(true);
            }
            if (flag5 && !flag18)
            {
                this.ownProvinceCapitalPanel.closeControl(true);
            }
            if (flag6 && !flag19)
            {
                this.ownCountryCapitalPanel.closeControl(true);
            }
            if (flag8 && !flag21)
            {
                this.parishCapitalVillagePanel.closeControl(true);
            }
            if (flag9 && !flag22)
            {
                this.countyCapitalVillagePanel.closeControl(true);
            }
            if (flag10 && !flag23)
            {
                this.provinceCapitalVillagePanel.closeControl(true);
            }
            if (flag11 && !flag24)
            {
                this.countryCapitalVillagePanel.closeControl(true);
            }
            if (flag12 && !flag25)
            {
                this.vassalVillagePanel.closeControl(true);
            }
            if (flag13 && !flag26)
            {
                this.vassalAttackVillagePanel.closeControl(true);
            }
            this.m_reallySelectedVillage = villageID;
            GameEngine.Instance.World.createTributeLinesList(villageID);
        }

        public void displayStockExchangeSidepanel()
        {
            this.stockExchangeSidePanel.initProperties(true, "", null);
            this.stockExchangeSidePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
            this.stockExchangeSidePanel.init();
        }

        public void displayTraderInfoPanel(long traderID)
        {
            this.MapSelectedTrader = traderID;
            if (!this.traderInfoPanel.isVisible())
            {
                this.traderInfoPanel.initProperties(true, "", null);
                this.traderInfoPanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
                this.traderInfoPanel.init();
            }
            this.traderInfoPanel.setTrader(traderID);
        }

        public void displayTradeWithPanel()
        {
            this.tradeWithPanel.initProperties(true, "", null);
            this.tradeWithPanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
            this.tradeWithPanel.init();
        }

        public void displayVassalSelectSidePanel()
        {
            this.vassalSelectSidePanel.initProperties(true, "", null);
            this.vassalSelectSidePanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
            this.vassalSelectSidePanel.init();
        }

        public void downCurrentFactionInfo()
        {
            CustomSelfDrawPanel.FactionPanelSideBar.downloadCurrentFactionInfo();
        }

        public void ensureInfoTabCleared()
        {
            if (this.villageInfoBar.isVisible())
            {
                this.villageInfoBar.hide();
            }
        }

        public void flushParishFrontPageInfo(int parishID)
        {
            this.villageReportBackgroundPanel.flushParishFrontPageInfo(parishID);
        }

        public Form getAdvancedCastleOptionsPopup()
        {
            return this.m_advancedCastleOptionsPopup;
        }

        public Form getAttackTargetsPopup()
        {
            return this.m_AttackTargetsPopup;
        }

        public BPPopupWindow getBPPopupWindow()
        {
            return this.m_BPPopupWindow;
        }

        public BuyVillagePopupWindow getBuyVillageWindow()
        {
            return this.m_buyVillageWindow;
        }

        public Form getCardWindow()
        {
            return this.m_playCardsWindow;
        }

        public ConfirmBuyOfferPopup getConfirmBuyOfferPopup()
        {
            return this.m_confirmBuyOfferPopup;
        }

        public ConfirmOpenPackPopup getConfirmOpenPackPopup()
        {
            return this.m_confirmOpenPackPopup;
        }

        public ConfirmPlayCardPopup getConfirmPlayCardPopup()
        {
            return this.m_confirmPlayCardPopup;
        }

        public ConnectionErrorWindow getConnectionErrorWindow()
        {
            return this.m_connectionErrorWindow;
        }

        public CreatePopupWindow getCreatePopupWindow()
        {
            return this.m_createPopupWindow;
        }

        public CustomTooltip getCustomTooltip()
        {
            return this.m_currentCustomTooltip;
        }

        public DonatePopup getDonatePopup()
        {
            return this.m_currentDonatePopup;
        }

        public DXPanel getDXBasePanel()
        {
            return this.parentMainWindow.getDXBasePanel();
        }

        public FactionTabBar2 getFactionTabBar()
        {
            return this.parentMainWindow.getFactionTabBar();
        }

        public Form getFormationPopup()
        {
            return this.m_formationPopup;
        }

        public FreeCardsPopup getFreeCardsPopup()
        {
            return this.m_freeCardsPopup;
        }

        public int getGameActivityMode()
        {
            if (this.parentForm == null)
            {
                this.lastTimeChangedMode = 2;
                return 3;
            }
            if (this.parentForm.WindowState == FormWindowState.Minimized)
            {
                this.lastTimeChangedMode = 1;
                return 5;
            }
            TimeSpan span = (TimeSpan) (DateTime.Now - GameEngine.Instance.lastMouseMoveTime);
            if (span.TotalSeconds > 180.0)
            {
                this.lastTimeChangedMode = 2;
                if (span.TotalMinutes > 25.0)
                {
                    return 4;
                }
                if (span.TotalMinutes > 15.0)
                {
                    return 3;
                }
                return 2;
            }
            Form activeForm = Form.ActiveForm;
            if (activeForm == null)
            {
                if (this.lastTimeChangedMode == 0)
                {
                    this.timeChangedToMode1 = DateTime.Now;
                }
                TimeSpan span2 = (TimeSpan) (DateTime.Now - this.timeChangedToMode1);
                if (span2.TotalMinutes > 10.0)
                {
                    return 2;
                }
                this.lastTimeChangedMode = 1;
                return 1;
            }
            if (activeForm == this.parentForm)
            {
                this.lastTimeChangedMode = 0;
                return 0;
            }
            for (Form form2 = this.parentForm.ParentForm; form2 != null; form2 = form2.ParentForm)
            {
                if (form2 == activeForm)
                {
                    this.lastTimeChangedMode = 0;
                    return 0;
                }
            }
            if (this.lastTimeChangedMode == 0)
            {
                this.timeChangedToMode1 = DateTime.Now;
            }
            TimeSpan span3 = (TimeSpan) (DateTime.Now - this.timeChangedToMode1);
            if (span3.TotalMinutes > 10.0)
            {
                return 2;
            }
            this.lastTimeChangedMode = 1;
            return 1;
        }

        public GreyOutWindow getGreyOutWindow()
        {
            return this.m_greyOutWindow;
        }

        public VillageMapBuilding getInBuildingBuilding()
        {
            return this.villageMapPanel.getInBuildingBuilding();
        }

        public Form getLogoutWindow()
        {
            return this.m_logoutOptionsWindow;
        }

        public MainMenuBar2 getMainMenuBar()
        {
            return this.parentMainWindow.getMainMenuBar();
        }

        public MainRightHandPanel getMainRightHandPanel()
        {
            return this.parentMainWindow.getMainRightHandPanel();
        }

        public MainTabBar2 getMainTabBar()
        {
            return this.parentMainWindow.getMainTabBar();
        }

        public Size getMainWindowSize()
        {
            return this.mainWindowPanel.Size;
        }

        public MenuPopup getMenuPopup()
        {
            return this.m_currentMenuPopup;
        }

        public Form getNewQuestRewardWindow()
        {
            return this.m_newQuestRewardPopup;
        }

        public string getPlagueText(int plagueLevel)
        {
            if (plagueLevel >= 0xb5)
            {
                return SK.Text("InterfaceMgr_Disease_10", "The Black Death");
            }
            if (plagueLevel >= 0xa1)
            {
                return SK.Text("InterfaceMgr_Disease_9", "Plague Symptoms");
            }
            if (plagueLevel >= 0x8d)
            {
                return SK.Text("InterfaceMgr_Disease_8", "Mass Delirium");
            }
            if (plagueLevel >= 0x79)
            {
                return SK.Text("InterfaceMgr_Disease_7", "Raging Fevers");
            }
            if (plagueLevel >= 0x65)
            {
                return SK.Text("InterfaceMgr_Disease_6", "Flu Epidemic");
            }
            if (plagueLevel >= 0x51)
            {
                return SK.Text("InterfaceMgr_Disease_5", "Flu Symptoms");
            }
            if (plagueLevel >= 0x3d)
            {
                return SK.Text("InterfaceMgr_Disease_4", "Bronchitis");
            }
            if (plagueLevel >= 0x29)
            {
                return SK.Text("InterfaceMgr_Disease_3", "Coughing");
            }
            if (plagueLevel >= 0x15)
            {
                return SK.Text("InterfaceMgr_Disease_2", "Colds");
            }
            return SK.Text("InterfaceMgr_Disease_1", "Slight Sniffles");
        }

        public Form getReportCaptureWindow()
        {
            return this.m_reportCapturePopup;
        }

        public object getReportData(long reportID)
        {
            return this.villageReportBackgroundPanel.getReportData(reportID);
        }

        public ScoutPopupWindow getScoutPopupWindow()
        {
            return this.m_scoutPopupWindow;
        }

        public int getSelectedMenuVillage()
        {
            if ((this.m_forcedMenuVillage >= 0) && RemoteServices.Instance.Admin)
            {
                return this.m_forcedMenuVillage;
            }
            if ((this.m_selectedMenuVillage >= 0) && (GameEngine.Instance.World.isUserVillage(this.m_selectedMenuVillage) || GameEngine.Instance.World.isUserRelatedVillage(this.m_selectedMenuVillage)))
            {
                return this.m_selectedMenuVillage;
            }
            int villageID = GameEngine.Instance.World.getNextUserVillage(-1, 1);
            this.setVillageNameBar(villageID);
            GameEngine.Instance.MovedFromVillageID = villageID;
            return villageID;
        }

        public SendMonkWindow getSendMonkWindow()
        {
            return this.m_sendMonkWindow;
        }

        public TopLeftMenu2 getTopLeftMenu()
        {
            return this.parentMainWindow.getTopLeftMenu();
        }

        public TopRightMenu getTopRightMenu()
        {
            return this.parentMainWindow.getTopRightMenu();
        }

        public TutorialArrowWindow getTutorialArrowWindow()
        {
            return this.m_currentTutorialArrowWindow;
        }

        public TutorialWindow getTutorialWindow()
        {
            return this.m_currentTutorialWindow;
        }

        public VacationCancelPopupWindow getVacationCancelPopupWindow()
        {
            return this.m_VacationCancelPopupWindow;
        }

        public int getVillageMapPanelBuildTabPos()
        {
            return this.villageMapPanel.TUTORIAL_getBuildTabYPos();
        }

        public int getVillageMapPanelHonourTabPos()
        {
            return this.villageMapPanel.calcInfoTabYPos();
        }

        public Point getVillageReportBackgroundLocation()
        {
            return this.villageReportBackgroundPanel.getLocation();
        }

        public VillageTabBar2 getVillageTabBar()
        {
            return this.parentMainWindow.getVillageTabBar();
        }

        public WheelPopup getWheelPopup()
        {
            return this.m_WheelPopup;
        }

        public WheelSelectPopup getWheelSelectPopup()
        {
            return this.m_WheelSelectPopup;
        }

        public Rectangle getWindowRect()
        {
            return new Rectangle(this.parentForm.Location, this.parentForm.Size);
        }

        public WorldSelectPopupWindow getWorldSelectPopupWindow()
        {
            return this.m_worldSelectPopupWindow;
        }

        public void initAllArmiesTab()
        {
            this.setVillageTabSubMode(0x17, true);
        }

        public void initCastleAttackerSetupTab()
        {
            this.showDXWindow(false);
            this.showDXCardBar(6);
            this.castleMapAttackerSetupPanel.Height = this.parentMainWindow.getMainRightHandPanel().Height;
            this.castleMapAttackerSetupPanel.initProperties(true, "Castle", null);
            this.castleMapAttackerSetupPanel.display(this.parentMainWindow.getMainRightHandPanel(), 2, 5);
            this.castleMapAttackerSetupPanel.init();
        }

        public void initCastleBattleTab(bool realBattle, int attackType, bool AIAttack)
        {
            this.showDXWindow(false);
            this.showDXCardBar(11);
            this.castleMapBattlePanel.Height = this.parentMainWindow.getMainRightHandPanel().Height;
            this.castleMapBattlePanel.initProperties(true, "Castle", null);
            this.castleMapBattlePanel.battleMode(realBattle, attackType, AIAttack);
            this.castleMapBattlePanel.display(this.parentMainWindow.getMainRightHandPanel(), 2, 5);
        }

        public void initCastleTab()
        {
            this.getTopRightMenu().showVillageTab(true);
            this.showDXWindow(false);
            this.showDXCardBar(11);
            this.castleMapPanel.Height = this.parentMainWindow.getMainRightHandPanel().Height;
            this.castleMapPanel.initProperties(true, "Castle", null);
            this.castleMapPanel.display(this.parentMainWindow.getMainRightHandPanel(), 6, 5);
            this.castleMapPanel.showNewInterface();
            this.castleInfoBar.show();
            this.castle_ClearSelectedTroop();
            if (GameEngine.Instance.Castle != null)
            {
                GameEngine.Instance.Castle.createSurroundSprites();
                GameEngine.Instance.Castle.recalcCastleLayout();
                GameEngine.Instance.Castle.moveMap(0, 0);
                if (GameEngine.Instance.Castle.isAnyConstructing())
                {
                    Sound.playVillageEnvironmental(0x12);
                }
                else
                {
                    Sound.playVillageEnvironmental(0x11);
                }
            }
            if (GameEngine.Instance.World.getTutorialStage() == 10)
            {
                GameEngine.Instance.World.handleQuestObjectiveHappening(0x2714);
            }
        }

        public void initChatPanel(int startingArea, int startAreaID)
        {
            if (this.chatScreenManager.isDocked())
            {
                this.getTopRightMenu().showVillageTab(false);
                this.getTopRightMenu().showFactionTabBar(false);
                this.chatScreenManager.Size = this.mainWindowPanel.Size;
                this.chatScreenManager.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                this.chatScreenManager.initProperties(true, "Main", this.mainWindowPanel);
                this.chatScreenManager.open(false, false, startingArea, startAreaID);
                this.chatScreenManager.display(this.mainWindowPanel, 0, 0);
            }
            else
            {
                this.chatScreenManager.open(false, false, startingArea, startAreaID);
            }
        }

        public void initGloryTab()
        {
            this.getTopRightMenu().showVillageTab(false);
            this.getTopRightMenu().showFactionTabBar(true);
            Instance.setVillageTabSubMode(0x16);
        }

        public void initInterfaces()
        {
            this.m_expandedMainSize = this.parentMainWindow.getDXBasePanel().Size;
            this.m_expandedMainSize.Width += this.parentMainWindow.getMainRightHandPanel().Size.Width;
        }

        public void initMailSubTab(int mode)
        {
            if (this.mailScreenManager.isDocked())
            {
                this.getTopRightMenu().showVillageTab(false);
                this.getTopRightMenu().showFactionTabBar(false);
                this.mailScreenManager.Size = this.mainWindowPanel.Size;
                this.mailScreenManager.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                this.mailScreenManager.initProperties(true, "Main", this.mainWindowPanel);
                this.mailScreenManager.open(false, false);
                this.mailScreenManager.display(this.mainWindowPanel, 0, 0);
            }
            else
            {
                this.mailScreenManager.open(false, false);
            }
            if (mode == 1)
            {
                this.mailScreenManager.startWithNewMessage(-1, "");
            }
        }

        public void initQuestsTab()
        {
            this.setVillageTabSubMode(0x1a, true);
            if (!GameEngine.Instance.World.TutorialIsAdvancing() && (GameEngine.Instance.World.getTutorialStage() == 0x65))
            {
                GameEngine.Instance.World.advanceTutorialOLD();
            }
        }

        public void initRankingsTab()
        {
            Instance.setVillageTabSubMode(0x13, true);
            RankingsPanel.setRanking(GameEngine.Instance.World.getRank(), GameEngine.Instance.World.getRankSubLevel());
            this.updateVillageReports();
        }

        public void initReportsLeaderboard()
        {
            this.setVillageTabSubMode(20, true);
        }

        public void initReportsReports()
        {
            this.setVillageTabSubMode(0x15, true);
        }

        public void initReportTab()
        {
            this.getTopRightMenu().showVillageTab(false);
            this.getTopRightMenu().showFactionTabBar(false);
            this.switchReportTabs(0);
        }

        public void initResearchTab()
        {
            this.getTopRightMenu().showVillageTab(false);
            this.getTopRightMenu().showFactionTabBar(false);
            this.researchPanel.initProperties(true, "Research", this.mainWindowPanel);
            this.researchPanel.Size = this.mainWindowPanel.Size;
            this.researchPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.researchPanel.updateBasedOnResearchData(GameEngine.Instance.World.UserResearchData, true);
            this.researchPanel.display(this.mainWindowPanel, 0, 0);
        }

        public void initVillageTab()
        {
            this.getTopRightMenu().showVillageTab(true);
            this.getTopRightMenu().showFactionTabBar(false);
            this.showDXWindow(false);
            this.showDXCardBar(10);
            this.villageMapPanel.Height = this.parentMainWindow.getMainRightHandPanel().Height;
            this.villageMapPanel.initProperties(true, "VillageBuildings", null);
            this.villageMapPanel.display(this.parentMainWindow.getMainRightHandPanel(), 6, 5);
            this.initVillageTab_Quick();
            if (GameEngine.Instance.World.getTutorialStage() == 100)
            {
                GameEngine.Instance.World.advanceTutorialOLD();
            }
            if (GameEngine.Instance.Village != null)
            {
                GameEngine.Instance.Village.createSurroundSprites();
                GameEngine.Instance.Village.moveMap(0, 0);
            }
        }

        public void initVillageTab_Quick()
        {
            this.updateVillageInfoBar();
            if (GameEngine.Instance.Village != null)
            {
                this.villageMapPanel.showAsVillage(!this.isSelectedVillageACapital(GameEngine.Instance.Village.VillageID));
            }
            VillageMap.closePopups();
            this.villageMapPanel.showNewInterface();
            this.villageMapPanel.showExtras();
        }

        public void initVillageTabTabBarsOnly()
        {
            this.getTopRightMenu().showVillageTab(true);
            this.getTopRightMenu().showFactionTabBar(false);
        }

        public void initVillageTabView()
        {
            this.getTopRightMenu().showVillageTab(false);
            this.getTopRightMenu().showFactionTabBar(false);
            this.showDXWindow(false);
            this.showDXCardBar(10);
            this.villageMapPanel.Height = this.parentMainWindow.getMainRightHandPanel().Height;
            this.villageMapPanel.initProperties(true, "VillageBuildings", null);
            this.villageMapPanel.display(this.parentMainWindow.getMainRightHandPanel(), 6, 5);
            this.villageInfoBar.show();
            VillageMap.closePopups();
            this.villageMapPanel.showExtras();
        }

        public void initWorldTab()
        {
            this.getTopRightMenu().showVillageTab(false);
            this.getTopRightMenu().showFactionTabBar(false);
            this.worldMapMode = 0;
            this.showDXWindow(true);
            this.showDXCardBar(9);
            this.userInfoPanel.initProperties(true, "User Village Info", this.parentMainWindow.getMainRightHandPanel());
            this.showMapFilterSelectPanel(true, true);
        }

        public void initWorldTab_attackTargetSelect()
        {
            this.worldMapMode = 3;
            this.attackTargetHomeVillage = -1;
            this.showDXWindow(true);
            this.showDXCardBar(9);
            this.displayAttackTargetSidepanel();
            this.setAttackTargetSidePanelVillage(-1);
            this.showMapFilterSelectPanel(false, true);
        }

        public void initWorldTab_courtierTargetSelect()
        {
            this.worldMapMode = 5;
            this.courtierHomeVillage = -1;
            this.showDXWindow(true);
            this.displayReinforcementTargetSidepanel();
            this.setReinforcementTargetSidePanelVillage(-1);
            this.showMapFilterSelectPanel(false, true);
        }

        public void initWorldTab_monkSelect()
        {
            this.worldMapMode = 9;
            this.monkSelectHomeVillage = -1;
            this.showDXWindow(true);
            this.showDXCardBar(9);
            this.displayMonkSelectSidePanel();
            this.setMonkSelectSidePanelVillage(-1);
            this.showMapFilterSelectPanel(false, true);
        }

        public void initWorldTab_scoutTargetSelect()
        {
            this.worldMapMode = 4;
            this.attackTargetHomeVillage = -1;
            this.showDXWindow(true);
            this.showDXCardBar(9);
            this.displayScoutTargetSidepanel();
            this.setScoutTargetSidePanelVillage(-1);
            this.showMapFilterSelectPanel(false, true);
        }

        public void initWorldTab_stockExchangeSelect()
        {
            this.worldMapMode = 2;
            this.stockExchangeBuyingVillage = -1;
            this.showDXWindow(true);
            this.showDXCardBar(9);
            this.displayStockExchangeSidepanel();
            this.setStockExchangeSidePanelVillage(-1);
            this.showMapFilterSelectPanel(false, true);
        }

        public void initWorldTab_tradingVillageSelect()
        {
            this.worldMapMode = 1;
            this.showDXWindow(true);
            this.showDXCardBar(9);
            this.displayTradeWithPanel();
            this.setTradeWithVillage(-1);
            this.showMapFilterSelectPanel(false, true);
        }

        public void initWorldTab_vassalSelect()
        {
            this.worldMapMode = 7;
            this.vassalSelectHomeVillage = -1;
            this.showDXWindow(true);
            this.showDXCardBar(9);
            this.displayVassalSelectSidePanel();
            this.setVassalSelectSidePanelVillage(-1);
            this.showMapFilterSelectPanel(false, true);
        }

        public void inviteToFaction(string username)
        {
            GameEngine.Instance.setNextFactionPage(0x2e);
            this.getMainTabBar().changeTab(8);
            Instance.setVillageTabSubMode(0x2e, false);
            this.villageReportBackgroundPanel.inviteToFaction(username);
        }

        public bool isAdvancedCastleOptionsPopup()
        {
            return this.isPopupWindowOpen(this.m_advancedCastleOptionsPopup);
        }

        public bool isAttackTargetsPopup()
        {
            return this.isPopupWindowOpen(this.m_AttackTargetsPopup);
        }

        public bool isBPPopup()
        {
            return this.isPopupWindowOpen(this.m_BPPopupWindow);
        }

        public bool isBuyVillage()
        {
            return this.isPopupWindowOpen(this.m_buyVillageWindow);
        }

        public bool isCardPopupOpen()
        {
            return this.isPopupWindowOpen(this.m_playCardsWindow);
        }

        public bool isChatDocked()
        {
            return this.chatScreenManager.isDocked();
        }

        public bool isConfirmBuyOfferPopup()
        {
            return this.isPopupWindowOpen(this.m_confirmBuyOfferPopup);
        }

        public bool isConfirmOpenPackPopup()
        {
            return this.isPopupWindowOpen(this.m_confirmOpenPackPopup);
        }

        public bool isConfirmPlayCardPopup()
        {
            return this.isPopupWindowOpen(this.m_confirmPlayCardPopup);
        }

        public bool isConnectionErrorWindow()
        {
            return this.isPopupWindowOpen(this.m_connectionErrorWindow);
        }

        public bool isCreatePopup()
        {
            return this.isPopupWindowOpen(this.m_createPopupWindow);
        }

        public bool isDonatePopupOpen()
        {
            return this.isPopupWindowOpen(this.m_currentDonatePopup);
        }

        public bool isDXVisible()
        {
            return (((this.parentMainWindow != null) && (this.parentMainWindow.getDXBasePanel() != null)) && this.parentMainWindow.getDXBasePanel().Visible);
        }

        public bool isFormationPopup()
        {
            return this.isPopupWindowOpen(this.m_formationPopup);
        }

        public bool isFreeCardsPopup()
        {
            return this.isPopupWindowOpen(this.m_freeCardsPopup);
        }

        public bool isGameMinimised()
        {
            return ((this.parentForm == null) || (this.parentForm.WindowState == FormWindowState.Minimized));
        }

        public bool isGreyOutWindow()
        {
            return this.isPopupWindowOpen(this.m_greyOutWindow);
        }

        public bool isInBuildingPanelOpen()
        {
            return this.villageMapPanel.isInBuildingPanelOpen();
        }

        public bool isInsideAchievementPopup()
        {
            return ((this.m_achievementPopup != null) && this.m_achievementPopup.isMouseInside());
        }

        public bool isLogoutPopupOpen()
        {
            return this.isPopupWindowOpen(this.m_logoutOptionsWindow);
        }

        public bool isMailDocked()
        {
            return this.mailScreenManager.isDocked();
        }

        public bool isMenuPopupOpen()
        {
            return this.isPopupWindowOpen(this.m_currentMenuPopup);
        }

        public bool isNewQuestRewardPopupOpen()
        {
            return this.isPopupWindowOpen(this.m_newQuestRewardPopup);
        }

        private bool isPopupWindowCreated(Form window)
        {
            return ((window != null) && window.Created);
        }

        private bool isPopupWindowOpen(Form window)
        {
            return (((window != null) && window.Created) && window.Visible);
        }

        private bool isPopupWindowOpenAndClose(Form window)
        {
            if ((window != null) && window.Created)
            {
                window.Close();
                return true;
            }
            return false;
        }

        public bool isReportCapturePopupOpen()
        {
            return this.isPopupWindowOpen(this.m_reportCapturePopup);
        }

        public bool isResearchOnEducationTab()
        {
            return this.researchPanel.isResearchOnEducationTab();
        }

        public bool isScoutPopup()
        {
            return this.isPopupWindowOpen(this.m_scoutPopupWindow);
        }

        public bool isSelectedVillageACapital()
        {
            return ((this.m_selectedMenuVillage >= 0) && GameEngine.Instance.World.isCapital(this.m_selectedMenuVillage));
        }

        public bool isSelectedVillageACapital(int villageID)
        {
            return ((villageID >= 0) && GameEngine.Instance.World.isCapital(villageID));
        }

        public bool isSelectedVillageACountryCapital()
        {
            return ((this.m_selectedMenuVillage >= 0) && GameEngine.Instance.World.isCountryCapital(this.m_selectedMenuVillage));
        }

        public bool isSelectedVillageACountyCapital()
        {
            return ((this.m_selectedMenuVillage >= 0) && GameEngine.Instance.World.isCountyCapital(this.m_selectedMenuVillage));
        }

        public bool isSelectedVillageAParishCapital()
        {
            return ((this.m_selectedMenuVillage >= 0) && GameEngine.Instance.World.isRegionCapital(this.m_selectedMenuVillage));
        }

        public bool isSelectedVillageAProvinceCapital()
        {
            return ((this.m_selectedMenuVillage >= 0) && GameEngine.Instance.World.isProvinceCapital(this.m_selectedMenuVillage));
        }

        public bool isSendMonk()
        {
            return this.isPopupWindowOpen(this.m_sendMonkWindow);
        }

        public bool isTextInputScreenActive()
        {
            return this.villageReportBackgroundPanel.isTextInputScreenActive();
        }

        public bool isTutorialArrowWindowOpen()
        {
            return this.isPopupWindowOpen(this.m_currentTutorialArrowWindow);
        }

        public bool isTutorialWindowOpen()
        {
            return this.isPopupWindowOpen(this.m_currentTutorialWindow);
        }

        public bool isUserInfoVisible()
        {
            return this.userInfoPanel.isVisible();
        }

        public bool isVacationCancelPopupWindow()
        {
            return this.isPopupWindowOpen(this.m_VacationCancelPopupWindow);
        }

        public bool isVillageHonourTabOpen()
        {
            return this.villageMapPanel.isHonourTabOpen();
        }

        public bool isVillageMapPanelOnFoodTab()
        {
            return this.villageMapPanel.isVillageMapPanelOnFoodTab();
        }

        public bool isVillageMapPanelOnIndustryTab()
        {
            return this.villageMapPanel.isVillageMapPanelOnIndustryTab();
        }

        public bool isVillageMapPanelOnPopularityBar()
        {
            return this.villageMapPanel.isVillageMapPanelOnPopularityBar();
        }

        public bool isWheelPopup()
        {
            return this.isPopupWindowOpen(this.m_WheelPopup);
        }

        public bool isWheelSelectPopup()
        {
            return this.isPopupWindowOpen(this.m_WheelSelectPopup);
        }

        public bool isWorldSelectPopup()
        {
            return this.isPopupWindowOpen(this.m_worldSelectPopupWindow);
        }

        public void leaderboardSearchComplete(LeaderBoardSearchResults results)
        {
            this.villageReportBackgroundPanel.leaderboardSearchComplete(results);
        }

        public void logout()
        {
            this.mailScreenManager.logout();
            this.mailScreenManager.close(true);
            this.chatScreenManager.close(true, true);
            this.chatScreenManager.logout();
            this.villageReportBackgroundPanel.clearAllReports();
            this.mailScreenManager.clearAllMail();
            this.villageReportBackgroundPanel.logout();
            this.clearControls();
            this.closeAllPopups();
            RemoteServices.Instance.UserID = -1;
            GameEngine.Instance.World.resetTutorialInfo();
            GameEngine.Instance.World.LastUpdatedCrowns = DateTime.Now.AddHours(-1.0);
            this.castleMapPanel.castleCommitReturn();
            CustomSelfDrawPanel.FactionPanelSideBar.logout();
            PlayCardsWindow.logout();
            TutorialPanel.logout();
            PostTutorialWindow.close();
            VideoWindow.ClosePopup();
            this.nextAchievementIDs.Clear();
        }

        public void mailPopupNewMail()
        {
            this.mailScreenManager.mailPopupNewMail();
        }

        public bool mailScreenNeedsOpening()
        {
            if (!this.mailScreenManager.isDocked())
            {
                return !this.mailScreenManager.isMailScreenVisible();
            }
            return true;
        }

        public void mailScreenRePop()
        {
            this.mailScreenManager.open(false, false);
        }

        public void mailTo(int userID, string userName)
        {
            this.mailScreenManager.mailTo(userID, userName);
        }

        public void mailTo(int userID, string[] userName)
        {
            this.mailScreenManager.mailTo(userID, userName);
        }

        public void mailUpdate()
        {
            this.mailScreenManager.mailUpdate();
        }

        public void mainWindowResize()
        {
            this.m_expandedMainSize = this.parentMainWindow.getDXBasePanel().Size;
            this.m_expandedMainSize.Height = this.parentForm.ClientSize.Height - 120;
            this.m_expandedMainSize.Width += this.parentMainWindow.getMainRightHandPanel().Size.Width;
            if (this.getTopLeftMenu().Height != 120)
            {
                this.parentMainWindow.getDXBasePanel().Size = new Size(this.parentMainWindow.getDXBasePanel().Width, (this.parentForm.ClientSize.Height - 120) + 0x1c);
            }
            else
            {
                this.parentMainWindow.getDXBasePanel().Size = new Size(this.parentMainWindow.getDXBasePanel().Width, this.parentForm.ClientSize.Height - 120);
            }
            if (this.parentMainWindow.isFullMainArea())
            {
                Size expandedMainSize = this.m_expandedMainSize;
                if (this.getTopLeftMenu().Height != 120)
                {
                    expandedMainSize.Height += 0x1c;
                }
                this.mainWindowPanel.Size = expandedMainSize;
            }
            else
            {
                this.mainWindowPanel.Size = this.parentMainWindow.getDXBasePanel().Size;
            }
            this.parentMainWindow.getMainRightHandPanel().Size = new Size(this.parentMainWindow.getMainRightHandPanel().Width, this.parentMainWindow.getDXBasePanel().Height);
            this.villageReportBackgroundPanel.screenResize();
            this.mailScreenManager.screenResize();
            this.chatScreenManager.screenResize();
            this.movePlayCardsWindow();
            this.moveLogoutWindow();
            this.moveScoutPopupWindow();
            this.moveBuyVillagePopupWindow();
            this.moveReportCaptureWindow();
            this.moveNewQuestRewardPopup();
            this.moveGreyOutWindow();
            this.moveMenuPopup();
            this.moveTutorialWindow();
            this.moveTutorialArrowWindow();
            this.moveFreeCardsPopup();
            this.moveWheelPopup();
            this.moveWheelSelectPopup();
            this.moveAdvancedCastleOptionsPopup();
            if (this.mapFilterSelectPanel.isVisible())
            {
                this.showMapFilterSelectPanel(true, true, true, false);
            }
        }

        public void mainWindowStartResize()
        {
            this.moveMenuPopup();
        }

        public void mapPanelCreates()
        {
            this.villageMapPanel.create();
            this.castleMapPanel.create();
            this.castleMapAttackerSetupPanel.create();
            this.castleMapBattlePanel.create();
        }

        public bool menuPopupClosedRecently()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - this.m_menuPopupClosedLastTime);
            return (span.TotalMilliseconds < 500.0);
        }

        public void mouseMoveDXCardBar(Point mousePos)
        {
            this.cardBarDX.mouseMove(mousePos);
        }

        public void moveAchievementPopup()
        {
            if (this.m_achievementPopup != null)
            {
                this.m_achievementPopup.move();
            }
        }

        public void moveAdvancedCastleOptionsPopup()
        {
            this.positionWindow(this.m_advancedCastleOptionsPopup, false, true);
        }

        public void moveAttackTargetsPopup()
        {
            this.positionWindow(this.m_AttackTargetsPopup, false, true);
        }

        public void moveBPPopupWindow()
        {
            Form bPPopupWindow = this.m_BPPopupWindow;
            if ((bPPopupWindow != null) && bPPopupWindow.Created)
            {
                this.m_BPPopupWindow.Location = new Point(Program.profileLogin.Location.X + ((Program.profileLogin.Width - this.m_BPPopupWindow.Width) / 2), (Program.profileLogin.Location.Y + ((Program.profileLogin.Height - this.m_BPPopupWindow.Height) / 2)) + 10);
            }
        }

        public void moveBuyVillagePopupWindow()
        {
            this.positionWindow(this.m_buyVillageWindow, true, true);
        }

        public void moveConfirmBuyOfferPopup()
        {
            this.positionWindow(this.m_confirmBuyOfferPopup, true, true);
        }

        public void moveConfirmOpenPackPopup()
        {
            this.positionWindow(this.m_confirmOpenPackPopup, true, true);
        }

        public void moveConfirmPlayCardPopup()
        {
            this.positionWindow(this.m_confirmPlayCardPopup, true, true);
        }

        public void moveConnectionErrorWindow()
        {
            this.positionWindow(this.m_connectionErrorWindow, true, true);
        }

        public void moveCreatePopupWindow()
        {
            Form createPopupWindow = this.m_createPopupWindow;
            if ((createPopupWindow != null) && createPopupWindow.Created)
            {
                this.m_createPopupWindow.Location = new Point(Program.profileLogin.Location.X + ((Program.profileLogin.Width - this.m_createPopupWindow.Width) / 2), Program.profileLogin.Location.Y + ((Program.profileLogin.Height - this.m_createPopupWindow.Height) / 2));
            }
        }

        public void moveCustomTooltip()
        {
        }

        public void moveDonatePopup()
        {
        }

        public void moveFormationPopup()
        {
            this.positionWindow(this.m_formationPopup, false, true);
        }

        public void moveFreeCardsPopup()
        {
            this.positionWindow(this.m_freeCardsPopup, false, true);
        }

        public void moveGreyOutWindow()
        {
            if (this.isPopupWindowCreated(this.m_greyOutWindow))
            {
                if (this.m_greyLogin)
                {
                    if (Program.profileLogin != null)
                    {
                        Size clientSize = Program.profileLogin.ClientSize;
                        Point point = Program.profileLogin.PointToScreen(new Point(0, 0));
                        this.m_greyOutWindow.Location = point;
                        this.m_greyOutWindow.Size = clientSize;
                    }
                }
                else
                {
                    Size size2 = this.parentMainWindow.ClientSize;
                    Point point2 = this.parentMainWindow.PointToScreen(new Point(0, 0));
                    this.m_greyOutWindow.Location = point2;
                    this.m_greyOutWindow.Size = size2;
                }
            }
        }

        public void moveLogoutWindow()
        {
            this.positionWindow(this.m_logoutOptionsWindow, false, true);
        }

        public void moveMenuPopup()
        {
            this.closeMenuPopup();
        }

        public void moveNewQuestRewardPopup()
        {
            this.positionWindow(this.m_newQuestRewardPopup, false, true);
        }

        public void movePlayCardsWindow()
        {
            this.positionWindow(this.m_playCardsWindow, false, true);
        }

        public void moveReportCaptureWindow()
        {
            this.positionWindow(this.m_reportCapturePopup, false, true);
        }

        public void moveReports(string folderName)
        {
            this.villageReportBackgroundPanel.moveReports(folderName);
        }

        public void moveScoutPopupWindow()
        {
            this.positionWindow(this.m_scoutPopupWindow, true, true);
        }

        public void moveSendMonkWindow()
        {
            this.positionWindow(this.m_sendMonkWindow, true, true);
        }

        public void moveTutorialArrowWindow()
        {
            if (this.m_currentTutorialArrowWindow != null)
            {
                this.m_currentTutorialArrowWindow.move();
            }
        }

        public void moveTutorialWindow()
        {
            if ((this.m_currentTutorialWindow != null) && this.m_currentTutorialWindow.Created)
            {
                this.m_currentTutorialWindow.updateLocation(0, this.ParentForm);
            }
        }

        public void moveVacationCancelPopupWindow()
        {
            Form vacationCancelPopupWindow = this.m_VacationCancelPopupWindow;
            if ((vacationCancelPopupWindow != null) && vacationCancelPopupWindow.Created)
            {
                this.m_VacationCancelPopupWindow.Location = new Point(Program.profileLogin.Location.X + ((Program.profileLogin.Width - this.m_VacationCancelPopupWindow.Width) / 2), Program.profileLogin.Location.Y + ((Program.profileLogin.Height - this.m_VacationCancelPopupWindow.Height) / 2));
            }
        }

        public void moveWheelPopup()
        {
            this.positionWindow(this.m_WheelPopup, false, true);
        }

        public void moveWheelSelectPopup()
        {
            this.positionWindow(this.m_WheelSelectPopup, false, true);
        }

        public void moveWorldSelectPopupWindow()
        {
            Form worldSelectPopupWindow = this.m_worldSelectPopupWindow;
            if ((worldSelectPopupWindow != null) && worldSelectPopupWindow.Created)
            {
                this.m_worldSelectPopupWindow.Location = new Point(Program.profileLogin.Location.X + ((Program.profileLogin.Width - this.m_worldSelectPopupWindow.Width) / 2), (Program.profileLogin.Location.Y + ((Program.profileLogin.Height - this.m_worldSelectPopupWindow.Height) / 2)) + 10);
            }
        }

        public void openAchievements(List<int> achievements)
        {
            this.closeMedalsPopup();
            this.medalsPopupPanel = new MedalsPopupWindow();
            this.medalsPopupPanel.init(achievements, this.ParentForm);
            this.medalsPopupPanel.Show(this.ParentForm);
        }

        public AdvancedCastleOptionsPopup openAdvancedCastleOptionsPopup(bool castleSetup)
        {
            this.openGreyOutWindow(false);
            this.closePopupWindow(this.m_advancedCastleOptionsPopup);
            this.m_advancedCastleOptionsPopup = new AdvancedCastleOptionsPopup();
            this.m_advancedCastleOptionsPopup.init(castleSetup);
            this.positionWindow(this.m_advancedCastleOptionsPopup, false, false);
            this.m_advancedCastleOptionsPopup.Show(this.getGreyOutWindow());
            GameEngine.Instance.DisableMouseClicks();
            return this.m_advancedCastleOptionsPopup;
        }

        public AttackTargetsPopup openAttackTargetsPopup()
        {
            this.closePopupWindow(this.m_AttackTargetsPopup);
            this.m_AttackTargetsPopup = new AttackTargetsPopup();
            this.positionWindow(this.m_AttackTargetsPopup, false, false);
            this.m_AttackTargetsPopup.Show(this.ParentMainWindow);
            GameEngine.Instance.DisableMouseClicks();
            return this.m_AttackTargetsPopup;
        }

        public BPPopupWindow openBPPopupWindow(ProfileLoginWindow parentForm)
        {
            this.openGreyOutWindowLogin(true);
            this.closePopupWindow(this.m_BPPopupWindow);
            this.m_BPPopupWindow = new BPPopupWindow();
            this.positionWindow(this.m_BPPopupWindow, true, false);
            this.m_BPPopupWindow.init(parentForm);
            this.m_BPPopupWindow.Show(this.getGreyOutWindow());
            GameEngine.Instance.DisableMouseClicks();
            this.m_BPPopupWindow.Location = new Point(Program.profileLogin.Location.X + ((Program.profileLogin.Width - this.m_BPPopupWindow.Width) / 2), (Program.profileLogin.Location.Y + ((Program.profileLogin.Height - this.m_BPPopupWindow.Height) / 2)) + 10);
            Program.profileLogin.TopMost = false;
            this.m_greyOutWindow.BringToFront();
            this.m_BPPopupWindow.BringToFront();
            this.m_BPPopupWindow.TopMost = true;
            this.m_BPPopupWindow.Focus();
            this.m_BPPopupWindow.TopMost = false;
            return this.m_BPPopupWindow;
        }

        public BuyVillagePopupWindow openBuyVillageWindow(int villageID, bool buy)
        {
            this.openGreyOutWindow(true);
            this.closePopupWindow(this.m_buyVillageWindow);
            this.m_buyVillageWindow = new BuyVillagePopupWindow();
            this.positionWindow(this.m_buyVillageWindow, true, false);
            this.m_buyVillageWindow.init(villageID, buy);
            this.m_buyVillageWindow.Show(this.getGreyOutWindow());
            if (Instance.isTutorialWindowOpen())
            {
                GameEngine.Instance.World.forceTutorialToBeShown();
            }
            GameEngine.Instance.DisableMouseClicks();
            return this.m_buyVillageWindow;
        }

        public ConfirmBuyOfferPopup openConfirmBuyOfferPopup(CustomSelfDrawPanel.UICardOffer offer, ConfirmBuyOfferPanel.CardClickPlayDelegate callback)
        {
            this.closePopupWindow(this.m_confirmBuyOfferPopup);
            this.m_confirmBuyOfferPopup = new ConfirmBuyOfferPopup();
            this.positionWindow(this.m_confirmBuyOfferPopup, false, false);
            this.m_confirmBuyOfferPopup.init(offer, callback);
            this.m_confirmBuyOfferPopup.Show(this.getCardWindow());
            return this.m_confirmBuyOfferPopup;
        }

        public ConfirmOpenPackPopup openConfirmOpenPackPopup(CustomSelfDrawPanel.UICardPack pack, ConfirmOpenPackPanel.CardClickPlayDelegate callback)
        {
            this.closePopupWindow(this.m_confirmOpenPackPopup);
            this.m_confirmOpenPackPopup = new ConfirmOpenPackPopup();
            this.positionWindow(this.m_confirmOpenPackPopup, false, false);
            this.m_confirmOpenPackPopup.init(pack, callback);
            this.m_confirmOpenPackPopup.Show(this.getCardWindow());
            return this.m_confirmOpenPackPopup;
        }

        public ConfirmPlayCardPopup openConfirmPlayCardPopup(CardTypes.CardDefinition def, ConfirmPlayCardPanel.CardClickPlayDelegate callback)
        {
            this.closePopupWindow(this.m_confirmPlayCardPopup);
            this.m_confirmPlayCardPopup = new ConfirmPlayCardPopup();
            this.positionWindow(this.m_confirmPlayCardPopup, false, false);
            this.m_confirmPlayCardPopup.init(def, callback);
            this.m_confirmPlayCardPopup.Show(this.getCardWindow());
            return this.m_confirmPlayCardPopup;
        }

        public ConnectionErrorWindow openConnectionErrorWindow()
        {
            this.openGreyOutWindow(true);
            this.closePopupWindow(this.m_connectionErrorWindow);
            this.m_connectionErrorWindow = new ConnectionErrorWindow();
            this.positionWindow(this.m_connectionErrorWindow, false, false);
            this.m_connectionErrorWindow.init();
            this.m_connectionErrorWindow.Show(this.getGreyOutWindow());
            if (Instance.isTutorialWindowOpen())
            {
                GameEngine.Instance.World.forceTutorialToBeShown();
            }
            GameEngine.Instance.DisableMouseClicks();
            return this.m_connectionErrorWindow;
        }

        public CreatePopupWindow openCreatePopupWindow()
        {
            this.openGreyOutWindowLogin(true);
            this.closePopupWindow(this.m_createPopupWindow);
            this.m_createPopupWindow = new CreatePopupWindow();
            this.positionWindow(this.m_createPopupWindow, true, false);
            this.m_createPopupWindow.init();
            this.m_createPopupWindow.Show(this.getGreyOutWindow());
            GameEngine.Instance.DisableMouseClicks();
            this.m_createPopupWindow.Location = new Point(Program.profileLogin.Location.X + ((Program.profileLogin.Width - this.m_createPopupWindow.Width) / 2), Program.profileLogin.Location.Y + ((Program.profileLogin.Height - this.m_createPopupWindow.Height) / 2));
            Program.profileLogin.TopMost = false;
            this.m_greyOutWindow.BringToFront();
            this.m_createPopupWindow.BringToFront();
            this.m_createPopupWindow.TopMost = true;
            this.m_createPopupWindow.Focus();
            this.m_createPopupWindow.TopMost = false;
            return this.m_createPopupWindow;
        }

        public FormationPopup openFormationPopup()
        {
            this.openGreyOutWindow(false);
            this.closePopupWindow(this.m_formationPopup);
            this.m_formationPopup = new FormationPopup();
            this.positionWindow(this.m_formationPopup, false, false);
            this.m_formationPopup.Show(this.getGreyOutWindow());
            GameEngine.Instance.DisableMouseClicks();
            return this.m_formationPopup;
        }

        public FreeCardsPopup openFreeCardsPopup()
        {
            this.openGreyOutWindow(true);
            this.closePopupWindow(this.m_freeCardsPopup);
            this.m_freeCardsPopup = new FreeCardsPopup();
            this.positionWindow(this.m_freeCardsPopup, false, false);
            this.m_freeCardsPopup.init();
            this.m_freeCardsPopup.Show(this.getGreyOutWindow());
            GameEngine.Instance.DisableMouseClicks();
            return this.m_freeCardsPopup;
        }

        public void openGloryVictoryPopup()
        {
            this.closeGloryVictoryWindowPopup();
            this.gloryVictoryWindow = new GloryVictoryWindow();
            this.gloryVictoryWindow.init(this.ParentForm);
            this.gloryVictoryWindow.Show(this.ParentForm);
        }

        public GreyOutWindow openGreyOutWindow(bool showBorder)
        {
            if (!this.isPopupWindowCreated(this.m_greyOutWindow))
            {
                this.m_greyLogin = false;
                this.m_greyOutWindow = new GreyOutWindow();
                Size clientSize = this.parentMainWindow.ClientSize;
                Point point = this.parentMainWindow.PointToScreen(new Point(0, 0));
                this.m_greyOutWindow.Location = point;
                this.m_greyOutWindow.Size = clientSize;
                this.m_greyOutWindow.init(showBorder);
                this.m_greyOutWindow.Show(this.ParentMainWindow);
            }
            return this.m_greyOutWindow;
        }

        public GreyOutWindow openGreyOutWindow(bool showBorder, Form parent)
        {
            if (!this.isPopupWindowCreated(this.m_greyOutWindow))
            {
                this.m_greyLogin = false;
                this.m_greyOutWindow = new GreyOutWindow();
                Size clientSize = parent.ClientSize;
                Point point = parent.PointToScreen(new Point(0, 0));
                this.m_greyOutWindow.Location = point;
                this.m_greyOutWindow.Size = clientSize;
                this.m_greyOutWindow.init(showBorder);
                this.m_greyOutWindow.Show(parent);
            }
            return this.m_greyOutWindow;
        }

        public GreyOutWindow openGreyOutWindowLogin(bool showBorder)
        {
            if (!this.isPopupWindowCreated(this.m_greyOutWindow))
            {
                if (Program.profileLogin == null)
                {
                    return null;
                }
                this.m_greyLogin = true;
                this.m_greyOutWindow = new GreyOutWindow();
                Size clientSize = Program.profileLogin.ClientSize;
                Point point = Program.profileLogin.PointToScreen(new Point(0, 0));
                this.m_greyOutWindow.Location = point;
                this.m_greyOutWindow.Size = clientSize;
                this.m_greyOutWindow.init(showBorder);
                this.m_greyOutWindow.Show(Program.profileLogin);
            }
            return this.m_greyOutWindow;
        }

        public SendArmyWindow openLaunchAttackPopup()
        {
            this.openGreyOutWindow(true);
            this.closePopupWindow(this.m_launchAttackPopup);
            this.m_launchAttackPopup = new SendArmyWindow();
            this.positionWindow(this.m_launchAttackPopup, true, false);
            this.m_launchAttackPopup.Show(this.getGreyOutWindow());
            if (Instance.isTutorialWindowOpen())
            {
                GameEngine.Instance.World.forceTutorialToBeShown();
            }
            GameEngine.Instance.DisableMouseClicks();
            return this.m_launchAttackPopup;
        }

        public LogoutOptionsWindow2 openLogoutWindow(bool normalLogout)
        {
            return this.openLogoutWindow(normalLogout, false);
        }

        public LogoutOptionsWindow2 openLogoutWindow(bool normalLogout, bool advertOnly)
        {
            this.openGreyOutWindow(false);
            this.closePopupWindow(this.m_logoutOptionsWindow);
            this.m_logoutOptionsWindow = new LogoutOptionsWindow2();
            this.positionWindow(this.m_logoutOptionsWindow, false, false);
            this.m_logoutOptionsWindow.init(normalLogout, advertOnly);
            this.m_logoutOptionsWindow.Show(this.getGreyOutWindow());
            GameEngine.Instance.DisableMouseClicks();
            return this.m_logoutOptionsWindow;
        }

        public void openNewQuestFurtherTextPopup(string questTag, int questID)
        {
            List<int> quests = new List<int>();
            this.closeNewQuestsCompletedPopup();
            this.newQuestsCompletedWindow = new NewQuestsCompletedWindow();
            this.newQuestsCompletedWindow.init(this.ParentForm, quests, false, questTag, questID);
            this.newQuestsCompletedWindow.Show(this.ParentForm);
        }

        public NewQuestRewardPopup openNewQuestRewardPopup(int questID, int villageID, NewQuestsPanel parent)
        {
            this.openGreyOutWindow(false);
            this.closePopupWindow(this.m_newQuestRewardPopup);
            this.m_newQuestRewardPopup = new NewQuestRewardPopup();
            this.m_newQuestRewardPopup.init(questID, villageID, parent);
            this.positionWindow(this.m_newQuestRewardPopup, false, false);
            this.m_newQuestRewardPopup.Show(this.getGreyOutWindow());
            GameEngine.Instance.DisableMouseClicks();
            return this.m_newQuestRewardPopup;
        }

        public void openNewQuestsCompletedPopup(List<int> completedQuests)
        {
            this.closeNewQuestsCompletedPopup();
            this.newQuestsCompletedWindow = new NewQuestsCompletedWindow();
            this.newQuestsCompletedWindow.init(this.ParentForm, completedQuests, true, null, -1);
            this.newQuestsCompletedWindow.Show(this.ParentForm);
        }

        public PlayCardsWindow openPlayCardsWindow(int cardSection)
        {
            this.openGreyOutWindow(false);
            this.closePopupWindow(this.m_playCardsWindow);
            this.m_playCardsWindow = new PlayCardsWindow();
            this.positionWindow(this.m_playCardsWindow, false, false);
            this.m_playCardsWindow.init(cardSection, true);
            this.m_playCardsWindow.Show(this.getGreyOutWindow());
            if (Instance.isTutorialWindowOpen())
            {
                GameEngine.Instance.World.forceTutorialToBeShown();
            }
            GameEngine.Instance.DisableMouseClicks();
            return this.m_playCardsWindow;
        }

        public PlayCardsWindow openPlayCardsWindowSearch(int cardSection, string searchText)
        {
            this.openGreyOutWindow(false);
            this.closePopupWindow(this.m_playCardsWindow);
            this.m_playCardsWindow = new PlayCardsWindow();
            this.positionWindow(this.m_playCardsWindow, false, false);
            this.m_playCardsWindow.init(cardSection, true);
            this.m_playCardsWindow.Show(this.getGreyOutWindow());
            this.m_playCardsWindow.tbSearchBox.Text = searchText;
            this.m_playCardsWindow.performSearch();
            if (Instance.isTutorialWindowOpen())
            {
                GameEngine.Instance.World.forceTutorialToBeShown();
            }
            GameEngine.Instance.DisableMouseClicks();
            return this.m_playCardsWindow;
        }

        public ReportCapturePopup openReportCaptureWindow(int mode)
        {
            this.openGreyOutWindow(false);
            this.closePopupWindow(this.m_reportCapturePopup);
            this.m_reportCapturePopup = new ReportCapturePopup();
            this.m_reportCapturePopup.init(mode);
            this.positionWindow(this.m_reportCapturePopup, false, false);
            this.m_reportCapturePopup.Show(this.getGreyOutWindow());
            GameEngine.Instance.DisableMouseClicks();
            return this.m_reportCapturePopup;
        }

        public ScoutPopupWindow openScoutPopupWindow(int villageID, bool resetData)
        {
            this.openGreyOutWindow(true);
            this.closePopupWindow(this.m_scoutPopupWindow);
            this.m_scoutPopupWindow = new ScoutPopupWindow();
            this.positionWindow(this.m_scoutPopupWindow, true, false);
            this.m_scoutPopupWindow.init(villageID, resetData);
            this.m_scoutPopupWindow.Show(this.getGreyOutWindow());
            if (Instance.isTutorialWindowOpen())
            {
                GameEngine.Instance.World.forceTutorialToBeShown();
            }
            GameEngine.Instance.DisableMouseClicks();
            return this.m_scoutPopupWindow;
        }

        public SendMonkWindow openSendMonkWindow(int villageID)
        {
            this.openGreyOutWindow(true);
            this.closePopupWindow(this.m_sendMonkWindow);
            this.m_sendMonkWindow = new SendMonkWindow();
            this.positionWindow(this.m_sendMonkWindow, true, false);
            this.m_sendMonkWindow.init(villageID);
            this.m_sendMonkWindow.Show(this.getGreyOutWindow());
            if (Instance.isTutorialWindowOpen())
            {
                GameEngine.Instance.World.forceTutorialToBeShown();
            }
            GameEngine.Instance.DisableMouseClicks();
            return this.m_sendMonkWindow;
        }

        public VacationCancelPopupWindow openVacationCancelPopupWindow(int secondsLeft, int secondsLeftToCancel, bool canCancel)
        {
            this.openGreyOutWindowLogin(true);
            this.closePopupWindow(this.m_VacationCancelPopupWindow);
            this.m_VacationCancelPopupWindow = new VacationCancelPopupWindow();
            this.positionWindow(this.m_VacationCancelPopupWindow, true, false);
            this.m_VacationCancelPopupWindow.init(secondsLeft, secondsLeftToCancel, canCancel);
            this.m_VacationCancelPopupWindow.Show(this.getGreyOutWindow());
            GameEngine.Instance.DisableMouseClicks();
            this.m_VacationCancelPopupWindow.Location = new Point(Program.profileLogin.Location.X + ((Program.profileLogin.Width - this.m_VacationCancelPopupWindow.Width) / 2), Program.profileLogin.Location.Y + ((Program.profileLogin.Height - this.m_VacationCancelPopupWindow.Height) / 2));
            Program.profileLogin.TopMost = false;
            this.m_greyOutWindow.BringToFront();
            this.m_VacationCancelPopupWindow.BringToFront();
            this.m_VacationCancelPopupWindow.TopMost = true;
            this.m_VacationCancelPopupWindow.Focus();
            this.m_VacationCancelPopupWindow.TopMost = false;
            return this.m_VacationCancelPopupWindow;
        }

        public WheelPopup openWheelPopup(int wheelType)
        {
            this.openGreyOutWindow(true);
            this.closePopupWindow(this.m_WheelPopup);
            this.m_WheelPopup = new WheelPopup();
            this.positionWindow(this.m_WheelPopup, false, false);
            this.m_WheelPopup.init(wheelType);
            this.m_WheelPopup.Show(this.getGreyOutWindow());
            GameEngine.Instance.DisableMouseClicks();
            return this.m_WheelPopup;
        }

        public WheelSelectPopup openWheelSelectPopup()
        {
            this.openGreyOutWindow(true);
            this.closePopupWindow(this.m_WheelSelectPopup);
            this.m_WheelSelectPopup = new WheelSelectPopup();
            this.positionWindow(this.m_WheelSelectPopup, false, false);
            this.m_WheelSelectPopup.init();
            this.m_WheelSelectPopup.Show(this.getGreyOutWindow());
            GameEngine.Instance.DisableMouseClicks();
            return this.m_WheelSelectPopup;
        }

        public WorldSelectPopupWindow openWorldSelectPopupWindow()
        {
            this.openGreyOutWindowLogin(true);
            this.closePopupWindow(this.m_worldSelectPopupWindow);
            this.m_worldSelectPopupWindow = new WorldSelectPopupWindow();
            this.positionWindow(this.m_worldSelectPopupWindow, true, false);
            this.m_worldSelectPopupWindow.init(0, false);
            this.m_worldSelectPopupWindow.Show(this.getGreyOutWindow());
            GameEngine.Instance.DisableMouseClicks();
            this.m_worldSelectPopupWindow.Location = new Point(Program.profileLogin.Location.X + ((Program.profileLogin.Width - this.m_worldSelectPopupWindow.Width) / 2), (Program.profileLogin.Location.Y + ((Program.profileLogin.Height - this.m_worldSelectPopupWindow.Height) / 2)) + 10);
            Program.profileLogin.TopMost = false;
            this.m_greyOutWindow.BringToFront();
            this.m_worldSelectPopupWindow.BringToFront();
            this.m_worldSelectPopupWindow.TopMost = true;
            this.m_worldSelectPopupWindow.Focus();
            this.m_worldSelectPopupWindow.TopMost = false;
            return this.m_worldSelectPopupWindow;
        }

        private void positionWindow(CustomSelfDrawPanel window, bool dxCentre, bool needCreated)
        {
        }

        private void positionWindow(Form window, bool dxCentre, bool needCreated)
        {
            if ((window != null) && (window.Created || !needCreated))
            {
                if (!dxCentre)
                {
                    Point location = this.parentMainWindow.Location;
                    Size clientSize = this.parentMainWindow.ClientSize;
                    int x = (((clientSize.Width - window.Size.Width) / 2) + location.X) + 4;
                    int y = (((((clientSize.Height - window.Size.Height) - 120) / 2) + 120) + location.Y) + 0x10;
                    window.Location = new Point(x, y);
                }
                else
                {
                    Size size = this.parentMainWindow.getDXBasePanel().Size;
                    Point point2 = this.parentMainWindow.getDXBasePanel().PointToScreen(new Point(0, 0));
                    int num3 = ((size.Width - window.Size.Width) / 2) + point2.X;
                    int num4 = ((size.Height - window.Size.Height) / 2) + point2.Y;
                    window.Location = new Point(num3, num4);
                }
            }
        }

        public void processAchievements(List<int> achievements)
        {
            if (achievements == null)
            {
                RemoteServices.Instance.UserAchievements = new List<int>();
            }
            else
            {
                List<int> list = new List<int>();
                bool flag = false;
                foreach (int num in achievements)
                {
                    if (num == -1)
                    {
                        flag = true;
                    }
                    else
                    {
                        if (flag)
                        {
                            this.activateAchievementPopup(num + 0x3e8);
                        }
                        list.Add(num);
                    }
                }
                RemoteServices.Instance.UserAchievements = list;
            }
        }

        public void reactiveMainWindow()
        {
            if (this.parentForm != null)
            {
                this.parentForm.TopMost = true;
                this.parentForm.Focus();
                this.parentForm.BringToFront();
                this.parentForm.TopMost = false;
            }
        }

        public void refreshCastleInterface()
        {
            this.castleMapPanel.refreshInterface();
        }

        public void refreshForMail(bool success)
        {
        }

        public void registerForm(Form parent, MainWindow newParentMainWindow)
        {
            this.parentForm = parent;
            this.parentMainWindow = newParentMainWindow;
        }

        public void reloadQuestPanel()
        {
            this.villageReportBackgroundPanel.questPanelInit();
        }

        public void reportTabSetup()
        {
            RemoteServices.Instance.getReportFolders();
        }

        public void researchDataChanged(ResearchData data)
        {
            if ((data != null) && this.researchPanel.isVisible())
            {
                this.researchPanel.updateBasedOnResearchData(data, true);
            }
        }

        public void resetVillageReportPanelData()
        {
            this.villageReportBackgroundPanel.resetData();
        }

        public void reShowDXWindow()
        {
            bool flag = this.getTopLeftMenu().contextBarVisible();
            this.showDXWindow(!flag);
        }

        public void runCastleInterface()
        {
            if (this.castleMapPanel.isVisible())
            {
                this.castleMapPanel.Run();
            }
        }

        public void runTooltips()
        {
            CustomTooltipManager.runTooltips();
            if (((this.m_currentCustomTooltip != null) && this.m_currentCustomTooltip.Created) && this.m_currentCustomTooltip.Visible)
            {
                this.m_currentCustomTooltip.updateLocation();
            }
        }

        public void runTutorialWindow()
        {
            if (this.m_currentTutorialWindow != null)
            {
                this.m_currentTutorialWindow.update();
            }
        }

        public void runVillageInterface()
        {
            if (this.villageMapPanel.isVisible())
            {
                this.villageMapPanel.run();
            }
        }

        public void selectAttackTarget(int villageID)
        {
            this.villageReportBackgroundPanel.selectAttackTarget(villageID);
        }

        public void selectCurrentUserVillage()
        {
            if ((this.m_selectedMenuVillage >= 0) && GameEngine.Instance.World.isUserVillage(this.m_selectedMenuVillage))
            {
                this.selectUserVillage(this.m_selectedMenuVillage, false);
            }
        }

        public void selectedVillageNameLeft()
        {
            int villageID = GameEngine.Instance.World.getNextUserVillage(this.m_selectedMenuVillage, -1);
            if (villageID >= 0)
            {
                this.selectUserVillage(villageID, true);
            }
        }

        public void selectedVillageNameRight()
        {
            int villageID = GameEngine.Instance.World.getNextUserVillage(this.m_selectedMenuVillage, 1);
            if (villageID >= 0)
            {
                this.selectUserVillage(villageID, true);
            }
        }

        public void selectScoutTarget(int villageID)
        {
            this.villageReportBackgroundPanel.selectScoutsTarget(villageID);
        }

        public void selectStockExchange(int villageID)
        {
            this.villageReportBackgroundPanel.selectExchange(villageID);
        }

        public void selectTutorialArmy()
        {
            long armyID = GameEngine.Instance.World.getTutorialArmyID();
            if (armyID >= 0L)
            {
                this.closeFilterPanel();
                this.closeSelectedVillagePanel();
                this.closeTraderInfoPanel();
                this.closeReinforcementSelectedPanel();
                this.closePersonInfoPanel();
                this.clearAndCloseUserInfo();
                this.displayArmySelectPanel(armyID);
            }
        }

        public void selectUserVillage(int villageID, bool zoomIn)
        {
            GameEngine.Instance.MovedFromVillageID = villageID;
            if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_WORLD)
            {
                if (zoomIn)
                {
                    Point point = GameEngine.Instance.World.getVillageLocation(villageID);
                    GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                }
                this.displaySelectedVillagePanel(villageID, false, true, true, false);
            }
            else
            {
                this.setVillageNameBar(villageID);
                if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE)
                {
                    if (this.wasShowingVassalSendScreen())
                    {
                        Instance.setVillageTabSubMode(8);
                    }
                    GameEngine.Instance.downloadCurrentVillage();
                }
                else if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
                {
                    if (GameEngine.Instance.GameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP)
                    {
                        Instance.getMainTabBar().changeTab(9);
                        Instance.getMainTabBar().changeTab(0);
                    }
                    else
                    {
                        GameEngine.Instance.downloadCurrentVillage();
                    }
                }
            }
            if (this.getMainTabBar().getCurrentTab() == 1)
            {
                if (GameEngine.Instance.World.isCapital(villageID))
                {
                    this.getMainTabBar().changeTabGfxOnly(2);
                    GameEngine.Instance.externalMainTabChange(2);
                }
            }
            else if ((this.getMainTabBar().getCurrentTab() == 2) && !GameEngine.Instance.World.isCapital(villageID))
            {
                this.getMainTabBar().changeTabGfxOnly(1);
                GameEngine.Instance.externalMainTabChange(1);
            }
        }

        public void selectVassalTarget(int villageID)
        {
            this.villageReportBackgroundPanel.selectVassalVillage(villageID);
        }

        public void selectVillage(int villageID)
        {
            this.m_reallySelectedVillage = villageID;
        }

        public void sendProclamation(int mailType, int areaID)
        {
            this.mailScreenManager.sendProclamation(mailType, areaID);
        }

        public void setAttackTargetSidePanelVillage(int villageID)
        {
            this.attackTargetSidePanel.setTarget(villageID);
        }

        public void setCapitalSendTargetVillage(int villageID)
        {
            this.villageReportBackgroundPanel.setCapitalSendTargetVillage(villageID);
        }

        public void setCardData(CardData cardData)
        {
            this.parentMainWindow.getTopLeftMenu().setCards(cardData);
        }

        public void setCastlePillageClock(int pillageClock, int pillageClockMax)
        {
            this.castleMapBattlePanel.setPillageClock(pillageClock, pillageClockMax);
        }

        public void setCastleReportClock(int reportClock, int reportClockMax)
        {
            this.castleMapBattlePanel.setCastleReportClock(reportClock, reportClockMax);
        }

        public void setCastleStats(int numGuardHouseSpaces, int numPlacedArchers, int numPlacedPeasants, int numPlacedPikemen, int numPlacedSwordsmen, DateTime completeTime, bool completed, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numPots, int numSmelterPlaces, bool castleDamaged, int numPlacedReinforceArchers, int numPlacedReinforcePeasants, int numPlacedReinforcePikemen, int numPlacedReinforceSwordsmen, int numReinforcePeasants, int numReinforceArchers, int numReinforcePikemen, int numReinforceSwordsmen, int numAvailableVassalReinforceDefenderPeasants, int numAvailableVassalReinforceDefenderArchers, int numAvailableVassalReinforceDefenderPikemen, int numAvailableVassalReinforceDefenderSwordsmen, int numPlacedVassalReinforceDefenderArchers, int numPlacedVassalReinforceDefenderPeasants, int numPlacedVassalReinforceDefenderPikemen, int numPlacedVassalReinforceDefenderSwordsmen, int numPlacedCaptains, int numCaptains)
        {
            this.castleMapPanel.setCastleStats(numGuardHouseSpaces, numPlacedArchers, numPlacedPeasants, numPlacedPikemen, numPlacedSwordsmen, completeTime, completed, numPeasants, numArchers, numPikemen, numSwordsmen, numPots, numSmelterPlaces, castleDamaged, numPlacedReinforceArchers, numPlacedReinforcePeasants, numPlacedReinforcePikemen, numPlacedReinforceSwordsmen, numReinforcePeasants, numReinforceArchers, numReinforcePikemen, numReinforceSwordsmen, numAvailableVassalReinforceDefenderPeasants, numAvailableVassalReinforceDefenderArchers, numAvailableVassalReinforceDefenderPikemen, numAvailableVassalReinforceDefenderSwordsmen, numPlacedVassalReinforceDefenderArchers, numPlacedVassalReinforceDefenderPeasants, numPlacedVassalReinforceDefenderPikemen, numPlacedVassalReinforceDefenderSwordsmen, numPlacedCaptains, numCaptains);
        }

        public void setCastleViewTimes(DateTime castleViewTime, bool castleAvailable, DateTime troopViewTime, bool troopAvailable)
        {
            this.castleMapAttackerSetupPanel.setTimes(castleViewTime, castleAvailable, troopViewTime, troopAvailable);
        }

        public void setConnectionLight(bool loading)
        {
            if (this.getMainMenuBar() != null)
            {
                this.getMainMenuBar().setLoadingLight(loading);
            }
        }

        public void setCurrentCustomTooltip(CustomTooltip customTooltip)
        {
            this.m_currentCustomTooltip = customTooltip;
        }

        public void setCurrentDonatePopup(DonatePopup donatePopup)
        {
            this.m_currentDonatePopup = donatePopup;
        }

        public void setCurrentMenuPopup(MenuPopup menuPopup)
        {
            this.m_currentMenuPopup = menuPopup;
        }

        public void setCurrentTutorialArrowWindow(TutorialArrowWindow donatePopup)
        {
            this.m_currentTutorialArrowWindow = donatePopup;
        }

        public void setCurrentTutorialWindow(TutorialWindow tutorialWindow)
        {
            this.m_currentTutorialWindow = tutorialWindow;
        }

        public void setFaithPoints(double newFaithPoints)
        {
            this.parentMainWindow.getTopLeftMenu().SetFaithPoints(newFaithPoints);
        }

        public void setFloatingTextSentDelegate(FloatingTextSent del)
        {
            this.sendTextDelegate = del;
        }

        public void setFloatingValueSentDelegate(FloatingValueSent del)
        {
            this.sendDelegate = del;
        }

        public void setGold(double newGold)
        {
            this.parentMainWindow.getTopLeftMenu().setGold(newGold);
        }

        public void setHonour(double newHonour, int rank)
        {
            this.parentMainWindow.getTopLeftMenu().setHonour(newHonour, rank);
        }

        public void setMonkSelectSidePanelVillage(int villageID)
        {
            this.monkTargetSidePanel.setTarget(villageID);
        }

        public void setPoints(int points)
        {
            this.parentMainWindow.getTopLeftMenu().setPoints(points);
        }

        public void setRank(int rank)
        {
            this.getTopLeftMenu().setRank(rank);
        }

        public void setReinforcementTargetSidePanelVillage(int villageID)
        {
            this.reinforcementTargetSidePanel.setReinforcementTarget(villageID);
        }

        public void setReinforcementVillage(int villageID)
        {
            this.villageReportBackgroundPanel.setReinforcementVillage(villageID);
        }

        public void setReportAlreadyRead(long reportID)
        {
            this.villageReportBackgroundPanel.setReportAlreadyRead(reportID);
        }

        public void setReportData(object reportData, long reportID)
        {
            this.villageReportBackgroundPanel.setReportData(reportData, reportID);
        }

        public void setScoutTargetSidePanelVillage(int villageID)
        {
            this.scoutTargetSidePanel.setTarget(villageID);
        }

        public void setServerTime(DateTime serverTime, int gameDay)
        {
            this.parentMainWindow.getMainMenuBar().setServerTime(serverTime, gameDay);
        }

        public void setStockExchangeSidePanelVillage(int villageID)
        {
            this.stockExchangeSidePanel.setStockExchange(villageID);
        }

        public void setTradeWithVillage(int villageID)
        {
            this.tradeWithPanel.setTradeWithVillage(villageID);
        }

        public void setupVillageName()
        {
            this.m_selectedMenuVillage = -1;
            int villageID = -1;
            do
            {
                villageID = GameEngine.Instance.World.getNextUserVillage(villageID, 1);
            }
            while ((villageID >= 0) && GameEngine.Instance.World.isCapital(villageID));
            if (villageID >= 0)
            {
                this.setVillageNameBar(villageID);
                this.selectUserVillage(villageID, false);
            }
        }

        public void setUserName(string userName)
        {
            this.getTopLeftMenu().setUserName(userName);
        }

        public void setVassalArmiesVillage(int villageID)
        {
            this.villageReportBackgroundPanel.setVassalArmiesVillage(villageID);
        }

        public void setVassalAttackMode(int vassalVillageID)
        {
            this.m_selectedVassalVillage = vassalVillageID;
        }

        public void setVassalSelectSidePanelVillage(int villageID)
        {
            this.vassalSelectSidePanel.setTarget(villageID);
        }

        public void setVassalTargetVillage(int villageID, int targetVillageID)
        {
            this.villageReportBackgroundPanel.setVassalTargetVillage(villageID, targetVillageID);
        }

        public void setVillageHeading(string text)
        {
            if (!this.villageInfoBar.isVisible() || (this.villageInfoBar.Parent == null))
            {
                this.villageInfoBar.show();
            }
            this.villageInfoBar.Visible = true;
            this.villageInfoBar.setHeading(text);
        }

        public void setVillageInfoBar(VillageInfoBar2 infoBar, CastleInfoBar2 cInfoBar)
        {
            this.villageInfoBar = infoBar;
            this.castleInfoBar = cInfoBar;
        }

        public void setVillageInfoData(int woodLevel, int clayLevel, int stoneLevel, int foodLevel, bool gotStockpile, bool gotGranary, int totalPeople, int housingCapacity, int spareWorkers, int pitchLevel, bool viewOnly, int ironLevel, int capitalGold, int villageID, int numFlags)
        {
            GameEngine.GameDisplays gameDisplayMode = GameEngine.Instance.GameDisplayMode;
            if (gameDisplayMode != GameEngine.GameDisplays.DISPLAY_VILLAGE)
            {
                if (gameDisplayMode != GameEngine.GameDisplays.DISPLAY_CASTLE)
                {
                    return;
                }
            }
            else
            {
                this.villageInfoBar.setDisplayedLevels(woodLevel, clayLevel, stoneLevel, foodLevel, gotStockpile, gotGranary, totalPeople, housingCapacity, spareWorkers, viewOnly, capitalGold, villageID, numFlags);
                return;
            }
            this.castleInfoBar.setDisplayedLevels(woodLevel, stoneLevel, pitchLevel, ironLevel);
        }

        public void setVillageNameBar(int villageID)
        {
            if (GameEngine.Instance.World.isUserVillage(villageID) || GameEngine.Instance.World.isUserRelatedVillage(villageID))
            {
                this.m_ownSelectedVillage = villageID;
            }
            GameEngine.Instance.World.createTributeLinesList(villageID);
            this.m_selectedMenuVillage = villageID;
            this.parentMainWindow.getTopRightMenu().setSelectedVillageName(GameEngine.Instance.World.getVillageName(villageID), this.isSelectedVillageACapital(villageID));
        }

        public void setVillageTabSubMode(int mode)
        {
            this.setVillageTabSubMode(mode, false);
        }

        public void setVillageTabSubMode(int mode, bool overlayTab)
        {
            this.lastVillageTab = mode;
            if (mode == -1)
            {
                this.getDXBasePanel().Controls.Remove(this.mainWindowPanel);
                this.parentMainWindow.setMainWindowAreaVisible(true);
                GameEngine.Instance.forceVillageTabUpdate();
                this.getVillageTabBar().forceChangeTab(0);
            }
            else
            {
                GameEngine.Instance.forceVillageTabUpdate();
                if ((mode >= 0x3e8) && !this.isSelectedVillageACapital())
                {
                    this.initVillageTab();
                }
                else if ((((((mode < 0x3e8) && this.isSelectedVillageACapital()) && ((mode != 6) && (mode != 10))) && (((mode != 0x13) && (mode != 20)) && ((mode != 0x15) && (mode != 0x16)))) && ((((mode != 0x17) && (mode != 0x18)) && ((mode != 0x19) && (mode != 0x1a))) && (((mode != 0x1f) && (mode != 0x20)) && ((mode != 0x21) && (mode != 0x22))))) && (((((mode != 0x29) && (mode != 0x2a)) && ((mode != 0x2d) && (mode != 0x2e))) && (((mode != 0x2b) && (mode != 0x2c)) && ((mode != 0x2f) && (mode != 0x30)))) && ((((mode != 0x34) && (mode != 0x33)) && ((mode != 0x63) && (mode != 100))) && (mode != 60))))
                {
                    this.initVillageTab();
                }
                else
                {
                    this.StopDrawing();
                    this.addMainWindow(this.firstVillageBackgroundCall, overlayTab);
                    this.firstVillageBackgroundCall = false;
                    this.villageReportBackgroundPanel.initProperties(true, "Village Reports", this.mainWindowPanel);
                    this.villageReportBackgroundPanel.Size = this.mainWindowPanel.Size;
                    this.villageReportBackgroundPanel.display(this.mainWindowPanel, 0, 0);
                    this.villageReportBackgroundPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                    this.villageReportBackgroundPanel.showPanel(mode);
                    switch (mode)
                    {
                        case 0:
                            this.getVillageTabBar().changeTabGfxOnly(5);
                            break;

                        case 1:
                            this.getVillageTabBar().changeTabGfxOnly(6);
                            break;

                        case 2:
                        case 0x3ea:
                            this.getVillageTabBar().changeTabGfxOnly(3);
                            break;

                        case 3:
                        case 0x3eb:
                            this.getVillageTabBar().changeTabGfxOnly(3);
                            break;

                        case 4:
                        case 0x3ec:
                            this.getVillageTabBar().changeTabGfxOnly(4);
                            break;

                        case 5:
                        case 0x3ed:
                            this.getVillageTabBar().changeTabGfxOnly(2);
                            GameEngine.Instance.forceVillageTabUpdate();
                            break;

                        case 8:
                            this.getVillageTabBar().changeTabGfxOnly(7);
                            break;

                        case 11:
                        case 12:
                        case 13:
                            this.getVillageTabBar().changeTabGfxOnly(6);
                            break;

                        case 15:
                        case 0x10:
                        case 0x11:
                            this.getVillageTabBar().changeTabGfxOnly(9);
                            break;

                        case 0x13:
                        case 20:
                        case 0x15:
                        case 0x17:
                        case 0x18:
                            this.getTopRightMenu().showVillageTab(false);
                            this.getTopRightMenu().showFactionTabBar(false);
                            break;

                        case 0x16:
                            this.getTopRightMenu().showVillageTab(false);
                            this.getTopRightMenu().showFactionTabBar(true);
                            break;

                        case 0x19:
                            this.getTopRightMenu().showVillageTab(false);
                            this.getTopRightMenu().showFactionTabBar(false);
                            break;

                        case 0x1a:
                            this.getTopRightMenu().showVillageTab(false);
                            this.getTopRightMenu().showFactionTabBar(false);
                            break;

                        case 0x1f:
                            this.getTopRightMenu().showVillageTab(false);
                            break;

                        case 0x20:
                        case 0x21:
                        case 0x22:
                            this.getTopRightMenu().showVillageTab(false);
                            break;

                        case 0x29:
                        case 0x2a:
                        case 0x2b:
                        case 0x2c:
                        case 0x2d:
                        case 0x2e:
                        case 0x2f:
                        case 0x30:
                        case 0x33:
                        case 0x34:
                            this.getTopRightMenu().showFactionTabBar(true);
                            break;

                        case 60:
                            this.getTopRightMenu().showVillageTab(false);
                            break;

                        case 0x3ee:
                        case 0x452:
                        case 0x4b6:
                        case 0x51a:
                            this.getVillageTabBar().changeTabGfxOnly(6);
                            break;

                        case 0x3ef:
                        case 0x3f1:
                        case 0x453:
                        case 0x4b7:
                        case 0x51b:
                            this.getVillageTabBar().changeTabGfxOnly(7);
                            break;

                        case 0x3f0:
                        case 0x454:
                        case 0x4b8:
                        case 0x51c:
                            this.getVillageTabBar().changeTabGfxOnly(5);
                            break;
                    }
                    this.StartDrawing();
                }
            }
        }

        public void SetVillageViewMode(bool viewOnly)
        {
            this.villageMapPanel.ViewOnly = viewOnly;
        }

        public void showAllVillagesScreen()
        {
            GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_ALL_VILLAGES;
            this.addMainWindow(false, true);
            this.setVillageTabSubMode(100, true);
            if (!GameEngine.Instance.World.isAccountPremium())
            {
                MyMessageBox.Show(SK.Text("AllVillageOverview_error2", "This feature requires a premium token to be in play."), SK.Text("AllVillageOverview_error", "Premium Village Overview"));
            }
        }

        public void showCapitalForumPosts(long threadID, long forumID, string threadTitle, int areaID, int areaType, string forumTitle)
        {
            CapitalForumPostsPanel.ThreadID = threadID;
            CapitalForumPostsPanel.parentForumID = forumID;
            CapitalForumPostsPanel.ThreadTitle = threadTitle;
            CapitalForumPostsPanel.ForumTitle = forumTitle;
            CapitalForumPostsPanel.areaID = areaID;
            CapitalForumPostsPanel.areaType = areaType;
            this.setVillageTabSubMode(0x3f1, false);
        }

        public void showCastlePieceInfo(string pieceName, int woodCost, int stoneCost, int ironCost, int pitchCost, int goldCost, string buildTimeString, bool rollover)
        {
            this.castleMapPanel.setCastleElementInfo(pieceName, woodCost, stoneCost, ironCost, pitchCost, goldCost, buildTimeString, rollover);
        }

        public void showDominationWindow()
        {
            this.dominationWindow = new DominationWindow();
            this.dominationWindow.Show(this.parentMainWindow);
        }

        public void showDXCardBar(int cardSection)
        {
            if (!DXPanel.skipPaint)
            {
                this.cardBarDX.init(cardSection);
            }
            else
            {
                this.cardBarDX.delayedInit(cardSection);
            }
        }

        public void showDXWindow(bool overlayTabBar)
        {
            int num = 0;
            if (overlayTabBar)
            {
                num = 0x1c;
            }
            this.parentMainWindow.setMainAreaVisible(true);
            Size size = new Size(this.parentMainWindow.getDXBasePanel().Width, (this.parentForm.ClientSize.Height - 120) + num);
            this.getTopLeftMenu().Height = 120 - num;
            this.getTopRightMenu().Height = 120 - num;
            this.getMainRightHandPanel().Height = (this.parentForm.ClientSize.Height - 120) + num;
            this.getMainRightHandPanel().Location = new Point(this.getMainRightHandPanel().Location.X, 120 - num);
            this.parentMainWindow.getDXBasePanel().Size = size;
            this.getTopLeftMenu().setContextBarVisible(!overlayTabBar);
            this.parentMainWindow.getDXBasePanel().Location = new Point(0, 120 - num);
            GameEngine.Instance.GFX.resizeWindow();
            if (GameEngine.Instance.World != null)
            {
                GameEngine.Instance.World.setScreenSize(this.parentMainWindow.getDXBasePanel().Width, this.parentMainWindow.getDXBasePanel().Height);
            }
        }

        public void showEditFactionPanel()
        {
            FactionStartFactionPanel.StartFaction = false;
            this.setVillageTabSubMode(0x2f, false);
        }

        public void showFactionForumPosts(long threadID, long forumID, string threadTitle, string forumTitle)
        {
            FactionNewForumPostsPanel.ThreadID = threadID;
            FactionNewForumPostsPanel.parentForumID = forumID;
            FactionNewForumPostsPanel.ThreadTitle = threadTitle;
            FactionNewForumPostsPanel.ForumTitle = forumTitle;
            this.setVillageTabSubMode(0x30, false);
        }

        public void showFactionPanel(int factionID)
        {
            if (this.getMainTabBar().getCurrentTab() != 8)
            {
                GameEngine.Instance.setNextFactionPage(0x2a);
                FactionMyFactionPanel.SelectedFaction = factionID;
                this.getMainTabBar().changeTab(8);
            }
            else if (this.getFactionTabBar().getCurrentTab() != 1)
            {
                GameEngine.Instance.setNextFactionPage(0x2a);
                FactionMyFactionPanel.SelectedFaction = factionID;
                this.getFactionTabBar().changeTab(1);
            }
            else
            {
                FactionMyFactionPanel.SelectedFaction = factionID;
                this.setVillageTabSubMode(0x2a, false);
            }
        }

        public void showFactionTabBar()
        {
            this.getTopRightMenu().showFactionTabBar(true);
        }

        public void showHousePanel(int houseID)
        {
            if (this.getMainTabBar().getCurrentTab() != 8)
            {
                GameEngine.Instance.setNextFactionPage(0x34);
                HouseInfoPanel.SelectedHouse = houseID;
                this.getMainTabBar().changeTab(8);
            }
            else if (this.getFactionTabBar().getCurrentTab() != 2)
            {
                GameEngine.Instance.setNextFactionPage(0x34);
                HouseInfoPanel.SelectedHouse = houseID;
                this.getFactionTabBar().changeTab(2);
            }
            else
            {
                HouseInfoPanel.SelectedHouse = houseID;
                this.setVillageTabSubMode(0x34, false);
            }
        }

        public void showInBuildingInfo(VillageMapBuilding building)
        {
            this.villageMapPanel.showInBuildingInfo(building);
        }

        public void showMapFilterPanel()
        {
            this.clearControls();
            this.mapFilterPanel.initProperties(true, "", null);
            this.mapFilterPanel.display(this.parentMainWindow.getMainRightHandPanel(), 0, 0);
            this.mapFilterPanel.init();
        }

        public void showMapFilterSelectPanel(bool show, bool showAsOpen)
        {
            this.showMapFilterSelectPanel(show, showAsOpen, false, false);
        }

        public void showMapFilterSelectPanel(bool show, bool showAsOpen, bool force, bool forceDoubleHeight)
        {
            if (!show)
            {
                this.mapFilterSelectPanel.closeControl(true);
            }
            else
            {
                int height = this.ParentForm.Height;
                bool doubleHeight = false;
                if ((height >= 750) || forceDoubleHeight)
                {
                    doubleHeight = true;
                }
                if (!this.mapFilterSelectPanel.isVisible())
                {
                    this.mapFilterSelectPanel.initProperties(true, "", null);
                    this.mapFilterSelectPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
                    if (doubleHeight)
                    {
                        this.mapFilterSelectPanel.display(this.parentMainWindow.getMainRightHandPanel(), 11, this.parentMainWindow.getMainRightHandPanel().Height - 60);
                    }
                    else
                    {
                        this.mapFilterSelectPanel.display(this.parentMainWindow.getMainRightHandPanel(), 11, this.parentMainWindow.getMainRightHandPanel().Height - 30);
                    }
                }
                else if (force)
                {
                    if (doubleHeight)
                    {
                        if (this.mapFilterSelectPanel.Size.Height < 40)
                        {
                            this.mapFilterSelectPanel.setPosition(11, this.parentMainWindow.getMainRightHandPanel().Height - 60);
                        }
                    }
                    else if (this.mapFilterSelectPanel.Size.Height > 40)
                    {
                        this.mapFilterSelectPanel.setPosition(11, this.parentMainWindow.getMainRightHandPanel().Height - 30);
                    }
                }
                this.mapFilterSelectPanel.init(showAsOpen, doubleHeight);
            }
        }

        public void showParishPanel(int parishID)
        {
        }

        public void showStartFactionPanel()
        {
            FactionStartFactionPanel.StartFaction = true;
            this.setVillageTabSubMode(0x2f, false);
        }

        public void showUserInfo()
        {
            if (!this.userInfoPanel.isVisible())
            {
                this.userInfoPanel.display(false, this.parentMainWindow.getMainRightHandPanel(), 0, 0xb6);
                this.userInfoPanel.init();
                this.userInfoRefreshCountdown = 5;
            }
            this.userInfoPanel.SendToBack();
            if ((this.userInfoPanel.Parent != null) && (this.userInfoRefreshCountdown > 0))
            {
                this.userInfoRefreshCountdown--;
                if (this.userInfoRefreshCountdown > 0)
                {
                    foreach (Control control in this.userInfoPanel.Parent.Controls)
                    {
                        if (control != this.userInfoPanel)
                        {
                            control.Invalidate();
                        }
                    }
                }
            }
        }

        public void showUserInfoScreen(WorldMap.CachedUserInfo userInfo)
        {
            GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_USER_INFO;
            this.addMainWindow(false, true);
            this.setVillageTabSubMode(0x63, true);
            this.villageReportBackgroundPanel.userInfoScreen.init(userInfo);
        }

        public void showUserInfoScreenAdmin(WorldMap.CachedUserInfo userInfo)
        {
            if (this.getMainTabBar().getCurrentTab() != 0)
            {
                this.getMainTabBar().changeTab(0);
            }
            this.clearControlsLeaveRightHandPanel();
            this.addMainWindow(false, true);
            this.userInfoScreen.Size = this.mainWindowPanel.Size;
            this.userInfoScreen.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.userInfoScreen.clear();
            this.userInfoScreen.init(userInfo);
            this.userInfoScreen.display(this.mainWindowPanel, 0, 0);
        }

        public void ShowViewBattleResults(bool attackerVictory, BattleTroopNumbers startingTroops, BattleTroopNumbers endingTroops, int villageID, GetReport_ReturnType reportReturnData)
        {
            this.castleMapBattlePanel.ShowViewBattleResults(attackerVictory, startingTroops, endingTroops, villageID, reportReturnData);
        }

        public void showVillageBuildingInfo(string buildingName, int woodCost, int stoneCost, int clayCost, int goldCost, int flagsNeeded, string buildTimeString, int buildingType, int realBuildingType)
        {
            this.villageMapPanel.setBuildingInfo(buildingName, woodCost, stoneCost, clayCost, goldCost, flagsNeeded, buildTimeString, buildingType, realBuildingType);
        }

        public void showVillageStats(int taxLevel, int rationsLevel, int aleRationsLevel, int popularity, double popularityChange, string timeLeftString, string migrationTimeString, double effectiveRationsLevel, int numFoodTypesEaten, double effectiveAleRationsLevel, double housingChangeLevel, double goldDayRate, double dailyFoodConsumption, int totalPeople, int housingCapacity, int numPositiveBuildings, int numNegativeBuildings, PopEventData[] popEvents, double dailyAleConsumption, DateTime curTime, double foodProductionRate, double aleProductionRate, int numPopularityBuildings, int parishTax)
        {
            this.villageMapPanel.showStats(taxLevel, rationsLevel, aleRationsLevel, popularity, popularityChange, timeLeftString, effectiveRationsLevel, numFoodTypesEaten, effectiveAleRationsLevel, housingChangeLevel);
            this.villageMapPanel.showMigration(popularity, migrationTimeString, totalPeople, housingCapacity);
            this.villageMapPanel.showGoldChange(GameEngine.Instance.World.getCurrentGold(), GameEngine.Instance.World.getCurrentGoldRate());
            this.villageMapPanel.showDayRates(goldDayRate, dailyFoodConsumption, dailyAleConsumption, foodProductionRate, aleProductionRate, parishTax);
            this.villageMapPanel.showBuildingInfo(numPositiveBuildings, numNegativeBuildings, numPopularityBuildings);
            this.villageMapPanel.showPopEvents(popEvents, curTime);
        }

        public void showVillageStats2(int numChurches, int numChapels, int numCathedrals, int numSmallGardens, int numLargeGardens, int numSmallStatues, int numLargeStatues, int numDovecotes, int numStocks, int numBurningPosts, int numGibbets, int numRacks, bool lastbanquetStored, double lastBanquetHonour, DateTime lastBanquetDate, double lastTributePayment, double popularityLevel, int capitalTaxRate, int parishTax, ParishTaxCalc[] parishTaxPeople, int parentCapitalTaxRate, int lastCapitalTaxRate, int parishBonus)
        {
            this.villageMapPanel.showHonour();
            this.villageMapPanel.showHonourBuildings(numChurches, numChapels, numCathedrals, numSmallGardens, numLargeGardens, numSmallStatues, numLargeStatues, numDovecotes, numStocks, numBurningPosts, numGibbets, numRacks, popularityLevel, parishBonus);
            if (lastbanquetStored)
            {
                this.villageMapPanel.showHonourBanquet(lastBanquetHonour, lastBanquetDate);
            }
            else
            {
                this.villageMapPanel.showHonourBanquet(0.0, lastBanquetDate);
            }
            this.villageMapPanel.showCapitalData(capitalTaxRate, parishTax, parishTaxPeople, parentCapitalTaxRate, lastCapitalTaxRate);
        }

        public void showVillageTabBar()
        {
            this.getTopRightMenu().showVillageTab(true);
        }

        public void StartDrawing()
        {
        }

        public void StopDrawing()
        {
        }

        public void stopIndustryEnabled()
        {
            this.villageMapPanel.stopIndustryEnabled();
        }

        public void switchReportTabs(int tabID)
        {
            this.mainWindowPanel.Controls.Clear();
            switch (tabID)
            {
                case 0:
                    this.initReportsReports();
                    break;

                case 1:
                case 2:
                    break;

                default:
                    return;
            }
        }

        public void toggleDXCardBarActive(bool value)
        {
            this.cardBarDX.toggleEnabled(value);
        }

        public void tradeWithResume(int villageID, bool keepInfo)
        {
            this.villageReportBackgroundPanel.tradeWithResume(villageID, keepInfo);
        }

        public bool TUTORIAL_openedWoodTab()
        {
            return this.castleMapPanel.TUTORIAL_openedWoodTab();
        }

        public void updateAllArmiesPanel()
        {
        }

        public void updateDominationWindow(string text)
        {
            if (this.dominationWindow != null)
            {
                this.dominationWindow.updateText(text);
            }
        }

        public void updateDXCardBar()
        {
            this.cardBarDX.update();
        }

        public void updateLeaderboard()
        {
        }

        public void updatePersonInfo()
        {
            if (this.personInfoPanel.isVisible())
            {
                this.personInfoPanel.update();
            }
        }

        public void updatePopups()
        {
            if (this.isTutorialArrowWindowOpen())
            {
                TutorialArrowWindow.updateArrow();
            }
            bool flag = true;
            if (this.isPopupWindowCreated(this.m_playCardsWindow))
            {
                this.m_playCardsWindow.update();
                Form activeForm = Form.ActiveForm;
                if ((Form.ActiveForm == this.ParentForm) || ((this.m_launchAttackPopup != null) && (Form.ActiveForm == this.m_launchAttackPopup)))
                {
                    this.m_playCardsWindow.Focus();
                }
                flag = false;
            }
            if (this.isPopupWindowCreated(this.m_launchAttackPopup))
            {
                this.m_launchAttackPopup.update();
                if (flag)
                {
                    Form form2 = Form.ActiveForm;
                    if (Form.ActiveForm == this.ParentForm)
                    {
                        this.m_launchAttackPopup.Focus();
                    }
                    flag = false;
                }
            }
            if (this.isPopupWindowCreated(this.m_scoutPopupWindow))
            {
                this.m_scoutPopupWindow.update();
                if (flag)
                {
                    Form form3 = Form.ActiveForm;
                    if ((Form.ActiveForm == this.ParentForm) || ((this.m_launchAttackPopup != null) && (Form.ActiveForm == this.m_scoutPopupWindow)))
                    {
                        this.m_scoutPopupWindow.Focus();
                    }
                    flag = false;
                }
            }
            if (this.isPopupWindowCreated(this.m_sendMonkWindow))
            {
                this.m_sendMonkWindow.update();
                if (flag)
                {
                    Form form4 = Form.ActiveForm;
                    if ((Form.ActiveForm == this.ParentForm) || ((this.m_launchAttackPopup != null) && (Form.ActiveForm == this.m_sendMonkWindow)))
                    {
                        this.m_sendMonkWindow.Focus();
                    }
                    flag = false;
                }
            }
            if (this.isPopupWindowCreated(this.m_buyVillageWindow))
            {
                this.m_buyVillageWindow.update();
                if (flag)
                {
                    Form form5 = Form.ActiveForm;
                    if (Form.ActiveForm == this.ParentForm)
                    {
                        this.m_buyVillageWindow.Focus();
                    }
                    flag = false;
                }
            }
            if (this.isPopupWindowCreated(this.m_connectionErrorWindow))
            {
                this.m_connectionErrorWindow.update();
                if (flag)
                {
                    Form form6 = Form.ActiveForm;
                    if (Form.ActiveForm == this.ParentForm)
                    {
                        this.m_connectionErrorWindow.Focus();
                    }
                    flag = false;
                }
            }
            if (this.isPopupWindowCreated(this.m_currentDonatePopup))
            {
                Form form7 = Form.ActiveForm;
                if (Form.ActiveForm != this.m_currentDonatePopup)
                {
                    this.closeDonatePopup();
                }
            }
            if (this.isPopupWindowCreated(this.m_logoutOptionsWindow))
            {
                this.m_logoutOptionsWindow.update();
                Form form8 = Form.ActiveForm;
                if (Form.ActiveForm == this.ParentForm)
                {
                    this.m_logoutOptionsWindow.Focus();
                }
            }
            if (this.isPopupWindowCreated(this.m_reportCapturePopup))
            {
                this.m_reportCapturePopup.update();
                Form form9 = Form.ActiveForm;
                if (Form.ActiveForm == this.ParentForm)
                {
                    this.m_reportCapturePopup.Focus();
                }
            }
            if (this.isPopupWindowCreated(this.m_newQuestRewardPopup))
            {
                this.m_newQuestRewardPopup.update();
                Form form10 = Form.ActiveForm;
                if (Form.ActiveForm == this.ParentForm)
                {
                    this.m_newQuestRewardPopup.Focus();
                }
            }
            if (this.isPopupWindowCreated(this.m_advancedCastleOptionsPopup))
            {
                this.m_advancedCastleOptionsPopup.update();
                Form form11 = Form.ActiveForm;
                if (Form.ActiveForm == this.ParentForm)
                {
                    this.m_advancedCastleOptionsPopup.Focus();
                }
            }
            if (this.isPopupWindowCreated(this.m_freeCardsPopup))
            {
                this.m_freeCardsPopup.update();
                Form form12 = Form.ActiveForm;
                if (Form.ActiveForm == this.ParentForm)
                {
                    this.m_freeCardsPopup.Focus();
                }
            }
            if (this.isPopupWindowCreated(this.m_WheelPopup))
            {
                this.m_WheelPopup.update();
                Form form13 = Form.ActiveForm;
                if (Form.ActiveForm == this.ParentForm)
                {
                    this.m_WheelPopup.Focus();
                }
            }
            if (this.isPopupWindowCreated(this.m_createPopupWindow))
            {
                this.m_createPopupWindow.update();
            }
            if (this.isPopupWindowCreated(this.m_VacationCancelPopupWindow))
            {
                this.m_VacationCancelPopupWindow.update();
            }
            if (this.isPopupWindowCreated(this.m_worldSelectPopupWindow))
            {
                this.m_worldSelectPopupWindow.update();
            }
            if (this.isPopupWindowCreated(this.m_BPPopupWindow))
            {
                this.m_BPPopupWindow.update();
            }
            if (this.isPopupWindowCreated(this.m_currentMenuPopup))
            {
                this.m_currentMenuPopup.update();
            }
            if (this.isPopupWindowCreated(this.m_achievementPopup))
            {
                this.m_achievementPopup.update();
                if (!this.m_achievementPopup.isActive())
                {
                    this.m_achievementPopup.Hide();
                    this.m_achievementPopup = null;
                    if (this.nextAchievementIDs.Count > 0)
                    {
                        this.m_achievementPopup = new AchievementPopup();
                        this.m_achievementPopup.activate(this.nextAchievementIDs[0]);
                        this.nextAchievementIDs.RemoveAt(0);
                    }
                }
            }
        }

        public void updateQuestsPanel()
        {
        }

        public void updateReports()
        {
        }

        public void updateResearch(bool fullTick)
        {
            this.researchPanel.update(fullTick);
        }

        public void updateSidepanelAfterBuildingPlaced()
        {
            this.villageMapPanel.refreshCurrentTab();
        }

        public void updateTraderInfo()
        {
            if (this.traderInfoPanel.isVisible())
            {
                this.traderInfoPanel.update();
            }
        }

        public void updateVillageInfoBar()
        {
            if (this.getVillageTabBar().getCurrentTab() == 0)
            {
                bool flag = GameEngine.Instance.World.isCapital(this.getSelectedMenuVillage());
                if (!this.villageInfoBar.isVisible() || (this.getVillageTabBar().lastVillageCapital != flag))
                {
                    this.villageInfoBar.show();
                    this.villageInfoBar.removeHeading();
                }
            }
            else if (((this.getVillageTabBar().getCurrentTab() == 3) && GameEngine.Instance.World.isCapital(this.getSelectedMenuVillage())) && (!this.villageInfoBar.isVisible() || !this.getVillageTabBar().lastVillageCapital))
            {
                this.villageInfoBar.show();
                this.villageInfoBar.removeHeading();
            }
        }

        public bool updateVillageReports()
        {
            if (this.lastVillageTab == -1)
            {
                return true;
            }
            this.villageReportBackgroundPanel.update(this.lastVillageTab);
            return false;
        }

        public void userInfoUpdate()
        {
            GameEngine.Instance.World.monitorCachedVillageUserInfo();
            WorldMap.VillageRolloverInfo villageInfo = null;
            WorldMap.CachedUserInfo userInfo = null;
            GameEngine.Instance.World.retrieveUserData(this.lastViewedVillage, -1, ref villageInfo, ref userInfo, false, false);
            if (userInfo != null)
            {
                this.showUserInfo();
                this.userInfoPanel.updateVillageInfo(villageInfo, userInfo);
            }
            else
            {
                this.closeUserInfo();
            }
        }

        public void validateUserVillage()
        {
            if (((this.m_selectedMenuVillage >= 0) && !GameEngine.Instance.World.isUserVillage(this.m_selectedMenuVillage)) && !GameEngine.Instance.World.isUserRelatedVillage(this.m_selectedMenuVillage))
            {
                this.getMainTabBar().changeTab(0);
                this.setupVillageName();
            }
        }

        public void villageChanged(int villageID)
        {
            if (GameEngine.Instance.Village != null)
            {
                GameEngine.Instance.Village.leaveMap();
            }
            this.villageMapPanel.showAsVillage(!this.isSelectedVillageACapital(villageID));
            this.villageMapPanel.showNewInterface();
            VillageMap.closePopups();
        }

        public void villageDownloaded(int villageID)
        {
            GameEngine.Instance.villageHasBeenDownloaded = true;
            ScoutPopupWindow window = this.getScoutPopupWindow();
            if (((window != null) && window.Visible) && window.Created)
            {
                window.villageLoaded(villageID);
            }
            else
            {
                Instance.getVillageTabBar().updateShownTabs();
                if ((this.getVillageTabBar().getCurrentTab() > 1) || !this.getVillageTabBar().Visible)
                {
                    this.villageReportBackgroundPanel.newVillageLoaded();
                }
                else if (this.getVillageTabBar().getCurrentTab() == 1)
                {
                    this.initCastleTab();
                }
                else if (this.getVillageTabBar().getCurrentTab() == 0)
                {
                    this.initVillageTab_Quick();
                    if (this.villageReportBackgroundPanel.isTab0OverLayActive())
                    {
                        this.villageReportBackgroundPanel.newVillageLoaded();
                    }
                }
            }
        }

        public void villageMapResizeWindow()
        {
            this.villageMapPanel.Height = this.parentMainWindow.getMainRightHandPanel().Height;
        }

        public void villageReshowAfterStockpilePlaced()
        {
            this.villageMapPanel.villageReshowAfterStockpilePlaced();
        }

        public bool wasShowingVassalSendScreen()
        {
            return (this.lastVillageTab == 15);
        }

        public void worldTabUpdate(bool special)
        {
            if (this.attackTargetSidePanel.isVisible())
            {
                this.attackTargetSidePanel.update();
            }
            if (this.monkTargetSidePanel.isVisible())
            {
                this.monkTargetSidePanel.update();
            }
            if (this.reinforcementTargetSidePanel.isVisible())
            {
                this.reinforcementTargetSidePanel.update();
            }
            if (this.scoutTargetSidePanel.isVisible())
            {
                this.scoutTargetSidePanel.update();
            }
            if (this.stockExchangeSidePanel.isVisible())
            {
                this.stockExchangeSidePanel.update();
            }
            if (this.tradeWithPanel.isVisible())
            {
                this.tradeWithPanel.update();
            }
            if (this.vassalAttackVillagePanel.isVisible())
            {
                this.vassalAttackVillagePanel.update();
            }
            if (this.vassalSelectSidePanel.isVisible())
            {
                this.vassalSelectSidePanel.update();
            }
            if (this.emptyVillagePanel.isVisible())
            {
                this.emptyVillagePanel.update();
            }
            if (this.selectArmyPanel.isVisible())
            {
                this.selectArmyPanel.update();
            }
            if (this.selectReinforcementPanel.isVisible())
            {
                this.selectReinforcementPanel.update();
            }
            if (this.userInfoScreen.isVisible())
            {
                this.userInfoScreen.update();
            }
            if (this.doUserInfoUpdate)
            {
                this.userInfoUpdate();
            }
            if (this.parishCapitalVillagePanel.isVisible())
            {
                this.parishCapitalVillagePanel.update();
            }
            if (this.countyCapitalVillagePanel.isVisible())
            {
                this.countyCapitalVillagePanel.update();
            }
            if (this.countryCapitalVillagePanel.isVisible())
            {
                this.countryCapitalVillagePanel.update();
            }
            if (this.provinceCapitalVillagePanel.isVisible())
            {
                this.provinceCapitalVillagePanel.update();
            }
            if (this.ownParishCapitalPanel.isVisible())
            {
                this.ownParishCapitalPanel.update();
            }
            if (this.ownCountyCapitalPanel.isVisible())
            {
                this.ownCountyCapitalPanel.update();
            }
            if (this.ownProvinceCapitalPanel.isVisible())
            {
                this.ownProvinceCapitalPanel.update();
            }
            if (this.ownCountryCapitalPanel.isVisible())
            {
                this.ownCountryCapitalPanel.update();
            }
            if (this.ownVillagePanel.isVisible())
            {
                this.ownVillagePanel.update();
            }
            if (this.otherVillagePanel.isVisible())
            {
                this.otherVillagePanel.update();
            }
            if (this.vassalVillagePanel.isVisible())
            {
                this.vassalVillagePanel.update();
            }
            if (this.mapFilterPanel.isVisible())
            {
                this.mapFilterPanel.update();
            }
        }

        public int AttackTargetHomeVillage
        {
            get
            {
                return this.attackTargetHomeVillage;
            }
            set
            {
                this.attackTargetHomeVillage = value;
            }
        }

        public Form ChatForm
        {
            get
            {
                return this.chatScreenManager.ChatForm();
            }
        }

        public int CourtierHomeVillage
        {
            get
            {
                return this.courtierHomeVillage;
            }
            set
            {
                this.courtierHomeVillage = value;
            }
        }

        public string FloatingInputString
        {
            get
            {
                return this.m_floatingInputString;
            }
        }

        public int FloatingInputValue
        {
            get
            {
                return this.m_floatingInputValue;
            }
        }

        public static InterfaceMgr Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InterfaceMgr();
                }
                return instance;
            }
        }

        public int LastVillageTab
        {
            get
            {
                return this.lastVillageTab;
            }
        }

        public int MonkSelectHomeVillage
        {
            get
            {
                return this.monkSelectHomeVillage;
            }
            set
            {
                this.monkSelectHomeVillage = value;
            }
        }

        public int OwnSelectedVillage
        {
            get
            {
                return this.m_ownSelectedVillage;
            }
        }

        public Form ParentForm
        {
            get
            {
                return this.parentForm;
            }
        }

        public MainWindow ParentMainWindow
        {
            get
            {
                return this.parentMainWindow;
            }
        }

        public int SelectedVassalVillage
        {
            get
            {
                return this.m_selectedVassalVillage;
            }
        }

        public int SelectedVillage
        {
            get
            {
                return this.m_reallySelectedVillage;
            }
            set
            {
                this.m_reallySelectedVillage = value;
                GameEngine.Instance.World.createTributeLinesList(this.m_reallySelectedVillage);
            }
        }

        public int StockExchangeBuyingVillage
        {
            get
            {
                return this.stockExchangeBuyingVillage;
            }
            set
            {
                this.stockExchangeBuyingVillage = value;
            }
        }

        public int VassalSelectHomeVillage
        {
            get
            {
                return this.vassalSelectHomeVillage;
            }
            set
            {
                this.vassalSelectHomeVillage = value;
            }
        }

        public int WorldMapMode
        {
            get
            {
                return this.worldMapMode;
            }
        }

        public delegate void FloatingTextSent(string text);

        public delegate void FloatingValueSent(int value);
    }
}

