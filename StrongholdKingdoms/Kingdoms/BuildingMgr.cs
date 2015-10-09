namespace Kingdoms
{
    using System;
    using System.Collections;

    public class BuildingMgr
    {
        private ArrayList buildingList = new ArrayList();
        private int buildingUID;
        private static readonly BuildingMgr instance = new BuildingMgr();

        private BuildingMgr()
        {
        }

        public void AddBuilding(int buildingType, int mapX, int mapY)
        {
            Building building = new Building(buildingType, mapX, mapY, this.buildingUID);
            this.buildingList.Add(building);
            this.buildingUID++;
        }

        public static BuildingMgr Instance
        {
            get
            {
                return instance;
            }
        }
    }
}

