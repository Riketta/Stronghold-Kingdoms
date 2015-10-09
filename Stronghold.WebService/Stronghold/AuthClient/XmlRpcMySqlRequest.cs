namespace Stronghold.AuthClient
{
    using System;

    public class XmlRpcMySqlRequest : ImySqlRequest
    {
        protected XmlRpcReqStruct_Sql mRequest = new XmlRpcReqStruct_Sql();

        public XmlRpcMySqlRequest(string query)
        {
            this.mRequest.mQuery = query;
        }

        public string Query
        {
            get
            {
                return this.mRequest.mQuery;
            }
            set
            {
                this.mRequest.mQuery = value;
            }
        }

        public XmlRpcReqStruct_Sql Request
        {
            get
            {
                return this.mRequest;
            }
        }
    }
}

