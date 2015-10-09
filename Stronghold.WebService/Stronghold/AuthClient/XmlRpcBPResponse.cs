namespace Stronghold.AuthClient
{
    using System;

    public class XmlRpcBPResponse : IBPResponse
    {
        protected XmlRpcRespStruct_BP mResponse;

        public XmlRpcBPResponse(string message, int? success, string url)
        {
            this.mResponse.mMessage = message;
            this.mResponse.mSuccessCode = success;
            this.mResponse.mURL = url;
        }

        public string Message
        {
            get
            {
                return this.mResponse.mMessage;
            }
        }

        public XmlRpcRespStruct_BP Response
        {
            get
            {
                return this.mResponse;
            }
        }

        public int? SuccessCode
        {
            get
            {
                return this.mResponse.mSuccessCode;
            }
        }

        public string URL
        {
            get
            {
                return this.mResponse.mURL;
            }
        }
    }
}

