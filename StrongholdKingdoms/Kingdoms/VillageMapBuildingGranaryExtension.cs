namespace Kingdoms
{
    using DXGraphics;
    using System;
    using System.Drawing;

    public class VillageMapBuildingGranaryExtension
    {
        public SpriteWrapper[] cell = new SpriteWrapper[0x15];
        public const int granaryBaseX = 5;
        public const int granaryBaseY = -33;
        public static int[] granaryLayout = new int[] { 
            8, 0x1c, 20, 0x20, -6, 0x1b, -6, 3, -6, -21, -37, 40, -37, 0x17, -37, 6, 
            0x20, 0x24, 0x29, 0x2b, 0x19, 0x27, 0x19, 0x17, 0x19, 7, -5, 50, -5, 0x1c, -16, 0x36, 
            -16, 0x18, 11, 0x3a, 11, 0x24, 0, 0x3e, 0, 0x20
         };

        public void colorSprites(Color col)
        {
            for (int i = 0; i < 0x15; i++)
            {
                if (this.cell[i] != null)
                {
                    this.cell[i].ColorToUse = col;
                }
            }
        }

        public void dispose()
        {
            for (int i = 0; i < 0x15; i++)
            {
                this.cell[i] = null;
            }
        }

        public void showGood(GraphicsMgr gfx, int cellID, int buildingType, int level)
        {
            if ((buildingType < 0) || (level == 0))
            {
                this.cell[cellID].Visible = false;
            }
            else
            {
                this.cell[cellID].Visible = true;
                float posX = this.cell[cellID].PosX;
                float posY = this.cell[cellID].PosY;
                int spriteNo = level - 1;
                switch (buildingType)
                {
                    case 13:
                        spriteNo += 80;
                        break;

                    case 14:
                        spriteNo += 0x80;
                        break;

                    case 15:
                        spriteNo += 160;
                        break;

                    case 0x10:
                        spriteNo += 0x70;
                        break;

                    case 0x11:
                        spriteNo += 0x60;
                        break;

                    case 0x12:
                        spriteNo += 0x40;
                        break;
                }
                this.cell[cellID].Initialize(gfx, GFXLibrary.Instance.Goods2TexID, spriteNo);
                PointF tf = new PointF(32f, 101f);
                this.cell[cellID].Center = tf;
                this.cell[cellID].PosX = posX;
                this.cell[cellID].PosY = posY;
            }
        }
    }
}

