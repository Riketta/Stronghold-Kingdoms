namespace Kingdoms
{
    using DXGraphics;
    using System;

    public class Building
    {
        private int m_type;
        private int m_UID;
        private int m_X;
        private int m_Y;
        private SpriteWrapper sprite;

        public Building(int buildingType, int mapX, int mapY, int UID)
        {
            this.m_type = buildingType;
            this.m_UID = UID;
            this.m_X = mapX;
            this.m_Y = mapY;
            this.sprite = new SpriteWrapper();
        }
    }
}

