namespace Stronghold.AuthClient
{
    using CommonTypes;
    using CookComputing.XmlRpc;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class XmlRpcCardsResponse : ICardsResponse
    {
        private Dictionary<int, string> mCards;
        private Dictionary<int, CardTypes.CardOffer> mOffers;
        private Dictionary<int, CardTypes.UserCardPack> mPacks;
        public XmlRpcRespStruct_Cards mResponse;
        private List<int> mSymbolList;

        public XmlRpcCardsResponse()
        {
        }

        public XmlRpcCardsResponse(string message, int? success)
        {
            this.mResponse.mMessage = message;
            this.mResponse.mSuccessCode = success;
        }

        public XmlRpcCardsResponse(string message, int? success, string strings)
        {
            this.mResponse.mMessage = message;
            this.mResponse.mSuccessCode = success;
            this.mResponse.mStrings = strings;
        }

        public XmlRpcCardsResponse(string message, int? success, int? numcrowns, int? seconds)
        {
            this.mResponse.mMessage = message;
            this.mResponse.mSuccessCode = success;
            this.mResponse.mNumcrowns = numcrowns;
            this.mResponse.mSeconds = seconds;
        }

        public XmlRpcCardsResponse(string message, int? success, int? countoffers, object rawoffers)
        {
            this.mResponse.mMessage = message;
            this.mResponse.mSuccessCode = success;
            this.mResponse.mCountOffers = countoffers;
            this.mResponse.mRawOffers = rawoffers;
        }

        public XmlRpcCardsResponse(string message, int? success, int? newpoints, string symbols)
        {
            this.mResponse.mMessage = message;
            this.mResponse.mSuccessCode = success;
            this.mResponse.mNewpoints = newpoints;
            this.mResponse.mSymbols = symbols;
        }

        public XmlRpcCardsResponse(string message, int? success, string strings, int? cardpoints)
        {
            this.mResponse.mMessage = message;
            this.mResponse.mSuccessCode = success;
            this.mResponse.mStrings = strings;
            this.mResponse.mCardpoints = cardpoints;
        }

        public XmlRpcCardsResponse(string message, int? success, string guid, object rawcards)
        {
            this.mResponse = new XmlRpcRespStruct_Cards();
            this.mResponse.mMessage = message;
            this.mResponse.mSuccessCode = success;
            this.mResponse.mUserGUID = guid;
            this.mResponse.mRawCards = rawcards;
        }

        public XmlRpcCardsResponse(string message, int? success, string guid, object rawcards, int? cardID)
        {
            this.mResponse = new XmlRpcRespStruct_Cards();
            this.mResponse.mMessage = message;
            this.mResponse.mSuccessCode = success;
            this.mResponse.mUserGUID = guid;
            this.mResponse.mRawCards = rawcards;
            this.mResponse.mCardID = cardID;
        }

        public XmlRpcCardsResponse(string message, int? success, string guid, object rawcards, int? cardID, int? crowns, int? cardpoints, int? premium)
        {
            this.mResponse = new XmlRpcRespStruct_Cards();
            this.mResponse.mMessage = message;
            this.mResponse.mSuccessCode = success;
            this.mResponse.mUserGUID = guid;
            this.mResponse.mRawCards = rawcards;
            this.mResponse.mCardID = cardID;
            this.mResponse.mCrowns = crowns;
            this.mResponse.mCardpoints = cardpoints;
            this.mResponse.mPremiumCards = premium;
        }

        public int? CardID
        {
            get
            {
                return this.mResponse.mCardID;
            }
        }

        public Dictionary<int, CardTypes.CardOffer> CardOffers
        {
            get
            {
                if (this.mOffers == null)
                {
                    this.mOffers = new Dictionary<int, CardTypes.CardOffer>();
                    if (this.mResponse.mRawOffers != null)
                    {
                        try
                        {
                            int result = 0;
                            Hashtable rawOffers = (Hashtable) this.RawOffers;
                            foreach (string str in rawOffers.Keys)
                            {
                                if (int.TryParse(str, out result))
                                {
                                    CardTypes.CardOffer offer = new CardTypes.CardOffer {
                                        ID = result,
                                        Name = (string) ((XmlRpcStruct) rawOffers[str])["PackName"],
                                        NumberOfCards = int.Parse((string) ((XmlRpcStruct) rawOffers[str])["NumberOfCards"]),
                                        Description = (string) ((XmlRpcStruct) rawOffers[str])["Description"],
                                        CrownCost = int.Parse((string) ((XmlRpcStruct) rawOffers[str])["CrownCost"]),
                                        Sequence = int.Parse((string) ((XmlRpcStruct) rawOffers[str])["Sequence"]),
                                        Buyable = int.Parse((string) ((XmlRpcStruct) rawOffers[str])["Buyable"]),
                                        Category = (string) ((XmlRpcStruct) rawOffers[str])["Category"]
                                    };
                                    this.mOffers.Add(result, offer);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            this.mOffers = new Dictionary<int, CardTypes.CardOffer>();
                            return this.mOffers;
                        }
                    }
                }
                return this.mOffers;
            }
        }

        public int? Cardpoints
        {
            get
            {
                return this.mResponse.mCardpoints;
            }
        }

        public Dictionary<int, string> Cards
        {
            get
            {
                if (this.mCards == null)
                {
                    this.mCards = new Dictionary<int, string>();
                    if (this.mResponse.mRawCards != null)
                    {
                        try
                        {
                            int result = 0;
                            Hashtable rawCards = (Hashtable) this.RawCards;
                            foreach (string str in rawCards.Keys)
                            {
                                if (int.TryParse(str, out result))
                                {
                                    this.mCards.Add(result, ((XmlRpcStruct) rawCards[str])["type"].ToString());
                                }
                            }
                        }
                        catch (Exception)
                        {
                            this.mCards = new Dictionary<int, string>();
                            return this.mCards;
                        }
                    }
                }
                return this.mCards;
            }
        }

        public int? CountOffers
        {
            get
            {
                return this.mResponse.mCountOffers;
            }
        }

        public int? Crowns
        {
            get
            {
                return this.mResponse.mCrowns;
            }
        }

        public int? Friends
        {
            get
            {
                return this.mResponse.mFriends;
            }
            set
            {
                this.mResponse.mFriends = value;
            }
        }

        public string Message
        {
            get
            {
                return this.mResponse.mMessage;
            }
        }

        public int[] NewCardIDs
        {
            get
            {
                try
                {
                    if (this.mResponse.mSuccessCode == 1)
                    {
                        string[] strArray = this.Strings.TrimEnd(",".ToCharArray()).Split(",".ToCharArray());
                        int[] numArray = new int[strArray.Length];
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            numArray[i] = Convert.ToInt32(strArray[(strArray.Length - 1) - i].Trim());
                        }
                        return numArray;
                    }
                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public int? Newpoints
        {
            get
            {
                return this.mResponse.mNewpoints;
            }
        }

        public int? Numcrowns
        {
            get
            {
                return this.mResponse.mNumcrowns;
            }
        }

        public int? PremiumCards
        {
            get
            {
                return this.mResponse.mPremiumCards;
            }
        }

        public object RawCardPacks
        {
            get
            {
                return this.mResponse.mRawPacks;
            }
            set
            {
                this.mResponse.mRawPacks = value;
            }
        }

        public object RawCards
        {
            get
            {
                return this.mResponse.mRawCards;
            }
        }

        public object RawOffers
        {
            get
            {
                return this.mResponse.mRawOffers;
            }
        }

        public XmlRpcRespStruct_Cards Response
        {
            get
            {
                return this.mResponse;
            }
            set
            {
                this.mResponse = value;
            }
        }

        public int? Seconds
        {
            get
            {
                return this.mResponse.mSeconds;
            }
        }

        public string Strings
        {
            get
            {
                return this.mResponse.mStrings;
            }
        }

        public int? SuccessCode
        {
            get
            {
                return this.mResponse.mSuccessCode;
            }
        }

        public List<int> SymbolList
        {
            get
            {
                if (this.mSymbolList == null)
                {
                    this.mSymbolList = new List<int>();
                    foreach (string str in this.mResponse.mSymbols.Split(",".ToCharArray()))
                    {
                        if (str.Length > 0)
                        {
                            switch (str.Trim().ToUpper())
                            {
                                case "APPLE":
                                {
                                    this.mSymbolList.Add(0x1000000);
                                    continue;
                                }
                                case "CROWN":
                                {
                                    this.mSymbolList.Add(0x40000000);
                                    continue;
                                }
                                case "HAWKSHEAD":
                                {
                                    this.mSymbolList.Add(0x4000000);
                                    continue;
                                }
                                case "JESTERSHEAD":
                                {
                                    this.mSymbolList.Add(0x20000000);
                                    continue;
                                }
                                case "SHIELD":
                                {
                                    this.mSymbolList.Add(0x8000000);
                                    continue;
                                }
                                case "TOWER":
                                {
                                    this.mSymbolList.Add(0x10000000);
                                    continue;
                                }
                                case "WOLFSHEAD":
                                {
                                    this.mSymbolList.Add(0x2000000);
                                    continue;
                                }
                            }
                            this.mSymbolList.Add(0x1000000);
                        }
                    }
                }
                return this.mSymbolList;
            }
        }

        public string Symbols
        {
            get
            {
                return this.mResponse.mSymbols;
            }
        }

        public Dictionary<int, CardTypes.UserCardPack> UserCardPacks
        {
            get
            {
                if (this.mPacks == null)
                {
                    this.mPacks = new Dictionary<int, CardTypes.UserCardPack>();
                    if (this.mResponse.mRawPacks != null)
                    {
                        try
                        {
                            int result = 0;
                            Hashtable rawCardPacks = (Hashtable) this.RawCardPacks;
                            foreach (string str in rawCardPacks.Keys)
                            {
                                if (int.TryParse(str, out result))
                                {
                                    CardTypes.UserCardPack pack = new CardTypes.UserCardPack {
                                        PackID = int.Parse((string) ((XmlRpcStruct) rawCardPacks[str])["PackID"]),
                                        Count = int.Parse((string) ((XmlRpcStruct) rawCardPacks[str])["Count"])
                                    };
                                    this.mPacks.Add(pack.PackID, pack);
                                }
                            }
                        }
                        catch
                        {
                            this.mPacks = new Dictionary<int, CardTypes.UserCardPack>();
                            return this.mPacks;
                        }
                    }
                }
                return this.mPacks;
            }
        }

        public string UserGUID
        {
            get
            {
                return this.mResponse.mUserGUID;
            }
        }

        public int? VeteranCurrentLevel
        {
            get
            {
                return this.mResponse.mVeteranCurrentLevel;
            }
        }

        public int? VeteranLevel1
        {
            get
            {
                return this.mResponse.mVeteranLv1;
            }
        }

        public int? VeteranLevel10
        {
            get
            {
                return this.mResponse.mVeteranLv10;
            }
        }

        public int? VeteranLevel2
        {
            get
            {
                return this.mResponse.mVeteranLv2;
            }
        }

        public int? VeteranLevel3
        {
            get
            {
                return this.mResponse.mVeteranLv3;
            }
        }

        public int? VeteranLevel4
        {
            get
            {
                return this.mResponse.mVeteranLv4;
            }
        }

        public int? VeteranLevel5
        {
            get
            {
                return this.mResponse.mVeteranLv5;
            }
        }

        public int? VeteranLevel6
        {
            get
            {
                return this.mResponse.mVeteranLv6;
            }
        }

        public int? VeteranLevel7
        {
            get
            {
                return this.mResponse.mVeteranLv7;
            }
        }

        public int? VeteranLevel8
        {
            get
            {
                return this.mResponse.mVeteranLv8;
            }
        }

        public int? VeteranLevel9
        {
            get
            {
                return this.mResponse.mVeteranLv9;
            }
        }

        public int? VeteranSecondsLeft
        {
            get
            {
                return this.mResponse.mVeteranSecondsLeft;
            }
        }

        public int? VeteranTotalSeconds
        {
            get
            {
                return this.mResponse.mVeteranTotalSeconds;
            }
        }
    }
}

