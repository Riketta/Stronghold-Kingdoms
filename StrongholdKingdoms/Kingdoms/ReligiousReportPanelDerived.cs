namespace Kingdoms
{
    using CommonTypes;
    using System;

    internal class ReligiousReportPanelDerived : GenericReportPanelBasic
    {
        private void initFurtherInfo()
        {
            string str2;
            switch (base.m_returnData.reportType)
            {
                case 0x42:
                    if (base.m_returnData.genericData1 <= 0)
                    {
                        if (base.m_returnData.genericData1 != -1)
                        {
                            string[] strArray9 = new string[] { SK.Text("Reports_Votes_Lost", "Votes Lost"), " : ", base.m_returnData.otherUser, " (", base.m_returnData.genericData1.ToString(), " ", SK.Text("Reports_Votes", "Votes"), ")" };
                            base.lblFurther.Text = string.Concat(strArray9);
                        }
                        else
                        {
                            base.lblFurther.Text = SK.Text("Reports_Votes_Lost", "Votes Lost") + " : " + base.m_returnData.otherUser + " (" + SK.Text("Reports_1_Vote", "1 Vote") + ")";
                        }
                    }
                    else if (base.m_returnData.genericData1 == 1)
                    {
                        base.lblFurther.Text = SK.Text("Reports_Votes_Given", "Votes Given") + " : " + base.m_returnData.otherUser + " (" + SK.Text("Reports_1_Vote", "1 Vote") + ")";
                    }
                    else
                    {
                        base.lblFurther.Text = SK.Text("Reports_Votes_Given", "Votes Given") + " : " + base.m_returnData.otherUser + " (" + base.m_returnData.genericData1.ToString() + " " + SK.Text("Reports_Votes", "Votes") + ")";
                    }
                    goto Label_088D;

                case 0x43:
                    base.lblFurther.Text = SK.Text("Reports_Disease_Points_Removed", "Disease points removed") + " : " + base.m_returnData.genericData1.ToString();
                    base.lblFurther.Text = base.lblFurther.Text + Environment.NewLine + Environment.NewLine;
                    if (base.m_returnData.genericData1 > 0)
                    {
                        base.lblFurther.Text = base.lblFurther.Text + SK.Text("GENERIC_Honour", "Honour") + " : " + ((base.m_returnData.genericData1 * GameEngine.Instance.LocalWorldData.HonourForClearingDisease)).ToString("N", base.nfi);
                    }
                    goto Label_088D;

                case 0x44:
                case 0x69:
                    if (base.m_returnData.genericData1 != 1)
                    {
                        base.lblFurther.Text = SK.Text("Reports_Protection", "Protection") + " : " + base.m_returnData.genericData1.ToString() + " " + SK.Text("Reports_Hours", "hours");
                    }
                    else
                    {
                        base.lblFurther.Text = SK.Text("Reports_Protection", "Protection") + " : " + SK.Text("Reports_1_hour", "1 hour");
                    }
                    goto Label_088D;

                case 0x45:
                case 0x68:
                    if (base.m_returnData.genericData2 == 10)
                    {
                        base.lblFurther.Text = SK.Text("Reports_Popularity_Penalty", "Popularity Penalty") + " : " + base.m_returnData.genericData1.ToString() + " (" + SK.Text("Reports_1_hour", "1 hour") + ")";
                    }
                    else
                    {
                        string str3 = "";
                        if ((base.m_returnData.genericData2 % 10) == 0)
                        {
                            str3 = (base.m_returnData.genericData2 / 10).ToString();
                        }
                        else
                        {
                            str3 = (((double) base.m_returnData.genericData2) / 10.0).ToString();
                        }
                        base.lblFurther.Text = SK.Text("Reports_Popularity_Penalty", "Popularity Penalty") + " : " + base.m_returnData.genericData1.ToString() + " (" + str3 + " " + SK.Text("Reports_Hours", "hours") + ")";
                    }
                    goto Label_088D;

                case 70:
                case 0x5b:
                    if (base.m_returnData.genericData1 == 10)
                    {
                        base.lblFurther.Text = SK.Text("Reports_Excommunication", "Excommunication") + " : " + SK.Text("Reports_1_hour", "1 hour");
                    }
                    else
                    {
                        string str4 = "";
                        if ((base.m_returnData.genericData1 % 10) == 0)
                        {
                            str4 = (base.m_returnData.genericData1 / 10).ToString();
                        }
                        else
                        {
                            str4 = (((double) base.m_returnData.genericData1) / 10.0).ToString();
                        }
                        base.lblFurther.Text = SK.Text("Reports_Excommunication", "Excommunication") + " : " + str4 + " " + SK.Text("Reports_Hours", "hours");
                    }
                    goto Label_088D;

                case 0x47:
                case 0x67:
                    if (base.m_returnData.genericData1 == 10)
                    {
                        base.lblFurther.Text = SK.Text("Reports_Absolution", "Absolution") + " : " + SK.Text("Reports_1_hour", "1 hour");
                    }
                    else
                    {
                        string str = "";
                        if ((base.m_returnData.genericData1 % 10) == 0)
                        {
                            str = (base.m_returnData.genericData1 / 10).ToString();
                        }
                        else
                        {
                            str = (((double) base.m_returnData.genericData1) / 10.0).ToString();
                        }
                        base.lblFurther.Text = SK.Text("Reports_Absolution", "Absolution") + " : " + str + " " + SK.Text("Reports_Hours", "hours");
                    }
                    goto Label_088D;

                case 0x48:
                    if (base.m_returnData.genericData2 == 10)
                    {
                        base.lblFurther.Text = SK.Text("Reports_Popularity_Bonus", "Popularity Bonus") + " : " + base.m_returnData.genericData1.ToString() + " (" + SK.Text("Reports_1_hour", "1 hour") + ")";
                        goto Label_088D;
                    }
                    str2 = "";
                    if ((base.m_returnData.genericData2 % 10) != 0)
                    {
                        str2 = (((double) base.m_returnData.genericData2) / 10.0).ToString();
                        break;
                    }
                    str2 = (base.m_returnData.genericData2 / 10).ToString();
                    break;

                default:
                    goto Label_088D;
            }
            base.lblFurther.Text = SK.Text("Reports_Popularity_Bonus", "Popularity Bonus") + " : " + base.m_returnData.genericData1.ToString() + " (" + str2 + " " + SK.Text("Reports_Hours", "hours") + ")";
        Label_088D:
            base.showFurtherInfo();
        }

        public override void setData(GetReport_ReturnType returnData)
        {
            base.setData(returnData);
            switch (returnData.reportType)
            {
                case 0x42:
                    base.lblMainText.Text = base.lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Has_Influenced_Voting", "Has Influenced Voting at");
                    base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                    break;

                case 0x43:
                    base.lblMainText.Text = base.lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Removed_Disease", "Has Removed Disease From");
                    base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                    break;

                case 0x44:
                    base.lblMainText.Text = base.lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Has_Interdicted", "Has Interdict Protected");
                    base.lblSecondaryText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    break;

                case 0x45:
                    base.lblMainText.Text = base.lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Has_Inquisited", "Has Inquisited");
                    base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                    break;

                case 70:
                    base.lblMainText.Text = base.lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Has_Excommunicated", "Has Excommunicated");
                    base.lblSecondaryText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    break;

                case 0x47:
                    base.lblMainText.Text = base.lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Has_Absolved", "Has Absolved");
                    base.lblSecondaryText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    break;

                case 0x48:
                    base.lblMainText.Text = base.lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Has_Blessed", "Has Blessed");
                    base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                    break;

                case 0x5b:
                    base.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Has_Excommunicated", "Has Excommunicated");
                    base.lblSecondaryText.Text = base.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    break;

                case 0x67:
                    base.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Has_Absolved", "Has Absolved");
                    base.lblSecondaryText.Text = base.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    break;

                case 0x68:
                    base.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Has_Inquisited", "Has Inquisited");
                    base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                    break;

                case 0x69:
                    base.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Has_Interdicted", "Has Interdict Protected");
                    base.lblSecondaryText.Text = base.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    break;

                case 0x6a:
                    base.lblMainText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                    if (returnData.genericData1 != 0)
                    {
                        base.lblSubTitle.Text = SK.Text("Reports_Interdiction_Termination", "Interdiction Was Terminated");
                        break;
                    }
                    base.lblSubTitle.Text = SK.Text("Reports_Interdiction_Has_Ended", "Interdiction Has Ended");
                    break;
            }
            this.initFurtherInfo();
        }
    }
}

