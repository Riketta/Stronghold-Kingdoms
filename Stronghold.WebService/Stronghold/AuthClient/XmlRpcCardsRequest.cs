namespace Stronghold.AuthClient
{
    using System;

    public class XmlRpcCardsRequest : ICardsRequest
    {
        protected XmlRpcReqStruct_Cards mRequest;

        public XmlRpcCardsRequest()
        {
            this.mRequest = new XmlRpcReqStruct_Cards();
            this.mRequest.mUserGUID = string.Empty;
        }

        public XmlRpcCardsRequest(string guid)
        {
            this.mRequest = new XmlRpcReqStruct_Cards();
            this.mRequest.mUserGUID = guid;
        }

        public XmlRpcCardsRequest(string guid, string offer)
        {
            this.mRequest = new XmlRpcReqStruct_Cards();
            this.mRequest.mUserGUID = guid;
            this.mRequest.mOfferID = offer;
        }

        public XmlRpcCardsRequest(string guid, string rank, string packid, string worldid)
        {
            this.mRequest = new XmlRpcReqStruct_Cards();
            this.mRequest.mUserGUID = guid;
            this.mRequest.mPackID = packid;
            this.mRequest.mRank = rank;
            this.mRequest.mWorldID = worldid;
        }

        public XmlRpcCardsRequest(string guid, string session, string cardid, string villageid, string worldid)
        {
            this.mRequest = new XmlRpcReqStruct_Cards();
            this.mRequest.mUserGUID = guid;
            this.mRequest.mSessionGUID = session;
            this.mRequest.mUserCardID = cardid;
            this.mRequest.mVillageID = villageid;
            this.mRequest.mWorldID = worldid;
        }

        public int? CardPoints
        {
            get
            {
                return this.mRequest.mCardPoints;
            }
            set
            {
                this.mRequest.mCardPoints = value;
            }
        }

        public string CardString
        {
            get
            {
                return this.mRequest.mCardString;
            }
            set
            {
                this.mRequest.mCardString = value;
            }
        }

        public string ChargeDesc
        {
            get
            {
                return this.mRequest.mChargeDesc;
            }
            set
            {
                this.mRequest.mChargeDesc = value;
            }
        }

        public int? Multiple
        {
            get
            {
                return this.mRequest.mMultiple;
            }
            set
            {
                this.mRequest.mMultiple = value;
            }
        }

        public int? NumPacks
        {
            get
            {
                return this.mRequest.mNumPacks;
            }
            set
            {
                this.mRequest.mNumPacks = value;
            }
        }

        public string OfferID
        {
            get
            {
                return this.mRequest.mOfferID;
            }
            set
            {
                this.mRequest.mOfferID = value;
            }
        }

        public string PackID
        {
            get
            {
                return this.mRequest.mPackID;
            }
            set
            {
                this.mRequest.mPackID = value;
            }
        }

        public string Rank
        {
            get
            {
                return this.mRequest.mRank;
            }
            set
            {
                this.mRequest.mRank = value;
            }
        }

        public XmlRpcReqStruct_Cards Request
        {
            get
            {
                return this.mRequest;
            }
        }

        public string SessionGUID
        {
            get
            {
                return this.mRequest.mSessionGUID;
            }
            set
            {
                this.mRequest.mSessionGUID = value;
            }
        }

        public string UserCardID
        {
            get
            {
                return this.mRequest.mUserCardID;
            }
            set
            {
                this.mRequest.mUserCardID = value;
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

        public int? VacationLength
        {
            get
            {
                return this.mRequest.mVacationLength;
            }
            set
            {
                this.mRequest.mVacationLength = value;
            }
        }

        public int? VeteranDonated
        {
            get
            {
                return this.mRequest.mVeteranDonated;
            }
            set
            {
                this.mRequest.mVeteranDonated = value;
            }
        }

        public int? VeteranFaction
        {
            get
            {
                return this.mRequest.mVeteranFaction;
            }
            set
            {
                this.mRequest.mVeteranFaction = value;
            }
        }

        public int? VeteranRank
        {
            get
            {
                return this.mRequest.mVeteranRank;
            }
            set
            {
                this.mRequest.mVeteranRank = value;
            }
        }

        public string VillageID
        {
            get
            {
                return this.mRequest.mVillageID;
            }
            set
            {
                this.mRequest.mVillageID = value;
            }
        }

        public string WorldID
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

