namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SelectReinforcementPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
        private IContainer components;
        private DockableControl dockableControl;
        private int fromVillageID = -1;
        private CustomSelfDrawPanel.CSDButton homeVillageButton = new CustomSelfDrawPanel.CSDButton();
        private int lastState = -2;
        private WorldMap.LocalArmyData m_reinforcements;
        private CustomSelfDrawPanel.CSDButton returnButton = new CustomSelfDrawPanel.CSDButton();
        private long selectedReinforcementID;
        private CustomSelfDrawPanel.CSDButton targetVillageButton = new CustomSelfDrawPanel.CSDButton();
        private int toVillageID = -1;
        private CustomSelfDrawPanel.CSDImage travelDirection = new CustomSelfDrawPanel.CSDImage();

        public SelectReinforcementPanel2()
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
            if ((this.m_reinforcements != null) && (this.fromVillageID >= 0))
            {
                GameEngine.Instance.World.zoomToVillage(this.fromVillageID);
            }
        }

        public void init()
        {
            base.clearControls();
            CustomSelfDrawPanel.CSDImage image = this.backGround.init(true, 0x3eb);
            this.backGround.updateHeading(SK.Text("SelectArmyPanel_Reinforcements", "Reinforcements"));
            this.backGround.centerSubHeading();
            base.addControl(this.backGround);
            this.backGround.initTravelButton(this.homeVillageButton);
            this.homeVillageButton.Position = new Point(11, 0x3d);
            this.homeVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.homeClick), "SelectReinforcementPanel2_home_village");
            image.addControl(this.homeVillageButton);
            this.backGround.initTravelButton(this.targetVillageButton);
            this.targetVillageButton.Position = new Point(11, 0x77);
            this.targetVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.targetClick), "SelectReinforcementPanel2_target_village");
            image.addControl(this.targetVillageButton);
            this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
            this.travelDirection.Alpha = 0.5f;
            this.travelDirection.Position = new Point(0x58, 90);
            image.addControl(this.travelDirection);
            this.returnButton.ImageNorm = (Image) GFXLibrary.mrhp_button_150x25[0];
            this.returnButton.ImageOver = (Image) GFXLibrary.mrhp_button_150x25[1];
            this.returnButton.ImageClick = (Image) GFXLibrary.mrhp_button_150x25[2];
            this.returnButton.Position = new Point(0x1a, 0x9b);
            this.returnButton.Text.Text = "";
            this.returnButton.TextYOffset = -3;
            this.returnButton.Text.Color = ARGBColors.Black;
            this.returnButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.returnButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.returnClick), "SelectReinforcementPanel2_return");
            this.returnButton.Visible = false;
            image.addControl(this.returnButton);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "SelectReinforcementPanel2";
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        public void reinforcementSelected(long reinforcementID)
        {
            WorldMap.LocalArmyData data = GameEngine.Instance.World.getReinforcement(reinforcementID);
            if (data != null)
            {
                this.selectedReinforcementID = reinforcementID;
                this.m_reinforcements = data;
                this.lastState = -2;
                this.update();
            }
            else
            {
                InterfaceMgr.Instance.closeReinforcementSelectedPanel();
                this.m_reinforcements = null;
            }
        }

        private void returnClick()
        {
            if (this.m_reinforcements != null)
            {
                RemoteServices.Instance.set_ReturnReinforcements_UserCallBack(new RemoteServices.ReturnReinforcements_UserCallBack(this.returnReinforcementsCallBack));
                RemoteServices.Instance.ReturnReinforcements(this.m_reinforcements.armyID);
                this.returnButton.Visible = false;
            }
        }

        private void returnReinforcementsCallBack(ReturnReinforcements_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.armyData != null)
                {
                    GameEngine.Instance.World.addReinforcementArmy(returnData.armyData);
                }
                if (returnData.armyData2 != null)
                {
                    GameEngine.Instance.World.addReinforcementArmy(returnData.armyData2);
                }
                this.update();
                this.returnButton.Visible = false;
            }
        }

        private void targetClick()
        {
            if ((this.m_reinforcements != null) && (this.toVillageID >= 0))
            {
                GameEngine.Instance.World.zoomToVillage(this.toVillageID);
            }
        }

        public void update()
        {
            this.backGround.update();
            this.m_reinforcements = GameEngine.Instance.World.getReinforcement(this.selectedReinforcementID);
            if (this.m_reinforcements != null)
            {
                if (this.m_reinforcements.dead)
                {
                    this.m_reinforcements = null;
                    InterfaceMgr.Instance.closeReinforcementSelectedPanel();
                }
                else
                {
                    if (this.m_reinforcements.attackType != this.lastState)
                    {
                        this.backGround.updateTravelButton(this.homeVillageButton, this.m_reinforcements.homeVillageID);
                        this.backGround.updateTravelButton(this.targetVillageButton, this.m_reinforcements.targetVillageID);
                        this.fromVillageID = this.m_reinforcements.homeVillageID;
                        this.toVillageID = this.m_reinforcements.targetVillageID;
                        this.lastState = this.m_reinforcements.attackType;
                        if (this.lastState == 20)
                        {
                            this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
                            if (GameEngine.Instance.World.isUserVillage(this.m_reinforcements.homeVillageID))
                            {
                                this.returnButton.Visible = true;
                            }
                            else
                            {
                                this.returnButton.Visible = false;
                            }
                            if (GameEngine.Instance.World.isUserVillage(this.m_reinforcements.homeVillageID))
                            {
                                this.returnButton.Text.TextDiffOnly = SK.Text("SelectArmyPanel_Retrieve", "Retrieve");
                            }
                            else
                            {
                                this.returnButton.Text.TextDiffOnly = SK.Text("SelectArmyPanel_Return", "Return");
                            }
                        }
                        else
                        {
                            this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[1];
                            this.returnButton.Visible = false;
                        }
                    }
                    double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
                    double num2 = this.m_reinforcements.localEndTime - num;
                    if (num2 < 0.0)
                    {
                        this.backGround.updateSubHeading("");
                    }
                    else
                    {
                        string subHeading = VillageMap.createBuildTimeString((int) num2);
                        this.backGround.updateSubHeading(subHeading);
                    }
                }
            }
            else
            {
                InterfaceMgr.Instance.closeReinforcementSelectedPanel();
            }
        }
    }
}

