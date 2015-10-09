namespace Kingdoms
{
    using CommonTypes;
    using System;

    internal class ResearchCompleteReportPanelDerived : GenericReportPanelBasic
    {
        public override void setData(GetReport_ReturnType returnData)
        {
            base.setData(returnData);
            string[] strArray = new string[] { ResearchData.getResearchName(returnData.genericData1), " : ", SK.Text("Reports_Research_Level", "Level"), " : ", (returnData.genericData2 + 1).ToString() };
            base.lblSecondaryText.Text = string.Concat(strArray);
            base.lblSubTitle.Text = SK.Text("Reports_Research_Complete", "Research Complete");
            base.btnUtility.Text.Text = SK.Text("GENERIC_Research", "Research");
            base.btnUtility.Visible = true;
        }

        protected override void utilityClick()
        {
            GameEngine.Instance.playInterfaceSound("ResearchCompleteReportPanel_research");
            InterfaceMgr.Instance.getMainTabBar().changeTab(3);
            base.m_parent.closeControl(true);
            InterfaceMgr.Instance.reactiveMainWindow();
        }
    }
}

