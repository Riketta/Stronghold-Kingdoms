namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class ReinforcementsReportPanelDerived : GenericReportPanelBasic
    {
        private Point mapTarget = new Point(-1, -1);
        private double targetZoomLevel;
        private ReportBattleValuesPanel valuesPanel;

        public ReinforcementsReportPanelDerived()
        {
            base.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.Size = new Size(580, 480);
        }

        private void attackerDoubleClick()
        {
            if (base.m_returnData != null)
            {
                Point point = GameEngine.Instance.World.getVillageLocation(base.m_returnData.attackingVillage);
                double targetZoom = 10000.0;
                if (point.X != -1)
                {
                    GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
                    InterfaceMgr.Instance.changeTab(0);
                    GameEngine.Instance.World.startMultiStageZoom(targetZoom, (double) point.X, (double) point.Y);
                    base.m_parent.closeControl(true);
                    InterfaceMgr.Instance.reactiveMainWindow();
                    InterfaceMgr.Instance.displaySelectedVillagePanel(base.m_returnData.attackingVillage, false, true, false, false);
                }
            }
        }

        private void defenderDoubleClick()
        {
            if (base.m_returnData != null)
            {
                Point point = GameEngine.Instance.World.getVillageLocation(base.m_returnData.defendingVillage);
                double targetZoom = 10000.0;
                if (point.X != -1)
                {
                    GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
                    InterfaceMgr.Instance.changeTab(0);
                    GameEngine.Instance.World.startMultiStageZoom(targetZoom, (double) point.X, (double) point.Y);
                    base.m_parent.closeControl(true);
                    InterfaceMgr.Instance.reactiveMainWindow();
                    InterfaceMgr.Instance.displaySelectedVillagePanel(base.m_returnData.defendingVillage, false, true, false, false);
                }
            }
        }

        public override void init(IDockableControl parent, Size size, object back)
        {
            base.init(parent, size, back);
            this.valuesPanel = new ReportBattleValuesPanel(this, new Size((base.btnClose.X - base.btnForward.Rectangle.Right) - 4, 200));
            this.valuesPanel.Position = new Point(base.btnForward.Rectangle.Right + 2, base.Height - this.valuesPanel.Height);
            string header = SK.Text("GENERIC_Reinforcements", "Reinforcements");
            this.valuesPanel.init(header, false, true);
            if (base.imgBackground.Image != null)
            {
                base.imgBackground.addControl(this.valuesPanel);
            }
            else
            {
                base.addControl(this.valuesPanel);
            }
        }

        public override void setData(GetReport_ReturnType returnData)
        {
            base.setData(returnData);
            this.valuesPanel.setData(returnData.genericData1, returnData.genericData2, returnData.genericData3, returnData.genericData4, returnData.genericData5, 0);
            switch (returnData.reportType)
            {
                case 0x11:
                    base.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Sent_Reinforcements_To", "sent reinforcements to");
                    base.lblSecondaryText.Text = base.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    break;

                case 0x12:
                    base.lblMainText.Text = base.lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Reinforcements_Returned_From", "Reinforcements have returned from");
                    base.lblSecondaryText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    break;

                case 0x13:
                    base.lblMainText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Retrieved_Reinforcements", "has retrieved reinforcements from");
                    base.lblSecondaryText.Text = base.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    break;
            }
            if (returnData.defendingVillage >= 0)
            {
                this.mapTarget = GameEngine.Instance.World.getVillageLocation(returnData.defendingVillage);
                this.targetZoomLevel = 10000.0;
                base.btnUtility.Visible = true;
                base.btnUtility.Text.Text = SK.Text("Reports_Show_On_Map", "Show On Map");
            }
            else
            {
                base.btnUtility.Visible = false;
            }
            base.lblMainText.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.attackerDoubleClick), "Reports_Attacker_DClick");
            base.lblSecondaryText.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.defenderDoubleClick), "Reports_Defender_DClick");
        }

        protected override void utilityClick()
        {
            if (this.mapTarget.X != -1)
            {
                GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
                InterfaceMgr.Instance.changeTab(0);
                GameEngine.Instance.World.startMultiStageZoom(this.targetZoomLevel, (double) this.mapTarget.X, (double) this.mapTarget.Y);
                base.m_parent.closeControl(true);
            }
        }
    }
}

