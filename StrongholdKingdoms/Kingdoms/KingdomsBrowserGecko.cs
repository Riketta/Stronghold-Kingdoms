namespace Kingdoms
{
    using Skybound.Gecko;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class KingdomsBrowserGecko : GeckoWebBrowser
    {
        public List<object> imageObjects = new List<object>();
        public IDictionary<string, string> PageValues;
        public static string PostContentType = "Content-Type: application/x-www-form-urlencoded";

        public event ClientFedbackEventHandler ClientFeedback;

        private void GetPageValues()
        {
            if (this.PageValues != null)
            {
                this.PageValues.Clear();
            }
            else
            {
                this.PageValues = new Dictionary<string, string>();
            }
            string[] strArray = base.Document.Cookie.Split(new char[] { ';' });
            if (strArray.Length > 0)
            {
                foreach (string str in strArray)
                {
                    if (str.Trim().StartsWith("KingdomsFeedback_", true, null))
                    {
                        string[] strArray2 = str.Split(new char[] { '=' });
                        if (strArray2.Length == 2)
                        {
                            this.PageValues.Add(new KeyValuePair<string, string>(strArray2[0].Trim().Remove(0, "KingdomsFeedback_".Length), strArray2[1]));
                        }
                    }
                }
            }
        }

        public void Navigate(Uri url)
        {
            base.Navigate(url.AbsoluteUri);
        }

        public void Navigate(Uri url, IDictionary<string, string> postVars)
        {
            string s = string.Empty;
            foreach (KeyValuePair<string, string> pair in postVars)
            {
                if (s != string.Empty)
                {
                    s = s + "&";
                }
                s = s + pair.Key + "=" + pair.Value;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            base.Navigate(url.AbsoluteUri, GeckoLoadFlags.None, null, bytes, PostContentType + Environment.NewLine + "Content-Length:" + bytes.Length.ToString());
        }

        protected virtual void OnClientFeedback(EventArgs e)
        {
            if (this.ClientFeedback != null)
            {
                this.ClientFeedback(this, e);
            }
        }

        protected override void OnDocumentCompleted(EventArgs e)
        {
            this.GetPageValues();
            this.OnClientFeedback(e);
            base.OnDocumentCompleted(e);
            string query = base.Url.Query;
            if (query.Contains("&url"))
            {
                string[] strArray = query.Split(new string[] { "&url=" }, StringSplitOptions.None);
                new Process { StartInfo = { FileName = strArray[1] } }.Start();
            }
        }

        protected override void OnStatusTextChanged(EventArgs e)
        {
            if (base.StatusText == "GiveClientFeedback")
            {
                this.GetPageValues();
                if (this.HasResponse)
                {
                    this.OnClientFeedback(e);
                }
            }
            base.OnStatusTextChanged(e);
        }

        public bool HasResponse
        {
            get
            {
                return (this.PageValues.Count > 0);
            }
        }
    }
}

