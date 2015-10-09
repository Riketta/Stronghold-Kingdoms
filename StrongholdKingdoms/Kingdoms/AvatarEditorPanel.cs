namespace Kingdoms
{
    using CommonTypes;
    using StatTracking;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AvatarEditorPanel : UserControl, IDockableControl
    {
        private bool allowItemChangeSFX = true;
        private BitmapButton btnDefault;
        private BitmapButton btnLastSaved;
        private BitmapButton btnRandom;
        private BitmapButton btnUploadAvatar;
        private IContainer components;
        private DockableControl dockableControl;
        private bool forceUpdate;
        private AvatarPanel imgAvatar;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Color[] lastArmsColours;
        private Color[] lastBodyColours;
        private AvatarData lastData;
        private Color[] lastFeetColours;
        private Color[] lastHairColours;
        private Color[] lastHandsColours;
        private Color[] lastHeadColours;
        private Color[] lastLegColours;
        private Color[] lastShouldersColours;
        private Color[] lastTabardColours;
        private Color[] lastTorsoColours;
        private Color[] lastWeaponColours;
        private Panel panel1;
        private Panel pnlArms;
        private CustomSelfDrawPanel pnlArmsCSD;
        private Panel pnlBody;
        private CustomSelfDrawPanel pnlBodyCSD;
        private Panel pnlFace;
        private CustomSelfDrawPanel pnlFaceCSD;
        private Panel pnlFeet;
        private CustomSelfDrawPanel pnlFeetCSD;
        private Panel pnlFloor;
        private CustomSelfDrawPanel pnlFloorCSD;
        private Panel pnlHair;
        private CustomSelfDrawPanel pnlHairCSD;
        private Panel pnlHands;
        private CustomSelfDrawPanel pnlHandsCSD;
        private Panel pnlHead;
        private CustomSelfDrawPanel pnlHeadCSD;
        private Panel pnlLegs;
        private CustomSelfDrawPanel pnlLegsCSD;
        private Panel pnlShoulders;
        private CustomSelfDrawPanel pnlShouldersCSD;
        private Panel pnlTabard;
        private CustomSelfDrawPanel pnlTabardCSD;
        private Panel pnlTorso;
        private CustomSelfDrawPanel pnlTorsoCSD;
        private Panel pnlWeapon;
        private CustomSelfDrawPanel pnlWeaponCSD;
        private RadioButton rbArms1;
        private RadioButton rbArms2;
        private RadioButton rbArms3;
        private RadioButton rbArms4;
        private RadioButton rbArmsOff;
        private RadioButton rbBody1;
        private RadioButton rbFace1;
        private RadioButton rbFace2;
        private RadioButton rbFace3;
        private RadioButton rbFace4;
        private RadioButton rbFace5;
        private RadioButton rbFace6;
        private RadioButton rbFace7;
        private RadioButton rbFeet1;
        private RadioButton rbFeet2;
        private RadioButton rbFeet3;
        private RadioButton rbFeet4;
        private RadioButton rbFeet5;
        private RadioButton rbFeet6;
        private RadioButton rbFeetOff;
        private RadioButton rbFemale;
        private RadioButton rbFloor1;
        private RadioButton rbFloor10;
        private RadioButton rbFloor11;
        private RadioButton rbFloor2;
        private RadioButton rbFloor3;
        private RadioButton rbFloor4;
        private RadioButton rbFloor5;
        private RadioButton rbFloor6;
        private RadioButton rbFloor7;
        private RadioButton rbFloor8;
        private RadioButton rbFloor9;
        private RadioButton rbHair1;
        private RadioButton rbHair2;
        private RadioButton rbHair3;
        private RadioButton rbHair4;
        private RadioButton rbHair5;
        private RadioButton rbHair6;
        private RadioButton rbHairOff;
        private RadioButton rbHands1;
        private RadioButton rbHands2;
        private RadioButton rbHands3;
        private RadioButton rbHands4;
        private RadioButton rbHandsOff;
        private RadioButton rbHead1;
        private RadioButton rbHead10;
        private RadioButton rbHead11;
        private RadioButton rbHead12;
        private RadioButton rbHead2;
        private RadioButton rbHead3;
        private RadioButton rbHead4;
        private RadioButton rbHead5;
        private RadioButton rbHead6;
        private RadioButton rbHead7;
        private RadioButton rbHead8;
        private RadioButton rbHead9;
        private RadioButton rbHeadOff;
        private RadioButton rbLegs1;
        private RadioButton rbLegs2;
        private RadioButton rbLegs3;
        private RadioButton rbLegs4;
        private RadioButton rbLegs5;
        private RadioButton rbLegs6;
        private RadioButton rbLegs7;
        private RadioButton rbMale;
        private RadioButton rbShoulderOff;
        private RadioButton rbShoulders1;
        private RadioButton rbShoulders2;
        private RadioButton rbShoulders3;
        private RadioButton rbShoulders4;
        private RadioButton rbTabard1;
        private RadioButton rbTabard2;
        private RadioButton rbTabard3;
        private RadioButton rbTabard4;
        private RadioButton rbTabard5;
        private RadioButton rbTabard6;
        private RadioButton rbTabard7;
        private RadioButton rbTabard8;
        private RadioButton rbTabardOff;
        private RadioButton rbTorso1;
        private RadioButton rbTorso2;
        private RadioButton rbTorso3;
        private RadioButton rbTorso4;
        private RadioButton rbWeapon1;
        private RadioButton rbWeapon2;
        private RadioButton rbWeapon3;
        private RadioButton rbWeapon4;
        private RadioButton rbWeapon5;
        private RadioButton rbWeapon6;
        private RadioButton rbWeaponOff;

        public AvatarEditorPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public Color addColours(CustomSelfDrawPanel panel, Color[] colours, Color curColour, int row)
        {
            panel.clearControls();
            CustomSelfDrawPanel.CSDFill fill = null;
            bool flag = false;
            int num = 0;
            foreach (Color color in colours)
            {
                CustomSelfDrawPanel.CSDFill control = new CustomSelfDrawPanel.CSDFill {
                    Size = new Size(12, 12),
                    Position = new Point(num * 13, 4)
                };
                control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.colourClickedCSD));
                control.FillColor = color;
                panel.addControl(control);
                num++;
                if (color == curColour)
                {
                    flag = true;
                    control.Border = true;
                }
                if (fill == null)
                {
                    fill = control;
                }
            }
            if (!flag)
            {
                curColour = colours[0];
                fill.Border = true;
            }
            panel.Invalidate();
            return curColour;
        }

        public Color addColours(Panel panel, Color[] colours, Color curColour, int row)
        {
            Panel panel2 = null;
            bool flag = false;
            int num = 0;
            foreach (Color color in colours)
            {
                Panel panel3 = new Panel();
                panel3.SuspendLayout();
                panel3.Size = new Size(12, 12);
                panel3.Location = new Point(280 + (num * 13), 4);
                panel3.Click += new EventHandler(this.colourClicked);
                panel3.BackColor = color;
                panel.Controls.Add(panel3);
                num++;
                if (color == curColour)
                {
                    flag = true;
                    panel3.BorderStyle = BorderStyle.FixedSingle;
                }
                if (panel2 == null)
                {
                    panel2 = panel3;
                }
            }
            if (!flag)
            {
                curColour = colours[0];
                panel2.BorderStyle = BorderStyle.FixedSingle;
            }
            return curColour;
        }

        private void AvatarEditorPanel_Load(object sender, EventArgs e)
        {
            this.rbMale.Text = SK.Text("AvatarEditor_Male", "Male");
            this.label1.Text = SK.Text("AvatarEditor_Sex", "Sex");
            this.rbFemale.Text = SK.Text("AvatarEditor_Female", "Female");
            this.label2.Text = SK.Text("AvatarEditor_Floor", "Floor");
            this.label3.Text = SK.Text("AvatarEditor_Legs", "Legs");
            this.label4.Text = SK.Text("AvatarEditor_Body", "Body");
            this.label5.Text = SK.Text("AvatarEditor_Feet", "Feet");
            this.label6.Text = SK.Text("AvatarEditor_Torso", "Torso");
            this.label7.Text = SK.Text("AvatarEditor_Tabard", "Tabard");
            this.label8.Text = SK.Text("AvatarEditor_Hands", "Hands");
            this.label9.Text = SK.Text("AvatarEditor_Arms", "Arms");
            this.label10.Text = SK.Text("AvatarEditor_Shoulders", "Shoulders");
            this.label11.Text = SK.Text("AvatarEditor_Face", "Face");
            this.label12.Text = SK.Text("AvatarEditor_Hair", "Hair");
            this.label13.Text = SK.Text("AvatarEditor_Head", "Head");
            this.label14.Text = SK.Text("AvatarEditor_Weapon", "Weapon");
            this.btnUploadAvatar.Text = SK.Text("AvatarEditor_Upload_Avatar", "Upload Avatar");
            this.btnDefault.Text = SK.Text("AvatarEditor_Reset_To_Default", "Reset To Default");
            this.btnLastSaved.Text = SK.Text("AvatarEditor_Reset_Last_Saved", "Reset To Last Saved");
            this.btnRandom.Text = "";
            this.btnRandom.ImageNormal = (Image) GFXLibrary.avatar_randomise;
            this.BackgroundImage = (Image) GFXLibrary.background_top;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("avatar_reset_to_default");
            AvatarData avatar = new AvatarData();
            avatar.validateColours();
            this.lastBodyColours = null;
            this.lastLegColours = null;
            this.lastFeetColours = null;
            this.lastTorsoColours = null;
            this.lastTabardColours = null;
            this.lastArmsColours = null;
            this.lastHandsColours = null;
            this.lastShouldersColours = null;
            this.lastHairColours = null;
            this.lastHeadColours = null;
            this.lastWeaponColours = null;
            this.import(avatar);
        }

        private void btnLastSaved_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("avatar_reset_to_last_saved");
            this.lastBodyColours = null;
            this.lastLegColours = null;
            this.lastFeetColours = null;
            this.lastTorsoColours = null;
            this.lastTabardColours = null;
            this.lastArmsColours = null;
            this.lastHandsColours = null;
            this.lastShouldersColours = null;
            this.lastHairColours = null;
            this.lastHeadColours = null;
            this.lastWeaponColours = null;
            this.import(RemoteServices.Instance.UserAvatar);
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            this.lastData.head = rnd.Next(-1, 12);
            this.lastData.hair = rnd.Next(-1, 6);
            this.lastData.face = rnd.Next(0, 7);
            this.lastData.shoulder = rnd.Next(-1, 4);
            this.lastData.tabard = rnd.Next(-1, 8);
            this.lastData.torso = rnd.Next(0, 4);
            this.lastData.arms = rnd.Next(-1, 4);
            this.lastData.hands = rnd.Next(-1, 4);
            this.lastData.weapon = rnd.Next(-1, 6);
            this.lastData.legs = rnd.Next(0, 7);
            this.lastData.feet = rnd.Next(-1, 6);
            this.lastData.floor = rnd.Next(0, 11);
            this.rbHeadOff.Checked = this.lastData.head == -1;
            this.rbHead1.Checked = this.lastData.head == 0;
            this.rbHead2.Checked = this.lastData.head == 1;
            this.rbHead3.Checked = this.lastData.head == 2;
            this.rbHead4.Checked = this.lastData.head == 3;
            this.rbHead5.Checked = this.lastData.head == 4;
            this.rbHead6.Checked = this.lastData.head == 5;
            this.rbHead7.Checked = this.lastData.head == 6;
            this.rbHead8.Checked = this.lastData.head == 7;
            this.rbHead9.Checked = this.lastData.head == 8;
            this.rbHead10.Checked = this.lastData.head == 9;
            this.rbHead11.Checked = this.lastData.head == 10;
            this.rbHead12.Checked = this.lastData.head == 11;
            this.rbHairOff.Checked = this.lastData.hair == -1;
            this.rbHair1.Checked = this.lastData.hair == 0;
            this.rbHair2.Checked = this.lastData.hair == 1;
            this.rbHair3.Checked = this.lastData.hair == 2;
            this.rbHair4.Checked = this.lastData.hair == 3;
            this.rbHair5.Checked = this.lastData.hair == 4;
            this.rbHair6.Checked = this.lastData.hair == 5;
            this.rbShoulderOff.Checked = this.lastData.shoulder == -1;
            this.rbShoulders1.Checked = this.lastData.shoulder == 0;
            this.rbShoulders2.Checked = this.lastData.shoulder == 1;
            this.rbShoulders3.Checked = this.lastData.shoulder == 2;
            this.rbShoulders4.Checked = this.lastData.shoulder == 3;
            this.rbTabardOff.Checked = this.lastData.tabard == -1;
            this.rbTabard1.Checked = this.lastData.tabard == 0;
            this.rbTabard2.Checked = this.lastData.tabard == 1;
            this.rbTabard3.Checked = this.lastData.tabard == 2;
            this.rbTabard4.Checked = this.lastData.tabard == 3;
            this.rbTabard5.Checked = this.lastData.tabard == 4;
            this.rbTabard6.Checked = this.lastData.tabard == 5;
            this.rbTabard7.Checked = this.lastData.tabard == 6;
            this.rbTabard8.Checked = this.lastData.tabard == 7;
            this.rbArmsOff.Checked = this.lastData.arms == -1;
            this.rbArms1.Checked = this.lastData.arms == 0;
            this.rbArms2.Checked = this.lastData.arms == 1;
            this.rbArms3.Checked = this.lastData.arms == 2;
            this.rbArms4.Checked = this.lastData.arms == 3;
            this.rbHandsOff.Checked = this.lastData.hands == -1;
            this.rbHands1.Checked = this.lastData.hands == 0;
            this.rbHands2.Checked = this.lastData.hands == 1;
            this.rbHands3.Checked = this.lastData.hands == 2;
            this.rbHands4.Checked = this.lastData.hands == 3;
            this.rbWeaponOff.Checked = this.lastData.weapon == -1;
            this.rbWeapon1.Checked = this.lastData.weapon == 0;
            this.rbWeapon2.Checked = this.lastData.weapon == 1;
            this.rbWeapon3.Checked = this.lastData.weapon == 2;
            this.rbWeapon4.Checked = this.lastData.weapon == 3;
            this.rbWeapon5.Checked = this.lastData.weapon == 4;
            this.rbWeapon6.Checked = this.lastData.weapon == 5;
            this.rbFeetOff.Checked = this.lastData.feet == -1;
            this.rbFeet1.Checked = this.lastData.feet == 0;
            this.rbFeet2.Checked = this.lastData.feet == 1;
            this.rbFeet3.Checked = this.lastData.feet == 2;
            this.rbFeet4.Checked = this.lastData.feet == 3;
            this.rbFeet5.Checked = this.lastData.feet == 4;
            this.rbFeet6.Checked = this.lastData.feet == 5;
            this.rbFace1.Checked = this.lastData.face == 0;
            this.rbFace2.Checked = this.lastData.face == 1;
            this.rbFace3.Checked = this.lastData.face == 2;
            this.rbFace4.Checked = this.lastData.face == 3;
            this.rbFace5.Checked = this.lastData.face == 4;
            this.rbFace6.Checked = this.lastData.face == 5;
            this.rbFace7.Checked = this.lastData.face == 6;
            this.rbTorso1.Checked = this.lastData.torso == 0;
            this.rbTorso2.Checked = this.lastData.torso == 1;
            this.rbTorso3.Checked = this.lastData.torso == 2;
            this.rbTorso4.Checked = this.lastData.torso == 3;
            this.rbLegs1.Checked = this.lastData.legs == 0;
            this.rbLegs2.Checked = this.lastData.legs == 1;
            this.rbLegs3.Checked = this.lastData.legs == 2;
            this.rbLegs4.Checked = this.lastData.legs == 3;
            this.rbLegs5.Checked = this.lastData.legs == 4;
            this.rbLegs6.Checked = this.lastData.legs == 5;
            this.rbLegs7.Checked = this.lastData.legs == 6;
            this.rbFloor1.Checked = this.lastData.floor == 0;
            this.rbFloor2.Checked = this.lastData.floor == 1;
            this.rbFloor3.Checked = this.lastData.floor == 2;
            this.rbFloor4.Checked = this.lastData.floor == 3;
            this.rbFloor5.Checked = this.lastData.floor == 4;
            this.rbFloor6.Checked = this.lastData.floor == 5;
            this.rbFloor7.Checked = this.lastData.floor == 6;
            this.rbFloor8.Checked = this.lastData.floor == 7;
            this.rbFloor9.Checked = this.lastData.floor == 8;
            this.rbFloor10.Checked = this.lastData.floor == 9;
            this.rbFloor11.Checked = this.lastData.floor == 10;
            this.lastBodyColours = null;
            this.lastLegColours = null;
            this.lastFeetColours = null;
            this.lastTorsoColours = null;
            this.lastTabardColours = null;
            this.lastArmsColours = null;
            this.lastHandsColours = null;
            this.lastShouldersColours = null;
            this.lastHairColours = null;
            this.lastHeadColours = null;
            this.lastWeaponColours = null;
            this.forceUpdate = true;
            this.update();
            this.imgAvatar.update(this.lastData);
            this.lastBodyColours = null;
            this.lastLegColours = null;
            this.lastFeetColours = null;
            this.lastTorsoColours = null;
            this.lastTabardColours = null;
            this.lastArmsColours = null;
            this.lastHandsColours = null;
            this.lastShouldersColours = null;
            this.lastHairColours = null;
            this.lastHeadColours = null;
            this.lastWeaponColours = null;
            this.setRandomColour(this.pnlArmsCSD, rnd);
            this.setRandomColour(this.pnlBodyCSD, rnd);
            this.setRandomColour(this.pnlFaceCSD, rnd);
            this.setRandomColour(this.pnlFeetCSD, rnd);
            this.setRandomColour(this.pnlFloorCSD, rnd);
            this.setRandomColour(this.pnlHairCSD, rnd);
            this.setRandomColour(this.pnlHandsCSD, rnd);
            this.setRandomColour(this.pnlHeadCSD, rnd);
            this.setRandomColour(this.pnlLegsCSD, rnd);
            this.setRandomColour(this.pnlShouldersCSD, rnd);
            this.setRandomColour(this.pnlTabardCSD, rnd);
            this.setRandomColour(this.pnlTorsoCSD, rnd);
            this.setRandomColour(this.pnlWeaponCSD, rnd);
            StatTrackingClient.Instance().ActivateTrigger(0x17, null);
            this.forceUpdate = true;
            this.update();
            this.imgAvatar.update(this.lastData);
        }

        private void btnUploadAvatar_Click(object sender, EventArgs e)
        {
            if (this.lastData != null)
            {
                GameEngine.Instance.playInterfaceSound("avatar_upload");
                AvatarData avatarData = this.lastData.clone();
                RemoteServices.Instance.set_UploadAvatar_UserCallBack(new RemoteServices.UploadAvatar_UserCallBack(this.uploadAvatarCallback));
                RemoteServices.Instance.UploadAvatar(avatarData);
                RemoteServices.Instance.UserAvatar = avatarData;
                GameEngine.Instance.World.reSetRanking();
            }
        }

        private void checkedChanged(object sender, EventArgs e)
        {
            if (!this.forceUpdate)
            {
                this.forceUpdate = true;
            }
            if (this.allowItemChangeSFX)
            {
                GameEngine.Instance.playInterfaceSound("avatar_item_changed");
            }
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
        }

        public void colourClicked(object sender, EventArgs e)
        {
            Panel panel = (Panel) sender;
            Panel parent = (Panel) panel.Parent;
            foreach (Control control in parent.Controls)
            {
                try
                {
                    Panel panel3 = (Panel) control;
                    if (panel3 != null)
                    {
                        if ((panel3.BorderStyle == BorderStyle.FixedSingle) && (panel == panel3))
                        {
                            return;
                        }
                        panel3.BorderStyle = BorderStyle.None;
                    }
                }
                catch (Exception)
                {
                }
            }
            panel.BorderStyle = BorderStyle.FixedSingle;
            this.forceUpdate = true;
            this.update();
        }

        public void colourClickedCSD()
        {
            CustomSelfDrawPanel.CSDFill staticClickedControl = (CustomSelfDrawPanel.CSDFill) CustomSelfDrawPanel.StaticClickedControl;
            if (staticClickedControl != null)
            {
                GameEngine.Instance.playInterfaceSound("avatar_colour_clicked");
                foreach (CustomSelfDrawPanel.CSDFill fill2 in staticClickedControl.Parent.Controls)
                {
                    fill2.Border = false;
                }
                staticClickedControl.Border = true;
                this.forceUpdate = true;
                this.update();
            }
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        public void createColours(AvatarData avatar)
        {
            Panel pnlBody = null;
            CustomSelfDrawPanel pnlBodyCSD = null;
            Color[] colorArray = null;
            colorArray = avatar.getBodyColourRange();
            if (!AvatarData.compare(colorArray, this.lastBodyColours))
            {
                pnlBody = this.pnlBody;
                pnlBodyCSD = this.pnlBodyCSD;
                avatar.BodyColour = this.addColours(pnlBodyCSD, colorArray, avatar.BodyColour, 0);
                this.lastBodyColours = colorArray;
            }
            colorArray = avatar.getLegsColourRange();
            if (!AvatarData.compare(colorArray, this.lastLegColours))
            {
                pnlBody = this.pnlLegs;
                pnlBodyCSD = this.pnlLegsCSD;
                avatar.LegsColour = this.addColours(pnlBodyCSD, colorArray, avatar.LegsColour, 1);
                this.lastLegColours = colorArray;
            }
            colorArray = avatar.getFeetColourRange();
            if (!AvatarData.compare(colorArray, this.lastFeetColours))
            {
                pnlBody = this.pnlFeet;
                pnlBodyCSD = this.pnlFeetCSD;
                avatar.FeetColour = this.addColours(pnlBodyCSD, colorArray, avatar.FeetColour, 2);
                this.lastFeetColours = colorArray;
            }
            colorArray = avatar.getTorsoColourRange();
            if (!AvatarData.compare(colorArray, this.lastTorsoColours))
            {
                pnlBody = this.pnlTorso;
                pnlBodyCSD = this.pnlTorsoCSD;
                avatar.TorsoColour = this.addColours(pnlBodyCSD, colorArray, avatar.TorsoColour, 3);
                this.lastTorsoColours = colorArray;
            }
            colorArray = avatar.getTabardColourRange();
            if (!AvatarData.compare(colorArray, this.lastTabardColours))
            {
                pnlBody = this.pnlTabard;
                pnlBodyCSD = this.pnlTabardCSD;
                avatar.TabardColour = this.addColours(pnlBodyCSD, colorArray, avatar.TabardColour, 4);
                this.lastTabardColours = colorArray;
            }
            colorArray = avatar.getArmsColourRange();
            if (!AvatarData.compare(colorArray, this.lastArmsColours))
            {
                pnlBody = this.pnlArms;
                pnlBodyCSD = this.pnlArmsCSD;
                avatar.ArmsColour = this.addColours(pnlBodyCSD, colorArray, avatar.ArmsColour, 5);
                this.lastArmsColours = colorArray;
            }
            colorArray = avatar.getHandsColourRange();
            if (!AvatarData.compare(colorArray, this.lastHandsColours))
            {
                pnlBody = this.pnlHands;
                pnlBodyCSD = this.pnlHandsCSD;
                avatar.HandsColour = this.addColours(pnlBodyCSD, colorArray, avatar.HandsColour, 6);
                this.lastHandsColours = colorArray;
            }
            colorArray = avatar.getShouldersColourRange();
            if (!AvatarData.compare(colorArray, this.lastShouldersColours))
            {
                pnlBody = this.pnlShoulders;
                pnlBodyCSD = this.pnlShouldersCSD;
                avatar.ShouldersColour = this.addColours(pnlBodyCSD, colorArray, avatar.ShouldersColour, 7);
                this.lastShouldersColours = colorArray;
            }
            colorArray = avatar.getHairColourRange();
            if (!AvatarData.compare(colorArray, this.lastHairColours))
            {
                pnlBody = this.pnlHair;
                pnlBodyCSD = this.pnlHairCSD;
                avatar.HairColour = this.addColours(pnlBodyCSD, colorArray, avatar.HairColour, 9);
                this.lastHairColours = colorArray;
            }
            colorArray = avatar.getHeadColourRange();
            if (!AvatarData.compare(colorArray, this.lastHeadColours))
            {
                pnlBody = this.pnlHead;
                pnlBodyCSD = this.pnlHeadCSD;
                pnlBody.SuspendLayout();
                this.removeColours(pnlBody);
                avatar.HeadColour = this.addColours(pnlBodyCSD, colorArray, avatar.HeadColour, 10);
                this.lastHeadColours = colorArray;
                this.resumeLayout(pnlBody);
            }
            colorArray = avatar.getWeaponColourRange();
            if (!AvatarData.compare(colorArray, this.lastWeaponColours))
            {
                pnlBody = this.pnlWeapon;
                pnlBodyCSD = this.pnlWeaponCSD;
                avatar.WeaponColour = this.addColours(pnlBodyCSD, colorArray, avatar.WeaponColour, 11);
                this.lastWeaponColours = colorArray;
            }
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

        public Color getColour(CustomSelfDrawPanel panel)
        {
            foreach (CustomSelfDrawPanel.CSDFill fill in panel.baseControl.Controls)
            {
                if ((fill != null) && fill.Border)
                {
                    return fill.FillColor;
                }
            }
            return ARGBColors.White;
        }

        public Color getColour(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                try
                {
                    Panel panel2 = (Panel) control;
                    if ((panel2 != null) && (panel2.BorderStyle == BorderStyle.FixedSingle))
                    {
                        return panel2.BackColor;
                    }
                }
                catch (Exception)
                {
                }
            }
            return ARGBColors.White;
        }

        public void import(AvatarData avatar)
        {
            this.rbArms4.Visible = true;
            this.rbFace3.Visible = true;
            this.rbFace4.Visible = true;
            this.rbFeet4.Visible = true;
            this.rbFloor3.Visible = true;
            this.rbFloor4.Visible = true;
            this.rbFloor5.Visible = true;
            this.rbHands4.Visible = true;
            this.rbHead4.Visible = true;
            this.rbLegs4.Visible = true;
            this.rbLegs5.Visible = true;
            this.rbTabard3.Visible = true;
            this.rbTabard4.Visible = true;
            this.allowItemChangeSFX = false;
            this.lastData = avatar;
            this.forceUpdate = true;
            if (avatar.male)
            {
                this.rbMale.Checked = true;
            }
            else
            {
                this.rbFemale.Checked = true;
            }
            switch (avatar.floor)
            {
                case 0:
                    this.rbFloor1.Checked = true;
                    break;

                case 1:
                    this.rbFloor2.Checked = true;
                    break;

                case 2:
                    this.rbFloor3.Checked = true;
                    break;

                case 3:
                    this.rbFloor4.Checked = true;
                    break;

                case 4:
                    this.rbFloor5.Checked = true;
                    break;

                case 5:
                    this.rbFloor6.Checked = true;
                    break;

                case 6:
                    this.rbFloor7.Checked = true;
                    break;

                case 7:
                    this.rbFloor8.Checked = true;
                    break;

                case 8:
                    this.rbFloor9.Checked = true;
                    break;

                case 9:
                    this.rbFloor10.Checked = true;
                    break;

                case 10:
                    this.rbFloor11.Checked = true;
                    break;
            }
            if (avatar.body == 0)
            {
                this.rbBody1.Checked = true;
            }
            switch (avatar.legs)
            {
                case 0:
                    this.rbLegs1.Checked = true;
                    break;

                case 1:
                    this.rbLegs2.Checked = true;
                    break;

                case 2:
                    this.rbLegs3.Checked = true;
                    break;

                case 3:
                    this.rbLegs4.Checked = true;
                    break;

                case 4:
                    this.rbLegs5.Checked = true;
                    break;

                case 5:
                    this.rbLegs6.Checked = true;
                    break;

                case 6:
                    this.rbLegs7.Checked = true;
                    break;
            }
            switch (avatar.feet)
            {
                case -1:
                    this.rbFeetOff.Checked = true;
                    break;

                case 0:
                    this.rbFeet1.Checked = true;
                    break;

                case 1:
                    this.rbFeet2.Checked = true;
                    break;

                case 2:
                    this.rbFeet3.Checked = true;
                    break;

                case 3:
                    this.rbFeet4.Checked = true;
                    break;

                case 4:
                    this.rbFeet5.Checked = true;
                    break;

                case 5:
                    this.rbFeet6.Checked = true;
                    break;
            }
            switch (avatar.torso)
            {
                case 0:
                    this.rbTorso1.Checked = true;
                    break;

                case 1:
                    this.rbTorso2.Checked = true;
                    break;

                case 2:
                    this.rbTorso3.Checked = true;
                    break;

                case 3:
                    this.rbTorso4.Checked = true;
                    break;
            }
            switch (avatar.tabard)
            {
                case -1:
                    this.rbTabardOff.Checked = true;
                    break;

                case 0:
                    this.rbTabard1.Checked = true;
                    break;

                case 1:
                    this.rbTabard2.Checked = true;
                    break;

                case 2:
                    this.rbTabard3.Checked = true;
                    break;

                case 3:
                    this.rbTabard4.Checked = true;
                    break;

                case 4:
                    this.rbTabard5.Checked = true;
                    break;

                case 5:
                    this.rbTabard6.Checked = true;
                    break;

                case 6:
                    this.rbTabard7.Checked = true;
                    break;

                case 7:
                    this.rbTabard8.Checked = true;
                    break;
            }
            switch (avatar.arms)
            {
                case -1:
                    this.rbArmsOff.Checked = true;
                    break;

                case 0:
                    this.rbArms1.Checked = true;
                    break;

                case 1:
                    this.rbArms2.Checked = true;
                    break;

                case 2:
                    this.rbArms3.Checked = true;
                    break;

                case 3:
                    this.rbArms4.Checked = true;
                    break;
            }
            switch (avatar.hands)
            {
                case -1:
                    this.rbHandsOff.Checked = true;
                    break;

                case 0:
                    this.rbHands1.Checked = true;
                    break;

                case 1:
                    this.rbHands2.Checked = true;
                    break;

                case 2:
                    this.rbHands3.Checked = true;
                    break;

                case 3:
                    this.rbHands4.Checked = true;
                    break;
            }
            switch (avatar.shoulder)
            {
                case -1:
                    this.rbShoulderOff.Checked = true;
                    break;

                case 0:
                    this.rbShoulders1.Checked = true;
                    break;

                case 1:
                    this.rbShoulders2.Checked = true;
                    break;

                case 2:
                    this.rbShoulders3.Checked = true;
                    break;

                case 3:
                    this.rbShoulders4.Checked = true;
                    break;
            }
            switch (avatar.face)
            {
                case 0:
                    this.rbFace1.Checked = true;
                    break;

                case 1:
                    this.rbFace2.Checked = true;
                    break;

                case 2:
                    this.rbFace3.Checked = true;
                    break;

                case 3:
                    this.rbFace4.Checked = true;
                    break;

                case 4:
                    this.rbFace5.Checked = true;
                    break;

                case 5:
                    this.rbFace6.Checked = true;
                    break;

                case 6:
                    this.rbFace7.Checked = true;
                    break;
            }
            switch (avatar.hair)
            {
                case -1:
                    this.rbHairOff.Checked = true;
                    break;

                case 0:
                    this.rbHair1.Checked = true;
                    break;

                case 1:
                    this.rbHair2.Checked = true;
                    break;

                case 2:
                    this.rbHair3.Checked = true;
                    break;

                case 3:
                    this.rbHair4.Checked = true;
                    break;

                case 4:
                    this.rbHair5.Checked = true;
                    break;

                case 5:
                    this.rbHair6.Checked = true;
                    break;
            }
            switch (avatar.head)
            {
                case -1:
                    this.rbHeadOff.Checked = true;
                    break;

                case 0:
                    this.rbHead1.Checked = true;
                    break;

                case 1:
                    this.rbHead2.Checked = true;
                    break;

                case 2:
                    this.rbHead3.Checked = true;
                    break;

                case 3:
                    this.rbHead4.Checked = true;
                    break;

                case 4:
                    this.rbHead5.Checked = true;
                    break;

                case 5:
                    this.rbHead6.Checked = true;
                    break;

                case 6:
                    this.rbHead7.Checked = true;
                    break;

                case 7:
                    this.rbHead8.Checked = true;
                    break;

                case 8:
                    this.rbHead9.Checked = true;
                    break;

                case 9:
                    this.rbHead10.Checked = true;
                    break;

                case 10:
                    this.rbHead11.Checked = true;
                    break;

                case 11:
                    this.rbHead12.Checked = true;
                    break;
            }
            switch (avatar.weapon)
            {
                case -1:
                    this.rbWeaponOff.Checked = true;
                    break;

                case 0:
                    this.rbWeapon1.Checked = true;
                    break;

                case 1:
                    this.rbWeapon2.Checked = true;
                    break;

                case 2:
                    this.rbWeapon3.Checked = true;
                    break;

                case 3:
                    this.rbWeapon4.Checked = true;
                    break;

                case 4:
                    this.rbWeapon5.Checked = true;
                    break;

                case 5:
                    this.rbWeapon6.Checked = true;
                    break;
            }
            this.createColours(avatar);
            this.update();
            this.allowItemChangeSFX = true;
        }

        public void init()
        {
            this.AvatarEditorPanel_Load(null, null);
            this.lastBodyColours = null;
            this.lastLegColours = null;
            this.lastFeetColours = null;
            this.lastTorsoColours = null;
            this.lastTabardColours = null;
            this.lastArmsColours = null;
            this.lastHandsColours = null;
            this.lastShouldersColours = null;
            this.lastHairColours = null;
            this.lastHeadColours = null;
            this.lastWeaponColours = null;
            this.import(RemoteServices.Instance.UserAvatar.clone());
        }

        private void InitializeComponent()
        {
            this.rbMale = new RadioButton();
            this.label1 = new Label();
            this.rbFemale = new RadioButton();
            this.rbFloor2 = new RadioButton();
            this.label2 = new Label();
            this.rbFloor1 = new RadioButton();
            this.rbLegs2 = new RadioButton();
            this.label3 = new Label();
            this.rbLegs1 = new RadioButton();
            this.rbLegs3 = new RadioButton();
            this.label4 = new Label();
            this.rbBody1 = new RadioButton();
            this.rbFeet3 = new RadioButton();
            this.rbFeet2 = new RadioButton();
            this.label5 = new Label();
            this.rbFeet1 = new RadioButton();
            this.rbTorso3 = new RadioButton();
            this.rbTorso2 = new RadioButton();
            this.label6 = new Label();
            this.rbTorso1 = new RadioButton();
            this.rbTorso4 = new RadioButton();
            this.rbTabard2 = new RadioButton();
            this.label7 = new Label();
            this.rbTabard1 = new RadioButton();
            this.rbHands3 = new RadioButton();
            this.rbHands2 = new RadioButton();
            this.label8 = new Label();
            this.rbHands1 = new RadioButton();
            this.rbArms3 = new RadioButton();
            this.rbArms2 = new RadioButton();
            this.label9 = new Label();
            this.rbArms1 = new RadioButton();
            this.rbShoulders4 = new RadioButton();
            this.rbShoulders3 = new RadioButton();
            this.rbShoulders2 = new RadioButton();
            this.label10 = new Label();
            this.rbShoulders1 = new RadioButton();
            this.rbFace2 = new RadioButton();
            this.label11 = new Label();
            this.rbFace1 = new RadioButton();
            this.rbHair4 = new RadioButton();
            this.rbHair3 = new RadioButton();
            this.rbHair2 = new RadioButton();
            this.label12 = new Label();
            this.rbHair1 = new RadioButton();
            this.rbHead3 = new RadioButton();
            this.rbHead2 = new RadioButton();
            this.rbHead1 = new RadioButton();
            this.label13 = new Label();
            this.rbHeadOff = new RadioButton();
            this.rbWeapon3 = new RadioButton();
            this.rbWeapon2 = new RadioButton();
            this.rbWeapon1 = new RadioButton();
            this.label14 = new Label();
            this.rbWeaponOff = new RadioButton();
            this.panel1 = new Panel();
            this.pnlFloor = new Panel();
            this.rbFloor6 = new RadioButton();
            this.rbFloor9 = new RadioButton();
            this.rbFloor10 = new RadioButton();
            this.rbFloor11 = new RadioButton();
            this.rbFloor8 = new RadioButton();
            this.rbFloor7 = new RadioButton();
            this.pnlFloorCSD = new CustomSelfDrawPanel();
            this.rbFloor3 = new RadioButton();
            this.rbFloor4 = new RadioButton();
            this.rbFloor5 = new RadioButton();
            this.pnlBody = new Panel();
            this.pnlBodyCSD = new CustomSelfDrawPanel();
            this.pnlLegs = new Panel();
            this.rbLegs7 = new RadioButton();
            this.rbLegs6 = new RadioButton();
            this.pnlLegsCSD = new CustomSelfDrawPanel();
            this.rbLegs5 = new RadioButton();
            this.rbLegs4 = new RadioButton();
            this.pnlFeet = new Panel();
            this.rbFeet6 = new RadioButton();
            this.rbFeet5 = new RadioButton();
            this.pnlFeetCSD = new CustomSelfDrawPanel();
            this.rbFeet4 = new RadioButton();
            this.rbFeetOff = new RadioButton();
            this.pnlTorso = new Panel();
            this.pnlTorsoCSD = new CustomSelfDrawPanel();
            this.pnlTabard = new Panel();
            this.rbTabard8 = new RadioButton();
            this.rbTabard7 = new RadioButton();
            this.rbTabard6 = new RadioButton();
            this.rbTabard5 = new RadioButton();
            this.pnlTabardCSD = new CustomSelfDrawPanel();
            this.rbTabard4 = new RadioButton();
            this.rbTabard3 = new RadioButton();
            this.rbTabardOff = new RadioButton();
            this.pnlArms = new Panel();
            this.pnlArmsCSD = new CustomSelfDrawPanel();
            this.rbArms4 = new RadioButton();
            this.rbArmsOff = new RadioButton();
            this.pnlHands = new Panel();
            this.pnlHandsCSD = new CustomSelfDrawPanel();
            this.rbHands4 = new RadioButton();
            this.rbHandsOff = new RadioButton();
            this.pnlShoulders = new Panel();
            this.pnlShouldersCSD = new CustomSelfDrawPanel();
            this.rbShoulderOff = new RadioButton();
            this.pnlFace = new Panel();
            this.rbFace7 = new RadioButton();
            this.rbFace6 = new RadioButton();
            this.rbFace5 = new RadioButton();
            this.pnlFaceCSD = new CustomSelfDrawPanel();
            this.rbFace4 = new RadioButton();
            this.rbFace3 = new RadioButton();
            this.pnlHair = new Panel();
            this.rbHair5 = new RadioButton();
            this.rbHair6 = new RadioButton();
            this.pnlHairCSD = new CustomSelfDrawPanel();
            this.rbHairOff = new RadioButton();
            this.pnlHead = new Panel();
            this.rbHead12 = new RadioButton();
            this.rbHead9 = new RadioButton();
            this.rbHead10 = new RadioButton();
            this.rbHead11 = new RadioButton();
            this.rbHead8 = new RadioButton();
            this.rbHead5 = new RadioButton();
            this.rbHead6 = new RadioButton();
            this.rbHead7 = new RadioButton();
            this.pnlHeadCSD = new CustomSelfDrawPanel();
            this.rbHead4 = new RadioButton();
            this.pnlWeapon = new Panel();
            this.rbWeapon5 = new RadioButton();
            this.rbWeapon6 = new RadioButton();
            this.pnlWeaponCSD = new CustomSelfDrawPanel();
            this.rbWeapon4 = new RadioButton();
            this.btnRandom = new BitmapButton();
            this.btnLastSaved = new BitmapButton();
            this.btnDefault = new BitmapButton();
            this.btnUploadAvatar = new BitmapButton();
            this.imgAvatar = new AvatarPanel();
            this.panel1.SuspendLayout();
            this.pnlFloor.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlLegs.SuspendLayout();
            this.pnlFeet.SuspendLayout();
            this.pnlTorso.SuspendLayout();
            this.pnlTabard.SuspendLayout();
            this.pnlArms.SuspendLayout();
            this.pnlHands.SuspendLayout();
            this.pnlShoulders.SuspendLayout();
            this.pnlFace.SuspendLayout();
            this.pnlHair.SuspendLayout();
            this.pnlHead.SuspendLayout();
            this.pnlWeapon.SuspendLayout();
            base.SuspendLayout();
            this.rbMale.AutoSize = true;
            this.rbMale.Location = new Point(0x5f, 3);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new Size(0x30, 0x11);
            this.rbMale.TabIndex = 1;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "Male";
            this.rbMale.UseVisualStyleBackColor = true;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 5);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x19, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sex";
            this.rbFemale.AutoSize = true;
            this.rbFemale.Location = new Point(0xa9, 3);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new Size(0x3b, 0x11);
            this.rbFemale.TabIndex = 3;
            this.rbFemale.TabStop = true;
            this.rbFemale.Text = "Female";
            this.rbFemale.UseVisualStyleBackColor = true;
            this.rbFemale.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFloor2.AutoSize = true;
            this.rbFloor2.Location = new Point(0x98, 3);
            this.rbFloor2.Name = "rbFloor2";
            this.rbFloor2.Size = new Size(14, 13);
            this.rbFloor2.TabIndex = 6;
            this.rbFloor2.TabStop = true;
            this.rbFloor2.UseVisualStyleBackColor = true;
            this.rbFloor2.CheckedChanged += new EventHandler(this.checkedChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(7, 5);
            this.label2.Name = "label2";
            this.label2.Size = new Size(30, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Floor";
            this.rbFloor1.AutoSize = true;
            this.rbFloor1.Location = new Point(0x84, 3);
            this.rbFloor1.Name = "rbFloor1";
            this.rbFloor1.Size = new Size(14, 13);
            this.rbFloor1.TabIndex = 4;
            this.rbFloor1.TabStop = true;
            this.rbFloor1.UseVisualStyleBackColor = true;
            this.rbFloor1.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbLegs2.AutoSize = true;
            this.rbLegs2.Location = new Point(0x98, 3);
            this.rbLegs2.Name = "rbLegs2";
            this.rbLegs2.Size = new Size(14, 13);
            this.rbLegs2.TabIndex = 9;
            this.rbLegs2.TabStop = true;
            this.rbLegs2.UseVisualStyleBackColor = true;
            this.rbLegs2.CheckedChanged += new EventHandler(this.checkedChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(7, 5);
            this.label3.Name = "label3";
            this.label3.Size = new Size(30, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Legs";
            this.rbLegs1.AutoSize = true;
            this.rbLegs1.Location = new Point(0x84, 3);
            this.rbLegs1.Name = "rbLegs1";
            this.rbLegs1.Size = new Size(14, 13);
            this.rbLegs1.TabIndex = 7;
            this.rbLegs1.TabStop = true;
            this.rbLegs1.UseVisualStyleBackColor = true;
            this.rbLegs1.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbLegs3.AutoSize = true;
            this.rbLegs3.Location = new Point(0xac, 3);
            this.rbLegs3.Name = "rbLegs3";
            this.rbLegs3.Size = new Size(14, 13);
            this.rbLegs3.TabIndex = 10;
            this.rbLegs3.TabStop = true;
            this.rbLegs3.UseVisualStyleBackColor = true;
            this.rbLegs3.CheckedChanged += new EventHandler(this.checkedChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(7, 5);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1f, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Body";
            this.rbBody1.AutoSize = true;
            this.rbBody1.Location = new Point(0x84, 3);
            this.rbBody1.Name = "rbBody1";
            this.rbBody1.Size = new Size(14, 13);
            this.rbBody1.TabIndex = 11;
            this.rbBody1.TabStop = true;
            this.rbBody1.UseVisualStyleBackColor = true;
            this.rbBody1.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFeet3.AutoSize = true;
            this.rbFeet3.Location = new Point(0xac, 3);
            this.rbFeet3.Name = "rbFeet3";
            this.rbFeet3.Size = new Size(14, 13);
            this.rbFeet3.TabIndex = 0x10;
            this.rbFeet3.TabStop = true;
            this.rbFeet3.UseVisualStyleBackColor = true;
            this.rbFeet3.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFeet2.AutoSize = true;
            this.rbFeet2.Location = new Point(0x98, 3);
            this.rbFeet2.Name = "rbFeet2";
            this.rbFeet2.Size = new Size(14, 13);
            this.rbFeet2.TabIndex = 15;
            this.rbFeet2.TabStop = true;
            this.rbFeet2.UseVisualStyleBackColor = true;
            this.rbFeet2.CheckedChanged += new EventHandler(this.checkedChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(7, 5);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1c, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Feet";
            this.rbFeet1.AutoSize = true;
            this.rbFeet1.Location = new Point(0x84, 3);
            this.rbFeet1.Name = "rbFeet1";
            this.rbFeet1.Size = new Size(14, 13);
            this.rbFeet1.TabIndex = 13;
            this.rbFeet1.TabStop = true;
            this.rbFeet1.UseVisualStyleBackColor = true;
            this.rbFeet1.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbTorso3.AutoSize = true;
            this.rbTorso3.Location = new Point(0xac, 3);
            this.rbTorso3.Name = "rbTorso3";
            this.rbTorso3.Size = new Size(14, 13);
            this.rbTorso3.TabIndex = 20;
            this.rbTorso3.TabStop = true;
            this.rbTorso3.UseVisualStyleBackColor = true;
            this.rbTorso3.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbTorso2.AutoSize = true;
            this.rbTorso2.Location = new Point(0x98, 3);
            this.rbTorso2.Name = "rbTorso2";
            this.rbTorso2.Size = new Size(14, 13);
            this.rbTorso2.TabIndex = 0x13;
            this.rbTorso2.TabStop = true;
            this.rbTorso2.UseVisualStyleBackColor = true;
            this.rbTorso2.CheckedChanged += new EventHandler(this.checkedChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(7, 5);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x22, 13);
            this.label6.TabIndex = 0x12;
            this.label6.Text = "Torso";
            this.rbTorso1.AutoSize = true;
            this.rbTorso1.Location = new Point(0x84, 3);
            this.rbTorso1.Name = "rbTorso1";
            this.rbTorso1.Size = new Size(14, 13);
            this.rbTorso1.TabIndex = 0x11;
            this.rbTorso1.TabStop = true;
            this.rbTorso1.UseVisualStyleBackColor = true;
            this.rbTorso1.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbTorso4.AutoSize = true;
            this.rbTorso4.Location = new Point(0xc0, 3);
            this.rbTorso4.Name = "rbTorso4";
            this.rbTorso4.Size = new Size(14, 13);
            this.rbTorso4.TabIndex = 0x15;
            this.rbTorso4.TabStop = true;
            this.rbTorso4.UseVisualStyleBackColor = true;
            this.rbTorso4.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbTabard2.AutoSize = true;
            this.rbTabard2.Location = new Point(0x98, 3);
            this.rbTabard2.Name = "rbTabard2";
            this.rbTabard2.Size = new Size(14, 13);
            this.rbTabard2.TabIndex = 0x18;
            this.rbTabard2.TabStop = true;
            this.rbTabard2.UseVisualStyleBackColor = true;
            this.rbTabard2.CheckedChanged += new EventHandler(this.checkedChanged);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(7, 5);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x29, 13);
            this.label7.TabIndex = 0x17;
            this.label7.Text = "Tabard";
            this.rbTabard1.AutoSize = true;
            this.rbTabard1.Location = new Point(0x84, 3);
            this.rbTabard1.Name = "rbTabard1";
            this.rbTabard1.Size = new Size(14, 13);
            this.rbTabard1.TabIndex = 0x16;
            this.rbTabard1.TabStop = true;
            this.rbTabard1.UseVisualStyleBackColor = true;
            this.rbTabard1.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHands3.AutoSize = true;
            this.rbHands3.Location = new Point(0xac, 3);
            this.rbHands3.Name = "rbHands3";
            this.rbHands3.Size = new Size(14, 13);
            this.rbHands3.TabIndex = 0x20;
            this.rbHands3.TabStop = true;
            this.rbHands3.UseVisualStyleBackColor = true;
            this.rbHands3.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHands2.AutoSize = true;
            this.rbHands2.Location = new Point(0x98, 3);
            this.rbHands2.Name = "rbHands2";
            this.rbHands2.Size = new Size(14, 13);
            this.rbHands2.TabIndex = 0x1f;
            this.rbHands2.TabStop = true;
            this.rbHands2.UseVisualStyleBackColor = true;
            this.rbHands2.CheckedChanged += new EventHandler(this.checkedChanged);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(7, 5);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x26, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Hands";
            this.rbHands1.AutoSize = true;
            this.rbHands1.Location = new Point(0x84, 3);
            this.rbHands1.Name = "rbHands1";
            this.rbHands1.Size = new Size(14, 13);
            this.rbHands1.TabIndex = 0x1d;
            this.rbHands1.TabStop = true;
            this.rbHands1.UseVisualStyleBackColor = true;
            this.rbHands1.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbArms3.AutoSize = true;
            this.rbArms3.Location = new Point(0xac, 3);
            this.rbArms3.Name = "rbArms3";
            this.rbArms3.Size = new Size(14, 13);
            this.rbArms3.TabIndex = 0x1c;
            this.rbArms3.TabStop = true;
            this.rbArms3.UseVisualStyleBackColor = true;
            this.rbArms3.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbArms2.AutoSize = true;
            this.rbArms2.Location = new Point(0x98, 3);
            this.rbArms2.Name = "rbArms2";
            this.rbArms2.Size = new Size(14, 13);
            this.rbArms2.TabIndex = 0x1b;
            this.rbArms2.TabStop = true;
            this.rbArms2.UseVisualStyleBackColor = true;
            this.rbArms2.CheckedChanged += new EventHandler(this.checkedChanged);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(7, 5);
            this.label9.Name = "label9";
            this.label9.Size = new Size(30, 13);
            this.label9.TabIndex = 0x1a;
            this.label9.Text = "Arms";
            this.rbArms1.AutoSize = true;
            this.rbArms1.Location = new Point(0x84, 3);
            this.rbArms1.Name = "rbArms1";
            this.rbArms1.Size = new Size(14, 13);
            this.rbArms1.TabIndex = 0x19;
            this.rbArms1.TabStop = true;
            this.rbArms1.UseVisualStyleBackColor = true;
            this.rbArms1.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbShoulders4.AutoSize = true;
            this.rbShoulders4.Location = new Point(0xc0, 3);
            this.rbShoulders4.Name = "rbShoulders4";
            this.rbShoulders4.Size = new Size(14, 13);
            this.rbShoulders4.TabIndex = 0x25;
            this.rbShoulders4.TabStop = true;
            this.rbShoulders4.UseVisualStyleBackColor = true;
            this.rbShoulders4.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbShoulders3.AutoSize = true;
            this.rbShoulders3.Location = new Point(0xac, 3);
            this.rbShoulders3.Name = "rbShoulders3";
            this.rbShoulders3.Size = new Size(14, 13);
            this.rbShoulders3.TabIndex = 0x24;
            this.rbShoulders3.TabStop = true;
            this.rbShoulders3.UseVisualStyleBackColor = true;
            this.rbShoulders3.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbShoulders2.AutoSize = true;
            this.rbShoulders2.Location = new Point(0x98, 3);
            this.rbShoulders2.Name = "rbShoulders2";
            this.rbShoulders2.Size = new Size(14, 13);
            this.rbShoulders2.TabIndex = 0x23;
            this.rbShoulders2.TabStop = true;
            this.rbShoulders2.UseVisualStyleBackColor = true;
            this.rbShoulders2.CheckedChanged += new EventHandler(this.checkedChanged);
            this.label10.AutoSize = true;
            this.label10.Location = new Point(7, 5);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x36, 13);
            this.label10.TabIndex = 0x22;
            this.label10.Text = "Shoulders";
            this.rbShoulders1.AutoSize = true;
            this.rbShoulders1.Location = new Point(0x84, 3);
            this.rbShoulders1.Name = "rbShoulders1";
            this.rbShoulders1.Size = new Size(14, 13);
            this.rbShoulders1.TabIndex = 0x21;
            this.rbShoulders1.TabStop = true;
            this.rbShoulders1.UseVisualStyleBackColor = true;
            this.rbShoulders1.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFace2.AutoSize = true;
            this.rbFace2.Location = new Point(0x98, 3);
            this.rbFace2.Name = "rbFace2";
            this.rbFace2.Size = new Size(14, 13);
            this.rbFace2.TabIndex = 40;
            this.rbFace2.TabStop = true;
            this.rbFace2.UseVisualStyleBackColor = true;
            this.rbFace2.CheckedChanged += new EventHandler(this.checkedChanged);
            this.label11.AutoSize = true;
            this.label11.Location = new Point(7, 5);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x1f, 13);
            this.label11.TabIndex = 0x27;
            this.label11.Text = "Face";
            this.rbFace1.AutoSize = true;
            this.rbFace1.Location = new Point(0x84, 3);
            this.rbFace1.Name = "rbFace1";
            this.rbFace1.Size = new Size(14, 13);
            this.rbFace1.TabIndex = 0x26;
            this.rbFace1.TabStop = true;
            this.rbFace1.UseVisualStyleBackColor = true;
            this.rbFace1.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHair4.AutoSize = true;
            this.rbHair4.Location = new Point(0xc0, 3);
            this.rbHair4.Name = "rbHair4";
            this.rbHair4.Size = new Size(14, 13);
            this.rbHair4.TabIndex = 0x2d;
            this.rbHair4.TabStop = true;
            this.rbHair4.UseVisualStyleBackColor = true;
            this.rbHair4.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHair3.AutoSize = true;
            this.rbHair3.Location = new Point(0xac, 3);
            this.rbHair3.Name = "rbHair3";
            this.rbHair3.Size = new Size(14, 13);
            this.rbHair3.TabIndex = 0x2c;
            this.rbHair3.TabStop = true;
            this.rbHair3.UseVisualStyleBackColor = true;
            this.rbHair3.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHair2.AutoSize = true;
            this.rbHair2.Location = new Point(0x98, 3);
            this.rbHair2.Name = "rbHair2";
            this.rbHair2.Size = new Size(14, 13);
            this.rbHair2.TabIndex = 0x2b;
            this.rbHair2.TabStop = true;
            this.rbHair2.UseVisualStyleBackColor = true;
            this.rbHair2.CheckedChanged += new EventHandler(this.checkedChanged);
            this.label12.AutoSize = true;
            this.label12.Location = new Point(7, 5);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x1a, 13);
            this.label12.TabIndex = 0x2a;
            this.label12.Text = "Hair";
            this.rbHair1.AutoSize = true;
            this.rbHair1.Location = new Point(0x84, 3);
            this.rbHair1.Name = "rbHair1";
            this.rbHair1.Size = new Size(14, 13);
            this.rbHair1.TabIndex = 0x29;
            this.rbHair1.TabStop = true;
            this.rbHair1.UseVisualStyleBackColor = true;
            this.rbHair1.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHead3.AutoSize = true;
            this.rbHead3.Location = new Point(0xac, 3);
            this.rbHead3.Name = "rbHead3";
            this.rbHead3.Size = new Size(14, 13);
            this.rbHead3.TabIndex = 50;
            this.rbHead3.TabStop = true;
            this.rbHead3.UseVisualStyleBackColor = true;
            this.rbHead3.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHead2.AutoSize = true;
            this.rbHead2.Location = new Point(0x98, 3);
            this.rbHead2.Name = "rbHead2";
            this.rbHead2.Size = new Size(14, 13);
            this.rbHead2.TabIndex = 0x31;
            this.rbHead2.TabStop = true;
            this.rbHead2.UseVisualStyleBackColor = true;
            this.rbHead2.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHead1.AutoSize = true;
            this.rbHead1.Location = new Point(0x84, 3);
            this.rbHead1.Name = "rbHead1";
            this.rbHead1.Size = new Size(14, 13);
            this.rbHead1.TabIndex = 0x30;
            this.rbHead1.TabStop = true;
            this.rbHead1.UseVisualStyleBackColor = true;
            this.rbHead1.CheckedChanged += new EventHandler(this.checkedChanged);
            this.label13.AutoSize = true;
            this.label13.Location = new Point(7, 5);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x21, 13);
            this.label13.TabIndex = 0x2f;
            this.label13.Text = "Head";
            this.rbHeadOff.AutoSize = true;
            this.rbHeadOff.Location = new Point(0x5f, 3);
            this.rbHeadOff.Name = "rbHeadOff";
            this.rbHeadOff.Size = new Size(0x20, 0x11);
            this.rbHeadOff.TabIndex = 0x2e;
            this.rbHeadOff.TabStop = true;
            this.rbHeadOff.Text = "X";
            this.rbHeadOff.UseVisualStyleBackColor = true;
            this.rbHeadOff.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbWeapon3.AutoSize = true;
            this.rbWeapon3.Location = new Point(0xac, 3);
            this.rbWeapon3.Name = "rbWeapon3";
            this.rbWeapon3.Size = new Size(14, 13);
            this.rbWeapon3.TabIndex = 0x37;
            this.rbWeapon3.TabStop = true;
            this.rbWeapon3.UseVisualStyleBackColor = true;
            this.rbWeapon3.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbWeapon2.AutoSize = true;
            this.rbWeapon2.Location = new Point(0x98, 3);
            this.rbWeapon2.Name = "rbWeapon2";
            this.rbWeapon2.Size = new Size(14, 13);
            this.rbWeapon2.TabIndex = 0x36;
            this.rbWeapon2.TabStop = true;
            this.rbWeapon2.UseVisualStyleBackColor = true;
            this.rbWeapon2.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbWeapon1.AutoSize = true;
            this.rbWeapon1.Location = new Point(0x84, 3);
            this.rbWeapon1.Name = "rbWeapon1";
            this.rbWeapon1.Size = new Size(14, 13);
            this.rbWeapon1.TabIndex = 0x35;
            this.rbWeapon1.TabStop = true;
            this.rbWeapon1.UseVisualStyleBackColor = true;
            this.rbWeapon1.CheckedChanged += new EventHandler(this.checkedChanged);
            this.label14.AutoSize = true;
            this.label14.Location = new Point(7, 5);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x30, 13);
            this.label14.TabIndex = 0x34;
            this.label14.Text = "Weapon";
            this.rbWeaponOff.AutoSize = true;
            this.rbWeaponOff.Location = new Point(0x5f, 3);
            this.rbWeaponOff.Name = "rbWeaponOff";
            this.rbWeaponOff.Size = new Size(0x20, 0x11);
            this.rbWeaponOff.TabIndex = 0x33;
            this.rbWeaponOff.TabStop = true;
            this.rbWeaponOff.Text = "X";
            this.rbWeaponOff.UseVisualStyleBackColor = true;
            this.rbWeaponOff.CheckedChanged += new EventHandler(this.checkedChanged);
            this.panel1.BackColor = Color.Transparent;
            this.panel1.Controls.Add(this.rbFemale);
            this.panel1.Controls.Add(this.rbMale);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new Point(0xf8, 0x1c);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x2c5, 0x16);
            this.panel1.TabIndex = 0x38;
            this.pnlFloor.BackColor = Color.Transparent;
            this.pnlFloor.Controls.Add(this.rbFloor6);
            this.pnlFloor.Controls.Add(this.rbFloor9);
            this.pnlFloor.Controls.Add(this.rbFloor10);
            this.pnlFloor.Controls.Add(this.rbFloor11);
            this.pnlFloor.Controls.Add(this.rbFloor8);
            this.pnlFloor.Controls.Add(this.rbFloor7);
            this.pnlFloor.Controls.Add(this.pnlFloorCSD);
            this.pnlFloor.Controls.Add(this.rbFloor3);
            this.pnlFloor.Controls.Add(this.rbFloor4);
            this.pnlFloor.Controls.Add(this.rbFloor5);
            this.pnlFloor.Controls.Add(this.rbFloor2);
            this.pnlFloor.Controls.Add(this.rbFloor1);
            this.pnlFloor.Controls.Add(this.label2);
            this.pnlFloor.Location = new Point(0xf8, 0x1e3);
            this.pnlFloor.Name = "pnlFloor";
            this.pnlFloor.Size = new Size(0x2c5, 0x16);
            this.pnlFloor.TabIndex = 0x39;
            this.rbFloor6.AutoSize = true;
            this.rbFloor6.Location = new Point(0xe8, 3);
            this.rbFloor6.Name = "rbFloor6";
            this.rbFloor6.Size = new Size(14, 13);
            this.rbFloor6.TabIndex = 0x41;
            this.rbFloor6.TabStop = true;
            this.rbFloor6.UseVisualStyleBackColor = true;
            this.rbFloor6.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFloor9.AutoSize = true;
            this.rbFloor9.Location = new Point(0x124, 3);
            this.rbFloor9.Name = "rbFloor9";
            this.rbFloor9.Size = new Size(14, 13);
            this.rbFloor9.TabIndex = 0x40;
            this.rbFloor9.TabStop = true;
            this.rbFloor9.UseVisualStyleBackColor = true;
            this.rbFloor9.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFloor10.AutoSize = true;
            this.rbFloor10.Location = new Point(0x138, 3);
            this.rbFloor10.Name = "rbFloor10";
            this.rbFloor10.Size = new Size(14, 13);
            this.rbFloor10.TabIndex = 0x3f;
            this.rbFloor10.TabStop = true;
            this.rbFloor10.UseVisualStyleBackColor = true;
            this.rbFloor10.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFloor11.AutoSize = true;
            this.rbFloor11.Location = new Point(0x14c, 3);
            this.rbFloor11.Name = "rbFloor11";
            this.rbFloor11.Size = new Size(14, 13);
            this.rbFloor11.TabIndex = 0x3e;
            this.rbFloor11.TabStop = true;
            this.rbFloor11.UseVisualStyleBackColor = true;
            this.rbFloor11.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFloor8.AutoSize = true;
            this.rbFloor8.Location = new Point(0x110, 3);
            this.rbFloor8.Name = "rbFloor8";
            this.rbFloor8.Size = new Size(14, 13);
            this.rbFloor8.TabIndex = 0x3d;
            this.rbFloor8.TabStop = true;
            this.rbFloor8.UseVisualStyleBackColor = true;
            this.rbFloor8.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFloor7.AutoSize = true;
            this.rbFloor7.Location = new Point(0xfc, 3);
            this.rbFloor7.Name = "rbFloor7";
            this.rbFloor7.Size = new Size(14, 13);
            this.rbFloor7.TabIndex = 60;
            this.rbFloor7.TabStop = true;
            this.rbFloor7.UseVisualStyleBackColor = true;
            this.rbFloor7.CheckedChanged += new EventHandler(this.checkedChanged);
            this.pnlFloorCSD.ClickThru = false;
            this.pnlFloorCSD.Location = new Point(0x174, 0);
            this.pnlFloorCSD.Name = "pnlFloorCSD";
            this.pnlFloorCSD.NoDrawBackground = false;
            this.pnlFloorCSD.PanelActive = true;
            this.pnlFloorCSD.SelfDrawBackground = false;
            this.pnlFloorCSD.Size = new Size(0x151, 0x16);
            this.pnlFloorCSD.StoredGraphics = null;
            this.pnlFloorCSD.TabIndex = 0x3b;
            this.rbFloor3.AutoSize = true;
            this.rbFloor3.Location = new Point(0xac, 3);
            this.rbFloor3.Name = "rbFloor3";
            this.rbFloor3.Size = new Size(14, 13);
            this.rbFloor3.TabIndex = 9;
            this.rbFloor3.TabStop = true;
            this.rbFloor3.UseVisualStyleBackColor = true;
            this.rbFloor3.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFloor4.AutoSize = true;
            this.rbFloor4.Location = new Point(0xc0, 3);
            this.rbFloor4.Name = "rbFloor4";
            this.rbFloor4.Size = new Size(14, 13);
            this.rbFloor4.TabIndex = 8;
            this.rbFloor4.TabStop = true;
            this.rbFloor4.UseVisualStyleBackColor = true;
            this.rbFloor4.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFloor5.AutoSize = true;
            this.rbFloor5.Location = new Point(0xd4, 3);
            this.rbFloor5.Name = "rbFloor5";
            this.rbFloor5.Size = new Size(14, 13);
            this.rbFloor5.TabIndex = 7;
            this.rbFloor5.TabStop = true;
            this.rbFloor5.UseVisualStyleBackColor = true;
            this.rbFloor5.CheckedChanged += new EventHandler(this.checkedChanged);
            this.pnlBody.BackColor = Color.Transparent;
            this.pnlBody.Controls.Add(this.pnlBodyCSD);
            this.pnlBody.Controls.Add(this.rbBody1);
            this.pnlBody.Controls.Add(this.label4);
            this.pnlBody.Location = new Point(0xf8, 0xee);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new Size(0x2c5, 0x16);
            this.pnlBody.TabIndex = 0x39;
            this.pnlBodyCSD.ClickThru = false;
            this.pnlBodyCSD.Location = new Point(0x174, 0);
            this.pnlBodyCSD.Name = "pnlBodyCSD";
            this.pnlBodyCSD.NoDrawBackground = false;
            this.pnlBodyCSD.PanelActive = true;
            this.pnlBodyCSD.SelfDrawBackground = false;
            this.pnlBodyCSD.Size = new Size(0x151, 0x16);
            this.pnlBodyCSD.StoredGraphics = null;
            this.pnlBodyCSD.TabIndex = 0x38;
            this.pnlLegs.BackColor = Color.Transparent;
            this.pnlLegs.Controls.Add(this.rbLegs7);
            this.pnlLegs.Controls.Add(this.rbLegs6);
            this.pnlLegs.Controls.Add(this.pnlLegsCSD);
            this.pnlLegs.Controls.Add(this.rbLegs5);
            this.pnlLegs.Controls.Add(this.rbLegs4);
            this.pnlLegs.Controls.Add(this.rbLegs1);
            this.pnlLegs.Controls.Add(this.label3);
            this.pnlLegs.Controls.Add(this.rbLegs2);
            this.pnlLegs.Controls.Add(this.rbLegs3);
            this.pnlLegs.Location = new Point(0xf8, 0x19d);
            this.pnlLegs.Name = "pnlLegs";
            this.pnlLegs.Size = new Size(0x2c5, 0x16);
            this.pnlLegs.TabIndex = 0x39;
            this.rbLegs7.AutoSize = true;
            this.rbLegs7.Location = new Point(0xfc, 3);
            this.rbLegs7.Name = "rbLegs7";
            this.rbLegs7.Size = new Size(14, 13);
            this.rbLegs7.TabIndex = 0x3b;
            this.rbLegs7.TabStop = true;
            this.rbLegs7.UseVisualStyleBackColor = true;
            this.rbLegs7.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbLegs6.AutoSize = true;
            this.rbLegs6.Location = new Point(0xe8, 3);
            this.rbLegs6.Name = "rbLegs6";
            this.rbLegs6.Size = new Size(14, 13);
            this.rbLegs6.TabIndex = 0x3a;
            this.rbLegs6.TabStop = true;
            this.rbLegs6.UseVisualStyleBackColor = true;
            this.rbLegs6.CheckedChanged += new EventHandler(this.checkedChanged);
            this.pnlLegsCSD.ClickThru = false;
            this.pnlLegsCSD.Location = new Point(0x174, 0);
            this.pnlLegsCSD.Name = "pnlLegsCSD";
            this.pnlLegsCSD.NoDrawBackground = false;
            this.pnlLegsCSD.PanelActive = true;
            this.pnlLegsCSD.SelfDrawBackground = false;
            this.pnlLegsCSD.Size = new Size(0x151, 0x16);
            this.pnlLegsCSD.StoredGraphics = null;
            this.pnlLegsCSD.TabIndex = 0x39;
            this.rbLegs5.AutoSize = true;
            this.rbLegs5.Location = new Point(0xd4, 3);
            this.rbLegs5.Name = "rbLegs5";
            this.rbLegs5.Size = new Size(14, 13);
            this.rbLegs5.TabIndex = 12;
            this.rbLegs5.TabStop = true;
            this.rbLegs5.UseVisualStyleBackColor = true;
            this.rbLegs5.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbLegs4.AutoSize = true;
            this.rbLegs4.Location = new Point(0xc0, 3);
            this.rbLegs4.Name = "rbLegs4";
            this.rbLegs4.Size = new Size(14, 13);
            this.rbLegs4.TabIndex = 11;
            this.rbLegs4.TabStop = true;
            this.rbLegs4.UseVisualStyleBackColor = true;
            this.rbLegs4.CheckedChanged += new EventHandler(this.checkedChanged);
            this.pnlFeet.BackColor = Color.Transparent;
            this.pnlFeet.Controls.Add(this.rbFeet6);
            this.pnlFeet.Controls.Add(this.rbFeet5);
            this.pnlFeet.Controls.Add(this.pnlFeetCSD);
            this.pnlFeet.Controls.Add(this.rbFeet4);
            this.pnlFeet.Controls.Add(this.rbFeetOff);
            this.pnlFeet.Controls.Add(this.rbFeet1);
            this.pnlFeet.Controls.Add(this.label5);
            this.pnlFeet.Controls.Add(this.rbFeet2);
            this.pnlFeet.Controls.Add(this.rbFeet3);
            this.pnlFeet.Location = new Point(0xf8, 0x1c0);
            this.pnlFeet.Name = "pnlFeet";
            this.pnlFeet.Size = new Size(0x2c5, 0x16);
            this.pnlFeet.TabIndex = 0x39;
            this.rbFeet6.AutoSize = true;
            this.rbFeet6.Location = new Point(0xe8, 3);
            this.rbFeet6.Name = "rbFeet6";
            this.rbFeet6.Size = new Size(14, 13);
            this.rbFeet6.TabIndex = 60;
            this.rbFeet6.TabStop = true;
            this.rbFeet6.UseVisualStyleBackColor = true;
            this.rbFeet6.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFeet5.AutoSize = true;
            this.rbFeet5.Location = new Point(0xd4, 3);
            this.rbFeet5.Name = "rbFeet5";
            this.rbFeet5.Size = new Size(14, 13);
            this.rbFeet5.TabIndex = 0x3b;
            this.rbFeet5.TabStop = true;
            this.rbFeet5.UseVisualStyleBackColor = true;
            this.rbFeet5.CheckedChanged += new EventHandler(this.checkedChanged);
            this.pnlFeetCSD.ClickThru = false;
            this.pnlFeetCSD.Location = new Point(0x174, 0);
            this.pnlFeetCSD.Name = "pnlFeetCSD";
            this.pnlFeetCSD.NoDrawBackground = false;
            this.pnlFeetCSD.PanelActive = true;
            this.pnlFeetCSD.SelfDrawBackground = false;
            this.pnlFeetCSD.Size = new Size(0x151, 0x16);
            this.pnlFeetCSD.StoredGraphics = null;
            this.pnlFeetCSD.TabIndex = 0x3a;
            this.rbFeet4.AutoSize = true;
            this.rbFeet4.Location = new Point(0xc0, 3);
            this.rbFeet4.Name = "rbFeet4";
            this.rbFeet4.Size = new Size(14, 13);
            this.rbFeet4.TabIndex = 0x31;
            this.rbFeet4.TabStop = true;
            this.rbFeet4.UseVisualStyleBackColor = true;
            this.rbFeet4.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFeetOff.AutoSize = true;
            this.rbFeetOff.Location = new Point(0x5f, 3);
            this.rbFeetOff.Name = "rbFeetOff";
            this.rbFeetOff.Size = new Size(0x20, 0x11);
            this.rbFeetOff.TabIndex = 0x30;
            this.rbFeetOff.TabStop = true;
            this.rbFeetOff.Text = "X";
            this.rbFeetOff.UseVisualStyleBackColor = true;
            this.pnlTorso.BackColor = Color.Transparent;
            this.pnlTorso.Controls.Add(this.pnlTorsoCSD);
            this.pnlTorso.Controls.Add(this.rbTorso1);
            this.pnlTorso.Controls.Add(this.label6);
            this.pnlTorso.Controls.Add(this.rbTorso2);
            this.pnlTorso.Controls.Add(this.rbTorso3);
            this.pnlTorso.Controls.Add(this.rbTorso4);
            this.pnlTorso.Location = new Point(0xf8, 0x111);
            this.pnlTorso.Name = "pnlTorso";
            this.pnlTorso.Size = new Size(0x2c5, 0x16);
            this.pnlTorso.TabIndex = 0x39;
            this.pnlTorsoCSD.ClickThru = false;
            this.pnlTorsoCSD.Location = new Point(0x174, 0);
            this.pnlTorsoCSD.Name = "pnlTorsoCSD";
            this.pnlTorsoCSD.NoDrawBackground = false;
            this.pnlTorsoCSD.PanelActive = true;
            this.pnlTorsoCSD.SelfDrawBackground = false;
            this.pnlTorsoCSD.Size = new Size(0x151, 0x16);
            this.pnlTorsoCSD.StoredGraphics = null;
            this.pnlTorsoCSD.TabIndex = 0x39;
            this.pnlTabard.BackColor = Color.Transparent;
            this.pnlTabard.Controls.Add(this.rbTabard8);
            this.pnlTabard.Controls.Add(this.rbTabard7);
            this.pnlTabard.Controls.Add(this.rbTabard6);
            this.pnlTabard.Controls.Add(this.rbTabard5);
            this.pnlTabard.Controls.Add(this.pnlTabardCSD);
            this.pnlTabard.Controls.Add(this.rbTabard4);
            this.pnlTabard.Controls.Add(this.rbTabard3);
            this.pnlTabard.Controls.Add(this.rbTabardOff);
            this.pnlTabard.Controls.Add(this.rbTabard2);
            this.pnlTabard.Controls.Add(this.rbTabard1);
            this.pnlTabard.Controls.Add(this.label7);
            this.pnlTabard.Location = new Point(0xf8, 0xcb);
            this.pnlTabard.Name = "pnlTabard";
            this.pnlTabard.Size = new Size(0x2c5, 0x16);
            this.pnlTabard.TabIndex = 0x39;
            this.rbTabard8.AutoSize = true;
            this.rbTabard8.Location = new Point(0x110, 3);
            this.rbTabard8.Name = "rbTabard8";
            this.rbTabard8.Size = new Size(14, 13);
            this.rbTabard8.TabIndex = 0x3b;
            this.rbTabard8.TabStop = true;
            this.rbTabard8.UseVisualStyleBackColor = true;
            this.rbTabard8.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbTabard7.AutoSize = true;
            this.rbTabard7.Location = new Point(0xfc, 3);
            this.rbTabard7.Name = "rbTabard7";
            this.rbTabard7.Size = new Size(14, 13);
            this.rbTabard7.TabIndex = 0x3a;
            this.rbTabard7.TabStop = true;
            this.rbTabard7.UseVisualStyleBackColor = true;
            this.rbTabard7.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbTabard6.AutoSize = true;
            this.rbTabard6.Location = new Point(0xe8, 3);
            this.rbTabard6.Name = "rbTabard6";
            this.rbTabard6.Size = new Size(14, 13);
            this.rbTabard6.TabIndex = 0x39;
            this.rbTabard6.TabStop = true;
            this.rbTabard6.UseVisualStyleBackColor = true;
            this.rbTabard6.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbTabard5.AutoSize = true;
            this.rbTabard5.Location = new Point(0xd4, 3);
            this.rbTabard5.Name = "rbTabard5";
            this.rbTabard5.Size = new Size(14, 13);
            this.rbTabard5.TabIndex = 0x38;
            this.rbTabard5.TabStop = true;
            this.rbTabard5.UseVisualStyleBackColor = true;
            this.rbTabard5.CheckedChanged += new EventHandler(this.checkedChanged);
            this.pnlTabardCSD.ClickThru = false;
            this.pnlTabardCSD.Location = new Point(0x174, 0);
            this.pnlTabardCSD.Name = "pnlTabardCSD";
            this.pnlTabardCSD.NoDrawBackground = false;
            this.pnlTabardCSD.PanelActive = true;
            this.pnlTabardCSD.SelfDrawBackground = false;
            this.pnlTabardCSD.Size = new Size(0x151, 0x16);
            this.pnlTabardCSD.StoredGraphics = null;
            this.pnlTabardCSD.TabIndex = 0x37;
            this.rbTabard4.AutoSize = true;
            this.rbTabard4.Location = new Point(0xc0, 3);
            this.rbTabard4.Name = "rbTabard4";
            this.rbTabard4.Size = new Size(14, 13);
            this.rbTabard4.TabIndex = 0x35;
            this.rbTabard4.TabStop = true;
            this.rbTabard4.UseVisualStyleBackColor = true;
            this.rbTabard4.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbTabard3.AutoSize = true;
            this.rbTabard3.Location = new Point(0xac, 3);
            this.rbTabard3.Name = "rbTabard3";
            this.rbTabard3.Size = new Size(14, 13);
            this.rbTabard3.TabIndex = 0x34;
            this.rbTabard3.TabStop = true;
            this.rbTabard3.UseVisualStyleBackColor = true;
            this.rbTabard3.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbTabardOff.AutoSize = true;
            this.rbTabardOff.Location = new Point(0x5f, 3);
            this.rbTabardOff.Name = "rbTabardOff";
            this.rbTabardOff.Size = new Size(0x20, 0x11);
            this.rbTabardOff.TabIndex = 0x33;
            this.rbTabardOff.TabStop = true;
            this.rbTabardOff.Text = "X";
            this.rbTabardOff.UseVisualStyleBackColor = true;
            this.pnlArms.BackColor = Color.Transparent;
            this.pnlArms.Controls.Add(this.pnlArmsCSD);
            this.pnlArms.Controls.Add(this.rbArms4);
            this.pnlArms.Controls.Add(this.rbArmsOff);
            this.pnlArms.Controls.Add(this.rbArms1);
            this.pnlArms.Controls.Add(this.label9);
            this.pnlArms.Controls.Add(this.rbArms2);
            this.pnlArms.Controls.Add(this.rbArms3);
            this.pnlArms.Location = new Point(0xf8, 0x134);
            this.pnlArms.Name = "pnlArms";
            this.pnlArms.Size = new Size(0x2c5, 0x16);
            this.pnlArms.TabIndex = 0x39;
            this.pnlArmsCSD.ClickThru = false;
            this.pnlArmsCSD.Location = new Point(0x174, 0);
            this.pnlArmsCSD.Name = "pnlArmsCSD";
            this.pnlArmsCSD.NoDrawBackground = false;
            this.pnlArmsCSD.PanelActive = true;
            this.pnlArmsCSD.SelfDrawBackground = false;
            this.pnlArmsCSD.Size = new Size(0x151, 0x16);
            this.pnlArmsCSD.StoredGraphics = null;
            this.pnlArmsCSD.TabIndex = 0x3a;
            this.rbArms4.AutoSize = true;
            this.rbArms4.Location = new Point(0xc0, 3);
            this.rbArms4.Name = "rbArms4";
            this.rbArms4.Size = new Size(14, 13);
            this.rbArms4.TabIndex = 0x31;
            this.rbArms4.TabStop = true;
            this.rbArms4.UseVisualStyleBackColor = true;
            this.rbArms4.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbArmsOff.AutoSize = true;
            this.rbArmsOff.Location = new Point(0x5f, 3);
            this.rbArmsOff.Name = "rbArmsOff";
            this.rbArmsOff.Size = new Size(0x20, 0x11);
            this.rbArmsOff.TabIndex = 0x30;
            this.rbArmsOff.TabStop = true;
            this.rbArmsOff.Text = "X";
            this.rbArmsOff.UseVisualStyleBackColor = true;
            this.pnlHands.BackColor = Color.Transparent;
            this.pnlHands.Controls.Add(this.pnlHandsCSD);
            this.pnlHands.Controls.Add(this.rbHands4);
            this.pnlHands.Controls.Add(this.rbHandsOff);
            this.pnlHands.Controls.Add(this.rbHands1);
            this.pnlHands.Controls.Add(this.label8);
            this.pnlHands.Controls.Add(this.rbHands2);
            this.pnlHands.Controls.Add(this.rbHands3);
            this.pnlHands.Location = new Point(0xf8, 0x157);
            this.pnlHands.Name = "pnlHands";
            this.pnlHands.Size = new Size(0x2c5, 0x16);
            this.pnlHands.TabIndex = 0x39;
            this.pnlHandsCSD.ClickThru = false;
            this.pnlHandsCSD.Location = new Point(0x174, 0);
            this.pnlHandsCSD.Name = "pnlHandsCSD";
            this.pnlHandsCSD.NoDrawBackground = false;
            this.pnlHandsCSD.PanelActive = true;
            this.pnlHandsCSD.SelfDrawBackground = false;
            this.pnlHandsCSD.Size = new Size(0x151, 0x16);
            this.pnlHandsCSD.StoredGraphics = null;
            this.pnlHandsCSD.TabIndex = 0x35;
            this.rbHands4.AutoSize = true;
            this.rbHands4.Location = new Point(0xc0, 3);
            this.rbHands4.Name = "rbHands4";
            this.rbHands4.Size = new Size(14, 13);
            this.rbHands4.TabIndex = 0x30;
            this.rbHands4.TabStop = true;
            this.rbHands4.UseVisualStyleBackColor = true;
            this.rbHands4.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHandsOff.AutoSize = true;
            this.rbHandsOff.Location = new Point(0x5f, 3);
            this.rbHandsOff.Name = "rbHandsOff";
            this.rbHandsOff.Size = new Size(0x20, 0x11);
            this.rbHandsOff.TabIndex = 0x2f;
            this.rbHandsOff.TabStop = true;
            this.rbHandsOff.Text = "X";
            this.rbHandsOff.UseVisualStyleBackColor = true;
            this.pnlShoulders.BackColor = Color.Transparent;
            this.pnlShoulders.Controls.Add(this.pnlShouldersCSD);
            this.pnlShoulders.Controls.Add(this.rbShoulderOff);
            this.pnlShoulders.Controls.Add(this.label10);
            this.pnlShoulders.Controls.Add(this.rbShoulders1);
            this.pnlShoulders.Controls.Add(this.rbShoulders2);
            this.pnlShoulders.Controls.Add(this.rbShoulders3);
            this.pnlShoulders.Controls.Add(this.rbShoulders4);
            this.pnlShoulders.Location = new Point(0xf8, 0xa8);
            this.pnlShoulders.Name = "pnlShoulders";
            this.pnlShoulders.Size = new Size(0x2c5, 0x16);
            this.pnlShoulders.TabIndex = 0x39;
            this.pnlShouldersCSD.ClickThru = false;
            this.pnlShouldersCSD.Location = new Point(0x174, 0);
            this.pnlShouldersCSD.Name = "pnlShouldersCSD";
            this.pnlShouldersCSD.NoDrawBackground = false;
            this.pnlShouldersCSD.PanelActive = true;
            this.pnlShouldersCSD.SelfDrawBackground = false;
            this.pnlShouldersCSD.Size = new Size(0x151, 0x16);
            this.pnlShouldersCSD.StoredGraphics = null;
            this.pnlShouldersCSD.TabIndex = 0x36;
            this.rbShoulderOff.AutoSize = true;
            this.rbShoulderOff.Location = new Point(0x5f, 3);
            this.rbShoulderOff.Name = "rbShoulderOff";
            this.rbShoulderOff.Size = new Size(0x20, 0x11);
            this.rbShoulderOff.TabIndex = 0x2f;
            this.rbShoulderOff.TabStop = true;
            this.rbShoulderOff.Text = "X";
            this.rbShoulderOff.UseVisualStyleBackColor = true;
            this.pnlFace.BackColor = Color.Transparent;
            this.pnlFace.Controls.Add(this.rbFace7);
            this.pnlFace.Controls.Add(this.rbFace6);
            this.pnlFace.Controls.Add(this.rbFace5);
            this.pnlFace.Controls.Add(this.pnlFaceCSD);
            this.pnlFace.Controls.Add(this.rbFace4);
            this.pnlFace.Controls.Add(this.rbFace3);
            this.pnlFace.Controls.Add(this.rbFace2);
            this.pnlFace.Controls.Add(this.rbFace1);
            this.pnlFace.Controls.Add(this.label11);
            this.pnlFace.Location = new Point(0xf8, 0x85);
            this.pnlFace.Name = "pnlFace";
            this.pnlFace.Size = new Size(0x2c5, 0x16);
            this.pnlFace.TabIndex = 0x39;
            this.rbFace7.AutoSize = true;
            this.rbFace7.Location = new Point(0xfc, 3);
            this.rbFace7.Name = "rbFace7";
            this.rbFace7.Size = new Size(14, 13);
            this.rbFace7.TabIndex = 0x38;
            this.rbFace7.TabStop = true;
            this.rbFace7.UseVisualStyleBackColor = true;
            this.rbFace7.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFace6.AutoSize = true;
            this.rbFace6.Location = new Point(0xe8, 3);
            this.rbFace6.Name = "rbFace6";
            this.rbFace6.Size = new Size(14, 13);
            this.rbFace6.TabIndex = 0x37;
            this.rbFace6.TabStop = true;
            this.rbFace6.UseVisualStyleBackColor = true;
            this.rbFace6.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFace5.AutoSize = true;
            this.rbFace5.Location = new Point(0xd4, 3);
            this.rbFace5.Name = "rbFace5";
            this.rbFace5.Size = new Size(14, 13);
            this.rbFace5.TabIndex = 0x36;
            this.rbFace5.TabStop = true;
            this.rbFace5.UseVisualStyleBackColor = true;
            this.rbFace5.CheckedChanged += new EventHandler(this.checkedChanged);
            this.pnlFaceCSD.ClickThru = false;
            this.pnlFaceCSD.Location = new Point(0x174, 0);
            this.pnlFaceCSD.Name = "pnlFaceCSD";
            this.pnlFaceCSD.NoDrawBackground = false;
            this.pnlFaceCSD.PanelActive = true;
            this.pnlFaceCSD.SelfDrawBackground = false;
            this.pnlFaceCSD.Size = new Size(0x151, 0x16);
            this.pnlFaceCSD.StoredGraphics = null;
            this.pnlFaceCSD.TabIndex = 0x35;
            this.rbFace4.AutoSize = true;
            this.rbFace4.Location = new Point(0xc0, 3);
            this.rbFace4.Name = "rbFace4";
            this.rbFace4.Size = new Size(14, 13);
            this.rbFace4.TabIndex = 0x2a;
            this.rbFace4.TabStop = true;
            this.rbFace4.UseVisualStyleBackColor = true;
            this.rbFace4.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbFace3.AutoSize = true;
            this.rbFace3.Location = new Point(0xac, 3);
            this.rbFace3.Name = "rbFace3";
            this.rbFace3.Size = new Size(14, 13);
            this.rbFace3.TabIndex = 0x29;
            this.rbFace3.TabStop = true;
            this.rbFace3.UseVisualStyleBackColor = true;
            this.rbFace3.CheckedChanged += new EventHandler(this.checkedChanged);
            this.pnlHair.BackColor = Color.Transparent;
            this.pnlHair.Controls.Add(this.rbHair5);
            this.pnlHair.Controls.Add(this.rbHair6);
            this.pnlHair.Controls.Add(this.pnlHairCSD);
            this.pnlHair.Controls.Add(this.rbHairOff);
            this.pnlHair.Controls.Add(this.rbHair1);
            this.pnlHair.Controls.Add(this.label12);
            this.pnlHair.Controls.Add(this.rbHair2);
            this.pnlHair.Controls.Add(this.rbHair3);
            this.pnlHair.Controls.Add(this.rbHair4);
            this.pnlHair.Location = new Point(0xf8, 0x62);
            this.pnlHair.Name = "pnlHair";
            this.pnlHair.Size = new Size(0x2c5, 0x16);
            this.pnlHair.TabIndex = 0x39;
            this.rbHair5.AutoSize = true;
            this.rbHair5.Location = new Point(0xd4, 3);
            this.rbHair5.Name = "rbHair5";
            this.rbHair5.Size = new Size(14, 13);
            this.rbHair5.TabIndex = 0x36;
            this.rbHair5.TabStop = true;
            this.rbHair5.UseVisualStyleBackColor = true;
            this.rbHair5.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHair6.AutoSize = true;
            this.rbHair6.Location = new Point(0xe8, 3);
            this.rbHair6.Name = "rbHair6";
            this.rbHair6.Size = new Size(14, 13);
            this.rbHair6.TabIndex = 0x37;
            this.rbHair6.TabStop = true;
            this.rbHair6.UseVisualStyleBackColor = true;
            this.rbHair6.CheckedChanged += new EventHandler(this.checkedChanged);
            this.pnlHairCSD.ClickThru = false;
            this.pnlHairCSD.Location = new Point(0x174, 0);
            this.pnlHairCSD.Name = "pnlHairCSD";
            this.pnlHairCSD.NoDrawBackground = false;
            this.pnlHairCSD.PanelActive = true;
            this.pnlHairCSD.SelfDrawBackground = false;
            this.pnlHairCSD.Size = new Size(0x151, 0x16);
            this.pnlHairCSD.StoredGraphics = null;
            this.pnlHairCSD.TabIndex = 0x35;
            this.rbHairOff.AutoSize = true;
            this.rbHairOff.Location = new Point(0x5f, 3);
            this.rbHairOff.Name = "rbHairOff";
            this.rbHairOff.Size = new Size(0x20, 0x11);
            this.rbHairOff.TabIndex = 0x33;
            this.rbHairOff.TabStop = true;
            this.rbHairOff.Text = "X";
            this.rbHairOff.UseVisualStyleBackColor = true;
            this.pnlHead.BackColor = Color.Transparent;
            this.pnlHead.Controls.Add(this.rbHead12);
            this.pnlHead.Controls.Add(this.rbHead9);
            this.pnlHead.Controls.Add(this.rbHead10);
            this.pnlHead.Controls.Add(this.rbHead11);
            this.pnlHead.Controls.Add(this.rbHead8);
            this.pnlHead.Controls.Add(this.rbHead5);
            this.pnlHead.Controls.Add(this.rbHead6);
            this.pnlHead.Controls.Add(this.rbHead7);
            this.pnlHead.Controls.Add(this.pnlHeadCSD);
            this.pnlHead.Controls.Add(this.rbHead4);
            this.pnlHead.Controls.Add(this.rbHeadOff);
            this.pnlHead.Controls.Add(this.label13);
            this.pnlHead.Controls.Add(this.rbHead1);
            this.pnlHead.Controls.Add(this.rbHead2);
            this.pnlHead.Controls.Add(this.rbHead3);
            this.pnlHead.Location = new Point(0xf8, 0x3f);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new Size(0x2c5, 0x16);
            this.pnlHead.TabIndex = 0x39;
            this.rbHead12.AutoSize = true;
            this.rbHead12.Location = new Point(0x160, 3);
            this.rbHead12.Name = "rbHead12";
            this.rbHead12.Size = new Size(14, 13);
            this.rbHead12.TabIndex = 60;
            this.rbHead12.TabStop = true;
            this.rbHead12.UseVisualStyleBackColor = true;
            this.rbHead12.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHead9.AutoSize = true;
            this.rbHead9.Location = new Point(0x124, 3);
            this.rbHead9.Name = "rbHead9";
            this.rbHead9.Size = new Size(14, 13);
            this.rbHead9.TabIndex = 0x39;
            this.rbHead9.TabStop = true;
            this.rbHead9.UseVisualStyleBackColor = true;
            this.rbHead9.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHead10.AutoSize = true;
            this.rbHead10.Location = new Point(0x138, 3);
            this.rbHead10.Name = "rbHead10";
            this.rbHead10.Size = new Size(14, 13);
            this.rbHead10.TabIndex = 0x3a;
            this.rbHead10.TabStop = true;
            this.rbHead10.UseVisualStyleBackColor = true;
            this.rbHead10.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHead11.AutoSize = true;
            this.rbHead11.Location = new Point(0x14c, 3);
            this.rbHead11.Name = "rbHead11";
            this.rbHead11.Size = new Size(14, 13);
            this.rbHead11.TabIndex = 0x3b;
            this.rbHead11.TabStop = true;
            this.rbHead11.UseVisualStyleBackColor = true;
            this.rbHead11.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHead8.AutoSize = true;
            this.rbHead8.Location = new Point(0x110, 3);
            this.rbHead8.Name = "rbHead8";
            this.rbHead8.Size = new Size(14, 13);
            this.rbHead8.TabIndex = 0x38;
            this.rbHead8.TabStop = true;
            this.rbHead8.UseVisualStyleBackColor = true;
            this.rbHead8.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHead5.AutoSize = true;
            this.rbHead5.Location = new Point(0xd4, 3);
            this.rbHead5.Name = "rbHead5";
            this.rbHead5.Size = new Size(14, 13);
            this.rbHead5.TabIndex = 0x35;
            this.rbHead5.TabStop = true;
            this.rbHead5.UseVisualStyleBackColor = true;
            this.rbHead5.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHead6.AutoSize = true;
            this.rbHead6.Location = new Point(0xe8, 3);
            this.rbHead6.Name = "rbHead6";
            this.rbHead6.Size = new Size(14, 13);
            this.rbHead6.TabIndex = 0x36;
            this.rbHead6.TabStop = true;
            this.rbHead6.UseVisualStyleBackColor = true;
            this.rbHead6.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbHead7.AutoSize = true;
            this.rbHead7.Location = new Point(0xfc, 3);
            this.rbHead7.Name = "rbHead7";
            this.rbHead7.Size = new Size(14, 13);
            this.rbHead7.TabIndex = 0x37;
            this.rbHead7.TabStop = true;
            this.rbHead7.UseVisualStyleBackColor = true;
            this.rbHead7.CheckedChanged += new EventHandler(this.checkedChanged);
            this.pnlHeadCSD.ClickThru = false;
            this.pnlHeadCSD.Location = new Point(0x174, 0);
            this.pnlHeadCSD.Name = "pnlHeadCSD";
            this.pnlHeadCSD.NoDrawBackground = false;
            this.pnlHeadCSD.PanelActive = true;
            this.pnlHeadCSD.SelfDrawBackground = false;
            this.pnlHeadCSD.Size = new Size(0x151, 0x16);
            this.pnlHeadCSD.StoredGraphics = null;
            this.pnlHeadCSD.TabIndex = 0x34;
            this.rbHead4.AutoSize = true;
            this.rbHead4.Location = new Point(0xc0, 3);
            this.rbHead4.Name = "rbHead4";
            this.rbHead4.Size = new Size(14, 13);
            this.rbHead4.TabIndex = 0x33;
            this.rbHead4.TabStop = true;
            this.rbHead4.UseVisualStyleBackColor = true;
            this.rbHead4.CheckedChanged += new EventHandler(this.checkedChanged);
            this.pnlWeapon.BackColor = Color.Transparent;
            this.pnlWeapon.Controls.Add(this.rbWeapon5);
            this.pnlWeapon.Controls.Add(this.rbWeapon6);
            this.pnlWeapon.Controls.Add(this.pnlWeaponCSD);
            this.pnlWeapon.Controls.Add(this.rbWeapon4);
            this.pnlWeapon.Controls.Add(this.rbWeaponOff);
            this.pnlWeapon.Controls.Add(this.label14);
            this.pnlWeapon.Controls.Add(this.rbWeapon1);
            this.pnlWeapon.Controls.Add(this.rbWeapon2);
            this.pnlWeapon.Controls.Add(this.rbWeapon3);
            this.pnlWeapon.Location = new Point(0xf8, 0x17a);
            this.pnlWeapon.Name = "pnlWeapon";
            this.pnlWeapon.Size = new Size(0x2c5, 0x16);
            this.pnlWeapon.TabIndex = 0x39;
            this.rbWeapon5.AutoSize = true;
            this.rbWeapon5.Location = new Point(0xd4, 3);
            this.rbWeapon5.Name = "rbWeapon5";
            this.rbWeapon5.Size = new Size(14, 13);
            this.rbWeapon5.TabIndex = 0x3a;
            this.rbWeapon5.TabStop = true;
            this.rbWeapon5.UseVisualStyleBackColor = true;
            this.rbWeapon5.CheckedChanged += new EventHandler(this.checkedChanged);
            this.rbWeapon6.AutoSize = true;
            this.rbWeapon6.Location = new Point(0xe8, 3);
            this.rbWeapon6.Name = "rbWeapon6";
            this.rbWeapon6.Size = new Size(14, 13);
            this.rbWeapon6.TabIndex = 0x3b;
            this.rbWeapon6.TabStop = true;
            this.rbWeapon6.UseVisualStyleBackColor = true;
            this.rbWeapon6.CheckedChanged += new EventHandler(this.checkedChanged);
            this.pnlWeaponCSD.ClickThru = false;
            this.pnlWeaponCSD.Location = new Point(0x174, 0);
            this.pnlWeaponCSD.Name = "pnlWeaponCSD";
            this.pnlWeaponCSD.NoDrawBackground = false;
            this.pnlWeaponCSD.PanelActive = true;
            this.pnlWeaponCSD.SelfDrawBackground = false;
            this.pnlWeaponCSD.Size = new Size(0x151, 0x16);
            this.pnlWeaponCSD.StoredGraphics = null;
            this.pnlWeaponCSD.TabIndex = 0x38;
            this.rbWeapon4.AutoSize = true;
            this.rbWeapon4.Location = new Point(0xc0, 3);
            this.rbWeapon4.Name = "rbWeapon4";
            this.rbWeapon4.Size = new Size(14, 13);
            this.rbWeapon4.TabIndex = 0x2e;
            this.rbWeapon4.TabStop = true;
            this.rbWeapon4.UseVisualStyleBackColor = true;
            this.btnRandom.BackgroundImageLayout = ImageLayout.None;
            this.btnRandom.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnRandom.BorderDrawing = true;
            this.btnRandom.FocusRectangleEnabled = false;
            this.btnRandom.ForeColor = SystemColors.ControlText;
            this.btnRandom.Image = null;
            this.btnRandom.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnRandom.ImageBorderEnabled = false;
            this.btnRandom.ImageDropShadow = false;
            this.btnRandom.ImageFocused = null;
            this.btnRandom.ImageInactive = null;
            this.btnRandom.ImageMouseOver = null;
            this.btnRandom.ImageNormal = null;
            this.btnRandom.ImagePressed = null;
            this.btnRandom.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnRandom.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnRandom.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnRandom.Location = new Point(0xf6, 0x205);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.OffsetPressedContent = true;
            this.btnRandom.Padding2 = 5;
            this.btnRandom.Size = new Size(0x5b, 0x2e);
            this.btnRandom.StretchImage = false;
            this.btnRandom.TabIndex = 0x3d;
            this.btnRandom.TextDropShadow = false;
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new EventHandler(this.btnRandom_Click);
            this.btnLastSaved.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnLastSaved.BorderDrawing = true;
            this.btnLastSaved.FocusRectangleEnabled = false;
            this.btnLastSaved.Image = null;
            this.btnLastSaved.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnLastSaved.ImageBorderEnabled = true;
            this.btnLastSaved.ImageDropShadow = true;
            this.btnLastSaved.ImageFocused = null;
            this.btnLastSaved.ImageInactive = null;
            this.btnLastSaved.ImageMouseOver = null;
            this.btnLastSaved.ImageNormal = null;
            this.btnLastSaved.ImagePressed = null;
            this.btnLastSaved.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnLastSaved.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnLastSaved.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnLastSaved.Location = new Point(0x227, 0x205);
            this.btnLastSaved.Name = "btnLastSaved";
            this.btnLastSaved.OffsetPressedContent = true;
            this.btnLastSaved.Padding2 = 5;
            this.btnLastSaved.Size = new Size(200, 0x2e);
            this.btnLastSaved.StretchImage = false;
            this.btnLastSaved.TabIndex = 60;
            this.btnLastSaved.Text = "Reset To Last Saved";
            this.btnLastSaved.TextDropShadow = false;
            this.btnLastSaved.UseVisualStyleBackColor = true;
            this.btnLastSaved.Click += new EventHandler(this.btnLastSaved_Click);
            this.btnDefault.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnDefault.BorderDrawing = true;
            this.btnDefault.FocusRectangleEnabled = false;
            this.btnDefault.Image = null;
            this.btnDefault.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnDefault.ImageBorderEnabled = true;
            this.btnDefault.ImageDropShadow = true;
            this.btnDefault.ImageFocused = null;
            this.btnDefault.ImageInactive = null;
            this.btnDefault.ImageMouseOver = null;
            this.btnDefault.ImageNormal = null;
            this.btnDefault.ImagePressed = null;
            this.btnDefault.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnDefault.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnDefault.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnDefault.Location = new Point(0x157, 0x205);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.OffsetPressedContent = true;
            this.btnDefault.Padding2 = 5;
            this.btnDefault.Size = new Size(200, 0x2e);
            this.btnDefault.StretchImage = false;
            this.btnDefault.TabIndex = 0x3b;
            this.btnDefault.Text = "Reset To Default";
            this.btnDefault.TextDropShadow = false;
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new EventHandler(this.btnDefault_Click);
            this.btnUploadAvatar.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnUploadAvatar.BorderDrawing = true;
            this.btnUploadAvatar.FocusRectangleEnabled = false;
            this.btnUploadAvatar.Image = null;
            this.btnUploadAvatar.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnUploadAvatar.ImageBorderEnabled = true;
            this.btnUploadAvatar.ImageDropShadow = true;
            this.btnUploadAvatar.ImageFocused = null;
            this.btnUploadAvatar.ImageInactive = null;
            this.btnUploadAvatar.ImageMouseOver = null;
            this.btnUploadAvatar.ImageNormal = null;
            this.btnUploadAvatar.ImagePressed = null;
            this.btnUploadAvatar.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnUploadAvatar.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnUploadAvatar.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnUploadAvatar.Location = new Point(0x2f5, 0x205);
            this.btnUploadAvatar.Name = "btnUploadAvatar";
            this.btnUploadAvatar.OffsetPressedContent = true;
            this.btnUploadAvatar.Padding2 = 5;
            this.btnUploadAvatar.Size = new Size(200, 0x2e);
            this.btnUploadAvatar.StretchImage = false;
            this.btnUploadAvatar.TabIndex = 0x3a;
            this.btnUploadAvatar.Text = "Upload Avatar";
            this.btnUploadAvatar.TextDropShadow = false;
            this.btnUploadAvatar.UseVisualStyleBackColor = true;
            this.btnUploadAvatar.Click += new EventHandler(this.btnUploadAvatar_Click);
            this.imgAvatar.BackColor = Color.Transparent;
            this.imgAvatar.Location = new Point(0x30, 0x1c);
            this.imgAvatar.Name = "imgAvatar";
            this.imgAvatar.Size = new Size(0x9a, 500);
            this.imgAvatar.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.BackgroundImageLayout = ImageLayout.Stretch;
            base.Controls.Add(this.btnRandom);
            base.Controls.Add(this.btnLastSaved);
            base.Controls.Add(this.btnDefault);
            base.Controls.Add(this.btnUploadAvatar);
            base.Controls.Add(this.pnlWeapon);
            base.Controls.Add(this.pnlHead);
            base.Controls.Add(this.pnlHair);
            base.Controls.Add(this.pnlFace);
            base.Controls.Add(this.pnlShoulders);
            base.Controls.Add(this.pnlHands);
            base.Controls.Add(this.pnlArms);
            base.Controls.Add(this.pnlTabard);
            base.Controls.Add(this.pnlTorso);
            base.Controls.Add(this.pnlFeet);
            base.Controls.Add(this.pnlLegs);
            base.Controls.Add(this.pnlBody);
            base.Controls.Add(this.pnlFloor);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.imgAvatar);
            this.MaximumSize = new Size(0x3e0, 0x236);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "AvatarEditorPanel";
            base.Size = new Size(0x3e0, 0x236);
            base.Load += new EventHandler(this.AvatarEditorPanel_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlFloor.ResumeLayout(false);
            this.pnlFloor.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlLegs.ResumeLayout(false);
            this.pnlLegs.PerformLayout();
            this.pnlFeet.ResumeLayout(false);
            this.pnlFeet.PerformLayout();
            this.pnlTorso.ResumeLayout(false);
            this.pnlTorso.PerformLayout();
            this.pnlTabard.ResumeLayout(false);
            this.pnlTabard.PerformLayout();
            this.pnlArms.ResumeLayout(false);
            this.pnlArms.PerformLayout();
            this.pnlHands.ResumeLayout(false);
            this.pnlHands.PerformLayout();
            this.pnlShoulders.ResumeLayout(false);
            this.pnlShoulders.PerformLayout();
            this.pnlFace.ResumeLayout(false);
            this.pnlFace.PerformLayout();
            this.pnlHair.ResumeLayout(false);
            this.pnlHair.PerformLayout();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.pnlWeapon.ResumeLayout(false);
            this.pnlWeapon.PerformLayout();
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

        private void panel15_Click(object sender, EventArgs e)
        {
        }

        public void removeColours(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                try
                {
                    Panel panel2 = (Panel) control;
                    if (panel2 != null)
                    {
                        panel2.SuspendLayout();
                        panel2.Visible = false;
                    }
                }
                catch (Exception)
                {
                }
            }
            bool flag = true;
            while (flag)
            {
                flag = false;
                foreach (Control control2 in panel.Controls)
                {
                    try
                    {
                        if (((Panel) control2) != null)
                        {
                            panel.Controls.Remove(control2);
                            flag = true;
                            continue;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public void resumeLayout(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                try
                {
                    Panel panel2 = (Panel) control;
                    if (panel2 != null)
                    {
                        panel2.ResumeLayout(false);
                    }
                }
                catch (Exception)
                {
                }
            }
            panel.ResumeLayout(false);
        }

        private void setRandomColour(CustomSelfDrawPanel colourPanel, Random rnd)
        {
            CustomSelfDrawPanel.CSDControl baseControl = colourPanel.baseControl;
            int num = rnd.Next(baseControl.Controls.Count);
            int num2 = 0;
            foreach (CustomSelfDrawPanel.CSDFill fill in baseControl.Controls)
            {
                fill.Border = num == num2;
                num2++;
            }
        }

        public void update()
        {
            if (this.forceUpdate)
            {
                AvatarData avatar = new AvatarData();
                if (this.rbMale.Checked)
                {
                    avatar.male = true;
                }
                if (this.rbFemale.Checked)
                {
                    avatar.male = false;
                }
                if (this.rbFloor1.Checked)
                {
                    avatar.floor = 0;
                }
                if (this.rbFloor2.Checked)
                {
                    avatar.floor = 1;
                }
                if (this.rbFloor3.Checked)
                {
                    avatar.floor = 2;
                }
                if (this.rbFloor4.Checked)
                {
                    avatar.floor = 3;
                }
                if (this.rbFloor5.Checked)
                {
                    avatar.floor = 4;
                }
                if (this.rbFloor6.Checked)
                {
                    avatar.floor = 5;
                }
                if (this.rbFloor7.Checked)
                {
                    avatar.floor = 6;
                }
                if (this.rbFloor8.Checked)
                {
                    avatar.floor = 7;
                }
                if (this.rbFloor9.Checked)
                {
                    avatar.floor = 8;
                }
                if (this.rbFloor10.Checked)
                {
                    avatar.floor = 9;
                }
                if (this.rbFloor11.Checked)
                {
                    avatar.floor = 10;
                }
                if (this.rbBody1.Checked)
                {
                    avatar.body = 0;
                }
                if (this.rbLegs1.Checked)
                {
                    avatar.legs = 0;
                }
                if (this.rbLegs2.Checked)
                {
                    avatar.legs = 1;
                }
                if (this.rbLegs3.Checked)
                {
                    avatar.legs = 2;
                }
                if (this.rbLegs4.Checked)
                {
                    avatar.legs = 3;
                }
                if (this.rbLegs5.Checked)
                {
                    avatar.legs = 4;
                }
                if (this.rbLegs6.Checked)
                {
                    avatar.legs = 5;
                }
                if (this.rbLegs7.Checked)
                {
                    avatar.legs = 6;
                }
                if (this.rbFeetOff.Checked)
                {
                    avatar.feet = -1;
                }
                if (this.rbFeet1.Checked)
                {
                    avatar.feet = 0;
                }
                if (this.rbFeet2.Checked)
                {
                    avatar.feet = 1;
                }
                if (this.rbFeet3.Checked)
                {
                    avatar.feet = 2;
                }
                if (this.rbFeet4.Checked)
                {
                    avatar.feet = 3;
                }
                if (this.rbFeet5.Checked)
                {
                    avatar.feet = 4;
                }
                if (this.rbFeet6.Checked)
                {
                    avatar.feet = 5;
                }
                if (this.rbTorso1.Checked)
                {
                    avatar.torso = 0;
                }
                if (this.rbTorso2.Checked)
                {
                    avatar.torso = 1;
                }
                if (this.rbTorso3.Checked)
                {
                    avatar.torso = 2;
                }
                if (this.rbTorso4.Checked)
                {
                    avatar.torso = 3;
                }
                if (this.rbTabardOff.Checked)
                {
                    avatar.tabard = -1;
                }
                if (this.rbTabard1.Checked)
                {
                    avatar.tabard = 0;
                }
                if (this.rbTabard2.Checked)
                {
                    avatar.tabard = 1;
                }
                if (this.rbTabard3.Checked)
                {
                    avatar.tabard = 2;
                }
                if (this.rbTabard4.Checked)
                {
                    avatar.tabard = 3;
                }
                if (this.rbTabard5.Checked)
                {
                    avatar.tabard = 4;
                }
                if (this.rbTabard6.Checked)
                {
                    avatar.tabard = 5;
                }
                if (this.rbTabard7.Checked)
                {
                    avatar.tabard = 6;
                }
                if (this.rbTabard8.Checked)
                {
                    avatar.tabard = 7;
                }
                if (this.rbArmsOff.Checked)
                {
                    avatar.arms = -1;
                }
                if (this.rbArms1.Checked)
                {
                    avatar.arms = 0;
                }
                if (this.rbArms2.Checked)
                {
                    avatar.arms = 1;
                }
                if (this.rbArms3.Checked)
                {
                    avatar.arms = 2;
                }
                if (this.rbArms4.Checked)
                {
                    avatar.arms = 3;
                }
                if (this.rbHandsOff.Checked)
                {
                    avatar.hands = -1;
                }
                if (this.rbHands1.Checked)
                {
                    avatar.hands = 0;
                }
                if (this.rbHands2.Checked)
                {
                    avatar.hands = 1;
                }
                if (this.rbHands3.Checked)
                {
                    avatar.hands = 2;
                }
                if (this.rbHands4.Checked)
                {
                    avatar.hands = 3;
                }
                if (this.rbShoulderOff.Checked)
                {
                    avatar.shoulder = -1;
                }
                if (this.rbShoulders1.Checked)
                {
                    avatar.shoulder = 0;
                }
                if (this.rbShoulders2.Checked)
                {
                    avatar.shoulder = 1;
                }
                if (this.rbShoulders3.Checked)
                {
                    avatar.shoulder = 2;
                }
                if (this.rbShoulders4.Checked)
                {
                    avatar.shoulder = 3;
                }
                if (this.rbFace1.Checked)
                {
                    avatar.face = 0;
                }
                if (this.rbFace2.Checked)
                {
                    avatar.face = 1;
                }
                if (this.rbFace3.Checked)
                {
                    avatar.face = 2;
                }
                if (this.rbFace4.Checked)
                {
                    avatar.face = 3;
                }
                if (this.rbFace5.Checked)
                {
                    avatar.face = 4;
                }
                if (this.rbFace6.Checked)
                {
                    avatar.face = 5;
                }
                if (this.rbFace7.Checked)
                {
                    avatar.face = 6;
                }
                if (this.rbHairOff.Checked)
                {
                    avatar.hair = -1;
                }
                if (this.rbHair1.Checked)
                {
                    avatar.hair = 0;
                }
                if (this.rbHair2.Checked)
                {
                    avatar.hair = 1;
                }
                if (this.rbHair3.Checked)
                {
                    avatar.hair = 2;
                }
                if (this.rbHair4.Checked)
                {
                    avatar.hair = 3;
                }
                if (this.rbHair5.Checked)
                {
                    avatar.hair = 4;
                }
                if (this.rbHair6.Checked)
                {
                    avatar.hair = 5;
                }
                if (this.rbHeadOff.Checked)
                {
                    avatar.head = -1;
                }
                if (this.rbHead1.Checked)
                {
                    avatar.head = 0;
                }
                if (this.rbHead2.Checked)
                {
                    avatar.head = 1;
                }
                if (this.rbHead3.Checked)
                {
                    avatar.head = 2;
                }
                if (this.rbHead4.Checked)
                {
                    avatar.head = 3;
                }
                if (this.rbHead5.Checked)
                {
                    avatar.head = 4;
                }
                if (this.rbHead6.Checked)
                {
                    avatar.head = 5;
                }
                if (this.rbHead7.Checked)
                {
                    avatar.head = 6;
                }
                if (this.rbHead8.Checked)
                {
                    avatar.head = 7;
                }
                if (this.rbHead9.Checked)
                {
                    avatar.head = 8;
                }
                if (this.rbHead10.Checked)
                {
                    avatar.head = 9;
                }
                if (this.rbHead11.Checked)
                {
                    avatar.head = 10;
                }
                if (this.rbHead12.Checked)
                {
                    avatar.head = 11;
                }
                if (this.rbWeaponOff.Checked)
                {
                    avatar.weapon = -1;
                }
                if (this.rbWeapon1.Checked)
                {
                    avatar.weapon = 0;
                }
                if (this.rbWeapon2.Checked)
                {
                    avatar.weapon = 1;
                }
                if (this.rbWeapon3.Checked)
                {
                    avatar.weapon = 2;
                }
                if (this.rbWeapon4.Checked)
                {
                    avatar.weapon = 3;
                }
                if (this.rbWeapon5.Checked)
                {
                    avatar.weapon = 4;
                }
                if (this.rbWeapon6.Checked)
                {
                    avatar.weapon = 5;
                }
                avatar.BodyColour = this.getColour(this.pnlBodyCSD);
                avatar.LegsColour = this.getColour(this.pnlLegsCSD);
                avatar.FeetColour = this.getColour(this.pnlFeetCSD);
                avatar.TorsoColour = this.getColour(this.pnlTorsoCSD);
                avatar.TabardColour = this.getColour(this.pnlTabardCSD);
                avatar.ArmsColour = this.getColour(this.pnlArmsCSD);
                avatar.HandsColour = this.getColour(this.pnlHandsCSD);
                avatar.ShouldersColour = this.getColour(this.pnlShouldersCSD);
                avatar.HairColour = this.getColour(this.pnlHairCSD);
                avatar.HeadColour = this.getColour(this.pnlHeadCSD);
                avatar.WeaponColour = this.getColour(this.pnlWeaponCSD);
                this.createColours(avatar);
                this.lastData = avatar;
                this.imgAvatar.update(avatar);
                this.forceUpdate = false;
            }
        }

        private void uploadAvatarCallback(UploadAvatar_ReturnType returnData)
        {
            if (returnData.Success)
            {
                MyMessageBox.Show(SK.Text("AvatarEditor_Uploaded", "Avatar Successfully Uploaded"), SK.Text("AvatarEditor_Avatar", "Avatar"));
            }
        }
    }
}

