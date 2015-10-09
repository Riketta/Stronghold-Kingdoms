namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class MainMenuBar2 : CustomSelfDrawPanel.CSDControl
    {
        private CustomSelfDrawPanel.CSDButton btnAdminMenu = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnCombat = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnFileMenu = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnHelpMenu = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnLogOut = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnMyAccount = new CustomSelfDrawPanel.CSDButton();
        private static bool castleCopyMode;
        private bool fixCommandSent;
        private LogoutOptionsWindow2 logoutPopup;
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private MenuMailPanel MenuButtonsPanel = new MenuMailPanel();
        private bool nextPlaybackCountries;
        private CustomSelfDrawPanel.CSDFill pnlLoadLight = new CustomSelfDrawPanel.CSDFill();
        private SaveFileDialog SaveCSVFileDialog = new SaveFileDialog();

        public MainMenuBar2()
        {
            this.SaveCSVFileDialog.DefaultExt = "csv";
            this.SaveCSVFileDialog.Filter = "CSV (*.csv)|*.csv";
            this.SaveCSVFileDialog.Title = "Save Stats .csv File";
            if (RemoteServices.Instance.Admin)
            {
                this.btnAdminMenu.Visible = true;
            }
            else
            {
                this.btnAdminMenu.Visible = false;
            }
            this.btnCombat.Visible = false;
        }

        private void accountOver()
        {
            if (MenuPopup.isAMenuVisible())
            {
                this.btnMyAccount_Click(false);
            }
        }

        private void adminOver()
        {
            if (MenuPopup.isAMenuVisible())
            {
                this.btnAdminMenu_Click();
            }
        }

        private void btnAdminMenu_Click()
        {
            MenuPopup popup = new MenuPopup();
            Point point = base.csd.PointToScreen(this.btnAdminMenu.Position);
            popup.setPosition(point.X, point.Y + 0x18);
            popup.setCallBack(new MenuPopup.MenuCallback(this.menu1Callback));
            popup.addMenuItem("Edit Admin Message", 0xc9);
            popup.addMenuItem("Retrieve Game Info", 0xcb);
            popup.addBar();
            popup.addMenuItem("Country Playback (Admins Only)", 0xca);
            popup.addMenuItem("Province Playback (Admins Only)", 0x23f2);
            popup.addMenuItem("Stop Playback (Admins Only)", 0x23f3);
            popup.addBar();
            popup.addMenuItem("Fix Lost Units (CAREFUL!)", 0xd1);
            popup.addMenuItem("Castle Copy Mode", 0x5207);
            if (!GameEngine.Instance.World.MapEditing)
            {
                popup.addMenuItem("Open County - Select Capital", 0x8ab);
            }
            else
            {
                int villageID = GameEngine.Instance.World.lastClickedVillage();
                if (GameEngine.Instance.World.isCountyCapital(villageID) && !GameEngine.Instance.World.isVillageVisible(villageID))
                {
                    int countyID = GameEngine.Instance.World.getCountyFromVillageID(villageID);
                    popup.addMenuItem("Open County : " + GameEngine.Instance.World.getCountyName(countyID), 0x8ab);
                }
                else
                {
                    popup.addMenuItem("Open County : NONE SELECTED", 0x8ab);
                }
            }
            popup.addBar();
            popup.addMenuItem("Toggle Village IDs", 0xe7);
            popup.addMenuItem("Toggle Village Names", 0xe8);
            popup.addBar();
            popup.addMenuItem("Create Ingame Message", 0xdd);
            popup.addMenuItem("Remove Ingame Message", 0xdf);
            popup.showMenu();
        }

        private void btnCombat_Click()
        {
        }

        private void btnHelp_Click()
        {
            this.btnHelp_Click(true);
        }

        private void btnHelp_Click(bool sfx)
        {
            if (sfx)
            {
                GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_help");
            }
            MenuPopup popup = new MenuPopup();
            Point point = base.csd.PointToScreen(this.btnHelpMenu.Position);
            popup.setPosition(point.X, point.Y + 0x18);
            popup.setCallBack(new MenuPopup.MenuCallback(this.menu1Callback));
            popup.addMenuItem(SK.Text("MENU_SHK_Help", "Stronghold Kingdoms Help"), 0x6c);
            popup.addMenuItem(SK.Text("MENU_Game_Rules", "Game Rules"), 0x6d);
            if (Program.mySettings.LanguageIdent == "en")
            {
                popup.addMenuItem("Terms & Conditions", 0x97);
            }
            else
            {
                popup.addMenuItem(SK.Text("MENU_TandC", "Terms & Conditions").Replace("&amp;", "&&"), 0x97);
            }
            popup.addMenuItem(SK.Text("MENU_Privacy", "Privacy Policy"), 0x98);
            popup.addMenuItem(SK.Text("MENU_Forum", "Forum"), 0x6b);
            popup.addBar();
            popup.addMenuItem(SK.Text("MENU_Show_Admin_Message", "Show Admin Message"), 0x67);
            popup.addBar();
            if (GameEngine.Instance.World.isTutorialResumable())
            {
                popup.addMenuItem(SK.Text("Options_Resume_Tutorial", "Resume Tutorial"), 0x455);
            }
            popup.addMenuItem(SK.Text("Options_Player_Guide", "Player Guide"), 0x4b1);
            popup.addBar();
            popup.addMenuItem(SK.Text("MENU_About_Stronghold Kingdoms", "About Stronghold Kingdoms"), 0x66);
            popup.showMenu();
        }

        private void btnLogOut_Click()
        {
            GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_logout");
            if ((this.logoutPopup != null) && this.logoutPopup.Created)
            {
                this.logoutPopup.Close();
                this.logoutPopup = null;
            }
            this.logoutPopup = InterfaceMgr.Instance.openLogoutWindow(true);
        }

        private void btnMail_Click(object sender, EventArgs e)
        {
        }

        private void btnMail_MouseEnter(object sender, EventArgs e)
        {
            if (MenuPopup.isAMenuVisible())
            {
                this.btnMail_Click(null, null);
            }
        }

        private void btnMyAccount_Click()
        {
            this.btnMyAccount_Click(true);
        }

        private void btnMyAccount_Click(bool sfx)
        {
            if (sfx)
            {
                GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_myaccount");
            }
            MenuPopup popup = new MenuPopup();
            Point point = base.csd.PointToScreen(this.btnMyAccount.Position);
            popup.setPosition(point.X, point.Y + 0x18);
            popup.setCallBack(new MenuPopup.MenuCallback(this.menu1Callback));
            popup.addMenuItem(SK.Text("MENU_Account_Information", "Account Information"), 0x5209);
            if ((!GameEngine.Instance.World.isBigpointAccount && !Program.bigpointInstall) && (!Program.aeriaInstall && !Program.bigpointPartnerInstall))
            {
                popup.addMenuItem(SK.Text("MENU_Invite_A_Friend", "Invite a Friend"), 0x520a);
            }
            popup.addMenuItem(SK.Text("MENU_Redeem_Offer_Code", "Redeem Offer Code"), 0x520b);
            popup.addBar();
            if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1)
            {
                popup.addMenuItem(SK.Text("MENU_VacationMode", "Vacation Mode Options"), 0x5211);
            }
            popup.showMenu();
        }

        private void button2_Click()
        {
            this.button2_Click(true);
        }

        private void button2_Click(bool sfx)
        {
            if (sfx)
            {
                GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_settings");
            }
            MenuPopup popup = new MenuPopup();
            Point point = base.csd.PointToScreen(this.btnFileMenu.Position);
            popup.setPosition(point.X, point.Y + 0x18);
            popup.setCallBack(new MenuPopup.MenuCallback(this.menu1Callback));
            popup.addMenuItem(SK.Text("MENU_Settings", "Settings"), 1);
            popup.addMenuItem(SK.Text("MENU_Edit_Avatar", "Edit Avatar"), 5);
            popup.addMenuItem(SK.Text("User_Manage_Relations", "Manage Diplomacy"), 300);
            int ownSelectedVillage = InterfaceMgr.Instance.OwnSelectedVillage;
            if (((ownSelectedVillage >= 0) && !GameEngine.Instance.World.isCapital(ownSelectedVillage)) && GameEngine.Instance.World.isUserVillage(ownSelectedVillage))
            {
                popup.addBar();
                popup.addMenuItem(SK.Text("MENU_Rename_Current_Village", "Rename Current Village"), 9);
                CustomSelfDrawPanel.CSDControl control = popup.addMenuItem(SK.Text("MENU_Convert_Current_Village", "Convert Current Village"), 12);
                CustomSelfDrawPanel.CSDControl control2 = popup.addMenuItem(SK.Text("MENU_Abandon_Current_Village", "Abandon Current Village"), 11);
                control.CustomTooltipID = 0x4b0;
                control2.CustomTooltipID = 0x4b1;
            }
            popup.showMenu();
        }

        public void clearIngameMessage()
        {
            RemoteServices.Instance.SetAdminMessage("", 0x3e8);
        }

        private void combatOver()
        {
            if (MenuPopup.isAMenuVisible())
            {
                this.btnCombat_Click();
            }
        }

        private void CompleteVillageCastleCallBack(CompleteVillageCastle_ReturnType returnData)
        {
            if (returnData.Success && (returnData.cardData >= 0))
            {
                GameEngine.Instance.World.addProfileCard(returnData.cardData, CardTypes.getStringFromCard(0xc08));
            }
            if (returnData.Success && this.fixCommandSent)
            {
                MyMessageBox.Show("Armies : " + returnData.armies.ToString() + Environment.NewLine + "Monks : " + returnData.monks.ToString() + Environment.NewLine + "Traders : " + returnData.traders.ToString() + Environment.NewLine + "Cards : " + returnData.cards.ToString() + Environment.NewLine, "Fixes");
            }
            this.fixCommandSent = false;
        }

        public void createIngameMessage()
        {
            new AdminIngameMessage().Show();
        }

        private void fileOver()
        {
            if (MenuPopup.isAMenuVisible())
            {
                this.button2_Click(false);
            }
        }

        private void genericLeave()
        {
        }

        public void getAdminStatsCallback(GetAdminStats_ReturnType returnData)
        {
            if (returnData.Success)
            {
                AdminStatsPopup popup = new AdminStatsPopup();
                popup.init(returnData);
                popup.Show();
            }
        }

        private void helpOver()
        {
            if (MenuPopup.isAMenuVisible())
            {
                this.btnHelp_Click(false);
            }
        }

        public void init2()
        {
            this.MenuButtonsPanel.init();
            this.clearControls();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.menu_Background;
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = new Size(0x1cf, 0x1d);
            base.addControl(this.mainBackgroundImage);
            this.btnAdminMenu.FillRectOverColor = Color.FromArgb(0x20, 0, 0, 0);
            this.btnAdminMenu.Position = new Point(0x4f, 1);
            this.btnAdminMenu.Size = new Size(0x2d, 0x17);
            this.btnAdminMenu.Text.Text = "Admin";
            this.btnAdminMenu.Text.Size = this.btnAdminMenu.Size;
            this.btnAdminMenu.Text.Color = ARGBColors.Black;
            this.btnAdminMenu.TextYOffset = -1;
            this.btnAdminMenu.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.btnAdminMenu.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.btnAdminMenu.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.adminOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.genericLeave));
            this.btnAdminMenu.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAdminMenu_Click));
            this.btnAdminMenu.Visible = RemoteServices.Instance.Admin;
            this.mainBackgroundImage.addControl(this.btnAdminMenu);
            this.btnCombat.FillRectOverColor = Color.FromArgb(0x20, 0, 0, 0);
            this.btnCombat.Position = new Point(0x13, 1);
            this.btnCombat.Size = new Size(0x36, 0x17);
            this.btnCombat.Text.Text = "Combat";
            this.btnCombat.Text.Size = this.btnCombat.Size;
            this.btnCombat.Text.Color = ARGBColors.Black;
            this.btnCombat.TextYOffset = -1;
            this.btnCombat.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.btnCombat.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.btnCombat.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.combatOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.genericLeave));
            this.btnCombat.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnCombat_Click));
            this.btnCombat.Visible = RemoteServices.Instance.Admin;
            this.mainBackgroundImage.addControl(this.btnCombat);
            this.pnlLoadLight.Position = new Point(0x1cb, 0);
            this.pnlLoadLight.Size = new Size(4, 4);
            this.pnlLoadLight.Visible = false;
            this.pnlLoadLight.FillColor = ARGBColors.Green;
            this.mainBackgroundImage.addControl(this.pnlLoadLight);
            this.MenuButtonsPanel.Position = new Point(10, 0);
            this.MenuButtonsPanel.Size = new Size(150, 0x18);
            this.mainBackgroundImage.addControl(this.MenuButtonsPanel);
            this.MenuButtonsPanel.init();
            this.btnHelpMenu.FillRectOverColor = Color.FromArgb(0x20, 0, 0, 0);
            this.btnHelpMenu.Position = new Point(0x143, 1);
            this.btnHelpMenu.Size = new Size(0x3e, 0x17);
            this.btnHelpMenu.Text.Text = SK.Text("MENU_Help", "Help");
            this.btnHelpMenu.Text.Size = this.btnHelpMenu.Size;
            this.btnHelpMenu.Text.Color = ARGBColors.Black;
            this.btnHelpMenu.TextYOffset = -1;
            this.btnHelpMenu.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.btnHelpMenu.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.btnHelpMenu.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.helpOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.genericLeave));
            this.btnHelpMenu.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnHelp_Click));
            this.mainBackgroundImage.addControl(this.btnHelpMenu);
            this.btnLogOut.FillRectOverColor = Color.FromArgb(0x20, 0, 0, 0);
            this.btnLogOut.Position = new Point(0x187, 1);
            this.btnLogOut.Size = new Size(0x3e, 0x17);
            this.btnLogOut.Text.Text = SK.Text("GENERIC_Log_Out", "Log Out");
            this.btnLogOut.Text.Size = this.btnLogOut.Size;
            this.btnLogOut.Text.Color = ARGBColors.Black;
            this.btnLogOut.TextYOffset = -1;
            this.btnLogOut.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.btnLogOut.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.btnLogOut.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnLogOut_Click));
            this.mainBackgroundImage.addControl(this.btnLogOut);
            this.btnFileMenu.FillRectOverColor = Color.FromArgb(0x20, 0, 0, 0);
            this.btnFileMenu.Position = new Point(0xf6, 1);
            this.btnFileMenu.Size = new Size(0x4e, 0x17);
            this.btnFileMenu.Text.Text = SK.Text("MENU_Settings", "Settings");
            this.btnFileMenu.Text.Size = this.btnFileMenu.Size;
            this.btnFileMenu.Text.Color = ARGBColors.Black;
            this.btnFileMenu.TextYOffset = -1;
            this.btnFileMenu.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.btnFileMenu.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.btnFileMenu.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.fileOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.genericLeave));
            this.btnFileMenu.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.button2_Click));
            this.mainBackgroundImage.addControl(this.btnFileMenu);
            this.btnMyAccount.FillRectOverColor = Color.FromArgb(0x20, 0, 0, 0);
            this.btnMyAccount.Position = new Point(0xa6, 1);
            this.btnMyAccount.Size = new Size(0x4e, 0x17);
            this.btnMyAccount.Text.Text = SK.Text("MENU_My_Account", "My Account");
            this.btnMyAccount.Text.Size = this.btnMyAccount.Size;
            this.btnMyAccount.Text.Color = ARGBColors.Black;
            this.btnMyAccount.TextYOffset = -1;
            this.btnMyAccount.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.btnMyAccount.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.btnMyAccount.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.accountOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.genericLeave));
            this.btnMyAccount.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnMyAccount_Click));
            this.mainBackgroundImage.addControl(this.btnMyAccount);
            this.resize();
        }

        private void menu1Callback(int id)
        {
            string str;
            GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_item_selected");
            this.fixCommandSent = false;
            switch (id)
            {
                case 0x5207:
                    castleCopyMode = true;
                    return;

                case 0x5208:
                case 0x19c9:
                case 0x44f:
                case 0x450:
                case 0x451:
                case 0x452:
                case 0x453:
                case 0x454:
                case 0x259:
                case 0x25a:
                case 0xde:
                case 3:
                case 4:
                case 6:
                case 7:
                case 8:
                case 10:
                case 0x68:
                case 0x6a:
                case 110:
                case 0x6f:
                case 0x71:
                case 0x72:
                case 0x73:
                case 0x74:
                case 0x75:
                case 0x76:
                case 0x77:
                case 120:
                    return;

                case 0x5209:
                {
                    string fileName = (URLs.AccountInfoURL + "?u=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&s=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "")) + "&lang=" + Program.mySettings.LanguageIdent.ToLower();
                    try
                    {
                        Process.Start(fileName);
                    }
                    catch (Exception)
                    {
                        MyMessageBox.Show(SK.Text("ERROR_Browser1", "Stronghold Kingdoms encountered an error when trying to open your system's Default Web Browser. Please check that your web browser is working correctly and there are no unresponsive copies showing in task manager->Processes and then try again.") + Environment.NewLine + Environment.NewLine + SK.Text("ERROR_Browser2", "If this problem persists, please contact support."), SK.Text("ERROR_Browser3", "Error opening Web Browser"));
                    }
                    return;
                }
                case 0x520a:
                {
                    string str4 = (URLs.InviteAFriendURL + "?u=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&s=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "")) + "&lang=" + Program.mySettings.LanguageIdent.ToLower();
                    try
                    {
                        Process.Start(str4);
                    }
                    catch (Exception)
                    {
                        MyMessageBox.Show(SK.Text("ERROR_Browser1", "Stronghold Kingdoms encountered an error when trying to open your system's Default Web Browser. Please check that your web browser is working correctly and there are no unresponsive copies showing in task manager->Processes and then try again.") + Environment.NewLine + Environment.NewLine + SK.Text("ERROR_Browser2", "If this problem persists, please contact support."), SK.Text("ERROR_Browser3", "Error opening Web Browser"));
                    }
                    return;
                }
                case 0x520b:
                {
                    string str5 = (URLs.AccountInfoURL + "?section=codes&u=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&s=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "")) + "&lang=" + Program.mySettings.LanguageIdent.ToLower();
                    try
                    {
                        Process.Start(str5);
                    }
                    catch (Exception)
                    {
                        MyMessageBox.Show(SK.Text("ERROR_Browser1", "Stronghold Kingdoms encountered an error when trying to open your system's Default Web Browser. Please check that your web browser is working correctly and there are no unresponsive copies showing in task manager->Processes and then try again.") + Environment.NewLine + Environment.NewLine + SK.Text("ERROR_Browser2", "If this problem persists, please contact support."), SK.Text("ERROR_Browser3", "Error opening Web Browser"));
                    }
                    return;
                }
                case 0x5211:
                    CreateVacationWindow.showVacationMode();
                    return;

                case 0x2a2f:
                    try
                    {
                        string str2 = "http://login.strongholdkingdoms.com/support/?u=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&s=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "") + "&lang=" + Program.mySettings.languageIdent;
                        new Process { StartInfo = { FileName = str2 } }.Start();
                    }
                    catch (Exception)
                    {
                    }
                    return;

                case 0x23f2:
                    this.nextPlaybackCountries = false;
                    if (!GameEngine.Instance.World.gotPlaybackData())
                    {
                        this.retrieveGameStats();
                        return;
                    }
                    GameEngine.Instance.World.playbackProvinces();
                    return;

                case 0x23f3:
                    GameEngine.Instance.World.stopPlayback();
                    return;

                case 0x4b1:
                    PostTutorialWindow.CreatePostTutorialWindow(false);
                    return;

                case 0x8ab:
                {
                    if (!GameEngine.Instance.World.MapEditing)
                    {
                        GameEngine.Instance.World.MapEditing = true;
                        return;
                    }
                    int villageID = GameEngine.Instance.World.lastClickedVillage();
                    if (GameEngine.Instance.World.isCountyCapital(villageID) && !GameEngine.Instance.World.isVillageVisible(villageID))
                    {
                        RemoteServices.Instance.CompleteVillageCastle(villageID, 0x15);
                    }
                    GameEngine.Instance.World.MapEditing = false;
                    return;
                }
                case 0x44d:
                    InterfaceMgr.Instance.getMainTabBar().selectDummyTab(2);
                    GameEngine.Instance.InitCastleAttackSetup();
                    return;

                case 0x44e:
                    GameEngine.Instance.SkipVillageTab();
                    InterfaceMgr.Instance.getMainTabBar().changeTab(1);
                    InterfaceMgr.Instance.getVillageTabBar().changeTab(1);
                    CastleMap.CreateMode = true;
                    return;

                case 0x455:
                    GameEngine.Instance.playInterfaceSound("Options_resume_tutorial");
                    GameEngine.Instance.World.resumeTutorial();
                    return;

                case 0x456:
                case 0x457:
                case 0x458:
                case 0x459:
                case 0x45a:
                case 0x45b:
                case 0x45c:
                case 0x45d:
                case 0x45e:
                case 0x45f:
                    CastleMap.FakeKeep = (id - 0x456) + 1;
                    return;

                case 0x460:
                case 0x461:
                case 0x462:
                case 0x463:
                    CastleMap.FakeDefensiveMode = id - 0x460;
                    return;

                case 0x3e9:
                    InterfaceMgr.Instance.ParentForm.Close();
                    return;

                case 0xe7:
                    GameEngine.Instance.World.DrawDebugNames = !GameEngine.Instance.World.DrawDebugNames;
                    GameEngine.Instance.World.DrawDebugVillageNames = false;
                    return;

                case 0xe8:
                    GameEngine.Instance.World.DrawDebugVillageNames = !GameEngine.Instance.World.DrawDebugVillageNames;
                    GameEngine.Instance.World.DrawDebugNames = false;
                    return;

                case 300:
                    InterfaceMgr.Instance.getMainTabBar().selectDummyTab(60);
                    return;

                case 0xdd:
                    this.createIngameMessage();
                    return;

                case 0xdf:
                    this.clearIngameMessage();
                    return;

                case 0xd1:
                {
                    MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
                    if (MyMessageBox.Show("This call is not entirely 'game friendly'. Only use sparingly and at quiet game times and make sure no one else is using them same function!", "Admin Warning!", yesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 0) == DialogResult.Yes)
                    {
                        this.fixCommandSent = true;
                        RemoteServices.Instance.set_CompleteVillageCastle_UserCallBack(new RemoteServices.CompleteVillageCastle_UserCallBack(this.CompleteVillageCastleCallBack));
                        RemoteServices.Instance.CompleteVillageCastle(InterfaceMgr.Instance.getSelectedMenuVillage(), 15);
                    }
                    return;
                }
                case 0x97:
                    try
                    {
                        new Process { StartInfo = { FileName = URLs.TermsAndConditions } }.Start();
                    }
                    catch (Exception)
                    {
                    }
                    return;

                case 0x98:
                    try
                    {
                        new Process { StartInfo = { FileName = URLs.PrivacyPolicy } }.Start();
                    }
                    catch (Exception)
                    {
                    }
                    return;

                case 0xc9:
                    AdminInfoPopup.showAdminEdit();
                    return;

                case 0xca:
                    this.nextPlaybackCountries = true;
                    if (!GameEngine.Instance.World.gotPlaybackData())
                    {
                        this.retrieveGameStats();
                        return;
                    }
                    GameEngine.Instance.World.playbackCountries();
                    return;

                case 0xcb:
                    this.retrieveGameInfo();
                    return;

                case 1:
                    GameEngine.Instance.playInterfaceSound("Options_open");
                    OptionsPopup.openSettings();
                    return;

                case 2:
                    InterfaceMgr.Instance.openLogoutWindow(true);
                    return;

                case 5:
                    InterfaceMgr.Instance.getMainTabBar().selectDummyTab(10);
                    return;

                case 9:
                {
                    if (!RemoteServices.Instance.Admin || !GameEngine.Instance.World.DrawDebugVillageNames)
                    {
                        int ownSelectedVillage = InterfaceMgr.Instance.OwnSelectedVillage;
                        if (((ownSelectedVillage >= 0) && !GameEngine.Instance.World.isCapital(ownSelectedVillage)) && GameEngine.Instance.World.isUserVillage(ownSelectedVillage))
                        {
                            RenameVillagePopup popup2 = new RenameVillagePopup();
                            popup2.setVillageID(ownSelectedVillage, GameEngine.Instance.World.getVillageNameOnly(ownSelectedVillage));
                            popup2.Show(InterfaceMgr.Instance.ParentForm);
                            return;
                        }
                        MyMessageBox.Show(SK.Text("MENU_Cannot_Rename", "You cannot rename this village."), SK.Text("MENU_Rename_Error", "Rename Error"));
                        return;
                    }
                    int selectedVillage = InterfaceMgr.Instance.SelectedVillage;
                    RenameVillagePopup popup = new RenameVillagePopup();
                    popup.setVillageID(selectedVillage, GameEngine.Instance.World.getVillageNameOnly(selectedVillage));
                    popup.Show(InterfaceMgr.Instance.ParentForm);
                    return;
                }
                case 11:
                {
                    int num3 = InterfaceMgr.Instance.OwnSelectedVillage;
                    if (((num3 < 0) || GameEngine.Instance.World.isCapital(num3)) || !GameEngine.Instance.World.isUserVillage(num3))
                    {
                        MyMessageBox.Show(SK.Text("MENU_Cannot_Abandon", "You cannot abandon this village."), SK.Text("GENERIC_Error", "Error"));
                        return;
                    }
                    GameEngine.Instance.villageToAbandon = num3;
                    return;
                }
                case 12:
                {
                    int num4 = InterfaceMgr.Instance.OwnSelectedVillage;
                    if (((num4 < 0) || GameEngine.Instance.World.isCapital(num4)) || !GameEngine.Instance.World.isUserVillage(num4))
                    {
                        return;
                    }
                    InterfaceMgr.Instance.changeTab(1);
                    VillageMap map = GameEngine.Instance.getVillage(num4);
                    if ((map == null) || (map.m_nextMapTypeChange <= VillageMap.getCurrentServerTime()))
                    {
                        InterfaceMgr.Instance.openBuyVillageWindow(num4, false);
                        return;
                    }
                    TimeSpan span = (TimeSpan) (map.m_nextMapTypeChange - VillageMap.getCurrentServerTime());
                    str = "";
                    if (span.Days <= 0)
                    {
                        str = string.Format("{0:D1} " + SK.Text("MENU_hours_short", "hrs") + ", {1:D2} " + SK.Text("MENU_minutes_short", "mins"), span.Hours, span.Minutes);
                        break;
                    }
                    str = string.Format("{0:D2} " + SK.Text("MENU_days", "days") + ", {1:D2} " + SK.Text("MENU_hours_short", "hrs") + ", {2:D2} " + SK.Text("MENU_minutes_short", "mins"), span.Days, span.Hours, span.Minutes);
                    break;
                }
                case 0x65:
                    new Process { StartInfo = { FileName = "readme.txt" } }.Start();
                    return;

                case 0x66:
                {
                    AboutPopup popup3 = new AboutPopup();
                    popup3.init();
                    popup3.Show();
                    return;
                }
                case 0x67:
                    AdminInfoPopup.showMessage();
                    return;

                case 0x69:
                    try
                    {
                        new Process { StartInfo = { FileName = URLs.FireflyHomepage } }.Start();
                    }
                    catch (Exception)
                    {
                    }
                    return;

                case 0x6b:
                    try
                    {
                        new Process { StartInfo = { FileName = "http://login.strongholdkingdoms.com/forum/?u=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&s=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "") + "&lang=" + Program.mySettings.languageIdent } }.Start();
                    }
                    catch (Exception)
                    {
                    }
                    return;

                case 0x6c:
                    try
                    {
                        new Process { StartInfo = { FileName = URLs.WikiPage } }.Start();
                    }
                    catch (Exception)
                    {
                    }
                    return;

                case 0x6d:
                    try
                    {
                        new Process { StartInfo = { FileName = URLs.IPSharingPage } }.Start();
                    }
                    catch (Exception)
                    {
                    }
                    return;

                case 0x70:
                    try
                    {
                        new Process { StartInfo = { FileName = URLs.TechnicalFAQPage } }.Start();
                    }
                    catch (Exception)
                    {
                    }
                    return;

                case 0x79:
                    try
                    {
                        new Process { StartInfo = { FileName = URLs.WikiPage } }.Start();
                    }
                    catch (Exception)
                    {
                    }
                    return;

                default:
                    return;
            }
            MyMessageBox.Show(SK.Text("MENU_Cannot_Change_Type", "You cannot change this Village's Type for") + " : " + str, SK.Text("MENU_Change_Type_Error", "Change Village Type Error"));
        }

        public void newMail(bool newMail)
        {
            this.MenuButtonsPanel.newMail(newMail);
        }

        public void resize()
        {
            this.mainBackgroundImage.Size = new Size(this.Size.Width, 0x1d);
            this.btnHelpMenu.Position = new Point(-140 + base.Width, 1);
            this.btnLogOut.Position = new Point(-72 + base.Width, 1);
            this.btnFileMenu.Position = new Point(-217 + base.Width, 1);
            this.btnMyAccount.Position = new Point(-297 + base.Width, 1);
            this.pnlLoadLight.Position = new Point(base.Width - 4, 0);
            this.MenuButtonsPanel.Position = new Point(-459 + base.Width, 0);
        }

        public void retrieveGameInfo()
        {
            RemoteServices.Instance.set_GetAdminStats_UserCallBack(new RemoteServices.GetAdminStats_UserCallBack(this.getAdminStatsCallback));
            RemoteServices.Instance.GetAdminStats();
        }

        public void retrieveGameStats()
        {
            RemoteServices.Instance.set_RetrieveStats_UserCallBack(new RemoteServices.RetrieveStats_UserCallBack(this.retrieveStatsCallback));
            RemoteServices.Instance.RetrieveStats();
        }

        public void retrieveStatsCallback(RetrieveStats_ReturnType returnData)
        {
            if (returnData.Success && (returnData.mapHistory != null))
            {
                GameEngine.Instance.World.setPlaybackData(returnData.mapHistory, returnData.worldStartTime);
                if (this.nextPlaybackCountries)
                {
                    GameEngine.Instance.World.playbackCountries();
                }
                else
                {
                    GameEngine.Instance.World.playbackProvinces();
                }
            }
        }

        public void setAdmin()
        {
            if (RemoteServices.Instance.Admin)
            {
                this.btnAdminMenu.Visible = true;
            }
            else
            {
                this.btnAdminMenu.Visible = false;
                this.btnCombat.Visible = false;
            }
        }

        public void setLoadingLight(bool loading)
        {
            this.pnlLoadLight.Visible = loading;
        }

        public void setMailAlpha(double alpha)
        {
            this.MenuButtonsPanel.setMailAlpha(alpha);
        }

        public void setServerTime(DateTime serverTime, int gameDay)
        {
            int num = GameEngine.Instance.World.getPlaybackDay();
            if (num >= 0)
            {
                InterfaceMgr.Instance.getTopLeftMenu().setServerTime(num.ToString());
            }
            else
            {
                bool flag = false;
                if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                {
                    TimeSpan span = GameEngine.Instance.getDominationTimeLeft();
                    if (span.TotalHours < 15.0)
                    {
                        flag = true;
                    }
                    int totalSeconds = (int) span.TotalSeconds;
                    int num3 = totalSeconds % 60;
                    int num4 = (totalSeconds / 60) % 60;
                    int num5 = (totalSeconds / 0xe10) % 0x18;
                    int num6 = totalSeconds / 0x15180;
                    if (span.TotalHours >= 24.0)
                    {
                        string text = num6.ToString() + SK.Text("VillageMap_Day_Abbrev", "d") + ":";
                        if (num5 == 0)
                        {
                            text = text + "00:";
                        }
                        else if (num5 < 10)
                        {
                            text = text + "0" + num5.ToString() + ":";
                        }
                        else
                        {
                            text = text + num5.ToString() + ":";
                        }
                        if (num4 == 0)
                        {
                            text = text + "00:";
                        }
                        else if (num4 < 10)
                        {
                            text = text + "0" + num4.ToString() + ":";
                        }
                        else
                        {
                            text = text + num4.ToString() + ":";
                        }
                        if (num3 == 0)
                        {
                            text = text + "00";
                        }
                        else if (num3 < 10)
                        {
                            text = text + "0" + num3.ToString();
                        }
                        else
                        {
                            text = text + num3.ToString();
                        }
                        if (flag)
                        {
                            InterfaceMgr.Instance.getTopLeftMenu().setServerTime(SK.Text("Dom_Time_Left", "Time Remaining") + " " + text);
                        }
                        InterfaceMgr.Instance.updateDominationWindow(text);
                    }
                    else
                    {
                        string str2 = "";
                        if (num5 == 0)
                        {
                            str2 = str2 + "00:";
                        }
                        else if (num5 < 10)
                        {
                            str2 = str2 + "0" + num5.ToString() + ":";
                        }
                        else
                        {
                            str2 = str2 + num5.ToString() + ":";
                        }
                        if (num4 == 0)
                        {
                            str2 = str2 + "00:";
                        }
                        else if (num4 < 10)
                        {
                            str2 = str2 + "0" + num4.ToString() + ":";
                        }
                        else
                        {
                            str2 = str2 + num4.ToString() + ":";
                        }
                        if (num3 == 0)
                        {
                            str2 = str2 + "00";
                        }
                        else if (num3 < 10)
                        {
                            str2 = str2 + "0" + num3.ToString();
                        }
                        else
                        {
                            str2 = str2 + num3.ToString();
                        }
                        if (flag)
                        {
                            InterfaceMgr.Instance.getTopLeftMenu().setServerTime(SK.Text("Dom_Time_Left", "Time Remaining") + " " + str2);
                        }
                        InterfaceMgr.Instance.updateDominationWindow(str2);
                    }
                }
                if (!flag)
                {
                    InterfaceMgr.Instance.getTopLeftMenu().setServerTime(SK.Text("MENU_Day_X", "Day") + " " + gameDay.ToString() + " - " + serverTime.ToLongTimeString());
                }
            }
        }

        public void shutdownMessage()
        {
        }

        public static void VillageRenameCallback(VillageRename_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.abandoned)
                {
                    GameEngine.Instance.World.newPlayer = false;
                    GameEngine.Instance.World.lastAttacker = RemoteServices.Instance.UserName;
                    GameEngine.Instance.World.lastAttackerLastUpdate = DateTime.Now;
                    GameEngine.Instance.flushVillages();
                    GameEngine.Instance.forceFullTick();
                }
            }
            else
            {
                if (returnData.m_errorCode == ErrorCodes.ErrorCode.ABANDONED_TOO_SOON)
                {
                    MyMessageBox.Show(SK.Text("MENU_Abandon_Once_Week", "You can only abandon your village once a week"), SK.Text("MENU_Abandon_Village_Error", "Abandon Village Error"));
                }
                if (returnData.m_errorCode == ErrorCodes.ErrorCode.CANT_ABANDON_WITH_INCOMING_ATTACKS)
                {
                    MyMessageBox.Show(SK.Text("MENU_Abandon_Incoming", "You cannot abandon your village while you have incoming attacks"), SK.Text("MENU_Abandon_Village_Error", "Abandon Village Error"));
                }
            }
        }

        public static bool CastleCopyMode
        {
            get
            {
                return (castleCopyMode && RemoteServices.Instance.Admin);
            }
        }
    }
}

