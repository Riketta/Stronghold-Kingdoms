namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Drawing;

    internal class ParishElectionReportPanelDerived : GenericReportPanelBasic
    {
        private Point mapTarget = new Point(-1, -1);
        private double targetZoomLevel;

        public override void setData(GetReport_ReturnType returnData)
        {
            base.setData(returnData);
            short reportType = returnData.reportType;
            if (reportType != 0x1c)
            {
                switch (reportType)
                {
                    case 0x4a:
                        base.lblMainText.Text = returnData.otherUser;
                        base.lblSubTitle.Text = SK.Text("Reports_Is_Elected_For", "Is Elected For");
                        base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.genericData8) + " / " + GameEngine.Instance.World.getProvinceName(GameEngine.Instance.World.getProvinceFromVillageID(returnData.genericData8));
                        break;

                    case 0x4b:
                        base.lblMainText.Text = returnData.otherUser;
                        base.lblSubTitle.Text = SK.Text("Reports_Is_Elected_For", "Is Elected For");
                        base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.genericData8) + " / " + GameEngine.Instance.World.getCountryName(GameEngine.Instance.World.getCountryFromVillageID(returnData.genericData8));
                        break;

                    case 0x35:
                        base.lblMainText.Text = returnData.otherUser;
                        base.lblSubTitle.Text = SK.Text("Reports_Is_Elected_For", "Is Elected For");
                        base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.genericData8) + " / " + GameEngine.Instance.World.getCountyName(GameEngine.Instance.World.getCountyFromVillageID(returnData.genericData8));
                        break;
                }
            }
            else
            {
                base.lblMainText.Text = returnData.otherUser;
                base.lblSubTitle.Text = SK.Text("Reports_Is_Elected_For", "Is Elected For");
                base.lblSecondaryText.Text = GameEngine.Instance.World.getParishNameFromVillageID(returnData.genericData8);
            }
            if (returnData.genericData8 >= 0)
            {
                this.mapTarget = GameEngine.Instance.World.getVillageLocation(returnData.genericData8);
                this.targetZoomLevel = 10000.0;
                base.btnUtility.Text.Text = SK.Text("Reports_Show_On_Map", "Show On Map");
                base.btnUtility.Visible = true;
            }
            else
            {
                base.btnUtility.Visible = false;
            }
        }

        protected override void utilityClick()
        {
            GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
            if (this.mapTarget.X != -1)
            {
                InterfaceMgr.Instance.changeTab(0);
                GameEngine.Instance.World.startMultiStageZoom(this.targetZoomLevel, (double) this.mapTarget.X, (double) this.mapTarget.Y);
                base.m_parent.closeControl(true);
                InterfaceMgr.Instance.reactiveMainWindow();
            }
        }
    }
}

