namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FactionNewForumPopup : MyFormBase
    {
        private BitmapButton btnCancel;
        private BitmapButton btnOK;
        private IContainer components;
        private Label lblTopic;
        private FactionNewForumPanel m_parent;
        private TextBox tbForumName;

        public FactionNewForumPopup()
        {
            this.InitializeComponent();
            this.btnOK.Enabled = false;
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("FactionNewForumPopup_cancel");
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("FactionNewForumPopup_ok");
            if (this.m_parent != null)
            {
                this.m_parent.createNewForum(this.tbForumName.Text);
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

        public void init(FactionNewForumPanel parent)
        {
            this.lblTopic.Text = SK.Text("FORUMS_Sub_Name", "Forum Sub Name");
            this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
            base.Title = this.Text = SK.Text("FORUMS_New_Sub_Forum", "New Sub Forum");
            this.m_parent = parent;
            this.btnOK.Enabled = false;
            this.tbForumName.Focus();
        }

        private void InitializeComponent()
        {
            this.tbForumName = new TextBox();
            this.lblTopic = new Label();
            this.btnCancel = new BitmapButton();
            this.btnOK = new BitmapButton();
            base.SuspendLayout();
            this.tbForumName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbForumName.Location = new Point(0xa2, 50);
            this.tbForumName.MaxLength = 0x63;
            this.tbForumName.Name = "tbForumName";
            this.tbForumName.Size = new Size(460, 20);
            this.tbForumName.TabIndex = 4;
            this.tbForumName.TextChanged += new EventHandler(this.tbForumName_TextChanged);
            this.tbForumName.KeyUp += new KeyEventHandler(this.tbForumName_KeyUp);
            this.lblTopic.AutoSize = true;
            this.lblTopic.BackColor = ARGBColors.Transparent;
            this.lblTopic.Location = new Point(7, 0x35);
            this.lblTopic.Name = "lblTopic";
            this.lblTopic.Size = new Size(0x59, 13);
            this.lblTopic.TabIndex = 5;
            this.lblTopic.Text = "Forum Sub Name";
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
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
            this.btnCancel.Location = new Point(0x1f2, 0x55);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OffsetPressedContent = true;
            this.btnCancel.Padding2 = 5;
            this.btnCancel.Size = new Size(0x7c, 0x1b);
            this.btnCancel.StretchImage = false;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextDropShadow = false;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnOK.BorderColor = ARGBColors.DarkBlue;
            this.btnOK.BorderDrawing = true;
            this.btnOK.FocusRectangleEnabled = false;
            this.btnOK.Image = null;
            this.btnOK.ImageBorderColor = ARGBColors.Chocolate;
            this.btnOK.ImageBorderEnabled = true;
            this.btnOK.ImageDropShadow = true;
            this.btnOK.ImageFocused = null;
            this.btnOK.ImageInactive = null;
            this.btnOK.ImageMouseOver = null;
            this.btnOK.ImageNormal = null;
            this.btnOK.ImagePressed = null;
            this.btnOK.InnerBorderColor = ARGBColors.LightGray;
            this.btnOK.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnOK.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnOK.Location = new Point(0x170, 0x55);
            this.btnOK.Name = "btnOK";
            this.btnOK.OffsetPressedContent = true;
            this.btnOK.Padding2 = 5;
            this.btnOK.Size = new Size(0x7c, 0x1b);
            this.btnOK.StretchImage = false;
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.TextDropShadow = false;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x282, 0x7f);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.lblTopic);
            base.Controls.Add(this.tbForumName);
            base.Icon = Resources.shk_icon;
            base.Name = "FactionNewForumPopup";
            base.ShowClose = true;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "New Sub Forum";
            base.Controls.SetChildIndex(this.tbForumName, 0);
            base.Controls.SetChildIndex(this.lblTopic, 0);
            base.Controls.SetChildIndex(this.btnCancel, 0);
            base.Controls.SetChildIndex(this.btnOK, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void setFocus()
        {
            this.tbForumName.Focus();
        }

        private void tbForumName_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue == 13) && (this.tbForumName.Text.Length > 0))
            {
                this.btnOK_Click(sender, e);
            }
        }

        private void tbForumName_TextChanged(object sender, EventArgs e)
        {
            if (this.tbForumName.Text.Length > 0)
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }
    }
}

