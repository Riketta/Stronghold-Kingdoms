namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class MailUserPopup : MyFormBase
    {
        private BitmapButton btnAdd;
        private BitmapButton btnAddToFavourites;
        private BitmapButton btnCancel;
        private BitmapButton btnClose;
        private BitmapButton btnRemove;
        private BitmapButton btnRemoveFromFavourites;
        private BitmapButton btnSearch;
        private IContainer components;
        private bool forwardPopup;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private double lastUpdateTime;
        private ListBox listBoxFavourites;
        private ListBox listBoxRecent;
        private ListBox listBoxRecipients;
        private ListBox listBoxSearch;
        private System.Threading.Timer m_searchTimer;
        private IMailUserInterface parentPopup;
        private object searchTimerLock = false;
        private TextBox textBoxNewRecipient;

        public MailUserPopup()
        {
            this.InitializeComponent();
            this.label4.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.m_searchTimer = new System.Threading.Timer(new TimerCallback(this.timerCallbackFunction), null, 1, 500);
            RemoteServices.Instance.set_GetMailUserSearch_UserCallBack(new RemoteServices.GetMailUserSearch_UserCallBack(this.getMailUserSearchCallback));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string str = this.getSelectedName();
            if ((str != "") && !this.listBoxRecipients.Items.Contains(str))
            {
                GameEngine.Instance.playInterfaceSound("MailUserPopup_add");
                this.parentPopup.addRecipient(str);
                if (!this.listBoxRecipients.Items.Contains(str))
                {
                    this.listBoxRecipients.Items.Add(str);
                }
                this.btnAdd.Enabled = false;
                this.btnRemove.Enabled = true;
                this.btnClose.Enabled = true;
            }
        }

        private void btnAddToFavourites_Click(object sender, EventArgs e)
        {
            string str = this.getSelectedName();
            if ((str != "") && !this.listBoxFavourites.Items.Contains(str))
            {
                GameEngine.Instance.playInterfaceSound("MailUserPopup_add_to_favourites");
                if (!this.listBoxFavourites.Items.Contains(str))
                {
                    this.listBoxFavourites.Items.Add(str);
                    RemoteServices.Instance.AddUserToFavourites(str);
                    GenericReportPanelBasic.ForceHistoryRefresh();
                    this.btnAddToFavourites.Enabled = false;
                    this.btnRemoveFromFavourites.Enabled = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("MailUserPopup_close");
            this.m_searchTimer.Dispose();
            this.parentPopup.popupClosed(true);
            base.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string str = this.getSelectedName();
            if ((str != "") && this.listBoxRecipients.Items.Contains(str))
            {
                this.btnAddToFavourites.Enabled = this.btnRemoveFromFavourites.Enabled = this.btnAdd.Enabled = this.listBoxRecipients.SelectedIndex == -1;
                this.btnRemove.Enabled = false;
                GameEngine.Instance.playInterfaceSound("MailUserPopup_add");
                this.listBoxRecipients.Items.Remove(str);
                if (this.forwardPopup && (this.listBoxRecipients.Items.Count <= 0))
                {
                    this.btnClose.Enabled = false;
                }
            }
        }

        private void btnRemoveFromFavourites_Click(object sender, EventArgs e)
        {
            string userName = this.getSelectedName();
            if (userName != "")
            {
                this.btnAddToFavourites.Enabled = this.btnRemove.Enabled = this.btnAdd.Enabled = this.listBoxFavourites.SelectedIndex == -1;
                this.btnRemoveFromFavourites.Enabled = false;
                GameEngine.Instance.playInterfaceSound("MailUserPopup_add_to_favourites");
                RemoteServices.Instance.RemoveUserFromFavourites(userName);
                this.listBoxFavourites.Items.Remove(userName);
                GenericReportPanelBasic.ForceHistoryRefresh();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.lastUpdateTime = 0.0;
            if (this.textBoxNewRecipient.Text.Length > 0)
            {
                GameEngine.Instance.playInterfaceSound("MailUserPopup_search");
                RemoteServices.Instance.GetMailUserSearch(this.textBoxNewRecipient.Text);
                if (this.listBoxSearch.SelectedIndex != -1)
                {
                    this.btnAdd.Enabled = this.btnRemove.Enabled = this.btnAddToFavourites.Enabled = this.btnRemoveFromFavourites.Enabled = false;
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

        public void getMailUserSearchCallback(GetMailUserSearch_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.listBoxSearch.Items.Clear();
                if (returnData.mailUsersSearch != null)
                {
                    foreach (string str in returnData.mailUsersSearch)
                    {
                        this.listBoxSearch.Items.Add(str);
                    }
                }
            }
        }

        private string getSelectedName()
        {
            if (this.listBoxSearch.SelectedIndex != -1)
            {
                return this.listBoxSearch.SelectedItem.ToString();
            }
            if (this.listBoxRecent.SelectedIndex != -1)
            {
                return this.listBoxRecent.SelectedItem.ToString();
            }
            if (this.listBoxRecipients.SelectedIndex != -1)
            {
                return this.listBoxRecipients.SelectedItem.ToString();
            }
            if (this.listBoxFavourites.SelectedIndex != -1)
            {
                return this.listBoxFavourites.SelectedItem.ToString();
            }
            return "";
        }

        private void InitializeComponent()
        {
            this.textBoxNewRecipient = new TextBox();
            this.btnAdd = new BitmapButton();
            this.listBoxSearch = new ListBox();
            this.listBoxRecent = new ListBox();
            this.listBoxFavourites = new ListBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.btnClose = new BitmapButton();
            this.label4 = new Label();
            this.listBoxRecipients = new ListBox();
            this.btnAddToFavourites = new BitmapButton();
            this.btnSearch = new BitmapButton();
            this.btnCancel = new BitmapButton();
            this.btnRemoveFromFavourites = new BitmapButton();
            this.label5 = new Label();
            this.btnRemove = new BitmapButton();
            base.SuspendLayout();
            this.textBoxNewRecipient.BackColor = ARGBColors.White;
            this.textBoxNewRecipient.ForeColor = ARGBColors.Black;
            this.textBoxNewRecipient.Location = new Point(0xd7, 0x42);
            this.textBoxNewRecipient.Name = "textBoxNewRecipient";
            this.textBoxNewRecipient.Size = new Size(160, 20);
            this.textBoxNewRecipient.TabIndex = 10;
            this.textBoxNewRecipient.KeyUp += new KeyEventHandler(this.textBoxNewRecipient_KeyUp);
            this.textBoxNewRecipient.KeyPress += new KeyPressEventHandler(this.textBoxNewRecipient_KeyPress);
            this.btnAdd.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnAdd.BorderDrawing = true;
            this.btnAdd.FocusRectangleEnabled = false;
            this.btnAdd.Image = null;
            this.btnAdd.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnAdd.ImageBorderEnabled = true;
            this.btnAdd.ImageDropShadow = true;
            this.btnAdd.ImageFocused = null;
            this.btnAdd.ImageInactive = null;
            this.btnAdd.ImageMouseOver = null;
            this.btnAdd.ImageNormal = null;
            this.btnAdd.ImagePressed = null;
            this.btnAdd.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnAdd.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnAdd.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnAdd.Location = new Point(12, 0x143);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.OffsetPressedContent = true;
            this.btnAdd.Padding2 = 5;
            this.btnAdd.Size = new Size(160, 0x1b);
            this.btnAdd.StretchImage = false;
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextDropShadow = false;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.listBoxSearch.FormattingEnabled = true;
            this.listBoxSearch.Location = new Point(0xd7, 0x83);
            this.listBoxSearch.Name = "listBoxSearch";
            this.listBoxSearch.Size = new Size(160, 0xba);
            this.listBoxSearch.TabIndex = 11;
            this.listBoxSearch.SelectedIndexChanged += new EventHandler(this.listBoxSearch_SelectedIndexChanged);
            this.listBoxSearch.DoubleClick += new EventHandler(this.listBoxSearch_DoubleClick);
            this.listBoxRecent.BackColor = ARGBColors.White;
            this.listBoxRecent.ForeColor = ARGBColors.Black;
            this.listBoxRecent.FormattingEnabled = true;
            this.listBoxRecent.Location = new Point(0x223, 0x42);
            this.listBoxRecent.Name = "listBoxRecent";
            this.listBoxRecent.Size = new Size(160, 0xfb);
            this.listBoxRecent.TabIndex = 12;
            this.listBoxRecent.SelectedIndexChanged += new EventHandler(this.listBoxRecent_SelectedIndexChanged);
            this.listBoxRecent.DoubleClick += new EventHandler(this.listBoxRecent_DoubleClick);
            this.listBoxFavourites.BackColor = ARGBColors.White;
            this.listBoxFavourites.ForeColor = ARGBColors.Black;
            this.listBoxFavourites.FormattingEnabled = true;
            this.listBoxFavourites.Location = new Point(0x17d, 0x42);
            this.listBoxFavourites.Name = "listBoxFavourites";
            this.listBoxFavourites.Size = new Size(160, 0xfb);
            this.listBoxFavourites.TabIndex = 13;
            this.listBoxFavourites.SelectedIndexChanged += new EventHandler(this.listBoxFavourites_SelectedIndexChanged);
            this.listBoxFavourites.DoubleClick += new EventHandler(this.listBoxFavourites_DoubleClick);
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label1.Location = new Point(0x220, 50);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2a, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Recent";
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label2.Location = new Point(0x17a, 50);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x38, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Favourites";
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
            this.btnClose.Location = new Point(0x223, 0x143);
            this.btnClose.Name = "btnClose";
            this.btnClose.OffsetPressedContent = true;
            this.btnClose.Padding2 = 5;
            this.btnClose.Size = new Size(160, 0x1b);
            this.btnClose.StretchImage = false;
            this.btnClose.TabIndex = 0x11;
            this.btnClose.Text = "Close";
            this.btnClose.TextDropShadow = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.label4.AutoSize = true;
            this.label4.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label4.Location = new Point(9, 0x2e);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x43, 13);
            this.label4.TabIndex = 0x13;
            this.label4.Text = "Recipients";
            this.listBoxRecipients.BackColor = ARGBColors.White;
            this.listBoxRecipients.FormattingEnabled = true;
            this.listBoxRecipients.Location = new Point(12, 0x42);
            this.listBoxRecipients.Name = "listBoxRecipients";
            this.listBoxRecipients.Size = new Size(160, 0xfb);
            this.listBoxRecipients.TabIndex = 0x12;
            this.listBoxRecipients.SelectedIndexChanged += new EventHandler(this.listBoxRecipients_SelectedIndexChanged);
            this.listBoxRecipients.DoubleClick += new EventHandler(this.listBoxRecipients_DoubleClick);
            this.btnAddToFavourites.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnAddToFavourites.BorderDrawing = true;
            this.btnAddToFavourites.FocusRectangleEnabled = false;
            this.btnAddToFavourites.Image = null;
            this.btnAddToFavourites.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnAddToFavourites.ImageBorderEnabled = true;
            this.btnAddToFavourites.ImageDropShadow = true;
            this.btnAddToFavourites.ImageFocused = null;
            this.btnAddToFavourites.ImageInactive = null;
            this.btnAddToFavourites.ImageMouseOver = null;
            this.btnAddToFavourites.ImageNormal = null;
            this.btnAddToFavourites.ImagePressed = null;
            this.btnAddToFavourites.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnAddToFavourites.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnAddToFavourites.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnAddToFavourites.Location = new Point(0x17d, 0x143);
            this.btnAddToFavourites.Name = "btnAddToFavourites";
            this.btnAddToFavourites.OffsetPressedContent = true;
            this.btnAddToFavourites.Padding2 = 5;
            this.btnAddToFavourites.Size = new Size(160, 0x1b);
            this.btnAddToFavourites.StretchImage = false;
            this.btnAddToFavourites.TabIndex = 20;
            this.btnAddToFavourites.Text = "Add to Favourites";
            this.btnAddToFavourites.TextDropShadow = false;
            this.btnAddToFavourites.UseVisualStyleBackColor = true;
            this.btnAddToFavourites.Click += new EventHandler(this.btnAddToFavourites_Click);
            this.btnSearch.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnSearch.BorderDrawing = true;
            this.btnSearch.FocusRectangleEnabled = false;
            this.btnSearch.Image = null;
            this.btnSearch.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnSearch.ImageBorderEnabled = true;
            this.btnSearch.ImageDropShadow = true;
            this.btnSearch.ImageFocused = null;
            this.btnSearch.ImageInactive = null;
            this.btnSearch.ImageMouseOver = null;
            this.btnSearch.ImageNormal = null;
            this.btnSearch.ImagePressed = null;
            this.btnSearch.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnSearch.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnSearch.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnSearch.Location = new Point(0xd7, 0x5c);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.OffsetPressedContent = true;
            this.btnSearch.Padding2 = 5;
            this.btnSearch.Size = new Size(160, 0x1b);
            this.btnSearch.StretchImage = false;
            this.btnSearch.TabIndex = 0x15;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextDropShadow = false;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
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
            this.btnCancel.Location = new Point(0x223, 0x164);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OffsetPressedContent = true;
            this.btnCancel.Padding2 = 5;
            this.btnCancel.Size = new Size(0x9f, 0x1b);
            this.btnCancel.StretchImage = false;
            this.btnCancel.TabIndex = 0x16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextDropShadow = false;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnRemoveFromFavourites.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnRemoveFromFavourites.BorderDrawing = true;
            this.btnRemoveFromFavourites.FocusRectangleEnabled = false;
            this.btnRemoveFromFavourites.Image = null;
            this.btnRemoveFromFavourites.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnRemoveFromFavourites.ImageBorderEnabled = true;
            this.btnRemoveFromFavourites.ImageDropShadow = true;
            this.btnRemoveFromFavourites.ImageFocused = null;
            this.btnRemoveFromFavourites.ImageInactive = null;
            this.btnRemoveFromFavourites.ImageMouseOver = null;
            this.btnRemoveFromFavourites.ImageNormal = null;
            this.btnRemoveFromFavourites.ImagePressed = null;
            this.btnRemoveFromFavourites.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnRemoveFromFavourites.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnRemoveFromFavourites.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnRemoveFromFavourites.Location = new Point(0x17d, 0x164);
            this.btnRemoveFromFavourites.Name = "btnRemoveFromFavourites";
            this.btnRemoveFromFavourites.OffsetPressedContent = true;
            this.btnRemoveFromFavourites.Padding2 = 5;
            this.btnRemoveFromFavourites.Size = new Size(160, 0x1b);
            this.btnRemoveFromFavourites.StretchImage = false;
            this.btnRemoveFromFavourites.TabIndex = 0x17;
            this.btnRemoveFromFavourites.Text = "Remove from Favourites";
            this.btnRemoveFromFavourites.TextDropShadow = false;
            this.btnRemoveFromFavourites.UseVisualStyleBackColor = true;
            this.btnRemoveFromFavourites.Click += new EventHandler(this.btnRemoveFromFavourites_Click);
            this.label5.AutoSize = true;
            this.label5.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label5.Location = new Point(0xd4, 50);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x77, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Search for Player Name";
            this.btnRemove.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnRemove.BorderDrawing = true;
            this.btnRemove.FocusRectangleEnabled = false;
            this.btnRemove.Image = null;
            this.btnRemove.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnRemove.ImageBorderEnabled = true;
            this.btnRemove.ImageDropShadow = true;
            this.btnRemove.ImageFocused = null;
            this.btnRemove.ImageInactive = null;
            this.btnRemove.ImageMouseOver = null;
            this.btnRemove.ImageNormal = null;
            this.btnRemove.ImagePressed = null;
            this.btnRemove.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnRemove.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnRemove.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnRemove.Location = new Point(12, 0x164);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.OffsetPressedContent = true;
            this.btnRemove.Padding2 = 5;
            this.btnRemove.Size = new Size(160, 0x1b);
            this.btnRemove.StretchImage = false;
            this.btnRemove.TabIndex = 0x18;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextDropShadow = false;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new EventHandler(this.btnRemove_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x2ce, 0x187);
            base.Controls.Add(this.btnRemove);
            base.Controls.Add(this.btnRemoveFromFavourites);
            base.Controls.Add(this.btnSearch);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.listBoxRecipients);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listBoxFavourites);
            base.Controls.Add(this.listBoxRecent);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.btnAddToFavourites);
            base.Controls.Add(this.textBoxNewRecipient);
            base.Controls.Add(this.listBoxSearch);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnAdd);
            base.Icon = Resources.shk_icon;
            base.Name = "MailUserPopup";
            base.ShowBar = true;
            base.ShowClose = true;
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Add Users";
            base.TopMost = true;
            base.FormClosing += new FormClosingEventHandler(this.MailUserPopup_FormClosing);
            base.Controls.SetChildIndex(this.btnAdd, 0);
            base.Controls.SetChildIndex(this.btnClose, 0);
            base.Controls.SetChildIndex(this.listBoxSearch, 0);
            base.Controls.SetChildIndex(this.textBoxNewRecipient, 0);
            base.Controls.SetChildIndex(this.btnAddToFavourites, 0);
            base.Controls.SetChildIndex(this.label5, 0);
            base.Controls.SetChildIndex(this.listBoxRecent, 0);
            base.Controls.SetChildIndex(this.listBoxFavourites, 0);
            base.Controls.SetChildIndex(this.label1, 0);
            base.Controls.SetChildIndex(this.btnCancel, 0);
            base.Controls.SetChildIndex(this.label2, 0);
            base.Controls.SetChildIndex(this.listBoxRecipients, 0);
            base.Controls.SetChildIndex(this.label4, 0);
            base.Controls.SetChildIndex(this.btnSearch, 0);
            base.Controls.SetChildIndex(this.btnRemoveFromFavourites, 0);
            base.Controls.SetChildIndex(this.btnRemove, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listBoxFavourites_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxFavourites.SelectedIndex >= 0)
            {
                GameEngine.Instance.playInterfaceSound("MailUserPopup_add");
                if (!this.listBoxRecipients.Items.Contains(this.listBoxFavourites.SelectedItem.ToString()))
                {
                    this.parentPopup.addRecipient(this.listBoxFavourites.SelectedItem.ToString());
                    this.listBoxRecipients.Items.Add(this.listBoxFavourites.SelectedItem.ToString());
                    this.btnAdd.Enabled = false;
                    this.btnClose.Enabled = true;
                    this.btnRemove.Enabled = true;
                }
                else
                {
                    this.listBoxRecipients.Items.Remove(this.listBoxFavourites.SelectedItem.ToString());
                    this.btnAdd.Enabled = true;
                    this.btnRemove.Enabled = false;
                    if (this.forwardPopup && (this.listBoxRecipients.Items.Count <= 0))
                    {
                        this.btnClose.Enabled = false;
                    }
                }
            }
        }

        private void listBoxFavourites_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxFavourites.SelectedIndex >= 0)
            {
                this.listBoxRecent.ClearSelected();
                this.listBoxRecipients.ClearSelected();
                this.listBoxSearch.ClearSelected();
                this.btnAdd.Enabled = !this.listBoxRecipients.Items.Contains(this.listBoxFavourites.SelectedItem.ToString());
                this.btnRemove.Enabled = !this.btnAdd.Enabled;
                this.btnAddToFavourites.Enabled = false;
                this.btnRemoveFromFavourites.Enabled = true;
            }
        }

        private void listBoxRecent_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxRecent.SelectedIndex >= 0)
            {
                GameEngine.Instance.playInterfaceSound("MailUserPopup_add");
                if (!this.listBoxRecipients.Items.Contains(this.listBoxRecent.SelectedItem.ToString()))
                {
                    this.parentPopup.addRecipient(this.listBoxRecent.SelectedItem.ToString());
                    this.listBoxRecipients.Items.Add(this.listBoxRecent.SelectedItem.ToString());
                    this.btnAdd.Enabled = false;
                    this.btnClose.Enabled = true;
                    this.btnRemove.Enabled = true;
                }
                else
                {
                    this.listBoxRecipients.Items.Remove(this.listBoxRecent.SelectedItem.ToString());
                    this.btnAdd.Enabled = true;
                    this.btnRemove.Enabled = false;
                    if (this.forwardPopup && (this.listBoxRecipients.Items.Count <= 0))
                    {
                        this.btnClose.Enabled = false;
                    }
                }
            }
        }

        private void listBoxRecent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxRecent.SelectedIndex >= 0)
            {
                this.listBoxSearch.ClearSelected();
                this.listBoxRecipients.ClearSelected();
                this.listBoxFavourites.ClearSelected();
                this.btnAdd.Enabled = !this.listBoxRecipients.Items.Contains(this.listBoxRecent.SelectedItem.ToString());
                this.btnRemove.Enabled = !this.btnAdd.Enabled;
                this.btnAddToFavourites.Enabled = !this.listBoxFavourites.Items.Contains(this.listBoxRecent.SelectedItem.ToString());
                this.btnRemoveFromFavourites.Enabled = !this.btnAddToFavourites.Enabled;
            }
        }

        private void listBoxRecipients_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxRecipients.SelectedIndex >= 0)
            {
                this.btnRemove.Enabled = false;
                this.btnAdd.Enabled = false;
                this.btnAddToFavourites.Enabled = false;
                this.btnRemoveFromFavourites.Enabled = false;
                GameEngine.Instance.playInterfaceSound("MailUserPopup_add");
                this.listBoxRecipients.Items.Remove(this.listBoxRecipients.SelectedItem.ToString());
                if (this.forwardPopup && (this.listBoxRecipients.Items.Count <= 0))
                {
                    this.btnClose.Enabled = false;
                }
            }
        }

        private void listBoxRecipients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxRecipients.SelectedIndex >= 0)
            {
                this.listBoxRecent.ClearSelected();
                this.listBoxSearch.ClearSelected();
                this.listBoxFavourites.ClearSelected();
                this.btnAdd.Enabled = false;
                this.btnRemove.Enabled = true;
                this.btnAddToFavourites.Enabled = !this.listBoxFavourites.Items.Contains(this.listBoxRecipients.SelectedItem.ToString());
                this.btnRemoveFromFavourites.Enabled = !this.btnAddToFavourites.Enabled;
            }
        }

        private void listBoxSearch_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxSearch.SelectedIndex >= 0)
            {
                GameEngine.Instance.playInterfaceSound("MailUserPopup_add");
                if (!this.listBoxRecipients.Items.Contains(this.listBoxSearch.SelectedItem.ToString()))
                {
                    this.parentPopup.addRecipient(this.listBoxSearch.SelectedItem.ToString());
                    this.listBoxRecipients.Items.Add(this.listBoxSearch.SelectedItem.ToString());
                    this.btnAdd.Enabled = false;
                    this.btnClose.Enabled = true;
                    this.btnRemove.Enabled = true;
                }
                else
                {
                    this.listBoxRecipients.Items.Remove(this.listBoxSearch.SelectedItem.ToString());
                    this.btnAdd.Enabled = true;
                    this.btnRemove.Enabled = false;
                    if (this.forwardPopup && (this.listBoxRecipients.Items.Count <= 0))
                    {
                        this.btnClose.Enabled = false;
                    }
                }
            }
        }

        private void listBoxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxSearch.SelectedIndex >= 0)
            {
                this.listBoxRecent.ClearSelected();
                this.listBoxRecipients.ClearSelected();
                this.listBoxFavourites.ClearSelected();
                this.btnAdd.Enabled = !this.listBoxRecipients.Items.Contains(this.listBoxSearch.SelectedItem.ToString());
                this.btnRemove.Enabled = !this.btnAdd.Enabled;
                this.btnAddToFavourites.Enabled = !this.listBoxFavourites.Items.Contains(this.listBoxSearch.SelectedItem.ToString());
                this.btnRemoveFromFavourites.Enabled = !this.btnAddToFavourites.Enabled;
            }
        }

        private void MailUserPopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("MailUserPopup_cancel");
            this.m_searchTimer.Dispose();
            this.parentPopup.popupClosed(false);
        }

        public void setAsMail()
        {
            this.btnCancel.Visible = false;
            this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
            this.btnAdd.Text = SK.Text("MailUserPopup_Add", "Add");
            this.label1.Text = SK.Text("MailUserPopup_Recent", "Recent");
            this.label2.Text = SK.Text("MailUserPopup_Favourites", "Favourites");
            this.label3.Text = SK.Text("MailUserPopup_Search_Results", "Search Results");
            this.label4.Text = SK.Text("MailUserPopup_Recipients", "Recipients");
            this.label5.Text = SK.Text("MailUserPopup_Player_Search", "Search for Player Name");
            this.btnAddToFavourites.Text = SK.Text("MailUserPopup_Add_To_Favourites", "Add to Favourites");
            this.btnSearch.Text = SK.Text("MailUserPopup_Search", "Search");
            this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.Text = base.Title = SK.Text("MailUserPopup_Add_Users", "Add Users");
            this.btnClose.Enabled = true;
            this.forwardPopup = false;
        }

        public void setAsReportForward()
        {
            this.btnCancel.Visible = true;
            this.btnClose.Text = SK.Text("MailUserPopup_Forward", "Forward");
            this.btnAdd.Text = SK.Text("MailUserPopup_Add", "Add");
            this.btnRemove.Text = SK.Text("MailUserPopup_Remove", "Remove");
            this.label1.Text = SK.Text("MailUserPopup_Recent", "Recent");
            this.label2.Text = SK.Text("MailUserPopup_Favourites", "Favourites");
            this.label3.Text = SK.Text("MailUserPopup_Search_Results", "Search Results");
            this.label4.Text = SK.Text("MailUserPopup_Recipients", "Recipients");
            this.label5.Text = SK.Text("MailUserPopup_Player_Search", "Search for Player Name");
            this.btnAddToFavourites.Text = SK.Text("MailUserPopup_Add_To_Favourites", "Add to Favourites");
            this.btnSearch.Text = SK.Text("MailUserPopup_Search", "Search");
            this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.Text = base.Title = SK.Text("MailUserPopup_Add_Users", "Add Users");
            this.btnRemoveFromFavourites.Text = SK.Text("MailUserPopup_Remove_From_Favourites", "Remove from Favourites");
            this.btnClose.Enabled = false;
            this.btnRemove.Enabled = false;
            this.btnRemoveFromFavourites.Enabled = false;
            this.forwardPopup = true;
        }

        public void setParent(IMailUserInterface parent, string[] history, string[] favourites, string[] recipients)
        {
            this.parentPopup = parent;
            if (history != null)
            {
                foreach (string str in history)
                {
                    this.listBoxRecent.Items.Add(str);
                }
            }
            if (favourites != null)
            {
                foreach (string str2 in favourites)
                {
                    this.listBoxFavourites.Items.Add(str2);
                }
            }
            if (recipients != null)
            {
                foreach (string str3 in recipients)
                {
                    this.listBoxRecipients.Items.Add(str3);
                }
            }
            this.btnSearch.Enabled = false;
            this.btnAdd.Enabled = false;
            this.btnAddToFavourites.Enabled = false;
        }

        private void textBoxNewRecipient_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.textBoxNewRecipient.Text.Length > 0)
                {
                    GameEngine.Instance.playInterfaceSound("MailUserPopup_search");
                    RemoteServices.Instance.GetMailUserSearch(this.textBoxNewRecipient.Text);
                    if (this.listBoxSearch.SelectedIndex != -1)
                    {
                        this.btnAdd.Enabled = this.btnRemove.Enabled = this.btnAddToFavourites.Enabled = this.btnRemoveFromFavourites.Enabled = false;
                    }
                }
                e.Handled = true;
            }
        }

        private void textBoxNewRecipient_KeyUp(object sender, KeyEventArgs e)
        {
            this.lastUpdateTime = DXTimer.GetCurrentMilliseconds();
            if (this.textBoxNewRecipient.Text.Length == 0)
            {
                this.btnSearch.Enabled = false;
            }
            else
            {
                this.btnSearch.Enabled = true;
            }
        }

        private void timerCallbackFunction(object o)
        {
            if (Monitor.TryEnter(this.searchTimerLock))
            {
                try
                {
                    this.updateSearch();
                }
                finally
                {
                    Monitor.Exit(this.searchTimerLock);
                }
            }
        }

        private void updateSearch()
        {
            if ((this.lastUpdateTime != 0.0) && ((DXTimer.GetCurrentMilliseconds() - this.lastUpdateTime) > 2000.0))
            {
                this.lastUpdateTime = 0.0;
                if (this.textBoxNewRecipient.Text.Length > 2)
                {
                    RemoteServices.Instance.GetMailUserSearch(this.textBoxNewRecipient.Text);
                }
            }
        }
    }
}

