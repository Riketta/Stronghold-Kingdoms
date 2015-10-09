namespace Kingdoms
{
    using CommonTypes;
    using System;

    internal class FactionReportPanelDerived : GenericReportPanelBasic
    {
        private int subTypeID = -1;

        public override void setData(GetReport_ReturnType returnData)
        {
            base.setData(returnData);
            FactionData data = null;
            switch (returnData.reportType)
            {
                case 0x6b:
                    base.lblMainText.Text = SK.Text("Reports_Faction_New_Member", "New Faction Member");
                    base.lblSecondaryText.Text = returnData.otherUser;
                    goto Label_0822;

                case 0x6c:
                    if (!(returnData.otherUser == ""))
                    {
                        base.lblMainText.Text = SK.Text("Reports_Faction_Member_Leave", "Member Leaving Faction");
                        base.lblSecondaryText.Text = returnData.otherUser;
                    }
                    else
                    {
                        base.lblMainText.Text = SK.Text("Reports_Faction_Member_Leave_Self", "You are no longer a member of this Faction");
                        data = GameEngine.Instance.World.getFaction(returnData.genericData1);
                        if (data != null)
                        {
                            base.lblSecondaryText.Text = data.factionName;
                        }
                    }
                    goto Label_0822;

                case 0x6d:
                    if (!(returnData.otherUser == ""))
                    {
                        base.lblMainText.Text = SK.Text("Reports_Faction_Member_Dismissed", "Faction Member Dismissed");
                        base.lblSecondaryText.Text = returnData.otherUser;
                    }
                    else
                    {
                        data = GameEngine.Instance.World.getFaction(returnData.genericData1);
                        if (data != null)
                        {
                            base.lblSecondaryText.Text = data.factionName;
                        }
                        switch (returnData.genericData3)
                        {
                            case -1:
                                base.lblMainText.Text = " ";
                                break;

                            case 1:
                                base.lblMainText.Text = SK.Text("Reports_Faction_Member_Dismissed_Leader", "You were dismissed by the leader of the faction");
                                break;

                            case 2:
                                base.lblMainText.Text = SK.Text("Reports_Faction_Member_Dismissed_Officer", "You were dismissed by an officer of the faction");
                                break;
                        }
                    }
                    goto Label_0822;

                case 110:
                    if (returnData.genericData2 != 2)
                    {
                        base.lblSubTitle.Text = SK.Text("Reports_Faction_Promotion_Leader", "You have been promoted To Leader");
                        break;
                    }
                    base.lblSubTitle.Text = SK.Text("Reports_Faction_Promotion_Officer", "You have been promoted To Officer");
                    break;

                case 0x6f:
                    if (returnData.genericData2 != 2)
                    {
                        base.lblSubTitle.Text = SK.Text("Reports_Faction_Demotion_Leader", "You have been demoted To Commoner");
                    }
                    else
                    {
                        base.lblSubTitle.Text = SK.Text("Reports_Faction_Demotion_Officer", "You have been demoted To Officer");
                    }
                    data = GameEngine.Instance.World.getFaction(returnData.genericData1);
                    if (data != null)
                    {
                        base.lblSecondaryText.Text = data.factionName;
                    }
                    goto Label_0822;

                case 0x70:
                {
                    switch (returnData.genericData3)
                    {
                        case -1:
                            base.lblSubTitle.Text = SK.Text("Reports_Faction_Enemy", "has set the following Faction as an Enemy") + " :";
                            break;

                        case 0:
                            base.lblSubTitle.Text = SK.Text("Reports_Faction_Neutral", "has set the following Faction as neutral") + " :";
                            break;

                        case 1:
                            base.lblSubTitle.Text = SK.Text("Reports_Faction_Friend", "has set the following Faction as a Friend") + " :";
                            break;
                    }
                    data = GameEngine.Instance.World.getFaction(returnData.genericData1);
                    if (data != null)
                    {
                        base.lblMainText.Text = data.factionName;
                    }
                    FactionData data2 = GameEngine.Instance.World.getFaction(returnData.genericData2);
                    if (data != null)
                    {
                        base.lblSecondaryText.Text = data2.factionName;
                    }
                    goto Label_0822;
                }
                case 0x71:
                    switch (returnData.genericData3)
                    {
                        case -1:
                            base.lblSubTitle.Text = SK.Text("Reports_Faction_LeaveHouse", "has left");
                            break;

                        case 1:
                            base.lblSubTitle.Text = SK.Text("Reports_Faction_JoinHouse", "has joined");
                            break;
                    }
                    data = GameEngine.Instance.World.getFaction(returnData.genericData1);
                    if (data != null)
                    {
                        base.lblMainText.Text = data.factionName;
                    }
                    base.lblSecondaryText.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + returnData.genericData2.ToString();
                    goto Label_0822;

                case 0x72:
                    switch (returnData.genericData3)
                    {
                        case -1:
                            base.lblSubTitle.Text = SK.Text("Reports_House_Enemy", "has set the following House as an Enemy") + " :";
                            break;

                        case 0:
                            base.lblSubTitle.Text = SK.Text("Reports_House_Neutral", "has set the following House as Neutral") + " :";
                            break;

                        case 1:
                            base.lblSubTitle.Text = SK.Text("Reports_House_Friend", "has set the following House as a Friend") + " :";
                            break;
                    }
                    base.lblMainText.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + returnData.genericData1.ToString();
                    base.lblSecondaryText.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + returnData.genericData2.ToString();
                    goto Label_0822;

                case 0x73:
                    base.lblSubTitle.Text = "";
                    base.lblMainText.Text = SK.Text("Reports_Faction_Application", "A Player Has Applied to your Faction");
                    base.lblSecondaryText.Text = returnData.otherUser;
                    goto Label_0822;

                case 0x74:
                    base.lblSubTitle.Text = SK.Text("Reports_Faction_Application_accepted", "Your Faction Application has been Accepted.");
                    data = GameEngine.Instance.World.getFaction(returnData.genericData1);
                    if (data != null)
                    {
                        base.lblSecondaryText.Text = data.factionName;
                    }
                    goto Label_0822;

                case 0x75:
                    base.lblSubTitle.Text = SK.Text("Reports_Faction_Application_rejected", "Your Faction Application has been Rejected.");
                    data = GameEngine.Instance.World.getFaction(returnData.genericData1);
                    if (data != null)
                    {
                        base.lblSecondaryText.Text = data.factionName;
                    }
                    goto Label_0822;

                case 0x76:
                    base.lblSubTitle.Text = "";
                    data = GameEngine.Instance.World.getFaction(returnData.genericData1);
                    if (data != null)
                    {
                        base.lblSecondaryText.Text = data.factionName;
                    }
                    switch (returnData.genericData3)
                    {
                        case -1:
                            base.lblMainText.Text = " ";
                            break;

                        case 1:
                            base.lblMainText.Text = SK.Text("Reports_Faction_Member_Dismissed_Leader", "You were dismissed by the leader of the faction");
                            break;

                        case 2:
                            base.lblMainText.Text = SK.Text("Reports_Faction_Member_Dismissed_Officer", "You were dismissed by an officer of the faction");
                            break;
                    }
                    if (returnData.otherUser != "")
                    {
                        base.lblSubTitle.Text = returnData.otherUser;
                    }
                    goto Label_0822;

                case 120:
                    if (returnData.genericData2 != 0)
                    {
                        base.lblMainText.Text = "";
                        base.lblSubTitle.Text = SK.Text("Reports_Faction_Glory_You_Obtained", "You claimed glory points for your house");
                        base.lblSecondaryText.Text = returnData.genericData1.ToString() + " " + SK.Text("Reports_Faction_Glory_Amount", "glory points awarded");
                    }
                    else
                    {
                        base.lblMainText.Text = returnData.otherUser;
                        base.lblSubTitle.Text = SK.Text("Reports_Faction_Glory_Obtained", "This player claimed glory points for your house");
                        base.lblSecondaryText.Text = returnData.genericData1.ToString() + " " + SK.Text("Reports_Faction_Glory_Amount", "glory points awarded");
                    }
                    goto Label_0822;

                case 50:
                    base.lblSubTitle.Text = SK.Text("Reports_Invite_to", "You have an Invitation To");
                    data = GameEngine.Instance.World.getFaction(returnData.genericData1);
                    if (data != null)
                    {
                        base.lblSecondaryText.Text = data.factionName;
                    }
                    goto Label_0822;

                default:
                    goto Label_0822;
            }
            data = GameEngine.Instance.World.getFaction(returnData.genericData1);
            if (data != null)
            {
                base.lblSecondaryText.Text = data.factionName;
            }
        Label_0822:
            switch (returnData.reportType)
            {
                case 0x6b:
                case 0x6c:
                case 0x6d:
                case 110:
                case 0x6f:
                case 0x74:
                case 0x76:
                case 120:
                    if (RemoteServices.Instance.UserFactionID != -1)
                    {
                        base.btnUtility.Text.Text = SK.Text("GENERIC_Factions", "Factions");
                        base.btnUtility.Visible = true;
                        this.subTypeID = 2;
                        return;
                    }
                    return;

                case 0x70:
                    if (RemoteServices.Instance.UserFactionID != -1)
                    {
                        base.btnUtility.Visible = true;
                        base.btnUtility.Text.Text = SK.Text("GENERIC_Factions", "Factions");
                        this.subTypeID = 4;
                        return;
                    }
                    return;

                case 0x71:
                case 0x72:
                case 0x75:
                    base.btnUtility.Text.Text = SK.Text("GENERIC_Factions", "Factions");
                    base.btnUtility.Visible = true;
                    this.subTypeID = 1;
                    return;

                case 0x73:
                    if (RemoteServices.Instance.UserFactionID != -1)
                    {
                        base.btnUtility.Text.Text = SK.Text("GENERIC_Factions", "Factions");
                        base.btnUtility.Visible = true;
                        this.subTypeID = 3;
                        return;
                    }
                    return;

                case 0x77:
                    break;

                case 50:
                    base.btnUtility.Visible = true;
                    base.btnUtility.Text.Text = SK.Text("GENERIC_Factions", "Factions");
                    this.subTypeID = 5;
                    break;

                default:
                    return;
            }
        }

        protected override void utilityClick()
        {
            GameEngine.Instance.playInterfaceSound("FactionInviteReportPanel_faction");
            CustomSelfDrawPanel.FactionPanelSideBar.forceReUpdate();
            switch (this.subTypeID)
            {
                case 1:
                    GameEngine.Instance.setNextFactionPage(0x33);
                    break;

                case 2:
                    InterfaceMgr.Instance.showFactionPanel(RemoteServices.Instance.UserFactionID);
                    break;

                case 3:
                    GameEngine.Instance.setNextFactionPage(0x2e);
                    break;

                case 4:
                    GameEngine.Instance.setNextFactionPage(0x2c);
                    break;

                case 5:
                    GameEngine.Instance.setNextFactionPage(0x29);
                    break;
            }
            if (this.subTypeID != 2)
            {
                InterfaceMgr.Instance.getMainTabBar().changeTab(8);
            }
            base.m_parent.closeControl(true);
            InterfaceMgr.Instance.reactiveMainWindow();
        }
    }
}

