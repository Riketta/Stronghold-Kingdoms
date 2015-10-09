namespace Stronghold.AuthClient
{
    using CommonTypes;
    using CookComputing.XmlRpc;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;

    public class XmlRpcAuthResponse : IAuthResponse
    {
        private Dictionary<int, string> mCards;
        private Dictionary<int, CardTypes.CardOffer> mOffers;
        private Dictionary<int, CardTypes.UserCardPack> mPacks;
        private List<string> mParishNameHistory;
        private Dictionary<int, string> mParishNames;
        private List<ProductInfo> mProducts;
        protected XmlRpcRespStruct mResponse;
        //private List<string> mSharers;
        private Dictionary<string, string> mShields;
        private Dictionary<int, CardTypes.PremiumToken> mTokens;
        private List<WorldInfo> mWorlds;
        private WorldsComparer worldsComparer;

        public XmlRpcAuthResponse()
        {
            this.worldsComparer = new WorldsComparer();
            this.mResponse = new XmlRpcRespStruct();
        }

        public XmlRpcAuthResponse(string message, int? success)
        {
            this.worldsComparer = new WorldsComparer();
            this.mResponse = new XmlRpcRespStruct();
            this.mResponse.mSuccessCode = success;
            this.mResponse.mMessage = message;
        }

        public XmlRpcAuthResponse(string message, int? success, string guid, string username, string email, string pass, string sessionid, string ip, string platform, object rawcards, int? crowns, int? cardpoints, int? premiumcards, object rawoffers, object rawpacks, object rawsharers, object rawtokens)
        {
            this.worldsComparer = new WorldsComparer();
            this.mResponse = new XmlRpcRespStruct();
            this.mResponse.mMessage = message;
            this.mResponse.mSuccessCode = success;
            this.mResponse.mUserGUID = guid;
            this.mResponse.mUsername = username;
            this.mResponse.mEmailAddress = email;
            this.mResponse.mPassword = pass;
            this.mResponse.mSessionID = sessionid;
            this.mResponse.mIPAddress = ip;
            this.mResponse.mPlatform = platform;
            this.mResponse.mRawCards = rawcards;
            this.mResponse.mCrowns = crowns;
            this.mResponse.mCardpoints = cardpoints;
            this.mResponse.mPremiumCards = premiumcards;
            this.mResponse.mRawOffers = rawoffers;
            this.mResponse.mRawPacks = rawpacks;
            this.mResponse.mRawSharers = rawsharers;
            this.mResponse.mRawTokens = rawtokens;
        }

        public XmlRpcAuthResponse(string message, int? success, string guid, string username, string email, string pass, string sessionid, string ip, string platform, object rawcards, int? crowns, int? cardpoints, int? premiumcards, object rawoffers, object rawpacks, object rawsharers, object rawtokens, int? l1, int? l2, int? l3, int? l4, int? l5, int? l6, int? l7, int? l8, int? l9, int? l10, int? total, int? left, int? current, int? premiumbox, object shields, object worlds, object products, string specialURL)
        {
            this.worldsComparer = new WorldsComparer();
            this.mResponse = new XmlRpcRespStruct();
            this.mResponse.mVeteranCurrentLevel = current;
            this.mResponse.mVeteranLv1 = l1;
            this.mResponse.mVeteranLv2 = l2;
            this.mResponse.mVeteranLv3 = l3;
            this.mResponse.mVeteranLv4 = l4;
            this.mResponse.mVeteranLv5 = l5;
            this.mResponse.mVeteranLv6 = l6;
            this.mResponse.mVeteranLv7 = l7;
            this.mResponse.mVeteranLv8 = l8;
            this.mResponse.mVeteranLv9 = l9;
            this.mResponse.mVeteranLv10 = l10;
            this.mResponse.mVeteranSecondsLeft = left;
            this.mResponse.mVeteranTotalSeconds = total;
            this.mResponse.mMessage = message;
            this.mResponse.mSuccessCode = success;
            this.mResponse.mUserGUID = guid;
            this.mResponse.mUsername = username;
            this.mResponse.mEmailAddress = email;
            this.mResponse.mPassword = pass;
            this.mResponse.mSessionID = sessionid;
            this.mResponse.mIPAddress = ip;
            this.mResponse.mPlatform = platform;
            this.mResponse.mRawCards = rawcards;
            this.mResponse.mCrowns = crowns;
            this.mResponse.mCardpoints = cardpoints;
            this.mResponse.mPremiumCards = premiumcards;
            this.mResponse.mRawOffers = rawoffers;
            this.mResponse.mRawPacks = rawpacks;
            this.mResponse.mRawSharers = rawsharers;
            this.mResponse.mRawTokens = rawtokens;
            this.mResponse.mPremiumBox = premiumbox;
            this.mResponse.mRawShields = shields;
            this.mResponse.mRawWorlds = worlds;
            this.mResponse.mRawProducts = products;
            this.mResponse.mSpecialURL = specialURL;
        }

        public int? CancelVacation
        {
            get
            {
                return this.mResponse.mCancelVacation;
            }
            set
            {
                this.mResponse.mCancelVacation = value;
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

        public int? Crowns
        {
            get
            {
                return this.mResponse.mCrowns;
            }
        }

        public string EmailAddress
        {
            get
            {
                return this.mResponse.mEmailAddress;
            }
        }

        public int? FacebookFreePack
        {
            get
            {
                return this.mResponse.mFBFreePack;
            }
            set
            {
                this.mResponse.mFBFreePack = value;
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

        public bool hasUnviewedOffers
        {
            get
            {
                if (!this.mResponse.mUnviewedOffers.HasValue)
                {
                    return false;
                }
                return (this.mResponse.mUnviewedOffers == 1);
            }
            set
            {
                if (value)
                {
                    this.mResponse.mUnviewedOffers = 1;
                }
                else
                {
                    this.mResponse.mUnviewedOffers = 0;
                }
            }
        }

        public string IPAddress
        {
            get
            {
                return this.mResponse.mIPAddress;
            }
        }

        public bool isBigPoint
        {
            get
            {
                if (!this.mResponse.mIsbigpoint.HasValue)
                {
                    return false;
                }
                return (this.mResponse.mIsbigpoint == 1);
            }
            set
            {
                if (value)
                {
                    this.mResponse.mIsbigpoint = 1;
                }
                else
                {
                    this.mResponse.mIsbigpoint = 0;
                }
            }
        }

        public bool isMapEditor
        {
            get
            {
                return (this.MapEditor.HasValue && (this.MapEditor.Value == 1));
            }
        }

        public int? MapEditor
        {
            get
            {
                return this.mResponse.mMapEditor;
            }
            set
            {
                this.mResponse.mMapEditor = value;
            }
        }

        public string Message
        {
            get
            {
                return this.mResponse.mMessage;
            }
        }

        public int? OnVacation
        {
            get
            {
                return this.mResponse.mOnVacation;
            }
            set
            {
                this.mResponse.mOnVacation = value;
            }
        }

        public List<string> ParishNameHistory
        {
            get
            {
                if (this.mParishNameHistory == null)
                {
                    this.mParishNameHistory = new List<string>();
                    if (this.mResponse.mParishNameHistory != null)
                    {
                        try
                        {
                            Hashtable mParishNameHistory = (Hashtable) this.mResponse.mParishNameHistory;
                            foreach (object obj2 in mParishNameHistory.Keys)
                            {
                                this.mParishNameHistory.Add((string) mParishNameHistory[obj2]);
                            }
                        }
                        catch (Exception)
                        {
                            this.mParishNameHistory = new List<string>();
                        }
                    }
                }
                return this.mParishNameHistory;
            }
        }

        public Dictionary<int, string> ParishNames
        {
            get
            {
                if (this.mParishNames == null)
                {
                    this.mParishNames = new Dictionary<int, string>();
                    if (this.mResponse.mRawParishNames != null)
                    {
                        try
                        {
                            Hashtable mRawParishNames = (Hashtable) this.mResponse.mRawParishNames;
                            foreach (object obj2 in mRawParishNames.Keys)
                            {
                                this.mParishNames.Add((int) obj2, (string) mRawParishNames[obj2]);
                            }
                        }
                        catch (Exception)
                        {
                            this.mParishNames = new Dictionary<int, string>();
                        }
                    }
                }
                return this.mParishNames;
            }
        }

        public string Password
        {
            get
            {
                return this.mResponse.mPassword;
            }
        }

        public string Platform
        {
            get
            {
                return this.mResponse.mPlatform;
            }
        }

        public int? Points
        {
            get
            {
                return this.mResponse.mPoints;
            }
            set
            {
                this.mResponse.mPoints = value;
            }
        }

        public int? PremiumBox
        {
            get
            {
                return this.mResponse.mPremiumBox;
            }
        }

        public int? PremiumCards
        {
            get
            {
                return this.mResponse.mPremiumCards;
            }
        }

        public List<ProductInfo> ProductList
        {
            get
            {
                if (this.mProducts == null)
                {
                    this.mProducts = new List<ProductInfo>();
                    if (this.mResponse.mRawProducts != null)
                    {
                        try
                        {
                            CultureInfo invariantCulture = CultureInfo.InvariantCulture;
                            foreach (object obj2 in (object[]) this.RawProducts)
                            {
                                Hashtable hashtable = (Hashtable) obj2;
                                ProductInfo item = new ProductInfo {
                                    Cost = double.Parse(hashtable["cost"].ToString(), invariantCulture.NumberFormat),
                                    Crowns = int.Parse(hashtable["crowns"].ToString()),
                                    Currency = hashtable["currencycode"].ToString(),
                                    ProductID = int.Parse(hashtable["productid"].ToString()),
                                    Strikethrough = int.Parse(hashtable["strikethrough"].ToString())
                                };
                                this.mProducts.Add(item);
                            }
                        }
                        catch (Exception)
                        {
                            this.mProducts = new List<ProductInfo>();
                        }
                    }
                }
                return this.mProducts;
            }
        }

        public object Products
        {
            get
            {
                return this.mResponse.mRawProducts;
            }
            set
            {
                this.mResponse.mRawProducts = value;
            }
        }

        public object RawCardPacks
        {
            get
            {
                return this.mResponse.mRawPacks;
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

        public object RawParishNameHistory
        {
            get
            {
                return this.mResponse.mParishNameHistory;
            }
            set
            {
                this.mResponse.mParishNameHistory = value;
            }
        }

        public object RawParishNames
        {
            get
            {
                return this.mResponse.mRawParishNames;
            }
            set
            {
                this.mResponse.mRawParishNames = value;
            }
        }

        public object RawProducts
        {
            get
            {
                return this.mResponse.mRawProducts;
            }
        }

        public object RawSharers
        {
            get
            {
                return this.mResponse.mRawSharers;
            }
        }

        public object RawShields
        {
            get
            {
                return this.mResponse.mRawShields;
            }
        }

        public object RawTokens
        {
            get
            {
                return this.mResponse.mRawTokens;
            }
        }

        public object RawWorlds
        {
            get
            {
                return this.mResponse.mRawWorlds;
            }
        }

        public int? RequiresOptInCheck
        {
            get
            {
                return this.mResponse.mRequiresOptInCheck;
            }
            set
            {
                this.mResponse.mRequiresOptInCheck = value;
            }
        }

        public XmlRpcRespStruct Response
        {
            get
            {
                return this.mResponse;
            }
        }

        public string SessionID
        {
            get
            {
                return this.mResponse.mSessionID;
            }
        }

        public Dictionary<string, string> Shields
        {
            get
            {
                if (this.mShields == null)
                {
                    this.mShields = new Dictionary<string, string>();
                    if (this.mResponse.mRawShields != null)
                    {
                        try
                        {
                            Hashtable mRawShields = (Hashtable) this.mResponse.mRawShields;
                            foreach (object obj2 in mRawShields.Keys)
                            {
                                this.mShields.Add((string) obj2, (string) mRawShields[obj2]);
                            }
                        }
                        catch (Exception)
                        {
                            this.mShields = new Dictionary<string, string>();
                        }
                    }
                }
                return this.mShields;
            }
        }

        public string SpecialURL
        {
            get
            {
                return this.mResponse.mSpecialURL;
            }
        }

        public int? SuccessCode
        {
            get
            {
                return this.mResponse.mSuccessCode;
            }
        }

        public Dictionary<int, CardTypes.PremiumToken> Tokens
        {
            get
            {
                if (this.mTokens == null)
                {
                    this.mTokens = new Dictionary<int, CardTypes.PremiumToken>();
                    if (this.mResponse.mRawTokens != null)
                    {
                        try
                        {
                            int result = 0;
                            Hashtable rawTokens = (Hashtable) this.RawTokens;
                            foreach (string str in rawTokens.Keys)
                            {
                                if (int.TryParse(str, out result))
                                {
                                    CardTypes.PremiumToken token = new CardTypes.PremiumToken();
                                    string cardString = (string) ((XmlRpcStruct) rawTokens[str])["premiumtype"];
                                    token.Type = CardTypes.getCardTypeFromString(cardString);
                                    token.Reward = int.Parse((string) ((XmlRpcStruct) rawTokens[str])["reward"]);
                                    token.WorldID = int.Parse((string) ((XmlRpcStruct) rawTokens[str])["kingdomsworldid"]);
                                    token.UserPremiumTokenID = int.Parse((string) ((XmlRpcStruct) rawTokens[str])["userpremiumtokenid"]);
                                    this.mTokens.Add(result, token);
                                }
                            }
                        }
                        catch
                        {
                            this.mTokens = new Dictionary<int, CardTypes.PremiumToken>();
                            return this.mTokens;
                        }
                    }
                }
                return this.mTokens;
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

        public string Username
        {
            get
            {
                return this.mResponse.mUsername;
            }
        }

        public int? VacationPossible
        {
            get
            {
                return this.mResponse.mVacationPossible;
            }
            set
            {
                this.mResponse.mVacationPossible = value;
            }
        }

        public int? VacationSecondsLeft
        {
            get
            {
                return this.mResponse.mVacationSecondsLeft;
            }
            set
            {
                this.mResponse.mVacationSecondsLeft = value;
            }
        }

        public int? VacationSecondsToCancel
        {
            get
            {
                return this.mResponse.mVacationSecondsToCancel;
            }
            set
            {
                this.mResponse.mVacationSecondsToCancel = value;
            }
        }

        public int? VacationsTaken
        {
            get
            {
                return this.mResponse.mVacationsTaken;
            }
            set
            {
                this.mResponse.mVacationsTaken = value;
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

        public List<WorldInfo> WorldList
        {
            get
            {
                if (this.mWorlds == null)
                {
                    this.mWorlds = new List<WorldInfo>();
                    if (this.mResponse.mRawWorlds != null)
                    {
                        try
                        {
                            foreach (object obj2 in (object[]) this.RawWorlds)
                            {
                                Hashtable hashtable = (Hashtable) obj2;
                                WorldInfo item = new WorldInfo {
                                    AvailableToJoin = int.Parse(hashtable["AvailableToJoin"].ToString()) == 1,
                                    KingdomsWorldID = int.Parse(hashtable["KingdomsWorldID"].ToString()),
                                    MapCulture = hashtable["mapculture"].ToString(),
                                    NewWorld = int.Parse(hashtable["NewWorld"].ToString()) == 1,
                                    Online = int.Parse(hashtable["Online"].ToString()) == 1,
                                    ShortDesc = hashtable["ShortDesc"].ToString(),
                                    Supportculture = hashtable["supportculture"].ToString(),
                                    WorldName = hashtable["WorldName"].ToString(),
                                    Playing = hashtable["UserID"].ToString() != "0"
                                };
                                if (hashtable["host"] != null)
                                {
                                    item.Host = hashtable["host"].ToString();
                                    item.HostExt = hashtable["hostext"].ToString();
                                    item.HostPath = hashtable["hostpath"].ToString();
                                    item.HostPort = hashtable["hostport"].ToString();
                                    item.HostProtocol = hashtable["hostprotocol"].ToString();
                                }
                                this.mWorlds.Add(item);
                            }
                        }
                        catch (Exception)
                        {
                            this.mWorlds = new List<WorldInfo>();
                        }
                    }
                }
                this.mWorlds.Sort(this.worldsComparer);
                return this.mWorlds;
            }
        }

        public class WorldsComparer : IComparer<WorldInfo>
        {
            public int Compare(WorldInfo x, WorldInfo y)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                return x.KingdomsWorldID.CompareTo(y.KingdomsWorldID);
            }
        }
    }
}

