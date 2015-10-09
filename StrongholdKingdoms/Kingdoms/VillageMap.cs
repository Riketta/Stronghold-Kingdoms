namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class VillageMap
    {
        private SparseArray animalArray = new SparseArray();
        private static short[] armourerIdleAnim = new short[] { 
            1, 1, 2, 2, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 
            4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 
            4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 
            4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 
            4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 
            6, 6, 7, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0x10, 8, 9, 10, 
            11, 12, 13, 14, 15, 0x10, 8, 9, 10, 11, 12, 13, 14, 15, 0x10, 8, 
            9, 10, 11, 12, 13, 14, 15, 0x10, 8, 9, 10, 11, 12, 13, 14, 15, 
            0x10, 8, 9, 10, 11, 12, 13, 14, 15, 0x10, 2, 3, 4, 4, 4, 5, 
            6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0x10
         };
        private SpriteWrapper backgroundOverlaySprite;
        private SpriteWrapper backgroundSprite;
        private static int backgroundTexture = -1;
        private static short[] bakerIdleAnim = new short[] { 
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 13, 
            14, 15, 0x10, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x10, 
            15, 14, 13, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 13, 14, 15, 0x10, 0x11, 0x12, 0x13, 20, 20, 
            20, 20, 20, 20, 0x13, 0x12, 0x13, 20, 20, 20, 0x13, 0x12, 0x11, 0x10, 15, 14, 
            13, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 13, 14, 15, 0x10, 0x11, 0x12, 0x13, 20, 
            20, 20, 20, 20, 20, 0x13, 0x12, 0x13, 20, 20, 20, 0x13, 0x12, 0x11, 0x10, 15, 
            14, 13, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 13, 13, 14, 14, 
            15, 15, 0x10, 0x10, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 
            0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 
            0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 
            0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 
            0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x10, 0x10, 15, 15, 
            14, 14, 13, 13, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 2, 12, 12, 13, 14, 15, 0x10, 0x11, 0x12, 0x13, 20, 20, 20, 20, 20, 
            20, 0x13, 0x12, 0x13, 20, 20, 20, 0x13, 0x12, 0x11, 0x10, 15, 14, 13, 12, 12, 
            12, 12, 12, 12, 12, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 
            2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1
         };
        private static DateTime baseServerTime = DateTime.Now;
        private static short[] blacksmithIdleAnim = new short[] { 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
            2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
            2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15
         };
        private static short[] brewerIdleAnim = new short[] { 
            1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 1, 2, 3, 4, 5, 
            6, 7, 8, 9, 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 1, 
            2, 3, 4, 5, 6, 7, 8, 9, 1, 1, 2, 2, 3, 3, 4, 4, 
            5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0x10, 0x10, 0x10, 0x10, 0x10, 
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 12, 12, 12, 12, 
            12, 12, 12, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 13, 14, 15, 0x10, 0x10, 0x10, 0x10, 0x10, 1, 
            2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 12, 12, 12, 12, 12, 
            12, 12, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 13, 14, 15, 0x10, 1, 1, 2, 3, 4, 5, 
            6, 7, 8, 9, 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 1, 
            2, 3, 4, 5, 6, 7, 8, 9, 1, 1, 2, 3, 4, 5, 6, 7, 
            8, 9, 1, 1, 2, 2, 3, 3, 4, 4, 5, 6, 7, 8, 9, 10, 
            11, 12, 13, 14, 15, 0x10, 0x10, 0x10, 0x10, 0x10, 1, 1, 2, 2, 3, 3, 
            4, 4, 5, 6, 7, 8, 9, 10, 11, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 
            13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 
            13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 12, 12, 
            12, 12, 12, 13, 13, 14, 14, 15, 15, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 1, 
            2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 12, 12, 12, 12, 12, 
            12, 12, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 13, 14, 15, 0x10
         };
        private BuildingOrderComparer buildingOrderComparer = new BuildingOrderComparer();
        private static short[] carpenterIdleAnim = new short[] { 
            1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 
            9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 0x10, 0x10, 
            0x11, 0x11, 0x12, 0x12, 0x13, 0x13, 20, 20, 0x15, 0x15, 0x16, 0x16, 0x17, 0x17, 0x18, 0x18
         };
        private List<DateTime> ConstrTimeCompletionList = new List<DateTime>();
        private static short[] cowIdleAnim = new short[] { 
            0, 8, 0x10, 0x18, 0x20, 40, 0x30, 0x38, 0x40, 0x48, 80, 0x58, 0x60, 0x68, 0x70, 120, 
            0x70, 0x68, 0x60, 0x58, 80, 0x48, 0x40, 0x38, 0x30, 40, 0x20, 0x18, 0x10, 8
         };
        private static short[] cowLayAnim = new short[] { 
            0, 8, 0x10, 0x18, 0x20, 40, 0x30, 0x38, 0x40, 0x48, 80, 0x58, 0x60, 0x68, 0x70, 120, 
            120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 
            120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 
            120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 
            120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 
            120, 120, 120, 120, 120, 120, 120, 0x70, 0x68, 0x60, 0x58, 80, 0x48, 0x40, 0x38, 0x30, 
            40, 0x20, 0x18, 0x10, 8, 0
         };
        private bool disbandPeopleLocked;
        private DateTime disbandPeopleLockedTime = DateTime.MinValue;
        private bool disbandTroopsLocked;
        private DateTime disbandTroopsLockedTime = DateTime.MinValue;
        private static short[] dockworkerIdleAnim = new short[] { 
            1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 
            9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 0x10, 0x10, 
            0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 15, 15, 14, 
            14, 13, 13, 12, 12, 11, 11, 10, 10, 9, 9, 8, 8, 7, 7, 6, 
            6, 5, 5, 4, 4, 3, 3, 2, 2, 1, 1
         };
        private VillageMapBuilding fakeArmoury = new VillageMapBuilding();
        private static short[] farmer3IdleAnim = new short[] { 
            0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 7, 7, 
            7, 7, 7, 7, 7, 7, 7, 6, 5, 4, 3, 2, 1, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0
         };
        private GraphicsMgr gfx;
        private static bool GFXLoaded = false;
        public int granaryOpenCount;
        private static short[] hunterIdleAnim = new short[] { 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 2, 2, 3, 3, 4, 4, 5, 6, 7, 8, 
            9, 10, 11, 12, 13, 14, 15, 0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x18, 
            0x19, 0x1a, 0x1b, 0x1c, 0x1d, 30, 30, 30, 0x1f, 0x1f, 0x20, 0x20, 0x21, 0x21, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 2, 2, 3, 3, 4, 4, 5, 6, 7, 8, 9, 
            10, 11, 12, 13, 14, 15, 0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x18, 0x19, 
            0x1a, 0x1b, 0x1c, 0x1d, 30, 30, 30, 0x1f, 0x1f, 0x20, 0x20, 0x21, 0x21, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 3, 3, 
            4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 
            12, 12, 13, 13, 14, 14, 15, 15, 0x10, 0x10, 0x11, 0x11, 0x12, 0x12, 0x13, 0x13, 
            20, 20, 0x15, 0x15, 0x16, 0x16, 0x17, 0x17, 0x18, 0x18, 0x19, 0x19, 0x1a, 0x1a, 0x1b, 0x1b, 
            0x1c, 0x1c, 0x1d, 0x1d, 0x1c, 0x1c, 0x1b, 0x1b, 0x1a, 0x1a, 0x19, 0x19, 0x18, 0x18, 0x17, 0x17, 
            0x16, 0x16, 0x15, 0x15, 20, 20, 0x13, 0x13, 0x12, 0x12, 0x11, 0x11, 0x10, 0x10, 15, 15, 
            14, 14, 13, 13, 12, 12, 11, 11, 10, 10, 9, 9, 8, 8, 7, 7, 
            8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 
            0x10, 0x10, 0x11, 0x11, 0x12, 0x12, 0x13, 0x13, 20, 20, 0x15, 0x15, 0x16, 0x16, 0x17, 0x17, 
            0x18, 0x18, 0x19, 0x19, 0x1a, 0x1a, 0x1b, 0x1b, 0x1c, 0x1c, 0x1d, 0x1d, 30, 30, 0x1f, 0x1f, 
            0x20, 0x20, 0x21, 0x21, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 3, 3, 4, 
            4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 
            12, 13, 13, 14, 14, 15, 15, 0x10, 0x10, 0x11, 0x11, 0x12, 0x12, 0x13, 0x13, 20, 
            20, 0x15, 0x15, 0x16, 0x16, 0x17, 0x17, 0x18, 0x18, 0x19, 0x19, 0x1a, 0x1a, 0x1b, 0x1b, 0x1c, 
            0x1c, 0x1d, 0x1d, 30, 30, 0x1f, 0x1f, 0x20, 0x20, 0x21, 0x21, 1
         };
        private bool inMarketSend;
        private bool inPlaceBuilding;
        private bool inSendBuildingActivity;
        private DateTime inSendBuildingActivityLastTime = DateTime.MinValue;
        private static string lastBackgroundImageName = "";
        private DateTime lastBuildingPlacement = DateTime.MinValue;
        private DateTime lastClickedSound = DateTime.MinValue;
        public DateTime lastDownloadedTime = DateTime.MinValue;
        private DateTime lastMarketSend = DateTime.MinValue;
        private Point lastPlaceBuildingLoc = new Point();
        private DateTime lastTraderRefresh = DateTime.MinValue;
        private VillageLayoutNew layout;
        private static double localBaseTime = 0.0;
        private List<VillageMapBuilding> localBuildings = new List<VillageMapBuilding>();
        private int localMadeTroops_Archers;
        private int localMadeTroops_Captains;
        private int localMadeTroops_Catapults;
        private DateTime localMadeTroops_lastTime = DateTime.MinValue;
        private int localMadeTroops_lastType = -1;
        private int localMadeTroops_Peasants;
        private int localMadeTroops_Pikemen;
        private int localMadeTroops_Scouts;
        private int localMadeTroops_Swordsmen;
        private int localMadeTroopsSent_Archers;
        private int localMadeTroopsSent_Captains;
        private int localMadeTroopsSent_Catapults;
        private int localMadeTroopsSent_Peasants;
        private int localMadeTroopsSent_Pikemen;
        private int localMadeTroopsSent_Scouts;
        private int localMadeTroopsSent_Swordsmen;
        public double m_aleConsumption;
        public double m_aleLevel;
        public int m_aleRationsLevel;
        public int m_aleRationsLevelSent;
        public int m_aleRationsLevelServer;
        public double m_applesConsumption;
        public double m_applesLevel;
        public double m_armourLevel;
        private Point m_baseMousePos = new Point();
        private double m_baseScreenX;
        private double m_baseScreenY;
        public double m_bowsLevel;
        public double m_breadConsumption;
        public double m_breadLevel;
        public List<int> m_capitalBuildingsBuilt;
        public double m_capitalGold;
        public int m_capitalTaxRate;
        public int m_capitalTaxRateSent;
        public int m_capitalTaxRateServer;
        public DateTime m_captainCreationTime = DateTime.MinValue;
        public DateTime m_captialNextDelete = DateTime.MinValue;
        public bool m_castleEnclosed;
        public double m_catapultsLevel;
        public double m_cheeseConsumption;
        public double m_cheeseLevel;
        public double m_clothesLevel;
        public DateTime m_consumptionChangeTime = DateTime.Now;
        public bool m_consumptionChangeTimeNeeded;
        public DateTime m_consumptionLastTime = DateTime.Now;
        public bool m_creatingCaptain;
        public double m_effectiveAleRationsLevel;
        public double m_effectiveRationsLevel;
        public DateTime m_excommunicationTime = DateTime.MinValue;
        public double m_fishConsumption;
        public double m_fishLevel;
        public double m_furnitureLevel;
        public int m_housingCapacity;
        public DateTime m_immigrationNextChangeTime = DateTime.Now;
        public DateTime m_interdictionTime = DateTime.MinValue;
        public double m_ironLevel;
        public DateTime m_lastBanquetDate = DateTime.Now;
        public double m_lastBanquetHonour;
        public bool m_lastBanquetStored;
        public int m_lastCapitalTaxRate;
        private Point m_lastMousePos = new Point();
        private DateTime m_lastMousePosChangeTime = DateTime.MaxValue;
        private double m_lastMousePressedTime;
        private long m_lastOverBuildingID = -1L;
        public DateTime m_lastParishPeopleTime = DateTime.MinValue;
        public DateTime m_lastServerReply = DateTime.Now;
        private bool m_leftMouseGrabbed;
        private bool m_leftMouseHeldDown;
        private int m_mapID = -1;
        private int m_mapVariant = -1;
        public double m_meatConsumption;
        public double m_meatLevel;
        public double m_metalwareLevel;
        private static VillageMapBuilding m_movingBuilding = null;
        public DateTime m_nextMapTypeChange = DateTime.MinValue;
        public DateTime m_nextWeaponsCheck = DateTime.Now.AddHours(4.0);
        public int m_numArchers;
        public int m_numCaptains;
        public int m_numCatapults;
        public int m_numFoodTypesEaten;
        public int m_numNegativeBuildings;
        public int m_numOfActiveChildrenAreas;
        public int m_numParishFlags;
        public int m_numPeasants;
        public int m_numPikemen;
        public int m_numPopularityBuildings;
        public int m_numPositiveBuildings;
        public int m_numScouts;
        public int m_numStationedArchers;
        public int m_numStationedCatapults;
        public int m_numStationedPeasants;
        public int m_numStationedPikemen;
        public int m_numStationedSwordsmen;
        public int m_numSwordsmen;
        public int m_numTradersAtHome;
        public DateTime m_ownedDate = DateTime.MinValue;
        public int m_parentCapitalTaxRate;
        public ResearchData m_parishCapitalResearchData;
        public ParishTaxCalc[] m_parishPeople;
        public double m_pikesLevel;
        public double m_pitchLevel;
        public PopEventData[] m_popEvents;
        public int m_popularityLevel;
        private int m_preCountedBurningPosts;
        private int m_preCountedCathedrals;
        private int m_preCountedChapels;
        private int m_preCountedChurches;
        private int m_preCountedDovecotes;
        private int m_preCountedGibbets;
        private int m_preCountedLargeGardens;
        private int m_preCountedLargeStatues;
        private int m_preCountedRacks;
        private int m_preCountedSmallGardens;
        private int m_preCountedSmallStatues;
        private int m_preCountedStocks;
        private Point m_previousMousePos = new Point();
        public DateTime m_productionEnd_Armour = DateTime.Now;
        public DateTime m_productionEnd_Bows = DateTime.Now;
        public DateTime m_productionEnd_Catapults = DateTime.Now;
        public DateTime m_productionEnd_Pikes = DateTime.Now;
        public DateTime m_productionEnd_Swords = DateTime.Now;
        public double m_productionRate_Armour;
        public double m_productionRate_Bows;
        public double m_productionRate_Catapults;
        public double m_productionRate_Pikes;
        public double m_productionRate_Swords;
        public DateTime m_productionStart_Armour = DateTime.Now;
        public DateTime m_productionStart_Bows = DateTime.Now;
        public DateTime m_productionStart_Catapults = DateTime.Now;
        public DateTime m_productionStart_Pikes = DateTime.Now;
        public DateTime m_productionStart_Swords = DateTime.Now;
        public int m_rationsLevel;
        public int m_rationsLevelSent;
        public int m_rationsLevelServer;
        public double m_saltLevel;
        public bool m_showAleEffective = true;
        public bool m_showEffective = true;
        public double m_silkLevel;
        public int m_spareWorkers;
        public double m_spicesLevel;
        public double m_statsChangeTime;
        public bool m_statsConsumptionUpdateRequested;
        public bool m_statsMigrationUpdateRequested;
        public double m_stoneLevel;
        public double m_swordsLevel;
        public int m_taxLevel;
        public int m_taxLevelSent;
        public int m_taxLevelServer;
        public double m_toBeMade_Armour;
        public double m_toBeMade_Bows;
        public double m_toBeMade_Catapults;
        public double m_toBeMade_Pikes;
        public double m_toBeMade_Swords;
        public int m_totalPeople;
        public double m_vegConsumption;
        public double m_vegLevel;
        public double m_venisonLevel;
        private int m_villageID = -1;
        private DateTime m_villageInfoUpdateLastTime = DateTime.MinValue;
        private int m_villageMapType;
        public double m_wineLevel;
        public double m_woodLevel;
        private bool makePeopleLocked;
        private DateTime makePeopleLockedTime = DateTime.MinValue;
        private bool makeTroopsLocked;
        private DateTime makeTroopsLockedTime = DateTime.MinValue;
        public const int MAP_NUM_TILES_HIGH = 0x80;
        public const int MAP_NUM_TILES_WIDE = 0x40;
        public const int MAP_TILE_HEIGHT = 0x10;
        public const int MAP_TILE_WIDTH = 0x20;
        private static short[] metalWorkerIdleAnim = new short[] { 
            1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 
            9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 0x10, 0x10
         };
        private bool overWikiHelp;
        private static short[] pitchworkerIdleAnim = new short[] { 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 3, 3, 4, 4, 5, 
            5, 6, 6, 7, 7, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 
            8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 
            8, 8, 8, 8, 7, 6, 5, 4, 3, 2, 1, 1, 1, 1, 1, 1, 
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
            1, 1
         };
        private int placementError;
        private static SpriteWrapper placementSprite = null;
        private static SpriteWrapper placementSprite_cancel = null;
        private static SpriteWrapper placementSprite_confirm = null;
        private static SpriteWrapper placementSprite_subSprite = null;
        private static int placementType = 0;
        private static bool placingAsFree = false;
        private Point productionArrowProductionBuilding = new Point(-1, -1);
        private Point productionArrowTarget2Building = new Point(-1, -1);
        private Point productionArrowTargetBuilding = new Point(-1, -1);
        private SparseArray randStateArray = new SparseArray();
        private static VillageBuildingDataNew[] s_villageBuildingData = null;
        private static VillageLayoutNew[] s_villageLayout = null;
        private static short[] siegeWorkerIdleAnim = new short[] { 
            1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 
            9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 0x10, 0x10
         };
        private static List<SpriteWrapper> surroundsprites = new List<SpriteWrapper>();
        private static short[] tailorIdleAnim = new short[] { 
            1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 
            9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 0x10, 0x10
         };
        private bool tooltipWasVisble;
        private List<MarketTraderData> traders = new List<MarketTraderData>();
        private static SpriteWrapper tutorialOverlaySprite = new SpriteWrapper();
        private bool tutorialStage_AppleFarm_Activated;
        private bool tutorialStage_Wood_Activated;
        private static short[] updatedSaltWorkerAnim = new short[] { 
            0, 4, 8, 12, 0x10, 20, 0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 0x10, 20, 
            0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 0x38, 0x34, 0x30, 0x2c, 40, 0x24, 0x20, 0x1c, 
            0x18, 20, 0x10, 20, 0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 0x38, 0x34, 0x30, 0x2c, 
            40, 0x24, 0x20, 0x1c, 0x18, 20, 0x10, 20, 0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 
            0x38, 0x34, 0x30, 0x2c, 40, 0x24, 0x20, 0x1c, 0x18, 20, 0x10, 20, 0x18, 0x1c, 0x20, 0x24, 
            40, 0x2c, 0x30, 0x34, 0x38, 0x34, 0x30, 0x2c, 40, 0x24, 0x20, 0x1c, 0x18, 20, 0x10, 20, 
            0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 0x38, 0x34, 0x30, 0x2c, 40, 0x24, 0x20, 0x1c, 
            0x18, 20, 0x10, 20, 0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 0x38, 0x34, 0x30, 0x2c, 
            40, 0x24, 0x20, 0x1c, 0x18, 20, 0x10, 12, 8, 4, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
         };
        private static short[] updatedVegWorkerAnim = new short[] { 
            0, 4, 8, 12, 0x10, 20, 0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 0x10, 20, 
            0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 0x38, 0x34, 0x30, 0x2c, 40, 0x24, 0x20, 0x1c, 
            0x18, 20, 0x10, 20, 0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 0x38, 0x34, 0x30, 0x2c, 
            40, 0x24, 0x20, 0x1c, 0x18, 20, 0x10, 20, 0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 
            0x38, 0x34, 0x30, 0x2c, 40, 0x24, 0x20, 0x1c, 0x18, 20, 0x10, 20, 0x18, 0x1c, 0x20, 0x24, 
            40, 0x2c, 0x30, 0x34, 0x38, 0x34, 0x30, 0x2c, 40, 0x24, 0x20, 0x1c, 0x18, 20, 0x10, 20, 
            0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 0x38, 0x34, 0x30, 0x2c, 40, 0x24, 0x20, 0x1c, 
            0x18, 20, 0x10, 20, 0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 0x38, 0x34, 0x30, 0x2c, 
            40, 0x24, 0x20, 0x1c, 0x18, 20, 0x10, 20, 0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 
            0x38, 0x34, 0x30, 0x2c, 40, 0x24, 0x20, 0x1c, 0x18, 20, 0x10, 20, 0x18, 0x1c, 0x20, 0x24, 
            40, 0x2c, 0x30, 0x34, 0x38, 0x34, 0x30, 0x2c, 40, 0x24, 0x20, 0x1c, 0x18, 20, 0x10, 20, 
            0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 0x38, 0x34, 0x30, 0x2c, 40, 0x24, 0x20, 0x1c, 
            0x18, 20, 0x10, 20, 0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 0x38, 0x34, 0x30, 0x2c, 
            40, 0x24, 0x20, 0x1c, 0x18, 20, 0x10, 20, 0x18, 0x1c, 0x20, 0x24, 40, 0x2c, 0x30, 0x34, 
            0x38, 0x34, 0x30, 0x2c, 40, 0x24, 0x20, 0x1c, 0x18, 20, 0x10, 12, 8, 4, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
         };
        private int updateFilter;
        private double viewHonour;
        private bool viewOnly;
        public static VillageClickMask villageClickMask = new VillageClickMask();
        private DateTime weaponProductionLastTimeRequest = DateTime.MinValue;
        private static SpriteWrapper wikiHelpSprite = new SpriteWrapper();
        private static short[] woodcutterIdleAnim = new short[] { 
            0, 1, 2, 3, 4, 5, 6, 6, 6, 6, 5, 5, 4, 4, 3, 3, 
            2, 2, 3, 4, 5, 6, 6, 6, 6, 6, 6, 5, 4, 3, 2, 1, 
            0, 0, 0, 1, 2, 3, 4, 5, 6, 6, 6, 6, 6, 7, 8, 9, 
            10, 11, 12, 13, 14, 15, 0x10, 0x10, 0x10, 15, 15, 14, 14, 13, 13, 12, 
            12, 11, 11, 10, 10, 9, 9, 9, 10, 11, 12, 13, 14, 15, 0x10, 0x10, 
            0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 0x17, 0x17, 0x17, 
            0x17, 0x17, 0x17
         };

        public VillageMap(int mapID, int mapVariant, int mapType, int villageID, GraphicsMgr mgr)
        {
            this.m_villageID = villageID;
            this.m_mapID = mapID;
            this.m_mapVariant = mapVariant;
            this.m_villageMapType = mapType;
            this.layout = villageLayout[mapID].createClone();
            this.gfx = mgr;
            this.loadBackgroundImage();
            this.initGFX(mgr);
            this.tutorialStage_AppleFarm_Activated = false;
            this.tutorialStage_Wood_Activated = false;
            if (!GameEngine.Instance.World.TutorialIsAdvancing() && (GameEngine.Instance.World.getTutorialStage() == 0x67))
            {
                GameEngine.Instance.World.checkQuestObjectiveComplete(14);
            }
        }

        private void addBuildingToMap(VillageMapBuilding newBuilding, Point location, int buildingType)
        {
            DataExport.addBuildingToMap(newBuilding, location, buildingType);

            try
            {
                int num = 0;
                if (buildingType == 1)
                {
                    switch (GameEngine.Instance.World.UserResearchData.Research_HousingCapacity)
                    {
                        case 2:
                        case 3:
                            buildingType = 0x27;
                            break;

                        case 4:
                        case 5:
                            buildingType = 40;
                            break;

                        case 6:
                            buildingType = 0x4c;
                            break;

                        case 7:
                        case 8:
                        case 9:
                            buildingType = 0x4d;
                            break;
                    }
                }
                else if (buildingType == 0)
                {
                    int num2 = GameEngine.Instance.World.getRank();
                    if (num2 < 10)
                    {
                        num = 0;
                    }
                    else if (num2 < 15)
                    {
                        num = 3;
                    }
                    else if (num2 < 0x15)
                    {
                        num = 6;
                    }
                    else
                    {
                        num = 6;
                    }
                }
                int[] numArray = VillageBuildingsData.getBuildingLayout(s_villageBuildingData[buildingType].size);
                for (int i = 0; i < (numArray.Length / 2); i++)
                {
                    int index = location.X + numArray[i * 2];
                    int num5 = location.Y + numArray[(i * 2) + 1];
                    if (((index >= 0) && (num5 >= 0)) && ((index < 0x40) && (num5 < 0x80)))
                    {
                        this.layout.mapData[num5][index] |= 0x4000;
                    }
                }
                if (s_villageBuildingData[buildingType].baseGfxTexID >= 0)
                {
                    PointF center = new PointF {
                        X = s_villageBuildingData[buildingType].baseOffset.X,
                        Y = s_villageBuildingData[buildingType].baseOffset.Y
                    };
                    if (s_villageBuildingData[buildingType].shadowGfxTexID >= 0)
                    {
                        newBuilding.shadowSprite = new SpriteWrapper();
                        newBuilding.shadowSprite.TextureID = s_villageBuildingData[buildingType].shadowGfxTexID;
                        newBuilding.shadowSprite.Initialize(this.gfx);
                        newBuilding.shadowSprite.PosX = newBuilding.buildingLocation.X * 0x20;
                        newBuilding.shadowSprite.PosY = (newBuilding.buildingLocation.Y * 0x10) + 8;
                        newBuilding.shadowSprite.SpriteNo = s_villageBuildingData[buildingType].shadowGfxID + num;
                        newBuilding.shadowSprite.Center = center;
                        this.addChildSprite(newBuilding.shadowSprite);
                    }
                    villageClickMask.addBuilding(newBuilding.buildingID, newBuilding.buildingLocation.X * 0x20, (newBuilding.buildingLocation.Y * 0x10) + 8, s_villageBuildingData[buildingType].baseGfxTexID, s_villageBuildingData[buildingType].baseGfxID + num, center);
                    newBuilding.baseSprite = new SpriteWrapper();
                    newBuilding.baseSprite.TextureID = s_villageBuildingData[buildingType].baseGfxTexID;
                    newBuilding.baseSprite.Initialize(this.gfx);
                    newBuilding.baseSprite.SpriteNo = s_villageBuildingData[buildingType].baseGfxID + num;
                    newBuilding.baseSprite.DrawChildrenWithParent = true;
                    newBuilding.baseSprite.Center = center;
                    if (newBuilding.shadowSprite != null)
                    {
                        newBuilding.baseSprite.PosX = 0f;
                        newBuilding.baseSprite.PosY = 0f;
                        newBuilding.shadowSprite.AddChild(newBuilding.baseSprite, 5);
                    }
                    else
                    {
                        newBuilding.baseSprite.PosX = newBuilding.buildingLocation.X * 0x20;
                        newBuilding.baseSprite.PosY = (newBuilding.buildingLocation.Y * 0x10) + 8;
                        this.addChildSprite(newBuilding.baseSprite, 6);
                    }
                    if ((s_villageBuildingData[buildingType].animGfxTexID >= 0) && s_villageBuildingData[buildingType].hasAnim)
                    {
                        newBuilding.animSprite = new SpriteWrapper();
                        newBuilding.animSprite.TextureID = s_villageBuildingData[buildingType].animGfxTexID;
                        newBuilding.animSprite.Initialize(this.gfx);
                        newBuilding.animSprite.PosX = 0f;
                        newBuilding.animSprite.PosY = 0f;
                        if (s_villageBuildingData[buildingType].animArray == null)
                        {
                            newBuilding.animSprite.initAnim(s_villageBuildingData[buildingType].animGfxID, s_villageBuildingData[buildingType].animCount, s_villageBuildingData[buildingType].animStride, s_villageBuildingData[buildingType].animRate);
                        }
                        else
                        {
                            newBuilding.animSprite.initAnim(s_villageBuildingData[buildingType].animGfxID, s_villageBuildingData[buildingType].animArray, s_villageBuildingData[buildingType].animRate);
                        }
                        newBuilding.animSprite.randomizeAnimStart();
                        PointF tf2 = new PointF {
                            X = s_villageBuildingData[buildingType].animOffset.X,
                            Y = s_villageBuildingData[buildingType].animOffset.Y
                        };
                        newBuilding.animSprite.Center = tf2;
                        newBuilding.baseSprite.AddChild(newBuilding.animSprite);
                        if (s_villageBuildingData[buildingType].animOnOpenOnly)
                        {
                            newBuilding.animSprite.Visible = false;
                        }
                    }
                    newBuilding.symbolSprite = new SpriteWrapper();
                    newBuilding.symbolSprite.TextureID = GFXLibrary.Instance.Bld_Various_01TexID;
                    newBuilding.symbolSprite.Initialize(this.gfx);
                    newBuilding.symbolSprite.Visible = false;
                    newBuilding.symbolSprite.SpriteNo = 0x3a;
                    newBuilding.updateSymbolGFX();
                    newBuilding.symbolSprite.PosX = 0f;
                    int num6 = VillageBuildingsData.getBuildingYSize(s_villageBuildingData[buildingType].size);
                    if ((VillageBuildingsData.getBuildingXSize(s_villageBuildingData[buildingType].size) & 1) == 1)
                    {
                        newBuilding.symbolSprite.PosX = 16f;
                    }
                    newBuilding.symbolSprite.PosY = -(num6 * 0x10);
                    newBuilding.symbolSprite.DrawChildrenWithParent = true;
                    newBuilding.symbolSprite.AutoCentre = true;
                    newBuilding.baseSprite.AddChild(newBuilding.symbolSprite);
                }
                newBuilding.productionSprite = new SpriteWrapper();
                newBuilding.productionSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
                newBuilding.productionSprite.Initialize(this.gfx);
                newBuilding.productionSprite.SpriteNo = 0x5f;
                newBuilding.productionSprite.PosX = 0f;
                newBuilding.productionSprite.PosY = -50f;
                newBuilding.productionSprite.Visible = false;
                newBuilding.productionSprite.DrawChildrenWithParent = true;
                newBuilding.productionSprite.AutoCentre = true;
                if ((newBuilding.baseSprite != null) && (newBuilding.productionSprite != null))
                {
                    newBuilding.baseSprite.AddChild(newBuilding.productionSprite);
                }
                if (buildingType == 14)
                {
                    this.createWindmill(newBuilding);
                }
                if (buildingType == 3)
                {
                    this.CreateAnimals(newBuilding);
                }
                this.localBuildings.Add(newBuilding);
            }
            catch (Exception)
            {
            }
        }

        public void addCaptainBack()
        {
            this.m_numCaptains++;
        }

        public void addChildSprite(SpriteWrapper sprite)
        {
            if (this.backgroundSprite != null)
            {
                this.removeChildSprite(sprite);
                this.backgroundSprite.AddChild(sprite);
            }
        }

        public void addChildSprite(SpriteWrapper sprite, int layerDiff)
        {
            if (this.backgroundSprite != null)
            {
                this.removeChildSprite(sprite);
                this.backgroundSprite.AddChild(sprite, layerDiff);
            }
        }

        public void addResources(int resource, int amount)
        {
            switch (resource)
            {
                case 6:
                    this.m_woodLevel += amount;
                    return;

                case 7:
                    this.m_stoneLevel += amount;
                    return;

                case 8:
                    this.m_ironLevel += amount;
                    return;

                case 9:
                    this.m_pitchLevel += amount;
                    return;

                case 10:
                case 11:
                case 20:
                case 0x1b:
                    break;

                case 12:
                    this.m_aleLevel += amount;
                    break;

                case 13:
                    this.m_applesLevel += amount;
                    return;

                case 14:
                    this.m_breadLevel += amount;
                    return;

                case 15:
                    this.m_vegLevel += amount;
                    return;

                case 0x10:
                    this.m_meatLevel += amount;
                    return;

                case 0x11:
                    this.m_cheeseLevel += amount;
                    return;

                case 0x12:
                    this.m_fishLevel += amount;
                    return;

                case 0x13:
                    this.m_clothesLevel += amount;
                    return;

                case 0x15:
                    this.m_furnitureLevel += amount;
                    return;

                case 0x16:
                    this.m_venisonLevel += amount;
                    return;

                case 0x17:
                    this.m_saltLevel += amount;
                    return;

                case 0x18:
                    this.m_spicesLevel += amount;
                    return;

                case 0x19:
                    this.m_silkLevel += amount;
                    return;

                case 0x1a:
                    this.m_metalwareLevel += amount;
                    return;

                case 0x1c:
                    this.m_pikesLevel += amount;
                    return;

                case 0x1d:
                    this.m_bowsLevel += amount;
                    return;

                case 30:
                    this.m_swordsLevel += amount;
                    return;

                case 0x1f:
                    this.m_armourLevel += amount;
                    return;

                case 0x20:
                    this.m_catapultsLevel += amount;
                    return;

                case 0x21:
                    this.m_wineLevel += amount;
                    return;

                default:
                    return;
            }
        }

        public void addTraders(int num, long traderID)
        {
            if (this.findBuildingType(0x4e) != null)
            {
                this.m_numTradersAtHome += num;
            }
            foreach (MarketTraderData data in this.traders)
            {
                if (data.traderID == traderID)
                {
                    this.traders.Remove(data);
                    break;
                }
            }
        }

        public void addTroops(int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults)
        {
            this.addTroops(numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, 0);
        }

        public void addTroops(int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts)
        {
            this.addTroops(numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, 0);
        }

        public void addTroops(int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, int numCaptains)
        {
            this.m_numPeasants += numPeasants;
            this.m_numArchers += numArchers;
            this.m_numPikemen += numPikemen;
            this.m_numSwordsmen += numSwordsmen;
            this.m_numCatapults += numCatapults;
            this.m_numScouts += numScouts;
            this.m_numCaptains += numCaptains;
        }

        public void addTroopsArmyReturnSpecial(int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, int numCaptains)
        {
            this.m_numPeasants += numPeasants;
            this.m_numArchers += numArchers;
            this.m_numPikemen += numPikemen;
            this.m_numSwordsmen += numSwordsmen;
            this.m_numCatapults += numCatapults;
            this.m_numCaptains += numCaptains;
            if (numScouts > 0)
            {
                int num = ResearchData.scoutResearchScoutsLevels[GameEngine.Instance.World.userResearchData.Research_Scouts];
                if ((this.m_numScouts + numScouts) <= num)
                {
                    this.m_numScouts += numScouts;
                }
            }
        }

        public void addVassalTroops(int numPeasants, int numArchers, int numPikemen, int numSwordsmen)
        {
            this.m_numStationedPeasants += numPeasants;
            this.m_numStationedArchers += numArchers;
            this.m_numStationedPikemen += numPikemen;
            this.m_numStationedSwordsmen += numSwordsmen;
        }

        public bool allowTutorialAdvance()
        {
            if (!GameEngine.Instance.World.TutorialIsAdvancing())
            {
                switch (GameEngine.Instance.World.getTutorialStage())
                {
                    case 2:
                        if (this.findBuildingType(13) == null)
                        {
                            break;
                        }
                        return true;

                    case 3:
                    {
                        VillageMapBuilding building2 = this.findBuildingType(6);
                        VillageMapBuilding building3 = this.findBuildingType(7);
                        if ((building2 == null) || (building3 == null))
                        {
                            break;
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public void buildingActiveCallback(VillageBuildingSetActive_ReturnType returnData)
        {
            this.inSendBuildingActivity = false;
            InterfaceMgr.Instance.stopIndustryEnabled();
            if (returnData.Success)
            {
                VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
                if (map != null)
                {
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    if (returnData.villageBuildingsChanged != null)
                    {
                        map.importVillageBuildings(returnData.villageBuildingsChanged, false);
                    }
                }
                setServerTime(returnData.currentTime);
            }
        }

        public void buildingPlacedCallback(PlaceVillageBuilding_ReturnType returnData)
        {
            this.inPlaceBuilding = false;
            VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
            if (map != null)
            {
                if (returnData.Success)
                {
                    map.removeBuildingFromMap((Point) returnData.buildingLocation, returnData.buildingType, -1L);
                    VillageMapBuilding newBuilding = new VillageMapBuilding();
                    newBuilding.createFromReturnData(returnData.villageBuilding);
                    map.addBuildingToMap(newBuilding, (Point) returnData.buildingLocation, returnData.buildingType);

                    DataExport.buildingPlacedCallback(returnData);

                    newBuilding.initStorageBuilding(this.gfx, this);
                    setServerTime(returnData.currentTime);
                    newBuilding.updateConstructionGFX(localBaseTime, baseServerTime, true, this);
                    newBuilding.updateSymbolGFX();
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                    GameEngine.Instance.World.setPoints(returnData.currentPoints);
                    if (returnData.m_cardData != null)
                    {
                        GameEngine.Instance.World.UserCardData = returnData.m_cardData;
                    }
                    InterfaceMgr.Instance.updateSidepanelAfterBuildingPlaced();
                }
                else
                {
                    map.removeBuildingFromMap((Point) returnData.buildingLocation, returnData.buildingType, -1L);
                    switch (returnData.m_errorCode)
                    {
                        case ErrorCodes.ErrorCode.VILLAGE_BUILDINGS_NO_LONGER_OWNER:
                            GameEngine.Instance.displayedVillageLost(this.m_villageID, true);
                            break;
                    }
                }
                map.startPlaceBuilding_ShowPanel(returnData.buildingType, "", false);
            }
        }

        public VillageLayoutNew buildMoveBuildingLayout()
        {
            if (m_movingBuilding == null)
            {
                return null;
            }
            VillageLayoutNew new2 = null;
            new2 = villageLayout[this.m_mapID].createClone();
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if (m_movingBuilding.buildingID != building.buildingID)
                {
                    int[] numArray = VillageBuildingsData.getBuildingLayout(s_villageBuildingData[building.buildingType].size);
                    for (int i = 0; i < (numArray.Length / 2); i++)
                    {
                        int index = building.buildingLocation.X + numArray[i * 2];
                        int num3 = building.buildingLocation.Y + numArray[(i * 2) + 1];
                        if (((index >= 0) && (num3 >= 0)) && ((index < 0x40) && (num3 < 0x80)))
                        {
                            new2.mapData[num3][index] |= 0x4000;
                        }
                    }
                }
            }
            return new2;
        }

        public int calcParishCapitalTaxIncome()
        {
            int baseTaxForAreaCounty = 0;
            if ((this.m_parishPeople != null) && (this.m_parishPeople.Length > 0))
            {
                WorldData localWorldData = GameEngine.Instance.LocalWorldData;
                foreach (ParishTaxCalc calc in this.m_parishPeople)
                {
                    calc.tax = (localWorldData.ranks_Tax[calc.rank] * this.m_capitalTaxRate) * calc.numVillages;
                    if (calc.gold < calc.tax)
                    {
                        calc.tax = calc.gold;
                    }
                    baseTaxForAreaCounty += calc.tax;
                }
                return baseTaxForAreaCounty;
            }
            if (GameEngine.Instance.World.isCountyCapital(this.VillageID))
            {
                baseTaxForAreaCounty = GameEngine.Instance.LocalWorldData.BaseTaxForAreaCounty;
            }
            else if (GameEngine.Instance.World.isProvinceCapital(this.VillageID))
            {
                baseTaxForAreaCounty = GameEngine.Instance.LocalWorldData.BaseTaxForAreaProvince;
            }
            else if (GameEngine.Instance.World.isCountryCapital(this.VillageID))
            {
                baseTaxForAreaCounty = GameEngine.Instance.LocalWorldData.BaseTaxForAreaCountry;
            }
            return (baseTaxForAreaCounty * (this.m_numOfActiveChildrenAreas * this.m_capitalTaxRate));
        }

        public int calcParishVillageTax()
        {
            int index = GameEngine.Instance.World.getRank();
            return (GameEngine.Instance.LocalWorldData.ranks_Tax[index] * this.m_capitalTaxRate);
        }

        public int calcResourceLevel(VillageMapBuilding building, double localTimeLapsed)
        {
            int lastDataLevel = (int) building.lastDataLevel;
            if ((building.calcRate != 0.0) && building.complete)
            {
                TimeSpan span = (TimeSpan) (baseServerTime - building.lastCalcTime);
                double num2 = (localTimeLapsed + span.TotalSeconds) + building.serverJourneyTime;
                double num3 = num2 / building.calcRate;
                int num4 = (int) num3;
                double num5 = CardTypes.adjustPayloadSize(GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(building.buildingType), building.buildingType);
                int num6 = (int) (num4 * num5);
                lastDataLevel += num6;
            }
            return lastDataLevel;
        }

        public int calcTotalMonks()
        {
            int athome = 0;
            return GameEngine.Instance.World.countVillagePeople(this.VillageID, 4, ref athome);
        }

        public int calcTotalMonksAtHome()
        {
            int athome = 0;
            GameEngine.Instance.World.countVillagePeople(this.VillageID, 4, ref athome);
            return athome;
        }

        public int calcTotalScouts()
        {
            return (this.m_numScouts + GameEngine.Instance.World.countYourArmyScouts(this.VillageID));
        }

        public int calcTotalScoutsAtHome()
        {
            return this.m_numScouts;
        }

        public int calcTotalTraders()
        {
            return this.numTraders();
        }

        public int calcTotalTradersAtHome()
        {
            return this.numFreeTraders();
        }

        public int calcTotalTroops()
        {
            int num = (((this.m_numArchers + this.m_numPeasants) + this.m_numPikemen) + this.m_numSwordsmen) + this.m_numCatapults;
            num += GameEngine.Instance.World.countYourArmyTroops(this.VillageID);
            num += GameEngine.Instance.World.countYourReinforcementTroops(this.VillageID);
            num += this.m_numCaptains;
            num += GameEngine.Instance.World.countYourArmyCaptains(this.VillageID);
            CastleMap castle = GameEngine.Instance.Castle;
            if (castle != null)
            {
                num += castle.countOwnPlacedTroops();
            }
            return num;
        }

        public int calcUnitUsages()
        {
            int num = this.calcTotalTroops() + (this.calcTotalScouts() * GameEngine.Instance.LocalWorldData.UnitSize_Scout);
            num += this.calcTotalTraders() * GameEngine.Instance.LocalWorldData.UnitSize_Trader;
            return (num + (this.calcTotalMonks() * GameEngine.Instance.LocalWorldData.UnitSize_Priests));
        }

        public void cancelDeleteBuilding(VillageMapBuilding building)
        {
            if (!GameEngine.Instance.World.isCapital(this.m_villageID))
            {
                if (building == null)
                {
                    villageClickMask.mapDirty = true;
                }
                else if (building.isDeleting())
                {
                    RemoteServices.Instance.set_CancelDeleteVillageBuilding_UserCallBack(new RemoteServices.CancelDeleteVillageBuilding_UserCallBack(this.cancelDeleteBuildingCallback));
                    RemoteServices.Instance.CancelDeleteVillageBuilding(this.m_villageID, building.buildingID);
                    building.serverDeleting = false;
                    building.baseSprite.ColorToUse = ARGBColors.White;
                    building.baseSprite.clearText();
                    building.baseSprite.clearSecondText();
                    if (building.animSprite != null)
                    {
                        building.animSprite.ColorToUse = ARGBColors.White;
                    }
                }
            }
        }

        public void cancelDeleteBuildingCallback(CancelDeleteVillageBuilding_ReturnType returnData)
        {
            if (returnData.Success)
            {
                VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
                if (map != null)
                {
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    if (returnData.villageBuildingsChanged != null)
                    {
                        map.importVillageBuildings(returnData.villageBuildingsChanged, false);
                    }
                }
                GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
                GameEngine.Instance.World.setPoints(returnData.currentPoints);
            }
        }

        public double capResource(int buildingType, double level)
        {
            return GameEngine.Instance.World.UserResearchData.capResource(GameEngine.Instance.LocalWorldData, buildingType, level, GameEngine.Instance.World.UserCardData, GameEngine.Instance.World.isCapital(this.m_villageID));
        }

        public void changeBuildngActivity(VillageMapBuilding building, int mode)
        {
            if (this.inSendBuildingActivity)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.inSendBuildingActivityLastTime);
                if (span.TotalSeconds < 15.0)
                {
                    return;
                }
            }
            this.inSendBuildingActivity = true;
            this.inSendBuildingActivityLastTime = DateTime.Now;
            switch (mode)
            {
                case 0:
                    RemoteServices.Instance.set_VillageBuildingSetActive_UserCallBack(new RemoteServices.VillageBuildingSetActive_UserCallBack(this.buildingActiveCallback));
                    RemoteServices.Instance.VillageBuildingTypeSetActive(this.m_villageID, building.buildingType, false);
                    foreach (VillageMapBuilding building2 in this.localBuildings)
                    {
                        if (building2.buildingType == building.buildingType)
                        {
                            building2.buildingActive = false;
                        }
                    }
                    break;

                case 1:
                    RemoteServices.Instance.set_VillageBuildingSetActive_UserCallBack(new RemoteServices.VillageBuildingSetActive_UserCallBack(this.buildingActiveCallback));
                    RemoteServices.Instance.VillageBuildingTypeSetActive(this.m_villageID, building.buildingType, true);
                    foreach (VillageMapBuilding building3 in this.localBuildings)
                    {
                        if (building3.buildingType == building.buildingType)
                        {
                            building3.buildingActive = true;
                        }
                    }
                    break;

                case 2:
                    RemoteServices.Instance.set_VillageBuildingSetActive_UserCallBack(new RemoteServices.VillageBuildingSetActive_UserCallBack(this.buildingActiveCallback));
                    RemoteServices.Instance.VillageBuildingSetActive(this.m_villageID, building.buildingID, false);
                    building.buildingActive = false;
                    return;

                case 3:
                    RemoteServices.Instance.set_VillageBuildingSetActive_UserCallBack(new RemoteServices.VillageBuildingSetActive_UserCallBack(this.buildingActiveCallback));
                    RemoteServices.Instance.VillageBuildingSetActive(this.m_villageID, building.buildingID, true);
                    building.buildingActive = true;
                    return;

                case 4:
                    RemoteServices.Instance.set_VillageBuildingSetActive_UserCallBack(new RemoteServices.VillageBuildingSetActive_UserCallBack(this.buildingActiveCallback));
                    RemoteServices.Instance.VillageAllBuildingsSetActive(this.m_villageID, false);
                    foreach (VillageMapBuilding building4 in this.localBuildings)
                    {
                        if (building4.buildingType == building.buildingType)
                        {
                            building4.buildingActive = false;
                        }
                    }
                    break;

                case 5:
                    RemoteServices.Instance.set_VillageBuildingSetActive_UserCallBack(new RemoteServices.VillageBuildingSetActive_UserCallBack(this.buildingActiveCallback));
                    RemoteServices.Instance.VillageAllBuildingsSetActive(this.m_villageID, true);
                    foreach (VillageMapBuilding building5 in this.localBuildings)
                    {
                        if (building5.buildingType == building.buildingType)
                        {
                            building5.buildingActive = true;
                        }
                    }
                    break;

                default:
                    return;
            }
        }

        public void changeStats(int taxChange, int rationsChange, int aleChange)
        {
            this.changeStats(taxChange, rationsChange, aleChange, 0);
        }

        public void changeStats(int taxChange, int rationsChange, int aleChange, int capitalTaxChange)
        {
            if (taxChange != 0)
            {
                this.m_taxLevel += taxChange;
                int num = CardTypes.getMaxTaxLevel(GameEngine.Instance.World.UserCardData);
                if (this.m_taxLevel < 0)
                {
                    this.m_taxLevel = 0;
                }
                else if (this.m_taxLevel > num)
                {
                    this.m_taxLevel = num;
                }
            }
            if (rationsChange != 0)
            {
                this.m_rationsLevel += rationsChange;
                if (this.m_rationsLevel < 0)
                {
                    this.m_rationsLevel = 0;
                }
                else if (this.m_rationsLevel > 6)
                {
                    this.m_rationsLevel = 6;
                }
            }
            if (aleChange != 0)
            {
                this.m_aleRationsLevel += aleChange;
                if (this.m_aleRationsLevel < 0)
                {
                    this.m_aleRationsLevel = 0;
                }
                else if (this.m_aleRationsLevel > 4)
                {
                    this.m_aleRationsLevel = 4;
                }
            }
            if (capitalTaxChange != 0)
            {
                this.m_capitalTaxRate += capitalTaxChange;
                if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                {
                    if (this.m_capitalTaxRate < -3)
                    {
                        this.m_capitalTaxRate = -3;
                    }
                    else if (this.m_capitalTaxRate > 50)
                    {
                        this.m_capitalTaxRate = 50;
                    }
                }
                else if (this.m_capitalTaxRate < -3)
                {
                    this.m_capitalTaxRate = -3;
                }
                else if (this.m_capitalTaxRate > 9)
                {
                    this.m_capitalTaxRate = 9;
                }
            }
            this.m_showEffective = true;
            this.m_showAleEffective = true;
            if (((this.m_taxLevel != this.m_taxLevelServer) || (this.m_rationsLevel != this.m_rationsLevelServer)) || ((this.m_aleRationsLevel != this.m_aleRationsLevelServer) || (this.m_capitalTaxRate != this.m_capitalTaxRateServer)))
            {
                this.m_statsChangeTime = DXTimer.GetCurrentMilliseconds();
                if (this.m_rationsLevel != this.m_rationsLevelServer)
                {
                    this.m_showEffective = false;
                }
                if (this.m_aleRationsLevel != this.m_aleRationsLevelServer)
                {
                    this.m_showAleEffective = false;
                }
            }
            else
            {
                this.m_statsChangeTime = 0.0;
            }
            this.showStats();
        }

        public void clearColouredBuildings()
        {
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                building.highlighted = false;
                if (building.isComplete() && !building.isDeleting())
                {
                    building.baseSprite.ColorToUse = ARGBColors.White;
                    if (building.animSprite != null)
                    {
                        building.animSprite.ColorToUse = ARGBColors.White;
                    }
                    if (building.extraAnimSprite1 != null)
                    {
                        building.extraAnimSprite1.ColorToUse = ARGBColors.White;
                    }
                    if (building.extraAnimSprite2 != null)
                    {
                        building.extraAnimSprite2.ColorToUse = ARGBColors.White;
                    }
                    if (building.stockpileExtension != null)
                    {
                        building.stockpileExtension.colorSprites(ARGBColors.White);
                    }
                    if (building.granaryExtension != null)
                    {
                        building.granaryExtension.colorSprites(ARGBColors.White);
                    }
                    if (building.innExtension != null)
                    {
                        building.innExtension.colorSprites(ARGBColors.White);
                    }
                }
                else
                {
                    building.updateConstructionGFX(localBaseTime, baseServerTime, false, this);
                }
            }
        }

        public static void closePopups()
        {
        }

        public int countBuildings()
        {
            return this.localBuildings.Count;
        }

        public int countBuildingType(int buildingType)
        {
            int num = 0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if (building.buildingType == buildingType)
                {
                    num++;
                }
                else
                {
                    switch (buildingType)
                    {
                        case 0x26:
                        case 0x29:
                        case 0x2a:
                        case 0x2b:
                        case 0x2c:
                        case 0x2d:
                            switch (building.buildingType)
                            {
                                case 0x26:
                                case 0x29:
                                case 0x2a:
                                case 0x2b:
                                case 0x2c:
                                case 0x2d:
                                    goto Label_011B;
                            }
                            break;

                        case 0x27:
                        case 40:
                        case 0x4c:
                        case 0x4d:
                        case 1:
                            switch (building.buildingType)
                            {
                                case 0x27:
                                case 40:
                                case 1:
                                case 0x4c:
                                case 0x4d:
                                    goto Label_0253;
                            }
                            break;

                        case 0x2e:
                        case 0x2f:
                        case 0x30:
                            switch (building.buildingType)
                            {
                                case 0x2e:
                                case 0x2f:
                                case 0x30:
                                    goto Label_0147;
                            }
                            break;

                        case 0x31:
                        case 50:
                        case 0x33:
                            switch (building.buildingType)
                            {
                                case 0x31:
                                case 50:
                                case 0x33:
                                    goto Label_0173;
                            }
                            break;

                        case 0x36:
                        case 0x37:
                        case 0x38:
                        case 0x39:
                            switch (building.buildingType)
                            {
                                case 0x36:
                                case 0x37:
                                case 0x38:
                                case 0x39:
                                    goto Label_01F8;
                            }
                            break;

                        case 0x3a:
                        case 0x3b:
                            switch (building.buildingType)
                            {
                                case 0x3a:
                                case 0x3b:
                                    goto Label_021A;
                            }
                            break;

                        case 70:
                        case 0x47:
                        case 0x48:
                        case 0x49:
                            switch (building.buildingType)
                            {
                                case 70:
                                case 0x47:
                                case 0x48:
                                case 0x49:
                                    goto Label_01A3;
                            }
                            break;

                        case 0x4a:
                        case 0x4b:
                            switch (building.buildingType)
                            {
                                case 0x4a:
                                case 0x4b:
                                    goto Label_01CB;
                            }
                            break;
                    }
                }
                continue;
            Label_011B:
                num++;
                continue;
            Label_0147:
                num++;
                continue;
            Label_0173:
                num++;
                continue;
            Label_01A3:
                num++;
                continue;
            Label_01CB:
                num++;
                continue;
            Label_01F8:
                num++;
                continue;
            Label_021A:
                num++;
                continue;
            Label_0253:
                num++;
            }
            return num;
        }

        public int countNumBuildingsConstructing()
        {
            int num = 0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if (!building.isComplete() && !building.isDeleting())
                {
                    num++;
                }
            }
            return num;
        }

        public int countWorkingMarkets()
        {
            int num = 0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if ((building.buildingType == 0x4e) && building.isComplete())
                {
                    num++;
                }
            }
            return num;
        }

        private void CreateAnimals(VillageMapBuilding building)
        {
            if (this.animalArray[building.buildingID] != null)
            {
                VillageAnimalCollection animals = (VillageAnimalCollection) this.animalArray[building.buildingID];
                if (animals != null)
                {
                    foreach (VillageAnimal animal in animals.animals)
                    {
                        animal.recreate(building);
                    }
                }
            }
            else
            {
                switch (building.buildingType)
                {
                    case 3:
                    {
                        VillageAnimalCollection animals4 = new VillageAnimalCollection();
                        for (int i = 0; i < 8; i++)
                        {
                            VillageAnimal item = new VillageAnimal {
                                buildingType = building.buildingType,
                                id = i
                            };
                            animals4.animals.Add(item);
                            item.init(building);
                        }
                        this.animalArray[building.buildingID] = animals4;
                        break;
                    }
                    case 0x10:
                    {
                        VillageAnimalCollection animals2 = new VillageAnimalCollection();
                        for (int j = 0; j < 3; j++)
                        {
                            VillageAnimal animal2 = new VillageAnimal {
                                buildingType = building.buildingType,
                                id = j
                            };
                            animals2.animals.Add(animal2);
                            animal2.init(building);
                        }
                        this.animalArray[building.buildingID] = animals2;
                        break;
                    }
                    case 0x13:
                    {
                        VillageAnimalCollection animals3 = new VillageAnimalCollection();
                        for (int k = 0; k < 5; k++)
                        {
                            VillageAnimal animal3 = new VillageAnimal {
                                buildingType = building.buildingType,
                                id = k
                            };
                            animals3.animals.Add(animal3);
                            animal3.init(building);
                        }
                        this.animalArray[building.buildingID] = animals3;
                        break;
                    }
                }
                if (GameEngine.Instance.Village != null)
                {
                    VillageMap village = GameEngine.Instance.Village;
                    for (int m = 0; m < 50; m++)
                    {
                        this.runAnimals(building, village, 50);
                    }
                }
            }
        }

        public static string createBuildTimeString(int secsLeft)
        {
            int num = secsLeft % 60;
            int num2 = (secsLeft / 60) % 60;
            int num3 = (secsLeft / 0xe10) % 0x18;
            int num4 = secsLeft / 0x15180;
            string str = "";
            if (num4 > 0)
            {
                str = str + num4.ToString() + SK.Text("VillageMap_Day_Abbrev", "d") + ":";
            }
            if (num3 > 0)
            {
                if (num3 < 10)
                {
                    str = str + "0";
                }
                str = str + num3.ToString() + SK.Text("VillageMap_Hour_Abbrev", "h") + ":";
            }
            if (num2 > 0)
            {
                if (num2 < 10)
                {
                    str = str + "0";
                }
                str = str + num2.ToString() + SK.Text("VillageMap_Minute_Abbrev", "m") + ":";
            }
            if ((num < 10) && (secsLeft >= 60))
            {
                str = str + "0";
            }
            return (str + num.ToString() + SK.Text("VillageMap_Second_Abbrev", "s"));
        }

        public static string createBuildTimeStringFull(int secsLeft)
        {
            int num = secsLeft % 60;
            int num2 = (secsLeft / 60) % 60;
            int num3 = (secsLeft / 0xe10) % 0x18;
            int num4 = secsLeft / 0x15180;
            string str = "";
            if (num4 > 0)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, num4.ToString(), SK.Text("VillageMap_Day_Abbrev", "d"), '\x00a0' });
            }
            if ((num3 > 0) || (num4 > 0))
            {
                if (num3 < 10)
                {
                    str = str + "0";
                }
                object obj3 = str;
                str = string.Concat(new object[] { obj3, num3.ToString(), SK.Text("VillageMap_Hour_Abbrev", "h"), '\x00a0' });
            }
            if (((num2 > 0) || (num3 > 0)) || (num4 > 0))
            {
                if (num2 < 10)
                {
                    str = str + "0";
                }
                object obj4 = str;
                str = string.Concat(new object[] { obj4, num2.ToString(), SK.Text("VillageMap_Minute_Abbrev", "m"), '\x00a0' });
            }
            if ((num < 10) && (secsLeft >= 60))
            {
                str = str + "0";
            }
            return (str + num.ToString() + SK.Text("VillageMap_Second_Abbrev", "s"));
        }

        public void createSurroundSprites()
        {
            if (this.backgroundSprite != null)
            {
                int viewportWidth = this.gfx.ViewportWidth;
                int viewportHeight = this.gfx.ViewportHeight;
                int width = (int) this.backgroundSprite.Width;
                int height = (int) this.backgroundSprite.Height;
                tutorialOverlaySprite.Initialize(this.gfx, GFXLibrary.Instance.TutorialIconNormalID, 0);
                tutorialOverlaySprite.Layer = 0x13;
                tutorialOverlaySprite.Center = new PointF(0f, 0f);
                tutorialOverlaySprite.PosX = 0f;
                tutorialOverlaySprite.PosY = viewportHeight - 0x40;
                tutorialOverlaySprite.Update();
                wikiHelpSprite.Initialize(this.gfx, GFXLibrary.Instance.WikiHelpIconNormal, 0);
                wikiHelpSprite.Layer = 0x13;
                wikiHelpSprite.Center = new PointF(0f, 0f);
                wikiHelpSprite.PosX = viewportWidth - 0x1f;
                wikiHelpSprite.PosY = 0f;
                wikiHelpSprite.Scale = 0.66f;
                wikiHelpSprite.Update();
                int num5 = 0x11;
                surroundsprites.Clear();
                if ((width < viewportWidth) && (height < viewportHeight))
                {
                    int num7;
                    int num11;
                    int num6 = (viewportHeight - height) / 2;
                    for (num7 = num6; num7 > 0; num7 -= 0x200)
                    {
                        for (int i = 0; i < viewportWidth; i += 0x200)
                        {
                            SpriteWrapper wrapper = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper.Initialize(this.gfx);
                            wrapper.Layer = num5;
                            wrapper.PosX = i;
                            wrapper.PosY = num7 - 0x200;
                            wrapper.Update();
                            surroundsprites.Add(wrapper);
                        }
                    }
                    for (num7 = ((viewportHeight - height) / 2) + height; num7 < viewportHeight; num7 += 0x200)
                    {
                        for (int j = 0; j < viewportWidth; j += 0x200)
                        {
                            SpriteWrapper wrapper2 = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper2.Initialize(this.gfx);
                            wrapper2.Layer = num5;
                            wrapper2.PosX = j;
                            wrapper2.PosY = num7;
                            wrapper2.Update();
                            surroundsprites.Add(wrapper2);
                        }
                    }
                    int num10 = (viewportWidth - width) / 2;
                    for (num11 = num10; num11 > 0; num11 -= 0x200)
                    {
                        for (int k = 0; k < viewportHeight; k += 0x200)
                        {
                            SpriteWrapper wrapper3 = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper3.Initialize(this.gfx);
                            wrapper3.Layer = num5;
                            wrapper3.PosX = num11 - 0x200;
                            wrapper3.PosY = k;
                            wrapper3.Update();
                            surroundsprites.Add(wrapper3);
                        }
                    }
                    for (num11 = ((viewportWidth - width) / 2) + width; num11 < viewportWidth; num11 += 0x200)
                    {
                        for (int m = 0; m < viewportHeight; m += 0x200)
                        {
                            SpriteWrapper wrapper4 = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper4.Initialize(this.gfx);
                            wrapper4.Layer = num5;
                            wrapper4.PosX = num11;
                            wrapper4.PosY = m;
                            wrapper4.Update();
                            surroundsprites.Add(wrapper4);
                        }
                    }
                    SpriteWrapper item = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    item.Initialize(this.gfx);
                    item.Layer = num5 + 1;
                    item.PosX = num10 - 3;
                    item.PosY = num6 - 3;
                    item.Size = (SizeF) new Size(3, height + 6);
                    item.Update();
                    surroundsprites.Add(item);
                    SpriteWrapper wrapper6 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper6.Initialize(this.gfx);
                    wrapper6.Layer = num5 + 1;
                    wrapper6.PosX = num10 + width;
                    wrapper6.PosY = num6;
                    wrapper6.Size = (SizeF) new Size(3, height);
                    wrapper6.Update();
                    surroundsprites.Add(wrapper6);
                    SpriteWrapper wrapper7 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper7.Initialize(this.gfx);
                    wrapper7.Layer = num5 + 1;
                    wrapper7.PosX = num10 + width;
                    wrapper7.PosY = num6 + 3;
                    wrapper7.Size = (SizeF) new Size(6, height);
                    wrapper7.Update();
                    surroundsprites.Add(wrapper7);
                    SpriteWrapper wrapper8 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper8.Initialize(this.gfx);
                    wrapper8.Layer = num5 + 1;
                    wrapper8.PosX = num10 + width;
                    wrapper8.PosY = num6 + 6;
                    wrapper8.Size = (SizeF) new Size(9, height);
                    wrapper8.Update();
                    surroundsprites.Add(wrapper8);
                    SpriteWrapper wrapper9 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper9.Initialize(this.gfx);
                    wrapper9.Layer = num5 + 1;
                    wrapper9.PosX = num10 + width;
                    wrapper9.PosY = num6 + 9;
                    wrapper9.Size = (SizeF) new Size(14, height);
                    wrapper9.Update();
                    surroundsprites.Add(wrapper9);
                    SpriteWrapper wrapper10 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper10.Initialize(this.gfx);
                    wrapper10.Layer = num5 + 1;
                    wrapper10.PosY = num6 - 3;
                    wrapper10.PosX = num10;
                    wrapper10.Size = (SizeF) new Size(width, 3);
                    wrapper10.Update();
                    surroundsprites.Add(wrapper10);
                    SpriteWrapper wrapper11 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper11.Initialize(this.gfx);
                    wrapper11.Layer = num5 + 1;
                    wrapper11.PosY = num6 + height;
                    wrapper11.PosX = num10;
                    wrapper11.Size = (SizeF) new Size(width, 3);
                    wrapper11.Update();
                    surroundsprites.Add(wrapper11);
                    SpriteWrapper wrapper12 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper12.Initialize(this.gfx);
                    wrapper12.Layer = num5 + 1;
                    wrapper12.PosY = num6 + height;
                    wrapper12.PosX = num10 + 3;
                    wrapper12.Size = (SizeF) new Size(width, 6);
                    wrapper12.Update();
                    surroundsprites.Add(wrapper12);
                    SpriteWrapper wrapper13 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper13.Initialize(this.gfx);
                    wrapper13.Layer = num5 + 1;
                    wrapper13.PosY = num6 + height;
                    wrapper13.PosX = num10 + 6;
                    wrapper13.Size = (SizeF) new Size(width, 9);
                    wrapper13.Update();
                    surroundsprites.Add(wrapper13);
                    SpriteWrapper wrapper14 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper14.Initialize(this.gfx);
                    wrapper14.Layer = num5 + 1;
                    wrapper14.PosY = num6 + height;
                    wrapper14.PosX = num10 + 9;
                    wrapper14.Size = (SizeF) new Size(width, 14);
                    wrapper14.Update();
                    surroundsprites.Add(wrapper14);
                }
                else if (width < viewportWidth)
                {
                    int num14 = (viewportWidth - width) / 2;
                    int num15 = num14;
                    while (num14 > 0)
                    {
                        for (int n = 0; n < viewportHeight; n += 0x200)
                        {
                            SpriteWrapper wrapper15 = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper15.Initialize(this.gfx);
                            wrapper15.Layer = num5;
                            wrapper15.PosX = num14 - 0x200;
                            wrapper15.PosY = n;
                            wrapper15.Update();
                            surroundsprites.Add(wrapper15);
                        }
                        num14 -= 0x200;
                    }
                    SpriteWrapper wrapper16 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper16.Initialize(this.gfx);
                    wrapper16.Layer = num5 + 1;
                    wrapper16.PosX = num15 - 3;
                    wrapper16.PosY = 0f;
                    wrapper16.Size = (SizeF) new Size(3, height);
                    wrapper16.Update();
                    surroundsprites.Add(wrapper16);
                    for (num14 = ((viewportWidth - width) / 2) + width; num14 < viewportWidth; num14 += 0x200)
                    {
                        for (int num17 = 0; num17 < viewportHeight; num17 += 0x200)
                        {
                            SpriteWrapper wrapper17 = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper17.Initialize(this.gfx);
                            wrapper17.Layer = num5;
                            wrapper17.PosX = num14;
                            wrapper17.PosY = num17;
                            wrapper17.Update();
                            surroundsprites.Add(wrapper17);
                        }
                    }
                    SpriteWrapper wrapper18 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper18.Initialize(this.gfx);
                    wrapper18.Layer = num5 + 1;
                    wrapper18.PosX = num15 + width;
                    wrapper18.PosY = 0f;
                    wrapper18.Size = (SizeF) new Size(3, height);
                    wrapper18.Update();
                    surroundsprites.Add(wrapper18);
                    SpriteWrapper wrapper19 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper19.Initialize(this.gfx);
                    wrapper19.Layer = num5 + 1;
                    wrapper19.PosX = num15 + width;
                    wrapper19.PosY = 0f;
                    wrapper19.Size = (SizeF) new Size(6, height);
                    wrapper19.Update();
                    surroundsprites.Add(wrapper19);
                    SpriteWrapper wrapper20 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper20.Initialize(this.gfx);
                    wrapper20.Layer = num5 + 1;
                    wrapper20.PosX = num15 + width;
                    wrapper20.PosY = 0f;
                    wrapper20.Size = (SizeF) new Size(9, height);
                    wrapper20.Update();
                    surroundsprites.Add(wrapper20);
                    SpriteWrapper wrapper21 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper21.Initialize(this.gfx);
                    wrapper21.Layer = num5 + 1;
                    wrapper21.PosX = num15 + width;
                    wrapper21.PosY = 0f;
                    wrapper21.Size = (SizeF) new Size(14, height);
                    wrapper21.Update();
                    surroundsprites.Add(wrapper21);
                }
                else if (height < viewportHeight)
                {
                    int num18 = (viewportHeight - height) / 2;
                    int num19 = num18;
                    while (num18 > 0)
                    {
                        for (int num20 = 0; num20 < viewportWidth; num20 += 0x200)
                        {
                            SpriteWrapper wrapper22 = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper22.Initialize(this.gfx);
                            wrapper22.Layer = num5;
                            wrapper22.PosX = num20;
                            wrapper22.PosY = num18 - 0x200;
                            wrapper22.Update();
                            surroundsprites.Add(wrapper22);
                        }
                        num18 -= 0x200;
                    }
                    SpriteWrapper wrapper23 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper23.Initialize(this.gfx);
                    wrapper23.Layer = num5 + 1;
                    wrapper23.PosY = num19 - 3;
                    wrapper23.PosX = 0f;
                    wrapper23.Size = (SizeF) new Size(width, 3);
                    wrapper23.Update();
                    surroundsprites.Add(wrapper23);
                    for (num18 = ((viewportHeight - height) / 2) + height; num18 < viewportHeight; num18 += 0x200)
                    {
                        for (int num21 = 0; num21 < viewportWidth; num21 += 0x200)
                        {
                            SpriteWrapper wrapper24 = new SpriteWrapper {
                                TextureID = GFXLibrary.Instance.ImageSurroundTexID3
                            };
                            wrapper24.Initialize(this.gfx);
                            wrapper24.Layer = num5;
                            wrapper24.PosX = num21;
                            wrapper24.PosY = num18;
                            wrapper24.Update();
                            surroundsprites.Add(wrapper24);
                        }
                    }
                    SpriteWrapper wrapper25 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper25.Initialize(this.gfx);
                    wrapper25.Layer = num5 + 1;
                    wrapper25.PosY = num19 + height;
                    wrapper25.PosX = 0f;
                    wrapper25.Size = (SizeF) new Size(width, 3);
                    wrapper25.Update();
                    surroundsprites.Add(wrapper25);
                    SpriteWrapper wrapper26 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper26.Initialize(this.gfx);
                    wrapper26.Layer = num5 + 1;
                    wrapper26.PosY = num19 + height;
                    wrapper26.PosX = 0f;
                    wrapper26.Size = (SizeF) new Size(width, 6);
                    wrapper26.Update();
                    surroundsprites.Add(wrapper26);
                    SpriteWrapper wrapper27 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper27.Initialize(this.gfx);
                    wrapper27.Layer = num5 + 1;
                    wrapper27.PosY = num19 + height;
                    wrapper27.PosX = 0f;
                    wrapper27.Size = (SizeF) new Size(width, 9);
                    wrapper27.Update();
                    surroundsprites.Add(wrapper27);
                    SpriteWrapper wrapper28 = new SpriteWrapper {
                        TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID
                    };
                    wrapper28.Initialize(this.gfx);
                    wrapper28.Layer = num5 + 1;
                    wrapper28.PosY = num19 + height;
                    wrapper28.PosX = 0f;
                    wrapper28.Size = (SizeF) new Size(width, 14);
                    wrapper28.Update();
                    surroundsprites.Add(wrapper28);
                }
            }
        }

        public void createWindmill(VillageMapBuilding newBuilding)
        {
            newBuilding.extraAnimSprite2 = new SpriteWrapper();
            newBuilding.extraAnimSprite2.TextureID = GFXLibrary.Instance.BakerAnimTexID;
            newBuilding.extraAnimSprite2.Initialize(this.gfx);
            newBuilding.extraAnimSprite2.PosX = 0f;
            newBuilding.extraAnimSprite2.PosY = 0f;
            newBuilding.extraAnimSprite2.initAnim(0x164, 15, 1, 0x4b);
            PointF tf = new PointF {
                X = 74f,
                Y = 318f
            };
            newBuilding.extraAnimSprite2.Center = tf;
            newBuilding.baseSprite.AddChild(newBuilding.extraAnimSprite2);
            newBuilding.extraAnimSprite1 = new SpriteWrapper();
            newBuilding.extraAnimSprite1.TextureID = GFXLibrary.Instance.BakerAnimTexID;
            newBuilding.extraAnimSprite1.Initialize(this.gfx);
            newBuilding.extraAnimSprite1.PosX = 0f;
            newBuilding.extraAnimSprite1.PosY = 0f;
            newBuilding.extraAnimSprite1.initAnim(0x155, 15, 1, 0x4b);
            PointF tf2 = new PointF {
                X = 86f,
                Y = 349f
            };
            newBuilding.extraAnimSprite1.Center = tf2;
            newBuilding.baseSprite.AddChild(newBuilding.extraAnimSprite1);
        }

        public void deleteBuilding(VillageMapBuilding building)
        {
            if (building == null)
            {
                villageClickMask.mapDirty = true;
            }
            else if (!building.isDeleting() && (building.buildingType != 0))
            {
                GameEngine.Instance.playInterfaceSound("Villagemap_Delete_building");
                RemoteServices.Instance.set_DeleteVillageBuilding_UserCallBack(new RemoteServices.DeleteVillageBuilding_UserCallBack(this.deleteBuildingCallback));
                RemoteServices.Instance.DeleteVillageBuilding(this.m_villageID, building.buildingID);
                if (GameEngine.Instance.World.isCapital(this.m_villageID))
                {
                    building.Visible = false;
                }
                else if (!building.isComplete())
                {
                    double localTimeLapsed = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
                    switch (building.buildingType)
                    {
                        case 6:
                            this.m_woodLevel += this.calcResourceLevel(building, localTimeLapsed);
                            break;

                        case 7:
                            this.m_stoneLevel += this.calcResourceLevel(building, localTimeLapsed);
                            break;

                        case 8:
                            this.m_ironLevel += this.calcResourceLevel(building, localTimeLapsed);
                            break;
                    }
                    building.Visible = false;
                }
            }
        }

        public void deleteBuildingCallback(DeleteVillageBuilding_ReturnType returnData)
        {
            VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
            if (map != null)
            {
                if (!returnData.Success)
                {
                    VillageMapBuilding building = map.findBuilding(returnData.buildingID);
                    if (building != null)
                    {
                        building.Visible = true;
                    }
                    if (returnData.m_errorCode == ErrorCodes.ErrorCode.VILLAGE_BUILDINGS_NO_LONGER_OWNER)
                    {
                        GameEngine.Instance.displayedVillageLost(this.m_villageID, true);
                    }
                }
                else
                {
                    bool flag = false;
                    if (returnData.villageBuildingsChanged != null)
                    {
                        foreach (VillageBuildingReturnData data in returnData.villageBuildingsChanged)
                        {
                            if (data.buildingID == returnData.buildingID)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (!flag)
                    {
                        map.removeBuildingFromMap(Point.Empty, returnData.buildingType, returnData.buildingID);
                    }
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    if (returnData.villageBuildingsChanged != null)
                    {
                        map.importVillageBuildings(returnData.villageBuildingsChanged, false);
                    }
                    switch (returnData.buildingType)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 0x23:
                            RemoteServices.Instance.GetVillageBuildingsList(this.m_villageID, false, false);
                            break;
                    }
                    GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                    GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
                    GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
                    GameEngine.Instance.World.setPoints(returnData.currentPoints);
                }
            }
        }

        public void disbandPeople(int peopleType, int amount)
        {
            if (this.disbandPeopleLocked)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.disbandPeopleLockedTime);
                if (span.TotalSeconds <= 45.0)
                {
                    return;
                }
            }
            this.disbandPeopleLockedTime = DateTime.Now;
            this.disbandPeopleLocked = true;
            RemoteServices.Instance.set_DisbandPeople_UserCallBack(new RemoteServices.DisbandPeople_UserCallBack(this.disbandPeopleCallback));
            RemoteServices.Instance.DisbandPeople(this.VillageID, peopleType, amount);
        }

        public void disbandPeopleCallback(DisbandPeople_ReturnType returnData)
        {
            this.disbandPeopleLocked = false;
            if (returnData.Success)
            {
                if (returnData.marketTraders != null)
                {
                    this.importTraders(returnData.marketTraders, returnData.currentTime);
                }
                if (returnData.people != null)
                {
                    GameEngine.Instance.World.importOrphanedPeople(returnData.people, returnData.currentTime, returnData.villageID);
                }
                GameEngine.Instance.forceDownloadCurrentVillage();
            }
        }

        public void disbandTroops(int troopType, int amount)
        {
            if (this.disbandTroopsLocked)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.disbandTroopsLockedTime);
                if (span.TotalSeconds <= 45.0)
                {
                    return;
                }
            }
            this.disbandTroopsLockedTime = DateTime.Now;
            this.disbandTroopsLocked = true;
            RemoteServices.Instance.set_DisbandTroops_UserCallBack(new RemoteServices.DisbandTroops_UserCallBack(this.disbandTroopsCallback));
            RemoteServices.Instance.DisbandTroops(this.VillageID, troopType, amount);
        }

        public void disbandTroopsCallback(DisbandTroops_ReturnType returnData)
        {
            this.disbandTroopsLocked = false;
            if (returnData.Success)
            {
                GameEngine.Instance.forceDownloadCurrentVillage();
            }
        }

        public void dispose()
        {
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                this.removeAnimals(building);
            }
            this.localBuildings.Clear();
            if (this.backgroundSprite != null)
            {
                this.backgroundSprite.RemoveAllChildren();
                this.backgroundSprite = null;
            }
            villageClickMask.clearMap();
        }

        public void drawProductionArrow()
        {
            if (this.productionArrowProductionBuilding.X != -1)
            {
                int num = ((this.productionArrowProductionBuilding.X * 0x20) + (((int) this.backgroundSprite.DrawPos.X) + 0x10)) - 0x10;
                int num2 = (this.productionArrowProductionBuilding.Y * 0x10) + (((int) this.backgroundSprite.DrawPos.Y) + 8);
                int num3 = ((this.productionArrowTargetBuilding.X * 0x20) + (((int) this.backgroundSprite.DrawPos.X) + 0x10)) - 0x10;
                int num4 = (this.productionArrowTargetBuilding.Y * 0x10) + (((int) this.backgroundSprite.DrawPos.Y) + 8);
                PointF point = new PointF((float) (num3 - num), (float) (num4 - num2));
                float num5 = ((float) Math.Sqrt((double) ((point.X * point.X) + (point.Y * point.Y)))) / 15f;
                point.X /= num5;
                point.Y /= num5;
                PointF tf2 = this.gfx.rotatePoint(point, -90);
                PointF tf3 = this.gfx.rotatePoint(point, 90);
                tf2.X += num;
                tf2.Y += num2;
                tf3.X += num;
                tf3.Y += num2;
                Color color = Color.FromArgb(0xc0, 0x80, 0xff, 0x80);
                if (num5 > 50f)
                {
                    color = Color.FromArgb(0xc0, 0xff, 0xc0, 0);
                }
                else if (num5 >= 15f)
                {
                    int num6 = ((((int) num5) - 15) * 0x80) / 0x23;
                    color = Color.FromArgb(0xc0, 0x80 + num6, 0xff - (num6 / 4), 0x80 - num6);
                }
                this.gfx.startPoly();
                this.gfx.addTriangle(Color.FromArgb(0, color), Color.FromArgb(0, color), color, tf2.X, tf2.Y, tf3.X, tf3.Y, num3 - (point.X * 5f), num4 - (point.Y * 5f));
                this.gfx.drawPoly();
            }
        }

        private void drawSurroundSprites()
        {
            foreach (SpriteWrapper wrapper in surroundsprites)
            {
                wrapper.AddToRenderList();
            }
        }

        public VillageMapBuilding findBuilding(long buildingID)
        {
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if (building.buildingID == buildingID)
                {
                    return building;
                }
            }
            return null;
        }

        public VillageMapBuilding findBuildingType(int buildingType)
        {
            if (buildingType == 4)
            {
                this.fakeArmoury.buildingType = 4;
                this.fakeArmoury.buildingLocation = new Point(0x1c, 0);
                return this.fakeArmoury;
            }
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if ((building.buildingType == buildingType) && building.isComplete())
                {
                    return building;
                }
            }
            return null;
        }

        public VillageMapBuilding findBuildingTypeIncludingConstructing(int buildingType)
        {
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if (building.buildingType == buildingType)
                {
                    return building;
                }
            }
            return null;
        }

        public Point findEmptyTile(Point Location, int range, Random rand)
        {
            int num = Location.X - (range / 2);
            if (num < 1)
            {
                num = 1;
            }
            int num2 = Location.X + (range / 2);
            if (num2 >= (this.layout.gridWidth - 2))
            {
                num2 = this.layout.gridWidth - 2;
            }
            int num3 = Location.Y - range;
            if (num3 < 1)
            {
                num3 = 1;
            }
            int num4 = Location.Y + range;
            if (num4 >= (this.layout.gridHeight - 2))
            {
                num4 = this.layout.gridHeight - 2;
            }
            List<Point> list = new List<Point>();
            for (int i = num3; i <= num4; i++)
            {
                for (int j = num; j <= num2; j++)
                {
                    if ((((this.layout.mapData[i][j] == 0) && (this.layout.mapData[i - 1][j] == 0)) && ((this.layout.mapData[i + 1][j] == 0) && (this.layout.mapData[i][j - 1] == 0))) && (this.layout.mapData[i][j + 1] == 0))
                    {
                        Point item = new Point(j, i);
                        list.Add(item);
                    }
                }
            }
            if (list.Count > 0)
            {
                return list[rand.Next(list.Count)];
            }
            return Location;
        }

        private int findRandStateData(VillageMapBuilding building, int data)
        {
            if (this.randStateArray[building.buildingID] != null)
            {
                data = (int) this.randStateArray[building.buildingID];
                building.randState = data;
                return data;
            }
            this.setRandStateData(building, data);
            return data;
        }

        public void forceDirtyMap()
        {
            villageClickMask.clearMap();
        }

        public bool genericBuildingValidation(Point location, int buildingType)
        {
            int num = this.countBuildingType(buildingType);
            int capitalType = GameEngine.Instance.World.getCapitalType(this.m_villageID);
            if (num >= GameEngine.Instance.LocalWorldData.getConstrMaxCount(buildingType, capitalType))
            {
                return false;
            }
            return true;
        }

        public double getAleProductionPerDay()
        {
            double num = 0.0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if (((building.buildingType == 12) && (building.calcRate != 0.0)) && building.complete)
                {
                    double num2 = CardTypes.adjustPayloadSize(GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(building.buildingType), building.buildingType);
                    num += (86400.0 / building.calcRate) * num2;
                }
            }
            return num;
        }

        public bool getArmouryLevels(ArmouryLevels levels)
        {
            levels.bowsLevel = this.m_bowsLevel;
            levels.pikesLevel = this.m_pikesLevel;
            levels.swordsLevel = this.m_swordsLevel;
            levels.armourLevel = this.m_armourLevel;
            levels.catapultsLevel = this.m_catapultsLevel;
            if (((this.m_toBeMade_Bows > 0.0) || (this.m_toBeMade_Pikes > 0.0)) || (((this.m_toBeMade_Swords > 0.0) || (this.m_toBeMade_Armour > 0.0)) || (this.m_toBeMade_Catapults > 0.0)))
            {
                double num = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
                DateTime time = baseServerTime + new TimeSpan(0, 0, (int) num);
                if ((this.m_toBeMade_Bows > 0.0) && (this.m_productionRate_Bows > 0.0))
                {
                    if (time >= this.m_productionEnd_Bows)
                    {
                        levels.bowsLevel += this.m_toBeMade_Bows;
                    }
                    else
                    {
                        TimeSpan span = (TimeSpan) (baseServerTime - this.m_productionStart_Bows);
                        double num2 = span.TotalSeconds + num;
                        double num3 = this.m_productionRate_Bows * num2;
                        levels.bowsLevel += num3;
                        levels.bowsLevel = Math.Floor(levels.bowsLevel);
                        levels.bowsLeftToMake = (int) ((this.m_toBeMade_Bows - num3) + 0.999999);
                    }
                }
                else
                {
                    levels.bowsLeftToMake = (int) this.m_toBeMade_Bows;
                }
                if ((this.m_toBeMade_Pikes > 0.0) && (this.m_productionRate_Pikes > 0.0))
                {
                    if (time >= this.m_productionEnd_Pikes)
                    {
                        levels.pikesLevel += this.m_toBeMade_Pikes;
                    }
                    else
                    {
                        TimeSpan span2 = (TimeSpan) (baseServerTime - this.m_productionStart_Pikes);
                        double num4 = span2.TotalSeconds + num;
                        double num5 = this.m_productionRate_Pikes * num4;
                        levels.pikesLevel += num5;
                        levels.pikesLevel = Math.Floor(levels.pikesLevel);
                        levels.pikesLeftToMake = (int) ((this.m_toBeMade_Pikes - num5) + 0.999999);
                    }
                }
                else
                {
                    levels.pikesLeftToMake = (int) this.m_toBeMade_Pikes;
                }
                if ((this.m_toBeMade_Swords > 0.0) && (this.m_productionRate_Swords > 0.0))
                {
                    if (time >= this.m_productionEnd_Swords)
                    {
                        levels.swordsLevel += this.m_toBeMade_Swords;
                    }
                    else
                    {
                        TimeSpan span3 = (TimeSpan) (baseServerTime - this.m_productionStart_Swords);
                        double num6 = span3.TotalSeconds + num;
                        double num7 = this.m_productionRate_Swords * num6;
                        levels.swordsLevel += num7;
                        levels.swordsLevel = Math.Floor(levels.swordsLevel);
                        levels.swordsLeftToMake = (int) ((this.m_toBeMade_Swords - num7) + 0.999999);
                    }
                }
                else
                {
                    levels.swordsLeftToMake = (int) this.m_toBeMade_Swords;
                }
                if ((this.m_toBeMade_Armour > 0.0) && (this.m_productionRate_Armour > 0.0))
                {
                    if (time >= this.m_productionEnd_Armour)
                    {
                        levels.armourLevel += this.m_toBeMade_Armour;
                    }
                    else
                    {
                        TimeSpan span4 = (TimeSpan) (baseServerTime - this.m_productionStart_Armour);
                        double num8 = span4.TotalSeconds + num;
                        double num9 = this.m_productionRate_Armour * num8;
                        levels.armourLevel += num9;
                        levels.armourLevel = Math.Floor(levels.armourLevel);
                        levels.armourLeftToMake = (int) ((this.m_toBeMade_Armour - num9) + 0.999999);
                    }
                }
                else
                {
                    levels.armourLeftToMake = (int) this.m_toBeMade_Armour;
                }
                if ((this.m_toBeMade_Catapults > 0.0) && (this.m_productionRate_Catapults > 0.0))
                {
                    if (time >= this.m_productionEnd_Catapults)
                    {
                        levels.catapultsLevel += this.m_toBeMade_Catapults;
                    }
                    else
                    {
                        TimeSpan span5 = (TimeSpan) (baseServerTime - this.m_productionStart_Catapults);
                        double num10 = span5.TotalSeconds + num;
                        double num11 = this.m_productionRate_Catapults * num10;
                        levels.catapultsLevel += num11;
                        levels.catapultsLevel = Math.Floor(levels.catapultsLevel);
                        levels.catapultsLeftToMake = (int) ((this.m_toBeMade_Catapults - num11) + 0.999999);
                    }
                }
                else
                {
                    levels.catapultsLeftToMake = (int) this.m_toBeMade_Catapults;
                }
            }
            levels.bowsLevel = this.capResource(0x1d, levels.bowsLevel);
            levels.pikesLevel = this.capResource(0x1c, levels.pikesLevel);
            levels.swordsLevel = this.capResource(30, levels.swordsLevel);
            levels.armourLevel = this.capResource(0x1f, levels.armourLevel);
            levels.catapultsLevel = this.capResource(0x20, levels.catapultsLevel);
            return true;
        }

        public PointF getBackgroundSpritePoint()
        {
            return new PointF(this.backgroundSprite.PosX, this.backgroundSprite.PosY);
        }

        public long getBuildingAtPoint(Point loc)
        {
            long num = villageClickMask.getBuildingIDFromMap(loc.X, loc.Y);
            if (num < 0L)
            {
                if (InterfaceMgr.Instance.isInBuildingPanelOpen())
                {
                    if (!GameEngine.Instance.World.isCapital(this.VillageID))
                    {
                        GameEngine.Instance.playInterfaceSound("VillageMap_select_building_Close");
                    }
                    else
                    {
                        GameEngine.Instance.playInterfaceSound("VillageMap_select_capital_building_Close");
                    }
                }
                InterfaceMgr.Instance.showInBuildingInfo(null);
            }
            return num;
        }

        public VillageMapBuilding getBuildingFromID(long buildingID)
        {
            foreach (VillageMapBuilding building2 in this.localBuildings)
            {
                if (building2.buildingID == buildingID)
                {
                    return building2;
                }
            }
            return null;
        }

        public static DateTime getCurrentServerTime()
        {
            double num = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
            return baseServerTime.AddSeconds(num);
        }

        public double getDistanceThroughCycle(VillageMapBuilding building)
        {
            if ((building.calcRate != 0.0) && building.complete)
            {
                double num = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
                TimeSpan span = (TimeSpan) (baseServerTime - building.lastCalcTime);
                double num2 = num + span.TotalSeconds;
                double num3 = num2 / building.calcRate;
                int num4 = (int) num3;
                return ((num2 - (num4 * building.calcRate)) / building.calcRate);
            }
            return 0.0;
        }

        public double getDistanceThroughCycleSecondary(VillageMapBuilding building)
        {
            if ((building.calcRate == 0.0) || !building.complete)
            {
                return 0.0;
            }
            double num = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
            DateTime now = DateTime.Now;
            switch (building.buildingType)
            {
                case 0x1c:
                    now = this.m_productionEnd_Pikes;
                    break;

                case 0x1d:
                    now = this.m_productionEnd_Bows;
                    break;

                case 30:
                    now = this.m_productionEnd_Swords;
                    break;

                case 0x1f:
                    now = this.m_productionEnd_Armour;
                    break;

                case 0x20:
                    now = this.m_productionEnd_Catapults;
                    break;
            }
            TimeSpan span = (TimeSpan) (now - baseServerTime);
            double num2 = span.TotalSeconds - num;
            double num3 = num2 / building.calcRate;
            int num4 = (int) num3;
            return ((building.calcRate - (num2 - (num4 * building.calcRate))) / building.calcRate);
        }

        public double getFoodProductionPerDay()
        {
            double num = 0.0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                switch (building.buildingType)
                {
                    case 13:
                    case 14:
                    case 15:
                    case 0x10:
                    case 0x11:
                    case 0x12:
                        if ((building.calcRate != 0.0) && building.complete)
                        {
                            double num2 = CardTypes.adjustPayloadSize(GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(building.buildingType), building.buildingType);
                            num += (86400.0 / building.calcRate) * num2;
                        }
                        break;
                }
            }
            return num;
        }

        public bool getGranaryLevels(GranaryLevels levels)
        {
            if (this.findBuildingType(3) == null)
            {
                return false;
            }
            levels.applesLevel = this.m_applesLevel;
            levels.breadLevel = this.m_breadLevel;
            levels.meatLevel = this.m_meatLevel;
            levels.cheeseLevel = this.m_cheeseLevel;
            levels.vegLevel = this.m_vegLevel;
            levels.fishLevel = this.m_fishLevel;
            double localTimeLapsed = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                switch (building.buildingType)
                {
                    case 13:
                        levels.applesLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 14:
                        levels.breadLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 15:
                        levels.vegLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 0x10:
                        levels.meatLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 0x11:
                        levels.cheeseLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 0x12:
                        levels.fishLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;
                }
            }
            TimeSpan span = (TimeSpan) (baseServerTime - this.m_consumptionLastTime);
            double num2 = localTimeLapsed + span.TotalSeconds;
            if (this.m_applesConsumption > 0.0)
            {
                levels.applesLevel -= (1.0 / this.m_applesConsumption) * num2;
            }
            if (this.m_breadConsumption > 0.0)
            {
                levels.breadLevel -= (1.0 / this.m_breadConsumption) * num2;
            }
            if (this.m_cheeseConsumption > 0.0)
            {
                levels.cheeseLevel -= (1.0 / this.m_cheeseConsumption) * num2;
            }
            if (this.m_meatConsumption > 0.0)
            {
                levels.meatLevel -= (1.0 / this.m_meatConsumption) * num2;
            }
            if (this.m_vegConsumption > 0.0)
            {
                levels.vegLevel -= (1.0 / this.m_vegConsumption) * num2;
            }
            if (this.m_fishConsumption > 0.0)
            {
                levels.fishLevel -= (1.0 / this.m_fishConsumption) * num2;
            }
            levels.applesLevel = Math.Floor(levels.applesLevel);
            levels.breadLevel = Math.Floor(levels.breadLevel);
            levels.cheeseLevel = Math.Floor(levels.cheeseLevel);
            levels.meatLevel = Math.Floor(levels.meatLevel);
            levels.vegLevel = Math.Floor(levels.vegLevel);
            levels.fishLevel = Math.Floor(levels.fishLevel);
            if (levels.applesLevel < 0.0)
            {
                levels.applesLevel = 0.0;
            }
            if (levels.breadLevel < 0.0)
            {
                levels.breadLevel = 0.0;
            }
            if (levels.cheeseLevel < 0.0)
            {
                levels.cheeseLevel = 0.0;
            }
            if (levels.meatLevel < 0.0)
            {
                levels.meatLevel = 0.0;
            }
            if (levels.vegLevel < 0.0)
            {
                levels.vegLevel = 0.0;
            }
            if (levels.fishLevel < 0.0)
            {
                levels.fishLevel = 0.0;
            }
            levels.applesLevel = this.capResource(13, levels.applesLevel);
            levels.breadLevel = this.capResource(14, levels.breadLevel);
            levels.meatLevel = this.capResource(0x10, levels.meatLevel);
            levels.cheeseLevel = this.capResource(0x11, levels.cheeseLevel);
            levels.vegLevel = this.capResource(15, levels.vegLevel);
            levels.fishLevel = this.capResource(0x12, levels.fishLevel);
            return true;
        }

        public bool getInnLevels(InnLevels levels)
        {
            if (this.findBuildingType(0x23) == null)
            {
                return false;
            }
            levels.aleLevel = this.m_aleLevel;
            double localTimeLapsed = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if (building.buildingType == 12)
                {
                    levels.aleLevel += this.calcResourceLevel(building, localTimeLapsed);
                }
            }
            TimeSpan span = (TimeSpan) (baseServerTime - this.m_consumptionLastTime);
            double num2 = localTimeLapsed + span.TotalSeconds;
            if (this.m_aleConsumption > 0.0)
            {
                levels.aleLevel -= (1.0 / this.m_aleConsumption) * num2;
            }
            levels.aleLevel = Math.Floor(levels.aleLevel);
            if (levels.aleLevel < 0.0)
            {
                levels.aleLevel = 0.0;
            }
            levels.aleLevel = this.capResource(12, levels.aleLevel);
            return true;
        }

        public double getJourneyTime(Point newStartPos, Point newEndPos)
        {
            Point startPoint = new Point(newStartPos.X, newStartPos.Y);
            Point endPoint = new Point(newEndPos.X, newEndPos.Y);
            startPoint.X *= 0x20;
            startPoint.Y *= 0x10;
            startPoint.Y += 8;
            endPoint.X *= 0x20;
            endPoint.Y *= 0x10;
            endPoint.Y += 8;
            return VillageBuildingsData.calcTravelTime(GameEngine.Instance.LocalWorldData, startPoint, endPoint).TotalSeconds;
        }

        private int getMaxBuildingQueueLength()
        {
            if (!GameEngine.Instance.World.isCapital(this.m_villageID))
            {
                if (GameEngine.Instance.World.isAccountPremium())
                {
                    return GameEngine.Instance.LocalWorldData.buildingQueueMaxLength;
                }
                return 1;
            }
            return GameEngine.Instance.LocalWorldData.capitalBuildingQueueMaxLength;
        }

        public VillageMapBuilding getNextBuilding(VillageMapBuilding building)
        {
            for (int i = 0; i < this.localBuildings.Count; i++)
            {
                if (this.localBuildings[i] == building)
                {
                    if ((i + 1) >= this.localBuildings.Count)
                    {
                        return this.localBuildings[0];
                    }
                    return this.localBuildings[i + 1];
                }
            }
            if (this.localBuildings.Count > 0)
            {
                return this.localBuildings[0];
            }
            return null;
        }

        public int getNumDeleting()
        {
            int num = 0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if (building.serverDeleting)
                {
                    num++;
                }
            }
            return num;
        }

        private int getNumTrips(int buildingType)
        {
            WorldData localWorldData = GameEngine.Instance.LocalWorldData;
            int trips = 1;
            switch (buildingType)
            {
                case 0x1c:
                    trips = localWorldData.pikesBaseProductionTrips;
                    break;

                case 0x1d:
                    trips = localWorldData.bowsBaseProductionTrips;
                    break;

                case 30:
                    trips = localWorldData.swordsBaseProductionTrips;
                    break;

                case 0x1f:
                    trips = localWorldData.armourBaseProductionTrips;
                    break;

                case 0x20:
                    trips = localWorldData.catapultsBaseProductionTrips;
                    break;
            }
            return CardTypes.cards_adjustWeaponProductionTrips(GameEngine.Instance.World.UserCardData, trips, buildingType);
        }

        public VillageMapBuilding getPreviousBuilding(VillageMapBuilding building)
        {
            for (int i = 0; i < this.localBuildings.Count; i++)
            {
                if (this.localBuildings[i] == building)
                {
                    if ((i - 1) < 0)
                    {
                        return this.localBuildings[this.localBuildings.Count - 1];
                    }
                    return this.localBuildings[i - 1];
                }
            }
            if (this.localBuildings.Count > 0)
            {
                return this.localBuildings[0];
            }
            return null;
        }

        public double getResourceLevel(int buildingType)
        {
            switch (buildingType)
            {
                case 6:
                case 7:
                case 8:
                case 9:
                {
                    StockpileLevels levels = new StockpileLevels();
                    this.getStockpileLevels(levels);
                    switch (buildingType)
                    {
                        case 6:
                            return this.capResource(buildingType, levels.woodLevel);

                        case 7:
                            return this.capResource(buildingType, levels.stoneLevel);

                        case 8:
                            return this.capResource(buildingType, levels.ironLevel);

                        case 9:
                            return this.capResource(buildingType, levels.pitchLevel);
                    }
                    break;
                }
                case 12:
                {
                    InnLevels levels4 = new InnLevels();
                    this.getInnLevels(levels4);
                    return this.capResource(buildingType, levels4.aleLevel);
                }
                case 13:
                case 14:
                case 15:
                case 0x10:
                case 0x11:
                case 0x12:
                {
                    GranaryLevels levels2 = new GranaryLevels();
                    this.getGranaryLevels(levels2);
                    switch (buildingType)
                    {
                        case 13:
                            return this.capResource(buildingType, levels2.applesLevel);

                        case 14:
                            return this.capResource(buildingType, levels2.breadLevel);

                        case 15:
                            return this.capResource(buildingType, levels2.vegLevel);

                        case 0x10:
                            return this.capResource(buildingType, levels2.meatLevel);

                        case 0x11:
                            return this.capResource(buildingType, levels2.cheeseLevel);

                        case 0x12:
                            return this.capResource(buildingType, levels2.fishLevel);
                    }
                    break;
                }
                case 0x13:
                case 0x15:
                case 0x16:
                case 0x17:
                case 0x18:
                case 0x19:
                case 0x1a:
                case 0x21:
                {
                    TownHallLevels levels3 = new TownHallLevels();
                    this.getTownHallLevels(levels3);
                    switch (buildingType)
                    {
                        case 0x13:
                            return this.capResource(buildingType, levels3.clothesLevel);

                        case 0x15:
                            return this.capResource(buildingType, levels3.furnitureLevel);

                        case 0x16:
                            return this.capResource(buildingType, levels3.venisonLevel);

                        case 0x17:
                            return this.capResource(buildingType, levels3.saltLevel);

                        case 0x18:
                            return this.capResource(buildingType, levels3.spicesLevel);

                        case 0x19:
                            return this.capResource(buildingType, levels3.silkLevel);

                        case 0x1a:
                            return this.capResource(buildingType, levels3.metalwareLevel);

                        case 0x21:
                            return this.capResource(buildingType, levels3.wineLevel);
                    }
                    break;
                }
                case 0x1c:
                case 0x1d:
                case 30:
                case 0x1f:
                case 0x20:
                {
                    ArmouryLevels levels5 = new ArmouryLevels();
                    this.getArmouryLevels(levels5);
                    switch (buildingType)
                    {
                        case 0x1c:
                            return this.capResource(buildingType, levels5.pikesLevel);

                        case 0x1d:
                            return this.capResource(buildingType, levels5.bowsLevel);

                        case 30:
                            return this.capResource(buildingType, levels5.swordsLevel);

                        case 0x1f:
                            return this.capResource(buildingType, levels5.armourLevel);

                        case 0x20:
                            return this.capResource(buildingType, levels5.catapultsLevel);
                    }
                    break;
                }
            }
            return 0.0;
        }

        public double getResourceProductionPerDay(int buildingType)
        {
            double num = 0.0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if (((building.buildingType == buildingType) && (building.calcRate != 0.0)) && building.complete)
                {
                    num += 86400.0 / building.calcRate;
                }
            }
            double num2 = CardTypes.adjustPayloadSize(GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(buildingType), buildingType);
            return (num * num2);
        }

        public bool getStockpileLevels(StockpileLevels levels)
        {
            if ((this.findBuildingType(2) == null) && !GameEngine.Instance.World.isCapital(this.VillageID))
            {
                return false;
            }
            levels.woodLevel = this.m_woodLevel;
            levels.stoneLevel = this.m_stoneLevel;
            levels.ironLevel = this.m_ironLevel;
            levels.pitchLevel = this.m_pitchLevel;
            double localTimeLapsed = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                switch (building.buildingType)
                {
                    case 6:
                        levels.woodLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 7:
                        levels.stoneLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 8:
                        levels.ironLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 9:
                        levels.pitchLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;
                }
            }
            levels.woodLevel = this.capResource(6, levels.woodLevel);
            levels.stoneLevel = this.capResource(7, levels.stoneLevel);
            levels.ironLevel = this.capResource(8, levels.ironLevel);
            levels.pitchLevel = this.capResource(9, levels.pitchLevel);
            return true;
        }

        public bool getTownHallLevels(TownHallLevels levels)
        {
            if (this.findBuildingType(0) == null)
            {
                return false;
            }
            levels.clothesLevel = this.m_clothesLevel;
            levels.furnitureLevel = this.m_furnitureLevel;
            levels.spicesLevel = this.m_spicesLevel;
            levels.silkLevel = this.m_silkLevel;
            levels.metalwareLevel = this.m_metalwareLevel;
            levels.saltLevel = this.m_saltLevel;
            levels.venisonLevel = this.m_venisonLevel;
            levels.wineLevel = this.m_wineLevel;
            double localTimeLapsed = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                switch (building.buildingType)
                {
                    case 0x13:
                        levels.clothesLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 0x15:
                        levels.furnitureLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 0x16:
                        levels.venisonLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 0x17:
                        levels.saltLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 0x18:
                        levels.spicesLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 0x19:
                        levels.silkLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 0x1a:
                        levels.metalwareLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;

                    case 0x21:
                        levels.wineLevel += this.calcResourceLevel(building, localTimeLapsed);
                        break;
                }
            }
            levels.saltLevel = this.capResource(0x17, levels.saltLevel);
            levels.venisonLevel = this.capResource(0x16, levels.venisonLevel);
            levels.wineLevel = this.capResource(0x21, levels.wineLevel);
            levels.spicesLevel = this.capResource(0x18, levels.spicesLevel);
            levels.silkLevel = this.capResource(0x19, levels.silkLevel);
            levels.metalwareLevel = this.capResource(0x1a, levels.metalwareLevel);
            levels.furnitureLevel = this.capResource(0x15, levels.furnitureLevel);
            levels.clothesLevel = this.capResource(0x13, levels.clothesLevel);
            return true;
        }

        public void getUserTradersCallback(GetUserTraders_ReturnType returnData)
        {
            if (returnData.Success)
            {
                VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
                if (map != null)
                {
                    setServerTime(returnData.currentTime);
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    map.importTraders(returnData.traders, returnData.currentTime);
                }
                else
                {
                    GameEngine.Instance.World.importOrphanedTraders(returnData.traders, returnData.currentTime, returnData.villageID);
                }
            }
        }

        public void getVillageTroops(ref int numAvailableDefenderPeasants, ref int numAvailableDefenderArchers, ref int numAvailableDefenderPikemen, ref int numAvailableDefenderSwordsmen, ref int numAvailableDefenderCaptains)
        {
            numAvailableDefenderPeasants = this.m_numPeasants;
            numAvailableDefenderArchers = this.m_numArchers;
            numAvailableDefenderPikemen = this.m_numPikemen;
            numAvailableDefenderSwordsmen = this.m_numSwordsmen;
            numAvailableDefenderCaptains = this.m_numCaptains;
        }

        public void getVillageVassalTroops(ref int numAvailableVassalDefenderPeasants, ref int numAvailableVassalDefenderArchers, ref int numAvailableVassalDefenderPikemen, ref int numAvailableVassalDefenderSwordsmen)
        {
            numAvailableVassalDefenderPeasants = this.m_numStationedPeasants;
            numAvailableVassalDefenderArchers = this.m_numStationedArchers;
            numAvailableVassalDefenderPikemen = this.m_numStationedPikemen;
            numAvailableVassalDefenderSwordsmen = this.m_numStationedSwordsmen;
        }

        public int getWeaponsPerDayValue(VillageMapBuilding building)
        {
            return (int) this.getWeaponsPerDayValueD(building);
        }

        public double getWeaponsPerDayValueD(VillageMapBuilding building)
        {
            VillageMapBuilding building2 = this.findBuildingType(4);
            VillageMapBuilding building3 = this.findBuildingType(2);
            if ((building2 == null) || (building3 == null))
            {
                return 0.0;
            }
            switch (building.buildingType)
            {
                case 0x1c:
                    return (this.m_productionRate_Pikes * 86400.0);

                case 0x1d:
                    return (this.m_productionRate_Bows * 86400.0);

                case 30:
                    return (this.m_productionRate_Swords * 86400.0);

                case 0x1f:
                    return (this.m_productionRate_Armour * 86400.0);

                case 0x20:
                    return (this.m_productionRate_Catapults * 86400.0);
            }
            return 0.0;
        }

        public void highlightBuilding(VillageMapBuilding highlightBuilding)
        {
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                Color white = ARGBColors.White;
                if (building != highlightBuilding)
                {
                    white = Color.FromArgb(0xb0, 0xb0, 0xb0);
                    if (!building.isComplete() || building.isDeleting())
                    {
                        continue;
                    }
                }
                building.baseSprite.ColorToUse = white;
                if (building.stockpileExtension != null)
                {
                    building.stockpileExtension.colorSprites(white);
                }
                if (building.granaryExtension != null)
                {
                    building.granaryExtension.colorSprites(white);
                }
                if (building.innExtension != null)
                {
                    building.innExtension.colorSprites(white);
                }
            }
            highlightBuilding.highlighted = true;
        }

        public void holdBanquetCallback(VillageHoldBanquet_ReturnType returnData)
        {
            if (returnData.Success)
            {
                VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
                if (map != null)
                {
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                }
                setServerTime(returnData.currentTime);
                GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
                if (HoldBanquetPanel.Instance != null)
                {
                    HoldBanquetPanel.Instance.updateLevels(true);
                }
            }
        }

        public bool holdingLeftMouse()
        {
            return this.m_leftMouseHeldDown;
        }

        public void importBuildingTypesActiveList(bool[] activeList)
        {
        }

        public void importParishTaxPeople(ParishTaxCalc[] parishPeople, DateTime updateTime)
        {
            this.m_parishPeople = parishPeople;
            this.m_lastParishPeopleTime = updateTime;
        }

        public void importResourcesAndStats(VillageResourceAndStatsReturnData resourceData, DateTime currentServerTime)
        {
            this.m_lastServerReply = currentServerTime;
            this.m_woodLevel = resourceData.woodLevel;
            this.m_stoneLevel = resourceData.stoneLevel;
            this.m_ironLevel = resourceData.ironLevel;
            this.m_pitchLevel = resourceData.pitchLevel;
            this.m_aleLevel = resourceData.aleLevel;
            this.m_applesLevel = resourceData.applesLevel;
            this.m_breadLevel = resourceData.breadLevel;
            this.m_cheeseLevel = resourceData.cheeseLevel;
            this.m_meatLevel = resourceData.meatLevel;
            this.m_vegLevel = resourceData.vegLevel;
            this.m_fishLevel = resourceData.fishLevel;
            this.m_saltLevel = resourceData.saltLevel;
            this.m_wineLevel = resourceData.wineLevel;
            this.m_venisonLevel = resourceData.venisonLevel;
            this.m_clothesLevel = resourceData.clothesLevel;
            this.m_furnitureLevel = resourceData.furnitureLevel;
            this.m_spicesLevel = resourceData.spicesLevel;
            this.m_silkLevel = resourceData.silkLevel;
            this.m_metalwareLevel = resourceData.metalwareLevel;
            this.m_bowsLevel = resourceData.bowsLevel;
            this.m_pikesLevel = resourceData.pikesLevel;
            this.m_swordsLevel = resourceData.swordsLevel;
            this.m_armourLevel = resourceData.armourLevel;
            this.m_catapultsLevel = resourceData.catapultLevel;
            this.m_taxLevelServer = resourceData.taxLevel;
            this.m_rationsLevelServer = resourceData.rationsLevel;
            this.m_aleRationsLevelServer = resourceData.aleRationsLevel;
            if (this.m_taxLevel == this.m_taxLevelSent)
            {
                this.m_taxLevel = this.m_taxLevelSent = resourceData.taxLevel;
            }
            if (this.m_rationsLevel == this.m_rationsLevelSent)
            {
                this.m_rationsLevel = this.m_rationsLevelSent = resourceData.rationsLevel;
            }
            if (this.m_aleRationsLevel == this.m_aleRationsLevelSent)
            {
                this.m_aleRationsLevel = this.m_aleRationsLevelSent = resourceData.aleRationsLevel;
            }
            this.m_popularityLevel = resourceData.popularityLevel;
            this.m_housingCapacity = resourceData.housingCapacity;
            if (resourceData.totalPeople < 0)
            {
                resourceData.totalPeople = 0;
            }
            this.m_totalPeople = resourceData.totalPeople;
            this.m_spareWorkers = resourceData.sparePeople;
            this.m_immigrationNextChangeTime = resourceData.immigrationChangeTime;
            this.m_numPositiveBuildings = resourceData.numPositiveBuildings;
            this.m_numNegativeBuildings = resourceData.numNegativeBuildings;
            this.m_numPopularityBuildings = resourceData.numPopularityBuildings;
            this.m_applesConsumption = resourceData.applesConsumption;
            this.m_breadConsumption = resourceData.breadConsumption;
            this.m_cheeseConsumption = resourceData.cheeseConsumption;
            this.m_meatConsumption = resourceData.meatConsumption;
            this.m_vegConsumption = resourceData.vegConsumption;
            this.m_fishConsumption = resourceData.fishConsumption;
            this.m_consumptionLastTime = resourceData.consumptionLastTime;
            this.m_effectiveRationsLevel = resourceData.effectiveRationsLevel;
            this.m_showEffective = true;
            this.m_consumptionChangeTimeNeeded = resourceData.consumptionChangeTimeNeeded;
            if (resourceData.consumptionChangeTimeNeeded)
            {
                this.m_consumptionChangeTime = resourceData.consumptionChangeTime;
            }
            this.m_numFoodTypesEaten = resourceData.numFoodTypesEaten;
            this.m_aleConsumption = resourceData.aleConsumption;
            this.m_effectiveAleRationsLevel = resourceData.effectiveAleRationsLevel;
            this.m_showAleEffective = true;
            this.mergePopEvents(resourceData.popEventList);
            this.m_toBeMade_Bows = resourceData.toBeMade_Bows;
            this.m_toBeMade_Pikes = resourceData.toBeMade_Pikes;
            this.m_toBeMade_Swords = resourceData.toBeMade_Swords;
            this.m_toBeMade_Armour = resourceData.toBeMade_Armour;
            this.m_toBeMade_Catapults = resourceData.toBeMade_Catapults;
            this.m_productionStart_Bows = resourceData.productionStart_Bows;
            this.m_productionStart_Pikes = resourceData.productionStart_Pikes;
            this.m_productionStart_Swords = resourceData.productionStart_Swords;
            this.m_productionStart_Armour = resourceData.productionStart_Armour;
            this.m_productionStart_Catapults = resourceData.productionStart_Catapults;
            this.m_productionEnd_Bows = resourceData.productionEnd_Bows;
            this.m_productionEnd_Pikes = resourceData.productionEnd_Pikes;
            this.m_productionEnd_Swords = resourceData.productionEnd_Swords;
            this.m_productionEnd_Armour = resourceData.productionEnd_Armour;
            this.m_productionEnd_Catapults = resourceData.productionEnd_Catapults;
            this.m_productionRate_Bows = resourceData.productionRate_Bows;
            this.m_productionRate_Pikes = resourceData.productionRate_Pikes;
            this.m_productionRate_Swords = resourceData.productionRate_Swords;
            this.m_productionRate_Armour = resourceData.productionRate_Armour;
            this.m_productionRate_Catapults = resourceData.productionRate_Catapults;
            this.m_nextWeaponsCheck = resourceData.nextWeaponsCheck.AddSeconds(10.0);
            this.m_parishCapitalResearchData = resourceData.capitalResearchData;
            this.m_ownedDate = resourceData.ownedDate;
            this.m_numParishFlags = resourceData.numParishFlags;
            if (resourceData.capitalBuildingsBuilt == null)
            {
                this.m_capitalBuildingsBuilt = null;
            }
            else
            {
                this.m_capitalBuildingsBuilt = new List<int>();
                this.m_capitalBuildingsBuilt.AddRange(resourceData.capitalBuildingsBuilt);
            }
            this.m_numArchers = resourceData.numTroops_Archers;
            this.m_numPeasants = resourceData.numTroops_Peasants;
            this.m_numPikemen = resourceData.numTroops_Pikemen;
            this.m_numSwordsmen = resourceData.numTroops_Swordsmen;
            this.m_numCatapults = resourceData.numTroops_Catapults;
            this.m_numScouts = resourceData.numTroops_Scouts;
            this.m_numCaptains = resourceData.numTroops_Captains;
            this.m_creatingCaptain = resourceData.captainCreating;
            this.m_captainCreationTime = resourceData.captainCreationTime;
            this.m_lastBanquetStored = resourceData.lastBanquetStored;
            this.m_lastBanquetHonour = resourceData.lastBanquetHonour;
            this.m_lastBanquetDate = resourceData.lastBanquetDate;
            this.m_capitalGold = resourceData.capitalGold;
            this.m_capitalTaxRateServer = resourceData.capitalTaxRate;
            this.m_parentCapitalTaxRate = resourceData.parentCapitalTaxRate;
            this.m_lastCapitalTaxRate = resourceData.lastCapitalTaxRate;
            this.m_numOfActiveChildrenAreas = resourceData.numOfActiveChildrenAreas;
            if (this.m_capitalTaxRate == this.m_capitalTaxRateSent)
            {
                this.m_capitalTaxRate = this.m_capitalTaxRateSent = resourceData.capitalTaxRate;
            }
            this.m_numStationedArchers = resourceData.numStationedTroops_Archers;
            this.m_numStationedPeasants = resourceData.numStationedTroops_Peasants;
            this.m_numStationedPikemen = resourceData.numStationedTroops_Pikemen;
            this.m_numStationedSwordsmen = resourceData.numStationedTroops_Swordsmen;
            this.m_numStationedCatapults = resourceData.numStationedTroops_Catapults;
            this.m_numTradersAtHome = resourceData.numTraders;
            this.m_nextMapTypeChange = resourceData.nextMapTypeChange;
            this.m_interdictionTime = resourceData.interdictProtectionEndTime;
            GameEngine.Instance.World.setInterdictTime(this.VillageID, resourceData.interdictProtectionEndTime);
            GameEngine.Instance.World.setPeaceTime(this.VillageID, resourceData.peaceTime);
            GameEngine.Instance.World.setExcommunicationTime(this.VillageID, resourceData.excommunicationEndTime);
            this.m_excommunicationTime = resourceData.excommunicationEndTime;
            this.m_castleEnclosed = resourceData.castleEnclosed;
            this.m_captialNextDelete = resourceData.nextCapitalDelete;
            if (resourceData.numMadeCaptains >= 0)
            {
                GameEngine.Instance.World.setNumMadeCaptains(resourceData.numMadeCaptains);
            }
            this.showStats();
            if (GameEngine.Instance.Castle != null)
            {
                GameEngine.Instance.Castle.updateAvailableTroops();
            }
        }

        public void importTraders(List<MarketTraderData> traderData, DateTime curServerTime)
        {
            if (traderData != null)
            {
                this.traders.Clear();
                this.traders.AddRange(traderData);
                GameEngine.Instance.World.clearTraderArray(this.m_villageID);
                foreach (MarketTraderData data in traderData)
                {
                    GameEngine.Instance.World.addTrader(data, curServerTime);
                }
            }
        }

        public void importVillageBuildings(List<VillageBuildingReturnData> newBuildings, bool fullUpdate)
        {
            if (fullUpdate)
            {
                List<long> list = new List<long>();
                foreach (VillageMapBuilding building in this.localBuildings)
                {
                    list.Add(building.buildingID);
                }
                this.localBuildings.Clear();
                villageClickMask.clearMap();
                this.backgroundSprite.RemoveAllChildren();
                if (((this.m_villageMapType == 10) || (this.m_villageMapType == 11)) || ((this.m_villageMapType == 12) || (this.m_villageMapType == 13)))
                {
                    if (this.m_villageMapType == 13)
                    {
                        this.backgroundOverlaySprite.PosY = 434f;
                    }
                    else
                    {
                        this.backgroundOverlaySprite.PosY = 474f;
                    }
                    this.backgroundSprite.AddChild(this.backgroundOverlaySprite, 0x13);
                }
                this.layout = villageLayout[this.m_mapID].createClone();
                if (newBuildings != null)
                {
                    foreach (VillageBuildingReturnData data in newBuildings)
                    {
                        VillageMapBuilding newBuilding = new VillageMapBuilding();
                        newBuilding.createFromReturnData(data);
                        this.addBuildingToMap(newBuilding, (Point) data.buildingLocation, data.buildingType);
                        newBuilding.initStorageBuilding(this.gfx, this);
                        newBuilding.calcRate = data.calcRate;
                        newBuilding.lastCalcTime = data.lastCalcTime;
                        newBuilding.storageLocation = (Point) data.storageLocation;
                        newBuilding.serverJourneyTime = data.journeyTime;
                        newBuilding.updateConstructionGFX(localBaseTime, baseServerTime, true, this);
                        newBuilding.updateSymbolGFX();
                        list.Remove(newBuilding.buildingID);
                    }
                }
                foreach (long num in list)
                {
                    this.removeAnimals(num);
                }
                this.updateBuildingsOnImport();
            }
            else
            {
                foreach (VillageBuildingReturnData data2 in newBuildings)
                {
                    foreach (VillageMapBuilding building3 in this.localBuildings)
                    {
                        if (building3.buildingID == data2.buildingID)
                        {
                            building3.createFromReturnData(data2);
                            building3.initStorageBuilding(this.gfx, this);
                            building3.updateConstructionGFX(localBaseTime, baseServerTime, true, this);
                            building3.updateSymbolGFX();
                            break;
                        }
                    }
                }
            }
            this.preCountHonourBuildings();
        }

        public void initCarryingAnim(VillageMapBuilding building)
        {
            building.worker.idling = false;
            building.worker.working = false;
            building.open = false;
            switch (building.buildingType)
            {
                case 6:
                    building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 7:
                    building.worker.initAnim(GFXLibrary.Instance.StonemasonAnimTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 8:
                    building.worker.initAnim(GFXLibrary.Instance.IronMinerAnimTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 9:
                    building.worker.initAnim(GFXLibrary.Instance.PitchworkerAnimTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 12:
                    building.worker.initAnim(GFXLibrary.Instance.Body_brewerTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 13:
                    building.worker.initAnim(GFXLibrary.Instance.FarmerAnimTexID, 7, 0x100, 0x10, 8, 50, true);
                    return;

                case 14:
                    building.worker.initAnim(GFXLibrary.Instance.BakerAnimTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 15:
                    building.worker.initAnim(GFXLibrary.Instance.Farmer2AnimTexID, 7, 0x100, 0x10, 8, 50, true);
                    return;

                case 0x10:
                    building.worker.initAnim(GFXLibrary.Instance.Farmer2AnimTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 0x11:
                    building.worker.initAnim(GFXLibrary.Instance.FarmerAnimTexID, 7, 0x180, 0x10, 8, 50, true);
                    return;

                case 0x12:
                    building.worker.initAnim(GFXLibrary.Instance.Farmer2AnimTexID, 7, 0x200, 0x10, 8, 50, true);
                    return;

                case 0x13:
                    building.worker.initAnim(GFXLibrary.Instance.Body_tailorTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 0x15:
                    building.worker.initAnim(GFXLibrary.Instance.Body_carpenterTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 0x16:
                    building.worker.initAnim(GFXLibrary.Instance.Body_hunterTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 0x17:
                    building.worker.initAnim(GFXLibrary.Instance.Farmer2AnimTexID, 7, 0x180, 0x10, 8, 50, true);
                    return;

                case 0x18:
                    building.worker.initAnim(GFXLibrary.Instance.DockworkerAnimTexID, 7, 0x100, 0x10, 8, 50, true);
                    return;

                case 0x19:
                    building.worker.initAnim(GFXLibrary.Instance.DockworkerAnimTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 0x1a:
                    building.worker.initAnim(GFXLibrary.Instance.MetalWorkerAnimTexID, 7, 0x100, 0x10, 8, 50, true);
                    return;

                case 0x1c:
                    building.worker.initAnim(GFXLibrary.Instance.PoleturnerAnimTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 0x1d:
                    building.worker.initAnim(GFXLibrary.Instance.FletcherAnimTexID, 7, 0x100, 0x10, 8, 50, true);
                    return;

                case 30:
                    building.worker.initAnim(GFXLibrary.Instance.BlacksmithAnimTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 0x1f:
                    building.worker.initAnim(GFXLibrary.Instance.ArmourerAnimTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 0x20:
                    building.worker.initAnim(GFXLibrary.Instance.Body_siegeworkerTexID, 7, 0x100, 0x10, 8, 50, true);
                    return;

                case 0x21:
                    building.worker.initAnim(GFXLibrary.Instance.Farmer2AnimTexID, 7, 0, 0x10, 8, 50, true);
                    return;
            }
            building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 7, 0x80, 0x10, 8, 50, true);
        }

        public void initClickMask()
        {
            int width = this.layout.gridWidth * 0x20;
            int height = this.layout.gridHeight * 0x10;
            villageClickMask.init(width, height, this.gfx);
        }

        public void initCollectingAnim(VillageMapBuilding building)
        {
            building.worker.idling = false;
            building.worker.working = false;
            building.open = false;
            switch (building.buildingType)
            {
                case 0x1c:
                    building.worker.initAnim(GFXLibrary.Instance.PoleturnerAnimTexID, 7, 0x100, 0x10, 8, 50, true);
                    return;

                case 0x1d:
                    building.worker.initAnim(GFXLibrary.Instance.FletcherAnimTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;

                case 30:
                    building.worker.initAnim(GFXLibrary.Instance.BlacksmithAnimTexID, 7, 0x100, 0x10, 8, 50, true);
                    return;

                case 0x1f:
                    building.worker.initAnim(GFXLibrary.Instance.ArmourerAnimTexID, 7, 0x100, 0x10, 8, 50, true);
                    return;

                case 0x20:
                    building.worker.initAnim(GFXLibrary.Instance.Body_siegeworkerTexID, 7, 0x80, 0x10, 8, 50, true);
                    return;
            }
            building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 7, 0x80, 0x10, 8, 50, true);
        }

        public void initGFX(GraphicsMgr mgr)
        {
            this.gfx = mgr;
            this.backgroundSprite = new SpriteWrapper();
            this.backgroundSprite.TextureID = backgroundTexture;
            this.backgroundSprite.Initialize(this.gfx);
            this.backgroundSprite.PosX = 0f;
            this.backgroundSprite.PosY = 0f;
            this.backgroundSprite.Scale = 1f;
            int width = this.layout.gridWidth * 0x20;
            int height = this.layout.gridHeight * 0x10;
            Rectangle rectangle = new Rectangle(0, 0, width, height);
            this.backgroundSprite.SourceRectangle = rectangle;
            SizeF ef = new SizeF((float) width, (float) height);
            this.backgroundSprite.Size = ef;
            this.backgroundSprite.PosX = (int) (0f - ((this.backgroundSprite.Width - InterfaceMgr.Instance.ParentMainWindow.getDXBasePanel().Width) / 2f));
            this.backgroundSprite.PosY = (int) (0f - ((this.backgroundSprite.Height - InterfaceMgr.Instance.ParentMainWindow.getDXBasePanel().Height) / 2f));
            this.createSurroundSprites();
            this.backgroundOverlaySprite = new SpriteWrapper();
            this.backgroundOverlaySprite.TextureID = backgroundTexture;
            this.backgroundOverlaySprite.Initialize(this.gfx);
            this.backgroundOverlaySprite.PosX = 0f;
            this.backgroundOverlaySprite.PosY = 474f;
            this.backgroundOverlaySprite.Scale = 1f;
            Rectangle rectangle2 = new Rectangle(0, 0x4b1, width, 800);
            this.backgroundOverlaySprite.SourceRectangle = rectangle2;
            SizeF ef2 = new SizeF((float) width, 800f);
            this.backgroundOverlaySprite.Size = ef2;
        }

        public void initIdlingAnim(VillageMapBuilding building)
        {
            building.worker.idling = true;
            building.worker.working = false;
            building.open = false;
            switch (building.buildingType)
            {
                case 6:
                    building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 0x100, woodcutterIdleAnim, 50);
                    return;

                case 7:
                    building.worker.initAnim(GFXLibrary.Instance.StonemasonAnimTexID, 3, 1, 50);
                    return;

                case 8:
                    building.worker.initAnim(GFXLibrary.Instance.IronMinerAnimTexID, 3, 1, 50);
                    return;

                case 9:
                    building.worker.initAnim(GFXLibrary.Instance.PitchworkerAnimTexID, 0xff, pitchworkerIdleAnim, 0x4b);
                    return;

                case 12:
                {
                    building.worker.setPos(building.buildingLocation);
                    PointF tf7 = building.worker.getCurrentPos();
                    tf7.X -= 81f;
                    tf7.Y += 23f;
                    building.worker.setPixelPos(Point.Truncate(tf7));
                    building.worker.initAnim(GFXLibrary.Instance.Body_brewerTexID, 0xff, brewerIdleAnim, 0x4b);
                    return;
                }
                case 13:
                {
                    building.worker.setPos(building.buildingLocation);
                    PointF tf = building.worker.getCurrentPos();
                    tf.X -= 66f;
                    tf.Y += 15f;
                    building.worker.setPixelPos(Point.Truncate(tf));
                    building.worker.initAnim(GFXLibrary.Instance.Farmer3AnimTexID, 0x100, farmer3IdleAnim, 150);
                    return;
                }
                case 14:
                {
                    building.worker.setPos(building.buildingLocation);
                    PointF tf6 = building.worker.getCurrentPos();
                    tf6.X -= 19f;
                    tf6.Y += 43f;
                    building.worker.setPixelPos(Point.Truncate(tf6));
                    building.worker.initAnim(GFXLibrary.Instance.BakerAnimTexID, 0xff, bakerIdleAnim, 100);
                    return;
                }
                case 15:
                {
                    building.worker.setPos(building.buildingLocation);
                    PointF tf2 = building.worker.getCurrentPos();
                    tf2.X += 22f;
                    tf2.Y += 22f;
                    building.worker.setPixelPos(Point.Truncate(tf2));
                    building.worker.initAnim(GFXLibrary.Instance.Farmer3AnimTexID, 0x100, farmer3IdleAnim, 150);
                    return;
                }
                case 0x10:
                {
                    building.worker.setPos(building.buildingLocation);
                    PointF tf5 = building.worker.getCurrentPos();
                    tf5.X += 32f;
                    tf5.Y += 3f;
                    building.worker.setPixelPos(Point.Truncate(tf5));
                    building.worker.initAnim(GFXLibrary.Instance.Farmer3AnimTexID, 0x100, farmer3IdleAnim, 150);
                    this.removeAnimals(building);
                    return;
                }
                case 0x11:
                {
                    building.worker.setPos(building.buildingLocation);
                    PointF tf4 = building.worker.getCurrentPos();
                    tf4.X -= 37f;
                    tf4.Y -= 20f;
                    building.worker.setPixelPos(Point.Truncate(tf4));
                    building.worker.initAnim(GFXLibrary.Instance.Farmer3AnimTexID, 0x100, farmer3IdleAnim, 150);
                    return;
                }
                case 0x12:
                {
                    building.worker.setPos(building.buildingLocation);
                    PointF tf3 = building.worker.getCurrentPos();
                    tf3.X += 26f;
                    tf3.Y -= 28f;
                    building.worker.setPixelPos(Point.Truncate(tf3));
                    building.worker.initAnim(GFXLibrary.Instance.Farmer3AnimTexID, 0x100, farmer3IdleAnim, 150);
                    return;
                }
                case 0x13:
                    building.worker.initAnim(GFXLibrary.Instance.Body_tailorTexID, 0xff, tailorIdleAnim, 0x4b);
                    this.removeAnimals(building);
                    return;

                case 0x15:
                    building.worker.initAnim(GFXLibrary.Instance.Body_carpenterTexID, 0xff, carpenterIdleAnim, 0x4b);
                    return;

                case 0x16:
                    building.worker.initAnim(GFXLibrary.Instance.Body_hunterTexID, 0xff, hunterIdleAnim, 0x4b);
                    return;

                case 0x17:
                    building.worker.initAnim(GFXLibrary.Instance.Farmer3AnimTexID, 0x100, farmer3IdleAnim, 150);
                    return;

                case 0x18:
                case 0x19:
                    building.worker.initAnim(GFXLibrary.Instance.DockworkerAnimTexID, 0x17f, dockworkerIdleAnim, 0x4b);
                    return;

                case 0x1a:
                    building.worker.initAnim(GFXLibrary.Instance.MetalWorkerAnimTexID, 0x17f, metalWorkerIdleAnim, 0x4b);
                    return;

                case 0x1c:
                    building.worker.initAnim(GFXLibrary.Instance.PoleturnerAnimTexID, 3, 1, 50);
                    return;

                case 0x1d:
                    building.worker.initAnim(GFXLibrary.Instance.FletcherAnimTexID, 3, 1, 50);
                    return;

                case 30:
                    building.worker.initAnim(GFXLibrary.Instance.BlacksmithAnimTexID, 0x17f, blacksmithIdleAnim, 0x4b);
                    return;

                case 0x1f:
                    building.worker.initAnim(GFXLibrary.Instance.ArmourerAnimTexID, 0x17f, armourerIdleAnim, 0x4b);
                    return;

                case 0x20:
                    building.worker.initAnim(GFXLibrary.Instance.Body_siegeworkerTexID, 0x17f, siegeWorkerIdleAnim, 0x4b);
                    return;

                case 0x21:
                    building.worker.initAnim(GFXLibrary.Instance.Farmer3AnimTexID, 0x100, farmer3IdleAnim, 150);
                    return;
            }
            building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 0x100, woodcutterIdleAnim, 50);
        }

        public void initWalkingAnim(VillageMapBuilding building)
        {
            building.worker.idling = false;
            building.worker.working = false;
            building.open = false;
            switch (building.buildingType)
            {
                case 6:
                    building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 7:
                    building.worker.initAnim(GFXLibrary.Instance.StonemasonAnimTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 8:
                    building.worker.initAnim(GFXLibrary.Instance.IronMinerAnimTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 9:
                    building.worker.initAnim(GFXLibrary.Instance.PitchworkerAnimTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 12:
                    building.worker.initAnim(GFXLibrary.Instance.Body_brewerTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 13:
                case 15:
                case 0x10:
                case 0x11:
                case 0x12:
                case 0x17:
                case 0x21:
                    building.worker.initAnim(GFXLibrary.Instance.FarmerAnimTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 14:
                    building.worker.initAnim(GFXLibrary.Instance.BakerAnimTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 0x13:
                    building.worker.initAnim(GFXLibrary.Instance.Body_tailorTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 0x15:
                    building.worker.initAnim(GFXLibrary.Instance.Body_carpenterTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 0x16:
                    building.worker.initAnim(GFXLibrary.Instance.Body_hunterTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 0x18:
                case 0x19:
                    building.worker.initAnim(GFXLibrary.Instance.DockworkerAnimTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 0x1a:
                    building.worker.initAnim(GFXLibrary.Instance.MetalWorkerAnimTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 0x1c:
                    building.worker.initAnim(GFXLibrary.Instance.PoleturnerAnimTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 0x1d:
                    building.worker.initAnim(GFXLibrary.Instance.FletcherAnimTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 30:
                    building.worker.initAnim(GFXLibrary.Instance.BlacksmithAnimTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 0x1f:
                    building.worker.initAnim(GFXLibrary.Instance.ArmourerAnimTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 0x20:
                    building.worker.initAnim(GFXLibrary.Instance.Body_siegeworkerTexID, 7, 0, 0x10, 8, 50, true);
                    return;

                case 0x4e:
                    if (building.secondaryWorker != null)
                    {
                        building.secondaryWorker.initAnim(GFXLibrary.Instance.TraderHorseAnimTexID, 7, 0, 0x10, 8, 50, true);
                        return;
                    }
                    building.worker.initAnim(GFXLibrary.Instance.TraderAnimTexID, 7, 0, 0x10, 8, 50, true);
                    return;
            }
            building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 7, 0, 0x10, 8, 50, true);
        }

        public void initWorkingAnim(VillageMapBuilding building, bool initialCall)
        {
            PointF tf;
            int num;
            building.worker.idling = false;
            building.worker.working = true;
            building.open = true;
            switch (building.buildingType)
            {
                case 6:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 7:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 8:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 9:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 12:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 13:
                    building.worker.workerSprite.Visible = false;
                    tf = new PointF(0f, 0f);
                    num = new Random().Next(5);
                    if (building.randState >= 0)
                    {
                        this.setRandStateData(building, num);
                        break;
                    }
                    num = this.findRandStateData(building, num);
                    break;

                case 14:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 15:
                case 0x17:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 0x10:
                    building.worker.workerSprite.Visible = false;
                    this.CreateAnimals(building);
                    goto Label_04D7;

                case 0x11:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 0x12:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 0x13:
                    building.worker.workerSprite.Visible = false;
                    this.CreateAnimals(building);
                    goto Label_04D7;

                case 0x15:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 0x16:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 0x18:
                case 0x19:
                    building.worker.initAnim(GFXLibrary.Instance.DockworkerAnimTexID, 0x17f, dockworkerIdleAnim, 0x4b);
                    goto Label_04D7;

                case 0x1a:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 0x1c:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 0x1d:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 30:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 0x1f:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 0x20:
                    building.worker.workerSprite.Visible = false;
                    goto Label_04D7;

                case 0x21:
                {
                    building.worker.workerSprite.Visible = false;
                    PointF tf2 = new PointF(0f, 0f);
                    int data = new Random().Next(5);
                    if (building.randState >= 0)
                    {
                        this.setRandStateData(building, data);
                    }
                    else
                    {
                        data = this.findRandStateData(building, data);
                    }
                    switch (data)
                    {
                        case 1:
                            tf2 = new PointF(11f, 64f);
                            building.animSprite.changeBaseFrame(0x40);
                            break;

                        case 2:
                            tf2 = new PointF(18f, 76f);
                            building.animSprite.changeBaseFrame(0x47);
                            break;

                        case 3:
                            tf2 = new PointF(14f, 71f);
                            building.animSprite.changeBaseFrame(0x42);
                            break;

                        case 4:
                            tf2 = new PointF(-31f, 54f);
                            building.animSprite.changeBaseFrame(70);
                            break;

                        default:
                            tf2 = new PointF(-22f, 51f);
                            building.animSprite.changeBaseFrame(0x40);
                            break;
                    }
                    building.animSprite.Center = tf2;
                    goto Label_04D7;
                }
                default:
                    building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 0x100, woodcutterIdleAnim, 50);
                    goto Label_04D7;
            }
            switch (num)
            {
                case 1:
                    tf = new PointF(83f, 51f);
                    building.animSprite.changeBaseFrame(0xa2);
                    break;

                case 2:
                    tf = new PointF(-18f, 51f);
                    building.animSprite.changeBaseFrame(0xa6);
                    break;

                case 3:
                    tf = new PointF(-21f, 53f);
                    building.animSprite.changeBaseFrame(160);
                    break;

                case 4:
                    tf = new PointF(40f, 24f);
                    building.animSprite.changeBaseFrame(160);
                    break;

                default:
                    tf = new PointF(65f, 34f);
                    building.animSprite.changeBaseFrame(160);
                    break;
            }
            building.animSprite.Center = tf;
        Label_04D7:
            if (GameEngine.Instance.Village != null)
            {
                GameEngine.Instance.Village.updateGFXState(building);
            }
        }

        public bool isCreatingCaptain(ref DateTime completeTime)
        {
            completeTime = this.m_captainCreationTime;
            return this.m_creatingCaptain;
        }

        public bool isMouseOverCancelButton(Point mousePos)
        {
            if (((placementSprite != null) && (this.backgroundSprite != null)) && (placementSprite_cancel != null))
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
            if (((placementSprite != null) && (this.backgroundSprite != null)) && (placementSprite_confirm != null))
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

        public bool isMouseOverPlacementSprite(Point mousePos)
        {
            if ((placementSprite != null) && (this.backgroundSprite != null))
            {
                Point point = mousePos;
                point.X -= ((int) this.backgroundSprite.DrawPos.X) + 0x10;
                point.Y -= ((int) this.backgroundSprite.DrawPos.Y) + 8;
                point.X += 0x10;
                point.Y += 8;
                point.X /= 0x20;
                point.Y /= 0x10;
                int num = 3;
                int num2 = Math.Abs((int) (point.X - this.lastPlaceBuildingLoc.X));
                int num3 = Math.Abs((int) (point.Y - this.lastPlaceBuildingLoc.Y));
                if ((num2 < num) && (num3 < num))
                {
                    UniversalDebugLog.Log("clicked on placement building");
                    return true;
                }
            }
            return false;
        }

        public static bool isMovingBuilding()
        {
            return (m_movingBuilding != null);
        }

        public bool isPlacingBuilding()
        {
            return (placementSprite != null);
        }

        public bool isValidBuilding(VillageMapBuilding building)
        {
            return this.localBuildings.Contains(building);
        }

        public void justDrawSprites()
        {
            if ((this.backgroundSprite != null) && InterfaceMgr.Instance.updateVillageReports())
            {
                this.backgroundSprite.Update();
                this.backgroundSprite.AddToRenderList();
                this.drawSurroundSprites();
            }
        }

        public void leaveMap()
        {
            this.stopPlaceBuilding(true);
            Sound.stopVillageEnvironmentalExceptWorld();
        }

        public void loadBackgroundImage()
        {
            string layoutFilename = this.layout.layoutFilename;
            if (layoutFilename == "vm_05_lowland1.vmp")
            {
                if (this.m_mapVariant == 1)
                {
                    layoutFilename = "vm_06_lowland2.vmp";
                }
                else if (this.m_mapVariant == 2)
                {
                    layoutFilename = "vm_07_lowland3.vmp";
                }
            }
            string str2 = @"assets\" + layoutFilename + ".png";
            if (str2 != lastBackgroundImageName)
            {
                lastBackgroundImageName = str2;
                backgroundTexture = this.gfx.loadTexture(Application.StartupPath + @"\assets\" + layoutFilename + ".png", backgroundTexture);
            }
            this.createSurroundSprites();
            this.randomiseSounds();
        }

        private static int loadBuildingTexture(string filename)
        {
            return GFXLibrary.Instance.getVillageBuildingTexture(filename);
        }

        public static void loadVillageBuildingsGFX()
        {
        }

        public static void loadVillageBuildingsGFX2()
        {
            if (!GFXLoaded || (s_villageBuildingData[0].baseGfxTexID < 0))
            {
                GFXLoaded = true;
                foreach (VillageBuildingDataNew new2 in s_villageBuildingData)
                {
                    if (new2.baseGfxFile.Length > 0)
                    {
                        new2.baseGfxTexID = loadBuildingTexture(new2.baseGfxFile);
                    }
                    if (new2.baseOpenGfxFile.Length > 0)
                    {
                        new2.baseOpenGfxTexID = loadBuildingTexture(new2.baseOpenGfxFile);
                    }
                    if (new2.shadowGfxFile.Length > 0)
                    {
                        new2.shadowGfxTexID = loadBuildingTexture(new2.shadowGfxFile);
                    }
                    if (new2.shadowOpenGfxFile.Length > 0)
                    {
                        new2.shadowOpenGfxTexID = loadBuildingTexture(new2.shadowOpenGfxFile);
                    }
                    if (new2.animGfxFile.Length > 0)
                    {
                        new2.animGfxTexID = loadBuildingTexture(new2.animGfxFile);
                    }
                }
            }
        }

        public static void loadVillageSounds()
        {
        }

        public void makePeople(int peopleType)
        {
            if (this.makePeopleLocked)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.makePeopleLockedTime);
                if (span.TotalSeconds <= 45.0)
                {
                    return;
                }
            }
            this.makePeopleLockedTime = DateTime.Now;
            this.makePeopleLocked = true;
            RemoteServices.Instance.set_MakePeople_UserCallBack(new RemoteServices.MakePeople_UserCallBack(this.makePeopleCallback));
            RemoteServices.Instance.MakePeople(this.VillageID, peopleType);
        }

        public void makePeopleCallback(MakePeople_ReturnType returnData)
        {
            this.makePeopleLocked = false;
            if (returnData.Success)
            {
                GameEngine.Instance.World.importOrphanedPeople(returnData.people, returnData.currentTime, -2);
                VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
                if (map != null)
                {
                    setServerTime(returnData.currentTime);
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                    if (GameEngine.Instance.Castle != null)
                    {
                        GameEngine.Instance.Castle.updateAvailableTroops();
                    }
                }
            }
        }

        public void makeTroopCallback(MakeTroop_ReturnType returnData)
        {
            this.makeTroopsLocked = false;
            if (returnData.Success)
            {
                VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
                if (map != null)
                {
                    setServerTime(returnData.currentTime);
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                    if (GameEngine.Instance.Castle != null)
                    {
                        GameEngine.Instance.Castle.updateAvailableTroops();
                    }
                    if (returnData.marketTraders != null)
                    {
                        map.importTraders(returnData.marketTraders, returnData.currentTime);
                    }
                    if (returnData.villageBuildings != null)
                    {
                        map.importVillageBuildings(returnData.villageBuildings, false);
                    }
                }
            }
            switch (returnData.troopTypeMade)
            {
                case 70:
                    this.localMadeTroopsSent_Peasants = 0;
                    return;

                case 0x47:
                    this.localMadeTroopsSent_Swordsmen = 0;
                    return;

                case 0x48:
                    this.localMadeTroopsSent_Archers = 0;
                    return;

                case 0x49:
                    this.localMadeTroopsSent_Pikemen = 0;
                    return;

                case 0x4a:
                    this.localMadeTroopsSent_Catapults = 0;
                    return;

                case 0x4b:
                    break;

                case 0x4c:
                    this.localMadeTroopsSent_Scouts = 0;
                    break;

                case 0x55:
                case 100:
                    this.localMadeTroopsSent_Captains = 0;
                    return;

                default:
                    return;
            }
        }

        public void makeTroops(int troopType)
        {
            this.makeTroops(troopType, 1, false);
        }

        public void makeTroops(int troopType, int amount, bool quickSend)
        {
            int num;
            if (troopType != -5)
            {
                if ((troopType == this.localMadeTroops_lastType) || (this.localMadeTroops_lastType == -1))
                {
                    goto Label_01CD;
                }
                num = 0;
                switch (this.localMadeTroops_lastType)
                {
                    case 70:
                        num = this.localMadeTroops_Peasants;
                        this.localMadeTroopsSent_Peasants = this.localMadeTroops_Peasants;
                        this.localMadeTroops_Peasants = 0;
                        break;

                    case 0x47:
                        num = this.localMadeTroops_Swordsmen;
                        this.localMadeTroopsSent_Swordsmen = this.localMadeTroops_Swordsmen;
                        this.localMadeTroops_Swordsmen = 0;
                        break;

                    case 0x48:
                        num = this.localMadeTroops_Archers;
                        this.localMadeTroopsSent_Archers = this.localMadeTroops_Archers;
                        this.localMadeTroops_Archers = 0;
                        break;

                    case 0x49:
                        num = this.localMadeTroops_Pikemen;
                        this.localMadeTroopsSent_Pikemen = this.localMadeTroops_Pikemen;
                        this.localMadeTroops_Pikemen = 0;
                        break;

                    case 0x4a:
                        num = this.localMadeTroops_Catapults;
                        this.localMadeTroopsSent_Catapults = this.localMadeTroops_Catapults;
                        this.localMadeTroops_Catapults = 0;
                        break;

                    case 0x4c:
                        num = this.localMadeTroops_Scouts;
                        this.localMadeTroopsSent_Scouts = this.localMadeTroops_Scouts;
                        this.localMadeTroops_Scouts = 0;
                        break;

                    case 0x55:
                    case 100:
                        num = this.localMadeTroops_Captains;
                        this.localMadeTroopsSent_Captains = this.localMadeTroops_Captains;
                        this.localMadeTroops_Captains = 0;
                        break;
                }
            }
            else
            {
                if (this.makeTroopsLocked)
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - this.makeTroopsLockedTime);
                    if (span.TotalSeconds <= 45.0)
                    {
                        return;
                    }
                }
                this.makeTroopsLockedTime = DateTime.Now;
                this.makeTroopsLocked = true;
                RemoteServices.Instance.set_MakeTroop_UserCallBack(new RemoteServices.MakeTroop_UserCallBack(this.makeTroopCallback));
                RemoteServices.Instance.MakeTroop(this.VillageID, troopType, amount);
                return;
            }
            this.makeTroopsLockedTime = DateTime.Now;
            RemoteServices.Instance.set_MakeTroop_UserCallBack(new RemoteServices.MakeTroop_UserCallBack(this.makeTroopCallback));
            RemoteServices.Instance.MakeTroop(this.VillageID, this.localMadeTroops_lastType, num);
            this.localMadeTroops_lastType = -1;
        Label_01CD:
            switch (troopType)
            {
                case 70:
                    this.localMadeTroops_Peasants += amount;
                    break;

                case 0x47:
                    this.localMadeTroops_Swordsmen += amount;
                    break;

                case 0x48:
                    this.localMadeTroops_Archers += amount;
                    break;

                case 0x49:
                    this.localMadeTroops_Pikemen += amount;
                    break;

                case 0x4a:
                    this.localMadeTroops_Catapults += amount;
                    break;

                case 0x4c:
                    this.localMadeTroops_Scouts += amount;
                    break;

                case 0x55:
                case 100:
                    this.localMadeTroops_Captains += amount;
                    break;
            }
            this.localMadeTroops_lastType = troopType;
            if (!quickSend)
            {
                this.localMadeTroops_lastTime = DateTime.Now;
            }
            else
            {
                this.localMadeTroops_lastTime = DateTime.Now.AddMinutes(-1.0);
            }
        }

        public void makeTroopsUpdate()
        {
            if (this.localMadeTroops_lastType > 0)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.localMadeTroops_lastTime);
                if (span.TotalSeconds > 2.0)
                {
                    int amount = 0;
                    switch (this.localMadeTroops_lastType)
                    {
                        case 70:
                            amount = this.localMadeTroops_Peasants;
                            this.localMadeTroopsSent_Peasants = this.localMadeTroops_Peasants;
                            this.localMadeTroops_Peasants = 0;
                            break;

                        case 0x47:
                            amount = this.localMadeTroops_Swordsmen;
                            this.localMadeTroopsSent_Swordsmen = this.localMadeTroops_Swordsmen;
                            this.localMadeTroops_Swordsmen = 0;
                            break;

                        case 0x48:
                            amount = this.localMadeTroops_Archers;
                            this.localMadeTroopsSent_Archers = this.localMadeTroops_Archers;
                            this.localMadeTroops_Archers = 0;
                            break;

                        case 0x49:
                            amount = this.localMadeTroops_Pikemen;
                            this.localMadeTroopsSent_Pikemen = this.localMadeTroops_Pikemen;
                            this.localMadeTroops_Pikemen = 0;
                            break;

                        case 0x4a:
                            amount = this.localMadeTroops_Catapults;
                            this.localMadeTroopsSent_Catapults = this.localMadeTroops_Catapults;
                            this.localMadeTroops_Catapults = 0;
                            break;

                        case 0x4c:
                            amount = this.localMadeTroops_Scouts;
                            this.localMadeTroopsSent_Scouts = this.localMadeTroops_Scouts;
                            this.localMadeTroops_Scouts = 0;
                            break;

                        case 0x55:
                        case 100:
                            amount = this.localMadeTroops_Captains;
                            this.localMadeTroopsSent_Captains = this.localMadeTroops_Captains;
                            this.localMadeTroops_Captains = 0;
                            break;
                    }
                    this.makeTroopsLockedTime = DateTime.Now;
                    RemoteServices.Instance.set_MakeTroop_UserCallBack(new RemoteServices.MakeTroop_UserCallBack(this.makeTroopCallback));
                    RemoteServices.Instance.MakeTroop(this.VillageID, this.localMadeTroops_lastType, amount);
                    this.localMadeTroops_lastType = -1;
                }
            }
        }

        public void manageBackgroundSounds()
        {
        }

        private void manageFadeOverBuildings(VillageMapPerson worker, VillageMapBuilding building, VillageMapBuilding destBuilding)
        {
            PointF tf = worker.getPos();
            Point point = new Point((int) tf.X, (int) tf.Y);
            List<long> list = new List<long>();
            for (int i = 0; i < 0x10; i++)
            {
                Point point2 = new Point((point.X - 8) + i, point.Y + 5);
                Point point3 = new Point((point.X - 8) + i, point.Y - 30);
                long item = villageClickMask.getBuildingIDFromMap(point2.X, point2.Y);
                long num3 = villageClickMask.getBuildingIDFromMap(point3.X, point3.Y);
                if (item >= 0L)
                {
                    list.Add(item);
                }
                if (num3 >= 0L)
                {
                    list.Add(num3);
                }
            }
            for (int j = 0; j < 0x23; j++)
            {
                Point point4 = new Point(point.X - 8, (point.Y - 30) + j);
                Point point5 = new Point(point.X + 8, (point.Y - 30) + j);
                long num5 = villageClickMask.getBuildingIDFromMap(point4.X, point4.Y);
                long num6 = villageClickMask.getBuildingIDFromMap(point5.X, point5.Y);
                if (num5 >= 0L)
                {
                    list.Add(num5);
                }
                if (num6 >= 0L)
                {
                    list.Add(num6);
                }
            }
            if (((list.Count == 0) || ((destBuilding != null) && list.Contains(destBuilding.buildingID))) || list.Contains(building.buildingID))
            {
                worker.fadeToSolid();
            }
            else
            {
                worker.fadeToTransparent();
            }
        }

        public void manageWorkingSounds(VillageMapBuilding building)
        {
        }

        private void mergePopEvents(PopEventData[] popEvents)
        {
            bool flag = false;
            foreach (PopEventData data in popEvents)
            {
                if (data.eventType == 1)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                if (popEvents.Length == 3)
                {
                    if (popEvents[0].endTime < popEvents[1].endTime)
                    {
                        PopEventData data2 = popEvents[0];
                        popEvents[0] = popEvents[1];
                        popEvents[1] = data2;
                    }
                    if (popEvents[1].endTime < popEvents[2].endTime)
                    {
                        PopEventData data3 = popEvents[1];
                        popEvents[1] = popEvents[2];
                        popEvents[2] = data3;
                    }
                    if (popEvents[0].endTime < popEvents[1].endTime)
                    {
                        PopEventData data4 = popEvents[0];
                        popEvents[0] = popEvents[1];
                        popEvents[1] = data4;
                    }
                }
                else if ((popEvents.Length == 2) && (popEvents[0].endTime < popEvents[1].endTime))
                {
                    PopEventData data5 = popEvents[0];
                    popEvents[0] = popEvents[1];
                    popEvents[1] = data5;
                }
            }
            List<PopEventData> list = new List<PopEventData>();
            foreach (PopEventData data6 in popEvents)
            {
                if (data6.eventType == 0x2af9)
                {
                    bool flag2 = false;
                    foreach (PopEventData data7 in list)
                    {
                        if (data7.eventType == 0x2af9)
                        {
                            data7.eventEffect += data6.eventEffect;
                            data7.numIndividualEvents++;
                            flag2 = true;
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        list.Add(data6);
                    }
                    continue;
                }
                if (data6.eventType == 0x2afa)
                {
                    bool flag3 = false;
                    foreach (PopEventData data8 in list)
                    {
                        if (data8.eventType == 0x2afa)
                        {
                            data8.eventEffect += data6.eventEffect;
                            data8.numIndividualEvents++;
                            flag3 = true;
                            break;
                        }
                    }
                    if (!flag3)
                    {
                        list.Add(data6);
                    }
                    continue;
                }
                if (data6.eventType == 0x2afb)
                {
                    bool flag4 = false;
                    foreach (PopEventData data9 in list)
                    {
                        if (data9.eventType == 0x2afb)
                        {
                            data9.eventEffect += data6.eventEffect;
                            data9.numIndividualEvents++;
                            flag4 = true;
                            break;
                        }
                    }
                    if (!flag4)
                    {
                        list.Add(data6);
                    }
                    continue;
                }
                if (data6.eventType == 0x2775)
                {
                    bool flag5 = false;
                    foreach (PopEventData data10 in list)
                    {
                        if (data10.eventType == 0x2775)
                        {
                            if (data6.endTime > data10.endTime)
                            {
                                data10.endTime = data6.endTime;
                            }
                            data10.eventEffect += data6.eventEffect;
                            if (data10.eventEffect > 100)
                            {
                                data10.eventEffect = 100;
                            }
                            data10.numIndividualEvents++;
                            flag5 = true;
                            break;
                        }
                    }
                    if (!flag5)
                    {
                        list.Add(data6);
                    }
                    continue;
                }
                if (data6.eventType == 0x2776)
                {
                    bool flag6 = false;
                    foreach (PopEventData data11 in list)
                    {
                        if (data11.eventType == 0x2776)
                        {
                            if (data6.endTime > data11.endTime)
                            {
                                data11.endTime = data6.endTime;
                            }
                            data11.eventEffect += data6.eventEffect;
                            if (data11.eventEffect < -100)
                            {
                                data11.eventEffect = -100;
                            }
                            data11.numIndividualEvents++;
                            flag6 = true;
                            break;
                        }
                    }
                    if (!flag6)
                    {
                        list.Add(data6);
                    }
                    continue;
                }
                list.Add(data6);
            }
            CardData userCardData = GameEngine.Instance.World.UserCardData;
            for (int i = 0; i < userCardData.cards.Length; i++)
            {
                int card = userCardData.cards[i];
                if (CardTypes.getCardType(card) == 0xb0f)
                {
                    PopEventData item = new PopEventData {
                        endTime = userCardData.cardsExpiry[i],
                        eventEffect = (int) CardTypes.getCardEffectValue(0xb0f),
                        eventType = 0x4e21,
                        villageID = this.m_villageID
                    };
                    list.Add(item);
                }
            }
            this.m_popEvents = list.ToArray();
        }

        public void monitorWeaponProduction()
        {
            if (!GameEngine.Instance.World.isCapital(this.m_villageID))
            {
                DateTime now = DateTime.Now;
                TimeSpan span = (TimeSpan) (now - this.weaponProductionLastTimeRequest);
                if (span.TotalMinutes >= 5.0)
                {
                    bool flag = false;
                    DateTime time2 = getCurrentServerTime();
                    if (((this.m_toBeMade_Bows > 0.0) || (this.m_toBeMade_Pikes > 0.0)) || (((this.m_toBeMade_Swords > 0.0) || (this.m_toBeMade_Armour > 0.0)) || (this.m_toBeMade_Catapults > 0.0)))
                    {
                        if (((this.m_toBeMade_Bows > 0.0) && (this.m_productionRate_Bows > 0.0)) && (time2 > this.m_productionEnd_Bows.AddSeconds(2.0)))
                        {
                            this.m_bowsLevel += this.m_toBeMade_Bows;
                            this.m_toBeMade_Bows = 0.0;
                            flag = true;
                        }
                        if (((this.m_toBeMade_Pikes > 0.0) && (this.m_productionRate_Pikes > 0.0)) && (time2 > this.m_productionEnd_Pikes.AddSeconds(2.0)))
                        {
                            this.m_pikesLevel += this.m_toBeMade_Pikes;
                            this.m_toBeMade_Pikes = 0.0;
                            flag = true;
                        }
                        if (((this.m_toBeMade_Swords > 0.0) && (this.m_productionRate_Swords > 0.0)) && (time2 > this.m_productionEnd_Swords.AddSeconds(2.0)))
                        {
                            this.m_swordsLevel += this.m_toBeMade_Swords;
                            this.m_toBeMade_Swords = 0.0;
                            flag = true;
                        }
                        if (((this.m_toBeMade_Armour > 0.0) && (this.m_productionRate_Armour > 0.0)) && (time2 > this.m_productionEnd_Armour.AddSeconds(2.0)))
                        {
                            this.m_armourLevel += this.m_toBeMade_Armour;
                            this.m_toBeMade_Armour = 0.0;
                            flag = true;
                        }
                        if (((this.m_toBeMade_Catapults > 0.0) && (this.m_productionRate_Catapults > 0.0)) && (time2 > this.m_productionEnd_Catapults.AddSeconds(2.0)))
                        {
                            this.m_catapultsLevel += this.m_toBeMade_Catapults;
                            this.m_toBeMade_Catapults = 0.0;
                            flag = true;
                        }
                    }
                    if (flag && !this.ViewOnly)
                    {
                        this.weaponProductionLastTimeRequest = now;
                        RemoteServices.Instance.set_UpdateVillageResourcesInfo_UserCallBack(new RemoteServices.UpdateVillageResourcesInfo_UserCallBack(this.updateVillageResourcesInfoCallback));
                        RemoteServices.Instance.UpdateVillageResourcesInfo(this.m_villageID);
                    }
                }
            }
        }

        public void mouseClicked(Point mousePos)
        {
            bool flag = true;
            if (!GameEngine.Instance.World.isCapital(this.m_villageID))
            {
                flag = false;
                if (InterfaceMgr.Instance.clickDXCardBar(mousePos))
                {
                    return;
                }
                if ((GameEngine.Instance.World.isTutorialActive() && (mousePos.X < 0x40)) && (mousePos.Y >= (this.gfx.ViewportHeight - 0x40)))
                {
                    GameEngine.Instance.World.forceTutorialToBeShown();
                    return;
                }
            }
            if ((mousePos.X > (this.gfx.ViewportWidth - 0x20)) && (mousePos.Y < 0x20))
            {
                if (!flag)
                {
                    CustomSelfDrawPanel.WikiLinkControl.openHelpLink(1);
                }
                else
                {
                    CustomSelfDrawPanel.WikiLinkControl.openHelpLink(9);
                }
            }
            else if (placementSprite != null)
            {
                this.placeBuilding(mousePos);
            }
            else
            {
                this.clearColouredBuildings();
                Point loc = mousePos;
                loc.X -= (int) this.backgroundSprite.DrawPos.X;
                loc.Y -= (int) this.backgroundSprite.DrawPos.Y;
                long buildingID = this.getBuildingAtPoint(loc);
                VillageMapBuilding building = this.getBuildingFromID(buildingID);
                if (building != null)
                {
                    if (building.goTransparent)
                    {
                        villageClickMask.mapDirty = true;
                        villageClickMask.ignoredBuildingID = buildingID;
                        long num2 = villageClickMask.getBuildingIDFromMap(loc.X, loc.Y);
                        if (num2 < 0L)
                        {
                            return;
                        }
                        building = this.getBuildingFromID(num2);
                    }
                    this.selectBuilding(building);
                }
            }
        }

        public void mouseDrag(Point mousePos)
        {
            if (!this.m_leftMouseHeldDown)
            {
                this.m_lastMousePressedTime = DXTimer.GetCurrentMilliseconds();
                this.m_leftMouseHeldDown = true;
                this.m_baseMousePos = mousePos;
                this.m_baseScreenX = this.backgroundSprite.PosX;
                this.m_baseScreenY = this.backgroundSprite.PosY;
                this.m_leftMouseGrabbed = false;
            }
            if ((((DXTimer.GetCurrentMilliseconds() - this.m_lastMousePressedTime) > 250.0) || (Math.Abs((int) (this.m_baseMousePos.X - mousePos.X)) > 3)) || (Math.Abs((int) (this.m_baseMousePos.Y - mousePos.Y)) > 3))
            {
                CursorManager.SetCursor(CursorManager.CursorType.Hand, InterfaceMgr.Instance.ParentForm);
                this.m_leftMouseGrabbed = true;
                int num2 = this.m_baseMousePos.X - mousePos.X;
                int num3 = this.m_baseMousePos.Y - mousePos.Y;
                this.backgroundSprite.PosX = ((float) this.m_baseScreenX) - num2;
                this.backgroundSprite.PosY = ((float) this.m_baseScreenY) - num3;
                this.moveMap(0, 0);
                if ((GameEngine.Instance.World.getTutorialStage() == 0x69) && !GameEngine.Instance.World.TutorialIsAdvancing())
                {
                    GameEngine.Instance.World.advanceTutorial();
                }
            }
        }

        public void mouseHoverOverPoint(Point loc)
        {
            bool flag = false;
            VillageMapBuilding building = null;
            long num = -1L;
            foreach (VillageMapBuilding building2 in this.localBuildings)
            {
                if (building2.goTransparent)
                {
                    building = building2;
                    break;
                }
            }
            long buildingID = villageClickMask.getBuildingIDFromMap(loc.X, loc.Y);
            if (buildingID >= 0L)
            {
                VillageMapBuilding building3 = this.getBuildingFromID(buildingID);
                if (building3 != null)
                {
                    if ((building3.buildingType == 3) && !GameEngine.shiftPressed)
                    {
                        if (this.granaryOpenCount == 0)
                        {
                            building3.open = true;
                            this.granaryOpenCount = 30;
                            this.updateGFXState(building3);
                            building3.updateGranary(this.gfx, this);
                        }
                        else
                        {
                            this.granaryOpenCount = 30;
                        }
                    }
                    else if (!building3.complete)
                    {
                        building3.showFullConstructionText = true;
                    }
                    else if (GameEngine.shiftPressed)
                    {
                        num = building3.buildingID;
                        building3.goTransparent = true;
                        Color color = Color.FromArgb(0x60, 0xff, 0xff, 0xff);
                        building3.baseSprite.ColorToUse = color;
                        if (building3.animSprite != null)
                        {
                            building3.animSprite.ColorToUse = color;
                        }
                        if (building3.extraAnimSprite1 != null)
                        {
                            building3.extraAnimSprite1.ColorToUse = color;
                        }
                        if (building3.extraAnimSprite2 != null)
                        {
                            building3.extraAnimSprite2.ColorToUse = color;
                        }
                    }
                    if ((this.m_parishCapitalResearchData != null) && GameEngine.Instance.World.isCapital(this.m_villageID))
                    {
                        bool flag2 = false;
                        int rank = this.m_parishCapitalResearchData.getCapitalResourceFromBuildingType(building3.buildingType);
                        if (rank < 0)
                        {
                            flag2 = true;
                        }
                        else if (((VillageBuildingsData.getRequiredResourceType(building3.buildingType, 0) >= 0) && (building3.capitalResourceLevels.Length > 0)) && (VillageBuildingsData.getRequiredResourceTypeLevel(building3.buildingType, 0, rank, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld) <= 0))
                        {
                            flag2 = true;
                        }
                        if (flag2)
                        {
                            CustomTooltipManager.MouseEnterTooltipArea(150, building3.buildingType);
                        }
                        else
                        {
                            CustomTooltipManager.MouseEnterTooltipArea(0x97, building3.buildingType);
                        }
                        flag = true;
                    }
                }
            }
            if ((building != null) && (num != building.buildingID))
            {
                building.goTransparent = false;
                Color white = ARGBColors.White;
                building.baseSprite.ColorToUse = white;
                if (building.animSprite != null)
                {
                    building.animSprite.ColorToUse = white;
                }
                if (building.extraAnimSprite1 != null)
                {
                    building.extraAnimSprite1.ColorToUse = white;
                }
                if (building.extraAnimSprite2 != null)
                {
                    building.extraAnimSprite2.ColorToUse = white;
                }
            }
            if (flag)
            {
                this.tooltipWasVisble = true;
            }
            else if (this.tooltipWasVisble)
            {
                CustomTooltipManager.MouseLeaveTooltipArea();
            }
        }

        public void mouseMoveUpdate(Point mousePos, bool mouseDown)
        {
            if (this.backgroundSprite != null)
            {
                if (!GameEngine.Instance.World.isCapital(this.m_villageID))
                {
                    InterfaceMgr.Instance.mouseMoveDXCardBar(mousePos);
                }
                this.m_previousMousePos = this.m_lastMousePos;
                this.m_lastMousePos = mousePos;
                Point loc = this.mouseToVillagePoint(mousePos);
                if ((mousePos.X > (this.gfx.ViewportWidth - 0x20)) && (mousePos.Y < 0x20))
                {
                    this.overWikiHelp = true;
                    CustomTooltipManager.MouseEnterTooltipArea(0x1130, 1);
                }
                else
                {
                    this.overWikiHelp = false;
                }
                if (mouseDown)
                {
                    this.mouseDrag(mousePos);
                }
                this.mouseHoverOverPoint(loc);
                this.movePlaceBuilding(mousePos);
            }
        }

        public void mouseNotClicked(Point mousePos)
        {
            if (this.m_leftMouseHeldDown)
            {
                if (!this.m_leftMouseGrabbed)
                {
                    this.mouseClicked(mousePos);
                }
                this.m_leftMouseHeldDown = false;
                this.m_leftMouseGrabbed = false;
                CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
            }
        }

        public Point mouseToVillagePoint(Point mousePos)
        {
            if (this.backgroundSprite == null)
            {
                return mousePos;
            }
            Point point = mousePos;
            point.X -= (int) this.backgroundSprite.DrawPos.X;
            point.Y -= (int) this.backgroundSprite.DrawPos.Y;
            return point;
        }

        public void moveMap(int dx, int dy)
        {
            if (this.backgroundSprite != null)
            {
                this.backgroundSprite.move(dx, dy);
                this.backgroundSprite.keepBounded();
                this.backgroundSprite.centreSmallerSprite();
                this.backgroundSprite.fixup2DPos();
            }
        }

        public bool movePlaceBuilding(Point mousePos)
        {
            bool flag = false;
            this.placementError = 0;
            if ((placementSprite != null) && (this.backgroundSprite != null))
            {
                Point buildingLocation = mousePos;
                buildingLocation.X += -((int) this.backgroundSprite.DrawPos.X) + 0x10;
                buildingLocation.Y += -((int) this.backgroundSprite.DrawPos.Y) + 8;
                buildingLocation.X /= 0x20;
                buildingLocation.Y /= 0x10;
                this.lastPlaceBuildingLoc = buildingLocation;
                if (((buildingLocation.X >= 0) && (buildingLocation.X < this.layout.gridWidth)) && ((buildingLocation.Y >= 0) && (buildingLocation.Y < this.layout.gridHeight)))
                {
                    placementSprite.PosX = buildingLocation.X * 0x20;
                    placementSprite.PosY = (buildingLocation.Y * 0x10) + 8;
                    VillageLayoutNew layout = null;
                    if (placingAsFree)
                    {
                        layout = this.buildMoveBuildingLayout();
                    }
                    if (layout == null)
                    {
                        layout = this.layout;
                    }
                    int[] buildingArray = VillageBuildingsData.getBuildingLayout(s_villageBuildingData[placementType].size);
                    ErrorCodes.ErrorCode code = VillageLayoutNew.checkBuildingAgainstLandscape(layout.mapData, buildingArray, buildingLocation, placementType, this.layout.gridWidth, this.layout.gridHeight);
                    if (code != ErrorCodes.ErrorCode.OK)
                    {
                        flag = true;
                        this.placementError = 1;
                        ErrorCodes.ErrorCode code2 = code;
                        if (VillageLayoutNew.checkBuildingAgainstOtherBuildings(layout.mapData, buildingArray, buildingLocation, placementType) == ErrorCodes.ErrorCode.OK)
                        {
                            if ((placementType == 6) || (placementType == 0x15))
                            {
                                this.placementError = 7;
                            }
                            else if (placementType == 7)
                            {
                                this.placementError = 8;
                            }
                            else if ((placementType == 8) || (placementType == 0x1a))
                            {
                                this.placementError = 9;
                            }
                            else if (placementType == 9)
                            {
                                this.placementError = 10;
                            }
                            else if (placementType == 0x12)
                            {
                                this.placementError = 11;
                            }
                            else if (placementType == 0x17)
                            {
                                this.placementError = 12;
                            }
                            else if ((placementType == 0x19) || (placementType == 0x18))
                            {
                                this.placementError = 13;
                            }
                        }
                        code = code2;
                    }
                    else if (VillageLayoutNew.checkBuildingAgainstOtherBuildings(layout.mapData, buildingArray, buildingLocation, placementType) != ErrorCodes.ErrorCode.OK)
                    {
                        flag = true;
                        this.placementError = 1;
                    }
                    if (!placingAsFree && !this.genericBuildingValidation(buildingLocation, placementType))
                    {
                        flag = true;
                        this.placementError = 2;
                    }
                }
                else
                {
                    flag = true;
                }
                if (!flag)
                {
                    if (!placingAsFree)
                    {
                        int num = this.getMaxBuildingQueueLength();
                        if (this.countNumBuildingsConstructing() >= num)
                        {
                            flag = true;
                            this.placementError = 3;
                            placementSprite.ColorToUse = Color.FromArgb(0x80, 0x80, 0x80, 0xff);
                        }
                        else
                        {
                            int woodNeeded = 0;
                            int stoneNeeded = 0;
                            int clayNeeded = 0;
                            int goldNeeded = 0;
                            int flagsNeeded = 0;
                            int matchedCard = -1;
                            if (!CardTypes.isFreeBuildingPlacement(GameEngine.Instance.World.UserCardData, placementType, ref matchedCard))
                            {
                                VillageBuildingsData.calcBuildingCosts(GameEngine.Instance.LocalWorldData, placementType, this.countBuildingType(placementType), ref woodNeeded, ref stoneNeeded, ref clayNeeded, ref goldNeeded, GameEngine.Instance.World.UserResearchData.Research_Tools, ref flagsNeeded);
                            }
                            if (((flagsNeeded > 0) && (GameEngine.Instance.LocalWorldData.constrFlagCost[placementType] > 0)) && ((this.m_capitalBuildingsBuilt != null) && this.m_capitalBuildingsBuilt.Contains(placementType)))
                            {
                                flagsNeeded = 0;
                            }
                            StockpileLevels levels = new StockpileLevels();
                            this.getStockpileLevels(levels);
                            double capitalGold = 0.0;
                            if (!GameEngine.Instance.World.isCapital(this.m_villageID))
                            {
                                capitalGold = GameEngine.Instance.World.getCurrentGold();
                            }
                            else
                            {
                                capitalGold = this.m_capitalGold;
                            }
                            if ((((woodNeeded > 0) && (woodNeeded > levels.woodLevel)) || ((stoneNeeded > 0) && (stoneNeeded > levels.stoneLevel))) || (((goldNeeded > 0) && (goldNeeded > capitalGold)) || ((flagsNeeded > 0) && (flagsNeeded > this.m_numParishFlags))))
                            {
                                flag = true;
                                if ((goldNeeded > 0) && (goldNeeded > capitalGold))
                                {
                                    this.placementError = 4;
                                }
                                else if ((flagsNeeded > 0) && (flagsNeeded > this.m_numParishFlags))
                                {
                                    this.placementError = 5;
                                }
                                else
                                {
                                    this.placementError = 6;
                                }
                                placementSprite.ColorToUse = Color.FromArgb(0x80, 0xff, 0xff, 0);
                            }
                            else
                            {
                                placementSprite.ColorToUse = ARGBColors.White;
                            }
                        }
                    }
                    else
                    {
                        placementSprite.ColorToUse = ARGBColors.White;
                    }
                }
                else
                {
                    placementSprite.ColorToUse = Color.FromArgb(0x80, 0xff, 0, 0);
                }
                placementSprite.Visible = !this.m_leftMouseGrabbed;
            }
            return !flag;
        }

        public void movePlacedCallback(MoveVillageBuilding_ReturnType returnData)
        {
            if (returnData.Success)
            {
                VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
                if (map != null)
                {
                    map.importVillageBuildings(returnData.villageBuildings, true);
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    setServerTime(returnData.currentTime);
                    map.reAddBuildingsToMap();
                }
            }
        }

        public bool needParishPeople()
        {
            if (this.m_parishPeople == null)
            {
                return true;
            }
            TimeSpan span = (TimeSpan) (getCurrentServerTime() - this.m_lastParishPeopleTime);
            return (span.TotalMinutes > 30.0);
        }

        public int numBuildingsOfType(int buildingType)
        {
            int num = 0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if (building.buildingType == buildingType)
                {
                    num++;
                }
            }
            return num;
        }

        public int numCapitalBuildings()
        {
            return this.localBuildings.Count;
        }

        public int numFreeTraders()
        {
            return this.m_numTradersAtHome;
        }

        public int numParishFlags()
        {
            return this.m_numParishFlags;
        }

        public int numTraders()
        {
            int numTradersAtHome = this.m_numTradersAtHome;
            foreach (MarketTraderData data in this.traders)
            {
                numTradersAtHome += data.numTraders;
            }
            return numTradersAtHome;
        }

        public int numWorkingBuildingsOfType(int buildingType)
        {
            int num = 0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if ((building.buildingType == buildingType) && (building.calcRate > 0.0))
                {
                    num++;
                }
            }
            return num;
        }

        public void placeBuilding(Point mousePos)
        {
            if (this.movePlaceBuilding(mousePos))
            {
                Point buildingLocation = mousePos;
                buildingLocation.X += -((int) this.backgroundSprite.DrawPos.X) + 0x10;
                buildingLocation.Y += -((int) this.backgroundSprite.DrawPos.Y) + 8;
                buildingLocation.X /= 0x20;
                buildingLocation.Y /= 0x10;
                if (m_movingBuilding != null)
                {
                    GameEngine.Instance.playInterfaceSound("VillageMap_move_building");
                    RemoteServices.Instance.set_MoveVillageBuilding_UserCallBack(new RemoteServices.MoveVillageBuilding_UserCallBack(this.movePlacedCallback));
                    RemoteServices.Instance.MoveVillageBuilding(this.m_villageID, m_movingBuilding.buildingID, buildingLocation);
                    m_movingBuilding.buildingLocation = buildingLocation;
                    if (m_movingBuilding.shadowSprite != null)
                    {
                        m_movingBuilding.shadowSprite.PosX = m_movingBuilding.buildingLocation.X * 0x20;
                        m_movingBuilding.shadowSprite.PosY = (m_movingBuilding.buildingLocation.Y * 0x10) + 8;
                    }
                    else
                    {
                        m_movingBuilding.baseSprite.PosX = m_movingBuilding.buildingLocation.X * 0x20;
                        m_movingBuilding.baseSprite.PosY = (m_movingBuilding.buildingLocation.Y * 0x10) + 8;
                    }
                    this.stopPlaceBuilding(true);
                }
                else
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - this.lastBuildingPlacement);
                    if ((span.TotalSeconds >= 45.0) || !this.inPlaceBuilding)
                    {
                        GameEngine.Instance.playInterfaceSound("VillageMap_place_building");
                        this.lastBuildingPlacement = DateTime.Now;
                        this.inPlaceBuilding = true;
                        RemoteServices.Instance.set_PlaceVillageBuilding_UserCallBack(new RemoteServices.PlaceVillageBuilding_UserCallBack(this.buildingPlacedCallback));
                        RemoteServices.Instance.PlaceVillageBuilding(this.m_villageID, placementType, buildingLocation);
                        Sound.playInterfaceSound(0x2711);
                        VillageMapBuilding newBuilding = new VillageMapBuilding {
                            buildingLocation = buildingLocation,
                            buildingType = placementType,
                            buildingID = -1L,
                            complete = false,
                            completionTime = DateTime.Now.AddDays(1000.0)
                        };
                        this.addBuildingToMap(newBuilding, buildingLocation, placementType);
                        newBuilding.updateConstructionGFX(localBaseTime, baseServerTime, true, this);
                        this.startPlaceBuilding_ShowPanel(placementType, "", false);
                        if ((placementType == 2) || (placementType >= 0x4f))
                        {
                            InterfaceMgr.Instance.villageReshowAfterStockpilePlaced();
                        }
                    }
                }
            }
            else
            {
                UniversalDebugLog.Log("placement failed");
            }
        }

        public void placeBuildingWhereItIs()
        {
            Point mousePos = new Point(((int) placementSprite.DrawPos.X) - 0x10, ((int) placementSprite.DrawPos.Y) - 8);
            this.placeBuilding(mousePos);
        }

        public void playEnvironmentalSounds()
        {
            if ((this.m_villageMapType >= 10) && (this.m_villageMapType <= 13))
            {
                if (this.localBuildings.Count < 4)
                {
                    Sound.playVillageEnvironmental(14);
                }
                else if (this.localBuildings.Count < 10)
                {
                    Sound.playVillageEnvironmental(15);
                }
                else
                {
                    Sound.playVillageEnvironmental(0x10);
                }
            }
            else
            {
                Sound.playVillageEnvironmental(this.m_villageMapType);
            }
        }

        private void preCountHonourBuildings()
        {
            this.m_preCountedChurches = 0;
            this.m_preCountedChapels = 0;
            this.m_preCountedCathedrals = 0;
            this.m_preCountedSmallGardens = 0;
            this.m_preCountedLargeGardens = 0;
            this.m_preCountedSmallStatues = 0;
            this.m_preCountedLargeStatues = 0;
            this.m_preCountedDovecotes = 0;
            this.m_preCountedStocks = 0;
            this.m_preCountedBurningPosts = 0;
            this.m_preCountedGibbets = 0;
            this.m_preCountedRacks = 0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if (building.isComplete())
                {
                    switch (building.buildingType)
                    {
                        case 0x22:
                            this.m_preCountedChapels++;
                            break;

                        case 0x24:
                            this.m_preCountedChurches++;
                            break;

                        case 0x25:
                            this.m_preCountedCathedrals++;
                            break;

                        case 0x26:
                        case 0x29:
                        case 0x2a:
                        case 0x2b:
                        case 0x2c:
                        case 0x2d:
                            this.m_preCountedSmallGardens++;
                            break;

                        case 0x31:
                        case 50:
                        case 0x33:
                            this.m_preCountedLargeGardens++;
                            break;

                        case 0x36:
                        case 0x37:
                        case 0x38:
                        case 0x39:
                            this.m_preCountedSmallStatues++;
                            break;

                        case 0x3a:
                        case 0x3b:
                            this.m_preCountedLargeStatues++;
                            break;

                        case 60:
                            this.m_preCountedDovecotes++;
                            break;

                        case 0x3d:
                            this.m_preCountedStocks++;
                            break;

                        case 0x3e:
                            this.m_preCountedBurningPosts++;
                            break;

                        case 0x3f:
                            this.m_preCountedGibbets++;
                            break;

                        case 0x40:
                            this.m_preCountedRacks++;
                            break;
                    }
                }
            }
        }

        public void produceWeaponsCallback(VillageProduceWeapons_ReturnType returnData)
        {
            if (returnData.Success)
            {
                VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
                if (map != null)
                {
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                }
                setServerTime(returnData.currentTime);
            }
        }

        public void randomiseSounds()
        {
        }

        public void reAddBuildingsToMap()
        {
            villageClickMask.clearMapAndBuildings();
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                this.reAddBuildingToMap(building);
            }
        }

        private void reAddBuildingToMap(VillageMapBuilding newBuilding)
        {
            int buildingType = newBuilding.buildingType;
            int num2 = 0;
            if (buildingType == 1)
            {
                switch (GameEngine.Instance.World.UserResearchData.Research_HousingCapacity)
                {
                    case 2:
                    case 3:
                        buildingType = 0x27;
                        break;

                    case 4:
                    case 5:
                        buildingType = 40;
                        break;

                    case 6:
                        buildingType = 0x4c;
                        break;

                    case 7:
                    case 8:
                    case 9:
                        buildingType = 0x4d;
                        break;
                }
            }
            else if (buildingType == 0)
            {
                int num3 = GameEngine.Instance.World.getRank();
                if (num3 < 10)
                {
                    num2 = 0;
                }
                else if (num3 < 15)
                {
                    num2 = 3;
                }
                else if (num3 < 0x15)
                {
                    num2 = 6;
                }
                else
                {
                    num2 = 6;
                }
            }
            if (s_villageBuildingData[buildingType].baseGfxTexID >= 0)
            {
                PointF center = new PointF {
                    X = s_villageBuildingData[buildingType].baseOffset.X,
                    Y = s_villageBuildingData[buildingType].baseOffset.Y
                };
                villageClickMask.addBuilding(newBuilding.buildingID, newBuilding.buildingLocation.X * 0x20, (newBuilding.buildingLocation.Y * 0x10) + 8, s_villageBuildingData[buildingType].baseGfxTexID, s_villageBuildingData[buildingType].baseGfxID + num2, center);
            }
        }

        public void refreshTraderNumbers()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - this.lastTraderRefresh);
            if (span.TotalSeconds > 60.0)
            {
                this.lastTraderRefresh = DateTime.Now;
                RemoteServices.Instance.set_GetUserTraders_UserCallBack(new RemoteServices.GetUserTraders_UserCallBack(this.getUserTradersCallback));
                RemoteServices.Instance.GetUserTraders(this.m_villageID);
            }
        }

        public void reInitGFX(GraphicsMgr mgr)
        {
            if (this.backgroundSprite == null)
            {
                this.initGFX(mgr);
            }
            RemoteServices.Instance.set_VillageProduceWeapons_UserCallBack(new RemoteServices.VillageProduceWeapons_UserCallBack(this.produceWeaponsCallback));
            RemoteServices.Instance.set_VillageHoldBanquet_UserCallBack(new RemoteServices.VillageHoldBanquet_UserCallBack(this.holdBanquetCallback));
        }

        private void reInitializeSecondaryBuilding(VillageMapBuilding building, double calcRate, VillageMapBuilding destBuilding, VillageMapBuilding sourceBuilding)
        {
            if ((destBuilding != null) && (sourceBuilding != null))
            {
                double num = GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, building.buildingType, false) * CardTypes.getResourceCapMultiplier(building.buildingType, GameEngine.Instance.World.UserCardData);
                double num2 = this.getResourceLevel(building.buildingType);
                if (num > num2)
                {
                    WorldData localWorldData = GameEngine.Instance.LocalWorldData;
                    double num3 = this.getNumTrips(building.buildingType);
                    building.journeyTime2 = this.getJourneyTime(building.buildingLocation, destBuilding.buildingLocation);
                    building.journeyTime = this.getJourneyTime(building.buildingLocation, sourceBuilding.buildingLocation);
                    building.productionTime = (((building.serverCalcRate - (building.journeyTime2 * 2.0)) - GameEngine.Instance.LocalWorldData.WeaponProductionOffScreenTime) / num3) - (building.journeyTime * 2.0);
                    building.calcRate = building.serverCalcRate;
                    building.tripCalcRate = ((building.serverCalcRate - (building.journeyTime2 * 2.0)) - GameEngine.Instance.LocalWorldData.WeaponProductionOffScreenTime) / num3;
                }
                else
                {
                    building.productionTime = 0.0;
                    building.journeyTime = 0.0;
                    building.journeyTime2 = 0.0;
                    if (building.productionState == 0)
                    {
                        building.productionState = 5;
                    }
                    building.calcRate = 0.0;
                    building.tripCalcRate = 0.0;
                    building.serverCalcRate = 0.0;
                }
            }
            else
            {
                building.productionTime = 0.0;
                building.journeyTime = 0.0;
                building.journeyTime2 = 0.0;
            }
        }

        public void releaseTouch()
        {
            this.m_leftMouseHeldDown = false;
            this.m_leftMouseGrabbed = false;
        }

        private void removeAnimals(VillageMapBuilding building)
        {
            VillageAnimalCollection animals = (VillageAnimalCollection) this.animalArray[building.buildingID];
            if (animals != null)
            {
                foreach (VillageAnimal animal in animals.animals)
                {
                    animal.dispose();
                }
                animals.animals.Clear();
            }
            this.animalArray[building.buildingID] = null;
        }

        private void removeAnimals(long buildingID)
        {
            VillageAnimalCollection animals = (VillageAnimalCollection) this.animalArray[buildingID];
            if (animals != null)
            {
                foreach (VillageAnimal animal in animals.animals)
                {
                    animal.dispose();
                }
                animals.animals.Clear();
            }
            this.animalArray[buildingID] = null;
        }

        private void removeBuildingFromMap(Point location, int buildingType, long buildingID)
        {
            villageClickMask.removeBuilding(buildingID);
            this.removeAnimals(buildingID);
            if (GameEngine.Instance.World.isCapital(this.m_villageID) && (buildingID >= 0L))
            {
                foreach (VillageMapBuilding building in this.localBuildings)
                {
                    if (building.buildingID == buildingID)
                    {
                        buildingType = building.buildingType;
                        break;
                    }
                }
            }
            foreach (VillageMapBuilding building2 in this.localBuildings)
            {
                if (((building2.buildingLocation == location) || ((building2.buildingID == buildingID) && (location == Point.Empty))) && ((building2.buildingType == buildingType) && (((buildingID == -1L) || (building2.buildingID == buildingID)) || (building2.buildingID == -1L))))
                {
                    int[] numArray = VillageBuildingsData.getBuildingLayout(s_villageBuildingData[buildingType].size);
                    if (location == Point.Empty)
                    {
                        location = building2.buildingLocation;
                    }
                    for (int i = 0; i < (numArray.Length / 2); i++)
                    {
                        int index = location.X + numArray[i * 2];
                        int num3 = location.Y + numArray[(i * 2) + 1];
                        if (((index >= 0) && (num3 >= 0)) && ((index < 0x40) && (num3 < 0x80)))
                        {
                            this.layout.mapData[num3][index] &= -16385;
                        }
                    }
                    if (building2.shadowSprite != null)
                    {
                        building2.shadowSprite.RemoveAllChildren();
                        this.backgroundSprite.RemoveChild(building2.shadowSprite);
                        building2.shadowSprite = null;
                    }
                    if (building2.baseSprite != null)
                    {
                        this.backgroundSprite.RemoveChild(building2.baseSprite);
                        building2.baseSprite = null;
                    }
                    if (building2.animSprite != null)
                    {
                        this.backgroundSprite.RemoveChild(building2.animSprite);
                        building2.animSprite = null;
                    }
                    if (building2.extraAnimSprite1 != null)
                    {
                        this.backgroundSprite.RemoveChild(building2.extraAnimSprite1);
                        building2.extraAnimSprite1 = null;
                    }
                    if (building2.extraAnimSprite2 != null)
                    {
                        this.backgroundSprite.RemoveChild(building2.extraAnimSprite2);
                        building2.extraAnimSprite2 = null;
                    }
                    if (building2.worker != null)
                    {
                        building2.worker.dispose();
                        building2.worker = null;
                    }
                    if (building2.stockpileExtension != null)
                    {
                        building2.stockpileExtension.dispose();
                        building2.stockpileExtension = null;
                    }
                    if (building2.granaryExtension != null)
                    {
                        building2.granaryExtension.dispose();
                        building2.granaryExtension = null;
                    }
                    this.localBuildings.Remove(building2);
                    break;
                }
            }
        }

        public void removeChildSprite(SpriteWrapper sprite)
        {
            if (this.backgroundSprite != null)
            {
                this.backgroundSprite.RemoveChild(sprite);
            }
        }

        public void resetMapType(int mapID, int mapVariant, int mapType)
        {
            if (((mapID != this.m_mapID) || (this.m_mapVariant != mapVariant)) || (mapType != this.m_villageMapType))
            {
                this.m_mapID = mapID;
                this.m_mapVariant = mapVariant;
                this.m_villageMapType = mapType;
                this.layout = villageLayout[mapID].createClone();
                this.loadBackgroundImage();
                this.initGFX(this.gfx);
            }
        }

        private void runAnimals(VillageMapBuilding building, VillageMap vm, int tickRate)
        {
            VillageAnimalCollection animals = (VillageAnimalCollection) this.animalArray[building.buildingID];
            if (animals != null)
            {
                foreach (VillageAnimal animal in animals.animals)
                {
                    if (animal.id == 0)
                    {
                        animal.run(building, vm, null, tickRate);
                    }
                    else
                    {
                        animal.run(building, vm, animals.animals[0], tickRate);
                    }
                }
            }
        }

        public void runBuilding(VillageMapBuilding building)
        {
            if (building.isComplete())
            {
                double num;
                switch (building.buildingType)
                {
                    case 2:
                    case 0x23:
                        building.initStorageBuilding(this.gfx, this);
                        return;

                    case 3:
                        building.initStorageBuilding(this.gfx, this);
                        this.runAnimals(building, this, 1);
                        return;

                    case 4:
                    case 5:
                    case 20:
                    case 0x1b:
                    case 0x26:
                    case 0x27:
                    case 40:
                    case 0x29:
                    case 0x2a:
                    case 0x2b:
                    case 0x2c:
                    case 0x2d:
                    case 0x2e:
                    case 0x2f:
                    case 0x30:
                    case 0x31:
                    case 50:
                    case 0x33:
                    case 0x34:
                    case 0x35:
                    case 0x36:
                    case 0x37:
                    case 0x38:
                    case 0x39:
                    case 0x3a:
                    case 0x3b:
                    case 60:
                    case 70:
                    case 0x47:
                    case 0x48:
                    case 0x49:
                    case 0x4a:
                    case 0x4b:
                    case 0x4c:
                    case 0x4d:
                        return;

                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        this.runPrimaryResourceBuilding(building, 2);
                        return;

                    case 12:
                        this.runPrimaryResourceBuilding(building, 0x23);
                        return;

                    case 13:
                    case 0x10:
                    case 0x12:
                        this.runPrimaryResourceBuilding(building, 3);
                        this.runAnimals(building, this, 1);
                        return;

                    case 14:
                        this.runPrimaryResourceBuilding(building, 3);
                        if (!building.open)
                        {
                            building.extraAnimSprite2.changeBaseFrame(0x164);
                            return;
                        }
                        building.extraAnimSprite2.changeBaseFrame(0x173);
                        return;

                    case 15:
                        this.runPrimaryResourceBuilding(building, 3);
                        return;

                    case 0x11:
                        this.runPrimaryResourceBuilding(building, 3);
                        if ((building.secondaryWorker != null) && (building.secondaryWorker.workerSprite != null))
                        {
                            if ((building.worker == null) || (building.worker.workerSprite == null))
                            {
                                building.secondaryWorker.workerSprite.Visible = true;
                                return;
                            }
                            building.secondaryWorker.workerSprite.Visible = building.worker.workerSprite.Visible;
                        }
                        return;

                    case 0x13:
                        this.runPrimaryResourceBuilding(building, 0);
                        this.runAnimals(building, this, 1);
                        return;

                    case 0x15:
                    case 0x16:
                    case 0x17:
                    case 0x18:
                    case 0x19:
                    case 0x1a:
                    case 0x21:
                        this.runPrimaryResourceBuilding(building, 0);
                        return;

                    case 0x1c:
                    {
                        double serverCalcRate = building.serverCalcRate;
                        if ((this.m_toBeMade_Pikes <= 0.0) || (serverCalcRate <= 0.0))
                        {
                            serverCalcRate = 0.0;
                        }
                        else
                        {
                            double num4 = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
                            DateTime time2 = baseServerTime + new TimeSpan(0, 0, (int) num4);
                            if (time2 >= this.m_productionEnd_Pikes)
                            {
                                serverCalcRate = 0.0;
                            }
                        }
                        this.runSecondaryResourceBuilding(building, 4, 2, serverCalcRate);
                        return;
                    }
                    case 0x1d:
                    {
                        num = building.serverCalcRate;
                        if ((this.m_toBeMade_Bows <= 0.0) || (num <= 0.0))
                        {
                            num = 0.0;
                            break;
                        }
                        double num2 = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
                        DateTime time = baseServerTime + new TimeSpan(0, 0, (int) num2);
                        if (time >= this.m_productionEnd_Bows)
                        {
                            num = 0.0;
                        }
                        break;
                    }
                    case 30:
                    {
                        double calcRate = building.serverCalcRate;
                        if ((this.m_toBeMade_Swords <= 0.0) || (calcRate <= 0.0))
                        {
                            calcRate = 0.0;
                        }
                        else
                        {
                            double num6 = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
                            DateTime time3 = baseServerTime + new TimeSpan(0, 0, (int) num6);
                            if (time3 >= this.m_productionEnd_Swords)
                            {
                                calcRate = 0.0;
                            }
                        }
                        this.runSecondaryResourceBuilding(building, 4, 2, calcRate);
                        return;
                    }
                    case 0x1f:
                    {
                        double num7 = building.serverCalcRate;
                        if ((this.m_toBeMade_Armour <= 0.0) || (num7 <= 0.0))
                        {
                            num7 = 0.0;
                        }
                        else
                        {
                            double num8 = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
                            DateTime time4 = baseServerTime + new TimeSpan(0, 0, (int) num8);
                            if (time4 >= this.m_productionEnd_Armour)
                            {
                                num7 = 0.0;
                            }
                        }
                        this.runSecondaryResourceBuilding(building, 4, 2, num7);
                        return;
                    }
                    case 0x20:
                    {
                        double num9 = building.serverCalcRate;
                        if ((this.m_toBeMade_Catapults <= 0.0) || (num9 <= 0.0))
                        {
                            num9 = 0.0;
                        }
                        else
                        {
                            double num10 = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
                            DateTime time5 = baseServerTime + new TimeSpan(0, 0, (int) num10);
                            if (time5 >= this.m_productionEnd_Catapults)
                            {
                                num9 = 0.0;
                            }
                        }
                        this.runSecondaryResourceBuilding(building, 4, 2, num9);
                        return;
                    }
                    case 0x22:
                    case 0x24:
                    case 0x25:
                        this.manageWorkingSounds(building);
                        return;

                    case 0x3d:
                    case 0x3e:
                    case 0x3f:
                    case 0x40:
                    case 0x41:
                    case 0x42:
                    case 0x45:
                        building.open = true;
                        return;

                    case 0x43:
                        if (!building.open)
                        {
                            Random random = new Random();
                            building.open = true;
                            if (random.Next(100) >= 50)
                            {
                                return;
                            }
                            building.animSprite.changeBaseFrame(0x12);
                        }
                        return;

                    case 0x44:
                        building.open = true;
                        if (building.animSprite.isAnimFinished())
                        {
                            int baseFrame = building.animSprite.SpriteNo + 1;
                            if (baseFrame >= 8)
                            {
                                baseFrame = 0;
                            }
                            building.animSprite.changeBaseFrame(baseFrame);
                            building.animSprite.restartAnim();
                        }
                        return;

                    case 0x4e:
                        if (building.worker != null)
                        {
                            this.runMarketTrader(building);
                        }
                        return;

                    default:
                        return;
                }
                this.runSecondaryResourceBuilding(building, 4, 2, num);
            }
        }

        private void runMarketTrader(VillageMapBuilding building)
        {
            building.productionState++;
            if (building.productionState == 60)
            {
                building.secondaryWorker = new VillageMapPerson(this.gfx);
                building.secondaryWorker.setPos(building.buildingLocation);
                building.secondaryWorker.startJourney(VillageBuildingsData.tileToPixel(building.buildingLocation), Point.Truncate(building.worker.endPos), 0.0);
                this.initWalkingAnim(building);
            }
            if (building.worker != null)
            {
                if (building.worker.isJourneyOver())
                {
                    building.worker.dispose();
                    building.worker = null;
                }
                else
                {
                    this.manageFadeOverBuildings(building.worker, building, null);
                    building.worker.update();
                }
            }
            if (building.secondaryWorker != null)
            {
                if (building.secondaryWorker.isJourneyOver())
                {
                    building.secondaryWorker.dispose();
                    building.secondaryWorker = null;
                }
                else
                {
                    this.manageFadeOverBuildings(building.secondaryWorker, building, null);
                    building.secondaryWorker.update();
                }
            }
        }

        private void runPrimaryResourceBuilding(VillageMapBuilding building, int storageBuilding)
        {
            VillageMapBuilding building2 = null;
            Random random;
            if (building.calcRate > 0.0)
            {
                building2 = this.findBuildingType(storageBuilding);
            }
            if (building2 != null)
            {
                int num = 1;
                if (building.worker == null)
                {
                    building.worker = new VillageMapPerson(this.gfx);
                    building.worker.setPos(building.buildingLocation);
                    building.workerNeedsReInitializing = true;
                    building.worker.initWorkerSprite();
                }
                if (building.workerNeedsReInitializing)
                {
                    this.getDistanceThroughCycle(building);
                    double calcRate = building.calcRate;
                    building.journeyTime = this.getJourneyTime(building.buildingLocation, building2.buildingLocation);
                    building.productionTime = building.calcRate - (building.journeyTime * 2.0);
                    building.productionState = 0;
                    num = 2;
                    building.workerNeedsReInitializing = false;
                    this.initWorkingAnim(building, true);
                }
                for (int i = 0; i < num; i++)
                {
                    switch (building.productionState)
                    {
                        case 0:
                        {
                            double num3 = this.getDistanceThroughCycle(building) * building.calcRate;
                            if (num3 < building.productionTime)
                            {
                                break;
                            }
                            double distThroughJourney = (num3 - building.productionTime) / building.journeyTime;
                            building.worker.startJourneyTileBased(building.buildingLocation, building2.buildingLocation, distThroughJourney);
                            this.initCarryingAnim(building);
                            building.productionState = 1;
                            continue;
                        }
                        case 1:
                        {
                            if (building.worker.isJourneyOver())
                            {
                                double num5 = this.getDistanceThroughCycle(building) * building.calcRate;
                                double num6 = (num5 - (building.productionTime + building.journeyTime)) / building.journeyTime;
                                building.worker.startJourneyTileBased(building2.buildingLocation, building.buildingLocation, num6);
                                this.initWalkingAnim(building);
                                building.updateProductionGFX(true);
                                building.productionState = 2;
                                if (building2.buildingType == 3)
                                {
                                    if (this.granaryOpenCount != 0)
                                    {
                                        goto Label_0338;
                                    }
                                    building2.open = true;
                                    this.granaryOpenCount = 150;
                                    this.updateGFXState(building2);
                                    building2.updateGranary(this.gfx, this);
                                }
                            }
                            continue;
                        }
                        case 2:
                        {
                            building.updateProductionGFX(false);
                            if (building.worker.isJourneyOver())
                            {
                                building.productionSprite.clearText();
                                building.productionSprite.clearSecondText();
                                building.productionSprite.Visible = false;
                                double num7 = this.getDistanceThroughCycle(building) * building.calcRate;
                                if (num7 < building.productionTime)
                                {
                                    building.productionState = 0;
                                    this.initWorkingAnim(building, false);
                                }
                            }
                            continue;
                        }
                        default:
                        {
                            continue;
                        }
                    }
                    this.manageWorkingSounds(building);
                    continue;
                Label_0338:
                    this.granaryOpenCount = 150;
                }
                if (building.buildingType != 0x11)
                {
                    goto Label_0554;
                }
                if (building.secondaryWorker != null)
                {
                    goto Label_0493;
                }
                building.secondaryWorker = new VillageMapPerson(this.gfx);
                random = new Random((int) building.buildingID);
                switch (random.Next(3))
                {
                    case 0:
                        building.secondaryWorker.setPixelPos(new Point(-52, -9));
                        break;

                    case 1:
                        building.secondaryWorker.setPixelPos(new Point(0x4b, 0));
                        break;

                    case 2:
                        building.secondaryWorker.setPixelPos(new Point(0x16, 30));
                        break;
                }
            }
            else
            {
                if (building.worker == null)
                {
                    if (building.gotEmployee)
                    {
                        building.worker = new VillageMapPerson(this.gfx);
                        building.productionState = 0;
                        building.worker.setPos(building.buildingLocation);
                        this.initIdlingAnim(building);
                    }
                    else
                    {
                        building.open = false;
                    }
                }
                else if (!building.gotEmployee)
                {
                    building.worker.dispose();
                    building.worker = null;
                    building.productionState = 0;
                    building.open = false;
                }
                building.workerNeedsReInitializing = true;
                switch (building.productionState)
                {
                    case 0:
                        if ((building.worker != null) && !building.worker.idling)
                        {
                            this.initIdlingAnim(building);
                        }
                        break;

                    case 1:
                    {
                        Point realStart = Point.Truncate(building.worker.currentPos);
                        Point realEnd = VillageBuildingsData.tileToPixel(building.buildingLocation);
                        building.worker.startJourney(realStart, realEnd, 0.0);
                        this.initWalkingAnim(building);
                        building.productionState = 2;
                        break;
                    }
                    case 2:
                        if (building.worker.isJourneyOver())
                        {
                            building.productionState = 0;
                            this.initIdlingAnim(building);
                        }
                        break;
                }
                if ((building.buildingType == 0x11) && (building.secondaryWorker != null))
                {
                    building.secondaryWorker.dispose();
                    building.secondaryWorker = null;
                }
                goto Label_0577;
            }
            building.secondaryWorker.initWorkerSpriteInBuilding(building.baseSprite);
            building.data2 = random.Next(8);
            building.secondaryWorker.initAnim(GFXLibrary.Instance.CowAnimTexID, building.data2, cowLayAnim, 100);
            building.data1 = 0;
        Label_0493:
            if (building.data1 == 0)
            {
                if (building.secondaryWorker.workerSprite.CurrentFramID == (cowLayAnim.Length - 1))
                {
                    building.data1 = 1;
                    building.secondaryWorker.initAnim(GFXLibrary.Instance.CowAnimTexID, 0x80 + building.data2, cowIdleAnim, 100);
                }
            }
            else if ((building.data1 == 1) && (building.secondaryWorker.workerSprite.CurrentFramID == (cowIdleAnim.Length - 1)))
            {
                Random random2 = new Random();
                if (random2.Next(5) == 1)
                {
                    building.data1 = 0;
                    building.secondaryWorker.initAnim(GFXLibrary.Instance.CowAnimTexID, building.data2, cowLayAnim, 100);
                }
            }
            building.secondaryWorker.update();
        Label_0554:
            if (building.productionState == 0)
            {
                building.worker.fadeToSolid();
            }
            else
            {
                this.manageFadeOverBuildings(building.worker, building, building2);
            }
        Label_0577:
            if (building.worker != null)
            {
                building.worker.update();
            }
        }

        private void runSecondaryResourceBuilding(VillageMapBuilding building, int storageBuildingType, int sourceBuildingType, double calcRate)
        {
            VillageMapBuilding destBuilding = null;
            VillageMapBuilding sourceBuilding = null;
            destBuilding = this.findBuildingType(storageBuildingType);
            sourceBuilding = this.findBuildingType(sourceBuildingType);
            if (((destBuilding == null) || (sourceBuilding == null)) || (calcRate == 0.0))
            {
                if (building.worker == null)
                {
                    if (building.gotEmployee)
                    {
                        building.worker = new VillageMapPerson(this.gfx);
                        building.productionState = 0;
                        building.worker.setPos(building.buildingLocation);
                        this.initIdlingAnim(building);
                        building.workerNeedsReInitializing = true;
                    }
                    else
                    {
                        building.open = false;
                    }
                }
                else if (!building.gotEmployee)
                {
                    building.worker.dispose();
                    building.worker = null;
                    building.productionState = 0;
                    building.open = false;
                }
                if (building.workerNeedsReInitializing)
                {
                    this.reInitializeSecondaryBuilding(building, calcRate, destBuilding, sourceBuilding);
                    building.workerNeedsReInitializing = false;
                }
                switch (building.productionState)
                {
                    case 1:
                    case 3:
                    case 4:
                    {
                        building.worker.workerSprite.Visible = true;
                        Point realStart = Point.Truncate(building.worker.currentPos);
                        Point realEnd = VillageBuildingsData.tileToPixel(building.buildingLocation);
                        building.worker.startJourney(realStart, realEnd, 0.0);
                        this.initWalkingAnim(building);
                        building.productionState = 5;
                        break;
                    }
                    case 2:
                    case 5:
                        if (building.worker.isJourneyOver())
                        {
                            building.productionState = 0;
                            this.initIdlingAnim(building);
                        }
                        break;
                }
            }
            else
            {
                int num = 1;
                if (building.worker == null)
                {
                    building.worker = new VillageMapPerson(this.gfx);
                    building.worker.setPos(building.buildingLocation);
                    building.workerNeedsReInitializing = true;
                    building.worker.initWorkerSprite();
                }
                if (building.workerNeedsReInitializing)
                {
                    this.reInitializeSecondaryBuilding(building, calcRate, destBuilding, sourceBuilding);
                    if (building.serverCalcRate == 0.0)
                    {
                        num = 0;
                    }
                    else
                    {
                        building.productionState = 0;
                        num = 2;
                        building.workerNeedsReInitializing = false;
                        this.initWorkingAnim(building, true);
                    }
                }
                this.getNumTrips(building.buildingType);
                for (int i = 0; i < num; i++)
                {
                    switch (building.productionState)
                    {
                        case 0:
                        {
                            if (!building.worker.working)
                            {
                                this.initWorkingAnim(building, false);
                            }
                            double num3 = this.getDistanceThroughCycleSecondary(building) * building.calcRate;
                            double num4 = num3 % building.tripCalcRate;
                            if (num4 < building.productionTime)
                            {
                                break;
                            }
                            double distThroughJourney = (num4 - building.productionTime) / building.journeyTime;
                            if (building.weaponContinuance)
                            {
                                distThroughJourney = 0.0;
                            }
                            building.worker.startJourneyTileBased(building.buildingLocation, sourceBuilding.buildingLocation, distThroughJourney);
                            this.initWalkingAnim(building);
                            building.productionState = 1;
                            continue;
                        }
                        case 1:
                        {
                            if (building.worker.isJourneyOver())
                            {
                                double num6 = this.getDistanceThroughCycleSecondary(building) * building.calcRate;
                                double num7 = num6 % building.tripCalcRate;
                                double num8 = (num7 - (building.productionTime + building.journeyTime)) / building.journeyTime;
                                if (building.weaponContinuance)
                                {
                                    num8 = 0.0;
                                }
                                building.worker.startJourneyTileBased(sourceBuilding.buildingLocation, building.buildingLocation, num8);
                                this.initCollectingAnim(building);
                                building.productionState = 2;
                            }
                            continue;
                        }
                        case 2:
                        {
                            if (building.worker.isJourneyOver())
                            {
                                building.weaponContinuance = false;
                                double num9 = this.getDistanceThroughCycleSecondary(building) * building.calcRate;
                                double num10 = (building.calcRate - (building.journeyTime2 * 2.0)) - GameEngine.Instance.LocalWorldData.WeaponProductionOffScreenTime;
                                if (num9 < num10)
                                {
                                    goto Label_03D7;
                                }
                                double num11 = (num9 - num10) / building.journeyTime2;
                                building.worker.startJourneyTileBased(building.buildingLocation, destBuilding.buildingLocation, num11);
                                this.initCarryingAnim(building);
                                building.productionState = 3;
                            }
                            continue;
                        }
                        case 3:
                        {
                            if (building.worker.isJourneyOver())
                            {
                                building.productionState = 4;
                                building.worker.workerSprite.Visible = false;
                                building.weaponContinuance = true;
                            }
                            continue;
                        }
                        case 4:
                        {
                            double num12 = this.getDistanceThroughCycleSecondary(building) * building.calcRate;
                            double num13 = (building.calcRate - (building.journeyTime2 * 2.0)) - GameEngine.Instance.LocalWorldData.WeaponProductionOffScreenTime;
                            if (num12 < num13)
                            {
                                building.worker.workerSprite.Visible = true;
                                building.productionState = 5;
                                double num14 = 0.0;
                                building.worker.startJourneyTileBased(destBuilding.buildingLocation, building.buildingLocation, num14);
                                this.initWalkingAnim(building);
                            }
                            continue;
                        }
                        case 5:
                        {
                            if (building.worker.isJourneyOver())
                            {
                                building.productionState = 0;
                                this.initWorkingAnim(building, false);
                            }
                            continue;
                        }
                        default:
                        {
                            continue;
                        }
                    }
                    building.weaponContinuance = false;
                    this.manageWorkingSounds(building);
                    continue;
                Label_03D7:
                    building.productionState = 0;
                    this.initWorkingAnim(building, false);
                }
                if (building.productionState == 0)
                {
                    building.worker.fadeToSolid();
                }
                else
                {
                    this.manageFadeOverBuildings(building.worker, building, destBuilding);
                }
            }
            if (building.worker != null)
            {
                building.worker.update();
            }
        }

        public void selectBuilding(VillageMapBuilding building)
        {
            if (InterfaceMgr.Instance.isInBuildingPanelOpen())
            {
                if (!GameEngine.Instance.World.isCapital(this.VillageID))
                {
                    GameEngine.Instance.playInterfaceSound("VillageMap_select_building_Already_Open");
                }
                else
                {
                    GameEngine.Instance.playInterfaceSound("VillageMap_select_capital_building_Already_Open");
                }
            }
            else if (!GameEngine.Instance.World.isCapital(this.VillageID))
            {
                GameEngine.Instance.playInterfaceSound("VillageMap_select_building");
            }
            else
            {
                GameEngine.Instance.playInterfaceSound("VillageMap_select_capital_building");
            }
            string tag = VillageBuildingsData.getBuildingNameLabel(building.buildingType);
            if (tag.Length > 0)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastClickedSound);
                if (span.TotalSeconds > 2.0)
                {
                    this.lastClickedSound = DateTime.Now;
                    if (!GameEngine.Instance.AudioEngine.isSoundPlaying(tag))
                    {
                        GameEngine.Instance.playInterfaceSound(tag);
                    }
                }
            }
            InterfaceMgr.Instance.showInBuildingInfo(building);
        }

        private void sendMarketResourcesCallback(SendMarketResources_ReturnType returnData)
        {
            this.inMarketSend = false;
            if (returnData.Success)
            {
                VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
                if (map != null)
                {
                    setServerTime(returnData.currentTime);
                    if (returnData.cardData != null)
                    {
                        GameEngine.Instance.World.UserCardData = returnData.cardData;
                    }
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    map.importTraders(returnData.traders, returnData.currentTime);
                    if (returnData.tradersJustStarting != null)
                    {
                        map.startVillageTraderMovement(returnData.tradersJustStarting, returnData.villageID, returnData.targetVillageID);
                    }
                }
                else
                {
                    GameEngine.Instance.World.importOrphanedTraders(returnData.traders, returnData.currentTime, returnData.villageID);
                }
            }
        }

        public void sendResources(int villageID, int resource, int amount)
        {
            if (this.inMarketSend)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastMarketSend);
                if (span.TotalSeconds < 45.0)
                {
                    return;
                }
                this.inMarketSend = false;
            }
            if (!this.inMarketSend)
            {
                this.inMarketSend = true;
                this.lastMarketSend = DateTime.Now;
                RemoteServices.Instance.set_SendMarketResources_UserCallBack(new RemoteServices.SendMarketResources_UserCallBack(this.sendMarketResourcesCallback));
                RemoteServices.Instance.SendMarketResources(this.m_villageID, villageID, resource, amount);
                AllVillagesPanel.travellersChanged();
            }
        }

        private void setRandStateData(VillageMapBuilding building, int data)
        {
            this.randStateArray[building.buildingID] = data;
            building.randState = data;
        }

        public static void setServerTime(DateTime serverTime)
        {
            baseServerTime = serverTime;
            localBaseTime = DXTimer.GetCurrentMilliseconds();
        }

        public void showStats()
        {
            DateTime now = DateTime.Now;
            TimeSpan span = (TimeSpan) (now - this.m_villageInfoUpdateLastTime);
            string timeLeftString = "";
            string migrationTimeString = "";
            bool flag = false;
            double num = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
            DateTime curTime = baseServerTime.AddSeconds(num);
            if (!GameEngine.Instance.World.isCapital(this.m_villageID))
            {
                bool flag2 = false;
                if ((this.m_totalPeople >= this.m_housingCapacity) && (this.m_popularityLevel > 0))
                {
                    flag2 = true;
                }
                else if ((this.m_totalPeople <= 4) && (this.m_popularityLevel < 0))
                {
                    flag2 = true;
                }
                if ((this.m_popularityLevel != 0) && !flag2)
                {
                    TimeSpan span2 = (TimeSpan) (this.m_immigrationNextChangeTime - curTime);
                    double num2 = span2.TotalSeconds + 3.0;
                    if (num2 > 0.0)
                    {
                        num2 -= 3.0;
                        if (num2 > 0.0)
                        {
                            migrationTimeString = createBuildTimeString((int) num2);
                            this.m_statsMigrationUpdateRequested = false;
                        }
                    }
                    else if (!this.m_statsMigrationUpdateRequested)
                    {
                        this.m_statsMigrationUpdateRequested = true;
                        if (!this.ViewOnly && (span.TotalSeconds > 30.0))
                        {
                            this.m_villageInfoUpdateLastTime = now;
                            RemoteServices.Instance.set_VillageBuildingChangeRates_UserCallBack(new RemoteServices.VillageBuildingChangeRates_UserCallBack(this.villageBuildingChangeRatesCallback));
                            RemoteServices.Instance.VillageBuildingChangeRates(this.m_villageID, -1, -1, -1, -1);
                        }
                        flag = true;
                    }
                }
                else
                {
                    this.m_statsMigrationUpdateRequested = false;
                }
            }
            if (this.m_consumptionChangeTimeNeeded)
            {
                TimeSpan span3 = (TimeSpan) (this.m_consumptionChangeTime - curTime);
                if (span3.TotalSeconds > 0.0)
                {
                    this.m_statsConsumptionUpdateRequested = false;
                }
                else if (!this.m_statsConsumptionUpdateRequested)
                {
                    this.m_consumptionChangeTimeNeeded = false;
                    this.m_statsConsumptionUpdateRequested = true;
                    if ((!this.ViewOnly && !GameEngine.Instance.World.isCapital(this.m_villageID)) && (!flag && (span.TotalSeconds > 30.0)))
                    {
                        this.m_villageInfoUpdateLastTime = now;
                        RemoteServices.Instance.set_VillageBuildingChangeRates_UserCallBack(new RemoteServices.VillageBuildingChangeRates_UserCallBack(this.villageBuildingChangeRatesCallback));
                        RemoteServices.Instance.VillageBuildingChangeRates(this.m_villageID, -1, -1, -1, -1);
                    }
                    flag = true;
                }
            }
            else
            {
                this.m_statsConsumptionUpdateRequested = false;
            }
            foreach (PopEventData data in this.m_popEvents)
            {
                if (data.eventID >= 0)
                {
                    TimeSpan span4 = (TimeSpan) (data.endTime - curTime);
                    double num4 = span4.TotalSeconds + 2.0;
                    if (num4 < 0.0)
                    {
                        if (!flag)
                        {
                            flag = true;
                            if ((!this.ViewOnly && !GameEngine.Instance.World.isCapital(this.m_villageID)) && (span.TotalSeconds > 30.0))
                            {
                                this.m_villageInfoUpdateLastTime = now;
                                RemoteServices.Instance.set_VillageBuildingChangeRates_UserCallBack(new RemoteServices.VillageBuildingChangeRates_UserCallBack(this.villageBuildingChangeRatesCallback));
                                RemoteServices.Instance.VillageBuildingChangeRates(this.m_villageID, -1, -1, -1, -1);
                            }
                        }
                        data.eventID = -1;
                    }
                }
            }
            double effectiveRationsLevel = this.m_effectiveRationsLevel;
            if (!this.m_showEffective)
            {
                effectiveRationsLevel = this.m_rationsLevel;
            }
            double effectiveAleRationsLevel = this.m_effectiveAleRationsLevel;
            if (!this.m_showAleEffective)
            {
                effectiveAleRationsLevel = this.m_aleRationsLevel;
            }
            double housingChangeLevel = VillageBuildingsData.getHousingPopularityLevel(this.m_totalPeople, this.m_housingCapacity);
            int totalPeople = this.m_totalPeople;
            if (this.m_housingCapacity < this.m_totalPeople)
            {
                totalPeople = this.m_housingCapacity;
            }
            double goldDayRate = (totalPeople * VillageBuildingsData.getTaxIncomeLevel(this.m_taxLevel, GameEngine.Instance.World.UserCardData)) * GameEngine.Instance.LocalWorldData.goldIncomeRate;
            decimal num10 = (decimal) this.m_effectiveRationsLevel;
            if (num10 <= 2M)
            {
                num10 /= 4M;
            }
            else if (num10 < 3M)
            {
                num10 = ((num10 - 2M) / 2M) + 0.5M;
            }
            else
            {
                num10 -= 2M;
            }
            decimal num11 = (decimal) this.m_effectiveAleRationsLevel;
            decimal num12 = (this.m_totalPeople / (((decimal) GameEngine.Instance.LocalWorldData.foodConsumptionRate) / 24M)) * num10;
            decimal num13 = (this.m_totalPeople / (((decimal) GameEngine.Instance.LocalWorldData.aleConsumptionRate) / 24M)) * num11;
            double popularityChange = 0.0;
            double popularityLevel = this.m_popularityLevel;
            if (((this.m_taxLevel != this.m_taxLevelServer) || (this.m_rationsLevel != this.m_rationsLevelServer)) || (this.m_aleRationsLevel != this.m_aleRationsLevelServer))
            {
                popularityLevel = 0.0;
                popularityLevel += VillageBuildingsData.getTaxPopularityLevel(this.m_taxLevel);
                double rationsLevel = effectiveRationsLevel;
                if (!this.m_showEffective)
                {
                    if ((this.m_effectiveRationsLevel == Math.Floor(this.m_effectiveRationsLevel)) && (this.m_effectiveRationsLevel == this.m_rationsLevelServer))
                    {
                        rationsLevel = effectiveRationsLevel;
                    }
                    else if (this.m_effectiveRationsLevel < this.m_rationsLevel)
                    {
                        effectiveRationsLevel = rationsLevel = this.m_effectiveRationsLevel;
                    }
                }
                popularityLevel += VillageBuildingsData.getRationsPopularityLevel(rationsLevel, GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.UserCardData);
                if (rationsLevel > 0.0)
                {
                    popularityLevel += VillageBuildingsData.getNumFoodTypesEatenPopularityLevel(this.m_numFoodTypesEaten);
                }
                popularityLevel += VillageBuildingsData.getHousingPopularityLevel(this.m_totalPeople, this.m_housingCapacity);
                double aleRationsLevel = effectiveAleRationsLevel;
                if (!this.m_showAleEffective)
                {
                    if ((this.m_effectiveAleRationsLevel == Math.Floor(this.m_effectiveAleRationsLevel)) && (this.m_effectiveAleRationsLevel == this.m_aleRationsLevelServer))
                    {
                        aleRationsLevel = effectiveAleRationsLevel;
                    }
                    else if (this.m_effectiveAleRationsLevel < this.m_aleRationsLevel)
                    {
                        effectiveAleRationsLevel = aleRationsLevel = this.m_effectiveAleRationsLevel;
                    }
                }
                popularityLevel += VillageBuildingsData.getAleRationsPopularityLevel(aleRationsLevel, GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.UserCardData);
                popularityLevel += VillageBuildingsData.getBuildingsTypePopularityLevel(this.m_numPositiveBuildings, this.m_numNegativeBuildings, GameEngine.Instance.World.UserCardData);
                foreach (PopEventData data2 in this.m_popEvents)
                {
                    popularityLevel += data2.eventEffect;
                }
            }
            int parishBonus = this.m_parishCapitalResearchData.Research_Gardening + this.m_parishCapitalResearchData.Research_Justice;
            double foodProductionRate = this.getFoodProductionPerDay();
            double aleProductionRate = this.getAleProductionPerDay();
            InterfaceMgr.Instance.showVillageStats(this.m_taxLevel, this.m_rationsLevel, this.m_aleRationsLevel, (int) popularityLevel, popularityChange, timeLeftString, migrationTimeString, effectiveRationsLevel, this.m_numFoodTypesEaten, effectiveAleRationsLevel, housingChangeLevel, goldDayRate, (double) num12, this.m_totalPeople, this.m_housingCapacity, this.m_numPositiveBuildings, this.m_numNegativeBuildings, this.m_popEvents, (double) num13, curTime, foodProductionRate, aleProductionRate, this.m_numPopularityBuildings, this.calcParishVillageTax());
            InterfaceMgr.Instance.showVillageStats2(this.m_preCountedChurches, this.m_preCountedChapels, this.m_preCountedCathedrals, this.m_preCountedSmallGardens, this.m_preCountedLargeGardens, this.m_preCountedSmallStatues, this.m_preCountedLargeStatues, this.m_preCountedDovecotes, this.m_preCountedStocks, this.m_preCountedBurningPosts, this.m_preCountedGibbets, this.m_preCountedRacks, this.m_lastBanquetStored, this.m_lastBanquetHonour, this.m_lastBanquetDate, 0.0, popularityLevel, this.m_capitalTaxRate, this.calcParishCapitalTaxIncome(), this.m_parishPeople, this.m_parentCapitalTaxRate, this.m_lastCapitalTaxRate, parishBonus);
        }

        public void startMoveBuildings(VillageMapBuilding building)
        {
            UniversalDebugLog.Log("startMoveBuildings");
            if ((building != null) && (m_movingBuilding == null))
            {
                m_movingBuilding = building;
                this.startPlaceBuilding(building.buildingType, true);
                if (m_movingBuilding.shadowSprite != null)
                {
                    m_movingBuilding.shadowSprite.Visible = false;
                }
                else
                {
                    m_movingBuilding.baseSprite.Visible = false;
                }
            }
        }

        public void startPlaceBuilding(int buildingType, bool moving)
        {
            int num4;
            this.stopPlaceBuilding(!moving);
            placingAsFree = moving;
            placementType = buildingType;
            placementSprite = new SpriteWrapper();
            InterfaceMgr.Instance.toggleDXCardBarActive(false);
            int villageOverlaysAnimTexID = GFXLibrary.Instance.VillageOverlaysAnimTexID;
            int x = 0;
            int y = 0;
            if (buildingType != 1)
            {
                num4 = -1;
                switch (buildingType)
                {
                    case 6:
                        num4 = 0x1b;
                        x = 10;
                        y = 0;
                        break;

                    case 7:
                        num4 = 0x16;
                        x = 30;
                        y = -60;
                        break;

                    case 8:
                        num4 = 12;
                        x = 10;
                        y = -20;
                        break;

                    case 9:
                        num4 = 0x12;
                        x = 20;
                        y = 20;
                        break;

                    case 12:
                        num4 = 0;
                        x = 30;
                        y = -50;
                        break;

                    case 13:
                        num4 = 1;
                        x = 30;
                        y = -60;
                        break;

                    case 14:
                        num4 = 4;
                        x = 130;
                        y = -60;
                        break;

                    case 15:
                        num4 = 0x18;
                        x = 30;
                        y = -10;
                        break;

                    case 0x10:
                        num4 = 13;
                        x = 30;
                        y = -20;
                        break;

                    case 0x11:
                        num4 = 6;
                        x = 30;
                        y = -45;
                        break;

                    case 0x12:
                        num4 = 8;
                        x = 30;
                        y = -20;
                        break;

                    case 0x13:
                        num4 = 7;
                        x = 10;
                        y = -35;
                        break;

                    case 0x15:
                        num4 = 10;
                        x = 10;
                        y = -50;
                        break;

                    case 0x16:
                        num4 = 0x19;
                        x = 15;
                        y = 0;
                        break;

                    case 0x17:
                        num4 = 0x13;
                        x = 10;
                        y = 0;
                        break;

                    case 0x18:
                        num4 = 0x15;
                        x = 10;
                        y = -100;
                        break;

                    case 0x19:
                        num4 = 20;
                        x = 10;
                        y = -100;
                        break;

                    case 0x1a:
                        num4 = 14;
                        x = 0x19;
                        y = -50;
                        break;

                    case 0x1c:
                        num4 = 0x11;
                        x = 30;
                        y = -20;
                        break;

                    case 0x1d:
                        num4 = 3;
                        x = 30;
                        y = -20;
                        break;

                    case 30:
                        num4 = 0x17;
                        x = 30;
                        y = -30;
                        break;

                    case 0x1f:
                        num4 = 2;
                        x = 30;
                        y = -30;
                        break;

                    case 0x20:
                        num4 = 5;
                        x = 40;
                        y = -40;
                        break;

                    case 0x21:
                        num4 = 0x1a;
                        x = 30;
                        y = -30;
                        break;

                    case 0x22:
                        num4 = 30;
                        x = 30;
                        y = -50;
                        break;

                    case 0x24:
                        num4 = 30;
                        x = 30;
                        y = -140;
                        break;

                    case 0x25:
                        num4 = 30;
                        x = 30;
                        y = -230;
                        break;

                    case 0x26:
                    case 0x29:
                    case 0x2a:
                    case 0x2b:
                    case 0x2c:
                    case 0x2d:
                        num4 = 11;
                        x = 10;
                        y = 20;
                        break;

                    case 0x31:
                    case 50:
                    case 0x33:
                        num4 = 11;
                        x = 10;
                        y = 20;
                        break;

                    case 0x36:
                    case 0x37:
                    case 0x38:
                    case 0x39:
                        num4 = 11;
                        x = 10;
                        y = -40;
                        break;

                    case 0x3a:
                    case 0x3b:
                        num4 = 11;
                        x = 20;
                        y = -80;
                        break;

                    case 60:
                        num4 = 11;
                        x = 30;
                        y = -85;
                        break;

                    case 0x3d:
                        num4 = 11;
                        x = 30;
                        y = 20;
                        break;

                    case 0x3e:
                        num4 = 11;
                        x = 10;
                        y = -20;
                        break;

                    case 0x3f:
                        num4 = 11;
                        x = 10;
                        y = -35;
                        break;

                    case 0x40:
                        num4 = 11;
                        x = 30;
                        y = 20;
                        break;

                    case 0x41:
                        x = 30;
                        num4 = 0x1c;
                        break;

                    case 0x42:
                        x = 0x18;
                        num4 = 0x1c;
                        break;

                    case 0x43:
                        x = 30;
                        y = -100;
                        num4 = 0x1c;
                        break;

                    case 0x44:
                        x = 20;
                        num4 = 0x1c;
                        break;

                    case 0x45:
                        x = 30;
                        y = -30;
                        num4 = 0x1c;
                        break;

                    case 70:
                    case 0x47:
                    case 0x48:
                    case 0x49:
                        num4 = 30;
                        x = 30;
                        y = -20;
                        break;

                    case 0x4a:
                    case 0x4b:
                        num4 = 30;
                        x = 30;
                        y = -10;
                        break;

                    case 0x4f:
                        num4 = 50;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 80:
                        num4 = 0x33;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x51:
                        num4 = 0x34;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x52:
                        num4 = 0x35;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x53:
                        num4 = 0x36;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x54:
                        num4 = 0x37;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x55:
                        num4 = 0x38;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x56:
                        num4 = 0x39;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x57:
                        num4 = 0x3a;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x58:
                        num4 = 0x3b;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x59:
                        num4 = 60;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 90:
                        num4 = 0x3d;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x5b:
                        num4 = 0x3e;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x5c:
                        num4 = 0x3f;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x5d:
                        num4 = 0x40;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x5e:
                        num4 = 0x41;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x5f:
                        num4 = 0x42;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x60:
                        num4 = 0x43;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x61:
                        num4 = 0x44;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x62:
                        num4 = 0x45;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x63:
                        num4 = 70;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 100:
                        num4 = 0x47;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x65:
                        num4 = 0x48;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;

                    case 0x66:
                        num4 = 0x49;
                        villageOverlaysAnimTexID = GFXLibrary.Instance.TownBuildindsTexID;
                        x = s_villageBuildingData[buildingType].animOffset.X;
                        y = s_villageBuildingData[buildingType].animOffset.Y;
                        break;
                }
            }
            else
            {
                switch (GameEngine.Instance.World.UserResearchData.Research_HousingCapacity)
                {
                    case 2:
                    case 3:
                        buildingType = 0x27;
                        break;

                    case 4:
                    case 5:
                        buildingType = 40;
                        break;

                    case 6:
                        buildingType = 0x4c;
                        break;

                    case 7:
                    case 8:
                    case 9:
                        buildingType = 0x4d;
                        break;
                }
                placementSprite.attachText("0", new Point(15, -90), ARGBColors.White, true, true);
                placementSprite_subSprite = new SpriteWrapper();
                placementSprite_subSprite.TextureID = GFXLibrary.Instance.VillageOverlaysAnimTexID;
                placementSprite_subSprite.Initialize(this.gfx);
                placementSprite_subSprite.SpriteNo = 0x10;
                placementSprite_subSprite.Center = new PointF(32f, 32f);
                placementSprite_subSprite.PosX = -15f;
                placementSprite_subSprite.PosY = -90f;
                placementSprite.DrawChildrenWithParent = true;
                placementSprite.AddChild(placementSprite_subSprite, 1);
                goto Label_0BD7;
            }
            if (num4 >= 0)
            {
                if (villageOverlaysAnimTexID == GFXLibrary.Instance.TownBuildindsTexID)
                {
                    placementSprite_subSprite = new SpriteWrapper();
                    placementSprite_subSprite.TextureID = villageOverlaysAnimTexID;
                    placementSprite_subSprite.Initialize(this.gfx);
                    placementSprite_subSprite.SpriteNo = num4;
                    placementSprite_subSprite.PosX = 0f;
                    placementSprite_subSprite.PosY = 0f;
                    PointF tf = new PointF {
                        X = s_villageBuildingData[buildingType].animOffset.X,
                        Y = s_villageBuildingData[buildingType].animOffset.Y
                    };
                    placementSprite_subSprite.Center = tf;
                    placementSprite.DrawChildrenWithParent = true;
                    placementSprite.AddChild(placementSprite_subSprite, 1);
                }
                else
                {
                    placementSprite.attachText("0", new Point(x, -90 + y), ARGBColors.White, false, true);
                    placementSprite_subSprite = new SpriteWrapper();
                    placementSprite_subSprite.TextureID = villageOverlaysAnimTexID;
                    placementSprite_subSprite.Initialize(this.gfx);
                    placementSprite_subSprite.SpriteNo = num4;
                    placementSprite_subSprite.Center = new PointF(32f, 32f);
                    placementSprite_subSprite.PosX = -30 + x;
                    placementSprite_subSprite.PosY = -90 + y;
                    placementSprite.DrawChildrenWithParent = true;
                    placementSprite.AddChild(placementSprite_subSprite, 1);
                }
            }
            else
            {
                placementSprite.attachText("", new Point(0, -90), ARGBColors.White, true, true);
            }
        Label_0BD7:
            placementSprite.TextureID = s_villageBuildingData[buildingType].baseGfxTexID;
            placementSprite.Initialize(this.gfx);
            placementSprite.PosX = -1000f;
            placementSprite.PosY = -1000f;
            placementSprite.SpriteNo = s_villageBuildingData[buildingType].baseGfxID;
            PointF tf2 = new PointF {
                X = s_villageBuildingData[buildingType].baseOffset.X,
                Y = s_villageBuildingData[buildingType].baseOffset.Y
            };
            placementSprite.Center = tf2;
            this.backgroundSprite.AddChild(placementSprite, 10);
        }

        public void startPlaceBuilding_ShowPanel(int buildingType, string name, bool showHelp)
        {
            int woodNeeded = 0;
            int stoneNeeded = 0;
            int clayNeeded = 0;
            int goldNeeded = 0;
            int flagsNeeded = 0;
            VillageBuildingsData.calcBuildingCosts(GameEngine.Instance.LocalWorldData, buildingType, this.countBuildingType(buildingType), ref woodNeeded, ref stoneNeeded, ref clayNeeded, ref goldNeeded, GameEngine.Instance.World.UserResearchData.Research_Tools, ref flagsNeeded);
            if (((flagsNeeded > 0) && (GameEngine.Instance.LocalWorldData.constrFlagCost[buildingType] > 0)) && ((this.m_capitalBuildingsBuilt != null) && this.m_capitalBuildingsBuilt.Contains(buildingType)))
            {
                flagsNeeded = 0;
            }
            TimeSpan span = new TimeSpan();
            double origTime = 0.0;
            if (!GameEngine.Instance.World.isCapital(this.m_villageID))
            {
                span = VillageBuildingsData.calcConstructionTime(GameEngine.Instance.LocalWorldData, buildingType, this.localBuildings.Count, GameEngine.Instance.World.UserResearchData.Research_Architecture, GameEngine.Instance.World.UserCardData, ref origTime);
            }
            else
            {
                int num7 = 4;
                int count = this.localBuildings.Count;
                switch (count)
                {
                    case 0:
                        num7 = 4;
                        break;

                    case 1:
                        num7 = 6;
                        break;

                    case 2:
                        num7 = 8;
                        break;

                    case 3:
                        num7 = 10;
                        break;

                    case 4:
                        num7 = 12;
                        break;

                    case 5:
                        num7 = 14;
                        break;

                    case 6:
                        num7 = 0x10;
                        break;

                    case 7:
                        num7 = 0x12;
                        break;

                    case 8:
                        num7 = 20;
                        break;

                    case 9:
                        num7 = 0x16;
                        break;

                    case 10:
                        num7 = 0x18;
                        break;

                    default:
                        num7 = 0x18 + (count - 10);
                        break;
                }
                int minutes = num7 * 60;
                minutes = (int) (minutes * ResearchData.ParishTownHallIncreases_Guilds[this.m_parishCapitalResearchData.Research_Architecture]);
                span = new TimeSpan(0, minutes, 0);
            }
            int realBuildingType = buildingType;
            if (!showHelp)
            {
                buildingType = -1;
            }
            int totalSeconds = (int) span.TotalSeconds;
            int num12 = (int) origTime;
            if ((GameEngine.Instance.World.getTutorialStage() == 2) && ((num12 + 2) == 0x11))
            {
                totalSeconds = 1;
            }
            if ((GameEngine.Instance.World.getTutorialStage() == 3) && (((num12 + 2) == 0x19) || ((num12 + 2) == 0x24)))
            {
                totalSeconds = 1;
            }
            InterfaceMgr.Instance.showVillageBuildingInfo(name, woodNeeded, stoneNeeded, clayNeeded, goldNeeded, flagsNeeded, createBuildTimeString(totalSeconds + 2), buildingType, realBuildingType);
        }

        private void startVillageTraderMovement(long[] traderList, int homeVillageID, int targetVillageID)
        {
            Point newEndPos = new Point(0, this.layout.gridHeight / 2);
            Point point2 = GameEngine.Instance.World.getVillageLocation(homeVillageID);
            Point point3 = GameEngine.Instance.World.getVillageLocation(targetVillageID);
            if (point2.X < point3.X)
            {
                newEndPos.X = this.layout.gridWidth + 5;
            }
            else
            {
                newEndPos.X = -5;
            }
            foreach (long num in traderList)
            {
                foreach (MarketTraderData data in this.traders)
                {
                    if (data.traderID == num)
                    {
                        foreach (VillageMapBuilding building in this.localBuildings)
                        {
                            if (building.buildingType == 0x4e)
                            {
                                if (building.worker == null)
                                {
                                    building.worker = new VillageMapPerson(this.gfx);
                                    building.productionState = 0;
                                    building.worker.setPos(building.buildingLocation);
                                    building.worker.startJourneyTileBased(building.buildingLocation, newEndPos, 0.0);
                                    this.initWalkingAnim(building);
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        public bool stockExchangeTrade(int targetExchange, int resource, int amount, bool buy)
        {
            if (this.inMarketSend)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastMarketSend);
                if (span.TotalSeconds < 45.0)
                {
                    return false;
                }
                this.inMarketSend = false;
            }
            if (!this.inMarketSend)
            {
                this.inMarketSend = true;
                this.lastMarketSend = DateTime.Now;
                RemoteServices.Instance.set_StockExchangeTrade_UserCallBack(new RemoteServices.StockExchangeTrade_UserCallBack(this.stockExchangeTradeCallback));
                RemoteServices.Instance.StockExchangeTrade(this.m_villageID, targetExchange, resource, amount, buy);
                AllVillagesPanel.travellersChanged();
            }
            return true;
        }

        private void stockExchangeTradeCallback(StockExchangeTrade_ReturnType returnData)
        {
            this.inMarketSend = false;
            if (returnData.Success)
            {
                VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
                if (map != null)
                {
                    setServerTime(returnData.currentTime);
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    map.importTraders(returnData.traders, returnData.currentTime);
                    if (returnData.tradersJustStarting != null)
                    {
                        map.startVillageTraderMovement(returnData.tradersJustStarting, returnData.villageID, returnData.targetVillageID);
                    }
                }
                else
                {
                    GameEngine.Instance.World.importOrphanedTraders(returnData.traders, returnData.currentTime, returnData.villageID);
                }
                GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                if (returnData.cardData != null)
                {
                    GameEngine.Instance.World.UserCardData = returnData.cardData;
                }
                returnData.stockExchangeData.SetAsSucceeded();
                if (StockExchangePanel.instance != null)
                {
                    StockExchangePanel.instance.getStockExchangeDataCallback(returnData.stockExchangeData);
                }
                if (CapitalTradePanel.instance != null)
                {
                    CapitalTradePanel.instance.getStockExchangeDataCallback(returnData.stockExchangeData);
                }
            }
            else if (returnData.m_errorCode == ErrorCodes.ErrorCode.TRADE_EXCHANGE_TOO_FAR)
            {
                MyMessageBox.Show(SK.Text("VillageMap_Stock_Exchange_Too_Far", "The Stock Exchange is too far away for you to trade with."), SK.Text("VillageMap_Trade_Error", "Trade Error"));
            }
        }

        public void stopPlaceBuilding(bool closeInterface)
        {
            if (placementSprite != null)
            {
                InterfaceMgr.Instance.toggleDXCardBarActive(true);
                if (placementSprite_subSprite != null)
                {
                    placementSprite.RemoveChild(placementSprite_subSprite);
                    placementSprite_subSprite = null;
                }
                if (this.backgroundSprite != null)
                {
                    this.backgroundSprite.RemoveChild(placementSprite);
                }
                placementSprite = null;
            }
            if (closeInterface)
            {
                InterfaceMgr.Instance.clearVillageBuildingInfo();
            }
            this.clearColouredBuildings();
            if (closeInterface)
            {
                InterfaceMgr.Instance.showInBuildingInfo(null);
            }
            placingAsFree = false;
            if (closeInterface && (m_movingBuilding != null))
            {
                if (m_movingBuilding.shadowSprite != null)
                {
                    m_movingBuilding.shadowSprite.Visible = true;
                }
                else
                {
                    m_movingBuilding.baseSprite.Visible = false;
                }
                m_movingBuilding = null;
            }
            this.placementError = 0;
        }

        public void Update(bool villageDisplayed)
        {
            VillageMapBuilding building16;
            VillageMapBuilding building17;
            double num30;
            double num31;
            int pikesBaseProductionTrips;
            if ((this.backgroundSprite != null) && villageDisplayed)
            {
                if (InterfaceMgr.Instance.updateVillageReports())
                {
                    this.backgroundSprite.Update();
                    this.backgroundSprite.AddToRenderList();
                    this.drawSurroundSprites();
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
                if (InterfaceMgr.Instance.isDXVisible())
                {
                    this.playEnvironmentalSounds();
                }
            }
            this.productionArrowProductionBuilding = new Point(-1, -1);
            this.productionArrowTargetBuilding = new Point(-1, -1);
            this.productionArrowTarget2Building = new Point(-1, -1);
            if ((this.placementError == 0) || (placementSprite == null))
            {
                if (placementSprite_subSprite == null)
                {
                    if (placementSprite != null)
                    {
                        placementSprite.changeText("");
                    }
                }
                else
                {
                    int num44;
                    placementSprite.changeText("");
                    switch (placementType)
                    {
                        case 1:
                        case 0x27:
                        case 40:
                        case 0x4c:
                        case 0x4d:
                        {
                            int num = ResearchData.researchHousingLevels[GameEngine.Instance.World.userResearchData.Research_HousingCapacity];
                            VillageMapBuilding building = this.findBuildingType(0);
                            if (building != null)
                            {
                                int dist = VillageBuildingsData.getMapDistance(building.buildingLocation, this.lastPlaceBuildingLoc);
                                placementSprite.changeText((num + VillageBuildingsData.getHousingCapacityBasedOnDistance(GameEngine.Instance.LocalWorldData, dist)).ToString());
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building.buildingLocation;
                            }
                            break;
                        }
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        {
                            VillageMapBuilding building13 = this.findBuildingType(2);
                            if (building13 != null)
                            {
                                double totalSeconds = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, building13.buildingLocation, this.lastPlaceBuildingLoc).TotalSeconds;
                                double num19 = VillageBuildingsData.calcProductionTime(GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.userResearchData, placementType, totalSeconds, 0.0, 1, this.m_villageMapType, this.m_parishCapitalResearchData, GameEngine.Instance.World.UserCardData);
                                double num20 = CardTypes.adjustPayloadSize(GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(placementType), placementType);
                                double num21 = (86400.0 / num19) * num20;
                                num44 = (int) num21;
                                placementSprite.changeText(num44.ToString());
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building13.buildingLocation;
                                break;
                            }
                            num44 = 0;
                            placementSprite.changeText(num44.ToString());
                            break;
                        }
                        case 12:
                        {
                            VillageMapBuilding building14 = this.findBuildingTypeIncludingConstructing(0x23);
                            if (building14 != null)
                            {
                                double travelTime = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, building14.buildingLocation, this.lastPlaceBuildingLoc).TotalSeconds;
                                double num23 = VillageBuildingsData.calcProductionTime(GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.userResearchData, placementType, travelTime, 0.0, 1, this.m_villageMapType, this.m_parishCapitalResearchData, GameEngine.Instance.World.UserCardData);
                                double num24 = CardTypes.adjustPayloadSize(GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(placementType), placementType);
                                double num25 = (86400.0 / num23) * num24;
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building14.buildingLocation;
                                num44 = (int) num25;
                                placementSprite.changeText(num44.ToString());
                                break;
                            }
                            num44 = 0;
                            placementSprite.changeText(num44.ToString());
                            break;
                        }
                        case 13:
                        case 14:
                        case 15:
                        case 0x10:
                        case 0x11:
                        case 0x12:
                        {
                            VillageMapBuilding building12 = this.findBuildingTypeIncludingConstructing(3);
                            if (building12 != null)
                            {
                                double num14 = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, building12.buildingLocation, this.lastPlaceBuildingLoc).TotalSeconds;
                                double num15 = VillageBuildingsData.calcProductionTime(GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.userResearchData, placementType, num14, 0.0, 1, this.m_villageMapType, this.m_parishCapitalResearchData, GameEngine.Instance.World.UserCardData);
                                double num16 = CardTypes.adjustPayloadSize(GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(placementType), placementType);
                                double num17 = (86400.0 / num15) * num16;
                                num44 = (int) num17;
                                placementSprite.changeText(num44.ToString());
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building12.buildingLocation;
                                break;
                            }
                            num44 = 0;
                            placementSprite.changeText(num44.ToString());
                            break;
                        }
                        case 0x13:
                        case 0x15:
                        case 0x16:
                        case 0x17:
                        case 0x18:
                        case 0x19:
                        case 0x1a:
                        case 0x21:
                        {
                            VillageMapBuilding building15 = this.findBuildingType(0);
                            if (building15 != null)
                            {
                                double num26 = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, building15.buildingLocation, this.lastPlaceBuildingLoc).TotalSeconds;
                                double num27 = VillageBuildingsData.calcProductionTime(GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.userResearchData, placementType, num26, 0.0, 1, this.m_villageMapType, this.m_parishCapitalResearchData, GameEngine.Instance.World.UserCardData);
                                double num28 = CardTypes.adjustPayloadSize(GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(placementType), placementType);
                                double num29 = (86400.0 / num27) * num28;
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building15.buildingLocation;
                                num44 = (int) num29;
                                placementSprite.changeText(num44.ToString());
                                break;
                            }
                            num44 = 0;
                            placementSprite.changeText(num44.ToString());
                            break;
                        }
                        case 0x1c:
                        case 0x1d:
                        case 30:
                        case 0x1f:
                            building16 = this.findBuildingType(4);
                            building17 = this.findBuildingType(2);
                            if ((building16 != null) && (building17 != null))
                            {
                                num30 = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, building16.buildingLocation, this.lastPlaceBuildingLoc).TotalSeconds + (GameEngine.Instance.LocalWorldData.WeaponProductionOffScreenTime / 2.0);
                                num31 = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, building17.buildingLocation, this.lastPlaceBuildingLoc).TotalSeconds;
                                pikesBaseProductionTrips = 1;
                                switch (placementType)
                                {
                                    case 0x1c:
                                        pikesBaseProductionTrips = GameEngine.Instance.LocalWorldData.pikesBaseProductionTrips;
                                        break;

                                    case 0x1d:
                                        pikesBaseProductionTrips = GameEngine.Instance.LocalWorldData.bowsBaseProductionTrips;
                                        break;

                                    case 30:
                                        pikesBaseProductionTrips = GameEngine.Instance.LocalWorldData.swordsBaseProductionTrips;
                                        break;

                                    case 0x1f:
                                        pikesBaseProductionTrips = GameEngine.Instance.LocalWorldData.armourBaseProductionTrips;
                                        break;

                                    case 0x20:
                                        pikesBaseProductionTrips = GameEngine.Instance.LocalWorldData.catapultsBaseProductionTrips;
                                        break;
                                }
                                goto Label_100A;
                            }
                            num44 = 0;
                            placementSprite.changeText(num44.ToString());
                            break;

                        case 0x20:
                        {
                            VillageMapBuilding building18 = this.findBuildingType(4);
                            VillageMapBuilding building19 = this.findBuildingType(2);
                            if ((building18 != null) && (building19 != null))
                            {
                                double num35 = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, building18.buildingLocation, this.lastPlaceBuildingLoc).TotalSeconds + (GameEngine.Instance.LocalWorldData.WeaponProductionOffScreenTime / 2.0);
                                double num36 = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, building19.buildingLocation, this.lastPlaceBuildingLoc).TotalSeconds;
                                int trips = 1;
                                switch (placementType)
                                {
                                    case 0x1c:
                                        trips = GameEngine.Instance.LocalWorldData.pikesBaseProductionTrips;
                                        break;

                                    case 0x1d:
                                        trips = GameEngine.Instance.LocalWorldData.bowsBaseProductionTrips;
                                        break;

                                    case 30:
                                        trips = GameEngine.Instance.LocalWorldData.swordsBaseProductionTrips;
                                        break;

                                    case 0x1f:
                                        trips = GameEngine.Instance.LocalWorldData.armourBaseProductionTrips;
                                        break;

                                    case 0x20:
                                        trips = GameEngine.Instance.LocalWorldData.catapultsBaseProductionTrips;
                                        break;
                                }
                                trips = CardTypes.cards_adjustWeaponProductionTrips(GameEngine.Instance.World.UserCardData, trips, placementType);
                                double num38 = VillageBuildingsData.calcProductionTime(GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.userResearchData, placementType, num36, num35, trips, this.m_villageMapType, this.m_parishCapitalResearchData, GameEngine.Instance.World.UserCardData);
                                placementSprite.changeText(((86400.0 / num38) * GameEngine.Instance.LocalWorldData.getPayloadSize(placementType)).ToString("0.#"));
                                break;
                            }
                            placementSprite.changeText(0.ToString());
                            break;
                        }
                        case 0x22:
                            placementSprite.changeText(GameEngine.Instance.LocalWorldData.FaithPoints_Chapel.ToString());
                            break;

                        case 0x24:
                            placementSprite.changeText(GameEngine.Instance.LocalWorldData.FaithPoints_Church.ToString());
                            break;

                        case 0x25:
                            placementSprite.changeText(GameEngine.Instance.LocalWorldData.FaithPoints_Cathedral.ToString());
                            break;

                        case 0x26:
                        case 0x29:
                        case 0x2a:
                        case 0x2b:
                        case 0x2c:
                        case 0x2d:
                        {
                            VillageMapBuilding building3 = this.findBuildingType(0);
                            if (building3 != null)
                            {
                                double num5 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_SmallGarden, building3.buildingLocation, this.lastPlaceBuildingLoc);
                                if (GameEngine.Instance.World.ThirdAgeWorld)
                                {
                                    num5 *= 4.0;
                                }
                                placementSprite.changeText(num5.ToString());
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building3.buildingLocation;
                            }
                            break;
                        }
                        case 0x31:
                        case 50:
                        case 0x33:
                        {
                            VillageMapBuilding building4 = this.findBuildingType(0);
                            if (building4 != null)
                            {
                                double num6 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_LargeGarden, building4.buildingLocation, this.lastPlaceBuildingLoc);
                                if (GameEngine.Instance.World.ThirdAgeWorld)
                                {
                                    num6 *= 4.0;
                                }
                                placementSprite.changeText(num6.ToString());
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building4.buildingLocation;
                            }
                            break;
                        }
                        case 0x36:
                        case 0x37:
                        case 0x38:
                        case 0x39:
                        {
                            VillageMapBuilding building5 = this.findBuildingType(0);
                            if (building5 != null)
                            {
                                double num7 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_SmallStatue, building5.buildingLocation, this.lastPlaceBuildingLoc);
                                if (GameEngine.Instance.World.ThirdAgeWorld)
                                {
                                    num7 *= 4.0;
                                }
                                placementSprite.changeText(num7.ToString());
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building5.buildingLocation;
                            }
                            break;
                        }
                        case 0x3a:
                        case 0x3b:
                        {
                            VillageMapBuilding building6 = this.findBuildingType(0);
                            if (building6 != null)
                            {
                                double num8 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_LargeStatue, building6.buildingLocation, this.lastPlaceBuildingLoc);
                                if (GameEngine.Instance.World.ThirdAgeWorld)
                                {
                                    num8 *= 4.0;
                                }
                                placementSprite.changeText(num8.ToString());
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building6.buildingLocation;
                            }
                            break;
                        }
                        case 60:
                        {
                            VillageMapBuilding building7 = this.findBuildingType(0);
                            if (building7 != null)
                            {
                                double num9 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_Dovecote, building7.buildingLocation, this.lastPlaceBuildingLoc);
                                if (GameEngine.Instance.World.ThirdAgeWorld)
                                {
                                    num9 *= 4.0;
                                }
                                placementSprite.changeText(num9.ToString());
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building7.buildingLocation;
                            }
                            break;
                        }
                        case 0x3d:
                        {
                            VillageMapBuilding building8 = this.findBuildingType(0);
                            if (building8 != null)
                            {
                                double num10 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_Stocks, building8.buildingLocation, this.lastPlaceBuildingLoc);
                                if (GameEngine.Instance.World.ThirdAgeWorld)
                                {
                                    num10 *= 4.0;
                                }
                                placementSprite.changeText(num10.ToString());
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building8.buildingLocation;
                            }
                            break;
                        }
                        case 0x3e:
                        {
                            VillageMapBuilding building9 = this.findBuildingType(0);
                            if (building9 != null)
                            {
                                double num11 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_BurningPost, building9.buildingLocation, this.lastPlaceBuildingLoc);
                                if (GameEngine.Instance.World.ThirdAgeWorld)
                                {
                                    num11 *= 4.0;
                                }
                                placementSprite.changeText(num11.ToString());
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building9.buildingLocation;
                            }
                            break;
                        }
                        case 0x3f:
                        {
                            VillageMapBuilding building10 = this.findBuildingType(0);
                            if (building10 != null)
                            {
                                double num12 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_Gibbet, building10.buildingLocation, this.lastPlaceBuildingLoc);
                                if (GameEngine.Instance.World.ThirdAgeWorld)
                                {
                                    num12 *= 4.0;
                                }
                                placementSprite.changeText(num12.ToString());
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building10.buildingLocation;
                            }
                            break;
                        }
                        case 0x40:
                        {
                            VillageMapBuilding building11 = this.findBuildingType(0);
                            if (building11 != null)
                            {
                                double num13 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_Rack, building11.buildingLocation, this.lastPlaceBuildingLoc);
                                if (GameEngine.Instance.World.ThirdAgeWorld)
                                {
                                    num13 *= 4.0;
                                }
                                placementSprite.changeText(num13.ToString());
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building11.buildingLocation;
                            }
                            break;
                        }
                        case 0x41:
                        case 0x42:
                        case 0x43:
                        case 0x44:
                        case 0x45:
                        {
                            VillageMapBuilding building2 = this.findBuildingType(0);
                            if (building2 != null)
                            {
                                int num3 = VillageBuildingsData.getMapDistance(building2.buildingLocation, this.lastPlaceBuildingLoc);
                                placementSprite.changeText(VillageBuildingsData.getBuildingPopularityBasedOnDistance(GameEngine.Instance.LocalWorldData, num3).ToString());
                                this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
                                this.productionArrowTargetBuilding = building2.buildingLocation;
                            }
                            break;
                        }
                        case 70:
                        case 0x47:
                        case 0x48:
                        case 0x49:
                            placementSprite.changeText(GameEngine.Instance.LocalWorldData.FaithPoints_SmallShrine.ToString());
                            break;

                        case 0x4a:
                        case 0x4b:
                            placementSprite.changeText(GameEngine.Instance.LocalWorldData.FaithPoints_LargeShrine.ToString());
                            break;
                    }
                }
                goto Label_12A7;
            }
            switch (this.placementError)
            {
                case 1:
                    placementSprite.changeText(SK.Text("VillageMap_Cannot_Be_Placed_Here", "Cannot be placed here"));
                    goto Label_12A7;

                case 2:
                    placementSprite.changeText(SK.Text("VillageMap_Cannot_Place_Any_More", "You cannot place any more of this building type"));
                    goto Label_12A7;

                case 3:
                    if (!GameEngine.Instance.World.isAccountPremium() && !GameEngine.Instance.World.isCapital(this.m_villageID))
                    {
                        placementSprite.changeText(SK.Text("VillageMap_Play_Premium_For_Build_Queue", "Play a Premium Token for a Building Queue"));
                    }
                    else
                    {
                        placementSprite.changeText(SK.Text("VillageMap_Building_Queue_Full", "Building Queue Is Full"));
                    }
                    goto Label_12A7;

                case 4:
                    placementSprite.changeText(SK.Text("VillageMap_Cannot_Afford_Building", "You cannot afford to place this building"));
                    goto Label_12A7;

                case 5:
                    placementSprite.changeText(SK.Text("VillageMap_Not_Enough_Flags", "You do not have enough flags to place this building"));
                    goto Label_12A7;

                case 6:
                    placementSprite.changeText(SK.Text("VillageMap_Not_Enough_Resources", "You do not have enough resources to place this building"));
                    goto Label_12A7;

                case 7:
                    placementSprite.changeText(SK.Text("VillageMap_Near_Trees", "Place near Trees"));
                    goto Label_12A7;

                case 8:
                    placementSprite.changeText(SK.Text("VillageMap_On_Stone", "Place on Stone"));
                    goto Label_12A7;

                case 9:
                    placementSprite.changeText(SK.Text("VillageMap_On_Iron", "Place on Iron"));
                    goto Label_12A7;

                case 10:
                    placementSprite.changeText(SK.Text("VillageMap_On_Marsh", "Place on Marsh"));
                    goto Label_12A7;

                case 11:
                    placementSprite.changeText(SK.Text("VillageMap_On_Water", "Place on Water"));
                    goto Label_12A7;

                case 12:
                    placementSprite.changeText(SK.Text("VillageMap_On_Salt_Flats", "Place on Salt Flats"));
                    goto Label_12A7;

                case 13:
                    placementSprite.changeText(SK.Text("VillageMap_On_River_Edge", "Place near Water"));
                    goto Label_12A7;

                default:
                    goto Label_12A7;
            }
        Label_100A:
            pikesBaseProductionTrips = CardTypes.cards_adjustWeaponProductionTrips(GameEngine.Instance.World.UserCardData, pikesBaseProductionTrips, placementType);
            double num33 = VillageBuildingsData.calcProductionTime(GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.userResearchData, placementType, num31, num30, pikesBaseProductionTrips, this.m_villageMapType, this.m_parishCapitalResearchData, GameEngine.Instance.World.UserCardData);
            double num34 = (86400.0 / num33) * GameEngine.Instance.LocalWorldData.getPayloadSize(placementType);
            this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
            this.productionArrowTargetBuilding = building17.buildingLocation;
            this.productionArrowTarget2Building = building16.buildingLocation;
            placementSprite.changeText(((int) num34).ToString());
        Label_12A7:
            if (this.granaryOpenCount > 0)
            {
                this.granaryOpenCount--;
            }
            List<VillageMapBuilding> list = new List<VillageMapBuilding>();
            foreach (VillageMapBuilding building20 in this.localBuildings)
            {
                if (!building20.complete && !building20.serverDeleting)
                {
                    if ((building20.updateConstructionGFX(localBaseTime, baseServerTime, false, this) && !building20.completeRequestSent) && !this.ViewOnly)
                    {
                        if (!GameEngine.Instance.World.isCapital(this.m_villageID))
                        {
                            GameEngine.Instance.playInterfaceSound("VillageMap_Building_Construction_Complete");
                            RemoteServices.Instance.set_VillageBuildingCompleteDataRetrieval_UserCallBack(new RemoteServices.VillageBuildingCompleteDataRetrieval_UserCallBack(this.villageBuildingCompleteDataRetrievalCallback));
                            RemoteServices.Instance.VillageBuildingCompleteDataRetrieval(this.m_villageID, building20.buildingID);
                            building20.completeRequestSent = true;
                        }
                        else
                        {
                            building20.complete = true;
                            building20.localComplete = true;
                        }
                    }
                }
                else if ((building20.serverDeleting && building20.updateConstructionGFX(localBaseTime, baseServerTime, false, this)) && !this.ViewOnly)
                {
                    RemoteServices.Instance.set_VillageBuildingCompleteDataRetrieval_UserCallBack(new RemoteServices.VillageBuildingCompleteDataRetrieval_UserCallBack(this.villageBuildingCompleteDataRetrievalCallback));
                    switch (building20.buildingType)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 0x23:
                            RemoteServices.Instance.GetVillageBuildingsList(this.m_villageID, false, false);
                            break;

                        default:
                            RemoteServices.Instance.VillageBuildingDeleteDataRetrieval(this.m_villageID, building20.buildingID);
                            break;
                    }
                    list.Add(building20);
                    continue;
                }
                if (building20.lastOpenState != building20.open)
                {
                    this.updateGFXState(building20);
                }
                this.runBuilding(building20);
            }
            this.monitorWeaponProduction();
            if (!GameEngine.Instance.World.TutorialIsAdvancing())
            {
                switch (GameEngine.Instance.World.getTutorialStage())
                {
                    case 2:
                        if ((this.findBuildingType(13) != null) && !this.tutorialStage_AppleFarm_Activated)
                        {
                            this.tutorialStage_AppleFarm_Activated = true;
                            GameEngine.Instance.World.forceTutorialToBeShown();
                        }
                        break;

                    case 3:
                    {
                        VillageMapBuilding building22 = this.findBuildingType(6);
                        VillageMapBuilding building23 = this.findBuildingType(7);
                        if (((building22 != null) && (building23 != null)) && !this.tutorialStage_Wood_Activated)
                        {
                            this.tutorialStage_Wood_Activated = true;
                            GameEngine.Instance.World.forceTutorialToBeShown();
                        }
                        break;
                    }
                }
            }
            if (list.Count > 0)
            {
                foreach (VillageMapBuilding building24 in list)
                {
                    villageClickMask.removeBuilding(building24.buildingID);
                    this.removeBuildingFromMap(Point.Empty, building24.buildingType, building24.buildingID);
                    this.localBuildings.Remove(building24);
                }
            }
            if ((this.updateFilter % 10) == 0)
            {
                StockpileLevels levels = new StockpileLevels();
                bool gotStockpile = this.getStockpileLevels(levels);
                GranaryLevels levels2 = new GranaryLevels();
                bool gotGranary = this.getGranaryLevels(levels2);
                int foodLevel = (int) (((((levels2.applesLevel + levels2.breadLevel) + levels2.cheeseLevel) + levels2.fishLevel) + levels2.meatLevel) + levels2.vegLevel);
                if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
                {
                    int goldLevel = 0;
                    GameEngine.Instance.Castle.adjustLevels(ref levels, ref goldLevel);
                }
                InterfaceMgr.Instance.setVillageInfoData((int) levels.woodLevel, 0, (int) levels.stoneLevel, foodLevel, gotStockpile, gotGranary, this.m_totalPeople, this.m_housingCapacity, this.m_spareWorkers, (int) levels.pitchLevel, this.ViewOnly, (int) levels.ironLevel, (int) this.m_capitalGold, this.VillageID, this.m_numParishFlags);
                this.updateStats();
                this.manageBackgroundSounds();
            }
            this.updateTraders();
            this.updateFilter++;
            if (this.m_previousMousePos == this.m_lastMousePos)
            {
                if (this.m_lastOverBuildingID < 0L)
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - this.m_lastMousePosChangeTime);
                    if (span.TotalSeconds > 1.0)
                    {
                        Point lastMousePos = this.m_lastMousePos;
                        lastMousePos.X += -((int) this.backgroundSprite.DrawPos.X);
                        lastMousePos.Y += -((int) this.backgroundSprite.DrawPos.Y);
                        long buildingID = villageClickMask.getBuildingIDFromMap(lastMousePos.X, lastMousePos.Y);
                        if (buildingID < 0L)
                        {
                            this.m_lastMousePosChangeTime = DateTime.MaxValue;
                        }
                        else
                        {
                            this.m_lastOverBuildingID = buildingID;
                            VillageMapBuilding building25 = this.findBuilding(buildingID);
                            if (building25 != null)
                            {
                                bool flag3 = true;
                                if (!building25.complete)
                                {
                                    flag3 = false;
                                }
                                else if (VillageBuildingsData.buildingRequiresWorker(building25.buildingType) && (!building25.buildingActive || !building25.gotEmployee))
                                {
                                    flag3 = false;
                                }
                                if (!flag3)
                                {
                                    this.m_lastMousePosChangeTime = DateTime.MaxValue;
                                }
                                else
                                {
                                    string tag = VillageBuildingsData.getBuildingNameLabel(building25.buildingType);
                                    if ((tag.Length > 0) && !GameEngine.Instance.AudioEngine.isSoundPlaying(tag))
                                    {
                                        GameEngine.Instance.playInterfaceSound(tag);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                this.m_lastOverBuildingID = -1L;
                this.m_lastMousePosChangeTime = DateTime.Now;
            }
        }

        public void updateBuildingsOnImport()
        {
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                this.runBuilding(building);
            }
        }

        public int updateConstructionDisplayTime(int secsLeft, DateTime completionTime, out int queuePosition)
        {
            this.ConstrTimeCompletionList.Clear();
            queuePosition = 0;
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                if (!building.isComplete() && !building.isDeleting())
                {
                    this.ConstrTimeCompletionList.Add(building.completionTime);
                }
            }
            if (this.ConstrTimeCompletionList.Count > 1)
            {
                queuePosition = 1;
                this.ConstrTimeCompletionList.Sort(this.buildingOrderComparer);
            }
            for (int i = 0; i < this.ConstrTimeCompletionList.Count; i++)
            {
                if (this.ConstrTimeCompletionList[i] == completionTime)
                {
                    queuePosition += i;
                    if (i == 0)
                    {
                        return secsLeft;
                    }
                    TimeSpan span = (TimeSpan) (completionTime - this.ConstrTimeCompletionList[i - 1]);
                    return (int) span.TotalSeconds;
                }
            }
            return secsLeft;
        }

        public void updateConstructionOnCachedLoad()
        {
            foreach (VillageMapBuilding building in this.localBuildings)
            {
                building.updateConstructionGFX(localBaseTime, baseServerTime, true, this);
                if (building.complete)
                {
                    building.baseSprite.clearText();
                    building.baseSprite.clearSecondText();
                    if ((!building.localComplete && !building.completeRequestSent) && !this.ViewOnly)
                    {
                        if (!GameEngine.Instance.World.isCapital(this.m_villageID))
                        {
                            RemoteServices.Instance.set_VillageBuildingCompleteDataRetrieval_UserCallBack(new RemoteServices.VillageBuildingCompleteDataRetrieval_UserCallBack(this.villageBuildingCompleteDataRetrievalCallback));
                            RemoteServices.Instance.VillageBuildingCompleteDataRetrieval(this.m_villageID, building.buildingID);
                            building.completeRequestSent = true;
                        }
                        else
                        {
                            building.complete = true;
                            building.localComplete = true;
                        }
                    }
                }
            }
        }

        private void updateGFXState(VillageMapBuilding building)
        {
            int buildingType = building.buildingType;
            if (buildingType == 1)
            {
                switch (GameEngine.Instance.World.UserResearchData.Research_HousingCapacity)
                {
                    case 2:
                    case 3:
                        buildingType = 0x27;
                        break;

                    case 4:
                    case 5:
                        buildingType = 40;
                        break;

                    case 6:
                        buildingType = 0x4c;
                        break;

                    case 7:
                    case 8:
                    case 9:
                        buildingType = 0x4d;
                        break;
                }
            }
            building.lastOpenState = building.open;
            if (s_villageBuildingData[buildingType].hasOpen)
            {
                if (!building.open)
                {
                    if (building.shadowSprite != null)
                    {
                        building.shadowSprite.reInitialize(s_villageBuildingData[buildingType].shadowGfxTexID, s_villageBuildingData[buildingType].shadowGfxID);
                    }
                    if (building.shadowSprite != null)
                    {
                        building.baseSprite.reInitialize(s_villageBuildingData[buildingType].baseGfxTexID, s_villageBuildingData[buildingType].baseGfxID);
                    }
                    if (building.animSprite != null)
                    {
                        if (s_villageBuildingData[buildingType].animOnOpenOnly)
                        {
                            building.animSprite.Visible = false;
                        }
                        else
                        {
                            building.animSprite.Visible = true;
                        }
                    }
                }
                else
                {
                    if ((building.shadowSprite != null) && (s_villageBuildingData[buildingType].shadowOpenGfxTexID != -1))
                    {
                        building.shadowSprite.reInitialize(s_villageBuildingData[buildingType].shadowOpenGfxTexID, s_villageBuildingData[buildingType].shadowOpenGfxID);
                    }
                    if ((building.shadowSprite != null) && (s_villageBuildingData[buildingType].baseOpenGfxTexID != -1))
                    {
                        building.baseSprite.reInitialize(s_villageBuildingData[buildingType].baseOpenGfxTexID, s_villageBuildingData[buildingType].baseOpenGfxID);
                    }
                    if (building.animSprite != null)
                    {
                        building.animSprite.Visible = true;
                    }
                }
            }
            else if (building.open)
            {
                if (building.animSprite != null)
                {
                    building.animSprite.Visible = true;
                }
            }
            else if (building.animSprite != null)
            {
                if (s_villageBuildingData[buildingType].animOnOpenOnly)
                {
                    building.animSprite.Visible = false;
                }
                else
                {
                    building.animSprite.Visible = true;
                }
            }
        }

        public void updateStats()
        {
            if (this.m_statsChangeTime != 0.0)
            {
                double num = DXTimer.GetCurrentMilliseconds() - this.m_statsChangeTime;
                if (num > 1000.0)
                {
                    if (this.m_taxLevel != this.m_taxLevelServer)
                    {
                        GameEngine.Instance.World.handleQuestObjectiveHappening(0x2713);
                    }
                    RemoteServices.Instance.set_VillageBuildingChangeRates_UserCallBack(new RemoteServices.VillageBuildingChangeRates_UserCallBack(this.villageBuildingChangeRatesCallback));
                    RemoteServices.Instance.VillageBuildingChangeRates(this.m_villageID, this.m_taxLevel, this.m_rationsLevel, this.m_aleRationsLevel, this.m_capitalTaxRate);
                    this.m_taxLevelSent = this.m_taxLevel;
                    this.m_rationsLevelSent = this.m_rationsLevel;
                    this.m_aleRationsLevelSent = this.m_aleRationsLevel;
                    this.m_capitalTaxRateSent = this.m_capitalTaxRate;
                    this.m_statsChangeTime = 0.0;
                }
            }
            this.showStats();
        }

        public void updateTraders()
        {
        }

        public void updateVillageResourcesInfoCallback(UpdateVillageResourcesInfo_ReturnType returnData)
        {
            if (returnData.Success)
            {
                VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
                if (map != null)
                {
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                }
                setServerTime(returnData.currentTime);
                GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
            }
        }

        public void villageBuildingChangeRatesCallback(VillageBuildingChangeRates_ReturnType returnData)
        {
            if (returnData.Success)
            {
                VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
                if (map != null)
                {
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    setServerTime(returnData.currentTime);
                    if (returnData.villageBuildings != null)
                    {
                        foreach (VillageMapBuilding building in map.Buildings)
                        {
                            foreach (VillageBuildingReturnData data in returnData.villageBuildings)
                            {
                                if (building.buildingID == data.buildingID)
                                {
                                    building.createFromReturnData(data);
                                    building.initStorageBuilding(this.gfx, this);
                                    building.updateSymbolGFX();
                                    break;
                                }
                            }
                        }
                    }
                }
                GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
            }
        }

        public void villageBuildingCompleteDataRetrievalCallback(VillageBuildingCompleteDataRetrieval_ReturnType returnData)
        {
            if (!returnData.Success)
            {
                if (returnData.m_errorCode == ErrorCodes.ErrorCode.VILLAGE_BUILDINGS_NO_LONGER_OWNER)
                {
                    GameEngine.Instance.displayedVillageLost(this.m_villageID, true);
                }
            }
            else
            {
                VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
                if (map != null)
                {
                    int buildingType = -1;
                    foreach (VillageMapBuilding building in map.Buildings)
                    {
                        if (building.buildingID == returnData.buildingID)
                        {
                            setServerTime(returnData.currentTime);
                            if (returnData.villageBuilding != null)
                            {
                                building.createFromReturnData(returnData.villageBuilding);
                                building.initStorageBuilding(this.gfx, this);
                                building.updateConstructionGFX(localBaseTime, baseServerTime, true, this);
                                building.updateSymbolGFX();
                            }
                            buildingType = building.buildingType;
                            break;
                        }
                    }
                    map.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
                    GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
                    GameEngine.Instance.World.setPoints(returnData.currentPoints);
                    if (returnData.cards != null)
                    {
                        GameEngine.Instance.World.UserCardData = returnData.cards;
                    }
                    if (returnData.traders != null)
                    {
                        map.importTraders(returnData.traders, returnData.currentTime);
                    }
                    switch (buildingType)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 0x23:
                            RemoteServices.Instance.GetVillageBuildingsList(this.m_villageID, false, false);
                            return;

                        default:
                            return;
                    }
                }
            }
        }

        public List<VillageMapBuilding> Buildings
        {
            get
            {
                return this.localBuildings;
            }
        }

        private GraphicsMgr GFX
        {
            get
            {
                return this.gfx;
            }
        }

        public int LocallyMade_Archers
        {
            get
            {
                return (this.localMadeTroops_Archers + this.localMadeTroopsSent_Archers);
            }
        }

        public int LocallyMade_Captains
        {
            get
            {
                return (this.localMadeTroops_Captains + this.localMadeTroopsSent_Captains);
            }
        }

        public int LocallyMade_Catapults
        {
            get
            {
                return (this.localMadeTroops_Catapults + this.localMadeTroopsSent_Catapults);
            }
        }

        public int LocallyMade_Peasants
        {
            get
            {
                return (this.localMadeTroops_Peasants + this.localMadeTroopsSent_Peasants);
            }
        }

        public int LocallyMade_Pikemen
        {
            get
            {
                return (this.localMadeTroops_Pikemen + this.localMadeTroopsSent_Pikemen);
            }
        }

        public int LocallyMade_Scouts
        {
            get
            {
                return (this.localMadeTroops_Scouts + this.localMadeTroopsSent_Scouts);
            }
        }

        public int LocallyMade_Swordsmen
        {
            get
            {
                return (this.localMadeTroops_Swordsmen + this.localMadeTroopsSent_Swordsmen);
            }
        }

        public double ViewHonour
        {
            get
            {
                return this.viewHonour;
            }
            set
            {
                this.viewHonour = value;
            }
        }

        public bool ViewOnly
        {
            get
            {
                return this.viewOnly;
            }
            set
            {
                this.viewOnly = value;
                InterfaceMgr.Instance.SetVillageViewMode(this.viewOnly);
            }
        }

        public static VillageBuildingDataNew[] villageBuildingData
        {
            get
            {
                return s_villageBuildingData;
            }
            set
            {
                s_villageBuildingData = value;
                s_villageBuildingData[0x17].animArray = updatedSaltWorkerAnim;
                s_villageBuildingData[15].animArray = updatedVegWorkerAnim;
                s_villageBuildingData[0x15].animCount = 0x18;
            }
        }

        public int VillageID
        {
            get
            {
                return this.m_villageID;
            }
        }

        public static VillageLayoutNew[] villageLayout
        {
            get
            {
                return s_villageLayout;
            }
            set
            {
                s_villageLayout = value;
            }
        }

        public int VillageMapType
        {
            get
            {
                return this.m_villageMapType;
            }
        }

        public class ArmouryLevels
        {
            public int armourLeftToMake;
            public double armourLevel;
            public int bowsLeftToMake;
            public double bowsLevel;
            public int catapultsLeftToMake;
            public double catapultsLevel;
            public int pikesLeftToMake;
            public double pikesLevel;
            public int swordsLeftToMake;
            public double swordsLevel;
        }

        public class BuildingOrderComparer : IComparer<DateTime>
        {
            public int Compare(DateTime x, DateTime y)
            {
                return x.CompareTo(y);
            }
        }

        public class GranaryLevels
        {
            public double applesLevel;
            public double breadLevel;
            public double cheeseLevel;
            public double fishLevel;
            public double meatLevel;
            public double vegLevel;
        }

        public class InnLevels
        {
            public double aleLevel;
        }

        public class StockpileLevels
        {
            public double ironLevel;
            public double pitchLevel;
            public double stoneLevel;
            public double woodLevel;
        }

        public class TownHallLevels
        {
            public double clothesLevel;
            public double furnitureLevel;
            public double metalwareLevel;
            public double saltLevel;
            public double silkLevel;
            public double spicesLevel;
            public double venisonLevel;
            public double wineLevel;
        }

        public class VillageAnimal
        {
            public int baseIdleFrame;
            public int buildingType;
            private static short[] chickenIdleAnim = new short[] { 
                0, 8, 0x10, 0x18, 0x20, 40, 0x30, 0x38, 0x40, 0x48, 80, 0x58, 0x60, 0x68, 0x70, 120, 
                0x70, 0x68, 0x60, 0x58, 80, 0x48, 0x40, 0x38, 0x30, 0x38, 0x40, 0x48, 80, 0x58, 0x60, 0x68, 
                0x70, 120, 0x70, 0x68, 0x60, 0x58, 80, 0x48, 0x40, 0x38, 0x30, 0x38, 0x40, 0x48, 80, 0x58, 
                0x60, 0x68, 0x70, 120, 0x70, 0x68, 0x60, 0x58, 80, 0x48, 0x40, 0x38, 0x30, 0x38, 0x40, 0x48, 
                80, 0x58, 0x60, 0x68, 0x70, 120, 0x70, 0x68, 0x60, 0x58, 80, 0x48, 0x40, 0x38, 0x30, 0x38, 
                0x40, 0x48, 80, 0x58, 0x60, 0x68, 0x70, 120, 0x70, 0x68, 0x60, 0x58, 80, 0x48, 0x40, 0x38, 
                40, 0x20, 0x18, 0x10, 8, 0
             };
            public Point currentPos = new Point();
            public int cycleCount;
            public Point endPos = new Point();
            public int fadeDir;
            public bool flock;
            public int id;
            public short[] idleAnim;
            public int idleTime = 1;
            public int journeyLength = 1;
            public int numIdleFrames = 1;
            public int numWalkFrames = 1;
            private static short[] pigIdleAnim = new short[] { 
                0, 8, 0x10, 0x18, 0x20, 40, 0x30, 0x38, 0x40, 0x48, 80, 0x58, 0x60, 0x68, 0x70, 120, 
                0x20, 40, 0x30, 0x38, 0x40, 0x48, 80, 0x58, 0x60, 0x68, 0x70, 120, 0x20, 40, 0x30, 0x38, 
                0x40, 0x48, 80, 0x58, 0x60, 0x68, 0x70, 120, 0x20, 40, 0x30, 0x38, 0x40, 0x48, 80, 0x58, 
                0x60, 0x68, 0x70, 120, 0x20, 40, 0x30, 0x38, 0x40, 0x48, 80, 0x58, 0x60, 0x68, 0x70, 120, 
                0x20, 40, 0x30, 0x38, 0x40, 0x48, 80, 0x58, 0x60, 0x68, 0x70, 120, 0x20, 40, 0x30, 0x38, 
                0x40, 0x48, 80, 0x58, 0x60, 0x68, 0x70, 120, 0x10, 8
             };
            public Random rand;
            public int randValue;
            public int range;
            public SpriteWrapper sprite;
            public Point startPos = new Point();
            public int state;
            public int tick;

            public void createJourney()
            {
                this.journeyLength = 1;
                double num = Math.Sqrt((double) (((this.endPos.X - this.startPos.X) * (this.endPos.X - this.startPos.X)) + ((this.endPos.Y - this.startPos.Y) * (this.endPos.Y - this.startPos.Y)))) * 1.0;
                this.journeyLength = (int) num;
                this.sprite.initDirectionality(8, 7, false);
                this.sprite.initAnim(0, this.numWalkFrames, 8, 50);
                this.sprite.setFacing(this.startPos, this.endPos);
            }

            public void dispose()
            {
                if (this.sprite != null)
                {
                    this.sprite.RemoveSelfFromParent();
                    this.sprite = null;
                }
            }

            public void fadeToSolid()
            {
                this.fadeDir = 10;
            }

            public void fadeToTransparent()
            {
                this.fadeDir = -10;
            }

            public Point findAnimalTarget(VillageMapBuilding building, VillageMap vm, int range)
            {
                return vm.findEmptyTile(building.buildingLocation, range, this.rand);
            }

            public Point findAnimalTarget(Point from, VillageMap vm, int range)
            {
                Point location = new Point(from.X / 0x20, from.Y / 0x10);
                return vm.findEmptyTile(location, range, this.rand);
            }

            public void init(VillageMapBuilding building)
            {
                this.sprite = new SpriteWrapper();
                if (GameEngine.Instance.Village != null)
                {
                    this.state = 0;
                    this.tick = 0;
                    this.rand = new Random(VillageMap.getCurrentServerTime().Millisecond + (this.id * 50));
                    this.randValue = this.rand.Next(0x100);
                    GameEngine.Instance.Village.addChildSprite(this.sprite, 15);
                    this.buildingType = building.buildingType;
                    int buildingType = this.buildingType;
                    switch (buildingType)
                    {
                        case 3:
                            Point point2;
                            this.range = 50;
                            if ((this.randValue & 1) == 0)
                            {
                                this.sprite.Initialize(GameEngine.Instance.Village.GFX, GFXLibrary.Instance.ChickenWhiteAnimTexID, 0);
                            }
                            else
                            {
                                this.sprite.Initialize(GameEngine.Instance.Village.GFX, GFXLibrary.Instance.ChickenBrownAnimTexID, 0);
                            }
                            this.sprite.Center = new PointF(50f, 68f);
                            this.numWalkFrames = 0x10;
                            this.numIdleFrames = 0x10;
                            this.baseIdleFrame = 0x80;
                            point2 = new Point(building.buildingLocation.X, building.buildingLocation.Y) {
                                // TODO: разобраться
                                X = building.buildingLocation.X * 0x20 + (((((this.id * 0x18) - 0x10) - 4) - 4) - 0x48),
                                Y = building.buildingLocation.Y * 0x10 + ((8 + (this.id * 12)) - 4),
                                //X = building.buildingLocation.X + (((((this.id * 0x18) - 0x10) - 4) - 4) - 0x48),
                                //Y = building.buildingLocation.Y + ((8 + (this.id * 12)) - 4)
                            };
                            this.sprite.PosX = point2.X;
                            this.sprite.PosY = point2.Y;
                            this.sprite.initDirectionality(8, 7, false);
                            this.sprite.initAnim(this.baseIdleFrame, chickenIdleAnim, 100);
                            this.idleAnim = chickenIdleAnim;
                            this.sprite.Facing = 1;
                            this.currentPos = this.startPos = this.endPos = point2;
                            this.idleTime = this.randValue % 20;
                            return;

                        case 0x10:
                            Point point;
                            this.range = 20;
                            this.sprite.Initialize(GameEngine.Instance.Village.GFX, GFXLibrary.Instance.PigAnimTexID, 0);
                            this.sprite.AutoCentre = true;
                            this.numWalkFrames = 0x10;
                            this.numIdleFrames = 0x10;
                            this.baseIdleFrame = 0x80;
                            point = new Point(building.buildingLocation.X, building.buildingLocation.Y) {
                                // TODO: разобраться
                                X = building.buildingLocation.X * 0x20 + ((((this.id * 0x18) - 0x10) - 4) - 4),
                                Y = building.buildingLocation.Y * 0x10 + ((8 + (this.id * 12)) - 4),
                                //X = building.buildingLocation.X + ((((this.id * 0x18) - 0x10) - 4) - 4),
                                //Y = building.buildingLocation.Y + ((8 + (this.id * 12)) - 4)
                            };
                            this.sprite.PosX = point.X;
                            this.sprite.PosY = point.Y;
                            this.sprite.initDirectionality(8, 7, false);
                            this.sprite.initAnim(this.baseIdleFrame, pigIdleAnim, 100);
                            this.idleAnim = pigIdleAnim;
                            this.sprite.Facing = 1;
                            this.currentPos = this.startPos = this.endPos = point;
                            this.idleTime = ((this.randValue % 3) + 1) * 30;
                            return;
                    }
                    if (buildingType == 0x13)
                    {
                        Point point3;
                        this.range = 40;
                        this.sprite.Initialize(GameEngine.Instance.Village.GFX, GFXLibrary.Instance.SheepAnimTexID, 0);
                        this.sprite.AutoCentre = true;
                        this.numWalkFrames = 0x10;
                        this.numIdleFrames = 0x19;
                        this.baseIdleFrame = 200;
                        point3 = new Point(building.buildingLocation.X, building.buildingLocation.Y) {
                            // TODO: разобраться
                            X = building.buildingLocation.X * 0x20 + ((((this.id * 0x18) - 0x10) - 4) - 0x30),
                            Y = building.buildingLocation.Y * 0x10 + (((8 + (this.id * 12)) - 4) - 2),
                            //X = building.buildingLocation.X + ((((this.id * 0x18) - 0x10) - 4) - 0x30),
                            //Y = building.buildingLocation.Y + (((8 + (this.id * 12)) - 4) - 2)
                        };
                        this.sprite.PosX = point3.X;
                        this.sprite.PosY = point3.Y;
                        this.sprite.initDirectionality(8, 7, false);
                        this.sprite.initAnim(this.baseIdleFrame, this.numIdleFrames, 8, 100);
                        this.sprite.Facing = 1;
                        this.currentPos = this.startPos = this.endPos = point3;
                        this.idleTime = (this.randValue % 20) + 1;
                        if (this.id > 0)
                        {
                            this.idleTime += 20;
                        }
                        this.flock = true;
                    }
                }
            }

            private void manageFadeOverBuildings(VillageMapBuilding building)
            {
                Point currentPos = this.currentPos;
                List<long> list = new List<long>();
                for (int i = 0; i < 0x10; i++)
                {
                    Point point2 = new Point((currentPos.X - 8) + i, currentPos.Y + 5);
                    Point point3 = new Point((currentPos.X - 8) + i, currentPos.Y - 30);
                    long item = VillageMap.villageClickMask.getBuildingIDFromMap(point2.X, point2.Y);
                    long num3 = VillageMap.villageClickMask.getBuildingIDFromMap(point3.X, point3.Y);
                    if (item >= 0L)
                    {
                        list.Add(item);
                    }
                    if (num3 >= 0L)
                    {
                        list.Add(num3);
                    }
                }
                for (int j = 0; j < 0x23; j++)
                {
                    Point point4 = new Point(currentPos.X - 8, (currentPos.Y - 30) + j);
                    Point point5 = new Point(currentPos.X + 8, (currentPos.Y - 30) + j);
                    long num5 = VillageMap.villageClickMask.getBuildingIDFromMap(point4.X, point4.Y);
                    long num6 = VillageMap.villageClickMask.getBuildingIDFromMap(point5.X, point5.Y);
                    if (num5 >= 0L)
                    {
                        list.Add(num5);
                    }
                    if (num6 >= 0L)
                    {
                        list.Add(num6);
                    }
                }
                if ((list.Count == 0) || list.Contains(building.buildingID))
                {
                    this.fadeToSolid();
                }
                else
                {
                    this.fadeToTransparent();
                }
                int num7 = this.sprite.ColorToUse.A + this.fadeDir;
                if (num7 < 120)
                {
                    num7 = 120;
                }
                else if (num7 > 0xff)
                {
                    num7 = 0xff;
                }
                this.sprite.ColorToUse = Color.FromArgb((byte) num7, 0xff, 0xff, 0xff);
            }

            public void recreate(VillageMapBuilding building)
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.addChildSprite(this.sprite, 15);
                }
            }

            public void run(VillageMapBuilding building, VillageMap vm, VillageMap.VillageAnimal parent, int tickRate)
            {
                Point point;
                this.tick += tickRate;
                switch (this.state)
                {
                    case 0:
                        if (this.tick <= this.idleTime)
                        {
                            return;
                        }
                        this.tick = 0;
                        point = new Point();
                        this.state++;
                        this.cycleCount++;
                        if (this.cycleCount <= 3)
                        {
                            if ((parent != null) && this.flock)
                            {
                                point = this.findAnimalTarget(parent.endPos, vm, 8);
                            }
                            else
                            {
                                point = this.findAnimalTarget(building, vm, this.range);
                            }
                            point.X *= 0x20;
                            point.Y *= 0x10;
                            point.Y += 8;
                            break;
                        }
                        this.cycleCount = 0;
                        point = new Point(building.buildingLocation.X, building.buildingLocation.Y) {
                            X = building.buildingLocation.X * 0x20 + ((((this.id * 0x18) - 0x10) - 4) - 4),
                            Y = building.buildingLocation.Y * 0x10 + ((8 + (this.id * 12)) - 4),
                            //X = point.X + ((((this.id * 0x18) - 0x10) - 4) - 4),
                            //Y = point.Y + ((8 + (this.id * 12)) - 4)
                        };
                        break;

                    case 1:
                    {
                        if (!this.updateJourney())
                        {
                            this.manageFadeOverBuildings(building);
                            return;
                        }
                        this.tick = 0;
                        this.state = 0;
                        this.randValue = this.rand.Next(0x100);
                        this.idleTime = ((this.randValue % 15) + 5) * 30;
                        int facing = this.sprite.Facing;
                        this.sprite.initDirectionality(8, 7, false);
                        if (this.idleAnim == null)
                        {
                            this.sprite.initAnim(this.baseIdleFrame, this.numIdleFrames, 8, 100);
                        }
                        else
                        {
                            this.sprite.initAnim(this.baseIdleFrame, this.idleAnim, 100);
                        }
                        if (this.cycleCount == 0)
                        {
                            this.sprite.Facing = 1;
                        }
                        else
                        {
                            this.sprite.Facing = facing;
                        }
                        this.fadeToSolid();
                        return;
                    }
                    default:
                        return;
                }
                this.startPos = this.currentPos;
                this.endPos = point;
                this.createJourney();
            }

            public bool updateJourney()
            {
                if (this.tick >= this.journeyLength)
                {
                    this.sprite.PosX = this.endPos.X;
                    this.sprite.PosY = this.endPos.Y;
                    this.currentPos.X = (int) this.sprite.PosX;
                    this.currentPos.Y = (int) this.sprite.PosY;
                    return true;
                }
                this.sprite.PosX = (((this.endPos.X - this.startPos.X) * this.tick) / this.journeyLength) + this.startPos.X;
                this.sprite.PosY = (((this.endPos.Y - this.startPos.Y) * this.tick) / this.journeyLength) + this.startPos.Y;
                this.currentPos.X = (int) this.sprite.PosX;
                this.currentPos.Y = (int) this.sprite.PosY;
                return false;
            }
        }

        public class VillageAnimalCollection
        {
            public List<VillageMap.VillageAnimal> animals = new List<VillageMap.VillageAnimal>();
            public long buildingID = -1L;
        }
    }
}

