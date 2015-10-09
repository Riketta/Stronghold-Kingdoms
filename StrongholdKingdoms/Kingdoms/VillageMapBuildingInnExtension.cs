namespace Kingdoms
{
    using DXGraphics;
    using System;
    using System.Drawing;

    public class VillageMapBuildingInnExtension
    {
        public SpriteWrapper[] cell = new SpriteWrapper[3];
        public const int innBaseX = -80;
        public const int innBaseY = -44;
        public static int[] innLayout = new int[] { 160, 0x40, 0x80, 80, 0x60, 0x60 };
        public const int numPilesAtInn = 3;

        public void colorSprites(Color col)
        {
            for (int i = 0; i < 3; i++)
            {
                if (this.cell[i] != null)
                {
                    this.cell[i].ColorToUse = col;
                }
            }
        }

        public void dispose()
        {
            for (int i = 0; i < 3; i++)
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
                spriteNo += 0xc0;
                this.cell[cellID].Initialize(gfx, GFXLibrary.Instance.Goods1TexID, spriteNo);
                PointF tf = new PointF(32f, 101f);
                this.cell[cellID].Center = tf;
                this.cell[cellID].PosX = posX;
                this.cell[cellID].PosY = posY;
            }
        }
    }
}

