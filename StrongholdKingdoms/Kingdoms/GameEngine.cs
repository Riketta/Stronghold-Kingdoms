namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using StatTracking;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using System.Threading;
    using System.Windows.Forms;

    public class GameEngine
    {
        private static IntPtr _hookID = IntPtr.Zero;
        private static LowLevelKeyboardProc _proc = new LowLevelKeyboardProc(GameEngine.HookCallback);
        private bool appClose;
        private Audio audio;
        private string[] badWords;
        public CastleMap castle;
        private CastleMap castle_AttackerSetup;
        private CastleMap castle_Battle;
        private SparseArray castles = new SparseArray();
        public int clockFrame;
        public int clockMode;
        public string connectionErrorString = "";
        private int currentResolution = -1;
        private DebugPopup dPop;
        private FontDesc dxFont1 = new FontDesc();
        private FontDesc dxFont2 = new FontDesc();
        public static bool enterPressed = false;
        public static bool f11Pressed = false;
        private MouseClickMessageFilter Filter;
        public bool finaliseResize;
        private bool firstCall = true;
        private bool forceTriggerFullTick;
        public bool forcingLogout;
        private const long FRAME_TIME = 0x21L;
        private GameDisplays gameDisplayMode = GameDisplays.DISPLAY_WORLD;
        private GameDisplaySubModes gameDisplayModeSubMode;
        private GraphicsMgr gfx;
        private bool gfxLoaded;
        private MouseInputState inputState;
        public static GameEngine Instance = null;
        private static bool keyboardHookedInstalled = false;
        private static string langPath = null;
        private int lastFactionTabID = -1;
        private long lastFrameTime;
        private double lastFullTickRegisterTime;
        private double lastFullTickTime;
        private static int lastKeyPressed = 0;
        public int lastLoadedCastle = -1;
        private int lastLoadedVillage = -1;
        public Point lastMouseMovePosition;
        public DateTime lastMouseMoveTime = DateTime.Now;
        private DateTime lastSoundClear = DateTime.MinValue;
        private int lastTabID = -1;
        private int lastVillageTabID = -1;
        private LostVillageWindow lostVillagePopup;
        private bool m_cancelLoading;
        private bool m_doReLogin;
        private ServerDowntimePopup m_downtimePopup;
        private bool m_firstDrawArmy = true;
        private bool m_firstDrawDummy = true;
        private bool m_firstDrawFactions = true;
        private bool m_firstDrawLeaderboard = true;
        private bool m_firstDrawMail = true;
        private bool m_firstDrawQuest = true;
        private bool m_firstDrawRank = true;
        private bool m_firstDrawReports = true;
        private bool m_firstDrawResearch = true;
        private LoginHistoryPopup m_loginHistoryPop;
        private ProfileLoginWindow m_loginWindow;
        private System.Threading.Timer m_tickTimer;
        private Thread m_WorkerThread;
        private int maxResolution = -1;
        public int movedFromVillageID = -1;
        public int movedFromVillageIDNonCapital = -1;
        private int newResolution = -1;
        private int nextFactionPage = -1;
        private static NumberFormatInfo nfi = null;
        private static NumberFormatInfo nfi_decimal = null;
        private static NumberFormatInfo nfi_decimal1 = null;
        private static NumberFormatInfo nfi_decimal2 = null;
        public NewAutoSelectVillageWindow noAutoVillagePopup;
        private NewSelectVillageAreaWindow noVillagePopup;
        private int pendingErrorCode = -1;
        private bool pendingUserVillageZoom;
        private int previousTabID = -1;
        private bool quitGame;
        public List<int> recentCards = new List<int>();
        public static bool scrollDown = false;
        public static bool scrollLeft = false;
        public static bool scrollRight = false;
        public static bool scrollUp = false;
        private int sentAttackingVillageID = -1;
        private int sentParentVillageID = -1;
        private int sentTargetVillageID = -1;
        private DateTime serverDowntime = DateTime.MinValue;
        private bool serverOffline;
        public static bool shiftPressed = false;
        public static bool shiftPressedAlways = false;
        private bool skipVillageTab;
        private static Censor staticCensor = null;
        public bool stopInterfaceSounds;
        public static bool StopKeyTrap = false;
        public static bool tabPressed = false;
        public static bool tabReleased = false;
        private int tickCount;
        private bool ticked;
        public int tryingToJoinCounty = -2;
        private static bool updatedPermissions = false;
        private static string userPath = null;
        private static string userPathBase = null;
        private VillageMap village;
        public bool villageHasBeenDownloaded;
        private SparseArray villages = new SparseArray();
        public int villageToAbandon = -1;
        private bool warning12H;
        private bool warning15;
        private bool warning24H;
        private bool warning30;
        private bool warning4H;
        private bool warning5;
        private bool warning60;
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE_LL = 7;
        private bool windowActive;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_MBUTTONDBLCLK = 0x209;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_MBUTTONUP = 520;
        private const int WM_MOUSEMOVE = 0x200;
        private const int WM_RBUTTONDBLCLK = 0x206;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_RBUTTONUP = 0x205;
        private WorldMap world = new WorldMap();
        private WorldData worldData;
        private WorldMapTypes worldMapTypesData;

        public GameEngine()
        {
            Instance = this;
        }

        public static void AddDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            DirectoryInfo info = new DirectoryInfo(FileName);
            DirectorySecurity accessControl = info.GetAccessControl();
            accessControl.AddAccessRule(new FileSystemAccessRule(Account, Rights, InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, ControlType));
            accessControl.AddAccessRule(new FileSystemAccessRule(Account, Rights, InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, ControlType));
            accessControl.AddAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));
            info.SetAccessControl(accessControl);
        }

        public void addRecentCard(int newCard)
        {
            if (this.recentCards.Contains(newCard))
            {
                this.recentCards.Remove(newCard);
            }
            if (this.recentCards.Count == 8)
            {
                this.recentCards.RemoveAt(7);
            }
            this.recentCards.Insert(0, newCard);
        }

        public void addRecentCardsFromServer(int[] cards)
        {
            int num = 0;
            this.recentCards.Clear();
            if (cards != null)
            {
                foreach (int num2 in cards)
                {
                    num++;
                    this.recentCards.Add(CardTypes.getCardType(num2));
                    if (num == 8)
                    {
                        break;
                    }
                }
            }
        }

        public void appClosing()
        {
            this.appClose = true;
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        public void cancelInterdictionCallback(CancelInterdiction_ReturnType returnData)
        {
            if (returnData.Success)
            {
                RemoteServices.Instance.set_PreAttackSetup_UserCallBack(new RemoteServices.PreAttackSetup_UserCallBack(this.preAttackSetupCallback));
                RemoteServices.Instance.PreAttackSetup(this.sentParentVillageID, this.sentAttackingVillageID, this.sentTargetVillageID, 0, 0, 0, 0, 0, 0, 0, 0);
            }
        }

        public bool cancelLoading()
        {
            return this.m_cancelLoading;
        }

        public string censorString(string text)
        {
            if (this.badWords == null)
            {
                return text;
            }
            if (staticCensor == null)
            {
                staticCensor = new Censor(this.badWords);
            }
            return staticCensor.CensorText(text);
        }

        public void chatSessionExpired(int errorNo)
        {
        }

        public void clearDowntimePopup()
        {
            if (this.m_downtimePopup != null)
            {
                if (this.m_downtimePopup.Created && this.m_downtimePopup.Visible)
                {
                    this.m_downtimePopup.Close();
                }
                this.m_downtimePopup = null;
            }
        }

        public void clearServerDowntime()
        {
            this.serverDowntime = DateTime.MinValue;
            this.warning5 = false;
            this.warning15 = false;
            this.warning30 = false;
            this.warning60 = false;
            this.serverOffline = false;
        }

        public void closeNoVillagePopup(bool pendingVillage)
        {
            if (this.noVillagePopup != null)
            {
                InterfaceMgr.Instance.ParentForm.Enabled = true;
                this.noVillagePopup.closePopup();
                this.noVillagePopup.Close();
                this.noVillagePopup = null;
                InterfaceMgr.Instance.closeGreyOut();
            }
            if (this.lostVillagePopup != null)
            {
                if (this.lostVillagePopup.isCardsPopup())
                {
                    this.lostVillagePopup.closePopup();
                    this.lostVillagePopup.Close();
                    this.lostVillagePopup = null;
                }
                else
                {
                    InterfaceMgr.Instance.ParentForm.Enabled = true;
                    this.lostVillagePopup.closePopup();
                    this.lostVillagePopup.Close();
                    this.lostVillagePopup = null;
                    InterfaceMgr.Instance.closeGreyOut();
                }
            }
            if (this.noAutoVillagePopup != null)
            {
                InterfaceMgr.Instance.ParentForm.Enabled = true;
                this.noAutoVillagePopup.closePopup();
                this.noAutoVillagePopup.Close();
                this.noAutoVillagePopup = null;
                InterfaceMgr.Instance.closeGreyOut();
            }
            if (pendingVillage)
            {
                this.pendingUserVillageZoom = true;
            }
        }

        private void debugPopupRun()
        {
            if ((this.dPop != null) && this.dPop.Created)
            {
                this.dPop.run();
            }
        }

        public void DisableMouseClicks()
        {
            if (this.Filter == null)
            {
                this.Filter = new MouseClickMessageFilter();
                Application.AddMessageFilter(this.Filter);
            }
        }

        public static void displayDirectXError()
        {
            MessageBox.Show(SK.Text("GameEngine_DX_problem", "There is a problem with DirectX, please contact Support."), SK.Text("GameEngine_DX_Error", "DirectX Error"));
            Application.Exit();
        }

        public void displayedVillageLost(int villageID, bool popup)
        {
            InterfaceMgr.Instance.closeVillageTab();
            InterfaceMgr.Instance.closeCastleTab();
            this.world.updateWorldMapOwnership();
            if (popup)
            {
                MyMessageBox.Show(SK.Text("GameEngine_Lost_Control_Of_Village", "You have lost control of this village!"), SK.Text("GENERIC_Error", "Error"));
            }
            if (this.villages[villageID] != null)
            {
                this.villages[villageID] = null;
                this.village = null;
            }
            InterfaceMgr.Instance.getMainTabBar().changeTab(9);
            InterfaceMgr.Instance.getMainTabBar().changeTab(0);
        }

        public void downloadCurrentCastle()
        {
        }

        public void downloadCurrentVillage()
        {
            int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            InterfaceMgr.Instance.villageChanged(villageID);
            InterfaceMgr.Instance.castleChanged();
            this.castle = null;
            this.village = null;
            this.lastLoadedVillage = villageID;
            bool needParishPeople = false;
            if ((this.villages[villageID] != null) && (this.castles[villageID] != null))
            {
                this.village = (VillageMap) this.villages[villageID];
                this.castle = (CastleMap) this.castles[villageID];
                this.village.moveMap(0, 0);
                this.castle.moveMap(0, 0);
                this.village.ViewOnly = false;
                if (this.World.isCapital(villageID) && this.village.needParishPeople())
                {
                    needParishPeople = true;
                }
                TimeSpan span = (TimeSpan) (DateTime.Now - this.village.lastDownloadedTime);
                if (span.TotalMinutes < 5.0)
                {
                    VillageMap.loadVillageBuildingsGFX2();
                    this.village.loadBackgroundImage();
                    this.village.reAddBuildingsToMap();
                    this.village.updateConstructionOnCachedLoad();
                    this.castle.reInitGFX();
                    CastleMap.CreateMode = false;
                    InterfaceMgr.Instance.villageDownloaded(villageID);
                    this.castle.castleShown(true);
                    return;
                }
            }
            else if (this.World.isCapital(villageID))
            {
                needParishPeople = true;
            }
            RemoteServices.Instance.GetVillageBuildingsList(villageID, true, needParishPeople);
            VillageMap.loadVillageBuildingsGFX2();
            if (this.village != null)
            {
                this.village.loadBackgroundImage();
            }
            if (this.castle != null)
            {
                this.castle.reInitGFX();
            }
            CastleMap.CreateMode = false;
        }

        public void enableConnectingPopup()
        {
        }

        public void enableConnectingPopup2()
        {
            if (this.m_loginWindow != null)
            {
                this.m_loginWindow.selfClose();
                this.m_loginWindow.Close();
            }
        }

        public void EnableMouseClicks()
        {
            if (this.Filter != null)
            {
                Application.RemoveMessageFilter(this.Filter);
                this.Filter = null;
            }
        }

        public void externalMainTabChange(int tabID)
        {
            this.lastTabID = tabID;
        }

        public void factionTabChange(int tabID)
        {
            if ((this.lastFactionTabID == tabID) && (tabID != 9))
            {
                return;
            }
            InterfaceMgr.Instance.StopDrawing();
            InterfaceMgr.Instance.getFactionTabBar().updateShownTabs();
            this.lastFactionTabID = tabID;
            InterfaceMgr.Instance.clearControls();
            this.gameDisplayModeSubMode = GameDisplaySubModes.SUBMODE_DEFAULT;
            switch (tabID)
            {
                case 0:
                    this.GameDisplayMode = GameDisplays.DISPLAY_FACTIONS;
                    InterfaceMgr.Instance.addMainWindow(this.m_firstDrawFactions, true);
                    this.m_firstDrawFactions = false;
                    InterfaceMgr.Instance.initGloryTab();
                    goto Label_0142;

                case 1:
                    this.GameDisplayMode = GameDisplays.DISPLAY_FACTIONS;
                    if ((this.nextFactionPage < 0) || (this.nextFactionPage == 0x3e7))
                    {
                        if (RemoteServices.Instance.UserFactionID >= 0)
                        {
                            InterfaceMgr.Instance.showFactionPanel(RemoteServices.Instance.UserFactionID);
                        }
                        else
                        {
                            InterfaceMgr.Instance.setVillageTabSubMode(0x29, false);
                        }
                        break;
                    }
                    InterfaceMgr.Instance.setVillageTabSubMode(this.nextFactionPage, false);
                    break;

                case 2:
                    this.GameDisplayMode = GameDisplays.DISPLAY_FACTIONS;
                    if (this.nextFactionPage < 0)
                    {
                        InterfaceMgr.Instance.setVillageTabSubMode(0x33, false);
                    }
                    else
                    {
                        InterfaceMgr.Instance.setVillageTabSubMode(this.nextFactionPage, false);
                    }
                    this.nextFactionPage = -1;
                    goto Label_0142;

                default:
                    goto Label_0142;
            }
            this.nextFactionPage = -1;
        Label_0142:
            InterfaceMgr.Instance.StartDrawing();
        }

        public void FlagQuitGame()
        {
            this.quitGame = true;
        }

        public void flushVillage(int villageID)
        {
            if (this.villages[villageID] != null)
            {
                VillageMap map = (VillageMap) this.villages[villageID];
                if (map != null)
                {
                    map.lastDownloadedTime = DateTime.MinValue;
                }
            }
        }

        public void flushVillages()
        {
            foreach (VillageMap map in this.villages)
            {
                if (map != null)
                {
                    map.lastDownloadedTime = DateTime.MinValue;
                }
            }
        }

        public void forceDownloadCurrentVillage()
        {
            int num = InterfaceMgr.Instance.getSelectedMenuVillage();
            this.villages[num] = null;
            this.downloadCurrentVillage();
        }

        public void forceFactionTabChange()
        {
            this.lastFactionTabID = -1;
        }

        public void forceFullTick()
        {
            this.forceTriggerFullTick = true;
        }

        public void forceLogout()
        {
            this.forcingLogout = true;
            InterfaceMgr.Instance.chatClose();
            this.m_doReLogin = true;
            this.World.invalidateWorldData();
            if ((this.dPop != null) && this.dPop.Created)
            {
                this.dPop.Close();
            }
            if ((this.m_loginHistoryPop != null) && this.m_loginHistoryPop.Created)
            {
                this.m_loginHistoryPop.Close();
            }
            this.pendingErrorCode = -1;
            InterfaceMgr.Instance.closeAllPopups();
        }

        public void forceRelogin()
        {
            this.m_doReLogin = true;
        }

        public void forceResetVillageIfChangedFromCapital()
        {
            if (this.World.isCapital(InterfaceMgr.Instance.getSelectedMenuVillage()))
            {
                if ((this.MovedFromVillageID >= 0) && !this.World.isCapital(this.MovedFromVillageID))
                {
                    InterfaceMgr.Instance.selectUserVillage(this.MovedFromVillageID, false);
                }
                else if ((this.movedFromVillageIDNonCapital >= 0) && !this.World.isCapital(this.movedFromVillageIDNonCapital))
                {
                    InterfaceMgr.Instance.selectUserVillage(this.movedFromVillageIDNonCapital, false);
                }
                else
                {
                    int movedFromVillageID = this.MovedFromVillageID;
                    List<int> list = this.World.getUserVillageIDList();
                    if (list.Count > 0)
                    {
                        InterfaceMgr.Instance.selectUserVillage(list[0], false);
                    }
                    this.MovedFromVillageID = movedFromVillageID;
                }
            }
        }

        public void forceVillageTabUpdate()
        {
            this.lastVillageTabID = -1;
        }

        public static string getCachePath()
        {
            string path = Path.Combine(getSettingsPath(false), "BrowserCache");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public void getCastleCallBack(GetVillageBuildingsList_ReturnType returnData)
        {
            if (returnData.Success)
            {
                int villageID = returnData.villageID;
                if (this.castles[villageID] == null)
                {
                    CastleMap map = new CastleMap(villageID, this.gfx, 0);
                    this.castles[villageID] = map;
                }
                CastleMap map2 = (CastleMap) this.castles[villageID];
                if (villageID == InterfaceMgr.Instance.getSelectedMenuVillage())
                {
                    this.castle = map2;
                }
                CastleMap.setServerTime(returnData.currentTime);
                map2.importElements(returnData.elements);
                map2.castleShown(true);
            }
        }

        public TimeSpan getDominationTimeLeft()
        {
            DateTime time = VillageMap.getCurrentServerTime();
            int days = Instance.World.getGameDay();
            TimeSpan span = new TimeSpan(days, time.Hour, time.Minute, time.Second);
            span -= new TimeSpan(14, 0, 0);
            return (new TimeSpan(0x3d, 0, 0, 0) - span);
        }

        public void getIngameMessageCallback(GetIngameMessage_ReturnType returnData)
        {
            if (returnData.Success && (returnData.message.Length > 0))
            {
                AdminInfoPopup.setMessage(returnData.message);
                AdminInfoPopup.showMessage();
            }
        }

        public static string getLangsPath()
        {
            if (langPath == null)
            {
                langPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Stronghold Kingdoms\";
                try
                {
                    Directory.CreateDirectory(langPath);
                }
                catch (Exception)
                {
                }
            }
            return langPath;
        }

        public ProfileLoginWindow getLoginWindow()
        {
            if ((this.m_loginWindow != null) && this.m_loginWindow.Created)
            {
                return this.m_loginWindow;
            }
            return null;
        }

        [DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        public static string getSettingsPath(bool createFolder)
        {
            if (userPath == null)
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                userPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                userPath = userPath + @"\";
                userPath = userPath + versionInfo.CompanyName;
                userPathBase = userPath;
                userPath = userPath + @"\";
                userPath = userPath + versionInfo.ProductName;
            }
            try
            {
                if (Directory.Exists(userPath) || !createFolder)
                {
                    updateFolderPermissions(userPath);
                    return userPath;
                }
                Directory.CreateDirectory(userPathBase);
                Directory.CreateDirectory(userPath);
                updateFolderPermissions(userPath);
            }
            catch (Exception)
            {
            }
            return userPath;
        }

        public VillageMap getVillage(int villageID)
        {
            if (villageID < 0)
            {
                return null;
            }
            if (this.villages[villageID] == null)
            {
                return null;
            }
            return (VillageMap) this.villages[villageID];
        }

        public void getVillageBuildingListCallBack(GetVillageBuildingsList_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.existingArmies != null)
                {
                    this.World.updateExistingArmies(returnData.existingArmies);
                }
                if (InterfaceMgr.Instance.getSelectedMenuVillage() == returnData.villageID)
                {
                    int villageID = returnData.villageID;
                    if (this.villages[villageID] == null)
                    {
                        VillageMap map = new VillageMap(returnData.mapID, returnData.mapVariant, returnData.mapType, villageID, this.gfx);
                        this.villages[villageID] = map;
                    }
                    bool flag = false;
                    VillageMap map2 = (VillageMap) this.villages[villageID];
                    if ((villageID == InterfaceMgr.Instance.getSelectedMenuVillage()) || returnData.viewOnly)
                    {
                        this.village = map2;
                        flag = true;
                    }
                    map2.resetMapType(returnData.mapID, returnData.mapVariant, returnData.mapType);
                    if (flag)
                    {
                        map2.loadBackgroundImage();
                        map2.reInitGFX(this.gfx);
                    }
                    map2.ViewOnly = returnData.viewOnly;
                    map2.ViewHonour = returnData.viewHonour;
                    map2.lastDownloadedTime = DateTime.Now;
                    if (returnData.parishTaxInfo != null)
                    {
                        map2.importParishTaxPeople(returnData.parishTaxInfo, returnData.currentTime);
                    }
                    VillageMap.setServerTime(returnData.currentTime);
                    if (returnData.fullUpdate)
                    {
                        map2.initClickMask();
                    }
                    map2.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    map2.importVillageBuildings(returnData.villageBuildings, returnData.fullUpdate);
                    if (!returnData.viewOnly)
                    {
                        map2.importTraders(returnData.traders, returnData.currentTime);
                    }
                    DXPanel.skipPaint = true;
                    if (this.lastVillageTabID == 0)
                    {
                        map2.playEnvironmentalSounds();
                    }
                    InterfaceMgr.Instance.villageDownloaded(returnData.villageID);
                    if (!returnData.viewOnly)
                    {
                        this.getCastleCallBack(returnData);
                    }
                    if (returnData.viewOnly)
                    {
                        InterfaceMgr.Instance.getMainTabBar().selectDummyTab(50);
                    }
                }
                if (!returnData.viewOnly)
                {
                    this.World.importOrphanedPeople(returnData.people, returnData.currentTime, returnData.villageID);
                }
            }
            else if ((returnData.m_errorCode == ErrorCodes.ErrorCode.VILLAGE_BUILDINGS_NO_LONGER_OWNER) && !returnData.viewOnly)
            {
                this.displayedVillageLost(returnData.villageID, true);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (Instance.GFX != null)
            {
                if ((nCode < 0) || !(wParam == ((IntPtr) 0x100)))
                {
                    lastKeyPressed = 0;
                    if ((nCode >= 0) && (wParam == ((IntPtr) 0x101)))
                    {
                        switch (Marshal.ReadInt32(lParam))
                        {
                            case 0x25:
                                scrollLeft = false;
                                break;

                            case 0x26:
                                scrollUp = false;
                                break;

                            case 0x27:
                                scrollRight = false;
                                break;

                            case 40:
                                scrollDown = false;
                                break;

                            case 9:
                                tabPressed = false;
                                tabReleased = true;
                                break;

                            case 160:
                            case 0xa1:
                                shiftPressed = false;
                                shiftPressedAlways = false;
                                break;

                            case 0xa2:
                            case 0xa3:
                                Instance.GFX.keyControlled = false;
                                break;
                        }
                    }
                }
                else
                {
                    Instance.lastMouseMoveTime = DateTime.Now;
                    int num = Marshal.ReadInt32(lParam);
                    switch (num)
                    {
                        case 160:
                        case 0xa1:
                            shiftPressedAlways = true;
                            break;
                    }
                    lastKeyPressed = num;
                    Form activeForm = Form.ActiveForm;
                    bool flag = false;
                    if ((!StopKeyTrap && (InterfaceMgr.Instance.ParentForm != null)) && ((activeForm == InterfaceMgr.Instance.ParentForm) || ((activeForm == InterfaceMgr.Instance.ChatForm) && (activeForm != null))))
                    {
                        bool flag2 = false;
                        if (((Instance.GameDisplayMode == GameDisplays.DISPLAY_MAIL) || (Instance.GameDisplayMode == GameDisplays.DISPLAY_CASTLE)) || ((Instance.GameDisplayMode == GameDisplays.DISPLAY_VILLAGE) || (Instance.GameDisplayMode == GameDisplays.DISPLAY_WORLD)))
                        {
                            flag2 = true;
                        }
                        if ((Instance.GameDisplayMode == GameDisplays.DISPLAY_VILLAGE) && InterfaceMgr.Instance.isTextInputScreenActive())
                        {
                            switch (num)
                            {
                                case 160:
                                case 0xa1:
                                    shiftPressed = true;
                                    break;

                                case 9:
                                    tabPressed = true;
                                    break;
                            }
                            return CallNextHookEx(_hookID, nCode, wParam, lParam);
                        }
                        if (activeForm == InterfaceMgr.Instance.ChatForm)
                        {
                            flag = true;
                        }
                        switch (num)
                        {
                            case 9:
                                if (!flag)
                                {
                                    tabPressed = true;
                                }
                                break;

                            case 13:
                                if (!flag)
                                {
                                    enterPressed = true;
                                }
                                break;

                            case 0x25:
                                if (!flag2)
                                {
                                    break;
                                }
                                if (!flag)
                                {
                                    scrollLeft = true;
                                }
                                if ((Instance.GameDisplayMode == GameDisplays.DISPLAY_MAIL) || flag)
                                {
                                    break;
                                }
                                return (IntPtr) 1;

                            case 0x26:
                                if (!flag2)
                                {
                                    break;
                                }
                                if (!flag)
                                {
                                    scrollUp = true;
                                }
                                if ((Instance.GameDisplayMode == GameDisplays.DISPLAY_MAIL) || flag)
                                {
                                    break;
                                }
                                return (IntPtr) 1;

                            case 0x27:
                                if (!flag2)
                                {
                                    break;
                                }
                                if (!flag)
                                {
                                    scrollRight = true;
                                }
                                if ((Instance.GameDisplayMode == GameDisplays.DISPLAY_MAIL) || flag)
                                {
                                    break;
                                }
                                return (IntPtr) 1;

                            case 40:
                                if (!flag2)
                                {
                                    break;
                                }
                                if (!flag)
                                {
                                    scrollDown = true;
                                }
                                if ((Instance.GameDisplayMode == GameDisplays.DISPLAY_MAIL) || flag)
                                {
                                    break;
                                }
                                return (IntPtr) 1;

                            case 0x7a:
                                f11Pressed = true;
                                break;

                            case 160:
                            case 0xa1:
                                shiftPressed = true;
                                break;

                            case 0xa2:
                            case 0xa3:
                                Instance.GFX.keyControlled = true;
                                break;
                        }
                    }
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public void InitBattle(int keepType, int fakeDefensiveMode)
        {
            CastleResearchData defenderResearchData = new CastleResearchData();
            CastleResearchData attackerResearchData = new CastleResearchData();
            if (fakeDefensiveMode >= 0)
            {
                switch (fakeDefensiveMode)
                {
                    case 0:
                        defenderResearchData.catapult = 0;
                        defenderResearchData.conscription = 0;
                        defenderResearchData.longBow = 0;
                        defenderResearchData.pike = 0;
                        defenderResearchData.sword = 0;
                        keepType = 1;
                        break;

                    case 1:
                        defenderResearchData.defences = 4;
                        keepType = 3;
                        defenderResearchData.catapult = 2;
                        defenderResearchData.conscription = 2;
                        defenderResearchData.longBow = 2;
                        defenderResearchData.pike = 2;
                        defenderResearchData.sword = 2;
                        break;

                    case 2:
                        defenderResearchData.defences = 8;
                        defenderResearchData.sallyForth = 2;
                        keepType = 5;
                        defenderResearchData.catapult = 4;
                        defenderResearchData.conscription = 4;
                        defenderResearchData.longBow = 4;
                        defenderResearchData.pike = 4;
                        defenderResearchData.sword = 4;
                        break;

                    case 3:
                        defenderResearchData.defences = 10;
                        defenderResearchData.sallyForth = 4;
                        keepType = 10;
                        defenderResearchData.catapult = 6;
                        defenderResearchData.conscription = 6;
                        defenderResearchData.longBow = 6;
                        defenderResearchData.pike = 6;
                        defenderResearchData.sword = 6;
                        defenderResearchData.tunnel = 6;
                        break;
                }
            }
            attackerResearchData.defences = this.World.UserResearchData.Research_Defences;
            attackerResearchData.catapult = this.World.UserResearchData.Research_Catapult;
            attackerResearchData.sword = this.World.UserResearchData.Research_Sword;
            attackerResearchData.pike = this.World.UserResearchData.Research_Pike;
            attackerResearchData.longBow = this.World.UserResearchData.Research_LongBow;
            attackerResearchData.conscription = this.World.UserResearchData.Research_Conscription;
            attackerResearchData.sallyForth = this.World.UserResearchData.Research_SallyForth;
            attackerResearchData.vaults = this.World.UserResearchData.Research_Vaults;
            InterfaceMgr.Instance.clearControls();
            this.castle_Battle = new CastleMap(-1, this.gfx, 3);
            this.castle_Battle.castleShown(false);
            this.castle_Battle.reInitGFX();
            this.castle_Battle.setCampMode(0);
            if (keepType < 0)
            {
                keepType = 1;
            }
            this.castle_Battle.launchBattle(this.castle_AttackerSetup.generateCastleMapSnapshot(), null, this.castle_AttackerSetup.generateCastleTroopsSnapshot(), null, keepType, defenderResearchData, attackerResearchData, 0, -1, -1, -1, 0, false, false);
            this.GameDisplayMode = GameDisplays.DISPLAY_CASTLE;
            this.gameDisplayModeSubMode = GameDisplaySubModes.SUBMODE_BATTLE;
            this.gfx.BGColor = ARGBColors.Black;
            InterfaceMgr.Instance.initCastleBattleTab(true, -1, false);
        }

        public void InitBattle(byte[] compressedCastleMap, byte[] compressedCastleDamageMap, byte[] compressedDefenderMap, byte[] compressedAttackerMap, int keepType, CastleResearchData defenderResearchData, CastleResearchData attackerResearchData, int campMode, int pillageInfo, int ransackCount, int raidCount, int attackType, int villageID, GetReport_ReturnType reportReturnData, int landType)
        {
            InterfaceMgr.Instance.clearControls();
            this.castle_Battle = new CastleMap(villageID, this.gfx, 3);
            this.castle_Battle.castleShown(false);
            this.castle_Battle.reInitGFX();
            this.castle_Battle.setCampMode(campMode);
            bool oldReport = false;
            if ((reportReturnData != null) && (reportReturnData.reportTime < CastlesCommon.PRE_FOREST_CHANGE_DATE))
            {
                oldReport = true;
            }
            this.castle_Battle.setReportData(reportReturnData);
            this.castle_Battle.launchBattle(compressedCastleMap, compressedCastleDamageMap, compressedDefenderMap, compressedAttackerMap, keepType, defenderResearchData, attackerResearchData, campMode, pillageInfo, ransackCount, raidCount, landType, false, oldReport);
            this.castle_Battle.returnToReports();
            this.GameDisplayMode = GameDisplays.DISPLAY_CASTLE;
            this.gameDisplayModeSubMode = GameDisplaySubModes.SUBMODE_BATTLE;
            this.gfx.BGColor = ARGBColors.Black;
            bool aIAttack = true;
            if (reportReturnData != null)
            {
                aIAttack = this.World.isSpecial(reportReturnData.attackingVillage);
            }
            InterfaceMgr.Instance.initCastleBattleTab(true, attackType, aIAttack);
        }

        public void InitCastleAttackSetup()
        {
            InterfaceMgr.Instance.clearControls();
            if (this.castle_AttackerSetup == null)
            {
                this.castle_AttackerSetup = new CastleMap(-1, this.gfx, 1);
            }
            this.castle_AttackerSetup.castleShown(false);
            this.castle_AttackerSetup.reInitGFX();
            if (this.castle == null)
            {
                this.castle_AttackerSetup.importDefenderSnapshot(null, null, 0, false, 0);
            }
            else
            {
                this.castle_AttackerSetup.importDefenderSnapshot(this.castle.generateCastleMapSnapshot(), this.castle.generateCastleTroopsSnapshot(), 0, false, 0);
            }
            this.castle_AttackerSetup.initFakeSetup();
            this.GameDisplayMode = GameDisplays.DISPLAY_CASTLE;
            this.gameDisplayModeSubMode = GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP;
            this.gfx.BGColor = ARGBColors.Black;
            InterfaceMgr.Instance.initCastleAttackerSetupTab();
        }

        public void InitCastleAttackSetup(byte[] castleMap, byte[] defenderMap, int keepLevel, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int attackingVillage, int targetVillage, int attackType, int pillagePercent, int captainsCommand, int parentOfAttackingVillage, int numPeasantsInCastle, int numArchersInCastle, int numPikemenInCastle, int numSwordsmenInCastle, int targetUserID, string targetUserName, BattleHonourData honourData, int numCaptainsInCastle, int numCaptains, int landType, double capitalAttackRate)
        {
            try
            {
                InterfaceMgr.Instance.clearControls();
                if (this.castle_AttackerSetup == null)
                {
                    this.castle_AttackerSetup = new CastleMap(-1, this.gfx, 1);
                }
                this.castle_AttackerSetup.castleShown(false);
                this.castle_AttackerSetup.reInitGFX();
                int mode = 0;
                switch (this.World.getSpecial(targetVillage))
                {
                    case 3:
                        mode = 1;
                        break;

                    case 5:
                        mode = 2;
                        break;
                }
                this.castle_AttackerSetup.setCampMode(mode);
                this.castle_AttackerSetup.importDefenderSnapshot(castleMap, defenderMap, keepLevel, true, landType);
                this.castle_AttackerSetup.initRealSetup(attackingVillage, targetVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, attackType, pillagePercent, captainsCommand, parentOfAttackingVillage, numPeasantsInCastle, numArchersInCastle, numPikemenInCastle, numSwordsmenInCastle, targetUserID, targetUserName, honourData, numCaptainsInCastle, numCaptains, capitalAttackRate);
                this.GameDisplayMode = GameDisplays.DISPLAY_CASTLE;
                this.gameDisplayModeSubMode = GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP;
                this.gfx.BGColor = ARGBColors.Black;
                InterfaceMgr.Instance.initCastleAttackerSetupTab();
            }
            catch (Exception exception)
            {
                UniversalDebugLog.Log(string.Concat(new object[] { 
                    exception.ToString(), " values = ", castleMap, defenderMap, keepLevel, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, attackingVillage, targetVillage, attackType, pillagePercent, captainsCommand, parentOfAttackingVillage, 
                    numPeasantsInCastle, numArchersInCastle
                 }));
            }
        }

        public void InitCastleView(byte[] compressedCastleMap, byte[] compressedDefenderMap, int keepType, int campMode, int defencesResearch, int villageID, int landType)
        {
            InterfaceMgr.Instance.clearControls();
            this.castle_Battle = new CastleMap(-1, this.gfx, 3);
            this.castle_Battle.castleShown(false);
            this.castle_Battle.reInitGFX();
            this.castle_Battle.setCampMode(campMode);
            this.castle_Battle.clearTempAttackers();
            CastleResearchData defenderResearchData = new CastleResearchData {
                defences = defencesResearch
            };
            this.castle_Battle.launchBattle(compressedCastleMap, null, compressedDefenderMap, null, keepType, defenderResearchData, new CastleResearchData(), campMode, -1, -1, -1, landType, true, false);
            this.castle_Battle.returnToReports();
            this.castle_Battle.setRealBattleMode(false);
            this.GameDisplayMode = GameDisplays.DISPLAY_CASTLE;
            this.gameDisplayModeSubMode = GameDisplaySubModes.SUBMODE_BATTLE;
            this.gfx.BGColor = ARGBColors.Black;
            InterfaceMgr.Instance.initCastleBattleTab(false, villageID, false);
        }

        public void initCensorText(string[] words)
        {
            this.badWords = words;
        }

        public bool Initialise(GraphicsMgr mgr, int maxRes, int curRes)
        {
            this.inputState = new MouseInputState();
            if (Program.mySettings.LanguageIdent == "de")
            {
                nfi = new CultureInfo("de-DE", false).NumberFormat;
                nfi.NumberDecimalDigits = 0;
                nfi_decimal = new CultureInfo("de-DE", false).NumberFormat;
                nfi_decimal1 = new CultureInfo("de-DE", false).NumberFormat;
                nfi_decimal1.NumberDecimalDigits = 1;
                nfi_decimal2 = new CultureInfo("de-DE", false).NumberFormat;
                nfi_decimal2.NumberDecimalDigits = 2;
            }
            else if (Program.mySettings.LanguageIdent == "fr")
            {
                nfi = new CultureInfo("fr-FR", false).NumberFormat;
                nfi.NumberDecimalDigits = 0;
                nfi_decimal = new CultureInfo("fr-FR", false).NumberFormat;
                nfi_decimal1 = new CultureInfo("fr-FR", false).NumberFormat;
                nfi_decimal1.NumberDecimalDigits = 1;
                nfi_decimal2 = new CultureInfo("fr-FR", false).NumberFormat;
                nfi_decimal2.NumberDecimalDigits = 2;
            }
            else if (Program.mySettings.LanguageIdent == "ru")
            {
                nfi = new CultureInfo("ru-RU", false).NumberFormat;
                nfi.NumberDecimalDigits = 0;
                nfi_decimal = new CultureInfo("ru-RU", false).NumberFormat;
                nfi_decimal1 = new CultureInfo("ru-RU", false).NumberFormat;
                nfi_decimal1.NumberDecimalDigits = 1;
                nfi_decimal2 = new CultureInfo("ru-RU", false).NumberFormat;
                nfi_decimal2.NumberDecimalDigits = 2;
            }
            else if (Program.mySettings.LanguageIdent == "es")
            {
                nfi = new CultureInfo("es-ES", false).NumberFormat;
                nfi.NumberDecimalDigits = 0;
                nfi_decimal = new CultureInfo("es-ES", false).NumberFormat;
                nfi_decimal1 = new CultureInfo("es-ES", false).NumberFormat;
                nfi_decimal1.NumberDecimalDigits = 1;
                nfi_decimal2 = new CultureInfo("es-ES", false).NumberFormat;
                nfi_decimal2.NumberDecimalDigits = 2;
            }
            else if (Program.mySettings.LanguageIdent == "pl")
            {
                nfi = new CultureInfo("pl-PL", false).NumberFormat;
                nfi.NumberDecimalDigits = 0;
                nfi_decimal = new CultureInfo("pl-PL", false).NumberFormat;
                nfi_decimal1 = new CultureInfo("pl-PL", false).NumberFormat;
                nfi_decimal1.NumberDecimalDigits = 1;
                nfi_decimal2 = new CultureInfo("pl-PL", false).NumberFormat;
                nfi_decimal2.NumberDecimalDigits = 2;
            }
            else if (Program.mySettings.LanguageIdent == "it")
            {
                nfi = new CultureInfo("it-IT", false).NumberFormat;
                nfi.NumberDecimalDigits = 0;
                nfi_decimal = new CultureInfo("it-IT", false).NumberFormat;
                nfi_decimal1 = new CultureInfo("it-IT", false).NumberFormat;
                nfi_decimal1.NumberDecimalDigits = 1;
                nfi_decimal2 = new CultureInfo("it-IT", false).NumberFormat;
                nfi_decimal2.NumberDecimalDigits = 2;
            }
            else if (Program.mySettings.LanguageIdent == "tr")
            {
                nfi = new CultureInfo("tr-TR", false).NumberFormat;
                nfi.NumberDecimalDigits = 0;
                nfi_decimal = new CultureInfo("tr-TR", false).NumberFormat;
                nfi_decimal1 = new CultureInfo("tr-TR", false).NumberFormat;
                nfi_decimal1.NumberDecimalDigits = 1;
                nfi_decimal2 = new CultureInfo("tr-TR", false).NumberFormat;
                nfi_decimal2.NumberDecimalDigits = 2;
            }
            else if (Program.mySettings.LanguageIdent == "pt")
            {
                nfi = new CultureInfo("pt-BR", false).NumberFormat;
                nfi.NumberDecimalDigits = 0;
                nfi_decimal = new CultureInfo("pt-BR", false).NumberFormat;
                nfi_decimal1 = new CultureInfo("pt-BR", false).NumberFormat;
                nfi_decimal1.NumberDecimalDigits = 1;
                nfi_decimal2 = new CultureInfo("pt-BR", false).NumberFormat;
                nfi_decimal2.NumberDecimalDigits = 2;
            }
            else
            {
                nfi = new CultureInfo(CultureInfo.CurrentCulture.Name, false).NumberFormat;
                nfi.NumberDecimalDigits = 0;
                nfi_decimal = new CultureInfo(CultureInfo.CurrentCulture.Name, false).NumberFormat;
                nfi_decimal1 = new CultureInfo(CultureInfo.CurrentCulture.Name, false).NumberFormat;
                nfi_decimal1.NumberDecimalDigits = 1;
                nfi_decimal2 = new CultureInfo(CultureInfo.CurrentCulture.Name, false).NumberFormat;
                nfi_decimal2.NumberDecimalDigits = 2;
            }
            NewQuests.loadCSV();
            this.maxResolution = maxRes;
            this.currentResolution = curRes;
            this.gfx = mgr;
            this.m_doReLogin = false;
            this.villageToAbandon = -1;
            if (this.firstCall)
            {
                this.m_tickTimer = new System.Threading.Timer(new TimerCallback(this.TimerCallbackFunction), null, 0x21, 0x21);
            }
            if (this.gfx.InitControl(InterfaceMgr.Instance.getDXBasePanel(), Program.mySettings.AAMode) != null)
            {
                displayDirectXError();
                return false;
            }
            if (this.gfx.calcedAAMode > 0)
            {
                Program.mySettings.AAMode = this.gfx.calcedAAMode;
            }
            this.dxFont1.Family = "Tahoma";
            this.dxFont1.Height = 0x12;
            this.dxFont2.Family = "Arial";
            this.dxFont2.Weight = FontDesc.Weighting.Normal;
            this.dxFont2.Height = 0x12;
            this.gfx.Initialize();
            this.gfx.initRenderCallback(new GraphicsMgr.RenderCallback(this.render));
            this.gfx.initFont(this.dxFont1, this.dxFont2);
            this.m_WorkerThread = new Thread(new ThreadStart(this.loadThread));
            this.m_WorkerThread.Name = "Loader";
            GFXLibrary.Instance.loadResources();
            InterfaceMgr.Instance.mapPanelCreates();
            this.m_WorkerThread.Start();
            if (this.firstCall)
            {
                Thread.Sleep(100);
            }
            this.worldMapTypesData = new WorldMapTypes();
            if (this.firstCall)
            {
                this.audio = new Audio();
                this.audio.initAudio();
                VillageMap.loadVillageSounds();
                Sound.createPlayLists();
            }
            RemoteServices.Instance.set_CommonData_UserCallBack(new RemoteServices.CommonData_UserCallBack(this.remoteConnectionCommonHandler));
            InterfaceMgr.Instance.initInterfaces();
            OptionsPopup.registerCallback(new OptionsPopup.ResolutionChangeCallback(this.resolutionButtonChange));
            this.world.registerWorldZoomCallback(new WorldMap.WorldZoomCallback(this.worldZoomChange));
            this.world.capZoom(0.0);
            InterfaceMgr.Instance.getMainTabBar().registerTabChangeCallback(new MainTabBar2.TabChangeCallback(this.mainTabChange));
            InterfaceMgr.Instance.getVillageTabBar().registerTabChangeCallback(new VillageTabBar2.TabChangeCallback(this.villageTabChange));
            InterfaceMgr.Instance.getFactionTabBar().registerTabChangeCallback(new FactionTabBar2.TabChangeCallback(this.factionTabChange));
            RemoteServices.Instance.set_GetVillageBuildingsList_UserCallBack(new RemoteServices.GetVillageBuildingsList_UserCallBack(this.getVillageBuildingListCallBack));
            this.gfx.BGColor = WorldMap.SEACOLOR;
            this.lastTabID = -1;
            InterfaceMgr.Instance.ignoreStopDraw = true;
            DXPanel.skipPaint = true;
            this.mainTabChange(0);
            DXPanel.skipPaint = false;
            InterfaceMgr.Instance.ignoreStopDraw = false;
            this.firstCall = false;
            this.lastFullTickRegisterTime = this.lastFullTickTime = DXTimer.GetCurrentMilliseconds();
            return true;
        }

        public void initWorldData(WorldData newWorldData)
        {
            this.worldData = newWorldData;
        }

        public void installKeyboardHook()
        {
            this.uninstallKeyboardHook();
            _hookID = SetHook(_proc, 13);
            keyboardHookedInstalled = true;
        }

        public bool isSelectNewVillageVisible()
        {
            if ((this.noVillagePopup == null) && (this.noAutoVillagePopup == null))
            {
                return false;
            }
            if (this.noVillagePopup != null)
            {
                if (!this.noVillagePopup.Created)
                {
                    return false;
                }
                if (!this.noVillagePopup.Visible)
                {
                    return false;
                }
            }
            if (this.noAutoVillagePopup != null)
            {
                if (!this.noAutoVillagePopup.Created)
                {
                    return false;
                }
                if (!this.noAutoVillagePopup.Visible)
                {
                    return false;
                }
            }
            return true;
        }

        public bool isStillLoading()
        {
            return !GFXLibrary.Instance.worldMapLoaded;
        }

        public void killLoadThread()
        {
            this.m_cancelLoading = true;
            while (!this.gfxLoaded)
            {
                Thread.Sleep(10);
                Program.DoEvents();
            }
        }

        public void lateStart()
        {
            InterfaceMgr.Instance.setUserName(RemoteServices.Instance.UserName);
            this.world.setCurrentZoom((float) (27.0 - this.World.WorldZoom));
            this.world.setScreenSize(InterfaceMgr.Instance.getDXBasePanel().Width, InterfaceMgr.Instance.getDXBasePanel().Height);
            int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            InterfaceMgr.Instance.getMainTabBar().changeTab(9);
            InterfaceMgr.Instance.getMainTabBar().changeTab(0);
            this.world.startGameZoom(villageID);
            InterfaceMgr.Instance.getTopLeftMenu().init();
            InterfaceMgr.Instance.getTopRightMenu().init();
            if (this.LocalWorldData.Alternate_Ruleset == 1)
            {
                InterfaceMgr.Instance.showDominationWindow();
            }
        }

        public void loadThread()
        {
            InterfaceMgr.Instance.showDXCardBar(0);
            CastleMap.loadCastleGFX(this.gfx);
            if (!this.gfxLoaded)
            {
                UVSpriteLoader.loadUVX(@"assets\uvx.resources");
                GFXLibrary.Instance.loadGFX(this.gfx);
                UVSpriteLoader.closeUVX();
                this.gfxLoaded = true;
            }
            else
            {
                this.gfx.reloadGFX();
            }
        }

        public bool loginCancelled()
        {
            return this.m_doReLogin;
        }

        private void loginHistoryRun()
        {
            if ((this.m_loginHistoryPop != null) && this.m_loginHistoryPop.Created)
            {
                this.m_loginHistoryPop.update();
            }
        }

        public void mainTabChange(int tabID)
        {
            UniversalDebugLog.Log("disabling blur on tabchange start " + tabID);
            InterfaceMgr.bgdBlurEnabled = false;
            if ((this.lastTabID == tabID) && (tabID != 9))
            {
                return;
            }
            if ((tabID == 1) && (InterfaceMgr.Instance.getSelectedMenuVillage() < 0))
            {
                InterfaceMgr.Instance.StopDrawing();
                InterfaceMgr.Instance.getMainTabBar().changeTab(this.lastTabID);
                InterfaceMgr.Instance.StartDrawing();
                return;
            }
            InterfaceMgr.Instance.StopDrawing();
            this.previousTabID = this.lastTabID;
            this.lastTabID = tabID;
            InterfaceMgr.Instance.clearControls();
            this.gameDisplayModeSubMode = GameDisplaySubModes.SUBMODE_DEFAULT;
            StatTrackingClient.Instance().ActivateTrigger(1, tabID);
            if (tabID != 1)
            {
                this.lastLoadedVillage = -1;
            }
            switch (tabID)
            {
                case 0:
                    this.GameDisplayMode = GameDisplays.DISPLAY_WORLD;
                    UniversalDebugLog.Log("disabling blur on select world tab");
                    InterfaceMgr.bgdBlurEnabled = false;
                    this.gfx.BGColor = WorldMap.SEACOLOR;
                    InterfaceMgr.Instance.initWorldTab();
                    InterfaceMgr.Instance.selectCurrentUserVillage();
                    goto Label_0689;

                case 1:
                    this.lastVillageTabID = -1;
                    InterfaceMgr.Instance.showVillageTabBar();
                    InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(0);
                    goto Label_0689;

                case 2:
                {
                    int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
                    if (!InterfaceMgr.Instance.isSelectedVillageACapital())
                    {
                        int parishID = this.World.getParishFromVillageID(villageID);
                        int num4 = this.World.getParishCapital(parishID);
                        if (num4 >= 0)
                        {
                            this.lastVillageTabID = -1;
                            InterfaceMgr.Instance.showVillageTabBar();
                            InterfaceMgr.Instance.selectUserVillage(num4, false);
                            InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(5);
                            this.MovedFromVillageID = villageID;
                        }
                        else
                        {
                            InterfaceMgr.Instance.getMainTabBar().changeTab(0);
                        }
                    }
                    else
                    {
                        this.lastVillageTabID = -1;
                        InterfaceMgr.Instance.showVillageTabBar();
                        InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(5);
                        this.MovedFromVillageID = villageID;
                    }
                    goto Label_0689;
                }
                case 3:
                    this.GameDisplayMode = GameDisplays.DISPLAY_RESEARCH;
                    UniversalDebugLog.Log("adding blur before researchtab init");
                    InterfaceMgr.bgdBlurEnabled = true;
                    InterfaceMgr.Instance.addMainWindow(this.m_firstDrawResearch, true);
                    this.m_firstDrawResearch = false;
                    InterfaceMgr.Instance.initResearchTab();
                    goto Label_0689;

                case 4:
                    this.GameDisplayMode = GameDisplays.DISPLAY_RANKINGS;
                    InterfaceMgr.Instance.addMainWindow(this.m_firstDrawRank, true);
                    this.m_firstDrawRank = false;
                    InterfaceMgr.Instance.initRankingsTab();
                    goto Label_0689;

                case 5:
                    this.GameDisplayMode = GameDisplays.DISPLAY_QUESTS;
                    InterfaceMgr.Instance.addMainWindow(this.m_firstDrawQuest, true);
                    this.m_firstDrawQuest = false;
                    InterfaceMgr.Instance.initQuestsTab();
                    goto Label_0689;

                case 6:
                    this.GameDisplayMode = GameDisplays.DISPLAY_ARMIES;
                    InterfaceMgr.Instance.addMainWindow(this.m_firstDrawArmy, true);
                    this.m_firstDrawArmy = false;
                    InterfaceMgr.Instance.initAllArmiesTab();
                    goto Label_0689;

                case 7:
                    this.GameDisplayMode = GameDisplays.DISPLAY_REPORTS;
                    InterfaceMgr.Instance.addMainWindow(this.m_firstDrawReports, false);
                    this.m_firstDrawReports = false;
                    InterfaceMgr.Instance.initReportTab();
                    goto Label_0689;

                case 8:
                    this.lastVillageTabID = -1;
                    this.GameDisplayMode = GameDisplays.DISPLAY_FACTIONS;
                    InterfaceMgr.Instance.showFactionTabBar();
                    if (this.nextFactionPage >= 0)
                    {
                        if ((this.nextFactionPage == 0x34) || (this.nextFactionPage == 0x33))
                        {
                            InterfaceMgr.Instance.getFactionTabBar().forceChangeTab(2);
                        }
                        else
                        {
                            InterfaceMgr.Instance.getFactionTabBar().forceChangeTab(1);
                        }
                    }
                    else
                    {
                        InterfaceMgr.Instance.getFactionTabBar().forceChangeTab(0);
                    }
                    goto Label_0689;

                case 9:
                {
                    int dummyMode = MainTabBar2.DummyMode;
                    MainTabBar2.LastDummyMode = dummyMode;
                    MainTabBar2.DummyMode = 0;
                    int num7 = dummyMode;
                    if (num7 > 50)
                    {
                        switch (num7)
                        {
                            case 60:
                                InterfaceMgr.bgdBlurEnabled = true;
                                InterfaceMgr.Instance.setVillageTabSubMode(60);
                                goto Label_0689;

                            case 100:
                                this.gfx.BGColor = ARGBColors.Black;
                                InterfaceMgr.bgdBlurEnabled = true;
                                InterfaceMgr.Instance.showAllVillagesScreen();
                                goto Label_0689;
                        }
                        break;
                    }
                    switch (num7)
                    {
                        case -14:
                        case 14:
                            this.GameDisplayMode = GameDisplays.DISPLAY_WORLD;
                            this.gfx.BGColor = WorldMap.SEACOLOR;
                            InterfaceMgr.Instance.initWorldTab_monkSelect();
                            goto Label_0689;

                        case -13:
                        case 13:
                            this.GameDisplayMode = GameDisplays.DISPLAY_WORLD;
                            this.gfx.BGColor = WorldMap.SEACOLOR;
                            InterfaceMgr.Instance.initWorldTab_vassalSelect();
                            goto Label_0689;

                        case -11:
                        case 11:
                            this.GameDisplayMode = GameDisplays.DISPLAY_WORLD;
                            this.gfx.BGColor = WorldMap.SEACOLOR;
                            InterfaceMgr.Instance.initWorldTab_courtierTargetSelect();
                            goto Label_0689;

                        case -7:
                        case 7:
                            this.GameDisplayMode = GameDisplays.DISPLAY_WORLD;
                            this.gfx.BGColor = WorldMap.SEACOLOR;
                            InterfaceMgr.Instance.initWorldTab_scoutTargetSelect();
                            goto Label_0689;

                        case -5:
                        case 5:
                            this.GameDisplayMode = GameDisplays.DISPLAY_WORLD;
                            this.gfx.BGColor = WorldMap.SEACOLOR;
                            InterfaceMgr.Instance.initWorldTab_attackTargetSelect();
                            goto Label_0689;

                        case -4:
                        case 4:
                            this.GameDisplayMode = GameDisplays.DISPLAY_WORLD;
                            this.gfx.BGColor = WorldMap.SEACOLOR;
                            InterfaceMgr.Instance.initWorldTab_stockExchangeSelect();
                            goto Label_0689;

                        case -3:
                        case 3:
                            this.GameDisplayMode = GameDisplays.DISPLAY_WORLD;
                            this.gfx.BGColor = WorldMap.SEACOLOR;
                            InterfaceMgr.Instance.initWorldTab_tradingVillageSelect();
                            goto Label_0689;

                        case 1:
                        case 2:
                        case 6:
                            goto Label_0689;

                        case 10:
                            this.GameDisplayMode = GameDisplays.DISPLAY_AVATAR_EDITOR;
                            InterfaceMgr.bgdBlurEnabled = true;
                            InterfaceMgr.Instance.getTopRightMenu().showVillageTab(false);
                            InterfaceMgr.Instance.addMainWindow(this.m_firstDrawDummy, true);
                            this.m_firstDrawDummy = false;
                            InterfaceMgr.Instance.setVillageTabSubMode(10);
                            goto Label_0689;

                        case 0x15:
                            InterfaceMgr.bgdBlurEnabled = true;
                            if (InterfaceMgr.Instance.isMailDocked())
                            {
                                this.GameDisplayMode = GameDisplays.DISPLAY_MAIL;
                                InterfaceMgr.Instance.addMainWindow(this.m_firstDrawMail, true);
                                this.m_firstDrawMail = false;
                            }
                            InterfaceMgr.Instance.initMailSubTab(0);
                            goto Label_0689;

                        case 0x16:
                            InterfaceMgr.bgdBlurEnabled = true;
                            this.GameDisplayMode = GameDisplays.DISPLAY_LEADERBOARD;
                            InterfaceMgr.Instance.addMainWindow(this.m_firstDrawLeaderboard, true);
                            this.m_firstDrawLeaderboard = false;
                            InterfaceMgr.Instance.initReportsLeaderboard();
                            goto Label_0689;

                        case 50:
                            this.GameDisplayMode = GameDisplays.DISPLAY_VILLAGE;
                            this.gfx.BGColor = ARGBColors.Black;
                            InterfaceMgr.Instance.initVillageTabView();
                            goto Label_0689;
                    }
                    break;
                }
                default:
                    goto Label_0689;
            }
            InterfaceMgr.bgdBlurEnabled = true;
            InterfaceMgr.Instance.addMainWindow(this.m_firstDrawDummy, true);
            this.m_firstDrawDummy = false;
        Label_0689:
            InterfaceMgr.Instance.StartDrawing();
        }

        public void manageInput()
        {
            this.inputState.getInput();
            InterfaceMgr.Instance.runTooltips();
            if (this.WindowActive)
            {
                if (f11Pressed)
                {
                    f11Pressed = false;
                    if ((InterfaceMgr.Instance.ParentForm != null) && InterfaceMgr.Instance.ParentForm.Visible)
                    {
                        if (InterfaceMgr.Instance.ParentForm.FormBorderStyle == FormBorderStyle.Sizable)
                        {
                            InterfaceMgr.Instance.ParentForm.FormBorderStyle = FormBorderStyle.None;
                        }
                        else if (InterfaceMgr.Instance.ParentForm.WindowState == FormWindowState.Maximized)
                        {
                            InterfaceMgr.Instance.ParentForm.Visible = false;
                            InterfaceMgr.Instance.ParentForm.WindowState = FormWindowState.Normal;
                            Program.DoEvents();
                            Thread.Sleep(100);
                            InterfaceMgr.Instance.ParentForm.FormBorderStyle = FormBorderStyle.Sizable;
                            Program.DoEvents();
                            InterfaceMgr.Instance.ParentForm.WindowState = FormWindowState.Maximized;
                            InterfaceMgr.Instance.ParentForm.Visible = true;
                            Program.DoEvents();
                            InterfaceMgr.Instance.ParentForm.Invalidate();
                        }
                        else
                        {
                            InterfaceMgr.Instance.ParentForm.FormBorderStyle = FormBorderStyle.Sizable;
                        }
                    }
                }
                else
                {
                    if (InterfaceMgr.Instance.getDXBasePanel().Visible)
                    {
                        if (this.GameDisplayMode == GameDisplays.DISPLAY_VILLAGE)
                        {
                            new VillageInputHandler(this.village).handleInput(this.inputState);
                        }
                        else if (this.GameDisplayMode == GameDisplays.DISPLAY_WORLD)
                        {
                            new WorldMapInputHandler(this.world).handleInput(this.inputState);
                        }
                        else if (this.GameDisplayMode == GameDisplays.DISPLAY_CASTLE)
                        {
                            CastleMap castle = this.castle;
                            if (this.gameDisplayModeSubMode == GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP)
                            {
                                castle = this.castle_AttackerSetup;
                            }
                            else if (this.gameDisplayModeSubMode == GameDisplaySubModes.SUBMODE_BATTLE)
                            {
                                castle = this.castle_Battle;
                            }
                            new CastleInputHandler(castle, this.gameDisplayModeSubMode).handleInput(this.inputState);
                        }
                    }
                    this.gfx.clearInput();
                }
            }
        }

        private void monitorDownTime()
        {
            if (this.serverDowntime != DateTime.MinValue)
            {
                TimeSpan span = (TimeSpan) (this.serverDowntime - VillageMap.getCurrentServerTime());
                if ((span.TotalMinutes < 1440.5) && !this.warning24H)
                {
                    this.warning24H = true;
                    this.showDowntimeWarning(span.TotalMinutes);
                    return;
                }
                if ((span.TotalMinutes < 720.5) && !this.warning12H)
                {
                    this.warning12H = true;
                    this.showDowntimeWarning(span.TotalMinutes);
                    return;
                }
                if ((span.TotalMinutes < 240.5) && !this.warning4H)
                {
                    this.warning4H = true;
                    this.showDowntimeWarning(span.TotalMinutes);
                    return;
                }
                if ((span.TotalMinutes < 60.5) && !this.warning60)
                {
                    this.warning60 = true;
                    this.showDowntimeWarning(span.TotalMinutes);
                    return;
                }
                if ((span.TotalMinutes < 30.5) && !this.warning30)
                {
                    this.warning30 = true;
                    this.showDowntimeWarning(span.TotalMinutes);
                    return;
                }
                if ((span.TotalMinutes < 15.5) && !this.warning15)
                {
                    this.warning15 = true;
                    this.showDowntimeWarning(span.TotalMinutes);
                    return;
                }
                if ((span.TotalMinutes < 5.5) && !this.warning5)
                {
                    this.warning5 = true;
                    this.showDowntimeWarning(span.TotalMinutes);
                    return;
                }
                if ((span.TotalMinutes < 0.0) && !this.serverOffline)
                {
                    this.serverOffline = true;
                    this.clearDowntimePopup();
                    this.sessionExpired(3);
                    return;
                }
            }
            if ((this.LocalWorldData.Alternate_Ruleset == 1) && (this.getDominationTimeLeft().TotalSeconds <= 0.0))
            {
                this.serverOffline = true;
                this.sessionExpired(0);
            }
        }

        public void OnPaintCallback()
        {
            bool flag = false;
            if (this.GameDisplayMode == GameDisplays.DISPLAY_WORLD)
            {
                flag = true;
            }
            if ((this.GameDisplayMode == GameDisplays.DISPLAY_VILLAGE) && InterfaceMgr.Instance.updateVillageReports())
            {
                flag = true;
            }
            if (this.GameDisplayMode == GameDisplays.DISPLAY_CASTLE)
            {
                flag = true;
            }
            if (flag && InterfaceMgr.Instance.ParentForm.Created)
            {
                bool renderContent = true;
                if (Program.steamInstall)
                {
                    renderContent = !Program.steamOverlayActive;
                }
                if (!this.gfx.render(renderContent))
                {
                    this.newResolution = this.currentResolution;
                    if (InterfaceMgr.Instance.ParentForm != null)
                    {
                        MyMessageBox.Show(SK.Text("GameEngine_Generic_Error", "An error has occurred and Stronghold Kingdoms will now close."), "DirectX");
                        InterfaceMgr.Instance.ParentForm.Close();
                    }
                }
            }
        }

        public void openAdvancedSelectVillage()
        {
            InterfaceMgr.Instance.openGreyOutWindow(false);
            InterfaceMgr.Instance.ParentForm.Enabled = false;
            this.noVillagePopup = new NewSelectVillageAreaWindow();
            this.noVillagePopup.init(this.tryingToJoinCounty);
            this.noVillagePopup.Show(InterfaceMgr.Instance.getGreyOutWindow());
        }

        public void openLostVillage(int age)
        {
            if (InterfaceMgr.Instance.ParentForm.WindowState == FormWindowState.Minimized)
            {
                InterfaceMgr.Instance.ParentForm.WindowState = FormWindowState.Normal;
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
            }
            InterfaceMgr.Instance.openGreyOutWindow(false);
            InterfaceMgr.Instance.ParentForm.Enabled = false;
            this.lostVillagePopup = new LostVillageWindow();
            this.lostVillagePopup.init(age, -1);
            this.lostVillagePopup.Show(InterfaceMgr.Instance.getGreyOutWindow());
        }

        public void openSimpleSelectVillage()
        {
            InterfaceMgr.Instance.openGreyOutWindow(false);
            InterfaceMgr.Instance.ParentForm.Enabled = false;
            this.noAutoVillagePopup = new NewAutoSelectVillageWindow();
            this.noAutoVillagePopup.init(this.tryingToJoinCounty);
            this.noAutoVillagePopup.Show(InterfaceMgr.Instance.getGreyOutWindow());
        }

        public void openSuperPackInfo(int mode)
        {
            this.lostVillagePopup = new LostVillageWindow();
            this.lostVillagePopup.init(0, mode);
            this.lostVillagePopup.Show(InterfaceMgr.Instance.getCardWindow());
        }

        public bool pendingError()
        {
            if (this.pendingErrorCode == -1)
            {
                return false;
            }
            this.sessionExpired(this.pendingErrorCode);
            return true;
        }

        public void playInterfaceSound(string soundTag)
        {
            this.playInterfaceSound(soundTag, true);
        }

        public void playInterfaceSound(string soundTag, bool overwritePlayingSound)
        {
            if (((((soundTag.Trim().Length != 0) && (this.AudioEngine != null)) && !this.stopInterfaceSounds) && Sound.SFXActive) && (overwritePlayingSound || !this.AudioEngine.isSoundPlaying(soundTag)))
            {
                this.AudioEngine.playInterfaceSound(soundTag);
            }
        }

        public void politicsTabChange(int tabID)
        {
        }

        public void preAttackSetup(int parentVillageID, int attackingVillageID, int targetVillageID)
        {
            RemoteServices.Instance.set_PreAttackSetup_UserCallBack(new RemoteServices.PreAttackSetup_UserCallBack(this.preAttackSetupCallback));
            RemoteServices.Instance.PreAttackSetup(parentVillageID, attackingVillageID, targetVillageID, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        public void preAttackSetupCallback(PreAttackSetup_ReturnType returnData)
        {
            if (returnData.sameFaction)
            {
                MyMessageBox.Show(SK.Text("GameEngine_Target_Faction", "Your target is in your Faction."), SK.Text("GENERIC_Cannot_Attack_Target", "Cannot Attack Target"));
                InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                InterfaceMgr.Instance.getMainTabBar().changeTab(0);
            }
            else if (returnData.sameHouse)
            {
                MyMessageBox.Show(SK.Text("GameEngine_Target_House", "Your target is in your House."), SK.Text("GENERIC_Cannot_Attack_Target", "Cannot Attack Target"));
                InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                InterfaceMgr.Instance.getMainTabBar().changeTab(0);
            }
            else if (returnData.protectedVillage)
            {
                MyMessageBox.Show(SK.Text("GameEngine_Protected_Interdiction", "This village is protected from attack by an Interdiction."), SK.Text("GENERIC_Village_Protected", "Village Protected"));
                InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                InterfaceMgr.Instance.getMainTabBar().changeTab(0);
            }
            else if (returnData.vacationVillage)
            {
                MyMessageBox.Show(SK.Text("GameEngine_Protected_Vacation", "This village is protected from attack by Vacation Mode."), SK.Text("GENERIC_Village_Protected", "Village Protected"));
                InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                InterfaceMgr.Instance.getMainTabBar().changeTab(0);
            }
            else if (returnData.vassalVacation)
            {
                MyMessageBox.Show(SK.Text("GameEngine_Vassal_Vacation", "Your vassal is in Vacation Mode and you cannot attack from here."), SK.Text("GENERIC_Cannot_Attack_Target", "Cannot Attack Target"));
                InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                InterfaceMgr.Instance.getMainTabBar().changeTab(0);
            }
            else if (returnData.peaceVillage)
            {
                if (!this.world.isCapital(returnData.targetVillage))
                {
                    MyMessageBox.Show(SK.Text("GameEngine_Protected_Peacetime", "This village is within Peace Time and cannot be attacked."), SK.Text("GENERIC_Village_Protected", "Village Protected"));
                }
                else
                {
                    MyMessageBox.Show(SK.Text("GameEngine_Protected_Peacetime_Capital", "This capital is within peace time and cannot be attacked."), SK.Text("GENERIC_Capital_Protected", "Capital Protected"));
                }
                InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                InterfaceMgr.Instance.getMainTabBar().changeTab(0);
            }
            else if (returnData.peaceAttacker)
            {
                if (returnData.parentAttackingVillage != returnData.attackingVillage)
                {
                    MyMessageBox.Show(SK.Text("GameEngine_Cannot_Attack_PeaceTime", "You are within Peace Time and cannot attack from this village."), SK.Text("GENERIC_Village_Protected", "Village Protected"));
                    InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                    InterfaceMgr.Instance.getMainTabBar().changeTab(0);
                }
                else
                {
                    MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
                    if (MyMessageBox.Show(SK.Text("GameEngine_Currently_Peacetime", "You are currently Peace Time protected") + "\n" + SK.Text("GameEngine_CancelProtection", "Do you wish to cancel this protection?"), SK.Text("GENERIC_Village_Protected", "Village Protected"), yesNo) == DialogResult.Yes)
                    {
                        this.sentParentVillageID = returnData.parentAttackingVillage;
                        this.sentAttackingVillageID = returnData.attackingVillage;
                        this.sentTargetVillageID = returnData.targetVillage;
                        RemoteServices.Instance.set_CancelInterdiction_UserCallBack(new RemoteServices.CancelInterdiction_UserCallBack(this.cancelInterdictionCallback));
                        RemoteServices.Instance.CancelInterdiction(returnData.attackingVillage);
                    }
                    else
                    {
                        InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                        InterfaceMgr.Instance.getMainTabBar().changeTab(0);
                    }
                }
            }
            else if (returnData.protectedAttacker)
            {
                if (returnData.parentAttackingVillage != returnData.attackingVillage)
                {
                    MyMessageBox.Show(SK.Text("GameEngine_Currently_Interdited_Vassal", "Your vassal is protected by Interdiction and you cannot attack from this village."), SK.Text("GameEngine_Currently_Interdited_protected", "Your Vassal is Protected"));
                    InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                    InterfaceMgr.Instance.getMainTabBar().changeTab(0);
                }
                else
                {
                    MessageBoxButtons buts = MessageBoxButtons.YesNo;
                    if (MyMessageBox.Show(SK.Text("GameEngine_Currently_Interdited", "You are currently Interdiction protected") + "\n" + SK.Text("GameEngine_CancelProtection", "Do you wish to cancel this protection?"), SK.Text("GENERIC_Protected", "You Are Protected"), buts) == DialogResult.Yes)
                    {
                        this.sentParentVillageID = returnData.parentAttackingVillage;
                        this.sentAttackingVillageID = returnData.attackingVillage;
                        this.sentTargetVillageID = returnData.targetVillage;
                        RemoteServices.Instance.set_CancelInterdiction_UserCallBack(new RemoteServices.CancelInterdiction_UserCallBack(this.cancelInterdictionCallback));
                        if (this.LocalWorldData.AIWorld)
                        {
                            RemoteServices.Instance.CancelInterdiction(-returnData.attackingVillage);
                        }
                        else
                        {
                            RemoteServices.Instance.CancelInterdiction(returnData.attackingVillage);
                        }
                    }
                    else
                    {
                        InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                        InterfaceMgr.Instance.getMainTabBar().changeTab(0);
                    }
                }
            }
            else if (returnData.Success)
            {
                int num = 0;
                if (returnData.battleHonourData != null)
                {
                    returnData.battleHonourData.attackType = 11;
                    if (!Instance.World.isCapital(returnData.parentAttackingVillage))
                    {
                        num = CastlesCommon.calcBattleHonourCost(returnData.battleHonourData, Instance.LocalWorldData.Alternate_Ruleset == 1);
                    }
                }
                if ((num > 0) && (this.World.getCurrentHonour() <= 0.0))
                {
                    MyMessageBox.Show(SK.Text("GameEngine_Require_Honour_To_Attack", "You require honour to attack this target."), SK.Text("GENERIC_Attack_Error", "Attack Error"));
                }
                else
                {
                    this.InitCastleAttackSetup(returnData.castleMapSnapshot, returnData.castleTroopsSnapshot, returnData.keepLevel, returnData.numPeasants, returnData.numArchers, returnData.numPikemen, returnData.numSwordsmen, returnData.numCatapults, returnData.attackingVillage, returnData.targetVillage, returnData.attackType, returnData.pillagePercent, returnData.captainsCommand, returnData.parentAttackingVillage, returnData.numPeasantsInCastle, returnData.numArchersInCastle, returnData.numPikemenInCastle, returnData.numSwordsmenInCastle, returnData.targetUserID, returnData.targetUserName, returnData.battleHonourData, returnData.numCaptainsInCastle, returnData.numCaptains, returnData.landType, returnData.capitalAttackRate);
                    InterfaceMgr.Instance.setCastleViewTimes(returnData.lastCastleTime, returnData.castleMapSnapshot != null, returnData.lastTroopTime, returnData.castleTroopsSnapshot != null);
                }
            }
            else
            {
                switch (returnData.m_errorCode)
                {
                    case ErrorCodes.ErrorCode.ATTACKING_NOT_ENOUGH_TROOPS:
                    case ErrorCodes.ErrorCode.ATTACKING_INVALID_TARGET:
                        MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("GENERIC_Attack_Error", "Attack Error"));
                        break;
                }
            }
        }

        public bool quitting()
        {
            return this.quitGame;
        }

        public bool reLogin()
        {
            bool doReLogin = this.m_doReLogin;
            this.m_doReLogin = false;
            return doReLogin;
        }

        public void remoteConnectionCommonHandler(Common_ReturnData returnData)
        {
            InterfaceMgr.Instance.getMainTabBar().newReports(returnData.NewReports);
            InterfaceMgr.Instance.getMainTabBar().newMail(returnData.NewMail);
            if (returnData.NewMail)
            {
                InterfaceMgr.Instance.mailPopupNewMail();
            }
            InterfaceMgr.Instance.getMainTabBar().newPoliticsPost(returnData.NewPoliticsForumPost);
            if (returnData.NewIngameMessage)
            {
                RemoteServices.Instance.set_GetIngameMessage_UserCallBack(new RemoteServices.GetIngameMessage_UserCallBack(this.getIngameMessageCallback));
                RemoteServices.Instance.GetIngameMessage();
            }
            if (returnData.NoVillages)
            {
                InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                InterfaceMgr.Instance.getMainTabBar().changeTab(0);
            }
        }

        public void render()
        {
            this.gfx.drawOverLayTexture = InterfaceMgr.Instance.allowDrawCircles();
            InterfaceMgr.Instance.updateDXCardBar();
            if (this.GameDisplayMode == GameDisplays.DISPLAY_WORLD)
            {
                this.world.drawVillageTree(this.gfx);
            }
            this.gfx.RenderList.render(this.gfx);
            if (this.GameDisplayMode == GameDisplays.DISPLAY_CASTLE)
            {
                if ((this.gameDisplayModeSubMode == GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP) && (this.castle_AttackerSetup != null))
                {
                    this.castle_AttackerSetup.drawCatapultLines();
                    this.castle_AttackerSetup.drawLasso();
                }
                if ((this.gameDisplayModeSubMode == GameDisplaySubModes.SUBMODE_DEFAULT) && (this.castle != null))
                {
                    this.castle.drawLasso();
                }
            }
            if ((this.GameDisplayMode == GameDisplays.DISPLAY_VILLAGE) && (this.village != null))
            {
                this.village.drawProductionArrow();
            }
            if (Program.ShowSeasonalFX && (SnowSystem.getInstance().snowTexture != null))
            {
                this.gfx.beginSprites();
                SnowSystem.getInstance().render(this.gfx);
                this.gfx.endSprites();
            }
        }

        public void ResetVillageIfChangedFromCapital()
        {
            if (this.MovedFromVillageID >= 0)
            {
                if (InterfaceMgr.Instance.getMainTabBar().getCurrentTab() == 0)
                {
                    this.GameDisplayMode = GameDisplays.DISPLAY_WORLD;
                }
                InterfaceMgr.Instance.selectUserVillage(this.MovedFromVillageID, false);
            }
        }

        public void resizeWindow()
        {
            if (this.gfx != null)
            {
                this.gfx.resizeWindow();
            }
            InterfaceMgr.Instance.mainWindowResize();
            switch (this.lastTabID)
            {
                case 0:
                    if (this.World != null)
                    {
                        this.World.moveMap(0.0, 0.0);
                    }
                    break;

                case 1:
                    if (this.GameDisplayMode != GameDisplays.DISPLAY_VILLAGE)
                    {
                        if (this.GameDisplayMode == GameDisplays.DISPLAY_CASTLE)
                        {
                            InterfaceMgr.Instance.castleMapResizeWindow();
                            if (this.gameDisplayModeSubMode == GameDisplaySubModes.SUBMODE_DEFAULT)
                            {
                                if (this.castle != null)
                                {
                                    this.castle.moveMap(0, 0);
                                    this.castle.createSurroundSprites();
                                    this.gfx.RenderList.clearLayers();
                                    this.castle.justDrawSprites();
                                    this.castle.recalcCastleLayout();
                                }
                            }
                            else if (this.gameDisplayModeSubMode == GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP)
                            {
                                if (this.castle_AttackerSetup != null)
                                {
                                    this.castle_AttackerSetup.moveMap(0, 0);
                                    this.castle_AttackerSetup.createSurroundSprites();
                                    this.gfx.RenderList.clearLayers();
                                    this.castle_AttackerSetup.justDrawSprites();
                                }
                            }
                            else if ((this.gameDisplayModeSubMode == GameDisplaySubModes.SUBMODE_BATTLE) && (this.castle_Battle != null))
                            {
                                this.castle_Battle.moveMap(0, 0);
                                this.castle_Battle.createSurroundSprites();
                                this.gfx.RenderList.clearLayers();
                                this.castle_Battle.justDrawSprites();
                            }
                        }
                        break;
                    }
                    InterfaceMgr.Instance.villageMapResizeWindow();
                    if (this.village != null)
                    {
                        this.village.moveMap(0, 0);
                        this.Village.createSurroundSprites();
                        this.gfx.RenderList.clearLayers();
                        this.village.justDrawSprites();
                    }
                    break;

                case 9:
                    if (this.GameDisplayMode == GameDisplays.DISPLAY_CASTLE)
                    {
                        InterfaceMgr.Instance.castleMapResizeWindow();
                        if (this.gameDisplayModeSubMode == GameDisplaySubModes.SUBMODE_DEFAULT)
                        {
                            if (this.castle != null)
                            {
                                this.castle.moveMap(0, 0);
                                this.castle.createSurroundSprites();
                                this.gfx.RenderList.clearLayers();
                                this.castle.justDrawSprites();
                                this.castle.recalcCastleLayout();
                            }
                        }
                        else if (this.gameDisplayModeSubMode == GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP)
                        {
                            if (this.castle_AttackerSetup != null)
                            {
                                this.castle_AttackerSetup.moveMap(0, 0);
                                this.castle_AttackerSetup.createSurroundSprites();
                                this.gfx.RenderList.clearLayers();
                                this.castle_AttackerSetup.justDrawSprites();
                            }
                        }
                        else if ((this.gameDisplayModeSubMode == GameDisplaySubModes.SUBMODE_BATTLE) && (this.castle_Battle != null))
                        {
                            this.castle_Battle.moveMap(0, 0);
                            this.castle_Battle.createSurroundSprites();
                            this.gfx.RenderList.clearLayers();
                            this.castle_Battle.justDrawSprites();
                        }
                    }
                    break;
            }
            InterfaceMgr.Instance.getDXBasePanel().Invalidate();
        }

        public void resolutionButtonChange(int newRes)
        {
            if (newRes <= this.maxResolution)
            {
                this.newResolution = newRes;
            }
        }

        public bool resolutionChange()
        {
            return (this.newResolution != -1);
        }

        public void resumeCommonRemote()
        {
            RemoteServices.Instance.set_CommonData_UserCallBack(new RemoteServices.CommonData_UserCallBack(this.remoteConnectionCommonHandler));
        }

        public void run()
        {
            long lastFrameTime = this.lastFrameTime;
            long currentMillisecondsLong = DXTimer.GetCurrentMillisecondsLong();
            if (lastFrameTime == 0L)
            {
                lastFrameTime = currentMillisecondsLong - 0x21L;
            }
            long num3 = currentMillisecondsLong - lastFrameTime;
            if (num3 >= 0x21L)
            {
                this.ticked = true;
                if (num3 >= 0x31L)
                {
                    this.lastFrameTime = currentMillisecondsLong;
                }
                else
                {
                    this.lastFrameTime += 0x21L;
                }
                InterfaceMgr.Instance.getDXBasePanel().AllowDraw();
                InterfaceMgr.Instance.getDXBasePanel().Invalidate();
                Program.DoEvents();
            }
            if (this.villageToAbandon >= 0)
            {
                DialogResult result;
                int villageToAbandon = this.villageToAbandon;
                this.villageHasBeenDownloaded = false;
                InterfaceMgr.Instance.changeTab(9);
                InterfaceMgr.Instance.changeTab(1);
                for (int i = 0; i < 210; i++)
                {
                    Thread.Sleep(0x21);
                    Program.DoEvents();
                    RemoteServices.Instance.processData();
                    if (this.villageHasBeenDownloaded)
                    {
                        this.villageToAbandon = -1;
                        for (int j = 0; j < 10; j++)
                        {
                            Thread.Sleep(0x21);
                            this.run();
                            RemoteServices.Instance.processData();
                        }
                        break;
                    }
                }
                int num7 = 0;
                VillageMap map = Instance.getVillage(villageToAbandon);
                if (map != null)
                {
                    num7 = map.countBuildings();
                }
                MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
                if (num7 <= 0)
                {
                    result = MyMessageBox.Show(SK.Text("Abandon_Message", "You are about to Abandon this village. You will lose ownership of this village and once abandoned it can not be reversed.") + Environment.NewLine + SK.Text("BuyVillagePopup_Are_You_REALLY_Sure", "Are You REALLY Sure you want to do this and that you have selected the correct village?") + Environment.NewLine + Environment.NewLine + SK.Text("BuyVillagePopup_To_Be_Abandon", "The Village to be abandoned is : ") + Environment.NewLine + Environment.NewLine + Instance.World.getVillageName(villageToAbandon) + Environment.NewLine + Environment.NewLine + ".", SK.Text("MENU_Abandon_Warning", "Warning! : Abandon") + " : " + Instance.World.getVillageName(villageToAbandon) + "?", yesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 0);
                }
                else
                {
                    result = MyMessageBox.Show(SK.Text("Abandon_Message", "You are about to Abandon this village. You will lose ownership of this village and once abandoned it can not be reversed.") + Environment.NewLine + SK.Text("BuyVillagePopup_Are_You_REALLY_Sure", "Are You REALLY Sure you want to do this and that you have selected the correct village?") + Environment.NewLine + Environment.NewLine + SK.Text("BuyVillagePopup_To_Be_Abandon", "The Village to be abandoned is : ") + Environment.NewLine + Environment.NewLine + Instance.World.getVillageName(villageToAbandon) + Environment.NewLine + Environment.NewLine + "." + SK.Text("BuyVillagePopup_Num_Buildings", "The number of buildings in this village : ") + num7.ToString() + Environment.NewLine + Environment.NewLine + ".", SK.Text("MENU_Abandon_Warning", "Warning! : Abandon") + " : " + Instance.World.getVillageName(villageToAbandon) + "?", yesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 0);
                }
                if (result == DialogResult.Yes)
                {
                    RemoteServices.Instance.set_VillageRename_UserCallBack(new RemoteServices.VillageRename_UserCallBack(MainMenuBar2.VillageRenameCallback));
                    RemoteServices.Instance.VillageAbandon(villageToAbandon);
                }
                this.villageToAbandon = -1;
            }
            if (!this.ticked)
            {
                goto Label_0F19;
            }
            if (this.lastSoundClear == DateTime.MinValue)
            {
                this.lastSoundClear = DateTime.Now;
            }
            else
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastSoundClear);
                if (span.TotalMinutes > 5.0)
                {
                    this.lastSoundClear = DateTime.Now;
                    this.AudioEngine.unloadUnplayingSounds();
                }
            }
            Program.steam_run();
            Program.arc_run();
            this.audio.update();
            Form activeForm = Form.ActiveForm;
            if ((activeForm != InterfaceMgr.Instance.ParentForm) && (activeForm != InterfaceMgr.Instance.ChatForm))
            {
                scrollLeft = false;
                scrollUp = false;
                scrollRight = false;
                scrollDown = false;
                Instance.GFX.keyControlled = false;
                shiftPressed = false;
                tabPressed = false;
            }
            this.gfx.RenderList.clearLayers();
            this.ticked = false;
            this.tickCount++;
            if (this.GameDisplayMode == GameDisplays.DISPLAY_VILLAGE)
            {
                if (this.village != null)
                {
                    this.village.Update(true);
                }
                if (this.castle != null)
                {
                    this.castle.Update(false);
                }
                InterfaceMgr.Instance.runVillageInterface();
            }
            else if (this.GameDisplayMode == GameDisplays.DISPLAY_CASTLE)
            {
                if (this.gameDisplayModeSubMode == GameDisplaySubModes.SUBMODE_DEFAULT)
                {
                    if (this.village != null)
                    {
                        this.village.Update(false);
                    }
                    if (this.castle != null)
                    {
                        this.castle.Update(true);
                    }
                    InterfaceMgr.Instance.runCastleInterface();
                }
                else if (this.gameDisplayModeSubMode == GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP)
                {
                    if (this.castle_AttackerSetup != null)
                    {
                        this.castle_AttackerSetup.Update(true);
                    }
                }
                else if ((this.gameDisplayModeSubMode == GameDisplaySubModes.SUBMODE_BATTLE) && (this.castle_Battle != null))
                {
                    this.castle_Battle.BattleUpdateManager(true);
                }
            }
            else
            {
                double currentMilliseconds;
                double callHomeRate;
                int num14;
                if (this.GameDisplayMode != GameDisplays.DISPLAY_WORLD)
                {
                    if (this.GameDisplayMode == GameDisplays.DISPLAY_REPORTS)
                    {
                        if ((this.tickCount % 10) == 0)
                        {
                            InterfaceMgr.Instance.updateVillageReports();
                        }
                        goto Label_095D;
                    }
                    if (this.GameDisplayMode == GameDisplays.DISPLAY_LEADERBOARD)
                    {
                        if ((this.tickCount % 10) == 0)
                        {
                            InterfaceMgr.Instance.updateVillageReports();
                        }
                        goto Label_095D;
                    }
                    if (this.GameDisplayMode == GameDisplays.DISPLAY_USER_INFO)
                    {
                        if ((this.tickCount % 10) == 0)
                        {
                            InterfaceMgr.Instance.updateVillageReports();
                        }
                        goto Label_095D;
                    }
                    if (this.GameDisplayMode == GameDisplays.DISPLAY_ALL_VILLAGES)
                    {
                        if ((this.tickCount % 10) == 0)
                        {
                            InterfaceMgr.Instance.updateVillageReports();
                        }
                        goto Label_095D;
                    }
                    if (this.GameDisplayMode == GameDisplays.DISPLAY_RANKINGS)
                    {
                        InterfaceMgr.Instance.updateVillageReports();
                        goto Label_095D;
                    }
                    if (this.GameDisplayMode == GameDisplays.DISPLAY_FACTIONS)
                    {
                        if ((this.tickCount % 10) == 0)
                        {
                            InterfaceMgr.Instance.updateVillageReports();
                        }
                        goto Label_095D;
                    }
                    if (this.GameDisplayMode == GameDisplays.DISPLAY_QUESTS)
                    {
                        InterfaceMgr.Instance.updateVillageReports();
                        goto Label_095D;
                    }
                    if (this.GameDisplayMode != GameDisplays.DISPLAY_ARMIES)
                    {
                        if (this.GameDisplayMode == GameDisplays.DISPLAY_RESEARCH)
                        {
                            InterfaceMgr.Instance.updateResearch((this.tickCount % 10) == 0);
                        }
                        else if (this.GameDisplayMode == GameDisplays.DISPLAY_AVATAR_EDITOR)
                        {
                            InterfaceMgr.Instance.updateVillageReports();
                        }
                        goto Label_095D;
                    }
                    if ((this.tickCount % 10) != 0)
                    {
                        goto Label_095D;
                    }
                    currentMilliseconds = DXTimer.GetCurrentMilliseconds();
                    callHomeRate = this.LocalWorldData.callHomeRate;
                    num14 = InterfaceMgr.Instance.getGameActivityMode();
                    switch (num14)
                    {
                        case 1:
                            callHomeRate *= 2.0;
                            break;

                        case 2:
                            callHomeRate *= 4.0;
                            break;

                        case 3:
                            callHomeRate *= 6.0;
                            break;

                        case 4:
                            callHomeRate *= 14.0;
                            break;

                        case 5:
                            callHomeRate *= 20.0;
                            break;
                    }
                }
                else
                {
                    bool special = false;
                    this.world.Update();
                    if (this.world.ZoomChange != 0.0)
                    {
                        this.world.changeZoom((float) this.world.ZoomChange);
                        if (this.world.ZoomChange <= 0.0)
                        {
                            this.world.centreMap(false);
                        }
                    }
                    if ((this.tickCount % 10) == 0)
                    {
                        InterfaceMgr.Instance.updateTraderInfo();
                        InterfaceMgr.Instance.updatePersonInfo();
                        this.World.updateLocalVillagesFromFactions();
                        InterfaceMgr.Instance.ensureInfoTabCleared();
                        this.World.monitorAIInvasionActivity();
                    }
                    double num8 = DXTimer.GetCurrentMilliseconds();
                    double num9 = this.LocalWorldData.callHomeRate;
                    int mode = InterfaceMgr.Instance.getGameActivityMode();
                    switch (mode)
                    {
                        case 1:
                            num9 *= 2.0;
                            break;

                        case 2:
                            num9 *= 4.0;
                            break;

                        case 3:
                            num9 *= 6.0;
                            break;

                        case 4:
                            num9 *= 14.0;
                            break;

                        case 5:
                            num9 *= 20.0;
                            break;
                    }
                    this.clockMode = mode;
                    double num11 = ((num8 - this.lastFullTickTime) * 64.0) / (num9 * 1000.0);
                    this.clockFrame = (int) num11;
                    if (this.clockFrame >= 0x3f)
                    {
                        this.clockFrame = 0x3f;
                    }
                    if (((num8 - this.lastFullTickTime) > (num9 * 1000.0)) || this.forceTriggerFullTick)
                    {
                        this.clockFrame = 0;
                        this.forceTriggerFullTick = false;
                        if ((num8 - this.lastFullTickRegisterTime) > 240000.0)
                        {
                            this.lastFullTickTime = num8;
                            this.lastFullTickRegisterTime = num8;
                            this.World.doFullTick(true, mode);
                        }
                        else if (!InterfaceMgr.Instance.isGameMinimised())
                        {
                            this.lastFullTickTime = num8;
                            this.World.doFullTick(false, mode);
                        }
                        special = true;
                    }
                    InterfaceMgr.Instance.worldTabUpdate(special);
                    goto Label_095D;
                }
                if ((currentMilliseconds - this.lastFullTickTime) > (callHomeRate * 1000.0))
                {
                    this.lastFullTickTime = currentMilliseconds;
                    this.World.doFullTick(false, num14);
                }
                InterfaceMgr.Instance.updateVillageReports();
            }
        Label_095D:
            if ((this.GameDisplayMode != GameDisplays.DISPLAY_QUESTS) && ((this.tickCount % 60) == 0x23))
            {
                NewQuestsPanel.handleClientSideQuestReporting(true);
            }
            if (this.forceTriggerFullTick && (this.GameDisplayMode != GameDisplays.DISPLAY_WORLD))
            {
                double num15 = DXTimer.GetCurrentMilliseconds();
                this.forceTriggerFullTick = false;
                this.lastFullTickTime = num15;
                this.World.doFullTick(false, 3);
            }
            if (((this.GameDisplayMode != GameDisplays.DISPLAY_WORLD) && (this.GameDisplayMode != GameDisplays.DISPLAY_ARMIES)) && !InterfaceMgr.Instance.isGameMinimised())
            {
                double num16 = DXTimer.GetCurrentMilliseconds();
                double num17 = this.LocalWorldData.callHomeRate;
                switch (InterfaceMgr.Instance.getGameActivityMode())
                {
                    case 0:
                        num17 *= 3.0;
                        break;

                    case 1:
                        num17 *= 4.0;
                        break;

                    case 2:
                        num17 *= 7.0;
                        break;

                    case 3:
                        num17 *= 10.0;
                        break;

                    case 4:
                        num17 *= 19.0;
                        break;

                    case 5:
                        num17 *= 25.0;
                        break;
                }
                if ((num16 - this.lastFullTickTime) > (num17 * 1000.0))
                {
                    this.lastFullTickTime = num16;
                    this.World.getArmiesIfNewAttacks();
                }
            }
            if ((this.tickCount % 10) == 0)
            {
                if (Instance.GameDisplayMode == GameDisplays.DISPLAY_CASTLE)
                {
                    int goldLevel = (int) this.world.getCurrentGold();
                    if ((this.Castle != null) && !this.World.isCapital(this.Castle.VillageID))
                    {
                        VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
                        this.Castle.adjustLevels(ref levels, ref goldLevel);
                        InterfaceMgr.Instance.setGold((double) goldLevel);
                    }
                }
                else
                {
                    InterfaceMgr.Instance.setGold(this.world.getCurrentGold());
                }
                InterfaceMgr.Instance.setHonour(this.world.getCurrentHonour(), this.world.getRank());
                InterfaceMgr.Instance.setFaithPoints(this.world.getCurrentFaithPoints());
                InterfaceMgr.Instance.setPoints(this.world.getCurrentPoints());
                InterfaceMgr.Instance.setServerTime(VillageMap.getCurrentServerTime(), this.World.getGameDay());
                if (RemoteServices.Instance.queueEmpty())
                {
                    InterfaceMgr.Instance.setConnectionLight(false);
                }
                else
                {
                    InterfaceMgr.Instance.setConnectionLight(true);
                }
                long highestAttackingArmy = -1L;
                int numAttacks = this.World.countIncomingAttacks(ref highestAttackingArmy);
                InterfaceMgr.Instance.getMainTabBar().incomingAttacks(numAttacks, highestAttackingArmy);
                InterfaceMgr.Instance.getMainTabBar().updateResearchTime(this.World.UserResearchData);
                foreach (VillageMap map2 in this.villages)
                {
                    if (map2 != null)
                    {
                        map2.makeTroopsUpdate();
                    }
                }
                this.World.updateArmyRetrievalData();
                this.monitorDownTime();
                Sound.monitorMusic();
            }
            InterfaceMgr.Instance.mailUpdate();
            InterfaceMgr.Instance.chatUpdate();
            InterfaceMgr.Instance.getMainTabBar().update();
            InterfaceMgr.Instance.updatePopups();
            this.World.updateResearch(false);
            this.World.updateArmies();
            this.World.updateTraders();
            this.World.updatePeople();
            PizzazzPopupWindow.updatePizzazz();
            InterfaceMgr.Instance.runTutorialWindow();
            this.manageInput();
            if ((this.tickCount % 0x708) == 0)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            if ((this.tickCount % 120) == 0)
            {
                this.World.runClientAchievementTests();
            }
            if (((this.noVillagePopup == null) && (this.noAutoVillagePopup == null)) && ((this.lostVillagePopup == null) && RemoteServices.Instance.Show5thAgeMessage))
            {
                Instance.openLostVillage(5);
                RemoteServices.Instance.Show2ndAgeMessage = false;
                RemoteServices.Instance.Show3rdAgeMessage = false;
                RemoteServices.Instance.Show4thAgeMessage = false;
            }
            if ((((this.noVillagePopup == null) && (this.noAutoVillagePopup == null)) && ((this.lostVillagePopup == null) && RemoteServices.Instance.Show4thAgeMessage)) && !RemoteServices.Instance.Show5thAgeMessage)
            {
                Instance.openLostVillage(4);
                RemoteServices.Instance.Show2ndAgeMessage = false;
                RemoteServices.Instance.Show3rdAgeMessage = false;
            }
            if ((((this.noVillagePopup == null) && (this.noAutoVillagePopup == null)) && ((this.lostVillagePopup == null) && RemoteServices.Instance.Show3rdAgeMessage)) && (!RemoteServices.Instance.Show4thAgeMessage && !RemoteServices.Instance.Show5thAgeMessage))
            {
                Instance.openLostVillage(3);
                RemoteServices.Instance.Show2ndAgeMessage = false;
            }
            if ((((this.noVillagePopup == null) && (this.noAutoVillagePopup == null)) && ((this.lostVillagePopup == null) && RemoteServices.Instance.Show2ndAgeMessage)) && ((!RemoteServices.Instance.Show3rdAgeMessage && !RemoteServices.Instance.Show4thAgeMessage) && !RemoteServices.Instance.Show5thAgeMessage))
            {
                Instance.openLostVillage(2);
            }
            if ((((this.noVillagePopup == null) && (this.noAutoVillagePopup == null)) && ((this.lostVillagePopup == null) && !this.World.isRetrievingUserVillages())) && !LoggingOutPopup.loggingOut)
            {
                if (this.World.numVillagesOwned() == 0)
                {
                    this.World.updateLastAttackerInfo();
                }
            }
            else if (this.noVillagePopup != null)
            {
                this.noVillagePopup.update();
            }
            else if (this.lostVillagePopup != null)
            {
                this.lostVillagePopup.update();
            }
            else if (this.noAutoVillagePopup != null)
            {
                this.noAutoVillagePopup.update();
            }
            TutorialWindow.runTutorial();
            this.debugPopupRun();
            this.loginHistoryRun();
            if (this.pendingUserVillageZoom)
            {
                int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
                if (villageID >= 0)
                {
                    this.pendingUserVillageZoom = false;
                    WorldMap.VillageData data = this.world.getVillageData(villageID);
                    if (data != null)
                    {
                        this.world.startMultiStageZoom(10000.0, (double) data.x, (double) data.y);
                    }
                }
            }
        Label_0F19:
            if (Program.ShowSeasonalFX && (SnowSystem.getInstance().snowTexture != null))
            {
                SnowSystem.getInstance().update(this.gfx);
            }
            if (this.finaliseResize)
            {
                try
                {
                    this.finaliseResize = false;
                    if (InterfaceMgr.Instance.ParentForm != null)
                    {
                        ((MainWindow) InterfaceMgr.Instance.ParentForm).finaliseResize();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void sessionExpired(int errorNo)
        {
            bool flag = false;
            if (errorNo == 11)
            {
                errorNo = 1;
                flag = true;
            }
            if (((errorNo == 1) && (InterfaceMgr.Instance.ParentForm != null)) && !this.appClose)
            {
                if (!InterfaceMgr.Instance.isConnectionErrorWindow() && !this.forcingLogout)
                {
                    if (!flag)
                    {
                        InterfaceMgr.Instance.closeAllPopups();
                        InterfaceMgr.Instance.getMainTabBar().changeTab(0);
                    }
                    InterfaceMgr.Instance.openConnectionErrorWindow();
                }
            }
            else
            {
                InterfaceMgr.Instance.closeAllPopups();
                InterfaceMgr.Instance.chatClose();
                if ((InterfaceMgr.Instance.ParentForm != null) && !this.appClose)
                {
                    switch (errorNo)
                    {
                        case 0:
                            if ((this.LocalWorldData.Alternate_Ruleset != 1) || (this.getDominationTimeLeft().TotalMinutes >= 5.0))
                            {
                                MyMessageBox.Show(SK.Text("GameEngine_Session_Lost_Message", "Your current session has been lost, please login again."), SK.Text("GameEngine_Session_Lost", "Session Lost"));
                                break;
                            }
                            Instance.openLostVillage(0x3e8);
                            while ((this.lostVillagePopup != null) && this.lostVillagePopup.Visible)
                            {
                                Thread.Sleep(100);
                                Application.DoEvents();
                            }
                            break;

                        case 1:
                            MyMessageBox.Show(SK.Text("GameEngine_Cannot_Access_Server", "Cannot access Server.") + "\n\n" + this.connectionErrorString, SK.Text("GENERIC_Connection_Error", "Connection Error"));
                            break;

                        case 2:
                            MyMessageBox.Show(SK.Text("GameEngine_Connection_Timed_Out", "Your connection has timed out."), SK.Text("GENERIC_Connection_Error", "Connection Error"));
                            break;

                        case 3:
                            MyMessageBox.Show(SK.Text("GameEngine_DownTime", "The server is now Offline for a scheduled downtime. You will now be logged off."), SK.Text("ServerDowntime_Scheduled_DownTime", "Scheduled Server Maintenance"));
                            break;
                    }
                    this.m_doReLogin = true;
                }
                this.World.invalidateWorldData();
                if ((this.dPop != null) && this.dPop.Created)
                {
                    this.dPop.Close();
                }
                if ((this.m_loginHistoryPop != null) && this.m_loginHistoryPop.Created)
                {
                    this.m_loginHistoryPop.Close();
                }
                this.pendingErrorCode = -1;
            }
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc, int type)
        {
            IntPtr ptr;
            using (Process process = Process.GetCurrentProcess())
            {
                using (ProcessModule module = process.MainModule)
                {
                    ptr = SetWindowsHookEx(type, proc, GetModuleHandle(module.ModuleName), 0);
                }
            }
            return ptr;
        }

        public void setNextFactionPage(int pageID)
        {
            this.nextFactionPage = pageID;
        }

        public void setPendingSessionExpiredStat(int errorNo)
        {
            this.pendingErrorCode = errorNo;
            if (errorNo != -1)
            {
                Instance.World.downloadingCounter = -100;
                this.forceRelogin();
            }
        }

        public void setProfileLogin(ProfileLoginWindow loginWindow)
        {
            this.m_loginWindow = loginWindow;
        }

        public void setServerDownTime(DateTime downtime)
        {
            if (downtime != this.serverDowntime)
            {
                if (downtime == DateTime.MinValue)
                {
                    this.clearServerDowntime();
                }
                else
                {
                    this.clearServerDowntime();
                    this.serverDowntime = downtime;
                    TimeSpan span = (TimeSpan) (this.serverDowntime - VillageMap.getCurrentServerTime());
                    if (span.TotalMinutes < 780.0)
                    {
                        this.warning24H = true;
                    }
                    if (span.TotalMinutes < 300.0)
                    {
                        this.warning12H = true;
                    }
                    if (span.TotalMinutes < 120.0)
                    {
                        this.warning4H = true;
                    }
                    if (span.TotalMinutes < 35.0)
                    {
                        this.warning60 = true;
                    }
                    if (span.TotalMinutes < 19.0)
                    {
                        this.warning30 = true;
                    }
                    if (span.TotalMinutes < 8.0)
                    {
                        this.warning15 = true;
                    }
                }
            }
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        public void showConnectingPopup()
        {
        }

        private void showDowntimeWarning(double minutes)
        {
            if (minutes >= 0.0)
            {
                this.clearDowntimePopup();
                this.m_downtimePopup = new ServerDowntimePopup();
                this.m_downtimePopup.show((int) minutes);
            }
        }

        public void showLoginHistory()
        {
            if (this.m_loginHistoryPop != null)
            {
                if (this.m_loginHistoryPop.Created)
                {
                    this.m_loginHistoryPop.Close();
                }
                this.m_loginHistoryPop = null;
            }
            this.m_loginHistoryPop = new LoginHistoryPopup();
            this.m_loginHistoryPop.Show();
        }

        public void SkipVillageTab()
        {
            this.skipVillageTab = true;
        }

        public void startResizeWindow()
        {
            InterfaceMgr.Instance.mainWindowStartResize();
        }

        private void TimerCallbackFunction(object o)
        {
        }

        public void toggleDebugPopup()
        {
            if (this.dPop == null)
            {
                this.dPop = new DebugPopup();
                this.dPop.Show();
            }
            else if (this.dPop.Created)
            {
                this.dPop.Close();
                this.dPop = null;
            }
            else
            {
                this.dPop = new DebugPopup();
                this.dPop.Show();
            }
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        public void uninstallKeyboardHook()
        {
            if (keyboardHookedInstalled)
            {
                UnhookWindowsHookEx(_hookID);
                keyboardHookedInstalled = false;
            }
        }

        public void updateConnectingPopup()
        {
            if (this.m_loginWindow != null)
            {
                this.m_loginWindow.update();
            }
        }

        public static void updateFolderPermissions(string path)
        {
            if (!updatedPermissions)
            {
                updatedPermissions = true;
                try
                {
                    SecurityIdentifier identifier = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
                    string account = (identifier.Translate(typeof(NTAccount)) as NTAccount).ToString();
                    AddDirectorySecurity(path, account, FileSystemRights.FullControl, AccessControlType.Allow);
                }
                catch (Exception)
                {
                }
            }
        }

        public void villageTabChange(int tabID)
        {
            if ((this.lastVillageTabID != tabID) || (tabID == 9))
            {
                InterfaceMgr.Instance.StopDrawing();
                InterfaceMgr.Instance.getVillageTabBar().updateShownTabs();
                this.lastVillageTabID = tabID;
                if (tabID <= 1)
                {
                    InterfaceMgr.Instance.clearControls();
                }
                else
                {
                    InterfaceMgr.Instance.clearControls(false, false, true, true);
                }
                this.gameDisplayModeSubMode = GameDisplaySubModes.SUBMODE_DEFAULT;
                if (InterfaceMgr.Instance.getSelectedMenuVillage() != this.lastLoadedVillage)
                {
                    this.downloadCurrentVillage();
                }
                switch (tabID)
                {
                    case 0:
                    {
                        InterfaceMgr.bgdBlurEnabled = false;
                        this.GameDisplayMode = GameDisplays.DISPLAY_VILLAGE;
                        this.gfx.BGColor = ARGBColors.Black;
                        if (!this.skipVillageTab)
                        {
                            InterfaceMgr.Instance.initVillageTab();
                        }
                        VillageMap map = this.getVillage(InterfaceMgr.Instance.getSelectedMenuVillage());
                        if (map != null)
                        {
                            map.playEnvironmentalSounds();
                        }
                        this.skipVillageTab = false;
                        break;
                    }
                    case 1:
                        this.GameDisplayMode = GameDisplays.DISPLAY_CASTLE;
                        InterfaceMgr.bgdBlurEnabled = false;
                        StatTrackingClient.Instance().ActivateTrigger(2, null);
                        this.gfx.BGColor = ARGBColors.Black;
                        InterfaceMgr.Instance.initCastleTab();
                        this.World.handleQuestObjectiveHappening(0x2714);
                        break;

                    case 2:
                        this.GameDisplayMode = GameDisplays.DISPLAY_VILLAGE;
                        InterfaceMgr.Instance.initVillageTabTabBarsOnly();
                        if (InterfaceMgr.Instance.isSelectedVillageACapital())
                        {
                            InterfaceMgr.Instance.setVillageTabSubMode(0x3ed);
                            break;
                        }
                        InterfaceMgr.Instance.setVillageTabSubMode(5);
                        break;

                    case 3:
                        this.GameDisplayMode = GameDisplays.DISPLAY_VILLAGE;
                        InterfaceMgr.Instance.initVillageTabTabBarsOnly();
                        if (InterfaceMgr.Instance.isSelectedVillageACapital())
                        {
                            InterfaceMgr.Instance.setVillageTabSubMode(0x3eb);
                            break;
                        }
                        InterfaceMgr.Instance.setVillageTabSubMode(3);
                        break;

                    case 4:
                        this.GameDisplayMode = GameDisplays.DISPLAY_VILLAGE;
                        InterfaceMgr.Instance.initVillageTabTabBarsOnly();
                        if (InterfaceMgr.Instance.isSelectedVillageACapital())
                        {
                            InterfaceMgr.Instance.setVillageTabSubMode(0x3ec);
                            break;
                        }
                        InterfaceMgr.Instance.setVillageTabSubMode(4);
                        break;

                    case 5:
                        this.GameDisplayMode = GameDisplays.DISPLAY_VILLAGE;
                        InterfaceMgr.Instance.initVillageTabTabBarsOnly();
                        if (InterfaceMgr.Instance.isSelectedVillageACapital())
                        {
                            if (InterfaceMgr.Instance.isSelectedVillageAParishCapital())
                            {
                                InterfaceMgr.Instance.setVillageTabSubMode(0x3f0);
                            }
                            else if (InterfaceMgr.Instance.isSelectedVillageACountyCapital())
                            {
                                InterfaceMgr.Instance.setVillageTabSubMode(0x454);
                            }
                            else if (InterfaceMgr.Instance.isSelectedVillageAProvinceCapital())
                            {
                                InterfaceMgr.Instance.setVillageTabSubMode(0x4b8);
                            }
                            else if (InterfaceMgr.Instance.isSelectedVillageACountryCapital())
                            {
                                InterfaceMgr.Instance.setVillageTabSubMode(0x51c);
                            }
                            break;
                        }
                        InterfaceMgr.Instance.setVillageTabSubMode(0x12);
                        break;

                    case 6:
                        this.GameDisplayMode = GameDisplays.DISPLAY_VILLAGE;
                        InterfaceMgr.Instance.initVillageTabTabBarsOnly();
                        if (InterfaceMgr.Instance.isSelectedVillageACapital())
                        {
                            if (InterfaceMgr.Instance.isSelectedVillageAParishCapital())
                            {
                                InterfaceMgr.Instance.setVillageTabSubMode(0x3ee);
                            }
                            else if (InterfaceMgr.Instance.isSelectedVillageACountyCapital())
                            {
                                InterfaceMgr.Instance.setVillageTabSubMode(0x452);
                            }
                            else if (InterfaceMgr.Instance.isSelectedVillageAProvinceCapital())
                            {
                                InterfaceMgr.Instance.setVillageTabSubMode(0x4b6);
                            }
                            else if (InterfaceMgr.Instance.isSelectedVillageACountryCapital())
                            {
                                InterfaceMgr.Instance.setVillageTabSubMode(0x51a);
                            }
                            break;
                        }
                        InterfaceMgr.Instance.setVillageTabSubMode(1);
                        break;

                    case 7:
                        this.GameDisplayMode = GameDisplays.DISPLAY_VILLAGE;
                        InterfaceMgr.Instance.initVillageTabTabBarsOnly();
                        if (InterfaceMgr.Instance.isSelectedVillageACapital())
                        {
                            if (InterfaceMgr.Instance.isSelectedVillageAParishCapital())
                            {
                                InterfaceMgr.Instance.setVillageTabSubMode(0x3ef);
                            }
                            else if (InterfaceMgr.Instance.isSelectedVillageACountyCapital())
                            {
                                InterfaceMgr.Instance.setVillageTabSubMode(0x453);
                            }
                            else if (InterfaceMgr.Instance.isSelectedVillageAProvinceCapital())
                            {
                                InterfaceMgr.Instance.setVillageTabSubMode(0x4b7);
                            }
                            else if (InterfaceMgr.Instance.isSelectedVillageACountryCapital())
                            {
                                InterfaceMgr.Instance.setVillageTabSubMode(0x51b);
                            }
                            break;
                        }
                        InterfaceMgr.Instance.setVillageTabSubMode(8);
                        break;

                    case 8:
                        InterfaceMgr.Instance.initVillageTabTabBarsOnly();
                        break;
                }
                InterfaceMgr.Instance.StartDrawing();
            }
        }

        public bool waitForConnectingPopupToClose()
        {
            return ((this.m_loginWindow != null) && this.m_loginWindow.Created);
        }

        public void windowClosing()
        {
            foreach (VillageMap map in this.villages)
            {
                map.dispose();
            }
            this.villages.Clear();
            foreach (CastleMap map2 in this.castles)
            {
                map2.dispose();
            }
            this.castles.Clear();
            this.World.UserCardData = null;
            InterfaceMgr.Instance.ignoreStopDraw = true;
            InterfaceMgr.Instance.logout();
            InterfaceMgr.Instance.changeTab(0);
            InterfaceMgr.Instance.ignoreStopDraw = false;
            this.World.clearParishChat();
            this.World.resetLeaderboards();
            this.World.logout();
            this.newResolution = -1;
            this.nextFactionPage = -1;
            this.villageToAbandon = -1;
        }

        public void worldZoomChange(double worldZoom, bool redraw)
        {
            this.world.WorldZoom = worldZoom;
            if (redraw)
            {
                this.run();
            }
        }

        public Audio AudioEngine
        {
            get
            {
                return this.audio;
            }
        }

        public CastleMap Castle
        {
            get
            {
                return this.castle;
            }
        }

        public CastleMap CastleAttackerSetup
        {
            get
            {
                return this.castle_AttackerSetup;
            }
        }

        public CastleMap CastleBattle
        {
            get
            {
                return this.castle_Battle;
            }
        }

        public int CurrentResolution
        {
            get
            {
                if (InterfaceMgr.Instance.ParentForm.Height < 960)
                {
                    return 0;
                }
                return 1;
            }
        }

        public GameDisplays GameDisplayMode
        {
            get
            {
                return this.gameDisplayMode;
            }
            set
            {
                if (Sound.isPlayingEnvironmental(0x13) && (value != GameDisplays.DISPLAY_WORLD))
                {
                    Sound.stopVillageEnvironmental();
                }
                this.gameDisplayMode = value;
                if (((value == GameDisplays.DISPLAY_WORLD) && (InterfaceMgr.Instance.ParentForm != null)) && InterfaceMgr.Instance.ParentForm.Visible)
                {
                    Sound.playVillageEnvironmental(0x13);
                }
            }
        }

        public GameDisplaySubModes GameDisplayModeSubMode
        {
            get
            {
                return this.gameDisplayModeSubMode;
            }
        }

        public GraphicsMgr GFX
        {
            get
            {
                return this.gfx;
            }
        }

        public WorldData LocalWorldData
        {
            get
            {
                return this.worldData;
            }
        }

        public int MaxResolution
        {
            get
            {
                return this.maxResolution;
            }
        }

        public int MovedFromVillageID
        {
            get
            {
                return this.movedFromVillageID;
            }
            set
            {
                this.movedFromVillageID = value;
                if ((value != -1) && !this.world.isCapital(value))
                {
                    this.movedFromVillageIDNonCapital = value;
                }
            }
        }

        public int NewResolution
        {
            get
            {
                return this.newResolution;
            }
        }

        public static NumberFormatInfo NFI
        {
            get
            {
                return nfi;
            }
        }

        public static NumberFormatInfo NFI_D
        {
            get
            {
                return nfi_decimal;
            }
        }

        public static NumberFormatInfo NFI_D1
        {
            get
            {
                return nfi_decimal1;
            }
        }

        public static NumberFormatInfo NFI_D2
        {
            get
            {
                return nfi_decimal2;
            }
        }

        public VillageMap Village
        {
            get
            {
                return this.village;
            }
        }

        public bool WindowActive
        {
            get
            {
                return this.windowActive;
            }
            set
            {
                this.windowActive = value;
                this.gfx.WindowActive = value;
            }
        }

        public WorldMap World
        {
            get
            {
                return this.world;
            }
        }

        public WorldMapTypes WorldMapTypesData
        {
            get
            {
                return this.worldMapTypesData;
            }
        }

        public enum GameDisplays
        {
            DISPLAY_VILLAGE,
            DISPLAY_WORLD,
            DISPLAY_REPORTS,
            DISPLAY_CASTLE,
            DISPLAY_RANKINGS,
            DISPLAY_RESEARCH,
            DISPLAY_ARMIES,
            DISPLAY_MAIL,
            DISPLAY_ELECTIONS,
            DISPLAY_POLITICS_VOTE,
            DISPLAY_POLITICS_FORUM,
            DISPLAY_AVATAR_EDITOR,
            DISPLAY_FACTIONS,
            DISPLAY_WEB,
            DISPLAY_LEADERBOARD,
            DISPLAY_QUESTS,
            DISPLAY_TEMP_DUMMY,
            DISPLAY_USER_INFO,
            DISPLAY_ALL_VILLAGES
        }

        public enum GameDisplaySubModes
        {
            SUBMODE_DEFAULT,
            SUBMODE_CASTLE_ATTACKER_SETUP,
            SUBMODE_BATTLE
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private delegate IntPtr MessageTextProc(int nCode, IntPtr wParam, IntPtr lParam);

        public class MouseClickMessageFilter : IMessageFilter
        {
            public bool PreFilterMessage(ref Message m)
            {
                return false;
            }
        }
    }
}

