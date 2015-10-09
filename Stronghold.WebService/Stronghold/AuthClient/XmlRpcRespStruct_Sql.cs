namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcRespStruct_Sql
    {
        [XmlRpcMember("resultset")]
        public object mResultset;
    }
}

