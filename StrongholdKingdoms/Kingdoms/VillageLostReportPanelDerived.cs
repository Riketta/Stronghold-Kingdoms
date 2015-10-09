namespace Kingdoms
{
    using CommonTypes;
    using System;

    internal class VillageLostReportPanelDerived : GenericReportPanelBasic
    {
        public override void setData(GetReport_ReturnType returnData)
        {
            base.setData(returnData);
            base.lblMainText.Text = SK.Text("Reports_VillageLost", "Village Lost");
            if (returnData.otherUser.Length == 0)
            {
                base.lblSubTitle.Text = SK.Text("Reports_VillageLost_inactivity", "Village Lost due to Inactivity");
            }
            else if (returnData.reportType == 0x80)
            {
                base.lblSubTitle.Text = SK.Text("Reports_VillageLost_abandoned", "Village Abandoned");
            }
            else
            {
                base.lblSubTitle.Text = SK.Text("Reports_VillageLost_attacked by", "Attacked By") + " : " + returnData.otherUser;
            }
            base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
        }
    }
}

