namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class PostTutorialPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDImage background = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDArea backgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton btnLogout = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDButton feature10Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton feature1Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton feature2Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton feature3Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton feature4Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton feature5Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton feature6Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton feature7Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton feature8Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton feature9Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel header1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel header2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel header3Label = new CustomSelfDrawPanel.CSDLabel();
        private PostTutorialWindow m_parent;
        private CustomSelfDrawPanel.CSDCheckBox showCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDFill transparentBackground = new CustomSelfDrawPanel.CSDFill();

        public PostTutorialPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void checkToggled()
        {
            Program.mySettings.showGameFeaturesScreenIcon = this.showCheck.Checked;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Point getIconPosition(int id, int total)
        {
            if ((id == 8) && (total == 9))
            {
                return new Point(0x11f, 0x1b9);
            }
            int num = id % 4;
            int num2 = id / 4;
            if (num2 == 2)
            {
                num++;
            }
            return new Point(0xad + (num * 0x4a), 0x13b + (num2 * 0x3f));
        }

        private void iconClicked()
        {
            if (base.ClickedControl != null)
            {
                switch (base.ClickedControl.Data)
                {
                    case 0:
                        InterfaceMgr.Instance.getMainTabBar().changeTab(3);
                        PostTutorialWindow.close();
                        return;

                    case 1:
                        InterfaceMgr.Instance.getMainTabBar().changeTab(4);
                        PostTutorialWindow.close();
                        return;

                    case 2:
                        InterfaceMgr.Instance.getMainTabBar().changeTab(4);
                        PostTutorialWindow.close();
                        return;

                    case 3:
                        InterfaceMgr.Instance.getMainTabBar().changeTab(5);
                        PostTutorialWindow.close();
                        return;

                    case 4:
                        InterfaceMgr.Instance.getMainTabBar().changeTab(7);
                        PostTutorialWindow.close();
                        return;

                    case 5:
                        Process.Start(URLs.shieldDesignerURL + "?UserGUID=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&SessionGUID=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "") + "&lang=" + Program.mySettings.LanguageIdent.ToLower());
                        return;

                    case 6:
                        InterfaceMgr.Instance.getMainTabBar().selectDummyTab(10);
                        PostTutorialWindow.close();
                        return;

                    case 7:
                    {
                        string fileName = (URLs.InviteAFriendURL + "?u=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&s=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "")) + "&lang=" + Program.mySettings.LanguageIdent.ToLower();
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
                    case 8:
                        InterfaceMgr.Instance.getMainTabBar().changeTab(2);
                        PostTutorialWindow.close();
                        return;

                    case 9:
                        if (!InterfaceMgr.Instance.isMailDocked())
                        {
                            if (InterfaceMgr.Instance.mailScreenNeedsOpening())
                            {
                                InterfaceMgr.Instance.initMailSubTab(0);
                            }
                            else
                            {
                                InterfaceMgr.Instance.mailScreenRePop();
                            }
                            break;
                        }
                        InterfaceMgr.Instance.getMainTabBar().selectDummyTab(0x15);
                        break;

                    default:
                        return;
                }
                PostTutorialWindow.close();
            }
        }

        public void init(bool fromTutorial, PostTutorialWindow parent)
        {
            this.m_parent = parent;
            base.clearControls();
            int total = 10;
            if ((GameEngine.Instance.World.isBigpointAccount || Program.bigpointInstall) || (Program.aeriaInstall || Program.bigpointPartnerInstall))
            {
                total = 9;
            }
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
            if (fromTutorial)
            {
                this.header3Label.Text = SK.Text("PT_TUT_header1", "Congratulations!");
                this.header3Label.Position = new Point(8, 0xd8);
                this.header3Label.Size = new Size(this.backgroundArea.Width, 150);
                this.header3Label.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
                this.header3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.header3Label.Color = ARGBColors.Black;
                this.header3Label.DropShadowColor = ARGBColors.LightGray;
                this.backgroundArea.addControl(this.header3Label);
                this.header1Label.Text = SK.Text("PT_TUT_header2", "You have completed the Stronghold Kingdoms Tutorial.");
                this.header1Label.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
            }
            else
            {
                this.header1Label.Text = SK.Text("PT_header1", "Welcome to the Stronghold Kingdoms Player Guide");
                this.header1Label.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
            }
            this.header1Label.Position = new Point(8, 0x100);
            this.header1Label.Size = new Size(this.backgroundArea.Width, 150);
            this.header1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.header1Label.Color = ARGBColors.Black;
            this.header1Label.DropShadowColor = ARGBColors.LightGray;
            this.backgroundArea.addControl(this.header1Label);
            this.header2Label.Text = SK.Text("PT_header2", "Here are a few suggestions for what to do next") + ":";
            this.header2Label.Position = new Point(0x6c, 0x115);
            this.header2Label.Size = new Size(this.backgroundArea.Width - 200, 0x22);
            this.header2Label.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.header2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.header2Label.Color = ARGBColors.Black;
            this.header2Label.DropShadowColor = ARGBColors.LightGray;
            this.backgroundArea.addControl(this.header2Label);
            int num2 = 0;
            this.feature1Button.ImageNorm = (Image) GFXLibrary.pt_Research;
            this.feature1Button.ImageOver = (Image) GFXLibrary.pt_Research_over;
            this.feature1Button.ImageClick = (Image) GFXLibrary.pt_Research_down;
            this.feature1Button.Position = this.getIconPosition(num2++, total);
            this.feature1Button.Data = 0;
            this.feature1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
            this.feature1Button.CustomTooltipID = 0x10cc;
            this.background.addControl(this.feature1Button);
            this.feature2Button.ImageNorm = (Image) GFXLibrary.pt_rank;
            this.feature2Button.ImageOver = (Image) GFXLibrary.pt_rank_over;
            this.feature2Button.ImageClick = (Image) GFXLibrary.pt_rank_down;
            this.feature2Button.Position = this.getIconPosition(num2++, total);
            this.feature2Button.Data = 1;
            this.feature2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
            this.feature2Button.CustomTooltipID = 0x10cd;
            this.background.addControl(this.feature2Button);
            this.feature3Button.ImageNorm = (Image) GFXLibrary.pt_Achievements;
            this.feature3Button.ImageOver = (Image) GFXLibrary.pt_Achievements_over;
            this.feature3Button.ImageClick = (Image) GFXLibrary.pt_Achievements_down;
            this.feature3Button.Position = this.getIconPosition(num2++, total);
            this.feature3Button.Data = 2;
            this.feature3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
            this.feature3Button.CustomTooltipID = 0x10ce;
            this.background.addControl(this.feature3Button);
            this.feature4Button.ImageNorm = (Image) GFXLibrary.pt_Quests;
            this.feature4Button.ImageOver = (Image) GFXLibrary.pt_Quests_over;
            this.feature4Button.ImageClick = (Image) GFXLibrary.pt_Quests_down;
            this.feature4Button.Position = this.getIconPosition(num2++, total);
            this.feature4Button.Data = 3;
            this.feature4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
            this.feature4Button.CustomTooltipID = 0x10cf;
            this.background.addControl(this.feature4Button);
            this.feature5Button.ImageNorm = (Image) GFXLibrary.pt_Reports;
            this.feature5Button.ImageOver = (Image) GFXLibrary.pt_Reports_over;
            this.feature5Button.ImageClick = (Image) GFXLibrary.pt_Reports_down;
            this.feature5Button.Position = this.getIconPosition(num2++, total);
            this.feature5Button.Data = 4;
            this.feature5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
            this.feature5Button.CustomTooltipID = 0x10d0;
            this.background.addControl(this.feature5Button);
            this.feature6Button.ImageNorm = (Image) GFXLibrary.pt_Coat_of_Arms;
            this.feature6Button.ImageOver = (Image) GFXLibrary.pt_Coat_of_Arms_over;
            this.feature6Button.ImageClick = (Image) GFXLibrary.pt_Coat_of_Arms_down;
            this.feature6Button.Position = this.getIconPosition(num2++, total);
            this.feature6Button.Data = 5;
            this.feature6Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
            this.feature6Button.CustomTooltipID = 0x10d1;
            this.background.addControl(this.feature6Button);
            this.feature7Button.ImageNorm = (Image) GFXLibrary.pt_Avatar;
            this.feature7Button.ImageOver = (Image) GFXLibrary.pt_Avatar_over;
            this.feature7Button.ImageClick = (Image) GFXLibrary.pt_Avatar_down;
            this.feature7Button.Position = this.getIconPosition(num2++, total);
            this.feature7Button.Data = 6;
            this.feature7Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
            this.feature7Button.CustomTooltipID = 0x10d2;
            this.background.addControl(this.feature7Button);
            if (total == 10)
            {
                this.feature8Button.ImageNorm = (Image) GFXLibrary.pt_Invite_a_Friend;
                this.feature8Button.ImageOver = (Image) GFXLibrary.pt_Invite_a_Friend_over;
                this.feature8Button.ImageClick = (Image) GFXLibrary.pt_Invite_a_Friend_down;
                this.feature8Button.Position = this.getIconPosition(num2++, total);
                this.feature8Button.Data = 7;
                this.feature8Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
                this.feature8Button.CustomTooltipID = 0x10d3;
                this.background.addControl(this.feature8Button);
            }
            this.feature9Button.ImageNorm = (Image) GFXLibrary.pt_Parish_Wall;
            this.feature9Button.ImageOver = (Image) GFXLibrary.pt_Parish_Wall_over;
            this.feature9Button.ImageClick = (Image) GFXLibrary.pt_Parish_Wall_down;
            this.feature9Button.Position = this.getIconPosition(num2++, total);
            this.feature9Button.Data = 8;
            this.feature9Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
            this.feature9Button.CustomTooltipID = 0x10d4;
            this.background.addControl(this.feature9Button);
            this.feature10Button.ImageNorm = (Image) GFXLibrary.pt_Mail;
            this.feature10Button.ImageOver = (Image) GFXLibrary.pt_Mail_over;
            this.feature10Button.ImageClick = (Image) GFXLibrary.pt_Mail_down;
            this.feature10Button.Position = this.getIconPosition(num2++, total);
            this.feature10Button.Data = 9;
            this.feature10Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
            this.feature10Button.CustomTooltipID = 0x10d5;
            this.background.addControl(this.feature10Button);
            this.btnLogout.ImageNorm = (Image) GFXLibrary.worldSelect_swap_norm;
            this.btnLogout.ImageOver = (Image) GFXLibrary.worldSelect_swap_over;
            this.btnLogout.ImageClick = (Image) GFXLibrary.worldSelect_swap_pushed;
            this.btnLogout.Position = new Point(0xf5, 0x204);
            this.btnLogout.Text.Text = SK.Text("GENERIC_Close", "Close");
            this.btnLogout.TextYOffset = -2;
            this.btnLogout.Text.Color = ARGBColors.White;
            this.btnLogout.Text.DropShadowColor = ARGBColors.Black;
            this.btnLogout.Text.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
            this.btnLogout.Text.Position = new Point(-3, 0);
            this.btnLogout.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.logoutClick));
            this.btnLogout.Enabled = true;
            this.backgroundArea.addControl(this.btnLogout);
            this.showCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.showCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.showCheck.Position = new Point(0xe1, 0x1ee);
            this.showCheck.Checked = Program.mySettings.showGameFeaturesScreenIcon;
            this.showCheck.CBLabel.Text = SK.Text("PT_show_icon", "Show Player Guide icon");
            this.showCheck.CBLabel.Color = ARGBColors.Black;
            this.showCheck.CBLabel.Position = new Point(20, -1);
            this.showCheck.CBLabel.Size = new Size(360, 0x23);
            this.showCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.showCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            this.backgroundArea.addControl(this.showCheck);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.Name = "PostTutorialPanel";
            base.Size = new Size(600, 0x37);
            base.ResumeLayout(false);
        }

        private void logoutClick()
        {
            PostTutorialWindow.close();
        }
    }
}

