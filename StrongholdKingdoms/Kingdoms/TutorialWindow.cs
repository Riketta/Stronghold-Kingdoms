namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TutorialWindow : Form
    {
        private IContainer components;
        private TutorialPanel customPanel;
        private static Form lastParent;
        public static bool overIcon;
        public static int tutorialWindowOverForm;

        public TutorialWindow()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        public void closing()
        {
            this.customPanel.closing();
        }

        public static void CreateTutorialWindow(int tutorialID, Form parentWindow)
        {
            bool force = false;
            TutorialWindow window = InterfaceMgr.Instance.getTutorialWindow();
            if (window == null)
            {
                window = new TutorialWindow();
                force = true;
            }
            else
            {
                if (parentWindow != lastParent)
                {
                    window.Close();
                    window = new TutorialWindow();
                    force = true;
                }
                if (!window.Created || !window.Visible)
                {
                    force = true;
                }
            }
            if (window != null)
            {
                lastParent = parentWindow;
                window.setText(tutorialID, force);
                window.updateLocation(tutorialID, parentWindow);
                window.showTutorialWindow(force, parentWindow);
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

        public void hidingTooltip()
        {
        }

        private void InitializeComponent()
        {
            this.customPanel = new TutorialPanel();
            base.SuspendLayout();
            this.customPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.customPanel.Location = new Point(0, 0);
            this.customPanel.Name = "customPanel";
            this.customPanel.PanelActive = true;
            this.customPanel.Size = new Size(0x308, 0xcb);
            this.customPanel.StoredGraphics = null;
            this.customPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Fuchsia;
            base.ClientSize = new Size(0x308, 0xcb);
            base.ControlBox = false;
            base.Controls.Add(this.customPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MinimumSize = new Size(10, 10);
            base.Name = "TutorialWindow";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "TutorialWindow";
            base.TransparencyKey = ARGBColors.Fuchsia;
            base.ResumeLayout(false);
        }

        public static void runTutorial()
        {
            if (GameEngine.Instance.World.numVillagesOwned() != 0)
            {
                bool flag = true;
                if (InterfaceMgr.Instance.isTutorialWindowOpen() && !flag)
                {
                    InterfaceMgr.Instance.closeTutorialWindow();
                }
                if (flag && (GameEngine.Instance.World.isNewTutorialAvailable() && !GameEngine.Instance.isSelectNewVillageVisible()))
                {
                    int tutorialID = GameEngine.Instance.World.getTutorialStage();
                    switch (tutorialID)
                    {
                        case 0:
                            GameEngine.Instance.World.advanceTutorial();
                            break;

                        case -1:
                        case -3:
                            InterfaceMgr.Instance.closeTutorialWindow();
                            InterfaceMgr.Instance.ParentForm.TopMost = true;
                            InterfaceMgr.Instance.ParentForm.TopMost = false;
                            InterfaceMgr.Instance.closeTutorialArrowWindow();
                            break;

                        default:
                            CreateTutorialWindow(tutorialID, InterfaceMgr.Instance.ParentForm);
                            tutorialWindowOverForm = 0;
                            break;
                    }
                    GameEngine.Instance.World.tutorialPopupShown();
                }
            }
        }

        public void setText(int tutorialID, bool force)
        {
            this.customPanel.setText(tutorialID, this, force);
        }

        public void showTutorialWindow(bool doShow, Form parentWindow)
        {
            base.ResumeLayout(false);
            base.PerformLayout();
            InterfaceMgr.Instance.setCurrentTutorialWindow(this);
            if (parentWindow == null)
            {
                parentWindow = InterfaceMgr.Instance.ParentForm;
            }
            if (doShow)
            {
                base.Show(parentWindow);
            }
        }

        public static void tooltip(Point dxMousePos)
        {
            overIcon = false;
            if ((GameEngine.Instance.World.isTutorialActive() || Program.mySettings.showGameFeaturesScreenIcon) && ((dxMousePos.X < 0x40) && (dxMousePos.Y > (GameEngine.Instance.GFX.viewHeight() - 0x40))))
            {
                overIcon = true;
                if (GameEngine.Instance.World.isTutorialActive())
                {
                    CustomTooltipManager.MouseEnterTooltipArea(0x640);
                }
                else if (Program.mySettings.showGameFeaturesScreenIcon)
                {
                    CustomTooltipManager.MouseEnterTooltipArea(0x641);
                }
            }
        }

        public void update()
        {
            if (base.Visible && base.Created)
            {
                this.customPanel.update();
            }
            else
            {
                this.customPanel.invisiUpdate();
            }
        }

        public void updateLocation(int tutorialID, Form parentWindow)
        {
            if (!base.IsDisposed)
            {
                Point location = parentWindow.Location;
                location.X += 4;
                location.Y += (parentWindow.Height - base.Size.Height) - 4;
                base.Location = location;
            }
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }
    }
}

