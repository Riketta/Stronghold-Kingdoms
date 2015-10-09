namespace Stronghold.AuthClient
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;

    public interface IAuthResponse
    {
        int? CancelVacation { get; set; }

        Dictionary<int, CardTypes.CardOffer> CardOffers { get; }

        int? Cardpoints { get; }

        Dictionary<int, string> Cards { get; }

        int? Crowns { get; }

        string EmailAddress { get; }

        int? FacebookFreePack { get; set; }

        int? Friends { get; set; }

        string IPAddress { get; }

        bool isBigPoint { get; set; }

        bool isMapEditor { get; }

        string Message { get; }

        int? OnVacation { get; set; }

        List<string> ParishNameHistory { get; }

        string Password { get; }

        string Platform { get; }

        int? PremiumBox { get; }

        int? PremiumCards { get; }

        object RawCardPacks { get; }

        object RawCards { get; }

        object RawOffers { get; }

        object RawSharers { get; }

        object RawShields { get; }

        object RawTokens { get; }

        object RawWorlds { get; }

        int? RequiresOptInCheck { get; set; }

        string SessionID { get; }

        int? SuccessCode { get; }

        Dictionary<int, CardTypes.PremiumToken> Tokens { get; }

        Dictionary<int, CardTypes.UserCardPack> UserCardPacks { get; }

        string UserGUID { get; }

        string Username { get; }

        int? VacationPossible { get; set; }

        int? VacationSecondsLeft { get; set; }

        int? VacationSecondsToCancel { get; set; }

        int? VacationsTaken { get; set; }
    }
}

