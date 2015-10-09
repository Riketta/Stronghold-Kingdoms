namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NameChangeHistoryPopup : MyFormBase
    {
        private BitmapButton btnOK;
        private IContainer components;
        private ListBox listBox1;

        public NameChangeHistoryPopup()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
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

        public void importData(string[] names, int parishID)
        {
            this.Text = base.Title = "Parish Name History : " + parishID.ToString();
            for (int i = 0; i < names.Length; i += 2)
            {
                HistoryItem item = new HistoryItem {
                    name = names[i],
                    userguid = names[i + 1]
                };
                this.listBox1.Items.Add(item);
            }
        }

        private void InitializeComponent()
        {
            this.listBox1 = new ListBox();
            this.btnOK = new BitmapButton();
            base.SuspendLayout();
            this.listBox1.Font = new Font("Lucida Console", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 11;
            this.listBox1.Location = new Point(14, 0x35);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(680, 290);
            this.listBox1.TabIndex = 13;
            this.btnOK.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
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
            this.btnOK.Location = new Point(0x267, 0x171);
            this.btnOK.Name = "btnOK";
            this.btnOK.OffsetPressedContent = true;
            this.btnOK.Padding2 = 5;
            this.btnOK.Size = new Size(0x4f, 20);
            this.btnOK.StretchImage = false;
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "OK";
            this.btnOK.TextDropShadow = false;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x2c2, 0x191);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.listBox1);
            base.Name = "NameChangeHistoryPopup";
            base.ShowClose = true;
            this.Text = "NameChangeHistoryPopup";
            base.Controls.SetChildIndex(this.listBox1, 0);
            base.Controls.SetChildIndex(this.btnOK, 0);
            base.ResumeLayout(false);
        }

        private class HistoryItem
        {
            public string name = "";
            public string userguid = "";

            public override string ToString()
            {
                return string.Format("{0,-50}{1, -32}", this.name, this.userguid);
            }
        }
    }
}

