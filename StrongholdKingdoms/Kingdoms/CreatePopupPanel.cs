namespace Kingdoms
{
    using CommonTypes;
    using Stronghold.AuthClient;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class CreatePopupPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDLabel alreadyLabel;
        public static Image closeImage;
        public static Image closeImageOver;
        private IContainer components;
        private bool emailconfirmvalid;
        private string emailPattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$";
        private bool emailValid;
        private CustomSelfDrawPanel.CSDLabel FeedbackLabel;
        private CustomSelfDrawPanel.CSDFill fillEmailConfirmValid;
        private CustomSelfDrawPanel.CSDFill fillEmailValid;
        private CustomSelfDrawPanel.CSDFill fillPasswordConfirmValid;
        private CustomSelfDrawPanel.CSDFill fillPasswordValid;
        private CustomSelfDrawPanel.CSDFill fillUsernameValid;
        public static Image headerImage;
        private CustomSelfDrawPanel.CSDImage HeaderTitle;
        public static Image headerTransferImage;
        private CustomSelfDrawPanel.CSDImage HintBox;
        private static Image HintBoxImage;
        private CustomSelfDrawPanel.CSDLabel HintBoxLabel;
        private string lastUsernameChecked = string.Empty;
        private bool lastUsernameValid;
        private CustomSelfDrawPanel.CSDLabel lblEmail;
        private CustomSelfDrawPanel.CSDLabel lblEmailconfirm;
        private CustomSelfDrawPanel.CSDLabel lblPassword;
        private CustomSelfDrawPanel.CSDLabel lblPasswordconfirm;
        private CustomSelfDrawPanel.CSDLabel lblUsername;
        private bool m_createMode = true;
        private CustomSelfDrawPanel.CSDCheckBox newsletterCheck;
        private CustomSelfDrawPanel.CSDImage NextButton;
        public static Image nextImage;
        public static Image nextImageOver;
        private bool passwordconfirmvalid;
        private bool passwordvalid;
        private CustomSelfDrawPanel.CSDLabel privacyLabel;
        private string strBack = SK.Text("FORUMS_Back", "Back");
        private string strClose = SK.Text("GENERIC_Close", "Close");
        private string strMerge = SK.Text("SIGNUP_MERGE", "Transfer Account");
        private string strNext = SK.Text("LOGIN_CreateAccount", "Create Account");
        private CustomSelfDrawPanel.CSDLabel tandcLabel;
        private string TextCreateHeader = SK.Text("SIGNUP_header", "Create Your Stronghold Kingdoms Account");
        private string TextCreateHeaderMerge = SK.Text("SIGNUP_header_merge", "Transfer Your Stronghold Kingdoms Account To Steam");
        private string TextEmail = SK.Text("SIGNUP_your_email", "Your email address");
        private string TextEmailConfirm = SK.Text("SIGNUP_confirm_your_email", "Confirm your email address");
        private string TextHint_Email = SK.Text("SIGNUP_Hint_Email", "This must be a valid email address in the form user@domain.com, your Stronghold account will be linked to this email address and you will be sent an email to confirm your identity.");
        private string TextHint_Email_Merge = SK.Text("SIGNUP_Hint_Email_Merge", "Please enter your current Stronghold Kingdoms Email Address");
        private string TextHint_EmailConfirm = SK.Text("SIGNUP_Hint_EmailConfirm", "Please confirm your email address above by typing it again here.");
        private string TextHint_Password = SK.Text("SIGNUP_Hint_Password", "Please enter your desired password. Your password may be anything you like. For better security please consider a strong password containing both upper and lower case letters and at least one digit. The password must be between 5-25 characters long.");
        private string TextHint_Password_Merge = SK.Text("SIGNUP_Hint_Password_Merge", "Please enter your current Stronghold Kingdoms password.");
        private string TextHint_PasswordConfirm = SK.Text("SIGNUP_Hint_PasswordConfirm", "Please confirm your chosen password by typing it again here.");
        private string TextHint_Username = SK.Text("SIGNUP_Hint_Username", "Please enter your preferred username. this will be your username in Stronghold Kingdoms and in any related web forums. A username must be at least 4 characters long and must be unique.");
        private string TextPassword = SK.Text("SIGNUP_password", "Choose a Password");
        private string TextPasswordConfirm = SK.Text("SIGNUP_confirm_password", "Confirm Password");
        private string TextPasswordMerge = SK.Text("SIGNUP_password_merge", "Enter your Password");
        private string TextUsername = SK.Text("SIGNUP_username", "Choose a Username");
        public static Image transferImage;
        public static Image transferImageOver;
        private TextBox txtEmail;
        private TextBox txtEmailconfirm;
        private TextBox txtPassword;
        private TextBox txtPasswordconfirm;
        private TextBox txtUsername;
        private bool usernameNotChecked;
        private string usernamePattern = "^[A-Za-z0-9 ._%+-]+$";
        private bool usernameValidationInProgress;
        private Color WebButtonblue = Color.FromArgb(0x55, 0x91, 0xcb);
        private Color WebButtonGrey = Color.FromArgb(0xe1, 0xe1, 0xe1);
        private int WebButtonheight = 0x16;
        private int WebButtonRadius = 10;
        private Color WebButtonRed = Color.FromArgb(160, 0, 0);
        private Color WebButtonYellow = Color.FromArgb(0xe1, 0xe1, 0);
        private Font WebTextFontBold = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-Bold.ttf", 10f, FontStyle.Bold);
        private Font WebTextFontBoldCond = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 10f, FontStyle.Bold);

        public CreatePopupPanel()
        {
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void AddControlToPanel(CustomSelfDrawPanel.CSDControl c)
        {
            base.addControl(c);
        }

        private void closeClick()
        {
            if ((!Program.steamActive && !Program.gamersFirstInstall) && !Program.arcInstall)
            {
                InterfaceMgr.Instance.closeCreatePopupWindow();
            }
            else
            {
                this.init(true);
            }
        }

        private void CreateUser()
        {
            XmlRpcAuthProvider provider = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
            XmlRpcAuthRequest req = new XmlRpcAuthRequest();
            if (Program.gamersFirstInstall)
            {
                req.SteamID = "gamersfirst";
                req.EmailAddress = "";
                req.Password = Program.gamersFirstTokenMD5;
            }
            else if (Program.arcInstall)
            {
                req.SteamID = "arc";
                req.EmailAddress = Program.arcUsername;
                req.Password = Program.getNewArcToken();
                Program.arcToken = "";
            }
            else
            {
                req.SteamID = Program.steamID;
                req.EmailAddress = this.txtEmail.Text;
                req.Password = this.txtPassword.Text;
                if (this.newsletterCheck.Checked)
                {
                    req.ParishID = 100;
                }
                else
                {
                    req.ParishID = 50;
                }
            }
            req.Culture = Program.mySettings.LanguageIdent.ToLower();
            req.Username = this.lastUsernameChecked;
            this.NextButton.Image = this.NextImageOver;
            this.NextButton.Enabled = false;
            this.txtEmail.Enabled = false;
            this.txtEmailconfirm.Enabled = false;
            this.txtUsername.Enabled = false;
            this.txtPassword.Enabled = false;
            this.txtPasswordconfirm.Enabled = false;
            provider.CreateUserSteam(req, new AuthEndResponseDelegate(this.createUserCallback), this);
        }

        private void createUserCallback(IAuthProvider sender, IAuthResponse response)
        {
            if (response.SuccessCode == 1)
            {
                Program.kingdomsAccountFound = true;
                if (Program.steamActive)
                {
                    Program.profileLogin.SetSteamEmail(this.txtEmail.Text);
                }
                else
                {
                    Program.profileLogin.SetNonSteamEmail(this.txtEmail.Text, this.txtPassword.Text);
                }
                Program.profileLogin.btnLogin_Click();
                InterfaceMgr.Instance.closeCreatePopupWindow();
                if ((!Program.steamActive && !Program.gamersFirstInstall) && !Program.arcInstall)
                {
                    MyMessageBox.Show(SK.Text("Login_Account_Created_Message", "Your account has been created and you will receive an Authorization Email soon. Follow the instructions in this email and then you will be able to join game worlds in Stronghold Kingdoms."), SK.Text("Login_Account_Created", "Account Created"));
                }
            }
            else
            {
                this.FeedbackLabel.Color = ARGBColors.Red;
                this.FeedbackLabel.TextDiffOnly = response.Message;
                this.NextButton.Enabled = true;
                this.NextButton.Image = this.NextImage;
                this.txtEmail.Enabled = true;
                this.txtEmailconfirm.Enabled = true;
                this.txtUsername.Enabled = true;
                this.txtPassword.Enabled = true;
                this.txtPasswordconfirm.Enabled = true;
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

        private bool everythingValid()
        {
            if (this.m_createMode)
            {
                if (!this.passwordconfirmvalid)
                {
                    return false;
                }
                if (!this.emailValid)
                {
                    return false;
                }
                if (this.usernameValidationInProgress)
                {
                    return false;
                }
                if (!this.lastUsernameValid)
                {
                    return false;
                }
                if (!this.passwordvalid)
                {
                    return false;
                }
                if (!this.passwordconfirmvalid)
                {
                    return false;
                }
                return true;
            }
            return ((this.txtEmail.Text.Length > 0) && (this.txtPassword.Text.Length > 0));
        }

        public void init(bool createMode)
        {
            CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate overDelegate = null;
            CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate leaveDelegate = null;
            CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate delegate4 = null;
            CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate delegate5 = null;
            CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate newDelegate = null;
            CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate delegate7 = null;
            CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate delegate8 = null;
            this.m_createMode = createMode;
            base.clearControls();
            base.Controls.Clear();
            this.BackColor = ARGBColors.White;
            int num = 0;
            int num2 = 0;
            if (!Program.steamActive)
            {
                num2 = -15;
            }
            else if (!this.m_createMode)
            {
                num2 = 30;
            }
            if (Program.gamersFirstInstall)
            {
                this.TextEmail = SK.Text("SIGNUP_GF_Email", "GamersFirst Gamer ID / Email");
                this.TextUsername = SK.Text("SIGNUP_GF_Username", "Choose a Stronghold Kingdoms Username");
            }
            else if (Program.arcInstall)
            {
                this.TextEmail = SK.Text("SIGNUP_Arc_Email", "Arc ID");
                this.TextUsername = SK.Text("SIGNUP_GF_Username", "Choose a Stronghold Kingdoms Username");
            }
            else if (this.m_createMode)
            {
                num = -20;
            }
            int x = 200;
            int num4 = (100 + num2) + num;
            int num5 = 0x17;
            int num6 = 14;
            int height = 12;
            this.HeaderTitle = new CustomSelfDrawPanel.CSDImage();
            if (this.m_createMode)
            {
                this.HeaderTitle.Image = this.HeaderImage;
            }
            else
            {
                this.HeaderTitle.Image = this.HeaderTranferImage;
            }
            this.HeaderTitle.Position = new Point((base.Width - this.HeaderTitle.Width) / 2, 0x20 + num2);
            this.lblEmail = new CustomSelfDrawPanel.CSDLabel();
            this.lblEmail.Position = new Point(x, num4 - 10);
            this.lblEmail.Text = this.TextEmail;
            this.lblEmail.Size = new Size(300, height);
            this.lblEmail.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.txtEmail = new TextBox();
            this.txtEmail.ForeColor = ARGBColors.Black;
            this.txtEmail.BackColor = ARGBColors.White;
            this.txtEmail.Location = new Point(x, (num4 + num6) - 10);
            this.txtEmail.Size = new Size(300, this.txtEmail.Height);
            this.fillEmailValid = new CustomSelfDrawPanel.CSDFill();
            this.fillEmailValid.Position = new Point((x + 300) + 5, this.txtEmail.Location.Y);
            this.fillEmailValid.Size = new Size(this.txtEmail.Height, this.txtEmail.Height);
            this.fillEmailValid.FillColor = ARGBColors.Red;
            if (this.m_createMode)
            {
                this.lblEmailconfirm = new CustomSelfDrawPanel.CSDLabel();
                this.lblEmailconfirm.Position = new Point(this.txtEmail.Location.X, this.txtEmail.Location.Y + num5);
                this.lblEmailconfirm.Text = this.TextEmailConfirm;
                this.lblEmailconfirm.Size = new Size(300, height);
                this.lblEmailconfirm.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.txtEmailconfirm = new TextBox();
                this.txtEmailconfirm.ForeColor = ARGBColors.Black;
                this.txtEmailconfirm.BackColor = ARGBColors.White;
                this.txtEmailconfirm.Location = new Point(this.lblEmailconfirm.Position.X, this.lblEmailconfirm.Position.Y + num6);
                this.txtEmailconfirm.Size = new Size(300, this.txtEmailconfirm.Height);
                this.fillEmailConfirmValid = new CustomSelfDrawPanel.CSDFill();
                this.fillEmailConfirmValid.Position = new Point((x + 300) + 5, this.txtEmailconfirm.Location.Y);
                this.fillEmailConfirmValid.Size = new Size(this.txtEmailconfirm.Height, this.txtEmailconfirm.Height);
                this.fillEmailConfirmValid.FillColor = ARGBColors.Red;
                if (Program.gamersFirstInstall || Program.arcInstall)
                {
                    this.lblEmailconfirm.Visible = false;
                    this.txtEmailconfirm.Visible = false;
                    this.fillEmailConfirmValid.Visible = false;
                    this.txtEmail.Visible = false;
                    this.lblEmail.Visible = false;
                }
                this.lblUsername = new CustomSelfDrawPanel.CSDLabel();
                this.lblUsername.Position = new Point(this.txtEmailconfirm.Location.X, this.txtEmailconfirm.Location.Y + num5);
                this.lblUsername.Text = this.TextUsername;
                this.lblUsername.Size = new Size(300, height);
                this.lblUsername.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.txtUsername = new TextBox();
                this.txtUsername.ForeColor = ARGBColors.Black;
                this.txtUsername.BackColor = ARGBColors.White;
                this.txtUsername.Location = new Point(this.lblUsername.Position.X, this.lblUsername.Position.Y + num6);
                this.txtUsername.Size = new Size(300, this.txtUsername.Height);
                this.fillUsernameValid = new CustomSelfDrawPanel.CSDFill();
                this.fillUsernameValid.Position = new Point((x + 300) + 5, this.txtUsername.Location.Y);
                this.fillUsernameValid.Size = new Size(this.txtUsername.Height, this.txtUsername.Height);
                this.fillUsernameValid.FillColor = ARGBColors.Red;
            }
            this.lblPassword = new CustomSelfDrawPanel.CSDLabel();
            if (this.m_createMode)
            {
                this.lblPassword.Position = new Point(this.txtUsername.Location.X, this.txtUsername.Location.Y + num5);
                this.lblPassword.Text = this.TextPassword;
            }
            else
            {
                this.lblPassword.Position = new Point(this.txtEmail.Location.X, this.txtEmail.Location.Y + num5);
                this.lblPassword.Text = this.TextPasswordMerge;
            }
            this.lblPassword.Size = new Size(300, height);
            this.lblPassword.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.txtPassword = new TextBox();
            this.txtPassword.ForeColor = ARGBColors.Black;
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.BackColor = ARGBColors.White;
            this.txtPassword.Location = new Point(this.lblPassword.Position.X, this.lblPassword.Position.Y + num6);
            this.txtPassword.Size = new Size(300, this.txtPassword.Height);
            this.fillPasswordValid = new CustomSelfDrawPanel.CSDFill();
            this.fillPasswordValid.Position = new Point((x + 300) + 5, this.txtPassword.Location.Y);
            this.fillPasswordValid.Size = new Size(this.txtPassword.Height, this.txtPassword.Height);
            this.fillPasswordValid.FillColor = ARGBColors.Red;
            if (this.m_createMode)
            {
                this.lblPasswordconfirm = new CustomSelfDrawPanel.CSDLabel();
                this.lblPasswordconfirm.Position = new Point(this.txtPassword.Location.X, this.txtPassword.Location.Y + num5);
                this.lblPasswordconfirm.Text = this.TextPasswordConfirm;
                this.lblPasswordconfirm.Size = new Size(300, height);
                this.lblPasswordconfirm.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.txtPasswordconfirm = new TextBox();
                this.txtPasswordconfirm.ForeColor = ARGBColors.Black;
                this.txtPasswordconfirm.PasswordChar = '*';
                this.txtPasswordconfirm.BackColor = ARGBColors.White;
                this.txtPasswordconfirm.Location = new Point(this.lblPasswordconfirm.Position.X, this.lblPasswordconfirm.Position.Y + num6);
                this.txtPasswordconfirm.Size = new Size(300, this.txtPasswordconfirm.Height);
                this.fillPasswordConfirmValid = new CustomSelfDrawPanel.CSDFill();
                this.fillPasswordConfirmValid.Position = new Point((x + 300) + 5, this.txtPasswordconfirm.Location.Y);
                this.fillPasswordConfirmValid.Size = new Size(this.txtPasswordconfirm.Height, this.txtPasswordconfirm.Height);
                this.fillPasswordConfirmValid.FillColor = ARGBColors.Red;
                if (Program.gamersFirstInstall || Program.arcInstall)
                {
                    this.lblPasswordconfirm.Visible = false;
                    this.txtPasswordconfirm.Visible = false;
                    this.fillPasswordConfirmValid.Visible = false;
                }
            }
            if (Program.gamersFirstInstall || Program.arcInstall)
            {
                this.lblPassword.Visible = false;
                this.txtPassword.Visible = false;
                this.fillPasswordValid.Visible = false;
                this.txtEmail.ReadOnly = true;
                this.fillEmailValid.Visible = false;
            }
            else
            {
                this.newsletterCheck = new CustomSelfDrawPanel.CSDCheckBox();
                this.newsletterCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
                this.newsletterCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
                this.newsletterCheck.Position = new Point((x + 5) + 60, (this.txtPasswordconfirm.Location.Y + 0x19) + 4);
                this.newsletterCheck.Checked = false;
                this.newsletterCheck.CBLabel.Text = SK.Text("Create_Subscribe Newsletter", "Subscribe to Newsletter");
                this.newsletterCheck.CBLabel.Color = ARGBColors.Black;
                this.newsletterCheck.CBLabel.Position = new Point(20, -1);
                this.newsletterCheck.CBLabel.Size = new Size(360, 0x23);
                this.newsletterCheck.CBLabel.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.AddControlToPanel(this.newsletterCheck);
            }
            this.NextButton = new CustomSelfDrawPanel.CSDImage();
            if (this.m_createMode)
            {
                this.NextButton.Image = this.NextImage;
                if (overDelegate == null)
                {
                    overDelegate = delegate {
                        this.NextButton.Image = this.NextImageOver;
                        this.Cursor = Cursors.Hand;
                    };
                }
                if (leaveDelegate == null)
                {
                    leaveDelegate = delegate {
                        this.NextButton.Image = this.NextImage;
                        this.Cursor = Cursors.Default;
                    };
                }
                this.NextButton.setMouseOverDelegate(overDelegate, leaveDelegate);
                this.NextButton.Position = new Point(this.txtPasswordconfirm.Location.X, ((this.txtPasswordconfirm.Location.Y + num5) + 10) - num);
            }
            else
            {
                this.NextButton.Image = this.TransferImage;
                if (delegate4 == null)
                {
                    delegate4 = delegate {
                        this.NextButton.Image = this.TransferImageOver;
                        this.Cursor = Cursors.Hand;
                    };
                }
                if (delegate5 == null)
                {
                    delegate5 = delegate {
                        this.NextButton.Image = this.TransferImage;
                        this.Cursor = Cursors.Default;
                    };
                }
                this.NextButton.setMouseOverDelegate(delegate4, delegate5);
                this.NextButton.Position = new Point(this.txtPasswordconfirm.Location.X, ((this.txtPasswordconfirm.Location.Y + num5) + 10) - 60);
            }
            this.NextButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.NextClicked), "CreatePopupPanel_next");
            if ((!Program.steamActive || !this.m_createMode) && (!Program.gamersFirstInstall && !Program.arcInstall))
            {
                CustomSelfDrawPanel.CSDButton c = new CustomSelfDrawPanel.CSDButton {
                    ImageNorm = this.CloseImage,
                    ImageOver = this.CloseImageOver,
                    Position = new Point(480, 410)
                };
                c.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CreatePopupPanel_close");
                this.AddControlToPanel(c);
            }
            if (HintBoxImage == null)
            {
                HintBoxImage = new Bitmap(500, 100);
                Graphics graphics = Graphics.FromImage(HintBoxImage);
                graphics.Clear(ARGBColors.White);
                graphics.DrawRectangle(Pens.LightGray, new Rectangle(0, 0, 500, 100));
                graphics.FillRectangle(Brushes.LightGray, new Rectangle(0, 0x60, 500, 4));
                graphics.FillRectangle(Brushes.LightGray, new Rectangle(0x1f0, 0, 4, 100));
                graphics.Dispose();
            }
            this.HintBox = new CustomSelfDrawPanel.CSDImage();
            this.HintBox.Image = HintBoxImage;
            this.HintBox.Width = HintBoxImage.Width;
            this.HintBox.Height = HintBoxImage.Height;
            if (this.m_createMode)
            {
                this.HintBox.Position = new Point(this.HeaderTitle.Position.X, (this.NextButton.Position.Y + num5) + 8);
            }
            else
            {
                this.HintBox.Position = new Point(this.HeaderTitle.Position.X + 50, (this.NextButton.Position.Y + num5) + 8);
            }
            this.HintBoxLabel = new CustomSelfDrawPanel.CSDLabel();
            this.HintBoxLabel.Width = HintBoxImage.Width - 0x20;
            this.HintBoxLabel.Height = ((HintBoxImage.Height - 0x20) / 2) + 8;
            this.HintBoxLabel.Position = new Point(this.HintBox.Position.X + 0x10, this.HintBox.Position.Y + 0x10);
            this.FeedbackLabel = new CustomSelfDrawPanel.CSDLabel();
            this.FeedbackLabel.Width = this.HintBoxLabel.Width;
            this.FeedbackLabel.Height = this.HintBoxLabel.Height;
            this.FeedbackLabel.Position = new Point(this.HintBoxLabel.Position.X, this.HintBoxLabel.Position.Y + this.HintBoxLabel.Height);
            this.FeedbackLabel.Color = ARGBColors.Red;
            this.tandcLabel = new CustomSelfDrawPanel.CSDLabel();
            this.tandcLabel.Text = SK.Text("MENU_TandC", "Terms & Conditions").Replace("&amp;", "&");
            if (Program.mySettings.LanguageIdent == "de")
            {
                this.tandcLabel.Size = new Size(270, 20);
                this.tandcLabel.Position = new Point(30, 0x193);
            }
            else
            {
                this.tandcLabel.Size = new Size(170, 20);
                this.tandcLabel.Position = new Point(100, 0x193);
            }
            this.tandcLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.tandcLabel.Color = ARGBColors.Black;
            this.tandcLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tcClicked));
            this.tandcLabel.setMouseOverDelegate(() => this.tandcLabel.Color = ARGBColors.Red, () => this.tandcLabel.Color = ARGBColors.Black);
            this.tandcLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.AddControlToPanel(this.tandcLabel);
            this.privacyLabel = new CustomSelfDrawPanel.CSDLabel();
            this.privacyLabel.Text = SK.Text("MENU_Privacy", "Privacy Policy");
            this.privacyLabel.Size = new Size(base.Width - 0x19e, 20);
            this.privacyLabel.Position = new Point(0xcf, 0x193);
            this.privacyLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.privacyLabel.Color = ARGBColors.Black;
            this.privacyLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.privacyClicked));
            this.privacyLabel.setMouseOverDelegate(() => this.privacyLabel.Color = ARGBColors.Red, () => this.privacyLabel.Color = ARGBColors.Black);
            this.privacyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.AddControlToPanel(this.privacyLabel);
            if (Program.steamActive && this.m_createMode)
            {
                this.alreadyLabel = new CustomSelfDrawPanel.CSDLabel();
                this.alreadyLabel.Text = SK.Text("Steam_already", "Already have a Stronghold Kingdoms account? Click Here.");
                this.alreadyLabel.Size = new Size(base.Width - 20, 20);
                this.alreadyLabel.Position = new Point(10, base.Height - 20);
                this.alreadyLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                this.alreadyLabel.Color = ARGBColors.Black;
                if (newDelegate == null)
                {
                    newDelegate = () => this.init(false);
                }
                this.alreadyLabel.setClickDelegate(newDelegate);
                if (delegate7 == null)
                {
                    delegate7 = () => this.alreadyLabel.Color = ARGBColors.Red;
                }
                if (delegate8 == null)
                {
                    delegate8 = () => this.alreadyLabel.Color = ARGBColors.Black;
                }
                this.alreadyLabel.setMouseOverDelegate(delegate7, delegate8);
                this.alreadyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.AddControlToPanel(this.alreadyLabel);
            }
            this.AddControlToPanel(this.HeaderTitle);
            this.AddControlToPanel(this.lblEmail);
            if (!Program.gamersFirstInstall && !Program.arcInstall)
            {
                base.Controls.Add(this.txtEmail);
            }
            if (this.m_createMode)
            {
                this.AddControlToPanel(this.fillEmailValid);
            }
            if (this.m_createMode)
            {
                if (!Program.gamersFirstInstall && !Program.arcInstall)
                {
                    this.AddControlToPanel(this.lblEmailconfirm);
                    base.Controls.Add(this.txtEmailconfirm);
                    this.AddControlToPanel(this.fillEmailConfirmValid);
                }
                this.AddControlToPanel(this.lblUsername);
                base.Controls.Add(this.txtUsername);
                this.txtUsername.MaxLength = 0x12;
                this.AddControlToPanel(this.fillUsernameValid);
            }
            this.AddControlToPanel(this.lblPassword);
            base.Controls.Add(this.txtPassword);
            if (this.m_createMode)
            {
                this.AddControlToPanel(this.fillPasswordValid);
            }
            if ((this.m_createMode && !Program.gamersFirstInstall) && !Program.arcInstall)
            {
                this.AddControlToPanel(this.lblPasswordconfirm);
                base.Controls.Add(this.txtPasswordconfirm);
                this.AddControlToPanel(this.fillPasswordConfirmValid);
            }
            if (Program.gamersFirstInstall || Program.arcInstall)
            {
                base.Controls.Add(this.txtEmail);
            }
            this.AddControlToPanel(this.NextButton);
            this.AddControlToPanel(this.HintBox);
            this.AddControlToPanel(this.HintBoxLabel);
            this.AddControlToPanel(this.FeedbackLabel);
            this.txtEmail.Name = "txtEmail";
            if (!Program.gamersFirstInstall && !Program.arcInstall)
            {
                this.txtEmail.GotFocus += new EventHandler(this.txtEmail_GotFocus);
            }
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.GotFocus += new EventHandler(this.txtPassword_GotFocus);
            this.txtEmail.TextChanged += new EventHandler(this.txtEmail_TextChanged);
            this.txtPassword.TextChanged += new EventHandler(this.txtPassword_TextChanged);
            if (this.m_createMode)
            {
                this.txtEmailconfirm.Name = "txtEmailConfirm";
                this.txtEmailconfirm.GotFocus += new EventHandler(this.txtEmailconfirm_GotFocus);
                this.txtUsername.Name = "txtUsername";
                this.txtUsername.GotFocus += new EventHandler(this.txtUsername_GotFocus);
                this.txtPasswordconfirm.Name = "txtPasswordConfirm";
                this.txtPasswordconfirm.GotFocus += new EventHandler(this.txtPasswordconfirm_GotFocus);
                this.txtEmailconfirm.TextChanged += new EventHandler(this.txtEmailconfirm_TextChanged);
                this.txtUsername.TextChanged += new EventHandler(this.txtUsername_TextChanged);
                this.txtPasswordconfirm.TextChanged += new EventHandler(this.txtPasswordconfirm_TextChanged);
            }
            base.BringToFront();
            base.Focus();
            if (Program.gamersFirstInstall || Program.arcInstall)
            {
                this.txtUsername.Focus();
            }
            else
            {
                this.txtEmail.Focus();
            }
            this.emailValid = false;
            this.emailconfirmvalid = false;
            this.passwordvalid = false;
            this.passwordconfirmvalid = false;
            this.lastUsernameValid = false;
            this.usernameValidationInProgress = false;
            this.lastUsernameChecked = string.Empty;
            this.usernameNotChecked = false;
            if (Program.gamersFirstInstall || Program.arcInstall)
            {
                this.passwordconfirmvalid = this.passwordvalid = true;
                this.emailValid = this.emailconfirmvalid = true;
                this.txtEmail.Text = ProfileLoginWindow.gfEmail;
                this.txtPassword.Text = ProfileLoginWindow.gfPW;
                this.txtUsername.Focus();
            }
            this.ValidateNextButton();
            this.txtEmail.KeyUp += new KeyEventHandler(this.Tabfix);
            base.Invalidate();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void NextClicked()
        {
            if (this.m_createMode)
            {
                if ((this.lastUsernameValid && !this.usernameValidationInProgress) && ((this.txtUsername.Text == this.lastUsernameChecked) && this.everythingValid()))
                {
                    this.CreateUser();
                }
                else if (((this.txtUsername.Text != this.lastUsernameChecked) && this.lastUsernameValid) && !this.usernameValidationInProgress)
                {
                    this.ValidateUsername(true);
                }
            }
            else if ((this.txtEmail.Text.Length > 0) && (this.txtPassword.Text.Length > 0))
            {
                this.TransferUser();
            }
        }

        private bool PasswordIsValid()
        {
            if (Program.gamersFirstInstall || Program.arcInstall)
            {
                return (this.txtPassword.Text.Length > 0);
            }
            return ((this.txtPassword.Text.Trim().Length >= 5) && (this.txtPassword.Text.Trim().Length <= 0x19));
        }

        private void privacyClicked()
        {
            try
            {
                new Process { StartInfo = { FileName = URLs.PrivacyPolicy } }.Start();
            }
            catch (Exception)
            {
            }
        }

        private void SwitchHint(string text, TextBox field)
        {
            this.HintBoxLabel.TextDiffOnly = text;
        }

        private void Tabfix(object sender, KeyEventArgs e)
        {
            bool flag1 = ((e.KeyData & Keys.KeyCode)).ToString().ToUpper() == "TAB";
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

        private void TransferUser()
        {
            XmlRpcAuthProvider provider = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
            XmlRpcAuthRequest req = new XmlRpcAuthRequest {
                SteamID = Program.steamID,
                Culture = Program.mySettings.LanguageIdent.ToLower(),
                Username = "##Transfer##",
                EmailAddress = this.txtEmail.Text,
                Password = this.txtPassword.Text
            };
            this.NextButton.Image = this.NextImageOver;
            this.NextButton.Enabled = false;
            this.txtEmail.Enabled = false;
            this.txtEmailconfirm.Enabled = false;
            this.txtUsername.Enabled = false;
            this.txtPassword.Enabled = false;
            this.txtPasswordconfirm.Enabled = false;
            provider.CreateUserSteam(req, new AuthEndResponseDelegate(this.createUserCallback), this);
        }

        private void txtEmail_GotFocus(object sender, EventArgs e)
        {
            if (this.m_createMode)
            {
                this.SwitchHint(this.TextHint_Email, this.txtEmail);
                this.ValidateEmail();
            }
            else
            {
                this.SwitchHint(this.TextHint_Email_Merge, this.txtEmail);
            }
            this.ValidateNextButton();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (this.m_createMode)
            {
                this.SwitchHint(this.TextHint_Email, this.txtEmail);
                this.ValidateEmail();
            }
            else
            {
                this.SwitchHint(this.TextHint_Email_Merge, this.txtEmail);
            }
            this.ValidateNextButton();
        }

        private void txtEmailconfirm_GotFocus(object sender, EventArgs e)
        {
            if (this.m_createMode)
            {
                this.SwitchHint(this.TextHint_EmailConfirm, this.txtEmailconfirm);
                this.ValidateEmailconfirm();
                this.ValidateNextButton();
            }
        }

        private void txtEmailconfirm_TextChanged(object sender, EventArgs e)
        {
            if (this.m_createMode)
            {
                this.SwitchHint(this.TextHint_EmailConfirm, this.txtEmailconfirm);
                this.ValidateEmailconfirm();
                this.ValidateNextButton();
            }
        }

        private void txtPassword_GotFocus(object sender, EventArgs e)
        {
            if (this.m_createMode)
            {
                this.SwitchHint(this.TextHint_Password, this.txtPassword);
                this.ValidatePassword();
            }
            else
            {
                this.SwitchHint(this.TextHint_Password_Merge, this.txtPassword);
            }
            this.ValidateNextButton();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (this.m_createMode)
            {
                this.SwitchHint(this.TextHint_Password, this.txtPassword);
                this.ValidatePassword();
            }
            else
            {
                this.SwitchHint(this.TextHint_Password_Merge, this.txtPassword);
            }
            this.ValidateNextButton();
        }

        private void txtPasswordconfirm_GotFocus(object sender, EventArgs e)
        {
            if (this.m_createMode)
            {
                this.SwitchHint(this.TextHint_PasswordConfirm, this.txtPasswordconfirm);
                this.ValidatePasswordConfirm();
                this.ValidateNextButton();
            }
        }

        private void txtPasswordconfirm_TextChanged(object sender, EventArgs e)
        {
            if (this.m_createMode)
            {
                this.SwitchHint(this.TextHint_PasswordConfirm, this.txtPasswordconfirm);
                this.ValidatePasswordConfirm();
                this.ValidateNextButton();
            }
        }

        private void txtUsername_GotFocus(object sender, EventArgs e)
        {
            if (this.m_createMode)
            {
                this.SwitchHint(this.TextHint_Username, this.txtUsername);
                this.ValidateNextButton();
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (this.m_createMode)
            {
                this.SwitchHint(this.TextHint_Username, this.txtUsername);
                this.ValidateUsername(false);
                this.ValidateNextButton();
            }
        }

        public void update()
        {
            if (this.usernameNotChecked)
            {
                this.ValidateUsername(false);
            }
        }

        private void usernameCheckCallback(IAuthProvider sender, IAuthResponse response)
        {
            this.usernameValidationInProgress = false;
            this.lastUsernameChecked = response.Username;
            if (response.SuccessCode == 1)
            {
                this.lastUsernameValid = true;
            }
            else
            {
                this.lastUsernameValid = false;
            }
            this.NextButton.Image = this.NextImage;
            if ((this.txtUsername.Text.Length < 4) || (this.txtUsername.Text.Length > 0x12))
            {
                this.FeedbackLabel.Color = ARGBColors.Red;
                this.FeedbackLabel.Text = string.Empty;
            }
            else if (this.lastUsernameValid)
            {
                this.FeedbackLabel.Color = ARGBColors.Green;
                this.FeedbackLabel.Text = response.Message;
            }
            else
            {
                this.FeedbackLabel.Color = ARGBColors.Red;
                this.FeedbackLabel.Text = response.Message;
            }
            this.ValidateNextButton();
        }

        private void usernameCheckCallbackThenCreate(IAuthProvider sender, IAuthResponse response)
        {
            this.usernameValidationInProgress = false;
            this.lastUsernameChecked = response.Username;
            if (response.SuccessCode == 1)
            {
                this.lastUsernameValid = true;
                this.CreateUser();
            }
            else
            {
                this.lastUsernameValid = false;
            }
        }

        private void ValidateEmail()
        {
            Regex regex = new Regex(this.emailPattern);
            if (regex.IsMatch(this.txtEmail.Text.Trim()))
            {
                this.FeedbackLabel.Color = ARGBColors.Green;
                this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Email_Valid", "Email Valid");
                this.emailValid = true;
            }
            else
            {
                this.FeedbackLabel.Color = ARGBColors.Red;
                this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Email_Invalid", "Email Invalid");
                this.emailValid = false;
            }
            this.emailconfirmvalid = (this.txtEmail.Text.Trim() == this.txtEmailconfirm.Text.Trim()) && this.emailValid;
            if (Program.gamersFirstInstall || Program.arcInstall)
            {
                this.emailconfirmvalid = this.emailValid = true;
            }
        }

        private void ValidateEmailconfirm()
        {
            this.ValidateEmail();
            if (!this.emailconfirmvalid && !this.emailValid)
            {
                this.FeedbackLabel.Color = ARGBColors.Red;
                this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Email_Invalid", "Email Invalid");
            }
            else if (!this.emailconfirmvalid && this.emailValid)
            {
                this.FeedbackLabel.Color = ARGBColors.Red;
                this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Email_Not_Match", "Emails do not match");
            }
            else if (this.emailconfirmvalid && this.emailValid)
            {
                this.FeedbackLabel.Color = ARGBColors.Green;
                this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Email_Match", "Emails match");
            }
        }

        private void ValidateNextButton()
        {
            this.NextButton.Enabled = this.everythingValid();
            if (this.m_createMode)
            {
                if (this.NextButton.Enabled)
                {
                    this.NextButton.Image = this.NextImage;
                }
                else
                {
                    this.NextButton.Image = this.NextImageOver;
                }
            }
            else if (this.NextButton.Enabled)
            {
                this.NextButton.Image = this.TransferImage;
            }
            else
            {
                this.NextButton.Image = this.TransferImageOver;
            }
            if (this.emailValid)
            {
                this.fillEmailValid.FillColor = ARGBColors.Green;
            }
            else
            {
                this.fillEmailValid.FillColor = ARGBColors.Red;
            }
            if (this.emailconfirmvalid)
            {
                this.fillEmailConfirmValid.FillColor = ARGBColors.Green;
            }
            else
            {
                this.fillEmailConfirmValid.FillColor = ARGBColors.Red;
            }
            if (this.lastUsernameValid)
            {
                this.fillUsernameValid.FillColor = ARGBColors.Green;
            }
            else
            {
                this.fillUsernameValid.FillColor = ARGBColors.Red;
            }
            if (this.passwordvalid)
            {
                this.fillPasswordValid.FillColor = ARGBColors.Green;
            }
            else
            {
                this.fillPasswordValid.FillColor = ARGBColors.Red;
            }
            if (this.passwordconfirmvalid)
            {
                this.fillPasswordConfirmValid.FillColor = ARGBColors.Green;
            }
            else
            {
                this.fillPasswordConfirmValid.FillColor = ARGBColors.Red;
            }
        }

        private void ValidatePassword()
        {
            if (this.PasswordIsValid())
            {
                this.FeedbackLabel.Color = ARGBColors.Green;
                this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Password_Valid", "Password Valid");
                this.passwordvalid = true;
            }
            else
            {
                this.FeedbackLabel.Color = ARGBColors.Red;
                this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Password_Invalid", "Password Invalid");
                this.passwordvalid = false;
            }
            this.passwordconfirmvalid = (this.txtPassword.Text.Trim() == this.txtPasswordconfirm.Text.Trim()) && this.passwordvalid;
            if (Program.gamersFirstInstall || Program.arcInstall)
            {
                this.passwordconfirmvalid = this.passwordvalid = true;
            }
        }

        private void ValidatePasswordConfirm()
        {
            this.ValidatePassword();
            if (!this.passwordconfirmvalid && !this.passwordvalid)
            {
                this.FeedbackLabel.Color = ARGBColors.Red;
                this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Password_Invalid", "Password Invalid");
            }
            else if (!this.passwordconfirmvalid && this.passwordvalid)
            {
                this.FeedbackLabel.Color = ARGBColors.Red;
                this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Password_Not_Match", "Passwords do not match");
            }
            else if (this.passwordconfirmvalid && this.passwordvalid)
            {
                this.FeedbackLabel.Color = ARGBColors.Green;
                this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Password_Match", "Passwords match");
            }
        }

        private void ValidateUsername(bool create)
        {
            Regex regex = new Regex(this.usernamePattern);
            if (((this.txtUsername.Text.Length >= 4) && (this.txtUsername.Text.Length <= 0x12)) && regex.IsMatch(this.txtUsername.Text.Trim()))
            {
                if (!this.usernameValidationInProgress)
                {
                    this.usernameNotChecked = false;
                    this.usernameValidationInProgress = true;
                    XmlRpcAuthProvider provider = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
                    XmlRpcAuthRequest req = new XmlRpcAuthRequest {
                        SteamID = Program.steamID,
                        Culture = Program.mySettings.LanguageIdent.ToLower(),
                        Username = this.txtUsername.Text.Trim()
                    };
                    this.NextButton.Image = this.NextImageOver;
                    this.NextButton.Enabled = false;
                    if (create)
                    {
                        provider.CheckUsernameSteam(req, new AuthEndResponseDelegate(this.usernameCheckCallbackThenCreate), this);
                    }
                    else
                    {
                        provider.CheckUsernameSteam(req, new AuthEndResponseDelegate(this.usernameCheckCallback), this);
                    }
                }
                else
                {
                    this.usernameNotChecked = true;
                }
            }
            else
            {
                this.FeedbackLabel.Color = ARGBColors.Red;
                this.FeedbackLabel.Text = string.Empty;
                this.lastUsernameValid = false;
                this.ValidateNextButton();
            }
        }

        public Image CloseImage
        {
            get
            {
                if (closeImage == null)
                {
                    closeImage = WebStyleButtonImage.Generate(200, this.WebButtonheight, Program.steamActive ? this.strBack : this.strClose, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
                }
                return closeImage;
            }
        }

        public Image CloseImageOver
        {
            get
            {
                if (closeImageOver == null)
                {
                    closeImageOver = WebStyleButtonImage.Generate(200, this.WebButtonheight, Program.steamActive ? this.strBack : this.strClose, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return closeImageOver;
            }
        }

        public Image HeaderImage
        {
            get
            {
                if (headerImage == null)
                {
                    headerImage = WebStyleButtonImage.Generate(500, 30, this.TextCreateHeader, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return headerImage;
            }
        }

        public Image HeaderTranferImage
        {
            get
            {
                if (headerTransferImage == null)
                {
                    headerTransferImage = WebStyleButtonImage.Generate(600, 30, this.TextCreateHeaderMerge, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return headerTransferImage;
            }
        }

        public Image NextImage
        {
            get
            {
                if (nextImage == null)
                {
                    nextImage = WebStyleButtonImage.Generate(300, this.WebButtonheight, this.strNext, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
                }
                return nextImage;
            }
        }

        public Image NextImageOver
        {
            get
            {
                if (nextImageOver == null)
                {
                    nextImageOver = WebStyleButtonImage.Generate(300, this.WebButtonheight, this.strNext, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return nextImageOver;
            }
        }

        public Image TransferImage
        {
            get
            {
                if (transferImage == null)
                {
                    transferImage = WebStyleButtonImage.Generate(300, this.WebButtonheight, this.strMerge, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
                }
                return transferImage;
            }
        }

        public Image TransferImageOver
        {
            get
            {
                if (transferImageOver == null)
                {
                    transferImageOver = WebStyleButtonImage.Generate(300, this.WebButtonheight, this.strMerge, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return transferImageOver;
            }
        }
    }
}

