namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MapFilterSelectPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDButton filterButton = new CustomSelfDrawPanel.CSDButton();

        public MapFilterSelectPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        private void attackClick()
        {
            InterfaceMgr.Instance.openAttackTargetsPopup();
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.clearControls();
            InterfaceMgr.Instance.showMapFilterSelectPanel(true, true);
            InterfaceMgr.Instance.selectCurrentUserVillage();
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

        private void filterClick()
        {
            InterfaceMgr.Instance.showMapFilterPanel();
            InterfaceMgr.Instance.showMapFilterSelectPanel(true, false, true, true);
        }

        public void init(bool showAsOpen, bool doubleHeight)
        {
            base.clearControls();
            if (doubleHeight)
            {
                this.filterButton.MoveOnClick = true;
                this.filterButton.ImageClick = null;
                this.filterButton.Position = new Point(8, 0x20);
                if (showAsOpen)
                {
                    if (GameEngine.Instance.World.worldMapFilter.FilterActive)
                    {
                        this.filterButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_normal;
                        this.filterButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_over;
                        this.filterButton.Text.Text = SK.Text("MapFilterSelectPanel_Filter_Active", "Filter Active");
                        this.filterButton.CustomTooltipID = 0x5d;
                    }
                    else
                    {
                        this.filterButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_off_normal;
                        this.filterButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_off_over;
                        this.filterButton.Text.Text = SK.Text("MapFilterSelectPanel_Map_Filtering", "Map Filtering");
                        this.filterButton.CustomTooltipID = 0x5b;
                    }
                    this.filterButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.filterClick), "MapFilterSelectPanel_filter");
                }
                else
                {
                    this.filterButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_off_normal;
                    this.filterButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_off_over;
                    this.filterButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "MapFilterSelectPanel_filter");
                    this.filterButton.Text.Text = SK.Text("GENERIC_Close", "Close");
                    this.filterButton.CustomTooltipID = 0x5c;
                }
                this.filterButton.Text.Color = ARGBColors.Black;
                this.filterButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.filterButton.Text.Size = new Size(130, 0x34);
                this.filterButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.filterButton.Text.Position = new Point(0x27, -10);
                base.addControl(this.filterButton);
                if (showAsOpen)
                {
                    this.attackButton.Position = new Point(8, 2);
                    this.attackButton.ImageNorm = (Image) GFXLibrary.mrhp_button_attack_normal;
                    this.attackButton.ImageOver = (Image) GFXLibrary.mrhp_button_attack_over;
                    this.attackButton.Text.Text = SK.Text("Attack_Targets", "Attack Targets");
                    this.attackButton.Text.Color = ARGBColors.Black;
                    this.attackButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    this.attackButton.Text.Size = new Size(130, 0x34);
                    this.attackButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.attackButton.Text.Position = new Point(0x27, -10);
                    this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.attackClick), "MapFilterSelectPanel_filter");
                    this.attackButton.CustomTooltipID = 0x5e;
                    base.addControl(this.attackButton);
                }
                base.Size = new Size(0xbb, 0x39);
            }
            else
            {
                this.filterButton.Position = new Point(8, 2);
                if (showAsOpen)
                {
                    if (GameEngine.Instance.World.worldMapFilter.FilterActive)
                    {
                        this.filterButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_off[6];
                        this.filterButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_off[7];
                        this.filterButton.ImageClick = (Image) GFXLibrary.mrhp_button_filter_off[8];
                        this.filterButton.Text.Text = "";
                        this.filterButton.CustomTooltipID = 0x5d;
                    }
                    else
                    {
                        this.filterButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_off[3];
                        this.filterButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_off[4];
                        this.filterButton.ImageClick = (Image) GFXLibrary.mrhp_button_filter_off[5];
                        this.filterButton.Text.Text = "";
                        this.filterButton.CustomTooltipID = 0x5b;
                    }
                    this.filterButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.filterClick), "MapFilterSelectPanel_filter");
                }
                else
                {
                    this.filterButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_off[3];
                    this.filterButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_off[4];
                    this.filterButton.ImageClick = (Image) GFXLibrary.mrhp_button_filter_off[5];
                    this.filterButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "MapFilterSelectPanel_filter");
                    this.filterButton.Text.Text = "";
                    this.filterButton.CustomTooltipID = 0x5c;
                }
                base.addControl(this.filterButton);
                this.attackButton.Position = new Point(0x66, 2);
                this.attackButton.Text.Text = "";
                this.attackButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_off[9];
                this.attackButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_off[10];
                this.attackButton.ImageClick = (Image) GFXLibrary.mrhp_button_filter_off[11];
                this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.attackClick), "MapFilterSelectPanel_filter");
                this.attackButton.CustomTooltipID = 0x5e;
                base.addControl(this.attackButton);
                base.Size = new Size(0xbb, 0x1b);
            }
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "MapFilterSelectPanel";
            base.Size = new Size(0xbb, 0x1b);
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

        public void setPosition(int x, int y)
        {
            this.dockableControl.setPosition(x, y);
        }
    }
}

