namespace Stronghold.AuthClient
{
    using System;

    public class XmlRpcMySqlResponse : ImySqlResponse
    {
        protected XmlRpcRespStruct_Sql mResponse;

        public XmlRpcMySqlResponse(object resultset)
        {
            this.mResponse.mResultset = resultset;
        }

        public XmlRpcRespStruct_Sql Response
        {
            get
            {
                return this.mResponse;
            }
        }

        public object Resultset
        {
            get
            {
                return this.mResponse.mResultset;
            }
        }
    }
}

