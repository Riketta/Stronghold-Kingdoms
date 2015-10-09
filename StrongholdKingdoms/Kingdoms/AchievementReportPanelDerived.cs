namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Drawing;

    internal class AchievementReportPanelDerived : GenericReportPanelBasic
    {
        public override void setData(GetReport_ReturnType returnData)
        {
            base.setData(returnData);
            base.lblSecondaryText.Text = CustomTooltipManager.getAchievementTitle(returnData.genericData1) + " - " + CustomTooltipManager.getAchievementRank(returnData.genericData1);
            base.lblSubTitle.Text = SK.Text("ReportsPanel_Achievement_Attained", "Achievement Attained");
            base.imgFurther.Image = (Image) GFXLibrary.com_32_honour_DS;
            base.imgFurther.setSizeToImage();
            base.imgFurther.Position = new Point((base.Width / 2) - base.imgFurther.Width, base.btnDelete.Position.Y);
            base.lblFurther.Text = base.m_returnData.genericData2.ToString("N", base.nfi);
            base.lblFurther.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            base.lblFurther.Size = new Size(base.Width, base.imgFurther.Height);
            base.lblFurther.Position = new Point(base.imgFurther.Rectangle.Right + 10, base.imgFurther.Position.Y);
            base.showFurtherInfo();
            base.btnUtility.Visible = true;
            base.btnUtility.Text.Text = SK.Text("GENERIC_Achievements", "Achievements");
        }

        protected override void utilityClick()
        {
            GameEngine.Instance.playInterfaceSound("AchievementReportPanel_achievements");
            InterfaceMgr.Instance.getMainTabBar().changeTab(4);
            base.m_parent.closeControl(true);
            InterfaceMgr.Instance.reactiveMainWindow();
        }
    }
}

