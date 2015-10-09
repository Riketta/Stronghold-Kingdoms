namespace Kingdoms
{
    using CommonTypes;
    using CustomSinks;
    using DXGraphics;
    using ServerInterface;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Runtime.Remoting.Channels;
    using System.Runtime.Remoting.Channels.Http;
    using System.Runtime.Remoting.Messaging;

    public class RemoteServices
    {
        private AsyncCallback AchievementProgress_Callback;
        private AchievementProgress_UserCallBack achievementProgress_UserCallBack;
        private List<int> achievements = new List<int>();
        private AsyncCallback AddCastleElement_Callback;
        private AddCastleElement_UserCallBack addCastleElement_UserCallBack;
        private AsyncCallback AddUserToFavourites_Callback;
        private AddUserToFavourites_UserCallBack addUserToFavourites_UserCallBack;
        private bool admin;
        private AsyncCallback ArmyAttack_Callback;
        private ArmyAttack_UserCallBack armyAttack_UserCallBack;
        private AsyncCallback AutoRepairCastle_Callback;
        private AutoRepairCastle_UserCallBack autoRepairCastle_UserCallBack;
        private bool boxUser;
        private AsyncCallback BreakLiegeLord_Callback;
        private BreakLiegeLord_UserCallBack breakLiegeLord_UserCallBack;
        private AsyncCallback BreakVassalage_Callback;
        private BreakVassalage_UserCallBack breakVassalage_UserCallBack;
        private AsyncCallback BuyResearchPoint_Callback;
        private BuyResearchPoint_UserCallBack buyResearchPoint_UserCallBack;
        private AsyncCallback BuyVillage_Callback;
        private BuyVillage_UserCallBack buyVillage_UserCallBack;
        private AsyncCallback CancelCard_Callback;
        private CancelCard_UserCallBack cancelCard_UserCallBack;
        private AsyncCallback CancelCastleAttack_Callback;
        private CancelCastleAttack_UserCallBack cancelCastleAttack_UserCallBack;
        private AsyncCallback CancelDeleteVillageBuilding_Callback;
        private CancelDeleteVillageBuilding_UserCallBack cancelDeleteVillageBuilding_UserCallBack;
        private AsyncCallback CancelInterdiction_Callback;
        private CancelInterdiction_UserCallBack cancelInterdiction_UserCallBack;
        private AsyncCallback ChangeCastleElementAggressiveDefender_Callback;
        private ChangeCastleElementAggressiveDefender_UserCallBack changeCastleElementAggressiveDefender_UserCallBack;
        private AsyncCallback ChangeFactionMotto_Callback;
        private ChangeFactionMotto_UserCallBack changeFactionMotto_UserCallBack;
        private HttpChannel channel;
        private AsyncCallback Chat_Admin_Command_Callback;
        private Chat_Admin_Command_UserCallBack chat_Admin_Command_UserCallBack;
        private AsyncCallback Chat_BackFillParishText_Callback;
        private Chat_BackFillParishText_UserCallBack chat_BackFillParishText_UserCallBack;
        private AsyncCallback Chat_Login_Callback;
        private Chat_Login_UserCallBack chat_Login_UserCallBack;
        private AsyncCallback Chat_Logout_Callback;
        private Chat_Logout_UserCallBack chat_Logout_UserCallBack;
        private AsyncCallback Chat_MarkParishTextRead_Callback;
        private Chat_MarkParishTextRead_UserCallBack chat_MarkParishTextRead_UserCallBack;
        private AsyncCallback Chat_ReceiveParishText_Callback;
        private Chat_ReceiveParishText_UserCallBack chat_ReceiveParishText_UserCallBack;
        private AsyncCallback Chat_ReceiveText_Callback;
        private Chat_ReceiveText_UserCallBack chat_ReceiveText_UserCallBack;
        private AsyncCallback Chat_SendParishText_Callback;
        private Chat_SendParishText_UserCallBack chat_SendParishText_UserCallBack;
        private AsyncCallback Chat_SendText_Callback;
        private Chat_SendText_UserCallBack chat_SendText_UserCallBack;
        private AsyncCallback Chat_SetReceivingState_Callback;
        private Chat_SetReceivingState_UserCallBack chat_SetReceivingState_UserCallBack;
        private bool chatActive;
        private AsyncCallback CheatAddTroops_Callback;
        private CheatAddTroops_UserCallBack cheatAddTroops_UserCallBack;
        private AsyncCallback CheckQuestObjectiveComplete_Callback;
        private CheckQuestObjectiveComplete_UserCallBack checkQuestObjectiveComplete_UserCallBack;
        private CommonData_UserCallBack commonData_UserCallBack;
        private AsyncCallback CompleteAbandonNewQuest_Callback;
        private CompleteAbandonNewQuest_UserCallBack completeAbandonNewQuest_UserCallBack;
        private AsyncCallback CompleteQuest_Callback;
        private CompleteQuest_UserCallBack completeQuest_UserCallBack;
        private AsyncCallback CompleteVillageCastle_Callback;
        private CompleteVillageCastle_UserCallBack completeVillageCastle_UserCallBack;
        private bool connectionErrored;
        private int consecutiveTimeOuts;
        private AsyncCallback ConvertVillage_Callback;
        private ConvertVillage_UserCallBack convertVillage_UserCallBack;
        private AsyncCallback CreateFaction_Callback;
        private CreateFaction_UserCallBack createFaction_UserCallBack;
        private AsyncCallback CreateFactionRelationship_Callback;
        private CreateFactionRelationship_UserCallBack createFactionRelationship_UserCallBack;
        private AsyncCallback CreateForum_Callback;
        private CreateForum_UserCallBack createForum_UserCallBack;
        private AsyncCallback CreateHouseRelationship_Callback;
        private CreateHouseRelationship_UserCallBack createHouseRelationship_UserCallBack;
        private AsyncCallback CreateMailFolder_Callback;
        private CreateMailFolder_UserCallBack createMailFolder_UserCallBack;
        private AsyncCallback CreateNewUser_Callback;
        private CreateNewUser_UserCallBack createNewUser_UserCallBack;
        private AsyncCallback CreateUserRelationship_Callback;
        private CreateUserRelationship_UserCallBack createUserRelationship_UserCallBack;
        private AsyncCallback DeleteCastleElement_Callback;
        private DeleteCastleElement_UserCallBack deleteCastleElement_UserCallBack;
        private AsyncCallback DeleteForum_Callback;
        private DeleteForum_UserCallBack deleteForum_UserCallBack;
        private AsyncCallback DeleteForumPost_Callback;
        private DeleteForumPost_UserCallBack deleteForumPost_UserCallBack;
        private AsyncCallback DeleteForumThread_Callback;
        private DeleteForumThread_UserCallBack deleteForumThread_UserCallBack;
        private AsyncCallback DeleteMailThread_Callback;
        private DeleteMailThread_UserCallBack deleteMailThread_UserCallBack;
        private AsyncCallback DeleteReports_Callback;
        private DeleteReports_UserCallBack deleteReports_UserCallBack;
        private AsyncCallback DeleteVillageBuilding_Callback;
        private DeleteVillageBuilding_UserCallBack deleteVillageBuilding_UserCallBack;
        private AsyncCallback DisbandFaction_Callback;
        private DisbandFaction_UserCallBack disbandFaction_UserCallBack;
        private AsyncCallback DisbandPeople_Callback;
        private DisbandPeople_UserCallBack disbandPeople_UserCallBack;
        private AsyncCallback DisbandTroops_Callback;
        private DisbandTroops_UserCallBack disbandTroops_UserCallBack;
        private AsyncCallback DonateCapitalGoods_Callback;
        private DonateCapitalGoods_UserCallBack donateCapitalGoods_UserCallBack;
        private AsyncCallback DoResearch_Callback;
        private DoResearch_UserCallBack doResearch_UserCallBack;
        private AsyncCallback FactionApplication_Callback;
        private FactionApplication_UserCallBack factionApplication_UserCallBack;
        private AsyncCallback FactionApplicationProcessing_Callback;
        private FactionApplicationProcessing_UserCallBack factionApplicationProcessing_UserCallBack;
        private AsyncCallback FactionChangeMemberStatus_Callback;
        private FactionChangeMemberStatus_UserCallBack factionChangeMemberStatus_UserCallBack;
        private AsyncCallback FactionLeadershipVote_Callback;
        private FactionLeadershipVote_UserCallBack factionLeadershipVote_UserCallBack;
        private AsyncCallback FactionLeave_Callback;
        private FactionLeave_UserCallBack factionLeave_UserCallBack;
        private AsyncCallback FactionReplyToInvite_Callback;
        private FactionReplyToInvite_UserCallBack factionReplyToInvite_UserCallBack;
        private AsyncCallback FactionSendInvite_Callback;
        private FactionSendInvite_UserCallBack factionSendInvite_UserCallBack;
        private AsyncCallback FactionWithdrawInvite_Callback;
        private FactionWithdrawInvite_UserCallBack factionWithdrawInvite_UserCallBack;
        private AsyncCallback FlagMailRead_Callback;
        private FlagMailRead_UserCallBack flagMailRead_UserCallBack;
        private AsyncCallback FlagQuestObjectiveComplete_Callback;
        private FlagQuestObjectiveComplete_UserCallBack flagQuestObjectiveComplete_UserCallBack;
        private AsyncCallback ForwardReport_Callback;
        private ForwardReport_UserCallBack forwardReport_UserCallBack;
        private AsyncCallback FullTick_Callback;
        private FullTick_UserCallBack fullTick_UserCallBack;
        private AsyncCallback GetActivePeople_Callback;
        private GetActivePeople_UserCallBack getActivePeople_UserCallBack;
        private AsyncCallback GetActiveTraders_Callback;
        private GetActiveTraders_UserCallBack getActiveTraders_UserCallBack;
        private AsyncCallback GetAdminStats_Callback;
        private GetAdminStats_UserCallBack getAdminStats_UserCallBack;
        private AsyncCallback GetAllVillageOwnerFactions_Callback;
        private int GetAllVillageOwnerFactions_Index;
        private GetAllVillageOwnerFactions_UserCallBack getAllVillageOwnerFactions_UserCallBack;
        public bool GetAllVillageOwnerFactions_ValidDownload;
        private AsyncCallback GetAreaFactionChanges_Callback;
        private GetAreaFactionChanges_UserCallBack getAreaFactionChanges_UserCallBack;
        private AsyncCallback GetArmyData_Callback;
        private GetArmyData_UserCallBack getArmyData_UserCallBack;
        private AsyncCallback GetBattleHonourRating_Callback;
        private GetBattleHonourRating_UserCallBack getBattleHonourRating_UserCallBack;
        private AsyncCallback GetCapitalBarracksSpace_Callback;
        private GetCapitalBarracksSpace_UserCallBack getCapitalBarracksSpace_UserCallBack;
        private AsyncCallback GetCastle_Callback;
        private GetCastle_UserCallBack getCastle_UserCallBack;
        private AsyncCallback GetCountryElectionInfo_Callback;
        private GetCountryElectionInfo_UserCallBack getCountryElectionInfo_UserCallBack;
        private AsyncCallback GetCountryFrontPageInfo_Callback;
        private GetCountryFrontPageInfo_UserCallBack getCountryFrontPageInfo_UserCallBack;
        private AsyncCallback GetCountyElectionInfo_Callback;
        private GetCountyElectionInfo_UserCallBack getCountyElectionInfo_UserCallBack;
        private AsyncCallback GetCountyFrontPageInfo_Callback;
        private GetCountyFrontPageInfo_UserCallBack getCountyFrontPageInfo_UserCallBack;
        private AsyncCallback GetCurrentElectionInfo_Callback;
        private GetCurrentElectionInfo_UserCallBack getCurrentElectionInfo_UserCallBack;
        private AsyncCallback GetExcommunicationStatus_Callback;
        private GetExcommunicationStatus_UserCallBack getExcommunicationStatus_UserCallBack;
        private AsyncCallback GetFactionData_Callback;
        private GetFactionData_UserCallBack getFactionData_UserCallBack;
        private AsyncCallback GetForumList_Callback;
        private GetForumList_UserCallBack getForumList_UserCallBack;
        private AsyncCallback GetForumThread_Callback;
        private GetForumThread_UserCallBack getForumThread_UserCallBack;
        private AsyncCallback GetForumThreadList_Callback;
        private GetForumThreadList_UserCallBack getForumThreadList_UserCallBack;
        private AsyncCallback GetHistoricalData_Callback;
        private GetHistoricalData_UserCallBack getHistoricalData_UserCallBack;
        private AsyncCallback GetHouseGloryPoints_Callback;
        private GetHouseGloryPoints_UserCallBack getHouseGloryPoints_UserCallBack;
        private AsyncCallback GetIngameMessage_Callback;
        private GetIngameMessage_UserCallBack getIngameMessage_UserCallBack;
        private AsyncCallback GetInvasionInfo_Callback;
        private GetInvasionInfo_UserCallBack getInvasionInfo_UserCallBack;
        private AsyncCallback GetLastAttacker_Callback;
        private GetLastAttacker_UserCallBack getLastAttacker_UserCallBack;
        private AsyncCallback GetLoginHistory_Callback;
        private GetLoginHistory_UserCallBack getLoginHistory_UserCallBack;
        private AsyncCallback GetMailFolders_Callback;
        private GetMailFolders_UserCallBack getMailFolders_UserCallBack;
        private AsyncCallback GetMailRecipientsHistory_Callback;
        private GetMailRecipientsHistory_UserCallBack getMailRecipientsHistory_UserCallBack;
        private AsyncCallback GetMailThread_Callback;
        private GetMailThread_UserCallBack getMailThread_UserCallBack;
        private AsyncCallback GetMailThreadList_Callback;
        private GetMailThreadList_UserCallBack getMailThreadList_UserCallBack;
        private AsyncCallback GetMailUserSearch_Callback;
        private GetMailUserSearch_UserCallBack getMailUserSearch_UserCallBack;
        private AsyncCallback GetOtherUserVillageIDList_Callback;
        private GetOtherUserVillageIDList_UserCallBack getOtherUserVillageIDList_UserCallBack;
        private AsyncCallback GetParishFrontPageInfo_Callback;
        private GetParishFrontPageInfo_UserCallBack getParishFrontPageInfo_UserCallBack;
        private AsyncCallback GetParishMembersList_Callback;
        private GetParishMembersList_UserCallBack getParishMembersList_UserCallBack;
        private AsyncCallback GetPreVassalInfo_Callback;
        private GetPreVassalInfo_UserCallBack getPreVassalInfo_UserCallBack;
        private AsyncCallback GetProvinceElectionInfo_Callback;
        private GetProvinceElectionInfo_UserCallBack getProvinceElectionInfo_UserCallBack;
        private AsyncCallback GetProvinceFrontPageInfo_Callback;
        private GetProvinceFrontPageInfo_UserCallBack getProvinceFrontPageInfo_UserCallBack;
        private AsyncCallback GetQuestData_Callback;
        private GetQuestData_UserCallBack getQuestData_UserCallBack;
        private AsyncCallback GetQuestStatus_Callback;
        private GetQuestStatus_UserCallBack getQuestStatus_UserCallBack;
        private AsyncCallback GetReport_Callback;
        private GetReport_UserCallBack getReport_UserCallBack;
        private AsyncCallback GetReportsList_Callback;
        private GetReportsList_UserCallBack getReportsList_UserCallBack;
        private AsyncCallback GetResearchData_Callback;
        private GetResearchData_UserCallBack getResearchData_UserCallBack;
        private AsyncCallback GetResourceLevel_Callback;
        private GetResourceLevel_UserCallBack getResourceLevel_UserCallBack;
        private AsyncCallback GetStockExchangeData_Callback;
        private GetStockExchangeData_UserCallBack getStockExchangeData_UserCallBack;
        private AsyncCallback GetUserIDFromName_Callback;
        private GetUserIDFromName_UserCallBack getUserIDFromName_UserCallBack;
        private AsyncCallback GetUserPeople_Callback;
        private GetUserPeople_UserCallBack getUserPeople_UserCallBack;
        private AsyncCallback GetUserTraders_Callback;
        private GetUserTraders_UserCallBack getUserTraders_UserCallBack;
        private AsyncCallback GetUserVillages_Callback;
        private GetUserVillages_UserCallBack getUserVillages_UserCallBack;
        private AsyncCallback GetVassalArmyInfo_Callback;
        private GetVassalArmyInfo_UserCallBack getVassalArmyInfo_UserCallBack;
        private AsyncCallback GetViewFactionData_Callback;
        private GetViewFactionData_UserCallBack getViewFactionData_UserCallBack;
        private AsyncCallback GetViewHouseData_Callback;
        private GetViewHouseData_UserCallBack getViewHouseData_UserCallBack;
        private AsyncCallback GetVillageBuildingsList_Callback;
        private GetVillageBuildingsList_UserCallBack getVillageBuildingsList_UserCallBack;
        private AsyncCallback GetVillageFactionChanges_Callback;
        private int GetVillageFactionChanges_Index;
        private GetVillageFactionChanges_UserCallBack getVillageFactionChanges_UserCallBack;
        public bool GetVillageFactionChanges_ValidDownload;
        private AsyncCallback GetVillageInfoForDonateCapitalGoods_Callback;
        private GetVillageInfoForDonateCapitalGoods_UserCallBack getVillageInfoForDonateCapitalGoods_UserCallBack;
        private AsyncCallback GetVillageNames_Callback;
        private int GetVillageNames_Index;
        private GetVillageNames_UserCallBack getVillageNames_UserCallBack;
        public bool GetVillageNames_ValidDownload;
        private AsyncCallback GetVillageRankTaxTree_Callback;
        private GetVillageRankTaxTree_UserCallBack getVillageRankTaxTree_UserCallBack;
        private AsyncCallback GetVillageStartLocations_Callback;
        private GetVillageStartLocations_UserCallBack getVillageStartLocations_UserCallBack;
        private AsyncCallback GiveForumAccess_Callback;
        private GiveForumAccess_UserCallBack giveForumAccess_UserCallBack;
        private AsyncCallback HandleVassalRequest_Callback;
        private HandleVassalRequest_UserCallBack handleVassalRequest_UserCallBack;
        private AsyncCallback HouseVote_Callback;
        private HouseVote_UserCallBack houseVote_UserCallBack;
        private AsyncCallback HouseVoteHouseLeader_Callback;
        private HouseVoteHouseLeader_UserCallBack houseVoteHouseLeader_UserCallBack;
        private AsyncCallback InitialiseFreeCards_Callback;
        private InitialiseFreeCards_UserCallBack initialiseFreeCards_UserCallBack;
        private bool inResultsProcessing;
        private static readonly RemoteServices instance = new RemoteServices();
        public int lastLatency;
        private AsyncCallback LaunchCastleAttack_Callback;
        private LaunchCastleAttack_UserCallBack launchCastleAttack_UserCallBack;
        private AsyncCallback LeaderBoard_Callback;
        private LeaderBoard_UserCallBack leaderBoard_UserCallBack;
        private AsyncCallback LeaderBoardSearch_Callback;
        private LeaderBoardSearch_UserCallBack leaderBoardSearch_UserCallBack;
        private AsyncCallback LeaveHouse_Callback;
        private LeaveHouse_UserCallBack leaveHouse_UserCallBack;
        private LoginLeadersInfo loginLeaderInfo;
        private AsyncCallback LoginUser_Callback;
        private LoginUser_UserCallBack loginUser_UserCallBack;
        private AsyncCallback LoginUserGuid_Callback;
        private LoginUserGuid_UserCallBack loginUserGuid_UserCallBack;
        private AsyncCallback LogOut_Callback;
        private LogOut_UserCallBack logOut_UserCallBack;
        private AsyncCallback MakeCountryVote_Callback;
        private MakeCountryVote_UserCallBack makeCountryVote_UserCallBack;
        private AsyncCallback MakeCountyVote_Callback;
        private MakeCountyVote_UserCallBack makeCountyVote_UserCallBack;
        private AsyncCallback MakeParishVote_Callback;
        private MakeParishVote_UserCallBack makeParishVote_UserCallBack;
        private AsyncCallback MakePeople_Callback;
        private MakePeople_UserCallBack makePeople_UserCallBack;
        private AsyncCallback MakeProvinceVote_Callback;
        private MakeProvinceVote_UserCallBack makeProvinceVote_UserCallBack;
        private AsyncCallback MakeTroop_Callback;
        private MakeTroop_UserCallBack makeTroop_UserCallBack;
        private AsyncCallback ManageReportFolders_Callback;
        private ManageReportFolders_UserCallBack manageReportFolders_UserCallBack;
        private bool mapEditor;
        private AsyncCallback MemorizeCastleTroops_Callback;
        private MemorizeCastleTroops_UserCallBack memorizeCastleTroops_UserCallBack;
        private bool moderator;
        private AsyncCallback MoveToMailFolder_Callback;
        private MoveToMailFolder_UserCallBack moveToMailFolder_UserCallBack;
        private AsyncCallback MoveVillageBuilding_Callback;
        private MoveVillageBuilding_UserCallBack moveVillageBuilding_UserCallBack;
        private AsyncCallback NewForumThread_Callback;
        private NewForumThread_UserCallBack newForumThread_UserCallBack;
        private AsyncCallback ParishWallDetailInfo_Callback;
        private ParishWallDetailInfo_UserCallBack parishWallDetailInfo_UserCallBack;
        private AsyncCallback PlaceVillageBuilding_Callback;
        private PlaceVillageBuilding_UserCallBack placeVillageBuilding_UserCallBack;
        private AsyncCallback PostToForumThread_Callback;
        private PostToForumThread_UserCallBack postToForumThread_UserCallBack;
        private AsyncCallback PreAttackSetup_Callback;
        private PreAttackSetup_UserCallBack preAttackSetup_UserCallBack;
        private AsyncCallback PremiumOverview_Callback;
        private PremiumOverview_UserCallBack premiumOverview_UserCallBack;
        private AsyncCallback PreValidateCardToBePlayed_Callback;
        private PreValidateCardToBePlayed_UserCallBack preValidateCardToBePlayed_UserCallBack;
        private int profileWorldID = -1;
        private ArrayList queuedResultList = new ArrayList();
        private string realname = "";
        private AsyncCallback RemoveMailFolder_Callback;
        private RemoveMailFolder_UserCallBack removeMailFolder_UserCallBack;
        private ReportFilterList reportFilters;
        private AsyncCallback ReportMail_Callback;
        private ReportMail_UserCallBack reportMail_UserCallBack;
        private bool requiresVerification;
        private AsyncCallback ResendVerificationEmail_Callback;
        private ResendVerificationEmail_UserCallBack resendVerificationEmail_UserCallBack;
        private AsyncCallback RestoreCastleTroops_Callback;
        private RestoreCastleTroops_UserCallBack restoreCastleTroops_UserCallBack;
        private List<CallBackEntryClass> resultList = new List<CallBackEntryClass>();
        private AsyncCallback RetrieveArmyFromGarrison_Callback;
        private RetrieveArmyFromGarrison_UserCallBack retrieveArmyFromGarrison_UserCallBack;
        private AsyncCallback RetrieveAttackResult_Callback;
        private RetrieveAttackResult_UserCallBack retrieveAttackResult_UserCallBack;
        private AsyncCallback RetrievePeople_Callback;
        private RetrievePeople_UserCallBack retrievePeople_UserCallBack;
        private AsyncCallback RetrieveStats_Callback;
        private RetrieveStats_UserCallBack retrieveStats_UserCallBack;
        private AsyncCallback RetrieveTroopsFromVassal_Callback;
        private RetrieveTroopsFromVassal_UserCallBack retrieveTroopsFromVassal_UserCallBack;
        private AsyncCallback RetrieveVillageUserInfo_Callback;
        private RetrieveVillageUserInfo_UserCallBack retrieveVillageUserInfo_UserCallBack;
        private AsyncCallback ReturnReinforcements_Callback;
        private ReturnReinforcements_UserCallBack returnReinforcements_UserCallBack;
        public List<RTT_Log_data> rtt_logging = new List<RTT_Log_data>();
        public int RTTAverageCount;
        public int RTTAverageLongCount;
        public double RTTAverageLongTime;
        public int RTTAverageShortCount;
        public double RTTAverageShortTime;
        public double RTTAverageTime;
        public int RTTTimeOuts;
        private AsyncCallback SelfJoinHouse_Callback;
        private SelfJoinHouse_UserCallBack selfJoinHouse_UserCallBack;
        private AsyncCallback SendCommands_Callback;
        private SendCommands_UserCallBack sendCommands_UserCallBack;
        private AsyncCallback SendMail_Callback;
        private SendMail_UserCallBack sendMail_UserCallBack;
        private AsyncCallback SendMarketResources_Callback;
        private SendMarketResources_UserCallBack sendMarketResources_UserCallBack;
        private AsyncCallback SendPeople_Callback;
        private SendPeople_UserCallBack sendPeople_UserCallBack;
        private AsyncCallback SendReinforcements_Callback;
        private SendReinforcements_UserCallBack sendReinforcements_UserCallBack;
        private AsyncCallback SendScouts_Callback;
        private SendScouts_UserCallBack sendScouts_UserCallBack;
        private AsyncCallback SendSpecialMail_Callback;
        private SendSpecialMail_UserCallBack sendSpecialMail_UserCallBack;
        private AsyncCallback SendTroopsToCapital_Callback;
        private SendTroopsToCapital_UserCallBack sendTroopsToCapital_UserCallBack;
        private AsyncCallback SendTroopsToVassal_Callback;
        private SendTroopsToVassal_UserCallBack sendTroopsToVassal_UserCallBack;
        private AsyncCallback SendVassalRequest_Callback;
        private SendVassalRequest_UserCallBack sendVassalRequest_UserCallBack;
        private IService service;
        private Guid sessionGuid = Guid.Empty;
        private int sessionID;
        private AsyncCallback SetAdminMessage_Callback;
        private SetAdminMessage_UserCallBack setAdminMessage_UserCallBack;
        private AsyncCallback SetHighestArmySeen_Callback;
        private SetHighestArmySeen_UserCallBack setHighestArmySeen_UserCallBack;
        private AsyncCallback SetStartingCounty_Callback;
        private SetStartingCounty_UserCallBack setStartingCounty_UserCallBack;
        private AsyncCallback SetVacationMode_Callback;
        private SetVacationMode_UserCallBack setVacationMode_UserCallBack;
        private bool show2ndAgeMessage;
        private bool show3rdAgeMessage;
        private bool show4thAgeMessage;
        private bool show5thAgeMessage;
        private bool showAdminMessage;
        private AsyncCallback SpecialVillageInfo_Callback;
        private SpecialVillageInfo_UserCallBack specialVillageInfo_UserCallBack;
        private AsyncCallback SpinTheWheel_Callback;
        private SpinTheWheel_UserCallBack spinTheWheel_UserCallBack;
        private AsyncCallback SpyCommand_Callback;
        private SpyCommand_UserCallBack spyCommand_UserCallBack;
        private AsyncCallback SpyGetArmyInfo_Callback;
        private SpyGetArmyInfo_UserCallBack spyGetArmyInfo_UserCallBack;
        private AsyncCallback SpyGetResearchInfo_Callback;
        private SpyGetResearchInfo_UserCallBack spyGetResearchInfo_UserCallBack;
        private AsyncCallback SpyGetVillageResourceInfo_Callback;
        private SpyGetVillageResourceInfo_UserCallBack spyGetVillageResourceInfo_UserCallBack;
        private AsyncCallback StandDownAsParishDespot_Callback;
        private StandDownAsParishDespot_UserCallBack standDownAsParishDespot_UserCallBack;
        private AsyncCallback StandInElection_Callback;
        private StandInElection_UserCallBack standInElection_UserCallBack;
        private AsyncCallback StartNewQuest_Callback;
        private StartNewQuest_UserCallBack startNewQuest_UserCallBack;
        private AsyncCallback StockExchangeTrade_Callback;
        private StockExchangeTrade_UserCallBack stockExchangeTrade_UserCallBack;
        private static object syncLock = new object();
        private AsyncCallback TestAchievements_Callback;
        private TestAchievements_UserCallBack testAchievements_UserCallBack;
        private AsyncCallback TouchHouseVisitDate_Callback;
        private TouchHouseVisitDate_UserCallBack touchHouseVisitDate_UserCallBack;
        private AsyncCallback TutorialCommand_Callback;
        private TutorialCommand_UserCallBack tutorialCommand_UserCallBack;
        private AsyncCallback UpdateCurrentCards_Callback;
        private UpdateCurrentCards_UserCallBack updateCurrentCards_UserCallBack;
        private AsyncCallback UpdateDiplomacyStatus_Callback;
        private UpdateDiplomacyStatus_UserCallBack updateDiplomacyStatus_UserCallBack;
        private AsyncCallback UpdateReportFilters_Callback;
        private UpdateReportFilters_UserCallBack updateReportFilters_UserCallBack;
        private AsyncCallback UpdateSelectedTitheType_Callback;
        private UpdateSelectedTitheType_UserCallBack updateSelectedTitheType_UserCallBack;
        private AsyncCallback UpdateUserOptions_Callback;
        private UpdateUserOptions_UserCallBack updateUserOptions_UserCallBack;
        private AsyncCallback UpdateVillageFavourites_Callback;
        private UpdateVillageFavourites_UserCallBack updateVillageFavourites_UserCallBack;
        private AsyncCallback UpdateVillageResourcesInfo_Callback;
        private UpdateVillageResourcesInfo_UserCallBack updateVillageResourcesInfo_UserCallBack;
        private AsyncCallback UpgradeRank_Callback;
        private UpgradeRank_UserCallBack upgradeRank_UserCallBack;
        private AsyncCallback UploadAvatar_Callback;
        private UploadAvatar_UserCallBack uploadAvatar_UserCallBack;
        private AvatarData userAvatar = new AvatarData();
        private int userFactionID;
        private Guid userGuid = Guid.Empty;
        private int userID = -1;
        private AsyncCallback UserInfo_Callback;
        private UserInfo_UserCallBack userInfo_UserCallBack;
        private string username = "";
        private GameOptionsData userOptions = new GameOptionsData();
        private AsyncCallback VassalInfo_Callback;
        private VassalInfo_UserCallBack vassalInfo_UserCallBack;
        private AsyncCallback VassalSendResources_Callback;
        private VassalSendResources_UserCallBack vassalSendResources_UserCallBack;
        private AsyncCallback ViewBattle_Callback;
        private ViewBattle_UserCallBack viewBattle_UserCallBack;
        private AsyncCallback ViewCastle_Callback;
        private ViewCastle_UserCallBack viewCastle_UserCallBack;
        private AsyncCallback VillageBuildingChangeRates_Callback;
        private VillageBuildingChangeRates_UserCallBack villageBuildingChangeRates_UserCallBack;
        private AsyncCallback VillageBuildingCompleteDataRetrieval_Callback;
        private VillageBuildingCompleteDataRetrieval_UserCallBack villageBuildingCompleteDataRetrieval_UserCallBack;
        private AsyncCallback VillageBuildingSetActive_Callback;
        private VillageBuildingSetActive_UserCallBack villageBuildingSetActive_UserCallBack;
        private AsyncCallback VillageHoldBanquet_Callback;
        private VillageHoldBanquet_UserCallBack villageHoldBanquet_UserCallBack;
        private AsyncCallback VillageProduceWeapons_Callback;
        private VillageProduceWeapons_UserCallBack villageProduceWeapons_UserCallBack;
        private AsyncCallback VillageRename_Callback;
        private VillageRename_UserCallBack villageRename_UserCallBack;
        private AsyncCallback VoteInElection_Callback;
        private VoteInElection_UserCallBack voteInElection_UserCallBack;
        private Guid worldGUID = Guid.Empty;
        private AsyncCallback WorldInfo_Callback;
        private WorldInfo_UserCallBack worldInfo_UserCallBack;

        private RemoteServices()
        {
        }

        public void AbandonNewQuest(int questID)
        {
            if (this.CompleteAbandonNewQuest_Callback == null)
            {
                this.CompleteAbandonNewQuest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CompleteAbandonNewQuest);
            }
            RemoteAsyncDelegate_CompleteAbandonNewQuest quest = new RemoteAsyncDelegate_CompleteAbandonNewQuest(this.service.CompleteAbandonNewQuest);
            this.registerRPCcall(quest.BeginInvoke(this.UserID, this.SessionID, questID, true, false, -1, this.CompleteAbandonNewQuest_Callback, null), typeof(CompleteAbandonNewQuest_ReturnType));
        }

        public void AcceptVassalRequest(int requesterVillageID, int vassalVillageID)
        {
            if (this.HandleVassalRequest_Callback == null)
            {
                this.HandleVassalRequest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_HandleVassalRequest);
            }
            RemoteAsyncDelegate_HandleVassalRequest request = new RemoteAsyncDelegate_HandleVassalRequest(this.service.HandleVassalRequest);
            this.registerRPCcall(request.BeginInvoke(this.UserID, this.SessionID, 1, requesterVillageID, vassalVillageID, this.HandleVassalRequest_Callback, null), typeof(HandleVassalRequest_ReturnType));
        }

        public void AchievementProgress()
        {
            if (this.AchievementProgress_Callback == null)
            {
                this.AchievementProgress_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_AchievementProgress);
            }
            RemoteAsyncDelegate_AchievementProgress progress = new RemoteAsyncDelegate_AchievementProgress(this.service.AchievementProgress);
            this.registerRPCcall(progress.BeginInvoke(this.UserID, this.SessionID, this.AchievementProgress_Callback, null), typeof(AchievementProgress_ReturnType));
        }

        public void AddCastleElement(int villageID, int elementType, int x, int y, long clientElementNumber)
        {
            this.AddCastleElement(villageID, elementType, x, y, clientElementNumber, false, false, null, null, null);
        }

        public void AddCastleElement(int villageID, int elementType, int x, int y, long clientElementNumber, bool reinforcement)
        {
            this.AddCastleElement(villageID, elementType, x, y, clientElementNumber, reinforcement, false, null, null, null);
        }

        public void AddCastleElement(int villageID, int elementType, int x, int y, long clientElementNumber, bool reinforcement, bool vassalReinforcement, byte[,] elementList, long[] troopsToDelete, MoveElementData[] troopsToMove)
        {
            if (this.AddCastleElement_Callback == null)
            {
                this.AddCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_AddCastleElement);
            }
            RemoteAsyncDelegate_AddCastleElement element = new RemoteAsyncDelegate_AddCastleElement(this.service.AddCastleElement);
            this.registerRPCcall(element.BeginInvoke(this.UserID, this.SessionID, villageID, elementType, x, y, clientElementNumber, -1, -1, reinforcement, vassalReinforcement, elementList, troopsToDelete, troopsToMove, this.AddCastleElement_Callback, null), typeof(AddCastleElement_ReturnType));
        }

        public void AddCastleElementList(int villageID, byte[,] elementList)
        {
            this.AddCastleElement(villageID, 0, 0, 0, -1L, false, false, elementList, null, null);
        }

        public void AddCastleElementList(int villageID, byte[,] elementList, long[] troopsToDelete, MoveElementData[] troopsToMove)
        {
            this.AddCastleElement(villageID, 0, 0, 0, -1L, false, false, elementList, troopsToDelete, troopsToMove);
        }

        public void AddCastleWallElement(int villageID, int elementType, int x, int y, long clientElementNumber, int wallX, int wallY)
        {
            if (this.AddCastleElement_Callback == null)
            {
                this.AddCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_AddCastleElement);
            }
            RemoteAsyncDelegate_AddCastleElement element = new RemoteAsyncDelegate_AddCastleElement(this.service.AddCastleElement);
            this.registerRPCcall(element.BeginInvoke(this.UserID, this.SessionID, villageID, elementType, x, y, clientElementNumber, wallX, wallY, false, false, null, null, null, this.AddCastleElement_Callback, null), typeof(AddCastleElement_ReturnType));
        }

        public void addPacket(Type classType, int time)
        {
            RTT_Log_data item = new RTT_Log_data {
                packetType = classType,
                time = time
            };
            this.rtt_logging.Add(item);
        }

        public void addReportFolder(string folderName)
        {
            if (this.ManageReportFolders_Callback == null)
            {
                this.ManageReportFolders_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ManageReportFolders);
            }
            RemoteAsyncDelegate_ManageReportFolders folders = new RemoteAsyncDelegate_ManageReportFolders(this.service.ManageReportFolders);
            this.registerRPCcall(folders.BeginInvoke(this.UserID, this.SessionID, 1, 0L, folderName, this.ManageReportFolders_Callback, null), typeof(ManageReportFolders_ReturnType));
        }

        public void AddUserToFavourites(string userName)
        {
            if (this.AddUserToFavourites_Callback == null)
            {
                this.AddUserToFavourites_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_AddUserToFavourites);
            }
            RemoteAsyncDelegate_AddUserToFavourites favourites = new RemoteAsyncDelegate_AddUserToFavourites(this.service.AddUserToFavourites);
            this.registerRPCcall(favourites.BeginInvoke(this.UserID, this.SessionID, userName, false, this.AddUserToFavourites_Callback, null), typeof(AddUserToFavourites_ReturnType));
        }

        public void ArmyAttack(int armyID, int targetVillage, int attackType)
        {
            if (this.ArmyAttack_Callback == null)
            {
                this.ArmyAttack_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ArmyAttack);
            }
            RemoteAsyncDelegate_ArmyAttack attack = new RemoteAsyncDelegate_ArmyAttack(this.service.ArmyAttack);
            this.registerRPCcall(attack.BeginInvoke(this.UserID, this.SessionID, armyID, targetVillage, attackType, this.ArmyAttack_Callback, null), typeof(ArmyAttack_ReturnType));
        }

        public void AutoRepairCastle(int villageID)
        {
            if (this.AutoRepairCastle_Callback == null)
            {
                this.AutoRepairCastle_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_AutoRepairCastle);
            }
            RemoteAsyncDelegate_AutoRepairCastle castle = new RemoteAsyncDelegate_AutoRepairCastle(this.service.AutoRepairCastle);
            this.registerRPCcall(castle.BeginInvoke(this.UserID, this.SessionID, villageID, this.AutoRepairCastle_Callback, null), typeof(AutoRepairCastle_ReturnType));
        }

        public void BreakLiegeLord(int villageID, int targetVillage)
        {
            if (this.BreakLiegeLord_Callback == null)
            {
                this.BreakLiegeLord_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_BreakLiegeLord);
            }
            RemoteAsyncDelegate_BreakLiegeLord lord = new RemoteAsyncDelegate_BreakLiegeLord(this.service.BreakLiegeLord);
            this.registerRPCcall(lord.BeginInvoke(this.UserID, this.SessionID, villageID, targetVillage, this.BreakLiegeLord_Callback, null), typeof(BreakLiegeLord_ReturnType));
        }

        public void BreakVassalage(int villageID, int targetVillage)
        {
            if (this.BreakVassalage_Callback == null)
            {
                this.BreakVassalage_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_BreakVassalage);
            }
            RemoteAsyncDelegate_BreakVassalage vassalage = new RemoteAsyncDelegate_BreakVassalage(this.service.BreakVassalage);
            this.registerRPCcall(vassalage.BeginInvoke(this.UserID, this.SessionID, villageID, targetVillage, this.BreakVassalage_Callback, null), typeof(BreakVassalage_ReturnType));
        }

        public void BuyResearchPoint()
        {
            if (this.BuyResearchPoint_Callback == null)
            {
                this.BuyResearchPoint_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_BuyResearchPoint);
            }
            RemoteAsyncDelegate_BuyResearchPoint point = new RemoteAsyncDelegate_BuyResearchPoint(this.service.BuyResearchPoint);
            this.registerRPCcall(point.BeginInvoke(this.UserID, this.SessionID, this.BuyResearchPoint_Callback, null), typeof(BuyResearchPoint_ReturnType));
        }

        public void BuyVillage(int fromVillageID, int villageID, int mapType, long startChangePos, bool peaceTime)
        {
            if (this.BuyVillage_Callback == null)
            {
                this.BuyVillage_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_BuyVillage);
            }
            RemoteAsyncDelegate_BuyVillage village = new RemoteAsyncDelegate_BuyVillage(this.service.BuyVillage);
            this.registerRPCcall(village.BeginInvoke(this.UserID, this.SessionID, fromVillageID, villageID, mapType, startChangePos, peaceTime, this.BuyVillage_Callback, null), typeof(BuyVillage_ReturnType));
        }

        public void CancelCard(int card)
        {
            if (this.CancelCard_Callback == null)
            {
                this.CancelCard_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CancelCard);
            }
            RemoteAsyncDelegate_CancelCard card2 = new RemoteAsyncDelegate_CancelCard(this.service.CancelCard);
            this.registerRPCcall(card2.BeginInvoke(this.UserID, this.SessionID, card, this.CancelCard_Callback, null), typeof(CancelCard_ReturnType));
        }

        public void CancelCastleAttack(long armyID)
        {
            if (this.CancelCastleAttack_Callback == null)
            {
                this.CancelCastleAttack_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CancelCastleAttack);
            }
            RemoteAsyncDelegate_CancelCastleAttack attack = new RemoteAsyncDelegate_CancelCastleAttack(this.service.CancelCastleAttack);
            this.registerRPCcall(attack.BeginInvoke(this.UserID, this.SessionID, armyID, this.CancelCastleAttack_Callback, null), typeof(CancelCastleAttack_ReturnType));
        }

        public void CancelDeleteVillageBuilding(int villageID, long buildingID)
        {
            if (this.CancelDeleteVillageBuilding_Callback == null)
            {
                this.CancelDeleteVillageBuilding_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CancelDeleteVillageBuilding);
            }
            RemoteAsyncDelegate_CancelDeleteVillageBuilding building = new RemoteAsyncDelegate_CancelDeleteVillageBuilding(this.service.CancelDeleteVillageBuilding);
            this.registerRPCcall(building.BeginInvoke(this.UserID, this.SessionID, villageID, buildingID, this.CancelDeleteVillageBuilding_Callback, null), typeof(CancelDeleteVillageBuilding_ReturnType));
        }

        public void CancelInterdiction(int villageID)
        {
            if (this.CancelInterdiction_Callback == null)
            {
                this.CancelInterdiction_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CancelInterdiction);
            }
            RemoteAsyncDelegate_CancelInterdiction interdiction = new RemoteAsyncDelegate_CancelInterdiction(this.service.CancelInterdiction);
            this.registerRPCcall(interdiction.BeginInvoke(this.UserID, this.SessionID, villageID, this.CancelInterdiction_Callback, null), typeof(CancelInterdiction_ReturnType));
        }

        public void CancelQueuedResearch(int researchType, int queuePos)
        {
            if (this.DoResearch_Callback == null)
            {
                this.DoResearch_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DoResearch);
            }
            RemoteAsyncDelegate_DoResearch research = new RemoteAsyncDelegate_DoResearch(this.service.DoResearch);
            this.registerRPCcall(research.BeginInvoke(this.UserID, this.SessionID, researchType, queuePos, this.DoResearch_Callback, null), typeof(DoResearch_ReturnType));
        }

        public void CancelVassalRequest(int requesterVillageID, int vassalVillageID)
        {
            if (this.HandleVassalRequest_Callback == null)
            {
                this.HandleVassalRequest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_HandleVassalRequest);
            }
            RemoteAsyncDelegate_HandleVassalRequest request = new RemoteAsyncDelegate_HandleVassalRequest(this.service.HandleVassalRequest);
            this.registerRPCcall(request.BeginInvoke(this.UserID, this.SessionID, 3, requesterVillageID, vassalVillageID, this.HandleVassalRequest_Callback, null), typeof(HandleVassalRequest_ReturnType));
        }

        public void ChangeCastleElementAggressiveDefender(int villageID, long[] elementID, bool state)
        {
            if (this.ChangeCastleElementAggressiveDefender_Callback == null)
            {
                this.ChangeCastleElementAggressiveDefender_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ChangeCastleElementAggressiveDefender);
            }
            RemoteAsyncDelegate_ChangeCastleElementAggressiveDefender defender = new RemoteAsyncDelegate_ChangeCastleElementAggressiveDefender(this.service.ChangeCastleElementAggressiveDefender);
            this.registerRPCcall(defender.BeginInvoke(this.UserID, this.SessionID, villageID, elementID, state, this.ChangeCastleElementAggressiveDefender_Callback, null), typeof(ChangeCastleElementAggressiveDefender_ReturnType));
        }

        public void ChangeFactionMotto(string factionName, string factionNameAbrv, string motto, int flagData)
        {
            if (this.ChangeFactionMotto_Callback == null)
            {
                this.ChangeFactionMotto_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ChangeFactionMotto);
            }
            RemoteAsyncDelegate_ChangeFactionMotto motto2 = new RemoteAsyncDelegate_ChangeFactionMotto(this.service.ChangeFactionMotto);
            this.registerRPCcall(motto2.BeginInvoke(this.UserID, this.SessionID, factionName, factionNameAbrv, motto, flagData, this.ChangeFactionMotto_Callback, null), typeof(ChangeFactionMotto_ReturnType));
        }

        public void Chat_Admin_Command(int command, int targetUserID)
        {
            if (this.Chat_Admin_Command_Callback == null)
            {
                this.Chat_Admin_Command_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_Admin_Command);
            }
            RemoteAsyncDelegate_Chat_Admin_Command command2 = new RemoteAsyncDelegate_Chat_Admin_Command(this.service.Chat_Admin_Command);
            this.registerRPCcall(command2.BeginInvoke(Instance.UserID, Instance.SessionID, command, targetUserID, this.Chat_Admin_Command_Callback, null), typeof(Chat_Admin_Command_ReturnType));
        }

        public void Chat_BackFillParishText(int parishID, int pageID, long oldestKnownID, DateTime lastTime)
        {
            if (this.ChatActive)
            {
                if (this.Chat_BackFillParishText_Callback == null)
                {
                    this.Chat_BackFillParishText_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_BackFillParishText);
                }
                RemoteAsyncDelegate_Chat_BackFillParishText text = new RemoteAsyncDelegate_Chat_BackFillParishText(this.service.Chat_BackFillParishText);
                this.registerRPCcall(text.BeginInvoke(Instance.UserID, Instance.SessionID, parishID, pageID, oldestKnownID, lastTime, Instance.UserOptions.profanityFilter, this.Chat_BackFillParishText_Callback, null), typeof(Chat_BackFillParishText_ReturnType));
            }
        }

        public void Chat_GetText(List<Chat_RoomID> roomsToRegister, bool changeRooms)
        {
            if (this.ChatActive)
            {
                if (this.Chat_ReceiveText_Callback == null)
                {
                    this.Chat_ReceiveText_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_ReceiveText);
                }
                RemoteAsyncDelegate_Chat_ReceiveText text = new RemoteAsyncDelegate_Chat_ReceiveText(this.service.Chat_ReceiveText);
                this.registerRPCcall(text.BeginInvoke(Instance.UserID, Instance.SessionID, roomsToRegister, changeRooms, Instance.UserOptions.profanityFilter, this.Chat_ReceiveText_Callback, null), typeof(Chat_ReceiveText_ReturnType));
            }
        }

        public void Chat_Login()
        {
            if (this.ChatActive)
            {
                if (this.Chat_Login_Callback == null)
                {
                    this.Chat_Login_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_Login);
                }
                RemoteAsyncDelegate_Chat_Login login = new RemoteAsyncDelegate_Chat_Login(this.service.Chat_Login);
                this.registerRPCcall(login.BeginInvoke(Instance.UserID, Instance.SessionID, this.Chat_Login_Callback, null), typeof(Chat_Login_ReturnType));
            }
        }

        public void Chat_Logout()
        {
            if (this.ChatActive && (this.SessionID != 0))
            {
                if (this.Chat_Logout_Callback == null)
                {
                    this.Chat_Logout_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_Logout);
                }
                RemoteAsyncDelegate_Chat_Logout logout = new RemoteAsyncDelegate_Chat_Logout(this.service.Chat_Logout);
                this.registerRPCcall(logout.BeginInvoke(Instance.UserID, Instance.SessionID, this.Chat_Logout_Callback, null), typeof(Chat_Logout_ReturnType));
            }
        }

        public void Chat_MarkParishTextRead(int parishID, int pageID, long readID)
        {
            if (this.ChatActive)
            {
                if (this.Chat_MarkParishTextRead_Callback == null)
                {
                    this.Chat_MarkParishTextRead_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_MarkParishTextRead);
                }
                RemoteAsyncDelegate_Chat_MarkParishTextRead read = new RemoteAsyncDelegate_Chat_MarkParishTextRead(this.service.Chat_MarkParishTextRead);
                this.registerRPCcall(read.BeginInvoke(Instance.UserID, Instance.SessionID, parishID, pageID, readID, this.Chat_MarkParishTextRead_Callback, null), typeof(Chat_MarkParishTextRead_ReturnType));
            }
        }

        public void Chat_ReceiveParishText(int parishID, DateTime lastTime)
        {
            if (this.ChatActive)
            {
                if (this.Chat_ReceiveParishText_Callback == null)
                {
                    this.Chat_ReceiveParishText_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_ReceiveParishText);
                }
                RemoteAsyncDelegate_Chat_ReceiveParishText text = new RemoteAsyncDelegate_Chat_ReceiveParishText(this.service.Chat_ReceiveParishText);
                this.registerRPCcall(text.BeginInvoke(Instance.UserID, Instance.SessionID, parishID, lastTime, Instance.UserOptions.profanityFilter, this.Chat_ReceiveParishText_Callback, null), typeof(Chat_ReceiveParishText_ReturnType));
            }
        }

        public void Chat_SendParishText(string text, int parishID, int subForumID, DateTime lastTime)
        {
            if (this.ChatActive)
            {
                if (this.Chat_SendParishText_Callback == null)
                {
                    this.Chat_SendParishText_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_SendParishText);
                }
                RemoteAsyncDelegate_Chat_SendParishText text2 = new RemoteAsyncDelegate_Chat_SendParishText(this.service.Chat_SendParishText);
                this.registerRPCcall(text2.BeginInvoke(Instance.UserID, Instance.SessionID, parishID, subForumID, text, lastTime, Instance.UserOptions.profanityFilter, this.Chat_SendParishText_Callback, null), typeof(Chat_SendParishText_ReturnType));
            }
        }

        public void Chat_SendText(string text, int roomType, int roomID)
        {
            if (this.ChatActive)
            {
                if (this.Chat_SendText_Callback == null)
                {
                    this.Chat_SendText_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_SendText);
                }
                RemoteAsyncDelegate_Chat_SendText text2 = new RemoteAsyncDelegate_Chat_SendText(this.service.Chat_SendText);
                this.registerRPCcall(text2.BeginInvoke(Instance.UserID, Instance.SessionID, roomType, roomID, text, Instance.UserOptions.profanityFilter, this.Chat_SendText_Callback, null), typeof(Chat_SendText_ReturnType));
            }
        }

        public void Chat_StartReceiving()
        {
            if (this.ChatActive)
            {
                if (this.Chat_SetReceivingState_Callback == null)
                {
                    this.Chat_SetReceivingState_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_SetReceivingState);
                }
                RemoteAsyncDelegate_Chat_SetReceivingState state = new RemoteAsyncDelegate_Chat_SetReceivingState(this.service.Chat_SetReceivingState);
                this.registerRPCcall(state.BeginInvoke(Instance.UserID, Instance.SessionID, true, this.Chat_SetReceivingState_Callback, null), typeof(Chat_SetReceivingState_ReturnType));
            }
        }

        public void Chat_StopReceiving()
        {
            if (this.ChatActive)
            {
                if (this.Chat_SetReceivingState_Callback == null)
                {
                    this.Chat_SetReceivingState_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_Chat_SetReceivingState);
                }
                RemoteAsyncDelegate_Chat_SetReceivingState state = new RemoteAsyncDelegate_Chat_SetReceivingState(this.service.Chat_SetReceivingState);
                this.registerRPCcall(state.BeginInvoke(Instance.UserID, Instance.SessionID, false, this.Chat_SetReceivingState_Callback, null), typeof(Chat_SetReceivingState_ReturnType));
            }
        }

        public void CheatAddTroops(int villageID, int troopsType, int numTroops)
        {
            if (this.CheatAddTroops_Callback == null)
            {
                this.CheatAddTroops_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CheatAddTroops);
            }
            RemoteAsyncDelegate_CheatAddTroops troops = new RemoteAsyncDelegate_CheatAddTroops(this.service.CheatAddTroops);
            this.registerRPCcall(troops.BeginInvoke(this.UserID, this.SessionID, villageID, troopsType, numTroops, this.CheatAddTroops_Callback, null), typeof(CheatAddTroops_ReturnType));
        }

        public void CheckQuestObjectiveComplete(int quest)
        {
            if (this.CheckQuestObjectiveComplete_Callback == null)
            {
                this.CheckQuestObjectiveComplete_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CheckQuestObjectiveComplete);
            }
            RemoteAsyncDelegate_CheckQuestObjectiveComplete complete = new RemoteAsyncDelegate_CheckQuestObjectiveComplete(this.service.CheckQuestObjectiveComplete);
            this.registerRPCcall(complete.BeginInvoke(this.UserID, this.SessionID, quest, this.CheckQuestObjectiveComplete_Callback, null), typeof(CheckQuestObjectiveComplete_ReturnType));
        }

        public void clearQueues()
        {
            this.resultList.Clear();
            this.queuedResultList.Clear();
        }

        public void CompleteNewQuest(int questID, bool glory, int villageID)
        {
            if (this.CompleteAbandonNewQuest_Callback == null)
            {
                this.CompleteAbandonNewQuest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CompleteAbandonNewQuest);
            }
            RemoteAsyncDelegate_CompleteAbandonNewQuest quest = new RemoteAsyncDelegate_CompleteAbandonNewQuest(this.service.CompleteAbandonNewQuest);
            this.registerRPCcall(quest.BeginInvoke(this.UserID, this.SessionID, questID, false, glory, villageID, this.CompleteAbandonNewQuest_Callback, null), typeof(CompleteAbandonNewQuest_ReturnType));
        }

        public void CompleteQuest(int quest)
        {
            if (this.CompleteQuest_Callback == null)
            {
                this.CompleteQuest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CompleteQuest);
            }
            RemoteAsyncDelegate_CompleteQuest quest2 = new RemoteAsyncDelegate_CompleteQuest(this.service.CompleteQuest);
            this.registerRPCcall(quest2.BeginInvoke(this.UserID, this.SessionID, quest, this.CompleteQuest_Callback, null), typeof(CompleteQuest_ReturnType));
        }

        public void CompleteVillageCastle(int villageID, int mode)
        {
            if (this.CompleteVillageCastle_Callback == null)
            {
                this.CompleteVillageCastle_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CompleteVillageCastle);
            }
            RemoteAsyncDelegate_CompleteVillageCastle castle = new RemoteAsyncDelegate_CompleteVillageCastle(this.service.CompleteVillageCastle);
            this.registerRPCcall(castle.BeginInvoke(this.UserID, this.SessionID, villageID, mode, this.CompleteVillageCastle_Callback, null), typeof(CompleteVillageCastle_ReturnType));
        }

        public void ConvertVillage(int villageID, int mapType)
        {
            if (this.ConvertVillage_Callback == null)
            {
                this.ConvertVillage_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ConvertVillage);
            }
            RemoteAsyncDelegate_ConvertVillage village = new RemoteAsyncDelegate_ConvertVillage(this.service.ConvertVillage);
            this.registerRPCcall(village.BeginInvoke(this.UserID, this.SessionID, villageID, mapType, this.ConvertVillage_Callback, null), typeof(ConvertVillage_ReturnType));
        }

        public void CreateFaction(string factionName, string factionNameabrv, string factionMotto, int flagdata)
        {
            if (this.CreateFaction_Callback == null)
            {
                this.CreateFaction_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CreateFaction);
            }
            RemoteAsyncDelegate_CreateFaction faction = new RemoteAsyncDelegate_CreateFaction(this.service.CreateFaction);
            this.registerRPCcall(faction.BeginInvoke(this.UserID, this.SessionID, factionName, factionNameabrv, factionMotto, flagdata, this.CreateFaction_Callback, null), typeof(CreateFaction_ReturnType));
        }

        public void CreateFactionRelationship(int targetFactionID, int relationship)
        {
            if (this.CreateFactionRelationship_Callback == null)
            {
                this.CreateFactionRelationship_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CreateFactionRelationship);
            }
            RemoteAsyncDelegate_CreateFactionRelationship relationship2 = new RemoteAsyncDelegate_CreateFactionRelationship(this.service.CreateFactionRelationship);
            this.registerRPCcall(relationship2.BeginInvoke(this.UserID, this.SessionID, targetFactionID, relationship, this.CreateFactionRelationship_Callback, null), typeof(CreateFactionRelationship_ReturnType));
        }

        public void CreateForum(int areaID, int areaType, string name)
        {
            if (this.CreateForum_Callback == null)
            {
                this.CreateForum_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CreateForum);
            }
            RemoteAsyncDelegate_CreateForum forum = new RemoteAsyncDelegate_CreateForum(this.service.CreateForum);
            this.registerRPCcall(forum.BeginInvoke(this.UserID, this.SessionID, areaID, areaType, name, this.CreateForum_Callback, null), typeof(CreateForum_ReturnType));
        }

        public void CreateHouseRelationship(int targetHouseID, int relationship)
        {
            if (this.CreateHouseRelationship_Callback == null)
            {
                this.CreateHouseRelationship_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CreateHouseRelationship);
            }
            RemoteAsyncDelegate_CreateHouseRelationship relationship2 = new RemoteAsyncDelegate_CreateHouseRelationship(this.service.CreateHouseRelationship);
            this.registerRPCcall(relationship2.BeginInvoke(this.UserID, this.SessionID, targetHouseID, relationship, this.CreateHouseRelationship_Callback, null), typeof(CreateHouseRelationship_ReturnType));
        }

        public void CreateMailFolder(string folderName)
        {
            if (this.CreateMailFolder_Callback == null)
            {
                this.CreateMailFolder_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CreateMailFolder);
            }
            RemoteAsyncDelegate_CreateMailFolder folder = new RemoteAsyncDelegate_CreateMailFolder(this.service.CreateMailFolder);
            this.registerRPCcall(folder.BeginInvoke(this.UserID, this.SessionID, folderName, this.CreateMailFolder_Callback, null), typeof(CreateMailFolder_ReturnType));
        }

        public void createNewUser(string username, string password, string realname, string emailaddress)
        {
            if (this.CreateNewUser_Callback == null)
            {
                this.CreateNewUser_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CreateNewUser);
            }
            RemoteAsyncDelegate_CreateNewUser user = new RemoteAsyncDelegate_CreateNewUser(this.service.CreateNewUser);
            this.registerRPCcall(user.BeginInvoke(username, password, realname, emailaddress, BuildVersion.VersionNumber, "", this.CreateNewUser_Callback, null), typeof(CreateNewUser_ReturnType));
        }

        public void CreateUserRelationship(int targetUserID, int relationship)
        {
            if (this.CreateUserRelationship_Callback == null)
            {
                this.CreateUserRelationship_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_CreateUserRelationship);
            }
            RemoteAsyncDelegate_CreateUserRelationship relationship2 = new RemoteAsyncDelegate_CreateUserRelationship(this.service.CreateUserRelationship);
            this.registerRPCcall(relationship2.BeginInvoke(this.UserID, this.SessionID, targetUserID, relationship, this.CreateUserRelationship_Callback, null), typeof(CreateUserRelationship_ReturnType));
        }

        public void DeclineVassalRequest(int requesterVillageID, int vassalVillageID)
        {
            if (this.HandleVassalRequest_Callback == null)
            {
                this.HandleVassalRequest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_HandleVassalRequest);
            }
            RemoteAsyncDelegate_HandleVassalRequest request = new RemoteAsyncDelegate_HandleVassalRequest(this.service.HandleVassalRequest);
            this.registerRPCcall(request.BeginInvoke(this.UserID, this.SessionID, 2, requesterVillageID, vassalVillageID, this.HandleVassalRequest_Callback, null), typeof(HandleVassalRequest_ReturnType));
        }

        public void DeleteAllCastleElements(int villageID)
        {
            if (this.DeleteCastleElement_Callback == null)
            {
                this.DeleteCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteCastleElement);
            }
            RemoteAsyncDelegate_DeleteCastleElement element = new RemoteAsyncDelegate_DeleteCastleElement(this.service.DeleteCastleElement);
            this.registerRPCcall(element.BeginInvoke(this.UserID, this.SessionID, villageID, -51L, null, this.DeleteCastleElement_Callback, null), typeof(DeleteCastleElement_ReturnType));
        }

        public void DeleteAllCastleMoatElements(int villageID)
        {
            if (this.DeleteCastleElement_Callback == null)
            {
                this.DeleteCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteCastleElement);
            }
            RemoteAsyncDelegate_DeleteCastleElement element = new RemoteAsyncDelegate_DeleteCastleElement(this.service.DeleteCastleElement);
            this.registerRPCcall(element.BeginInvoke(this.UserID, this.SessionID, villageID, -71L, null, this.DeleteCastleElement_Callback, null), typeof(DeleteCastleElement_ReturnType));
        }

        public void DeleteAllCastleOilPotsElements(int villageID)
        {
            if (this.DeleteCastleElement_Callback == null)
            {
                this.DeleteCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteCastleElement);
            }
            RemoteAsyncDelegate_DeleteCastleElement element = new RemoteAsyncDelegate_DeleteCastleElement(this.service.DeleteCastleElement);
            this.registerRPCcall(element.BeginInvoke(this.UserID, this.SessionID, villageID, -81L, null, this.DeleteCastleElement_Callback, null), typeof(DeleteCastleElement_ReturnType));
        }

        public void DeleteAllCastlePitsElements(int villageID)
        {
            if (this.DeleteCastleElement_Callback == null)
            {
                this.DeleteCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteCastleElement);
            }
            RemoteAsyncDelegate_DeleteCastleElement element = new RemoteAsyncDelegate_DeleteCastleElement(this.service.DeleteCastleElement);
            this.registerRPCcall(element.BeginInvoke(this.UserID, this.SessionID, villageID, -61L, null, this.DeleteCastleElement_Callback, null), typeof(DeleteCastleElement_ReturnType));
        }

        public void DeleteCastleElement(int villageID, List<long> elementList)
        {
            if (this.DeleteCastleElement_Callback == null)
            {
                this.DeleteCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteCastleElement);
            }
            RemoteAsyncDelegate_DeleteCastleElement element = new RemoteAsyncDelegate_DeleteCastleElement(this.service.DeleteCastleElement);
            this.registerRPCcall(element.BeginInvoke(this.UserID, this.SessionID, villageID, -1L, elementList, this.DeleteCastleElement_Callback, null), typeof(DeleteCastleElement_ReturnType));
        }

        public void DeleteCastleElement(int villageID, long elementID)
        {
            if (this.DeleteCastleElement_Callback == null)
            {
                this.DeleteCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteCastleElement);
            }
            RemoteAsyncDelegate_DeleteCastleElement element = new RemoteAsyncDelegate_DeleteCastleElement(this.service.DeleteCastleElement);
            this.registerRPCcall(element.BeginInvoke(this.UserID, this.SessionID, villageID, elementID, null, this.DeleteCastleElement_Callback, null), typeof(DeleteCastleElement_ReturnType));
        }

        public void DeleteConstructingCastleElements(int villageID)
        {
            if (this.DeleteCastleElement_Callback == null)
            {
                this.DeleteCastleElement_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteCastleElement);
            }
            RemoteAsyncDelegate_DeleteCastleElement element = new RemoteAsyncDelegate_DeleteCastleElement(this.service.DeleteCastleElement);
            this.registerRPCcall(element.BeginInvoke(this.UserID, this.SessionID, villageID, -1L, null, this.DeleteCastleElement_Callback, null), typeof(DeleteCastleElement_ReturnType));
        }

        public void DeleteForum(int areaID, int areaType, long forumID)
        {
            if (this.DeleteForum_Callback == null)
            {
                this.DeleteForum_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteForum);
            }
            RemoteAsyncDelegate_DeleteForum forum = new RemoteAsyncDelegate_DeleteForum(this.service.DeleteForum);
            this.registerRPCcall(forum.BeginInvoke(this.UserID, this.SessionID, areaID, areaType, forumID, this.DeleteForum_Callback, null), typeof(DeleteForum_ReturnType));
        }

        public void DeleteForumPost(int areaID, int areaType, string name, long forumID, long forumThreadID, long forumPostID)
        {
            if (this.DeleteForumPost_Callback == null)
            {
                this.DeleteForumPost_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteForumPost);
            }
            RemoteAsyncDelegate_DeleteForumPost post = new RemoteAsyncDelegate_DeleteForumPost(this.service.DeleteForumPost);
            this.registerRPCcall(post.BeginInvoke(this.UserID, this.SessionID, areaID, areaType, name, forumID, forumThreadID, forumPostID, this.DeleteForumPost_Callback, null), typeof(DeleteForumPost_ReturnType));
        }

        public void DeleteForumThread(int areaID, int areaType, string name, long forumID, long forumThreadID)
        {
            if (this.DeleteForumThread_Callback == null)
            {
                this.DeleteForumThread_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteForumThread);
            }
            RemoteAsyncDelegate_DeleteForumThread thread = new RemoteAsyncDelegate_DeleteForumThread(this.service.DeleteForumThread);
            this.registerRPCcall(thread.BeginInvoke(this.UserID, this.SessionID, areaID, areaType, name, forumID, forumThreadID, this.DeleteForumThread_Callback, null), typeof(DeleteForumThread_ReturnType));
        }

        public void DeleteMailThread(long threadID)
        {
            if (this.DeleteMailThread_Callback == null)
            {
                this.DeleteMailThread_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteMailThread);
            }
            RemoteAsyncDelegate_DeleteMailThread thread = new RemoteAsyncDelegate_DeleteMailThread(this.service.DeleteMailThread);
            this.registerRPCcall(thread.BeginInvoke(this.UserID, this.SessionID, threadID, this.DeleteMailThread_Callback, null), typeof(DeleteMailThread_ReturnType));
        }

        public void deleteReportFolder(long folderID, int mode)
        {
            if (this.ManageReportFolders_Callback == null)
            {
                this.ManageReportFolders_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ManageReportFolders);
            }
            RemoteAsyncDelegate_ManageReportFolders folders = new RemoteAsyncDelegate_ManageReportFolders(this.service.ManageReportFolders);
            this.registerRPCcall(folders.BeginInvoke(this.UserID, this.SessionID, mode, folderID, "", this.ManageReportFolders_Callback, null), typeof(ManageReportFolders_ReturnType));
        }

        public void DeleteReports(long[] reportsToDelete)
        {
            if (this.DeleteReports_Callback == null)
            {
                this.DeleteReports_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteReports);
            }
            RemoteAsyncDelegate_DeleteReports reports = new RemoteAsyncDelegate_DeleteReports(this.service.DeleteOrMoveReports);
            this.registerRPCcall(reports.BeginInvoke(this.UserID, this.SessionID, 0, reportsToDelete, -1L, this.DeleteReports_Callback, null), typeof(DeleteReports_ReturnType));
        }

        public void DeleteVillageBuilding(int villageID, long buildingID)
        {
            if (this.DeleteVillageBuilding_Callback == null)
            {
                this.DeleteVillageBuilding_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteVillageBuilding);
            }
            RemoteAsyncDelegate_DeleteVillageBuilding building = new RemoteAsyncDelegate_DeleteVillageBuilding(this.service.DeleteVillageBuilding);
            this.registerRPCcall(building.BeginInvoke(this.UserID, this.SessionID, villageID, buildingID, this.DeleteVillageBuilding_Callback, null), typeof(DeleteVillageBuilding_ReturnType));
        }

        public void DisbandFaction()
        {
            if (this.DisbandFaction_Callback == null)
            {
                this.DisbandFaction_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DisbandFaction);
            }
            RemoteAsyncDelegate_DisbandFaction faction = new RemoteAsyncDelegate_DisbandFaction(this.service.DisbandFaction);
            this.registerRPCcall(faction.BeginInvoke(this.UserID, this.SessionID, this.UserFactionID, this.DisbandFaction_Callback, null), typeof(DisbandFaction_ReturnType));
        }

        public void DisbandPeople(int villageID, int troopType, int amount)
        {
            if (this.DisbandPeople_Callback == null)
            {
                this.DisbandPeople_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DisbandPeople);
            }
            RemoteAsyncDelegate_DisbandPeople people = new RemoteAsyncDelegate_DisbandPeople(this.service.DisbandPeople);
            this.registerRPCcall(people.BeginInvoke(this.UserID, this.SessionID, villageID, troopType, amount, this.DisbandPeople_Callback, null), typeof(DisbandPeople_ReturnType));
        }

        public void DisbandTroops(int villageID, int troopType, int amount)
        {
            if (this.DisbandTroops_Callback == null)
            {
                this.DisbandTroops_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DisbandTroops);
            }
            RemoteAsyncDelegate_DisbandTroops troops = new RemoteAsyncDelegate_DisbandTroops(this.service.DisbandTroops);
            this.registerRPCcall(troops.BeginInvoke(this.UserID, this.SessionID, villageID, troopType, amount, this.DisbandTroops_Callback, null), typeof(DisbandTroops_ReturnType));
        }

        public void DonateCapitalGoods(int targetVillageID, int sourceVillageID, int resourceType, int amount, int buildingType, long targetBuildingID)
        {
            if (this.DonateCapitalGoods_Callback == null)
            {
                this.DonateCapitalGoods_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DonateCapitalGoods);
            }
            RemoteAsyncDelegate_DonateCapitalGoods goods = new RemoteAsyncDelegate_DonateCapitalGoods(this.service.DonateCapitalGoods);
            this.registerRPCcall(goods.BeginInvoke(this.UserID, this.SessionID, targetVillageID, sourceVillageID, resourceType, amount, buildingType, targetBuildingID, this.DonateCapitalGoods_Callback, null), typeof(DonateCapitalGoods_ReturnType));
        }

        public void DoResearch(int researchType)
        {
            if (this.DoResearch_Callback == null)
            {
                this.DoResearch_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DoResearch);
            }
            RemoteAsyncDelegate_DoResearch research = new RemoteAsyncDelegate_DoResearch(this.service.DoResearch);
            this.registerRPCcall(research.BeginInvoke(this.UserID, this.SessionID, researchType, -1, this.DoResearch_Callback, null), typeof(DoResearch_ReturnType));
        }

        public void FactionApplication(int factionID)
        {
            if (this.FactionApplication_Callback == null)
            {
                this.FactionApplication_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionApplication);
            }
            RemoteAsyncDelegate_FactionApplication application = new RemoteAsyncDelegate_FactionApplication(this.service.FactionApplication);
            this.registerRPCcall(application.BeginInvoke(this.UserID, this.SessionID, factionID, false, this.FactionApplication_Callback, null), typeof(FactionApplication_ReturnType));
        }

        public void FactionApplicationAccept(int userID)
        {
            if (this.FactionApplicationProcessing_Callback == null)
            {
                this.FactionApplicationProcessing_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionApplicationProcessing);
            }
            RemoteAsyncDelegate_FactionApplicationProcessing processing = new RemoteAsyncDelegate_FactionApplicationProcessing(this.service.FactionApplicationProcessing);
            this.registerRPCcall(processing.BeginInvoke(this.UserID, this.SessionID, userID, true, false, false, this.FactionApplicationProcessing_Callback, null), typeof(FactionApplicationProcessing_ReturnType));
        }

        public void FactionApplicationCancel(int factionID)
        {
            if (this.FactionApplication_Callback == null)
            {
                this.FactionApplication_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionApplication);
            }
            RemoteAsyncDelegate_FactionApplication application = new RemoteAsyncDelegate_FactionApplication(this.service.FactionApplication);
            this.registerRPCcall(application.BeginInvoke(this.UserID, this.SessionID, factionID, true, this.FactionApplication_Callback, null), typeof(FactionApplication_ReturnType));
        }

        public void FactionApplicationReject(int userID)
        {
            if (this.FactionApplicationProcessing_Callback == null)
            {
                this.FactionApplicationProcessing_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionApplicationProcessing);
            }
            RemoteAsyncDelegate_FactionApplicationProcessing processing = new RemoteAsyncDelegate_FactionApplicationProcessing(this.service.FactionApplicationProcessing);
            this.registerRPCcall(processing.BeginInvoke(this.UserID, this.SessionID, userID, false, true, false, this.FactionApplicationProcessing_Callback, null), typeof(FactionApplicationProcessing_ReturnType));
        }

        public void FactionApplicationSetMode(bool accepting)
        {
            if (this.FactionApplicationProcessing_Callback == null)
            {
                this.FactionApplicationProcessing_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionApplicationProcessing);
            }
            RemoteAsyncDelegate_FactionApplicationProcessing processing = new RemoteAsyncDelegate_FactionApplicationProcessing(this.service.FactionApplicationProcessing);
            this.registerRPCcall(processing.BeginInvoke(this.UserID, this.SessionID, -1, false, false, accepting, this.FactionApplicationProcessing_Callback, null), typeof(FactionApplicationProcessing_ReturnType));
        }

        public void FactionChangeMemberStatus(int memberUserID, int targetRank)
        {
            if (this.FactionChangeMemberStatus_Callback == null)
            {
                this.FactionChangeMemberStatus_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionChangeMemberStatus);
            }
            RemoteAsyncDelegate_FactionChangeMemberStatus status = new RemoteAsyncDelegate_FactionChangeMemberStatus(this.service.FactionChangeMemberStatus);
            this.registerRPCcall(status.BeginInvoke(this.UserID, this.SessionID, memberUserID, targetRank, this.FactionChangeMemberStatus_Callback, null), typeof(FactionChangeMemberStatus_ReturnType));
        }

        public void FactionLeadershipVote(int factionID, int votedID)
        {
            if (this.FactionLeadershipVote_Callback == null)
            {
                this.FactionLeadershipVote_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionLeadershipVote);
            }
            RemoteAsyncDelegate_FactionLeadershipVote vote = new RemoteAsyncDelegate_FactionLeadershipVote(this.service.FactionLeadershipVote);
            this.registerRPCcall(vote.BeginInvoke(this.UserID, this.SessionID, factionID, votedID, this.FactionLeadershipVote_Callback, null), typeof(FactionLeadershipVote_ReturnType));
        }

        public void FactionLeave()
        {
            if (this.FactionLeave_Callback == null)
            {
                this.FactionLeave_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionLeave);
            }
            RemoteAsyncDelegate_FactionLeave leave = new RemoteAsyncDelegate_FactionLeave(this.service.FactionLeave);
            this.registerRPCcall(leave.BeginInvoke(this.UserID, this.SessionID, this.FactionLeave_Callback, null), typeof(FactionLeave_ReturnType));
        }

        public void FactionReplyToInvite(int factionID, bool accept)
        {
            if (this.FactionReplyToInvite_Callback == null)
            {
                this.FactionReplyToInvite_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionReplyToInvite);
            }
            RemoteAsyncDelegate_FactionReplyToInvite invite = new RemoteAsyncDelegate_FactionReplyToInvite(this.service.FactionReplyToInvite);
            this.registerRPCcall(invite.BeginInvoke(this.UserID, this.SessionID, factionID, accept, this.FactionReplyToInvite_Callback, null), typeof(FactionReplyToInvite_ReturnType));
        }

        public void FactionSendInvite(string targetUser)
        {
            if (this.FactionSendInvite_Callback == null)
            {
                this.FactionSendInvite_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionSendInvite);
            }
            RemoteAsyncDelegate_FactionSendInvite invite = new RemoteAsyncDelegate_FactionSendInvite(this.service.FactionSendInvite);
            this.registerRPCcall(invite.BeginInvoke(this.UserID, this.SessionID, targetUser, this.FactionSendInvite_Callback, null), typeof(FactionSendInvite_ReturnType));
        }

        public void FactionWithdrawInvite(int targetUserID)
        {
            if (this.FactionWithdrawInvite_Callback == null)
            {
                this.FactionWithdrawInvite_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FactionWithdrawInvite);
            }
            RemoteAsyncDelegate_FactionWithdrawInvite invite = new RemoteAsyncDelegate_FactionWithdrawInvite(this.service.FactionWithdrawInvite);
            this.registerRPCcall(invite.BeginInvoke(this.UserID, this.SessionID, targetUserID, this.FactionWithdrawInvite_Callback, null), typeof(FactionWithdrawInvite_ReturnType));
        }

        public void FlagMailRead(long mailID)
        {
            if (this.FlagMailRead_Callback == null)
            {
                this.FlagMailRead_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FlagMailRead);
            }
            RemoteAsyncDelegate_FlagMailRead read = new RemoteAsyncDelegate_FlagMailRead(this.service.FlagMailRead);
            this.registerRPCcall(read.BeginInvoke(this.UserID, this.SessionID, mailID, -1L, true, this.FlagMailRead_Callback, null), typeof(FlagMailRead_ReturnType));
        }

        public void FlagQuestObjectiveComplete(int objective)
        {
            if (this.FlagQuestObjectiveComplete_Callback == null)
            {
                this.FlagQuestObjectiveComplete_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FlagQuestObjectiveComplete);
            }
            RemoteAsyncDelegate_FlagQuestObjectiveComplete complete = new RemoteAsyncDelegate_FlagQuestObjectiveComplete(this.service.FlagQuestObjectiveComplete);
            this.registerRPCcall(complete.BeginInvoke(this.UserID, this.SessionID, objective, this.FlagQuestObjectiveComplete_Callback, null), typeof(FlagQuestObjectiveComplete_ReturnType));
        }

        public void FlagThreadRead(long threadID)
        {
            if (this.FlagMailRead_Callback == null)
            {
                this.FlagMailRead_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FlagMailRead);
            }
            RemoteAsyncDelegate_FlagMailRead read = new RemoteAsyncDelegate_FlagMailRead(this.service.FlagMailRead);
            this.registerRPCcall(read.BeginInvoke(this.UserID, this.SessionID, -1L, threadID, true, this.FlagMailRead_Callback, null), typeof(FlagMailRead_ReturnType));
        }

        public void FlagThreadUnread(long threadID)
        {
            if (this.FlagMailRead_Callback == null)
            {
                this.FlagMailRead_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FlagMailRead);
            }
            RemoteAsyncDelegate_FlagMailRead read = new RemoteAsyncDelegate_FlagMailRead(this.service.FlagMailRead);
            this.registerRPCcall(read.BeginInvoke(this.UserID, this.SessionID, -1L, threadID, false, this.FlagMailRead_Callback, null), typeof(FlagMailRead_ReturnType));
        }

        public void ForwardReport(long reportID, string[] recipients)
        {
            if (this.ForwardReport_Callback == null)
            {
                this.ForwardReport_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ForwardReport);
            }
            RemoteAsyncDelegate_ForwardReport report = new RemoteAsyncDelegate_ForwardReport(this.service.ForwardReport);
            this.registerRPCcall(report.BeginInvoke(this.UserID, this.SessionID, reportID, recipients, this.ForwardReport_Callback, null), typeof(ForwardReport_ReturnType));
        }

        public void FullTick(long startChangePos, long regionStartPos, long countyStartPos, long provinceStartPos, long countryStartPos, bool registerSession, long villageNamePos, long factionsChangePos, DateTime lastTraderTime, DateTime lastPeopleTime, long parishFlagsPos, long countyFlagsPos, long provinceFlagsPos, long countryFlagsPos, long highestArmyID, int mode, bool fullMode)
        {
            if (this.FullTick_Callback == null)
            {
                this.FullTick_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_FullTick);
            }
            RemoteAsyncDelegate_FullTick tick = new RemoteAsyncDelegate_FullTick(this.service.FullTick);
            this.registerRPCcall(tick.BeginInvoke(this.UserID, this.SessionID, startChangePos, regionStartPos, countyStartPos, provinceStartPos, countryStartPos, registerSession, villageNamePos, factionsChangePos, lastTraderTime, lastPeopleTime, parishFlagsPos, countyFlagsPos, provinceFlagsPos, countryFlagsPos, highestArmyID, mode, fullMode, this.FullTick_Callback, null), typeof(FullTick_ReturnType));
        }

        public void GetActivePeople(DateTime lastTime)
        {
            if (this.GetActivePeople_Callback == null)
            {
                this.GetActivePeople_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetActivePeople);
            }
            RemoteAsyncDelegate_GetActivePeople people = new RemoteAsyncDelegate_GetActivePeople(this.service.GetActivePeople);
            this.registerRPCcall(people.BeginInvoke(this.UserID, this.SessionID, lastTime, this.GetActivePeople_Callback, null), typeof(GetActivePeople_ReturnType));
        }

        public void GetActiveTraders(DateTime lastTime)
        {
            if (this.GetActiveTraders_Callback == null)
            {
                this.GetActiveTraders_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetActiveTraders);
            }
            RemoteAsyncDelegate_GetActiveTraders traders = new RemoteAsyncDelegate_GetActiveTraders(this.service.GetActiveTraders);
            this.registerRPCcall(traders.BeginInvoke(this.UserID, this.SessionID, lastTime, this.GetActiveTraders_Callback, null), typeof(GetActiveTraders_ReturnType));
        }

        public void GetAdminStats()
        {
            if (this.GetAdminStats_Callback == null)
            {
                this.GetAdminStats_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetAdminStats);
            }
            RemoteAsyncDelegate_GetAdminStats stats = new RemoteAsyncDelegate_GetAdminStats(this.service.GetAdminStats);
            this.registerRPCcall(stats.BeginInvoke(this.UserID, this.SessionID, this.GetAdminStats_Callback, null), typeof(GetAdminStats_ReturnType));
        }

        public void GetAllVillageOwnerFactions()
        {
            if (this.GetAllVillageOwnerFactions_Callback == null)
            {
                this.GetAllVillageOwnerFactions_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetAllVillageOwnerFactions);
            }
            RemoteAsyncDelegate_GetAllVillageOwnerFactions factions = new RemoteAsyncDelegate_GetAllVillageOwnerFactions(this.service.GetAllVillageOwnerFactions);
            this.GetAllVillageOwnerFactions_ValidDownload = false;
            this.GetAllVillageOwnerFactions_Index++;
            this.registerRPCcall(factions.BeginInvoke(this.UserID, this.SessionID, this.GetAllVillageOwnerFactions_Index, this.GetAllVillageOwnerFactions_Callback, null), typeof(GetAllVillageOwnerFactions_ReturnType));
        }

        public void GetAreaFactionChanges(long regionStartPos, long countyStartPos, long provinceStartPos, long countryStartPos, long parishFlagsPos, long countyFlagsPos, long provinceFlagsPos, long countryFlagsPos)
        {
            if (regionStartPos < -1L)
            {
                regionStartPos = -1L;
            }
            if (countyStartPos < -1L)
            {
                countyStartPos = -1L;
            }
            if (provinceStartPos < -1L)
            {
                provinceStartPos = -1L;
            }
            if (countryStartPos < -1L)
            {
                countryStartPos = -1L;
            }
            if (this.GetAreaFactionChanges_Callback == null)
            {
                this.GetAreaFactionChanges_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetAreaFactionChanges);
            }
            RemoteAsyncDelegate_GetAreaFactionChanges changes = new RemoteAsyncDelegate_GetAreaFactionChanges(this.service.GetAreaFactionChanges);
            this.registerRPCcall(changes.BeginInvoke(this.UserID, this.SessionID, regionStartPos, countyStartPos, provinceStartPos, countryStartPos, parishFlagsPos, countyFlagsPos, provinceFlagsPos, countryFlagsPos, this.GetAreaFactionChanges_Callback, null), typeof(GetAreaFactionChanges_ReturnType));
        }

        public void GetArmyData(long highestSeen)
        {
            if (this.GetArmyData_Callback == null)
            {
                this.GetArmyData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetArmyData);
            }
            RemoteAsyncDelegate_GetArmyData data = new RemoteAsyncDelegate_GetArmyData(this.service.GetArmyData);
            this.registerRPCcall(data.BeginInvoke(this.UserID, this.SessionID, highestSeen, this.GetArmyData_Callback, null), typeof(GetArmyData_ReturnType));
        }

        public void GetBattleHonourRating(int attackedVillage)
        {
            if (this.GetBattleHonourRating_Callback == null)
            {
                this.GetBattleHonourRating_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetBattleHonourRating);
            }
            RemoteAsyncDelegate_GetBattleHonourRating rating = new RemoteAsyncDelegate_GetBattleHonourRating(this.service.GetBattleHonourRating);
            this.registerRPCcall(rating.BeginInvoke(this.UserID, this.SessionID, attackedVillage, this.GetBattleHonourRating_Callback, null), typeof(GetBattleHonourRating_ReturnType));
        }

        public void GetCapitalBarracksSpace(int sourceVillageID, int targetVillageID)
        {
            if (this.GetCapitalBarracksSpace_Callback == null)
            {
                this.GetCapitalBarracksSpace_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetCapitalBarracksSpace);
            }
            RemoteAsyncDelegate_GetCapitalBarracksSpace space = new RemoteAsyncDelegate_GetCapitalBarracksSpace(this.service.GetCapitalBarracksSpace);
            this.registerRPCcall(space.BeginInvoke(this.UserID, this.SessionID, sourceVillageID, targetVillageID, this.GetCapitalBarracksSpace_Callback, null), typeof(GetCapitalBarracksSpace_ReturnType));
        }

        public void GetCastle(int villageID)
        {
            if (this.GetCastle_Callback == null)
            {
                this.GetCastle_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetCastle);
            }
            RemoteAsyncDelegate_GetCastle castle = new RemoteAsyncDelegate_GetCastle(this.service.GetCastle);
            this.registerRPCcall(castle.BeginInvoke(this.UserID, this.SessionID, villageID, this.GetCastle_Callback, null), typeof(GetCastle_ReturnType));
        }

        public void GetCountryElectionInfo(int villageID)
        {
            if (this.GetCountryElectionInfo_Callback == null)
            {
                this.GetCountryElectionInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetCountryElectionInfo);
            }
            RemoteAsyncDelegate_GetCountryElectionInfo info = new RemoteAsyncDelegate_GetCountryElectionInfo(this.service.GetCountryElectionInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, this.GetCountryElectionInfo_Callback, null), typeof(GetCountryElectionInfo_ReturnType));
        }

        public void GetCountryFrontPageInfo(int villageID)
        {
            if (this.GetCountryFrontPageInfo_Callback == null)
            {
                this.GetCountryFrontPageInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetCountryFrontPageInfo);
            }
            RemoteAsyncDelegate_GetCountryFrontPageInfo info = new RemoteAsyncDelegate_GetCountryFrontPageInfo(this.service.GetCountryFrontPageInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, this.GetCountryFrontPageInfo_Callback, null), typeof(GetCountryFrontPageInfo_ReturnType));
        }

        public void GetCountyElectionInfo(int villageID)
        {
            if (this.GetCountyElectionInfo_Callback == null)
            {
                this.GetCountyElectionInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetCountyElectionInfo);
            }
            RemoteAsyncDelegate_GetCountyElectionInfo info = new RemoteAsyncDelegate_GetCountyElectionInfo(this.service.GetCountyElectionInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, this.GetCountyElectionInfo_Callback, null), typeof(GetCountyElectionInfo_ReturnType));
        }

        public void GetCountyFrontPageInfo(int villageID)
        {
            if (this.GetCountyFrontPageInfo_Callback == null)
            {
                this.GetCountyFrontPageInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetCountyFrontPageInfo);
            }
            RemoteAsyncDelegate_GetCountyFrontPageInfo info = new RemoteAsyncDelegate_GetCountyFrontPageInfo(this.service.GetCountyFrontPageInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, this.GetCountyFrontPageInfo_Callback, null), typeof(GetCountyFrontPageInfo_ReturnType));
        }

        public void GetCurrentElectionInfo(int areaID, int areaType)
        {
            if (this.GetCurrentElectionInfo_Callback == null)
            {
                this.GetCurrentElectionInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetCurrentElectionInfo);
            }
            RemoteAsyncDelegate_GetCurrentElectionInfo info = new RemoteAsyncDelegate_GetCurrentElectionInfo(this.service.GetCurrentElectionInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, areaID, areaType, this.GetCurrentElectionInfo_Callback, null), typeof(GetCurrentElectionInfo_ReturnType));
        }

        public List<RTT_Log_data> getDetailedLogging()
        {
            return this.rtt_logging;
        }

        public void GetExcommunicationStatus(int villageID, int targetVillageID)
        {
            if (this.GetExcommunicationStatus_Callback == null)
            {
                this.GetExcommunicationStatus_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetExcommunicationStatus);
            }
            RemoteAsyncDelegate_GetExcommunicationStatus status = new RemoteAsyncDelegate_GetExcommunicationStatus(this.service.GetExcommunicationStatus);
            this.registerRPCcall(status.BeginInvoke(this.UserID, this.SessionID, villageID, targetVillageID, this.GetExcommunicationStatus_Callback, null), typeof(GetExcommunicationStatus_ReturnType));
        }

        public void GetFactionData(int factionID, long factionChangesPos)
        {
            if (this.GetFactionData_Callback == null)
            {
                this.GetFactionData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetFactionData);
            }
            RemoteAsyncDelegate_GetFactionData data = new RemoteAsyncDelegate_GetFactionData(this.service.GetFactionData);
            this.registerRPCcall(data.BeginInvoke(this.UserID, this.SessionID, factionID, factionChangesPos, this.GetFactionData_Callback, null), typeof(GetFactionData_ReturnType));
        }

        public void GetForumList(int areaID, int areaType)
        {
            if (this.GetForumList_Callback == null)
            {
                this.GetForumList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetForumList);
            }
            RemoteAsyncDelegate_GetForumList list = new RemoteAsyncDelegate_GetForumList(this.service.GetForumList);
            this.registerRPCcall(list.BeginInvoke(this.UserID, this.SessionID, areaID, areaType, this.GetForumList_Callback, null), typeof(GetForumList_ReturnType));
        }

        public void GetForumThread(long forumID, long threadID, DateTime lastGet, bool forceGet)
        {
            if (this.GetForumThread_Callback == null)
            {
                this.GetForumThread_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetForumThread);
            }
            RemoteAsyncDelegate_GetForumThread thread = new RemoteAsyncDelegate_GetForumThread(this.service.GetForumThread);
            this.registerRPCcall(thread.BeginInvoke(this.UserID, this.SessionID, forumID, threadID, lastGet, forceGet, this.GetForumThread_Callback, null), typeof(GetForumThread_ReturnType));
        }

        public void GetForumThreadList(long forumID, DateTime lastGet, bool forceGet)
        {
            if (this.GetForumThreadList_Callback == null)
            {
                this.GetForumThreadList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetForumThreadList);
            }
            RemoteAsyncDelegate_GetForumThreadList list = new RemoteAsyncDelegate_GetForumThreadList(this.service.GetForumThreadList);
            this.registerRPCcall(list.BeginInvoke(this.UserID, this.SessionID, forumID, lastGet, forceGet, this.GetForumThreadList_Callback, null), typeof(GetForumThreadList_ReturnType));
        }

        public void GetHistoricalData()
        {
            if (this.GetHistoricalData_Callback == null)
            {
                this.GetHistoricalData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetHistoricalData);
            }
            RemoteAsyncDelegate_GetHistoricalData data = new RemoteAsyncDelegate_GetHistoricalData(this.service.GetHistoricalData);
            this.registerRPCcall(data.BeginInvoke(this.UserID, this.SessionID, this.GetHistoricalData_Callback, null), typeof(GetHistoricalData_ReturnType));
        }

        public void GetHouseGloryPoints()
        {
            if (this.GetHouseGloryPoints_Callback == null)
            {
                this.GetHouseGloryPoints_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetHouseGloryPoints);
            }
            RemoteAsyncDelegate_GetHouseGloryPoints points = new RemoteAsyncDelegate_GetHouseGloryPoints(this.service.GetHouseGloryPoints);
            this.registerRPCcall(points.BeginInvoke(this.UserID, this.SessionID, this.GetHouseGloryPoints_Callback, null), typeof(GetHouseGloryPoints_ReturnType));
        }

        public void GetIngameMessage()
        {
            if (this.GetIngameMessage_Callback == null)
            {
                this.GetIngameMessage_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetIngameMessage);
            }
            RemoteAsyncDelegate_GetIngameMessage message = new RemoteAsyncDelegate_GetIngameMessage(this.service.GetIngameMessage);
            this.registerRPCcall(message.BeginInvoke(this.UserID, this.SessionID, this.GetIngameMessage_Callback, null), typeof(GetIngameMessage_ReturnType));
        }

        public void GetInvasionInfo()
        {
            if (this.GetInvasionInfo_Callback == null)
            {
                this.GetInvasionInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetInvasionInfo);
            }
            RemoteAsyncDelegate_GetInvasionInfo info = new RemoteAsyncDelegate_GetInvasionInfo(this.service.GetInvasionInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, this.GetInvasionInfo_Callback, null), typeof(GetInvasionInfo_ReturnType));
        }

        public void GetLastAttacker()
        {
            if (this.GetLastAttacker_Callback == null)
            {
                this.GetLastAttacker_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetLastAttacker);
            }
            RemoteAsyncDelegate_GetLastAttacker attacker = new RemoteAsyncDelegate_GetLastAttacker(this.service.GetLastAttacker);
            this.registerRPCcall(attacker.BeginInvoke(this.UserID, this.SessionID, this.GetLastAttacker_Callback, null), typeof(GetLastAttacker_ReturnType));
        }

        public void GetLoginHistory()
        {
            if (this.GetLoginHistory_Callback == null)
            {
                this.GetLoginHistory_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetLoginHistory);
            }
            RemoteAsyncDelegate_GetLoginHistory history = new RemoteAsyncDelegate_GetLoginHistory(this.service.GetLoginHistory);
            this.registerRPCcall(history.BeginInvoke(this.UserID, this.SessionID, this.GetLoginHistory_Callback, null), typeof(GetLoginHistory_ReturnType));
        }

        public void GetMailFolders()
        {
            if (this.GetMailFolders_Callback == null)
            {
                this.GetMailFolders_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetMailFolders);
            }
            RemoteAsyncDelegate_GetMailFolders folders = new RemoteAsyncDelegate_GetMailFolders(this.service.GetMailFolders);
            this.registerRPCcall(folders.BeginInvoke(this.UserID, this.SessionID, this.GetMailFolders_Callback, null), typeof(GetMailFolders_ReturnType));
        }

        public void GetMailRecipientsHistory()
        {
            if (this.GetMailRecipientsHistory_Callback == null)
            {
                this.GetMailRecipientsHistory_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetMailRecipientsHistory);
            }
            RemoteAsyncDelegate_GetMailRecipientsHistory history = new RemoteAsyncDelegate_GetMailRecipientsHistory(this.service.GetMailRecipientsHistory);
            this.registerRPCcall(history.BeginInvoke(this.UserID, this.SessionID, this.GetMailRecipientsHistory_Callback, null), typeof(GetMailRecipientsHistory_ReturnType));
        }

        public void GetMailThread(long threadID, int localCount, long highestSegmentID)
        {
            if (this.GetMailThread_Callback == null)
            {
                this.GetMailThread_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetMailThread);
            }
            RemoteAsyncDelegate_GetMailThread thread = new RemoteAsyncDelegate_GetMailThread(this.service.GetMailThread);
            this.registerRPCcall(thread.BeginInvoke(this.UserID, this.SessionID, threadID, localCount, highestSegmentID, this.GetMailThread_Callback, null), typeof(GetMailThread_ReturnType));
        }

        public void GetMailThreadList(bool initialRequest, int mode, DateTime lastRetrieved)
        {
            if (this.GetMailThreadList_Callback == null)
            {
                this.GetMailThreadList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetMailThreadList);
            }
            RemoteAsyncDelegate_GetMailThreadList list = new RemoteAsyncDelegate_GetMailThreadList(this.service.GetMailThreadList);
            this.registerRPCcall(list.BeginInvoke(this.UserID, this.SessionID, initialRequest, mode, lastRetrieved, this.GetMailThreadList_Callback, null), typeof(GetMailThreadList_ReturnType));
        }

        public void GetMailUserSearch(string filter)
        {
            if (this.GetMailUserSearch_Callback == null)
            {
                this.GetMailUserSearch_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetMailUserSearch);
            }
            RemoteAsyncDelegate_GetMailUserSearch search = new RemoteAsyncDelegate_GetMailUserSearch(this.service.GetMailUserSearch);
            this.registerRPCcall(search.BeginInvoke(this.UserID, this.SessionID, filter, this.GetMailUserSearch_Callback, null), typeof(GetMailUserSearch_ReturnType));
        }

        public void GetOtherUserVillageIDList(string targetUser)
        {
            if (this.GetOtherUserVillageIDList_Callback == null)
            {
                this.GetOtherUserVillageIDList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetOtherUserVillageIDList);
            }
            RemoteAsyncDelegate_GetOtherUserVillageIDList list = new RemoteAsyncDelegate_GetOtherUserVillageIDList(this.service.GetOtherUserVillageIDList);
            this.registerRPCcall(list.BeginInvoke(this.UserID, targetUser, this.SessionID, this.GetOtherUserVillageIDList_Callback, null), typeof(GetOtherUserVillageIDList_ReturnType));
        }

        public void GetParishFrontPageInfo(int villageID, DateTime lastTime)
        {
            if (this.GetParishFrontPageInfo_Callback == null)
            {
                this.GetParishFrontPageInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetParishFrontPageInfo);
            }
            RemoteAsyncDelegate_GetParishFrontPageInfo info = new RemoteAsyncDelegate_GetParishFrontPageInfo(this.service.GetParishFrontPageInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, lastTime, this.GetParishFrontPageInfo_Callback, null), typeof(GetParishFrontPageInfo_ReturnType));
        }

        public void GetParishMembersList(int villageID)
        {
            if (this.GetParishMembersList_Callback == null)
            {
                this.GetParishMembersList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetParishMembersList);
            }
            RemoteAsyncDelegate_GetParishMembersList list = new RemoteAsyncDelegate_GetParishMembersList(this.service.GetParishMembersList);
            this.registerRPCcall(list.BeginInvoke(this.UserID, this.SessionID, villageID, this.GetParishMembersList_Callback, null), typeof(GetParishMembersList_ReturnType));
        }

        public void GetPreVassalInfo(int villageID, int targetVillage)
        {
            if (this.GetPreVassalInfo_Callback == null)
            {
                this.GetPreVassalInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetPreVassalInfo);
            }
            RemoteAsyncDelegate_GetPreVassalInfo info = new RemoteAsyncDelegate_GetPreVassalInfo(this.service.GetPreVassalInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, targetVillage, this.GetPreVassalInfo_Callback, null), typeof(GetPreVassalInfo_ReturnType));
        }

        public void GetProvinceElectionInfo(int villageID)
        {
            if (this.GetProvinceElectionInfo_Callback == null)
            {
                this.GetProvinceElectionInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetProvinceElectionInfo);
            }
            RemoteAsyncDelegate_GetProvinceElectionInfo info = new RemoteAsyncDelegate_GetProvinceElectionInfo(this.service.GetProvinceElectionInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, this.GetProvinceElectionInfo_Callback, null), typeof(GetProvinceElectionInfo_ReturnType));
        }

        public void GetProvinceFrontPageInfo(int villageID)
        {
            if (this.GetProvinceFrontPageInfo_Callback == null)
            {
                this.GetProvinceFrontPageInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetProvinceFrontPageInfo);
            }
            RemoteAsyncDelegate_GetProvinceFrontPageInfo info = new RemoteAsyncDelegate_GetProvinceFrontPageInfo(this.service.GetProvinceFrontPageInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, this.GetProvinceFrontPageInfo_Callback, null), typeof(GetProvinceFrontPageInfo_ReturnType));
        }

        public void GetQuestData(bool full)
        {
            if (this.GetQuestData_Callback == null)
            {
                this.GetQuestData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetQuestData);
            }
            RemoteAsyncDelegate_GetQuestData data = new RemoteAsyncDelegate_GetQuestData(this.service.GetQuestData);
            this.registerRPCcall(data.BeginInvoke(this.UserID, this.SessionID, full, this.GetQuestData_Callback, null), typeof(GetQuestData_ReturnType));
        }

        public void GetQuestStatus()
        {
            if (this.GetQuestStatus_Callback == null)
            {
                this.GetQuestStatus_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetQuestStatus);
            }
            RemoteAsyncDelegate_GetQuestStatus status = new RemoteAsyncDelegate_GetQuestStatus(this.service.GetQuestStatus);
            this.registerRPCcall(status.BeginInvoke(this.UserID, this.SessionID, this.GetQuestStatus_Callback, null), typeof(GetQuestStatus_ReturnType));
        }

        public void GetReport(long reportID)
        {
            if (this.GetReport_Callback == null)
            {
                this.GetReport_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetReport);
            }
            RemoteAsyncDelegate_GetReport report = new RemoteAsyncDelegate_GetReport(this.service.GetReport);
            this.registerRPCcall(report.BeginInvoke(this.UserID, this.SessionID, reportID, this.GetReport_Callback, null), typeof(GetReport_ReturnType));
        }

        public void getReportFolders()
        {
            if (this.ManageReportFolders_Callback == null)
            {
                this.ManageReportFolders_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ManageReportFolders);
            }
            RemoteAsyncDelegate_ManageReportFolders folders = new RemoteAsyncDelegate_ManageReportFolders(this.service.ManageReportFolders);
            this.registerRPCcall(folders.BeginInvoke(this.UserID, this.SessionID, 0, 0L, "", this.ManageReportFolders_Callback, null), typeof(ManageReportFolders_ReturnType));
        }

        public void GetReportsList(int readFilter, long clientHighest)
        {
            if (this.GetReportsList_Callback == null)
            {
                this.GetReportsList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetReportsList);
            }
            RemoteAsyncDelegate_GetReportsList list = new RemoteAsyncDelegate_GetReportsList(this.service.GetReportsList);
            this.registerRPCcall(list.BeginInvoke(this.UserID, this.SessionID, readFilter, null, -1L, clientHighest, this.GetReportsList_Callback, null), typeof(GetReportsList_ReturnType));
        }

        public void GetReportsList(int readFilter, int[] typeFilters, long folderID)
        {
            if (this.GetReportsList_Callback == null)
            {
                this.GetReportsList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetReportsList);
            }
            RemoteAsyncDelegate_GetReportsList list = new RemoteAsyncDelegate_GetReportsList(this.service.GetReportsList);
            this.registerRPCcall(list.BeginInvoke(this.UserID, this.SessionID, readFilter, typeFilters, folderID, -1L, this.GetReportsList_Callback, null), typeof(GetReportsList_ReturnType));
        }

        public void GetResearchData()
        {
            if (this.GetResearchData_Callback == null)
            {
                this.GetResearchData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetResearchData);
            }
            RemoteAsyncDelegate_GetResearchData data = new RemoteAsyncDelegate_GetResearchData(this.service.GetResearchData);
            this.registerRPCcall(data.BeginInvoke(this.UserID, this.SessionID, this.GetResearchData_Callback, null), typeof(GetResearchData_ReturnType));
        }

        public void GetResourceLevel(int villageID, int buildingType)
        {
            if (this.GetResourceLevel_Callback == null)
            {
                this.GetResourceLevel_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetResourceLevel);
            }
            RemoteAsyncDelegate_GetResourceLevel level = new RemoteAsyncDelegate_GetResourceLevel(this.service.GetResourceLevel);
            this.registerRPCcall(level.BeginInvoke(this.UserID, this.SessionID, villageID, buildingType, this.GetResourceLevel_Callback, null), typeof(GetResourceLevel_ReturnType));
        }

        public void GetStockExchangeData(int villageID, bool stockExchange)
        {
            if (this.GetStockExchangeData_Callback == null)
            {
                this.GetStockExchangeData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetStockExchangeData);
            }
            RemoteAsyncDelegate_GetStockExchangeData data = new RemoteAsyncDelegate_GetStockExchangeData(this.service.GetStockExchangeData);
            this.registerRPCcall(data.BeginInvoke(this.UserID, this.SessionID, villageID, stockExchange, null, this.GetStockExchangeData_Callback, null), typeof(GetStockExchangeData_ReturnType));
        }

        public void GetStockExchangePremiumData(int villageID, int[] closeVillages)
        {
            if (this.GetStockExchangeData_Callback == null)
            {
                this.GetStockExchangeData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetStockExchangeData);
            }
            RemoteAsyncDelegate_GetStockExchangeData data = new RemoteAsyncDelegate_GetStockExchangeData(this.service.GetStockExchangeData);
            this.registerRPCcall(data.BeginInvoke(this.UserID, this.SessionID, villageID, true, closeVillages, this.GetStockExchangeData_Callback, null), typeof(GetStockExchangeData_ReturnType));
        }

        public void GetUserIDFromName(string targetUser)
        {
            if (this.GetUserIDFromName_Callback == null)
            {
                this.GetUserIDFromName_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetUserIDFromName);
            }
            RemoteAsyncDelegate_GetUserIDFromName name = new RemoteAsyncDelegate_GetUserIDFromName(this.service.GetUserIDFromName);
            this.registerRPCcall(name.BeginInvoke(this.UserID, this.SessionID, targetUser, this.GetUserIDFromName_Callback, null), typeof(GetUserIDFromName_ReturnType));
        }

        public void GetUserPeople()
        {
            if (this.GetUserPeople_Callback == null)
            {
                this.GetUserPeople_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetUserPeople);
            }
            RemoteAsyncDelegate_GetUserPeople people = new RemoteAsyncDelegate_GetUserPeople(this.service.GetUserPeople);
            this.registerRPCcall(people.BeginInvoke(this.UserID, this.SessionID, this.GetUserPeople_Callback, null), typeof(GetUserPeople_ReturnType));
        }

        public void GetUserTraders()
        {
            if (this.GetUserTraders_Callback == null)
            {
                this.GetUserTraders_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetUserTraders);
            }
            RemoteAsyncDelegate_GetUserTraders traders = new RemoteAsyncDelegate_GetUserTraders(this.service.GetUserTraders);
            this.registerRPCcall(traders.BeginInvoke(this.UserID, this.SessionID, -1, this.GetUserTraders_Callback, null), typeof(GetUserTraders_ReturnType));
        }

        public void GetUserTraders(int villageID)
        {
            if (this.GetUserTraders_Callback == null)
            {
                this.GetUserTraders_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetUserTraders);
            }
            RemoteAsyncDelegate_GetUserTraders traders = new RemoteAsyncDelegate_GetUserTraders(this.service.GetUserTraders);
            this.registerRPCcall(traders.BeginInvoke(this.UserID, this.SessionID, villageID, this.GetUserTraders_Callback, null), typeof(GetUserTraders_ReturnType));
        }

        public void GetUserVillages()
        {
            if (this.GetUserVillages_Callback == null)
            {
                this.GetUserVillages_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetUserVillages);
            }
            RemoteAsyncDelegate_GetUserVillages villages = new RemoteAsyncDelegate_GetUserVillages(this.service.GetUserVillages);
            this.registerRPCcall(villages.BeginInvoke(this.UserID, this.SessionID, this.GetUserVillages_Callback, null), typeof(GetUserVillages_ReturnType));
        }

        public void GetVassalArmyInfo(int vassalVillageID, int mode, int attackedVillage)
        {
            if (this.GetVassalArmyInfo_Callback == null)
            {
                this.GetVassalArmyInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetVassalArmyInfo);
            }
            RemoteAsyncDelegate_GetVassalArmyInfo info = new RemoteAsyncDelegate_GetVassalArmyInfo(this.service.GetVassalArmyInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, vassalVillageID, mode, attackedVillage, this.GetVassalArmyInfo_Callback, null), typeof(GetVassalArmyInfo_ReturnType));
        }

        public void GetViewFactionData(int factionID)
        {
            if (this.GetViewFactionData_Callback == null)
            {
                this.GetViewFactionData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetViewFactionData);
            }
            RemoteAsyncDelegate_GetViewFactionData data = new RemoteAsyncDelegate_GetViewFactionData(this.service.GetViewFactionData);
            this.registerRPCcall(data.BeginInvoke(this.UserID, this.SessionID, factionID, this.GetViewFactionData_Callback, null), typeof(GetViewFactionData_ReturnType));
        }

        public void GetViewHouseData(int houseID)
        {
            if (this.GetViewHouseData_Callback == null)
            {
                this.GetViewHouseData_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetViewHouseData);
            }
            RemoteAsyncDelegate_GetViewHouseData data = new RemoteAsyncDelegate_GetViewHouseData(this.service.GetViewHouseData);
            this.registerRPCcall(data.BeginInvoke(this.UserID, this.SessionID, houseID, this.GetViewHouseData_Callback, null), typeof(GetViewHouseData_ReturnType));
        }

        public void GetVillageBuildingsList(int villageID, bool fullUpdate, bool needParishPeople)
        {
            if (this.GetVillageBuildingsList_Callback == null)
            {
                this.GetVillageBuildingsList_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetVillageBuildingsList);
            }
            RemoteAsyncDelegate_GetVillageBuildingsList list = new RemoteAsyncDelegate_GetVillageBuildingsList(this.service.GetVillageBuildingsList);
            this.registerRPCcall(list.BeginInvoke(this.UserID, this.SessionID, villageID, fullUpdate, false, needParishPeople, this.GetVillageBuildingsList_Callback, null), typeof(GetVillageBuildingsList_ReturnType));
        }

        public void GetVillageFactionChanges(long startChangePos, long factionsChangePos)
        {
            if (startChangePos < -1L)
            {
                startChangePos = -1L;
            }
            if (factionsChangePos < -1L)
            {
                factionsChangePos = -1L;
            }
            if (this.GetVillageFactionChanges_Callback == null)
            {
                this.GetVillageFactionChanges_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetVillageFactionChanges);
            }
            RemoteAsyncDelegate_GetVillageFactionChanges changes = new RemoteAsyncDelegate_GetVillageFactionChanges(this.service.GetVillageFactionChanges);
            this.GetVillageFactionChanges_ValidDownload = false;
            this.GetVillageFactionChanges_Index++;
            this.registerRPCcall(changes.BeginInvoke(this.UserID, this.SessionID, startChangePos, factionsChangePos, this.GetVillageFactionChanges_Index, this.GetVillageFactionChanges_Callback, null), typeof(GetVillageFactionChanges_ReturnType));
        }

        public void GetVillageInfoForDonateCapitalGoods(int parishCapitalID, int targetBuildingType)
        {
            if (this.GetVillageInfoForDonateCapitalGoods_Callback == null)
            {
                this.GetVillageInfoForDonateCapitalGoods_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetVillageInfoForDonateCapitalGoods);
            }
            RemoteAsyncDelegate_GetVillageInfoForDonateCapitalGoods goods = new RemoteAsyncDelegate_GetVillageInfoForDonateCapitalGoods(this.service.GetVillageInfoForDonateCapitalGoods);
            this.registerRPCcall(goods.BeginInvoke(this.UserID, this.SessionID, parishCapitalID, targetBuildingType, this.GetVillageInfoForDonateCapitalGoods_Callback, null), typeof(GetVillageInfoForDonateCapitalGoods_ReturnType));
        }

        public void GetVillageNames(long changePos)
        {
            if (this.GetVillageNames_Callback == null)
            {
                this.GetVillageNames_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetVillageNames);
            }
            RemoteAsyncDelegate_GetVillageNames names = new RemoteAsyncDelegate_GetVillageNames(this.service.GetVillageNames);
            this.GetVillageNames_ValidDownload = false;
            this.GetVillageNames_Index++;
            this.registerRPCcall(names.BeginInvoke(this.UserID, this.SessionID, changePos, this.GetVillageNames_Index, this.GetVillageNames_Callback, null), typeof(GetVillageNames_ReturnType));
        }

        public void GetVillageRankTaxTree()
        {
            if (this.GetVillageRankTaxTree_Callback == null)
            {
                this.GetVillageRankTaxTree_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetVillageRankTaxTree);
            }
            RemoteAsyncDelegate_GetVillageRankTaxTree tree = new RemoteAsyncDelegate_GetVillageRankTaxTree(this.service.GetVillageRankTaxTree);
            this.registerRPCcall(tree.BeginInvoke(this.UserID, this.SessionID, this.GetVillageRankTaxTree_Callback, null), typeof(GetVillageRankTaxTree_ReturnType));
        }

        public void GetVillageStartLocations()
        {
            if (this.GetVillageStartLocations_Callback == null)
            {
                this.GetVillageStartLocations_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GetVillageStartLocations);
            }
            RemoteAsyncDelegate_GetVillageStartLocations locations = new RemoteAsyncDelegate_GetVillageStartLocations(this.service.GetVillageStartLocations);
            this.registerRPCcall(locations.BeginInvoke(this.UserID, this.SessionID, this.GetVillageStartLocations_Callback, null), typeof(GetVillageStartLocations_ReturnType));
        }

        public void GiveForumAccess(long forumID, int[] users)
        {
            if (this.GiveForumAccess_Callback == null)
            {
                this.GiveForumAccess_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_GiveForumAccess);
            }
            RemoteAsyncDelegate_GiveForumAccess access = new RemoteAsyncDelegate_GiveForumAccess(this.service.GiveForumAccess);
            this.registerRPCcall(access.BeginInvoke(this.UserID, this.SessionID, forumID, users, this.GiveForumAccess_Callback, null), typeof(GiveForumAccess_ReturnType));
        }

        public void HouseVote(int targetFaction, bool application, bool vote, long factionsChangePos)
        {
            FactionData yourFaction = GameEngine.Instance.World.YourFaction;
            if (yourFaction != null)
            {
                if (this.HouseVote_Callback == null)
                {
                    this.HouseVote_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_HouseVote);
                }
                RemoteAsyncDelegate_HouseVote vote2 = new RemoteAsyncDelegate_HouseVote(this.service.HouseVote);
                this.registerRPCcall(vote2.BeginInvoke(this.UserID, this.SessionID, yourFaction.factionID, yourFaction.houseID, targetFaction, application, vote, factionsChangePos, this.HouseVote_Callback, null), typeof(HouseVote_ReturnType));
            }
        }

        public void HouseVoteHouseLeader(int factionID, int houseID, int votedFactionID, long factionsChangePos)
        {
            if (this.HouseVoteHouseLeader_Callback == null)
            {
                this.HouseVoteHouseLeader_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_HouseVoteHouseLeader);
            }
            RemoteAsyncDelegate_HouseVoteHouseLeader leader = new RemoteAsyncDelegate_HouseVoteHouseLeader(this.service.HouseVoteHouseLeader);
            this.registerRPCcall(leader.BeginInvoke(this.UserID, this.SessionID, factionID, houseID, votedFactionID, factionsChangePos, this.HouseVoteHouseLeader_Callback, null), typeof(HouseVoteHouseLeader_ReturnType));
        }

        public void init(string remotePath)
        {
            this.connectionErrored = false;
            this.clearQueues();
            this.initChannel();
            this.service = (IService) Activator.GetObject(typeof(IService), remotePath);
            ChannelServices.GetChannelSinkProperties(this.service)["credentials"] = CredentialCache.DefaultCredentials;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 1;
            this.chatActive = true;
        }

        public void initChannel()
        {
            if (this.channel == null)
            {
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.MaxServicePointIdleTime = 1;
                ListDictionary properties = new ListDictionary();
                BinaryClientFormatterSinkProvider clientSinkProvider = new BinaryClientFormatterSinkProvider();
                ListDictionary dictionary2 = new ListDictionary();
                ListDictionary providerData = new ListDictionary();
                dictionary2.Add("customSinkType", "CompressedSink.CompressedClientSink, CustomSinks");
                clientSinkProvider.Next = new CustomClientSinkProvider(dictionary2, providerData);
                this.channel = new HttpChannel(properties, clientSinkProvider, null);
                ChannelServices.RegisterChannel(this.channel, false);
            }
        }

        public void InitialiseFreeCards()
        {
            if (this.InitialiseFreeCards_Callback == null)
            {
                this.InitialiseFreeCards_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_InitialiseFreeCards);
            }
            RemoteAsyncDelegate_InitialiseFreeCards cards = new RemoteAsyncDelegate_InitialiseFreeCards(this.service.InitialiseFreeCards);
            this.registerRPCcall(cards.BeginInvoke(this.UserID, this.SessionID, this.InitialiseFreeCards_Callback, null), typeof(InitialiseFreeCards_ReturnType));
        }

        private bool isDataNotNull(object data)
        {
            return (data != null);
        }

        public void LaunchCastleAttack(int parentOfAttackingVillageID, int sourceVillageID, int targetVillageID, byte[] troopMap, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int attackType, int pillagePercent, int CaptainsCommand, int numCaptains)
        {
            if (this.LaunchCastleAttack_Callback == null)
            {
                this.LaunchCastleAttack_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LaunchCastleAttack);
            }
            RemoteAsyncDelegate_LaunchCastleAttack attack = new RemoteAsyncDelegate_LaunchCastleAttack(this.service.LaunchCastleAttack);
            this.registerRPCcall(attack.BeginInvoke(this.UserID, this.SessionID, parentOfAttackingVillageID, targetVillageID, sourceVillageID, troopMap, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, attackType, pillagePercent, CaptainsCommand, numCaptains, this.LaunchCastleAttack_Callback, null), typeof(LaunchCastleAttack_ReturnType));
        }

        public void LeaderBoard(int mode)
        {
            if (this.LeaderBoard_Callback == null)
            {
                this.LeaderBoard_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LeaderBoard);
            }
            RemoteAsyncDelegate_LeaderBoard board = new RemoteAsyncDelegate_LeaderBoard(this.service.LeaderBoard);
            this.registerRPCcall(board.BeginInvoke(this.UserID, this.SessionID, mode, -1, -1, DateTime.MinValue, this.LeaderBoard_Callback, null), typeof(LeaderBoard_ReturnType));
        }

        public void LeaderBoard(int mode, int minValue, int maxValue, DateTime lastUpdate)
        {
            if (this.LeaderBoard_Callback == null)
            {
                this.LeaderBoard_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LeaderBoard);
            }
            RemoteAsyncDelegate_LeaderBoard board = new RemoteAsyncDelegate_LeaderBoard(this.service.LeaderBoard);
            this.registerRPCcall(board.BeginInvoke(this.UserID, this.SessionID, mode, minValue, maxValue, lastUpdate, this.LeaderBoard_Callback, null), typeof(LeaderBoard_ReturnType));
        }

        public void LeaderBoardSearch(int mode, string searchString, DateTime lastUpdate)
        {
            if (this.LeaderBoardSearch_Callback == null)
            {
                this.LeaderBoardSearch_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LeaderBoardSearch);
            }
            RemoteAsyncDelegate_LeaderBoardSearch search = new RemoteAsyncDelegate_LeaderBoardSearch(this.service.LeaderBoardSearch);
            this.registerRPCcall(search.BeginInvoke(this.UserID, this.SessionID, mode, searchString, lastUpdate, this.LeaderBoardSearch_Callback, null), typeof(LeaderBoardSearch_ReturnType));
        }

        public void LeaveHouse(int factionID, int houseID, long factionsChangePos)
        {
            if (this.LeaveHouse_Callback == null)
            {
                this.LeaveHouse_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LeaveHouse);
            }
            RemoteAsyncDelegate_LeaveHouse house = new RemoteAsyncDelegate_LeaveHouse(this.service.LeaveHouse);
            this.registerRPCcall(house.BeginInvoke(this.UserID, this.SessionID, factionID, houseID, factionsChangePos, this.LeaveHouse_Callback, null), typeof(LeaveHouse_ReturnType));
        }

        public void LoginUser(string username, string password, string verificationString)
        {
            bool needVillageData = true;
            if (VillageMap.villageBuildingData != null)
            {
                needVillageData = false;
            }
            if (this.LoginUser_Callback == null)
            {
                this.LoginUser_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LoginUser);
            }
            RemoteAsyncDelegate_LoginUser user = new RemoteAsyncDelegate_LoginUser(this.service.LoginUser);
            this.registerRPCcall(user.BeginInvoke(username, password, BuildVersion.VersionNumber, verificationString, needVillageData, this.LoginUser_Callback, null), typeof(LoginUser_ReturnType));
        }

        public void LoginUserGuid(string username, Guid userGuid, Guid sessionGuid)
        {
            bool needVillageData = true;
            if (VillageMap.villageBuildingData != null)
            {
                needVillageData = false;
            }
            if (this.LoginUserGuid_Callback == null)
            {
                this.LoginUserGuid_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LoginUserGuid);
            }
            RemoteAsyncDelegate_LoginUserGuid guid = new RemoteAsyncDelegate_LoginUserGuid(this.service.LoginUserGuid);
            this.registerRPCcall(guid.BeginInvoke(username, userGuid.ToString(), sessionGuid.ToString(), needVillageData, BuildVersion.VersionNumber, this.LoginUserGuid_Callback, null), typeof(LoginUserGuid_ReturnType));
        }

        public void LogOut(bool manual, bool autoScout, bool autoTrade, bool autoAttack, bool autoAttackWolf, bool autoAttackBandit, bool autoAttackAI, int resourceType, int percent, bool autoRecruit, bool autoRecruitPeasant, bool autoRecruitArchers, bool autoRecruitPikemen, bool autoRecruitSwordsmen, bool autoRecruitCatapults, int autoRecruitPeasant_Cap, int autoRecruitArchers_Cap, int autoRecruitPikemen_Cap, int autoRecruitSwordsmen_Cap, int autoRecruitCatapults_Cap)
        {
            if (this.service != null)
            {
                if (this.LogOut_Callback == null)
                {
                    this.LogOut_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_LogOut);
                }
                RemoteAsyncDelegate_LogOut @out = new RemoteAsyncDelegate_LogOut(this.service.LogOut);
                this.registerRPCcall(@out.BeginInvoke(this.UserID, this.SessionID, manual, autoScout, autoTrade, autoAttack, autoAttackWolf, autoAttackBandit, autoAttackAI, resourceType, percent, autoRecruit, autoRecruitPeasant, autoRecruitArchers, autoRecruitPikemen, autoRecruitSwordsmen, autoRecruitCatapults, autoRecruitPeasant_Cap, autoRecruitArchers_Cap, autoRecruitPikemen_Cap, autoRecruitSwordsmen_Cap, autoRecruitCatapults_Cap, this.LogOut_Callback, null), typeof(LogOut_ReturnType));
            }
        }

        public void MakeCountryVote(int villageID, int votedParishID)
        {
            if (this.MakeCountryVote_Callback == null)
            {
                this.MakeCountryVote_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MakeCountryVote);
            }
            RemoteAsyncDelegate_MakeCountryVote vote = new RemoteAsyncDelegate_MakeCountryVote(this.service.MakeCountryVote);
            this.registerRPCcall(vote.BeginInvoke(this.UserID, this.SessionID, villageID, votedParishID, this.MakeCountryVote_Callback, null), typeof(MakeCountryVote_ReturnType));
        }

        public void MakeCountyVote(int villageID, int votedParishID)
        {
            if (this.MakeCountyVote_Callback == null)
            {
                this.MakeCountyVote_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MakeCountyVote);
            }
            RemoteAsyncDelegate_MakeCountyVote vote = new RemoteAsyncDelegate_MakeCountyVote(this.service.MakeCountyVote);
            this.registerRPCcall(vote.BeginInvoke(this.UserID, this.SessionID, villageID, votedParishID, this.MakeCountyVote_Callback, null), typeof(MakeCountyVote_ReturnType));
        }

        public void MakeParishVote(int villageID, int votedUserID)
        {
            if (this.MakeParishVote_Callback == null)
            {
                this.MakeParishVote_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MakeParishVote);
            }
            RemoteAsyncDelegate_MakeParishVote vote = new RemoteAsyncDelegate_MakeParishVote(this.service.MakeParishVote);
            this.registerRPCcall(vote.BeginInvoke(this.UserID, this.SessionID, villageID, votedUserID, this.MakeParishVote_Callback, null), typeof(MakeParishVote_ReturnType));
        }

        public void MakePeople(int villageID, int personType)
        {
            if (this.MakePeople_Callback == null)
            {
                this.MakePeople_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MakePeople);
            }
            RemoteAsyncDelegate_MakePeople people = new RemoteAsyncDelegate_MakePeople(this.service.MakePeople);
            this.registerRPCcall(people.BeginInvoke(this.UserID, this.SessionID, villageID, personType, this.MakePeople_Callback, null), typeof(MakePeople_ReturnType));
        }

        public void MakeProvinceVote(int villageID, int votedParishID)
        {
            if (this.MakeProvinceVote_Callback == null)
            {
                this.MakeProvinceVote_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MakeProvinceVote);
            }
            RemoteAsyncDelegate_MakeProvinceVote vote = new RemoteAsyncDelegate_MakeProvinceVote(this.service.MakeProvinceVote);
            this.registerRPCcall(vote.BeginInvoke(this.UserID, this.SessionID, villageID, votedParishID, this.MakeProvinceVote_Callback, null), typeof(MakeProvinceVote_ReturnType));
        }

        public void MakeTroop(int villageID, int troopType, int amount)
        {
            if (this.MakeTroop_Callback == null)
            {
                this.MakeTroop_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MakeTroop);
            }
            RemoteAsyncDelegate_MakeTroop troop = new RemoteAsyncDelegate_MakeTroop(this.service.MakeTroop);
            this.registerRPCcall(troop.BeginInvoke(this.UserID, this.SessionID, villageID, troopType, amount, this.MakeTroop_Callback, null), typeof(MakeTroop_ReturnType));
        }

        private void manageRemoteExpection(IAsyncResult ar, Common_ReturnData returnData, Exception e)
        {
            if (e.GetType() == typeof(WebException))
            {
                GameEngine.Instance.connectionErrorString = e.Message + "\n" + e.ToString();
                returnData.m_errorCode = CommonTypes.ErrorCodes.ErrorCode.CONNECTION_NO_SERVER;
                returnData.SetAsFailed();
                this.storeRPCresult(ar, returnData);
                this.connectionErrored = true;
            }
        }

        public void MarkReportsRead(long[] reportsToMark)
        {
            if (this.DeleteReports_Callback == null)
            {
                this.DeleteReports_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteReports);
            }
            RemoteAsyncDelegate_DeleteReports reports = new RemoteAsyncDelegate_DeleteReports(this.service.DeleteOrMoveReports);
            this.registerRPCcall(reports.BeginInvoke(this.UserID, this.SessionID, 2, reportsToMark, -1L, this.DeleteReports_Callback, null), typeof(DeleteReports_ReturnType));
        }

        public void MemorizeCastleTroops(int villageID)
        {
            if (this.MemorizeCastleTroops_Callback == null)
            {
                this.MemorizeCastleTroops_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MemorizeCastleTroops);
            }
            RemoteAsyncDelegate_MemorizeCastleTroops troops = new RemoteAsyncDelegate_MemorizeCastleTroops(this.service.MemorizeCastleTroops);
            this.registerRPCcall(troops.BeginInvoke(this.UserID, this.SessionID, villageID, this.MemorizeCastleTroops_Callback, null), typeof(MemorizeCastleTroops_ReturnType));
        }

        public void MoveReports(long[] reportsToDelete, long folderID)
        {
            if (this.DeleteReports_Callback == null)
            {
                this.DeleteReports_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_DeleteReports);
            }
            RemoteAsyncDelegate_DeleteReports reports = new RemoteAsyncDelegate_DeleteReports(this.service.DeleteOrMoveReports);
            this.registerRPCcall(reports.BeginInvoke(this.UserID, this.SessionID, 1, reportsToDelete, folderID, this.DeleteReports_Callback, null), typeof(DeleteReports_ReturnType));
        }

        public void MoveToMailFolder(long threadID, long folderID)
        {
            if (this.MoveToMailFolder_Callback == null)
            {
                this.MoveToMailFolder_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MoveToMailFolder);
            }
            RemoteAsyncDelegate_MoveToMailFolder folder = new RemoteAsyncDelegate_MoveToMailFolder(this.service.MoveToMailFolder);
            this.registerRPCcall(folder.BeginInvoke(this.UserID, this.SessionID, threadID, folderID, this.MoveToMailFolder_Callback, null), typeof(MoveToMailFolder_ReturnType));
        }

        public void MoveVillageBuilding(int villageID, long buildingID, Point buildingLocation)
        {
            if (this.MoveVillageBuilding_Callback == null)
            {
                this.MoveVillageBuilding_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_MoveVillageBuilding);
            }
            RemoteAsyncDelegate_MoveVillageBuilding building = new RemoteAsyncDelegate_MoveVillageBuilding(this.service.MoveVillageBuilding);
            this.registerRPCcall(building.BeginInvoke(this.UserID, this.SessionID, villageID, buildingID, buildingLocation, this.MoveVillageBuilding_Callback, null), typeof(MoveVillageBuilding_ReturnType));
        }

        public void NewForumThread(long forumID, string headingText, string bodyText)
        {
            if (this.NewForumThread_Callback == null)
            {
                this.NewForumThread_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_NewForumThread);
            }
            RemoteAsyncDelegate_NewForumThread thread = new RemoteAsyncDelegate_NewForumThread(this.service.NewForumThread);
            this.registerRPCcall(thread.BeginInvoke(this.UserID, this.SessionID, forumID, headingText, bodyText, this.NewForumThread_Callback, null), typeof(NewForumThread_ReturnType));
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_AchievementProgress(IAsyncResult ar)
        {
            RemoteAsyncDelegate_AchievementProgress asyncDelegate = (RemoteAsyncDelegate_AchievementProgress) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                AchievementProgress_ReturnType returnData = new AchievementProgress_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_AddCastleElement(IAsyncResult ar)
        {
            RemoteAsyncDelegate_AddCastleElement asyncDelegate = (RemoteAsyncDelegate_AddCastleElement) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                AddCastleElement_ReturnType returnData = new AddCastleElement_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_AddUserToFavourites(IAsyncResult ar)
        {
            RemoteAsyncDelegate_AddUserToFavourites asyncDelegate = (RemoteAsyncDelegate_AddUserToFavourites) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                AddUserToFavourites_ReturnType returnData = new AddUserToFavourites_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_ArmyAttack(IAsyncResult ar)
        {
            RemoteAsyncDelegate_ArmyAttack asyncDelegate = (RemoteAsyncDelegate_ArmyAttack) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                ArmyAttack_ReturnType returnData = new ArmyAttack_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_AutoRepairCastle(IAsyncResult ar)
        {
            RemoteAsyncDelegate_AutoRepairCastle asyncDelegate = (RemoteAsyncDelegate_AutoRepairCastle) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                AutoRepairCastle_ReturnType returnData = new AutoRepairCastle_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_BreakLiegeLord(IAsyncResult ar)
        {
            RemoteAsyncDelegate_BreakLiegeLord asyncDelegate = (RemoteAsyncDelegate_BreakLiegeLord) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                BreakLiegeLord_ReturnType returnData = new BreakLiegeLord_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_BreakVassalage(IAsyncResult ar)
        {
            RemoteAsyncDelegate_BreakVassalage asyncDelegate = (RemoteAsyncDelegate_BreakVassalage) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                BreakVassalage_ReturnType returnData = new BreakVassalage_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_BuyResearchPoint(IAsyncResult ar)
        {
            RemoteAsyncDelegate_BuyResearchPoint asyncDelegate = (RemoteAsyncDelegate_BuyResearchPoint) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                BuyResearchPoint_ReturnType returnData = new BuyResearchPoint_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_BuyVillage(IAsyncResult ar)
        {
            RemoteAsyncDelegate_BuyVillage asyncDelegate = (RemoteAsyncDelegate_BuyVillage) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                BuyVillage_ReturnType returnData = new BuyVillage_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CancelCard(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CancelCard asyncDelegate = (RemoteAsyncDelegate_CancelCard) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CancelCard_ReturnType returnData = new CancelCard_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CancelCastleAttack(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CancelCastleAttack asyncDelegate = (RemoteAsyncDelegate_CancelCastleAttack) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CancelCastleAttack_ReturnType returnData = new CancelCastleAttack_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CancelDeleteVillageBuilding(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CancelDeleteVillageBuilding asyncDelegate = (RemoteAsyncDelegate_CancelDeleteVillageBuilding) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CancelDeleteVillageBuilding_ReturnType returnData = new CancelDeleteVillageBuilding_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CancelInterdiction(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CancelInterdiction asyncDelegate = (RemoteAsyncDelegate_CancelInterdiction) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CancelInterdiction_ReturnType returnData = new CancelInterdiction_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_ChangeCastleElementAggressiveDefender(IAsyncResult ar)
        {
            RemoteAsyncDelegate_ChangeCastleElementAggressiveDefender asyncDelegate = (RemoteAsyncDelegate_ChangeCastleElementAggressiveDefender) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                ChangeCastleElementAggressiveDefender_ReturnType returnData = new ChangeCastleElementAggressiveDefender_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_ChangeFactionMotto(IAsyncResult ar)
        {
            RemoteAsyncDelegate_ChangeFactionMotto asyncDelegate = (RemoteAsyncDelegate_ChangeFactionMotto) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                ChangeFactionMotto_ReturnType returnData = new ChangeFactionMotto_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_Chat_Admin_Command(IAsyncResult ar)
        {
            RemoteAsyncDelegate_Chat_Admin_Command asyncDelegate = (RemoteAsyncDelegate_Chat_Admin_Command) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                Chat_Admin_Command_ReturnType returnData = new Chat_Admin_Command_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_Chat_BackFillParishText(IAsyncResult ar)
        {
            RemoteAsyncDelegate_Chat_BackFillParishText asyncDelegate = (RemoteAsyncDelegate_Chat_BackFillParishText) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                Chat_BackFillParishText_ReturnType returnData = new Chat_BackFillParishText_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_Chat_Login(IAsyncResult ar)
        {
            RemoteAsyncDelegate_Chat_Login asyncDelegate = (RemoteAsyncDelegate_Chat_Login) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                Chat_Login_ReturnType returnData = new Chat_Login_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_Chat_Logout(IAsyncResult ar)
        {
            RemoteAsyncDelegate_Chat_Logout asyncDelegate = (RemoteAsyncDelegate_Chat_Logout) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                Chat_Logout_ReturnType returnData = new Chat_Logout_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_Chat_MarkParishTextRead(IAsyncResult ar)
        {
            RemoteAsyncDelegate_Chat_MarkParishTextRead asyncDelegate = (RemoteAsyncDelegate_Chat_MarkParishTextRead) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                Chat_MarkParishTextRead_ReturnType returnData = new Chat_MarkParishTextRead_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_Chat_ReceiveParishText(IAsyncResult ar)
        {
            RemoteAsyncDelegate_Chat_ReceiveParishText asyncDelegate = (RemoteAsyncDelegate_Chat_ReceiveParishText) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                Chat_ReceiveParishText_ReturnType returnData = new Chat_ReceiveParishText_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_Chat_ReceiveText(IAsyncResult ar)
        {
            RemoteAsyncDelegate_Chat_ReceiveText asyncDelegate = (RemoteAsyncDelegate_Chat_ReceiveText) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                Chat_ReceiveText_ReturnType returnData = new Chat_ReceiveText_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_Chat_SendParishText(IAsyncResult ar)
        {
            RemoteAsyncDelegate_Chat_SendParishText asyncDelegate = (RemoteAsyncDelegate_Chat_SendParishText) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                Chat_SendParishText_ReturnType returnData = new Chat_SendParishText_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_Chat_SendText(IAsyncResult ar)
        {
            RemoteAsyncDelegate_Chat_SendText asyncDelegate = (RemoteAsyncDelegate_Chat_SendText) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                Chat_SendText_ReturnType returnData = new Chat_SendText_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_Chat_SetReceivingState(IAsyncResult ar)
        {
            RemoteAsyncDelegate_Chat_SetReceivingState asyncDelegate = (RemoteAsyncDelegate_Chat_SetReceivingState) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                Chat_SetReceivingState_ReturnType returnData = new Chat_SetReceivingState_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CheatAddTroops(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CheatAddTroops asyncDelegate = (RemoteAsyncDelegate_CheatAddTroops) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CheatAddTroops_ReturnType returnData = new CheatAddTroops_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CheckQuestObjectiveComplete(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CheckQuestObjectiveComplete asyncDelegate = (RemoteAsyncDelegate_CheckQuestObjectiveComplete) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CheckQuestObjectiveComplete_ReturnType returnData = new CheckQuestObjectiveComplete_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CompleteAbandonNewQuest(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CompleteAbandonNewQuest asyncDelegate = (RemoteAsyncDelegate_CompleteAbandonNewQuest) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CompleteAbandonNewQuest_ReturnType returnData = new CompleteAbandonNewQuest_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CompleteQuest(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CompleteQuest asyncDelegate = (RemoteAsyncDelegate_CompleteQuest) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CompleteQuest_ReturnType returnData = new CompleteQuest_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CompleteVillageCastle(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CompleteVillageCastle asyncDelegate = (RemoteAsyncDelegate_CompleteVillageCastle) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CompleteVillageCastle_ReturnType returnData = new CompleteVillageCastle_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_ConvertVillage(IAsyncResult ar)
        {
            RemoteAsyncDelegate_ConvertVillage asyncDelegate = (RemoteAsyncDelegate_ConvertVillage) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                ConvertVillage_ReturnType returnData = new ConvertVillage_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CreateFaction(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CreateFaction asyncDelegate = (RemoteAsyncDelegate_CreateFaction) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CreateFaction_ReturnType returnData = new CreateFaction_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CreateFactionRelationship(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CreateFactionRelationship asyncDelegate = (RemoteAsyncDelegate_CreateFactionRelationship) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CreateFactionRelationship_ReturnType returnData = new CreateFactionRelationship_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CreateForum(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CreateForum asyncDelegate = (RemoteAsyncDelegate_CreateForum) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CreateForum_ReturnType returnData = new CreateForum_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CreateHouseRelationship(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CreateHouseRelationship asyncDelegate = (RemoteAsyncDelegate_CreateHouseRelationship) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CreateHouseRelationship_ReturnType returnData = new CreateHouseRelationship_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CreateMailFolder(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CreateMailFolder asyncDelegate = (RemoteAsyncDelegate_CreateMailFolder) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CreateMailFolder_ReturnType returnData = new CreateMailFolder_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CreateNewUser(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CreateNewUser asyncDelegate = (RemoteAsyncDelegate_CreateNewUser) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CreateNewUser_ReturnType returnData = new CreateNewUser_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_CreateUserRelationship(IAsyncResult ar)
        {
            RemoteAsyncDelegate_CreateUserRelationship asyncDelegate = (RemoteAsyncDelegate_CreateUserRelationship) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                CreateUserRelationship_ReturnType returnData = new CreateUserRelationship_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_DeleteCastleElement(IAsyncResult ar)
        {
            RemoteAsyncDelegate_DeleteCastleElement asyncDelegate = (RemoteAsyncDelegate_DeleteCastleElement) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                DeleteCastleElement_ReturnType returnData = new DeleteCastleElement_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_DeleteForum(IAsyncResult ar)
        {
            RemoteAsyncDelegate_DeleteForum asyncDelegate = (RemoteAsyncDelegate_DeleteForum) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                DeleteForum_ReturnType returnData = new DeleteForum_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_DeleteForumPost(IAsyncResult ar)
        {
            RemoteAsyncDelegate_DeleteForumPost asyncDelegate = (RemoteAsyncDelegate_DeleteForumPost) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                DeleteForumPost_ReturnType returnData = new DeleteForumPost_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_DeleteForumThread(IAsyncResult ar)
        {
            RemoteAsyncDelegate_DeleteForumThread asyncDelegate = (RemoteAsyncDelegate_DeleteForumThread) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                DeleteForumThread_ReturnType returnData = new DeleteForumThread_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_DeleteMailThread(IAsyncResult ar)
        {
            RemoteAsyncDelegate_DeleteMailThread asyncDelegate = (RemoteAsyncDelegate_DeleteMailThread) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                DeleteMailThread_ReturnType returnData = new DeleteMailThread_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_DeleteReports(IAsyncResult ar)
        {
            RemoteAsyncDelegate_DeleteReports asyncDelegate = (RemoteAsyncDelegate_DeleteReports) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                DeleteReports_ReturnType returnData = new DeleteReports_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_DeleteVillageBuilding(IAsyncResult ar)
        {
            RemoteAsyncDelegate_DeleteVillageBuilding asyncDelegate = (RemoteAsyncDelegate_DeleteVillageBuilding) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                DeleteVillageBuilding_ReturnType returnData = new DeleteVillageBuilding_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_DisbandFaction(IAsyncResult ar)
        {
            RemoteAsyncDelegate_DisbandFaction asyncDelegate = (RemoteAsyncDelegate_DisbandFaction) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                DisbandFaction_ReturnType returnData = new DisbandFaction_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_DisbandPeople(IAsyncResult ar)
        {
            RemoteAsyncDelegate_DisbandPeople asyncDelegate = (RemoteAsyncDelegate_DisbandPeople) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                DisbandPeople_ReturnType returnData = new DisbandPeople_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_DisbandTroops(IAsyncResult ar)
        {
            RemoteAsyncDelegate_DisbandTroops asyncDelegate = (RemoteAsyncDelegate_DisbandTroops) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                DisbandTroops_ReturnType returnData = new DisbandTroops_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_DonateCapitalGoods(IAsyncResult ar)
        {
            RemoteAsyncDelegate_DonateCapitalGoods asyncDelegate = (RemoteAsyncDelegate_DonateCapitalGoods) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                DonateCapitalGoods_ReturnType returnData = new DonateCapitalGoods_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_DoResearch(IAsyncResult ar)
        {
            RemoteAsyncDelegate_DoResearch asyncDelegate = (RemoteAsyncDelegate_DoResearch) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                DoResearch_ReturnType returnData = new DoResearch_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_FactionApplication(IAsyncResult ar)
        {
            RemoteAsyncDelegate_FactionApplication asyncDelegate = (RemoteAsyncDelegate_FactionApplication) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                FactionApplication_ReturnType returnData = new FactionApplication_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_FactionApplicationProcessing(IAsyncResult ar)
        {
            RemoteAsyncDelegate_FactionApplicationProcessing asyncDelegate = (RemoteAsyncDelegate_FactionApplicationProcessing) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                FactionApplicationProcessing_ReturnType returnData = new FactionApplicationProcessing_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_FactionChangeMemberStatus(IAsyncResult ar)
        {
            RemoteAsyncDelegate_FactionChangeMemberStatus asyncDelegate = (RemoteAsyncDelegate_FactionChangeMemberStatus) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                FactionChangeMemberStatus_ReturnType returnData = new FactionChangeMemberStatus_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_FactionLeadershipVote(IAsyncResult ar)
        {
            RemoteAsyncDelegate_FactionLeadershipVote asyncDelegate = (RemoteAsyncDelegate_FactionLeadershipVote) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                FactionLeadershipVote_ReturnType returnData = new FactionLeadershipVote_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_FactionLeave(IAsyncResult ar)
        {
            RemoteAsyncDelegate_FactionLeave asyncDelegate = (RemoteAsyncDelegate_FactionLeave) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                FactionLeave_ReturnType returnData = new FactionLeave_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_FactionReplyToInvite(IAsyncResult ar)
        {
            RemoteAsyncDelegate_FactionReplyToInvite asyncDelegate = (RemoteAsyncDelegate_FactionReplyToInvite) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                FactionReplyToInvite_ReturnType returnData = new FactionReplyToInvite_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_FactionSendInvite(IAsyncResult ar)
        {
            RemoteAsyncDelegate_FactionSendInvite asyncDelegate = (RemoteAsyncDelegate_FactionSendInvite) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                FactionSendInvite_ReturnType returnData = new FactionSendInvite_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_FactionWithdrawInvite(IAsyncResult ar)
        {
            RemoteAsyncDelegate_FactionWithdrawInvite asyncDelegate = (RemoteAsyncDelegate_FactionWithdrawInvite) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                FactionWithdrawInvite_ReturnType returnData = new FactionWithdrawInvite_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_FlagMailRead(IAsyncResult ar)
        {
            RemoteAsyncDelegate_FlagMailRead asyncDelegate = (RemoteAsyncDelegate_FlagMailRead) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                FlagMailRead_ReturnType returnData = new FlagMailRead_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_FlagQuestObjectiveComplete(IAsyncResult ar)
        {
            RemoteAsyncDelegate_FlagQuestObjectiveComplete asyncDelegate = (RemoteAsyncDelegate_FlagQuestObjectiveComplete) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                FlagQuestObjectiveComplete_ReturnType returnData = new FlagQuestObjectiveComplete_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_ForwardReport(IAsyncResult ar)
        {
            RemoteAsyncDelegate_ForwardReport asyncDelegate = (RemoteAsyncDelegate_ForwardReport) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                ForwardReport_ReturnType returnData = new ForwardReport_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_FullTick(IAsyncResult ar)
        {
            RemoteAsyncDelegate_FullTick asyncDelegate = (RemoteAsyncDelegate_FullTick) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                FullTick_ReturnType returnData = new FullTick_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetActivePeople(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetActivePeople asyncDelegate = (RemoteAsyncDelegate_GetActivePeople) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetActivePeople_ReturnType returnData = new GetActivePeople_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetActiveTraders(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetActiveTraders asyncDelegate = (RemoteAsyncDelegate_GetActiveTraders) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetActiveTraders_ReturnType returnData = new GetActiveTraders_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetAdminStats(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetAdminStats asyncDelegate = (RemoteAsyncDelegate_GetAdminStats) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetAdminStats_ReturnType returnData = new GetAdminStats_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetAllVillageOwnerFactions(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetAllVillageOwnerFactions asyncDelegate = (RemoteAsyncDelegate_GetAllVillageOwnerFactions) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                GetAllVillageOwnerFactions_ReturnType resultData = asyncDelegate.EndInvoke(ar);
                if (resultData.sendIndex == this.GetAllVillageOwnerFactions_Index)
                {
                    this.GetAllVillageOwnerFactions_ValidDownload = true;
                    this.storeRPCresult(ar, resultData);
                }
                else
                {
                    this.removeRPCresult(ar);
                }
            }
            catch (Exception exception)
            {
                GetAllVillageOwnerFactions_ReturnType returnData = new GetAllVillageOwnerFactions_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetAreaFactionChanges(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetAreaFactionChanges asyncDelegate = (RemoteAsyncDelegate_GetAreaFactionChanges) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetAreaFactionChanges_ReturnType returnData = new GetAreaFactionChanges_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetArmyData(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetArmyData asyncDelegate = (RemoteAsyncDelegate_GetArmyData) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetArmyData_ReturnType returnData = new GetArmyData_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetBattleHonourRating(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetBattleHonourRating asyncDelegate = (RemoteAsyncDelegate_GetBattleHonourRating) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetBattleHonourRating_ReturnType returnData = new GetBattleHonourRating_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetCapitalBarracksSpace(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetCapitalBarracksSpace asyncDelegate = (RemoteAsyncDelegate_GetCapitalBarracksSpace) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetCapitalBarracksSpace_ReturnType returnData = new GetCapitalBarracksSpace_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetCastle(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetCastle asyncDelegate = (RemoteAsyncDelegate_GetCastle) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetCastle_ReturnType returnData = new GetCastle_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetCountryElectionInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetCountryElectionInfo asyncDelegate = (RemoteAsyncDelegate_GetCountryElectionInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetCountryElectionInfo_ReturnType returnData = new GetCountryElectionInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetCountryFrontPageInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetCountryFrontPageInfo asyncDelegate = (RemoteAsyncDelegate_GetCountryFrontPageInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetCountryFrontPageInfo_ReturnType returnData = new GetCountryFrontPageInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetCountyElectionInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetCountyElectionInfo asyncDelegate = (RemoteAsyncDelegate_GetCountyElectionInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetCountyElectionInfo_ReturnType returnData = new GetCountyElectionInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetCountyFrontPageInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetCountyFrontPageInfo asyncDelegate = (RemoteAsyncDelegate_GetCountyFrontPageInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetCountyFrontPageInfo_ReturnType returnData = new GetCountyFrontPageInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetCurrentElectionInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetCurrentElectionInfo asyncDelegate = (RemoteAsyncDelegate_GetCurrentElectionInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetCurrentElectionInfo_ReturnType returnData = new GetCurrentElectionInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetExcommunicationStatus(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetExcommunicationStatus asyncDelegate = (RemoteAsyncDelegate_GetExcommunicationStatus) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetExcommunicationStatus_ReturnType returnData = new GetExcommunicationStatus_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetFactionData(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetFactionData asyncDelegate = (RemoteAsyncDelegate_GetFactionData) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetFactionData_ReturnType returnData = new GetFactionData_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetForumList(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetForumList asyncDelegate = (RemoteAsyncDelegate_GetForumList) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetForumList_ReturnType returnData = new GetForumList_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetForumThread(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetForumThread asyncDelegate = (RemoteAsyncDelegate_GetForumThread) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetForumThread_ReturnType returnData = new GetForumThread_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetForumThreadList(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetForumThreadList asyncDelegate = (RemoteAsyncDelegate_GetForumThreadList) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetForumThreadList_ReturnType returnData = new GetForumThreadList_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetHistoricalData(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetHistoricalData asyncDelegate = (RemoteAsyncDelegate_GetHistoricalData) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetHistoricalData_ReturnType returnData = new GetHistoricalData_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetHouseGloryPoints(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetHouseGloryPoints asyncDelegate = (RemoteAsyncDelegate_GetHouseGloryPoints) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetHouseGloryPoints_ReturnType returnData = new GetHouseGloryPoints_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetIngameMessage(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetIngameMessage asyncDelegate = (RemoteAsyncDelegate_GetIngameMessage) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetIngameMessage_ReturnType returnData = new GetIngameMessage_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetInvasionInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetInvasionInfo asyncDelegate = (RemoteAsyncDelegate_GetInvasionInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetInvasionInfo_ReturnType returnData = new GetInvasionInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetLastAttacker(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetLastAttacker asyncDelegate = (RemoteAsyncDelegate_GetLastAttacker) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetLastAttacker_ReturnType returnData = new GetLastAttacker_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetLoginHistory(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetLoginHistory asyncDelegate = (RemoteAsyncDelegate_GetLoginHistory) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetLoginHistory_ReturnType returnData = new GetLoginHistory_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetMailFolders(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetMailFolders asyncDelegate = (RemoteAsyncDelegate_GetMailFolders) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetMailFolders_ReturnType returnData = new GetMailFolders_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetMailRecipientsHistory(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetMailRecipientsHistory asyncDelegate = (RemoteAsyncDelegate_GetMailRecipientsHistory) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetMailRecipientsHistory_ReturnType returnData = new GetMailRecipientsHistory_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetMailThread(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetMailThread asyncDelegate = (RemoteAsyncDelegate_GetMailThread) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetMailThread_ReturnType returnData = new GetMailThread_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetMailThreadList(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetMailThreadList asyncDelegate = (RemoteAsyncDelegate_GetMailThreadList) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetMailThreadList_ReturnType returnData = new GetMailThreadList_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetMailUserSearch(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetMailUserSearch asyncDelegate = (RemoteAsyncDelegate_GetMailUserSearch) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetMailUserSearch_ReturnType returnData = new GetMailUserSearch_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetOtherUserVillageIDList(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetOtherUserVillageIDList asyncDelegate = (RemoteAsyncDelegate_GetOtherUserVillageIDList) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetOtherUserVillageIDList_ReturnType returnData = new GetOtherUserVillageIDList_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetParishFrontPageInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetParishFrontPageInfo asyncDelegate = (RemoteAsyncDelegate_GetParishFrontPageInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetParishFrontPageInfo_ReturnType returnData = new GetParishFrontPageInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetParishMembersList(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetParishMembersList asyncDelegate = (RemoteAsyncDelegate_GetParishMembersList) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetParishMembersList_ReturnType returnData = new GetParishMembersList_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetPreVassalInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetPreVassalInfo asyncDelegate = (RemoteAsyncDelegate_GetPreVassalInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetPreVassalInfo_ReturnType returnData = new GetPreVassalInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetProvinceElectionInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetProvinceElectionInfo asyncDelegate = (RemoteAsyncDelegate_GetProvinceElectionInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetProvinceElectionInfo_ReturnType returnData = new GetProvinceElectionInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetProvinceFrontPageInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetProvinceFrontPageInfo asyncDelegate = (RemoteAsyncDelegate_GetProvinceFrontPageInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetProvinceFrontPageInfo_ReturnType returnData = new GetProvinceFrontPageInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetQuestData(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetQuestData asyncDelegate = (RemoteAsyncDelegate_GetQuestData) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetQuestData_ReturnType returnData = new GetQuestData_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetQuestStatus(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetQuestStatus asyncDelegate = (RemoteAsyncDelegate_GetQuestStatus) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetQuestStatus_ReturnType returnData = new GetQuestStatus_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetReport(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetReport asyncDelegate = (RemoteAsyncDelegate_GetReport) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetReport_ReturnType returnData = new GetReport_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetReportsList(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetReportsList asyncDelegate = (RemoteAsyncDelegate_GetReportsList) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetReportsList_ReturnType returnData = new GetReportsList_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetResearchData(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetResearchData asyncDelegate = (RemoteAsyncDelegate_GetResearchData) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetResearchData_ReturnType returnData = new GetResearchData_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetResourceLevel(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetResourceLevel asyncDelegate = (RemoteAsyncDelegate_GetResourceLevel) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetResourceLevel_ReturnType returnData = new GetResourceLevel_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetStockExchangeData(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetStockExchangeData asyncDelegate = (RemoteAsyncDelegate_GetStockExchangeData) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetStockExchangeData_ReturnType returnData = new GetStockExchangeData_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetUserIDFromName(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetUserIDFromName asyncDelegate = (RemoteAsyncDelegate_GetUserIDFromName) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetUserIDFromName_ReturnType returnData = new GetUserIDFromName_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetUserPeople(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetUserPeople asyncDelegate = (RemoteAsyncDelegate_GetUserPeople) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetUserPeople_ReturnType returnData = new GetUserPeople_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetUserTraders(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetUserTraders asyncDelegate = (RemoteAsyncDelegate_GetUserTraders) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetUserTraders_ReturnType returnData = new GetUserTraders_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetUserVillages(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetUserVillages asyncDelegate = (RemoteAsyncDelegate_GetUserVillages) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetUserVillages_ReturnType returnData = new GetUserVillages_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetVassalArmyInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetVassalArmyInfo asyncDelegate = (RemoteAsyncDelegate_GetVassalArmyInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetVassalArmyInfo_ReturnType returnData = new GetVassalArmyInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetViewFactionData(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetViewFactionData asyncDelegate = (RemoteAsyncDelegate_GetViewFactionData) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetViewFactionData_ReturnType returnData = new GetViewFactionData_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetViewHouseData(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetViewHouseData asyncDelegate = (RemoteAsyncDelegate_GetViewHouseData) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetViewHouseData_ReturnType returnData = new GetViewHouseData_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetVillageBuildingsList(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetVillageBuildingsList asyncDelegate = (RemoteAsyncDelegate_GetVillageBuildingsList) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetVillageBuildingsList_ReturnType returnData = new GetVillageBuildingsList_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetVillageFactionChanges(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetVillageFactionChanges asyncDelegate = (RemoteAsyncDelegate_GetVillageFactionChanges) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                GetVillageFactionChanges_ReturnType resultData = asyncDelegate.EndInvoke(ar);
                if (resultData.sendIndex == this.GetVillageFactionChanges_Index)
                {
                    this.GetVillageFactionChanges_ValidDownload = true;
                    this.storeRPCresult(ar, resultData);
                }
                else
                {
                    this.removeRPCresult(ar);
                }
            }
            catch (Exception exception)
            {
                GetVillageFactionChanges_ReturnType returnData = new GetVillageFactionChanges_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetVillageInfoForDonateCapitalGoods(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetVillageInfoForDonateCapitalGoods asyncDelegate = (RemoteAsyncDelegate_GetVillageInfoForDonateCapitalGoods) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetVillageInfoForDonateCapitalGoods_ReturnType returnData = new GetVillageInfoForDonateCapitalGoods_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetVillageNames(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetVillageNames asyncDelegate = (RemoteAsyncDelegate_GetVillageNames) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                GetVillageNames_ReturnType resultData = asyncDelegate.EndInvoke(ar);
                if (resultData.sendIndex == this.GetVillageNames_Index)
                {
                    this.GetVillageNames_ValidDownload = true;
                    this.storeRPCresult(ar, resultData);
                }
                else
                {
                    this.removeRPCresult(ar);
                }
            }
            catch (Exception exception)
            {
                GetVillageNames_ReturnType returnData = new GetVillageNames_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetVillageRankTaxTree(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetVillageRankTaxTree asyncDelegate = (RemoteAsyncDelegate_GetVillageRankTaxTree) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetVillageRankTaxTree_ReturnType returnData = new GetVillageRankTaxTree_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GetVillageStartLocations(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GetVillageStartLocations asyncDelegate = (RemoteAsyncDelegate_GetVillageStartLocations) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GetVillageStartLocations_ReturnType returnData = new GetVillageStartLocations_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_GiveForumAccess(IAsyncResult ar)
        {
            RemoteAsyncDelegate_GiveForumAccess asyncDelegate = (RemoteAsyncDelegate_GiveForumAccess) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                GiveForumAccess_ReturnType returnData = new GiveForumAccess_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_HandleVassalRequest(IAsyncResult ar)
        {
            RemoteAsyncDelegate_HandleVassalRequest asyncDelegate = (RemoteAsyncDelegate_HandleVassalRequest) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                HandleVassalRequest_ReturnType returnData = new HandleVassalRequest_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_HouseVote(IAsyncResult ar)
        {
            RemoteAsyncDelegate_HouseVote asyncDelegate = (RemoteAsyncDelegate_HouseVote) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                HouseVote_ReturnType returnData = new HouseVote_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_HouseVoteHouseLeader(IAsyncResult ar)
        {
            RemoteAsyncDelegate_HouseVoteHouseLeader asyncDelegate = (RemoteAsyncDelegate_HouseVoteHouseLeader) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                HouseVoteHouseLeader_ReturnType returnData = new HouseVoteHouseLeader_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_InitialiseFreeCards(IAsyncResult ar)
        {
            RemoteAsyncDelegate_InitialiseFreeCards asyncDelegate = (RemoteAsyncDelegate_InitialiseFreeCards) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                InitialiseFreeCards_ReturnType returnData = new InitialiseFreeCards_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_LaunchCastleAttack(IAsyncResult ar)
        {
            RemoteAsyncDelegate_LaunchCastleAttack asyncDelegate = (RemoteAsyncDelegate_LaunchCastleAttack) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                LaunchCastleAttack_ReturnType returnData = new LaunchCastleAttack_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_LeaderBoard(IAsyncResult ar)
        {
            RemoteAsyncDelegate_LeaderBoard asyncDelegate = (RemoteAsyncDelegate_LeaderBoard) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                LeaderBoard_ReturnType returnData = new LeaderBoard_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_LeaderBoardSearch(IAsyncResult ar)
        {
            RemoteAsyncDelegate_LeaderBoardSearch asyncDelegate = (RemoteAsyncDelegate_LeaderBoardSearch) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                LeaderBoardSearch_ReturnType returnData = new LeaderBoardSearch_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_LeaveHouse(IAsyncResult ar)
        {
            RemoteAsyncDelegate_LeaveHouse asyncDelegate = (RemoteAsyncDelegate_LeaveHouse) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                LeaveHouse_ReturnType returnData = new LeaveHouse_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_LoginUser(IAsyncResult ar)
        {
            RemoteAsyncDelegate_LoginUser asyncDelegate = (RemoteAsyncDelegate_LoginUser) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                LoginUser_ReturnType returnData = new LoginUser_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_LoginUserGuid(IAsyncResult ar)
        {
            RemoteAsyncDelegate_LoginUserGuid asyncDelegate = (RemoteAsyncDelegate_LoginUserGuid) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                LoginUserGuid_ReturnType returnData = new LoginUserGuid_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_LogOut(IAsyncResult ar)
        {
            RemoteAsyncDelegate_LogOut asyncDelegate = (RemoteAsyncDelegate_LogOut) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                LogOut_ReturnType returnData = new LogOut_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_MakeCountryVote(IAsyncResult ar)
        {
            RemoteAsyncDelegate_MakeCountryVote asyncDelegate = (RemoteAsyncDelegate_MakeCountryVote) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                MakeCountryVote_ReturnType returnData = new MakeCountryVote_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_MakeCountyVote(IAsyncResult ar)
        {
            RemoteAsyncDelegate_MakeCountyVote asyncDelegate = (RemoteAsyncDelegate_MakeCountyVote) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                MakeCountyVote_ReturnType returnData = new MakeCountyVote_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_MakeParishVote(IAsyncResult ar)
        {
            RemoteAsyncDelegate_MakeParishVote asyncDelegate = (RemoteAsyncDelegate_MakeParishVote) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                MakeParishVote_ReturnType returnData = new MakeParishVote_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_MakePeople(IAsyncResult ar)
        {
            RemoteAsyncDelegate_MakePeople asyncDelegate = (RemoteAsyncDelegate_MakePeople) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                MakePeople_ReturnType returnData = new MakePeople_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_MakeProvinceVote(IAsyncResult ar)
        {
            RemoteAsyncDelegate_MakeProvinceVote asyncDelegate = (RemoteAsyncDelegate_MakeProvinceVote) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                MakeProvinceVote_ReturnType returnData = new MakeProvinceVote_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_MakeTroop(IAsyncResult ar)
        {
            RemoteAsyncDelegate_MakeTroop asyncDelegate = (RemoteAsyncDelegate_MakeTroop) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                MakeTroop_ReturnType returnData = new MakeTroop_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_ManageReportFolders(IAsyncResult ar)
        {
            RemoteAsyncDelegate_ManageReportFolders asyncDelegate = (RemoteAsyncDelegate_ManageReportFolders) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                ManageReportFolders_ReturnType returnData = new ManageReportFolders_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_MemorizeCastleTroops(IAsyncResult ar)
        {
            RemoteAsyncDelegate_MemorizeCastleTroops asyncDelegate = (RemoteAsyncDelegate_MemorizeCastleTroops) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                MemorizeCastleTroops_ReturnType returnData = new MemorizeCastleTroops_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_MoveToMailFolder(IAsyncResult ar)
        {
            RemoteAsyncDelegate_MoveToMailFolder asyncDelegate = (RemoteAsyncDelegate_MoveToMailFolder) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                MoveToMailFolder_ReturnType returnData = new MoveToMailFolder_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_MoveVillageBuilding(IAsyncResult ar)
        {
            RemoteAsyncDelegate_MoveVillageBuilding asyncDelegate = (RemoteAsyncDelegate_MoveVillageBuilding) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                MoveVillageBuilding_ReturnType returnData = new MoveVillageBuilding_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_NewForumThread(IAsyncResult ar)
        {
            RemoteAsyncDelegate_NewForumThread asyncDelegate = (RemoteAsyncDelegate_NewForumThread) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                NewForumThread_ReturnType returnData = new NewForumThread_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_ParishWallDetailInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_ParishWallDetailInfo asyncDelegate = (RemoteAsyncDelegate_ParishWallDetailInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                ParishWallDetailInfo_ReturnType returnData = new ParishWallDetailInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_PlaceVillageBuilding(IAsyncResult ar)
        {
            RemoteAsyncDelegate_PlaceVillageBuilding asyncDelegate = (RemoteAsyncDelegate_PlaceVillageBuilding) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                PlaceVillageBuilding_ReturnType returnData = new PlaceVillageBuilding_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_PostToForumThread(IAsyncResult ar)
        {
            RemoteAsyncDelegate_PostToForumThread asyncDelegate = (RemoteAsyncDelegate_PostToForumThread) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                PostToForumThread_ReturnType returnData = new PostToForumThread_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_PreAttackSetup(IAsyncResult ar)
        {
            RemoteAsyncDelegate_PreAttackSetup asyncDelegate = (RemoteAsyncDelegate_PreAttackSetup) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                PreAttackSetup_ReturnType returnData = new PreAttackSetup_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_PremiumOverview(IAsyncResult ar)
        {
            RemoteAsyncDelegate_PremiumOverview asyncDelegate = (RemoteAsyncDelegate_PremiumOverview) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                PremiumOverview_ReturnType returnData = new PremiumOverview_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_PreValidateCardToBePlayed(IAsyncResult ar)
        {
            RemoteAsyncDelegate_PreValidateCardToBePlayed asyncDelegate = (RemoteAsyncDelegate_PreValidateCardToBePlayed) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                PreValidateCardToBePlayed_ReturnType returnData = new PreValidateCardToBePlayed_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_RemoveMailFolder(IAsyncResult ar)
        {
            RemoteAsyncDelegate_RemoveMailFolder asyncDelegate = (RemoteAsyncDelegate_RemoveMailFolder) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                RemoveMailFolder_ReturnType returnData = new RemoveMailFolder_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_ReportMail(IAsyncResult ar)
        {
            RemoteAsyncDelegate_ReportMail asyncDelegate = (RemoteAsyncDelegate_ReportMail) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                ReportMail_ReturnType returnData = new ReportMail_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_ResendVerificationEmail(IAsyncResult ar)
        {
            RemoteAsyncDelegate_ResendVerificationEmail asyncDelegate = (RemoteAsyncDelegate_ResendVerificationEmail) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                ResendVerificationEmail_ReturnType returnData = new ResendVerificationEmail_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_RestoreCastleTroops(IAsyncResult ar)
        {
            RemoteAsyncDelegate_RestoreCastleTroops asyncDelegate = (RemoteAsyncDelegate_RestoreCastleTroops) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                RestoreCastleTroops_ReturnType returnData = new RestoreCastleTroops_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_RetrieveArmyFromGarrison(IAsyncResult ar)
        {
            RemoteAsyncDelegate_RetrieveArmyFromGarrison asyncDelegate = (RemoteAsyncDelegate_RetrieveArmyFromGarrison) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                RetrieveArmyFromGarrison_ReturnType returnData = new RetrieveArmyFromGarrison_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_RetrieveAttackResult(IAsyncResult ar)
        {
            RemoteAsyncDelegate_RetrieveAttackResult asyncDelegate = (RemoteAsyncDelegate_RetrieveAttackResult) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                RetrieveAttackResult_ReturnType returnData = new RetrieveAttackResult_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_RetrievePeople(IAsyncResult ar)
        {
            RemoteAsyncDelegate_RetrievePeople asyncDelegate = (RemoteAsyncDelegate_RetrievePeople) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                RetrievePeople_ReturnType returnData = new RetrievePeople_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_RetrieveStats(IAsyncResult ar)
        {
            RemoteAsyncDelegate_RetrieveStats asyncDelegate = (RemoteAsyncDelegate_RetrieveStats) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                RetrieveStats_ReturnType returnData = new RetrieveStats_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_RetrieveTroopsFromVassal(IAsyncResult ar)
        {
            RemoteAsyncDelegate_RetrieveTroopsFromVassal asyncDelegate = (RemoteAsyncDelegate_RetrieveTroopsFromVassal) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                RetrieveTroopsFromVassal_ReturnType returnData = new RetrieveTroopsFromVassal_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_RetrieveVillageUserInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_RetrieveVillageUserInfo asyncDelegate = (RemoteAsyncDelegate_RetrieveVillageUserInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                RetrieveVillageUserInfo_ReturnType returnData = new RetrieveVillageUserInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_ReturnReinforcements(IAsyncResult ar)
        {
            RemoteAsyncDelegate_ReturnReinforcements asyncDelegate = (RemoteAsyncDelegate_ReturnReinforcements) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                ReturnReinforcements_ReturnType returnData = new ReturnReinforcements_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SelfJoinHouse(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SelfJoinHouse asyncDelegate = (RemoteAsyncDelegate_SelfJoinHouse) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SelfJoinHouse_ReturnType returnData = new SelfJoinHouse_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SendCommands(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SendCommands asyncDelegate = (RemoteAsyncDelegate_SendCommands) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SendCommands_ReturnType returnData = new SendCommands_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SendMail(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SendMail asyncDelegate = (RemoteAsyncDelegate_SendMail) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SendMail_ReturnType returnData = new SendMail_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SendMarketResources(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SendMarketResources asyncDelegate = (RemoteAsyncDelegate_SendMarketResources) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SendMarketResources_ReturnType returnData = new SendMarketResources_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SendPeople(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SendPeople asyncDelegate = (RemoteAsyncDelegate_SendPeople) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SendPeople_ReturnType returnData = new SendPeople_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SendReinforcements(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SendReinforcements asyncDelegate = (RemoteAsyncDelegate_SendReinforcements) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SendReinforcements_ReturnType returnData = new SendReinforcements_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SendScouts(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SendScouts asyncDelegate = (RemoteAsyncDelegate_SendScouts) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SendScouts_ReturnType returnData = new SendScouts_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SendSpecialMail(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SendSpecialMail asyncDelegate = (RemoteAsyncDelegate_SendSpecialMail) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SendSpecialMail_ReturnType returnData = new SendSpecialMail_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SendTroopsToCapital(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SendTroopsToCapital asyncDelegate = (RemoteAsyncDelegate_SendTroopsToCapital) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SendTroopsToCapital_ReturnType returnData = new SendTroopsToCapital_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SendTroopsToVassal(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SendTroopsToVassal asyncDelegate = (RemoteAsyncDelegate_SendTroopsToVassal) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SendTroopsToVassal_ReturnType returnData = new SendTroopsToVassal_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SendVassalRequest(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SendVassalRequest asyncDelegate = (RemoteAsyncDelegate_SendVassalRequest) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SendVassalRequest_ReturnType returnData = new SendVassalRequest_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SetAdminMessage(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SetAdminMessage asyncDelegate = (RemoteAsyncDelegate_SetAdminMessage) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SetAdminMessage_ReturnType returnData = new SetAdminMessage_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SetHighestArmySeen(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SetHighestArmySeen asyncDelegate = (RemoteAsyncDelegate_SetHighestArmySeen) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SetHighestArmySeen_ReturnType returnData = new SetHighestArmySeen_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SetStartingCounty(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SetStartingCounty asyncDelegate = (RemoteAsyncDelegate_SetStartingCounty) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SetStartingCounty_ReturnType returnData = new SetStartingCounty_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SetVacationMode(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SetVacationMode asyncDelegate = (RemoteAsyncDelegate_SetVacationMode) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SetVacationMode_ReturnType returnData = new SetVacationMode_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SpecialVillageInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SpecialVillageInfo asyncDelegate = (RemoteAsyncDelegate_SpecialVillageInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SpecialVillageInfo_ReturnType returnData = new SpecialVillageInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SpinTheWheel(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SpinTheWheel asyncDelegate = (RemoteAsyncDelegate_SpinTheWheel) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SpinTheWheel_ReturnType returnData = new SpinTheWheel_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SpyCommand(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SpyCommand asyncDelegate = (RemoteAsyncDelegate_SpyCommand) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SpyCommand_ReturnType returnData = new SpyCommand_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SpyGetArmyInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SpyGetArmyInfo asyncDelegate = (RemoteAsyncDelegate_SpyGetArmyInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SpyGetArmyInfo_ReturnType returnData = new SpyGetArmyInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SpyGetResearchInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SpyGetResearchInfo asyncDelegate = (RemoteAsyncDelegate_SpyGetResearchInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SpyGetResearchInfo_ReturnType returnData = new SpyGetResearchInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_SpyGetVillageResourceInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_SpyGetVillageResourceInfo asyncDelegate = (RemoteAsyncDelegate_SpyGetVillageResourceInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                SpyGetVillageResourceInfo_ReturnType returnData = new SpyGetVillageResourceInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_StandDownAsParishDespot(IAsyncResult ar)
        {
            RemoteAsyncDelegate_StandDownAsParishDespot asyncDelegate = (RemoteAsyncDelegate_StandDownAsParishDespot) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                StandDownAsParishDespot_ReturnType returnData = new StandDownAsParishDespot_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_StandInElection(IAsyncResult ar)
        {
            RemoteAsyncDelegate_StandInElection asyncDelegate = (RemoteAsyncDelegate_StandInElection) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                StandInElection_ReturnType returnData = new StandInElection_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_StartNewQuest(IAsyncResult ar)
        {
            RemoteAsyncDelegate_StartNewQuest asyncDelegate = (RemoteAsyncDelegate_StartNewQuest) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                StartNewQuest_ReturnType returnData = new StartNewQuest_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_StockExchangeTrade(IAsyncResult ar)
        {
            RemoteAsyncDelegate_StockExchangeTrade asyncDelegate = (RemoteAsyncDelegate_StockExchangeTrade) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                StockExchangeTrade_ReturnType returnData = new StockExchangeTrade_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_TestAchievements(IAsyncResult ar)
        {
            RemoteAsyncDelegate_TestAchievements asyncDelegate = (RemoteAsyncDelegate_TestAchievements) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                TestAchievements_ReturnType returnData = new TestAchievements_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_TouchHouseVisitDate(IAsyncResult ar)
        {
            RemoteAsyncDelegate_TouchHouseVisitDate asyncDelegate = (RemoteAsyncDelegate_TouchHouseVisitDate) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                TouchHouseVisitDate_ReturnType returnData = new TouchHouseVisitDate_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_TutorialCommand(IAsyncResult ar)
        {
            RemoteAsyncDelegate_TutorialCommand asyncDelegate = (RemoteAsyncDelegate_TutorialCommand) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                TutorialCommand_ReturnType returnData = new TutorialCommand_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_UpdateCurrentCards(IAsyncResult ar)
        {
            RemoteAsyncDelegate_UpdateCurrentCards asyncDelegate = (RemoteAsyncDelegate_UpdateCurrentCards) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                UpdateCurrentCards_ReturnType returnData = new UpdateCurrentCards_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_UpdateDiplomacyStatus(IAsyncResult ar)
        {
            RemoteAsyncDelegate_UpdateDiplomacyStatus asyncDelegate = (RemoteAsyncDelegate_UpdateDiplomacyStatus) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                UpdateDiplomacyStatus_ReturnType returnData = new UpdateDiplomacyStatus_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_UpdateReportFilters(IAsyncResult ar)
        {
            RemoteAsyncDelegate_UpdateReportFilters asyncDelegate = (RemoteAsyncDelegate_UpdateReportFilters) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                UpdateReportFilters_ReturnType returnData = new UpdateReportFilters_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_UpdateSelectedTitheType(IAsyncResult ar)
        {
            RemoteAsyncDelegate_UpdateSelectedTitheType asyncDelegate = (RemoteAsyncDelegate_UpdateSelectedTitheType) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                UpdateSelectedTitheType_ReturnType returnData = new UpdateSelectedTitheType_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_UpdateUserOptions(IAsyncResult ar)
        {
            RemoteAsyncDelegate_UpdateUserOptions asyncDelegate = (RemoteAsyncDelegate_UpdateUserOptions) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                UpdateUserOptions_ReturnType returnData = new UpdateUserOptions_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_UpdateVillageFavourites(IAsyncResult ar)
        {
            RemoteAsyncDelegate_UpdateVillageFavourites asyncDelegate = (RemoteAsyncDelegate_UpdateVillageFavourites) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                UpdateVillageFavourites_ReturnType returnData = new UpdateVillageFavourites_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_UpdateVillageResourcesInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_UpdateVillageResourcesInfo asyncDelegate = (RemoteAsyncDelegate_UpdateVillageResourcesInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                UpdateVillageResourcesInfo_ReturnType returnData = new UpdateVillageResourcesInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_UpgradeRank(IAsyncResult ar)
        {
            RemoteAsyncDelegate_UpgradeRank asyncDelegate = (RemoteAsyncDelegate_UpgradeRank) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                UpgradeRank_ReturnType returnData = new UpgradeRank_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_UploadAvatar(IAsyncResult ar)
        {
            RemoteAsyncDelegate_UploadAvatar asyncDelegate = (RemoteAsyncDelegate_UploadAvatar) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                UploadAvatar_ReturnType returnData = new UploadAvatar_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_UserInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_UserInfo asyncDelegate = (RemoteAsyncDelegate_UserInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                UserInfo_ReturnType returnData = new UserInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_VassalInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_VassalInfo asyncDelegate = (RemoteAsyncDelegate_VassalInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                VassalInfo_ReturnType returnData = new VassalInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_VassalSendResources(IAsyncResult ar)
        {
            RemoteAsyncDelegate_VassalSendResources asyncDelegate = (RemoteAsyncDelegate_VassalSendResources) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                VassalSendResources_ReturnType returnData = new VassalSendResources_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_ViewBattle(IAsyncResult ar)
        {
            RemoteAsyncDelegate_ViewBattle asyncDelegate = (RemoteAsyncDelegate_ViewBattle) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                ViewBattle_ReturnType returnData = new ViewBattle_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_ViewCastle(IAsyncResult ar)
        {
            RemoteAsyncDelegate_ViewCastle asyncDelegate = (RemoteAsyncDelegate_ViewCastle) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                ViewCastle_ReturnType returnData = new ViewCastle_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_VillageBuildingChangeRates(IAsyncResult ar)
        {
            RemoteAsyncDelegate_VillageBuildingChangeRates asyncDelegate = (RemoteAsyncDelegate_VillageBuildingChangeRates) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                VillageBuildingChangeRates_ReturnType returnData = new VillageBuildingChangeRates_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_VillageBuildingCompleteDataRetrieval(IAsyncResult ar)
        {
            RemoteAsyncDelegate_VillageBuildingCompleteDataRetrieval asyncDelegate = (RemoteAsyncDelegate_VillageBuildingCompleteDataRetrieval) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                VillageBuildingCompleteDataRetrieval_ReturnType returnData = new VillageBuildingCompleteDataRetrieval_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_VillageBuildingSetActive(IAsyncResult ar)
        {
            RemoteAsyncDelegate_VillageBuildingSetActive asyncDelegate = (RemoteAsyncDelegate_VillageBuildingSetActive) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                VillageBuildingSetActive_ReturnType returnData = new VillageBuildingSetActive_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_VillageHoldBanquet(IAsyncResult ar)
        {
            RemoteAsyncDelegate_VillageHoldBanquet asyncDelegate = (RemoteAsyncDelegate_VillageHoldBanquet) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                VillageHoldBanquet_ReturnType returnData = new VillageHoldBanquet_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_VillageProduceWeapons(IAsyncResult ar)
        {
            RemoteAsyncDelegate_VillageProduceWeapons asyncDelegate = (RemoteAsyncDelegate_VillageProduceWeapons) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                VillageProduceWeapons_ReturnType returnData = new VillageProduceWeapons_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_VillageRename(IAsyncResult ar)
        {
            RemoteAsyncDelegate_VillageRename asyncDelegate = (RemoteAsyncDelegate_VillageRename) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                VillageRename_ReturnType returnData = new VillageRename_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_VoteInElection(IAsyncResult ar)
        {
            RemoteAsyncDelegate_VoteInElection asyncDelegate = (RemoteAsyncDelegate_VoteInElection) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                VoteInElection_ReturnType returnData = new VoteInElection_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        [OneWay]
        public void OurRemoteAsyncCallBack_WorldInfo(IAsyncResult ar)
        {
            RemoteAsyncDelegate_WorldInfo asyncDelegate = (RemoteAsyncDelegate_WorldInfo) ((AsyncResult) ar).AsyncDelegate;
            try
            {
                this.storeRPCresult(ar, asyncDelegate.EndInvoke(ar));
            }
            catch (Exception exception)
            {
                WorldInfo_ReturnType returnData = new WorldInfo_ReturnType();
                this.manageRemoteExpection(ar, returnData, exception);
            }
        }

        private bool packetTimeOut(double timeTaken, CallBackEntryClass cbe)
        {
            double num = 180000.0;
            if (((cbe.classType == typeof(GetVillageNames_ReturnType)) || (cbe.classType == typeof(GetVillageFactionChanges_ReturnType))) || ((cbe.classType == typeof(FullTick_ReturnType)) || (cbe.classType == typeof(GetAllVillageOwnerFactions_ReturnType))))
            {
                num = 600000.0;
            }
            return (timeTaken > num);
        }

        public void ParishWallDetailInfo(int parishCapitalID, long wallInfoID)
        {
            if (this.ParishWallDetailInfo_Callback == null)
            {
                this.ParishWallDetailInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ParishWallDetailInfo);
            }
            RemoteAsyncDelegate_ParishWallDetailInfo info = new RemoteAsyncDelegate_ParishWallDetailInfo(this.service.ParishWallDetailInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, parishCapitalID, wallInfoID, -1, -1, this.ParishWallDetailInfo_Callback, null), typeof(ParishWallDetailInfo_ReturnType));
        }

        public void ParishWallDetailInfo(int parishCapitalID, int targetUserId, int wallType)
        {
            if (this.ParishWallDetailInfo_Callback == null)
            {
                this.ParishWallDetailInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ParishWallDetailInfo);
            }
            RemoteAsyncDelegate_ParishWallDetailInfo info = new RemoteAsyncDelegate_ParishWallDetailInfo(this.service.ParishWallDetailInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, parishCapitalID, -1L, targetUserId, wallType, this.ParishWallDetailInfo_Callback, null), typeof(ParishWallDetailInfo_ReturnType));
        }

        public void PlaceVillageBuilding(int villageID, int buildingType, Point buildingLocation)
        {
            if (this.PlaceVillageBuilding_Callback == null)
            {
                this.PlaceVillageBuilding_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_PlaceVillageBuilding);
            }
            RemoteAsyncDelegate_PlaceVillageBuilding building = new RemoteAsyncDelegate_PlaceVillageBuilding(this.service.PlaceVillageBuilding);
            this.registerRPCcall(building.BeginInvoke(this.UserID, this.SessionID, villageID, buildingType, buildingLocation, this.PlaceVillageBuilding_Callback, null), typeof(PlaceVillageBuilding_ReturnType));
        }

        public void PostToForumThread(long threadID, long forumID, string text)
        {
            if (this.PostToForumThread_Callback == null)
            {
                this.PostToForumThread_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_PostToForumThread);
            }
            RemoteAsyncDelegate_PostToForumThread thread = new RemoteAsyncDelegate_PostToForumThread(this.service.PostToForumThread);
            this.registerRPCcall(thread.BeginInvoke(this.UserID, this.SessionID, threadID, forumID, text, this.PostToForumThread_Callback, null), typeof(PostToForumThread_ReturnType));
        }

        public void PreAttackSetup(int parentAttackingVillage, int attackingVillage, int targetVillage, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int attackType, int pillagePercent, int captainsCommand)
        {
            if (this.PreAttackSetup_Callback == null)
            {
                this.PreAttackSetup_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_PreAttackSetup);
            }
            RemoteAsyncDelegate_PreAttackSetup setup = new RemoteAsyncDelegate_PreAttackSetup(this.service.PreAttackSetup);
            this.registerRPCcall(setup.BeginInvoke(this.UserID, this.SessionID, parentAttackingVillage, attackingVillage, targetVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, attackType, pillagePercent, captainsCommand, this.PreAttackSetup_Callback, null), typeof(PreAttackSetup_ReturnType));
        }

        public void PremiumOverview()
        {
            if (this.PremiumOverview_Callback == null)
            {
                this.PremiumOverview_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_PremiumOverview);
            }
            RemoteAsyncDelegate_PremiumOverview overview = new RemoteAsyncDelegate_PremiumOverview(this.service.PremiumOverview);
            this.registerRPCcall(overview.BeginInvoke(this.UserID, this.SessionID, this.PremiumOverview_Callback, null), typeof(PremiumOverview_ReturnType));
        }

        public void PreValidateCardToBePlayed(int card, int data)
        {
            if (this.PreValidateCardToBePlayed_Callback == null)
            {
                this.PreValidateCardToBePlayed_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_PreValidateCardToBePlayed);
            }
            RemoteAsyncDelegate_PreValidateCardToBePlayed played = new RemoteAsyncDelegate_PreValidateCardToBePlayed(this.service.PreValidateCardToBePlayed);
            this.registerRPCcall(played.BeginInvoke(this.UserID, this.SessionID, card, data, this.PreValidateCardToBePlayed_Callback, null), typeof(PreValidateCardToBePlayed_ReturnType));
        }

        public void processData()
        {
            if (this.connectionErrored)
            {
                bool flag = true;
                foreach (CallBackEntryClass class2 in this.resultList)
                {
                    if (((class2.state == 1) && (class2.classType != typeof(FullTick_ReturnType))) && (class2.classType != typeof(GetArmyData_ReturnType)))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    GameEngine.Instance.sessionExpired(11);
                }
                else
                {
                    GameEngine.Instance.sessionExpired(1);
                }
                this.connectionErrored = false;
            }
            this.inResultsProcessing = true;
            bool flag2 = false;
            foreach (CallBackEntryClass class3 in this.resultList)
            {
                if (class3.state == 1)
                {
                    double timeTaken = DXTimer.GetCurrentMilliseconds() - class3.timer;
                    if (timeTaken > 30000.0)
                    {
                        if (class3.classType == typeof(CreateNewUser_ReturnType))
                        {
                            class3.state = 0;
                            class3.data = null;
                            if (this.createNewUser_UserCallBack != null)
                            {
                                CreateNewUser_ReturnType returnData = new CreateNewUser_ReturnType();
                                returnData.SetAsFailed();
                                returnData.m_errorCode = CommonTypes.ErrorCodes.ErrorCode.CONNECTION_TIMED_OUT;
                                this.createNewUser_UserCallBack(returnData);
                            }
                        }
                        else if (class3.classType == typeof(LoginUser_ReturnType))
                        {
                            class3.state = 0;
                            class3.data = null;
                            if (this.loginUser_UserCallBack != null)
                            {
                                LoginUser_ReturnType type2 = new LoginUser_ReturnType();
                                type2.SetAsFailed();
                                type2.m_errorCode = CommonTypes.ErrorCodes.ErrorCode.CONNECTION_TIMED_OUT;
                                this.loginUser_UserCallBack(type2);
                            }
                        }
                        else if (class3.classType == typeof(LoginUserGuid_ReturnType))
                        {
                            class3.state = 0;
                            class3.data = null;
                            if (this.loginUserGuid_UserCallBack != null)
                            {
                                LoginUserGuid_ReturnType type3 = new LoginUserGuid_ReturnType();
                                type3.SetAsFailed();
                                type3.m_errorCode = CommonTypes.ErrorCodes.ErrorCode.CONNECTION_TIMED_OUT;
                                this.loginUserGuid_UserCallBack(type3);
                            }
                        }
                    }
                    if ((this.packetTimeOut(timeTaken, class3) && (class3.classType != typeof(RetrieveStats_ReturnType))) && !InterfaceMgr.Instance.isConnectionErrorWindow())
                    {
                        this.RTTTimeOuts++;
                        class3.state = 0;
                        class3.data = null;
                        this.addPacket(class3.classType, -1);
                        this.consecutiveTimeOuts++;
                        if (this.consecutiveTimeOuts >= 10)
                        {
                            if (this.sessionID != 0)
                            {
                                this.sessionID = 0;
                                GameEngine.Instance.sessionExpired(2);
                                flag2 = true;
                            }
                            break;
                        }
                    }
                }
                if (class3.state != 2)
                {
                    continue;
                }
                class3.state = 0;
                this.consecutiveTimeOuts = 0;
                if (!class3.data.Success && (class3.data.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.CONNECTION_SESSION_ENDED))
                {
                    if (this.sessionID != 0)
                    {
                        this.sessionID = 0;
                        GameEngine.Instance.sessionExpired(0);
                        flag2 = true;
                    }
                    class3.data = null;
                    break;
                }
                if ((((class3.classType != typeof(CreateNewUser_ReturnType)) && (class3.classType != typeof(LoginUser_ReturnType))) && ((class3.classType != typeof(LogOut_ReturnType)) && (class3.classType != typeof(LoginUserGuid_ReturnType)))) && (!class3.data.Success && (class3.data.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.CONNECTION_NO_SERVER)))
                {
                    class3.data = null;
                    break;
                }
                if (InterfaceMgr.Instance.isConnectionErrorWindow())
                {
                    InterfaceMgr.Instance.closeConnectionErrorWindow();
                }
                if (!class3.data.Success && (class3.data.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.COMMUNICATION_BAN))
                {
                    if ((class3.classType == typeof(SendMail_ReturnType)) || (class3.classType == typeof(SendSpecialMail_ReturnType)))
                    {
                        MyMessageBox.Show(SK.Text("ChatScreenManager_Ban_Message_mail", "Your mail posting privileges have been suspended for violations of the game rules or code of conduct.") + Environment.NewLine + URLs.IPSharingPage, SK.Text("GENERIC_Mail Banned", "Mail Posting Privileges Ban"));
                    }
                    else if ((class3.classType == typeof(NewForumThread_ReturnType)) || (class3.classType == typeof(PostToForumThread_ReturnType)))
                    {
                        MyMessageBox.Show(SK.Text("ChatScreenManager_Ban_Message_forum", "Your forum posting privileges have been suspended for violations of the game rules or code of conduct.") + Environment.NewLine + URLs.IPSharingPage, SK.Text("GENERIC_Forum Banned", "Forum Posting Privileges Ban"));
                    }
                    else
                    {
                        MyMessageBox.Show(SK.Text("ChatScreenManager_Ban_Message", "You have been banned from using this function, contact support if you wish to discuss this.") + Environment.NewLine + "http://support.strongholdkingdoms.com", SK.Text("GENERIC_Banned", "Banned"));
                    }
                    class3.data = null;
                    continue;
                }
                if ((this.commonData_UserCallBack != null) && (class3.classType != typeof(WorldInfo_ReturnType)))
                {
                    Common_ReturnData data = class3.data;
                    if (data.Success)
                    {
                        this.commonData_UserCallBack(data);
                    }
                }
                Common_ReturnData data2 = class3.data;
                if (!data2.Success)
                {
                    if (data2.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.ACTION_NOT_IMPLEMENTED)
                    {
                        MyMessageBox.Show("This Function is not Implement Yet.", "Error");
                    }
                    else
                    {
                        class3.state = 0;
                        switch (data2.m_errorCode)
                        {
                            case CommonTypes.ErrorCodes.ErrorCode.SHARED_TRADING:
                            case CommonTypes.ErrorCodes.ErrorCode.SHARED_VASSALS:
                            case CommonTypes.ErrorCodes.ErrorCode.SHARED_ATTACK_TARGET:
                            case CommonTypes.ErrorCodes.ErrorCode.SHARED_REINFORCEMENTS:
                            case CommonTypes.ErrorCodes.ErrorCode.SHARED_MONKS:
                            case CommonTypes.ErrorCodes.ErrorCode.SHARED_VOTING:
                                SharedIPErrorPopup.showSharedIPPopup(CommonTypes.ErrorCodes.getErrorString(data2.m_errorCode));
                                foreach (CallBackEntryClass class4 in this.resultList)
                                {
                                    class4.timer = DXTimer.GetCurrentMilliseconds();
                                }
                                break;
                        }
                    }
                }
                if (class3.classType == typeof(CreateNewUser_ReturnType))
                {
                    CreateNewUser_ReturnType type4 = (CreateNewUser_ReturnType) class3.data;
                    if (this.createNewUser_UserCallBack != null)
                    {
                        this.createNewUser_UserCallBack(type4);
                    }
                }
                else if (class3.classType == typeof(LoginUser_ReturnType))
                {
                    LoginUser_ReturnType type5 = (LoginUser_ReturnType) class3.data;
                    if (this.loginUser_UserCallBack != null)
                    {
                        this.loginUser_UserCallBack(type5);
                    }
                }
                else if (class3.classType == typeof(LoginUserGuid_ReturnType))
                {
                    LoginUserGuid_ReturnType type6 = (LoginUserGuid_ReturnType) class3.data;
                    if (this.loginUserGuid_UserCallBack != null)
                    {
                        this.loginUserGuid_UserCallBack(type6);
                    }
                }
                else if (class3.classType == typeof(ResendVerificationEmail_ReturnType))
                {
                    ResendVerificationEmail_ReturnType type7 = (ResendVerificationEmail_ReturnType) class3.data;
                    if (this.resendVerificationEmail_UserCallBack != null)
                    {
                        this.resendVerificationEmail_UserCallBack(type7);
                    }
                }
                else if (class3.classType == typeof(RetrieveVillageUserInfo_ReturnType))
                {
                    RetrieveVillageUserInfo_ReturnType type8 = (RetrieveVillageUserInfo_ReturnType) class3.data;
                    if (this.retrieveVillageUserInfo_UserCallBack != null)
                    {
                        this.retrieveVillageUserInfo_UserCallBack(type8);
                    }
                }
                else if (class3.classType == typeof(SpecialVillageInfo_ReturnType))
                {
                    SpecialVillageInfo_ReturnType type9 = (SpecialVillageInfo_ReturnType) class3.data;
                    if (this.specialVillageInfo_UserCallBack != null)
                    {
                        this.specialVillageInfo_UserCallBack(type9);
                    }
                }
                else if (class3.classType == typeof(GetAllVillageOwnerFactions_ReturnType))
                {
                    GetAllVillageOwnerFactions_ReturnType type10 = (GetAllVillageOwnerFactions_ReturnType) class3.data;
                    if (this.getAllVillageOwnerFactions_UserCallBack != null)
                    {
                        this.getAllVillageOwnerFactions_UserCallBack(type10);
                    }
                }
                else if (class3.classType == typeof(GetVillageNames_ReturnType))
                {
                    GetVillageNames_ReturnType type11 = (GetVillageNames_ReturnType) class3.data;
                    if (this.getVillageNames_UserCallBack != null)
                    {
                        this.getVillageNames_UserCallBack(type11);
                    }
                }
                else if (class3.classType == typeof(GetVillageFactionChanges_ReturnType))
                {
                    GetVillageFactionChanges_ReturnType type12 = (GetVillageFactionChanges_ReturnType) class3.data;
                    if (this.getVillageFactionChanges_UserCallBack != null)
                    {
                        this.getVillageFactionChanges_UserCallBack(type12);
                    }
                }
                else if (class3.classType == typeof(GetAreaFactionChanges_ReturnType))
                {
                    GetAreaFactionChanges_ReturnType type13 = (GetAreaFactionChanges_ReturnType) class3.data;
                    if (this.getAreaFactionChanges_UserCallBack != null)
                    {
                        this.getAreaFactionChanges_UserCallBack(type13);
                    }
                }
                else if (class3.classType == typeof(GetUserVillages_ReturnType))
                {
                    GetUserVillages_ReturnType type14 = (GetUserVillages_ReturnType) class3.data;
                    if (this.getUserVillages_UserCallBack != null)
                    {
                        this.getUserVillages_UserCallBack(type14);
                    }
                }
                else if (class3.classType == typeof(GetOtherUserVillageIDList_ReturnType))
                {
                    GetOtherUserVillageIDList_ReturnType type15 = (GetOtherUserVillageIDList_ReturnType) class3.data;
                    if (this.getOtherUserVillageIDList_UserCallBack != null)
                    {
                        this.getOtherUserVillageIDList_UserCallBack(type15);
                    }
                }
                else if (class3.classType == typeof(BuyVillage_ReturnType))
                {
                    BuyVillage_ReturnType type16 = (BuyVillage_ReturnType) class3.data;
                    if (this.buyVillage_UserCallBack != null)
                    {
                        this.buyVillage_UserCallBack(type16);
                    }
                }
                else if (class3.classType == typeof(ConvertVillage_ReturnType))
                {
                    ConvertVillage_ReturnType type17 = (ConvertVillage_ReturnType) class3.data;
                    if (this.convertVillage_UserCallBack != null)
                    {
                        this.convertVillage_UserCallBack(type17);
                    }
                }
                else if (class3.classType == typeof(FullTick_ReturnType))
                {
                    FullTick_ReturnType type18 = (FullTick_ReturnType) class3.data;
                    if (this.fullTick_UserCallBack != null)
                    {
                        this.fullTick_UserCallBack(type18);
                    }
                }
                else if (class3.classType == typeof(LeaderBoard_ReturnType))
                {
                    LeaderBoard_ReturnType type19 = (LeaderBoard_ReturnType) class3.data;
                    if (this.leaderBoard_UserCallBack != null)
                    {
                        this.leaderBoard_UserCallBack(type19);
                    }
                }
                else if (class3.classType == typeof(LeaderBoardSearch_ReturnType))
                {
                    LeaderBoardSearch_ReturnType type20 = (LeaderBoardSearch_ReturnType) class3.data;
                    if (this.leaderBoardSearch_UserCallBack != null)
                    {
                        this.leaderBoardSearch_UserCallBack(type20);
                    }
                }
                else if (class3.classType == typeof(LogOut_ReturnType))
                {
                    LogOut_ReturnType type21 = (LogOut_ReturnType) class3.data;
                    if (this.logOut_UserCallBack != null)
                    {
                        this.logOut_UserCallBack(type21);
                    }
                }
                else if (class3.classType == typeof(UserInfo_ReturnType))
                {
                    UserInfo_ReturnType type22 = (UserInfo_ReturnType) class3.data;
                    if (this.userInfo_UserCallBack != null)
                    {
                        this.userInfo_UserCallBack(type22);
                    }
                }
                else if (class3.classType == typeof(GetArmyData_ReturnType))
                {
                    GetArmyData_ReturnType type23 = (GetArmyData_ReturnType) class3.data;
                    if (this.getArmyData_UserCallBack != null)
                    {
                        this.getArmyData_UserCallBack(type23);
                    }
                }
                else if (class3.classType == typeof(ArmyAttack_ReturnType))
                {
                    ArmyAttack_ReturnType type24 = (ArmyAttack_ReturnType) class3.data;
                    if (this.armyAttack_UserCallBack != null)
                    {
                        this.armyAttack_UserCallBack(type24);
                    }
                }
                else if (class3.classType == typeof(RetrieveAttackResult_ReturnType))
                {
                    RetrieveAttackResult_ReturnType type25 = (RetrieveAttackResult_ReturnType) class3.data;
                    if (this.retrieveAttackResult_UserCallBack != null)
                    {
                        this.retrieveAttackResult_UserCallBack(type25);
                    }
                }
                else if (class3.classType == typeof(SetAdminMessage_ReturnType))
                {
                    SetAdminMessage_ReturnType type26 = (SetAdminMessage_ReturnType) class3.data;
                    if (this.setAdminMessage_UserCallBack != null)
                    {
                        this.setAdminMessage_UserCallBack(type26);
                    }
                }
                else if (class3.classType == typeof(CompleteVillageCastle_ReturnType))
                {
                    CompleteVillageCastle_ReturnType type27 = (CompleteVillageCastle_ReturnType) class3.data;
                    if (this.completeVillageCastle_UserCallBack != null)
                    {
                        this.completeVillageCastle_UserCallBack(type27);
                    }
                }
                else if (class3.classType == typeof(RetrieveStats_ReturnType))
                {
                    RetrieveStats_ReturnType type28 = (RetrieveStats_ReturnType) class3.data;
                    if (this.retrieveStats_UserCallBack != null)
                    {
                        this.retrieveStats_UserCallBack(type28);
                    }
                }
                else if (class3.classType == typeof(GetAdminStats_ReturnType))
                {
                    GetAdminStats_ReturnType type29 = (GetAdminStats_ReturnType) class3.data;
                    if (this.getAdminStats_UserCallBack != null)
                    {
                        this.getAdminStats_UserCallBack(type29);
                    }
                }
                else if (class3.classType == typeof(GetReportsList_ReturnType))
                {
                    GetReportsList_ReturnType type30 = (GetReportsList_ReturnType) class3.data;
                    if (this.getReportsList_UserCallBack != null)
                    {
                        this.getReportsList_UserCallBack(type30);
                    }
                }
                else if (class3.classType == typeof(GetReport_ReturnType))
                {
                    GetReport_ReturnType type31 = (GetReport_ReturnType) class3.data;
                    if (this.getReport_UserCallBack != null)
                    {
                        this.getReport_UserCallBack(type31);
                    }
                }
                else if (class3.classType == typeof(ForwardReport_ReturnType))
                {
                    ForwardReport_ReturnType type32 = (ForwardReport_ReturnType) class3.data;
                    if (this.forwardReport_UserCallBack != null)
                    {
                        this.forwardReport_UserCallBack(type32);
                    }
                }
                else if (class3.classType == typeof(ViewBattle_ReturnType))
                {
                    ViewBattle_ReturnType type33 = (ViewBattle_ReturnType) class3.data;
                    if (this.viewBattle_UserCallBack != null)
                    {
                        this.viewBattle_UserCallBack(type33);
                    }
                }
                else if (class3.classType == typeof(ViewCastle_ReturnType))
                {
                    ViewCastle_ReturnType type34 = (ViewCastle_ReturnType) class3.data;
                    if (this.viewCastle_UserCallBack != null)
                    {
                        this.viewCastle_UserCallBack(type34);
                    }
                }
                else if (class3.classType == typeof(DeleteReports_ReturnType))
                {
                    DeleteReports_ReturnType type35 = (DeleteReports_ReturnType) class3.data;
                    if (this.deleteReports_UserCallBack != null)
                    {
                        this.deleteReports_UserCallBack(type35);
                    }
                }
                else if (class3.classType == typeof(UpdateReportFilters_ReturnType))
                {
                    UpdateReportFilters_ReturnType type36 = (UpdateReportFilters_ReturnType) class3.data;
                    if (this.updateReportFilters_UserCallBack != null)
                    {
                        this.updateReportFilters_UserCallBack(type36);
                    }
                }
                else if (class3.classType == typeof(UpdateUserOptions_ReturnType))
                {
                    UpdateUserOptions_ReturnType type37 = (UpdateUserOptions_ReturnType) class3.data;
                    if (this.updateUserOptions_UserCallBack != null)
                    {
                        this.updateUserOptions_UserCallBack(type37);
                    }
                }
                else if (class3.classType == typeof(ManageReportFolders_ReturnType))
                {
                    ManageReportFolders_ReturnType type38 = (ManageReportFolders_ReturnType) class3.data;
                    if (this.manageReportFolders_UserCallBack != null)
                    {
                        this.manageReportFolders_UserCallBack(type38);
                    }
                }
                else if (class3.classType == typeof(GetHistoricalData_ReturnType))
                {
                    GetHistoricalData_ReturnType type39 = (GetHistoricalData_ReturnType) class3.data;
                    if (this.getHistoricalData_UserCallBack != null)
                    {
                        this.getHistoricalData_UserCallBack(type39);
                    }
                }
                else if (class3.classType == typeof(GetMailThreadList_ReturnType))
                {
                    GetMailThreadList_ReturnType type40 = (GetMailThreadList_ReturnType) class3.data;
                    if (this.getMailThreadList_UserCallBack != null)
                    {
                        this.getMailThreadList_UserCallBack(type40);
                    }
                }
                else if (class3.classType == typeof(GetMailThread_ReturnType))
                {
                    GetMailThread_ReturnType type41 = (GetMailThread_ReturnType) class3.data;
                    if (this.getMailThread_UserCallBack != null)
                    {
                        this.getMailThread_UserCallBack(type41);
                    }
                }
                else if (class3.classType == typeof(GetMailFolders_ReturnType))
                {
                    GetMailFolders_ReturnType type42 = (GetMailFolders_ReturnType) class3.data;
                    if (this.getMailFolders_UserCallBack != null)
                    {
                        this.getMailFolders_UserCallBack(type42);
                    }
                }
                else if (class3.classType == typeof(CreateMailFolder_ReturnType))
                {
                    CreateMailFolder_ReturnType type43 = (CreateMailFolder_ReturnType) class3.data;
                    if (this.createMailFolder_UserCallBack != null)
                    {
                        this.createMailFolder_UserCallBack(type43);
                    }
                }
                else if (class3.classType == typeof(MoveToMailFolder_ReturnType))
                {
                    MoveToMailFolder_ReturnType type44 = (MoveToMailFolder_ReturnType) class3.data;
                    if (this.moveToMailFolder_UserCallBack != null)
                    {
                        this.moveToMailFolder_UserCallBack(type44);
                    }
                }
                else if (class3.classType == typeof(RemoveMailFolder_ReturnType))
                {
                    RemoveMailFolder_ReturnType type45 = (RemoveMailFolder_ReturnType) class3.data;
                    if (this.removeMailFolder_UserCallBack != null)
                    {
                        this.removeMailFolder_UserCallBack(type45);
                    }
                }
                else if (class3.classType == typeof(ReportMail_ReturnType))
                {
                    ReportMail_ReturnType type46 = (ReportMail_ReturnType) class3.data;
                    if (this.reportMail_UserCallBack != null)
                    {
                        this.reportMail_UserCallBack(type46);
                    }
                }
                else if (class3.classType == typeof(FlagMailRead_ReturnType))
                {
                    FlagMailRead_ReturnType type47 = (FlagMailRead_ReturnType) class3.data;
                    if (this.flagMailRead_UserCallBack != null)
                    {
                        this.flagMailRead_UserCallBack(type47);
                    }
                }
                else if (class3.classType == typeof(SendMail_ReturnType))
                {
                    SendMail_ReturnType type48 = (SendMail_ReturnType) class3.data;
                    if (this.sendMail_UserCallBack != null)
                    {
                        this.sendMail_UserCallBack(type48);
                    }
                }
                else if (class3.classType == typeof(SendSpecialMail_ReturnType))
                {
                    SendSpecialMail_ReturnType type49 = (SendSpecialMail_ReturnType) class3.data;
                    if (this.sendSpecialMail_UserCallBack != null)
                    {
                        this.sendSpecialMail_UserCallBack(type49);
                    }
                }
                else if (class3.classType == typeof(DeleteMailThread_ReturnType))
                {
                    DeleteMailThread_ReturnType type50 = (DeleteMailThread_ReturnType) class3.data;
                    if (this.deleteMailThread_UserCallBack != null)
                    {
                        this.deleteMailThread_UserCallBack(type50);
                    }
                }
                else if (class3.classType == typeof(GetMailRecipientsHistory_ReturnType))
                {
                    GetMailRecipientsHistory_ReturnType type51 = (GetMailRecipientsHistory_ReturnType) class3.data;
                    if (this.getMailRecipientsHistory_UserCallBack != null)
                    {
                        this.getMailRecipientsHistory_UserCallBack(type51);
                    }
                }
                else if (class3.classType == typeof(GetMailUserSearch_ReturnType))
                {
                    GetMailUserSearch_ReturnType type52 = (GetMailUserSearch_ReturnType) class3.data;
                    if (this.getMailUserSearch_UserCallBack != null)
                    {
                        this.getMailUserSearch_UserCallBack(type52);
                    }
                }
                else if (class3.classType == typeof(AddUserToFavourites_ReturnType))
                {
                    AddUserToFavourites_ReturnType type53 = (AddUserToFavourites_ReturnType) class3.data;
                    if (this.addUserToFavourites_UserCallBack != null)
                    {
                        this.addUserToFavourites_UserCallBack(type53);
                    }
                }
                else if (class3.classType == typeof(GetResourceLevel_ReturnType))
                {
                    GetResourceLevel_ReturnType type54 = (GetResourceLevel_ReturnType) class3.data;
                    if (this.getResourceLevel_UserCallBack != null)
                    {
                        this.getResourceLevel_UserCallBack(type54);
                    }
                }
                else if (class3.classType == typeof(GetVillageBuildingsList_ReturnType))
                {
                    GetVillageBuildingsList_ReturnType type55 = (GetVillageBuildingsList_ReturnType) class3.data;
                    if (this.getVillageBuildingsList_UserCallBack != null)
                    {
                        this.getVillageBuildingsList_UserCallBack(type55);
                    }
                }
                else if (class3.classType == typeof(PlaceVillageBuilding_ReturnType))
                {
                    PlaceVillageBuilding_ReturnType type56 = (PlaceVillageBuilding_ReturnType) class3.data;
                    if (this.placeVillageBuilding_UserCallBack != null)
                    {
                        this.placeVillageBuilding_UserCallBack(type56);
                    }
                }
                else if (class3.classType == typeof(MoveVillageBuilding_ReturnType))
                {
                    MoveVillageBuilding_ReturnType type57 = (MoveVillageBuilding_ReturnType) class3.data;
                    if (this.moveVillageBuilding_UserCallBack != null)
                    {
                        this.moveVillageBuilding_UserCallBack(type57);
                    }
                }
                else if (class3.classType == typeof(DeleteVillageBuilding_ReturnType))
                {
                    DeleteVillageBuilding_ReturnType type58 = (DeleteVillageBuilding_ReturnType) class3.data;
                    if (this.deleteVillageBuilding_UserCallBack != null)
                    {
                        this.deleteVillageBuilding_UserCallBack(type58);
                    }
                }
                else if (class3.classType == typeof(CancelDeleteVillageBuilding_ReturnType))
                {
                    CancelDeleteVillageBuilding_ReturnType type59 = (CancelDeleteVillageBuilding_ReturnType) class3.data;
                    if (this.cancelDeleteVillageBuilding_UserCallBack != null)
                    {
                        this.cancelDeleteVillageBuilding_UserCallBack(type59);
                    }
                }
                else if (class3.classType == typeof(VillageBuildingCompleteDataRetrieval_ReturnType))
                {
                    VillageBuildingCompleteDataRetrieval_ReturnType type60 = (VillageBuildingCompleteDataRetrieval_ReturnType) class3.data;
                    if (this.villageBuildingCompleteDataRetrieval_UserCallBack != null)
                    {
                        this.villageBuildingCompleteDataRetrieval_UserCallBack(type60);
                    }
                }
                else if (class3.classType == typeof(VillageBuildingSetActive_ReturnType))
                {
                    VillageBuildingSetActive_ReturnType type61 = (VillageBuildingSetActive_ReturnType) class3.data;
                    if (this.villageBuildingSetActive_UserCallBack != null)
                    {
                        this.villageBuildingSetActive_UserCallBack(type61);
                    }
                }
                else if (class3.classType == typeof(VillageBuildingChangeRates_ReturnType))
                {
                    VillageBuildingChangeRates_ReturnType type62 = (VillageBuildingChangeRates_ReturnType) class3.data;
                    if (this.villageBuildingChangeRates_UserCallBack != null)
                    {
                        this.villageBuildingChangeRates_UserCallBack(type62);
                    }
                }
                else if (class3.classType == typeof(VillageRename_ReturnType))
                {
                    VillageRename_ReturnType type63 = (VillageRename_ReturnType) class3.data;
                    if (this.villageRename_UserCallBack != null)
                    {
                        this.villageRename_UserCallBack(type63);
                    }
                }
                else if (class3.classType == typeof(VillageProduceWeapons_ReturnType))
                {
                    VillageProduceWeapons_ReturnType type64 = (VillageProduceWeapons_ReturnType) class3.data;
                    if (this.villageProduceWeapons_UserCallBack != null)
                    {
                        this.villageProduceWeapons_UserCallBack(type64);
                    }
                }
                else if (class3.classType == typeof(VillageHoldBanquet_ReturnType))
                {
                    VillageHoldBanquet_ReturnType type65 = (VillageHoldBanquet_ReturnType) class3.data;
                    if (this.villageHoldBanquet_UserCallBack != null)
                    {
                        this.villageHoldBanquet_UserCallBack(type65);
                    }
                }
                else if (class3.classType == typeof(GetCastle_ReturnType))
                {
                    GetCastle_ReturnType type66 = (GetCastle_ReturnType) class3.data;
                    if (this.getCastle_UserCallBack != null)
                    {
                        this.getCastle_UserCallBack(type66);
                    }
                }
                else if (class3.classType == typeof(AddCastleElement_ReturnType))
                {
                    AddCastleElement_ReturnType type67 = (AddCastleElement_ReturnType) class3.data;
                    if (this.addCastleElement_UserCallBack != null)
                    {
                        this.addCastleElement_UserCallBack(type67);
                    }
                }
                else if (class3.classType == typeof(DeleteCastleElement_ReturnType))
                {
                    DeleteCastleElement_ReturnType type68 = (DeleteCastleElement_ReturnType) class3.data;
                    if (this.deleteCastleElement_UserCallBack != null)
                    {
                        this.deleteCastleElement_UserCallBack(type68);
                    }
                }
                else if (class3.classType == typeof(CheatAddTroops_ReturnType))
                {
                    CheatAddTroops_ReturnType type69 = (CheatAddTroops_ReturnType) class3.data;
                    if (this.cheatAddTroops_UserCallBack != null)
                    {
                        this.cheatAddTroops_UserCallBack(type69);
                    }
                }
                else if (class3.classType == typeof(AutoRepairCastle_ReturnType))
                {
                    AutoRepairCastle_ReturnType type70 = (AutoRepairCastle_ReturnType) class3.data;
                    if (this.autoRepairCastle_UserCallBack != null)
                    {
                        this.autoRepairCastle_UserCallBack(type70);
                    }
                }
                else if (class3.classType == typeof(MemorizeCastleTroops_ReturnType))
                {
                    MemorizeCastleTroops_ReturnType type71 = (MemorizeCastleTroops_ReturnType) class3.data;
                    if (this.memorizeCastleTroops_UserCallBack != null)
                    {
                        this.memorizeCastleTroops_UserCallBack(type71);
                    }
                }
                else if (class3.classType == typeof(RestoreCastleTroops_ReturnType))
                {
                    RestoreCastleTroops_ReturnType type72 = (RestoreCastleTroops_ReturnType) class3.data;
                    if (this.restoreCastleTroops_UserCallBack != null)
                    {
                        this.restoreCastleTroops_UserCallBack(type72);
                    }
                }
                else if (class3.classType == typeof(LaunchCastleAttack_ReturnType))
                {
                    LaunchCastleAttack_ReturnType type73 = (LaunchCastleAttack_ReturnType) class3.data;
                    if (this.launchCastleAttack_UserCallBack != null)
                    {
                        this.launchCastleAttack_UserCallBack(type73);
                    }
                }
                else if (class3.classType == typeof(ChangeCastleElementAggressiveDefender_ReturnType))
                {
                    ChangeCastleElementAggressiveDefender_ReturnType type74 = (ChangeCastleElementAggressiveDefender_ReturnType) class3.data;
                    if (this.changeCastleElementAggressiveDefender_UserCallBack != null)
                    {
                        this.changeCastleElementAggressiveDefender_UserCallBack(type74);
                    }
                }
                else if (class3.classType == typeof(SendMarketResources_ReturnType))
                {
                    SendMarketResources_ReturnType type75 = (SendMarketResources_ReturnType) class3.data;
                    if (this.sendMarketResources_UserCallBack != null)
                    {
                        this.sendMarketResources_UserCallBack(type75);
                    }
                }
                else if (class3.classType == typeof(GetUserTraders_ReturnType))
                {
                    GetUserTraders_ReturnType type76 = (GetUserTraders_ReturnType) class3.data;
                    if (this.getUserTraders_UserCallBack != null)
                    {
                        this.getUserTraders_UserCallBack(type76);
                    }
                }
                else if (class3.classType == typeof(GetActiveTraders_ReturnType))
                {
                    GetActiveTraders_ReturnType type77 = (GetActiveTraders_ReturnType) class3.data;
                    if (this.getActiveTraders_UserCallBack != null)
                    {
                        this.getActiveTraders_UserCallBack(type77);
                    }
                }
                else if (class3.classType == typeof(GetStockExchangeData_ReturnType))
                {
                    GetStockExchangeData_ReturnType type78 = (GetStockExchangeData_ReturnType) class3.data;
                    if (this.getStockExchangeData_UserCallBack != null)
                    {
                        this.getStockExchangeData_UserCallBack(type78);
                    }
                }
                else if (class3.classType == typeof(StockExchangeTrade_ReturnType))
                {
                    StockExchangeTrade_ReturnType type79 = (StockExchangeTrade_ReturnType) class3.data;
                    if (this.stockExchangeTrade_UserCallBack != null)
                    {
                        this.stockExchangeTrade_UserCallBack(type79);
                    }
                }
                else if (class3.classType == typeof(UpdateVillageFavourites_ReturnType))
                {
                    UpdateVillageFavourites_ReturnType type80 = (UpdateVillageFavourites_ReturnType) class3.data;
                    if (this.updateVillageFavourites_UserCallBack != null)
                    {
                        this.updateVillageFavourites_UserCallBack(type80);
                    }
                }
                else if (class3.classType == typeof(MakeTroop_ReturnType))
                {
                    MakeTroop_ReturnType type81 = (MakeTroop_ReturnType) class3.data;
                    if (this.makeTroop_UserCallBack != null)
                    {
                        this.makeTroop_UserCallBack(type81);
                    }
                }
                else if (class3.classType == typeof(DisbandTroops_ReturnType))
                {
                    DisbandTroops_ReturnType type82 = (DisbandTroops_ReturnType) class3.data;
                    if (this.disbandTroops_UserCallBack != null)
                    {
                        this.disbandTroops_UserCallBack(type82);
                    }
                }
                else if (class3.classType == typeof(DisbandPeople_ReturnType))
                {
                    DisbandPeople_ReturnType type83 = (DisbandPeople_ReturnType) class3.data;
                    if (this.disbandPeople_UserCallBack != null)
                    {
                        this.disbandPeople_UserCallBack(type83);
                    }
                }
                else if (class3.classType == typeof(GetVillageInfoForDonateCapitalGoods_ReturnType))
                {
                    GetVillageInfoForDonateCapitalGoods_ReturnType type84 = (GetVillageInfoForDonateCapitalGoods_ReturnType) class3.data;
                    if (this.getVillageInfoForDonateCapitalGoods_UserCallBack != null)
                    {
                        this.getVillageInfoForDonateCapitalGoods_UserCallBack(type84);
                    }
                }
                else if (class3.classType == typeof(DonateCapitalGoods_ReturnType))
                {
                    DonateCapitalGoods_ReturnType type85 = (DonateCapitalGoods_ReturnType) class3.data;
                    if (this.donateCapitalGoods_UserCallBack != null)
                    {
                        this.donateCapitalGoods_UserCallBack(type85);
                    }
                }
                else if (class3.classType == typeof(GetVillageStartLocations_ReturnType))
                {
                    GetVillageStartLocations_ReturnType type86 = (GetVillageStartLocations_ReturnType) class3.data;
                    if (this.getVillageStartLocations_UserCallBack != null)
                    {
                        this.getVillageStartLocations_UserCallBack(type86);
                    }
                }
                else if (class3.classType == typeof(SetStartingCounty_ReturnType))
                {
                    SetStartingCounty_ReturnType type87 = (SetStartingCounty_ReturnType) class3.data;
                    if (this.setStartingCounty_UserCallBack != null)
                    {
                        this.setStartingCounty_UserCallBack(type87);
                    }
                }
                else if (class3.classType == typeof(UpdateCurrentCards_ReturnType))
                {
                    UpdateCurrentCards_ReturnType type88 = (UpdateCurrentCards_ReturnType) class3.data;
                    if (this.updateCurrentCards_UserCallBack != null)
                    {
                        this.updateCurrentCards_UserCallBack(type88);
                    }
                }
                else if (class3.classType == typeof(CancelCard_ReturnType))
                {
                    CancelCard_ReturnType type89 = (CancelCard_ReturnType) class3.data;
                    if (this.cancelCard_UserCallBack != null)
                    {
                        this.cancelCard_UserCallBack(type89);
                    }
                }
                else if (class3.classType == typeof(TutorialCommand_ReturnType))
                {
                    TutorialCommand_ReturnType type90 = (TutorialCommand_ReturnType) class3.data;
                    if (this.tutorialCommand_UserCallBack != null)
                    {
                        this.tutorialCommand_UserCallBack(type90);
                    }
                }
                else if (class3.classType == typeof(FlagQuestObjectiveComplete_ReturnType))
                {
                    FlagQuestObjectiveComplete_ReturnType type91 = (FlagQuestObjectiveComplete_ReturnType) class3.data;
                    if (this.flagQuestObjectiveComplete_UserCallBack != null)
                    {
                        this.flagQuestObjectiveComplete_UserCallBack(type91);
                    }
                }
                else if (class3.classType == typeof(CheckQuestObjectiveComplete_ReturnType))
                {
                    CheckQuestObjectiveComplete_ReturnType type92 = (CheckQuestObjectiveComplete_ReturnType) class3.data;
                    if (this.checkQuestObjectiveComplete_UserCallBack != null)
                    {
                        this.checkQuestObjectiveComplete_UserCallBack(type92);
                    }
                }
                else if (class3.classType == typeof(UpdateDiplomacyStatus_ReturnType))
                {
                    UpdateDiplomacyStatus_ReturnType type93 = (UpdateDiplomacyStatus_ReturnType) class3.data;
                    if (this.updateDiplomacyStatus_UserCallBack != null)
                    {
                        this.updateDiplomacyStatus_UserCallBack(type93);
                    }
                }
                else if (class3.classType == typeof(SendCommands_ReturnType))
                {
                    SendCommands_ReturnType type94 = (SendCommands_ReturnType) class3.data;
                    if (this.sendCommands_UserCallBack != null)
                    {
                        this.sendCommands_UserCallBack(type94);
                    }
                }
                else if (class3.classType == typeof(GetQuestStatus_ReturnType))
                {
                    GetQuestStatus_ReturnType type95 = (GetQuestStatus_ReturnType) class3.data;
                    if (this.getQuestStatus_UserCallBack != null)
                    {
                        this.getQuestStatus_UserCallBack(type95);
                    }
                }
                else if (class3.classType == typeof(CompleteQuest_ReturnType))
                {
                    CompleteQuest_ReturnType type96 = (CompleteQuest_ReturnType) class3.data;
                    if (this.completeQuest_UserCallBack != null)
                    {
                        this.completeQuest_UserCallBack(type96);
                    }
                }
                else if (class3.classType == typeof(UpgradeRank_ReturnType))
                {
                    UpgradeRank_ReturnType type97 = (UpgradeRank_ReturnType) class3.data;
                    if (this.upgradeRank_UserCallBack != null)
                    {
                        this.upgradeRank_UserCallBack(type97);
                    }
                }
                else if (class3.classType == typeof(PreAttackSetup_ReturnType))
                {
                    PreAttackSetup_ReturnType type98 = (PreAttackSetup_ReturnType) class3.data;
                    if (this.preAttackSetup_UserCallBack != null)
                    {
                        this.preAttackSetup_UserCallBack(type98);
                    }
                }
                else if (class3.classType == typeof(GetBattleHonourRating_ReturnType))
                {
                    GetBattleHonourRating_ReturnType type99 = (GetBattleHonourRating_ReturnType) class3.data;
                    if (this.getBattleHonourRating_UserCallBack != null)
                    {
                        this.getBattleHonourRating_UserCallBack(type99);
                    }
                }
                else if (class3.classType == typeof(RetrieveArmyFromGarrison_ReturnType))
                {
                    RetrieveArmyFromGarrison_ReturnType type100 = (RetrieveArmyFromGarrison_ReturnType) class3.data;
                    if (this.retrieveArmyFromGarrison_UserCallBack != null)
                    {
                        this.retrieveArmyFromGarrison_UserCallBack(type100);
                    }
                }
                else if (class3.classType == typeof(GetVillageRankTaxTree_ReturnType))
                {
                    GetVillageRankTaxTree_ReturnType type101 = (GetVillageRankTaxTree_ReturnType) class3.data;
                    if (this.getVillageRankTaxTree_UserCallBack != null)
                    {
                        this.getVillageRankTaxTree_UserCallBack(type101);
                    }
                }
                else if (class3.classType == typeof(GetResearchData_ReturnType))
                {
                    GetResearchData_ReturnType type102 = (GetResearchData_ReturnType) class3.data;
                    if (this.getResearchData_UserCallBack != null)
                    {
                        this.getResearchData_UserCallBack(type102);
                    }
                }
                else if (class3.classType == typeof(DoResearch_ReturnType))
                {
                    DoResearch_ReturnType type103 = (DoResearch_ReturnType) class3.data;
                    if (this.doResearch_UserCallBack != null)
                    {
                        this.doResearch_UserCallBack(type103);
                    }
                }
                else if (class3.classType == typeof(BuyResearchPoint_ReturnType))
                {
                    BuyResearchPoint_ReturnType type104 = (BuyResearchPoint_ReturnType) class3.data;
                    if (this.buyResearchPoint_UserCallBack != null)
                    {
                        this.buyResearchPoint_UserCallBack(type104);
                    }
                }
                else if (class3.classType == typeof(SendReinforcements_ReturnType))
                {
                    SendReinforcements_ReturnType type105 = (SendReinforcements_ReturnType) class3.data;
                    if (this.sendReinforcements_UserCallBack != null)
                    {
                        this.sendReinforcements_UserCallBack(type105);
                    }
                }
                else if (class3.classType == typeof(ReturnReinforcements_ReturnType))
                {
                    ReturnReinforcements_ReturnType type106 = (ReturnReinforcements_ReturnType) class3.data;
                    if (this.returnReinforcements_UserCallBack != null)
                    {
                        this.returnReinforcements_UserCallBack(type106);
                    }
                }
                else if (class3.classType == typeof(CancelCastleAttack_ReturnType))
                {
                    CancelCastleAttack_ReturnType type107 = (CancelCastleAttack_ReturnType) class3.data;
                    if (this.cancelCastleAttack_UserCallBack != null)
                    {
                        this.cancelCastleAttack_UserCallBack(type107);
                    }
                }
                else if (class3.classType == typeof(VassalInfo_ReturnType))
                {
                    VassalInfo_ReturnType type108 = (VassalInfo_ReturnType) class3.data;
                    if (this.vassalInfo_UserCallBack != null)
                    {
                        this.vassalInfo_UserCallBack(type108);
                    }
                }
                else if (class3.classType == typeof(HandleVassalRequest_ReturnType))
                {
                    HandleVassalRequest_ReturnType type109 = (HandleVassalRequest_ReturnType) class3.data;
                    if (this.handleVassalRequest_UserCallBack != null)
                    {
                        this.handleVassalRequest_UserCallBack(type109);
                    }
                }
                else if (class3.classType == typeof(GetVassalArmyInfo_ReturnType))
                {
                    GetVassalArmyInfo_ReturnType type110 = (GetVassalArmyInfo_ReturnType) class3.data;
                    if (this.getVassalArmyInfo_UserCallBack != null)
                    {
                        this.getVassalArmyInfo_UserCallBack(type110);
                    }
                }
                else if (class3.classType == typeof(SendTroopsToVassal_ReturnType))
                {
                    SendTroopsToVassal_ReturnType type111 = (SendTroopsToVassal_ReturnType) class3.data;
                    if (this.sendTroopsToVassal_UserCallBack != null)
                    {
                        this.sendTroopsToVassal_UserCallBack(type111);
                    }
                }
                else if (class3.classType == typeof(RetrieveTroopsFromVassal_ReturnType))
                {
                    RetrieveTroopsFromVassal_ReturnType type112 = (RetrieveTroopsFromVassal_ReturnType) class3.data;
                    if (this.retrieveTroopsFromVassal_UserCallBack != null)
                    {
                        this.retrieveTroopsFromVassal_UserCallBack(type112);
                    }
                }
                else if (class3.classType == typeof(VassalSendResources_ReturnType))
                {
                    VassalSendResources_ReturnType type113 = (VassalSendResources_ReturnType) class3.data;
                    if (this.vassalSendResources_UserCallBack != null)
                    {
                        this.vassalSendResources_UserCallBack(type113);
                    }
                }
                else if (class3.classType == typeof(UpdateSelectedTitheType_ReturnType))
                {
                    UpdateSelectedTitheType_ReturnType type114 = (UpdateSelectedTitheType_ReturnType) class3.data;
                    if (this.updateSelectedTitheType_UserCallBack != null)
                    {
                        this.updateSelectedTitheType_UserCallBack(type114);
                    }
                }
                else if (class3.classType == typeof(BreakVassalage_ReturnType))
                {
                    BreakVassalage_ReturnType type115 = (BreakVassalage_ReturnType) class3.data;
                    if (this.breakVassalage_UserCallBack != null)
                    {
                        this.breakVassalage_UserCallBack(type115);
                    }
                }
                else if (class3.classType == typeof(SendVassalRequest_ReturnType))
                {
                    SendVassalRequest_ReturnType type116 = (SendVassalRequest_ReturnType) class3.data;
                    if (this.sendVassalRequest_UserCallBack != null)
                    {
                        this.sendVassalRequest_UserCallBack(type116);
                    }
                }
                else if (class3.classType == typeof(GetPreVassalInfo_ReturnType))
                {
                    GetPreVassalInfo_ReturnType type117 = (GetPreVassalInfo_ReturnType) class3.data;
                    if (this.getPreVassalInfo_UserCallBack != null)
                    {
                        this.getPreVassalInfo_UserCallBack(type117);
                    }
                }
                else if (class3.classType == typeof(BreakLiegeLord_ReturnType))
                {
                    BreakLiegeLord_ReturnType type118 = (BreakLiegeLord_ReturnType) class3.data;
                    if (this.breakLiegeLord_UserCallBack != null)
                    {
                        this.breakLiegeLord_UserCallBack(type118);
                    }
                }
                else if (class3.classType == typeof(UpdateVillageResourcesInfo_ReturnType))
                {
                    UpdateVillageResourcesInfo_ReturnType type119 = (UpdateVillageResourcesInfo_ReturnType) class3.data;
                    if (this.updateVillageResourcesInfo_UserCallBack != null)
                    {
                        this.updateVillageResourcesInfo_UserCallBack(type119);
                    }
                }
                else
                {
                    if (class3.classType == typeof(SendScouts_ReturnType))
                    {
                        SendScouts_ReturnType type120 = (SendScouts_ReturnType) class3.data;
                        if (this.sendScouts_UserCallBack == null)
                        {
                            goto Label_3601;
                        }
                        try
                        {
                            this.sendScouts_UserCallBack(type120);
                            goto Label_3601;
                        }
                        catch (Exception)
                        {
                            goto Label_3601;
                        }
                    }
                    if (class3.classType == typeof(SetHighestArmySeen_ReturnType))
                    {
                        SetHighestArmySeen_ReturnType type121 = (SetHighestArmySeen_ReturnType) class3.data;
                        if (this.setHighestArmySeen_UserCallBack != null)
                        {
                            this.setHighestArmySeen_UserCallBack(type121);
                        }
                    }
                    else if (class3.classType == typeof(GetForumList_ReturnType))
                    {
                        GetForumList_ReturnType type122 = (GetForumList_ReturnType) class3.data;
                        if (this.getForumList_UserCallBack != null)
                        {
                            this.getForumList_UserCallBack(type122);
                        }
                    }
                    else if (class3.classType == typeof(GetForumThreadList_ReturnType))
                    {
                        GetForumThreadList_ReturnType type123 = (GetForumThreadList_ReturnType) class3.data;
                        if (this.getForumThreadList_UserCallBack != null)
                        {
                            this.getForumThreadList_UserCallBack(type123);
                        }
                    }
                    else if (class3.classType == typeof(GetForumThread_ReturnType))
                    {
                        GetForumThread_ReturnType type124 = (GetForumThread_ReturnType) class3.data;
                        if (this.getForumThread_UserCallBack != null)
                        {
                            this.getForumThread_UserCallBack(type124);
                        }
                    }
                    else if (class3.classType == typeof(NewForumThread_ReturnType))
                    {
                        NewForumThread_ReturnType type125 = (NewForumThread_ReturnType) class3.data;
                        if (this.newForumThread_UserCallBack != null)
                        {
                            this.newForumThread_UserCallBack(type125);
                        }
                    }
                    else if (class3.classType == typeof(PostToForumThread_ReturnType))
                    {
                        PostToForumThread_ReturnType type126 = (PostToForumThread_ReturnType) class3.data;
                        if (this.postToForumThread_UserCallBack != null)
                        {
                            this.postToForumThread_UserCallBack(type126);
                        }
                    }
                    else if (class3.classType == typeof(GiveForumAccess_ReturnType))
                    {
                        GiveForumAccess_ReturnType type127 = (GiveForumAccess_ReturnType) class3.data;
                        if (this.giveForumAccess_UserCallBack != null)
                        {
                            this.giveForumAccess_UserCallBack(type127);
                        }
                    }
                    else if (class3.classType == typeof(CreateForum_ReturnType))
                    {
                        CreateForum_ReturnType type128 = (CreateForum_ReturnType) class3.data;
                        if (this.createForum_UserCallBack != null)
                        {
                            this.createForum_UserCallBack(type128);
                        }
                    }
                    else if (class3.classType == typeof(DeleteForum_ReturnType))
                    {
                        DeleteForum_ReturnType type129 = (DeleteForum_ReturnType) class3.data;
                        if (this.deleteForum_UserCallBack != null)
                        {
                            this.deleteForum_UserCallBack(type129);
                        }
                    }
                    else if (class3.classType == typeof(DeleteForumThread_ReturnType))
                    {
                        DeleteForumThread_ReturnType type130 = (DeleteForumThread_ReturnType) class3.data;
                        if (this.deleteForumThread_UserCallBack != null)
                        {
                            this.deleteForumThread_UserCallBack(type130);
                        }
                    }
                    else if (class3.classType == typeof(DeleteForumPost_ReturnType))
                    {
                        DeleteForumPost_ReturnType type131 = (DeleteForumPost_ReturnType) class3.data;
                        if (this.deleteForumPost_UserCallBack != null)
                        {
                            this.deleteForumPost_UserCallBack(type131);
                        }
                    }
                    else if (class3.classType == typeof(GetCurrentElectionInfo_ReturnType))
                    {
                        GetCurrentElectionInfo_ReturnType type132 = (GetCurrentElectionInfo_ReturnType) class3.data;
                        if (this.getCurrentElectionInfo_UserCallBack != null)
                        {
                            this.getCurrentElectionInfo_UserCallBack(type132);
                        }
                    }
                    else if (class3.classType == typeof(StandInElection_ReturnType))
                    {
                        StandInElection_ReturnType type133 = (StandInElection_ReturnType) class3.data;
                        if (this.standInElection_UserCallBack != null)
                        {
                            this.standInElection_UserCallBack(type133);
                        }
                    }
                    else if (class3.classType == typeof(VoteInElection_ReturnType))
                    {
                        VoteInElection_ReturnType type134 = (VoteInElection_ReturnType) class3.data;
                        if (this.voteInElection_UserCallBack != null)
                        {
                            this.voteInElection_UserCallBack(type134);
                        }
                    }
                    else if (class3.classType == typeof(UploadAvatar_ReturnType))
                    {
                        UploadAvatar_ReturnType type135 = (UploadAvatar_ReturnType) class3.data;
                        if (this.uploadAvatar_UserCallBack != null)
                        {
                            this.uploadAvatar_UserCallBack(type135);
                        }
                    }
                    else if (class3.classType == typeof(MakePeople_ReturnType))
                    {
                        MakePeople_ReturnType type136 = (MakePeople_ReturnType) class3.data;
                        if (this.makePeople_UserCallBack != null)
                        {
                            this.makePeople_UserCallBack(type136);
                        }
                    }
                    else if (class3.classType == typeof(GetUserPeople_ReturnType))
                    {
                        GetUserPeople_ReturnType type137 = (GetUserPeople_ReturnType) class3.data;
                        if (this.getUserPeople_UserCallBack != null)
                        {
                            this.getUserPeople_UserCallBack(type137);
                        }
                    }
                    else if (class3.classType == typeof(GetUserIDFromName_ReturnType))
                    {
                        GetUserIDFromName_ReturnType type138 = (GetUserIDFromName_ReturnType) class3.data;
                        if (this.getUserIDFromName_UserCallBack != null)
                        {
                            this.getUserIDFromName_UserCallBack(type138);
                        }
                    }
                    else if (class3.classType == typeof(GetActivePeople_ReturnType))
                    {
                        GetActivePeople_ReturnType type139 = (GetActivePeople_ReturnType) class3.data;
                        if (this.getActivePeople_UserCallBack != null)
                        {
                            this.getActivePeople_UserCallBack(type139);
                        }
                    }
                    else if (class3.classType == typeof(SendPeople_ReturnType))
                    {
                        SendPeople_ReturnType type140 = (SendPeople_ReturnType) class3.data;
                        if (this.sendPeople_UserCallBack != null)
                        {
                            this.sendPeople_UserCallBack(type140);
                        }
                    }
                    else if (class3.classType == typeof(RetrievePeople_ReturnType))
                    {
                        RetrievePeople_ReturnType type141 = (RetrievePeople_ReturnType) class3.data;
                        if (this.retrievePeople_UserCallBack != null)
                        {
                            this.retrievePeople_UserCallBack(type141);
                        }
                    }
                    else if (class3.classType == typeof(SpyCommand_ReturnType))
                    {
                        SpyCommand_ReturnType type142 = (SpyCommand_ReturnType) class3.data;
                        if (this.spyCommand_UserCallBack != null)
                        {
                            this.spyCommand_UserCallBack(type142);
                        }
                    }
                    else if (class3.classType == typeof(SpyGetVillageResourceInfo_ReturnType))
                    {
                        SpyGetVillageResourceInfo_ReturnType type143 = (SpyGetVillageResourceInfo_ReturnType) class3.data;
                        if (this.spyGetVillageResourceInfo_UserCallBack != null)
                        {
                            this.spyGetVillageResourceInfo_UserCallBack(type143);
                        }
                    }
                    else if (class3.classType == typeof(SpyGetArmyInfo_ReturnType))
                    {
                        SpyGetArmyInfo_ReturnType type144 = (SpyGetArmyInfo_ReturnType) class3.data;
                        if (this.spyGetArmyInfo_UserCallBack != null)
                        {
                            this.spyGetArmyInfo_UserCallBack(type144);
                        }
                    }
                    else if (class3.classType == typeof(SpyGetResearchInfo_ReturnType))
                    {
                        SpyGetResearchInfo_ReturnType type145 = (SpyGetResearchInfo_ReturnType) class3.data;
                        if (this.spyGetResearchInfo_UserCallBack != null)
                        {
                            this.spyGetResearchInfo_UserCallBack(type145);
                        }
                    }
                    else if (class3.classType == typeof(GetLoginHistory_ReturnType))
                    {
                        GetLoginHistory_ReturnType type146 = (GetLoginHistory_ReturnType) class3.data;
                        if (this.getLoginHistory_UserCallBack != null)
                        {
                            this.getLoginHistory_UserCallBack(type146);
                        }
                    }
                    else if (class3.classType == typeof(CreateFaction_ReturnType))
                    {
                        CreateFaction_ReturnType type147 = (CreateFaction_ReturnType) class3.data;
                        if (this.createFaction_UserCallBack != null)
                        {
                            this.createFaction_UserCallBack(type147);
                        }
                    }
                    else if (class3.classType == typeof(DisbandFaction_ReturnType))
                    {
                        DisbandFaction_ReturnType type148 = (DisbandFaction_ReturnType) class3.data;
                        if (this.disbandFaction_UserCallBack != null)
                        {
                            this.disbandFaction_UserCallBack(type148);
                        }
                    }
                    else if (class3.classType == typeof(FactionSendInvite_ReturnType))
                    {
                        FactionSendInvite_ReturnType type149 = (FactionSendInvite_ReturnType) class3.data;
                        if (this.factionSendInvite_UserCallBack != null)
                        {
                            this.factionSendInvite_UserCallBack(type149);
                        }
                    }
                    else if (class3.classType == typeof(FactionWithdrawInvite_ReturnType))
                    {
                        FactionWithdrawInvite_ReturnType type150 = (FactionWithdrawInvite_ReturnType) class3.data;
                        if (this.factionWithdrawInvite_UserCallBack != null)
                        {
                            this.factionWithdrawInvite_UserCallBack(type150);
                        }
                    }
                    else if (class3.classType == typeof(FactionReplyToInvite_ReturnType))
                    {
                        FactionReplyToInvite_ReturnType type151 = (FactionReplyToInvite_ReturnType) class3.data;
                        if (this.factionReplyToInvite_UserCallBack != null)
                        {
                            this.factionReplyToInvite_UserCallBack(type151);
                        }
                    }
                    else if (class3.classType == typeof(FactionChangeMemberStatus_ReturnType))
                    {
                        FactionChangeMemberStatus_ReturnType type152 = (FactionChangeMemberStatus_ReturnType) class3.data;
                        if (this.factionChangeMemberStatus_UserCallBack != null)
                        {
                            this.factionChangeMemberStatus_UserCallBack(type152);
                        }
                    }
                    else if (class3.classType == typeof(FactionLeave_ReturnType))
                    {
                        FactionLeave_ReturnType type153 = (FactionLeave_ReturnType) class3.data;
                        if (this.factionLeave_UserCallBack != null)
                        {
                            this.factionLeave_UserCallBack(type153);
                        }
                    }
                    else if (class3.classType == typeof(FactionApplication_ReturnType))
                    {
                        FactionApplication_ReturnType type154 = (FactionApplication_ReturnType) class3.data;
                        if (this.factionApplication_UserCallBack != null)
                        {
                            this.factionApplication_UserCallBack(type154);
                        }
                    }
                    else if (class3.classType == typeof(FactionApplicationProcessing_ReturnType))
                    {
                        FactionApplicationProcessing_ReturnType type155 = (FactionApplicationProcessing_ReturnType) class3.data;
                        if (this.factionApplicationProcessing_UserCallBack != null)
                        {
                            this.factionApplicationProcessing_UserCallBack(type155);
                        }
                    }
                    else if (class3.classType == typeof(GetFactionData_ReturnType))
                    {
                        GetFactionData_ReturnType type156 = (GetFactionData_ReturnType) class3.data;
                        if (this.getFactionData_UserCallBack != null)
                        {
                            this.getFactionData_UserCallBack(type156);
                        }
                    }
                    else if (class3.classType == typeof(FactionLeadershipVote_ReturnType))
                    {
                        FactionLeadershipVote_ReturnType type157 = (FactionLeadershipVote_ReturnType) class3.data;
                        if (this.factionLeadershipVote_UserCallBack != null)
                        {
                            this.factionLeadershipVote_UserCallBack(type157);
                        }
                    }
                    else if (class3.classType == typeof(CreateUserRelationship_ReturnType))
                    {
                        CreateUserRelationship_ReturnType type158 = (CreateUserRelationship_ReturnType) class3.data;
                        if (this.createUserRelationship_UserCallBack != null)
                        {
                            this.createUserRelationship_UserCallBack(type158);
                        }
                    }
                    else if (class3.classType == typeof(CreateFactionRelationship_ReturnType))
                    {
                        CreateFactionRelationship_ReturnType type159 = (CreateFactionRelationship_ReturnType) class3.data;
                        if (this.createFactionRelationship_UserCallBack != null)
                        {
                            this.createFactionRelationship_UserCallBack(type159);
                        }
                    }
                    else if (class3.classType == typeof(ChangeFactionMotto_ReturnType))
                    {
                        ChangeFactionMotto_ReturnType type160 = (ChangeFactionMotto_ReturnType) class3.data;
                        if (this.changeFactionMotto_UserCallBack != null)
                        {
                            this.changeFactionMotto_UserCallBack(type160);
                        }
                    }
                    else if (class3.classType == typeof(CreateHouseRelationship_ReturnType))
                    {
                        CreateHouseRelationship_ReturnType type161 = (CreateHouseRelationship_ReturnType) class3.data;
                        if (this.createHouseRelationship_UserCallBack != null)
                        {
                            this.createHouseRelationship_UserCallBack(type161);
                        }
                    }
                    else if (class3.classType == typeof(GetHouseGloryPoints_ReturnType))
                    {
                        GetHouseGloryPoints_ReturnType type162 = (GetHouseGloryPoints_ReturnType) class3.data;
                        if (this.getHouseGloryPoints_UserCallBack != null)
                        {
                            this.getHouseGloryPoints_UserCallBack(type162);
                        }
                    }
                    else if (class3.classType == typeof(GetViewFactionData_ReturnType))
                    {
                        GetViewFactionData_ReturnType type163 = (GetViewFactionData_ReturnType) class3.data;
                        if (this.getViewFactionData_UserCallBack != null)
                        {
                            this.getViewFactionData_UserCallBack(type163);
                        }
                    }
                    else if (class3.classType == typeof(GetViewHouseData_ReturnType))
                    {
                        GetViewHouseData_ReturnType type164 = (GetViewHouseData_ReturnType) class3.data;
                        if (this.getViewHouseData_UserCallBack != null)
                        {
                            this.getViewHouseData_UserCallBack(type164);
                        }
                    }
                    else if (class3.classType == typeof(SelfJoinHouse_ReturnType))
                    {
                        SelfJoinHouse_ReturnType type165 = (SelfJoinHouse_ReturnType) class3.data;
                        if (this.selfJoinHouse_UserCallBack != null)
                        {
                            this.selfJoinHouse_UserCallBack(type165);
                        }
                    }
                    else if (class3.classType == typeof(HouseVote_ReturnType))
                    {
                        HouseVote_ReturnType type166 = (HouseVote_ReturnType) class3.data;
                        if (this.houseVote_UserCallBack != null)
                        {
                            this.houseVote_UserCallBack(type166);
                        }
                    }
                    else if (class3.classType == typeof(HouseVoteHouseLeader_ReturnType))
                    {
                        HouseVoteHouseLeader_ReturnType type167 = (HouseVoteHouseLeader_ReturnType) class3.data;
                        if (this.houseVoteHouseLeader_UserCallBack != null)
                        {
                            this.houseVoteHouseLeader_UserCallBack(type167);
                        }
                    }
                    else if (class3.classType == typeof(TouchHouseVisitDate_ReturnType))
                    {
                        TouchHouseVisitDate_ReturnType type168 = (TouchHouseVisitDate_ReturnType) class3.data;
                        if (this.touchHouseVisitDate_UserCallBack != null)
                        {
                            this.touchHouseVisitDate_UserCallBack(type168);
                        }
                    }
                    else if (class3.classType == typeof(LeaveHouse_ReturnType))
                    {
                        LeaveHouse_ReturnType type169 = (LeaveHouse_ReturnType) class3.data;
                        if (this.leaveHouse_UserCallBack != null)
                        {
                            this.leaveHouse_UserCallBack(type169);
                        }
                    }
                    else if (class3.classType == typeof(GetParishMembersList_ReturnType))
                    {
                        GetParishMembersList_ReturnType type170 = (GetParishMembersList_ReturnType) class3.data;
                        if (this.getParishMembersList_UserCallBack != null)
                        {
                            this.getParishMembersList_UserCallBack(type170);
                        }
                    }
                    else if (class3.classType == typeof(GetParishFrontPageInfo_ReturnType))
                    {
                        GetParishFrontPageInfo_ReturnType type171 = (GetParishFrontPageInfo_ReturnType) class3.data;
                        if (this.getParishFrontPageInfo_UserCallBack != null)
                        {
                            this.getParishFrontPageInfo_UserCallBack(type171);
                        }
                    }
                    else if (class3.classType == typeof(ParishWallDetailInfo_ReturnType))
                    {
                        ParishWallDetailInfo_ReturnType type172 = (ParishWallDetailInfo_ReturnType) class3.data;
                        if (this.parishWallDetailInfo_UserCallBack != null)
                        {
                            this.parishWallDetailInfo_UserCallBack(type172);
                        }
                    }
                    else if (class3.classType == typeof(GetCountyElectionInfo_ReturnType))
                    {
                        GetCountyElectionInfo_ReturnType type173 = (GetCountyElectionInfo_ReturnType) class3.data;
                        if (this.getCountyElectionInfo_UserCallBack != null)
                        {
                            this.getCountyElectionInfo_UserCallBack(type173);
                        }
                    }
                    else if (class3.classType == typeof(GetCountyFrontPageInfo_ReturnType))
                    {
                        GetCountyFrontPageInfo_ReturnType type174 = (GetCountyFrontPageInfo_ReturnType) class3.data;
                        if (this.getCountyFrontPageInfo_UserCallBack != null)
                        {
                            this.getCountyFrontPageInfo_UserCallBack(type174);
                        }
                    }
                    else if (class3.classType == typeof(StandDownAsParishDespot_ReturnType))
                    {
                        StandDownAsParishDespot_ReturnType type175 = (StandDownAsParishDespot_ReturnType) class3.data;
                        if (this.standDownAsParishDespot_UserCallBack != null)
                        {
                            this.standDownAsParishDespot_UserCallBack(type175);
                        }
                    }
                    else if (class3.classType == typeof(MakeParishVote_ReturnType))
                    {
                        MakeParishVote_ReturnType type176 = (MakeParishVote_ReturnType) class3.data;
                        if (this.makeParishVote_UserCallBack != null)
                        {
                            this.makeParishVote_UserCallBack(type176);
                        }
                    }
                    else if (class3.classType == typeof(MakeCountyVote_ReturnType))
                    {
                        MakeCountyVote_ReturnType type177 = (MakeCountyVote_ReturnType) class3.data;
                        if (this.makeCountyVote_UserCallBack != null)
                        {
                            this.makeCountyVote_UserCallBack(type177);
                        }
                    }
                    else if (class3.classType == typeof(GetCountryElectionInfo_ReturnType))
                    {
                        GetCountryElectionInfo_ReturnType type178 = (GetCountryElectionInfo_ReturnType) class3.data;
                        if (this.getCountryElectionInfo_UserCallBack != null)
                        {
                            this.getCountryElectionInfo_UserCallBack(type178);
                        }
                    }
                    else if (class3.classType == typeof(GetCountryFrontPageInfo_ReturnType))
                    {
                        GetCountryFrontPageInfo_ReturnType type179 = (GetCountryFrontPageInfo_ReturnType) class3.data;
                        if (this.getCountryFrontPageInfo_UserCallBack != null)
                        {
                            this.getCountryFrontPageInfo_UserCallBack(type179);
                        }
                    }
                    else if (class3.classType == typeof(MakeCountryVote_ReturnType))
                    {
                        MakeCountryVote_ReturnType type180 = (MakeCountryVote_ReturnType) class3.data;
                        if (this.makeCountryVote_UserCallBack != null)
                        {
                            this.makeCountryVote_UserCallBack(type180);
                        }
                    }
                    else if (class3.classType == typeof(GetProvinceElectionInfo_ReturnType))
                    {
                        GetProvinceElectionInfo_ReturnType type181 = (GetProvinceElectionInfo_ReturnType) class3.data;
                        if (this.getProvinceElectionInfo_UserCallBack != null)
                        {
                            this.getProvinceElectionInfo_UserCallBack(type181);
                        }
                    }
                    else if (class3.classType == typeof(GetProvinceFrontPageInfo_ReturnType))
                    {
                        GetProvinceFrontPageInfo_ReturnType type182 = (GetProvinceFrontPageInfo_ReturnType) class3.data;
                        if (this.getProvinceFrontPageInfo_UserCallBack != null)
                        {
                            this.getProvinceFrontPageInfo_UserCallBack(type182);
                        }
                    }
                    else if (class3.classType == typeof(MakeProvinceVote_ReturnType))
                    {
                        MakeProvinceVote_ReturnType type183 = (MakeProvinceVote_ReturnType) class3.data;
                        if (this.makeProvinceVote_UserCallBack != null)
                        {
                            this.makeProvinceVote_UserCallBack(type183);
                        }
                    }
                    else if (class3.classType == typeof(SendTroopsToCapital_ReturnType))
                    {
                        SendTroopsToCapital_ReturnType type184 = (SendTroopsToCapital_ReturnType) class3.data;
                        if (this.sendTroopsToCapital_UserCallBack != null)
                        {
                            this.sendTroopsToCapital_UserCallBack(type184);
                        }
                    }
                    else if (class3.classType == typeof(GetCapitalBarracksSpace_ReturnType))
                    {
                        GetCapitalBarracksSpace_ReturnType type185 = (GetCapitalBarracksSpace_ReturnType) class3.data;
                        if (this.getCapitalBarracksSpace_UserCallBack != null)
                        {
                            this.getCapitalBarracksSpace_UserCallBack(type185);
                        }
                    }
                    else if (class3.classType == typeof(GetIngameMessage_ReturnType))
                    {
                        GetIngameMessage_ReturnType type186 = (GetIngameMessage_ReturnType) class3.data;
                        if (this.getIngameMessage_UserCallBack != null)
                        {
                            this.getIngameMessage_UserCallBack(type186);
                        }
                    }
                    else if (class3.classType == typeof(CancelInterdiction_ReturnType))
                    {
                        CancelInterdiction_ReturnType type187 = (CancelInterdiction_ReturnType) class3.data;
                        if (this.cancelInterdiction_UserCallBack != null)
                        {
                            this.cancelInterdiction_UserCallBack(type187);
                        }
                    }
                    else if (class3.classType == typeof(GetExcommunicationStatus_ReturnType))
                    {
                        GetExcommunicationStatus_ReturnType type188 = (GetExcommunicationStatus_ReturnType) class3.data;
                        if (this.getExcommunicationStatus_UserCallBack != null)
                        {
                            this.getExcommunicationStatus_UserCallBack(type188);
                        }
                    }
                    else if (class3.classType == typeof(InitialiseFreeCards_ReturnType))
                    {
                        InitialiseFreeCards_ReturnType type189 = (InitialiseFreeCards_ReturnType) class3.data;
                        if (this.initialiseFreeCards_UserCallBack != null)
                        {
                            this.initialiseFreeCards_UserCallBack(type189);
                        }
                    }
                    else if (class3.classType == typeof(TestAchievements_ReturnType))
                    {
                        TestAchievements_ReturnType type190 = (TestAchievements_ReturnType) class3.data;
                        if (this.testAchievements_UserCallBack != null)
                        {
                            this.testAchievements_UserCallBack(type190);
                        }
                    }
                    else if (class3.classType == typeof(AchievementProgress_ReturnType))
                    {
                        AchievementProgress_ReturnType type191 = (AchievementProgress_ReturnType) class3.data;
                        if (this.achievementProgress_UserCallBack != null)
                        {
                            this.achievementProgress_UserCallBack(type191);
                        }
                    }
                    else if (class3.classType == typeof(GetQuestData_ReturnType))
                    {
                        GetQuestData_ReturnType type192 = (GetQuestData_ReturnType) class3.data;
                        if (this.getQuestData_UserCallBack != null)
                        {
                            this.getQuestData_UserCallBack(type192);
                        }
                    }
                    else if (class3.classType == typeof(StartNewQuest_ReturnType))
                    {
                        StartNewQuest_ReturnType type193 = (StartNewQuest_ReturnType) class3.data;
                        if (this.startNewQuest_UserCallBack != null)
                        {
                            this.startNewQuest_UserCallBack(type193);
                        }
                    }
                    else if (class3.classType == typeof(CompleteAbandonNewQuest_ReturnType))
                    {
                        CompleteAbandonNewQuest_ReturnType type194 = (CompleteAbandonNewQuest_ReturnType) class3.data;
                        if (this.completeAbandonNewQuest_UserCallBack != null)
                        {
                            this.completeAbandonNewQuest_UserCallBack(type194);
                        }
                    }
                    else if (class3.classType == typeof(SpinTheWheel_ReturnType))
                    {
                        SpinTheWheel_ReturnType type195 = (SpinTheWheel_ReturnType) class3.data;
                        if (this.spinTheWheel_UserCallBack != null)
                        {
                            this.spinTheWheel_UserCallBack(type195);
                        }
                    }
                    else if (class3.classType == typeof(SetVacationMode_ReturnType))
                    {
                        SetVacationMode_ReturnType type196 = (SetVacationMode_ReturnType) class3.data;
                        if (this.setVacationMode_UserCallBack != null)
                        {
                            this.setVacationMode_UserCallBack(type196);
                        }
                    }
                    else if (class3.classType == typeof(PremiumOverview_ReturnType))
                    {
                        PremiumOverview_ReturnType type197 = (PremiumOverview_ReturnType) class3.data;
                        if (this.premiumOverview_UserCallBack != null)
                        {
                            this.premiumOverview_UserCallBack(type197);
                        }
                    }
                    else if (class3.classType == typeof(GetLastAttacker_ReturnType))
                    {
                        GetLastAttacker_ReturnType type198 = (GetLastAttacker_ReturnType) class3.data;
                        if (this.getLastAttacker_UserCallBack != null)
                        {
                            this.getLastAttacker_UserCallBack(type198);
                        }
                    }
                    else if (class3.classType == typeof(GetInvasionInfo_ReturnType))
                    {
                        GetInvasionInfo_ReturnType type199 = (GetInvasionInfo_ReturnType) class3.data;
                        if (this.getInvasionInfo_UserCallBack != null)
                        {
                            this.getInvasionInfo_UserCallBack(type199);
                        }
                    }
                    else if (class3.classType == typeof(PreValidateCardToBePlayed_ReturnType))
                    {
                        PreValidateCardToBePlayed_ReturnType type200 = (PreValidateCardToBePlayed_ReturnType) class3.data;
                        if (this.preValidateCardToBePlayed_UserCallBack != null)
                        {
                            this.preValidateCardToBePlayed_UserCallBack(type200);
                        }
                    }
                    else if (class3.classType == typeof(WorldInfo_ReturnType))
                    {
                        WorldInfo_ReturnType type201 = (WorldInfo_ReturnType) class3.data;
                        if (this.worldInfo_UserCallBack != null)
                        {
                            this.worldInfo_UserCallBack(type201);
                        }
                    }
                    else if (class3.classType == typeof(Chat_Login_ReturnType))
                    {
                        Chat_Login_ReturnType type202 = (Chat_Login_ReturnType) class3.data;
                        if (this.chat_Login_UserCallBack != null)
                        {
                            this.chat_Login_UserCallBack(type202);
                        }
                    }
                    else if (class3.classType == typeof(Chat_Logout_ReturnType))
                    {
                        Chat_Logout_ReturnType type203 = (Chat_Logout_ReturnType) class3.data;
                        if (this.chat_Logout_UserCallBack != null)
                        {
                            this.chat_Logout_UserCallBack(type203);
                        }
                    }
                    else if (class3.classType == typeof(Chat_SetReceivingState_ReturnType))
                    {
                        Chat_SetReceivingState_ReturnType type204 = (Chat_SetReceivingState_ReturnType) class3.data;
                        if (this.chat_SetReceivingState_UserCallBack != null)
                        {
                            this.chat_SetReceivingState_UserCallBack(type204);
                        }
                    }
                    else if (class3.classType == typeof(Chat_SendText_ReturnType))
                    {
                        Chat_SendText_ReturnType type205 = (Chat_SendText_ReturnType) class3.data;
                        if (this.chat_SendText_UserCallBack != null)
                        {
                            this.chat_SendText_UserCallBack(type205);
                        }
                    }
                    else if (class3.classType == typeof(Chat_ReceiveText_ReturnType))
                    {
                        Chat_ReceiveText_ReturnType type206 = (Chat_ReceiveText_ReturnType) class3.data;
                        if (this.chat_ReceiveText_UserCallBack != null)
                        {
                            this.chat_ReceiveText_UserCallBack(type206);
                        }
                    }
                    else if (class3.classType == typeof(Chat_SendParishText_ReturnType))
                    {
                        Chat_SendParishText_ReturnType type207 = (Chat_SendParishText_ReturnType) class3.data;
                        if (this.chat_SendParishText_UserCallBack != null)
                        {
                            this.chat_SendParishText_UserCallBack(type207);
                        }
                    }
                    else if (class3.classType == typeof(Chat_ReceiveParishText_ReturnType))
                    {
                        Chat_ReceiveParishText_ReturnType type208 = (Chat_ReceiveParishText_ReturnType) class3.data;
                        if (this.chat_ReceiveParishText_UserCallBack != null)
                        {
                            this.chat_ReceiveParishText_UserCallBack(type208);
                        }
                    }
                    else if (class3.classType == typeof(Chat_BackFillParishText_ReturnType))
                    {
                        Chat_BackFillParishText_ReturnType type209 = (Chat_BackFillParishText_ReturnType) class3.data;
                        if (this.chat_BackFillParishText_UserCallBack != null)
                        {
                            this.chat_BackFillParishText_UserCallBack(type209);
                        }
                    }
                    else if (class3.classType == typeof(Chat_MarkParishTextRead_ReturnType))
                    {
                        Chat_MarkParishTextRead_ReturnType type210 = (Chat_MarkParishTextRead_ReturnType) class3.data;
                        if (this.chat_MarkParishTextRead_UserCallBack != null)
                        {
                            this.chat_MarkParishTextRead_UserCallBack(type210);
                        }
                    }
                    else if (class3.classType == typeof(Chat_Admin_Command_ReturnType))
                    {
                        Chat_Admin_Command_ReturnType type211 = (Chat_Admin_Command_ReturnType) class3.data;
                        if (this.chat_Admin_Command_UserCallBack != null)
                        {
                            this.chat_Admin_Command_UserCallBack(type211);
                        }
                    }
                }
            Label_3601:
                class3.data = null;
            }
            if (flag2)
            {
                this.resultList.Clear();
            }
            this.inResultsProcessing = false;
            if (this.queuedResultList.Count > 0)
            {
                bool flag3 = false;
                bool flag4 = true;
                foreach (CallBackEntryClass class5 in this.queuedResultList)
                {
                    int count = this.resultList.Count;
                    if (flag4)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (this.resultList[i].state == 0)
                            {
                                this.resultList[i] = class5;
                                flag3 = true;
                                break;
                            }
                        }
                    }
                    if (!flag3)
                    {
                        flag4 = false;
                        this.resultList.Add(class5);
                    }
                }
                this.queuedResultList.Clear();
            }
        }

        public bool queueEmpty()
        {
            int count = this.resultList.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.resultList[i].state != 0)
                {
                    return false;
                }
            }
            if (this.queuedResultList.Count > 0)
            {
                return false;
            }
            return true;
        }

        public void registerRPCcall(IAsyncResult RemAr, Type classType)
        {
            lock (syncLock)
            {
                if (this.SessionID == 0)
                {
                    this.RTTAverageTime++;
                }
                CallBackEntryClass item = new CallBackEntryClass {
                    ar = RemAr,
                    classType = classType,
                    data = null,
                    timer = DXTimer.GetCurrentMilliseconds()
                };
                if (!this.inResultsProcessing)
                {
                    int count = this.resultList.Count;
                    bool flag = false;
                    for (int i = 0; i < count; i++)
                    {
                        if (this.resultList[i].state == 0)
                        {
                            this.resultList[i] = item;
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        this.resultList.Add(item);
                    }
                }
                else
                {
                    this.queuedResultList.Add(item);
                }
            }
        }

        public void RemoveMailFolder(long folderID)
        {
            if (this.RemoveMailFolder_Callback == null)
            {
                this.RemoveMailFolder_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RemoveMailFolder);
            }
            RemoteAsyncDelegate_RemoveMailFolder folder = new RemoteAsyncDelegate_RemoveMailFolder(this.service.RemoveMailFolder);
            this.registerRPCcall(folder.BeginInvoke(this.UserID, this.SessionID, folderID, this.RemoveMailFolder_Callback, null), typeof(RemoveMailFolder_ReturnType));
        }

        public void removeRPCresult(IAsyncResult ar)
        {
            foreach (CallBackEntryClass class2 in this.resultList)
            {
                if ((class2.state == 1) && (class2.ar == ar))
                {
                    class2.state = 0;
                    return;
                }
            }
            foreach (CallBackEntryClass class3 in this.queuedResultList)
            {
                if ((class3.state == 1) && (class3.ar == ar))
                {
                    class3.state = 0;
                    break;
                }
            }
        }

        public void RemoveUserFromFavourites(string userName)
        {
            if (this.AddUserToFavourites_Callback == null)
            {
                this.AddUserToFavourites_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_AddUserToFavourites);
            }
            RemoteAsyncDelegate_AddUserToFavourites favourites = new RemoteAsyncDelegate_AddUserToFavourites(this.service.AddUserToFavourites);
            this.registerRPCcall(favourites.BeginInvoke(this.UserID, this.SessionID, userName, true, this.AddUserToFavourites_Callback, null), typeof(AddUserToFavourites_ReturnType));
        }

        public void ReportMail(long mailID, long threadID, string reason, string summary)
        {
            if (this.ReportMail_Callback == null)
            {
                this.ReportMail_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ReportMail);
            }
            RemoteAsyncDelegate_ReportMail mail = new RemoteAsyncDelegate_ReportMail(this.service.ReportMail);
            this.registerRPCcall(mail.BeginInvoke(this.UserID, this.SessionID, mailID, threadID, reason, summary, this.ReportMail_Callback, null), typeof(ReportMail_ReturnType));
        }

        public void ResendVerificationEmail(string username, string password)
        {
            if (this.ResendVerificationEmail_Callback == null)
            {
                this.ResendVerificationEmail_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ResendVerificationEmail);
            }
            RemoteAsyncDelegate_ResendVerificationEmail email = new RemoteAsyncDelegate_ResendVerificationEmail(this.service.ResendVerificationEmail);
            this.registerRPCcall(email.BeginInvoke(username, password, this.ResendVerificationEmail_Callback, null), typeof(ResendVerificationEmail_ReturnType));
        }

        public void RestoreCastleTroops(int villageID)
        {
            if (this.RestoreCastleTroops_Callback == null)
            {
                this.RestoreCastleTroops_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RestoreCastleTroops);
            }
            RemoteAsyncDelegate_RestoreCastleTroops troops = new RemoteAsyncDelegate_RestoreCastleTroops(this.service.RestoreCastleTroops);
            this.registerRPCcall(troops.BeginInvoke(this.UserID, this.SessionID, villageID, this.RestoreCastleTroops_Callback, null), typeof(RestoreCastleTroops_ReturnType));
        }

        public void RetrieveArmyFromGarrison(int villageID)
        {
            if (this.RetrieveArmyFromGarrison_Callback == null)
            {
                this.RetrieveArmyFromGarrison_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RetrieveArmyFromGarrison);
            }
            RemoteAsyncDelegate_RetrieveArmyFromGarrison garrison = new RemoteAsyncDelegate_RetrieveArmyFromGarrison(this.service.RetrieveArmyFromGarrison);
            this.registerRPCcall(garrison.BeginInvoke(this.UserID, this.SessionID, villageID, this.RetrieveArmyFromGarrison_Callback, null), typeof(RetrieveArmyFromGarrison_ReturnType));
        }

        public void RetrieveAttackResult(long armyID, long startChangePos)
        {
            if (this.RetrieveAttackResult_Callback == null)
            {
                this.RetrieveAttackResult_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RetrieveAttackResult);
            }
            RemoteAsyncDelegate_RetrieveAttackResult result = new RemoteAsyncDelegate_RetrieveAttackResult(this.service.RetrieveAttackResult);
            this.registerRPCcall(result.BeginInvoke(this.UserID, this.SessionID, armyID, startChangePos, this.RetrieveAttackResult_Callback, null), typeof(RetrieveAttackResult_ReturnType));
        }

        public void RetrievePeople(List<long> people, int villageID, int personType)
        {
            if (this.RetrievePeople_Callback == null)
            {
                this.RetrievePeople_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RetrievePeople);
            }
            RemoteAsyncDelegate_RetrievePeople people2 = new RemoteAsyncDelegate_RetrievePeople(this.service.RetrievePeople);
            this.registerRPCcall(people2.BeginInvoke(this.UserID, this.SessionID, villageID, people, personType, this.RetrievePeople_Callback, null), typeof(RetrievePeople_ReturnType));
        }

        public void RetrieveStats()
        {
            if (this.RetrieveStats_Callback == null)
            {
                this.RetrieveStats_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RetrieveStats);
            }
            RemoteAsyncDelegate_RetrieveStats stats = new RemoteAsyncDelegate_RetrieveStats(this.service.RetrieveStats);
            this.registerRPCcall(stats.BeginInvoke(this.UserID, this.SessionID, this.RetrieveStats_Callback, null), typeof(RetrieveStats_ReturnType));
        }

        public void RetrieveTroopsFromVassal(int liegeLordVillageID, int vassalVillageID)
        {
            if (this.RetrieveTroopsFromVassal_Callback == null)
            {
                this.RetrieveTroopsFromVassal_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RetrieveTroopsFromVassal);
            }
            RemoteAsyncDelegate_RetrieveTroopsFromVassal vassal = new RemoteAsyncDelegate_RetrieveTroopsFromVassal(this.service.RetrieveTroopsFromVassal);
            this.registerRPCcall(vassal.BeginInvoke(this.UserID, this.SessionID, liegeLordVillageID, vassalVillageID, this.RetrieveTroopsFromVassal_Callback, null), typeof(RetrieveTroopsFromVassal_ReturnType));
        }

        public void RetrieveVillageUserInfo(int villageID, int targetUserID, bool extended)
        {
            if (this.RetrieveVillageUserInfo_Callback == null)
            {
                this.RetrieveVillageUserInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_RetrieveVillageUserInfo);
            }
            RemoteAsyncDelegate_RetrieveVillageUserInfo info = new RemoteAsyncDelegate_RetrieveVillageUserInfo(this.service.RetrieveVillageUserInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, targetUserID, extended, this.RetrieveVillageUserInfo_Callback, null), typeof(RetrieveVillageUserInfo_ReturnType));
        }

        public void ReturnReinforcements(long reinforcementID)
        {
            if (this.ReturnReinforcements_Callback == null)
            {
                this.ReturnReinforcements_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ReturnReinforcements);
            }
            RemoteAsyncDelegate_ReturnReinforcements reinforcements = new RemoteAsyncDelegate_ReturnReinforcements(this.service.ReturnReinforcements);
            this.registerRPCcall(reinforcements.BeginInvoke(this.UserID, this.SessionID, reinforcementID, -1, -1, -1, -1, -1, this.ReturnReinforcements_Callback, null), typeof(ReturnReinforcements_ReturnType));
        }

        public void ReturnReinforcements(long reinforcementID, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults)
        {
            if (this.ReturnReinforcements_Callback == null)
            {
                this.ReturnReinforcements_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ReturnReinforcements);
            }
            RemoteAsyncDelegate_ReturnReinforcements reinforcements = new RemoteAsyncDelegate_ReturnReinforcements(this.service.ReturnReinforcements);
            this.registerRPCcall(reinforcements.BeginInvoke(this.UserID, this.SessionID, reinforcementID, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, this.ReturnReinforcements_Callback, null), typeof(ReturnReinforcements_ReturnType));
        }

        public void SelfJoinHouse(int factionID, int houseID, long factionsChangePos)
        {
            if (this.SelfJoinHouse_Callback == null)
            {
                this.SelfJoinHouse_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SelfJoinHouse);
            }
            RemoteAsyncDelegate_SelfJoinHouse house = new RemoteAsyncDelegate_SelfJoinHouse(this.service.SelfJoinHouse);
            this.registerRPCcall(house.BeginInvoke(this.UserID, this.SessionID, factionID, houseID, factionsChangePos, this.SelfJoinHouse_Callback, null), typeof(SelfJoinHouse_ReturnType));
        }

        public void SendCommands(int targetUserID, int command, int duration, string reason)
        {
            if (this.SendCommands_Callback == null)
            {
                this.SendCommands_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendCommands);
            }
            RemoteAsyncDelegate_SendCommands commands = new RemoteAsyncDelegate_SendCommands(this.service.SendCommands);
            this.registerRPCcall(commands.BeginInvoke(this.UserID, this.SessionID, targetUserID, command, duration, reason, this.SendCommands_Callback, null), typeof(SendCommands_ReturnType));
        }

        public void SendMail(string subject, string body, string[] recipients, long threadID, bool forwardThread)
        {
            if (this.SendMail_Callback == null)
            {
                this.SendMail_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendMail);
            }
            RemoteAsyncDelegate_SendMail mail = new RemoteAsyncDelegate_SendMail(this.service.SendMail);
            this.registerRPCcall(mail.BeginInvoke(this.UserID, this.SessionID, subject, body, recipients, threadID, forwardThread, this.SendMail_Callback, null), typeof(SendMail_ReturnType));
        }

        public void SendMarketResources(int homeVillageID, int targetVillage, int resource, int amount)
        {
            if (this.SendMarketResources_Callback == null)
            {
                this.SendMarketResources_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendMarketResources);
            }
            RemoteAsyncDelegate_SendMarketResources resources = new RemoteAsyncDelegate_SendMarketResources(this.service.SendMarketResources);
            this.registerRPCcall(resources.BeginInvoke(this.UserID, this.SessionID, homeVillageID, targetVillage, resource, amount, this.SendMarketResources_Callback, null), typeof(SendMarketResources_ReturnType));
        }

        public void SendPeople(int homeVillage, int targetVillage, int personType, int number, int command, int data)
        {
            if (this.SendPeople_Callback == null)
            {
                this.SendPeople_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendPeople);
            }
            RemoteAsyncDelegate_SendPeople people = new RemoteAsyncDelegate_SendPeople(this.service.SendPeople);
            this.registerRPCcall(people.BeginInvoke(this.UserID, this.SessionID, homeVillage, targetVillage, personType, number, command, data, this.SendPeople_Callback, null), typeof(SendPeople_ReturnType));
        }

        public void SendReinforcements(int sourceVillageID, int targetVillageID, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults)
        {
            if (this.SendReinforcements_Callback == null)
            {
                this.SendReinforcements_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendReinforcements);
            }
            RemoteAsyncDelegate_SendReinforcements reinforcements = new RemoteAsyncDelegate_SendReinforcements(this.service.SendReinforcements);
            this.registerRPCcall(reinforcements.BeginInvoke(this.UserID, this.SessionID, sourceVillageID, targetVillageID, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, this.SendReinforcements_Callback, null), typeof(SendReinforcements_ReturnType));
        }

        public void SendScouts(int sourceVillageID, int targetVillageID, int numScouts)
        {
            if (this.SendScouts_Callback == null)
            {
                this.SendScouts_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendScouts);
            }
            RemoteAsyncDelegate_SendScouts scouts = new RemoteAsyncDelegate_SendScouts(this.service.SendScouts);
            this.registerRPCcall(scouts.BeginInvoke(this.UserID, this.SessionID, targetVillageID, sourceVillageID, numScouts, this.SendScouts_Callback, null), typeof(SendScouts_ReturnType));
        }

        public void SendSpecialMail(int mailType, int area, string subject, string body)
        {
            if (this.SendSpecialMail_Callback == null)
            {
                this.SendSpecialMail_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendSpecialMail);
            }
            RemoteAsyncDelegate_SendSpecialMail mail = new RemoteAsyncDelegate_SendSpecialMail(this.service.SendSpecialMail);
            this.registerRPCcall(mail.BeginInvoke(this.UserID, this.SessionID, mailType, area, subject, body, this.SendSpecialMail_Callback, null), typeof(SendSpecialMail_ReturnType));
        }

        public void SendTroopsToCapital(int sourceVillageID, int targetVillageID, int peasants, int archers, int pikemen, int swordsmen, int catapults)
        {
            if (this.SendTroopsToCapital_Callback == null)
            {
                this.SendTroopsToCapital_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendTroopsToCapital);
            }
            RemoteAsyncDelegate_SendTroopsToCapital capital = new RemoteAsyncDelegate_SendTroopsToCapital(this.service.SendTroopsToCapital);
            this.registerRPCcall(capital.BeginInvoke(this.UserID, this.SessionID, sourceVillageID, targetVillageID, peasants, archers, pikemen, swordsmen, catapults, this.SendTroopsToCapital_Callback, null), typeof(SendTroopsToCapital_ReturnType));
        }

        public void SendTroopsToVassal(int liegeLordVillageID, int vassalVillageID, int peasants, int archers, int pikemen, int swordsmen, int catapults)
        {
            if (this.SendTroopsToVassal_Callback == null)
            {
                this.SendTroopsToVassal_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendTroopsToVassal);
            }
            RemoteAsyncDelegate_SendTroopsToVassal vassal = new RemoteAsyncDelegate_SendTroopsToVassal(this.service.SendTroopsToVassal);
            this.registerRPCcall(vassal.BeginInvoke(this.UserID, this.SessionID, liegeLordVillageID, vassalVillageID, peasants, archers, pikemen, swordsmen, catapults, this.SendTroopsToVassal_Callback, null), typeof(SendTroopsToVassal_ReturnType));
        }

        public void SendVassalRequest(int villageID, int targetVillage)
        {
            if (this.SendVassalRequest_Callback == null)
            {
                this.SendVassalRequest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SendVassalRequest);
            }
            RemoteAsyncDelegate_SendVassalRequest request = new RemoteAsyncDelegate_SendVassalRequest(this.service.SendVassalRequest);
            this.registerRPCcall(request.BeginInvoke(this.UserID, this.SessionID, villageID, targetVillage, this.SendVassalRequest_Callback, null), typeof(SendVassalRequest_ReturnType));
        }

        public void set_AchievementProgress_UserCallBack(AchievementProgress_UserCallBack callback)
        {
            this.achievementProgress_UserCallBack = callback;
        }

        public void set_AddCastleElement_UserCallBack(AddCastleElement_UserCallBack callback)
        {
            this.addCastleElement_UserCallBack = callback;
        }

        public void set_AddUserToFavourites_UserCallBack(AddUserToFavourites_UserCallBack callback)
        {
            this.addUserToFavourites_UserCallBack = callback;
        }

        public void set_ArmyAttack_UserCallBack(ArmyAttack_UserCallBack callback)
        {
            this.armyAttack_UserCallBack = callback;
        }

        public void set_AutoRepairCastle_UserCallBack(AutoRepairCastle_UserCallBack callback)
        {
            this.autoRepairCastle_UserCallBack = callback;
        }

        public void set_BreakLiegeLord_UserCallBack(BreakLiegeLord_UserCallBack callback)
        {
            this.breakLiegeLord_UserCallBack = callback;
        }

        public void set_BreakVassalage_UserCallBack(BreakVassalage_UserCallBack callback)
        {
            this.breakVassalage_UserCallBack = callback;
        }

        public void set_BuyResearchPoint_UserCallBack(BuyResearchPoint_UserCallBack callback)
        {
            this.buyResearchPoint_UserCallBack = callback;
        }

        public void set_BuyVillage_UserCallBack(BuyVillage_UserCallBack callback)
        {
            this.buyVillage_UserCallBack = callback;
        }

        public void set_CancelCard_UserCallBack(CancelCard_UserCallBack callback)
        {
            this.cancelCard_UserCallBack = callback;
        }

        public void set_CancelCastleAttack_UserCallBack(CancelCastleAttack_UserCallBack callback)
        {
            this.cancelCastleAttack_UserCallBack = callback;
        }

        public void set_CancelDeleteVillageBuilding_UserCallBack(CancelDeleteVillageBuilding_UserCallBack callback)
        {
            this.cancelDeleteVillageBuilding_UserCallBack = callback;
        }

        public void set_CancelInterdiction_UserCallBack(CancelInterdiction_UserCallBack callback)
        {
            this.cancelInterdiction_UserCallBack = callback;
        }

        public void set_ChangeCastleElementAggressiveDefender_UserCallBack(ChangeCastleElementAggressiveDefender_UserCallBack callback)
        {
            this.changeCastleElementAggressiveDefender_UserCallBack = callback;
        }

        public void set_ChangeFactionMotto_UserCallBack(ChangeFactionMotto_UserCallBack callback)
        {
            this.changeFactionMotto_UserCallBack = callback;
        }

        public void set_Chat_Admin_Command_UserCallBack(Chat_Admin_Command_UserCallBack callback)
        {
            this.chat_Admin_Command_UserCallBack = callback;
        }

        public void set_Chat_BackFillParishText_UserCallBack(Chat_BackFillParishText_UserCallBack callback)
        {
            this.chat_BackFillParishText_UserCallBack = callback;
        }

        public void set_Chat_Login_UserCallBack(Chat_Login_UserCallBack callback)
        {
            this.chat_Login_UserCallBack = callback;
        }

        public void set_Chat_Logout_UserCallBack(Chat_Logout_UserCallBack callback)
        {
            this.chat_Logout_UserCallBack = callback;
        }

        public void set_Chat_MarkParishTextRead_UserCallBack(Chat_MarkParishTextRead_UserCallBack callback)
        {
            this.chat_MarkParishTextRead_UserCallBack = callback;
        }

        public void set_Chat_ReceiveParishText_UserCallBack(Chat_ReceiveParishText_UserCallBack callback)
        {
            this.chat_ReceiveParishText_UserCallBack = callback;
        }

        public void set_Chat_ReceiveText_UserCallBack(Chat_ReceiveText_UserCallBack callback)
        {
            this.chat_ReceiveText_UserCallBack = callback;
        }

        public void set_Chat_SendParishText_UserCallBack(Chat_SendParishText_UserCallBack callback)
        {
            this.chat_SendParishText_UserCallBack = callback;
        }

        public void set_Chat_SendText_UserCallBack(Chat_SendText_UserCallBack callback)
        {
            this.chat_SendText_UserCallBack = callback;
        }

        public void set_Chat_SetReceivingState_UserCallBack(Chat_SetReceivingState_UserCallBack callback)
        {
            this.chat_SetReceivingState_UserCallBack = callback;
        }

        public void set_CheatAddTroops_UserCallBack(CheatAddTroops_UserCallBack callback)
        {
            this.cheatAddTroops_UserCallBack = callback;
        }

        public void set_CheckQuestObjectiveComplete_UserCallBack(CheckQuestObjectiveComplete_UserCallBack callback)
        {
            this.checkQuestObjectiveComplete_UserCallBack = callback;
        }

        public void set_CommonData_UserCallBack(CommonData_UserCallBack callback)
        {
            this.commonData_UserCallBack = callback;
        }

        public void set_CompleteAbandonNewQuest_UserCallBack(CompleteAbandonNewQuest_UserCallBack callback)
        {
            this.completeAbandonNewQuest_UserCallBack = callback;
        }

        public void set_CompleteQuest_UserCallBack(CompleteQuest_UserCallBack callback)
        {
            this.completeQuest_UserCallBack = callback;
        }

        public void set_CompleteVillageCastle_UserCallBack(CompleteVillageCastle_UserCallBack callback)
        {
            this.completeVillageCastle_UserCallBack = callback;
        }

        public void set_ConvertVillage_UserCallBack(ConvertVillage_UserCallBack callback)
        {
            this.convertVillage_UserCallBack = callback;
        }

        public void set_CreateFaction_UserCallBack(CreateFaction_UserCallBack callback)
        {
            this.createFaction_UserCallBack = callback;
        }

        public void set_CreateFactionRelationship_UserCallBack(CreateFactionRelationship_UserCallBack callback)
        {
            this.createFactionRelationship_UserCallBack = callback;
        }

        public void set_CreateForum_UserCallBack(CreateForum_UserCallBack callback)
        {
            this.createForum_UserCallBack = callback;
        }

        public void set_CreateHouseRelationship_UserCallBack(CreateHouseRelationship_UserCallBack callback)
        {
            this.createHouseRelationship_UserCallBack = callback;
        }

        public void set_CreateMailFolder_UserCallBack(CreateMailFolder_UserCallBack callback)
        {
            this.createMailFolder_UserCallBack = callback;
        }

        public void set_CreateNewUser_UserCallBack(CreateNewUser_UserCallBack callback)
        {
            this.createNewUser_UserCallBack = callback;
        }

        public void set_CreateUserRelationship_UserCallBack(CreateUserRelationship_UserCallBack callback)
        {
            this.createUserRelationship_UserCallBack = callback;
        }

        public void set_DeleteCastleElement_UserCallBack(DeleteCastleElement_UserCallBack callback)
        {
            this.deleteCastleElement_UserCallBack = callback;
        }

        public void set_DeleteForum_UserCallBack(DeleteForum_UserCallBack callback)
        {
            this.deleteForum_UserCallBack = callback;
        }

        public void set_DeleteForumPost_UserCallBack(DeleteForumPost_UserCallBack callback)
        {
            this.deleteForumPost_UserCallBack = callback;
        }

        public void set_DeleteForumThread_UserCallBack(DeleteForumThread_UserCallBack callback)
        {
            this.deleteForumThread_UserCallBack = callback;
        }

        public void set_DeleteMailThread_UserCallBack(DeleteMailThread_UserCallBack callback)
        {
            this.deleteMailThread_UserCallBack = callback;
        }

        public void set_DeleteOrMoveReports_UserCallBack(DeleteReports_UserCallBack callback)
        {
            this.deleteReports_UserCallBack = callback;
        }

        public void set_DeleteVillageBuilding_UserCallBack(DeleteVillageBuilding_UserCallBack callback)
        {
            this.deleteVillageBuilding_UserCallBack = callback;
        }

        public void set_DisbandFaction_UserCallBack(DisbandFaction_UserCallBack callback)
        {
            this.disbandFaction_UserCallBack = callback;
        }

        public void set_DisbandPeople_UserCallBack(DisbandPeople_UserCallBack callback)
        {
            this.disbandPeople_UserCallBack = callback;
        }

        public void set_DisbandTroops_UserCallBack(DisbandTroops_UserCallBack callback)
        {
            this.disbandTroops_UserCallBack = callback;
        }

        public void set_DonateCapitalGoods_UserCallBack(DonateCapitalGoods_UserCallBack callback)
        {
            this.donateCapitalGoods_UserCallBack = callback;
        }

        public void set_DoResearch_UserCallBack(DoResearch_UserCallBack callback)
        {
            this.doResearch_UserCallBack = callback;
        }

        public void set_FactionApplication_UserCallBack(FactionApplication_UserCallBack callback)
        {
            this.factionApplication_UserCallBack = callback;
        }

        public void set_FactionApplicationProcessing_UserCallBack(FactionApplicationProcessing_UserCallBack callback)
        {
            this.factionApplicationProcessing_UserCallBack = callback;
        }

        public void set_FactionChangeMemberStatus_UserCallBack(FactionChangeMemberStatus_UserCallBack callback)
        {
            this.factionChangeMemberStatus_UserCallBack = callback;
        }

        public void set_FactionLeadershipVote_UserCallBack(FactionLeadershipVote_UserCallBack callback)
        {
            this.factionLeadershipVote_UserCallBack = callback;
        }

        public void set_FactionLeave_UserCallBack(FactionLeave_UserCallBack callback)
        {
            this.factionLeave_UserCallBack = callback;
        }

        public void set_FactionReplyToInvite_UserCallBack(FactionReplyToInvite_UserCallBack callback)
        {
            this.factionReplyToInvite_UserCallBack = callback;
        }

        public void set_FactionSendInvite_UserCallBack(FactionSendInvite_UserCallBack callback)
        {
            this.factionSendInvite_UserCallBack = callback;
        }

        public void set_FactionWithdrawInvite_UserCallBack(FactionWithdrawInvite_UserCallBack callback)
        {
            this.factionWithdrawInvite_UserCallBack = callback;
        }

        public void set_FlagMailRead_UserCallBack(FlagMailRead_UserCallBack callback)
        {
            this.flagMailRead_UserCallBack = callback;
        }

        public void set_FlagQuestObjectiveComplete_UserCallBack(FlagQuestObjectiveComplete_UserCallBack callback)
        {
            this.flagQuestObjectiveComplete_UserCallBack = callback;
        }

        public void set_ForwardReport_UserCallBack(ForwardReport_UserCallBack callback)
        {
            this.forwardReport_UserCallBack = callback;
        }

        public void set_FullTick_UserCallBack(FullTick_UserCallBack callback)
        {
            this.fullTick_UserCallBack = callback;
        }

        public void set_GetActivePeople_UserCallBack(GetActivePeople_UserCallBack callback)
        {
            this.getActivePeople_UserCallBack = callback;
        }

        public void set_GetActiveTraders_UserCallBack(GetActiveTraders_UserCallBack callback)
        {
            this.getActiveTraders_UserCallBack = callback;
        }

        public void set_GetAdminStats_UserCallBack(GetAdminStats_UserCallBack callback)
        {
            this.getAdminStats_UserCallBack = callback;
        }

        public void set_GetAllVillageOwnerFactions_UserCallBack(GetAllVillageOwnerFactions_UserCallBack callback)
        {
            this.getAllVillageOwnerFactions_UserCallBack = callback;
        }

        public void set_GetAreaFactionChanges_UserCallBack(GetAreaFactionChanges_UserCallBack callback)
        {
            this.getAreaFactionChanges_UserCallBack = callback;
        }

        public void set_GetArmyData_UserCallBack(GetArmyData_UserCallBack callback)
        {
            this.getArmyData_UserCallBack = callback;
        }

        public void set_GetBattleHonourRating_UserCallBack(GetBattleHonourRating_UserCallBack callback)
        {
            this.getBattleHonourRating_UserCallBack = callback;
        }

        public void set_GetCapitalBarracksSpace_UserCallBack(GetCapitalBarracksSpace_UserCallBack callback)
        {
            this.getCapitalBarracksSpace_UserCallBack = callback;
        }

        public void set_GetCastle_UserCallBack(GetCastle_UserCallBack callback)
        {
            this.getCastle_UserCallBack = callback;
        }

        public void set_GetCountryElectionInfo_UserCallBack(GetCountryElectionInfo_UserCallBack callback)
        {
            this.getCountryElectionInfo_UserCallBack = callback;
        }

        public void set_GetCountryFrontPageInfo_UserCallBack(GetCountryFrontPageInfo_UserCallBack callback)
        {
            this.getCountryFrontPageInfo_UserCallBack = callback;
        }

        public void set_GetCountyElectionInfo_UserCallBack(GetCountyElectionInfo_UserCallBack callback)
        {
            this.getCountyElectionInfo_UserCallBack = callback;
        }

        public void set_GetCountyFrontPageInfo_UserCallBack(GetCountyFrontPageInfo_UserCallBack callback)
        {
            this.getCountyFrontPageInfo_UserCallBack = callback;
        }

        public void set_GetCurrentElectionInfo_UserCallBack(GetCurrentElectionInfo_UserCallBack callback)
        {
            this.getCurrentElectionInfo_UserCallBack = callback;
        }

        public void set_GetExcommunicationStatus_UserCallBack(GetExcommunicationStatus_UserCallBack callback)
        {
            this.getExcommunicationStatus_UserCallBack = callback;
        }

        public void set_GetFactionData_UserCallBack(GetFactionData_UserCallBack callback)
        {
            this.getFactionData_UserCallBack = callback;
        }

        public void set_GetForumList_UserCallBack(GetForumList_UserCallBack callback)
        {
            this.getForumList_UserCallBack = callback;
        }

        public void set_GetForumThread_UserCallBack(GetForumThread_UserCallBack callback)
        {
            this.getForumThread_UserCallBack = callback;
        }

        public void set_GetForumThreadList_UserCallBack(GetForumThreadList_UserCallBack callback)
        {
            this.getForumThreadList_UserCallBack = callback;
        }

        public void set_GetHistoricalData_UserCallBack(GetHistoricalData_UserCallBack callback)
        {
            this.getHistoricalData_UserCallBack = callback;
        }

        public void set_GetHouseGloryPoints_UserCallBack(GetHouseGloryPoints_UserCallBack callback)
        {
            this.getHouseGloryPoints_UserCallBack = callback;
        }

        public void set_GetIngameMessage_UserCallBack(GetIngameMessage_UserCallBack callback)
        {
            this.getIngameMessage_UserCallBack = callback;
        }

        public void set_GetInvasionInfo_UserCallBack(GetInvasionInfo_UserCallBack callback)
        {
            this.getInvasionInfo_UserCallBack = callback;
        }

        public void set_GetLastAttacker_UserCallBack(GetLastAttacker_UserCallBack callback)
        {
            this.getLastAttacker_UserCallBack = callback;
        }

        public void set_GetLoginHistory_UserCallBack(GetLoginHistory_UserCallBack callback)
        {
            this.getLoginHistory_UserCallBack = callback;
        }

        public void set_GetMailFolders_UserCallBack(GetMailFolders_UserCallBack callback)
        {
            this.getMailFolders_UserCallBack = callback;
        }

        public void set_GetMailRecipientsHistory_UserCallBack(GetMailRecipientsHistory_UserCallBack callback)
        {
            this.getMailRecipientsHistory_UserCallBack = callback;
        }

        public void set_GetMailThread_UserCallBack(GetMailThread_UserCallBack callback)
        {
            this.getMailThread_UserCallBack = callback;
        }

        public void set_GetMailThreadList_UserCallBack(GetMailThreadList_UserCallBack callback)
        {
            this.getMailThreadList_UserCallBack = callback;
        }

        public void set_GetMailUserSearch_UserCallBack(GetMailUserSearch_UserCallBack callback)
        {
            this.getMailUserSearch_UserCallBack = callback;
        }

        public void set_GetOtherUserVillageIDList_UserCallBack(GetOtherUserVillageIDList_UserCallBack callback)
        {
            this.getOtherUserVillageIDList_UserCallBack = callback;
        }

        public void set_GetParishFrontPageInfo_UserCallBack(GetParishFrontPageInfo_UserCallBack callback)
        {
            this.getParishFrontPageInfo_UserCallBack = callback;
        }

        public void set_GetParishMembersList_UserCallBack(GetParishMembersList_UserCallBack callback)
        {
            this.getParishMembersList_UserCallBack = callback;
        }

        public void set_GetPreVassalInfo_UserCallBack(GetPreVassalInfo_UserCallBack callback)
        {
            this.getPreVassalInfo_UserCallBack = callback;
        }

        public void set_GetProvinceElectionInfo_UserCallBack(GetProvinceElectionInfo_UserCallBack callback)
        {
            this.getProvinceElectionInfo_UserCallBack = callback;
        }

        public void set_GetProvinceFrontPageInfo_UserCallBack(GetProvinceFrontPageInfo_UserCallBack callback)
        {
            this.getProvinceFrontPageInfo_UserCallBack = callback;
        }

        public void set_GetQuestData_UserCallBack(GetQuestData_UserCallBack callback)
        {
            this.getQuestData_UserCallBack = callback;
        }

        public void set_GetQuestStatus_UserCallBack(GetQuestStatus_UserCallBack callback)
        {
            this.getQuestStatus_UserCallBack = callback;
        }

        public void set_GetReport_UserCallBack(GetReport_UserCallBack callback)
        {
            this.getReport_UserCallBack = callback;
        }

        public void set_GetReportsList_UserCallBack(GetReportsList_UserCallBack callback)
        {
            this.getReportsList_UserCallBack = callback;
        }

        public void set_GetResearchData_UserCallBack(GetResearchData_UserCallBack callback)
        {
            this.getResearchData_UserCallBack = callback;
        }

        public void set_GetResourceLevel_UserCallBack(GetResourceLevel_UserCallBack callback)
        {
            this.getResourceLevel_UserCallBack = callback;
        }

        public void set_GetStockExchangeData_UserCallBack(GetStockExchangeData_UserCallBack callback)
        {
            this.getStockExchangeData_UserCallBack = callback;
        }

        public void set_GetUserIDFromName_UserCallBack(GetUserIDFromName_UserCallBack callback)
        {
            this.getUserIDFromName_UserCallBack = callback;
        }

        public void set_GetUserPeople_UserCallBack(GetUserPeople_UserCallBack callback)
        {
            this.getUserPeople_UserCallBack = callback;
        }

        public void set_GetUserTraders_UserCallBack(GetUserTraders_UserCallBack callback)
        {
            this.getUserTraders_UserCallBack = callback;
        }

        public void set_GetUserVillages_UserCallBack(GetUserVillages_UserCallBack callback)
        {
            this.getUserVillages_UserCallBack = callback;
        }

        public void set_GetVassalArmyInfo_UserCallBack(GetVassalArmyInfo_UserCallBack callback)
        {
            this.getVassalArmyInfo_UserCallBack = callback;
        }

        public void set_GetViewFactionData_UserCallBack(GetViewFactionData_UserCallBack callback)
        {
            this.getViewFactionData_UserCallBack = callback;
        }

        public void set_GetViewHouseData_UserCallBack(GetViewHouseData_UserCallBack callback)
        {
            this.getViewHouseData_UserCallBack = callback;
        }

        public void set_GetVillageBuildingsList_UserCallBack(GetVillageBuildingsList_UserCallBack callback)
        {
            this.getVillageBuildingsList_UserCallBack = callback;
        }

        public void set_GetVillageFactionChanges_UserCallBack(GetVillageFactionChanges_UserCallBack callback)
        {
            this.getVillageFactionChanges_UserCallBack = callback;
        }

        public void set_GetVillageInfoForDonateCapitalGoods_UserCallBack(GetVillageInfoForDonateCapitalGoods_UserCallBack callback)
        {
            this.getVillageInfoForDonateCapitalGoods_UserCallBack = callback;
        }

        public void set_GetVillageNames_UserCallBack(GetVillageNames_UserCallBack callback)
        {
            this.getVillageNames_UserCallBack = callback;
        }

        public void set_GetVillageRankTaxTree_UserCallBack(GetVillageRankTaxTree_UserCallBack callback)
        {
            this.getVillageRankTaxTree_UserCallBack = callback;
        }

        public void set_GetVillageStartLocations_UserCallBack(GetVillageStartLocations_UserCallBack callback)
        {
            this.getVillageStartLocations_UserCallBack = callback;
        }

        public void set_GiveForumAccess_UserCallBack(GiveForumAccess_UserCallBack callback)
        {
            this.giveForumAccess_UserCallBack = callback;
        }

        public void set_HandleVassalRequest_UserCallBack(HandleVassalRequest_UserCallBack callback)
        {
            this.handleVassalRequest_UserCallBack = callback;
        }

        public void set_HouseVote_UserCallBack(HouseVote_UserCallBack callback)
        {
            this.houseVote_UserCallBack = callback;
        }

        public void set_HouseVoteHouseLeader_UserCallBack(HouseVoteHouseLeader_UserCallBack callback)
        {
            this.houseVoteHouseLeader_UserCallBack = callback;
        }

        public void set_InitialiseFreeCards_UserCallBack(InitialiseFreeCards_UserCallBack callback)
        {
            this.initialiseFreeCards_UserCallBack = callback;
        }

        public void set_LaunchCastleAttack_UserCallBack(LaunchCastleAttack_UserCallBack callback)
        {
            this.launchCastleAttack_UserCallBack = callback;
        }

        public void set_LeaderBoard_UserCallBack(LeaderBoard_UserCallBack callback)
        {
            this.leaderBoard_UserCallBack = callback;
        }

        public void set_LeaderBoardSearch_UserCallBack(LeaderBoardSearch_UserCallBack callback)
        {
            this.leaderBoardSearch_UserCallBack = callback;
        }

        public void set_LeaveHouse_UserCallBack(LeaveHouse_UserCallBack callback)
        {
            this.leaveHouse_UserCallBack = callback;
        }

        public void set_LoginUser_UserCallBack(LoginUser_UserCallBack callback)
        {
            this.loginUser_UserCallBack = callback;
        }

        public void set_LoginUserGuid_UserCallBack(LoginUserGuid_UserCallBack callback)
        {
            this.loginUserGuid_UserCallBack = callback;
        }

        public void set_LogOut_UserCallBack(LogOut_UserCallBack callback)
        {
            this.logOut_UserCallBack = callback;
        }

        public void set_MakeCountryVote_UserCallBack(MakeCountryVote_UserCallBack callback)
        {
            this.makeCountryVote_UserCallBack = callback;
        }

        public void set_MakeCountyVote_UserCallBack(MakeCountyVote_UserCallBack callback)
        {
            this.makeCountyVote_UserCallBack = callback;
        }

        public void set_MakeParishVote_UserCallBack(MakeParishVote_UserCallBack callback)
        {
            this.makeParishVote_UserCallBack = callback;
        }

        public void set_MakePeople_UserCallBack(MakePeople_UserCallBack callback)
        {
            this.makePeople_UserCallBack = callback;
        }

        public void set_MakeProvinceVote_UserCallBack(MakeProvinceVote_UserCallBack callback)
        {
            this.makeProvinceVote_UserCallBack = callback;
        }

        public void set_MakeTroop_UserCallBack(MakeTroop_UserCallBack callback)
        {
            this.makeTroop_UserCallBack = callback;
        }

        public void set_ManageReportFolders_UserCallBack(ManageReportFolders_UserCallBack callback)
        {
            this.manageReportFolders_UserCallBack = callback;
        }

        public void set_MemorizeCastleTroops_UserCallBack(MemorizeCastleTroops_UserCallBack callback)
        {
            this.memorizeCastleTroops_UserCallBack = callback;
        }

        public void set_MoveToMailFolder_UserCallBack(MoveToMailFolder_UserCallBack callback)
        {
            this.moveToMailFolder_UserCallBack = callback;
        }

        public void set_MoveVillageBuilding_UserCallBack(MoveVillageBuilding_UserCallBack callback)
        {
            this.moveVillageBuilding_UserCallBack = callback;
        }

        public void set_NewForumThread_UserCallBack(NewForumThread_UserCallBack callback)
        {
            this.newForumThread_UserCallBack = callback;
        }

        public void set_ParishWallDetailInfo_UserCallBack(ParishWallDetailInfo_UserCallBack callback)
        {
            this.parishWallDetailInfo_UserCallBack = callback;
        }

        public void set_PlaceVillageBuilding_UserCallBack(PlaceVillageBuilding_UserCallBack callback)
        {
            this.placeVillageBuilding_UserCallBack = callback;
        }

        public void set_PostToForumThread_UserCallBack(PostToForumThread_UserCallBack callback)
        {
            this.postToForumThread_UserCallBack = callback;
        }

        public void set_PreAttackSetup_UserCallBack(PreAttackSetup_UserCallBack callback)
        {
            this.preAttackSetup_UserCallBack = callback;
        }

        public void set_PremiumOverview_UserCallBack(PremiumOverview_UserCallBack callback)
        {
            this.premiumOverview_UserCallBack = callback;
        }

        public void set_PreValidateCardToBePlayed_UserCallBack(PreValidateCardToBePlayed_UserCallBack callback)
        {
            this.preValidateCardToBePlayed_UserCallBack = callback;
        }

        public void set_RemoveMailFolder_UserCallBack(RemoveMailFolder_UserCallBack callback)
        {
            this.removeMailFolder_UserCallBack = callback;
        }

        public void set_ReportMail_UserCallBack(ReportMail_UserCallBack callback)
        {
            this.reportMail_UserCallBack = callback;
        }

        public void set_ResendVerificationEmail_UserCallBack(ResendVerificationEmail_UserCallBack callback)
        {
            this.resendVerificationEmail_UserCallBack = callback;
        }

        public void set_RestoreCastleTroops_UserCallBack(RestoreCastleTroops_UserCallBack callback)
        {
            this.restoreCastleTroops_UserCallBack = callback;
        }

        public void set_RetrieveArmyFromGarrison_UserCallBack(RetrieveArmyFromGarrison_UserCallBack callback)
        {
            this.retrieveArmyFromGarrison_UserCallBack = callback;
        }

        public void set_RetrieveAttackResult_UserCallBack(RetrieveAttackResult_UserCallBack callback)
        {
            this.retrieveAttackResult_UserCallBack = callback;
        }

        public void set_RetrievePeople_UserCallBack(RetrievePeople_UserCallBack callback)
        {
            this.retrievePeople_UserCallBack = callback;
        }

        public void set_RetrieveStats_UserCallBack(RetrieveStats_UserCallBack callback)
        {
            this.retrieveStats_UserCallBack = callback;
        }

        public void set_RetrieveTroopsFromVassal_UserCallBack(RetrieveTroopsFromVassal_UserCallBack callback)
        {
            this.retrieveTroopsFromVassal_UserCallBack = callback;
        }

        public void set_RetrieveVillageUserInfo_UserCallBack(RetrieveVillageUserInfo_UserCallBack callback)
        {
            this.retrieveVillageUserInfo_UserCallBack = callback;
        }

        public void set_ReturnReinforcements_UserCallBack(ReturnReinforcements_UserCallBack callback)
        {
            this.returnReinforcements_UserCallBack = callback;
        }

        public void set_SelfJoinHouse_UserCallBack(SelfJoinHouse_UserCallBack callback)
        {
            this.selfJoinHouse_UserCallBack = callback;
        }

        public void set_SendCommands_UserCallBack(SendCommands_UserCallBack callback)
        {
            this.sendCommands_UserCallBack = callback;
        }

        public void set_SendMail_UserCallBack(SendMail_UserCallBack callback)
        {
            this.sendMail_UserCallBack = callback;
        }

        public void set_SendMarketResources_UserCallBack(SendMarketResources_UserCallBack callback)
        {
            this.sendMarketResources_UserCallBack = callback;
        }

        public void set_SendPeople_UserCallBack(SendPeople_UserCallBack callback)
        {
            this.sendPeople_UserCallBack = callback;
        }

        public void set_SendReinforcements_UserCallBack(SendReinforcements_UserCallBack callback)
        {
            this.sendReinforcements_UserCallBack = callback;
        }

        public void set_SendScouts_UserCallBack(SendScouts_UserCallBack callback)
        {
            this.sendScouts_UserCallBack = callback;
        }

        public void set_SendSpecialMail_UserCallBack(SendSpecialMail_UserCallBack callback)
        {
            this.sendSpecialMail_UserCallBack = callback;
        }

        public void set_SendTroopsToCapital_UserCallBack(SendTroopsToCapital_UserCallBack callback)
        {
            this.sendTroopsToCapital_UserCallBack = callback;
        }

        public void set_SendTroopsToVassal_UserCallBack(SendTroopsToVassal_UserCallBack callback)
        {
            this.sendTroopsToVassal_UserCallBack = callback;
        }

        public void set_SendVassalRequest_UserCallBack(SendVassalRequest_UserCallBack callback)
        {
            this.sendVassalRequest_UserCallBack = callback;
        }

        public void set_SetAdminMessage_UserCallBack(SetAdminMessage_UserCallBack callback)
        {
            this.setAdminMessage_UserCallBack = callback;
        }

        public void set_SetHighestArmySeen_UserCallBack(SetHighestArmySeen_UserCallBack callback)
        {
            this.setHighestArmySeen_UserCallBack = callback;
        }

        public void set_SetStartingCounty_UserCallBack(SetStartingCounty_UserCallBack callback)
        {
            this.setStartingCounty_UserCallBack = callback;
        }

        public void set_SetVacationMode_UserCallBack(SetVacationMode_UserCallBack callback)
        {
            this.setVacationMode_UserCallBack = callback;
        }

        public void set_SpecialVillageInfo_UserCallBack(SpecialVillageInfo_UserCallBack callback)
        {
            this.specialVillageInfo_UserCallBack = callback;
        }

        public void set_SpinTheWheel_UserCallBack(SpinTheWheel_UserCallBack callback)
        {
            this.spinTheWheel_UserCallBack = callback;
        }

        public void set_SpyCommand_UserCallBack(SpyCommand_UserCallBack callback)
        {
            this.spyCommand_UserCallBack = callback;
        }

        public void set_SpyGetArmyInfo_UserCallBack(SpyGetArmyInfo_UserCallBack callback)
        {
            this.spyGetArmyInfo_UserCallBack = callback;
        }

        public void set_SpyGetResearchInfo_UserCallBack(SpyGetResearchInfo_UserCallBack callback)
        {
            this.spyGetResearchInfo_UserCallBack = callback;
        }

        public void set_SpyGetVillageResourceInfo_UserCallBack(SpyGetVillageResourceInfo_UserCallBack callback)
        {
            this.spyGetVillageResourceInfo_UserCallBack = callback;
        }

        public void set_StandDownAsParishDespot_UserCallBack(StandDownAsParishDespot_UserCallBack callback)
        {
            this.standDownAsParishDespot_UserCallBack = callback;
        }

        public void set_StandInElection_UserCallBack(StandInElection_UserCallBack callback)
        {
            this.standInElection_UserCallBack = callback;
        }

        public void set_StartNewQuest_UserCallBack(StartNewQuest_UserCallBack callback)
        {
            this.startNewQuest_UserCallBack = callback;
        }

        public void set_StockExchangeTrade_UserCallBack(StockExchangeTrade_UserCallBack callback)
        {
            this.stockExchangeTrade_UserCallBack = callback;
        }

        public void set_TestAchievements_UserCallBack(TestAchievements_UserCallBack callback)
        {
            this.testAchievements_UserCallBack = callback;
        }

        public void set_TouchHouseVisitDate_UserCallBack(TouchHouseVisitDate_UserCallBack callback)
        {
            this.touchHouseVisitDate_UserCallBack = callback;
        }

        public void set_TutorialCommand_UserCallBack(TutorialCommand_UserCallBack callback)
        {
            this.tutorialCommand_UserCallBack = callback;
        }

        public void set_UpdateCurrentCards_UserCallBack(UpdateCurrentCards_UserCallBack callback)
        {
            this.updateCurrentCards_UserCallBack = callback;
        }

        public void set_UpdateDiplomacyStatus_UserCallBack(UpdateDiplomacyStatus_UserCallBack callback)
        {
            this.updateDiplomacyStatus_UserCallBack = callback;
        }

        public void set_UpdateReportFilters_UserCallBack(UpdateReportFilters_UserCallBack callback)
        {
            this.updateReportFilters_UserCallBack = callback;
        }

        public void set_UpdateSelectedTitheType_UserCallBack(UpdateSelectedTitheType_UserCallBack callback)
        {
            this.updateSelectedTitheType_UserCallBack = callback;
        }

        public void set_UpdateUserOptions_UserCallBack(UpdateUserOptions_UserCallBack callback)
        {
            this.updateUserOptions_UserCallBack = callback;
        }

        public void set_UpdateVillageFavourites_UserCallBack(UpdateVillageFavourites_UserCallBack callback)
        {
            this.updateVillageFavourites_UserCallBack = callback;
        }

        public void set_UpdateVillageResourcesInfo_UserCallBack(UpdateVillageResourcesInfo_UserCallBack callback)
        {
            this.updateVillageResourcesInfo_UserCallBack = callback;
        }

        public void set_UpgradeRank_UserCallBack(UpgradeRank_UserCallBack callback)
        {
            this.upgradeRank_UserCallBack = callback;
        }

        public void set_UploadAvatar_UserCallBack(UploadAvatar_UserCallBack callback)
        {
            this.uploadAvatar_UserCallBack = callback;
        }

        public void set_UserInfo_UserCallBack(UserInfo_UserCallBack callback)
        {
            this.userInfo_UserCallBack = callback;
        }

        public void set_VassalInfo_UserCallBack(VassalInfo_UserCallBack callback)
        {
            this.vassalInfo_UserCallBack = callback;
        }

        public void set_VassalSendResources_UserCallBack(VassalSendResources_UserCallBack callback)
        {
            this.vassalSendResources_UserCallBack = callback;
        }

        public void set_ViewBattle_UserCallBack(ViewBattle_UserCallBack callback)
        {
            this.viewBattle_UserCallBack = callback;
        }

        public void set_ViewCastle_UserCallBack(ViewCastle_UserCallBack callback)
        {
            this.viewCastle_UserCallBack = callback;
        }

        public void set_VillageBuildingChangeRates_UserCallBack(VillageBuildingChangeRates_UserCallBack callback)
        {
            this.villageBuildingChangeRates_UserCallBack = callback;
        }

        public void set_VillageBuildingCompleteDataRetrieval_UserCallBack(VillageBuildingCompleteDataRetrieval_UserCallBack callback)
        {
            this.villageBuildingCompleteDataRetrieval_UserCallBack = callback;
        }

        public void set_VillageBuildingSetActive_UserCallBack(VillageBuildingSetActive_UserCallBack callback)
        {
            this.villageBuildingSetActive_UserCallBack = callback;
        }

        public void set_VillageHoldBanquet_UserCallBack(VillageHoldBanquet_UserCallBack callback)
        {
            this.villageHoldBanquet_UserCallBack = callback;
        }

        public void set_VillageProduceWeapons_UserCallBack(VillageProduceWeapons_UserCallBack callback)
        {
            this.villageProduceWeapons_UserCallBack = callback;
        }

        public void set_VillageRename_UserCallBack(VillageRename_UserCallBack callback)
        {
            this.villageRename_UserCallBack = callback;
        }

        public void set_VoteInElection_UserCallBack(VoteInElection_UserCallBack callback)
        {
            this.voteInElection_UserCallBack = callback;
        }

        public void set_WorldInfo_UserCallBack(WorldInfo_UserCallBack callback)
        {
            this.worldInfo_UserCallBack = callback;
        }

        public void SetAdminMessage(string message, int type)
        {
            if (this.SetAdminMessage_Callback == null)
            {
                this.SetAdminMessage_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SetAdminMessage);
            }
            RemoteAsyncDelegate_SetAdminMessage message2 = new RemoteAsyncDelegate_SetAdminMessage(this.service.SetAdminMessage);
            this.registerRPCcall(message2.BeginInvoke(this.UserID, this.SessionID, message, type, this.SetAdminMessage_Callback, null), typeof(SetAdminMessage_ReturnType));
        }

        public void SetHighestArmySeen(long highestArmyIDSeen)
        {
            if (this.SetHighestArmySeen_Callback == null)
            {
                this.SetHighestArmySeen_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SetHighestArmySeen);
            }
            RemoteAsyncDelegate_SetHighestArmySeen seen = new RemoteAsyncDelegate_SetHighestArmySeen(this.service.SetHighestArmySeen);
            this.registerRPCcall(seen.BeginInvoke(this.UserID, this.SessionID, highestArmyIDSeen, this.SetHighestArmySeen_Callback, null), typeof(SetHighestArmySeen_ReturnType));
        }

        public void SetStartingCounty(int countyID)
        {
            if (this.SetStartingCounty_Callback == null)
            {
                this.SetStartingCounty_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SetStartingCounty);
            }
            RemoteAsyncDelegate_SetStartingCounty county = new RemoteAsyncDelegate_SetStartingCounty(this.service.SetStartingCounty);
            this.registerRPCcall(county.BeginInvoke(this.UserID, this.SessionID, countyID, this.SetStartingCounty_Callback, null), typeof(SetStartingCounty_ReturnType));
        }

        public void SetVacationMode(int numDays)
        {
            if (this.SetVacationMode_Callback == null)
            {
                this.SetVacationMode_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SetVacationMode);
            }
            RemoteAsyncDelegate_SetVacationMode mode = new RemoteAsyncDelegate_SetVacationMode(this.service.SetVacationMode);
            this.registerRPCcall(mode.BeginInvoke(this.UserID, this.SessionID, numDays, this.SetVacationMode_Callback, null), typeof(SetVacationMode_ReturnType));
        }

        public void SpecialVillageInfo(int villageID)
        {
            if (this.SpecialVillageInfo_Callback == null)
            {
                this.SpecialVillageInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SpecialVillageInfo);
            }
            RemoteAsyncDelegate_SpecialVillageInfo info = new RemoteAsyncDelegate_SpecialVillageInfo(this.service.SpecialVillageInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, this.SpecialVillageInfo_Callback, null), typeof(SpecialVillageInfo_ReturnType));
        }

        public void SpinTheRoyalWheel(int villageID, int wheelType)
        {
            if (this.SpinTheWheel_Callback == null)
            {
                this.SpinTheWheel_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SpinTheWheel);
            }
            RemoteAsyncDelegate_SpinTheWheel wheel = new RemoteAsyncDelegate_SpinTheWheel(this.service.SpinTheWheel);
            this.registerRPCcall(wheel.BeginInvoke(this.UserID, this.SessionID, villageID, wheelType, this.SpinTheWheel_Callback, null), typeof(SpinTheWheel_ReturnType));
        }

        public void SpyCommand(int villageID, int command)
        {
            if (this.SpyCommand_Callback == null)
            {
                this.SpyCommand_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SpyCommand);
            }
            RemoteAsyncDelegate_SpyCommand command2 = new RemoteAsyncDelegate_SpyCommand(this.service.SpyCommand);
            this.registerRPCcall(command2.BeginInvoke(this.UserID, this.SessionID, villageID, command, this.SpyCommand_Callback, null), typeof(SpyCommand_ReturnType));
        }

        public void SpyGetArmyInfo(int villageID)
        {
            if (this.SpyGetArmyInfo_Callback == null)
            {
                this.SpyGetArmyInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SpyGetArmyInfo);
            }
            RemoteAsyncDelegate_SpyGetArmyInfo info = new RemoteAsyncDelegate_SpyGetArmyInfo(this.service.SpyGetArmyInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, this.SpyGetArmyInfo_Callback, null), typeof(SpyGetArmyInfo_ReturnType));
        }

        public void SpyGetResearchInfo(int villageID)
        {
            if (this.SpyGetResearchInfo_Callback == null)
            {
                this.SpyGetResearchInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SpyGetResearchInfo);
            }
            RemoteAsyncDelegate_SpyGetResearchInfo info = new RemoteAsyncDelegate_SpyGetResearchInfo(this.service.SpyGetResearchInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, this.SpyGetResearchInfo_Callback, null), typeof(SpyGetResearchInfo_ReturnType));
        }

        public void SpyGetVillageResourceInfo(int villageID)
        {
            if (this.SpyGetVillageResourceInfo_Callback == null)
            {
                this.SpyGetVillageResourceInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_SpyGetVillageResourceInfo);
            }
            RemoteAsyncDelegate_SpyGetVillageResourceInfo info = new RemoteAsyncDelegate_SpyGetVillageResourceInfo(this.service.SpyGetVillageResourceInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, this.SpyGetVillageResourceInfo_Callback, null), typeof(SpyGetVillageResourceInfo_ReturnType));
        }

        public void StandDownAsParishDespot(int villageID)
        {
            if (this.StandDownAsParishDespot_Callback == null)
            {
                this.StandDownAsParishDespot_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_StandDownAsParishDespot);
            }
            RemoteAsyncDelegate_StandDownAsParishDespot despot = new RemoteAsyncDelegate_StandDownAsParishDespot(this.service.StandDownAsParishDespot);
            this.registerRPCcall(despot.BeginInvoke(this.UserID, this.SessionID, villageID, this.StandDownAsParishDespot_Callback, null), typeof(StandDownAsParishDespot_ReturnType));
        }

        public void StandInElection(int areaID, int areaType, bool state)
        {
            if (this.StandInElection_Callback == null)
            {
                this.StandInElection_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_StandInElection);
            }
            RemoteAsyncDelegate_StandInElection election = new RemoteAsyncDelegate_StandInElection(this.service.StandInElection);
            this.registerRPCcall(election.BeginInvoke(this.UserID, this.SessionID, areaID, areaType, state, this.StandInElection_Callback, null), typeof(StandInElection_ReturnType));
        }

        public void StartNewQuest(int questID)
        {
            if (this.StartNewQuest_Callback == null)
            {
                this.StartNewQuest_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_StartNewQuest);
            }
            RemoteAsyncDelegate_StartNewQuest quest = new RemoteAsyncDelegate_StartNewQuest(this.service.StartNewQuest);
            this.registerRPCcall(quest.BeginInvoke(this.UserID, this.SessionID, questID, this.StartNewQuest_Callback, null), typeof(StartNewQuest_ReturnType));
        }

        public void StockExchangeTrade(int villageID, int targetExchange, int resource, int amount, bool buy)
        {
            if (this.StockExchangeTrade_Callback == null)
            {
                this.StockExchangeTrade_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_StockExchangeTrade);
            }
            RemoteAsyncDelegate_StockExchangeTrade trade = new RemoteAsyncDelegate_StockExchangeTrade(this.service.StockExchangeTrade);
            this.registerRPCcall(trade.BeginInvoke(this.UserID, this.SessionID, villageID, targetExchange, resource, amount, buy, this.StockExchangeTrade_Callback, null), typeof(StockExchangeTrade_ReturnType));
        }

        public void storeRPCresult(IAsyncResult ar, Common_ReturnData resultData)
        {
            foreach (CallBackEntryClass class2 in this.resultList)
            {
                if ((class2.state == 1) && (class2.ar == ar))
                {
                    class2.data = resultData;
                    class2.state = 2;
                    class2.timer = DXTimer.GetCurrentMilliseconds() - class2.timer;
                    this.lastLatency = (int) class2.timer;
                    if (this.RTTAverageCount == 0)
                    {
                        this.RTTAverageCount = 1;
                        this.RTTAverageTime = class2.timer;
                    }
                    else
                    {
                        double num = this.RTTAverageTime * this.RTTAverageCount;
                        num += class2.timer;
                        this.RTTAverageCount++;
                        this.RTTAverageTime = num / ((double) this.RTTAverageCount);
                    }
                    this.addPacket(class2.classType, (int) class2.timer);
                    if (class2.timer < 1000.0)
                    {
                        if (this.RTTAverageShortCount == 0)
                        {
                            this.RTTAverageShortCount = 1;
                            this.RTTAverageShortTime = class2.timer;
                        }
                        else
                        {
                            double num2 = this.RTTAverageShortTime * this.RTTAverageShortCount;
                            num2 += class2.timer;
                            this.RTTAverageShortCount++;
                            this.RTTAverageShortTime = num2 / ((double) this.RTTAverageShortCount);
                        }
                    }
                    else if (this.RTTAverageLongCount == 0)
                    {
                        this.RTTAverageLongCount = 1;
                        this.RTTAverageLongTime = class2.timer;
                    }
                    else
                    {
                        double num3 = this.RTTAverageLongTime * this.RTTAverageLongCount;
                        num3 += class2.timer;
                        this.RTTAverageLongCount++;
                        this.RTTAverageLongTime = num3 / ((double) this.RTTAverageLongCount);
                    }
                    break;
                }
            }
            foreach (CallBackEntryClass class3 in this.queuedResultList)
            {
                if ((class3.state == 1) && (class3.ar == ar))
                {
                    class3.data = resultData;
                    class3.state = 2;
                    class3.timer = DXTimer.GetCurrentMilliseconds() - class3.timer;
                    if (this.RTTAverageCount == 0)
                    {
                        this.RTTAverageCount = 1;
                        this.RTTAverageTime = class3.timer;
                    }
                    else
                    {
                        double num4 = this.RTTAverageTime * this.RTTAverageCount;
                        num4 += class3.timer;
                        this.RTTAverageCount++;
                        this.RTTAverageTime = num4 / ((double) this.RTTAverageCount);
                    }
                    this.addPacket(class3.classType, (int) class3.timer);
                    if (class3.timer < 1000.0)
                    {
                        if (this.RTTAverageShortCount == 0)
                        {
                            this.RTTAverageShortCount = 1;
                            this.RTTAverageShortTime = class3.timer;
                        }
                        else
                        {
                            double num5 = this.RTTAverageShortTime * this.RTTAverageShortCount;
                            num5 += class3.timer;
                            this.RTTAverageShortCount++;
                            this.RTTAverageShortTime = num5 / ((double) this.RTTAverageShortCount);
                        }
                    }
                    else if (this.RTTAverageLongCount == 0)
                    {
                        this.RTTAverageLongCount = 1;
                        this.RTTAverageLongTime = class3.timer;
                    }
                    else
                    {
                        double num6 = this.RTTAverageLongTime * this.RTTAverageLongCount;
                        num6 += class3.timer;
                        this.RTTAverageLongTime++;
                        this.RTTAverageLongTime = num6 / ((double) this.RTTAverageLongCount);
                    }
                    break;
                }
            }
        }

        public void TestAchievements(List<int> achievementsToTest, List<AchievementData> achievementData)
        {
            if (this.TestAchievements_Callback == null)
            {
                this.TestAchievements_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_TestAchievements);
            }
            RemoteAsyncDelegate_TestAchievements achievements = new RemoteAsyncDelegate_TestAchievements(this.service.TestAchievements);
            this.registerRPCcall(achievements.BeginInvoke(this.UserID, this.SessionID, achievementsToTest, achievementData, this.TestAchievements_Callback, null), typeof(TestAchievements_ReturnType));
        }

        public void TouchHouseVisitDate(int factionID)
        {
            if (this.TouchHouseVisitDate_Callback == null)
            {
                this.TouchHouseVisitDate_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_TouchHouseVisitDate);
            }
            RemoteAsyncDelegate_TouchHouseVisitDate date = new RemoteAsyncDelegate_TouchHouseVisitDate(this.service.TouchHouseVisitDate);
            this.registerRPCcall(date.BeginInvoke(this.UserID, this.SessionID, factionID, this.TouchHouseVisitDate_Callback, null), typeof(TouchHouseVisitDate_ReturnType));
        }

        public void TutorialCommand(int command)
        {
            if (this.TutorialCommand_Callback == null)
            {
                this.TutorialCommand_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_TutorialCommand);
            }
            RemoteAsyncDelegate_TutorialCommand command2 = new RemoteAsyncDelegate_TutorialCommand(this.service.TutorialCommand);
            this.registerRPCcall(command2.BeginInvoke(this.UserID, this.SessionID, command, this.TutorialCommand_Callback, null), typeof(TutorialCommand_ReturnType));
        }

        public void UpdateCurrentCards()
        {
            if (this.UpdateCurrentCards_Callback == null)
            {
                this.UpdateCurrentCards_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpdateCurrentCards);
            }
            RemoteAsyncDelegate_UpdateCurrentCards cards = new RemoteAsyncDelegate_UpdateCurrentCards(this.service.UpdateCurrentCards);
            this.registerRPCcall(cards.BeginInvoke(this.UserID, this.SessionID, this.UpdateCurrentCards_Callback, null), typeof(UpdateCurrentCards_ReturnType));
        }

        public void UpdateDiplomacyStatus(bool state)
        {
            if (this.UpdateDiplomacyStatus_Callback == null)
            {
                this.UpdateDiplomacyStatus_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpdateDiplomacyStatus);
            }
            RemoteAsyncDelegate_UpdateDiplomacyStatus status = new RemoteAsyncDelegate_UpdateDiplomacyStatus(this.service.UpdateDiplomacyStatus);
            this.registerRPCcall(status.BeginInvoke(this.UserID, this.SessionID, state, this.UpdateDiplomacyStatus_Callback, null), typeof(UpdateDiplomacyStatus_ReturnType));
        }

        public void UpdateReportFilters(ReportFilterList filters)
        {
            if (this.UpdateReportFilters_Callback == null)
            {
                this.UpdateReportFilters_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpdateReportFilters);
            }
            RemoteAsyncDelegate_UpdateReportFilters filters2 = new RemoteAsyncDelegate_UpdateReportFilters(this.service.UpdateReportFilters);
            this.registerRPCcall(filters2.BeginInvoke(this.UserID, this.SessionID, filters, this.UpdateReportFilters_Callback, null), typeof(UpdateReportFilters_ReturnType));
        }

        public void UpdateSelectedTitheType(int villageID, int type)
        {
            if (this.UpdateSelectedTitheType_Callback == null)
            {
                this.UpdateSelectedTitheType_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpdateSelectedTitheType);
            }
            RemoteAsyncDelegate_UpdateSelectedTitheType type2 = new RemoteAsyncDelegate_UpdateSelectedTitheType(this.service.UpdateSelectedTitheType);
            this.registerRPCcall(type2.BeginInvoke(this.UserID, this.SessionID, villageID, type, this.UpdateSelectedTitheType_Callback, null), typeof(UpdateSelectedTitheType_ReturnType));
        }

        public void UpdateUserOptions(GameOptionsData options)
        {
            if (this.UpdateUserOptions_Callback == null)
            {
                this.UpdateUserOptions_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpdateUserOptions);
            }
            RemoteAsyncDelegate_UpdateUserOptions options2 = new RemoteAsyncDelegate_UpdateUserOptions(this.service.UpdateUserOptions);
            this.registerRPCcall(options2.BeginInvoke(this.UserID, this.SessionID, options, this.UpdateUserOptions_Callback, null), typeof(UpdateUserOptions_ReturnType));
        }

        public void UpdateVillageFavourites(int mode, int villageID)
        {
            if (this.UpdateVillageFavourites_Callback == null)
            {
                this.UpdateVillageFavourites_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpdateVillageFavourites);
            }
            RemoteAsyncDelegate_UpdateVillageFavourites favourites = new RemoteAsyncDelegate_UpdateVillageFavourites(this.service.UpdateVillageFavourites);
            this.registerRPCcall(favourites.BeginInvoke(this.UserID, this.SessionID, mode, villageID, this.UpdateVillageFavourites_Callback, null), typeof(UpdateVillageFavourites_ReturnType));
        }

        public void UpdateVillageResourcesInfo(int villageID)
        {
            if (this.UpdateVillageResourcesInfo_Callback == null)
            {
                this.UpdateVillageResourcesInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpdateVillageResourcesInfo);
            }
            RemoteAsyncDelegate_UpdateVillageResourcesInfo info = new RemoteAsyncDelegate_UpdateVillageResourcesInfo(this.service.UpdateVillageResourcesInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, this.UpdateVillageResourcesInfo_Callback, null), typeof(UpdateVillageResourcesInfo_ReturnType));
        }

        public void UpgradeRank(int rank, int rankSubLevel)
        {
            if (this.UpgradeRank_Callback == null)
            {
                this.UpgradeRank_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UpgradeRank);
            }
            RemoteAsyncDelegate_UpgradeRank rank2 = new RemoteAsyncDelegate_UpgradeRank(this.service.UpgradeRank);
            this.registerRPCcall(rank2.BeginInvoke(this.UserID, this.SessionID, rank, rankSubLevel, this.UpgradeRank_Callback, null), typeof(UpgradeRank_ReturnType));
        }

        public void UploadAvatar(AvatarData avatarData)
        {
            if (this.UploadAvatar_Callback == null)
            {
                this.UploadAvatar_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UploadAvatar);
            }
            RemoteAsyncDelegate_UploadAvatar avatar = new RemoteAsyncDelegate_UploadAvatar(this.service.UploadAvatar);
            this.registerRPCcall(avatar.BeginInvoke(this.UserID, this.SessionID, avatarData, this.UploadAvatar_Callback, null), typeof(UploadAvatar_ReturnType));
        }

        public void UserInfo(int requestedUser)
        {
            if (this.UserInfo_Callback == null)
            {
                this.UserInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_UserInfo);
            }
            RemoteAsyncDelegate_UserInfo info = new RemoteAsyncDelegate_UserInfo(this.service.UserInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, requestedUser, this.UserInfo_Callback, null), typeof(UserInfo_ReturnType));
        }

        public void VassalInfo(int villageID)
        {
            if (this.VassalInfo_Callback == null)
            {
                this.VassalInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VassalInfo);
            }
            RemoteAsyncDelegate_VassalInfo info = new RemoteAsyncDelegate_VassalInfo(this.service.VassalInfo);
            this.registerRPCcall(info.BeginInvoke(this.UserID, this.SessionID, villageID, this.VassalInfo_Callback, null), typeof(VassalInfo_ReturnType));
        }

        public void VassalSendResources(int villageID, int targetVillage, int type, int amount)
        {
            if (this.VassalSendResources_Callback == null)
            {
                this.VassalSendResources_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VassalSendResources);
            }
            RemoteAsyncDelegate_VassalSendResources resources = new RemoteAsyncDelegate_VassalSendResources(this.service.VassalSendResources);
            this.registerRPCcall(resources.BeginInvoke(this.UserID, this.SessionID, villageID, targetVillage, type, amount, this.VassalSendResources_Callback, null), typeof(VassalSendResources_ReturnType));
        }

        public void ViewBattle(long reportID)
        {
            if (this.ViewBattle_Callback == null)
            {
                this.ViewBattle_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ViewBattle);
            }
            RemoteAsyncDelegate_ViewBattle battle = new RemoteAsyncDelegate_ViewBattle(this.service.ViewBattle);
            this.registerRPCcall(battle.BeginInvoke(this.UserID, this.SessionID, reportID, this.ViewBattle_Callback, null), typeof(ViewBattle_ReturnType));
        }

        public void ViewCastle_Report(long reportID)
        {
            if (this.ViewCastle_Callback == null)
            {
                this.ViewCastle_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ViewCastle);
            }
            RemoteAsyncDelegate_ViewCastle castle = new RemoteAsyncDelegate_ViewCastle(this.service.ViewCastle);
            this.registerRPCcall(castle.BeginInvoke(this.UserID, this.SessionID, -1, reportID, this.ViewCastle_Callback, null), typeof(ViewCastle_ReturnType));
        }

        public void ViewCastle_Village(int villageID)
        {
            if (this.ViewCastle_Callback == null)
            {
                this.ViewCastle_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_ViewCastle);
            }
            RemoteAsyncDelegate_ViewCastle castle = new RemoteAsyncDelegate_ViewCastle(this.service.ViewCastle);
            this.registerRPCcall(castle.BeginInvoke(this.UserID, this.SessionID, villageID, -1L, this.ViewCastle_Callback, null), typeof(ViewCastle_ReturnType));
        }

        public void VillageAbandon(int villageID)
        {
            if (this.VillageRename_Callback == null)
            {
                this.VillageRename_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageRename);
            }
            RemoteAsyncDelegate_VillageRename rename = new RemoteAsyncDelegate_VillageRename(this.service.VillageRename);
            this.registerRPCcall(rename.BeginInvoke(this.UserID, this.SessionID, villageID, "DoAbandon", true, false, this.VillageRename_Callback, null), typeof(VillageRename_ReturnType));
        }

        public void VillageAllBuildingsSetActive(int villageID, bool state)
        {
            if (this.VillageBuildingSetActive_Callback == null)
            {
                this.VillageBuildingSetActive_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageBuildingSetActive);
            }
            RemoteAsyncDelegate_VillageBuildingSetActive active = new RemoteAsyncDelegate_VillageBuildingSetActive(this.service.VillageBuildingSetActive);
            this.registerRPCcall(active.BeginInvoke(this.UserID, this.SessionID, -1L, villageID, -1, state, this.VillageBuildingSetActive_Callback, null), typeof(VillageBuildingSetActive_ReturnType));
        }

        public void VillageBuildingChangeRates(int villageID, int taxLevel, int rationsLevel, int aleRationsLevel, int capitalTaxRate)
        {
            if (this.VillageBuildingChangeRates_Callback == null)
            {
                this.VillageBuildingChangeRates_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageBuildingChangeRates);
            }
            RemoteAsyncDelegate_VillageBuildingChangeRates rates = new RemoteAsyncDelegate_VillageBuildingChangeRates(this.service.VillageBuildingChangeRates);
            this.registerRPCcall(rates.BeginInvoke(this.UserID, this.SessionID, villageID, taxLevel, rationsLevel, aleRationsLevel, capitalTaxRate, this.VillageBuildingChangeRates_Callback, null), typeof(VillageBuildingChangeRates_ReturnType));
        }

        public void VillageBuildingCompleteDataRetrieval(int villageID, long buildingID)
        {
            if (this.VillageBuildingCompleteDataRetrieval_Callback == null)
            {
                this.VillageBuildingCompleteDataRetrieval_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageBuildingCompleteDataRetrieval);
            }
            RemoteAsyncDelegate_VillageBuildingCompleteDataRetrieval retrieval = new RemoteAsyncDelegate_VillageBuildingCompleteDataRetrieval(this.service.VillageBuildingCompleteDataRetrieval);
            this.registerRPCcall(retrieval.BeginInvoke(this.UserID, this.SessionID, villageID, buildingID, 0, this.VillageBuildingCompleteDataRetrieval_Callback, null), typeof(VillageBuildingCompleteDataRetrieval_ReturnType));
        }

        public void VillageBuildingDeleteDataRetrieval(int villageID, long buildingID)
        {
            if (this.VillageBuildingCompleteDataRetrieval_Callback == null)
            {
                this.VillageBuildingCompleteDataRetrieval_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageBuildingCompleteDataRetrieval);
            }
            RemoteAsyncDelegate_VillageBuildingCompleteDataRetrieval retrieval = new RemoteAsyncDelegate_VillageBuildingCompleteDataRetrieval(this.service.VillageBuildingCompleteDataRetrieval);
            this.registerRPCcall(retrieval.BeginInvoke(this.UserID, this.SessionID, villageID, buildingID, 1, this.VillageBuildingCompleteDataRetrieval_Callback, null), typeof(VillageBuildingCompleteDataRetrieval_ReturnType));
        }

        public void VillageBuildingSetActive(int villageID, long buildingID, bool state)
        {
            if (this.VillageBuildingSetActive_Callback == null)
            {
                this.VillageBuildingSetActive_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageBuildingSetActive);
            }
            RemoteAsyncDelegate_VillageBuildingSetActive active = new RemoteAsyncDelegate_VillageBuildingSetActive(this.service.VillageBuildingSetActive);
            this.registerRPCcall(active.BeginInvoke(this.UserID, this.SessionID, buildingID, villageID, -1, state, this.VillageBuildingSetActive_Callback, null), typeof(VillageBuildingSetActive_ReturnType));
        }

        public void VillageBuildingTypeSetActive(int villageID, int buildingType, bool state)
        {
            if (this.VillageBuildingSetActive_Callback == null)
            {
                this.VillageBuildingSetActive_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageBuildingSetActive);
            }
            RemoteAsyncDelegate_VillageBuildingSetActive active = new RemoteAsyncDelegate_VillageBuildingSetActive(this.service.VillageBuildingSetActive);
            this.registerRPCcall(active.BeginInvoke(this.UserID, this.SessionID, -1L, villageID, buildingType, state, this.VillageBuildingSetActive_Callback, null), typeof(VillageBuildingSetActive_ReturnType));
        }

        public void VillageHoldBanquet(int villageID, int venison, int furniture, int metalwork, int clothing, int wine, int salt, int spice, int silk)
        {
            if (this.VillageHoldBanquet_Callback == null)
            {
                this.VillageHoldBanquet_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageHoldBanquet);
            }
            RemoteAsyncDelegate_VillageHoldBanquet banquet = new RemoteAsyncDelegate_VillageHoldBanquet(this.service.VillageHoldBanquet);
            this.registerRPCcall(banquet.BeginInvoke(this.UserID, this.SessionID, villageID, venison, wine, salt, spice, silk, clothing, furniture, metalwork, this.VillageHoldBanquet_Callback, null), typeof(VillageHoldBanquet_ReturnType));
        }

        public void VillageProduceWeapons(int villageID, int weaponType, int amount)
        {
            if (this.VillageProduceWeapons_Callback == null)
            {
                this.VillageProduceWeapons_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageProduceWeapons);
            }
            RemoteAsyncDelegate_VillageProduceWeapons weapons = new RemoteAsyncDelegate_VillageProduceWeapons(this.service.VillageProduceWeapons);
            this.registerRPCcall(weapons.BeginInvoke(this.UserID, this.SessionID, villageID, weaponType, amount, this.VillageProduceWeapons_Callback, null), typeof(VillageProduceWeapons_ReturnType));
        }

        public void VillageRename(int villageID, string villageName)
        {
            if (this.VillageRename_Callback == null)
            {
                this.VillageRename_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageRename);
            }
            RemoteAsyncDelegate_VillageRename rename = new RemoteAsyncDelegate_VillageRename(this.service.VillageRename);
            this.registerRPCcall(rename.BeginInvoke(this.UserID, this.SessionID, villageID, villageName, false, false, this.VillageRename_Callback, null), typeof(VillageRename_ReturnType));
        }

        public void VillageResetName(int villageID)
        {
            if (this.VillageRename_Callback == null)
            {
                this.VillageRename_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VillageRename);
            }
            RemoteAsyncDelegate_VillageRename rename = new RemoteAsyncDelegate_VillageRename(this.service.VillageRename);
            this.registerRPCcall(rename.BeginInvoke(this.UserID, this.SessionID, villageID, "DoReset", false, true, this.VillageRename_Callback, null), typeof(VillageRename_ReturnType));
        }

        public void VoteInElection(int areaID, int areaType, int candidate)
        {
            if (this.VoteInElection_Callback == null)
            {
                this.VoteInElection_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_VoteInElection);
            }
            RemoteAsyncDelegate_VoteInElection election = new RemoteAsyncDelegate_VoteInElection(this.service.VoteInElection);
            this.registerRPCcall(election.BeginInvoke(this.UserID, this.SessionID, areaID, areaType, candidate, this.VoteInElection_Callback, null), typeof(VoteInElection_ReturnType));
        }

        public void WorldInfo()
        {
            if (this.WorldInfo_Callback == null)
            {
                this.WorldInfo_Callback = new AsyncCallback(this.OurRemoteAsyncCallBack_WorldInfo);
            }
            RemoteAsyncDelegate_WorldInfo info = new RemoteAsyncDelegate_WorldInfo(this.service.WorldInfo);
            this.registerRPCcall(info.BeginInvoke(this.WorldInfo_Callback, null), typeof(WorldInfo_ReturnType));
        }

        public bool Admin
        {
            get
            {
                return this.admin;
            }
            set
            {
                this.admin = value;
            }
        }

        public bool BoxUser
        {
            get
            {
                return this.boxUser;
            }
            set
            {
                this.boxUser = value;
            }
        }

        public bool ChatActive
        {
            get
            {
                return this.chatActive;
            }
            set
            {
                this.chatActive = value;
            }
        }

        public static RemoteServices Instance
        {
            get
            {
                return instance;
            }
        }

        public LoginLeadersInfo LoginLeaderInfo
        {
            get
            {
                return this.loginLeaderInfo;
            }
            set
            {
                this.loginLeaderInfo = value;
            }
        }

        public bool MapEditor
        {
            get
            {
                return this.mapEditor;
            }
            set
            {
                this.mapEditor = value;
            }
        }

        public bool Moderator
        {
            get
            {
                return this.moderator;
            }
            set
            {
                this.moderator = value;
            }
        }

        public int ProfileWorldID
        {
            get
            {
                return this.profileWorldID;
            }
            set
            {
                this.profileWorldID = value;
            }
        }

        public string RealName
        {
            get
            {
                return this.realname;
            }
            set
            {
                this.realname = value;
            }
        }

        public ReportFilterList ReportFilters
        {
            get
            {
                return this.reportFilters;
            }
            set
            {
                this.reportFilters = value;
            }
        }

        public bool RequiresVerification
        {
            get
            {
                return this.requiresVerification;
            }
            set
            {
                this.requiresVerification = value;
            }
        }

        public Guid SessionGuid
        {
            get
            {
                return this.sessionGuid;
            }
            set
            {
                this.sessionGuid = value;
            }
        }

        public int SessionID
        {
            get
            {
                return this.sessionID;
            }
            set
            {
                this.sessionID = value;
            }
        }

        public bool Show2ndAgeMessage
        {
            get
            {
                bool flag = this.show2ndAgeMessage;
                this.show2ndAgeMessage = false;
                return flag;
            }
            set
            {
                this.show2ndAgeMessage = value;
            }
        }

        public bool Show3rdAgeMessage
        {
            get
            {
                bool flag = this.show3rdAgeMessage;
                this.show3rdAgeMessage = false;
                return flag;
            }
            set
            {
                this.show3rdAgeMessage = value;
            }
        }

        public bool Show4thAgeMessage
        {
            get
            {
                bool flag = this.show4thAgeMessage;
                this.show4thAgeMessage = false;
                return flag;
            }
            set
            {
                this.show4thAgeMessage = value;
            }
        }

        public bool Show5thAgeMessage
        {
            get
            {
                bool flag = this.show5thAgeMessage;
                this.show5thAgeMessage = false;
                return flag;
            }
            set
            {
                this.show5thAgeMessage = value;
            }
        }

        public bool ShowAdminMessage
        {
            get
            {
                bool showAdminMessage = this.showAdminMessage;
                this.showAdminMessage = false;
                return showAdminMessage;
            }
            set
            {
                this.showAdminMessage = value;
            }
        }

        public List<int> UserAchievements
        {
            get
            {
                return this.achievements;
            }
            set
            {
                this.achievements = value;
            }
        }

        public AvatarData UserAvatar
        {
            get
            {
                return this.userAvatar;
            }
            set
            {
                this.userAvatar = value;
            }
        }

        public int UserFactionID
        {
            get
            {
                return this.userFactionID;
            }
            set
            {
                this.userFactionID = value;
            }
        }

        public Guid UserGuid
        {
            get
            {
                return this.userGuid;
            }
            set
            {
                this.userGuid = value;
            }
        }

        public int UserID
        {
            get
            {
                return this.userID;
            }
            set
            {
                this.userID = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }

        public GameOptionsData UserOptions
        {
            get
            {
                return this.userOptions;
            }
            set
            {
                this.userOptions = value;
            }
        }

        public Guid WorldGUID
        {
            get
            {
                return this.worldGUID;
            }
            set
            {
                this.worldGUID = value;
            }
        }

        public delegate void AchievementProgress_UserCallBack(AchievementProgress_ReturnType returnData);

        public delegate void AddCastleElement_UserCallBack(AddCastleElement_ReturnType returnData);

        public delegate void AddUserToFavourites_UserCallBack(AddUserToFavourites_ReturnType returnData);

        public delegate void ArmyAttack_UserCallBack(ArmyAttack_ReturnType returnData);

        public delegate void AutoRepairCastle_UserCallBack(AutoRepairCastle_ReturnType returnData);

        public delegate void BreakLiegeLord_UserCallBack(BreakLiegeLord_ReturnType returnData);

        public delegate void BreakVassalage_UserCallBack(BreakVassalage_ReturnType returnData);

        public delegate void BuyResearchPoint_UserCallBack(BuyResearchPoint_ReturnType returnData);

        public delegate void BuyVillage_UserCallBack(BuyVillage_ReturnType returnData);

        private class CallBackEntryClass
        {
            public IAsyncResult ar;
            public Type classType;
            public Common_ReturnData data;
            public int state = 1;
            public double timer;
        }

        public delegate void CancelCard_UserCallBack(CancelCard_ReturnType returnData);

        public delegate void CancelCastleAttack_UserCallBack(CancelCastleAttack_ReturnType returnData);

        public delegate void CancelDeleteVillageBuilding_UserCallBack(CancelDeleteVillageBuilding_ReturnType returnData);

        public delegate void CancelInterdiction_UserCallBack(CancelInterdiction_ReturnType returnData);

        public delegate void ChangeCastleElementAggressiveDefender_UserCallBack(ChangeCastleElementAggressiveDefender_ReturnType returnData);

        public delegate void ChangeFactionMotto_UserCallBack(ChangeFactionMotto_ReturnType returnData);

        public delegate void Chat_Admin_Command_UserCallBack(Chat_Admin_Command_ReturnType returnData);

        public delegate void Chat_BackFillParishText_UserCallBack(Chat_BackFillParishText_ReturnType returnData);

        public delegate void Chat_Login_UserCallBack(Chat_Login_ReturnType returnData);

        public delegate void Chat_Logout_UserCallBack(Chat_Logout_ReturnType returnData);

        public delegate void Chat_MarkParishTextRead_UserCallBack(Chat_MarkParishTextRead_ReturnType returnData);

        public delegate void Chat_ReceiveParishText_UserCallBack(Chat_ReceiveParishText_ReturnType returnData);

        public delegate void Chat_ReceiveText_UserCallBack(Chat_ReceiveText_ReturnType returnData);

        public delegate void Chat_SendParishText_UserCallBack(Chat_SendParishText_ReturnType returnData);

        public delegate void Chat_SendText_UserCallBack(Chat_SendText_ReturnType returnData);

        public delegate void Chat_SetReceivingState_UserCallBack(Chat_SetReceivingState_ReturnType returnData);

        public delegate void CheatAddTroops_UserCallBack(CheatAddTroops_ReturnType returnData);

        public delegate void CheckQuestObjectiveComplete_UserCallBack(CheckQuestObjectiveComplete_ReturnType returnData);

        public delegate void CommonData_UserCallBack(Common_ReturnData returnData);

        public delegate void CompleteAbandonNewQuest_UserCallBack(CompleteAbandonNewQuest_ReturnType returnData);

        public delegate void CompleteQuest_UserCallBack(CompleteQuest_ReturnType returnData);

        public delegate void CompleteVillageCastle_UserCallBack(CompleteVillageCastle_ReturnType returnData);

        public delegate void ConvertVillage_UserCallBack(ConvertVillage_ReturnType returnData);

        public delegate void CreateFaction_UserCallBack(CreateFaction_ReturnType returnData);

        public delegate void CreateFactionRelationship_UserCallBack(CreateFactionRelationship_ReturnType returnData);

        public delegate void CreateForum_UserCallBack(CreateForum_ReturnType returnData);

        public delegate void CreateHouseRelationship_UserCallBack(CreateHouseRelationship_ReturnType returnData);

        public delegate void CreateMailFolder_UserCallBack(CreateMailFolder_ReturnType returnData);

        public delegate void CreateNewUser_UserCallBack(CreateNewUser_ReturnType returnData);

        public delegate void CreateUserRelationship_UserCallBack(CreateUserRelationship_ReturnType returnData);

        public delegate void DeleteCastleElement_UserCallBack(DeleteCastleElement_ReturnType returnData);

        public delegate void DeleteForum_UserCallBack(DeleteForum_ReturnType returnData);

        public delegate void DeleteForumPost_UserCallBack(DeleteForumPost_ReturnType returnData);

        public delegate void DeleteForumThread_UserCallBack(DeleteForumThread_ReturnType returnData);

        public delegate void DeleteMailThread_UserCallBack(DeleteMailThread_ReturnType returnData);

        public delegate void DeleteReports_UserCallBack(DeleteReports_ReturnType returnData);

        public delegate void DeleteVillageBuilding_UserCallBack(DeleteVillageBuilding_ReturnType returnData);

        public delegate void DisbandFaction_UserCallBack(DisbandFaction_ReturnType returnData);

        public delegate void DisbandPeople_UserCallBack(DisbandPeople_ReturnType returnData);

        public delegate void DisbandTroops_UserCallBack(DisbandTroops_ReturnType returnData);

        public delegate void DonateCapitalGoods_UserCallBack(DonateCapitalGoods_ReturnType returnData);

        public delegate void DoResearch_UserCallBack(DoResearch_ReturnType returnData);

        public delegate void FactionApplication_UserCallBack(FactionApplication_ReturnType returnData);

        public delegate void FactionApplicationProcessing_UserCallBack(FactionApplicationProcessing_ReturnType returnData);

        public delegate void FactionChangeMemberStatus_UserCallBack(FactionChangeMemberStatus_ReturnType returnData);

        public delegate void FactionLeadershipVote_UserCallBack(FactionLeadershipVote_ReturnType returnData);

        public delegate void FactionLeave_UserCallBack(FactionLeave_ReturnType returnData);

        public delegate void FactionReplyToInvite_UserCallBack(FactionReplyToInvite_ReturnType returnData);

        public delegate void FactionSendInvite_UserCallBack(FactionSendInvite_ReturnType returnData);

        public delegate void FactionWithdrawInvite_UserCallBack(FactionWithdrawInvite_ReturnType returnData);

        public delegate void FlagMailRead_UserCallBack(FlagMailRead_ReturnType returnData);

        public delegate void FlagQuestObjectiveComplete_UserCallBack(FlagQuestObjectiveComplete_ReturnType returnData);

        public delegate void ForwardReport_UserCallBack(ForwardReport_ReturnType returnData);

        public delegate void FullTick_UserCallBack(FullTick_ReturnType returnData);

        public delegate void GetActivePeople_UserCallBack(GetActivePeople_ReturnType returnData);

        public delegate void GetActiveTraders_UserCallBack(GetActiveTraders_ReturnType returnData);

        public delegate void GetAdminStats_UserCallBack(GetAdminStats_ReturnType returnData);

        public delegate void GetAllVillageOwnerFactions_UserCallBack(GetAllVillageOwnerFactions_ReturnType returnData);

        public delegate void GetAreaFactionChanges_UserCallBack(GetAreaFactionChanges_ReturnType returnData);

        public delegate void GetArmyData_UserCallBack(GetArmyData_ReturnType returnData);

        public delegate void GetBattleHonourRating_UserCallBack(GetBattleHonourRating_ReturnType returnData);

        public delegate void GetCapitalBarracksSpace_UserCallBack(GetCapitalBarracksSpace_ReturnType returnData);

        public delegate void GetCastle_UserCallBack(GetCastle_ReturnType returnData);

        public delegate void GetCountryElectionInfo_UserCallBack(GetCountryElectionInfo_ReturnType returnData);

        public delegate void GetCountryFrontPageInfo_UserCallBack(GetCountryFrontPageInfo_ReturnType returnData);

        public delegate void GetCountyElectionInfo_UserCallBack(GetCountyElectionInfo_ReturnType returnData);

        public delegate void GetCountyFrontPageInfo_UserCallBack(GetCountyFrontPageInfo_ReturnType returnData);

        public delegate void GetCurrentElectionInfo_UserCallBack(GetCurrentElectionInfo_ReturnType returnData);

        public delegate void GetExcommunicationStatus_UserCallBack(GetExcommunicationStatus_ReturnType returnData);

        public delegate void GetFactionData_UserCallBack(GetFactionData_ReturnType returnData);

        public delegate void GetForumList_UserCallBack(GetForumList_ReturnType returnData);

        public delegate void GetForumThread_UserCallBack(GetForumThread_ReturnType returnData);

        public delegate void GetForumThreadList_UserCallBack(GetForumThreadList_ReturnType returnData);

        public delegate void GetHistoricalData_UserCallBack(GetHistoricalData_ReturnType returnData);

        public delegate void GetHouseGloryPoints_UserCallBack(GetHouseGloryPoints_ReturnType returnData);

        public delegate void GetIngameMessage_UserCallBack(GetIngameMessage_ReturnType returnData);

        public delegate void GetInvasionInfo_UserCallBack(GetInvasionInfo_ReturnType returnData);

        public delegate void GetLastAttacker_UserCallBack(GetLastAttacker_ReturnType returnData);

        public delegate void GetLoginHistory_UserCallBack(GetLoginHistory_ReturnType returnData);

        public delegate void GetMailFolders_UserCallBack(GetMailFolders_ReturnType returnData);

        public delegate void GetMailRecipientsHistory_UserCallBack(GetMailRecipientsHistory_ReturnType returnData);

        public delegate void GetMailThread_UserCallBack(GetMailThread_ReturnType returnData);

        public delegate void GetMailThreadList_UserCallBack(GetMailThreadList_ReturnType returnData);

        public delegate void GetMailUserSearch_UserCallBack(GetMailUserSearch_ReturnType returnData);

        public delegate void GetOtherUserVillageIDList_UserCallBack(GetOtherUserVillageIDList_ReturnType returnData);

        public delegate void GetParishFrontPageInfo_UserCallBack(GetParishFrontPageInfo_ReturnType returnData);

        public delegate void GetParishMembersList_UserCallBack(GetParishMembersList_ReturnType returnData);

        public delegate void GetPreVassalInfo_UserCallBack(GetPreVassalInfo_ReturnType returnData);

        public delegate void GetProvinceElectionInfo_UserCallBack(GetProvinceElectionInfo_ReturnType returnData);

        public delegate void GetProvinceFrontPageInfo_UserCallBack(GetProvinceFrontPageInfo_ReturnType returnData);

        public delegate void GetQuestData_UserCallBack(GetQuestData_ReturnType returnData);

        public delegate void GetQuestStatus_UserCallBack(GetQuestStatus_ReturnType returnData);

        public delegate void GetReport_UserCallBack(GetReport_ReturnType returnData);

        public delegate void GetReportsList_UserCallBack(GetReportsList_ReturnType returnData);

        public delegate void GetResearchData_UserCallBack(GetResearchData_ReturnType returnData);

        public delegate void GetResourceLevel_UserCallBack(GetResourceLevel_ReturnType returnData);

        public delegate void GetStockExchangeData_UserCallBack(GetStockExchangeData_ReturnType returnData);

        public delegate void GetUserIDFromName_UserCallBack(GetUserIDFromName_ReturnType returnData);

        public delegate void GetUserPeople_UserCallBack(GetUserPeople_ReturnType returnData);

        public delegate void GetUserTraders_UserCallBack(GetUserTraders_ReturnType returnData);

        public delegate void GetUserVillages_UserCallBack(GetUserVillages_ReturnType returnData);

        public delegate void GetVassalArmyInfo_UserCallBack(GetVassalArmyInfo_ReturnType returnData);

        public delegate void GetViewFactionData_UserCallBack(GetViewFactionData_ReturnType returnData);

        public delegate void GetViewHouseData_UserCallBack(GetViewHouseData_ReturnType returnData);

        public delegate void GetVillageBuildingsList_UserCallBack(GetVillageBuildingsList_ReturnType returnData);

        public delegate void GetVillageFactionChanges_UserCallBack(GetVillageFactionChanges_ReturnType returnData);

        public delegate void GetVillageInfoForDonateCapitalGoods_UserCallBack(GetVillageInfoForDonateCapitalGoods_ReturnType returnData);

        public delegate void GetVillageNames_UserCallBack(GetVillageNames_ReturnType returnData);

        public delegate void GetVillageRankTaxTree_UserCallBack(GetVillageRankTaxTree_ReturnType returnData);

        public delegate void GetVillageStartLocations_UserCallBack(GetVillageStartLocations_ReturnType returnData);

        public delegate void GiveForumAccess_UserCallBack(GiveForumAccess_ReturnType returnData);

        public delegate void HandleVassalRequest_UserCallBack(HandleVassalRequest_ReturnType returnData);

        public delegate void HouseVote_UserCallBack(HouseVote_ReturnType returnData);

        public delegate void HouseVoteHouseLeader_UserCallBack(HouseVoteHouseLeader_ReturnType returnData);

        public delegate void InitialiseFreeCards_UserCallBack(InitialiseFreeCards_ReturnType returnData);

        public delegate void LaunchCastleAttack_UserCallBack(LaunchCastleAttack_ReturnType returnData);

        public delegate void LeaderBoard_UserCallBack(LeaderBoard_ReturnType returnData);

        public delegate void LeaderBoardSearch_UserCallBack(LeaderBoardSearch_ReturnType returnData);

        public delegate void LeaveHouse_UserCallBack(LeaveHouse_ReturnType returnData);

        public delegate void LoginUser_UserCallBack(LoginUser_ReturnType returnData);

        public delegate void LoginUserGuid_UserCallBack(LoginUserGuid_ReturnType returnData);

        public delegate void LogOut_UserCallBack(LogOut_ReturnType returnData);

        public delegate void MakeCountryVote_UserCallBack(MakeCountryVote_ReturnType returnData);

        public delegate void MakeCountyVote_UserCallBack(MakeCountyVote_ReturnType returnData);

        public delegate void MakeParishVote_UserCallBack(MakeParishVote_ReturnType returnData);

        public delegate void MakePeople_UserCallBack(MakePeople_ReturnType returnData);

        public delegate void MakeProvinceVote_UserCallBack(MakeProvinceVote_ReturnType returnData);

        public delegate void MakeTroop_UserCallBack(MakeTroop_ReturnType returnData);

        public delegate void ManageReportFolders_UserCallBack(ManageReportFolders_ReturnType returnData);

        public delegate void MemorizeCastleTroops_UserCallBack(MemorizeCastleTroops_ReturnType returnData);

        public delegate void MoveToMailFolder_UserCallBack(MoveToMailFolder_ReturnType returnData);

        public delegate void MoveVillageBuilding_UserCallBack(MoveVillageBuilding_ReturnType returnData);

        public delegate void NewForumThread_UserCallBack(NewForumThread_ReturnType returnData);

        public delegate void ParishWallDetailInfo_UserCallBack(ParishWallDetailInfo_ReturnType returnData);

        public delegate void PlaceVillageBuilding_UserCallBack(PlaceVillageBuilding_ReturnType returnData);

        public delegate void PostToForumThread_UserCallBack(PostToForumThread_ReturnType returnData);

        public delegate void PreAttackSetup_UserCallBack(PreAttackSetup_ReturnType returnData);

        public delegate void PremiumOverview_UserCallBack(PremiumOverview_ReturnType returnData);

        public delegate void PreValidateCardToBePlayed_UserCallBack(PreValidateCardToBePlayed_ReturnType returnData);

        public delegate AchievementProgress_ReturnType RemoteAsyncDelegate_AchievementProgress(int userID, int sessionID);

        public delegate AddCastleElement_ReturnType RemoteAsyncDelegate_AddCastleElement(int userID, int sessionID, int villageID, int elementType, int xPos, int yPos, long clientElementNumber, int wallEndX, int wallEndY, bool reinforcement, bool vassalReinforcement, byte[,] elementList, long[] troopsToDelete, MoveElementData[] troopsToMove);

        public delegate AddUserToFavourites_ReturnType RemoteAsyncDelegate_AddUserToFavourites(int userID, int sessionID, string userName, bool doRemove);

        public delegate ArmyAttack_ReturnType RemoteAsyncDelegate_ArmyAttack(int userID, int sessionID, int armyID, int targetVillage, int attackType);

        public delegate AutoRepairCastle_ReturnType RemoteAsyncDelegate_AutoRepairCastle(int userID, int sessionID, int villageID);

        public delegate BreakLiegeLord_ReturnType RemoteAsyncDelegate_BreakLiegeLord(int userID, int sessionID, int villageID, int targetVillage);

        public delegate BreakVassalage_ReturnType RemoteAsyncDelegate_BreakVassalage(int userID, int sessionID, int villageID, int targetVillage);

        public delegate BuyResearchPoint_ReturnType RemoteAsyncDelegate_BuyResearchPoint(int userID, int sessionID);

        public delegate BuyVillage_ReturnType RemoteAsyncDelegate_BuyVillage(int userID, int sessionID, int fromVillageID, int villageID, int mapType, long startChangePos, bool peaceTime);

        public delegate CancelCard_ReturnType RemoteAsyncDelegate_CancelCard(int userID, int sessionID, int card);

        public delegate CancelCastleAttack_ReturnType RemoteAsyncDelegate_CancelCastleAttack(int userID, int sessionID, long armyID);

        public delegate CancelDeleteVillageBuilding_ReturnType RemoteAsyncDelegate_CancelDeleteVillageBuilding(int userID, int sessionID, int villageID, long buildingID);

        public delegate CancelInterdiction_ReturnType RemoteAsyncDelegate_CancelInterdiction(int userID, int sessionID, int villageID);

        public delegate ChangeCastleElementAggressiveDefender_ReturnType RemoteAsyncDelegate_ChangeCastleElementAggressiveDefender(int userID, int sessionID, int villageID, long[] elementID, bool state);

        public delegate ChangeFactionMotto_ReturnType RemoteAsyncDelegate_ChangeFactionMotto(int userID, int sessionID, string factionName, string factionNameAbrv, string motto, int flagData);

        public delegate Chat_Admin_Command_ReturnType RemoteAsyncDelegate_Chat_Admin_Command(int userID, int sessionID, int command, int targetUserID);

        public delegate Chat_BackFillParishText_ReturnType RemoteAsyncDelegate_Chat_BackFillParishText(int userID, int sessionID, int parishID, int subPage, long oldestIDDownloaded, DateTime oldestTime, bool filter);

        public delegate Chat_Login_ReturnType RemoteAsyncDelegate_Chat_Login(int userID, int sessionID);

        public delegate Chat_Logout_ReturnType RemoteAsyncDelegate_Chat_Logout(int userID, int sessionID);

        public delegate Chat_MarkParishTextRead_ReturnType RemoteAsyncDelegate_Chat_MarkParishTextRead(int userID, int sessionID, int parishID, int pageID, long readID);

        public delegate Chat_ReceiveParishText_ReturnType RemoteAsyncDelegate_Chat_ReceiveParishText(int userID, int sessionID, int parishID, DateTime lastTime, bool filter);

        public delegate Chat_ReceiveText_ReturnType RemoteAsyncDelegate_Chat_ReceiveText(int userID, int sessionID, List<Chat_RoomID> roomsToRegister, bool changeRooms, bool filter);

        public delegate Chat_SendParishText_ReturnType RemoteAsyncDelegate_Chat_SendParishText(int userID, int sessionID, int parishID, int subForumID, string text, DateTime lastTime, bool filter);

        public delegate Chat_SendText_ReturnType RemoteAsyncDelegate_Chat_SendText(int userID, int sessionID, int roomType, int roomID, string text, bool filter);

        public delegate Chat_SetReceivingState_ReturnType RemoteAsyncDelegate_Chat_SetReceivingState(int userID, int sessionID, bool state);

        public delegate CheatAddTroops_ReturnType RemoteAsyncDelegate_CheatAddTroops(int userID, int sessionID, int villageID, int troopType, int numToMake);

        public delegate CheckQuestObjectiveComplete_ReturnType RemoteAsyncDelegate_CheckQuestObjectiveComplete(int userID, int sessionID, int quest);

        public delegate CompleteAbandonNewQuest_ReturnType RemoteAsyncDelegate_CompleteAbandonNewQuest(int userID, int sessionID, int questID, bool abandon, bool glory, int villageID);

        public delegate CompleteQuest_ReturnType RemoteAsyncDelegate_CompleteQuest(int userID, int sessionID, int quest);

        public delegate CompleteVillageCastle_ReturnType RemoteAsyncDelegate_CompleteVillageCastle(int userID, int sessionID, int villageID, int mode);

        public delegate ConvertVillage_ReturnType RemoteAsyncDelegate_ConvertVillage(int userID, int sessionID, int villageID, int mapType);

        public delegate CreateFaction_ReturnType RemoteAsyncDelegate_CreateFaction(int userID, int sessionID, string factionName, string factionNameabrv, string factionMotto, int flagdata);

        public delegate CreateFactionRelationship_ReturnType RemoteAsyncDelegate_CreateFactionRelationship(int userID, int sessionID, int targetFactionID, int relationship);

        public delegate CreateForum_ReturnType RemoteAsyncDelegate_CreateForum(int userID, int sessionID, int areaID, int areaType, string name);

        public delegate CreateHouseRelationship_ReturnType RemoteAsyncDelegate_CreateHouseRelationship(int userID, int sessionID, int targetHouseID, int relationship);

        public delegate CreateMailFolder_ReturnType RemoteAsyncDelegate_CreateMailFolder(int userID, int sessionID, string folderName);

        public delegate CreateNewUser_ReturnType RemoteAsyncDelegate_CreateNewUser(string username, string password, string realname, string emailaddress, int versionNo, string securityString);

        public delegate CreateUserRelationship_ReturnType RemoteAsyncDelegate_CreateUserRelationship(int userID, int sessionID, int targetUserID, int relationship);

        public delegate DeleteCastleElement_ReturnType RemoteAsyncDelegate_DeleteCastleElement(int userID, int sessionID, int villageID, long elementNumber, List<long> elementList);

        public delegate DeleteForum_ReturnType RemoteAsyncDelegate_DeleteForum(int userID, int sessionID, int areaID, int areaType, long forumID);

        public delegate DeleteForumPost_ReturnType RemoteAsyncDelegate_DeleteForumPost(int userID, int sessionID, int areaID, int areaType, string forumTitle, long forumID, long forumThreadID, long forumPostID);

        public delegate DeleteForumThread_ReturnType RemoteAsyncDelegate_DeleteForumThread(int userID, int sessionID, int areaID, int areaType, string forumTitle, long forumID, long forumThreadID);

        public delegate DeleteMailThread_ReturnType RemoteAsyncDelegate_DeleteMailThread(int userID, int sessionID, long threadID);

        public delegate DeleteReports_ReturnType RemoteAsyncDelegate_DeleteReports(int userID, int sessionID, int mode, long[] reportsToDelete, long folderID);

        public delegate DeleteVillageBuilding_ReturnType RemoteAsyncDelegate_DeleteVillageBuilding(int userID, int sessionID, int villageID, long buildingID);

        public delegate DisbandFaction_ReturnType RemoteAsyncDelegate_DisbandFaction(int userID, int sessionID, int factionID);

        public delegate DisbandPeople_ReturnType RemoteAsyncDelegate_DisbandPeople(int userID, int sessionID, int villageID, int troopType, int amount);

        public delegate DisbandTroops_ReturnType RemoteAsyncDelegate_DisbandTroops(int userID, int sessionID, int villageID, int troopType, int amount);

        public delegate DonateCapitalGoods_ReturnType RemoteAsyncDelegate_DonateCapitalGoods(int userID, int sessionID, int targetVillageID, int sourceVillageID, int resourceType, int amount, int buildingType, long targetBuildingID);

        public delegate DoResearch_ReturnType RemoteAsyncDelegate_DoResearch(int userID, int sessionID, int researchType, int queuePos);

        public delegate FactionApplication_ReturnType RemoteAsyncDelegate_FactionApplication(int userID, int sessionID, int factionID, bool cancel);

        public delegate FactionApplicationProcessing_ReturnType RemoteAsyncDelegate_FactionApplicationProcessing(int userID, int sessionID, int otherUserID, bool accept, bool reject, bool setMode);

        public delegate FactionChangeMemberStatus_ReturnType RemoteAsyncDelegate_FactionChangeMemberStatus(int userID, int sessionID, int memberUserID, int targetRank);

        public delegate FactionLeadershipVote_ReturnType RemoteAsyncDelegate_FactionLeadershipVote(int userID, int sessionID, int factionID, int votedID);

        public delegate FactionLeave_ReturnType RemoteAsyncDelegate_FactionLeave(int userID, int sessionID);

        public delegate FactionReplyToInvite_ReturnType RemoteAsyncDelegate_FactionReplyToInvite(int userID, int sessionID, int factionID, bool accept);

        public delegate FactionSendInvite_ReturnType RemoteAsyncDelegate_FactionSendInvite(int userID, int sessionID, string targetUser);

        public delegate FactionWithdrawInvite_ReturnType RemoteAsyncDelegate_FactionWithdrawInvite(int userID, int sessionID, int targetUserID);

        public delegate FlagMailRead_ReturnType RemoteAsyncDelegate_FlagMailRead(int userID, int sessionID, long mailID, long threadID, bool asRead);

        public delegate FlagQuestObjectiveComplete_ReturnType RemoteAsyncDelegate_FlagQuestObjectiveComplete(int userID, int sessionID, int objective);

        public delegate ForwardReport_ReturnType RemoteAsyncDelegate_ForwardReport(int userID, int sessionID, long reportID, string[] recipients);

        public delegate FullTick_ReturnType RemoteAsyncDelegate_FullTick(int userID, int sessionID, long startChangePos, long regionStartPos, long countyStartPos, long provinceStartPos, long countryStartPos, bool registerSession, long villageNamePos, long factionsChangePos, DateTime lastTraderTime, DateTime lastPeopleTime, long parishFlagsPos, long countyFlagsPos, long provinceFlagsPos, long countryFlagsPos, long highestArmyID, int mode, bool fullMode);

        public delegate GetActivePeople_ReturnType RemoteAsyncDelegate_GetActivePeople(int userID, int sessionID, DateTime lastTime);

        public delegate GetActiveTraders_ReturnType RemoteAsyncDelegate_GetActiveTraders(int userID, int sessionID, DateTime lastTime);

        public delegate GetAdminStats_ReturnType RemoteAsyncDelegate_GetAdminStats(int userID, int sessionID);

        public delegate GetAllVillageOwnerFactions_ReturnType RemoteAsyncDelegate_GetAllVillageOwnerFactions(int userID, int sessionID, int sendIndex);

        public delegate GetAreaFactionChanges_ReturnType RemoteAsyncDelegate_GetAreaFactionChanges(int userID, int sessionID, long regionStartPos, long countyStartPos, long provinceStartPos, long countryStartPos, long parishFlagsPos, long countyFlagsPos, long provinceFlagsPos, long countryFlagsPos);

        public delegate GetArmyData_ReturnType RemoteAsyncDelegate_GetArmyData(int userID, int sessionID, long highestSeendID);

        public delegate GetBattleHonourRating_ReturnType RemoteAsyncDelegate_GetBattleHonourRating(int userID, int sessionID, int attackedVillage);

        public delegate GetCapitalBarracksSpace_ReturnType RemoteAsyncDelegate_GetCapitalBarracksSpace(int userID, int sessionID, int sourceVillageID, int targetVillageID);

        public delegate GetCastle_ReturnType RemoteAsyncDelegate_GetCastle(int userID, int sessionID, int villageID);

        public delegate GetCountryElectionInfo_ReturnType RemoteAsyncDelegate_GetCountryElectionInfo(int userID, int sessionID, int villageID);

        public delegate GetCountryFrontPageInfo_ReturnType RemoteAsyncDelegate_GetCountryFrontPageInfo(int userID, int sessionID, int villageID);

        public delegate GetCountyElectionInfo_ReturnType RemoteAsyncDelegate_GetCountyElectionInfo(int userID, int sessionID, int villageID);

        public delegate GetCountyFrontPageInfo_ReturnType RemoteAsyncDelegate_GetCountyFrontPageInfo(int userID, int sessionID, int villageID);

        public delegate GetCurrentElectionInfo_ReturnType RemoteAsyncDelegate_GetCurrentElectionInfo(int userID, int sessionID, int areaID, int areaType);

        public delegate GetExcommunicationStatus_ReturnType RemoteAsyncDelegate_GetExcommunicationStatus(int userID, int sessionID, int villageID, int targetVillageID);

        public delegate GetFactionData_ReturnType RemoteAsyncDelegate_GetFactionData(int userID, int sessionID, int factionID, long factionChangesPos);

        public delegate GetForumList_ReturnType RemoteAsyncDelegate_GetForumList(int userID, int sessionID, int areaID, int areaType);

        public delegate GetForumThread_ReturnType RemoteAsyncDelegate_GetForumThread(int userID, int sessionID, long forumID, long threadID, DateTime lastGet, bool forceGet);

        public delegate GetForumThreadList_ReturnType RemoteAsyncDelegate_GetForumThreadList(int userID, int sessionID, long forumID, DateTime lastGet, bool forceGet);

        public delegate GetHistoricalData_ReturnType RemoteAsyncDelegate_GetHistoricalData(int userID, int sessionID);

        public delegate GetHouseGloryPoints_ReturnType RemoteAsyncDelegate_GetHouseGloryPoints(int userID, int sessionID);

        public delegate GetIngameMessage_ReturnType RemoteAsyncDelegate_GetIngameMessage(int userID, int sessionID);

        public delegate GetInvasionInfo_ReturnType RemoteAsyncDelegate_GetInvasionInfo(int userID, int sessionID);

        public delegate GetLastAttacker_ReturnType RemoteAsyncDelegate_GetLastAttacker(int userID, int sessionID);

        public delegate GetLoginHistory_ReturnType RemoteAsyncDelegate_GetLoginHistory(int userID, int sessionID);

        public delegate GetMailFolders_ReturnType RemoteAsyncDelegate_GetMailFolders(int userID, int sessionID);

        public delegate GetMailRecipientsHistory_ReturnType RemoteAsyncDelegate_GetMailRecipientsHistory(int userID, int sessionID);

        public delegate GetMailThread_ReturnType RemoteAsyncDelegate_GetMailThread(int userID, int sessionID, long threadID, int localCount, long highestSegmentID);

        public delegate GetMailThreadList_ReturnType RemoteAsyncDelegate_GetMailThreadList(int userID, int sessionID, bool initialRequest, int retrieveMode, DateTime lastRetrieved);

        public delegate GetMailUserSearch_ReturnType RemoteAsyncDelegate_GetMailUserSearch(int userID, int sessionID, string filter);

        public delegate GetOtherUserVillageIDList_ReturnType RemoteAsyncDelegate_GetOtherUserVillageIDList(int userID, string userName, int sessionID);

        public delegate GetParishFrontPageInfo_ReturnType RemoteAsyncDelegate_GetParishFrontPageInfo(int userID, int sessionID, int villageID, DateTime lastTime);

        public delegate GetParishMembersList_ReturnType RemoteAsyncDelegate_GetParishMembersList(int userID, int sessionID, int villageID);

        public delegate GetPreVassalInfo_ReturnType RemoteAsyncDelegate_GetPreVassalInfo(int userID, int sessionID, int yourVillageID, int targetVillageID);

        public delegate GetProvinceElectionInfo_ReturnType RemoteAsyncDelegate_GetProvinceElectionInfo(int userID, int sessionID, int villageID);

        public delegate GetProvinceFrontPageInfo_ReturnType RemoteAsyncDelegate_GetProvinceFrontPageInfo(int userID, int sessionID, int villageID);

        public delegate GetQuestData_ReturnType RemoteAsyncDelegate_GetQuestData(int userID, int sessionID, bool full);

        public delegate GetQuestStatus_ReturnType RemoteAsyncDelegate_GetQuestStatus(int userID, int sessionID);

        public delegate GetReport_ReturnType RemoteAsyncDelegate_GetReport(int userID, int sessionID, long reportID);

        public delegate GetReportsList_ReturnType RemoteAsyncDelegate_GetReportsList(int userID, int sessionID, int readFilter, int[] typeFilters, long folderID, long clientHighest);

        public delegate GetResearchData_ReturnType RemoteAsyncDelegate_GetResearchData(int userID, int sessionID);

        public delegate GetResourceLevel_ReturnType RemoteAsyncDelegate_GetResourceLevel(int userID, int sessionID, int villageID, int buildingType);

        public delegate GetStockExchangeData_ReturnType RemoteAsyncDelegate_GetStockExchangeData(int userID, int sessionID, int villageID, bool stockExchange, int[] closeVillages);

        public delegate GetUserIDFromName_ReturnType RemoteAsyncDelegate_GetUserIDFromName(int userID, int sessionID, string targetUser);

        public delegate GetUserPeople_ReturnType RemoteAsyncDelegate_GetUserPeople(int userID, int sessionID);

        public delegate GetUserTraders_ReturnType RemoteAsyncDelegate_GetUserTraders(int userID, int sessionID, int villageID);

        public delegate GetUserVillages_ReturnType RemoteAsyncDelegate_GetUserVillages(int userID, int sessionID);

        public delegate GetVassalArmyInfo_ReturnType RemoteAsyncDelegate_GetVassalArmyInfo(int userID, int sessionID, int vassalVillageID, int mode, int attackedVillage);

        public delegate GetViewFactionData_ReturnType RemoteAsyncDelegate_GetViewFactionData(int userID, int sessionID, int factionID);

        public delegate GetViewHouseData_ReturnType RemoteAsyncDelegate_GetViewHouseData(int userID, int sessionID, int houseID);

        public delegate GetVillageBuildingsList_ReturnType RemoteAsyncDelegate_GetVillageBuildingsList(int userID, int sessionID, int villageID, bool fullUpdate, bool viewOnly, bool needParishPeople);

        public delegate GetVillageFactionChanges_ReturnType RemoteAsyncDelegate_GetVillageFactionChanges(int userID, int sessionID, long startChangePos, long factionsChangePos, int sendIndex);

        public delegate GetVillageInfoForDonateCapitalGoods_ReturnType RemoteAsyncDelegate_GetVillageInfoForDonateCapitalGoods(int userID, int sessionID, int parishCapitalID, int targetBuildingType);

        public delegate GetVillageNames_ReturnType RemoteAsyncDelegate_GetVillageNames(int userID, int sessionID, long currentPos, int sendIndex);

        public delegate GetVillageRankTaxTree_ReturnType RemoteAsyncDelegate_GetVillageRankTaxTree(int userID, int sessionID);

        public delegate GetVillageStartLocations_ReturnType RemoteAsyncDelegate_GetVillageStartLocations(int userID, int sessionID);

        public delegate GiveForumAccess_ReturnType RemoteAsyncDelegate_GiveForumAccess(int userID, int sessionID, long forumID, int[] users);

        public delegate HandleVassalRequest_ReturnType RemoteAsyncDelegate_HandleVassalRequest(int userID, int sessionID, int command, int liegeLordVillageID, int vassalVillageID);

        public delegate HouseVote_ReturnType RemoteAsyncDelegate_HouseVote(int userID, int sessionID, int factionID, int houseID, int targetFaction, bool application, bool vote, long factionsChangePos);

        public delegate HouseVoteHouseLeader_ReturnType RemoteAsyncDelegate_HouseVoteHouseLeader(int userID, int sessionID, int factionID, int houseID, int leaderVote, long factionsChangePos);

        public delegate InitialiseFreeCards_ReturnType RemoteAsyncDelegate_InitialiseFreeCards(int userID, int sessionID);

        public delegate LaunchCastleAttack_ReturnType RemoteAsyncDelegate_LaunchCastleAttack(int userID, int sessionID, int parentOfAttackingVillageID, int targetVillageID, int sourceVillageID, byte[] attackersMap, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int attackType, int pillagePercent, int CaptainsCommand, int numCaptains);

        public delegate LeaderBoard_ReturnType RemoteAsyncDelegate_LeaderBoard(int userID, int sessionID, int mode, int minValue, int maxValue, DateTime lastUpdate);

        public delegate LeaderBoardSearch_ReturnType RemoteAsyncDelegate_LeaderBoardSearch(int userID, int sessionID, int category, string searchString, DateTime lastUpdate);

        public delegate LeaveHouse_ReturnType RemoteAsyncDelegate_LeaveHouse(int userID, int sessionID, int factionID, int houseID, long factionsChangePos);

        public delegate LoginUser_ReturnType RemoteAsyncDelegate_LoginUser(string username, string password, int versionNo, string verificationString, bool needVillageData);

        public delegate LoginUserGuid_ReturnType RemoteAsyncDelegate_LoginUserGuid(string userName, string userGuid, string sessionGuid, bool needVillageData, int versionID);

        public delegate LogOut_ReturnType RemoteAsyncDelegate_LogOut(int userID, int sessionID, bool manual, bool autoScout, bool autoTrade, bool autoAttack, bool autoAttackWolf, bool autoAttackBandit, bool autoAttackAI, int resourceType, int percent, bool autoRecruit, bool autoRecruitPeasant, bool autoRecruitArchers, bool autoRecruitPikemen, bool autoRecruitSwordsmen, bool autoRecruitCatapults, int autoRecruitPeasant_Cap, int autoRecruitArchers_Cap, int autoRecruitPikemen_Cap, int autoRecruitSwordsmen_Cap, int autoRecruitCatapults_Cap);

        public delegate MakeCountryVote_ReturnType RemoteAsyncDelegate_MakeCountryVote(int userID, int sessionID, int villageID, int votedUserID);

        public delegate MakeCountyVote_ReturnType RemoteAsyncDelegate_MakeCountyVote(int userID, int sessionID, int villageID, int votedUserID);

        public delegate MakeParishVote_ReturnType RemoteAsyncDelegate_MakeParishVote(int userID, int sessionID, int villageID, int votedUserID);

        public delegate MakePeople_ReturnType RemoteAsyncDelegate_MakePeople(int userID, int sessionID, int villageIDF, int personType);

        public delegate MakeProvinceVote_ReturnType RemoteAsyncDelegate_MakeProvinceVote(int userID, int sessionID, int villageID, int votedUserID);

        public delegate MakeTroop_ReturnType RemoteAsyncDelegate_MakeTroop(int userID, int sessionID, int villageID, int troopType, int amount);

        public delegate ManageReportFolders_ReturnType RemoteAsyncDelegate_ManageReportFolders(int userID, int sessionID, int mode, long folderID, string groupNames);

        public delegate MemorizeCastleTroops_ReturnType RemoteAsyncDelegate_MemorizeCastleTroops(int userID, int sessionID, int villageID);

        public delegate MoveToMailFolder_ReturnType RemoteAsyncDelegate_MoveToMailFolder(int userID, int sessionID, long threadID, long folderID);

        public delegate MoveVillageBuilding_ReturnType RemoteAsyncDelegate_MoveVillageBuilding(int userID, int sessionID, int villageID, long buildingID, Point buildingLocation);

        public delegate NewForumThread_ReturnType RemoteAsyncDelegate_NewForumThread(int userID, int sessionID, long forumID, string headingText, string bodyText);

        public delegate ParishWallDetailInfo_ReturnType RemoteAsyncDelegate_ParishWallDetailInfo(int userID, int sessionID, int parishCapitalID, long wallInfoID, int targetUserId, int type);

        public delegate PlaceVillageBuilding_ReturnType RemoteAsyncDelegate_PlaceVillageBuilding(int userID, int sessionID, int villageID, int buildingType, Point buildingLocation);

        public delegate PostToForumThread_ReturnType RemoteAsyncDelegate_PostToForumThread(int userID, int sessionID, long threadID, long forumID, string text);

        public delegate PreAttackSetup_ReturnType RemoteAsyncDelegate_PreAttackSetup(int userID, int sessionID, int parentAttackingVillage, int attackingVillage, int targetVillage, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int attackType, int pillagePercent, int captainsCommand);

        public delegate PremiumOverview_ReturnType RemoteAsyncDelegate_PremiumOverview(int userID, int sessionID);

        public delegate PreValidateCardToBePlayed_ReturnType RemoteAsyncDelegate_PreValidateCardToBePlayed(int userID, int sessionID, int card, int data);

        public delegate RemoveMailFolder_ReturnType RemoteAsyncDelegate_RemoveMailFolder(int userID, int sessionID, long folderID);

        public delegate ReportMail_ReturnType RemoteAsyncDelegate_ReportMail(int userID, int sessionID, long mailID, long threadID, string reason, string summary);

        public delegate ResendVerificationEmail_ReturnType RemoteAsyncDelegate_ResendVerificationEmail(string username, string password);

        public delegate RestoreCastleTroops_ReturnType RemoteAsyncDelegate_RestoreCastleTroops(int userID, int sessionID, int villageID);

        public delegate RetrieveArmyFromGarrison_ReturnType RemoteAsyncDelegate_RetrieveArmyFromGarrison(int userID, int sessionID, int villageID);

        public delegate RetrieveAttackResult_ReturnType RemoteAsyncDelegate_RetrieveAttackResult(int userID, int sessionID, long armyID, long startChangePos);

        public delegate RetrievePeople_ReturnType RemoteAsyncDelegate_RetrievePeople(int userID, int sessionID, int villageID, List<long> people, int personType);

        public delegate RetrieveStats_ReturnType RemoteAsyncDelegate_RetrieveStats(int userID, int sessionID);

        public delegate RetrieveTroopsFromVassal_ReturnType RemoteAsyncDelegate_RetrieveTroopsFromVassal(int userID, int sessionID, int liegeLordVillageID, int vassalVillageID);

        public delegate RetrieveVillageUserInfo_ReturnType RemoteAsyncDelegate_RetrieveVillageUserInfo(int userID, int sessionID, int villageID, int targetUserID, bool extended);

        public delegate ReturnReinforcements_ReturnType RemoteAsyncDelegate_ReturnReinforcements(int userID, int sessionID, long reinforcementID, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults);

        public delegate SelfJoinHouse_ReturnType RemoteAsyncDelegate_SelfJoinHouse(int userID, int sessionID, int factionID, int houseID, long factionsChangePos);

        public delegate SendCommands_ReturnType RemoteAsyncDelegate_SendCommands(int userID, int sessionID, int targetUserID, int command, int duration, string reason);

        public delegate SendMail_ReturnType RemoteAsyncDelegate_SendMail(int userID, int sessionID, string subject, string body, string[] recipients, long threadID, bool forwardThread);

        public delegate SendMarketResources_ReturnType RemoteAsyncDelegate_SendMarketResources(int userID, int sessionID, int homeVillageID, int targetVillage, int resource, int amount);

        public delegate SendPeople_ReturnType RemoteAsyncDelegate_SendPeople(int userID, int sessionID, int homeVillageID, int targetVillage, int personType, int number, int command, int data);

        public delegate SendReinforcements_ReturnType RemoteAsyncDelegate_SendReinforcements(int userID, int sessionID, int homeVillageID, int supportedVillageID, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults);

        public delegate SendScouts_ReturnType RemoteAsyncDelegate_SendScouts(int userID, int sessionID, int targetVillageID, int sourceVillageID, int numScouts);

        public delegate SendSpecialMail_ReturnType RemoteAsyncDelegate_SendSpecialMail(int userID, int sessionID, int mailType, int area, string subject, string body);

        public delegate SendTroopsToCapital_ReturnType RemoteAsyncDelegate_SendTroopsToCapital(int userID, int sessionID, int sourceVillageID, int targetVillageID, int peasants, int archers, int pikemen, int swordsmen, int catapults);

        public delegate SendTroopsToVassal_ReturnType RemoteAsyncDelegate_SendTroopsToVassal(int userID, int sessionID, int liegeLordVillageID, int vassalVillageID, int peasants, int archers, int pikemen, int swordsmen, int catapults);

        public delegate SendVassalRequest_ReturnType RemoteAsyncDelegate_SendVassalRequest(int userID, int sessionID, int yourVillageID, int targetVillageID);

        public delegate SetAdminMessage_ReturnType RemoteAsyncDelegate_SetAdminMessage(int userID, int sessionID, string message, int type);

        public delegate SetHighestArmySeen_ReturnType RemoteAsyncDelegate_SetHighestArmySeen(int userID, int sessionID, long highestArmyIDSeen);

        public delegate SetStartingCounty_ReturnType RemoteAsyncDelegate_SetStartingCounty(int userID, int sessionID, int countyID);

        public delegate SetVacationMode_ReturnType RemoteAsyncDelegate_SetVacationMode(int userID, int sessionID, int numDays);

        public delegate SpecialVillageInfo_ReturnType RemoteAsyncDelegate_SpecialVillageInfo(int userID, int sessionID, int villageID);

        public delegate SpinTheWheel_ReturnType RemoteAsyncDelegate_SpinTheWheel(int userID, int sessionID, int villageID, int wheelType);

        public delegate SpyCommand_ReturnType RemoteAsyncDelegate_SpyCommand(int userID, int sessionID, int villageID, int command);

        public delegate SpyGetArmyInfo_ReturnType RemoteAsyncDelegate_SpyGetArmyInfo(int userID, int sessionID, int villageID);

        public delegate SpyGetResearchInfo_ReturnType RemoteAsyncDelegate_SpyGetResearchInfo(int userID, int sessionID, int villageID);

        public delegate SpyGetVillageResourceInfo_ReturnType RemoteAsyncDelegate_SpyGetVillageResourceInfo(int userID, int sessionID, int villageID);

        public delegate StandDownAsParishDespot_ReturnType RemoteAsyncDelegate_StandDownAsParishDespot(int userID, int sessionID, int villageID);

        public delegate StandInElection_ReturnType RemoteAsyncDelegate_StandInElection(int userID, int sessionID, int areaID, int areaType, bool state);

        public delegate StartNewQuest_ReturnType RemoteAsyncDelegate_StartNewQuest(int userID, int sessionID, int questID);

        public delegate StockExchangeTrade_ReturnType RemoteAsyncDelegate_StockExchangeTrade(int userID, int sessionID, int villageID, int targetExchange, int resource, int amount, bool buy);

        public delegate TestAchievements_ReturnType RemoteAsyncDelegate_TestAchievements(int userID, int sessionID, List<int> achievementsToTest, List<AchievementData> achievementData);

        public delegate TouchHouseVisitDate_ReturnType RemoteAsyncDelegate_TouchHouseVisitDate(int userID, int sessionID, int factionID);

        public delegate TutorialCommand_ReturnType RemoteAsyncDelegate_TutorialCommand(int userID, int sessionID, int tutorialAction);

        public delegate UpdateCurrentCards_ReturnType RemoteAsyncDelegate_UpdateCurrentCards(int userID, int sessionID);

        public delegate UpdateDiplomacyStatus_ReturnType RemoteAsyncDelegate_UpdateDiplomacyStatus(int userID, int sessionID, bool state);

        public delegate UpdateReportFilters_ReturnType RemoteAsyncDelegate_UpdateReportFilters(int userID, int sessionID, ReportFilterList filters);

        public delegate UpdateSelectedTitheType_ReturnType RemoteAsyncDelegate_UpdateSelectedTitheType(int userID, int sessionID, int villageID, int titheType);

        public delegate UpdateUserOptions_ReturnType RemoteAsyncDelegate_UpdateUserOptions(int userID, int sessionID, GameOptionsData options);

        public delegate UpdateVillageFavourites_ReturnType RemoteAsyncDelegate_UpdateVillageFavourites(int userID, int sessionID, int mode, int villageID);

        public delegate UpdateVillageResourcesInfo_ReturnType RemoteAsyncDelegate_UpdateVillageResourcesInfo(int userID, int sessionID, int villageID);

        public delegate UpgradeRank_ReturnType RemoteAsyncDelegate_UpgradeRank(int userID, int sessionID, int curRank, int curRankSubLevel);

        public delegate UploadAvatar_ReturnType RemoteAsyncDelegate_UploadAvatar(int userID, int sessionID, AvatarData avatarData);

        public delegate UserInfo_ReturnType RemoteAsyncDelegate_UserInfo(int userID, int sessionID, int requestUserID);

        public delegate VassalInfo_ReturnType RemoteAsyncDelegate_VassalInfo(int userID, int sessionID, int villageID);

        public delegate VassalSendResources_ReturnType RemoteAsyncDelegate_VassalSendResources(int userID, int sessionID, int liegeLordVillageID, int vassalVillageID, int resourceType, int amount);

        public delegate ViewBattle_ReturnType RemoteAsyncDelegate_ViewBattle(int userID, int sessionID, long reportID);

        public delegate ViewCastle_ReturnType RemoteAsyncDelegate_ViewCastle(int userID, int sessionID, int villageID, long reportID);

        public delegate VillageBuildingChangeRates_ReturnType RemoteAsyncDelegate_VillageBuildingChangeRates(int userID, int sessionID, int villageID, int taxLevel, int rationsLevel, int aleRationsLevel, int capitalTaxRate);

        public delegate VillageBuildingCompleteDataRetrieval_ReturnType RemoteAsyncDelegate_VillageBuildingCompleteDataRetrieval(int userID, int sessionID, int villageID, long buildingID, int mode);

        public delegate VillageBuildingSetActive_ReturnType RemoteAsyncDelegate_VillageBuildingSetActive(int userID, int sessionID, long buildingID, int villageID, int buildingType, bool state);

        public delegate VillageHoldBanquet_ReturnType RemoteAsyncDelegate_VillageHoldBanquet(int userID, int sessionID, int villageID, int venison, int wine, int salt, int spice, int silk, int clothing, int furniture, int metalwork);

        public delegate VillageProduceWeapons_ReturnType RemoteAsyncDelegate_VillageProduceWeapons(int userID, int sessionID, int villageID, int weaponType, int amount);

        public delegate VillageRename_ReturnType RemoteAsyncDelegate_VillageRename(int userID, int sessionID, int villageID, string villageName, bool abandon, bool modReset);

        public delegate VoteInElection_ReturnType RemoteAsyncDelegate_VoteInElection(int userID, int sessionID, int areaID, int areaType, int candidate);

        public delegate WorldInfo_ReturnType RemoteAsyncDelegate_WorldInfo();

        public delegate void RemoveMailFolder_UserCallBack(RemoveMailFolder_ReturnType returnData);

        public delegate void ReportMail_UserCallBack(ReportMail_ReturnType returnData);

        public delegate void ResendVerificationEmail_UserCallBack(ResendVerificationEmail_ReturnType returnData);

        public delegate void RestoreCastleTroops_UserCallBack(RestoreCastleTroops_ReturnType returnData);

        public delegate void RetrieveArmyFromGarrison_UserCallBack(RetrieveArmyFromGarrison_ReturnType returnData);

        public delegate void RetrieveAttackResult_UserCallBack(RetrieveAttackResult_ReturnType returnData);

        public delegate void RetrievePeople_UserCallBack(RetrievePeople_ReturnType returnData);

        public delegate void RetrieveStats_UserCallBack(RetrieveStats_ReturnType returnData);

        public delegate void RetrieveTroopsFromVassal_UserCallBack(RetrieveTroopsFromVassal_ReturnType returnData);

        public delegate void RetrieveVillageUserInfo_UserCallBack(RetrieveVillageUserInfo_ReturnType returnData);

        public delegate void ReturnReinforcements_UserCallBack(ReturnReinforcements_ReturnType returnData);

        public class RTT_Log_data
        {
            public Type packetType;
            public int time;
        }

        public delegate void SelfJoinHouse_UserCallBack(SelfJoinHouse_ReturnType returnData);

        public delegate void SendCommands_UserCallBack(SendCommands_ReturnType returnData);

        public delegate void SendMail_UserCallBack(SendMail_ReturnType returnData);

        public delegate void SendMarketResources_UserCallBack(SendMarketResources_ReturnType returnData);

        public delegate void SendPeople_UserCallBack(SendPeople_ReturnType returnData);

        public delegate void SendReinforcements_UserCallBack(SendReinforcements_ReturnType returnData);

        public delegate void SendScouts_UserCallBack(SendScouts_ReturnType returnData);

        public delegate void SendSpecialMail_UserCallBack(SendSpecialMail_ReturnType returnData);

        public delegate void SendTroopsToCapital_UserCallBack(SendTroopsToCapital_ReturnType returnData);

        public delegate void SendTroopsToVassal_UserCallBack(SendTroopsToVassal_ReturnType returnData);

        public delegate void SendVassalRequest_UserCallBack(SendVassalRequest_ReturnType returnData);

        public delegate void SetAdminMessage_UserCallBack(SetAdminMessage_ReturnType returnData);

        public delegate void SetHighestArmySeen_UserCallBack(SetHighestArmySeen_ReturnType returnData);

        public delegate void SetStartingCounty_UserCallBack(SetStartingCounty_ReturnType returnData);

        public delegate void SetVacationMode_UserCallBack(SetVacationMode_ReturnType returnData);

        public delegate void SpecialVillageInfo_UserCallBack(SpecialVillageInfo_ReturnType returnData);

        public delegate void SpinTheWheel_UserCallBack(SpinTheWheel_ReturnType returnData);

        public delegate void SpyCommand_UserCallBack(SpyCommand_ReturnType returnData);

        public delegate void SpyGetArmyInfo_UserCallBack(SpyGetArmyInfo_ReturnType returnData);

        public delegate void SpyGetResearchInfo_UserCallBack(SpyGetResearchInfo_ReturnType returnData);

        public delegate void SpyGetVillageResourceInfo_UserCallBack(SpyGetVillageResourceInfo_ReturnType returnData);

        public delegate void StandDownAsParishDespot_UserCallBack(StandDownAsParishDespot_ReturnType returnData);

        public delegate void StandInElection_UserCallBack(StandInElection_ReturnType returnData);

        public delegate void StartNewQuest_UserCallBack(StartNewQuest_ReturnType returnData);

        public delegate void StockExchangeTrade_UserCallBack(StockExchangeTrade_ReturnType returnData);

        public delegate void TestAchievements_UserCallBack(TestAchievements_ReturnType returnData);

        public delegate void TouchHouseVisitDate_UserCallBack(TouchHouseVisitDate_ReturnType returnData);

        public delegate void TutorialCommand_UserCallBack(TutorialCommand_ReturnType returnData);

        public delegate void UpdateCurrentCards_UserCallBack(UpdateCurrentCards_ReturnType returnData);

        public delegate void UpdateDiplomacyStatus_UserCallBack(UpdateDiplomacyStatus_ReturnType returnData);

        public delegate void UpdateReportFilters_UserCallBack(UpdateReportFilters_ReturnType returnData);

        public delegate void UpdateSelectedTitheType_UserCallBack(UpdateSelectedTitheType_ReturnType returnData);

        public delegate void UpdateUserOptions_UserCallBack(UpdateUserOptions_ReturnType returnData);

        public delegate void UpdateVillageFavourites_UserCallBack(UpdateVillageFavourites_ReturnType returnData);

        public delegate void UpdateVillageResourcesInfo_UserCallBack(UpdateVillageResourcesInfo_ReturnType returnData);

        public delegate void UpgradeRank_UserCallBack(UpgradeRank_ReturnType returnData);

        public delegate void UploadAvatar_UserCallBack(UploadAvatar_ReturnType returnData);

        public delegate void UserInfo_UserCallBack(UserInfo_ReturnType returnData);

        public delegate void VassalInfo_UserCallBack(VassalInfo_ReturnType returnData);

        public delegate void VassalSendResources_UserCallBack(VassalSendResources_ReturnType returnData);

        public delegate void ViewBattle_UserCallBack(ViewBattle_ReturnType returnData);

        public delegate void ViewCastle_UserCallBack(ViewCastle_ReturnType returnData);

        public delegate void VillageBuildingChangeRates_UserCallBack(VillageBuildingChangeRates_ReturnType returnData);

        public delegate void VillageBuildingCompleteDataRetrieval_UserCallBack(VillageBuildingCompleteDataRetrieval_ReturnType returnData);

        public delegate void VillageBuildingSetActive_UserCallBack(VillageBuildingSetActive_ReturnType returnData);

        public delegate void VillageHoldBanquet_UserCallBack(VillageHoldBanquet_ReturnType returnData);

        public delegate void VillageProduceWeapons_UserCallBack(VillageProduceWeapons_ReturnType returnData);

        public delegate void VillageRename_UserCallBack(VillageRename_ReturnType returnData);

        public delegate void VoteInElection_UserCallBack(VoteInElection_ReturnType returnData);

        public delegate void WorldInfo_UserCallBack(WorldInfo_ReturnType returnData);
    }
}

