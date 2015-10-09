namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;

    [XmlRpcUrl("http://localhost:8080/services/auth.php")]
    public interface IATestProxy : IXmlRpcProxy
    {
        [XmlRpcBegin("ping")]
        IAsyncResult Beginping();
        [XmlRpcBegin("ping")]
        IAsyncResult Beginping(AsyncCallback acb);
        [XmlRpcBegin("ping")]
        IAsyncResult Beginping(AsyncCallback acb, object state);
        [XmlRpcEnd]
        string Endping(IAsyncResult iasr);
        [XmlRpcMethod("ping")]
        string ping();
    }
}

