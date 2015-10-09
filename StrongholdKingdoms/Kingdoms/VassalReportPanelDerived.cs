namespace Kingdoms
{
    using CommonTypes;
    using System;

    internal class VassalReportPanelDerived : GenericReportPanelBasic
    {
        private int villageID = -1;

        public override void setData(GetReport_ReturnType returnData)
        {
            base.setData(returnData);
            switch (returnData.reportType)
            {
                case 15:
                    base.lblMainText.Text = base.lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Has_Lost_Vassal", "Has Lost a Vassal");
                    base.lblSecondaryText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    this.villageID = returnData.attackingVillage;
                    base.lblFurther.Text = SK.Text("Reports_Troops_Lost", "Troops Lost") + " : " + returnData.genericData1.ToString("N", base.nfi);
                    base.lblFurther.Visible = true;
                    base.addControl(base.lblFurther);
                    break;

                case 0x10:
                    base.lblMainText.Text = base.lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_No_Longer_Liege_Lord", "No longer has a liege lord");
                    base.lblSecondaryText.Text = "";
                    this.villageID = returnData.attackingVillage;
                    break;

                case 0x2e:
                    base.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Offers_Liege_lord", "offers to be liege lord of");
                    base.lblSecondaryText.Text = base.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    this.villageID = returnData.defendingVillage;
                    break;

                case 0x2f:
                    base.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Accepted_Liege_Lord", "has accepted your liege lord offer and becomes your vassal to");
                    base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.attackingVillage);
                    this.villageID = returnData.attackingVillage;
                    break;

                case 0x30:
                    base.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Has_declined_Liege_lord_Offer", "has declined your liege lord offer from");
                    base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.attackingVillage);
                    this.villageID = returnData.attackingVillage;
                    break;

                case 0x31:
                    base.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Withdrawn_Liege_Lord_Offer", "has withdrawn the liege lord offer for");
                    base.lblSecondaryText.Text = base.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    this.villageID = returnData.defendingVillage;
                    break;
            }
            if (GameEngine.Instance.World.isUserVillage(this.villageID))
            {
                base.btnUtility.Text.Text = SK.Text("GENERIC_Vassals", "Vassals");
                base.btnUtility.Visible = true;
            }
            else
            {
                base.btnUtility.Visible = false;
            }
        }

        protected override void utilityClick()
        {
            if (this.villageID >= 0)
            {
                GameEngine.Instance.playInterfaceSound("VassalLostReportPanel_vassals");
                InterfaceMgr.Instance.selectUserVillage(this.villageID, false);
                GameEngine.Instance.SkipVillageTab();
                InterfaceMgr.Instance.getMainTabBar().changeTab(1);
                InterfaceMgr.Instance.setVillageTabSubMode(8);
                base.m_parent.closeControl(true);
                InterfaceMgr.Instance.reactiveMainWindow();
            }
        }
    }
}

