namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class RenameVillagePopup : MyFormBase
    {
        private BitmapButton btnCancel;
        private BitmapButton btnHistory;
        private BitmapButton btnOK;
        private IContainer components;
        private Label label1;
        private Label label2;
        private Label label3;
        private int m_villageID = -1;
        private bool parishNameMode;
        private TextBox tbNewName;
        private TextBox tbOldName;

        public RenameVillagePopup()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("RenameVillagePopup_cancel");
            base.Close();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("RenameVillagePopup_rename");
            if ((((this.tbNewName.Text.Length > 0) && (this.tbNewName.Text.Length <= 0x20)) && (StringValidation.isValidGameString(this.tbNewName.Text) && StringValidation.notAllSpaces(this.tbNewName.Text))) && (this.tbNewName.Text != this.tbOldName.Text))
            {
                if ((this.m_villageID >= 0) && !this.parishNameMode)
                {
                    RemoteServices.Instance.set_VillageRename_UserCallBack(new RemoteServices.VillageRename_UserCallBack(this.testCallback));
                    RemoteServices.Instance.VillageRename(this.m_villageID, this.tbNewName.Text);
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

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.tbOldName = new TextBox();
            this.tbNewName = new TextBox();
            this.btnOK = new BitmapButton();
            this.btnCancel = new BitmapButton();
            this.label3 = new Label();
            this.btnHistory = new BitmapButton();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label1.Location = new Point(0x10, 0x33);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x49, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Original Name";
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label2.Location = new Point(0x10, 0x57);
            this.label2.Name = "label2";
            this.label2.Size = new Size(60, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "New Name";
            this.tbOldName.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            this.tbOldName.ForeColor = ARGBColors.Black;
            this.tbOldName.Location = new Point(0x7a, 0x30);
            this.tbOldName.Name = "tbOldName";
            this.tbOldName.ReadOnly = true;
            this.tbOldName.Size = new Size(0x90, 20);
            this.tbOldName.TabIndex = 4;
            this.tbNewName.BackColor = Color.FromArgb(0xeb, 240, 0xf3);
            this.tbNewName.ForeColor = ARGBColors.Black;
            this.tbNewName.Location = new Point(0x7a, 0x54);
            this.tbNewName.MaxLength = 0x20;
            this.tbNewName.Name = "tbNewName";
            this.tbNewName.Size = new Size(0x90, 20);
            this.tbNewName.TabIndex = 1;
            this.tbNewName.TextChanged += new EventHandler(this.tbNewName_TextChanged);
            this.tbNewName.KeyPress += new KeyPressEventHandler(this.tbNewName_KeyPress);
            this.btnOK.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnOK.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnOK.BorderDrawing = true;
            this.btnOK.FocusRectangleEnabled = false;
            this.btnOK.Image = null;
            this.btnOK.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnOK.ImageBorderEnabled = true;
            this.btnOK.ImageDropShadow = true;
            this.btnOK.ImageFocused = null;
            this.btnOK.ImageInactive = null;
            this.btnOK.ImageMouseOver = null;
            this.btnOK.ImageNormal = null;
            this.btnOK.ImagePressed = null;
            this.btnOK.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnOK.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnOK.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnOK.Location = new Point(0x12d, 0x30);
            this.btnOK.Name = "btnOK";
            this.btnOK.OffsetPressedContent = true;
            this.btnOK.Padding2 = 5;
            this.btnOK.Size = new Size(0x4f, 20);
            this.btnOK.StretchImage = false;
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.TextDropShadow = false;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnCancel.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnCancel.BorderDrawing = true;
            this.btnCancel.FocusRectangleEnabled = false;
            this.btnCancel.Image = null;
            this.btnCancel.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnCancel.ImageBorderEnabled = true;
            this.btnCancel.ImageDropShadow = true;
            this.btnCancel.ImageFocused = null;
            this.btnCancel.ImageInactive = null;
            this.btnCancel.ImageMouseOver = null;
            this.btnCancel.ImageNormal = null;
            this.btnCancel.ImagePressed = null;
            this.btnCancel.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnCancel.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnCancel.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnCancel.Location = new Point(0x12d, 0x54);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OffsetPressedContent = true;
            this.btnCancel.Padding2 = 5;
            this.btnCancel.Size = new Size(0x4f, 20);
            this.btnCancel.StretchImage = false;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextDropShadow = false;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.label3.AutoSize = true;
            this.label3.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label3.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.ForeColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.label3.Location = new Point(0xb3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0, 0x10);
            this.label3.TabIndex = 9;
            this.btnHistory.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnHistory.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnHistory.BorderDrawing = true;
            this.btnHistory.FocusRectangleEnabled = false;
            this.btnHistory.Image = null;
            this.btnHistory.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnHistory.ImageBorderEnabled = true;
            this.btnHistory.ImageDropShadow = true;
            this.btnHistory.ImageFocused = null;
            this.btnHistory.ImageInactive = null;
            this.btnHistory.ImageMouseOver = null;
            this.btnHistory.ImageNormal = null;
            this.btnHistory.ImagePressed = null;
            this.btnHistory.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnHistory.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnHistory.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnHistory.Location = new Point(0x12d, 0x67);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.OffsetPressedContent = true;
            this.btnHistory.Padding2 = 5;
            this.btnHistory.Size = new Size(0x4f, 20);
            this.btnHistory.StretchImage = false;
            this.btnHistory.TabIndex = 13;
            this.btnHistory.Text = "History";
            this.btnHistory.TextDropShadow = false;
            this.btnHistory.UseVisualStyleBackColor = false;
            this.btnHistory.Visible = false;
            this.btnHistory.Click += new EventHandler(this.btnHistory_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            base.ClientSize = new Size(400, 0x7b);
            base.Controls.Add(this.btnHistory);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tbNewName);
            base.Controls.Add(this.tbOldName);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Icon = Resources.shk_icon;
            base.Name = "RenameVillagePopup";
            base.ShowBar = true;
            base.ShowClose = true;
            base.Controls.SetChildIndex(this.label1, 0);
            base.Controls.SetChildIndex(this.label2, 0);
            base.Controls.SetChildIndex(this.tbOldName, 0);
            base.Controls.SetChildIndex(this.tbNewName, 0);
            base.Controls.SetChildIndex(this.btnOK, 0);
            base.Controls.SetChildIndex(this.btnCancel, 0);
            base.Controls.SetChildIndex(this.btnHistory, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private bool notAllSpaces(string name)
        {
            foreach (char ch in name)
            {
                if (ch != ' ')
                {
                    return true;
                }
            }
            return false;
        }

        public void setParishVillageID(int villageID, string oldName)
        {
        }

        public void setVillageID(int villageID, string oldName)
        {
            this.parishNameMode = false;
            this.label1.Text = SK.Text("ReinforcementsRetrieval_Original_Name", "Original Name");
            this.label2.Text = SK.Text("ReinforcementsRetrieval_New_Name", "New Name");
            this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
            this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.Text = base.Title = SK.Text("ReinforcementsRetrieval_Rename_Village", "Rename Village");
            this.m_villageID = villageID;
            this.tbOldName.Text = oldName;
            this.tbNewName.Text = oldName;
            this.btnOK.Enabled = false;
        }

        private void tbNewName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.btnOK_Click(sender, e);
                e.Handled = true;
            }
            else if (!StringValidation.isValidChar(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbNewName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.btnOK_Click(sender, e);
            }
        }

        private void tbNewName_TextChanged(object sender, EventArgs e)
        {
            if (((this.tbNewName.Text.Length > 0) && (this.tbNewName.Text.Length <= 0x20)) && (StringValidation.isValidGameString(this.tbNewName.Text) && StringValidation.notAllSpaces(this.tbNewName.Text)))
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }

        public void testCallback(VillageRename_ReturnType returnData)
        {
            if (returnData.Success)
            {
                GameEngine.Instance.World.setVillageName(returnData.villageID, returnData.renamedName);
                if (InterfaceMgr.Instance.getSelectedMenuVillage() == returnData.villageID)
                {
                    InterfaceMgr.Instance.getTopRightMenu().setSelectedVillageName(returnData.renamedName, false);
                }
            }
        }
    }
}

