namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcRespStruct_Cards
    {
        [XmlRpcMember("message")]
        public string mMessage;
        [XmlRpcMember("success")]
        public int? mSuccessCode;
        [XmlRpcMember("UserGUID")]
        public string mUserGUID;
        [XmlRpcMember("cards")]
        public object mRawCards;
        [XmlRpcMember("CardID")]
        public int? mCardID;
        [XmlRpcMember("crowns")]
        public int? mCrowns;
        [XmlRpcMember("cardpoints")]
        public int? mCardpoints;
        [XmlRpcMember("premiumcards")]
        public int? mPremiumCards;
        [XmlRpcMember("count")]
        public int? mCountOffers;
        [XmlRpcMember("rawoffers")]
        public object mRawOffers;
        [XmlRpcMember("numcrowns")]
        public int? mNumcrowns;
        [XmlRpcMember("seconds")]
        public int? mSeconds;
        [XmlRpcMember("strings")]
        public string mStrings;
        [XmlRpcMember("symbols")]
        public string mSymbols;
        [XmlRpcMember("newpoints")]
        public int? mNewpoints;
        [XmlRpcMember("friends")]
        public int? mFriends;
        [XmlRpcMember("rawpacks")]
        public object mRawPacks;
        [XmlRpcMember("lv1")]
        public int? mVeteranLv1;
        [XmlRpcMember("lv2")]
        public int? mVeteranLv2;
        [XmlRpcMember("lv3")]
        public int? mVeteranLv3;
        [XmlRpcMember("lv4")]
        public int? mVeteranLv4;
        [XmlRpcMember("lv5")]
        public int? mVeteranLv5;
        [XmlRpcMember("lv6")]
        public int? mVeteranLv6;
        [XmlRpcMember("lv7")]
        public int? mVeteranLv7;
        [XmlRpcMember("lv8")]
        public int? mVeteranLv8;
        [XmlRpcMember("lv9")]
        public int? mVeteranLv9;
        [XmlRpcMember("lv10")]
        public int? mVeteranLv10;
        [XmlRpcMember("totalseconds")]
        public int? mVeteranTotalSeconds;
        [XmlRpcMember("secondsleft")]
        public int? mVeteranSecondsLeft;
        [XmlRpcMember("currentlevel")]
        public int? mVeteranCurrentLevel;
    }
}

