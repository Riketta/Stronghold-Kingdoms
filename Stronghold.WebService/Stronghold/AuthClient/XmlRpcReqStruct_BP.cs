namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcReqStruct_BP
    {
        [XmlRpcMember("userguid")]
        public string mUserGUID;
        [XmlRpcMember("sessionid")]
        public string mSessionID;
    }
}

