namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcRespStruct
    {
        [XmlRpcMember("message")]
        public string mMessage;
        [XmlRpcMember("successcode")]
        public int? mSuccessCode;
        [XmlRpcMember("userguid")]
        public string mUserGUID;
        [XmlRpcMember("username")]
        public string mUsername;
        [XmlRpcMember("emailaddress")]
        public string mEmailAddress;
        [XmlRpcMember("password")]
        public string mPassword;
        [XmlRpcMember("sessionid")]
        public string mSessionID;
        [XmlRpcMember("ipaddress")]
        public string mIPAddress;
        [XmlRpcMember("platform")]
        public string mPlatform;
        [XmlRpcMember("rawcards")]
        public object mRawCards;
        [XmlRpcMember("rawtokens")]
        public object mRawTokens;
        [XmlRpcMember("crowns")]
        public int? mCrowns;
        [XmlRpcMember("cardpoints")]
        public int? mCardpoints;
        [XmlRpcMember("premiumcards")]
        public int? mPremiumCards;
        [XmlRpcMember("rawoffers")]
        public object mRawOffers;
        [XmlRpcMember("rawpacks")]
        public object mRawPacks;
        [XmlRpcMember("sharers")]
        public object mRawSharers;
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
        [XmlRpcMember("premiumbox")]
        public int? mPremiumBox;
        [XmlRpcMember("shields")]
        public object mRawShields;
        [XmlRpcMember("worlds")]
        public object mRawWorlds;
        [XmlRpcMember("products")]
        public object mRawProducts;
        [XmlRpcMember("specialurl")]
        public string mSpecialURL;
        [XmlRpcMember("friends")]
        public int? mFriends;
        [XmlRpcMember("isbigpoint")]
        public int? mIsbigpoint;
        [XmlRpcMember("unviewedoffers")]
        public int? mUnviewedOffers;
        [XmlRpcMember("points")]
        public int? mPoints;
        [XmlRpcMember("rawparishnames")]
        public object mRawParishNames;
        [XmlRpcMember("mapeditor")]
        public int? mMapEditor;
        [XmlRpcMember("parishnamehistory")]
        public object mParishNameHistory;
        [XmlRpcMember("onvacation")]
        public int? mOnVacation;
        [XmlRpcMember("vacationstaken")]
        public int? mVacationsTaken;
        [XmlRpcMember("cancancelvacation")]
        public int? mCancelVacation;
        [XmlRpcMember("vacationsecondsleft")]
        public int? mVacationSecondsLeft;
        [XmlRpcMember("vacationsecondstocancel")]
        public int? mVacationSecondsToCancel;
        [XmlRpcMember("vacationpossible")]
        public int? mVacationPossible;
        [XmlRpcMember("requiresoptincheck")]
        public int? mRequiresOptInCheck;
        [XmlRpcMember("facebookfreepack")]
        public int? mFBFreePack;
    }
}

