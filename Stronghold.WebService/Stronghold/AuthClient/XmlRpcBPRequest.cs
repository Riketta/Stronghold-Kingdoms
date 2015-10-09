namespace Stronghold.AuthClient
{
    using System;

    public class XmlRpcBPRequest : IBPRequest
    {
        protected XmlRpcReqStruct_BP mRequest = new XmlRpcReqStruct_BP();

        public XmlRpcBPRequest()
        {
            this.mRequest.mSessionID = string.Empty;
            this.mRequest.mUserGUID = string.Empty;
        }

        public XmlRpcReqStruct_BP Request
        {
            get
            {
                return this.mRequest;
            }
        }

        public string SessionID
        {
            get
            {
                return this.mRequest.mSessionID;
            }
            set
            {
                this.mRequest.mSessionID = value;
            }
        }

        public string UserGUID
        {
            get
            {
                return this.mRequest.mUserGUID;
            }
            set
            {
                this.mRequest.mUserGUID = value;
            }
        }
    }
}

