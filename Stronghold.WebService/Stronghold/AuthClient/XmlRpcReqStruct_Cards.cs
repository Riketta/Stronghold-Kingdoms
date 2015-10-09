namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcReqStruct_Cards
    {
        [XmlRpcMember("UserGUID")]
        public string mUserGUID;
        [XmlRpcMember("SessionGUID")]
        public string mSessionGUID;
        [XmlRpcMember("UserCardID")]
        public string mUserCardID;
        [XmlRpcMember("VillageID")]
        public string mVillageID;
        [XmlRpcMember("KingdomsWorldID")]
        public string mWorldID;
        [XmlRpcMember("OfferID")]
        public string mOfferID;
        [XmlRpcMember("Rank")]
        public string mRank;
        [XmlRpcMember("PackID")]
        public string mPackID;
        [XmlRpcMember("CardString")]
        public string mCardString;
        [XmlRpcMember("CardPoints")]
        public int? mCardPoints;
        [XmlRpcMember("VeteranRank")]
        public int? mVeteranRank;
        [XmlRpcMember("VeteranFaction")]
        public int? mVeteranFaction;
        [XmlRpcMember("VeteranDonated")]
        public int? mVeteranDonated;
        [XmlRpcMember("Multiple")]
        public int? mMultiple;
        [XmlRpcMember("chargeDesc")]
        public string mChargeDesc;
        [XmlRpcMember("numPacks")]
        public int? mNumPacks;
        [XmlRpcMember("vacationlength")]
        public int? mVacationLength;
    }
}

