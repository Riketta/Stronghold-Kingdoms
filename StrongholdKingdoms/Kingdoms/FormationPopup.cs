namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class FormationPopup : MyFormBase
    {
        private BitmapButton btnClose;
        private BitmapButton btnDelete;
        private BitmapButton btnRename;
        private BitmapButton btnRestore;
        private BitmapButton btnSave;
        private IContainer components;
        private FormationPanel customPanel;
        private GroupBox gbManage;
        private GroupBox gbNew;
        private Label lblName;
        private Label lblSelected;
        private Label lblStored;
        private ListBox lstStored;
        private PictureBox pictureBox1;
        private string saveName = "";
        private TextBox txtSaveName;
        private TextBox txtSelected;

        public FormationPopup()
        {
            this.InitializeComponent();
            GameEngine.Instance.CastleAttackerSetup.updateOldAttackSetupFilenames();
            this.loadNames();
            this.btnRestore.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnRename.Enabled = false;
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.btnSave.Text = SK.Text("Formations_Save", "Save Formation");
            this.btnRestore.Text = SK.Text("Formations_Load", "Load Formation");
            this.btnDelete.Text = SK.Text("Formations_Delete", "Delete Formation");
            this.btnRename.Text = SK.Text("Formations_Rename", "Rename Formation");
            this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
            this.lblStored.Text = SK.Text("Formations_Stored", "Stored Formations");
            this.lblName.Text = SK.Text("Formations_Name", "Formation Name");
            this.lblSelected.Text = SK.Text("Formations_Selected", "Selected Formation");
            this.gbNew.Text = SK.Text("Formations_New_Box", "Create New Formation");
            this.gbManage.Text = SK.Text("Formations_Manage_Box", "Manage Formations");
            base.Title = SK.Text("CastleMapPanel_Manage_Formations", "Manage Formations");
            base.closeCallback = new MyFormBase.MFBClose(this.closeFunction);
            this.pictureBox1.BackgroundImage = (Image) GFXLibrary.formations_img;
            this.customPanel.init(this);
            this.btnSave.Visible = false;
            this.btnRestore.Visible = false;
            this.btnDelete.Visible = false;
            this.btnRename.Visible = false;
            this.btnClose.Visible = false;
            this.lblStored.Visible = false;
            this.lblName.Visible = false;
            this.lblSelected.Visible = false;
            this.gbNew.Visible = false;
            this.gbManage.Visible = false;
            this.pictureBox1.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.closeFunction();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if ((this.lstStored.SelectedIndex != -1) && (MyMessageBox.Show(SK.Text("Formations_Confirm_Delete", "Are you sure you want to delete this formation?"), SK.Text("Formations_Delete_Confirmation_Title", "Confirm Deletion"), MessageBoxButtons.YesNo) != DialogResult.No))
            {
                GameEngine.Instance.CastleAttackerSetup.deleteAttackSetup((string) this.lstStored.SelectedItem);
                this.lstStored.Items.Remove(this.lstStored.SelectedItem);
                this.txtSelected.Text = "";
                this.btnRename.Enabled = false;
                this.btnRestore.Enabled = false;
                this.btnDelete.Enabled = false;
                this.saveNames();
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if ((this.lstStored.SelectedIndex != -1) && (this.txtSelected.Text != ""))
            {
                if (this.lstStored.Items.Contains(this.txtSelected.Text))
                {
                    MyMessageBox.Show(SK.Text("Formations_Name_Exists", "That name is already in use"), SK.Text("Formations_Overwrite_Title", "Name Already in Use"));
                }
                else
                {
                    string selectedItem = (string) this.lstStored.SelectedItem;
                    string text = this.txtSelected.Text;
                    GameEngine.Instance.CastleAttackerSetup.renameAttackSetup(selectedItem, text);
                    this.lstStored.Items.Remove(selectedItem);
                    this.lstStored.Items.Add(text);
                    if (this.saveNames())
                    {
                        this.lstStored.SelectedItem = text;
                    }
                }
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if ((this.lstStored.SelectedIndex != -1) && (GameEngine.Instance.CastleAttackerSetup != null))
            {
                GameEngine.Instance.CastleAttackerSetup.restoreAttackSetup((string) this.lstStored.SelectedItem);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.saveName = this.txtSaveName.Text;
            if ((this.saveName != "") && (!this.lstStored.Items.Contains(this.saveName) || (MyMessageBox.Show(SK.Text("Formations_Overwrite", "That name is already in use. Do you want to replace the existing formation?"), SK.Text("Formations_Overwrite_Title", "Name Already in Use"), MessageBoxButtons.YesNo) != DialogResult.No)))
            {
                this.saveFormation();
                if (!this.lstStored.Items.Contains(this.saveName))
                {
                    this.lstStored.Items.Add(this.saveName);
                }
                this.saveNames();
            }
        }

        private void closeFunction()
        {
            InterfaceMgr.Instance.closeFormationPopup();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public string getCreateText()
        {
            return this.txtSaveName.Text;
        }

        public string getSelectedText()
        {
            return this.txtSelected.Text;
        }

        private void InitializeComponent()
        {
            this.customPanel = new FormationPanel();
            this.lstStored = new ListBox();
            this.txtSaveName = new TextBox();
            this.pictureBox1 = new PictureBox();
            this.btnRestore = new BitmapButton();
            this.btnDelete = new BitmapButton();
            this.btnSave = new BitmapButton();
            this.btnClose = new BitmapButton();
            this.lblStored = new Label();
            this.btnRename = new BitmapButton();
            this.lblName = new Label();
            this.gbNew = new GroupBox();
            this.gbManage = new GroupBox();
            this.lblSelected = new Label();
            this.txtSelected = new TextBox();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.gbNew.SuspendLayout();
            this.gbManage.SuspendLayout();
            base.SuspendLayout();
            this.customPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.customPanel.ClickThru = false;
            this.customPanel.Location = new Point(0, 0x22);
            this.customPanel.Name = "customPanel";
            this.customPanel.PanelActive = true;
            this.customPanel.Size = base.Size;
            this.customPanel.StoredGraphics = null;
            this.customPanel.TabIndex = 0x63;
            this.lstStored.BackColor = ARGBColors.White;
            this.lstStored.ForeColor = ARGBColors.Black;
            this.lstStored.FormattingEnabled = true;
            this.lstStored.Location = new Point(6, 0x23);
            this.lstStored.Name = "lstStored";
            this.lstStored.Size = new Size(160, 0xc7);
            this.lstStored.Sorted = true;
            this.lstStored.TabIndex = 0;
            this.lstStored.MouseDoubleClick += new MouseEventHandler(this.lstStored_MouseDoubleClick);
            this.lstStored.SelectedIndexChanged += new EventHandler(this.lstStored_SelectedIndexChanged);
            this.txtSaveName.BackColor = ARGBColors.White;
            this.txtSaveName.ForeColor = ARGBColors.Black;
            this.txtSaveName.BorderStyle = BorderStyle.FixedSingle;
            this.txtSaveName.Name = "txtSaveName";
            this.txtSaveName.Size = new Size(160, 20);
            this.txtSaveName.Location = new Point(0x24, 0x17d);
            this.txtSaveName.TabIndex = 1;
            this.txtSaveName.TextChanged += new EventHandler(this.txtSaveName_TextChanged);
            this.txtSaveName.KeyDown += new KeyEventHandler(this.txtSaveName_KeyDown);
            this.txtSaveName.KeyPress += new KeyPressEventHandler(this.txtSaveName_KeyPress);
            this.pictureBox1.BackgroundImageLayout = ImageLayout.Center;
            this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            this.pictureBox1.Location = new Point(0x17, 0xb3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0xeb, 150);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            this.btnRestore.BorderColor = ARGBColors.DarkBlue;
            this.btnRestore.BorderDrawing = true;
            this.btnRestore.FocusRectangleEnabled = false;
            this.btnRestore.Image = null;
            this.btnRestore.ImageBorderColor = ARGBColors.Chocolate;
            this.btnRestore.ImageBorderEnabled = true;
            this.btnRestore.ImageDropShadow = true;
            this.btnRestore.ImageFocused = null;
            this.btnRestore.ImageInactive = null;
            this.btnRestore.ImageMouseOver = null;
            this.btnRestore.ImageNormal = null;
            this.btnRestore.ImagePressed = null;
            this.btnRestore.InnerBorderColor = ARGBColors.LightGray;
            this.btnRestore.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnRestore.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnRestore.Location = new Point(6, 240);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.OffsetPressedContent = true;
            this.btnRestore.Padding2 = 5;
            this.btnRestore.Size = new Size(160, 0x1b);
            this.btnRestore.StretchImage = false;
            this.btnRestore.TabIndex = 15;
            this.btnRestore.Text = "Deploy Formation";
            this.btnRestore.TextDropShadow = false;
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new EventHandler(this.btnRestore_Click);
            this.btnDelete.BorderColor = ARGBColors.DarkBlue;
            this.btnDelete.BorderDrawing = true;
            this.btnDelete.FocusRectangleEnabled = false;
            this.btnDelete.Image = null;
            this.btnDelete.ImageBorderColor = ARGBColors.Chocolate;
            this.btnDelete.ImageBorderEnabled = true;
            this.btnDelete.ImageDropShadow = true;
            this.btnDelete.ImageFocused = null;
            this.btnDelete.ImageInactive = null;
            this.btnDelete.ImageMouseOver = null;
            this.btnDelete.ImageNormal = null;
            this.btnDelete.ImagePressed = null;
            this.btnDelete.InnerBorderColor = ARGBColors.LightGray;
            this.btnDelete.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnDelete.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnDelete.Location = new Point(0xac, 0x5e);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.OffsetPressedContent = true;
            this.btnDelete.Padding2 = 5;
            this.btnDelete.Size = new Size(160, 0x1b);
            this.btnDelete.StretchImage = false;
            this.btnDelete.TabIndex = 0x10;
            this.btnDelete.Text = "Delete Formation";
            this.btnDelete.TextDropShadow = false;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnSave.BorderColor = ARGBColors.DarkBlue;
            this.btnSave.BorderDrawing = true;
            this.btnSave.FocusRectangleEnabled = false;
            this.btnSave.Image = null;
            this.btnSave.ImageBorderColor = ARGBColors.Chocolate;
            this.btnSave.ImageBorderEnabled = true;
            this.btnSave.ImageDropShadow = true;
            this.btnSave.ImageFocused = null;
            this.btnSave.ImageInactive = null;
            this.btnSave.ImageMouseOver = null;
            this.btnSave.ImageNormal = null;
            this.btnSave.ImagePressed = null;
            this.btnSave.InnerBorderColor = ARGBColors.LightGray;
            this.btnSave.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnSave.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnSave.Location = new Point(0x23, 0x3d);
            this.btnSave.Name = "btnSave";
            this.btnSave.OffsetPressedContent = true;
            this.btnSave.Padding2 = 5;
            this.btnSave.Size = new Size(160, 0x1b);
            this.btnSave.StretchImage = false;
            this.btnSave.TabIndex = 0x11;
            this.btnSave.Text = "Save Formation";
            this.btnSave.TextDropShadow = false;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            this.btnClose.BorderColor = ARGBColors.DarkBlue;
            this.btnClose.BorderDrawing = true;
            this.btnClose.FocusRectangleEnabled = false;
            this.btnClose.Image = null;
            this.btnClose.ImageBorderColor = ARGBColors.Chocolate;
            this.btnClose.ImageBorderEnabled = true;
            this.btnClose.ImageDropShadow = true;
            this.btnClose.ImageFocused = null;
            this.btnClose.ImageInactive = null;
            this.btnClose.ImageMouseOver = null;
            this.btnClose.ImageNormal = null;
            this.btnClose.ImagePressed = null;
            this.btnClose.InnerBorderColor = ARGBColors.LightGray;
            this.btnClose.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnClose.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnClose.Location = new Point(460, 0x15c);
            this.btnClose.Name = "btnClose";
            this.btnClose.OffsetPressedContent = true;
            this.btnClose.Padding2 = 5;
            this.btnClose.Size = new Size(160, 0x1b);
            this.btnClose.StretchImage = false;
            this.btnClose.TabIndex = 0x12;
            this.btnClose.Text = "Close";
            this.btnClose.TextDropShadow = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.lblStored.AutoSize = true;
            this.lblStored.BackColor = ARGBColors.Transparent;
            this.lblStored.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblStored.Location = new Point(3, 0x13);
            this.lblStored.Name = "lblStored";
            this.lblStored.Size = new Size(0x6d, 13);
            this.lblStored.TabIndex = 20;
            this.lblStored.Text = "Stored Formations";
            this.btnRename.BorderColor = ARGBColors.DarkBlue;
            this.btnRename.BorderDrawing = true;
            this.btnRename.FocusRectangleEnabled = false;
            this.btnRename.Image = null;
            this.btnRename.ImageBorderColor = ARGBColors.Chocolate;
            this.btnRename.ImageBorderEnabled = true;
            this.btnRename.ImageDropShadow = true;
            this.btnRename.ImageFocused = null;
            this.btnRename.ImageInactive = null;
            this.btnRename.ImageMouseOver = null;
            this.btnRename.ImageNormal = null;
            this.btnRename.ImagePressed = null;
            this.btnRename.InnerBorderColor = ARGBColors.LightGray;
            this.btnRename.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnRename.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnRename.Location = new Point(0xad, 0x3d);
            this.btnRename.Name = "btnRename";
            this.btnRename.OffsetPressedContent = true;
            this.btnRename.Padding2 = 5;
            this.btnRename.Size = new Size(160, 0x1b);
            this.btnRename.StretchImage = false;
            this.btnRename.TabIndex = 0x15;
            this.btnRename.Text = "Rename Formation";
            this.btnRename.TextDropShadow = false;
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new EventHandler(this.btnRename_Click);
            this.lblName.AutoSize = true;
            this.lblName.BackColor = ARGBColors.Transparent;
            this.lblName.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblName.Location = new Point(0x20, 0x13);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0x62, 13);
            this.lblName.TabIndex = 0x16;
            this.lblName.Text = "Formation Name";
            this.lblName.Click += new EventHandler(this.lblName_Click);
            this.gbNew.BackColor = ARGBColors.Transparent;
            this.gbNew.Controls.Add(this.lblName);
            this.gbNew.Controls.Add(this.btnSave);
            this.gbNew.Location = new Point(0x17, 0x31);
            this.gbNew.Name = "gbNew";
            this.gbNew.RightToLeft = RightToLeft.No;
            this.gbNew.Size = new Size(0xeb, 0x6d);
            this.gbNew.TabIndex = 0x17;
            this.gbNew.TabStop = false;
            this.gbNew.Text = "Create a New Formation";
            this.gbManage.BackColor = ARGBColors.Transparent;
            this.gbManage.Controls.Add(this.lstStored);
            this.gbManage.Controls.Add(this.lblSelected);
            this.gbManage.Controls.Add(this.btnRestore);
            this.gbManage.Controls.Add(this.btnDelete);
            this.gbManage.Controls.Add(this.lblStored);
            this.gbManage.Controls.Add(this.btnRename);
            this.gbManage.Location = new Point(0x117, 0x31);
            this.gbManage.Name = "gbManage";
            this.gbManage.Size = new Size(0x155, 0x116);
            this.gbManage.TabIndex = 0x18;
            this.gbManage.TabStop = false;
            this.gbManage.Text = "Manage Formations";
            this.lblSelected.AutoSize = true;
            this.lblSelected.BackColor = ARGBColors.Transparent;
            this.lblSelected.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblSelected.Location = new Point(170, 0x13);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new Size(0x74, 13);
            this.lblSelected.TabIndex = 0x18;
            this.lblSelected.Text = "Selected Formation";
            this.txtSelected.BackColor = ARGBColors.White;
            this.txtSelected.ForeColor = ARGBColors.Black;
            this.txtSelected.BorderStyle = BorderStyle.FixedSingle;
            this.txtSelected.Size = new Size(160, 20);
            this.txtSelected.Location = new Point(270, 0x17d);
            this.txtSelected.Name = "txtSelected";
            this.txtSelected.TabIndex = 0x17;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(700, 450);
            base.Controls.Add(this.gbManage);
            base.Controls.Add(this.gbNew);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.txtSelected);
            base.Controls.Add(this.txtSaveName);
            base.Controls.Add(this.customPanel);
            this.DoubleBuffered = true;
            base.Name = "FormationPopup";
            base.ShowClose = true;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Manage Formations";
            base.TransparencyKey = ARGBColors.Fuchsia;
            base.Controls.SetChildIndex(this.customPanel, 0);
            base.Controls.SetChildIndex(this.txtSaveName, 0);
            base.Controls.SetChildIndex(this.txtSelected, 0);
            base.Controls.SetChildIndex(this.btnClose, 0);
            base.Controls.SetChildIndex(this.pictureBox1, 0);
            base.Controls.SetChildIndex(this.gbNew, 0);
            base.Controls.SetChildIndex(this.gbManage, 0);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.gbNew.ResumeLayout(false);
            this.gbNew.PerformLayout();
            this.gbManage.ResumeLayout(false);
            this.gbManage.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void lblName_Click(object sender, EventArgs e)
        {
        }

        private void loadNames()
        {
            string str = GameEngine.getSettingsPath(true);
            string path = "StoredSetupNames.cas";
            FileStream input = null;
            BinaryReader reader = null;
            path = str + @"\" + path;
            try
            {
                input = new FileStream(path, FileMode.Open);
                reader = new BinaryReader(input);
                int num = reader.ReadInt32();
                this.lstStored.ClearSelected();
                this.lstStored.Items.Clear();
                for (int i = 0; i < num; i++)
                {
                    this.lstStored.Items.Add(reader.ReadString());
                }
                reader.Close();
                input.Close();
            }
            catch (Exception)
            {
                if (input != null)
                {
                    input.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        private void lstStored_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (((this.lstStored.Items.Count >= 1) && (this.lstStored.SelectedIndex >= 0)) && (GameEngine.Instance.CastleAttackerSetup != null))
            {
                GameEngine.Instance.CastleAttackerSetup.restoreAttackSetup((string) this.lstStored.SelectedItem);
            }
        }

        private void lstStored_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.lstStored.SelectedIndex != -1;
            this.btnRestore.Enabled = flag;
            this.btnDelete.Enabled = flag;
            this.btnRename.Enabled = flag;
            if (flag)
            {
                this.txtSelected.Text = (string) this.lstStored.SelectedItem;
            }
            else
            {
                this.txtSelected.Text = "";
            }
        }

        private void saveFormation()
        {
            if (GameEngine.Instance.CastleAttackerSetup != null)
            {
                GameEngine.Instance.CastleAttackerSetup.memoriseAttackSetup(this.saveName);
            }
        }

        private bool saveNames()
        {
            string str = GameEngine.getSettingsPath(true);
            string path = "StoredSetupNames.cas";
            FileStream output = null;
            BinaryWriter writer = null;
            path = str + @"\" + path;
            try
            {
                output = new FileStream(path, FileMode.Create);
                writer = new BinaryWriter(output);
                writer.Write(this.lstStored.Items.Count);
                foreach (object obj2 in this.lstStored.Items)
                {
                    writer.Write(obj2.ToString());
                }
                writer.Close();
                output.Close();
                return true;
            }
            catch (Exception)
            {
                try
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                    if (output != null)
                    {
                        output.Close();
                    }
                }
                catch (Exception)
                {
                }
                return false;
            }
        }

        public void setCreateText(string newText)
        {
            this.txtSaveName.Text = newText;
        }

        public void setSelectedText(string newText)
        {
            this.txtSelected.Text = newText;
        }

        private void txtSaveName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Enter))
            {
                this.btnSave_Click(sender, e);
            }
        }

        private void txtSaveName_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
            char keyChar = e.KeyChar;
            if (keyChar != '\b')
            {
                foreach (char ch2 in invalidFileNameChars)
                {
                    if (keyChar == ch2)
                    {
                        e.Handled = true;
                        break;
                    }
                }
            }
        }

        private void txtSaveName_TextChanged(object sender, EventArgs e)
        {
            if (this.txtSaveName.Text.Length == 0)
            {
                this.btnSave.Enabled = false;
            }
            else
            {
                string text = this.txtSaveName.Text;
                foreach (char ch in Path.GetInvalidFileNameChars())
                {
                    text = text.Replace(ch, ' ');
                }
                if (text != this.txtSaveName.Text)
                {
                    this.txtSaveName.Text = text;
                }
                this.btnSave.Enabled = true;
            }
        }
    }
}

