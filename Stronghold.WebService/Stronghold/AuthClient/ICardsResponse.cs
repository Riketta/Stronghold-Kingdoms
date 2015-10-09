namespace Stronghold.AuthClient
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;

    public interface ICardsResponse
    {
        int? CardID { get; }

        Dictionary<int, CardTypes.CardOffer> CardOffers { get; }

        int? Cardpoints { get; }

        Dictionary<int, string> Cards { get; }

        int? CountOffers { get; }

        int? Crowns { get; }

        int? Friends { get; set; }

        string Message { get; }

        int? Newpoints { get; }

        int? Numcrowns { get; }

        int? PremiumCards { get; }

        object RawCards { get; }

        object RawOffers { get; }

        int? Seconds { get; }

        string Strings { get; }

        int? SuccessCode { get; }

        string Symbols { get; }

        Dictionary<int, CardTypes.UserCardPack> UserCardPacks { get; }

        string UserGUID { get; }
    }
}

