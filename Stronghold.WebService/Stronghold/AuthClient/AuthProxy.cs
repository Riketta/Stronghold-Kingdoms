namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;

    [XmlRpcUrl("http://localhost:8080/services/auth.php")]
    public interface AuthProxy : IXmlRpcProxy
    {
        [XmlRpcMethod("aeriagetbalance")]
        XmlRpcRespStruct AeriaGetBalance(XmlRpcReqStruct request);
        [XmlRpcMethod("aeriamakepayment")]
        XmlRpcRespStruct AeriaMakePayment(XmlRpcReqStruct request);
        [XmlRpcMethod("authenticate")]
        XmlRpcRespStruct authenticate(XmlRpcReqStruct request);
        [XmlRpcMethod("authsteamaccount")]
        XmlRpcRespStruct AuthSteamAccount(XmlRpcReqStruct request);
        [XmlRpcBegin("aeriagetbalance")]
        IAsyncResult BeginAeriaGetBalance(XmlRpcReqStruct request);
        [XmlRpcBegin("aeriagetbalance")]
        IAsyncResult BeginAeriaGetBalance(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("aeriagetbalance")]
        IAsyncResult BeginAeriaGetBalance(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("aeriamakepayment")]
        IAsyncResult BeginAeriaMakePayment(XmlRpcReqStruct request);
        [XmlRpcBegin("aeriamakepayment")]
        IAsyncResult BeginAeriaMakePayment(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("aeriamakepayment")]
        IAsyncResult BeginAeriaMakePayment(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("authenticate")]
        IAsyncResult Beginauthenticate(XmlRpcReqStruct request);
        [XmlRpcBegin("authenticate")]
        IAsyncResult Beginauthenticate(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("authenticate")]
        IAsyncResult Beginauthenticate(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("authsteamaccount")]
        IAsyncResult BeginAuthSteamAccount(XmlRpcReqStruct request);
        [XmlRpcBegin("authsteamaccount")]
        IAsyncResult BeginAuthSteamAccount(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("authsteamaccount")]
        IAsyncResult BeginAuthSteamAccount(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("betalogin")]
        IAsyncResult BeginBeatlogin(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("betalogin")]
        IAsyncResult BeginBetalogin(XmlRpcReqStruct request);
        [XmlRpcBegin("betalogin")]
        IAsyncResult BeginBetalogin(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("checkusernamesteam")]
        IAsyncResult BeginCheckUsernameSteam(XmlRpcReqStruct request);
        [XmlRpcBegin("checkusernamesteam")]
        IAsyncResult BeginCheckUsernameSteam(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("checkusernamesteam")]
        IAsyncResult BeginCheckUsernameSteam(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("clientchooseworld")]
        IAsyncResult BeginChooseWorld(XmlRpcReqStruct request);
        [XmlRpcBegin("clientchooseworld")]
        IAsyncResult BeginChooseWorld(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("clientchooseworld")]
        IAsyncResult BeginChooseWorld(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("clientlogin")]
        IAsyncResult BeginclientLogin(XmlRpcReqStruct request);
        [XmlRpcBegin("clientlogin")]
        IAsyncResult BeginclientLogin(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("clientlogin")]
        IAsyncResult BeginclientLogin(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("clientlogout")]
        IAsyncResult BeginclientLogout(XmlRpcReqStruct request);
        [XmlRpcBegin("clientlogout")]
        IAsyncResult BeginclientLogout(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("clientlogout")]
        IAsyncResult BeginclientLogout(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("createusersteam")]
        IAsyncResult BeginCreateUserSteam(XmlRpcReqStruct request);
        [XmlRpcBegin("createusersteam")]
        IAsyncResult BeginCreateUserSteam(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("createusersteam")]
        IAsyncResult BeginCreateUserSteam(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("getparishnamechanges")]
        IAsyncResult BeginGetParishNameHistory(XmlRpcReqStruct request);
        [XmlRpcBegin("getparishnamechanges")]
        IAsyncResult BeginGetParishNameHistory(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("getparishnamechanges")]
        IAsyncResult BeginGetParishNameHistory(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("getparishnames")]
        IAsyncResult BeginGetParishNames(XmlRpcReqStruct request);
        [XmlRpcBegin("getparishnames")]
        IAsyncResult BeginGetParishNames(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("getparishnames")]
        IAsyncResult BeginGetParishNames(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("getworlds")]
        IAsyncResult BeginGetWorlds(XmlRpcReqStruct request);
        [XmlRpcBegin("getworlds")]
        IAsyncResult BeginGetWorlds(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("getworlds")]
        IAsyncResult BeginGetWorlds(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("logout")]
        IAsyncResult Beginlogout(XmlRpcReqStruct request);
        [XmlRpcBegin("logout")]
        IAsyncResult Beginlogout(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("logout")]
        IAsyncResult Beginlogout(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("ping")]
        IAsyncResult Beginping();
        [XmlRpcBegin("ping")]
        IAsyncResult Beginping(AsyncCallback acb);
        [XmlRpcBegin("ping")]
        IAsyncResult Beginping(AsyncCallback acb, object state);
        [XmlRpcBegin("renameparish")]
        IAsyncResult BeginRenameParish(XmlRpcReqStruct request);
        [XmlRpcBegin("renameparish")]
        IAsyncResult BeginRenameParish(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("renameparish")]
        IAsyncResult BeginRenameParish(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("setemailoptin")]
        IAsyncResult BeginsetEmailOptIn(XmlRpcReqStruct request);
        [XmlRpcBegin("setemailoptin")]
        IAsyncResult BeginsetEmailOptIn(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("setemailoptin")]
        IAsyncResult BeginsetEmailOptIn(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("steamgetproductlist")]
        IAsyncResult BeginSteamGetProductList(XmlRpcReqStruct request);
        [XmlRpcBegin("steamgetproductlist")]
        IAsyncResult BeginSteamGetProductList(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("steamgetproductlist")]
        IAsyncResult BeginSteamGetProductList(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("steampaymentfinalize")]
        IAsyncResult BeginSteamPaymentFinalize(XmlRpcReqStruct request);
        [XmlRpcBegin("steampaymentfinalize")]
        IAsyncResult BeginSteamPaymentFinalize(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("steampaymentfinalize")]
        IAsyncResult BeginSteamPaymentFinalize(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcBegin("steampaymentinit")]
        IAsyncResult BeginSteamPaymentInit(XmlRpcReqStruct request);
        [XmlRpcBegin("steampaymentinit")]
        IAsyncResult BeginSteamPaymentInit(XmlRpcReqStruct request, AsyncCallback acb);
        [XmlRpcBegin("steampaymentinit")]
        IAsyncResult BeginSteamPaymentInit(XmlRpcReqStruct request, AsyncCallback acb, object state);
        [XmlRpcMethod("betalogin")]
        XmlRpcRespStruct Betalogin(XmlRpcReqStruct request);
        [XmlRpcMethod("checkusernamesteam")]
        XmlRpcRespStruct CheckUsernameSteam(XmlRpcReqStruct request);
        [XmlRpcMethod("clientchooseworld")]
        XmlRpcRespStruct ChooseWorld(XmlRpcReqStruct request);
        [XmlRpcMethod("clientlogin")]
        XmlRpcRespStruct clientLogin(XmlRpcReqStruct request);
        [XmlRpcMethod("clientlogout")]
        XmlRpcRespStruct clientLogout(XmlRpcReqStruct request);
        [XmlRpcMethod("createusersteam")]
        XmlRpcRespStruct CreateUserSteam(XmlRpcReqStruct request);
        [XmlRpcEnd]
        XmlRpcRespStruct EndAeriaGetBalance(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndAeriaMakePayment(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct Endauthenticate(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndAuthSteamAccount(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndBetalogin(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndCheckUsernameSteam(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndChooseWorld(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndclientLogin(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndclientLogout(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndCreateUserSteam(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndGetParishNameHistory(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndGetParishNames(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndGetWorlds(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct Endlogout(IAsyncResult iasr);
        [XmlRpcEnd]
        string Endping(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndRenameParish(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndsetEmailOptIn(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndSteamPaymentFinalize(IAsyncResult iasr);
        [XmlRpcEnd]
        XmlRpcRespStruct EndSteamPaymentInit(IAsyncResult iasr);
        [XmlRpcMethod("getparishnamechanges")]
        XmlRpcRespStruct GetParishNameHistory(XmlRpcReqStruct request);
        [XmlRpcMethod("getparishnames")]
        XmlRpcRespStruct GetParishNames(XmlRpcReqStruct request);
        [XmlRpcMethod("getworlds")]
        XmlRpcRespStruct GetWorlds(XmlRpcReqStruct request);
        [XmlRpcMethod("logout")]
        XmlRpcRespStruct logout(XmlRpcReqStruct request);
        [XmlRpcMethod("ping")]
        string ping();
        [XmlRpcMethod("renameparish")]
        XmlRpcRespStruct RenameParish(XmlRpcReqStruct request);
        [XmlRpcMethod("setemailoptin")]
        XmlRpcRespStruct setEmailOptIn(XmlRpcReqStruct request);
        [XmlRpcMethod("steamgetproductlist")]
        XmlRpcRespStruct SteamGetProductList(XmlRpcReqStruct request);
        [XmlRpcMethod("steampaymentfinalize")]
        XmlRpcRespStruct SteamPaymentFinalize(XmlRpcReqStruct request);
        [XmlRpcMethod("steampaymentinit")]
        XmlRpcRespStruct SteamPaymentInit(XmlRpcReqStruct request);
    }
}

