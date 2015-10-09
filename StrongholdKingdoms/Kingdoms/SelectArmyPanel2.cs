namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SelectArmyPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
        private IContainer components;
        private DockableControl dockableControl;
        private bool forceReturnOff;
        private int fromVillageID = -1;
        private CustomSelfDrawPanel.CSDButton homeVillageButton = new CustomSelfDrawPanel.CSDButton();
        private int lastState = -2;
        private WorldMap.LocalArmyData m_army;
        private CustomSelfDrawPanel.CSDButton returnButton = new CustomSelfDrawPanel.CSDButton();
        private long selectedArmyID;
        private CustomSelfDrawPanel.CSDButton targetVillageButton = new CustomSelfDrawPanel.CSDButton();
        private int toVillageID = -1;
        private CustomSelfDrawPanel.CSDImage travelDirection = new CustomSelfDrawPanel.CSDImage();

        public SelectArmyPanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        public void armySelected(long armyID)
        {
            WorldMap.LocalArmyData data = GameEngine.Instance.World.getArmy(armyID);
            if (data != null)
            {
                this.selectedArmyID = armyID;
                this.m_army = data;
                this.lastState = -2;
                this.update();
            }
            else
            {
                InterfaceMgr.Instance.closeArmySelectedPanel();
                this.m_army = null;
            }
        }

        private void cancelCastleAttackCallBack(CancelCastleAttack_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.armyData != null)
                {
                    ArmyReturnData[] armyReturnData = new ArmyReturnData[] { returnData.armyData };
                    GameEngine.Instance.World.doGetArmyData(armyReturnData, null, false);
                    GameEngine.Instance.World.addExistingArmy(returnData.armyData.armyID);
                    GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
                    GameEngine.Instance.World.deleteArmy(returnData.oldArmyID);
                    if (SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(returnData.armyData.targetVillageID)))
                    {
                        GameEngine.Instance.World.setLastTreasureCastleAttackTime(DateTime.MinValue);
                    }
                }
                this.update();
                this.returnButton.Visible = false;
            }
            else
            {
                this.forceReturnOff = false;
            }
        }

        private void cancelClick()
        {
            if (this.m_army != null)
            {
                this.returnButton.Visible = false;
                this.forceReturnOff = true;
                RemoteServices.Instance.set_CancelCastleAttack_UserCallBack(new RemoteServices.CancelCastleAttack_UserCallBack(this.cancelCastleAttackCallBack));
                RemoteServices.Instance.CancelCastleAttack(this.m_army.armyID);
            }
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
            if ((this.m_army != null) && (this.fromVillageID >= 0))
            {
                GameEngine.Instance.World.zoomToVillage(this.fromVillageID);
            }
        }

        public void init()
        {
            base.clearControls();
            CustomSelfDrawPanel.CSDImage image = this.backGround.init(true, 0x3e8);
            this.backGround.centerSubHeading();
            base.addControl(this.backGround);
            this.backGround.initTravelButton(this.homeVillageButton);
            this.homeVillageButton.Position = new Point(11, 0x3d);
            this.homeVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.homeClick), "SelectArmyPanel2_home_village");
            image.addControl(this.homeVillageButton);
            this.backGround.initTravelButton(this.targetVillageButton);
            this.targetVillageButton.Position = new Point(11, 0x77);
            this.targetVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.targetClick), "SelectArmyPanel2_target_village");
            image.addControl(this.targetVillageButton);
            this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
            this.travelDirection.Position = new Point(0x58, 90);
            this.travelDirection.Alpha = 0.5f;
            image.addControl(this.travelDirection);
            this.returnButton.ImageNorm = (Image) GFXLibrary.mrhp_button_150x25[0];
            this.returnButton.ImageOver = (Image) GFXLibrary.mrhp_button_150x25[1];
            this.returnButton.ImageClick = (Image) GFXLibrary.mrhp_button_150x25[2];
            this.returnButton.Position = new Point(0x1a, 0x9b);
            this.returnButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.returnButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.returnButton.TextYOffset = -3;
            this.returnButton.Text.Color = ARGBColors.Black;
            this.returnButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelClick), "SelectArmyPanel2_cancel");
            this.returnButton.Visible = false;
            image.addControl(this.returnButton);
            this.forceReturnOff = false;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "SelectArmyPanel2";
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

        private void targetClick()
        {
            if ((this.m_army != null) && (this.toVillageID >= 0))
            {
                GameEngine.Instance.World.zoomToVillage(this.toVillageID);
            }
        }

        private bool targetIsAI(int villageID)
        {
            return (GameEngine.Instance.World.getSpecial(villageID) != 0);
        }

        public void update()
        {
            this.backGround.update();
            this.m_army = GameEngine.Instance.World.getArmy(this.selectedArmyID);
            if (this.m_army == null)
            {
                InterfaceMgr.Instance.closeArmySelectedPanel();
            }
            else if (this.m_army.dead)
            {
                this.m_army = null;
                InterfaceMgr.Instance.closeArmySelectedPanel();
            }
            else
            {
                double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
                double num2 = this.m_army.localEndTime - num;
                if (num2 < 0.0)
                {
                    num2 = 0.0;
                }
                string subHeading = VillageMap.createBuildTimeString((int) num2);
                if ((!GameEngine.Instance.World.isUserVillage(this.m_army.homeVillageID) || ((((this.m_army.localStartTime + (GameEngine.Instance.LocalWorldData.AttackCancelDuration * 60)) < num) && !this.m_army.isScouts()) && !this.targetIsAI(this.m_army.targetVillageID))) || ((this.m_army.lootType >= 0) || this.forceReturnOff))
                {
                    this.returnButton.Visible = false;
                }
                else
                {
                    this.returnButton.Visible = true;
                }
                if (this.m_army.lootType != this.lastState)
                {
                    bool flag = false;
                    if ((this.m_army.attackType == 30) || (this.m_army.attackType == 0x1f))
                    {
                        this.backGround.updateHeading(SK.Text("SelectArmyPanel_Troops", "Troops"));
                        this.backGround.updatePanelType(0x3e8);
                    }
                    else if (this.m_army.attackType == 0x11)
                    {
                        this.backGround.updateHeading(SK.Text("GENERIC_Invasion", "Invasion"));
                        this.backGround.updatePanelType(0x3e8);
                    }
                    else if (!this.m_army.isScouts())
                    {
                        this.backGround.updateHeading(SK.Text("SelectArmyPanel_Army", "Army"));
                        this.backGround.updatePanelType(0x3e8);
                    }
                    else
                    {
                        flag = true;
                        this.backGround.updatePanelType(0x3ea);
                        this.backGround.updateHeading(SK.Text("SelectArmyPanel_Scouts", "Scouts"));
                    }
                    this.lastState = this.m_army.lootType;
                    if (this.lastState >= 0)
                    {
                        this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[1];
                        this.backGround.updatePanelText(SK.Text("SelectArmyPanel_Returning", "Returning"));
                        if (this.m_army.attackType != 13)
                        {
                            this.backGround.updateTravelButton(this.homeVillageButton, this.m_army.travelFromVillageID);
                        }
                        else
                        {
                            this.backGround.updateTravelButton(this.homeVillageButton, SK.Text("SelectArmyPanel_Tutorial", "Tutorial"));
                        }
                        this.backGround.updateTravelButton(this.targetVillageButton, this.m_army.targetVillageID);
                        if (flag && (this.homeVillageButton.Text.Text.Length == 0))
                        {
                            this.backGround.updateTravelButton(this.targetVillageButton, SK.Text("GENERIC_Unknown", "Unknown"));
                        }
                        this.fromVillageID = this.m_army.travelFromVillageID;
                        this.toVillageID = this.m_army.targetVillageID;
                        this.returnButton.Visible = false;
                    }
                    else
                    {
                        this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
                        if ((this.m_army.attackType == 30) || (this.m_army.attackType == 0x1f))
                        {
                            this.backGround.updatePanelText(SK.Text("SelectArmyPanel_Stationing", "Stationing"));
                        }
                        else if (flag)
                        {
                            this.backGround.updatePanelText(SK.Text("SelectArmyPanel_Scouting", "Scouting"));
                        }
                        else if (!GameEngine.Instance.LocalWorldData.AIWorld || (this.m_army.attackType != 0x11))
                        {
                            this.backGround.updatePanelText(SK.Text("GENERIC_Attacking", "Attacking"));
                        }
                        else
                        {
                            bool flag2 = false;
                            int special = GameEngine.Instance.World.getVillageData(this.m_army.travelFromVillageID).special;
                            int num4 = GameEngine.Instance.World.getVillageData(this.m_army.targetVillageID).special;
                            if (special == 30)
                            {
                                switch (num4)
                                {
                                    case 7:
                                    case 8:
                                    case 9:
                                    case 10:
                                    case 11:
                                    case 12:
                                    case 13:
                                    case 14:
                                        this.backGround.updatePanelText(SK.Text("BARRACKS_Reinforcing", "Reinforcing"));
                                        flag2 = true;
                                        break;
                                }
                            }
                            if (!flag2)
                            {
                                this.backGround.updatePanelText(SK.Text("GENERIC_Attacking", "Attacking"));
                            }
                        }
                        if (this.m_army.attackType != 13)
                        {
                            this.backGround.updateTravelButton(this.homeVillageButton, this.m_army.travelFromVillageID);
                        }
                        else
                        {
                            this.backGround.updateTravelButton(this.homeVillageButton, SK.Text("SelectArmyPanel_Tutorial", "Tutorial"));
                        }
                        this.backGround.updateTravelButton(this.targetVillageButton, this.m_army.targetVillageID);
                        if (flag && (this.homeVillageButton.Text.Text.Length == 0))
                        {
                            this.backGround.updateTravelButton(this.targetVillageButton, SK.Text("GENERIC_Unknown", "Unknown"));
                        }
                        this.fromVillageID = this.m_army.travelFromVillageID;
                        this.toVillageID = this.m_army.targetVillageID;
                    }
                }
                this.backGround.updateSubHeading(subHeading);
            }
        }
    }
}

