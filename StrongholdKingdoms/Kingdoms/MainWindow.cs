namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MainWindow : Form
    {
        public static bool captureCloseMenuEvent;
        private IContainer components;
        private DXPanel dxBasePanel;
        private bool m_allowResizing;
        private ToolTip m_wndToolTip;
        private MainRightHandPanel mainRightHandPanel1;
        private Point origDXLoc = new Point();
        private Size origDXSize = new Size();
        private bool steamOverlayed;
        private TopLeftMenu2 topLeftMenu1;
        private TopRightMenu topRightMenu1;

        public MainWindow()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.mainRightHandPanel1.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.topLeftMenu1.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        public void allowResizing(bool state)
        {
            this.m_allowResizing = state;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void finaliseResize()
        {
            this.MainWindowLarge_ResizeEnd(null, null);
        }

        public DXPanel getDXBasePanel()
        {
            return this.dxBasePanel;
        }

        public FactionTabBar2 getFactionTabBar()
        {
            return this.topRightMenu1.getFactionTabBar();
        }

        public MainMenuBar2 getMainMenuBar()
        {
            return this.topRightMenu1.mainMenuBar;
        }

        public MainRightHandPanel getMainRightHandPanel()
        {
            return this.mainRightHandPanel1;
        }

        public MainTabBar2 getMainTabBar()
        {
            return this.topRightMenu1.getMainTabBar();
        }

        public TopLeftMenu2 getTopLeftMenu()
        {
            return this.topLeftMenu1;
        }

        public TopRightMenu getTopRightMenu()
        {
            return this.topRightMenu1;
        }

        public VillageTabBar2 getVillageTabBar()
        {
            return this.topRightMenu1.getVillageTabBar();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            new ComponentResourceManager(typeof(MainWindow));
            this.m_wndToolTip = new ToolTip(this.components);
            this.dxBasePanel = new DXPanel();
            this.mainRightHandPanel1 = new MainRightHandPanel();
            this.topLeftMenu1 = new TopLeftMenu2();
            this.topRightMenu1 = new TopRightMenu();
            base.SuspendLayout();
            this.dxBasePanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.dxBasePanel.BackColor = ARGBColors.Black;
            this.dxBasePanel.Location = new Point(0, 120);
            this.dxBasePanel.Name = "dxBasePanel";
            this.dxBasePanel.Size = new Size(0x521, 0x3c6);
            this.dxBasePanel.TabIndex = 0;
            this.mainRightHandPanel1.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            this.mainRightHandPanel1.BackColor = ARGBColors.Red;
            this.mainRightHandPanel1.Font = new Font("Microsoft Sans Serif", 8.25f);
            this.mainRightHandPanel1.Location = new Point(0x521, 120);
            this.mainRightHandPanel1.Name = "mainRightHandPanel1";
            this.mainRightHandPanel1.Size = new Size(200, 0x3c6);
            this.mainRightHandPanel1.TabIndex = 2;
            this.topLeftMenu1.ClickThru = false;
            this.topLeftMenu1.Font = new Font("Microsoft Sans Serif", 8.25f);
            this.topLeftMenu1.Location = new Point(0, 0);
            this.topLeftMenu1.Name = "topLeftMenu1";
            this.topLeftMenu1.PanelActive = true;
            this.topLeftMenu1.Size = new Size(0x20f, 120);
            this.topLeftMenu1.StoredGraphics = null;
            this.topLeftMenu1.TabIndex = 4;
            this.topRightMenu1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.topRightMenu1.BackColor = ARGBColors.Red;
            this.topRightMenu1.ClickThru = false;
            this.topRightMenu1.Location = new Point(0x20f, 0);
            this.topRightMenu1.MinimumSize = new Size(0x1cf, 0);
            this.topRightMenu1.Name = "topRightMenu1";
            this.topRightMenu1.PanelActive = true;
            this.topRightMenu1.Size = new Size(0x3d9, 120);
            this.topRightMenu1.StoredGraphics = null;
            this.topRightMenu1.TabIndex = 5;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Black;
            base.ClientSize = new Size(0x5e8, 0x43e);
            base.Controls.Add(this.dxBasePanel);
            base.Controls.Add(this.mainRightHandPanel1);
            base.Controls.Add(this.topLeftMenu1);
            base.Controls.Add(this.topRightMenu1);
            base.Icon = Resources.shk_icon;
            this.MaximumSize = new Size(0x5f0, 0x460);
            this.MinimumSize = new Size(0x3e8, 720);
            base.Name = "MainWindow";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Stronghold Kingdoms";
            base.Deactivate += new EventHandler(this.MainWindowLarge_Deactivate);
            base.Load += new EventHandler(this.MainWindow_Load);
            base.ResizeBegin += new EventHandler(this.MainWindowLarge_ResizeBegin);
            base.SizeChanged += new EventHandler(this.MainWindowLarge_SizeChanged);
            base.Activated += new EventHandler(this.MainWindowLarge_Activated);
            base.FormClosed += new FormClosedEventHandler(this.MainWindowLarge_FormClosed);
            base.FormClosing += new FormClosingEventHandler(this.MainWindow_FormClosing);
            base.LocationChanged += new EventHandler(this.MainWindow_LocationChanged);
            base.ResizeEnd += new EventHandler(this.MainWindowLarge_ResizeEnd);
            base.ResumeLayout(false);
        }

        public bool isFullMainArea()
        {
            return !this.mainRightHandPanel1.Visible;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && (RemoteServices.Instance.UserID >= 0))
            {
                if (!InterfaceMgr.Instance.isLogoutPopupOpen())
                {
                    InterfaceMgr.Instance.openLogoutWindow(true);
                }
                e.Cancel = true;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Screen primaryScreen = Screen.PrimaryScreen;
            Point location = primaryScreen.WorkingArea.Location;
            location.X += (primaryScreen.WorkingArea.Width - base.Size.Width) / 2;
            location.Y += (primaryScreen.WorkingArea.Height - base.Size.Height) / 2;
            base.Location = location;
        }

        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            InterfaceMgr.Instance.movePlayCardsWindow();
            InterfaceMgr.Instance.moveLogoutWindow();
            InterfaceMgr.Instance.moveReportCaptureWindow();
            InterfaceMgr.Instance.moveScoutPopupWindow();
            InterfaceMgr.Instance.moveGreyOutWindow();
            InterfaceMgr.Instance.moveTutorialWindow();
            InterfaceMgr.Instance.moveTutorialArrowWindow();
            InterfaceMgr.Instance.moveFreeCardsPopup();
            InterfaceMgr.Instance.moveWheelPopup();
            InterfaceMgr.Instance.moveWheelSelectPopup();
            InterfaceMgr.Instance.moveAdvancedCastleOptionsPopup();
            InterfaceMgr.Instance.moveAchievementPopup();
        }

        private void MainWindowLarge_Activated(object sender, EventArgs e)
        {
            GameEngine.Instance.WindowActive = true;
        }

        private void MainWindowLarge_Deactivate(object sender, EventArgs e)
        {
            GameEngine.Instance.WindowActive = false;
        }

        private void MainWindowLarge_FormClosed(object sender, FormClosedEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.WindowsShutDown:
                case CloseReason.UserClosing:
                case CloseReason.ApplicationExitCall:
                    GameEngine.Instance.windowClosing();
                    break;

                case CloseReason.MdiFormClosing:
                    break;

                default:
                    return;
            }
        }

        private void MainWindowLarge_ResizeBegin(object sender, EventArgs e)
        {
            this.dxBasePanel.resizing = true;
            GameEngine.Instance.startResizeWindow();
        }

        private void MainWindowLarge_ResizeEnd(object sender, EventArgs e)
        {
            this.dxBasePanel.resizing = false;
            if (GameEngine.Instance != null)
            {
                GameEngine.Instance.resizeWindow();
                if (GameEngine.Instance.World != null)
                {
                    GameEngine.Instance.World.setScreenSize(this.dxBasePanel.Width, this.dxBasePanel.Height);
                }
            }
            Program.mySettings.ScreenWidth = base.ClientSize.Width;
            Program.mySettings.ScreenHeight = base.ClientSize.Height;
            this.topLeftMenu1.resize();
            this.topRightMenu1.resize();
        }

        public void MainWindowLarge_SizeChanged(object sender, EventArgs e)
        {
            if (this.m_allowResizing)
            {
                GameEngine.Instance.finaliseResize = true;
                if (this.steamOverlayed)
                {
                    this.origDXSize = new Size(base.ClientSize.Width - this.mainRightHandPanel1.Width, base.ClientSize.Height - 120);
                    this.dxBasePanel.Size = base.ClientSize;
                }
                else
                {
                    this.dxBasePanel.Width = base.ClientSize.Width - this.mainRightHandPanel1.Width;
                    this.dxBasePanel.Height = base.ClientSize.Height - 120;
                }
                this.mainRightHandPanel1.Height = base.ClientSize.Height - 120;
                this.mainRightHandPanel1.Location = new Point(base.ClientSize.Width - this.mainRightHandPanel1.Width, this.mainRightHandPanel1.Location.Y);
                this.topRightMenu1.Size = new Size(base.ClientSize.Width - this.topLeftMenu1.Width, this.topRightMenu1.Height);
                if (GameEngine.Instance != null)
                {
                    GameEngine.Instance.resizeWindow();
                    if (GameEngine.Instance.World != null)
                    {
                        GameEngine.Instance.World.setScreenSize(this.dxBasePanel.Width, this.dxBasePanel.Height);
                    }
                }
                if (base.ClientSize.Width >= 0x44c)
                {
                    this.topLeftMenu1.Size = new Size((base.ClientSize.Width / 2) + 0x56, this.topLeftMenu1.Size.Height);
                }
                else
                {
                    this.topLeftMenu1.Size = new Size(base.ClientSize.Width - 0x1cf, this.topLeftMenu1.Size.Height);
                }
                this.topRightMenu1.Size = new Size(base.ClientSize.Width - this.topLeftMenu1.Size.Width, this.topRightMenu1.Size.Height);
                this.topRightMenu1.Location = new Point(this.topLeftMenu1.Size.Width, this.topRightMenu1.Location.Y);
                this.topLeftMenu1.resize();
                this.topRightMenu1.resize();
            }
        }

        public void makeFullDX()
        {
            this.origDXLoc = this.dxBasePanel.Location;
            this.origDXSize = this.dxBasePanel.Size;
            this.dxBasePanel.Location = new Point(0, 0);
            this.dxBasePanel.Size = base.ClientSize;
            this.steamOverlayed = true;
            if (Program.arcInstall && (GameEngine.Instance.World != null))
            {
                GameEngine.Instance.World.setScreenSize(this.dxBasePanel.Width, this.dxBasePanel.Height);
            }
        }

        public void restoreDXSize()
        {
            this.dxBasePanel.Location = this.origDXLoc;
            this.dxBasePanel.Size = this.origDXSize;
            this.steamOverlayed = false;
            InterfaceMgr.Instance.reShowDXWindow();
            if (Program.arcInstall && (GameEngine.Instance.World != null))
            {
                GameEngine.Instance.World.setScreenSize(this.dxBasePanel.Width, this.dxBasePanel.Height);
            }
        }

        public void setMainAreaVisible(bool state)
        {
            this.mainRightHandPanel1.Visible = state;
            this.dxBasePanel.Visible = state;
        }

        public void setMainWindowAreaVisible(bool state)
        {
            this.dxBasePanel.Visible = state;
        }

        public void setTooltipText(Control control, string text)
        {
            this.m_wndToolTip.SetToolTip(control, text);
        }

        protected override void WndProc(ref Message m)
        {
            if ((m.Msg == 0x210) && captureCloseMenuEvent)
            {
                InterfaceMgr.Instance.closeMenuPopup();
            }
            else if ((m.Msg == 0x637) && Program.arcInstall)
            {
                if (((int) m.LParam) != 0)
                {
                    if (!this.steamOverlayed)
                    {
                        InterfaceMgr.Instance.ParentMainWindow.makeFullDX();
                        GameEngine.Instance.GFX.fullDeviceReset();
                    }
                    InterfaceMgr.Instance.closeAllPopups();
                    Program.arc_overlay_open = true;
                }
                else
                {
                    InterfaceMgr.Instance.ParentMainWindow.restoreDXSize();
                    GameEngine.Instance.GFX.resizeWindow();
                    Program.arc_overlay_open = false;
                }
                Program.arc_overlay_delay = 5;
            }
            base.WndProc(ref m);
        }
    }
}

