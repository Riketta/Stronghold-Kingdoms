namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Drawing;

    internal class TradeReportPanelDerived : GenericReportPanelBasic
    {
        public override void setData(GetReport_ReturnType returnData)
        {
            base.setData(returnData);
            switch (returnData.reportType)
            {
                case 0x49:
                    base.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Sent_Resources_To", "Has sent resources to");
                    base.lblSecondaryText.Text = base.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    break;

                case 0x4e:
                    base.lblMainText.Text = GameEngine.Instance.World.getVillageName(returnData.attackingVillage);
                    base.lblSubTitle.Text = SK.Text("Report_Auto_Sent_Resources_To", "Has Auto-Sent resources to");
                    base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                    break;
            }
            base.imgFurther.Image = (Image) GFXLibrary.getCommodity32Image(base.m_returnData.genericData1);
            base.imgFurther.setSizeToImage();
            base.imgFurther.Position = new Point((base.Width / 2) - base.imgFurther.Width, base.btnDelete.Position.Y);
            base.lblFurther.Text = base.m_returnData.genericData2.ToString("N", base.nfi);
            base.lblFurther.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            base.lblFurther.Size = new Size(base.Width, base.imgFurther.Height);
            base.lblFurther.Position = new Point(base.imgFurther.Rectangle.Right + 10, base.imgFurther.Position.Y);
            base.showFurtherInfo();
        }
    }
}

