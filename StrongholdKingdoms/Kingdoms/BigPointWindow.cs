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
    public class BigPointWindow : MyFormBase
    {
        private IContainer components;
        public static string futureURL = "";
        private static BigPointWindow instance = null;
        private static ProfileLoginWindow m_parent = null;
        private BPForgottenPasswordPanel panel5;
        public static bool vidLoaded = false;
        private WebBrowser webBrowser1;

        public event BigPointEventHandler login;

        public BigPointWindow()
        {
            this.InitializeComponent();
            this.Text = base.Title = SK.Text("BIGPOINT_LOGIN", "Bigpoint Login");
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            this.webBrowser1.ObjectForScripting = this;
        }

        private void bigpointLogin(string userGuid)
        {
            if (m_parent != null)
            {
                m_parent.bigpointLogin(userGuid, "");
            }
            base.Close();
            instance = null;
        }

        private void BigPointWindow_Load(object sender, EventArgs e)
        {
            vidLoaded = true;
        }

        public static void closing()
        {
            if (m_parent != null)
            {
                m_parent.bigpointClose();
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

        public void IframeLogin(object userguid, object token)
        {
            this.onlogin(new BigPointEventArgs((string) userguid, (string) token));
        }

        private void InitializeComponent()
        {
            this.webBrowser1 = new WebBrowser();
            this.panel5 = new BPForgottenPasswordPanel();
            base.SuspendLayout();
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new Point(2, 0x20);
            this.webBrowser1.MinimumSize = new Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new Size(0x207, 0x20d);
            this.webBrowser1.TabIndex = 13;
            this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.panel5.BackColor = Color.White;
            this.panel5.Location = new Point(2, 0x22c);
            this.panel5.Name = "panel5";
            this.panel5.Size = new Size(0x207, 0x23);
            this.panel5.TabIndex = 14;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0, 0, 0);
            base.ClientSize = new Size(0x20b, 0x251);
            base.Controls.Add(this.panel5);
            base.Controls.Add(this.webBrowser1);
            base.Name = "BigPointWindow";
            base.ShowBar = true;
            base.ShowClose = true;
            this.Text = "BigPointWindow";
            base.Load += new EventHandler(this.BigPointWindow_Load);
            base.Controls.SetChildIndex(this.webBrowser1, 0);
            base.Controls.SetChildIndex(this.panel5, 0);
            base.ResumeLayout(false);
        }

        protected virtual void onlogin(BigPointEventArgs e)
        {
            this.login(this, e);
            this.bigpointLogin(e.userguid);
        }

        public static void ShowBigPointLogin(string url, string urlFirst, ProfileLoginWindow parent, BigPointEventHandler loginCallback)
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
            BigPointWindow window = new BigPointWindow();
            m_parent = parent;
            Point point = new Point(parent.Location.X + ((parent.Width - window.Width) / 2), parent.Location.Y + ((parent.Height - window.Height) / 2));
            window.closeCallback = new MyFormBase.MFBClose(BigPointWindow.closing);
            window.Location = point;
            window.Show(parent);
            instance = window;
            instance.login = (BigPointEventHandler) Delegate.Combine(instance.login, loginCallback);
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
            if (BigPointWindow.futureURL.Length > 0)
            {
                for (int i = 0; i < 50; i++)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
                string futureURL = BigPointWindow.futureURL;
                BigPointWindow.futureURL = "";
                this.webBrowser1.Navigate(futureURL);
            }
        }
    }
}

