namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AGUR : Form
    {
        private Button btnCancel;
        private Button btnGiveResources;
        private IContainer components;
        private Label label1;
        private string m_reasonString = "";
        private int m_villageID = -1;
        private RadioButton rbAle;
        private RadioButton rbApples;
        private RadioButton rbArmour;
        private RadioButton rbBows;
        private RadioButton rbBread;
        private RadioButton rbCatapults;
        private RadioButton rbCheese;
        private RadioButton rbClothes;
        private RadioButton rbFish;
        private RadioButton rbFurniture;
        private RadioButton rbIron;
        private RadioButton rbMeat;
        private RadioButton rbMetalware;
        private RadioButton rbPikes;
        private RadioButton rbPitch;
        private RadioButton rbSalt;
        private RadioButton rbSilk;
        private RadioButton rbSpices;
        private RadioButton rbStone;
        private RadioButton rbSwords;
        private RadioButton rbVeg;
        private RadioButton rbVenison;
        private RadioButton rbWine;
        private RadioButton rbWood;
        private TextBox tbAmount;

        public AGUR()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnGiveResources_Click(object sender, EventArgs e)
        {
            int duration = UserInfoScreen2.getInt32FromString(this.tbAmount.Text);
            if ((duration > 0) && (duration <= 0x186a0))
            {
                int num2 = 0;
                if (this.rbWood.Checked)
                {
                    num2 = 6;
                }
                if (this.rbStone.Checked)
                {
                    num2 = 7;
                }
                if (this.rbIron.Checked)
                {
                    num2 = 8;
                }
                if (this.rbPitch.Checked)
                {
                    num2 = 9;
                }
                if (this.rbAle.Checked)
                {
                    num2 = 12;
                }
                if (this.rbApples.Checked)
                {
                    num2 = 13;
                }
                if (this.rbBread.Checked)
                {
                    num2 = 14;
                }
                if (this.rbMeat.Checked)
                {
                    num2 = 0x10;
                }
                if (this.rbCheese.Checked)
                {
                    num2 = 0x11;
                }
                if (this.rbVeg.Checked)
                {
                    num2 = 15;
                }
                if (this.rbFish.Checked)
                {
                    num2 = 0x12;
                }
                if (this.rbBows.Checked)
                {
                    num2 = 0x1d;
                }
                if (this.rbPikes.Checked)
                {
                    num2 = 0x1c;
                }
                if (this.rbSwords.Checked)
                {
                    num2 = 30;
                }
                if (this.rbArmour.Checked)
                {
                    num2 = 0x1f;
                }
                if (this.rbCatapults.Checked)
                {
                    num2 = 0x20;
                }
                if (this.rbVenison.Checked)
                {
                    num2 = 0x16;
                }
                if (this.rbClothes.Checked)
                {
                    num2 = 0x13;
                }
                if (this.rbFurniture.Checked)
                {
                    num2 = 0x15;
                }
                if (this.rbMetalware.Checked)
                {
                    num2 = 0x1a;
                }
                if (this.rbSalt.Checked)
                {
                    num2 = 0x17;
                }
                if (this.rbWine.Checked)
                {
                    num2 = 0x21;
                }
                if (this.rbSpices.Checked)
                {
                    num2 = 0x18;
                }
                if (this.rbSilk.Checked)
                {
                    num2 = 0x19;
                }
                if (num2 != 0)
                {
                    this.sendCommandToServer(0x3e8 + num2, duration);
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

        public void init(int villageID)
        {
            this.m_villageID = villageID;
            this.Text = "Give Resources To : " + GameEngine.Instance.World.getVillageName(this.m_villageID);
            if (GameEngine.Instance.World.isCapital(this.m_villageID))
            {
                this.btnGiveResources.Enabled = false;
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(AGUR));
            this.btnGiveResources = new Button();
            this.btnCancel = new Button();
            this.tbAmount = new TextBox();
            this.label1 = new Label();
            this.rbWood = new RadioButton();
            this.rbStone = new RadioButton();
            this.rbIron = new RadioButton();
            this.rbPitch = new RadioButton();
            this.rbApples = new RadioButton();
            this.rbMeat = new RadioButton();
            this.rbCheese = new RadioButton();
            this.rbBread = new RadioButton();
            this.rbVeg = new RadioButton();
            this.rbFish = new RadioButton();
            this.rbAle = new RadioButton();
            this.rbBows = new RadioButton();
            this.rbPikes = new RadioButton();
            this.rbSwords = new RadioButton();
            this.rbArmour = new RadioButton();
            this.rbCatapults = new RadioButton();
            this.rbVenison = new RadioButton();
            this.rbClothes = new RadioButton();
            this.rbFurniture = new RadioButton();
            this.rbWine = new RadioButton();
            this.rbSalt = new RadioButton();
            this.rbMetalware = new RadioButton();
            this.rbSpices = new RadioButton();
            this.rbSilk = new RadioButton();
            base.SuspendLayout();
            this.btnGiveResources.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnGiveResources.Location = new Point(0x126, 0xf4);
            this.btnGiveResources.Name = "btnGiveResources";
            this.btnGiveResources.Size = new Size(0x74, 0x1a);
            this.btnGiveResources.TabIndex = 0;
            this.btnGiveResources.Text = "Give Resources";
            this.btnGiveResources.UseVisualStyleBackColor = true;
            this.btnGiveResources.Click += new EventHandler(this.btnGiveResources_Click);
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel.Location = new Point(0xac, 0xf4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x74, 0x1a);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.tbAmount.Location = new Point(0x15, 0xcf);
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.Size = new Size(100, 20);
            this.tbAmount.TabIndex = 2;
            this.tbAmount.Text = "0";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x7f, 210);
            this.label1.Name = "label1";
            this.label1.Size = new Size(100, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Amount (1-100,000)";
            this.rbWood.AutoSize = true;
            this.rbWood.Checked = true;
            this.rbWood.Location = new Point(0x15, 12);
            this.rbWood.Name = "rbWood";
            this.rbWood.Size = new Size(0x36, 0x11);
            this.rbWood.TabIndex = 4;
            this.rbWood.TabStop = true;
            this.rbWood.Text = "Wood";
            this.rbWood.UseVisualStyleBackColor = true;
            this.rbStone.AutoSize = true;
            this.rbStone.Location = new Point(0x15, 0x23);
            this.rbStone.Name = "rbStone";
            this.rbStone.Size = new Size(0x35, 0x11);
            this.rbStone.TabIndex = 5;
            this.rbStone.Text = "Stone";
            this.rbStone.UseVisualStyleBackColor = true;
            this.rbIron.AutoSize = true;
            this.rbIron.Location = new Point(0x15, 0x3a);
            this.rbIron.Name = "rbIron";
            this.rbIron.Size = new Size(0x2b, 0x11);
            this.rbIron.TabIndex = 6;
            this.rbIron.Text = "Iron";
            this.rbIron.UseVisualStyleBackColor = true;
            this.rbPitch.AutoSize = true;
            this.rbPitch.Location = new Point(0x15, 0x51);
            this.rbPitch.Name = "rbPitch";
            this.rbPitch.Size = new Size(0x31, 0x11);
            this.rbPitch.TabIndex = 7;
            this.rbPitch.Text = "Pitch";
            this.rbPitch.UseVisualStyleBackColor = true;
            this.rbApples.AutoSize = true;
            this.rbApples.Location = new Point(0x69, 12);
            this.rbApples.Name = "rbApples";
            this.rbApples.Size = new Size(0x39, 0x11);
            this.rbApples.TabIndex = 8;
            this.rbApples.Text = "Apples";
            this.rbApples.UseVisualStyleBackColor = true;
            this.rbMeat.AutoSize = true;
            this.rbMeat.Location = new Point(0x69, 0x23);
            this.rbMeat.Name = "rbMeat";
            this.rbMeat.Size = new Size(0x31, 0x11);
            this.rbMeat.TabIndex = 9;
            this.rbMeat.Text = "Meat";
            this.rbMeat.UseVisualStyleBackColor = true;
            this.rbCheese.AutoSize = true;
            this.rbCheese.Location = new Point(0x69, 0x3a);
            this.rbCheese.Name = "rbCheese";
            this.rbCheese.Size = new Size(0x3d, 0x11);
            this.rbCheese.TabIndex = 10;
            this.rbCheese.Text = "Cheese";
            this.rbCheese.UseVisualStyleBackColor = true;
            this.rbBread.AutoSize = true;
            this.rbBread.Location = new Point(0x69, 0x51);
            this.rbBread.Name = "rbBread";
            this.rbBread.Size = new Size(0x35, 0x11);
            this.rbBread.TabIndex = 11;
            this.rbBread.Text = "Bread";
            this.rbBread.UseVisualStyleBackColor = true;
            this.rbVeg.AutoSize = true;
            this.rbVeg.Location = new Point(0x69, 0x68);
            this.rbVeg.Name = "rbVeg";
            this.rbVeg.Size = new Size(0x2c, 0x11);
            this.rbVeg.TabIndex = 12;
            this.rbVeg.Text = "Veg";
            this.rbVeg.UseVisualStyleBackColor = true;
            this.rbFish.AutoSize = true;
            this.rbFish.Location = new Point(0x69, 0x7f);
            this.rbFish.Name = "rbFish";
            this.rbFish.Size = new Size(0x2c, 0x11);
            this.rbFish.TabIndex = 13;
            this.rbFish.Text = "Fish";
            this.rbFish.UseVisualStyleBackColor = true;
            this.rbAle.AutoSize = true;
            this.rbAle.Location = new Point(0x15, 0x7f);
            this.rbAle.Name = "rbAle";
            this.rbAle.Size = new Size(40, 0x11);
            this.rbAle.TabIndex = 14;
            this.rbAle.Text = "Ale";
            this.rbAle.UseVisualStyleBackColor = true;
            this.rbBows.AutoSize = true;
            this.rbBows.Location = new Point(0xc9, 12);
            this.rbBows.Name = "rbBows";
            this.rbBows.Size = new Size(0x33, 0x11);
            this.rbBows.TabIndex = 15;
            this.rbBows.Text = "Bows";
            this.rbBows.UseVisualStyleBackColor = true;
            this.rbPikes.AutoSize = true;
            this.rbPikes.Location = new Point(0xc9, 0x23);
            this.rbPikes.Name = "rbPikes";
            this.rbPikes.Size = new Size(0x33, 0x11);
            this.rbPikes.TabIndex = 0x10;
            this.rbPikes.Text = "Pikes";
            this.rbPikes.UseVisualStyleBackColor = true;
            this.rbSwords.AutoSize = true;
            this.rbSwords.Location = new Point(0xc9, 0x3a);
            this.rbSwords.Name = "rbSwords";
            this.rbSwords.Size = new Size(60, 0x11);
            this.rbSwords.TabIndex = 0x11;
            this.rbSwords.Text = "Swords";
            this.rbSwords.UseVisualStyleBackColor = true;
            this.rbArmour.AutoSize = true;
            this.rbArmour.Location = new Point(0xc9, 0x51);
            this.rbArmour.Name = "rbArmour";
            this.rbArmour.Size = new Size(0x3a, 0x11);
            this.rbArmour.TabIndex = 0x12;
            this.rbArmour.Text = "Armour";
            this.rbArmour.UseVisualStyleBackColor = true;
            this.rbCatapults.AutoSize = true;
            this.rbCatapults.Location = new Point(0xc9, 0x68);
            this.rbCatapults.Name = "rbCatapults";
            this.rbCatapults.Size = new Size(0x45, 0x11);
            this.rbCatapults.TabIndex = 0x13;
            this.rbCatapults.Text = "Catapults";
            this.rbCatapults.UseVisualStyleBackColor = true;
            this.rbVenison.AutoSize = true;
            this.rbVenison.Location = new Point(0x12a, 12);
            this.rbVenison.Name = "rbVenison";
            this.rbVenison.Size = new Size(0x3f, 0x11);
            this.rbVenison.TabIndex = 20;
            this.rbVenison.Text = "Venison";
            this.rbVenison.UseVisualStyleBackColor = true;
            this.rbClothes.AutoSize = true;
            this.rbClothes.Location = new Point(0x12a, 0x23);
            this.rbClothes.Name = "rbClothes";
            this.rbClothes.Size = new Size(60, 0x11);
            this.rbClothes.TabIndex = 0x15;
            this.rbClothes.Text = "Clothes";
            this.rbClothes.UseVisualStyleBackColor = true;
            this.rbFurniture.AutoSize = true;
            this.rbFurniture.Location = new Point(0x12a, 0x3a);
            this.rbFurniture.Name = "rbFurniture";
            this.rbFurniture.Size = new Size(0x42, 0x11);
            this.rbFurniture.TabIndex = 0x16;
            this.rbFurniture.Text = "Furniture";
            this.rbFurniture.UseVisualStyleBackColor = true;
            this.rbWine.AutoSize = true;
            this.rbWine.Location = new Point(0x12a, 0x51);
            this.rbWine.Name = "rbWine";
            this.rbWine.Size = new Size(50, 0x11);
            this.rbWine.TabIndex = 0x17;
            this.rbWine.Text = "Wine";
            this.rbWine.UseVisualStyleBackColor = true;
            this.rbSalt.AutoSize = true;
            this.rbSalt.Location = new Point(0x12a, 0x68);
            this.rbSalt.Name = "rbSalt";
            this.rbSalt.Size = new Size(0x2b, 0x11);
            this.rbSalt.TabIndex = 0x18;
            this.rbSalt.Text = "Salt";
            this.rbSalt.UseVisualStyleBackColor = true;
            this.rbMetalware.AutoSize = true;
            this.rbMetalware.Location = new Point(0x12a, 0x7f);
            this.rbMetalware.Name = "rbMetalware";
            this.rbMetalware.Size = new Size(0x4a, 0x11);
            this.rbMetalware.TabIndex = 0x19;
            this.rbMetalware.Text = "Metalware";
            this.rbMetalware.UseVisualStyleBackColor = true;
            this.rbSpices.AutoSize = true;
            this.rbSpices.Location = new Point(0x12a, 150);
            this.rbSpices.Name = "rbSpices";
            this.rbSpices.Size = new Size(0x39, 0x11);
            this.rbSpices.TabIndex = 0x1a;
            this.rbSpices.Text = "Spices";
            this.rbSpices.UseVisualStyleBackColor = true;
            this.rbSilk.AutoSize = true;
            this.rbSilk.Location = new Point(0x12a, 0xad);
            this.rbSilk.Name = "rbSilk";
            this.rbSilk.Size = new Size(0x2a, 0x11);
            this.rbSilk.TabIndex = 0x1b;
            this.rbSilk.Text = "Silk";
            this.rbSilk.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a6, 0x11a);
            base.Controls.Add(this.rbSilk);
            base.Controls.Add(this.rbSpices);
            base.Controls.Add(this.rbMetalware);
            base.Controls.Add(this.rbSalt);
            base.Controls.Add(this.rbWine);
            base.Controls.Add(this.rbFurniture);
            base.Controls.Add(this.rbClothes);
            base.Controls.Add(this.rbVenison);
            base.Controls.Add(this.rbCatapults);
            base.Controls.Add(this.rbArmour);
            base.Controls.Add(this.rbSwords);
            base.Controls.Add(this.rbPikes);
            base.Controls.Add(this.rbBows);
            base.Controls.Add(this.rbAle);
            base.Controls.Add(this.rbFish);
            base.Controls.Add(this.rbVeg);
            base.Controls.Add(this.rbBread);
            base.Controls.Add(this.rbCheese);
            base.Controls.Add(this.rbMeat);
            base.Controls.Add(this.rbApples);
            base.Controls.Add(this.rbPitch);
            base.Controls.Add(this.rbIron);
            base.Controls.Add(this.rbStone);
            base.Controls.Add(this.rbWood);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.tbAmount);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnGiveResources);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "AGUR";
            this.Text = "Give Resources";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void sendCommandToServer(int command, int duration)
        {
            if (RemoteServices.Instance.Admin)
            {
                this.m_reasonString = "";
                ReasonPopup popup = new ReasonPopup();
                popup.initResources(this, command - 0x3e8);
                popup.ShowDialog();
                if (this.m_reasonString.Length > 0)
                {
                    RemoteServices.Instance.SendCommands(this.m_villageID, command, duration, this.m_reasonString);
                    base.Close();
                }
                else
                {
                    MyMessageBox.Show("Not reason given", "Admin Error");
                }
            }
            else
            {
                MyMessageBox.Show("Command not sent", "Admin Error");
            }
        }

        public void setReasonString(string reasonString)
        {
            this.m_reasonString = reasonString;
        }
    }
}

