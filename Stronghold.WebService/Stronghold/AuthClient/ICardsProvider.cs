namespace Stronghold.AuthClient
{
    using System;
    using System.Windows.Forms;

    public interface ICardsProvider
    {
        void GetCardCatalog(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl);
        void GetUserCards(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl);

        string EndpointProtocol { get; set; }

        string EndpointServerName { get; set; }

        string EndpointServerPath { get; set; }

        string EndpointServerPort { get; set; }

        string EndpointUri { get; }

        ICardsRequest Request { get; }

        ICardsResponse Response { get; }
    }
}

