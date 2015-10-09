namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DisbandTroopsPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDButton btnCancel = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnDisband = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnEdit = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDImage imgBackground = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel lblCurValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblMax = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblMin = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblTroopType = new CustomSelfDrawPanel.CSDLabel();
        private bool m_isTroops;
        private MyFormBase m_parent;
        private int m_troopType = -1;
        private CustomSelfDrawPanel.CSDTrackBar tbTroopsDisband = new CustomSelfDrawPanel.CSDTrackBar();

        public DisbandTroopsPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void disbandClick()
        {
            int amount = this.tbTroopsDisband.Value;
            if (amount > 0)
            {
                VillageMap village = GameEngine.Instance.Village;
                if (village != null)
                {
                    GameEngine.Instance.playInterfaceSound("DisbandTroopsPopup_disband");
                    if (this.m_isTroops)
                    {
                        village.disbandTroops(this.m_troopType, amount);
                    }
                    else
                    {
                        village.disbandPeople(this.m_troopType, amount);
                    }
                    this.m_parent.Close();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void editValue()
        {
            InterfaceMgr.Instance.setFloatingValueSentDelegate(new InterfaceMgr.FloatingValueSent(this.setTrackCB));
            Point point = this.m_parent.PointToScreen(new Point(this.btnEdit.Rectangle.Right, this.btnEdit.Y + 0x22));
            FloatingInput.openDisband(point.X, point.Y, this.tbTroopsDisband.Value, this.tbTroopsDisband.Max, this.m_parent);
        }

        public void init(MyFormBase parent, int troopType, bool isTroops)
        {
            this.init(parent, troopType, isTroops, null);
        }

        public void init(MyFormBase parent, int troopType, bool isTroops, object back)
        {
            base.clearControls();
            this.imgBackground.Image = (Image) back;
            this.m_isTroops = isTroops;
            this.m_parent = parent;
            base.Size = this.m_parent.Size;
            this.BackColor = ARGBColors.Transparent;
            this.imgBackground.Size = base.Size;
            this.imgBackground.Position = new Point(0, 0);
            this.imgBackground.Visible = true;
            base.addControl(this.imgBackground);
            VillageMap village = GameEngine.Instance.Village;
            this.m_troopType = troopType;
            int numPeasants = 0;
            this.lblTroopType.Text = "";
            this.lblTroopType.Color = ARGBColors.White;
            this.lblTroopType.DropShadowColor = ARGBColors.Black;
            this.lblTroopType.Position = new Point(0, 10);
            this.lblTroopType.Size = new Size(base.Width, 0x18);
            this.lblTroopType.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblTroopType.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.lblMax.Text = "";
            if (village != null)
            {
                switch (troopType)
                {
                    case 1:
                        this.lblTroopType.Text = SK.Text("GENERIC_Monks", "Monks");
                        numPeasants = village.calcTotalMonksAtHome();
                        break;

                    case 2:
                        this.lblTroopType.Text = SK.Text("GENERIC_Merchants", "Merchants");
                        numPeasants = village.calcTotalTradersAtHome();
                        break;

                    case 3:
                        this.lblTroopType.Text = SK.Text("GENERIC_Spiese", "Spies");
                        numPeasants = 0;
                        break;

                    case 4:
                        this.lblTroopType.Text = SK.Text("GENERIC_Scouts", "Scouts");
                        numPeasants = village.calcTotalScoutsAtHome();
                        break;

                    case 70:
                        this.lblTroopType.Text = SK.Text("GENERIC_Peasants", "Peasants");
                        numPeasants = village.m_numPeasants;
                        break;

                    case 0x47:
                        this.lblTroopType.Text = SK.Text("GENERIC_Swordsmen", "Swordsmen");
                        numPeasants = village.m_numSwordsmen;
                        break;

                    case 0x48:
                        this.lblTroopType.Text = SK.Text("GENERIC_Archers", "Archers");
                        numPeasants = village.m_numArchers;
                        break;

                    case 0x49:
                        this.lblTroopType.Text = SK.Text("GENERIC_Pikemen", "Pikemen");
                        numPeasants = village.m_numPikemen;
                        break;

                    case 0x4a:
                        this.lblTroopType.Text = SK.Text("GENERIC_Catapults", "Catapults");
                        numPeasants = village.m_numCatapults;
                        break;

                    case 100:
                        this.lblTroopType.Text = SK.Text("GENERIC_Captains", "Captains");
                        numPeasants = village.m_numCaptains;
                        break;
                }
                this.lblMax.Text = numPeasants.ToString();
            }
            this.tbTroopsDisband.Position = new Point((base.Width / 2) - (GFXLibrary.int_slidebar_ruler.Width / 2), 40);
            this.tbTroopsDisband.Size = new Size(base.Width - 50, 0x17);
            this.tbTroopsDisband.StepValue = 1;
            this.tbTroopsDisband.Value = 0;
            this.tbTroopsDisband.Max = numPeasants;
            this.tbTroopsDisband.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.trackMoved));
            this.tbTroopsDisband.Create((Image) GFXLibrary.int_slidebar_ruler, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider);
            this.lblMin.Text = "0";
            this.lblMin.Color = ARGBColors.White;
            this.lblMin.DropShadowColor = ARGBColors.Black;
            this.lblMin.Position = new Point(0, this.tbTroopsDisband.Position.Y);
            this.lblMin.Size = new Size(this.tbTroopsDisband.Position.X - 10, this.tbTroopsDisband.Height);
            this.lblMin.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.lblMin.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.lblMax.Color = ARGBColors.White;
            this.lblMax.DropShadowColor = ARGBColors.Black;
            this.lblMax.Position = new Point(this.tbTroopsDisband.Rectangle.Right + 5, this.tbTroopsDisband.Position.Y);
            this.lblMax.Size = new Size((base.Width - this.tbTroopsDisband.Rectangle.Right) - 10, this.tbTroopsDisband.Height);
            this.lblMax.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.lblMax.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.lblCurValue.Text = SK.Text("GENERIC_Disband", "Disband");
            this.lblCurValue.Text = this.lblCurValue.Text + ": 0";
            this.lblCurValue.Color = ARGBColors.White;
            this.lblCurValue.DropShadowColor = ARGBColors.Black;
            this.lblCurValue.Position = new Point(this.tbTroopsDisband.Position.X, this.tbTroopsDisband.Rectangle.Bottom + 10);
            this.lblCurValue.Size = new Size(base.Width, 0x1a);
            this.lblCurValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.lblCurValue.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.btnDisband.Text.Text = SK.Text("GENERIC_Disband", "Disband");
            this.btnDisband.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.btnDisband.ImageOver = (Image) GFXLibrary.button_132_over;
            this.btnDisband.ImageClick = (Image) GFXLibrary.button_132_in;
            this.btnDisband.setSizeToImage();
            this.btnDisband.Position = new Point((base.Width / 2) - (this.btnDisband.Width / 2), this.lblCurValue.Rectangle.Bottom + 10);
            this.btnDisband.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.btnDisband.TextYOffset = -2;
            this.btnDisband.Text.Color = ARGBColors.Black;
            this.btnDisband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandClick), "Disband_Disband");
            this.btnDisband.Enabled = true;
            this.btnEdit.ImageNorm = (Image) GFXLibrary.faction_pen;
            this.btnEdit.ImageOver = (Image) GFXLibrary.faction_pen;
            this.btnEdit.ImageClick = (Image) GFXLibrary.faction_pen;
            this.btnEdit.setSizeToImage();
            this.btnEdit.MoveOnClick = true;
            this.btnEdit.OverBrighten = true;
            this.btnEdit.Position = new Point(this.tbTroopsDisband.Rectangle.Right - this.btnEdit.Width, this.lblCurValue.Position.Y);
            this.btnEdit.Data = 1;
            this.btnEdit.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editValue), "Disband_EditValue");
            if (this.imgBackground.Image != null)
            {
                this.imgBackground.addControl(this.btnEdit);
                this.imgBackground.addControl(this.btnDisband);
                this.imgBackground.addControl(this.lblCurValue);
                this.imgBackground.addControl(this.lblMax);
                this.imgBackground.addControl(this.lblMin);
                this.imgBackground.addControl(this.tbTroopsDisband);
                this.imgBackground.addControl(this.lblTroopType);
            }
            else
            {
                base.addControl(this.btnEdit);
                base.addControl(this.btnDisband);
                base.addControl(this.lblCurValue);
                base.addControl(this.lblMax);
                base.addControl(this.lblMin);
                base.addControl(this.tbTroopsDisband);
                base.addControl(this.lblTroopType);
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
        }

        private void setTrackCB(int value)
        {
            this.tbTroopsDisband.Value = value;
            this.tbTroopsDisband.invalidate();
            base.Invalidate();
            this.trackMoved();
        }

        private void trackMoved()
        {
            this.lblCurValue.Text = SK.Text("GENERIC_Disband", "Disband");
            this.lblCurValue.Text = this.lblCurValue.Text + ": " + this.tbTroopsDisband.Value;
        }
    }
}

