namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;

    [XmlRpcUrl("http://localhost:8080/services/auth.php")]
    public interface CardsProxy : IXmlRpcProxy
    {
        [XmlRpcBegin("buyCardOffer")]
        IAsyncResult BeginbuyCardOffer(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("buyCardOffer")]
        IAsyncResult BeginbuyCardOffer(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("buyCardOffer")]
        IAsyncResult BeginbuyCardOffer(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("buyMultipleCards")]
        IAsyncResult BeginbuyMultipleCards(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("buyMultipleCards")]
        IAsyncResult BeginbuyMultipleCards(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("buyMultipleCards")]
        IAsyncResult BeginbuyMultipleCards(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("buyPremium")]
        IAsyncResult BeginbuyPremium(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("buyPremium")]
        IAsyncResult BeginbuyPremium(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("buyPremium")]
        IAsyncResult BeginbuyPremium(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("cancelVacation")]
        IAsyncResult BegincancelVacation(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("cancelVacation")]
        IAsyncResult BegincancelVacation(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("cancelVacation")]
        IAsyncResult BegincancelVacation(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("cashInCards")]
        IAsyncResult BegincashInCards(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("cashInCards")]
        IAsyncResult BegincashInCards(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("cashInCards")]
        IAsyncResult BegincashInCards(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("checkInvitedFriends")]
        IAsyncResult BegincheckInvitedFriends(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("checkInvitedFriends")]
        IAsyncResult BegincheckInvitedFriends(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("checkInvitedFriends")]
        IAsyncResult BegincheckInvitedFriends(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("doVacation")]
        IAsyncResult BegindoVacation(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("doVacation")]
        IAsyncResult BegindoVacation(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("doVacation")]
        IAsyncResult BegindoVacation(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("getCardCatalog")]
        IAsyncResult BeginGetCardCatalog(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("getCardCatalog")]
        IAsyncResult BeginGetCardCatalog(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("getCardCatalog")]
        IAsyncResult BeginGetCardCatalog(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("getCardOffers")]
        IAsyncResult BegingetCardOffers(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("getCardOffers")]
        IAsyncResult BegingetCardOffers(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("getCardOffers")]
        IAsyncResult BegingetCardOffers(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("getCrowns")]
        IAsyncResult BegingetCrowns(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("getCrowns")]
        IAsyncResult BegingetCrowns(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("getCrowns")]
        IAsyncResult BegingetCrowns(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("getFreeCard")]
        IAsyncResult BegingetFreeCard(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("getFreeCard")]
        IAsyncResult BegingetFreeCard(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("getFreeCard")]
        IAsyncResult BegingetFreeCard(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("getFreeCrowns")]
        IAsyncResult BegingetFreeCrowns(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("getFreeCrowns")]
        IAsyncResult BegingetFreeCrowns(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("getFreeCrowns")]
        IAsyncResult BegingetFreeCrowns(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("getRewardCards")]
        IAsyncResult BegingetRewardCards(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("getRewardCards")]
        IAsyncResult BegingetRewardCards(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("getRewardCards")]
        IAsyncResult BegingetRewardCards(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("getUserCards")]
        IAsyncResult BegingetUserCards(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("getUserCards")]
        IAsyncResult BegingetUserCards(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("getUserCards")]
        IAsyncResult BegingetUserCards(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("getVeteranLevel")]
        IAsyncResult BegingetVeteranLevel(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("getVeteranLevel")]
        IAsyncResult BegingetVeteranLevel(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("getVeteranLevel")]
        IAsyncResult BegingetVeteranLevel(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("giveCardPacks")]
        IAsyncResult BegingiveCardPacks(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("giveCardPacks")]
        IAsyncResult BegingiveCardPacks(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("giveCardPacks")]
        IAsyncResult BegingiveCardPacks(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("giveCardPoints")]
        IAsyncResult BegingiveCardPoints(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("giveCardPoints")]
        IAsyncResult BegingiveCardPoints(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("giveCardPoints")]
        IAsyncResult BegingiveCardPoints(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("giveCards")]
        IAsyncResult BegingiveCards(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("giveCards")]
        IAsyncResult BegingiveCards(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("giveCards")]
        IAsyncResult BegingiveCards(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("givePremium")]
        IAsyncResult BegingivePremium(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("givePremium")]
        IAsyncResult BegingivePremium(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("givePremium")]
        IAsyncResult BegingivePremium(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("ingameBan")]
        IAsyncResult BeginingameBan(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("ingameBan")]
        IAsyncResult BeginingameBan(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("ingameBan")]
        IAsyncResult BeginingameBan(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("makeChargeAvailable")]
        IAsyncResult BeginmakeChargeAvailable(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("makeChargeAvailable")]
        IAsyncResult BeginmakeChargeAvailable(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("makeChargeAvailable")]
        IAsyncResult BeginmakeChargeAvailable(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("openCardPack")]
        IAsyncResult BeginopenCardPack(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("openCardPack")]
        IAsyncResult BeginopenCardPack(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("openCardPack")]
        IAsyncResult BeginopenCardPack(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("playCard")]
        IAsyncResult BeginplayCard(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("playCard")]
        IAsyncResult BeginplayCard(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("playCard")]
        IAsyncResult BeginplayCard(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("playPremium")]
        IAsyncResult BeginplayPremium(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("playPremium")]
        IAsyncResult BeginplayPremium(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("playPremium")]
        IAsyncResult BeginplayPremium(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("setVeteranData")]
        IAsyncResult BeginsetVeteranData(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("setVeteranData")]
        IAsyncResult BeginsetVeteranData(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("setVeteranData")]
        IAsyncResult BeginsetVeteranData(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcBegin("veteranLevelUp")]
        IAsyncResult BeginveteranLevelUp(XmlRpcReqStruct_Cards request);
        [XmlRpcBegin("veteranLevelUp")]
        IAsyncResult BeginveteranLevelUp(XmlRpcReqStruct_Cards request, AsyncCallback acb);
        [XmlRpcBegin("veteranLevelUp")]
        IAsyncResult BeginveteranLevelUp(XmlRpcReqStruct_Cards request, AsyncCallback acb, object state);
        [XmlRpcMethod("buyCardOffer")]
        XmlRpcRespStruct_Cards buyCardOffer(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("buyMultipleCards")]
        XmlRpcRespStruct_Cards buyMultipleCards(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("buyPremium")]
        XmlRpcRespStruct_Cards buyPremium(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("cancelVacation")]
        XmlRpcRespStruct_Cards cancelVacation(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("cashInCards")]
        XmlRpcRespStruct_Cards cashInCards(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("checkInvitedFriends")]
        XmlRpcRespStruct_Cards checkInvitedFriends(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("doVacation")]
        XmlRpcRespStruct_Cards doVacation(XmlRpcReqStruct_Cards request);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndbuyCardOffer(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndbuyMultipleCards(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndbuyPremium(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndcancelVacation(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndcashInCards(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndcheckInvitedFriends(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EnddoVacation(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndGetCardCatalog(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndgetCardOffers(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndgetCrowns(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndgetFreeCard(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndgetFreeCrowns(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndgetRewardCards(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndgetUserCards(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndgetVeteranLevel(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndgingameBan(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndgiveCardPacks(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndgiveCardPoints(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndgiveCards(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndgivePremium(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndmakeChargeAvailable(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndopenCardPack(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndplayCard(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndplayPremium(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndsetVeteranData(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct_Cards EndveteranLevelUp(IAsyncResult iasr);
        [XmlRpcMethod("getCardCatalog")]
        XmlRpcRespStruct_Cards getCardCatalog(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("getCardOffers")]
        XmlRpcRespStruct_Cards getCardOffers(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("getCrowns")]
        XmlRpcRespStruct_Cards getCrowns(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("getFreeCard")]
        XmlRpcRespStruct_Cards getFreeCard(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("getFreeCrowns")]
        XmlRpcRespStruct_Cards getFreeCrowns(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("getRewardCards")]
        XmlRpcRespStruct_Cards getRewardCards(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("getUserCards")]
        XmlRpcRespStruct_Cards getUserCards(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("getVeteranLevel")]
        XmlRpcRespStruct_Cards getVeteranLevel(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("giveCardPacks")]
        XmlRpcRespStruct_Cards giveCardPacks(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("giveCardPoints")]
        XmlRpcRespStruct_Cards giveCardPoints(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("giveCards")]
        XmlRpcRespStruct_Cards giveCards(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("givePremium")]
        XmlRpcRespStruct_Cards givePremium(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("ingameBan")]
        XmlRpcRespStruct_Cards ingameBan(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("makeChargeAvailable")]
        XmlRpcRespStruct_Cards makeChargeAvailable(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("openCardPack")]
        XmlRpcRespStruct_Cards openCardPack(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("playCard")]
        XmlRpcRespStruct_Cards playCard(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("playPremium")]
        XmlRpcRespStruct_Cards playPremium(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("setVeteranData")]
        XmlRpcRespStruct_Cards setVeteranData(XmlRpcReqStruct_Cards request);
        [XmlRpcMethod("veteranLevelUp")]
        XmlRpcRespStruct_Cards veteranLevelUp(XmlRpcReqStruct_Cards request);
    }
}

