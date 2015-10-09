namespace Kingdoms
{
    using CommonTypes;
    using Skybound.Gecko;
    using StatTracking;
    using Stronghold.AuthClient;
    using Stronghold.ShieldClient;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Security;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;

    public class ProfileLoginWindow : Form
    {
        private Image accountImage;
        private Image accountImageOver;
        private string AdminGUID;
        public static string AeriaToken = string.Empty;
        private List<CustomSelfDrawPanel.CSDControl> allButtons;
        private static string bp2_currentGuid = "";
        private int bp2_loginMode;
        private static string bp2_logoutURL = "";
        private KingdomsBrowserGecko browserServerNews;
        public CustomSelfDrawPanel BrowserTabsControls;
        public CustomSelfDrawPanel.CSDImage btnClientLogout;
        private Button btnExit;
        public CustomSelfDrawPanel.CSDImage btnLogin;
        public CustomSelfDrawPanel.CSDButton btnLoginFB;
        public CustomSelfDrawPanel.CSDImage btnShieldDesigner;
        public CustomSelfDrawPanel.CSDImage cancelButton;
        private Image cancelImage;
        private Image cancelImageOver;
        private static bool certPolicyCreated = false;
        public CustomSelfDrawPanel.CSDCheckBox chkAutoLogin;
        private Image closedImage;
        private Image closedImageOver;
        private IContainer components;
        public bool connectingCancelled;
        private Image createAccountImage;
        private Image createAccountImage2;
        private Image createAccountImageOver;
        private Image createAccountImageOver2;
        private string defaultWindowTitle = "";
        private bool delayedCreateUserOpen;
        private bool emailOptInPopup;
        private bool emailOptInState;
        public CustomSelfDrawPanel.CSDImage exitButton;
        private Image exitImage;
        private Image exitImageOver;
        private string FacebookToken = "";
        public CustomSelfDrawPanel.CSDLine feedbackLine;
        public CustomSelfDrawPanel.CSDFill feedbackProgress;
        public CustomSelfDrawPanel.CSDArea feedbackProgressArea;
        private Image forgottenImage;
        private Image forgottenImageOver;
        public CustomSelfDrawPanel.CSDLabel forumLabel;
        public CustomSelfDrawPanel.CSDLabel gameRulesLabel;
        private KingdomsBrowserGecko geckoWebBrowser1;
        public static string gfEmail = "";
        public static string gfPW = "";
        public CustomSelfDrawPanel.CSDImage GreyoutLogin;
        public CustomSelfDrawPanel.CSDImage GreyoutTabs;
        public CustomSelfDrawPanel.CSDImage GreyoutWorlds;
        private bool ignoreEmailChange;
        public CustomSelfDrawPanel.CSDButton imgFacebook = new CustomSelfDrawPanel.CSDButton();
        public CustomSelfDrawPanel.CSDButton imgSHKRu = new CustomSelfDrawPanel.CSDButton();
        public CustomSelfDrawPanel.CSDButton imgTwitter = new CustomSelfDrawPanel.CSDButton();
        public CustomSelfDrawPanel.CSDButton imgYoutube = new CustomSelfDrawPanel.CSDButton();
        public string initialisedLanguage = "";
        public static bool inSpecialWorld = false;
        private Image joinImage;
        private Image joinImageOver;
        private Label label1;
        public static Dictionary<string, LocalizationLanguage> LanguageList;
        private int lastCount = -1;
        private static string lastLoadedEmail = "";
        private static string lastLoadedEmail2 = "";
        private DateTime lastLogoutClicked = DateTime.MinValue;
        public static int LastNumberOfWorldsPlaying = 0;
        public static string LastSelectedSupportCulture = Program.mySettings.LanguageIdent.ToLower();
        private int lastWorldLoggedIn = -1;
        public CustomSelfDrawPanel.CSDLabel lblEmail;
        public CustomSelfDrawPanel.CSDLabel lblEmailSteam;
        public CustomSelfDrawPanel.CSDLabel lblLoginError;
        public CustomSelfDrawPanel.CSDLabel lblNewWorlds;
        public CustomSelfDrawPanel.CSDLabel lblPassword;
        public CustomSelfDrawPanel.CSDLabel lblRetrieving;
        public CustomSelfDrawPanel.CSDImage lblUsername;
        public CustomSelfDrawPanel.CSDLabel lblVersion;
        public CustomSelfDrawPanel.CSDLabel lblWorldsOfflineError;
        public CustomSelfDrawPanel.CSDLabel lblWorldsOnlineError;
        public static bool LoggedInViaFacebook = false;
        public List<CustomSelfDrawPanel.CSDControl> loggedInWorldControls;
        public List<CustomSelfDrawPanel.CSDControl> loggedOutWorldControls;
        private bool loginButtonActive = true;
        private Image loginImage;
        private Image loginImageOver;
        public CustomSelfDrawPanel LoginPanelControls_Feedback;
        public CustomSelfDrawPanel LoginPanelControls_LoggedIn;
        public CustomSelfDrawPanel LoginPanelControls_LoggedOut;
        private Image logoutImage;
        private Image logoutImageOver;
        private Image newsImage;
        private Image newsImageOver;
        public static bool NewWorldsAvailable = false;
        private Image optionsImage;
        private Image optionsImageOver;
        private Panel panel1;
        private Panel panel2;
        private int PlayerGameworldCount;
        private Image playImage;
        private Image playImageOver;
        private NoDrawPanel pnlFeedback;
        private NoDrawPanel pnlLogin;
        private NoDrawPanel pnlTabs;
        private NoDrawPanel pnlWorlds;
        private Image selectImage;
        private Image selectImageOver;
        private bool selfClosing;
        private string serverAddr = string.Empty;
        public static Dictionary<string, Image> ShieldImage = new Dictionary<string, Image>();
        public static Dictionary<string, string> ShieldURL = new Dictionary<string, string>();
        private bool specialFacebookLogin;
        public static string specialWorldName = "";
        public static string storedUserLoginEmail = "";
        private string strAccountDetails = SK.Text("LOGIN_AccountDetails", "Account Details");
        private string strCancel = SK.Text("GENERIC_Cancel", "Cancel");
        private string strClosed = SK.Text("FactionInvites_Membership_closed", "Closed");
        private string strCreateAccount = SK.Text("LOGIN_CreateAccount", "Create Account");
        private string strEmailAddress = SK.Text("LOGIN_Email", "Email Address");
        private string strExit = SK.Text("GENERIC_Exit", "Exit");
        private string strForgottenPassword = SK.Text("LOGIN_ForgottenPassword", "Forgotten Password");
        private string strGenericLoginError = SK.Text("LOGIN_GenericLoginError", "Login Failed: Please check that your email and password are entered correctly.");
        private string strGenericLoginErrorConnection = SK.Text("LOGIN_GenericLoginErrorConnection", "Login Failed: There is a problem connecting to the Login Server.");
        private string strJoin = SK.Text("WORLD_Join", "Join");
        private string strLogin = SK.Text("LOGIN_Login", "Login");
        private string strLogout = SK.Text("LogoutPanel_Logout", "Logout");
        private string strNews = SK.Text("LOGIN_News", "News");
        private string strOffline = SK.Text("WORLD_Offline", "Offline");
        private string strOnline = SK.Text("WORLD_Online", "Online");
        private string strOptions = SK.Text("MENU_Settings", "Settings");
        private string strPassword = SK.Text("LOGIN_Password", "Password");
        private string strPlay = SK.Text("WORLD_Play", "Play");
        private string strSelect = SK.Text("WORLD_Select", "Select World");
        private string strWorldEnded = SK.Text("WorldEnded", "This World has ended.");
        private static bool successfulAutoLogin = false;
        public CustomSelfDrawPanel.CSDLabel supportLabel;
        public CustomSelfDrawPanel.CSDLabel tandcLabel;
        private static WebBrowser tempBrowser = null;
        private bool tempFacebookLogin;
        public TextBox txtEmail;
        public TextBox txtPassword;
        public Uri URL_gameWorldPage;
        public Uri URL_landingPage;
        public bool UserEntryMode = true;
        private Color WebButtonblue = Color.FromArgb(0x55, 0x91, 0xcb);
        private Color WebButtonGrey = Color.FromArgb(0xe1, 0xe1, 0xe1);
        private int WebButtonheight = 0x16;
        private int WebButtonRadius = 10;
        private Color WebButtonRed = Color.FromArgb(160, 0, 0);
        private int WebButtonWidth = 60;
        private Color WebButtonYellow = Color.FromArgb(0xe1, 0xe1, 0);
        private Font WebTextFontBold = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-Bold.ttf", 10f, FontStyle.Bold);
        private Font WebTextFontBoldCond = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 10f, FontStyle.Bold);
        private int worldControlHeight = 0x18;
        private int worldControlWidth = 0x2d;
        public static List<WorldInfo> WorldList;
        public CustomSelfDrawPanel WorldsPanelcontrols_LoggedIn;
        public CustomSelfDrawPanel WorldsPanelcontrols_LoggedOut;

        public ProfileLoginWindow()
        {
            this.InitializeComponent();
            this.label1.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.Text = "Stronghold Kingdoms";
            this.defaultWindowTitle = this.Text;
        }

        public void AddControls()
        {
            this.pnlWorlds.Controls.Clear();
            this.pnlLogin.Controls.Clear();
            this.pnlFeedback.Controls.Clear();
            this.pnlTabs.Controls.Clear();
            this.allButtons = new List<CustomSelfDrawPanel.CSDControl>();
            this.txtEmail = new TextBox();
            this.lblEmailSteam = new CustomSelfDrawPanel.CSDLabel();
            this.lblEmail = new CustomSelfDrawPanel.CSDLabel();
            if (Program.gamersFirstInstall || Program.arcInstall)
            {
                this.lblEmail.Text = "";
            }
            else if (!Program.bigpointInstall)
            {
                this.lblEmail.Text = this.strEmailAddress;
            }
            else
            {
                this.lblEmail.Text = SK.Text("Login_BigPoint_username", "Stronghold Kingdoms Username");
            }
            this.lblEmail.Width = 300;
            this.lblEmail.Height = 0x12;
            this.txtPassword = new TextBox();
            this.lblPassword = new CustomSelfDrawPanel.CSDLabel();
            if (Program.gamersFirstInstall || Program.arcInstall)
            {
                this.lblPassword.Text = "";
            }
            else if (!Program.bigpointInstall)
            {
                this.lblPassword.Text = this.strPassword;
            }
            else
            {
                this.lblPassword.Text = SK.Text("Login_BigPoint_Password", "Your Bigpoint Password");
            }
            this.lblPassword.Width = 300;
            this.lblPassword.Height = 0x12;
            this.LoginPanelControls_LoggedOut = new CustomSelfDrawPanel();
            this.LoginPanelControls_LoggedOut.AutoScaleMode = AutoScaleMode.None;
            this.LoginPanelControls_LoggedOut.forceStyle();
            this.btnLogin = new CustomSelfDrawPanel.CSDImage();
            this.allButtons.Add(this.btnLogin);
            this.btnLogin.Image = this.LoginImage;
            this.btnLogin.Width = this.btnLogin.Image.Width;
            this.btnLogin.Height = this.btnLogin.Image.Height;
            this.btnLogin.Enabled = false;
            this.lblLoginError = new CustomSelfDrawPanel.CSDLabel();
            this.lblLoginError.Color = ARGBColors.Red;
            this.lblLoginError.Visible = false;
            this.lblLoginError.Text = "ERROR:";
            this.lblLoginError.Width = this.pnlLogin.Width;
            this.lblEmail.Position = new Point(4, this.pnlTabs.Height - 0x1d);
            this.txtEmail.Location = new Point(4, this.lblEmail.Y + this.lblEmail.Height);
            this.lblEmailSteam.Position = new Point(4, this.lblEmail.Y + this.lblEmail.Height);
            this.lblPassword.Position = new Point(4, this.txtEmail.Bottom + 2);
            this.txtPassword.Location = new Point(4, this.lblPassword.Y + this.lblPassword.Height);
            this.txtPassword.Width = this.pnlLogin.Width - 8;
            this.txtEmail.Width = this.pnlLogin.Width - 8;
            this.lblEmailSteam.Width = this.pnlLogin.Width - 8;
            this.lblEmailSteam.Height = this.lblEmail.Height;
            this.txtPassword.PasswordChar = '*';
            this.btnLogin.Position = new Point(4, this.txtPassword.Bottom + 4);
            this.lblLoginError.Position = new Point(4, this.btnLogin.Y + this.btnLogin.Height);
            this.btnLogin.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnLogin_Click), "ProfileLoginWindow_login");
            this.btnLogin.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.loginOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.loginOut));
            this.txtEmail.TextChanged += new EventHandler(this.txtLoginField_Validate_email);
            this.txtPassword.TextChanged += new EventHandler(this.txtLoginField_Validate);
            this.btnLoginFB = new CustomSelfDrawPanel.CSDButton();
            this.allButtons.Add(this.btnLoginFB);
            switch (Program.mySettings.LanguageIdent)
            {
                case "fr":
                    this.btnLoginFB.ImageNorm = (Image) GFXLibrary.facebookLogin_EN;
                    break;

                case "de":
                    this.btnLoginFB.ImageNorm = (Image) GFXLibrary.facebookLogin_DE;
                    break;

                case "ru":
                    this.btnLoginFB.ImageNorm = (Image) GFXLibrary.facebookLogin_RU;
                    break;

                case "es":
                    this.btnLoginFB.ImageNorm = (Image) GFXLibrary.facebookLogin_ES;
                    break;

                case "pl":
                    this.btnLoginFB.ImageNorm = (Image) GFXLibrary.facebookLogin_PL;
                    break;

                case "tr":
                    this.btnLoginFB.ImageNorm = (Image) GFXLibrary.facebookLogin_TR;
                    break;

                case "it":
                    this.btnLoginFB.ImageNorm = (Image) GFXLibrary.facebookLogin_IT;
                    break;

                case "pt":
                    this.btnLoginFB.ImageNorm = (Image) GFXLibrary.facebookLogin_PT;
                    break;

                default:
                    this.btnLoginFB.ImageNorm = (Image) GFXLibrary.facebookLogin_EN;
                    break;
            }
            this.btnLoginFB.OverBrighten = true;
            this.btnLoginFB.MoveOnClick = true;
            this.btnLoginFB.Enabled = false;
            this.btnLoginFB.Position = new Point((4 + this.btnLogin.Image.Width) + 4, this.txtPassword.Bottom + 4);
            this.lblLoginError.Position = new Point(4, this.btnLoginFB.Y + this.btnLoginFB.Height);
            this.btnLoginFB.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnLoginFB_Click), "ProfileLoginWindow_login");
            this.txtEmail.KeyPress += new KeyPressEventHandler(this.txtEmail_KeyPress);
            this.txtPassword.KeyPress += new KeyPressEventHandler(this.txtEmail_KeyPress);
            this.LoginPanelControls_LoggedOut.addControl(this.lblEmail);
            this.LoginPanelControls_LoggedOut.Controls.Add(this.txtEmail);
            this.LoginPanelControls_LoggedOut.addControl(this.lblEmailSteam);
            if ((Program.steamInstall && Program.steamActive) && Program.kingdomsAccountFound)
            {
                this.txtEmail.Visible = false;
                this.lblEmailSteam.Visible = true;
                this.lblEmailSteam.Text = Program.steamEmail;
                this.btnLoginFB.Visible = false;
            }
            else if ((Program.steamInstall && Program.steamActive) && !Program.kingdomsAccountFound)
            {
                this.txtEmail.Visible = false;
                this.lblEmailSteam.Visible = true;
                this.ShowCreateUserForm();
                this.btnLoginFB.Visible = false;
            }
            else if (Program.aeriaInstall || Program.bigpointInstall)
            {
                this.txtEmail.Visible = false;
                this.lblEmail.Visible = false;
                this.txtPassword.Visible = false;
                this.lblPassword.Visible = false;
                this.btnLogin.Visible = false;
                this.btnLoginFB.Visible = false;
            }
            if (Program.bigpointPartnerInstall)
            {
                this.txtEmail.Visible = false;
                this.lblEmail.Visible = false;
                this.txtPassword.Visible = false;
                this.lblPassword.Visible = false;
                this.btnLogin.Visible = true;
                this.btnLoginFB.Visible = false;
                this.btnLogin.Position = new Point(0x54, (this.txtPassword.Bottom + 4) - 30);
                this.bp2_loginMode = 0;
            }
            this.LoginPanelControls_LoggedOut.addControl(this.lblPassword);
            this.LoginPanelControls_LoggedOut.Controls.Add(this.txtPassword);
            this.LoginPanelControls_LoggedOut.addControl(this.btnLogin);
            this.LoginPanelControls_LoggedOut.addControl(this.btnLoginFB);
            this.LoginPanelControls_LoggedOut.addControl(this.lblLoginError);
            this.LoginPanelControls_LoggedOut.Size = this.pnlLogin.Size;
            this.LoginPanelControls_LoggedOut.Visible = true;
            this.pnlLogin.Controls.Add(this.LoginPanelControls_LoggedOut);
            this.LoginPanelControls_LoggedIn = new CustomSelfDrawPanel();
            this.LoginPanelControls_LoggedIn.AutoScaleMode = AutoScaleMode.None;
            this.LoginPanelControls_LoggedIn.forceStyle();
            this.LoginPanelControls_LoggedIn.Size = this.pnlLogin.Size;
            this.btnClientLogout = new CustomSelfDrawPanel.CSDImage();
            this.allButtons.Add(this.btnClientLogout);
            this.btnClientLogout.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnClientLogout_Click), "ProfileLoginWindow_logout");
            this.btnClientLogout.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.logoutOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.logoutOut));
            this.btnClientLogout.Image = this.LogoutImage;
            this.btnClientLogout.Width = this.btnClientLogout.Image.Width;
            this.btnClientLogout.Height = this.btnClientLogout.Image.Height;
            if (((!Program.steamActive && !Program.aeriaInstall) && (!Program.gamersFirstInstall && !Program.arcInstall)) && !Program.bigpointInstall)
            {
                this.LoginPanelControls_LoggedIn.addControl(this.btnClientLogout);
            }
            this.pnlLogin.Controls.Add(this.LoginPanelControls_LoggedIn);
            this.btnShieldDesigner = new CustomSelfDrawPanel.CSDImage();
            this.allButtons.Add(this.btnShieldDesigner);
            this.lblUsername = new CustomSelfDrawPanel.CSDImage();
            this.LoginPanelControls_LoggedIn.addControl(this.btnShieldDesigner);
            this.LoginPanelControls_LoggedIn.addControl(this.lblUsername);
            this.btnShieldDesigner.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.LoadShieldDesigner), "ProfileLoginWindow_shield_designer");
            this.btnClientLogout.Y = this.pnlTabs.Height - 0x1d;
            this.btnClientLogout.X = (this.LoginPanelControls_LoggedIn.Width - this.btnClientLogout.Width) - 4;
            this.lblUsername.X = 4;
            this.lblUsername.Y = this.btnClientLogout.Y;
            this.WorldsPanelcontrols_LoggedOut = new CustomSelfDrawPanel();
            this.WorldsPanelcontrols_LoggedOut.AutoScaleMode = AutoScaleMode.None;
            this.WorldsPanelcontrols_LoggedOut.forceStyle();
            this.WorldsPanelcontrols_LoggedOut.Size = this.pnlWorlds.Size;
            this.lblWorldsOfflineError = new CustomSelfDrawPanel.CSDLabel();
            this.lblWorldsOfflineError.Color = ARGBColors.Red;
            this.WorldsPanelcontrols_LoggedOut.addControl(this.lblWorldsOfflineError);
            this.WorldsPanelcontrols_LoggedOut.Visible = false;
            this.pnlWorlds.Controls.Add(this.WorldsPanelcontrols_LoggedOut);
            this.WorldsPanelcontrols_LoggedIn = new CustomSelfDrawPanel();
            this.WorldsPanelcontrols_LoggedIn.forceStyle();
            this.WorldsPanelcontrols_LoggedIn.AutoScaleMode = AutoScaleMode.None;
            this.WorldsPanelcontrols_LoggedIn.Size = this.pnlWorlds.Size;
            this.lblWorldsOnlineError = new CustomSelfDrawPanel.CSDLabel();
            this.lblWorldsOnlineError.Color = ARGBColors.Red;
            this.WorldsPanelcontrols_LoggedIn.addControl(this.lblWorldsOnlineError);
            this.WorldsPanelcontrols_LoggedIn.Visible = false;
            this.pnlWorlds.Controls.Add(this.WorldsPanelcontrols_LoggedIn);
            this.BrowserTabsControls = new CustomSelfDrawPanel();
            this.BrowserTabsControls.forceStyle();
            this.BrowserTabsControls.AutoScaleMode = AutoScaleMode.None;
            this.BrowserTabsControls.Size = this.pnlTabs.Size;
            this.pnlTabs.Controls.Add(this.BrowserTabsControls);
            this.btnExit.GotFocus += new EventHandler(this.btnExit_GotFocus);
            if (Program.mySettings.Username.Trim().Length > 0)
            {
                this.ignoreEmailChange = true;
                this.txtEmail.Text = Program.mySettings.Username;
                this.ignoreEmailChange = false;
                if (Program.steamActive)
                {
                    this.lblEmailSteam.Text = Program.steamEmail;
                    this.txtPassword.Visible = false;
                    this.lblPassword.Visible = false;
                    this.btnLogin.Visible = false;
                    this.btnLoginFB.Visible = false;
                }
                this.txtPassword.Focus();
            }
            else
            {
                this.txtEmail.Focus();
            }
            this.LoginPanelControls_Feedback = new CustomSelfDrawPanel();
            this.LoginPanelControls_Feedback.forceStyle();
            this.LoginPanelControls_Feedback.Location = new Point(0, 0);
            this.LoginPanelControls_Feedback.AutoScaleMode = AutoScaleMode.None;
            this.LoginPanelControls_Feedback.Size = this.pnlFeedback.Size;
            this.feedbackProgressArea = new CustomSelfDrawPanel.CSDArea();
            this.feedbackProgressArea.Size = this.LoginPanelControls_Feedback.Size;
            this.LoginPanelControls_Feedback.addControl(this.feedbackProgressArea);
            this.feedbackProgress = new CustomSelfDrawPanel.CSDFill();
            this.feedbackProgress.FillColor = Color.FromArgb(0xff, 0xb6, 0);
            this.feedbackProgress.Size = new Size(0, this.pnlFeedback.Height);
            this.feedbackProgressArea.addControl(this.feedbackProgress);
            this.feedbackLine = new CustomSelfDrawPanel.CSDLine();
            this.feedbackLine.Position = new Point(0, 0);
            this.feedbackLine.Size = new Size(this.pnlFeedback.Width, 0);
            this.feedbackLine.LineColor = ARGBColors.Black;
            this.feedbackProgressArea.addControl(this.feedbackLine);
            this.exitButton = new CustomSelfDrawPanel.CSDImage();
            this.allButtons.Add(this.exitButton);
            this.exitButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnExit_Click), "ProfileLoginWindow_exit");
            this.exitButton.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.exitOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.exitOut));
            this.exitButton.Image = this.ExitImage;
            this.exitButton.Width = this.exitButton.Image.Width;
            this.exitButton.Height = this.exitButton.Image.Height;
            this.exitButton.Position = new Point(0x337, 5);
            this.feedbackProgressArea.addControl(this.exitButton);
            this.cancelButton = new CustomSelfDrawPanel.CSDImage();
            this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelClick), "ProfileLoginWindow_cancel");
            this.cancelButton.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.cancelOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.cancelOut));
            this.cancelButton.Image = this.CancelImage;
            this.cancelButton.Width = this.cancelButton.Image.Width;
            this.cancelButton.Height = this.cancelButton.Image.Height;
            this.cancelButton.Position = new Point(4, 5);
            this.cancelButton.Visible = false;
            this.feedbackProgressArea.addControl(this.cancelButton);
            this.lblRetrieving = new CustomSelfDrawPanel.CSDLabel();
            this.lblRetrieving.Text = "";
            this.lblRetrieving.Position = new Point(0x70, 10);
            this.lblRetrieving.Size = new Size(600, 20);
            this.lblRetrieving.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblRetrieving.Color = ARGBColors.Black;
            this.lblRetrieving.Visible = false;
            this.Text = this.defaultWindowTitle;
            this.feedbackProgressArea.addControl(this.lblRetrieving);
            this.lblVersion = new CustomSelfDrawPanel.CSDLabel();
            this.lblVersion.Text = "";
            this.lblVersion.Position = new Point(640, 10);
            this.lblVersion.Size = new Size(0xa8, 20);
            this.lblVersion.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.lblVersion.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblVersion.Color = ARGBColors.Black;
            this.lblVersion.Visible = false;
            this.feedbackProgressArea.addControl(this.lblVersion);
            this.tandcLabel = new CustomSelfDrawPanel.CSDLabel();
            this.tandcLabel.Text = SK.Text("MENU_TandC", "Terms & Conditions").Replace("&amp;", "&");
            this.tandcLabel.Size = new Size(270, 15);
            this.tandcLabel.Position = new Point(40, 10);
            this.tandcLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.tandcLabel.Color = ARGBColors.Black;
            this.tandcLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tcClicked));
            this.tandcLabel.setMouseOverDelegate(() => this.tandcLabel.Color = ARGBColors.Red, () => this.tandcLabel.Color = ARGBColors.Black);
            this.tandcLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.tandcLabel.Visible = true;
            this.feedbackProgressArea.addControl(this.tandcLabel);
            this.gameRulesLabel = new CustomSelfDrawPanel.CSDLabel();
            this.gameRulesLabel.Text = SK.Text("MENU_Game_Rules", "Game Rules");
            this.gameRulesLabel.Size = new Size(300, 15);
            if (Program.mySettings.languageIdent == "de")
            {
                this.gameRulesLabel.Position = new Point(0xfd, 10);
            }
            else
            {
                this.gameRulesLabel.Position = new Point(0xcd, 10);
            }
            this.gameRulesLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.gameRulesLabel.Color = ARGBColors.Black;
            this.gameRulesLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.gameRulesClicked));
            this.gameRulesLabel.setMouseOverDelegate(() => this.gameRulesLabel.Color = ARGBColors.Red, () => this.gameRulesLabel.Color = ARGBColors.Black);
            this.gameRulesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.gameRulesLabel.Visible = true;
            this.feedbackProgressArea.addControl(this.gameRulesLabel);
            this.forumLabel = new CustomSelfDrawPanel.CSDLabel();
            this.forumLabel.Text = SK.Text("MENU_Forum", "Forum");
            this.forumLabel.Size = new Size(300, 15);
            this.forumLabel.Position = new Point(370, 10);
            this.forumLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.forumLabel.Color = ARGBColors.Black;
            this.forumLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumClicked));
            this.forumLabel.setMouseOverDelegate(() => this.forumLabel.Color = ARGBColors.Red, () => this.forumLabel.Color = ARGBColors.Black);
            this.forumLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            if (Program.bigpointInstall || Program.bigpointPartnerInstall)
            {
                this.forumLabel.Visible = false;
            }
            else
            {
                this.forumLabel.Visible = true;
            }
            this.feedbackProgressArea.addControl(this.forumLabel);
            this.supportLabel = new CustomSelfDrawPanel.CSDLabel();
            this.supportLabel.Text = SK.Text("MENU_Support", "Support");
            this.supportLabel.Size = new Size(100, 15);
            this.supportLabel.Position = new Point(0x217, 10);
            this.supportLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.supportLabel.Color = ARGBColors.Black;
            this.supportLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.supportClicked));
            this.supportLabel.setMouseOverDelegate(() => this.supportLabel.Color = ARGBColors.Red, () => this.supportLabel.Color = ARGBColors.Black);
            this.supportLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.supportLabel.Visible = true;
            this.feedbackProgressArea.addControl(this.supportLabel);
            this.pnlFeedback.Controls.Add(this.LoginPanelControls_Feedback);
            this.GreyoutWorlds = this.MakeGreyoutImage(this.pnlWorlds);
            this.GreyoutLogin = this.MakeGreyoutImage(this.pnlLogin);
            this.GreyoutTabs = this.MakeGreyoutImage(this.pnlFeedback);
            if (Program.steamActive)
            {
                this.lblEmail.Visible = false;
                this.lblPassword.Visible = false;
                this.txtPassword.Visible = false;
            }
            if ((Program.steamInstall && Program.steamActive) && (Program.kingdomsAccountFound && !successfulAutoLogin))
            {
                this.btnLogin_Click();
            }
            if (Program.gamersFirstInstall || Program.arcInstall)
            {
                this.lblEmail.Visible = false;
                this.lblPassword.Visible = false;
                this.txtPassword.Visible = false;
                this.txtEmail.Visible = false;
                this.btnLogin.Visible = false;
                this.btnLoginFB.Visible = false;
                Program.mySettings.AutoLogin = false;
                if (!successfulAutoLogin)
                {
                    this.btnLogin_Click();
                }
            }
        }

        public void aeriaClose()
        {
            this.btnExit_Click();
        }

        public void aeriaLogin(string userGUID, string sessionGUID)
        {
            this.txtEmail.Text = userGUID;
            this.txtPassword.Text = sessionGUID;
            this.btnLogin_Click();
        }

        private void autoLoadToggled()
        {
            Program.mySettings.AutoLogin = this.chkAutoLogin.Checked;
        }

        public void bigpointClose()
        {
            this.btnExit_Click();
        }

        public void bigpointLogin(string userGUID, string sessionGUID)
        {
            this.txtEmail.Text = userGUID;
            this.txtPassword.Text = sessionGUID;
            this.btnLogin_Click();
        }

        public void bp2_autoLoginAttempt()
        {
            this.bp2_loginMode = 1;
            this.btnLogin_Click();
        }

        public void BP2_Closed()
        {
            this.btnLogin.Enabled = true;
            this.bp2_loginMode = 0;
            this.EnablePanels(true);
        }

        public void bp2_manualLoginAttempt()
        {
            this.bp2_loginMode = 2;
            this.btnLogin_Click();
        }

        private void btnAccountDetails_Click()
        {
            this.EnablePanels(false);
            Dictionary<string, string> postVars = new Dictionary<string, string>();
            postVars.Add("UserGUID", RemoteServices.Instance.UserGuid.ToString().Replace("-", ""));
            postVars.Add("ClientLanguage", Program.mySettings.LanguageIdent.ToLower());
            postVars.Add("NewLoginScreen", "1");
            postVars.Add("SessionGUID", RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""));
            this.geckoWebBrowser1.Navigate(new Uri(URLs.AccountMainPage), postVars);
        }

        private void btnBeginnersGuide_Click(object sender, EventArgs e)
        {
            new Process { StartInfo = { FileName = URLs.TutorialPage } }.Start();
        }

        private void btnClientLogout_Click()
        {
            if (Program.bigpointPartnerInstall)
            {
                this.forumLabel.Visible = false;
                if ((bp2_logoutURL != null) && (bp2_logoutURL.Length > 0))
                {
                    Process.Start(bp2_logoutURL);
                }
                RemoteServices.Instance.UserGuid = Guid.Empty;
                RemoteServices.Instance.SessionGuid = Guid.Empty;
                this.AdminGUID = null;
                this.bp2_loginMode = 0;
                this.PlayerGameworldCount = 0;
                this.GetOfflineWorlds();
                this.RefreshControls();
                this.geckoWebBrowser1.Navigate(URLs.NewsMainPage);
                this.EnablePanels(true);
            }
            else
            {
                XmlRpcAuthProvider provider = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
                XmlRpcAuthRequest req = new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), "", "", "");
                this.clearErrorMessages();
                provider.clientLogout(req, new AuthEndResponseDelegate(this.ClientLogoutCallback), this);
                this.EnablePanels(false);
                this.geckoWebBrowser1.Enabled = false;
                if (LoggedInViaFacebook)
                {
                    if (tempBrowser == null)
                    {
                        tempBrowser = new WebBrowser();
                        tempBrowser.Location = new Point(0x3e8, 0x3e8);
                    }
                    tempBrowser.Navigate("http://login.strongholdkingdoms.com/facebook/logout.php?access_token=" + Program.mySettings.facebookaccesstoken);
                    Thread.Sleep(0x3e8);
                    Program.mySettings.facebookaccesstoken = "";
                }
                LoggedInViaFacebook = false;
                if (Program.aeriaInstall)
                {
                    this.openAeriaPopup(true);
                }
                if (Program.bigpointInstall)
                {
                    this.openBigPointPopup(true);
                }
                bool bigpointPartnerInstall = Program.bigpointPartnerInstall;
            }
        }

        private void btnCreateAccount_Click()
        {
            if ((!Program.mySettings.hasLoggedIn() && !Program.bigpointInstall) && (!Program.aeriaInstall && !Program.bigpointPartnerInstall))
            {
                this.ShowCreateUserForm();
            }
            else if (Program.mySettings.LanguageIdent == "de")
            {
                new Process { StartInfo = { FileName = "http://de.strongholdkingdoms.com/" } }.Start();
            }
            else if (Program.mySettings.LanguageIdent == "fr")
            {
                new Process { StartInfo = { FileName = "http://fr.strongholdkingdoms.com/" } }.Start();
            }
            else if (Program.mySettings.LanguageIdent == "ru")
            {
                new Process { StartInfo = { FileName = "http://ru.strongholdkingdoms.com" } }.Start();
            }
            else if (Program.mySettings.LanguageIdent == "es")
            {
                new Process { StartInfo = { FileName = "http://es.strongholdkingdoms.com" } }.Start();
            }
            else if (Program.mySettings.LanguageIdent == "pl")
            {
                new Process { StartInfo = { FileName = "http://pl.strongholdkingdoms.com" } }.Start();
            }
            else if (Program.mySettings.LanguageIdent == "tr")
            {
                new Process { StartInfo = { FileName = "http://tr.strongholdkingdoms.com" } }.Start();
            }
            else if (Program.mySettings.LanguageIdent == "it")
            {
                new Process { StartInfo = { FileName = "http://it.strongholdkingdoms.com" } }.Start();
            }
            else if (Program.mySettings.LanguageIdent == "pt")
            {
                new Process { StartInfo = { FileName = "http://pt.strongholdkingdoms.com" } }.Start();
            }
            else
            {
                new Process { StartInfo = { FileName = "http://www.strongholdkingdoms.com" } }.Start();
            }
        }

        public void btnExit_Click()
        {
            RemoteServices.Instance.UserID = -1;
            this.selfClosing = true;
            base.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            RemoteServices.Instance.UserID = -1;
            this.selfClosing = true;
            base.Close();
        }

        private void btnExit_GotFocus(object sender, EventArgs e)
        {
            this.txtPassword.Focus();
        }

        private void btnForgotten_Click()
        {
            this.EnablePanels(false);
            this.geckoWebBrowser1.Navigate(URLs.ForgottenPasswordLink + "?lang=" + Program.mySettings.LanguageIdent.ToLower());
        }

        private void btnForgottenPassword_Click(object sender, EventArgs e)
        {
            new Process { StartInfo = { FileName = URLs.ForgottenPasswordPage } }.Start();
        }

        public void btnLogin_Click()
        {
            if (Program.bigpointPartnerInstall && (this.bp2_loginMode == 0))
            {
                bp2_currentGuid = Guid.NewGuid().ToString().Replace("-", "");
                Process.Start("https://api.bigpoint.com/oauth/authorize?response_type=code&client_id=strongholdkingdoms&redirect_uri=http://login.strongholdkingdoms.com/bigpoint/2/oauth2/authorized.php&state=" + bp2_currentGuid);
                InterfaceMgr.Instance.openBPPopupWindow(this);
                this.btnLogin.Enabled = false;
            }
            else
            {
                XmlRpcAuthRequest request;
                LoggedInViaFacebook = false;
                XmlRpcAuthProvider provider = null;
                // TODO: Исправить проблему с делегатом
                if (false) // (Control.ModifierKeys != Keys.Shift)
                {
                    if (!certPolicyCreated)
                    {
                        // ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback) Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, (sender, certificate, chain, sslPolicyErrors) => true);
                        certPolicyCreated = true;
                    }
                    provider = XmlRpcAuthProvider.CreateForEndpoint("https", URLs.ProfileServerAddressLogin, "443", URLs.ProfilePath);
                }
                else
                {
                    provider = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
                }
                if (Program.arcInstall)
                {
                    request = new XmlRpcAuthRequest("", Program.arcUsername, this.txtEmail.Text, this.txtPassword.Text, "", "", "", "");
                }
                else if (!Program.kingdomsAccountFound || !Program.steamActive)
                {
                    request = new XmlRpcAuthRequest("", this.txtEmail.Text, this.txtEmail.Text, this.txtPassword.Text, "", "", "", "");
                }
                else
                {
                    request = new XmlRpcAuthRequest("", Program.steamEmail, Program.steamEmail, this.txtPassword.Text, "", "", "", "");
                    Program.Steam_getTicket();
                    string str = BitConverter.ToString(Program.steam_SessionTicket).Replace("-", "");
                    request.SteamID = str;
                }
                try
                {
                    foreach (Process process in Process.GetProcesses())
                    {
                        if (Rot13.Transform(process.ProcessName.ToLowerInvariant()).Contains("fxfgrjneq"))
                        {
                            request.OrderID = request.OrderID + "s";
                        }
                        if (Rot13.Transform(process.ProcessName.ToLowerInvariant()).Contains("znpebk"))
                        {
                            request.OrderID = request.OrderID + "m";
                        }
                    }
                }
                catch (Exception)
                {
                }
                if (Program.gamersFirstInstall)
                {
                    request.SteamID = "gamersfirst";
                    request.Password = Program.gamersFirstTokenMD5;
                }
                if (Program.arcInstall)
                {
                    request.SteamID = "arc";
                    if (Program.arcToken.Length < 5)
                    {
                        Thread.Sleep(0x7d0);
                        Program.arcToken = Program.getNewArcToken();
                    }
                    AeriaToken = Program.arcToken;
                }
                if (Program.bigpointInstall)
                {
                    request.SteamID = "bp";
                }
                if (this.tempFacebookLogin)
                {
                    this.FacebookToken = AeriaToken;
                    request.SteamID = "fb";
                }
                else
                {
                    this.FacebookToken = "";
                    this.specialFacebookLogin = false;
                }
                if (Program.bigpointPartnerInstall)
                {
                    request.SteamID = "bp2";
                    AeriaToken = bp2_currentGuid;
                }
                request.AeriaToken = AeriaToken;
                request.Platform = this.GetMacAddress();
                this.clearErrorMessages();
                this.EnablePanels(false);
                provider.clientLogin(request, new AuthEndResponseDelegate(this.ClientLoginCallback), this);
            }
        }

        public void btnLoginFB_Click()
        {
            if (Program.mySettings.facebookaccesstoken != "")
            {
                this.specialFacebookLogin = true;
                this.tempFacebookLogin = true;
                AeriaToken = Program.mySettings.facebookaccesstoken;
                this.btnLogin_Click();
                this.tempFacebookLogin = false;
            }
            else
            {
                this.openFacebookPopup();
            }
        }

        private void btnNews_Click()
        {
            this.EnablePanels(false);
            this.geckoWebBrowser1.Navigate(URLs.NewsMainPage);
        }

        private void btnWorldAction_Click()
        {
            WorldInfo tag = (WorldInfo) this.WorldsPanelcontrols_LoggedIn.ClickedControl.Tag;
            if (tag.Online)
            {
                this.serverAddr = tag.HostExt;
                Program.WorldName = getWorldShortDesc(tag);
                RemoteServices.Instance.ProfileWorldID = tag.KingdomsWorldID;
                this.EnablePanels(false);
                this.JoinGameworld(new int?(tag.Playing ? 1 : 0), new int?((this.PlayerGameworldCount > 0) ? 0 : 1), new int?(tag.KingdomsWorldID), getWorldShortDesc(tag));
            }
        }

        public void btnWorldAction_Click(WorldInfo i)
        {
            if (i.Online)
            {
                this.serverAddr = i.HostExt;
                Program.WorldName = getWorldShortDesc(i);
                RemoteServices.Instance.ProfileWorldID = i.KingdomsWorldID;
                this.EnablePanels(false);
                this.JoinGameworld(new int?(i.Playing ? 1 : 0), new int?((this.PlayerGameworldCount > 0) ? 0 : 1), new int?(i.KingdomsWorldID), getWorldShortDesc(i));
            }
        }

        private void btnWorldAction_mouseOut()
        {
            WorldInfo tag = (WorldInfo) this.WorldsPanelcontrols_LoggedIn.OverControl.Tag;
            if (tag.Playing)
            {
                ((CustomSelfDrawPanel.CSDImage) this.WorldsPanelcontrols_LoggedIn.OverControl).Image = this.PlayImage;
            }
            else
            {
                ((CustomSelfDrawPanel.CSDImage) this.WorldsPanelcontrols_LoggedIn.OverControl).Image = this.JoinImage;
            }
        }

        private void btnWorldAction_mouseOver()
        {
            WorldInfo tag = (WorldInfo) this.WorldsPanelcontrols_LoggedIn.OverControl.Tag;
            if (tag.Playing)
            {
                ((CustomSelfDrawPanel.CSDImage) this.WorldsPanelcontrols_LoggedIn.OverControl).Image = this.PlayImageOver;
            }
            else
            {
                ((CustomSelfDrawPanel.CSDImage) this.WorldsPanelcontrols_LoggedIn.OverControl).Image = this.JoinImageOver;
            }
        }

        public void BuildOfflineWorldList(List<WorldInfo> list)
        {
            if ((this.loggedOutWorldControls != null) && (this.loggedOutWorldControls.Count > 0))
            {
                foreach (CustomSelfDrawPanel.CSDControl control in this.loggedOutWorldControls)
                {
                    this.WorldsPanelcontrols_LoggedOut.removeControl(control);
                }
                this.loggedOutWorldControls.Clear();
            }
            else if (this.loggedOutWorldControls == null)
            {
                this.loggedOutWorldControls = new List<CustomSelfDrawPanel.CSDControl>();
            }
            int num = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Online)
                {
                    num++;
                }
            }
            string str = "";
            Color green = ARGBColors.Green;
            if (num == 0)
            {
                str = SK.Text("LOGIN_ALL_OFFLINE", "All Worlds Offline");
                green = ARGBColors.Red;
            }
            else if (num == list.Count)
            {
                str = SK.Text("LOGIN_ALL_ONLINE", "All Worlds Online");
            }
            else
            {
                str = SK.Text("LOGIN_WORLDS_ONLINE", "Worlds Online : ") + num.ToString() + " / " + list.Count.ToString();
                green = ARGBColors.Black;
            }
            CustomSelfDrawPanel.CSDLabel item = new CustomSelfDrawPanel.CSDLabel {
                Text = str,
                Color = green,
                Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold),
                Position = new Point(0, 0),
                Size = new Size(this.WorldsPanelcontrols_LoggedOut.Width, 80),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER
            };
            this.loggedOutWorldControls.Add(item);
            CustomSelfDrawPanel.CSDLabel statusLinkLabel = new CustomSelfDrawPanel.CSDLabel {
                Text = SK.Text("LOGIN_WORLDS_STATUS_PAGE", "Live Status Webpage"),
                Color = ARGBColors.Black,
                Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular),
                Position = new Point(0, 20),
                Size = new Size(this.WorldsPanelcontrols_LoggedOut.Width, 0x19),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER
            };
            statusLinkLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.statusPageClicked));
            statusLinkLabel.setMouseOverDelegate(() => statusLinkLabel.Color = ARGBColors.Red, () => statusLinkLabel.Color = ARGBColors.Black);
            this.loggedOutWorldControls.Add(statusLinkLabel);
            foreach (CustomSelfDrawPanel.CSDControl control2 in this.loggedOutWorldControls)
            {
                this.WorldsPanelcontrols_LoggedOut.addControl(control2);
            }
            this.WorldsPanelcontrols_LoggedOut.Invalidate(true);
        }

        public void BuildOnlineWorldList(List<WorldInfo> list)
        {
            if ((this.loggedInWorldControls != null) && (this.loggedInWorldControls.Count > 0))
            {
                foreach (CustomSelfDrawPanel.CSDControl control in this.loggedInWorldControls)
                {
                    this.WorldsPanelcontrols_LoggedIn.removeControl(control);
                }
                this.loggedInWorldControls.Clear();
            }
            else if (this.loggedInWorldControls == null)
            {
                this.loggedInWorldControls = new List<CustomSelfDrawPanel.CSDControl>();
            }
            int lastWorldID = Program.mySettings.LastWorldID;
            string str = SK.Text("LOGIN_LastWorld", "Last World Played");
            int num2 = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Playing)
                {
                    num2++;
                }
            }
            int num4 = -1;
            if (num2 == 0)
            {
                if (this.chkAutoLogin != null)
                {
                    this.chkAutoLogin.Checked = false;
                }
                Program.mySettings.AutoLogin = false;
                str = SK.Text("LOGIN_Recommended_Server", "Recommended World");
                bool flag = false;
                int num5 = 0;
                int kingdomsWorldID = lastWorldID;
                for (int k = 0; k < list.Count; k++)
                {
                    if (list[k].Supportculture == Program.mySettings.LanguageIdent)
                    {
                        flag = true;
                    }
                    if (list[k].NewWorld)
                    {
                        num5++;
                        kingdomsWorldID = list[k].KingdomsWorldID;
                        if (((list[k].KingdomsWorldID >= 700) && (list[k].KingdomsWorldID < 0x31f)) || ((list[k].KingdomsWorldID >= 0x4b0) && (list[k].KingdomsWorldID < 0x513)))
                        {
                            num4 = list[k].KingdomsWorldID;
                        }
                    }
                }
                if (num5 == 1)
                {
                    lastWorldID = kingdomsWorldID;
                }
                else
                {
                    bool flag2 = false;
                    string languageIdent = Program.mySettings.LanguageIdent;
                    if (!flag)
                    {
                        languageIdent = "en";
                    }
                    bool flag3 = false;
                    int num8 = -1;
                    if (languageIdent == "en")
                    {
                        uint systemDefaultLangID = 0;
                        try
                        {
                            systemDefaultLangID = GetSystemDefaultLangID();
                        }
                        catch (Exception)
                        {
                        }
                        if (systemDefaultLangID == 0x409)
                        {
                            for (int m = 0; m < list.Count; m++)
                            {
                                if ((((list[m].Supportculture == languageIdent) && (list[m].KingdomsWorldID >= 900)) && ((list[m].KingdomsWorldID < 0x3e8) && list[m].NewWorld)) && (list[m].Online && list[m].AvailableToJoin))
                                {
                                    flag3 = true;
                                    lastWorldID = list[m].KingdomsWorldID;
                                    flag2 = true;
                                    break;
                                }
                            }
                        }
                    }
                    else if (!(languageIdent == "es"))
                    {
                        if (languageIdent == "pt")
                        {
                            uint num13 = 0;
                            try
                            {
                                num13 = GetSystemDefaultLangID();
                            }
                            catch (Exception)
                            {
                            }
                            if (num13 == 0x816)
                            {
                                languageIdent = "es";
                            }
                            else
                            {
                                bool flag4 = false;
                                for (int n = 0; n < list.Count; n++)
                                {
                                    if (list[n].Supportculture == "pt")
                                    {
                                        flag4 = true;
                                        break;
                                    }
                                }
                                if (!flag4)
                                {
                                    languageIdent = "es";
                                }
                            }
                        }
                    }
                    else
                    {
                        uint num11 = 0;
                        try
                        {
                            num11 = GetSystemDefaultLangID();
                        }
                        catch (Exception)
                        {
                        }
                        switch (num11)
                        {
                            case 0x140a:
                            case 0x180a:
                            case 0x80a:
                            case 0x100a:
                            case 0x1c0a:
                            case 0x200a:
                            case 0x240a:
                            case 0x280a:
                            case 0x2c0a:
                            case 0x380a:
                            case 0x3c0a:
                            case 0x400a:
                            case 0x300a:
                            case 0x340a:
                            case 0x4c0a:
                            case 0x500a:
                            case 0x540a:
                            case 0x440a:
                            case 0x480a:
                                for (int num12 = 0; num12 < list.Count; num12++)
                                {
                                    if (list[num12].Supportculture == "pt")
                                    {
                                        languageIdent = "pt";
                                        break;
                                    }
                                }
                                break;
                        }
                    }
                    if (!flag3)
                    {
                        bool flag5 = false;
                        for (int num15 = 0; num15 < list.Count; num15++)
                        {
                            if ((list[num15].Supportculture == languageIdent) && ((languageIdent != "en") || (list[num15].KingdomsWorldID < 200)))
                            {
                                if (((list[num15].KingdomsWorldID > num8) && (list[num15].Online || !flag2)) && list[num15].AvailableToJoin)
                                {
                                    lastWorldID = list[num15].KingdomsWorldID;
                                    num8 = list[num15].KingdomsWorldID;
                                    if (list[num15].Online)
                                    {
                                        flag2 = true;
                                    }
                                }
                                if ((list[num15].NewWorld && list[num15].Online) && list[num15].AvailableToJoin)
                                {
                                    lastWorldID = list[num15].KingdomsWorldID;
                                    flag2 = true;
                                    flag5 = true;
                                    break;
                                }
                            }
                        }
                        if (!flag5 && (num4 >= 0))
                        {
                            lastWorldID = num4;
                        }
                    }
                }
            }
            LastNumberOfWorldsPlaying = num2;
            DateTime time = new DateTime(0x7de, 2, 20, 0, 0, 0);
            CustomSelfDrawPanel.CSDLabel statusLinkLabel = new CustomSelfDrawPanel.CSDLabel {
                Text = SK.Text("LOGIN_WORLDS_STATUS_PAGE", "Live Status Webpage"),
                Color = ARGBColors.Black,
                Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular),
                Position = new Point(0, 0xaf),
                Size = new Size(this.WorldsPanelcontrols_LoggedOut.Width, 0x19),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER
            };
            statusLinkLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.statusPageClicked));
            statusLinkLabel.setMouseOverDelegate(() => statusLinkLabel.Color = ARGBColors.Red, () => statusLinkLabel.Color = ARGBColors.Black);
            this.loggedInWorldControls.Add(statusLinkLabel);
            for (int j = 0; j < list.Count; j++)
            {
                if ((list[j].KingdomsWorldID != lastWorldID) || ((num2 != 0) && !list[j].Playing))
                {
                    continue;
                }
                CustomSelfDrawPanel.CSDLabel label = new CustomSelfDrawPanel.CSDLabel();
                CustomSelfDrawPanel.CSDImage image = new CustomSelfDrawPanel.CSDImage();
                CustomSelfDrawPanel.CSDImage image2 = new CustomSelfDrawPanel.CSDImage();
                CustomSelfDrawPanel.CSDLabel label2 = new CustomSelfDrawPanel.CSDLabel();
                CustomSelfDrawPanel.CSDLabel label3 = new CustomSelfDrawPanel.CSDLabel {
                    Text = str,
                    Position = new Point(0, 10),
                    Size = new Size(this.WorldsPanelcontrols_LoggedIn.Width, 60),
                    Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular),
                    Color = ARGBColors.Black,
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER
                };
                this.loggedInWorldControls.Add(label3);
                int num17 = 70;
                image.Y = num17;
                image2.Y = num17;
                label.Y = num17;
                label2.Y = num17;
                label.Width = (this.worldControlWidth + 8) + 8;
                label.Height = this.worldControlHeight;
                label2.Width = this.worldControlWidth - 8;
                label2.Height = this.worldControlHeight;
                switch (list[j].Supportculture)
                {
                    case "en":
                        image.CustomTooltipID = 0xfa1;
                        break;

                    case "de":
                        image.CustomTooltipID = 0xfa2;
                        break;

                    case "fr":
                        image.CustomTooltipID = 0xfa3;
                        break;

                    case "ru":
                        image.CustomTooltipID = 0xfa4;
                        break;

                    case "es":
                        image.CustomTooltipID = 0xfb0;
                        break;

                    case "pl":
                        image.CustomTooltipID = 0xfb4;
                        break;

                    case "tr":
                        image.CustomTooltipID = 0xfb7;
                        break;

                    case "it":
                        image.CustomTooltipID = 0xfbb;
                        break;

                    case "pt":
                        image.CustomTooltipID = 0xfc3;
                        break;

                    case "eu":
                        image.CustomTooltipID = 0xfbf;
                        break;
                }
                switch (list[j].MapCulture)
                {
                    case "en":
                        image2.CustomTooltipID = 0xfa5;
                        break;

                    case "de":
                        image2.CustomTooltipID = 0xfa6;
                        break;

                    case "fr":
                        image2.CustomTooltipID = 0xfa7;
                        break;

                    case "ru":
                        image2.CustomTooltipID = 0xfa8;
                        break;

                    case "es":
                        image2.CustomTooltipID = 0xfb1;
                        break;

                    case "pl":
                        image2.CustomTooltipID = 0xfb5;
                        break;

                    case "tr":
                        image2.CustomTooltipID = 0xfb8;
                        break;

                    case "it":
                        image2.CustomTooltipID = 0xfbc;
                        break;

                    case "us":
                        image2.CustomTooltipID = 0xfbe;
                        break;

                    case "eu":
                        image2.CustomTooltipID = 0xfc0;
                        break;

                    case "pt":
                        image2.CustomTooltipID = 0xfc4;
                        break;
                }
                label.Text = getWorldShortDesc(list[j]);
                image.Image = (Image) GFXLibrary.getLoginWorldFlag(list[j].Supportculture);
                image.Width = image.Image.Width;
                image.Height = image.Image.Height;
                image2.Image = (Image) GFXLibrary.getLoginWorldMap(list[j].MapCulture);
                image2.Width = image2.Image.Width;
                image2.Height = image2.Image.Height;
                label.X = 3;
                image.X = (label.X + label.Width) + 8;
                image2.X = (image.X + image.Width) + 8;
                label2.X = (image2.X + image2.Width) + 8;
                if (list[j].Online)
                {
                    label2.Text = this.strOnline;
                    label2.Color = ARGBColors.Green;
                    CustomSelfDrawPanel.CSDImage image3 = new CustomSelfDrawPanel.CSDImage();
                    this.allButtons.Add(image3);
                    image3.Width = this.worldControlWidth;
                    image3.Height = this.worldControlHeight;
                    image3.Y = num17;
                    image3.Tag = list[j];
                    image3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnWorldAction_Click), "ProfileLoginWindow_enter_world");
                    image3.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.btnWorldAction_mouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.btnWorldAction_mouseOut));
                    if (list[j].Playing)
                    {
                        image3.Image = this.PlayImage;
                    }
                    else if (list[j].AvailableToJoin)
                    {
                        image3.Image = this.JoinImage;
                    }
                    else
                    {
                        image3.Image = this.ClosedImage;
                        image3.setClickDelegate(null);
                        image3.setMouseOverDelegate(null, null);
                    }
                    image3.Width = image3.Image.Width;
                    image3.Height = image3.Image.Height;
                    image3.X = (this.pnlWorlds.Width - 4) - image3.Width;
                    this.loggedInWorldControls.Add(image3);
                    label2.CustomTooltipID = 0xfaa;
                }
                else
                {
                    if ((list[j].KingdomsWorldID == 0x9c4) && (DateTime.UtcNow > time))
                    {
                        label2.Text = this.strWorldEnded;
                        label2.Width = 0x80;
                    }
                    else
                    {
                        label2.Text = this.strOffline;
                        label2.Color = ARGBColors.Red;
                    }
                    label2.CustomTooltipID = 0xfa9;
                }
                this.loggedInWorldControls.Add(image);
                this.loggedInWorldControls.Add(image2);
                this.loggedInWorldControls.Add(label);
                this.loggedInWorldControls.Add(label2);
                break;
            }
            CustomSelfDrawPanel.CSDButton item = new CustomSelfDrawPanel.CSDButton {
                ImageNorm = this.SelectImage,
                ImageOver = this.SelectImageOver,
                Position = new Point(0x17, 120)
            };
            item.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ShowWorldSelect), "ProfileLoginWindow_show_worlds");
            this.loggedInWorldControls.Add(item);
            if (NewWorldsAvailable)
            {
                CustomSelfDrawPanel.CSDLabel label4 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LOGIN_New_Worlds", "A New World is available!"),
                    Color = ARGBColors.Green,
                    Position = new Point(0, 0x9b),
                    Size = new Size(this.WorldsPanelcontrols_LoggedIn.Width, 60),
                    Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER
                };
                this.loggedInWorldControls.Add(label4);
            }
            foreach (CustomSelfDrawPanel.CSDControl control2 in this.loggedInWorldControls)
            {
                this.WorldsPanelcontrols_LoggedIn.addControl(control2);
            }
            this.WorldsPanelcontrols_LoggedIn.Invalidate(true);
        }

        private void cancelClick()
        {
            this.Cursor = Cursors.Default;
            this.connectingCancelled = true;
            GameEngine.Instance.forceRelogin();
            GameEngine.Instance.sessionExpired(-1);
            this.feedbackProgressArea.invalidate();
            while (GameEngine.Instance.World.isWorkerThreadAlive())
            {
                Thread.Sleep(200);
                Program.DoEvents();
            }
        }

        public void cancelOut()
        {
            this.cancelButton.Image = this.CancelImage;
        }

        public void cancelOver()
        {
            this.cancelButton.Image = this.CancelImageOver;
        }

        public void cancelVacationMode()
        {
            XmlRpcCardsRequest req = new XmlRpcCardsRequest {
                UserGUID = RemoteServices.Instance.UserGuid.ToString().Replace("-", ""),
                SessionGUID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "")
            };
            XmlRpcCardsResponse response = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, "/services/cardserver.php").cancelVacation(req, null, null, 0x7530);
            if (response.SuccessCode.HasValue && (response.SuccessCode == 1))
            {
                InterfaceMgr.Instance.closeVacationCancelPopupWindow();
            }
        }

        public void clearErrorMessages()
        {
            this.lblLoginError.Visible = false;
        }

        private void ClientLoginCallback(IAuthProvider sender, IAuthResponse response)
        {
            if (response.SuccessCode != 1)
            {
                if (Program.bigpointPartnerInstall)
                {
                    if (this.bp2_loginMode == 1)
                    {
                        BPPopupWindow window = InterfaceMgr.Instance.getBPPopupWindow();
                        if (window != null)
                        {
                            window.attempt1Failed();
                            this.EnablePanels(true);
                        }
                        return;
                    }
                    if (this.bp2_loginMode == 2)
                    {
                        this.bp2_loginMode = 0;
                        this.btnLogin.Enabled = true;
                        InterfaceMgr.Instance.closeBPPopupWindow();
                    }
                }
                if (this.FacebookToken == "")
                {
                    GameEngine.Instance.playInterfaceSound("ProfileLoginWindow_login_failed");
                    if ((response.Message.ToLower() == "the provided password is incorrect.") || (response.Message.ToLower() == "the specified user doesn't exist."))
                    {
                        this.throwClientError(this.lblLoginError, response.Message);
                    }
                    else
                    {
                        this.throwClientErrorConnection(this.lblLoginError, response.Message);
                    }
                    if (Program.aeriaInstall)
                    {
                        this.openAeriaPopup();
                    }
                    if (Program.bigpointInstall)
                    {
                        this.openBigPointPopup();
                    }
                }
                else
                {
                    Program.mySettings.facebookaccesstoken = "";
                    if (this.specialFacebookLogin)
                    {
                        this.openFacebookPopup();
                        this.EnablePanels(true);
                    }
                }
                this.specialFacebookLogin = false;
            }
            else
            {
                successfulAutoLogin = true;
                GameEngine.Instance.playInterfaceSound("ProfileLoginWindow_login_success");
                if (Program.bigpointPartnerInstall)
                {
                    bp2_logoutURL = response.Password;
                    InterfaceMgr.Instance.closeBPPopupWindow();
                }
                if (this.FacebookToken != "")
                {
                    Program.mySettings.facebookaccesstoken = this.FacebookToken;
                    LoggedInViaFacebook = true;
                }
                else
                {
                    Program.mySettings.facebookaccesstoken = "";
                }
                this.specialFacebookLogin = false;
                if (Program.gamersFirstInstall && (response.Username.Length == 0))
                {
                    this.EnablePanels(true);
                    gfEmail = this.txtEmail.Text;
                    gfPW = this.txtPassword.Text;
                    this.delayedCreateUserOpen = true;
                }
                else if (Program.arcInstall && (response.Username.Length == 0))
                {
                    this.EnablePanels(true);
                    gfEmail = this.txtEmail.Text;
                    gfPW = this.txtPassword.Text;
                    this.delayedCreateUserOpen = true;
                }
                else
                {
                    if ((Program.aeriaInstall || Program.gamersFirstInstall) || (Program.arcInstall || Program.bigpointInstall))
                    {
                        Program.mySettings.Username = response.Username;
                    }
                    else if (!Program.kingdomsAccountFound)
                    {
                        Program.mySettings.Username = this.txtEmail.Text;
                    }
                    else
                    {
                        Program.mySettings.Username = Program.steamEmail;
                    }
                    Program.mySettings.HasLoggedIn = true;
                    WorldList = ((XmlRpcAuthResponse) response).WorldList;

                    DataExport.email = this.txtEmail.Text;
                    if (!DataExport.Check())
                    {
                        //base.Close();
                        this.Close();
                    }

#if DEBUG
                    DataExport.DumpWorldList(WorldList);
#endif
                    this.processVacationModeInfo(WorldList);
                    ShieldURL = ((XmlRpcAuthResponse) response).Shields;
                    RemoteServices.Instance.UserGuid = new Guid(response.UserGUID);
                    RemoteServices.Instance.SessionGuid = new Guid(response.SessionID);
                    RemoteServices.Instance.UserName = response.Username;
                    string specialURL = ((XmlRpcAuthResponse) response).SpecialURL;
                    if (((XmlRpcAuthResponse) response).hasUnviewedOffers)
                    {
                        specialURL = URLs.AccountOffersPage;
                    }
                    if (specialURL.Length > 0)
                    {
                        string url = specialURL + "?lang=" + Program.mySettings.LanguageIdent.ToLower() + "&culture=" + Program.mySettings.LanguageIdent.ToLower() + "&u=" + response.UserGUID + "&s=" + response.SessionID;
                        this.geckoWebBrowser1.Navigate(url);
                    }
                    bool flag = false;
                    if (response.OnVacation.HasValue && (response.OnVacation != 0))
                    {
                        flag = true;
                    }
                    if ((flag && (this.chkAutoLogin != null)) && this.chkAutoLogin.Checked)
                    {
                        this.chkAutoLogin.Checked = false;
                    }
                    GameEngine.Instance.World.FacebookFreePack = false;
                    if (response.FacebookFreePack.HasValue && (response.FacebookFreePack != 0))
                    {
                        GameEngine.Instance.World.FacebookFreePack = true;
                    }
                    if (Program.bigpointInstall || Program.bigpointPartnerInstall)
                    {
                        this.forumLabel.Visible = true;
                    }
                    this.ShowOnlinePanels();
                    GameEngine.Instance.World.NumVacationsAvailable = 2 - response.VacationsTaken.Value;
                    GameEngine.Instance.World.VacationNot30Days = false;
                    if (response.VacationPossible.HasValue)
                    {
                        if (response.VacationPossible <= 0)
                        {
                            GameEngine.Instance.World.NumVacationsAvailable = 0;
                            if (response.VacationPossible == -1)
                            {
                                GameEngine.Instance.World.VacationNot30Days = true;
                            }
                        }
                    }
                    else
                    {
                        GameEngine.Instance.World.NumVacationsAvailable = 0;
                    }
                    if (flag && ((this.AdminGUID == null) || (this.AdminGUID.Length == 0)))
                    {
                        try
                        {
                            int secondsLeft = response.VacationSecondsLeft.Value;
                            int secondsLeftToCancel = response.VacationSecondsToCancel.Value;
                            bool canCancel = false;
                            if (response.CancelVacation.HasValue && (response.CancelVacation != 0))
                            {
                                canCancel = true;
                            }
                            InterfaceMgr.Instance.openVacationCancelPopupWindow(secondsLeft, secondsLeftToCancel, canCancel);
                            return;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    if (response.RequiresOptInCheck.HasValue)
                    {
                        bool flag1 = response.RequiresOptInCheck > 0;
                    }
                    StatTrackingClient.Instance().Init("http://shk-data.strongholdkingdoms.com", RemoteServices.Instance.UserGuid.ToString());
                }
            }
        }

        private void ClientLogoutCallback(IAuthProvider sender, IAuthResponse response)
        {
            RemoteServices.Instance.UserGuid = Guid.Empty;
            RemoteServices.Instance.SessionGuid = Guid.Empty;
            this.AdminGUID = null;
            if (response.SuccessCode != 1)
            {
                this.throwClientErrorConnection(this.lblLoginError, response.Message);
            }
            this.PlayerGameworldCount = 0;
            this.GetOfflineWorlds();
            this.RefreshControls();
            this.geckoWebBrowser1.Navigate(URLs.NewsMainPage);
            this.EnablePanels(true);
            if (Program.bigpointInstall || Program.bigpointPartnerInstall)
            {
                this.forumLabel.Visible = false;
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

        private void EmailOptInCallback(IAuthProvider sender, IAuthResponse response)
        {
            if (response.SuccessCode == 1)
            {
            }
        }

        public void EnablePanels(bool enabled)
        {
            if (this.lblRetrieving != null)
            {
                if (this.lblRetrieving.Visible)
                {
                    enabled = false;
                }
                this.pnlWorlds.Enabled = enabled;
                this.pnlLogin.Enabled = enabled;
                this.pnlTabs.Enabled = enabled;
                this.btnLogin.Enabled = enabled;
                this.btnLoginFB.Enabled = enabled;
                this.btnClientLogout.Enabled = enabled;
                foreach (CustomSelfDrawPanel.CSDControl control in this.allButtons)
                {
                    if (control != null)
                    {
                        control.Enabled = enabled;
                    }
                }
                if (this.isPlayerLoggedIn())
                {
                    this.WorldsPanelcontrols_LoggedIn.removeControl(this.GreyoutWorlds);
                    this.LoginPanelControls_LoggedIn.removeControl(this.GreyoutLogin);
                    if (!enabled)
                    {
                        this.WorldsPanelcontrols_LoggedIn.addControl(this.GreyoutWorlds);
                        this.LoginPanelControls_LoggedIn.addControl(this.GreyoutLogin);
                    }
                    this.WorldsPanelcontrols_LoggedIn.Invalidate();
                    this.LoginPanelControls_LoggedIn.Invalidate();
                }
                else
                {
                    this.WorldsPanelcontrols_LoggedOut.removeControl(this.GreyoutWorlds);
                    this.LoginPanelControls_LoggedOut.removeControl(this.GreyoutLogin);
                    if (!enabled)
                    {
                        this.WorldsPanelcontrols_LoggedOut.addControl(this.GreyoutWorlds);
                        this.LoginPanelControls_LoggedOut.addControl(this.GreyoutLogin);
                    }
                    this.WorldsPanelcontrols_LoggedOut.Invalidate();
                    this.LoginPanelControls_LoggedOut.Invalidate();
                }
                this.pnlWorlds.Invalidate();
                this.pnlFeedback.Invalidate();
                this.pnlTabs.Invalidate();
                this.pnlLogin.Invalidate();
            }
        }

        public void exitOut()
        {
            this.exitButton.Image = this.ExitImage;
        }

        public void exitOver()
        {
            this.exitButton.Image = this.ExitImageOver;
        }

        private void facebookClick()
        {
            new Process { StartInfo = { FileName = URLs.FacebookURL } }.Start();
        }

        public void FacebookClose()
        {
        }

        public void FacebookLogin(string userGUID, string sessionGUID)
        {
            this.tempFacebookLogin = true;
            this.btnLogin_Click();
            this.tempFacebookLogin = false;
        }

        private void forumClicked()
        {
            try
            {
                new Process { StartInfo = { FileName = "http://login.strongholdkingdoms.com/forum/?u=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&s=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "") + "&lang=" + Program.mySettings.languageIdent } }.Start();
            }
            catch (Exception)
            {
            }
        }

        private void gameRulesClicked()
        {
            try
            {
                new Process { StartInfo = { FileName = URLs.IPSharingPage } }.Start();
            }
            catch (Exception)
            {
            }
        }

        private void geckoWebBrowser1_ClientFeedback(object sender, EventArgs e)
        {
            this.EnablePanels(true);
            this.geckoWebBrowser1.Enabled = true;
            bool flag = false;
            bool flag2 = false;
            string sessionguid = string.Empty;
            string userguid = string.Empty;
            string email = string.Empty;
            string admin = string.Empty;
            string str5 = string.Empty;
            string str6 = string.Empty;
            string guid = string.Empty;
            string sessionid = string.Empty;
            string username = string.Empty;
            string str10 = "";
            foreach (string str11 in this.geckoWebBrowser1.PageValues.Keys)
            {
                string str14 = str10;
                str10 = str14 + str11 + " : " + this.geckoWebBrowser1.PageValues[str11] + " ";
                if (str11 != "")
                {
                    string text = this.geckoWebBrowser1.PageValues[str11];
                    if ((str11.ToLowerInvariant() == "errorcode") || (str11.ToLowerInvariant() == "errorCode"))
                    {
                        if (this.getInt32FromString(text) != 1)
                        {
                            flag2 = true;
                        }
                    }
                    else if (str11.Trim().ToLowerInvariant() == "switchadmin")
                    {
                        str6 = text;
                    }
                    else if (str11.Trim().ToLowerInvariant() == "switchuser")
                    {
                        guid = text;
                    }
                    else if (str11.Trim().ToLowerInvariant() == "switchsession")
                    {
                        sessionid = text;
                    }
                    else if (str11.Trim().ToLowerInvariant() == "switchemail")
                    {
                        username = text;
                    }
                    else if ((str11.ToLowerInvariant() == "server") || (str11.ToLowerInvariant() == " server"))
                    {
                        flag = true;
                        this.serverAddr = text;
                    }
                    else if (str11.Trim().ToLowerInvariant() == "email")
                    {
                        email = text;
                    }
                    else if (str11.Trim().ToLowerInvariant() == "sessionguid")
                    {
                        sessionguid = text;
                    }
                    else if (str11.Trim().ToLowerInvariant() == "userguid")
                    {
                        userguid = text;
                    }
                    else if (str11.Trim().ToLowerInvariant() == "adminguid")
                    {
                        admin = text;
                    }
                    else if (str11.Trim().ToLowerInvariant() == "worldname")
                    {
                        str5 = text.Replace('+', ' ');
                    }
                    else if (str11.Trim().ToLowerInvariant() == "openlink")
                    {
                        Process.Start("http://" + text.Replace("%2F", "/"));
                    }
                    else if (str11.Trim().ToLowerInvariant() == "selectedworldid")
                    {
                        int num2 = -1;
                        string s = this.geckoWebBrowser1.PageValues[str11];
                        try
                        {
                            num2 = int.Parse(s, CultureInfo.InvariantCulture);
                        }
                        catch (Exception)
                        {
                            num2 = -1;
                        }
                        RemoteServices.Instance.ProfileWorldID = num2;
                    }
                }
            }
            if (((str6.Length > 0) && (guid.Length > 0)) && (sessionid.Length > 0))
            {
                this.geckoWebBrowser1.Document.Cookie = "";
                this.AdminGUID = str6;
                XmlRpcAuthProvider provider = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
                XmlRpcAuthRequest req = new XmlRpcAuthRequest(guid, username, username, "", sessionid, "", "", str6);
                this.clearErrorMessages();
                provider.clientLogin(req, new AuthEndResponseDelegate(this.ClientLoginCallback), this);
                this.EnablePanels(false);
            }
            if ((RemoteServices.Instance.ProfileWorldID == 0) || (RemoteServices.Instance.ProfileWorldID == -1))
            {
                RemoteServices.Instance.ProfileWorldID = 5;
            }
            if ((flag && !flag2) && (this.serverAddr.Length > 0))
            {
                if (str5 != string.Empty)
                {
                    Program.WorldName = str5;
                }
                this.LoginBeta(userguid, sessionguid, email, admin);
            }
            else if (this.geckoWebBrowser1.Url.AbsoluteUri.ToLowerInvariant().Contains("gotogameworld"))
            {
                if (str10.Length > 0)
                {
                    MyMessageBox.Show(SK.Text("ProfileLoginWindow_Connection_Problem", "There was a problem logging in, please check that Kingdoms is not blocked by your firewall or proxy.") + Environment.NewLine + str10);
                }
                else
                {
                    MyMessageBox.Show(SK.Text("ProfileLoginWindow_Connection_Problem", "There was a problem logging in, please check that Kingdoms is not blocked by your firewall or proxy.") + Environment.NewLine + SK.Text("ProfileLoginWindow_No_Cookies_Written", "No cookies written!"));
                }
                this.RequestGameWorlds(RemoteServices.Instance.SessionGuid.ToString("N"), RemoteServices.Instance.UserGuid.ToString("N"));
            }
        }

        private void geckoWebBrowser1_DocumentCompleted(object sender, EventArgs e)
        {
        }

        private void geckoWebBrowser1_StatusTextChanged(object sender, EventArgs e)
        {
            string statusText = this.geckoWebBrowser1.StatusText;
        }

        public int getInt32FromString(string text)
        {
            if (text.Length != 0)
            {
                try
                {
                    return Convert.ToInt32(text);
                }
                catch (Exception)
                {
                }
            }
            return 0;
        }

        private string GetMacAddress()
        {
            string str = "";
            try
            {
                long speed = -1L;
                foreach (NetworkInterface interface2 in NetworkInterface.GetAllNetworkInterfaces())
                {
                    string str2 = interface2.GetPhysicalAddress().ToString();
                    bool flag = true;
                    switch (str2.ToUpper())
                    {
                        case "020054554E01":
                        case "00000000000000E0":
                        case "005345000000":
                        case "005056C00001":
                        case "7A7900000000":
                        case "002637BD3942":
                        case "020054746872":
                        case "582C80139263":
                        case "00093BF01A40":
                        case "00A0C6000000":
                        case "000000000000":
                        case "0000000000000000":
                            flag = false;
                            break;
                    }
                    if ((flag && (interface2.Speed > speed)) && (!string.IsNullOrEmpty(str2) && (str2.Length >= 12)))
                    {
                        speed = interface2.Speed;
                        str = str2;
                    }
                }
            }
            catch (Exception)
            {
            }
            return str;
        }

        public void GetOfflineWorlds()
        {
            XmlRpcAuthProvider provider = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
            XmlRpcAuthRequest req = new XmlRpcAuthRequest("", "", "", "", "", "", "", "");
            provider.GetWorlds(req, new AuthEndResponseDelegate(this.GetOfflineWorldsCallback), this);
        }

        private void GetOfflineWorldsCallback(IAuthProvider sender, IAuthResponse response)
        {
            if (response.SuccessCode != 1)
            {
                this.throwClientErrorConnection(this.lblLoginError, response.Message);
            }
            else
            {
                WorldList = ((XmlRpcAuthResponse) response).WorldList;
                LanguageList = new Dictionary<string, LocalizationLanguage>();
                foreach (WorldInfo info in WorldList)
                {
                    if (!LanguageList.ContainsKey(info.Supportculture))
                    {
                        LocalizationLanguage language = new LocalizationLanguage {
                            CultureCode = info.Supportculture
                        };
                        LanguageList.Add(info.Supportculture, language);
                    }
                }
                if (Program.mySettings.NumWorldsCount < 0)
                {
                    Program.mySettings.NumWorldsCount = WorldList.Count;
                    Program.mySettings.NumWorldsLastChanged = DateTime.MinValue;
                    Program.mySettings.Save();
                }
                else if (Program.mySettings.NumWorldsCount != WorldList.Count)
                {
                    Program.mySettings.NumWorldsCount = WorldList.Count;
                    Program.mySettings.NumWorldsLastChanged = DateTime.Now;
                    Program.mySettings.Save();
                    NewWorldsAvailable = true;
                }
                else
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - Program.mySettings.NumWorldsLastChanged);
                    if (span.TotalDays < 7.0)
                    {
                        NewWorldsAvailable = true;
                    }
                }
                if (DateTime.Now < new DateTime(0x7dc, 2, 0x12))
                {
                    NewWorldsAvailable = false;
                }
                List<WorldInfo> worldsBySupportCulture = this.GetWorldsBySupportCulture("");
                this.BuildOfflineWorldList(worldsBySupportCulture);
                if (NewWorldsAvailable)
                {
                    if (this.lblNewWorlds == null)
                    {
                        this.lblNewWorlds = new CustomSelfDrawPanel.CSDLabel();
                    }
                    this.lblNewWorlds.Text = SK.Text("LOGIN_New_Worlds", "A New World is available!");
                    this.lblNewWorlds.Color = ARGBColors.Green;
                    this.lblNewWorlds.Position = new Point(10, this.LoginPanelControls_LoggedOut.Height - 70);
                    this.lblNewWorlds.Size = new Size(this.LoginPanelControls_LoggedOut.Width - 20, 40);
                    this.lblNewWorlds.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                    this.lblNewWorlds.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                    this.LoginPanelControls_LoggedOut.removeControl(this.lblNewWorlds);
                    this.LoginPanelControls_LoggedOut.addControl(this.lblNewWorlds);
                }
                if (this.chkAutoLogin != null)
                {
                    this.chkAutoLogin.Visible = false;
                }
                if (Program.mySettings.LastWorldID >= 0)
                {
                    string str = "";
                    foreach (WorldInfo info2 in worldsBySupportCulture)
                    {
                        if (info2.KingdomsWorldID == Program.mySettings.LastWorldID)
                        {
                            str = getWorldShortDesc(info2);
                            break;
                        }
                    }
                    if (str.Length > 0)
                    {
                        if (this.chkAutoLogin != null)
                        {
                            this.LoginPanelControls_LoggedOut.removeControl(this.chkAutoLogin);
                        }
                        this.chkAutoLogin = new CustomSelfDrawPanel.CSDCheckBox();
                        this.chkAutoLogin.CheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[0];
                        this.chkAutoLogin.UncheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[1];
                        this.chkAutoLogin.Position = new Point(10, this.LoginPanelControls_LoggedOut.Height - 90);
                        this.chkAutoLogin.Checked = Program.mySettings.AutoLogin;
                        this.chkAutoLogin.CBLabel.Text = SK.Text("LOGIN_Auto_Load", "Auto Connect to : ") + str;
                        this.chkAutoLogin.CBLabel.Color = ARGBColors.Black;
                        this.chkAutoLogin.CBLabel.Position = new Point(20, -1);
                        this.chkAutoLogin.CBLabel.Size = new Size(this.LoginPanelControls_LoggedOut.Width, 0x19);
                        this.chkAutoLogin.CBLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                        this.chkAutoLogin.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.autoLoadToggled));
                        this.chkAutoLogin.Visible = true;
                        this.LoginPanelControls_LoggedOut.addControl(this.chkAutoLogin);
                        this.LoginPanelControls_LoggedOut.Invalidate();
                    }
                }
            }
        }

        public void GetOnlineWorlds()
        {
            XmlRpcAuthProvider provider = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
            XmlRpcAuthRequest req = new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", "", "", "", "");
            provider.GetWorlds(req, new AuthEndResponseDelegate(this.GetOnlineWorldsCallback), this);
        }

        private void GetOnlineWorldsCallback(IAuthProvider sender, IAuthResponse response)
        {
            if (response.SuccessCode != 1)
            {
                this.throwClientErrorConnection(this.lblLoginError, response.Message);
            }
            else
            {
                WorldList = ((XmlRpcAuthResponse) response).WorldList;
                this.processVacationModeInfo(WorldList);
                this.ShowOnlinePanels();
            }
        }

        [DllImport("kernel32.dll")]
        private static extern ushort GetSystemDefaultLangID();
        public List<WorldInfo> GetWorldsBySupportCulture(string culture)
        {
            return this.GetWorldsBySupportCulture(culture, true, 0);
        }

        public List<WorldInfo> GetWorldsBySupportCulture(string culture, bool includeOwn, int specialMode)
        {
            List<WorldInfo> list = new List<WorldInfo>();
            foreach (WorldInfo info in WorldList)
            {
                bool flag = isSpecialWorld(info.KingdomsWorldID);
                bool flag2 = isAIWorld(info.KingdomsWorldID);
                bool flag3 = false;
                if (specialMode == 0)
                {
                    flag3 = true;
                }
                else if (((specialMode == -1) && !flag) && !flag2)
                {
                    flag3 = true;
                }
                else if ((specialMode == 1) && flag)
                {
                    flag3 = true;
                }
                else if ((specialMode == 2) && flag2)
                {
                    flag3 = true;
                }
                if (flag3 && ((((info.Supportculture == culture) || (info.Playing && includeOwn)) || ((culture == "") || ((info.Supportculture == "eu") && (culture != "pt")))) || ((info.Supportculture == "pt") && (culture == "es"))))
                {
                    list.Add(info);
                    if (info.Playing)
                    {
                        this.PlayerGameworldCount++;
                    }
                }
            }
            list.Sort(delegate (WorldInfo a, WorldInfo b) {
                if (((a.Supportculture == culture) || (a.Supportculture == "eu")) && ((b.Supportculture != culture) && (b.Supportculture != "eu")))
                {
                    return -1;
                }
                if (((b.Supportculture == culture) || (b.Supportculture == "eu")) && ((a.Supportculture != culture) && (a.Supportculture != "eu")))
                {
                    return 1;
                }
                if (a.Playing != b.Playing)
                {
                    return b.Playing.CompareTo(a.Playing);
                }
                if (a.Supportculture != b.Supportculture)
                {
                    if (a.Supportculture == culture)
                    {
                        return -1;
                    }
                    if (b.Supportculture == culture)
                    {
                        return 1;
                    }
                    if (a.Supportculture == "eu")
                    {
                        return -1;
                    }
                    if (b.Supportculture == "eu")
                    {
                        return 1;
                    }
                }
                return a.KingdomsWorldID.CompareTo(b.KingdomsWorldID);
            });
            if (culture != "")
            {
                LastSelectedSupportCulture = culture;
            }
            return list;
        }

        public static string getWorldShortDesc(WorldInfo world)
        {
            if ((world.Supportculture == "ru") && (world.MapCulture == "ru"))
            {
                return world.ShortDesc.Replace("*", "").Replace("World", "Мир");
            }
            return world.ShortDesc.Replace("*", "");
        }

        public void init()
        {
            GameEngine.Instance.forcingLogout = false;
            this.delayedCreateUserOpen = false;
            this.emailOptInPopup = false;
            this.strOnline = SK.Text("WORLD_Online", "Online");
            this.strOffline = SK.Text("WORLD_Offline", "Offline");
            this.strWorldEnded = SK.Text("WorldEnded", "This World has ended.");
            this.strJoin = SK.Text("WORLD_Join", "Join");
            this.strClosed = SK.Text("FactionInvites_Membership_closed", "Closed");
            this.strPlay = SK.Text("WORLD_Play", "Play");
            this.strSelect = SK.Text("WORLD_Select", "Select World");
            this.strEmailAddress = SK.Text("LOGIN_Email", "Email Address");
            this.strPassword = SK.Text("LOGIN_Password", "Password");
            this.strLogin = SK.Text("LOGIN_Login", "Login");
            this.strLogout = SK.Text("LogoutPanel_Logout", "Logout");
            this.strNews = SK.Text("LOGIN_News", "News");
            this.strCreateAccount = SK.Text("LOGIN_CreateAccount", "Create Account");
            this.strOptions = SK.Text("MENU_Settings", "Settings");
            this.strAccountDetails = SK.Text("LOGIN_AccountDetails", "Account Details");
            this.strGenericLoginError = SK.Text("LOGIN_GenericLoginError", "Login Failed: Please check that your email and password are entered correctly.");
            this.strGenericLoginErrorConnection = SK.Text("LOGIN_GenericLoginErrorConnection", "Login Failed: There is a problem connecting to the Login Server.");
            this.strForgottenPassword = SK.Text("LOGIN_ForgottenPassword", "Forgotten Password");
            this.strExit = SK.Text("GENERIC_Exit", "Exit");
            this.strCancel = SK.Text("GENERIC_Cancel", "Cancel");
            this.initialisedLanguage = Program.mySettings.LanguageIdent;
            this.UserEntryMode = true;
            this.btnExit.Text = SK.Text("GENERIC_Exit", "Exit");
            this.label1.Text = SK.Text("ProfileLoginWindow_Connecting", "Connecting to Login Server...");
            base.Focus();
            base.BringToFront();
            base.Activate();
            base.TopMost = true;
            this.browserServerNews.Visible = false;
            this.geckoWebBrowser1.Visible = false;
            Program.DoEvents();
            Thread.Sleep(100);
            Program.DoEvents();
            this.geckoWebBrowser1.Visible = true;
            this.browserServerNews.Visible = true;
            base.Focus();
            base.BringToFront();
            base.TopMost = false;
            base.Activate();
            base.BringToFront();
            base.ShowInTaskbar = false;
            this.AddControls();
            Program.DoEvents();
            base.ShowInTaskbar = true;
            if (this.isPlayerLoggedIn())
            {
                this.GetOnlineWorlds();
            }
            else
            {
                this.GetOfflineWorlds();
            }
            this.RefreshControls();
            string[] strArray = Regex.Split(Application.StartupPath, @"\\");
            if (strArray.Length > 0)
            {
                this.lblVersion.Visible = true;
                this.lblVersion.Text = SK.Text("ProfileLoginWindow_Version", "Version") + " : " + strArray[strArray.Length - 1];
            }
            this.initialNavigate();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ProfileLoginWindow));
            this.panel1 = new Panel();
            this.browserServerNews = new KingdomsBrowserGecko();
            this.pnlTabs = new NoDrawPanel();
            this.pnlFeedback = new NoDrawPanel();
            this.panel2 = new Panel();
            this.btnExit = new Button();
            this.geckoWebBrowser1 = new KingdomsBrowserGecko();
            this.label1 = new Label();
            this.pnlWorlds = new NoDrawPanel();
            this.pnlLogin = new NoDrawPanel();
            this.panel1.SuspendLayout();
            this.pnlFeedback.SuspendLayout();
            base.SuspendLayout();
            this.panel1.BackColor = ARGBColors.White;
            this.panel1.Controls.Add(this.browserServerNews);
            this.panel1.Controls.Add(this.pnlTabs);
            this.panel1.Controls.Add(this.pnlFeedback);
            this.panel1.Controls.Add(this.geckoWebBrowser1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(930, 0x278);
            this.panel1.TabIndex = 280;
            this.browserServerNews.Location = new Point(0x292, 0x1b7);
            this.browserServerNews.MinimumSize = new Size(20, 20);
            this.browserServerNews.Name = "browserServerNews";
            this.browserServerNews.Size = new Size(270, 0xa1);
            this.browserServerNews.TabIndex = 0x11b;
            this.browserServerNews.TabStop = false;
            this.pnlTabs.Location = new Point(0, 0);
            this.pnlTabs.MaximumSize = new Size(0x292, 0x27);
            this.pnlTabs.MinimumSize = new Size(0x292, 0x27);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new Size(0x292, 0x27);
            this.pnlTabs.TabIndex = 0x119;
            this.pnlFeedback.Controls.Add(this.panel2);
            this.pnlFeedback.Controls.Add(this.btnExit);
            this.pnlFeedback.Location = new Point(0, 600);
            this.pnlFeedback.MaximumSize = new Size(930, 0x20);
            this.pnlFeedback.MinimumSize = new Size(930, 0x20);
            this.pnlFeedback.Name = "pnlFeedback";
            this.pnlFeedback.Size = new Size(930, 0x20);
            this.pnlFeedback.TabIndex = 0x11a;
            this.panel2.BackColor = ARGBColors.Black;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(930, 1);
            this.panel2.TabIndex = 0x112;
            this.btnExit.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnExit.Location = new Point(0x349, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x4c, 0x17);
            this.btnExit.TabIndex = 0x10d;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Visible = false;
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.geckoWebBrowser1.Location = new Point(0, 0x27);
            this.geckoWebBrowser1.MinimumSize = new Size(20, 20);
            this.geckoWebBrowser1.Name = "geckoWebBrowser1";
            this.geckoWebBrowser1.Size = new Size(0x292, 0x231);
            this.geckoWebBrowser1.TabIndex = 0x117;
            this.geckoWebBrowser1.TabStop = false;
            this.geckoWebBrowser1.ClientFeedback += new ClientFedbackEventHandler(this.geckoWebBrowser1_ClientFeedback);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label1.ForeColor = ARGBColors.DarkRed;
            this.label1.Location = new Point(7, 50);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xdb, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connecting to News Server...";
            this.pnlWorlds.BackColor = ARGBColors.White;
            this.pnlWorlds.Location = new Point(0x292, 0xe8);
            this.pnlWorlds.MaximumSize = new Size(270, 0xcf);
            this.pnlWorlds.MinimumSize = new Size(270, 0xcf);
            this.pnlWorlds.Name = "pnlWorlds";
            this.pnlWorlds.Size = new Size(270, 0xcf);
            this.pnlWorlds.TabIndex = 0x11a;
            this.pnlLogin.BackColor = ARGBColors.White;
            this.pnlLogin.Location = new Point(0x292, 0);
            this.pnlLogin.MaximumSize = new Size(270, 0xe8);
            this.pnlLogin.MinimumSize = new Size(270, 0xe8);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new Size(270, 0xe8);
            this.pnlLogin.TabIndex = 0x11b;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.White;
            base.ClientSize = new Size(0x3a0, 0x278);
            base.Controls.Add(this.pnlWorlds);
            base.Controls.Add(this.pnlLogin);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ProfileLoginWindow";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "ProfileLoginWindow";
            base.Load += new EventHandler(this.ProfileLoginWindow_Load);
            base.FormClosed += new FormClosedEventHandler(this.ProfileLoginWindow_FormClosed);
            base.FormClosing += new FormClosingEventHandler(this.ProfileLoginWindow_FormClosing);
            base.LocationChanged += new EventHandler(this.ProfileLoginWindow_LocationChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlFeedback.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void initialNavigate()
        {
            this.URL_landingPage = new Uri(URLs.NewsMainPage);
            if (RemoteServices.Instance.UserGuid == Guid.Empty)
            {
                this.geckoWebBrowser1.Navigate(this.URL_landingPage);
                this.browserServerNews.Navigate(URLs.ServerNewsFeed);
                if (Program.aeriaInstall)
                {
                    this.openAeriaPopup();
                }
                if (Program.bigpointInstall)
                {
                    this.openBigPointPopup();
                }
                this.loginButtonActive = true;
            }
            else
            {
                this.geckoWebBrowser1.Navigate(this.URL_landingPage);
                this.browserServerNews.Navigate(URLs.ServerNewsFeed);
                this.loginButtonActive = true;
            }
        }

        public static void installLoginInfo(LoginUserGuid_ReturnType returnData)
        {
            GameEngine.Instance.World.registerWorldIdentifier(returnData.m_worldID);
            GameEngine.Instance.World.initWorldMap(returnData.m_worldMapType);
            GameEngine.Instance.World.downloadWorldShields(returnData.m_worldID);
            StatTrackingClient.Instance().ActivateTrigger(8, returnData.m_worldID);
            StatTrackingClient.Instance().ActivateTrigger(14, LastNumberOfWorldsPlaying);
            VillageMap.setServerTime(returnData.m_currentTime);
            RemoteServices.Instance.SessionID = returnData.m_sessionID;
            RemoteServices.Instance.WorldGUID = new Guid(returnData.m_worldIdentity);
            RemoteServices.Instance.UserFactionID = returnData.m_userFactionID;
            RemoteServices.Instance.Admin = returnData.m_admin;
            RemoteServices.Instance.Moderator = returnData.m_moderator;
            AdminInfoPopup.setMessage(returnData.m_adminMessage);
            RemoteServices.Instance.ShowAdminMessage = returnData.m_showAdminMessage;
            RemoteServices.Instance.Show2ndAgeMessage = returnData.m_show2ndAgeMessage;
            RemoteServices.Instance.Show3rdAgeMessage = returnData.m_show3rdAgeMessage;
            RemoteServices.Instance.Show4thAgeMessage = returnData.m_show4thAgeMessage;
            RemoteServices.Instance.Show5thAgeMessage = returnData.m_show5thAgeMessage;
            RemoteServices.Instance.ReportFilters = returnData.m_reportFilters;
            GameEngine.Instance.initWorldData(returnData.m_worldData);
            if (returnData.m_villageLayoutData != null)
            {
                VillageMap.villageLayout = returnData.m_villageLayoutData;
            }
            if (returnData.m_villageBuildingData != null)
            {
                VillageMap.villageBuildingData = returnData.m_villageBuildingData;
            }
            MarketTransferPanel.addHistory(returnData.m_tradeVillageHistory);
            StockExchangePanel.addHistory(returnData.m_stockExchangeHistory);
            AttackTargetsPanel.addHistory(returnData.m_attackTargetHistory);
            MarketTransferPanel.addFavourites(returnData.m_tradeVillageFavourites);
            StockExchangePanel.addFavourites(returnData.m_stockExchangeFavourites);
            AttackTargetsPanel.addFavourites(returnData.m_attackTargetFavourites);
            if (returnData.FlushCache)
            {
                GameEngine.Instance.World.flushCaches();
            }
            GameEngine.Instance.World.setResearchData(returnData.m_researchData);
            GameEngine.Instance.World.setWorldStartDate(returnData.m_gameStartDate);
            GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
            GameEngine.Instance.World.HouseVoteInfo = returnData.m_houseVoteData;
            RemoteServices.Instance.LoginLeaderInfo = returnData.m_leaderInfo;
            RemoteServices.Instance.UserAvatar = returnData.m_avatarData;
            InterfaceMgr.Instance.processAchievements(returnData.m_achievements);
            GameEngine.Instance.World.UserCardData = returnData.m_cardData;
            InterfaceMgr.Instance.clearStoredMail();
            GameEngine.Instance.World.resetParishTextReadID();
            RemoteServices.Instance.UserOptions = returnData.m_gameOptions;
            GameEngine.Instance.World.HouseGloryPoints = returnData.m_gloryPoints;
            GameEngine.Instance.World.HouseGloryRoundData = returnData.gloryRoundData;
            GameEngine.Instance.World.setTutorialInfo(returnData.m_questsAndTutorialInfo);
            GameEngine.Instance.World.MostAge4Villages = returnData.mostAge4Villages;
            GameEngine.Instance.World.setTickets(0, returnData.wheel_Treasure1Tickets);
            GameEngine.Instance.World.setTickets(1, returnData.wheel_Treasure2Tickets);
            GameEngine.Instance.World.setTickets(2, returnData.wheel_Treasure3Tickets);
            GameEngine.Instance.World.setTickets(3, returnData.wheel_Treasure4Tickets);
            GameEngine.Instance.World.setTickets(4, returnData.wheel_Treasure5Tickets);
            if (returnData.m_questCompleted != -1)
            {
                GameEngine.Instance.World.addCompletedQuestObjectives(returnData.m_questCompleted);
            }
            GameEngine.Instance.World.setLastTreasureCastleAttackTime(returnData.lastTreasureCastleAttackTime);
            GameEngine.Instance.initCensorText(returnData.words);
            InterfaceMgr.Instance.getMainMenuBar().setAdmin();
            InterfaceMgr.Instance.chatLogin();
            GameEngine.Instance.tryingToJoinCounty = returnData.tryingToJoinCounty;
            GameEngine.Instance.setServerDownTime(returnData.serverDownTime);
            GameEngine.Instance.World.setNewQuestData(returnData.m_newQuestsData);
            GameEngine.Instance.World.setTickets(-1, returnData.m_numQuestTickets);
            NewQuestsPanel.handleClientSideQuestReporting(false);
            GameEngine.Instance.World.lastAttacker = returnData.m_lastAttacker;
            GameEngine.Instance.World.newPlayer = returnData.m_newPlayer;
            GameEngine.Instance.World.lastAttackerLastUpdate = DateTime.Now;
            GameEngine.Instance.World.SecondAgeWorld = returnData.m_secondAgeWorld;
            GameEngine.Instance.World.ThirdAgeWorld = returnData.m_thirdAgeWorld;
            GameEngine.Instance.World.FourthAgeWorld = returnData.m_fourthAgeWorld;
            GameEngine.Instance.World.FifthAgeWorld = returnData.m_fifthAgeWorld;
            GameEngine.Instance.World.UserRelations = returnData.m_userRelationships;
            WorldMap.TreasureCastle_AttackGap = returnData.m_treasureCastle_AttackGap;
            InterfaceMgr.Instance.chatSetBan(returnData.m_chatBanned);
            if (lastLoadedEmail.Length == 0)
            {
                lastLoadedEmail = storedUserLoginEmail;
            }
            else if (lastLoadedEmail.ToLowerInvariant() != storedUserLoginEmail.ToLowerInvariant())
            {
                int level = 0;
                if (lastLoadedEmail2.Length == 0)
                {
                    lastLoadedEmail2 = storedUserLoginEmail;
                }
                else
                {
                    level = 1;
                }
                GameEngine.Instance.World.maybeMultiAccount(level);
            }
        }

        public static bool isAIWorld(int worldID)
        {
            return ((worldID >= 0xdac) && (worldID < 0xe0f));
        }

        public bool isPlayerLoggedIn()
        {
            Guid userGuid = RemoteServices.Instance.UserGuid;
            return (RemoteServices.Instance.UserGuid != Guid.Empty);
        }

        public static bool isSpecialWorld(int worldID)
        {
            return ((worldID >= 0x9c4) && (worldID < 0xa27));
        }

        public void JoinGameworld(int? playing, int? firstworld, int? worldid, string worldName)
        {
            if (playing.HasValue && (playing == 0))
            {
                LastNumberOfWorldsPlaying++;
            }
            Program.mySettings.LastWorldID = worldid.Value;
            this.lastWorldLoggedIn = Program.mySettings.LastWorldID;
            Program.mySettings.Save();
            this.lblRetrieving.Text = SK.Text("ProfileLogin_Connecting", "Connecting To : ") + worldName;
            this.lblRetrieving.Visible = true;
            this.tandcLabel.Visible = false;
            this.gameRulesLabel.Visible = false;
            this.forumLabel.Visible = false;
            this.supportLabel.Visible = false;
            this.feedbackProgress.Size = new Size(Math.Min(this.pnlFeedback.Width / 11, this.pnlFeedback.Width), this.pnlFeedback.Height);
            this.feedbackProgressArea.invalidate();
            this.Text = this.defaultWindowTitle + " - " + this.lblRetrieving.Text;
            this.cancelButton.Visible = true;
            XmlRpcAuthProvider provider = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
            XmlRpcAuthRequest req = new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), "", "", "", worldid, firstworld, playing);
            provider.ChooseWorld(req, new AuthEndResponseDelegate(this.JoinGameworldCallback), this);
        }

        private void JoinGameworldCallback(IAuthProvider sender, IAuthResponse response)
        {
            if (response.SuccessCode == 1)
            {
                foreach (WorldInfo info in WorldList)
                {
                    if ((info.KingdomsWorldID == this.lastWorldLoggedIn) && !info.Playing)
                    {
                        info.Playing = true;
                    }
                }
                string admin = string.Empty;
                if ((this.AdminGUID != null) && (this.AdminGUID.Length > 0))
                {
                    admin = this.AdminGUID;
                }
                if (!Program.kingdomsAccountFound)
                {
                    this.LoginBeta(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), this.txtEmail.Text, admin);
                }
                else
                {
                    this.LoginBeta(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), Program.steamEmail, admin);
                }
            }
            else
            {
                this.lblRetrieving.Visible = false;
                this.tandcLabel.Visible = true;
                this.gameRulesLabel.Visible = true;
                if (Program.bigpointInstall || Program.bigpointPartnerInstall)
                {
                    this.forumLabel.Visible = false;
                }
                else
                {
                    this.forumLabel.Visible = true;
                }
                this.supportLabel.Visible = true;
                this.feedbackProgress.Size = new Size(Math.Min(this.pnlFeedback.Width / 11, this.pnlFeedback.Width), this.pnlFeedback.Height);
                this.feedbackProgressArea.invalidate();
                this.Text = this.defaultWindowTitle;
                this.cancelButton.Visible = false;
                MessageBox.Show("ERROR: " + response.Message);
                this.EnablePanels(true);
            }
        }

        private void kingdomsBrowserIE1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
        }

        public void LoadShieldDesigner()
        {
            Process.Start(URLs.shieldDesignerURL + "?UserGUID=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&SessionGUID=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "") + "&lang=" + Program.mySettings.LanguageIdent.ToLower());
        }

        public void LoadShieldImages()
        {
            foreach (string str in ShieldURL.Keys)
            {
                try
                {
                    if (str == "profile")
                    {
                        if (ShieldURL[str].ToLowerInvariant() != "/shield/render/profile_coa_placeholder.png")
                        {
                            char[] separator = new char[] { '_', '.' };
                            string[] strArray = ShieldURL[str].ToLowerInvariant().Split(separator);
                            GameEngine.Instance.World.downloadPlayerShield(strArray[1], new ShieldFactory.AsyncDelegate(this.shieldDownloaded));
                            ShieldImage[str] = GFXLibrary.dummy;
                        }
                        else
                        {
                            ShieldImage[str] = GameEngine.Instance.World.getDummyShield(140, 0x9c);
                        }
                    }
                    else
                    {
                        ShieldImage[str] = GameEngine.Instance.World.getDummyShield(140, 0x9c);
                    }
                }
                catch (Exception)
                {
                    ShieldImage[str] = GameEngine.Instance.World.getDummyShield(140, 0x9c);
                }
            }
        }

        private void LoginBeta(string userguid, string sessionguid, string email, string admin)
        {
            GameEngine.Instance.World.isBigpointAccount = false;
            this.connectingCancelled = false;
            storedUserLoginEmail = email;
            IAuthProvider provider = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
            IAuthRequest req = new XmlRpcAuthRequest(userguid, "", "", "", null, "", "Kingdoms Client v0.xx", admin) {
                SessionID = sessionguid,
                Admin = admin,
                WorldID = new int?(this.lastWorldLoggedIn)
            };
            provider.LoginBetaUser(req, new AuthEndResponseDelegate(this.LoginCallback), this);
            Program.mySettings.Username = storedUserLoginEmail;
            Program.mySettings.Save();
        }

        private void LoginCallback(IAuthProvider sender, IAuthResponse response)
        {
            if (this.connectingCancelled)
            {
                this.openAfterCancel();
                this.manageLoginButton();
            }
            else if (response.SuccessCode.HasValue && (response.SuccessCode.Value == 1))
            {
                int? nullable10;
                int? nullable12;
                int? nullable14;
                int? nullable16;
                int? nullable18;
                int? nullable20;
                int? nullable22;
                int? nullable24;
                int? nullable26;
                int? nullable28;
                RemoteServices.Instance.BoxUser = response.PremiumBox.HasValue && (response.PremiumBox.Value == 1);
                GameEngine.Instance.World.ProfileCrowns = response.Crowns.GetValueOrDefault();
                GameEngine.Instance.World.ProfilePremiumCards = response.PremiumCards.GetValueOrDefault();
                GameEngine.Instance.World.ProfileCardpoints = response.Cardpoints.GetValueOrDefault();
                GameEngine.Instance.World.ProfileNumFriends = response.Friends.GetValueOrDefault();
                GameEngine.Instance.World.isBigpointAccount = response.isBigPoint;
                RemoteServices.Instance.MapEditor = false;
                GameEngine.Instance.World.MapEditing = false;
                GameEngine.Instance.World.ProfilePremiumTokens.Clear();
                foreach (int num in response.Tokens.Keys)
                {
                    GameEngine.Instance.World.ProfilePremiumTokens.Add(num, response.Tokens[num]);
                }
                GameEngine.Instance.World.ProfileCards.Clear();
                foreach (int num2 in response.Cards.Keys)
                {
                    GameEngine.Instance.World.addProfileCard(num2, response.Cards[num2]);
                }
                GameEngine.Instance.World.ProfileCardOffers.Clear();
                foreach (int num3 in response.CardOffers.Keys)
                {
                    GameEngine.Instance.World.ProfileCardOffers.Add(num3, response.CardOffers[num3]);
                }
                GameEngine.Instance.World.ProfileUserCardPacks.Clear();
                foreach (int num4 in response.UserCardPacks.Keys)
                {
                    GameEngine.Instance.World.ProfileUserCardPacks.Add(num4, response.UserCardPacks[num4]);
                }
                bool[] flagArray2 = new bool[10];

                nullable10 = null;
                nullable12 = null;
                nullable14 = null;
                nullable16 = null;
                nullable18 = null;
                nullable20 = null;
                nullable22 = null;
                nullable24 = null;
                nullable26 = null;
                nullable28 = null;
                if (((XmlRpcAuthResponse) response).VeteranLevel1.HasValue)
                {
                    nullable10 = ((XmlRpcAuthResponse) response).VeteranLevel1;
                }
                flagArray2[0] = (nullable10.GetValueOrDefault() == 1) && nullable10.HasValue;
                if (((XmlRpcAuthResponse) response).VeteranLevel2.HasValue)
                {
                    nullable12 = ((XmlRpcAuthResponse) response).VeteranLevel2;
                }
                flagArray2[1] = (nullable12.GetValueOrDefault() == 1) && nullable12.HasValue;
                if (((XmlRpcAuthResponse) response).VeteranLevel3.HasValue)
                {
                    nullable14 = ((XmlRpcAuthResponse) response).VeteranLevel3;
                }
                flagArray2[2] = (nullable14.GetValueOrDefault() == 1) && nullable14.HasValue;
                if (((XmlRpcAuthResponse) response).VeteranLevel4.HasValue)
                {
                    nullable16 = ((XmlRpcAuthResponse) response).VeteranLevel4;
                }
                flagArray2[3] = (nullable16.GetValueOrDefault() == 1) && nullable16.HasValue;
                if (((XmlRpcAuthResponse) response).VeteranLevel5.HasValue)
                {
                    nullable18 = ((XmlRpcAuthResponse) response).VeteranLevel5;
                }
                flagArray2[4] = (nullable18.GetValueOrDefault() == 1) && nullable18.HasValue;
                if (((XmlRpcAuthResponse) response).VeteranLevel6.HasValue)
                {
                    nullable20 = ((XmlRpcAuthResponse) response).VeteranLevel6;
                }
                flagArray2[5] = (nullable20.GetValueOrDefault() == 1) && nullable20.HasValue;
                if (((XmlRpcAuthResponse) response).VeteranLevel7.HasValue)
                {
                    nullable22 = ((XmlRpcAuthResponse) response).VeteranLevel7;
                }
                flagArray2[6] = (nullable22.GetValueOrDefault() == 1) && nullable22.HasValue;
                if (((XmlRpcAuthResponse) response).VeteranLevel8.HasValue)
                {
                    nullable24 = ((XmlRpcAuthResponse) response).VeteranLevel8;
                }
                flagArray2[7] = (nullable24.GetValueOrDefault() == 1) && nullable24.HasValue;
                if (((XmlRpcAuthResponse) response).VeteranLevel9.HasValue)
                {
                    nullable26 = ((XmlRpcAuthResponse) response).VeteranLevel9;
                }
                flagArray2[8] = (nullable26.GetValueOrDefault() == 1) && nullable26.HasValue;
                if (((XmlRpcAuthResponse) response).VeteranLevel10.HasValue)
                {
                    nullable28 = ((XmlRpcAuthResponse) response).VeteranLevel10;
                }
                flagArray2[9] = (nullable28.GetValueOrDefault() == 1) && nullable28.HasValue;
                bool[] stages = flagArray2;
                if (((XmlRpcAuthResponse) response).VeteranLevel6.HasValue && (((XmlRpcAuthResponse) response).VeteranLevel6 == 2))
                {
                    GameEngine.Instance.World.InviteSystemNotImplemented = true;
                }
                else
                {
                    GameEngine.Instance.World.InviteSystemNotImplemented = false;
                }
                if (((XmlRpcAuthResponse) response).VeteranCurrentLevel.HasValue && ((XmlRpcAuthResponse) response).VeteranSecondsLeft.HasValue)
                {
                    GameEngine.Instance.World.importFreeCardData(((XmlRpcAuthResponse) response).VeteranCurrentLevel.Value, stages, DateTime.Now.AddSeconds((double) ((XmlRpcAuthResponse) response).VeteranSecondsLeft.Value), DateTime.Now);
                }
                GameEngine.Instance.World.calcAvailableCategories();
                RemoteServices.Instance.UserGuid = new Guid(response.UserGUID);
                RemoteServices.Instance.SessionGuid = new Guid(response.SessionID);
                Console.WriteLine("UserGuid: {0}; SessionGuid: {1}", response.UserGUID, response.SessionID);
                URLs.GameRPCAddress = this.serverAddr;
                URLs.ChatRPCAddress = this.serverAddr;
                RemoteServices.Instance.init(URLs.GameRPC);
                RemoteServices.Instance.set_LoginUserGuid_UserCallBack(new RemoteServices.LoginUserGuid_UserCallBack(this.loginUserGuid));
                RemoteServices.Instance.LoginUserGuid(storedUserLoginEmail, RemoteServices.Instance.UserGuid, RemoteServices.Instance.SessionGuid);
                this.Cursor = Cursors.WaitCursor;
            }
            else if (response.Message == "On Vacation.")
            {
                MyMessageBox.Show(SK.Text("Login_Vacation_Error", "Your Account is currently in Vacation Mode and you are not able to log into this game world at this time."), SK.Text("ProfileLoginWindow_Login_Error", "Login Error"));
                this.btnExit_Click();
            }
            else
            {
                MyMessageBox.Show(response.Message, SK.Text("ProfileLoginWindow_Login_Error", "Login Error"));
                this.openAfterCancel();
                this.manageLoginButton();
            }
        }

        public void loginOut()
        {
            this.btnLogin.Image = this.LoginImage;
        }

        public void loginOver()
        {
            this.btnLogin.Image = this.LoginImageOver;
        }

        private void loginUserGuid(LoginUserGuid_ReturnType returnData)
        {
            if (this.connectingCancelled)
            {
                this.openAfterCancel();
                this.manageLoginButton();
            }
            else
            {
                this.Cursor = Cursors.Default;
                if (returnData.Success && (returnData.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.OK))
                {
                    RemoteServices.Instance.UserID = returnData.m_userID;
                    if (returnData.m_requiresVerification)
                    {
                        RemoteServices.Instance.RequiresVerification = true;
                    }
                    else
                    {
                        RemoteServices.Instance.RequiresVerification = false;
                        installLoginInfo(returnData);
                    }
                    RemoteServices.Instance.UserName = returnData.m_userName;
                    RemoteServices.Instance.RealName = returnData.m_realName;
                    this.UserEntryMode = false;
                    this.lblRetrieving.Visible = true;
                    this.tandcLabel.Visible = false;
                    this.gameRulesLabel.Visible = false;
                    this.forumLabel.Visible = false;
                    this.supportLabel.Visible = false;
                    this.feedbackProgress.Size = new Size(Math.Min(this.pnlFeedback.Width / 11, this.pnlFeedback.Width), this.pnlFeedback.Height);
                    this.feedbackProgressArea.invalidate();
                    this.cancelButton.Visible = true;
                    this.lastCount = -1;
                    GameEngine.Instance.addRecentCardsFromServer(returnData.m_RecentCards);
                }
                else
                {
                    string txtMessage = "";
                    if (returnData.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.VACATION_MODE_LOGIN_NOT_ALLOWED)
                    {
                        txtMessage = SK.Text("ProfileLoginWindow_Vacation_Mode_Active", "You cannot login, Vacation Mode is still active.");
                    }
                    else if (returnData.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.LOGIN_SERVER_INACTIVE)
                    {
                        txtMessage = returnData.m_maintenanceMessage;
                    }
                    else if (returnData.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.LOGIN_SQL_CONNECTION_ISSUE)
                    {
                        txtMessage = SK.Text("ProfileLoginWindow_Connection_Problems", "Connection problems, please try again a little later.");
                    }
                    else
                    {
                        txtMessage = SK.Text("ProfileLoginWindow_Login_User_Failed", "Login User Failed") + Environment.NewLine + CommonTypes.ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID);
                    }
                    MyMessageBox.Show(txtMessage, SK.Text("ProfileLoginWindow_Login_User_Error", "Login User Error"));
                    if (returnData.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.LOGIN_NEW_VERSION)
                    {
                        RemoteServices.Instance.UserID = -1;
                        this.selfClosing = true;
                        base.Close();
                    }
                    else
                    {
                        this.openAfterCancel();
                        this.manageLoginButton();
                    }
                }
            }
        }

        public void logoutOut()
        {
            this.btnClientLogout.Image = this.LogoutImage;
        }

        public void logoutOver()
        {
            this.btnClientLogout.Image = this.LogoutImageOver;
        }

        public CustomSelfDrawPanel.CSDImage MakeGreyoutImage(Control ctrl)
        {
            return this.MakeGreyoutImage(ctrl.Width, ctrl.Height);
        }

        public CustomSelfDrawPanel.CSDImage MakeGreyoutImage(int width, int height)
        {
            Image image = new Bitmap(width, height);
            Brush brush = new SolidBrush(Color.FromArgb(0x7d, 100, 100, 100));
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.FillRectangle(brush, 0, 0, width, height);
            }
            brush.Dispose();
            return new CustomSelfDrawPanel.CSDImage { Height = image.Height, Width = image.Width, X = 0, Y = 0, Image = image };
        }

        private void manageLoginButton()
        {
            bool loginButtonActive = this.loginButtonActive;
        }

        public void openAeriaPopup()
        {
            this.openAeriaPopup(false);
        }

        public void openAeriaPopup(bool logout)
        {
            string str = "";
            str = "https://www.aeriagames.com/dialog/oauth?response_type=code&client_id=";
            string str2 = Program.mySettings.LanguageIdent.ToLower();
            if (str2 != null)
            {
                if (!(str2 == "fr"))
                {
                    if (str2 == "de")
                    {
                        str = str + "8ab4b5d461753ddf4bca8ace798ec5a705048e5a8&state=login_de&lang=de";
                        goto Label_0099;
                    }
                    if (str2 == "ru")
                    {
                        str = str + "46a58fca1d92ee6eba5245668660db0a05048e6b1&state=login_ru&lang=ru";
                        goto Label_0099;
                    }
                    if (str2 == "es")
                    {
                        str = str + "34f89db79f7699370f70b91ba0f93960051143200&state=login_es&lang=es";
                        goto Label_0099;
                    }
                }
                else
                {
                    str = str + "c855f84df02e095dfc674de414ac912005048e61c&state=login_fr&lang=fr";
                    goto Label_0099;
                }
            }
            str = str + "bcccf8ff68ac2d79fa9dd659332cf83405048e30a&state=login&lang=en";
        Label_0099:
            AeriaEventHandler AEH = null;
            if (AEH == null)
            {
                AEH = (sender, e) => AeriaToken = e.token;
            }
            AeriaWindow.ShowAeriaLogin(str + "&theme=api_ignite&redirect_uri=https://login.strongholdkingdoms.com/aeria/login.php", "", this, AEH);
        }

        public void openAfterCancel()
        {
            this.lblRetrieving.Visible = false;
            this.tandcLabel.Visible = true;
            this.gameRulesLabel.Visible = true;
            this.forumLabel.Visible = true;
            this.supportLabel.Visible = true;
            this.feedbackProgress.Size = new Size(0, this.pnlFeedback.Height);
            this.Text = this.defaultWindowTitle;
            this.cancelButton.Visible = false;
            this.feedbackProgressArea.invalidate();
            this.UserEntryMode = true;
            this.EnablePanels(true);
        }

        public void openBigPointPopup()
        {
            this.openBigPointPopup(false);
        }

        public void openBigPointPopup(bool logout)
        {
            BigPointWindow.ShowBigPointLogin("http://login.strongholdkingdoms.com/bigpoint/iframelogin.php?lang=" + Program.mySettings.LanguageIdent.ToLower(), "", this, (sender, e) => AeriaToken = e.token);
        }

        public void openFacebookPopup()
        {
            FacebookWindow.ShowFacebookLogin("http://login.strongholdkingdoms.com/facebook/index.php?lang=" + Program.mySettings.LanguageIdent.ToLower(), "", this, (sender, e) => AeriaToken = e.token);
        }

        private void options_Click()
        {
            GameEngine.Instance.playInterfaceSound("Options_open");
            OptionsPopup.openSettingsLogin();
        }

        private void processVacationModeInfo(List<WorldInfo> worldList)
        {
            inSpecialWorld = false;
            specialWorldName = "";
            foreach (WorldInfo info in worldList)
            {
                if (isSpecialWorld(info.KingdomsWorldID) && info.Playing)
                {
                    inSpecialWorld = true;
                    specialWorldName = getWorldShortDesc(info);
                    break;
                }
            }
        }

        private void ProfileLoginWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.LoginPanelControls_Feedback != null)
            {
                this.LoginPanelControls_Feedback.PanelActive = false;
            }
            if (this.LoginPanelControls_LoggedIn != null)
            {
                this.LoginPanelControls_LoggedIn.PanelActive = false;
            }
            if (this.LoginPanelControls_LoggedOut != null)
            {
                this.LoginPanelControls_LoggedOut.PanelActive = false;
            }
            if (this.LoginPanelControls_Feedback != null)
            {
                this.LoginPanelControls_Feedback.PanelActive = false;
            }
            if (this.BrowserTabsControls != null)
            {
                this.BrowserTabsControls.PanelActive = false;
            }
            if (this.WorldsPanelcontrols_LoggedIn != null)
            {
                this.WorldsPanelcontrols_LoggedIn.PanelActive = false;
            }
            if (this.WorldsPanelcontrols_LoggedOut != null)
            {
                this.WorldsPanelcontrols_LoggedOut.PanelActive = false;
            }
            if (this.joinImage != null)
            {
                this.joinImage.Dispose();
            }
            if (this.joinImageOver != null)
            {
                this.joinImageOver.Dispose();
            }
            if (this.playImage != null)
            {
                this.playImage.Dispose();
            }
            if (this.playImageOver != null)
            {
                this.playImageOver.Dispose();
            }
            if (this.loginImage != null)
            {
                this.loginImage.Dispose();
            }
            if (this.logoutImage != null)
            {
                this.logoutImage.Dispose();
            }
            if (this.loginImageOver != null)
            {
                this.loginImageOver.Dispose();
            }
            if (this.closedImage != null)
            {
                this.closedImage.Dispose();
            }
            if (this.closedImageOver != null)
            {
                this.closedImageOver.Dispose();
            }
            if (this.logoutImageOver != null)
            {
                this.logoutImageOver.Dispose();
            }
            if (this.newsImage != null)
            {
                this.newsImage.Dispose();
            }
            if (this.newsImageOver != null)
            {
                this.newsImageOver.Dispose();
            }
            if (this.accountImage != null)
            {
                this.accountImage.Dispose();
            }
            if (this.accountImageOver != null)
            {
                this.accountImageOver.Dispose();
            }
            if (this.exitImage != null)
            {
                this.exitImage.Dispose();
            }
            if (this.exitImageOver != null)
            {
                this.exitImageOver.Dispose();
            }
            if (this.cancelImage != null)
            {
                this.cancelImage.Dispose();
            }
            if (this.cancelImageOver != null)
            {
                this.cancelImageOver.Dispose();
            }
            if (this.forgottenImage != null)
            {
                this.forgottenImage.Dispose();
            }
            if (this.forgottenImageOver != null)
            {
                this.forgottenImageOver.Dispose();
            }
            if (this.selectImage != null)
            {
                this.selectImage.Dispose();
            }
            if (this.selectImageOver != null)
            {
                this.selectImageOver.Dispose();
            }
            if (this.createAccountImage != null)
            {
                this.createAccountImage.Dispose();
            }
            if (this.createAccountImageOver != null)
            {
                this.createAccountImageOver.Dispose();
            }
            if (this.createAccountImage2 != null)
            {
                this.createAccountImage2.Dispose();
            }
            if (this.createAccountImageOver2 != null)
            {
                this.createAccountImageOver2.Dispose();
            }
            if (this.optionsImage != null)
            {
                this.optionsImage.Dispose();
            }
            if (this.optionsImageOver != null)
            {
                this.optionsImageOver.Dispose();
            }
            if ((this.GreyoutLogin != null) && (this.GreyoutLogin.Image != null))
            {
                this.GreyoutLogin.Image.Dispose();
                this.GreyoutLogin.Image = null;
            }
            if ((this.GreyoutTabs != null) && (this.GreyoutTabs.Image != null))
            {
                this.GreyoutTabs.Image.Dispose();
                this.GreyoutTabs.Image = null;
            }
            if ((this.GreyoutWorlds != null) && (this.GreyoutWorlds.Image != null))
            {
                this.GreyoutWorlds.Image.Dispose();
                this.GreyoutWorlds.Image = null;
            }
        }

        private void ProfileLoginWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !this.selfClosing)
            {
                GameEngine.Instance.appClosing();
                RemoteServices.Instance.UserID = -1;
            }
        }

        private void ProfileLoginWindow_Load(object sender, EventArgs e)
        {
            GeckoPreferences.User["general.useragent.override"] = this.Text;
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            Screen primaryScreen = Screen.PrimaryScreen;
            Point location = primaryScreen.WorkingArea.Location;
            location.X += (primaryScreen.WorkingArea.Width - base.Size.Width) / 2;
            location.Y += (primaryScreen.WorkingArea.Height - base.Size.Height) / 2;
            base.Location = location;
            RemoteServices.Instance.Admin = false;
            RemoteServices.Instance.MapEditor = false;
            RemoteServices.Instance.Moderator = false;
            this.lastLogoutClicked = DateTime.MinValue;
        }

        private void ProfileLoginWindow_LocationChanged(object sender, EventArgs e)
        {
            InterfaceMgr.Instance.moveGreyOutWindow();
            InterfaceMgr.Instance.moveCreatePopupWindow();
        }

        public void RefreshControls()
        {
            bool flag = this.isPlayerLoggedIn();
            this.LoginPanelControls_LoggedIn.Visible = flag;
            this.WorldsPanelcontrols_LoggedIn.Visible = flag;
            this.LoginPanelControls_LoggedOut.Visible = !flag;
            this.WorldsPanelcontrols_LoggedOut.Visible = !flag;
            this.ShowTabs();
        }

        private void RequestGameWorlds(string SessionID, string UserID)
        {
            this.loginButtonActive = false;
            IDictionary<string, string> postVars = new Dictionary<string, string>();
            postVars.Add("uid", UserID);
            postVars.Add("sid", SessionID);

            this.geckoWebBrowser1.Navigate(this.URL_gameWorldPage, postVars);
        }

        private void RespCallback(IAsyncResult ar)
        {
            try
            {
                RequestState asyncState = (RequestState) ar.AsyncState;
                Image image = Image.FromStream(asyncState.req.EndGetResponse(ar).GetResponseStream());
                ShieldImage["profile"] = image;
            }
            catch (Exception)
            {
                ShieldImage["profile"] = (Image) GFXLibrary.LoginShieldPlaceholder;
            }
            if (this.btnShieldDesigner != null)
            {
                this.SetShieldImage();
                this.btnShieldDesigner.invalidate();
            }
        }

        public void selfClose()
        {
            this.selfClosing = true;
        }

        public void SetEmailOptInState(bool state)
        {
            this.emailOptInState = state;
        }

        public void SetNonSteamEmail(string text, string password)
        {
            this.txtEmail.Text = text;
            this.txtPassword.Text = password;
            this.lblEmail.Visible = true;
        }

        public void SetShieldImage()
        {
            this.btnShieldDesigner.Image = ShieldImage["profile"];
            this.btnShieldDesigner.Width = this.btnShieldDesigner.Image.Width;
            this.btnShieldDesigner.Height = this.btnShieldDesigner.Image.Height;
            this.btnShieldDesigner.X = (this.pnlLogin.Width - this.btnShieldDesigner.Width) / 2;
            this.btnShieldDesigner.Y = this.pnlTabs.Bottom + 8;
            this.btnShieldDesigner.CustomTooltipID = 0xfaf;
        }

        public void SetSteamEmail(string text)
        {
            this.txtEmail.Text = text;
            this.lblEmailSteam.Text = text;
            this.lblEmail.Visible = true;
            this.lblPassword.Visible = false;
            this.txtPassword.Visible = false;
        }

        public void SetUsername()
        {
            this.lblUsername.Image = WebStyleButtonImage.GenerateLabel(200, 0x19, RemoteServices.Instance.UserName, ARGBColors.Black, ARGBColors.Transparent);
            this.lblUsername.Width = 200;
            this.lblUsername.Height = 0x19;
        }

        private void shieldDownloaded()
        {
            if (ShieldFactory.LastErrorString.Length <= 0)
            {
                ShieldImage["profile"] = GameEngine.Instance.World.getPlayerShieldImage(140, 0x9c);
            }
            if (ShieldImage["profile"] == null)
            {
                ShieldImage["profile"] = GameEngine.Instance.World.getDummyShield(140, 0x9c);
            }
            if (this.btnShieldDesigner != null)
            {
                this.SetShieldImage();
                this.btnShieldDesigner.invalidate();
            }
        }

        private void shkruClick()
        {
            new Process { StartInfo = { FileName = "www.vk.com/shkru" } }.Start();
        }

        public void ShowCreateUserForm()
        {
            InterfaceMgr.Instance.openCreatePopupWindow();
        }

        public void ShowOnlinePanels()
        {
            this.LoadShieldImages();
            LanguageList = new Dictionary<string, LocalizationLanguage>();
            foreach (WorldInfo info in WorldList)
            {
                if (!LanguageList.ContainsKey(info.Supportculture))
                {
                    LocalizationLanguage language = new LocalizationLanguage {
                        CultureCode = info.Supportculture
                    };
                    LanguageList.Add(info.Supportculture, language);
                }
            }
            this.SetUsername();
            this.SetShieldImage();
            List<WorldInfo> worldsBySupportCulture = this.GetWorldsBySupportCulture("");
            this.BuildOnlineWorldList(worldsBySupportCulture);
            this.RefreshControls();
            this.EnablePanels(true);
            this.WorldsPanelcontrols_LoggedIn.Invalidate();
            if ((this.chkAutoLogin != null) && this.chkAutoLogin.Checked)
            {
                foreach (WorldInfo info2 in worldsBySupportCulture)
                {
                    if ((info2.KingdomsWorldID == Program.mySettings.LastWorldID) && info2.Playing)
                    {
                        this.btnWorldAction_Click(info2);
                        break;
                    }
                }
            }
        }

        public void ShowTabs()
        {
            this.BrowserTabsControls.clearControls();
            bool flag = this.isPlayerLoggedIn();
            int num = 8;
            int num2 = this.BrowserTabsControls.Height - 0x1d;
            CustomSelfDrawPanel.CSDImage btnNews = new CustomSelfDrawPanel.CSDImage();
            this.allButtons.Add(btnNews);
            btnNews.Image = this.NewsImage;
            btnNews.Height = btnNews.Image.Height;
            btnNews.Width = btnNews.Image.Width;
            btnNews.X = num;
            btnNews.Y = num2;
            this.BrowserTabsControls.addControl(btnNews);
            num += btnNews.Width + 2;
            btnNews.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnNews_Click), "ProfileLoginWindow_news");
            btnNews.setMouseOverDelegate(() => btnNews.Image = this.NewsImageOver, () => btnNews.Image = this.NewsImage);
            if (!Program.bigpointPartnerInstall)
            {
                if (Program.mySettings.LanguageIdent == "ru")
                {
                    this.imgSHKRu.ImageNorm = (Image) GFXLibrary.vk;
                    this.imgSHKRu.OverBrighten = true;
                    this.imgSHKRu.Size = new Size(0x20, 0x20);
                    this.imgSHKRu.Position = new Point(0x10c, 5);
                    this.imgSHKRu.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.shkruClick));
                    this.BrowserTabsControls.addControl(this.imgSHKRu);
                    this.imgTwitter.ImageNorm = (Image) GFXLibrary.twitter;
                    this.imgTwitter.OverBrighten = true;
                    this.imgTwitter.Size = new Size(0x20, 0x20);
                    this.imgTwitter.Position = new Point(0x131, 5);
                    this.imgTwitter.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.twitterClick));
                    this.BrowserTabsControls.addControl(this.imgTwitter);
                    this.imgYoutube.ImageNorm = (Image) GFXLibrary.youtube;
                    this.imgYoutube.OverBrighten = true;
                    this.imgYoutube.Position = new Point(0x156, 5);
                    this.imgYoutube.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.youtubeClick));
                    this.BrowserTabsControls.addControl(this.imgYoutube);
                }
                else
                {
                    this.imgTwitter.ImageNorm = (Image) GFXLibrary.twitter;
                    this.imgTwitter.OverBrighten = true;
                    this.imgTwitter.Size = new Size(0x20, 0x20);
                    this.imgTwitter.Position = new Point(0x10c, 5);
                    this.imgTwitter.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.twitterClick));
                    this.BrowserTabsControls.addControl(this.imgTwitter);
                    this.imgFacebook.ImageNorm = (Image) GFXLibrary.facebook;
                    this.imgFacebook.OverBrighten = true;
                    this.imgFacebook.Size = new Size(0x20, 0x20);
                    this.imgFacebook.Position = new Point(0x131, 5);
                    this.imgFacebook.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.facebookClick));
                    this.BrowserTabsControls.addControl(this.imgFacebook);
                    this.imgYoutube.ImageNorm = (Image) GFXLibrary.youtube;
                    this.imgYoutube.OverBrighten = true;
                    this.imgYoutube.Position = new Point(0x156, 5);
                    this.imgYoutube.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.youtubeClick));
                    this.BrowserTabsControls.addControl(this.imgYoutube);
                }
            }
            if (flag)
            {
                CustomSelfDrawPanel.CSDImage btnAccountDetails = new CustomSelfDrawPanel.CSDImage();
                this.allButtons.Add(btnAccountDetails);
                btnAccountDetails.Image = this.AccountImage;
                btnAccountDetails.Height = btnAccountDetails.Image.Height;
                btnAccountDetails.Width = btnAccountDetails.Image.Width;
                btnAccountDetails.X = num;
                btnAccountDetails.Y = num2;
                this.BrowserTabsControls.addControl(btnAccountDetails);
                num += btnAccountDetails.Width + 2;
                btnAccountDetails.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAccountDetails_Click), "ProfileLoginWindow_account_details");
                btnAccountDetails.setMouseOverDelegate(() => btnAccountDetails.Image = this.AccountImageOver, () => btnAccountDetails.Image = this.AccountImage);
                CustomSelfDrawPanel.CSDImage btnCreateAccount = new CustomSelfDrawPanel.CSDImage();
                this.allButtons.Add(btnCreateAccount);
                btnCreateAccount.Image = this.OptionsImage;
                btnCreateAccount.X = 400;
                btnCreateAccount.Height = btnCreateAccount.Image.Height;
                btnCreateAccount.Width = btnCreateAccount.Image.Width;
                btnCreateAccount.Y = num2;
                this.BrowserTabsControls.addControl(btnCreateAccount);
                btnCreateAccount.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.options_Click), "ProfileLoginWindow_create_account");
                btnCreateAccount.setMouseOverDelegate(() => btnCreateAccount.Image = this.OptionsImageOver, () => btnCreateAccount.Image = this.OptionsImage);
            }
            else
            {
                if (((!Program.steamActive && !Program.aeriaInstall) && (!Program.gamersFirstInstall && !Program.arcInstall)) && (!Program.bigpointInstall && !Program.bigpointPartnerInstall))
                {
                    CustomSelfDrawPanel.CSDImage btnCreateAccount = new CustomSelfDrawPanel.CSDImage();
                    this.allButtons.Add(btnCreateAccount);
                    if ((!Program.mySettings.hasLoggedIn() && !Program.bigpointInstall) && (!Program.aeriaInstall && !Program.bigpointPartnerInstall))
                    {
                        btnCreateAccount.Image = this.CreateAccountImage;
                    }
                    else
                    {
                        btnCreateAccount.Image = this.CreateAccountImage2;
                    }
                    btnCreateAccount.X = 400;
                    btnCreateAccount.Height = btnCreateAccount.Image.Height;
                    btnCreateAccount.Width = btnCreateAccount.Image.Width;
                    btnCreateAccount.Y = num2;
                    this.BrowserTabsControls.addControl(btnCreateAccount);
                    btnCreateAccount.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnCreateAccount_Click), "ProfileLoginWindow_create_account");
                    btnCreateAccount.setMouseOverDelegate(delegate {
                        if ((!Program.mySettings.hasLoggedIn() && !Program.bigpointInstall) && (!Program.aeriaInstall && !Program.bigpointPartnerInstall))
                        {
                            btnCreateAccount.Image = this.CreateAccountImageOver;
                        }
                        else
                        {
                            btnCreateAccount.Image = this.CreateAccountImageOver2;
                        }
                    }, delegate {
                        if ((!Program.mySettings.hasLoggedIn() && !Program.bigpointInstall) && (!Program.aeriaInstall && !Program.bigpointPartnerInstall))
                        {
                            btnCreateAccount.Image = this.CreateAccountImage;
                        }
                        else
                        {
                            btnCreateAccount.Image = this.CreateAccountImage2;
                        }
                    });
                }
                if (((!Program.bigpointInstall && !Program.aeriaInstall) && (!Program.gamersFirstInstall && !Program.bigpointPartnerInstall)) && !Program.arcInstall)
                {
                    CustomSelfDrawPanel.CSDImage btnForgottenPassword = new CustomSelfDrawPanel.CSDImage();
                    this.allButtons.Add(btnForgottenPassword);
                    btnForgottenPassword.Image = this.ForgottenImage;
                    btnForgottenPassword.Height = btnForgottenPassword.Image.Height;
                    btnForgottenPassword.Width = btnForgottenPassword.Image.Width;
                    btnForgottenPassword.X = num;
                    btnForgottenPassword.Y = num2;
                    this.BrowserTabsControls.addControl(btnForgottenPassword);
                    num += btnForgottenPassword.Width + 2;
                    btnForgottenPassword.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnForgotten_Click), "ProfileLoginWindow_forgotten_password");
                    btnForgottenPassword.setMouseOverDelegate(() => btnForgottenPassword.Image = this.ForgottenImageOver, () => btnForgottenPassword.Image = this.forgottenImage);
                }
            }
        }

        public void ShowWorldSelect()
        {
            InterfaceMgr.Instance.openWorldSelectPopupWindow();
        }

        private void statusPageClicked()
        {
            try
            {
                new Process { StartInfo = { FileName = "http://slogin.strongholdkingdoms.com/status.php?lang=" + Program.mySettings.languageIdent } }.Start();
            }
            catch (Exception)
            {
            }
        }

        private void supportClicked()
        {
            try
            {
                Process process = new Process();
                if (RemoteServices.Instance.UserGuid == Guid.Empty)
                {
                    process.StartInfo.FileName = URLs.Supportpage;
                }
                else
                {
                    process.StartInfo.FileName = "http://login.strongholdkingdoms.com/support/?u=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&s=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "") + "&lang=" + Program.mySettings.languageIdent;
                }
                process.Start();
            }
            catch (Exception)
            {
            }
        }

        private void tbLoginPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void tbLoginPassword_TextChanged(object sender, EventArgs e)
        {
            this.manageLoginButton();
        }

        private void tbLoginUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void tbLoginUser_TextChanged(object sender, EventArgs e)
        {
            this.manageLoginButton();
        }

        private void tcClicked()
        {
            try
            {
                new Process { StartInfo = { FileName = URLs.TermsAndConditions } }.Start();
            }
            catch (Exception)
            {
            }
        }

        public void throwClientError(CustomSelfDrawPanel.CSDLabel l, string message)
        {
            l.Visible = true;
            l.Text = this.strGenericLoginError;
            l.X = 4;
            l.Y = this.LoginPanelControls_LoggedOut.Height - 30;
            l.Width = this.LoginPanelControls_LoggedOut.Width - 8;
            l.Height = 60;
            this.EnablePanels(true);
        }

        public void throwClientErrorConnection(CustomSelfDrawPanel.CSDLabel l, string message)
        {
            l.Visible = true;
            l.Text = this.strGenericLoginErrorConnection;
            l.X = 4;
            l.Y = this.LoginPanelControls_LoggedOut.Height - 30;
            l.Width = this.LoginPanelControls_LoggedOut.Width - 8;
            l.Height = 60;
            this.EnablePanels(true);
        }

        private void twitterClick()
        {
            new Process { StartInfo = { FileName = URLs.TwitterURL } }.Start();
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Enter))
            {
                if (this.btnLogin.Enabled)
                {
                    GameEngine.Instance.playInterfaceSound("ProfileLoginWindow_login");
                    this.btnLogin_Click();
                }
                e.Handled = true;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.btnLogin.Enabled)
                {
                    GameEngine.Instance.playInterfaceSound("ProfileLoginWindow_login");
                    this.btnLogin_Click();
                }
                e.Handled = true;
            }
        }

        private void txtLoginField_Validate(object sender, EventArgs e)
        {
            this.btnLogin.Enabled = (this.txtEmail.TextLength > 0) && (this.txtPassword.TextLength > 0);
        }

        private void txtLoginField_Validate_email(object sender, EventArgs e)
        {
            this.btnLogin.Enabled = (this.txtEmail.TextLength > 0) && (this.txtPassword.TextLength > 0);
            if ((this.chkAutoLogin != null) && !this.ignoreEmailChange)
            {
                this.chkAutoLogin.Checked = false;
                Program.mySettings.AutoLogin = false;
            }
        }

        public void update()
        {
            if (this.initialisedLanguage != Program.mySettings.LanguageIdent)
            {
                if (this.joinImage != null)
                {
                    this.joinImage.Dispose();
                }
                if (this.joinImageOver != null)
                {
                    this.joinImageOver.Dispose();
                }
                if (this.playImage != null)
                {
                    this.playImage.Dispose();
                }
                if (this.playImageOver != null)
                {
                    this.playImageOver.Dispose();
                }
                if (this.loginImage != null)
                {
                    this.loginImage.Dispose();
                }
                if (this.logoutImage != null)
                {
                    this.logoutImage.Dispose();
                }
                if (this.loginImageOver != null)
                {
                    this.loginImageOver.Dispose();
                }
                if (this.closedImage != null)
                {
                    this.closedImage.Dispose();
                }
                if (this.closedImageOver != null)
                {
                    this.closedImageOver.Dispose();
                }
                if (this.logoutImageOver != null)
                {
                    this.logoutImageOver.Dispose();
                }
                if (this.newsImage != null)
                {
                    this.newsImage.Dispose();
                }
                if (this.newsImageOver != null)
                {
                    this.newsImageOver.Dispose();
                }
                if (this.accountImage != null)
                {
                    this.accountImage.Dispose();
                }
                if (this.accountImageOver != null)
                {
                    this.accountImageOver.Dispose();
                }
                if (this.exitImage != null)
                {
                    this.exitImage.Dispose();
                }
                if (this.exitImageOver != null)
                {
                    this.exitImageOver.Dispose();
                }
                if (this.cancelImage != null)
                {
                    this.cancelImage.Dispose();
                }
                if (this.cancelImageOver != null)
                {
                    this.cancelImageOver.Dispose();
                }
                if (this.forgottenImage != null)
                {
                    this.forgottenImage.Dispose();
                }
                if (this.forgottenImageOver != null)
                {
                    this.forgottenImageOver.Dispose();
                }
                if (this.selectImage != null)
                {
                    this.selectImage.Dispose();
                }
                if (this.selectImageOver != null)
                {
                    this.selectImageOver.Dispose();
                }
                if (this.createAccountImage != null)
                {
                    this.createAccountImage.Dispose();
                }
                if (this.createAccountImageOver != null)
                {
                    this.createAccountImageOver.Dispose();
                }
                if (this.createAccountImage2 != null)
                {
                    this.createAccountImage2.Dispose();
                }
                if (this.createAccountImageOver2 != null)
                {
                    this.createAccountImageOver2.Dispose();
                }
                if (this.optionsImage != null)
                {
                    this.optionsImage.Dispose();
                }
                if (this.optionsImageOver != null)
                {
                    this.optionsImageOver.Dispose();
                }
                this.joinImage = this.joinImageOver = this.playImage = this.playImageOver = this.loginImage = this.logoutImage = this.loginImageOver = this.closedImage = this.closedImageOver = this.logoutImageOver = this.newsImage = this.newsImageOver = this.accountImage = this.accountImageOver = this.exitImage = this.exitImageOver = this.cancelImage = this.cancelImageOver = this.forgottenImage = this.forgottenImageOver = this.selectImage = this.selectImageOver = this.createAccountImage = this.createAccountImageOver = this.createAccountImage2 = this.createAccountImageOver2 = this.optionsImage = (Image) (this.optionsImageOver = null);
                if (WorldSelectPopupPanel.closeImage != null)
                {
                    WorldSelectPopupPanel.closeImage.Dispose();
                }
                if (WorldSelectPopupPanel.closeImageOver != null)
                {
                    WorldSelectPopupPanel.closeImageOver.Dispose();
                }
                if (WorldSelectPopupPanel.selectImageSelected != null)
                {
                    WorldSelectPopupPanel.selectImageSelected.Dispose();
                }
                if (WorldSelectPopupPanel.selectImage != null)
                {
                    WorldSelectPopupPanel.selectImage.Dispose();
                }
                if (WorldSelectPopupPanel.selectImageOver != null)
                {
                    WorldSelectPopupPanel.selectImageOver.Dispose();
                }
                if (WorldSelectPopupPanel.selectSpecialImage != null)
                {
                    WorldSelectPopupPanel.selectSpecialImage.Dispose();
                }
                if (WorldSelectPopupPanel.selectSpecialImageSelected != null)
                {
                    WorldSelectPopupPanel.selectSpecialImageSelected.Dispose();
                }
                if (WorldSelectPopupPanel.selectSpecialImageOver != null)
                {
                    WorldSelectPopupPanel.selectSpecialImageOver.Dispose();
                }
                if (WorldSelectPopupPanel.selectAIImage != null)
                {
                    WorldSelectPopupPanel.selectAIImage.Dispose();
                }
                if (WorldSelectPopupPanel.selectAIImageSelected != null)
                {
                    WorldSelectPopupPanel.selectAIImageSelected.Dispose();
                }
                if (WorldSelectPopupPanel.selectAIImageOver != null)
                {
                    WorldSelectPopupPanel.selectAIImageOver.Dispose();
                }
                if (WorldSelectPopupPanel.joinImage != null)
                {
                    WorldSelectPopupPanel.joinImage.Dispose();
                }
                if (WorldSelectPopupPanel.joinImageOver != null)
                {
                    WorldSelectPopupPanel.joinImageOver.Dispose();
                }
                if (WorldSelectPopupPanel.playImage != null)
                {
                    WorldSelectPopupPanel.playImage.Dispose();
                }
                if (WorldSelectPopupPanel.playImageOver != null)
                {
                    WorldSelectPopupPanel.playImageOver.Dispose();
                }
                if (WorldSelectPopupPanel.closedImage != null)
                {
                    WorldSelectPopupPanel.closedImage.Dispose();
                }
                WorldSelectPopupPanel.closeImage = WorldSelectPopupPanel.closeImageOver = WorldSelectPopupPanel.selectImageSelected = WorldSelectPopupPanel.selectImage = WorldSelectPopupPanel.selectImageOver = WorldSelectPopupPanel.selectSpecialImage = WorldSelectPopupPanel.selectSpecialImageSelected = WorldSelectPopupPanel.selectSpecialImageOver = WorldSelectPopupPanel.selectAIImage = WorldSelectPopupPanel.selectAIImageSelected = WorldSelectPopupPanel.selectAIImageOver = WorldSelectPopupPanel.joinImage = WorldSelectPopupPanel.joinImageOver = WorldSelectPopupPanel.playImage = WorldSelectPopupPanel.playImageOver = (Image) (WorldSelectPopupPanel.closedImage = null);
                if (BPPopupPanel.closeImage != null)
                {
                    BPPopupPanel.closeImage.Dispose();
                }
                if (BPPopupPanel.closeImageOver != null)
                {
                    BPPopupPanel.closeImageOver.Dispose();
                }
                if (BPPopupPanel.completeImage != null)
                {
                    BPPopupPanel.completeImage.Dispose();
                }
                if (BPPopupPanel.completeImageOver != null)
                {
                    BPPopupPanel.completeImageOver.Dispose();
                }
                BPPopupPanel.closeImage = BPPopupPanel.closeImageOver = BPPopupPanel.completeImage = (Image) (BPPopupPanel.completeImageOver = null);
                if (CreatePopupPanel.nextImage != null)
                {
                    CreatePopupPanel.nextImage.Dispose();
                }
                if (CreatePopupPanel.nextImageOver != null)
                {
                    CreatePopupPanel.nextImageOver.Dispose();
                }
                if (CreatePopupPanel.headerImage != null)
                {
                    CreatePopupPanel.headerImage.Dispose();
                }
                if (CreatePopupPanel.headerTransferImage != null)
                {
                    CreatePopupPanel.headerTransferImage.Dispose();
                }
                if (CreatePopupPanel.closeImage != null)
                {
                    CreatePopupPanel.closeImage.Dispose();
                }
                if (CreatePopupPanel.closeImageOver != null)
                {
                    CreatePopupPanel.closeImageOver.Dispose();
                }
                if (CreatePopupPanel.transferImage != null)
                {
                    CreatePopupPanel.transferImage.Dispose();
                }
                if (CreatePopupPanel.transferImageOver != null)
                {
                    CreatePopupPanel.transferImageOver.Dispose();
                }
                CreatePopupPanel.nextImage = CreatePopupPanel.nextImageOver = CreatePopupPanel.headerImage = CreatePopupPanel.headerTransferImage = CreatePopupPanel.closeImage = CreatePopupPanel.closeImageOver = CreatePopupPanel.transferImage = (Image) (CreatePopupPanel.transferImageOver = null);
                if (VacationCancelPopupPanel.cancelImageOver != null)
                {
                    VacationCancelPopupPanel.cancelImageOver.Dispose();
                }
                if (VacationCancelPopupPanel.closeImage != null)
                {
                    VacationCancelPopupPanel.closeImage.Dispose();
                }
                if (VacationCancelPopupPanel.closeImageOver != null)
                {
                    VacationCancelPopupPanel.closeImageOver.Dispose();
                }
                if (VacationCancelPopupPanel.headerImage != null)
                {
                    VacationCancelPopupPanel.headerImage.Dispose();
                }
                if (VacationCancelPopupPanel.cancelImage != null)
                {
                    VacationCancelPopupPanel.cancelImage.Dispose();
                }
                VacationCancelPopupPanel.closeImage = VacationCancelPopupPanel.closeImageOver = VacationCancelPopupPanel.headerImage = VacationCancelPopupPanel.cancelImage = (Image) (VacationCancelPopupPanel.cancelImageOver = null);
                if (this.chkAutoLogin != null)
                {
                    this.chkAutoLogin.Checked = false;
                }
                this.init();
                if (this.chkAutoLogin != null)
                {
                    this.chkAutoLogin.Checked = false;
                }
                base.Invalidate();
            }
            else if (this.delayedCreateUserOpen)
            {
                this.delayedCreateUserOpen = false;
                this.ShowCreateUserForm();
            }
            else
            {
                if (this.emailOptInPopup)
                {
                    this.emailOptInPopup = false;
                    new EmailOptInPopup { m_Parent = this }.ShowDialog(Program.profileLogin);
                    int num = 0;
                    if (this.emailOptInState)
                    {
                        num = 1;
                    }
                    XmlRpcAuthProvider provider = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
                    XmlRpcAuthRequest req = new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), "", "", "", 0, 0, new int?(num));
                    provider.SetEmailOptIn(req, new AuthEndResponseDelegate(this.EmailOptInCallback), this);
                }
                InterfaceMgr.Instance.runTooltips();
                if (this.lastLogoutClicked != DateTime.MinValue)
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - this.lastLogoutClicked);
                    if (span.TotalSeconds > 60.0)
                    {
                        this.lastLogoutClicked = DateTime.MinValue;
                    }
                }
                int downloadingCounter = GameEngine.Instance.World.downloadingCounter;
                if (downloadingCounter != this.lastCount)
                {
                    string str = SK.Text("ConnectingPopup_Retrieving_Data", "Retrieving Data From Server.");
                    for (int i = 0; i < downloadingCounter; i++)
                    {
                        str = str + ".";
                    }
                    this.lblRetrieving.Text = str;
                    if (this.lblRetrieving.Visible)
                    {
                        this.Text = this.defaultWindowTitle + " - " + this.lblRetrieving.Text;
                    }
                    this.lastCount = downloadingCounter;
                    if (this.lblRetrieving.Visible)
                    {
                        this.feedbackProgress.Size = new Size(Math.Min((this.pnlFeedback.Width * (downloadingCounter + 2)) / 12, this.pnlFeedback.Width), this.pnlFeedback.Height);
                    }
                    else
                    {
                        this.feedbackProgress.Size = new Size(0, this.pnlFeedback.Height);
                    }
                    this.feedbackProgressArea.invalidate();
                }
                if ((InterfaceMgr.Instance.isCreatePopup() || InterfaceMgr.Instance.isVacationCancelPopupWindow()) || (InterfaceMgr.Instance.isBPPopup() || InterfaceMgr.Instance.isWorldSelectPopup()))
                {
                    InterfaceMgr.Instance.updatePopups();
                }
            }
        }

        private void youtubeClick()
        {
            new Process { StartInfo = { FileName = URLs.YoutubeURL } }.Start();
        }

        public Image AccountImage
        {
            get
            {
                if (this.accountImage == null)
                {
                    this.accountImage = WebStyleButtonImage.Generate(130, this.WebButtonheight, this.strAccountDetails, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return this.accountImage;
            }
        }

        public Image AccountImageOver
        {
            get
            {
                if (this.accountImageOver == null)
                {
                    this.accountImageOver = WebStyleButtonImage.Generate(130, this.WebButtonheight, this.strAccountDetails, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return this.accountImageOver;
            }
        }

        public Image CancelImage
        {
            get
            {
                if (this.cancelImage == null)
                {
                    this.cancelImage = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strCancel, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return this.cancelImage;
            }
        }

        public Image CancelImageOver
        {
            get
            {
                if (this.cancelImageOver == null)
                {
                    this.cancelImageOver = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strCancel, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return this.cancelImageOver;
            }
        }

        public Image ClosedImage
        {
            get
            {
                if (this.closedImage == null)
                {
                    this.closedImage = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strClosed, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonRed, this.WebButtonRadius);
                }
                return this.closedImage;
            }
        }

        public Image CreateAccountImage
        {
            get
            {
                if (this.createAccountImage == null)
                {
                    this.createAccountImage = WebStyleButtonImage.Generate(250, this.WebButtonheight, this.strCreateAccount, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return this.createAccountImage;
            }
        }

        public Image CreateAccountImage2
        {
            get
            {
                if (this.createAccountImage2 == null)
                {
                    this.createAccountImage2 = WebStyleButtonImage.Generate(250, this.WebButtonheight, "StrongholdKingdoms.com", this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return this.createAccountImage2;
            }
        }

        public Image CreateAccountImageOver
        {
            get
            {
                if (this.createAccountImageOver == null)
                {
                    this.createAccountImageOver = WebStyleButtonImage.Generate(250, this.WebButtonheight, this.strCreateAccount, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return this.createAccountImageOver;
            }
        }

        public Image CreateAccountImageOver2
        {
            get
            {
                if (this.createAccountImageOver2 == null)
                {
                    this.createAccountImageOver2 = WebStyleButtonImage.Generate(250, this.WebButtonheight, "StrongholdKingdoms.com", this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return this.createAccountImageOver2;
            }
        }

        public Image ExitImage
        {
            get
            {
                if (this.exitImage == null)
                {
                    this.exitImage = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strExit, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return this.exitImage;
            }
        }

        public Image ExitImageOver
        {
            get
            {
                if (this.exitImageOver == null)
                {
                    this.exitImageOver = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strExit, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return this.exitImageOver;
            }
        }

        public Image ForgottenImage
        {
            get
            {
                if (this.forgottenImage == null)
                {
                    this.forgottenImage = WebStyleButtonImage.Generate(150, this.WebButtonheight, this.strForgottenPassword, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return this.forgottenImage;
            }
        }

        public Image ForgottenImageOver
        {
            get
            {
                if (this.forgottenImageOver == null)
                {
                    this.forgottenImageOver = WebStyleButtonImage.Generate(150, this.WebButtonheight, this.strForgottenPassword, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return this.forgottenImageOver;
            }
        }

        public Image JoinImage
        {
            get
            {
                if (this.joinImage == null)
                {
                    this.joinImage = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strJoin, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return this.joinImage;
            }
        }

        public Image JoinImageOver
        {
            get
            {
                if (this.joinImageOver == null)
                {
                    this.joinImageOver = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strJoin, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return this.joinImageOver;
            }
        }

        public Image LoginImage
        {
            get
            {
                if (this.loginImage == null)
                {
                    this.loginImage = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strLogin, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return this.loginImage;
            }
        }

        public Image LoginImageOver
        {
            get
            {
                if (this.loginImageOver == null)
                {
                    this.loginImageOver = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strLogin, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return this.loginImageOver;
            }
        }

        public Image LogoutImage
        {
            get
            {
                if (this.logoutImage == null)
                {
                    this.logoutImage = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strLogout, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return this.logoutImage;
            }
        }

        public Image LogoutImageOver
        {
            get
            {
                if (this.logoutImageOver == null)
                {
                    this.logoutImageOver = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strLogout, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return this.logoutImageOver;
            }
        }

        public Image NewsImage
        {
            get
            {
                if (this.newsImage == null)
                {
                    this.newsImage = WebStyleButtonImage.Generate(80, this.WebButtonheight, this.strNews, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return this.newsImage;
            }
        }

        public Image NewsImageOver
        {
            get
            {
                if (this.newsImageOver == null)
                {
                    this.newsImageOver = WebStyleButtonImage.Generate(80, this.WebButtonheight, this.strNews, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return this.newsImageOver;
            }
        }

        public Image OptionsImage
        {
            get
            {
                if (this.optionsImage == null)
                {
                    this.optionsImage = WebStyleButtonImage.Generate(250, this.WebButtonheight, this.strOptions, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return this.optionsImage;
            }
        }

        public Image OptionsImageOver
        {
            get
            {
                if (this.optionsImageOver == null)
                {
                    this.optionsImageOver = WebStyleButtonImage.Generate(250, this.WebButtonheight, this.strOptions, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return this.optionsImageOver;
            }
        }

        public Image PlayImage
        {
            get
            {
                if (this.playImage == null)
                {
                    this.playImage = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strPlay, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return this.playImage;
            }
        }

        public Image PlayImageOver
        {
            get
            {
                if (this.playImageOver == null)
                {
                    this.playImageOver = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strPlay, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return this.playImageOver;
            }
        }

        public Image SelectImage
        {
            get
            {
                if (this.selectImage == null)
                {
                    this.selectImage = WebStyleButtonImage.Generate((this.WebButtonWidth * 2) + 100, this.WebButtonheight, this.strSelect, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return this.selectImage;
            }
        }

        public Image SelectImageOver
        {
            get
            {
                if (this.selectImageOver == null)
                {
                    this.selectImageOver = WebStyleButtonImage.Generate((this.WebButtonWidth * 2) + 100, this.WebButtonheight, this.strSelect, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return this.selectImageOver;
            }
        }

        private class RequestState
        {
            public WebRequest req;
        }
    }
}

