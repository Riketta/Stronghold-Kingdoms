namespace Kingdoms
{
    using CommonTypes;
    using System;

    public class WorldMapFilter
    {
        private bool filterActive;
        private bool filterAlwaysShowYourVillages = true;
        private int filterMode;
        private bool filterShowFactionSymbols = true;
        private bool filterShowHouseSymbols = true;
        private bool filterShowUserSymbols = true;
        public const int MAPFILTER_CUSTOM = 0x2710;
        public const int MAPFILTER_OFF = 0;
        public const int MAPFILTER_PRESET_AI_ONLY = 8;
        public const int MAPFILTER_PRESET_ATTACKS = 6;
        public const int MAPFILTER_PRESET_FORAGING = 3;
        public const int MAPFILTER_PRESET_MARKETS = 5;
        public const int MAPFILTER_PRESET_OPEN_FACTIONS = 7;
        public const int MAPFILTER_PRESET_TRADERS = 4;
        public const int MAPFILTER_PRESET_YOUR_FACTION = 1;
        public const int MAPFILTER_PRESET_YOUR_HOUSE = 2;

        public void setFilterMode(int mode)
        {
            if (mode == 0)
            {
                this.FilterActive = false;
            }
            else
            {
                this.FilterActive = true;
                this.filterMode = mode;
            }
        }

        public bool showArmy(WorldMap.LocalArmyData army)
        {
            WorldMap.VillageData data;
            if (!this.FilterActive || (InterfaceMgr.Instance.WorldMapMode != 0))
            {
                return true;
            }
            switch (this.filterMode)
            {
                case 6:
                    if (!GameEngine.Instance.World.isAttackingArmy(army.armyID))
                    {
                        goto Label_00F2;
                    }
                    return true;

                case 8:
                    if (army.lootType >= 0)
                    {
                        goto Label_00F2;
                    }
                    data = GameEngine.Instance.World.getVillageData(army.targetVillageID);
                    if (data == null)
                    {
                        goto Label_00F2;
                    }
                    switch (data.special)
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
                    break;

                case 3:
                    if (!GameEngine.Instance.World.isForagingArmy(army.armyID))
                    {
                        goto Label_00F2;
                    }
                    return true;

                default:
                    goto Label_00F2;
            }
            if (SpecialVillageTypes.IS_TREASURE_CASTLE(data.special))
            {
                return true;
            }
        Label_00F2:
            return false;
        }

        public long showArmy(long armyID)
        {
            if (!this.FilterActive || (InterfaceMgr.Instance.WorldMapMode != 0))
            {
                return armyID;
            }
            WorldMap.LocalArmyData army = GameEngine.Instance.World.getArmy(armyID);
            if ((army != null) && this.showArmy(army))
            {
                return armyID;
            }
            return -1L;
        }

        public bool showPeople(WorldMap.LocalPerson person)
        {
            if (this.FilterActive && (InterfaceMgr.Instance.WorldMapMode == 0))
            {
                return false;
            }
            return true;
        }

        public long showPeople(long personID)
        {
            if (!this.FilterActive || (InterfaceMgr.Instance.WorldMapMode != 0))
            {
                return personID;
            }
            WorldMap.LocalPerson person = GameEngine.Instance.World.getPerson(personID);
            if ((person != null) && this.showPeople(person))
            {
                return personID;
            }
            return -1L;
        }

        public bool showReinforcements(WorldMap.LocalArmyData army)
        {
            if (this.FilterActive && (InterfaceMgr.Instance.WorldMapMode == 0))
            {
                return false;
            }
            return true;
        }

        public long showReinforcements(long reinfID)
        {
            if (!this.FilterActive || (InterfaceMgr.Instance.WorldMapMode != 0))
            {
                return reinfID;
            }
            WorldMap.LocalArmyData army = GameEngine.Instance.World.getReinforcement(reinfID);
            if ((army != null) && this.showReinforcements(army))
            {
                return reinfID;
            }
            return -1L;
        }

        public bool showTrader(WorldMap.LocalTrader trader)
        {
            if (!this.FilterActive || (InterfaceMgr.Instance.WorldMapMode != 0))
            {
                return true;
            }
            switch (this.filterMode)
            {
                case 4:
                case 5:
                    if ((trader.trader.traderState != 1) && (trader.trader.traderState != 2))
                    {
                        if ((trader.trader.traderState > 2) && (trader.trader.traderState <= 6))
                        {
                            return true;
                        }
                        break;
                    }
                    return true;
            }
            return false;
        }

        public long showTrader(long traderID)
        {
            if (!this.FilterActive || (InterfaceMgr.Instance.WorldMapMode != 0))
            {
                return traderID;
            }
            WorldMap.LocalTrader trader = GameEngine.Instance.World.getTrader(traderID);
            if ((trader != null) && this.showTrader(trader))
            {
                return traderID;
            }
            return -1L;
        }

        public bool showVillage(WorldMap.VillageData village)
        {
            if (!this.FilterActive || (InterfaceMgr.Instance.WorldMapMode != 0))
            {
                return true;
            }
            if (this.filterAlwaysShowYourVillages && (village.userID == RemoteServices.Instance.UserID))
            {
                return true;
            }
            switch (this.filterMode)
            {
                case 1:
                    if (village.userID >= 0)
                    {
                        if (village.userID != RemoteServices.Instance.UserID)
                        {
                            int userFactionID = RemoteServices.Instance.UserFactionID;
                            if ((userFactionID < 0) || (village.factionID < 0))
                            {
                                return false;
                            }
                            if (village.factionID != userFactionID)
                            {
                                goto Label_02AE;
                            }
                        }
                        return true;
                    }
                    return false;

                case 2:
                    if (village.userID >= 0)
                    {
                        if (village.userID != RemoteServices.Instance.UserID)
                        {
                            int factionID = RemoteServices.Instance.UserFactionID;
                            if ((factionID < 0) || (village.factionID < 0))
                            {
                                return false;
                            }
                            if (village.factionID == factionID)
                            {
                                return true;
                            }
                            FactionData data2 = GameEngine.Instance.World.getFaction(factionID);
                            FactionData data3 = GameEngine.Instance.World.getFaction(village.factionID);
                            if ((data2 == null) || (data3 == null))
                            {
                                return false;
                            }
                            if ((data2.houseID != data3.houseID) || (data2.houseID == 0))
                            {
                                goto Label_02AE;
                            }
                        }
                        return true;
                    }
                    return false;

                case 3:
                    if (!GameEngine.Instance.World.isForagingSpecial(village.id))
                    {
                        if (GameEngine.Instance.World.isForagingVillage(village.id))
                        {
                            return true;
                        }
                        goto Label_02AE;
                    }
                    return true;

                case 4:
                case 5:
                    if (!GameEngine.Instance.World.isVillageTrading(village.id))
                    {
                        if (village.Capital || GameEngine.Instance.World.isVillageMarketTrading(village.id))
                        {
                            return true;
                        }
                        goto Label_02AE;
                    }
                    return true;

                case 6:
                    if (!GameEngine.Instance.World.isVillageInvolvedInAttacks(village.id))
                    {
                        goto Label_02AE;
                    }
                    return true;

                case 7:
                    if (village.userID >= 0)
                    {
                        if (village.userID != RemoteServices.Instance.UserID)
                        {
                            if ((RemoteServices.Instance.UserFactionID >= 0) || (village.factionID < 0))
                            {
                                return false;
                            }
                            FactionData data = GameEngine.Instance.World.getFaction(village.factionID);
                            if (data == null)
                            {
                                return false;
                            }
                            if (!data.openForApplications)
                            {
                                goto Label_02AE;
                            }
                        }
                        return true;
                    }
                    return false;

                case 8:
                    switch (village.special)
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
                    break;

                default:
                    goto Label_02AE;
            }
            if (GameEngine.Instance.World.isVillageInvolvedInAIAttacks(village.id))
            {
                return true;
            }
            if (SpecialVillageTypes.IS_TREASURE_CASTLE(village.special))
            {
                return true;
            }
        Label_02AE:
            return false;
        }

        public int showVillage(int villageID)
        {
            if (!this.FilterActive || (InterfaceMgr.Instance.WorldMapMode != 0))
            {
                return villageID;
            }
            WorldMap.VillageData village = GameEngine.Instance.World.getVillageData(villageID);
            if ((village != null) && this.showVillage(village))
            {
                return villageID;
            }
            return -1;
        }

        public bool FilterActive
        {
            get
            {
                return this.filterActive;
            }
            set
            {
                this.filterActive = value;
            }
        }

        public bool FilterAlwaysShowYourVillages
        {
            get
            {
                return this.filterAlwaysShowYourVillages;
            }
            set
            {
                this.filterAlwaysShowYourVillages = value;
            }
        }

        public int FilterMode
        {
            get
            {
                return this.filterMode;
            }
        }

        public bool FilterShowFactionSymbols
        {
            get
            {
                return this.filterShowFactionSymbols;
            }
            set
            {
                this.filterShowFactionSymbols = value;
            }
        }

        public bool FilterShowHouseSymbols
        {
            get
            {
                return this.filterShowHouseSymbols;
            }
            set
            {
                this.filterShowHouseSymbols = value;
            }
        }

        public bool FilterShowUserSymbols
        {
            get
            {
                return this.filterShowUserSymbols;
            }
            set
            {
                this.filterShowUserSymbols = value;
            }
        }
    }
}

