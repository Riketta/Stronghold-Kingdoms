namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MailUserBlockPopup : MyFormBase
    {
        private List<string> blockedNames = new List<string>();
        private BitmapButton btnClose;
        private BitmapButton btnRemoveBlock;
        private CheckBox cbAggressive;
        private IContainer components;
        private Label label3;
        private ListBox listBoxSearch;
        private MailScreen m_parent;

        public MailUserBlockPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            CustomTooltipManager.addTooltipToSystemControl(this.cbAggressive, 0x1f8);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("MailUserBlockPopup_close");
            base.Close();
        }

        private void btnRemoveBlock_Click(object sender, EventArgs e)
        {
            if (this.listBoxSearch.SelectedIndex >= 0)
            {
                GameEngine.Instance.playInterfaceSound("MailUserBlockPopup_remove");
                string item = (string) this.listBoxSearch.Items[this.listBoxSearch.SelectedIndex];
                this.blockedNames.Remove(item);
                this.m_parent.updateBlockedList(this.blockedNames);
                this.listBoxSearch.Items.Clear();
                foreach (string str2 in this.blockedNames)
                {
                    this.listBoxSearch.Items.Add(str2);
                }
                this.btnRemoveBlock.Enabled = false;
            }
        }

        private void cbAggressive_CheckedChanged(object sender, EventArgs e)
        {
            this.m_parent.AggressiveBlocking = this.cbAggressive.Checked;
            this.m_parent.saveBlockedList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(MailScreen parent, string newBlockedUser)
        {
            this.m_parent = parent;
            this.blockedNames = this.m_parent.getBlockedList();
            if ((newBlockedUser.Length > 0) && !this.blockedNames.Contains(newBlockedUser))
            {
                this.blockedNames.Add(newBlockedUser);
                this.m_parent.updateBlockedList(this.blockedNames);
            }
            this.listBoxSearch.Items.Clear();
            foreach (string str in this.blockedNames)
            {
                this.listBoxSearch.Items.Add(str);
            }
            base.Title = SK.Text("MailBlock_title", "Manage Blocked Mail Users");
            this.btnRemoveBlock.Text = SK.Text("MailBlock_remove", "Remove");
            this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
            this.cbAggressive.Text = SK.Text("MailBlock", "Aggressive Blocking");
            this.cbAggressive.Checked = this.m_parent.AggressiveBlocking;
            this.btnRemoveBlock.Enabled = false;
        }

        private void InitializeComponent()
        {
            this.listBoxSearch = new ListBox();
            this.label3 = new Label();
            this.btnClose = new BitmapButton();
            this.btnRemoveBlock = new BitmapButton();
            this.cbAggressive = new CheckBox();
            base.SuspendLayout();
            this.listBoxSearch.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.listBoxSearch.BackColor = ARGBColors.White;
            this.listBoxSearch.ForeColor = ARGBColors.Black;
            this.listBoxSearch.FormattingEnabled = true;
            this.listBoxSearch.Location = new Point(14, 0x30);
            this.listBoxSearch.Name = "listBoxSearch";
            this.listBoxSearch.Size = new Size(0x16e, 0xfb);
            this.listBoxSearch.TabIndex = 11;
            this.listBoxSearch.SelectedIndexChanged += new EventHandler(this.listBoxSearch_SelectedIndexChanged);
            this.listBoxSearch.DoubleClick += new EventHandler(this.listBoxSearch_DoubleClick);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(7, 0x4c);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4f, 13);
            this.label3.TabIndex = 0x10;
            this.label3.Text = "Search Results";
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnClose.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnClose.BorderDrawing = true;
            this.btnClose.FocusRectangleEnabled = false;
            this.btnClose.Image = null;
            this.btnClose.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnClose.ImageBorderEnabled = true;
            this.btnClose.ImageDropShadow = true;
            this.btnClose.ImageFocused = null;
            this.btnClose.ImageInactive = null;
            this.btnClose.ImageMouseOver = null;
            this.btnClose.ImageNormal = null;
            this.btnClose.ImagePressed = null;
            this.btnClose.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnClose.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnClose.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnClose.Location = new Point(0x10f, 0x171);
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
            this.btnRemoveBlock.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnRemoveBlock.BorderDrawing = true;
            this.btnRemoveBlock.FocusRectangleEnabled = false;
            this.btnRemoveBlock.Image = null;
            this.btnRemoveBlock.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnRemoveBlock.ImageBorderEnabled = true;
            this.btnRemoveBlock.ImageDropShadow = true;
            this.btnRemoveBlock.ImageFocused = null;
            this.btnRemoveBlock.ImageInactive = null;
            this.btnRemoveBlock.ImageMouseOver = null;
            this.btnRemoveBlock.ImageNormal = null;
            this.btnRemoveBlock.ImagePressed = null;
            this.btnRemoveBlock.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnRemoveBlock.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnRemoveBlock.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnRemoveBlock.Location = new Point(0x74, 0x131);
            this.btnRemoveBlock.Name = "btnRemoveBlock";
            this.btnRemoveBlock.OffsetPressedContent = true;
            this.btnRemoveBlock.Padding2 = 5;
            this.btnRemoveBlock.Size = new Size(0xa1, 0x1b);
            this.btnRemoveBlock.StretchImage = false;
            this.btnRemoveBlock.TabIndex = 0x17;
            this.btnRemoveBlock.Text = "Remove Block";
            this.btnRemoveBlock.TextDropShadow = false;
            this.btnRemoveBlock.UseVisualStyleBackColor = true;
            this.btnRemoveBlock.Click += new EventHandler(this.btnRemoveBlock_Click);
            this.cbAggressive.AutoSize = true;
            this.cbAggressive.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.cbAggressive.ForeColor = ARGBColors.Black;
            this.cbAggressive.Location = new Point(0x3b, 0x155);
            this.cbAggressive.Name = "cbAggressive";
            this.cbAggressive.Size = new Size(80, 0x11);
            this.cbAggressive.TabIndex = 0x18;
            this.cbAggressive.Text = "checkBox1";
            this.cbAggressive.UseVisualStyleBackColor = false;
            this.cbAggressive.CheckedChanged += new EventHandler(this.cbAggressive_CheckedChanged);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x189, 0x198);
            base.Controls.Add(this.cbAggressive);
            base.Controls.Add(this.btnRemoveBlock);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.listBoxSearch);
            base.Icon = Resources.shk_icon;
            base.Name = "MailUserBlockPopup";
            base.ShowBar = true;
            base.ShowClose = true;
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Add Users";
            base.TopMost = true;
            base.Controls.SetChildIndex(this.listBoxSearch, 0);
            base.Controls.SetChildIndex(this.btnClose, 0);
            base.Controls.SetChildIndex(this.btnRemoveBlock, 0);
            base.Controls.SetChildIndex(this.cbAggressive, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listBoxSearch_DoubleClick(object sender, EventArgs e)
        {
            int selectedIndex = this.listBoxSearch.SelectedIndex;
        }

        private void listBoxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxSearch.SelectedIndex >= 0)
            {
                this.btnRemoveBlock.Enabled = true;
            }
        }

        public static void ShowPopup(MailScreen parent, string newBlockedUser)
        {
            Timer timer = new Timer {
                Interval = 30
            };
            timer.Tick += new EventHandler(MailUserBlockPopup.tooltipCallbackFunction);
            timer.Tag = "0";
            timer.Enabled = true;
            MailUserBlockPopup popup = new MailUserBlockPopup();
            popup.init(parent, newBlockedUser);
            popup.ShowDialog(InterfaceMgr.Instance.ParentForm);
            popup.Dispose();
            timer.Stop();
            timer.Dispose();
        }

        private static void tooltipCallbackFunction(object sender, EventArgs ee)
        {
            InterfaceMgr.Instance.runTooltips();
        }
    }
}

