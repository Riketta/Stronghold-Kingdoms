namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;

    [XmlRpcUrl("http://localhost:8080/services/bpEndpoint.php")]
    public interface BPProxy : IXmlRpcProxy
    {
        [XmlRpcBegin("getPaymentURL")]
        IAsyncResult BegingetPaymentURL(XmlRpcReqStruct_BP request);
        [XmlRpcBegin("getPaymentURL")]
        IAsyncResult BegingetPaymentURL(XmlRpcReqStruct_BP request, AsyncCallback acb);
        [XmlRpcBegin("getPaymentURL")]
        IAsyncResult BegingetPaymentURL(XmlRpcReqStruct_BP request, AsyncCallback acb, object state);
        [XmlRpcEnd]
        XmlRpcRespStruct_BP EndgetPaymentURL(IAsyncResult iasr);
        [XmlRpcMethod("getPaymentURL")]
        XmlRpcRespStruct_BP getPaymentURL(XmlRpcReqStruct_BP request);
    }
}

