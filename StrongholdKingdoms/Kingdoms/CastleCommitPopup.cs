namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CastleCommitPopup : MyFormBase
    {
        private IContainer components;
        private Label label1;

        public CastleCommitPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            base.ShowClose = false;
        }

        private void CastleCommitPopup_Load(object sender, EventArgs e)
        {
            this.label1.Text = SK.Text("CastleCommitPopup_Updating_Castle_Please_Wait", "Updating Castle, Please wait....");
            this.Text = base.Title = SK.Text("CastleCommitPopup_Updating_Castle", "Updating Castle");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            base.SuspendLayout();
            this.label1.BackColor = ARGBColors.Transparent;
            this.label1.Location = new Point(3, 0x24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x10c, 0x23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Updating Castle, Please wait....";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x112, 0x4f);
            base.Controls.Add(this.label1);
            base.Name = "CastleCommitPopup";
            base.ShowClose = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Updating Castle";
            base.TopMost = true;
            base.Load += new EventHandler(this.CastleCommitPopup_Load);
            base.Controls.SetChildIndex(this.label1, 0);
            base.ResumeLayout(false);
        }
    }
}

