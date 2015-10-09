namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PersonInfoPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDButton homeVillageButton = new CustomSelfDrawPanel.CSDButton();
        private int lastState = -1;
        private WorldMap.LocalPerson m_person;
        private CustomSelfDrawPanel.CSDButton targetVillageButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage travelDirection = new CustomSelfDrawPanel.CSDImage();

        public PersonInfoPanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        private void btnFromVillage_Click(object sender, EventArgs e)
        {
            if (this.m_person != null)
            {
                if (((this.m_person.person.state == 1) || (this.m_person.person.state == 11)) || (((this.m_person.person.state == 0x15) || (this.lastState == 0x1f)) || (this.lastState == 0x4b)))
                {
                    GameEngine.Instance.World.zoomToVillage(this.m_person.person.homeVillageID);
                }
                else if (this.m_person.person.state == 50)
                {
                    GameEngine.Instance.World.zoomToVillage(this.m_person.person.targetVillageID);
                }
            }
        }

        private void btnToVillage_Click(object sender, EventArgs e)
        {
            if (this.m_person != null)
            {
                if (this.m_person.person.state == 50)
                {
                    GameEngine.Instance.World.zoomToVillage(this.m_person.person.homeVillageID);
                }
                else if (((this.m_person.person.state == 1) || (this.m_person.person.state == 11)) || (((this.m_person.person.state == 0x15) || (this.lastState == 0x1f)) || (this.lastState == 0x4b)))
                {
                    GameEngine.Instance.World.zoomToVillage(this.m_person.person.targetVillageID);
                }
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
            if (this.m_person != null)
            {
                GameEngine.Instance.World.zoomToVillage(this.m_person.person.homeVillageID);
            }
        }

        public void init()
        {
            base.clearControls();
            CustomSelfDrawPanel.CSDImage image = this.backGround.init(true, 0x3e9);
            this.backGround.centerSubHeading();
            base.addControl(this.backGround);
            this.backGround.initTravelButton(this.homeVillageButton);
            this.homeVillageButton.Position = new Point(11, 0x3d);
            this.homeVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.homeClick), "PersonInfoPanel2_home_village");
            image.addControl(this.homeVillageButton);
            this.backGround.initTravelButton(this.targetVillageButton);
            this.targetVillageButton.Position = new Point(11, 0x77);
            this.targetVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.targetClick), "PersonInfoPanel2_target_village");
            image.addControl(this.targetVillageButton);
            this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
            this.travelDirection.Position = new Point(0x58, 90);
            this.travelDirection.Alpha = 0.5f;
            image.addControl(this.travelDirection);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "PersonInfoPanel2";
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

        public void setPerson(long personID)
        {
            WorldMap.LocalPerson person = GameEngine.Instance.World.getPerson(personID);
            if (person == null)
            {
                InterfaceMgr.Instance.closePersonInfoPanel();
            }
            else
            {
                switch (person.person.personType)
                {
                    case 4:
                        this.backGround.updatePanelType(0x3e9);
                        this.backGround.updateHeading(SK.Text("PersonInfoPanel_Monk", "Monk"));
                        break;

                    case 100:
                        this.backGround.updatePanelType(0x3ee);
                        this.backGround.updateHeading(SK.Text("PersonInfoPanel_Disease_Rat", "Disease Rat"));
                        break;
                }
                this.m_person = person;
                this.lastState = -1;
                this.update();
            }
        }

        private void targetClick()
        {
            if (this.m_person != null)
            {
                GameEngine.Instance.World.zoomToVillage(this.m_person.person.targetVillageID);
            }
        }

        public void update()
        {
            this.backGround.update();
            if ((this.m_person != null) && !this.m_person.dying)
            {
                if (this.m_person.person.state != this.lastState)
                {
                    this.backGround.updateTravelButton(this.homeVillageButton, this.m_person.person.homeVillageID);
                    this.backGround.updateTravelButton(this.targetVillageButton, this.m_person.person.targetVillageID);
                    this.lastState = this.m_person.person.state;
                    if (this.lastState == 0)
                    {
                        InterfaceMgr.Instance.closePersonInfoPanel();
                        return;
                    }
                    if (((this.lastState == 1) || (this.lastState == 11)) || (((this.lastState == 0x15) || (this.lastState == 0x1f)) || (this.lastState == 0x4b)))
                    {
                        this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
                    }
                    else if (this.lastState == 50)
                    {
                        this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[1];
                    }
                }
                double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
                double num2 = this.m_person.localEndTime - num;
                string subHeading = VillageMap.createBuildTimeString((int) num2);
                this.backGround.updateSubHeading(subHeading);
            }
            else
            {
                InterfaceMgr.Instance.closePersonInfoPanel();
            }
        }
    }
}

