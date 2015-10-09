namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Drawing;

    internal class VillageCharterReportPanelDerived : GenericReportPanelBasic
    {
        private Point mapTarget = new Point(-1, -1);
        private double targetZoomLevel;

        public override void setData(GetReport_ReturnType returnData)
        {
            base.setData(returnData);
            base.lblSubTitle.Text = SK.Text("Reports_purchased_charter_Failed", "Has Failed to Purchase Village Charter");
            base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
            switch (returnData.reportType)
            {
                case 0x5d:
                    base.lblSubTitle.Text = SK.Text("Reports_purchased_charter", "Has Purchased Village Charter");
                    base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                    break;

                case 0x5e:
                    base.lblFurther.Text = SK.Text("Reports_purchased_charter_Failed_gold", "You had insufficient Gold to purchase this Charter when your Captain arrived at the village.");
                    break;

                case 0x5f:
                    base.lblFurther.Text = SK.Text("Reports_purchased_charter_Failed_bought", "Someone has purchased this Charter before your captain arrived.");
                    break;

                case 0x60:
                    base.lblFurther.Text = SK.Text("Reports_purchased_charter_Failed_too_many", "You already have your maximum number of villages and cannot buy this Charter.");
                    break;
            }
            if (returnData.reportType != 0x5d)
            {
                base.showFurtherInfo();
            }
            if (base.m_returnData.defendingVillage >= 0)
            {
                this.mapTarget = GameEngine.Instance.World.getVillageLocation(base.m_returnData.defendingVillage);
                this.targetZoomLevel = 10000.0;
                base.btnUtility.Visible = true;
                base.btnUtility.Text.Text = SK.Text("Reports_Show_On_Map", "Show On Map");
            }
        }

        protected override void utilityClick()
        {
            if (this.mapTarget.X != -1)
            {
                GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
                InterfaceMgr.Instance.changeTab(0);
                GameEngine.Instance.World.startMultiStageZoom(this.targetZoomLevel, (double) this.mapTarget.X, (double) this.mapTarget.Y);
                base.m_parent.closeControl(true);
                InterfaceMgr.Instance.reactiveMainWindow();
            }
        }
    }
}

