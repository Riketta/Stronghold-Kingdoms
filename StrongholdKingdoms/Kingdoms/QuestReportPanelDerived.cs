namespace Kingdoms
{
    using CommonTypes;
    using System;

    internal class QuestReportPanelDerived : GenericReportPanelBasic
    {
        public override void setData(GetReport_ReturnType returnData)
        {
            base.setData(returnData);
            switch (returnData.reportType)
            {
                case 100:
                {
                    base.lblSubTitle.Text = SK.Text("Reports_Quest_Complete", "Completed Quest") + " :";
                    NewQuests.NewQuestDefinition definition = NewQuests.getNewQuestDef(returnData.genericData1);
                    base.lblSecondaryText.Text = SK.NoStoreText("Z_QUESTS_" + definition.tagString);
                    goto Label_0242;
                }
                case 0x65:
                {
                    base.lblSubTitle.Text = SK.Text("Reports_Quest Failed", "Failed Quest") + " :";
                    NewQuests.NewQuestDefinition definition2 = NewQuests.getNewQuestDef(returnData.genericData1);
                    base.lblSecondaryText.Text = SK.NoStoreText("Z_QUESTS_" + definition2.tagString);
                    goto Label_0242;
                }
                case 0x66:
                    base.lblSubTitle.Text = SK.Text("Reports_Spins", "Wheel Spin Prize");
                    base.lblSecondaryText.Text = Wheel.getRewardText(returnData.genericData1, returnData.genericData2, base.nfi);
                    return;

                case 0x81:
                case 130:
                case 0x83:
                    if (returnData.reportType != 0x81)
                    {
                        if (returnData.reportType == 0x83)
                        {
                            base.lblSubTitle.Text = SK.Text("Reports_AI_Spins_capture", "Wheel Spin Bonus from AI Capture");
                        }
                        else
                        {
                            base.lblSubTitle.Text = SK.Text("Reports_Forage_Spins", "Wheel Spin Bonus from Foraging");
                        }
                        break;
                    }
                    base.lblSubTitle.Text = SK.Text("Reports_AI_Spins", "Wheel Spin Bonus from AI Razing");
                    break;

                default:
                    goto Label_0242;
            }
            switch (returnData.genericData1)
            {
                case 2:
                    base.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins2", "Tier 2 Wheel Spin");
                    return;

                case 3:
                    base.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins3", "Tier 3 Wheel Spin");
                    return;

                case 4:
                    base.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins4", "Tier 4 Wheel Spin");
                    return;

                case 5:
                    base.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins5", "Tier 5 Wheel Spin");
                    return;

                default:
                    base.lblSecondaryText.Text = SK.Text("REPORTS_SeasonalWheelSpins1", "Tier 1 Wheel Spin");
                    return;
            }
        Label_0242:
            base.btnUtility.Visible = true;
            base.btnUtility.Text.Text = SK.Text("GENERIC_Quests", "Quests");
        }

        protected override void utilityClick()
        {
            InterfaceMgr.Instance.getMainTabBar().changeTab(5);
            base.m_parent.closeControl(true);
            InterfaceMgr.Instance.reactiveMainWindow();
        }
    }
}

