namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class VassalAttackVillagePanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
        private IContainer components;
        private DockableControl dockableControl;
        private int m_selectedVillage = -1;
        private CustomSelfDrawPanel.CSDLabel treasureCastleTimeoutLabel = new CustomSelfDrawPanel.CSDLabel();
        private bool wasTall = true;

        public VassalAttackVillagePanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        private void btnAttack_Click()
        {
            if (this.m_selectedVillage >= 0)
            {
                int selectedVassalVillage = InterfaceMgr.Instance.SelectedVassalVillage;
                GameEngine.Instance.preAttackSetup(InterfaceMgr.Instance.OwnSelectedVillage, selectedVassalVillage, this.m_selectedVillage);
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

        public void init(int villageID)
        {
            this.wasTall = this.isTallTreasureChestPanel(villageID);
            int num = 0;
            if (this.wasTall)
            {
                num = 60;
            }
            base.clearControls();
            CustomSelfDrawPanel.CSDImage image = this.backGround.init(this.wasTall, 0x2710);
            base.addControl(this.backGround);
            this.attackButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[1];
            this.attackButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[8];
            this.attackButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[15];
            this.attackButton.Position = new Point(80, 0x31 + num);
            this.attackButton.Enabled = false;
            this.attackButton.CustomTooltipID = 0x96b;
            this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAttack_Click), "VassalAttackVillagePanel2_attack");
            image.addControl(this.attackButton);
            this.treasureCastleTimeoutLabel.Text = "";
            this.treasureCastleTimeoutLabel.Color = ARGBColors.Black;
            this.treasureCastleTimeoutLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.treasureCastleTimeoutLabel.Position = new Point(10, 50);
            this.treasureCastleTimeoutLabel.Size = new Size(image.Width - 20, 80);
            this.treasureCastleTimeoutLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.treasureCastleTimeoutLabel.Visible = false;
            image.addControl(this.treasureCastleTimeoutLabel);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "VassalAttackVillagePanel2";
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

        private bool isTallTreasureChestPanel(int villageID)
        {
            if ((GameEngine.Instance.World.isSpecial(villageID) && GameEngine.Instance.World.isAttackableSpecial(villageID)) && SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(villageID)))
            {
                TimeSpan span = (TimeSpan) (VillageMap.getCurrentServerTime() - GameEngine.Instance.World.getLastTreasureCastleAttackTime());
                int num2 = WorldMap.TreasureCastle_AttackGap;
                if (span.TotalSeconds < num2)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        public void update()
        {
            this.backGround.update();
            this.updateTreasureCastleTimeout();
        }

        public void updateOtherVillageText(int selectedVillage)
        {
            bool flag = false;
            if (GameEngine.Instance.World.isSpecial(selectedVillage) && GameEngine.Instance.World.isAttackableSpecial(selectedVillage))
            {
                bool flag2 = this.isTallTreasureChestPanel(selectedVillage);
                if (flag2 != this.wasTall)
                {
                    this.init(selectedVillage);
                }
                flag = flag2;
            }
            this.m_selectedVillage = selectedVillage;
            this.backGround.updateHeading(GameEngine.Instance.World.getVillageNameOrType(selectedVillage));
            this.backGround.updatePanelTypeFromVillageID(selectedVillage);
            if (selectedVillage < 0)
            {
                this.attackButton.Enabled = false;
            }
            else if (GameEngine.Instance.World.isAttackableSpecial(selectedVillage))
            {
                this.attackButton.Enabled = true;
                if (SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(selectedVillage)))
                {
                    if (GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
                    {
                        this.attackButton.Enabled = false;
                    }
                    if (flag)
                    {
                        this.updateTreasureCastleTimeout();
                        this.treasureCastleTimeoutLabel.Visible = true;
                        this.attackButton.Enabled = false;
                    }
                }
            }
            else if (!GameEngine.Instance.World.isCapital(selectedVillage) && (GameEngine.Instance.World.getVillageUserID(selectedVillage) >= 0))
            {
                this.attackButton.Enabled = true;
            }
            else
            {
                this.attackButton.Enabled = false;
            }
        }

        private void updateTreasureCastleTimeout()
        {
            if (GameEngine.Instance.World.isSpecial(this.m_selectedVillage) && GameEngine.Instance.World.isAttackableSpecial(this.m_selectedVillage))
            {
                TimeSpan span = (TimeSpan) (VillageMap.getCurrentServerTime() - GameEngine.Instance.World.getLastTreasureCastleAttackTime());
                int num = WorldMap.TreasureCastle_AttackGap;
                if (span.TotalSeconds < num)
                {
                    this.treasureCastleTimeoutLabel.TextDiffOnly = SK.Text("EmptyVillage_NextAttackAvailable", "Next Attack Available in") + " " + VillageMap.createBuildTimeString(num - ((int) span.TotalSeconds));
                }
                else
                {
                    this.treasureCastleTimeoutLabel.TextDiffOnly = "";
                    if (this.treasureCastleTimeoutLabel.Visible && !GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
                    {
                        this.attackButton.Enabled = true;
                    }
                }
            }
        }
    }
}

