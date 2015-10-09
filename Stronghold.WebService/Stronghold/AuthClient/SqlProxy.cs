namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;

    [XmlRpcUrl("http://localhost:8080/services/auth.php")]
    public interface SqlProxy : IXmlRpcProxy
    {
        [XmlRpcBegin("execute")]
        IAsyncResult BeginExecute(XmlRpcReqStruct_Sql request);
        [XmlRpcBegin("execute")]
        IAsyncResult BeginExecute(XmlRpcReqStruct_Sql request, AsyncCallback acb);
        [XmlRpcBegin("execute")]
        IAsyncResult BeginExecute(XmlRpcReqStruct_Sql request, AsyncCallback acb, object state);
        [XmlRpcEnd]
        XmlRpcRespStruct_Sql EndExecute(IAsyncResult iasr);
        [XmlRpcMethod("execute")]
        XmlRpcRespStruct_Cards Execute(XmlRpcReqStruct_Sql request);
    }
}

