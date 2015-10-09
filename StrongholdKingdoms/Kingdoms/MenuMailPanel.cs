namespace Kingdoms
{
    using System;
    using System.Drawing;

    public class MenuMailPanel : CustomSelfDrawPanel.CSDControl
    {
        private CustomSelfDrawPanel.CSDButton chatButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton leaderboardButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton mailButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage overlayIcon = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton premiumVOButton = new CustomSelfDrawPanel.CSDButton();

        public void chatClicked()
        {
            GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_chat");
            InterfaceMgr.Instance.initChatPanel(-1, -1);
        }

        public void init()
        {
            this.clearControls();
            this.premiumVOButton.ImageNorm = (Image) GFXLibrary.premium_menubar_normal;
            this.premiumVOButton.ImageOver = (Image) GFXLibrary.premium_menubar_over;
            this.premiumVOButton.Position = new Point(0, 0);
            this.premiumVOButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.voClicked));
            this.premiumVOButton.CustomTooltipID = 0x21;
            base.addControl(this.premiumVOButton);
            this.mailButton.ImageNorm = (Image) GFXLibrary.mail_menubar_open;
            this.mailButton.ImageOver = (Image) GFXLibrary.mail_menubar_open_bright;
            this.mailButton.Position = new Point(40, 0);
            this.mailButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailClicked));
            this.mailButton.CustomTooltipID = 0x1c;
            base.addControl(this.mailButton);
            this.overlayIcon.Image = (Image) GFXLibrary.mail_menubar_closed_bright;
            this.overlayIcon.Position = new Point(0, 0);
            this.overlayIcon.Visible = false;
            this.mailButton.addControl(this.overlayIcon);
            this.chatButton.ImageNorm = (Image) GFXLibrary.bubble_normal;
            this.chatButton.ImageOver = (Image) GFXLibrary.bubble_over;
            this.chatButton.Position = new Point(0x51, 0);
            this.chatButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.chatClicked));
            this.chatButton.CustomTooltipID = 0x35;
            base.addControl(this.chatButton);
            this.leaderboardButton.ImageNorm = (Image) GFXLibrary.points_menubar_normal;
            this.leaderboardButton.ImageOver = (Image) GFXLibrary.points_menubar_bright;
            this.leaderboardButton.Position = new Point(120, 0);
            this.leaderboardButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.leaderboardClicked));
            this.leaderboardButton.CustomTooltipID = 0x1d;
            base.addControl(this.leaderboardButton);
        }

        public void leaderboardClicked()
        {
            GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_leaderboard");
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(0x16);
        }

        public void mailClicked()
        {
            GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_mail");
            if (InterfaceMgr.Instance.isMailDocked())
            {
                InterfaceMgr.Instance.getMainTabBar().selectDummyTab(0x15);
            }
            else if (InterfaceMgr.Instance.mailScreenNeedsOpening())
            {
                InterfaceMgr.Instance.initMailSubTab(0);
            }
            else
            {
                InterfaceMgr.Instance.mailScreenRePop();
            }
        }

        public void newMail(bool newMail)
        {
            if (newMail)
            {
                this.mailButton.ImageNorm = (Image) GFXLibrary.mail_menubar_closed;
                this.mailButton.ImageOver = (Image) GFXLibrary.mail_menubar_closed_bright;
                this.overlayIcon.Visible = true;
            }
            else
            {
                this.mailButton.ImageNorm = (Image) GFXLibrary.mail_menubar_open;
                this.mailButton.ImageOver = (Image) GFXLibrary.mail_menubar_open_bright;
                this.overlayIcon.Visible = false;
            }
        }

        public void setMailAlpha(double alpha)
        {
            this.overlayIcon.Alpha = (float) alpha;
            this.overlayIcon.invalidate();
        }

        public void voClicked()
        {
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(100);
        }
    }
}

