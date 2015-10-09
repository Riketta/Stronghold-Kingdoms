namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SearchForVillagePopup : MyFormBase
    {
        private BitmapButton btnCancel;
        private BitmapButton btnSearchByID;
        private BitmapButton btnSearchByName;
        private IContainer components;
        private Label label3;
        private Label lblSearchByID;
        private Label lblSearchByName;
        private ListBox listBoxVillages;
        private TextBox tbSearchName;
        private TextBox tbVillageID;

        public SearchForVillagePopup()
        {
            this.InitializeComponent();
            this.lblSearchByID.Text = SK.Text("SearchForVillagePopup_search_by_ID", "Search By Village ID");
            this.lblSearchByName.Text = SK.Text("SearchForVillagePopup_search_by_Name", "Search By Village Name");
            this.btnCancel.Text = SK.Text("GENERIC_Close", "Close");
            this.Text = base.Title = SK.Text("SearchForVillagePopup_for_village", "Search For Village");
            this.btnSearchByID.Text = SK.Text("MailUserPopup_Search", "Search");
            this.btnSearchByName.Text = SK.Text("MailUserPopup_Search", "Search");
            if (!Program.mySettings.viewVillageIDs)
            {
                this.lblSearchByID.Visible = false;
                this.btnSearchByID.Visible = false;
                this.tbVillageID.Visible = false;
            }
            this.btnSearchByName.Enabled = false;
            this.btnSearchByID.Enabled = false;
        }

        private bool aiWorldSpecial(int villageID)
        {
            if (GameEngine.Instance.LocalWorldData.AIWorld)
            {
                if (!GameEngine.Instance.World.isSpecial(villageID))
                {
                    return false;
                }
                switch (GameEngine.Instance.World.getSpecial(villageID))
                {
                    case 7:
                    case 9:
                    case 11:
                    case 13:
                        return true;
                }
            }
            return false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("RenameVillagePopup_cancel");
            base.Close();
        }

        private void btnSearchByID_Click(object sender, EventArgs e)
        {
            int villageID = getInt32FromString(this.tbVillageID.Text);
            if (villageID >= 0)
            {
                this.listBoxVillages.Items.Clear();
                if ((!GameEngine.Instance.World.isCapital(villageID) && (!GameEngine.Instance.World.isSpecial(villageID) || this.aiWorldSpecial(villageID))) && GameEngine.Instance.World.isVillageVisible(villageID))
                {
                    VillageItem item = new VillageItem {
                        villageID = villageID
                    };
                    this.listBoxVillages.Items.Add(item);
                }
            }
        }

        private void btnSearchByName_Click(object sender, EventArgs e)
        {
            if (this.tbSearchName.Text.Length > 0)
            {
                List<int> list = GameEngine.Instance.World.searchVillageNames(this.tbSearchName.Text);
                this.listBoxVillages.Items.Clear();
                foreach (int num in list)
                {
                    VillageItem item = new VillageItem {
                        villageID = num
                    };
                    this.listBoxVillages.Items.Add(item);
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

        public static int getInt32FromString(string text)
        {
            if (text.Length != 0)
            {
                try
                {
                    return Convert.ToInt32(text);
                }
                catch (Exception)
                {
                }
            }
            return -1;
        }

        private void InitializeComponent()
        {
            this.lblSearchByName = new Label();
            this.tbSearchName = new TextBox();
            this.btnCancel = new BitmapButton();
            this.label3 = new Label();
            this.tbVillageID = new TextBox();
            this.lblSearchByID = new Label();
            this.listBoxVillages = new ListBox();
            this.btnSearchByName = new BitmapButton();
            this.btnSearchByID = new BitmapButton();
            base.SuspendLayout();
            this.lblSearchByName.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.lblSearchByName.ForeColor = ARGBColors.Black;
            this.lblSearchByName.Location = new Point(12, 0x29);
            this.lblSearchByName.Name = "lblSearchByName";
            this.lblSearchByName.Size = new Size(140, 40);
            this.lblSearchByName.TabIndex = 11;
            this.lblSearchByName.Text = "Search By Name";
            this.lblSearchByName.TextAlign = ContentAlignment.MiddleLeft;
            this.tbSearchName.BackColor = Color.FromArgb(0xeb, 240, 0xf3);
            this.tbSearchName.ForeColor = ARGBColors.Black;
            this.tbSearchName.Location = new Point(0x9e, 0x31);
            this.tbSearchName.MaxLength = 0x20;
            this.tbSearchName.Name = "tbSearchName";
            this.tbSearchName.Size = new Size(0x9b, 20);
            this.tbSearchName.TabIndex = 1;
            this.tbSearchName.TextChanged += new EventHandler(this.tbNewName_TextChanged);
            this.tbSearchName.KeyUp += new KeyEventHandler(this.tbSearchName_KeyUp);
            this.tbSearchName.KeyPress += new KeyPressEventHandler(this.tbSearchName_KeyPress);
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
            this.btnCancel.Location = new Point(0x142, 0x163);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OffsetPressedContent = true;
            this.btnCancel.Padding2 = 5;
            this.btnCancel.Size = new Size(0x7a, 0x20);
            this.btnCancel.StretchImage = false;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Close";
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
            this.tbVillageID.BackColor = Color.FromArgb(0xeb, 240, 0xf3);
            this.tbVillageID.Location = new Point(0x9e, 0x54);
            this.tbVillageID.MaxLength = 0x20;
            this.tbVillageID.Name = "tbVillageID";
            this.tbVillageID.Size = new Size(0x9b, 20);
            this.tbVillageID.TabIndex = 13;
            this.tbVillageID.TextChanged += new EventHandler(this.tbVillageID_TextChanged);
            this.tbVillageID.KeyUp += new KeyEventHandler(this.tbVillageID_KeyUp);
            this.tbVillageID.KeyPress += new KeyPressEventHandler(this.tbVillageID_KeyPress);
            this.lblSearchByID.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.lblSearchByID.ForeColor = ARGBColors.Black;
            this.lblSearchByID.Location = new Point(12, 0x4b);
            this.lblSearchByID.Name = "lblSearchByID";
            this.lblSearchByID.Size = new Size(140, 40);
            this.lblSearchByID.TabIndex = 14;
            this.lblSearchByID.Text = "Search By VillageID";
            this.lblSearchByID.TextAlign = ContentAlignment.MiddleLeft;
            this.listBoxVillages.BackColor = ARGBColors.White;
            this.listBoxVillages.ForeColor = ARGBColors.Black;
            this.listBoxVillages.FormattingEnabled = true;
            this.listBoxVillages.Location = new Point(0x22, 0x75);
            this.listBoxVillages.Name = "listBoxVillages";
            this.listBoxVillages.Size = new Size(0x181, 0xe1);
            this.listBoxVillages.TabIndex = 15;
            this.listBoxVillages.DoubleClick += new EventHandler(this.listBoxVillages_DoubleClick);
            this.btnSearchByName.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnSearchByName.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnSearchByName.BorderDrawing = true;
            this.btnSearchByName.FocusRectangleEnabled = false;
            this.btnSearchByName.Image = null;
            this.btnSearchByName.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnSearchByName.ImageBorderEnabled = true;
            this.btnSearchByName.ImageDropShadow = true;
            this.btnSearchByName.ImageFocused = null;
            this.btnSearchByName.ImageInactive = null;
            this.btnSearchByName.ImageMouseOver = null;
            this.btnSearchByName.ImageNormal = null;
            this.btnSearchByName.ImagePressed = null;
            this.btnSearchByName.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnSearchByName.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnSearchByName.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnSearchByName.Location = new Point(0x14b, 0x31);
            this.btnSearchByName.Name = "btnSearchByName";
            this.btnSearchByName.OffsetPressedContent = true;
            this.btnSearchByName.Padding2 = 5;
            this.btnSearchByName.Size = new Size(0x71, 0x15);
            this.btnSearchByName.StretchImage = false;
            this.btnSearchByName.TabIndex = 0x10;
            this.btnSearchByName.Text = "Search";
            this.btnSearchByName.TextDropShadow = false;
            this.btnSearchByName.UseVisualStyleBackColor = false;
            this.btnSearchByName.Click += new EventHandler(this.btnSearchByName_Click);
            this.btnSearchByID.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnSearchByID.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnSearchByID.BorderDrawing = true;
            this.btnSearchByID.FocusRectangleEnabled = false;
            this.btnSearchByID.Image = null;
            this.btnSearchByID.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnSearchByID.ImageBorderEnabled = true;
            this.btnSearchByID.ImageDropShadow = true;
            this.btnSearchByID.ImageFocused = null;
            this.btnSearchByID.ImageInactive = null;
            this.btnSearchByID.ImageMouseOver = null;
            this.btnSearchByID.ImageNormal = null;
            this.btnSearchByID.ImagePressed = null;
            this.btnSearchByID.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnSearchByID.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnSearchByID.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnSearchByID.Location = new Point(0x14b, 0x53);
            this.btnSearchByID.Name = "btnSearchByID";
            this.btnSearchByID.OffsetPressedContent = true;
            this.btnSearchByID.Padding2 = 5;
            this.btnSearchByID.Size = new Size(0x71, 0x15);
            this.btnSearchByID.StretchImage = false;
            this.btnSearchByID.TabIndex = 0x11;
            this.btnSearchByID.Text = "Search";
            this.btnSearchByID.TextDropShadow = false;
            this.btnSearchByID.UseVisualStyleBackColor = false;
            this.btnSearchByID.Click += new EventHandler(this.btnSearchByID_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            base.ClientSize = new Size(0x1c8, 0x18f);
            base.Controls.Add(this.btnSearchByID);
            base.Controls.Add(this.btnSearchByName);
            base.Controls.Add(this.listBoxVillages);
            base.Controls.Add(this.tbVillageID);
            base.Controls.Add(this.lblSearchByID);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.tbSearchName);
            base.Controls.Add(this.lblSearchByName);
            base.Icon = Resources.shk_icon;
            base.Name = "SearchForVillagePopup";
            base.ShowBar = true;
            base.ShowClose = true;
            base.StartPosition = FormStartPosition.CenterParent;
            base.Controls.SetChildIndex(this.lblSearchByName, 0);
            base.Controls.SetChildIndex(this.tbSearchName, 0);
            base.Controls.SetChildIndex(this.btnCancel, 0);
            base.Controls.SetChildIndex(this.lblSearchByID, 0);
            base.Controls.SetChildIndex(this.tbVillageID, 0);
            base.Controls.SetChildIndex(this.listBoxVillages, 0);
            base.Controls.SetChildIndex(this.btnSearchByName, 0);
            base.Controls.SetChildIndex(this.btnSearchByID, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listBoxVillages_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxVillages.SelectedIndex >= 0)
            {
                VillageItem selectedItem = (VillageItem) this.listBoxVillages.SelectedItem;
                if (selectedItem != null)
                {
                    GameEngine.Instance.World.zoomToVillage(selectedItem.villageID);
                    base.Close();
                }
            }
        }

        private void tbNewName_TextChanged(object sender, EventArgs e)
        {
            if (this.tbSearchName.Text.Length > 0)
            {
                this.btnSearchByName.Enabled = true;
            }
            else
            {
                this.btnSearchByName.Enabled = false;
            }
        }

        private void tbSearchName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.btnSearchByName_Click(sender, e);
                e.Handled = true;
            }
        }

        private void tbSearchName_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void tbVillageID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.btnSearchByID_Click(sender, e);
                e.Handled = true;
            }
        }

        private void tbVillageID_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void tbVillageID_TextChanged(object sender, EventArgs e)
        {
            if (getInt32FromString(this.tbVillageID.Text) >= 0)
            {
                this.btnSearchByID.Enabled = true;
            }
            else
            {
                this.btnSearchByID.Enabled = false;
            }
        }

        private class VillageItem
        {
            public int villageID = -1;

            public override string ToString()
            {
                return GameEngine.Instance.World.getVillageNameOrType(this.villageID);
            }
        }
    }
}

