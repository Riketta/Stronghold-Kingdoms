namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;
    using System.Windows.Forms;

    public class XmlRpcBPProvider : IBPProvider
    {
        private BPEndResponseDelegate CallbackMethod;
        private Control FormsControl;
        private string mPath;
        private string mPort;
        private string mProtocol;
        private XmlRpcBPRequest mRequest;
        private XmlRpcBPResponse mResponse;
        private string mServer;

        private XmlRpcBPProvider()
        {
        }

        public static XmlRpcBPProvider CreateForEndpoint(string protocol, string server, string port, string path)
        {
            return new XmlRpcBPProvider { EndpointProtocol = protocol, EndpointServerName = server, EndpointServerPath = path, EndpointServerPort = port };
        }

        public void GetPaymentURL(IBPRequest req, BPEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcBPRequest) req;
            BPProxy proxy = XmlRpcProxyGen.Create<BPProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BegingetPaymentURL(this.mRequest.Request, new AsyncCallback(this.GetPaymentURLResponse), null);
        }

        private void GetPaymentURLResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            BPProxy clientProtocol = (BPProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_BP t_bp = clientProtocol.EndgetPaymentURL(asr);
                this.mResponse = new XmlRpcBPResponse(t_bp.mMessage, t_bp.mSuccessCode, t_bp.mURL);
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcBPResponse("Login Server Unavailable, please try again later", 0, string.Empty);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new BPEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

        public string EndpointProtocol
        {
            get
            {
                return this.mProtocol;
            }
            set
            {
                this.mProtocol = value;
            }
        }

        public string EndpointServerName
        {
            get
            {
                return this.mServer;
            }
            set
            {
                this.mServer = value;
            }
        }

        public string EndpointServerPath
        {
            get
            {
                return this.mPath;
            }
            set
            {
                this.mPath = value;
            }
        }

        public string EndpointServerPort
        {
            get
            {
                return this.mPort;
            }
            set
            {
                this.mPort = value;
            }
        }

        public string EndpointUri
        {
            get
            {
                return (this.mProtocol + "://" + this.mServer + ":" + this.mPort + this.mPath);
            }
        }

        public IBPRequest Request
        {
            get
            {
                return this.mRequest;
            }
        }

        public IBPResponse Response
        {
            get
            {
                return this.mResponse;
            }
        }
    }
}

