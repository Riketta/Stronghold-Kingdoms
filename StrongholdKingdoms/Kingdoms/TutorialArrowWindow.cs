namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TutorialArrowWindow : Form
    {
        private IContainer components;
        private TutorialArrowPanel customPanel;
        public static Form lastParent;
        public AnchorStyles m_anchor = (AnchorStyles.Left | AnchorStyles.Top);
        public int m_animOffset;
        private int m_arrowAnimClock;
        private int m_arrowAnimOffset;
        public Point m_offset = new Point();
        private bool m_upArrow;

        public TutorialArrowWindow()
        {
            this.InitializeComponent();
        }

        public static void CreateTutorialArrowWindow(bool upArrow, Point offset, AnchorStyles anchor, Form parentWindow)
        {
            bool doShow = false;
            TutorialArrowWindow window = InterfaceMgr.Instance.getTutorialArrowWindow();
            if (window == null)
            {
                window = new TutorialArrowWindow();
                doShow = true;
            }
            else
            {
                if (parentWindow != lastParent)
                {
                    window.Close();
                    window = new TutorialArrowWindow();
                    doShow = true;
                }
                if (!window.Created || !window.Visible)
                {
                    doShow = true;
                }
            }
            if ((window != null) && ((doShow || (offset != window.m_offset)) || (anchor != window.m_anchor)))
            {
                lastParent = parentWindow;
                window.show(upArrow, offset, anchor);
                window.updateLocation(parentWindow);
                window.showTutorialArrowWindow(doShow, parentWindow);
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

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(TutorialArrowWindow));
            this.customPanel = new TutorialArrowPanel();
            base.SuspendLayout();
            this.customPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.customPanel.Location = new Point(0, 0);
            this.customPanel.Name = "customPanel";
            this.customPanel.PanelActive = true;
            this.customPanel.Size = new Size(0x40, 0x40);
            this.customPanel.StoredGraphics = null;
            this.customPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Fuchsia;
            base.ClientSize = new Size(0x40, 0x40);
            base.ControlBox = false;
            base.Controls.Add(this.customPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            this.MinimumSize = new Size(0x40, 0x40);
            base.Name = "TutorialArrowWindow";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "TutorialArrowWindow";
            base.TransparencyKey = ARGBColors.Fuchsia;
            base.ResumeLayout(false);
        }

        public void move()
        {
            if (base.Visible && base.Created)
            {
                this.updateLocation(lastParent);
            }
        }

        public void show(bool upArrow, Point offset, AnchorStyles anchor)
        {
            this.m_upArrow = upArrow;
            this.m_anchor = anchor;
            this.m_offset = offset;
            this.customPanel.show(upArrow, this);
        }

        public void showTutorialArrowWindow(bool doShow, Form parentWindow)
        {
            InterfaceMgr.Instance.setCurrentTutorialArrowWindow(this);
            if (parentWindow == null)
            {
                parentWindow = InterfaceMgr.Instance.ParentForm;
            }
            if (doShow)
            {
                base.Show(parentWindow);
            }
        }

        public void update()
        {
            this.m_arrowAnimClock++;
            if (this.m_arrowAnimClock < 20)
            {
                this.m_arrowAnimOffset = this.m_arrowAnimClock / 2;
            }
            else if (this.m_arrowAnimClock >= 40)
            {
                this.m_arrowAnimOffset = 0;
                this.m_arrowAnimClock = 0;
            }
            else
            {
                this.m_arrowAnimOffset = 20 - (this.m_arrowAnimClock / 2);
            }
            if (base.Visible && base.Created)
            {
                this.customPanel.update();
                if (this.m_arrowAnimOffset != this.m_animOffset)
                {
                    this.m_animOffset = this.m_arrowAnimOffset;
                    this.updateLocation(lastParent);
                }
            }
        }

        public static void updateArrow()
        {
            TutorialArrowWindow window = InterfaceMgr.Instance.getTutorialArrowWindow();
            if (((window != null) && window.Created) && window.Visible)
            {
                window.update();
            }
        }

        public void updateLocation(Form parentWindow)
        {
            if (!base.IsDisposed)
            {
                int num = (parentWindow.Width - parentWindow.ClientSize.Width) / 2;
                int num2 = (parentWindow.Height - parentWindow.ClientSize.Height) - (2 * num);
                Point location = parentWindow.ClientRectangle.Location;
                location.X += parentWindow.Location.X + num;
                location.Y += (parentWindow.Location.Y + num) + num2;
                Size size = parentWindow.ClientRectangle.Size;
                if (this.m_anchor == AnchorStyles.None)
                {
                    Point point2 = InterfaceMgr.Instance.getVillageReportBackgroundLocation();
                    location.X += point2.X + this.m_offset.X;
                    location.Y += point2.Y + this.m_offset.Y;
                }
                else
                {
                    if ((this.m_anchor & AnchorStyles.Top) != AnchorStyles.None)
                    {
                        location.Y += this.m_offset.Y;
                    }
                    else
                    {
                        location.Y = (location.Y + size.Height) - this.m_offset.Y;
                    }
                    if ((this.m_anchor & AnchorStyles.Left) != AnchorStyles.None)
                    {
                        location.X += this.m_offset.X;
                    }
                    else
                    {
                        location.X = (location.X + size.Width) - this.m_offset.X;
                    }
                }
                if (this.m_upArrow)
                {
                    location.X -= 0x1c;
                    location.Y -= 8;
                    location.Y += this.m_animOffset;
                }
                else
                {
                    location.X -= 0x2f;
                    location.Y -= 0x24;
                    location.X -= this.m_animOffset;
                }
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

