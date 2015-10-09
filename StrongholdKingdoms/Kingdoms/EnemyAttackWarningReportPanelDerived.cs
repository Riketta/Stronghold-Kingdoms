namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Drawing;

    internal class EnemyAttackWarningReportPanelDerived : GenericReportPanelBasic
    {
        private Point mapTarget = new Point(-1, -1);

        public override void setData(GetReport_ReturnType returnData)
        {
            base.setData(returnData);
            switch (returnData.reportType)
            {
                case 80:
                    base.lblMainText.Text = SK.Text("Reports_Enemy_Warning_1", "The enemy arrives in our parish!");
                    base.lblSecondaryText.Text = SK.Text("Reports_Enemy_Warning_2", "Our countries enemies have set up a siege camp in the parish. It is too well defended to attack. We must do our bit for the country and make sure our castle holds firm.");
                    break;

                case 0x51:
                    base.lblMainText.Text = SK.Text("Reports_Enemy_First_Attack_1", "Enemy probes castle defences.");
                    base.lblSecondaryText.Text = SK.Text("Reports_Enemy_First_Attack_2", "The enemy has sent a small force to test our castle defences.");
                    break;

                case 0x52:
                    base.lblMainText.Text = SK.Text("Reports_Enemy_Normal_Attack_1", "Enemy launches attack.");
                    base.lblSecondaryText.Text = SK.Text("Reports_Enemy_Normal_Attack_2", "Enemy troops are advancing on our castle.");
                    break;

                case 0x53:
                    base.lblMainText.Text = SK.Text("Reports_Enemy_Prefinal_Attack_1", "Enemy troops advancing in large numbers.");
                    base.lblSecondaryText.Text = SK.Text("Reports_Enemy_Prefinal_Attack_2", "The enemy is intensifying its efforts and has sent a large force against our castle. ");
                    break;

                case 0x54:
                    base.lblMainText.Text = SK.Text("Reports_Enemy_Final_Attack_1", "Enemy launches final attack.");
                    base.lblSecondaryText.Text = SK.Text("Reports_Enemy_Final_Attack_2", "The enemy has thrown all their troops against our castle in one final siege.");
                    break;

                case 0x55:
                    base.lblMainText.Text = SK.Text("Reports_Enemy_Leave_Map_1", "The enemy is vanquished!.");
                    base.lblSecondaryText.Text = SK.Text("Reports_Enemy_Leave_Map_2", "Our parish has stood firm, the few remaining enemy troops have fled. Our castle is safe... for now!");
                    break;

                case 0x56:
                    base.lblMainText.Text = SK.Text("Reports_Enemy_Diplomacy_1", "Enemy Attack stopped by Diplomacy.");
                    base.lblSecondaryText.Text = SK.Text("Reports_Enemy_Diplomacy_2", "Your diplomacy skills have prevented an attack from the enemy.");
                    base.lblFurther.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData1.ToString("N", base.nfi);
                    base.lblSubTitle.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                    this.showHonour();
                    break;

                case 0x57:
                    base.lblMainText.Text = SK.Text("Reports_Enemy_Diplomacy_Rat_1", "Rat's Attack stopped by Diplomacy.");
                    base.lblSecondaryText.Text = SK.Text("Reports_Enemy_Diplomacy_Rat_2", "Your diplomacy skills have prevented an attack from the Rat.");
                    base.lblFurther.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData1.ToString("N", base.nfi);
                    base.lblSubTitle.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                    this.showHonour();
                    break;

                case 0x58:
                    base.lblMainText.Text = SK.Text("Reports_Enemy_Diplomacy_Snake_1", "Snake's Attack stopped by Diplomacy.");
                    base.lblSecondaryText.Text = SK.Text("Reports_Enemy_Diplomacy_Snake_2", "Your diplomacy skills have prevented an attack from the Snake.");
                    base.lblFurther.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData1.ToString("N", base.nfi);
                    base.lblSubTitle.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                    this.showHonour();
                    break;

                case 0x59:
                    base.lblMainText.Text = SK.Text("Reports_Enemy_Diplomacy_Pig_1", "Pig's Attack stopped by Diplomacy.");
                    base.lblSecondaryText.Text = SK.Text("Reports_Enemy_Diplomacy_Pig_2", "Your diplomacy skills have prevented an attack from the Pig.");
                    base.lblFurther.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData1.ToString("N", base.nfi);
                    base.lblSubTitle.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                    this.showHonour();
                    break;

                case 90:
                    base.lblMainText.Text = SK.Text("Reports_Enemy_Diplomacy_Wolf_1", "Wolf Attack stopped by Diplomacy.");
                    base.lblSecondaryText.Text = SK.Text("Reports_Enemy_Diplomacy_Wolf_2", "Your diplomacy skills have prevented an attack from the Wolf.");
                    base.lblFurther.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData1.ToString("N", base.nfi);
                    base.lblSubTitle.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                    this.showHonour();
                    break;
            }
            base.lblSecondaryText.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            base.lblSecondaryText.Size = new Size(base.Width - 40, 80);
            base.lblDate.Position = new Point(0, base.lblSecondaryText.Rectangle.Bottom);
            if (returnData.defendingVillage >= 0)
            {
                this.mapTarget = GameEngine.Instance.World.getVillageLocation(returnData.defendingVillage);
                base.lblSubTitle.Text = SK.Text("GENERIC_Parish", "Parish") + " : " + GameEngine.Instance.World.getParishNameFromVillageID(returnData.defendingVillage);
                base.btnUtility.Visible = true;
                base.btnUtility.Text.Text = SK.Text("Reports_View_Target", "View Target");
            }
            else
            {
                base.btnUtility.Visible = false;
            }
        }

        private void showHonour()
        {
            base.imgFurther.Image = (Image) GFXLibrary.com_32_honour_DS;
            base.imgFurther.setSizeToImage();
            base.imgFurther.Position = new Point((base.Width / 2) - (base.imgFurther.Width / 2), base.btnDelete.Position.Y);
            base.lblFurther.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            base.lblFurther.Size = new Size((base.btnClose.X - base.btnForward.Rectangle.Right) - 10, 0x1a);
            base.lblFurther.Position = new Point(base.btnForward.Rectangle.Right + 5, base.btnForward.Y);
            base.showFurtherInfo();
        }

        protected override void utilityClick()
        {
            if (this.mapTarget.X != -1)
            {
                GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
                InterfaceMgr.Instance.changeTab(0);
                GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) this.mapTarget.X, (double) this.mapTarget.Y);
                base.m_parent.closeControl(true);
                InterfaceMgr.Instance.reactiveMainWindow();
            }
        }
    }
}

