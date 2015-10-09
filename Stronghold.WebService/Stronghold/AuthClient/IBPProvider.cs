namespace Stronghold.AuthClient
{
    using System;
    using System.Windows.Forms;

    public interface IBPProvider
    {
        void GetPaymentURL(IBPRequest req, BPEndResponseDelegate callbackHandler, Control control);

        string EndpointProtocol { get; set; }

        string EndpointServerName { get; set; }

        string EndpointServerPath { get; set; }

        string EndpointServerPort { get; set; }

        string EndpointUri { get; }

        IBPRequest Request { get; }

        IBPResponse Response { get; }
    }
}

