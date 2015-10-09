namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class UserinfoScreenLine : UserControl
    {
        private IContainer components;
        private Label lblName;
        private int m_villageID = -1;

        public UserinfoScreenLine()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(string villageName, int villageID)
        {
            this.m_villageID = villageID;
            this.lblName.Text = villageName;
            if (GameEngine.Instance.World.isRegionCapital(villageID))
            {
                this.lblName.Text = this.lblName.Text + " - (" + SK.Text("UserinfoScreenLine_Parish_Steward", "Parish Steward") + ")";
            }
            else if (GameEngine.Instance.World.isCountyCapital(villageID))
            {
                this.lblName.Text = this.lblName.Text + " - (" + SK.Text("UserinfoScreenLine_County_Sheriff", "County Sheriff") + ")";
            }
            else if (GameEngine.Instance.World.isProvinceCapital(villageID))
            {
                this.lblName.Text = this.lblName.Text + " - (" + SK.Text("UserinfoScreenLine_Province_Governor", "Province Governor") + ")";
            }
            else if (GameEngine.Instance.World.isCountryCapital(villageID))
            {
                this.lblName.Text = this.lblName.Text + " - (" + SK.Text("UserinfoScreenLine_King", "King") + ")";
            }
        }

        private void InitializeComponent()
        {
            this.lblName = new Label();
            base.SuspendLayout();
            this.lblName.Location = new Point(3, 7);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0x139, 0x13);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Name";
            this.lblName.Click += new EventHandler(this.lblName_DoubleClick);
            base.AutoScaleMode = AutoScaleMode.None;
            base.Controls.Add(this.lblName);
            base.Name = "UserinfoScreenLine";
            base.Size = new Size(0x13c, 30);
            base.Click += new EventHandler(this.UserinfoScreenLine_DoubleClick);
            base.ResumeLayout(false);
        }

        private void lblName_DoubleClick(object sender, EventArgs e)
        {
            if (this.m_villageID >= 0)
            {
                if (RemoteServices.Instance.Admin && GameEngine.shiftPressed)
                {
                    AGUR agur = new AGUR();
                    agur.init(this.m_villageID);
                    agur.Show(InterfaceMgr.Instance.ParentForm);
                }
                else
                {
                    GameEngine.Instance.playInterfaceSound("UserinfoScreenLine_village");
                    Point point = GameEngine.Instance.World.getVillageLocation(this.m_villageID);
                    InterfaceMgr.Instance.closeParishPanel();
                    GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                    InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_villageID, false, true, true, false);
                }
            }
        }

        public bool update()
        {
            return false;
        }

        private void UserinfoScreenLine_DoubleClick(object sender, EventArgs e)
        {
            if (this.m_villageID >= 0)
            {
                if (RemoteServices.Instance.Admin && GameEngine.shiftPressed)
                {
                    AGUR agur = new AGUR();
                    agur.init(this.m_villageID);
                    agur.Show(InterfaceMgr.Instance.ParentForm);
                }
                else
                {
                    GameEngine.Instance.playInterfaceSound("UserinfoScreenLine_village");
                    Point point = GameEngine.Instance.World.getVillageLocation(this.m_villageID);
                    InterfaceMgr.Instance.closeParishPanel();
                    GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                    InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_villageID, false, true, true, false);
                }
            }
        }
    }
}

