namespace Stronghold.AuthClient
{
    using System;

    public interface ImySqlProvider
    {
        string EndpointProtocol { get; set; }

        string EndpointServerName { get; set; }

        string EndpointServerPath { get; set; }

        string EndpointServerPort { get; set; }

        string EndpointUri { get; }

        ImySqlRequest Request { get; }

        ImySqlResponse Response { get; }
    }
}

