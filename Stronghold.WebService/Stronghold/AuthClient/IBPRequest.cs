namespace Stronghold.AuthClient
{
    using System;

    public interface IBPRequest
    {
        string SessionID { get; set; }

        string UserGUID { get; set; }
    }
}

