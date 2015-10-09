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
    public class AeriaWindow : MyFormBase
    {
        private IContainer components;
        public static string futureURL = "";
        private static AeriaWindow instance = null;
        private static ProfileLoginWindow m_parent = null;
        public static bool vidLoaded = false;
        private WebBrowser webBrowser1;

        public event AeriaEventHandler login;

        public AeriaWindow()
        {
            this.InitializeComponent();
            this.Text = base.Title = SK.Text("AERIA_LOGIN", "Aeria Games Login");
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            this.webBrowser1.ObjectForScripting = this;
        }

        private void aeriaLogin(string userGuid)
        {
            if (m_parent != null)
            {
                m_parent.aeriaLogin(userGuid, "");
            }
            base.Close();
            instance = null;
        }

        public void AeriaLogin(object userguid, object aeriatoken)
        {
            this.onlogin(new AeriaEventArgs((string) userguid, (string) aeriatoken));
        }

        private void AeriaWindow_Load(object sender, EventArgs e)
        {
            vidLoaded = true;
        }

        public static void closing()
        {
            if (m_parent != null)
            {
                m_parent.aeriaClose();
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

        private void InitializeComponent()
        {
            this.webBrowser1 = new WebBrowser();
            base.SuspendLayout();
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new Point(2, 0x20);
            this.webBrowser1.MinimumSize = new Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new Size(420, 440);
            this.webBrowser1.TabIndex = 13;
            this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Black;
            base.ClientSize = new Size(0x1a8, 0x1da);
            base.Controls.Add(this.webBrowser1);
            base.Name = "AeriaWindow";
            base.ShowClose = true;
            this.Text = "AeriaWindow";
            base.Load += new EventHandler(this.AeriaWindow_Load);
            base.Controls.SetChildIndex(this.webBrowser1, 0);
            base.ResumeLayout(false);
        }

        protected virtual void onlogin(AeriaEventArgs e)
        {
            this.login(this, e);
            this.aeriaLogin(e.userguid);
        }

        public static void ShowAeriaLogin(string url, string urlFirst, ProfileLoginWindow parent, AeriaEventHandler loginCallback)
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
            AeriaWindow window = new AeriaWindow();
            m_parent = parent;
            Point point = new Point(parent.Location.X + ((parent.Width - window.Width) / 2), parent.Location.Y + ((parent.Height - window.Height) / 2));
            window.closeCallback = new MyFormBase.MFBClose(AeriaWindow.closing);
            window.Location = point;
            window.Show(parent);
            instance = window;
            instance.login = (AeriaEventHandler) Delegate.Combine(instance.login, loginCallback);
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
            if (AeriaWindow.futureURL.Length > 0)
            {
                for (int i = 0; i < 50; i++)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
                string futureURL = AeriaWindow.futureURL;
                AeriaWindow.futureURL = "";
                this.webBrowser1.Navigate(futureURL);
            }
        }
    }
}

