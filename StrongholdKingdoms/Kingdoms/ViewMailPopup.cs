namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ViewMailPopup : MyFormBase
    {
        private BitmapButton btnClose;
        private BitmapButton btnCopyClipboard;
        private BitmapButton btnCopySelected;
        private IContainer components;
        private Label label3;
        private Label lbDate;
        private Label lbDateValue;
        private Label lbFrom;
        private Label lblFromName;
        private MailScreen m_parent;
        private TextBox tbBody;
        private TextBox textBox2;

        public ViewMailPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.tbBody.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("MailUserBlockPopup_close");
            base.Close();
        }

        private void btnCopyClipboard_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(this.tbBody.Text);
            }
            catch (Exception)
            {
            }
        }

        private void btnCopySelected_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tbBody.SelectedText.Length > 0)
                {
                    Clipboard.SetText(this.tbBody.SelectedText);
                }
            }
            catch (Exception)
            {
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

        public void init(MailScreen parent, string header, string body, string from, string date)
        {
            body = body.Replace("\r\n", "\n");
            body = body.Replace("\n", "\r\n");
            this.m_parent = parent;
            base.Title = SK.Text("MailScreen_Mail", "Mail");
            this.tbBody.Text = body;
            this.textBox2.Text = header;
            this.lblFromName.Text = from;
            this.lbDateValue.Text = date;
            this.lbFrom.Text = SK.Text("MailScreen_From", "From") + " :";
            this.lbDate.Text = SK.Text("MailScreen_Date", "Date") + " :";
            this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
            this.btnCopySelected.Text = SK.Text("MailScreen_CopySelected", "Copy Selected");
            this.btnCopyClipboard.Text = SK.Text("MailScreen_CopyAll", "Copy All");
            this.tbBody.Focus();
        }

        private void InitializeComponent()
        {
            this.label3 = new Label();
            this.btnClose = new BitmapButton();
            this.tbBody = new TextBox();
            this.textBox2 = new TextBox();
            this.lbFrom = new Label();
            this.lblFromName = new Label();
            this.lbDate = new Label();
            this.lbDateValue = new Label();
            this.btnCopyClipboard = new BitmapButton();
            this.btnCopySelected = new BitmapButton();
            base.SuspendLayout();
            this.label3.AutoSize = true;
            this.label3.Location = new Point(7, 0x4c);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4f, 13);
            this.label3.TabIndex = 0x10;
            this.label3.Text = "Search Results";
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
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
            this.btnClose.Location = new Point(0x2a4, 0x1e7);
            this.btnClose.Name = "btnClose";
            this.btnClose.OffsetPressedContent = true;
            this.btnClose.Padding2 = 5;
            this.btnClose.Size = new Size(110, 0x1b);
            this.btnClose.StretchImage = false;
            this.btnClose.TabIndex = 0x11;
            this.btnClose.Text = "Close";
            this.btnClose.TextDropShadow = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.tbBody.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tbBody.BackColor = ARGBColors.White;
            this.tbBody.ForeColor = ARGBColors.Black;
            this.tbBody.Location = new Point(14, 0x52);
            this.tbBody.Multiline = true;
            this.tbBody.Name = "textBox1";
            this.tbBody.ReadOnly = true;
            this.tbBody.ScrollBars = ScrollBars.Vertical;
            this.tbBody.Size = new Size(0x304, 0x18f);
            this.tbBody.TabIndex = 0x12;
            this.textBox2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.textBox2.BackColor = ARGBColors.White;
            this.textBox2.ForeColor = ARGBColors.Black;
            this.textBox2.Location = new Point(14, 0x38);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(0x304, 20);
            this.textBox2.TabIndex = 0x13;
            this.lbFrom.AutoSize = true;
            this.lbFrom.BackColor = ARGBColors.Transparent;
            this.lbFrom.ForeColor = ARGBColors.White;
            this.lbFrom.Location = new Point(0x12, 0x27);
            this.lbFrom.Name = "lbFrom";
            this.lbFrom.Size = new Size(0x23, 13);
            this.lbFrom.TabIndex = 20;
            this.lbFrom.Text = "label1";
            this.lblFromName.AutoSize = true;
            this.lblFromName.BackColor = ARGBColors.Transparent;
            this.lblFromName.ForeColor = ARGBColors.White;
            this.lblFromName.Location = new Point(0x53, 0x27);
            this.lblFromName.Name = "lblFromName";
            this.lblFromName.Size = new Size(0x23, 13);
            this.lblFromName.TabIndex = 0x15;
            this.lblFromName.Text = "label1";
            this.lbDate.AutoSize = true;
            this.lbDate.BackColor = ARGBColors.Transparent;
            this.lbDate.ForeColor = ARGBColors.White;
            this.lbDate.Location = new Point(0x252, 0x27);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new Size(0x23, 13);
            this.lbDate.TabIndex = 0x16;
            this.lbDate.Text = "label1";
            this.lbDateValue.AutoSize = true;
            this.lbDateValue.BackColor = ARGBColors.Transparent;
            this.lbDateValue.ForeColor = ARGBColors.White;
            this.lbDateValue.Location = new Point(0x290, 0x27);
            this.lbDateValue.Name = "lbDateValue";
            this.lbDateValue.Size = new Size(0x23, 13);
            this.lbDateValue.TabIndex = 0x17;
            this.lbDateValue.Text = "label1";
            this.btnCopyClipboard.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCopyClipboard.BorderColor = ARGBColors.DarkBlue;
            this.btnCopyClipboard.BorderDrawing = true;
            this.btnCopyClipboard.FocusRectangleEnabled = false;
            this.btnCopyClipboard.Image = null;
            this.btnCopyClipboard.ImageBorderColor = ARGBColors.Chocolate;
            this.btnCopyClipboard.ImageBorderEnabled = true;
            this.btnCopyClipboard.ImageDropShadow = true;
            this.btnCopyClipboard.ImageFocused = null;
            this.btnCopyClipboard.ImageInactive = null;
            this.btnCopyClipboard.ImageMouseOver = null;
            this.btnCopyClipboard.ImageNormal = null;
            this.btnCopyClipboard.ImagePressed = null;
            this.btnCopyClipboard.InnerBorderColor = ARGBColors.LightGray;
            this.btnCopyClipboard.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnCopyClipboard.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnCopyClipboard.Location = new Point(14, 0x1e7);
            this.btnCopyClipboard.Name = "btnCopyClipboard";
            this.btnCopyClipboard.OffsetPressedContent = true;
            this.btnCopyClipboard.Padding2 = 5;
            this.btnCopyClipboard.Size = new Size(0xd8, 0x1b);
            this.btnCopyClipboard.StretchImage = false;
            this.btnCopyClipboard.TabIndex = 0x18;
            this.btnCopyClipboard.Text = "Copy All to Clipboard";
            this.btnCopyClipboard.TextDropShadow = false;
            this.btnCopyClipboard.UseVisualStyleBackColor = true;
            this.btnCopyClipboard.Click += new EventHandler(this.btnCopyClipboard_Click);
            this.btnCopySelected.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCopySelected.BorderColor = ARGBColors.DarkBlue;
            this.btnCopySelected.BorderDrawing = true;
            this.btnCopySelected.FocusRectangleEnabled = false;
            this.btnCopySelected.Image = null;
            this.btnCopySelected.ImageBorderColor = ARGBColors.Chocolate;
            this.btnCopySelected.ImageBorderEnabled = true;
            this.btnCopySelected.ImageDropShadow = true;
            this.btnCopySelected.ImageFocused = null;
            this.btnCopySelected.ImageInactive = null;
            this.btnCopySelected.ImageMouseOver = null;
            this.btnCopySelected.ImageNormal = null;
            this.btnCopySelected.ImagePressed = null;
            this.btnCopySelected.InnerBorderColor = ARGBColors.LightGray;
            this.btnCopySelected.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnCopySelected.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnCopySelected.Location = new Point(0xec, 0x1e7);
            this.btnCopySelected.Name = "btnCopySelected";
            this.btnCopySelected.OffsetPressedContent = true;
            this.btnCopySelected.Padding2 = 5;
            this.btnCopySelected.Size = new Size(0xd8, 0x1b);
            this.btnCopySelected.StretchImage = false;
            this.btnCopySelected.TabIndex = 0x19;
            this.btnCopySelected.Text = "Copy Selected to Clipboard";
            this.btnCopySelected.TextDropShadow = false;
            this.btnCopySelected.UseVisualStyleBackColor = true;
            this.btnCopySelected.Click += new EventHandler(this.btnCopySelected_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x31e, 0x20e);
            base.Controls.Add(this.btnCopySelected);
            base.Controls.Add(this.btnCopyClipboard);
            base.Controls.Add(this.lbDateValue);
            base.Controls.Add(this.lbDate);
            base.Controls.Add(this.lblFromName);
            base.Controls.Add(this.lbFrom);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.tbBody);
            base.Controls.Add(this.btnClose);
            base.Icon = Resources.shk_icon;
            base.Name = "ViewMailPopup";
            base.ShowClose = true;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Add Users";
            base.TopMost = true;
            base.Controls.SetChildIndex(this.btnClose, 0);
            base.Controls.SetChildIndex(this.tbBody, 0);
            base.Controls.SetChildIndex(this.textBox2, 0);
            base.Controls.SetChildIndex(this.lbFrom, 0);
            base.Controls.SetChildIndex(this.lblFromName, 0);
            base.Controls.SetChildIndex(this.lbDate, 0);
            base.Controls.SetChildIndex(this.lbDateValue, 0);
            base.Controls.SetChildIndex(this.btnCopyClipboard, 0);
            base.Controls.SetChildIndex(this.btnCopySelected, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

