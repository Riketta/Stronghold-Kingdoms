namespace Stronghold.AuthClient
{
    using System;

    public interface IAuthRequest
    {
        string Admin { get; set; }

        string AeriaToken { get; set; }

        string Culture { get; set; }

        string EmailAddress { get; set; }

        int? FirstWorld { get; set; }

        string IPAddress { get; set; }

        string Password { get; set; }

        string Platform { get; set; }

        int? Playing { get; set; }

        string SessionID { get; set; }

        string SteamID { get; set; }

        string UserGUID { get; set; }

        string Username { get; set; }

        int? WorldID { get; set; }
    }
}

