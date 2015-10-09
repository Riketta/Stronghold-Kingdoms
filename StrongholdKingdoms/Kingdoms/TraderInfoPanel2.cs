namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class TraderInfoPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDButton homeVillageButton = new CustomSelfDrawPanel.CSDButton();
        private int lastState = -1;
        private WorldMap.LocalTrader m_trader;
        private CustomSelfDrawPanel.CSDLabel resourceAmountLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage resourceImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton targetVillageButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage travelDirection = new CustomSelfDrawPanel.CSDImage();

        public TraderInfoPanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        public void display(ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(parent, x, y);
        }

        public void display(bool asPopup, ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(asPopup, parent, x, y);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void homeClick()
        {
            if (this.m_trader != null)
            {
                GameEngine.Instance.World.zoomToVillage(this.m_trader.trader.homeVillageID);
            }
        }

        public void init()
        {
            base.clearControls();
            CustomSelfDrawPanel.CSDImage image = this.backGround.init(true, 0x3ec);
            this.backGround.updateHeading(SK.Text("SelectArmyPanel_Trader", "Trader"));
            this.backGround.centerSubHeading();
            base.addControl(this.backGround);
            this.backGround.initTravelButton(this.homeVillageButton);
            this.homeVillageButton.Position = new Point(11, 0x3d);
            this.homeVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.homeClick), "TraderInfoPanel2_home_village");
            image.addControl(this.homeVillageButton);
            this.backGround.initTravelButton(this.targetVillageButton);
            this.targetVillageButton.Position = new Point(11, 0x77);
            this.targetVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.targetClick), "TraderInfoPanel2_target_village");
            image.addControl(this.targetVillageButton);
            this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
            this.travelDirection.Position = new Point(0x58, 90);
            this.travelDirection.Alpha = 0.5f;
            image.addControl(this.travelDirection);
            this.resourceImage.Image = GFXLibrary.dummy;
            this.resourceImage.Position = new Point(0x2d, 0x90);
            this.resourceImage.Visible = false;
            image.addControl(this.resourceImage);
            this.resourceAmountLabel.Text = "";
            this.resourceAmountLabel.Color = ARGBColors.Black;
            this.resourceAmountLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.resourceAmountLabel.Position = new Point(90, 0x9e);
            this.resourceAmountLabel.Size = new Size(0xa8, 0x17);
            this.resourceAmountLabel.Visible = false;
            image.addControl(this.resourceAmountLabel);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "TraderInfoPanel2";
            base.Size = new Size(0xc7, 0xd5);
            base.ResumeLayout(false);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        public void setTrader(long traderID)
        {
            WorldMap.LocalTrader trader = GameEngine.Instance.World.getTrader(traderID);
            if (trader != null)
            {
                this.m_trader = trader;
                this.lastState = -1;
                this.update();
            }
            else
            {
                InterfaceMgr.Instance.closeTraderInfoPanel();
            }
        }

        private void targetClick()
        {
            if (this.m_trader != null)
            {
                GameEngine.Instance.World.zoomToVillage(this.m_trader.trader.targetVillageID);
            }
        }

        public void update()
        {
            this.backGround.update();
            if (this.m_trader != null)
            {
                if (this.m_trader.trader.traderState != this.lastState)
                {
                    this.lastState = this.m_trader.trader.traderState;
                    this.backGround.updateTravelButton(this.homeVillageButton, this.m_trader.trader.homeVillageID);
                    this.backGround.updateTravelButton(this.targetVillageButton, this.m_trader.trader.targetVillageID);
                    this.resourceImage.Visible = false;
                    this.resourceAmountLabel.Visible = false;
                    if (this.lastState == 0)
                    {
                        InterfaceMgr.Instance.closeTraderInfoPanel();
                        return;
                    }
                    if (((this.lastState == 1) || (this.lastState == 3)) || (this.lastState == 6))
                    {
                        this.backGround.updatePanelText(SK.Text("SelectArmyPanel_Trading", "Trading"));
                        if (GameEngine.Instance.World.isUserVillage(this.m_trader.trader.homeVillageID))
                        {
                            this.resourceImage.Image = (Image) GFXLibrary.getCommodity32DSImage(this.m_trader.trader.resource);
                            this.resourceImage.Visible = true;
                            NumberFormatInfo nFI = GameEngine.NFI;
                            this.resourceAmountLabel.TextDiffOnly = GameEngine.Instance.World.getTradingAmount(this.m_trader.traderID).ToString("N", nFI);
                            this.resourceAmountLabel.Visible = true;
                        }
                        if (this.lastState == 6)
                        {
                            this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[1];
                        }
                        else
                        {
                            this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
                        }
                    }
                    else if ((this.lastState == 2) || (this.lastState == 4))
                    {
                        this.backGround.updatePanelText(SK.Text("SelectArmyPanel_Returning", "Returning"));
                        this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[1];
                    }
                    else if (this.lastState == 5)
                    {
                        this.backGround.updatePanelText(SK.Text("SelectArmyPanel_Collecting", "Collecting"));
                        this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
                    }
                }
                double num2 = DXTimer.GetCurrentMilliseconds() / 1000.0;
                double num3 = this.m_trader.localEndTime - num2;
                if (num3 < 0.0)
                {
                    num3 = 0.0;
                }
                string subHeading = VillageMap.createBuildTimeString((int) num3);
                this.backGround.updateSubHeading(subHeading);
            }
        }
    }
}

