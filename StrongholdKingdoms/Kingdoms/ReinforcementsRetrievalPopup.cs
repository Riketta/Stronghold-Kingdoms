namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ReinforcementsRetrievalPopup : MyFormBase
    {
        private BitmapButton btnAll;
        private BitmapButton btnCancel;
        private BitmapButton btnRetrieve;
        private IContainer components;
        private bool drawing = true;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label7;
        private Label label9;
        private Label lblArchers;
        private Label lblCatapults;
        private Label lblPeasants;
        private Label lblPikemen;
        private Label lblSwordsmen;
        private int numArchers;
        private int numCatapults;
        private int numPeasants;
        private int numPikemen;
        private int numSwordsmen;
        private VillageReinforcementsPanel2 parent;
        private long reinfID = -1L;
        private TrackBar tbArchers;
        private TrackBar tbCatapults;
        private TrackBar tbPeasants;
        private TrackBar tbPikemen;
        private TrackBar tbSwordsmen;

        public ReinforcementsRetrievalPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("ReinforcementsRetrievalPopup_all");
            this.drawing = false;
            this.tbPeasants.Value = this.numPeasants;
            this.tbArchers.Value = this.numArchers;
            this.tbPikemen.Value = this.numPikemen;
            this.tbSwordsmen.Value = this.numSwordsmen;
            this.tbCatapults.Value = this.numCatapults;
            this.drawing = true;
            this.updateText();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("ReinforcementsRetrievalPopup_cancel");
            base.Close();
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("ReinforcementsRetrievalPopup_retrieve");
            bool flag = true;
            if (((this.tbPeasants.Value != this.numPeasants) || (this.tbArchers.Value != this.numArchers)) || (((this.tbPikemen.Value != this.numPikemen) || (this.tbSwordsmen.Value != this.numSwordsmen)) || (this.tbCatapults.Value != this.numCatapults)))
            {
                flag = false;
            }
            if (flag)
            {
                RemoteServices.Instance.ReturnReinforcements(this.reinfID);
            }
            else
            {
                RemoteServices.Instance.ReturnReinforcements(this.reinfID, this.tbPeasants.Value, this.tbArchers.Value, this.tbPikemen.Value, this.tbSwordsmen.Value, this.tbCatapults.Value);
            }
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(VillageReinforcementsPanel2 p, long reinforcementID, int totalPeasants, int totalArchers, int totalPikemen, int totalSwordsmen, int totalCatapults)
        {
            this.btnRetrieve.Text = SK.Text("ReinforcementsRetrieval_Retrieve", "Retrieve");
            this.btnAll.Text = SK.Text("ReinforcementsRetrieval_Select_All", "Select All");
            this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.label2.Text = SK.Text("GENERIC_Peasants", "Peasants");
            this.label3.Text = SK.Text("GENERIC_Archers", "Archers");
            this.label5.Text = SK.Text("GENERIC_Pikemen", "Pikemen");
            this.label7.Text = SK.Text("GENERIC_Swordsmens", "Swordsmen");
            this.label9.Text = SK.Text("GENERIC_Catapults", "Catapults");
            base.Title = this.Text = SK.Text("ReinforcementsRetrieval_Retrieve_Reinforcements", "Retrieve Reinforcements");
            this.parent = p;
            this.reinfID = reinforcementID;
            this.numPeasants = totalPeasants;
            this.numArchers = totalArchers;
            this.numPikemen = totalPikemen;
            this.numSwordsmen = totalSwordsmen;
            this.numCatapults = totalCatapults;
            this.drawing = false;
            this.tbPeasants.Value = 0;
            this.tbPeasants.Maximum = Math.Max(this.numPeasants, 0);
            this.tbPeasants.Value = this.tbPeasants.Maximum;
            this.tbArchers.Value = 0;
            this.tbArchers.Maximum = Math.Max(this.numArchers, 0);
            this.tbArchers.Value = this.tbArchers.Maximum;
            this.tbPikemen.Value = 0;
            this.tbPikemen.Maximum = Math.Max(this.numPikemen, 0);
            this.tbPikemen.Value = this.tbPikemen.Maximum;
            this.tbSwordsmen.Value = 0;
            this.tbSwordsmen.Maximum = Math.Max(this.numSwordsmen, 0);
            this.tbSwordsmen.Value = this.tbSwordsmen.Maximum;
            this.tbCatapults.Value = 0;
            this.tbCatapults.Maximum = Math.Max(this.numCatapults, 0);
            this.tbCatapults.Value = this.tbCatapults.Maximum;
            this.drawing = true;
            this.updateText();
        }

        private void InitializeComponent()
        {
            this.btnRetrieve = new BitmapButton();
            this.btnAll = new BitmapButton();
            this.btnCancel = new BitmapButton();
            this.tbPeasants = new TrackBar();
            this.lblPeasants = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.lblArchers = new Label();
            this.tbArchers = new TrackBar();
            this.label5 = new Label();
            this.lblPikemen = new Label();
            this.tbPikemen = new TrackBar();
            this.label7 = new Label();
            this.lblSwordsmen = new Label();
            this.tbSwordsmen = new TrackBar();
            this.label9 = new Label();
            this.lblCatapults = new Label();
            this.tbCatapults = new TrackBar();
            this.tbPeasants.BeginInit();
            this.tbArchers.BeginInit();
            this.tbPikemen.BeginInit();
            this.tbSwordsmen.BeginInit();
            this.tbCatapults.BeginInit();
            base.SuspendLayout();
            this.btnRetrieve.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnRetrieve.BorderColor = ARGBColors.DarkBlue;
            this.btnRetrieve.BorderDrawing = true;
            this.btnRetrieve.FocusRectangleEnabled = false;
            this.btnRetrieve.Image = null;
            this.btnRetrieve.ImageBorderColor = ARGBColors.Chocolate;
            this.btnRetrieve.ImageBorderEnabled = true;
            this.btnRetrieve.ImageDropShadow = true;
            this.btnRetrieve.ImageFocused = null;
            this.btnRetrieve.ImageInactive = null;
            this.btnRetrieve.ImageMouseOver = null;
            this.btnRetrieve.ImageNormal = null;
            this.btnRetrieve.ImagePressed = null;
            this.btnRetrieve.InnerBorderColor = ARGBColors.LightGray;
            this.btnRetrieve.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnRetrieve.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnRetrieve.Location = new Point(0x10c, 0x143);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.OffsetPressedContent = true;
            this.btnRetrieve.Padding2 = 5;
            this.btnRetrieve.Size = new Size(90, 30);
            this.btnRetrieve.StretchImage = false;
            this.btnRetrieve.TabIndex = 0x38;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.TextDropShadow = false;
            this.btnRetrieve.UseVisualStyleBackColor = true;
            this.btnRetrieve.Click += new EventHandler(this.btnRetrieve_Click);
            this.btnAll.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnAll.BorderColor = ARGBColors.DarkBlue;
            this.btnAll.BorderDrawing = true;
            this.btnAll.FocusRectangleEnabled = false;
            this.btnAll.Image = null;
            this.btnAll.ImageBorderColor = ARGBColors.Chocolate;
            this.btnAll.ImageBorderEnabled = true;
            this.btnAll.ImageDropShadow = true;
            this.btnAll.ImageFocused = null;
            this.btnAll.ImageInactive = null;
            this.btnAll.ImageMouseOver = null;
            this.btnAll.ImageNormal = null;
            this.btnAll.ImagePressed = null;
            this.btnAll.InnerBorderColor = ARGBColors.LightGray;
            this.btnAll.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnAll.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnAll.Location = new Point(140, 0x143);
            this.btnAll.Name = "btnAll";
            this.btnAll.OffsetPressedContent = true;
            this.btnAll.Padding2 = 5;
            this.btnAll.Size = new Size(90, 30);
            this.btnAll.StretchImage = false;
            this.btnAll.TabIndex = 0x39;
            this.btnAll.Text = "Select All";
            this.btnAll.TextDropShadow = false;
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new EventHandler(this.btnAll_Click);
            this.btnCancel.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnCancel.BorderColor = ARGBColors.DarkBlue;
            this.btnCancel.BorderDrawing = true;
            this.btnCancel.FocusRectangleEnabled = false;
            this.btnCancel.Image = null;
            this.btnCancel.ImageBorderColor = ARGBColors.Chocolate;
            this.btnCancel.ImageBorderEnabled = true;
            this.btnCancel.ImageDropShadow = true;
            this.btnCancel.ImageFocused = null;
            this.btnCancel.ImageInactive = null;
            this.btnCancel.ImageMouseOver = null;
            this.btnCancel.ImageNormal = null;
            this.btnCancel.ImagePressed = null;
            this.btnCancel.InnerBorderColor = ARGBColors.LightGray;
            this.btnCancel.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnCancel.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnCancel.Location = new Point(12, 0x143);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OffsetPressedContent = true;
            this.btnCancel.Padding2 = 5;
            this.btnCancel.Size = new Size(90, 30);
            this.btnCancel.StretchImage = false;
            this.btnCancel.TabIndex = 0x3a;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextDropShadow = false;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.tbPeasants.BackColor = Color.FromArgb(0x63, 0x70, 0x79);
            this.tbPeasants.Location = new Point(80, 0x2b);
            this.tbPeasants.Name = "tbPeasants";
            this.tbPeasants.Size = new Size(0xc6, 0x2d);
            this.tbPeasants.TabIndex = 0x3b;
            this.tbPeasants.ValueChanged += new EventHandler(this.tbPeasants_ValueChanged);
            this.lblPeasants.BackColor = ARGBColors.Transparent;
            this.lblPeasants.Location = new Point(0x114, 60);
            this.lblPeasants.Name = "lblPeasants";
            this.lblPeasants.Size = new Size(0x52, 0x1c);
            this.lblPeasants.TabIndex = 60;
            this.lblPeasants.Text = "0/0";
            this.lblPeasants.TextAlign = ContentAlignment.TopRight;
            this.label2.AutoSize = true;
            this.label2.BackColor = ARGBColors.Transparent;
            this.label2.Location = new Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x33, 13);
            this.label2.TabIndex = 0x3d;
            this.label2.Text = "Peasants";
            this.label3.AutoSize = true;
            this.label3.BackColor = ARGBColors.Transparent;
            this.label3.Location = new Point(12, 0x6f);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2b, 13);
            this.label3.TabIndex = 0x40;
            this.label3.Text = "Archers";
            this.lblArchers.BackColor = ARGBColors.Transparent;
            this.lblArchers.Location = new Point(0x114, 0x6f);
            this.lblArchers.Name = "lblArchers";
            this.lblArchers.Size = new Size(0x52, 0x1c);
            this.lblArchers.TabIndex = 0x3f;
            this.lblArchers.Text = "0/0";
            this.lblArchers.TextAlign = ContentAlignment.TopRight;
            this.tbArchers.BackColor = Color.FromArgb(0x6d, 0x7c, 0x85);
            this.tbArchers.Location = new Point(80, 0x5e);
            this.tbArchers.Name = "tbArchers";
            this.tbArchers.Size = new Size(0xc6, 0x2d);
            this.tbArchers.TabIndex = 0x3e;
            this.tbArchers.ValueChanged += new EventHandler(this.tbArchers_ValueChanged);
            this.label5.AutoSize = true;
            this.label5.BackColor = ARGBColors.Transparent;
            this.label5.Location = new Point(12, 0xa2);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x30, 13);
            this.label5.TabIndex = 0x43;
            this.label5.Text = "Pikemen";
            this.lblPikemen.BackColor = ARGBColors.Transparent;
            this.lblPikemen.Location = new Point(0x114, 0xa2);
            this.lblPikemen.Name = "lblPikemen";
            this.lblPikemen.Size = new Size(0x52, 0x1c);
            this.lblPikemen.TabIndex = 0x42;
            this.lblPikemen.Text = "0/0";
            this.lblPikemen.TextAlign = ContentAlignment.TopRight;
            this.tbPikemen.BackColor = Color.FromArgb(0x79, 0x89, 0x94);
            this.tbPikemen.Location = new Point(80, 0x91);
            this.tbPikemen.Name = "tbPikemen";
            this.tbPikemen.Size = new Size(0xc6, 0x2d);
            this.tbPikemen.TabIndex = 0x41;
            this.tbPikemen.ValueChanged += new EventHandler(this.tbPikemen_ValueChanged);
            this.label7.AutoSize = true;
            this.label7.BackColor = ARGBColors.Transparent;
            this.label7.Location = new Point(12, 0xd5);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x3e, 13);
            this.label7.TabIndex = 70;
            this.label7.Text = "Swordsmen";
            this.lblSwordsmen.BackColor = ARGBColors.Transparent;
            this.lblSwordsmen.Location = new Point(0x114, 0xd5);
            this.lblSwordsmen.Name = "lblSwordsmen";
            this.lblSwordsmen.Size = new Size(0x52, 0x1c);
            this.lblSwordsmen.TabIndex = 0x45;
            this.lblSwordsmen.Text = "0/0";
            this.lblSwordsmen.TextAlign = ContentAlignment.TopRight;
            this.tbSwordsmen.BackColor = Color.FromArgb(130, 0x93, 0x9e);
            this.tbSwordsmen.Location = new Point(80, 0xc4);
            this.tbSwordsmen.Name = "tbSwordsmen";
            this.tbSwordsmen.Size = new Size(0xc6, 0x2d);
            this.tbSwordsmen.TabIndex = 0x44;
            this.tbSwordsmen.ValueChanged += new EventHandler(this.tbSwordsmen_ValueChanged);
            this.label9.AutoSize = true;
            this.label9.BackColor = ARGBColors.Transparent;
            this.label9.Location = new Point(12, 0x108);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x33, 13);
            this.label9.TabIndex = 0x49;
            this.label9.Text = "Catapults";
            this.lblCatapults.BackColor = ARGBColors.Transparent;
            this.lblCatapults.Location = new Point(0x114, 0x108);
            this.lblCatapults.Name = "lblCatapults";
            this.lblCatapults.Size = new Size(0x52, 0x1c);
            this.lblCatapults.TabIndex = 0x48;
            this.lblCatapults.Text = "0/0";
            this.lblCatapults.TextAlign = ContentAlignment.TopRight;
            this.tbCatapults.BackColor = Color.FromArgb(140, 0x9f, 170);
            this.tbCatapults.Location = new Point(80, 0xf7);
            this.tbCatapults.Name = "tbCatapults";
            this.tbCatapults.Size = new Size(0xc6, 0x2d);
            this.tbCatapults.TabIndex = 0x47;
            this.tbCatapults.ValueChanged += new EventHandler(this.tbCatapults_ValueChanged);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(370, 0x16d);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.lblCatapults);
            base.Controls.Add(this.tbCatapults);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.lblSwordsmen);
            base.Controls.Add(this.tbSwordsmen);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.lblPikemen);
            base.Controls.Add(this.tbPikemen);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.lblArchers);
            base.Controls.Add(this.tbArchers);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.lblPeasants);
            base.Controls.Add(this.tbPeasants);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnAll);
            base.Controls.Add(this.btnRetrieve);
            base.Icon = Resources.shk_icon;
            base.Name = "ReinforcementsRetrievalPopup";
            base.ShowClose = true;
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Retrieve Reinforcements";
            base.Controls.SetChildIndex(this.btnRetrieve, 0);
            base.Controls.SetChildIndex(this.btnAll, 0);
            base.Controls.SetChildIndex(this.btnCancel, 0);
            base.Controls.SetChildIndex(this.tbPeasants, 0);
            base.Controls.SetChildIndex(this.lblPeasants, 0);
            base.Controls.SetChildIndex(this.label2, 0);
            base.Controls.SetChildIndex(this.tbArchers, 0);
            base.Controls.SetChildIndex(this.lblArchers, 0);
            base.Controls.SetChildIndex(this.label3, 0);
            base.Controls.SetChildIndex(this.tbPikemen, 0);
            base.Controls.SetChildIndex(this.lblPikemen, 0);
            base.Controls.SetChildIndex(this.label5, 0);
            base.Controls.SetChildIndex(this.tbSwordsmen, 0);
            base.Controls.SetChildIndex(this.lblSwordsmen, 0);
            base.Controls.SetChildIndex(this.label7, 0);
            base.Controls.SetChildIndex(this.tbCatapults, 0);
            base.Controls.SetChildIndex(this.lblCatapults, 0);
            base.Controls.SetChildIndex(this.label9, 0);
            this.tbPeasants.EndInit();
            this.tbArchers.EndInit();
            this.tbPikemen.EndInit();
            this.tbSwordsmen.EndInit();
            this.tbCatapults.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void tbArchers_ValueChanged(object sender, EventArgs e)
        {
            this.updateText();
        }

        private void tbCatapults_ValueChanged(object sender, EventArgs e)
        {
            this.updateText();
        }

        private void tbPeasants_ValueChanged(object sender, EventArgs e)
        {
            this.updateText();
        }

        private void tbPikemen_ValueChanged(object sender, EventArgs e)
        {
            this.updateText();
        }

        private void tbSwordsmen_ValueChanged(object sender, EventArgs e)
        {
            this.updateText();
        }

        private void updateText()
        {
            if (this.drawing)
            {
                this.lblPeasants.Text = this.tbPeasants.Value.ToString() + "/" + this.numPeasants.ToString();
                this.lblArchers.Text = this.tbArchers.Value.ToString() + "/" + this.numArchers.ToString();
                this.lblPikemen.Text = this.tbPikemen.Value.ToString() + "/" + this.numPikemen.ToString();
                this.lblSwordsmen.Text = this.tbSwordsmen.Value.ToString() + "/" + this.numSwordsmen.ToString();
                this.lblCatapults.Text = this.tbCatapults.Value.ToString() + "/" + this.numCatapults.ToString();
            }
        }
    }
}

