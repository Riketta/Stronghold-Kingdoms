namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FactionNewPostPopup : MyFormBase
    {
        private BitmapButton btnCancel;
        private BitmapButton btnOK;
        private IContainer components;
        private Label lblTopic;
        private IForumReplyParent m_parent;
        private TextBox tbHeading;
        private TextBox tbMainText;
        private long ThreadID = -1L;

        public FactionNewPostPopup()
        {
            this.InitializeComponent();
            this.btnOK.Enabled = false;
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("FactionNewPostPopup_cancel");
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("FactionNewPostPopup_ok");
            if (this.m_parent != null)
            {
                this.m_parent.newPost(this.ThreadID, this.tbMainText.Text);
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

        public void init(long threadID, IForumReplyParent parent, string heading)
        {
            this.lblTopic.Text = SK.Text("FORUMS_Topic", "Topic");
            this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
            base.Title = this.Text = SK.Text("FORUMS_New_Post", "New Post");
            this.ThreadID = threadID;
            this.m_parent = parent;
            this.tbHeading.Text = heading;
        }

        private void InitializeComponent()
        {
            this.tbHeading = new TextBox();
            this.lblTopic = new Label();
            this.tbMainText = new TextBox();
            this.btnCancel = new BitmapButton();
            this.btnOK = new BitmapButton();
            base.SuspendLayout();
            this.tbHeading.BackColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.tbHeading.Location = new Point(0x7a, 0x26);
            this.tbHeading.MaxLength = 0x63;
            this.tbHeading.Name = "tbHeading";
            this.tbHeading.ReadOnly = true;
            this.tbHeading.Size = new Size(500, 20);
            this.tbHeading.TabIndex = 4;
            this.lblTopic.AutoSize = true;
            this.lblTopic.BackColor = ARGBColors.Transparent;
            this.lblTopic.Location = new Point(12, 0x29);
            this.lblTopic.Name = "lblTopic";
            this.lblTopic.Size = new Size(0x22, 13);
            this.lblTopic.TabIndex = 5;
            this.lblTopic.Text = "Topic";
            this.tbMainText.Location = new Point(15, 0x40);
            this.tbMainText.Multiline = true;
            this.tbMainText.Name = "tbMainText";
            this.tbMainText.Size = new Size(0x25f, 0x123);
            this.tbMainText.TabIndex = 1;
            this.tbMainText.TextChanged += new EventHandler(this.tbMainText_TextChanged);
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
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.TextDropShadow = false;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x282, 0x197);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.lblTopic);
            base.Controls.Add(this.tbHeading);
            base.Controls.Add(this.tbMainText);
            base.Icon = Resources.shk_icon;
            base.Name = "FactionNewPostPopup";
            base.ShowClose = true;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "New post";
            base.Controls.SetChildIndex(this.tbMainText, 0);
            base.Controls.SetChildIndex(this.tbHeading, 0);
            base.Controls.SetChildIndex(this.lblTopic, 0);
            base.Controls.SetChildIndex(this.btnCancel, 0);
            base.Controls.SetChildIndex(this.btnOK, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void tbMainText_TextChanged(object sender, EventArgs e)
        {
            if (this.tbMainText.Text.Length > 0)
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

