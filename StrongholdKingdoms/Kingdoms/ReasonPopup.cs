namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ReasonPopup : MyFormBase
    {
        private BitmapButton btnCancel;
        private BitmapButton btnOK;
        private IContainer components;
        private Label label1;
        private AGUR m_agur;
        private UserInfoScreen m_parent;
        private TextBox tbReason;

        public ReasonPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.m_parent != null)
            {
                this.m_parent.setReasonString("");
            }
            if (this.m_agur != null)
            {
                this.m_agur.setReasonString("");
            }
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.tbReason.Text.Length > 0)
            {
                if (this.m_parent != null)
                {
                    this.m_parent.setReasonString(this.tbReason.Text);
                }
                if (this.m_agur != null)
                {
                    this.m_agur.setReasonString(this.tbReason.Text);
                }
                base.Close();
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

        public void init(UserInfoScreen parent)
        {
            this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
            this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.label1.Text = SK.Text("ReasonPopup_Enter_Reason", "Enter Reason For this Action");
            this.Text = base.Title = SK.Text("ReasonPopup_Reason", "Reason");
            this.m_parent = parent;
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(ReasonPopup));
            this.btnOK = new BitmapButton();
            this.btnCancel = new BitmapButton();
            this.tbReason = new TextBox();
            this.label1 = new Label();
            base.SuspendLayout();
            this.btnOK.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnOK.Location = new Point(0x110, 0x7c);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnCancel.Location = new Point(20, 0x7c);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.tbReason.BackColor = Color.FromArgb(0xeb, 240, 0xf3);
            this.tbReason.Location = new Point(0x1f, 0x40);
            this.tbReason.Multiline = true;
            this.tbReason.Name = "tbReason";
            this.tbReason.Size = new Size(0x12f, 0x2e);
            this.tbReason.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.BackColor = ARGBColors.Transparent;
            this.label1.Location = new Point(20, 40);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x8e, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Enter Reason For this Action";
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x169, 0x9e);
            base.ControlBox = false;
            base.Controls.Add(this.label1);
            base.Controls.Add(this.tbReason);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            base.Name = "ReasonPopup";
            base.ShowIcon = false;
            this.Text = "Reason";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void initResources(AGUR parent, int resource)
        {
            this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
            this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.label1.Text = SK.Text("ReasonPopup_Enter_Reason", "Enter Reason For this Action");
            this.Text = base.Title = SK.Text("ReasonPopup_Reason", "Reason") + " : " + VillageBuildingsData.getResourceNames(resource);
            this.m_agur = parent;
        }
    }
}

