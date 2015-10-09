namespace Stronghold.AuthClient
{
    using System;

    public class XmlRpcAuthRequest : IAuthRequest
    {
        protected XmlRpcReqStruct mRequest;

        public XmlRpcAuthRequest()
        {
            this.mRequest = new XmlRpcReqStruct();
            this.mRequest.mUserGUID = string.Empty;
            this.mRequest.mUsername = string.Empty;
            this.mRequest.mEmailAddress = string.Empty;
            this.mRequest.mPassword = string.Empty;
            this.mRequest.mSessionID = string.Empty;
            this.mRequest.mIPAddress = string.Empty;
            this.mRequest.mPlatform = string.Empty;
            this.mRequest.mAdmin = string.Empty;
            this.mRequest.mPlaying = 0;
            this.mRequest.mWorldID = 0;
            this.mRequest.mFirstWorld = 0;
            this.mRequest.mSteamID = string.Empty;
            this.mRequest.mCulture = string.Empty;
            this.mRequest.mCountryCode = string.Empty;
            this.mRequest.mCurrency = string.Empty;
            this.mRequest.mItemID = string.Empty;
            this.mRequest.mOrderID = string.Empty;
            this.mRequest.mAeriaToken = string.Empty;
        }

        public XmlRpcAuthRequest(string guid, string username, string email, string pass, string sessionid, string ip, string platform, string admin)
        {
            this.mRequest = new XmlRpcReqStruct();
            this.mRequest.mUserGUID = guid;
            this.mRequest.mUsername = username;
            this.mRequest.mEmailAddress = email;
            this.mRequest.mPassword = pass;
            this.mRequest.mSessionID = sessionid;
            this.mRequest.mIPAddress = ip;
            this.mRequest.mPlatform = platform;
            this.mRequest.mAdmin = admin;
        }

        public XmlRpcAuthRequest(string guid, string username, string email, string pass, string sessionid, string ip, string platform, string admin, int? worldid, int? first, int? playing)
        {
            this.mRequest = new XmlRpcReqStruct();
            this.mRequest.mUserGUID = guid;
            this.mRequest.mUsername = username;
            this.mRequest.mEmailAddress = email;
            this.mRequest.mPassword = pass;
            this.mRequest.mSessionID = sessionid;
            this.mRequest.mIPAddress = ip;
            this.mRequest.mPlatform = platform;
            this.mRequest.mAdmin = admin;
            if (worldid.HasValue)
            {
                this.mRequest.mWorldID = worldid;
            }
            if (first.HasValue)
            {
                this.mRequest.mFirstWorld = first;
            }
            if (playing.HasValue)
            {
                this.mRequest.mPlaying = playing;
            }
        }

        public string Admin
        {
            get
            {
                return this.mRequest.mAdmin;
            }
            set
            {
                this.mRequest.mAdmin = value;
            }
        }

        public string AeriaToken
        {
            get
            {
                return this.mRequest.mAeriaToken;
            }
            set
            {
                this.mRequest.mAeriaToken = value;
            }
        }

        public string Country
        {
            get
            {
                return this.mRequest.mCountryCode;
            }
            set
            {
                this.mRequest.mCountryCode = value;
            }
        }

        public string Culture
        {
            get
            {
                return this.mRequest.mCulture;
            }
            set
            {
                this.mRequest.mCulture = value;
            }
        }

        public string Currency
        {
            get
            {
                return this.mRequest.mCurrency;
            }
            set
            {
                this.mRequest.mCurrency = value;
            }
        }

        public string EmailAddress
        {
            get
            {
                return this.mRequest.mEmailAddress;
            }
            set
            {
                this.mRequest.mEmailAddress = value;
            }
        }

        public int? FirstWorld
        {
            get
            {
                return this.mRequest.mFirstWorld;
            }
            set
            {
                this.mRequest.mFirstWorld = value;
            }
        }

        public string IPAddress
        {
            get
            {
                return this.mRequest.mIPAddress;
            }
            set
            {
                this.mRequest.mIPAddress = value;
            }
        }

        public string ItemID
        {
            get
            {
                return this.mRequest.mItemID;
            }
            set
            {
                this.mRequest.mItemID = value;
            }
        }

        public string OrderID
        {
            get
            {
                return this.mRequest.mOrderID;
            }
            set
            {
                this.mRequest.mOrderID = value;
            }
        }

        public int? ParishID
        {
            get
            {
                return this.mRequest.mParishId;
            }
            set
            {
                this.mRequest.mParishId = value;
            }
        }

        public string ParishName
        {
            get
            {
                return this.mRequest.mParishName;
            }
            set
            {
                this.mRequest.mParishName = value;
            }
        }

        public string Password
        {
            get
            {
                return this.mRequest.mPassword;
            }
            set
            {
                this.mRequest.mPassword = value;
            }
        }

        public string Platform
        {
            get
            {
                return this.mRequest.mPlatform;
            }
            set
            {
                this.mRequest.mPlatform = value;
            }
        }

        public int? Playing
        {
            get
            {
                return this.mRequest.mPlaying;
            }
            set
            {
                this.mRequest.mPlaying = value;
            }
        }

        public XmlRpcReqStruct Request
        {
            get
            {
                return this.mRequest;
            }
        }

        public string SessionID
        {
            get
            {
                return this.mRequest.mSessionID;
            }
            set
            {
                this.mRequest.mSessionID = value;
            }
        }

        public string SteamID
        {
            get
            {
                return this.mRequest.mSteamID;
            }
            set
            {
                this.mRequest.mSteamID = value;
            }
        }

        public string UserGUID
        {
            get
            {
                return this.mRequest.mUserGUID;
            }
            set
            {
                this.mRequest.mUserGUID = value;
            }
        }

        public string Username
        {
            get
            {
                return this.mRequest.mUsername;
            }
            set
            {
                this.mRequest.mUsername = value;
            }
        }

        public int? WorldID
        {
            get
            {
                return this.mRequest.mWorldID;
            }
            set
            {
                this.mRequest.mWorldID = value;
            }
        }
    }
}

