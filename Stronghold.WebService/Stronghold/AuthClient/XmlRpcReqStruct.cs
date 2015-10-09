namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcReqStruct
    {
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
        [XmlRpcMember("adminguid")]
        public string mAdmin;
        [XmlRpcMember("playing")]
        public int? mPlaying;
        [XmlRpcMember("worldid")]
        public int? mWorldID;
        [XmlRpcMember("firstworld")]
        public int? mFirstWorld;
        [XmlRpcMember("orderid")]
        public string mOrderID;
        [XmlRpcMember("steamid")]
        public string mSteamID;
        [XmlRpcMember("culture")]
        public string mCulture;
        [XmlRpcMember("countrycode")]
        public string mCountryCode;
        [XmlRpcMember("currency")]
        public string mCurrency;
        [XmlRpcMember("itemid")]
        public string mItemID;
        [XmlRpcMember("aeriatoken")]
        public string mAeriaToken;
        [XmlRpcMember("parishid")]
        public int? mParishId;
        [XmlRpcMember("parishname")]
        public string mParishName;
    }
}

