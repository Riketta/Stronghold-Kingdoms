namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;
    using System.Windows.Forms;

    public class XmlRpcMySqlProvider : ImySqlProvider
    {
        private mySqlEndResponseDelegate CallbackMethod;
        private Control FormsControl;
        private string mPath;
        private string mPort;
        private string mProtocol;
        private XmlRpcMySqlRequest mRequest;
        private XmlRpcMySqlResponse mResponse;
        private string mServer;

        private XmlRpcMySqlProvider()
        {
        }

        public static XmlRpcMySqlProvider CreateForEndpoint(string protocol, string server, string port, string path)
        {
            return new XmlRpcMySqlProvider { EndpointProtocol = protocol, EndpointServerName = server, EndpointServerPath = path, EndpointServerPort = port };
        }

        public void Execute(ImySqlRequest req, mySqlEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcMySqlRequest) req;
            SqlProxy proxy = XmlRpcProxyGen.Create<SqlProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginExecute(this.mRequest.Request, new AsyncCallback(this.ExecuteResponse), null);
        }

        public void ExecuteResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            SqlProxy clientProtocol = (SqlProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Sql sql = clientProtocol.EndExecute(asr);
                this.mResponse = new XmlRpcMySqlResponse(sql.mResultset);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcMySqlResponse(exception);
            }
            if (this.FormsControl != null)
            {
                this.FormsControl.Invoke(new mySqlEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
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

        public ImySqlRequest Request
        {
            get
            {
                return this.mRequest;
            }
        }

        public ImySqlResponse Response
        {
            get
            {
                return this.mResponse;
            }
        }
    }
}

