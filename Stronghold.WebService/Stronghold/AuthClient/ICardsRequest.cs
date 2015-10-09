namespace Stronghold.AuthClient
{
    using System;

    public interface ICardsRequest
    {
        string CardString { get; set; }

        string ChargeDesc { get; set; }

        int? NumPacks { get; set; }

        string OfferID { get; set; }

        string PackID { get; set; }

        string Rank { get; set; }

        string SessionGUID { get; set; }

        string UserCardID { get; set; }

        string UserGUID { get; set; }

        int? VacationLength { get; set; }

        string VillageID { get; set; }

        string WorldID { get; set; }
    }
}

