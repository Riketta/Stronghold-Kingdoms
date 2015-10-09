namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MapFilterPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton aiButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
        private CustomSelfDrawPanel.CSDImage backImage;
        private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton clearButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel diplomacyLabel = new CustomSelfDrawPanel.CSDLabel();
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDButton factionButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDCheckBox factionSymbols = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDButton houseButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDCheckBox houseSymbols = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDButton scoutButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton searchButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage selectedImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDCheckBox userSymbols = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox yourVillages = new CustomSelfDrawPanel.CSDCheckBox();

        public MapFilterPanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        private void aiClick()
        {
            GameEngine.Instance.World.worldMapFilter.setFilterMode(8);
            this.selectedImage.Position = this.aiButton.Position;
            this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
            this.selectedImage.Visible = true;
            this.backImage.invalidate();
        }

        private void attackClick()
        {
            GameEngine.Instance.World.worldMapFilter.setFilterMode(6);
            this.selectedImage.Position = this.attackButton.Position;
            this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
            this.selectedImage.Visible = true;
            this.backImage.invalidate();
        }

        private void clearFilter()
        {
            GameEngine.Instance.World.worldMapFilter.setFilterMode(0);
            this.selectedImage.Visible = false;
            this.backImage.invalidate();
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

        private void factionClick()
        {
            if (RemoteServices.Instance.UserFactionID >= 0)
            {
                GameEngine.Instance.World.worldMapFilter.setFilterMode(1);
            }
            else
            {
                GameEngine.Instance.World.worldMapFilter.setFilterMode(7);
            }
            this.selectedImage.Position = this.factionButton.Position;
            this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
            this.selectedImage.Visible = true;
            this.backImage.invalidate();
        }

        private void factionToggled()
        {
            GameEngine.Instance.World.worldMapFilter.FilterShowFactionSymbols = this.factionSymbols.Checked;
        }

        private void houseClick()
        {
            GameEngine.Instance.World.worldMapFilter.setFilterMode(2);
            this.selectedImage.Position = this.houseButton.Position;
            this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
            this.selectedImage.Visible = true;
            this.backImage.invalidate();
        }

        private void houseToggled()
        {
            GameEngine.Instance.World.worldMapFilter.FilterShowHouseSymbols = this.houseSymbols.Checked;
        }

        public void init()
        {
            base.clearControls();
            this.backImage = this.backGround.init(true, 0x5e2);
            this.backGround.updateHeading(SK.Text("MapFilterSelectPanel_Map_Filtering", "Map Filtering"));
            base.addControl(this.backGround);
            this.backGround.stretchBackground();
            this.selectedImage.Image = (Image) GFXLibrary.mrhp_world_icons_filter_selected;
            this.selectedImage.Position = new Point(6, 0x2d);
            this.selectedImage.Visible = false;
            this.backImage.addControl(this.selectedImage);
            this.tradeButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0];
            this.tradeButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[7];
            this.tradeButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[14];
            this.tradeButton.Position = new Point(0x73, 0x2a);
            this.tradeButton.CustomTooltipID = 0x996;
            this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tradeClick), "MapFilterPanel2_trade");
            this.backImage.addControl(this.tradeButton);
            this.attackButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[1];
            this.attackButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[8];
            this.attackButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[15];
            this.attackButton.Position = new Point(80, 0x4c);
            this.attackButton.CustomTooltipID = 0x997;
            this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.attackClick), "MapFilterPanel2_attack");
            this.backImage.addControl(this.attackButton);
            this.scoutButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[3];
            this.scoutButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[10];
            this.scoutButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x11];
            this.scoutButton.Position = new Point(0x2d, 0x4c);
            this.scoutButton.CustomTooltipID = 0x998;
            this.scoutButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.scoutsClick), "MapFilterPanel2_scout");
            this.backImage.addControl(this.scoutButton);
            this.houseButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[6];
            this.houseButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[13];
            this.houseButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[20];
            this.houseButton.Position = new Point(0x2d, 0x2a);
            this.houseButton.CustomTooltipID = 0x999;
            this.houseButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClick), "MapFilterPanel2_house");
            this.backImage.addControl(this.houseButton);
            this.factionButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x24];
            this.factionButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x25];
            this.factionButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x26];
            this.factionButton.Position = new Point(80, 0x2a);
            if (RemoteServices.Instance.UserFactionID >= 0)
            {
                this.factionButton.CustomTooltipID = 0x99a;
            }
            else
            {
                this.factionButton.CustomTooltipID = 0x99d;
            }
            this.factionButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClick), "MapFilterPanel2_faction");
            this.backImage.addControl(this.factionButton);
            this.aiButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_ai[0];
            this.aiButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_ai[1];
            this.aiButton.ImageClick = (Image) GFXLibrary.mrhp_button_filter_ai[2];
            this.aiButton.Position = new Point(0x73, 0x4c);
            this.aiButton.CustomTooltipID = 0x99e;
            this.aiButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.aiClick), "MapFilterPanel2_ai");
            this.backImage.addControl(this.aiButton);
            this.yourVillages.CheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[0];
            this.yourVillages.UncheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[1];
            this.yourVillages.Position = new Point(15, 0x75);
            this.yourVillages.Checked = GameEngine.Instance.World.worldMapFilter.FilterAlwaysShowYourVillages;
            this.yourVillages.CBLabel.Text = SK.Text("MapFilterPanel_Always_Show_Your_Villages", "Always Show Your Villages");
            this.yourVillages.CBLabel.Color = ARGBColors.Black;
            this.yourVillages.CBLabel.Position = new Point(20, -1);
            this.yourVillages.CBLabel.Size = new Size(180, 0x19);
            this.yourVillages.CBLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.yourVillages.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.yourToggled));
            this.backImage.addControl(this.yourVillages);
            this.diplomacyLabel.Text = SK.Text("MapFilterPanel_Diplomacy", "Diplomacy Symbols");
            this.diplomacyLabel.Position = new Point(5, 0x89);
            this.diplomacyLabel.Color = ARGBColors.Black;
            this.diplomacyLabel.Size = new Size(180, 0x19);
            this.diplomacyLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            this.diplomacyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.backImage.addControl(this.diplomacyLabel);
            this.houseSymbols.CheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[0];
            this.houseSymbols.UncheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[1];
            this.houseSymbols.Position = new Point(15, 0x9d);
            this.houseSymbols.Checked = GameEngine.Instance.World.worldMapFilter.FilterShowHouseSymbols;
            this.houseSymbols.CBLabel.Text = SK.Text("MapFilterPanel_Show_House_Symbols", "Show House Symbols");
            this.houseSymbols.CBLabel.Color = ARGBColors.Black;
            this.houseSymbols.CBLabel.Position = new Point(20, -1);
            this.houseSymbols.CBLabel.Size = new Size(180, 0x19);
            this.houseSymbols.CBLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.houseSymbols.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.houseToggled));
            this.backImage.addControl(this.houseSymbols);
            this.factionSymbols.CheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[0];
            this.factionSymbols.UncheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[1];
            this.factionSymbols.Position = new Point(15, 0xb1);
            this.factionSymbols.Checked = GameEngine.Instance.World.worldMapFilter.FilterShowFactionSymbols;
            this.factionSymbols.CBLabel.Text = SK.Text("MapFilterPanel_Show_Faction_Symbols", "Show Faction Symbols");
            this.factionSymbols.CBLabel.Color = ARGBColors.Black;
            this.factionSymbols.CBLabel.Position = new Point(20, -1);
            this.factionSymbols.CBLabel.Size = new Size(180, 0x19);
            this.factionSymbols.CBLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.factionSymbols.Data = 0;
            this.factionSymbols.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.factionToggled));
            this.backImage.addControl(this.factionSymbols);
            this.userSymbols.CheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[0];
            this.userSymbols.UncheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[1];
            this.userSymbols.Position = new Point(15, 0xc5);
            this.userSymbols.Checked = GameEngine.Instance.World.worldMapFilter.FilterShowUserSymbols;
            this.userSymbols.CBLabel.Text = SK.Text("MapFilterPanel_Show_User_Symbols", "Show Player Symbols");
            this.userSymbols.CBLabel.Color = ARGBColors.Black;
            this.userSymbols.CBLabel.Position = new Point(20, -1);
            this.userSymbols.CBLabel.Size = new Size(180, 0x19);
            this.userSymbols.CBLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.userSymbols.Data = 0;
            this.userSymbols.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.userToggled));
            this.backImage.addControl(this.userSymbols);
            this.searchButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_search[0];
            this.searchButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_search[1];
            this.searchButton.ImageClick = (Image) GFXLibrary.mrhp_button_filter_search[2];
            this.searchButton.Position = new Point(0x67, 0xd7);
            this.searchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.searchFilter), "StatsPanel_search");
            this.searchButton.CustomTooltipID = 0x99c;
            this.backImage.addControl(this.searchButton);
            this.clearButton.ImageNorm = (Image) GFXLibrary.mrhp_button_filter_off[0];
            this.clearButton.ImageOver = (Image) GFXLibrary.mrhp_button_filter_off[1];
            this.clearButton.ImageClick = (Image) GFXLibrary.mrhp_button_filter_off[2];
            this.clearButton.Position = new Point(0x13, 0xd7);
            this.clearButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clearFilter), "MapFilterPanel2_clear");
            this.clearButton.CustomTooltipID = 0x99b;
            this.backImage.addControl(this.clearButton);
            this.cancelButton.ImageNorm = (Image) GFXLibrary.mrhp_button_80_normal;
            this.cancelButton.ImageOver = (Image) GFXLibrary.mrhp_button_80_over;
            this.cancelButton.ImageClick = (Image) GFXLibrary.mrhp_button_80_pushed;
            this.cancelButton.Position = new Point(0x67, 0xd7);
            this.cancelButton.Text.Text = SK.Text("GENERIC_Close", "Close");
            this.cancelButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.cancelButton.Text.Size = new Size(this.cancelButton.ImageNorm.Size.Width - 6, this.cancelButton.ImageNorm.Size.Height);
            this.cancelButton.TextYOffset = -3;
            this.cancelButton.Text.Color = ARGBColors.Black;
            this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "MapFilterPanel2_close");
            this.backImage.invalidate();
            InterfaceMgr.Instance.closeArmySelectedPanel();
            InterfaceMgr.Instance.closeTraderInfoPanel();
            InterfaceMgr.Instance.closeReinforcementSelectedPanel();
            InterfaceMgr.Instance.closePersonInfoPanel();
            InterfaceMgr.Instance.closeSelectedVillagePanel();
            int filterMode = GameEngine.Instance.World.worldMapFilter.FilterMode;
            if (GameEngine.Instance.World.worldMapFilter.FilterActive)
            {
                switch (filterMode)
                {
                    case 0:
                        return;

                    case 1:
                    case 7:
                        this.selectedImage.Position = this.factionButton.Position;
                        this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
                        this.selectedImage.Visible = true;
                        return;

                    case 2:
                        this.selectedImage.Position = this.houseButton.Position;
                        this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
                        this.selectedImage.Visible = true;
                        return;

                    case 3:
                        this.selectedImage.Position = this.scoutButton.Position;
                        this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
                        this.selectedImage.Visible = true;
                        return;

                    case 4:
                        this.selectedImage.Position = this.tradeButton.Position;
                        this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
                        this.selectedImage.Visible = true;
                        return;

                    case 5:
                        this.selectedImage.Position = this.tradeButton.Position;
                        this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
                        this.selectedImage.Visible = true;
                        return;

                    case 6:
                        this.selectedImage.Position = this.attackButton.Position;
                        this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
                        this.selectedImage.Visible = true;
                        return;

                    case 8:
                        this.selectedImage.Position = this.aiButton.Position;
                        this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
                        this.selectedImage.Visible = true;
                        return;
                }
            }
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "MapFilterPanel2";
            base.Size = new Size(0xc7, 0x111);
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

        private void scoutsClick()
        {
            GameEngine.Instance.World.worldMapFilter.setFilterMode(3);
            this.selectedImage.Position = this.scoutButton.Position;
            this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
            this.selectedImage.Visible = true;
            this.backImage.invalidate();
        }

        private void searchFilter()
        {
            SearchForVillagePopup popup = new SearchForVillagePopup();
            popup.ShowDialog(InterfaceMgr.Instance.ParentForm);
            popup.Dispose();
        }

        private void tradeClick()
        {
            GameEngine.Instance.World.worldMapFilter.setFilterMode(4);
            this.selectedImage.Position = this.tradeButton.Position;
            this.selectedImage.Position = new Point(this.selectedImage.Position.X - 5, this.selectedImage.Position.Y - 5);
            this.selectedImage.Visible = true;
            this.backImage.invalidate();
        }

        public void update()
        {
            this.backGround.update();
        }

        private void userToggled()
        {
            GameEngine.Instance.World.worldMapFilter.FilterShowUserSymbols = this.userSymbols.Checked;
        }

        private void yourToggled()
        {
            GameEngine.Instance.World.worldMapFilter.FilterAlwaysShowYourVillages = this.yourVillages.Checked;
        }
    }
}

