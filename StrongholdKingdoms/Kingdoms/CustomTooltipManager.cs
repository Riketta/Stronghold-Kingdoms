namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class CustomTooltipManager
    {
        private static int currentOverData = 0;
        private static int currentOverLastData = 0;
        private static int currentOverTooltip = 0;
        private static Form currentParentWindow = null;
        private static bool inSystemControl = false;
        private static DateTime lastLeaveTime = DateTime.MinValue;
        private static DateTime lastMouseMoveTime = DateTime.MinValue;
        private static Point lastMousePosition = new Point();
        private static int lastOverTooltip = 0;
        private static bool overTooltip = false;
        private static int showingTooltip = 0;
        private static bool staticMouse = false;
        public static int storedCurrentOverData = 0;
        public static int storedCurrentOverTooltip = 0;
        public static Form storedCurrentParentWindow = null;
        public static WorldMap.CachedUserInfo UserInfo = null;

        public static void addTooltipToSystemControl(Control control, int ID)
        {
            control.MouseLeave += new EventHandler(CustomTooltipManager.systemToolTipLeave);
            control.MouseEnter += new EventHandler(CustomTooltipManager.systemToolTipEnter);
            control.Name = ID.ToString();
        }

        public static void addTooltipZonesToSystemControl(Control control, ToolTipZone[] zones)
        {
            ToolTipZoneControlChild child = findZoneControl(control);
            if (child != null)
            {
                control.Controls.Remove(child);
            }
            else
            {
                control.MouseLeave += new EventHandler(CustomTooltipManager.systemToolTipZoneLeave);
                control.MouseEnter += new EventHandler(CustomTooltipManager.systemToolTipZoneEnter);
                control.MouseMove += new MouseEventHandler(CustomTooltipManager.systemToolTipZoneMove);
            }
            ToolTipZoneControlChild child2 = new ToolTipZoneControlChild {
                Visible = false,
                zones = zones
            };
            control.Controls.Add(child2);
        }

        public static void addTooltipZonesToTabControl(TabControl control, ToolTipZone[] zones)
        {
            ToolTipZoneTabChild child = findZoneControlInTab(control);
            if (child != null)
            {
                control.Controls.Remove(child);
            }
            else
            {
                control.MouseLeave += new EventHandler(CustomTooltipManager.systemToolTipZoneTabLeave);
                control.MouseEnter += new EventHandler(CustomTooltipManager.systemToolTipZoneTabEnter);
                control.MouseMove += new MouseEventHandler(CustomTooltipManager.systemToolTipZoneTabMove);
            }
            ToolTipZoneTabChild child2 = new ToolTipZoneTabChild {
                Visible = false,
                zones = zones
            };
            control.Controls.Add(child2);
        }

        private static bool dynamicUpdateTooltips(int ID)
        {
            int num = ID;
            return (num == 0x2710);
        }

        private static ToolTipZoneControlChild findZoneControl(object parent)
        {
            Control control = (Control) parent;
            if (control != null)
            {
                foreach (Control control2 in control.Controls)
                {
                    if (control2.GetType() == typeof(ToolTipZoneControlChild))
                    {
                        return (ToolTipZoneControlChild) control2;
                    }
                }
            }
            return null;
        }

        private static ToolTipZoneTabChild findZoneControlInTab(object parent)
        {
            Control control = (Control) parent;
            if (control != null)
            {
                foreach (Control control2 in control.Controls)
                {
                    if (control2.GetType() == typeof(ToolTipZoneTabChild))
                    {
                        return (ToolTipZoneTabChild) control2;
                    }
                }
            }
            return null;
        }

        public static string getAchievementRank(int achievement)
        {
            switch ((achievement & 0x70000000))
            {
                case 0x10000000:
                    return SK.Text("Achievements_Silver", "Silver");

                case 0x20000000:
                    return SK.Text("Achievements_Gold", "Gold");

                case 0x40000000:
                    return SK.Text("Achievements_Diamond", "Diamond");

                case 0x50000000:
                    return SK.Text("Achievements_Diamond2", "Double Diamond");

                case 0x60000000:
                    return SK.Text("Achievements_Diamond3", "Triple Diamond");

                case 0x70000000:
                    return SK.Text("Achievements_Sapphire", "Sapphire");
            }
            return SK.Text("Achievements_Iron", "Iron");
        }

        public static string getAchievementRequirement(int achievement, int rankLevel)
        {
            if ((rankLevel >= 0) && (rankLevel <= 6))
            {
                switch (achievement)
                {
                    case 1:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR_1", "Kill 20 wolves");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR_2", "Kill 200 wolves ");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR_3", "Kill 1,000 wolves");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR_4", "Kill 10,000 wolves");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR_5", "Kill 50,000 wolves");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR_6", "Kill 100,000 wolves");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR_7", "Kill 250,000 wolves");
                        }
                        break;

                    case 2:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER1", "Kill 10 bandits");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER2", "Kill 100 bandits");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER3", "Kill 500 bandits");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER4", "Kill 5,000 bandits");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER5", "Kill 10,000 bandits");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER6", "Kill 20,000 bandits");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER7", "Kill 50,000 bandits");
                        }
                        break;

                    case 3:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR1", "Kill 50 troops as an attacker");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR2", "Kill 1,000 troops as an attacker");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR3", "Kill 20,000 troops as an attacker");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR5", "Kill 50,000 troops as an attacker");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR6", "Kill 75,000 troops as an attacker");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR7", "Kill 100,000 troops as an attacker");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR4", "Kill 250,000 troops as an attacker");
                        }
                        break;

                    case 4:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER1", "Destroy 3 Wolf Lairs");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER2", "Destroy 20 Wolf Lairs");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER3", "Destroy 100 Wolf Lairs");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER4", "Destroy 300 Wolf Lairs");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER5", "Destroy 1,000 Wolf Lairs");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER6", "Destroy 5,000 Wolf Lairs");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER7", "Destroy 15,000 Wolf Lairs");
                        }
                        break;

                    case 5:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD1", "Destroy 2 Bandit Camps");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD2", "Destroy 10 Bandit Camps");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD3", "Destroy 50 Bandit Camps");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD4", "Destroy 200 Bandit Camps");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD5", "Destroy 400 Bandit Camps");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD6", "Destroy 700 Bandit Camps");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD7", "Destroy 1,000 Bandit Camps");
                        }
                        break;

                    case 6:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN1", "Destroy 1 Rat's Castle");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN2", "Destroy 3 Rat's Castles");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN3", "Destroy 10 Rat's Castles");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN4", "Destroy 25 Rat's Castles");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN5", "Destroy 50 Rat's Castles");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN6", "Destroy 75 Rat's Castles");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN7", "Destroy 100 Rat's Castles");
                        }
                        break;

                    case 7:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL1", "Destroy 1 Snakes Castle");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL2", "Destroy 3 Snakes Castles");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL3", "Destroy 10 Snakes Castles");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL4", "Destroy 25 Snakes Castles");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL5", "Destroy 50 Snakes Castles");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL6", "Destroy 75 Snakes Castles");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL7", "Destroy 100 Snakes Castles");
                        }
                        break;

                    case 8:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY1", "Destroy 1 Pig's Castle");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY2", "Destroy 3 Pig's Castles");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY3", "Destroy 10 Pig's Castles");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY4", "Destroy 25 Pig's Castles");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY5", "Destroy 50 Pig's Castles");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY6", "Destroy 75 Pig's Castles");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY7", "Destroy 100 Pig's Castles");
                        }
                        break;

                    case 9:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE1", "Destroy 1 Wolf's Castles");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE2", "Destroy 3 Wolf's Castles");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE3", "Destroy 10 Wolf's Castles");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE4", "Destroy 25 Wolf's Castles");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE5", "Destroy 50 Wolf's Castles");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE6", "Destroy 75 Wolf's Castles");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE7", "Destroy 100 Wolf's Castles");
                        }
                        break;

                    case 10:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER1", "Capture 2 Flags for your parish");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER2", "Capture 10 Flags for your parish");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER3", "Capture 50 Flags for your parish");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER4", "Capture 250 Flags for your parish");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER5", "Capture 500 Flags for your parish");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER6", "Capture 750 Flags for your parish");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER7", "Capture 1,000 Flags for your parish");
                        }
                        break;

                    case 11:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER1", "Raze 1 village");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER2", "Raze 3 villages");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER3", "Raze 10 villages");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER4", "Raze 50 villages");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER5", "Raze 100 villages");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER6", "Raze 150 villages");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER7", "Raze 250 villages");
                        }
                        break;

                    case 12:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR1", "Capture 1 village");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR2", "Capture 3 villages");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR3", "Capture 7 villages");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR4", "Capture 20 villages");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR5", "Capture 40 villages");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR6", "Capture 60 villages");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR7", "Capture 100 villages");
                        }
                        break;

                    case 13:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING1", "Pillage a total of 10 packets of goods from another player");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING2", "Pillage a total of 100 packets of goods from another player");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING3", "Pillage a total of 2,000 packets of goods from another player");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING4a", "Pillage a total of 10,000 packets of goods from another player");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING5", "Pillage a total of 20,000 packets of goods from another player");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING6", "Pillage a total of 40,000 packets of goods from another player");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING7", "Pillage a total of 75,000 packets of goods from another player");
                        }
                        break;

                    case 14:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL1", "Destroy 2 buildings through ransacking a village");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL2", "Destroy 20 buildings through ransacking a village");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL3", "Destroy 200 buildings through ransacking a village");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL4", "Destroy 2,000 buildings through ransacking a village");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL5", "Destroy 3,000 buildings through ransacking a village");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL6", "Destroy 4,000 buildings through ransacking a village");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL7", "Destroy 5,000 buildings through ransacking a village");
                        }
                        break;

                    case 15:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD1", "Destroy 1 Paladin's Castle");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD2", "Destroy 3 Paladin's Castles");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD3", "Destroy 10 Paladin's Castles");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD4", "Destroy 25 Paladin's Castles");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD5", "Destroy 50 Paladin's Castles");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD6", "Destroy 75 Paladin's Castles");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD7", "Destroy 100 Paladin's Castles");
                        }
                        break;

                    case 0x10:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASUREHUNTER1", "Capture 1 Treasure Chest from Treasure Castles");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASUREHUNTER2", "Capture 3 Treasure Chests from Treasure Castles");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASUREHUNTER3", "Capture 10 Treasure Chests from Treasure Castles");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASUREHUNTER4", "Capture 25 Treasure Chests from Treasure Castles");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASUREHUNTER5", "Capture 50 Treasure Chests from Treasure Castles");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASUREHUNTER6", "Capture 75 Treasure Chests from Treasure Castles");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASUREHUNTER7", "Capture 100 Treasure Chests from Treasure Castles");
                        }
                        break;

                    case 0x21:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CANT_TOUCH_ME1", "Survive attacks 3 consecutive enemy attacks");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CANT_TOUCH_ME2", "Survive attacks 12 consecutive enemy attacks");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CANT_TOUCH_ME3", "Survive attacks 50 consecutive enemy attacks");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CANT_TOUCH_ME4", "Survive attacks 250 consecutive enemy attacks");
                        }
                        break;

                    case 0x22:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEFENSIVE_MASTER1", "Survive an attack from an army of over 20 troops");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEFENSIVE_MASTER2", "Survive an attack from an army of over 100 troops");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEFENSIVE_MASTER3", "Survive an attack from an army of over 250 troops");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEFENSIVE_MASTER4", "Survive an attack from an army of over 400 troops");
                        }
                        break;

                    case 0x23:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HELPING_HAND1", "Your reinforcements have helped defend 1 castle");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HELPING_HAND2", "Your reinforcements have helped defend 5 castles");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HELPING_HAND3", "Your reinforcements have helped defend 25 castles");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HELPING_HAND4", "Your reinforcements have helped defend 200 castles");
                        }
                        break;

                    case 0x24:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_ROCKHARD1", "A castle survives - killing over 500 troops in a server day");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_ROCKHARD2", "A castle survives - killing over 1,000 troops in a server day");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_ROCKHARD3", "A castle survives - killing over 4,000 troops in a server day");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_ROCKHARD4", "A castle survives - killing over 10,000 troops in a server day");
                        }
                        break;

                    case 0x25:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER1", "Kill 100 troops defending your castles");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER2", "Kill 1,000 troops defending your castles");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER3", "Kill 10,000 troops defending your castles");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER4", "Kill 100,000 troops defending your castles");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER5", "Kill 250,000 troops defending your castles");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER6", "Kill 500,000 troops defending your castles");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER7", "Kill 1,000,000 troops defending your castles");
                        }
                        break;

                    case 0x41:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY1", "Fire over 50 ballistae bolts in your castles");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY2", "Fire over 250 ballistae bolts in your castles");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY3", "Fire over 4,000 ballistae bolts in your castles");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY4", "Fire over 25,000 ballistae bolts in your castles");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY5", "Fire over 100,000 ballistae bolts in your castles");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY6", "Fire over 500,000 ballistae bolts in your castles");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY7", "Fire over 1,000,000 ballistae bolts in your castles");
                        }
                        break;

                    case 0x42:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT1", "Pour over 3 oil pots in your castles");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT2", "Pour over 25 oil pots in your castles");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT3", "Pour over 200 oil pots in your castles");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT4", "Pour over 3,000 oil pots in your castles");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT5", "Pour over 10,000 oil pots in your castles");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT6", "Pour over 50,000 oil pots in your castles");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT7", "Pour over 100,000 oil pots in your castles");
                        }
                        break;

                    case 0x43:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP1", "Over 20 killing pits triggered in your castles");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP2", "Over 200 killing pits triggered in your castles");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP3", "Over 2,000 killing pits triggered in your castles");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP4", "Over 10,000 killing pits triggered in your castles");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP5", "Over 25,000 killing pits triggered in your castles");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP6", "Over 50,000 killing pits triggered in your castles");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP7", "Over 100,000 killing pits triggered in your castles");
                        }
                        break;

                    case 0x61:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEAVY_DRINKER1", "Place 5 hop farms in your villages");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEAVY_DRINKER2", "Place 10 hop farms in your villages");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEAVY_DRINKER3", "Place 30 hop farms in your villages");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEAVY_DRINKER4", "Place 60 hop farms in your villages");
                        }
                        break;

                    case 0x62:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLUTTON1", "Place 20 food farms in your villages");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLUTTON2", "Place 50 food farms in your villages");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLUTTON3", "Place 120 food farms in your villages");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLUTTON4", "Place 250 food farms in your villages");
                        }
                        break;

                    case 0x63:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GARDENER1", "Place 5 gardens in your villages");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GARDENER2", "Place 15 gardens in your villages");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GARDENER3", "Place 30 gardens in your villages");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GARDENER4", "Place 60 gardens in your villages");
                        }
                        break;

                    case 100:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER1", "Store over 20,000 gold");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER2", "Store over 200,000 gold");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER3", "Store over 1,000,000 gold");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER4", "Store over 5,000,000 gold");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER5", "Store over 10,000,000 gold");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER6", "Store over 20,000,000 gold");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER7", "Store over 50,000,000 gold");
                        }
                        break;

                    case 0x65:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY1", "Send another player over 10 packets of goods");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY2", "Send another player over 500 packets of goods");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY3", "Send another player over 2,000 packets of goods");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY4", "Send another player over 10,000 packets of goods");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY5", "Send another player over 20,000 packets of goods");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY6", "Send another player over 40,000 packets of goods");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY7", "Send another player over 60,000 packets of goods");
                        }
                        break;

                    case 0x81:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER1", "Cure over 10 points of disease");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER2", "Cure over 100 points of disease");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER3", "Cure over 1,000 points of disease");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER4", "Cure over 10,000 points of disease");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER5", "Cure over 20,000 points of disease");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER6", "Cure over 35,000 points of disease");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER7", "Cure over 50,000 points of disease");
                        }
                        break;

                    case 130:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER1", "Interdict 2 villages that you do not own");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER2", "Interdict 10 villages that you do not own");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER3", "Interdict 50 villages that you do not own");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER4", "Interdict 200 villages that you do not own");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER5", "Interdict 400 villages that you do not own");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER6", "Interdict 600 villages that you do not own");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER7", "Interdict 1,000 villages that you do not own");
                        }
                        break;

                    case 0x83:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT1", "Influence an election by 10 votes ");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT2", "Influence an election by 100 votes ");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT3", "Influence an election by 500 votes ");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT4", "Influence an election by 2,000 votes ");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT5", "Influence an election by 5,000 votes ");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT6", "Influence an election by 10,000 votes ");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT7", "Influence an election by 25,000 votes ");
                        }
                        break;

                    case 0xa1:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER1", "Scout 10 resource stashes");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER2", "Scout 50 resource stashes");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER3", "Scout 250 resource stashes");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER4", "Scout 1,000 resource stashes");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER5", "Scout 10,000 resource stashes");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER6", "Scout 50,000 resource stashes");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER7", "Scout 100,000 resource stashes");
                        }
                        break;

                    case 0xa2:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED1", "Uncover 2 resource stashes");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED2", "Uncover 20 resource stashes");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED3", "Uncover 200 resource stashes");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED4", "Uncover 2,000 resource stashes");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED5", "Uncover 5,000 resource stashes");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED6", "Uncover 10,000 resource stashes");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED7", "Uncover 25,000 resource stashes");
                        }
                        break;

                    case 0xa3:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER1", "Bring back over 20 packets of goods");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER2", "Bring back over 200 packets of goods");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER3", "Bring back over 2,000 packets of goods");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER4", "Bring back over 20,000 packets of goods");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER5", "Bring back over 50,000 packets of goods");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER6", "Bring back over 75,000 packets of goods");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER7", "Bring back over 150,000 packets of goods");
                        }
                        break;

                    case 0xc1:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLORY_STAR1", "Be a member of a house that wins 1 glory round");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLORY_STAR2", "Be a member of a house that wins 2 glory rounds");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLORY_STAR3", "Be a member of a house that wins 3 glory rounds");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLORY_STAR4", "Be a member of a house that wins 4 glory rounds");
                        }
                        break;

                    case 0xc2:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_KINGS_KIN1", "Be a member of a house that controls a county");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_KINGS_KIN2", "Be a member of a house that controls a province");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_KINGS_KIN3", "Be a member of a house that controls a country");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_KINGS_KIN4", "Be a member of a house that controls more than 1 country");
                        }
                        break;

                    case 0xc3:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LOYAL_MEMBER1", "Spend 2 weeks in the same faction");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LOYAL_MEMBER2", "Spend 10 weeks in the same faction");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LOYAL_MEMBER3", "Spend 26 weeks in the same faction");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LOYAL_MEMBER4", "Spend 52 weeks in the same faction");
                        }
                        break;

                    case 0xe1:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GENIUS1", "Research everything in 1 branch of the tree");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GENIUS2", "Research everything in 2 branches of the tree");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GENIUS3", "Research everything in 3 branches of the tree");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_GENIUS4", "Research everything in the research tree");
                        }
                        break;

                    case 0xe2:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LEARNED_SCHOLAR1", "Complete 20 researches");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LEARNED_SCHOLAR2", "Complete 80 researches");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LEARNED_SCHOLAR3", "Complete 150 researches");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LEARNED_SCHOLAR4", "Complete 500 researches");
                        }
                        break;

                    case 0x101:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER1", "Make over 1,000 gold from selling goods");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER2", "Make over 10,000 gold from selling goods");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER3", "Make over 100,000 gold from selling goods");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER4", "Make over 1,000,000 gold from selling goods");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER5", "Make over 10,000,000 gold from selling goods");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER6", "Make over 25,000,000 gold from selling goods");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER7", "Make over 50,000,000 gold from selling goods");
                        }
                        break;

                    case 0x121:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FINE_DINING1", "Hold a banquet with at least 3 goods types");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FINE_DINING2", "Hold a banquet with at least 5 goods types");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FINE_DINING3", "Hold a banquet with at least 7 goods types");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FINE_DINING4", "Hold a banquet with all 8 goods types");
                        }
                        break;

                    case 290:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING1", "Raise over 1,000 honour from banqueting");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING2", "Raise over 10,000 honour from banqueting");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING3", "Raise over 100,000 honour from banqueting");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING4", "Raise over 1,000,000 honour from banqueting");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING5", "Raise over 10,000,000 honour from banqueting");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING6", "Raise over 100,000,000 honour from banqueting");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING7", "Raise over 250,000,000 honour from banqueting");
                        }
                        break;

                    case 0x161:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER1", "Donate 10 packets to capital buildings");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER2", "Donate 100 packets to capital buildings");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER3", "Donate 1,000 packets to capital buildings");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER4", "Donate 10,000 packets to capital buildings");

                            case 4:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER5", "Donate 100,000 packets to capital buildings");

                            case 5:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER6", "Donate 250,000 packets to capital buildings");

                            case 6:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER7", "Donate 500,000 packets to capital buildings");
                        }
                        break;

                    case 0x162:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SKILLED_RULER1", "Place 3 buildings in any capital town");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SKILLED_RULER2", "Place 15 buildings in any capital town");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SKILLED_RULER3", "Place 50 buildings in any capital town");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_SKILLED_RULER4", "Place 250 buildings in any capital town");
                        }
                        break;

                    case 0x141:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FAME1", "Reach the top 100 in any of the leaderboards");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FAME2", "Reach the top 20 in any of the leaderboards");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FAME3", "Reach the top 5 in any of the leaderboards");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_FAME4", "Reach the top position in any of the leaderboards");
                        }
                        break;

                    case 0x181:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_OFFICER1", "Become a Parish Steward");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_OFFICER2", "Hold the office of Steward in 2 parishes");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_OFFICER3", "Hold the office of Steward in 3 parishes");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_OFFICER4", "Hold the office of Steward in 4 parishes");
                        }
                        break;

                    case 0x182:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HIGH_OFFICER1", "Become a Sheriff of a County");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HIGH_OFFICER2", "Hold the office of Sheriff in 2 Counties");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HIGH_OFFICER3", "Hold the office of Sheriff in 3 Counties");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_HIGH_OFFICER4", "Hold the office of Sheriff in 4 Counties");
                        }
                        break;

                    case 0x183:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MIGHTY_RULER1", "Become a Governor of a Province ");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MIGHTY_RULER2", "Hold the office of Governor in 2 Provinces");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MIGHTY_RULER3", "Hold the office of Governor in 3 Provinces");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_MIGHTY_RULER4", "Hold the office of Governor in 4 Provinces");
                        }
                        break;

                    case 0x184:
                        switch (rankLevel)
                        {
                            case 0:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIONHEART1", "Become the King of a Country");

                            case 1:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIONHEART2", "Be the King of 2 Countries");

                            case 2:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIONHEART3", "Be the King of 3 Countries");

                            case 3:
                                return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIONHEART4", "Be the King of 4 Countries");
                        }
                        break;
                }
            }
            return "";
        }

        public static string getAchievementTitle(int achievement)
        {
            switch ((achievement & 0xfffffff))
            {
                case 1:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_PROTECTOR", "Protector");

                case 2:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_LAW_BRINGER", "Law Bringer");

                case 3:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_WARRIOR", "Warrior");

                case 4:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLF_HUNTER", "Wolf Hunter");

                case 5:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_WEREGILD", "Weregild");

                case 6:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_RATTY_LOST_AGAIN", "Ratty lost again!");

                case 7:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_SNAKES_DOWNFALL", "Snakes Downfall");

                case 8:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_SQUEALPIGGY", "Squeal Piggy!");

                case 9:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_WOLFBANE", "Wolf Bane");

                case 10:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_FLAG_RAIDER", "Flag Raider");

                case 11:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_FIRESTARTER", "Firestarter");

                case 12:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_CONQUEROR", "Conqueror");

                case 13:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_VIKING", "Viking");

                case 14:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANDAL", "Vandal");

                case 15:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_EVILLORD", "Evil Lord");

                case 0x10:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_TREASURE_HUNTER", "Treasure Hunter");

                case 0x21:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_CANT_TOUCH_ME", "Can't touch me");

                case 0x22:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEFENSIVE_MASTER", "Defensive master");

                case 0x23:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_HELPING_HAND", "Helping Hand");

                case 0x24:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_ROCKHARD", "Rock hard");

                case 0x25:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_VANQUISHER", "Vanquisher");

                case 0x41:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_BALLISTA_CRAZY", "Ballista Crazy");

                case 0x42:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_FEEL_THE_HEAT", "Feel the Heat");

                case 0x43:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_DEATHTRAP", "Deathtrap");

                case 0x61:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEAVY_DRINKER", "Heavy Drinker");

                case 0x62:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLUTTON", "Glutton");

                case 0x63:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_GARDENER", "Gardener");

                case 100:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_MISER", "Miser");

                case 0x65:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_CHARITY", "Charity");

                case 0x81:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_HEALER", "Healer");

                case 130:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_PEACEBRINGER", "Peacebringer");

                case 0x83:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_DIPLOMAT", "Diplomat");

                case 0xa1:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_HORSE_MASTER", "Horse Master");

                case 0xa2:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIGHTNING_SPEED", "Lightning Speed");

                case 0xa3:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_MASTER_FORAGER", "Master Forager");

                case 0xc1:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_GLORY_STAR", "Glory Star");

                case 0xc2:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_KINGS_KIN", "King's Kin");

                case 0xc3:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_LOYAL_MEMBER", "Loyal Member");

                case 0xe1:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_GENIUS", "Genius");

                case 0xe2:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_LEARNED_SCHOLAR", "Learned Scholar");

                case 0x101:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_STOCKBROKER", "Stockbroker");

                case 0x121:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_FINE_DINING", "Fine Dining");

                case 290:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_BANQUET_KING", "Banquet King");

                case 0x161:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_TEAM_PLAYER", "Team player");

                case 0x162:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_SKILLED_RULER", "Skilled ruler");

                case 0x141:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_FAME", "Fame");

                case 0x181:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_OFFICER", "Officer");

                case 0x182:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_HIGH_OFFICER", "High Office");

                case 0x183:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_MIGHTY_RULER", "Mighty Ruler");

                case 0x184:
                    return SK.Text("TOOLTIPS_ACHIEVEMENTS_LIONHEART", "Lionheart");
            }
            return "";
        }

        public static string getHouseMotto(int houseID)
        {
            switch (houseID)
            {
                case 0:
                    return SK.Text("House_Motto_0", "The Dispossessed");

                case 1:
                    return SK.Text("House_Motto_1", "The Heavenly Ones");

                case 2:
                    return SK.Text("House_Motto_2", "High Castle");

                case 3:
                    return SK.Text("House_Motto_3", "The Dragons");

                case 4:
                    return SK.Text("House_Motto_4", "The Farmers");

                case 5:
                    return SK.Text("House_Motto_5", "The Navigators");

                case 6:
                    return SK.Text("House_Motto_6", "The Free Folk");

                case 7:
                    return SK.Text("House_Motto_7", "The Royals");

                case 8:
                    return SK.Text("House_Motto_8", "The Roses");

                case 9:
                    return SK.Text("House_Motto_9", "The Rams");

                case 10:
                    return SK.Text("House_Motto_10", "Fighters");

                case 11:
                    return SK.Text("House_Motto_11", "Heroes");

                case 12:
                    return SK.Text("House_Motto_12", "Stags");

                case 13:
                    return SK.Text("House_Motto_13", "Oak");

                case 14:
                    return SK.Text("House_Motto_14", "Beasts");

                case 15:
                    return SK.Text("House_Motto_15", "The Pure");

                case 0x10:
                    return SK.Text("House_Motto_16", "Lionheart");

                case 0x11:
                    return SK.Text("House_Motto_17", "The Insane");

                case 0x12:
                    return SK.Text("House_Motto_18", "Sinners");

                case 0x13:
                    return SK.Text("House_Motto_19", "The Double-Eagles");

                case 20:
                    return SK.Text("House_Motto_20", "Maidens");
            }
            return "";
        }

        public static void MouseEnterTooltipArea(int ID)
        {
            if (Program.mySettings.SETTINGS_showTooltips)
            {
                if (ID == 0)
                {
                    MouseLeaveTooltipArea();
                }
                else
                {
                    storedCurrentOverTooltip = currentOverTooltip = ID;
                    storedCurrentOverData = currentOverData = 0;
                    storedCurrentParentWindow = (Form) (currentParentWindow = null);
                }
            }
        }

        public static void MouseEnterTooltipArea(int ID, int data)
        {
            if (Program.mySettings.SETTINGS_showTooltips)
            {
                if (ID == 0)
                {
                    MouseLeaveTooltipArea();
                }
                else
                {
                    storedCurrentOverTooltip = currentOverTooltip = ID;
                    storedCurrentOverData = currentOverData = data;
                    storedCurrentParentWindow = (Form) (currentParentWindow = null);
                }
            }
        }

        public static void MouseEnterTooltipArea(int ID, int data, Form parentWindow)
        {
            if (Program.mySettings.SETTINGS_showTooltips)
            {
                if (ID == 0)
                {
                    MouseLeaveTooltipArea();
                }
                else
                {
                    storedCurrentOverTooltip = currentOverTooltip = ID;
                    storedCurrentOverData = currentOverData = data;
                    storedCurrentParentWindow = currentParentWindow = parentWindow;
                }
            }
        }

        public static void MouseEnterTooltipAreaStored()
        {
            MouseEnterTooltipArea(storedCurrentOverTooltip, storedCurrentOverData, storedCurrentParentWindow);
            overTooltip = true;
        }

        public static void MouseLeaveTooltipArea()
        {
            if ((currentOverTooltip != 0) && !overTooltip)
            {
                lastOverTooltip = currentOverTooltip;
                lastLeaveTime = DateTime.Now;
                currentOverTooltip = 0;
            }
        }

        public static void MouseLeaveTooltipAreaMapSpecial()
        {
            if (((currentOverTooltip != 0) && !inSystemControl) && !overTooltip)
            {
                lastOverTooltip = currentOverTooltip;
                lastLeaveTime = DateTime.Now;
                currentOverTooltip = 0;
            }
        }

        public static void MouseLeaveTooltipAreaStored()
        {
            overTooltip = false;
        }

        public static void runTooltips()
        {
            DateTime now = DateTime.Now;
            bool flag = false;
            if ((currentOverTooltip >= 0x10cc) && (currentOverTooltip <= 0x10d5))
            {
                flag = true;
                staticMouse = true;
            }
            if (!Program.mySettings.SETTINGS_instantTooltips && !flag)
            {
                Point point = new Point(Cursor.Position.X, Cursor.Position.Y);
                if (!point.Equals(lastMousePosition))
                {
                    lastMousePosition = point;
                    staticMouse = false;
                    lastMouseMoveTime = now;
                }
                else if (!staticMouse)
                {
                    TimeSpan span = (TimeSpan) (now - lastMouseMoveTime);
                    if (span.TotalMilliseconds > Program.mySettings.SETTINGS_staticMouseTime)
                    {
                        staticMouse = true;
                    }
                }
            }
            int iD = 0;
            if (currentOverTooltip != 0)
            {
                iD = currentOverTooltip;
            }
            else if (lastOverTooltip != 0)
            {
                TimeSpan span2 = (TimeSpan) (now - lastLeaveTime);
                if (span2.TotalMilliseconds > 100.0)
                {
                    lastOverTooltip = 0;
                }
            }
            if (iD != 0)
            {
                lastOverTooltip = iD;
                showToolTip(iD);
            }
            else if ((showingTooltip != 0) && (showingTooltip != lastOverTooltip))
            {
                showingTooltip = 0;
                InterfaceMgr.Instance.closeCustomTooltip();
            }
        }

        private static void showToolTip(int ID)
        {
            int num6;
            int num7;
            string str2;
            int num19;
            int num20;
            string str4;
            string str8;
            if (((!Program.mySettings.SETTINGS_instantTooltips && !staticMouse) || (showingTooltip != 0)) && ((((ID == showingTooltip) && (CustomTooltipManager.currentOverData == currentOverLastData)) && !dynamicUpdateTooltips(ID)) || (showingTooltip == 0)))
            {
                return;
            }
            Form currentParentWindow = CustomTooltipManager.currentParentWindow;
            currentOverLastData = CustomTooltipManager.currentOverData;
            showingTooltip = ID;
            string text = SK.Text("TOOLIPS_UNDEFINED", "Undefined Tooltip");
            int num22 = ID;
            switch (num22)
            {
                case 1:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_CARDS_SHOW_ALL_CARDS", "Show All Cards");
                    if (GameEngine.Instance.World.UserCardData.premiumCard > 0)
                    {
                        double totalSeconds = GameEngine.Instance.World.UserCardData.premiumCardExpiry.Subtract(VillageMap.getCurrentServerTime()).TotalSeconds;
                        double d = totalSeconds / 86400.0;
                        double num3 = (totalSeconds % 86400.0) / 3600.0;
                        double num4 = (totalSeconds % 3600.0) / 60.0;
                        str8 = text + Environment.NewLine + Environment.NewLine;
                        text = str8 + SK.Text("TOOLTIPS_LOGOUT_PREMIUM", "Your Premium Token expires in : ") + Math.Floor(d).ToString().PadLeft(2, '0') + ":" + Math.Floor(num3).ToString().PadLeft(2, '0') + ":" + Math.Floor(num4).ToString().PadLeft(2, '0') + " (dd:hh:mm)";
                    }
                    goto Label_51DF;

                case 2:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_USERNAME", "Your Username");
                    goto Label_51DF;

                case 3:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_RANKING", "Your Current Rank");
                    goto Label_51DF;

                case 4:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_HONOUR", "Your Current Honour Level");
                    goto Label_51DF;

                case 5:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_GOLD", "Your Current Gold Level");
                    goto Label_51DF;

                case 6:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_FAITHPOINTS", "Your Current Faith Points");
                    goto Label_51DF;

                case 7:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_POINTS", "Your Current Points");
                    goto Label_51DF;

                case 8:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_SECOND_AGE", "This World is in its Second Age. Click here for more details");
                    goto Label_51DF;

                case 9:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_THIRD_AGE", "This World is in its Third Age. Click here for more details");
                    goto Label_51DF;

                case 10:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_DOMINATION_WORLD", "This World uses the Domination World Rules. Click here for more details");
                    goto Label_51DF;

                case 11:
                {
                    if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1)
                    {
                        goto Label_51DF;
                    }
                    TimeSpan span = GameEngine.Instance.getDominationTimeLeft();
                    int num5 = (int) span.TotalSeconds;
                    num6 = num5 % 60;
                    num7 = (num5 / 60) % 60;
                    int num8 = (num5 / 0xe10) % 0x18;
                    int num9 = num5 / 0x15180;
                    if (span.TotalHours < 24.0)
                    {
                        string str3 = "";
                        if (num8 == 0)
                        {
                            str3 = str3 + "00:";
                        }
                        else if (num8 < 10)
                        {
                            str3 = str3 + "0" + num8.ToString() + ":";
                        }
                        else
                        {
                            str3 = str3 + num8.ToString() + ":";
                        }
                        if (num7 == 0)
                        {
                            str3 = str3 + "00:";
                        }
                        else if (num7 < 10)
                        {
                            str3 = str3 + "0" + num7.ToString() + ":";
                        }
                        else
                        {
                            str3 = str3 + num7.ToString() + ":";
                        }
                        if (num6 == 0)
                        {
                            str3 = str3 + "00";
                        }
                        else if (num6 < 10)
                        {
                            str3 = str3 + "0" + num6.ToString();
                        }
                        else
                        {
                            str3 = str3 + num6.ToString();
                        }
                        text = SK.Text("Dom_Time_Left", "Time Remaining") + " " + str3;
                        goto Label_51DF;
                    }
                    str2 = num9.ToString() + SK.Text("VillageMap_Day_Abbrev", "d") + ":";
                    if (num8 != 0)
                    {
                        if (num8 < 10)
                        {
                            str2 = str2 + "0" + num8.ToString() + ":";
                        }
                        else
                        {
                            str2 = str2 + num8.ToString() + ":";
                        }
                        break;
                    }
                    str2 = str2 + "00:";
                    break;
                }
                case 12:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_FOURTH_AGE", "This World is in its Fourth Age. Click here for more details");
                    goto Label_51DF;

                case 13:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_LEFT_FIFTH_AGE", "This World is in its Fifth Age. Click here for more details");
                    goto Label_51DF;

                case 20:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_SCROLL", "Click to Scroll Through Your Villages");
                    goto Label_51DF;

                case 0x15:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_LIST", "Click to View a List of Your Villages");
                    goto Label_51DF;

                case 0x16:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_WORLDMAP", "Click to View the Map");
                    goto Label_51DF;

                case 0x17:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_VILLAGEMAP", "Click to View Your Village");
                    goto Label_51DF;

                case 0x18:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_RESEARCH", "Click for Research");
                    goto Label_51DF;

                case 0x19:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_RANKING", "Click to Upgrade Your Rank");
                    goto Label_51DF;

                case 0x1a:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_ATTACKS", "Click to View Attacks");
                    goto Label_51DF;

                case 0x1b:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_REPORTS", "Click to View Reports");
                    goto Label_51DF;

                case 0x1c:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_MAIL", "Click to View your Mail");
                    goto Label_51DF;

                case 0x1d:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_LEADERBOARD", "Click to View the Leaderboards");
                    goto Label_51DF;

                case 30:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_FACTIONS", "Click to View your Faction and House");
                    goto Label_51DF;

                case 0x1f:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_QUESTS", "Click to View your Current Quests");
                    goto Label_51DF;

                case 0x20:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_CAPITAL", "Click to View the Parish Capital");
                    goto Label_51DF;

                case 0x21:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_PREMIUM_VO", "Click to View the Premium Village Overview Screen");
                    goto Label_51DF;

                case 40:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_VILLAGE", "Click to View Your Village Map");
                    goto Label_51DF;

                case 0x29:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CASTLE", "Click to View Your Castle Map");
                    goto Label_51DF;

                case 0x2a:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_RESOURCES", "Click to View Your Resources");
                    goto Label_51DF;

                case 0x2b:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_TRADING", "Click to Trade");
                    goto Label_51DF;

                case 0x2c:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_TROOPS", "Click to Make Troops");
                    goto Label_51DF;

                case 0x2d:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_UNITS", "Click to Make Units");
                    goto Label_51DF;

                case 0x2e:
                    text = "Coming Soon";
                    goto Label_51DF;

                case 0x2f:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_BANQUETING", "Click to Hold a Banquet");
                    goto Label_51DF;

                case 0x30:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_VASSALS", "Click to View Manage Your Vassals");
                    goto Label_51DF;

                case 0x31:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CAPITAL_INFO", "Click to View the Capital Info");
                    goto Label_51DF;

                case 50:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CAPITAL_VOTE", "Click to Vote");
                    goto Label_51DF;

                case 0x33:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CAPITAL_FORUM", "Click to View the Capital's Forum");
                    goto Label_51DF;

                case 0x34:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_NOT_IMPLEMENTED_YET", "Not Implemented Yet");
                    goto Label_51DF;

                case 0x35:
                    text = SK.Text("TOOLTIPS_MAINWINDOW_TOP_RIGHT_MAIN_TAB_CHAT", "Click to Chat");
                    goto Label_51DF;

                case 90:
                    text = GameEngine.Instance.World.getVillageNameOrType(CustomTooltipManager.currentOverData);
                    goto Label_51DF;

                case 0x5b:
                    text = SK.Text("MapFilterSelectPanel_Map_Filtering", "Map Filtering");
                    goto Label_51DF;

                case 0x5c:
                    text = SK.Text("GENERIC_Close", "Close");
                    goto Label_51DF;

                case 0x5d:
                    text = SK.Text("MapFilterSelectPanel_Filter_Active", "Filter Active");
                    goto Label_51DF;

                case 0x5e:
                    text = SK.Text("Attack_Targets", "Attack Targets");
                    goto Label_51DF;

                case 100:
                    if (CustomTooltipManager.currentOverData < 0x3e8)
                    {
                        int num10 = 0;
                        VillageMap village = null;
                        bool flag2 = true;
                        if ((GameEngine.Instance.Village != null) && GameEngine.Instance.World.isCapital(GameEngine.Instance.Village.VillageID))
                        {
                            village = GameEngine.Instance.Village;
                            if (village.m_parishCapitalResearchData != null)
                            {
                                num10 = village.m_parishCapitalResearchData.getCapitalResourceFromBuildingType(CustomTooltipManager.currentOverData);
                                if (num10 > 0)
                                {
                                    flag2 = false;
                                }
                            }
                        }
                        if (flag2)
                        {
                            text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_ROLLOVER", "Click to Place") + " : " + VillageBuildingsData.getBuildingName(CustomTooltipManager.currentOverData);
                        }
                        else
                        {
                            text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_ROLLOVER", "Click to Place") + " : " + VillageBuildingsData.getBuildingName(CustomTooltipManager.currentOverData) + " - " + SK.Text("VillageMapPanel_Current_Level", "Current Level") + " " + num10.ToString();
                        }
                    }
                    else
                    {
                        switch (CustomTooltipManager.currentOverData)
                        {
                            case 0x3e8:
                                text = SK.Text("TOOLTIPS_SUBMENU_RELIGIOUS", "Click to Open Religious Sub Menu");
                                break;

                            case 0x3e9:
                                text = SK.Text("TOOLTIPS_SUBMENU_DECORATIVE", "Click to Open Decorative Sub Menu");
                                break;

                            case 0x3ea:
                                text = SK.Text("TOOLTIPS_SUBMENU_JUSTICE", "Click to Open Justice Sub Menu");
                                break;

                            case 0x3eb:
                                text = SK.Text("TOOLTIPS_SUBMENU_ENTERTAINMENT", "Click to Open Entertainment Sub Menu");
                                break;

                            case 0x3ec:
                                text = SK.Text("TOOLTIPS_SUBMENU_SMALL_SHRINE", "Click to Open Small Shrine Sub Menu");
                                break;

                            case 0x3ed:
                                text = SK.Text("TOOLTIPS_SUBMENU_LARGE_SHRINE", "Click to Open Large Shrine Sub Menu");
                                break;

                            case 0x3ee:
                                text = SK.Text("TOOLTIPS_SUBMENU_SMALL_GARDEN", "Click to Open Formal Garden Sub Menu");
                                break;

                            case 0x3ef:
                                text = "";
                                break;

                            case 0x3f0:
                                text = SK.Text("TOOLTIPS_SUBMENU_LARGE_GARDEN", "Click to Open Flower Bed Sub Menu");
                                break;

                            case 0x3f1:
                                text = "";
                                break;

                            case 0x3f2:
                                text = SK.Text("TOOLTIPS_SUBMENU_SMALL_STATUE", "Click to Open Gilded Statue Sub Menu");
                                break;

                            case 0x457:
                                text = SK.Text("TOOLTIPS_SUBMENU_LARGE_STATUE", "Click to Open Stone Statue Sub Menu");
                                break;

                            case 0x458:
                                text = SK.Text("TOOLTIPS_SUBMENU_CAPITAL_RESOURCE", "Click to Open Resources Sub Menu");
                                break;

                            case 0x459:
                                text = SK.Text("TOOLTIPS_SUBMENU_CAPITAL_FOOD", "Click to Open Food Sub Menu");
                                break;

                            case 0x45a:
                                text = SK.Text("TOOLTIPS_SUBMENU_CAPITAL_BANQUET", "Click to Open Banquet Sub Menu");
                                break;

                            case 0x45b:
                                text = SK.Text("TOOLTIPS_SUBMENU_CAPITAL_WEAPONS", "Click to Open Weapons Sub Menu");
                                break;

                            case 0x45c:
                                text = SK.Text("TOOLTIPS_SUBMENU_CAPITAL_BANQUET_FOOD", "Click to Open Banquet Sub Menu");
                                break;

                            case 0x7d0:
                                text = SK.Text("TOOLTIPS_SUBMENU_BACK", "Click to go Back");
                                break;
                        }
                    }
                    goto Label_51DF;

                case 0x65:
                    if (CustomTooltipManager.currentOverData != 0)
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_1_CLOSE", "Click to Close Town Buildings Tab");
                    }
                    else
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_1_OPEN", "Click to Open Town Buildings Tab");
                    }
                    goto Label_51DF;

                case 0x66:
                    if (CustomTooltipManager.currentOverData != 0)
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_2_CLOSE", "Click to Close Industry Buildings Tab");
                    }
                    else
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_2_OPEN", "Click to Open Industry Buildings Tab");
                    }
                    goto Label_51DF;

                case 0x67:
                    if (CustomTooltipManager.currentOverData != 0)
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_3_CLOSE", "Click to Close Farm Buildings Tab");
                    }
                    else
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_3_OPEN", "Click to Open Farm Buildings Tab");
                    }
                    goto Label_51DF;

                case 0x68:
                    if (CustomTooltipManager.currentOverData != 0)
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_4_CLOSE", "Click to Close Weapon Production Buildings Tab");
                    }
                    else
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_4_OPEN", "Click to Open Weapon Production Buildings Tab");
                    }
                    goto Label_51DF;

                case 0x69:
                    if (CustomTooltipManager.currentOverData != 0)
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_5_CLOSE", "Click to Close Banqueting Resource Buildings Tab");
                    }
                    else
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_5_OPEN", "Click to Open Banqueting Resource Buildings Tab");
                    }
                    goto Label_51DF;

                case 0x6a:
                    if (CustomTooltipManager.currentOverData != 0)
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_1_CLOSE", "Click to Close Castle Buildings Tab");
                    }
                    else
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_1_OPEN", "Click to Open Castle Buildings Tab");
                    }
                    goto Label_51DF;

                case 0x6b:
                    if (CustomTooltipManager.currentOverData != 0)
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_2_CLOSE", "Click to Close Army Buildings Tab");
                    }
                    else
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_2_OPEN", "Click to Open Army Buildings Tab");
                    }
                    goto Label_51DF;

                case 0x6c:
                    if (CustomTooltipManager.currentOverData != 0)
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_3_CLOSE", "Click to Close Civil Buildings Tab");
                    }
                    else
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_3_OPEN", "Click to Open Civil Buildings Tab");
                    }
                    goto Label_51DF;

                case 0x6d:
                    if (CustomTooltipManager.currentOverData != 0)
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_4_CLOSE", "Click to Close Guild Buildings Tab");
                    }
                    else
                    {
                        text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_4_OPEN", "Click to Open Guild Buildings Tab");
                    }
                    goto Label_51DF;

                case 110:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_EXTRAS_BAR", "Click to View Extra Info Bars");
                    goto Label_51DF;

                case 0x6f:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_HONOUR_BAR", "Click to View Popularity To Honour Information");
                    goto Label_51DF;

                case 0x70:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_HONOUR_BAR_CLOSE", "Click to Close Popularity To Honour Information");
                    goto Label_51DF;

                case 0x71:
                    text = "";
                    if (CustomTooltipManager.currentOverData >= 0)
                    {
                        text = VillageBuildingsData.getBuildingName(CustomTooltipManager.currentOverData) + Environment.NewLine + Environment.NewLine;
                    }
                    text = text + SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_IN_BUILDING_CLOSE", "Click to Close the Selected Building Information");
                    goto Label_51DF;

                case 0x72:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_MOVE_BUILDING", "Click to Move the Selected Building");
                    goto Label_51DF;

                case 0x73:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_TAX_INC", "Click to Increase Your Tax Rate");
                    goto Label_51DF;

                case 0x74:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_TAX_DEC", "Click to Decrease Your Tax Rate");
                    goto Label_51DF;

                case 0x75:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_RATIONS_INC", "Click to Increase Your Rations");
                    goto Label_51DF;

                case 0x76:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_RATIONS_DEC", "Click to Decrease Your Rations");
                    goto Label_51DF;

                case 0x77:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_ALE_INC", "Click to Increase Your Ale Rations");
                    goto Label_51DF;

                case 120:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_ALE_DEC", "Click to Decrease Your Ale Rations");
                    goto Label_51DF;

                case 0x79:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_POPULARITY_BAR", "Click to View Popularity Information");
                    goto Label_51DF;

                case 0x7a:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_POPULARITY_BAR_CLOSE", "Click to Close Popularity Information");
                    goto Label_51DF;

                case 0x7b:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_TAX_OPEN", "Click to View Detailed Tax Information");
                    goto Label_51DF;

                case 0x7c:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_TAX_CLOSE", "Click to Close Detailed Tax Information");
                    goto Label_51DF;

                case 0x7d:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_RATIONS_OPEN", "Click to View Detailed Rations Information");
                    goto Label_51DF;

                case 0x7e:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_RATIONS_CLOSE", "Click to Close Detailed Rations Information");
                    goto Label_51DF;

                case 0x7f:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_ALE_OPEN", "Click to View Detailed Ale Rations Information");
                    goto Label_51DF;

                case 0x80:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_ALE_CLOSE", "Click to Close Detailed Ale Rations Information");
                    goto Label_51DF;

                case 0x81:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_HOUSING_OPEN", "Click to View Detailed Housing Information");
                    goto Label_51DF;

                case 130:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_HOUSING_CLOSE", "Click to Close Detailed Housing Information");
                    goto Label_51DF;

                case 0x83:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_ENTERTAINMENT_OPEN", "Click to View Detailed Buildings Information");
                    goto Label_51DF;

                case 0x84:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_ENTERTAINMENT_CLOSE", "Click to Close Detailed Buildings Information");
                    goto Label_51DF;

                case 0x85:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_EVENTS_OPEN", "Click to View Detailed Events Information");
                    goto Label_51DF;

                case 0x86:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_DETAILED_EVENTS_CLOSE", "Click to Close Detailed Events Information");
                    goto Label_51DF;

                case 140:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_RIGHT_PANEL_BUILD_INFO", "Time and Cost to Build");
                    goto Label_51DF;

                case 0x8d:
                    text = "";
                    goto Label_51DF;

                case 0x8e:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_INFO_BAR_WOOD", "Current Wood Level");
                    goto Label_51DF;

                case 0x8f:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_INFO_BAR_STONE", "Current Stone Level");
                    goto Label_51DF;

                case 0x90:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_INFO_BAR_FOOD", "Current Food Level");
                    goto Label_51DF;

                case 0x91:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_INFO_BAR_CAPITAL_GOLD", "Current Capital Gold Level");
                    goto Label_51DF;

                case 0x92:
                    text = SK.Text("TOOLTIPS_VILLAGEMAP_INFO_BAR_CAPITAL_FLAGS", "Current Flags");
                    goto Label_51DF;

                case 0x93:
                    text = SK.Text("VILLAGEMAP_INFO_BAR_DONATION_TYPE", "Type of Goods Needed to Upgrade");
                    goto Label_51DF;

                case 0x94:
                    text = SK.Text("TOOLTIPS_VO_Pitch", "Current Pitch Level");
                    goto Label_51DF;

                case 0x95:
                    text = SK.Text("TOOLTIPS_VO_Iron", "Current Iron Level");
                    goto Label_51DF;

                case 150:
                    text = VillageBuildingsData.getBuildingName(CustomTooltipManager.currentOverData) + " : " + SK.Text("VILLAGEMAP_CAPITAL_OVER_COMPLETED", "Fully Upgraded");
                    goto Label_51DF;

                case 0x97:
                    text = VillageBuildingsData.getBuildingName(CustomTooltipManager.currentOverData) + " : " + SK.Text("VILLAGEMAP_CAPITAL_OVER_NOTCOMPLETED", "Upgrades available");
                    goto Label_51DF;

                case 200:
                    if (CustomTooltipManager.currentOverData < 0x3e8)
                    {
                        text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_ROLLOVER", "Click to Place : ") + CastlesCommon.getPieceName(CustomTooltipManager.currentOverData);
                        if (((CustomTooltipManager.currentOverData == 0x26) || (CustomTooltipManager.currentOverData == 0x25)) || ((CustomTooltipManager.currentOverData == 40) || (CustomTooltipManager.currentOverData == 0x27)))
                        {
                            text = text + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_ROLLOVER_HELP", "Use Mouse Wheel or Spacebar to rotate.");
                        }
                    }
                    goto Label_51DF;

                case 0xc9:
                    if (CustomTooltipManager.currentOverData != 0)
                    {
                        text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_1_CLOSE", "Click to Close Troops Tab");
                    }
                    else
                    {
                        text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_1_OPEN", "Click to Open Troops Tab");
                    }
                    goto Label_51DF;

                case 0xca:
                    if (CustomTooltipManager.currentOverData != 0)
                    {
                        text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_2_CLOSE", "Click to Close Wood Tab");
                    }
                    else
                    {
                        text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_2_OPEN", "Click to Open Wood Tab");
                    }
                    goto Label_51DF;

                case 0xcb:
                    if (CustomTooltipManager.currentOverData != 0)
                    {
                        text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_3_CLOSE", "Click to Close Stone Tab");
                    }
                    else
                    {
                        text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_3_OPEN", "Click to Open Stone Tab");
                    }
                    goto Label_51DF;

                case 0xcc:
                    if (CustomTooltipManager.currentOverData != 0)
                    {
                        text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_4_CLOSE", "Click to Close Buildings Tab");
                    }
                    else
                    {
                        text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_4_OPEN", "Click to Open Buildings Tab");
                    }
                    goto Label_51DF;

                case 0xcd:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_TOGGLE_REINFORCEMENTS_ON", "Click to Place Reinforcements");
                    goto Label_51DF;

                case 0xce:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_TOGGLE_REINFORCEMENTS_OFF", "Click to Place Your Troops");
                    goto Label_51DF;

                case 0xcf:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_1X1", "Click to Change Placement Pattern : 1x1");
                    goto Label_51DF;

                case 0xd0:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_3X3", "Click to Change Placement Pattern : 3x3");
                    goto Label_51DF;

                case 0xd1:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_5X5", "Click to Change Placement Pattern : 5x5");
                    goto Label_51DF;

                case 210:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_1X5", "Click to Change Placement Pattern : 1x5");
                    goto Label_51DF;

                case 0xd3:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_TOGGLE_VIEWMODE_HIGH", "Click to View Castle in Full Mode");
                    goto Label_51DF;

                case 0xd4:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_TOGGLE_VIEWMODE_LOW", "Click to View Castle in Collapsed Mode");
                    goto Label_51DF;

                case 0xd5:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_TOGGLE_DELETE_ON", "Click to Start Delete Mode");
                    goto Label_51DF;

                case 0xd6:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_TOGGLE_DELETE_OFF", "Click to Stop Delete Mode");
                    goto Label_51DF;

                case 0xd7:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_CASTLE_CONSTRUCTION_OPTIONS_OPEN", "Click to View Castle Construction Options");
                    goto Label_51DF;

                case 0xd8:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_CASTLE_CONSTRUCTION_OPTIONS_CLOSE", "Click to Close Castle Construction Options");
                    goto Label_51DF;

                case 0xd9:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_REPAIR", "Click to Repair all damaged castle infrastructure.");
                    goto Label_51DF;

                case 0xda:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_DELETE_CONSTRUCTING", "Click to Delete all castle structures currently under construction");
                    goto Label_51DF;

                case 0xdb:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_CONFIRM", "Click to Upload your castle changes to the server");
                    goto Label_51DF;

                case 220:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_CANCEL", "Click to Cancel all unconfirmed changes");
                    goto Label_51DF;

                case 0xdd:
                {
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_DELETE_TROOPS", "Remove Placed Troops") + " : ";
                    int currentOverData = CustomTooltipManager.currentOverData;
                    switch (currentOverData)
                    {
                        case 70:
                            text = text + SK.Text("GENERIC_Peasants", "Peasants");
                            goto Label_51DF;

                        case 0x47:
                            text = text + SK.Text("GENERIC_Swordsmen", "Swordsmen");
                            goto Label_51DF;

                        case 0x48:
                            text = text + SK.Text("GENERIC_Archers", "Archers");
                            goto Label_51DF;

                        case 0x49:
                            text = text + SK.Text("GENERIC_Pikemen", "Pikemen");
                            goto Label_51DF;
                    }
                    if (currentOverData == 0x55)
                    {
                        text = text + SK.Text("GENERIC_Captains", "Captains");
                    }
                    goto Label_51DF;
                }
                case 0xde:
                    text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_TOGGLE_AGGRESSIVE", "Toggle Aggressive Defender State");
                    goto Label_51DF;

                case 0xdf:
                    if (CustomTooltipManager.currentOverData != 0)
                    {
                        text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_5_CLOSE", "Click to Close Parish Unlocked Structures Tab");
                    }
                    else
                    {
                        text = SK.Text("TOOLTIPS_CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_5_OPEN", "Click to Open Parish Unlocked Structures Tab");
                    }
                    goto Label_51DF;

                case 300:
                    text = SK.Text("TOOLTIPS_RESEARCHTREE_LIST_MODE", "Click to View a List of Available Researches");
                    goto Label_51DF;

                case 0x12d:
                    text = SK.Text("TOOLTIPS_RESEARCHTREE_TREE_MODE", "Click to View the Research Tree");
                    goto Label_51DF;

                case 0x12e:
                {
                    ResearchData userResearchData = GameEngine.Instance.World.UserResearchData;
                    if (((userResearchData != null) && (userResearchData.research_queueEntries != null)) && (CustomTooltipManager.currentOverData < userResearchData.research_queueEntries.Length))
                    {
                        int researchType = userResearchData.research_queueEntries[CustomTooltipManager.currentOverData];
                        text = ResearchData.getResearchName(researchType) + Environment.NewLine + Environment.NewLine;
                        DateTime time = VillageMap.getCurrentServerTime();
                        TimeSpan span2 = (TimeSpan) (userResearchData.research_completionTime - time);
                        for (int i = 0; i < (CustomTooltipManager.currentOverData + 1); i++)
                        {
                            TimeSpan span3 = userResearchData.calcResearchTime(userResearchData.research_pointCount + i, GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData);
                            if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                            {
                                span3 = new TimeSpan(span3.Ticks / 2L);
                            }
                            span2 += span3;
                        }
                        text = text + SK.Text("Research_Completed_In", "Completed In") + " : " + VillageMap.createBuildTimeString((int) span2.TotalSeconds);
                    }
                    goto Label_51DF;
                }
                case 400:
                    text = SK.Text("TOOLTIPS_RANKING_UPGRADE", "Click to Upgrade your Rank");
                    goto Label_51DF;

                case 0x191:
                    text = Rankings.getRankingName(CustomTooltipManager.currentOverData, RemoteServices.Instance.UserAvatar.male) + " (" + ((CustomTooltipManager.currentOverData + 1)).ToString() + ")";
                    goto Label_51DF;

                case 500:
                    text = SK.Text("TOOLTIPS_MAILSCREEN_FLOAT", "Click to Detach Mail Screen");
                    goto Label_51DF;

                case 0x1f5:
                    text = SK.Text("TOOLTIPS_MAILSCREEN_DOCK", "Click to Dock the Mail Screen");
                    goto Label_51DF;

                case 0x1f6:
                    text = SK.Text("TOOLTIPS_MAILSCREEN_CLOSE", "Click to Close");
                    goto Label_51DF;

                case 0x1f7:
                    text = SK.Text("TOOLTIPS_MAILSCREEN_REPORT", "This is for reporting offensive language or personal abuse only");
                    goto Label_51DF;

                case 0x1f8:
                    text = SK.Text("TOOLTIPS_MAILSCREEN_AGGRESSIVE_BLOCK", "Aggressive Mail Block, blocks from view any mail thread that contains someone on your block list. Normal mail block only removes threads that only contain users that are in your block list.");
                    goto Label_51DF;

                case 0x1f9:
                    text = SK.Text("TOOLTIPS_MAIL_SEARCH", "Search For User");
                    goto Label_51DF;

                case 0x1fa:
                    text = SK.Text("TOOLTIPS_MAIL_RECENT", "Recent Recipients");
                    goto Label_51DF;

                case 0x1fb:
                    text = SK.Text("TOOLTIPS_MAIL_FAVOURITES", "Favourites");
                    goto Label_51DF;

                case 0x1fc:
                    text = SK.Text("TOOLTIPS_MAIL_OTHERS_KNOWN", "Faction Members and Personal Allies");
                    goto Label_51DF;

                case 0x1ff:
                    text = SK.Text("TOOLTIPS_MAIL_SELECT_VILLAGE", "Select Village");
                    goto Label_51DF;

                case 0x200:
                    text = SK.Text("TOOLTIPS_MAIL_SEARCH_REGION", "Search for Parish");
                    goto Label_51DF;

                case 0x201:
                    text = SK.Text("TOOLTIPS_MAIL_CURRENT_ATTACHMENTS", "Attached Targets");
                    goto Label_51DF;

                case 0x202:
                    text = SK.Text("TOOLTIPS_MAIL_OPEN_ATTACHMENTS", "Open Targets");
                    goto Label_51DF;

                case 0x203:
                    text = SK.Text("TOOLTIPS_MAIL_PLAYER_LINK", "Link to Player");
                    goto Label_51DF;

                case 0x204:
                    text = SK.Text("TOOLTIPS_MAIL_VILLAGE_LINK", "Link to Village");
                    goto Label_51DF;

                case 0x205:
                    text = SK.Text("TOOLTIPS_MAIL_PARISH_LINK", "Link to Parish");
                    goto Label_51DF;

                case 0x206:
                    text = SK.Text("TOOLTIPS_VILLAGE_SEARCH_DISABLED", "Select a player by searching to view their villages");
                    goto Label_51DF;

                case 600:
                    text = SK.Text("TOOLTIPS_BARRACKS_DISBAND", "Click for Disband Options");
                    goto Label_51DF;

                case 0x259:
                    text = SK.Text("TOOLTIPS_BARRACKS_CLOSE", "Click to Close");
                    goto Label_51DF;

                case 0x25a:
                    text = SK.Text("GENERIC_Armed_Peasants", "Armed Peasants");
                    goto Label_51DF;

                case 0x25b:
                    text = SK.Text("GENERIC_Archers", "Archers");
                    goto Label_51DF;

                case 0x25c:
                    text = SK.Text("GENERIC_Pikemen", "Pikemen");
                    goto Label_51DF;

                case 0x25d:
                    text = SK.Text("GENERIC_Swordsmen", "Swordsmen");
                    goto Label_51DF;

                case 0x25e:
                    text = SK.Text("GENERIC_Catapults", "Catapults");
                    goto Label_51DF;

                case 0x25f:
                    text = SK.Text("GENERIC_Captains", "Captains");
                    goto Label_51DF;

                case 0x260:
                    text = SK.Text("TOOLTIPS_BARRACKS_ARCHERS_NOT_RESEARCHED", "To recruit Archers you must be Rank 6 and research 'Long Bows'.");
                    goto Label_51DF;

                case 0x261:
                    text = SK.Text("TOOLTIPS_BARRACKS_PIKEMEN_NOT_RESEARCHED", "To recruit Pikemen you must be Rank 11 and research 'Pike'.");
                    goto Label_51DF;

                case 610:
                    text = SK.Text("TOOLTIPS_BARRACKS_SWORDSMEN_NOT_RESEARCHED", "To recruit Swordsmen you must be Rank 13 and research 'Sword'.");
                    goto Label_51DF;

                case 0x263:
                    text = SK.Text("TOOLTIPS_BARRACKS_CATAPULTS_NOT_RESEARCHED", "To recruit Catapults you must be Rank 15 and research 'Catapult'.");
                    goto Label_51DF;

                case 0x264:
                    text = SK.Text("TOOLTIPS_BARRACKS_CAPTAINS_NOT_RESEARCHED", "To recruit Captains you must be Rank 12 and research 'Leadership' and 'Captains'.");
                    goto Label_51DF;

                case 700:
                    text = SK.Text("TOOLTIPS_UNITS_DISBAND", "Click for Disband Options");
                    goto Label_51DF;

                case 0x2bd:
                    text = SK.Text("TOOLTIPS_UNITS_CLOSE", "Click to Close");
                    goto Label_51DF;

                case 0x2be:
                    text = SK.Text("TOOLTIPS_UNITS_SPACE_REQUIRED", "Unit Space Required") + " : " + CustomTooltipManager.currentOverData.ToString();
                    goto Label_51DF;

                case 0x2bf:
                    text = SK.Text("GENERIC_Merchants", "Merchants");
                    goto Label_51DF;

                case 0x2c0:
                    text = SK.Text("GENERIC_Monks", "Monks");
                    goto Label_51DF;

                case 0x2c1:
                    text = SK.Text("GENERIC_Scouts", "Scouts");
                    goto Label_51DF;

                case 0x2c2:
                    text = SK.Text("TOOLTIPS_UNITS_MERCHANTS_NOT_RESEARCHED", "To recruit Merchants you must be Rank 5, research 'Merchant Guilds' and build a Market.");
                    goto Label_51DF;

                case 0x2c3:
                    text = SK.Text("TOOLTIPS_UNITS_MONKS_NOT_RESEARCHED", "To recruit Monks you must be Rank 8 and research 'Ordination'.");
                    goto Label_51DF;

                case 0x2c4:
                    text = SK.Text("TOOLTIPS_UNITS_SCOUTS_NOT_RESEARCHED", "To recruit Scouts you must research 'Scouts'.");
                    goto Label_51DF;

                case 800:
                    text = SK.Text("TOOLTIPS_STOCKEXCHANGE_CLOSE", "Click to Close");
                    goto Label_51DF;

                case 0x321:
                    text = SK.Text("TOOLTIPS_TRADE_CLOSE", "Click to Close");
                    goto Label_51DF;

                case 0x322:
                    text = SK.Text("TOOLTIPS_TRADE_RESOURCES", "Click to Trade Resources");
                    goto Label_51DF;

                case 0x323:
                    text = SK.Text("TOOLTIPS_TRADE_FOOD", "Click to Trade Food");
                    goto Label_51DF;

                case 0x324:
                    text = SK.Text("TOOLTIPS_TRADE_WEAPONS", "Click to Trade Weapons");
                    goto Label_51DF;

                case 0x325:
                    text = SK.Text("TOOLTIPS_TRADE_BANQUETING", "Click to Trade Banqueting Goods");
                    goto Label_51DF;

                case 0x326:
                    text = SK.Text("TOOLTIPS_TOGGLE_TO_STOCKEXCHANGE", "Click for Stock Exchange Trading");
                    goto Label_51DF;

                case 0x327:
                    text = SK.Text("TOOLTIPS_TOGGLE_TO_TRADING", "Click for Village to Village Trading");
                    goto Label_51DF;

                case 0x328:
                    text = SK.Text("TOOLTIPS_REMOVE_FAVOURITE", "Click here to remove this Stock Exchange from your Favourites");
                    goto Label_51DF;

                case 0x329:
                    text = SK.Text("TOOLTIPS_ADD_FAVOURITE", "Click here to add this Stock Exchange to your Favourites");
                    goto Label_51DF;

                case 810:
                    text = SK.Text("TOOLTIPS_REMOVE_RECENT", "Click here to remove this Stock Exchange from your recent Stock Exchanges");
                    goto Label_51DF;

                case 0x32b:
                    text = SK.Text("TOOLTIPS_REMOVE_FAVOURITE_MARKET", "Click here to remove this Village from your Favourites");
                    goto Label_51DF;

                case 0x32c:
                    text = SK.Text("TOOLTIPS_ADD_FAVOURITE_MARKET", "Click here to add this Village to your Favourites");
                    goto Label_51DF;

                case 0x32d:
                    text = SK.Text("TOOLTIPS_REMOVE_RECENT_MARKET", "Click here to remove this Village from your Recent List");
                    goto Label_51DF;

                case 0x32e:
                    text = SK.Text("TOOLTIPS_FIND_HIGHEST_PRICE", "Find the highest price for this item in the 20 closest Stock Exchanges. (Premium Token in play Required)");
                    goto Label_51DF;

                case 0x32f:
                    text = SK.Text("TOOLTIPS_FIND_LOWEST_PRICE", "Find the lowest price for this item in the 20 closest Stock Exchanges. (Premium Token in play Required)");
                    goto Label_51DF;

                case 900:
                    text = SK.Text("TOOLTIPS_RESOURCES_CLOSE", "Click to Close");
                    goto Label_51DF;

                case 0x385:
                    text = SK.Text("TOOLTIPS_RESOURCES_PRODUCTION_INFO", "Click for Production Information");
                    goto Label_51DF;

                case 0x3e8:
                    text = SK.Text("TOOLTIPS_BANQUET_CLOSE", "Click to Close");
                    goto Label_51DF;

                case 0x44c:
                    text = SK.Text("TOOLTIPS_AREASELECT_OVER_TAG", "Click to Select this county as your starting area");
                    goto Label_51DF;

                case 0x44d:
                    text = SK.Text("TOOLTIPS_AREASELECT_OVER_TAG_FULL", "This County is Full");
                    goto Label_51DF;

                case 0x4b0:
                    text = SK.Text("TOOLTIPS_MENUBAR_CONVERT", "This allows you to change your village type to another, all buildings are lost but all resources, units and your castle are kept.");
                    goto Label_51DF;

                case 0x4b1:
                    text = SK.Text("TOOLTIPS_MENUBAR_ABANDON", "This allows you to disown the selected village, allowing you to make a new village elsewhere, all buildings, resources, units and your castle are lost.");
                    goto Label_51DF;

                case 0x514:
                    text = StatsPanel.getCategoryTitle(CustomTooltipManager.currentOverData) + " : " + StatsPanel.getCategoryDescription(CustomTooltipManager.currentOverData);
                    goto Label_51DF;

                case 0x578:
                    text = SK.Text("TOOLTIPS_LOGOUT_CLOSE", "Close logout window without logging out");
                    goto Label_51DF;

                case 0x579:
                    text = SK.Text("TOOLTIPS_LOGOUT_AUTO_TRADE", "Toggle Auto-Trade On/Off");
                    goto Label_51DF;

                case 0x57a:
                    text = SK.Text("TOOLTIPS_LOGOUT_AUTO_SCOUT", "Toggle Auto-Scouting On/Off");
                    goto Label_51DF;

                case 0x57b:
                    text = SK.Text("TOOLTIPS_LOGOUT_AUTO_ATTACK", "Toggle Auto-Attack On/Off");
                    goto Label_51DF;

                case 0x57c:
                    text = SK.Text("TOOLTIPS_LOGOUT_AUTO_RECRUIT", "Toggle Auto-Recruit On/Off");
                    goto Label_51DF;

                case 0x57d:
                    text = SK.Text("TOOLTIPS_LOGOUT_AUTO_REBUILD", "Toggle Auto-Rebuild On/Off");
                    goto Label_51DF;

                case 0x57e:
                    text = SK.Text("TOOLTIPS_LOGOUT_AUTO_TRANSFER", "Toggle Auto-Transfer On/Off");
                    goto Label_51DF;

                case 0x57f:
                    text = SK.Text("TOOLTIPS_LOGOUT_SELECT_TRADE_RESOURCE", "Select Auto-Trade Resource") + ". " + SK.Text("TOOLTIPS_LOGOUT_SELECT_TRADE_RESOURCE_currently", "Currently") + " : " + VillageBuildingsData.getResourceNames(CustomTooltipManager.currentOverData);
                    goto Label_51DF;

                case 0x580:
                    text = SK.Text("TOOLTIPS_LOGOUT_SELECT_TRADE_PERCENT", "Adjust % at which Auto-Trade starts trading ");
                    goto Label_51DF;

                case 0x581:
                    text = SK.Text("TOOLTIPS_LOGOUT_ATTACK_BANDITS", "Toggle Auto-Attack Bandit Camps");
                    goto Label_51DF;

                case 0x582:
                    text = SK.Text("TOOLTIPS_LOGOUT_ATTACK_WOLVES", "Toggle Auto-Attack Wolf Lairs");
                    goto Label_51DF;

                case 0x583:
                    text = SK.Text("TOOLTIPS_LOGOUT_ATTACK_AI", "Toggle Auto-Attack AI Castles");
                    goto Label_51DF;

                case 0x584:
                    text = SK.Text("TOOLTIPS_LOGOUT_RECRUIT_PEASANT", "Toggle Auto-Recruit Peasants");
                    goto Label_51DF;

                case 0x585:
                    text = SK.Text("TOOLTIPS_LOGOUT_RECRUIT_ARCHER", "Toggle Auto-Recruit Archers");
                    goto Label_51DF;

                case 0x586:
                    text = SK.Text("TOOLTIPS_LOGOUT_RECRUIT_PIKEMAN", "Toggle Auto-Recruit Pikemen");
                    goto Label_51DF;

                case 0x587:
                    text = SK.Text("TOOLTIPS_LOGOUT_RECRUIT_SWORDSMAN", "Toggle Auto-Recruit Swordsmen");
                    goto Label_51DF;

                case 0x588:
                    text = SK.Text("TOOLTIPS_LOGOUT_RECRUIT_CATAPULT", "Toggle Auto-Recruit Catapults");
                    goto Label_51DF;

                case 0x589:
                    text = SK.Text("TOOLTIPS_LOGOUT_RESOURCES", "Select Auto-Trade Resource : " + VillageBuildingsData.getResourceNames(CustomTooltipManager.currentOverData));
                    goto Label_51DF;

                case 0x58a:
                    text = SK.Text("TOOLTIPS_LOGOUT_EXIT", "Exit Stronghold Kingdoms");
                    goto Label_51DF;

                case 0x58b:
                    text = SK.Text("TOOLTIPS_LOGOUT_CANCEL", "Close logout window without logging out");
                    goto Label_51DF;

                case 0x58c:
                    text = SK.Text("TOOLTIPS_LOGOUT_SWAP_WORLDS", "Log out of this Game World and return to the World Selection Screen");
                    goto Label_51DF;

                case 0x58d:
                {
                    int premiumCard = GameEngine.Instance.World.UserCardData.premiumCard;
                    double num13 = GameEngine.Instance.World.UserCardData.premiumCardExpiry.Subtract(VillageMap.getCurrentServerTime()).TotalSeconds;
                    double num14 = num13 / 86400.0;
                    double num15 = (num13 % 86400.0) / 3600.0;
                    double num16 = (num13 % 3600.0) / 60.0;
                    text = SK.Text("TOOLTIPS_LOGOUT_PREMIUM", "Your Premium Token expires in : ") + Math.Floor(num14).ToString().PadLeft(2, '0') + ":" + Math.Floor(num15).ToString().PadLeft(2, '0') + ":" + Math.Floor(num16).ToString().PadLeft(2, '0') + " (dd:hh:mm)";
                    goto Label_51DF;
                }
                case 0x5dc:
                    text = SK.Text("TOOLTIPS_REPORTS_FILTER", "Change Report Filtering");
                    goto Label_51DF;

                case 0x5dd:
                    text = SK.Text("TOOLTIPS_REPORTS_CAPTURE", "Change Report Capturing");
                    goto Label_51DF;

                case 0x5de:
                    text = SK.Text("TOOLTIPS_REPORTS_DELETE", "Report Marking and Deleting options");
                    goto Label_51DF;

                case 0x640:
                    text = SK.Text("TOOLTIPS_TUTORIAL_REOPEN", "Show Adviser");
                    goto Label_51DF;

                case 0x641:
                    text = SK.Text("Options_Player_Guide", "Player Guide");
                    goto Label_51DF;

                case 0x6a4:
                case 0x6a5:
                case 0x6a6:
                case 0x6a7:
                case 0x6a8:
                case 0x6a9:
                case 0x6aa:
                case 0x6ab:
                case 0x6ac:
                case 0x6ad:
                case 0x6ae:
                case 0x6af:
                case 0x6b0:
                case 0x6b1:
                case 0x6b2:
                case 0x6b3:
                case 0x6b4:
                case 0x6b5:
                case 0x6b6:
                case 0x6b7:
                {
                    int num17 = (ID - 0x6a4) + 1;
                    NumberFormatInfo provider = GameEngine.NFI;
                    text = (SK.Text("TOOLTIPS_GLORY_HOUSE", "House") + " " + num17.ToString() + " - ") + SK.Text("TOOLTIPS_GLORY_POINTS", "Glory") + " " + CustomTooltipManager.currentOverData.ToString("N", provider);
                    goto Label_51DF;
                }
                case 0x708:
                    text = SK.Text("TOOLTIPS_NEW_VILLAGE_ENTER", "Choose a starting location for me");
                    goto Label_51DF;

                case 0x709:
                    text = SK.Text("TOOLTIPS_NEW_VILLAGE_ADVANCED", "Try and join the game in a particular part of the kingdom");
                    goto Label_51DF;

                case 0x70a:
                    text = SK.Text("TOOLTIPS_NEW_VILLAGE_LOGOUT", "Log out");
                    goto Label_51DF;

                case 0x7d0:
                    text = SK.Text("SendMonksPanel_Influence", "Influence");
                    goto Label_51DF;

                case 0x7d1:
                    text = SK.Text("VillageMapPanel_Blessing", "Blessing");
                    goto Label_51DF;

                case 0x7d2:
                    text = SK.Text("VillageMapPanel_Inquisition", "Inquisition");
                    goto Label_51DF;

                case 0x7d3:
                    text = SK.Text("SendMonksPanel_Interdiction", "Interdiction");
                    goto Label_51DF;

                case 0x7d4:
                    text = SK.Text("SendMonksPanel_Restoration", "Restoration");
                    goto Label_51DF;

                case 0x7d5:
                    text = SK.Text("SendMonksPanel_Absolution", "Absolution");
                    goto Label_51DF;

                case 0x7d6:
                    text = SK.Text("SendMonksPanel_Excommnunication", "Excommunication");
                    goto Label_51DF;

                case 0x7d7:
                    text = SK.Text("SendMonksPanel_Positive_Influence", "Positive Influence");
                    goto Label_51DF;

                case 0x7d8:
                    text = SK.Text("SendMonksPanel_Negative_Influence", "Negative Influence");
                    goto Label_51DF;

                case 0x7e2:
                    text = SK.Text("TOOLTIPS_FAVOURITE_ATTACK_TARGET_MAKE", "Mark this village as a Favourite");
                    goto Label_51DF;

                case 0x76c:
                    text = CapitalDonateResourcesPanel2.capitalTooltipText;
                    goto Label_51DF;

                case 0x834:
                    text = SK.Text("LaunchAttackPopup_Vandalise", "Vandalise");
                    goto Label_51DF;

                case 0x835:
                    text = SK.Text("GENERIC_Capture", "Capture");
                    goto Label_51DF;

                case 0x836:
                    text = SK.Text("GENERIC_Pillage", "Pillage");
                    goto Label_51DF;

                case 0x837:
                    text = SK.Text("GENERIC_Ransack", "Ransack");
                    goto Label_51DF;

                case 0x838:
                    text = SK.Text("GENERIC_Raze", "Raze");
                    goto Label_51DF;

                case 0x839:
                    text = SK.Text("GENERIC_Gold_Raid", "Gold Raid");
                    goto Label_51DF;

                case 0x83a:
                    text = SK.Text("GENERIC_Attack", "Attack");
                    goto Label_51DF;

                case 0x83b:
                    text = SK.Text("TOOLTIPS_FAVOURITE_ATTACK_TARGET_CLEAR", "Remove this village from your Favourites");
                    goto Label_51DF;

                case 0x9c5:
                    text = GameEngine.Instance.World.getFactionName(CustomTooltipManager.currentOverData);
                    goto Label_51DF;

                case 0x9c6:
                    text = SK.Text("MailScreen_Send_Mail", "Send Mail");
                    goto Label_51DF;

                case 0x9c7:
                    if (UserInfo == null)
                    {
                        text = "";
                    }
                    else
                    {
                        NumberFormatInfo info3 = GameEngine.NFI;
                        text = SK.Text("STATS_CATEGORY_TITLE_RANK", "Rank") + " : ";
                        if (UserInfo.avatarData == null)
                        {
                            text = text + Rankings.getRankingName(UserInfo.rank);
                        }
                        else
                        {
                            text = text + Rankings.getRankingName(UserInfo.rank, UserInfo.avatarData.male);
                        }
                        str8 = text + Environment.NewLine;
                        str8 = str8 + SK.Text("GENERIC_Villages", "Villages") + " : " + UserInfo.numVillages.ToString("N", info3) + Environment.NewLine;
                        text = str8 + SK.Text("UserInfoPanel_Points", "Points") + " : " + UserInfo.points.ToString("N", info3) + Environment.NewLine;
                        if (UserInfo.standing >= 0)
                        {
                            text = text + SK.Text("UserInfoPanel_Position", "Position") + " : " + UserInfo.standing.ToString("N", info3);
                        }
                    }
                    goto Label_51DF;

                case 0x9c8:
                    text = SK.Text("TOOLTIPS_MAPSIDE_BUY_CHARTER_RANK_TOO_LOW", "You can't own more villages at this time due your current Rank or your Leadership Research level.");
                    goto Label_51DF;

                case 0x9c9:
                    text = SK.Text("TOOLTIPS_MAPSIDE_CANT_BUY_FROM_HERE", "You cannot purchase other villages from your currently selected village.");
                    goto Label_51DF;

                case 0x9ca:
                    text = SK.Text("TOOLTIPS_MAPSIDE_BUY_CHARTER_RANK_TOO_LOW12", "You need to be rank 12 to own more villages.");
                    goto Label_51DF;

                case 0xa8c:
                {
                    text = "";
                    bool flag4 = false;
                    if ((CustomTooltipManager.currentOverData & 1) != 0)
                    {
                        if (flag4)
                        {
                            text = text + Environment.NewLine;
                        }
                        text = text + SK.Text("TOOLTIPS_UNIT_MAKE_ERROR_PEASANTS", "- No Peasants Available");
                        flag4 = true;
                    }
                    if ((CustomTooltipManager.currentOverData & 2) != 0)
                    {
                        if (flag4)
                        {
                            text = text + Environment.NewLine;
                        }
                        text = text + SK.Text("TOOLTIPS_UNIT_MAKE_ERROR_GOLD", "- Not enough Gold");
                        flag4 = true;
                    }
                    if ((CustomTooltipManager.currentOverData & 0x20) != 0)
                    {
                        if (flag4)
                        {
                            text = text + Environment.NewLine;
                        }
                        text = text + SK.Text("TOOLTIPS_UNIT_MAKE_ERROR_TROOP_SPACE", "- Not enough Army Space");
                        flag4 = true;
                    }
                    else if ((CustomTooltipManager.currentOverData & 4) != 0)
                    {
                        if (flag4)
                        {
                            text = text + Environment.NewLine;
                        }
                        text = text + SK.Text("TOOLTIPS_UNIT_MAKE_ERROR_SPACE", "- Not enough Unit Space");
                        flag4 = true;
                    }
                    if ((CustomTooltipManager.currentOverData & 8) != 0)
                    {
                        if (flag4)
                        {
                            text = text + Environment.NewLine;
                        }
                        text = text + SK.Text("TOOLTIPS_UNIT_MAKE_ERROR_FULL", "- You have made the Maximum amount");
                        flag4 = true;
                    }
                    if ((CustomTooltipManager.currentOverData & 0x10) != 0)
                    {
                        if (flag4)
                        {
                            text = text + Environment.NewLine;
                        }
                        text = text + SK.Text("TOOLTIPS_UNIT_MAKE_ERROR_WEAPON", "- No Weapons Available");
                        flag4 = true;
                    }
                    goto Label_51DF;
                }
                case 0xaf0:
                    text = SK.Text("TOOLTIPS_VASSAL_AVAILABLE_TROOPS", "Available Troops");
                    goto Label_51DF;

                case 0x8fc:
                    text = SK.Text("TOOLTIPS_FACTIONTAB_GLORY", "Click to View the Glory Screen");
                    goto Label_51DF;

                case 0x8fd:
                    text = SK.Text("TOOLTIPS_FACTIONTAB_FACTIONS", "Click to View the Faction Screens");
                    goto Label_51DF;

                case 0x8fe:
                    text = SK.Text("TOOLTIPS_FACTIONTAB_HOUSE", "Click to View the House Screens");
                    goto Label_51DF;

                case 0x8ff:
                    text = SK.Text("GENERIC_Ally", "Ally");
                    goto Label_51DF;

                case 0x900:
                    text = SK.Text("GENERIC_Enemy", "Enemy");
                    goto Label_51DF;

                case 0x901:
                    text = SK.Text("TOOLTIPS_FACTION_LEADER", "Faction Leader");
                    goto Label_51DF;

                case 0x902:
                    text = SK.Text("TOOLTIPS_FACTION_OFFICER", "Faction Officer");
                    goto Label_51DF;

                case 0x903:
                    text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + CustomTooltipManager.currentOverData.ToString();
                    goto Label_51DF;

                case 0x904:
                {
                    HouseData data2 = null;
                    try
                    {
                        data2 = GameEngine.Instance.World.HouseInfo[CustomTooltipManager.currentOverData];
                    }
                    catch (Exception)
                    {
                    }
                    int num18 = GameEngine.Instance.World.getGloryRank(CustomTooltipManager.currentOverData);
                    text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + CustomTooltipManager.currentOverData.ToString() + Environment.NewLine + "\"" + getHouseMotto(CustomTooltipManager.currentOverData) + "\"" + Environment.NewLine + Environment.NewLine;
                    if (num18 >= 0)
                    {
                        NumberFormatInfo info2 = GameEngine.NFI;
                        str8 = text;
                        string[] strArray = new string[] { str8, SK.Text("FactionInvites_Glory_Rank", "Glory Rank"), " : ", (num18 + 1).ToString(), Environment.NewLine };
                        text = string.Concat(strArray) + SK.Text("FactionInvites_Glory_Points", "Glory Points") + " : " + GameEngine.Instance.World.getGloryPoints(CustomTooltipManager.currentOverData).ToString("N", info2);
                        if ((data2 != null) && (data2.numVictories > 0))
                        {
                            str8 = text;
                            text = str8 + Environment.NewLine + Environment.NewLine + SK.Text("FactionInvites_Glory Victories", "Glory Victories") + " : " + data2.numVictories.ToString();
                        }
                    }
                    else
                    {
                        text = text + SK.Text("FactionInvites_Glory_Eliminatated", "Eliminated From Glory Race");
                    }
                    goto Label_51DF;
                }
                case 0x92e:
                    text = SK.Text("FACTION_SIDEBAR_SHOW_ALL", "A List of All Factions in the Game World");
                    goto Label_51DF;

                case 0x92f:
                    text = SK.Text("FACTION_SIDEBAR_MY_FACTION", "Details of Your Faction");
                    goto Label_51DF;

                case 0x930:
                    text = SK.Text("FACTION_SIDEBAR_DIPLOMACY", "Your Faction's Relationship to Other Factions");
                    goto Label_51DF;

                case 0x931:
                    text = SK.Text("FACTION_SIDEBAR_OFFICERS", "Manage Your Faction and View the List of Officers");
                    goto Label_51DF;

                case 0x932:
                    text = SK.Text("FACTION_SIDEBAR_FORUM", "Your Faction's Forum");
                    goto Label_51DF;

                case 0x933:
                    text = SK.Text("FACTION_SIDEBAR_MAIL", "Send a Mail to Everyone in Your Faction");
                    goto Label_51DF;

                case 0x934:
                    text = SK.Text("FACTION_SIDEBAR_INVITES", "Details of Your Faction Invites and Applications");
                    goto Label_51DF;

                case 0x935:
                    text = SK.Text("FACTION_SIDEBAR_CHAT", "Your Faction's Chat Channel");
                    goto Label_51DF;

                case 0x936:
                    text = SK.Text("FACTION_SIDEBAR_START", "Start a New Faction");
                    goto Label_51DF;

                case 0x937:
                    text = SK.Text("FACTION_SIDEBAR_LEAVE", "Leave your current Faction");
                    goto Label_51DF;

                case 0x960:
                    text = SK.Text("GENERIC_Cancel", "Cancel");
                    goto Label_51DF;

                case 0x961:
                    text = SK.Text("GENERIC_Select_Target", "Select Target");
                    goto Label_51DF;

                case 0x962:
                    text = SK.Text("GENERIC_Select_Target", "Select Target");
                    goto Label_51DF;

                case 0x963:
                    text = SK.Text("TradeWithPanel_Trade_With", "Trade With");
                    goto Label_51DF;

                case 0x964:
                    text = SK.Text("StockExchangeSidePanel_Select_Exchange", "Select Exchange");
                    goto Label_51DF;

                case 0x965:
                    text = SK.Text("MonkTargetSidePanel_Select_Target", "Select Target");
                    goto Label_51DF;

                case 0x966:
                    text = SK.Text("MonkTargetSidePanel_Select_Target", "Select Target");
                    goto Label_51DF;

                case 0x967:
                    text = SK.Text("VassalSelectSidePanel_Select_Vassal", "Select Vassal");
                    goto Label_51DF;

                case 0x96a:
                    text = SK.Text("TradeWithPanel_Trade_With", "Trade With");
                    goto Label_51DF;

                case 0x96b:
                    text = SK.Text("GENERIC_Attack", "Attack");
                    goto Label_51DF;

                case 0x96c:
                    text = SK.Text("GENERIC_Scout_Out_Village", "Scout Out Village");
                    goto Label_51DF;

                case 0x96d:
                    text = SK.Text("GENERIC_Send_Troops", "Send Troops");
                    goto Label_51DF;

                case 0x96e:
                    text = SK.Text("GENERIC_Send_Monks", "Send Monks");
                    goto Label_51DF;

                case 0x974:
                    text = SK.Text("GENERIC_Parish", "Parish");
                    goto Label_51DF;

                case 0x975:
                    text = SK.Text("GENERIC_County", "County");
                    goto Label_51DF;

                case 0x976:
                    text = SK.Text("GENERIC_Province", "Province");
                    goto Label_51DF;

                case 0x977:
                    text = SK.Text("GENERIC_Country", "Country");
                    goto Label_51DF;

                case 0x978:
                    text = SK.Text("GENERIC_Bandit_Camp", "Bandit Camp");
                    goto Label_51DF;

                case 0x979:
                    text = SK.Text("GENERIC_Wolf_Camp", "Wolf Lair");
                    goto Label_51DF;

                case 0x97a:
                    text = SK.Text("GENERIC_Rats_Castle", "Rat's Castle");
                    goto Label_51DF;

                case 0x97b:
                    text = SK.Text("GENERIC_Snakes_Castle", "Snake's Castle");
                    goto Label_51DF;

                case 0x97c:
                    text = SK.Text("GENERIC_Pigs_Castle", "Pig's Castle");
                    goto Label_51DF;

                case 0x97d:
                    text = SK.Text("GENERIC_Wolfs_Castle", "Wolf's Castle");
                    goto Label_51DF;

                case 0x97e:
                    text = SpecialVillageTypes.getName(CustomTooltipManager.currentOverData, "");
                    goto Label_51DF;

                case 0x97f:
                    text = SK.Text("OwnVillagePanel_Send_Out_Resources", "Send Out Resources");
                    goto Label_51DF;

                case 0x980:
                    text = SK.Text("OwnVillagePanel_Send_Out_Troops", "Send Out Troops");
                    goto Label_51DF;

                case 0x981:
                    text = SK.Text("OwnVillagePanel_Send_Out_Scouts", "Send Out Scouts");
                    goto Label_51DF;

                case 0x982:
                    text = SK.Text("OwnVillagePanel_Send_Out_Reinforcements", "Send Out Reinforcements");
                    goto Label_51DF;

                case 0x983:
                    text = SK.Text("OwnVillagePanel_Send_Out_Monks", "Send Out Monks");
                    goto Label_51DF;

                case 0x984:
                    switch (CustomTooltipManager.currentOverData)
                    {
                        case 0:
                            text = SK.Text("MapTypes_Lowland", "Lowland");
                            break;

                        case 1:
                            text = SK.Text("MapTypes_Highland", "Highland");
                            break;

                        case 2:
                            text = SK.Text("MapTypes_River", "River") + " 1";
                            break;

                        case 3:
                            text = SK.Text("MapTypes_River", "River") + " 2";
                            break;

                        case 4:
                            text = SK.Text("MapTypes_Mountain_Peak", "Mountain Peak");
                            break;

                        case 5:
                            text = SK.Text("MapTypes_Salt_Flat", "Salt Flat");
                            break;

                        case 6:
                            text = SK.Text("MapTypes_Marsh", "Marsh");
                            break;

                        case 7:
                            text = SK.Text("MapTypes_Plains", "Plains");
                            break;

                        case 8:
                            text = SK.Text("MapTypes_Valley_Side", "Valley Side");
                            break;

                        case 9:
                            text = SK.Text("MapTypes_Forest", "Forest");
                            break;
                    }
                    goto Label_51DF;

                case 0x985:
                    text = SK.Text("TOOLTIPS_View_Village", "View Village");
                    goto Label_51DF;

                case 0x986:
                    text = SK.Text("TOOLTIPS_View_Castle", "View Castle");
                    goto Label_51DF;

                case 0x987:
                    text = SK.Text("TOOLTIPS_View_Resources", "View Resources");
                    goto Label_51DF;

                case 0x988:
                    text = SK.Text("TOOLTIPS_Make_Troops", "Make Troops");
                    goto Label_51DF;

                case 0x989:
                    text = SK.Text("CapitalTradePanel_", "Purchase Goods");
                    goto Label_51DF;

                case 0x98a:
                    text = SK.Text("CapitalBarracksPanel_Mercenaries", "Mercenaries");
                    goto Label_51DF;

                case 0x98b:
                    text = SK.Text("TOOLTIPS_Scout_Stash", "Scout Stash");
                    goto Label_51DF;

                case 0x98c:
                    text = SK.Text("EmptyVillagePanel_Available_Village", "New Village Charter");
                    goto Label_51DF;

                case 0x98d:
                    text = SK.Text("TOOLTIPS_View_Castle_Report", "View most recent report of this Castle");
                    goto Label_51DF;

                case 0x98e:
                    text = SK.Text("OtherVillagePanel_Make_Vassal", "Make Vassal");
                    goto Label_51DF;

                case 0x98f:
                    text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
                    goto Label_51DF;

                case 0x990:
                    text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
                    goto Label_51DF;

                case 0x991:
                    text = SpecialVillageTypes.getName(CustomTooltipManager.currentOverData, "");
                    goto Label_51DF;

                case 0x992:
                    text = SK.Text("GENERIC_Parish_Plague", "Disease Infested Parish") + " : " + InterfaceMgr.Instance.getPlagueText(CustomTooltipManager.currentOverData) + "  (" + CustomTooltipManager.currentOverData.ToString() + ")";
                    goto Label_51DF;

                case 0x993:
                    text = SK.Text("VassalVillagePanel_Manage_Vassal_Troops", "Manage Vassal's Troops");
                    goto Label_51DF;

                case 0x994:
                    text = SK.Text("VassalVillagePanel_Manage_Vassal", "Manage Vassal");
                    goto Label_51DF;

                case 0x995:
                    text = SK.Text("VassalVillagePanel_Attack_From_Here", "Attack From Here");
                    goto Label_51DF;

                case 0x996:
                    text = SK.Text("TOOLTIPS_Filter_Traders", "Show only Traders");
                    goto Label_51DF;

                case 0x997:
                    text = SK.Text("TOOLTIPS_Filter_Attacks", "Show only Attacking Troops");
                    goto Label_51DF;

                case 0x998:
                    text = SK.Text("TOOLTIPS_Filter_Foraging", "Show only Foraging Scouts");
                    goto Label_51DF;

                case 0x999:
                    text = SK.Text("TOOLTIPS_Filter_House", "Show only villages in your House");
                    goto Label_51DF;

                case 0x99a:
                    text = SK.Text("TOOLTIPS_Filter_Faction", "Show only villages in your Faction");
                    goto Label_51DF;

                case 0x99b:
                    text = SK.Text("TOOLTIPS_Clear_Filter", "Clear Filter");
                    goto Label_51DF;

                case 0x99c:
                    text = SK.Text("TOOLTIPS_village_search", "Search For Villages");
                    goto Label_51DF;

                case 0x99d:
                    text = SK.Text("TOOLTIPS_Filter_Faction_Open", "Show only villages in Factions accepting Invitations");
                    goto Label_51DF;

                case 0x99e:
                    text = SK.Text("TOOLTIPS_Filter_AI", "Show only attackable Wolf Camps, Bandit Camps and AI Castles");
                    goto Label_51DF;

                case 0xb54:
                    text = SK.Text("TOOLTIPS_ARMIES_ATTACKS", "View All Attacks");
                    goto Label_51DF;

                case 0xb55:
                    text = SK.Text("TOOLTIPS_ARMIES_SCOUTS", "View All Scouts");
                    goto Label_51DF;

                case 0xb56:
                    text = SK.Text("TOOLTIPS_ARMIES_REINFORCEMENTS", "View All Reinforcements");
                    goto Label_51DF;

                case 0xb57:
                    text = SK.Text("TOOLTIPS_ARMIES_MERCHANTS", "View All Merchants");
                    goto Label_51DF;

                case 0xb58:
                    text = SK.Text("TOOLTIPS_ARMIES_MONKS", "View All Monks");
                    goto Label_51DF;

                case 0xb59:
                    switch (CustomTooltipManager.currentOverData)
                    {
                        case 1:
                            text = SK.Text("TOOLTIPS_ARMIES_SORTING_FROM", "Sort By Target Village");
                            break;

                        case 2:
                            text = SK.Text("TOOLTIPS_ARMIES_SORTING_HOME", "Sort By Source Village");
                            break;

                        case 3:
                            text = SK.Text("TOOLTIPS_ARMIES_SORTING_PEASANTS", "Sort By Number of Peasants");
                            break;

                        case 4:
                            text = SK.Text("TOOLTIPS_ARMIES_SORTING_ARCHERS", "Sort By Number of Archers");
                            break;

                        case 5:
                            text = SK.Text("TOOLTIPS_ARMIES_SORTING_PIKEMEN", "Sort By Number of Pikemen");
                            break;

                        case 6:
                            text = SK.Text("TOOLTIPS_ARMIES_SORTING_SWORDSMEN", "Sort By Number of Swordsmen");
                            break;

                        case 7:
                            text = SK.Text("TOOLTIPS_ARMIES_SORTING_CATAPULTS", "Sort By Number of Catapults");
                            break;

                        case 8:
                            text = SK.Text("TOOLTIPS_ARMIES_SORTING_TIME", "Sort By Arrival Time");
                            break;

                        case 9:
                            text = SK.Text("TOOLTIPS_ARMIES_SORTING_SCOUTS", "Sort By Scouts");
                            break;

                        case 10:
                            text = SK.Text("TOOLTIPS_ARMIES_SORTING_TRADE_AMOUNT", "Sort By Trade Size");
                            break;

                        case 11:
                            text = SK.Text("TOOLTIPS_ARMIES_SORTING_TRADE_COMMAND", "Sort By Trade Status");
                            break;

                        case 12:
                            text = SK.Text("TOOLTIPS_ARMIES_SORTING_MONKS", "Sort By Number of Monks");
                            break;

                        case 13:
                            text = SK.Text("TOOLTIPS_ARMIES_SORTING_MONKS_COMMAND", "Sort By Monk Command");
                            break;

                        case 20:
                            text = SK.Text("TOOLTIPS_ARMIES_SORTING_CAPTAINS", "Sort By Number of Captains");
                            break;
                    }
                    goto Label_51DF;

                case 0xbb9:
                case 0xbba:
                case 0xbbb:
                case 0xbbc:
                    num19 = CustomTooltipManager.currentOverData & 0xfff;
                    num20 = 0;
                    str4 = getAchievementRank(CustomTooltipManager.currentOverData);
                    num22 = CustomTooltipManager.currentOverData & 0x70000000;
                    if (num22 > 0x40000000)
                    {
                        switch (num22)
                        {
                            case 0x50000000:
                                num20 = 4;
                                goto Label_3CF0;

                            case 0x60000000:
                                num20 = 5;
                                goto Label_3CF0;

                            case 0x70000000:
                                num20 = 6;
                                goto Label_3CF0;
                        }
                    }
                    else
                    {
                        switch (num22)
                        {
                            case 0x10000000:
                                num20 = 1;
                                goto Label_3CF0;

                            case 0x20000000:
                                num20 = 2;
                                goto Label_3CF0;

                            case 0x40000000:
                                num20 = 3;
                                goto Label_3CF0;
                        }
                    }
                    num20 = 0;
                    goto Label_3CF0;

                case 0xc1d:
                    text = "Admin Functions";
                    goto Label_51DF;

                case 0xc1e:
                    text = SK.Text("TOOLTIPS_USER_CLEAR_DIPLOMACY", "Remove Diplomatic Status for this Player");
                    goto Label_51DF;

                case 0xc1f:
                    text = SK.Text("TOOLTIPS_USER_CLEAR_DIPLOMACY_NOTES", "Edit Notes for this Player");
                    goto Label_51DF;

                case 0xc81:
                    text = SK.Text("TOOLTIPS_START_QUEST", "Start this quest");
                    goto Label_51DF;

                case 0xc82:
                    text = SK.Text("TOOLTIPS_ABANDON_QUEST", "Abandon this quest and remove it from the list.");
                    goto Label_51DF;

                case 0xc83:
                    text = SK.Text("TOOLTIPS_QUEST_REWARD_HONOUR", "Honour");
                    goto Label_51DF;

                case 0xc84:
                    text = SK.Text("TOOLTIPS_QUEST_REWARD_GOLD", "Gold");
                    goto Label_51DF;

                case 0xc85:
                    text = SK.Text("TOOLTIPS_QUEST_REWARD_WOOD", "Wood");
                    goto Label_51DF;

                case 0xc86:
                    text = SK.Text("TOOLTIPS_QUEST_REWARD_STONE", "Stone");
                    goto Label_51DF;

                case 0xc87:
                    text = SK.Text("TOOLTIPS_QUEST_REWARD_APPLES", "Apples");
                    goto Label_51DF;

                case 0xc88:
                    text = SK.Text("TOOLTIPS_QUEST_REWARD_CARD_PACKS", "Card Packs");
                    goto Label_51DF;

                case 0xc89:
                    text = SK.Text("TOOLTIPS_QUEST_REWARD_PREMIUM_CARD", "2 Day Premium Token");
                    goto Label_51DF;

                case 0xc8a:
                    text = SK.Text("TOOLTIPS_QUEST_REWARD_FAITHPOINTS", "Faith Points");
                    goto Label_51DF;

                case 0xc8b:
                    text = SK.Text("TOOLTIPS_QUEST_REWARD_GLORY", "Glory");
                    goto Label_51DF;

                case 0xc8c:
                    text = SK.Text("TOOLTIPS_QUEST_REWARD_SHIELD_CHARGES", "A new Charge for your Shield");
                    goto Label_51DF;

                case 0xc8d:
                    text = SK.Text("TOOLTIPS_QUEST_REWARD_TICKETS", "Quest Wheel Spins");
                    goto Label_51DF;

                case 0xc8e:
                    text = SK.Text("ResourceType_Fish", "Fish");
                    goto Label_51DF;

                case 0xfa1:
                    text = SK.Text("TOOLTIPS_LOGIN_ENGLISH_SUPPORT", "Customer Support for this world is in English");
                    goto Label_51DF;

                case 0xfa2:
                    text = SK.Text("TOOLTIPS_LOGIN_GERMAN_SUPPORT", "Customer Support for this world is in German");
                    goto Label_51DF;

                case 0xfa3:
                    text = SK.Text("TOOLTIPS_LOGIN_FRENCH_SUPPORT", "Customer Support for this world is in French");
                    goto Label_51DF;

                case 0xfa4:
                    text = SK.Text("TOOLTIPS_LOGIN_RUSSIAN_SUPPORT", "Customer Support for this world is in Russian");
                    goto Label_51DF;

                case 0xfa5:
                    text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_ENGLAND", "The map of this world is centred around Great Britain");
                    goto Label_51DF;

                case 0xfa6:
                    text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_GERMANY", "The map of this world is centred around Germany");
                    goto Label_51DF;

                case 0xfa7:
                    text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_FRANCE", "The map of this world is centred around France");
                    goto Label_51DF;

                case 0xfa8:
                    text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_RUSSIA", "The map of this world is centred around Russia");
                    goto Label_51DF;

                case 0xfa9:
                    text = SK.Text("TOOLTIPS_LOGIN_OFFLINE", "This world is currently Offline, most likely due to server maintenance and should be back Online soon.");
                    goto Label_51DF;

                case 0xfaa:
                    text = SK.Text("TOOLTIPS_LOGIN_ONLINE", "This world is currently Online");
                    goto Label_51DF;

                case 0xfab:
                    text = SK.Text("TOOLTIPS_LOGIN_ENGLISH_FLAG", "View available worlds with English Support");
                    goto Label_51DF;

                case 0xfac:
                    text = SK.Text("TOOLTIPS_LOGIN_GERMAN_FLAG", "View available worlds with German Support");
                    goto Label_51DF;

                case 0xfad:
                    text = SK.Text("TOOLTIPS_LOGIN_FRENCH_FLAG", "View available worlds with French Support");
                    goto Label_51DF;

                case 0xfae:
                    text = SK.Text("TOOLTIPS_LOGIN_RUSSIAN_FLAG", "View available worlds with Russian Support");
                    goto Label_51DF;

                case 0xfaf:
                    text = SK.Text("TOOLTIPS_LOGIN_EDIT_SHIELD", "Click to Edit Your Coat of Arms");
                    goto Label_51DF;

                case 0xfb0:
                    text = SK.Text("TOOLTIPS_LOGIN_SPANISH_SUPPORT", "Customer Support for this world is in Spanish");
                    goto Label_51DF;

                case 0xfb1:
                    text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_SPAIN", "The map of this world is centred around Spain");
                    goto Label_51DF;

                case 0xfb2:
                    text = SK.Text("TOOLTIPS_LOGIN_SPANISH_FLAG", "View available worlds with Spanish Support");
                    goto Label_51DF;

                case 0xfb3:
                    text = SK.Text("TOOLTIPS_LOGIN_SECOND_AGE", "This World is in its Second Age.");
                    goto Label_51DF;

                case 0xfb4:
                    text = SK.Text("TOOLTIPS_LOGIN_POLISH_SUPPORT", "Customer Support for this world is in Polish");
                    goto Label_51DF;

                case 0xfb5:
                    text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_POLAND", "The map of this world is Poland");
                    goto Label_51DF;

                case 0xfb6:
                    text = SK.Text("LOGIN_POLISH_FLAG", "View available worlds with Polish Support");
                    goto Label_51DF;

                case 0xfb7:
                    text = SK.Text("TOOLTIPS_LOGIN_TURKISH_SUPPORT", "Customer Support for this world is in Turkish");
                    goto Label_51DF;

                case 0xfb8:
                    text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_TURKEY", "The map of this world is Turkey");
                    goto Label_51DF;

                case 0xfb9:
                    text = SK.Text("TOOLTIP_LOGIN_TURKISH_FLAG", "View available worlds with Turkish Support");
                    goto Label_51DF;

                case 0xfba:
                    text = SK.Text("TOOLTIPS_LOGIN_THIRD_AGE", "This World is in its Third Age.");
                    goto Label_51DF;

                case 0xfbb:
                    text = SK.Text("TOOLTIPS_LOGIN_ITALIAN_SUPPORT", "Customer Support for this world is in Italian");
                    goto Label_51DF;

                case 0xfbc:
                    text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_ITALY", "The map of this world is centred around Italy and the Adriatic");
                    goto Label_51DF;

                case 0xfbd:
                    text = SK.Text("TOOLTIP_LOGIN_ITALIAN_FLAG", "View available worlds with Italian Support");
                    goto Label_51DF;

                case 0xfbe:
                    text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_USA", "The map of this world is the USA");
                    goto Label_51DF;

                case 0xfbf:
                    text = SK.Text("TOOLTIPS_LOGIN_EUROPE_SUPPORT", "Customer Support for this world is available in all currently supported languages");
                    goto Label_51DF;

                case 0xfc0:
                    text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_EUROPE", "The map of this world is Europe");
                    goto Label_51DF;

                case 0xfc1:
                    text = SK.Text("TOOLTIP_LOGIN_EUROPEAN_FLAG", "View available worlds with the European Map");
                    goto Label_51DF;

                case 0xfc2:
                    text = SK.Text("TOOLTIPS_LOGIN_FOURTH_AGE", "This World is in its Fourth Age.");
                    goto Label_51DF;

                case 0xfc3:
                    text = SK.Text("TOOLTIPS_LOGIN_PORTUGUESE_SUPPORT", "Customer Support for this world is in Brazilian Portuguese and Spanish");
                    goto Label_51DF;

                case 0xfc4:
                    text = SK.Text("TOOLTIPS_LOGIN_MAP_OF_SOUTH_AMERICA", "The map of this world is Central and South America");
                    goto Label_51DF;

                case 0xfc5:
                    text = SK.Text("TOOLTIP_LOGIN_PORTUGUESE_FLAG", "View available worlds with Brazilian-Portuguese Support");
                    goto Label_51DF;

                case 0xfc6:
                    text = SK.Text("TOOLTIPS_LOGIN_FIRST_AGE", "This World is in its First Age. It is recommended that new players join a world that in its First Age.");
                    goto Label_51DF;

                case 0xfc7:
                    text = SK.Text("TOOLTIPS_LOGIN_FIFTH_AGE", "This World is in its Fifth Age.");
                    goto Label_51DF;

                case 0x1004:
                    text = SK.Text("BARRACKS_Troops", "Troops");
                    goto Label_51DF;

                case 0x1005:
                    text = SK.Text("GENERIC_Scouts", "Scouts");
                    goto Label_51DF;

                case 0x1006:
                    text = SK.Text("GENERIC_Merchants", "Merchants");
                    goto Label_51DF;

                case 0x1007:
                    text = SK.Text("GENERIC_Monks", "Monks");
                    goto Label_51DF;

                case 0x1008:
                    text = SK.Text("TOOLTIPS_VO_POPULARITY", "Popularity");
                    goto Label_51DF;

                case 0x1009:
                    text = SK.Text("TOOLTIPS_VO_NUM_BUILDINGS", "Number of Placed Buildings");
                    goto Label_51DF;

                case 0x100a:
                    text = SK.Text("TOOLTIPS_VO_KEEP_ENCLOSED", "Keep Enclosed");
                    goto Label_51DF;

                case 0x100b:
                    text = SK.Text("TOOLTIPS_VO_KEEP_NOT_ENCLOSED", "Keep Not Enclosed");
                    goto Label_51DF;

                case 0x100c:
                    text = SK.Text("GENERIC_Peasants", "Peasants");
                    goto Label_51DF;

                case 0x100d:
                    text = SK.Text("GENERIC_Archers", "Archers");
                    goto Label_51DF;

                case 0x100e:
                    text = SK.Text("GENERIC_Pikemen", "Pikemen");
                    goto Label_51DF;

                case 0x100f:
                    text = SK.Text("GENERIC_Swordsmen", "Swordsmen");
                    goto Label_51DF;

                case 0x1010:
                    text = SK.Text("GENERIC_Catapults", "Catapults");
                    goto Label_51DF;

                case 0x1011:
                    text = SK.Text("GENERIC_Captains", "Captains");
                    goto Label_51DF;

                case 0x1012:
                    text = SK.Text("TOOLTIPS_VO_TROOPS_EXPAND", "Expand for Detailed Troop Information");
                    goto Label_51DF;

                case 0x1013:
                    text = SK.Text("TOOLTIPS_VO_TROOPS_COLLAPSE", "Close Detailed Troop Information");
                    goto Label_51DF;

                case 0x1014:
                    text = SK.Text("TOOLTIPS_VO_SCOUTS_EXTRA", "Total Scouts (Active Scouts)");
                    goto Label_51DF;

                case 0x1015:
                    text = SK.Text("TOOLTIPS_VO_MERCHANTS_EXTRA", "Total Merchants (Active Merchants)");
                    goto Label_51DF;

                case 0x1016:
                    text = SK.Text("TOOLTIPS_VO_MONKS_EXTRA", "Total Monks (Active Monks)");
                    goto Label_51DF;

                case 0x1017:
                    text = SK.Text("VillageMapPanel_Tax_Income", "Tax Income");
                    goto Label_51DF;

                case 0x1018:
                    text = SK.Text("TOOLTIPS_VO_RATIONS", "Rations");
                    goto Label_51DF;

                case 0x1019:
                    text = SK.Text("TOOLTIPS_VO_ALE_RATIONS", "Ale Rations");
                    goto Label_51DF;

                case 0x101a:
                    text = SK.Text("TOOLTIPS_VO_PEOPLE", "Workers / Housing Capacity - Spare Workers");
                    goto Label_51DF;

                case 0x101b:
                    text = SK.Text("TOOLTIPS_VO_INTERDICTION", "This village is Interdicted.") + AllVillagesPanel.getTooltipDate(CustomTooltipManager.currentOverData);
                    goto Label_51DF;

                case 0x101c:
                    text = SK.Text("TOOLTIPS_VO_EXCOMMUNICATION", "This village is Excommunicated.") + AllVillagesPanel.getTooltipDate(CustomTooltipManager.currentOverData);
                    goto Label_51DF;

                case 0x101d:
                    text = SK.Text("TOOLTIPS_VO_PEACETIME", "This village is in Peacetime.") + AllVillagesPanel.getTooltipDate(CustomTooltipManager.currentOverData);
                    goto Label_51DF;

                case 0x101e:
                    text = SK.Text("TOOLTIPS_VO_NOT_PREMIUM", "A Premium Token is required to view this information.");
                    goto Label_51DF;

                case 0x101f:
                    text = SK.Text("TOOLTIPS_VO_Iron", "Current Iron Level");
                    goto Label_51DF;

                case 0x1020:
                    text = SK.Text("TOOLTIPS_VO_Pitch", "Current Pitch Level");
                    goto Label_51DF;

                case 0x1068:
                    text = SK.Text("TOOLTIPS_PROCLAMATION_HELP_PARISH", "As Parish Steward, you can click here to send a mail to all your Parish members. This feature can only be used once every 7 days.");
                    goto Label_51DF;

                case 0x1069:
                    text = SK.Text("TOOLTIPS_PROCLAMATION_HELP_COUNTY", "As County Sheriff, you can click here to send a mail to all your County members. This feature can only be used once every 7 days.");
                    goto Label_51DF;

                case 0x106a:
                    text = SK.Text("TOOLTIPS_PROCLAMATION_HELP_PROVINCE", "As Province Governor, you can click here to send a mail to all Parish Stewards within your Province. This feature can only be used once every 3 days.");
                    goto Label_51DF;

                case 0x106b:
                    text = SK.Text("TOOLTIPS_PROCLAMATION_HELP_COUNTRY", "As King, you can click here to send a mail to all County Sheriffs within your Country. This feature can only be used once every 3 days.");
                    goto Label_51DF;

                case 0x10cc:
                    text = SK.Text("GENERIC_Research", "Research") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_RESEARCH", "Research new technology and boost productivity on the tech tree");
                    goto Label_51DF;

                case 0x10cd:
                    text = SK.Text("STATS_CATEGORY_TITLE_RANK", "Rank") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_RANK", "Rank up to unlock new research, villages and abilities");
                    goto Label_51DF;

                case 0x10ce:
                    text = SK.Text("GENERIC_Achievements", "Achievements") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_ACHIEVEMENTS", "Master the game by unlocking every achievement");
                    goto Label_51DF;

                case 0x10cf:
                    text = SK.Text("GENERIC_Quests", "Quests") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_QUESTS", "Complete Quests for gold, honour, resources, cards and more");
                    goto Label_51DF;

                case 0x10d0:
                    text = SK.Text("Reports_Reports", "Reports") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_REPORTS", "Check the reports screen for incoming attacks and local news");
                    goto Label_51DF;

                case 0x10d1:
                    text = SK.Text("GENERIC_CoatOfArms", "Coat of Arms") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_COAT_OF_ARMS", "Create a unique shield design and display it on the World Map");
                    goto Label_51DF;

                case 0x10d2:
                    text = SK.Text("AvatarEditor_Avatar", "Avatar") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_AVATAR", "Design your Avatar and display it on the World Map");
                    goto Label_51DF;

                case 0x10d3:
                    text = SK.Text("MENU_Invite_A_Friend", "Invite a Friend") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_INVITE_A_FRIEND", "Earn up to 1500 Crowns when you Invite a Friend");
                    goto Label_51DF;

                case 0x10d4:
                    text = SK.Text("GENERIC_ParishWall", "Parish Wall") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_PARISH_WALL", "Introduce yourself on the Parish Wall");
                    goto Label_51DF;

                case 0x10d5:
                    text = SK.Text("MailScreen_Mail", "Mail") + Environment.NewLine + Environment.NewLine + SK.Text("TOOLTIPS_PT_MAIL", "Check your mail and communicate with other players");
                    goto Label_51DF;

                case 0x1130:
                    text = SK.Text("TOOLTIPS_WIKI_HELP_LINK", "Wiki Help Link");
                    switch (CustomTooltipManager.currentOverData)
                    {
                        case 0:
                            text = SK.Text("TOOLTIP_WIKI_WORLD_MAP", "What is the World Map?");
                            break;

                        case 1:
                            text = SK.Text("TOOLTIP_WIKI_VILLAGE_VILLAGE_MAP", "What are Villages?");
                            break;

                        case 2:
                            text = SK.Text("TOOLTIP_WIKI_VILLAGE_CASTLE_MAP", "What are Castles?");
                            break;

                        case 3:
                            text = SK.Text("TOOLTIP_WIKI_VILLAGE_RESOURCES", "What are Resources?");
                            break;

                        case 4:
                            text = SK.Text("TOOLTIP_WIKI_VILLAGE_TRADE", "How To: Trade");
                            break;

                        case 5:
                            text = SK.Text("TOOLTIP_WIKI_VILLAGE_TROOPS", "How To: Recruit Troops");
                            break;

                        case 6:
                            text = SK.Text("TOOLTIP_WIKI_VILLAGE_UNITS", "How To: Recruit Units");
                            break;

                        case 7:
                            text = SK.Text("TOOLTIP_WIKI_VILLAGE_HOLD_A_BANQUET", "What are Banquets?");
                            break;

                        case 8:
                            text = SK.Text("TOOLTIP_WIKI_VILLAGE_VASSALS", "What are Vassals & Liege Lords?").Replace("&amp;", "&");
                            break;

                        case 9:
                            text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_VILLAGE_MAP", "What is the Capital Town?");
                            break;

                        case 10:
                            text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_CASTLE_MAP", "What is the Capital Castle?");
                            break;

                        case 11:
                            text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_RESOURCES", "What are Resources?");
                            break;

                        case 12:
                            text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_TRADE", "What are Parishes & Capitals").Replace("&amp;", "&");
                            break;

                        case 13:
                            text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_TROOPS", "What are Capital Troops");
                            break;

                        case 14:
                            text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_TRADE", "What are Parishes & Capitals").Replace("&amp;", "&");
                            break;

                        case 15:
                            text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_VOTE", "How To: Vote");
                            break;

                        case 0x10:
                            text = SK.Text("TOOLTIP_WIKI_PARISH_CAPITAL_PARISH_FORUM", "What is the Parish Forum?");
                            break;

                        case 0x11:
                            text = SK.Text("TOOLTIP_WIKI_RESEARCH", "What is Research?");
                            break;

                        case 0x12:
                            text = SK.Text("TOOLTIP_WIKI_RANK", "What are Ranks?");
                            break;

                        case 0x13:
                            text = SK.Text("TOOLTIP_WIKI_QUESTS", "What are Quests?");
                            break;

                        case 20:
                            text = SK.Text("TOOLTIP_WIKI_ATTACKS", "How To: Attack");
                            break;

                        case 0x15:
                            text = SK.Text("TOOLTIP_WIKI_REPORTS", "What are Reports?");
                            break;

                        case 0x16:
                            text = SK.Text("TOOLTIP_WIKI_FACTIONS_HOUSES_GLORY", "What is Glory?");
                            break;

                        case 0x17:
                            text = SK.Text("TOOLTIP_WIKI_FACTIONS_HOUSES_FACTION", "What are Factions?");
                            break;

                        case 0x18:
                            text = SK.Text("TOOLTIP_WIKI_FACTIONS_HOUSES_HOUSE", "What are Houses?");
                            break;

                        case 0x19:
                            text = SK.Text("TOOLTIP_WIKI_CARDS", "What are Strategy Cards?");
                            break;

                        case 0x1a:
                            text = SK.Text("TOOLTIP_WIKI_MAIL", "How To: Use Mail");
                            break;

                        case 0x1b:
                            text = SK.Text("TOOLTIP_WIKI_CHAT", "How To: Use Chat");
                            break;

                        case 0x1c:
                            text = SK.Text("TOOLTIP_WIKI_LEADERBOARD", "What is the Leaderboard?");
                            break;

                        case 30:
                            text = SK.Text("TOOLTIP_WIKI_SETTINGS", "How To: View Options & Settings").Replace("&amp;", "&");
                            break;

                        case 0x20:
                            text = SK.Text("TOOLTIP_WIKI_QUEST_WHEEL", "What is the Quest Wheel?");
                            break;

                        case 0x21:
                            text = SK.Text("TOOLTIP_WIKI_SEND_ATTACK", "How To: Send Attacks");
                            break;

                        case 0x22:
                            text = SK.Text("TOOLTIP_WIKI_SEND_SCOUTS", "How To: Send Scouts");
                            break;

                        case 0x23:
                            text = SK.Text("TOOLTIP_WIKI_SEND_MONKS", "How To: Send Monks");
                            break;

                        case 0x25:
                            text = SK.Text("TOOLTIP_WIKI_BUY_PREMIUM_TOKENS", "What are Premium Tokens?");
                            break;

                        case 0x26:
                            text = SK.Text("TOOLTIP_WIKI_BUY_CROWNS", "What are Crowns?");
                            break;

                        case 0x27:
                            text = SK.Text("TOOLTIP_WIKI_BUY_CARDS", "What are Strategy Cards?");
                            break;

                        case 40:
                            text = SK.Text("TOOLTIP_WIKI_SWAP_CARDS", "How To: Swap Cards");
                            break;

                        case 0x29:
                            text = SK.Text("TOOLTIP_WIKI_LOGOUT", "What are Premium Tokens?");
                            break;

                        case 0x2a:
                            text = SK.Text("TOOLTIP_WIKI_DONATE_TO_PARISH", "How To: Donate to Parish");
                            break;

                        case 0x2b:
                            text = SK.Text("TOOLTIP_WIKI_VILLAGES_OVERVIEW", "What is the Village Overview?");
                            break;

                        case 0x2c:
                            text = SK.Text("TOOLTIP_WIKI_ACHIEVEMENTS", "What are Achievements?");
                            break;

                        case 0x2d:
                            text = SK.Text("TOOLTIP_WIKI_SECONDAGE", "What is the Second Age?");
                            break;

                        case 0x2e:
                            text = SK.Text("TOOLTIP_WIKI_THIRDAGE", "What is the Third Age?");
                            break;

                        case 0x2f:
                            text = SK.Text("TOOLTIP_WIKI_DOMINATION_RULES", "Domination World Explained?");
                            break;

                        case 0x30:
                            text = SK.Text("TOOLTIP_WIKI_FOURTHAGE", "What is the Fourth Age?");
                            break;

                        case 0x31:
                            text = SK.Text("TOOLTIP_WIKI_TREASURE_CASTLES", "What is a Treasure Castle?");
                            break;

                        case 50:
                            text = SK.Text("TOOLTIP_WIKI_PALADIN_CASTLES", "What is a Paladin Castle?");
                            break;

                        case 0x33:
                            text = SK.Text("TOOLTIP_WIKI_FIFTHAGE", "What is the Fifth Age?");
                            break;
                    }
                    goto Label_51DF;

                case 0x1131:
                    text = SK.Text("TOOLTIP_WIKI_CHAT", "How To: Use Chat");
                    goto Label_51DF;

                case 0x1132:
                    text = SK.Text("TOOLTIP_WIKI_SETTINGS", "How To: View Options & Settings").Replace("&amp;", "&");
                    goto Label_51DF;

                case 0x2710:
                case 0x2775:
                    text = "";
                    goto Label_51DF;

                case 0x2711:
                    text = SK.Text("TOOLTIPS_CARD_BAR_PLAY_CARDS", "Click to Play Cards");
                    goto Label_51DF;

                case 0x2712:
                    text = SK.Text("TOOLTIPS_CARD_BAR_EXPAND", "Show Available Cards");
                    goto Label_51DF;

                case 0x2713:
                    text = SK.Text("TOOLTIPS_CARD_BAR_COLLAPSE", "Hide Available Cards");
                    goto Label_51DF;

                case 0x2714:
                    text = SK.Text("TOOLTIPS_CARD_BAR_NEXT", "Show Next Set");
                    goto Label_51DF;

                case 0x2715:
                    text = SK.Text("TOOLTIPS_CARD_BAR_PREV", "Show Previous Set");
                    goto Label_51DF;

                case 0x2774:
                    text = SK.Text("TOOLTIPS_CARD_WINDOW_CLOSE", "Close Window");
                    goto Label_51DF;

                case 0x2776:
                    text = SK.Text("TOOLTIPS_CARD_WINDOW_FILTER", "Filter Cards: ") + CardFilters.getName(CustomTooltipManager.currentOverData);
                    goto Label_51DF;

                case 0x2777:
                    text = SK.Text("ManageCandsPanel_Cash_In", "Cash In");
                    goto Label_51DF;

                case 0x2778:
                    text = SK.Text("ManageCandsPanel_Get_Cards", "Get Cards");
                    goto Label_51DF;

                case 0x2779:
                    text = string.Concat(new object[] { SK.Text("TOOLTIPS_CARD_WINDOW_FILTER", "Filter Cards: "), CardFilters.getName2(CustomTooltipManager.currentOverData), " (", GameEngine.Instance.World.countCardsInCategory(CustomTooltipManager.currentOverData), ")" });
                    goto Label_51DF;

                case 0x283d:
                    text = SK.Text("CARD_OFFERS_Food_Pack", "Food Pack");
                    goto Label_51DF;

                case 0x283e:
                    text = SK.Text("CARD_OFFERS_Castle_Pack", "Castle Pack");
                    goto Label_51DF;

                case 0x283f:
                    text = SK.Text("CARD_OFFERS_Defense_Pack", "Defence Pack");
                    goto Label_51DF;

                case 0x2840:
                    text = SK.Text("CARD_OFFERS_Army_Pack", "Army Pack");
                    goto Label_51DF;

                case 0x2841:
                    text = SK.Text("CARD_OFFERS_Random_Pack", "Random Pack");
                    goto Label_51DF;

                case 0x2842:
                    text = SK.Text("CARD_OFFERS_Industry_Pack", "Industry Pack");
                    goto Label_51DF;

                case 0x2843:
                    text = SK.Text("CARD_OFFERS_Research_Pack", "Research Pack");
                    goto Label_51DF;

                case 0x2844:
                    text = SK.Text("CARD_OFFERS_Exclusive_Pack", "Exclusive Pack");
                    goto Label_51DF;

                case 0x2845:
                    text = SK.Text("CARD_OFFERS_Super_Food_Pack", "Super Food Pack");
                    goto Label_51DF;

                case 0x2846:
                    text = SK.Text("CARD_OFFERS_Super_Defense_Pack", "Super Defence Pack");
                    goto Label_51DF;

                case 0x2847:
                    text = SK.Text("CARD_OFFERS_Super_Army_Pack", "Super Army Pack");
                    goto Label_51DF;

                case 0x2848:
                    text = SK.Text("CARD_OFFERS_Super_Random_Pack", "Super Random Pack");
                    goto Label_51DF;

                case 0x2849:
                    text = SK.Text("CARD_OFFERS_Super_Industry_Pack", "Super Industry Pack");
                    goto Label_51DF;

                case 0x284a:
                    text = SK.Text("CARD_OFFERS_Ultimate_Food_Pack", "Ultimate Food Pack");
                    goto Label_51DF;

                case 0x284b:
                    text = SK.Text("CARD_OFFERS_Ultimate_Defense_Pack", "Ultimate Defence Pack");
                    goto Label_51DF;

                case 0x284c:
                    text = SK.Text("CARD_OFFERS_Ultimate_Army_Pack", "Ultimate Army Pack");
                    goto Label_51DF;

                case 0x284d:
                    text = SK.Text("CARD_OFFERS_Ultimate_Random_Pack", "Ultimate Random Pack");
                    goto Label_51DF;

                case 0x284e:
                    text = SK.Text("CARD_OFFERS_Ultimate_Industry_Pack", "Ultimate Industry Pack");
                    goto Label_51DF;

                case 0x284f:
                    text = SK.Text("CARDS_SEARCH_CARDS", "Search for Cards by Name");
                    goto Label_51DF;

                case 0x2850:
                    text = SK.Text("CARDS_CLEAR_SEARCH_CARDS", "Close Search");
                    goto Label_51DF;

                case 0x2851:
                    text = SK.Text("CARD_OFFERS_Platinum_Pack", "Platinum Pack");
                    goto Label_51DF;

                case 0x286e:
                    text = SK.Text("TOOLTIPS_Aeria_Points", "Click to Purchase AP");
                    goto Label_51DF;

                case 0x2896:
                    text = SK.Text("TOOLTIPS_MAPSIDE_RENAME", "Mod Command : Reset Village Name to Default");
                    goto Label_51DF;

                case 0x2904:
                    text = SK.Text("TOOLTIPS_FREE_CARDS_MAIN", "Click to collect your Free Cards");
                    goto Label_51DF;

                case 0x2905:
                    text = SK.Text("TOOLTIPS_ROYAL_TICKETS_MAIN", "Click to spin the Quest Wheel");
                    goto Label_51DF;

                case 0x2906:
                    text = SK.Text("AI_WolfsRevenge", "Wolf's Revenge") + Environment.NewLine + SK.Text("AI_EndsIn", "Ends In") + " : " + VillageMap.createBuildTimeString(CustomTooltipManager.currentOverData);
                    goto Label_51DF;

                case 0x4e20:
                    text = VillageMap.createBuildTimeString(CustomTooltipManager.currentOverData);
                    goto Label_51DF;

                case 0x5208:
                    text = SK.Text("TOOLTIPS_TRACK_BAR_HINT", "Double-click to enter amount");
                    goto Label_51DF;

                default:
                    goto Label_51DF;
            }
            if (num7 == 0)
            {
                str2 = str2 + "00:";
            }
            else if (num7 < 10)
            {
                str2 = str2 + "0" + num7.ToString() + ":";
            }
            else
            {
                str2 = str2 + num7.ToString() + ":";
            }
            if (num6 == 0)
            {
                str2 = str2 + "00";
            }
            else if (num6 < 10)
            {
                str2 = str2 + "0" + num6.ToString();
            }
            else
            {
                str2 = str2 + num6.ToString();
            }
            text = SK.Text("Dom_Time_Left", "Time Remaining") + " " + str2;
            goto Label_51DF;
        Label_3CF0:
            if (ID == 0xbb9)
            {
                num20 = -1;
            }
            string str5 = getAchievementTitle(num19);
            string str6 = getAchievementRequirement(num19, num20);
            string str7 = getAchievementRequirement(num19, num20 + 1);
            int num21 = 1;
            bool flag3 = true;
            if ((ID == 0xbb9) || (ID == 0xbba))
            {
                flag3 = false;
            }
            else
            {
                num21 = RankingsPanel.getProgressValue(num19);
            }
            NumberFormatInfo nFI = GameEngine.NFI;
            if (((num20 >= 0) && (num20 <= 5)) && (str7.Length > 0))
            {
                text = str5 + Environment.NewLine + str4 + Environment.NewLine + str6 + Environment.NewLine + Environment.NewLine + SK.Text("Achievements_NextLevel", "Next Level") + Environment.NewLine + str7;
                if (flag3 && (num21 > 0))
                {
                    str8 = text;
                    text = str8 + Environment.NewLine + Environment.NewLine + SK.Text("Achievements_CurrentProgress", "Current Progress") + " : " + num21.ToString("N", nFI);
                }
            }
            else if (num20 < 0)
            {
                text = str5 + Environment.NewLine + SK.Text("Achievements_NoLevel", "No Achievements Earned") + Environment.NewLine + str7;
                if (flag3 && (num21 > 0))
                {
                    str8 = text;
                    text = str8 + Environment.NewLine + Environment.NewLine + SK.Text("Achievements_CurrentProgress", "Current Progress") + " : " + num21.ToString("N", nFI);
                }
            }
            else if ((num20 >= 3) && (str7.Length == 0))
            {
                text = str5 + Environment.NewLine + str4 + Environment.NewLine + str6;
            }
        Label_51DF:
            CustomTooltip.CreateToolTip(text, ID, CustomTooltipManager.currentOverData, currentParentWindow);
        }

        private static void systemToolTipEnter(object sender, EventArgs e)
        {
            Control control = (Control) sender;
            MouseEnterTooltipArea(Convert.ToInt32(control.Name), 0, control.FindForm());
            inSystemControl = true;
        }

        private static void systemToolTipLeave(object sender, EventArgs e)
        {
            inSystemControl = false;
            MouseLeaveTooltipArea();
        }

        private static void systemToolTipZoneEnter(object sender, EventArgs e)
        {
            ToolTipZoneControlChild child = findZoneControl(sender);
            if (child != null)
            {
                Point point = new Point(Cursor.Position.X, Cursor.Position.Y);
                Point point2 = child.PointToScreen(((Control) sender).Location);
                Point pt = new Point(point.X - point2.X, point.Y - point2.Y);
                foreach (ToolTipZone zone in child.zones)
                {
                    if (zone.rect.Contains(pt))
                    {
                        if (((zone.relatedControl != null) && !zone.relatedControl.Visible) || ((zone.relatedCSDControl != null) && !zone.relatedCSDControl.Visible))
                        {
                            break;
                        }
                        MouseEnterTooltipArea(zone.ID, 0, child.FindForm());
                        return;
                    }
                }
            }
            MouseLeaveTooltipArea();
        }

        private static void systemToolTipZoneLeave(object sender, EventArgs e)
        {
            MouseLeaveTooltipArea();
        }

        private static void systemToolTipZoneMove(object sender, MouseEventArgs e)
        {
            systemToolTipZoneEnter(sender, e);
        }

        private static void systemToolTipZoneTabEnter(object sender, EventArgs e)
        {
            ToolTipZoneTabChild child = findZoneControlInTab(sender);
            if (child != null)
            {
                Point point = new Point(Cursor.Position.X, Cursor.Position.Y);
                Point point2 = child.PointToScreen(((Control) sender).Location);
                Point pt = new Point(point.X - point2.X, point.Y - point2.Y);
                foreach (ToolTipZone zone in child.zones)
                {
                    if (zone.rect.Contains(pt))
                    {
                        if (((zone.relatedControl != null) && !zone.relatedControl.Visible) || ((zone.relatedCSDControl != null) && !zone.relatedCSDControl.Visible))
                        {
                            break;
                        }
                        MouseEnterTooltipArea(zone.ID);
                        return;
                    }
                }
            }
            MouseLeaveTooltipArea();
        }

        private static void systemToolTipZoneTabLeave(object sender, EventArgs e)
        {
            MouseLeaveTooltipArea();
        }

        private static void systemToolTipZoneTabMove(object sender, MouseEventArgs e)
        {
            systemToolTipZoneTabEnter(sender, e);
        }

        public static void updateTooltipForSystemControl(Control control, int ID)
        {
            control.Name = ID.ToString();
        }

        public class TooltipID
        {
            public const int ABANDON_QUEST = 0xc82;
            public const int ACHIEVEMENT_INPROGRESS = 0xbba;
            public const int ACHIEVEMENT_INPROGRESS_OWN = 0xbbc;
            public const int ACHIEVEMENT_NOT_STARTED = 0xbb9;
            public const int ACHIEVEMENT_NOT_STARTED_OWN = 0xbbb;
            public const int ADD_FAVOURITE = 0x329;
            public const int ADD_FAVOURITE_MARKET = 0x32c;
            public const int ADMIN = 0xc1d;
            public const int AREASELECT_OVER_TAG = 0x44c;
            public const int AREASELECT_OVER_TAG_FULL = 0x44d;
            public const int ARMIES_ATTACKS = 0xb54;
            public const int ARMIES_MERCHANTS = 0xb57;
            public const int ARMIES_MONKS = 0xb58;
            public const int ARMIES_REINFORCEMENTS = 0xb56;
            public const int ARMIES_SCOUTS = 0xb55;
            public const int ARMIES_SORTING = 0xb59;
            public const int ARMY_ATTACK = 0x83a;
            public const int ARMY_CAPTURE = 0x835;
            public const int ARMY_GOLD_RAID = 0x839;
            public const int ARMY_PILLAGE = 0x836;
            public const int ARMY_RANSACK = 0x837;
            public const int ARMY_RAZE = 0x838;
            public const int ARMY_VANDALISE = 0x834;
            public const int BANQUET_CLOSE = 0x3e8;
            public const int BARRACKS_ARCHERS = 0x25b;
            public const int BARRACKS_ARCHERS_NOT_RESEARCHED = 0x260;
            public const int BARRACKS_CAPTAINS = 0x25f;
            public const int BARRACKS_CAPTAINS_NOT_RESEARCHED = 0x264;
            public const int BARRACKS_CATAPULTS = 0x25e;
            public const int BARRACKS_CATAPULTS_NOT_RESEARCHED = 0x263;
            public const int BARRACKS_CLOSE = 0x259;
            public const int BARRACKS_DISBAND = 600;
            public const int BARRACKS_PEASANTS = 0x25a;
            public const int BARRACKS_PIKEMEN = 0x25c;
            public const int BARRACKS_PIKEMEN_NOT_RESEARCHED = 0x261;
            public const int BARRACKS_SWORDSMEN = 0x25d;
            public const int BARRACKS_SWORDSMEN_NOT_RESEARCHED = 610;
            public const int BUY_AERIA_POINTS = 0x286e;
            public const int CAPITAL_DONATE_DESCRIPTION = 0x76c;
            public const int CARD_BAR_CIRCLES = 0x2710;
            public const int CARD_BAR_COLLAPSE = 0x2713;
            public const int CARD_BAR_EXPAND = 0x2712;
            public const int CARD_BAR_NEXT = 0x2714;
            public const int CARD_BAR_PLAY_CARDS = 0x2711;
            public const int CARD_BAR_PREV = 0x2715;
            public const int CARD_WINDOW_CARDS = 0x2775;
            public const int CARD_WINDOW_CASH_IN = 0x2777;
            public const int CARD_WINDOW_CLOSE = 0x2774;
            public const int CARD_WINDOW_FILTER = 0x2776;
            public const int CARD_WINDOW_FILTER2 = 0x2779;
            public const int CARD_WINDOW_GET_CARDS = 0x2778;
            public const int CARDS_ARMY_PACK = 0x2840;
            public const int CARDS_CASTLE_PACK = 0x283e;
            public const int CARDS_CLEAR_SEARCH_CARDS = 0x2850;
            public const int CARDS_DEFENSE_PACK = 0x283f;
            public const int CARDS_EXCLUSIVE_PACK = 0x2844;
            public const int CARDS_FARMING_PACK = 0x283d;
            public const int CARDS_INDUSTRY_PACK = 0x2842;
            public const int CARDS_PLATINUM_PACK = 0x2851;
            public const int CARDS_RANDOM_PACK = 0x2841;
            public const int CARDS_RESEARCH_PACK = 0x2843;
            public const int CARDS_SEARCH_CARDS = 0x284f;
            public const int CARDS_SUPER_ARMY_PACK = 0x2847;
            public const int CARDS_SUPER_DEFENSE_PACK = 0x2846;
            public const int CARDS_SUPER_FARMING_PACK = 0x2845;
            public const int CARDS_SUPER_INDUSTRY_PACK = 0x2849;
            public const int CARDS_SUPER_RANDOM_PACK = 0x2848;
            public const int CARDS_ULTIMATE_ARMY_PACK = 0x284c;
            public const int CARDS_ULTIMATE_DEFENSE_PACK = 0x284b;
            public const int CARDS_ULTIMATE_FARMING_PACK = 0x284a;
            public const int CARDS_ULTIMATE_INDUSTRY_PACK = 0x284e;
            public const int CARDS_ULTIMATE_RANDOM_PACK = 0x284d;
            public const int CASTLEMAP_RIGHT_PANEL_1X1 = 0xcf;
            public const int CASTLEMAP_RIGHT_PANEL_1X5 = 210;
            public const int CASTLEMAP_RIGHT_PANEL_3X3 = 0xd0;
            public const int CASTLEMAP_RIGHT_PANEL_5X5 = 0xd1;
            public const int CASTLEMAP_RIGHT_PANEL_BUILDING_ROLLOVER = 200;
            public const int CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_1 = 0xc9;
            public const int CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_2 = 0xca;
            public const int CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_3 = 0xcb;
            public const int CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_4 = 0xcc;
            public const int CASTLEMAP_RIGHT_PANEL_BUILDING_TAB_5 = 0xdf;
            public const int CASTLEMAP_RIGHT_PANEL_CANCEL = 220;
            public const int CASTLEMAP_RIGHT_PANEL_CASTLE_CONSTRUCTION_OPTIONS_CLOSE = 0xd8;
            public const int CASTLEMAP_RIGHT_PANEL_CASTLE_CONSTRUCTION_OPTIONS_OPEN = 0xd7;
            public const int CASTLEMAP_RIGHT_PANEL_CONFIRM = 0xdb;
            public const int CASTLEMAP_RIGHT_PANEL_DELETE_CONSTRUCTING = 0xda;
            public const int CASTLEMAP_RIGHT_PANEL_DELETE_TROOPS = 0xdd;
            public const int CASTLEMAP_RIGHT_PANEL_REPAIR = 0xd9;
            public const int CASTLEMAP_RIGHT_PANEL_TOGGLE_AGGRESSIVE = 0xde;
            public const int CASTLEMAP_RIGHT_PANEL_TOGGLE_DELETE_OFF = 0xd6;
            public const int CASTLEMAP_RIGHT_PANEL_TOGGLE_DELETE_ON = 0xd5;
            public const int CASTLEMAP_RIGHT_PANEL_TOGGLE_REINFORCEMENTS_OFF = 0xce;
            public const int CASTLEMAP_RIGHT_PANEL_TOGGLE_REINFORCEMENTS_ON = 0xcd;
            public const int CASTLEMAP_RIGHT_PANEL_TOGGLE_VIEWMODE_HIGH = 0xd3;
            public const int CASTLEMAP_RIGHT_PANEL_TOGGLE_VIEWMODE_LOW = 0xd4;
            public const int FACTION_ALLY = 0x8ff;
            public const int FACTION_ENEMY = 0x900;
            public const int FACTION_LEADER = 0x901;
            public const int FACTION_OFFICER = 0x902;
            public const int FACTION_SIDEBAR_CHAT = 0x935;
            public const int FACTION_SIDEBAR_DIPLOMACY = 0x930;
            public const int FACTION_SIDEBAR_FORUM = 0x932;
            public const int FACTION_SIDEBAR_INVITES = 0x934;
            public const int FACTION_SIDEBAR_LEAVE = 0x937;
            public const int FACTION_SIDEBAR_MAIL = 0x933;
            public const int FACTION_SIDEBAR_MY_FACTION = 0x92f;
            public const int FACTION_SIDEBAR_OFFICERS = 0x931;
            public const int FACTION_SIDEBAR_SHOW_ALL = 0x92e;
            public const int FACTION_SIDEBAR_START = 0x936;
            public const int FACTIONTAB_FACTIONS = 0x8fd;
            public const int FACTIONTAB_GLORY = 0x8fc;
            public const int FACTIONTAB_HOUSE = 0x8fe;
            public const int FAVOURITE_ATTACK_TARGET_CLEAR = 0x83b;
            public const int FAVOURITE_ATTACK_TARGET_MAKE = 0x7e2;
            public const int FIND_HIGHEST_PRICE = 0x32e;
            public const int FIND_LOWEST_PRICE = 0x32f;
            public const int FREE_CARDS_MAIN = 0x2904;
            public const int GENERIC_TIME = 0x4e20;
            public const int GLORY_HOUSE = 0x6a4;
            public const int GLORY_HOUSE_1 = 0x6a4;
            public const int GLORY_HOUSE_10 = 0x6ad;
            public const int GLORY_HOUSE_11 = 0x6ae;
            public const int GLORY_HOUSE_12 = 0x6af;
            public const int GLORY_HOUSE_13 = 0x6b0;
            public const int GLORY_HOUSE_14 = 0x6b1;
            public const int GLORY_HOUSE_15 = 0x6b2;
            public const int GLORY_HOUSE_16 = 0x6b3;
            public const int GLORY_HOUSE_17 = 0x6b4;
            public const int GLORY_HOUSE_18 = 0x6b5;
            public const int GLORY_HOUSE_19 = 0x6b6;
            public const int GLORY_HOUSE_2 = 0x6a5;
            public const int GLORY_HOUSE_20 = 0x6b7;
            public const int GLORY_HOUSE_3 = 0x6a6;
            public const int GLORY_HOUSE_4 = 0x6a7;
            public const int GLORY_HOUSE_5 = 0x6a8;
            public const int GLORY_HOUSE_6 = 0x6a9;
            public const int GLORY_HOUSE_7 = 0x6aa;
            public const int GLORY_HOUSE_8 = 0x6ab;
            public const int GLORY_HOUSE_9 = 0x6ac;
            public const int HOUSE_GLORY_ROLLOVER = 0x904;
            public const int HOUSE_ROLLOVER = 0x903;
            public const int LOGIN_EDIT_SHIELD = 0xfaf;
            public const int LOGIN_ENGLISH_FLAG = 0xfab;
            public const int LOGIN_ENGLISH_SUPPORT = 0xfa1;
            public const int LOGIN_EUROPE_SUPPORT = 0xfbf;
            public const int LOGIN_EUROPEAN_FLAG = 0xfc1;
            public const int LOGIN_FIFTH_AGE = 0xfc7;
            public const int LOGIN_FIRST_AGE = 0xfc6;
            public const int LOGIN_FOURTH_AGE = 0xfc2;
            public const int LOGIN_FRENCH_FLAG = 0xfad;
            public const int LOGIN_FRENCH_SUPPORT = 0xfa3;
            public const int LOGIN_GERMAN_FLAG = 0xfac;
            public const int LOGIN_GERMAN_SUPPORT = 0xfa2;
            public const int LOGIN_ITALIAN_FLAG = 0xfbd;
            public const int LOGIN_ITALIAN_SUPPORT = 0xfbb;
            public const int LOGIN_MAP_OF_ENGLAND = 0xfa5;
            public const int LOGIN_MAP_OF_EUROPE = 0xfc0;
            public const int LOGIN_MAP_OF_FRANCE = 0xfa7;
            public const int LOGIN_MAP_OF_GERMANY = 0xfa6;
            public const int LOGIN_MAP_OF_ITALY = 0xfbc;
            public const int LOGIN_MAP_OF_POLAND = 0xfb5;
            public const int LOGIN_MAP_OF_RUSSIA = 0xfa8;
            public const int LOGIN_MAP_OF_SOUTH_AMERICA = 0xfc4;
            public const int LOGIN_MAP_OF_SPAIN = 0xfb1;
            public const int LOGIN_MAP_OF_TURKEY = 0xfb8;
            public const int LOGIN_MAP_OF_USA = 0xfbe;
            public const int LOGIN_OFFLINE = 0xfa9;
            public const int LOGIN_ONLINE = 0xfaa;
            public const int LOGIN_POLISH_FLAG = 0xfb6;
            public const int LOGIN_POLISH_SUPPORT = 0xfb4;
            public const int LOGIN_PORTUGUESE_FLAG = 0xfc5;
            public const int LOGIN_PORTUGUESE_SUPPORT = 0xfc3;
            public const int LOGIN_RUSSIAN_FLAG = 0xfae;
            public const int LOGIN_RUSSIAN_SUPPORT = 0xfa4;
            public const int LOGIN_SECOND_AGE = 0xfb3;
            public const int LOGIN_SPANISH_FLAG = 0xfb2;
            public const int LOGIN_SPANISH_SUPPORT = 0xfb0;
            public const int LOGIN_THIRD_AGE = 0xfba;
            public const int LOGIN_TURKISH_FLAG = 0xfb9;
            public const int LOGIN_TURKISH_SUPPORT = 0xfb7;
            public const int LOGOUT_ATTACK_AI = 0x583;
            public const int LOGOUT_ATTACK_BANDITS = 0x581;
            public const int LOGOUT_ATTACK_WOLVES = 0x582;
            public const int LOGOUT_AUTO_ATTACK = 0x57b;
            public const int LOGOUT_AUTO_REBUILD = 0x57d;
            public const int LOGOUT_AUTO_RECRUIT = 0x57c;
            public const int LOGOUT_AUTO_SCOUT = 0x57a;
            public const int LOGOUT_AUTO_TRADE = 0x579;
            public const int LOGOUT_AUTO_TRANSFER = 0x57e;
            public const int LOGOUT_CANCEL = 0x58b;
            public const int LOGOUT_CLOSE = 0x578;
            public const int LOGOUT_EXIT = 0x58a;
            public const int LOGOUT_PREMIUM = 0x58d;
            public const int LOGOUT_RECRUIT_ARCHER = 0x585;
            public const int LOGOUT_RECRUIT_CATAPULT = 0x588;
            public const int LOGOUT_RECRUIT_PEASANT = 0x584;
            public const int LOGOUT_RECRUIT_PIKEMAN = 0x586;
            public const int LOGOUT_RECRUIT_SWORDSMAN = 0x587;
            public const int LOGOUT_RESOURCES = 0x589;
            public const int LOGOUT_SELECT_TRADE_PERCENT = 0x580;
            public const int LOGOUT_SELECT_TRADE_RESOURCE = 0x57f;
            public const int LOGOUT_SWAP_WORLDS = 0x58c;
            public const int MAIL_CURRENT_ATTACHMENTS = 0x201;
            public const int MAIL_FAVOURITES = 0x1fb;
            public const int MAIL_LINK_PARISH = 0x205;
            public const int MAIL_LINK_PLAYER = 0x203;
            public const int MAIL_LINK_VILLAGE = 0x204;
            public const int MAIL_OPEN_ATTACHMENTS = 0x202;
            public const int MAIL_OTHERS_KNOWN = 0x1fc;
            public const int MAIL_RECENT = 0x1fa;
            public const int MAIL_SEARCH = 0x1f9;
            public const int MAIL_SEARCH_REGION = 0x200;
            public const int MAIL_SEARCH_USER = 510;
            public const int MAIL_SELECT_VILLAGE = 0x1ff;
            public const int MAIL_VILLAGE_SEARCH_DISABLED = 0x206;
            public const int MAILSCREEN_AGGRESSIVE_BLOCK = 0x1f8;
            public const int MAILSCREEN_CLOSE = 0x1f6;
            public const int MAILSCREEN_DOCK = 0x1f5;
            public const int MAILSCREEN_FLOAT = 500;
            public const int MAILSCREEN_REPORT = 0x1f7;
            public const int MAINWINDOW_ATTACK_TARGETS = 0x5e;
            public const int MAINWINDOW_CARDS_SHOW_ALL_CARDS = 1;
            public const int MAINWINDOW_MAP_FILTERING = 0x5b;
            public const int MAINWINDOW_MAP_FILTERING_ACTIVE = 0x5d;
            public const int MAINWINDOW_MAP_FILTERING_CLOSE = 0x5c;
            public const int MAINWINDOW_TOP_LEFT_DOMINATION_WORLD = 10;
            public const int MAINWINDOW_TOP_LEFT_FAITHPOINTS = 6;
            public const int MAINWINDOW_TOP_LEFT_FIFTH_AGE = 13;
            public const int MAINWINDOW_TOP_LEFT_FOURTH_AGE = 12;
            public const int MAINWINDOW_TOP_LEFT_GOLD = 5;
            public const int MAINWINDOW_TOP_LEFT_HC_TIME_LEFT = 11;
            public const int MAINWINDOW_TOP_LEFT_HONOUR = 4;
            public const int MAINWINDOW_TOP_LEFT_POINTS = 7;
            public const int MAINWINDOW_TOP_LEFT_RANKING = 3;
            public const int MAINWINDOW_TOP_LEFT_SECOND_AGE = 8;
            public const int MAINWINDOW_TOP_LEFT_THIRD_AGE = 9;
            public const int MAINWINDOW_TOP_LEFT_USERNAME = 2;
            public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_ATTACKS = 0x1a;
            public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_CAPITAL = 0x20;
            public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_CHAT = 0x35;
            public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_FACTIONS = 30;
            public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_LEADERBOARD = 0x1d;
            public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_MAIL = 0x1c;
            public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_PREMIUM_VO = 0x21;
            public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_QUESTS = 0x1f;
            public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_RANKING = 0x19;
            public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_REPORTS = 0x1b;
            public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_RESEARCH = 0x18;
            public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_VILLAGEMAP = 0x17;
            public const int MAINWINDOW_TOP_RIGHT_MAIN_TAB_WORLDMAP = 0x16;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_LIST = 0x15;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_SCROLL = 20;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_BANQUETING = 0x2f;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CAPITAL_FORUM = 0x33;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CAPITAL_INFO = 0x31;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CAPITAL_VOTE = 50;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_CASTLE = 0x29;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_NOT_IMPLEMENTED_YET = 0x34;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_RESOURCES = 0x2a;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_TRADING = 0x2b;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_TROOPS = 0x2c;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_UNITS = 0x2d;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_UNKNOWN = 0x2e;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_VASSALS = 0x30;
            public const int MAINWINDOW_TOP_RIGHT_VILLAGE_TAB_VILLAGE = 40;
            public const int MAINWINDOW_VILLAGE_NAME_TEST = 90;
            public const int MAPSIDE_ATTACK = 0x961;
            public const int MAPSIDE_BANDIT_CAMP = 0x978;
            public const int MAPSIDE_BUY_CHARTER_RANK_TOO_LOW = 0x9c8;
            public const int MAPSIDE_BUY_CHARTER_RANK_TOO_LOW12 = 0x9ca;
            public const int MAPSIDE_CANCEL = 0x960;
            public const int MAPSIDE_CANT_BUY_FROM_HERE = 0x9c9;
            public const int MAPSIDE_CAPITAL_ATTACK = 0x96b;
            public const int MAPSIDE_CAPITAL_MONK = 0x96e;
            public const int MAPSIDE_CAPITAL_REINFORCE = 0x96d;
            public const int MAPSIDE_CAPITAL_SCOUT = 0x96c;
            public const int MAPSIDE_CAPITAL_TRADE = 0x96a;
            public const int MAPSIDE_COUNTRY = 0x977;
            public const int MAPSIDE_COUNTY = 0x975;
            public const int MAPSIDE_FACTIONNAME = 0x9c5;
            public const int MAPSIDE_FILTER_AI = 0x99e;
            public const int MAPSIDE_FILTER_ATTACK = 0x997;
            public const int MAPSIDE_FILTER_CLEAR = 0x99b;
            public const int MAPSIDE_FILTER_FACTION = 0x99a;
            public const int MAPSIDE_FILTER_HOUSE = 0x999;
            public const int MAPSIDE_FILTER_OPEN_FACTION = 0x99d;
            public const int MAPSIDE_FILTER_SCOUT = 0x998;
            public const int MAPSIDE_FILTER_SEARCH = 0x99c;
            public const int MAPSIDE_FILTER_TRADE = 0x996;
            public const int MAPSIDE_MAKE_MERCENARIES = 0x98a;
            public const int MAPSIDE_MAKE_TROOPS = 0x988;
            public const int MAPSIDE_MAKE_VASSAL = 0x98e;
            public const int MAPSIDE_MARKET = 0x964;
            public const int MAPSIDE_MEDIUM_PALADIN_CASTLE = 0x990;
            public const int MAPSIDE_MONK = 0x966;
            public const int MAPSIDE_OWN_ATTACK = 0x980;
            public const int MAPSIDE_OWN_MONK = 0x983;
            public const int MAPSIDE_OWN_REINFORCE = 0x982;
            public const int MAPSIDE_OWN_SCOUT = 0x981;
            public const int MAPSIDE_OWN_TRADE = 0x97f;
            public const int MAPSIDE_PARISH = 0x974;
            public const int MAPSIDE_PARISH_PLAGUE = 0x992;
            public const int MAPSIDE_PARISH_TRADE = 0x989;
            public const int MAPSIDE_PIGS_CASTLE = 0x97c;
            public const int MAPSIDE_PROVINCE = 0x976;
            public const int MAPSIDE_RATS_CASTLE = 0x97a;
            public const int MAPSIDE_REINFORCE = 0x962;
            public const int MAPSIDE_RENAME = 0x2896;
            public const int MAPSIDE_SCOUT = 0x965;
            public const int MAPSIDE_SCOUT_STASH = 0x98b;
            public const int MAPSIDE_SENDMAIL = 0x9c6;
            public const int MAPSIDE_SMALL_PALADIN_CASTLE = 0x98f;
            public const int MAPSIDE_SNAKES_CASTLE = 0x97b;
            public const int MAPSIDE_STASH = 0x97e;
            public const int MAPSIDE_TERRAIN_TYPE = 0x984;
            public const int MAPSIDE_TRADE = 0x963;
            public const int MAPSIDE_TREASURE_CASTLE = 0x991;
            public const int MAPSIDE_USERINFO = 0x9c7;
            public const int MAPSIDE_VASSAL = 0x967;
            public const int MAPSIDE_VASSAL_ATTACK_FROM = 0x995;
            public const int MAPSIDE_VASSAL_MANAGE_TROOPS = 0x993;
            public const int MAPSIDE_VASSAL_MANAGE_VASSAL = 0x994;
            public const int MAPSIDE_VIEW_CASTLE = 0x986;
            public const int MAPSIDE_VIEW_CASTLE_REPORT = 0x98d;
            public const int MAPSIDE_VIEW_RESOURCES = 0x987;
            public const int MAPSIDE_VIEW_VILLAGE = 0x985;
            public const int MAPSIDE_VILLAGE_CHARTER = 0x98c;
            public const int MAPSIDE_WOLF_LAIR = 0x979;
            public const int MAPSIDE_WOLFS_CASTLE = 0x97d;
            public const int MENUBAR_ABANDON = 0x4b1;
            public const int MENUBAR_CONVERT = 0x4b0;
            public const int MONK_ABSOLUTION = 0x7d5;
            public const int MONK_BLESSING = 0x7d1;
            public const int MONK_EXCOMMUNICATION = 0x7d6;
            public const int MONK_INFLUENCE = 0x7d0;
            public const int MONK_INQUISITION = 0x7d2;
            public const int MONK_INTERDICTS = 0x7d3;
            public const int MONK_NEGATIVE_INFLUENCE = 0x7d8;
            public const int MONK_POSITIVE_INFLUENCE = 0x7d7;
            public const int MONK_RESTORATION = 0x7d4;
            public const int NEW_VILLAGE_ADVANCED = 0x709;
            public const int NEW_VILLAGE_ENTER = 0x708;
            public const int NEW_VILLAGE_LOGOUT = 0x70a;
            public const int NO_TOOLTIP = 0;
            public const int PROCLAMATION_HELP_COUNTRY = 0x106b;
            public const int PROCLAMATION_HELP_COUNTY = 0x1069;
            public const int PROCLAMATION_HELP_PARISH = 0x1068;
            public const int PROCLAMATION_HELP_PROVINCE = 0x106a;
            public const int PT_ACHIEVEMENTS = 0x10ce;
            public const int PT_AVATAR = 0x10d2;
            public const int PT_COAT_OF_ARMS = 0x10d1;
            public const int PT_INVITE_A_FRIEND = 0x10d3;
            public const int PT_MAIL = 0x10d5;
            public const int PT_PARISH_WALL = 0x10d4;
            public const int PT_QUESTS = 0x10cf;
            public const int PT_RANK = 0x10cd;
            public const int PT_REPORTS = 0x10d0;
            public const int PT_RESEARCH = 0x10cc;
            public const int QUEST_REWARD_APPLES = 0xc87;
            public const int QUEST_REWARD_CARD_PACKS = 0xc88;
            public const int QUEST_REWARD_FAITHPOINTS = 0xc8a;
            public const int QUEST_REWARD_FISH = 0xc8e;
            public const int QUEST_REWARD_GLORY = 0xc8b;
            public const int QUEST_REWARD_GOLD = 0xc84;
            public const int QUEST_REWARD_HONOUR = 0xc83;
            public const int QUEST_REWARD_PREMIUM_CARD = 0xc89;
            public const int QUEST_REWARD_SHIELD_CHARGES = 0xc8c;
            public const int QUEST_REWARD_STONE = 0xc86;
            public const int QUEST_REWARD_TICKETS = 0xc8d;
            public const int QUEST_REWARD_WOOD = 0xc85;
            public const int RANKING_IMAGE = 0x191;
            public const int RANKING_UPGRADE = 400;
            public const int REMOVE_FAVOURITE = 0x328;
            public const int REMOVE_FAVOURITE_MARKET = 0x32b;
            public const int REMOVE_RECENT = 810;
            public const int REMOVE_RECENT_MARKET = 0x32d;
            public const int REPORTS_CAPTURE = 0x5dd;
            public const int REPORTS_DELETE = 0x5de;
            public const int REPORTS_FILTER = 0x5dc;
            public const int RESEARCH_QUEUE = 0x12e;
            public const int RESEARCHTREE_LIST_MODE = 300;
            public const int RESEARCHTREE_TREE_MODE = 0x12d;
            public const int RESOURCES_CLOSE = 900;
            public const int RESOURCES_INFO = 0x385;
            public const int ROYAL_TICKETS_MAIN = 0x2905;
            public const int START_QUEST = 0xc81;
            public const int STATS_CATEGORY_ICONS = 0x514;
            public const int STOCKEXCHANGE_CLOSE = 800;
            public const int TOGGLE_TO_STOCKEXCHANGE = 0x326;
            public const int TOGGLE_TO_TRADING = 0x327;
            public const int TRACK_BAR_HINT = 0x5208;
            public const int TRADE_BANQUETING = 0x325;
            public const int TRADE_CLOSE = 0x321;
            public const int TRADE_FOOD = 0x323;
            public const int TRADE_RESOURCES = 0x322;
            public const int TRADE_WEAPONS = 0x324;
            public const int TUTORIAL_PLAYER_GUILD = 0x641;
            public const int TUTORIAL_REOPEN = 0x640;
            public const int UNIT_MAKE_ERROR = 0xa8c;
            public const int UNIT_MAKE_ERROR_FULL = 8;
            public const int UNIT_MAKE_ERROR_GOLD = 2;
            public const int UNIT_MAKE_ERROR_PEASANTS = 1;
            public const int UNIT_MAKE_ERROR_SPACE = 4;
            public const int UNIT_MAKE_ERROR_TROOP_SPACE = 0x20;
            public const int UNIT_MAKE_ERROR_WEAPON = 0x10;
            public const int UNITS_CLOSE = 0x2bd;
            public const int UNITS_DISBAND = 700;
            public const int UNITS_MERCHANTS = 0x2bf;
            public const int UNITS_MERCHANTS_NOT_RESEARCHED = 0x2c2;
            public const int UNITS_MONKS = 0x2c0;
            public const int UNITS_MONKS_NOT_RESEARCHED = 0x2c3;
            public const int UNITS_SCOUTS = 0x2c1;
            public const int UNITS_SCOUTS_NOT_RESEARCHED = 0x2c4;
            public const int UNITS_SPACE_REQUIRED = 0x2be;
            public const int USER_CLEAR_DIPLOMACY = 0xc1e;
            public const int USER_CLEAR_DIPLOMACY_NOTES = 0xc1f;
            public const int VASSAL_AVAILABLE_TROOPS = 0xaf0;
            public const int VILLAGEMAP_CAPITAL_OVER_COMPLETED = 150;
            public const int VILLAGEMAP_CAPITAL_OVER_NOTCOMPLETED = 0x97;
            public const int VILLAGEMAP_INFO_BAR_CAPITAL_FLAGS = 0x92;
            public const int VILLAGEMAP_INFO_BAR_CAPITAL_GOLD = 0x91;
            public const int VILLAGEMAP_INFO_BAR_DONATION_TYPE = 0x93;
            public const int VILLAGEMAP_INFO_BAR_FOOD = 0x90;
            public const int VILLAGEMAP_INFO_BAR_IRON = 0x95;
            public const int VILLAGEMAP_INFO_BAR_PEOPLE = 0x8d;
            public const int VILLAGEMAP_INFO_BAR_PITCH = 0x94;
            public const int VILLAGEMAP_INFO_BAR_STONE = 0x8f;
            public const int VILLAGEMAP_INFO_BAR_WOOD = 0x8e;
            public const int VILLAGEMAP_RIGHT_PANEL_ALE_DEC = 120;
            public const int VILLAGEMAP_RIGHT_PANEL_ALE_INC = 0x77;
            public const int VILLAGEMAP_RIGHT_PANEL_BUILD_INFO = 140;
            public const int VILLAGEMAP_RIGHT_PANEL_BUILDING_ROLLOVER = 100;
            public const int VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_1 = 0x65;
            public const int VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_2 = 0x66;
            public const int VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_3 = 0x67;
            public const int VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_4 = 0x68;
            public const int VILLAGEMAP_RIGHT_PANEL_BUILDING_TAB_5 = 0x69;
            public const int VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_1 = 0x6a;
            public const int VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_2 = 0x6b;
            public const int VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_3 = 0x6c;
            public const int VILLAGEMAP_RIGHT_PANEL_CAPITAL_BUILDING_TAB_4 = 0x6d;
            public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_ALE_CLOSE = 0x80;
            public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_ALE_OPEN = 0x7f;
            public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_ENTERTAINMENT_CLOSE = 0x84;
            public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_ENTERTAINMENT_OPEN = 0x83;
            public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_EVENTS_CLOSE = 0x86;
            public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_EVENTS_OPEN = 0x85;
            public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_HOUSING_CLOSE = 130;
            public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_HOUSING_OPEN = 0x81;
            public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_RATIONS_CLOSE = 0x7e;
            public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_RATIONS_OPEN = 0x7d;
            public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_TAX_CLOSE = 0x7c;
            public const int VILLAGEMAP_RIGHT_PANEL_DETAILED_TAX_OPEN = 0x7b;
            public const int VILLAGEMAP_RIGHT_PANEL_EXTRAS_BAR = 110;
            public const int VILLAGEMAP_RIGHT_PANEL_HONOUR_BAR = 0x6f;
            public const int VILLAGEMAP_RIGHT_PANEL_HONOUR_BAR_CLOSE = 0x70;
            public const int VILLAGEMAP_RIGHT_PANEL_IN_BUILDING_CLOSE = 0x71;
            public const int VILLAGEMAP_RIGHT_PANEL_MOVE_BUILDING = 0x72;
            public const int VILLAGEMAP_RIGHT_PANEL_POPULARITY_BAR = 0x79;
            public const int VILLAGEMAP_RIGHT_PANEL_POPULARITY_BAR_CLOSE = 0x7a;
            public const int VILLAGEMAP_RIGHT_PANEL_RATIONS_DEC = 0x76;
            public const int VILLAGEMAP_RIGHT_PANEL_RATIONS_INC = 0x75;
            public const int VILLAGEMAP_RIGHT_PANEL_TAX_DEC = 0x74;
            public const int VILLAGEMAP_RIGHT_PANEL_TAX_INC = 0x73;
            public const int VO_ALE_RATIONS = 0x1019;
            public const int VO_EXCOMMUNICATION = 0x101c;
            public const int VO_INTERDICTION = 0x101b;
            public const int VO_IRON = 0x101f;
            public const int VO_KEEP_ENCLOSED = 0x100a;
            public const int VO_KEEP_NOT_ENCLOSED = 0x100b;
            public const int VO_MERCHANTS = 0x1006;
            public const int VO_MERCHANTS_EXTRA = 0x1015;
            public const int VO_MONKS = 0x1007;
            public const int VO_MONKS_EXTRA = 0x1016;
            public const int VO_NOT_PREMIUM = 0x101e;
            public const int VO_NUM_BUILDINGS = 0x1009;
            public const int VO_PEACETIME = 0x101d;
            public const int VO_PEOPLE = 0x101a;
            public const int VO_PITCH = 0x1020;
            public const int VO_POPULARITY = 0x1008;
            public const int VO_RATIONS = 0x1018;
            public const int VO_SCOUTS = 0x1005;
            public const int VO_SCOUTS_EXTRA = 0x1014;
            public const int VO_TAX_INCOME = 0x1017;
            public const int VO_TROOPS = 0x1004;
            public const int VO_TROOPS_ARCHERS = 0x100d;
            public const int VO_TROOPS_CAPTAINS = 0x1011;
            public const int VO_TROOPS_CATAPULTS = 0x1010;
            public const int VO_TROOPS_COLLAPSE = 0x1013;
            public const int VO_TROOPS_EXPAND = 0x1012;
            public const int VO_TROOPS_PEASANTS = 0x100c;
            public const int VO_TROOPS_PIKEMEN = 0x100e;
            public const int VO_TROOPS_SWORDSMEN = 0x100f;
            public const int WIKI_HELP_LINK = 0x1130;
            public const int WIKI_HELP_LINK_CHAT_SPECIAL = 0x1131;
            public const int WIKI_HELP_LINK_SETTINGS_SPECIAL = 0x1132;
            public const int WOLFS_REVENGE = 0x2906;
        }

        public class ToolTipZone
        {
            public int ID;
            public Rectangle rect;
            public Control relatedControl;
            public CustomSelfDrawPanel.CSDControl relatedCSDControl;

            public ToolTipZone(int x, int y, int width, int height, int tooltipID)
            {
                this.rect = new Rectangle(x, y, width, height);
                this.ID = tooltipID;
            }

            public ToolTipZone(int x, int y, int width, int height, int tooltipID, CustomSelfDrawPanel.CSDControl relative)
            {
                this.rect = new Rectangle(x, y, width, height);
                this.ID = tooltipID;
                this.relatedCSDControl = relative;
            }

            public ToolTipZone(int x, int y, int width, int height, int tooltipID, Control relative)
            {
                this.rect = new Rectangle(x, y, width, height);
                this.ID = tooltipID;
                this.relatedControl = relative;
            }
        }

        public class ToolTipZoneControlChild : Control
        {
            public CustomTooltipManager.ToolTipZone[] zones;
        }

        public class ToolTipZoneTabChild : TabPage
        {
            public CustomTooltipManager.ToolTipZone[] zones;
        }
    }
}

