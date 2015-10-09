namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;
    using System.Windows.Forms;

    public class XmlRpcCardsProvider : ICardsProvider
    {
        private CardsEndResponseDelegate CallbackMethod;
        private Control FormsControl;
        private string mPath;
        private string mPort;
        private string mProtocol;
        private XmlRpcCardsRequest mRequest;
        private XmlRpcCardsResponse mResponse;
        private string mServer;

        private XmlRpcCardsProvider()
        {
        }

        public void buyCardOffer(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginbuyCardOffer(this.mRequest.Request, new AsyncCallback(this.BuyOfferResponse), null);
        }

        public void buyMultipleCards(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginbuyMultipleCards(this.mRequest.Request, new AsyncCallback(this.BuyMultipleResponse), null);
        }

        public void BuyMultipleResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndbuyMultipleCards(asr);
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode, cards.mStrings);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void BuyOfferResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndbuyCardOffer(asr);
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void buyPremium(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginbuyPremium(this.mRequest.Request, new AsyncCallback(this.BuyPremiumResponse), null);
        }

        public void BuyPremiumResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndbuyPremium(asr);
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode, cards.mStrings);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public XmlRpcCardsResponse cancelVacation(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct_Cards cards = proxy.cancelVacation(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            return this.mResponse;
        }

        public void cashInCards(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BegincashInCards(this.mRequest.Request, new AsyncCallback(this.CashInResponse), null);
        }

        public void CashInResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndcashInCards(asr);
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode, cards.mNewpoints, cards.mSymbols);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public XmlRpcCardsResponse checkInvitedFriends(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct_Cards cards = proxy.checkInvitedFriends(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode);
                this.mResponse.Friends = cards.mFriends;
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            return this.mResponse;
        }

        public static XmlRpcCardsProvider CreateForEndpoint(string protocol, string server, string port, string path)
        {
            return new XmlRpcCardsProvider { EndpointProtocol = protocol, EndpointServerName = server, EndpointServerPath = path, EndpointServerPort = port };
        }

        public XmlRpcCardsResponse doVacation(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct_Cards cards = proxy.doVacation(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            return this.mResponse;
        }

        public void GetCardCatalog(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginGetCardCatalog(this.mRequest.Request, new AsyncCallback(this.GetCardCatalogResponse), null);
        }

        public void GetCardCatalogResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndGetCardCatalog(asr);
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode, cards.mUserGUID, cards.mRawCards);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0, this.mRequest.UserGUID, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void getCardOffers(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BegingetCardOffers(this.mRequest.Request, new AsyncCallback(this.GetOffersResponse), null);
        }

        public void getCrowns(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BegingetCrowns(this.mRequest.Request, new AsyncCallback(this.getCrownsResponse), null);
        }

        public void getCrownsResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndgetCrowns(asr);
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode, cards.mUserGUID, null, null, cards.mCrowns, null, null);
                this.mResponse.RawCardPacks = cards.mRawPacks;
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void getFreeCard(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BegingetFreeCard(this.mRequest.Request, new AsyncCallback(this.getFreeCardResponse), null);
        }

        public void getFreeCardResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndgetVeteranLevel(asr);
                this.mResponse = new XmlRpcCardsResponse();
                XmlRpcRespStruct_Cards cards2 = new XmlRpcRespStruct_Cards {
                    mVeteranLv1 = cards.mVeteranLv1,
                    mVeteranLv2 = cards.mVeteranLv2,
                    mVeteranLv3 = cards.mVeteranLv3,
                    mVeteranLv4 = cards.mVeteranLv4,
                    mVeteranLv5 = cards.mVeteranLv5,
                    mVeteranLv6 = cards.mVeteranLv6,
                    mVeteranLv7 = cards.mVeteranLv7,
                    mVeteranLv8 = cards.mVeteranLv8,
                    mVeteranLv9 = cards.mVeteranLv9,
                    mVeteranLv10 = cards.mVeteranLv10,
                    mVeteranSecondsLeft = cards.mVeteranSecondsLeft,
                    mMessage = cards.mMessage,
                    mSuccessCode = cards.mSuccessCode,
                    mVeteranCurrentLevel = cards.mVeteranCurrentLevel,
                    mStrings = cards.mStrings
                };
                this.mResponse.Response = cards2;
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            try
            {
                this.CallbackMethod(this, this.Response);
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void getFreeCrowns(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BegingetFreeCrowns(this.mRequest.Request, new AsyncCallback(this.GetFreeResponse), null);
        }

        public void GetFreeResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndgetFreeCrowns(asr);
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode, cards.mNumcrowns, cards.mSeconds);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void GetOffersResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndgetCardOffers(asr);
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode, cards.mCountOffers, cards.mRawOffers);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0, 0, 0);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void getRewardCards(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BegingetRewardCards(this.mRequest.Request, new AsyncCallback(this.getRewardCardsResponse), null);
        }

        public void getRewardCardsResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndgetRewardCards(asr);
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode, cards.mUserGUID, cards.mRawCards, null, cards.mCrowns, cards.mCardpoints, null);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void GetUserCards(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BegingetUserCards(this.mRequest.Request, new AsyncCallback(this.GetUserCardsResponse), null);
        }

        public void GetUserCardsResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndgetUserCards(asr);
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode, cards.mUserGUID, cards.mRawCards, cards.mCardID, cards.mCrowns, cards.mCardpoints, cards.mPremiumCards);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0, this.mRequest.UserGUID, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void getVeteranLevel(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BegingetVeteranLevel(this.mRequest.Request, new AsyncCallback(this.getVeteranLevelResponse), null);
        }

        public void getVeteranLevelResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndgetVeteranLevel(asr);
                this.mResponse = new XmlRpcCardsResponse();
                XmlRpcRespStruct_Cards cards2 = new XmlRpcRespStruct_Cards {
                    mVeteranLv1 = cards.mVeteranLv1,
                    mVeteranLv2 = cards.mVeteranLv2,
                    mVeteranLv3 = cards.mVeteranLv3,
                    mVeteranLv4 = cards.mVeteranLv4,
                    mVeteranLv5 = cards.mVeteranLv5,
                    mVeteranLv6 = cards.mVeteranLv6,
                    mVeteranLv7 = cards.mVeteranLv7,
                    mVeteranLv8 = cards.mVeteranLv8,
                    mVeteranLv9 = cards.mVeteranLv9,
                    mVeteranLv10 = cards.mVeteranLv10,
                    mVeteranSecondsLeft = cards.mVeteranSecondsLeft,
                    mMessage = cards.mMessage,
                    mSuccessCode = cards.mSuccessCode,
                    mVeteranCurrentLevel = cards.mVeteranCurrentLevel
                };
                this.mResponse.Response = cards2;
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public XmlRpcCardsResponse giveCardPacks(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct_Cards cards = proxy.giveCardPacks(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            return this.mResponse;
        }

        public XmlRpcCardsResponse giveCardPoints(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct_Cards cards = proxy.giveCardPoints(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode, cards.mStrings, cards.mCardpoints);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            return this.mResponse;
        }

        public XmlRpcCardsResponse giveCards(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct_Cards cards = proxy.giveCards(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode, cards.mStrings);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            return this.mResponse;
        }

        public XmlRpcCardsResponse givePremium(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct_Cards cards = proxy.givePremium(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode, cards.mStrings);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            return this.mResponse;
        }

        public XmlRpcCardsResponse ingameBan(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct_Cards cards = proxy.ingameBan(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            return this.mResponse;
        }

        public XmlRpcCardsResponse makeChargeAvailable(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct_Cards cards = proxy.makeChargeAvailable(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            return this.mResponse;
        }

        public void openCardPack(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginopenCardPack(this.mRequest.Request, new AsyncCallback(this.OpenPackResponse), null);
        }

        public void OpenPackResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndopenCardPack(asr);
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode, cards.mStrings);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void PlayCardResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndplayCard(asr);
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode, cards.mUserGUID, cards.mRawCards, cards.mCardID);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0, this.mRequest.UserGUID, null);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public XmlRpcCardsResponse playPremium(ICardsRequest req, int timeout)
        {
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct_Cards cards = proxy.playPremium(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            return this.mResponse;
        }

        public void playPremium(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginplayPremium(this.mRequest.Request, new AsyncCallback(this.PlayPremiumResponse), null);
        }

        public void PlayPremiumResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndplayPremium(asr);
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
        }

        public void PlayUserCard(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginplayCard(this.mRequest.Request, new AsyncCallback(this.PlayCardResponse), null);
        }

        public XmlRpcCardsResponse setVeteranData(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl, int timeout)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.Timeout = timeout;
            XmlRpcRespStruct_Cards cards = proxy.setVeteranData(this.mRequest.Request);
            try
            {
                this.mResponse = new XmlRpcCardsResponse(cards.mMessage, cards.mSuccessCode);
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            return this.mResponse;
        }

        public void veteranLevelUp(ICardsRequest req, CardsEndResponseDelegate callbackHandler, Control ctrl)
        {
            this.FormsControl = ctrl;
            this.CallbackMethod = callbackHandler;
            this.mRequest = (XmlRpcCardsRequest) req;
            CardsProxy proxy = XmlRpcProxyGen.Create<CardsProxy>();
            proxy.Url = this.EndpointUri;
            proxy.BeginveteranLevelUp(this.mRequest.Request, new AsyncCallback(this.veteranLevelUpResponse), null);
        }

        public void veteranLevelUpResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            CardsProxy clientProtocol = (CardsProxy) result.ClientProtocol;
            clientProtocol.Url = this.EndpointUri;
            try
            {
                XmlRpcRespStruct_Cards cards = clientProtocol.EndgetVeteranLevel(asr);
                this.mResponse = new XmlRpcCardsResponse();
                XmlRpcRespStruct_Cards cards2 = new XmlRpcRespStruct_Cards {
                    mVeteranLv1 = cards.mVeteranLv1,
                    mVeteranLv2 = cards.mVeteranLv2,
                    mVeteranLv3 = cards.mVeteranLv3,
                    mVeteranLv4 = cards.mVeteranLv4,
                    mVeteranLv5 = cards.mVeteranLv5,
                    mVeteranLv6 = cards.mVeteranLv6,
                    mVeteranLv7 = cards.mVeteranLv7,
                    mVeteranLv8 = cards.mVeteranLv8,
                    mVeteranLv9 = cards.mVeteranLv9,
                    mVeteranLv10 = cards.mVeteranLv10,
                    mVeteranSecondsLeft = cards.mVeteranSecondsLeft,
                    mMessage = cards.mMessage,
                    mSuccessCode = cards.mSuccessCode,
                    mVeteranCurrentLevel = cards.mVeteranCurrentLevel
                };
                this.mResponse.Response = cards2;
            }
            catch (Exception exception)
            {
                this.mResponse = new XmlRpcCardsResponse(exception.Message, 0);
            }
            try
            {
                if (this.FormsControl != null)
                {
                    this.FormsControl.Invoke(new CardsEndResponseDelegate(this.CallbackMethod.Invoke), new object[] { this, this.Response });
                }
            }
            catch (Exception exception2)
            {
                string message = exception2.Message;
            }
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

        public ICardsRequest Request
        {
            get
            {
                return this.mRequest;
            }
        }

        public ICardsResponse Response
        {
            get
            {
                return this.mResponse;
            }
        }
    }
}

