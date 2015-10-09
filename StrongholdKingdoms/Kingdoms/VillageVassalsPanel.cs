namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class VillageVassalsPanel : CustomSelfDrawPanel, IDockableControl
    {
        private ArmyComparer armyComparer = new ArmyComparer();
        private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backgroundLeftEdge = new CustomSelfDrawPanel.CSDImage();
        private int blockYSize;
        private CustomSelfDrawPanel.CSDButton btnBreakVassalage = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnClose = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnRequestVassalage = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnSelectVassal = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnVassalsOverview = new CustomSelfDrawPanel.CSDButton();
        private VassalRequestInfo[] cachedRequestsOfYou;
        private VassalRequestInfo[] cachedRequestsYouveMade;
        private VassalInfo[] cachedVassalInfo;
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel currentLiegeLordInfoLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel currentLiegeLordLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        private Panel focusPanel;
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        public static VillageVassalsPanel instance;
        private CustomSelfDrawPanel.CSDLabel lblArchers = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblCatapults = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblHonourPerDay = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblPikemen = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblSwordsmen = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblVassalError = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDExtendingPanel liegeLordImageArea = new CustomSelfDrawPanel.CSDExtendingPanel();
        private VassalInfo liegeLordInfo = new VassalInfo();
        private List<ArmyLine> lineList = new List<ArmyLine>();
        public int m_selectedVillage = -1;
        private CustomSelfDrawPanel.CSDLabel maxVassalsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel noResearchText = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDExtendingPanel noResearchWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel outGoingFromLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage smallPeasantImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage smallPeasantImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel tbSelectVassalName = new CustomSelfDrawPanel.CSDLabel();
        public bool validVassalTarget;
        private CustomSelfDrawPanel.CSDArea vassalScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar vassalScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private int villageIDRef;
        private int vmVillageIDRef;
        private CustomSelfDrawPanel.CSDLabel yourVassalsLabel = new CustomSelfDrawPanel.CSDLabel();

        public VillageVassalsPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.focusPanel.Focus();
        }

        public void acceptRequest(int villageID)
        {
            this.villageIDRef = villageID;
            if (((this.liegeLordInfo == null) || (this.liegeLordInfo.villageID < 0)) || (MyMessageBox.Show(SK.Text("VassalControlPanel_AcceptLiegeLordWarning", "Accepting a new Liege Lord will break you from your current Liege Lord and any troops stationed will be lost."), SK.Text("VassalControlPanel_AcceptLiegeLord", "Accept New Liege Lord?"), MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                this.AcceptRequestTrue();
            }
        }

        private void AcceptRequestTrue()
        {
            RemoteServices.Instance.set_HandleVassalRequest_UserCallBack(new RemoteServices.HandleVassalRequest_UserCallBack(this.handleVassalRequestCallBack));
            RemoteServices.Instance.AcceptVassalRequest(this.villageIDRef, GameEngine.Instance.Village.VillageID);
        }

        private void allVassals()
        {
            InterfaceMgr.Instance.setVillageTabSubMode(0x18, false);
        }

        public void breakLiegeLordCallBack(BreakLiegeLord_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.liegeLordInfo = returnData.liegeLordInfo;
                this.cachedVassalInfo = returnData.vassals;
                this.reAddVassals();
                GameEngine.Instance.World.updateUserVassals();
            }
            CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
        }

        public void breakVassalage(int villageID)
        {
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                this.villageIDRef = villageID;
                this.vmVillageIDRef = village.VillageID;
                if (MyMessageBox.Show(SK.Text("VassalControlPanel_BreakVassalage_Warning", "Breaking from your vassal will mean any troops stationed there will be lost."), SK.Text("VassalControlPanel_BreakVassalage", "Break Vassalage?"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.BreakVassalageTrue();
                }
            }
        }

        private void BreakVassalage()
        {
            RemoteServices.Instance.set_BreakLiegeLord_UserCallBack(new RemoteServices.BreakLiegeLord_UserCallBack(this.breakLiegeLordCallBack));
            RemoteServices.Instance.BreakLiegeLord(this.villageIDRef, this.vmVillageIDRef);
            GameEngine.Instance.World.breakVassal(this.villageIDRef, this.vmVillageIDRef);
        }

        public void breakVassalageCallBack(BreakVassalage_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.liegeLordInfo = returnData.liegeLordInfo;
                this.cachedVassalInfo = returnData.vassals;
                this.reAddVassals();
                GameEngine.Instance.World.updateUserVassals();
            }
            CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
        }

        private void BreakVassalageTrue()
        {
            RemoteServices.Instance.set_BreakVassalage_UserCallBack(new RemoteServices.BreakVassalage_UserCallBack(this.breakVassalageCallBack));
            RemoteServices.Instance.BreakVassalage(this.vmVillageIDRef, this.villageIDRef);
            GameEngine.Instance.World.breakVassal(this.vmVillageIDRef, this.villageIDRef);
        }

        private void btnBreakVassalage_Click()
        {
            this.villageIDRef = this.liegeLordInfo.villageID;
            VillageMap village = GameEngine.Instance.Village;
            this.vmVillageIDRef = village.VillageID;
            if ((village != null) && (MyMessageBox.Show(SK.Text("VassalControlPanel_BreakFromLiegeLord_Warning", "Breaking from your Liege Lord will remove any stationed troops from your village."), SK.Text("VassalControlPanel_BreakFromLiegeLord", "Break from Liege Lord?"), MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                this.BreakVassalage();
            }
        }

        private void btnRequestVassalage_Click()
        {
            if (this.m_selectedVillage >= 0)
            {
                RemoteServices.Instance.set_SendVassalRequest_UserCallBack(new RemoteServices.SendVassalRequest_UserCallBack(this.sendVassalRequestCallBack));
                VillageMap village = GameEngine.Instance.Village;
                if (village != null)
                {
                    RemoteServices.Instance.SendVassalRequest(village.VillageID, this.m_selectedVillage);
                    this.btnRequestVassalage.Enabled = false;
                }
            }
        }

        private void btnSelectVassal_Click()
        {
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                GameEngine.Instance.World.zoomToVillage(village.VillageID);
            }
            InterfaceMgr.Instance.getMainTabBar().selectDummyTabFast(13);
        }

        public void cancelRequest(int villageID)
        {
            RemoteServices.Instance.set_HandleVassalRequest_UserCallBack(new RemoteServices.HandleVassalRequest_UserCallBack(this.handleVassalRequestCallBack));
            RemoteServices.Instance.CancelVassalRequest(GameEngine.Instance.Village.VillageID, villageID);
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.setVillageTabSubMode(-1);
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

        public void declineRequest(int villageID)
        {
            RemoteServices.Instance.set_HandleVassalRequest_UserCallBack(new RemoteServices.HandleVassalRequest_UserCallBack(this.handleVassalRequestCallBack));
            RemoteServices.Instance.DeclineVassalRequest(villageID, GameEngine.Instance.Village.VillageID);
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

        public void getPreVassalInfoCallBack(GetPreVassalInfo_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if ((returnData.alreadyHasLiegeLord || returnData.rankTooHigh) || returnData.invalidTarget)
                {
                    this.validVassalTarget = false;
                    this.btnRequestVassalage.Enabled = false;
                    if (returnData.alreadyHasLiegeLord)
                    {
                        this.lblVassalError.Text = SK.Text("VassalControlPanel_Village_Has_Liege_Lord", "Village already has a liege lord");
                    }
                    else if (returnData.rankTooHigh)
                    {
                        this.lblVassalError.Text = SK.Text("VassalControlPanel_Rank_Too_High", "The Player's Rank is too high");
                    }
                    else if (returnData.invalidTarget)
                    {
                        this.lblVassalError.Text = SK.Text("VassalControlPanel_Invalid_Village", "Not a valid village for vassaling.");
                    }
                    this.lblVassalError.Visible = true;
                }
                else
                {
                    this.validVassalTarget = true;
                    int num = GameEngine.Instance.World.numVassalsAllowed();
                    int num2 = GameEngine.Instance.World.countVassals();
                    if (num > num2)
                    {
                        this.btnRequestVassalage.Enabled = true;
                    }
                    this.lblVassalError.Visible = false;
                }
            }
        }

        public void handleVassalRequestCallBack(HandleVassalRequest_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.liegeLordInfo = returnData.liegeLordInfo;
                this.cachedVassalInfo = returnData.vassals;
                this.importVassalRequests(returnData.requestsYouveMade, returnData.requestsOfYou);
                this.reAddVassals();
                GameEngine.Instance.World.updateUserVassals();
                this.lblVassalError.Visible = false;
                this.btnRequestVassalage.Enabled = false;
                this.tbSelectVassalName.Text = "";
            }
        }

        public void importVassalRequests(VassalRequestInfo[] requestsYouveSent, VassalRequestInfo[] requestsOfYou)
        {
            this.cachedRequestsYouveMade = requestsYouveSent;
            this.cachedRequestsOfYou = requestsOfYou;
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
            int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            this.parishNameLabel.Text = SK.Text("GENERIC_Vassals", "Vassals") + " : " + GameEngine.Instance.World.getVillageNameOrType(villageID);
            this.parishNameLabel.Color = ARGBColors.White;
            this.parishNameLabel.DropShadowColor = ARGBColors.Black;
            this.parishNameLabel.Position = new Point(20, 0);
            this.parishNameLabel.Size = new Size(base.Width - 40, 40);
            this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerImage.addControl(this.parishNameLabel);
            this.btnVassalsOverview.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
            this.btnVassalsOverview.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
            this.btnVassalsOverview.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
            this.btnVassalsOverview.Position = new Point(base.Width - 230, 7);
            this.btnVassalsOverview.Text.Text = SK.Text("Vassals_Overview", "Vassals Overview");
            this.btnVassalsOverview.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.btnVassalsOverview.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.btnVassalsOverview.TextYOffset = -3;
            this.btnVassalsOverview.Text.Color = ARGBColors.Black;
            this.btnVassalsOverview.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.allVassals), "VillageVassalsPanel_all_vassals");
            this.headerImage.addControl(this.btnVassalsOverview);
            CustomSelfDrawPanel.WikiLinkControl.init(this.headerImage, 8, new Point((base.Width - 230) - 50, 3));
            this.liegeLordImageArea.Size = new Size(base.Width - 50, 0x55);
            this.liegeLordImageArea.Position = new Point(0x19, 20);
            this.backgroundImage.addControl(this.liegeLordImageArea);
            this.liegeLordImageArea.Create((Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_right);
            this.currentLiegeLordLabel.Text = SK.Text("VassalControlPanel_Current_Liege_Lord", "Current Liege Lord");
            this.currentLiegeLordLabel.Color = ARGBColors.Black;
            this.currentLiegeLordLabel.Position = new Point(5, 5);
            this.currentLiegeLordLabel.Size = new Size(500, 0x19);
            this.currentLiegeLordLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.currentLiegeLordLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.liegeLordImageArea.addControl(this.currentLiegeLordLabel);
            Graphics graphics = base.CreateGraphics();
            Size size = graphics.MeasureString(this.currentLiegeLordLabel.Text, this.currentLiegeLordLabel.Font, 500).ToSize();
            graphics.Dispose();
            this.currentLiegeLordInfoLabel.Text = "";
            this.currentLiegeLordInfoLabel.Color = ARGBColors.Black;
            this.currentLiegeLordInfoLabel.Position = new Point((10 + size.Width) + 5, 7);
            this.currentLiegeLordInfoLabel.Size = new Size(500, 50);
            this.currentLiegeLordInfoLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.currentLiegeLordInfoLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.currentLiegeLordInfoLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.liegeLordClicked));
            this.liegeLordImageArea.addControl(this.currentLiegeLordInfoLabel);
            this.lblHonourPerDay.Text = SK.Text("VassalControlPanel_Honour_Gained_Per_Day", "Honour Gained Per Day") + " : ";
            this.lblHonourPerDay.Color = ARGBColors.Black;
            this.lblHonourPerDay.Position = new Point(0xf3, 0x38);
            this.lblHonourPerDay.Size = new Size(500, 0x19);
            this.lblHonourPerDay.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.lblHonourPerDay.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.liegeLordImageArea.addControl(this.lblHonourPerDay);
            this.smallPeasantImage2.Image = (Image) GFXLibrary.armies_screen_troops;
            this.smallPeasantImage2.Position = new Point(0x282, 5);
            this.smallPeasantImage2.ClipRect = new Rectangle(0, 0, (this.smallPeasantImage2.Image.Width * 5) / 6, this.smallPeasantImage2.Image.Height);
            this.liegeLordImageArea.addControl(this.smallPeasantImage2);
            this.lblPeasants.Text = "0";
            this.lblPeasants.Color = ARGBColors.Black;
            this.lblPeasants.Position = new Point(0x270, 0x37);
            this.lblPeasants.Size = new Size(0x37, 0x19);
            this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
            this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.liegeLordImageArea.addControl(this.lblPeasants);
            this.lblArchers.Text = "0";
            this.lblArchers.Color = ARGBColors.Black;
            this.lblArchers.Position = new Point(0x2ac, 0x37);
            this.lblArchers.Size = new Size(0x37, 0x19);
            this.lblArchers.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
            this.lblArchers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.liegeLordImageArea.addControl(this.lblArchers);
            this.lblPikemen.Text = "0";
            this.lblPikemen.Color = ARGBColors.Black;
            this.lblPikemen.Position = new Point(0x2e8, 0x37);
            this.lblPikemen.Size = new Size(0x37, 0x19);
            this.lblPikemen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
            this.lblPikemen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.liegeLordImageArea.addControl(this.lblPikemen);
            this.lblSwordsmen.Text = "0";
            this.lblSwordsmen.Color = ARGBColors.Black;
            this.lblSwordsmen.Position = new Point(0x324, 0x37);
            this.lblSwordsmen.Size = new Size(0x37, 0x19);
            this.lblSwordsmen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
            this.lblSwordsmen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.liegeLordImageArea.addControl(this.lblSwordsmen);
            this.lblCatapults.Text = "0";
            this.lblCatapults.Color = ARGBColors.Black;
            this.lblCatapults.Position = new Point(0x360, 0x37);
            this.lblCatapults.Size = new Size(0x37, 0x19);
            this.lblCatapults.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
            this.lblCatapults.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.liegeLordImageArea.addControl(this.lblCatapults);
            this.btnBreakVassalage.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
            this.btnBreakVassalage.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
            this.btnBreakVassalage.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
            this.btnBreakVassalage.Position = new Point(0x25, 0x48);
            this.btnBreakVassalage.Text.Text = SK.Text("VassalControlPanel_Break_From_Liege_Lord", "Break From Liege Lord");
            this.btnBreakVassalage.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.btnBreakVassalage.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.btnBreakVassalage.TextYOffset = -3;
            this.btnBreakVassalage.Text.Color = ARGBColors.Black;
            this.btnBreakVassalage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnBreakVassalage_Click), "VillageVassalsPanel_break_vassal");
            this.backgroundImage.addControl(this.btnBreakVassalage);
            this.blockYSize = ((height - 40) - 0x38) - 0x7c;
            this.headerLabelsImage.Size = new Size((base.Width - 0x19) - 0x17, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 0x81);
            this.backgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.brown_mail2_field_bar_mail_left, (Image) GFXLibrary.brown_mail2_field_bar_mail_middle, (Image) GFXLibrary.brown_mail2_field_bar_mail_right);
            this.divider2Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider2Image.Position = new Point(300, 0);
            this.headerLabelsImage.addControl(this.divider2Image);
            this.yourVassalsLabel.Text = SK.Text("VassalControlPanel_Your_Vassals", "Your Vassals") + " (" + GameEngine.Instance.World.countVassals().ToString() + ")";
            this.yourVassalsLabel.Color = ARGBColors.Black;
            this.yourVassalsLabel.Position = new Point(12, -3);
            this.yourVassalsLabel.Size = new Size(0xdf, this.headerLabelsImage.Height);
            this.yourVassalsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.yourVassalsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.yourVassalsLabel);
            this.maxVassalsLabel.Text = SK.Text("VassalControlPanel_Max_Vassals", "Maximum Vassals Allowed") + " : " + GameEngine.Instance.World.numVassalsAllowed().ToString();
            this.maxVassalsLabel.Color = ARGBColors.Black;
            this.maxVassalsLabel.Position = new Point(this.headerLabelsImage.Width - 0x14d, -3);
            this.maxVassalsLabel.Size = new Size(0x13f, this.headerLabelsImage.Height);
            this.maxVassalsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.maxVassalsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.headerLabelsImage.addControl(this.maxVassalsLabel);
            this.vassalScrollArea.Position = new Point(0x19, 0xa4);
            this.vassalScrollArea.Size = new Size(0x393, (this.blockYSize - 40) - 10);
            this.vassalScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, (this.blockYSize - 40) - 10));
            this.backgroundImage.addControl(this.vassalScrollArea);
            this.vassalScrollArea.Visible = true;
            int num3 = this.vassalScrollBar.Value;
            this.vassalScrollBar.Position = new Point(0x3af, 0xa4);
            this.vassalScrollBar.Size = new Size(0x18, (this.blockYSize - 40) - 10);
            this.backgroundImage.addControl(this.vassalScrollBar);
            this.vassalScrollBar.Value = 0;
            this.vassalScrollBar.Max = 100;
            this.vassalScrollBar.NumVisibleLines = 0x19;
            this.vassalScrollBar.Create(null, null, null, (Image) GFXLibrary.brown_24wide_thumb_top, (Image) GFXLibrary.brown_24wide_thumb_middle, (Image) GFXLibrary.brown_24wide_thumb_bottom);
            this.vassalScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            this.smallPeasantImage.Image = (Image) GFXLibrary.armies_screen_troops;
            this.smallPeasantImage.Position = new Point(0x143, -10);
            this.smallPeasantImage.ClipRect = new Rectangle(0, 0, (this.smallPeasantImage.Image.Width * 5) / 6, this.smallPeasantImage.Image.Height);
            this.headerLabelsImage.addControl(this.smallPeasantImage);
            if (resized)
            {
                this.vassalScrollBar.Value = num3;
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
            this.btnClose.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "VillageVassalsPanel_close");
            this.backgroundImage.addControl(this.btnClose);
            this.btnSelectVassal.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
            this.btnSelectVassal.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
            this.btnSelectVassal.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
            this.btnSelectVassal.Position = new Point(20, ((height - 40) - 40) - 4);
            this.btnSelectVassal.Text.Text = SK.Text("VassalControlPanel_Select_Vassal", "Select Vassal");
            this.btnSelectVassal.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.btnSelectVassal.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.btnSelectVassal.TextYOffset = -3;
            this.btnSelectVassal.Text.Color = ARGBColors.Black;
            this.btnSelectVassal.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnSelectVassal_Click), "VillageVassalsPanel_select");
            this.backgroundImage.addControl(this.btnSelectVassal);
            this.tbSelectVassalName.Text = "Selected Vassal";
            this.tbSelectVassalName.Color = ARGBColors.White;
            this.tbSelectVassalName.DropShadowColor = ARGBColors.Black;
            this.tbSelectVassalName.Position = new Point(240, (((height - 40) - 40) - 4) + 6);
            this.tbSelectVassalName.Size = new Size(200, 0x19);
            this.tbSelectVassalName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.tbSelectVassalName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.backgroundImage.addControl(this.tbSelectVassalName);
            this.lblVassalError.Text = "";
            this.lblVassalError.Color = ARGBColors.Black;
            this.lblVassalError.Position = new Point(20, ((((height - 40) - 40) - 4) + 6) - 0x18);
            this.lblVassalError.Size = new Size(0x27a, 0x19);
            this.lblVassalError.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.lblVassalError.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.backgroundImage.addControl(this.lblVassalError);
            this.btnRequestVassalage.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
            this.btnRequestVassalage.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
            this.btnRequestVassalage.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
            this.btnRequestVassalage.Position = new Point(450, ((height - 40) - 40) - 4);
            this.btnRequestVassalage.Text.Text = SK.Text("VassalControlPanel_RequestVassalage", "Request Vassalage");
            this.btnRequestVassalage.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.btnRequestVassalage.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.btnRequestVassalage.TextYOffset = -3;
            this.btnRequestVassalage.Text.Color = ARGBColors.Black;
            this.btnRequestVassalage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnRequestVassalage_Click), "VillageVassalsPanel_request");
            this.btnRequestVassalage.Enabled = false;
            this.backgroundImage.addControl(this.btnRequestVassalage);
            this.liegeLordInfo.villageID = -1;
            this.cachedVassalInfo = null;
            this.cachedRequestsYouveMade = null;
            this.cachedRequestsOfYou = null;
            this.lblVassalError.Visible = false;
            this.btnRequestVassalage.Enabled = false;
            this.btnSelectVassal.Visible = false;
            this.tbSelectVassalName.Visible = false;
            this.btnRequestVassalage.Visible = false;
            this.tbSelectVassalName.Text = "";
            this.noResearchWindow.Size = new Size(0x2e3, 150);
            this.noResearchWindow.Position = new Point(0x7e, 230);
            this.backgroundImage.addControl(this.noResearchWindow);
            this.noResearchWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
            this.noResearchWindow.Visible = false;
            this.noResearchText.Text = SK.Text("Vassal_Need_Rank", "You don't currently have the required Rank (8) to make another player your Vassal.");
            this.noResearchText.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
            this.noResearchText.Position = new Point(20, 0);
            this.noResearchText.Size = new Size(this.noResearchWindow.Width - 40, this.noResearchWindow.Height);
            this.noResearchText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.noResearchText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.noResearchWindow.addControl(this.noResearchText);
            RemoteServices.Instance.set_VassalInfo_UserCallBack(new RemoteServices.VassalInfo_UserCallBack(this.vassalInfoCallBack));
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                RemoteServices.Instance.VassalInfo(village.VillageID);
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
            base.Name = "VillageVassalsPanel";
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

        private void liegeLordClicked()
        {
            if ((this.liegeLordInfo != null) && (this.liegeLordInfo.villageID >= 0))
            {
                int villageID = this.liegeLordInfo.villageID;
                Point point = GameEngine.Instance.World.getVillageLocation(villageID);
                InterfaceMgr.Instance.changeTab(9);
                InterfaceMgr.Instance.changeTab(0);
                InterfaceMgr.Instance.closeParishPanel();
                GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                InterfaceMgr.Instance.displaySelectedVillagePanel(villageID, false, true, true, false);
            }
        }

        public void logout()
        {
        }

        private void reAddVassals()
        {
            this.lineList.Clear();
            if ((this.liegeLordInfo == null) || (this.liegeLordInfo.villageID < 0))
            {
                this.currentLiegeLordInfoLabel.Text = SK.Text("VassalControlPanel_You_Have_No_Liege_Lord", "You currently have no Liege Lord. Accept offers from other players to become their Vassal to receive a daily Honour boost.");
                this.btnBreakVassalage.Visible = false;
                this.lblHonourPerDay.Visible = false;
                this.smallPeasantImage2.Visible = false;
                this.lblPeasants.Visible = false;
                this.lblArchers.Visible = false;
                this.lblPikemen.Visible = false;
                this.lblSwordsmen.Visible = false;
                this.lblCatapults.Visible = false;
            }
            else
            {
                this.btnBreakVassalage.Visible = true;
                this.smallPeasantImage2.Visible = true;
                this.lblPeasants.Visible = true;
                this.lblArchers.Visible = true;
                this.lblPikemen.Visible = true;
                this.lblSwordsmen.Visible = true;
                this.lblCatapults.Visible = true;
                NumberFormatInfo nFI = GameEngine.NFI;
                this.lblPeasants.Text = this.liegeLordInfo.stationed_Peasants.ToString("N", nFI);
                this.lblArchers.Text = this.liegeLordInfo.stationed_Archers.ToString("N", nFI);
                this.lblPikemen.Text = this.liegeLordInfo.stationed_Pikemen.ToString("N", nFI);
                this.lblSwordsmen.Text = this.liegeLordInfo.stationed_Swordsmen.ToString("N", nFI);
                this.lblCatapults.Text = this.liegeLordInfo.stationed_Catapults.ToString("N", nFI);
                this.currentLiegeLordInfoLabel.Text = GameEngine.Instance.World.getVillageName(this.liegeLordInfo.villageID) + " (" + this.liegeLordInfo.liegelordname + " - " + Rankings.getRankingName(GameEngine.Instance.LocalWorldData, this.liegeLordInfo.rank, this.liegeLordInfo.subrank, this.liegeLordInfo.male) + ")";
                this.lblHonourPerDay.Visible = true;
                this.lblHonourPerDay.Text = SK.Text("VassalControlPanel_Honour_Gained_Per_Day", "Honour Gained Per Day") + " : " + ((int) (this.liegeLordInfo.honourPerSecond * 86400.0)).ToString("N", nFI);
            }
            if (GameEngine.Instance.World.getRank() < 7)
            {
                this.noResearchWindow.Visible = true;
            }
            this.vassalScrollArea.clearControls();
            int y = 0;
            int position = 0;
            if (this.cachedRequestsOfYou != null)
            {
                foreach (VassalRequestInfo info2 in this.cachedRequestsOfYou)
                {
                    if (y != 0)
                    {
                        y += 5;
                    }
                    ArmyLine control = new ArmyLine {
                        Position = new Point(0, y)
                    };
                    control.initAsked(position, this, info2.requesterVillageID, info2.requesterUserName, info2.requestMadeTime);
                    this.vassalScrollArea.addControl(control);
                    y += control.Height;
                    this.lineList.Add(control);
                    position++;
                }
            }
            if (this.cachedRequestsYouveMade != null)
            {
                foreach (VassalRequestInfo info3 in this.cachedRequestsYouveMade)
                {
                    if (y != 0)
                    {
                        y += 5;
                    }
                    ArmyLine line2 = new ArmyLine {
                        Position = new Point(0, y)
                    };
                    line2.initAsking(position, this, info3.vassalVillageID, info3.vassalUserName, info3.requestMadeTime);
                    this.vassalScrollArea.addControl(line2);
                    y += line2.Height;
                    this.lineList.Add(line2);
                    position++;
                }
            }
            if (this.cachedVassalInfo != null)
            {
                foreach (VassalInfo info4 in this.cachedVassalInfo)
                {
                    if (y != 0)
                    {
                        y += 5;
                    }
                    ArmyLine line3 = new ArmyLine {
                        Position = new Point(0, y)
                    };
                    line3.init(position, this, info4.villageID, info4.honourPerSecond, info4.stationed_Peasants, info4.stationed_Archers, info4.stationed_Pikemen, info4.stationed_Swordsmen, info4.stationed_Catapults, info4.vassalPlayerName);
                    this.vassalScrollArea.addControl(line3);
                    y += line3.Height;
                    this.lineList.Add(line3);
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
            bool validVassalTarget = this.validVassalTarget;
            int selectedVillage = this.m_selectedVillage;
            this.init(false);
            this.validVassalTarget = validVassalTarget;
            this.m_selectedVillage = selectedVillage;
            if (this.validVassalTarget)
            {
                int num2 = GameEngine.Instance.World.numVassalsAllowed();
                int num3 = GameEngine.Instance.World.countVassals();
                if (num2 > num3)
                {
                    this.btnRequestVassalage.Enabled = true;
                }
            }
            if (this.m_selectedVillage >= 0)
            {
                this.tbSelectVassalName.Text = GameEngine.Instance.World.getVillageName(this.m_selectedVillage);
            }
        }

        public void sendVassalRequestCallBack(SendVassalRequest_ReturnType returnData)
        {
            if (returnData.Success && (GameEngine.Instance.Village != null))
            {
                this.importVassalRequests(returnData.requestsYouveMade, returnData.requestsOfYou);
                this.reAddVassals();
            }
        }

        public void setVassalVillage(int villageID)
        {
            this.validVassalTarget = false;
            this.m_selectedVillage = villageID;
            this.tbSelectVassalName.Text = GameEngine.Instance.World.getVillageName(this.m_selectedVillage);
            if (villageID >= 0)
            {
                RemoteServices.Instance.set_GetPreVassalInfo_UserCallBack(new RemoteServices.GetPreVassalInfo_UserCallBack(this.getPreVassalInfoCallBack));
                RemoteServices.Instance.GetPreVassalInfo(InterfaceMgr.Instance.OwnSelectedVillage, villageID);
            }
            this.btnRequestVassalage.Enabled = false;
            this.lblVassalError.Visible = false;
        }

        private void swipeLeft()
        {
            InterfaceMgr.Instance.getVillageTabBar().changeTabLeft();
        }

        private void swiperight()
        {
            InterfaceMgr.Instance.getVillageTabBar().changeTabRight();
        }

        public void update()
        {
        }

        public void vassalInfoCallBack(VassalInfo_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.liegeLordInfo = returnData.liegeLordInfo;
                this.cachedVassalInfo = returnData.vassals;
                this.importVassalRequests(returnData.requestsYouveMade, returnData.requestsOfYou);
                this.reAddVassals();
                GameEngine.Instance.World.updateUserVassals();
                int num = GameEngine.Instance.World.numVassalsAllowed();
                int num2 = GameEngine.Instance.World.countVassals();
                if (num > num2)
                {
                    this.btnSelectVassal.Visible = true;
                    this.tbSelectVassalName.Visible = true;
                    this.btnRequestVassalage.Visible = true;
                    if (this.validVassalTarget)
                    {
                        this.btnRequestVassalage.Enabled = true;
                    }
                    else
                    {
                        this.btnRequestVassalage.Enabled = false;
                    }
                }
                else
                {
                    this.btnSelectVassal.Visible = false;
                    this.tbSelectVassalName.Visible = false;
                    this.btnRequestVassalage.Visible = false;
                }
            }
        }

        private void wallScrollBarMoved()
        {
            int y = this.vassalScrollBar.Value;
            this.vassalScrollArea.Position = new Point(this.vassalScrollArea.X, 0xa4 - y);
            this.vassalScrollArea.ClipRect = new Rectangle(this.vassalScrollArea.ClipRect.X, y, this.vassalScrollArea.ClipRect.Width, this.vassalScrollArea.ClipRect.Height);
            this.vassalScrollArea.invalidate();
            this.vassalScrollBar.invalidate();
        }

        public class ArmyComparer : IComparer<WorldMap.LocalArmyData>
        {
            public int Compare(WorldMap.LocalArmyData x, WorldMap.LocalArmyData y)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                if (x.armyID > y.armyID)
                {
                    return 1;
                }
                if (x.armyID < y.armyID)
                {
                    return -1;
                }
                return 0;
            }
        }

        public class ArmyLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDButton btnAccept = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton btnBreakVassalage = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton btnCancel = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton btnReject = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDLabel lblArchers = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblArrivalTime = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblCatapults = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblPikemen = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblSwordsmen = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
            private DateTime m_arrivalTime = DateTime.Now;
            private VillageVassalsPanel m_parent;
            private int m_position = -1000;
            private int m_villageID = -1;

            private void acceptVassalageRequest()
            {
                if (this.m_parent != null)
                {
                    this.m_parent.acceptRequest(this.m_villageID);
                }
            }

            private void breakVassalage()
            {
                if (this.m_parent != null)
                {
                    this.m_parent.breakVassalage(this.m_villageID);
                }
            }

            private void cancelVassalageRequest()
            {
                if (this.m_parent != null)
                {
                    this.m_parent.cancelRequest(this.m_villageID);
                }
            }

            private void declineVassalageRequest()
            {
                if (this.m_parent != null)
                {
                    this.m_parent.declineRequest(this.m_villageID);
                }
            }

            public void init(int position, VillageVassalsPanel parent, int villageID, double honourPerSecond, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, string username)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.m_villageID = villageID;
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
                this.lblVillage.Text = GameEngine.Instance.World.getVillageNameOrType(villageID);
                if (username.Length > 0)
                {
                    this.lblVillage.Text = this.lblVillage.Text + " (" + username + ")";
                }
                this.lblVillage.Color = ARGBColors.Black;
                this.lblVillage.RolloverColor = ARGBColors.White;
                this.lblVillage.Position = new Point(9, 0);
                this.lblVillage.Size = new Size(290, this.backgroundImage.Height);
                this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "VillageVassalsPanel_village");
                this.backgroundImage.addControl(this.lblVillage);
                this.lblPeasants.Text = numPeasants.ToString();
                this.lblPeasants.Color = ARGBColors.Black;
                this.lblPeasants.RolloverColor = ARGBColors.White;
                this.lblPeasants.Position = new Point(0x131, 0);
                this.lblPeasants.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.lblPeasants.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick));
                this.lblPeasants.CustomTooltipID = 0xaf0;
                this.backgroundImage.addControl(this.lblPeasants);
                this.lblArchers.Text = numArchers.ToString();
                this.lblArchers.Color = ARGBColors.Black;
                this.lblArchers.RolloverColor = ARGBColors.White;
                this.lblArchers.Position = new Point(0x16d, 0);
                this.lblArchers.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblArchers.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblArchers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.lblArchers.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick));
                this.lblArchers.CustomTooltipID = 0xaf0;
                this.backgroundImage.addControl(this.lblArchers);
                this.lblPikemen.Text = numPikemen.ToString();
                this.lblPikemen.Color = ARGBColors.Black;
                this.lblPikemen.RolloverColor = ARGBColors.White;
                this.lblPikemen.Position = new Point(0x1a9, 0);
                this.lblPikemen.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblPikemen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblPikemen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.lblPikemen.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick));
                this.lblPikemen.CustomTooltipID = 0xaf0;
                this.backgroundImage.addControl(this.lblPikemen);
                this.lblSwordsmen.Text = numSwordsmen.ToString();
                this.lblSwordsmen.Color = ARGBColors.Black;
                this.lblSwordsmen.RolloverColor = ARGBColors.White;
                this.lblSwordsmen.Position = new Point(0x1e5, 0);
                this.lblSwordsmen.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblSwordsmen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblSwordsmen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.lblSwordsmen.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick));
                this.lblSwordsmen.CustomTooltipID = 0xaf0;
                this.backgroundImage.addControl(this.lblSwordsmen);
                this.lblCatapults.Text = numCatapults.ToString();
                this.lblCatapults.Color = ARGBColors.Black;
                this.lblCatapults.RolloverColor = ARGBColors.White;
                this.lblCatapults.Position = new Point(0x221, 0);
                this.lblCatapults.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblCatapults.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblCatapults.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.lblCatapults.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopClick));
                this.lblCatapults.CustomTooltipID = 0xaf0;
                this.backgroundImage.addControl(this.lblCatapults);
                this.btnBreakVassalage.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
                this.btnBreakVassalage.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
                this.btnBreakVassalage.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
                this.btnBreakVassalage.Position = new Point(0x2c2, 3);
                this.btnBreakVassalage.Text.Text = SK.Text("VassalControlSentLine_Break_Vassalage", "Break Vassalage");
                this.btnBreakVassalage.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.btnBreakVassalage.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.btnBreakVassalage.TextYOffset = -3;
                this.btnBreakVassalage.Text.Color = ARGBColors.Black;
                this.btnBreakVassalage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.breakVassalage), "VillageVassalsPanel_break_vassal_line");
                this.backgroundImage.addControl(this.btnBreakVassalage);
                base.invalidate();
            }

            public void initAsked(int position, VillageVassalsPanel parent, int villageID, string userName, DateTime requestTime)
            {
                this.m_villageID = villageID;
                this.m_parent = parent;
                this.m_position = position;
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
                this.lblVillage.Text = GameEngine.Instance.World.getVillageName(villageID) + " (" + userName + ")";
                this.lblVillage.Color = ARGBColors.Black;
                this.lblVillage.RolloverColor = ARGBColors.White;
                this.lblVillage.Position = new Point(9, 0);
                this.lblVillage.Size = new Size(290, this.backgroundImage.Height);
                this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "VillageVassalsPanel_village");
                this.backgroundImage.addControl(this.lblVillage);
                this.lblPeasants.Text = SK.Text("VassalControlRequestLine_Request_Made", "Request Made") + " :" + requestTime.ToShortTimeString() + " : " + requestTime.ToShortDateString();
                this.lblPeasants.Color = ARGBColors.Black;
                this.lblPeasants.Position = new Point(0x131, 0);
                this.lblPeasants.Size = new Size(430, this.backgroundImage.Height);
                this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.lblPeasants);
                this.btnAccept.ImageNorm = (Image) GFXLibrary.brown_mail2_button_blue_141wide_normal;
                this.btnAccept.ImageOver = (Image) GFXLibrary.brown_mail2_button_blue_141wide_over;
                this.btnAccept.ImageClick = (Image) GFXLibrary.brown_mail2_button_blue_141wide_pushed;
                this.btnAccept.Position = new Point(0x272, 3);
                this.btnAccept.Text.Text = SK.Text("GENERIC_Accept", "Accept");
                this.btnAccept.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.btnAccept.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.btnAccept.TextYOffset = -3;
                this.btnAccept.Text.Color = ARGBColors.Black;
                this.btnAccept.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.acceptVassalageRequest), "VillageVassalsPanel_accept");
                this.backgroundImage.addControl(this.btnAccept);
                this.btnReject.ImageNorm = (Image) GFXLibrary.brown_mail2_button_blue_141wide_normal;
                this.btnReject.ImageOver = (Image) GFXLibrary.brown_mail2_button_blue_141wide_over;
                this.btnReject.ImageClick = (Image) GFXLibrary.brown_mail2_button_blue_141wide_pushed;
                this.btnReject.Position = new Point(0x308, 3);
                this.btnReject.Text.Text = SK.Text("GENERIC_Decline", "Decline");
                this.btnReject.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.btnReject.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.btnReject.TextYOffset = -3;
                this.btnReject.Text.Color = ARGBColors.Black;
                this.btnReject.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.declineVassalageRequest), "VillageVassalsPanel_reject");
                this.backgroundImage.addControl(this.btnReject);
            }

            public void initAsking(int position, VillageVassalsPanel parent, int villageID, string userName, DateTime requestTime)
            {
                this.m_villageID = villageID;
                this.m_parent = parent;
                this.m_position = position;
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
                this.lblVillage.Text = GameEngine.Instance.World.getVillageName(villageID) + " (" + userName + ")";
                this.lblVillage.Color = ARGBColors.Black;
                this.lblVillage.RolloverColor = ARGBColors.White;
                this.lblVillage.Position = new Point(9, 0);
                this.lblVillage.Size = new Size(290, this.backgroundImage.Height);
                this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "VillageVassalsPanel_village");
                this.backgroundImage.addControl(this.lblVillage);
                this.lblPeasants.Text = SK.Text("VassalControlRequestLine_Request_Made", "Request Made") + " :" + requestTime.ToShortTimeString() + " : " + requestTime.ToShortDateString();
                this.lblPeasants.Color = ARGBColors.Black;
                this.lblPeasants.Position = new Point(0x131, 0);
                this.lblPeasants.Size = new Size(430, this.backgroundImage.Height);
                this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.lblPeasants);
                this.btnCancel.ImageNorm = (Image) GFXLibrary.brown_mail2_button_blue_141wide_normal;
                this.btnCancel.ImageOver = (Image) GFXLibrary.brown_mail2_button_blue_141wide_over;
                this.btnCancel.ImageClick = (Image) GFXLibrary.brown_mail2_button_blue_141wide_pushed;
                this.btnCancel.Position = new Point(0x308, 3);
                this.btnCancel.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
                this.btnCancel.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.btnCancel.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.btnCancel.TextYOffset = -3;
                this.btnCancel.Text.Color = ARGBColors.Black;
                this.btnCancel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelVassalageRequest), "VillageVassalsPanel_cancel");
                this.backgroundImage.addControl(this.btnCancel);
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

            private void troopClick()
            {
                if (this.m_villageID >= 0)
                {
                    GameEngine.Instance.playInterfaceSound("VillageVassalsPanel_troops");
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

