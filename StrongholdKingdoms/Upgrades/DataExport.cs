using CommonTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Kingdoms
{
    class DataExport
    {
        public static string version = "1.3.1";
        public static ControlForm controlForm;
        public static bool premium = false;

        public static List<string> Users = new List<string>();
        public static string email = null;

        public static bool Check()
        {
			return true;
        }

        public static bool IsStash(int type)
        {
            switch(type)
            {
                case 100:
                case 0x6a:
                case 0x6b:
                case 0x6c:
                case 0x6d:
                case 0x70:
                case 0x71:
                case 0x72:
                case 0x73:
                case 0x74:
                case 0x75:
                case 0x76:
                case 0x77:
                case 0x79:
                case 0x7a:
                case 0x7b:
                case 0x7c:
                case 0x7d:
                case 0x7e:
                case 0x80:
                case 0x81:
                case 130:
                case 0x83:
                case 0x84:
                case 0x85:
                    return true;
            }
            return false;
        }

        public static void Fill()
        {
         
        }

        public static void DumpWorldList(List<WorldInfo> worlds)
        {
            string str = GameEngine.getSettingsPath(true);

            StreamWriter writer = new StreamWriter(str + @"\WorldList_" + DateTime.Now.Ticks + ".txt");

            //foreach (var world in worlds)
            for (int i = 0; i < worlds.Count; i++)
            {
                WorldInfo world = worlds[i];
                writer.WriteLine("=== WORLD " + i);
                writer.WriteLine("Name: " + world.WorldName);
                writer.WriteLine("AvailableToJoin: " + world.AvailableToJoin);
                writer.WriteLine("KingdomsWorldID: " + world.KingdomsWorldID);
                writer.WriteLine("MapCulture: " + world.MapCulture);
                writer.WriteLine("NewWorld: " + world.NewWorld);
                writer.WriteLine("Online: " + world.Online);
                writer.WriteLine("ShortDesc: " + world.ShortDesc);
                writer.WriteLine("Supportculture: " + world.Supportculture);
                writer.WriteLine("Playing: " + world.Playing); // This user playing
                writer.WriteLine("Host: " + world.Host);
                writer.WriteLine("HostExt: " + world.HostExt);
                writer.WriteLine("HostPath: " + world.HostPath);
                writer.WriteLine("HostPort: " + world.HostPort);
                writer.WriteLine("HostProtocol: " + world.HostProtocol);
                writer.WriteLine("\n\n\n");
            }

            writer.Close();
        }

        public static void addBuildingToMap(VillageMapBuilding newBuilding, Point location, int buildingType)
        {

        }

        public static void getMapTileFromMousePos(Point mousePos, int mapX, int mapY)
        {
            if (mapX != -1 && mapY != -1)
                Console.WriteLine("Mouse tile coords: {0}; {1}", mapX, mapY);
        }

        public static bool isAccountPremium(WorldMap map)
        {
            if (premium)
                return true;
            return (CardTypes.isPremiumToken(map.UserCardData.premiumCard) && (VillageMap.getCurrentServerTime() < map.UserCardData.premiumCardExpiry));
        }

        public static void dumpBuildingMap(int villageID)
        {
            Console.WriteLine("Building dump");
            VillageMap map = GameEngine.Instance.getVillage(villageID);
            foreach (var building in map.Buildings)
                Console.WriteLine(((Point)building.buildingLocation).X.ToString() + " - " +
                ((Point)building.buildingLocation).Y.ToString() + " | " + building.buildingType);
        }

        public static void buildingPlacedCallback(PlaceVillageBuilding_ReturnType returnData)
        {
            Console.WriteLine("New building");
            Console.WriteLine(((Point) returnData.buildingLocation).X.ToString() + " - " + 
                ((Point) returnData.buildingLocation).Y.ToString());
            Console.WriteLine(returnData.buildingType);
        }

        public static void saveNamesData(WorldMap map)
        {
            string str = GameEngine.getSettingsPath(true);

            try
            {
                Console.WriteLine("NameData dump");
                StreamWriter writer = new StreamWriter(str + @"\NameData" + map.m_globalWorldID.ToString() + "_" + DateTime.Now.Ticks + ".txt");
                byte[] buffer = RemoteServices.Instance.WorldGUID.ToByteArray();
                writer.WriteLine(RemoteServices.Instance.WorldGUID.ToString());
                writer.WriteLine(map.storedVillageNamePos);
                int num = 0;
                for (int i = 0; i < map.villageList.Length; i++)
                {
                    writer.WriteLine(map.villageList[i].m_villageName);
                    num ^= map.villageList[i].m_villageName.GetHashCode();
                }
                for (int j = 0; j < map.regionList.Length; j++)
                {
                    writer.WriteLine(map.regionList[j].areaName);
                    num ^= map.regionList[j].areaName.GetHashCode();
                }
                for (int k = 0; k < map.countyList.Length; k++)
                {
                    writer.WriteLine(map.countyList[k].areaName);
                    num ^= map.countyList[k].areaName.GetHashCode();
                }
                for (int m = 0; m < map.provincesList.Length; m++)
                {
                    writer.WriteLine(map.provincesList[m].areaName);
                    num ^= map.provincesList[m].areaName.GetHashCode();
                }
                for (int n = 0; n < map.countryList.Length; n++)
                {
                    writer.WriteLine(map.countryList[n].areaName);
                    num ^= map.countryList[n].areaName.GetHashCode();
                }
                writer.WriteLine(num);
                writer.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }


        public static void saveFactionData(WorldMap map)
        {
                string str = GameEngine.getSettingsPath(true);
                try
                {
                    Console.WriteLine("VillageData dump");
                    StreamWriter writer = new StreamWriter(str + @"\VillageData" + map.m_globalWorldID.ToString() + "_" + DateTime.Now.Ticks + ".txt");

                    writer.WriteLine(RemoteServices.Instance.WorldGUID.ToString());
                    writer.WriteLine(map.storedVillageFactionsPos);
                    for (int i = 0; i < map.villageList.Length; i++)
                    {
                        writer.Write(map.villageList[i].factionID + " | ");
                        writer.Write(map.villageList[i].userID + " | ");
                        writer.Write(map.villageList[i].connecter + " | ");
                        writer.Write(map.villageList[i].special + " | ");
                        writer.Write(map.villageList[i].villageTerrain + " | ");
                        writer.WriteLine(map.villageList[i].numFlags);
                    }
                    writer.WriteLine(map.storedRegionFactionsPos);
                    writer.WriteLine(map.storedParishFlagsPos);
                    writer.WriteLine(map.storedCountyFlagsPos);
                    writer.WriteLine(map.storedProvinceFlagsPos);
                    writer.WriteLine(map.storedCountryFlagsPos);
                    for (int j = 0; j < map.regionList.Length; j++)
                    {
                        writer.Write(map.regionList[j].factionID + " | ");
                        writer.Write(map.regionList[j].userID + " | ");
                        writer.WriteLine(map.regionList[j].plague);
                    }
                    writer.WriteLine(map.storedCountyFactionsPos);
                    for (int k = 0; k < map.countyList.Length; k++)
                    {
                        writer.Write(map.countyList[k].factionID + " | ");
                        writer.WriteLine(map.countyList[k].userID);
                    }
                    writer.WriteLine(map.storedProvinceFactionsPos);
                    for (int m = 0; m < map.provincesList.Length; m++)
                    {
                        writer.Write(map.provincesList[m].factionID + " | ");
                        writer.WriteLine(map.provincesList[m].userID);
                    }
                    writer.WriteLine(map.storedCountryFactionsPos);
                    for (int n = 0; n < map.countryList.Length; n++)
                    {
                        writer.Write(map.countryList[n].factionID + " | ");
                        writer.WriteLine(map.countryList[n].userID);
                    }
                    for (int num7 = 0; num7 < map.villageList.Length; num7++)
                    {
                        writer.WriteLine(map.villageList[num7].visible);
                    }
                    writer.WriteLine(map.storedFactionChangesPos);
                    int num8 = 0;
                    IEnumerator enumerator = map.m_factionData.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        FactionData current = (FactionData)enumerator.Current;
                        num8++;
                    }

                    writer.WriteLine(num8);
                    foreach (FactionData data in map.m_factionData)
                    {
                        writer.Write(data.factionID + " | ");
                        writer.Write(data.active + " | ");
                        writer.Write(data.factionName + " | ");
                        writer.Write(data.factionNameAbrv + " | ");
                        writer.Write(data.houseID + " | ");
                        writer.Write(data.numMembers + " | ");
                        writer.Write(data.points + " | ");
                        writer.Write(data.flagData + " | ");
                        writer.WriteLine(data.openForApplications);
                    }
                    writer.Close();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
        }
    }
}
