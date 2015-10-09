namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Threading;
    using System.Windows.Forms;

    [ComVisible(true), PermissionSet(SecurityAction.Demand, Name="FullTrust")]
    public class FacebookWindow : MyFormBase
    {
        private IContainer components;
        public static string futureURL = "";
        private static FacebookWindow instance = null;
        private static ProfileLoginWindow m_parent = null;
        public static bool vidLoaded = false;
        private ExtendedWebBrowser webBrowser1;

        public event FacebookEventHandler login;

        public FacebookWindow()
        {
            this.InitializeComponent();
            this.Text = base.Title = SK.Text("Facebook_LOGIN", "Facebook Login");
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            this.webBrowser1.ObjectForScripting = this;
        }

        public static void closing()
        {
            if (m_parent != null)
            {
                m_parent.FacebookClose();
            }
            instance = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FacebookLogin(string userGuid)
        {
            if (m_parent != null)
            {
                m_parent.FacebookLogin(userGuid, "");
            }
            base.Close();
            instance = null;
        }

        private void FacebookWindow_Load(object sender, EventArgs e)
        {
            vidLoaded = true;
        }

        public void IframeLogin(object userguid, object token)
        {
            this.onlogin(new FacebookEventArgs((string) userguid, (string) token));
        }

        private void InitializeComponent()
        {
            this.webBrowser1 = new ExtendedWebBrowser();
            base.SuspendLayout();
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new Point(2, 0x20);
            this.webBrowser1.MinimumSize = new Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new Size(0x397, 0x249);
            this.webBrowser1.TabIndex = 13;
            this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Black;
            base.ClientSize = new Size(0x39b, 0x26b);
            base.Controls.Add(this.webBrowser1);
            base.Name = "FacebookWindow";
            base.ShowClose = true;
            this.Text = "FacebookWindow";
            base.Load += new EventHandler(this.FacebookWindow_Load);
            base.Controls.SetChildIndex(this.webBrowser1, 0);
            base.ResumeLayout(false);
        }

        protected virtual void onlogin(FacebookEventArgs e)
        {
            this.login(this, e);
            this.FacebookLogin(e.userguid);
        }

        public static void ShowFacebookLogin(string url, string urlFirst, Form parent)
        {
            if (instance != null)
            {
                try
                {
                    instance.Close();
                    instance = null;
                }
                catch (Exception)
                {
                }
            }
            vidLoaded = false;
            FacebookWindow window = new FacebookWindow();
            m_parent = null;
            Point point = new Point(parent.Location.X + ((parent.Width - window.Width) / 2), parent.Location.Y + ((parent.Height - window.Height) / 2));
            window.Location = point;
            window.Show(parent);
            instance = window;
            while (!vidLoaded)
            {
                Thread.Sleep(100);
                Application.DoEvents();
            }
            Thread.Sleep(500);
            if (urlFirst.Length > 0)
            {
                futureURL = url;
                url = urlFirst;
            }
            window.webBrowser1.Navigate(url);
        }

        public static void ShowFacebookLogin(string url, string urlFirst, ProfileLoginWindow parent, FacebookEventHandler loginCallback)
        {
            if (instance != null)
            {
                try
                {
                    instance.Close();
                    instance = null;
                }
                catch (Exception)
                {
                }
            }
            vidLoaded = false;
            FacebookWindow window = new FacebookWindow();
            m_parent = parent;
            Point point = new Point(parent.Location.X + ((parent.Width - window.Width) / 2), parent.Location.Y + ((parent.Height - window.Height) / 2));
            window.closeCallback = new MyFormBase.MFBClose(FacebookWindow.closing);
            window.Location = point;
            window.Show(parent);
            instance = window;
            if (loginCallback != null)
            {
                instance.login = (FacebookEventHandler) Delegate.Combine(instance.login, loginCallback);
            }
            while (!vidLoaded)
            {
                Thread.Sleep(100);
                Application.DoEvents();
            }
            Thread.Sleep(500);
            if (urlFirst.Length > 0)
            {
                futureURL = url;
                url = urlFirst;
            }
            window.webBrowser1.Navigate(url);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (FacebookWindow.futureURL.Length > 0)
            {
                for (int i = 0; i < 50; i++)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
                string futureURL = FacebookWindow.futureURL;
                FacebookWindow.futureURL = "";
                this.webBrowser1.Navigate(futureURL);
            }
        }
    }
}

