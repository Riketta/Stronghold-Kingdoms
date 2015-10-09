namespace Kingdoms
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;

    public class KingdomsBrowserIE : WebBrowser
    {
        public IDictionary<string, string> PageValues;
        public static string PostContentType = "Content-Type: application/x-www-form-urlencoded";

        public string GetPageValueByID(string id)
        {
            return base.Document.GetElementById(id).GetAttribute("value");
        }

        private void GetPageValues()
        {
            this.PageValues = new Dictionary<string, string>();
            HtmlElement elementById = base.Document.GetElementById("ClientFeedbackDIV");
            if (elementById != null)
            {
                foreach (HtmlElement element2 in elementById.GetElementsByTagName("input"))
                {
                    this.PageValues.Add(new KeyValuePair<string, string>(element2.Id, element2.GetAttribute("value")));
                }
            }
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
            base.Navigate(url, string.Empty, bytes, PostContentType + Environment.NewLine);
        }

        protected override void OnNavigated(WebBrowserNavigatedEventArgs e)
        {
            this.GetPageValues();
            base.OnNavigated(e);
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

