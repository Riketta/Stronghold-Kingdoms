namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AllVassalsPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backgroundLeftEdge = new CustomSelfDrawPanel.CSDImage();
        private int blockYSize;
        private CustomSelfDrawPanel.CSDButton btnClose = new CustomSelfDrawPanel.CSDButton();
        private VassalInfo[] cachedVassalInfo;
        private IContainer components;
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        private Panel focusPanel;
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        public static AllVassalsPanel instance;
        private VassalInfo liegeLordInfo = new VassalInfo();
        private List<ArmyLine> lineList = new List<ArmyLine>();
        private CustomSelfDrawPanel.CSDLabel outGoingFromLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private MyMessageBoxPopUp PopUpRef;
        private CustomSelfDrawPanel.CSDImage smallPeasantImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage smallPeasantImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDArea vassalScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar vassalScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private int villageIDRef;
        private CustomSelfDrawPanel.CSDLabel yourVassalsLabel = new CustomSelfDrawPanel.CSDLabel();
        private int yourVillageIDRef;

        public AllVassalsPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.focusPanel.Focus();
        }

        public void breakVassalage(int yourVillageID, int villageID)
        {
            this.villageIDRef = villageID;
            this.yourVillageIDRef = yourVillageID;
            if (MyMessageBox.Show(SK.Text("VassalControlPanel_BreakVassalage_Warning", "Breaking from your vassal will mean any troops stationed there will be lost."), SK.Text("VassalControlPanel_BreakVassalage", "Break Vassalage?"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.BreakVassalage();
            }
        }

        private void BreakVassalage()
        {
            RemoteServices.Instance.set_BreakVassalage_UserCallBack(new RemoteServices.BreakVassalage_UserCallBack(this.breakVassalageCallBack));
            RemoteServices.Instance.BreakVassalage(this.yourVillageIDRef, this.villageIDRef);
            GameEngine.Instance.World.breakVassal(this.yourVillageIDRef, this.villageIDRef);
        }

        public void breakVassalageCallBack(BreakVassalage_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.cachedVassalInfo = returnData.vassals;
                this.reAddVassals();
                GameEngine.Instance.World.updateUserVassals();
            }
            CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.initVillageTab();
            InterfaceMgr.Instance.setVillageTabSubMode(8);
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
            base.clearControls();
            this.closing();
        }

        public void closing()
        {
            InterfaceMgr.Instance.closeDonatePopup();
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

        public void init(bool resized)
        {
            int height = base.Height;
            instance = this;
            base.clearControls();
            this.backgroundImage.Image = (Image) GFXLibrary.body_background_002;
            this.backgroundImage.Size = new Size(base.Width, height - 40);
            this.backgroundImage.Tile = true;
            this.backgroundImage.Position = new Point(0, 40);
            base.addControl(this.backgroundImage);
            this.backgroundLeftEdge.Image = (Image) GFXLibrary.body_background_canvas_left_edge;
            this.backgroundLeftEdge.Position = new Point(0, 0);
            this.backgroundLeftEdge.Size = new Size(this.backgroundLeftEdge.Image.Width, height - 40);
            this.backgroundLeftEdge.Tile = true;
            this.backgroundImage.addControl(this.backgroundLeftEdge);
            this.headerImage.Size = new Size(base.Width, 40);
            this.headerImage.Position = new Point(0, 0);
            base.addControl(this.headerImage);
            this.headerImage.CreateX((Image) GFXLibrary.mail_top_drag_bar_left, (Image) GFXLibrary.mail_top_drag_bar_middle, (Image) GFXLibrary.mail_top_drag_bar_right, -2, 2);
            InterfaceMgr.Instance.getSelectedMenuVillage();
            this.parishNameLabel.Text = SK.Text("Vassals_Overview", "Vassals Overview");
            this.parishNameLabel.Color = ARGBColors.White;
            this.parishNameLabel.DropShadowColor = ARGBColors.Black;
            this.parishNameLabel.Position = new Point(20, 0);
            this.parishNameLabel.Size = new Size(base.Width - 40, 40);
            this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerImage.addControl(this.parishNameLabel);
            this.blockYSize = (height - 40) - 0x38;
            this.headerLabelsImage.Size = new Size((base.Width - 0x19) - 0x17, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 5);
            this.backgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.brown_mail2_field_bar_mail_left, (Image) GFXLibrary.brown_mail2_field_bar_mail_middle, (Image) GFXLibrary.brown_mail2_field_bar_mail_right);
            this.divider2Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider2Image.Position = new Point(580, 0);
            this.headerLabelsImage.addControl(this.divider2Image);
            this.yourVassalsLabel.Text = SK.Text("VassalControlPanel_Your_Vassals", "Your Vassals");
            this.yourVassalsLabel.Color = ARGBColors.Black;
            this.yourVassalsLabel.Position = new Point(12, -3);
            this.yourVassalsLabel.Size = new Size(0xdf, this.headerLabelsImage.Height);
            this.yourVassalsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.yourVassalsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.yourVassalsLabel);
            this.vassalScrollArea.Position = new Point(0x19, 40);
            this.vassalScrollArea.Size = new Size(0x393, (this.blockYSize - 40) - 10);
            this.vassalScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, (this.blockYSize - 40) - 10));
            this.backgroundImage.addControl(this.vassalScrollArea);
            int num2 = this.vassalScrollBar.Value;
            this.vassalScrollBar.Position = new Point(0x3af, 40);
            this.vassalScrollBar.Size = new Size(0x18, (this.blockYSize - 40) - 10);
            this.backgroundImage.addControl(this.vassalScrollBar);
            this.vassalScrollBar.Value = 0;
            this.vassalScrollBar.Max = 100;
            this.vassalScrollBar.NumVisibleLines = 0x19;
            this.vassalScrollBar.Create(null, null, null, (Image) GFXLibrary.brown_24wide_thumb_top, (Image) GFXLibrary.brown_24wide_thumb_middle, (Image) GFXLibrary.brown_24wide_thumb_bottom);
            this.vassalScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            this.smallPeasantImage.Image = (Image) GFXLibrary.armies_screen_troops;
            this.smallPeasantImage.Position = new Point(0x25b, -10);
            this.smallPeasantImage.ClipRect = new Rectangle(0, 0, (this.smallPeasantImage.Image.Width * 5) / 6, this.smallPeasantImage.Image.Height);
            this.headerLabelsImage.addControl(this.smallPeasantImage);
            if (resized)
            {
                this.vassalScrollBar.Value = num2;
            }
            this.btnClose.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
            this.btnClose.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
            this.btnClose.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
            this.btnClose.Position = new Point(base.Width - 230, ((height - 40) - 40) - 4);
            this.btnClose.Text.Text = SK.Text("GENERIC_Close", "Close");
            this.btnClose.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.btnClose.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.btnClose.TextYOffset = -3;
            this.btnClose.Text.Color = ARGBColors.Black;
            this.btnClose.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "AllVassalsPanel_close");
            this.backgroundImage.addControl(this.btnClose);
            this.cachedVassalInfo = null;
            RemoteServices.Instance.set_VassalInfo_UserCallBack(new RemoteServices.VassalInfo_UserCallBack(this.vassalInfoCallBack));
            if (GameEngine.Instance.Village != null)
            {
                RemoteServices.Instance.VassalInfo(-1);
            }
            this.reAddVassals();
        }

        private void InitializeComponent()
        {
            this.focusPanel = new Panel();
            base.SuspendLayout();
            this.focusPanel.BackColor = ARGBColors.Transparent;
            this.focusPanel.ForeColor = ARGBColors.Transparent;
            this.focusPanel.Location = new Point(0x3dc, 3);
            this.focusPanel.Name = "focusPanel";
            this.focusPanel.Size = new Size(1, 1);
            this.focusPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.Controls.Add(this.focusPanel);
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "AllVassalsPanel";
            base.Size = new Size(0x3e0, 0x236);
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

        public void logout()
        {
        }

        private void reAddVassals()
        {
            this.lineList.Clear();
            this.vassalScrollArea.clearControls();
            int y = 0;
            int position = 0;
            if (this.cachedVassalInfo != null)
            {
                foreach (VassalInfo info in this.cachedVassalInfo)
                {
                    if (y != 0)
                    {
                        y += 5;
                    }
                    ArmyLine control = new ArmyLine {
                        Position = new Point(0, y)
                    };
                    control.init(position, this, info.yourVillageID, info.villageID, info.honourPerSecond, info.stationed_Peasants, info.stationed_Archers, info.stationed_Pikemen, info.stationed_Swordsmen, info.stationed_Catapults, info.vassalPlayerName);
                    this.vassalScrollArea.addControl(control);
                    y += control.Height;
                    this.lineList.Add(control);
                    position++;
                }
            }
            this.vassalScrollArea.Size = new Size(this.vassalScrollArea.Width, y);
            if (y < this.vassalScrollBar.Height)
            {
                this.vassalScrollBar.Visible = false;
            }
            else
            {
                this.vassalScrollBar.Visible = true;
                this.vassalScrollBar.NumVisibleLines = this.vassalScrollBar.Height;
                this.vassalScrollBar.Max = y - this.vassalScrollBar.Height;
            }
            this.vassalScrollArea.invalidate();
            this.vassalScrollBar.invalidate();
            this.backgroundImage.invalidate();
        }

        public void reinit()
        {
            this.init(false);
        }

        public void update()
        {
        }

        public void vassalInfoCallBack(VassalInfo_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.cachedVassalInfo = returnData.vassals;
                this.reAddVassals();
                GameEngine.Instance.World.updateUserVassals();
            }
        }

        private void wallScrollBarMoved()
        {
            int y = this.vassalScrollBar.Value;
            this.vassalScrollArea.Position = new Point(this.vassalScrollArea.X, 40 - y);
            this.vassalScrollArea.ClipRect = new Rectangle(this.vassalScrollArea.ClipRect.X, y, this.vassalScrollArea.ClipRect.Width, this.vassalScrollArea.ClipRect.Height);
            this.vassalScrollArea.invalidate();
            this.vassalScrollBar.invalidate();
        }

        public class ArmyLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel lblArchers = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblCatapults = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblPikemen = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblSwordsmen = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblYourVillage = new CustomSelfDrawPanel.CSDLabel();
            private AllVassalsPanel m_parent;
            private int m_position = -1000;
            private int m_villageID = -1;
            private int m_yourVillageID = -1;

            public void init(int position, AllVassalsPanel parent, int yourVillageID, int villageID, double honourPerSecond, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, string username)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.m_villageID = villageID;
                this.m_yourVillageID = yourVillageID;
                this.ClipVisible = true;
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.brown_lineitem_strip_02_light;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.brown_lineitem_strip_02_dark;
                }
                this.backgroundImage.Position = new Point(0, 0);
                base.addControl(this.backgroundImage);
                this.Size = this.backgroundImage.Size;
                this.lblYourVillage.Text = GameEngine.Instance.World.getVillageNameOrType(this.m_yourVillageID);
                this.lblYourVillage.Color = ARGBColors.Black;
                this.lblYourVillage.RolloverColor = ARGBColors.White;
                this.lblYourVillage.Position = new Point(9, 0);
                this.lblYourVillage.Size = new Size(290, this.backgroundImage.Height);
                this.lblYourVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblYourVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblYourVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblYourVillage_Click), "AllVassalsPanel_village");
                this.backgroundImage.addControl(this.lblYourVillage);
                this.lblVillage.Text = GameEngine.Instance.World.getVillageNameOrType(villageID);
                if (username.Length > 0)
                {
                    this.lblVillage.Text = this.lblVillage.Text + " (" + username + ")";
                }
                this.lblVillage.Color = ARGBColors.Black;
                this.lblVillage.RolloverColor = ARGBColors.White;
                this.lblVillage.Position = new Point(0x117, 0);
                this.lblVillage.Size = new Size(290, this.backgroundImage.Height);
                this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "AllVassalsPanel_other_village");
                this.backgroundImage.addControl(this.lblVillage);
                this.lblPeasants.Text = numPeasants.ToString();
                this.lblPeasants.Color = ARGBColors.Black;
                this.lblPeasants.RolloverColor = ARGBColors.White;
                this.lblPeasants.Position = new Point(0x249, 0);
                this.lblPeasants.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.lblPeasants.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick), "AllVassalsPanel_troops");
                this.backgroundImage.addControl(this.lblPeasants);
                this.lblArchers.Text = numArchers.ToString();
                this.lblArchers.Color = ARGBColors.Black;
                this.lblArchers.RolloverColor = ARGBColors.White;
                this.lblArchers.Position = new Point(0x285, 0);
                this.lblArchers.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblArchers.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblArchers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.lblArchers.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick), "AllVassalsPanel_troops");
                this.backgroundImage.addControl(this.lblArchers);
                this.lblPikemen.Text = numPikemen.ToString();
                this.lblPikemen.Color = ARGBColors.Black;
                this.lblPikemen.RolloverColor = ARGBColors.White;
                this.lblPikemen.Position = new Point(0x2c1, 0);
                this.lblPikemen.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblPikemen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblPikemen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.lblPikemen.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick), "AllVassalsPanel_troops");
                this.backgroundImage.addControl(this.lblPikemen);
                this.lblSwordsmen.Text = numSwordsmen.ToString();
                this.lblSwordsmen.Color = ARGBColors.Black;
                this.lblSwordsmen.RolloverColor = ARGBColors.White;
                this.lblSwordsmen.Position = new Point(0x2fd, 0);
                this.lblSwordsmen.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblSwordsmen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblSwordsmen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.lblSwordsmen.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick), "AllVassalsPanel_troops");
                this.backgroundImage.addControl(this.lblSwordsmen);
                this.lblCatapults.Text = numCatapults.ToString();
                this.lblCatapults.Color = ARGBColors.Black;
                this.lblCatapults.RolloverColor = ARGBColors.White;
                this.lblCatapults.Position = new Point(0x339, 0);
                this.lblCatapults.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblCatapults.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblCatapults.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.lblCatapults.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick), "AllVassalsPanel_troops");
                this.backgroundImage.addControl(this.lblCatapults);
                base.invalidate();
            }

            private void lblVillage_Click()
            {
                if (this.m_villageID >= 0)
                {
                    Point point = GameEngine.Instance.World.getVillageLocation(this.m_villageID);
                    InterfaceMgr.Instance.changeTab(9);
                    InterfaceMgr.Instance.changeTab(0);
                    InterfaceMgr.Instance.closeParishPanel();
                    GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                    InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_villageID, false, true, true, false);
                }
            }

            private void lblYourVillage_Click()
            {
                if (this.m_yourVillageID >= 0)
                {
                    InterfaceMgr.Instance.selectUserVillage(this.m_yourVillageID, false);
                    GameEngine.Instance.SkipVillageTab();
                    InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                    InterfaceMgr.Instance.getMainTabBar().changeTab(1);
                    InterfaceMgr.Instance.setVillageTabSubMode(8);
                }
            }

            private void troopClick()
            {
                if (this.m_villageID >= 0)
                {
                    InterfaceMgr.Instance.selectUserVillage(this.m_yourVillageID, false);
                    GameEngine.Instance.SkipVillageTab();
                    InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                    InterfaceMgr.Instance.getMainTabBar().changeTab(1);
                    InterfaceMgr.Instance.setVassalArmiesVillage(this.m_villageID);
                    InterfaceMgr.Instance.setVillageTabSubMode(15);
                }
            }

            public bool update()
            {
                return false;
            }
        }
    }
}

