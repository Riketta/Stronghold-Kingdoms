namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MailScreenManager : UserControl, IDockableControl, IDockWindow
    {
        private Bitmap _backBuffer;
        private Image backgroundImage;
        private IContainer components;
        private int currentPanelHeight;
        private int currentPanelWidth;
        private DockableControl dockableControl;
        private bool docked = true;
        private DockWindow dockWindow;
        public bool forceBackgroundRedraw = true;
        private bool initScreen = true;
        private int lastFloatX;
        private int lastFloatY;
        private MailScreen mailScreen = new MailScreen();
        private bool openFresh = true;

        public MailScreenManager()
        {
            this.dockableControl = new DockableControl(this);
            this.dockWindow = new DockWindow(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        public void AddControl(UserControl control, int x, int y)
        {
            this.dockWindow.AddControl(control, x, y);
        }

        public void clearAllMail()
        {
        }

        public void clearStoredMail()
        {
            this.mailScreen.clearStoredMail();
        }

        public void close(bool forceClose)
        {
            if (forceClose || this.docked)
            {
                this.mailScreen.closeControl(true);
            }
        }

        public void closeControl(bool includePopups)
        {
            if (this.mailScreen.Visible)
            {
                this.mailScreen.closeAttachmentsPopup(true);
            }
            this.dockableControl.closeControl(includePopups);
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        public void display(ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(parent, x, y);
        }

        public void display(bool asPopup, ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(asPopup, parent, x, y);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void drawImageStretched(Graphics g, Image image, float x, float y, float width, float height)
        {
            RectangleF ef;
            if (image.Width == 1)
            {
                ef = new RectangleF(0f, 0f, 1E-05f, (float) image.Height);
            }
            else
            {
                ef = new RectangleF(0f, 0f, (float) image.Width, 1E-05f);
            }
            RectangleF destRect = new RectangleF(x, y, width, height);
            g.DrawImage(image, destRect, ef, GraphicsUnit.Pixel);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Black;
            base.Name = "MailScreenManager";
            base.Size = new Size(630, 0x1ca);
            base.ResumeLayout(false);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        public bool isDocked()
        {
            return this.docked;
        }

        public bool isMailScreenVisible()
        {
            return this.mailScreen.isVisible();
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        public void logout()
        {
            this.mailScreen.logout();
        }

        public void mailPopupNewMail()
        {
            if (!this.docked && this.mailScreen.isVisible())
            {
                this.mailScreen.refreshMail();
            }
        }

        public void mailTo(int userID, string userName)
        {
            this.startWithNewMessage(userID, userName);
        }

        public void mailTo(int userID, string[] userNames)
        {
            this.mailScreen.mailTo(userNames);
        }

        public void mailUpdate()
        {
            this.mailScreen.update();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if ((this._backBuffer == null) || this.forceBackgroundRedraw)
            {
                if (this._backBuffer == null)
                {
                    this._backBuffer = new Bitmap(base.ClientSize.Width, base.ClientSize.Height);
                }
                this.forceBackgroundRedraw = false;
                Graphics g = Graphics.FromImage(this._backBuffer);
                if (this.backgroundImage != null)
                {
                    for (int i = 0; i < base.ClientSize.Height; i += 0x200)
                    {
                        for (int j = 0; j < base.ClientSize.Width; j += 0x200)
                        {
                            g.DrawImageUnscaledAndClipped(this.backgroundImage, new Rectangle(j, i, 0x200, 0x200));
                        }
                    }
                }
                g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_topleft, 0, 0, 0x80, 0x80);
                g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_topright, base.ClientSize.Width - 0x80, 0, 0x80, 0x80);
                g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_bottomleft, 0, base.ClientSize.Height - 0x80, 0x80, 0x80);
                g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_bottomright, base.ClientSize.Width - 0x80, base.ClientSize.Height - 0x80, 0x80, 0x80);
                this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_top, 128f, 0f, (float) (base.ClientSize.Width - 0x100), 128f);
                this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_bottom, 128f, (float) (base.ClientSize.Height - 0x80), (float) (base.ClientSize.Width - 0x100), 128f);
                this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_left, 0f, 128f, 128f, (float) (base.ClientSize.Height - 0x100));
                this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_right, (float) (base.ClientSize.Width - 0x80), 128f, 128f, (float) (base.ClientSize.Height - 0x100));
                int num3 = ((base.ClientSize.Width - this.currentPanelWidth) / 2) + 8;
                int num4 = ((base.ClientSize.Height - this.currentPanelHeight) / 2) + 8;
                if ((num3 > 0) || (num4 > 0))
                {
                    g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_topleft, num3 - 0x80, num4 - 0x80, 0x80, 0x80);
                    g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_topright, num3 + this.currentPanelWidth, num4 - 0x80, 0x80, 0x80);
                    g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_bottomleft, num3 - 0x80, num4 + this.currentPanelHeight, 0x80, 0x80);
                    g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_bottomright, num3 + this.currentPanelWidth, num4 + this.currentPanelHeight, 0x80, 0x80);
                    if (num3 > 0)
                    {
                        this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_top, (float) num3, (float) (num4 - 0x80), (float) this.currentPanelWidth, 128f);
                        this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_bottom, (float) num3, (float) (num4 + this.currentPanelHeight), (float) this.currentPanelWidth, 128f);
                    }
                    if (num4 > 0)
                    {
                        this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_left, (float) (num3 - 0x80), (float) num4, 128f, (float) this.currentPanelHeight);
                        this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_right, (float) (num3 + this.currentPanelWidth), (float) num4, 128f, (float) this.currentPanelHeight);
                    }
                }
                g.Dispose();
            }
            if (e != null)
            {
                e.Graphics.DrawImageUnscaled(this._backBuffer, 0, 0);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (this._backBuffer != null)
            {
                this._backBuffer.Dispose();
                this._backBuffer = null;
                base.Invalidate();
            }
            base.OnSizeChanged(e);
        }

        public void open(bool forceDock, bool forceFloat)
        {
            if (forceDock)
            {
                this.docked = true;
            }
            if (forceFloat)
            {
                this.docked = false;
            }
            this.close(true);
            if (this.docked)
            {
                this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                this.mailScreen.initProperties(true, "Mail", this);
                this.mailScreen.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                this.mailScreen.display(false, this, (base.Size.Width - this.mailScreen.Size.Width) / 2, (base.Size.Height - this.mailScreen.Size.Height) / 2);
                this.mailScreen.BringToFront();
                if (this.initScreen)
                {
                    this.mailScreen.init(this);
                }
                this.mailScreen.open(this.openFresh, false);
                this.currentPanelWidth = this.mailScreen.Size.Width;
                this.currentPanelHeight = this.mailScreen.Size.Height;
                this.currentPanelWidth -= 0xa8;
                this.currentPanelHeight -= 0xa8;
                this.OnPaint(null);
            }
            else
            {
                if (this.initScreen)
                {
                    this.mailScreen.init(this);
                }
                this.mailScreen.open(this.openFresh, false);
                this.mailScreen.display(true, this, this.lastFloatX, this.lastFloatY);
            }
            this.initScreen = false;
            this.openFresh = true;
        }

        public void RemoveControl(UserControl control)
        {
            this.dockWindow.RemoveControl(control);
        }

        public void screenResize()
        {
            if (this.docked)
            {
                this.mailScreen.Location = new Point((base.Size.Width - this.mailScreen.Size.Width) / 2, (base.Size.Height - this.mailScreen.Size.Height) / 2);
            }
        }

        public void sendProclamation(int mailType, int areaID)
        {
            this.mailScreen.sendProclamation(mailType, areaID);
        }

        public void setAsDocked()
        {
            this.docked = true;
        }

        public void setAsReopen()
        {
            this.openFresh = false;
        }

        public void setBackgroundImage(Image image)
        {
            if (this.backgroundImage != image)
            {
                this.backgroundImage = image;
                this.forceBackgroundRedraw = true;
            }
        }

        public void startWithNewMessage(int userID, string userName)
        {
            this.mailScreen.mailTo(userName);
        }
    }
}

