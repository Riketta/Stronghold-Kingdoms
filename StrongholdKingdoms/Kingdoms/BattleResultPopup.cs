namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class BattleResultPopup : MyFormBase
    {
        private BitmapButton btnClose;
        private BitmapButton btnMinimise;
        private BitmapButton btnShowResources;
        private IContainer components;
        private Label label1;
        private Label label11;
        private Label label12;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label lblAttackersArchers;
        private Label lblAttackersCaptains;
        private Label lblAttackersCatapults;
        private Label lblAttackersPeasants;
        private Label lblAttackersPikemen;
        private Label lblAttackersSwordsmen;
        private Label lblAttackType;
        private Label lblDate;
        private Label lblDefendersArchers;
        private Label lblDefendersCaptains;
        private Label lblDefendersPeasants;
        private Label lblDefendersPikemen;
        private Label lblDefendersSwordsmen;
        private Label lblHonour;
        private Label lblResult;
        private Label lblSpoils;
        private Label lblTargetVillageNameAndInfo;
        private Label lblVillageName;
        private Label lblWolves;
        private static CastleMapBattlePanel2 m_parent;
        private GetReport_ReturnType m_reportReturnData;
        private int m_villageID = -1;
        private static AttackReportsResourcesPanel resourcesPanel;
        private bool showPostTutorialThingy;
        private int sizeAddition;

        public BattleResultPopup()
        {
            this.InitializeComponent();
            this.lblHonour.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblSpoils.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblTargetVillageNameAndInfo.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblDate.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblResult.Font = FontManager.GetFont("Microsoft Sans Serif", 12f);
            this.lblAttackType.Font = FontManager.GetFont("Microsoft Sans Serif", 16f);
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void BattleResultPopup_Load(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.showPostTutorialThingy)
            {
                CastleMapBattlePanel2.fromReports = false;
            }
            if ((resourcesPanel != null) && !resourcesPanel.Created)
            {
                resourcesPanel.Close();
                resourcesPanel = null;
            }
            base.Close();
            m_parent.closePopup(true, this.showPostTutorialThingy);
        }

        private void btnMinimise_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("BattleResultPopup_close");
            if ((resourcesPanel != null) && !resourcesPanel.Created)
            {
                resourcesPanel.Close();
                resourcesPanel = null;
            }
            m_parent.closePopup(false, false);
            base.Close();
        }

        private void btnShowResources_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("BattleResultPopup_resources");
            if ((resourcesPanel != null) && !resourcesPanel.Created)
            {
                resourcesPanel.Close();
                resourcesPanel = null;
            }
            resourcesPanel = new AttackReportsResourcesPanel();
            resourcesPanel.setResources(this.m_reportReturnData);
            resourcesPanel.Show();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(bool attackerVictory, BattleTroopNumbers startingTroops, BattleTroopNumbers endingTroops, int attackType, int villageID, GetReport_ReturnType returnData, CastleMapBattlePanel2 parent)
        {
            base.ShowClose = false;
            this.lblVillageName.Text = SK.Text("BattleResultPopup_Village_Name", "Village Name");
            this.lblAttackType.Text = SK.Text("GENERIC_Attack_Type", "Attack Type");
            this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
            this.btnMinimise.Text = SK.Text("BattleResultPopup_Minimise", "Minimise");
            this.label1.Text = SK.Text("GENERIC_Peasants", "Peasants");
            this.label2.Text = SK.Text("GENERIC_Archers", "Archers");
            this.label3.Text = SK.Text("GENERIC_Pikemen", "Pikemen");
            this.label4.Text = SK.Text("GENERIC_Swordsmen", "Swordsmen");
            this.label11.Text = SK.Text("GENERIC_Attackers", "Attackers");
            this.label12.Text = SK.Text("GENERIC_Defenders", "Defenders");
            this.label6.Text = SK.Text("GENERIC_Catapults", "Catapults");
            this.label5.Text = SK.Text("GENERIC_Captains", "Captains");
            this.lblWolves.Text = SK.Text("GENERIC_Wolves", "Wolves");
            this.btnShowResources.Text = SK.Text("GENERIC_Show_Resources", "Show Resources");
            this.lblHonour.Text = SK.Text("GENERIC_Honour", "Honour");
            this.lblSpoils.Text = SK.Text("GENERIC_Honour", "Honour");
            this.lblResult.Text = SK.Text("BattleResultPopup_Defender_won", "The Defender Won");
            base.Title = this.Text = SK.Text("BattleResultPopup_Battle_Results", "Battle Results");
            this.showPostTutorialThingy = false;
            if (returnData == null)
            {
                returnData = new GetReport_ReturnType();
                returnData.successStatus = true;
                returnData.genericData11 = 0x18;
                returnData.reportType = 0x4f;
                returnData.genericData20 = -1;
                returnData.reportTime = DateTime.Now;
                List<int> list = GameEngine.Instance.World.getUserVillageIDList();
                if (list.Count > 0)
                {
                    returnData.defendingVillage = list[0];
                }
                this.showPostTutorialThingy = true;
            }
            m_parent = parent;
            this.m_villageID = villageID;
            this.m_reportReturnData = returnData;
            this.lblAttackersPeasants.Text = endingTroops.numAttackingPeasants.ToString() + "/" + startingTroops.numAttackingPeasants.ToString() + "\n";
            this.lblAttackersArchers.Text = endingTroops.numAttackingArchers.ToString() + "/" + startingTroops.numAttackingArchers.ToString() + "\n";
            this.lblAttackersPikemen.Text = endingTroops.numAttackingPikemen.ToString() + "/" + startingTroops.numAttackingPikemen.ToString() + "\n";
            this.lblAttackersSwordsmen.Text = endingTroops.numAttackingSwordsmen.ToString() + "/" + startingTroops.numAttackingSwordsmen.ToString() + "\n";
            if (((!attackerVictory && (endingTroops.numAttackingPeasants == 0)) && ((endingTroops.numAttackingArchers == 0) && (endingTroops.numAttackingPikemen == 0))) && (endingTroops.numAttackingSwordsmen == 0))
            {
                endingTroops.numAttackingCatapults = 0;
            }
            this.lblAttackersCatapults.Text = endingTroops.numAttackingCatapults.ToString() + "/" + startingTroops.numAttackingCatapults.ToString() + "\n";
            this.lblAttackersCaptains.Text = endingTroops.numAttackingCaptains.ToString() + "/" + startingTroops.numAttackingCaptains.ToString() + "\n";
            this.lblDefendersPeasants.Text = endingTroops.numDefendingPeasants.ToString() + "/" + startingTroops.numDefendingPeasants.ToString() + "\n";
            this.lblDefendersArchers.Text = endingTroops.numDefendingArchers.ToString() + "/" + startingTroops.numDefendingArchers.ToString() + "\n";
            this.lblDefendersPikemen.Text = endingTroops.numDefendingPikemen.ToString() + "/" + startingTroops.numDefendingPikemen.ToString() + "\n";
            this.lblDefendersSwordsmen.Text = endingTroops.numDefendingSwordsmen.ToString() + "/" + startingTroops.numDefendingSwordsmen.ToString();
            this.lblDefendersCaptains.Text = endingTroops.numDefendingCaptains.ToString() + "/" + startingTroops.numDefendingCaptains.ToString();
            if (returnData.reportType == 0x18)
            {
                this.lblVillageName.Text = SK.Text("GENERIC_Bandit_Camp", "Bandit Camp");
                this.lblAttackType.Text = "";
            }
            else if (returnData.reportType == 0x19)
            {
                this.lblVillageName.Text = SK.Text("GENERIC_Wolf_Camp", "Wolf Lair");
                this.lblWolves.Visible = true;
                this.lblDefendersArchers.Visible = false;
                this.lblDefendersPikemen.Visible = false;
                this.lblDefendersSwordsmen.Visible = false;
                this.lblDefendersCaptains.Visible = false;
                this.lblDefendersPeasants.Text = endingTroops.numDefendingSwordsmen.ToString() + "/" + startingTroops.numDefendingSwordsmen.ToString();
                this.lblAttackType.Text = "";
            }
            else if (returnData.reportType == 0x3a)
            {
                this.lblVillageName.Text = SK.Text("GENERIC_Rats_Castle", "Rat's Castle");
                this.lblAttackType.Text = "";
            }
            else if (returnData.reportType == 0x3b)
            {
                this.lblVillageName.Text = SK.Text("GENERIC_Snakes_Castle", "Snake's Castle");
                this.lblAttackType.Text = "";
            }
            else if (returnData.reportType == 60)
            {
                this.lblVillageName.Text = SK.Text("GENERIC_Pigs_Castle", "Pig's Castle");
                this.lblAttackType.Text = "";
            }
            else if (returnData.reportType == 0x3d)
            {
                this.lblVillageName.Text = SK.Text("GENERIC_Wolfs_Castle", "Wolf's Castle");
                this.lblAttackType.Text = "";
            }
            else if (returnData.reportType == 0x7b)
            {
                this.lblVillageName.Text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
                this.lblAttackType.Text = "";
            }
            else if (returnData.reportType == 0x7c)
            {
                this.lblVillageName.Text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
                this.lblAttackType.Text = "";
            }
            else if (returnData.reportType == 0x7d)
            {
                string[] strArray = new string[] { SK.Text("GENERIC_Treasure_Castle", "Treasure Castle"), " ", SK.Text("GENERIC_TREASURE_CASTLE_LEVEL", "Level"), " : ", (returnData.genericData31 + 1).ToString() };
                this.lblVillageName.Text = string.Concat(strArray);
                this.lblAttackType.Text = "";
            }
            else
            {
                this.lblVillageName.Text = GameEngine.Instance.World.getVillageName(villageID);
                this.lblAttackType.Text = CastlesCommon.getAttackTypeLabel(attackType);
            }
            if (returnData == null)
            {
                returnData = new GetReport_ReturnType();
            }
            string otherUser = returnData.otherUser;
            string str2 = "";
            string str3 = SK.Text("GENERIC_The_Attacker_Wins", "The Attacker Wins");
            string str4 = "";
            NumberFormatInfo nFI = GameEngine.NFI;
            string reportAboutUser = returnData.reportAboutUser;
            if ((reportAboutUser == null) || (reportAboutUser.Length == 0))
            {
                reportAboutUser = RemoteServices.Instance.UserName;
            }
            this.btnShowResources.Visible = false;
            switch (returnData.reportType)
            {
                case 1:
                    str2 = reportAboutUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    if (otherUser.Length != 0)
                    {
                        string text1 = otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                        break;
                    }
                    SK.Text("GENERIC_An_Empty_Village", "An empty village");
                    break;

                case 3:
                case 0x3e:
                case 0x3f:
                case 0x40:
                case 0x41:
                case 0x4f:
                    this.lblSpoils.Text = "";
                    this.lblHonour.Text = "";
                    switch (returnData.reportType)
                    {
                        case 0x3e:
                            str2 = SK.Text("GENERIC_CharacterName_Rat", "Rat");
                            break;

                        case 0x3f:
                            str2 = SK.Text("GENERIC_CharacterName_Snake", "Snake");
                            break;

                        case 0x40:
                            str2 = SK.Text("GENERIC_CharacterName_Pig", "Pig");
                            break;

                        case 0x41:
                            str2 = SK.Text("GENERIC_CharacterName_Wolf", "Wolf");
                            break;

                        case 0x4f:
                            str2 = SK.Text("GENERIC_CharacterName_The_Enemy", "The Enemy");
                            break;

                        case 3:
                            if (otherUser.Length == 0)
                            {
                                str2 = SK.Text("GENERIC_An_Unknown_Player", "An Unknown Player");
                            }
                            else
                            {
                                str2 = otherUser;
                            }
                            str2 = str2 + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                            break;
                    }
                    if (returnData.genericData11 < 0)
                    {
                        this.lblHonour.Text = SK.Text("GENERIC_Honour_Cost", "Honour Cost") + " : " + returnData.genericData11.ToString("N", nFI);
                    }
                    else
                    {
                        this.lblHonour.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData11.ToString("N", nFI);
                    }
                    if (otherUser.Length == 0)
                    {
                        SK.Text("GENERIC_An_Empty_Village", "An empty village");
                    }
                    else
                    {
                        string text2 = reportAboutUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    }
                    if (returnData.successStatus)
                    {
                        str3 = SK.Text("GENERIC_The_Defender_Wins", "The Defender Wins");
                    }
                    else if ((returnData.genericData20 == 0) && (returnData.genericData21 >= 0))
                    {
                        if ((returnData.genericData30 == 11) || (returnData.genericData30 == 13))
                        {
                            str4 = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Was_Attacked", "Was attacked");
                        }
                        else
                        {
                            str4 = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Captured", "Captured");
                        }
                    }
                    else if ((returnData.genericData20 == 10) && (returnData.genericData21 >= 0))
                    {
                        str4 = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Made_a_Vassal", "Made a vassal");
                    }
                    else if ((returnData.genericData20 == 5) && (returnData.genericData21 >= 0))
                    {
                        str4 = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Has_Been_Razed", "Has been razed");
                    }
                    else if (returnData.genericData20 == 6)
                    {
                        str4 = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Has_Been_Raided", "Has been raided");
                        this.lblSpoils.Text = returnData.genericData21.ToString("N", nFI) + " " + SK.Text("GENERIC_Gold_Raided", "Gold raided");
                    }
                    else
                    {
                        str4 = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Was_Attacked", "Was attacked");
                    }
                    if (returnData.genericData20 == 2)
                    {
                        this.lblSpoils.Text = ((((((((returnData.genericData22 + returnData.genericData23) + returnData.genericData24) + returnData.genericData25) + returnData.genericData26) + returnData.genericData27) + returnData.genericData28) + returnData.genericData29)).ToString("N", nFI) + " " + SK.Text("GENERIC_Resources_Lost", "Resources lost");
                        this.btnShowResources.Visible = true;
                    }
                    else if (returnData.genericData20 > 0x3e8)
                    {
                        if (returnData.genericData22 >= 0)
                        {
                            this.lblSpoils.Text = VillageBuildingsData.getBuildingName(returnData.genericData22);
                        }
                        if (returnData.genericData23 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData23);
                        }
                        if (returnData.genericData24 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData24);
                        }
                        if (returnData.genericData25 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData25);
                        }
                        if (returnData.genericData26 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData26);
                        }
                        if (returnData.genericData27 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData27);
                        }
                        if (returnData.genericData28 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData28);
                        }
                        if (returnData.genericData29 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData29);
                        }
                        this.lblSpoils.Text = this.lblSpoils.Text + " - " + SK.Text("GENERIC_Destroyed", "Destroyed");
                    }
                    else if (returnData.genericData20 == 0x3e8)
                    {
                        this.lblSpoils.Text = SK.Text("GENERIC_You_Had_No_buildings_Destroyed", "You had no buildings that could be destroyed.");
                    }
                    else if (returnData.genericData20 == 1)
                    {
                        this.lblSpoils.Text = SK.Text("GENERIC_The_Attack_Failed", "The attack failed.");
                    }
                    goto Label_16A2;

                case 0x18:
                case 0x19:
                case 0x3a:
                case 0x3b:
                case 60:
                case 0x3d:
                case 0x7b:
                case 0x7c:
                case 0x7d:
                    str2 = reportAboutUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    switch (returnData.reportType)
                    {
                        case 0x18:
                            SK.Text("GENERIC_A_Bandit_Camp", "A Bandit Camp");
                            break;

                        case 0x19:
                            SK.Text("GENERIC_A_Wolf_Lair", "A Wolf Lair");
                            break;

                        case 0x3a:
                            SK.Text("GENERIC_Rats_Castle", "Rat's Castle");
                            break;

                        case 0x3b:
                            SK.Text("GENERIC_Snakes_Castle", "Snake's Castle");
                            break;

                        case 60:
                            SK.Text("GENERIC_Pigs_Castle", "Pig's Castle");
                            break;

                        case 0x3d:
                            SK.Text("GENERIC_Wolfs_Castle", "Wolf's Castle");
                            break;

                        case 0x7b:
                            SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
                            break;

                        case 0x7c:
                            SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
                            break;

                        case 0x7d:
                            SK.Text("GENERIC_Treasure_Castle", "Treasure Castle");
                            break;
                    }
                    this.lblSpoils.Text = "";
                    this.lblHonour.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData11.ToString("N", nFI);
                    if (!returnData.successStatus)
                    {
                        str3 = SK.Text("GENERIC_The_Defender_Wins", "The Defender Wins");
                    }
                    else if (((returnData.reportType == 0x7d) && (returnData.genericData20 >= 700)) && (returnData.genericData20 < 710))
                    {
                        this.lblHonour.Text = "";
                        switch (returnData.genericData20)
                        {
                            case 700:
                                str4 = SK.Text("REPORTS_TreasureWheelSpins1", "Treasure Found : Tier 1 Wheel Spin");
                                break;

                            case 0x2bd:
                                str4 = SK.Text("REPORTS_TreasureWheelSpins2", "Treasure Found : Tier 2 Wheel Spin");
                                break;

                            case 0x2be:
                                str4 = SK.Text("REPORTS_TreasureWheelSpins3", "Treasure Found : Tier 3 Wheel Spin");
                                break;

                            case 0x2bf:
                                str4 = SK.Text("REPORTS_TreasureWheelSpins4", "Treasure Found : Tier 4 Wheel Spin");
                                break;

                            case 0x2c0:
                                str4 = SK.Text("REPORTS_TreasureWheelSpins5", "Treasure Found : Tier 5 Wheel Spin");
                                break;
                        }
                    }
                    if (returnData.genericData21 == 1)
                    {
                    }
                    goto Label_16A2;

                default:
                    goto Label_16A2;
            }
            this.lblSpoils.Text = "";
            if (!returnData.successStatus)
            {
                str3 = SK.Text("GENERIC_The_Defender_Wins", "The Defender Wins");
            }
            else if ((returnData.genericData20 == 0) && (returnData.genericData21 >= 0))
            {
                if ((returnData.genericData30 == 11) || (returnData.genericData30 == 13))
                {
                    str4 = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Successfully_Attacked", "Successfully attacked");
                }
                else
                {
                    str4 = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Captured", "Captured");
                }
            }
            else if ((returnData.genericData20 == 10) && (returnData.genericData21 >= 0))
            {
                str4 = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Made_a_Vassal", "Made a vassal");
            }
            else if ((returnData.genericData20 == 5) && (returnData.genericData21 >= 0))
            {
                str4 = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Has_Been_Razed", "Has been razed");
            }
            else if (returnData.genericData20 == 6)
            {
                str4 = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Has_Been_Raided", "Has been raided");
                this.lblSpoils.Text = returnData.genericData21.ToString("N", nFI) + " " + SK.Text("GENERIC_Gold_Raided", "Gold raided");
            }
            else
            {
                str4 = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Successfully_Attacked", "Successfully attacked");
            }
            if (returnData.genericData11 < 0)
            {
                this.lblHonour.Text = SK.Text("GENERIC_Honour_Cost", "Honour Cost") + " : " + returnData.genericData11.ToString("N", nFI);
            }
            else
            {
                this.lblHonour.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData11.ToString("N", nFI);
            }
            if (returnData.genericData20 != 0)
            {
                if (returnData.genericData20 == 2)
                {
                    this.lblSpoils.Text = ((((((((returnData.genericData22 + returnData.genericData23) + returnData.genericData24) + returnData.genericData25) + returnData.genericData26) + returnData.genericData27) + returnData.genericData28) + returnData.genericData29)).ToString("N", nFI) + " " + SK.Text("GENERIC_Resources_Taken", "Resources taken");
                    this.btnShowResources.Visible = true;
                }
                else if (returnData.genericData20 > 0x3e8)
                {
                    if (returnData.genericData22 >= 0)
                    {
                        this.lblSpoils.Text = VillageBuildingsData.getBuildingName(returnData.genericData22);
                    }
                    if (returnData.genericData23 >= 0)
                    {
                        this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData23);
                    }
                    if (returnData.genericData24 >= 0)
                    {
                        this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData24);
                    }
                    if (returnData.genericData25 >= 0)
                    {
                        this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData25);
                    }
                    if (returnData.genericData26 >= 0)
                    {
                        this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData26);
                    }
                    if (returnData.genericData27 >= 0)
                    {
                        this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData27);
                    }
                    if (returnData.genericData28 >= 0)
                    {
                        this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData28);
                    }
                    if (returnData.genericData29 >= 0)
                    {
                        this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData29);
                    }
                    this.lblSpoils.Text = this.lblSpoils.Text + " - " + SK.Text("GENERIC_Destroyed", "Destroyed");
                }
                else if (returnData.genericData20 == 0x3e8)
                {
                    this.lblSpoils.Text = SK.Text("GENERIC_No_Destroyable_Buildings", "There were no destroyable buildings.");
                }
                else if (returnData.genericData20 == 1)
                {
                    this.lblSpoils.Text = SK.Text("GENERIC_Attack_Failed", "This attack failed.");
                }
            }
        Label_16A2:
            this.lblResult.Text = str3;
            this.lblDate.Text = returnData.reportTime.ToString();
            this.lblTargetVillageNameAndInfo.Text = str4;
            this.lblDate.Text = this.m_reportReturnData.reportTime.ToString();
        }

        private void InitializeComponent()
        {
            this.lblVillageName = new Label();
            this.lblAttackType = new Label();
            this.btnClose = new BitmapButton();
            this.btnMinimise = new BitmapButton();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.lblAttackersSwordsmen = new Label();
            this.lblAttackersPikemen = new Label();
            this.lblAttackersArchers = new Label();
            this.lblAttackersPeasants = new Label();
            this.label11 = new Label();
            this.label12 = new Label();
            this.lblDefendersSwordsmen = new Label();
            this.lblDefendersPikemen = new Label();
            this.lblDefendersArchers = new Label();
            this.lblDefendersPeasants = new Label();
            this.lblDefendersCaptains = new Label();
            this.label5 = new Label();
            this.lblAttackersCaptains = new Label();
            this.label6 = new Label();
            this.lblAttackersCatapults = new Label();
            this.lblWolves = new Label();
            this.btnShowResources = new BitmapButton();
            this.lblHonour = new Label();
            this.lblSpoils = new Label();
            this.lblTargetVillageNameAndInfo = new Label();
            this.lblDate = new Label();
            this.lblResult = new Label();
            base.SuspendLayout();
            this.lblVillageName.AutoSize = true;
            this.lblVillageName.BackColor = ARGBColors.Transparent;
            this.lblVillageName.ForeColor = ARGBColors.White;
            this.lblVillageName.Location = new Point(0x19, 40);
            this.lblVillageName.Name = "lblVillageName";
            this.lblVillageName.Size = new Size(this.sizeAddition + 0x45, 13);
            this.lblVillageName.TabIndex = 0;
            this.lblVillageName.Text = "Village Name";
            this.lblAttackType.AutoSize = true;
            this.lblAttackType.BackColor = ARGBColors.Transparent;
            this.lblAttackType.ForeColor = ARGBColors.White;
            this.lblAttackType.Location = new Point(0x19, 0x49);
            this.lblAttackType.Name = "lblAttackType";
            this.lblAttackType.Size = new Size(this.sizeAddition + 0x41, 13);
            this.lblAttackType.TabIndex = 1;
            this.lblAttackType.Text = "Attack Type";
            this.btnClose.Anchor = AnchorStyles.Bottom;
            this.btnClose.BackColor = ARGBColors.Transparent;
            this.btnClose.BorderColor = ARGBColors.DarkBlue;
            this.btnClose.BorderDrawing = true;
            this.btnClose.FocusRectangleEnabled = false;
            this.btnClose.Image = null;
            this.btnClose.ImageBorderColor = ARGBColors.Chocolate;
            this.btnClose.ImageBorderEnabled = true;
            this.btnClose.ImageDropShadow = true;
            this.btnClose.ImageFocused = null;
            this.btnClose.ImageInactive = null;
            this.btnClose.ImageMouseOver = null;
            this.btnClose.ImageNormal = null;
            this.btnClose.ImagePressed = null;
            this.btnClose.InnerBorderColor = ARGBColors.LightGray;
            this.btnClose.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnClose.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnClose.Location = new Point(0x19e, 0x1cb);
            this.btnClose.Name = "btnClose";
            this.btnClose.OffsetPressedContent = true;
            this.btnClose.Padding2 = 5;
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.StretchImage = false;
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.TextDropShadow = false;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.btnMinimise.Anchor = AnchorStyles.Bottom;
            this.btnMinimise.BackColor = ARGBColors.Transparent;
            this.btnMinimise.BorderColor = ARGBColors.DarkBlue;
            this.btnMinimise.BorderDrawing = true;
            this.btnMinimise.FocusRectangleEnabled = false;
            this.btnMinimise.Image = null;
            this.btnMinimise.ImageBorderColor = ARGBColors.Chocolate;
            this.btnMinimise.ImageBorderEnabled = true;
            this.btnMinimise.ImageDropShadow = true;
            this.btnMinimise.ImageFocused = null;
            this.btnMinimise.ImageInactive = null;
            this.btnMinimise.ImageMouseOver = null;
            this.btnMinimise.ImageNormal = null;
            this.btnMinimise.ImagePressed = null;
            this.btnMinimise.InnerBorderColor = ARGBColors.LightGray;
            this.btnMinimise.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnMinimise.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnMinimise.Location = new Point(0x14d, 0x1cb);
            this.btnMinimise.Name = "btnMinimise";
            this.btnMinimise.OffsetPressedContent = true;
            this.btnMinimise.Padding2 = 5;
            this.btnMinimise.Size = new Size(0x4b, 0x17);
            this.btnMinimise.StretchImage = false;
            this.btnMinimise.TabIndex = 3;
            this.btnMinimise.Text = "Minimise";
            this.btnMinimise.TextDropShadow = false;
            this.btnMinimise.UseVisualStyleBackColor = false;
            this.btnMinimise.Click += new EventHandler(this.btnMinimise_Click);
            this.label1.AutoSize = true;
            this.label1.BackColor = ARGBColors.Transparent;
            this.label1.ForeColor = ARGBColors.White;
            this.label1.Location = new Point(0x19, 0x80);
            this.label1.Name = "label1";
            this.label1.Size = new Size(this.sizeAddition + 0x33, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Peasants";
            this.label2.AutoSize = true;
            this.label2.BackColor = ARGBColors.Transparent;
            this.label2.ForeColor = ARGBColors.White;
            this.label2.Location = new Point(0x19, 0x9e);
            this.label2.Name = "label2";
            this.label2.Size = new Size(this.sizeAddition + 0x2b, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Archers";
            this.label3.AutoSize = true;
            this.label3.BackColor = ARGBColors.Transparent;
            this.label3.ForeColor = ARGBColors.White;
            this.label3.Location = new Point(0x19, 0xbc);
            this.label3.Name = "label3";
            this.label3.Size = new Size(this.sizeAddition + 0x30, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Pikemen";
            this.label4.AutoSize = true;
            this.label4.BackColor = ARGBColors.Transparent;
            this.label4.ForeColor = ARGBColors.White;
            this.label4.Location = new Point(0x19, 0xda);
            this.label4.Name = "label4";
            this.label4.Size = new Size(this.sizeAddition + 0x3e, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Swordsmen";
            this.lblAttackersSwordsmen.BackColor = ARGBColors.Transparent;
            this.lblAttackersSwordsmen.ForeColor = ARGBColors.White;
            this.lblAttackersSwordsmen.Location = new Point(0x87, 0xda);
            this.lblAttackersSwordsmen.Name = "lblAttackersSwordsmen";
            this.lblAttackersSwordsmen.Size = new Size(this.sizeAddition + 0x4a, 13);
            this.lblAttackersSwordsmen.TabIndex = 12;
            this.lblAttackersSwordsmen.Text = "0";
            this.lblAttackersSwordsmen.TextAlign = ContentAlignment.TopRight;
            this.lblAttackersPikemen.BackColor = ARGBColors.Transparent;
            this.lblAttackersPikemen.ForeColor = ARGBColors.White;
            this.lblAttackersPikemen.Location = new Point(0x87, 0xbc);
            this.lblAttackersPikemen.Name = "lblAttackersPikemen";
            this.lblAttackersPikemen.Size = new Size(this.sizeAddition + 0x4a, 13);
            this.lblAttackersPikemen.TabIndex = 11;
            this.lblAttackersPikemen.Text = "0";
            this.lblAttackersPikemen.TextAlign = ContentAlignment.TopRight;
            this.lblAttackersArchers.BackColor = ARGBColors.Transparent;
            this.lblAttackersArchers.ForeColor = ARGBColors.White;
            this.lblAttackersArchers.Location = new Point(0x87, 0x9e);
            this.lblAttackersArchers.Name = "lblAttackersArchers";
            this.lblAttackersArchers.Size = new Size(this.sizeAddition + 0x4a, 13);
            this.lblAttackersArchers.TabIndex = 10;
            this.lblAttackersArchers.Text = "0";
            this.lblAttackersArchers.TextAlign = ContentAlignment.TopRight;
            this.lblAttackersPeasants.BackColor = ARGBColors.Transparent;
            this.lblAttackersPeasants.ForeColor = ARGBColors.White;
            this.lblAttackersPeasants.Location = new Point(0x87, 0x80);
            this.lblAttackersPeasants.Name = "lblAttackersPeasants";
            this.lblAttackersPeasants.Size = new Size(this.sizeAddition + 0x4a, 13);
            this.lblAttackersPeasants.TabIndex = 9;
            this.lblAttackersPeasants.Text = "0";
            this.lblAttackersPeasants.TextAlign = ContentAlignment.TopRight;
            this.label11.AutoSize = true;
            this.label11.BackColor = ARGBColors.Transparent;
            this.label11.ForeColor = ARGBColors.White;
            this.label11.Location = new Point(0x9e, 0x67);
            this.label11.Name = "label11";
            this.label11.Size = new Size(this.sizeAddition + 0x34, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Attackers";
            this.label12.AutoSize = true;
            this.label12.BackColor = ARGBColors.Transparent;
            this.label12.ForeColor = ARGBColors.White;
            this.label12.Location = new Point(330, 0x67);
            this.label12.Name = "label12";
            this.label12.Size = new Size(this.sizeAddition + 0x38, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Defenders";
            this.lblDefendersSwordsmen.BackColor = ARGBColors.Transparent;
            this.lblDefendersSwordsmen.ForeColor = ARGBColors.White;
            this.lblDefendersSwordsmen.Location = new Point(0x133, 0xda);
            this.lblDefendersSwordsmen.Name = "lblDefendersSwordsmen";
            this.lblDefendersSwordsmen.Size = new Size(this.sizeAddition + 0x4a, 13);
            this.lblDefendersSwordsmen.TabIndex = 0x12;
            this.lblDefendersSwordsmen.Text = "0";
            this.lblDefendersSwordsmen.TextAlign = ContentAlignment.TopRight;
            this.lblDefendersPikemen.BackColor = ARGBColors.Transparent;
            this.lblDefendersPikemen.ForeColor = ARGBColors.White;
            this.lblDefendersPikemen.Location = new Point(0x133, 0xbc);
            this.lblDefendersPikemen.Name = "lblDefendersPikemen";
            this.lblDefendersPikemen.Size = new Size(this.sizeAddition + 0x4a, 13);
            this.lblDefendersPikemen.TabIndex = 0x11;
            this.lblDefendersPikemen.Text = "0";
            this.lblDefendersPikemen.TextAlign = ContentAlignment.TopRight;
            this.lblDefendersArchers.BackColor = ARGBColors.Transparent;
            this.lblDefendersArchers.ForeColor = ARGBColors.White;
            this.lblDefendersArchers.Location = new Point(0x133, 0x9e);
            this.lblDefendersArchers.Name = "lblDefendersArchers";
            this.lblDefendersArchers.Size = new Size(this.sizeAddition + 0x4a, 13);
            this.lblDefendersArchers.TabIndex = 0x10;
            this.lblDefendersArchers.Text = "0";
            this.lblDefendersArchers.TextAlign = ContentAlignment.TopRight;
            this.lblDefendersPeasants.BackColor = ARGBColors.Transparent;
            this.lblDefendersPeasants.ForeColor = ARGBColors.White;
            this.lblDefendersPeasants.Location = new Point(0x133, 0x80);
            this.lblDefendersPeasants.Name = "lblDefendersPeasants";
            this.lblDefendersPeasants.Size = new Size(this.sizeAddition + 0x4a, 13);
            this.lblDefendersPeasants.TabIndex = 15;
            this.lblDefendersPeasants.Text = "0";
            this.lblDefendersPeasants.TextAlign = ContentAlignment.TopRight;
            this.lblDefendersCaptains.BackColor = ARGBColors.Transparent;
            this.lblDefendersCaptains.ForeColor = ARGBColors.White;
            this.lblDefendersCaptains.Location = new Point(0x133, 0x115);
            this.lblDefendersCaptains.Name = "lblDefendersCaptains";
            this.lblDefendersCaptains.Size = new Size(this.sizeAddition + 0x4a, 13);
            this.lblDefendersCaptains.TabIndex = 0x2c;
            this.lblDefendersCaptains.Text = "0";
            this.lblDefendersCaptains.TextAlign = ContentAlignment.TopRight;
            this.label5.AutoSize = true;
            this.label5.BackColor = ARGBColors.Transparent;
            this.label5.ForeColor = ARGBColors.White;
            this.label5.Location = new Point(0x19, 0x115);
            this.label5.Name = "label5";
            this.label5.Size = new Size(this.sizeAddition + 0x30, 13);
            this.label5.TabIndex = 0x2a;
            this.label5.Text = "Captains";
            this.lblAttackersCaptains.BackColor = ARGBColors.Transparent;
            this.lblAttackersCaptains.ForeColor = ARGBColors.White;
            this.lblAttackersCaptains.Location = new Point(0x87, 0x115);
            this.lblAttackersCaptains.Name = "lblAttackersCaptains";
            this.lblAttackersCaptains.Size = new Size(this.sizeAddition + 0x4a, 13);
            this.lblAttackersCaptains.TabIndex = 0x2b;
            this.lblAttackersCaptains.Text = "0";
            this.lblAttackersCaptains.TextAlign = ContentAlignment.TopRight;
            this.label6.AutoSize = true;
            this.label6.BackColor = ARGBColors.Transparent;
            this.label6.ForeColor = ARGBColors.White;
            this.label6.Location = new Point(0x19, 0xf8);
            this.label6.Name = "label6";
            this.label6.Size = new Size(this.sizeAddition + 0x33, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Catapults";
            this.lblAttackersCatapults.BackColor = ARGBColors.Transparent;
            this.lblAttackersCatapults.ForeColor = ARGBColors.White;
            this.lblAttackersCatapults.Location = new Point(0x87, 0xf8);
            this.lblAttackersCatapults.Name = "lblAttackersCatapults";
            this.lblAttackersCatapults.Size = new Size(this.sizeAddition + 0x4a, 13);
            this.lblAttackersCatapults.TabIndex = 0x29;
            this.lblAttackersCatapults.Text = "0";
            this.lblAttackersCatapults.TextAlign = ContentAlignment.TopRight;
            this.lblWolves.AutoSize = true;
            this.lblWolves.BackColor = ARGBColors.Transparent;
            this.lblWolves.ForeColor = ARGBColors.White;
            this.lblWolves.Location = new Point(0x10a, 0x80);
            this.lblWolves.Name = "lblWolves";
            this.lblWolves.Size = new Size(this.sizeAddition + 0x2b, 13);
            this.lblWolves.TabIndex = 0x27;
            this.lblWolves.Text = "Wolves";
            this.lblWolves.Visible = false;
            this.btnShowResources.Anchor = AnchorStyles.Bottom;
            this.btnShowResources.BackColor = ARGBColors.Transparent;
            this.btnShowResources.BorderColor = ARGBColors.DarkBlue;
            this.btnShowResources.BorderDrawing = true;
            this.btnShowResources.FocusRectangleEnabled = false;
            this.btnShowResources.Image = null;
            this.btnShowResources.ImageBorderColor = ARGBColors.Chocolate;
            this.btnShowResources.ImageBorderEnabled = true;
            this.btnShowResources.ImageDropShadow = true;
            this.btnShowResources.ImageFocused = null;
            this.btnShowResources.ImageInactive = null;
            this.btnShowResources.ImageMouseOver = null;
            this.btnShowResources.ImageNormal = null;
            this.btnShowResources.ImagePressed = null;
            this.btnShowResources.InnerBorderColor = ARGBColors.LightGray;
            this.btnShowResources.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnShowResources.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnShowResources.Location = new Point(0x14d, 0x175);
            this.btnShowResources.Name = "btnShowResources";
            this.btnShowResources.OffsetPressedContent = true;
            this.btnShowResources.Padding2 = 5;
            this.btnShowResources.Size = new Size(this.sizeAddition + 0x9d, 0x17);
            this.btnShowResources.StretchImage = false;
            this.btnShowResources.TabIndex = 0x26;
            this.btnShowResources.Text = "Show Resources";
            this.btnShowResources.TextDropShadow = false;
            this.btnShowResources.UseVisualStyleBackColor = false;
            this.btnShowResources.Click += new EventHandler(this.btnShowResources_Click);
            this.lblHonour.Anchor = AnchorStyles.Bottom;
            this.lblHonour.BackColor = ARGBColors.Transparent;
            this.lblHonour.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblHonour.ForeColor = ARGBColors.White;
            this.lblHonour.Location = new Point(0x12, 0x1b5);
            this.lblHonour.Name = "lblHonour";
            this.lblHonour.Size = new Size(this.sizeAddition + 0x1d8, 20);
            this.lblHonour.TabIndex = 0x25;
            this.lblHonour.Text = "Honour";
            this.lblHonour.TextAlign = ContentAlignment.MiddleCenter;
            this.lblSpoils.Anchor = AnchorStyles.Bottom;
            this.lblSpoils.BackColor = ARGBColors.Transparent;
            this.lblSpoils.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblSpoils.ForeColor = ARGBColors.White;
            this.lblSpoils.Location = new Point(0x12, 0x18d);
            this.lblSpoils.Name = "lblSpoils";
            this.lblSpoils.Size = new Size(this.sizeAddition + 0x1d8, 20);
            this.lblSpoils.TabIndex = 0x24;
            this.lblSpoils.Text = "Honour";
            this.lblSpoils.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTargetVillageNameAndInfo.Anchor = AnchorStyles.Bottom;
            this.lblTargetVillageNameAndInfo.BackColor = ARGBColors.Transparent;
            this.lblTargetVillageNameAndInfo.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblTargetVillageNameAndInfo.ForeColor = ARGBColors.White;
            this.lblTargetVillageNameAndInfo.Location = new Point(0x12, 0x1a1);
            this.lblTargetVillageNameAndInfo.Name = "lblTargetVillageNameAndInfo";
            this.lblTargetVillageNameAndInfo.Size = new Size(this.sizeAddition + 0x1d8, 20);
            this.lblTargetVillageNameAndInfo.TabIndex = 0x23;
            this.lblTargetVillageNameAndInfo.Text = "Lordwibble took 0 villages";
            this.lblTargetVillageNameAndInfo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblDate.Anchor = AnchorStyles.Bottom;
            this.lblDate.BackColor = ARGBColors.Transparent;
            this.lblDate.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblDate.ForeColor = ARGBColors.White;
            this.lblDate.Location = new Point(0x11, 320);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new Size(this.sizeAddition + 0x1d8, 20);
            this.lblDate.TabIndex = 0x22;
            this.lblDate.Text = "06/12/2004 15:00";
            this.lblDate.TextAlign = ContentAlignment.MiddleCenter;
            this.lblResult.Anchor = AnchorStyles.Bottom;
            this.lblResult.BackColor = ARGBColors.Transparent;
            this.lblResult.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblResult.ForeColor = ARGBColors.White;
            this.lblResult.Location = new Point(0x11, 0x15d);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new Size(this.sizeAddition + 0x1d8, 20);
            this.lblResult.TabIndex = 0x21;
            this.lblResult.Text = "The Defender Won";
            this.lblResult.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            base.ClientSize = new Size(this.sizeAddition + 0x1f7, 0x1f0);
            base.Controls.Add(this.lblDefendersCaptains);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.lblAttackersCaptains);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.lblAttackersCatapults);
            base.Controls.Add(this.lblWolves);
            base.Controls.Add(this.btnShowResources);
            base.Controls.Add(this.lblHonour);
            base.Controls.Add(this.lblSpoils);
            base.Controls.Add(this.lblTargetVillageNameAndInfo);
            base.Controls.Add(this.lblDate);
            base.Controls.Add(this.lblResult);
            base.Controls.Add(this.lblVillageName);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnMinimise);
            base.Controls.Add(this.label12);
            base.Controls.Add(this.lblAttackType);
            base.Controls.Add(this.lblDefendersSwordsmen);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.lblDefendersPikemen);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.lblDefendersArchers);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.lblDefendersPeasants);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label11);
            base.Controls.Add(this.lblAttackersPeasants);
            base.Controls.Add(this.lblAttackersSwordsmen);
            base.Controls.Add(this.lblAttackersArchers);
            base.Controls.Add(this.lblAttackersPikemen);
            base.Icon = Resources.shk_icon;
            base.Name = "BattleResultPopup";
            base.ShowClose = true;
            this.Text = "Battle Results";
            base.Load += new EventHandler(this.BattleResultPopup_Load);
            base.Controls.SetChildIndex(this.lblAttackersPikemen, 0);
            base.Controls.SetChildIndex(this.lblAttackersArchers, 0);
            base.Controls.SetChildIndex(this.lblAttackersSwordsmen, 0);
            base.Controls.SetChildIndex(this.lblAttackersPeasants, 0);
            base.Controls.SetChildIndex(this.label11, 0);
            base.Controls.SetChildIndex(this.label4, 0);
            base.Controls.SetChildIndex(this.lblDefendersPeasants, 0);
            base.Controls.SetChildIndex(this.label3, 0);
            base.Controls.SetChildIndex(this.lblDefendersArchers, 0);
            base.Controls.SetChildIndex(this.label2, 0);
            base.Controls.SetChildIndex(this.lblDefendersPikemen, 0);
            base.Controls.SetChildIndex(this.label1, 0);
            base.Controls.SetChildIndex(this.lblDefendersSwordsmen, 0);
            base.Controls.SetChildIndex(this.lblAttackType, 0);
            base.Controls.SetChildIndex(this.label12, 0);
            base.Controls.SetChildIndex(this.btnMinimise, 0);
            base.Controls.SetChildIndex(this.btnClose, 0);
            base.Controls.SetChildIndex(this.lblVillageName, 0);
            base.Controls.SetChildIndex(this.lblResult, 0);
            base.Controls.SetChildIndex(this.lblDate, 0);
            base.Controls.SetChildIndex(this.lblTargetVillageNameAndInfo, 0);
            base.Controls.SetChildIndex(this.lblSpoils, 0);
            base.Controls.SetChildIndex(this.lblHonour, 0);
            base.Controls.SetChildIndex(this.btnShowResources, 0);
            base.Controls.SetChildIndex(this.lblWolves, 0);
            base.Controls.SetChildIndex(this.lblAttackersCatapults, 0);
            base.Controls.SetChildIndex(this.label6, 0);
            base.Controls.SetChildIndex(this.lblAttackersCaptains, 0);
            base.Controls.SetChildIndex(this.label5, 0);
            base.Controls.SetChildIndex(this.lblDefendersCaptains, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

