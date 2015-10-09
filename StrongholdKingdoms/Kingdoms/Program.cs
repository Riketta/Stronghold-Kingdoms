namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using Skybound.Gecko;
    using StatTracking;
    using Stronghold.AuthClient;
    using Stronghold.ShieldClient;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Remoting;
    using System.Runtime.Remoting.Channels;
    using System.Runtime.Remoting.Channels.Ipc;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    internal static class Program
    {
        public static bool aeriaInstall = false;
        public static int arc_delayedManualOpening = -1;
        private static bool arc_delayedNextState = false;
        private static Form arc_form = null;
        private static bool arc_initialised = false;
        public static int arc_overlay_delay = 0;
        public static bool arc_overlay_open = false;
        public static bool arcInstall = false;
        public static bool arcLauncherStart = false;
        public static string arcToken = "";
        public static string arcUsername = "";
        public static bool bigpointInstall = false;
        public static bool bigpointPartnerInstall = false;
        public static List<SKLang> communityLangs = new List<SKLang>();
        public static int CurrentInstallerBuild = 0x74;
        public static bool gamersFirstError = false;
        public static bool gamersFirstInstall = false;
        public static string gamersFirstTokenMD5 = "";
        private static string[] geckoFXDlls = new string[] { 
            "sqlite3.dll", "nspr4.dll", "softokn3.dll", "AccessibleMarshal.dll", "freebl3.dll", "IA2Marshal.dll", "ssl3.dll", "js3250.dll", "javaxpcomglue.dll", "nss3.dll", "mozctl.dll", "mozcrt19.dll", "mozctlx.dll", "nssdbm3.dll", "nssckbi.dll", "smime3.dll", 
            "nssutil3.dll", "plc4.dll", "plds4.dll", "xpcom.dll", "xul.dll", @"plugins\npnul32.dll"
         };
        public static string installedLangCode = "";
        public static bool kingdomsAccountFound = false;
        private static bool last_arc_overlay_open = false;
        private static bool lastOverlayState = false;
        private static uint LOAD_WITH_ALTERED_SEARCH_PATH = 8;
        private static List<IntPtr> loadedDLLs = new List<IntPtr>();
        public static MySettings mySettings = null;
        public static ProfileLoginWindow profileLogin = null;
        public static byte[] steam_SessionTicket = null;
        public static int steam_SessionTicketUserID = 0;
        public static bool steamActive = false;
        public static string steamEmail = string.Empty;
        public static string steamID = string.Empty;
        public static bool steamInstall = false;
        public static bool steamOverlayActive = false;
        private static uint timerPeriod = 1;
        public static string WorldName = string.Empty;
        private static bool xmas_period = false;

        public static void arc_exit()
        {
            if (arcInstall)
            {
                if (arc_initialised)
                {
                    Arc_Exit();
                }
                arc_initialised = false;
            }
        }

        [DllImport("ArcWrapper.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern void Arc_Exit();
        public static void arc_forceoverlay()
        {
            if (!InterfaceMgr.Instance.isDXVisible())
            {
                InterfaceMgr.Instance.changeTab(9);
                InterfaceMgr.Instance.changeTab(0);
            }
            InterfaceMgr.Instance.closeAllPopups();
            arc_overlay_open = last_arc_overlay_open = true;
            arc_delayedManualOpening = 6;
            arc_delayedNextState = true;
        }

        [DllImport("ArcWrapper.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr Arc_GetAuthToken(IntPtr username);
        private static unsafe string arc_getTicket(string username)
        {
            List<char> list = new List<char>();
            foreach (char ch in username)
            {
                list.Add(ch);
            }
            IntPtr ptr2 = Arc_GetAuthToken(Marshal.UnsafeAddrOfPinnedArrayElement(list.ToArray(), 0));
            if (!(ptr2 != IntPtr.Zero))
            {
                return "";
            }
            char* chPtr = (char*) ptr2.ToPointer();
            List<char> list2 = new List<char>();
            for (int i = 0; i < 0x63; i++)
            {
                if (chPtr[i] == '\0')
                {
                    break;
                }
                list2.Add(chPtr[i]);
            }
            return new string(list2.ToArray());
        }

        public static void arc_init(Form form)
        {
            if (arcInstall && (Arc_Init(form.Handle) != 0))
            {
                arc_form = form;
                arc_initialised = true;
                arc_overlay_open = false;
                last_arc_overlay_open = false;
                arc_overlay_delay = 0;
            }
        }

        [DllImport("ArcWrapper.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int Arc_Init(IntPtr handle);
        public static void arc_launchClient(string lang)
        {
            Arc_LaunchClient();
        }

        [DllImport("ArcWrapper.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern void Arc_LaunchClient();
        public static bool arc_login(string username)
        {
            if (arcInstall)
            {
                arcUsername = username;
                if (Arc_Init(IntPtr.Zero) == 0)
                {
                    MessageBox.Show(SK.Text("ARC_Init_Error", "Unable to initialize Arc"), SK.Text("ARC_Error", "Arc Error"));
                    return false;
                }
                arc_initialised = true;
                arcToken = arc_getTicket(username);
                if (arcToken == "")
                {
                    MessageBox.Show(SK.Text("ARC_Login_Error", "Unable to login with user") + " : " + username, SK.Text("ARC_Error", "Arc Error"));
                    return false;
                }
                Arc_Exit();
            }
            return true;
        }

        public static void arc_openURL(string url)
        {
            List<char> list = new List<char>();
            foreach (char ch in url)
            {
                list.Add(ch);
            }
            IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(list.ToArray(), 0);
            if (!InterfaceMgr.Instance.isDXVisible())
            {
                InterfaceMgr.Instance.changeTab(9);
                InterfaceMgr.Instance.changeTab(0);
            }
            Arc_OpenURL(ptr);
        }

        [DllImport("ArcWrapper.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern void Arc_OpenURL(IntPtr url);
        [DllImport("ArcWrapper.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern void Arc_Overlay(int state);
        public static void arc_run()
        {
            if ((arcInstall && arc_initialised) && ((arc_form != null) && arc_form.Visible))
            {
                if (arc_delayedManualOpening > 0)
                {
                    arc_delayedManualOpening--;
                    if (arc_delayedManualOpening == 0)
                    {
                        if (arc_delayedNextState)
                        {
                            if (!arc_overlay_open)
                            {
                                Arc_Overlay(1);
                            }
                        }
                        else if (arc_overlay_open)
                        {
                            Arc_Overlay(0);
                        }
                    }
                }
                else if (arc_overlay_delay > 0)
                {
                    arc_overlay_delay--;
                    GameEngine.tabPressed = false;
                }
                else if (arc_overlay_open && !InterfaceMgr.Instance.isDXVisible())
                {
                    arc_overlay_open = last_arc_overlay_open = false;
                    Arc_Overlay(0);
                }
            }
        }

        private static void CurrentDomain_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception exception = e.Exception;
            MessageBox.Show(SK.Text("ProgramMain_Unexpected_Error", "There has been an unexpected error. Please forward a copy of this report to Support.") + "\n\n" + exception.Message + "\n\n" + exception.ToString(), SK.Text("ProgramMain_SK_Error", "Stronghold Kingdoms Error"));
            Application.Exit();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exceptionObject = e.ExceptionObject as Exception;
            MessageBox.Show(SK.Text("ProgramMain_Unexpected_Error", "There has been an unexpected error. Please forward a copy of this report to Support.") + "\n\n" + exceptionObject.Message + "\n\n" + exceptionObject.ToString(), SK.Text("ProgramMain_SK_Error", "Stronghold Kingdoms Error"));
            Application.Exit();
        }

        public static void DoEvents()
        {
            Application.DoEvents();
            CustomSelfDrawPanel.processInvalidRectCache();
        }

        public static void forceSteamDXOverlay()
        {
            if (!InterfaceMgr.Instance.isDXVisible())
            {
                InterfaceMgr.Instance.changeTab(9);
                InterfaceMgr.Instance.changeTab(0);
                lastOverlayState = true;
                OLActive();
            }
        }

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr dllPointer);
        public static unsafe string GetLocalUserName()
        {
            if (!steamActive)
            {
                return "";
            }
            byte* numPtr = (byte*) Steam_User_GetPersonaName().ToPointer();
            int index = 0;
            while (numPtr[index] != 0)
            {
                index++;
            }
            byte[] bytes = new byte[index];
            for (int i = 0; numPtr[i] != 0; i++)
            {
                bytes[i] = numPtr[i];
            }
            return Encoding.UTF8.GetString(bytes, 0, index);
        }

        public static string getNewArcToken()
        {
            if (arc_initialised)
            {
                return arc_getTicket(arcUsername);
            }
            if (Arc_Init(IntPtr.Zero) != 0)
            {
                string str = arc_getTicket(arcUsername);
                Arc_Exit();
                return str;
            }
            return "";
        }

        private static void loadGeckoDLLs(string basePath)
        {
            foreach (string str in geckoFXDlls)
            {
                IntPtr item = LoadWin32Library(basePath + @"\" + str);
                if (item != IntPtr.Zero)
                {
                    loadedDLLs.Add(item);
                }
            }
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibraryEx(string dllFilePath, IntPtr hFile, uint dwFlags);
        public static IntPtr LoadWin32Library(string dllFilePath)
        {
            try
            {
                IntPtr ptr = LoadLibraryEx(dllFilePath, IntPtr.Zero, LOAD_WITH_ALTERED_SEARCH_PATH);
                bool flag1 = ptr == IntPtr.Zero;
                return ptr;
            }
            catch (Exception)
            {
            }
            return IntPtr.Zero;
        }
#if DEBUG
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();
#endif

        [STAThread]
        private static void Main(string[] args)
        {
#if DEBUG
            AllocConsole();
#endif
            try
            {
                args = new string[] { "-InstallerVersion", CurrentInstallerBuild.ToString(), "en" };
#if DEBUG
                Console.WriteLine("Nichosy " + DataExport.version);
#endif

                DataExport.Fill();
                bool flag = false;
                bool flag2 = false;
                string str = "en";
                if ((args == null) || (args.Length < 1))
                {
                    flag = true;
                }
                if ((args != null) && (args.Length > 1))
                {
                    if (args[0].ToLowerInvariant() == "-installerversion")
                    {
                        if (Convert.ToInt32(args[1]) < CurrentInstallerBuild)
                        {
                            flag2 = true;
                        }
                    }
                    else if (!(args[0].ToLowerInvariant() == "-installer"))
                    {
                        flag = true;
                    }
                    if (args.Length > 2)
                    {
                        if (args[2].Length > 0)
                        {
                            str = args[2];
                        }
                        if ((args.Length > 3) && (args[3].Length > 0))
                        {
                            if (args[3] == "st")
                            {
                                steamInstall = true;
                            }
                            if (args[3] == "bp")
                            {
                                bigpointInstall = true;
                            }
                            if (args[3] == "bp2")
                            {
                                bigpointPartnerInstall = true;
                            }
                            if (args[3] == "ae")
                            {
                                aeriaInstall = true;
                            }
                            if (args[3] == "gf")
                            {
                                gamersFirstInstall = true;
                                if ((args.Length > 4) && (args[4].Length > 0))
                                {
                                    gamersFirstTokenMD5 = args[4];
                                }
                            }
                            if (args[3] == "arc")
                            {
                                if (args.Length > 4)
                                {
                                    arcUsername = args[4];
                                    arcInstall = true;
                                    if (arcUsername.Length <= 0)
                                    {
                                        arcLauncherStart = true;
                                    }
                                }
                                else
                                {
                                    arcLauncherStart = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    flag2 = true;
                }
                xmas_period = HolidayPeriods.xmas(DateTime.Now);
                if (arcLauncherStart)
                {
                    arc_launchClient(str);
                }
                else if (flag)
                {
                    MessageBox.Show(SK.Text("ProgramMain_Launch_Failure1", "This is not the game exe!") + Environment.NewLine + Environment.NewLine + SK.Text("ProgramMain_Launch_Failure2", "Please run Stronghold Kingdoms in the normal manner."), SK.Text("ProgramMain_Launch_Failure", "Stronghold Kingdoms Error"), MessageBoxButtons.OK);
                }
                else if (flag2 && !steamInstall)
                {
                    MessageBoxButtons oKCancel = MessageBoxButtons.OKCancel;
                    if (MessageBox.Show(SK.Text("ProgramMain_New_nInstaller", "A new version of the Updater/Installer is needed") + Environment.NewLine + SK.Text("ProgramMain_Must_Install", "You cannot Launch Stronghold Kingdoms until this is installed") + Environment.NewLine + Environment.NewLine + SK.Text("ProgramMain_Install_Now", "Do you wish to install this now?"), SK.Text("ProgramMain_Installer_Update", "Stronghold Kingdoms Installer Update"), oKCancel) == DialogResult.OK)
                    {
                        string path = InstallerUpdater.downloadSelfUpdater(new Uri("http://static.strongholdkingdoms.com/Kingdoms/kingdoms-setup-update-" + CurrentInstallerBuild.ToString() + ".exe"));
                        if ((path != null) && (path.Length > 0))
                        {
                            InstallerUpdater.runInstaller(path);
                        }
                    }
                }
                else
                {
                    bool flag3;
                    string name = @"Global\StrongholdKingdoms";
                    using (new Mutex(true, name, out flag3))
                    {
                        bool flag4;
                        try
                        {
                            OperatingSystem oSVersion = Environment.OSVersion;
                            if ((oSVersion.Platform == PlatformID.Win32NT) && (oSVersion.Version.Major >= 6))
                            {
                                SetProcessDPIAware();
                            }
                        }
                        catch (Exception)
                        {
                        }
                        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);
                        Application.ThreadException += new ThreadExceptionEventHandler(Program.CurrentDomain_ThreadException);
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        communityLangs = SKLocalization.scanForLanguages(GameEngine.getLangsPath());
                        installedLangCode = str;
                        mySettings = MySettings.load();
                        if (mySettings.LanguageIdent.Length == 0)
                        {
                            mySettings.LanguageIdent = str;
                        }
                        else if (mySettings.InstalledLanguageIdent != str)
                        {
                            mySettings.LanguageIdent = str;
                            mySettings.InstalledLanguageIdent = str;
                        }
                        if (!mySettings.OwnLanguageAvailableAndChecked)
                        {
                            string str6 = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
                            if (str6 != mySettings.LanguageIdent)
                            {
                                if (str6 == "en")
                                {
                                    mySettings.OwnLanguageAvailableAndChecked = true;
                                }
                                else if ((((str6 == "de") || (str6 == "fr")) || ((str6 == "ru") || (str6 == "es"))) || (((str6 == "pl") || (str6 == "it")) || ((str6 == "tr") || (str6 == "pt"))))
                                {
                                    string str7 = SK.Text("ProgramMain_A_New_Language", "A New Language is available : ");
                                    switch (str6)
                                    {
                                        case "de":
                                            str7 = str7 + "Deutsch";
                                            break;

                                        case "fr":
                                            str7 = str7 + "Fran\x00e7ais";
                                            break;

                                        case "ru":
                                            str7 = str7 + "Русский";
                                            break;

                                        case "es":
                                            str7 = str7 + "Espa\x00f1ol";
                                            break;

                                        case "pl":
                                            str7 = str7 + "Polski";
                                            break;

                                        case "it":
                                            str7 = str7 + "Italiano";
                                            break;

                                        case "tr":
                                            str7 = str7 + "T\x00fcrk\x00e7e";
                                            break;

                                        case "pt":
                                            str7 = str7 + "Portugu\x00eas do Brasil";
                                            break;
                                    }
                                    if (MessageBox.Show(str7 + Environment.NewLine + SK.Text("ProgramMain_Use_New_Language", "Your system settings indicate you are using this language, do you wish to play Stronghold Kingdoms in this language?"), SK.Text("ProgramMain_NewLanguageAvailable", "New Language Available"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        mySettings.LanguageIdent = str6;
                                    }
                                    mySettings.OwnLanguageAvailableAndChecked = true;
                                }
                            }
                            else
                            {
                                mySettings.OwnLanguageAvailableAndChecked = true;
                            }
                        }
                        switch (mySettings.LanguageIdent)
                        {
                            case "en":
                            case "de":
                            case "fr":
                            case "ru":
                            case "es":
                            case "pl":
                            case "pt":
                            case "tr":
                            case "it":
                                SKLocalization.LoadLocalization(Application.StartupPath + @"\Localization\", mySettings.LanguageIdent);
                                goto Label_079B;

                            default:
                                flag4 = false;
                                foreach (SKLang lang in communityLangs)
                                {
                                    if (lang.id == mySettings.LanguageIdent)
                                    {
                                        SKLocalization.LoadLocalization(GameEngine.getLangsPath(), lang.id);
                                        if ((SKLocalization.Instance == null) || !SKLocalization.Instance.valid)
                                        {
                                            flag4 = false;
                                        }
                                        else
                                        {
                                            flag4 = true;
                                        }
                                        break;
                                    }
                                }
                                break;
                        }
                        if (!flag4)
                        {
                            mySettings.LanguageIdent = "en";
                            SKLocalization.LoadLocalization(Application.StartupPath + @"\Localization\", mySettings.LanguageIdent);
                        }
                    Label_079B:
                        if (steamInstall)
                        {
                            bool flag5 = false;
                            try
                            {
                                if (Steam_Init() > 0)
                                {
                                    steamActive = true;
                                    Steam_getTicket();
                                    string sessionid = BitConverter.ToString(steam_SessionTicket).Replace("-", "");
                                    XmlRpcAuthRequest req = new XmlRpcAuthRequest("", "", "", "", sessionid, "", "", "");
                                    XmlRpcAuthResponse response = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath).AuthenticateSteamAccount(req, null, null, 0x3a98);
                                    if (response.SuccessCode == 1)
                                    {
                                        flag5 = true;
                                        steamID = response.Message;
                                        steamEmail = response.UserGUID;
                                        mySettings.AutoLogin = false;
                                        if (steamEmail.Trim().Length > 0)
                                        {
                                            kingdomsAccountFound = true;
                                        }
                                        else
                                        {
                                            kingdomsAccountFound = false;
                                        }
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                steamActive = false;
                            }
                            if (!flag5)
                            {
                                MessageBox.Show(SK.Text("Steam_steam_required", "Stronghold Kingdoms requires the Steam Client to be running in Online mode."), SK.Text("Steam_error", "Steam Error"));
                                Application.Exit();
                                return;
                            }
                        }
                        if (!arcInstall || arc_login(arcUsername))
                        {
                            if (gamersFirstInstall && (gamersFirstTokenMD5.Length == 0))
                            {
                                MessageBox.Show(SK.Text("GF_token_error", "Unable to verify your GamersFirst identity. Please try again. If this issue persists, please contact support."), SK.Text("GF_Error", "GamersFirst Error"));
                                Application.Exit();
                            }
                            else
                            {
                                LoadingPanel panel = new LoadingPanel();
                                panel.init();
                                panel.Show();
                                panel.TopMost = true;
                                panel.BringToFront();
                                panel.Focus();
                                panel.BringToFront();
                                panel.TopMost = false;
                                string basePath = Application.StartupPath + @"\geckofx\xulrunner";
                                loadGeckoDLLs(basePath);
                                Xpcom.Initialize(basePath);
                                bool flag6 = testMutex();
                                if (!flag3 || flag6)
                                {
                                    MessageBox.Show(SK.Text("ProgramMain_Already_Running", "Already running") + "...", "Stronghold Kingdoms");
                                }
                                else
                                {
                                    bool flag7 = true;
                                    TimerCaps caps = new TimerCaps();
                                    timeGetDevCaps(ref caps, Marshal.SizeOf(caps));
                                    timerPeriod = Math.Max(caps.periodMin, 1);
                                    timeBeginPeriod(timerPeriod);
                                    DXTimer.Init();
                                    GameEngine engine = null;
                                    try
                                    {
                                        engine = new GameEngine();
                                    }
                                    catch (FileNotFoundException exception)
                                    {
                                        if (exception.FileName.Contains("irectX"))
                                        {
                                            GameEngine.displayDirectXError();
                                        }
                                        timeEndPeriod(timerPeriod);
                                        panel.Close();
                                        return;
                                    }
                                    GraphicsMgr mgr = new GraphicsMgr();
                                    int maxRes = 2;
                                    Screen primaryScreen = Screen.PrimaryScreen;
                                    int width = primaryScreen.Bounds.Width;
                                    int height = primaryScreen.Bounds.Height;
                                    if ((width < 0x400) || (height < 0x300))
                                    {
                                        MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
                                        panel.Close();
                                        panel = null;
                                        if (MessageBox.Show(SK.Text("ProgramMain_Screen_Too_Small", "Your screen resolution is too small to run Stronghold Kingdoms") + Environment.NewLine + Environment.NewLine + SK.Text("ProgramMain_Try_Anyway", "Try to anyway?"), SK.Text("ProgramMain_Error", "Error"), yesNo) != DialogResult.Yes)
                                        {
                                            timeEndPeriod(timerPeriod);
                                            return;
                                        }
                                    }
                                    int num6 = width - 80;
                                    int num7 = height - 100;
                                    if (num6 < 0x3b0)
                                    {
                                        num6 = 0x3b0;
                                    }
                                    if (num7 < 0x29c)
                                    {
                                        num7 = 0x29c;
                                    }
                                    mySettings.Save();
                                    CastleMap.displayCollapsed = mySettings.CastleWalls;
                                    MainWindow newParentMainWindow = null;
                                    Form form = null;
                                    form = new MainWindow
                                    {
                                        Visible = false
                                    };
                                    if (arcInstall)
                                    {
                                        arc_init(form);
                                    }
                                    ((MainWindow)form).allowResizing(false);
                                    int screenWidth = num6;
                                    int screenHeight = num7;
                                    if (mySettings.ScreenWidth > 0)
                                    {
                                        screenWidth = mySettings.ScreenWidth;
                                    }
                                    if (mySettings.ScreenHeight > 0)
                                    {
                                        screenHeight = mySettings.ScreenHeight;
                                    }
                                    if (screenWidth > width)
                                    {
                                        screenWidth = width;
                                    }
                                    if (screenHeight > height)
                                    {
                                        screenHeight = height;
                                    }
                                    if (flag7)
                                    {
                                        form.MaximumSize = new Size(0xf00, 0x870);
                                        form.ClientSize = new Size(screenWidth, screenHeight);
                                    }
                                    else
                                    {
                                        form.ClientSize = new Size(0x3e8, 720);
                                        form.MaximumSize = new Size(0x41a, 760);
                                    }
                                    if (mySettings.Maximize)
                                    {
                                        form.WindowState = FormWindowState.Maximized;
                                    }
                                    form.Text = "Stronghold Kingdoms";
                                    ((MainWindow)form).allowResizing(true);
                                    newParentMainWindow = (MainWindow)form;
                                    InterfaceMgr.Instance.registerForm(form, newParentMainWindow);
                                    if (!engine.Initialise(mgr, maxRes, 2))
                                    {
                                        if (panel != null)
                                        {
                                            panel.Close();
                                            panel = null;
                                        }
                                    }
                                    else
                                    {
                                        SVG_Source instance = SVG_Source.Instance;
                                        Sound.setMusicState(mySettings.Music);
                                        GameEngine.Instance.AudioEngine.setMP3MasterVolume(((float)mySettings.MusicVolume) / 100f, 0);
                                        Sound.setSFXState(mySettings.SFX);
                                        Sound.setBattleSFXState(mySettings.BattleSFX);
                                        GameEngine.Instance.AudioEngine.setSFXMasterVolume(((float)mySettings.SFXVolume) / 100f);
                                        Sound.setEnvironmentalState(mySettings.Environmentals);
                                        GameEngine.Instance.AudioEngine.setEnvironmentalMasterVolume(((float)mySettings.EnvironmentalVolume) / 100f);
                                        bool flag8 = true;
                                        if (panel != null)
                                        {
                                            panel.Close();
                                            panel = null;
                                        }
                                        RemoteServices.Instance.initChannel();
                                        while (flag8)
                                        {
                                            engine.reLogin();
                                            flag8 = false;
                                            RemoteServices.Instance.UserID = -1;
                                            RemoteServices.Instance.set_CommonData_UserCallBack(null);
                                            while (RemoteServices.Instance.UserID < 0)
                                            {
                                                engine.installKeyboardHook();
                                                GameEngine.Instance.reLogin();
                                                GameEngine.Instance.clearServerDowntime();
                                                profileLogin = engine.getLoginWindow();
                                                if (profileLogin == null)
                                                {
                                                    profileLogin = new ProfileLoginWindow();
                                                    GameEngine.Instance.setProfileLogin(profileLogin);
                                                    profileLogin.Show();
                                                    profileLogin.init();
                                                }
                                                else
                                                {
                                                    profileLogin.openAfterCancel();
                                                }
                                                RemoteServices.Instance.clearQueues();
                                                while (profileLogin.Created && profileLogin.UserEntryMode)
                                                {
                                                    RemoteServices.Instance.processData();
                                                    Thread.Sleep(1);
                                                    DoEvents();
                                                    profileLogin.update();
                                                    StatTrackingClient.Instance().Update(0.01);
                                                }
                                                GameEngine.Instance.reLogin();
                                                form.Text = "Stronghold Kingdoms";
                                                if (WorldName != string.Empty)
                                                {
                                                    form.Text = form.Text + " - " + WorldName;
                                                }
                                                if (RemoteServices.Instance.UserID == -1)
                                                {
                                                    GameEngine.Instance.killLoadThread();
                                                    mySettings.Maximize = form.WindowState == FormWindowState.Maximized;
                                                    form.Close();
                                                    shutdown();
                                                    return;
                                                }
                                            }
                                            engine.showConnectingPopup();
                                            engine.World.loadLocalWorldData();
                                            engine.World.updateWorldMapOwnership();
                                            bool flag9 = true;
                                            while (flag9)
                                            {
                                                flag9 = false;
                                                VillageMap.loadVillageBuildingsGFX();
                                                while (engine.isStillLoading())
                                                {
                                                    Thread.Sleep(10);
                                                    DoEvents();
                                                    RemoteServices.Instance.processData();
                                                    GameEngine.Instance.updateConnectingPopup();
                                                }
                                                GameEngine.Instance.World.initSprites(GameEngine.Instance.GFX);
                                                GameEngine.Instance.resumeCommonRemote();
                                                engine.enableConnectingPopup();
                                                while (!GameEngine.Instance.World.isDownloadComplete())
                                                {
                                                    Thread.Sleep(10);
                                                    Application.DoEvents();
                                                    RemoteServices.Instance.processData();
                                                    GameEngine.Instance.updateConnectingPopup();
                                                    if (engine.loginCancelled())
                                                    {
                                                        break;
                                                    }
                                                }

                                                if (DataExport.controlForm != null)
                                                    DataExport.controlForm.Close();
                                                DataExport.controlForm = new ControlForm();
                                                DataExport.controlForm.Show();

                                                if (engine.pendingError())
                                                {
                                                    engine.updateConnectingPopup();
                                                    engine.forceRelogin();
                                                }
                                                if (!engine.loginCancelled())
                                                {
                                                    engine.World.saveFactionData();
                                                    engine.World.saveNamesData();
#if DEBUG
                                                    DataExport.saveFactionData(engine.World);
                                                    DataExport.saveNamesData(engine.World);
#endif
                                                    engine.enableConnectingPopup2();
                                                    while (engine.waitForConnectingPopupToClose())
                                                    {
                                                        Thread.Sleep(10);
                                                        DoEvents();
                                                        RemoteServices.Instance.processData();
                                                        if (engine.loginCancelled())
                                                        {
                                                            break;
                                                        }
                                                    }
                                                }
                                                if (RemoteServices.Instance.UserID == -1)
                                                {
                                                    GameEngine.Instance.killLoadThread();
                                                    mySettings.Maximize = form.WindowState == FormWindowState.Maximized;
                                                    form.Close();
                                                    shutdown();
                                                    return;
                                                }
                                                Sound.playMusic();

                                                bool flag10 = false;
                                                if (!engine.reLogin())
                                                {
                                                    InterfaceMgr.Instance.setupVillageName();
                                                    form.Show();
                                                    form.Visible = true;
                                                    newParentMainWindow.MainWindowLarge_SizeChanged(null, null);
                                                    GameEngine.Instance.lateStart();
                                                    if ((GameEngine.Instance.World.numVillagesOwned() > 0) && RemoteServices.Instance.ShowAdminMessage)
                                                    {
                                                        AdminInfoPopup.showMessage();
                                                    }
                                                    while (form.Created)
                                                    {
                                                        engine.run();
                                                        if (engine.reLogin())
                                                        {
                                                            form.Hide();
                                                            form.Visible = false;
                                                            engine.windowClosing();
                                                            if (!engine.quitting())
                                                            {
                                                                flag8 = true;
                                                            }
                                                            flag10 = true;
                                                            break;
                                                        }
                                                        StatTrackingClient.Instance().Update(0.01);
                                                        RemoteServices.Instance.processData();
                                                        if (form.Created)
                                                        {
                                                            Thread.Sleep(1);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    flag8 = true;
                                                    flag10 = true;
                                                }
                                                if (!flag10)
                                                {
                                                    form.Hide();
                                                    form.Visible = false;
                                                    form = null;
                                                    newParentMainWindow = null;
                                                }
                                                engine.World.saveFactionData();
                                                engine.World.saveNamesData();
#if DEBUG
                                                DataExport.saveFactionData(engine.World);
                                                DataExport.saveNamesData(engine.World);
#endif
                                                Sound.stopMusic();
                                            }
                                        }
                                        try
                                        {
                                            if (form != null)
                                            {
                                                mySettings.Maximize = form.WindowState == FormWindowState.Maximized;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                        }
                                        shutdown();
                                    }
                                }
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                File.WriteAllText("ex.txt", ex.ToString());
            }
            finally
            {
                if (DataExport.controlForm != null)
                    DataExport.controlForm.Close();
            }
        }

        public static void OLActive()
        {
            steamOverlayActive = true;
            InterfaceMgr.Instance.closeAllPopups();
            InterfaceMgr.Instance.ParentMainWindow.makeFullDX();
        }

        public static void OLInactive()
        {
            steamOverlayActive = false;
            InterfaceMgr.Instance.ParentMainWindow.restoreDXSize();
        }

        private static string readLocalTxt()
        {
            string str = "en";
            StreamReader reader = null;
            try
            {
                reader = File.OpenText(Application.StartupPath + @"\local.txt");
                str = reader.ReadLine();
                reader.Close();
            }
            catch (Exception)
            {
                try
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                }
            }
            str = str.Substring(0, 2);
            if ((((str != "en") && (str != "fr")) && ((str != "de") && (str != "ru"))) && (str != "es"))
            {
                str = "en";
            }
            return str;
        }

        [DllImport("user32.dll", SetLastError=true)]
        private static extern bool SetProcessDPIAware();
        private static void shutdown()
        {
            mySettings.CastleWalls = CastleMap.displayCollapsed;
            StatTrackingClient.Instance().Terminate();
            mySettings.Save();
            RemoteServices.Instance.LogOut(false, false, false, false, false, false, false, 0, 100, false, false, false, false, false, false, 500, 500, 500, 500, 250);
            GameEngine.Instance.uninstallKeyboardHook();
            Thread.Sleep(0x3e8);
            unloadGeckoDLLs();
            timeEndPeriod(timerPeriod);
            if (GameEngine.Instance.AudioEngine != null)
            {
                GameEngine.Instance.AudioEngine.closeAudio();
            }
            if (arcInstall)
            {
                Arc_Exit();
            }
        }

        [DllImport("SteamWrap3.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr Steam_GetAuthTicket();
        [DllImport("SteamWrap3.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int Steam_GetAuthTicketLength();
        public static ulong Steam_GetSteamUserID()
        {
            if (steamActive)
            {
                return Steam_User_GetLocalUserSteamID();
            }
            return 0L;
        }

        public static unsafe void Steam_getTicket()
        {
            IntPtr ptr = Steam_GetAuthTicket();
            int num = Steam_GetAuthTicketLength();
            if (num != -1)
            {
                byte* numPtr = (byte*) ptr.ToPointer();
                steam_SessionTicket = new byte[num];
                for (int i = 0; i < num; i++)
                {
                    steam_SessionTicket[i] = numPtr[i];
                }
            }
        }

        [DllImport("SteamWrap3.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int Steam_Init();
        [DllImport("SteamWrap3.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int Steam_IsOverlayEnabled();
        [DllImport("SteamWrap3.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern uint Steam_MT_AppID();
        [DllImport("SteamWrap3.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int Steam_MT_Authorised();
        [DllImport("SteamWrap3.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern void Steam_MT_Clear_Response();
        [DllImport("SteamWrap3.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int Steam_MT_Got_Response();
        [DllImport("SteamWrap3.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern ulong Steam_MT_OrderID();
        [DllImport("SteamWrap3.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern void Steam_OpenOverlay();
        [DllImport("SteamWrap3.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern int Steam_OverlayActive();
        public static bool Steam_PollOverlayStatus()
        {
            return (steamActive && (Steam_OverlayActive() > 0));
        }

        public static void steam_run()
        {
            if (steamActive)
            {
                Steam_RunCallBacks();
                bool lastOverlayState = Program.lastOverlayState;
                if (Steam_OverlayActive() > 0)
                {
                    if (!Program.lastOverlayState)
                    {
                        Program.lastOverlayState = true;
                        OLActive();
                    }
                }
                else if (Program.lastOverlayState)
                {
                    Program.lastOverlayState = false;
                    OLInactive();
                }
                if (((!Program.lastOverlayState && !lastOverlayState) && (GameEngine.tabPressed && GameEngine.shiftPressed)) && ((Steam_IsOverlayEnabled() > 0) && !InterfaceMgr.Instance.isDXVisible()))
                {
                    InterfaceMgr.Instance.changeTab(9);
                    InterfaceMgr.Instance.changeTab(0);
                    Steam_OpenOverlay();
                }
                if (Steam_MT_Got_Response() > 0)
                {
                    bool flag2 = Steam_MT_Authorised() == 1;
                    ulong num = Steam_MT_OrderID();
                    Steam_MT_AppID();
                    Steam_MT_Clear_Response();
                    if (flag2)
                    {
                        XmlRpcAuthProvider provider = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
                        XmlRpcAuthRequest req = new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), null, null, null) {
                            OrderID = num.ToString()
                        };
                        if (provider.SteamPaymentFinal(req, null, null, 0x3a98).SuccessCode == 1)
                        {
                            MyMessageBox.Show(SK.Text("STEAM_AUTHORIZED_OK", "Your payment was received and your crowns have been credited - Thank You!"));
                        }
                        else
                        {
                            MyMessageBox.Show(SK.Text("STEAM_AUTHORIZED_BAD", "There was a problem processing your payment through Steam, please contact support."));
                        }
                    }
                }
            }
        }

        [DllImport("SteamWrap3.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern void Steam_RunCallBacks();
        [DllImport("SteamWrap3.dll", CallingConvention=CallingConvention.Cdecl)]
        public static extern ulong Steam_User_GetLocalUserSteamID();
        [DllImport("SteamWrap3.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern IntPtr Steam_User_GetPersonaName();
        public static bool testMutex()
        {
            try
            {
                IpcClientChannel chnl = new IpcClientChannel();
                ChannelServices.RegisterChannel(chnl, false);
                SHKMutex mutex = (SHKMutex) Activator.GetObject(typeof(SHKMutex), "ipc://SHKMutex/KingdomsRemoteObj");
                try
                {
                    if (mutex.HelloWorld() == "Hello World")
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                }
                ChannelServices.UnregisterChannel(chnl);
                IpcServerChannel channel2 = new IpcServerChannel("SHKMutex");
                ChannelServices.RegisterChannel(channel2, false);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(SHKMutexObject), "KingdomsRemoteObj", WellKnownObjectMode.SingleCall);
            }
            catch (Exception)
            {
            }
            return false;
        }

        [DllImport("winmm.dll")]
        internal static extern uint timeBeginPeriod(uint period);
        [DllImport("winmm.dll")]
        internal static extern uint timeEndPeriod(uint period);
        [DllImport("winmm.dll")]
        private static extern int timeGetDevCaps(ref TimerCaps caps, int sizeOfTimerCaps);
        private static void unloadGeckoDLLs()
        {
            foreach (IntPtr ptr in loadedDLLs)
            {
                FreeLibrary(ptr);
            }
        }

        public static bool ShowSeasonalFX
        {
            get
            {
                return (xmas_period && mySettings.SeasonalSpecialFX);
            }
        }

        public static bool ShowSeasonalFXOption
        {
            get
            {
                return xmas_period;
            }
        }

        public static bool ShowSeasonalGraphics
        {
            get
            {
                return (xmas_period && mySettings.SeasonalWinterLandscape);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TimerCaps
        {
            public uint periodMin;
            public uint periodMax;
        }
    }
}

