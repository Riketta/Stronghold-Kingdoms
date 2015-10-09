namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using StatTracking;
    using Stronghold.ShieldClient;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    public class WorldMap
    {
        public int a_endAt;
        public int a_perFrame = 100;
        public int a_startAt;
        public int activeShieldsWorldID = -1;
        public int aiWorldGloryWinLevel = 0x3e8;
        public List<IslandInfoList> aiWorldInvasionLineList;
        public List<int> aiWorldSpecialVillages = new List<int>();
        public bool aiWorldTreeBuilding;
        public static Color[] areaColorList = new Color[] { 
            Color.FromArgb(0xff, 0xa9, 0xca, 0x95), Color.FromArgb(0xff, 0xec, 20, 20), Color.FromArgb(0xff, 0xec, 0x80, 20), Color.FromArgb(0xff, 240, 240, 0x19), Color.FromArgb(0xff, 0x16, 0xec, 0x16), Color.FromArgb(0xff, 4, 200, 200), Color.FromArgb(0xff, 2, 2, 0xbc), Color.FromArgb(0xff, 0x9b, 0x44, 0xc4), Color.FromArgb(0xff, 0xf1, 0x7b, 0xb1), Color.FromArgb(0xff, 0xe7, 0xe7, 0xe7), Color.FromArgb(0xff, 0x29, 0x29, 0x30), Color.FromArgb(0xff, 0x7f, 0x7f, 0x7f), Color.FromArgb(0xff, 0x6d, 0x67, 3), Color.FromArgb(0xff, 2, 0x5e, 0x2e), Color.FromArgb(0xff, 3, 0x5c, 0x7d), Color.FromArgb(0xff, 0x3a, 0xa2, 230), 
            Color.FromArgb(0xff, 0x9d, 0x2a, 0x29), Color.FromArgb(0xff, 0x86, 0x59, 0x2e), Color.FromArgb(0xff, 0xc2, 0xc2, 0x52), Color.FromArgb(0xff, 0x54, 3, 0xcf), Color.FromArgb(0xff, 210, 0xaf, 0xe2)
         };
        public SparseArray armyArray = new SparseArray();
        public SparseArray armyIconsFilter = new SparseArray();
        public SparseArray attackingArmies = new SparseArray();
        public bool bLinelessMap;
        public int cached_retrieveUserID = -1;
        public int cached_retrieveVillageID = -1;
        public DateTime cached_retrieveVillageUserInfoDate = DateTime.MinValue;
        public SparseArray cachedFactionMemberData = new SparseArray();
        public SparseArray cachedUserInfo = new SparseArray();
        public List<CounterSpyInfo> counterSpyInfo = new List<CounterSpyInfo>();
        public WorldPointList[] countryList;
        public WorldPointList[] countyList;
        public int currentMapType;
        public int[] deCountryColour;
        public int[] deCountyColour;
        public int[] deProvinceColour;
        public bool dirtyStanding;
        public bool doSelectTutorialArmy;
        public bool downloadComplete;
        public int downloadingCounter;
        public bool drawDebugNames;
        public bool drawDebugVillageNames;
        public bool drawFakeProvinceBorders;
        public static LeaderBoardEntryData dummyEntry = null;
        public List<InterVillageLine> dynamicVillageLines = new List<InterVillageLine>();
        public int[] esCountryColour;
        public int[] esCountyColour;
        public int[] esProvinceColour;
        public int[] euCountryColour;
        public int[] euCountyColour;
        public bool EUMap;
        public int[] euProvinceColour;
        public int[] experimentalColourRemapping = new int[] { 0, 3, 1, 2, 4 };
        public bool FacebookFreePack;
        public int FakeCardPoints;
        public bool fifthAgeWorld;
        public bool fourthAgeWorld;
        public int[] frCountryColour;
        public int[] frCountyColour;
        public FreeCardsData freeCardInfo = new FreeCardsData();
        public SpriteWrapper freeCardsSprite = new SpriteWrapper();
        public SpriteWrapper freeCardsSprite2 = new SpriteWrapper();
        public int[] frProvinceColour;
        public static bool fullTickFullMode = true;
        public bool GeographicalMap = true;
        public GraphicsMgr gfx;
        public bool haveInitMapTiles;
        public long highestArmySeen = -1L;
        public long highestDownloadedArmy = -1L;
        public FactionData inactiveFaction = new FactionData();
        public bool inBuyPoint;
        public bool inDoResearch;
        public bool inDownloading;
        public bool inLeaderboardSearch;
        public List<InterVillageLine> interVillageLines = new List<InterVillageLine>();
        public bool inTestAchievements;
        public bool inTutorialAdvance;
        public bool inUpdateLastAttackerInfo;
        public List<AIWorldInvasionData> invasionInfo;
        public SparseArray invasionMarkerState = new SparseArray();
        public bool inviteSystemNotImplemented = true;
        public bool isBigpointAccount;
        public IslandInfoList[] islandList;
        public int[] itCountryColour;
        public int[] itCountyColour;
        public int[] itProvinceColour;
        public int lastActualSpecialRequestSent = -1;
        public DateTime lastActualSpecialRequestTime = DateTime.MinValue;
        public string lastAttacker = "";
        public DateTime lastAttackerLastUpdate = DateTime.MinValue;
        public DateTime lastBuyPointClick = DateTime.MinValue;
        public CardTypes.CardDefinition lastCardCatalogSearchCriteria;
        public string lastCardCatalogSortOrder = string.Empty;
        public Point lastClickedLocation;
        public int lastCountedCategory = -1;
        public int lastCountedCategoryValue;
        public DateTime lastDoResearchClick = DateTime.MinValue;
        public bool lastForceExtended;
        public DateTime lastHouseGloryPointsUpdate = DateTime.MinValue;
        public DateTime lastInvasionInfoTime = DateTime.MinValue;
        public DateTime lastPersonTime = DateTime.Now.AddYears(-5);
        public int lastPigsValue;
        public int lastRatsValue;
        public int lastRetieveUserID = -1;
        public DateTime lastRetieveUserTime = DateTime.MinValue;
        public int lastRetieveVillageID = -1;
        public DateTime lastRetieveVillageTime = DateTime.MinValue;
        public int lastSnakesValue;
        public int lastSpecialRequestSent = -1;
        public DateTime lastTestAchievements = DateTime.MinValue;
        public DateTime lastTimeOwnMembersUpdated = DateTime.MinValue;
        public DateTime lastTraderTime = DateTime.Now.AddYears(-5);
        public DateTime LastUpdatedCrowns = DateTime.Now.AddHours(-1.0);
        public DateTime lastUpdateInvasionInfoTime = DateTime.MinValue;
        public string lastUserCardNameFilter = string.Empty;
        public CardTypes.CardDefinition lastUserCardSearchCriteria;
        public string lastUserCardSortOrder = string.Empty;
        public int lastWolfsValue;
        public DateTime lastZeroDownload_leaderboard_Factions;
        public DateTime lastZeroDownload_leaderboard_Houses;
        public DateTime lastZeroDownload_leaderboard_Main;
        public DateTime lastZeroDownload_leaderboard_MainRank;
        public DateTime lastZeroDownload_leaderboard_MainVillages;
        public DateTime lastZeroDownload_leaderboard_ParishFlags;
        public DateTime lastZeroDownload_leaderboard_Sub_Achiever;
        public DateTime lastZeroDownload_leaderboard_Sub_AIKiller;
        public DateTime lastZeroDownload_leaderboard_Sub_Banditkiller;
        public DateTime lastZeroDownload_leaderboard_Sub_banquetter;
        public DateTime lastZeroDownload_leaderboard_Sub_Brewer;
        public DateTime lastZeroDownload_leaderboard_Sub_Capture;
        public DateTime lastZeroDownload_leaderboard_Sub_Defender;
        public DateTime lastZeroDownload_leaderboard_Sub_Donater;
        public DateTime lastZeroDownload_leaderboard_Sub_Farmer;
        public DateTime lastZeroDownload_leaderboard_Sub_Forager;
        public DateTime lastZeroDownload_leaderboard_Sub_Glory;
        public DateTime lastZeroDownload_leaderboard_Sub_Pillager;
        public DateTime lastZeroDownload_leaderboard_Sub_Ransack;
        public DateTime lastZeroDownload_leaderboard_Sub_Raze;
        public DateTime lastZeroDownload_leaderboard_Sub_Stockpiler;
        public DateTime lastZeroDownload_leaderboard_Sub_Trader;
        public DateTime lastZeroDownload_leaderboard_Sub_Weaponsmith;
        public DateTime lastZeroDownload_leaderboard_Sub_Wolfsbane;
        public SparseArray leaderboard_Factions;
        public SparseArray leaderboard_Houses;
        public SparseArray leaderboard_Main;
        public SparseArray leaderboard_MainRank;
        public SparseArray leaderboard_MainVillages;
        public SparseArray leaderboard_ParishFlags;
        public SparseArray leaderboard_Sub_Achiever;
        public SparseArray leaderboard_Sub_AIKiller;
        public SparseArray leaderboard_Sub_Banditkiller;
        public SparseArray leaderboard_Sub_banquetter;
        public SparseArray leaderboard_Sub_Brewer;
        public SparseArray leaderboard_Sub_Capture;
        public SparseArray leaderboard_Sub_Defender;
        public SparseArray leaderboard_Sub_Donater;
        public SparseArray leaderboard_Sub_Farmer;
        public SparseArray leaderboard_Sub_Forager;
        public SparseArray leaderboard_Sub_Glory;
        public SparseArray leaderboard_Sub_Pillager;
        public SparseArray leaderboard_Sub_Ransack;
        public SparseArray leaderboard_Sub_Raze;
        public SparseArray leaderboard_Sub_Stockpiler;
        public SparseArray leaderboard_Sub_Trader;
        public SparseArray leaderboard_Sub_Weaponsmith;
        public SparseArray leaderboard_Sub_Wolfsbane;
        public DateTime leaderboardLastUpdateTime;
        public List<LeaderBoardSearchResults> leaderboardSearchResults;
        public List<LeaderBoardSelfRankings> leaderboardSelfRankings;
        public LeaderboardSelfRankingsComparer leaderboardSelfRankingsComparer;
        public bool loadingErrored;
        public List<LoginHistoryInfo> loginHistory;
        public Point m_baseMousePos = new Point();
        public VillageQuadNode m_baseNode;
        public double m_baseScreenX;
        public double m_baseScreenY;
        public bool m_cachesFlushed;
        public bool m_dataLoaded;
        public Point m_doubleClickMousePos = new Point();
        public double m_doubleClickTime = DXTimer.GetCurrentMilliseconds();
        public bool m_downloadedDataSafely = true;
        public int[] m_factionAllies;
        public List<FactionInviteData> m_factionApplications;
        public SparseArray m_factionData = new SparseArray();
        public int[] m_factionEnemies;
        public FactionInviteData[] m_factionInvites;
        public int m_factionLeaderVote = -1;
        public FactionMemberData[] m_factionMembers;
        public int m_globalWorldID = -1;
        public int[] m_gloryPoints;
        public GloryRoundData m_gloryRoundData;
        public int[] m_houseAllies;
        public HouseData[] m_houseData;
        public int[] m_houseEnemies;
        public HouseVoteData m_houseVoteData;
        public double m_lastFaithPointsUpdate;
        public double m_lastGoldUpdate;
        public double m_lastHonourUpdate;
        public double m_lastMousePressedTime;
        public DateTime m_lastResearchCompleteRequestTime = DateTime.MinValue;
        public DateTime m_lastResearchCompleteTimeMatch = DateTime.MinValue;
        public DateTime m_lastTreasureCastleAttackTime = DateTime.MinValue;
        public bool m_leftMouseGrabbed;
        public bool m_leftMouseHeldDown;
        public int m_mostAge4Villages;
        public int m_multiStageSoundMode;
        public bool m_namesLoaded;
        public NewQuestsData m_newQuestData;
        public int m_numMadeCaptains;
        public int m_numQuestTickets;
        public SparseArray m_parishChatLog = new SparseArray();
        public SparseArray m_parishWallDonateDetails = new SparseArray();
        public int m_researchLagCount;
        public Point m_rolloverLastMousepos = new Point();
        public long m_rolloverLastTime;
        public int m_rolloverTargetVillage = -1;
        public int m_rolloverTargetVillageNoDelay = -1;
        public int m_rolloverVillageShieldID = -1;
        public double m_screenCentreX;
        public double m_screenCentreY;
        public int m_screenHeight;
        public int m_screenWidth;
        public double m_stagedTargetX;
        public double m_stagedTargetY;
        public double m_stagedTargetZoom;
        public double m_targetZoom;
        public int m_treasure1Tickets;
        public int m_treasure2Tickets;
        public int m_treasure3Tickets;
        public int m_treasure4Tickets;
        public int m_treasure5Tickets;
        public QuestsAndTutorialInfo m_tutorialInfo = new QuestsAndTutorialInfo();
        public CardData m_userCardData;
        public double m_userFaithPointsLevel;
        public double m_userFaithPointsRate;
        public double m_userGoldIncomeRate;
        public double m_userGoldLevel;
        public double m_userHonourIncomeRate;
        public double m_userHonourLevel;
        public int m_userInfoShieldRolloverUserID = -1;
        public int m_userPoints;
        public int m_userRank;
        public int m_userRankSubLevel;
        public List<UserVillageData> m_userRelatedVillages = new List<UserVillageData>();
        public List<UserVillageData> m_userVillages;
        public Thread m_WorkerThread;
        public double m_worldScale = 1.0;
        public DateTime m_worldStartDate = DateTime.Now;
        public double m_worldZoom;
        public double m_zoomCap;
        public double m_zoomChangeThisFrame;
        public double m_zoomDiff;
        public bool m_zooming;
        public int m_zoomStage = -1;
        public double m_zoomXPosDiff;
        public double m_zoomXPosTarget;
        public double m_zoomYPosDiff;
        public double m_zoomYPosTarget;
        public bool mapEditing;
        public short[,] mapTileGrid;
        public List<int> marketTradingVillageList = new List<int>();
        public int max_leaderboard_Factions;
        public int max_leaderboard_Houses;
        public int max_leaderboard_Main;
        public int max_leaderboard_MainRank;
        public int max_leaderboard_MainVillages;
        public int max_leaderboard_ParishFlags;
        public int max_leaderboard_Sub_Achiever;
        public int max_leaderboard_Sub_AIKiller;
        public int max_leaderboard_Sub_Banditkiller;
        public int max_leaderboard_Sub_banquetter;
        public int max_leaderboard_Sub_Brewer;
        public int max_leaderboard_Sub_Capture;
        public int max_leaderboard_Sub_Defender;
        public int max_leaderboard_Sub_Donater;
        public int max_leaderboard_Sub_Farmer;
        public int max_leaderboard_Sub_Forager;
        public int max_leaderboard_Sub_Glory;
        public int max_leaderboard_Sub_Pillager;
        public int max_leaderboard_Sub_Ransack;
        public int max_leaderboard_Sub_Raze;
        public int max_leaderboard_Sub_Stockpiler;
        public int max_leaderboard_Sub_Trader;
        public int max_leaderboard_Sub_Weaponsmith;
        public int max_leaderboard_Sub_Wolfsbane;
        public List<int> mCatalogCardsSearch;
        public Dictionary<int, CardTypes.CardOffer> mProfileCardOffers;
        public Dictionary<int, CardTypes.CardDefinition> mProfileCards;
        public List<int> mProfileCardsSearch;
        public List<int> mProfileCardsSet;
        public Dictionary<int, CardTypes.PremiumToken> mProfilePremiumTokens;
        public Dictionary<int, CardTypes.UserCardPack> mProfileUserCardPacks;
        public List<int> mShoppingCartCards;
        public bool NewCategoriesAvailable_Capacity;
        public bool NewCategoriesAvailable_Catapults;
        public bool NewCategoriesAvailable_FullHeight;
        public bool NewCategoriesAvailable_Parish;
        public bool NewCategoriesAvailable_Salt;
        public bool NewCategoriesAvailable_Silk;
        public bool NewCategoriesAvailable_Spice;
        public bool NewCategoriesAvailable_Strategy;
        public bool newPlayer;
        public bool newTutorialAvailable;
        public DateTime nextPigsCalc = DateTime.MinValue;
        public DateTime nextRatsCalc = DateTime.MinValue;
        public DateTime nextSnakesCalc = DateTime.MinValue;
        public DateTime nextWolfsCalc = DateTime.MinValue;
        public const int NUM_VILLAGE_SPRITEWRAPPERS = 1;
        public const int NUMLEVELS = 5;
        public int numVacationsAvailable = 2;
        public bool overIcon;
        public SpriteWrapper overlaySprite = new SpriteWrapper();
        public bool overrideLinelessMap;
        public bool overTicketsIcon;
        public bool overWikiHelp;
        public bool overWolf;
        public ParishChatComparer parishChatComparer = new ParishChatComparer();
        public SparseArray personArray = new SparseArray();
        public Shield pigShield = new Shield(constants.PIG_SHIELD);
        public int playbackBasedDay;
        public int[,] playbackCountriesData;
        public int playbackDay;
        public List<WorldHouseHistoryItem> playbackItems;
        public int playbackMaxCountries;
        public int playbackMaxProvinces;
        public int[,] playbackProvincesData;
        public DateTime playbackStartTime;
        public int playbackTotalDays;
        public Shield playerShield;
        public ShieldFactory.AsyncDelegate playerShieldCallback;
        public ShieldFactory playerShieldFactory;
        public bool playingCountries;
        public bool playingProvinces;
        public int[] plCountryColour;
        public int[] plCountyColour;
        public int[] plProvinceColour;
        public WorldPoint[] pointList;
        public bool PolitcalMap = true;
        public bool PolitcalMapView = true;
        public int ProfileCardpoints;
        public int ProfileCrowns;
        public int ProfileNumFriends;
        public int ProfilePremiumCards;
        public WorldPointList[] provincesList;
        public int pulse;
        public int pulseValue;
        public SparseArray QuestObjectivesSent = new SparseArray();
        public Shield ratShield = new Shield(constants.RAT_SHIELD);
        public const int READ_AROUND_RANGE = 50;
        public const double REGION_BORDER_DRAW_ZOOM_LEVEL = 5.0;
        public const double REGION_DRAW_ZOOM_LEVEL = 5.0;
        public WorldPointList[] regionList;
        public SparseArray reinforcementArray = new SparseArray();
        public List<long> rememberedExistingArmies = new List<long>();
        public List<ArmyRetrieveData> requestedReturnedArmyIDs = new List<ArmyRetrieveData>();
        public bool requestSent;
        public bool retrievingUserVillages;
        public int[] ruCountryColour;
        public int[] ruCountyColour;
        public int[] ruProvinceColour;
        public int[] saCountryColour;
        public int[] saCountyColour;
        public int[] saProvinceColour;
        public const int SAVEDATA_VERSION_ID = 10;
        public SparseArray scoutsForaging = new SparseArray();
        public SparseArray scoutsVillageForaging = new SparseArray();
        public static Color SEACOLOR = Color.FromArgb(140, 0xb6, 0xce);
        public WorldPointList[] seaList;
        public bool secondAgeWorld;
        public const int SHIELD_CACHE_SIZE = 0x7d;
        public bool smallMapFont;
        public Shield snakeShield = new Shield(constants.SNAKE_SHIELD);
        public SparseArray specialVillageCache = new SparseArray();
        public long storedCountryFactionsPos = -1L;
        public long storedCountryFlagsPos = -1L;
        public long storedCountyFactionsPos = -1L;
        public long storedCountyFlagsPos = -1L;
        public long storedFactionChangesPos = -1L;
        public long storedParishFlagsPos = -1L;
        public long storedProvinceFactionsPos = -1L;
        public long storedProvinceFlagsPos = -1L;
        public long storedRegionFactionsPos = -1L;
        public long storedVillageFactionsPos = -1L;
        public long storedVillageNamePos = -1L;
        public List<MapText> textDrawList = new List<MapText>();
        public bool thirdAgeWorld;
        public int threadDelaySize = 400;
        public SpriteWrapper ticketsSprite = new SpriteWrapper();
        public SpriteWrapper ticketsSprite2 = new SpriteWrapper();
        public int TILEMAP_HEIGHT = 0x324;
        public int TILEMAP_WIDTH = 0x2b3;
        public SparseArray traderArray = new SparseArray();
        public List<int> tradingVillageList = new List<int>();
        public int[] trCountryColour;
        public int[] trCountyColour;
        public static int TreasureCastle_AttackGap = 0x15180;
        public byte[,] tree1Grid;
        public byte[,] tree2Grid;
        public bool TributeLines;
        public int[] trProvinceColour;
        public long tutorialArmyID = -1L;
        public SpriteWrapper tutorialOverlaySprite = new SpriteWrapper();
        public List<int> tutorialQuestsObjectivesComplete = new List<int>();
        public int[] uk2CountryColour;
        public int[] uk2CountyColour;
        public int[] uk2ProvinceColour;
        public int[] ukCountryColour;
        public int[] ukCountyColour = new int[] { 
            0, 1, 2, 1, 2, 1, 0, 1, 0, 0, 2, 0, 3, 1, 3, 2, 
            1, 0, 2, 0, 1, 0, 2, 3, 1, 3, 0, 1, 0, 0, 1, 3, 
            1, 0, 0, 0, 1, 2, 3, 2, 1, 0, 3, 0, 3, 2, 1, 3, 
            0, 1, 0, 0, 0, 2, 1, 3, 0, 1, 3, 2, 0, 3, 1, 0, 
            1, 0, 3, 0, 3, 1, 0, 1, 1, 2, 0, 1, 0, 0, 0, 0, 
            0, 0, 2, 1, 0, 2, 0, 1, 2, 3, 1, 2, 0, 3, 2, 3, 
            3, 1, 0, 3, 0, 2, 0, 1, 2, 0, 1, 0, 1, 3, 2, 3, 
            2, 0
         };
        public int[] ukProvinceColour = new int[] { 
            0, 1, 2, 0, 1, 3, 1, 1, 2, 0, 2, 0, 2, 0, 1, 2, 
            2, 0, 1, 0, 1, 2, 0
         };
        public SpriteWrapper updateClockSprite = new SpriteWrapper();
        public int[] usCountryColour;
        public int[] usCountyColour;
        public bool UserCardDataChanged;
        public List<UserRelationship> userRelations = new List<UserRelationship>();
        public ResearchData userResearchData;
        public int[] usProvinceColour;
        public bool vacationNot30Days;
        public static Color[] villageColorList = new Color[] { 
            Color.FromArgb(0xff, 0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xec, 20, 20), Color.FromArgb(0xff, 0xec, 0x80, 20), Color.FromArgb(0xff, 240, 240, 0x19), Color.FromArgb(0xff, 0x16, 0xec, 0x16), Color.FromArgb(0xff, 4, 200, 200), Color.FromArgb(0xff, 2, 2, 0xbc), Color.FromArgb(0xff, 0x9b, 0x44, 0xc4), Color.FromArgb(0xff, 0xf1, 0x7b, 0xb1), Color.FromArgb(0xff, 0xe7, 0xe7, 0xe7), Color.FromArgb(0xff, 0x29, 0x29, 0x30), Color.FromArgb(0xff, 0x7f, 0x7f, 0x7f), Color.FromArgb(0xff, 0x6d, 0x67, 3), Color.FromArgb(0xff, 2, 0x5e, 0x2e), Color.FromArgb(0xff, 3, 0x5c, 0x7d), Color.FromArgb(0xff, 0x3a, 0xa2, 230), 
            Color.FromArgb(0xff, 0x9d, 0x2a, 0x29), Color.FromArgb(0xff, 0x86, 0x59, 0x2e), Color.FromArgb(0xff, 0xc2, 0xc2, 0x52), Color.FromArgb(0xff, 0x54, 3, 0xcf), Color.FromArgb(0xff, 210, 0xaf, 0xe2), Color.FromArgb(0xff, 0, 0, 0)
         };
        public VillageData[] villageList;
        public int villageMapHeight;
        public int villageMapWidth;
        public VillageNameComparer villageNameComparer = new VillageNameComparer();
        public SparseArray villagesInvolvedInAIAttacks = new SparseArray();
        public SparseArray villagesInvolvedInAttacks = new SparseArray();
        public SpriteWrapper villageSprite;
        public SpriteWrapper wikiHelpSprite = new SpriteWrapper();
        public Shield wolfShield = new Shield(constants.WOLF_SHIELD);
        public DateTime wolfsRevengeEnd = DateTime.MinValue;
        public SpriteWrapper wolfsRevengeSprite = new SpriteWrapper();
        public SpriteWrapper wolfsRevengeSprite2 = new SpriteWrapper();
        public DateTime wolfsRevengeStart = DateTime.MinValue;
        public const float WORLD_SCALE_MAX = 24f;
        public const float WORLD_SCALE_MAX_CLOSE = 23.9f;
        public const float WORLD_SCALE_OLD_MAX = 17f;
        public WorldMapFilter worldMapFilter = new WorldMapFilter();
        public int worldMapHeight;
        public int worldMapWidth;
        public List<ShieldTextureCacheEntry> worldShieldCache = new List<ShieldTextureCacheEntry>();
        public SparseArray worldShieldCachePlayerIDs = new SparseArray();
        public SparseArray worldShields = new SparseArray();
        public bool worldShieldsAvailable;
        public SpriteWrapper worldTileSprite = new SpriteWrapper();
        public SpriteWrapper worldTreeSprite = new SpriteWrapper();
        public WorldZoomCallback worldZoomCallback;
        public bool xmasPresents;
        public int yMarkerOffset;
        public const int ZOOM_CENTRE_VAL = 0x17;
        public const int ZOOM_MAX_VAL = 0x1b;
        public const int ZOOM_MAX_VAL_CHANGE = 7;
        public const double ZOOM_TIME = 16.0;
        public int zoomCurrent = 0xfa0;
        public int zoomMax = 0x6978;
        public int zoomMin;

        public WorldMap()
        {
            int[] numArray = new int[4];
            numArray[1] = 1;
            numArray[2] = 1;
            this.ukCountryColour = numArray;
            this.deCountyColour = new int[] { 
                3, 1, 2, 2, 0, 1, 2, 3, 1, 2, 0, 3, 0, 2, 1, 0, 
                3, 0, 2, 3, 0, 1, 3, 2, 1, 0, 1, 2, 0, 1, 2, 2, 
                3, 0, 3, 2, 1, 1, 2, 0, 0, 3, 1, 3, 2, 1, 2, 3, 
                0, 3, 0, 3, 1, 2, 0, 1, 2, 0, 3, 0, 2, 1, 1, 0, 
                3
             };
            this.deProvinceColour = new int[] { 0, 1, 2, 2, 0, 1, 1, 3, 2, 0, 3, 1, 0, 1, 0 };
            this.deCountryColour = new int[] { 0, 1, 2, 1 };
            this.frCountyColour = new int[] { 
                2, 1, 0, 1, 3, 0, 1, 0, 2, 3, 2, 3, 3, 2, 0, 1, 
                0, 3, 2, 0, 1, 1, 0, 3, 0, 2, 0, 3, 1, 2, 2, 0, 
                3, 0, 3, 0, 3, 2, 0, 3, 1, 0, 0, 2, 1, 2, 3, 1, 
                2, 3, 1, 0, 0, 2, 3, 0, 2, 0, 1, 3, 0, 1, 0, 2, 
                3, 1, 0, 3, 1, 1, 2, 1, 3, 2, 0, 1, 3, 1, 0, 2, 
                0, 2, 1, 0, 2, 2, 3, 1, 2, 3, 1, 1, 0, 1, 0, 2, 
                3, 0
             };
            this.frProvinceColour = new int[] { 
                0, 1, 1, 2, 0, 1, 1, 0, 1, 0, 2, 3, 1, 2, 0, 3, 
                1, 2
             };
            this.frCountryColour = new int[] { 0, 1, 2, 1, 0, 2, 0 };
            this.ruCountyColour = new int[] { 
                0, 1, 0, 2, 1, 2, 0, 1, 3, 0, 2, 3, 0, 1, 1, 3, 
                0, 3, 0, 1, 2, 0, 1, 3, 0, 2, 3, 0, 1, 3, 2, 0, 
                3, 1, 1, 3, 2, 3, 0, 1, 0, 2, 2, 1, 2, 0, 3, 2, 
                0, 2, 1, 0, 2, 0, 1, 3, 0, 2, 0, 1, 3, 1, 0, 3, 
                1, 0, 2, 1, 2, 2, 2, 0, 2, 3, 1, 3, 0, 2, 1, 0, 
                2, 3, 1, 3, 0, 1, 2, 3, 0, 3, 2, 3, 1, 2, 1, 3, 
                0, 2, 1, 0, 2, 0, 1, 2
             };
            this.ruProvinceColour = new int[] { 
                0, 1, 1, 0, 2, 3, 2, 0, 3, 1, 2, 0, 1, 0, 1, 2, 
                2, 1, 2, 0
             };
            this.ruCountryColour = new int[] { 0, 1, 2, 1, 1, 1, 0 };
            this.esCountyColour = new int[] { 
                0, 1, 2, 0, 0, 2, 1, 0, 1, 2, 0, 3, 2, 0, 2, 2, 
                0, 1, 3, 0, 2, 0, 1, 1, 2, 0, 2, 0, 3, 2, 3, 0, 
                2, 3, 1, 0, 1, 1, 3, 2, 1, 0, 2, 0, 3, 1, 0, 0, 
                0, 0, 0, 1, 2, 3, 0, 3, 1, 0, 3, 1, 0, 1, 2, 3, 
                1, 3, 0, 2
             };
            this.esProvinceColour = new int[] { 
                0, 1, 2, 0, 1, 0, 2, 0, 3, 1, 0, 3, 2, 0, 2, 1, 
                3
             };
            int[] numArray2 = new int[2];
            numArray2[1] = 1;
            this.esCountryColour = numArray2;
            this.euCountyColour = new int[] { 
                2, 3, 0, 1, 1, 0, 3, 0, 3, 1, 0, 2, 1, 0, 2, 3, 
                1, 0, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 1, 3, 3, 2, 
                1, 3, 0, 1, 3, 1, 0, 0, 2, 3, 1, 2, 3, 1, 2, 0, 
                1, 3, 2, 0, 2, 0, 1, 0, 2, 1, 2, 3, 1, 0, 2, 1, 
                3, 1, 2, 3, 0, 2, 1, 0, 2, 0, 0, 1, 3, 2, 3, 1, 
                3, 2, 0, 1, 0, 1, 1, 2, 2, 1, 0, 2, 3, 0, 1, 3, 
                2, 1, 0, 3, 1, 1, 2, 0, 0, 3, 0, 1, 1, 2, 0, 3, 
                2, 3, 1, 2, 3, 0, 3, 2, 1, 1, 2, 1, 0, 3, 2, 0, 
                0, 3, 1, 0, 3, 1, 2, 1, 0, 2, 0, 3, 1, 1, 2, 0, 
                3, 3, 0, 1, 3, 2, 0, 0, 1, 2, 3, 0, 1, 2, 0, 3, 
                2, 0, 3, 3, 2, 2, 0, 1, 2, 0, 3, 1, 3, 0, 2, 1, 
                2, 1, 2, 3, 2, 0, 1, 3, 2, 3, 0, 1, 0, 1, 1, 2, 
                2, 2, 3, 0, 2
             };
            this.euProvinceColour = new int[] { 
                0, 1, 0, 1, 0, 1, 2, 0, 0, 1, 2, 0, 3, 1, 0, 2, 
                0, 1, 0, 2, 1, 0, 2, 3, 0, 1, 2, 3, 0, 2, 0, 0, 
                0, 1, 2, 1, 0, 0, 2, 1, 3, 1, 2, 0, 0, 3, 1, 1, 
                2, 3, 1, 2, 0, 2, 1, 0, 0, 3, 0, 2, 0, 3, 2, 1, 
                0, 2, 3, 2, 1, 0, 1, 1, 2, 0, 2, 2, 0, 0, 1, 1, 
                0, 2, 1, 2, 1, 0
             };
            this.euCountryColour = new int[] { 
                0, 0, 0, 0, 1, 0, 1, 0, 2, 0, 1, 2, 0, 1, 2, 3, 
                1, 0, 1, 2, 0, 1, 3, 0, 3, 0, 2, 1, 0, 3, 2, 1, 
                1
             };
            this.itCountyColour = new int[] { 
                0, 0, 3, 1, 0, 1, 2, 0, 3, 0, 3, 1, 1, 0, 2, 3, 
                2, 2, 2, 3, 0, 1, 0, 3, 2, 1, 1, 3, 0, 0, 2, 3, 
                1, 1, 0, 0, 3, 2, 1, 0, 1, 3, 3, 2, 0, 2, 2, 3, 
                2, 0, 1, 1, 1, 0, 1, 2, 0, 3, 2, 0, 0, 1, 1, 2, 
                0, 1, 2, 0, 3, 2, 1, 2, 0, 1, 3, 1, 2, 0, 1, 2, 
                1, 0, 1, 2, 0, 0, 2, 1, 0, 2, 1, 0, 2, 1, 0, 0, 
                2, 0, 1, 1, 0, 0, 0, 2, 1, 0, 2, 1, 0, 2, 0, 2, 
                1, 0, 3, 2, 0, 2, 1, 0, 1, 3, 1, 2, 0, 2, 1, 2, 
                0, 2, 1, 2, 1, 0, 0, 1, 0, 3, 0, 1, 3, 2, 2, 0, 
                2, 1, 3, 1, 0, 3, 1, 1, 2, 2, 0, 3, 0, 2, 1, 0, 
                1, 2, 0, 3, 1, 0, 2, 1, 2, 1, 2, 0, 1, 0, 3, 0, 
                1, 2, 3, 0, 3, 2, 3, 0, 1, 3, 2, 0, 3, 0, 2, 1, 
                3, 0, 2, 0, 1, 2, 1, 0, 2
             };
            this.itProvinceColour = new int[] { 
                0, 1, 0, 2, 0, 3, 1, 0, 2, 3, 1, 0, 1, 0, 2, 1, 
                0, 0, 1, 0, 1, 2, 0, 2, 0, 1, 3, 0, 1, 0, 2, 1, 
                0, 2, 1, 2, 0, 0
             };
            this.itCountryColour = new int[] { 0, 1, 0, 1, 2, 3, 2, 0 };
            this.plCountyColour = new int[] { 
                0, 1, 2, 1, 2, 0, 3, 0, 1, 3, 0, 2, 1, 0, 3, 0, 
                1, 2, 1, 0, 2, 2, 0, 1, 2, 0, 2, 1, 0, 0, 3, 2, 
                1, 3, 2, 2, 0, 1, 0, 3, 3, 1, 2, 0, 1, 3, 2, 1, 
                2, 1, 0, 3
             };
            this.plProvinceColour = new int[] { 0, 1, 2, 1, 0, 1, 3, 0, 3, 0, 1, 2, 0, 3, 0, 3 };
            this.plCountryColour = new int[1];
            this.saCountyColour = new int[] { 
                0, 1, 1, 2, 2, 0, 0, 3, 1, 1, 2, 0, 1, 0, 2, 0, 
                1, 1, 0, 2, 0, 1, 3, 0, 1, 2, 2, 3, 0, 1, 2, 3, 
                0, 0, 1, 2, 2, 0, 1, 0, 2, 3, 1, 0, 2, 1, 2, 3, 
                0, 2, 2, 3, 0, 2, 1, 0, 1, 0, 3, 0, 0, 2, 1, 3, 
                0, 1, 2, 1, 0, 2, 3, 3, 0, 3, 0, 1, 3, 0, 3, 2, 
                1, 2, 0, 1, 3, 2, 0, 1, 0, 2, 2, 0, 1, 3, 0, 1, 
                2, 0, 1, 2, 3, 1, 0, 0, 3, 2, 1, 2, 1, 0, 0, 0, 
                2, 0, 2, 0, 2, 0, 2, 1, 0, 0, 0, 0, 1, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
             };
            this.saProvinceColour = new int[] { 
                0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 2, 0, 1, 3, 0, 2, 
                0, 3, 2, 0, 1
             };
            int[] numArray4 = new int[5];
            numArray4[1] = 1;
            numArray4[3] = 2;
            this.saCountryColour = numArray4;
            this.trCountyColour = new int[] { 
                2, 3, 1, 2, 1, 3, 2, 3, 0, 3, 1, 2, 1, 2, 0, 3, 
                0, 2, 1, 0, 3, 2, 3, 0, 1, 1, 3, 2, 3, 0, 1, 0, 
                3, 0, 1, 1, 2, 3, 0, 0, 1, 3, 0, 1, 2, 3, 1, 0, 
                2, 1, 3, 1, 0, 2, 1, 3, 0, 2, 0, 2, 3, 1, 2, 0, 
                1, 0, 2, 3, 2, 0, 1, 3, 0, 2, 1, 3, 2, 0, 3, 0, 
                0
             };
            this.trProvinceColour = new int[] { 0, 1, 0, 2, 1, 3, 1 };
            this.trCountryColour = new int[1];
            this.uk2CountyColour = new int[] { 
                0, 1, 2, 1, 2, 1, 0, 1, 0, 0, 2, 0, 3, 1, 3, 2, 
                1, 0, 2, 0, 1, 0, 2, 3, 1, 3, 0, 1, 0, 0, 1, 3, 
                1, 0, 0, 0, 1, 2, 3, 2, 1, 0, 3, 0, 3, 2, 1, 3, 
                0, 1, 0, 0, 0, 2, 1, 3, 0, 1, 3, 2, 0, 3, 1, 0, 
                1, 0, 3, 0, 3, 1, 0, 1, 1, 2, 0, 1, 0, 0, 0, 0, 
                0, 0, 2, 1, 0, 2, 0, 1, 2, 3, 1, 2, 0, 3, 2, 3, 
                3, 1, 0, 3, 0, 2, 0, 1, 2, 0, 1, 0, 1, 3, 2, 3, 
                2, 0
             };
            this.uk2ProvinceColour = new int[] { 
                0, 0, 2, 1, 0, 1, 2, 0, 1, 0, 1, 0, 1, 0, 1, 2, 
                2, 0, 1, 0, 2, 0, 2, 1, 1, 1, 1, 0
             };
            this.uk2CountryColour = new int[] { 0, 1, 0, 0, 0, 1, 2, 1, 0, 1, 2, 0, 0, 2 };
            this.usCountyColour = new int[] { 
                0, 1, 2, 1, 0, 1, 0, 0, 2, 2, 0, 1, 0, 2, 0, 1, 
                2, 3, 0, 0, 1, 2, 1, 3, 0, 3, 0, 1, 0, 1, 0, 2, 
                1, 1, 2, 0, 1, 0, 0, 1, 2, 0, 3, 0, 2, 3, 2, 3, 
                0, 1, 2, 1, 2, 0, 1, 2, 3, 2, 1, 2, 0, 1, 3, 2, 
                0, 2, 3, 0, 1, 2, 1, 0, 2, 2, 1, 3, 1, 2, 0, 3, 
                0, 1, 0, 1, 2, 3, 0, 1, 1, 3, 0, 3, 1, 0, 2, 0, 
                1, 2, 0, 2, 1
             };
            this.usProvinceColour = new int[] { 
                0, 1, 1, 0, 2, 2, 1, 0, 2, 1, 0, 2, 3, 0, 3, 1, 
                0, 0, 3, 1, 0, 3, 0, 2, 1, 2, 1, 2, 1, 0, 3, 3, 
                1, 0, 1, 2, 3, 2, 0, 3, 0, 1
             };
            this.usCountryColour = new int[] { 0, 1, 2, 0, 0, 1, 2, 1, 0, 1, 2 };
            this.leaderboard_Main = new SparseArray();
            this.leaderboard_MainRank = new SparseArray();
            this.leaderboard_MainVillages = new SparseArray();
            this.leaderboard_Factions = new SparseArray();
            this.leaderboard_Houses = new SparseArray();
            this.leaderboard_ParishFlags = new SparseArray();
            this.leaderboard_Sub_Pillager = new SparseArray();
            this.leaderboard_Sub_Defender = new SparseArray();
            this.leaderboard_Sub_Ransack = new SparseArray();
            this.leaderboard_Sub_Wolfsbane = new SparseArray();
            this.leaderboard_Sub_Banditkiller = new SparseArray();
            this.leaderboard_Sub_AIKiller = new SparseArray();
            this.leaderboard_Sub_Trader = new SparseArray();
            this.leaderboard_Sub_Forager = new SparseArray();
            this.leaderboard_Sub_Stockpiler = new SparseArray();
            this.leaderboard_Sub_Farmer = new SparseArray();
            this.leaderboard_Sub_Brewer = new SparseArray();
            this.leaderboard_Sub_Weaponsmith = new SparseArray();
            this.leaderboard_Sub_banquetter = new SparseArray();
            this.leaderboard_Sub_Achiever = new SparseArray();
            this.leaderboard_Sub_Donater = new SparseArray();
            this.leaderboard_Sub_Capture = new SparseArray();
            this.leaderboard_Sub_Raze = new SparseArray();
            this.leaderboard_Sub_Glory = new SparseArray();
            this.max_leaderboard_Main = -1;
            this.max_leaderboard_MainRank = -1;
            this.max_leaderboard_MainVillages = -1;
            this.max_leaderboard_Factions = -1;
            this.max_leaderboard_Houses = -1;
            this.max_leaderboard_ParishFlags = -1;
            this.max_leaderboard_Sub_Pillager = -1;
            this.max_leaderboard_Sub_Defender = -1;
            this.max_leaderboard_Sub_Ransack = -1;
            this.max_leaderboard_Sub_Wolfsbane = -1;
            this.max_leaderboard_Sub_Banditkiller = -1;
            this.max_leaderboard_Sub_AIKiller = -1;
            this.max_leaderboard_Sub_Trader = -1;
            this.max_leaderboard_Sub_Forager = -1;
            this.max_leaderboard_Sub_Stockpiler = -1;
            this.max_leaderboard_Sub_Farmer = -1;
            this.max_leaderboard_Sub_Brewer = -1;
            this.max_leaderboard_Sub_Weaponsmith = -1;
            this.max_leaderboard_Sub_banquetter = -1;
            this.max_leaderboard_Sub_Achiever = -1;
            this.max_leaderboard_Sub_Donater = -1;
            this.max_leaderboard_Sub_Capture = -1;
            this.max_leaderboard_Sub_Raze = -1;
            this.max_leaderboard_Sub_Glory = -1;
            this.lastZeroDownload_leaderboard_Main = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_MainRank = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_MainVillages = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Factions = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Houses = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_ParishFlags = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Pillager = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Defender = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Ransack = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Wolfsbane = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Banditkiller = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_AIKiller = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Trader = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Forager = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Stockpiler = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Farmer = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Brewer = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Weaponsmith = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_banquetter = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Achiever = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Donater = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Capture = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Raze = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Glory = DateTime.MinValue;
            this.leaderboardLastUpdateTime = DateTime.MinValue;
            this.leaderboardSelfRankings = new List<LeaderBoardSelfRankings>();
            this.leaderboardSearchResults = new List<LeaderBoardSearchResults>();
            this.leaderboardSelfRankingsComparer = new LeaderboardSelfRankingsComparer();
        }

        public void addArmyToArray(ArmyReturnData data, ref SparseArray armyRequestSentFlag, bool singleAdd)
        {
            LocalArmyData data2 = new LocalArmyData {
                armyID = data.armyID,
                homeVillageID = data.homeVillageID,
                travelFromVillageID = data.travelFromVillageID,
                userID = data.userID,
                attackType = data.attackType,
                targetVillageID = data.targetVillageID,
                numPeasants = data.numPeasants,
                numArchers = data.numArchers,
                numPikemen = data.numPikemen,
                numSwordsmen = data.numSwordsmen,
                numCatapults = data.numCatapults,
                numCaptains = data.numCaptains,
                numScouts = data.numScouts,
                captainsCommand = data.captainsCommand,
                carryingFlag = data.carryingFlag,
                lootLevel = data.lootLevel,
                lootType = data.lootType,
                lootData = data.lootData,
                aiPlayer = data.aiPlayer,
                reinforcements = data.reinforcements
            };
            if (singleAdd)
            {
                data2.singlyAdded = true;
            }
            if ((data2.targetVillageID >= 0) && (data2.targetVillageID < this.villageList.Length))
            {
                data2.createJourney(data.startTime, data.curTime, data.endTime);
                data2.targetDisplayX = this.villageList[data.targetVillageID].x;
                data2.targetDisplayY = this.villageList[data.targetVillageID].y;
            }
            else
            {
                data2.serverEndTime = data.curTime;
            }
            if (data2.travelFromVillageID < this.villageList.Length)
            {
                data2.baseDisplayX = this.villageList[data.travelFromVillageID].x;
                data2.baseDisplayY = this.villageList[data.travelFromVillageID].y;
            }
            if ((!data2.reinforcements && (armyRequestSentFlag != null)) && (armyRequestSentFlag[data2.armyID] != null))
            {
                data2.requestSent = (bool) armyRequestSentFlag[data2.armyID];
            }
            data2.updatePosition();
            if (!data.reinforcements)
            {
                this.armyArray[data.armyID] = data2;
            }
            else
            {
                this.reinforcementArray[data.armyID] = data2;
            }
        }

        public void addCircleTriangle(RectangleF screenRect, float x1, float y1, float x2, float y2, PointF centre, Color color)
        {
            x1 = ((x1 - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
            y1 = ((y1 - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
            float num = ((centre.X - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
            float num2 = ((centre.Y - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
            x2 = ((x2 - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
            y2 = ((y2 - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
            this.gfx.addTriangle(color, x1, y1, x2, y2, num, num2);
        }

        public void addCompletedQuestObjectives(int quest)
        {
            this.tutorialQuestsObjectivesComplete.Add(quest);
        }

        public void addDynamicInterVillageLine(Point start, Point end, int style)
        {
            InterVillageLine item = new InterVillageLine {
                start = new PointF((float) start.X, (float) start.Y),
                end = new PointF((float) end.X, (float) end.Y),
                style = style,
                minLength = false
            };
            this.dynamicVillageLines.Add(item);
        }

        public void addDynamicInterVillageLine(PointF start, PointF end, int style)
        {
            this.addDynamicInterVillageLine(start, end, style, 1.1f);
        }

        public void addDynamicInterVillageLine(PointF start, PointF end, int style, float scalar)
        {
            InterVillageLine item = new InterVillageLine {
                start = start,
                end = end,
                style = style,
                widthScalar = scalar
            };
            if (item.widthScalar < 1f)
            {
                item.widthScalar = 1f;
            }
            else if (item.widthScalar > 2f)
            {
                item.widthScalar = 2f;
            }
            this.dynamicVillageLines.Add(item);
        }

        public void addExistingArmy(long armyID)
        {
            this.rememberedExistingArmies.Add(armyID);
        }

        public void addFaithPoints(double amount)
        {
            this.m_userFaithPointsLevel += amount;
        }

        public void addGold(double gold)
        {
            this.m_userGoldLevel += gold;
        }

        public void addHonour(double honour)
        {
            this.m_userHonourLevel += honour;
        }

        public void addInterVillageLine(Point start, Point end, bool yours)
        {
            InterVillageLine item = new InterVillageLine {
                start = new PointF((float) start.X, (float) start.Y),
                end = new PointF((float) end.X, (float) end.Y)
            };
            if (yours)
            {
                item.style = 2;
            }
            this.interVillageLines.Add(item);
        }

        public void addInterVillageLine(Point start, Point end, bool yours, float scale)
        {
            InterVillageLine item = new InterVillageLine {
                start = new PointF((float) start.X, (float) start.Y),
                end = new PointF((float) end.X, (float) end.Y)
            };
            if (yours)
            {
                item.style = 2;
            }
            item.widthScalar = scale;
            if (item.widthScalar < 1f)
            {
                item.widthScalar = 1f;
            }
            else if (item.widthScalar > 2f)
            {
                item.widthScalar = 2f;
            }
            this.interVillageLines.Add(item);
        }

        public List<Chat_TextEntry> addParishChat(int parishID, List<Chat_TextEntry> newText)
        {
            if ((newText == null) || (newText.Count == 0))
            {
                return null;
            }
            if (this.m_parishChatLog[parishID] == null)
            {
                ParishChatData data = new ParishChatData();
                data.init();
                this.m_parishChatLog[parishID] = data;
            }
            ParishChatData data2 = (ParishChatData) this.m_parishChatLog[parishID];
            List<Chat_TextEntry> list = new List<Chat_TextEntry>();
            foreach (Chat_TextEntry entry in newText)
            {
                if (data2.addEntry(entry))
                {
                    list.Add(entry);
                }
            }
            return list;
        }

        public void addPerson(PersonData personData, DateTime curServerTime)
        {
            LocalPerson person = new LocalPerson {
                person = personData,
                personID = personData.personID
            };
            if (personData.state > 0)
            {
                person.createJourney(personData.startTime, curServerTime, personData.endTime);
                if (personData.targetVillageID < this.villageList.Length)
                {
                    person.targetDisplayX = this.villageList[personData.targetVillageID].x;
                    person.targetDisplayY = this.villageList[personData.targetVillageID].y;
                }
                foreach (LocalPerson person2 in this.personArray)
                {
                    person2.childrenCount = 0;
                    if ((((person2.parentPerson == -1L) && (person2.personID != person.personID)) && ((person2.person.state == personData.state) && (person2.person.targetVillageID == personData.targetVillageID))) && (person2.person.homeVillageID == personData.homeVillageID))
                    {
                        TimeSpan span = (TimeSpan) (person2.person.endTime - personData.endTime);
                        if ((span.TotalSeconds < 1.0) && (span.TotalSeconds > -1.0))
                        {
                            person.parentPerson = person2.person.personID;
                        }
                    }
                }
            }
            if (personData.homeVillageID < this.villageList.Length)
            {
                person.baseDisplayX = this.villageList[personData.homeVillageID].x;
                person.baseDisplayY = this.villageList[personData.homeVillageID].y;
            }
            person.updatePosition();
            this.personArray[person.personID] = person;
        }

        public void addProfileCard(int id, string type)
        {
            if (!this.ProfileCards.ContainsKey(id))
            {
                try
                {
                    this.ProfileCards.Add(id, CardTypes.getCardDefinitionFromString(type.Trim()));
                    return;
                }
                catch (Exception)
                {
                    throw new Exception(string.Concat(new object[] { "Tried to add a card and couldn't: UserTradingCardID= ", id, " type= ", type }));
                }
            }
            throw new Exception("Tried to add a card that was already there: UserTradingCardID=" + id);
        }

        public void addReinforcementArmy(ArmyReturnData data)
        {
            SparseArray armyRequestSentFlag = null;
            this.addArmyToArray(data, ref armyRequestSentFlag, false);
        }

        public void addResearchPoints(int amount)
        {
            if (this.userResearchData != null)
            {
                this.userResearchData.research_points += amount;
            }
        }

        public void addText(string text, PointF loc, Color col, bool centered, int size)
        {
            this.addText(text, loc, col, centered, size, false);
        }

        public void addText(string text, PointF loc, Color col, bool centered, int size, bool bordered)
        {
            MapText item = new MapText {
                text = text,
                loc = loc,
                col = col,
                size = size,
                centered = centered,
                bordered = bordered
            };
            this.textDrawList.Add(item);
        }

        public void addTickets(int level, int numberToUse)
        {
            switch (level)
            {
                case -1:
                    this.m_numQuestTickets += numberToUse;
                    return;

                case 0:
                    this.m_treasure1Tickets += numberToUse;
                    return;

                case 1:
                    this.m_treasure2Tickets += numberToUse;
                    return;

                case 2:
                    this.m_treasure3Tickets += numberToUse;
                    return;

                case 3:
                    this.m_treasure4Tickets += numberToUse;
                    return;

                case 4:
                    this.m_treasure5Tickets += numberToUse;
                    return;
            }
        }

        public void addTrader(MarketTraderData marketTrader, DateTime curServerTime)
        {
            LocalTrader trader = new LocalTrader {
                trader = marketTrader,
                traderID = marketTrader.traderID
            };
            if ((marketTrader.targetVillageID >= 0) && (marketTrader.homeVillageID >= 0))
            {
                if (marketTrader.traderState > 0)
                {
                    trader.createJourney(marketTrader.startTime, curServerTime, marketTrader.endTime);
                    trader.targetDisplayX = this.villageList[marketTrader.targetVillageID].x;
                    trader.targetDisplayY = this.villageList[marketTrader.targetVillageID].y;
                    foreach (LocalTrader trader2 in this.traderArray)
                    {
                        if ((((trader2.parentTrader == -1L) && (trader2.traderID != trader.traderID)) && ((trader2.trader.traderState == marketTrader.traderState) && (trader2.trader.targetVillageID == marketTrader.targetVillageID))) && ((trader2.trader.homeVillageID == marketTrader.homeVillageID) && (trader2.trader.resource == marketTrader.resource)))
                        {
                            TimeSpan span = (TimeSpan) (trader2.trader.endTime - marketTrader.endTime);
                            if ((span.TotalSeconds < 1.0) && (span.TotalSeconds > -1.0))
                            {
                                trader.parentTrader = trader2.trader.traderID;
                            }
                        }
                    }
                }
                trader.baseDisplayX = this.villageList[marketTrader.homeVillageID].x;
                trader.baseDisplayY = this.villageList[marketTrader.homeVillageID].y;
                trader.updatePosition();
                this.traderArray[trader.traderID] = trader;
            }
        }

        public void addUserVillage(int villageID)
        {
            bool flag = false;
            int num = 0;
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    if (data.villageID == villageID)
                    {
                        this.villageList[data.villageID].userVillageID = num;
                        flag = true;
                        break;
                    }
                    num++;
                }
                if (!flag)
                {
                    UserVillageData item = new UserVillageData {
                        villageID = villageID
                    };
                    this.m_userVillages.Add(item);
                    this.villageList[item.villageID].userVillageID = this.m_userVillages.Count - 1;
                }
                this.sortUserVillages();
            }
            this.updateUserRelatedVillages();
        }

        public void advanceTutorial()
        {
            if (GameEngine.Instance.World.getTutorialStage() != 0)
            {
                StatTrackingClient.Instance().ActivateTrigger(12, GameEngine.Instance.World.getTutorialStage());
            }
            this.inTutorialAdvance = true;
            RemoteServices.Instance.set_TutorialCommand_UserCallBack(new RemoteServices.TutorialCommand_UserCallBack(this.TutorialCommandCallback));
            RemoteServices.Instance.TutorialCommand(-2);
        }

        public void advanceTutorialOLD()
        {
        }

        public bool allowExchangeTrade(int exchangeVillageID, int buyingVillageID)
        {
            if (((exchangeVillageID < 0) || (buyingVillageID < 0)) || ((exchangeVillageID >= this.villageList.Length) || (buyingVillageID >= this.villageList.Length)))
            {
                return false;
            }
            VillageData data = this.villageList[exchangeVillageID];
            VillageData data2 = this.villageList[buyingVillageID];
            if (!data.Capital)
            {
                return false;
            }
            switch (this.UserResearchData.Research_Commerce)
            {
                case 0:
                    if (data.regionID == data2.regionID)
                    {
                        break;
                    }
                    return false;

                case 1:
                    if (data.countyID == data2.countyID)
                    {
                        break;
                    }
                    return false;

                case 2:
                    if ((data2.countyID >= 0) && (data.countyID >= 0))
                    {
                        int parentID = this.countyList[data.countyID].parentID;
                        int num2 = this.countyList[data2.countyID].parentID;
                        if (parentID == num2)
                        {
                            break;
                        }
                        return false;
                    }
                    return false;

                case 3:
                    if ((data2.countyID >= 0) && (data.countyID >= 0))
                    {
                        int index = this.countyList[data.countyID].parentID;
                        int num4 = this.countyList[data2.countyID].parentID;
                        if ((index < 0) || (num4 < 0))
                        {
                            return false;
                        }
                        int num5 = this.provincesList[index].parentID;
                        int num6 = this.provincesList[num4].parentID;
                        if (num5 != num6)
                        {
                            return false;
                        }
                        break;
                    }
                    return false;
            }
            return true;
        }

        public bool alreadyGotFactionApplication(int factionID)
        {
            if (this.m_factionApplications != null)
            {
                foreach (FactionInviteData data in this.m_factionApplications)
                {
                    if (data.factionID == factionID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool areSelfStandingsDirty()
        {
            bool dirtyStanding = this.dirtyStanding;
            this.dirtyStanding = false;
            return dirtyStanding;
        }

        public void armyAttackCallback(ArmyAttack_ReturnType returnData)
        {
        }

        public void breakVassal(int lordVillage, int vassalVillage)
        {
            this.villageList[vassalVillage].connecter = -1;
        }

        public void buildVillageTree()
        {
            this.m_baseNode = new VillageQuadNode(0, 0, this.villageMapWidth, this.villageMapHeight);
            this.m_baseNode.setWorld(this);
            foreach (VillageData data in this.villageList)
            {
                this.m_baseNode.addVillage(data, 0);
            }
            this.WorldZoom = 4.0;
            this.m_screenCentreX = ((double) this.worldMapWidth) / 2.0;
            this.m_screenCentreY = ((double) this.worldMapHeight) / 2.0;
        }

        public void buyResearchPoint()
        {
            if (this.inBuyPoint)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastBuyPointClick);
                if (span.TotalSeconds < 120.0)
                {
                    return;
                }
            }
            this.inBuyPoint = true;
            this.lastBuyPointClick = DateTime.Now;
            RemoteServices.Instance.set_BuyResearchPoint_UserCallBack(new RemoteServices.BuyResearchPoint_UserCallBack(this.buyResearchPointCallback));
            RemoteServices.Instance.BuyResearchPoint();
        }

        public void buyResearchPointCallback(BuyResearchPoint_ReturnType returnData)
        {
            this.inBuyPoint = false;
            if (returnData.Success || (returnData.m_errorCode == ErrorCodes.ErrorCode.RESEARCH_NOT_ENOUGH_HONOUR))
            {
                this.setResearchData(returnData.researchData);
                this.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                InterfaceMgr.Instance.researchDataChanged(returnData.researchData);
            }
        }

        public void calcAvailableCategories()
        {
            foreach (CardTypes.CardDefinition definition in CardTypes.cardList)
            {
                if (((definition.cardRank > 0) && (definition.cardRarity > 0)) && (definition.available == 1))
                {
                    if (CardTypes.isCardInNewCategory(definition.id, 0x4006))
                    {
                        this.NewCategoriesAvailable_Salt = true;
                    }
                    if (CardTypes.isCardInNewCategory(definition.id, 0x4007))
                    {
                        this.NewCategoriesAvailable_Spice = true;
                    }
                    if (CardTypes.isCardInNewCategory(definition.id, 0x4008))
                    {
                        this.NewCategoriesAvailable_Silk = true;
                    }
                    if (CardTypes.isCardInNewCategory(definition.id, 0x8005))
                    {
                        this.NewCategoriesAvailable_Catapults = true;
                    }
                    if (CardTypes.isCardInNewCategory(definition.id, 0x20005))
                    {
                        this.NewCategoriesAvailable_Strategy = true;
                    }
                    if (CardTypes.isCardInNewCategory(definition.id, 0x40007))
                    {
                        this.NewCategoriesAvailable_Capacity = true;
                    }
                    if (CardTypes.isCardInNewCategory(definition.id, 0x80000))
                    {
                        this.NewCategoriesAvailable_Parish = true;
                    }
                }
            }
            if (this.NewCategoriesAvailable_Parish && (this.NewCategoriesAvailable_Capacity || ((this.NewCategoriesAvailable_Salt && this.NewCategoriesAvailable_Spice) && this.NewCategoriesAvailable_Silk)))
            {
                this.NewCategoriesAvailable_FullHeight = true;
            }
        }

        public double calcVillageDistance(int fromVillageID, int villageID)
        {
            if (this.m_userVillages == null)
            {
                return 0.0;
            }
            if ((villageID < 0) || (villageID >= this.villageList.Length))
            {
                return 0.0;
            }
            if ((fromVillageID < 0) || (fromVillageID >= this.villageList.Length))
            {
                return 0.0;
            }
            int regionID = this.villageList[villageID].regionID;
            int capitalVillage = this.regionList[regionID].capitalVillage;
            int index = this.villageList[fromVillageID].regionID;
            if (((index == regionID) || (index < 0)) || (regionID < 0))
            {
                return 0.0;
            }
            int num4 = this.regionList[index].capitalVillage;
            int num5 = this.villageList[capitalVillage].x - this.villageList[num4].x;
            int num6 = this.villageList[capitalVillage].y - this.villageList[num4].y;
            int maxVillageCostDistance = (num5 * num5) + (num6 * num6);
            maxVillageCostDistance = (int) Math.Sqrt((double) maxVillageCostDistance);
            if (maxVillageCostDistance > GameEngine.Instance.LocalWorldData.maxVillageCostDistance)
            {
                maxVillageCostDistance = GameEngine.Instance.LocalWorldData.maxVillageCostDistance;
            }
            return (((double) maxVillageCostDistance) / ((double) GameEngine.Instance.LocalWorldData.maxVillageCostDistance));
        }

        public void CancelQueuedResearch(int researchType, int queuePos)
        {
            if (this.inDoResearch)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastDoResearchClick);
                if (span.TotalSeconds < 120.0)
                {
                    return;
                }
            }
            this.inDoResearch = true;
            this.lastDoResearchClick = DateTime.Now;
            RemoteServices.Instance.set_DoResearch_UserCallBack(new RemoteServices.DoResearch_UserCallBack(this.doResearchCallback));
            RemoteServices.Instance.CancelQueuedResearch(researchType, queuePos);
        }

        public bool canUserOwnMoreVassals()
        {
            int userRank = this.m_userRank;
            int num2 = GameEngine.Instance.LocalWorldData.getMaxVassals(userRank, this.m_userRankSubLevel);
            return (this.countVassals() < num2);
        }

        public bool canUserOwnMoreVillages()
        {
            int num = this.numVillagesAllowed();
            return (this.numVillagesOwned() < num);
        }

        public void capZoom(double cap)
        {
            this.m_zoomCap = cap;
            this.capZoomIFace((float) cap);
        }

        public void capZoomIFace(float cap)
        {
            this.zoomMin = (int) (cap * 1000f);
        }

        public void CardPlayed(int section, int type, int villageid)
        {
            this.handleQuestObjectiveHappening_PlayedCard(CardTypes.getCardType(type));
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            switch (CardTypes.getCardType(type))
            {
                case 0x101:
                case 0x102:
                case 0x103:
                    flag2 = true;
                    break;

                case 0x201:
                case 0x202:
                case 0x203:
                case 0x204:
                case 0x205:
                case 0x206:
                case 0x207:
                case 520:
                case 0x209:
                case 0x20a:
                case 0x20b:
                case 0x20c:
                case 0x20d:
                case 0x20e:
                case 0x20f:
                case 0x210:
                case 0x211:
                case 530:
                case 0x213:
                case 0x214:
                case 0x215:
                case 0x216:
                case 0x217:
                case 0x218:
                case 0x219:
                case 0x21a:
                case 0x21b:
                case 540:
                case 0x21d:
                case 0x21e:
                case 0x301:
                case 770:
                case 0x303:
                case 0x304:
                case 0x305:
                case 0x306:
                case 0x307:
                case 0x308:
                case 0x309:
                case 0x30a:
                case 0x30b:
                case 780:
                case 0x30d:
                case 0x30e:
                case 0x30f:
                case 0x401:
                case 0x402:
                case 0x403:
                case 0x404:
                case 0x405:
                case 0x406:
                case 0x407:
                case 0x408:
                case 0x409:
                case 0x40a:
                case 0x40b:
                case 0x40c:
                case 0x40d:
                case 0x40e:
                case 0x40f:
                case 0x501:
                case 0x502:
                case 0x503:
                case 0x504:
                case 0x505:
                case 0x506:
                case 0x507:
                case 0x508:
                case 0x509:
                case 0x50a:
                case 0x50b:
                case 0x50c:
                case 0x50d:
                case 0x50e:
                case 0x50f:
                case 0x510:
                case 0x511:
                case 0x512:
                case 0x513:
                case 0x514:
                case 0x515:
                case 0x516:
                case 0x517:
                case 0x518:
                case 0x519:
                case 0x51a:
                case 0x51b:
                    flag3 = true;
                    break;

                case 0x708:
                case 0x709:
                case 0x70a:
                    flag2 = true;
                    break;

                case 0xb01:
                case 0xb02:
                case 0xb03:
                case 0xb04:
                case 0xb05:
                case 0xb06:
                case 0xb07:
                case 0xb08:
                case 0xb09:
                case 0xb0a:
                case 0xb0b:
                case 0xb0c:
                case 0xb0d:
                case 0xb0e:
                case 0xb0f:
                    flag3 = true;
                    break;

                case 0xb81:
                case 0xb82:
                case 0xb83:
                    flag3 = true;
                    break;

                case 0xb9a:
                case 0xb9b:
                case 0xb9c:
                case 0xb9d:
                case 0xb9e:
                case 0xb9f:
                    flag3 = true;
                    break;

                case 0xc01:
                case 0xc02:
                case 0xc03:
                    flag4 = true;
                    break;

                case 0xc04:
                    flag = true;
                    break;

                case 0xc05:
                case 0xc06:
                case 0xc07:
                case 0xc08:
                case 0xc09:
                case 0xc0a:
                case 0xc0b:
                case 0xc0c:
                    flag = true;
                    break;

                case 0xc0d:
                case 0xc0e:
                case 0xc0f:
                case 0xc10:
                case 0xc11:
                case 0xc12:
                case 0xc13:
                case 0xc14:
                case 0xc15:
                case 0xc16:
                case 0xc17:
                case 0xc18:
                case 0xc19:
                case 0xc1a:
                case 0xc1b:
                case 0xc1c:
                case 0xc1d:
                case 0xc1e:
                case 0xc1f:
                case 0xc20:
                case 0xc21:
                case 0xc22:
                case 0xc23:
                case 0xc24:
                case 0xc25:
                case 0xc26:
                case 0xc27:
                case 0xc28:
                case 0xc29:
                case 0xc2a:
                case 0xc2b:
                case 0xc2c:
                case 0xc2d:
                case 0xc2e:
                case 0xc2f:
                case 0xc30:
                case 0xc31:
                case 0xc32:
                case 0xc33:
                case 0xc34:
                case 0xc35:
                case 0xc36:
                case 0xc37:
                case 0xc38:
                case 0xc39:
                case 0xc3a:
                case 0xc3b:
                case 0xc3c:
                case 0xc3d:
                case 0xc3e:
                case 0xc3f:
                case 0xc40:
                case 0xc41:
                case 0xc42:
                case 0xc43:
                case 0xc44:
                case 0xc45:
                case 0xc46:
                case 0xc47:
                case 0xc48:
                case 0xc49:
                case 0xc4a:
                case 0xc4b:
                case 0xc4c:
                case 0xc4d:
                case 0xc4e:
                case 0xc4f:
                case 0xc50:
                case 0xc51:
                case 0xc52:
                case 0xc53:
                case 0xc54:
                case 0xc55:
                case 0xc56:
                case 0xc57:
                case 0xc58:
                case 0xc59:
                case 0xc5a:
                case 0xc5b:
                case 0xc5c:
                case 0xc5d:
                case 0xc5e:
                case 0xc5f:
                case 0xc60:
                case 0xc61:
                case 0xc62:
                case 0xc63:
                case 0xc64:
                case 0xc65:
                case 0xc66:
                case 0xc67:
                case 0xc68:
                case 0xc69:
                case 0xc6a:
                case 0xc6b:
                case 0xc6c:
                    flag2 = true;
                    break;

                case 0xc81:
                case 0xc82:
                case 0xc83:
                    flag2 = true;
                    break;

                case 0xca1:
                case 0xca2:
                case 0xca3:
                    flag4 = true;
                    break;

                case 0xca4:
                case 0xca5:
                case 0xca6:
                    flag2 = true;
                    break;

                case 0xcb1:
                case 0xcb2:
                case 0xcb3:
                case 0xcb4:
                    flag4 = true;
                    break;

                case 0xcc0:
                case 0xcc1:
                case 0xcc2:
                case 0xcc3:
                case 0xcc4:
                case 0xcc5:
                case 0xcc6:
                case 0xcc7:
                case 0xcc8:
                case 0xcc9:
                case 0xcca:
                case 0xccb:
                case 0xccc:
                case 0xccd:
                case 0xcce:
                case 0xccf:
                case 0xcd0:
                case 0xcd1:
                case 0xcd2:
                case 0xcd3:
                    flag2 = true;
                    break;

                case 0xcd4:
                case 0xcd5:
                case 0xcd6:
                    flag2 = true;
                    break;

                case 0xcd7:
                case 0xcd8:
                case 0xcd9:
                case 0xcda:
                case 0xcdb:
                case 0xcdc:
                case 0xcdd:
                case 0xcde:
                case 0xcdf:
                case 0xce0:
                case 0xce1:
                case 0xce2:
                    flag2 = true;
                    break;
            }
            if (flag)
            {
                GameEngine.Instance.flushVillages();
                GameEngine.Instance.World.doFullTick(true, 0);
            }
            else
            {
                if (flag3)
                {
                    GameEngine.Instance.flushVillages();
                    GameEngine.Instance.downloadCurrentVillage();
                    Thread.Sleep(200);
                }
                else if (flag2 && (villageid >= 0))
                {
                    GameEngine.Instance.flushVillage(villageid);
                    GameEngine.Instance.downloadCurrentVillage();
                    Thread.Sleep(200);
                }
                if (flag4)
                {
                    GameEngine.Instance.World.updateResearch(true);
                    Thread.Sleep(200);
                }
                RemoteServices.Instance.set_UpdateCurrentCards_UserCallBack(new RemoteServices.UpdateCurrentCards_UserCallBack(this.updateCurrentCardsCallback));
                RemoteServices.Instance.UpdateCurrentCards();
            }
        }

        public void centreMap(bool useTarget)
        {
            if ((this.m_zoomStage < 0) || (this.m_zoomStage >= 6))
            {
                double worldScale = this.m_worldScale;
                double screenCentreX = this.m_screenCentreX;
                double screenCentreY = this.m_screenCentreY;
                if (useTarget && (this.m_zoomDiff != 0.0))
                {
                    double num4 = 27.0 - this.m_targetZoom;
                    if (num4 >= 23.0)
                    {
                        worldScale = 1.0 / (num4 - 22.0);
                    }
                    else
                    {
                        worldScale = 24.0 - num4;
                    }
                    screenCentreX = this.m_zoomXPosTarget;
                    screenCentreY = this.m_zoomYPosTarget;
                }
                int num5 = 0;
                double num6 = ((0.0 - (((double) this.m_screenWidth) / 2.0)) / worldScale) + screenCentreX;
                double num7 = ((-num5 - (((double) this.m_screenHeight) / 2.0)) / worldScale) + screenCentreY;
                double num8 = ((this.m_screenWidth - (((double) this.m_screenWidth) / 2.0)) / worldScale) + screenCentreX;
                double num9 = ((this.m_screenHeight - (((double) this.m_screenHeight) / 2.0)) / worldScale) + screenCentreY;
                bool flag = false;
                if (this.m_zooming && (this.m_zoomDiff > 0.0))
                {
                    flag = true;
                }
                if ((num6 < 0.0) && (num8 >= this.worldMapWidth))
                {
                    this.m_screenCentreX = this.worldMapWidth / 2;
                    if (!flag)
                    {
                        this.m_zoomXPosDiff = 0.0;
                    }
                }
                else if (num6 < 0.0)
                {
                    double num10 = ((double) this.m_screenWidth) / worldScale;
                    if (num10 > this.worldMapWidth)
                    {
                        this.m_screenCentreX = this.worldMapWidth / 2;
                        if (!flag)
                        {
                            this.m_zoomXPosDiff = 0.0;
                        }
                    }
                    else
                    {
                        this.m_screenCentreX = num10 / 2.0;
                    }
                }
                else if (num8 >= this.worldMapWidth)
                {
                    double num11 = ((double) this.m_screenWidth) / worldScale;
                    if (num11 > this.worldMapWidth)
                    {
                        this.m_screenCentreX = this.worldMapWidth / 2;
                        if (!flag)
                        {
                            this.m_zoomXPosDiff = 0.0;
                        }
                    }
                    else
                    {
                        this.m_screenCentreX = this.worldMapWidth - (num11 / 2.0);
                    }
                }
                if ((num7 < 0.0) && (num9 >= this.worldMapHeight))
                {
                    this.m_screenCentreY = (this.worldMapHeight / 2) + ((((double) num5) / 2.0) / worldScale);
                    if (!flag)
                    {
                        this.m_zoomYPosDiff = 0.0;
                    }
                }
                else if (num7 < 0.0)
                {
                    double num12 = ((double) this.m_screenHeight) / worldScale;
                    if (num12 <= this.worldMapHeight)
                    {
                        this.m_screenCentreY = (num12 / 2.0) + (((double) num5) / worldScale);
                    }
                    else
                    {
                        this.m_screenCentreY = (this.worldMapHeight / 2) + ((((double) num5) / 2.0) / worldScale);
                        if (!flag)
                        {
                            this.m_zoomYPosDiff = 0.0;
                        }
                    }
                }
                else if (num9 >= this.worldMapHeight)
                {
                    double num13 = ((double) this.m_screenHeight) / worldScale;
                    if (num13 > this.worldMapHeight)
                    {
                        this.m_screenCentreY = (this.worldMapHeight / 2) + (((double) (num5 / 2)) / worldScale);
                        if (!flag)
                        {
                            this.m_zoomYPosDiff = 0.0;
                        }
                    }
                    else
                    {
                        this.m_screenCentreY = this.worldMapHeight - (num13 / 2.0);
                    }
                }
            }
        }

        public void changeVillageNames(List<NameUpdateListItem> newNames)
        {
            if (newNames != null)
            {
                foreach (NameUpdateListItem item in newNames)
                {
                    this.villageList[item.areaID].villageName = item.newName;
                }
            }
        }

        public void changeZoom(float change)
        {
            int zoomMin = this.zoomCurrent + ((int) (change * 1000f));
            if (zoomMin < this.zoomMin)
            {
                zoomMin = this.zoomMin;
            }
            if (zoomMin > this.zoomMax)
            {
                zoomMin = this.zoomMax;
            }
            this.zoomCurrent = zoomMin;
            this.worldZoomCallback(((double) this.zoomCurrent) / 1000.0, false);
        }

        public void changeZoom(float change, Point mousePos)
        {
            double mapPosX = 0.0;
            double mapPosY = 0.0;
            this.getMapCoords(mousePos, ref mapPosX, ref mapPosY);
            int zoomMin = (int) (change * 1000f);
            if (zoomMin < this.zoomMin)
            {
                zoomMin = this.zoomMin;
            }
            if (zoomMin > this.zoomMax)
            {
                zoomMin = this.zoomMax;
            }
            this.zoomCurrent = zoomMin;
            this.worldZoomCallback(((double) this.zoomCurrent) / 1000.0, false);
            if (zoomMin < this.zoomMax)
            {
                double num4 = 0.0;
                double num5 = 0.0;
                this.getMapCoords(mousePos, ref num4, ref num5);
                mapPosX += this.m_screenCentreX - num4;
                mapPosY += this.m_screenCentreY - num5;
                this.m_screenCentreX = mapPosX;
                this.m_screenCentreY = mapPosY;
            }
            if (this.m_zoomDiff <= 0.0)
            {
                this.centreMap(false);
            }
        }

        public void checkQuestObjectiveComplete(int quest)
        {
            RemoteServices.Instance.set_CheckQuestObjectiveComplete_UserCallBack(new RemoteServices.CheckQuestObjectiveComplete_UserCallBack(this.CheckQuestObjectiveCompleteCallBack));
            RemoteServices.Instance.CheckQuestObjectiveComplete(quest);
        }

        public void CheckQuestObjectiveCompleteCallBack(CheckQuestObjectiveComplete_ReturnType returnData)
        {
            if (returnData.Success && (returnData.questCompleted >= 0))
            {
                this.tutorialQuestsObjectivesComplete.Add(returnData.questCompleted);
            }
        }

        public bool checkRecentRetrievalSend(long armyID)
        {
            foreach (ArmyRetrieveData data in this.requestedReturnedArmyIDs)
            {
                if (data.armyID == armyID)
                {
                    return false;
                }
            }
            return true;
        }

        public void clearCachedVillageUserInfo()
        {
            this.cached_retrieveVillageID = -1;
            this.cached_retrieveUserID = -1;
        }

        public void clearGloryHistory()
        {
            this.lastHouseGloryPointsUpdate = DateTime.MinValue;
        }

        public void clearInterVillageLines()
        {
            this.interVillageLines.Clear();
        }

        public void clearParishChat()
        {
            this.m_parishChatLog = new SparseArray();
            this.m_parishWallDonateDetails = new SparseArray();
        }

        public void clearPersonArray(int villageID)
        {
            if (villageID != -2)
            {
                if (villageID < 0)
                {
                    this.personArray.Clear();
                }
                else
                {
                    List<LocalPerson> list = new List<LocalPerson>();
                    foreach (LocalPerson person in this.personArray)
                    {
                        if (person.person.homeVillageID == villageID)
                        {
                            list.Add(person);
                        }
                    }
                    foreach (LocalPerson person2 in list)
                    {
                        this.personArray[person2.personID] = null;
                    }
                }
            }
        }

        public void clearShieldCache()
        {
            foreach (ShieldTextureCacheEntry entry in this.worldShieldCache)
            {
                entry.playerID = -10000;
            }
            this.worldShieldCachePlayerIDs.Clear();
        }

        public void clearTraderArray(int villageID)
        {
            if (villageID != -2)
            {
                if (villageID < 0)
                {
                    this.traderArray.Clear();
                }
                else
                {
                    List<LocalTrader> list = new List<LocalTrader>();
                    foreach (LocalTrader trader in this.traderArray)
                    {
                        if (trader.trader.homeVillageID == villageID)
                        {
                            list.Add(trader);
                        }
                    }
                    foreach (LocalTrader trader2 in list)
                    {
                        this.traderArray[trader2.traderID] = null;
                    }
                }
            }
        }

        public void clearVillageRolloverInfo(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                this.villageList[villageID].rolloverInfo = null;
            }
        }

        public int countCardsInCategory(int newCategory)
        {
            if (newCategory == this.lastCountedCategory)
            {
                return this.lastCountedCategoryValue;
            }
            int num = 0;
            foreach (int num2 in this.ProfileCards.Keys)
            {
                if ((CardTypes.isCardInNewCategory(this.ProfileCards[num2].id, newCategory) || ((newCategory == 0x100000) && GameEngine.Instance.recentCards.Contains(this.ProfileCards[num2].id))) || ((newCategory == 0x200000) && (this.ProfileCards[num2].cardRank <= (this.getRank() + 1))))
                {
                    num++;
                }
            }
            this.lastCountedCategoryValue = num;
            this.lastCountedCategory = newCategory;
            return num;
        }

        public void countChildren()
        {
            foreach (LocalPerson person in this.personArray)
            {
                if (((person.parentPerson != -1L) && (person.personID != person.parentPerson)) && this.personArray.ContainsKey(person.parentPerson))
                {
                    LocalPerson person1 = (LocalPerson) this.personArray[person.parentPerson];
                    person1.childrenCount++;
                }
            }
        }

        public int countHouseMembers(int houseID)
        {
            int num = 0;
            foreach (FactionData data in this.m_factionData)
            {
                if ((data.active && (data.factionName.Length > 0)) && ((data.numMembers > 0) && (data.houseID == houseID)))
                {
                    num++;
                }
            }
            return num;
        }

        public int countIncomingAttacks(ref long highestAttackingArmy)
        {
            int num = 0;
            highestAttackingArmy = -1L;
            foreach (LocalArmyData data in this.armyArray)
            {
                if ((this.isUserVillage(data.targetVillageID) && (data.lootType < 0)) && ((data.attackType != 30) && (data.attackType != 0x1f)))
                {
                    num++;
                    if (data.armyID > highestAttackingArmy)
                    {
                        highestAttackingArmy = data.armyID;
                    }
                }
            }
            return num;
        }

        public int countPigsCastles()
        {
            if (this.nextPigsCalc >= DateTime.Now)
            {
                return this.lastPigsValue;
            }
            int num = 0;
            foreach (VillageData data in this.villageList)
            {
                if ((data.special == 11) && data.visible)
                {
                    num++;
                }
            }
            this.lastPigsValue = num;
            this.nextPigsCalc = DateTime.Now.AddSeconds(30.0);
            return num;
        }

        public int countPlayableCards(int category)
        {
            int num = 0;
            if ((this.ProfileCards != null) && (this.ProfileCards.Values != null))
            {
                foreach (CardTypes.CardDefinition definition in this.ProfileCards.Values)
                {
                    if (definition != null)
                    {
                        if ((definition.cardCategory == category) || (category == 0))
                        {
                            num++;
                        }
                        else if (((category == 9) && ((definition.cardCategory == 6) || (definition.cardCategory == 7))) && (definition.name != "CARDTYPE_FLAG"))
                        {
                            num++;
                        }
                    }
                }
            }
            return num;
        }

        public int countRatsCastles()
        {
            if (this.nextRatsCalc >= DateTime.Now)
            {
                return this.lastRatsValue;
            }
            int num = 0;
            foreach (VillageData data in this.villageList)
            {
                if ((data.special == 7) && data.visible)
                {
                    num++;
                }
            }
            this.lastRatsValue = num;
            this.nextRatsCalc = DateTime.Now.AddSeconds(30.0);
            return num;
        }

        public int countSnakesCastles()
        {
            if (this.nextSnakesCalc >= DateTime.Now)
            {
                return this.lastSnakesValue;
            }
            int num = 0;
            foreach (VillageData data in this.villageList)
            {
                if ((data.special == 9) && data.visible)
                {
                    num++;
                }
            }
            this.lastSnakesValue = num;
            this.nextSnakesCalc = DateTime.Now.AddSeconds(30.0);
            return num;
        }

        public int countVassals()
        {
            int num = 0;
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    if (data.vassals != null)
                    {
                        num += data.vassals.Count;
                    }
                }
            }
            return num;
        }

        public int countVillagePeople(int villageID, int personType, ref int athome)
        {
            athome = 0;
            int num = 0;
            foreach (LocalPerson person in this.personArray)
            {
                if ((person.person.homeVillageID == villageID) && (person.person.personType == personType))
                {
                    num++;
                    if (person.person.state == 0)
                    {
                        athome++;
                    }
                }
            }
            return num;
        }

        public int countWolfsCastles()
        {
            if (this.nextWolfsCalc >= DateTime.Now)
            {
                return this.lastWolfsValue;
            }
            int num = 0;
            foreach (VillageData data in this.villageList)
            {
                if ((data.special == 13) && data.visible)
                {
                    num++;
                }
            }
            this.lastWolfsValue = num;
            this.nextWolfsCalc = DateTime.Now.AddSeconds(30.0);
            return num;
        }

        public int countYourArmyCaptains(int villageID)
        {
            int num = 0;
            foreach (LocalArmyData data in this.armyArray)
            {
                if (data.travelFromVillageID == villageID)
                {
                    num += data.numCaptains;
                }
            }
            return num;
        }

        public int countYourArmyScouts(int villageID)
        {
            int num = 0;
            foreach (LocalArmyData data in this.armyArray)
            {
                if (data.homeVillageID == villageID)
                {
                    num += data.numScouts;
                }
            }
            return num;
        }

        public int countYourArmyTroops(int villageID)
        {
            int num = 0;
            foreach (LocalArmyData data in this.armyArray)
            {
                if ((data.travelFromVillageID == villageID) && (data.homeVillageID == villageID))
                {
                    num += data.numPeasants;
                    num += data.numArchers;
                    num += data.numPikemen;
                    num += data.numSwordsmen;
                    num += data.numCatapults;
                }
            }
            return num;
        }

        public int countYourReinforcementTroops(int villageID)
        {
            int num = 0;
            foreach (LocalArmyData data in this.reinforcementArray)
            {
                if (data.homeVillageID == villageID)
                {
                    num += data.numPeasants;
                    num += data.numArchers;
                    num += data.numPikemen;
                    num += data.numSwordsmen;
                    num += data.numCatapults;
                }
            }
            return num;
        }

        public void createTributeLinesList(int villageID)
        {
            this.clearInterVillageLines();
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                VillageData data = this.villageList[villageID];
                foreach (VillageData data2 in this.villageList)
                {
                    if (data2.connecter == villageID)
                    {
                        float scale = 2f - (((float) data2.villageInfo) / 120f);
                        this.addInterVillageLine(new Point(data.x, data.y), new Point(data2.x, data2.y), true, scale);
                    }
                }
                if ((data.connecter >= 0) && (data.connecter < this.villageList.Length))
                {
                    VillageData data3 = this.villageList[data.connecter];
                    float num2 = 2f - (((float) data.villageInfo) / 120f);
                    this.addInterVillageLine(new Point(data3.x, data3.y), new Point(data.x, data.y), false, num2);
                }
            }
        }

        public void deleteArmy(long armyID)
        {
            this.armyArray[armyID] = null;
        }

        public bool doesUserHaveVillageInParish(int parishID)
        {
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    if (this.getParishFromVillageID(data.villageID) == parishID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool doesUserHaveVillageInParishByCapital(int capitalID)
        {
            int parishID = this.getParishFromVillageID(capitalID);
            return this.doesUserHaveVillageInParish(parishID);
        }

        public void doFullTick(bool registerSession, int mode)
        {
            RemoteServices.Instance.FullTick(this.storedVillageFactionsPos, this.storedRegionFactionsPos, this.storedCountyFactionsPos, this.storedProvinceFactionsPos, this.storedCountryFactionsPos, registerSession, this.storedVillageNamePos, this.storedFactionChangesPos, this.lastTraderTime, this.lastPersonTime, this.storedParishFlagsPos, this.storedCountyFlagsPos, this.storedProvinceFlagsPos, this.storedCountryFlagsPos, this.highestDownloadedArmy, mode, fullTickFullMode | (mode > 1));
            fullTickFullMode = !fullTickFullMode;
        }

        public void doGetArmyData(IEnumerable<ArmyReturnData> armyReturnData, IEnumerable<ArmyReturnData> reinforcementReturnData, bool clearArray)
        {
            if (armyReturnData != null)
            {
                SparseArray armyRequestSentFlag = new SparseArray();
                foreach (LocalArmyData data in this.armyArray)
                {
                    armyRequestSentFlag[data.armyID] = data.requestSent;
                }
                if (clearArray)
                {
                    if (armyReturnData != null)
                    {
                        this.armyArray.Clear();
                    }
                    if (reinforcementReturnData != null)
                    {
                        this.reinforcementArray.Clear();
                    }
                }
                if (armyReturnData != null)
                {
                    foreach (ArmyReturnData data2 in armyReturnData)
                    {
                        this.addArmyToArray(data2, ref armyRequestSentFlag, clearArray);
                    }
                }
                if (reinforcementReturnData != null)
                {
                    foreach (ArmyReturnData data3 in reinforcementReturnData)
                    {
                        this.addArmyToArray(data3, ref armyRequestSentFlag, false);
                    }
                }
                foreach (LocalArmyData data4 in this.armyArray)
                {
                    if (data4.lootType == 0x5f)
                    {
                        double lootLevel = data4.lootLevel;
                        data4.lootType = 1;
                        long num = data4.armyID - ((long) data4.lootLevel);
                        if (this.armyArray[num] != null)
                        {
                            LocalArmyData data5 = (LocalArmyData) this.armyArray[num];
                            data5.dead = true;
                        }
                    }
                }
            }
        }

        public void doGetUserVillages(List<int> userVillageList, List<string> userVillageNameList)
        {
            foreach (VillageData data in this.villageList)
            {
                data.userVillageID = -1;
            }
            int count = userVillageList.Count;
            this.m_userVillages = new List<UserVillageData>();
            for (int i = 0; i < count; i++)
            {
                UserVillageData item = new UserVillageData {
                    villageID = userVillageList[i]
                };
                this.m_userVillages.Add(item);
                this.villageList[item.villageID].userVillageID = i;
                this.villageList[item.villageID].visible = true;
                this.villageList[item.villageID].userID = RemoteServices.Instance.UserID;
                this.villageList[item.villageID].factionID = RemoteServices.Instance.UserFactionID;
            }
            this.updateUserRelatedVillages();
            this.sortUserVillages();
            InterfaceMgr.Instance.validateUserVillage();
            this.updateUserVassals();
        }

        public void doResearch(int researchType)
        {
            if (this.inDoResearch)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastDoResearchClick);
                if (span.TotalSeconds < 120.0)
                {
                    return;
                }
            }
            this.inDoResearch = true;
            this.lastDoResearchClick = DateTime.Now;
            RemoteServices.Instance.set_DoResearch_UserCallBack(new RemoteServices.DoResearch_UserCallBack(this.doResearchCallback));
            RemoteServices.Instance.DoResearch(researchType);
        }

        public void doResearchCallback(DoResearch_ReturnType returnData)
        {
            this.inDoResearch = false;
            if (returnData.Success)
            {
                this.setResearchData(returnData.researchData);
                VillageMap.setServerTime(returnData.currentTime);
                InterfaceMgr.Instance.researchDataChanged(returnData.researchData);
            }
            else
            {
                switch (returnData.m_errorCode)
                {
                    case ErrorCodes.ErrorCode.RESEARCH_CANNOT_DO_RESEARCH_ALREADY_RESEARCHING:
                    case ErrorCodes.ErrorCode.RESEARCH_CANNOT_DO_RESEARCH_NOT_ENOUGH_POINTS:
                    case ErrorCodes.ErrorCode.RESEARCH_CANNOT_DO_RESEARCH_NOT_AVAILABLE:
                    case ErrorCodes.ErrorCode.RESEARCH_CANNOT_DO_RESEARCH_FULL:
                        InterfaceMgr.Instance.researchDataChanged(returnData.researchData);
                        return;
                }
            }
        }

        public void downloadAIInvasionInfo()
        {
            this.wolfsRevengeStart = DateTime.MinValue;
            this.wolfsRevengeEnd = DateTime.MinValue;
            this.lastInvasionInfoTime = DateTime.Now.AddHours(1.0);
            this.lastUpdateInvasionInfoTime = DateTime.Now.AddMinutes(5.0);
            RemoteServices.Instance.set_GetInvasionInfo_UserCallBack(new RemoteServices.GetInvasionInfo_UserCallBack(this.GetInvasionInfo_callback));
            RemoteServices.Instance.GetInvasionInfo();
        }

        public bool downloadingLeaderboard()
        {
            if (!this.inDownloading)
            {
                return this.inLeaderboardSearch;
            }
            return true;
        }

        public void downloadLeaderboard(SparseArray currentArray, int mode, int position, int pageSize)
        {
            if (!this.inDownloading)
            {
                this.inDownloading = true;
                RemoteServices.Instance.set_LeaderBoard_UserCallBack(new RemoteServices.LeaderBoard_UserCallBack(this.LeaderboardCallback));
                if (position < 0)
                {
                    RemoteServices.Instance.LeaderBoard(mode, -1, -1, this.leaderboardLastUpdateTime);
                }
                else
                {
                    int minValue = position;
                    int maxValue = position;
                    bool flag = false;
                    bool flag2 = false;
                    for (int i = 1; i < (50 + pageSize); i++)
                    {
                        int num4 = position - i;
                        if (num4 < 1)
                        {
                            num4 = 1;
                        }
                        int num5 = position + i;
                        if (!flag)
                        {
                            if (currentArray[num4] != null)
                            {
                                flag = true;
                            }
                            else
                            {
                                minValue = num4;
                            }
                        }
                        if (!flag2)
                        {
                            if (currentArray[num5] != null)
                            {
                                if (i > (pageSize + 1))
                                {
                                    flag = true;
                                }
                            }
                            else
                            {
                                maxValue = num5;
                            }
                        }
                    }
                    RemoteServices.Instance.LeaderBoard(mode, minValue, maxValue, this.leaderboardLastUpdateTime);
                }
            }
        }

        public void downloadPlayerShield(string md5, ShieldFactory.AsyncDelegate callback)
        {
            this.playerShieldCallback = callback;
            if (this.playerShieldFactory == null)
            {
                this.playerShieldFactory = new ShieldFactory();
            }
            this.playerShieldFactory.clear();
            this.playerShield = null;
            this.playerShieldFactory.downloadPlayerShieldAsync(md5, new ShieldFactory.AsyncDelegate(this.shieldDownloaded));
        }

        public void downloadWait()
        {
            Thread.Sleep(this.threadDelaySize);
            DateTime now = DateTime.Now;
            while (!RemoteServices.Instance.queueEmpty())
            {
                Thread.Sleep(20);
                if (GameEngine.Instance.loginCancelled())
                {
                    this.m_WorkerThread.Abort();
                }
                TimeSpan span = (TimeSpan) (DateTime.Now - now);
                if (span.TotalMinutes > 10.0)
                {
                    break;
                }
            }
            if (GameEngine.Instance.loginCancelled())
            {
                this.m_WorkerThread.Abort();
            }
            if (!this.loadingErrored)
            {
                this.downloadingCounter++;
            }
            else
            {
                Thread.Sleep((int) (0x7d0 + new Random().Next(0x3e8)));
            }
        }

        public void downloadWorldShields(int worldID)
        {
            this.activeShieldsWorldID = worldID;
            ShieldFactory factory = null;
            ShieldFactory factory2 = null;
            factory2 = (ShieldFactory) this.worldShields[worldID];
            if (factory2 != null)
            {
                factory = factory2;
                if (!factory.WorldsRequireRefresh(new TimeSpan(1, 0, 0)))
                {
                    this.worldShieldsAvailable = true;
                    this.clearShieldCache();
                    return;
                }
            }
            else
            {
                factory = new ShieldFactory();
                this.worldShields[worldID] = factory;
            }
            this.worldShieldsAvailable = false;
            this.clearShieldCache();
            factory.downloadWorldShieldsAsync(worldID, new ShieldFactory.AsyncDelegate(this.worldShieldsDownloaded));
        }

        public void drawAIWorldLines(RectangleF screenRect)
        {
            float thickness = (((float) this.m_worldScale) - 2f) / 3f;
            if (thickness < 1f)
            {
                thickness = 1f;
            }
            thickness *= 2f;
            if (this.aiWorldInvasionLineList != null)
            {
                foreach (IslandInfoList list in this.aiWorldInvasionLineList)
                {
                    if ((((list.sx >= screenRect.Left) || (list.ex >= screenRect.Left)) && ((list.sy >= screenRect.Top) || (list.ey >= screenRect.Top))) && (((list.sx <= screenRect.Right) || (list.ex <= screenRect.Right)) && ((list.sy <= screenRect.Bottom) || (list.ey <= screenRect.Bottom))))
                    {
                        Color darkRed = ARGBColors.DarkRed;
                        if (this.getAIInvasionMarkerState(list.villageID) == 0)
                        {
                            darkRed = Color.FromArgb(80, darkRed);
                        }
                        this.gfx.startThickLine(darkRed, thickness);
                        this.gfx.setThickLineRadius((float) this.m_worldScale);
                        float num2 = ((list.sx - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                        float num3 = ((list.sy - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                        this.gfx.addThickLinePoint(num2, num3);
                        float num4 = ((list.ex - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                        float num5 = ((list.ey - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                        this.gfx.addThickLinePoint(num4, num5);
                        this.gfx.drawThickLines(true);
                    }
                }
            }
        }

        public void drawAreaPoly(RectangleF screenRect, WorldPointList wpl, Color col)
        {
            if (wpl.isVisible(screenRect))
            {
                int length = wpl.triangleList.Length;
                if (length > 0)
                {
                    for (int i = 0; i < length; i++)
                    {
                        float num3 = wpl.triangleList[i].x1;
                        float num4 = wpl.triangleList[i].x2;
                        float num5 = wpl.triangleList[i].x3;
                        float num6 = wpl.triangleList[i].y1;
                        float num7 = wpl.triangleList[i].y2;
                        float num8 = wpl.triangleList[i].y3;
                        num3 = ((num3 - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                        num4 = ((num4 - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                        num5 = ((num5 - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                        num6 = ((num6 - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                        num7 = ((num7 - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                        num8 = ((num8 - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                        this.gfx.addTriangle(col, num3, num6, num4, num7, num5, num8);
                    }
                }
            }
        }

        public void drawArmies(RectangleF screenRect, bool normalMode)
        {
            try
            {
                this.scoutsForaging.Clear();
                this.scoutsVillageForaging.Clear();
                this.villagesInvolvedInAttacks.Clear();
                this.attackingArmies.Clear();
                this.villagesInvolvedInAIAttacks.Clear();
                float num = (((float) this.m_worldScale) / 28f) / 0.6f;
                if (num < 0.1f)
                {
                    num = 0.1f;
                }
                if (num > 1f)
                {
                    num = 1f;
                }
                List<long> list = new List<long>();
                WorldMapFilter worldMapFilter = GameEngine.Instance.World.worldMapFilter;
                this.armyIconsFilter.Clear();
                float num2 = num;
                foreach (LocalArmyData data in this.armyArray)
                {
                    bool flag2;
                    bool flag3;
                    num2 = num;
                    if (data.dead)
                    {
                        list.Add(data.armyID);
                        continue;
                    }
                    if (!normalMode && (data.attackType != 0x11))
                    {
                        continue;
                    }
                    if ((data.attackType == 0x11) && (num2 < 0.5f))
                    {
                        num2 = 0.5f;
                    }
                    bool flag = true;
                    if (worldMapFilter.FilterActive && (InterfaceMgr.Instance.WorldMapMode == 0))
                    {
                        if (data.isScouts())
                        {
                            if (this.isForagingSpecial(data.targetVillageID))
                            {
                                this.scoutsForaging[data.armyID] = data.armyID;
                                this.scoutsVillageForaging[data.homeVillageID] = data.homeVillageID;
                            }
                            else
                            {
                                this.attackingArmies[data.armyID] = data.armyID;
                                this.villagesInvolvedInAttacks[data.homeVillageID] = data.homeVillageID;
                                this.villagesInvolvedInAttacks[data.targetVillageID] = data.targetVillageID;
                            }
                        }
                        else
                        {
                            this.attackingArmies[data.armyID] = data.armyID;
                            this.villagesInvolvedInAttacks[data.homeVillageID] = data.homeVillageID;
                            this.villagesInvolvedInAttacks[data.targetVillageID] = data.targetVillageID;
                        }
                        flag = worldMapFilter.showArmy(data);
                        if (flag)
                        {
                            this.villagesInvolvedInAIAttacks[data.homeVillageID] = data.homeVillageID;
                            this.villagesInvolvedInAIAttacks[data.targetVillageID] = data.targetVillageID;
                        }
                    }
                    if (!data.isVisible(screenRect) || !flag)
                    {
                        continue;
                    }
                    this.villageSprite.PosX = ((((float) data.displayX) - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                    this.villageSprite.PosY = ((((float) data.displayY) - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                    int num3 = ((((int) this.villageSprite.PosX) / 3) * 0x186a0) + (((int) this.villageSprite.PosY) / 3);
                    if (this.armyIconsFilter[num3] != null)
                    {
                        continue;
                    }
                    this.armyIconsFilter[num3] = 1;
                    int num4 = 2;
                    int num5 = 6;
                    if ((data.attackType == 0x1f) || (data.attackType == 30))
                    {
                        num5 = 5;
                        num4 = 1;
                    }
                    else if (data.isScouts())
                    {
                        num5 = 14;
                        num4 = 2;
                        if (data.userID != RemoteServices.Instance.UserID)
                        {
                            num5++;
                            num4++;
                            if (GameEngine.Instance.LocalWorldData.AIWorld)
                            {
                                switch (this.villageList[data.travelFromVillageID].special)
                                {
                                    case 7:
                                    case 8:
                                        num5 = 0x193;
                                        num4 = 0x194;
                                        break;

                                    case 9:
                                    case 10:
                                        num5 = 0x197;
                                        num4 = 0x198;
                                        break;

                                    case 11:
                                    case 12:
                                        num5 = 0x19b;
                                        num4 = 0x19c;
                                        break;

                                    case 13:
                                    case 14:
                                        num5 = 0x19f;
                                        num4 = 0x1a0;
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!data.isCaptainAttack())
                        {
                            goto Label_05C6;
                        }
                        num5 = 0x180;
                        num4 = 2;
                        if (data.userID != RemoteServices.Instance.UserID)
                        {
                            num5++;
                            num4++;
                            if (GameEngine.Instance.LocalWorldData.AIWorld)
                            {
                                switch (this.villageList[data.travelFromVillageID].special)
                                {
                                    case 7:
                                    case 8:
                                        num5 = 0x195;
                                        num4 = 0x194;
                                        break;

                                    case 9:
                                    case 10:
                                        num5 = 0x199;
                                        num4 = 0x198;
                                        break;

                                    case 11:
                                    case 12:
                                        num5 = 0x19d;
                                        num4 = 0x19c;
                                        break;

                                    case 13:
                                    case 14:
                                        num5 = 0x1a1;
                                        num4 = 0x1a0;
                                        break;

                                    case 30:
                                        goto Label_0502;
                                }
                            }
                        }
                    }
                    goto Label_07D1;
                Label_0502:
                    flag2 = false;
                    switch (this.villageList[data.targetVillageID].special)
                    {
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                            num5 = 5;
                            num4 = 1;
                            flag2 = true;
                            break;
                    }
                    if (!flag2)
                    {
                        switch (data.aiPlayer)
                        {
                            case 0:
                                num5 = 0x195;
                                num4 = 0x194;
                                break;

                            case 1:
                                num5 = 0x199;
                                num4 = 0x198;
                                break;

                            case 2:
                                num5 = 0x19d;
                                num4 = 0x19c;
                                break;

                            case 3:
                                num5 = 0x1a1;
                                num4 = 0x1a0;
                                break;
                        }
                    }
                    goto Label_07D1;
                Label_05C6:
                    if (data.userID != RemoteServices.Instance.UserID)
                    {
                        num5 = 7;
                        num4 = 3;
                        if (GameEngine.Instance.LocalWorldData.AIWorld)
                        {
                            switch (this.villageList[data.travelFromVillageID].special)
                            {
                                case 7:
                                case 8:
                                    num5 = 0x192;
                                    num4 = 0x194;
                                    break;

                                case 9:
                                case 10:
                                    num5 = 0x196;
                                    num4 = 0x198;
                                    break;

                                case 11:
                                case 12:
                                    num5 = 410;
                                    num4 = 0x19c;
                                    break;

                                case 13:
                                case 14:
                                    num5 = 0x19e;
                                    num4 = 0x1a0;
                                    break;

                                case 30:
                                    goto Label_068B;
                            }
                        }
                    }
                    goto Label_07D1;
                Label_068B:
                    flag3 = false;
                    switch (this.villageList[data.targetVillageID].special)
                    {
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                            num5 = 5;
                            num4 = 1;
                            flag3 = true;
                            break;
                    }
                    if (!flag3)
                    {
                        VillageData data2 = this.villageList[data.targetVillageID];
                        if ((data2.visible && (data2.userID <= 0)) && (data2.special == 0))
                        {
                            switch (data.aiPlayer)
                            {
                                case 0:
                                    num5 = 0x195;
                                    num4 = 0x194;
                                    break;

                                case 1:
                                    num5 = 0x199;
                                    num4 = 0x198;
                                    break;

                                case 2:
                                    num5 = 0x19d;
                                    num4 = 0x19c;
                                    break;

                                case 3:
                                    num5 = 0x1a1;
                                    num4 = 0x1a0;
                                    break;
                            }
                            flag3 = true;
                        }
                    }
                    if (!flag3)
                    {
                        switch (data.aiPlayer)
                        {
                            case 0:
                                num5 = 0x192;
                                num4 = 0x194;
                                break;

                            case 1:
                                num5 = 0x196;
                                num4 = 0x198;
                                break;

                            case 2:
                                num5 = 410;
                                num4 = 0x19c;
                                break;

                            case 3:
                                num5 = 0x19e;
                                num4 = 0x1a0;
                                break;
                        }
                    }
                Label_07D1:
                    this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                    this.villageSprite.SpriteNo = num4;
                    this.villageSprite.Center = new PointF(44f, 44f);
                    this.villageSprite.RotationAngle = SpriteWrapper.getFacing(data.BasePoint(), data.TargetPoint());
                    this.villageSprite.Scale = num2;
                    this.villageSprite.Update();
                    this.villageSprite.DrawAndClear();
                    this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                    this.villageSprite.SpriteNo = num5;
                    this.villageSprite.Center = new PointF(44f, 44f);
                    this.villageSprite.Scale = num2;
                    this.villageSprite.Update();
                    this.villageSprite.DrawAndClear();
                    if (data.carryingFlag)
                    {
                        this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                        this.villageSprite.SpriteNo = 0x1d;
                        this.villageSprite.Center = new PointF(44f, 59f);
                        this.villageSprite.Scale = num2;
                        this.villageSprite.Update();
                        this.villageSprite.DrawAndClear();
                    }
                }
                foreach (long num6 in list)
                {
                    this.armyArray.RemoveAt(num6);
                }
                foreach (LocalArmyData data3 in this.reinforcementArray)
                {
                    if ((data3.visible && data3.isVisible(screenRect)) && worldMapFilter.showReinforcements(data3))
                    {
                        int num7 = 1;
                        this.villageSprite.PosX = ((((float) data3.displayX) - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                        this.villageSprite.PosY = ((((float) data3.displayY) - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                        this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                        this.villageSprite.SpriteNo = num7;
                        this.villageSprite.Center = new PointF(44f, 44f);
                        this.villageSprite.RotationAngle = SpriteWrapper.getFacing(data3.BasePoint(), data3.TargetPoint());
                        this.villageSprite.Scale = num;
                        this.villageSprite.Update();
                        this.villageSprite.DrawAndClear();
                        num7 = 5;
                        this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                        this.villageSprite.SpriteNo = num7;
                        this.villageSprite.Center = new PointF(44f, 44f);
                        this.villageSprite.Scale = num;
                        this.villageSprite.Update();
                        this.villageSprite.DrawAndClear();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void drawCountryBorders(RectangleF screenRect)
        {
            float scale = (((float) this.m_worldScale) - 2f) / 3f;
            if (scale < 1f)
            {
                scale = 1f;
            }
            scale *= 7f;
            Color purple = ARGBColors.Purple;
            if (this.m_worldScale <= 0.2)
            {
                scale = 3f;
                purple = ARGBColors.Black;
            }
            double num2 = 0.22;
            if (this.EUMap)
            {
                num2 = 0.5;
            }
            if (this.playingCountries)
            {
                purple = ARGBColors.Black;
            }
            foreach (WorldPointList list in this.countryList)
            {
                if ((this.drawProvinceBorder(screenRect, list, scale, purple) && (list.marker.X >= 0)) && (this.m_worldScale <= num2))
                {
                    float x = ((list.marker.X - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                    float num4 = ((list.marker.Y - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                    if (this.smallMapFont)
                    {
                        this.addText(list.areaName, new PointF(x, num4 + this.yMarkerOffset), ARGBColors.Black, true, 0, true);
                    }
                    else
                    {
                        this.addText(list.areaName, new PointF(x, num4 + this.yMarkerOffset), ARGBColors.Black, true, 1, true);
                    }
                }
            }
            if (!this.drawFakeProvinceBorders)
            {
                foreach (IslandInfoList list2 in this.islandList)
                {
                    WorldPointList wpl = this.countyList[list2.county];
                    this.drawProvinceBorder(screenRect, wpl, scale, purple);
                }
            }
            this.gfx.renderLines();
        }

        public void drawCountryPoly(RectangleF screenRect)
        {
            this.gfx.startPoly();
            foreach (WorldPointList list in this.countryList)
            {
                int areaCol = this.getVillageColour(list);
                Color baseColor = this.getAreaColour(areaCol, list);
                if (this.GeographicalMap)
                {
                    float num2 = 255f;
                    float num3 = ((float) this.m_worldZoom) / 17.5f;
                    if (num3 < 1f)
                    {
                        num2 *= num3;
                    }
                    if ((areaCol == 0) && (this.m_worldScale >= 23.899999998509884))
                    {
                        continue;
                    }
                    baseColor = Color.FromArgb((int) num2, baseColor);
                }
                this.drawAreaPoly(screenRect, list, baseColor);
            }
            foreach (IslandInfoList list2 in this.islandList)
            {
                WorldPointList wpl = this.countryList[list2.country];
                int num4 = this.getVillageColour(wpl);
                Color color2 = this.getAreaColour(num4, wpl);
                if (this.GeographicalMap)
                {
                    float num5 = 255f;
                    float num6 = ((float) this.m_worldZoom) / 17.5f;
                    if (num6 < 1f)
                    {
                        num5 *= num6;
                    }
                    if ((num4 == 0) && (this.m_worldScale >= 23.899999998509884))
                    {
                        continue;
                    }
                    color2 = Color.FromArgb((int) num5, color2);
                }
                WorldPointList list4 = this.countyList[list2.county];
                this.drawAreaPoly(screenRect, list4, color2);
            }
            this.gfx.drawPoly();
        }

        public void drawCountryPolyPlayback(RectangleF screenRect)
        {
            this.gfx.startPoly();
            int num = 0;
            foreach (WorldPointList list in this.countryList)
            {
                int index = this.getPlaybackCountryHouse(this.playbackDay, num++);
                Color col = areaColorList[index];
                this.drawAreaPoly(screenRect, list, col);
            }
            foreach (IslandInfoList list2 in this.islandList)
            {
                Color color2 = areaColorList[this.getPlaybackCountryHouse(this.playbackDay, list2.country)];
                WorldPointList wpl = this.countyList[list2.county];
                this.drawAreaPoly(screenRect, wpl, color2);
            }
            this.gfx.drawPoly();
        }

        public void drawCountyBorders(RectangleF screenRect, bool solidBorders)
        {
            Color black = ARGBColors.Black;
            if (!solidBorders)
            {
                Color.FromArgb(80, ARGBColors.Black);
            }
            float thickness = ((((float) this.m_worldScale) + 0.001f) - 2f) / 3f;
            if (thickness < 1f)
            {
                thickness = 1f;
            }
            double num2 = 0.5;
            double num3 = 5.0;
            if (this.EUMap)
            {
                num2 = 1.5;
            }
            foreach (WorldPointList list in this.countyList)
            {
                if (list.isVisible(screenRect))
                {
                    if (!this.LinelessMaps)
                    {
                        int length = list.borderList.Length;
                        if (length > 1)
                        {
                            this.gfx.startThickLine(ARGBColors.Black, thickness);
                            this.gfx.setThickLineRadius((float) this.m_worldScale);
                            for (int i = 0; i < length; i++)
                            {
                                WorldPoint point = this.pointList[list.borderList[i]];
                                float num6 = ((point.x - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                                float num7 = ((point.y - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                                this.gfx.addThickLinePoint(num6, num7);
                            }
                            this.gfx.drawThickLines(true);
                        }
                    }
                    if (((list.marker.X >= 0) && (this.m_worldScale < num3)) && (this.m_worldScale > num2))
                    {
                        float x = ((list.marker.X - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                        float num9 = ((list.marker.Y - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                        if (this.smallMapFont)
                        {
                            this.addText(list.areaName, new PointF(x, num9 + this.yMarkerOffset), ARGBColors.Black, true, 0, true);
                        }
                        else
                        {
                            this.addText(list.areaName, new PointF(x, num9 + this.yMarkerOffset), ARGBColors.Black, true, 1, true);
                        }
                    }
                }
            }
            this.gfx.renderLines();
        }

        public void drawCountyPoly(RectangleF screenRect)
        {
            this.gfx.startPoly();
            foreach (WorldPointList list in this.countyList)
            {
                int areaCol = this.getVillageColour(list);
                Color baseColor = this.getAreaColour(areaCol, list);
                if (this.GeographicalMap)
                {
                    float num2 = 255f;
                    float num3 = ((float) this.m_worldZoom) / 17.5f;
                    if (num3 < 1f)
                    {
                        num2 *= num3;
                    }
                    if ((areaCol == 0) && (this.m_worldScale >= 23.899999998509884))
                    {
                        continue;
                    }
                    baseColor = Color.FromArgb((int) num2, baseColor);
                }
                this.drawAreaPoly(screenRect, list, baseColor);
            }
            this.gfx.drawPoly();
        }

        public void drawInterVillageLine(InterVillageLine line, RectangleF screenRect, Color outerColour, Color innerColour)
        {
            PointF point = new PointF(line.end.X - line.start.X, line.end.Y - line.start.Y);
            PointF tf2 = point;
            float num = ((float) Math.Sqrt((double) ((point.X * point.X) + (point.Y * point.Y)))) * line.widthScalar;
            point.X /= num;
            point.Y /= num;
            PointF tf3 = this.gfx.rotatePoint(point, -90);
            PointF tf4 = this.gfx.rotatePoint(point, 90);
            tf3.X += line.end.X;
            tf3.Y += line.end.Y;
            tf4.X += line.end.X;
            tf4.Y += line.end.Y;
            float num2 = ((tf2.X / 5f) * (tf2.X / 5f)) + ((tf2.Y / 5f) * (tf2.Y / 5f));
            float num3 = ((point.X * 1.5f) * (point.X * 1.5f)) + ((point.Y * 1.5f) * (point.Y * 1.5f));
            if ((num2 < num3) && line.minLength)
            {
                point.X *= 1.5f;
                point.Y *= 1.5f;
                point.X = line.end.X - point.X;
                point.Y = line.end.Y - point.Y;
            }
            else
            {
                point.X = line.end.X - (tf2.X / 5f);
                point.Y = line.end.Y - (tf2.Y / 5f);
            }
            float num4 = ((tf3.X - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
            float num5 = ((tf3.Y - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
            float num6 = ((tf4.X - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
            float num7 = ((tf4.Y - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
            float num8 = ((line.start.X - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
            float num9 = ((line.start.Y - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
            float num10 = ((point.X - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
            float num11 = ((point.Y - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
            if ((line.style == 5) || (line.style == 6))
            {
                this.gfx.addTriangle(outerColour, innerColour, outerColour, num4, num5, num8, num9, num6, num7);
            }
            else
            {
                this.gfx.addTriangle(outerColour, num4, num5, num8, num9, num6, num7);
                this.gfx.addTriangle(innerColour, num4, num5, num10, num11, num6, num7);
            }
        }

        public void drawInterVillageLines(RectangleF screenRect)
        {
            Color outerColour = Color.FromArgb(0x80, ARGBColors.Blue);
            Color innerColour = Color.FromArgb(0x80, ARGBColors.LightBlue);
            Color color3 = Color.FromArgb(0x80, ARGBColors.Green);
            Color color4 = Color.FromArgb(0x80, ARGBColors.LightGreen);
            Color color5 = Color.FromArgb(0x80, Color.FromArgb(0x80, 0xff, 0x80));
            Color color6 = Color.FromArgb(0x80, Color.FromArgb(0xc0, 0xff, 0xc0));
            Color color7 = Color.FromArgb(0x80, Color.FromArgb(0xff, 0x80, 0x80));
            Color color8 = Color.FromArgb(0x80, Color.FromArgb(0xff, 0xc0, 0xc0));
            double num = 27.0 - this.m_worldZoom;
            if ((num >= 8.0) && ((this.interVillageLines.Count > 0) || (this.dynamicVillageLines.Count > 0)))
            {
                float num2 = (((float) this.m_worldScale) - 2f) / 3f;
                if (num2 < 0.3f)
                {
                    num2 = 0.3f;
                }
                this.gfx.startPoly();
                if (InterfaceMgr.Instance.WorldMapMode == 0)
                {
                    foreach (InterVillageLine line in this.interVillageLines)
                    {
                        if (line.style == 2)
                        {
                            this.drawInterVillageLine(line, screenRect, color3, color4);
                        }
                        else if (line.style == 1)
                        {
                            this.drawInterVillageLine(line, screenRect, outerColour, innerColour);
                        }
                    }
                }
                foreach (InterVillageLine line2 in this.dynamicVillageLines)
                {
                    if (line2.style == 3)
                    {
                        this.drawInterVillageLine(line2, screenRect, color5, color6);
                    }
                    else if (line2.style == 4)
                    {
                        this.drawInterVillageLine(line2, screenRect, color7, color8);
                    }
                    else if (line2.style == 5)
                    {
                        this.drawInterVillageLine(line2, screenRect, ARGBColors.Yellow, Color.FromArgb(0x40, 0xff, 0x40));
                    }
                    else if (line2.style == 6)
                    {
                        double num3 = ((double) this.pulse) / 128.0;
                        if (num3 > 1.0)
                        {
                            num3 = 2.0 - num3;
                        }
                        Color color9 = Color.FromArgb(0x60, 0xff - ((int) (191.0 * num3)), 0xff, (int) (64.0 * num3));
                        Color color10 = Color.FromArgb(0x60, ARGBColors.Yellow);
                        this.drawInterVillageLine(line2, screenRect, color10, color9);
                    }
                }
                this.gfx.drawPoly();
            }
            this.dynamicVillageLines.Clear();
        }

        public void drawIslandLines(RectangleF screenRect)
        {
            float thickness = (((float) this.m_worldScale) - 2f) / 3f;
            if (thickness < 1f)
            {
                thickness = 1f;
            }
            thickness *= 2f;
            foreach (IslandInfoList list in this.islandList)
            {
                if ((((list.sx >= screenRect.Left) || (list.ex >= screenRect.Left)) && ((list.sy >= screenRect.Top) || (list.ey >= screenRect.Top))) && (((list.sx <= screenRect.Right) || (list.ex <= screenRect.Right)) && ((list.sy <= screenRect.Bottom) || (list.ey <= screenRect.Bottom))))
                {
                    this.gfx.startThickLine(ARGBColors.DarkBlue, thickness);
                    this.gfx.setThickLineRadius((float) this.m_worldScale);
                    float num2 = ((list.sx - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                    float num3 = ((list.sy - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                    this.gfx.addThickLinePoint(num2, num3);
                    float num4 = ((list.ex - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                    float num5 = ((list.ey - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                    this.gfx.addThickLinePoint(num4, num5);
                    this.gfx.drawThickLines(true);
                }
            }
        }

        public void drawPeople(RectangleF screenRect)
        {
            SparseArray array = new SparseArray();
            SparseArray array2 = new SparseArray();
            float num = (((float) this.m_worldScale) / 28f) / 0.6f;
            if (num < 0.1f)
            {
                num = 0.1f;
            }
            if (num > 1f)
            {
                num = 1f;
            }
            WorldMapFilter worldMapFilter = GameEngine.Instance.World.worldMapFilter;
            foreach (LocalPerson person in this.personArray)
            {
                if (((person.person.state <= 0) || !person.isVisible(screenRect)) || !worldMapFilter.showPeople(person))
                {
                    continue;
                }
                if (((((person.person.state == 1) || (person.person.state == 11)) || ((person.person.state == 0x15) || (person.person.state == 0x1f))) || ((person.person.state == 50) || (person.person.state == 0x4b))) && (person.parentPerson == -1L))
                {
                    int num2 = 0;
                    if (!this.isUserVillage(person.person.homeVillageID))
                    {
                        num2 = 1;
                    }
                    int num3 = 2 + num2;
                    if (person.person.personType == 100)
                    {
                        num3 = 0x8e;
                    }
                    this.villageSprite.PosX = ((((float) person.displayX) - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                    this.villageSprite.PosY = ((((float) person.displayY) - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                    this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                    this.villageSprite.SpriteNo = num3;
                    this.villageSprite.Center = new PointF(44f, 44f);
                    this.villageSprite.RotationAngle = SpriteWrapper.getFacing(person.BasePoint(), person.TargetPoint());
                    this.villageSprite.Scale = num;
                    this.villageSprite.Update();
                    this.villageSprite.DrawAndClear();
                    switch (person.person.personType)
                    {
                        case 4:
                            num3 = 0x12 + num2;
                            break;

                        case 100:
                            num3 = 0xad;
                            break;
                    }
                    this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                    this.villageSprite.SpriteNo = num3;
                    this.villageSprite.Center = new PointF(44f, 44f);
                    this.villageSprite.Scale = num;
                    this.villageSprite.Update();
                    this.villageSprite.DrawAndClear();
                    continue;
                }
                if (person.person.state == 2)
                {
                    int targetVillageID = person.person.targetVillageID;
                    CapitalPeopleGFX egfx = null;
                    if (array[targetVillageID] == null)
                    {
                        egfx = new CapitalPeopleGFX {
                            posX = ((((float) person.displayX) - screenRect.Left) / screenRect.Width) * this.m_screenWidth,
                            posY = ((((float) person.displayY) - screenRect.Top) / screenRect.Height) * this.m_screenHeight
                        };
                        array[targetVillageID] = egfx;
                    }
                    else
                    {
                        egfx = (CapitalPeopleGFX) array[targetVillageID];
                    }
                    if (!this.isUserVillage(person.person.homeVillageID))
                    {
                        egfx.numOthers++;
                    }
                    else
                    {
                        egfx.numYours++;
                    }
                }
                else if ((person.person.state == 12) || (person.person.state == 0x16))
                {
                    int num5 = person.person.targetVillageID;
                    CapitalPeopleGFX egfx2 = null;
                    if (array2[num5] == null)
                    {
                        egfx2 = new CapitalPeopleGFX {
                            posX = ((((float) person.displayX) - screenRect.Left) / screenRect.Width) * this.m_screenWidth,
                            posY = ((((float) person.displayY) - screenRect.Top) / screenRect.Height) * this.m_screenHeight
                        };
                        array2[num5] = egfx2;
                    }
                    else
                    {
                        egfx2 = (CapitalPeopleGFX) array2[num5];
                    }
                    if (person.person.state == 12)
                    {
                        egfx2.numYours++;
                    }
                    else
                    {
                        egfx2.numOthers++;
                    }
                }
            }
        }

        public bool drawProvinceBorder(RectangleF screenRect, WorldPointList wpl, float scale, Color col)
        {
            if (!this.LinelessMaps)
            {
                if (!wpl.isVisible(screenRect))
                {
                    return false;
                }
                int length = wpl.borderList.Length;
                if (length > 1)
                {
                    this.gfx.startThickLine(col, scale);
                    this.gfx.setThickLineRadius((float) this.m_worldScale);
                    for (int i = 0; i < length; i++)
                    {
                        if (wpl.borderList[i] == -1)
                        {
                            this.gfx.drawThickLines(true);
                            this.gfx.startThickLine(col, scale);
                            this.gfx.setThickLineRadius((float) this.m_worldScale);
                        }
                        else
                        {
                            WorldPoint point = this.pointList[wpl.borderList[i]];
                            float num3 = ((point.x - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                            float num4 = ((point.y - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                            this.gfx.addThickLinePoint(num3, num4);
                        }
                    }
                    this.gfx.drawThickLines(true);
                }
            }
            return true;
        }

        public void drawProvinceBorders(RectangleF screenRect, bool thickBorders, bool political)
        {
            Color green = ARGBColors.Green;
            if (!political)
            {
                green = ARGBColors.Black;
            }
            float scale = (((float) this.m_worldScale) - 2f) / 3f;
            if (scale < 1f)
            {
                scale = 1f;
            }
            if (thickBorders)
            {
                scale *= 3.5f;
            }
            else
            {
                scale *= 2f;
            }
            double num2 = 0.5;
            double num3 = 0.22;
            if (this.EUMap)
            {
                num2 = 1.5;
                num3 = 0.5;
            }
            if (this.playingProvinces)
            {
                green = ARGBColors.Black;
            }
            foreach (WorldPointList list in this.provincesList)
            {
                if ((this.drawProvinceBorder(screenRect, list, scale, green) && (this.m_worldScale > num3)) && ((list.marker.X >= 0) && (this.m_worldScale <= num2)))
                {
                    float x = ((list.marker.X - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                    float num5 = ((list.marker.Y - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                    if (this.smallMapFont)
                    {
                        this.addText(list.areaName, new PointF(x, num5 + this.yMarkerOffset), ARGBColors.Black, true, 0, true);
                    }
                    else
                    {
                        this.addText(list.areaName, new PointF(x, num5 + this.yMarkerOffset), ARGBColors.Black, true, 1, true);
                    }
                }
            }
            if (!this.drawFakeProvinceBorders)
            {
                foreach (IslandInfoList list2 in this.islandList)
                {
                    WorldPointList wpl = this.countyList[list2.county];
                    this.drawProvinceBorder(screenRect, wpl, scale, green);
                }
            }
            this.gfx.renderLines();
        }

        public void drawProvincePoly(RectangleF screenRect)
        {
            this.gfx.startPoly();
            foreach (WorldPointList list in this.provincesList)
            {
                int areaCol = this.getVillageColour(list);
                Color baseColor = this.getAreaColour(areaCol, list);
                if (this.GeographicalMap)
                {
                    float num2 = 255f;
                    float num3 = ((float) this.m_worldZoom) / 17.5f;
                    if (num3 < 1f)
                    {
                        num2 *= num3;
                    }
                    if ((areaCol == 0) && (this.m_worldScale >= 23.899999998509884))
                    {
                        continue;
                    }
                    baseColor = Color.FromArgb((int) num2, baseColor);
                }
                this.drawAreaPoly(screenRect, list, baseColor);
            }
            foreach (IslandInfoList list2 in this.islandList)
            {
                WorldPointList wpl = this.provincesList[list2.province];
                int num4 = this.getVillageColour(wpl);
                Color color2 = this.getAreaColour(num4, wpl);
                if (this.GeographicalMap)
                {
                    float num5 = 255f;
                    float num6 = ((float) this.m_worldZoom) / 17.5f;
                    if (num6 < 1f)
                    {
                        num5 *= num6;
                    }
                    if ((num4 == 0) && (this.m_worldScale >= 23.899999998509884))
                    {
                        continue;
                    }
                    color2 = Color.FromArgb((int) num5, color2);
                }
                WorldPointList list4 = this.countyList[list2.county];
                this.drawAreaPoly(screenRect, list4, color2);
            }
            this.gfx.drawPoly();
        }

        public void drawProvincePolyPlayback(RectangleF screenRect)
        {
            this.gfx.startPoly();
            int num = 0;
            foreach (WorldPointList list in this.provincesList)
            {
                int index = this.getPlaybackProvinceHouse(this.playbackDay, num++);
                Color col = areaColorList[index];
                this.drawAreaPoly(screenRect, list, col);
            }
            foreach (IslandInfoList list2 in this.islandList)
            {
                Color color2 = areaColorList[this.getPlaybackProvinceHouse(this.playbackDay, list2.province)];
                WorldPointList wpl = this.countyList[list2.county];
                this.drawAreaPoly(screenRect, wpl, color2);
            }
            this.gfx.drawPoly();
        }

        public void drawRangeCircle(RectangleF screenRect)
        {
            if (((InterfaceMgr.Instance.OwnSelectedVillage >= 0) || (InterfaceMgr.Instance.SelectedVassalVillage >= 0)) && ((this.isSpecial(InterfaceMgr.Instance.SelectedVillage) && (this.getSpecial(InterfaceMgr.Instance.SelectedVillage) != 0x15)) && (this.getSpecial(InterfaceMgr.Instance.SelectedVillage) != 20)))
            {
                int ownSelectedVillage = InterfaceMgr.Instance.OwnSelectedVillage;
                if (InterfaceMgr.Instance.SelectedVassalVillage >= 0)
                {
                    ownSelectedVillage = InterfaceMgr.Instance.SelectedVassalVillage;
                }
                VillageData data = this.villageList[ownSelectedVillage];
                this.drawRangeCircle((PointF) new Point(data.x, data.y), (float) CardTypes.adjustScoutingHonourRange(this.UserCardData, GameEngine.Instance.LocalWorldData.BaseScoutHonourRange), screenRect);
            }
        }

        public void drawRangeCircle(PointF centre, float radius, RectangleF screenRect)
        {
            int num = (int) (((double) ((radius * 2f) * 10f)) / (this.m_worldZoom + 10.0));
            if (num < 0x20)
            {
                num = 0x20;
            }
            float num6 = 6.28318f / ((float) num);
            float num7 = 0f;
            float num8 = 0f;
            Color color = Color.FromArgb(80, 0xff, 0, 0);
            for (int i = -1; i < num; i++)
            {
                float num5 = i * num6;
                float num3 = centre.X + ((float) (radius * Math.Cos((double) num5)));
                float num4 = centre.Y - ((float) (radius * Math.Sin((double) num5)));
                if (i >= 0)
                {
                    this.addCircleTriangle(screenRect, num7, num8, num3, num4, centre, color);
                    this.addCircleTriangle(screenRect, num3, num4, num7, num8, centre, color);
                }
                num7 = num3;
                num8 = num4;
            }
            this.gfx.drawPoly();
        }

        public void drawRegions(RectangleF screenRect)
        {
            this.gfx.startPoly();
            int num = -1;
            foreach (WorldPointList list in this.regionList)
            {
                num++;
                int areaCol = this.getVillageColour(list);
                Color baseColor = this.getAreaColour(areaCol, list);
                if (list.isVisible(screenRect))
                {
                    int length = list.triangleList.Length;
                    if (length > 0)
                    {
                        float num4 = 255f;
                        float num5 = ((float) (27.0 - this.m_worldZoom)) - 5f;
                        if (num5 < 1f)
                        {
                            num4 *= num5;
                        }
                        if (this.GeographicalMap)
                        {
                            num5 = ((float) this.m_worldZoom) / 8f;
                            if (num5 < 1f)
                            {
                                num4 *= num5;
                            }
                            if ((areaCol == 0) && (this.m_worldScale >= 23.899999998509884))
                            {
                                continue;
                            }
                        }
                        Color col = Color.FromArgb((int) num4, baseColor);
                        for (int i = 0; i < length; i++)
                        {
                            float num7 = list.triangleList[i].x1;
                            float num8 = list.triangleList[i].x2;
                            float num9 = list.triangleList[i].x3;
                            float num10 = list.triangleList[i].y1;
                            float num11 = list.triangleList[i].y2;
                            float num12 = list.triangleList[i].y3;
                            num7 = ((num7 - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                            num8 = ((num8 - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                            num9 = ((num9 - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                            num10 = ((num10 - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                            num11 = ((num11 - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                            num12 = ((num12 - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                            this.gfx.addTriangle(col, num7, num10, num8, num11, num9, num12);
                        }
                    }
                }
            }
            this.gfx.drawPoly();
        }

        public void drawRegionsBorder(RectangleF screenRect, bool forcedDraw)
        {
            Color darkGreen = ARGBColors.DarkGreen;
            float num = 255f;
            float num2 = ((float) (27.0 - this.m_worldZoom)) - 5f;
            if (num2 < 0f)
            {
                num2 = 0f;
            }
            if (num2 < 1f)
            {
                num *= num2;
                darkGreen = Color.FromArgb((int) num, darkGreen);
            }
            int num3 = -1;
            foreach (WorldPointList list in this.regionList)
            {
                num3++;
                if (list.isVisible(screenRect))
                {
                    int parentID = list.parentID;
                    if (((parentID < 0) || forcedDraw) || ((this.getHouse(list.factionID) == this.getHouse(this.countyList[parentID].factionID)) || this.GeographicalMap))
                    {
                        int length = list.regionBorderList.Length;
                        if (length > 1)
                        {
                            this.gfx.startThickLine(darkGreen, 1f);
                            this.gfx.setThickLineRadius((float) this.m_worldScale);
                            for (int i = 0; i < length; i++)
                            {
                                WorldPoint point = list.regionBorderList[i];
                                if ((point.x == 0f) && (point.y == 0f))
                                {
                                    break;
                                }
                                float num7 = ((point.x - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                                float num8 = ((point.y - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                                this.gfx.addThickLinePoint(num7, num8);
                            }
                            this.gfx.drawThickLines(true);
                        }
                    }
                }
            }
            this.gfx.renderLines();
        }

        public void drawSeas(RectangleF screenRect)
        {
            if (!this.GeographicalMap)
            {
                foreach (WorldPointList list in this.seaList)
                {
                    Color sEACOLOR = SEACOLOR;
                    if (list.data == 1)
                    {
                        sEACOLOR = Color.FromArgb(0xff, 0x98, 0xb5, 0x86);
                    }
                    if (list.isVisible(screenRect))
                    {
                        int length = list.triangleList.Length;
                        if (length > 0)
                        {
                            this.gfx.startPoly();
                            for (int i = 0; i < length; i++)
                            {
                                float num3 = list.triangleList[i].x1;
                                float num4 = list.triangleList[i].x2;
                                float num5 = list.triangleList[i].x3;
                                float num6 = list.triangleList[i].y1;
                                float num7 = list.triangleList[i].y2;
                                float num8 = list.triangleList[i].y3;
                                num3 = ((num3 - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                                num4 = ((num4 - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                                num5 = ((num5 - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                                num6 = ((num6 - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                                num7 = ((num7 - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                                num8 = ((num8 - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                                this.gfx.addTriangle(sEACOLOR, num3, num6, num4, num7, num5, num8);
                            }
                            this.gfx.drawPoly();
                        }
                    }
                }
            }
        }

        public void drawSurroundBox(RectangleF screenRect, Color col, float x1, float y1, float x2, float y2)
        {
            x1 = ((x1 - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
            x2 = ((x2 - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
            y1 = ((y1 - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
            y2 = ((y2 - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
            this.gfx.addTriangle(col, x1, y1, x2, y1, x1, y2);
            this.gfx.addTriangle(col, x2, y1, x2, y2, x1, y2);
        }

        public void drawText()
        {
            this.gfx.startPoly();
            foreach (MapText text in this.textDrawList)
            {
                if (text.bordered && Program.mySettings.UseMapTextBorders)
                {
                    Rectangle rectangle = this.gfx.getTextSize(text.text, text.size);
                    float num = (text.loc.X - (rectangle.Width / 2)) - 5f;
                    float num2 = (text.loc.X + (rectangle.Width / 2)) + 3f;
                    float num3 = text.loc.Y - 2f;
                    float num4 = text.loc.Y + rectangle.Height;
                    if (text.size == 0)
                    {
                        num3 += 2f;
                    }
                    this.gfx.addTriangle(Color.FromArgb(0x90, 0xff, 0xff, 0xff), num, num3 + 2f, num2, num3 + 2f, num, num4 - 2f);
                    this.gfx.addTriangle(Color.FromArgb(0x90, 0xff, 0xff, 0xff), num2, num3 + 2f, num2, num4 - 2f, num, num4 - 2f);
                    this.gfx.addTriangle(Color.FromArgb(0x90, 0xff, 0xff, 0xff), num + 1f, num3 + 1f, num2 - 1f, num3 + 1f, num + 1f, num3 + 2f);
                    this.gfx.addTriangle(Color.FromArgb(0x90, 0xff, 0xff, 0xff), num2 - 1f, num3 + 1f, num2 - 1f, num3 + 2f, num + 1f, num3 + 2f);
                    this.gfx.addTriangle(Color.FromArgb(0x90, 0xff, 0xff, 0xff), num + 1f, num4 - 2f, num2 - 1f, num4 - 2f, num + 1f, num4 - 1f);
                    this.gfx.addTriangle(Color.FromArgb(0x90, 0xff, 0xff, 0xff), num2 - 1f, num4 - 2f, num2 - 1f, num4 - 1f, num + 1f, num4 - 1f);
                    this.gfx.addTriangle(Color.FromArgb(0x90, 0xff, 0xff, 0xff), num + 2f, num3, num2 - 2f, num3, num + 2f, num3 + 1f);
                    this.gfx.addTriangle(Color.FromArgb(0x90, 0xff, 0xff, 0xff), num2 - 2f, num3, num2 - 2f, num3 + 1f, num + 2f, num3 + 1f);
                    this.gfx.addTriangle(Color.FromArgb(0x90, 0xff, 0xff, 0xff), num + 2f, num4 - 1f, num2 - 2f, num4 - 1f, num + 2f, num4);
                    this.gfx.addTriangle(Color.FromArgb(0x90, 0xff, 0xff, 0xff), num2 - 2f, num4 - 1f, num2 - 2f, num4, num + 2f, num4);
                }
            }
            this.gfx.drawPoly();
            foreach (MapText text2 in this.textDrawList)
            {
                this.gfx.drawText(text2.text, text2.loc.X, text2.loc.Y, text2.col, text2.centered, text2.size);
            }
            this.textDrawList.Clear();
        }

        public void drawTraders(RectangleF screenRect)
        {
            this.tradingVillageList.Clear();
            this.marketTradingVillageList.Clear();
            float num = (((float) this.m_worldScale) / 28f) / 0.6f;
            if (num < 0.1f)
            {
                num = 0.1f;
            }
            if (num > 1f)
            {
                num = 1f;
            }
            WorldMapFilter worldMapFilter = GameEngine.Instance.World.worldMapFilter;
            foreach (LocalTrader trader in this.traderArray)
            {
                if ((trader.trader.traderState <= 0) || (trader.parentTrader != -1L))
                {
                    continue;
                }
                switch (trader.trader.traderState)
                {
                    case 1:
                    case 2:
                        this.tradingVillageList.Add(trader.trader.targetVillageID);
                        this.tradingVillageList.Add(trader.trader.homeVillageID);
                        break;

                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        this.marketTradingVillageList.Add(trader.trader.targetVillageID);
                        this.marketTradingVillageList.Add(trader.trader.homeVillageID);
                        break;
                }
                if (trader.isVisible(screenRect) && worldMapFilter.showTrader(trader))
                {
                    int num2 = 0;
                    if (!this.isUserVillage(trader.trader.homeVillageID))
                    {
                        num2 = 1;
                    }
                    int num3 = 2 + num2;
                    this.villageSprite.PosX = ((((float) trader.displayX) - screenRect.Left) / screenRect.Width) * this.m_screenWidth;
                    this.villageSprite.PosY = ((((float) trader.displayY) - screenRect.Top) / screenRect.Height) * this.m_screenHeight;
                    this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                    this.villageSprite.SpriteNo = num3;
                    this.villageSprite.Center = new PointF(44f, 44f);
                    this.villageSprite.RotationAngle = SpriteWrapper.getFacing(trader.BasePoint(), trader.TargetPoint());
                    this.villageSprite.Scale = num;
                    this.villageSprite.Update();
                    this.villageSprite.DrawAndClear();
                    num3 = 10 + num2;
                    this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                    this.villageSprite.SpriteNo = num3;
                    this.villageSprite.Center = new PointF(44f, 44f);
                    this.villageSprite.Scale = num;
                    this.villageSprite.Update();
                    this.villageSprite.DrawAndClear();
                }
            }
        }

        public void drawVillage(VillageData village, double xPos, double yPos)
        {
            float num10;
            if (!this.worldMapFilter.showVillage(village))
            {
                return;
            }
            if (InterfaceMgr.Instance.WorldMapMode == 1)
            {
                if (this.isSpecial(village.id))
                {
                    return;
                }
                if (!village.Capital && (village.userID < 0))
                {
                    return;
                }
            }
            if (InterfaceMgr.Instance.WorldMapMode == 2)
            {
                if (!village.Capital && ((village.id != InterfaceMgr.Instance.StockExchangeBuyingVillage) || (InterfaceMgr.Instance.StockExchangeBuyingVillage < 0)))
                {
                    return;
                }
                if ((village.id != InterfaceMgr.Instance.StockExchangeBuyingVillage) && !this.allowExchangeTrade(village.id, InterfaceMgr.Instance.StockExchangeBuyingVillage))
                {
                    return;
                }
            }
            if (((InterfaceMgr.Instance.WorldMapMode == 5) || (InterfaceMgr.Instance.WorldMapMode == 7)) && village.Capital)
            {
                return;
            }
            if (InterfaceMgr.Instance.WorldMapMode == 3)
            {
                if (this.isSpecial(village.id) && !this.isAttackableSpecial(village.id))
                {
                    return;
                }
                if ((!this.isSpecial(village.id) && (village.userID < 0)) && !village.Capital)
                {
                    return;
                }
            }
            if (InterfaceMgr.Instance.WorldMapMode == 7)
            {
                if (this.isSpecial(village.id) && !this.isAttackableSpecial(village.id))
                {
                    return;
                }
                if (!this.isSpecial(village.id) && (village.userID < 0))
                {
                    return;
                }
            }
            if ((InterfaceMgr.Instance.WorldMapMode == 5) && ((this.isSpecial(village.id) || village.Capital) || (village.userID < 0)))
            {
                return;
            }
            if ((InterfaceMgr.Instance.WorldMapMode == 6) && (this.isSpecial(village.id) || village.Capital))
            {
                return;
            }
            if (InterfaceMgr.Instance.WorldMapMode == 4)
            {
                if (this.isSpecial(village.id) && !this.isScoutableSpecial(village.id))
                {
                    return;
                }
                if ((!this.isSpecial(village.id) && (village.userID < 0)) && !this.isCapital(village.id))
                {
                    return;
                }
            }
            if ((InterfaceMgr.Instance.WorldMapMode == 9) && (this.isSpecial(village.id) || ((village.userID < 0) && !village.Capital)))
            {
                return;
            }
            bool flag = false;
            float scale = 1f;
            int index = 0;
            int worldMapIconsTexID = GFXLibrary.Instance.WorldMapIconsTexID;
            int num4 = -1;
            bool flag2 = false;
            bool flag3 = false;
            Color white = ARGBColors.White;
            Color color2 = Color.FromArgb(0x80, 0xff, 0xff, 0xc0);
            bool flag4 = false;
            string text = "";
            bool flag5 = true;
            if ((village.userID >= 0) && (this.getHouse(village.factionID) > 0))
            {
                flag4 = true;
            }
            double num5 = 27.0 - this.m_worldZoom;
            int num6 = 30;
            int num7 = 110;
            bool flag6 = false;
            if (village.countryCapital)
            {
                flag = true;
                index = this.getVillageColour(village);
                white = this.getColorFromArray(villageColorList, index);
                flag3 = true;
                scale = ((float) this.m_worldScale) / 8.5f;
                index = 0x3a;
                if (scale < 0.15f)
                {
                    scale = 0.15f;
                }
                if (scale > 1f)
                {
                    scale = 1f;
                }
                if (this.m_worldScale > 3.0)
                {
                    text = village.villageName;
                }
                goto Label_09F7;
            }
            if (village.provinceCapital)
            {
                flag = true;
                index = this.getVillageColour(village);
                white = this.getColorFromArray(villageColorList, index);
                flag3 = true;
                scale = ((float) this.m_worldScale) / 8.5f;
                index = 0x39;
                if (scale < 0.15f)
                {
                    scale = 0.15f;
                }
                if (scale > 1f)
                {
                    scale = 1f;
                }
                if (this.m_worldScale > 3.0)
                {
                    text = village.villageName;
                }
                goto Label_09F7;
            }
            if (village.countyCapital)
            {
                if (num5 >= 4.0)
                {
                    flag = true;
                    index = this.getVillageColour(village);
                    white = this.getColorFromArray(villageColorList, index);
                    flag3 = true;
                    scale = ((float) this.m_worldScale) / 11.33333f;
                    index = 0x38;
                    if (scale < 0.1f)
                    {
                        scale = 0.1f;
                    }
                    if (scale > 1f)
                    {
                        scale = 1f;
                    }
                    if (this.m_worldScale > 3.0)
                    {
                        text = village.villageName;
                    }
                }
                goto Label_09F7;
            }
            if (village.regionCapital)
            {
                if ((num5 >= 6.0) || this.mapEditing)
                {
                    flag = true;
                    index = this.getVillageColour(village);
                    white = this.getColorFromArray(villageColorList, index);
                    flag3 = true;
                    scale = ((float) this.m_worldScale) / 17f;
                    index = 0x37;
                    if (scale < 0.1f)
                    {
                        scale = 0.1f;
                    }
                    if (scale > 1f)
                    {
                        scale = 1f;
                    }
                    if (this.m_worldScale > 5.0)
                    {
                        text = village.villageName;
                    }
                }
                goto Label_09F7;
            }
            if (village.special != 30)
            {
                if (num5 >= 8.0)
                {
                    if ((this.m_worldScale < 11.0) && ((((village.special < 100) || (village.special > 0xc7)) && (!SpecialVillageTypes.IS_TREASURE_CASTLE(village.special) && (village.special != 30))) || (num5 < 6.0)))
                    {
                        this.gfx.endSprites();
                        this.gfx.drawLine(ARGBColors.Black, ((float) xPos) * this.m_screenWidth, ((float) yPos) * this.m_screenHeight, (((float) xPos) * this.m_screenWidth) + 1f, ((float) yPos) * this.m_screenHeight);
                    }
                    else
                    {
                        flag = true;
                        scale = (((float) this.m_worldScale) - 8.5f) / 8.5f;
                        if (scale > 1f)
                        {
                            scale = 1f;
                        }
                        if (village.special == 0)
                        {
                            index = this.getVillageColour(village);
                            white = this.getColorFromArray(villageColorList, index);
                            index = this.fixupVillageSprites(index);
                            worldMapIconsTexID = GFXLibrary.Instance.WorldMapIconsTexID;
                            flag2 = true;
                        }
                        else if ((village.special >= 100) && (village.special <= 0xc7))
                        {
                            num4 = GFXLibrary.getCommodity32GFXno(village.special - 100);
                            if (num4 < 0)
                            {
                                if (this.xmasPresents)
                                {
                                    index = 400;
                                }
                                else
                                {
                                    index = 0x7c;
                                }
                            }
                            else
                            {
                                index = -1;
                            }
                            scale = 1f;
                        }
                        else if (village.special == 30)
                        {
                            scale = 1f;
                            index = 0x3b;
                            flag2 = true;
                            white = ARGBColors.White;
                        }
                        else if (village.special == 3)
                        {
                            index = 0x3b;
                            flag2 = true;
                            white = ARGBColors.White;
                        }
                        else if (village.special == 4)
                        {
                            index = 0x3d;
                            flag2 = true;
                            white = ARGBColors.White;
                        }
                        else if (village.special == 5)
                        {
                            index = 60;
                            flag2 = true;
                            white = ARGBColors.White;
                        }
                        else if (village.special == 6)
                        {
                            index = 0x3e;
                            flag2 = true;
                            white = ARGBColors.White;
                        }
                        else if (((village.special == 7) || (village.special == 9)) || ((village.special == 11) || (village.special == 13)))
                        {
                            if (GameEngine.Instance.LocalWorldData.AIWorld)
                            {
                                white = ARGBColors.Black;
                                flag6 = true;
                                flag2 = true;
                            }
                            else
                            {
                                index = 0x3f;
                                flag2 = true;
                                white = ARGBColors.White;
                            }
                        }
                        else if (((village.special == 8) || (village.special == 10)) || ((village.special == 12) || (village.special == 14)))
                        {
                            index = 0x40;
                            flag2 = true;
                            white = ARGBColors.White;
                        }
                        else if ((village.special == 15) || (village.special == 0x11))
                        {
                            index = 0x184;
                            flag2 = true;
                            white = ARGBColors.White;
                            num7 = -214;
                        }
                        else if ((village.special == 0x10) || (village.special == 0x12))
                        {
                            index = 0x185;
                            flag2 = true;
                            white = ARGBColors.White;
                            num7 = -214;
                        }
                        else if ((village.special >= 0x29) && (village.special <= 50))
                        {
                            index = 390;
                            flag2 = true;
                            white = ARGBColors.White;
                            num7 = -216;
                            scale = 1f;
                        }
                        else if ((village.special >= 0x33) && (village.special <= 60))
                        {
                            index = 0x188;
                            flag2 = true;
                            white = ARGBColors.White;
                            num7 = -218;
                            scale = 1f;
                        }
                        else if ((village.special >= 0x3d) && (village.special <= 70))
                        {
                            index = 0x18a;
                            flag2 = true;
                            white = ARGBColors.White;
                            num7 = -220;
                            scale = 1f;
                        }
                        else if ((village.special >= 0x47) && (village.special <= 80))
                        {
                            index = 0x18c;
                            flag2 = true;
                            white = ARGBColors.White;
                            num7 = -222;
                            scale = 1f;
                        }
                        else if ((village.special >= 0x51) && (village.special <= 90))
                        {
                            index = 0x18e;
                            flag2 = true;
                            white = ARGBColors.White;
                            num7 = -224;
                            scale = 1f;
                        }
                        else if (village.special == 40)
                        {
                            index = 0x187;
                            flag2 = true;
                            white = ARGBColors.White;
                            num7 = -216;
                        }
                        else
                        {
                            if (village.special == 20)
                            {
                                return;
                            }
                            if (village.special == 0x15)
                            {
                                index = 0x178;
                                flag2 = true;
                                white = ARGBColors.White;
                                num6 = 2;
                                num7 = 4;
                            }
                            else if (village.special == 2)
                            {
                                village.visible = false;
                            }
                        }
                    }
                }
                goto Label_09F7;
            }
            flag5 = false;
            flag = true;
            if (this.m_worldScale > 0.67)
            {
                scale = 1f;
            }
            else
            {
                scale = (float) (this.m_worldScale / 0.66);
            }
            switch (this.getAIInvasionMarkerState(village.id))
            {
                case 0:
                    index = 0x1a2;
                    goto Label_05A2;

                case 2:
                {
                    this.gfx.beginSprites();
                    this.villageSprite.PosX = ((float) xPos) * this.m_screenWidth;
                    this.villageSprite.PosY = ((float) yPos) * this.m_screenHeight;
                    int pulseValue = this.pulseValue;
                    this.villageSprite.Scale = scale;
                    this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                    this.villageSprite.SpriteNo = 420;
                    this.villageSprite.ColorToUse = Color.FromArgb(pulseValue, ARGBColors.Yellow);
                    this.villageSprite.Update();
                    this.villageSprite.DrawAndClear_NoCenter();
                    this.villageSprite.ColorToUse = ARGBColors.White;
                    break;
                }
            }
            index = 0x1a3;
        Label_05A2:
            flag2 = true;
            white = ARGBColors.White;
        Label_09F7:
            num10 = scale;
            this.villageSprite.PosX = ((float) xPos) * this.m_screenWidth;
            this.villageSprite.PosY = ((float) yPos) * this.m_screenHeight;
            if (flag)
            {
                this.gfx.beginSprites();
                if ((((InterfaceMgr.Instance.SelectedVillage == village.id) || (InterfaceMgr.Instance.OwnSelectedVillage == village.id)) || (InterfaceMgr.Instance.SelectedVassalVillage == village.id)) && ((!flag2 && !flag3) && flag5))
                {
                    this.villageSprite.Scale = num10;
                    this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                    if ((InterfaceMgr.Instance.OwnSelectedVillage == village.id) || (InterfaceMgr.Instance.SelectedVassalVillage == village.id))
                    {
                        this.villageSprite.SpriteNo = 0x22;
                    }
                    else
                    {
                        this.villageSprite.SpriteNo = 30;
                    }
                    if (flag2)
                    {
                        this.villageSprite.SpriteNo += 2;
                        this.villageSprite.Center = new PointF(44f, 47f);
                    }
                    else
                    {
                        if (village.regionCapital)
                        {
                            this.villageSprite.SpriteNo++;
                        }
                        if (village.countyCapital)
                        {
                            this.villageSprite.SpriteNo += 2;
                        }
                        if (village.provinceCapital)
                        {
                            this.villageSprite.SpriteNo += 3;
                        }
                        if (village.countryCapital)
                        {
                            this.villageSprite.SpriteNo += 3;
                        }
                    }
                    this.villageSprite.Update();
                    this.villageSprite.DrawAndClear();
                }
                if (!village.Capital)
                {
                    Color color3 = ARGBColors.White;
                    if ((village.special == 0) || flag2)
                    {
                        color2 = Color.FromArgb(0xc0, white);
                        if (white == ARGBColors.White)
                        {
                            color2 = Color.FromArgb(0x80, white);
                        }
                        this.villageSprite.Scale = num10;
                        int num11 = 0;
                        int num12 = 0x23;
                        if (flag6)
                        {
                            num11 = Math.Min((village.villageInfo * 3) + 4, 0x13);
                        }
                        else if (village.special == 0)
                        {
                            num11 = Math.Min(village.villageInfo / 6, 0x13);
                        }
                        else
                        {
                            num12 = index;
                        }
                        if ((village.special == 0) || flag6)
                        {
                            if (flag4)
                            {
                                this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                                this.villageSprite.SpriteNo = (num12 + num6) + num11;
                                this.villageSprite.ColorToUse = color2;
                                this.villageSprite.Update();
                                this.villageSprite.DrawAndClear_NoCenter();
                            }
                            else if (flag6)
                            {
                                color2 = Color.FromArgb(0xff, 0x29, 0x29, 0x30);
                                this.villageSprite.Scale = num10;
                                this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                                this.villageSprite.SpriteNo = (num12 + num6) + num11;
                                this.villageSprite.ColorToUse = color2;
                                this.villageSprite.Update();
                                this.villageSprite.DrawAndClear_NoCenter();
                                this.villageSprite.ColorToUse = ARGBColors.White;
                                color3 = Color.FromArgb(0xff, 0xe4, 0xe4);
                                color2 = Color.FromArgb(0xff, 0xff, 0, 0);
                                this.villageSprite.Scale = num10;
                                this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                                if ((num12 == 0x3f) || (num12 == 0x40))
                                {
                                    this.villageSprite.SpriteNo = ((num12 + num7) + num11) + 1;
                                }
                                else
                                {
                                    this.villageSprite.SpriteNo = (num12 + num7) + num11;
                                }
                                this.villageSprite.ColorToUse = color2;
                                this.villageSprite.Update();
                                this.villageSprite.DrawAndClear_NoCenter();
                                this.villageSprite.ColorToUse = ARGBColors.White;
                                color3 = Color.FromArgb(0xff, 0xe4, 0xe4);
                            }
                            this.villageSprite.ColorToUse = ARGBColors.White;
                        }
                        if ((InterfaceMgr.Instance.OwnSelectedVillage == village.id) || (InterfaceMgr.Instance.SelectedVassalVillage == village.id))
                        {
                            color2 = Color.FromArgb(this.pulseValue, ARGBColors.Yellow);
                            this.villageSprite.Scale = num10;
                            this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                            this.villageSprite.SpriteNo = (num12 + num7) + num11;
                            this.villageSprite.ColorToUse = color2;
                            this.villageSprite.Update();
                            this.villageSprite.DrawAndClear_NoCenter();
                            this.villageSprite.ColorToUse = ARGBColors.White;
                            color3 = Color.FromArgb(0xff, 0xff, 0xc0);
                        }
                        else if (InterfaceMgr.Instance.SelectedVillage == village.id)
                        {
                            color2 = Color.FromArgb(this.pulseValue, 0x40, 0xff, 0x40);
                            this.villageSprite.Scale = num10;
                            this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                            if ((village.userID < 0) && ((village.special == 0) || (village.special == 30)))
                            {
                                this.villageSprite.SpriteNo = 0x41;
                            }
                            else if ((num12 == 0x3f) || (num12 == 0x40))
                            {
                                this.villageSprite.SpriteNo = ((num12 + num7) + num11) + 1;
                            }
                            else
                            {
                                this.villageSprite.SpriteNo = (num12 + num7) + num11;
                            }
                            this.villageSprite.ColorToUse = color2;
                            this.villageSprite.Update();
                            this.villageSprite.DrawAndClear_NoCenter();
                            this.villageSprite.ColorToUse = ARGBColors.White;
                            color3 = Color.FromArgb(0xc0, 0xff, 0xc0);
                        }
                        this.villageSprite.Scale = num10;
                        if ((village.userID >= 0) || (village.special != 0))
                        {
                            this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                            this.villageSprite.SpriteNo = num12 + num11;
                        }
                        else
                        {
                            this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                            this.villageSprite.SpriteNo = 0x191;
                        }
                        this.villageSprite.ColorToUse = color3;
                        this.villageSprite.Update();
                        this.villageSprite.DrawAndClear();
                        if (this.drawDebugNames)
                        {
                            this.addText(village.id.ToString(), new PointF(this.villageSprite.PosX, this.villageSprite.PosY + (10f * num10)), ARGBColors.Black, true, 1);
                        }
                        if (this.drawDebugVillageNames)
                        {
                            this.addText(village.villageName, new PointF(this.villageSprite.PosX, this.villageSprite.PosY + (10f * num10)), ARGBColors.Black, true, 1);
                        }
                    }
                }
                if ((index >= 0) && !flag2)
                {
                    if (flag3)
                    {
                        color2 = Color.FromArgb(0xc0, white);
                        if (white == ARGBColors.White)
                        {
                            color2 = Color.FromArgb(0x80, white);
                        }
                        if (flag4)
                        {
                            this.villageSprite.TextureID = worldMapIconsTexID;
                            this.villageSprite.SpriteNo = index + num6;
                            this.villageSprite.ColorToUse = color2;
                            this.villageSprite.Center = new PointF(75f, 105f);
                            this.villageSprite.Scale = num10;
                            this.villageSprite.Update();
                            this.villageSprite.DrawAndClear_NoCenter();
                        }
                        Color color4 = ARGBColors.White;
                        if ((InterfaceMgr.Instance.OwnSelectedVillage == village.id) || (InterfaceMgr.Instance.SelectedVassalVillage == village.id))
                        {
                            color2 = Color.FromArgb(this.pulseValue, ARGBColors.Yellow);
                            this.villageSprite.TextureID = worldMapIconsTexID;
                            this.villageSprite.SpriteNo = index + num7;
                            this.villageSprite.ColorToUse = color2;
                            this.villageSprite.Center = new PointF(75f, 105f);
                            this.villageSprite.Scale = num10;
                            this.villageSprite.Update();
                            this.villageSprite.DrawAndClear_NoCenter();
                            color4 = Color.FromArgb(0xff, 0xff, 0xc0);
                        }
                        else if (InterfaceMgr.Instance.SelectedVillage == village.id)
                        {
                            color2 = Color.FromArgb(this.pulseValue, 0x40, 0xff, 0x40);
                            this.villageSprite.TextureID = worldMapIconsTexID;
                            this.villageSprite.SpriteNo = index + num7;
                            this.villageSprite.ColorToUse = color2;
                            this.villageSprite.Center = new PointF(75f, 105f);
                            this.villageSprite.Scale = num10;
                            this.villageSprite.Update();
                            this.villageSprite.DrawAndClear_NoCenter();
                            color4 = Color.FromArgb(0xc0, 0xff, 0xc0);
                        }
                        this.villageSprite.TextureID = worldMapIconsTexID;
                        this.villageSprite.SpriteNo = index;
                        this.villageSprite.ColorToUse = color4;
                        this.villageSprite.Center = new PointF(75f, 105f);
                        if (this.drawDebugNames)
                        {
                            if (village.regionCapital)
                            {
                                this.addText("P:" + village.regionID.ToString() + " V:" + village.id.ToString(), new PointF(this.villageSprite.PosX, this.villageSprite.PosY + (30f * num10)), ARGBColors.Black, true, 1, true);
                            }
                            if (village.countyCapital)
                            {
                                this.addText("Cty:" + village.countyID.ToString() + " V:" + village.id.ToString(), new PointF(this.villageSprite.PosX, this.villageSprite.PosY + (30f * num10)), ARGBColors.Black, true, 1, true);
                            }
                            if (village.provinceCapital)
                            {
                                this.addText("Prv:" + this.getCountyProvince(village.countyID).ToString() + " V:" + village.id.ToString(), new PointF(this.villageSprite.PosX, this.villageSprite.PosY + (30f * num10)), ARGBColors.Black, true, 1, true);
                            }
                            if (village.countryCapital)
                            {
                                int provinceID = this.getCountyProvince(village.countyID);
                                this.addText("Ctry:" + this.getProvinceCountry(provinceID).ToString() + " V:" + village.id.ToString(), new PointF(this.villageSprite.PosX, this.villageSprite.PosY + (30f * num10)), ARGBColors.Black, true, 1, true);
                            }
                        }
                        else if (text.Length > 0)
                        {
                            Color black = ARGBColors.Black;
                            this.addText(text, new PointF(this.villageSprite.PosX, this.villageSprite.PosY + (40f * num10)), black, true, 1, true);
                        }
                        this.villageSprite.Scale = num10;
                        this.villageSprite.Update();
                        this.villageSprite.DrawAndClear();
                        int regionID = village.regionID;
                        int num21 = this.getParishPlague(regionID);
                        if (num21 > 0)
                        {
                            this.villageSprite.TextureID = worldMapIconsTexID;
                            if (num21 > 0x85)
                            {
                                this.villageSprite.SpriteNo = 0xb0;
                            }
                            else if (num21 > 0x42)
                            {
                                this.villageSprite.SpriteNo = 0x90;
                            }
                            else
                            {
                                this.villageSprite.SpriteNo = 0x8f;
                            }
                            this.villageSprite.Center = new PointF(61f, 61f);
                            this.villageSprite.Scale = num10;
                            this.villageSprite.Update();
                            this.villageSprite.DrawAndClear();
                        }
                        if (village.numFlags > 0)
                        {
                            this.villageSprite.TextureID = worldMapIconsTexID;
                            this.villageSprite.SpriteNo = 0x1c;
                            this.villageSprite.Center = new PointF(26f, 58f);
                            this.villageSprite.Scale = num10;
                            this.villageSprite.Update();
                            this.villageSprite.DrawAndClear();
                            if (scale == 1f)
                            {
                                Color col = ARGBColors.Black;
                                if (village.whiteFlags && (this.m_worldScale >= 11.0))
                                {
                                    col = Color.FromArgb(240, 240, 240);
                                    this.addText(village.numFlags.ToString(), new PointF((this.villageSprite.PosX + (35f * num10)) + 1f, (this.villageSprite.PosY + (-27f * num10)) + 1f), ARGBColors.Black, false, 1, false);
                                    this.addText(village.numFlags.ToString(), new PointF(this.villageSprite.PosX + (35f * num10), this.villageSprite.PosY + (-27f * num10)), col, false, 1, false);
                                }
                                else
                                {
                                    this.addText(village.numFlags.ToString(), new PointF(this.villageSprite.PosX + (35f * num10), this.villageSprite.PosY + (-27f * num10)), col, false, 1, false);
                                }
                            }
                        }
                    }
                    else
                    {
                        this.villageSprite.TextureID = worldMapIconsTexID;
                        this.villageSprite.SpriteNo = index;
                        this.villageSprite.Scale = num10;
                        this.villageSprite.Update();
                        this.villageSprite.DrawAndClear();
                    }
                }
                if (num4 >= 0)
                {
                    this.gfx.beginSprites();
                    this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                    this.villageSprite.SpriteNo = num4 + 0x5f;
                    this.villageSprite.Scale = num10;
                    this.villageSprite.Update();
                    this.villageSprite.DrawAndClear();
                }
            }
            bool flag7 = false;
            bool force = false;
            int userID = village.userID;
            int num23 = 0;
            int num24 = 0;
            if (userID < 0)
            {
                userID = -10000;
            }
            if (this.isUserVillage(village.id))
            {
                flag7 = true;
                force = true;
            }
            else if (this.isUserRelatedVillage(village.id))
            {
                flag7 = true;
                force = true;
            }
            else if (village.Capital && (this.m_worldScale >= 7.0))
            {
                flag7 = true;
            }
            if (this.m_worldScale >= 11.0)
            {
                if ((this.m_rolloverTargetVillageNoDelay > 0) && (this.m_rolloverTargetVillageNoDelay < this.villageList.Length))
                {
                    if ((this.villageList[this.m_rolloverTargetVillageNoDelay].userID >= 0) && (this.villageList[this.m_rolloverTargetVillageNoDelay].userID == village.userID))
                    {
                        flag7 = true;
                    }
                    else if (!GameEngine.Instance.LocalWorldData.AIWorld)
                    {
                        if ((((this.villageList[this.m_rolloverTargetVillageNoDelay].special == 7) || (this.villageList[this.m_rolloverTargetVillageNoDelay].special == 11)) || ((this.villageList[this.m_rolloverTargetVillageNoDelay].special == 13) || (this.villageList[this.m_rolloverTargetVillageNoDelay].special == 9))) && (((village.special == 7) || (village.special == 11)) || ((village.special == 13) || (village.special == 9))))
                        {
                            flag7 = true;
                            num23 = -1;
                            num24 = -4;
                            if (village.special == 7)
                            {
                                userID = -1;
                            }
                            if (village.special == 9)
                            {
                                userID = -2;
                            }
                            if (village.special == 11)
                            {
                                userID = -3;
                            }
                            if (village.special == 13)
                            {
                                userID = -4;
                            }
                        }
                    }
                    else if ((((this.villageList[this.m_rolloverTargetVillageNoDelay].special == 7) || (this.villageList[this.m_rolloverTargetVillageNoDelay].special == 11)) || ((this.villageList[this.m_rolloverTargetVillageNoDelay].special == 13) || (this.villageList[this.m_rolloverTargetVillageNoDelay].special == 9))) && (village.special == this.villageList[this.m_rolloverTargetVillageNoDelay].special))
                    {
                        flag7 = true;
                        num23 = -1;
                        num24 = -4;
                        if (village.special == 7)
                        {
                            userID = -1;
                        }
                        if (village.special == 9)
                        {
                            userID = -2;
                        }
                        if (village.special == 11)
                        {
                            userID = -3;
                        }
                        if (village.special == 13)
                        {
                            userID = -4;
                        }
                    }
                }
                if ((this.m_userInfoShieldRolloverUserID != -1) && (village.userID == this.m_userInfoShieldRolloverUserID))
                {
                    flag7 = true;
                }
            }
            if (flag7 && (userID > -10000))
            {
                int textureID = this.getWorldShieldTexture(userID, force);
                if (textureID > 0)
                {
                    this.gfx.beginSprites();
                    Color baseColor = ARGBColors.White;
                    if ((village.userID == RemoteServices.Instance.UserID) || this.isUserRelatedVillage(village.id))
                    {
                        this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                        if (village.userID == RemoteServices.Instance.UserID)
                        {
                            this.villageSprite.SpriteNo = 0x182;
                            this.villageSprite.Center = new PointF(33f, 69f);
                            baseColor = Color.FromArgb(0xff, 0xff, 0);
                        }
                        else
                        {
                            this.villageSprite.SpriteNo = 0x183;
                            this.villageSprite.Center = new PointF(33f, 69f);
                            baseColor = Color.FromArgb(0, 0xff, 0);
                        }
                        scale = ((float) this.m_worldScale) / 17f;
                        if (scale < 0.15f)
                        {
                            scale = 0.15f;
                        }
                        if (scale > 1f)
                        {
                            scale = 1f;
                        }
                        if (village.id == InterfaceMgr.Instance.getSelectedMenuVillage())
                        {
                            float num26 = ((float) this.pulse) / 128f;
                            if (num26 > 1f)
                            {
                                num26 = 2f - num26;
                            }
                            baseColor = Color.FromArgb((int) (255f - (255f * (num26 / 2f))), baseColor);
                        }
                        this.villageSprite.ColorToUse = baseColor;
                        this.villageSprite.Scale = scale;
                        this.villageSprite.Update();
                        this.villageSprite.DrawAndClear();
                    }
                    scale = ((float) this.m_worldScale) / 17f;
                    if (scale < 0.15f)
                    {
                        scale = 0.15f;
                    }
                    if (scale > 1f)
                    {
                        scale = 1f;
                    }
                    int width = 0x20;
                    int num28 = 14;
                    if (village.userID == RemoteServices.Instance.UserID)
                    {
                        width = 0x40;
                        num28 = 0x12;
                    }
                    Rectangle srcRect = new Rectangle(0, 0, width, width);
                    Size size = new Size(this.iScale(width, scale), this.iScale(width, scale));
                    PointF renderPos = new PointF(this.villageSprite.PosX - ((num28 - num23) * scale), this.villageSprite.PosY - (((0x24 + num28) - num24) * scale));
                    this.gfx.Draw2D(this.gfx.getTexture(textureID), srcRect, (SizeF) size, renderPos, ARGBColors.White);
                }
                else
                {
                    flag7 = false;
                }
            }
            if ((!this.isUserVillage(village.id) && !this.isUserRelatedVillage(village.id)) && ((((village.factionID >= 0) && (RemoteServices.Instance.UserFactionID >= 0)) && (this.worldMapFilter.FilterShowHouseSymbols || this.worldMapFilter.FilterShowFactionSymbols)) || ((village.userID >= 0) && this.worldMapFilter.FilterShowUserSymbols)))
            {
                bool flag9 = false;
                if (village.countryCapital)
                {
                    flag9 = true;
                }
                else if (village.provinceCapital)
                {
                    flag9 = true;
                }
                else if (village.countyCapital)
                {
                    if (num5 >= 4.0)
                    {
                        flag9 = true;
                    }
                }
                else if (village.regionCapital)
                {
                    if (num5 >= 6.0)
                    {
                        flag9 = true;
                    }
                }
                else if (num5 >= 8.0)
                {
                    flag9 = true;
                }
                if (flag9)
                {
                    bool flag10 = false;
                    int num29 = -1;
                    int num30 = -1;
                    if (village.factionID == RemoteServices.Instance.UserFactionID)
                    {
                        if (this.worldMapFilter.FilterShowUserSymbols)
                        {
                            int num31 = this.getUserRelationship(village.userID);
                            if (num31 > 0)
                            {
                                num30 = 0xb3;
                            }
                            else if (num31 < 0)
                            {
                                num30 = 180;
                            }
                        }
                        if ((num30 == -1) && (RemoteServices.Instance.UserFactionID >= 0))
                        {
                            num30 = 0xb2;
                        }
                    }
                    else
                    {
                        if (this.worldMapFilter.FilterShowHouseSymbols && (num30 == -1))
                        {
                            int num32 = this.getHouse(RemoteServices.Instance.UserFactionID);
                            int otherHouseID = this.getHouse(village.factionID);
                            if (num32 != otherHouseID)
                            {
                                int num34 = this.getYourHouseRelation(otherHouseID);
                                if (num34 > 0)
                                {
                                    num30 = 0xb3;
                                }
                                else if (num34 < 0)
                                {
                                    num30 = 180;
                                }
                            }
                        }
                        if (this.worldMapFilter.FilterShowFactionSymbols)
                        {
                            if (num30 != -1)
                            {
                                num29 = num30;
                            }
                            int num35 = this.getYourFactionRelation(village.factionID);
                            if (num35 > 0)
                            {
                                num30 = 0xb3;
                            }
                            else if (num35 < 0)
                            {
                                num30 = 180;
                            }
                            if (((num29 != -1) && (num30 != -1)) && (num30 != num29))
                            {
                                flag10 = true;
                            }
                        }
                        if (num30 != -1)
                        {
                            num29 = num30;
                        }
                        int num36 = this.getUserRelationship(village.userID);
                        if (num36 > 0)
                        {
                            num30 = 0xb3;
                        }
                        else if (num36 < 0)
                        {
                            num30 = 180;
                        }
                        if (((num29 != -1) && (num30 != -1)) && (num30 != num29))
                        {
                            flag10 = true;
                        }
                    }
                    if (num30 >= 0)
                    {
                        if (flag10)
                        {
                            num30 = 0xb3;
                        }
                        this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                        this.villageSprite.SpriteNo = num30;
                        int num37 = 0;
                        if (flag7)
                        {
                            num37 = 30;
                        }
                        if (num30 != 180)
                        {
                            this.villageSprite.Center = new PointF((float) (0x27 + num37), 77f);
                        }
                        else
                        {
                            this.villageSprite.Center = new PointF((float) (0x2a + num37), 77f);
                        }
                        scale = ((float) this.m_worldScale) / 17f;
                        if (scale < 0.15f)
                        {
                            scale = 0.15f;
                        }
                        if (scale > 1f)
                        {
                            scale = 1f;
                        }
                        this.gfx.beginSprites();
                        this.villageSprite.Scale = scale;
                        this.villageSprite.Update();
                        this.villageSprite.DrawAndClear();
                        if (flag10)
                        {
                            num30 = 180;
                            this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                            this.villageSprite.SpriteNo = num30;
                            if (num30 != 180)
                            {
                                this.villageSprite.Center = new PointF((float) ((0x27 + num37) + 20), 77f);
                            }
                            else
                            {
                                this.villageSprite.Center = new PointF((float) ((0x2a + num37) + 20), 77f);
                            }
                            scale = ((float) this.m_worldScale) / 17f;
                            scale *= 0.75f;
                            if (scale < 0.15f)
                            {
                                scale = 0.15f;
                            }
                            if (scale > 0.75f)
                            {
                                scale = 0.75f;
                            }
                            this.villageSprite.Scale = scale;
                            this.villageSprite.Update();
                            this.villageSprite.DrawAndClear();
                        }
                    }
                }
            }
        }

        public void drawVillages(RectangleF screenRect)
        {
            FastScreenRect rect = new FastScreenRect {
                left = (int) screenRect.Left,
                top = (int) screenRect.Top,
                Left = screenRect.Left,
                Top = screenRect.Top,
                Width = screenRect.Width,
                Height = screenRect.Height,
                right = (int) (screenRect.Right + 0.999f),
                bottom = (int) (screenRect.Bottom + 0.999f),
                zoomLevel = 27.0 - this.m_worldZoom
            };
            this.m_baseNode.drawVillages(rect);
            if ((rect.zoomLevel < 8.0) && (this.m_userVillages != null))
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    if (!data.capital)
                    {
                        VillageData village = this.villageList[data.villageID];
                        float num = (village.x - rect.Left) / rect.Width;
                        float num2 = (village.y - rect.Top) / rect.Height;
                        if (((num >= -0.1f) && (num <= 1.1f)) && ((num2 >= -0.1f) && (num2 <= 1.1f)))
                        {
                            this.drawVillage(village, (double) num, (double) num2);
                        }
                    }
                }
            }
        }

        public void drawVillageTree(GraphicsMgr newGFX)
        {
            this.xmasPresents = HolidayPeriods.xmas(VillageMap.getCurrentServerTime()) && !GameEngine.Instance.LocalWorldData.AIWorld;
            this.pulse += 8;
            if (this.pulse > 0xff)
            {
                this.pulse -= 0xff;
            }
            if (this.pulse > 0x7f)
            {
                this.pulseValue = (0xff - this.pulse) + 0x7f;
            }
            else
            {
                this.pulseValue = this.pulse + 0x7f;
            }
            this.gfx = newGFX;
            double num = ((double) this.m_screenWidth) / this.m_worldScale;
            double num2 = ((double) this.m_screenHeight) / this.m_worldScale;
            RectangleF screenRect = new RectangleF((float) (this.m_screenCentreX - (num / 2.0)), (float) (this.m_screenCentreY - (num2 / 2.0)), (float) num, (float) num2);
            if (this.m_worldScale == 24.0)
            {
                this.GeographicalMap = true;
                this.PolitcalMap = false;
            }
            else if (this.m_worldScale >= 7.0)
            {
                if ((this.m_worldScale >= 23.899999998509884) && !this.Zooming)
                {
                    this.m_worldScale = 24.0;
                }
                this.GeographicalMap = true;
                this.PolitcalMap = true;
            }
            else
            {
                this.GeographicalMap = false;
                this.PolitcalMap = true;
            }
            this.gfx.beginSprites();
            for (int i = 0; i < this.m_screenHeight; i += 0x200)
            {
                for (int j = 0; j < this.m_screenWidth; j += 0x200)
                {
                    this.gfx.addSprite(GFXLibrary.Instance.ImageSurroundTexID2, new Rectangle(0, 0, 0x200, 0x200), (SizeF) new Size(0x200, 0x200), new PointF((float) j, (float) i));
                }
            }
            this.gfx.drawSprites();
            this.gfx.endSprites();
            float num5 = 0f;
            float num6 = 0f;
            float worldMapWidth = this.worldMapWidth;
            float worldMapHeight = this.worldMapHeight;
            this.gfx.startPoly();
            this.drawSurroundBox(screenRect, Color.FromArgb(0x40, ARGBColors.Black), num5 + 100f, num6 + 100f, worldMapWidth + 100f, worldMapHeight + 100f);
            this.drawSurroundBox(screenRect, Color.FromArgb(0x40, ARGBColors.Black), num5 + 75f, num6 + 75f, worldMapWidth + 75f, worldMapHeight + 75f);
            this.drawSurroundBox(screenRect, Color.FromArgb(0x40, ARGBColors.Black), num5 + 50f, num6 + 50f, worldMapWidth + 50f, worldMapHeight + 50f);
            this.drawSurroundBox(screenRect, Color.FromArgb(0x40, ARGBColors.Black), num5 + 25f, num6 + 25f, worldMapWidth + 25f, worldMapHeight + 25f);
            this.drawSurroundBox(screenRect, Color.FromArgb(0xc0, ARGBColors.Black), num5 - 2f, num6 - 2f, worldMapWidth + 2f, worldMapHeight + 2f);
            this.drawSurroundBox(screenRect, SEACOLOR, num5, num6, worldMapWidth, worldMapHeight);
            this.gfx.drawPoly();
            if (this.playingCountries)
            {
                this.updatePlaybackDay();
                this.drawCountryPolyPlayback(screenRect);
                this.drawSeas(screenRect);
                this.drawCountryBorders(screenRect);
            }
            else if (this.playingProvinces)
            {
                this.updatePlaybackDay();
                this.drawProvincePolyPlayback(screenRect);
                this.drawSeas(screenRect);
                this.drawProvinceBorders(screenRect, true, !this.GeographicalMap);
            }
            else
            {
                if (this.GeographicalMap)
                {
                    this.gfx.beginSprites();
                    if (this.m_worldScale >= 3.0)
                    {
                        int num9 = (int) (((((-64f * screenRect.Width) / ((float) this.m_screenWidth)) + screenRect.Left) * 17f) / 64f);
                        int num10 = (int) (((((-64f * screenRect.Height) / ((float) this.m_screenHeight)) + screenRect.Top) * 17f) / 64f);
                        int num11 = (int) (((((this.m_screenWidth * screenRect.Width) / ((float) this.m_screenWidth)) + screenRect.Left) * 17f) / 64f);
                        int num12 = (int) (((((this.m_screenHeight * screenRect.Height) / ((float) this.m_screenHeight)) + screenRect.Top) * 17f) / 64f);
                        if (num9 < 0)
                        {
                            num9 = 0;
                        }
                        else if (num9 > (this.TILEMAP_WIDTH - 1))
                        {
                            num9 = this.TILEMAP_WIDTH - 1;
                        }
                        if (num10 < 0)
                        {
                            num10 = 0;
                        }
                        else if (num10 > (this.TILEMAP_HEIGHT - 1))
                        {
                            num10 = this.TILEMAP_HEIGHT - 1;
                        }
                        if (num11 < 0)
                        {
                            num11 = 0;
                        }
                        else if (num11 > (this.TILEMAP_WIDTH - 1))
                        {
                            num11 = this.TILEMAP_WIDTH - 1;
                        }
                        if (num12 < 0)
                        {
                            num12 = 0;
                        }
                        else if (num12 > (this.TILEMAP_HEIGHT - 1))
                        {
                            num12 = this.TILEMAP_HEIGHT - 1;
                        }
                        float num13 = ((float) this.m_screenWidth) / screenRect.Width;
                        float num14 = ((float) this.m_screenHeight) / screenRect.Height;
                        for (int k = num10; k <= num12; k++)
                        {
                            for (int n = num9; n <= num11; n++)
                            {
                                float num17 = (((64f * n) / 17f) - screenRect.Left) * num13;
                                float num18 = (((64f * k) / 17f) - screenRect.Top) * num14;
                                float num19 = (((64f * (n + 1f)) / 17f) - screenRect.Left) * num13;
                                float num20 = (((64f * (k + 1f)) / 17f) - screenRect.Top) * num14;
                                this.worldTileSprite.PosX = num17;
                                this.worldTileSprite.PosY = num18;
                                this.worldTileSprite.SpriteNo = this.mapTileGrid[n, k];
                                this.worldTileSprite.specialTileScaleAdjust(num19 - num17, num20 - num18);
                                this.worldTileSprite.Update();
                                this.worldTileSprite.Draw();
                            }
                        }
                        this.gfx.drawSprites();
                        for (int m = num10; m <= num12; m++)
                        {
                            for (int num22 = num9; num22 <= num11; num22++)
                            {
                                if (this.tree1Grid[num22, m] > 0)
                                {
                                    float num23 = (((64f * num22) / 17f) - screenRect.Left) * num13;
                                    float num24 = ((((64f * m) - 8f) / 17f) - screenRect.Top) * num14;
                                    float single1 = (64f * (num22 + 1f)) / 17f;
                                    float left = screenRect.Left;
                                    float single3 = (64f * ((m + 1f) - 8f)) / 17f;
                                    float top = screenRect.Top;
                                    this.worldTreeSprite.PosX = num23;
                                    this.worldTreeSprite.PosY = num24;
                                    this.worldTreeSprite.SpriteNo = this.tree1Grid[num22, m] - 1;
                                    this.worldTreeSprite.Scale = (float) (this.m_worldScale / 23.611);
                                    this.worldTreeSprite.Update();
                                    this.worldTreeSprite.Draw();
                                }
                                if (this.tree2Grid[num22, m] > 0)
                                {
                                    float num25 = (((64f * num22) / 17f) - screenRect.Left) * num13;
                                    float num26 = ((((64f * m) - 8f) / 17f) - screenRect.Top) * num14;
                                    float single5 = (64f * (num22 + 1f)) / 17f;
                                    float single6 = screenRect.Left;
                                    float single7 = (64f * ((m + 1f) - 8f)) / 17f;
                                    float single8 = screenRect.Top;
                                    this.worldTreeSprite.PosX = num25;
                                    this.worldTreeSprite.PosY = num26;
                                    this.worldTreeSprite.SpriteNo = this.tree2Grid[num22, m] - 1;
                                    this.worldTreeSprite.Scale = (float) (this.m_worldScale / 23.611);
                                    this.worldTreeSprite.Update();
                                    this.worldTreeSprite.Draw();
                                }
                            }
                        }
                        this.gfx.drawSprites();
                    }
                    this.gfx.endSprites();
                    this.manageDynamicLines();
                    this.overrideLinelessMap = false;
                    if (!this.PolitcalMap)
                    {
                        this.overrideLinelessMap = true;
                        if ((27.0 - this.m_worldZoom) > 2.3)
                        {
                            if ((27.0 - this.m_worldZoom) >= 5.0)
                            {
                                this.drawRegionsBorder(screenRect, true);
                            }
                            this.drawCountyBorders(screenRect, true);
                            this.drawCountryBorders(screenRect);
                            this.drawProvinceBorders(screenRect, true, false);
                            this.drawRangeCircle(screenRect);
                            if ((27.0 - this.m_worldZoom) >= 13.0)
                            {
                                this.drawInterVillageLines(screenRect);
                            }
                        }
                        else if ((27.0 - this.m_worldZoom) > 0.1)
                        {
                            this.drawCountryBorders(screenRect);
                            this.drawProvinceBorders(screenRect, true, false);
                            this.drawRangeCircle(screenRect);
                        }
                        else
                        {
                            this.drawProvinceBorders(screenRect, false, false);
                            this.drawCountryBorders(screenRect);
                            this.drawRangeCircle(screenRect);
                        }
                    }
                }
                if (this.PolitcalMap)
                {
                    this.overrideLinelessMap = false;
                    if ((27.0 - this.m_worldZoom) > 9.5)
                    {
                        this.overrideLinelessMap = true;
                    }
                    if ((27.0 - this.m_worldZoom) > 2.3)
                    {
                        this.drawCountyPoly(screenRect);
                        this.drawSeas(screenRect);
                        if ((27.0 - this.m_worldZoom) >= 5.0)
                        {
                            this.drawRegions(screenRect);
                        }
                        if (!this.LinelessMaps && ((27.0 - this.m_worldZoom) >= 5.0))
                        {
                            this.drawRegionsBorder(screenRect, false);
                        }
                        this.drawCountyBorders(screenRect, true);
                        this.drawCountryBorders(screenRect);
                        this.drawProvinceBorders(screenRect, true, !this.GeographicalMap);
                        this.drawRangeCircle(screenRect);
                        if ((27.0 - this.m_worldZoom) >= 13.0)
                        {
                            this.drawInterVillageLines(screenRect);
                        }
                    }
                    else if ((27.0 - this.m_worldZoom) > 0.1)
                    {
                        this.drawProvincePoly(screenRect);
                        this.drawSeas(screenRect);
                        this.drawCountryBorders(screenRect);
                        this.drawProvinceBorders(screenRect, true, !this.GeographicalMap);
                        this.drawRangeCircle(screenRect);
                    }
                    else
                    {
                        this.drawCountryPoly(screenRect);
                        this.drawSeas(screenRect);
                        this.drawProvinceBorders(screenRect, false, !this.GeographicalMap);
                        this.drawCountryBorders(screenRect);
                        this.drawRangeCircle(screenRect);
                    }
                    if (this.m_worldScale < 0.5)
                    {
                        this.drawIslandLines(screenRect);
                    }
                }
                if (this.m_worldScale >= 23.999)
                {
                    this.gfx.beginSprites();
                    this.gfx.testBlending(true);
                    int num27 = ((int) (1000000.0 / this.m_worldScale)) - ((int) (this.m_screenCentreX * this.m_worldScale));
                    int num28 = ((int) (1000000.0 / this.m_worldScale)) - ((int) (this.m_screenCentreY * this.m_worldScale));
                    int num29 = (num27 / 0x200) * 0x200;
                    num27 -= num29;
                    int num30 = (num28 / 0x200) * 0x200;
                    num28 -= num30;
                    while (num27 > 0)
                    {
                        num27 -= 0x200;
                    }
                    while (num28 > 0)
                    {
                        num28 -= 0x200;
                    }
                    for (int num31 = num28; num31 < this.m_screenHeight; num31 += 0x200)
                    {
                        for (int num32 = num27; num32 < this.m_screenWidth; num32 += 0x200)
                        {
                            this.overlaySprite.PosX = num32;
                            this.overlaySprite.PosY = num31;
                            this.overlaySprite.Update();
                            this.overlaySprite.Draw();
                        }
                    }
                    this.gfx.drawSprites();
                    this.gfx.endSprites();
                    this.gfx.testBlending(false);
                }
                else if (!Program.ShowSeasonalGraphics)
                {
                    this.gfx.testBlending(true);
                    this.gfx.startPoly();
                    this.gfx.addTriangle(Color.FromArgb(0xfb, 0xfb, 0xd5), 0f, 0f, (float) this.m_screenWidth, 0f, 0f, (float) this.m_screenHeight);
                    this.gfx.addTriangle(Color.FromArgb(0xfb, 0xfb, 0xd5), 0f, (float) this.m_screenHeight, (float) this.m_screenWidth, 0f, (float) this.m_screenWidth, (float) this.m_screenHeight);
                    this.gfx.drawPoly();
                    this.gfx.testBlending(false);
                }
                this.gfx.beginSprites();
                this.drawVillages(screenRect);
                if ((27.0 - this.m_worldZoom) >= 5.5)
                {
                    this.gfx.endSprites();
                    this.gfx.beginSprites();
                    if ((InterfaceMgr.Instance.WorldMapMode != 1) && (InterfaceMgr.Instance.WorldMapMode != 2))
                    {
                        this.gfx.setSpriteSamplerStateNone(false);
                        this.drawPeople(screenRect);
                        this.drawTraders(screenRect);
                        this.drawArmies(screenRect, true);
                        this.gfx.setSpriteSamplerStateNone(true);
                    }
                }
                else if (GameEngine.Instance.LocalWorldData.AIWorld)
                {
                    this.gfx.endSprites();
                    this.gfx.beginSprites();
                    this.gfx.setSpriteSamplerStateNone(false);
                    this.drawArmies(screenRect, false);
                    this.gfx.setSpriteSamplerStateNone(true);
                }
                this.gfx.endSprites();
                this.drawText();
                this.gfx.renderLines();
                this.gfx.setSpriteSamplerStateNone(false);
                this.gfx.beginSprites();
                switch (GameEngine.Instance.clockMode)
                {
                    case 1:
                        this.updateClockSprite.SpriteNo = 0xf8 + GameEngine.Instance.clockFrame;
                        break;

                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        this.updateClockSprite.SpriteNo = 0x138 + GameEngine.Instance.clockFrame;
                        break;

                    default:
                        this.updateClockSprite.SpriteNo = 0xb8 + GameEngine.Instance.clockFrame;
                        break;
                }
                this.updateClockSprite.PosX = (this.m_screenWidth - 80f) + 41f;
                this.updateClockSprite.PosY = -10f;
                this.updateClockSprite.Scale = 0.8f;
                this.updateClockSprite.Update();
                this.updateClockSprite.Draw();
                if (this.isTutorialActive() || Program.mySettings.showGameFeaturesScreenIcon)
                {
                    if (!TutorialWindow.overIcon)
                    {
                        this.tutorialOverlaySprite.TextureID = GFXLibrary.Instance.TutorialIconNormalID;
                    }
                    else
                    {
                        this.tutorialOverlaySprite.TextureID = GFXLibrary.Instance.TutorialIconOverID;
                    }
                    this.tutorialOverlaySprite.PosX = 0f;
                    this.tutorialOverlaySprite.PosY = this.m_screenHeight - 64f;
                    this.tutorialOverlaySprite.Update();
                    this.tutorialOverlaySprite.Draw();
                }
                TimeSpan span = this.freeCardInfo.timeUntilNextFreeCard();
                if ((span.TotalDays > 10.0) || (span.TotalSeconds < 1.0))
                {
                    this.freeCardsSprite2.SpriteNo = 4;
                    this.freeCardsSprite2.PosX = 0f;
                    this.freeCardsSprite2.PosY = 64f;
                    this.freeCardsSprite2.FakeWidthShrink = 0;
                    this.freeCardsSprite2.ColorToUse = Color.FromArgb(Math.Max((this.pulseValue - 0x80) * 2, 0), ARGBColors.White);
                    this.freeCardsSprite2.Center = new PointF(0f, 0f);
                    this.freeCardsSprite2.defaultScaling();
                    this.freeCardsSprite2.Update();
                    this.freeCardsSprite2.Draw();
                    this.freeCardsSprite2.Draw();
                    if (!this.overIcon)
                    {
                        this.freeCardsSprite.SpriteNo = 2;
                    }
                    else
                    {
                        this.freeCardsSprite.SpriteNo = 3;
                    }
                    this.freeCardsSprite.PosX = 0f;
                    this.freeCardsSprite.PosY = 64f;
                    int red = ((this.pulseValue - 0x7f) / 2) + 0xc0;
                    if (red > 0xff)
                    {
                        red = 0xff;
                    }
                    this.freeCardsSprite.ColorToUse = Color.FromArgb(red, red, red);
                    this.freeCardsSprite.Update();
                    this.freeCardsSprite.Draw();
                }
                else
                {
                    if (!this.overIcon)
                    {
                        this.freeCardsSprite.SpriteNo = 0;
                    }
                    else
                    {
                        this.freeCardsSprite.SpriteNo = 1;
                    }
                    this.freeCardsSprite.PosX = 0f;
                    this.freeCardsSprite.PosY = 64f;
                    this.freeCardsSprite.ColorToUse = ARGBColors.White;
                    this.freeCardsSprite.Update();
                    this.freeCardsSprite.Draw();
                    double num34 = this.freeCardInfo.durationHours();
                    double num36 = (span.TotalHours / num34) * 50.0;
                    this.freeCardsSprite2.ColorToUse = ARGBColors.White;
                    this.freeCardsSprite2.FakeWidthShrink = (int) num36;
                    this.freeCardsSprite2.SpriteNo = 5;
                    this.freeCardsSprite2.PosX = 0f;
                    this.freeCardsSprite2.PosY = 64f;
                    this.freeCardsSprite2.Update();
                    this.freeCardsSprite2.Draw();
                }
                if (this.numWheelTypesAvailable() > 0)
                {
                    this.ticketsSprite2.SpriteNo = 4;
                    this.ticketsSprite2.PosX = 0f;
                    this.ticketsSprite2.PosY = 144f;
                    this.ticketsSprite2.FakeWidthShrink = 0;
                    this.ticketsSprite2.ColorToUse = Color.FromArgb(Math.Max((this.pulseValue - 0x80) * 2, 0), ARGBColors.White);
                    this.ticketsSprite2.Center = new PointF(0f, 0f);
                    this.ticketsSprite2.defaultScaling();
                    this.ticketsSprite2.Update();
                    this.ticketsSprite2.Draw();
                    this.ticketsSprite2.Draw();
                    if (!this.overTicketsIcon)
                    {
                        this.ticketsSprite.SpriteNo = 0x17;
                    }
                    else
                    {
                        this.ticketsSprite.SpriteNo = 0x18;
                    }
                    this.ticketsSprite.PosX = 0f;
                    this.ticketsSprite.PosY = 144f;
                    int num37 = ((this.pulseValue - 0x7f) / 2) + 0xc0;
                    if (num37 > 0xff)
                    {
                        num37 = 0xff;
                    }
                    this.ticketsSprite.ColorToUse = Color.FromArgb(num37, num37, num37);
                    this.ticketsSprite.Update();
                    this.ticketsSprite.Draw();
                }
                if (GameEngine.Instance.LocalWorldData.AIWorld && this.isInWolfsRevenge())
                {
                    this.wolfsRevengeSprite.SpriteNo = 30;
                    this.wolfsRevengeSprite.PosX = 0f;
                    this.wolfsRevengeSprite.PosY = 224f;
                    this.wolfsRevengeSprite.ColorToUse = ARGBColors.White;
                    this.wolfsRevengeSprite.Update();
                    this.wolfsRevengeSprite.Draw();
                    TimeSpan span2 = (TimeSpan) (this.wolfsRevengeEnd - this.wolfsRevengeStart);
                    double totalHours = span2.TotalHours;
                    TimeSpan span3 = (TimeSpan) (this.wolfsRevengeEnd - VillageMap.getCurrentServerTime());
                    double num39 = span3.TotalHours;
                    if ((totalHours > 0.0) && (num39 > 0.0))
                    {
                        double num40 = (num39 / totalHours) * 50.0;
                        this.wolfsRevengeSprite2.ColorToUse = ARGBColors.White;
                        this.wolfsRevengeSprite2.FakeWidthShrink = (int) num40;
                        this.wolfsRevengeSprite2.SpriteNo = 0x1f;
                        this.wolfsRevengeSprite2.PosX = 0f;
                        this.wolfsRevengeSprite2.PosY = 224f;
                        this.wolfsRevengeSprite2.Update();
                        this.wolfsRevengeSprite2.Draw();
                    }
                }
                if (this.overWikiHelp)
                {
                    this.gfx.addSprite(GFXLibrary.Instance.WikiHelpIconOver, new Rectangle(0, 0, 0x40, 0x40), (SizeF) new Size(40, 40), new PointF((float) (((this.m_screenWidth - 80) + 0x29) + 11), 32f));
                }
                else
                {
                    this.gfx.addSprite(GFXLibrary.Instance.WikiHelpIconNormal, new Rectangle(0, 0, 0x40, 0x40), (SizeF) new Size(40, 40), new PointF((float) (((this.m_screenWidth - 80) + 0x29) + 11), 32f));
                }
                this.gfx.drawSprites();
                this.gfx.endSprites();
                this.gfx.setSpriteSamplerStateNone(true);
            }
        }

        public void endTutorial()
        {
            StatTrackingClient.Instance().ActivateTrigger(13, GameEngine.Instance.World.getTutorialStage());
            this.m_tutorialInfo.tutorialStage = -3;
            RemoteServices.Instance.set_TutorialCommand_UserCallBack(new RemoteServices.TutorialCommand_UserCallBack(this.TutorialCommandCallback));
            RemoteServices.Instance.TutorialCommand(-3);
        }

        public void experimentalStuff(string mapName)
        {
            int[] ukCountyColour = null;
            int[] ukProvinceColour = null;
            int[] ukCountryColour = null;
            switch (mapName)
            {
                case "uk.wmpdata":
                    ukCountyColour = this.ukCountyColour;
                    ukProvinceColour = this.ukProvinceColour;
                    ukCountryColour = this.ukCountryColour;
                    break;

                case "de.wmpdata":
                    ukCountyColour = this.deCountyColour;
                    ukProvinceColour = this.deProvinceColour;
                    ukCountryColour = this.deCountryColour;
                    break;

                case "fr.wmpdata":
                    ukCountyColour = this.frCountyColour;
                    ukProvinceColour = this.frProvinceColour;
                    ukCountryColour = this.frCountryColour;
                    break;

                case "ru.wmpdata":
                    ukCountyColour = this.ruCountyColour;
                    ukProvinceColour = this.ruProvinceColour;
                    ukCountryColour = this.ruCountryColour;
                    break;

                case "es.wmpdata":
                    ukCountyColour = this.esCountyColour;
                    ukProvinceColour = this.esProvinceColour;
                    ukCountryColour = this.esCountryColour;
                    break;

                case "eu.wmpdata":
                    ukCountyColour = this.euCountyColour;
                    ukProvinceColour = this.euProvinceColour;
                    ukCountryColour = this.euCountryColour;
                    break;

                case "it.wmpdata":
                    ukCountyColour = this.itCountyColour;
                    ukProvinceColour = this.itProvinceColour;
                    ukCountryColour = this.itCountryColour;
                    break;

                case "pl.wmpdata":
                    ukCountyColour = this.plCountyColour;
                    ukProvinceColour = this.plProvinceColour;
                    ukCountryColour = this.plCountryColour;
                    break;

                case "sa.wmpdata":
                    ukCountyColour = this.saCountyColour;
                    ukProvinceColour = this.saProvinceColour;
                    ukCountryColour = this.saCountryColour;
                    break;

                case "tr.wmpdata":
                    ukCountyColour = this.trCountyColour;
                    ukProvinceColour = this.trProvinceColour;
                    ukCountryColour = this.trCountryColour;
                    break;

                case "uk2.wmpdata":
                    ukCountyColour = this.uk2CountyColour;
                    ukProvinceColour = this.uk2ProvinceColour;
                    ukCountryColour = this.uk2CountryColour;
                    break;

                case "us.wmpdata":
                    ukCountyColour = this.usCountyColour;
                    ukProvinceColour = this.usProvinceColour;
                    ukCountryColour = this.usCountryColour;
                    break;
            }
            if (ukCountyColour != null)
            {
                int num = 0;
                foreach (WorldPointList list in this.countyList)
                {
                    list.experimentalColourVariant = this.experimentalColourRemapping[ukCountyColour[num++]];
                }
            }
            if (ukProvinceColour != null)
            {
                int num2 = 0;
                foreach (WorldPointList list2 in this.provincesList)
                {
                    list2.experimentalColourVariant = this.experimentalColourRemapping[ukProvinceColour[num2++]];
                }
            }
            if (ukCountyColour != null)
            {
                int num3 = 0;
                foreach (WorldPointList list3 in this.countryList)
                {
                    list3.experimentalColourVariant = this.experimentalColourRemapping[ukCountryColour[num3++]];
                }
            }
            foreach (WorldPointList list4 in this.regionList)
            {
                WorldPointList list5 = this.countyList[list4.parentID];
                int num4 = list5.experimentalColourVariant + 2;
                if (num4 >= 4)
                {
                    num4 -= 4;
                }
                list4.experimentalColourVariant = num4;
            }
        }

        public bool filterCard(CardTypes.CardDefinition filter, CardTypes.CardDefinition card)
        {
            if (((filter.cardCategory != 0) && (filter.cardCategory != card.cardCategory)) && ((filter.cardCategory != 9) || ((card.cardCategory != 6) && (card.cardCategory != 7))))
            {
                return false;
            }
            if ((filter.cardColour != 0) && (filter.cardColour != card.cardColour))
            {
                return false;
            }
            if ((filter.cardRank != 0) && (filter.cardRank < card.cardRank))
            {
                return false;
            }
            if ((filter.cardFilter != 0) && (filter.cardFilter != card.cardFilter))
            {
                return false;
            }
            if (card.rewardcard && (!filter.rewardcard || (card.worldID != RemoteServices.Instance.ProfileWorldID)))
            {
                return false;
            }
            if (filter.keywords.Length > 0)
            {
                bool flag = false;
                foreach (string str in filter.keywords.Split(",".ToCharArray()))
                {
                    if (card.keywords.Contains(str))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    return false;
                }
            }
            return true;
        }

        public long findNearestArmyFromMapPos(double mapX, double mapY, ref double bestDist)
        {
            WorldMapFilter worldMapFilter = GameEngine.Instance.World.worldMapFilter;
            long armyID = -1L;
            double num2 = 2.25;
            foreach (LocalArmyData data in this.armyArray)
            {
                if (worldMapFilter.showArmy(data))
                {
                    double num3 = ((data.displayX - mapX) * (data.displayX - mapX)) + ((data.displayY - mapY) * (data.displayY - mapY));
                    if (num3 < num2)
                    {
                        num2 = num3;
                        armyID = data.armyID;
                    }
                }
            }
            bestDist = num2;
            return armyID;
        }

        public long findNearestArmyFromScreenPos(Point mousePos, ref double bestDist)
        {
            if (InterfaceMgr.Instance.WorldMapMode == 0)
            {
                double mapX = ((mousePos.X - (((double) this.m_screenWidth) / 2.0)) / this.m_worldScale) + this.m_screenCentreX;
                double mapY = ((mousePos.Y - (((double) this.m_screenHeight) / 2.0)) / this.m_worldScale) + this.m_screenCentreY;
                if (((mapX >= 0.0) && (mapX < this.worldMapWidth)) && ((mapY >= 0.0) && (mapY < this.worldMapHeight)))
                {
                    return this.findNearestArmyFromMapPos(mapX, mapY, ref bestDist);
                }
            }
            return -1L;
        }

        public long findNearestPersonFromMapPos(double mapX, double mapY, ref double bestDist)
        {
            WorldMapFilter worldMapFilter = GameEngine.Instance.World.worldMapFilter;
            long personID = -1L;
            double num2 = 2.25;
            foreach (LocalPerson person in this.personArray)
            {
                if ((((person.person.state > 0) && (person.person.state != 2)) && ((person.person.state != 12) && (person.person.state != 0x16))) && ((person.parentPerson == -1L) && worldMapFilter.showPeople(person)))
                {
                    double num3 = ((person.displayX - mapX) * (person.displayX - mapX)) + ((person.displayY - mapY) * (person.displayY - mapY));
                    if (num3 < num2)
                    {
                        num2 = num3;
                        personID = person.personID;
                    }
                }
            }
            bestDist = num2;
            return personID;
        }

        public long findNearestPersonFromScreenPos(Point mousePos, ref double bestDist)
        {
            if (InterfaceMgr.Instance.WorldMapMode == 0)
            {
                double mapX = ((mousePos.X - (((double) this.m_screenWidth) / 2.0)) / this.m_worldScale) + this.m_screenCentreX;
                double mapY = ((mousePos.Y - (((double) this.m_screenHeight) / 2.0)) / this.m_worldScale) + this.m_screenCentreY;
                if (((mapX >= 0.0) && (mapX < this.worldMapWidth)) && ((mapY >= 0.0) && (mapY < this.worldMapHeight)))
                {
                    return this.findNearestPersonFromMapPos(mapX, mapY, ref bestDist);
                }
            }
            return -1L;
        }

        public long findNearestReinforcementFromMapPos(double mapX, double mapY, ref double bestDist)
        {
            WorldMapFilter worldMapFilter = GameEngine.Instance.World.worldMapFilter;
            long armyID = -1L;
            double num2 = 2.25;
            double num3 = DXTimer.GetCurrentMilliseconds() / 1000.0;
            foreach (LocalArmyData data in this.reinforcementArray)
            {
                if (((data.localEndTime != 0.0) && (data.localEndTime >= num3)) && worldMapFilter.showReinforcements(data))
                {
                    double num4 = ((data.displayX - mapX) * (data.displayX - mapX)) + ((data.displayY - mapY) * (data.displayY - mapY));
                    if (num4 < num2)
                    {
                        num2 = num4;
                        armyID = data.armyID;
                    }
                }
            }
            bestDist = num2;
            return armyID;
        }

        public long findNearestReinforcementFromScreenPos(Point mousePos, ref double bestDist)
        {
            if (InterfaceMgr.Instance.WorldMapMode == 0)
            {
                double mapX = ((mousePos.X - (((double) this.m_screenWidth) / 2.0)) / this.m_worldScale) + this.m_screenCentreX;
                double mapY = ((mousePos.Y - (((double) this.m_screenHeight) / 2.0)) / this.m_worldScale) + this.m_screenCentreY;
                if (((mapX >= 0.0) && (mapX < this.worldMapWidth)) && ((mapY >= 0.0) && (mapY < this.worldMapHeight)))
                {
                    return this.findNearestReinforcementFromMapPos(mapX, mapY, ref bestDist);
                }
            }
            return -1L;
        }

        public long findNearestTraderFromMapPos(double mapX, double mapY, ref double bestDist)
        {
            WorldMapFilter worldMapFilter = GameEngine.Instance.World.worldMapFilter;
            long traderID = -1L;
            double num2 = 2.25;
            foreach (LocalTrader trader in this.traderArray)
            {
                if (((trader.trader.traderState > 0) && (trader.parentTrader == -1L)) && worldMapFilter.showTrader(trader))
                {
                    double num3 = ((trader.displayX - mapX) * (trader.displayX - mapX)) + ((trader.displayY - mapY) * (trader.displayY - mapY));
                    if (num3 < num2)
                    {
                        num2 = num3;
                        traderID = trader.traderID;
                    }
                }
            }
            bestDist = num2;
            return traderID;
        }

        public long findNearestTraderFromScreenPos(Point mousePos, ref double bestDist)
        {
            if (InterfaceMgr.Instance.WorldMapMode == 0)
            {
                double mapX = ((mousePos.X - (((double) this.m_screenWidth) / 2.0)) / this.m_worldScale) + this.m_screenCentreX;
                double mapY = ((mousePos.Y - (((double) this.m_screenHeight) / 2.0)) / this.m_worldScale) + this.m_screenCentreY;
                if (((mapX >= 0.0) && (mapX < this.worldMapWidth)) && ((mapY >= 0.0) && (mapY < this.worldMapHeight)))
                {
                    return this.findNearestTraderFromMapPos(mapX, mapY, ref bestDist);
                }
            }
            return -1L;
        }

        public int findNearestVillageFromMapPos(double mapX, double mapY, ref double bestDist)
        {
            int id = -1;
            double num2 = 64.0;
            foreach (VillageData data in this.villageList)
            {
                if (data.visible)
                {
                    double num3 = ((data.x - mapX) * (data.x - mapX)) + ((data.y - mapY) * (data.y - mapY));
                    if (num3 < num2)
                    {
                        num2 = num3;
                        id = data.id;
                    }
                }
            }
            bestDist = num2;
            return id;
        }

        public int findNearestVillageFromMapPosAnyVis(double mapX, double mapY, ref double bestDist)
        {
            int id = -1;
            double num2 = 64.0;
            foreach (VillageData data in this.villageList)
            {
                double num3 = ((data.x - mapX) * (data.x - mapX)) + ((data.y - mapY) * (data.y - mapY));
                if (num3 < num2)
                {
                    num2 = num3;
                    id = data.id;
                }
            }
            bestDist = num2;
            return id;
        }

        public int findNearestVillageFromScreenPos(Point mousePos, ref double bestDist)
        {
            double mapX = ((mousePos.X - (((double) this.m_screenWidth) / 2.0)) / this.m_worldScale) + this.m_screenCentreX;
            double mapY = ((mousePos.Y - (((double) this.m_screenHeight) / 2.0)) / this.m_worldScale) + this.m_screenCentreY;
            if (((mapX >= 0.0) && (mapX < this.worldMapWidth)) && ((mapY >= 0.0) && (mapY < this.worldMapHeight)))
            {
                return this.findNearestVillageFromMapPos(mapX, mapY, ref bestDist);
            }
            return -1;
        }

        public int findNearestVillageFromScreenPosAnyVis(Point mousePos, ref double bestDist)
        {
            double mapX = ((mousePos.X - (((double) this.m_screenWidth) / 2.0)) / this.m_worldScale) + this.m_screenCentreX;
            double mapY = ((mousePos.Y - (((double) this.m_screenHeight) / 2.0)) / this.m_worldScale) + this.m_screenCentreY;
            if (((mapX >= 0.0) && (mapX < this.worldMapWidth)) && ((mapY >= 0.0) && (mapY < this.worldMapHeight)))
            {
                return this.findNearestVillageFromMapPosAnyVis(mapX, mapY, ref bestDist);
            }
            return -1;
        }

        public int findSelfInLeaderboard(int mode)
        {
            int userID = RemoteServices.Instance.UserID;
            SparseArray array = null;
            switch (mode)
            {
                case -6:
                    array = this.leaderboard_MainVillages;
                    break;

                case -5:
                    array = this.leaderboard_MainRank;
                    break;

                case -4:
                {
                    array = this.leaderboard_ParishFlags;
                    userID = 1;
                    int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
                    if (!this.isCapital(villageID))
                    {
                        userID = this.getParishFromVillageID(villageID);
                        break;
                    }
                    if (!this.isRegionCapital(villageID))
                    {
                        List<int> list = this.getUserVillageIDList();
                        if (list.Count > 0)
                        {
                            foreach (int num3 in list)
                            {
                                if (!this.isCapital(num3))
                                {
                                    userID = this.getParishFromVillageID(villageID);
                                    break;
                                }
                            }
                        }
                        break;
                    }
                    userID = this.getParishFromVillageID(villageID);
                    break;
                }
                case -3:
                    array = this.leaderboard_Houses;
                    userID = GameEngine.Instance.World.getHouse(RemoteServices.Instance.UserFactionID);
                    break;

                case -2:
                    array = this.leaderboard_Factions;
                    userID = RemoteServices.Instance.UserFactionID;
                    break;

                case -1:
                    array = this.leaderboard_Main;
                    break;

                case 0:
                    array = this.leaderboard_Sub_Pillager;
                    break;

                case 1:
                    array = this.leaderboard_Sub_Defender;
                    break;

                case 2:
                    array = this.leaderboard_Sub_Ransack;
                    break;

                case 3:
                    array = this.leaderboard_Sub_Wolfsbane;
                    break;

                case 4:
                    array = this.leaderboard_Sub_Banditkiller;
                    break;

                case 5:
                    array = this.leaderboard_Sub_AIKiller;
                    break;

                case 6:
                    array = this.leaderboard_Sub_Trader;
                    break;

                case 7:
                    array = this.leaderboard_Sub_Forager;
                    break;

                case 8:
                    array = this.leaderboard_Sub_Stockpiler;
                    break;

                case 9:
                    array = this.leaderboard_Sub_Farmer;
                    break;

                case 10:
                    array = this.leaderboard_Sub_Brewer;
                    break;

                case 11:
                    array = this.leaderboard_Sub_Weaponsmith;
                    break;

                case 12:
                    array = this.leaderboard_Sub_banquetter;
                    break;

                case 13:
                    array = this.leaderboard_Sub_Achiever;
                    break;

                case 14:
                    array = this.leaderboard_Sub_Donater;
                    break;

                case 15:
                    array = this.leaderboard_Sub_Capture;
                    break;

                case 0x10:
                    array = this.leaderboard_Sub_Raze;
                    break;

                case 0x11:
                    array = this.leaderboard_Sub_Glory;
                    break;
            }
            foreach (LeaderBoardEntryData data in array)
            {
                if (data.entryID == userID)
                {
                    return data.standing;
                }
            }
            return 1;
        }

        public void fixupNames()
        {
            CommonTypes.WorldMapType type = GameEngine.Instance.WorldMapTypesData.getMapData(this.currentMapType);
            if (type.mapName.ToLower() == "de.wmpData".ToLower())
            {
                this.fixupParishName(0x8f0, 0xf7a3, "Milovice");
                this.fixupParishName(0x4e3, 0x8980, "Česk\x00fd Krumlov");
                this.fixupParishName(0x875, 0xeb41, "Lazec");
                this.fixupParishName(0x376, 0x6354, "Samtens");
                this.fixupParishName(0x1031, 0x1b31a, "Bergen auf R\x00fcgen");
                this.fixupParishName(0x872, 0xeb1d, "Gro\x00df Kordshagen");
                this.fixupParishName(0x73f, 0xc973, "Gransee");
                this.fixupParishName(0xfbb, 0x1a72c, "O\x00dfnig");
                this.fixupParishName(0x2fa, 0x553b, "Tietzow");
                this.fixupParishName(0x2c2, 0x4ebb, "Wustermark");
                this.fixupParishName(0xbf9, 0x1488c, "Spremberg");
                this.fixupParishName(0x8cf, 0xf48b, "Lieske");
                this.fixupParishName(0x410, 0x718d, "Welzow");
                this.fixupParishName(0xa4c, 0x11d09, "Gro\x00dfr\x00e4schen");
                this.fixupParishName(0x318, 0x581c, "Spremberg");
                this.fixupParishName(0xe04, 0x17f6e, "Lugau");
                this.fixupParishName(0x23, 0x346, "Sayda");
                this.fixupParishName(0xd2c, 0x16815, "Gro\x00dfenhain");
                this.fixupParishName(0x554, 0x952f, "Star\x00fd Mateřov");
                this.fixupParishName(0x1e9, 0x3695, "Stavenhagen");
                this.fixupParishName(0x45d, 0x7a73, "Lemberg");
                this.fixupParishName(0xc61, 0x15269, "Marienbaum");
                this.fixupParishName(0x92e, 0xfe0b, "Geeste");
                this.fixupParishName(0x3ea, 0x6dbd, "Lastrup");
                this.fixupParishName(0xd42, 0x169b8, "Markhausen");
                this.fixupParishName(0x76f, 0xd076, "Bar\x00dfel");
                this.fixupParishName(0x86, 0xf31, "Leer");
                this.fixupParishName(700, 0x4de9, "D\x00f6rverden");
                this.fixupParishName(0xddd, 0x17af5, "Hoya");
                this.fixupParishName(0x51, 0x9bd, "Str\x00fcth");
                this.fixupParishName(0xcee, 0x1601b, "Nackenheim");
                this.fixupParishName(0xbbb, 0x142cc, "Hammersbach");
                this.fixupParishName(760, 0x5512, "Grenderich");
                this.fixupParishName(0x48c, 0x7f17, "Blankenrath");
                this.fixupParishName(0x428, 0x73cd, "Beltheim");
                this.fixupParishName(0x174, 0x27be, "Stocksee");
                this.fixupParishName(0x6ee, 0xc02e, "Scharbeutz");
                this.fixupParishName(0x70b, 0xc3aa, "Potsdam");
                this.fixupParishName(0xbf2, 0x147e3, "Gol\x00dfen");
                this.fixupParishName(0x707, 0xc329, "Dahme-Spreewald");
                this.fixupParishName(0x436, 0x756d, "Treuenbrietzen");
                this.fixupParishName(0x720, 0xc617, "Hohenseefeld");
                this.fixupParishName(0x288, 0x484d, "Jessen");
                this.fixupParishName(0x270, 0x460a, "Sonnewalde");
                this.fixupParishName(0xf0e, 0x197ca, "Droy\x00dfig");
                this.fixupParishName(0x8cb, 0xf419, "Rohr");
                this.fixupParishName(0x803, 0xe02a, "Martigny");
                this.fixupParishName(0x3be, 0x6977, "Amberg-Sulzbach");
                this.fixupParishName(0xf3c, 0x19c4b, "Amberg");
                this.fixupParishName(0x1057, 0x1b6f5, "Ebersdorf");
                this.fixupParishName(0x712, 0xc494, "Bad Bederkesa");
                this.fixupParishName(0x10f, 0x1d3f, "Osnabr\x00fcck");
                this.fixupParishName(0xfca, 0x1a876, "F\x00fcrstenau");
                this.fixupParishName(0xfd5, 0x1a95f, "V\x00f6rden");
                this.fixupParishName(0x520, 0x906c, "Bad Holzhausen");
                this.fixupParishName(0xc81, 0x155ae, "Spenge");
                this.fixupParishName(0x6f8, 0xc119, "L\x00f6hne");
                this.fixupParishName(0xf96, 0x1a40c, "Steinegge");
                this.fixupParishName(0x2f5, 0x5499, "Porta Westfalica");
                this.fixupParishName(0x8b5, 0xf221, "Auetal");
                this.fixupParishName(0x898, 0xef7c, "Merxhausen");
                this.fixupParishName(0x758, 0xcc5b, "Schwarmstedt");
                this.fixupParishName(0xbfa, 0x1488d, "Bad Essen");
                this.fixupParishName(0xc58, 0x15187, "Hemke");
                this.fixupParishName(890, 0x63e4, "T\x00e1bor");
                this.fixupParishName(0x1091, 0x1bbe6, "Rok");
                this.fixupParishName(0xc15, 0x14b54, "Bad Gandersheim");
                this.fixupParishName(0xfd, 0x1b03, "Dannenberg");
                this.fixupParishName(0xd19, 0x16658, "L\x00fcbbow");
                this.countyList[6].areaName = "Braunschweig-L\x00fcneburg";
                this.provincesList[9].areaName = "B\x00f6hmen";
                this.provincesList[10].areaName = "M\x00e4hren";
            }
            if (type.mapName.ToLower() == "fr.wmpData".ToLower())
            {
                this.fixupParishName(0xe31, 0x19d1a, "Aub\x00e9pine");
                this.fixupParishName(0x3b8, 0x6dad, "Uza");
                this.fixupParishName(0x6f2, 0xcf6e, "Fontvieille");
                this.fixupParishName(0x764, 0xdc39, "Fourques");
                this.fixupParishName(0xe13, 0x199a4, "Camargue");
                this.fixupParishName(0x3e2, 0x71e2, "La Dynamite");
                this.fixupParishName(0x3ef, 0x7377, "Pioch Badet");
                this.fixupParishName(0x610, 0xb4b3, "Marignane");
                this.fixupParishName(0x61f, 0xb598, "Saint-Laurent d'Aigouze");
                this.fixupParishName(0xc56, 0x167c9, "Desvres");
                this.fixupParishName(0x3a0, 0x6a74, "Diksmuide");
                this.fixupParishName(0x91b, 0x10b47, "Arendonk");
                this.fixupParishName(0xe7b, 0x1a4f5, "Dessel");
                this.fixupParishName(0x153, 0x2843, "Simmerath");
                this.fixupParishName(0xf30, 0x1ba72, "Hachiville");
                this.fixupParishName(0x119, 0x1fdb, "Germingen");
                this.fixupParishName(0xda3, 0x18c46, "Montreux");
                this.fixupParishName(0x935, 0x10e2b, "Mouthe");
                this.fixupParishName(0xe25, 0x19c01, "Moissey");
                this.fixupParishName(0x484, 0x854a, "La Giettaz");
                this.fixupParishName(0xd03, 0x17a4e, "Lanslevillard");
                this.fixupParishName(0xb37, 0x148e5, "Le Lautaret");
                this.fixupParishName(0x96c, 0x112ee, "Le Freney-d'Oisans");
                this.fixupParishName(0xd3, 0x1694, "Mont Thabor");
                this.fixupParishName(0xc58, 0x167f8, "Saint-Laurent-en-Beaumont");
                this.fixupParishName(0x11, 0x194, "Orci\x00e8res");
                this.fixupParishName(0x94a, 0x10ffc, "La Martre");
                this.fixupParishName(0xe83, 0x1a707, "Cuers");
                this.fixupParishName(0xab3, 0x13aa1, "Vauvenargues");
                this.fixupParishName(0x2cf, 0x53b8, "Riez");
                this.fixupParishName(0x3eb, 0x72fb, "Le Liouquet");
                this.fixupParishName(0xc93, 0x16d96, "Murat-sur-V\x00e8bre");
                this.fixupParishName(0xf8, 0x1af0, "Le Margu\x00e8s");
                this.fixupParishName(0x749, 0xd8d5, "Sainte-Foi");
                this.fixupParishName(0x4eb, 0x926b, "Saint Etienne de Ba\x00efgorry");
                this.fixupParishName(0x3b8, 0x6dad, "L\x00e9vignac");
                this.fixupParishName(0x78e, 0xe024, "Contis Plage");
                this.fixupParishName(0x21b, 0x3efe, "Garein");
                this.fixupParishName(0xc61, 0x168dc, "Sanguinet");
                this.fixupParishName(0x784, 0xde9f, "Lugos");
                this.fixupParishName(290, 0x207c, "Cazaux");
                this.fixupParishName(0xb9a, 0x15433, "Le Pilat");
                this.fixupParishName(0x59c, 0xa86f, "Lugos");
                this.fixupParishName(0x2b8, 0x5138, "Audenge");
                this.fixupParishName(790, 0x5b47, "Saumos");
                this.fixupParishName(0x49d, 0x8822, "Lesparre-M\x00e9doc");
                this.fixupParishName(0x5af, 0xaad7, "Tr\x00e9bivan");
                this.fixupParishName(0xdc0, 0x1900e, "Guiscriff");
                this.fixupParishName(0x873, 0xf801, "Rosporden");
                this.fixupParishName(0x8cf, 0x100ef, "Plouasne");
                this.fixupParishName(0x23f, 0x441b, "Villenauxe-la-Grande");
                this.fixupParishName(0x5eb, 0xb0a3, "Nemours");
                this.fixupParishName(0xac2, 0x13cf5, "Charny");
                this.fixupParishName(0xb78, 0x14f2f, "Saint Gondon");
                this.fixupParishName(0xbc4, 0x1592a, "M\x00e9nestreau-en-Villette");
                this.fixupParishName(0xe45, 0x19f2c, "La Brosse");
                this.fixupParishName(0x6b0, 0xc758, "M\x00e9n\x00e9tr\x00e9ol-sur-Sauldre");
                this.fixupParishName(0x824, 0xeff2, "Saulieu");
                this.fixupParishName(0x9da, 0x11e3f, "Evron");
                this.fixupParishName(0x127, 0x208f, "Oudenaarde");
                this.fixupParishName(0xb94, 0x15337, "Bi\x00e8vre");
                this.fixupParishName(0xc1, 0x149d, "Eprave");
                this.fixupParishName(0x87b, 0xf8e3, "Etalle");
                this.fixupParishName(0x195, 0x2ed2, "Paulhenc");
                this.fixupParishName(0x63f, 0xb924, "Froidchapelle");
                this.fixupParishName(0x3f9, 0x74a5, "Momignies");
                this.fixupParishName(0xdee, 0x1950a, "Sivry-Rance");
                this.fixupParishName(0x34b, 0x6143, "Bi\x00e8vre");
                this.fixupParishName(0x800, 0xebe8, "Jodoigne");
                this.fixupParishName(0x4ea, 0x9264, "Fontenois-la-Ville");
                this.fixupParishName(0x266, 0x479a, "Ninove");
                this.fixupParishName(0x5ff, 0xb2ba, "Landos");
                this.fixupParishName(0xbc9, 0x15980, "Mallemort");
                this.fixupParishName(0xf39, 0x1bc05, "La Trimouille");
                this.fixupParishName(0xec2, 0x1ae91, "Saint-Germain-de-la-Coudre");
                this.fixupParishName(0x475, 0x83e1, "Hasti\x00e8re-par-dela");
                this.fixupParishName(0x69, 0xb8d, "Aragnouet");
                this.fixupParishName(0x85f, 0xf570, "Thermes Maguoac");
                this.fixupParishName(0x44a, 0x7de5, "Gavarnie");
                this.fixupParishName(0x47, 0x6c7, "Castelnau-Rivi\x00e8re-Basse");
                this.fixupParishName(0xa77, 0x1321b, "Bar\x00e8ges");
                this.fixupParishName(0x7da, 0xe848, "Cauterets");
                this.fixupParishName(0x14c, 0x255b, "Launemezan");
                this.fixupParishName(0x74f, 0xd963, "G\x00e8dre");
                this.fixupParishName(0x38f, 0x6836, "Bagn\x00e8res-de-Bigorre");
                this.fixupParishName(590, 0x4549, "Tournoy");
                this.fixupParishName(0x585, 0xa5cb, "Arreau");
                this.fixupParishName(130, 0xdf3, "Bord\x00e8res-Louron");
                this.fixupParishName(0x561, 0xa16d, "Sost");
                this.fixupParishName(0x4ae, 0x8a4d, "Sarrancolin");
                this.fixupParishName(0x1a9, 0x3117, "La Soula");
                this.fixupParishName(0xdce, 0x191bf, "Siradan");
                this.fixupParishName(0xb7e, 0x14fd5, "La Barthe-de-Neste");
                this.fixupParishName(0x875, 0xf84e, "Argel\x00e8s-Gazost");
                this.fixupParishName(0x333, 0x5ea0, "Cauterets");
                this.fixupParishName(0x429, 0x7a10, "Arrens-Marsous");
                this.fixupParishName(0x685, 0xc170, "Pouyastruc");
                this.fixupParishName(0xb62, 0x14d5e, "Labatut-Rivi\x00e8re");
                this.fixupParishName(0xd43, 0x18271, "Lafitole");
                this.fixupParishName(0xc9b, 0x16eba, "Trie-sur-Baise");
                this.fixupParishName(0xf27, 0x1b9ba, "Issoudun");
                this.fixupParishName(0x60, 0x97b, "Tr\x00e8ves");
                this.fixupParishName(0xb59, 0x14c2f, "Vissec");
                this.fixupParishName(0xa86, 0x134b9, "Centuri");
                this.fixupParishName(1, 0x5f, "Sainte-Lucie-de-Tallano");
                this.fixupParishName(0xa4f, 0x12cce, "Falzet");
                this.fixupParishName(0xf15, 0x1b808, "Chezal-Benoit");
                this.fixupParishName(0xa5f, 0x12f27, "Le Massegros");
                this.fixupParishName(0xbe0, 0x15c04, "Ocquier");
                this.fixupParishName(0xcc9, 0x1743c, "Lourdes");
                this.fixupParishName(0xac2, 0x13cf5, "Fontenouilles");
                this.fixupParishName(0x3ab, 0x6a80, "San-Gavino-di-Carbini");
                this.villageList[0x6f75].m_villageName = "Poitiers";
            }
            bool flag1 = type.mapName.ToLower() == "uk.wmpData".ToLower();
            if (type.mapName.ToLower() == "ru.wmpData".ToLower())
            {
                this.countyList[0x37].areaName = "Минск";
                this.villageList[0xd26f].villageName = "Кишинёв";
                this.countyList[0x36].areaName = "Витебская область";
            }
            if (type.mapName.ToLower() == "pl.wmpData".ToLower())
            {
                this.provincesList[0].areaName = "Zachodniopomorskie";
                this.provincesList[6].areaName = "Ł\x00f3dzkie";
            }
            if (type.mapName.ToLower() == "us.wmpData".ToLower())
            {
                this.fixupParishName(0x876, 0xcc11, "Niagara");
                this.fixupParishName(0xa8a, 0xfc8e, "Juniata");
                this.fixupParishName(0x10f0, 0x19d65, "Barnstable");
                this.fixupParishName(0xb42, 0x10fd9, "Cuba");
                this.fixupParishName(0x5ce, 0x8c25, "Jacksonville");
                this.countyList[0x20].areaName = "East Kentucky";
                this.countyList[0x23].areaName = "South Indiana";
                this.countyList[0x33].areaName = "San Antonio";
            }
            if (type.mapName.ToLower() == "it.wmpData".ToLower())
            {
                this.countyList[0x70].areaName = "Osijek-Baranja";
                this.provincesList[0x25].areaName = "Sardegna";
                this.provincesList[7].areaName = "Toscana";
                this.fixupParishName(0x5e1, 0x8195, "Krusevo");
                this.provincesList[0x1b].areaName = "Južna Crna Gora";
                this.provincesList[0x1c].areaName = "Sjeverna Crna Gora";
                this.countyList[0x74].areaName = "Bjelovar-Bilogora";
                this.villageList[0x713f].villageName = "Bjelovar";
                this.fixupParishName(0xc55, 0x10f54, "Fagagna");
                this.fixupParishName(0x578, 0x7754, "Cimolais");
            }
            if (type.mapName.ToLower() == "eu.wmpData".ToLower())
            {
                this.countyList[0xbf].areaName = "G\x00f6taland";
                this.countyList[0xbd].areaName = "Svealand";
                this.countyList[0xbc].areaName = "Norrland";
                this.countyList[0xc0].areaName = "Dalarna";
                this.countyList[70].areaName = "Jylland";
                this.provincesList[30].areaName = "Region Jylland";
                this.countyList[60].areaName = "Gelre";
                this.countyList[0x3a].areaName = "Holland";
                this.countyList[0x39].areaName = "Brabant";
                this.countyList[0x65].areaName = "Dunant\x00f9l Megye";
                this.countyList[0x66].areaName = "Duna-Tisza K\x00f6ze Megye";
                this.countyList[0x67].areaName = "Tisz\x00e0nt\x00f9l Megye";
                this.countyList[0xbb].areaName = "Lapin L\x00e4\x00e4ni";
                this.countyList[0xba].areaName = "Oulun L\x00e4\x00e4ni";
                this.countyList[0xb9].areaName = "L\x00e4nsi-Suomen L\x00e4\x00e4ni";
                this.countyList[0xb8].areaName = "It\x00e4-Suomen L\x00e4\x00e4ni";
                this.countyList[0x7d].areaName = "Област Враца";
                this.countyList[0x7e].areaName = "Област Велико Търново";
                this.countyList[0x7f].areaName = "Област Варна";
                this.countyList[0x81].areaName = "Област Пловдив";
                this.countyList[0xb0].areaName = "Harjumaa";
                this.countyList[0x35].areaName = "Distrito de Vila Real";
                this.provincesList[0x51].areaName = "Pohjois-Suomi";
                this.provincesList[80].areaName = "Etel\x00e4-Suomi";
                this.countyList[0x7c].areaName = "Κρήτη";
                this.countyList[0x7b].areaName = "Πελοπόννησος";
                this.countyList[0x7a].areaName = "Στερεά Ελλάδα";
                this.countyList[120].areaName = "Θεσσαλία";
                this.countyList[0x77].areaName = "Κεντρική Μακεδονία";
                this.countyList[0x79].areaName = "Ανατολική Μακεδονία και Θράκη";
                this.countyList[0x36].areaName = "Estremadura";
                this.countyList[0x35].areaName = "Douro";
                this.countyList[0x34].areaName = "Beiras";
                this.countyList[0x33].areaName = "Alentejo";
                this.countyList[0x41].areaName = "Sachsen";
                this.countyList[0x5f].areaName = "Jihomoravsk\x00fd kraj";
                this.countyList[2].areaName = "Tr\x00f8ndelag";
                this.provincesList[10].areaName = "Eastern Ireland";
                this.fixupParishName(0xb74, 0xfd1f, "Builth Wells");
                this.fixupParishName(0x88b, 0xbd2a, "Skegness");
                this.countyList[4].areaName = "Inverness-shire";
                this.fixupParishName(0x101c, 0x16eb2, "Karlovy Vary");
                this.fixupParishName(0x1a, 0x1ff, "Сергиев Посад");
                string villageName = this.villageList[0xd753].m_villageName;
                this.villageList[0xd753].m_villageName = this.villageList[0x7c64].m_villageName;
                this.villageList[0x7c64].m_villageName = villageName;
            }
        }

        public void fixupParishName(int parishID, int villageID, string newName)
        {
            this.villageList[villageID].m_villageName = newName;
            this.regionList[parishID].areaName = newName;
        }

        public int fixupVillageSprites(int colourID)
        {
            if (colourID == 0)
            {
                colourID = 7;
                return colourID;
            }
            if (colourID == 7)
            {
                colourID = 0;
            }
            return colourID;
        }

        public void fixupVisibleParishCapitals()
        {
            foreach (VillageData data in this.villageList)
            {
                if (data.visible && !data.Capital)
                {
                    int regionID = data.regionID;
                    if (regionID >= 0)
                    {
                        this.villageList[this.regionList[regionID].capitalVillage].visible = true;
                    }
                }
            }
        }

        public void FlagQuestObjectiveCompleteCallBack(FlagQuestObjectiveComplete_ReturnType returnData)
        {
            if (returnData.Success && (returnData.objectiveCompleted >= 0))
            {
                int item = Quests.getQuestFromObjectiveFlag(returnData.objectiveCompleted);
                if (item >= 0)
                {
                    this.tutorialQuestsObjectivesComplete.Add(item);
                    this.TutorialQuestCompleted(item);
                }
            }
        }

        public void flushCaches()
        {
            this.m_cachesFlushed = true;
        }

        public void flushParishWallDonation(int parishCapitalID, int userID)
        {
            long num = (parishCapitalID << 0x20) + userID;
            this.m_parishWallDonateDetails[num] = null;
        }

        public void forceTutorialToBeShown()
        {
            this.newTutorialAvailable = true;
        }

        public void freeCardTooltip(Point dxMousePos)
        {
            this.overIcon = false;
            if (((dxMousePos.X < 70) && (dxMousePos.Y >= 0x40)) && (dxMousePos.Y < 0x86))
            {
                this.overIcon = true;
                CustomTooltipManager.MouseEnterTooltipArea(0x2904);
            }
            this.overTicketsIcon = false;
            if (((this.numWheelTypesAvailable() > 0) && (dxMousePos.X < 70)) && ((dxMousePos.Y >= 0x90) && (dxMousePos.Y < 0xd6)))
            {
                this.overTicketsIcon = true;
                CustomTooltipManager.MouseEnterTooltipArea(0x2905);
            }
            this.overWikiHelp = false;
            if (((dxMousePos.X > (this.m_screenWidth - 30)) && (dxMousePos.Y > 30)) && (dxMousePos.Y < 60))
            {
                this.overWikiHelp = true;
                CustomTooltipManager.MouseEnterTooltipArea(0x1130, 0);
            }
            this.overWolf = false;
            if (((GameEngine.Instance.LocalWorldData.AIWorld && (dxMousePos.X < 70)) && ((dxMousePos.Y >= 0xe0) && (dxMousePos.Y < 0x126))) && this.isInWolfsRevenge())
            {
                this.overWolf = true;
                TimeSpan span = (TimeSpan) (this.wolfsRevengeEnd - VillageMap.getCurrentServerTime());
                CustomTooltipManager.MouseEnterTooltipArea(0x2906, (int) span.TotalSeconds);
            }
        }

        public void fullTickCallBack(FullTick_ReturnType returnData)
        {
            if (InterfaceMgr.Instance.getCardWindow() != null)
            {
                CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.getCardWindow());
            }
            if (returnData.Success)
            {
                if (returnData.villageUpdateList != null)
                {
                    this.processVillageFactionChangesList(returnData.villageUpdateList, returnData.currentVillageChangePos);
                }
                else if (returnData.villageOwnerFactions != null)
                {
                    this.processVillageFactionList(returnData.villageOwnerFactions, returnData.currentVillageChangePos);
                }
                this.updateRegionFactions(returnData.regionUpdateList, returnData.regionFactions, returnData.currentRegionChangePos);
                this.updateCountyFactions(returnData.countyUpdateList, returnData.countyFactions, returnData.currentCountyChangePos);
                this.updateProvinceFactions(returnData.provinceUpdateList, returnData.provinceFactions, returnData.currentProvinceChangePos);
                this.updateCountryFactions(returnData.countryUpdateList, returnData.countryFactions, returnData.currentCountryChangePos);
                if (returnData.userCapitals != null)
                {
                    this.updateUserCapitals(returnData.userCapitals);
                }
                if (returnData.villageChangedNames != null)
                {
                    this.changeVillageNames(returnData.villageChangedNames);
                }
                if (returnData.factionsList != null)
                {
                    this.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
                }
                if (returnData.parishFlagChanges != null)
                {
                    this.updateParishFlags(returnData.parishFlagChanges, returnData.currentParishFlagPos);
                }
                if (returnData.countyFlagChanges != null)
                {
                    this.updateCountyFlags(returnData.countyFlagChanges, returnData.currentCountyFlagPos);
                }
                if (returnData.provinceFlagChanges != null)
                {
                    this.updateProvinceFlags(returnData.provinceFlagChanges, returnData.currentProvinceFlagPos);
                }
                if (returnData.countryFlagChanges != null)
                {
                    this.updateCountryFlags(returnData.countryFlagChanges, returnData.currentCountryFlagPos);
                }
                this.storedVillageNamePos = returnData.currentVillageNameChangePos;
                if (returnData.armyDataReturn != null)
                {
                    returnData.armyDataReturn.SetAsSucceeded();
                    this.getArmyData(returnData.armyDataReturn);
                }
                this.highestArmySeen = returnData.armyHighestSeen;
                if ((returnData.changedTraders != null) && (returnData.changedTraders.Count > 0))
                {
                    this.importOrphanedTraders(returnData.changedTraders, returnData.currentTime, -2);
                    this.lastTraderTime = returnData.currentTime;
                }
                if ((returnData.people != null) && (returnData.people.Count > 0))
                {
                    this.importOrphanedPeople(returnData.people, returnData.currentTime, -2);
                    this.lastPersonTime = returnData.currentTime;
                }
                this.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                this.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
                this.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
                this.setPoints(returnData.currentPoints);
                this.setNumMadeCaptains(returnData.numMadeCaptains);
                this.m_mostAge4Villages = returnData.mostAge4Villages;
                GameEngine.Instance.setServerDownTime(returnData.serverDowntime);
                this.setTickets(0, returnData.wheel_Treasure1Tickets);
                this.setTickets(1, returnData.wheel_Treasure2Tickets);
                this.setTickets(2, returnData.wheel_Treasure3Tickets);
                this.setTickets(3, returnData.wheel_Treasure4Tickets);
                this.setTickets(4, returnData.wheel_Treasure5Tickets);
                if (returnData.m_cardData != null)
                {
                    GameEngine.Instance.World.UserCardData = returnData.m_cardData;
                }
                if (returnData.achievements != null)
                {
                    InterfaceMgr.Instance.processAchievements(returnData.achievements);
                }
                if (returnData.m_newQuestsData != null)
                {
                    this.setNewQuestData(returnData.m_newQuestsData);
                }
                TreasureCastle_AttackGap = returnData.m_treasureCastle_AttackGap;
            }
        }

        public List<int> getAchievementsToTest(ref List<AchievementData> achievementData)
        {
            achievementData = new List<AchievementData>();
            List<int> list = new List<int>();
            List<int> userAchievements = RemoteServices.Instance.UserAchievements;
            if (userAchievements != null)
            {
                double num = this.getCurrentGold();
                if (num >= 20000.0)
                {
                    if (!userAchievements.Contains(100))
                    {
                        list.Add(100);
                    }
                    if (num >= 200000.0)
                    {
                        if (!userAchievements.Contains(0x10000064))
                        {
                            list.Add(0x10000064);
                        }
                        if (num >= 1000000.0)
                        {
                            if (!userAchievements.Contains(0x20000064))
                            {
                                list.Add(0x20000064);
                            }
                            if (num >= 5000000.0)
                            {
                                if (!userAchievements.Contains(0x40000064))
                                {
                                    list.Add(0x40000064);
                                }
                                if (num >= 10000000.0)
                                {
                                    if (!userAchievements.Contains(0x50000064))
                                    {
                                        list.Add(0x50000064);
                                    }
                                    if (num >= 20000000.0)
                                    {
                                        if (!userAchievements.Contains(0x60000064))
                                        {
                                            list.Add(0x60000064);
                                        }
                                        if ((num >= 50000000.0) && !userAchievements.Contains(0x70000064))
                                        {
                                            list.Add(0x70000064);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                int num2 = this.numUserParishes();
                if (num2 >= 1)
                {
                    if (!userAchievements.Contains(0x181))
                    {
                        list.Add(0x181);
                    }
                    if (num2 >= 2)
                    {
                        if (!userAchievements.Contains(0x10000181))
                        {
                            list.Add(0x10000181);
                        }
                        if (num2 >= 3)
                        {
                            if (!userAchievements.Contains(0x20000181))
                            {
                                list.Add(0x20000181);
                            }
                            if ((num2 >= 4) && !userAchievements.Contains(0x40000181))
                            {
                                list.Add(0x40000181);
                            }
                        }
                    }
                }
                int num3 = this.numUserCounties();
                if (num3 >= 1)
                {
                    if (!userAchievements.Contains(0x182))
                    {
                        list.Add(0x182);
                    }
                    if (num3 >= 2)
                    {
                        if (!userAchievements.Contains(0x10000182))
                        {
                            list.Add(0x10000182);
                        }
                        if (num3 >= 3)
                        {
                            if (!userAchievements.Contains(0x20000182))
                            {
                                list.Add(0x20000182);
                            }
                            if ((num3 >= 4) && !userAchievements.Contains(0x40000182))
                            {
                                list.Add(0x40000182);
                            }
                        }
                    }
                }
                int num4 = this.numUserProvinces();
                if (num4 >= 1)
                {
                    if (!userAchievements.Contains(0x183))
                    {
                        list.Add(0x183);
                    }
                    if (num4 >= 2)
                    {
                        if (!userAchievements.Contains(0x10000183))
                        {
                            list.Add(0x10000183);
                        }
                        if (num4 >= 3)
                        {
                            if (!userAchievements.Contains(0x20000183))
                            {
                                list.Add(0x20000183);
                            }
                            if ((num4 >= 4) && !userAchievements.Contains(0x40000183))
                            {
                                list.Add(0x40000183);
                            }
                        }
                    }
                }
                int num5 = this.numUserCountries();
                if (num5 >= 1)
                {
                    if (!userAchievements.Contains(0x184))
                    {
                        list.Add(0x184);
                    }
                    if (num5 >= 2)
                    {
                        if (!userAchievements.Contains(0x10000184))
                        {
                            list.Add(0x10000184);
                        }
                        if (num5 >= 3)
                        {
                            if (!userAchievements.Contains(0x20000184))
                            {
                                list.Add(0x20000184);
                            }
                            if ((num5 >= 4) && !userAchievements.Contains(0x40000184))
                            {
                                list.Add(0x40000184);
                            }
                        }
                    }
                }
                int num6 = 0;
                if (this.UserResearchData != null)
                {
                    num6 = this.UserResearchData.numBranchesComplete(GameEngine.Instance.LocalWorldData);
                }
                if (num6 >= 1)
                {
                    if (!userAchievements.Contains(0xe1))
                    {
                        list.Add(0xe1);
                    }
                    if (num6 >= 2)
                    {
                        if (!userAchievements.Contains(0x100000e1))
                        {
                            list.Add(0x100000e1);
                        }
                        if (num6 >= 3)
                        {
                            if (!userAchievements.Contains(0x200000e1))
                            {
                                list.Add(0x200000e1);
                            }
                            if ((num6 >= 4) && !userAchievements.Contains(0x400000e1))
                            {
                                list.Add(0x400000e1);
                            }
                        }
                    }
                }
                FactionData yourFaction = this.YourFaction;
                if (yourFaction == null)
                {
                    return list;
                }
                int houseID = yourFaction.houseID;
                if (houseID > 0)
                {
                    List<int> list3 = new List<int>();
                    int num8 = 0;
                    foreach (WorldPointList list4 in this.countyList)
                    {
                        if (this.getHouse(list4.factionID) == houseID)
                        {
                            num8++;
                            list3.Add(list4.capitalVillage);
                        }
                    }
                    List<int> list5 = new List<int>();
                    int num9 = 0;
                    foreach (WorldPointList list6 in this.provincesList)
                    {
                        if (this.getHouse(list6.factionID) == houseID)
                        {
                            num9++;
                            list5.Add(list6.capitalVillage);
                        }
                    }
                    List<int> list7 = new List<int>();
                    int num10 = 0;
                    foreach (WorldPointList list8 in this.countryList)
                    {
                        if (this.getHouse(list8.factionID) == houseID)
                        {
                            num10++;
                            list7.Add(list8.capitalVillage);
                        }
                    }
                    if ((num8 >= 1) && !userAchievements.Contains(0xc2))
                    {
                        list.Add(0xc2);
                        foreach (int num11 in list3)
                        {
                            AchievementData item = new AchievementData {
                                data = num11,
                                achievement = 0xc2
                            };
                            achievementData.Add(item);
                        }
                    }
                    if ((num9 >= 1) && !userAchievements.Contains(0x100000c2))
                    {
                        list.Add(0x100000c2);
                        foreach (int num12 in list5)
                        {
                            AchievementData data3 = new AchievementData {
                                data = num12,
                                achievement = 0x100000c2
                            };
                            achievementData.Add(data3);
                        }
                    }
                    if (num10 >= 1)
                    {
                        if (!userAchievements.Contains(0x200000c2))
                        {
                            list.Add(0x200000c2);
                            foreach (int num13 in list7)
                            {
                                AchievementData data4 = new AchievementData {
                                    data = num13,
                                    achievement = 0x200000c2
                                };
                                achievementData.Add(data4);
                            }
                        }
                        if ((num10 > 1) && !userAchievements.Contains(0x400000c2))
                        {
                            list.Add(0x400000c2);
                            foreach (int num14 in list7)
                            {
                                AchievementData data5 = new AchievementData {
                                    data = num14,
                                    achievement = 0x400000c2
                                };
                                achievementData.Add(data5);
                            }
                        }
                    }
                }
                VillageMap.getCurrentServerTime();
                if (this.FactionMembers != null)
                {
                    foreach (FactionMemberData data6 in this.FactionMembers)
                    {
                        if (data6.userID == RemoteServices.Instance.UserID)
                        {
                            TimeSpan span = (TimeSpan) (VillageMap.getCurrentServerTime() - data6.dateJoined);
                            if (span.TotalDays >= 14.0)
                            {
                                if (!userAchievements.Contains(0xc3))
                                {
                                    list.Add(0xc3);
                                }
                                if (span.TotalDays < 70.0)
                                {
                                    return list;
                                }
                                if (!userAchievements.Contains(0x100000c3))
                                {
                                    list.Add(0x100000c3);
                                }
                                if (span.TotalDays < 182.0)
                                {
                                    return list;
                                }
                                if (!userAchievements.Contains(0x200000c3))
                                {
                                    list.Add(0x200000c3);
                                }
                                if ((span.TotalDays >= 364.0) && !userAchievements.Contains(0x400000c3))
                                {
                                    list.Add(0x400000c3);
                                }
                            }
                            return list;
                        }
                    }
                }
            }
            return list;
        }

        public void getActivePeople()
        {
            RemoteServices.Instance.set_GetActivePeople_UserCallBack(new RemoteServices.GetActivePeople_UserCallBack(this.getActivePeopleCallback));
            RemoteServices.Instance.GetActivePeople(this.lastPersonTime);
        }

        public void getActivePeopleCallback(GetActivePeople_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.loadingErrored = false;
                this.importOrphanedPeople(returnData.people, returnData.currentTime, -2);
                this.lastPersonTime = returnData.currentTime;
            }
            else
            {
                this.loadingErrored = true;
            }
        }

        public int[] getActiveQuests()
        {
            if ((this.m_tutorialInfo != null) && (this.m_tutorialInfo.questsActive != null))
            {
                return this.m_tutorialInfo.questsActive;
            }
            return new int[0];
        }

        public void getActiveTraders()
        {
            RemoteServices.Instance.set_GetActiveTraders_UserCallBack(new RemoteServices.GetActiveTraders_UserCallBack(this.getActiveTradersCallback));
            RemoteServices.Instance.GetActiveTraders(this.lastTraderTime);
        }

        public void getActiveTradersCallback(GetActiveTraders_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.loadingErrored = false;
                this.importOrphanedTraders(returnData.traders, returnData.currentTime, -2);
                this.lastTraderTime = returnData.currentTime;
            }
            else
            {
                this.loadingErrored = true;
            }
        }

        public int getAIInvasionMarkerState(int villageID)
        {
            if (this.invasionMarkerState[villageID] == null)
            {
                return 0;
            }
            return (int) this.invasionMarkerState[villageID];
        }

        public SparseArray getAllFactions()
        {
            return this.m_factionData;
        }

        public void getAllVillageOwnerFactionsCallback(GetAllVillageOwnerFactions_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.loadingErrored = false;
                this.processVillageFactionList(returnData.villageOwnerFactions, returnData.currentVillageChangePos);
                if (returnData.factionsList != null)
                {
                    this.m_factionData.Clear();
                    this.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
                }
            }
            else
            {
                this.loadingErrored = true;
                this.m_downloadedDataSafely = false;
            }
        }

        public Color getAreaColour(int areaCol, WorldPointList wpl)
        {
            Color color = areaColorList[areaCol];
            if (!this.bLinelessMap || (wpl.experimentalColourVariant <= 0))
            {
                return color;
            }
            int num = 2;
            switch (areaCol)
            {
                case 0:
                    num = 2;
                    break;

                case 1:
                    num = 5;
                    break;

                case 2:
                    num = 4;
                    break;

                case 3:
                    num = 4;
                    break;

                case 4:
                    num = 5;
                    break;

                case 5:
                    num = 4;
                    break;

                case 6:
                    num = 8;
                    break;

                case 7:
                    num = 4;
                    break;

                case 8:
                    num = 4;
                    break;

                case 9:
                    num = 3;
                    break;

                case 10:
                    num = 8;
                    break;

                case 11:
                    num = 4;
                    break;

                case 12:
                    num = 4;
                    break;

                case 13:
                    num = 4;
                    break;

                case 14:
                    num = 5;
                    break;

                case 15:
                    num = 4;
                    break;

                case 0x10:
                    num = 5;
                    break;

                case 0x11:
                    num = 4;
                    break;

                case 0x12:
                    num = 3;
                    break;

                case 0x13:
                    num = 5;
                    break;

                case 20:
                    num = 3;
                    break;
            }
            int num2 = 100 - (wpl.experimentalColourVariant * num);
            int red = (color.R * num2) / 100;
            int green = (color.G * num2) / 100;
            int blue = (color.B * num2) / 100;
            return Color.FromArgb(0xff, red, green, blue);
        }

        public void getAreaFactionChangesCallback(GetAreaFactionChanges_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.loadingErrored = false;
                this.updateRegionFactions(returnData.regionUpdateList, returnData.regionFactions, returnData.currentRegionChangePos);
                this.updateCountyFactions(returnData.countyUpdateList, returnData.countyFactions, returnData.currentCountyChangePos);
                this.updateProvinceFactions(returnData.provinceUpdateList, returnData.provinceFactions, returnData.currentProvinceChangePos);
                this.updateCountryFactions(returnData.countryUpdateList, returnData.countryFactions, returnData.currentCountryChangePos);
                this.updateParishFlags(returnData.parishFlagChanges, returnData.currentParishFlagPos);
                this.updateCountyFlags(returnData.countyFlagChanges, returnData.currentCountyFlagPos);
                this.updateProvinceFlags(returnData.provinceFlagChanges, returnData.currentProvinceFlagPos);
                this.updateCountryFlags(returnData.countryFlagChanges, returnData.currentCountryFlagPos);
            }
            else
            {
                this.loadingErrored = true;
                this.m_downloadedDataSafely = false;
            }
        }

        public void getArmiesIfNewAttacks()
        {
            RemoteServices.Instance.GetArmyData(this.highestDownloadedArmy);
        }

        public LocalArmyData getArmy(long armyID)
        {
            if (armyID >= 0L)
            {
                return (LocalArmyData) this.armyArray[armyID];
            }
            return null;
        }

        public SparseArray getArmyArray()
        {
            return this.armyArray;
        }

        public void getArmyData(GetArmyData_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.m_newQuestsData != null)
                {
                    this.setNewQuestData(returnData.m_newQuestsData);
                }
                this.loadingErrored = false;
                SparseArray array = new SparseArray();
                if (returnData.existingArmies != null)
                {
                    foreach (long num in returnData.existingArmies)
                    {
                        array[num] = num;
                    }
                }
                if (returnData.armyData != null)
                {
                    this.doGetArmyData(returnData.armyData, returnData.reinforcementData, true);
                    this.highestArmySeen = returnData.armyHighestSeen;
                    foreach (LocalArmyData data in this.armyArray)
                    {
                        data.singlyAdded = false;
                    }
                    if (returnData.existingArmies != null)
                    {
                        foreach (ArmyReturnData data2 in returnData.armyData)
                        {
                            if (array[data2.armyID] == null)
                            {
                                array[data2.armyID] = data2.armyID;
                                returnData.existingArmies.Add(data2.armyID);
                            }
                        }
                    }
                    GameEngine.Instance.setServerDownTime(returnData.serverDowntime);
                }
                List<long> list = new List<long>();
                if (returnData.armyDataNew != null)
                {
                    this.doGetArmyData(returnData.armyDataNew, returnData.reinforcementData, false);
                    foreach (LocalArmyData data3 in this.armyArray)
                    {
                        list.Add(data3.armyID);
                        data3.singlyAdded = false;
                    }
                    if (returnData.existingArmies != null)
                    {
                        foreach (ArmyReturnData data4 in returnData.armyDataNew)
                        {
                            if (array[data4.armyID] == null)
                            {
                                array[data4.armyID] = data4.armyID;
                                returnData.existingArmies.Add(data4.armyID);
                            }
                        }
                    }
                }
                else if (returnData.existingArmies != null)
                {
                    foreach (LocalArmyData data5 in this.armyArray)
                    {
                        list.Add(data5.armyID);
                    }
                }
                if (returnData.existingArmies != null)
                {
                    List<long> existingArmies = returnData.existingArmies;
                    existingArmies.AddRange(this.rememberedExistingArmies);
                    array.Clear();
                    foreach (long num2 in existingArmies)
                    {
                        array[num2] = num2;
                    }
                    foreach (long num3 in list)
                    {
                        if (array[num3] == null)
                        {
                            LocalArmyData data6 = (LocalArmyData) this.armyArray[num3];
                            if (data6 != null)
                            {
                                if (data6.attackType == 13)
                                {
                                    TimeSpan span = (TimeSpan) (VillageMap.getCurrentServerTime() - data6.serverEndTime);
                                    if (span.TotalSeconds < 10.0)
                                    {
                                        continue;
                                    }
                                }
                                if ((RemoteServices.Instance.UserID == data6.userID) && (data6.lootType >= 0))
                                {
                                    data6.localEndTime = data6.localStartTime + 1.0;
                                    data6.updatePosition();
                                }
                            }
                            this.armyArray[num3] = null;
                        }
                    }
                }
                long armyID = -1L;
                foreach (LocalArmyData data7 in this.armyArray)
                {
                    if ((data7.armyID > armyID) && !data7.singlyAdded)
                    {
                        armyID = data7.armyID;
                    }
                }
                this.highestDownloadedArmy = armyID;
            }
            else
            {
                this.loadingErrored = true;
            }
            if (this.doSelectTutorialArmy)
            {
                this.doSelectTutorialArmy = false;
                InterfaceMgr.Instance.selectTutorialArmy();
            }
            this.rememberedExistingArmies.Clear();
        }

        public List<int> getCapitalList()
        {
            List<int> list = new List<int>();
            foreach (VillageData data in this.villageList)
            {
                if (data.Capital && data.visible)
                {
                    list.Add(data.id);
                }
            }
            return list;
        }

        public int getCapitalType(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                if (this.villageList[villageID].regionCapital)
                {
                    return 3;
                }
                if (this.villageList[villageID].countyCapital)
                {
                    return 2;
                }
                if (this.villageList[villageID].provinceCapital)
                {
                    return 1;
                }
                if (this.villageList[villageID].countryCapital)
                {
                    return 0;
                }
            }
            return 4;
        }

        public Color getColorFromArray(Color[] carray, int index)
        {
            if (((carray != null) && (index >= 0)) && (index < carray.Length))
            {
                return carray[index];
            }
            UniversalDebugLog.Log("Tried to fetch a color outside of array: " + index);
            return ARGBColors.White;
        }

        public Color getColorFromFaction(int factionID)
        {
            if (factionID > 20)
            {
                factionID = 0;
            }
            return areaColorList[factionID];
        }

        public int[] getCompletedQuests()
        {
            if ((this.m_tutorialInfo != null) && (this.m_tutorialInfo.questsCompleted != null))
            {
                return this.m_tutorialInfo.questsCompleted;
            }
            return new int[0];
        }

        public List<CounterSpyInfo> getCounterSpyInfo(int villageID)
        {
            List<CounterSpyInfo> list = new List<CounterSpyInfo>();
            if (this.counterSpyInfo != null)
            {
                foreach (CounterSpyInfo info in this.counterSpyInfo)
                {
                    if (info.targetVillageID == villageID)
                    {
                        list.Add(info);
                    }
                }
            }
            return list;
        }

        public int getCountryCapital(int countryID)
        {
            if ((countryID >= 0) && (countryID < this.countryList.Length))
            {
                return this.countryList[countryID].capitalVillage;
            }
            return -1;
        }

        public int getCountryFromVillageID(int villageID)
        {
            int index = this.getProvinceFromVillageID(villageID);
            if (index >= 0)
            {
                return this.provincesList[index].parentID;
            }
            return -1;
        }

        public string getCountryName(int countryID)
        {
            if ((countryID >= 0) && (countryID < this.countryList.Length))
            {
                return this.countryList[countryID].areaName;
            }
            return "";
        }

        public Point getCountyCapitalLocation(int countyID)
        {
            Point point = new Point(-1, -1);
            if ((countyID >= 0) && (countyID < this.countyList.Length))
            {
                int capitalVillage = this.countyList[countyID].capitalVillage;
                if ((capitalVillage >= 0) && (capitalVillage < this.villageList.Length))
                {
                    point.X = this.villageList[capitalVillage].x;
                    point.Y = this.villageList[capitalVillage].y;
                    return point;
                }
            }
            return point;
        }

        public int getCountyCapitalVillage(int countyID)
        {
            if ((countyID >= 0) && (countyID < this.countyList.Length))
            {
                return this.countyList[countyID].capitalVillage;
            }
            return -1;
        }

        public int getCountyFromVillageID(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return this.villageList[villageID].countyID;
            }
            return -1;
        }

        public Point getCountyMarkerLocation(int countyID)
        {
            Point marker = new Point(-1, -1);
            if ((countyID >= 0) && (countyID < this.countyList.Length))
            {
                marker = this.countyList[countyID].marker;
            }
            return marker;
        }

        public string getCountyName(int countyID)
        {
            if ((countyID >= 0) && (countyID < this.countyList.Length))
            {
                return this.countyList[countyID].areaName;
            }
            return "";
        }

        public int getCountyProvince(int countyID)
        {
            if ((countyID >= 0) && (countyID < this.countyList.Length))
            {
                return this.countyList[countyID].parentID;
            }
            return 0;
        }

        public int getCurrentFactionDuration()
        {
            if (this.FactionMembers != null)
            {
                foreach (FactionMemberData data in this.FactionMembers)
                {
                    if (data.userID == RemoteServices.Instance.UserID)
                    {
                        TimeSpan span = (TimeSpan) (VillageMap.getCurrentServerTime() - data.dateJoined);
                        return (((int) span.TotalDays) / 7);
                    }
                }
            }
            return 0;
        }

        public double getCurrentFaithPoints()
        {
            double num2 = (DXTimer.GetCurrentMilliseconds() - this.m_lastFaithPointsUpdate) / 1000.0;
            return (this.m_userFaithPointsLevel + (this.m_userFaithPointsRate * num2));
        }

        public double getCurrentFaithPointsRate()
        {
            return this.m_userFaithPointsRate;
        }

        public double getCurrentGold()
        {
            double num2 = (DXTimer.GetCurrentMilliseconds() - this.m_lastGoldUpdate) / 1000.0;
            double num3 = this.m_userGoldLevel + (this.m_userGoldIncomeRate * num2);
            if (num3 < 0.0)
            {
                num3 = 0.0;
            }
            return num3;
        }

        public double getCurrentGoldRate()
        {
            return this.m_userGoldIncomeRate;
        }

        public double getCurrentHonour()
        {
            double num2 = (DXTimer.GetCurrentMilliseconds() - this.m_lastHonourUpdate) / 1000.0;
            return (this.m_userHonourLevel + (this.m_userHonourIncomeRate * num2));
        }

        public double getCurrentHonourRate()
        {
            return this.m_userHonourIncomeRate;
        }

        public int getCurrentPoints()
        {
            return this.m_userPoints;
        }

        public Image getDummyShield(int width, int height)
        {
            Image sourceImage = Shield.RenderDummyShield(width, height);
            return this.shieldOverlay(sourceImage, width, height, width, height);
        }

        public Image getDummyShield(int width, int height, int bmapWidth, int bmapHeight)
        {
            Image sourceImage = Shield.RenderDummyShield(width, height, bmapWidth, bmapHeight);
            return this.shieldOverlay(sourceImage, width, height, bmapWidth, bmapHeight);
        }

        public string getExchangeName(int villageID)
        {
            if (this.isRegionCapital(villageID))
            {
                return this.getParishNameFromVillageID(villageID);
            }
            return this.getVillageName(villageID);
        }

        public DateTime getExcommunicationTime(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return this.villageList[villageID].excommunicationTime;
            }
            return DateTime.MinValue;
        }

        public FactionData getFaction(int factionID)
        {
            return (FactionData) this.m_factionData[factionID];
        }

        public FactionData getFactionLeadingHouse(int houseID)
        {
            foreach (FactionData data in this.m_factionData)
            {
                if ((data.houseID == houseID) && (data.houseRank == 10))
                {
                    return data;
                }
            }
            return null;
        }

        public FactionMemberData[] getFactionMemberData(int factionID, ref bool uptodate)
        {
            if (factionID == RemoteServices.Instance.UserFactionID)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastTimeOwnMembersUpdated);
                if (span.TotalMinutes < 1.0)
                {
                    uptodate = true;
                }
                else
                {
                    uptodate = false;
                }
                return this.FactionMembers;
            }
            uptodate = false;
            if (this.cachedFactionMemberData[factionID] == null)
            {
                return null;
            }
            FactionCachedMemberData data = (FactionCachedMemberData) this.cachedFactionMemberData[factionID];
            TimeSpan span2 = (TimeSpan) (DateTime.Now - data.lastRefreshed);
            if (span2.TotalMinutes < 3.0)
            {
                uptodate = true;
            }
            return data.memberData;
        }

        public string getFactionName(int factionID)
        {
            FactionData data = this.getFaction(factionID);
            if (data == null)
            {
                return "";
            }
            return data.factionName;
        }

        public int getFactionRank(int factionID)
        {
            FactionData data = this.getFaction(factionID);
            if (data == null)
            {
                return -1;
            }
            int points = data.points;
            int num2 = 0;
            foreach (FactionData data2 in this.m_factionData)
            {
                if (data2.factionID != data.factionID)
                {
                    if (data2.points > points)
                    {
                        num2++;
                    }
                    else if ((data2.points == points) && (data2.factionID < data.factionID))
                    {
                        num2++;
                    }
                }
            }
            return num2;
        }

        public int getGameDay()
        {
            TimeSpan span = (TimeSpan) (VillageMap.getCurrentServerTime() - this.m_worldStartDate);
            return (int) span.TotalDays;
        }

        public int GetGlobalWorldID()
        {
            return this.m_globalWorldID;
        }

        public int getGloryPoints(int houseID)
        {
            if (((houseID > 0) && (this.m_gloryPoints != null)) && (houseID < this.m_gloryPoints.Length))
            {
                return this.m_gloryPoints[houseID];
            }
            return 0;
        }

        public int getGloryRank(int houseID)
        {
            int[,] numArray = new int[20, 2];
            int num = 0;
            for (int i = 0; i < 20; i++)
            {
                if (!GameEngine.Instance.World.HouseInfo[i + 1].loser)
                {
                    numArray[i, 0] = GameEngine.Instance.World.HouseGloryPoints[i + 1];
                }
                else
                {
                    numArray[i, 0] = -1;
                    num++;
                }
                numArray[i, 1] = i;
            }
            for (int j = 0; j < 0x13; j++)
            {
                for (int m = 0; m < 0x13; m++)
                {
                    if (numArray[m, 0] < numArray[m + 1, 0])
                    {
                        int num5 = numArray[m, 0];
                        numArray[m, 0] = numArray[m + 1, 0];
                        numArray[m + 1, 0] = num5;
                        num5 = numArray[m, 1];
                        numArray[m, 1] = numArray[m + 1, 1];
                        numArray[m + 1, 1] = num5;
                    }
                }
            }
            for (int k = 0; k < (20 - num); k++)
            {
                int num7 = numArray[k, 1] + 1;
                if (num7 == houseID)
                {
                    return k;
                }
            }
            return -1;
        }

        public long getHighestReadID(int parishID, int pageID)
        {
            if (this.m_parishChatLog[parishID] == null)
            {
                return -1L;
            }
            ParishChatData data = (ParishChatData) this.m_parishChatLog[parishID];
            List<Chat_TextEntry> list = data.m_pages[pageID];
            if (list.Count <= 0)
            {
                return -1L;
            }
            long textID = -1L;
            foreach (Chat_TextEntry entry in list)
            {
                if (entry.textID > textID)
                {
                    textID = entry.textID;
                }
            }
            return textID;
        }

        public int getHouse(int factionID)
        {
            if (factionID < 0)
            {
                return 0;
            }
            try
            {
                FactionData data = (FactionData) this.m_factionData[factionID];
                if (data != null)
                {
                    if ((data.houseID < 0) || (data.houseID > 20))
                    {
                        return 0;
                    }
                    return data.houseID;
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public FactionData[] getHouseFactions(int houseID)
        {
            List<FactionData> list = new List<FactionData>();
            if (this.m_factionData != null)
            {
                foreach (FactionData data in this.m_factionData)
                {
                    if (data.houseID == houseID)
                    {
                        list.Add(data);
                    }
                }
            }
            return list.ToArray();
        }

        public DateTime getInterdictTime(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return this.villageList[villageID].interdictionTime;
            }
            return DateTime.MinValue;
        }

        public void GetInvasionInfo_callback(GetInvasionInfo_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.aiWorldGloryWinLevel = returnData.gloryRoundWinPoints;
                this.invasionInfo = returnData.invasions;
                if (this.invasionInfo == null)
                {
                    this.invasionInfo = new List<AIWorldInvasionData>();
                }
                for (int i = 0; i < this.invasionInfo.Count; i++)
                {
                    AIWorldInvasionData item = this.invasionInfo[i];
                    if (item.invasionID == -12345)
                    {
                        AIWorldInvasionData data2 = this.invasionInfo[i + 1];
                        this.wolfsRevengeStart = item.date;
                        this.wolfsRevengeEnd = data2.date;
                        this.invasionInfo.Remove(item);
                        this.invasionInfo.Remove(data2);
                        break;
                    }
                }
                this.updateAIInvasions();
            }
        }

        public void getLastAttackerCallback(GetLastAttacker_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.lastAttacker = returnData.lastAttacker;
                this.newPlayer = returnData.newPlayer;
                this.lastAttackerLastUpdate = DateTime.Now;
                InterfaceMgr.Instance.ParentForm.Enabled = false;
                if (!this.newPlayer)
                {
                    GameEngine.Instance.openLostVillage(0);
                }
                else
                {
                    GameEngine.Instance.openSimpleSelectVillage();
                }
            }
            this.inUpdateLastAttackerInfo = false;
        }

        public DateTime getLastLeaderboardUpdate()
        {
            return this.leaderboardLastUpdateTime;
        }

        public DateTime getLastTreasureCastleAttackTime()
        {
            return this.m_lastTreasureCastleAttackTime;
        }

        public SparseArray getLeaderboardArray(int mode)
        {
            switch (mode)
            {
                case -6:
                    return this.leaderboard_MainVillages;

                case -5:
                    return this.leaderboard_MainRank;

                case -4:
                    return this.leaderboard_ParishFlags;

                case -3:
                    return this.leaderboard_Houses;

                case -2:
                    return this.leaderboard_Factions;

                case -1:
                    return this.leaderboard_Main;

                case 0:
                    return this.leaderboard_Sub_Pillager;

                case 1:
                    return this.leaderboard_Sub_Defender;

                case 2:
                    return this.leaderboard_Sub_Ransack;

                case 3:
                    return this.leaderboard_Sub_Wolfsbane;

                case 4:
                    return this.leaderboard_Sub_Banditkiller;

                case 5:
                    return this.leaderboard_Sub_AIKiller;

                case 6:
                    return this.leaderboard_Sub_Trader;

                case 7:
                    return this.leaderboard_Sub_Forager;

                case 8:
                    return this.leaderboard_Sub_Stockpiler;

                case 9:
                    return this.leaderboard_Sub_Farmer;

                case 10:
                    return this.leaderboard_Sub_Brewer;

                case 11:
                    return this.leaderboard_Sub_Weaponsmith;

                case 12:
                    return this.leaderboard_Sub_banquetter;

                case 13:
                    return this.leaderboard_Sub_Achiever;

                case 14:
                    return this.leaderboard_Sub_Donater;

                case 15:
                    return this.leaderboard_Sub_Capture;

                case 0x10:
                    return this.leaderboard_Sub_Raze;

                case 0x11:
                    return this.leaderboard_Sub_Glory;
            }
            return null;
        }

        public LeaderBoardEntryData getLeaderboardEntry(int mode, int position, int pageSize)
        {
            DateTime minValue = DateTime.MinValue;
            int num = -1;
            SparseArray currentArray = null;
            switch (mode)
            {
                case -6:
                    currentArray = this.leaderboard_MainVillages;
                    num = this.max_leaderboard_MainVillages;
                    minValue = this.lastZeroDownload_leaderboard_MainVillages;
                    break;

                case -5:
                    currentArray = this.leaderboard_MainRank;
                    num = this.max_leaderboard_MainRank;
                    minValue = this.lastZeroDownload_leaderboard_MainRank;
                    break;

                case -4:
                    currentArray = this.leaderboard_ParishFlags;
                    num = this.max_leaderboard_ParishFlags;
                    minValue = this.lastZeroDownload_leaderboard_ParishFlags;
                    break;

                case -3:
                    currentArray = this.leaderboard_Houses;
                    num = this.max_leaderboard_Houses;
                    minValue = this.lastZeroDownload_leaderboard_Houses;
                    break;

                case -2:
                    currentArray = this.leaderboard_Factions;
                    num = this.max_leaderboard_Factions;
                    minValue = this.lastZeroDownload_leaderboard_Factions;
                    break;

                case -1:
                    currentArray = this.leaderboard_Main;
                    num = this.max_leaderboard_Main;
                    minValue = this.lastZeroDownload_leaderboard_Main;
                    break;

                case 0:
                    currentArray = this.leaderboard_Sub_Pillager;
                    num = this.max_leaderboard_Sub_Pillager;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Pillager;
                    break;

                case 1:
                    currentArray = this.leaderboard_Sub_Defender;
                    num = this.max_leaderboard_Sub_Defender;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Defender;
                    break;

                case 2:
                    currentArray = this.leaderboard_Sub_Ransack;
                    num = this.max_leaderboard_Sub_Ransack;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Ransack;
                    break;

                case 3:
                    currentArray = this.leaderboard_Sub_Wolfsbane;
                    num = this.max_leaderboard_Sub_Wolfsbane;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Wolfsbane;
                    break;

                case 4:
                    currentArray = this.leaderboard_Sub_Banditkiller;
                    num = this.max_leaderboard_Sub_Banditkiller;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Banditkiller;
                    break;

                case 5:
                    currentArray = this.leaderboard_Sub_AIKiller;
                    num = this.max_leaderboard_Sub_AIKiller;
                    minValue = this.lastZeroDownload_leaderboard_Sub_AIKiller;
                    break;

                case 6:
                    currentArray = this.leaderboard_Sub_Trader;
                    num = this.max_leaderboard_Sub_Trader;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Trader;
                    break;

                case 7:
                    currentArray = this.leaderboard_Sub_Forager;
                    num = this.max_leaderboard_Sub_Forager;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Forager;
                    break;

                case 8:
                    currentArray = this.leaderboard_Sub_Stockpiler;
                    num = this.max_leaderboard_Sub_Stockpiler;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Stockpiler;
                    break;

                case 9:
                    currentArray = this.leaderboard_Sub_Farmer;
                    num = this.max_leaderboard_Sub_Farmer;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Farmer;
                    break;

                case 10:
                    currentArray = this.leaderboard_Sub_Brewer;
                    num = this.max_leaderboard_Sub_Brewer;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Brewer;
                    break;

                case 11:
                    currentArray = this.leaderboard_Sub_Weaponsmith;
                    num = this.max_leaderboard_Sub_Weaponsmith;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Weaponsmith;
                    break;

                case 12:
                    currentArray = this.leaderboard_Sub_banquetter;
                    num = this.max_leaderboard_Sub_banquetter;
                    minValue = this.lastZeroDownload_leaderboard_Sub_banquetter;
                    break;

                case 13:
                    currentArray = this.leaderboard_Sub_Achiever;
                    num = this.max_leaderboard_Sub_Achiever;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Achiever;
                    break;

                case 14:
                    currentArray = this.leaderboard_Sub_Donater;
                    num = this.max_leaderboard_Sub_Donater;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Donater;
                    break;

                case 15:
                    currentArray = this.leaderboard_Sub_Capture;
                    num = this.max_leaderboard_Sub_Capture;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Capture;
                    break;

                case 0x10:
                    currentArray = this.leaderboard_Sub_Raze;
                    num = this.max_leaderboard_Sub_Raze;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Raze;
                    break;

                case 0x11:
                    currentArray = this.leaderboard_Sub_Glory;
                    num = this.max_leaderboard_Sub_Glory;
                    minValue = this.lastZeroDownload_leaderboard_Sub_Glory;
                    break;
            }
            if (currentArray == null)
            {
                return null;
            }
            if (num <= 0)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - minValue);
                if (span.TotalMinutes < 1.0)
                {
                    return null;
                }
            }
            if (currentArray.Count == 0)
            {
                this.downloadLeaderboard(currentArray, mode, -1, pageSize);
                return null;
            }
            if (position < 0)
            {
                return null;
            }
            if (currentArray[position] != null)
            {
                return (LeaderBoardEntryData) currentArray[position];
            }
            if (position <= num)
            {
                this.downloadLeaderboard(currentArray, mode, position, pageSize);
                return null;
            }
            if (num < 0)
            {
                return null;
            }
            if (dummyEntry == null)
            {
                dummyEntry = new LeaderBoardEntryData();
                dummyEntry.dummy = true;
            }
            return dummyEntry;
        }

        public LeaderBoardSelfRankings getLeaderboardSelfStanding(int position)
        {
            if (position >= this.leaderboardSelfRankings.Count)
            {
                return null;
            }
            return this.leaderboardSelfRankings[position];
        }

        public List<int> getListOfUserCounties()
        {
            List<int> list = new List<int>();
            foreach (int num in this.getListOfUserVillages())
            {
                int item = this.getCountyFromVillageID(num);
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public List<int> getListOfUserCountries()
        {
            List<int> list = new List<int>();
            foreach (int num in this.getListOfUserVillages())
            {
                int item = this.getCountryFromVillageID(num);
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public List<int> getListOfUserParishes()
        {
            List<int> list = new List<int>();
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    if (!data.capital)
                    {
                        int regionID = this.villageList[data.villageID].regionID;
                        if (!list.Contains(regionID))
                        {
                            list.Add(regionID);
                        }
                    }
                }
            }
            return list;
        }

        public List<int> getListOfUserProvinces()
        {
            List<int> list = new List<int>();
            foreach (int num in this.getListOfUserVillages())
            {
                int item = this.getProvinceFromVillageID(num);
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public List<int> getListOfUserVillages()
        {
            List<int> list = new List<int>();
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    if (!data.capital)
                    {
                        list.Add(data.villageID);
                    }
                }
            }
            return list;
        }

        public List<LoginHistoryInfo> getLoginHistory(bool request)
        {
            if (this.loginHistory != null)
            {
                return this.loginHistory;
            }
            if (request)
            {
                RemoteServices.Instance.set_GetLoginHistory_UserCallBack(new RemoteServices.GetLoginHistory_UserCallBack(this.getLoginHistoryCallback));
                RemoteServices.Instance.GetLoginHistory();
            }
            return null;
        }

        public void getLoginHistoryCallback(GetLoginHistory_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.loginHistory = returnData.history;
            }
        }

        public void getMapCoords(Point mousePos, ref double mapPosX, ref double mapPosY)
        {
            mapPosX = ((mousePos.X - (((double) this.m_screenWidth) / 2.0)) / this.m_worldScale) + this.m_screenCentreX;
            mapPosY = ((mousePos.Y - (((double) this.m_screenHeight) / 2.0)) / this.m_worldScale) + this.m_screenCentreY;
            if (mapPosX < 0.0)
            {
                mapPosX = 0.0;
            }
            else if (mapPosX >= this.worldMapWidth)
            {
                mapPosX = this.worldMapWidth - 1;
            }
            if (mapPosY < 0.0)
            {
                mapPosY = 0.0;
            }
            else if (mapPosY >= this.worldMapHeight)
            {
                mapPosY = this.worldMapHeight - 1;
            }
        }

        public int getMaxLeaderboardEntries(int mode)
        {
            switch (mode)
            {
                case -6:
                    return this.max_leaderboard_MainVillages;

                case -5:
                    return this.max_leaderboard_MainRank;

                case -4:
                    return this.max_leaderboard_ParishFlags;

                case -3:
                    return this.max_leaderboard_Houses;

                case -2:
                    return this.max_leaderboard_Factions;

                case -1:
                    return this.max_leaderboard_Main;

                case 0:
                    return this.max_leaderboard_Sub_Pillager;

                case 1:
                    return this.max_leaderboard_Sub_Defender;

                case 2:
                    return this.max_leaderboard_Sub_Ransack;

                case 3:
                    return this.max_leaderboard_Sub_Wolfsbane;

                case 4:
                    return this.max_leaderboard_Sub_Banditkiller;

                case 5:
                    return this.max_leaderboard_Sub_AIKiller;

                case 6:
                    return this.max_leaderboard_Sub_Trader;

                case 7:
                    return this.max_leaderboard_Sub_Forager;

                case 8:
                    return this.max_leaderboard_Sub_Stockpiler;

                case 9:
                    return this.max_leaderboard_Sub_Farmer;

                case 10:
                    return this.max_leaderboard_Sub_Brewer;

                case 11:
                    return this.max_leaderboard_Sub_Weaponsmith;

                case 12:
                    return this.max_leaderboard_Sub_banquetter;

                case 13:
                    return this.max_leaderboard_Sub_Achiever;

                case 14:
                    return this.max_leaderboard_Sub_Donater;

                case 15:
                    return this.max_leaderboard_Sub_Capture;

                case 0x10:
                    return this.max_leaderboard_Sub_Raze;

                case 0x11:
                    return this.max_leaderboard_Sub_Glory;
            }
            return -1;
        }

        public NewQuestsData getNewQuestData()
        {
            return this.m_newQuestData;
        }

        public int[] getNewQuestList()
        {
            if (this.m_newQuestData != null)
            {
                return this.m_newQuestData.availableQuests;
            }
            return null;
        }

        public DateTime getNextAIInvasionDate(int villageID)
        {
            foreach (AIWorldInvasionData data in this.invasionInfo)
            {
                if (data.invasionVillageID == villageID)
                {
                    return data.date;
                }
            }
            return DateTime.MinValue;
        }

        public int getNextUserVillage(int curVillage, int searchDir)
        {
            if ((this.m_userVillages == null) || (this.m_userVillages.Count == 0))
            {
                return -1;
            }
            int num = -1;
            bool flag = false;
            List<UserVillageData> list = new List<UserVillageData>();
            list.AddRange(this.m_userVillages);
            list.AddRange(this.m_userRelatedVillages);
            if ((curVillage >= 0) && (curVillage < this.villageList.Length))
            {
                num = 0;
                foreach (UserVillageData data in list)
                {
                    if (data.villageID == curVillage)
                    {
                        break;
                    }
                    num++;
                }
                if (this.villageList[curVillage].Capital)
                {
                    flag = true;
                }
            }
            int num2 = 0;
            int count = list.Count;
            int num4 = 0;
            while (num4 < count)
            {
                num += searchDir;
                if (num < 0)
                {
                    num = list.Count - 1;
                }
                if (num >= count)
                {
                    num = 0;
                }
                if (flag)
                {
                    if (this.villageList[list[num].villageID].Capital)
                    {
                        break;
                    }
                }
                else if (!this.villageList[list[num].villageID].Capital)
                {
                    break;
                }
                num2++;
                if (num2 > 0x3e8)
                {
                    return -1;
                }
            }
            return list[num].villageID;
        }

        public int getNumHouseMembers(int houseID)
        {
            int num = 0;
            foreach (FactionData data in this.m_factionData)
            {
                if (data.houseID == houseID)
                {
                    num += data.numMembers;
                }
            }
            return num;
        }

        public int getNumMadeCaptains()
        {
            if (this.m_numMadeCaptains <= 0)
            {
                return 1;
            }
            return this.m_numMadeCaptains;
        }

        public int getNumParishes()
        {
            return this.regionList.Length;
        }

        public double getOrigWorldZoom()
        {
            return (27.0 - this.m_worldZoom);
        }

        public int getParishCapital(int parishID)
        {
            if ((parishID >= 0) && (parishID < this.regionList.Length))
            {
                return this.regionList[parishID].capitalVillage;
            }
            return -1;
        }

        public List<Chat_TextEntry> getParishChat(int parishID, int subForum, DateTime minTime)
        {
            if ((minTime == DateTime.MaxValue) && RemoteServices.Instance.Admin)
            {
                minTime = DateTime.MinValue;
            }
            if (this.m_parishChatLog[parishID] == null)
            {
                return new List<Chat_TextEntry>();
            }
            List<Chat_TextEntry> list = ((ParishChatData) this.m_parishChatLog[parishID]).getChatPage(subForum);
            if (list == null)
            {
                return new List<Chat_TextEntry>();
            }
            List<Chat_TextEntry> list2 = new List<Chat_TextEntry>();
            foreach (Chat_TextEntry entry in list)
            {
                if (entry.postedTime >= minTime)
                {
                    list2.Add(entry);
                }
            }
            if (list2.Count > 100)
            {
                List<Chat_TextEntry> list3 = new List<Chat_TextEntry>();
                for (int i = list2.Count - 100; i < list2.Count; i++)
                {
                    list3.Add(list2[i]);
                }
                list3.Sort(this.parishChatComparer);
                return list3;
            }
            list2.Sort(this.parishChatComparer);
            return list2;
        }

        public DateTime getParishChatNewestPostTime(int parishID, DateTime allowedMinTime)
        {
            DateTime minValue = DateTime.MinValue;
            if (this.m_parishChatLog[parishID] != null)
            {
                ParishChatData data = (ParishChatData) this.m_parishChatLog[parishID];
                minValue = data.m_newestPost;
            }
            else
            {
                minValue = VillageMap.getCurrentServerTime().AddDays(-30.0);
            }
            if (minValue < allowedMinTime)
            {
                minValue = allowedMinTime;
            }
            if ((minValue == DateTime.MaxValue) && RemoteServices.Instance.Admin)
            {
                minValue = DateTime.Now.AddDays(-30.0);
            }
            return minValue;
        }

        public int getParishFromVillageID(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return this.villageList[villageID].regionID;
            }
            return -1;
        }

        public int getParishIDFromName(string parishName)
        {
            for (int i = 0; i < this.regionList.Length; i++)
            {
                if (this.regionList[i].areaName == parishName)
                {
                    return i;
                }
            }
            return -1;
        }

        public string getParishName(int parishID)
        {
            if ((parishID >= 0) && (parishID < this.regionList.Length))
            {
                return this.regionList[parishID].areaName;
            }
            return "";
        }

        public string getParishNameFromVillageID(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                int regionID = this.villageList[villageID].regionID;
                return this.regionList[regionID].areaName;
            }
            return "";
        }

        public string[] getParishNameList()
        {
            string[] strArray = new string[this.regionList.Length];
            for (int i = 0; i < this.regionList.Length; i++)
            {
                strArray[i] = this.regionList[i].areaName;
            }
            return strArray;
        }

        public int getParishPlague(int parishID)
        {
            if ((parishID >= 0) && (parishID < this.regionList.Length))
            {
                return this.regionList[parishID].plague;
            }
            return 0;
        }

        public int getParishPlagueLevel(int villageID)
        {
            if (((villageID >= 0) && (villageID < this.villageList.Length)) && (this.villageList[villageID].rolloverInfo != null))
            {
                return this.villageList[villageID].rolloverInfo.plagueLevel;
            }
            return -1;
        }

        public ParishWallDetailInfo_ReturnType getParishWallDonateDetails(int parishCapitalID, int userID)
        {
            long num = (parishCapitalID << 0x20) + userID;
            if (this.m_parishWallDonateDetails[num] != null)
            {
                ParishWallDonateInfo info = (ParishWallDonateInfo) this.m_parishWallDonateDetails[num];
                TimeSpan span = (TimeSpan) (DateTime.Now - info.lastUpdateTime);
                if (span.TotalMinutes < 2.0)
                {
                    return info.returnData;
                }
            }
            return null;
        }

        public DateTime getPeaceTime(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return this.villageList[villageID].peaceTime;
            }
            return DateTime.MinValue;
        }

        public SparseArray getPeopleArray()
        {
            return this.personArray;
        }

        public LocalPerson getPerson(long personID)
        {
            return (LocalPerson) this.personArray[personID];
        }

        public void getPersonData()
        {
            this.lastPersonTime = DateTime.Now.AddYears(-5);
            RemoteServices.Instance.set_GetUserPeople_UserCallBack(new RemoteServices.GetUserPeople_UserCallBack(this.getUserPeopleCallback));
            RemoteServices.Instance.GetUserPeople();
        }

        public int getPlaybackCountryHouse(int day, int countryID)
        {
            if (((day >= 0) && (day < this.playbackTotalDays)) && (countryID < this.playbackMaxCountries))
            {
                return this.playbackCountriesData[day, countryID];
            }
            return 0;
        }

        public int getPlaybackDay()
        {
            if (!this.playingCountries && !this.playingProvinces)
            {
                return -1;
            }
            return (this.playbackDay + this.playbackBasedDay);
        }

        public int getPlaybackProvinceHouse(int day, int provinceID)
        {
            if (((day >= 0) && (day < this.playbackTotalDays)) && (provinceID < this.playbackMaxProvinces))
            {
                return this.playbackProvincesData[day, provinceID];
            }
            return 0;
        }

        public Image getPlayerShieldImage(int width, int height)
        {
            return this.getPlayerShieldImage(width, height, width, height);
        }

        public Image getPlayerShieldImage(int width, int height, int bmapWidth, int bmapHeight)
        {
            if ((this.playerShieldFactory == null) || !this.playerShieldFactory.PlayerAvailable)
            {
                return this.getDummyShield(width, height, bmapWidth, bmapHeight);
            }
            if (this.playerShield == null)
            {
                this.playerShield = this.playerShieldFactory.getPlayerShield();
            }
            if (this.playerShield == null)
            {
                return null;
            }
            Image sourceImage = this.playerShield.Render(width, height, bmapWidth, bmapHeight);
            return this.shieldOverlay(sourceImage, width, height, bmapWidth, bmapHeight);
        }

        public string getPlayerShieldString()
        {
            if ((this.playerShieldFactory != null) && this.playerShieldFactory.PlayerAvailable)
            {
                if (this.playerShield == null)
                {
                    this.playerShield = this.playerShieldFactory.getPlayerShield();
                }
                if (this.playerShield != null)
                {
                    return this.playerShield.getString();
                }
            }
            return "";
        }

        public int getProvinceCapital(int provinceID)
        {
            if ((provinceID >= 0) && (provinceID < this.provincesList.Length))
            {
                return this.provincesList[provinceID].capitalVillage;
            }
            return -1;
        }

        public Point getProvinceCapitalLocation(int provinceID)
        {
            Point point = new Point(-1, -1);
            if ((provinceID >= 0) && (provinceID < this.provincesList.Length))
            {
                int capitalVillage = this.provincesList[provinceID].capitalVillage;
                if ((capitalVillage >= 0) && (capitalVillage < this.villageList.Length))
                {
                    point.X = this.villageList[capitalVillage].x;
                    point.Y = this.villageList[capitalVillage].y;
                    return point;
                }
            }
            return point;
        }

        public int getProvinceCountry(int provinceID)
        {
            if ((provinceID >= 0) && (provinceID < this.provincesList.Length))
            {
                return this.provincesList[provinceID].parentID;
            }
            return 0;
        }

        public int getProvinceFromVillageID(int villageID)
        {
            int index = this.getCountyFromVillageID(villageID);
            if (index >= 0)
            {
                return this.countyList[index].parentID;
            }
            return -1;
        }

        public string getProvinceName(int provinceID)
        {
            if ((provinceID >= 0) && (provinceID < this.provincesList.Length))
            {
                return this.provincesList[provinceID].areaName;
            }
            return "";
        }

        public int getRank()
        {
            return this.m_userRank;
        }

        public int getRankSubLevel()
        {
            return this.m_userRankSubLevel;
        }

        public Point getRegionCapitalLocation(int regionID)
        {
            Point point = new Point(-1, -1);
            if ((regionID >= 0) && (regionID < this.regionList.Length))
            {
                int capitalVillage = this.regionList[regionID].capitalVillage;
                if ((capitalVillage >= 0) && (capitalVillage < this.villageList.Length))
                {
                    point.X = this.villageList[capitalVillage].x;
                    point.Y = this.villageList[capitalVillage].y;
                    return point;
                }
            }
            return point;
        }

        public int getRegionCapitalVillage(int regionID)
        {
            if ((regionID >= 0) && (regionID < this.regionList.Length))
            {
                return this.regionList[regionID].capitalVillage;
            }
            return -1;
        }

        public LocalArmyData getReinforcement(long armyID)
        {
            if (armyID >= 0L)
            {
                return (LocalArmyData) this.reinforcementArray[armyID];
            }
            return null;
        }

        public SparseArray getReinforcementsArray()
        {
            return this.reinforcementArray;
        }

        public void getReinforceTotals(int villageID, ref int numPeasants, ref int numArchers, ref int numPikemen, ref int numSwordsmen)
        {
            int num = 0;
            numPeasants = 0;
            numArchers = 0;
            numPikemen = 0;
            numSwordsmen = 0;
            num = 0;
            foreach (LocalArmyData data in this.reinforcementArray)
            {
                if ((data.targetVillageID == villageID) && (data.serverEndTime < VillageMap.getCurrentServerTime()))
                {
                    numPeasants += data.numPeasants;
                    numArchers += data.numArchers;
                    numPikemen += data.numPikemen;
                    numSwordsmen += data.numSwordsmen;
                    num += data.numCatapults;
                }
            }
        }

        public void getResearchDataCallback(GetResearchData_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.setResearchData(returnData.researchData);
                VillageMap.setServerTime(returnData.currentTime);
                InterfaceMgr.Instance.researchDataChanged(returnData.researchData);
                GameEngine.Instance.World.setPoints(returnData.currentPoints);
                if (((returnData.researchData != null) && ((this.m_lastResearchCompleteTimeMatch != returnData.researchData.research_completionTime) || (returnData.researchData.researchingType < 0))) && (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE))
                {
                    GameEngine.Instance.downloadCurrentVillage();
                }
            }
        }

        public ResearchData GetResearchDataForCurrentVillage()
        {
            int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            if (this.isCapital(villageID) && (GameEngine.Instance.Village != null))
            {
                return GameEngine.Instance.Village.m_parishCapitalResearchData;
            }
            return this.UserResearchData;
        }

        public ResearchData GetResearchDataForVillage(int villageID)
        {
            if (this.isCapital(villageID))
            {
                VillageMap map = GameEngine.Instance.getVillage(villageID);
                if (map != null)
                {
                    return map.m_parishCapitalResearchData;
                }
            }
            return this.UserResearchData;
        }

        public Point getScreenPosFromMapCoords(double mapX, double mapY)
        {
            return new Point { X = (int) (((mapX - this.m_screenCentreX) * this.m_worldScale) + (((double) this.m_screenWidth) / 2.0)), Y = (int) (((mapY - this.m_screenCentreY) * this.m_worldScale) + (((double) this.m_screenHeight) / 2.0)) };
        }

        public int getSpecial(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return this.villageList[villageID].special;
            }
            return 0;
        }

        public SpecialVillageCache getSpecialVillageData(int villageID, bool download)
        {
            SpecialVillageCache cache = null;
            if (villageID >= 0)
            {
                bool flag = false;
                if (this.specialVillageCache[villageID] == null)
                {
                    flag = true;
                }
                else
                {
                    cache = (SpecialVillageCache) this.specialVillageCache[villageID];
                    TimeSpan span2 = (TimeSpan) (DateTime.Now - cache.lastUpdate);
                    if (span2.TotalMinutes > 1.0)
                    {
                        flag = true;
                    }
                    if ((this.villageList[villageID].special > 100) && (this.villageList[villageID].special <= 0xc7))
                    {
                        int num = this.villageList[villageID].special - 100;
                        if (num != cache.resourceType)
                        {
                            this.specialVillageCache[villageID] = null;
                            cache = null;
                        }
                    }
                    else
                    {
                        this.specialVillageCache[villageID] = null;
                        cache = null;
                    }
                }
                if (!flag || (this.lastSpecialRequestSent == villageID))
                {
                    return cache;
                }
                bool flag2 = true;
                if (this.lastActualSpecialRequestSent == villageID)
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - this.lastActualSpecialRequestTime);
                    if (span.TotalMinutes < 1.0)
                    {
                        flag2 = false;
                    }
                }
                if ((flag2 && (this.villageList[villageID].special > 100)) && (this.villageList[villageID].special <= 0xc7))
                {
                    RemoteServices.Instance.set_SpecialVillageInfo_UserCallBack(new RemoteServices.SpecialVillageInfo_UserCallBack(this.specialVillageInfoCallback));
                    RemoteServices.Instance.SpecialVillageInfo(villageID);
                    this.lastSpecialRequestSent = villageID;
                    this.lastActualSpecialRequestSent = villageID;
                    this.lastActualSpecialRequestTime = DateTime.Now;
                }
            }
            return cache;
        }

        public int getSquareDistance(int villageID1, int villageID2)
        {
            return (((this.villageList[villageID1].x - this.villageList[villageID2].x) * (this.villageList[villageID1].x - this.villageList[villageID2].x)) + ((this.villageList[villageID1].y - this.villageList[villageID2].y) * (this.villageList[villageID1].y - this.villageList[villageID2].y)));
        }

        public CachedUserInfo getStoredUserInfo(int userID)
        {
            return (CachedUserInfo) this.cachedUserInfo[userID];
        }

        public int getTickets(int level)
        {
            switch (level)
            {
                case -1:
                    return this.m_numQuestTickets;

                case 0:
                    return this.m_treasure1Tickets;

                case 1:
                    return this.m_treasure2Tickets;

                case 2:
                    return this.m_treasure3Tickets;

                case 3:
                    return this.m_treasure4Tickets;

                case 4:
                    return this.m_treasure5Tickets;
            }
            return 0;
        }

        public int getTotalMerchantsFromVillage(int villageID)
        {
            int num = 0;
            foreach (LocalTrader trader in this.traderArray)
            {
                if (trader.trader.homeVillageID == villageID)
                {
                    num += trader.trader.numTraders;
                }
            }
            return num;
        }

        public void getTotalTroopsOutOfVillage(int villageID, ref int numPeasants, ref int numArchers, ref int numPikemen, ref int numSwordsmen, ref int numCatapults, ref int numCaptains, ref int numReinfPeasants, ref int numReinfArchers, ref int numReinfPikemen, ref int numReinfSwordsmen, ref int numReinfCatapults, ref int numReinfCaptains)
        {
            numPeasants = 0;
            numArchers = 0;
            numPikemen = 0;
            numSwordsmen = 0;
            numCatapults = 0;
            numCaptains = 0;
            numReinfPeasants = 0;
            numReinfArchers = 0;
            numReinfPikemen = 0;
            numReinfSwordsmen = 0;
            numReinfCatapults = 0;
            numReinfCaptains = 0;
            foreach (LocalArmyData data in this.armyArray)
            {
                if ((data.travelFromVillageID == villageID) && (data.homeVillageID == villageID))
                {
                    numPeasants += data.numPeasants;
                    numArchers += data.numArchers;
                    numPikemen += data.numPikemen;
                    numSwordsmen += data.numSwordsmen;
                    numCatapults += data.numCatapults;
                    numCaptains += data.numCaptains;
                }
            }
            foreach (LocalArmyData data2 in this.reinforcementArray)
            {
                if (data2.travelFromVillageID == villageID)
                {
                    numReinfPeasants += data2.numPeasants;
                    numReinfArchers += data2.numArchers;
                    numReinfPikemen += data2.numPikemen;
                    numReinfSwordsmen += data2.numSwordsmen;
                    numReinfCatapults += data2.numCatapults;
                }
            }
        }

        public LocalTrader getTrader(long traderID)
        {
            return (LocalTrader) this.traderArray[traderID];
        }

        public SparseArray getTraderArray()
        {
            return this.traderArray;
        }

        public void getTraderData()
        {
            this.clearTraderArray(-1);
            this.lastTraderTime = DateTime.Now.AddYears(-5);
            RemoteServices.Instance.set_GetUserTraders_UserCallBack(new RemoteServices.GetUserTraders_UserCallBack(this.getUserTradersCallback));
            RemoteServices.Instance.GetUserTraders();
        }

        public int getTradingAmount(long traderID)
        {
            int num = 0;
            if (this.traderArray[traderID] != null)
            {
                LocalTrader trader = (LocalTrader) this.traderArray[traderID];
                num += trader.trader.amount;
                foreach (LocalTrader trader2 in this.traderArray)
                {
                    if (trader2.parentTrader == traderID)
                    {
                        num += trader2.trader.amount;
                    }
                }
            }
            return num;
        }

        public long getTutorialArmyID()
        {
            foreach (LocalArmyData data in this.armyArray)
            {
                if (data.attackType == 13)
                {
                    return data.armyID;
                }
            }
            return -1L;
        }

        public int getTutorialStage()
        {
            if (this.m_tutorialInfo == null)
            {
                return 0;
            }
            if (!this.m_tutorialInfo.tutorialActive)
            {
                return -1;
            }
            return this.m_tutorialInfo.tutorialStage;
        }

        public void getUserPeopleCallback(GetUserPeople_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.loadingErrored = false;
                this.importOrphanedPeople(returnData.people, returnData.currentTime, -2);
            }
            else
            {
                this.loadingErrored = true;
            }
        }

        public int getUserRelationship(int userID)
        {
            foreach (UserRelationship relationship in this.userRelations)
            {
                if (relationship.userID == userID)
                {
                    if (relationship.friendly)
                    {
                        return 1;
                    }
                    return -1;
                }
            }
            return 0;
        }

        public void getUserTradersCallback(GetUserTraders_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.loadingErrored = false;
                this.importOrphanedTraders(returnData.traders, returnData.currentTime, -2);
            }
            else
            {
                this.loadingErrored = true;
            }
        }

        public List<int> getUserVillageIDList()
        {
            List<int> list = new List<int>();
            foreach (UserVillageData data in this.m_userVillages)
            {
                if (!this.villageList[data.villageID].Capital)
                {
                    list.Add(data.villageID);
                }
            }
            return list;
        }

        public List<UserVillageData> getUserVillageList()
        {
            return this.m_userVillages;
        }

        public List<VillageNameItem> getUserVillageNamesList()
        {
            List<VillageNameItem> list = new List<VillageNameItem>();
            if (this.m_userVillages != null)
            {
                int num = 0;
                foreach (UserVillageData data in this.m_userVillages)
                {
                    if (this.villageList[data.villageID].Capital)
                    {
                        int num2 = 1;
                        if (this.villageList[data.villageID].regionCapital)
                        {
                            num2 = 1;
                        }
                        else if (this.villageList[data.villageID].countyCapital)
                        {
                            num2 = 2;
                        }
                        else if (this.villageList[data.villageID].provinceCapital)
                        {
                            num2 = 3;
                        }
                        else if (this.villageList[data.villageID].countryCapital)
                        {
                            num2 = 4;
                        }
                        if (num != num2)
                        {
                            VillageNameItem item = new VillageNameItem {
                                villageName = "-----------------",
                                villageID = -1
                            };
                            list.Add(item);
                            num = num2;
                        }
                    }
                    VillageNameItem item2 = new VillageNameItem {
                        villageName = this.getVillageName(data.villageID),
                        villageID = data.villageID,
                        capital = data.capital
                    };
                    list.Add(item2);
                }
            }
            return list;
        }

        public List<VillageNameItem> getUserVillageNamesListAndCapitals()
        {
            List<VillageNameItem> list = new List<VillageNameItem>();
            if (this.m_userVillages != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i > 0)
                    {
                        VillageNameItem item = new VillageNameItem();
                        switch (i)
                        {
                            case 1:
                                item.villageName = SK.Text("GENERIC_Parishes", "Parishes");
                                item.villageID = -1;
                                list.Add(item);
                                break;

                            case 2:
                                item.villageName = SK.Text("GENERIC_Counties", "Counties");
                                item.villageID = -1;
                                list.Add(item);
                                break;

                            case 3:
                                item.villageName = SK.Text("GENERIC_Provinces", "Provinces");
                                item.villageID = -1;
                                list.Add(item);
                                break;

                            case 4:
                                item.villageName = SK.Text("GENERIC_Countries", "Countries");
                                item.villageID = -1;
                                list.Add(item);
                                break;
                        }
                    }
                    foreach (UserVillageData data in this.m_userVillages)
                    {
                        bool flag = false;
                        switch (i)
                        {
                            case 0:
                                if (!data.capital)
                                {
                                    flag = true;
                                }
                                break;

                            case 1:
                                if (data.parishCapital)
                                {
                                    flag = true;
                                }
                                break;

                            case 2:
                                if (data.countyCapital)
                                {
                                    flag = true;
                                }
                                break;

                            case 3:
                                if (data.provinceCapital)
                                {
                                    flag = true;
                                }
                                break;

                            case 4:
                                if (data.countryCapital)
                                {
                                    flag = true;
                                }
                                break;
                        }
                        if (flag)
                        {
                            VillageNameItem item2 = new VillageNameItem {
                                villageName = this.getVillageName(data.villageID)
                            };
                            switch (i)
                            {
                                case 2:
                                    item2.villageName = item2.villageName + " / " + this.getCountyName(this.getCountyFromVillageID(data.villageID));
                                    break;

                                case 3:
                                    item2.villageName = item2.villageName + " / " + this.getProvinceName(this.getProvinceFromVillageID(data.villageID));
                                    break;

                                case 4:
                                    item2.villageName = item2.villageName + " / " + this.getCountryName(this.getCountryFromVillageID(data.villageID));
                                    break;
                            }
                            item2.villageID = data.villageID;
                            item2.capital = data.capital;
                            list.Add(item2);
                        }
                    }
                    foreach (UserVillageData data2 in this.m_userRelatedVillages)
                    {
                        bool flag2 = false;
                        switch (i)
                        {
                            case 0:
                                if (!data2.capital)
                                {
                                    flag2 = true;
                                }
                                break;

                            case 1:
                                if (data2.parishCapital)
                                {
                                    flag2 = true;
                                }
                                break;

                            case 2:
                                if (data2.countyCapital)
                                {
                                    flag2 = true;
                                }
                                break;

                            case 3:
                                if (data2.provinceCapital)
                                {
                                    flag2 = true;
                                }
                                break;

                            case 4:
                                if (data2.countryCapital)
                                {
                                    flag2 = true;
                                }
                                break;
                        }
                        if (flag2)
                        {
                            VillageNameItem item3 = new VillageNameItem {
                                villageName = this.getVillageName(data2.villageID)
                            };
                            switch (i)
                            {
                                case 2:
                                    item3.villageName = item3.villageName + " / " + this.getCountyName(this.getCountyFromVillageID(data2.villageID));
                                    break;

                                case 3:
                                    item3.villageName = item3.villageName + " / " + this.getProvinceName(this.getProvinceFromVillageID(data2.villageID));
                                    break;

                                case 4:
                                    item3.villageName = item3.villageName + " / " + this.getCountryName(this.getCountryFromVillageID(data2.villageID));
                                    break;
                            }
                            item3.villageID = data2.villageID;
                            item3.capital = data2.capital;
                            list.Add(item3);
                        }
                    }
                }
            }
            return list;
        }

        public void getUserVillages(GetUserVillages_ReturnType returnData)
        {
            this.retrievingUserVillages = false;
            if (returnData.Success)
            {
                this.loadingErrored = false;
                this.doGetUserVillages(returnData.userVillageList, returnData.userVillageNameList);
                this.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                this.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
                this.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
                this.setRanking(returnData.rank, returnData.rankSubLevel);
            }
            else
            {
                this.loadingErrored = true;
            }
        }

        public static Color getVillageColor(int colourid)
        {
            if (colourid < 0)
            {
                colourid = 0;
            }
            if (colourid >= villageColorList.Length)
            {
                colourid = 0;
            }
            return villageColorList[colourid];
        }

        public int getVillageColour(VillageData village)
        {
            if (village.userID < 0)
            {
                return 0x15;
            }
            return this.getHouse(village.factionID);
        }

        public int getVillageColour(WorldPointList wpl)
        {
            return this.getHouse(wpl.factionID);
        }

        public int getVillageCounty(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return this.villageList[villageID].countyID;
            }
            return 0;
        }

        public VillageData getVillageData(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return this.villageList[villageID];
            }
            return null;
        }

        public int getVillageFaction(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return this.villageList[villageID].factionID;
            }
            return 0;
        }

        public void getVillageFactionChangesCallback(GetVillageFactionChanges_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.loadingErrored = false;
                if (returnData.villageUpdateList != null)
                {
                    this.processVillageFactionChangesList(returnData.villageUpdateList, returnData.currentVillageChangePos);
                }
                else if (returnData.villageOwnerFactions != null)
                {
                    this.processVillageFactionList(returnData.villageOwnerFactions, returnData.currentVillageChangePos);
                }
                if (returnData.factionsList != null)
                {
                    this.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
                }
            }
            else
            {
                this.loadingErrored = true;
                this.m_downloadedDataSafely = false;
            }
        }

        public int getVillageLandscapeType(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                if (this.villageList[villageID].coastalVillage)
                {
                    return 2;
                }
                if (this.villageList[villageID].mountainVillage)
                {
                    return 1;
                }
            }
            return 0;
        }

        public Point getVillageLocation(int villageID)
        {
            Point point = new Point(-1, -1);
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                point.X = this.villageList[villageID].x;
                point.Y = this.villageList[villageID].y;
            }
            return point;
        }

        public string getVillageName(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return this.villageList[villageID].villageName;
            }
            return "";
        }

        public string getVillageNameOnly(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return this.villageList[villageID].m_villageName;
            }
            return "";
        }

        public string getVillageNameOrType(int villageID)
        {
            if (!this.isSpecial(villageID))
            {
                if (!this.isVillageVisible(villageID))
                {
                    return "";
                }
                if (!this.isCapital(villageID) && (this.getVillageUserID(villageID) < 0))
                {
                    return SK.Text("ReportFilter_Village_Charter", "Village Charter");
                }
                return GameEngine.Instance.World.getVillageName(villageID);
            }
            int type = this.getSpecial(villageID);
            if (GameEngine.Instance.LocalWorldData.AIWorld)
            {
                switch (type)
                {
                    case 7:
                    case 9:
                    case 11:
                    case 13:
                        if (!Program.mySettings.viewVillageIDs)
                        {
                            break;
                        }
                        return ("[" + villageID.ToString() + "] " + SpecialVillageTypes.getName(type, Program.mySettings.LanguageIdent));
                }
            }
            switch (type)
            {
                case 2:
                    return SK.Text("GENERIC_Unknown", "Unknown");

                case 3:
                case 4:
                    return SK.Text("GENERIC_Bandit_Camp", "Bandit Camp");

                case 5:
                case 6:
                    return SK.Text("GENERIC_Wolf_Camp", "Wolf Lair");

                case 30:
                    return SK.Text("GENERIC_Invasion", "Invasion");
            }
            return SpecialVillageTypes.getName(type, Program.mySettings.LanguageIdent);
        }

        public void getVillageRankTaxTreeCallback(GetVillageRankTaxTree_ReturnType returnData)
        {
            if (returnData.Success)
            {
                for (int i = 0; i < returnData.villageConnecters.Length; i++)
                {
                    this.villageList[i].connecter = returnData.villageConnecters[i];
                }
                this.updateUserVassals();
                VillageMap.setServerTime(returnData.currentTime);
                GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
            }
        }

        public int getVillageRegion(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return this.villageList[villageID].regionID;
            }
            return 0;
        }

        public int getVillageSize(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return Math.Min(this.villageList[villageID].villageInfo / 6, 0x13);
            }
            return 0;
        }

        public int getVillageTerrainType(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return this.villageList[villageID].villageTerrain;
            }
            return 0;
        }

        public int getVillageUserID(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                return this.villageList[villageID].userID;
            }
            return -1;
        }

        public Image getWorldShield(int playerID, int width, int height)
        {
            return this.getWorldShield(playerID, width, height, width, height);
        }

        public Image getWorldShield(int playerID, int width, int height, int bmapWidth, int bmapHeight)
        {
            if (GameEngine.Instance.LocalWorldData.AIWorld)
            {
                switch (playerID)
                {
                    case 1:
                        playerID = -1;
                        break;

                    case 2:
                        playerID = -2;
                        break;

                    case 3:
                        playerID = -3;
                        break;

                    case 4:
                        playerID = -4;
                        break;
                }
            }
            if (playerID == RemoteServices.Instance.UserID)
            {
                return this.getPlayerShieldImage(width, height, bmapWidth, bmapHeight);
            }
            if (playerID < 0)
            {
                return this.renderAIShield(playerID, width, height, bmapWidth, bmapHeight);
            }
            if (this.worldShieldsAvailable)
            {
                ShieldFactory factory = (ShieldFactory) this.worldShields[this.activeShieldsWorldID];
                if ((factory != null) && factory.WorldAvailable)
                {
                    Shield shield = factory.getWorldShield(playerID);
                    if (shield != null)
                    {
                        Image sourceImage = shield.Render(width, height, bmapWidth, bmapHeight);
                        return this.shieldOverlay(sourceImage, width, height, bmapWidth, bmapHeight);
                    }
                }
            }
            return null;
        }

        public Image getWorldShieldOrBlank(int playerID, int width, int height)
        {
            Image image = this.getWorldShield(playerID, width, height, width, height);
            if (image == null)
            {
                image = this.getDummyShield(width, height);
            }
            return image;
        }

        public int getWorldShieldTexture(int playerID)
        {
            return this.getWorldShieldTexture(playerID, false);
        }

        public int getWorldShieldTexture(int playerID, bool force)
        {
            ShieldTextureCacheEntry entry = null;
            ShieldTextureCacheEntry entry2 = null;
            if (this.worldShieldCachePlayerIDs[playerID] != null)
            {
                int num = (int) this.worldShieldCachePlayerIDs[playerID];
                if (num >= 0)
                {
                    if (num != 0xbc614e)
                    {
                        return this.worldShieldCache[num].textureID;
                    }
                    if (!force)
                    {
                        return -1;
                    }
                }
            }
            Bitmap newBitmap = null;
            if (playerID == RemoteServices.Instance.UserID)
            {
                newBitmap = (Bitmap) this.getWorldShield(playerID, 0x20, 0x24, 0x40, 0x40);
                if ((newBitmap == null) && force)
                {
                    newBitmap = (Bitmap) this.getDummyShield(0x20, 0x24, 0x40, 0x40);
                }
            }
            else
            {
                newBitmap = (Bitmap) this.getWorldShield(playerID, 0x19, 0x1c, 0x20, 0x20);
                if ((newBitmap == null) && force)
                {
                    newBitmap = (Bitmap) this.getDummyShield(0x19, 0x1c, 0x20, 0x20);
                }
            }
            if (newBitmap == null)
            {
                this.worldShieldCachePlayerIDs[playerID] = 0xbc614e;
            }
            else
            {
                foreach (ShieldTextureCacheEntry entry3 in this.worldShieldCache)
                {
                    if ((entry == null) && (entry3.playerID < -1000))
                    {
                        entry = entry3;
                        break;
                    }
                    if ((entry2 == null) || (entry3.lastUsage < entry2.lastUsage))
                    {
                        entry2 = entry3;
                    }
                }
                if (entry != null)
                {
                    entry.playerID = playerID;
                    entry.lastUsage = DateTime.Now;
                    entry.textureID = GameEngine.Instance.GFX.loadTextureFromBitmap(newBitmap, entry.textureID);
                    this.worldShieldCachePlayerIDs[playerID] = entry.index;
                    return entry.textureID;
                }
                if (this.worldShieldCache.Count < 0x7d)
                {
                    ShieldTextureCacheEntry item = new ShieldTextureCacheEntry {
                        playerID = playerID,
                        lastUsage = DateTime.Now,
                        textureID = GameEngine.Instance.GFX.loadTextureFromBitmap(newBitmap)
                    };
                    if (item.textureID >= 0)
                    {
                        item.index = this.worldShieldCache.Count;
                        this.worldShieldCachePlayerIDs[playerID] = this.worldShieldCache.Count;
                        this.worldShieldCache.Add(item);
                        return item.textureID;
                    }
                }
                if (entry2 != null)
                {
                    this.worldShieldCachePlayerIDs[entry2.playerID] = -1;
                    entry2.playerID = playerID;
                    entry2.lastUsage = DateTime.Now;
                    entry2.textureID = GameEngine.Instance.GFX.loadTextureFromBitmap(newBitmap, entry2.textureID);
                    this.worldShieldCachePlayerIDs[playerID] = entry2.index;
                    return entry2.textureID;
                }
            }
            return -1;
        }

        public int getYourFactionRank()
        {
            if (this.m_factionMembers != null)
            {
                foreach (FactionMemberData data in this.m_factionMembers)
                {
                    if (data.userID == RemoteServices.Instance.UserID)
                    {
                        return data.status;
                    }
                }
            }
            return 0;
        }

        public int getYourFactionRelation(int otherFactionID)
        {
            int userFactionID = RemoteServices.Instance.UserFactionID;
            if (userFactionID >= 0)
            {
                if (otherFactionID == userFactionID)
                {
                    return 0;
                }
                if (this.m_factionAllies != null)
                {
                    foreach (int num2 in this.m_factionAllies)
                    {
                        if (num2 == otherFactionID)
                        {
                            return 1;
                        }
                    }
                }
                if (this.m_factionEnemies != null)
                {
                    foreach (int num3 in this.m_factionEnemies)
                    {
                        if (num3 == otherFactionID)
                        {
                            return -1;
                        }
                    }
                }
            }
            return 0;
        }

        public int getYourHouseRank()
        {
            int userFactionID = RemoteServices.Instance.UserFactionID;
            if (userFactionID < 0)
            {
                return 0;
            }
            FactionData data = this.getFaction(userFactionID);
            if (data == null)
            {
                return 0;
            }
            return data.houseRank;
        }

        public int getYourHouseRelation(int otherHouseID)
        {
            int userFactionID = RemoteServices.Instance.UserFactionID;
            if (userFactionID >= 0)
            {
                FactionData data = this.getFaction(userFactionID);
                if (data == null)
                {
                    return 0;
                }
                int houseID = data.houseID;
                if ((houseID == 0) || (otherHouseID == houseID))
                {
                    return 0;
                }
                if (this.m_houseAllies != null)
                {
                    foreach (int num3 in this.m_houseAllies)
                    {
                        if (num3 == otherHouseID)
                        {
                            return 1;
                        }
                    }
                }
                if (this.m_houseEnemies != null)
                {
                    foreach (int num4 in this.m_houseEnemies)
                    {
                        if (num4 == otherHouseID)
                        {
                            return -1;
                        }
                    }
                }
            }
            return 0;
        }

        public void givePlaguesToParish(int parishID)
        {
            if (((parishID >= 0) && (parishID < this.regionList.Length)) && (this.regionList[parishID].plague == 0))
            {
                this.regionList[parishID].plague = 1;
            }
        }

        public bool gotPlaybackData()
        {
            return (this.playbackItems != null);
        }

        public void handleQuestObjectiveHappening(int objective)
        {
            int num = Quests.getQuestFromObjectiveFlag(objective);
            if (((num >= 0) && ((this.m_tutorialInfo != null) && (this.m_tutorialInfo.questsActive != null))) && (this.QuestObjectivesSent[objective] == null))
            {
                foreach (int num2 in this.m_tutorialInfo.questsActive)
                {
                    if (num2 == num)
                    {
                        RemoteServices.Instance.set_FlagQuestObjectiveComplete_UserCallBack(new RemoteServices.FlagQuestObjectiveComplete_UserCallBack(this.FlagQuestObjectiveCompleteCallBack));
                        RemoteServices.Instance.FlagQuestObjectiveComplete(objective);
                        this.QuestObjectivesSent[objective] = 1;
                        break;
                    }
                }
                if ((objective == 0x2715) || (objective == 0x2712))
                {
                    bool flag = false;
                    foreach (int num3 in this.m_tutorialInfo.questsCompleted)
                    {
                        if (num3 == num)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        RemoteServices.Instance.set_FlagQuestObjectiveComplete_UserCallBack(new RemoteServices.FlagQuestObjectiveComplete_UserCallBack(this.FlagQuestObjectiveCompleteCallBack));
                        RemoteServices.Instance.FlagQuestObjectiveComplete(objective);
                        this.QuestObjectivesSent[objective] = 1;
                    }
                }
            }
        }

        public void handleQuestObjectiveHappening_PlayedCard(int cardType)
        {
            int objective = -1;
            switch (cardType)
            {
                case 0x301:
                case 770:
                case 0xb86:
                    objective = 0x2712;
                    break;

                case 0xc81:
                case 0xc82:
                case 0xc83:
                    objective = 0x2715;
                    break;
            }
            if (objective >= 0)
            {
                this.handleQuestObjectiveHappening(objective);
                if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_QUESTS)
                {
                    InterfaceMgr.Instance.reloadQuestPanel();
                }
            }
        }

        public bool holdingLeftMouse()
        {
            return this.m_leftMouseHeldDown;
        }

        public void importCounterSpyInfo(List<CounterSpyInfo> info)
        {
            this.counterSpyInfo = info;
        }

        public void importFreeCardData(int currentLevel, bool[] stages, DateTime nextCardTime, DateTime serverTime)
        {
            this.freeCardInfo = FreeCardsData.createFreeCardData(currentLevel, stages, nextCardTime, serverTime);
        }

        public void importLeaderboardData(SparseArray currentArray, int leaderboardType, List<LeaderboardDataMainClass> mainLeaderboard, List<LeaderboardSubDataClass> subLeaderboard, List<ParishFlagLeaderboardInfo> parishLeaderboard, List<HouseLeaderboardInfo> houseLeaderboard, List<FactionLeaderboardInfo> factionLeaderboard)
        {
            switch (leaderboardType)
            {
                case -6:
                    foreach (LeaderboardDataMainClass class4 in mainLeaderboard)
                    {
                        int standing = class4.standing;
                        LeaderBoardEntryData data6 = new LeaderBoardEntryData {
                            standing = standing,
                            name = class4.userName,
                            house = class4.house,
                            value = class4.numVillages,
                            entryID = class4.userID
                        };
                        currentArray[standing] = data6;
                    }
                    if (mainLeaderboard.Count != 0)
                    {
                        break;
                    }
                    this.lastZeroDownload_leaderboard_MainVillages = DateTime.Now;
                    return;

                case -5:
                    foreach (LeaderboardDataMainClass class3 in mainLeaderboard)
                    {
                        int num6 = class3.standing;
                        LeaderBoardEntryData data5 = new LeaderBoardEntryData {
                            standing = num6,
                            name = class3.userName,
                            house = class3.house
                        };
                        if (class3.rank >= 0)
                        {
                            data5.value = class3.rank;
                            data5.male = true;
                        }
                        else
                        {
                            data5.value = -1 - class3.rank;
                            data5.male = false;
                        }
                        data5.entryID = class3.userID;
                        currentArray[num6] = data5;
                    }
                    if (mainLeaderboard.Count != 0)
                    {
                        break;
                    }
                    this.lastZeroDownload_leaderboard_MainRank = DateTime.Now;
                    return;

                case -4:
                    foreach (ParishFlagLeaderboardInfo info3 in parishLeaderboard)
                    {
                        int num5 = info3.standing;
                        LeaderBoardEntryData data4 = new LeaderBoardEntryData {
                            standing = num5,
                            name = this.getParishName(info3.regionID),
                            house = 0,
                            value = info3.points,
                            entryID = info3.regionID
                        };
                        currentArray[num5] = data4;
                    }
                    if (parishLeaderboard.Count != 0)
                    {
                        break;
                    }
                    this.lastZeroDownload_leaderboard_ParishFlags = DateTime.Now;
                    return;

                case -3:
                {
                    int num3 = 1;
                    foreach (HouseLeaderboardInfo info2 in houseLeaderboard)
                    {
                        int num4 = num3++;
                        LeaderBoardEntryData data3 = new LeaderBoardEntryData {
                            standing = num4,
                            name = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + info2.houseID.ToString(),
                            house = info2.houseID,
                            value = info2.housePoints,
                            entryID = info2.houseID
                        };
                        currentArray[num4] = data3;
                    }
                    if (houseLeaderboard.Count != 0)
                    {
                        break;
                    }
                    this.lastZeroDownload_leaderboard_Houses = DateTime.Now;
                    return;
                }
                case -2:
                    foreach (FactionLeaderboardInfo info in factionLeaderboard)
                    {
                        int num2 = info.standing;
                        LeaderBoardEntryData data2 = new LeaderBoardEntryData {
                            standing = num2,
                            name = info.factionname,
                            house = info.house,
                            value = info.factionPoints,
                            entryID = info.factionID
                        };
                        currentArray[num2] = data2;
                    }
                    if (factionLeaderboard.Count != 0)
                    {
                        break;
                    }
                    this.lastZeroDownload_leaderboard_Factions = DateTime.Now;
                    return;

                case -1:
                    foreach (LeaderboardDataMainClass class2 in mainLeaderboard)
                    {
                        int num = class2.standing;
                        LeaderBoardEntryData data = new LeaderBoardEntryData {
                            standing = num,
                            name = class2.userName,
                            house = class2.house,
                            value = class2.numPoints,
                            entryID = class2.userID
                        };
                        currentArray[num] = data;
                    }
                    if (mainLeaderboard.Count != 0)
                    {
                        break;
                    }
                    this.lastZeroDownload_leaderboard_Main = DateTime.Now;
                    return;

                default:
                    foreach (LeaderboardSubDataClass class5 in subLeaderboard)
                    {
                        int num8 = class5.standing;
                        LeaderBoardEntryData data7 = new LeaderBoardEntryData {
                            standing = num8,
                            name = class5.userName,
                            house = class5.house,
                            value = class5.data,
                            entryID = class5.userID
                        };
                        currentArray[num8] = data7;
                    }
                    if (subLeaderboard.Count != 0)
                    {
                        break;
                    }
                    switch (leaderboardType)
                    {
                        case 0:
                            this.lastZeroDownload_leaderboard_Sub_Pillager = DateTime.Now;
                            return;

                        case 1:
                            this.lastZeroDownload_leaderboard_Sub_Defender = DateTime.Now;
                            return;

                        case 2:
                            this.lastZeroDownload_leaderboard_Sub_Ransack = DateTime.Now;
                            return;

                        case 3:
                            this.lastZeroDownload_leaderboard_Sub_Wolfsbane = DateTime.Now;
                            return;

                        case 4:
                            this.lastZeroDownload_leaderboard_Sub_Banditkiller = DateTime.Now;
                            return;

                        case 5:
                            this.lastZeroDownload_leaderboard_Sub_AIKiller = DateTime.Now;
                            return;

                        case 6:
                            this.lastZeroDownload_leaderboard_Sub_Trader = DateTime.Now;
                            return;

                        case 7:
                            this.lastZeroDownload_leaderboard_Sub_Forager = DateTime.Now;
                            return;

                        case 8:
                            this.lastZeroDownload_leaderboard_Sub_Stockpiler = DateTime.Now;
                            return;

                        case 9:
                            this.lastZeroDownload_leaderboard_Sub_Farmer = DateTime.Now;
                            return;

                        case 10:
                            this.lastZeroDownload_leaderboard_Sub_Brewer = DateTime.Now;
                            return;

                        case 11:
                            this.lastZeroDownload_leaderboard_Sub_Weaponsmith = DateTime.Now;
                            return;

                        case 12:
                            this.lastZeroDownload_leaderboard_Sub_banquetter = DateTime.Now;
                            return;

                        case 13:
                            this.lastZeroDownload_leaderboard_Sub_Achiever = DateTime.Now;
                            return;

                        case 14:
                            this.lastZeroDownload_leaderboard_Sub_Donater = DateTime.Now;
                            return;

                        case 15:
                            this.lastZeroDownload_leaderboard_Sub_Capture = DateTime.Now;
                            return;

                        case 0x10:
                            this.lastZeroDownload_leaderboard_Sub_Raze = DateTime.Now;
                            return;

                        case 0x11:
                            this.lastZeroDownload_leaderboard_Sub_Glory = DateTime.Now;
                            return;
                    }
                    return;
            }
        }

        public void importOrphanedPeople(List<PersonData> personData, DateTime curServerTime, int villageID)
        {
            this.clearPersonArray(villageID);
            if (personData != null)
            {
                AllArmiesPanel2.MonksUpdated = true;
                foreach (PersonData data in personData)
                {
                    this.addPerson(data, curServerTime);
                }
            }
            this.countChildren();
        }

        public void importOrphanedTraders(List<MarketTraderData> traderData, DateTime curServerTime, int villageID)
        {
            GameEngine.Instance.World.clearTraderArray(villageID);
            if (traderData != null)
            {
                AllArmiesPanel2.TradersUpdated = true;
                foreach (MarketTraderData data in traderData)
                {
                    this.addTrader(data, curServerTime);
                }
            }
        }

        public void ImportParishNames(string[] newNames)
        {
            if ((newNames != null) && (newNames.Length > 0))
            {
                for (int i = 0; i < newNames.Length; i++)
                {
                    this.regionList[i].areaName = newNames[i];
                    int capitalVillage = this.regionList[i].capitalVillage;
                    if ((capitalVillage >= 0) && (capitalVillage < this.villageList.Length))
                    {
                        this.villageList[capitalVillage].villageName = newNames[i];
                        this.villageList[capitalVillage].visible = true;
                    }
                }
            }
        }

        public void importStandings(int[,] standings)
        {
            this.dirtyStanding = true;
            this.leaderboardSelfRankings.Clear();
            for (int i = 0; i < 0x15; i++)
            {
                if (standings[i, 0] > 0)
                {
                    LeaderBoardSelfRankings item = new LeaderBoardSelfRankings {
                        place = standings[i, 0],
                        value = standings[i, 1],
                        oldPlace = standings[i, 2]
                    };
                    if (i < 15)
                    {
                        item.category = i;
                    }
                    else if (i == 15)
                    {
                        item.category = -1;
                    }
                    else if (i == 0x10)
                    {
                        item.category = -5;
                    }
                    else if (i == 0x11)
                    {
                        item.category = -6;
                    }
                    else if (i == 0x12)
                    {
                        item.category = 15;
                    }
                    else if (i == 0x13)
                    {
                        item.category = 0x10;
                    }
                    else if (i == 20)
                    {
                        item.category = 0x11;
                    }
                    this.leaderboardSelfRankings.Add(item);
                }
            }
            this.leaderboardSelfRankings.Sort(this.leaderboardSelfRankingsComparer);
            if (this.getGameDay() >= 30)
            {
                bool flag = false;
                bool flag2 = false;
                bool flag3 = false;
                bool flag4 = false;
                foreach (LeaderBoardSelfRankings rankings2 in this.leaderboardSelfRankings)
                {
                    if (rankings2.place <= 1)
                    {
                        flag = true;
                    }
                    if (rankings2.place <= 5)
                    {
                        flag2 = true;
                    }
                    if (rankings2.place <= 20)
                    {
                        flag3 = true;
                    }
                    if (rankings2.place <= 100)
                    {
                        flag4 = true;
                    }
                }
                List<int> userAchievements = RemoteServices.Instance.UserAchievements;
                if (userAchievements != null)
                {
                    List<int> achievementToTest = new List<int>();
                    if (flag4)
                    {
                        if (!userAchievements.Contains(0x141))
                        {
                            achievementToTest.Add(0x141);
                        }
                        if (flag3)
                        {
                            if (!userAchievements.Contains(0x10000141))
                            {
                                achievementToTest.Add(0x10000141);
                            }
                            if (flag2)
                            {
                                if (!userAchievements.Contains(0x20000141))
                                {
                                    achievementToTest.Add(0x20000141);
                                }
                                if (flag && !userAchievements.Contains(0x40000141))
                                {
                                    achievementToTest.Add(0x40000141);
                                }
                            }
                        }
                    }
                    if (achievementToTest.Count > 0)
                    {
                        GameEngine.Instance.World.testAchievements(achievementToTest, new List<AchievementData>(), false);
                    }
                }
            }
        }

        public void initMapTiles(string fileName, int width, int height)
        {
            this.TILEMAP_WIDTH = width;
            this.TILEMAP_HEIGHT = height;
            this.mapTileGrid = new short[this.TILEMAP_WIDTH, this.TILEMAP_HEIGHT];
            this.tree1Grid = new byte[this.TILEMAP_WIDTH, this.TILEMAP_HEIGHT];
            this.tree2Grid = new byte[this.TILEMAP_WIDTH, this.TILEMAP_HEIGHT];
            try
            {
                FileStream input = new FileStream(Application.StartupPath + @"\assets\" + fileName, FileMode.Open);
                BinaryReader reader = new BinaryReader(input);
                reader.ReadInt32();
                int num = reader.ReadInt32();
                byte[] buffer = new byte[num];
                for (int i = 0; i < num; i++)
                {
                    buffer[i] = reader.ReadByte();
                }
                reader.Close();
                input.Close();
                byte[] buffer2 = RLECompress.DecodeData(buffer);
                int num3 = 0;
                for (int j = 0; j < this.TILEMAP_HEIGHT; j++)
                {
                    for (int num5 = 0; num5 < this.TILEMAP_WIDTH; num5++)
                    {
                        this.mapTileGrid[num5, j] = buffer2[num3++];
                    }
                }
                for (int k = 0; k < this.TILEMAP_HEIGHT; k++)
                {
                    for (int num7 = 0; num7 < this.TILEMAP_WIDTH; num7++)
                    {
                        short num1 = this.mapTileGrid[num7, k];
                        num1 = (short) (num1 | ((short) (buffer2[num3++] << 8)));
                    }
                }
                for (int m = 0; m < this.TILEMAP_HEIGHT; m++)
                {
                    for (int num9 = 0; num9 < this.TILEMAP_WIDTH; num9++)
                    {
                        this.tree1Grid[num9, m] = buffer2[num3++];
                    }
                }
                for (int n = 0; n < this.TILEMAP_HEIGHT; n++)
                {
                    for (int num11 = 0; num11 < this.TILEMAP_WIDTH; num11++)
                    {
                        this.tree2Grid[num11, n] = buffer2[num3++];
                    }
                }
                this.haveInitMapTiles = true;
            }
            catch (Exception)
            {
            }
        }

        public void initSprites(GraphicsMgr newGFX)
        {
            this.gfx = newGFX;
            this.villageSprite = new SpriteWrapper();
            this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapTilesTexID;
            this.villageSprite.SpriteNo = 0;
            this.villageSprite.Initialize(this.gfx);
            this.villageSprite.AutoCentre = true;
            this.villageSprite = new SpriteWrapper();
            this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapTilesTexID;
            this.villageSprite.SpriteNo = 0;
            this.villageSprite.Initialize(this.gfx);
            this.villageSprite.AutoCentre = true;
            this.worldTileSprite.TextureID = GFXLibrary.Instance.WorldMapTilesTexID;
            this.worldTileSprite.SpriteNo = 0;
            this.worldTileSprite.PolySprite = true;
            this.worldTileSprite.Initialize(this.gfx);
            this.worldTreeSprite.TextureID = GFXLibrary.Instance.MapElementsTexID;
            this.worldTreeSprite.SpriteNo = 0;
            this.worldTreeSprite.PolySprite = true;
            this.worldTreeSprite.Initialize(this.gfx);
            this.overlaySprite.TextureID = GFXLibrary.Instance.EffectLayerTexID;
            this.overlaySprite.SpriteNo = 0;
            this.overlaySprite.PolySprite = true;
            this.overlaySprite.Initialize(this.gfx);
            this.updateClockSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
            this.updateClockSprite.SpriteNo = 30;
            this.updateClockSprite.PolySprite = true;
            this.updateClockSprite.Initialize(this.gfx);
            this.tutorialOverlaySprite.TextureID = GFXLibrary.Instance.TutorialIconNormalID;
            this.tutorialOverlaySprite.SpriteNo = 0;
            this.tutorialOverlaySprite.PolySprite = true;
            this.tutorialOverlaySprite.Initialize(this.gfx);
            this.freeCardsSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
            this.freeCardsSprite.SpriteNo = 0;
            this.freeCardsSprite.PolySprite = false;
            this.freeCardsSprite.Initialize(this.gfx);
            this.freeCardsSprite2.TextureID = GFXLibrary.Instance.FreeCardIconsID;
            this.freeCardsSprite2.SpriteNo = 0;
            this.freeCardsSprite2.PolySprite = false;
            this.freeCardsSprite2.Initialize(this.gfx);
            this.wolfsRevengeSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
            this.wolfsRevengeSprite.SpriteNo = 0;
            this.wolfsRevengeSprite.PolySprite = false;
            this.wolfsRevengeSprite.Initialize(this.gfx);
            this.wolfsRevengeSprite2.TextureID = GFXLibrary.Instance.FreeCardIconsID;
            this.wolfsRevengeSprite2.SpriteNo = 0;
            this.wolfsRevengeSprite2.PolySprite = false;
            this.wolfsRevengeSprite2.Initialize(this.gfx);
            this.ticketsSprite.TextureID = GFXLibrary.Instance.FreeCardIconsID;
            this.ticketsSprite.SpriteNo = 0;
            this.ticketsSprite.PolySprite = false;
            this.ticketsSprite.Initialize(this.gfx);
            this.ticketsSprite2.TextureID = GFXLibrary.Instance.FreeCardIconsID;
            this.ticketsSprite2.SpriteNo = 0;
            this.ticketsSprite2.PolySprite = false;
            this.ticketsSprite2.Initialize(this.gfx);
        }

        public void initUserVillages()
        {
        }

        public void initWorldMap(int mapType)
        {
            this.storedRegionFactionsPos = -1L;
            this.storedCountyFactionsPos = -1L;
            this.storedProvinceFactionsPos = -1L;
            this.storedVillageFactionsPos = -1L;
            this.storedCountryFactionsPos = -1L;
            this.storedFactionChangesPos = -1L;
            this.storedParishFlagsPos = -1L;
            this.storedCountyFlagsPos = -1L;
            this.storedProvinceFlagsPos = -1L;
            this.storedCountryFlagsPos = -1L;
            this.currentMapType = mapType;
            CommonTypes.WorldMapType type = GameEngine.Instance.WorldMapTypesData.getMapData(mapType);
            if (type == null)
            {
                throw new Exception("Map Data was null");
            }
            if (type.mapName == null)
            {
                throw new Exception("Map Data Name was null");
            }
            this.loadData(type.mapName);
            this.initMapTiles(type.tmapName, type.tileMapWidth, type.tileMapHeight);
            if (type.mapName.ToLower() == "uk.wmpData".ToLower())
            {
                WorldPointList list = this.regionList[0x95f];
                List<Triangle> list2 = new List<Triangle>();
                list2.AddRange(list.triangleList);
                list2.Add(this.makeTriangle(list.regionBorderList[9], list.regionBorderList[11], list.regionBorderList[10]));
                list2.Add(this.makeTriangle(list.regionBorderList[11], list.regionBorderList[9], list.regionBorderList[12]));
                list.triangleList = list2.ToArray();
                List<WorldPoint> list3 = new List<WorldPoint>();
                for (int i = 0; i < list.regionBorderList.Length; i++)
                {
                    if ((i < 9) || (i > 12))
                    {
                        list3.Add(list.regionBorderList[i]);
                    }
                }
                list.regionBorderList = list3.ToArray();
                list = this.regionList[0xc50];
                list2 = new List<Triangle>();
                list2.AddRange(list.triangleList);
                list2.Add(this.makeTriangle(list.regionBorderList[10], list.regionBorderList[12], list.regionBorderList[11]));
                list.triangleList = list2.ToArray();
                list3 = new List<WorldPoint>();
                for (int j = 0; j < list.regionBorderList.Length; j++)
                {
                    if ((j < 10) || (j > 12))
                    {
                        list3.Add(list.regionBorderList[j]);
                    }
                }
                list.regionBorderList = list3.ToArray();
                list = this.regionList[0x758];
                list2 = new List<Triangle>();
                list2.AddRange(list.triangleList);
                list2.Add(this.makeTriangle(list.regionBorderList[4], list.regionBorderList[5], list.regionBorderList[6]));
                list.triangleList = list2.ToArray();
                list3 = new List<WorldPoint>();
                for (int k = 0; k < list.regionBorderList.Length; k++)
                {
                    if ((k < 4) || (k > 6))
                    {
                        list3.Add(list.regionBorderList[k]);
                    }
                }
                list.regionBorderList = list3.ToArray();
                list = this.regionList[0x38b];
                list2 = new List<Triangle>();
                list2.AddRange(list.triangleList);
                list2.Add(this.makeTriangle(list.regionBorderList[9], list.regionBorderList[10], list.regionBorderList[11]));
                list.triangleList = list2.ToArray();
                list3 = new List<WorldPoint>();
                for (int m = 0; m < list.regionBorderList.Length; m++)
                {
                    if (m != 10)
                    {
                        list3.Add(list.regionBorderList[m]);
                    }
                }
                list.regionBorderList = list3.ToArray();
                list = this.regionList[0x23];
                list2 = new List<Triangle>();
                list2.AddRange(list.triangleList);
                list2.Add(this.makeTriangle(list.regionBorderList[2], list.regionBorderList[4], list.regionBorderList[3]));
                list.triangleList = list2.ToArray();
                list3 = new List<WorldPoint>();
                for (int n = 0; n < list.regionBorderList.Length; n++)
                {
                    if (n != 3)
                    {
                        list3.Add(list.regionBorderList[n]);
                    }
                }
                list.regionBorderList = list3.ToArray();
            }
            if ((type.mapName.ToLower() == "uk2.wmpData".ToLower()) || (type.mapName.ToLower() == "ukai.wmpData".ToLower()))
            {
                WorldPointList list4 = this.regionList[0x48a];
                list4.regionBorderList[2].x = 1826f;
                list4.regionBorderList[2].y = 2747f;
                list4.triangleList[4].x2 = 1826f;
                list4.triangleList[4].y2 = 2747f;
            }
            if (type.mapName.ToLower() == "de.wmpData".ToLower())
            {
                WorldPointList list5 = null;
                List<Triangle> list6 = null;
                List<WorldPoint> list7 = null;
                list5 = this.regionList[0xfda];
                list6 = new List<Triangle>();
                list6.AddRange(list5.triangleList);
                list6.Add(this.makeTriangle(list5.regionBorderList[4], list5.regionBorderList[6], list5.regionBorderList[5]));
                list5.triangleList = list6.ToArray();
                list7 = new List<WorldPoint>();
                for (int num6 = 0; num6 < list5.regionBorderList.Length; num6++)
                {
                    if ((num6 < 4) || (num6 > 6))
                    {
                        list7.Add(list5.regionBorderList[num6]);
                    }
                }
                list5.regionBorderList = list7.ToArray();
                list5 = this.regionList[0x227];
                list6 = new List<Triangle>();
                for (int num7 = 0; num7 < list5.triangleList.Length; num7++)
                {
                    if ((((list5.triangleList[num7].x1 != list5.regionBorderList[5].x) || (list5.triangleList[num7].y1 != list5.regionBorderList[5].y)) && ((list5.triangleList[num7].x2 != list5.regionBorderList[5].x) || (list5.triangleList[num7].y2 != list5.regionBorderList[5].y))) && ((list5.triangleList[num7].x3 != list5.regionBorderList[5].x) || (list5.triangleList[num7].y3 != list5.regionBorderList[5].y)))
                    {
                        list6.Add(list5.triangleList[num7]);
                    }
                }
                list5.triangleList = list6.ToArray();
                list7 = new List<WorldPoint>();
                for (int num8 = 0; num8 < list5.regionBorderList.Length; num8++)
                {
                    if ((num8 < 3) || (num8 > 5))
                    {
                        list7.Add(list5.regionBorderList[num8]);
                    }
                }
                list5.regionBorderList = list7.ToArray();
                list5 = this.regionList[0xb4a];
                list6 = new List<Triangle>();
                for (int num9 = 0; num9 < list5.triangleList.Length; num9++)
                {
                    if ((((list5.triangleList[num9].x1 != list5.regionBorderList[7].x) || (list5.triangleList[num9].y1 != list5.regionBorderList[7].y)) && ((list5.triangleList[num9].x2 != list5.regionBorderList[7].x) || (list5.triangleList[num9].y2 != list5.regionBorderList[7].y))) && ((list5.triangleList[num9].x3 != list5.regionBorderList[7].x) || (list5.triangleList[num9].y3 != list5.regionBorderList[7].y)))
                    {
                        list6.Add(list5.triangleList[num9]);
                    }
                }
                list6.Add(this.makeTriangle(list5.regionBorderList[6], list5.regionBorderList[13], list5.regionBorderList[10]));
                list5.triangleList = list6.ToArray();
                list7 = new List<WorldPoint>();
                for (int num10 = 0; num10 < list5.regionBorderList.Length; num10++)
                {
                    if ((num10 < 6) || (num10 > 8))
                    {
                        list7.Add(list5.regionBorderList[num10]);
                    }
                }
                list5.regionBorderList = list7.ToArray();
                list5 = this.regionList[0xe8d];
                list6 = new List<Triangle>();
                for (int num11 = 0; num11 < list5.triangleList.Length; num11++)
                {
                    if ((((list5.triangleList[num11].x1 != list5.regionBorderList[4].x) || (list5.triangleList[num11].y1 != list5.regionBorderList[4].y)) && ((list5.triangleList[num11].x2 != list5.regionBorderList[4].x) || (list5.triangleList[num11].y2 != list5.regionBorderList[4].y))) && ((list5.triangleList[num11].x3 != list5.regionBorderList[4].x) || (list5.triangleList[num11].y3 != list5.regionBorderList[4].y)))
                    {
                        list6.Add(list5.triangleList[num11]);
                    }
                }
                list6.Add(this.makeTriangle(list5.regionBorderList[3], list5.regionBorderList[4], list5.regionBorderList[8]));
                list6.Add(this.makeTriangle(list5.regionBorderList[8], list5.regionBorderList[4], list5.regionBorderList[9]));
                list6.Add(this.makeTriangle(list5.regionBorderList[4], list5.regionBorderList[6], list5.regionBorderList[9]));
                list5.triangleList = list6.ToArray();
                list7 = new List<WorldPoint>();
                for (int num12 = 0; num12 < list5.regionBorderList.Length; num12++)
                {
                    if ((num12 < 3) || (num12 > 9))
                    {
                        list7.Add(list5.regionBorderList[num12]);
                    }
                    else if (num12 == 3)
                    {
                        list7.Add(list5.regionBorderList[num12]);
                        list7.Add(list5.regionBorderList[4]);
                        list7.Add(list5.regionBorderList[6]);
                        list7.Add(list5.regionBorderList[9]);
                    }
                }
                list5.regionBorderList = list7.ToArray();
                list5 = this.regionList[0x270];
                list6 = new List<Triangle>();
                for (int num13 = 0; num13 < list5.triangleList.Length; num13++)
                {
                    if ((((list5.triangleList[num13].x1 != list5.regionBorderList[6].x) || (list5.triangleList[num13].y1 != list5.regionBorderList[6].y)) && ((list5.triangleList[num13].x2 != list5.regionBorderList[6].x) || (list5.triangleList[num13].y2 != list5.regionBorderList[6].y))) && ((list5.triangleList[num13].x3 != list5.regionBorderList[6].x) || (list5.triangleList[num13].y3 != list5.regionBorderList[6].y)))
                    {
                        list6.Add(list5.triangleList[num13]);
                    }
                }
                list5.triangleList = list6.ToArray();
                list7 = new List<WorldPoint>();
                for (int num14 = 0; num14 < list5.regionBorderList.Length; num14++)
                {
                    if ((num14 < 4) || (num14 > 6))
                    {
                        list7.Add(list5.regionBorderList[num14]);
                    }
                }
                list5.regionBorderList = list7.ToArray();
                list5 = this.regionList[0x112];
                list6 = new List<Triangle>();
                list6.AddRange(list5.triangleList);
                list6.Add(this.makeTriangle(list5.regionBorderList[7], list5.regionBorderList[9], list5.regionBorderList[8]));
                list5.triangleList = list6.ToArray();
                list7 = new List<WorldPoint>();
                for (int num15 = 0; num15 < list5.regionBorderList.Length; num15++)
                {
                    if ((num15 < 7) || (num15 > 9))
                    {
                        list7.Add(list5.regionBorderList[num15]);
                    }
                }
                list5.regionBorderList = list7.ToArray();
                list5 = this.regionList[0x6a8];
                list6 = new List<Triangle>();
                for (int num16 = 0; num16 < list5.triangleList.Length; num16++)
                {
                    if ((((list5.triangleList[num16].x1 != list5.regionBorderList[5].x) || (list5.triangleList[num16].y1 != list5.regionBorderList[5].y)) && ((list5.triangleList[num16].x2 != list5.regionBorderList[5].x) || (list5.triangleList[num16].y2 != list5.regionBorderList[5].y))) && ((list5.triangleList[num16].x3 != list5.regionBorderList[5].x) || (list5.triangleList[num16].y3 != list5.regionBorderList[5].y)))
                    {
                        list6.Add(list5.triangleList[num16]);
                    }
                }
                list7 = new List<WorldPoint>();
                for (int num17 = 0; num17 < list5.regionBorderList.Length; num17++)
                {
                    if ((num17 < 5) || (num17 > 7))
                    {
                        list7.Add(list5.regionBorderList[num17]);
                    }
                }
                list5.regionBorderList = list7.ToArray();
                list6.Add(this.makeTriangle(list5.regionBorderList[4], list5.regionBorderList[5], list5.regionBorderList[8]));
                list6.Add(this.makeTriangle(list5.regionBorderList[5], list5.regionBorderList[6], list5.regionBorderList[8]));
                list5.triangleList = list6.ToArray();
                list5 = this.regionList[0x2e1];
                list6 = new List<Triangle>();
                for (int num18 = 0; num18 < list5.triangleList.Length; num18++)
                {
                    if ((((list5.triangleList[num18].x1 != list5.regionBorderList[9].x) || (list5.triangleList[num18].y1 != list5.regionBorderList[9].y)) && ((list5.triangleList[num18].x2 != list5.regionBorderList[9].x) || (list5.triangleList[num18].y2 != list5.regionBorderList[9].y))) && ((list5.triangleList[num18].x3 != list5.regionBorderList[9].x) || (list5.triangleList[num18].y3 != list5.regionBorderList[9].y)))
                    {
                        list6.Add(list5.triangleList[num18]);
                    }
                }
                list6.Add(this.makeTriangle(list5.regionBorderList[10], list5.regionBorderList[6], list5.regionBorderList[7]));
                list6.Add(this.makeTriangle(list5.regionBorderList[10], list5.regionBorderList[7], list5.regionBorderList[8]));
                list5.triangleList = list6.ToArray();
                list7 = new List<WorldPoint>();
                for (int num19 = 0; num19 < list5.regionBorderList.Length; num19++)
                {
                    if ((num19 < 8) || (num19 > 10))
                    {
                        list7.Add(list5.regionBorderList[num19]);
                    }
                }
                list5.regionBorderList = list7.ToArray();
                list5 = this.regionList[0x7d2];
                list6 = new List<Triangle>();
                for (int num20 = 0; num20 < list5.triangleList.Length; num20++)
                {
                    if ((((list5.triangleList[num20].x1 != list5.regionBorderList[7].x) || (list5.triangleList[num20].y1 != list5.regionBorderList[7].y)) && ((list5.triangleList[num20].x2 != list5.regionBorderList[7].x) || (list5.triangleList[num20].y2 != list5.regionBorderList[7].y))) && ((list5.triangleList[num20].x3 != list5.regionBorderList[7].x) || (list5.triangleList[num20].y3 != list5.regionBorderList[7].y)))
                    {
                        list6.Add(list5.triangleList[num20]);
                    }
                }
                list5.triangleList = list6.ToArray();
                list7 = new List<WorldPoint>();
                for (int num21 = 0; num21 < list5.regionBorderList.Length; num21++)
                {
                    if ((num21 < 6) || (num21 > 8))
                    {
                        list7.Add(list5.regionBorderList[num21]);
                    }
                }
                list5.regionBorderList = list7.ToArray();
            }
            if (type.mapName.ToLower() == "fr.wmpData".ToLower())
            {
                WorldPointList list8 = null;
                List<Triangle> list9 = null;
                List<WorldPoint> list10 = null;
                list8 = this.regionList[0x506];
                list10 = new List<WorldPoint>();
                for (int num22 = 0; num22 < list8.regionBorderList.Length; num22++)
                {
                    if (num22 == 2)
                    {
                        list10.Add(this.pointList[this.pointList.Length - 1]);
                    }
                    list10.Add(list8.regionBorderList[num22]);
                }
                list8.regionBorderList = list10.ToArray();
                list9 = new List<Triangle>();
                for (int num23 = 0; num23 < list8.triangleList.Length; num23++)
                {
                    list9.Add(list8.triangleList[num23]);
                }
                list9.Add(this.makeTriangle(list8.regionBorderList[1], list8.regionBorderList[2], list8.regionBorderList[3]));
                list8.triangleList = list9.ToArray();
            }
            bool flag1 = type.mapName.ToLower() == "es.wmpData".ToLower();
            if (((type.mapName.ToLower() == "uk.wmpData".ToLower()) || (type.mapName.ToLower() == "ukai.wmpData".ToLower())) || (type.mapName.ToLower() == "uk2.wmpData".ToLower()))
            {
                this.villageList[0x16a50].whiteName = true;
                this.villageList[0x19816].whiteName = true;
                this.villageList[0x8c83].whiteName = true;
                this.villageList[0x9fad].whiteName = true;
                this.villageList[0x13808].whiteName = true;
                this.villageList[0x3cf6].whiteName = true;
                this.villageList[0xb82e].whiteName = true;
                this.villageList[0x9838].whiteName = true;
                this.villageList[0x377].whiteName = true;
                this.villageList[0xf70a].whiteName = true;
                this.villageList[0xbedc].whiteName = true;
                this.villageList[0x2656].whiteName = true;
                this.villageList[0xf772].whiteName = true;
                this.villageList[0x1b88].whiteName = true;
                this.villageList[0x68c6].whiteName = true;
                this.villageList[0x168a3].whiteName = true;
                this.villageList[0x4e60].whiteName = true;
                this.villageList[0x18aa0].whiteName = true;
                this.villageList[0x137e].whiteName = true;
                this.villageList[0xdef0].whiteName = true;
                this.villageList[0x10b3b].whiteName = true;
                this.villageList[0x17e90].whiteName = true;
                this.villageList[0x179ef].whiteName = true;
                this.villageList[0xb996].whiteName = true;
                this.villageList[0x18760].whiteName = true;
                this.villageList[0x108ec].whiteName = true;
                this.villageList[0x9e88].whiteName = true;
                this.villageList[0x1b6f].whiteName = true;
                this.villageList[0x5196].whiteName = true;
                this.villageList[0xb9f9].whiteName = true;
                this.villageList[0xc7a1].whiteName = true;
                this.villageList[0x4348].whiteName = true;
                this.villageList[0x9ead].whiteName = true;
                this.villageList[0x131ba].whiteName = true;
                this.villageList[0x184b7].whiteName = true;
                this.villageList[0x152cf].whiteName = true;
                this.villageList[0x177d5].whiteName = true;
                this.villageList[0x1733].whiteName = true;
                this.villageList[0x812c].whiteName = true;
                this.villageList[0x17a98].whiteName = true;
                this.villageList[0x11afe].whiteName = true;
                this.villageList[0xfe23].whiteName = true;
                this.villageList[0x13bfb].whiteName = true;
                this.villageList[0x90a6].whiteName = true;
                this.villageList[0xadba].whiteName = true;
                this.villageList[0x16acb].whiteName = true;
                this.villageList[0x4ec].whiteName = true;
                this.villageList[0x18194].whiteName = true;
                this.villageList[0x14a7b].whiteName = true;
                this.villageList[0x346c].whiteName = true;
                this.villageList[0x18f30].whiteName = true;
                this.villageList[0xf0b1].whiteName = true;
                this.villageList[0x189a].whiteName = true;
                this.villageList[0x739].whiteName = true;
                this.villageList[0x782].whiteName = true;
                this.villageList[0xb74e].whiteName = true;
                this.villageList[0x204d].whiteName = true;
                this.villageList[0xb964].whiteName = true;
                this.villageList[0x11110].whiteName = true;
                this.villageList[0x11dc7].whiteName = true;
                this.villageList[0x160b8].whiteName = true;
                this.villageList[0x91d].whiteName = true;
                this.villageList[0x12809].whiteName = true;
                this.villageList[0xf224].whiteName = true;
                this.villageList[0x17622].whiteName = true;
                this.villageList[0x132b].whiteName = true;
                this.villageList[0x13c51].whiteName = true;
                this.villageList[0xf8a0].whiteName = true;
                this.villageList[0xe91c].whiteName = true;
                this.villageList[0x17c78].whiteName = true;
                this.villageList[0x12011].whiteName = true;
                this.villageList[0x17de].whiteName = true;
                this.villageList[0x15d3a].whiteName = true;
                this.villageList[0x10571].whiteName = true;
                this.villageList[0x1188b].whiteName = true;
                this.villageList[0x1000].whiteName = true;
                this.villageList[0x12e97].whiteName = true;
                this.villageList[0xbe0f].whiteName = true;
                this.villageList[0x11326].whiteName = true;
                this.villageList[0x107d1].whiteName = true;
                this.villageList[0x13c6d].whiteName = true;
                this.villageList[0x123d7].whiteName = true;
                this.villageList[0x13490].whiteName = true;
                this.villageList[0x11e97].whiteName = true;
                this.villageList[0x40a5].whiteName = true;
                this.villageList[0x15b87].whiteName = true;
                this.villageList[0x2d6a].whiteName = true;
                this.villageList[0x16039].whiteName = true;
                this.villageList[0x1ad6].whiteName = true;
                this.villageList[0xd5b].whiteName = true;
                this.villageList[0x4081].whiteName = true;
                this.villageList[0x1424d].whiteName = true;
                this.villageList[0x70c7].whiteName = true;
                this.villageList[0x660f].whiteName = true;
                this.villageList[0x859a].whiteName = true;
                this.villageList[0x11e41].whiteName = true;
                this.villageList[0x89e1].whiteName = true;
                this.villageList[0x17e2e].whiteName = true;
                this.villageList[0x145ee].whiteName = true;
                this.villageList[0x1623].whiteName = true;
                this.villageList[0x14b1e].whiteName = true;
                this.villageList[0x22e3].whiteName = true;
                this.villageList[0x197aa].whiteName = true;
                this.villageList[0x9e00].whiteName = true;
                this.villageList[0xde8c].whiteName = true;
                this.villageList[0xda38].whiteName = true;
                this.villageList[0x113a8].whiteName = true;
                this.villageList[0x6f9c].whiteName = true;
                this.villageList[0x16776].whiteName = true;
                this.villageList[0x11474].whiteName = true;
                this.villageList[0x11be0].whiteName = true;
                this.villageList[0x1523].whiteName = true;
                this.villageList[0x577].whiteName = true;
                this.villageList[0x11b2c].whiteName = true;
                this.villageList[0x3cf5].whiteName = true;
                this.villageList[0x19944].whiteName = true;
                this.villageList[0x10854].whiteName = true;
                this.villageList[0xc55e].whiteName = true;
                this.villageList[0x11aed].whiteName = true;
                this.villageList[0xb771].whiteName = true;
                this.villageList[0xc55e].whiteName = true;
                this.villageList[0x11aed].whiteName = true;
                this.villageList[0x6c18].whiteName = true;
                this.villageList[0x540f].whiteName = true;
                this.villageList[0x57c6].whiteName = true;
                this.villageList[0x284c].whiteName = true;
                this.villageList[0xfaec].whiteName = true;
                this.villageList[0x6c18].whiteName = true;
                this.villageList[0xa5b5].whiteName = true;
                this.villageList[0xd989].whiteName = true;
                this.villageList[0x8a31].whiteName = true;
                this.villageList[0x785].whiteName = true;
                this.villageList[0x13cfb].whiteName = true;
                this.villageList[0x17b1b].whiteName = true;
                this.villageList[0xeed1].whiteName = true;
                this.villageList[0x21ff].whiteName = true;
                this.villageList[0x6e7e].whiteName = true;
                this.villageList[0x9489].whiteName = true;
                this.villageList[0x1a9c].whiteName = true;
                this.villageList[0x38c8].whiteName = true;
                this.villageList[0x1297c].whiteName = true;
                this.villageList[0x11d6f].whiteName = true;
                this.villageList[0x5dbb].whiteName = true;
                this.villageList[0xab1e].whiteName = true;
                this.villageList[0x16c62].whiteName = true;
                this.villageList[0x1819b].whiteName = true;
                this.villageList[0x474d].whiteName = true;
                this.villageList[0x14ca4].whiteName = true;
                this.villageList[0x5c7].whiteName = true;
                this.villageList[0xb771].whiteName = true;
                this.villageList[0xab23].whiteName = true;
                this.villageList[0x153b3].whiteName = true;
                this.villageList[0x16a50].whiteFlags = true;
                this.villageList[0x19816].whiteFlags = true;
                this.villageList[0x8c83].whiteFlags = true;
                this.villageList[0x9fad].whiteFlags = true;
                this.villageList[0x13808].whiteFlags = true;
                this.villageList[0x3cf6].whiteFlags = true;
                this.villageList[0xb82e].whiteFlags = true;
                this.villageList[0x9838].whiteFlags = true;
                this.villageList[0x377].whiteFlags = true;
                this.villageList[0xf70a].whiteFlags = true;
                this.villageList[0xbedc].whiteFlags = true;
                this.villageList[0x2656].whiteFlags = true;
                this.villageList[0xf772].whiteFlags = true;
                this.villageList[0x8b2e].whiteFlags = true;
                this.villageList[0x9e00].whiteFlags = true;
                this.villageList[0x1408a].whiteFlags = true;
                this.villageList[0x1488b].whiteFlags = true;
                this.villageList[0xa5f2].whiteFlags = true;
                this.villageList[0xe6e4].whiteFlags = true;
                this.villageList[0x74a3].whiteFlags = true;
                this.villageList[0x1b6f].whiteFlags = true;
                this.villageList[0x5196].whiteFlags = true;
                this.villageList[0xb9f9].whiteFlags = true;
                this.villageList[0x75fb].whiteFlags = true;
                this.villageList[0x94f4].whiteFlags = true;
                this.villageList[0x7bfc].whiteFlags = true;
                this.villageList[0x14aac].whiteFlags = true;
                this.villageList[0x18f50].whiteFlags = true;
                this.villageList[0x18b8a].whiteFlags = true;
                this.villageList[0xdf2d].whiteFlags = true;
                this.villageList[0xa7f9].whiteFlags = true;
                this.villageList[0x1733].whiteFlags = true;
                this.villageList[0x812c].whiteFlags = true;
                this.villageList[0x17a98].whiteFlags = true;
                this.villageList[0x11afe].whiteFlags = true;
                this.villageList[0xfe23].whiteFlags = true;
                this.villageList[0x13bfb].whiteFlags = true;
                this.villageList[0x90a6].whiteFlags = true;
                this.villageList[0xadba].whiteFlags = true;
                this.villageList[0x16acb].whiteFlags = true;
                this.villageList[0x4ec].whiteFlags = true;
                this.villageList[0x18194].whiteFlags = true;
                this.villageList[0x14a7b].whiteFlags = true;
                this.villageList[0x346c].whiteFlags = true;
                this.villageList[0x18f30].whiteFlags = true;
                this.villageList[0xf0b1].whiteFlags = true;
                this.villageList[0xc64f].whiteFlags = true;
                this.villageList[0x16289].whiteFlags = true;
                this.villageList[0x51f2].whiteFlags = true;
                this.villageList[0x19a68].whiteFlags = true;
                this.villageList[0x68c0].whiteFlags = true;
                this.villageList[0xd999].whiteFlags = true;
                this.villageList[0x11d86].whiteFlags = true;
                this.villageList[0x6f8f].whiteFlags = true;
                this.villageList[0x16292].whiteFlags = true;
                this.villageList[0xd028].whiteFlags = true;
                this.villageList[0x1449d].whiteFlags = true;
                this.villageList[0x164be].whiteFlags = true;
                this.villageList[0xeb7].whiteFlags = true;
                this.villageList[0x2d6a].whiteFlags = true;
                this.villageList[0x16039].whiteFlags = true;
                this.villageList[0x1ad6].whiteFlags = true;
                this.villageList[0xd5b].whiteFlags = true;
                this.villageList[0x4081].whiteFlags = true;
                this.villageList[0x1424d].whiteFlags = true;
                this.villageList[0x11d6f].whiteFlags = true;
                this.villageList[0x10520].whiteFlags = true;
                this.villageList[0xf7f4].whiteFlags = true;
                this.villageList[0xa51b].whiteFlags = true;
                this.villageList[0x12f4b].whiteFlags = true;
                this.villageList[0x18760].whiteFlags = true;
                this.villageList[0x143db].whiteFlags = true;
                this.villageList[0x10167].whiteFlags = true;
                this.villageList[0x19045].whiteFlags = true;
                this.villageList[0xdef0].whiteFlags = true;
                this.villageList[0x13246].whiteFlags = true;
                this.villageList[0x17e90].whiteFlags = true;
                this.villageList[0x8a31].whiteFlags = true;
                this.villageList[0x9489].whiteFlags = true;
                this.villageList[0x1297c].whiteFlags = true;
                this.villageList[0x68bf].whiteFlags = true;
                this.villageList[0x10571].whiteFlags = true;
                this.villageList[0x1188b].whiteFlags = true;
                this.villageList[0x295a].whiteFlags = true;
                this.villageList[0x4031].whiteFlags = true;
                this.villageList[0x124e9].whiteFlags = true;
                this.villageList[0x859a].whiteFlags = true;
                this.villageList[0x151b0].whiteFlags = true;
                this.villageList[0x190bc].whiteFlags = true;
                this.villageList[0x184b7].whiteFlags = true;
                this.villageList[0x152cf].whiteFlags = true;
                this.villageList[0xfaec].whiteFlags = true;
                this.villageList[0x10e5].whiteFlags = true;
            }
            bool flag2 = type.mapName.ToLower() == "de.wmpData".ToLower();
            bool flag3 = type.mapName.ToLower() == "fr.wmpData".ToLower();
            bool flag4 = type.mapName.ToLower() == "ru.wmpData".ToLower();
            if ((type.mapName.ToLower() == "uk2.wmpData".ToLower()) || (type.mapName.ToLower() == "ukai.wmpData".ToLower()))
            {
                this.provincesList[0x18].parentID = 3;
            }
            this.experimentalStuff(type.mapName.ToLower());
        }

        public void invalidateWorldData()
        {
            this.m_dataLoaded = false;
            this.loginHistory = null;
            this.m_userVillages = null;
        }

        public bool isAccount730Premium()
        {
            return (CardTypes.is730PremiumToken(this.UserCardData.premiumCard) && (VillageMap.getCurrentServerTime() < this.UserCardData.premiumCardExpiry));
        }

        public bool isAccountPremium()
        {
            return DataExport.isAccountPremium(this);
        }

        public bool isArmyMoving(long armyID)
        {
            LocalArmyData data = (LocalArmyData) this.armyArray[armyID];
            return ((data != null) && (data.targetVillageID >= 0));
        }

        public bool isArmyReallyReturning(long armyID)
        {
            LocalArmyData data = (LocalArmyData) this.armyArray[armyID];
            if ((data != null) && (data.lootType == 0x2710))
            {
                return false;
            }
            return true;
        }

        public bool isAttackableSpecial(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                switch (this.villageList[villageID].special)
                {
                    case 3:
                    case 5:
                    case 7:
                    case 9:
                    case 11:
                    case 13:
                    case 15:
                    case 0x11:
                        return true;
                }
                if (SpecialVillageTypes.IS_TREASURE_CASTLE(this.villageList[villageID].special))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isAttackingArmy(long armyID)
        {
            return (this.attackingArmies[armyID] != null);
        }

        public int iScale(int i, float scale)
        {
            return (int) (i * scale);
        }

        public bool isCapital(int villageID)
        {
            if (((villageID < 0) || (villageID >= this.villageList.Length)) || ((!this.villageList[villageID].regionCapital && !this.villageList[villageID].countyCapital) && (!this.villageList[villageID].provinceCapital && !this.villageList[villageID].countryCapital)))
            {
                return false;
            }
            return true;
        }

        public bool isCountryCapital(int villageID)
        {
            return (((villageID >= 0) && (villageID < this.villageList.Length)) && this.villageList[villageID].countryCapital);
        }

        public bool isCountyCapital(int villageID)
        {
            return (((villageID >= 0) && (villageID < this.villageList.Length)) && this.villageList[villageID].countyCapital);
        }

        public bool isDownloadComplete()
        {
            return this.downloadComplete;
        }

        public bool isForagingArmy(long armyID)
        {
            return (this.scoutsForaging[armyID] != null);
        }

        public bool isForagingSpecial(int villageID)
        {
            return (((villageID >= 0) && (villageID < this.villageList.Length)) && ((this.villageList[villageID].special >= 100) && (this.villageList[villageID].special <= 0xc7)));
        }

        public bool isForagingVillage(int villageID)
        {
            return (this.scoutsVillageForaging[villageID] != null);
        }

        public bool isInWolfsRevenge()
        {
            if (this.wolfsRevengeEnd < VillageMap.getCurrentServerTime())
            {
                return false;
            }
            return true;
        }

        public bool isLeaderboardCategoryPopulated(int mode)
        {
            SparseArray array = null;
            switch (mode)
            {
                case -6:
                    array = this.leaderboard_MainVillages;
                    break;

                case -5:
                    array = this.leaderboard_MainRank;
                    break;

                case -4:
                    array = this.leaderboard_ParishFlags;
                    break;

                case -3:
                    array = this.leaderboard_Houses;
                    break;

                case -2:
                    array = this.leaderboard_Factions;
                    break;

                case -1:
                    array = this.leaderboard_Main;
                    break;

                case 0:
                    array = this.leaderboard_Sub_Pillager;
                    break;

                case 1:
                    array = this.leaderboard_Sub_Defender;
                    break;

                case 2:
                    array = this.leaderboard_Sub_Ransack;
                    break;

                case 3:
                    array = this.leaderboard_Sub_Wolfsbane;
                    break;

                case 4:
                    array = this.leaderboard_Sub_Banditkiller;
                    break;

                case 5:
                    array = this.leaderboard_Sub_AIKiller;
                    break;

                case 6:
                    array = this.leaderboard_Sub_Trader;
                    break;

                case 7:
                    array = this.leaderboard_Sub_Forager;
                    break;

                case 8:
                    array = this.leaderboard_Sub_Stockpiler;
                    break;

                case 9:
                    array = this.leaderboard_Sub_Farmer;
                    break;

                case 10:
                    array = this.leaderboard_Sub_Brewer;
                    break;

                case 11:
                    array = this.leaderboard_Sub_Weaponsmith;
                    break;

                case 12:
                    array = this.leaderboard_Sub_banquetter;
                    break;

                case 13:
                    array = this.leaderboard_Sub_Achiever;
                    break;

                case 14:
                    array = this.leaderboard_Sub_Donater;
                    break;

                case 15:
                    array = this.leaderboard_Sub_Capture;
                    break;

                case 0x10:
                    array = this.leaderboard_Sub_Raze;
                    break;

                case 0x11:
                    array = this.leaderboard_Sub_Glory;
                    break;
            }
            if (array == null)
            {
                return false;
            }
            return (array.Count > 0);
        }

        public bool isNewTutorialAvailable()
        {
            return this.newTutorialAvailable;
        }

        public bool isOnlyOverVillageShield(int villageID, PointF mousePos)
        {
            if (this.isUserVillage(villageID) || this.isUserRelatedVillage(villageID))
            {
                VillageData data = this.villageList[villageID];
                double num = ((mousePos.X - (((double) this.m_screenWidth) / 2.0)) / this.m_worldScale) + this.m_screenCentreX;
                double num2 = ((mousePos.Y - (((double) this.m_screenHeight) / 2.0)) / this.m_worldScale) + this.m_screenCentreY;
                double num3 = num - data.x;
                double num4 = num2 - data.y;
                if (((num3 > -0.5) && (num3 < 0.3)) && ((num4 < -1.1) && (num4 > -2.0)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isOverVillageShield(int villageID, PointF mousePos, bool fromNotOwn)
        {
            if (this.isUserVillage(villageID) || this.isUserRelatedVillage(villageID))
            {
                VillageData data = this.villageList[villageID];
                double num = ((mousePos.X - (((double) this.m_screenWidth) / 2.0)) / this.m_worldScale) + this.m_screenCentreX;
                double num2 = ((mousePos.Y - (((double) this.m_screenHeight) / 2.0)) / this.m_worldScale) + this.m_screenCentreY;
                double num3 = num - data.x;
                double num4 = num2 - data.y;
                if (((num3 > -0.5) && (num3 < 0.3)) && ((num4 < -1.1) && (num4 > -2.0)))
                {
                    return true;
                }
                if (InterfaceMgr.Instance.OwnSelectedVillage < 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isProvinceCapital(int villageID)
        {
            return (((villageID >= 0) && (villageID < this.villageList.Length)) && this.villageList[villageID].provinceCapital);
        }

        public bool isQuestComplete(int quest)
        {
            if ((this.m_tutorialInfo != null) && (this.m_tutorialInfo.questsCompleted != null))
            {
                foreach (int num in this.m_tutorialInfo.questsCompleted)
                {
                    if (num == quest)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool isQuestObjectiveComplete(int quest)
        {
            return this.tutorialQuestsObjectivesComplete.Contains(quest);
        }

        public bool isRegionCapital(int villageID)
        {
            return (((villageID >= 0) && (villageID < this.villageList.Length)) && this.villageList[villageID].regionCapital);
        }

        public bool isResearchLagging()
        {
            if ((this.userResearchData.researchingType >= 0) && (VillageMap.getCurrentServerTime() > this.userResearchData.research_completionTime.AddSeconds(15.0)))
            {
                DateTime now = DateTime.Now;
                if (this.m_lastResearchCompleteTimeMatch == this.userResearchData.research_completionTime)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isRetrievingUserVillages()
        {
            return this.retrievingUserVillages;
        }

        public bool isScoutableSpecial(int villageID)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                if ((this.villageList[villageID].special >= 100) && (this.villageList[villageID].special <= 0xc7))
                {
                    return true;
                }
                switch (this.villageList[villageID].special)
                {
                    case 3:
                    case 5:
                    case 7:
                    case 9:
                    case 11:
                    case 13:
                    case 15:
                    case 0x11:
                        return true;
                }
                if (SpecialVillageTypes.IS_TREASURE_CASTLE(this.villageList[villageID].special))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isScoutHonourOutOfRange(int userVillageID, int targetVillageID)
        {
            if ((userVillageID < 0) || (targetVillageID < 0))
            {
                return false;
            }
            if (this.isCapital(userVillageID))
            {
                return false;
            }
            if (!this.isSpecial(targetVillageID))
            {
                return false;
            }
            int num = CardTypes.adjustScoutingHonourRange(this.UserCardData, GameEngine.Instance.LocalWorldData.BaseScoutHonourRange);
            int num2 = num * num;
            int x = this.villageList[targetVillageID].x;
            int y = this.villageList[targetVillageID].y;
            int num5 = this.villageList[userVillageID].x;
            int num6 = this.villageList[userVillageID].y;
            int num7 = ((x - num5) * (x - num5)) + ((y - num6) * (y - num6));
            if (num7 < num2)
            {
                return false;
            }
            return true;
        }

        public bool isSpecial(int villageID)
        {
            return (((villageID >= 0) && (villageID < this.villageList.Length)) && (this.villageList[villageID].special > 2));
        }

        public bool isSpecialAIPlayer(int villageID)
        {
            if (((villageID >= 0) && (villageID < this.villageList.Length)) && (this.villageList[villageID].special > 2))
            {
                switch (this.villageList[villageID].special)
                {
                    case 7:
                    case 9:
                    case 11:
                    case 13:
                        return true;
                }
            }
            return false;
        }

        public bool isSpyCommandDataActive(int villageID, int commandBits)
        {
            foreach (LocalPerson person in this.personArray)
            {
                if (((person.person.targetVillageID == villageID) && (person.person.personType == 1)) && ((person.person.state == 12) && ((person.person.spyCommandsDone & commandBits) != 0)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isTutorialActive()
        {
            return ((this.getTutorialStage() != -3) && (this.getTutorialStage() != -1));
        }

        public bool isTutorialResumable()
        {
            if (this.m_tutorialInfo == null)
            {
                return false;
            }
            return (this.m_tutorialInfo.resumeStage >= 0);
        }

        public bool isUserRelatedVillage(int villageID)
        {
            return (((villageID >= 0) && (villageID < this.villageList.Length)) && this.villageList[villageID].userRelatedVillage);
        }

        public bool isUserVillage(int villageID)
        {
            return (((villageID >= 0) && (villageID < this.villageList.Length)) && (this.villageList[villageID].userVillageID != -1));
        }

        public bool isValidArmyTarget(int villageID)
        {
            if ((villageID < 0) || (villageID >= this.villageList.Length))
            {
                return false;
            }
            if ((this.villageList[villageID].countyCapital || this.villageList[villageID].provinceCapital) || this.villageList[villageID].countryCapital)
            {
                return false;
            }
            if (this.villageList[villageID].regionCapital && (this.villageList[villageID].factionID == 0))
            {
                return false;
            }
            return true;
        }

        public bool isVassal(int yourVillage, int potentialVassalVillage)
        {
            return (((potentialVassalVillage >= 0) && (potentialVassalVillage < this.villageList.Length)) && (this.villageList[potentialVassalVillage].connecter == yourVillage));
        }

        public bool isVillageAVassal(int villageID)
        {
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    foreach (int num in data.vassals)
                    {
                        if (num == villageID)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool isVillageExcommunicated(int villageID)
        {
            return (((villageID >= 0) && (villageID < this.villageList.Length)) && (VillageMap.getCurrentServerTime() < this.villageList[villageID].excommunicationTime));
        }

        public bool isVillageInterdictProtected(int villageID)
        {
            return (((villageID >= 0) && (villageID < this.villageList.Length)) && (VillageMap.getCurrentServerTime() < this.villageList[villageID].interdictionTime));
        }

        public bool isVillageInvolvedInAIAttacks(int villageID)
        {
            return (this.villagesInvolvedInAIAttacks[villageID] != null);
        }

        public bool isVillageInvolvedInAttacks(int villageID)
        {
            return (this.villagesInvolvedInAttacks[villageID] != null);
        }

        public bool isVillageMarketTrading(int villageID)
        {
            return this.marketTradingVillageList.Contains(villageID);
        }

        public bool isVillagePeaceTimeProtected(int villageID)
        {
            return (((villageID >= 0) && (villageID < this.villageList.Length)) && (VillageMap.getCurrentServerTime() < this.villageList[villageID].peaceTime));
        }

        public bool isVillageTrading(int villageID)
        {
            return this.tradingVillageList.Contains(villageID);
        }

        public bool isVillageVacationProtected(int villageID)
        {
            return (((villageID >= 0) && (villageID < this.villageList.Length)) && this.villageList[villageID].vacationMode);
        }

        public bool isVillageVisible(int villageID)
        {
            return (((villageID >= 0) && (villageID < this.villageList.Length)) && this.villageList[villageID].visible);
        }

        public bool isWorkerThreadAlive()
        {
            return ((this.m_WorkerThread != null) && this.m_WorkerThread.IsAlive);
        }

        public bool isWorldShieldAvailable(int playerID)
        {
            if (playerID == RemoteServices.Instance.UserID)
            {
                if (this.playerShieldFactory != null)
                {
                    return this.playerShieldFactory.PlayerAvailable;
                }
            }
            else if (this.worldShieldsAvailable)
            {
                ShieldFactory factory = (ShieldFactory) this.worldShields[this.activeShieldsWorldID];
                if ((factory != null) && factory.WorldAvailable)
                {
                    return factory.isWorldShieldAvailable(playerID);
                }
            }
            return false;
        }

        public int lastClickedVillage()
        {
            double bestDist = -1.0;
            return this.findNearestVillageFromScreenPosAnyVis(this.lastClickedLocation, ref bestDist);
        }

        public void LeaderboardCallback(LeaderBoard_ReturnType returnData)
        {
            this.inDownloading = false;
            if (returnData.Success)
            {
                if (returnData.lastUpdate != this.leaderboardLastUpdateTime)
                {
                    if (this.leaderboardLastUpdateTime != DateTime.MinValue)
                    {
                        this.resetLeaderboards();
                    }
                    this.leaderboardLastUpdateTime = returnData.lastUpdate;
                }
                if (returnData.ownStandings != null)
                {
                    this.importStandings(returnData.ownStandings);
                }
                int maxValue = returnData.maxValue;
                SparseArray currentArray = null;
                switch (returnData.leaderboardType)
                {
                    case -6:
                        currentArray = this.leaderboard_MainVillages;
                        this.max_leaderboard_MainVillages = maxValue;
                        break;

                    case -5:
                        currentArray = this.leaderboard_MainRank;
                        this.max_leaderboard_MainRank = maxValue;
                        break;

                    case -4:
                        currentArray = this.leaderboard_ParishFlags;
                        this.max_leaderboard_ParishFlags = maxValue;
                        break;

                    case -3:
                        currentArray = this.leaderboard_Houses;
                        this.max_leaderboard_Houses = maxValue;
                        break;

                    case -2:
                        currentArray = this.leaderboard_Factions;
                        this.max_leaderboard_Factions = maxValue;
                        break;

                    case -1:
                        currentArray = this.leaderboard_Main;
                        this.max_leaderboard_Main = maxValue;
                        break;

                    case 0:
                        currentArray = this.leaderboard_Sub_Pillager;
                        this.max_leaderboard_Sub_Pillager = maxValue;
                        break;

                    case 1:
                        currentArray = this.leaderboard_Sub_Defender;
                        this.max_leaderboard_Sub_Defender = maxValue;
                        break;

                    case 2:
                        currentArray = this.leaderboard_Sub_Ransack;
                        this.max_leaderboard_Sub_Ransack = maxValue;
                        break;

                    case 3:
                        currentArray = this.leaderboard_Sub_Wolfsbane;
                        this.max_leaderboard_Sub_Wolfsbane = maxValue;
                        break;

                    case 4:
                        currentArray = this.leaderboard_Sub_Banditkiller;
                        this.max_leaderboard_Sub_Banditkiller = maxValue;
                        break;

                    case 5:
                        currentArray = this.leaderboard_Sub_AIKiller;
                        this.max_leaderboard_Sub_AIKiller = maxValue;
                        break;

                    case 6:
                        currentArray = this.leaderboard_Sub_Trader;
                        this.max_leaderboard_Sub_Trader = maxValue;
                        break;

                    case 7:
                        currentArray = this.leaderboard_Sub_Forager;
                        this.max_leaderboard_Sub_Forager = maxValue;
                        break;

                    case 8:
                        currentArray = this.leaderboard_Sub_Stockpiler;
                        this.max_leaderboard_Sub_Stockpiler = maxValue;
                        break;

                    case 9:
                        currentArray = this.leaderboard_Sub_Farmer;
                        this.max_leaderboard_Sub_Farmer = maxValue;
                        break;

                    case 10:
                        currentArray = this.leaderboard_Sub_Brewer;
                        this.max_leaderboard_Sub_Brewer = maxValue;
                        break;

                    case 11:
                        currentArray = this.leaderboard_Sub_Weaponsmith;
                        this.max_leaderboard_Sub_Weaponsmith = maxValue;
                        break;

                    case 12:
                        currentArray = this.leaderboard_Sub_banquetter;
                        this.max_leaderboard_Sub_banquetter = maxValue;
                        break;

                    case 13:
                        currentArray = this.leaderboard_Sub_Achiever;
                        this.max_leaderboard_Sub_Achiever = maxValue;
                        break;

                    case 14:
                        currentArray = this.leaderboard_Sub_Donater;
                        this.max_leaderboard_Sub_Donater = maxValue;
                        break;

                    case 15:
                        currentArray = this.leaderboard_Sub_Capture;
                        this.max_leaderboard_Sub_Capture = maxValue;
                        break;

                    case 0x10:
                        currentArray = this.leaderboard_Sub_Raze;
                        this.max_leaderboard_Sub_Raze = maxValue;
                        break;

                    case 0x11:
                        currentArray = this.leaderboard_Sub_Glory;
                        this.max_leaderboard_Sub_Glory = maxValue;
                        break;
                }
                this.importLeaderboardData(currentArray, returnData.leaderboardType, returnData.mainLeaderboard, returnData.subLeaderboard, returnData.parishLeaderboard, returnData.houseLeaderboard, returnData.factionLeaderboard);
            }
        }

        public void leaderboardLookHigher(int mode, int position, int pageSize)
        {
            SparseArray array = this.getLeaderboardArray(mode);
            int minValue = position;
            int maxValue = position;
            bool flag = false;
            for (int i = 1; i < (50 + pageSize); i++)
            {
                int num4 = position - i;
                if (num4 < 1)
                {
                    num4 = 1;
                }
                if (array[num4] != null)
                {
                    if ((i >= (pageSize + 5)) && !flag)
                    {
                        return;
                    }
                }
                else if (!flag)
                {
                    minValue = maxValue = num4;
                    flag = true;
                }
                else
                {
                    minValue = num4;
                }
            }
            if (minValue != position)
            {
                RemoteServices.Instance.LeaderBoard(mode, minValue, maxValue, this.leaderboardLastUpdateTime);
            }
        }

        public void leaderboardLookLower(int mode, int position, int pageSize)
        {
            position += pageSize;
            SparseArray array = this.getLeaderboardArray(mode);
            int num = this.getMaxLeaderboardEntries(mode);
            int minValue = position;
            int maxValue = position;
            bool flag = false;
            for (int i = 1; i < (50 + pageSize); i++)
            {
                int num5 = position + i;
                if (num5 >= num)
                {
                    num5 = num;
                }
                if (array[num5] != null)
                {
                    if ((i >= (pageSize + 5)) && !flag)
                    {
                        return;
                    }
                }
                else if (!flag)
                {
                    minValue = maxValue = num5;
                    flag = true;
                }
                else
                {
                    maxValue = num5;
                }
            }
            if (minValue != position)
            {
                RemoteServices.Instance.LeaderBoard(mode, minValue, maxValue, this.leaderboardLastUpdateTime);
            }
        }

        public void leaderboardSearch(int category, string searchString)
        {
            searchString = searchString.ToLowerInvariant();
            foreach (LeaderBoardSearchResults results in this.leaderboardSearchResults)
            {
                if (results.category != category)
                {
                    continue;
                }
                if (searchString == results.searchString)
                {
                    InterfaceMgr.Instance.leaderboardSearchComplete(results);
                    return;
                }
                if (searchString.Contains(results.searchString))
                {
                    LeaderBoardSearchResults item = new LeaderBoardSearchResults {
                        category = category,
                        searchString = searchString
                    };
                    switch (category)
                    {
                        case -4:
                        case -3:
                            break;

                        default:
                        {
                            SparseArray array = this.getLeaderboardArray(category);
                            foreach (int num in results.entries)
                            {
                                LeaderBoardEntryData data = (LeaderBoardEntryData) array[num];
                                if ((data != null) && data.name.ToLower().Contains(searchString))
                                {
                                    item.entries.Add(num);
                                }
                            }
                            break;
                        }
                    }
                    item.entries.Sort();
                    this.leaderboardSearchResults.Add(item);
                    InterfaceMgr.Instance.leaderboardSearchComplete(item);
                    return;
                }
            }
            this.inLeaderboardSearch = true;
            RemoteServices.Instance.set_LeaderBoardSearch_UserCallBack(new RemoteServices.LeaderBoardSearch_UserCallBack(this.leaderboardSearchCallback));
            RemoteServices.Instance.LeaderBoardSearch(category, searchString, this.leaderboardLastUpdateTime);
        }

        public void leaderboardSearchCallback(LeaderBoardSearch_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.lastUpdate != this.leaderboardLastUpdateTime)
                {
                    if (this.leaderboardLastUpdateTime != DateTime.MinValue)
                    {
                        this.resetLeaderboards();
                    }
                    this.leaderboardLastUpdateTime = returnData.lastUpdate;
                }
                if (returnData.ownStandings != null)
                {
                    this.importStandings(returnData.ownStandings);
                }
                int maxValue = returnData.maxValue;
                SparseArray currentArray = null;
                switch (returnData.leaderboardType)
                {
                    case -6:
                        currentArray = this.leaderboard_MainVillages;
                        this.max_leaderboard_MainVillages = maxValue;
                        break;

                    case -5:
                        currentArray = this.leaderboard_MainRank;
                        this.max_leaderboard_MainRank = maxValue;
                        break;

                    case -4:
                        currentArray = this.leaderboard_ParishFlags;
                        this.max_leaderboard_ParishFlags = maxValue;
                        break;

                    case -3:
                        currentArray = this.leaderboard_Houses;
                        this.max_leaderboard_Houses = maxValue;
                        break;

                    case -2:
                        currentArray = this.leaderboard_Factions;
                        this.max_leaderboard_Factions = maxValue;
                        break;

                    case -1:
                        currentArray = this.leaderboard_Main;
                        this.max_leaderboard_Main = maxValue;
                        break;

                    case 0:
                        currentArray = this.leaderboard_Sub_Pillager;
                        this.max_leaderboard_Sub_Pillager = maxValue;
                        break;

                    case 1:
                        currentArray = this.leaderboard_Sub_Defender;
                        this.max_leaderboard_Sub_Defender = maxValue;
                        break;

                    case 2:
                        currentArray = this.leaderboard_Sub_Ransack;
                        this.max_leaderboard_Sub_Ransack = maxValue;
                        break;

                    case 3:
                        currentArray = this.leaderboard_Sub_Wolfsbane;
                        this.max_leaderboard_Sub_Wolfsbane = maxValue;
                        break;

                    case 4:
                        currentArray = this.leaderboard_Sub_Banditkiller;
                        this.max_leaderboard_Sub_Banditkiller = maxValue;
                        break;

                    case 5:
                        currentArray = this.leaderboard_Sub_AIKiller;
                        this.max_leaderboard_Sub_AIKiller = maxValue;
                        break;

                    case 6:
                        currentArray = this.leaderboard_Sub_Trader;
                        this.max_leaderboard_Sub_Trader = maxValue;
                        break;

                    case 7:
                        currentArray = this.leaderboard_Sub_Forager;
                        this.max_leaderboard_Sub_Forager = maxValue;
                        break;

                    case 8:
                        currentArray = this.leaderboard_Sub_Stockpiler;
                        this.max_leaderboard_Sub_Stockpiler = maxValue;
                        break;

                    case 9:
                        currentArray = this.leaderboard_Sub_Farmer;
                        this.max_leaderboard_Sub_Farmer = maxValue;
                        break;

                    case 10:
                        currentArray = this.leaderboard_Sub_Brewer;
                        this.max_leaderboard_Sub_Brewer = maxValue;
                        break;

                    case 11:
                        currentArray = this.leaderboard_Sub_Weaponsmith;
                        this.max_leaderboard_Sub_Weaponsmith = maxValue;
                        break;

                    case 12:
                        currentArray = this.leaderboard_Sub_banquetter;
                        this.max_leaderboard_Sub_banquetter = maxValue;
                        break;

                    case 13:
                        currentArray = this.leaderboard_Sub_Achiever;
                        this.max_leaderboard_Sub_Achiever = maxValue;
                        break;

                    case 14:
                        currentArray = this.leaderboard_Sub_Donater;
                        this.max_leaderboard_Sub_Donater = maxValue;
                        break;

                    case 15:
                        currentArray = this.leaderboard_Sub_Capture;
                        this.max_leaderboard_Sub_Capture = maxValue;
                        break;

                    case 0x10:
                        currentArray = this.leaderboard_Sub_Raze;
                        this.max_leaderboard_Sub_Raze = maxValue;
                        break;

                    case 0x11:
                        currentArray = this.leaderboard_Sub_Glory;
                        this.max_leaderboard_Sub_Glory = maxValue;
                        break;
                }
                this.importLeaderboardData(currentArray, returnData.leaderboardType, returnData.mainLeaderboard, returnData.subLeaderboard, returnData.parishLeaderboard, returnData.houseLeaderboard, returnData.factionLeaderboard);
                LeaderBoardSearchResults item = new LeaderBoardSearchResults();
                switch (returnData.leaderboardType)
                {
                    case -6:
                    case -5:
                    case -1:
                        foreach (LeaderboardDataMainClass class2 in returnData.mainLeaderboard)
                        {
                            item.entries.Add(class2.standing);
                        }
                        break;

                    case -4:
                        foreach (ParishFlagLeaderboardInfo info2 in returnData.parishLeaderboard)
                        {
                            item.entries.Add(info2.standing);
                        }
                        break;

                    case -2:
                        foreach (FactionLeaderboardInfo info in returnData.factionLeaderboard)
                        {
                            item.entries.Add(info.standing);
                        }
                        break;

                    default:
                        foreach (LeaderboardSubDataClass class3 in returnData.subLeaderboard)
                        {
                            item.entries.Add(class3.standing);
                        }
                        break;
                }
                item.entries.Sort();
                item.searchString = returnData.searchString;
                item.category = returnData.leaderboardType;
                this.leaderboardSearchResults.Add(item);
                InterfaceMgr.Instance.leaderboardSearchComplete(item);
            }
            this.inLeaderboardSearch = false;
        }

        public void leftMouseDown(Point mousePos)
        {
            if (!this.m_leftMouseHeldDown)
            {
                this.m_lastMousePressedTime = DXTimer.GetCurrentMilliseconds();
                this.m_leftMouseHeldDown = true;
                this.m_baseMousePos = mousePos;
                this.m_baseScreenX = this.m_screenCentreX;
                this.m_baseScreenY = this.m_screenCentreY;
                this.m_leftMouseGrabbed = false;
            }
            else if ((((DXTimer.GetCurrentMilliseconds() - this.m_lastMousePressedTime) > 250.0) || (Math.Abs((int) (this.m_baseMousePos.X - mousePos.X)) > 3)) || (Math.Abs((int) (this.m_baseMousePos.Y - mousePos.Y)) > 3))
            {
                bool flag = true;
                if ((!this.m_leftMouseGrabbed && (Math.Abs((int) (this.m_baseMousePos.X - mousePos.X)) <= 3)) && ((Math.Abs((int) (this.m_baseMousePos.Y - mousePos.Y)) <= 3) && ((27.0 - this.m_worldZoom) > 18.5)))
                {
                    double bestDist = 100000.0;
                    int num3 = this.findNearestVillageFromScreenPos(mousePos, ref bestDist);
                    if (bestDist > 4.0)
                    {
                        num3 = -1;
                    }
                    long num4 = -1L;
                    long num5 = -1L;
                    long num6 = -1L;
                    long num7 = -1L;
                    if ((num3 < 0) && (InterfaceMgr.Instance.WorldMapMode == 0))
                    {
                        double num8 = 0.0;
                        num4 = this.findNearestArmyFromScreenPos(mousePos, ref num8);
                        if ((num4 >= 0L) && (num8 > 4.0))
                        {
                            num4 = -1L;
                        }
                        if (num4 < 0L)
                        {
                            double num9 = 0.0;
                            num5 = this.findNearestTraderFromScreenPos(mousePos, ref num9);
                            if ((num5 >= 0L) && (num9 > 4.0))
                            {
                                num5 = -1L;
                            }
                            if (num5 < 0L)
                            {
                                double num10 = 0.0;
                                num6 = this.findNearestReinforcementFromScreenPos(mousePos, ref num10);
                                if ((num6 >= 0L) && (num10 > 4.0))
                                {
                                    num6 = -1L;
                                }
                                if (num6 < 0L)
                                {
                                    double num11 = 0.0;
                                    num7 = this.findNearestPersonFromScreenPos(mousePos, ref num11);
                                    if ((num7 >= 0L) && (num11 > 4.0))
                                    {
                                        num7 = -1L;
                                    }
                                }
                            }
                        }
                    }
                    if (((num4 >= 0L) || (num3 >= 0)) || (((num5 >= 0L) || (num6 >= 0L)) || (num7 >= 0L)))
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    this.m_zooming = false;
                    this.m_leftMouseGrabbed = true;
                    this.m_screenCentreX = this.m_baseScreenX;
                    this.m_screenCentreY = this.m_baseScreenY;
                    double mapPosX = 0.0;
                    double mapPosY = 0.0;
                    this.getMapCoords(mousePos, ref mapPosX, ref mapPosY);
                    double num14 = 0.0;
                    double num15 = 0.0;
                    this.getMapCoords(this.m_baseMousePos, ref num14, ref num15);
                    this.m_screenCentreX = (this.m_baseScreenX + num14) - mapPosX;
                    this.m_screenCentreY = (this.m_baseScreenY + num15) - mapPosY;
                    this.moveMap(0.0, 0.0);
                    this.centreMap(false);
                }
            }
        }

        public void loadData(string dataName)
        {
            this.drawFakeProvinceBorders = false;
            this.EUMap = false;
            this.yMarkerOffset = 0;
            int num = 0;
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            if (dataName.ToLower() == "de.wmpData".ToLower())
            {
                flag5 = true;
            }
            if (dataName.ToLower() == "sa.wmpData".ToLower())
            {
                this.yMarkerOffset = -10;
            }
            if (dataName.ToLower() == "uk.wmpData".ToLower())
            {
                flag = true;
            }
            if (dataName.ToLower() == "ukai.wmpData".ToLower())
            {
                flag6 = true;
            }
            if (dataName.ToLower() == "uk2.wmpData".ToLower())
            {
                flag2 = true;
            }
            if (dataName.ToLower() == "fr.wmpData".ToLower())
            {
                num = 1;
                flag3 = true;
            }
            if (dataName.ToLower() == "es.wmpData".ToLower())
            {
                num = 1;
                flag4 = true;
            }
            this.smallMapFont = false;
            if (dataName.ToLower() == "eu.wmpData".ToLower())
            {
                this.smallMapFont = true;
                this.EUMap = true;
            }
            dataName = Application.StartupPath + @"\assets\" + dataName;
            FileStream input = new FileStream(dataName, FileMode.Open);
            BinaryReader reader = new BinaryReader(input);
            int num2 = reader.ReadInt32();
            for (int i = 0; i < num2; i++)
            {
                reader.ReadString();
            }
            int num4 = reader.ReadInt32();
            int[] numArray = new int[num4];
            for (int j = 0; j < num4; j++)
            {
                numArray[j] = reader.ReadInt32();
            }
            reader.Close();
            input.Close();
            int num6 = 0;
            this.worldMapWidth = numArray[num6++];
            this.worldMapHeight = numArray[num6++];
            this.villageMapWidth = numArray[num6++];
            this.villageMapHeight = numArray[num6++];
            int index = numArray[num6++];
            this.pointList = new WorldPoint[index + num];
            for (int k = 0; k < index; k++)
            {
                this.pointList[k] = new WorldPoint { x = numArray[num6++], y = numArray[num6++] };
            }
            if (flag3)
            {
                this.pointList[index] = new WorldPoint { x = 1885f, y = 2204f };
            }
            int num9 = numArray[num6++];
            this.countryList = new WorldPointList[num9];
            for (int m = 0; m < num9; m++)
            {
                WorldPointList list = new WorldPointList {
                    parentID = numArray[num6++],
                    capitalVillage = numArray[num6++]
                };
                int x = numArray[num6++];
                int y = numArray[num6++];
                list.marker = new Point(x, y);
                num6++;
                int num13 = numArray[num6++];
                list.borderList = new int[num13];
                for (int num14 = 0; num14 < num13; num14++)
                {
                    list.borderList[num14] = numArray[num6++];
                    list.updateBounds(this.pointList[list.borderList[num14]]);
                }
                int num15 = numArray[num6++];
                list.childList = new int[num15];
                for (int num16 = 0; num16 < num15; num16++)
                {
                    list.childList[num16] = numArray[num6++];
                }
                int num17 = numArray[num6++];
                list.triangleList = new Triangle[num17];
                for (int num18 = 0; num18 < num17; num18++)
                {
                    list.triangleList[num18] = new Triangle { x1 = ((float) numArray[num6++]) / 10000f, y1 = ((float) numArray[num6++]) / 10000f, x2 = ((float) numArray[num6++]) / 10000f, y2 = ((float) numArray[num6++]) / 10000f, x3 = ((float) numArray[num6++]) / 10000f, y3 = ((float) numArray[num6++]) / 10000f };
                }
                this.countryList[m] = list;
            }
            int num19 = numArray[num6++];
            this.provincesList = new WorldPointList[num19];
            for (int n = 0; n < num19; n++)
            {
                WorldPointList list2 = new WorldPointList {
                    parentID = numArray[num6++],
                    capitalVillage = numArray[num6++]
                };
                int num21 = numArray[num6++];
                int num22 = numArray[num6++];
                list2.marker = new Point(num21, num22);
                num6++;
                int num23 = numArray[num6++];
                list2.borderList = new int[num23];
                for (int num24 = 0; num24 < num23; num24++)
                {
                    list2.borderList[num24] = numArray[num6++];
                    list2.updateBounds(this.pointList[list2.borderList[num24]]);
                }
                int num25 = numArray[num6++];
                list2.childList = new int[num25];
                for (int num26 = 0; num26 < num25; num26++)
                {
                    list2.childList[num26] = numArray[num6++];
                }
                int num27 = numArray[num6++];
                list2.triangleList = new Triangle[num27];
                for (int num28 = 0; num28 < num27; num28++)
                {
                    list2.triangleList[num28] = new Triangle { x1 = ((float) numArray[num6++]) / 10000f, y1 = ((float) numArray[num6++]) / 10000f, x2 = ((float) numArray[num6++]) / 10000f, y2 = ((float) numArray[num6++]) / 10000f, x3 = ((float) numArray[num6++]) / 10000f, y3 = ((float) numArray[num6++]) / 10000f };
                }
                this.provincesList[n] = list2;
            }
            int num29 = numArray[num6++];
            this.countyList = new WorldPointList[num29];
            for (int num30 = 0; num30 < num29; num30++)
            {
                WorldPointList list3 = new WorldPointList {
                    parentID = numArray[num6++],
                    capitalVillage = numArray[num6++]
                };
                int num31 = numArray[num6++];
                int num32 = numArray[num6++];
                list3.marker = new Point(num31, num32);
                num6++;
                int num33 = numArray[num6++];
                list3.borderList = new int[num33];
                for (int num34 = 0; num34 < num33; num34++)
                {
                    list3.borderList[num34] = numArray[num6++];
                    list3.updateBounds(this.pointList[list3.borderList[num34]]);
                }
                int num35 = numArray[num6++];
                list3.childList = new int[num35];
                for (int num36 = 0; num36 < num35; num36++)
                {
                    list3.childList[num36] = numArray[num6++];
                }
                int num37 = numArray[num6++];
                list3.triangleList = new Triangle[num37];
                for (int num38 = 0; num38 < num37; num38++)
                {
                    list3.triangleList[num38] = new Triangle { x1 = ((float) numArray[num6++]) / 10000f, y1 = ((float) numArray[num6++]) / 10000f, x2 = ((float) numArray[num6++]) / 10000f, y2 = ((float) numArray[num6++]) / 10000f, x3 = ((float) numArray[num6++]) / 10000f, y3 = ((float) numArray[num6++]) / 10000f };
                }
                this.countyList[num30] = list3;
            }
            int num39 = numArray[num6++];
            this.villageList = new VillageData[num39];
            for (int num40 = 0; num40 < num39; num40++)
            {
                VillageData data = new VillageData {
                    id = numArray[num6++],
                    x = numArray[num6++],
                    y = numArray[num6++]
                };
                if ((num40 == 0x172a6) && flag5)
                {
                    data.y--;
                }
                data.countyID = numArray[num6++];
                data.regionID = numArray[num6++];
                int num41 = numArray[num6++];
                if ((num41 & 1) != 0)
                {
                    data.regionCapital = true;
                }
                else
                {
                    data.regionCapital = false;
                }
                if ((num41 & 2) != 0)
                {
                    data.coastalVillage = true;
                }
                else
                {
                    data.coastalVillage = false;
                }
                if ((num41 & 4) != 0)
                {
                    data.mountainVillage = true;
                }
                else
                {
                    data.mountainVillage = false;
                }
                if ((num41 & 8) != 0)
                {
                    data.countyCapital = true;
                }
                else
                {
                    data.countyCapital = false;
                }
                if ((num41 & 0x10) != 0)
                {
                    data.provinceCapital = true;
                }
                else
                {
                    data.provinceCapital = false;
                }
                if ((num41 & 0x20) != 0)
                {
                    data.countryCapital = true;
                }
                else
                {
                    data.countryCapital = false;
                }
                this.villageList[num40] = data;
                num6++;
            }
            int num42 = numArray[num6++];
            this.regionList = new WorldPointList[num42];
            for (int num43 = 0; num43 < num42; num43++)
            {
                WorldPointList list4 = new WorldPointList {
                    parentID = numArray[num6++]
                };
                num6++;
                int num44 = numArray[num6++];
                list4.childList = new int[num44];
                for (int num45 = 0; num45 < num44; num45++)
                {
                    list4.childList[num45] = numArray[num6++];
                }
                int num46 = numArray[num6++];
                list4.triangleList = new Triangle[num46];
                for (int num47 = 0; num47 < num46; num47++)
                {
                    list4.triangleList[num47] = new Triangle { x1 = ((float) numArray[num6++]) / 10000f, y1 = ((float) numArray[num6++]) / 10000f, x2 = ((float) numArray[num6++]) / 10000f, y2 = ((float) numArray[num6++]) / 10000f, x3 = ((float) numArray[num6++]) / 10000f, y3 = ((float) numArray[num6++]) / 10000f };
                }
                list4.updateBoundsFromTriangles();
                int num48 = numArray[num6++];
                list4.regionBorderList = new WorldPoint[num48];
                for (int num49 = 0; num49 < num48; num49++)
                {
                    list4.regionBorderList[num49] = new WorldPoint { x = numArray[num6++], y = numArray[num6++] };
                }
                this.regionList[num43] = list4;
            }
            foreach (VillageData data2 in this.villageList)
            {
                if (data2.regionCapital)
                {
                    this.regionList[data2.regionID].capitalVillage = data2.id;
                }
            }
            int num50 = numArray[num6++];
            List<WorldPointList> list5 = new List<WorldPointList>();
            for (int num51 = 0; num51 < num50; num51++)
            {
                WorldPointList item = new WorldPointList {
                    data = numArray[num6++]
                };
                if (item.data >= 0)
                {
                    int num52 = numArray[num6++];
                    item.triangleList = new Triangle[num52];
                    for (int num53 = 0; num53 < num52; num53++)
                    {
                        item.triangleList[num53] = new Triangle { x1 = ((float) numArray[num6++]) / 10000f, y1 = ((float) numArray[num6++]) / 10000f, x2 = ((float) numArray[num6++]) / 10000f, y2 = ((float) numArray[num6++]) / 10000f, x3 = ((float) numArray[num6++]) / 10000f, y3 = ((float) numArray[num6++]) / 10000f };
                    }
                    item.updateBoundsFromTriangles();
                    list5.Add(item);
                }
                else
                {
                    this.drawFakeProvinceBorders = true;
                    if (item.data > -2000)
                    {
                        int num54 = -item.data - 0x3e8;
                        WorldPointList list7 = this.provincesList[num54];
                        List<int> list8 = new List<int>();
                        if (!list7.rebuiltBorderList)
                        {
                            list7.rebuiltBorderList = true;
                        }
                        list8.AddRange(list7.borderList);
                        list8.Add(-1);
                        int num55 = numArray[num6++];
                        for (int num56 = 0; num56 < num55; num56++)
                        {
                            int num57 = numArray[num6++];
                            list8.Add(num57);
                            list7.updateBounds(this.pointList[num57]);
                        }
                        list7.borderList = list8.ToArray();
                        int num58 = numArray[num6++];
                        num6 += num58 * 6;
                    }
                    else
                    {
                        int num59 = -item.data - 0xbb8;
                        WorldPointList list9 = this.countryList[num59];
                        List<int> list10 = new List<int>();
                        if (!list9.rebuiltBorderList)
                        {
                            list9.rebuiltBorderList = true;
                        }
                        list10.AddRange(list9.borderList);
                        list10.Add(-1);
                        int num60 = numArray[num6++];
                        for (int num61 = 0; num61 < num60; num61++)
                        {
                            int num62 = numArray[num6++];
                            list10.Add(num62);
                            list9.updateBounds(this.pointList[num62]);
                        }
                        list9.borderList = list10.ToArray();
                        int num63 = numArray[num6++];
                        num6 += num63 * 6;
                    }
                }
            }
            this.seaList = list5.ToArray();
            int num64 = numArray[num6++];
            this.islandList = new IslandInfoList[num64];
            for (int num65 = 0; num65 < num64; num65++)
            {
                this.islandList[num65] = new IslandInfoList { county = numArray[num6++], province = numArray[num6++], country = numArray[num6++], sx = numArray[num6++], sy = numArray[num6++], ex = numArray[num6++], ey = numArray[num6++] };
            }
            if (flag)
            {
                this.villageList[0xa62a].x = 0x4e7;
                this.villageList[0xa62a].y = 0x414;
                this.villageList[0x5756].x = 0x436;
                this.villageList[0x5756].y = 0x20d;
                this.villageList[0x11d85].x = 0x424;
                this.villageList[0x11d85].y = 0x210;
                this.villageList[0xc05a].x = 0x439;
                this.villageList[0xc05a].y = 0x215;
                this.villageList[0x12fe8].x = 0x42e;
                this.villageList[0x12fe8].y = 0x211;
                this.villageList[0x10e1d].x = 0x411;
                this.villageList[0x10e1d].y = 0x221;
                this.villageList[0xbf92].x = 0x266;
                this.villageList[0xbf92].y = 0x816;
                this.villageList[0xda9a].x = 0x5f7;
                this.villageList[0xda9a].y = 0x517;
                this.villageList[0x18231].x = 0x5fc;
                this.villageList[0x18231].y = 0x512;
                this.villageList[0xfe31].x = 0x3d2;
                this.villageList[0xfe31].y = 950;
                this.villageList[0x4a59].x = 0x460;
                this.villageList[0x4a59].y = 0x363;
                this.villageList[0x95d0].x = 0x45b;
                this.villageList[0x95d0].y = 0x36a;
            }
            if ((flag || flag2) || flag6)
            {
                this.villageList[0x11130].x = 0x722;
                this.villageList[0x11130].y = 0xabb;
            }
            if (flag5)
            {
                this.villageList[0x21ca].x = 0x6a5;
                this.villageList[0x21ca].y = 0xa4d;
            }
            if (flag4)
            {
                this.villageList[0xe81b].x = 0x5ac;
                this.villageList[0xe81b].y = 0x574;
                this.villageList[0x673c].x = 0x5ba;
                this.villageList[0x673c].y = 0x4fb;
                this.villageList[0x7ec3].x = 0x5aa;
                this.villageList[0x7ec3].y = 0x4f5;
            }
            this.aiWorldTreeBuilding = false;
            this.aiWorldInvasionLineList = null;
            this.aiWorldSpecialVillages.Clear();
            if (flag6)
            {
                this.aiWorldTreeBuilding = true;
                this.aiWorldInvasionLineList = new List<IslandInfoList>();
                foreach (VillageData data3 in this.villageList)
                {
                    int num66 = data3.x;
                    int num67 = data3.y;
                    if (AIWorldSettings.getAIWorldVillageLocation(data3.id, ref data3.x, ref data3.y))
                    {
                        IslandInfoList list12 = new IslandInfoList {
                            villageID = data3.id
                        };
                        double num68 = num66;
                        double num69 = num67;
                        double num70 = data3.x;
                        double num71 = data3.y;
                        double num72 = num68 - num70;
                        double num73 = num69 - num71;
                        double num74 = Math.Sqrt((num72 * num72) + (num73 * num73));
                        num72 /= num74;
                        num73 /= num74;
                        num68 -= num72 * 50.0;
                        num69 -= num73 * 50.0;
                        num70 += num72 * 25.0;
                        num71 += num73 * 25.0;
                        list12.sx = (int) num68;
                        list12.sy = (int) num69;
                        list12.ex = (int) num70;
                        list12.ey = (int) num71;
                        this.aiWorldInvasionLineList.Add(list12);
                        this.aiWorldSpecialVillages.Add(data3.id);
                    }
                }
            }
            this.buildVillageTree();
            this.initUserVillages();
        }

        public bool loadFactionData()
        {
            string str = GameEngine.getSettingsPath(false);
            FileStream input = null;
            BinaryReader reader = null;
            try
            {
                input = new FileStream(str + @"\VillageData" + this.m_globalWorldID.ToString() + ".dat", FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(input);
                if (reader.ReadInt32() != 10)
                {
                    reader.Close();
                    input.Close();
                    return false;
                }
                byte[] b = new byte[0x10];
                b = reader.ReadBytes(0x10);
                Guid guid = new Guid(b);
                if (RemoteServices.Instance.WorldGUID.CompareTo(guid) != 0)
                {
                    reader.Close();
                    input.Close();
                    return false;
                }
                this.storedVillageFactionsPos = reader.ReadInt64();
                for (int i = 0; i < this.villageList.Length; i++)
                {
                    this.villageList[i].factionID = reader.ReadInt32();
                    this.villageList[i].userID = reader.ReadInt32();
                    this.villageList[i].connecter = reader.ReadInt32();
                    this.villageList[i].special = reader.ReadInt32();
                    this.villageList[i].villageTerrain = reader.ReadInt32();
                    this.villageList[i].numFlags = reader.ReadInt32();
                }
                this.storedRegionFactionsPos = reader.ReadInt64();
                this.storedParishFlagsPos = reader.ReadInt64();
                this.storedCountyFlagsPos = reader.ReadInt64();
                this.storedProvinceFlagsPos = reader.ReadInt64();
                this.storedCountryFlagsPos = reader.ReadInt64();
                for (int j = 0; j < this.regionList.Length; j++)
                {
                    this.regionList[j].factionID = reader.ReadInt32();
                    this.regionList[j].userID = reader.ReadInt32();
                    this.regionList[j].plague = reader.ReadInt32();
                }
                this.storedCountyFactionsPos = reader.ReadInt64();
                for (int k = 0; k < this.countyList.Length; k++)
                {
                    this.countyList[k].factionID = reader.ReadInt32();
                    this.countyList[k].userID = reader.ReadInt32();
                }
                this.storedProvinceFactionsPos = reader.ReadInt64();
                for (int m = 0; m < this.provincesList.Length; m++)
                {
                    this.provincesList[m].factionID = reader.ReadInt32();
                    this.provincesList[m].userID = reader.ReadInt32();
                }
                this.storedCountryFactionsPos = reader.ReadInt64();
                for (int n = 0; n < this.countryList.Length; n++)
                {
                    this.countryList[n].factionID = reader.ReadInt32();
                    this.countryList[n].userID = reader.ReadInt32();
                }
                for (int num7 = 0; num7 < this.villageList.Length; num7++)
                {
                    this.villageList[num7].visible = reader.ReadBoolean();
                }
                this.storedFactionChangesPos = reader.ReadInt64();
                int num8 = reader.ReadInt32();
                for (int num9 = 0; num9 < num8; num9++)
                {
                    FactionData data = new FactionData {
                        factionID = reader.ReadInt32(),
                        active = reader.ReadBoolean(),
                        factionName = reader.ReadString(),
                        factionNameAbrv = reader.ReadString(),
                        houseID = reader.ReadInt32(),
                        numMembers = reader.ReadInt32(),
                        points = reader.ReadInt32(),
                        flagData = reader.ReadInt32(),
                        openForApplications = reader.ReadBoolean()
                    };
                    this.m_factionData[data.factionID] = data;
                }
                reader.Close();
                input.Close();
            }
            catch (Exception)
            {
                try
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    if (input != null)
                    {
                        input.Close();
                    }
                }
                catch (Exception)
                {
                }
                return false;
            }
            return true;
        }

        public void loadInitParishText()
        {
        }

        public void loadLocalWorldData()
        {
            this.m_factionData.Clear();
            this.m_downloadedDataSafely = true;
            if (!this.m_cachesFlushed)
            {
                this.m_dataLoaded = this.loadFactionData();
                this.m_namesLoaded = this.loadNamesData();
                UniversalDebugLog.Log("m_dataLoaded = " + this.m_dataLoaded.ToString());
                UniversalDebugLog.Log("m_namesLoaded = " + this.m_namesLoaded.ToString());
                int num = 0;
                int num2 = 0;
                foreach (VillageData data in this.villageList)
                {
                    if (data.villageName.Length == 0)
                    {
                        num++;
                    }
                    if (data.visible)
                    {
                        num2++;
                    }
                }
                if ((num > 500) || (num2 < 2))
                {
                    this.m_dataLoaded = false;
                    this.m_namesLoaded = false;
                }
            }
            else
            {
                this.m_cachesFlushed = false;
                this.m_dataLoaded = false;
                this.m_namesLoaded = false;
            }
        }

        public bool loadNamesData()
        {
            string str = GameEngine.getSettingsPath(false);
            FileStream input = null;
            BinaryReader reader = null;
            try
            {
                input = new FileStream(str + @"\NameData" + this.m_globalWorldID.ToString() + ".dat", FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(input);
                byte[] b = new byte[0x10];
                b = reader.ReadBytes(0x10);
                Guid guid = new Guid(b);
                if (RemoteServices.Instance.WorldGUID.CompareTo(guid) != 0)
                {
                    reader.Close();
                    input.Close();
                    return false;
                }
                bool flag = false;
                this.storedVillageNamePos = reader.ReadInt64();
                int num = 0;
                for (int i = 0; i < this.villageList.Length; i++)
                {
                    this.villageList[i].villageName = reader.ReadString();
                    num ^= this.villageList[i].villageName.GetHashCode();
                }
                for (int j = 0; j < this.regionList.Length; j++)
                {
                    this.regionList[j].areaName = reader.ReadString();
                    num ^= this.regionList[j].areaName.GetHashCode();
                    if (this.regionList[j].areaName.Length == 0)
                    {
                        flag = true;
                    }
                }
                for (int k = 0; k < this.countyList.Length; k++)
                {
                    this.countyList[k].areaName = reader.ReadString();
                    num ^= this.countyList[k].areaName.GetHashCode();
                }
                for (int m = 0; m < this.provincesList.Length; m++)
                {
                    this.provincesList[m].areaName = reader.ReadString();
                    num ^= this.provincesList[m].areaName.GetHashCode();
                }
                for (int n = 0; n < this.countryList.Length; n++)
                {
                    this.countryList[n].areaName = reader.ReadString();
                    num ^= this.countryList[n].areaName.GetHashCode();
                }
                int num7 = reader.ReadInt32();
                reader.Close();
                input.Close();
                if (num7 != num)
                {
                    return false;
                }
                if (flag)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                try
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    if (input != null)
                    {
                        input.Close();
                    }
                }
                catch (Exception)
                {
                }
                return false;
            }
            return true;
        }

        public void logout()
        {
            this.clearGloryHistory();
            if (this.cachedUserInfo != null)
            {
                foreach (CachedUserInfo info in this.cachedUserInfo)
                {
                    if ((info != null) && (info.avatarBitmap != null))
                    {
                        info.avatarBitmap.Dispose();
                        info.avatarBitmap = null;
                    }
                }
            }
            this.cachedUserInfo = new SparseArray();
            this.cachedFactionMemberData = new SparseArray();
            if (this.villageList != null)
            {
                foreach (VillageData data in this.villageList)
                {
                    data.rolloverInfo = null;
                }
            }
            this.cached_retrieveUserID = -1;
            this.cached_retrieveVillageID = -1;
            this.downloadingCounter = 0;
            this.mProfileCardsSet = new List<int>();
            this.playbackItems = null;
            this.playingCountries = false;
            this.playingProvinces = false;
            this.invasionMarkerState = new SparseArray();
            this.m_userRelatedVillages.Clear();
        }

        public Triangle makeTriangle(WorldPoint p1, WorldPoint p2, WorldPoint p3)
        {
            return new Triangle { x1 = p1.x, y1 = p1.y, x2 = p2.x, y2 = p2.y, x3 = p3.x, y3 = p3.y };
        }

        public void manageDynamicLines()
        {
            if (InterfaceMgr.Instance.MapSelectedArmy >= 0L)
            {
                LocalArmyData data = (LocalArmyData) this.armyArray[InterfaceMgr.Instance.MapSelectedArmy];
                if (((data != null) && (data.targetVillageID >= 0)) && (data.travelFromVillageID >= 0))
                {
                    PointF start = data.TargetPoint();
                    PointF end = new PointF((float) data.displayX, (float) data.displayY);
                    PointF tf3 = data.BasePoint();
                    this.addDynamicInterVillageLine(start, end, 3, 1.2f);
                    this.addDynamicInterVillageLine(end, tf3, 4, 1.2f);
                }
            }
            if (InterfaceMgr.Instance.MapSelectedReinforcement >= 0L)
            {
                LocalArmyData data2 = (LocalArmyData) this.reinforcementArray[InterfaceMgr.Instance.MapSelectedReinforcement];
                if (((data2 != null) && (data2.targetVillageID >= 0)) && (data2.travelFromVillageID >= 0))
                {
                    PointF tf4 = data2.TargetPoint();
                    PointF tf5 = new PointF((float) data2.displayX, (float) data2.displayY);
                    PointF tf6 = data2.BasePoint();
                    this.addDynamicInterVillageLine(tf4, tf5, 3, 1.2f);
                    this.addDynamicInterVillageLine(tf5, tf6, 4, 1.2f);
                }
            }
            if (InterfaceMgr.Instance.MapSelectedTrader >= 0L)
            {
                LocalTrader trader = (LocalTrader) this.traderArray[InterfaceMgr.Instance.MapSelectedTrader];
                if (((trader != null) && (trader.trader.targetVillageID >= 0)) && (trader.trader.homeVillageID >= 0))
                {
                    PointF tf7 = trader.TargetPoint();
                    PointF tf8 = new PointF((float) trader.displayX, (float) trader.displayY);
                    PointF tf9 = trader.BasePoint();
                    this.addDynamicInterVillageLine(tf7, tf8, 3, 1.2f);
                    this.addDynamicInterVillageLine(tf8, tf9, 4, 1.2f);
                }
            }
            if (InterfaceMgr.Instance.MapSelectedPerson >= 0L)
            {
                LocalPerson person = (LocalPerson) this.personArray[InterfaceMgr.Instance.MapSelectedPerson];
                if (((person != null) && (person.person.targetVillageID >= 0)) && (person.person.homeVillageID >= 0))
                {
                    PointF tf10 = person.TargetPoint();
                    PointF tf11 = new PointF((float) person.displayX, (float) person.displayY);
                    PointF tf12 = person.BasePoint();
                    this.addDynamicInterVillageLine(tf10, tf11, 3, 1.2f);
                    this.addDynamicInterVillageLine(tf11, tf12, 4, 1.2f);
                }
            }
            if (InterfaceMgr.Instance.SelectedVassalVillage >= 0)
            {
                if (((InterfaceMgr.Instance.SelectedVillage >= 0) && (InterfaceMgr.Instance.SelectedVassalVillage >= 0)) && (InterfaceMgr.Instance.SelectedVillage != InterfaceMgr.Instance.SelectedVassalVillage))
                {
                    VillageData data3 = this.villageList[InterfaceMgr.Instance.SelectedVassalVillage];
                    VillageData data4 = this.villageList[InterfaceMgr.Instance.SelectedVillage];
                    float scalar = 2f - (((float) data3.villageInfo) / 120f);
                    this.addDynamicInterVillageLine((PointF) new Point(data4.x, data4.y), (PointF) new Point(data3.x, data3.y), 5, scalar);
                }
            }
            else if (((InterfaceMgr.Instance.SelectedVillage >= 0) && (InterfaceMgr.Instance.OwnSelectedVillage >= 0)) && (InterfaceMgr.Instance.SelectedVillage != InterfaceMgr.Instance.OwnSelectedVillage))
            {
                VillageData data5 = this.villageList[InterfaceMgr.Instance.OwnSelectedVillage];
                VillageData data6 = this.villageList[InterfaceMgr.Instance.SelectedVillage];
                float num2 = 2f - (((float) data5.villageInfo) / 120f);
                this.addDynamicInterVillageLine((PointF) new Point(data6.x, data6.y), (PointF) new Point(data5.x, data5.y), 5, num2);
            }
            if (((this.m_rolloverTargetVillage >= 0) && (InterfaceMgr.Instance.OwnSelectedVillage >= 0)) && (InterfaceMgr.Instance.OwnSelectedVillage != this.m_rolloverTargetVillage))
            {
                VillageData data7 = this.villageList[InterfaceMgr.Instance.OwnSelectedVillage];
                VillageData data8 = this.villageList[this.m_rolloverTargetVillage];
                float num3 = 2f - (((float) data7.villageInfo) / 120f);
                this.addDynamicInterVillageLine((PointF) new Point(data8.x, data8.y), (PointF) new Point(data7.x, data7.y), 6, num3);
            }
        }

        public void maybeMultiAccount(int level)
        {
            int num = 0x578;
            if (level == 0)
            {
                num = 0x578;
            }
            else if (level == 1)
            {
                num = 0xa8c;
            }
            if (num > this.threadDelaySize)
            {
                this.threadDelaySize = num;
            }
        }

        public void monitorAIInvasionActivity()
        {
            if (GameEngine.Instance.LocalWorldData.AIWorld)
            {
                if (this.lastInvasionInfoTime < DateTime.Now)
                {
                    this.downloadAIInvasionInfo();
                }
                else if (this.lastUpdateInvasionInfoTime < DateTime.Now)
                {
                    this.lastUpdateInvasionInfoTime = DateTime.Now.AddMinutes(5.0);
                    this.updateAIInvasions();
                }
            }
        }

        public void monitorCachedVillageUserInfo()
        {
            if ((this.cached_retrieveVillageID != -1) || (this.cached_retrieveUserID != -1))
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.cached_retrieveVillageUserInfoDate);
                if (span.TotalMilliseconds > 800.0)
                {
                    RemoteServices.Instance.set_RetrieveVillageUserInfo_UserCallBack(new RemoteServices.RetrieveVillageUserInfo_UserCallBack(this.villageUserInfoCallback));
                    RemoteServices.Instance.RetrieveVillageUserInfo(this.cached_retrieveVillageID, this.cached_retrieveUserID, false);
                    this.cached_retrieveVillageID = -1;
                    this.cached_retrieveUserID = -1;
                }
            }
        }

        public void mouseNotClicked(Point mousePos)
        {
            if (this.m_leftMouseHeldDown)
            {
                if (!this.m_leftMouseGrabbed)
                {
                    double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
                    bool doubleClick = false;
                    if ((((currentMilliseconds - this.m_doubleClickTime) < 300.0) && (Math.Abs((int) (mousePos.X - this.m_doubleClickMousePos.X)) < 3)) && (Math.Abs((int) (mousePos.Y - this.m_doubleClickMousePos.Y)) < 3))
                    {
                        doubleClick = true;
                    }
                    this.windowClicked(mousePos, doubleClick);
                    this.m_doubleClickTime = currentMilliseconds;
                    this.m_doubleClickMousePos = mousePos;
                }
                this.m_leftMouseHeldDown = false;
                this.m_leftMouseGrabbed = false;
            }
        }

        public void moveMap(double dx, double dy)
        {
            this.m_screenCentreX += dx;
            this.m_screenCentreY += dy;
            if (this.m_screenCentreX < 0.0)
            {
                this.m_screenCentreX = 0.0;
            }
            if (this.m_screenCentreY < 0.0)
            {
                this.m_screenCentreY = 0.0;
            }
            if (this.m_screenCentreX >= this.worldMapWidth)
            {
                this.m_screenCentreX = this.worldMapWidth - 1;
            }
            if (this.m_screenCentreY >= this.worldMapHeight)
            {
                this.m_screenCentreY = this.worldMapHeight - 1;
            }
        }

        public void moveMouse(Point mousePos)
        {
            this.m_rolloverTargetVillage = -1;
            this.m_rolloverTargetVillageNoDelay = -1;
            this.m_rolloverVillageShieldID = -1;
            if ((27.0 - this.m_worldZoom) > 13.0)
            {
                double bestDist = 100000.0;
                int villageID = this.findNearestVillageFromScreenPos(mousePos, ref bestDist);
                if (bestDist > 4.0)
                {
                    villageID = -1;
                }
                long num3 = -1L;
                long num4 = -1L;
                long num5 = -1L;
                long num6 = -1L;
                if ((villageID < 0) && (InterfaceMgr.Instance.WorldMapMode == 0))
                {
                    double num7 = 0.0;
                    num3 = this.findNearestArmyFromScreenPos(mousePos, ref num7);
                    if ((num3 >= 0L) && (num7 > 4.0))
                    {
                        num3 = -1L;
                    }
                    if (num3 < 0L)
                    {
                        double num8 = 0.0;
                        num4 = this.findNearestTraderFromScreenPos(mousePos, ref num8);
                        if ((num4 >= 0L) && (num8 > 4.0))
                        {
                            num4 = -1L;
                        }
                        if (num4 < 0L)
                        {
                            double num9 = 0.0;
                            num5 = this.findNearestReinforcementFromScreenPos(mousePos, ref num9);
                            if ((num5 >= 0L) && (num9 > 4.0))
                            {
                                num5 = -1L;
                            }
                            if (num5 < 0L)
                            {
                                double num10 = 0.0;
                                num6 = this.findNearestPersonFromScreenPos(mousePos, ref num10);
                                if ((num6 >= 0L) && (num10 > 4.0))
                                {
                                    num6 = -1L;
                                }
                            }
                        }
                    }
                }
                if ((((num3 < 0L) && (villageID < 0)) && ((num4 < 0L) && (num6 < 0L))) && (((num5 < 0L) && !InterfaceMgr.Instance.isMenuPopupOpen()) && !InterfaceMgr.Instance.isInsideAchievementPopup()))
                {
                    CursorManager.SetCursor(CursorManager.CursorType.Hand, InterfaceMgr.Instance.ParentForm);
                }
                else
                {
                    CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
                }
                bool flag = false;
                if (this.m_rolloverLastMousepos == mousePos)
                {
                    if ((DXTimer.GetCurrentMillisecondsLong() - this.m_rolloverLastTime) > 150L)
                    {
                        flag = true;
                    }
                }
                else
                {
                    this.m_rolloverLastMousepos = mousePos;
                    this.m_rolloverLastTime = DXTimer.GetCurrentMillisecondsLong();
                }
                if (((villageID >= 0) && (this.m_worldZoom < 0.001)) && !this.m_leftMouseHeldDown)
                {
                    this.m_rolloverTargetVillageNoDelay = villageID;
                    if (flag)
                    {
                        this.m_rolloverTargetVillage = villageID;
                        if (this.isOverVillageShield(villageID, (PointF) mousePos, false))
                        {
                            this.m_rolloverVillageShieldID = villageID;
                            this.m_rolloverTargetVillage = -1;
                        }
                    }
                }
            }
            else
            {
                double num12 = 100000.0;
                int num13 = this.findNearestVillageFromScreenPos(mousePos, ref num12);
                if (num12 > 4.0)
                {
                    num13 = -1;
                }
                if (((num13 < 0) && !InterfaceMgr.Instance.isMenuPopupOpen()) && !InterfaceMgr.Instance.isInsideAchievementPopup())
                {
                    CursorManager.SetCursor(CursorManager.CursorType.Hand, InterfaceMgr.Instance.ParentForm);
                }
                else
                {
                    CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
                }
            }
        }

        public void nextStageZoom(bool initialStage)
        {
            if (this.m_zoomStage < 0)
            {
                return;
            }
            int zoomStage = this.m_zoomStage;
            switch (zoomStage)
            {
                case 0:
                    if (this.m_stagedTargetZoom == 27.0)
                    {
                        double num2 = ((this.m_screenCentreX - this.m_stagedTargetX) * (this.m_screenCentreX - this.m_stagedTargetX)) + ((this.m_screenCentreY - this.m_stagedTargetY) * (this.m_screenCentreY - this.m_stagedTargetY));
                        if (num2 >= 10000.0)
                        {
                            if (num2 < 30625.0)
                            {
                                zoomStage = 4;
                            }
                            break;
                        }
                        zoomStage = 5;
                    }
                    break;

                case 1:
                    if (this.m_stagedTargetZoom >= 9.5)
                    {
                        double num3 = ((this.m_screenCentreX - this.m_stagedTargetX) * (this.m_screenCentreX - this.m_stagedTargetX)) + ((this.m_screenCentreY - this.m_stagedTargetY) * (this.m_screenCentreY - this.m_stagedTargetY));
                        if (num3 < 360000.0)
                        {
                            zoomStage = 3;
                        }
                    }
                    if (zoomStage == 1)
                    {
                        this.setZooming(3.5, this.m_screenCentreX, this.m_screenCentreY, initialStage);
                    }
                    else
                    {
                        this.setZooming(3.5, ((this.m_screenCentreX - this.m_stagedTargetX) / 2.0) + this.m_stagedTargetX, ((this.m_screenCentreY - this.m_stagedTargetY) / 2.0) + this.m_stagedTargetY, initialStage);
                    }
                    goto Label_02C0;

                case 2:
                    this.setZooming(this.m_zoomCap, ((this.m_screenCentreX - this.m_stagedTargetX) / 2.0) + this.m_stagedTargetX, ((this.m_screenCentreY - this.m_stagedTargetY) / 2.0) + this.m_stagedTargetY, initialStage);
                    goto Label_02C0;

                case 3:
                    this.setZooming(3.5, this.m_stagedTargetX, this.m_stagedTargetY, initialStage);
                    goto Label_02C0;

                case 4:
                    this.setZooming(9.5, this.m_stagedTargetX, this.m_stagedTargetY, initialStage);
                    goto Label_02C0;

                case 5:
                    this.m_zoomStage = zoomStage + 1;
                    this.setZooming(27.0, this.m_stagedTargetX, this.m_stagedTargetY, initialStage);
                    goto Label_02C0;

                default:
                    goto Label_02C0;
            }
            switch (zoomStage)
            {
                case 0:
                    this.setZooming(9.5, this.m_screenCentreX, this.m_screenCentreY, initialStage);
                    break;

                case 4:
                    this.setZooming(9.5, ((this.m_screenCentreX - this.m_stagedTargetX) / 2.0) + this.m_stagedTargetX, ((this.m_screenCentreY - this.m_stagedTargetY) / 2.0) + this.m_stagedTargetY, initialStage);
                    break;

                case 5:
                    this.m_zoomStage = zoomStage + 1;
                    this.setZooming(27.0, this.m_stagedTargetX, this.m_stagedTargetY, initialStage);
                    break;
            }
        Label_02C0:
            this.m_zoomStage = zoomStage + 1;
            if (zoomStage >= 3)
            {
                if (this.m_stagedTargetZoom <= this.m_targetZoom)
                {
                    this.m_zoomStage = -1;
                }
                this.centreMap(true);
            }
        }

        public int numCounties()
        {
            return this.countyList.Length;
        }

        public int numUserCounties()
        {
            int num = 0;
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    if (data.countyCapital)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public int numUserCountries()
        {
            int num = 0;
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    if (data.countryCapital)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public int numUserParishes()
        {
            int num = 0;
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    if (data.parishCapital)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public int numUserProvinces()
        {
            int num = 0;
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    if (data.provinceCapital)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public int numVassalsAllowed()
        {
            int userRank = this.m_userRank;
            if (userRank < 0)
            {
                return 0;
            }
            return GameEngine.Instance.LocalWorldData.getMaxVassals(userRank, this.m_userRankSubLevel);
        }

        public int numVassalsAllowed(int rank)
        {
            if (rank < 0)
            {
                return 0;
            }
            return GameEngine.Instance.LocalWorldData.getMaxVassals(rank, this.m_userRankSubLevel);
        }

        public int numVassalsOwned()
        {
            return 0;
        }

        public int numVillagesAllowed()
        {
            int num = ResearchData.leadershipVillages[this.UserResearchData.Research_Leadership];
            if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
            {
                num = ResearchData.leadershipVillages2[this.UserResearchData.Research_Leadership];
            }
            if ((num >= 0x19) && GameEngine.Instance.World.FourthAgeWorld)
            {
                return 40;
            }
            if ((num >= 0x19) && GameEngine.Instance.World.ThirdAgeWorld)
            {
                num = 30;
            }
            return num;
        }

        public int numVillagesOwned()
        {
            if (this.m_userVillages == null)
            {
                return 1;
            }
            int num = 0;
            int count = this.m_userVillages.Count;
            for (int i = 0; i < count; i++)
            {
                if (!this.villageList[this.m_userVillages[i].villageID].Capital)
                {
                    num++;
                }
            }
            return num;
        }

        public int numWheelTypesAvailable()
        {
            int num = 0;
            if (this.m_treasure1Tickets > 0)
            {
                num++;
            }
            if (this.m_treasure2Tickets > 0)
            {
                num++;
            }
            if (this.m_treasure3Tickets > 0)
            {
                num++;
            }
            if (this.m_treasure4Tickets > 0)
            {
                num++;
            }
            if (this.m_treasure5Tickets > 0)
            {
                num++;
            }
            if (this.m_numQuestTickets > 0)
            {
                num++;
            }
            return num;
        }

        public PointF pixelAlignPoint(float x, float y)
        {
            return new PointF((float) Math.Round((double) x), (float) Math.Round((double) y));
        }

        public void playbackCountries()
        {
            if (this.gotPlaybackData())
            {
                this.playingCountries = true;
                this.playingProvinces = false;
                this.playbackDay = 0;
                this.playbackStartTime = DateTime.Now;
            }
        }

        public void playbackProvinces()
        {
            if (this.gotPlaybackData())
            {
                this.playingCountries = false;
                this.playingProvinces = true;
                this.playbackDay = 0;
                this.playbackStartTime = DateTime.Now;
            }
        }

        public void processFactionsList(List<FactionData> factionsList, long currentFactionChangePos)
        {
            foreach (FactionData data in factionsList)
            {
                this.m_factionData[data.factionID] = data;
            }
            if (GameEngine.Instance.LocalWorldData.AIWorld)
            {
                foreach (FactionData data2 in this.m_factionData)
                {
                    if ((data2.factionID >= 1) && (data2.factionID <= 4))
                    {
                        data2.numMembers = 1;
                        data2.openForApplications = false;
                        data2.houseRank = 10;
                        switch (data2.factionID)
                        {
                            case 1:
                                data2.flagData = 0x38b88197;
                                break;

                            case 2:
                                data2.flagData = 0x38202088;
                                break;

                            case 3:
                                data2.flagData = 0x3862eb8b;
                                break;

                            case 4:
                                data2.flagData = 0x3822e0ab;
                                break;
                        }
                    }
                }
            }
            this.inactiveFaction.active = false;
            int factionID = -1;
            foreach (FactionData data3 in this.m_factionData)
            {
                if (data3.factionID > factionID)
                {
                    factionID = data3.factionID;
                }
            }
            for (int i = 0; i <= factionID; i++)
            {
                if (this.m_factionData[i] == null)
                {
                    this.m_factionData[i] = this.inactiveFaction;
                }
            }
            this.storedFactionChangesPos = currentFactionChangePos;
        }

        public void processVillageFactionChangesList(AreaUpdateListItem[] ownerList, long newPos)
        {
            int num = 0;
            bool flag = false;
            foreach (AreaUpdateListItem item in ownerList)
            {
                if ((item != null) && (item.areaID >= 0))
                {
                    if (item.areaID < this.villageList.Length)
                    {
                        if (((this.villageList[item.areaID].userID == RemoteServices.Instance.UserID) || (item.newOwnerID == RemoteServices.Instance.UserID)) && (this.villageList[item.areaID].userID != item.newOwnerID))
                        {
                            flag = true;
                        }
                        this.villageList[item.areaID].factionID = item.newFactionID;
                        this.villageList[item.areaID].userID = item.newOwnerID;
                        this.villageList[item.areaID].visible = true;
                        this.villageList[item.areaID].connecter = item.connectorID;
                        if (item.special != -1)
                        {
                            this.villageList[item.areaID].special = item.special;
                        }
                        if (item.mapTerrain >= 0)
                        {
                            this.villageList[item.areaID].villageTerrain = item.mapTerrain;
                        }
                        if (((item.special == 2) || (item.special == -1)) || (item.special == 20))
                        {
                            this.villageList[item.areaID].visible = false;
                        }
                        this.villageList[item.areaID].rolloverInfo = null;
                    }
                }
                else
                {
                    num++;
                }
            }
            if ((ownerList.Length < (newPos - this.storedVillageFactionsPos)) || (num > 1))
            {
                num = 100;
            }
            if (num >= 0)
            {
                this.storedVillageFactionsPos = newPos;
                this.updateUserVassals();
                if (flag)
                {
                    this.retrieveUserVillages(false);
                }
            }
            if (ownerList.Length > 0)
            {
                this.fixupVisibleParishCapitals();
            }
        }

        public void processVillageFactionList(int[,] ownerList, long newPos)
        {
            int index = 0;
            int num2 = 0;
            if (ownerList.GetUpperBound(1) == 5)
            {
                for (int i = 0; i < (ownerList.Length / 6); i++)
                {
                    this.villageList[index].factionID = ownerList[i, 0];
                    this.villageList[index].userID = ownerList[i, 1];
                    if (ownerList[i, 2] == 1)
                    {
                        this.villageList[index].visible = true;
                        num2++;
                    }
                    else
                    {
                        this.villageList[index].visible = false;
                    }
                    this.villageList[index].connecter = ownerList[i, 3];
                    this.villageList[index].special = ownerList[i, 4];
                    this.villageList[index].villageTerrain = ownerList[i, 5];
                    if (this.villageList[index].special == 20)
                    {
                        this.villageList[index].visible = false;
                    }
                    this.villageList[index].rolloverInfo = null;
                    index++;
                }
            }
            else
            {
                for (int j = 0; j < (ownerList.Length / 6); j++)
                {
                    if (index >= this.villageList.Length)
                    {
                        break;
                    }
                    this.villageList[index].factionID = ownerList[0, j];
                    this.villageList[index].userID = ownerList[1, j];
                    if (ownerList[2, j] == 1)
                    {
                        this.villageList[index].visible = true;
                        num2++;
                    }
                    else
                    {
                        this.villageList[index].visible = false;
                    }
                    this.villageList[index].connecter = ownerList[3, j];
                    this.villageList[index].special = ownerList[4, j];
                    this.villageList[index].villageTerrain = ownerList[5, j];
                    if (this.villageList[index].special == 20)
                    {
                        this.villageList[index].visible = false;
                    }
                    this.villageList[index].rolloverInfo = null;
                    index++;
                }
            }
            this.updateUserVassals();
            if (num2 > 2)
            {
                this.storedVillageFactionsPos = newPos;
            }
            else
            {
                this.storedVillageFactionsPos = -1L;
            }
            if (ownerList.Length > 0)
            {
                this.fixupVisibleParishCapitals();
            }
        }

        public void registerParishWallDonateDetails(ParishWallDetailInfo_ReturnType returnData)
        {
            int parishCapitalID = returnData.parishCapitalID;
            int userID = returnData.userID;
            ParishWallDonateInfo info = new ParishWallDonateInfo {
                returnData = returnData,
                lastUpdateTime = DateTime.Now
            };
            long num3 = (parishCapitalID << 0x20) + userID;
            this.m_parishWallDonateDetails[num3] = info;
        }

        public void registerWorldIdentifier(int worldID)
        {
            if (worldID != this.m_globalWorldID)
            {
                this.m_globalWorldID = worldID;
            }
        }

        public void registerWorldZoomCallback(WorldZoomCallback newWorldZoomCallback)
        {
            this.worldZoomCallback = newWorldZoomCallback;
        }

        public List<int> removeDuplicateQuests(List<int> availableQuests)
        {
            List<int> list = new List<int>();
            list.AddRange(this.m_newQuestData.completedQuests);
            List<int> list2 = new List<int>();
            foreach (int num in availableQuests)
            {
                if ((!list.Contains(num) && !list2.Contains(num)) && (this.m_newQuestData.questID != num))
                {
                    list2.Add(num);
                }
            }
            return list2;
        }

        public void removeProfileCard(int id)
        {
            if (!this.ProfileCards.ContainsKey(id))
            {
                throw new Exception("Tried to remove a card that wasn't there: UserTradingCardID=" + id);
            }
            this.ProfileCards.Remove(id);
            if (this.ProfileCardsSearch.Contains(id))
            {
                this.ProfileCardsSearch.Remove(id);
            }
            if (this.ProfileCardsSet.Contains(id))
            {
                this.ProfileCardsSet.Remove(id);
            }
        }

        public Image renderAIShield(int AI, int width, int height, int bmapWidth, int bmapHeight)
        {
            if (AI == -1)
            {
                Image sourceImage = this.ratShield.Render(width, height, bmapWidth, bmapHeight);
                return this.shieldOverlay(sourceImage, width, height, width, height);
            }
            if (AI == -2)
            {
                Image image2 = this.snakeShield.Render(width, height, bmapWidth, bmapHeight);
                return this.shieldOverlay(image2, width, height, width, height);
            }
            if (AI == -3)
            {
                Image image3 = this.pigShield.Render(width, height, bmapWidth, bmapHeight);
                return this.shieldOverlay(image3, width, height, width, height);
            }
            if (AI == -4)
            {
                Image image4 = this.wolfShield.Render(width, height, bmapWidth, bmapHeight);
                return this.shieldOverlay(image4, width, height, width, height);
            }
            return null;
        }

        public void resetLeaderboards()
        {
            this.leaderboardSearchResults = new List<LeaderBoardSearchResults>();
            this.leaderboard_Main = new SparseArray();
            this.leaderboard_MainRank = new SparseArray();
            this.leaderboard_MainVillages = new SparseArray();
            this.leaderboard_Factions = new SparseArray();
            this.leaderboard_Houses = new SparseArray();
            this.leaderboard_ParishFlags = new SparseArray();
            this.leaderboard_Sub_Pillager = new SparseArray();
            this.leaderboard_Sub_Defender = new SparseArray();
            this.leaderboard_Sub_Ransack = new SparseArray();
            this.leaderboard_Sub_Wolfsbane = new SparseArray();
            this.leaderboard_Sub_Banditkiller = new SparseArray();
            this.leaderboard_Sub_AIKiller = new SparseArray();
            this.leaderboard_Sub_Trader = new SparseArray();
            this.leaderboard_Sub_Forager = new SparseArray();
            this.leaderboard_Sub_Stockpiler = new SparseArray();
            this.leaderboard_Sub_Farmer = new SparseArray();
            this.leaderboard_Sub_Brewer = new SparseArray();
            this.leaderboard_Sub_Weaponsmith = new SparseArray();
            this.leaderboard_Sub_banquetter = new SparseArray();
            this.leaderboard_Sub_Achiever = new SparseArray();
            this.leaderboard_Sub_Donater = new SparseArray();
            this.leaderboard_Sub_Capture = new SparseArray();
            this.leaderboard_Sub_Raze = new SparseArray();
            this.leaderboard_Sub_Glory = new SparseArray();
            this.max_leaderboard_Main = -1;
            this.max_leaderboard_MainRank = -1;
            this.max_leaderboard_MainVillages = -1;
            this.max_leaderboard_Factions = -1;
            this.max_leaderboard_Houses = -1;
            this.max_leaderboard_ParishFlags = -1;
            this.max_leaderboard_Sub_Pillager = -1;
            this.max_leaderboard_Sub_Defender = -1;
            this.max_leaderboard_Sub_Ransack = -1;
            this.max_leaderboard_Sub_Wolfsbane = -1;
            this.max_leaderboard_Sub_Banditkiller = -1;
            this.max_leaderboard_Sub_AIKiller = -1;
            this.max_leaderboard_Sub_Trader = -1;
            this.max_leaderboard_Sub_Forager = -1;
            this.max_leaderboard_Sub_Stockpiler = -1;
            this.max_leaderboard_Sub_Farmer = -1;
            this.max_leaderboard_Sub_Brewer = -1;
            this.max_leaderboard_Sub_Weaponsmith = -1;
            this.max_leaderboard_Sub_banquetter = -1;
            this.max_leaderboard_Sub_Achiever = -1;
            this.max_leaderboard_Sub_Donater = -1;
            this.max_leaderboard_Sub_Capture = -1;
            this.max_leaderboard_Sub_Raze = -1;
            this.max_leaderboard_Sub_Glory = -1;
            this.lastZeroDownload_leaderboard_Main = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_MainRank = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_MainVillages = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Factions = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Houses = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_ParishFlags = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Pillager = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Defender = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Ransack = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Wolfsbane = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Banditkiller = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_AIKiller = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Trader = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Forager = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Stockpiler = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Farmer = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Brewer = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Weaponsmith = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_banquetter = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Achiever = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Donater = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Capture = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Raze = DateTime.MinValue;
            this.lastZeroDownload_leaderboard_Sub_Glory = DateTime.MinValue;
            this.inDownloading = false;
        }

        public void resetParishTextReadID()
        {
            foreach (ParishChatData data in this.m_parishChatLog)
            {
                data.m_readIDs[0] = -1L;
                data.m_readIDs[1] = -1L;
                data.m_readIDs[2] = -1L;
                data.m_readIDs[3] = -1L;
                data.m_readIDs[4] = -1L;
                data.m_readIDs[5] = -1L;
            }
        }

        public void reSetRanking()
        {
            InterfaceMgr.Instance.setRank(this.m_userRank);
        }

        public void resetTutorialInfo()
        {
            this.m_tutorialInfo = new QuestsAndTutorialInfo();
            this.QuestObjectivesSent = new SparseArray();
            this.tutorialQuestsObjectivesComplete = new List<int>();
        }

        public void restartTutorial()
        {
            RemoteServices.Instance.set_TutorialCommand_UserCallBack(new RemoteServices.TutorialCommand_UserCallBack(this.TutorialCommandCallback));
            RemoteServices.Instance.TutorialCommand(-4);
        }

        public void resumeTutorial()
        {
            this.inTutorialAdvance = true;
            RemoteServices.Instance.set_TutorialCommand_UserCallBack(new RemoteServices.TutorialCommand_UserCallBack(this.TutorialCommandCallback));
            RemoteServices.Instance.TutorialCommand(-5);
        }

        public void retrieveArmies()
        {
            this.highestDownloadedArmy = -1L;
            this.armyArray.Clear();
            RemoteServices.Instance.set_GetArmyData_UserCallBack(new RemoteServices.GetArmyData_UserCallBack(this.getArmyData));
            RemoteServices.Instance.set_RetrieveAttackResult_UserCallBack(new RemoteServices.RetrieveAttackResult_UserCallBack(this.retrieveAttackResultCallback));
            RemoteServices.Instance.GetArmyData(-2L);
        }

        public void retrieveAttackResultCallback(RetrieveAttackResult_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.armyData != null)
                {
                    if ((this.tutorialArmyID != -1L) && (returnData.armyData.armyID == this.tutorialArmyID))
                    {
                        this.tutorialArmyID = -1L;
                        TutorialBattleReportPopup popup = new TutorialBattleReportPopup();
                        popup.init();
                        popup.Show(InterfaceMgr.Instance.ParentForm);
                    }
                    if (this.isUserVillage(returnData.armyData.targetVillageID) || this.isUserRelatedVillage(returnData.armyData.targetVillageID))
                    {
                        GameEngine.Instance.flushVillage(returnData.armyData.targetVillageID);
                    }
                    if (((LocalArmyData) this.armyArray[returnData.armyData.armyID]) == null)
                    {
                        return;
                    }
                    if (returnData.armyData.dead)
                    {
                        this.armyArray[returnData.armyData.armyID] = null;
                        return;
                    }
                    ArmyReturnData[] armyReturnData = new ArmyReturnData[] { returnData.armyData };
                    this.doGetArmyData(armyReturnData, null, false);
                    if (returnData.reinforcementData != null)
                    {
                        this.doGetArmyData(null, returnData.reinforcementData, true);
                    }
                }
                if (returnData.villageUpdateList != null)
                {
                    if (returnData.userVillageList != null)
                    {
                        bool retrievingUserVillages = this.retrievingUserVillages;
                        this.retrievingUserVillages = true;
                        this.processVillageFactionChangesList(returnData.villageUpdateList, returnData.currentVillageChangePos);
                        this.retrievingUserVillages = retrievingUserVillages;
                    }
                    else
                    {
                        this.processVillageFactionChangesList(returnData.villageUpdateList, returnData.currentVillageChangePos);
                    }
                }
                else if (returnData.villageOwnerFactions != null)
                {
                    this.processVillageFactionList(returnData.villageOwnerFactions, returnData.currentVillageChangePos);
                }
                if (returnData.userVillageList != null)
                {
                    GameEngine.Instance.World.doGetUserVillages(returnData.userVillageList, returnData.userVillageNameList);
                }
                this.setPoints(returnData.currentPoints);
                this.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                this.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
                this.setNumMadeCaptains(returnData.numMadeCaptains);
                if (returnData.cardData != null)
                {
                    GameEngine.Instance.World.UserCardData = returnData.cardData;
                }
            }
        }

        public bool retrieveUserData(int villageID, int userID, ref VillageRolloverInfo villageInfo, ref CachedUserInfo userInfo, bool doServerRetrieve, bool forceExtended)
        {
            try
            {
                if (doServerRetrieve)
                {
                    villageInfo = null;
                }
                villageInfo = null;
                userInfo = null;
                if (villageID >= 0)
                {
                    userID = this.villageList[villageID].userID;
                    if ((userID == -1) && GameEngine.Instance.LocalWorldData.AIWorld)
                    {
                        this.getSpecial(villageID);
                        switch (GameEngine.Instance.World.getSpecial(villageID))
                        {
                            case 7:
                                userID = 1;
                                break;

                            case 9:
                                userID = 2;
                                break;

                            case 11:
                                userID = 3;
                                break;

                            case 13:
                                userID = 4;
                                break;
                        }
                    }
                    villageInfo = this.villageList[villageID].rolloverInfo;
                    if (villageInfo != null)
                    {
                        TimeSpan span = (TimeSpan) (DateTime.Now - villageInfo.lastUpdateTime);
                        if (span.TotalMinutes > 3.0)
                        {
                            villageInfo = null;
                        }
                    }
                }
                bool flag = false;
                if (userID >= 0)
                {
                    userInfo = (CachedUserInfo) this.cachedUserInfo[userID];
                    if (userInfo != null)
                    {
                        TimeSpan span2 = (TimeSpan) (DateTime.Now - userInfo.lastUpdateTime);
                        if (span2.TotalMinutes > 2.0)
                        {
                            flag = true;
                        }
                        if (!flag && (userInfo.villages == null))
                        {
                            flag = true;
                        }
                    }
                }
                if (((villageID < 0) || (villageInfo != null)) && ((userID < 0) || ((userInfo != null) && !flag)))
                {
                    return true;
                }
                if (doServerRetrieve)
                {
                    bool flag2 = false;
                    if (userID >= 0)
                    {
                        if ((userID == this.lastRetieveUserID) && (forceExtended == this.lastForceExtended))
                        {
                            TimeSpan span3 = (TimeSpan) (DateTime.Now - this.lastRetieveUserTime);
                            if ((span3.TotalSeconds < 30.0) && (((villageID < 0) || (villageInfo != null)) || (villageID == this.lastRetieveVillageID)))
                            {
                                flag2 = true;
                            }
                            else
                            {
                                this.lastRetieveUserTime = DateTime.Now;
                            }
                        }
                        else
                        {
                            this.lastRetieveUserID = userID;
                            this.lastRetieveUserTime = DateTime.Now;
                            this.lastForceExtended = forceExtended;
                        }
                    }
                    else if (villageID == this.lastRetieveVillageID)
                    {
                        TimeSpan span4 = (TimeSpan) (DateTime.Now - this.lastRetieveVillageTime);
                        if (span4.TotalSeconds < 30.0)
                        {
                            flag2 = true;
                        }
                        else
                        {
                            this.lastRetieveVillageTime = DateTime.Now;
                        }
                    }
                    else
                    {
                        this.lastRetieveVillageID = villageID;
                        this.lastRetieveVillageTime = DateTime.Now;
                    }
                    if (!flag2)
                    {
                        if (forceExtended)
                        {
                            RemoteServices.Instance.set_RetrieveVillageUserInfo_UserCallBack(new RemoteServices.RetrieveVillageUserInfo_UserCallBack(this.villageUserInfoCallback));
                            RemoteServices.Instance.RetrieveVillageUserInfo(villageID, userID, forceExtended);
                            this.cached_retrieveVillageID = -1;
                            this.cached_retrieveUserID = -1;
                        }
                        else if ((this.cached_retrieveUserID != userID) || (this.cached_retrieveVillageID != villageID))
                        {
                            this.cached_retrieveUserID = userID;
                            this.cached_retrieveVillageID = villageID;
                            this.cached_retrieveVillageUserInfoDate = DateTime.Now;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void retrieveUserVillages(bool force)
        {
            if (!this.retrievingUserVillages || force)
            {
                this.retrievingUserVillages = true;
                RemoteServices.Instance.set_GetUserVillages_UserCallBack(new RemoteServices.GetUserVillages_UserCallBack(this.getUserVillages));
                RemoteServices.Instance.GetUserVillages();
            }
        }

        public void runClientAchievementTests()
        {
            List<AchievementData> achievementData = null;
            List<int> achievementToTest = this.getAchievementsToTest(ref achievementData);
            if ((achievementToTest != null) && (achievementToTest.Count > 0))
            {
                this.testAchievements(achievementToTest, achievementData, false);
            }
        }

        public void saveFactionData()
        {
            if (this.m_downloadedDataSafely)
            {
                string str = GameEngine.getSettingsPath(true);
                try
                {
                    FileInfo info = new FileInfo(str + @"\VillageData" + this.m_globalWorldID.ToString() + ".dat") {
                        IsReadOnly = false
                    };
                }
                catch (Exception exception)
                {
                    UniversalDebugLog.Log("Exception in saveFactionData " + exception.Message);
                }
                try
                {
                    FileStream output = new FileStream(str + @"\VillageData" + this.m_globalWorldID.ToString() + ".dat", FileMode.Create);
                    BinaryWriter writer = new BinaryWriter(output);
                    byte[] buffer = RemoteServices.Instance.WorldGUID.ToByteArray();
                    int num = 10;
                    writer.Write(num);
                    writer.Write(buffer, 0, 0x10);
                    writer.Write(this.storedVillageFactionsPos);
                    for (int i = 0; i < this.villageList.Length; i++)
                    {
                        writer.Write(this.villageList[i].factionID);
                        writer.Write(this.villageList[i].userID);
                        writer.Write(this.villageList[i].connecter);
                        writer.Write(this.villageList[i].special);
                        writer.Write(this.villageList[i].villageTerrain);
                        writer.Write(this.villageList[i].numFlags);
                    }
                    writer.Write(this.storedRegionFactionsPos);
                    writer.Write(this.storedParishFlagsPos);
                    writer.Write(this.storedCountyFlagsPos);
                    writer.Write(this.storedProvinceFlagsPos);
                    writer.Write(this.storedCountryFlagsPos);
                    for (int j = 0; j < this.regionList.Length; j++)
                    {
                        writer.Write(this.regionList[j].factionID);
                        writer.Write(this.regionList[j].userID);
                        writer.Write(this.regionList[j].plague);
                    }
                    writer.Write(this.storedCountyFactionsPos);
                    for (int k = 0; k < this.countyList.Length; k++)
                    {
                        writer.Write(this.countyList[k].factionID);
                        writer.Write(this.countyList[k].userID);
                    }
                    writer.Write(this.storedProvinceFactionsPos);
                    for (int m = 0; m < this.provincesList.Length; m++)
                    {
                        writer.Write(this.provincesList[m].factionID);
                        writer.Write(this.provincesList[m].userID);
                    }
                    writer.Write(this.storedCountryFactionsPos);
                    for (int n = 0; n < this.countryList.Length; n++)
                    {
                        writer.Write(this.countryList[n].factionID);
                        writer.Write(this.countryList[n].userID);
                    }
                    for (int num7 = 0; num7 < this.villageList.Length; num7++)
                    {
                        writer.Write(this.villageList[num7].visible);
                    }
                    writer.Write(this.storedFactionChangesPos);
                    int num8 = 0;
                    IEnumerator enumerator = this.m_factionData.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        FactionData current = (FactionData) enumerator.Current;
                        num8++;
                    }

                    writer.Write(num8);
                    foreach (FactionData data in this.m_factionData)
                    {
                        writer.Write(data.factionID);
                        writer.Write(data.active);
                        writer.Write(data.factionName);
                        writer.Write(data.factionNameAbrv);
                        writer.Write(data.houseID);
                        writer.Write(data.numMembers);
                        writer.Write(data.points);
                        writer.Write(data.flagData);
                        writer.Write(data.openForApplications);
                    }
                    writer.Close();
                    output.Close();
                }
                catch (Exception exception2)
                {
                    MyMessageBox.Show(SK.Text("WorldMapLoader_DataSaveError_Text", "A problem occurred saving 'VillageData.data'") + "\n\n" + exception2.ToString(), SK.Text("WorldMapLoader_DataSaveError_Header", "Data Save Error"));
                }
            }
        }

        public void saveNamesData()
        {
            if (!this.MapEditing)
            {
                string str = GameEngine.getSettingsPath(true);
                try
                {
                    FileInfo info = new FileInfo(str + @"\NameData" + this.m_globalWorldID.ToString() + ".dat") {
                        IsReadOnly = false
                    };
                }
                catch (Exception)
                {
                }
                try
                {
                    FileStream output = new FileStream(str + @"\NameData" + this.m_globalWorldID.ToString() + ".dat", FileMode.Create);
                    BinaryWriter writer = new BinaryWriter(output);
                    byte[] buffer = RemoteServices.Instance.WorldGUID.ToByteArray();
                    writer.Write(buffer, 0, 0x10);
                    writer.Write(this.storedVillageNamePos);
                    int num = 0;
                    for (int i = 0; i < this.villageList.Length; i++)
                    {
                        writer.Write(this.villageList[i].m_villageName);
                        num ^= this.villageList[i].m_villageName.GetHashCode();
                    }
                    for (int j = 0; j < this.regionList.Length; j++)
                    {
                        writer.Write(this.regionList[j].areaName);
                        num ^= this.regionList[j].areaName.GetHashCode();
                    }
                    for (int k = 0; k < this.countyList.Length; k++)
                    {
                        writer.Write(this.countyList[k].areaName);
                        num ^= this.countyList[k].areaName.GetHashCode();
                    }
                    for (int m = 0; m < this.provincesList.Length; m++)
                    {
                        writer.Write(this.provincesList[m].areaName);
                        num ^= this.provincesList[m].areaName.GetHashCode();
                    }
                    for (int n = 0; n < this.countryList.Length; n++)
                    {
                        writer.Write(this.countryList[n].areaName);
                        num ^= this.countryList[n].areaName.GetHashCode();
                    }
                    writer.Write(num);
                    writer.Close();
                    output.Close();
                }
                catch (Exception exception)
                {
                    MyMessageBox.Show(SK.Text("WorldMapLoader_NameSaveError_Text", "A problem occurred saving 'NameData.data'") + "\n\n" + exception.ToString(), SK.Text("WorldMapLoader_DataSaveError_Header", "Data Save Error"));
                }
            }
        }

        public void searchProfileCards(CardTypes.CardDefinition filter, string sort, string namefilter)
        {
            Comparison<int> comparison = null;
            Comparison<int> comparison2 = null;
            Comparison<int> comparison3 = null;
            this.ProfileCardsSearch.Clear();
            List<int> list = new List<int>();
            filter.cardFilter = 0;
            foreach (int num in this.ProfileCards.Keys)
            {
                if (this.filterCard(filter, this.ProfileCards[num]))
                {
                    list.Add(num);
                }
            }
            foreach (int num2 in list)
            {
                if (CardTypes.isCardInNewCategory(this.ProfileCards[num2].id, filter.newCardCategoryFilter) && ((namefilter.Length == 0) || CardTypes.containsName(this.ProfileCards[num2].id, namefilter)))
                {
                    this.ProfileCardsSearch.Add(num2);
                }
            }
            if (this.ProfileCardsSearch.Count > 0)
            {
                if (sort == "rarity")
                {
                    if (comparison == null)
                    {
                        comparison = delegate (int first, int next) {
                            int cardRarity = CardTypes.getCardDefinition(this.ProfileCards[first].id).cardRarity;
                            int num2 = CardTypes.getCardDefinition(this.ProfileCards[next].id).cardRarity;
                            if (cardRarity != num2)
                            {
                                return num2.CompareTo(cardRarity);
                            }
                            return this.ProfileCards[first].id.CompareTo(this.ProfileCards[next].id);
                        };
                    }
                    this.ProfileCardsSearch.Sort(comparison);
                }
                else if (sort == "meta")
                {
                    if (comparison2 == null)
                    {
                        comparison2 = delegate (int first, int next) {
                            int metaScore = CardTypes.getCardDefinition(this.ProfileCards[first].id).metaScore;
                            int num2 = CardTypes.getCardDefinition(this.ProfileCards[next].id).metaScore;
                            if (metaScore != num2)
                            {
                                return num2.CompareTo(metaScore);
                            }
                            return this.ProfileCards[first].id.CompareTo(this.ProfileCards[next].id);
                        };
                    }
                    this.ProfileCardsSearch.Sort(comparison2);
                }
                else
                {
                    if (comparison3 == null)
                    {
                        comparison3 = delegate (int first, int next) {
                            string str = CardTypes.getDescriptionFromCard(this.ProfileCards[first].id);
                            string strB = CardTypes.getDescriptionFromCard(this.ProfileCards[next].id);
                            if (str != strB)
                            {
                                return str.CompareTo(strB);
                            }
                            return this.ProfileCards[first].id.CompareTo(this.ProfileCards[next].id);
                        };
                    }
                    this.ProfileCardsSearch.Sort(comparison3);
                }
            }
            this.lastUserCardSearchCriteria = filter;
            this.lastUserCardSortOrder = sort;
            this.lastUserCardNameFilter = namefilter;
        }

        public void searchProfileCardsRedoLast()
        {
            if (this.lastUserCardSearchCriteria != null)
            {
                this.searchProfileCards(this.lastUserCardSearchCriteria, this.lastUserCardSortOrder, this.lastUserCardNameFilter);
            }
        }

        public void searchProfileCardsRedoLast(string nameFilter)
        {
            if (this.lastUserCardSearchCriteria != null)
            {
                this.searchProfileCards(this.lastUserCardSearchCriteria, this.lastUserCardSortOrder, nameFilter);
            }
        }

        public List<int> searchVillageNames(string searchString)
        {
            List<int> list = new List<int>();
            searchString = searchString.ToLower();
            foreach (VillageData data in this.villageList)
            {
                if ((((data.special == 0) && data.visible) && ((data.userID >= 0) || data.Capital)) && data.m_villageName.ToLower().Contains(searchString))
                {
                    list.Add(data.id);
                }
            }
            return list;
        }

        public void setCurrentZoom(float zoom)
        {
            this.zoomCurrent = (int) (zoom * 1000f);
        }

        public void setExcommunicationTime(int villageID, DateTime excommunicationTime)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                this.villageList[villageID].excommunicationTime = excommunicationTime;
            }
        }

        public void setFactionData(FactionData fd)
        {
            if ((fd != null) && (fd.factionID >= 0))
            {
                this.m_factionData[fd.factionID] = fd;
            }
        }

        public void setFactionMemberData(int factionID, FactionMemberData[] memberData)
        {
            if (factionID == RemoteServices.Instance.UserFactionID)
            {
                this.FactionMembers = memberData;
            }
            else if (this.cachedFactionMemberData[factionID] == null)
            {
                FactionCachedMemberData data = new FactionCachedMemberData {
                    factionID = factionID,
                    memberData = memberData,
                    lastRefreshed = DateTime.Now
                };
                this.cachedFactionMemberData[factionID] = data;
            }
            else
            {
                FactionCachedMemberData data2 = (FactionCachedMemberData) this.cachedFactionMemberData[factionID];
                data2.memberData = memberData;
                data2.lastRefreshed = DateTime.Now;
            }
        }

        public void setFaithPointsData(double faithPointsLevel, double faithPointsRate)
        {
            this.m_userFaithPointsLevel = faithPointsLevel;
            this.m_userFaithPointsRate = faithPointsRate;
            this.m_lastFaithPointsUpdate = DXTimer.GetCurrentMilliseconds();
        }

        public void setGoldData(double goldLevel, double goldRate)
        {
            this.m_userGoldLevel = goldLevel;
            this.m_userGoldIncomeRate = goldRate;
            this.m_lastGoldUpdate = DXTimer.GetCurrentMilliseconds();
        }

        public void setHonourData(double honourLevel, double honourRate)
        {
            this.m_userHonourLevel = honourLevel;
            this.m_userHonourIncomeRate = honourRate;
            this.m_lastHonourUpdate = DXTimer.GetCurrentMilliseconds();
        }

        public void setInterdictTime(int villageID, DateTime interdictionTime)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                this.villageList[villageID].interdictionTime = interdictionTime;
            }
        }

        public void setLastTreasureCastleAttackTime(DateTime lastTime)
        {
            this.m_lastTreasureCastleAttackTime = lastTime;
        }

        public void setMouseWheelZoomOut(float change)
        {
            if (this.zoomCurrent > 0.1f)
            {
                GameEngine.Instance.playInterfaceSound("WorldMap_mousewheel_zoomout");
            }
            int zoomMin = (int) (change * 1000f);
            if (zoomMin < this.zoomMin)
            {
                zoomMin = this.zoomMin;
            }
            if (zoomMin > this.zoomMax)
            {
                zoomMin = this.zoomMax;
            }
            this.zoomCurrent = zoomMin;
            this.worldZoomCallback(((double) this.zoomCurrent) / 1000.0, false);
            this.centreMap(false);
        }

        public void setNewQuestData(NewQuestsData data)
        {
            try
            {
                if ((data.availableQuests != null) || (this.m_newQuestData == null))
                {
                    this.m_newQuestData = data;
                    List<int> availableQuests = new List<int>();
                    availableQuests.AddRange(this.m_newQuestData.availableQuests);
                    availableQuests = this.removeDuplicateQuests(availableQuests);
                    availableQuests.Sort(delegate (int first, int second) {
                        try
                        {
                            int num = NewQuests.questSortOrder[first];
                            int num2 = NewQuests.questSortOrder[second];
                            return num.CompareTo(num2);
                        }
                        catch (Exception)
                        {
                            return first.CompareTo(second);
                        }
                    });
                    this.m_newQuestData.availableQuests = availableQuests.ToArray();
                }
                else
                {
                    this.m_newQuestData.completionState = data.completionState;
                    this.m_newQuestData.data = data.data;
                    this.m_newQuestData.questID = data.questID;
                    this.m_newQuestData.startingData = data.startingData;
                    this.m_newQuestData.startTime = data.startTime;
                    this.m_newQuestData.totalCompleted = data.totalCompleted;
                }
                switch (this.m_newQuestData.questID)
                {
                    case 0x22:
                    case 0x30:
                    case 4:
                    case 0x10:
                    case 0x65:
                    case 0x7a:
                    case 0x40:
                    case 0x54:
                        this.m_newQuestData.data = 0x3e8;
                        break;
                }
                InterfaceMgr.Instance.getMainTabBar().newQuestsCompleted((NewQuestsPanel.isQuestComplete(this.m_newQuestData) || ((this.m_newQuestData.questID < 0) && (this.m_newQuestData.totalCompleted == 0))) && (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_QUESTS));
            }
            catch
            {
                UniversalDebugLog.Log("setNewQuestData had an error");
            }
        }

        public void setNumMadeCaptains(int numCaptains)
        {
            this.m_numMadeCaptains = numCaptains;
        }

        public void setParishName(int villageID, string villageName)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                this.villageList[villageID].villageName = villageName;
                int index = this.getParishFromVillageID(villageID);
                if ((index >= 0) && (index < this.regionList.Length))
                {
                    this.regionList[index].areaName = villageName;
                }
                this.sortUserVillages();
            }
        }

        public void setPeaceTime(int villageID, DateTime peaceTime)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                this.villageList[villageID].peaceTime = peaceTime;
            }
        }

        public void setPlaybackData(List<WorldHouseHistoryItem> items, DateTime startDate)
        {
            if ((items != null) && (items.Count != 0))
            {
                this.playbackItems = items;
                int countryID = 0;
                int provinceID = 0;
                TimeSpan span = (TimeSpan) (VillageMap.getCurrentServerTime() - startDate);
                int num3 = ((int) span.TotalDays) + 1;
                int num4 = 0x989680;
                foreach (WorldHouseHistoryItem item in items)
                {
                    if (item.countryID > countryID)
                    {
                        countryID = item.countryID;
                    }
                    if (item.provinceID > provinceID)
                    {
                        provinceID = item.provinceID;
                    }
                    TimeSpan span2 = (TimeSpan) (item.date - startDate);
                    int totalDays = (int) span2.TotalDays;
                    if (totalDays < num4)
                    {
                        num4 = totalDays;
                    }
                }
                if (num4 < 20)
                {
                    num4 = 0;
                }
                else
                {
                    num4 -= 20;
                }
                this.playbackTotalDays = num3 - num4;
                if ((this.playbackTotalDays <= 0) || (this.playbackTotalDays > 0x186a0))
                {
                    this.playbackTotalDays = 0;
                    this.playbackItems = null;
                }
                else
                {
                    this.playbackCountriesData = new int[this.playbackTotalDays, countryID + 1];
                    this.playbackProvincesData = new int[this.playbackTotalDays, provinceID + 1];
                    this.playbackBasedDay = num4;
                    foreach (WorldHouseHistoryItem item2 in items)
                    {
                        TimeSpan span3 = (TimeSpan) (item2.date - startDate);
                        int num6 = ((int) span3.TotalDays) - num4;
                        if (item2.countryID >= 0)
                        {
                            this.playbackCountriesData[num6, item2.countryID] = item2.houseID;
                        }
                        if (item2.provinceID >= 0)
                        {
                            this.playbackProvincesData[num6, item2.provinceID] = item2.houseID;
                        }
                    }
                    this.playbackMaxCountries = countryID + 1;
                    this.playbackMaxProvinces = provinceID + 1;
                }
            }
        }

        public void setPoints(int points)
        {
            this.m_userPoints = points;
        }

        public void setRanking(int rank, int rankSubLevel)
        {
            this.m_userRank = rank;
            this.m_userRankSubLevel = rankSubLevel;
            InterfaceMgr.Instance.setRank(rank);
        }

        public int[] setReadIDs(int parishID, long[] readIDs)
        {
            int[] numArray = new int[6];
            if (this.m_parishChatLog[parishID] != null)
            {
                ParishChatData data = (ParishChatData) this.m_parishChatLog[parishID];
                data.setReadIDs(readIDs);
                for (int i = 0; i < 6; i++)
                {
                    numArray[i] = 0;
                    bool flag = false;
                    long num2 = data.getReadID(i);
                    foreach (Chat_TextEntry entry in data.m_pages[i])
                    {
                        if (entry.textID > num2)
                        {
                            flag = true;
                        }
                        if (flag)
                        {
                            numArray[i]++;
                        }
                    }
                }
            }
            return numArray;
        }

        public void setResearchData(ResearchData data)
        {
            if (data != null)
            {
                this.userResearchData = data;
            }
            this.requestSent = false;
        }

        public void setScreenSize(int screenWidth, int screenHeight)
        {
            this.m_screenWidth = screenWidth;
            this.m_screenHeight = screenHeight;
        }

        public void setTickets(int level, int number)
        {
            switch (level)
            {
                case -1:
                    this.m_numQuestTickets = number;
                    return;

                case 0:
                    this.m_treasure1Tickets = number;
                    return;

                case 1:
                    this.m_treasure2Tickets = number;
                    return;

                case 2:
                    this.m_treasure3Tickets = number;
                    return;

                case 3:
                    this.m_treasure4Tickets = number;
                    return;

                case 4:
                    this.m_treasure5Tickets = number;
                    return;
            }
        }

        public void setTutorialInfo(QuestsAndTutorialInfo tutorialInfo)
        {
            if (tutorialInfo != null)
            {
                int tutorialStage = this.m_tutorialInfo.tutorialStage;
                if (tutorialInfo.questsActive == null)
                {
                    this.m_tutorialInfo.tutorialActive = tutorialInfo.tutorialActive;
                    this.m_tutorialInfo.tutorialCompleted = tutorialInfo.tutorialCompleted;
                    this.m_tutorialInfo.tutorialStage = tutorialInfo.tutorialStage;
                    this.m_tutorialInfo.resumeStage = tutorialInfo.resumeStage;
                }
                else
                {
                    this.m_tutorialInfo = tutorialInfo;
                }
                if (this.m_tutorialInfo.tutorialActive && (tutorialStage != this.m_tutorialInfo.tutorialStage))
                {
                    this.newTutorialAvailable = true;
                }
            }
        }

        public void setUserRelationship(int userID, int relationship, string username)
        {
            foreach (UserRelationship relationship2 in this.userRelations)
            {
                if (relationship2.userID == userID)
                {
                    if (relationship == 0)
                    {
                        this.userRelations.Remove(relationship2);
                    }
                    else
                    {
                        relationship2.friendly = relationship > 0;
                    }
                    return;
                }
            }
            if (relationship != 0)
            {
                UserRelationship item = new UserRelationship {
                    userID = userID,
                    userName = username,
                    friendly = relationship > 0
                };
                this.userRelations.Add(item);
            }
        }

        public void setVillageName(int villageID, string villageName)
        {
            if ((villageID >= 0) && (villageID < this.villageList.Length))
            {
                this.villageList[villageID].villageName = villageName;
                this.sortUserVillages();
            }
        }

        public void setWorldStartDate(DateTime startDate)
        {
            this.m_worldStartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0, 0);
        }

        public void setZooming(double targetZoom, double xPos, double yPos)
        {
            this.setZooming(targetZoom, xPos, yPos, (double) 16.0);
        }

        public void setZooming(double targetZoom, double xPos, double yPos, bool initialStage)
        {
            if (initialStage)
            {
                double num = (targetZoom - (27.0 - this.m_worldZoom)) / 16.0;
                if (num == 0.0)
                {
                    if ((xPos != this.m_screenCentreX) || (yPos != this.m_screenCentreY))
                    {
                        GameEngine.Instance.playInterfaceSound("WorldMap_map_moving_sideways");
                    }
                }
                else if (num < 0.0)
                {
                    this.m_multiStageSoundMode = 1;
                    GameEngine.Instance.playInterfaceSound("WorldMap_map_zooming_out");
                }
                else
                {
                    GameEngine.Instance.playInterfaceSound("WorldMap_map_zooming_in");
                }
            }
            else if (this.m_multiStageSoundMode == 1)
            {
                double num2 = (targetZoom - (27.0 - this.m_worldZoom)) / 16.0;
                if (num2 > 0.0)
                {
                    GameEngine.Instance.playInterfaceSound("WorldMap_map_zooming_in");
                    this.m_multiStageSoundMode = 2;
                }
            }
            this.setZooming(targetZoom, xPos, yPos, (double) 16.0);
        }

        public void setZooming(double targetZoom, double xPos, double yPos, double zoomTime)
        {
            bool flag = false;
            if ((this.m_zoomStage >= 0) && (this.m_zoomStage < 6))
            {
                flag = true;
            }
            this.m_zoomStage = -1;
            this.m_zooming = true;
            this.m_targetZoom = targetZoom;
            this.m_zoomDiff = (this.m_targetZoom - (27.0 - this.m_worldZoom)) / zoomTime;
            if ((targetZoom == 27.0) && (this.m_worldZoom < 0.001))
            {
                this.m_zoomDiff = 0.0;
            }
            if (!flag)
            {
                this.m_zoomXPosTarget = xPos;
                this.m_zoomYPosTarget = yPos;
                double screenCentreX = this.m_screenCentreX;
                double screenCentreY = this.m_screenCentreY;
                this.m_screenCentreX = xPos;
                this.m_screenCentreY = yPos;
                this.centreMap(true);
                xPos = this.m_screenCentreX;
                yPos = this.m_screenCentreY;
                this.m_screenCentreX = screenCentreX;
                this.m_screenCentreY = screenCentreY;
            }
            if ((this.m_zoomDiff != 0.0) && (Math.Abs(this.m_zoomDiff) < 0.07))
            {
                if (this.m_zoomDiff < 0.0)
                {
                    this.m_zoomDiff = -0.07;
                }
                else
                {
                    this.m_zoomDiff = 0.07;
                }
            }
            this.m_zoomXPosTarget = xPos;
            this.m_zoomYPosTarget = yPos;
            this.m_zoomXPosDiff = (xPos - this.m_screenCentreX) / zoomTime;
            this.m_zoomYPosDiff = (yPos - this.m_screenCentreY) / zoomTime;
        }

        public void setZoomingPaced(double targetZoom, double xPos, double yPos)
        {
            if (targetZoom > 27.0)
            {
                targetZoom = 27.0;
            }
            double zoomTime = 16.0;
            double num2 = Math.Abs((double) (xPos - this.m_screenCentreX));
            double num3 = Math.Abs((double) (yPos - this.m_screenCentreY));
            if (num3 > num2)
            {
                num2 = num3;
            }
            if (num2 > 300.0)
            {
                zoomTime *= num2 / 300.0;
            }
            this.setZooming(targetZoom, xPos, yPos, zoomTime);
        }

        public void shieldDownloaded()
        {
            if (this.playerShieldCallback != null)
            {
                this.playerShieldCallback();
            }
        }

        public Image shieldOverlay(Image sourceImage, int width, int height, int bmapWidth, int bmapHeight)
        {
            int num = width;
            int num2 = height;
            int x = 0;
            int y = 0;
            Image image = null;
            if ((width == 140) && (height == 0x9c))
            {
                num = 0x9e;
                num2 = 0xaf;
                x = 8;
                y = 9;
                image = (Image) GFXLibrary.shieldOverlay_144x160;
            }
            else if ((width == 0x45) && (height == 0x4d))
            {
                num = 0x51;
                num2 = 0x58;
                x = 4;
                y = 5;
                image = (Image) GFXLibrary.shieldOverlay_70x78;
            }
            else if ((width == 0x2f) && (height == 0x36))
            {
                num = 0x37;
                num2 = 0x3d;
                x = 3;
                y = 3;
                image = (Image) GFXLibrary.shieldOverlay_56x64;
            }
            else if ((width == 0x20) && (height == 0x24))
            {
                num = 0x25;
                num2 = 0x29;
                x = 2;
                y = 2;
                image = (Image) GFXLibrary.shieldOverlay_32x36;
            }
            else if ((width == 0x19) && (height == 0x1c))
            {
                num = 30;
                num2 = 0x20;
                x = 2;
                y = 2;
                image = (Image) GFXLibrary.shieldOverlay_25x28;
            }
            if (image == null)
            {
                return sourceImage;
            }
            if (width != bmapWidth)
            {
                num = bmapWidth;
            }
            if (height != bmapHeight)
            {
                num2 = bmapHeight;
            }
            Bitmap bitmap = new Bitmap(num, num2);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.PageUnit = GraphicsUnit.Pixel;
            if (sourceImage != null)
            {
                graphics.DrawImage(sourceImage, x, y, new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), GraphicsUnit.Pixel);
            }
            graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            graphics.Dispose();
            return bitmap;
        }

        public void showShieldUser(int userID)
        {
            this.m_userInfoShieldRolloverUserID = userID;
        }

        public void sortUserVillages()
        {
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    if (this.villageList[data.villageID].Capital)
                    {
                        data.capital = true;
                        if (this.villageList[data.villageID].regionCapital)
                        {
                            data.parishCapital = true;
                        }
                        else if (this.villageList[data.villageID].countyCapital)
                        {
                            data.countyCapital = true;
                        }
                        else if (this.villageList[data.villageID].provinceCapital)
                        {
                            data.provinceCapital = true;
                        }
                        else if (this.villageList[data.villageID].countryCapital)
                        {
                            data.countryCapital = true;
                        }
                    }
                }
                this.m_userVillages.Sort(this.villageNameComparer);
                int num = 0;
                foreach (UserVillageData data2 in this.m_userVillages)
                {
                    this.villageList[data2.villageID].userVillageID = num;
                    num++;
                }
            }
        }

        public void specialVillageInfoCallback(SpecialVillageInfo_ReturnType returnData)
        {
            this.lastSpecialRequestSent = -1;
            if (returnData.Success && (returnData.villageID >= 0))
            {
                SpecialVillageCache cache = new SpecialVillageCache {
                    resourceType = returnData.resourceType,
                    resourceLevel = returnData.resourceLevel
                };
                this.specialVillageCache[returnData.villageID] = cache;
            }
        }

        public void startGameZoom(int villageID)
        {
            if (villageID >= 0)
            {
                VillageData data = this.villageList[villageID];
                this.setZooming(27.0, (double) data.x, (double) data.y);
                while (this.Zooming)
                {
                    this.updateZooming();
                    if (this.ZoomChange != 0.0)
                    {
                        this.changeZoom((float) this.ZoomChange);
                        this.centreMap(false);
                    }
                }
            }
            else
            {
                this.setZooming(0.0, 0.0, 0.0);
                while (this.Zooming)
                {
                    this.updateZooming();
                    if (this.ZoomChange != 0.0)
                    {
                        this.changeZoom((float) this.ZoomChange);
                        this.centreMap(false);
                    }
                }
            }
        }

        public void startMultiStageZoom(double targetZoom, double xPos, double yPos)
        {
            if (targetZoom > 27.0)
            {
                targetZoom = 27.0;
            }
            this.m_stagedTargetZoom = targetZoom;
            this.m_stagedTargetX = xPos;
            this.m_stagedTargetY = yPos;
            double num = 27.0 - this.m_worldZoom;
            if (num > 9.51)
            {
                this.m_zoomStage = 0;
            }
            else if (num > 3.51)
            {
                this.m_zoomStage = 1;
            }
            else if (num > (this.m_zoomCap + 0.5))
            {
                this.m_zoomStage = 2;
            }
            else
            {
                this.m_zoomStage = 3;
            }
            this.m_multiStageSoundMode = 0;
            this.nextStageZoom(true);
        }

        public void stopPlayback()
        {
            this.playingCountries = false;
            this.playingProvinces = false;
        }

        public void stopZoom()
        {
            this.m_zooming = false;
            this.m_zoomStage = -1;
        }

        public void testAchievements(List<int> achievementToTest, List<AchievementData> achievementData, bool onLoading)
        {
            if ((achievementToTest != null) && (achievementToTest.Count > 0))
            {
                if (this.inTestAchievements)
                {
                    int num = 30;
                    if (!onLoading)
                    {
                        num = 60;
                    }
                    TimeSpan span = (TimeSpan) (DateTime.Now - this.lastTestAchievements);
                    if (span.TotalSeconds > num)
                    {
                        this.inTestAchievements = false;
                    }
                }
                if (!this.inTestAchievements)
                {
                    this.inTestAchievements = true;
                    this.lastTestAchievements = DateTime.Now;
                    RemoteServices.Instance.set_TestAchievements_UserCallBack(new RemoteServices.TestAchievements_UserCallBack(this.testAchievementsCallback));
                    RemoteServices.Instance.TestAchievements(achievementToTest, achievementData);
                }
            }
        }

        public void testAchievementsCallback(TestAchievements_ReturnType returnData)
        {
            this.inTestAchievements = false;
            if (returnData.Success)
            {
                this.loadingErrored = false;
                if (returnData.achievements != null)
                {
                    InterfaceMgr.Instance.processAchievements(returnData.achievements);
                }
            }
            else
            {
                this.loadingErrored = true;
            }
        }

        public bool testGloryPointsUpdate()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - this.lastHouseGloryPointsUpdate);
            if (span.TotalHours > 2.0)
            {
                this.lastHouseGloryPointsUpdate = DateTime.Now;
                return true;
            }
            return false;
        }

        public void TutorialCommandCallback(TutorialCommand_ReturnType returnData)
        {
            this.inTutorialAdvance = false;
            if (returnData.Success)
            {
                this.setTutorialInfo(returnData.m_tutorialInfo);
                if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_QUESTS)
                {
                    InterfaceMgr.Instance.reloadQuestPanel();
                }
                if ((returnData.m_tutorialInfo != null) && (returnData.m_tutorialInfo.tutorialStage == -1))
                {
                    this.doSelectTutorialArmy = true;
                    GameEngine.Instance.World.getArmiesIfNewAttacks();
                }
            }
        }

        public bool TutorialIsAdvancing()
        {
            return this.inTutorialAdvance;
        }

        public void tutorialPopupShown()
        {
            this.newTutorialAvailable = false;
        }

        public void TutorialQuestCompleted(int quest)
        {
            int num = Tutorials.getQuestsTutorialStage(quest);
            if ((num >= 0) && (num == this.getTutorialStage()))
            {
                this.forceTutorialToBeShown();
            }
        }

        public void Update()
        {
            this.updateZooming();
        }

        public void updateAIInvasions()
        {
            if (GameEngine.Instance.LocalWorldData.AIWorld)
            {
                foreach (int num in AIWorldSettings.invasionVillages)
                {
                    this.invasionMarkerState[num] = 0;
                }
                foreach (LocalArmyData data in this.armyArray)
                {
                    if (data.attackType == 0x11)
                    {
                        this.invasionMarkerState[data.homeVillageID] = 2;
                    }
                }
                if (this.invasionInfo != null)
                {
                    DateTime time = VillageMap.getCurrentServerTime().AddDays(10.0);
                    foreach (AIWorldInvasionData data2 in this.invasionInfo)
                    {
                        if (((data2.date < time) && (this.invasionMarkerState[data2.invasionVillageID] != null)) && (((int) this.invasionMarkerState[data2.invasionVillageID]) != 2))
                        {
                            this.invasionMarkerState[data2.invasionVillageID] = 1;
                        }
                    }
                }
            }
        }

        public void updateArmies()
        {
            List<long> list = new List<long>();
            foreach (LocalArmyData data in this.armyArray)
            {
                data.updatePosition();
                if (data.dead)
                {
                    list.Add(data.armyID);
                }
            }
            foreach (long num in list)
            {
                this.armyArray.RemoveAt(num);
            }
            List<long> list2 = new List<long>();
            foreach (LocalArmyData data2 in this.reinforcementArray)
            {
                data2.updatePosition();
                if (data2.dead)
                {
                    list2.Add(data2.armyID);
                }
            }
            foreach (long num2 in list2)
            {
                this.reinforcementArray.RemoveAt(num2);
            }
        }

        public void updateArmyRetrievalData()
        {
            if (this.requestedReturnedArmyIDs.Count > 0)
            {
                DateTime now = DateTime.Now;
                List<ArmyRetrieveData> list = new List<ArmyRetrieveData>();
                foreach (ArmyRetrieveData data in this.requestedReturnedArmyIDs)
                {
                    if (data.expiryTime < now)
                    {
                        if (this.isArmyReallyReturning(data.armyID))
                        {
                            list.Add(data);
                        }
                        else
                        {
                            RemoteServices.Instance.RetrieveAttackResult(data.armyID, GameEngine.Instance.World.StoredVillageFactionPos);
                            this.requestedReturnedArmyIDs.Remove(data);
                            break;
                        }
                    }
                }
                foreach (ArmyRetrieveData data2 in list)
                {
                    this.requestedReturnedArmyIDs.Remove(data2);
                }
            }
        }

        public void updateCountryFactions(AreaUpdateListItem[] countryUpdateList, int[,] countryFactions, long currentCountryChangePos)
        {
            if (countryUpdateList != null)
            {
                foreach (AreaUpdateListItem item in countryUpdateList)
                {
                    if ((item != null) && (item.areaID < this.countryList.Length))
                    {
                        this.countryList[item.areaID].factionID = item.newFactionID;
                    }
                }
            }
            else if (countryFactions != null)
            {
                int index = 0;
                for (int i = 0; i < (countryFactions.Length / 2); i++)
                {
                    if (index < this.countryList.Length)
                    {
                        this.countryList[index].factionID = countryFactions[i, 0];
                        this.countryList[index++].userID = countryFactions[i, 1];
                    }
                }
            }
            this.storedCountryFactionsPos = currentCountryChangePos;
        }

        public void updateCountryFlags(List<CapitalFlagChangeInfo> countryFlagChanges, long countryFlagChangePos)
        {
            this.storedCountryFlagsPos = countryFlagChangePos;
            foreach (CapitalFlagChangeInfo info in countryFlagChanges)
            {
                if (info.areaID >= 0)
                {
                    int areaID = info.areaID;
                    if (areaID < this.countryList.Length)
                    {
                        int capitalVillage = this.countryList[areaID].capitalVillage;
                        if (capitalVillage >= 0)
                        {
                            this.villageList[capitalVillage].numFlags = info.numFlags;
                        }
                    }
                }
            }
        }

        public void updateCountyFactions(AreaUpdateListItem[] countyUpdateList, int[,] countyFactions, long currentCountyChangePos)
        {
            if (countyUpdateList != null)
            {
                foreach (AreaUpdateListItem item in countyUpdateList)
                {
                    if ((item != null) && (item.areaID < this.countyList.Length))
                    {
                        this.countyList[item.areaID].factionID = item.newFactionID;
                    }
                }
            }
            else if (countyFactions != null)
            {
                int index = 0;
                for (int i = 0; i < (countyFactions.Length / 2); i++)
                {
                    if (index < this.countyList.Length)
                    {
                        this.countyList[index].factionID = countyFactions[i, 0];
                        this.countyList[index++].userID = countyFactions[i, 1];
                    }
                }
            }
            this.storedCountyFactionsPos = currentCountyChangePos;
        }

        public void updateCountyFlags(List<CapitalFlagChangeInfo> countyFlagChanges, long countyFlagChangePos)
        {
            this.storedCountyFlagsPos = countyFlagChangePos;
            foreach (CapitalFlagChangeInfo info in countyFlagChanges)
            {
                if (info.areaID >= 0)
                {
                    int areaID = info.areaID;
                    if (areaID < this.countyList.Length)
                    {
                        int capitalVillage = this.countyList[areaID].capitalVillage;
                        if (capitalVillage >= 0)
                        {
                            this.villageList[capitalVillage].numFlags = info.numFlags;
                        }
                    }
                }
            }
        }

        public void updateCurrentCardsCallback(UpdateCurrentCards_ReturnType returnData)
        {
            if (returnData.Success && (returnData.m_cardData != null))
            {
                GameEngine.Instance.World.UserCardData = returnData.m_cardData;
                GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
                GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
            }
            if (InterfaceMgr.Instance.getCardWindow() != null)
            {
                CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.getCardWindow());
            }
        }

        public void updateExistingArmies(long[] existingArmiesX)
        {
            List<long> list = new List<long>();
            foreach (LocalArmyData data in this.armyArray)
            {
                list.Add(data.armyID);
            }
            SparseArray array = new SparseArray();
            foreach (long num in existingArmiesX)
            {
                array[num] = num;
            }
            foreach (long num2 in list)
            {
                if (array[num2] == null)
                {
                    LocalArmyData data2 = (LocalArmyData) this.armyArray[num2];
                    if (((data2 != null) && (RemoteServices.Instance.UserID == data2.userID)) && (data2.lootType >= 0))
                    {
                        data2.localEndTime = data2.localStartTime + 1.0;
                        data2.updatePosition();
                    }
                    this.armyArray[num2] = null;
                }
            }
        }

        public void updateLastAttackerInfo()
        {
            if (!this.inUpdateLastAttackerInfo)
            {
                this.inUpdateLastAttackerInfo = true;
                RemoteServices.Instance.set_GetLastAttacker_UserCallBack(new RemoteServices.GetLastAttacker_UserCallBack(this.getLastAttackerCallback));
                RemoteServices.Instance.GetLastAttacker();
            }
        }

        public void updateLocalVillagesFromFactions()
        {
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    if (this.villageList[data.villageID].factionID != RemoteServices.Instance.UserFactionID)
                    {
                        this.villageList[data.villageID].userVillageID = -1;
                    }
                }
                this.updateUserRelatedVillages();
            }
        }

        public void updateParishFlags(List<CapitalFlagChangeInfo> parishFlagChanges, long parishFlagChangePos)
        {
            this.storedParishFlagsPos = parishFlagChangePos;
            foreach (CapitalFlagChangeInfo info in parishFlagChanges)
            {
                if (info.areaID >= 0)
                {
                    int areaID = info.areaID;
                    if (areaID < this.regionList.Length)
                    {
                        int capitalVillage = this.regionList[areaID].capitalVillage;
                        if (capitalVillage >= 0)
                        {
                            this.villageList[capitalVillage].numFlags = info.numFlags;
                        }
                    }
                }
            }
        }

        public void updatePeople()
        {
            List<long> list = new List<long>();
            foreach (LocalPerson person in this.personArray)
            {
                person.updatePosition();
                if (person.dying)
                {
                    list.Add(person.personID);
                }
            }
            foreach (long num in list)
            {
                this.personArray.RemoveAt(num);
            }
        }

        public void updatePlaybackDay()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - this.playbackStartTime);
            int num2 = (int) (span.TotalMilliseconds / 500.0);
            if (num2 >= (this.playbackTotalDays - 1))
            {
                this.playbackDay = this.playbackTotalDays - 2;
            }
            else
            {
                this.playbackDay = num2;
            }
        }

        public void updateProvinceFactions(AreaUpdateListItem[] provinceUpdateList, int[,] provinceFactions, long currentProvinceChangePos)
        {
            if (provinceUpdateList != null)
            {
                foreach (AreaUpdateListItem item in provinceUpdateList)
                {
                    if ((item != null) && (item.areaID < this.provincesList.Length))
                    {
                        this.provincesList[item.areaID].factionID = item.newFactionID;
                    }
                }
            }
            else if (provinceFactions != null)
            {
                int index = 0;
                for (int i = 0; i < (provinceFactions.Length / 2); i++)
                {
                    if (index < this.provincesList.Length)
                    {
                        this.provincesList[index].factionID = provinceFactions[i, 0];
                        this.provincesList[index++].userID = provinceFactions[i, 1];
                    }
                }
            }
            this.storedProvinceFactionsPos = currentProvinceChangePos;
        }

        public void updateProvinceFlags(List<CapitalFlagChangeInfo> provinceFlagChanges, long provinceFlagChangePos)
        {
            this.storedProvinceFlagsPos = provinceFlagChangePos;
            foreach (CapitalFlagChangeInfo info in provinceFlagChanges)
            {
                if (info.areaID >= 0)
                {
                    int areaID = info.areaID;
                    if (areaID < this.provincesList.Length)
                    {
                        int capitalVillage = this.provincesList[areaID].capitalVillage;
                        if (capitalVillage >= 0)
                        {
                            this.villageList[capitalVillage].numFlags = info.numFlags;
                        }
                    }
                }
            }
        }

        public void updateRegionFactions(AreaUpdateListItem[] regionUpdateList, int[,] regionFactions, long currentRegionChangePos)
        {
            if (regionUpdateList != null)
            {
                foreach (AreaUpdateListItem item in regionUpdateList)
                {
                    if ((item != null) && (item.areaID < this.regionList.Length))
                    {
                        this.regionList[item.areaID].factionID = item.newFactionID;
                        this.regionList[item.areaID].plague = item.special;
                    }
                }
            }
            else if (regionFactions != null)
            {
                int index = 0;
                for (int i = 0; i < (regionFactions.Length / 3); i++)
                {
                    if (index < this.regionList.Length)
                    {
                        this.regionList[index].factionID = regionFactions[i, 0];
                        this.regionList[index].userID = regionFactions[i, 1];
                        this.regionList[index++].plague = regionFactions[i, 2];
                    }
                }
            }
            this.storedRegionFactionsPos = currentRegionChangePos;
        }

        public void updateResearch(bool force)
        {
            if (force)
            {
                this.requestSent = true;
                RemoteServices.Instance.set_GetResearchData_UserCallBack(new RemoteServices.GetResearchData_UserCallBack(this.getResearchDataCallback));
                RemoteServices.Instance.GetResearchData();
            }
            else if (((this.userResearchData != null) && (this.userResearchData.researchingType >= 0)) && (!this.requestSent && (VillageMap.getCurrentServerTime() > this.userResearchData.research_completionTime.AddSeconds(5.0))))
            {
                DateTime now = DateTime.Now;
                if (this.m_lastResearchCompleteTimeMatch == this.userResearchData.research_completionTime)
                {
                    int num = 40 * this.m_researchLagCount;
                    if (num < 40)
                    {
                        num = 40;
                    }
                    else if (num > 300)
                    {
                        num = 300;
                    }
                    TimeSpan span = (TimeSpan) (now - this.m_lastResearchCompleteRequestTime);
                    if (span.TotalSeconds < num)
                    {
                        return;
                    }
                    this.m_researchLagCount++;
                }
                else
                {
                    this.m_researchLagCount = 0;
                }
                if ((this.userResearchData.researchingType == 0x3b) && (this.getTutorialStage() == 5))
                {
                    GameEngine.Instance.World.TutorialQuestCompleted(4);
                    Thread.Sleep(200);
                }
                this.m_lastResearchCompleteRequestTime = now;
                this.m_lastResearchCompleteTimeMatch = this.userResearchData.research_completionTime;
                this.requestSent = true;
                RemoteServices.Instance.set_GetResearchData_UserCallBack(new RemoteServices.GetResearchData_UserCallBack(this.getResearchDataCallback));
                RemoteServices.Instance.GetResearchData();
            }
        }

        public void updateSeasonalGFX()
        {
            this.overlaySprite.TextureID = GFXLibrary.Instance.EffectLayerTexID;
            this.worldTreeSprite.TextureID = GFXLibrary.Instance.MapElementsTexID;
            this.villageSprite.TextureID = GFXLibrary.Instance.WorldMapTilesTexID;
            this.worldTileSprite.TextureID = GFXLibrary.Instance.WorldMapTilesTexID;
        }

        public void updateTraders()
        {
            List<LocalTrader> list = new List<LocalTrader>();
            foreach (LocalTrader trader in this.traderArray)
            {
                trader.updatePosition();
                if (trader.dying)
                {
                    list.Add(trader);
                }
            }
            foreach (LocalTrader trader2 in list)
            {
                this.traderArray[trader2.trader.traderID] = null;
            }
        }

        public void updateUserCapitals(int[] userCapitals)
        {
            foreach (int num in userCapitals)
            {
                if (!this.isUserVillage(num))
                {
                    this.addUserVillage(num);
                }
            }
            if (this.m_userVillages != null)
            {
                bool flag = false;
                foreach (UserVillageData data in this.m_userVillages)
                {
                    if (!data.capital)
                    {
                        continue;
                    }
                    bool flag2 = false;
                    for (int i = 0; i < userCapitals.Length; i++)
                    {
                        if (userCapitals[i] == data.villageID)
                        {
                            flag2 = true;
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    this.retrieveUserVillages(false);
                }
            }
            this.sortUserVillages();
            this.updateUserRelatedVillages();
        }

        public void updateUserRelatedVillages()
        {
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userRelatedVillages)
                {
                    this.villageList[data.villageID].userRelatedVillage = false;
                }
                this.m_userRelatedVillages.Clear();
                List<int> list = new List<int>();
                foreach (UserVillageData data2 in this.m_userVillages)
                {
                    if (!this.villageList[data2.villageID].Capital)
                    {
                        int regionID = this.villageList[data2.villageID].regionID;
                        int capitalVillage = this.regionList[regionID].capitalVillage;
                        if (!list.Contains(capitalVillage) && !this.isUserVillage(capitalVillage))
                        {
                            list.Add(capitalVillage);
                            UserVillageData item = new UserVillageData {
                                villageID = capitalVillage,
                                capital = true,
                                parishCapital = true
                            };
                            this.m_userRelatedVillages.Add(item);
                            this.villageList[capitalVillage].userRelatedVillage = true;
                        }
                    }
                }
                foreach (UserVillageData data4 in this.m_userVillages)
                {
                    if (!this.villageList[data4.villageID].Capital)
                    {
                        int countyID = this.villageList[data4.villageID].countyID;
                        int num4 = this.countyList[countyID].capitalVillage;
                        if (!list.Contains(num4) && !this.isUserVillage(num4))
                        {
                            list.Add(num4);
                            UserVillageData data5 = new UserVillageData {
                                villageID = num4,
                                capital = true,
                                countyCapital = true
                            };
                            this.m_userRelatedVillages.Add(data5);
                            this.villageList[num4].userRelatedVillage = true;
                        }
                    }
                }
                foreach (UserVillageData data6 in this.m_userVillages)
                {
                    if (!this.villageList[data6.villageID].Capital)
                    {
                        int index = this.villageList[data6.villageID].countyID;
                        int parentID = this.countyList[index].parentID;
                        int num7 = this.provincesList[parentID].capitalVillage;
                        if (!list.Contains(num7) && !this.isUserVillage(num7))
                        {
                            list.Add(num7);
                            UserVillageData data7 = new UserVillageData {
                                villageID = num7,
                                capital = true,
                                provinceCapital = true
                            };
                            this.m_userRelatedVillages.Add(data7);
                            this.villageList[num7].userRelatedVillage = true;
                        }
                    }
                }
                foreach (UserVillageData data8 in this.m_userVillages)
                {
                    if (!this.villageList[data8.villageID].Capital)
                    {
                        int num8 = this.villageList[data8.villageID].countyID;
                        int num9 = this.countyList[num8].parentID;
                        int num10 = this.provincesList[num9].parentID;
                        int num11 = this.countryList[num10].capitalVillage;
                        if (!list.Contains(num11) && !this.isUserVillage(num11))
                        {
                            list.Add(num11);
                            UserVillageData data9 = new UserVillageData {
                                villageID = num11,
                                capital = true,
                                countryCapital = true
                            };
                            this.m_userRelatedVillages.Add(data9);
                            this.villageList[num11].userRelatedVillage = true;
                        }
                    }
                }
            }
        }

        public void updateUserVassals()
        {
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    data.vassals.Clear();
                }
                foreach (VillageData data2 in this.villageList)
                {
                    if (data2.connecter >= 0)
                    {
                        foreach (UserVillageData data3 in this.m_userVillages)
                        {
                            if (data3.villageID == data2.connecter)
                            {
                                data3.vassals.Add(data2.id);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void updateWorldMapOwnership()
        {
            this.downloadingCounter = 0;
            this.downloadComplete = false;
            this.m_WorkerThread = new Thread(new ThreadStart(this.updateWorldMapOwnershipX));
            this.m_WorkerThread.Name = "Downloading";
            this.m_WorkerThread.Start();
        }

        public void updateWorldMapOwnershipX()
        {
            RemoteServices.Instance.set_FullTick_UserCallBack(new RemoteServices.FullTick_UserCallBack(this.fullTickCallBack));
            this.requestedReturnedArmyIDs.Clear();
            GameEngine.Instance.setPendingSessionExpiredStat(-1);
            if (!this.m_namesLoaded)
            {
                this.storedVillageNamePos = -1L;
            }
            int num = 0;
            do
            {
                this.loadingErrored = false;
                RemoteServices.Instance.set_GetVillageNames_UserCallBack(new RemoteServices.GetVillageNames_UserCallBack(this.villageNamesCallBack));
                RemoteServices.Instance.GetVillageNames(this.storedVillageNamePos);
                this.downloadWait();
            }
            while ((this.loadingErrored || !RemoteServices.Instance.GetVillageNames_ValidDownload) && (num++ < 3));
            if (this.loadingErrored || !RemoteServices.Instance.GetVillageNames_ValidDownload)
            {
                GameEngine.Instance.setPendingSessionExpiredStat(2);
            }
            else
            {
                if (!this.m_dataLoaded)
                {
                    this.storedRegionFactionsPos = -1L;
                    this.storedCountyFactionsPos = -1L;
                    this.storedProvinceFactionsPos = -1L;
                    this.storedVillageFactionsPos = -1L;
                    this.storedCountryFactionsPos = -1L;
                    this.storedFactionChangesPos = -1L;
                    this.storedParishFlagsPos = -1L;
                    this.storedCountyFlagsPos = -1L;
                    this.storedProvinceFlagsPos = -1L;
                    this.storedCountryFlagsPos = -1L;
                    foreach (VillageData data in this.villageList)
                    {
                        data.numFlags = 0;
                    }
                }
                num = 0;
                do
                {
                    this.loadingErrored = false;
                    RemoteServices.Instance.set_GetAreaFactionChanges_UserCallBack(new RemoteServices.GetAreaFactionChanges_UserCallBack(this.getAreaFactionChangesCallback));
                    RemoteServices.Instance.GetAreaFactionChanges(this.storedRegionFactionsPos - 50L, this.storedCountyFactionsPos - 10L, this.storedProvinceFactionsPos - 10L, this.storedCountryFactionsPos - 5L, this.storedParishFlagsPos, this.storedCountyFlagsPos, this.storedProvinceFlagsPos, this.storedCountryFlagsPos);
                    this.downloadWait();
                }
                while (this.loadingErrored && (num++ < 3));
                if (this.loadingErrored)
                {
                    GameEngine.Instance.setPendingSessionExpiredStat(2);
                }
                else
                {
                    if (this.m_dataLoaded)
                    {
                        num = 0;
                        do
                        {
                            this.loadingErrored = false;
                            RemoteServices.Instance.set_GetVillageFactionChanges_UserCallBack(new RemoteServices.GetVillageFactionChanges_UserCallBack(this.getVillageFactionChangesCallback));
                            RemoteServices.Instance.GetVillageFactionChanges(this.storedVillageFactionsPos - 500L, this.storedFactionChangesPos - 10L);
                            this.downloadWait();
                        }
                        while ((this.loadingErrored || !RemoteServices.Instance.GetVillageFactionChanges_ValidDownload) && (num++ < 3));
                        if (this.loadingErrored || !RemoteServices.Instance.GetVillageFactionChanges_ValidDownload)
                        {
                            GameEngine.Instance.setPendingSessionExpiredStat(2);
                            return;
                        }
                    }
                    else
                    {
                        num = 0;
                        do
                        {
                            this.loadingErrored = false;
                            RemoteServices.Instance.set_GetAllVillageOwnerFactions_UserCallBack(new RemoteServices.GetAllVillageOwnerFactions_UserCallBack(this.getAllVillageOwnerFactionsCallback));
                            RemoteServices.Instance.GetAllVillageOwnerFactions();
                            this.downloadWait();
                        }
                        while ((this.loadingErrored || !RemoteServices.Instance.GetAllVillageOwnerFactions_ValidDownload) && (num++ < 3));
                        if (this.loadingErrored || !RemoteServices.Instance.GetAllVillageOwnerFactions_ValidDownload)
                        {
                            GameEngine.Instance.setPendingSessionExpiredStat(2);
                            return;
                        }
                    }
                    num = 0;
                    do
                    {
                        this.loadingErrored = false;
                        this.retrieveUserVillages(true);
                        this.downloadWait();
                    }
                    while (this.loadingErrored && (num++ < 3));
                    if (this.loadingErrored)
                    {
                        GameEngine.Instance.setPendingSessionExpiredStat(2);
                    }
                    else
                    {
                        num = 0;
                        do
                        {
                            this.loadingErrored = false;
                            this.retrieveArmies();
                            this.downloadWait();
                        }
                        while (this.loadingErrored && (num++ < 3));
                        if (this.loadingErrored)
                        {
                            GameEngine.Instance.setPendingSessionExpiredStat(2);
                        }
                        else
                        {
                            num = 0;
                            do
                            {
                                this.loadingErrored = false;
                                this.getTraderData();
                                this.downloadWait();
                            }
                            while (this.loadingErrored && (num++ < 3));
                            if (this.loadingErrored)
                            {
                                GameEngine.Instance.setPendingSessionExpiredStat(2);
                            }
                            else
                            {
                                num = 0;
                                do
                                {
                                    this.loadingErrored = false;
                                    this.getActiveTraders();
                                    this.downloadWait();
                                }
                                while (this.loadingErrored && (num++ < 3));
                                if (this.loadingErrored)
                                {
                                    GameEngine.Instance.setPendingSessionExpiredStat(2);
                                }
                                else
                                {
                                    GameEngine.Instance.World.clearPersonArray(-1);
                                    num = 0;
                                    do
                                    {
                                        this.loadingErrored = false;
                                        this.getPersonData();
                                        this.downloadWait();
                                    }
                                    while (this.loadingErrored && (num++ < 3));
                                    if (this.loadingErrored)
                                    {
                                        GameEngine.Instance.setPendingSessionExpiredStat(2);
                                    }
                                    else
                                    {
                                        num = 0;
                                        do
                                        {
                                            this.loadingErrored = false;
                                            this.getActivePeople();
                                            this.downloadWait();
                                        }
                                        while (this.loadingErrored && (num++ < 3));
                                        if (this.loadingErrored)
                                        {
                                            GameEngine.Instance.setPendingSessionExpiredStat(2);
                                        }
                                        else
                                        {
                                            InterfaceMgr.Instance.downCurrentFactionInfo();
                                            this.downloadWait();
                                            List<AchievementData> achievementData = null;
                                            List<int> achievementToTest = this.getAchievementsToTest(ref achievementData);
                                            if ((achievementToTest != null) && (achievementToTest.Count > 0))
                                            {
                                                num = 0;
                                                do
                                                {
                                                    this.loadingErrored = false;
                                                    this.inTestAchievements = false;
                                                    this.testAchievements(achievementToTest, achievementData, true);
                                                    this.downloadWait();
                                                }
                                                while (this.loadingErrored && (num++ < 3));
                                                if (this.loadingErrored)
                                                {
                                                    GameEngine.Instance.setPendingSessionExpiredStat(2);
                                                    return;
                                                }
                                            }
                                            this.fixupNames();
                                            this.downloadComplete = true;
                                            GC.Collect();
                                            GC.WaitForPendingFinalizers();
                                            if (GameEngine.Instance.LocalWorldData.AIWorld)
                                            {
                                                this.downloadAIInvasionInfo();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void updateYourVillageFactions(int yourNewFaction)
        {
            if (this.m_userVillages != null)
            {
                foreach (UserVillageData data in this.m_userVillages)
                {
                    this.villageList[data.villageID].factionID = yourNewFaction;
                }
            }
        }

        public void updateZooming()
        {
            this.m_zoomChangeThisFrame = 0.0;
            if (this.m_zooming)
            {
                double screenCentreX = this.m_screenCentreX;
                double screenCentreY = this.m_screenCentreY;
                double num3 = (27.0 - this.m_worldZoom) + this.m_zoomDiff;
                this.m_zoomChangeThisFrame = this.m_zoomDiff;
                this.moveMap(this.m_zoomXPosDiff, this.m_zoomYPosDiff);
                if (this.m_zoomXPosDiff < 0.0)
                {
                    if (this.m_screenCentreX < this.m_zoomXPosTarget)
                    {
                        this.m_screenCentreX = this.m_zoomXPosTarget;
                        this.m_zoomXPosDiff = 0.0;
                    }
                }
                else if (this.m_screenCentreX > this.m_zoomXPosTarget)
                {
                    this.m_screenCentreX = this.m_zoomXPosTarget;
                    this.m_zoomXPosDiff = 0.0;
                }
                if (this.m_zoomYPosDiff < 0.0)
                {
                    if (this.m_screenCentreY < this.m_zoomYPosTarget)
                    {
                        this.m_screenCentreY = this.m_zoomYPosTarget;
                        this.m_zoomYPosDiff = 0.0;
                    }
                }
                else if (this.m_screenCentreY > this.m_zoomYPosTarget)
                {
                    this.m_screenCentreY = this.m_zoomYPosTarget;
                    this.m_zoomYPosDiff = 0.0;
                }
                if (Math.Abs((double) (this.m_zoomXPosTarget - this.m_screenCentreX)) < 0.1)
                {
                    this.m_zoomXPosDiff = 0.0;
                }
                if (Math.Abs((double) (this.m_zoomYPosTarget - this.m_screenCentreY)) < 0.1)
                {
                    this.m_zoomYPosDiff = 0.0;
                }
                if (this.m_zoomDiff > 0.0)
                {
                    if (num3 >= this.m_targetZoom)
                    {
                        this.m_zoomChangeThisFrame = this.m_targetZoom - (27.0 - this.m_worldZoom);
                        this.m_zoomDiff = 0.0;
                        this.WorldZoom = this.m_targetZoom;
                    }
                }
                else if ((this.m_zoomDiff < 0.0) && (num3 <= this.m_targetZoom))
                {
                    this.m_zoomChangeThisFrame = this.m_targetZoom - (27.0 - this.m_worldZoom);
                    this.m_zoomDiff = 0.0;
                    this.WorldZoom = this.m_targetZoom;
                }
                if ((this.m_zoomStage < 0) && (this.m_zoomDiff <= 0.0))
                {
                    this.centreMap(false);
                }
                if (((this.m_zoomDiff == 0.0) && (screenCentreX == this.m_screenCentreX)) && (screenCentreY == this.m_screenCentreY))
                {
                    this.m_zoomXPosDiff = 0.0;
                    this.m_zoomXPosDiff = 0.0;
                }
                if (((this.m_zoomDiff == 0.0) && (this.m_zoomXPosDiff == 0.0)) && (this.m_zoomYPosDiff == 0.0))
                {
                    this.m_zooming = false;
                    this.nextStageZoom(false);
                }
            }
        }

        public void useTickets(int level, int numberToUse)
        {
            switch (level)
            {
                case -1:
                    this.m_numQuestTickets -= numberToUse;
                    return;

                case 0:
                    this.m_treasure1Tickets -= numberToUse;
                    return;

                case 1:
                    this.m_treasure2Tickets -= numberToUse;
                    return;

                case 2:
                    this.m_treasure3Tickets -= numberToUse;
                    return;

                case 3:
                    this.m_treasure4Tickets -= numberToUse;
                    return;

                case 4:
                    this.m_treasure5Tickets -= numberToUse;
                    return;
            }
        }

        public void villageNamesCallBack(GetVillageNames_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.loadingErrored = false;
                if (returnData.villageNames != null)
                {
                    int num = 0;
                    foreach (string str in returnData.villageNames)
                    {
                        if (num < this.villageList.Length)
                        {
                            if (str.Length == 0)
                            {
                                this.villageList[num++].villageName = "Village:" + ((num - 1)).ToString();
                            }
                            else
                            {
                                this.villageList[num++].villageName = str;
                            }
                        }
                    }
                }
                if (returnData.regionNames != null)
                {
                    int num2 = 0;
                    foreach (string str2 in returnData.regionNames)
                    {
                        if (num2 < this.regionList.Length)
                        {
                            this.regionList[num2++].areaName = str2;
                        }
                    }
                }
                if (returnData.countyNames != null)
                {
                    int num3 = 0;
                    foreach (string str3 in returnData.countyNames)
                    {
                        if (num3 < this.countyList.Length)
                        {
                            this.countyList[num3++].areaName = str3;
                        }
                    }
                }
                if (returnData.provinceNames != null)
                {
                    int num4 = 0;
                    foreach (string str4 in returnData.provinceNames)
                    {
                        if (num4 < this.provincesList.Length)
                        {
                            this.provincesList[num4++].areaName = str4;
                        }
                    }
                }
                if (returnData.countryNames != null)
                {
                    int num5 = 0;
                    foreach (string str5 in returnData.countryNames)
                    {
                        if (num5 < this.countryList.Length)
                        {
                            this.countryList[num5++].areaName = str5;
                        }
                    }
                }
                if (returnData.villageChangedNames != null)
                {
                    this.changeVillageNames(returnData.villageChangedNames);
                }
                this.storedVillageNamePos = returnData.currentVillageNameChangePos;
                for (int i = 0; i < returnData.worldMapCachedData.Length; i++)
                {
                    if ((i < this.villageList.Length) && (this.villageList[i] != null))
                    {
                        this.villageList[i].villageInfo = returnData.worldMapCachedData[i];
                    }
                }
                this.saveNamesData();
                VillageMap.setServerTime(returnData.currentTime);
                GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
                GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
                GameEngine.Instance.World.setPoints(returnData.currentPoints);
            }
            else
            {
                this.loadingErrored = true;
                this.m_downloadedDataSafely = false;
            }
        }

        public void villageUserInfoCallback(RetrieveVillageUserInfo_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.villageID >= 0)
                {
                    VillageRolloverInfo info = new VillageRolloverInfo {
                        lastUpdateTime = DateTime.Now,
                        interdictionTime = returnData.interdictionDate,
                        vacationMode = returnData.vacationMode,
                        peaceTime = returnData.peaceTime,
                        villageID = returnData.villageID,
                        plagueLevel = returnData.plagueLevel
                    };
                    this.villageList[returnData.villageID].rolloverInfo = info;
                    this.villageList[returnData.villageID].userID = returnData.userID;
                    this.villageList[returnData.villageID].villageTerrain = returnData.mapType;
                    if (returnData.numVillageBuildings >= 0xff)
                    {
                        this.villageList[returnData.villageID].villageInfo = 0xff;
                    }
                    else
                    {
                        this.villageList[returnData.villageID].villageInfo = (byte) returnData.numVillageBuildings;
                    }
                    this.villageList[returnData.villageID].interdictionTime = returnData.interdictionDate;
                    this.villageList[returnData.villageID].peaceTime = returnData.peaceTime;
                    this.villageList[returnData.villageID].excommunicationTime = returnData.excommunicationTime;
                    this.villageList[returnData.villageID].vacationMode = returnData.vacationMode;
                }
                if (returnData.userID >= 0)
                {
                    CachedUserInfo info2 = new CachedUserInfo {
                        userID = returnData.userID,
                        userName = returnData.userName,
                        rank = returnData.userRank,
                        numVillages = returnData.numVillages,
                        numQuests = returnData.numQuests,
                        completedQuests = returnData.completedQuests,
                        points = returnData.points,
                        standing = returnData.standing,
                        factionID = returnData.factionID,
                        lastUpdateTime = DateTime.Now,
                        avatarData = returnData.avatarData
                    };
                    if (GameEngine.Instance.LocalWorldData.AIWorld)
                    {
                        switch (returnData.userID)
                        {
                            case 1:
                                info2.avatarData = Avatar.getRatAvatar();
                                break;

                            case 2:
                                info2.avatarData = Avatar.getSnakeAvatar();
                                break;

                            case 3:
                                info2.avatarData = Avatar.getPigAvatar();
                                break;

                            case 4:
                                info2.avatarData = Avatar.getWolfAvatar();
                                break;
                        }
                    }
                    info2.avatarData.validateColours();
                    if (info2.avatarBitmap != null)
                    {
                        info2.avatarBitmap.Dispose();
                    }
                    info2.avatarBitmap = Avatar.CreateAvatar(info2.avatarData, 0xd6, ARGBColors.Transparent, false);
                    if (returnData.villages != null)
                    {
                        info2.villages = returnData.villages.ToArray();
                        info2.admin = returnData.admin;
                        info2.moderator = returnData.moderator;
                        info2.stuff = returnData.stuff;
                    }
                    if (returnData.achievements != null)
                    {
                        info2.achievements = returnData.achievements;
                    }
                    this.cachedUserInfo[returnData.userID] = info2;
                }
            }
        }

        public void windowClicked(Point mousePos, bool doubleClick)
        {
            bool flag = true;
            this.lastClickedLocation = mousePos;
            if (!InterfaceMgr.Instance.clickDXCardBar(mousePos))
            {
                if (GameEngine.Instance.World.isTutorialActive() || Program.mySettings.showGameFeaturesScreenIcon)
                {
                    int num = this.gfx.ViewportHeight - 0x40;
                    if ((mousePos.X < 0x40) && (mousePos.Y >= num))
                    {
                        if (GameEngine.Instance.World.isTutorialActive())
                        {
                            GameEngine.Instance.World.forceTutorialToBeShown();
                            GameEngine.Instance.playInterfaceSound("WorldMap_tutorial_open");
                            return;
                        }
                        if (Program.mySettings.showGameFeaturesScreenIcon)
                        {
                            PostTutorialWindow.CreatePostTutorialWindow(false);
                            return;
                        }
                    }
                }
                if (((mousePos.X < 70) && (mousePos.Y >= 0x40)) && (mousePos.Y < 0x86))
                {
                    InterfaceMgr.Instance.openFreeCardsPopup();
                    GameEngine.Instance.playInterfaceSound("WorldMap_open_free_Cards");
                }
                else
                {
                    int num2 = this.numWheelTypesAvailable();
                    if (((num2 <= 0) || (mousePos.X >= 70)) || ((mousePos.Y < 0x90) || (mousePos.Y >= 0xd6)))
                    {
                        if (((mousePos.X > (this.m_screenWidth - 30)) && (mousePos.Y > 30)) && (mousePos.Y < 60))
                        {
                            CustomSelfDrawPanel.WikiLinkControl.openHelpLink(0);
                        }
                        else
                        {
                            double bestDist = 0.0;
                            double num5 = 0.0;
                            double num6 = 0.0;
                            double num7 = 0.0;
                            double num8 = 0.0;
                            long armyID = -1L;
                            long num10 = -1L;
                            long traderID = -1L;
                            long personID = -1L;
                            int villageID = this.findNearestVillageFromScreenPos(mousePos, ref bestDist);
                            bool flag2 = false;
                            if (((villageID >= 0) && ((27.0 - this.m_worldZoom) > 18.5)) && this.isOnlyOverVillageShield(villageID, (PointF) mousePos))
                            {
                                flag2 = true;
                            }
                            if (((villageID == -1) || ((27.0 - this.m_worldZoom) > 18.5)) && (!flag2 && (InterfaceMgr.Instance.WorldMapMode == 0)))
                            {
                                armyID = this.findNearestArmyFromScreenPos(mousePos, ref num5);
                                if ((armyID >= 0L) && ((villageID == -1) || (num5 <= bestDist)))
                                {
                                    LocalArmyData data = (LocalArmyData) this.armyArray[armyID];
                                    if (data.numScouts > 0)
                                    {
                                        GameEngine.Instance.playInterfaceSound("WorldMap_scouts", false);
                                    }
                                    else
                                    {
                                        GameEngine.Instance.playInterfaceSound("WorldMap_army", false);
                                    }
                                    if ((data.attackType != 0x11) && flag)
                                    {
                                        this.setZooming(27.0, data.displayX, data.displayY);
                                    }
                                    InterfaceMgr.Instance.closeFilterPanel();
                                    InterfaceMgr.Instance.closeSelectedVillagePanel();
                                    InterfaceMgr.Instance.closeTraderInfoPanel();
                                    InterfaceMgr.Instance.closeReinforcementSelectedPanel();
                                    InterfaceMgr.Instance.closePersonInfoPanel();
                                    InterfaceMgr.Instance.clearAndCloseUserInfo();
                                    InterfaceMgr.Instance.displayArmySelectPanel(armyID);
                                    return;
                                }
                                num10 = this.findNearestReinforcementFromScreenPos(mousePos, ref num6);
                                if ((num10 >= 0L) && ((villageID == -1) || (num6 <= bestDist)))
                                {
                                    GameEngine.Instance.playInterfaceSound("WorldMap_reinforcement", false);
                                    LocalArmyData data2 = (LocalArmyData) this.reinforcementArray[num10];
                                    if (flag)
                                    {
                                        this.setZooming(27.0, data2.displayX, data2.displayY);
                                    }
                                    InterfaceMgr.Instance.closeFilterPanel();
                                    InterfaceMgr.Instance.closeSelectedVillagePanel();
                                    InterfaceMgr.Instance.closeTraderInfoPanel();
                                    InterfaceMgr.Instance.closeArmySelectedPanel();
                                    InterfaceMgr.Instance.closePersonInfoPanel();
                                    InterfaceMgr.Instance.clearAndCloseUserInfo();
                                    InterfaceMgr.Instance.displayReinforcementSelectPanel(num10);
                                    return;
                                }
                                traderID = this.findNearestTraderFromScreenPos(mousePos, ref num7);
                                if ((traderID >= 0L) && ((villageID == -1) || (num7 <= bestDist)))
                                {
                                    GameEngine.Instance.playInterfaceSound("WorldMap_trader", false);
                                    LocalTrader trader = (LocalTrader) this.traderArray[traderID];
                                    if (flag)
                                    {
                                        this.setZooming(27.0, trader.displayX, trader.displayY);
                                    }
                                    InterfaceMgr.Instance.closeFilterPanel();
                                    InterfaceMgr.Instance.closeSelectedVillagePanel();
                                    InterfaceMgr.Instance.closeArmySelectedPanel();
                                    InterfaceMgr.Instance.closeReinforcementSelectedPanel();
                                    InterfaceMgr.Instance.closePersonInfoPanel();
                                    InterfaceMgr.Instance.clearAndCloseUserInfo();
                                    InterfaceMgr.Instance.displayTraderInfoPanel(traderID);
                                    return;
                                }
                                personID = this.findNearestPersonFromScreenPos(mousePos, ref num8);
                                if ((personID >= 0L) && ((villageID == -1) || (num8 <= bestDist)))
                                {
                                    LocalPerson person = (LocalPerson) this.personArray[personID];
                                    if (person.person.personType == 100)
                                    {
                                        GameEngine.Instance.playInterfaceSound("WorldMap_rat", false);
                                    }
                                    else
                                    {
                                        GameEngine.Instance.playInterfaceSound("WorldMap_monk", false);
                                    }
                                    if (flag)
                                    {
                                        this.setZooming(27.0, person.displayX, person.displayY);
                                    }
                                    InterfaceMgr.Instance.closeFilterPanel();
                                    InterfaceMgr.Instance.closeSelectedVillagePanel();
                                    InterfaceMgr.Instance.closeArmySelectedPanel();
                                    InterfaceMgr.Instance.closeReinforcementSelectedPanel();
                                    InterfaceMgr.Instance.closeTraderInfoPanel();
                                    InterfaceMgr.Instance.clearAndCloseUserInfo();
                                    InterfaceMgr.Instance.displayPersonInfoPanel(personID);
                                    return;
                                }
                            }
                            InterfaceMgr.Instance.closeArmySelectedPanel();
                            InterfaceMgr.Instance.closeTraderInfoPanel();
                            InterfaceMgr.Instance.closeReinforcementSelectedPanel();
                            InterfaceMgr.Instance.closePersonInfoPanel();
                            if (villageID < 0)
                            {
                                double mapPosX = 0.0;
                                double mapPosY = 0.0;
                                this.getMapCoords(mousePos, ref mapPosX, ref mapPosY);
                                if (((27.0 - this.m_worldZoom) < 1.1) && flag)
                                {
                                    this.setZooming(2.1, mapPosX, mapPosY);
                                    GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
                                }
                                else if (((27.0 - this.m_worldZoom) < 3.1) && flag)
                                {
                                    this.setZooming(3.5, mapPosX, mapPosY);
                                    GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
                                }
                                else if (((27.0 - this.m_worldZoom) < 5.5) && flag)
                                {
                                    this.setZooming(6.01, mapPosX, mapPosY);
                                    GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
                                }
                                else if (((27.0 - this.m_worldZoom) < 8.0) && flag)
                                {
                                    this.setZooming(9.51, mapPosX, mapPosY);
                                    GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
                                }
                                else if (flag)
                                {
                                    this.setZooming(27.0, mapPosX, mapPosY);
                                    if (this.m_worldZoom > 0.10000000149011612)
                                    {
                                        GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
                                    }
                                }
                            }
                            else
                            {
                                bool flag3 = true;
                                if (this.villageList[villageID].special == 30)
                                {
                                    InterfaceMgr.Instance.displaySelectedVillagePanel(villageID, doubleClick, true, false, false);
                                }
                                else if ((27.0 - this.m_worldZoom) < 1.1)
                                {
                                    int countyID = this.villageList[villageID].countyID;
                                    if (countyID >= 0)
                                    {
                                        PointF tf = this.countyList[countyID].getCentrePoint();
                                        if (flag)
                                        {
                                            this.setZooming(2.1, (double) tf.X, (double) tf.Y);
                                        }
                                        GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
                                    }
                                }
                                else if ((27.0 - this.m_worldZoom) < 3.1)
                                {
                                    int index = this.villageList[villageID].countyID;
                                    if (index >= 0)
                                    {
                                        PointF tf2 = this.countyList[index].getCentrePoint();
                                        if (flag)
                                        {
                                            this.setZooming(3.5, (double) tf2.X, (double) tf2.Y);
                                        }
                                        GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
                                    }
                                }
                                else if ((27.0 - this.m_worldZoom) < 5.5)
                                {
                                    int regionID = this.villageList[villageID].regionID;
                                    if (regionID >= 0)
                                    {
                                        PointF tf3 = this.regionList[regionID].getCentrePoint();
                                        if (flag)
                                        {
                                            this.setZooming(6.01, (double) tf3.X, (double) tf3.Y);
                                        }
                                        GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
                                    }
                                }
                                else if ((27.0 - this.m_worldZoom) < 8.0)
                                {
                                    int num17 = this.villageList[villageID].regionID;
                                    if (num17 >= 0)
                                    {
                                        PointF tf4 = this.regionList[num17].getCentrePoint();
                                        if (flag)
                                        {
                                            this.setZooming(9.51, (double) tf4.X, (double) tf4.Y);
                                        }
                                        GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
                                    }
                                }
                                else if ((27.0 - this.m_worldZoom) <= 14.5)
                                {
                                    GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
                                    if (flag)
                                    {
                                        this.setZooming(27.0, (double) this.villageList[villageID].x, (double) this.villageList[villageID].y);
                                    }
                                }
                                else
                                {
                                    if (flag)
                                    {
                                        this.setZooming(27.0, (double) this.villageList[villageID].x, (double) this.villageList[villageID].y);
                                    }
                                    if (this.m_worldZoom > 0.10000000149011612)
                                    {
                                        GameEngine.Instance.playInterfaceSound("WorldMap_zoomin");
                                    }
                                    if (this.isVillageVisible(villageID))
                                    {
                                        if (this.isCapital(villageID))
                                        {
                                            if (this.isRegionCapital(villageID))
                                            {
                                                if (this.isUserVillage(villageID))
                                                {
                                                    GameEngine.Instance.playInterfaceSound("WorldMap_parish_capital_clicked_player_owned", false);
                                                }
                                                else
                                                {
                                                    GameEngine.Instance.playInterfaceSound("WorldMap_parish_capital_clicked", false);
                                                }
                                            }
                                            if (this.isCountyCapital(villageID))
                                            {
                                                if (this.isUserVillage(villageID))
                                                {
                                                    GameEngine.Instance.playInterfaceSound("WorldMap_county_capital_clicked_player_owned", false);
                                                }
                                                else
                                                {
                                                    GameEngine.Instance.playInterfaceSound("WorldMap_county_capital_clicked", false);
                                                }
                                            }
                                            if (this.isProvinceCapital(villageID))
                                            {
                                                if (this.isUserVillage(villageID))
                                                {
                                                    GameEngine.Instance.playInterfaceSound("WorldMap_province_capital_clicked_player_owned", false);
                                                }
                                                else
                                                {
                                                    GameEngine.Instance.playInterfaceSound("WorldMap_province_capital_clicked", false);
                                                }
                                            }
                                            if (this.isCountryCapital(villageID))
                                            {
                                                if (this.isUserVillage(villageID))
                                                {
                                                    GameEngine.Instance.playInterfaceSound("WorldMap_country_capital_clicked_player_owned", false);
                                                }
                                                else
                                                {
                                                    GameEngine.Instance.playInterfaceSound("WorldMap_country_capital_clicked", false);
                                                }
                                            }
                                        }
                                        else if (!this.isSpecial(villageID))
                                        {
                                            if (this.isUserVillage(villageID))
                                            {
                                                GameEngine.Instance.playInterfaceSound("WorldMap_user_village_clicked", false);
                                            }
                                            else if (this.getVillageUserID(villageID) >= 0)
                                            {
                                                GameEngine.Instance.playInterfaceSound("WorldMap_normal_village_clicked", false);
                                            }
                                            else
                                            {
                                                GameEngine.Instance.playInterfaceSound("WorldMap_charter_clicked", false);
                                            }
                                        }
                                        else
                                        {
                                            int type = this.getSpecial(villageID);
                                            if (SpecialVillageTypes.IS_TREASURE_CASTLE(type))
                                            {
                                                GameEngine.Instance.playInterfaceSound("WorldMap_AI_Castle_clicked", false);
                                            }
                                            else
                                            {
                                                switch (type)
                                                {
                                                    case 3:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_bandit_camp_clicked", false);
                                                        break;

                                                    case 4:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_bandit_camp_destroyed_clicked", false);
                                                        break;

                                                    case 5:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_wolf_lair_clicked", false);
                                                        break;

                                                    case 6:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_wolf_lair_destroyed_clicked", false);
                                                        break;

                                                    case 7:
                                                    case 9:
                                                    case 11:
                                                    case 13:
                                                    case 15:
                                                    case 0x11:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_AI_Castle_clicked", false);
                                                        break;

                                                    case 8:
                                                    case 10:
                                                    case 12:
                                                    case 14:
                                                    case 0x10:
                                                    case 0x12:
                                                    case 40:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_AI_Castle_destroyed_clicked", false);
                                                        break;

                                                    case 0x15:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_enemy_camp_clicked", false);
                                                        break;
                                                }
                                            }
                                            if (type == 100)
                                            {
                                                GameEngine.Instance.playInterfaceSound("WorldMap_unknown_resource_stash_clicked", false);
                                            }
                                            else if ((type > 100) && (type <= 0xc7))
                                            {
                                                switch (type)
                                                {
                                                    case 0x6a:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_wood", false);
                                                        break;

                                                    case 0x6b:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_stone", false);
                                                        break;

                                                    case 0x6c:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_iron", false);
                                                        break;

                                                    case 0x6d:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_pitch", false);
                                                        break;

                                                    case 0x70:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_ale", false);
                                                        break;

                                                    case 0x71:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_apple", false);
                                                        break;

                                                    case 0x72:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_bread", false);
                                                        break;

                                                    case 0x73:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_veg", false);
                                                        break;

                                                    case 0x74:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_meat", false);
                                                        break;

                                                    case 0x75:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_cheese", false);
                                                        break;

                                                    case 0x76:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_fish", false);
                                                        break;

                                                    case 0x77:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_clothes", false);
                                                        break;

                                                    case 0x79:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_furniture", false);
                                                        break;

                                                    case 0x7a:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_venison", false);
                                                        break;

                                                    case 0x7b:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_salt", false);
                                                        break;

                                                    case 0x7c:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_spices", false);
                                                        break;

                                                    case 0x7d:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_silk", false);
                                                        break;

                                                    case 0x7e:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_metalware", false);
                                                        break;

                                                    case 0x85:
                                                        GameEngine.Instance.playInterfaceSound("WorldMap_resource_stash_clicked_wine", false);
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    if ((InterfaceMgr.Instance.OwnSelectedVillage >= 0) || ((InterfaceMgr.Instance.WorldMapMode == 0) && this.isOverVillageShield(villageID, (PointF) mousePos, true)))
                                    {
                                        if (InterfaceMgr.Instance.WorldMapMode == 1)
                                        {
                                            VillageData data3 = this.getVillageData(villageID);
                                            if (!this.isSpecial(villageID) && (this.isCapital(villageID) || (data3.userID >= 0)))
                                            {
                                                flag3 = false;
                                                InterfaceMgr.Instance.SelectedVillage = villageID;
                                                InterfaceMgr.Instance.setTradeWithVillage(villageID);
                                            }
                                            else
                                            {
                                                InterfaceMgr.Instance.SelectedVillage = -1;
                                                InterfaceMgr.Instance.setTradeWithVillage(-1);
                                            }
                                        }
                                        else if (InterfaceMgr.Instance.WorldMapMode == 2)
                                        {
                                            if (this.isCapital(villageID))
                                            {
                                                bool flag4 = true;
                                                if (!this.allowExchangeTrade(villageID, InterfaceMgr.Instance.StockExchangeBuyingVillage))
                                                {
                                                    flag4 = false;
                                                }
                                                if (flag4)
                                                {
                                                    flag3 = false;
                                                    InterfaceMgr.Instance.SelectedVillage = villageID;
                                                    InterfaceMgr.Instance.setStockExchangeSidePanelVillage(villageID);
                                                }
                                                else
                                                {
                                                    InterfaceMgr.Instance.SelectedVillage = -1;
                                                    InterfaceMgr.Instance.setStockExchangeSidePanelVillage(-1);
                                                }
                                            }
                                        }
                                        else if (InterfaceMgr.Instance.WorldMapMode == 3)
                                        {
                                            VillageData data4 = this.getVillageData(villageID);
                                            bool flag5 = true;
                                            if (this.isSpecial(villageID) && !this.isAttackableSpecial(villageID))
                                            {
                                                flag5 = false;
                                            }
                                            else if ((!this.isSpecial(villageID) && (data4.userID < 0)) && !this.isCapital(villageID))
                                            {
                                                flag5 = false;
                                            }
                                            if (flag5)
                                            {
                                                flag3 = false;
                                                InterfaceMgr.Instance.SelectedVillage = villageID;
                                                InterfaceMgr.Instance.setAttackTargetSidePanelVillage(villageID);
                                            }
                                            else
                                            {
                                                InterfaceMgr.Instance.SelectedVillage = -1;
                                                InterfaceMgr.Instance.setAttackTargetSidePanelVillage(-1);
                                            }
                                        }
                                        else if (InterfaceMgr.Instance.WorldMapMode == 4)
                                        {
                                            VillageData data5 = this.getVillageData(villageID);
                                            bool flag6 = true;
                                            if (this.isSpecial(villageID) && !this.isScoutableSpecial(villageID))
                                            {
                                                flag6 = false;
                                            }
                                            else if ((!this.isSpecial(villageID) && (data5.userID < 0)) && !this.isCapital(villageID))
                                            {
                                                flag6 = false;
                                            }
                                            if (flag6)
                                            {
                                                flag3 = false;
                                                InterfaceMgr.Instance.SelectedVillage = villageID;
                                                InterfaceMgr.Instance.setScoutTargetSidePanelVillage(villageID);
                                            }
                                            else
                                            {
                                                InterfaceMgr.Instance.SelectedVillage = -1;
                                                InterfaceMgr.Instance.setScoutTargetSidePanelVillage(-1);
                                            }
                                        }
                                        else if (InterfaceMgr.Instance.WorldMapMode == 5)
                                        {
                                            VillageData data6 = this.getVillageData(villageID);
                                            if ((!this.isCapital(villageID) && !this.isSpecial(villageID)) && (data6.userID >= 0))
                                            {
                                                flag3 = false;
                                                InterfaceMgr.Instance.SelectedVillage = villageID;
                                                InterfaceMgr.Instance.setReinforcementTargetSidePanelVillage(villageID);
                                            }
                                            else
                                            {
                                                InterfaceMgr.Instance.SelectedVillage = -1;
                                                InterfaceMgr.Instance.setReinforcementTargetSidePanelVillage(-1);
                                            }
                                        }
                                        else if (InterfaceMgr.Instance.WorldMapMode != 6)
                                        {
                                            if (InterfaceMgr.Instance.WorldMapMode == 7)
                                            {
                                                VillageData data7 = this.getVillageData(villageID);
                                                bool flag7 = true;
                                                if (this.isCapital(villageID))
                                                {
                                                    flag7 = false;
                                                }
                                                else if (this.isSpecial(villageID) && !this.isAttackableSpecial(villageID))
                                                {
                                                    flag7 = false;
                                                }
                                                else if (!this.isSpecial(villageID) && (data7.userID < 0))
                                                {
                                                    flag7 = false;
                                                }
                                                if (flag7)
                                                {
                                                    flag3 = false;
                                                    InterfaceMgr.Instance.SelectedVillage = villageID;
                                                    InterfaceMgr.Instance.setVassalSelectSidePanelVillage(villageID);
                                                }
                                                else
                                                {
                                                    InterfaceMgr.Instance.SelectedVillage = -1;
                                                    InterfaceMgr.Instance.setVassalSelectSidePanelVillage(-1);
                                                }
                                            }
                                            else if (InterfaceMgr.Instance.WorldMapMode == 9)
                                            {
                                                VillageData data8 = this.getVillageData(villageID);
                                                if (!this.isSpecial(villageID) && ((data8.userID >= 0) || data8.Capital))
                                                {
                                                    flag3 = false;
                                                    InterfaceMgr.Instance.SelectedVillage = villageID;
                                                    InterfaceMgr.Instance.setMonkSelectSidePanelVillage(villageID);
                                                }
                                                else
                                                {
                                                    InterfaceMgr.Instance.SelectedVillage = -1;
                                                    InterfaceMgr.Instance.setMonkSelectSidePanelVillage(-1);
                                                }
                                            }
                                            else
                                            {
                                                bool forceSelfClick = this.isOverVillageShield(villageID, (PointF) mousePos, false);
                                                flag3 = false;
                                                if (this.worldMapFilter.showVillage(villageID) >= 0)
                                                {
                                                    InterfaceMgr.Instance.displaySelectedVillagePanel(villageID, doubleClick, true, forceSelfClick, false);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        InterfaceMgr.Instance.clearAndCloseUserInfo();
                                        InterfaceMgr.Instance.displaySelectedVillagePanel(villageID, doubleClick, true, false, true);
                                        flag3 = false;
                                    }
                                    if (flag3)
                                    {
                                        InterfaceMgr.Instance.closeSelectedVillagePanelButNotSelect();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (num2 == 1)
                        {
                            for (int i = -1; i < 5; i++)
                            {
                                if (this.getTickets(i) > 0)
                                {
                                    InterfaceMgr.Instance.openWheelPopup(i);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            InterfaceMgr.Instance.openWheelSelectPopup();
                        }
                        GameEngine.Instance.playInterfaceSound("WorldMap_open_wheel");
                    }
                }
            }
        }

        public void worldShieldsDownloaded()
        {
            this.worldShieldsAvailable = true;
            this.clearShieldCache();
        }

        public void zoomOut()
        {
            InterfaceMgr.Instance.clearAndCloseUserInfo();
            InterfaceMgr.Instance.closeSelectedVillagePanelButNotSelect();
            InterfaceMgr.Instance.closeArmySelectedPanel();
            InterfaceMgr.Instance.closeReinforcementSelectedPanel();
            InterfaceMgr.Instance.closePersonInfoPanel();
            InterfaceMgr.Instance.closeTraderInfoPanel();
            if (!this.m_zooming || (this.m_zoomDiff >= 0.0))
            {
                if ((27.0 - this.m_worldZoom) > 9.51)
                {
                    this.setZooming(9.51, this.m_screenCentreX, this.m_screenCentreY);
                    GameEngine.Instance.playInterfaceSound("WorldMap_zoomout");
                }
                else if ((27.0 - this.m_worldZoom) > 6.02)
                {
                    this.setZooming(6.01, this.m_screenCentreX, this.m_screenCentreY);
                    GameEngine.Instance.playInterfaceSound("WorldMap_zoomout");
                }
                else if ((27.0 - this.m_worldZoom) > 4.02)
                {
                    this.setZooming(4.01, this.m_screenCentreX, this.m_screenCentreY);
                    GameEngine.Instance.playInterfaceSound("WorldMap_zoomout");
                }
                else if ((27.0 - this.m_worldZoom) > 2.12)
                {
                    this.setZooming(2.11, this.m_screenCentreX, this.m_screenCentreY);
                    GameEngine.Instance.playInterfaceSound("WorldMap_zoomout");
                }
                else
                {
                    this.setZooming(this.m_zoomCap, this.m_screenCentreX, this.m_screenCentreY);
                    if (this.m_worldZoom < 26.899999998509884)
                    {
                        GameEngine.Instance.playInterfaceSound("WorldMap_zoomout");
                    }
                }
            }
        }

        public void zoomToVillage(int villageID)
        {
            this.startMultiStageZoom(27.0, (double) this.villageList[villageID].x, (double) this.villageList[villageID].y);
        }

        public List<int> CatalogCardsSearch
        {
            get
            {
                if (this.mCatalogCardsSearch == null)
                {
                    this.mCatalogCardsSearch = new List<int>();
                }
                return this.mCatalogCardsSearch;
            }
        }

        public long CurrentVillageFactionsPos
        {
            get
            {
                return this.storedVillageFactionsPos;
            }
        }

        public bool DrawDebugNames
        {
            get
            {
                return this.drawDebugNames;
            }
            set
            {
                this.drawDebugNames = value;
            }
        }

        public bool DrawDebugVillageNames
        {
            get
            {
                return this.drawDebugVillageNames;
            }
            set
            {
                this.drawDebugVillageNames = value;
            }
        }

        public int[] FactionAllies
        {
            get
            {
                return this.m_factionAllies;
            }
            set
            {
                this.m_factionAllies = value;
            }
        }

        public List<FactionInviteData> FactionApplications
        {
            get
            {
                return this.m_factionApplications;
            }
            set
            {
                this.m_factionApplications = value;
            }
        }

        public int[] FactionEnemies
        {
            get
            {
                return this.m_factionEnemies;
            }
            set
            {
                this.m_factionEnemies = value;
            }
        }

        public FactionInviteData[] FactionInvites
        {
            get
            {
                return this.m_factionInvites;
            }
            set
            {
                this.m_factionInvites = value;
            }
        }

        public FactionMemberData[] FactionMembers
        {
            get
            {
                return this.m_factionMembers;
            }
            set
            {
                this.m_factionMembers = value;
                this.lastTimeOwnMembersUpdated = DateTime.Now;
            }
        }

        public bool FifthAgeWorld
        {
            get
            {
                return this.fifthAgeWorld;
            }
            set
            {
                this.fifthAgeWorld = value;
            }
        }

        public bool FourthAgeWorld
        {
            get
            {
                return this.fourthAgeWorld;
            }
            set
            {
                this.fourthAgeWorld = value;
            }
        }

        public FreeCardsData FreeCardInfo
        {
            get
            {
                return this.freeCardInfo;
            }
        }

        public long HighestArmyIDSeen
        {
            get
            {
                return this.highestArmySeen;
            }
            set
            {
                this.highestArmySeen = value;
            }
        }

        public int[] HouseAllies
        {
            get
            {
                return this.m_houseAllies;
            }
            set
            {
                this.m_houseAllies = value;
            }
        }

        public int[] HouseEnemies
        {
            get
            {
                return this.m_houseEnemies;
            }
            set
            {
                this.m_houseEnemies = value;
            }
        }

        public int[] HouseGloryPoints
        {
            get
            {
                return this.m_gloryPoints;
            }
            set
            {
                this.m_gloryPoints = value;
                this.lastHouseGloryPointsUpdate = DateTime.Now;
            }
        }

        public GloryRoundData HouseGloryRoundData
        {
            get
            {
                return this.m_gloryRoundData;
            }
            set
            {
                this.m_gloryRoundData = value;
            }
        }

        public HouseData[] HouseInfo
        {
            get
            {
                return this.m_houseData;
            }
            set
            {
                this.m_houseData = value;
            }
        }

        public HouseVoteData HouseVoteInfo
        {
            get
            {
                return this.m_houseVoteData;
            }
            set
            {
                this.m_houseVoteData = value;
            }
        }

        public bool InviteSystemNotImplemented
        {
            get
            {
                return this.inviteSystemNotImplemented;
            }
            set
            {
                this.inviteSystemNotImplemented = value;
            }
        }

        public bool LinelessMaps
        {
            get
            {
                return (this.bLinelessMap && !this.overrideLinelessMap);
            }
        }

        public bool MapEditing
        {
            get
            {
                return this.mapEditing;
            }
            set
            {
                this.mapEditing = value;
            }
        }

        public int MostAge4Villages
        {
            get
            {
                return this.m_mostAge4Villages;
            }
            set
            {
                this.m_mostAge4Villages = value;
            }
        }

        public int NumVacationsAvailable
        {
            get
            {
                return this.numVacationsAvailable;
            }
            set
            {
                this.numVacationsAvailable = value;
            }
        }

        public Dictionary<int, CardTypes.CardOffer> ProfileCardOffers
        {
            get
            {
                if (this.mProfileCardOffers == null)
                {
                    this.mProfileCardOffers = new Dictionary<int, CardTypes.CardOffer>();
                }
                return this.mProfileCardOffers;
            }
        }

        public Dictionary<int, CardTypes.CardDefinition> ProfileCards
        {
            get
            {
                if (this.mProfileCards == null)
                {
                    this.mProfileCards = new Dictionary<int, CardTypes.CardDefinition>();
                }
                return this.mProfileCards;
            }
        }

        public List<int> ProfileCardsSearch
        {
            get
            {
                if (this.mProfileCardsSearch == null)
                {
                    this.mProfileCardsSearch = new List<int>();
                }
                return this.mProfileCardsSearch;
            }
        }

        public List<int> ProfileCardsSet
        {
            get
            {
                if (this.mProfileCardsSet == null)
                {
                    this.mProfileCardsSet = new List<int>();
                }
                return this.mProfileCardsSet;
            }
        }

        public Dictionary<int, CardTypes.PremiumToken> ProfilePremiumTokens
        {
            get
            {
                if (this.mProfilePremiumTokens == null)
                {
                    this.mProfilePremiumTokens = new Dictionary<int, CardTypes.PremiumToken>();
                }
                return this.mProfilePremiumTokens;
            }
        }

        public Dictionary<int, CardTypes.UserCardPack> ProfileUserCardPacks
        {
            get
            {
                if (this.mProfileUserCardPacks == null)
                {
                    this.mProfileUserCardPacks = new Dictionary<int, CardTypes.UserCardPack>();
                }
                return this.mProfileUserCardPacks;
            }
            set
            {
                this.mProfileUserCardPacks = value;
            }
        }

        public int ProfileUserCardPacksCount
        {
            get
            {
                if (this.mProfileUserCardPacks == null)
                {
                    return 0;
                }
                int num = 0;
                foreach (CardTypes.UserCardPack pack in this.mProfileUserCardPacks.Values)
                {
                    num += pack.Count;
                }
                return num;
            }
        }

        public double ScreenCentreX
        {
            get
            {
                return this.m_screenCentreX;
            }
        }

        public double ScreenCentreY
        {
            get
            {
                return this.m_screenCentreY;
            }
        }

        public bool SecondAgeWorld
        {
            get
            {
                return this.secondAgeWorld;
            }
            set
            {
                this.secondAgeWorld = value;
            }
        }

        public List<int> ShoppingCartCards
        {
            get
            {
                if (this.mShoppingCartCards == null)
                {
                    this.mShoppingCartCards = new List<int>();
                }
                return this.mShoppingCartCards;
            }
        }

        public long StoredFactionChangesPos
        {
            get
            {
                return this.storedFactionChangesPos;
            }
        }

        public long StoredVillageFactionPos
        {
            get
            {
                return this.storedVillageFactionsPos;
            }
        }

        public bool ThirdAgeWorld
        {
            get
            {
                return this.thirdAgeWorld;
            }
            set
            {
                this.thirdAgeWorld = value;
            }
        }

        public CardData UserCardData
        {
            get
            {
                if (this.m_userCardData == null)
                {
                    return new CardData();
                }
                DateTime time = VillageMap.getCurrentServerTime();
                for (int i = 0; i < this.m_userCardData.cards.Length; i++)
                {
                    if ((this.m_userCardData.cards[i] != 0) && (time > this.m_userCardData.cardsExpiry[i]))
                    {
                        this.m_userCardData.cards[i] = 0;
                    }
                }
                if (this.m_userCardData.premiumCard != 0)
                {
                    if ((this.m_userCardData.premiumCard == 0x1011) && (time > this.m_userCardData.premiumCardExpiry))
                    {
                        this.m_userCardData.premiumCard = 0;
                        if (!Program.mySettings.AdvertShown)
                        {
                            this.m_userCardData.premiumAdvertNeeded = true;
                        }
                    }
                    else if (time > this.m_userCardData.premiumCardExpiry)
                    {
                        this.m_userCardData.premiumCard = 0;
                    }
                }
                if (this.m_userCardData.premiumAdvertNeeded)
                {
                    this.m_userCardData.premiumAdvertNeeded = false;
                    Program.mySettings.AdvertShown = true;
                    InterfaceMgr.Instance.openLogoutWindow(true, true);
                }
                return this.m_userCardData;
            }
            set
            {
                this.m_userCardData = value;
                if (this.m_userCardData != null)
                {
                    InterfaceMgr.Instance.setCardData(this.m_userCardData);
                }
                this.UserCardDataChanged = true;
            }
        }

        public List<UserRelationship> UserRelations
        {
            get
            {
                return this.userRelations;
            }
            set
            {
                if (value != null)
                {
                    this.userRelations = value;
                }
            }
        }

        public ResearchData UserResearchData
        {
            get
            {
                return this.userResearchData;
            }
        }

        public bool VacationNot30Days
        {
            get
            {
                return this.vacationNot30Days;
            }
            set
            {
                this.vacationNot30Days = value;
            }
        }

        public string WorldDefaultLanguage
        {
            get
            {
                CommonTypes.WorldMapType type = GameEngine.Instance.WorldMapTypesData.getMapData(this.currentMapType);
                if (type.mapName.ToLower() != "uk.wmpData".ToLower())
                {
                    if (type.mapName.ToLower() == "de.wmpData".ToLower())
                    {
                        return "de";
                    }
                    if (type.mapName.ToLower() == "fr.wmpData".ToLower())
                    {
                        return "fr";
                    }
                    if (type.mapName.ToLower() == "ru.wmpData".ToLower())
                    {
                        return "ru";
                    }
                    if (type.mapName.ToLower() == "es.wmpData".ToLower())
                    {
                        return "es";
                    }
                    if (type.mapName.ToLower() == "pl.wmpData".ToLower())
                    {
                        return "pl";
                    }
                    if (type.mapName.ToLower() == "sa.wmpData".ToLower())
                    {
                        return "pt";
                    }
                    if (type.mapName.ToLower() == "it.wmpData".ToLower())
                    {
                        return "it";
                    }
                    if (type.mapName.ToLower() == "tr.wmpData".ToLower())
                    {
                        return "tr";
                    }
                }
                return "en";
            }
        }

        public int WorldMapType
        {
            get
            {
                return this.currentMapType;
            }
        }

        public double WorldScale
        {
            get
            {
                return this.m_worldScale;
            }
        }

        public double WorldZoom
        {
            get
            {
                return this.m_worldZoom;
            }
            set
            {
                this.m_worldZoom = 27.0 - value;
                if (this.m_worldZoom >= 23.0)
                {
                    this.m_worldScale = 1.0 / (this.m_worldZoom - 22.0);
                }
                else
                {
                    this.m_worldScale = 24.0 - this.m_worldZoom;
                }
            }
        }

        public FactionData YourFaction
        {
            get
            {
                if (RemoteServices.Instance.UserFactionID >= 0)
                {
                    return (FactionData) this.m_factionData[RemoteServices.Instance.UserFactionID];
                }
                return null;
            }
            set
            {
                if ((value != null) && (value.factionID >= 0))
                {
                    this.m_factionData[value.factionID] = value;
                    RemoteServices.Instance.UserFactionID = value.factionID;
                }
                else
                {
                    RemoteServices.Instance.UserFactionID = -1;
                }
            }
        }

        public int YourFactionVote
        {
            get
            {
                return this.m_factionLeaderVote;
            }
            set
            {
                this.m_factionLeaderVote = value;
            }
        }

        public int YourHouse
        {
            get
            {
                FactionData yourFaction = this.YourFaction;
                if (yourFaction != null)
                {
                    return yourFaction.houseID;
                }
                return 0;
            }
        }

        public double ZoomCap
        {
            get
            {
                return this.m_zoomCap;
            }
        }

        public double ZoomChange
        {
            get
            {
                return this.m_zoomChangeThisFrame;
            }
        }

        public bool Zooming
        {
            get
            {
                return this.m_zooming;
            }
            set
            {
                this.m_zooming = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ArmyRetrieveData
        {
            public long armyID;
            public DateTime expiryTime;
        }

        public class CachedUserInfo
        {
            public List<int> achievements;
            public bool admin;
            public Bitmap avatarBitmap;
            public AvatarData avatarData;
            public List<int> completedQuests;
            public int factionID = -1;
            public double gold = -1.0;
            public double honour = -1.0;
            public DateTime lastUpdateTime = DateTime.Now.AddYears(-1);
            public bool moderator;
            public int numQuests;
            public int numVillages;
            public int points;
            public int rank;
            public int standing;
            public string stuff = "";
            public int userID = -1;
            public string userName = "";
            public int[] villages;

            ~CachedUserInfo()
            {
                if (this.avatarBitmap != null)
                {
                    this.avatarBitmap.Dispose();
                }
            }
        }

        public class CapitalPeopleGFX
        {
            public int numOthers;
            public int numYours;
            public float posX;
            public float posY;
        }

        public class FastScreenRect
        {
            public int bottom;
            public float Height;
            public int left;
            public float Left;
            public int right;
            public int top;
            public float Top;
            public float Width;
            public double zoomLevel;
        }

        public class InterVillageLine
        {
            public PointF end;
            public bool minLength = true;
            public PointF start;
            public int style = 1;
            public float widthScalar = 1.1f;
        }

        public class InterVillageLinesStyles
        {
            public const int SELECT_GREEN_YELLOW_TAPER = 5;
            public const int SELECT_GREEN_YELLOW_TAPER_PULSE = 6;
            public const int VASSAL_BLUE = 1;
            public const int VASSAL_GREEN = 2;
            public const int VASSAL_LIGHTGREEN = 3;
            public const int VASSAL_LIGHTRED = 4;
        }

        public class IslandInfoList
        {
            public int country = -1;
            public int county = -1;
            public int ex;
            public int ey;
            public int province = -1;
            public int sx;
            public int sy;
            public int villageID = -1;
        }

        public class LeaderboardSelfRankingsComparer : IComparer<LeaderBoardSelfRankings>
        {
            public int Compare(LeaderBoardSelfRankings y, LeaderBoardSelfRankings x)
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
                if (x.value == 0)
                {
                    if (y.value == 0)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y.value == 0)
                {
                    return 1;
                }
                if (x.place < y.place)
                {
                    return 1;
                }
                if (x.place > y.place)
                {
                    return -1;
                }
                return 0;
            }
        }

        public class LocalArmyData
        {
            public int aiPlayer;
            public long armyID;
            public int attackType;
            public double baseDisplayX;
            public double baseDisplayY;
            public int captainsCommand;
            public bool carryingFlag;
            public bool dead;
            public double displayX;
            public double displayY;
            public double fakeEndTime;
            public int homeVillageID;
            public double localEndTime;
            public double localStartTime;
            public ArmyLootData lootData;
            public double lootLevel;
            public int lootType;
            public int numArchers;
            public int numCaptains;
            public int numCatapults;
            public int numPeasants;
            public int numPikemen;
            public int numScouts;
            public int numSwordsmen;
            public bool realData = true;
            public bool reinforcements;
            public bool requestSent;
            public DateTime serverEndTime;
            public bool singlyAdded;
            public double targetDisplayX;
            public double targetDisplayY;
            public int targetVillageID;
            public int travelFromVillageID;
            public int userID;
            public bool visible = true;

            public PointF BasePoint()
            {
                if (!this.reinforcements)
                {
                    if (this.lootType < 0)
                    {
                        return new PointF((float) this.baseDisplayX, (float) this.baseDisplayY);
                    }
                    return new PointF((float) this.targetDisplayX, (float) this.targetDisplayY);
                }
                if (this.attackType == 20)
                {
                    return new PointF((float) this.baseDisplayX, (float) this.baseDisplayY);
                }
                return new PointF((float) this.targetDisplayX, (float) this.targetDisplayY);
            }

            public void createJourney(DateTime startTime, DateTime curTime, DateTime endTime)
            {
                endTime = endTime.AddSeconds(2.0);
                this.serverEndTime = endTime;
                if ((curTime > endTime) && this.reinforcements)
                {
                    this.visible = false;
                }
                else
                {
                    this.visible = true;
                    double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
                    TimeSpan span = (TimeSpan) (curTime - startTime);
                    this.localStartTime = num - span.TotalSeconds;
                    TimeSpan span2 = (TimeSpan) (endTime - curTime);
                    this.localEndTime = num + span2.TotalSeconds;
                    this.fakeEndTime = this.localEndTime + 3.0;
                    this.requestSent = false;
                    if ((this.attackType == 0x15) && !GameEngine.Instance.World.isUserVillage(this.homeVillageID))
                    {
                        this.visible = false;
                    }
                }
            }

            public bool isCaptainAttack()
            {
                if (this.numCaptains <= 0)
                {
                    return (this.attackType == 0x12);
                }
                return true;
            }

            public bool isScouts()
            {
                return (this.numScouts > 0);
            }

            public bool isVisible(RectangleF screenRect)
            {
                if ((screenRect.Top - 50f) > this.displayY)
                {
                    return false;
                }
                if ((screenRect.Left - 50f) > this.displayX)
                {
                    return false;
                }
                if ((screenRect.Bottom + 50f) < this.displayY)
                {
                    return false;
                }
                if ((screenRect.Right + 50f) < this.displayX)
                {
                    return false;
                }
                return true;
            }

            public PointF TargetPoint()
            {
                if (!this.reinforcements)
                {
                    if (this.lootType < 0)
                    {
                        return new PointF((float) this.targetDisplayX, (float) this.targetDisplayY);
                    }
                    return new PointF((float) this.baseDisplayX, (float) this.baseDisplayY);
                }
                if (this.attackType == 20)
                {
                    return new PointF((float) this.targetDisplayX, (float) this.targetDisplayY);
                }
                return new PointF((float) this.baseDisplayX, (float) this.baseDisplayY);
            }

            public void updatePosition()
            {
                if ((this.targetVillageID >= 0) && this.visible)
                {
                    double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
                    double localStartTime = this.localStartTime;
                    double localEndTime = this.localEndTime;
                    if (this.lootType >= 0)
                    {
                        localStartTime += 3.0;
                        localEndTime += 3.0;
                    }
                    double num4 = (num - localStartTime) / (localEndTime - localStartTime);
                    double num5 = (num - localStartTime) / (this.fakeEndTime - localStartTime);
                    double num6 = num4;
                    if (!this.reinforcements)
                    {
                        if (this.lootType < 0)
                        {
                            num6 = num5;
                            if (num4 > 1.0)
                            {
                                if ((this.attackType == 30) || (this.attackType == 0x1f))
                                {
                                    this.dead = true;
                                }
                                else if ((!this.requestSent && ((num - this.localEndTime) > 1.0)) && ((num - this.localEndTime) < 60.0))
                                {
                                    if (((RemoteServices.Instance.UserID == this.userID) || GameEngine.Instance.World.isUserVillage(this.targetVillageID)) && GameEngine.Instance.World.checkRecentRetrievalSend(this.armyID))
                                    {
                                        if (this.attackType == 13)
                                        {
                                            GameEngine.Instance.World.tutorialArmyID = this.armyID;
                                        }
                                        RemoteServices.Instance.RetrieveAttackResult(this.armyID, GameEngine.Instance.World.StoredVillageFactionPos);
                                        WorldMap.ArmyRetrieveData item = new WorldMap.ArmyRetrieveData {
                                            armyID = this.armyID,
                                            expiryTime = DateTime.Now.AddSeconds(30.0)
                                        };
                                        GameEngine.Instance.World.requestedReturnedArmyIDs.Add(item);
                                    }
                                    this.requestSent = true;
                                }
                            }
                        }
                        if (num6 > 1.0)
                        {
                            if (this.lootType < 0)
                            {
                                if (GameEngine.Instance.LocalWorldData.AIWorld && (this.attackType == 0x11))
                                {
                                    this.dead = true;
                                }
                                else
                                {
                                    this.requestSent = false;
                                    num6 = 0.0;
                                    this.lootType = 0x2710;
                                    double num7 = this.localEndTime - this.localStartTime;
                                    this.localStartTime = this.localEndTime;
                                    this.localEndTime += num7;
                                    this.fakeEndTime += num7;
                                    this.realData = false;
                                }
                            }
                            else
                            {
                                this.dead = true;
                                VillageMap map = GameEngine.Instance.getVillage(this.travelFromVillageID);
                                if ((map != null) && this.realData)
                                {
                                    map.addTroopsArmyReturnSpecial(this.numPeasants, this.numArchers, this.numPikemen, this.numSwordsmen, this.numCatapults, this.numScouts, this.numCaptains);
                                    if (((this.numScouts > 0) && (this.lootType >= 100)) && (this.lootType <= 0xc7))
                                    {
                                        map.addResources(this.lootType - 100, (int) this.lootLevel);
                                    }
                                    if ((this.lootType == 2) && (this.lootData != null))
                                    {
                                        map.addResources(6, this.lootData.woodLevel);
                                        map.addResources(7, this.lootData.stoneLevel);
                                        map.addResources(8, this.lootData.ironLevel);
                                        map.addResources(9, this.lootData.pitchLevel);
                                        map.addResources(13, this.lootData.applesLevel);
                                        map.addResources(14, this.lootData.breadLevel);
                                        map.addResources(0x11, this.lootData.cheeseLevel);
                                        map.addResources(0x10, this.lootData.meatLevel);
                                        map.addResources(0x12, this.lootData.fishLevel);
                                        map.addResources(15, this.lootData.vegLevel);
                                        map.addResources(12, this.lootData.aleLevel);
                                        map.addResources(0x15, this.lootData.furnitureLevel);
                                        map.addResources(0x13, this.lootData.clothesLevel);
                                        map.addResources(0x16, this.lootData.venisonLevel);
                                        map.addResources(0x17, this.lootData.saltLevel);
                                        map.addResources(0x21, this.lootData.wineLevel);
                                        map.addResources(0x1a, this.lootData.metalwareLevel);
                                        map.addResources(0x18, this.lootData.spicesLevel);
                                        map.addResources(0x19, this.lootData.silkLevel);
                                        map.addResources(0x1d, this.lootData.bowsLevel);
                                        map.addResources(0x1c, this.lootData.pikesLevel);
                                        map.addResources(30, this.lootData.swordsLevel);
                                        map.addResources(0x1f, this.lootData.armourLevel);
                                        map.addResources(0x20, this.lootData.catapultLevel);
                                    }
                                    else if (((this.lootType >= 500) && (this.lootType < 0x3e8)) && (this.lootType < 700))
                                    {
                                        map.addResources(this.lootType, (int) this.lootLevel);
                                    }
                                }
                                else if (map != null)
                                {
                                    GameEngine.Instance.flushVillage(map.VillageID);
                                }
                            }
                        }
                        if (num6 < 0.0)
                        {
                            num6 = 0.0;
                        }
                        if (this.lootType < 0)
                        {
                            this.displayX = ((this.targetDisplayX - this.baseDisplayX) * num6) + this.baseDisplayX;
                            this.displayY = ((this.targetDisplayY - this.baseDisplayY) * num6) + this.baseDisplayY;
                        }
                        else
                        {
                            this.displayX = ((this.baseDisplayX - this.targetDisplayX) * num6) + this.targetDisplayX;
                            this.displayY = ((this.baseDisplayY - this.targetDisplayY) * num6) + this.targetDisplayY;
                        }
                    }
                    else if (num4 <= 1.0)
                    {
                        if (this.attackType == 20)
                        {
                            this.displayX = ((this.targetDisplayX - this.baseDisplayX) * num6) + this.baseDisplayX;
                            this.displayY = ((this.targetDisplayY - this.baseDisplayY) * num6) + this.baseDisplayY;
                        }
                        else
                        {
                            this.displayX = ((this.baseDisplayX - this.targetDisplayX) * num6) + this.targetDisplayX;
                            this.displayY = ((this.baseDisplayY - this.targetDisplayY) * num6) + this.targetDisplayY;
                        }
                    }
                    else
                    {
                        this.visible = false;
                        VillageMap map2 = GameEngine.Instance.getVillage(this.homeVillageID);
                        if ((this.attackType == 0x15) && (map2 != null))
                        {
                            this.dead = true;
                            map2.addTroops(this.numPeasants, this.numArchers, this.numPikemen, this.numSwordsmen, this.numCatapults, this.numScouts);
                            this.numPeasants = this.numArchers = this.numPikemen = this.numSwordsmen = this.numCatapults = 0;
                        }
                    }
                }
                else
                {
                    this.displayX = this.baseDisplayX;
                    this.displayY = this.baseDisplayY;
                }
            }
        }

        public class LocalPerson
        {
            public double baseDisplayX;
            public double baseDisplayY;
            public int childrenCount;
            public double displayX;
            public double displayY;
            public bool dying;
            public double lastRatio;
            public double localEndTime;
            public double localStartTime;
            public long parentPerson = -1L;
            public PersonData person;
            public long personID;
            public DateTime serverEndTime;
            public double targetDisplayX;
            public double targetDisplayY;

            public PointF BasePoint()
            {
                if ((((this.person.state != 0) && (this.person.state != 1)) && ((this.person.state != 2) && (this.person.state != 11))) && (((this.person.state != 12) && (this.person.state != 0x15)) && (((this.person.state != 0x16) && (this.person.state != 0x1f)) && (this.person.state != 0x4b))))
                {
                    return new PointF((float) this.targetDisplayX, (float) this.targetDisplayY);
                }
                return new PointF((float) this.baseDisplayX, (float) this.baseDisplayY);
            }

            public void createJourney(DateTime startTime, DateTime curTime, DateTime endTime)
            {
                this.serverEndTime = endTime;
                double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
                TimeSpan span = (TimeSpan) (curTime - startTime);
                this.localStartTime = num - span.TotalSeconds;
                TimeSpan span2 = (TimeSpan) (endTime - curTime);
                this.localEndTime = num + span2.TotalSeconds;
            }

            public bool isVisible(RectangleF screenRect)
            {
                if ((screenRect.Top - 50f) > this.displayY)
                {
                    return false;
                }
                if ((screenRect.Left - 50f) > this.displayX)
                {
                    return false;
                }
                if ((screenRect.Bottom + 50f) < this.displayY)
                {
                    return false;
                }
                if ((screenRect.Right + 50f) < this.displayX)
                {
                    return false;
                }
                return true;
            }

            public PointF TargetPoint()
            {
                if ((((this.person.state != 0) && (this.person.state != 1)) && ((this.person.state != 2) && (this.person.state != 11))) && (((this.person.state != 12) && (this.person.state != 0x15)) && (((this.person.state != 0x16) && (this.person.state != 0x1f)) && (this.person.state != 0x4b))))
                {
                    return new PointF((float) this.baseDisplayX, (float) this.baseDisplayY);
                }
                return new PointF((float) this.targetDisplayX, (float) this.targetDisplayY);
            }

            public void updatePosition()
            {
                if (((this.person.state == 2) || (this.person.state == 12)) || (this.person.state == 0x16))
                {
                    this.displayX = ((this.targetDisplayX - this.baseDisplayX) * 1.0) + this.baseDisplayX;
                    this.displayY = ((this.targetDisplayY - this.baseDisplayY) * 1.0) + this.baseDisplayY;
                }
                else if (this.person.state > 0)
                {
                    double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
                    double localStartTime = this.localStartTime;
                    double localEndTime = this.localEndTime;
                    double num4 = (num - localStartTime) / (localEndTime - localStartTime);
                    if (num4 > 1.0)
                    {
                        if (((this.person.state == 1) || (this.person.state == 11)) || (this.person.state == 0x15))
                        {
                            num4 = 1.0;
                            this.person.state++;
                        }
                        else if (this.person.state == 50)
                        {
                            num4 = 0.0;
                            this.person.state = 0;
                            this.person.lastSpyTime = VillageMap.getCurrentServerTime();
                        }
                        else if ((this.person.state == 0x1f) || (this.person.state == 0x4b))
                        {
                            this.dying = true;
                            if (this.person.state == 0x4b)
                            {
                                int parishID = GameEngine.Instance.World.getParishFromVillageID(this.person.targetVillageID);
                                GameEngine.Instance.World.givePlaguesToParish(parishID);
                            }
                            GameEngine.Instance.World.clearVillageRolloverInfo(this.person.targetVillageID);
                        }
                    }
                    if (num4 < 0.0)
                    {
                        num4 = 0.0;
                    }
                    this.lastRatio = num4;
                    if (((this.person.state == 1) || (this.person.state == 11)) || (((this.person.state == 0x15) || (this.person.state == 0x1f)) || (this.person.state == 0x4b)))
                    {
                        this.displayX = ((this.targetDisplayX - this.baseDisplayX) * num4) + this.baseDisplayX;
                        this.displayY = ((this.targetDisplayY - this.baseDisplayY) * num4) + this.baseDisplayY;
                    }
                    else if (this.person.state == 50)
                    {
                        this.displayX = ((this.baseDisplayX - this.targetDisplayX) * num4) + this.targetDisplayX;
                        this.displayY = ((this.baseDisplayY - this.targetDisplayY) * num4) + this.targetDisplayY;
                    }
                    else
                    {
                        this.displayX = this.baseDisplayX;
                        this.displayY = this.baseDisplayY;
                    }
                }
                else
                {
                    this.displayX = this.baseDisplayX;
                    this.displayY = this.baseDisplayY;
                }
            }
        }

        public class LocalTrader
        {
            public double baseDisplayX;
            public double baseDisplayY;
            public double displayX;
            public double displayY;
            public bool dying;
            public double lastRatio;
            public double localEndTime;
            public double localStartTime;
            public long parentTrader = -1L;
            public DateTime serverEndTime;
            public double targetDisplayX;
            public double targetDisplayY;
            public MarketTraderData trader;
            public long traderID;

            public PointF BasePoint()
            {
                if (((this.trader.traderState != 0) && (this.trader.traderState != 1)) && ((this.trader.traderState != 3) && (this.trader.traderState != 5)))
                {
                    return new PointF((float) this.targetDisplayX, (float) this.targetDisplayY);
                }
                return new PointF((float) this.baseDisplayX, (float) this.baseDisplayY);
            }

            public void createJourney(DateTime startTime, DateTime curTime, DateTime endTime)
            {
                this.serverEndTime = endTime;
                double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
                TimeSpan span = (TimeSpan) (curTime - startTime);
                this.localStartTime = num - span.TotalSeconds;
                TimeSpan span2 = (TimeSpan) (endTime - curTime);
                this.localEndTime = num + span2.TotalSeconds;
            }

            public bool isVisible(RectangleF screenRect)
            {
                if ((screenRect.Top - 50f) > this.displayY)
                {
                    return false;
                }
                if ((screenRect.Left - 50f) > this.displayX)
                {
                    return false;
                }
                if ((screenRect.Bottom + 50f) < this.displayY)
                {
                    return false;
                }
                if ((screenRect.Right + 50f) < this.displayX)
                {
                    return false;
                }
                return true;
            }

            public PointF TargetPoint()
            {
                if (((this.trader.traderState != 0) && (this.trader.traderState != 1)) && ((this.trader.traderState != 3) && (this.trader.traderState != 5)))
                {
                    return new PointF((float) this.baseDisplayX, (float) this.baseDisplayY);
                }
                return new PointF((float) this.targetDisplayX, (float) this.targetDisplayY);
            }

            public void updatePosition()
            {
                if (this.trader.traderState > 0)
                {
                    double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
                    double num2 = this.localStartTime + 3.0;
                    double num3 = this.localEndTime + 3.0;
                    double num4 = (num - num2) / (num3 - num2);
                    if (num4 < 0.0)
                    {
                        num4 = 0.0;
                    }
                    if (num4 > 1.0)
                    {
                        if (this.trader.traderState == 1)
                        {
                            num4 = 0.0;
                            this.trader.traderState++;
                            double num5 = this.localEndTime - this.localStartTime;
                            this.localStartTime = this.localEndTime;
                            this.localEndTime += num5;
                            this.serverEndTime = this.serverEndTime.AddSeconds(num5);
                            int targetVillageID = this.trader.targetVillageID;
                            VillageMap map = GameEngine.Instance.getVillage(targetVillageID);
                            if (map != null)
                            {
                                map.addResources(this.trader.resource, this.trader.amount);
                            }
                        }
                        else if (this.trader.traderState == 3)
                        {
                            num4 = 0.0;
                            this.trader.traderState++;
                            double num7 = this.localEndTime - this.localStartTime;
                            this.localStartTime = this.localEndTime;
                            this.localEndTime += num7;
                            this.serverEndTime = this.serverEndTime.AddSeconds(num7);
                            int homeVillageID = this.trader.homeVillageID;
                            if (GameEngine.Instance.World.isUserVillage(homeVillageID))
                            {
                                GameEngine.Instance.World.addGold((double) this.trader.goldCost);
                            }
                        }
                        else if ((this.trader.traderState == 2) || (this.trader.traderState == 4))
                        {
                            this.dying = true;
                            num4 = 0.0;
                            this.trader.traderState = 0;
                            VillageMap map2 = GameEngine.Instance.getVillage(this.trader.homeVillageID);
                            if (map2 != null)
                            {
                                map2.addTraders(this.trader.numTraders, this.trader.traderID);
                            }
                        }
                        else if (this.trader.traderState == 5)
                        {
                            num4 = 0.0;
                            this.trader.traderState++;
                            double num9 = this.localEndTime - this.localStartTime;
                            this.localStartTime = this.localEndTime;
                            this.localEndTime += num9;
                            this.serverEndTime = this.serverEndTime.AddSeconds(num9);
                            int num1 = this.trader.targetVillageID;
                        }
                        else if (this.trader.traderState == 6)
                        {
                            this.dying = true;
                            num4 = 0.0;
                            this.trader.traderState = 0;
                            int villageID = this.trader.homeVillageID;
                            VillageMap map3 = GameEngine.Instance.getVillage(villageID);
                            if (map3 != null)
                            {
                                map3.addResources(this.trader.resource, this.trader.amount);
                                map3.addTraders(this.trader.numTraders, this.trader.traderID);
                            }
                        }
                    }
                    if (num4 < 0.0)
                    {
                        num4 = 0.0;
                    }
                    this.lastRatio = num4;
                    if (((this.trader.traderState == 1) || (this.trader.traderState == 3)) || (this.trader.traderState == 5))
                    {
                        this.displayX = ((this.targetDisplayX - this.baseDisplayX) * num4) + this.baseDisplayX;
                        this.displayY = ((this.targetDisplayY - this.baseDisplayY) * num4) + this.baseDisplayY;
                    }
                    else if (((this.trader.traderState == 2) || (this.trader.traderState == 4)) || (this.trader.traderState == 6))
                    {
                        this.displayX = ((this.baseDisplayX - this.targetDisplayX) * num4) + this.targetDisplayX;
                        this.displayY = ((this.baseDisplayY - this.targetDisplayY) * num4) + this.targetDisplayY;
                    }
                    else
                    {
                        this.displayX = this.baseDisplayX;
                        this.displayY = this.baseDisplayY;
                    }
                }
                else
                {
                    this.displayX = this.baseDisplayX;
                    this.displayY = this.baseDisplayY;
                }
            }
        }

        public class MapText
        {
            public bool bordered;
            public bool centered;
            public Color col = ARGBColors.Black;
            public PointF loc;
            public int size = 1;
            public string text = "";
        }

        public class ParishChatComparer : IComparer<Chat_TextEntry>
        {
            public int Compare(Chat_TextEntry x, Chat_TextEntry y)
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
                return y.textID.CompareTo(x.textID);
            }
        }

        public class ParishChatData
        {
            public DateTime m_newestPost = DateTime.MinValue;
            public List<Chat_TextEntry>[] m_pages = new List<Chat_TextEntry>[6];
            public long[] m_readIDs = new long[6];

            public bool addEntry(Chat_TextEntry textEntry)
            {
                List<Chat_TextEntry> list = this.getChatPage(textEntry.roomID);
                if (list == null)
                {
                    return false;
                }
                foreach (Chat_TextEntry entry in list)
                {
                    if (entry.textID == textEntry.textID)
                    {
                        return false;
                    }
                }
                list.Add(textEntry);
                if (textEntry.postedTime > this.m_newestPost)
                {
                    this.m_newestPost = textEntry.postedTime;
                }
                return true;
            }

            public List<Chat_TextEntry> getChatPage(int pageID)
            {
                switch (pageID)
                {
                    case 0:
                        return this.m_pages[0];

                    case 1:
                        return this.m_pages[1];

                    case 2:
                        return this.m_pages[2];

                    case 3:
                        return this.m_pages[3];

                    case 4:
                        return this.m_pages[4];

                    case 5:
                        return this.m_pages[5];
                }
                return null;
            }

            public long getReadID(int pageID)
            {
                return this.m_readIDs[pageID];
            }

            public void init()
            {
                for (int i = 0; i < 6; i++)
                {
                    this.m_pages[i] = new List<Chat_TextEntry>();
                    this.m_readIDs[i] = -1L;
                }
            }

            public void setReadIDs(long[] readIDs)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (this.m_readIDs[i] < readIDs[i])
                    {
                        this.m_readIDs[i] = readIDs[i];
                    }
                }
            }
        }

        public class ParishWallDonateInfo
        {
            public DateTime lastUpdateTime = DateTime.MinValue;
            public ParishWallDetailInfo_ReturnType returnData;
        }

        public class ShieldTextureCacheEntry
        {
            public int index = -1;
            public DateTime lastUsage = DateTime.MinValue;
            public int playerID = -10000;
            public int textureID = -1;
        }

        public class SpecialVillageCache
        {
            public DateTime lastUpdate = DateTime.Now;
            public int resourceLevel;
            public int resourceType;
        }

        public class Triangle
        {
            public float x1;
            public float x2;
            public float x3;
            public float y1;
            public float y2;
            public float y3;
        }

        public class UserVillageData
        {
            public bool capital;
            public bool countryCapital;
            public bool countyCapital;
            public bool parishCapital;
            public bool provinceCapital;
            public List<int> vassals = new List<int>();
            public int villageID;
        }

        public class VillageData
        {
            public bool coastalVillage;
            public int connecter = -1;
            public bool countryCapital;
            public bool countyCapital;
            public int countyID = -1;
            public DateTime excommunicationTime = DateTime.MinValue;
            public int factionID = 1;
            public int id = -1;
            public DateTime interdictionTime = DateTime.MinValue;
            public string m_villageName = "";
            public bool mountainVillage;
            public int numFlags;
            public DateTime peaceTime = DateTime.MinValue;
            public bool provinceCapital;
            public bool regionCapital;
            public int regionID = -1;
            public WorldMap.VillageRolloverInfo rolloverInfo;
            public int special;
            public int userID = 1;
            public bool userRelatedVillage;
            public int userVillageID = -1;
            public bool vacationMode;
            public byte villageInfo;
            public int villageTerrain;
            public bool visible;
            public bool whiteFlags;
            public bool whiteName;
            public int x = -1;
            public int y = -1;

            public bool Capital
            {
                get
                {
                    return (((this.regionCapital | this.countyCapital) | this.provinceCapital) | this.countryCapital);
                }
            }

            public string villageName
            {
                get
                {
                    if (Program.mySettings.viewVillageIDs && !this.Capital)
                    {
                        return ("[" + this.id.ToString() + "] " + this.m_villageName);
                    }
                    if (Program.mySettings.viewCapitalIDs && this.Capital)
                    {
                        return ("[" + this.id.ToString() + "] " + this.m_villageName);
                    }
                    return this.m_villageName;
                }
                set
                {
                    this.m_villageName = value;
                }
            }
        }

        public class VillageNameComparer : IComparer<WorldMap.UserVillageData>
        {
            public int Compare(WorldMap.UserVillageData x, WorldMap.UserVillageData y)
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
                if (x.capital && !y.capital)
                {
                    return 1;
                }
                if (!x.capital && y.capital)
                {
                    return -1;
                }
                string str = GameEngine.Instance.World.getVillageNameOnly(x.villageID);
                string strB = GameEngine.Instance.World.getVillageNameOnly(y.villageID);
                return str.CompareTo(strB);
            }
        }

        public class VillageNameItem
        {
            public bool capital;
            public int sortLevel;
            public int villageID;
            public string villageName;

            public override string ToString()
            {
                if (this.capital && GameEngine.Instance.World.isUserVillage(this.villageID))
                {
                    return (this.villageName + "*");
                }
                return this.villageName;
            }
        }

        public class VillageQuadNode
        {
            public WorldMap.VillageQuadNode m_BL;
            public WorldMap.VillageQuadNode m_BR;
            public List<WorldMap.VillageData> m_capitalList;
            public int m_centreX;
            public int m_centreY;
            public bool m_childrenDisplayed;
            public bool m_drawLevel;
            public int m_height;
            public int m_level;
            public WorldMap.VillageQuadNode m_TL;
            public WorldMap.VillageQuadNode m_TR;
            public List<WorldMap.VillageData> m_villageList;
            public int m_width;
            public int m_x;
            public int m_y;
            public static WorldMap world;

            public VillageQuadNode(int x, int y, int width, int height)
            {
                this.m_x = x;
                this.m_y = y;
                this.m_width = width;
                this.m_height = height;
                this.m_centreX = (width / 2) + x;
                this.m_centreY = (height / 2) + y;
            }

            public void addVillage(WorldMap.VillageData village, int level)
            {
                if (level < 5)
                {
                    if (village.x < this.m_centreX)
                    {
                        if (village.y < this.m_centreY)
                        {
                            if (this.m_TL == null)
                            {
                                this.m_TL = new WorldMap.VillageQuadNode(this.m_x, this.m_y, (this.m_width / 2) + 1, (this.m_height / 2) + 1);
                            }
                            this.m_TL.addVillage(village, level + 1);
                        }
                        else
                        {
                            if (this.m_BL == null)
                            {
                                this.m_BL = new WorldMap.VillageQuadNode(this.m_x, this.m_centreY, (this.m_width / 2) + 1, (this.m_height / 2) + 1);
                            }
                            this.m_BL.addVillage(village, level + 1);
                        }
                    }
                    else if (village.y < this.m_centreY)
                    {
                        if (this.m_TR == null)
                        {
                            this.m_TR = new WorldMap.VillageQuadNode(this.m_centreX, this.m_y, (this.m_width / 2) + 1, (this.m_height / 2) + 1);
                        }
                        this.m_TR.addVillage(village, level + 1);
                    }
                    else
                    {
                        if (this.m_BR == null)
                        {
                            this.m_BR = new WorldMap.VillageQuadNode(this.m_centreX, this.m_centreY, (this.m_width / 2) + 1, (this.m_height / 2) + 1);
                        }
                        this.m_BR.addVillage(village, level + 1);
                    }
                }
                else
                {
                    this.m_drawLevel = true;
                    if (this.m_villageList == null)
                    {
                        this.m_villageList = new List<WorldMap.VillageData>();
                    }
                    this.m_villageList.Add(village);
                    if (village.Capital || (world.aiWorldTreeBuilding && world.aiWorldSpecialVillages.Contains(village.id)))
                    {
                        if (this.m_capitalList == null)
                        {
                            this.m_capitalList = new List<WorldMap.VillageData>();
                        }
                        this.m_capitalList.Add(village);
                    }
                }
            }

            public void drawVillages(WorldMap.FastScreenRect screenRect)
            {
                if (this.m_drawLevel)
                {
                    if (screenRect.zoomLevel >= 8.0)
                    {
                        if (this.m_villageList != null)
                        {
                            if (!GameEngine.Instance.World.mapEditing)
                            {
                                foreach (WorldMap.VillageData data in this.m_villageList)
                                {
                                    if (data.visible)
                                    {
                                        float num = (data.x - screenRect.Left) / screenRect.Width;
                                        float num2 = (data.y - screenRect.Top) / screenRect.Height;
                                        if (((num >= -0.1f) && (num <= 1.1f)) && ((num2 >= -0.1f) && (num2 <= 1.1f)))
                                        {
                                            world.drawVillage(data, (double) num, (double) num2);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (WorldMap.VillageData data2 in this.m_villageList)
                                {
                                    if (data2.visible || data2.Capital)
                                    {
                                        float num3 = (data2.x - screenRect.Left) / screenRect.Width;
                                        float num4 = (data2.y - screenRect.Top) / screenRect.Height;
                                        if (((num3 >= -0.1f) && (num3 <= 1.1f)) && ((num4 >= -0.1f) && (num4 <= 1.1f)))
                                        {
                                            world.drawVillage(data2, (double) num3, (double) num4);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (this.m_capitalList != null)
                    {
                        foreach (WorldMap.VillageData data3 in this.m_capitalList)
                        {
                            if (data3.visible)
                            {
                                float num5 = (data3.x - screenRect.Left) / screenRect.Width;
                                float num6 = (data3.y - screenRect.Top) / screenRect.Height;
                                if (((num5 >= -0.1f) && (num5 <= 1.1f)) && ((num6 >= -0.1f) && (num6 <= 1.1f)))
                                {
                                    world.drawVillage(data3, (double) num5, (double) num6);
                                }
                            }
                        }
                    }
                }
                else if (this.isAreaVisible(screenRect))
                {
                    if (this.m_TL != null)
                    {
                        this.m_TL.drawVillages(screenRect);
                    }
                    if (this.m_TR != null)
                    {
                        this.m_TR.drawVillages(screenRect);
                    }
                    if (this.m_BL != null)
                    {
                        this.m_BL.drawVillages(screenRect);
                    }
                    if (this.m_BR != null)
                    {
                        this.m_BR.drawVillages(screenRect);
                    }
                }
            }

            public bool isAreaVisible(WorldMap.FastScreenRect screenRect)
            {
                if ((screenRect.top - 50) > (this.m_y + this.m_height))
                {
                    return false;
                }
                if ((screenRect.left - 50) > (this.m_x + this.m_width))
                {
                    return false;
                }
                if ((screenRect.bottom + 50) < this.m_y)
                {
                    return false;
                }
                if ((screenRect.right + 50) < this.m_x)
                {
                    return false;
                }
                return true;
            }

            public void setWorld(WorldMap newWorld)
            {
                world = newWorld;
            }
        }

        public class VillageRolloverInfo
        {
            public DateTime interdictionTime = DateTime.MinValue;
            public DateTime lastUpdateTime = DateTime.Now.AddYears(-1);
            public DateTime peaceTime = DateTime.MinValue;
            public int plagueLevel = -1;
            public bool vacationMode;
            public int villageID = -1;
            public string villageName = "";
        }

        public class WorldPoint
        {
            public float x;
            public float y;
        }

        public class WorldPointList
        {
            public string areaName = "";
            public int[] borderList;
            public int capitalVillage = -1;
            public int[] childList;
            public int data;
            public int experimentalColourVariant;
            public int factionID;
            public Point marker = new Point(-1, -1);
            public float maxX = -1E+08f;
            public float maxY = -1E+08f;
            public float minX = 1E+08f;
            public float minY = 1E+08f;
            public int parentID = -1;
            public int plague;
            public bool rebuiltBorderList;
            public WorldMap.WorldPoint[] regionBorderList;
            public WorldMap.Triangle[] triangleList;
            public int userID;

            public PointF getCentrePoint()
            {
                return new PointF { X = ((this.maxX - this.minX) / 2f) + this.minX, Y = ((this.maxY - this.minY) / 2f) + this.minY };
            }

            public bool isVisible(RectangleF screenRect)
            {
                if (this.maxX < screenRect.Left)
                {
                    return false;
                }
                if (this.maxY < screenRect.Top)
                {
                    return false;
                }
                if (this.minX > screenRect.Right)
                {
                    return false;
                }
                if (this.minY > screenRect.Bottom)
                {
                    return false;
                }
                return true;
            }

            public void updateBounds(WorldMap.WorldPoint wp)
            {
                if (wp.x < this.minX)
                {
                    this.minX = wp.x;
                }
                if (wp.y < this.minY)
                {
                    this.minY = wp.y;
                }
                if (wp.x > this.maxX)
                {
                    this.maxX = wp.x;
                }
                if (wp.y > this.maxY)
                {
                    this.maxY = wp.y;
                }
            }

            public void updateBoundsFromTriangles()
            {
                foreach (WorldMap.Triangle triangle in this.triangleList)
                {
                    if (triangle.x1 < this.minX)
                    {
                        this.minX = triangle.x1;
                    }
                    if (triangle.x2 < this.minX)
                    {
                        this.minX = triangle.x2;
                    }
                    if (triangle.x3 < this.minX)
                    {
                        this.minX = triangle.x3;
                    }
                    if (triangle.y1 < this.minY)
                    {
                        this.minY = triangle.y1;
                    }
                    if (triangle.y2 < this.minY)
                    {
                        this.minY = triangle.y2;
                    }
                    if (triangle.y3 < this.minY)
                    {
                        this.minY = triangle.y3;
                    }
                    if (triangle.x1 > this.maxX)
                    {
                        this.maxX = triangle.x1;
                    }
                    if (triangle.x2 > this.maxX)
                    {
                        this.maxX = triangle.x2;
                    }
                    if (triangle.x3 > this.maxX)
                    {
                        this.maxX = triangle.x3;
                    }
                    if (triangle.y1 > this.maxY)
                    {
                        this.maxY = triangle.y1;
                    }
                    if (triangle.y2 > this.maxY)
                    {
                        this.maxY = triangle.y2;
                    }
                    if (triangle.y3 > this.maxY)
                    {
                        this.maxY = triangle.y3;
                    }
                }
            }
        }

        public delegate void WorldZoomCallback(double newWorldZoom, bool redraw);
    }
}

