namespace Stronghold.AuthClient
{
    using System;
    using System.Windows.Forms;

    public interface IAuthProvider
    {
        void AuthenticateUser(IAuthRequest req, AuthEndResponseDelegate callbackHandler);
        void AuthenticateUser(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl);
        void LoginBetaUser(IAuthRequest req, AuthEndResponseDelegate callbackHandler);
        void LoginBetaUser(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl);
        void LogoutUser(IAuthRequest req, AuthEndResponseDelegate callbackHandler);
        void LogoutUser(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl);

        string EndpointProtocol { get; set; }

        string EndpointServerName { get; set; }

        string EndpointServerPath { get; set; }

        string EndpointServerPort { get; set; }

        string EndpointUri { get; }

        IAuthRequest Request { get; }

        IAuthResponse Response { get; }
    }
}

