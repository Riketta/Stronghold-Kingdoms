namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class CastleMap
    {
        private static SparseArray activeCastleInfrastructureElements = new SparseArray();
        private int[] archerAttackFiringDown = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 
            0x18, 0x19, 0x19, 0x19, 0x19, 0x1a, 0x1b, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c
         };
        private int[] archerAttackFiringStraight = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 
            12, 13, 13, 13, 13, 14, 15, 0x10, 0x10, 0x10, 0x10, 0x10
         };
        private int[] archerAttackFiringUp = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 0x1d, 30, 0x1f, 0x20, 0x21, 0x22, 0x23, 
            0x24, 0x25, 0x25, 0x25, 0x25, 0x26, 0x27, 40, 40, 40, 40, 40
         };
        private int[] archerAttackMoat = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        private int[] archerAttackUnit = new int[] { 0, 9, 10, 11, 10, 9 };
        private int[] archerAttackWall = new int[] { 0, 1, 2, 3, 4, 5, 5, 5, 4, 3, 2, 1 };
        private int[] archerBlocked = new int[] { 0, 0, 1, 1, 2, 2, 1, 1 };
        private int[] archerDyingArrow = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17
         };
        private int[] archerDyingNormal = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 30, 0x1f
         };
        private string[] arrow_high_sounds = new string[] { "arrow_high_1", "arrow_high_2", "arrow_high_3", "arrow_high_4", "arrow_high_5", "arrow_high_6", "arrow_high_7", "arrow_high_8", "arrow_high_9", "arrow_high_10" };
        private string[] arrow_low_sounds = new string[] { "arrow_low_1", "arrow_low_2", "arrow_low_3", "arrow_low_4", "arrow_low_5", "arrow_low_6", "arrow_low_7", "arrow_low_8", "arrow_low_9", "arrow_low_10" };
        private string[] arrow_mid_sounds = new string[] { "arrow_med_1", "arrow_med_2", "arrow_med_3", "arrow_med_4", "arrow_med_5", "arrow_med_6", "arrow_med_7", "arrow_med_8", "arrow_med_9", "arrow_med_10" };
        private BattlePlaySFX arrowSounds = new BattlePlaySFX();
        private double attackCapitalAttackRate;
        private int attackCaptainsCommand;
        private bool attackerSetupForest;
        private bool attackerSetupMode;
        private int attackMaxArchers;
        private int attackMaxArchersInCastle;
        private int attackMaxCaptains;
        private int attackMaxCatapults;
        private int attackMaxPeasants;
        private int attackMaxPeasantsInCastle;
        private int attackMaxPikemen;
        private int attackMaxPikemenInCastle;
        private int attackMaxSwordsmen;
        private int attackMaxSwordsmenInCastle;
        private int attackNumArchers;
        private int attackNumCaptains;
        private int attackNumCatapults;
        private int attackNumPeasants;
        private int attackNumPikemen;
        private int attackNumSwordsmen;
        private int attackPillagePercent;
        public int attackRealAttackingVillage = -1;
        private int attackRealAttackType;
        public int attackRealTargetVillage = -1;
        private static SpriteWrapper backgroundSprite = null;
        private string[] ballista_high_sounds = new string[] { "ballista_high_1", "ballista_high_2", "ballista_high_3", "ballista_high_4", "ballista_high_5" };
        private string[] ballista_low_sounds = new string[] { "ballista_low_1", "ballista_low_2", "ballista_low_3", "ballista_low_4", "ballista_low_5" };
        private string[] ballista_mid_sounds = new string[] { "ballista_med_1", "ballista_med_2", "ballista_med_3", "ballista_med_4", "ballista_med_5" };
        private BattlePlaySFX ballistaBoltSounds = new BattlePlaySFX();
        private static DateTime baseServerTime = DateTime.Now;
        private int battleLandType;
        private bool battleMode;
        private Point battleModeMousePos = new Point(-1000, -1000);
        public static int Builder_MapX = 0;
        public static int Builder_MapY = 0;
        private int campMode;
        private int[] captainAttackMoat = new int[] { 
            1, 2, 3, 4, 5, 6, 7, 8, 8, 9, 10, 10, 10, 10, 11, 11, 
            12, 13, 14, 15, 0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x17, 0x17, 0x18, 0x19, 
            0x1a, 0x1b, 0x1c, 0x1d, 30, 0x1f, 0x20, 0x21, 0x22, 0x23, 0x24, 13, 14, 15, 0x10, 0x11, 
            0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x17, 0x17, 0x18
         };
        private int[] captainAttackUnit = new int[] { 
            1, 2, 3, 4, 5, 6, 7, 8, 8, 9, 10, 10, 10, 10, 11, 11, 
            12, 13, 14, 15, 0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x17, 0x17, 0x18, 0x19, 
            0x1a, 0x1b, 0x1c, 0x1d, 30, 0x1f, 0x20, 0x21, 0x22, 0x23, 0x24, 13, 14, 15, 0x10, 0x11, 
            0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x17, 0x17, 0x18
         };
        private int[] captainAttackWall = new int[] { 
            1, 2, 3, 4, 5, 6, 7, 8, 8, 9, 10, 10, 10, 10, 11, 11, 
            12, 13, 14, 15, 0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x17, 0x17, 0x18, 0x19, 
            0x1a, 0x1b, 0x1c, 0x1d, 30, 0x1f, 0x20, 0x21, 0x22, 0x23, 0x24, 13, 14, 15, 0x10, 0x11, 
            0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x17, 0x17, 0x18
         };
        private int[] captainBattleCryAnim = new int[] { 0x14f, 0x14e, 0x14d, 0x14c, 0x14b, 0x14c, 0x14d, 0x14e, 0x14f };
        private int[] captainDyingAnim = new int[] { 
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0x10, 
            0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c
         };
        private int[] captainIdle = new int[] { 
            1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 
            4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 
            4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 
            5, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 3, 3, 
            3, 3, 3, 3, 2, 2, 2, 2, 2
         };
        private List<CaptainsDetails> captainsDetails = new List<CaptainsDetails>();
        public const int CASTLE_MODE_ATTACKER_SETUP = 1;
        public const int CASTLE_MODE_BATTLE = 3;
        public const int CASTLE_MODE_NORMAL = 0;
        public const int CASTLE_MODE_VIEW_SPY_REPORT = 2;
        private static SpriteWrapper[,] castleAttackerSpriteGrid = new SpriteWrapper[0x76, 0x76];
        private CastleCombat castleCombat;
        private bool castleDamaged;
        private static SpriteWrapper[,] castleDefenderSpriteGrid = new SpriteWrapper[0x76, 0x76];
        private static List<SpriteWrapper> castleExtraSprites = new List<SpriteWrapper>();
        private CastleLayout castleLayout;
        private static SpriteWrapper[,] castleSpriteGrid = new SpriteWrapper[0x76, 0x76];
        private static Point[,] castleUnitSpritePoint = new Point[0x76, 0x76];
        private int[] catapultAnim = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b
         };
        private string[] catapultdeath_low_sounds = new string[] { "catapultdeath_1", "catapultdeath_2", "catapulteath_3", "catapultdeath_4", "catapultdeath_5" };
        private BattlePlaySFX catapultDeathSounds = new BattlePlaySFX();
        public List<CatapultLine> catapultLines = new List<CatapultLine>();
        private bool catapultTargetMoveValid;
        private int catapultTargetMoveX;
        private int catapultTargetMoveY;
        private List<CatapultTarget> catapultTargets = new List<CatapultTarget>();
        private Random chipRand = new Random();
        private CastleCommitPopup commitPopup;
        private static bool createMode = false;
        private int currentMoveOriginalX = -1;
        private int currentMoveOriginalY = -1;
        private int debugDisplayMode;
        private bool deleting;
        private long deletingHighlightElementID = -2L;
        private bool deletingTroops;
        public static bool displayCollapsed = true;
        public int displayType;
        private bool draggingWall;
        private SpriteWrapper dummySprite = new SpriteWrapper();
        private int[] dyingOnFire = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 30, 0x1f, 
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26
         };
        private List<CastleElement> elements;
        private int enclosedGlow;
        private static SpriteWrapper enclosedOverlaySprite = new SpriteWrapper();
        private static SpriteWrapper enclosedOverlaySprite2 = new SpriteWrapper();
        private BattleTroopNumbers endingTroopNumbers;
        private bool endOfBattle;
        private static int fakeDefensiveMode = -1;
        private static int fakeKeep = -1;
        private bool fastPlayback;
        private int[] fireEnd = new int[] { 0, 1, 2, 3, 4, 5, 6 };
        private int[] fireLoop = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 30, 0x1f, 
            0x20, 0x21, 0x22
         };
        private int[] fireStart = new int[] { 0, 1, 2, 3, 4, 5, 6 };
        private GraphicsMgr gfx;
        private string[] horsedeath_low_sounds = new string[] { "horsedeath_1", "horsedeath_2", "horsedeath_3", "horsedeath_4", "horsedeath_5" };
        private BattlePlaySFX horseDeathSounds = new BattlePlaySFX();
        private bool inBuilderMode;
        private bool inDeleteConstructing;
        private bool inDeleting;
        private string[] infraStone_high_sounds = new string[] { "infrastructure_stone_high_1", "infrastructure_stone_high_2", "infrastructure_stone_high_3", "infrastructure_stone_high_4", "infrastructure_stone_high_5", "infrastructure_stone_high_6", "infrastructure_stone_high_7", "infrastructure_stone_high_8", "infrastructure_stone_high_9", "infrastructure_stone_high_10" };
        private string[] infraStone_low_sounds = new string[] { "infrastructure_stone_low_1", "infrastructure_stone_low_2", "infrastructure_stone_low_3", "infrastructure_stone_low_4", "infrastructure_stone_low_5", "infrastructure_stone_low_6", "infrastructure_stone_low_7", "infrastructure_stone_low_8", "infrastructure_stone_low_9", "infrastructure_stone_low_10" };
        private string[] infraStone_mid_sounds = new string[] { "infrastructure_stone_med_1", "infrastructure_stone_med_2", "infrastructure_stone_med_3", "infrastructure_stone_med_4", "infrastructure_stone_med_5", "infrastructure_stone_med_6", "infrastructure_stone_med_7", "infrastructure_stone_med_8", "infrastructure_stone_med_9", "infrastructure_stone_med_10" };
        private BattlePlaySFX infraStoneLargeDestroyedSounds = new BattlePlaySFX();
        private BattlePlaySFX infraStoneSmallDestroyedSounds = new BattlePlaySFX();
        private BattlePlaySFX infraStoneSounds = new BattlePlaySFX();
        private string[] infraWood_high_sounds = new string[] { "infrastructure_wood_high_1", "infrastructure_wood_high_2", "infrastructure_wood_high_3", "infrastructure_wood_high_4", "infrastructure_wood_high_5", "infrastructure_wood_high_6", "infrastructure_wood_high_7", "infrastructure_wood_high_8", "infrastructure_wood_high_9", "infrastructure_wood_high_10" };
        private string[] infraWood_low_sounds = new string[] { "infrastructure_wood_low_1", "infrastructure_wood_low_2", "infrastructure_wood_low_3", "infrastructure_wood_low_4", "infrastructure_wood_low_5", "infrastructure_wood_low_6", "infrastructure_wood_low_7", "infrastructure_wood_low_8", "infrastructure_wood_low_9", "infrastructure_wood_low_10" };
        private string[] infraWood_mid_sounds = new string[] { "infrastructure_wood_med_1", "infrastructure_wood_med_2", "infrastructure_wood_med_3", "infrastructure_wood_med_4", "infrastructure_wood_med_5", "infrastructure_wood_med_6", "infrastructure_wood_med_7", "infrastructure_wood_med_8", "infrastructure_wood_med_9", "infrastructure_wood_med_10" };
        private BattlePlaySFX infraWoodDestroyedSounds = new BattlePlaySFX();
        private BattlePlaySFX infraWoodSounds = new BattlePlaySFX();
        private bool inTroopPlacerMode;
        private string[] knight_high_sounds = new string[] { "movingknight_high_1", "movingknight_high_2", "movingknight_high_3", "movingknight_high_4", "movingknight_high_5", "movingknight_high_6" };
        private string[] knight_low_sounds = new string[] { "movingknight_low_1", "movingknight_low_2", "movingknight_low_3", "movingknight_low_4", "movingknight_low_5", "movingknight_low_6" };
        private string[] knight_mid_sounds = new string[] { "movingknight_med_1", "movingknight_med_2", "movingknight_med_3", "movingknight_med_4", "movingknight_med_5", "movingknight_med_6" };
        private int[] knightAttackUnit = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        private int[] knightDyingArrow = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17
         };
        private int[] knightDyingNormal = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17
         };
        private int[] knightHorseIdle = new int[] { 
            0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1, 
            0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 2, 1, 0, 0, 0, 
            0, 1, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 2, 1, 
            0, 0, 0, 0
         };
        private DateTime lastDeleteConstructing = DateTime.MinValue;
        private DateTime lastDeleteTime = DateTime.MinValue;
        public int lastGHX = -1;
        public int lastGHY = -1;
        private int lastMoveTileX = -1;
        private int lastMoveTileY = -1;
        private int lastValidWallX = -1;
        private int lastValidWallY = -1;
        private static double localBaseTime = 0.0;
        private long localTempElementNumber = -3L;
        private CastleResearchData m_attackerResearch;
        private Point m_baseMousePos = new Point();
        private double m_baseScreenX;
        private double m_baseScreenY;
        public BattleHonourData m_battleHonourData;
        private bool m_castleEnclosed;
        private int m_castleMode;
        private CastleResearchData m_defenderResearch;
        private bool m_holdLassoModeAvailable;
        private List<long> m_lassoElements = new List<long>();
        private int m_lassoEndX;
        private int m_lassoEndY;
        private int m_lassoLastX;
        private int m_lassoLastY;
        private bool m_lassoLeftHeldDown;
        private bool m_lassoMade;
        private int m_lassoStartX;
        private int m_lassoStartY;
        private double m_lastMousePressedTime;
        private bool m_leftMouseGrabbed;
        private bool m_leftMouseHeldDown;
        private int m_nextCaptainBattleSound = -1000000;
        private int m_nextCaptainDelaySound = -1000000;
        private int m_nextCaptainRallySound = -1000000;
        private int m_nextKnightSound = -1000000;
        private int m_nextWolfSound = -1000000;
        private GetReport_ReturnType m_reportReturnData;
        public int m_targetUserID = -1;
        public string m_targetUserName = "";
        private bool m_usingCastleTroopsOK;
        private int m_villageID = -1;
        private string[] meleeLight_high_sounds = new string[] { "melee_light_high_1", "melee_light_high_2", "melee_light_high_3", "melee_light_high_4", "melee_light_high_5", "melee_light_high_6", "melee_light_high_7", "melee_light_high_8", "melee_light_high_9", "melee_light_high_10" };
        private string[] meleeLight_low_sounds = new string[] { "melee_light_low_1", "melee_light_low_2", "melee_light_low_3", "melee_light_low_4", "melee_light_low_5", "melee_light_low_6", "melee_light_low_7", "melee_light_low_8", "melee_light_low_9", "melee_light_low_10" };
        private string[] meleeLight_mid_sounds = new string[] { "melee_light_med_1", "melee_light_med_2", "melee_light_med_3", "melee_light_med_4", "melee_light_med_5", "melee_light_med_6", "melee_light_med_7", "melee_light_med_8", "melee_light_med_9", "melee_light_med_10" };
        private BattlePlaySFX meleeLightSounds = new BattlePlaySFX();
        private string[] meleeMetal_high_sounds = new string[] { "melee_metal_high_1", "melee_metal_high_2", "melee_metal_high_3", "melee_metal_high_4", "melee_metal_high_5", "melee_metal_high_6", "melee_metal_high_7", "melee_metal_high_8", "melee_metal_high_9", "melee_metal_high_10" };
        private string[] meleeMetal_low_sounds = new string[] { "melee_metal_low_1", "melee_metal_low_2", "melee_metal_low_3", "melee_metal_low_4", "melee_metal_low_5", "melee_metal_low_6", "melee_metal_low_7", "melee_metal_low_8", "melee_metal_low_9", "melee_metal_low_10" };
        private string[] meleeMetal_mid_sounds = new string[] { "melee_metal_med_1", "melee_metal_med_2", "melee_metal_med_3", "melee_metal_med_4", "melee_metal_med_5", "melee_metal_med_6", "melee_metal_med_7", "melee_metal_med_8", "melee_metal_med_9", "melee_metal_med_10" };
        private BattlePlaySFX meleeMetalSounds = new BattlePlaySFX();
        public int[] moatSurroundLogic = new int[] { 
            0x10c, 2, 0, 2, 1, 0, 1, 1, 2, 0x10d, 1, 1, 2, 1, 0, 2, 
            0, 2, 270, 2, 1, 1, 0, 1, 2, 0, 2, 0x10f, 2, 0, 2, 0, 
            1, 2, 1, 1, 0x110, 1, 1, 2, 1, 0, 1, 1, 2, 0x111, 1, 1, 
            1, 1, 1, 2, 0, 2, 0x112, 2, 1, 1, 0, 1, 2, 1, 1, 0x113, 
            2, 0, 2, 1, 1, 1, 1, 1, 0x114, 1, 1, 1, 1, 1, 1, 1, 
            1, 0x115, 1, 1, 1, 1, 1, 0, 1, 1, 0x116, 0, 1, 1, 1, 1, 
            1, 1, 1, 0x117, 1, 1, 0, 1, 1, 1, 1, 1, 280, 1, 1, 1, 
            1, 1, 1, 1, 0, 0x119, 2, 1, 2, 0, 0, 2, 1, 2, 0x11a, 2, 
            0, 2, 1, 1, 2, 0, 2, 0x11b, 0, 1, 0, 1, 1, 0, 1, 0, 
            0x11c, 0, 1, 2, 1, 0, 2, 0, 2, 0x11d, 2, 0, 2, 0, 1, 2, 
            1, 0, 0x11e, 2, 1, 0, 0, 1, 2, 0, 2, 0x11f, 2, 0, 2, 1, 
            0, 0, 1, 2, 0x120, 2, 0, 2, 0, 0, 2, 0, 2, 0x121, 0, 1, 
            2, 1, 0, 0, 1, 2, 290, 2, 0, 2, 1, 1, 0, 1, 0, 0x123, 
            0, 1, 0, 1, 1, 2, 0, 2, 0x124, 2, 1, 0, 0, 1, 2, 1, 
            0, 0x125, 2, 0, 2, 1, 0, 2, 0, 2, 0x126, 2, 0, 2, 0, 0, 
            2, 1, 2, 0x127, 2, 1, 2, 0, 0, 2, 0, 2, 0x128, 2, 0, 2, 
            0, 1, 2, 0, 2, 0x129, 2, 0, 2, 1, 1, 1, 1, 0, 0x12a, 0, 
            1, 2, 1, 0, 1, 1, 2, 0x12b, 1, 1, 0, 1, 1, 2, 0, 2, 
            300, 1, 1, 2, 1, 0, 0, 1, 2, 0x12d, 0, 1, 1, 1, 1, 2, 
            0, 2, 0x12e, 2, 1, 1, 0, 1, 2, 1, 0, 0x12f, 2, 1, 0, 0, 
            1, 2, 1, 1, 0x130, 2, 0, 2, 1, 1, 0, 1, 1, 0x131, 1, 1, 
            0, 1, 1, 1, 1, 0, 0x132, 1, 1, 1, 1, 1, 0, 1, 0, 0x133, 
            0, 1, 1, 1, 1, 0, 1, 1, 0x134, 0, 1, 0, 1, 1, 1, 1, 
            1, 0x135, 1, 1, 0, 1, 1, 0, 1, 1, 310, 0, 1, 1, 1, 1, 
            1, 1, 0, 0x137, 0, 1, 0, 1, 1, 0, 1, 1, 0x138, 0, 1, 0, 
            1, 1, 1, 1, 0, 0x139, 1, 1, 0, 1, 1, 0, 1, 0, 0x13a, 0, 
            1, 1, 1, 1, 0, 1, 0, -1
         };
        public int[] moatSurroundTests = new int[] { -1, -1, 0, -1, 1, -1, -1, 0, 1, 0, -1, 1, 0, 1, 1, 1 };
        private List<CastleElement> movedElements;
        private List<CastleElement> movedElementsOriginal;
        private int nextExtraSpriteID;
        private int nextWallCacheSpriteID;
        private int numAvailableDefenderArchers;
        private int numAvailableDefenderCaptains;
        private int numAvailableDefenderPeasants;
        private int numAvailableDefenderPikemen;
        private int numAvailableDefenderSwordsmen;
        private int numAvailableReinforceDefenderArchers;
        private int numAvailableReinforceDefenderPeasants;
        private int numAvailableReinforceDefenderPikemen;
        private int numAvailableReinforceDefenderSwordsmen;
        private int numAvailableVassalReinforceDefenderArchers;
        private int numAvailableVassalReinforceDefenderPeasants;
        private int numAvailableVassalReinforceDefenderPikemen;
        private int numAvailableVassalReinforceDefenderSwordsmen;
        private static int numClickAreas = 0;
        private int numGuardHouses;
        private int numGuardHouseSpaces;
        private int numPlacedDefenderArchers;
        private int numPlacedDefenderCaptains;
        private int numPlacedDefenderPeasants;
        private int numPlacedDefenderPikemen;
        private int numPlacedDefenderSwordsmen;
        private int numPlacedReinforceDefenderArchers;
        private int numPlacedReinforceDefenderPeasants;
        private int numPlacedReinforceDefenderPikemen;
        private int numPlacedReinforceDefenderSwordsmen;
        private int numPlacedVassalReinforceDefenderArchers;
        private int numPlacedVassalReinforceDefenderPeasants;
        private int numPlacedVassalReinforceDefenderPikemen;
        private int numPlacedVassalReinforceDefenderSwordsmen;
        private int numPots;
        private int numSmelter;
        private int numSmelterPlaces;
        private string[] oil_low_sounds = new string[] { "oil_single_1", "oil_single_2", "oil_single_3", "oil_single_4", "oil_single_5" };
        private string[] oil_mid_sounds = new string[] { "oil_several_1", "oil_several_2", "oil_several_3", "oil_several_4", "oil_several_5" };
        private BattlePlaySFX oilSounds = new BattlePlaySFX();
        private string[] openpits_high_sounds = new string[] { "openpits_high_1", "openpits_high_2", "openpits_high_3", "openpits_high_4", "openpits_high_5", "openpits_high_6" };
        private string[] openpits_low_sounds = new string[] { "openpits_low_1", "openpits_low_2", "openpits_low_3", "openpits_low_4", "openpits_low_5", "openpits_low_6" };
        private string[] openpits_mid_sounds = new string[] { "openpits_med_1", "openpits_med_2", "openpits_med_3", "openpits_med_4", "openpits_med_5", "openpits_med_6" };
        private BattlePlaySFX openPitsSounds = new BattlePlaySFX();
        private bool overWikiHelp;
        public int ParentOfAttackingVillage = -1;
        private int[] peasantAttack = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        private int[] peasantAttackMoat = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
        private int[] peasantBlocked = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 
            2, 1
         };
        private int[] peasantDyingArrow = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17
         };
        private int[] peasantDyingNormal = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17
         };
        private int[] peasantIdle = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 
            2, 1
         };
        private int[] pikemanAttackChop = new int[] { 4, 5, 5, 6, 6, 7, 7, 7, 7, 7, 7, 2, 1, 0, 0 };
        private int[] pikemanAttackJab = new int[] { 0, 1, 2, 3, 3, 3, 3, 2, 2, 1, 1, 0, 0, 0, 0 };
        private int[] pikemanAttackJabQuick = new int[] { 1, 2, 3, 3, 2, 1, 0 };
        private int[] pikemanAttackMoat = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
        private int[] pikemanBlocked = new int[] { 0, 0, 1, 1, 2, 2, 1, 1 };
        private int[] pikemanDyingArrow = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17
         };
        private int[] pikemanDyingNormal = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17
         };
        private int[] pikemanIdle = new int[] { 
            0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 2, 2, 2, 1, 
            1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0
         };
        private int placementSize = 1;
        private static SpriteWrapper placementSprite = null;
        private static SpriteWrapper placementSprite_cancel = null;
        private static SpriteWrapper placementSprite_confirm = null;
        private static SpriteWrapper placementSprite_handleone = null;
        private static SpriteWrapper placementSprite_handletwo = null;
        private static SpriteWrapper[] placementTroopCastleSprite = new SpriteWrapper[0x19];
        private static SpriteWrapper[] placementTroopSprite = new SpriteWrapper[0x19];
        public static int placementType = 0;
        private static List<SpriteWrapper> placementWallSprites = new List<SpriteWrapper>();
        private bool placingAttackerRealMode;
        private static bool placingDefender = true;
        private static bool placingElement = true;
        private static bool placingReinforcement = false;
        private int pulse;
        private int pulseValue;
        private const double RAD2DEG = 57.2957795;
        private bool realBattleMode = true;
        private List<CastleElement> removedElements;
        private List<RockChip> rockChips = new List<RockChip>();
        private string[] rockfired_high_sounds = new string[] { "rockfired_high_1", "rockfired_high_2", "rockfired_high_3", "rockfired_high_4", "rockfired_high_5", "rockfired_high_6", "rockfired_high_7", "rockfired_high_8", "rockfired_high_9", "rockfired_high_10" };
        private string[] rockfired_low_sounds = new string[] { "rockfired_low_1", "rockfired_low_2", "rockfired_low_3", "rockfired_low_4", "rockfired_low_5", "rockfired_low_6", "rockfired_low_7", "rockfired_low_8", "rockfired_low_9", "rockfired_low_10" };
        private string[] rockfired_mid_sounds = new string[] { "rockfired_med_1", "rockfired_med_2", "rockfired_med_3", "rockfired_med_4", "rockfired_med_5", "rockfired_med_6", "rockfired_med_7", "rockfired_med_8", "rockfired_med_9", "rockfired_med_10" };
        private BattlePlaySFX rockFirstSounds = new BattlePlaySFX();
        private string[] rockhit_high_sounds = new string[] { "rockhit_high_1", "rockhit_high_2", "rockhit_high_3", "rockhit_high_4", "rockhit_high_5", "rockhit_high_6", "rockhit_high_7", "rockhit_high_8", "rockhit_high_9", "rockhit_high_10" };
        private string[] rockhit_low_sounds = new string[] { "rockhit_low_1", "rockhit_low_2", "rockhit_low_3", "rockhit_low_4", "rockhit_low_5", "rockhit_low_6", "rockhit_low_7", "rockhit_low_8", "rockhit_low_9", "rockhit_low_10" };
        private string[] rockhit_mid_sounds = new string[] { "rockhit_med_1", "rockhit_med_2", "rockhit_med_3", "rockhit_med_4", "rockhit_med_5", "rockhit_med_6", "rockhit_med_7", "rockhit_med_8", "rockhit_med_9", "rockhit_med_10" };
        private BattlePlaySFX rockHitSounds = new BattlePlaySFX();
        private string[] rockland_high_sounds = new string[] { "rockland_high_1", "rockland_high_2", "rockland_high_3", "rockland_high_4", "rockland_high_5", "rockland_high_6", "rockland_high_7", "rockland_high_8", "rockland_high_9", "rockland_high_10" };
        private string[] rockland_low_sounds = new string[] { "rockland_low_1", "rockland_low_2", "rockland_low_3", "rockland_low_4", "rockland_low_5", "rockland_low_6", "rockland_low_7", "rockland_low_8", "rockland_low_9", "rockland_low_10" };
        private string[] rockland_mid_sounds = new string[] { "rockland_med_1", "rockland_med_2", "rockland_med_3", "rockland_med_4", "rockland_med_5", "rockland_med_6", "rockland_med_7", "rockland_med_8", "rockland_med_9", "rockland_med_10" };
        private BattlePlaySFX rockLandSounds = new BattlePlaySFX();
        private List<RockSmoke> rockSmoke = new List<RockSmoke>();
        private long selectedCatapult = -1L;
        protected Random sfxRandom = new Random();
        private bool showCatapultTargets;
        private bool spreadTypeDiamond = true;
        private static bool spritesInitiated = false;
        private BattleTroopNumbers startingTroopNumbers;
        private int startWallMapX = -1;
        private int startWallMapY = -1;
        private string[] stonelargedestroyed_high_sounds = new string[] { "stonelargedestroyed_high_1", "stonelargedestroyed_high_2", "stonelargedestroyed_high_3", "stonelargedestroyed_high_4" };
        private string[] stonelargedestroyed_low_sounds = new string[] { "stonelargedestroyed_low_1", "stonelargedestroyed_low_2", "stonelargedestroyed_low_3", "stonelargedestroyed_low_4" };
        private string[] stonelargedestroyed_mid_sounds = new string[] { "stonelargedestroyed_med_1", "stonelargedestroyed_med_2", "stonelargedestroyed_med_3", "stonelargedestroyed_med_4" };
        private string[] stonesmalldestroyed_high_sounds = new string[] { "stonesmalldestroyed_high_1", "stonesmalldestroyed_high_2", "stonesmalldestroyed_high_3", "stonesmalldestroyed_high_4", "stonesmalldestroyed_high_5", "stonesmalldestroyed_high_6" };
        private string[] stonesmalldestroyed_low_sounds = new string[] { "stonesmalldestroyed_low_1", "stonesmalldestroyed_low_2", "stonesmalldestroyed_low_3", "stonesmalldestroyed_low_4", "stonesmalldestroyed_low_5", "stonesmalldestroyed_low_6" };
        private string[] stonesmalldestroyed_mid_sounds = new string[] { "stonesmalldestroyed_med_1", "stonesmalldestroyed_med_2", "stonesmalldestroyed_med_3", "stonesmalldestroyed_med_4", "stonesmalldestroyed_med_5", "stonesmalldestroyed_med_6" };
        private bool stopPlacementOnTroopModeSwap;
        private static List<SpriteWrapper> surroundsprites = new List<SpriteWrapper>();
        private int[] swordsmanAttackMoat = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
        private int[] swordsmanAttackUnit = new int[] { 0, 1, 2, 3, 4, 5, 6, 6, 6, 7, 8, 9, 10, 11 };
        private int[] swordsmanAttackWall = new int[] { 0, 1, 2, 3, 3, 4, 5, 6, 7, 8, 8, 8, 9, 10, 11 };
        private int[] swordsmanBlocked = new int[] { 0, 0, 1, 1, 2, 2, 1, 1 };
        private int[] swordsmanDyingArrow = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17
         };
        private int[] swordsmanDyingNormal = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17
         };
        private static SpriteWrapper TCWarningSprite = null;
        private static byte[] tempCompressedAttackerMap;
        private TempTileSortComparer tempTileSortComparer = new TempTileSortComparer();
        private int tick;
        private bool treasureCastle;
        private int treasureCastleClock;
        private static List<TroopClickArea> troopClickAreas = new List<TroopClickArea>();
        private string[] troopdeath_high_sounds = new string[] { 
            "troopdeath_low_1", "troopdeath_low_2", "troopdeath_low_3", "troopdeath_low_4", "troopdeath_low_5", "troopdeath_low_6", "troopdeath_low_7", "troopdeath_low_8", "troopdeath_low_9", "troopdeath_low_10", "troopdeath_med_1", "troopdeath_med_2", "troopdeath_med_3", "troopdeath_med_4", "troopdeath_med_5", "troopdeath_med_6", 
            "troopdeath_med_7", "troopdeath_med_8", "troopdeath_med_9", "troopdeath_med_10", "troopdeath_high_1", "troopdeath_high_2", "troopdeath_high_3", "troopdeath_high_4", "troopdeath_high_5", "troopdeath_high_6", "troopdeath_high_7", "troopdeath_high_8", "troopdeath_high_9", "troopdeath_high_10"
         };
        private string[] troopdeath_low_sounds = new string[] { 
            "troopdeath_low_1", "troopdeath_low_2", "troopdeath_low_3", "troopdeath_low_4", "troopdeath_low_5", "troopdeath_low_6", "troopdeath_low_7", "troopdeath_low_8", "troopdeath_low_9", "troopdeath_low_10", "troopdeath_med_1", "troopdeath_med_2", "troopdeath_med_3", "troopdeath_med_4", "troopdeath_med_5", "troopdeath_med_6", 
            "troopdeath_med_7", "troopdeath_med_8", "troopdeath_med_9", "troopdeath_med_10", "troopdeath_high_1", "troopdeath_high_2", "troopdeath_high_3", "troopdeath_high_4", "troopdeath_high_5", "troopdeath_high_6", "troopdeath_high_7", "troopdeath_high_8", "troopdeath_high_9", "troopdeath_high_10"
         };
        private string[] troopdeath_mid_sounds = new string[] { 
            "troopdeath_low_1", "troopdeath_low_2", "troopdeath_low_3", "troopdeath_low_4", "troopdeath_low_5", "troopdeath_low_6", "troopdeath_low_7", "troopdeath_low_8", "troopdeath_low_9", "troopdeath_low_10", "troopdeath_med_1", "troopdeath_med_2", "troopdeath_med_3", "troopdeath_med_4", "troopdeath_med_5", "troopdeath_med_6", 
            "troopdeath_med_7", "troopdeath_med_8", "troopdeath_med_9", "troopdeath_med_10", "troopdeath_high_1", "troopdeath_high_2", "troopdeath_high_3", "troopdeath_high_4", "troopdeath_high_5", "troopdeath_high_6", "troopdeath_high_7", "troopdeath_high_8", "troopdeath_high_9", "troopdeath_high_10"
         };
        private string[] troopdeathonfire_low_sounds = new string[] { "troopdeathonfire_low_1", "troopdeathonfire_low_2", "troopdeathonfire_low_3", "troopdeathonfire_low_4", "troopdeathonfire_med_1", "troopdeathonfire_med_2", "troopdeathonfire_med_3", "troopdeathonfire_med_4", "troopdeathonfire_high_1", "troopdeathonfire_high_2", "troopdeathonfire_high_3", "troopdeathonfire_high_4" };
        private BattlePlaySFX troopDeathOnFireSounds = new BattlePlaySFX();
        private BattlePlaySFX troopDeathSounds = new BattlePlaySFX();
        private long troopMovingElemID = -2L;
        private bool troopMovingMode;
        private DateTime troopSelectDoubleClickTIme = DateTime.MinValue;
        private long troopSelected = -1L;
        private static SpriteWrapper tutorialOverlaySprite = new SpriteWrapper();
        private int updates;
        private bool waitingForWallReturn;
        private static List<SpriteWrapper> wallCachedSprites = new List<SpriteWrapper>();
        private bool wallWasValid;
        private static SpriteWrapper wikiHelpSprite = new SpriteWrapper();
        private int[] wolfAttackUnit = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b
         };
        private string[] wolfdeath_low_sounds = new string[] { "wolfdeath_1", "wolfdeath_2", "wolfdeath_3", "wolfdeath_4", "wolfdeath_5" };
        private BattlePlaySFX wolfDeathSounds = new BattlePlaySFX();
        private int[] wolfDyingArrow = new int[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17
         };
        private int[] wolfDyingNormal = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
        private string[] wolves_high_sounds = new string[] { "wolfhowl_high_1", "wolfhowl_high_2", "wolfhowl_high_3", "wolfhowl_high_4", "wolfhowl_high_5", "wolfhowl_high_6", "wolfhowl_high_7", "wolfhowl_high_8", "wolfhowl_high_9", "wolfhowl_high_10" };
        private string[] wolves_low_sounds = new string[] { "wolfhowl_low_1", "wolfhowl_low_2", "wolfhowl_low_3", "wolfhowl_low_4", "wolfhowl_low_5", "wolfhowl_low_6", "wolfhowl_low_7", "wolfhowl_low_8", "wolfhowl_low_9", "wolfhowl_low_10" };
        private string[] wolves_mid_sounds = new string[] { "wolfhowl_med_1", "wolfhowl_med_2", "wolfhowl_med_3", "wolfhowl_med_4", "wolfhowl_med_5", "wolfhowl_med_6", "wolfhowl_med_7", "wolfhowl_med_8", "wolfhowl_med_9", "wolfhowl_med_10" };
        private string[] wooddestroyed_high_sounds = new string[] { "wooddestroyed_high_1", "wooddestroyed_high_2", "wooddestroyed_high_3", "wooddestroyed_high_4", "wooddestroyed_high_5", "wooddestroyed_high_6" };
        private string[] wooddestroyed_low_sounds = new string[] { "wooddestroyed_low_1", "wooddestroyed_low_2", "wooddestroyed_low_3", "wooddestroyed_low_4", "wooddestroyed_low_5", "wooddestroyed_low_6" };
        private string[] wooddestroyed_mid_sounds = new string[] { "wooddestroyed_med_1", "wooddestroyed_med_2", "wooddestroyed_med_3", "wooddestroyed_med_4", "wooddestroyed_med_5", "wooddestroyed_med_6" };

        public CastleMap(int villageID, GraphicsMgr mgr, int mode)
        {
            fakeKeep = -1;
            this.m_castleMode = mode;
            this.m_villageID = villageID;
            this.gfx = mgr;
            if (!spritesInitiated)
            {
                List<TempTileSortClass> list = new List<TempTileSortClass>();
                List<TempTileSortClass> list2 = new List<TempTileSortClass>();
                for (int i = 0; i < 0x76; i++)
                {
                    for (int j = 0; j < 0x76; j++)
                    {
                        int num3 = ((i * 0x10) + (j * 0x10)) - 0x39a;
                        int num4 = ((j * 8) - (i * 8)) + 0x1da;
                        if (((num3 >= -48) && (num4 >= -24)) && ((num3 < 0x7a0) && (num4 < 0x3d0)))
                        {
                            if (((i >= 0x1d) && (j >= 0x1d)) && ((i < 0x59) && (j < 0x59)))
                            {
                                TempTileSortClass item = new TempTileSortClass {
                                    gx = i,
                                    gy = j,
                                    sx = num3,
                                    sy = num4
                                };
                                list.Add(item);
                            }
                            else
                            {
                                TempTileSortClass class3 = new TempTileSortClass {
                                    gx = i,
                                    gy = j,
                                    sx = num3,
                                    sy = num4
                                };
                                list2.Add(class3);
                            }
                        }
                    }
                }
                backgroundSprite = new SpriteWrapper();
                backgroundSprite.TextureID = GFXLibrary.Instance.CastleBackgroundTexID;
                backgroundSprite.Initialize(this.gfx);
                backgroundSprite.SpriteNo = 0;
                backgroundSprite.PosX = (int) (0f - ((backgroundSprite.Width - InterfaceMgr.Instance.ParentMainWindow.getDXBasePanel().Width) / 2f));
                backgroundSprite.PosY = (int) (0f - ((backgroundSprite.Height - InterfaceMgr.Instance.ParentMainWindow.getDXBasePanel().Height) / 2f));
                backgroundSprite.Scale = 1f;
                this.createSurroundSprites();
                list.Sort(this.tempTileSortComparer);
                foreach (TempTileSortClass class4 in list)
                {
                    castleSpriteGrid[class4.gx, class4.gy] = new SpriteWrapper();
                    castleSpriteGrid[class4.gx, class4.gy].TextureID = GFXLibrary.Instance.CastleSpritesTexID;
                    castleSpriteGrid[class4.gx, class4.gy].Initialize(this.gfx);
                    castleSpriteGrid[class4.gx, class4.gy].PosX = class4.sx + 0x10;
                    castleSpriteGrid[class4.gx, class4.gy].PosY = class4.sy + 8;
                    castleSpriteGrid[class4.gx, class4.gy].Center = new PointF(16f, 8f);
                    castleSpriteGrid[class4.gx, class4.gy].SpriteNo = 0;
                    castleSpriteGrid[class4.gx, class4.gy].Visible = false;
                    castleDefenderSpriteGrid[class4.gx, class4.gy] = new SpriteWrapper();
                    castleDefenderSpriteGrid[class4.gx, class4.gy].TextureID = GFXLibrary.Instance.CastleSpritesTexID;
                    castleDefenderSpriteGrid[class4.gx, class4.gy].Initialize(this.gfx);
                    castleDefenderSpriteGrid[class4.gx, class4.gy].PosX = class4.sx + 0x10;
                    castleDefenderSpriteGrid[class4.gx, class4.gy].PosY = class4.sy + 8;
                    (castleUnitSpritePoint[class4.gx, class4.gy]) = new Point(class4.sx + 0x10, class4.sy + 8);
                    castleDefenderSpriteGrid[class4.gx, class4.gy].Center = new PointF(50f, 66f);
                    castleDefenderSpriteGrid[class4.gx, class4.gy].SpriteNo = 0;
                    castleDefenderSpriteGrid[class4.gx, class4.gy].Visible = false;
                }
                list2.Sort(this.tempTileSortComparer);
                foreach (TempTileSortClass class5 in list2)
                {
                    castleSpriteGrid[class5.gx, class5.gy] = new SpriteWrapper();
                    castleSpriteGrid[class5.gx, class5.gy].TextureID = GFXLibrary.Instance.CastleSpritesTexID;
                    castleSpriteGrid[class5.gx, class5.gy].Initialize(this.gfx);
                    castleSpriteGrid[class5.gx, class5.gy].PosX = class5.sx + 0x10;
                    castleSpriteGrid[class5.gx, class5.gy].PosY = class5.sy + 8;
                    castleSpriteGrid[class5.gx, class5.gy].Center = new PointF(16f, 8f);
                    castleSpriteGrid[class5.gx, class5.gy].SpriteNo = 0;
                    castleSpriteGrid[class5.gx, class5.gy].Visible = false;
                    castleAttackerSpriteGrid[class5.gx, class5.gy] = new SpriteWrapper();
                    castleAttackerSpriteGrid[class5.gx, class5.gy].TextureID = GFXLibrary.Instance.CastleSpritesTexID;
                    castleAttackerSpriteGrid[class5.gx, class5.gy].Initialize(this.gfx);
                    castleAttackerSpriteGrid[class5.gx, class5.gy].PosX = class5.sx + 0x10;
                    castleAttackerSpriteGrid[class5.gx, class5.gy].PosY = class5.sy + 8;
                    (castleUnitSpritePoint[class5.gx, class5.gy]) = new Point(class5.sx + 0x10, class5.sy + 8);
                    castleAttackerSpriteGrid[class5.gx, class5.gy].Center = new PointF(50f, 66f);
                    castleAttackerSpriteGrid[class5.gx, class5.gy].SpriteNo = 0;
                    castleAttackerSpriteGrid[class5.gx, class5.gy].Visible = false;
                }
                spritesInitiated = true;
            }
        }

        public void addCatapultTargetLine(int sx, int sy, int ex, int ey)
        {
            CatapultLine item = new CatapultLine {
                startX = sx,
                startY = sy,
                endX = ex,
                endY = ey
            };
            this.catapultLines.Add(item);
        }

        private void addFakeWallSprite(int sx, int sy)
        {
            CastleElement item = new CastleElement {
                elementID = this.localTempElementNumber
            };
            this.localTempElementNumber -= 1L;
            if (placementType == 0x42)
            {
                item.elementType = 0x21;
            }
            else if (placementType == 0x41)
            {
                item.elementType = 0x22;
            }
            else
            {
                item.elementType = (byte) placementType;
            }
            item.xPos = (byte) sx;
            item.yPos = (byte) sy;
            this.elements.Add(item);
        }

        public void addNewCaptainDetails(CastleElement element)
        {
            CaptainsDetails item = new CaptainsDetails {
                elemID = element.elementID,
                seconds = 5
            };
            this.captainsDetails.Add(item);
        }

        public void addNewCatapultTargetDefault(CastleElement element)
        {
            CatapultTarget item = new CatapultTarget {
                elemID = element.elementID
            };
            item.createDefaultLocation(element.xPos, element.yPos, element);
            this.catapultTargets.Add(item);
        }

        private void addNoBuildTile(int x, int y)
        {
            Rectangle rectangle;
            PointF tf;
            SpriteWrapper child = castleSpriteGrid[x, y];
            SizeF realSize = new SizeF(0f, 0f);
            child.Visible = true;
            child.ColorToUse = Color.FromArgb(0x40, ARGBColors.Black);
            PointF tf2 = new PointF(16f, 0f);
            float num = 8f;
            int spriteTag = 1;
            this.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref spriteTag).GetSpriteXYdata(spriteTag, 0x114, out rectangle, out tf, out realSize);
            tf2.Y = ((int) realSize.Height) - num;
            child.SpriteNo = 0x114;
            child.Center = tf2;
            backgroundSprite.AddChild(child, 2);
        }

        private void addRockChips(int posX, int posY, bool black)
        {
            RockChip item = new RockChip();
            Point point = (castleUnitSpritePoint[posX, posY]);
            item.xPos = point.X;
            item.yPos = point.Y;
            item.height = 1f;
            item.vVelocity = (100 + this.chipRand.Next(0x19)) / 5;
            item.gravityValue = 2.55112f;
            item.dx = this.chipRand.Next(-50, 50);
            item.dy = this.chipRand.Next(-50, 50);
            float num = ((float) Math.Sqrt((double) ((item.dx * item.dx) + (item.dy * item.dy)))) / 1.3f;
            item.dx /= num;
            item.dy /= num;
            item.image = this.chipRand.Next(8);
            item.black = black;
            this.rockChips.Add(item);
        }

        private void addRockSmoke(float xPos, float yPos, bool black)
        {
            RockSmoke item = new RockSmoke {
                xPos = xPos,
                yPos = yPos,
                animFrame = 2,
                black = black
            };
            this.rockSmoke.Add(item);
        }

        private void addTCWarningMessage()
        {
        }

        private void addWallConstructionToMap(int mapX, int mapY, int placementType)
        {
            CastleElement tempElement = null;
            bool forceInvalid = false;
            int testType = this.correctPlacementType(placementType);
            if (((placementType == 0x24) || (placementType == 0x23)) || ((placementType == 0x41) || (placementType == 0x42)))
            {
                int startWallMapX = this.startWallMapX;
                int startWallMapY = this.startWallMapY;
                int num4 = mapX;
                int num5 = mapY;
                if (startWallMapX > num4)
                {
                    int num6 = startWallMapX;
                    startWallMapX = num4;
                    num4 = num6;
                }
                if (startWallMapY > num5)
                {
                    int num7 = startWallMapY;
                    startWallMapY = num5;
                    num5 = num7;
                }
                List<CastleElement> newElements = new List<CastleElement>();
                for (int i = startWallMapY; i <= num5; i++)
                {
                    for (int k = startWallMapX; k <= num4; k++)
                    {
                        if (!this.testWallSprite(k, i, out tempElement, testType))
                        {
                            forceInvalid = true;
                        }
                        else if (tempElement != null)
                        {
                            newElements.Add(tempElement);
                        }
                    }
                }
                if ((this.castleLayout == null) || this.castleLayout.isCastleEnclosed(null, newElements))
                {
                    forceInvalid = true;
                }
                if (placementType == 0x23)
                {
                    int num10 = this.countMoat();
                    int count = newElements.Count;
                    if ((num10 + count) > GameEngine.Instance.LocalWorldData.Castle_Max_Moat_Tiles)
                    {
                        forceInvalid = true;
                    }
                }
                if (!forceInvalid)
                {
                    int woodCost = 0;
                    int stoneCost = 0;
                    int goldCost = 0;
                    int oilCost = 0;
                    int ironCost = 0;
                    if (!CreateMode)
                    {
                        CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, testType, ref woodCost, ref stoneCost, ref goldCost, ref oilCost, ref ironCost);
                        woodCost *= newElements.Count;
                        stoneCost *= newElements.Count;
                        ironCost *= newElements.Count;
                        oilCost *= newElements.Count;
                        goldCost *= newElements.Count;
                        VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
                        if (GameEngine.Instance.Village != null)
                        {
                            GameEngine.Instance.Village.getStockpileLevels(levels);
                        }
                        int goldLevel = 0;
                        if (!GameEngine.Instance.World.isCapital(this.m_villageID))
                        {
                            goldLevel = (int) GameEngine.Instance.World.getCurrentGold();
                        }
                        else if (GameEngine.Instance.Village != null)
                        {
                            goldLevel = (int) GameEngine.Instance.Village.m_capitalGold;
                        }
                        this.adjustLevels(ref levels, ref goldLevel);
                        if (((((woodCost <= 0) || (woodCost > levels.woodLevel)) && ((stoneCost <= 0) || (stoneCost > levels.stoneLevel))) && (((goldCost <= 0) || (goldCost > goldLevel)) && ((oilCost <= 0) || (oilCost > levels.pitchLevel)))) && ((ironCost <= 0) || (ironCost > levels.ironLevel)))
                        {
                            forceInvalid = true;
                        }
                    }
                }
                this.wallWasValid = !forceInvalid;
                this.lastValidWallX = mapX;
                this.lastValidWallY = mapY;
                for (int j = startWallMapY; j <= num5; j++)
                {
                    for (int m = startWallMapX; m <= num4; m++)
                    {
                        this.addWallSprite(m, j, forceInvalid);
                    }
                }
            }
            else
            {
                int num20 = this.startWallMapX;
                int num21 = this.startWallMapY;
                int num22 = mapX;
                int num23 = mapY;
                int num24 = 0;
                int num25 = 0;
                if (num20 > num22)
                {
                    num24 = -1;
                }
                else if (num20 < num22)
                {
                    num24 = 1;
                }
                if (num21 > num23)
                {
                    num25 = -1;
                }
                else if (num21 < num23)
                {
                    num25 = 1;
                }
                List<CastleElement> list2 = new List<CastleElement>();
                if (!this.testWallSprite(num20, num21, out tempElement, placementType))
                {
                    forceInvalid = true;
                }
                else
                {
                    if (tempElement != null)
                    {
                        list2.Add(tempElement);
                    }
                    while ((num20 != num22) || (num21 != num23))
                    {
                        if (num20 != num22)
                        {
                            num20 += num24;
                        }
                        if (num21 != num23)
                        {
                            num21 += num25;
                        }
                        if (!this.testWallSprite(num20, num21, out tempElement, placementType))
                        {
                            forceInvalid = true;
                            break;
                        }
                        if (tempElement != null)
                        {
                            list2.Add(tempElement);
                        }
                    }
                }
                if ((this.castleLayout == null) || this.castleLayout.isCastleEnclosed(null, list2))
                {
                    forceInvalid = true;
                }
                if (!forceInvalid)
                {
                    int num26 = 0;
                    int num27 = 0;
                    int num28 = 0;
                    int num29 = 0;
                    int num30 = 0;
                    if (!CreateMode)
                    {
                        CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, placementType, ref num26, ref num27, ref num28, ref num29, ref num30);
                        num26 *= list2.Count;
                        num27 *= list2.Count;
                        VillageMap.StockpileLevels levels2 = new VillageMap.StockpileLevels();
                        if (GameEngine.Instance.Village != null)
                        {
                            GameEngine.Instance.Village.getStockpileLevels(levels2);
                        }
                        int num31 = 0;
                        this.adjustLevels(ref levels2, ref num31);
                        if (((num26 <= 0) || (num26 > levels2.woodLevel)) && ((num27 <= 0) || (num27 > levels2.stoneLevel)))
                        {
                            forceInvalid = true;
                        }
                    }
                }
                this.wallWasValid = !forceInvalid;
                this.lastValidWallX = mapX;
                this.lastValidWallY = mapY;
                num20 = this.startWallMapX;
                num21 = this.startWallMapY;
                this.addWallSprite(num20, num21, forceInvalid);
                while ((num20 != num22) || (num21 != num23))
                {
                    if (num20 != num22)
                    {
                        num20 += num24;
                    }
                    if (num21 != num23)
                    {
                        num21 += num25;
                    }
                    this.addWallSprite(num20, num21, forceInvalid);
                }
            }
        }

        private bool addWallSprite(int sx, int sy, bool forceInvalid)
        {
            SpriteWrapper sprite = this.getNextWallCacheSprite();
            int num = -1;
            if (placementType == 0x42)
            {
                num = this.initCastleSprite(sprite, 0x21, 0, 0, true, null);
            }
            else if (placementType == 0x41)
            {
                num = this.initCastleSprite(sprite, 0x22, 0, 0, true, null);
            }
            else
            {
                num = this.initCastleSprite(sprite, placementType, 0, 0, true, null);
            }
            sprite.SpriteNo = num;
            bool flag = this.movePlaceElement(sx, sy, sprite, forceInvalid, false);
            placementWallSprites.Add(sprite);
            backgroundSprite.AddChild(sprite, 10);
            return flag;
        }

        private void addWallSprites()
        {
            CastleElement tempElement = null;
            int testType = this.correctPlacementType(placementType);
            if (((placementType == 0x24) || (placementType == 0x23)) || ((placementType == 0x41) || (placementType == 0x42)))
            {
                int startWallMapX = this.startWallMapX;
                int startWallMapY = this.startWallMapY;
                int lastValidWallX = this.lastValidWallX;
                int lastValidWallY = this.lastValidWallY;
                if (startWallMapX > lastValidWallX)
                {
                    int num6 = startWallMapX;
                    startWallMapX = lastValidWallX;
                    lastValidWallX = num6;
                }
                if (startWallMapY > lastValidWallY)
                {
                    int num7 = startWallMapY;
                    startWallMapY = lastValidWallY;
                    lastValidWallY = num7;
                }
                for (int i = startWallMapY; i <= lastValidWallY; i++)
                {
                    for (int j = startWallMapX; j <= lastValidWallX; j++)
                    {
                        if (this.testWallSprite(j, i, out tempElement, testType) && (tempElement != null))
                        {
                            this.addFakeWallSprite(j, i);
                        }
                    }
                }
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                this.recalcCastleLayout();
                this.clearPlacementWallSprites();
            }
            else
            {
                int mapX = this.startWallMapX;
                int mapY = this.startWallMapY;
                int num12 = this.lastValidWallX;
                int num13 = this.lastValidWallY;
                int num14 = 0;
                int num15 = 0;
                if (mapX > num12)
                {
                    num14 = -1;
                }
                else if (mapX < num12)
                {
                    num14 = 1;
                }
                if (mapY > num13)
                {
                    num15 = -1;
                }
                else if (mapY < num13)
                {
                    num15 = 1;
                }
                mapX = this.startWallMapX;
                mapY = this.startWallMapY;
                if (this.testWallSprite(mapX, mapY, out tempElement, testType) && (tempElement != null))
                {
                    this.addFakeWallSprite(mapX, mapY);
                }
                while ((mapX != num12) || (mapY != num13))
                {
                    if (mapX != num12)
                    {
                        mapX += num14;
                    }
                    if (mapY != num13)
                    {
                        mapY += num15;
                    }
                    if (this.testWallSprite(mapX, mapY, out tempElement, testType) && (tempElement != null))
                    {
                        this.addFakeWallSprite(mapX, mapY);
                    }
                }
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                this.recalcCastleLayout();
                this.clearPlacementWallSprites();
            }
        }

        public void adjustLevels(ref VillageMap.StockpileLevels levels, ref int goldLevel)
        {
            if (this.inBuilderMode)
            {
                bool flag = false;
                foreach (CastleElement element in this.elements)
                {
                    if (element.elementID < -1L)
                    {
                        int woodCost = 0;
                        int stoneCost = 0;
                        int goldCost = 0;
                        int oilCost = 0;
                        int ironCost = 0;
                        CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, element.elementType, ref woodCost, ref stoneCost, ref goldCost, ref oilCost, ref ironCost);
                        levels.woodLevel -= woodCost;
                        levels.stoneLevel -= stoneCost;
                        levels.pitchLevel -= oilCost;
                        levels.ironLevel -= ironCost;
                        goldLevel -= goldCost;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    this.inBuilderMode = false;
                    this.recalcCastleLayout();
                }
            }
            if (this.inTroopPlacerMode && placingDefender)
            {
                bool flag2 = false;
                foreach (CastleElement element2 in this.elements)
                {
                    if (element2.elementID < -1L)
                    {
                        int num6 = 0;
                        int num7 = 0;
                        int num8 = 0;
                        int num9 = 0;
                        int num10 = 0;
                        CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, element2.elementType, ref num6, ref num7, ref num8, ref num9, ref num10);
                        levels.woodLevel -= num6;
                        levels.stoneLevel -= num7;
                        levels.pitchLevel -= num9;
                        levels.ironLevel -= num10;
                        goldLevel -= num8;
                        flag2 = true;
                    }
                }
                if (!flag2)
                {
                    this.inBuilderMode = false;
                    this.recalcCastleLayout();
                }
            }
        }

        public void autoPlaceAttackers(int mode)
        {
            int num = 0;
            int num2 = 0;
            placingDefender = false;
            while (!this.isAttackReady())
            {
                int mapX = 0;
                int mapY = 0;
                switch (mode)
                {
                    case 0:
                        if ((num & 1) != 1)
                        {
                            break;
                        }
                        mapX = 0x3a - ((num + 1) / 2);
                        goto Label_0047;

                    case 1:
                        if ((num & 1) != 1)
                        {
                            goto Label_0097;
                        }
                        mapY = 0x3a - ((num + 1) / 2);
                        goto Label_009E;

                    case 2:
                        if ((num & 1) != 1)
                        {
                            goto Label_007B;
                        }
                        mapY = 0x3a - ((num + 1) / 2);
                        goto Label_0082;

                    case 3:
                        if ((num & 1) != 1)
                        {
                            goto Label_005C;
                        }
                        mapX = 0x3a - ((num + 1) / 2);
                        goto Label_0063;

                    default:
                        goto Label_00A3;
                }
                mapX = 0x3a + (num / 2);
            Label_0047:
                mapY = num2;
                goto Label_00A3;
            Label_005C:
                mapX = 0x3a + (num / 2);
            Label_0063:
                mapY = 0x75 - num2;
                goto Label_00A3;
            Label_007B:
                mapY = 0x3a + (num / 2);
            Label_0082:
                mapX = num2;
                goto Label_00A3;
            Label_0097:
                mapY = 0x3a + (num / 2);
            Label_009E:
                mapX = 0x75 - num2;
            Label_00A3:
                if (this.attackNumCatapults != this.attackMaxCatapults)
                {
                    placementType = 0x5e;
                }
                else if (this.attackNumArchers != this.attackMaxArchers)
                {
                    placementType = 0x5c;
                }
                else if (this.attackNumPikemen != this.attackMaxPikemen)
                {
                    placementType = 0x5d;
                }
                else if (this.attackNumSwordsmen != this.attackMaxSwordsmen)
                {
                    placementType = 0x5b;
                }
                else
                {
                    placementType = 90;
                }
                this.startPlacingAttackerTroops(placementType);
                if (this.mouseMovePlaceTroops(mapX, mapY, true, 0))
                {
                    this.troopPlaceAttacker(mapX, mapY);
                }
                num++;
                if (num >= 0x76)
                {
                    num = 0;
                    num2++;
                    if (num2 >= 0x76)
                    {
                        break;
                    }
                }
            }
            this.stopPlaceElement();
            CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
            this.recalcCastleLayout();
        }

        public void autoRepairCastle()
        {
            RemoteServices.Instance.set_AutoRepairCastle_UserCallBack(new RemoteServices.AutoRepairCastle_UserCallBack(this.AutoRepairCastleCallback));
            RemoteServices.Instance.AutoRepairCastle(this.VillageID);
        }

        public void AutoRepairCastleCallback(AutoRepairCastle_ReturnType returnData)
        {
            if (returnData.Success)
            {
                setServerTime(returnData.currentTime);
                if ((returnData.villageResourcesAndStats != null) && (GameEngine.Instance.Village != null))
                {
                    GameEngine.Instance.Village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    VillageMap village = GameEngine.Instance.Village;
                    if (village != null)
                    {
                        this.numAvailableDefenderPeasants = 0;
                        this.numAvailableDefenderArchers = 0;
                        this.numAvailableDefenderPikemen = 0;
                        this.numAvailableDefenderSwordsmen = 0;
                        this.numAvailableDefenderCaptains = 0;
                        village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
                        GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
                        village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
                    }
                }
                if (returnData.elements != null)
                {
                    this.importElements(returnData.elements);
                }
            }
        }

        public void BattleUpdate(bool villageDisplayed, bool addToGFX)
        {
            if ((backgroundSprite != null) && villageDisplayed)
            {
                this.updates++;
                if (!this.castleCombat.Paused)
                {
                    this.updateRocks();
                }
                if (!this.endOfBattle)
                {
                    if (this.castleCombat.tick())
                    {
                        this.elements = this.castleCombat.getElementList();
                    }
                    this.runCastleSounds();
                    if (this.castleCombat.hasBattleFinished())
                    {
                        this.castleCombat.CloseExtremeLogging();
                        this.castleCombat.battlePaused = true;
                        this.endOfBattle = true;
                        this.endingTroopNumbers = this.castleCombat.getBattleTroopNumbers();
                        InterfaceMgr.Instance.ShowViewBattleResults(this.castleCombat.hasAttackerWon(), this.startingTroopNumbers, this.endingTroopNumbers, this.VillageID, this.m_reportReturnData);
                    }
                }
                if (addToGFX)
                {
                    this.recalcCastleLayout();
                    this.drawRockChips();
                    backgroundSprite.Update();
                    backgroundSprite.AddToRenderList();
                    InterfaceMgr.Instance.setCastlePillageClock(this.castleCombat.PillageClock, this.castleCombat.PillageClockMax);
                    InterfaceMgr.Instance.setCastleReportClock(this.castleCombat.ReportClock, this.castleCombat.ReportClockMax);
                }
            }
        }

        public void BattleUpdateManager(bool villageDisplayed)
        {
            if (this.realBattleMode)
            {
                if (this.fastPlayback)
                {
                    this.BattleUpdate(villageDisplayed, false);
                    this.BattleUpdate(villageDisplayed, false);
                }
                this.BattleUpdate(villageDisplayed, true);
            }
            else
            {
                backgroundSprite.Update();
                backgroundSprite.AddToRenderList();
            }
            this.drawSurroundSprites();
        }

        private int calcTilt(BattleArrow arrow, int startHeight, int targetHeight)
        {
            double x = (arrow.fullDist / 8) * 0x18;
            double y = 0.0;
            y = targetHeight - startHeight;
            double num4 = Math.Atan2(y, x) * 57.2957795;
            if (num4 < -50.0)
            {
                return 0;
            }
            if (num4 < -35.0)
            {
                return 1;
            }
            if (num4 < -17.0)
            {
                return 2;
            }
            if (num4 < -5.0)
            {
                return 3;
            }
            if (num4 < 5.0)
            {
                return 4;
            }
            if (num4 < 20.0)
            {
                return 5;
            }
            if (num4 < 35.0)
            {
                return 6;
            }
            if (num4 < 50.0)
            {
                return 7;
            }
            return 8;
        }

        public void cancelBuilderMode()
        {
            if (this.inBuilderMode)
            {
                List<CastleElement> list = new List<CastleElement>();
                foreach (CastleElement element in this.elements)
                {
                    if (element.elementID < -1L)
                    {
                        list.Add(element);
                    }
                }
                foreach (CastleElement element2 in list)
                {
                    this.elements.Remove(element2);
                }
                if (this.removedElements != null)
                {
                    this.elements.AddRange(this.removedElements);
                    this.removedElements.Clear();
                }
                this.inBuilderMode = false;
                if (GameEngine.Instance.World.getTutorialStage() == 11)
                {
                    this.tutorialAutoPlace();
                    this.tutorialAutoPlace();
                }
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                this.recalcCastleLayout();
            }
            if (this.InTroopPlacerMode)
            {
                List<CastleElement> list2 = new List<CastleElement>();
                VillageMap village = GameEngine.Instance.Village;
                if (village != null)
                {
                    foreach (CastleElement element3 in this.elements)
                    {
                        if (element3.elementID < -1L)
                        {
                            list2.Add(element3);
                            switch (element3.elementType)
                            {
                                case 70:
                                    if (element3.reinforcement || element3.vassalReinforcements)
                                    {
                                        goto Label_0194;
                                    }
                                    village.addTroops(1, 0, 0, 0, 0);
                                    break;

                                case 0x47:
                                    if (element3.reinforcement || element3.vassalReinforcements)
                                    {
                                        goto Label_0275;
                                    }
                                    village.addTroops(0, 0, 0, 1, 0);
                                    break;

                                case 0x48:
                                    if (element3.reinforcement || element3.vassalReinforcements)
                                    {
                                        goto Label_01E3;
                                    }
                                    village.addTroops(0, 1, 0, 0, 0);
                                    break;

                                case 0x49:
                                    if (element3.reinforcement || element3.vassalReinforcements)
                                    {
                                        goto Label_022F;
                                    }
                                    village.addTroops(0, 0, 1, 0, 0);
                                    break;

                                case 0x55:
                                    goto Label_029B;
                            }
                        }
                        continue;
                    Label_0194:
                        if (!element3.vassalReinforcements)
                        {
                            this.numPlacedReinforceDefenderPeasants--;
                        }
                        else
                        {
                            village.addVassalTroops(1, 0, 0, 0);
                        }
                        continue;
                    Label_01E3:
                        if (!element3.vassalReinforcements)
                        {
                            this.numPlacedReinforceDefenderArchers--;
                        }
                        else
                        {
                            village.addVassalTroops(0, 1, 0, 0);
                        }
                        continue;
                    Label_022F:
                        if (!element3.vassalReinforcements)
                        {
                            this.numPlacedReinforceDefenderPikemen--;
                        }
                        else
                        {
                            village.addVassalTroops(0, 0, 1, 0);
                        }
                        continue;
                    Label_0275:
                        if (!element3.vassalReinforcements)
                        {
                            this.numPlacedReinforceDefenderSwordsmen--;
                        }
                        else
                        {
                            village.addVassalTroops(0, 0, 0, 1);
                        }
                        continue;
                    Label_029B:
                        village.addTroops(0, 0, 0, 0, 0, 0, 1);
                    }
                }
                foreach (CastleElement element4 in list2)
                {
                    this.elements.Remove(element4);
                }
                if (this.removedElements != null)
                {
                    if (village != null)
                    {
                        foreach (CastleElement element5 in this.removedElements)
                        {
                            switch (element5.elementType)
                            {
                                case 70:
                                    village.addTroops(-1, 0, 0, 0, 0);
                                    break;

                                case 0x47:
                                    village.addTroops(0, 0, 0, -1, 0);
                                    break;

                                case 0x48:
                                    village.addTroops(0, -1, 0, 0, 0);
                                    break;

                                case 0x49:
                                    village.addTroops(0, 0, -1, 0, 0);
                                    break;

                                case 0x55:
                                    village.addTroops(0, 0, 0, 0, 0, 0, -1);
                                    break;
                            }
                        }
                    }
                    this.elements.AddRange(this.removedElements);
                    this.removedElements.Clear();
                }
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                if (this.movedElementsOriginal != null)
                {
                    foreach (CastleElement element6 in this.movedElementsOriginal)
                    {
                        CastleElement element7 = this.castleLayout.getElementFromElemID(element6.elementID);
                        element7.xPos = element6.xPos;
                        element7.yPos = element6.yPos;
                    }
                }
                this.inTroopPlacerMode = false;
                if (village != null)
                {
                    this.numAvailableDefenderPeasants = 0;
                    this.numAvailableDefenderArchers = 0;
                    this.numAvailableDefenderPikemen = 0;
                    this.numAvailableDefenderSwordsmen = 0;
                    this.numAvailableDefenderCaptains = 0;
                    this.numAvailableDefenderCaptains = 0;
                    village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
                    GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
                    village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
                }
                this.clearLasso();
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                this.recalcCastleLayout();
            }
        }

        public bool canMemoriseAttackSetup()
        {
            return (this.castleLayout.createCastleCampArray_MemoriseAttackSetupTroops().Length > 0);
        }

        public bool captainPlaced()
        {
            foreach (CastleElement element in this.elements)
            {
                if ((element.elementType >= 100) && (element.elementType <= 0x6d))
                {
                    return true;
                }
            }
            return false;
        }

        public bool castleNeedsRepair()
        {
            return this.castleDamaged;
        }

        public void castleShown(bool getTroops)
        {
            backgroundSprite.PosX = (int) (0f - ((backgroundSprite.Width - InterfaceMgr.Instance.ParentMainWindow.getDXBasePanel().Width) / 2f));
            backgroundSprite.PosY = (int) (0f - ((backgroundSprite.Height - InterfaceMgr.Instance.ParentMainWindow.getDXBasePanel().Height) / 2f));
            this.stopPlaceElement();
            this.displayType = 0;
            fakeKeep = -1;
            if (getTroops)
            {
                this.manageTutorial();
                VillageMap village = GameEngine.Instance.Village;
                if (village != null)
                {
                    this.numAvailableDefenderPeasants = 0;
                    this.numAvailableDefenderArchers = 0;
                    this.numAvailableDefenderPikemen = 0;
                    this.numAvailableDefenderSwordsmen = 0;
                    this.numAvailableDefenderCaptains = 0;
                    village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
                    GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
                    village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
                }
                this.recalcCastleLayout();
            }
        }

        public void changeDebugLayers()
        {
            this.debugDisplayMode++;
            if (this.debugDisplayMode >= 3)
            {
                this.debugDisplayMode = 0;
            }
            this.recalcCastleLayout();
        }

        public void changeSpreadType()
        {
            this.spreadTypeDiamond = !this.spreadTypeDiamond;
            this.castleCombat.buildAttackerRouteMap(this.spreadTypeDiamond);
            this.recalcCastleLayout();
        }

        public bool checkNormalTroopsAvailable(int troopType)
        {
            switch (troopType)
            {
                case 90:
                    if (this.attackNumPeasants < this.attackMaxPeasants)
                    {
                        break;
                    }
                    return false;

                case 0x5b:
                    if (this.attackNumSwordsmen < this.attackMaxSwordsmen)
                    {
                        break;
                    }
                    return false;

                case 0x5c:
                    if (this.attackNumArchers < this.attackMaxArchers)
                    {
                        break;
                    }
                    return false;

                case 0x5d:
                    if (this.attackNumPikemen < this.attackMaxPikemen)
                    {
                        break;
                    }
                    return false;
            }
            return true;
        }

        public void cleanUpAttackSaveNames()
        {
            char[] separator = new char[] { '_' };
            string[] files = Directory.GetFiles(GameEngine.getSettingsPath(true), "*.cas");
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            List<string> list = new List<string>();
            foreach (string str in files)
            {
                string fileName = Path.GetFileName(str);
                string[] strArray2 = fileName.Remove(fileName.LastIndexOf('.')).Split(separator);
                if ((strArray2.Length >= 3) && (strArray2[0].ToLowerInvariant() == "attacksetup"))
                {
                    try
                    {
                        Convert.ToInt32(strArray2[strArray2.Length - 1]);
                    }
                    catch
                    {
                        continue;
                    }
                    fileName = "";
                    for (int j = 1; j < (strArray2.Length - 1); j++)
                    {
                        if (j > 1)
                        {
                            fileName = fileName + "_";
                        }
                        fileName = fileName + strArray2[j];
                    }
                    if (list.Contains(fileName))
                    {
                        list.Remove(fileName);
                        dictionary.Add(fileName, 1);
                    }
                    else if (!dictionary.ContainsKey(fileName))
                    {
                        list.Add(fileName);
                    }
                }
            }
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    string item = Path.GetFileName(files[i]);
                    string[] strArray3 = item.Remove(item.LastIndexOf('.')).Split(separator);
                    string destFileName = "";
                    if ((strArray3.Length >= 3) && (strArray3[0].ToLowerInvariant() == "attacksetup"))
                    {
                        item = "";
                        for (int k = 1; k < (strArray3.Length - 1); k++)
                        {
                            if (k > 1)
                            {
                                item = item + "_";
                            }
                            item = item + strArray3[k];
                        }
                        if (list.Contains(item))
                        {
                            destFileName = files[i].Replace("_" + strArray3[strArray3.Length - 1], "");
                        }
                        else if (dictionary.ContainsKey(item))
                        {
                            int num4;
                            Dictionary<string, int> dictionary2;
                            string str5;
                            dictionary.TryGetValue(item, out num4);
                            destFileName = files[i].Replace("_" + strArray3[strArray3.Length - 1], " (" + num4.ToString() + ")");
                            (dictionary2 = dictionary)[str5 = item] = dictionary2[str5] + 1;
                        }
                        if (destFileName != "")
                        {
                            File.Move(files[i], destFileName);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void clearAttackingTroops()
        {
            List<CastleElement> list = new List<CastleElement>();
            foreach (CastleElement element in this.elements)
            {
                if ((element.elementType >= 90) && (element.elementType <= 0x5e))
                {
                    list.Add(element);
                }
            }
            foreach (CastleElement element2 in list)
            {
                this.elements.Remove(element2);
                this.deleteCatapultTarget(element2.elementID);
                this.deleteCaptainsDetails(element2.elementID);
            }
            CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
            this.recalcCastleLayout();
        }

        public void clearCatapultLines()
        {
            this.catapultLines.Clear();
        }

        public void clearLasso()
        {
            this.m_lassoElements.Clear();
            this.m_lassoMade = false;
            InterfaceMgr.Instance.castle_ClearSelectedTroop();
            this.recalcCastleLayout();
        }

        private void clearPlacementTroopSprites()
        {
            for (int i = 0; i < 0x19; i++)
            {
                if (placementTroopCastleSprite[i] != null)
                {
                    if (placementTroopSprite[i] != null)
                    {
                        placementTroopSprite[i].RemoveChild(placementTroopCastleSprite[i]);
                    }
                    placementTroopCastleSprite[i] = null;
                }
                if (placementTroopSprite[i] != null)
                {
                    if (backgroundSprite != null)
                    {
                        backgroundSprite.RemoveChild(placementTroopSprite[i]);
                    }
                    placementTroopSprite[i] = null;
                }
            }
        }

        private void clearPlacementWallSprites()
        {
            foreach (SpriteWrapper wrapper in placementWallSprites)
            {
                backgroundSprite.RemoveChild(wrapper);
            }
            placementWallSprites.Clear();
            this.nextWallCacheSpriteID = 0;
        }

        public void clearTempAttackers()
        {
            tempCompressedAttackerMap = null;
        }

        private long clickFindTroop(Point mousePos)
        {
            int y = -1000;
            long elementID = -2L;
            for (int i = 0; i < numClickAreas; i++)
            {
                TroopClickArea area = troopClickAreas[i];
                if ((area.y > y) && area.clicked(mousePos))
                {
                    y = area.y;
                    elementID = area.elementID;
                }
            }
            return elementID;
        }

        public void commitCastle()
        {
            int num = 0;
            foreach (CastleElement element in this.elements)
            {
                if (element.elementID < -1L)
                {
                    num++;
                }
            }
            if (num >= 0)
            {
                List<long> list = new List<long>();
                if (this.inTroopPlacerMode)
                {
                    foreach (CastleElement element2 in this.removedElements)
                    {
                        if (element2.elementID >= 0L)
                        {
                            list.Add(element2.elementID);
                        }
                    }
                }
                byte[,] elementList = new byte[num, 4];
                int num2 = 0;
                foreach (CastleElement element3 in this.elements)
                {
                    if (element3.elementID >= -1L)
                    {
                        continue;
                    }
                    if (element3.aggressiveDefender && (((element3.elementType == 70) || (element3.elementType == 0x49)) || (element3.elementType == 0x47)))
                    {
                        switch (element3.elementType)
                        {
                            case 70:
                                elementList[num2, 0] = 80;
                                break;

                            case 0x47:
                                elementList[num2, 0] = 0x51;
                                break;

                            case 0x49:
                                elementList[num2, 0] = 0x52;
                                break;
                        }
                    }
                    else if ((element3.elementType == 0x47) && element3.aggressiveDefender)
                    {
                        elementList[num2, 0] = 0x53;
                    }
                    else
                    {
                        elementList[num2, 0] = element3.elementType;
                    }
                    elementList[num2, 1] = element3.xPos;
                    elementList[num2, 2] = element3.yPos;
                    elementList[num2, 3] = 0;
                    if (element3.reinforcement)
                    {
                        byte num1 = 0;
                        num1 = (byte) (num1 | 1);
                    }
                    if (element3.vassalReinforcements)
                    {
                        byte num4 = 0;
                        num4 = (byte) (num4 | 2);
                    }
                    num2++;
                }
                if (this.removedElements != null)
                {
                    this.removedElements.Clear();
                }
                if (this.movedElementsOriginal != null)
                {
                    this.movedElementsOriginal.Clear();
                }
                RemoteServices.Instance.set_AddCastleElement_UserCallBack(new RemoteServices.AddCastleElement_UserCallBack(this.newElementCallback));
                if ((list.Count == 0) && ((this.movedElements == null) || (this.movedElements.Count == 0)))
                {
                    RemoteServices.Instance.AddCastleElementList(this.m_villageID, elementList);
                }
                else
                {
                    List<MoveElementData> list2 = new List<MoveElementData>();
                    if (this.movedElements != null)
                    {
                        foreach (CastleElement element4 in this.movedElements)
                        {
                            MoveElementData item = new MoveElementData {
                                elementID = element4.elementID,
                                xPos = element4.xPos,
                                yPos = element4.yPos
                            };
                            list2.Add(item);
                        }
                    }
                    RemoteServices.Instance.AddCastleElementList(this.m_villageID, elementList, list.ToArray(), list2.ToArray());
                }
                if (this.commitPopup != null)
                {
                    this.commitPopup.Close();
                }
                this.commitPopup = new CastleCommitPopup();
                this.commitPopup.Show();
            }
            this.clearLasso();
            this.stopPlaceElement();
        }

        public bool commonMouseClicked(Point mousePos)
        {
            bool flag = true;
            if (!GameEngine.Instance.World.isCapital(this.m_villageID))
            {
                flag = false;
                if (InterfaceMgr.Instance.clickDXCardBar(mousePos))
                {
                    return true;
                }
                if ((GameEngine.Instance.World.isTutorialActive() && (mousePos.X < 0x40)) && (mousePos.Y >= (this.gfx.ViewportHeight - 0x40)))
                {
                    GameEngine.Instance.World.forceTutorialToBeShown();
                    return true;
                }
            }
            if ((this.attackerSetupMode || (mousePos.X <= (this.gfx.ViewportWidth - 0x20))) || ((mousePos.Y >= (32f + wikiHelpSprite.PosY)) || (mousePos.Y <= wikiHelpSprite.PosY)))
            {
                return false;
            }
            if (!flag)
            {
                CustomSelfDrawPanel.WikiLinkControl.openHelpLink(2);
            }
            else
            {
                CustomSelfDrawPanel.WikiLinkControl.openHelpLink(10);
            }
            return true;
        }

        public void confirmTroopPlacement(int mapX, int mapY)
        {
            UniversalDebugLog.Log(string.Concat(new object[] { "confirming troop placement ", mapX, " ", mapY }));
            if (this.placementSize > 1)
            {
                if (placementType != 0x5e)
                {
                    if (this.placementSize != 4)
                    {
                        int spriteIndex = 0;
                        int num2 = this.placementSize - 1;
                        for (int i = mapY - num2; i <= (mapY + num2); i++)
                        {
                            for (int j = mapX - num2; j <= (mapX + num2); j++)
                            {
                                if (this.mouseMovePlaceTroops(j, i, true, spriteIndex))
                                {
                                    if (placingDefender)
                                    {
                                        this.troopPlaceDefender(j, i);
                                    }
                                    else
                                    {
                                        this.troopPlaceAttacker(j, i);
                                    }
                                }
                                spriteIndex++;
                            }
                        }
                    }
                    else
                    {
                        int num5 = 0;
                        if (mapX < mapY)
                        {
                            if ((0x75 - mapX) < mapY)
                            {
                                num5 = 0;
                            }
                            else
                            {
                                num5 = 2;
                            }
                        }
                        else if ((0x75 - mapX) < mapY)
                        {
                            num5 = 6;
                        }
                        else
                        {
                            num5 = 4;
                        }
                        if ((num5 == 0) || (num5 == 4))
                        {
                            int num6 = 0;
                            int num7 = 2;
                            for (int k = mapX - num7; k <= (mapX + num7); k++)
                            {
                                if (this.mouseMovePlaceTroops(k, mapY, true, num6))
                                {
                                    if (placingDefender)
                                    {
                                        this.troopPlaceDefender(k, mapY);
                                    }
                                    else
                                    {
                                        this.troopPlaceAttacker(k, mapY);
                                    }
                                }
                                num6++;
                            }
                        }
                        else
                        {
                            int num9 = 0;
                            int num10 = 2;
                            for (int m = mapY - num10; m <= (mapY + num10); m++)
                            {
                                if (this.mouseMovePlaceTroops(mapX, m, true, num9))
                                {
                                    if (placingDefender)
                                    {
                                        this.troopPlaceDefender(mapX, m);
                                    }
                                    else
                                    {
                                        this.troopPlaceAttacker(mapX, m);
                                    }
                                }
                                num9++;
                            }
                        }
                    }
                }
                else
                {
                    int num12 = 0;
                    if (mapX < mapY)
                    {
                        if ((0x75 - mapX) < mapY)
                        {
                            num12 = 0;
                        }
                        else
                        {
                            num12 = 2;
                        }
                    }
                    else if ((0x75 - mapX) < mapY)
                    {
                        num12 = 6;
                    }
                    else
                    {
                        num12 = 4;
                    }
                    if ((num12 == 0) || (num12 == 4))
                    {
                        int num13 = 0;
                        int num14 = (this.placementSize - 1) * 2;
                        for (int n = mapX - num14; n <= (mapX + num14); n += 2)
                        {
                            if (this.mouseMovePlaceTroops(n, mapY, true, num13))
                            {
                                this.troopPlaceAttacker(n, mapY);
                            }
                            num13++;
                        }
                    }
                    else
                    {
                        int num16 = 0;
                        int num17 = (this.placementSize - 1) * 2;
                        for (int num18 = mapY - num17; num18 <= (mapY + num17); num18 += 2)
                        {
                            if (this.mouseMovePlaceTroops(mapX, num18, true, num16))
                            {
                                this.troopPlaceAttacker(mapX, num18);
                            }
                            num16++;
                        }
                    }
                }
            }
            else if (this.mouseMovePlaceTroops(mapX, mapY, true, 0))
            {
                if (placingDefender)
                {
                    this.troopPlaceDefender(mapX, mapY);
                }
                else
                {
                    this.troopPlaceAttacker(mapX, mapY);
                }
            }
        }

        public bool containsCaptain()
        {
            foreach (CastleElement element in this.elements)
            {
                if ((element.elementType >= 100) && (element.elementType <= 0x6d))
                {
                    return true;
                }
            }
            return false;
        }

        private int correctPlacementType(int placementType)
        {
            int num = placementType;
            switch (num)
            {
                case 0x41:
                    return 0x22;

                case 0x42:
                    return 0x21;
            }
            return num;
        }

        public int countBallistas()
        {
            int num = 0;
            foreach (CastleElement element in this.elements)
            {
                if (element.elementType == 0x2a)
                {
                    num++;
                }
            }
            return num;
        }

        public int countBombards()
        {
            int num = 0;
            foreach (CastleElement element in this.elements)
            {
                if (element.elementType == 0x2c)
                {
                    num++;
                }
            }
            return num;
        }

        public int countCompletedSmelters()
        {
            int num = 0;
            foreach (CastleElement element in this.elements)
            {
                if ((element.elementType == 0x20) && (element.completionTime < VillageMap.getCurrentServerTime()))
                {
                    num++;
                }
            }
            return num;
        }

        public int countGuardHouses()
        {
            int num = 0;
            foreach (CastleElement element in this.elements)
            {
                if (element.elementType == 0x1f)
                {
                    num++;
                }
            }
            return num;
        }

        public int countMoat()
        {
            int num = 0;
            foreach (CastleElement element in this.elements)
            {
                if (element.elementType == 0x23)
                {
                    num++;
                }
            }
            return num;
        }

        public int countOwnPlacedCaptains()
        {
            int num = 0;
            foreach (CastleElement element in this.elements)
            {
                if ((!element.reinforcement && !element.vassalReinforcements) && (element.elementType == 0x55))
                {
                    num++;
                }
            }
            return num;
        }

        public int countOwnPlacedTroops()
        {
            int num = 0;
            foreach (CastleElement element in this.elements)
            {
                if (!element.reinforcement && !element.vassalReinforcements)
                {
                    switch (element.elementType)
                    {
                        case 70:
                        case 0x47:
                        case 0x48:
                        case 0x49:
                        case 0x4d:
                        case 0x55:
                            goto Label_005D;
                    }
                }
                continue;
            Label_005D:
                num++;
            }
            return num;
        }

        public void countOwnPlacedTroopTypes(ref int numPeasants, ref int numArchers, ref int numPikemen, ref int numSwordsmen, ref int numCaptains)
        {
            numPeasants = 0;
            numArchers = 0;
            numPikemen = 0;
            numSwordsmen = 0;
            numCaptains = 0;
            foreach (CastleElement element in this.elements)
            {
                if (!element.reinforcement && !element.vassalReinforcements)
                {
                    switch (element.elementType)
                    {
                        case 70:
                            numPeasants++;
                            break;

                        case 0x47:
                            numSwordsmen++;
                            break;

                        case 0x48:
                            numArchers++;
                            break;

                        case 0x49:
                            numPikemen++;
                            break;

                        case 0x55:
                            goto Label_007F;
                    }
                }
                continue;
            Label_007F:
                numCaptains++;
            }
        }

        public int countPlacedInfrastructure()
        {
            int num = 0;
            foreach (CastleElement element in this.elements)
            {
                if ((element.elementType > 10) && (element.elementType < 0x45))
                {
                    num++;
                }
            }
            return num;
        }

        public int countPlacedMoat()
        {
            int num = 0;
            foreach (CastleElement element in this.elements)
            {
                if (element.elementType == 0x23)
                {
                    num++;
                }
            }
            return num;
        }

        public int countPlacedOilPots()
        {
            int num = 0;
            foreach (CastleElement element in this.elements)
            {
                if (element.elementType == 0x4b)
                {
                    num++;
                }
            }
            return num;
        }

        public int countPlacedPits()
        {
            int num = 0;
            foreach (CastleElement element in this.elements)
            {
                if (element.elementType == 0x24)
                {
                    num++;
                }
            }
            return num;
        }

        public int countPlacedTroops()
        {
            int num = 0;
            foreach (CastleElement element in this.elements)
            {
                switch (element.elementType)
                {
                    case 70:
                    case 0x47:
                    case 0x48:
                    case 0x49:
                    case 0x4d:
                    case 0x55:
                        num++;
                        break;
                }
            }
            return num;
        }

        public int countTurrets()
        {
            int num = 0;
            foreach (CastleElement element in this.elements)
            {
                if (element.elementType == 0x29)
                {
                    num++;
                }
            }
            return num;
        }

        private void createDestroyPlacementTroopSprites()
        {
            UniversalDebugLog.Log("refreshing sprites size:" + this.placementSize);
            int num = 1;
            if (this.placementSize == 2)
            {
                num = 9;
            }
            else if (this.placementSize == 3)
            {
                num = 0x19;
            }
            else if (this.placementSize == 4)
            {
                num = 5;
            }
            for (int i = 0; i < 0x19; i++)
            {
                if (i >= num)
                {
                    goto Label_03F5;
                }
                if (placementTroopSprite[i] != null)
                {
                    continue;
                }
                PointF tf = new PointF(50f, 66f);
                placementTroopSprite[i] = new SpriteWrapper();
                switch (placementType)
                {
                    case 70:
                        if (placingReinforcement)
                        {
                            goto Label_01AF;
                        }
                        placementTroopSprite[i].TextureID = GFXLibrary.Instance.PeasantAnimTexID;
                        goto Label_031C;

                    case 0x47:
                        if (placingReinforcement)
                        {
                            goto Label_0233;
                        }
                        placementTroopSprite[i].TextureID = GFXLibrary.Instance.SwordsmanAnimTexID;
                        goto Label_031C;

                    case 0x48:
                        if (placingReinforcement)
                        {
                            break;
                        }
                        placementTroopSprite[i].TextureID = GFXLibrary.Instance.ArcherAnimTexID;
                        goto Label_031C;

                    case 0x49:
                        if (placingReinforcement)
                        {
                            goto Label_0172;
                        }
                        placementTroopSprite[i].TextureID = GFXLibrary.Instance.PikemanAnimTexID;
                        goto Label_031C;

                    case 0x4b:
                        placementTroopSprite[i].TextureID = GFXLibrary.Instance.CastleSpritesTexID;
                        placementTroopSprite[i].SpriteNo = 0x18c;
                        goto Label_031C;

                    case 0x4d:
                        placementTroopSprite[i].TextureID = GFXLibrary.Instance.WolfAnimTexID;
                        goto Label_031C;

                    case 0x55:
                        placementTroopSprite[i].TextureID = GFXLibrary.Instance.CaptainAnimTexID;
                        goto Label_031C;

                    case 90:
                        placementTroopSprite[i].TextureID = GFXLibrary.Instance.PeasantRedAnimTexID;
                        tf = new PointF(18f, 28f);
                        goto Label_031C;

                    case 0x5c:
                        placementTroopSprite[i].TextureID = GFXLibrary.Instance.ArcherRedAnimTexID;
                        goto Label_031C;

                    case 0x5d:
                        placementTroopSprite[i].TextureID = GFXLibrary.Instance.PikemanRedAnimTexID;
                        goto Label_031C;

                    case 0x5e:
                        placementTroopSprite[i].TextureID = GFXLibrary.Instance.CatapultAnimTexID;
                        tf = new PointF(93f, 100f);
                        goto Label_031C;

                    case 100:
                    case 0x65:
                    case 0x66:
                    case 0x67:
                    case 0x68:
                    case 0x69:
                    case 0x6a:
                    case 0x6b:
                        placementTroopSprite[i].TextureID = GFXLibrary.Instance.CaptainAnimRedTexID;
                        goto Label_031C;

                    default:
                        placementTroopSprite[i].TextureID = GFXLibrary.Instance.SwordsmanRedAnimTexID;
                        goto Label_031C;
                }
                placementTroopSprite[i].TextureID = GFXLibrary.Instance.ArcherGreenAnimTexID;
                goto Label_031C;
            Label_0172:
                placementTroopSprite[i].TextureID = GFXLibrary.Instance.PikemanGreenAnimTexID;
                goto Label_031C;
            Label_01AF:
                placementTroopSprite[i].TextureID = GFXLibrary.Instance.PeasantGreenAnimTexID;
                goto Label_031C;
            Label_0233:
                placementTroopSprite[i].TextureID = GFXLibrary.Instance.SwordsmanGreenAnimTexID;
            Label_031C:
                placementTroopSprite[i].Initialize(this.gfx);
                placementTroopSprite[i].Center = tf;
                backgroundSprite.AddChild(placementTroopSprite[i], 10);
                placementTroopCastleSprite[i] = new SpriteWrapper();
                placementTroopCastleSprite[i].TextureID = GFXLibrary.Instance.CastleSpritesTexID;
                placementTroopCastleSprite[i].SpriteNo = 0x24;
                placementTroopCastleSprite[i].Initialize(this.gfx);
                placementTroopCastleSprite[i].PosX = 0f;
                placementTroopCastleSprite[i].PosY = -50f;
                placementTroopCastleSprite[i].ColorToUse = Color.FromArgb(0x80, 0xff, 0x80);
                placementTroopCastleSprite[i].Visible = false;
                placementTroopSprite[i].AddChild(placementTroopCastleSprite[i], 1);
                continue;
            Label_03F5:
                if (placementTroopSprite[i] != null)
                {
                    if (placementTroopCastleSprite[i] != null)
                    {
                        placementTroopSprite[i].RemoveChild(placementTroopCastleSprite[i]);
                        placementTroopCastleSprite[i] = null;
                    }
                    if (backgroundSprite != null)
                    {
                        backgroundSprite.RemoveChild(placementTroopSprite[i]);
                    }
                    placementTroopSprite[i] = null;
                }
            }
        }

        public void createSurroundSprites()
        {
            if (backgroundSprite != null)
            {
                int viewportWidth = this.gfx.ViewportWidth;
                int viewportHeight = this.gfx.ViewportHeight;
                int width = (int) backgroundSprite.Width;
                int height = (int) backgroundSprite.Height;
                if (!this.attackerSetupMode && !this.battleMode)
                {
                    int num5 = (viewportHeight - height) / 2;
                    int num6 = (viewportWidth - width) / 2;
                    if (num5 < 0)
                    {
                        num5 = 0;
                    }
                    if (num6 < 0)
                    {
                        num6 = 0;
                    }
                    int num7 = viewportWidth;
                    if (num6 > 0)
                    {
                        num7 = num6 + width;
                    }
                    enclosedOverlaySprite.Initialize(this.gfx, GFXLibrary.Instance.CastleSpritesTexID, 0x1d1);
                    PointF tf = new PointF(0f, 0f);
                    enclosedOverlaySprite.Layer = 0x13;
                    enclosedOverlaySprite.Center = tf;
                    enclosedOverlaySprite.PosX = num7 - 60;
                    enclosedOverlaySprite.PosY = num5;
                    enclosedOverlaySprite.Update();
                    enclosedOverlaySprite2.Initialize(this.gfx, GFXLibrary.Instance.CastleSpritesTexID, 0x1d1);
                    enclosedOverlaySprite2.Layer = 0x13;
                    enclosedOverlaySprite2.Center = tf;
                    enclosedOverlaySprite2.PosX = num7 - 60;
                    enclosedOverlaySprite2.PosY = num5;
                    enclosedOverlaySprite2.Update();
                    if (!GameEngine.Instance.World.isCapital(this.m_villageID))
                    {
                        bool castleEnclosed = false;
                        if (this.inBuilderMode)
                        {
                            castleEnclosed = this.castleLayout.isCastleEnclosedGateHouseBlocking();
                        }
                        else
                        {
                            VillageMap village = GameEngine.Instance.Village;
                            if (village != null)
                            {
                                castleEnclosed = village.m_castleEnclosed;
                            }
                            else if (this.castleLayout != null)
                            {
                                castleEnclosed = this.castleLayout.isCastleEnclosedGateHouseBlocking();
                            }
                        }
                        if (castleEnclosed)
                        {
                            enclosedOverlaySprite.Initialize(this.gfx, GFXLibrary.Instance.CastleSpritesTexID, 0x1d1);
                        }
                        else
                        {
                            enclosedOverlaySprite.Initialize(this.gfx, GFXLibrary.Instance.CastleSpritesTexID, 0x1d2);
                            enclosedOverlaySprite2.Initialize(this.gfx, GFXLibrary.Instance.CastleSpritesTexID, 0x1d3);
                        }
                        this.m_castleEnclosed = castleEnclosed;
                    }
                    tutorialOverlaySprite.Initialize(this.gfx, GFXLibrary.Instance.TutorialIconNormalID, 0);
                    tutorialOverlaySprite.Layer = 0x13;
                    tutorialOverlaySprite.Center = tf;
                    tutorialOverlaySprite.PosX = 0f;
                    tutorialOverlaySprite.PosY = viewportHeight - 0x40;
                    tutorialOverlaySprite.Update();
                    wikiHelpSprite.Initialize(this.gfx, GFXLibrary.Instance.WikiHelpIconNormal, 0);
                    wikiHelpSprite.Layer = 0x13;
                    wikiHelpSprite.Center = new PointF(0f, 0f);
                    wikiHelpSprite.PosX = viewportWidth - 0x1f;
                    wikiHelpSprite.PosY = num5 + 60;
                    wikiHelpSprite.Scale = 0.66f;
                    wikiHelpSprite.Update();
                }
                surroundsprites.Clear();
                int num8 = 0x11;
                if ((width < viewportWidth) && (height < viewportHeight))
                {
                    int num10;
                    int num14;
                    int num9 = (viewportHeight - height) / 2;
                    for (num10 = num9; num10 > 0; num10 -= 0x200)
                    {
                        for (int i = 0; i < viewportWidth; i += 0x200)
                        {
                            SpriteWrapper wrapper = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper.Initialize(this.gfx);
                            wrapper.Layer = num8;
                            wrapper.PosX = i;
                            wrapper.PosY = num10 - 0x200;
                            wrapper.Update();
                            surroundsprites.Add(wrapper);
                        }
                    }
                    for (num10 = ((viewportHeight - height) / 2) + height; num10 < viewportHeight; num10 += 0x200)
                    {
                        for (int j = 0; j < viewportWidth; j += 0x200)
                        {
                            SpriteWrapper wrapper2 = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper2.Initialize(this.gfx);
                            wrapper2.Layer = num8;
                            wrapper2.PosX = j;
                            wrapper2.PosY = num10;
                            wrapper2.Update();
                            surroundsprites.Add(wrapper2);
                        }
                    }
                    int num13 = (viewportWidth - width) / 2;
                    for (num14 = num13; num14 > 0; num14 -= 0x200)
                    {
                        for (int k = 0; k < viewportHeight; k += 0x200)
                        {
                            SpriteWrapper wrapper3 = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper3.Initialize(this.gfx);
                            wrapper3.Layer = num8;
                            wrapper3.PosX = num14 - 0x200;
                            wrapper3.PosY = k;
                            wrapper3.Update();
                            surroundsprites.Add(wrapper3);
                        }
                    }
                    for (num14 = ((viewportWidth - width) / 2) + width; num14 < viewportWidth; num14 += 0x200)
                    {
                        for (int m = 0; m < viewportHeight; m += 0x200)
                        {
                            SpriteWrapper wrapper4 = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper4.Initialize(this.gfx);
                            wrapper4.Layer = num8;
                            wrapper4.PosX = num14;
                            wrapper4.PosY = m;
                            wrapper4.Update();
                            surroundsprites.Add(wrapper4);
                        }
                    }
                    SpriteWrapper item = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    item.Initialize(this.gfx);
                    item.Layer = num8 + 1;
                    item.PosX = num13 - 3;
                    item.PosY = num9 - 3;
                    item.Size = (SizeF) new Size(3, height + 6);
                    item.Update();
                    surroundsprites.Add(item);
                    SpriteWrapper wrapper6 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper6.Initialize(this.gfx);
                    wrapper6.Layer = num8 + 1;
                    wrapper6.PosX = num13 + width;
                    wrapper6.PosY = num9;
                    wrapper6.Size = (SizeF) new Size(3, height);
                    wrapper6.Update();
                    surroundsprites.Add(wrapper6);
                    SpriteWrapper wrapper7 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper7.Initialize(this.gfx);
                    wrapper7.Layer = num8 + 1;
                    wrapper7.PosX = num13 + width;
                    wrapper7.PosY = num9 + 3;
                    wrapper7.Size = (SizeF) new Size(6, height);
                    wrapper7.Update();
                    surroundsprites.Add(wrapper7);
                    SpriteWrapper wrapper8 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper8.Initialize(this.gfx);
                    wrapper8.Layer = num8 + 1;
                    wrapper8.PosX = num13 + width;
                    wrapper8.PosY = num9 + 6;
                    wrapper8.Size = (SizeF) new Size(9, height);
                    wrapper8.Update();
                    surroundsprites.Add(wrapper8);
                    SpriteWrapper wrapper9 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper9.Initialize(this.gfx);
                    wrapper9.Layer = num8 + 1;
                    wrapper9.PosX = num13 + width;
                    wrapper9.PosY = num9 + 9;
                    wrapper9.Size = (SizeF) new Size(14, height);
                    wrapper9.Update();
                    surroundsprites.Add(wrapper9);
                    SpriteWrapper wrapper10 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper10.Initialize(this.gfx);
                    wrapper10.Layer = num8 + 1;
                    wrapper10.PosY = num9 - 3;
                    wrapper10.PosX = num13;
                    wrapper10.Size = (SizeF) new Size(width, 3);
                    wrapper10.Update();
                    surroundsprites.Add(wrapper10);
                    SpriteWrapper wrapper11 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper11.Initialize(this.gfx);
                    wrapper11.Layer = num8 + 1;
                    wrapper11.PosY = num9 + height;
                    wrapper11.PosX = num13;
                    wrapper11.Size = (SizeF) new Size(width, 3);
                    wrapper11.Update();
                    surroundsprites.Add(wrapper11);
                    SpriteWrapper wrapper12 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper12.Initialize(this.gfx);
                    wrapper12.Layer = num8 + 1;
                    wrapper12.PosY = num9 + height;
                    wrapper12.PosX = num13 + 3;
                    wrapper12.Size = (SizeF) new Size(width, 6);
                    wrapper12.Update();
                    surroundsprites.Add(wrapper12);
                    SpriteWrapper wrapper13 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper13.Initialize(this.gfx);
                    wrapper13.Layer = num8 + 1;
                    wrapper13.PosY = num9 + height;
                    wrapper13.PosX = num13 + 6;
                    wrapper13.Size = (SizeF) new Size(width, 9);
                    wrapper13.Update();
                    surroundsprites.Add(wrapper13);
                    SpriteWrapper wrapper14 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper14.Initialize(this.gfx);
                    wrapper14.Layer = num8 + 1;
                    wrapper14.PosY = num9 + height;
                    wrapper14.PosX = num13 + 9;
                    wrapper14.Size = (SizeF) new Size(width, 14);
                    wrapper14.Update();
                    surroundsprites.Add(wrapper14);
                }
                else if (width < viewportWidth)
                {
                    int num17 = (viewportWidth - width) / 2;
                    int num18 = num17;
                    while (num17 > 0)
                    {
                        for (int n = 0; n < viewportHeight; n += 0x200)
                        {
                            SpriteWrapper wrapper15 = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper15.Initialize(this.gfx);
                            wrapper15.Layer = num8;
                            wrapper15.PosX = num17 - 0x200;
                            wrapper15.PosY = n;
                            wrapper15.Update();
                            surroundsprites.Add(wrapper15);
                        }
                        num17 -= 0x200;
                    }
                    SpriteWrapper wrapper16 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper16.Initialize(this.gfx);
                    wrapper16.Layer = num8 + 1;
                    wrapper16.PosX = num18 - 3;
                    wrapper16.PosY = 0f;
                    wrapper16.Size = (SizeF) new Size(3, height);
                    wrapper16.Update();
                    surroundsprites.Add(wrapper16);
                    for (num17 = ((viewportWidth - width) / 2) + width; num17 < viewportWidth; num17 += 0x200)
                    {
                        for (int num20 = 0; num20 < viewportHeight; num20 += 0x200)
                        {
                            SpriteWrapper wrapper17 = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper17.Initialize(this.gfx);
                            wrapper17.Layer = num8;
                            wrapper17.PosX = num17;
                            wrapper17.PosY = num20;
                            wrapper17.Update();
                            surroundsprites.Add(wrapper17);
                        }
                    }
                    SpriteWrapper wrapper18 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper18.Initialize(this.gfx);
                    wrapper18.Layer = num8 + 1;
                    wrapper18.PosX = num18 + width;
                    wrapper18.PosY = 0f;
                    wrapper18.Size = (SizeF) new Size(3, height);
                    wrapper18.Update();
                    surroundsprites.Add(wrapper18);
                    SpriteWrapper wrapper19 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper19.Initialize(this.gfx);
                    wrapper19.Layer = num8 + 1;
                    wrapper19.PosX = num18 + width;
                    wrapper19.PosY = 0f;
                    wrapper19.Size = (SizeF) new Size(6, height);
                    wrapper19.Update();
                    surroundsprites.Add(wrapper19);
                    SpriteWrapper wrapper20 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper20.Initialize(this.gfx);
                    wrapper20.Layer = num8 + 1;
                    wrapper20.PosX = num18 + width;
                    wrapper20.PosY = 0f;
                    wrapper20.Size = (SizeF) new Size(9, height);
                    wrapper20.Update();
                    surroundsprites.Add(wrapper20);
                    SpriteWrapper wrapper21 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper21.Initialize(this.gfx);
                    wrapper21.Layer = num8 + 1;
                    wrapper21.PosX = num18 + width;
                    wrapper21.PosY = 0f;
                    wrapper21.Size = (SizeF) new Size(14, height);
                    wrapper21.Update();
                    surroundsprites.Add(wrapper21);
                }
                else if (height < viewportHeight)
                {
                    int num21 = (viewportHeight - height) / 2;
                    int num22 = num21;
                    while (num21 > 0)
                    {
                        for (int num23 = 0; num23 < viewportWidth; num23 += 0x200)
                        {
                            SpriteWrapper wrapper22 = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper22.Initialize(this.gfx);
                            wrapper22.Layer = num8;
                            wrapper22.PosX = num23;
                            wrapper22.PosY = num21 - 0x200;
                            wrapper22.Update();
                            surroundsprites.Add(wrapper22);
                        }
                        num21 -= 0x200;
                    }
                    SpriteWrapper wrapper23 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper23.Initialize(this.gfx);
                    wrapper23.Layer = num8 + 1;
                    wrapper23.PosY = num22 - 3;
                    wrapper23.PosX = 0f;
                    wrapper23.Size = (SizeF) new Size(width, 3);
                    wrapper23.Update();
                    surroundsprites.Add(wrapper23);
                    for (num21 = ((viewportHeight - height) / 2) + height; num21 < viewportHeight; num21 += 0x200)
                    {
                        for (int num24 = 0; num24 < viewportWidth; num24 += 0x200)
                        {
                            SpriteWrapper wrapper24 = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper24.Initialize(this.gfx);
                            wrapper24.Layer = num8;
                            wrapper24.PosX = num24;
                            wrapper24.PosY = num21;
                            wrapper24.Update();
                            surroundsprites.Add(wrapper24);
                        }
                    }
                    SpriteWrapper wrapper25 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper25.Initialize(this.gfx);
                    wrapper25.Layer = num8 + 1;
                    wrapper25.PosY = num22 + height;
                    wrapper25.PosX = 0f;
                    wrapper25.Size = (SizeF) new Size(width, 3);
                    wrapper25.Update();
                    surroundsprites.Add(wrapper25);
                    SpriteWrapper wrapper26 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper26.Initialize(this.gfx);
                    wrapper26.Layer = num8 + 1;
                    wrapper26.PosY = num22 + height;
                    wrapper26.PosX = 0f;
                    wrapper26.Size = (SizeF) new Size(width, 6);
                    wrapper26.Update();
                    surroundsprites.Add(wrapper26);
                    SpriteWrapper wrapper27 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper27.Initialize(this.gfx);
                    wrapper27.Layer = num8 + 1;
                    wrapper27.PosY = num22 + height;
                    wrapper27.PosX = 0f;
                    wrapper27.Size = (SizeF) new Size(width, 9);
                    wrapper27.Update();
                    surroundsprites.Add(wrapper27);
                    SpriteWrapper wrapper28 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper28.Initialize(this.gfx);
                    wrapper28.Layer = num8 + 1;
                    wrapper28.PosY = num22 + height;
                    wrapper28.PosX = 0f;
                    wrapper28.Size = (SizeF) new Size(width, 14);
                    wrapper28.Update();
                    surroundsprites.Add(wrapper28);
                }
            }
        }

        public void DEBUG_SaveAIWorldSetup(string filename)
        {
            TextWriter writer = new StreamWriter(filename);
            foreach (CampCastleElement element in this.castleLayout.createCastleCampArray_MemoriseAttackSetupTroops())
            {
                byte[] buffer = new byte[6];
                buffer[0] = element.elementType;
                buffer[1] = element.xPos;
                buffer[2] = element.yPos;
                if (element.elementType == 0x5e)
                {
                    Point point = this.getCatapultAttackLocation(element.xPos, element.yPos);
                    buffer[3] = (byte) point.X;
                    buffer[4] = (byte) point.Y;
                }
                if ((element.elementType >= 100) && (element.elementType < 0x6d))
                {
                    int num = this.getCaptainsDelayValue(element.xPos, element.yPos);
                    buffer[5] = (byte) num;
                    if ((element.elementType == 0x66) || (element.elementType == 0x67))
                    {
                        Point point2 = this.getCatapultAttackLocation(element.xPos, element.yPos);
                        buffer[3] = (byte) point2.X;
                        buffer[4] = (byte) point2.Y;
                    }
                }
                writer.WriteLine(string.Concat(new object[] { buffer[0], ",", buffer[1], ",", buffer[2], ",", buffer[3], ",", buffer[4], ",", buffer[5], "," }));
            }
            writer.Close();
        }

        public void DEBUG_SaveCastleMap(string filename)
        {
            FileStream output = new FileStream(filename, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(output);
            byte[] buffer = this.generateCastleMapSnapshot();
            byte[] buffer2 = this.generateCastleTroopsSnapshot();
            byte[] buffer4 = CastlesCommon.compressCastleData(this.castleLayout.createAttackerMapArray());
            writer.Write(buffer.Length);
            writer.Write(buffer, 0, buffer.Length);
            writer.Write(buffer2.Length);
            writer.Write(buffer2, 0, buffer2.Length);
            writer.Write(buffer4.Length);
            writer.Write(buffer4, 0, buffer4.Length);
            writer.Close();
            output.Close();
        }

        public void deleteAllAttackers()
        {
            List<CastleElement> list = new List<CastleElement>();
            foreach (CastleElement element in this.elements)
            {
                if ((element.elementType >= 90) && (element.elementType <= 0x6d))
                {
                    this.deleteCatapultTarget(element.elementID);
                    this.deleteCaptainsDetails(element.elementID);
                    list.Add(element);
                    this.castleLayout.removeTroop(element.xPos, element.yPos, element.elementID);
                }
            }
            foreach (CastleElement element2 in list)
            {
                this.elements.Remove(element2);
            }
            this.recalcCastleLayout();
        }

        public void deleteAllElements()
        {
            if (this.inDeleteConstructing)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastDeleteConstructing);
                if (span.TotalMinutes > 2.0)
                {
                    this.inDeleteConstructing = false;
                }
            }
            if (!this.inDeleteConstructing)
            {
                this.inDeleting = true;
                this.inDeleteConstructing = true;
                this.lastDeleteConstructing = DateTime.Now;
                RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
                RemoteServices.Instance.DeleteAllCastleElements(this.m_villageID);
                this.stopPlaceElement();
            }
        }

        public void deleteAllMoatElements()
        {
            if (this.inDeleteConstructing)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastDeleteConstructing);
                if (span.TotalMinutes > 2.0)
                {
                    this.inDeleteConstructing = false;
                }
            }
            if (!this.inDeleteConstructing)
            {
                this.inDeleting = true;
                this.inDeleteConstructing = true;
                this.lastDeleteConstructing = DateTime.Now;
                RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
                RemoteServices.Instance.DeleteAllCastleMoatElements(this.m_villageID);
                this.stopPlaceElement();
            }
        }

        public void deleteAllOilPotElements()
        {
            if (this.inDeleteConstructing)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastDeleteConstructing);
                if (span.TotalMinutes > 2.0)
                {
                    this.inDeleteConstructing = false;
                }
            }
            if (!this.inDeleteConstructing)
            {
                this.inDeleting = true;
                this.inDeleteConstructing = true;
                this.lastDeleteConstructing = DateTime.Now;
                RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
                RemoteServices.Instance.DeleteAllCastleOilPotsElements(this.m_villageID);
                this.stopPlaceElement();
            }
        }

        public void deleteAllPitsElements()
        {
            if (this.inDeleteConstructing)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastDeleteConstructing);
                if (span.TotalMinutes > 2.0)
                {
                    this.inDeleteConstructing = false;
                }
            }
            if (!this.inDeleteConstructing)
            {
                this.inDeleting = true;
                this.inDeleteConstructing = true;
                this.lastDeleteConstructing = DateTime.Now;
                RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
                RemoteServices.Instance.DeleteAllCastlePitsElements(this.m_villageID);
                this.stopPlaceElement();
            }
        }

        public bool deleteAttackSetup(string name)
        {
            try
            {
                File.Delete(this.getAttackSetupSaveName(name));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void deleteCaptainsDetails(long elemID)
        {
            foreach (CaptainsDetails details in this.captainsDetails)
            {
                if (details.elemID == elemID)
                {
                    this.captainsDetails.Remove(details);
                    break;
                }
            }
        }

        public void deleteCatapultTarget(long elemID)
        {
            foreach (CatapultTarget target in this.catapultTargets)
            {
                if (target.elemID == elemID)
                {
                    this.catapultTargets.Remove(target);
                    break;
                }
            }
        }

        public void deleteConstructingElements()
        {
            if (this.inDeleteConstructing)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastDeleteConstructing);
                if (span.TotalMinutes > 2.0)
                {
                    this.inDeleteConstructing = false;
                }
            }
            if (!this.inDeleteConstructing)
            {
                this.inDeleting = true;
                this.inDeleteConstructing = true;
                this.lastDeleteConstructing = DateTime.Now;
                RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
                RemoteServices.Instance.DeleteConstructingCastleElements(this.m_villageID);
                this.stopPlaceElement();
            }
        }

        public void DeleteElementCallback(DeleteCastleElement_ReturnType returnData)
        {
            this.inDeleteConstructing = false;
            if (this.inDeleting)
            {
                this.inDeleting = false;
                CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
            }
            if (returnData.Success)
            {
                setServerTime(returnData.currentTime);
                GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                if ((returnData.villageResourcesAndStats != null) && (GameEngine.Instance.Village != null))
                {
                    GameEngine.Instance.Village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    VillageMap village = GameEngine.Instance.Village;
                    if (village != null)
                    {
                        this.numAvailableDefenderPeasants = 0;
                        this.numAvailableDefenderArchers = 0;
                        this.numAvailableDefenderPikemen = 0;
                        this.numAvailableDefenderSwordsmen = 0;
                        this.numAvailableDefenderCaptains = 0;
                        village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
                        GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
                        village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
                    }
                }
                if (returnData.elements != null)
                {
                    this.importElements(returnData.elements);
                }
                InterfaceMgr.Instance.refreshCastleInterface();
            }
            else
            {
                MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("CastleMap_Placement_Error", "Castle Placement Error"));
            }
        }

        public void deleteTroops(Point mousePos)
        {
            Point point = mousePos;
            point.X += -((int) backgroundSprite.DrawPos.X);
            point.Y += -((int) backgroundSprite.DrawPos.Y);
            if (placingDefender)
            {
                long elementID = this.clickFindTroop(point);
                if ((elementID >= 0L) || (CreateMode && (elementID != -2L)))
                {
                    if (!CreateMode)
                    {
                        RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
                        RemoteServices.Instance.DeleteCastleElement(this.m_villageID, elementID);
                    }
                    foreach (CastleElement element in this.elements)
                    {
                        if (element.elementID != elementID)
                        {
                            continue;
                        }
                        this.elements.Remove(element);
                        VillageMap village = GameEngine.Instance.Village;
                        if (village != null)
                        {
                            switch (element.elementType)
                            {
                                case 70:
                                    village.addTroops(-1, 0, 0, 0, 0);
                                    break;

                                case 0x47:
                                    village.addTroops(0, 0, 0, -1, 0);
                                    break;

                                case 0x48:
                                    village.addTroops(0, -1, 0, 0, 0);
                                    break;

                                case 0x49:
                                    village.addTroops(0, 0, -1, 0, 0);
                                    break;
                            }
                            this.numAvailableDefenderPeasants = 0;
                            this.numAvailableDefenderArchers = 0;
                            this.numAvailableDefenderPikemen = 0;
                            this.numAvailableDefenderSwordsmen = 0;
                            this.numAvailableDefenderCaptains = 0;
                            village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
                            GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
                            village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
                        }
                        CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                        this.recalcCastleLayout();
                        CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
                        break;
                    }
                }
            }
            else
            {
                long num2 = this.clickFindTroop(point);
                if (num2 < -2L)
                {
                    foreach (CastleElement element2 in this.elements)
                    {
                        if (element2.elementID == num2)
                        {
                            this.elements.Remove(element2);
                            this.deleteCatapultTarget(element2.elementID);
                            this.deleteCaptainsDetails(element2.elementID);
                            CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
                            CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                            this.recalcCastleLayout();
                            break;
                        }
                    }
                }
            }
        }

        public void dispose()
        {
            this.elements.Clear();
        }

        private void doFireList()
        {
            foreach (BattleFire fire in this.castleCombat.getFireList())
            {
                Point point = (castleUnitSpritePoint[fire.xPos, fire.yPos]);
                int num = 0;
                if (displayCollapsed)
                {
                    num = this.castleLayout.collapsedHeightMap[fire.xPos, fire.yPos];
                }
                else
                {
                    num = this.castleLayout.fullHeightMap[fire.xPos, fire.yPos];
                }
                int spriteNo = 0;
                if (fire.state == 0)
                {
                    spriteNo = this.fireStart[fire.animFrame];
                }
                else if (fire.state == 1)
                {
                    spriteNo = 7 + this.fireLoop[(fire.animFrame + fire.randValue) % this.fireLoop.Length];
                }
                else if (fire.state == 2)
                {
                    spriteNo = 0x29 + this.fireEnd[fire.animFrame];
                }
                SpriteWrapper child = this.getNextExtraSprite(GFXLibrary.Instance.FireTexID, spriteNo);
                PointF tf = new PointF(50f, (float) (0x53 + num));
                child.Center = tf;
                child.PosX = point.X;
                child.PosY = point.Y;
                backgroundSprite.AddChild(child, 2);
            }
        }

        public void dragHandle(Point mousePos, int handleBeingDragged)
        {
            int mapX = -1;
            int mapY = -1;
            this.getMapTileFromMousePos(mousePos, ref mapX, ref mapY);
            Point point = this.getScreenPosFromMapTile(new Point(mapX, mapY));
            if (handleBeingDragged == 1)
            {
                if ((mapX != this.startWallMapX) || (mapY != this.startWallMapY))
                {
                    this.startWallMapX = mapX;
                    this.startWallMapY = mapY;
                    placementSprite_handleone.PosX = point.X;
                    placementSprite_handleone.PosY = point.Y + 0x20;
                    this.wallMouseMove(this.lastValidWallX, this.lastValidWallY, true);
                }
            }
            else if ((handleBeingDragged == 2) && ((mapX != this.lastMoveTileX) || (mapY != this.lastMoveTileY)))
            {
                this.lastValidWallX = mapX;
                this.lastValidWallY = mapY;
                placementSprite_handletwo.PosX = point.X + 0x20;
                placementSprite_handletwo.PosY = point.Y + 0x20;
                this.wallMouseMove(this.lastValidWallX, this.lastValidWallY, true);
            }
        }

        public void dragTroopPlacer(Point mousePos)
        {
            int mapX = -1;
            int mapY = -1;
            this.getMapTileFromMousePos(mousePos, ref mapX, ref mapY);
            this.troopsFollowMouse(mapX, mapY);
        }

        private void drawArrows()
        {
            foreach (BattleArrow arrow in this.castleCombat.getArrows())
            {
                int textureID = 0;
                int num2 = 0;
                if (arrow.bolt)
                {
                    textureID = GFXLibrary.Instance.Missile2TexID;
                    num2 = 50;
                }
                else
                {
                    textureID = GFXLibrary.Instance.MissileTexID;
                    num2 = 0x1c;
                }
                for (int i = 0; i < arrow.trail.Length; i++)
                {
                    if ((arrow.trail[i] != null) && arrow.trail[i].visible)
                    {
                        SpriteWrapper wrapper = this.getNextExtraSprite(textureID, arrow.trail[i].gfx + (arrow.trail[i].tilt * 0x10));
                        PointF tf = new PointF((float) num2, (float) ((num2 + 0x1b) + arrow.trail[i].height));
                        wrapper.Center = tf;
                        wrapper.PosX = arrow.trail[i].pos.X;
                        wrapper.PosY = arrow.trail[i].pos.Y;
                        wrapper.ColorToUse = Color.FromArgb(0xff - ((0xff * i) / arrow.trail.Length), ARGBColors.White);
                        backgroundSprite.AddChild(wrapper, 2);
                    }
                }
                Point point = (castleUnitSpritePoint[arrow.startX, arrow.startY]);
                Point point2 = (castleUnitSpritePoint[arrow.targetX, arrow.targetY]);
                int startHeight = 0;
                int targetHeight = 0;
                float num6 = ((float) arrow.travelledDist) / ((float) arrow.fullDist);
                int tilt = 4;
                if (displayCollapsed)
                {
                    if (arrow.turretArrow)
                    {
                        startHeight = arrow.turrentCollapsedHeight;
                    }
                    else
                    {
                        startHeight = this.castleLayout.collapsedHeightMap[arrow.startX, arrow.startY];
                    }
                    targetHeight = this.castleLayout.collapsedHeightMap[arrow.targetX, arrow.targetY];
                    if (arrow.tilt < 0)
                    {
                        arrow.tilt = this.calcTilt(arrow, startHeight, targetHeight);
                    }
                    tilt = arrow.tilt;
                }
                else
                {
                    if (arrow.turretArrow)
                    {
                        startHeight = arrow.turretFullHeight;
                    }
                    else
                    {
                        startHeight = this.castleLayout.fullHeightMap[arrow.startX, arrow.startY];
                    }
                    targetHeight = this.castleLayout.fullHeightMap[arrow.targetX, arrow.targetY];
                    if (arrow.tiltHigh < 0)
                    {
                        arrow.tiltHigh = this.calcTilt(arrow, startHeight, targetHeight);
                    }
                    tilt = arrow.tiltHigh;
                }
                point2.X = ((int) ((point2.X - point.X) * num6)) + point.X;
                point2.Y = ((int) ((point2.Y - point.Y) * num6)) + point.Y;
                targetHeight = ((int) ((targetHeight - startHeight) * num6)) + startHeight;
                SpriteWrapper child = this.getNextExtraSprite(textureID, arrow.gfxDirc + (tilt * 0x10));
                PointF tf2 = new PointF((float) num2, (float) ((num2 + 0x1b) + targetHeight));
                child.Center = tf2;
                child.PosX = point2.X;
                child.PosY = point2.Y;
                backgroundSprite.AddChild(child, 2);
                if ((!this.castleCombat.Paused && (!arrow.bolt || ((this.castleCombat.TickValue & 1) == 0))) && (arrow.trail[0] != null))
                {
                    for (int j = arrow.trail.Length - 1; j > 0; j--)
                    {
                        arrow.trail[j].pos = arrow.trail[j - 1].pos;
                        arrow.trail[j].height = arrow.trail[j - 1].height;
                        arrow.trail[j].visible = arrow.trail[j - 1].visible;
                        arrow.trail[j].tilt = arrow.trail[j - 1].tilt;
                        arrow.trail[j].gfx = arrow.trail[j - 1].gfx;
                    }
                    arrow.trail[0].pos = new PointF((float) point2.X, (float) point2.Y);
                    arrow.trail[0].height = targetHeight;
                    arrow.trail[0].gfx = arrow.gfxDirc;
                    arrow.trail[0].tilt = tilt;
                    arrow.trail[0].visible = true;
                }
            }
        }

        private void drawCastleLoop(bool collapsed, ref bool completed, ref DateTime completeTime, DateTime curTime)
        {
            foreach (CastleElement element in this.elements)
            {
                Rectangle rectangle;
                PointF tf;
                SizeF ef;
                SpriteWrapper wrapper6;
                PointF tf2;
                SpriteWrapper wrapper7;
                SpriteWrapper wrapper8;
                PointF tf4;
                SpriteWrapper wrapper9;
                SpriteWrapper wrapper10;
                int elementType;
                int num17;
                int xPos = element.xPos;
                int yPos = element.yPos;
                Color white = ARGBColors.White;
                if (element.elementType >= 0x45)
                {
                    continue;
                }
                SpriteWrapper sprite = castleSpriteGrid[xPos, yPos];
                if ((sprite == null) || (this.debugDisplayMode > 0))
                {
                    continue;
                }
                int spriteNo = -1;
                spriteNo = this.initCastleSprite(sprite, element.elementType, xPos, yPos, collapsed, element);
                if (spriteNo < 0)
                {
                    continue;
                }
                activeCastleInfrastructureElements[element.elementID] = element;
                bool flag = false;
                int castleSpritesTexID = GFXLibrary.Instance.CastleSpritesTexID;
                switch (element.elementType)
                {
                    case 0x33:
                    case 0x34:
                    case 0x35:
                    case 0x36:
                    case 0x37:
                    case 0x38:
                    case 0x39:
                        castleSpritesTexID = GFXLibrary.Instance.FreeCardIconsID;
                        flag = true;
                        break;
                }
                if ((!flag || !this.attackerSetupForest) || (yPos >= 0x21))
                {
                    sprite.reInitializeSpecial(castleSpritesTexID, spriteNo);
                    if (sprite.Visible)
                    {
                        backgroundSprite.AddChild(sprite, 2);
                    }
                    if (this.displayType == 0)
                    {
                        if ((element.completionTime > curTime) && !this.inBuilderMode)
                        {
                            sprite.ColorToUse = Color.FromArgb(0x80, ARGBColors.White);
                        }
                    }
                    else if (((this.displayType != 1) && (this.displayType == 2)) && (element.completionTime > curTime))
                    {
                        sprite.Visible = false;
                    }
                    if (element.completionTime > completeTime)
                    {
                        completeTime = element.completionTime;
                        completed = false;
                    }
                    white = sprite.ColorToUse;
                    int num5 = (int) (element.damage * 10f);
                    int num6 = CastleCombat.GetInfrastructureMaxDamage(GameEngine.Instance.LocalWorldData, element.elementType, this.getDefenderDefenceResearch(), this.getLandType()) * 10;
                    int green = 0xc0 - ((num5 * 0xc0) / num6);
                    if (green < 0)
                    {
                        green = 0;
                    }
                    if (num5 != 0)
                    {
                        if ((element.elementType != 0x24) || !this.battleMode)
                        {
                            this.castleDamaged = true;
                            if (element.elementType != 0x23)
                            {
                                white = Color.FromArgb(white.A, 0xff, green, green);
                            }
                            else
                            {
                                white = Color.FromArgb(white.A, green, 0xff, green);
                            }
                        }
                    }
                    else if (this.inBuilderMode && (element.elementID >= -1L))
                    {
                        white = Color.FromArgb(0xff, 0x7f, 0x7f, 0x7f);
                    }
                    sprite.ColorToUse = white;
                    if ((!this.battleMode && (element.elementID == this.deletingHighlightElementID)) && (!this.inBuilderMode || (element.elementID < -1L)))
                    {
                        sprite.ColorToUse = Color.FromArgb(0x7f, 0x20, 0x20, 0x20);
                    }
                    switch (element.elementType)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                            elementType = element.elementType;
                            if (fakeKeep >= 0)
                            {
                                elementType = fakeKeep;
                            }
                            if (!displayCollapsed)
                            {
                                goto Label_0A42;
                            }
                            if (this.displayType == 1)
                            {
                                int spriteID = 0;
                                if (this.campMode == 0)
                                {
                                    spriteID = (0x19d + elementType) - 1;
                                }
                                else if (this.campMode == 1)
                                {
                                    spriteID = 0x1c1;
                                }
                                else if (this.campMode == 2)
                                {
                                    spriteID = 0x1bc;
                                }
                                SpriteWrapper wrapper11 = this.getNextExtraSprite(spriteID);
                                PointF tf7 = new PointF(96f, 0f);
                                int num16 = 1;
                                this.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref num16).GetSpriteXYdata(num16, wrapper11.SpriteNo, out rectangle, out tf, out ef);
                                tf7.Y = (int) ef.Height;
                                wrapper11.Center = tf7;
                                wrapper11.PosY = 40f;
                                wrapper11.ColorToUse = Color.FromArgb(160, white);
                                sprite.DrawChildrenWithParent = true;
                                sprite.AddChildAsLast(wrapper11);
                            }
                            break;

                        case 11:
                            if (!displayCollapsed && ((this.displayType != 0) || (element.completionTime <= curTime)))
                            {
                                SpriteWrapper wrapper2 = this.getNextExtraSprite(0x2b);
                                wrapper2.Center = new PointF(32f, 42f);
                                wrapper2.PosY = -121f;
                                wrapper2.ColorToUse = white;
                                sprite.DrawChildrenWithParent = true;
                                sprite.AddChildAsLast(wrapper2);
                            }
                            break;

                        case 12:
                            if (!displayCollapsed && ((this.displayType != 0) || (element.completionTime <= curTime)))
                            {
                                SpriteWrapper wrapper3 = this.getNextExtraSprite(0x2c);
                                wrapper3.Center = new PointF(48f, 57f);
                                wrapper3.PosY = -120f;
                                wrapper3.ColorToUse = white;
                                sprite.DrawChildrenWithParent = true;
                                sprite.AddChildAsLast(wrapper3);
                            }
                            break;

                        case 13:
                            if (!displayCollapsed && ((this.displayType != 0) || (element.completionTime <= curTime)))
                            {
                                SpriteWrapper wrapper4 = this.getNextExtraSprite(0x2d);
                                wrapper4.Center = new PointF(64f, 66f);
                                wrapper4.PosY = -120f;
                                wrapper4.ColorToUse = white;
                                sprite.DrawChildrenWithParent = true;
                                sprite.AddChildAsLast(wrapper4);
                            }
                            break;

                        case 14:
                            if (!displayCollapsed && ((this.displayType != 0) || (element.completionTime <= curTime)))
                            {
                                SpriteWrapper wrapper5 = this.getNextExtraSprite(0x2e);
                                wrapper5.Center = new PointF(80f, 74f);
                                wrapper5.PosY = -124f;
                                wrapper5.ColorToUse = white;
                                sprite.DrawChildrenWithParent = true;
                                sprite.AddChildAsLast(wrapper5);
                            }
                            break;

                        case 0x1f:
                            if (element.completionTime <= curTime)
                            {
                                this.numGuardHouses++;
                            }
                            break;

                        case 0x20:
                            if (element.completionTime <= curTime)
                            {
                                this.numSmelter++;
                            }
                            break;

                        case 0x25:
                        case 0x26:
                            if (!this.battleMode)
                            {
                                break;
                            }
                            if (!displayCollapsed)
                            {
                                goto Label_0614;
                            }
                            wrapper6 = null;
                            if (element.elementType != 0x25)
                            {
                                goto Label_0573;
                            }
                            wrapper6 = this.getNextExtraSprite(0x1b0);
                            goto Label_0580;

                        case 0x27:
                        case 40:
                            if (!this.battleMode)
                            {
                                break;
                            }
                            if (!displayCollapsed)
                            {
                                goto Label_07A3;
                            }
                            wrapper8 = null;
                            if (element.elementType != 0x27)
                            {
                                goto Label_0702;
                            }
                            wrapper8 = this.getNextExtraSprite(0x1ca);
                            goto Label_070F;

                        case 0x2a:
                        {
                            wrapper10 = null;
                            if (!this.battleMode)
                            {
                                goto Label_08A4;
                            }
                            BattleBuilding building = (BattleBuilding) element;
                            int num12 = (building.facing + 6) & 7;
                            num12 += building.animFrame * 8;
                            wrapper10 = this.getNextExtraSprite(GFXLibrary.Instance.BallistaTexID, num12);
                            goto Label_08B7;
                        }
                    }
                }
                continue;
            Label_0573:
                wrapper6 = this.getNextExtraSprite(0x1ac);
            Label_0580:
                tf2 = new PointF(64f, 0f);
                int spriteTag = 1;
                this.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref spriteTag).GetSpriteXYdata(spriteTag, wrapper6.SpriteNo, out rectangle, out tf, out ef);
                tf2.Y = ((int) ef.Height) - 9;
                wrapper6.Center = tf2;
                wrapper6.PosY = 24f;
                wrapper6.ColorToUse = Color.FromArgb(160, white);
                sprite.DrawChildrenWithParent = true;
                sprite.AddChildAsLast(wrapper6);
                continue;
            Label_0614:
                wrapper7 = null;
                if (element.elementType == 0x25)
                {
                    wrapper7 = this.getNextExtraSprite(0x1af);
                }
                else
                {
                    wrapper7 = this.getNextExtraSprite(0x1ab);
                }
                PointF tf3 = new PointF(64f, 0f);
                int num9 = 1;
                this.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref num9).GetSpriteXYdata(num9, wrapper7.SpriteNo, out rectangle, out tf, out ef);
                tf3.Y = ((int) ef.Height) - 9;
                wrapper7.Center = tf3;
                wrapper7.PosY = 24f;
                wrapper7.ColorToUse = Color.FromArgb(160, white);
                sprite.DrawChildrenWithParent = true;
                sprite.AddChildAsLast(wrapper7);
                continue;
            Label_0702:
                wrapper8 = this.getNextExtraSprite(0x1c6);
            Label_070F:
                tf4 = new PointF(64f, 0f);
                int num10 = 1;
                this.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref num10).GetSpriteXYdata(num10, wrapper8.SpriteNo, out rectangle, out tf, out ef);
                tf4.Y = ((int) ef.Height) - 9;
                wrapper8.Center = tf4;
                wrapper8.PosY = 24f;
                wrapper8.ColorToUse = Color.FromArgb(160, white);
                sprite.DrawChildrenWithParent = true;
                sprite.AddChildAsLast(wrapper8);
                continue;
            Label_07A3:
                wrapper9 = null;
                if (element.elementType == 0x27)
                {
                    wrapper9 = this.getNextExtraSprite(0x1c9);
                }
                else
                {
                    wrapper9 = this.getNextExtraSprite(0x1c5);
                }
                PointF tf5 = new PointF(64f, 0f);
                int num11 = 1;
                this.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref num11).GetSpriteXYdata(num11, wrapper9.SpriteNo, out rectangle, out tf, out ef);
                tf5.Y = ((int) ef.Height) - 9;
                wrapper9.Center = tf5;
                wrapper9.PosY = 24f;
                wrapper9.ColorToUse = Color.FromArgb(160, white);
                sprite.DrawChildrenWithParent = true;
                sprite.AddChildAsLast(wrapper9);
                continue;
            Label_08A4:
                wrapper10 = this.getNextExtraSprite(GFXLibrary.Instance.BallistaTexID, 0);
            Label_08B7:
                if (collapsed)
                {
                    wrapper10.PosY = -16f;
                }
                else
                {
                    wrapper10.PosY = -100f;
                }
                PointF tf6 = new PointF(65f, 65f);
                int num13 = 1;
                this.gfx.getSpriteLoader(GFXLibrary.Instance.BallistaTexID, ref num13).GetSpriteXYdata(num13, wrapper10.SpriteNo, out rectangle, out tf, out ef);
                wrapper10.Center = tf6;
                wrapper10.ColorToUse = white;
                sprite.DrawChildrenWithParent = true;
                sprite.AddChild(wrapper10);
                continue;
            Label_0A42:
                num17 = 0;
                if (this.campMode == 0)
                {
                    num17 = (0x102 + elementType) - 1;
                }
                else if (this.campMode == 1)
                {
                    num17 = 450;
                }
                else if (this.campMode == 2)
                {
                    num17 = 0x1bd;
                }
                SpriteWrapper child = this.getNextExtraSprite(num17);
                PointF tf8 = new PointF(96f, 0f);
                int num18 = 1;
                this.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref num18).GetSpriteXYdata(num18, child.SpriteNo, out rectangle, out tf, out ef);
                tf8.Y = (int) ef.Height;
                child.Center = tf8;
                child.PosY = 40f;
                child.ColorToUse = Color.FromArgb(160, white);
                sprite.DrawChildrenWithParent = true;
                sprite.AddChildAsLast(child);
            }
        }

        public void drawCatapultLines()
        {
            foreach (CatapultLine line in this.catapultLines)
            {
                this.gfx.startThickLine(Color.FromArgb(0, 0xff, 0), 3f);
                this.gfx.setThickLineRadius(1f);
                Point point = (castleUnitSpritePoint[line.startX, line.startY]);
                Point point2 = (castleUnitSpritePoint[line.endX, line.endY]);
                this.gfx.addThickLinePoint(point.X + backgroundSprite.DrawPos.X, point.Y + backgroundSprite.DrawPos.Y);
                this.gfx.addThickLinePoint(point2.X + backgroundSprite.DrawPos.X, point2.Y + backgroundSprite.DrawPos.Y);
                this.gfx.drawThickLines(true);
            }
        }

        private void drawDyingTroops()
        {
            foreach (BattleTroop troop in this.castleCombat.getDyingTroops())
            {
                int num8;
                int xPos = troop.xPos;
                int yPos = troop.yPos;
                int textureID = -1;
                int knightTopAnimTexID = -1;
                int spriteNo = 0;
                int num6 = -1;
                int num7 = 0x20;
                if (troop.dyingOnFire)
                {
                    textureID = GFXLibrary.Instance.ManOnFireTexID;
                    spriteNo = (troop.facing + 6) & 7;
                    spriteNo += this.dyingOnFire[Math.Min(troop.animFrame, this.dyingOnFire.Length - 1)] * 8;
                    num7 = 0x27;
                }
                else
                {
                    switch (troop.elementType)
                    {
                        case 70:
                            textureID = GFXLibrary.Instance.PeasantAnimTexID;
                            if (!troop.dyingArrowAttack)
                            {
                                goto Label_0565;
                            }
                            spriteNo = 0x80 + this.peasantDyingArrow[Math.Min(troop.animFrame, this.peasantDyingArrow.Length - 1)];
                            break;

                        case 0x47:
                            textureID = GFXLibrary.Instance.SwordsmanAnimTexID;
                            if (!troop.dyingArrowAttack)
                            {
                                goto Label_0243;
                            }
                            spriteNo = 400 + this.swordsmanDyingArrow[Math.Min(troop.animFrame, this.swordsmanDyingArrow.Length - 1)];
                            break;

                        case 0x48:
                            textureID = GFXLibrary.Instance.ArcherAnimTexID;
                            if (!troop.dyingArrowAttack)
                            {
                                goto Label_0177;
                            }
                            spriteNo = 0x1f0 + this.archerDyingArrow[Math.Min(troop.animFrame, this.archerDyingArrow.Length - 1)];
                            break;

                        case 0x49:
                            textureID = GFXLibrary.Instance.PikemanAnimTexID;
                            if (!troop.dyingArrowAttack)
                            {
                                goto Label_0379;
                            }
                            spriteNo = 0xd8 + this.pikemanDyingArrow[Math.Min(troop.animFrame, this.pikemanDyingArrow.Length - 1)];
                            break;

                        case 0x4b:
                            textureID = GFXLibrary.Instance.OilPotAnimTexID;
                            break;

                        case 0x4d:
                            textureID = GFXLibrary.Instance.WolfAnimTexID;
                            if (!troop.dyingArrowAttack)
                            {
                                goto Label_03DF;
                            }
                            spriteNo = 440 + this.wolfDyingArrow[Math.Min(troop.animFrame, this.wolfDyingArrow.Length - 1)];
                            break;

                        case 0x4e:
                            textureID = GFXLibrary.Instance.KnightAnimTexID;
                            knightTopAnimTexID = GFXLibrary.Instance.KnightTopAnimTexID;
                            if (!troop.dyingArrowAttack)
                            {
                                goto Label_0475;
                            }
                            spriteNo = 0xb0 + this.knightDyingArrow[Math.Min(troop.animFrame, this.knightDyingArrow.Length - 1)];
                            num6 = 0xf8 + this.knightDyingArrow[Math.Min(troop.animFrame, this.knightDyingArrow.Length - 1)];
                            break;

                        case 0x55:
                            textureID = GFXLibrary.Instance.CaptainAnimTexID;
                            spriteNo = 0x23f + this.captainDyingAnim[Math.Min(troop.animFrame, this.captainDyingAnim.Length - 1)];
                            break;

                        case 90:
                            textureID = GFXLibrary.Instance.PeasantRedAnimTexID;
                            if (!troop.dyingArrowAttack)
                            {
                                goto Label_05C5;
                            }
                            spriteNo = 0x80 + this.peasantDyingArrow[Math.Min(troop.animFrame, this.peasantDyingArrow.Length - 1)];
                            break;

                        case 0x5b:
                            textureID = GFXLibrary.Instance.SwordsmanRedAnimTexID;
                            if (!troop.dyingArrowAttack)
                            {
                                goto Label_02A9;
                            }
                            spriteNo = 400 + this.swordsmanDyingArrow[Math.Min(troop.animFrame, this.swordsmanDyingArrow.Length - 1)];
                            break;

                        case 0x5c:
                            textureID = GFXLibrary.Instance.ArcherRedAnimTexID;
                            if (!troop.dyingArrowAttack)
                            {
                                goto Label_01DD;
                            }
                            spriteNo = 0x1f0 + this.archerDyingArrow[Math.Min(troop.animFrame, this.archerDyingArrow.Length - 1)];
                            break;

                        case 0x5d:
                            textureID = GFXLibrary.Instance.PikemanRedAnimTexID;
                            if (!troop.dyingArrowAttack)
                            {
                                goto Label_04FF;
                            }
                            spriteNo = 0xd8 + this.pikemanDyingArrow[Math.Min(troop.animFrame, this.pikemanDyingArrow.Length - 1)];
                            break;

                        case 0x5e:
                            textureID = GFXLibrary.Instance.CatapultAnimTexID;
                            break;

                        case 100:
                        case 0x65:
                        case 0x66:
                        case 0x67:
                        case 0x68:
                        case 0x69:
                        case 0x6a:
                        case 0x6b:
                            textureID = GFXLibrary.Instance.CaptainAnimRedTexID;
                            spriteNo = 0x23f + this.captainDyingAnim[Math.Min(troop.animFrame, this.captainDyingAnim.Length - 1)];
                            break;
                    }
                }
                goto Label_0605;
            Label_0177:
                spriteNo = 0x1d0 + this.archerDyingNormal[Math.Min(troop.animFrame, this.archerDyingNormal.Length - 1)];
                goto Label_0605;
            Label_01DD:
                spriteNo = 0x1d0 + this.archerDyingNormal[Math.Min(troop.animFrame, this.archerDyingNormal.Length - 1)];
                goto Label_0605;
            Label_0243:
                spriteNo = 0x160 + this.swordsmanDyingNormal[Math.Min(troop.animFrame, this.swordsmanDyingNormal.Length - 1)];
                goto Label_0605;
            Label_02A9:
                spriteNo = 0x160 + this.swordsmanDyingNormal[Math.Min(troop.animFrame, this.swordsmanDyingNormal.Length - 1)];
                goto Label_0605;
            Label_0379:
                spriteNo = 0xc0 + this.pikemanDyingNormal[Math.Min(troop.animFrame, this.pikemanDyingNormal.Length - 1)];
                goto Label_0605;
            Label_03DF:
                spriteNo = 0x1d0 + this.wolfDyingNormal[Math.Min(troop.animFrame, this.wolfDyingNormal.Length - 1)];
                goto Label_0605;
            Label_0475:
                spriteNo = 0x98 + this.knightDyingNormal[Math.Min(troop.animFrame, this.knightDyingNormal.Length - 1)];
                num6 = 0xe0 + this.knightDyingNormal[Math.Min(troop.animFrame, this.knightDyingNormal.Length - 1)];
                goto Label_0605;
            Label_04FF:
                spriteNo = 0xc0 + this.pikemanDyingNormal[Math.Min(troop.animFrame, this.pikemanDyingNormal.Length - 1)];
                goto Label_0605;
            Label_0565:
                spriteNo = 0x98 + this.peasantDyingNormal[Math.Min(troop.animFrame, this.peasantDyingNormal.Length - 1)];
                goto Label_0605;
            Label_05C5:
                spriteNo = 0x98 + this.peasantDyingNormal[Math.Min(troop.animFrame, this.peasantDyingNormal.Length - 1)];
            Label_0605:
                num8 = 1;
                if (knightTopAnimTexID >= 0)
                {
                    num8 = 2;
                }
                for (int i = 0; i < num8; i++)
                {
                    if (i == 1)
                    {
                        textureID = knightTopAnimTexID;
                        spriteNo = num6;
                    }
                    SpriteWrapper child = this.getNextExtraSprite(textureID, spriteNo);
                    Point point = (castleUnitSpritePoint[xPos, yPos]);
                    if (troop.moving)
                    {
                        Point point2 = (castleUnitSpritePoint[troop.otherX, troop.otherY]);
                        float num10 = troop.getMoveRatio();
                        point.X = ((int) ((point.X - point2.X) * num10)) + point2.X;
                        point.Y = ((int) ((point.Y - point2.Y) * num10)) + point2.Y;
                    }
                    int num11 = 0;
                    if (displayCollapsed)
                    {
                        num11 = this.castleLayout.collapsedHeightMap[xPos, yPos];
                    }
                    else
                    {
                        num11 = this.castleLayout.fullHeightMap[xPos, yPos];
                    }
                    point.Y -= num11;
                    child.Visible = true;
                    if (troop.elementType == 0x4e)
                    {
                        child.Center = new PointF(75f, 100f);
                    }
                    else
                    {
                        child.Center = new PointF(50f, 66f);
                    }
                    child.PosX = point.X;
                    child.PosY = point.Y;
                    if (troop.animFrame > num7)
                    {
                        int alpha = ((num7 + 0x10) - troop.animFrame) * 0x10;
                        if (alpha < 0x10)
                        {
                            alpha = 0x10;
                        }
                        else if (alpha > 0xff)
                        {
                            alpha = 0xff;
                        }
                        child.ColorToUse = Color.FromArgb(alpha, ARGBColors.White);
                    }
                    if (castleSpriteGrid[xPos, yPos] != null)
                    {
                        if (castleSpriteGrid[xPos, yPos].Visible)
                        {
                            SpriteWrapper wrapper2 = castleSpriteGrid[xPos, yPos];
                            child.PosX -= wrapper2.PosX;
                            child.PosY -= wrapper2.PosY;
                            wrapper2.DrawChildrenWithParent = true;
                            wrapper2.AddChild(child);
                        }
                        else
                        {
                            long num13 = this.castleLayout.elemMap[xPos, yPos];
                            if ((num13 >= 0L) || (num13 == -1L))
                            {
                                CastleElement element = (CastleElement) activeCastleInfrastructureElements[num13];
                                if (element != null)
                                {
                                    SpriteWrapper wrapper3 = castleSpriteGrid[element.xPos, element.yPos];
                                    if (wrapper3.Visible)
                                    {
                                        child.PosX -= wrapper3.PosX;
                                        child.PosY -= wrapper3.PosY;
                                        wrapper3.DrawChildrenWithParent = true;
                                        wrapper3.AddChild(child);
                                    }
                                    else
                                    {
                                        backgroundSprite.AddChild(child, 2);
                                    }
                                }
                                else
                                {
                                    backgroundSprite.AddChild(child, 2);
                                }
                            }
                            else
                            {
                                backgroundSprite.AddChild(child, 2 + i);
                            }
                        }
                    }
                    else
                    {
                        backgroundSprite.AddChild(child, 2 + i);
                    }
                }
            }
        }

        public void drawLasso()
        {
            if ((this.m_lassoLeftHeldDown && (this.m_lassoStartX != this.m_lassoEndX)) && (this.m_lassoStartY != this.m_lassoEndY))
            {
                this.gfx.startThickLine(ARGBColors.Black, 2f);
                this.gfx.setThickLineRadius(1f);
                this.gfx.addThickLinePoint((float) this.m_lassoStartX, (float) this.m_lassoStartY);
                this.gfx.addThickLinePoint((float) this.m_lassoEndX, (float) this.m_lassoStartY);
                this.gfx.addThickLinePoint((float) this.m_lassoEndX, (float) this.m_lassoEndY);
                this.gfx.addThickLinePoint((float) this.m_lassoStartX, (float) this.m_lassoEndY);
                this.gfx.drawThickLines(true);
            }
        }

        private void drawRockChips()
        {
            foreach (RockChip chip in this.rockChips)
            {
                SpriteWrapper child = this.getNextExtraSprite(GFXLibrary.Instance.MissileTexID, 0x90 + chip.image);
                PointF tf = new PointF(28f, 28f + chip.height);
                child.Center = tf;
                child.PosX = chip.xPos;
                child.PosY = chip.yPos;
                child.Scale = 0.4f;
                if (chip.black)
                {
                    child.ColorToUse = Color.FromArgb(0xff, 0x40, 0x40, 0x40);
                }
                else
                {
                    child.ColorToUse = ARGBColors.White;
                }
                backgroundSprite.AddChild(child, 2);
            }
            this.drawSmoke();
        }

        private void drawRocks()
        {
            foreach (RockMissile missile in this.castleCombat.getRocks())
            {
                if (missile.firingDelay <= 0)
                {
                    for (int i = 0; i < missile.trail.Length; i++)
                    {
                        if (missile.trail[i].visible)
                        {
                            SpriteWrapper wrapper = null;
                            if (missile.bombard)
                            {
                                wrapper = this.getNextExtraSprite(GFXLibrary.Instance.MissileTexID, 0x98);
                            }
                            else
                            {
                                wrapper = this.getNextExtraSprite(GFXLibrary.Instance.MissileTexID, 0x90);
                            }
                            PointF tf = new PointF(28f, (float) (0x1c + missile.trail[i].height));
                            wrapper.Center = tf;
                            wrapper.PosX = missile.trail[i].pos.X;
                            wrapper.PosY = missile.trail[i].pos.Y;
                            wrapper.ColorToUse = Color.FromArgb(0xff - ((0xff * i) / missile.trail.Length), ARGBColors.White);
                            backgroundSprite.AddChild(wrapper, 2);
                        }
                    }
                    Point point = (castleUnitSpritePoint[missile.startX, missile.startY]);
                    Point point2 = (castleUnitSpritePoint[missile.targX, missile.targY]);
                    int height = (int) missile.height;
                    double num3 = missile.distTravelled / missile.journeyLength;
                    point2.X = ((int) ((point2.X - point.X) * num3)) + point.X;
                    point2.Y = ((int) ((point2.Y - point.Y) * num3)) + point.Y;
                    SpriteWrapper child = null;
                    if (missile.bombard)
                    {
                        child = this.getNextExtraSprite(GFXLibrary.Instance.MissileTexID, 0x98);
                    }
                    else
                    {
                        child = this.getNextExtraSprite(GFXLibrary.Instance.MissileTexID, 0x90 + ((this.castleCombat.TickValue / 3) % 8));
                    }
                    PointF tf2 = new PointF(28f, (float) (0x1c + height));
                    child.Center = tf2;
                    child.PosX = point2.X;
                    child.PosY = point2.Y;
                    backgroundSprite.AddChild(child, 2);
                    SpriteWrapper wrapper3 = null;
                    if (missile.bombard)
                    {
                        wrapper3 = this.getNextExtraSprite(GFXLibrary.Instance.MissileTexID, 0x98);
                    }
                    else
                    {
                        wrapper3 = this.getNextExtraSprite(GFXLibrary.Instance.MissileTexID, 0x90 + ((this.castleCombat.TickValue / 3) % 8));
                    }
                    PointF tf3 = new PointF(28f, 28f);
                    wrapper3.Center = tf3;
                    wrapper3.PosX = point2.X;
                    wrapper3.PosY = point2.Y;
                    wrapper3.ColorToUse = Color.FromArgb(0x40, ARGBColors.Black);
                    wrapper3.Scale = 0.85f;
                    backgroundSprite.AddChild(wrapper3);
                    if (!this.castleCombat.Paused)
                    {
                        for (int j = missile.trail.Length - 1; j > 0; j--)
                        {
                            missile.trail[j].pos = missile.trail[j - 1].pos;
                            missile.trail[j].height = missile.trail[j - 1].height;
                            missile.trail[j].visible = missile.trail[j - 1].visible;
                        }
                        missile.trail[0].pos = new PointF((float) point2.X, (float) point2.Y);
                        missile.trail[0].height = height;
                        missile.trail[0].visible = true;
                    }
                }
            }
        }

        private void drawSmoke()
        {
            foreach (RockSmoke smoke in this.rockSmoke)
            {
                SpriteWrapper child = this.getNextExtraSprite(GFXLibrary.Instance.Smoke1TexID, smoke.animFrame / 2);
                child.Center = new PointF(50f, 75f);
                child.PosX = smoke.xPos;
                child.PosY = smoke.yPos;
                child.Scale = 1.5f;
                if (smoke.black)
                {
                    child.ColorToUse = Color.FromArgb(0xff, 0x40, 0x40, 0x40);
                    child.ScaleX = 2.3f;
                }
                else
                {
                    child.ColorToUse = ARGBColors.White;
                }
                backgroundSprite.AddChild(child, 2);
            }
        }

        private void drawSurroundSprites()
        {
            foreach (SpriteWrapper wrapper in surroundsprites)
            {
                wrapper.AddToRenderList();
            }
            if (!this.attackerSetupMode && !this.battleMode)
            {
                enclosedOverlaySprite.AddToRenderList();
                if (!this.m_castleEnclosed)
                {
                    enclosedOverlaySprite2.AddToRenderList();
                    this.enclosedGlow += 0x10;
                    if (this.enclosedGlow >= 0x200)
                    {
                        this.enclosedGlow = 0;
                    }
                    int enclosedGlow = this.enclosedGlow;
                    if (enclosedGlow >= 0x100)
                    {
                        enclosedGlow = 0x1ff - enclosedGlow;
                    }
                    enclosedOverlaySprite2.ColorToUse = Color.FromArgb(enclosedGlow, ARGBColors.White);
                }
                if (GameEngine.Instance.World.isTutorialActive())
                {
                    if (!TutorialWindow.overIcon)
                    {
                        tutorialOverlaySprite.TextureID = GFXLibrary.Instance.TutorialIconNormalID;
                    }
                    else
                    {
                        tutorialOverlaySprite.TextureID = GFXLibrary.Instance.TutorialIconOverID;
                    }
                    tutorialOverlaySprite.AddToRenderList();
                }
                if (!this.overWikiHelp)
                {
                    wikiHelpSprite.TextureID = GFXLibrary.Instance.WikiHelpIconNormal;
                }
                else
                {
                    wikiHelpSprite.TextureID = GFXLibrary.Instance.WikiHelpIconOver;
                }
                wikiHelpSprite.Scale = 0.66f;
                wikiHelpSprite.AddToRenderList();
            }
            if (placementSprite_confirm != null)
            {
                placementSprite_confirm.Update();
                placementSprite_confirm.AddToRenderList();
            }
            if (placementSprite_cancel != null)
            {
                placementSprite_cancel.Update();
                placementSprite_cancel.AddToRenderList();
            }
            this.addTCWarningMessage();
        }

        private void drawTroops()
        {
            foreach (CastleElement element in this.elements)
            {
                SpriteWrapper wrapper;
                if (element.elementType < 0x45)
                {
                    continue;
                }
                int xPos = element.xPos;
                int yPos = element.yPos;
                int texID = -1;
                int textureID = -1;
                bool flag = true;
                if (element.elementType >= 90)
                {
                    flag = false;
                }
                switch (element.elementType)
                {
                    case 70:
                        if (element.reinforcement || element.vassalReinforcements)
                        {
                            goto Label_0219;
                        }
                        texID = GFXLibrary.Instance.PeasantAnimTexID;
                        goto Label_0259;

                    case 0x47:
                        if (element.reinforcement || element.vassalReinforcements)
                        {
                            goto Label_0159;
                        }
                        texID = GFXLibrary.Instance.SwordsmanAnimTexID;
                        goto Label_0259;

                    case 0x48:
                        if (element.reinforcement || element.vassalReinforcements)
                        {
                            break;
                        }
                        texID = GFXLibrary.Instance.ArcherAnimTexID;
                        goto Label_0259;

                    case 0x49:
                        if (element.reinforcement || element.vassalReinforcements)
                        {
                            goto Label_01E2;
                        }
                        texID = GFXLibrary.Instance.PikemanAnimTexID;
                        goto Label_0259;

                    case 0x4b:
                        texID = GFXLibrary.Instance.CastleSpritesTexID;
                        this.numPots++;
                        goto Label_0259;

                    case 0x4d:
                        texID = GFXLibrary.Instance.WolfAnimTexID;
                        goto Label_0259;

                    case 0x4e:
                        texID = GFXLibrary.Instance.KnightAnimTexID;
                        textureID = GFXLibrary.Instance.KnightTopAnimTexID;
                        goto Label_0259;

                    case 0x55:
                        texID = GFXLibrary.Instance.CaptainAnimTexID;
                        goto Label_0259;

                    case 90:
                        texID = GFXLibrary.Instance.PeasantRedAnimTexID;
                        goto Label_0259;

                    case 0x5b:
                        texID = GFXLibrary.Instance.SwordsmanRedAnimTexID;
                        goto Label_0259;

                    case 0x5c:
                        texID = GFXLibrary.Instance.ArcherRedAnimTexID;
                        goto Label_0259;

                    case 0x5d:
                        texID = GFXLibrary.Instance.PikemanRedAnimTexID;
                        goto Label_0259;

                    case 0x5e:
                        texID = GFXLibrary.Instance.CatapultAnimTexID;
                        goto Label_0259;

                    case 100:
                    case 0x65:
                    case 0x66:
                    case 0x67:
                    case 0x68:
                    case 0x69:
                    case 0x6a:
                    case 0x6b:
                        texID = GFXLibrary.Instance.CaptainAnimRedTexID;
                        goto Label_0259;

                    default:
                        goto Label_0259;
                }
                texID = GFXLibrary.Instance.ArcherGreenAnimTexID;
                goto Label_0259;
            Label_0159:
                texID = GFXLibrary.Instance.SwordsmanGreenAnimTexID;
                goto Label_0259;
            Label_01E2:
                texID = GFXLibrary.Instance.PikemanGreenAnimTexID;
                goto Label_0259;
            Label_0219:
                texID = GFXLibrary.Instance.PeasantGreenAnimTexID;
            Label_0259:
                if (texID < 0)
                {
                    goto Label_17D6;
                }
                BattleTroop troop = null;
                if (this.battleMode)
                {
                    troop = (BattleTroop) element;
                }
                PointF tf = new PointF(18f, 28f);
                int spriteNo = 0;
                int num6 = 0;
                if (element.elementType == 0x4b)
                {
                    spriteNo = 0x18c;
                    if (this.battleMode && troop.pouring)
                    {
                        texID = GFXLibrary.Instance.OilPotAnimTexID;
                        spriteNo = (troop.facing + 6) & 7;
                        spriteNo += troop.animFrame * 8;
                        tf = new PointF(48f, 54f);
                    }
                }
                else if (!this.battleMode)
                {
                    if (xPos < yPos)
                    {
                        if ((0x75 - xPos) < yPos)
                        {
                            if (flag)
                            {
                                spriteNo = 2;
                            }
                            else
                            {
                                spriteNo = 6;
                            }
                        }
                        else if (flag)
                        {
                            spriteNo = 4;
                        }
                        else
                        {
                            spriteNo = 0;
                        }
                    }
                    else if ((0x75 - xPos) < yPos)
                    {
                        if (flag)
                        {
                            spriteNo = 0;
                        }
                        else
                        {
                            spriteNo = 4;
                        }
                    }
                    else if (flag)
                    {
                        spriteNo = 6;
                    }
                    else
                    {
                        spriteNo = 2;
                    }
                }
                else
                {
                    spriteNo = (troop.facing + 6) & 7;
                    num6 = spriteNo;
                    if (troop.moving)
                    {
                        num6 = spriteNo += ((troop.animFrame + troop.walkAnimOffset) % 0x10) * 8;
                        if (troop.pillageCarry)
                        {
                            switch (troop.elementType)
                            {
                                case 90:
                                    texID = GFXLibrary.Instance.PeasantCarryAnimTexID;
                                    break;

                                case 0x5b:
                                    texID = GFXLibrary.Instance.SwordsmanCarryAnimTexID;
                                    break;

                                case 0x5c:
                                    texID = GFXLibrary.Instance.ArcherCarryAnimTexID;
                                    break;

                                case 0x5d:
                                    texID = GFXLibrary.Instance.PikemanCarryAnimTexID;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (!troop.attackingEnemy)
                        {
                            goto Label_074D;
                        }
                        if (troop.blockedClock <= 0)
                        {
                            goto Label_054F;
                        }
                        switch (troop.elementType)
                        {
                            case 70:
                                spriteNo += 0xd0 + (this.peasantBlocked[Math.Min(troop.animFrame, this.peasantBlocked.Length - 1)] * 8);
                                texID = GFXLibrary.Instance.Peasant2AnimTexID;
                                break;

                            case 0x47:
                            case 0x5b:
                                goto Label_0526;

                            case 0x48:
                            case 0x5c:
                                spriteNo += 0x268 + (this.archerBlocked[troop.animFrame % this.archerBlocked.Length] * 8);
                                break;

                            case 0x49:
                            case 0x5d:
                                goto Label_04FD;

                            case 90:
                                spriteNo += 0xd0 + (this.peasantBlocked[Math.Min(troop.animFrame, this.peasantBlocked.Length - 1)] * 8);
                                texID = GFXLibrary.Instance.Peasant2RedAnimTexID;
                                break;
                        }
                    }
                }
                goto Label_0F2B;
            Label_04FD:
                spriteNo += 0x1c0 + (this.pikemanBlocked[troop.animFrame % this.pikemanBlocked.Length] * 8);
                goto Label_0F2B;
            Label_0526:
                spriteNo += 0x1a8 + (this.swordsmanBlocked[troop.animFrame % this.swordsmanBlocked.Length] * 8);
                goto Label_0F2B;
            Label_054F:
                switch (troop.elementType)
                {
                    case 70:
                        spriteNo += this.peasantAttack[troop.animFrame % this.peasantAttack.Length] * 8;
                        texID = GFXLibrary.Instance.Peasant2AnimTexID;
                        goto Label_0F2B;

                    case 0x47:
                    case 0x5b:
                        spriteNo += 0x80 + (this.swordsmanAttackUnit[troop.animFrame % this.swordsmanAttackUnit.Length] * 8);
                        goto Label_0F2B;

                    case 0x48:
                    case 0x5c:
                        spriteNo += 520 + (this.archerAttackUnit[troop.animFrame % this.archerAttackUnit.Length] * 8);
                        goto Label_0F2B;

                    case 0x49:
                    case 0x5d:
                        spriteNo += 0x108 + (this.pikemanAttackJab[troop.animFrame % this.pikemanAttackJab.Length] * 8);
                        goto Label_0F2B;

                    case 0x4d:
                        spriteNo += 0x80 + (this.wolfAttackUnit[troop.animFrame % this.wolfAttackUnit.Length] * 8);
                        goto Label_0F2B;

                    case 0x4e:
                        spriteNo += 0x80;
                        num6 += 0x80 + (this.knightAttackUnit[troop.animFrame % this.knightAttackUnit.Length] * 8);
                        goto Label_0F2B;

                    case 0x55:
                    case 100:
                    case 0x65:
                    case 0x66:
                    case 0x67:
                    case 0x68:
                    case 0x69:
                    case 0x6a:
                    case 0x6b:
                        spriteNo += 0x11f + (this.captainAttackUnit[troop.animFrame % this.captainAttackUnit.Length] * 8);
                        goto Label_0F2B;

                    case 90:
                        spriteNo += this.peasantAttack[troop.animFrame % this.peasantAttack.Length] * 8;
                        texID = GFXLibrary.Instance.Peasant2RedAnimTexID;
                        goto Label_0F2B;

                    default:
                        goto Label_0F2B;
                }
            Label_074D:
                if (troop.attackingMoat)
                {
                    switch (troop.elementType)
                    {
                        case 70:
                            spriteNo += 0x120 + (this.peasantAttackMoat[troop.animFrame % this.peasantAttackMoat.Length] * 8);
                            texID = GFXLibrary.Instance.Peasant2AnimTexID;
                            break;

                        case 0x47:
                        case 0x5b:
                            spriteNo += 0x1c8 + (this.swordsmanAttackMoat[troop.animFrame % this.swordsmanAttackMoat.Length] * 8);
                            break;

                        case 0x48:
                            spriteNo += this.archerAttackMoat[troop.animFrame % this.archerAttackMoat.Length] * 8;
                            texID = GFXLibrary.Instance.Archer2AnimTexID;
                            break;

                        case 0x49:
                        case 0x5d:
                            spriteNo += 0x148 + (this.pikemanAttackMoat[troop.animFrame % this.pikemanAttackMoat.Length] * 8);
                            break;

                        case 0x55:
                        case 100:
                        case 0x65:
                        case 0x66:
                        case 0x67:
                        case 0x68:
                        case 0x69:
                        case 0x6a:
                        case 0x6b:
                            spriteNo += 0x11f + (this.captainAttackMoat[troop.animFrame % this.captainAttackMoat.Length] * 8);
                            break;

                        case 90:
                            spriteNo += 0x120 + (this.peasantAttackMoat[troop.animFrame % this.peasantAttackMoat.Length] * 8);
                            texID = GFXLibrary.Instance.Peasant2RedAnimTexID;
                            break;

                        case 0x5c:
                            spriteNo += this.archerAttackMoat[troop.animFrame % this.archerAttackMoat.Length] * 8;
                            texID = GFXLibrary.Instance.Archer2RedAnimTexID;
                            break;
                    }
                }
                else if (troop.attackingIntrastructure)
                {
                    switch (troop.elementType)
                    {
                        case 70:
                            spriteNo += this.peasantAttack[troop.animFrame % this.peasantAttack.Length] * 8;
                            texID = GFXLibrary.Instance.Peasant2AnimTexID;
                            break;

                        case 0x47:
                        case 0x5b:
                            spriteNo += 0x80 + (this.swordsmanAttackWall[troop.animFrame % this.swordsmanAttackWall.Length] * 8);
                            break;

                        case 0x48:
                        case 0x5c:
                            spriteNo += 520 + (this.archerAttackWall[troop.animFrame % this.archerAttackWall.Length] * 8);
                            break;

                        case 0x49:
                        case 0x5d:
                            spriteNo += 0x108 + (this.pikemanAttackChop[troop.animFrame % this.pikemanAttackChop.Length] * 8);
                            break;

                        case 0x55:
                        case 100:
                        case 0x65:
                        case 0x66:
                        case 0x67:
                        case 0x68:
                        case 0x69:
                        case 0x6a:
                        case 0x6b:
                            spriteNo += 0x11f + (this.captainAttackWall[troop.animFrame % this.captainAttackWall.Length] * 8);
                            break;

                        case 90:
                            spriteNo += this.peasantAttack[troop.animFrame % this.peasantAttack.Length] * 8;
                            texID = GFXLibrary.Instance.Peasant2RedAnimTexID;
                            break;
                    }
                }
                else if (troop.firingRock)
                {
                    if (troop.animFrame < this.catapultAnim.Length)
                    {
                        spriteNo += this.catapultAnim[troop.animFrame % this.catapultAnim.Length] * 8;
                    }
                }
                else if (troop.shootingArrow)
                {
                    if (displayCollapsed)
                    {
                        if (troop.animFrame < this.archerAttackFiringStraight.Length)
                        {
                            spriteNo += 0x88 + (this.archerAttackFiringStraight[troop.animFrame % this.archerAttackFiringStraight.Length] * 8);
                        }
                        else
                        {
                            spriteNo += 640;
                        }
                    }
                    else
                    {
                        if (troop.arrowTilt < 0)
                        {
                            int startHeight = this.castleLayout.fullHeightMap[troop.arrow.startX, troop.arrow.startY];
                            int targetHeight = this.castleLayout.fullHeightMap[troop.arrow.targetX, troop.arrow.targetY];
                            troop.arrowTilt = this.calcTilt(troop.arrow, startHeight, targetHeight);
                        }
                        if (troop.arrowTilt < 3)
                        {
                            if (troop.animFrame < this.archerAttackFiringDown.Length)
                            {
                                spriteNo += 0x88 + (this.archerAttackFiringDown[troop.animFrame % this.archerAttackFiringDown.Length] * 8);
                            }
                            else
                            {
                                spriteNo += 640;
                            }
                        }
                        else if (troop.arrowTilt < 6)
                        {
                            if (troop.animFrame < this.archerAttackFiringStraight.Length)
                            {
                                spriteNo += 0x88 + (this.archerAttackFiringStraight[troop.animFrame % this.archerAttackFiringStraight.Length] * 8);
                            }
                            else
                            {
                                spriteNo += 640;
                            }
                        }
                        else if (troop.animFrame < this.archerAttackFiringUp.Length)
                        {
                            spriteNo += 0x88 + (this.archerAttackFiringUp[troop.animFrame % this.archerAttackFiringUp.Length] * 8);
                        }
                        else
                        {
                            spriteNo += 640;
                        }
                    }
                }
                else if (troop.pillageCarry)
                {
                    switch (troop.elementType)
                    {
                        case 90:
                            texID = GFXLibrary.Instance.PeasantCarryAnimTexID;
                            break;

                        case 0x5b:
                            texID = GFXLibrary.Instance.SwordsmanCarryAnimTexID;
                            break;

                        case 0x5c:
                            texID = GFXLibrary.Instance.ArcherCarryAnimTexID;
                            break;

                        case 0x5d:
                            texID = GFXLibrary.Instance.PikemanCarryAnimTexID;
                            break;
                    }
                }
                else
                {
                    switch (troop.elementType)
                    {
                        case 70:
                            spriteNo += 0x80 + (this.peasantIdle[troop.animFrame % this.peasantIdle.Length] * 8);
                            texID = GFXLibrary.Instance.Peasant2AnimTexID;
                            break;

                        case 0x47:
                        case 0x5b:
                            spriteNo += 0x1c0;
                            break;

                        case 0x48:
                        case 0x5c:
                            spriteNo += 640;
                            break;

                        case 0x49:
                            if (troop.blockClock < GameEngine.Instance.LocalWorldData.Castle_Pikeman_BlockRechargeTime)
                            {
                                goto Label_0E9B;
                            }
                            spriteNo += 0x108;
                            break;

                        case 0x4d:
                            goto Label_0ECD;

                        case 0x4e:
                            goto Label_0ED9;

                        case 0x55:
                        case 100:
                        case 0x65:
                        case 0x66:
                        case 0x67:
                        case 0x68:
                        case 0x69:
                        case 0x6a:
                        case 0x6b:
                            spriteNo += 0x80 + ((this.captainIdle[(troop.animFrame / 2) % this.captainIdle.Length] - 1) * 8);
                            break;

                        case 90:
                            spriteNo += 0x80 + (this.peasantIdle[troop.animFrame % this.peasantIdle.Length] * 8);
                            texID = GFXLibrary.Instance.Peasant2RedAnimTexID;
                            break;

                        case 0x5d:
                            goto Label_0E4B;
                    }
                }
                goto Label_0F2B;
            Label_0E4B:
                spriteNo += 0x80 + (this.pikemanIdle[troop.animFrame % this.pikemanIdle.Length] * 8);
                goto Label_0F2B;
            Label_0E9B:
                spriteNo += 0x80 + (this.pikemanIdle[troop.animFrame % this.pikemanIdle.Length] * 8);
                goto Label_0F2B;
            Label_0ECD:
                spriteNo += 0x1a8;
                goto Label_0F2B;
            Label_0ED9:
                spriteNo += 0x80 + (this.knightHorseIdle[troop.animFrame % this.knightHorseIdle.Length] * 8);
            Label_0F2B:
                wrapper = castleDefenderSpriteGrid[xPos, yPos];
                if (wrapper == null)
                {
                    wrapper = castleAttackerSpriteGrid[xPos, yPos];
                }
                int num9 = 1;
                if (textureID >= 0)
                {
                    num9 = 2;
                }
                SpriteWrapper wrapper2 = null;
                for (int i = 0; i < num9; i++)
                {
                    if ((textureID >= 0) && (i == 1))
                    {
                        wrapper = this.getNextExtraSprite(textureID, num6);
                        texID = textureID;
                        spriteNo = num6;
                    }
                    else
                    {
                        wrapper.Initialize(this.gfx, texID, spriteNo);
                    }
                    Point pos = (castleUnitSpritePoint[xPos, yPos]);
                    if (this.battleMode && troop.moving)
                    {
                        Point point2 = (castleUnitSpritePoint[troop.otherX, troop.otherY]);
                        float num11 = troop.getMoveRatio();
                        pos.X = ((int) ((pos.X - point2.X) * num11)) + point2.X;
                        pos.Y = ((int) ((pos.Y - point2.Y) * num11)) + point2.Y;
                    }
                    int num12 = 0;
                    if (displayCollapsed)
                    {
                        num12 = this.castleLayout.collapsedHeightMap[xPos, yPos];
                    }
                    else
                    {
                        num12 = this.castleLayout.fullHeightMap[xPos, yPos];
                    }
                    pos.Y -= num12;
                    wrapper.PosX = pos.X;
                    wrapper.PosY = pos.Y;
                    wrapper.Visible = true;
                    if (element.elementType == 0x4b)
                    {
                        wrapper.Center = tf;
                    }
                    else if (element.elementType == 0x5e)
                    {
                        wrapper.Center = new PointF(93f, 100f);
                    }
                    else if (element.elementType == 0x4e)
                    {
                        wrapper.Center = new PointF(75f, 100f);
                    }
                    else if (((element.elementType >= 0x55) && (element.elementType <= 0x59)) || ((element.elementType >= 100) && (element.elementType <= 0x6d)))
                    {
                        wrapper.Center = new PointF(65f, 82f);
                    }
                    else
                    {
                        wrapper.Center = new PointF(50f, 66f);
                    }
                    this.getNextClickArea().addUnit(pos, element.elementID);
                    if ((element.elementID == this.troopMovingElemID) && this.troopMovingMode)
                    {
                        wrapper.ColorToUse = Color.FromArgb(0x80, ARGBColors.White);
                    }
                    else if ((element.elementID == this.selectedCatapult) && (this.selectedCatapult != -1L))
                    {
                        wrapper.ColorToUse = Color.FromArgb(0xc0, ARGBColors.Red);
                    }
                    else if ((element.elementID == this.troopSelected) && (this.troopSelected != -1L))
                    {
                        wrapper.ColorToUse = Color.FromArgb(0xc0, ARGBColors.Red);
                    }
                    else if (this.m_lassoLeftHeldDown)
                    {
                        if (!this.m_lassoElements.Contains(element.elementID))
                        {
                            wrapper.ColorToUse = Color.FromArgb(160, ARGBColors.White);
                        }
                        else
                        {
                            wrapper.ColorToUse = Color.FromArgb(0xff, this.pulseValue, this.pulseValue, this.pulseValue);
                        }
                    }
                    else if (this.m_lassoMade)
                    {
                        if (!this.m_lassoElements.Contains(element.elementID))
                        {
                            wrapper.ColorToUse = Color.FromArgb(0x80, ARGBColors.White);
                        }
                        else
                        {
                            wrapper.ColorToUse = Color.FromArgb(0xff, this.pulseValue, this.pulseValue, this.pulseValue);
                        }
                    }
                    else
                    {
                        wrapper.ColorToUse = ARGBColors.White;
                    }
                    if (!this.battleMode && (element.elementID == this.deletingHighlightElementID))
                    {
                        wrapper.ColorToUse = Color.FromArgb(0x7f, 0x20, 0x20, 0x20);
                    }
                    if (castleSpriteGrid[xPos, yPos] != null)
                    {
                        if (castleSpriteGrid[xPos, yPos].Visible)
                        {
                            SpriteWrapper wrapper3 = castleSpriteGrid[xPos, yPos];
                            wrapper.PosX -= wrapper3.PosX;
                            wrapper.PosY -= wrapper3.PosY;
                            wrapper3.DrawChildrenWithParent = true;
                            wrapper3.AddChild(wrapper);
                        }
                        else
                        {
                            long num13 = this.castleLayout.elemMap[xPos, yPos];
                            if ((num13 != -2L) || (num13 == -1L))
                            {
                                CastleElement element2 = (CastleElement) activeCastleInfrastructureElements[num13];
                                if (element2 != null)
                                {
                                    SpriteWrapper wrapper4 = castleSpriteGrid[element2.xPos, element2.yPos];
                                    if (wrapper4.Visible)
                                    {
                                        wrapper.PosX -= wrapper4.PosX;
                                        wrapper.PosY -= wrapper4.PosY;
                                        wrapper4.DrawChildrenWithParent = true;
                                        wrapper4.AddChild(wrapper);
                                    }
                                    else
                                    {
                                        backgroundSprite.AddChild(wrapper, 2);
                                    }
                                }
                                else
                                {
                                    backgroundSprite.AddChild(wrapper, 2);
                                }
                            }
                            else if (wrapper2 != null)
                            {
                                wrapper.PosX -= wrapper2.PosX;
                                wrapper.PosY -= wrapper2.PosY;
                                wrapper2.DrawChildrenWithParent = true;
                                wrapper2.AddChild(wrapper, 2);
                            }
                            else
                            {
                                backgroundSprite.AddChild(wrapper, 2 + i);
                            }
                        }
                    }
                    else if (wrapper2 != null)
                    {
                        wrapper.PosX -= wrapper2.PosX;
                        wrapper.PosY -= wrapper2.PosY;
                        wrapper2.DrawChildrenWithParent = true;
                        wrapper2.AddChild(wrapper, 2);
                    }
                    else
                    {
                        backgroundSprite.AddChild(wrapper, 2 + i);
                    }
                    if ((this.battleMode && (this.battleModeMousePos.X != 0x3e8)) && (troop.elementType != 0x4b))
                    {
                        int num14 = ((this.battleModeMousePos.X - pos.X) * (this.battleModeMousePos.X - pos.X)) + ((this.battleModeMousePos.Y - (pos.Y - 15)) * (this.battleModeMousePos.Y - (pos.Y - 15)));
                        if (num14 < 0x57e4)
                        {
                            int damage = (int) troop.damage;
                            int num16 = this.castleCombat.getUnitMaxDamage(troop.elementType);
                            int num17 = this.castleCombat.getUnitMaxDamageNumLevels(troop.elementType);
                            int num18 = (damage * num17) / num16;
                            if (num18 >= num17)
                            {
                                num18 = num17 - 1;
                            }
                            int num19 = 0;
                            switch (num17)
                            {
                                case 12:
                                    num19 = 11;
                                    break;

                                case 13:
                                    num19 = 0x17;
                                    break;

                                case 14:
                                    num19 = 0x24;
                                    break;

                                case 15:
                                    num19 = 50;
                                    break;

                                case 0x10:
                                    num19 = 0x41;
                                    break;

                                case 0x11:
                                    num19 = 0x51;
                                    break;

                                case 0x12:
                                    num19 = 0x62;
                                    break;

                                case 0x13:
                                    num19 = 0x74;
                                    break;

                                case 20:
                                    num19 = 0x87;
                                    break;

                                case 0x15:
                                    num19 = 0x9b;
                                    break;

                                default:
                                    num19 = 0;
                                    break;
                            }
                            SpriteWrapper child = this.getNextExtraSprite(GFXLibrary.Instance.HpsBarsTexID, (num19 + (num17 - 1)) - num18);
                            child.Center = new PointF(11f, 2f);
                            child.PosX = 0f;
                            child.PosY = -40f;
                            wrapper.DrawChildrenWithParent = true;
                            wrapper.AddChild(child);
                        }
                    }
                    if (this.battleMode && (troop.captainsBonusDamageClock > 0))
                    {
                        int num20 = 900 - troop.captainsBonusDamageClock;
                        if (num20 >= 0x16)
                        {
                            if (num20 > 0x36e)
                            {
                                num20 = 900 - num20;
                            }
                            else
                            {
                                num20 = (num20 - 0x16) % 0x2c;
                                if (num20 >= 0x17)
                                {
                                    num20 = 0x2d - num20;
                                }
                                num20 += 0x16;
                            }
                        }
                        if (num20 < 0)
                        {
                            num20 = 0;
                        }
                        else if (num20 >= 0x2d)
                        {
                            num20 = 0x2c;
                        }
                        num20 += 0x2d;
                        SpriteWrapper wrapper6 = this.getNextExtraSprite(GFXLibrary.Instance.ArmyAnimsTexID, num20);
                        wrapper6.Center = new PointF(30f, 32f);
                        wrapper6.PosX = 0f;
                        wrapper6.PosY = -40f;
                        wrapper.DrawChildrenWithParent = true;
                        wrapper.AddChild(wrapper6);
                    }
                    if (this.battleMode && (troop.captainsHealAnimClock > 0))
                    {
                        int num21 = (450 - troop.captainsHealAnimClock) % 0x2d;
                        if (num21 < 0)
                        {
                            num21 = 0;
                        }
                        else if (num21 >= 0x2d)
                        {
                            num21 = 0x2c;
                        }
                        SpriteWrapper wrapper7 = this.getNextExtraSprite(GFXLibrary.Instance.ArmyAnimsTexID, num21);
                        wrapper7.Center = new PointF(35f, 32f);
                        wrapper7.PosX = 0f;
                        wrapper7.PosY = -50f;
                        wrapper.DrawChildrenWithParent = true;
                        wrapper.AddChild(wrapper7);
                    }
                    wrapper2 = wrapper;
                }
            Label_17D6:
                switch (element.elementType)
                {
                    case 70:
                    {
                        if (element.reinforcement || element.vassalReinforcements)
                        {
                            goto Label_18E7;
                        }
                        this.numPlacedDefenderPeasants++;
                        continue;
                    }
                    case 0x47:
                    {
                        if (element.reinforcement || element.vassalReinforcements)
                        {
                            goto Label_1938;
                        }
                        this.numPlacedDefenderSwordsmen++;
                        continue;
                    }
                    case 0x48:
                    {
                        if (element.reinforcement || element.vassalReinforcements)
                        {
                            break;
                        }
                        this.numPlacedDefenderArchers++;
                        continue;
                    }
                    case 0x49:
                    {
                        if (element.reinforcement || element.vassalReinforcements)
                        {
                            goto Label_199C;
                        }
                        this.numPlacedDefenderPikemen++;
                        continue;
                    }
                    case 0x4a:
                    case 0x4b:
                    case 0x4c:
                    case 0x56:
                    case 0x57:
                    case 0x58:
                    case 0x59:
                    case 0x5f:
                    case 0x60:
                    case 0x61:
                    case 0x62:
                    case 0x63:
                    {
                        continue;
                    }
                    case 0x4d:
                    {
                        this.numPlacedReinforceDefenderSwordsmen++;
                        continue;
                    }
                    case 0x55:
                    {
                        this.numPlacedDefenderCaptains++;
                        continue;
                    }
                    case 90:
                    {
                        this.attackNumPeasants++;
                        continue;
                    }
                    case 0x5b:
                    {
                        this.attackNumSwordsmen++;
                        continue;
                    }
                    case 0x5c:
                    {
                        this.attackNumArchers++;
                        continue;
                    }
                    case 0x5d:
                    {
                        this.attackNumPikemen++;
                        continue;
                    }
                    case 0x5e:
                    {
                        this.attackNumCatapults++;
                        continue;
                    }
                    case 100:
                    case 0x65:
                    case 0x66:
                    case 0x67:
                    case 0x68:
                    case 0x69:
                    case 0x6a:
                    case 0x6b:
                    {
                        this.attackNumCaptains++;
                        continue;
                    }
                    default:
                    {
                        continue;
                    }
                }
                if (element.reinforcement)
                {
                    this.numPlacedReinforceDefenderArchers++;
                }
                else
                {
                    this.numPlacedVassalReinforceDefenderArchers++;
                }
                continue;
            Label_18E7:
                if (element.reinforcement)
                {
                    this.numPlacedReinforceDefenderPeasants++;
                }
                else
                {
                    this.numPlacedVassalReinforceDefenderPeasants++;
                }
                continue;
            Label_1938:
                if (element.reinforcement)
                {
                    this.numPlacedReinforceDefenderSwordsmen++;
                }
                else
                {
                    this.numPlacedVassalReinforceDefenderSwordsmen++;
                }
                continue;
            Label_199C:
                if (element.reinforcement)
                {
                    this.numPlacedReinforceDefenderPikemen++;
                }
                else
                {
                    this.numPlacedVassalReinforceDefenderPikemen++;
                }
            }
        }

        public byte[] generateCastleMapSnapshot()
        {
            return CastlesCommon.compressCastleData(this.castleLayout.createCastleMapArray(getCurrentServerTime()));
        }

        public byte[] generateCastleTroopsSnapshot()
        {
            return CastlesCommon.compressCastleData(this.castleLayout.createDefenderMapArray());
        }

        public List<RestoreCastleElement> getAttackSetup(string name)
        {
            List<RestoreCastleElement> list = new List<RestoreCastleElement>();
            try
            {
                FileStream input = new FileStream(this.getAttackSetupSaveName(name), FileMode.Open);
                BinaryReader reader = new BinaryReader(input);
                int num = reader.ReadInt32();
                for (int i = 0; i < num; i++)
                {
                    RestoreCastleElement item = new RestoreCastleElement {
                        xPos = reader.ReadByte(),
                        yPos = reader.ReadByte(),
                        elementType = reader.ReadByte()
                    };
                    if (item.elementType == 0x5e)
                    {
                        item.targXPos = reader.ReadByte();
                        item.targYPos = reader.ReadByte();
                    }
                    if ((item.elementType >= 100) && (item.elementType < 0x6d))
                    {
                        item.delay = reader.ReadByte();
                        if ((item.elementType == 0x66) || (item.elementType == 0x67))
                        {
                            item.targXPos = reader.ReadByte();
                            item.targYPos = reader.ReadByte();
                        }
                    }
                    list.Add(item);
                }
                reader.Close();
                input.Close();
            }
            catch (Exception)
            {
                list.Clear();
            }
            return list;
        }

        private string getAttackSetupSaveName(int ID)
        {
            string str = GameEngine.getSettingsPath(true);
            string str2 = "AttackSetup_" + ID.ToString() + ".cas";
            return (str + @"\" + str2);
        }

        private string getAttackSetupSaveName(string name)
        {
            string str = GameEngine.getSettingsPath(true);
            string str2 = "AttackSetup_" + name + ".cas";
            return (str + @"\" + str2);
        }

        private int getCaptainsDelayValue(int x, int y)
        {
            foreach (CastleElement element in this.elements)
            {
                if ((element.xPos == x) && (element.yPos == y))
                {
                    long elementID = element.elementID;
                    foreach (CaptainsDetails details in this.captainsDetails)
                    {
                        if (details.elemID == elementID)
                        {
                            return details.seconds;
                        }
                    }
                    break;
                }
            }
            return 5;
        }

        public int getCaptainsDetails(long elemID)
        {
            foreach (CaptainsDetails details in this.captainsDetails)
            {
                if (details.elemID == elemID)
                {
                    return details.seconds;
                }
            }
            return 0;
        }

        private Point getCatapultAttackLocation(int x, int y)
        {
            Point point = new Point(-1, -1);
            foreach (CastleElement element in this.elements)
            {
                if ((element.xPos == x) && (element.yPos == y))
                {
                    long elementID = element.elementID;
                    foreach (CatapultTarget target in this.catapultTargets)
                    {
                        if (target.elemID == elementID)
                        {
                            point.X = target.xPos;
                            point.Y = target.yPos;
                            return point;
                        }
                    }
                    return point;
                }
            }
            return point;
        }

        public CampCastleElement[] getCurrentAttackSetup()
        {
            return this.castleLayout.createCastleCampArray_MemoriseAttackSetupTroops();
        }

        public static DateTime getCurrentServerTime()
        {
            double num = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
            return baseServerTime.AddSeconds(num);
        }

        private int getDefenderDefenceResearch()
        {
            if (this.battleMode && (this.m_defenderResearch != null))
            {
                return this.m_defenderResearch.defences;
            }
            if (this.placingAttackerRealMode)
            {
                return 0;
            }
            return GameEngine.Instance.World.GetResearchDataForCurrentVillage().Research_Defences;
        }

        public int getDisplayMode()
        {
            return this.displayType;
        }

        private string getInfrastructureSaveName()
        {
            string str = GameEngine.getSettingsPath(true);
            string str2 = "CasInfra_" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + this.m_villageID.ToString() + ".cas";
            return (str + @"\" + str2);
        }

        private int getLandType()
        {
            if (this.battleMode)
            {
                return this.battleLandType;
            }
            return 0;
        }

        public bool getMapTileFromMousePos(Point mousePos, ref int mapX, ref int mapY)
        {
#if DEBUG
            DataExport.getMapTileFromMousePos(mousePos, mapX, mapY);
#endif
            mapX = -1;
            mapY = -1;
            Point point = mousePos;
            point.X += -((int) backgroundSprite.DrawPos.X) - 5;
            point.Y += ((-((int) backgroundSprite.DrawPos.Y) - 2) - 4) - 10;
            point.Y *= 2;
            PointF tf = new PointF((float) point.X, (float) point.Y);
            PointF tf2 = this.gfx.rotatePoint(tf, 0x13b);
            tf2.X /= 22.62742f;
            tf2.Y /= 22.62742f;
            tf2.X += 58f;
            if (((tf2.X >= 0f) && (tf2.X < 118f)) && ((tf2.Y >= 0f) && (tf2.Y < 118f)))
            {
                mapX = (int) tf2.X;
                mapY = (int) tf2.Y;
                return true;
            }
            return false;
        }

        public string GetNewBuildTime()
        {
            bool isCapital = GameEngine.Instance.World.isCapital(this.m_villageID);
            CardData cardData = new CardData();
            if (!isCapital)
            {
                cardData = GameEngine.Instance.World.UserCardData;
            }
            double num = 0.0;
            foreach (CastleElement element in this.elements)
            {
                if (element.elementID < -1L)
                {
                    num += CastlesCommon.calcConstrTime(GameEngine.Instance.LocalWorldData, element.elementType, GameEngine.Instance.World.GetResearchDataForCurrentVillage().Research_Construction, isCapital, cardData);
                }
            }
            return VillageMap.createBuildTimeString((int) (num * 3600.0));
        }

        private TroopClickArea getNextClickArea()
        {
            TroopClickArea item = null;
            if (numClickAreas < troopClickAreas.Count)
            {
                item = troopClickAreas[numClickAreas];
                numClickAreas++;
                return item;
            }
            item = new TroopClickArea();
            troopClickAreas.Add(item);
            numClickAreas = troopClickAreas.Count;
            return item;
        }

        public SpriteWrapper getNextExtraSprite()
        {
            SpriteWrapper item = null;
            if (this.nextExtraSpriteID >= castleExtraSprites.Count)
            {
                item = new SpriteWrapper();
                castleExtraSprites.Add(item);
            }
            else
            {
                item = castleExtraSprites[this.nextExtraSpriteID];
            }
            this.nextExtraSpriteID++;
            item.Initialize(this.gfx, GFXLibrary.Instance.CastleSpritesTexID, 0);
            item.Scale = 1f;
            item.Visible = true;
            item.ColorToUse = ARGBColors.White;
            return item;
        }

        public SpriteWrapper getNextExtraSprite(int spriteID)
        {
            SpriteWrapper item = null;
            if (this.nextExtraSpriteID >= castleExtraSprites.Count)
            {
                item = new SpriteWrapper();
                castleExtraSprites.Add(item);
            }
            else
            {
                item = castleExtraSprites[this.nextExtraSpriteID];
            }
            this.nextExtraSpriteID++;
            item.Initialize(this.gfx, GFXLibrary.Instance.CastleSpritesTexID, spriteID);
            item.Scale = 1f;
            item.Visible = true;
            item.ColorToUse = ARGBColors.White;
            return item;
        }

        public SpriteWrapper getNextExtraSprite(int textureID, int spriteNo)
        {
            SpriteWrapper item = null;
            if (this.nextExtraSpriteID >= castleExtraSprites.Count)
            {
                item = new SpriteWrapper();
                castleExtraSprites.Add(item);
            }
            else
            {
                item = castleExtraSprites[this.nextExtraSpriteID];
            }
            this.nextExtraSpriteID++;
            item.Initialize(this.gfx, textureID, spriteNo);
            item.Scale = 1f;
            item.Visible = true;
            item.ColorToUse = ARGBColors.White;
            return item;
        }

        public SpriteWrapper getNextWallCacheSprite()
        {
            SpriteWrapper item = null;
            if (this.nextWallCacheSpriteID >= wallCachedSprites.Count)
            {
                item = new SpriteWrapper();
                wallCachedSprites.Add(item);
            }
            else
            {
                item = wallCachedSprites[this.nextWallCacheSpriteID];
            }
            this.nextWallCacheSpriteID++;
            item.Initialize(this.gfx, GFXLibrary.Instance.CastleSpritesTexID, 0);
            item.Scale = 1f;
            item.Visible = true;
            item.ColorToUse = ARGBColors.White;
            return item;
        }

        public Point getScreenPosFromMapTile(Point mapTile)
        {
            return new Point { X = ((mapTile.X * 0x10) + (mapTile.Y * 0x10)) - 0x39a, Y = ((mapTile.Y * 8) - (mapTile.X * 8)) + 0x1da };
        }

        private string getTroopsSaveName()
        {
            string str = GameEngine.getSettingsPath(true);
            string str2 = "CasTroop_" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + this.m_villageID.ToString() + ".cas";
            return (str + @"\" + str2);
        }

        public bool gotAttackSetupSave(int ID)
        {
            return File.Exists(this.getAttackSetupSaveName(ID));
        }

        public bool gotInfrastructureSave()
        {
            return File.Exists(this.getInfrastructureSaveName());
        }

        public bool gotTroopsSave()
        {
            return File.Exists(this.getTroopsSaveName());
        }

        public bool holdingLeftMouse()
        {
            if (!this.m_leftMouseHeldDown)
            {
                return this.m_lassoLeftHeldDown;
            }
            return true;
        }

        public void importDefenderSnapshot(byte[] compressedCastleMap, byte[] compressedDefenderMap, int keepLevel, bool ignorePits, int landType)
        {
            this.attackerSetupMode = true;
            this.captainsDetails.Clear();
            this.displayType = 1;
            this.showCatapultTargets = false;
            if (this.elements == null)
            {
                this.elements = new List<CastleElement>();
            }
            else
            {
                this.elements.Clear();
            }
            CastleElement item = new CastleElement {
                completionTime = DateTime.Now.AddDays(-100.0)
            };
            if (keepLevel < 1)
            {
                keepLevel = 1;
            }
            item.elementType = (byte) keepLevel;
            item.elementID = -1L;
            item.xPos = 0x3a;
            item.yPos = 0x3b;
            this.elements.Add(item);
            long num = -100000L;
            if (compressedDefenderMap != null)
            {
                byte[] buffer = CastlesCommon.decompressCastleData(compressedDefenderMap);
                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] <= 0)
                    {
                        continue;
                    }
                    CastleElement element2 = new CastleElement {
                        completionTime = item.completionTime
                    };
                    num -= 1L;
                    element2.elementID = num;
                    element2.elementType = buffer[i];
                    if (element2.elementType >= 80)
                    {
                        switch (element2.elementType)
                        {
                            case 80:
                                element2.elementType = 70;
                                element2.aggressiveDefender = true;
                                break;

                            case 0x51:
                                element2.elementType = 0x47;
                                element2.aggressiveDefender = true;
                                break;

                            case 0x52:
                                element2.elementType = 0x49;
                                element2.aggressiveDefender = true;
                                break;
                        }
                    }
                    else if (element2.elementType == 0x4d)
                    {
                        element2.aggressiveDefender = true;
                    }
                    element2.damage = 0f;
                    element2.xPos = (byte) (i % 0x76);
                    element2.yPos = (byte) (i / 0x76);
                    this.elements.Add(element2);
                }
            }
            if (compressedCastleMap != null)
            {
                byte[] buffer2 = CastlesCommon.decompressCastleData(compressedCastleMap);
                for (int j = 0; j < buffer2.Length; j++)
                {
                    if ((buffer2[j] > 0) && ((buffer2[j] != 0x24) || !ignorePits))
                    {
                        CastleElement element3 = new CastleElement {
                            completionTime = item.completionTime
                        };
                        num -= 1L;
                        element3.elementID = num;
                        element3.elementType = buffer2[j];
                        element3.damage = 0f;
                        element3.xPos = (byte) (j % 0x76);
                        element3.yPos = (byte) (j / 0x76);
                        this.elements.Add(element3);
                    }
                }
            }
            CastlesCommon.addLandTypeAdditions(this.elements, landType);
            this.attackerSetupForest = landType == 9;
            this.regenerateDefaultCatapultTargets();
            CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
            this.recalcCastleLayout();
        }

        public void importElements(List<CastleElement> newElements)
        {
            if (this.elements == null)
            {
                this.elements = new List<CastleElement>();
            }
            else
            {
                this.elements.Clear();
            }
            CastleElement item = new CastleElement {
                completionTime = DateTime.Now.AddDays(-100.0)
            };
            int num = GameEngine.Instance.World.GetResearchDataForCurrentVillage().Research_Castellation - 1;
            if (num < 0)
            {
                num = 0;
            }
            num++;
            if (CreateMode)
            {
                num = 1;
            }
            item.elementType = (byte) num;
            item.elementID = -1L;
            item.xPos = 0x3a;
            item.yPos = 0x3b;
            this.elements.Add(item);
            this.elements.AddRange(newElements);
            VillageMap map = GameEngine.Instance.getVillage(this.m_villageID);
            if (map != null)
            {
                int villageMapType = map.VillageMapType;
                CastlesCommon.addLandTypeAdditions(this.elements, villageMapType);
                this.attackerSetupForest = villageMapType == 9;
            }
            CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
            this.recalcCastleLayout();
        }

        public int initCastleSprite(SpriteWrapper sprite, int elementType, int gx, int gy, bool collapsed, CastleElement element)
        {
            Rectangle rectangle;
            PointF tf;
            SizeF ef;
            int num12;
            int castleSpritesTexID = GFXLibrary.Instance.CastleSpritesTexID;
            sprite.Visible = true;
            sprite.ColorToUse = ARGBColors.White;
            PointF tf2 = new PointF(16f, 0f);
            float num2 = 8f;
            int spriteTagOfset = 0;
            switch (elementType)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    tf2.X = 96f;
                    num2 = 40f;
                    if (!collapsed)
                    {
                        if (this.campMode == 0)
                        {
                            spriteTagOfset = 0xf4;
                        }
                        else if (this.campMode == 1)
                        {
                            spriteTagOfset = 0x1c0;
                        }
                        else if (this.campMode == 2)
                        {
                            spriteTagOfset = 0x1bb;
                        }
                    }
                    else if (this.campMode != 0)
                    {
                        if (this.campMode == 1)
                        {
                            spriteTagOfset = 0x1be;
                            if (this.displayType == 1)
                            {
                                spriteTagOfset = 0x1bf;
                            }
                        }
                        else if (this.campMode == 2)
                        {
                            spriteTagOfset = 0x1b9;
                            if (this.displayType == 1)
                            {
                                spriteTagOfset = 0x1ba;
                            }
                        }
                    }
                    else
                    {
                        spriteTagOfset = 0xe0;
                        if (this.displayType == 1)
                        {
                            spriteTagOfset = 0xea;
                        }
                    }
                    if (this.campMode == 0)
                    {
                        if (fakeKeep >= 0)
                        {
                            spriteTagOfset += fakeKeep - 1;
                        }
                        else
                        {
                            spriteTagOfset += elementType - 1;
                        }
                    }
                    goto Label_09D3;

                case 11:
                    tf2.X = 32f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 0xda;
                    }
                    else
                    {
                        spriteTagOfset = 0xd6;
                    }
                    goto Label_09D3;

                case 12:
                    tf2.X = 48f;
                    num2 = 24f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 0xdb;
                    }
                    else
                    {
                        spriteTagOfset = 0xd7;
                    }
                    goto Label_09D3;

                case 13:
                    tf2.X = 64f;
                    num2 = 24f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 220;
                    }
                    else
                    {
                        spriteTagOfset = 0xd8;
                    }
                    goto Label_09D3;

                case 14:
                    tf2.X = 80f;
                    num2 = 40f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 0xdd;
                    }
                    else
                    {
                        spriteTagOfset = 0xd9;
                    }
                    goto Label_09D3;

                case 0x15:
                    tf2.X = 32f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 0xdf;
                    }
                    else
                    {
                        spriteTagOfset = 0xde;
                    }
                    goto Label_09D3;

                case 0x1f:
                {
                    tf2.X = 101f;
                    int num4 = this.getDefenderDefenceResearch();
                    if (num4 >= 4)
                    {
                        if (num4 < 8)
                        {
                            if (collapsed)
                            {
                                spriteTagOfset = 0x1b4;
                            }
                            else
                            {
                                spriteTagOfset = 0x1b3;
                            }
                            num2 = 74f;
                        }
                        else if (num4 < 10)
                        {
                            if (collapsed)
                            {
                                spriteTagOfset = 0x1b6;
                            }
                            else
                            {
                                spriteTagOfset = 0x1b5;
                            }
                            num2 = 74f;
                        }
                        else
                        {
                            if (collapsed)
                            {
                                spriteTagOfset = 440;
                            }
                            else
                            {
                                spriteTagOfset = 0x1b7;
                            }
                            num2 = 74f;
                        }
                        goto Label_09D3;
                    }
                    if (!collapsed)
                    {
                        spriteTagOfset = 0x1b1;
                        break;
                    }
                    spriteTagOfset = 0x1b2;
                    break;
                }
                case 0x20:
                    tf2.X = 64f;
                    num2 = 24f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 210;
                    }
                    else
                    {
                        spriteTagOfset = 0xd3;
                    }
                    goto Label_09D3;

                case 0x21:
                    spriteTagOfset = (gx + gy) % 8;
                    if (!collapsed)
                    {
                        spriteTagOfset += 0x9a;
                    }
                    else
                    {
                        spriteTagOfset += 0xa2;
                    }
                    goto Label_09D3;

                case 0x22:
                    if (!collapsed)
                    {
                        spriteTagOfset = 0;
                        if (this.isInNorthSouthWall(gx, gy))
                        {
                            spriteTagOfset = 0x10 - (gy & 15);
                        }
                        else if (this.isInEastWestWall(gx, gy))
                        {
                            if ((gx & 15) == 15)
                            {
                                spriteTagOfset = 0;
                            }
                            else
                            {
                                spriteTagOfset = 0x1f - (gx & 15);
                            }
                        }
                        else if (this.isSouthEndWall(gx, gy))
                        {
                            spriteTagOfset = 0x21;
                        }
                        else if (this.isEastEndWall(gx, gy))
                        {
                            spriteTagOfset = 0x20;
                        }
                        else
                        {
                            spriteTagOfset = 0x22;
                        }
                    }
                    else
                    {
                        spriteTagOfset = ((gx + gy) % 8) + 0x23;
                    }
                    goto Label_09D3;

                case 0x23:
                    if (((gx >= 0x21) && (gy >= 0x21)) && ((gx < 0x55) && (gy < 0x55)))
                    {
                        spriteTagOfset = 0x114;
                        int num7 = 0;
                        int num8 = 0;
                        int num9 = 0;
                        while (this.moatSurroundLogic[num7 * 9] > 0)
                        {
                            bool flag = false;
                            for (int i = 0; i < 8; i++)
                            {
                                num8 = this.castleLayout.map[gx + this.moatSurroundTests[i * 2], gy + this.moatSurroundTests[(i * 2) + 1]];
                                num9 = this.moatSurroundLogic[((num7 * 9) + 1) + i];
                                if (num9 != 2)
                                {
                                    if (num8 == 0x23)
                                    {
                                        if (num9 != 0)
                                        {
                                            continue;
                                        }
                                        flag = true;
                                        break;
                                    }
                                    if (num9 == 1)
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                            if (!flag)
                            {
                                spriteTagOfset = this.moatSurroundLogic[num7 * 9];
                                int num11 = ((gx ^ gy) ^ (gx / 10)) ^ (gy / 20);
                                switch (spriteTagOfset)
                                {
                                    case 0x110:
                                        if ((num11 % 3) != 2)
                                        {
                                            spriteTagOfset = 0x18d + (num11 % 3);
                                        }
                                        break;

                                    case 0x111:
                                        if ((num11 % 3) != 2)
                                        {
                                            spriteTagOfset = 0x18f + (num11 % 3);
                                        }
                                        break;

                                    case 0x112:
                                        if ((num11 % 3) != 2)
                                        {
                                            spriteTagOfset = 0x191 + (num11 % 3);
                                        }
                                        break;

                                    case 0x113:
                                        if ((num11 % 3) != 2)
                                        {
                                            spriteTagOfset = 0x193 + (num11 % 3);
                                        }
                                        break;

                                    case 0x114:
                                        if ((num11 % 9) != 8)
                                        {
                                            spriteTagOfset = 0x195 + (num11 % 9);
                                        }
                                        break;
                                }
                                break;
                            }
                            num7++;
                        }
                    }
                    else
                    {
                        spriteTagOfset = 0x114;
                    }
                    goto Label_09D3;

                case 0x24:
                {
                    int num5 = (gx ^ gy) % 8;
                    spriteTagOfset = 0x184 + num5;
                    if ((element != null) && this.battleMode)
                    {
                        BattleBuilding building = (BattleBuilding) element;
                        if (!building.visible)
                        {
                            sprite.Visible = false;
                            return -1;
                        }
                        if (building.openPit)
                        {
                            spriteTagOfset -= 8;
                        }
                        if (building.animating)
                        {
                            int animFrame = building.animFrame;
                            if (this.castleCombat.TickValue < building.endingTick)
                            {
                                if (animFrame > 3)
                                {
                                    animFrame = 3;
                                }
                                SpriteWrapper child = this.getNextExtraSprite(GFXLibrary.Instance.AnimKillingPitsTexID, animFrame + (num5 * 4));
                                child.Center = new PointF(50f, 50f);
                                child.PosX = 0f;
                                child.PosY = 0f;
                                sprite.AddChild(child);
                            }
                        }
                    }
                    goto Label_09D3;
                }
                case 0x25:
                    tf2.X = 64f;
                    num2 = 33f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 0x1ad;
                    }
                    else
                    {
                        spriteTagOfset = 430;
                    }
                    goto Label_09D3;

                case 0x26:
                    tf2.X = 64f;
                    num2 = 33f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 0x1a9;
                    }
                    else
                    {
                        spriteTagOfset = 0x1aa;
                    }
                    goto Label_09D3;

                case 0x27:
                    tf2.X = 64f;
                    num2 = 33f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 0x1c7;
                    }
                    else
                    {
                        spriteTagOfset = 0x1c8;
                    }
                    goto Label_09D3;

                case 40:
                    tf2.X = 64f;
                    num2 = 33f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 0x1c3;
                    }
                    else
                    {
                        spriteTagOfset = 0x1c4;
                    }
                    goto Label_09D3;

                case 0x29:
                    tf2.X = 80f;
                    num2 = 16f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 460;
                    }
                    else
                    {
                        spriteTagOfset = 0x1cf;
                    }
                    goto Label_09D3;

                case 0x2a:
                    tf2.X = 80f;
                    num2 = 32f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 0x1cb;
                    }
                    else
                    {
                        spriteTagOfset = 0x1ce;
                    }
                    goto Label_09D3;

                case 0x2b:
                    tf2.X = 91f;
                    num2 = 16f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 0x1cd;
                    }
                    else
                    {
                        spriteTagOfset = 0x1d0;
                    }
                    if ((element != null) && this.battleMode)
                    {
                        BattleBuilding building2 = (BattleBuilding) element;
                        if (!building2.visible)
                        {
                            sprite.Visible = false;
                            return -1;
                        }
                    }
                    else if (this.attackerSetupMode)
                    {
                        sprite.Visible = false;
                        return -1;
                    }
                    goto Label_09D3;

                case 0x2c:
                    tf2.X = 80f;
                    num2 = 32f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 0x1d4;
                    }
                    else
                    {
                        spriteTagOfset = 0x1d5;
                    }
                    goto Label_09D3;

                case 0x2d:
                    spriteTagOfset = 470;
                    tf2.X = 24f;
                    num2 = 8f;
                    goto Label_09D3;

                case 0x2e:
                    spriteTagOfset = 0x1d7;
                    tf2.X = 24f;
                    num2 = 8f;
                    goto Label_09D3;

                case 0x33:
                    castleSpritesTexID = GFXLibrary.Instance.FreeCardIconsID;
                    if (!collapsed)
                    {
                        spriteTagOfset = 7;
                    }
                    else
                    {
                        spriteTagOfset = 11;
                    }
                    goto Label_09D3;

                case 0x34:
                    castleSpritesTexID = GFXLibrary.Instance.FreeCardIconsID;
                    if (!collapsed)
                    {
                        spriteTagOfset = 8;
                    }
                    else
                    {
                        spriteTagOfset = 12;
                    }
                    goto Label_09D3;

                case 0x35:
                    castleSpritesTexID = GFXLibrary.Instance.FreeCardIconsID;
                    if (!collapsed)
                    {
                        spriteTagOfset = 9;
                    }
                    else
                    {
                        spriteTagOfset = 13;
                    }
                    goto Label_09D3;

                case 0x36:
                    castleSpritesTexID = GFXLibrary.Instance.FreeCardIconsID;
                    if (!collapsed)
                    {
                        spriteTagOfset = 10;
                    }
                    else
                    {
                        spriteTagOfset = 14;
                    }
                    goto Label_09D3;

                case 0x37:
                    castleSpritesTexID = GFXLibrary.Instance.FreeCardIconsID;
                    tf2.X = 32f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 15;
                    }
                    else
                    {
                        spriteTagOfset = 0x11;
                    }
                    goto Label_09D3;

                case 0x38:
                    castleSpritesTexID = GFXLibrary.Instance.FreeCardIconsID;
                    tf2.X = 32f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 0x10;
                    }
                    else
                    {
                        spriteTagOfset = 0x12;
                    }
                    goto Label_09D3;

                case 0x39:
                    castleSpritesTexID = GFXLibrary.Instance.FreeCardIconsID;
                    tf2.X = 48f;
                    num2 = 24f;
                    if (!collapsed)
                    {
                        spriteTagOfset = 0x13;
                    }
                    else
                    {
                        spriteTagOfset = 20;
                    }
                    goto Label_09D3;

                default:
                    goto Label_09D3;
            }
            num2 = 95f;
        Label_09D3:
            num12 = 1;
            this.gfx.getSpriteLoader(castleSpritesTexID, ref num12).GetSpriteXYdata(num12, spriteTagOfset, out rectangle, out tf, out ef);
            tf2.Y = ((int) ef.Height) - num2;
            sprite.Center = tf2;
            return spriteTagOfset;
        }

        public void initFakeSetup()
        {
            this.placingAttackerRealMode = false;
            this.attackMaxPeasants = 0x3e8;
            this.attackMaxArchers = 0x3e8;
            this.attackMaxPikemen = 0x3e8;
            this.attackMaxSwordsmen = 0x3e8;
            this.attackMaxCatapults = 0x3e8;
            this.attackMaxCaptains = 5;
            this.attackRealAttackingVillage = -1;
            this.attackRealTargetVillage = -1;
            this.attackNumPeasants = 0;
            this.attackNumArchers = 0;
            this.attackNumPikemen = 0;
            this.attackNumSwordsmen = 0;
            this.attackNumCatapults = 0;
            this.attackNumCaptains = 0;
            this.attackMaxPeasantsInCastle = 0;
            this.attackMaxArchersInCastle = 0;
            this.attackMaxPikemenInCastle = 0;
            this.attackMaxSwordsmenInCastle = 0;
            this.attackCaptainsCommand = 1;
            InterfaceMgr.Instance.castleShowPlacedAttackers(this.attackNumPeasants, this.attackNumArchers, this.attackNumPikemen, this.attackNumSwordsmen, this.attackNumCatapults, this.attackMaxPeasants, this.attackMaxArchers, this.attackMaxPikemen, this.attackMaxSwordsmen, this.attackMaxCatapults, this.attackNumCaptains, this.attackMaxCaptains, this.attackCaptainsCommand, this.attackMaxPeasantsInCastle, this.attackMaxArchersInCastle, this.attackMaxPikemenInCastle, this.attackMaxSwordsmenInCastle);
            InterfaceMgr.Instance.castleAttackShowRealAttack(false);
            this.localTempElementNumber = -3L;
        }

        public void initRealSetup(int attackingVillage, int targetVillage, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int attackType, int pillagePercent, int captainsCommand, int parentOfAttackingVillage, int numPeasantsInCastle, int numArchersInCastle, int numPikemenInCastle, int numSwordsmenInCastle, int targetUserID, string targetUserName, BattleHonourData battleHonourData, int numCaptainsInCastle, int numCaptains, double capitalAttackRate)
        {
            this.m_villageID = attackingVillage;
            this.ParentOfAttackingVillage = parentOfAttackingVillage;
            this.placingAttackerRealMode = true;
            this.attackRealAttackingVillage = attackingVillage;
            this.attackRealTargetVillage = targetVillage;
            this.attackMaxPeasants = numPeasants;
            this.attackMaxArchers = numArchers;
            this.attackMaxPikemen = numPikemen;
            this.attackMaxSwordsmen = numSwordsmen;
            this.attackMaxCatapults = numCatapults;
            this.attackMaxPeasantsInCastle = numPeasantsInCastle;
            this.attackMaxArchersInCastle = numArchersInCastle;
            this.attackMaxPikemenInCastle = numPikemenInCastle;
            this.attackMaxSwordsmenInCastle = numSwordsmenInCastle;
            this.attackRealAttackType = attackType;
            this.attackPillagePercent = pillagePercent;
            this.attackCaptainsCommand = captainsCommand;
            this.attackMaxCaptains = numCaptains;
            this.attackNumPeasants = 0;
            this.attackNumArchers = 0;
            this.attackNumPikemen = 0;
            this.attackNumSwordsmen = 0;
            this.attackNumCatapults = 0;
            this.attackNumCaptains = 0;
            this.m_targetUserID = targetUserID;
            this.m_targetUserName = targetUserName;
            this.m_battleHonourData = battleHonourData;
            this.attackCapitalAttackRate = capitalAttackRate;
            InterfaceMgr.Instance.castleShowPlacedAttackers(this.attackNumPeasants, this.attackNumArchers, this.attackNumPikemen, this.attackNumSwordsmen, this.attackNumCatapults, this.attackMaxPeasants, this.attackMaxArchers, this.attackMaxPikemen, this.attackMaxSwordsmen, this.attackMaxCatapults, this.attackNumCaptains, this.attackMaxCaptains, this.attackCaptainsCommand, this.attackMaxPeasantsInCastle, this.attackMaxArchersInCastle, this.attackMaxPikemenInCastle, this.attackMaxSwordsmenInCastle);
            InterfaceMgr.Instance.castleAttackShowRealAttack(true);
            this.localTempElementNumber = -3L;
        }

        public void initRockchips()
        {
            this.castleCombat.setRockCallback(new CastleCombat.RockChipCallback(this.rockChipCallback));
        }

        public bool isAnyConstructing()
        {
            DateTime time = VillageMap.getCurrentServerTime();
            foreach (CastleElement element in this.elements)
            {
                if ((element.elementType < 0x45) && (element.completionTime > time))
                {
                    return true;
                }
            }
            return false;
        }

        private bool isAttackReady()
        {
            if (((this.attackNumPeasants <= 0) && (this.attackNumArchers <= 0)) && (((this.attackNumPikemen <= 0) && (this.attackNumSwordsmen <= 0)) && (this.attackNumCaptains <= 0)))
            {
                return false;
            }
            return true;
        }

        public bool isEastEndWall(int x, int y)
        {
            if (this.castleLayout != null)
            {
                if (x >= 0x74)
                {
                    return false;
                }
                if (this.castleLayout.map[x + 1, y] == 0x22)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isInDeleteConstructing()
        {
            if (this.inDeleteConstructing)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastDeleteConstructing);
                if (span.TotalMinutes > 2.0)
                {
                    this.inDeleteConstructing = false;
                }
            }
            return this.inDeleteConstructing;
        }

        public bool isInEastWestWall(int x, int y)
        {
            if (this.castleLayout != null)
            {
                if ((x <= 0) || (x >= 0x74))
                {
                    return false;
                }
                if ((this.castleLayout.map[x + 1, y] == 0x22) && (this.castleLayout.map[x - 1, y] == 0x22))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isInNorthSouthWall(int x, int y)
        {
            if (this.castleLayout != null)
            {
                if ((y <= 0) || (y >= 0x74))
                {
                    return false;
                }
                if ((this.castleLayout.map[x, y + 1] == 0x22) && (this.castleLayout.map[x, y - 1] == 0x22))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isMouseOverCancelButton(Point mousePos)
        {
            if ((backgroundSprite != null) && (placementSprite_cancel != null))
            {
                Rectangle rectangle = new Rectangle {
                    X = (int) (placementSprite_cancel.DrawPos.X - (placementSprite_cancel.Width / 2f)),
                    Y = (int) (placementSprite_cancel.DrawPos.Y - (placementSprite_cancel.Height / 2f)),
                    Width = (int) placementSprite_cancel.Width,
                    Height = (int) placementSprite_cancel.Height
                };
                if (rectangle.Contains(mousePos))
                {
                    UniversalDebugLog.Log("hit cancel button");
                    return true;
                }
            }
            return false;
        }

        public bool isMouseOverConfirmButton(Point mousePos)
        {
            if ((backgroundSprite != null) && (placementSprite_confirm != null))
            {
                Rectangle rectangle = new Rectangle {
                    X = (int) (placementSprite_confirm.DrawPos.X - (placementSprite_confirm.Width / 2f)),
                    Y = (int) (placementSprite_confirm.DrawPos.Y - (placementSprite_confirm.Height / 2f)),
                    Width = (int) placementSprite_confirm.Width,
                    Height = (int) placementSprite_confirm.Height
                };
                if (rectangle.Contains(mousePos))
                {
                    UniversalDebugLog.Log("hit confirm button");
                    return true;
                }
            }
            return false;
        }

        public int isMouseOverHandle(Point mousePos)
        {
            if (((placementSprite != null) && (backgroundSprite != null)) && ((placementSprite_handleone != null) && (placementSprite_handletwo != null)))
            {
                Rectangle rectangle = new Rectangle {
                    X = (int) (placementSprite_handleone.DrawPos.X - (placementSprite_handleone.Width / 2f)),
                    Y = (int) (placementSprite_handleone.DrawPos.Y - (placementSprite_handleone.Height / 2f)),
                    Width = (int) placementSprite_handleone.Width,
                    Height = (int) placementSprite_handleone.Height
                };
                Rectangle rectangle2 = new Rectangle {
                    X = (int) (placementSprite_handletwo.DrawPos.X - (placementSprite_handletwo.Width / 2f)),
                    Y = (int) (placementSprite_handletwo.DrawPos.Y - (placementSprite_handletwo.Height / 2f)),
                    Width = (int) placementSprite_handletwo.Width,
                    Height = (int) placementSprite_handletwo.Height
                };
                if (rectangle.Contains(mousePos))
                {
                    UniversalDebugLog.Log("hit handle one");
                    return 1;
                }
                if (rectangle2.Contains(mousePos))
                {
                    UniversalDebugLog.Log("hit handle two");
                    return 2;
                }
            }
            return 0;
        }

        public bool isMouseOverPlacementSprite(Point mousePos)
        {
            if ((placementSprite != null) && (backgroundSprite != null))
            {
                if (((placementType == 0x22) || (placementType == 0x21)) || ((placementType == 0x41) || (placementType == 0x42)))
                {
                    return false;
                }
                int mapX = -1;
                int mapY = -1;
                this.getMapTileFromMousePos(mousePos, ref mapX, ref mapY);
                int num3 = 3;
                int num4 = Math.Abs((int) (mapX - this.lastMoveTileX));
                int num5 = Math.Abs((int) (mapY - this.lastMoveTileY));
                if ((num4 < num3) && (num5 < num3))
                {
                    UniversalDebugLog.Log("clicked on placement building");
                    return true;
                }
            }
            return false;
        }

        public bool isMouseOverTroopPlacementSprite(Point mousePos)
        {
            if (this.dummySprite != null)
            {
                int mapX = -1;
                int mapY = -1;
                this.getMapTileFromMousePos(mousePos, ref mapX, ref mapY);
                int num3 = 5;
                int num4 = Math.Abs((int) (mapX - this.lastMoveTileX));
                int num5 = Math.Abs((int) (mapY - this.lastMoveTileY));
                if ((num4 < num3) && (num5 < num3))
                {
                    UniversalDebugLog.Log("clicked on placement troop");
                    return true;
                }
            }
            else
            {
                UniversalDebugLog.Log("Dummy sprite is null");
            }
            return false;
        }

        public bool isSouthEndWall(int x, int y)
        {
            if (this.castleLayout != null)
            {
                if (y >= 0x74)
                {
                    return false;
                }
                if (this.castleLayout.map[x, y + 1] == 0x22)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isTutorialEnclosedComplete()
        {
            if (!this.inBuilderMode && !this.inTroopPlacerMode)
            {
                if (this.m_castleEnclosed)
                {
                    return true;
                }
                if (this.elements.Count != 0x1f)
                {
                    foreach (CastleElement element in this.elements)
                    {
                        if ((element.elementType > 10) && (element.elementType < 0x45))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void justDrawSprites()
        {
            if (backgroundSprite != null)
            {
                backgroundSprite.Update();
                backgroundSprite.AddToRenderList();
                this.drawSurroundSprites();
            }
        }

        public void lassoDelete(bool attacking, int troopType)
        {
            if (!attacking)
            {
                if (!this.InTroopPlacerMode)
                {
                    this.startTroopPlacerMode();
                }
                List<long> list = new List<long>();
                foreach (long num in this.m_lassoElements)
                {
                    VillageMap map;
                    CastleElement item = this.castleLayout.getElementFromElemID(num);
                    if ((item != null) && (item.elementType == troopType))
                    {
                        if (item.elementID >= 0L)
                        {
                            list.Add(num);
                        }
                        this.elements.Remove(item);
                        if (!CreateMode)
                        {
                            map = GameEngine.Instance.Village;
                            if (map != null)
                            {
                                switch (item.elementType)
                                {
                                    case 70:
                                        if (!item.vassalReinforcements)
                                        {
                                            goto Label_00D3;
                                        }
                                        map.addVassalTroops(1, 0, 0, 0);
                                        break;

                                    case 0x47:
                                        if (!item.vassalReinforcements)
                                        {
                                            goto Label_018D;
                                        }
                                        map.addVassalTroops(0, 0, 0, 1);
                                        break;

                                    case 0x48:
                                        if (!item.vassalReinforcements)
                                        {
                                            goto Label_0115;
                                        }
                                        map.addVassalTroops(0, 1, 0, 0);
                                        break;

                                    case 0x49:
                                        if (!item.vassalReinforcements)
                                        {
                                            goto Label_0154;
                                        }
                                        map.addVassalTroops(0, 0, 1, 0);
                                        break;

                                    case 0x55:
                                        goto Label_01B2;
                                }
                            }
                        }
                    }
                    continue;
                Label_00D3:
                    if (item.reinforcement)
                    {
                        this.numPlacedReinforceDefenderPeasants--;
                    }
                    else
                    {
                        map.addTroops(1, 0, 0, 0, 0);
                    }
                    continue;
                Label_0115:
                    if (item.reinforcement)
                    {
                        this.numPlacedReinforceDefenderArchers--;
                    }
                    else
                    {
                        map.addTroops(0, 1, 0, 0, 0);
                    }
                    continue;
                Label_0154:
                    if (item.reinforcement)
                    {
                        this.numPlacedReinforceDefenderPikemen--;
                    }
                    else
                    {
                        map.addTroops(0, 0, 1, 0, 0);
                    }
                    continue;
                Label_018D:
                    if (item.reinforcement)
                    {
                        this.numPlacedReinforceDefenderSwordsmen--;
                    }
                    else
                    {
                        map.addTroops(0, 0, 0, 1, 0);
                    }
                    continue;
                Label_01B2:
                    if (!item.vassalReinforcements && !item.reinforcement)
                    {
                        map.addTroops(0, 0, 0, 0, 0, 0, 1);
                    }
                }
                foreach (long num2 in list)
                {
                    CastleElement element2 = this.castleLayout.getElementFromElemID(num2);
                    if ((element2 != null) && (element2.elementType == troopType))
                    {
                        if (element2.elementID >= 0L)
                        {
                            this.removedElements.Add(element2);
                        }
                        this.movedElements.Remove(element2);
                    }
                }
                if (troopType == 70)
                {
                    this.numAvailableDefenderPeasants = 0;
                }
                if (troopType == 0x48)
                {
                    this.numAvailableDefenderArchers = 0;
                }
                if (troopType == 0x49)
                {
                    this.numAvailableDefenderPikemen = 0;
                }
                if (troopType == 0x47)
                {
                    this.numAvailableDefenderSwordsmen = 0;
                }
                if (troopType == 0x55)
                {
                    this.numAvailableDefenderCaptains = 0;
                }
                VillageMap village = GameEngine.Instance.Village;
                if (village != null)
                {
                    village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
                    village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
                }
                GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                this.recalcCastleLayout();
                this.updateLasso(true);
                if (this.m_lassoElements.Count > 0)
                {
                    this.lassoMade();
                }
                else
                {
                    this.clearLasso();
                }
                InterfaceMgr.Instance.castleStartBuilderMode();
            }
            else
            {
                foreach (long num3 in this.m_lassoElements)
                {
                    CastleElement element3 = this.castleLayout.getElementFromElemID(num3);
                    if ((element3 != null) && this.matchDeleteTypeForCaptains(element3.elementType, troopType))
                    {
                        this.elements.Remove(element3);
                        this.deleteCatapultTarget(num3);
                        this.deleteCaptainsDetails(num3);
                    }
                }
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                this.recalcCastleLayout();
                this.updateLasso(true);
                if (this.m_lassoElements.Count > 0)
                {
                    this.lassoMade();
                }
                else
                {
                    this.clearLasso();
                }
            }
        }

        public void lassoMade()
        {
            if (!this.attackerSetupMode)
            {
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                int num7 = 0;
                int num8 = 0;
                int num9 = 0;
                int num10 = 0;
                foreach (long num11 in this.m_lassoElements)
                {
                    CastleElement element = this.castleLayout.getElementFromElemID(num11);
                    if (element != null)
                    {
                        if (!element.aggressiveDefender)
                        {
                            goto Label_00AE;
                        }
                        switch (element.elementType)
                        {
                            case 70:
                                num2++;
                                break;

                            case 0x47:
                                num8++;
                                break;

                            case 0x48:
                                num4++;
                                break;

                            case 0x49:
                                num6++;
                                break;

                            case 0x55:
                                goto Label_00A6;
                        }
                    }
                    continue;
                Label_00A6:
                    num10++;
                    continue;
                Label_00AE:
                    switch (element.elementType)
                    {
                        case 70:
                            num++;
                            break;

                        case 0x47:
                            num7++;
                            break;

                        case 0x48:
                            num3++;
                            break;

                        case 0x49:
                            num5++;
                            break;

                        case 0x55:
                            num9++;
                            break;
                    }
                }
                int peasantsState = 0;
                if (num2 > 0)
                {
                    if (num > 0)
                    {
                        peasantsState = -1;
                    }
                    else
                    {
                        peasantsState = 1;
                    }
                }
                int archersState = 0;
                if (num4 > 0)
                {
                    if (num3 > 0)
                    {
                        archersState = -1;
                    }
                    else
                    {
                        archersState = 1;
                    }
                }
                int pikemenState = 0;
                if (num6 > 0)
                {
                    if (num5 > 0)
                    {
                        pikemenState = -1;
                    }
                    else
                    {
                        pikemenState = 1;
                    }
                }
                int swordsmenState = 0;
                if (num8 > 0)
                {
                    if (num7 > 0)
                    {
                        swordsmenState = -1;
                    }
                    else
                    {
                        swordsmenState = 1;
                    }
                }
                int captainState = 0;
                if (num10 > 0)
                {
                    if (num9 > 0)
                    {
                        captainState = -1;
                    }
                    else
                    {
                        captainState = 1;
                    }
                }
                InterfaceMgr.Instance.castle_SetSelectedTroop(num + num2, peasantsState, num3 + num4, archersState, num5 + num6, pikemenState, num7 + num8, swordsmenState, num9 + num10, captainState);
            }
            else
            {
                int numPeasants = 0;
                int numArchers = 0;
                int numPikemen = 0;
                int numSwordsmen = 0;
                int numCatapults = 0;
                int numCaptains = 0;
                int captainsCommand = 0;
                int captainsData = 0;
                foreach (long num25 in this.m_lassoElements)
                {
                    CastleElement element2 = this.castleLayout.getElementFromElemID(num25);
                    if (element2 != null)
                    {
                        switch (element2.elementType)
                        {
                            case 90:
                                numPeasants++;
                                break;

                            case 0x5b:
                                numSwordsmen++;
                                break;

                            case 0x5c:
                                numArchers++;
                                break;

                            case 0x5d:
                                numPikemen++;
                                break;

                            case 0x5e:
                                numCatapults++;
                                break;

                            case 100:
                            case 0x65:
                            case 0x66:
                            case 0x67:
                            case 0x68:
                            case 0x69:
                            case 0x6a:
                            case 0x6b:
                                numCaptains++;
                                captainsCommand = element2.elementType;
                                if (numCaptains == 1)
                                {
                                    captainsData = this.getCaptainsDetails(element2.elementID);
                                }
                                break;
                        }
                    }
                }
                InterfaceMgr.Instance.castleAttack_SetSelectedTroop(numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numCaptains, captainsCommand, captainsData);
            }
        }

        private bool lassoMoveTroop(long elemID, int mapX, int mapY)
        {
            CastleElement element = this.castleLayout.getElementFromElemID(elemID);
            if (element == null)
            {
                return false;
            }
            placementType = element.elementType;
            if (!this.mouseMovePlaceTroops(mapX, mapY, false, -1) || (element.elementID != elemID))
            {
                return false;
            }
            if (!this.attackerSetupMode && !CreateMode)
            {
                if (!this.InTroopPlacerMode)
                {
                    this.startTroopPlacerMode();
                }
                this.moveTroopLocal(element, mapX, mapY);
            }
            element.xPos = (byte) mapX;
            element.yPos = (byte) mapY;
            if (((element.elementType == 0x5e) || (element.elementType == 0x66)) || (element.elementType == 0x67))
            {
                foreach (CatapultTarget target in this.catapultTargets)
                {
                    if (element.elementID == target.elemID)
                    {
                        target.createDefaultLocation(element.xPos, element.yPos, element);
                        break;
                    }
                }
            }
            CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
            InterfaceMgr.Instance.castleStartBuilderMode();
            return true;
        }

        public void launchArmy()
        {
            byte[] fullData = this.castleLayout.createAttackerMapArray();
            if (this.catapultTargets.Count > 0)
            {
                byte[] buffer2 = new byte[this.catapultTargets.Count * 4];
                int index = 0;
                foreach (CatapultTarget target in this.catapultTargets)
                {
                    foreach (CastleElement element in this.elements)
                    {
                        if ((((element.elementType == 0x5e) || (element.elementType == 0x66)) || (element.elementType == 0x67)) && (element.elementID == target.elemID))
                        {
                            if (!target.valid)
                            {
                                target.createDefaultLocation(element.xPos, element.yPos, element);
                            }
                            buffer2[index * 4] = element.xPos;
                            buffer2[(index * 4) + 1] = element.yPos;
                            buffer2[(index * 4) + 2] = target.xPos;
                            buffer2[(index * 4) + 3] = target.yPos;
                            index++;
                            break;
                        }
                    }
                }
                byte[] buffer3 = new byte[fullData.Length + buffer2.Length];
                index = 0;
                int num3 = 0;
                while (num3 < fullData.Length)
                {
                    buffer3[index] = fullData[num3];
                    num3++;
                    index++;
                }
                int num4 = 0;
                while (num4 < buffer2.Length)
                {
                    buffer3[index] = buffer2[num4];
                    num4++;
                    index++;
                }
                fullData = buffer3;
            }
            if (this.captainsDetails.Count > 0)
            {
                byte[] buffer4 = new byte[this.captainsDetails.Count * 3];
                int num6 = 0;
                foreach (CaptainsDetails details in this.captainsDetails)
                {
                    foreach (CastleElement element2 in this.elements)
                    {
                        if (((element2.elementType >= 100) && (element2.elementType <= 0x6d)) && (element2.elementID == details.elemID))
                        {
                            buffer4[num6 * 3] = element2.xPos;
                            buffer4[(num6 * 3) + 1] = element2.yPos;
                            buffer4[(num6 * 3) + 2] = details.seconds;
                            num6++;
                            break;
                        }
                    }
                }
                byte[] buffer5 = new byte[fullData.Length + buffer4.Length];
                num6 = 0;
                int num7 = 0;
                while (num7 < fullData.Length)
                {
                    buffer5[num6] = fullData[num7];
                    num7++;
                    num6++;
                }
                int num8 = 0;
                while (num8 < buffer4.Length)
                {
                    buffer5[num6] = buffer4[num8];
                    num8++;
                    num6++;
                }
                fullData = buffer5;
            }
            byte[] troopMap = CastlesCommon.compressCastleData(fullData);
            int targetVillageID = -1;
            if (this.placingAttackerRealMode)
            {
                targetVillageID = this.attackRealTargetVillage;
            }
            RemoteServices.Instance.set_LaunchCastleAttack_UserCallBack(new RemoteServices.LaunchCastleAttack_UserCallBack(this.launchCastleAttackCallback));
            RemoteServices.Instance.LaunchCastleAttack(this.ParentOfAttackingVillage, this.m_villageID, targetVillageID, troopMap, this.attackNumPeasants, this.attackNumArchers, this.attackNumPikemen, this.attackNumSwordsmen, this.attackNumCatapults, this.attackRealAttackType, this.attackPillagePercent, this.attackCaptainsCommand, this.attackNumCaptains);
            AllVillagesPanel.travellersChanged();
            tempCompressedAttackerMap = troopMap;
            GameEngine.Instance.flushVillage(this.m_villageID);
        }

        public void launchBattle(byte[] compressedCastleMap, byte[] compressedCastleDamageMap, byte[] compressedDefenderMap, byte[] compressedAttackerMap, int keepType, CastleResearchData defenderResearchData, CastleResearchData attackerResearchData, int castleMode, int pillageInfo, int ransackCount, int raidCount, int landType, bool addLandFeatures, bool oldReport)
        {
            GameEngine.Instance.AudioEngine.unloadUnplayingSounds();
            this.m_defenderResearch = defenderResearchData;
            this.m_attackerResearch = attackerResearchData;
            this.endOfBattle = false;
            this.battleMode = true;
            this.displayType = 1;
            this.fastPlayback = false;
            this.realBattleMode = true;
            this.battleLandType = landType;
            bool ignoreForestSetup = false;
            this.attackerSetupForest = landType == 9;
            if (this.attackerSetupForest && oldReport)
            {
                this.attackerSetupForest = false;
                ignoreForestSetup = true;
            }
            this.castleCombat = new CastleCombat();
            if (castleMode == 1)
            {
                this.castleCombat.setAsBanditCamp();
            }
            if (castleMode == 2)
            {
                this.castleCombat.setAsWolfCamp();
            }
            this.initRockchips();
            if (compressedAttackerMap == null)
            {
                compressedAttackerMap = tempCompressedAttackerMap;
            }
            this.castleCombat.InitExtremeLogging("Client View Report.txt");
            this.castleCombat.setSoundTracking();
            this.castleLayout = this.castleCombat.startBattle(GameEngine.Instance.LocalWorldData, compressedCastleMap, compressedCastleDamageMap, compressedDefenderMap, compressedAttackerMap, 0x3e8, 0x3e8, 0x3e8, 0x3e8, keepType, null, VillageMap.getCurrentServerTime(), defenderResearchData, attackerResearchData, pillageInfo, ransackCount, raidCount, landType, ignoreForestSetup);
            this.startingTroopNumbers = this.castleCombat.getBattleTroopNumbers();
            this.elements = this.castleCombat.getElementList();
            if (addLandFeatures)
            {
                CastlesCommon.addLandTypeAdditions(this.elements, landType);
            }
            this.recalcCastleLayout();
            if (this.castleCombat.numTreasurePieces > 0)
            {
                this.treasureCastle = true;
                this.treasureCastleClock = 300;
            }
            else
            {
                this.treasureCastle = false;
            }
        }

        public void launchCastleAttackCallback(LaunchCastleAttack_ReturnType returnData)
        {
            if (returnData.protectedVillage)
            {
                MessageBox.Show(SK.Text("CastleMap_Interdiction", "This village is protected from attack by an Interdiction."), SK.Text("CastleMap_Protected", "Village Protected"));
                InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                InterfaceMgr.Instance.getMainTabBar().changeTab(0);
            }
            else if ((returnData.targetVillage >= 0) && returnData.Success)
            {
                if (returnData.villageResourcesAndStats != null)
                {
                    VillageMap map = GameEngine.Instance.getVillage(returnData.sourceVillage);
                    if (map != null)
                    {
                        map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    }
                }
                ArmyReturnData[] armyReturnData = new ArmyReturnData[] { returnData.armyData };
                GameEngine.Instance.World.doGetArmyData(armyReturnData, null, false);
                GameEngine.Instance.World.addExistingArmy(returnData.armyData.armyID);
                InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                InterfaceMgr.Instance.getMainTabBar().changeTab(0);
                if (SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(returnData.targetVillage)))
                {
                    GameEngine.Instance.World.setLastTreasureCastleAttackTime(VillageMap.getCurrentServerTime());
                }
                AttackTargetsPanel.addRecent(returnData.targetVillage);
            }
        }

        public void leaveMap()
        {
            Sound.stopVillageEnvironmentalExceptWorld();
        }

        public void loadCamp(string filename)
        {
            new Random();
            List<CampCastleElement> list = new List<CampCastleElement>();
            FileStream input = new FileStream(filename, FileMode.Open);
            BinaryReader reader = new BinaryReader(input);
            int num = reader.ReadInt32();
            for (int i = 0; i < num; i++)
            {
                CampCastleElement item = new CampCastleElement {
                    xPos = reader.ReadByte(),
                    yPos = reader.ReadByte(),
                    elementType = reader.ReadByte(),
                    aggressiveDefender = reader.ReadBoolean()
                };
                list.Add(item);
            }
            reader.Close();
            input.Close();
            this.elements.Clear();
            this.localTempElementNumber = -4L;
            foreach (CampCastleElement element2 in list)
            {
                long num3;
                CastleElement element3 = new CastleElement {
                    xPos = element2.xPos,
                    yPos = element2.yPos,
                    elementType = element2.elementType,
                    aggressiveDefender = element2.aggressiveDefender
                };
                this.localTempElementNumber = (num3 = this.localTempElementNumber) - 1L;
                element3.elementID = num3;
                element3.completionTime = DateTime.Now.AddHours(-1.0);
                this.elements.Add(element3);
            }
            CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
            this.recalcCastleLayout();
        }

        public static void loadCastleGFX(GraphicsMgr gfx)
        {
        }

        public void manageTutorial()
        {
            if ((this.m_castleEnclosed && !GameEngine.Instance.World.TutorialIsAdvancing()) && (GameEngine.Instance.World.getTutorialStage() == 11))
            {
                GameEngine.Instance.World.forceTutorialToBeShown();
            }
        }

        private bool matchDeleteTypeForCaptains(int type1, int type2)
        {
            return ((type1 == type2) || (((type1 >= 100) && (type1 <= 0x6d)) && ((type2 >= 100) && (type2 <= 0x6d))));
        }

        public bool memoriseAttackSetup(int ID)
        {
            FileStream output = null;
            BinaryWriter writer = null;
            try
            {
                CampCastleElement[] elementArray = this.castleLayout.createCastleCampArray_MemoriseAttackSetupTroops();
                output = new FileStream(this.getAttackSetupSaveName(ID), FileMode.Create);
                writer = new BinaryWriter(output);
                int length = elementArray.Length;
                writer.Write(length);
                foreach (CampCastleElement element in elementArray)
                {
                    writer.Write(element.xPos);
                    writer.Write(element.yPos);
                    writer.Write(element.elementType);
                    if (element.elementType == 0x5e)
                    {
                        Point point = this.getCatapultAttackLocation(element.xPos, element.yPos);
                        writer.Write((byte) point.X);
                        writer.Write((byte) point.Y);
                    }
                    if ((element.elementType >= 100) && (element.elementType < 0x6d))
                    {
                        int num2 = this.getCaptainsDelayValue(element.xPos, element.yPos);
                        writer.Write((byte) num2);
                        if ((element.elementType == 0x66) || (element.elementType == 0x67))
                        {
                            Point point2 = this.getCatapultAttackLocation(element.xPos, element.yPos);
                            writer.Write((byte) point2.X);
                            writer.Write((byte) point2.Y);
                        }
                    }
                }
                writer.Close();
                output.Close();
                return true;
            }
            catch (Exception)
            {
                try
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                    if (output != null)
                    {
                        output.Close();
                    }
                }
                catch (Exception)
                {
                }
                return false;
            }
        }

        public bool memoriseAttackSetup(string name)
        {
            FileStream output = null;
            BinaryWriter writer = null;
            try
            {
                CampCastleElement[] elementArray = this.castleLayout.createCastleCampArray_MemoriseAttackSetupTroops();
                output = new FileStream(this.getAttackSetupSaveName(name), FileMode.Create);
                writer = new BinaryWriter(output);
                int length = elementArray.Length;
                writer.Write(length);
                foreach (CampCastleElement element in elementArray)
                {
                    writer.Write(element.xPos);
                    writer.Write(element.yPos);
                    writer.Write(element.elementType);
                    if (element.elementType == 0x5e)
                    {
                        Point point = this.getCatapultAttackLocation(element.xPos, element.yPos);
                        writer.Write((byte) point.X);
                        writer.Write((byte) point.Y);
                    }
                    if ((element.elementType >= 100) && (element.elementType < 0x6d))
                    {
                        int num2 = this.getCaptainsDelayValue(element.xPos, element.yPos);
                        writer.Write((byte) num2);
                        if ((element.elementType == 0x66) || (element.elementType == 0x67))
                        {
                            Point point2 = this.getCatapultAttackLocation(element.xPos, element.yPos);
                            writer.Write((byte) point2.X);
                            writer.Write((byte) point2.Y);
                        }
                    }
                }
                writer.Close();
                output.Close();
                return true;
            }
            catch (Exception)
            {
                try
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                    if (output != null)
                    {
                        output.Close();
                    }
                }
                catch (Exception)
                {
                }
                return false;
            }
        }

        public bool memoriseInfrastructure()
        {
            FileStream output = null;
            BinaryWriter writer = null;
            try
            {
                CampCastleElement[] elementArray = this.castleLayout.createCastleCampArray_MemoriseInfrastructure();
                output = new FileStream(this.getInfrastructureSaveName(), FileMode.Create);
                writer = new BinaryWriter(output);
                int length = elementArray.Length;
                writer.Write(length);
                foreach (CampCastleElement element in elementArray)
                {
                    writer.Write(element.xPos);
                    writer.Write(element.yPos);
                    writer.Write(element.elementType);
                    writer.Write(element.aggressiveDefender);
                }
                writer.Close();
                output.Close();
                return true;
            }
            catch (Exception)
            {
                try
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                    if (output != null)
                    {
                        output.Close();
                    }
                }
                catch (Exception)
                {
                }
                return false;
            }
        }

        public bool memoriseTroops()
        {
            FileStream output = null;
            BinaryWriter writer = null;
            try
            {
                CampCastleElementLL[] tllArray = this.castleLayout.createCastleCampArray_MemoriseTroops();
                output = new FileStream(this.getTroopsSaveName(), FileMode.Create);
                writer = new BinaryWriter(output);
                int num = -1;
                writer.Write(num);
                int length = tllArray.Length;
                writer.Write(length);
                foreach (CampCastleElementLL tll in tllArray)
                {
                    writer.Write(tll.xPos);
                    writer.Write(tll.yPos);
                    writer.Write(tll.elementType);
                    writer.Write(tll.aggressiveDefender);
                    writer.Write(tll.reinforcement);
                }
                writer.Close();
                output.Close();
                return true;
            }
            catch (Exception)
            {
                try
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                    if (output != null)
                    {
                        output.Close();
                    }
                }
                catch (Exception)
                {
                }
                return false;
            }
        }

        public void mouseClicked(Point mousePos)
        {
            if (backgroundSprite != null)
            {
                if (this.m_lassoMade && !GameEngine.shiftPressed)
                {
                    Point point = mousePos;
                    point.X += -((int) backgroundSprite.DrawPos.X);
                    point.Y += -((int) backgroundSprite.DrawPos.Y);
                    long num = this.clickFindTroop(point);
                    if ((this.m_lassoElements.Count == 1) && (this.m_lassoElements[0] == num))
                    {
                        TimeSpan span = (TimeSpan) (DateTime.Now - this.troopSelectDoubleClickTIme);
                        if (span.TotalMilliseconds < 500.0)
                        {
                            foreach (CastleElement element in this.elements)
                            {
                                if (element.elementID == num)
                                {
                                    foreach (CastleElement element2 in this.elements)
                                    {
                                        if (((element2 != element) && (element2.elementType == element.elementType)) && (!this.attackerSetupMode || (element2.elementID < -2L)))
                                        {
                                            this.m_lassoElements.Add(element2.elementID);
                                        }
                                    }
                                    break;
                                }
                            }
                            this.lassoMade();
                            this.recalcCastleLayout();
                            return;
                        }
                    }
                    this.clearLasso();
                }
                int mapX = -1;
                int mapY = -1;
                if (!this.troopMovingMode)
                {
                    if (!this.deleting)
                    {
                        if ((placementSprite != null) && placingElement)
                        {
                            if (((((placementType != 0x22) && (placementType != 0x21)) && ((placementType != 0x41) && (placementType != 0x42))) && ((placementType != 0x24) && (placementType != 0x23))) && this.getMapTileFromMousePos(mousePos, ref mapX, ref mapY))
                            {
                                this.placeBuildingElement(mapX, mapY);
                            }
                        }
                        else if (!this.inBuilderMode || CreateMode)
                        {
                            if ((placementTroopSprite[0] != null) && !placingElement)
                            {
                                if (this.getMapTileFromMousePos(mousePos, ref mapX, ref mapY))
                                {
                                    this.stopPlacementOnTroopModeSwap = false;
                                    this.confirmTroopPlacement(mapX, mapY);
                                    CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                                    this.recalcCastleLayout();
                                    if (this.stopPlacementOnTroopModeSwap)
                                    {
                                        this.stopPlaceElement();
                                    }
                                }
                            }
                            else if ((((!this.attackerSetupMode || !this.getMapTileFromMousePos(mousePos, ref mapX, ref mapY)) || (this.m_lassoMade && !GameEngine.shiftPressed)) || !this.selectCatapult(mapX, mapY)) && (!this.battleMode && !this.draggingWall))
                            {
                                Point point2 = mousePos;
                                point2.X += -((int) backgroundSprite.DrawPos.X);
                                point2.Y += -((int) backgroundSprite.DrawPos.Y);
                                long item = this.clickFindTroop(point2);
                                if ((item != -2L) && (!this.attackerSetupMode || (item < -2L)))
                                {
                                    if (this.m_lassoMade && GameEngine.shiftPressed)
                                    {
                                        if (!this.m_lassoElements.Contains(item))
                                        {
                                            this.m_lassoElements.Add(item);
                                            this.lassoMade();
                                        }
                                    }
                                    else
                                    {
                                        this.troopSelectDoubleClickTIme = DateTime.Now;
                                        this.clearLasso();
                                        this.m_lassoMade = true;
                                        this.m_lassoElements.Add(item);
                                        this.lassoMade();
                                    }
                                    this.recalcCastleLayout();
                                }
                            }
                        }
                    }
                    else if ((this.getMapTileFromMousePos(mousePos, ref mapX, ref mapY) && (this.castleLayout != null)) && (this.castleLayout.map[mapX, mapY] != 0))
                    {
                        long elementID = this.castleLayout.elemMap[mapX, mapY];
                        if (!CreateMode && !this.inBuilderMode)
                        {
                            switch (this.castleLayout.map[mapX, mapY])
                            {
                                case 0x33:
                                case 0x34:
                                case 0x35:
                                case 0x36:
                                case 0x37:
                                case 0x38:
                                    return;

                                case 0x39:
                                    return;
                            }
                            if (elementID >= 0L)
                            {
                                if (this.inDeleting)
                                {
                                    TimeSpan span2 = (TimeSpan) (DateTime.Now - this.lastDeleteTime);
                                    if (span2.TotalSeconds > 8.0)
                                    {
                                        this.inDeleting = false;
                                    }
                                }
                                if (!this.inDeleting)
                                {
                                    GameEngine.Instance.playInterfaceSound("CastleMap_delete");
                                    CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, InterfaceMgr.Instance.ParentForm);
                                    this.inDeleting = true;
                                    this.lastDeleteTime = DateTime.Now;
                                    RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
                                    RemoteServices.Instance.DeleteCastleElement(this.m_villageID, elementID);
                                    foreach (CastleElement element3 in this.elements)
                                    {
                                        if (element3.elementID == elementID)
                                        {
                                            this.elements.Remove(element3);
                                            CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                                            this.recalcCastleLayout();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else if ((elementID < -1L) || CreateMode)
                        {
                            foreach (CastleElement element4 in this.elements)
                            {
                                if ((element4 != null) && (element4.elementID == elementID))
                                {
                                    this.elements.Remove(element4);
                                    CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                                    this.recalcCastleLayout();
                                    InterfaceMgr.Instance.castleStartBuilderMode();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool mouseDrag(Point mousePos, bool viewOnly)
        {
            double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
            if (!this.m_leftMouseHeldDown)
            {
                this.m_lastMousePressedTime = currentMilliseconds;
                this.m_leftMouseHeldDown = true;
                this.m_baseMousePos = mousePos;
                this.m_baseScreenX = backgroundSprite.PosX;
                this.m_baseScreenY = backgroundSprite.PosY;
                this.m_leftMouseGrabbed = false;
                if (!viewOnly && !this.inBuilderMode)
                {
                    this.m_holdLassoModeAvailable = true;
                }
            }
            bool flag = false;
            if ((Math.Abs((int) (this.m_baseMousePos.X - mousePos.X)) > 3) || (Math.Abs((int) (this.m_baseMousePos.Y - mousePos.Y)) > 3))
            {
                flag = true;
            }
            if (((currentMilliseconds - this.m_lastMousePressedTime) > 250.0) || flag)
            {
                this.m_leftMouseGrabbed = true;
                int num2 = this.m_baseMousePos.X - mousePos.X;
                int num3 = this.m_baseMousePos.Y - mousePos.Y;
                backgroundSprite.PosX = ((float) this.m_baseScreenX) - num2;
                backgroundSprite.PosY = ((float) this.m_baseScreenY) - num3;
                this.moveMap(0, 0);
            }
            return flag;
        }

        private void mouseMoveCatapultTarget(int mapX, int mapY)
        {
            this.catapultTargetMoveX = mapX;
            this.catapultTargetMoveY = mapY;
            this.catapultTargetMoveValid = false;
            foreach (CastleElement element in this.elements)
            {
                if (element.elementID == this.selectedCatapult)
                {
                    this.catapultTargetMoveValid = CatapultTarget.validateCatapultRange(element, mapX, mapY, GameEngine.Instance.LocalWorldData.Castle_Catapult_MaxRange);
                    break;
                }
            }
            this.recalcCastleLayout();
        }

        public bool mouseMovePlaceTroops(int mapX, int mapY, bool placing, int spriteIndex)
        {
            SpriteWrapper dummySprite = null;
            SpriteWrapper wrapper2 = null;
            if ((spriteIndex >= 0) && (spriteIndex < placementTroopSprite.Length))
            {
                dummySprite = placementTroopSprite[spriteIndex];
                wrapper2 = placementTroopCastleSprite[spriteIndex];
            }
            else
            {
                dummySprite = this.dummySprite;
                wrapper2 = this.dummySprite;
            }
            if (dummySprite == null)
            {
                goto Label_0668;
            }
            int num = ((mapX * 0x10) + (mapY * 0x10)) - 0x39a;
            int num2 = ((mapY * 8) - (mapX * 8)) + 0x1da;
            if (((num < 0) || (num2 < 0)) || ((num >= 0x770) || (num2 >= 0x3b8)))
            {
                dummySprite.Visible = false;
                goto Label_0668;
            }
            int num3 = 0;
            if (displayCollapsed)
            {
                num3 = this.castleLayout.collapsedHeightMap[mapX, mapY];
            }
            else
            {
                num3 = this.castleLayout.fullHeightMap[mapX, mapY];
            }
            dummySprite.Visible = true;
            dummySprite.PosX = num + 0x10;
            dummySprite.PosY = (num2 + 8) - num3;
            if (placementType == 0x4b)
            {
                dummySprite.Center = new PointF(18f, 28f);
            }
            else if (placementType == 0x5e)
            {
                dummySprite.Center = new PointF(93f, 100f);
            }
            else
            {
                dummySprite.Center = new PointF(50f, 66f);
            }
            CastleElement element = this.castleLayout.getCastleElement(mapX, mapY);
            if (((num3 > 0) && (element != null)) && ((element.elementType < 1) || (element.elementType > 10)))
            {
                wrapper2.Visible = true;
            }
            else
            {
                wrapper2.Visible = false;
            }
            if (!placingDefender)
            {
                int num10 = 0;
                if (mapX < mapY)
                {
                    if ((0x75 - mapX) < mapY)
                    {
                        num10 = 0;
                    }
                    else
                    {
                        num10 = 2;
                    }
                }
                else if ((0x75 - mapX) < mapY)
                {
                    num10 = 6;
                }
                else
                {
                    num10 = 4;
                }
                dummySprite.SpriteNo = (num10 + 6) & 7;
                bool flag2 = true;
                if (this.placingAttackerRealMode && placing)
                {
                    switch (placementType)
                    {
                        case 90:
                            if (!this.m_usingCastleTroopsOK)
                            {
                                if (this.attackNumPeasants >= this.attackMaxPeasants)
                                {
                                    flag2 = false;
                                }
                                break;
                            }
                            if (this.attackNumPeasants >= (this.attackMaxPeasants + this.attackMaxPeasantsInCastle))
                            {
                                flag2 = false;
                            }
                            break;

                        case 0x5b:
                            if (!this.m_usingCastleTroopsOK)
                            {
                                if (this.attackNumSwordsmen >= this.attackMaxSwordsmen)
                                {
                                    flag2 = false;
                                }
                                break;
                            }
                            if (this.attackNumSwordsmen >= (this.attackMaxSwordsmen + this.attackMaxSwordsmenInCastle))
                            {
                                flag2 = false;
                            }
                            break;

                        case 0x5c:
                            if (!this.m_usingCastleTroopsOK)
                            {
                                if (this.attackNumArchers >= this.attackMaxArchers)
                                {
                                    flag2 = false;
                                }
                                break;
                            }
                            if (this.attackNumArchers >= (this.attackMaxArchers + this.attackMaxArchersInCastle))
                            {
                                flag2 = false;
                            }
                            break;

                        case 0x5d:
                            if (!this.m_usingCastleTroopsOK)
                            {
                                if (this.attackNumPikemen >= this.attackMaxPikemen)
                                {
                                    flag2 = false;
                                }
                                break;
                            }
                            if (this.attackNumPikemen >= (this.attackMaxPikemen + this.attackMaxPikemenInCastle))
                            {
                                flag2 = false;
                            }
                            break;

                        case 0x5e:
                            if (this.attackNumCatapults >= this.attackMaxCatapults)
                            {
                                flag2 = false;
                            }
                            break;

                        case 100:
                        case 0x65:
                        case 0x66:
                        case 0x67:
                        case 0x68:
                        case 0x69:
                        case 0x6a:
                        case 0x6b:
                            if (this.attackNumCaptains >= this.attackMaxCaptains)
                            {
                                flag2 = false;
                            }
                            break;
                    }
                    if (!flag2)
                    {
                        this.stopPlacementOnTroopModeSwap = true;
                    }
                }
                if (this.castleLayout.map[mapX, mapY] != 0)
                {
                    flag2 = false;
                }
                if (!flag2)
                {
                    dummySprite.ColorToUse = Color.FromArgb(0x80, ARGBColors.Blue);
                }
                else if (!this.castleLayout.canPlaceAttackerHere(placementType, mapX, mapY, this.attackerSetupForest))
                {
                    if (this.attackerSetupForest && (mapY < 0x21))
                    {
                        dummySprite.ColorToUse = Color.FromArgb(0x80, ARGBColors.Blue);
                    }
                    else
                    {
                        dummySprite.ColorToUse = Color.FromArgb(0x80, ARGBColors.Red);
                    }
                }
                else
                {
                    dummySprite.ColorToUse = ARGBColors.White;
                    return true;
                }
                goto Label_0668;
            }
            bool flag = true;
            if (placing)
            {
                switch (placementType)
                {
                    case 70:
                        if (placingReinforcement)
                        {
                            if (this.numPlacedReinforceDefenderPeasants >= (this.numAvailableReinforceDefenderPeasants + this.numAvailableVassalReinforceDefenderPeasants))
                            {
                                flag = false;
                            }
                            break;
                        }
                        if (this.numAvailableDefenderPeasants == 0)
                        {
                            flag = false;
                        }
                        break;

                    case 0x47:
                        if (placingReinforcement)
                        {
                            if (this.numPlacedReinforceDefenderSwordsmen >= (this.numAvailableReinforceDefenderSwordsmen + this.numAvailableVassalReinforceDefenderSwordsmen))
                            {
                                flag = false;
                            }
                            break;
                        }
                        if (this.numAvailableDefenderSwordsmen == 0)
                        {
                            flag = false;
                        }
                        break;

                    case 0x48:
                        if (placingReinforcement)
                        {
                            if (this.numPlacedReinforceDefenderArchers >= (this.numAvailableReinforceDefenderArchers + this.numAvailableVassalReinforceDefenderArchers))
                            {
                                flag = false;
                            }
                            break;
                        }
                        if (this.numAvailableDefenderArchers == 0)
                        {
                            flag = false;
                        }
                        break;

                    case 0x49:
                        if (placingReinforcement)
                        {
                            if (this.numPlacedReinforceDefenderPikemen >= (this.numAvailableReinforceDefenderPikemen + this.numAvailableVassalReinforceDefenderPikemen))
                            {
                                flag = false;
                            }
                            break;
                        }
                        if (this.numAvailableDefenderPikemen == 0)
                        {
                            flag = false;
                        }
                        break;

                    case 0x55:
                        if (!placingReinforcement)
                        {
                            if (this.numAvailableDefenderCaptains == 0)
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            flag = false;
                        }
                        break;

                    case 0x4b:
                        if (this.numPots >= this.numSmelterPlaces)
                        {
                            flag = false;
                        }
                        goto Label_0317;
                }
                if (((((((((((((this.numPlacedDefenderPeasants + this.numPlacedDefenderArchers) + this.numPlacedDefenderPikemen) + this.numPlacedDefenderSwordsmen) + this.numPlacedDefenderCaptains) + this.numPlacedReinforceDefenderPeasants) + this.numPlacedReinforceDefenderArchers) + this.numPlacedReinforceDefenderPikemen) + this.numPlacedReinforceDefenderSwordsmen) + this.numPlacedVassalReinforceDefenderPeasants) + this.numPlacedVassalReinforceDefenderArchers) + this.numPlacedVassalReinforceDefenderPikemen) + this.numPlacedVassalReinforceDefenderSwordsmen) >= this.numGuardHouseSpaces)
                {
                    flag = false;
                }
                if (CreateMode)
                {
                    flag = true;
                }
            }
        Label_0317:
            if (!flag)
            {
                dummySprite.ColorToUse = Color.FromArgb(0x80, ARGBColors.Blue);
            }
            else if (!this.castleLayout.canPlaceDefenderHere(placementType, mapX, mapY))
            {
                dummySprite.ColorToUse = Color.FromArgb(0x80, ARGBColors.Red);
            }
            else if ((placementType == 0x4b) && placing)
            {
                int woodCost = 0;
                int stoneCost = 0;
                int goldCost = 0;
                int oilCost = 0;
                int ironCost = 0;
                CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, placementType, ref woodCost, ref stoneCost, ref goldCost, ref oilCost, ref ironCost);
                VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.getStockpileLevels(levels);
                }
                int goldLevel = 0;
                this.adjustLevels(ref levels, ref goldLevel);
                if (oilCost <= levels.pitchLevel)
                {
                    dummySprite.ColorToUse = ARGBColors.White;
                    return true;
                }
                dummySprite.ColorToUse = Color.FromArgb(0x80, ARGBColors.Red);
            }
            else
            {
                dummySprite.ColorToUse = ARGBColors.White;
                return true;
            }
        Label_0668:
            return false;
        }

        public void mouseMoveUpdate(Point mousePos, bool leftDown)
        {
            bool viewOnly = false;
            if (!GameEngine.Instance.World.isUserVillage(this.m_villageID) && !this.attackerSetupMode)
            {
                viewOnly = true;
            }
            if (backgroundSprite != null)
            {
                if (!GameEngine.Instance.World.isCapital(this.m_villageID))
                {
                    InterfaceMgr.Instance.mouseMoveDXCardBar(mousePos);
                }
                if (!this.attackerSetupMode)
                {
                    if (((mousePos.X > (this.gfx.ViewportWidth - 0x20)) && (mousePos.Y < (32f + wikiHelpSprite.PosY))) && (mousePos.Y > wikiHelpSprite.PosY))
                    {
                        this.overWikiHelp = true;
                        CustomTooltipManager.MouseEnterTooltipArea(0x1130, 2);
                    }
                    else
                    {
                        this.overWikiHelp = false;
                    }
                }
                if (!this.troopMovingMode || !this.updateTroopMove(mousePos, leftDown))
                {
                    if ((this.gfx.keyControlled || this.m_lassoLeftHeldDown) && ((!this.battleMode && !viewOnly) && !this.inBuilderMode))
                    {
                        if (leftDown)
                        {
                            if (!this.m_lassoLeftHeldDown)
                            {
                                this.m_lassoLeftHeldDown = true;
                                this.m_lassoLastX = this.m_lassoEndX = this.m_lassoStartX = mousePos.X;
                                this.m_lassoLastY = this.m_lassoEndY = this.m_lassoStartY = mousePos.Y;
                                this.castleLayout.createSparseArray();
                                this.updateLasso(false);
                                this.recalcCastleLayout();
                            }
                            else
                            {
                                this.m_lassoEndX = mousePos.X;
                                this.m_lassoEndY = mousePos.Y;
                                this.updateLasso(false);
                            }
                        }
                        else if (this.m_lassoLeftHeldDown)
                        {
                            this.m_lassoLeftHeldDown = false;
                            this.m_lassoMade = true;
                            this.m_lassoEndX = mousePos.X;
                            this.m_lassoEndY = mousePos.Y;
                            this.updateLasso(false);
                            if (this.m_lassoElements.Count > 0)
                            {
                                if (!this.inTroopPlacerMode)
                                {
                                    this.startTroopPlacerMode();
                                }
                                this.lassoMade();
                                this.recalcCastleLayout();
                            }
                            else
                            {
                                this.clearLasso();
                            }
                        }
                    }
                    else
                    {
                        this.m_lassoLeftHeldDown = false;
                        if (((leftDown && (placementType != 0x22)) && ((placementType != 0x21) && (placementType != 0x24))) && (((placementType != 0x23) && (placementType != 0x41)) && (placementType != 0x42)))
                        {
                            double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
                            bool flag2 = this.mouseDrag(mousePos, viewOnly);
                            if (this.m_holdLassoModeAvailable && flag2)
                            {
                                this.m_holdLassoModeAvailable = false;
                            }
                            if (this.m_holdLassoModeAvailable && ((currentMilliseconds - this.m_lastMousePressedTime) > 250.0))
                            {
                                CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
                                this.m_lassoLeftHeldDown = true;
                                this.m_lassoLastX = this.m_lassoEndX = this.m_lassoStartX = mousePos.X;
                                this.m_lassoLastY = this.m_lassoEndY = this.m_lassoStartY = mousePos.Y;
                                this.clearLasso();
                                this.castleLayout.createSparseArray();
                                this.updateLasso(false);
                                return;
                            }
                        }
                        if (!this.m_lassoMade)
                        {
                            int mapX = -1;
                            int mapY = -1;
                            if (this.getMapTileFromMousePos(mousePos, ref mapX, ref mapY))
                            {
                                Builder_MapX = mapX;
                                Builder_MapY = mapY;
                                if (this.deleting)
                                {
                                    long deletingHighlightElementID = this.deletingHighlightElementID;
                                    this.deletingHighlightElementID = this.castleLayout.getCastleElementID(mapX, mapY);
                                    int num5 = this.castleLayout.map[mapX, mapY];
                                    if (((num5 >= 1) && (num5 <= 10)) || ((num5 >= 0x33) && (num5 <= 0x39)))
                                    {
                                        this.deletingHighlightElementID = -2L;
                                    }
                                    if (deletingHighlightElementID != this.deletingHighlightElementID)
                                    {
                                        this.recalcCastleLayout();
                                    }
                                    if (!this.inDeleting)
                                    {
                                        CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
                                    }
                                    else
                                    {
                                        TimeSpan span = (TimeSpan) (DateTime.Now - this.lastDeleteTime);
                                        if (span.TotalSeconds > 8.0)
                                        {
                                            CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
                                        }
                                        else
                                        {
                                            CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, InterfaceMgr.Instance.ParentForm);
                                        }
                                    }
                                }
                                else if (this.deletingTroops || this.troopMovingMode)
                                {
                                    Point point = mousePos;
                                    point.X += -((int) backgroundSprite.DrawPos.X);
                                    point.Y += -((int) backgroundSprite.DrawPos.Y);
                                    long num6 = this.deletingHighlightElementID;
                                    if (placingDefender)
                                    {
                                        long num7 = this.clickFindTroop(point);
                                        if (num7 != -2L)
                                        {
                                            this.deletingHighlightElementID = num7;
                                        }
                                        else
                                        {
                                            this.deletingHighlightElementID = -2L;
                                        }
                                    }
                                    if (num6 != this.deletingHighlightElementID)
                                    {
                                        this.recalcCastleLayout();
                                    }
                                    CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
                                }
                                else if (this.selectedCatapult != -1L)
                                {
                                    this.mouseMoveCatapultTarget(mapX, mapY);
                                }
                                else if (!placingElement)
                                {
                                    this.troopsFollowMouse(mapX, mapY);
                                }
                                else
                                {
                                    this.moveConstruction(mousePos, leftDown);
                                    if (this.battleMode)
                                    {
                                        this.battleModeMousePos = mousePos;
                                        this.battleModeMousePos.X += -((int) backgroundSprite.DrawPos.X);
                                        this.battleModeMousePos.Y += -((int) backgroundSprite.DrawPos.Y);
                                    }
                                }
                            }
                            else if (this.battleMode)
                            {
                                this.battleModeMousePos = new Point(-1000, -1000);
                            }
                        }
                    }
                }
            }
        }

        public void mouseNotClicked(Point mousePos)
        {
            if (this.m_leftMouseHeldDown)
            {
                if (!this.m_leftMouseGrabbed)
                {
                    if (GameEngine.Instance.World.isUserVillage(this.m_villageID) || this.attackerSetupMode)
                    {
                        if (!this.commonMouseClicked(mousePos))
                        {
                            this.mouseClicked(mousePos);
                        }
                    }
                    else
                    {
                        this.commonMouseClicked(mousePos);
                    }
                }
                this.m_leftMouseHeldDown = false;
                this.m_leftMouseGrabbed = false;
                if (!this.troopMovingMode)
                {
                    CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
                }
                else
                {
                    CursorManager.SetCursor(CursorManager.CursorType.VSplit, InterfaceMgr.Instance.ParentForm);
                }
            }
        }

        public void mouseWheel()
        {
            if (placingElement)
            {
                bool flag = false;
                if (placementType == 40)
                {
                    flag = true;
                    placementType = 0x27;
                }
                else if (placementType == 0x27)
                {
                    flag = true;
                    placementType = 40;
                }
                if (placementType == 0x26)
                {
                    flag = true;
                    placementType = 0x25;
                }
                else if (placementType == 0x25)
                {
                    flag = true;
                    placementType = 0x26;
                }
                if (flag)
                {
                    this.startPlaceElement(placementType);
                }
            }
        }

        public void moveConstruction(Point mousePos, bool leftDown)
        {
            int mapX = -1;
            int mapY = -1;
            this.getMapTileFromMousePos(mousePos, ref mapX, ref mapY);
            if ((((placementType == 0x22) || (placementType == 0x21)) || ((placementType == 0x41) || (placementType == 0x42))) || ((placementType == 0x24) || (placementType == 0x23)))
            {
                if (((mapX != this.lastMoveTileX) || (mapY != this.lastMoveTileY)) || !this.draggingWall)
                {
                    this.lastMoveTileX = mapX;
                    this.lastMoveTileY = mapY;
                    this.wallMouseMove(mapX, mapY, leftDown);
                }
            }
            else if ((mapX != this.lastMoveTileX) || (mapY != this.lastMoveTileY))
            {
                this.lastMoveTileX = mapX;
                this.lastMoveTileY = mapY;
                this.movePlaceElement(mapX, mapY, placementSprite, false, true);
            }
        }

        public void moveLassoTroops(Point mousePos)
        {
            int mapX = 0;
            int mapY = 0;
            if (this.getMapTileFromMousePos(mousePos, ref mapX, ref mapY) && (this.m_lassoElements.Count > 0))
            {
                if (this.attackerSetupMode)
                {
                    placingDefender = false;
                    bool flag = false;
                    if (((mapX >= 0x21) && (mapX < 0x55)) && ((mapY >= 0x21) && (mapY < 0x55)))
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        CastleElement element = this.castleLayout.getElementFromElemID(this.m_lassoElements[0]);
                        if ((element != null) && (((element.elementType == 0x5e) || (element.elementType == 0x66)) || (element.elementType == 0x67)))
                        {
                            for (int k = 0; k < this.m_lassoElements.Count; k++)
                            {
                                element = this.castleLayout.getElementFromElemID(this.m_lassoElements[k]);
                                if (element != null)
                                {
                                    foreach (CatapultTarget target in this.catapultTargets)
                                    {
                                        if (element.elementID == target.elemID)
                                        {
                                            target.xPos = (byte) mapX;
                                            target.yPos = (byte) mapY;
                                            target.validate(element, GameEngine.Instance.LocalWorldData.Castle_Catapult_MaxRange);
                                            if (!target.valid)
                                            {
                                                target.createDefaultLocation(element.xPos, element.yPos, element);
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            this.recalcCastleLayout();
                        }
                        return;
                    }
                }
                else
                {
                    placingDefender = true;
                }
                List<Point> list = new List<Point>();
                for (int i = 0; i < this.m_lassoElements.Count; i++)
                {
                    CastleElement element2 = this.castleLayout.getElementFromElemID(this.m_lassoElements[i]);
                    if (element2 != null)
                    {
                        list.Add(new Point(element2.xPos, element2.yPos));
                        element2.xPos = 1;
                        element2.yPos = 1;
                    }
                    else
                    {
                        list.Add(new Point(0, 0));
                    }
                }
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                int num5 = 0;
                while (num5 < this.m_lassoElements.Count)
                {
                    bool flag2 = false;
                    int num6 = 1;
                    int num7 = 1;
                    for (int m = 1; m < 0xec; m += 2)
                    {
                        if (m > 1)
                        {
                            num6 = (m * m) - ((m - 2) * (m - 2));
                            num7 = num6 / 4;
                        }
                        for (int n = 0; n < num6; n++)
                        {
                            int num10 = n;
                            int num11 = 0;
                            int num12 = 0;
                            if (m == 1)
                            {
                                num11 = mapX;
                                num12 = mapY;
                            }
                            if (m > 1)
                            {
                                num10 += (m - 1) / 2;
                                num10 = num10 % num6;
                                int num13 = num10 / num7;
                                int num14 = num10 % num7;
                                switch (num13)
                                {
                                    case 0:
                                        num11 = (mapX - ((m - 1) / 2)) + num14;
                                        num12 = mapY - ((m - 1) / 2);
                                        break;

                                    case 1:
                                        num11 = mapX + ((m - 1) / 2);
                                        num12 = (mapY - ((m - 1) / 2)) + num14;
                                        break;

                                    case 2:
                                        num11 = (mapX + ((m - 1) / 2)) - num14;
                                        num12 = mapY + ((m - 1) / 2);
                                        break;

                                    case 3:
                                        num11 = mapX - ((m - 1) / 2);
                                        num12 = (mapY + ((m - 1) / 2)) - num14;
                                        break;
                                }
                            }
                            Point point = list[num5];
                            this.currentMoveOriginalX = point.X;
                            this.currentMoveOriginalY = point.Y;
                            if (this.lassoMoveTroop(this.m_lassoElements[num5], num11, num12))
                            {
                                flag2 = true;
                                num5++;
                                if (num5 >= this.m_lassoElements.Count)
                                {
                                    m = 0x2710;
                                    break;
                                }
                            }
                        }
                    }
                    if (!flag2)
                    {
                        num5++;
                    }
                }
                for (int j = 0; j < this.m_lassoElements.Count; j++)
                {
                    CastleElement element3 = this.castleLayout.getElementFromElemID(this.m_lassoElements[j]);
                    if (((element3 != null) && (element3.xPos == 1)) && (element3.yPos == 1))
                    {
                        Point point2 = list[j];
                        element3.xPos = (byte) point2.X;
                        Point point3 = list[j];
                        element3.yPos = (byte) point3.Y;
                    }
                }
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
            }
            this.recalcCastleLayout();
        }

        public void moveMap(int dx, int dy)
        {
            if (backgroundSprite != null)
            {
                backgroundSprite.move(dx, dy);
                backgroundSprite.keepBounded();
                backgroundSprite.centreSmallerSprite();
                backgroundSprite.fixup2DPos();
            }
        }

        public bool movePlaceElement(int mapX, int mapY, SpriteWrapper sprite, bool forceInvalid, bool checkEnclosed)
        {
            if ((sprite != null) && (backgroundSprite != null))
            {
                int num = ((mapX * 0x10) + (mapY * 0x10)) - 0x39a;
                int num2 = ((mapY * 8) - (mapX * 8)) + 0x1da;
                if (((num >= 0) && (num2 >= 0)) && ((num < 0x770) && (num2 < 0x3b8)))
                {
                    sprite.Visible = true;
                    sprite.PosX = num + 0x10;
                    sprite.PosY = num2 + 8;
                    CastleElement element = new CastleElement {
                        elementType = (byte) placementType,
                        xPos = (byte) mapX,
                        yPos = (byte) mapY
                    };
                    long repairedElementID = -1L;
                    int woodCost = 0;
                    int stoneCost = 0;
                    int goldCost = 0;
                    int oilCost = 0;
                    int ironCost = 0;
                    if (!forceInvalid && CastlesCommon.validatePlacement(element))
                    {
                        if (((placementType == 0x2b) && this.attackerSetupForest) && (mapY < 0x21))
                        {
                            forceInvalid = true;
                        }
                        else if (!CreateMode)
                        {
                            CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, placementType, ref woodCost, ref stoneCost, ref goldCost, ref oilCost, ref ironCost);
                            VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
                            if (GameEngine.Instance.Village != null)
                            {
                                GameEngine.Instance.Village.getStockpileLevels(levels);
                            }
                            int goldLevel = 0;
                            if (!GameEngine.Instance.World.isCapital(this.m_villageID))
                            {
                                goldLevel = (int) GameEngine.Instance.World.getCurrentGold();
                            }
                            else if (GameEngine.Instance.Village != null)
                            {
                                goldLevel = (int) GameEngine.Instance.Village.m_capitalGold;
                            }
                            this.adjustLevels(ref levels, ref goldLevel);
                            if (((((woodCost <= 0) || (woodCost > levels.woodLevel)) && ((stoneCost <= 0) || (stoneCost > levels.stoneLevel))) && (((goldCost <= 0) || (goldCost > goldLevel)) && ((ironCost <= 0) || (ironCost > levels.ironLevel)))) && (((woodCost != 0) || (stoneCost != 0)) || (((goldCost != 0) || (ironCost != 0)) || (oilCost != 0))))
                            {
                                forceInvalid = true;
                            }
                        }
                    }
                    if (forceInvalid)
                    {
                        sprite.ColorToUse = Color.FromArgb(0x80, ARGBColors.Red);
                        return false;
                    }
                    if (!CastlesCommon.validatePlacement(element))
                    {
                        sprite.ColorToUse = Color.FromArgb(0x80, ARGBColors.White);
                        return false;
                    }
                    if ((this.castleLayout != null) && !this.castleLayout.testElement(element, ref repairedElementID))
                    {
                        sprite.ColorToUse = Color.FromArgb(0x80, ARGBColors.Red);
                        return false;
                    }
                    if (((this.castleLayout != null) && checkEnclosed) && this.castleLayout.isCastleEnclosed(element, null))
                    {
                        sprite.ColorToUse = Color.FromArgb(0x80, ARGBColors.Blue);
                        return false;
                    }
                    sprite.ColorToUse = ARGBColors.White;
                    if ((((placementType == 40) || (placementType == 0x27)) || ((placementType == 0x26) | (placementType == 0x25))) && ((mapX != this.lastGHX) || (mapY != this.lastGHY)))
                    {
                        this.lastGHX = mapX;
                        this.lastGHY = mapY;
                        bool flag = true;
                        if (mapX < mapY)
                        {
                            if ((0x75 - mapX) < mapY)
                            {
                                flag = true;
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                        else if ((0x75 - mapX) < mapY)
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                        if (flag)
                        {
                            if (placementType == 40)
                            {
                                this.startPlaceElement(0x27);
                            }
                            if (placementType == 0x26)
                            {
                                this.startPlaceElement(0x25);
                            }
                        }
                        else
                        {
                            if (placementType == 0x27)
                            {
                                this.startPlaceElement(40);
                            }
                            if (placementType == 0x25)
                            {
                                this.startPlaceElement(0x26);
                            }
                        }
                    }
                    return true;
                }
                sprite.Visible = false;
            }
            return false;
        }

        public void moveTroopLocal(CastleElement element, int mapX, int mapY)
        {
            if ((element.elementID >= 0L) && !this.movedElements.Contains(element))
            {
                CastleElement item = new CastleElement {
                    elementID = element.elementID,
                    xPos = (byte) this.currentMoveOriginalX,
                    yPos = (byte) this.currentMoveOriginalY
                };
                this.movedElementsOriginal.Add(item);
                this.movedElements.Add(element);
            }
        }

        public void newElementCallback(AddCastleElement_ReturnType returnData)
        {
            InterfaceMgr.Instance.castleCommitReturn();
            if (this.commitPopup != null)
            {
                this.commitPopup.Close();
                this.commitPopup = null;
            }
            if (returnData.villageID == this.m_villageID)
            {
                if (returnData.list)
                {
                    this.newElementListCallback(returnData);
                    this.manageTutorial();
                }
                else
                {
                    if (returnData.element == null)
                    {
                        this.waitingForWallReturn = false;
                        this.clearPlacementWallSprites();
                    }
                    if (returnData.clientElementNumber < 0L)
                    {
                        foreach (CastleElement element in this.elements)
                        {
                            if (element.elementID == returnData.clientElementNumber)
                            {
                                this.elements.Remove(element);
                                break;
                            }
                        }
                    }
                    bool flag = true;
                    if (returnData.Success)
                    {
                        GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                        setServerTime(returnData.currentTime);
                        if (returnData.element != null)
                        {
                            if (returnData.element.elementType == 0x2b)
                            {
                                List<CastleElement> list = new List<CastleElement>();
                                foreach (CastleElement element2 in this.elements)
                                {
                                    if (element2.elementType == 0x2b)
                                    {
                                        list.Add(element2);
                                    }
                                }
                                foreach (CastleElement element3 in list)
                                {
                                    this.elements.Remove(element3);
                                }
                            }
                            this.elements.Add(returnData.element);
                        }
                        if (returnData.elements != null)
                        {
                            this.importElements(returnData.elements);
                            flag = false;
                        }
                        if ((returnData.villageResourcesAndStats != null) && (GameEngine.Instance.Village != null))
                        {
                            GameEngine.Instance.Village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                            VillageMap village = GameEngine.Instance.Village;
                            if (village != null)
                            {
                                this.numAvailableDefenderPeasants = 0;
                                this.numAvailableDefenderArchers = 0;
                                this.numAvailableDefenderPikemen = 0;
                                this.numAvailableDefenderSwordsmen = 0;
                                this.numAvailableDefenderCaptains = 0;
                                village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
                                GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
                                village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
                            }
                        }
                    }
                    else
                    {
                        foreach (CastleElement element4 in this.elements)
                        {
                            if (element4.elementID == returnData.clientElementNumber)
                            {
                                this.elements.Remove(element4);
                                break;
                            }
                        }
                        VillageMap map2 = GameEngine.Instance.Village;
                        if (map2 != null)
                        {
                            switch (returnData.elementType)
                            {
                                case 70:
                                    map2.addTroops(1, 0, 0, 0, 0);
                                    break;

                                case 0x47:
                                    map2.addTroops(0, 0, 0, 1, 0);
                                    break;

                                case 0x48:
                                    map2.addTroops(0, 1, 0, 0, 0);
                                    break;

                                case 0x49:
                                    map2.addTroops(0, 0, 1, 0, 0);
                                    break;

                                case 0x55:
                                    map2.addTroops(0, 0, 0, 0, 0, 0, 1);
                                    break;
                            }
                            this.numAvailableDefenderPeasants = 0;
                            this.numAvailableDefenderArchers = 0;
                            this.numAvailableDefenderPikemen = 0;
                            this.numAvailableDefenderSwordsmen = 0;
                            this.numAvailableDefenderCaptains = 0;
                            map2.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
                            GameEngine.Instance.World.getReinforceTotals(map2.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
                            map2.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
                        }
                    }
                    if (flag)
                    {
                        CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                        this.recalcCastleLayout();
                    }
                    InterfaceMgr.Instance.refreshCastleInterface();
                    this.manageTutorial();
                }
            }
        }

        public void newElementListCallback(AddCastleElement_ReturnType returnData)
        {
            if ((returnData.villageResourcesAndStats != null) && (GameEngine.Instance.Village != null))
            {
                GameEngine.Instance.Village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                VillageMap village = GameEngine.Instance.Village;
                if (village != null)
                {
                    this.numAvailableDefenderPeasants = 0;
                    this.numAvailableDefenderArchers = 0;
                    this.numAvailableDefenderPikemen = 0;
                    this.numAvailableDefenderSwordsmen = 0;
                    this.numAvailableDefenderCaptains = 0;
                    village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
                    GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
                    village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
                }
            }
            if (returnData.Success)
            {
                if (returnData.elements != null)
                {
                    this.inBuilderMode = false;
                    this.inTroopPlacerMode = false;
                    this.importElements(returnData.elements);
                    InterfaceMgr.Instance.castleEndBuilderMode();
                }
            }
            else
            {
                MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("CastleMap_Placement_Error", "Castle Placement Error"));
            }
            GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
            setServerTime(returnData.currentTime);
        }

        public void pauseBattle()
        {
            Sound.pauseEnvironmental(true);
            this.castleCombat.pause(true);
        }

        public CastleElement placeBuildingElement(int mapX, int mapY)
        {
            return this.placeBuildingElement(mapX, mapY, false);
        }

        public CastleElement placeBuildingElement(int mapX, int mapY, bool fastMode)
        {
            int num4;
            int num5;
            if (!this.movePlaceElement(mapX, mapY, placementSprite, false, true))
            {
                return null;
            }
            if (!this.inBuilderMode)
            {
                this.startBuilderMode();
            }
            if (placementType == 0x2b)
            {
                foreach (CastleElement element in this.elements)
                {
                    if ((element.elementID < -1L) && (element.elementType == 0x2b))
                    {
                        this.elements.Remove(element);
                        break;
                    }
                }
            }
            CastleElement element2 = new CastleElement {
                elementID = this.localTempElementNumber
            };
            this.localTempElementNumber -= 1L;
            element2.elementType = (byte) placementType;
            element2.xPos = (byte) mapX;
            element2.yPos = (byte) mapY;
            bool flag = false;
            switch (placementType)
            {
                case 11:
                case 12:
                case 13:
                case 14:
                case 0x15:
                case 0x20:
                case 0x25:
                case 0x26:
                case 0x27:
                case 40:
                    flag = true;
                    goto Label_0212;

                case 0x1f:
                    flag = true;
                    num4 = this.countGuardHouses() + 1;
                    num5 = 400 / GameEngine.Instance.LocalWorldData.castle_troopsPerGuardHouse;
                    if (GameEngine.Instance.World.isCapital(this.m_villageID))
                    {
                        num5 -= 5;
                        break;
                    }
                    num5 -= 2;
                    break;

                case 0x29:
                {
                    flag = true;
                    int num2 = this.countTurrets() + 1;
                    if (num2 >= GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Turrets)
                    {
                        this.stopPlaceElement();
                    }
                    goto Label_0212;
                }
                case 0x2a:
                {
                    flag = true;
                    int num3 = this.countBallistas() + 1;
                    if (num3 >= GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Ballista)
                    {
                        this.stopPlaceElement();
                    }
                    goto Label_0212;
                }
                case 0x2c:
                {
                    int num = this.countBombards() + 1;
                    if (num >= GameEngine.Instance.Village.m_parishCapitalResearchData.Research_Leadership)
                    {
                        this.stopPlaceElement();
                    }
                    goto Label_0212;
                }
                default:
                    goto Label_0212;
            }
            if (num4 >= num5)
            {
                this.stopPlaceElement();
            }
        Label_0212:
            if (flag)
            {
                foreach (long num6 in this.castleLayout.getUnderlyingWallElements(element2))
                {
                    foreach (CastleElement element3 in this.elements)
                    {
                        if (element3.elementID == num6)
                        {
                            this.elements.Remove(element3);
                            if (element3.elementID >= 0L)
                            {
                                this.removedElements.Add(element3);
                            }
                            break;
                        }
                    }
                }
            }
            GameEngine.Instance.playInterfaceSound("CastleMap_place_building");
            this.elements.Add(element2);
            if (!fastMode)
            {
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                this.recalcCastleLayout();
                InterfaceMgr.Instance.castleStartBuilderMode();
            }
            return element2;
        }

        private void playRandSFX(string[] tags)
        {
            if (tags != null)
            {
                GameEngine.Instance.playInterfaceSound(tags[this.sfxRandom.Next(tags.Length)]);
            }
        }

        private void playRandSFXNoOverwrite(string[] tags)
        {
            if (tags != null)
            {
                GameEngine.Instance.playInterfaceSound(tags[this.sfxRandom.Next(tags.Length)], false);
            }
        }

        public void pressConstructionConfirm()
        {
            UniversalDebugLog.Log("begin pressConstructionConfirm");
            if (((placementType == 0x22) || (placementType == 0x21)) || ((placementType == 0x41) || (placementType == 0x42)))
            {
                this.wallMouseMove(this.lastMoveTileX, this.lastMoveTileY, false);
            }
            else if ((placementType == 0x24) || (placementType == 0x23))
            {
                this.wallMouseMove(this.lastMoveTileX, this.lastMoveTileY, false);
            }
            else if (placementType > 0x45)
            {
                this.confirmTroopPlacement(this.lastMoveTileX, this.lastMoveTileY);
            }
            else
            {
                this.placeBuildingElement(this.lastMoveTileX, this.lastMoveTileY);
            }
        }

        private void putTroopPlacerInCenterOfScreen()
        {
            Point point = new Point(this.gfx.ViewportWidth, this.gfx.ViewportHeight);
            Point mousePos = new Point(point.X / 2, point.Y / 2);
            Point point3 = new Point(mousePos.X, mousePos.Y);
            int mapX = -1;
            int mapY = -1;
            this.getMapTileFromMousePos(point3, ref mapX, ref mapY);
            this.mouseMovePlaceTroops(mapX, mapY, false, 0);
            this.updateTroopMove(mousePos, true);
        }

        public void recalcCastleInit()
        {
            if (this.attackerSetupForest)
            {
                if (backgroundSprite.TextureID == GFXLibrary.Instance.CastleBackgroundTexID)
                {
                    backgroundSprite.Initialize(this.gfx, GFXLibrary.Instance.FreeCardIconsID, 0x1d);
                }
            }
            else if (backgroundSprite.TextureID != GFXLibrary.Instance.CastleBackgroundTexID)
            {
                backgroundSprite.Initialize(this.gfx, GFXLibrary.Instance.CastleBackgroundTexID, 0);
            }
            backgroundSprite.RemoveAllChildrenFast();
            for (int i = 0; i < 0x76; i++)
            {
                for (int k = 0; k < 0x76; k++)
                {
                    if (castleSpriteGrid[i, k] != null)
                    {
                        castleSpriteGrid[i, k].Visible = false;
                    }
                    SpriteWrapper wrapper = castleDefenderSpriteGrid[i, k];
                    if ((wrapper != null) && wrapper.Visible)
                    {
                        wrapper.RemoveSelfFromParent();
                        wrapper.Visible = false;
                    }
                    SpriteWrapper wrapper2 = castleAttackerSpriteGrid[i, k];
                    if ((wrapper2 != null) && wrapper2.Visible)
                    {
                        wrapper2.RemoveSelfFromParent();
                        wrapper2.Visible = false;
                    }
                }
            }
            foreach (SpriteWrapper wrapper3 in castleExtraSprites)
            {
                if (wrapper3.Visible)
                {
                    wrapper3.RemoveSelfFromParent();
                    wrapper3.Visible = false;
                }
            }
            if (placementSprite != null)
            {
                backgroundSprite.AddChild(placementSprite, 10);
            }
            if (placementSprite_handleone != null)
            {
                backgroundSprite.AddChild(placementSprite_handleone, 10);
            }
            if (placementSprite_handletwo != null)
            {
                backgroundSprite.AddChild(placementSprite_handletwo, 10);
            }
            for (int j = 0; j < 0x19; j++)
            {
                if (placementTroopSprite[j] != null)
                {
                    backgroundSprite.AddChild(placementTroopSprite[j], 10);
                    if (placementTroopCastleSprite[j] != null)
                    {
                        placementTroopSprite[j].AddChild(placementTroopCastleSprite[j], 1);
                    }
                }
            }
            foreach (SpriteWrapper wrapper4 in placementWallSprites)
            {
                backgroundSprite.AddChild(wrapper4, 10);
            }
        }

        public void recalcCastleLayout()
        {
            activeCastleInfrastructureElements.Clear();
            this.recalcCastleInit();
            this.nextExtraSpriteID = 0;
            numClickAreas = 0;
            double num = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
            DateTime curTime = baseServerTime.AddSeconds(num);
            this.numGuardHouses = 0;
            this.numPlacedDefenderArchers = 0;
            this.numPlacedDefenderPeasants = 0;
            this.numPlacedDefenderSwordsmen = 0;
            this.numPlacedDefenderPikemen = 0;
            this.numPlacedDefenderCaptains = 0;
            this.numPlacedReinforceDefenderArchers = 0;
            this.numPlacedReinforceDefenderPeasants = 0;
            this.numPlacedReinforceDefenderSwordsmen = 0;
            this.numPlacedReinforceDefenderPikemen = 0;
            this.numPlacedVassalReinforceDefenderArchers = 0;
            this.numPlacedVassalReinforceDefenderPeasants = 0;
            this.numPlacedVassalReinforceDefenderSwordsmen = 0;
            this.numPlacedVassalReinforceDefenderPikemen = 0;
            this.attackNumPeasants = 0;
            this.attackNumArchers = 0;
            this.attackNumPikemen = 0;
            this.attackNumSwordsmen = 0;
            this.attackNumCatapults = 0;
            this.attackNumCaptains = 0;
            this.numPots = 0;
            this.castleDamaged = false;
            bool displayCollapsed = CastleMap.displayCollapsed;
            bool completed = true;
            DateTime completeTime = curTime;
            if (this.elements != null)
            {
                if (((this.debugDisplayMode == 1) || (this.debugDisplayMode == 2)) || (this.debugDisplayMode == 3))
                {
                    for (int i = 0; i < 0x76; i++)
                    {
                        for (int j = 0; j < 0x76; j++)
                        {
                            SpriteWrapper child = castleSpriteGrid[j, i];
                            if (child != null)
                            {
                                PointF tf2 = new PointF(16f, 0f);
                                float num4 = 8f;
                                int spriteTagOfset = -1;
                                if (this.debugDisplayMode == 1)
                                {
                                    int num6 = this.castleCombat.getAttackerRouteMap(j, i);
                                    if (num6 >= 0)
                                    {
                                        spriteTagOfset = 0x13b + (num6 % 0x40);
                                    }
                                }
                                else if (this.debugDisplayMode == 2)
                                {
                                    int num7 = this.castleCombat.getObstacleMap(j, i);
                                    if (num7 > 0)
                                    {
                                        spriteTagOfset = (0x13b + num7) - 1;
                                    }
                                }
                                else if ((this.debugDisplayMode == 3) && this.castleCombat.getPillageLeaveTargetMap(j, i))
                                {
                                    spriteTagOfset = 0x13b;
                                }
                                if (spriteTagOfset >= 0)
                                {
                                    Rectangle rectangle;
                                    PointF tf;
                                    SizeF ef;
                                    child.Visible = true;
                                    child.ColorToUse = ARGBColors.White;
                                    int spriteTag = 1;
                                    this.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref spriteTag).GetSpriteXYdata(spriteTag, spriteTagOfset, out rectangle, out tf, out ef);
                                    tf2.Y = ((int) ef.Height) - num4;
                                    child.SpriteNo = spriteTagOfset;
                                    child.Center = tf2;
                                }
                                backgroundSprite.AddChild(child, 2);
                            }
                        }
                    }
                }
                if ((!this.attackerSetupMode && !this.battleMode) && (this.InBuilderMode || (placementType != -1)))
                {
                    for (int k = 0x37; k < 0x3f; k++)
                    {
                        this.addNoBuildTile(k, 0x37);
                        this.addNoBuildTile(k, 0x3e);
                    }
                    for (int m = 0x38; m < 0x3e; m++)
                    {
                        this.addNoBuildTile(0x37, m);
                        this.addNoBuildTile(0x3e, m);
                    }
                }
                this.drawCastleLoop(displayCollapsed, ref completed, ref completeTime, curTime);
                if (this.battleMode)
                {
                    this.doFireList();
                }
                this.drawTroops();
                if (this.castleCombat != null)
                {
                    this.drawDyingTroops();
                    this.drawArrows();
                    this.drawRocks();
                }
                if (this.attackerSetupMode)
                {
                    this.clearCatapultLines();
                    foreach (CatapultTarget target in this.catapultTargets)
                    {
                        if (((this.selectedCatapult == target.elemID) || this.showCatapultTargets) || this.m_lassoElements.Contains(target.elemID))
                        {
                            SpriteWrapper wrapper2 = this.getNextExtraSprite(GFXLibrary.Instance.CastleSpritesTexID, 0x17b);
                            PointF tf3 = new PointF(96f, 46f);
                            wrapper2.Center = tf3;
                            Point point = (castleUnitSpritePoint[target.xPos, target.yPos]);
                            wrapper2.PosX = point.X;
                            wrapper2.PosY = point.Y;
                            if (!target.valid)
                            {
                                wrapper2.ColorToUse = Color.FromArgb(0x80, ARGBColors.Red);
                            }
                            backgroundSprite.AddChild(wrapper2, 10);
                            if (target.catapult != null)
                            {
                                this.addCatapultTargetLine(target.catapult.xPos, target.catapult.yPos, target.xPos, target.yPos);
                            }
                        }
                    }
                    if (this.selectedCatapult != -1L)
                    {
                        bool flag3 = true;
                        if (this.selectedCatapult != -1L)
                        {
                            foreach (CastleElement element in this.elements)
                            {
                                if (element.elementID == this.selectedCatapult)
                                {
                                    flag3 = CatapultTarget.validateCatapultSelect(element, this.catapultTargetMoveX, this.catapultTargetMoveY);
                                    break;
                                }
                            }
                        }
                        if (flag3)
                        {
                            SpriteWrapper wrapper3 = this.getNextExtraSprite(GFXLibrary.Instance.CastleSpritesTexID, 0x17b);
                            PointF tf4 = new PointF(96f, 46f);
                            wrapper3.Center = tf4;
                            Point point2 = (castleUnitSpritePoint[this.catapultTargetMoveX, this.catapultTargetMoveY]);
                            wrapper3.PosX = point2.X;
                            wrapper3.PosY = point2.Y;
                            if (!this.catapultTargetMoveValid)
                            {
                                wrapper3.ColorToUse = Color.FromArgb(0x80, ARGBColors.Red);
                            }
                            backgroundSprite.AddChild(wrapper3, 10);
                        }
                    }
                }
                backgroundSprite.Update();
            }
            if (!this.attackerSetupMode && !this.battleMode)
            {
                if (!GameEngine.Instance.World.isCapital(this.m_villageID))
                {
                    this.numGuardHouseSpaces = (this.numGuardHouses + 2) * GameEngine.Instance.LocalWorldData.castle_troopsPerGuardHouse;
                    this.numGuardHouseSpaces = CardTypes.adjustGuardHouseSpace(GameEngine.Instance.World.UserCardData, this.numGuardHouseSpaces);
                    bool castleEnclosed = false;
                    if (this.inBuilderMode)
                    {
                        castleEnclosed = this.castleLayout.isCastleEnclosedGateHouseBlocking();
                    }
                    else
                    {
                        VillageMap village = GameEngine.Instance.Village;
                        if (village != null)
                        {
                            castleEnclosed = village.m_castleEnclosed;
                        }
                        else if (this.castleLayout != null)
                        {
                            castleEnclosed = this.castleLayout.isCastleEnclosedGateHouseBlocking();
                        }
                    }
                    if (castleEnclosed)
                    {
                        enclosedOverlaySprite.Initialize(this.gfx, GFXLibrary.Instance.CastleSpritesTexID, 0x1d1);
                    }
                    else
                    {
                        enclosedOverlaySprite.Initialize(this.gfx, GFXLibrary.Instance.CastleSpritesTexID, 0x1d2);
                        enclosedOverlaySprite2.Initialize(this.gfx, GFXLibrary.Instance.CastleSpritesTexID, 0x1d3);
                    }
                    this.m_castleEnclosed = castleEnclosed;
                }
                else
                {
                    this.numGuardHouseSpaces = (this.numGuardHouses + 5) * GameEngine.Instance.LocalWorldData.castle_troopsPerGuardHouse;
                }
                this.numSmelterPlaces = this.numSmelter * GameEngine.Instance.LocalWorldData.castle_oilPerSmelter;
                InterfaceMgr.Instance.setCastleStats(this.numGuardHouseSpaces, this.numPlacedDefenderArchers, this.numPlacedDefenderPeasants, this.numPlacedDefenderPikemen, this.numPlacedDefenderSwordsmen, completeTime, completed, this.numAvailableDefenderPeasants, this.numAvailableDefenderArchers, this.numAvailableDefenderPikemen, this.numAvailableDefenderSwordsmen, this.numPots, this.numSmelterPlaces, this.castleDamaged, this.numPlacedReinforceDefenderArchers, this.numPlacedReinforceDefenderPeasants, this.numPlacedReinforceDefenderPikemen, this.numPlacedReinforceDefenderSwordsmen, this.numAvailableReinforceDefenderPeasants, this.numAvailableReinforceDefenderArchers, this.numAvailableReinforceDefenderPikemen, this.numAvailableReinforceDefenderSwordsmen, this.numAvailableVassalReinforceDefenderPeasants, this.numAvailableVassalReinforceDefenderArchers, this.numAvailableVassalReinforceDefenderPikemen, this.numAvailableVassalReinforceDefenderSwordsmen, this.numPlacedVassalReinforceDefenderArchers, this.numPlacedVassalReinforceDefenderPeasants, this.numPlacedVassalReinforceDefenderPikemen, this.numPlacedVassalReinforceDefenderSwordsmen, this.numPlacedDefenderCaptains, this.numAvailableDefenderCaptains);
                if ((!this.attackerSetupMode && !this.battleMode) && (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE))
                {
                    if (!completed)
                    {
                        Sound.playVillageEnvironmental(0x12);
                    }
                    else
                    {
                        Sound.playVillageEnvironmental(0x11);
                    }
                }
            }
            if (this.attackerSetupMode)
            {
                InterfaceMgr.Instance.castleShowPlacedAttackers(this.attackNumPeasants, this.attackNumArchers, this.attackNumPikemen, this.attackNumSwordsmen, this.attackNumCatapults, this.attackMaxPeasants, this.attackMaxArchers, this.attackMaxPikemen, this.attackMaxSwordsmen, this.attackMaxCatapults, this.attackNumCaptains, this.attackMaxCaptains, this.attackCaptainsCommand, this.attackMaxPeasantsInCastle, this.attackMaxArchersInCastle, this.attackMaxPikemenInCastle, this.attackMaxSwordsmenInCastle);
                this.updateLaunchButton();
            }
        }

        public void regenerateDefaultCatapultTargets()
        {
            this.catapultTargets.Clear();
            foreach (CastleElement element in this.elements)
            {
                if (((element.elementType == 0x5e) || (element.elementType == 0x66)) || (element.elementType == 0x67))
                {
                    CatapultTarget item = new CatapultTarget {
                        elemID = element.elementID
                    };
                    item.createDefaultLocation(element.xPos, element.yPos, element);
                    this.catapultTargets.Add(item);
                }
            }
        }

        public void reInitGFX()
        {
            this.recalcCastleLayout();
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                this.numAvailableDefenderPeasants = 0;
                this.numAvailableDefenderArchers = 0;
                this.numAvailableDefenderPikemen = 0;
                this.numAvailableDefenderSwordsmen = 0;
                this.numAvailableDefenderCaptains = 0;
                village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
                GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
                village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
            }
            this.createSurroundSprites();
            this.cancelBuilderMode();
        }

        public bool renameAttackSetup(string oldName, string newName)
        {
            string path = this.getAttackSetupSaveName(oldName);
            string destFileName = this.getAttackSetupSaveName(newName);
            if (File.Exists(path))
            {
                File.Move(path, destFileName);
                return true;
            }
            return false;
        }

        public bool restoreAttackSetup(int ID)
        {
            List<RestoreCastleElement> list = new List<RestoreCastleElement>();
            try
            {
                FileStream input = new FileStream(this.getAttackSetupSaveName(ID), FileMode.Open);
                BinaryReader reader = new BinaryReader(input);
                int num = reader.ReadInt32();
                for (int i = 0; i < num; i++)
                {
                    RestoreCastleElement item = new RestoreCastleElement {
                        xPos = reader.ReadByte(),
                        yPos = reader.ReadByte(),
                        elementType = reader.ReadByte()
                    };
                    if (item.elementType == 0x5e)
                    {
                        item.targXPos = reader.ReadByte();
                        item.targYPos = reader.ReadByte();
                    }
                    if ((item.elementType >= 100) && (item.elementType < 0x6d))
                    {
                        item.delay = reader.ReadByte();
                        if ((item.elementType == 0x66) || (item.elementType == 0x67))
                        {
                            item.targXPos = reader.ReadByte();
                            item.targYPos = reader.ReadByte();
                        }
                    }
                    list.Add(item);
                }
                reader.Close();
                input.Close();
            }
            catch (Exception)
            {
                return false;
            }
            GameEngine.Instance.stopInterfaceSounds = true;
            int num3 = 0;
            int pieceType = -1;
            foreach (RestoreCastleElement element2 in list)
            {
                int elementType = element2.elementType;
                if ((element2.elementType >= 100) && (element2.elementType < 0x6d))
                {
                    element2.elementType = 100;
                }
                this.startPlacingAttackerTroops(element2.elementType);
                this.setPlacementSize(1);
                if (this.mouseMovePlaceTroops(element2.xPos, element2.yPos, true, 0))
                {
                    CastleElement element = this.troopPlaceAttacker(element2.xPos, element2.yPos);
                    pieceType = element2.elementType;
                    num3++;
                    if (element2.elementType == 0x5e)
                    {
                        foreach (CatapultTarget target in this.catapultTargets)
                        {
                            if (target.elemID == element.elementID)
                            {
                                target.xPos = element2.targXPos;
                                target.yPos = element2.targYPos;
                                break;
                            }
                        }
                    }
                    if ((element2.elementType >= 100) && (element2.elementType < 0x6d))
                    {
                        foreach (CaptainsDetails details in this.captainsDetails)
                        {
                            if (details.elemID == element.elementID)
                            {
                                details.seconds = element2.delay;
                                break;
                            }
                        }
                        if (elementType != 100)
                        {
                            element.elementType = (byte) elementType;
                        }
                        switch (elementType)
                        {
                            case 0x66:
                            case 0x67:
                                this.addNewCatapultTargetDefault(element);
                                foreach (CatapultTarget target2 in this.catapultTargets)
                                {
                                    if (target2.elemID == element.elementID)
                                    {
                                        target2.xPos = element2.targXPos;
                                        target2.yPos = element2.targYPos;
                                        break;
                                    }
                                }
                                break;
                        }
                    }
                    CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                }
            }
            if (pieceType >= 0)
            {
                this.startPlaceElement_ShowPanel(pieceType, CastlesCommon.getPieceName(pieceType), false);
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                this.recalcCastleLayout();
                InterfaceMgr.Instance.castleStartBuilderMode();
            }
            this.stopPlaceElement();
            GameEngine.Instance.stopInterfaceSounds = false;
            return true;
        }

        public bool restoreAttackSetup(string name)
        {
            List<RestoreCastleElement> list = new List<RestoreCastleElement>();
            try
            {
                FileStream input = new FileStream(this.getAttackSetupSaveName(name), FileMode.Open);
                BinaryReader reader = new BinaryReader(input);
                int num = reader.ReadInt32();
                for (int i = 0; i < num; i++)
                {
                    RestoreCastleElement item = new RestoreCastleElement {
                        xPos = reader.ReadByte(),
                        yPos = reader.ReadByte(),
                        elementType = reader.ReadByte()
                    };
                    if (item.elementType == 0x5e)
                    {
                        item.targXPos = reader.ReadByte();
                        item.targYPos = reader.ReadByte();
                    }
                    if ((item.elementType >= 100) && (item.elementType < 0x6d))
                    {
                        item.delay = reader.ReadByte();
                        if ((item.elementType == 0x66) || (item.elementType == 0x67))
                        {
                            item.targXPos = reader.ReadByte();
                            item.targYPos = reader.ReadByte();
                        }
                        bool flag = false;
                        int num3 = GameEngine.Instance.World.UserResearchData.Research_Tactics;
                        switch (item.elementType)
                        {
                            case 100:
                                flag = true;
                                break;

                            case 0x65:
                                if (num3 > 0)
                                {
                                    flag = true;
                                }
                                break;

                            case 0x66:
                                if (num3 > 1)
                                {
                                    flag = true;
                                }
                                break;

                            case 0x67:
                                if (num3 > 3)
                                {
                                    flag = true;
                                }
                                break;

                            case 0x68:
                                if (num3 > 2)
                                {
                                    flag = true;
                                }
                                break;
                        }
                        if (!flag)
                        {
                            continue;
                        }
                    }
                    list.Add(item);
                }
                reader.Close();
                input.Close();
            }
            catch (Exception)
            {
                return false;
            }
            GameEngine.Instance.stopInterfaceSounds = true;
            int num4 = 0;
            int pieceType = -1;
            foreach (RestoreCastleElement element2 in list)
            {
                int elementType = element2.elementType;
                if ((element2.elementType >= 100) && (element2.elementType < 0x6d))
                {
                    element2.elementType = 100;
                }
                this.startPlacingAttackerTroops(element2.elementType);
                this.setPlacementSize(1);
                if (this.mouseMovePlaceTroops(element2.xPos, element2.yPos, true, 0))
                {
                    CastleElement element = this.troopPlaceAttacker(element2.xPos, element2.yPos);
                    pieceType = element2.elementType;
                    num4++;
                    if (element2.elementType == 0x5e)
                    {
                        foreach (CatapultTarget target in this.catapultTargets)
                        {
                            if (target.elemID == element.elementID)
                            {
                                target.xPos = element2.targXPos;
                                target.yPos = element2.targYPos;
                                break;
                            }
                        }
                    }
                    if ((element2.elementType >= 100) && (element2.elementType < 0x6d))
                    {
                        foreach (CaptainsDetails details in this.captainsDetails)
                        {
                            if (details.elemID == element.elementID)
                            {
                                details.seconds = element2.delay;
                                break;
                            }
                        }
                        if (elementType != 100)
                        {
                            element.elementType = (byte) elementType;
                        }
                        switch (elementType)
                        {
                            case 0x66:
                            case 0x67:
                                this.addNewCatapultTargetDefault(element);
                                foreach (CatapultTarget target2 in this.catapultTargets)
                                {
                                    if (target2.elemID == element.elementID)
                                    {
                                        target2.xPos = element2.targXPos;
                                        target2.yPos = element2.targYPos;
                                        break;
                                    }
                                }
                                break;
                        }
                    }
                    CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                }
            }
            if (pieceType >= 0)
            {
                this.startPlaceElement_ShowPanel(pieceType, CastlesCommon.getPieceName(pieceType), false);
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                this.recalcCastleLayout();
                InterfaceMgr.Instance.castleStartBuilderMode();
            }
            this.stopPlaceElement();
            GameEngine.Instance.stopInterfaceSounds = false;
            return true;
        }

        public void restoreCastleTroopsCallback(RestoreCastleTroops_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.elements != null)
                {
                    this.importElements(returnData.elements);
                }
                if ((returnData.villageResourcesAndStats != null) && (GameEngine.Instance.Village != null))
                {
                    GameEngine.Instance.Village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    VillageMap village = GameEngine.Instance.Village;
                    if (village != null)
                    {
                        this.numAvailableDefenderPeasants = 0;
                        this.numAvailableDefenderArchers = 0;
                        this.numAvailableDefenderPikemen = 0;
                        this.numAvailableDefenderSwordsmen = 0;
                        this.numAvailableDefenderCaptains = 0;
                        village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
                        GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
                        village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
                    }
                }
            }
        }

        public int restoreInfrastructure()
        {
            List<CampCastleElement> list = new List<CampCastleElement>();
            try
            {
                FileStream input = new FileStream(this.getInfrastructureSaveName(), FileMode.Open);
                BinaryReader reader = new BinaryReader(input);
                int num = reader.ReadInt32();
                for (int i = 0; i < num; i++)
                {
                    CampCastleElement item = new CampCastleElement {
                        xPos = reader.ReadByte(),
                        yPos = reader.ReadByte(),
                        elementType = reader.ReadByte(),
                        aggressiveDefender = reader.ReadBoolean()
                    };
                    if ((item.elementType < 0x45) && (item.elementType != 0x2b))
                    {
                        list.Add(item);
                    }
                }
                reader.Close();
                input.Close();
            }
            catch (Exception)
            {
                return -1;
            }
            GameEngine.Instance.stopInterfaceSounds = true;
            int num3 = 0;
            int pieceType = -1;
            foreach (CampCastleElement element2 in list)
            {
                if (this.startPlaceElement(element2.elementType) && (this.placeBuildingElement(element2.xPos, element2.yPos, true) != null))
                {
                    num3++;
                }
            }
            if (num3 > 0)
            {
                GameEngine.Instance.Castle.startPlaceElement_ShowPanel(pieceType, CastlesCommon.getPieceName(pieceType), false);
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                this.recalcCastleLayout();
                InterfaceMgr.Instance.castleStartBuilderMode();
            }
            this.stopPlaceElement();
            GameEngine.Instance.stopInterfaceSounds = false;
            return num3;
        }

        public int restoreTroops()
        {
            List<CampCastleElementLL> list = new List<CampCastleElementLL>();
            try
            {
                FileStream input = new FileStream(this.getTroopsSaveName(), FileMode.Open);
                BinaryReader reader = new BinaryReader(input);
                int num = reader.ReadInt32();
                if (num >= 0)
                {
                    for (int i = 0; i < num; i++)
                    {
                        CampCastleElementLL item = new CampCastleElementLL {
                            xPos = reader.ReadByte(),
                            yPos = reader.ReadByte(),
                            elementType = reader.ReadByte(),
                            aggressiveDefender = reader.ReadBoolean()
                        };
                        if (item.elementType > 0x45)
                        {
                            list.Add(item);
                        }
                    }
                }
                else
                {
                    num = reader.ReadInt32();
                    for (int j = 0; j < num; j++)
                    {
                        CampCastleElementLL tll2 = new CampCastleElementLL {
                            xPos = reader.ReadByte(),
                            yPos = reader.ReadByte(),
                            elementType = reader.ReadByte(),
                            aggressiveDefender = reader.ReadBoolean(),
                            reinforcement = reader.ReadBoolean()
                        };
                        if (tll2.elementType > 0x45)
                        {
                            list.Add(tll2);
                        }
                    }
                }
                reader.Close();
                input.Close();
            }
            catch (Exception)
            {
                return -1;
            }
            GameEngine.Instance.stopInterfaceSounds = true;
            int num4 = 0;
            int pieceType = -1;
            foreach (CampCastleElementLL tll3 in list)
            {
                this.startPlacingTroops(tll3.elementType, tll3.reinforcement);
                this.setPlacementSize(1);
                if (this.mouseMovePlaceTroops(tll3.xPos, tll3.yPos, true, 0))
                {
                    CastleElement element = this.troopPlaceDefender(tll3.xPos, tll3.yPos, true);
                    if (element != null)
                    {
                        element.aggressiveDefender = tll3.aggressiveDefender;
                    }
                    pieceType = tll3.elementType;
                    num4++;
                }
            }
            if (pieceType >= 0)
            {
                this.startPlaceElement_ShowPanel(pieceType, CastlesCommon.getPieceName(pieceType), false);
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                this.recalcCastleLayout();
                InterfaceMgr.Instance.castleStartBuilderMode();
            }
            this.stopPlaceElement();
            GameEngine.Instance.stopInterfaceSounds = false;
            return num4;
        }

        public void retrieveArmyFromGarrison()
        {
            List<CastleElement> list = new List<CastleElement>();
            foreach (CastleElement element in this.elements)
            {
                if ((element.elementType >= 70) && (element.elementType <= 0x47))
                {
                    list.Add(element);
                }
            }
            foreach (CastleElement element2 in list)
            {
                this.elements.Remove(element2);
            }
            CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
            this.recalcCastleLayout();
        }

        public void returnToReports()
        {
        }

        public void rightClick(Point mousePos)
        {
            if (this.m_lassoMade && (this.m_lassoElements.Count > 0))
            {
                this.moveLassoTroops(mousePos);
            }
            else
            {
                this.stopPlaceElement();
            }
        }

        public void rockChipCallback(RockMissile rock)
        {
            int num = this.chipRand.Next(12, 0x12);
            for (int i = 0; i < num; i++)
            {
                this.addRockChips(rock.targX, rock.targY, rock.bombard);
            }
            Point point = (castleUnitSpritePoint[rock.targX, rock.targY]);
            this.addRockSmoke((float) point.X, (float) point.Y, rock.bombard);
        }

        private void runCastleSounds()
        {
            if (!this.castleCombat.isBattlePaused())
            {
                int tickValue = this.castleCombat.TickValue;
                if ((tickValue % 30) == 1)
                {
                    int num2 = this.castleCombat.sfxGetTotalPeople();
                    int villageType = -1;
                    if (num2 < 20)
                    {
                        villageType = 0x2a;
                    }
                    else if (num2 < 150)
                    {
                        villageType = 0x2b;
                    }
                    else if (num2 < 400)
                    {
                        villageType = 0x2c;
                    }
                    else
                    {
                        villageType = 0x2d;
                    }
                    int num4 = Sound.getCurrentEnvironmental();
                    if ((villageType != num4) && !Sound.isFading())
                    {
                        if ((num4 >= 0x2a) && (num4 <= 0x2d))
                        {
                            Sound.fadeOutCurrentPlaying();
                        }
                        else
                        {
                            Sound.fadeInVillageEnvironmental(villageType);
                        }
                    }
                }
                if ((tickValue % 300) == 5)
                {
                    GameEngine.Instance.AudioEngine.unloadUnplayingSounds();
                }
                int multiplier = 1;
                if (this.fastPlayback)
                {
                    multiplier = 3;
                }
                this.castleCombat.processSoundTrackingQueue(90, 90, 90, 150, 240, 30, 90, 30, 30, 90, 30, 30);
                this.arrowSounds.playBattleSounds(tickValue, this.castleCombat.sfxGetNumArrows(), 30, multiplier, 3, this.arrow_low_sounds, 20, this.arrow_mid_sounds, this.arrow_high_sounds, this);
                this.meleeLightSounds.playBattleSounds(tickValue, this.castleCombat.sfxGetNumMeleeLight(), 30, multiplier, 3, this.meleeLight_low_sounds, 20, this.meleeLight_mid_sounds, this.meleeLight_high_sounds, this);
                this.meleeMetalSounds.playBattleSounds(tickValue, this.castleCombat.sfxGetNumMeleeMetal(), 30, multiplier, 3, this.meleeMetal_low_sounds, 20, this.meleeMetal_mid_sounds, this.meleeMetal_high_sounds, this);
                this.infraWoodSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumInfraWood(), 30, multiplier, 3, this.infraWood_low_sounds, 20, this.infraWood_mid_sounds, this.infraWood_high_sounds, this);
                this.infraStoneSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumInfraStone(), 30, multiplier, 3, this.infraStone_low_sounds, 20, this.infraStone_mid_sounds, this.infraStone_high_sounds, this);
                this.oilSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumOilPots(), 30, multiplier, 2, this.oil_low_sounds, 0x2710, this.oil_mid_sounds, this.oil_mid_sounds, this);
                this.ballistaBoltSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumBallistaBolts(), 0x2d, multiplier, 3, this.ballista_low_sounds, 10, this.ballista_mid_sounds, this.ballista_high_sounds, this);
                this.troopDeathSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumTroopDeaths(), 30, multiplier, 3, this.troopdeath_low_sounds, 10, this.troopdeath_mid_sounds, this.troopdeath_high_sounds, this);
                this.troopDeathOnFireSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumTroopDeathsOnFire(), 90, multiplier, 2, this.troopdeathonfire_low_sounds, 8, this.troopdeathonfire_low_sounds, this.troopdeathonfire_low_sounds, this);
                this.infraWoodDestroyedSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumWoodDestroyed(), 30, multiplier, 3, this.wooddestroyed_low_sounds, 15, this.wooddestroyed_mid_sounds, this.wooddestroyed_high_sounds, this);
                this.infraStoneSmallDestroyedSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumStoneSmallDestroyed(), 30, multiplier, 3, this.stonesmalldestroyed_low_sounds, 15, this.stonesmalldestroyed_mid_sounds, this.stonesmalldestroyed_high_sounds, this);
                this.infraStoneLargeDestroyedSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumStoneLargeDestroyed(), 30, multiplier, 2, this.stonelargedestroyed_low_sounds, 10, this.stonelargedestroyed_mid_sounds, this.stonelargedestroyed_high_sounds, this);
                this.rockFirstSounds.playBattleSounds(tickValue, this.castleCombat.sfxGetNumRocksFired(), 30, multiplier, 3, this.rockfired_low_sounds, 15, this.rockfired_mid_sounds, this.rockfired_high_sounds, this);
                this.rockLandSounds.playBattleSounds(tickValue, this.castleCombat.sfxGetNumRocksLand(), 30, multiplier, 3, this.rockland_low_sounds, 15, this.rockland_mid_sounds, this.rockland_high_sounds, this);
                this.rockHitSounds.playBattleSounds(tickValue, this.castleCombat.sfxGetNumRocksHit(), 30, multiplier, 3, this.rockhit_low_sounds, 15, this.rockhit_mid_sounds, this.rockhit_high_sounds, this);
                this.openPitsSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumPitsOpen(), 30, multiplier, 2, this.openpits_low_sounds, 8, this.openpits_mid_sounds, this.openpits_high_sounds, this);
                this.horseDeathSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumHorseDeaths(), 100, multiplier, 0x186a0, this.horsedeath_low_sounds, 10, this.horsedeath_low_sounds, this.horsedeath_low_sounds, this);
                this.wolfDeathSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumWolfDeaths(), 80, multiplier, 0x186a0, this.wolfdeath_low_sounds, 10, this.wolfdeath_low_sounds, this.wolfdeath_low_sounds, this);
                this.catapultDeathSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumCatapultsDeaths(), 80, multiplier, 0x186a0, this.catapultdeath_low_sounds, 10, this.catapultdeath_low_sounds, this.catapultdeath_low_sounds, this);
                if (this.m_nextWolfSound < tickValue)
                {
                    this.m_nextWolfSound = (tickValue + 30) + this.sfxRandom.Next(60);
                    int num6 = this.castleCombat.sfxGetNumWolves();
                    if (num6 > 0)
                    {
                        if (num6 < 3)
                        {
                            this.playRandSFXNoOverwrite(this.wolves_low_sounds);
                        }
                        else if (num6 < 15)
                        {
                            this.playRandSFXNoOverwrite(this.wolves_mid_sounds);
                        }
                        else
                        {
                            this.playRandSFXNoOverwrite(this.wolves_high_sounds);
                        }
                    }
                }
                if (this.m_nextKnightSound < tickValue)
                {
                    this.m_nextKnightSound = (tickValue + 30) + this.sfxRandom.Next(30);
                    int num7 = this.castleCombat.sfxGetNumKnights();
                    if (num7 > 0)
                    {
                        if (num7 < 3)
                        {
                            this.playRandSFXNoOverwrite(this.knight_low_sounds);
                        }
                        else if (num7 < 10)
                        {
                            this.playRandSFXNoOverwrite(this.knight_mid_sounds);
                        }
                        else
                        {
                            this.playRandSFXNoOverwrite(this.knight_high_sounds);
                        }
                    }
                }
                if (this.m_nextCaptainDelaySound < tickValue)
                {
                    this.m_nextCaptainDelaySound = tickValue + 30;
                    if (this.castleCombat.sfxGetNumCaptainDelay() > 0)
                    {
                        GameEngine.Instance.playInterfaceSound("captain_delay_sound", false);
                    }
                }
                if (this.m_nextCaptainRallySound < tickValue)
                {
                    this.m_nextCaptainRallySound = tickValue + 30;
                    if (this.castleCombat.sfxGetNumCaptainRallyCry() > 0)
                    {
                        GameEngine.Instance.playInterfaceSound("captain_rally_sound", false);
                    }
                }
                if (this.m_nextCaptainBattleSound < tickValue)
                {
                    this.m_nextCaptainBattleSound = tickValue + 30;
                    if (this.castleCombat.sfxGetNumCaptainBattleCry() > 0)
                    {
                        GameEngine.Instance.playInterfaceSound("captain_battle_sound", false);
                    }
                }
            }
        }

        public void saveCamp(string filename)
        {
            CampCastleElement[] elementArray = this.castleLayout.createCastleCampArray();
            FileStream output = new FileStream(filename, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(output);
            int length = elementArray.Length;
            writer.Write(length);
            foreach (CampCastleElement element in elementArray)
            {
                writer.Write(element.xPos);
                writer.Write(element.yPos);
                writer.Write(element.elementType);
                writer.Write(element.aggressiveDefender);
            }
            writer.Close();
            output.Close();
        }

        private bool selectCatapult(int mapX, int mapY)
        {
            if (this.castleLayout.attackerMap[mapX, mapY] != 0x5e)
            {
                return false;
            }
            long item = this.castleLayout.elemCatapultTroopMap[mapX, mapY];
            if (this.m_lassoMade)
            {
                if (!this.m_lassoElements.Contains(item))
                {
                    this.m_lassoElements.Add(item);
                }
            }
            else
            {
                this.clearLasso();
                this.m_lassoMade = true;
                this.m_lassoElements.Add(item);
            }
            this.recalcCastleLayout();
            return true;
        }

        public void setCampMode(int mode)
        {
            this.campMode = mode;
        }

        public void setCaptainsDetails(long elemID, int value)
        {
            foreach (CaptainsDetails details in this.captainsDetails)
            {
                if (details.elemID == elemID)
                {
                    details.seconds = (byte) value;
                }
            }
        }

        public void setFastPlayback(bool state)
        {
            this.fastPlayback = state;
        }

        public void setPlacementSize(int size)
        {
            UniversalDebugLog.Log("setting placement size");
            this.placementSize = size;
            this.createDestroyPlacementTroopSprites();
            this.troopsFollowMouse(this.lastMoveTileX, this.lastMoveTileY);
        }

        public void setRealBattleMode(bool state)
        {
            this.realBattleMode = state;
        }

        public void setReportData(GetReport_ReturnType reportReturnData)
        {
            this.m_reportReturnData = reportReturnData;
        }

        public static void setServerTime(DateTime serverTime)
        {
            baseServerTime = serverTime;
            localBaseTime = DXTimer.GetCurrentMilliseconds();
        }

        public void setTroopAggressiveMode(int troopType, bool state)
        {
            if (this.m_lassoMade)
            {
                List<long> list = new List<long>();
                foreach (long num in this.m_lassoElements)
                {
                    CastleElement element = this.castleLayout.getElementFromElemID(num);
                    if (((element != null) && (element.aggressiveDefender != state)) && (element.elementType == troopType))
                    {
                        element.aggressiveDefender = state;
                        if (!CreateMode)
                        {
                            list.Add(num);
                        }
                    }
                }
                if (list.Count > 0)
                {
                    RemoteServices.Instance.ChangeCastleElementAggressiveDefender(this.m_villageID, list.ToArray(), state);
                }
                this.lassoMade();
            }
        }

        public void setupLaunchArmy(int attackType, int pillagePercent, int captainsCommand)
        {
            this.attackRealAttackType = attackType;
            this.attackPillagePercent = pillagePercent;
            this.attackCaptainsCommand = captainsCommand;
        }

        public void setUsingCastleTroops(bool mode)
        {
            this.m_usingCastleTroopsOK = mode;
        }

        public void showTargets(bool show)
        {
            this.showCatapultTargets = show;
            this.recalcCastleLayout();
        }

        public void startAttackerTroopMove()
        {
            this.stopPlaceElement();
            this.troopMovingMode = true;
            this.troopMovingElemID = -2L;
            placingDefender = false;
        }

        public void startBuilderMode()
        {
            this.inBuilderMode = true;
            if (this.removedElements == null)
            {
                this.removedElements = new List<CastleElement>();
            }
            else
            {
                this.removedElements.Clear();
            }
            this.recalcCastleLayout();
        }

        public void startDefenderTroopMove()
        {
            this.stopPlaceElement();
            this.deletingHighlightElementID = -2L;
            placingDefender = true;
            this.troopMovingMode = true;
            this.troopMovingElemID = -2L;
        }

        public void startDeleteAttackingTroops(int troopType)
        {
            if (this.m_lassoMade)
            {
                this.lassoDelete(true, troopType);
            }
        }

        public void startDeleteTroops(int troopType)
        {
            if (this.m_lassoMade)
            {
                this.lassoDelete(false, troopType);
            }
        }

        public void startDeleting()
        {
            this.stopPlaceElement();
            this.deleting = true;
            this.deletingHighlightElementID = -2L;
            CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
        }

        public bool startPlaceElement(int elementType)
        {
            this.stopPlaceElement();
            if ((elementType == 0x29) && (this.countTurrets() >= GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Turrets))
            {
                return false;
            }
            if ((elementType == 0x2a) && (this.countBallistas() >= GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Ballista))
            {
                return false;
            }
            if ((elementType == 0x2c) && (this.countBombards() >= GameEngine.Instance.Village.m_parishCapitalResearchData.Research_Leadership))
            {
                return false;
            }
            placingElement = true;
            placementType = elementType;
            placementSprite = new SpriteWrapper();
            placementSprite.TextureID = GFXLibrary.Instance.CastleSpritesTexID;
            placementSprite.Initialize(this.gfx);
            int num4 = elementType;
            switch (num4)
            {
                case 0x41:
                    num4 = 0x22;
                    break;

                case 0x42:
                    num4 = 0x21;
                    break;
            }
            int num5 = this.initCastleSprite(placementSprite, num4, 0, 0, true, null);
            placementSprite.SpriteNo = num5;
            backgroundSprite.AddChild(placementSprite, 10);
            this.draggingWall = false;
            this.clearPlacementWallSprites();
            if (((elementType == 0x26) || (elementType == 0x25)) || ((elementType == 40) || (elementType == 0x27)))
            {
                this.startPlaceElement_ShowPanel(elementType, CastlesCommon.getPieceName(elementType), false);
            }
            this.recalcCastleLayout();
            InterfaceMgr.Instance.toggleDXCardBarActive(false);
            return true;
        }

        public void startPlaceElement_ShowPanel(int pieceType, string name, bool rollover)
        {
            if (pieceType == 0x41)
            {
                pieceType = 0x22;
            }
            if (pieceType == 0x42)
            {
                pieceType = 0x21;
            }
            int woodCost = 0;
            int stoneCost = 0;
            int ironCost = 0;
            int oilCost = 0;
            int goldCost = 0;
            CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, pieceType, ref woodCost, ref stoneCost, ref goldCost, ref oilCost, ref ironCost);
            bool isCapital = GameEngine.Instance.World.isCapital(this.m_villageID);
            CardData cardData = new CardData();
            if (!isCapital)
            {
                cardData = GameEngine.Instance.World.UserCardData;
            }
            double num6 = CastlesCommon.calcConstrTime(GameEngine.Instance.LocalWorldData, pieceType, GameEngine.Instance.World.GetResearchDataForCurrentVillage().Research_Construction, isCapital, cardData);
            InterfaceMgr.Instance.showCastlePieceInfo(name, woodCost, stoneCost, ironCost, oilCost, goldCost, VillageMap.createBuildTimeString((int) (num6 * 3600.0)), rollover);
        }

        public void startPlacingAttackerTroops(int type)
        {
            if ((type == 100) || (type == 0x55))
            {
                this.placementSize = 1;
            }
            this.stopPlaceElement();
            placingElement = false;
            placementType = type;
            placingDefender = false;
            this.createDestroyPlacementTroopSprites();
            InterfaceMgr.Instance.toggleDXCardBarActive(false);
        }

        public void startPlacingTroops(int type, bool reinforcement)
        {
            UniversalDebugLog.Log("Start placing troops type: " + type);
            if (((type == 100) || (type == 0x55)) || (type == 0x4b))
            {
                this.placementSize = 1;
            }
            this.stopPlaceElement();
            placingElement = false;
            placementType = type;
            placingDefender = true;
            placingReinforcement = reinforcement;
            this.createDestroyPlacementTroopSprites();
            InterfaceMgr.Instance.toggleDXCardBarActive(false);
        }

        public void startSetupCatapults()
        {
            this.stopPlaceElement();
        }

        public void startTroopPlacerMode()
        {
            this.inTroopPlacerMode = true;
            if (this.removedElements == null)
            {
                this.removedElements = new List<CastleElement>();
            }
            else
            {
                this.removedElements.Clear();
            }
            if (this.movedElements == null)
            {
                this.movedElements = new List<CastleElement>();
            }
            else
            {
                this.movedElements.Clear();
            }
            if (this.movedElementsOriginal == null)
            {
                this.movedElementsOriginal = new List<CastleElement>();
            }
            else
            {
                this.movedElementsOriginal.Clear();
            }
        }

        public void stopPlaceElement()
        {
            UniversalDebugLog.Log("stopPlaceElement");
            CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
            InterfaceMgr.Instance.toggleDXCardBarActive(true);
            if (placementSprite != null)
            {
                if (backgroundSprite != null)
                {
                    backgroundSprite.RemoveChild(placementSprite);
                }
                placementSprite = null;
            }
            this.clearPlacementTroopSprites();
            this.clearPlacementWallSprites();
            this.draggingWall = false;
            placingElement = true;
            placingDefender = true;
            if (this.troopMovingMode)
            {
                this.troopMovingMode = false;
                if ((this.troopMovingElemID != -2L) || (this.deletingHighlightElementID != -2L))
                {
                    this.deletingHighlightElementID = -2L;
                    this.troopMovingElemID = -2L;
                    this.recalcCastleLayout();
                }
            }
            this.troopMovingElemID = -2L;
            placementType = -1;
            if (this.selectedCatapult != -1L)
            {
                this.selectedCatapult = -1L;
                this.recalcCastleLayout();
            }
            if (this.troopSelected != -1L)
            {
                this.troopSelected = -1L;
                this.recalcCastleLayout();
                InterfaceMgr.Instance.castle_ClearSelectedTroop();
            }
            if (this.deleting || this.deletingTroops)
            {
                CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
                this.deleting = false;
                this.deletingTroops = false;
                this.deletingHighlightElementID = -2L;
                this.recalcCastleLayout();
            }
            if (this.m_lassoMade)
            {
                this.clearLasso();
            }
            InterfaceMgr.Instance.castleStopPlacing();
            this.lastMoveTileX = -1;
            this.lastMoveTileY = -1;
        }

        private bool testWallSprite(int mapX, int mapY, out CastleElement tempElement, int testType)
        {
            CastleElement element = new CastleElement();
            if (placementType == 0x42)
            {
                element.elementType = 0x21;
            }
            else if (placementType == 0x41)
            {
                element.elementType = 0x22;
            }
            else
            {
                element.elementType = (byte) placementType;
            }
            element.xPos = (byte) mapX;
            element.yPos = (byte) mapY;
            tempElement = element;
            long repairedElementID = -1L;
            if (!CastlesCommon.validatePlacement(element))
            {
                tempElement = null;
                return true;
            }
            if ((this.castleLayout != null) && !this.castleLayout.testElement(element, ref repairedElementID))
            {
                tempElement = null;
                return true;
            }
            return true;
        }

        public void toggleDisplayMode()
        {
            this.displayType++;
            if (this.displayType > 2)
            {
                this.displayType = 0;
            }
            this.recalcCastleLayout();
        }

        public void toggleHeight()
        {
            displayCollapsed = !displayCollapsed;
            this.recalcCastleLayout();
        }

        public void toggleHeight(bool high)
        {
            displayCollapsed = !high;
            this.recalcCastleLayout();
        }

        public CastleElement troopPlaceAttacker(int mapX, int mapY)
        {
            UniversalDebugLog.Log("place attacking troop");
            CastleElement item = new CastleElement {
                elementID = this.localTempElementNumber
            };
            this.localTempElementNumber -= 1L;
            item.elementType = (byte) placementType;
            item.xPos = (byte) mapX;
            item.yPos = (byte) mapY;
            GameEngine.Instance.playInterfaceSound("CastleMap_place_attacker");
            this.elements.Add(item);
            switch (placementType)
            {
                case 90:
                    this.attackNumPeasants++;
                    return item;

                case 0x5b:
                    this.attackNumSwordsmen++;
                    return item;

                case 0x5c:
                    this.attackNumArchers++;
                    return item;

                case 0x5d:
                    this.attackNumPikemen++;
                    return item;

                case 0x5e:
                    this.addNewCatapultTargetDefault(item);
                    this.attackNumCatapults++;
                    return item;

                case 0x5f:
                case 0x60:
                case 0x61:
                case 0x62:
                case 0x63:
                    return item;

                case 100:
                case 0x65:
                case 0x66:
                case 0x67:
                case 0x68:
                case 0x69:
                case 0x6a:
                case 0x6b:
                    this.addNewCaptainDetails(item);
                    this.attackNumCaptains++;
                    return item;
            }
            return item;
        }

        public CastleElement troopPlaceDefender(int mapX, int mapY)
        {
            return this.troopPlaceDefender(mapX, mapY, false);
        }

        public CastleElement troopPlaceDefender(int mapX, int mapY, bool fastMode)
        {
            UniversalDebugLog.Log("place attacking troop fastmode:" + fastMode);
            bool flag = false;
            if (!CreateMode)
            {
                if (placingReinforcement)
                {
                    switch (placementType)
                    {
                        case 70:
                            if (this.numPlacedReinforceDefenderPeasants >= this.numAvailableReinforceDefenderPeasants)
                            {
                                flag = true;
                            }
                            break;

                        case 0x47:
                            if (this.numPlacedReinforceDefenderSwordsmen >= this.numAvailableReinforceDefenderSwordsmen)
                            {
                                flag = true;
                            }
                            break;

                        case 0x48:
                            if (this.numPlacedReinforceDefenderArchers >= this.numAvailableReinforceDefenderArchers)
                            {
                                flag = true;
                            }
                            break;

                        case 0x49:
                            if (this.numPlacedReinforceDefenderPikemen >= this.numAvailableReinforceDefenderPikemen)
                            {
                                flag = true;
                            }
                            break;
                    }
                }
                if (!this.inTroopPlacerMode)
                {
                    this.startTroopPlacerMode();
                }
            }
            CastleElement item = new CastleElement {
                elementID = this.localTempElementNumber
            };
            this.localTempElementNumber -= 1L;
            item.elementType = (byte) placementType;
            if (placementType == 0x47)
            {
                item.aggressiveDefender = true;
            }
            item.xPos = (byte) mapX;
            item.yPos = (byte) mapY;
            if (!flag)
            {
                item.reinforcement = placingReinforcement;
            }
            item.vassalReinforcements = flag;
            GameEngine.Instance.playInterfaceSound("CastleMap_place_defender");
            this.elements.Add(item);
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                switch (placementType)
                {
                    case 70:
                        if (placingReinforcement)
                        {
                            if (!flag)
                            {
                                this.numPlacedReinforceDefenderPeasants++;
                            }
                            else
                            {
                                village.addVassalTroops(-1, 0, 0, 0);
                            }
                            break;
                        }
                        village.addTroops(-1, 0, 0, 0, 0);
                        break;

                    case 0x47:
                        if (placingReinforcement)
                        {
                            if (!flag)
                            {
                                this.numPlacedReinforceDefenderSwordsmen++;
                            }
                            else
                            {
                                village.addVassalTroops(0, 0, 0, -1);
                            }
                            break;
                        }
                        village.addTroops(0, 0, 0, -1, 0);
                        break;

                    case 0x48:
                        if (placingReinforcement)
                        {
                            if (!flag)
                            {
                                this.numPlacedReinforceDefenderArchers++;
                            }
                            else
                            {
                                village.addVassalTroops(0, -1, 0, 0);
                            }
                            break;
                        }
                        village.addTroops(0, -1, 0, 0, 0);
                        break;

                    case 0x49:
                        if (placingReinforcement)
                        {
                            if (!flag)
                            {
                                this.numPlacedReinforceDefenderPikemen++;
                            }
                            else
                            {
                                village.addVassalTroops(0, 0, -1, 0);
                            }
                            break;
                        }
                        village.addTroops(0, 0, -1, 0, 0);
                        break;

                    case 0x55:
                        village.addTroops(0, 0, 0, 0, 0, 0, -1);
                        break;
                }
                this.numAvailableDefenderPeasants = 0;
                this.numAvailableDefenderArchers = 0;
                this.numAvailableDefenderPikemen = 0;
                this.numAvailableDefenderSwordsmen = 0;
                this.numAvailableDefenderCaptains = 0;
                this.numAvailableDefenderCaptains = 0;
                village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
                GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
                village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
            }
            if (!fastMode)
            {
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                this.recalcCastleLayout();
                InterfaceMgr.Instance.castleStartBuilderMode();
            }
            return item;
        }

        public void troopsFollowMouse(int mapX, int mapY)
        {
            if (this.placementSize == 1)
            {
                this.mouseMovePlaceTroops(mapX, mapY, true, 0);
            }
            else if (placementType != 0x5e)
            {
                if (this.placementSize != 4)
                {
                    int spriteIndex = 0;
                    int num2 = this.placementSize - 1;
                    for (int i = mapY - num2; i <= (mapY + num2); i++)
                    {
                        for (int j = mapX - num2; j <= (mapX + num2); j++)
                        {
                            this.mouseMovePlaceTroops(j, i, true, spriteIndex);
                            spriteIndex++;
                        }
                    }
                }
                else
                {
                    int num5 = 0;
                    if (mapX < mapY)
                    {
                        if ((0x75 - mapX) < mapY)
                        {
                            num5 = 0;
                        }
                        else
                        {
                            num5 = 2;
                        }
                    }
                    else if ((0x75 - mapX) < mapY)
                    {
                        num5 = 6;
                    }
                    else
                    {
                        num5 = 4;
                    }
                    if ((num5 == 0) || (num5 == 4))
                    {
                        int num6 = 0;
                        int num7 = 2;
                        for (int k = mapX - num7; k <= (mapX + num7); k++)
                        {
                            this.mouseMovePlaceTroops(k, mapY, true, num6);
                            num6++;
                        }
                    }
                    else
                    {
                        int num9 = 0;
                        int num10 = 2;
                        for (int m = mapY - num10; m <= (mapY + num10); m++)
                        {
                            this.mouseMovePlaceTroops(mapX, m, true, num9);
                            num9++;
                        }
                    }
                }
            }
            else
            {
                int num12 = 0;
                if (mapX < mapY)
                {
                    if ((0x75 - mapX) < mapY)
                    {
                        num12 = 0;
                    }
                    else
                    {
                        num12 = 2;
                    }
                }
                else if ((0x75 - mapX) < mapY)
                {
                    num12 = 6;
                }
                else
                {
                    num12 = 4;
                }
                if ((num12 == 0) || (num12 == 4))
                {
                    int num13 = 0;
                    int num14 = (this.placementSize - 1) * 2;
                    for (int n = mapX - num14; n <= (mapX + num14); n += 2)
                    {
                        this.mouseMovePlaceTroops(n, mapY, true, num13);
                        num13++;
                    }
                }
                else
                {
                    int num16 = 0;
                    int num17 = (this.placementSize - 1) * 2;
                    for (int num18 = mapY - num17; num18 <= (mapY + num17); num18 += 2)
                    {
                        this.mouseMovePlaceTroops(mapX, num18, true, num16);
                        num16++;
                    }
                }
            }
        }

        public void tutorialAutoPlace()
        {
            if (this.elements.Count <= 1)
            {
                this.startPlaceElement(40);
                this.placeBuildingElement(0x35, 0x3b, true);
                this.startPlaceElement(0x21);
                this.placeBuildingElement(0x3f, 0x36, true);
                this.placeBuildingElement(0x3e, 0x36, true);
                this.placeBuildingElement(0x3d, 0x36, true);
                this.placeBuildingElement(60, 0x36, true);
                this.placeBuildingElement(0x3b, 0x36, true);
                this.placeBuildingElement(0x3a, 0x36, true);
                this.placeBuildingElement(0x39, 0x36, true);
                this.placeBuildingElement(0x38, 0x36, true);
                this.placeBuildingElement(0x37, 0x36, true);
                this.placeBuildingElement(0x36, 0x36, true);
                this.placeBuildingElement(0x3f, 0x37, true);
                this.placeBuildingElement(0x3f, 0x38, true);
                this.placeBuildingElement(0x3f, 0x39, true);
                this.placeBuildingElement(0x3f, 0x3a, true);
                this.placeBuildingElement(0x3f, 0x3b, true);
                this.placeBuildingElement(0x3f, 60, true);
                this.placeBuildingElement(0x3f, 0x3d, true);
                this.placeBuildingElement(0x3f, 0x3e, true);
                this.placeBuildingElement(0x3f, 0x3f, true);
                this.placeBuildingElement(0x3e, 0x3f, true);
                this.placeBuildingElement(0x3d, 0x3f, true);
                this.placeBuildingElement(0x36, 0x3f, true);
                this.placeBuildingElement(0x37, 0x3f, true);
                this.placeBuildingElement(0x38, 0x3f, true);
                this.placeBuildingElement(0x36, 0x37, true);
                this.placeBuildingElement(0x36, 0x38, true);
                this.placeBuildingElement(0x36, 0x39, true);
                this.placeBuildingElement(0x36, 0x3d, true);
                this.placeBuildingElement(0x36, 0x3e, true);
                this.stopPlaceElement();
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                this.recalcCastleLayout();
                this.inBuilderMode = false;
            }
        }

        public void tutorialFastForward()
        {
            List<CastleElement> list = this.castleCombat.getAttackerList();
            if (list.Count > 0)
            {
                int xPos = list[0].xPos;
                int yPos = list[0].yPos;
                if (xPos > 100)
                {
                    this.moveMap(0x2710, 0x2710);
                    this.moveMap(-808, 0);
                }
                if (yPos < 20)
                {
                    this.moveMap(0x2710, 0x2710);
                    this.moveMap(-280, 0);
                }
                if (yPos > 100)
                {
                    this.moveMap(0x2710, 0x2710);
                    this.moveMap(-808, -399);
                }
                if (xPos < 20)
                {
                    this.moveMap(0x2710, 0x2710);
                    this.moveMap(-280, -399);
                }
            }
            for (int i = 0; i < 0x145; i++)
            {
                this.castleCombat.tick();
            }
        }

        public void unpauseBattle()
        {
            Sound.pauseEnvironmental(false);
            this.castleCombat.pause(false);
        }

        public void Update(bool villageDisplayed)
        {
            if ((backgroundSprite != null) && villageDisplayed)
            {
                this.tick++;
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
                if ((((this.tick % 300) == 0) || ((fakeKeep > 0) && ((this.tick % 30) == 0))) || (this.m_lassoMade || CreateMode))
                {
                    this.recalcCastleLayout();
                }
                if ((this.tick % 0x2d) == 0)
                {
                    GameEngine.Instance.World.getReinforceTotals(this.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
                }
                backgroundSprite.Update();
                backgroundSprite.AddToRenderList();
                this.drawSurroundSprites();
            }
        }

        public void updateAttackingCaptainCommand(int captainsCommand)
        {
            if (this.m_lassoMade && (this.m_lassoElements.Count == 1))
            {
                foreach (long num in this.m_lassoElements)
                {
                    CastleElement element = this.castleLayout.getElementFromElemID(num);
                    if (((element != null) && (element.elementType >= 100)) && (element.elementType <= 0x6d))
                    {
                        if ((element.elementType == 0x66) || (element.elementType == 0x67))
                        {
                            this.deleteCatapultTarget(num);
                            this.attackNumCatapults--;
                        }
                        element.elementType = (byte) captainsCommand;
                        if ((element.elementType == 0x66) || (element.elementType == 0x67))
                        {
                            this.addNewCatapultTargetDefault(element);
                            this.attackNumCatapults++;
                        }
                        CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                        this.recalcCastleLayout();
                        break;
                    }
                }
            }
        }

        public void updateAvailableTroops()
        {
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                this.numAvailableDefenderPeasants = 0;
                this.numAvailableDefenderArchers = 0;
                this.numAvailableDefenderPikemen = 0;
                this.numAvailableDefenderSwordsmen = 0;
                this.numAvailableDefenderCaptains = 0;
                village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
                GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
                village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
            }
        }

        public void updateCaptainsDetails(int value)
        {
            if (this.m_lassoElements.Count == 1)
            {
                foreach (long num in this.m_lassoElements)
                {
                    this.setCaptainsDetails(num, value);
                }
            }
        }

        public void updateLasso(bool force)
        {
            if (((this.m_lassoLastX != this.m_lassoEndX) || (this.m_lassoLastY != this.m_lassoEndY)) || force)
            {
                this.m_lassoLastX = this.m_lassoEndX;
                this.m_lassoLastY = this.m_lassoEndY;
                this.m_lassoElements.Clear();
                if (!this.attackerSetupMode)
                {
                    int lassoStartX = this.m_lassoStartX;
                    int lassoStartY = this.m_lassoStartY;
                    int lassoEndX = this.m_lassoEndX;
                    int lassoEndY = this.m_lassoEndY;
                    if (lassoStartX > lassoEndX)
                    {
                        int num5 = lassoStartX;
                        lassoStartX = lassoEndX;
                        lassoEndX = num5;
                    }
                    if (lassoStartY > lassoEndY)
                    {
                        int num6 = lassoStartY;
                        lassoStartY = lassoEndY;
                        lassoEndY = num6;
                    }
                    placingDefender = true;
                    int mapX = 0;
                    int mapY = 0;
                    for (int i = lassoStartY; i < lassoEndY; i += 4)
                    {
                        for (int j = lassoStartX; j < lassoEndX; j += 4)
                        {
                            if (this.getMapTileFromMousePos(new Point(j, i), ref mapX, ref mapY))
                            {
                                CastleElement element = this.castleLayout.getTroopElement(mapX, mapY);
                                if ((element == null) && (CreateMode || this.inTroopPlacerMode))
                                {
                                    element = this.castleLayout.getTroopElementMover(mapX, mapY);
                                }
                                if (((element != null) && (((element.elementType <= 0x4b) && (element.elementType >= 0x45)) || ((element.elementType <= 0x59) && (element.elementType >= 0x55)))) && !this.m_lassoElements.Contains(element.elementID))
                                {
                                    if (element.elementType != 0x4b)
                                    {
                                        this.m_lassoElements.Insert(0, element.elementID);
                                    }
                                    else
                                    {
                                        this.m_lassoElements.Add(element.elementID);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    int num11 = this.m_lassoStartX;
                    int num12 = this.m_lassoStartY;
                    int num13 = this.m_lassoEndX;
                    int num14 = this.m_lassoEndY;
                    if (num11 > num13)
                    {
                        int num15 = num11;
                        num11 = num13;
                        num13 = num15;
                    }
                    if (num12 > num14)
                    {
                        int num16 = num12;
                        num12 = num14;
                        num14 = num16;
                    }
                    placingDefender = false;
                    int num17 = 0;
                    int num18 = 0;
                    List<long> list = new List<long>();
                    for (int k = num12; k < num14; k += 4)
                    {
                        for (int m = num11; m < num13; m += 4)
                        {
                            if (this.getMapTileFromMousePos(new Point(m, k), ref num17, ref num18))
                            {
                                CastleElement element2 = this.castleLayout.getTroopElementMover(num17, num18);
                                if (element2 != null)
                                {
                                    if (element2.elementType == 0x5e)
                                    {
                                        if (!list.Contains(element2.elementID))
                                        {
                                            list.Add(element2.elementID);
                                        }
                                    }
                                    else if ((element2.elementType >= 90) && !this.m_lassoElements.Contains(element2.elementID))
                                    {
                                        this.m_lassoElements.Add(element2.elementID);
                                    }
                                }
                            }
                        }
                    }
                    if ((this.m_lassoElements.Count == 0) && (list.Count > 0))
                    {
                        this.m_lassoElements = list;
                    }
                }
                this.recalcCastleLayout();
            }
        }

        public void updateLaunchButton()
        {
            if (this.placingAttackerRealMode)
            {
                bool state = this.isAttackReady();
                InterfaceMgr.Instance.castleAttackShowAttackReady(state);
            }
            else
            {
                InterfaceMgr.Instance.castleAttackShowAttackReady(true);
            }
        }

        public void updateOldAttackSetupFilenames()
        {
            FileStream input = null;
            BinaryWriter writer = null;
            BinaryReader reader = null;
            List<string> list = new List<string>();
            int num = 0;
            bool flag = false;
            try
            {
                for (int i = 1; i < 6; i++)
                {
                    string path = this.getAttackSetupSaveName(i);
                    if (File.Exists(path))
                    {
                        string destFileName = this.getAttackSetupSaveName("Formation " + i.ToString());
                        File.Move(path, destFileName);
                        list.Add("Formation " + i.ToString());
                        num++;
                        flag = true;
                    }
                }
                if (flag)
                {
                    string str3 = GameEngine.getSettingsPath(true);
                    string str4 = "StoredSetupNames.cas";
                    str4 = str3 + @"\" + str4;
                    if (File.Exists(str4))
                    {
                        input = new FileStream(str4, FileMode.Open);
                        reader = new BinaryReader(input);
                        num += reader.ReadInt32();
                        for (int j = 0; j < num; j++)
                        {
                            list.Add(reader.ReadString());
                        }
                        reader.Close();
                        input.Close();
                    }
                    input = new FileStream(str4, FileMode.Create);
                    writer = new BinaryWriter(input);
                    writer.Write(num);
                    foreach (string str5 in list)
                    {
                        writer.Write(str5);
                    }
                    writer.Close();
                    input.Close();
                }
            }
            catch (Exception)
            {
                if (input != null)
                {
                    input.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        private void updateRocks()
        {
            List<RockChip> list = new List<RockChip>();
            foreach (RockChip chip in this.rockChips)
            {
                chip.height += chip.vVelocity;
                chip.vVelocity -= chip.gravityValue;
                chip.xPos += chip.dx;
                chip.yPos += chip.dy;
                if (chip.height <= 0f)
                {
                    list.Add(chip);
                }
            }
            foreach (RockChip chip2 in list)
            {
                this.rockChips.Remove(chip2);
            }
            this.updateRockSmoke();
        }

        private void updateRockSmoke()
        {
            List<RockSmoke> list = new List<RockSmoke>();
            foreach (RockSmoke smoke in this.rockSmoke)
            {
                smoke.animFrame++;
                if (smoke.animFrame >= 20)
                {
                    list.Add(smoke);
                }
            }
            foreach (RockSmoke smoke2 in list)
            {
                this.rockSmoke.Remove(smoke2);
            }
        }

        public bool updateTroopMove(Point mousePos, bool leftDown)
        {
            if (leftDown)
            {
                if (this.troopMovingElemID == -2L)
                {
                    Point point = mousePos;
                    point.X += -((int) backgroundSprite.DrawPos.X);
                    point.Y += -((int) backgroundSprite.DrawPos.Y);
                    long num3 = this.clickFindTroop(point);
                    if (num3 != -2L)
                    {
                        foreach (CastleElement element2 in this.elements)
                        {
                            if (element2.elementID == num3)
                            {
                                this.troopMovingMode = false;
                                if (placingDefender)
                                {
                                    this.startPlacingTroops(element2.elementType, element2.reinforcement);
                                }
                                else
                                {
                                    this.setPlacementSize(1);
                                    this.startPlacingAttackerTroops(element2.elementType);
                                }
                                CursorManager.SetCursor(CursorManager.CursorType.VSplit, InterfaceMgr.Instance.ParentForm);
                                this.troopMovingMode = true;
                                this.troopMovingElemID = num3;
                                this.recalcCastleLayout();
                                return true;
                            }
                        }
                    }
                    return false;
                }
                int num4 = -1;
                int num5 = -1;
                if (this.getMapTileFromMousePos(mousePos, ref num4, ref num5))
                {
                    this.mouseMovePlaceTroops(num4, num5, false, 0);
                }
                return true;
            }
            UniversalDebugLog.Log("updatedTroopMove " + this.troopMovingElemID);
            if (this.troopMovingElemID == -2L)
            {
                return false;
            }
            int mapX = -1;
            int mapY = -1;
            if (this.getMapTileFromMousePos(mousePos, ref mapX, ref mapY) && this.mouseMovePlaceTroops(mapX, mapY, false, 0))
            {
                foreach (CastleElement element in this.elements)
                {
                    if (element.elementID == this.troopMovingElemID)
                    {
                        if (placingDefender && !CreateMode)
                        {
                            RemoteServices.Instance.set_AddCastleElement_UserCallBack(new RemoteServices.AddCastleElement_UserCallBack(this.newElementCallback));
                            RemoteServices.Instance.AddCastleElement(this.m_villageID, element.elementType, mapX, mapY, this.troopMovingElemID);
                        }
                        element.xPos = (byte) mapX;
                        element.yPos = (byte) mapY;
                        CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
                        this.recalcCastleLayout();
                        break;
                    }
                }
            }
            this.troopMovingElemID = -2L;
            if (placementTroopCastleSprite[0] != null)
            {
                if (placementTroopSprite[0] != null)
                {
                    placementTroopSprite[0].RemoveChild(placementTroopCastleSprite[0]);
                }
                placementTroopCastleSprite[0] = null;
            }
            if (placementTroopSprite[0] != null)
            {
                if (backgroundSprite != null)
                {
                    backgroundSprite.RemoveChild(placementTroopSprite[0]);
                }
                placementTroopSprite[0] = null;
            }
            this.recalcCastleLayout();
            return true;
        }

        public bool useHandlesForLayingOut(int element)
        {
            if (((element != 0x22) && (element != 0x21)) && (element != 0x41))
            {
                return (element == 0x42);
            }
            return true;
        }

        public void wallMouseMove(int mapX, int mapY, bool leftDown)
        {
            this.correctPlacementType(placementType);
            if (!leftDown || this.waitingForWallReturn)
            {
                if (this.draggingWall)
                {
                    this.draggingWall = false;
                    if (this.wallWasValid)
                    {
                        if (!this.inBuilderMode)
                        {
                            this.startBuilderMode();
                        }
                        if (placementType == 0x24)
                        {
                            GameEngine.Instance.playInterfaceSound("CastleMap_EndPit");
                        }
                        else if (placementType == 0x23)
                        {
                            GameEngine.Instance.playInterfaceSound("CastleMap_EndMoat");
                        }
                        else if ((placementType == 0x22) || (placementType == 0x41))
                        {
                            GameEngine.Instance.playInterfaceSound("CastleMap_EndStoneWall");
                        }
                        else if ((placementType == 0x21) || (placementType == 0x42))
                        {
                            GameEngine.Instance.playInterfaceSound("CastleMap_EndWoodWall");
                        }
                        this.addWallSprites();
                        InterfaceMgr.Instance.castleStartBuilderMode();
                        this.wallWasValid = false;
                    }
                    else
                    {
                        this.clearPlacementWallSprites();
                    }
                }
                else
                {
                    this.movePlaceElement(mapX, mapY, placementSprite, false, true);
                }
            }
            else
            {
                if (!this.draggingWall)
                {
                    if (placementType == 0x24)
                    {
                        GameEngine.Instance.playInterfaceSound("CastleMap_StartPit");
                    }
                    else if (placementType == 0x23)
                    {
                        GameEngine.Instance.playInterfaceSound("CastleMap_StartMoat");
                    }
                    else if ((placementType == 0x22) || (placementType == 0x41))
                    {
                        GameEngine.Instance.playInterfaceSound("CastleMap_StartStoneWall");
                    }
                    else if ((placementType == 0x21) || (placementType == 0x42))
                    {
                        GameEngine.Instance.playInterfaceSound("CastleMap_StartWoodWall");
                    }
                    this.startWallMapX = mapX;
                    this.startWallMapY = mapY;
                    this.draggingWall = true;
                    if (placementSprite != null)
                    {
                        placementSprite.Visible = false;
                    }
                }
                this.clearPlacementWallSprites();
                this.addWallConstructionToMap(mapX, mapY, placementType);
            }
        }

        public double CapitalAttackRate
        {
            get
            {
                return this.attackCapitalAttackRate;
            }
        }

        public int CastleMode
        {
            get
            {
                return this.m_castleMode;
            }
        }

        public static bool CreateMode
        {
            get
            {
                return createMode;
            }
            set
            {
                createMode = value;
            }
        }

        public static int FakeDefensiveMode
        {
            set
            {
                fakeDefensiveMode = value;
            }
        }

        public static int FakeKeep
        {
            set
            {
                fakeKeep = value;
            }
        }

        public bool InBuilderMode
        {
            get
            {
                return this.inBuilderMode;
            }
        }

        public bool InTroopPlacerMode
        {
            get
            {
                return this.inTroopPlacerMode;
            }
        }

        public int VillageID
        {
            get
            {
                return this.m_villageID;
            }
        }

        public class BattlePlaySFX
        {
            public int sfx_nextSound = -1000000;

            public void playBattleSounds(int tick, int numEvents, int soundDelay, int multiplier, int lowThreshold, string[] lowSounds, int midThreshold, string[] midSounds, string[] highSounds, CastleMap parent)
            {
                if (((tick - this.sfx_nextSound) > 0) && (numEvents > 0))
                {
                    this.sfx_nextSound = tick + (soundDelay * multiplier);
                    if (numEvents < lowThreshold)
                    {
                        parent.playRandSFX(lowSounds);
                    }
                    else if (numEvents < midThreshold)
                    {
                        parent.playRandSFX(midSounds);
                    }
                    else
                    {
                        parent.playRandSFX(highSounds);
                    }
                }
            }

            public void playBattleSoundsNO(int tick, int numEvents, int soundDelay, int multiplier, int lowThreshold, string[] lowSounds, int midThreshold, string[] midSounds, string[] highSounds, CastleMap parent)
            {
                if (((tick - this.sfx_nextSound) > 0) && (numEvents > 0))
                {
                    this.sfx_nextSound = tick + ((soundDelay + parent.sfxRandom.Next(soundDelay / 2)) * multiplier);
                    if (numEvents < lowThreshold)
                    {
                        parent.playRandSFXNoOverwrite(lowSounds);
                    }
                    else if (numEvents < midThreshold)
                    {
                        parent.playRandSFXNoOverwrite(midSounds);
                    }
                    else
                    {
                        parent.playRandSFXNoOverwrite(highSounds);
                    }
                }
            }
        }

        public class CatapultLine
        {
            public int endX = -1;
            public int endY = -1;
            public int startX = -1;
            public int startY = -1;
        }

        public class RestoreCastleElement
        {
            public byte delay;
            public byte elementType;
            public byte targXPos;
            public byte targYPos;
            public byte xPos;
            public byte yPos;
        }

        public class RockChip
        {
            public bool black;
            public float dx;
            public float dy;
            public float gravityValue;
            public float height;
            public int image;
            public float vVelocity;
            public float xPos;
            public float yPos;
        }

        public class RockSmoke
        {
            public int animFrame;
            public bool black;
            public float xPos;
            public float yPos;
        }

        public class TempTileSortClass
        {
            public int gx;
            public int gy;
            public int sx;
            public int sy;
        }

        public class TempTileSortComparer : IComparer<CastleMap.TempTileSortClass>
        {
            public int Compare(CastleMap.TempTileSortClass y, CastleMap.TempTileSortClass x)
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
                if (x.sy < y.sy)
                {
                    return 1;
                }
                if (x.sy > y.sy)
                {
                    return -1;
                }
                return 0;
            }
        }

        private class TroopClickArea
        {
            public long elementID;
            public int h;
            public int w;
            public int x;
            public int y;

            public void addUnit(Point pos, long id)
            {
                this.elementID = id;
                this.x = pos.X - 0x10;
                this.w = 0x20;
                this.h = 50;
                this.y = pos.Y - 0x27;
            }

            public bool clicked(Point mousePos)
            {
                return (((mousePos.X >= this.x) && (mousePos.X <= (this.x + this.w))) && ((mousePos.Y >= this.y) && (mousePos.Y <= (this.y + this.h))));
            }
        }
    }
}

