namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;

    [XmlRpcUrl("http://localhost:8080/services/auth.php")]
    public interface ITestProxy : IXmlRpcProxy
    {
        [XmlRpcMethod("ping")]
        string ping();
    }
}

