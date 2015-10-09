namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AttackTargetSidePanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
        private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDButton okButton = new CustomSelfDrawPanel.CSDButton();
        private int selectedVillage = -1;

        public AttackTargetSidePanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        private void cancelClick()
        {
            InterfaceMgr.Instance.getMainTabBar().changeTab(0);
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

        public void init()
        {
            base.clearControls();
            CustomSelfDrawPanel.CSDImage image = this.backGround.init(false, 0x2710);
            this.backGround.updatePanelText(SK.Text("AttackTargetSidePanel_Select_Attack_Target", "Select Attack Target"));
            this.backGround.setAction(0x3e8);
            base.addControl(this.backGround);
            this.okButton.ImageNorm = (Image) GFXLibrary.mrhp_button_check_normal;
            this.okButton.ImageOver = (Image) GFXLibrary.mrhp_button_check_over;
            this.okButton.ImageClick = (Image) GFXLibrary.mrhp_button_check_pushed;
            this.okButton.Position = new Point(0x66, 0x40);
            this.okButton.Enabled = false;
            this.okButton.CustomTooltipID = 0x961;
            this.okButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectTargetClick), "AttackTargetSidePanel2_ok");
            image.addControl(this.okButton);
            this.cancelButton.ImageNorm = (Image) GFXLibrary.mrhp_button_x_normal;
            this.cancelButton.ImageOver = (Image) GFXLibrary.mrhp_button_x_over;
            this.cancelButton.ImageClick = (Image) GFXLibrary.mrhp_button_x_pushed;
            this.cancelButton.Position = new Point(20, 0x40);
            this.cancelButton.CustomTooltipID = 0x960;
            this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelClick), "AttackTargetSidePanel2_cancel");
            image.addControl(this.cancelButton);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "AttackTargetSidePanel2";
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

        private void selectTargetClick()
        {
            if (GameEngine.Instance.World.isCapital(this.selectedVillage) && GameEngine.Instance.World.isUserVillage(this.selectedVillage))
            {
                GameEngine.Instance.SkipVillageTab();
                InterfaceMgr.Instance.getMainTabBar().changeTab(1);
                InterfaceMgr.Instance.setCapitalSendTargetVillage(this.selectedVillage);
                InterfaceMgr.Instance.setVillageTabSubMode(0x11);
            }
            else
            {
                GameEngine.Instance.preAttackSetup(InterfaceMgr.Instance.OwnSelectedVillage, InterfaceMgr.Instance.OwnSelectedVillage, this.selectedVillage);
            }
        }

        public void setTarget(int villageID)
        {
            this.selectedVillage = villageID;
            if ((villageID < 0) || !GameEngine.Instance.World.isVillageVisible(villageID))
            {
                this.backGround.updateHeading("");
                this.backGround.updatePanelType(0x2710);
                this.okButton.Enabled = false;
            }
            else
            {
                this.backGround.updateHeading(GameEngine.Instance.World.getVillageNameOrType(villageID));
                this.backGround.updatePanelTypeFromVillageID(villageID);
                this.okButton.Enabled = true;
            }
        }

        public void update()
        {
            this.backGround.update();
        }
    }
}

