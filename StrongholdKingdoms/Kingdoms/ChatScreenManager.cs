namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ChatScreenManager : UserControl, IDockableControl, IDockWindow
    {
        private Bitmap _backBuffer;
        private Image backgroundImage;
        private bool chatBanned;
        private ChatScreen chatScreen = new ChatScreen();
        private IContainer components;
        private int currentPanelHeight;
        private int currentPanelWidth;
        private DockableControl dockableControl;
        private bool docked;
        private DockWindow dockWindow;
        public bool forceBackgroundRedraw = true;
        private bool initScreen = true;
        private int lastFloatX;
        private int lastFloatY;

        public ChatScreenManager()
        {
            this.dockableControl = new DockableControl(this);
            this.dockWindow = new DockWindow(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint, true);
            this.dockableControl.setSizeableWindow();
        }

        public void AddControl(UserControl control, int x, int y)
        {
            this.dockWindow.AddControl(control, x, y);
        }

        private void Chat_Login_Callback(Chat_Login_ReturnType returnData)
        {
            if (returnData.Success)
            {
            }
        }

        public Form ChatForm()
        {
            if (this.chatScreen != null)
            {
                return this.chatScreen.ParentForm;
            }
            return null;
        }

        public void chatUpdate()
        {
            if (this.chatScreen.isActive())
            {
                this.chatScreen.update();
            }
        }

        public void close(bool forceClose, bool closingChat)
        {
            if (forceClose || this.docked)
            {
                this.chatScreen.closeControl(true);
                if (closingChat)
                {
                    RemoteServices.Instance.Chat_StopReceiving();
                }
            }
        }

        public void closeControl(bool includePopups)
        {
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
            base.Name = "ChatScreenManager";
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

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        public void login()
        {
            this.chatBanned = false;
            RemoteServices.Instance.set_Chat_Login_UserCallBack(new RemoteServices.Chat_Login_UserCallBack(this.Chat_Login_Callback));
            RemoteServices.Instance.Chat_Login();
        }

        public void logout()
        {
            RemoteServices.Instance.Chat_Logout();
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

        public void open(bool forceDock, bool forceFloat, int areaType, int areaID)
        {
            if (this.chatBanned)
            {
                MyMessageBox.Show(SK.Text("ChatScreenManager_Ban_Message_Chat", "Your chat privileges have been suspended for violations of the game rules or code of conduct.") + Environment.NewLine + URLs.IPSharingPage, SK.Text("GENERIC_Chat_Banned", "Chat Privileges Ban"));
            }
            else
            {
                if (forceDock)
                {
                    this.docked = true;
                }
                if (forceFloat)
                {
                    this.docked = false;
                }
                this.close(true, false);
                if (this.initScreen)
                {
                    this.chatScreen.initProperties(true, "Chat", this);
                    this.chatScreen.init(this);
                }
                RemoteServices.Instance.Chat_StartReceiving();
                this.chatScreen.openFresh(areaType, areaID);
                this.chatScreen.display(true, this, this.lastFloatX, this.lastFloatY);
                this.chatScreen.openUpdate();
                this.initScreen = false;
            }
        }

        public void RemoveControl(UserControl control)
        {
            this.dockWindow.RemoveControl(control);
        }

        public void screenResize()
        {
            if (this.docked)
            {
                this.chatScreen.Location = new Point((base.Size.Width - this.chatScreen.Size.Width) / 2, (base.Size.Height - this.chatScreen.Size.Height) / 2);
            }
        }

        public void setAsDocked()
        {
            this.docked = true;
        }

        public void setBackgroundImage(Image image)
        {
            if (this.backgroundImage != image)
            {
                this.backgroundImage = image;
                this.forceBackgroundRedraw = true;
            }
        }

        public void setChatBan(bool banned)
        {
            this.chatBanned = banned;
        }
    }
}

