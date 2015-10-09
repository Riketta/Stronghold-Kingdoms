namespace Kingdoms
{
    using DXGraphics;
    using System;
    using System.Drawing;

    public class VillageMapBuildingStockpileExtension
    {
        public SpriteWrapper[] cell = new SpriteWrapper[0x10];
        public const int stockpileBaseX = -96;
        public const int stockpileBaseY = -43;
        public static int[] stockpileLayout = new int[] { 
            0x60, 0, 0x40, 0x10, 0x80, 0x10, 0x20, 0x20, 0x60, 0x20, 160, 0x20, 0, 0x30, 0x40, 0x30, 
            0x80, 0x30, 0xc0, 0x30, 0x20, 0x40, 0x60, 0x40, 160, 0x40, 0x40, 80, 0x80, 80, 0x60, 0x60
         };

        public void colorSprites(Color col)
        {
            for (int i = 0; i < 0x10; i++)
            {
                if (this.cell[i] != null)
                {
                    this.cell[i].ColorToUse = col;
                }
            }
        }

        public void dispose()
        {
            for (int i = 0; i < 0x10; i++)
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
                    case 6:
                        spriteNo += 0x90;
                        break;

                    case 7:
                        spriteNo += 0xe0;
                        break;

                    case 8:
                        spriteNo += 0x30;
                        break;

                    case 9:
                        spriteNo += 0xd0;
                        break;
                }
                this.cell[cellID].Initialize(gfx, GFXLibrary.Instance.Goods1TexID, spriteNo);
                PointF tf = new PointF(32f, 101f);
                this.cell[cellID].Center = tf;
                this.cell[cellID].PosX = posX;
                this.cell[cellID].PosY = posY;
            }
        }
    }
}

