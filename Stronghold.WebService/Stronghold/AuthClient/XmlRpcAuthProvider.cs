namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;
    using System.Windows.Forms;

    public class XmlRpcAuthProvider : IAuthProvider
    {
        private AuthEndResponseDelegate CallbackMethod;
        private Control FormsControl;
        private string mPath;
        private string mPort;
        private string mProtocol;
        private XmlRpcAuthRequest mRequest;
        private XmlRpcAuthResponse mResponse;
        private string mServer;

        private XmlRpcAuthProvider()
        {
        }

        public void AeriaGetBalance(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginAeriaGetBalance(this.mRequest.Request, new AsyncCallback(this.AeriaGetBalanceResponse), null);
        }

        public int AeriaGetBalance(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl, int timeout, ref XmlRpcAuthResponse response)
        {
            try
            {
                this.FormsControl = ctrl;
                this.CallbackMethod = callbackHandler;
                this.mRequest = (XmlRpcAuthRequest) req;
                AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
                proxy.Url = this.EndpointUri;
                proxy.Timeout = timeout;
                XmlRpcRespStruct struct2 = proxy.AeriaGetBalance(this.mRequest.Request);
                int num = 0;
                num = struct2.mPoints.Value;
                this.mResponse = new XmlRpcAuthResponse();
                this.mResponse.Products = struct2.mRawProducts;
                response = this.mResponse;
                return num;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void AeriaGetBalanceResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            AuthProxy clientProtocol = (AuthProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct struct2 = clientProtocol.EndAeriaGetBalance(asr);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode);
                this.mResponse.Points = struct2.mPoints;
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new AuthEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

        public void AeriaMakePayment(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginAeriaMakePayment(this.mRequest.Request, new AsyncCallback(this.AeriaMakePaymentResponse), null);
        }

        public XmlRpcAuthResponse AeriaMakePayment(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            try
            {
                this.FormsControl = ctrl;
                this.CallbackMethod = callbackHandler;
                this.mRequest = (XmlRpcAuthRequest) req;
                AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
                proxy.Url = this.EndpointUri;
                proxy.Timeout = timeout;
                XmlRpcRespStruct struct2 = proxy.AeriaMakePayment(this.mRequest.Request);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, struct2.mRawProducts, null);
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            return this.mResponse;
        }

        private void AeriaMakePaymentResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            AuthProxy clientProtocol = (AuthProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct struct2 = clientProtocol.EndAeriaMakePayment(asr);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode);
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new AuthEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

        public void AuthenticateSteamAccount(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginAuthSteamAccount(this.mRequest.Request, new AsyncCallback(this.AuthenticateSteamAccountResponse), null);
        }

        public XmlRpcAuthResponse AuthenticateSteamAccount(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct struct2 = proxy.AuthSteamAccount(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens);
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            return this.mResponse;
        }

        private void AuthenticateSteamAccountResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            AuthProxy clientProtocol = (AuthProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct struct2 = clientProtocol.EndAuthSteamAccount(asr);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens);
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new AuthEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

        public void AuthenticateUser(IAuthRequest req, AuthEndResponseDelegate callbackHandler)
        {
        }

        public void AuthenticateUser(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Beginauthenticate(this.mRequest.Request, new AsyncCallback(this.AuthenticateUserResponse), null);
        }

        private void AuthenticateUserResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            AuthProxy clientProtocol = (AuthProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                int? mIsbigpoint = null;
                int? mUnviewedOffers = null;
                XmlRpcRespStruct struct2 = clientProtocol.Endauthenticate(asr);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens);
                if (struct2.mIsbigpoint.HasValue)
                {
                    mIsbigpoint = struct2.mIsbigpoint;
                }
                this.mResponse.isBigPoint = (mIsbigpoint.GetValueOrDefault() == 1) && mIsbigpoint.HasValue;
                if (struct2.mUnviewedOffers.HasValue)
                {
                    mUnviewedOffers = struct2.mUnviewedOffers;
                }
                this.mResponse.hasUnviewedOffers = (mUnviewedOffers.GetValueOrDefault() == 1) && mUnviewedOffers.HasValue;
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new AuthEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

        public void CheckUsernameSteam(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginCheckUsernameSteam(this.mRequest.Request, new AsyncCallback(this.CheckUsernameSteamResponse), null);
        }

        private void CheckUsernameSteamResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            AuthProxy clientProtocol = (AuthProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct struct2 = clientProtocol.EndCheckUsernameSteam(asr);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens);
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new AuthEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

        public void ChooseWorld(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginChooseWorld(this.mRequest.Request, new AsyncCallback(this.ChooseWorldResponse), null);
        }

        private void ChooseWorldResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            AuthProxy clientProtocol = (AuthProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct struct2 = clientProtocol.EndChooseWorld(asr);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens, struct2.mVeteranLv1, struct2.mVeteranLv2, struct2.mVeteranLv3, struct2.mVeteranLv4, struct2.mVeteranLv5, struct2.mVeteranLv6, struct2.mVeteranLv7, struct2.mVeteranLv8, struct2.mVeteranLv9, struct2.mVeteranLv10, struct2.mVeteranTotalSeconds, struct2.mVeteranSecondsLeft, struct2.mVeteranCurrentLevel, struct2.mPremiumBox, struct2.mRawShields, struct2.mRawWorlds, struct2.mRawProducts, struct2.mSpecialURL);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcAuthResponse(exception.Message, 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new AuthEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void clientLogin(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginclientLogin(this.mRequest.Request, new AsyncCallback(this.clientLoginResponse), null);
        }

        private void clientLoginResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            AuthProxy clientProtocol = (AuthProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                int? mIsbigpoint = null;
                int? mUnviewedOffers = null;
                XmlRpcRespStruct struct2 = clientProtocol.EndclientLogin(asr);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens, struct2.mVeteranLv1, struct2.mVeteranLv2, struct2.mVeteranLv3, struct2.mVeteranLv4, struct2.mVeteranLv5, struct2.mVeteranLv6, struct2.mVeteranLv7, struct2.mVeteranLv8, struct2.mVeteranLv9, struct2.mVeteranLv10, struct2.mVeteranTotalSeconds, struct2.mVeteranSecondsLeft, struct2.mVeteranCurrentLevel, struct2.mPremiumBox, struct2.mRawShields, struct2.mRawWorlds, struct2.mRawProducts, struct2.mSpecialURL);
                if (struct2.mIsbigpoint.HasValue)
                {
                    mIsbigpoint = struct2.mIsbigpoint;
                }
                this.mResponse.isBigPoint = (mIsbigpoint.GetValueOrDefault() == 1) && mIsbigpoint.HasValue;
                if (struct2.mUnviewedOffers.HasValue)
                {
                    mUnviewedOffers = struct2.mUnviewedOffers;
                }
                this.mResponse.hasUnviewedOffers = (mUnviewedOffers.GetValueOrDefault() == 1) && mUnviewedOffers.HasValue;
                this.mResponse.OnVacation = struct2.mOnVacation;
                this.mResponse.FacebookFreePack = struct2.mFBFreePack;
                this.mResponse.VacationsTaken = struct2.mVacationsTaken;
                this.mResponse.CancelVacation = struct2.mCancelVacation;
                this.mResponse.VacationSecondsLeft = struct2.mVacationSecondsLeft;
                this.mResponse.VacationSecondsToCancel = struct2.mVacationSecondsToCancel;
                this.mResponse.VacationPossible = struct2.mVacationPossible;
                this.mResponse.RequiresOptInCheck = struct2.mRequiresOptInCheck;
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcAuthResponse(exception.Message, 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new AuthEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void clientLogout(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginclientLogout(this.mRequest.Request, new AsyncCallback(this.clientLogoutResponse), null);
        }

        private void clientLogoutResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            AuthProxy clientProtocol = (AuthProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct struct2 = clientProtocol.EndclientLogout(asr);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens, struct2.mVeteranLv1, struct2.mVeteranLv2, struct2.mVeteranLv3, struct2.mVeteranLv4, struct2.mVeteranLv5, struct2.mVeteranLv6, struct2.mVeteranLv7, struct2.mVeteranLv8, struct2.mVeteranLv9, struct2.mVeteranLv10, struct2.mVeteranTotalSeconds, struct2.mVeteranSecondsLeft, struct2.mVeteranCurrentLevel, struct2.mPremiumBox, struct2.mRawShields, struct2.mRawWorlds, struct2.mRawProducts, struct2.mSpecialURL);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcAuthResponse(exception.Message, 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new AuthEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public static XmlRpcAuthProvider CreateForEndpoint(string protocol, string server, string port, string path)
        {
            return new XmlRpcAuthProvider { EndpointProtocol = protocol, EndpointServerName = server, EndpointServerPath = path, EndpointServerPort = port };
        }

        public void CreateUserSteam(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginCreateUserSteam(this.mRequest.Request, new AsyncCallback(this.CreateUserSteamResponse), null);
        }

        private void CreateUserSteamResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            AuthProxy clientProtocol = (AuthProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct struct2 = clientProtocol.EndCreateUserSteam(asr);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens);
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new AuthEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

        public XmlRpcAuthResponse GetParishNameHistory(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            try
            {
                this.FormsControl = ctrl;
                this.CallbackMethod = callbackHandler;
                this.mRequest = (XmlRpcAuthRequest) req;
                AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
                proxy.Url = this.EndpointUri;
                proxy.Timeout = timeout;
                XmlRpcRespStruct parishNameHistory = proxy.GetParishNameHistory(this.mRequest.Request);
                this.mResponse = new XmlRpcAuthResponse(parishNameHistory.mMessage, parishNameHistory.mSuccessCode);
                if (this.mResponse.SuccessCode.HasValue && (this.mResponse.SuccessCode.Value == 1))
                {
                    this.mResponse.RawParishNameHistory = parishNameHistory.mParishNameHistory;
                }
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            return this.mResponse;
        }

        public XmlRpcAuthResponse GetParishNames(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            try
            {
                this.FormsControl = ctrl;
                this.CallbackMethod = callbackHandler;
                this.mRequest = (XmlRpcAuthRequest) req;
                AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
                proxy.Url = this.EndpointUri;
                proxy.Timeout = timeout;
                XmlRpcRespStruct parishNames = proxy.GetParishNames(this.mRequest.Request);
                this.mResponse = new XmlRpcAuthResponse(parishNames.mMessage, parishNames.mSuccessCode);
                if (this.mResponse.SuccessCode.HasValue && (this.mResponse.SuccessCode.Value == 1))
                {
                    this.mResponse.RawParishNames = parishNames.mRawParishNames;
                }
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            return this.mResponse;
        }

        public void GetWorlds(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginGetWorlds(this.mRequest.Request, new AsyncCallback(this.GetWorldsResponse), null);
        }

        private void GetWorldsResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            AuthProxy clientProtocol = (AuthProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct struct2 = clientProtocol.EndGetWorlds(asr);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens, struct2.mVeteranLv1, struct2.mVeteranLv2, struct2.mVeteranLv3, struct2.mVeteranLv4, struct2.mVeteranLv5, struct2.mVeteranLv6, struct2.mVeteranLv7, struct2.mVeteranLv8, struct2.mVeteranLv9, struct2.mVeteranLv10, struct2.mVeteranTotalSeconds, struct2.mVeteranSecondsLeft, struct2.mVeteranCurrentLevel, struct2.mPremiumBox, struct2.mRawShields, struct2.mRawWorlds, struct2.mRawProducts, struct2.mSpecialURL);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcAuthResponse(exception.Message, 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new AuthEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void LoginBetaUser(IAuthRequest req, AuthEndResponseDelegate callbackHandler)
        {
        }

        public void LoginBetaUser(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginBeatlogin(this.mRequest.Request, new AsyncCallback(this.LoginBetaUserResponse), null);
        }

        private void LoginBetaUserResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            AuthProxy clientProtocol = (AuthProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                int? mIsbigpoint = null;
                int? mUnviewedOffers = null;
                XmlRpcRespStruct struct2 = clientProtocol.EndBetalogin(asr);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens, struct2.mVeteranLv1, struct2.mVeteranLv2, struct2.mVeteranLv3, struct2.mVeteranLv4, struct2.mVeteranLv5, struct2.mVeteranLv6, struct2.mVeteranLv7, struct2.mVeteranLv8, struct2.mVeteranLv9, struct2.mVeteranLv10, struct2.mVeteranTotalSeconds, struct2.mVeteranSecondsLeft, struct2.mVeteranCurrentLevel, struct2.mPremiumBox, struct2.mRawShields, struct2.mRawWorlds, struct2.mRawProducts, struct2.mSpecialURL);
                this.mResponse.Friends = struct2.mFriends;
                if (struct2.mIsbigpoint.HasValue)
                {
                    mIsbigpoint = struct2.mIsbigpoint;
                }
                this.mResponse.isBigPoint = (mIsbigpoint.GetValueOrDefault() == 1) && mIsbigpoint.HasValue;
                if (struct2.mUnviewedOffers.HasValue)
                {
                    mUnviewedOffers = struct2.mUnviewedOffers;
                }
                this.mResponse.hasUnviewedOffers = (mUnviewedOffers.GetValueOrDefault() == 1) && mUnviewedOffers.HasValue;
                this.mResponse.MapEditor = struct2.mMapEditor;
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcAuthResponse(exception.Message, 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new AuthEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void LogoutUser(IAuthRequest req, AuthEndResponseDelegate callbackHandler)
        {
        }

        public void LogoutUser(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Beginlogout(this.mRequest.Request, new AsyncCallback(this.LogoutUserResponse), null);
        }

        private void LogoutUserResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            AuthProxy clientProtocol = (AuthProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct struct2 = clientProtocol.Endlogout(asr);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcAuthResponse(exception.Message, 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new AuthEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public XmlRpcAuthResponse RenameParish(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            try
            {
                this.FormsControl = ctrl;
                this.CallbackMethod = callbackHandler;
                this.mRequest = (XmlRpcAuthRequest) req;
                AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
                proxy.Url = this.EndpointUri;
                proxy.Timeout = timeout;
                XmlRpcRespStruct struct2 = proxy.RenameParish(this.mRequest.Request);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode);
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            return this.mResponse;
        }

        public void SetEmailOptIn(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginsetEmailOptIn(this.mRequest.Request, new AsyncCallback(this.SetEmailOptInResponse), null);
        }

        private void SetEmailOptInResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            AuthProxy clientProtocol = (AuthProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct struct2 = clientProtocol.EndsetEmailOptIn(asr);
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens);
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new AuthEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

        public XmlRpcAuthResponse SteamGetProductList(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct struct2 = proxy.SteamGetProductList(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, struct2.mRawProducts, null);
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            return this.mResponse;
        }

        public XmlRpcAuthResponse SteamPaymentFinal(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct struct2 = proxy.SteamPaymentFinalize(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, struct2.mRawProducts, null);
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            return this.mResponse;
        }

        public XmlRpcAuthResponse SteamPaymentInit(IAuthRequest req, AuthEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcAuthRequest) req;
            AuthProxy proxy = XmlRpcProxyGen.Create<AuthProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct struct2 = proxy.SteamPaymentInit(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcAuthResponse(struct2.mMessage, struct2.mSuccessCode, struct2.mUserGUID, struct2.mUsername, struct2.mEmailAddress, struct2.mPassword, struct2.mSessionID, struct2.mIPAddress, struct2.mPlatform, struct2.mRawCards, struct2.mCrowns, struct2.mCardpoints, struct2.mPremiumCards, struct2.mRawOffers, struct2.mRawPacks, struct2.mRawSharers, struct2.mRawTokens, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, struct2.mRawProducts, null);
            }
            catch (Exception)
            {
                this.mResponse = new XmlRpcAuthResponse("Login Server Unavailable, please try again later", 0, this.mRequest.UserGUID, this.mRequest.Username, this.mRequest.EmailAddress, this.mRequest.Password, this.mRequest.SessionID, this.mRequest.IPAddress, this.mRequest.Platform, null, 0, 0, 0, null, null, null, null);
            }
            return this.mResponse;
        }

        public string EndpointProtocol
        {
            get
            {
                return this.mProtocol;
            }
            set
            {
                this.mProtocol = value;
            }
        }

        public string EndpointServerName
        {
            get
            {
                return this.mServer;
            }
            set
            {
                this.mServer = value;
            }
        }

        public string EndpointServerPath
        {
            get
            {
                return this.mPath;
            }
            set
            {
                this.mPath = value;
            }
        }

        public string EndpointServerPort
        {
            get
            {
                return this.mPort;
            }
            set
            {
                this.mPort = value;
            }
        }

        public string EndpointUri
        {
            get
            {
                return (this.mProtocol + "://" + this.mServer + ":" + this.mPort + this.mPath);
            }
        }

        public IAuthRequest Request
        {
            get
            {
                return this.mRequest;
            }
        }

        public IAuthResponse Response
        {
            get
            {
                return this.mResponse;
            }
        }
    }
}

