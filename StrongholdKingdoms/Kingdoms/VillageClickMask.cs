namespace Kingdoms
{
    using DXGraphics;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class VillageClickMask
    {
        private List<BuildingClickMask> buildings = new List<BuildingClickMask>();
        private GraphicsMgr gfx;
        public long ignoredBuildingID = -1L;
        public bool mapClear = true;
        public bool mapDirty;
        public int mapHeight;
        public int mapWidth;
        public byte[,] maskMap;

        public void addBuilding(long buildingID, int xPos, int yPos, int textureID, int spriteNo, PointF center)
        {
            if (buildingID >= 0L)
            {
                BuildingClickMask item = new BuildingClickMask {
                    buildingID = buildingID,
                    x = xPos,
                    y = yPos,
                    center = new Point((int) center.X, (int) center.Y),
                    textureID = textureID,
                    spriteNo = spriteNo,
                    vcmID = this.buildings.Count
                };
                this.buildings.Add(item);
                this.mapDirty = true;
            }
        }

        public void clearMap()
        {
            if ((this.maskMap != null) && !this.mapClear)
            {
                for (int i = 0; i < this.mapWidth; i++)
                {
                    for (int j = 0; j < this.mapHeight; j++)
                    {
                        this.maskMap[i, j] = 0;
                    }
                }
            }
            this.mapClear = true;
            this.mapDirty = false;
        }

        public void clearMapAndBuildings()
        {
            this.clearMap();
            this.buildings.Clear();
        }

        private static int CompareBuildingByYpos(BuildingClickMask x, BuildingClickMask y)
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
            if (x.y > y.y)
            {
                return 1;
            }
            if (x.y == y.y)
            {
                return 0;
            }
            return -1;
        }

        public void forceDirtyMap()
        {
            this.mapDirty = true;
        }

        public long getBuildingIDFromMap(int mapX, int mapY)
        {
            this.rebuildMap();
            if (((mapX >= 0) && (mapX < this.mapWidth)) && ((mapY >= 0) && (mapY < this.mapHeight)))
            {
                int num = this.maskMap[mapX, mapY] - 1;
                if (num >= 0)
                {
                    if (((this.buildings.Count > 250) && (mapY > ((this.mapHeight * 3) / 4))) && ((num < 100) && ((num + 0xff) < this.buildings.Count)))
                    {
                        num += 0xff;
                    }
                    if ((num < this.buildings.Count) && (this.buildings[num] != null))
                    {
                        return this.buildings[num].buildingID;
                    }
                }
            }
            return -1L;
        }

        public void init(int width, int height, GraphicsMgr graphics)
        {
            this.gfx = graphics;
            this.mapWidth = width;
            this.mapHeight = height;
            this.maskMap = new byte[width, height];
            this.clearMap();
            this.buildings.Clear();
        }

        private void rebuildMap()
        {
            if (this.mapDirty)
            {
                this.clearMap();
                this.mapDirty = false;
                this.mapClear = false;
                this.buildings.Sort(new Comparison<BuildingClickMask>(VillageClickMask.CompareBuildingByYpos));
                int num = 0;
                foreach (BuildingClickMask mask in this.buildings)
                {
                    mask.vcmID = num++;
                    if ((mask.buildingID >= 0L) && (mask.buildingID != this.ignoredBuildingID))
                    {
                        int tagID = 1;
                        UVSpriteLoader loader = this.gfx.getSpriteLoader(mask.textureID, ref tagID);
                        if (loader != null)
                        {
                            UVSpriteLoader.MaskImage image = loader.getMask(tagID, mask.spriteNo);
                            if (image != null)
                            {
                                Rectangle rectangle;
                                PointF tf;
                                SizeF ef;
                                loader.GetSpriteXYdata(tagID, mask.spriteNo, out rectangle, out tf, out ef);
                                byte num3 = (byte) (mask.vcmID + 1);
                                if (mask.vcmID >= 0xff)
                                {
                                    num3 = (byte) (num3 + 1);
                                }
                                int num4 = (mask.x + ((int) tf.X)) - mask.center.X;
                                int num5 = (mask.y + ((int) tf.Y)) - mask.center.Y;
                                int width = rectangle.Width;
                                int height = rectangle.Height;
                                for (int i = 0; i < height; i++)
                                {
                                    if (((num5 + i) >= 0) && ((num5 + i) < this.mapHeight))
                                    {
                                        for (int j = 0; j < width; j++)
                                        {
                                            if ((((num4 + j) >= 0) && ((num4 + j) < this.mapWidth)) && image.test(j, i))
                                            {
                                                this.maskMap[num4 + j, num5 + i] = num3;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (this.ignoredBuildingID >= 0L)
                {
                    this.mapDirty = true;
                    this.ignoredBuildingID = -1L;
                }
            }
        }

        public void removeBuilding(long buildingID)
        {
            if (buildingID >= 0L)
            {
                foreach (BuildingClickMask mask in this.buildings)
                {
                    if (mask.buildingID == buildingID)
                    {
                        mask.buildingID = -1L;
                        this.mapDirty = true;
                    }
                }
            }
        }

        private class BuildingClickMask
        {
            public long buildingID;
            public Point center;
            public int spriteNo;
            public int textureID = -1;
            public int vcmID = -1;
            public int x;
            public int y;
        }
    }
}

