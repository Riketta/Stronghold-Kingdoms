namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class JoiningWorldPopup : MyFormBase
    {
        private IContainer components;
        private Label label1;
        private Label label2;
        private Label lblCounty;

        public JoiningWorldPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(int county, string country)
        {
            if (county >= 0)
            {
                this.label1.Text = SK.Text("JoiningWorldPopup_Find_Village", "Trying to Find Village in :");
                this.lblCounty.Text = GameEngine.Instance.World.getCountyName(county);
            }
            else
            {
                this.label1.Text = SK.Text("JoiningWorldPopup_Find_Village2", "Trying to Find Village");
                this.lblCounty.Text = "";
            }
            this.label2.Text = SK.Text("JoiningWorldPopup_Please_Wait", "Please wait, this may take a few moments.");
            this.Text = base.Title = SK.Text("JoiningWorldPopup_Finding_Village", "Finding Village");
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(JoiningWorldPopup));
            this.label1 = new Label();
            this.lblCounty = new Label();
            this.label2 = new Label();
            base.SuspendLayout();
            this.label1.BackColor = ARGBColors.Transparent;
            this.label1.Location = new Point(0x1c, 0x34);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x131, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trying to Find Village in";
            this.label1.TextAlign = ContentAlignment.TopCenter;
            this.lblCounty.BackColor = ARGBColors.Transparent;
            this.lblCounty.Location = new Point(0x1c, 0x4c);
            this.lblCounty.Name = "lblCounty";
            this.lblCounty.Size = new Size(0x131, 20);
            this.lblCounty.TabIndex = 1;
            this.lblCounty.Text = "County";
            this.lblCounty.TextAlign = ContentAlignment.TopCenter;
            this.label2.BackColor = ARGBColors.Transparent;
            this.label2.Location = new Point(0x1c, 0x65);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x131, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "Please wait, this may take a few moments.";
            this.label2.TextAlign = ContentAlignment.TopCenter;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x169, 0x8b);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.lblCounty);
            base.Controls.Add(this.label1);
            base.Icon = Resources.shk_icon;
            base.Name = "JoiningWorldPopup";
            this.Text = "Finding Village";
            base.Controls.SetChildIndex(this.label1, 0);
            base.Controls.SetChildIndex(this.lblCounty, 0);
            base.Controls.SetChildIndex(this.label2, 0);
            base.ResumeLayout(false);
        }
    }
}

