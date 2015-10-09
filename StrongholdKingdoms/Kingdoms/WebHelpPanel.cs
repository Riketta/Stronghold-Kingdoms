namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class WebHelpPanel : UserControl, IDockableControl
    {
        private IContainer components;
        private DockableControl dockableControl;
        private KingdomsBrowserGecko geckoWebBrowser1;

        public WebHelpPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
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

        private void geckoWebBrowser1_ClientFeedback(object sender, EventArgs e)
        {
            foreach (string str in this.geckoWebBrowser1.PageValues.Keys)
            {
                if (str != "")
                {
                    string str2 = this.geckoWebBrowser1.PageValues[str];
                    if (str.Trim().ToLowerInvariant() == "openlink")
                    {
                        Process.Start("http://" + str2.Replace("%2F", "/"));
                    }
                }
            }
        }

        public void GoBack()
        {
        }

        public void GoForward()
        {
        }

        private void InitializeComponent()
        {
            this.geckoWebBrowser1 = new KingdomsBrowserGecko();
            base.SuspendLayout();
            this.geckoWebBrowser1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.geckoWebBrowser1.Location = new Point(0, 0);
            this.geckoWebBrowser1.MinimumSize = new Size(20, 20);
            this.geckoWebBrowser1.Name = "geckoWebBrowser1";
            this.geckoWebBrowser1.Size = new Size(0x19d, 350);
            this.geckoWebBrowser1.TabIndex = 280;
            this.geckoWebBrowser1.ClientFeedback += new ClientFedbackEventHandler(this.geckoWebBrowser1_ClientFeedback);
            base.Controls.Add(this.geckoWebBrowser1);
            base.Name = "WebHelpPanel";
            base.Size = new Size(0x19d, 350);
            base.ResumeLayout(false);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        public void openPage(string address)
        {
            this.geckoWebBrowser1.Navigate(new Uri(address));
        }

        public void openUrl(string address)
        {
            this.recreateWebControl();
            if (!string.IsNullOrEmpty(address) && !address.Equals("about:blank"))
            {
                if (!address.StartsWith("http://") && !address.StartsWith("https://"))
                {
                    address = "http://" + address;
                }
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    IDictionary<string, string> postVars = new Dictionary<string, string>();
                    postVars.Add("uid", RemoteServices.Instance.UserGuid.ToString("N"));
                    postVars.Add("sid", RemoteServices.Instance.SessionGuid.ToString("N"));

                    int num = -1;
                    int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
                    if (!GameEngine.Instance.World.isCapital(villageID))
                    {
                        num = villageID;
                    }
                    postVars.Add(new KeyValuePair<string, string>("CurrentvillageID", num.ToString()));
                    postVars.Add(new KeyValuePair<string, string>("CurrentWorldID", GameEngine.Instance.World.GetGlobalWorldID().ToString()));
                    this.geckoWebBrowser1.Navigate(new Uri(address), postVars);
                    Cursor.Current = Cursors.Default;
                }
                catch (UriFormatException)
                {
                }
            }
        }

        public void recreateWebControl()
        {
        }

        private void updateCurrentCardsCallback(UpdateCurrentCards_ReturnType returnData)
        {
        }

        private void webBrowserHelp_CanGoBackChanged(object sender, EventArgs e)
        {
        }

        private void webBrowserHelp_CanGoForwardChanged(object sender, EventArgs e)
        {
        }

        private void webBrowserHelp_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }

        private void webBrowserHelp_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
        }
    }
}

