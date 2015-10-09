namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcRespStruct_BP
    {
        [XmlRpcMember("message")]
        public string mMessage;
        [XmlRpcMember("success")]
        public int? mSuccessCode;
        [XmlRpcMember("url")]
        public string mURL;
    }
}

