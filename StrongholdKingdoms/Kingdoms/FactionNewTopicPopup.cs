namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FactionNewTopicPopup : MyFormBase
    {
        private BitmapButton btnCancel;
        private BitmapButton btnOK;
        private IContainer components;
        private long ForumID = -1L;
        private Label lblTopic;
        private IForumPostParent m_parent;
        private TextBox tbHeading;
        private TextBox tbMainText;

        public FactionNewTopicPopup()
        {
            this.InitializeComponent();
            this.btnOK.Enabled = false;
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("FactionNewTopicPopup_cancel");
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("FactionNewTopicPopup_ok");
            if (this.m_parent != null)
            {
                this.m_parent.newTopic(this.ForumID, this.tbHeading.Text, this.tbMainText.Text);
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

        public void init(long forumID, IForumPostParent parent)
        {
            this.lblTopic.Text = SK.Text("FORUMS_Topic", "Topic");
            this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
            base.Title = this.Text = SK.Text("FORUMS_New_Topic", "New Topic");
            this.ForumID = forumID;
            this.m_parent = parent;
        }

        private void InitializeComponent()
        {
            this.tbHeading = new TextBox();
            this.lblTopic = new Label();
            this.tbMainText = new TextBox();
            this.btnCancel = new BitmapButton();
            this.btnOK = new BitmapButton();
            base.SuspendLayout();
            this.tbHeading.Location = new Point(0x7a, 0x26);
            this.tbHeading.MaxLength = 0x63;
            this.tbHeading.Name = "tbHeading";
            this.tbHeading.Size = new Size(500, 20);
            this.tbHeading.TabIndex = 0;
            this.tbHeading.TextChanged += new EventHandler(this.tbHeading_TextChanged);
            this.lblTopic.AutoSize = true;
            this.lblTopic.BackColor = ARGBColors.Transparent;
            this.lblTopic.Location = new Point(12, 0x29);
            this.lblTopic.Name = "lblTopic";
            this.lblTopic.Size = new Size(0x22, 13);
            this.lblTopic.TabIndex = 1;
            this.lblTopic.Text = "Topic";
            this.tbMainText.Location = new Point(15, 0x40);
            this.tbMainText.Multiline = true;
            this.tbMainText.Name = "tbMainText";
            this.tbMainText.Size = new Size(0x25f, 0x123);
            this.tbMainText.TabIndex = 2;
            this.tbMainText.TextChanged += new EventHandler(this.tbMainText_TextChanged);
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
            this.btnCancel.Location = new Point(0x1f2, 0x170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OffsetPressedContent = true;
            this.btnCancel.Padding2 = 5;
            this.btnCancel.Size = new Size(0x7c, 0x1d);
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
            this.btnOK.Location = new Point(0x170, 0x170);
            this.btnOK.Name = "btnOK";
            this.btnOK.OffsetPressedContent = true;
            this.btnOK.Padding2 = 5;
            this.btnOK.Size = new Size(0x7c, 0x1d);
            this.btnOK.StretchImage = false;
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.TextDropShadow = false;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x282, 0x197);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.tbMainText);
            base.Controls.Add(this.lblTopic);
            base.Controls.Add(this.tbHeading);
            base.Icon = Resources.shk_icon;
            base.Name = "FactionNewTopicPopup";
            base.ShowClose = true;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "New Topic";
            base.Controls.SetChildIndex(this.tbHeading, 0);
            base.Controls.SetChildIndex(this.lblTopic, 0);
            base.Controls.SetChildIndex(this.tbMainText, 0);
            base.Controls.SetChildIndex(this.btnCancel, 0);
            base.Controls.SetChildIndex(this.btnOK, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void tbHeading_TextChanged(object sender, EventArgs e)
        {
            if ((this.tbHeading.Text.Length > 0) && (this.tbMainText.Text.Length > 0))
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }

        private void tbMainText_TextChanged(object sender, EventArgs e)
        {
            if ((this.tbHeading.Text.Length > 0) && (this.tbMainText.Text.Length > 0))
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

