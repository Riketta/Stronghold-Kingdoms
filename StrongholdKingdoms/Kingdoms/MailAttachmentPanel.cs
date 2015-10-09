namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class MailAttachmentPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDImage backImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton btnClose = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea currentAttachmentArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar currentAttachmentBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDArea currentAttachmentScrollArea = new CustomSelfDrawPanel.CSDArea();
        private int currentTab = -1;
        private CustomSelfDrawPanel.CSDButton currentTabButton = new CustomSelfDrawPanel.CSDButton();
        private List<LinkLine> lineList = new List<LinkLine>();
        public List<MailLink> linkList = new List<MailLink>();
        private MailScreen m_mailParent;
        private MailAttachmentPopup m_parent;
        private CustomSelfDrawPanel.CSDArea parentArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton playerAddButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea playerSearchArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDListBox playerSearchList = new CustomSelfDrawPanel.CSDListBox();
        private CustomSelfDrawPanel.CSDButton playerTabButton = new CustomSelfDrawPanel.CSDButton();
        private bool readOnly;
        private CustomSelfDrawPanel.CSDButton regionAddButton = new CustomSelfDrawPanel.CSDButton();
        private string[] regionNames;
        private CustomSelfDrawPanel.CSDArea regionSearchArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDListBox regionSearchList = new CustomSelfDrawPanel.CSDListBox();
        private CustomSelfDrawPanel.CSDButton regionTabButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton removeLinkButton = new CustomSelfDrawPanel.CSDButton();
        private string searchText = "";
        private LinkLine selectedLine;
        private VillageLine selectedVillage;
        private CustomSelfDrawPanel.CSDButton villageAddButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDVertScrollBar villageBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private List<VillageLine> villageLines = new List<VillageLine>();
        private CustomSelfDrawPanel.CSDArea villageScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea villageSearchArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDListBox villageSearchList = new CustomSelfDrawPanel.CSDListBox();
        private CustomSelfDrawPanel.CSDButton villageTabButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel villageUserLabel = new CustomSelfDrawPanel.CSDLabel();

        public MailAttachmentPanel()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void addPlayer(CustomSelfDrawPanel.CSDListItem item)
        {
            bool flag = false;
            foreach (MailLink link in this.linkList)
            {
                if ((link.linkType == 1) && (link.objectName == item.Text))
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                MailLink link2 = new MailLink {
                    linkType = 1,
                    objectName = item.Text,
                    objectID = -1
                };
                this.linkList.Add(link2);
                this.playerSearchList.highlightedItems.Add(item);
                this.playerSearchList.updateEntries();
            }
        }

        private void addRegion(CustomSelfDrawPanel.CSDListItem item)
        {
            bool flag = false;
            foreach (MailLink link in this.linkList)
            {
                if ((link.linkType == 3) && (link.objectName == item.Text))
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                MailLink link2 = new MailLink {
                    linkType = 3,
                    objectName = item.Text,
                    objectID = item.Data
                };
                this.linkList.Add(link2);
                this.regionSearchList.highlightedItems.Add(item);
                this.regionSearchList.clearSelectedItem();
            }
        }

        private void addVillage(CustomSelfDrawPanel.CSDListItem item)
        {
            bool flag = false;
            foreach (MailLink link in this.linkList)
            {
                if ((link.linkType == 2) && (link.objectID == item.Data))
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                MailLink link2 = new MailLink {
                    linkType = 2,
                    objectName = item.Text,
                    objectID = item.Data
                };
                this.linkList.Add(link2);
                MyMessageBox.Show(SK.Text("Attachments__Added", "Added to mail"));
            }
        }

        private void addVillage(VillageLine line)
        {
            bool flag = false;
            foreach (MailLink link in this.linkList)
            {
                if ((link.linkType == 2) && (link.objectID == line.villageID))
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                MailLink item = new MailLink {
                    linkType = 2,
                    objectName = line.nameLabel.Text,
                    objectID = line.villageID
                };
                this.linkList.Add(item);
                line.isAdded = true;
                line.invalidate();
            }
        }

        private void changeTabIcons(int tab)
        {
            this.currentTab = tab;
            this.playerTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_player_normal;
            this.playerTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_player_over;
            this.playerTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_player_normal;
            this.playerSearchArea.Visible = false;
            this.villageTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_village_normal;
            this.villageTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_village_over;
            this.villageTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_village_normal;
            this.villageSearchArea.Visible = false;
            this.regionTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_parish_normal;
            this.regionTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_parish_over;
            this.regionTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_parish_normal;
            this.regionSearchArea.Visible = false;
            this.currentTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_current_normal;
            this.currentTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_current_over;
            this.currentTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_current_normal;
            this.currentAttachmentArea.Visible = false;
            switch (tab)
            {
                case 0:
                    this.playerTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_player_selected;
                    this.playerTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_player_selected;
                    this.playerTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_player_selected;
                    this.playerSearchArea.Visible = true;
                    this.m_parent.setTextBoxVisible(1);
                    return;

                case 1:
                    this.villageTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_village_selected;
                    this.villageTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_village_selected;
                    this.villageTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_village_selected;
                    this.villageSearchArea.Visible = true;
                    this.m_parent.setTextBoxVisible(-1);
                    return;

                case 2:
                    this.regionTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_parish_selected;
                    this.regionTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_parish_selected;
                    this.regionTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_parish_selected;
                    this.regionSearchArea.Visible = true;
                    this.m_parent.setTextBoxVisible(3);
                    return;

                case 3:
                    this.currentTabButton.ImageNorm = (Image) GFXLibrary.mail2_attach_current_selected;
                    this.currentTabButton.ImageOver = (Image) GFXLibrary.mail2_attach_current_selected;
                    this.currentTabButton.ImageClick = (Image) GFXLibrary.mail2_attach_current_selected;
                    this.currentAttachmentArea.Visible = true;
                    this.m_parent.setTextBoxVisible(-1);
                    return;
            }
            this.m_parent.setTextBoxVisible(-1);
        }

        public void clearContents(bool includeLinks)
        {
            if (includeLinks)
            {
                this.linkList.Clear();
            }
            this.playerSearchList.populate(new List<CustomSelfDrawPanel.CSDListItem>());
            this.villageSearchList.populate(new List<CustomSelfDrawPanel.CSDListItem>());
            this.regionSearchList.populate(new List<CustomSelfDrawPanel.CSDListItem>());
            this.villageTabButton.Active = false;
            this.villageTabButton.Alpha = 0.5f;
            this.villageTabButton.CustomTooltipID = 0x206;
            this.playerAddButton.Enabled = false;
            this.villageAddButton.Enabled = false;
            this.regionAddButton.Enabled = false;
            this.selectedVillage = null;
            this.selectedLine = null;
            this.changeTabIcons(-1);
        }

        public void closeClick()
        {
            GameEngine.Instance.playInterfaceSound("ReportsGeneric_close");
            this.m_parent.closeControl(true);
            InterfaceMgr.Instance.reactiveMainWindow();
        }

        private void currentScrollBarMoved()
        {
            int y = this.currentAttachmentBar.Value;
            this.currentAttachmentScrollArea.Position = new Point(this.currentAttachmentScrollArea.X, -y);
            this.currentAttachmentScrollArea.ClipRect = new Rectangle(this.currentAttachmentScrollArea.ClipRect.X, y, this.currentAttachmentScrollArea.ClipRect.Width, this.currentAttachmentScrollArea.ClipRect.Height);
            this.currentAttachmentScrollArea.invalidate();
            this.currentAttachmentBar.invalidate();
        }

        private void factionTabClick()
        {
            this.changeTabIcons(2);
        }

        private void getMailUserSearchCallback(GetMailUserSearch_ReturnType returnData)
        {
            if (returnData.Success)
            {
                List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
                if (returnData.mailUsersSearch != null)
                {
                    this.playerSearchList.highlightedItems.Clear();
                    foreach (string str in returnData.mailUsersSearch)
                    {
                        CustomSelfDrawPanel.CSDListItem item = new CustomSelfDrawPanel.CSDListItem {
                            Text = str
                        };
                        items.Add(item);
                        foreach (MailLink link in this.linkList)
                        {
                            if ((link.linkType == 1) && (link.objectName.ToLower() == str.ToLower()))
                            {
                                this.playerSearchList.highlightedItems.Add(item);
                            }
                        }
                    }
                }
                string userName = RemoteServices.Instance.UserName;
                if (userName.ToLower().Contains(this.searchText.ToLower()))
                {
                    CustomSelfDrawPanel.CSDListItem item2 = new CustomSelfDrawPanel.CSDListItem {
                        Text = userName
                    };
                    items.Add(item2);
                    foreach (MailLink link2 in this.linkList)
                    {
                        if ((link2.linkType == 1) && (link2.objectName.ToLower() == userName.ToLower()))
                        {
                            this.playerSearchList.highlightedItems.Add(item2);
                        }
                    }
                }
                this.playerSearchList.populate(items);
                this.playerAddButton.Enabled = this.playerSearchList.getSelectedItem() != null;
                this.villageTabButton.Active = this.playerAddButton.Enabled;
                this.villageTabButton.Alpha = this.playerAddButton.Enabled ? 1f : 0.5f;
                this.villageTabButton.CustomTooltipID = this.playerAddButton.Enabled ? 0x1ff : 0x206;
            }
        }

        public void init(Size parentSize, MailAttachmentPopup parent, MailScreen parentMail)
        {
            base.Size = parentSize;
            this.m_parent = parent;
            this.m_mailParent = parentMail;
            this.btnClose.Text.Text = SK.Text("GENERIC_Close", "Close");
            this.btnClose.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.btnClose.ImageOver = (Image) GFXLibrary.button_132_over;
            this.btnClose.ImageClick = (Image) GFXLibrary.button_132_in;
            this.btnClose.setSizeToImage();
            this.btnClose.Position = new Point((base.Width / 2) - (this.btnClose.Width / 2), (base.Height - this.btnClose.Height) - 5);
            this.btnClose.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.btnClose.TextYOffset = -2;
            this.btnClose.Text.Color = ARGBColors.Black;
            this.btnClose.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Mail_Attachment_Close");
            this.btnClose.Enabled = true;
            base.addControl(this.btnClose);
            this.initIconArea();
        }

        public void initCurrentAttachments()
        {
            this.lineList.Clear();
            this.currentAttachmentScrollArea.clearControls();
            this.currentAttachmentArea.invalidate();
            int y = 0;
            int position = 0;
            foreach (MailLink link in this.linkList)
            {
                LinkLine control = new LinkLine();
                if (y != 0)
                {
                    y += 2;
                }
                control.Position = new Point(3, y);
                control.init(link, position, this.currentAttachmentScrollArea.Width - 6, this.readOnly, this);
                this.currentAttachmentScrollArea.addControl(control);
                y += control.Height;
                this.lineList.Add(control);
                position++;
            }
            this.removeLinkButton.Enabled = this.linkList.Count > 0;
            this.currentAttachmentScrollArea.Size = new Size(this.currentAttachmentScrollArea.Width, y);
            if (y < this.currentAttachmentBar.Height)
            {
                this.currentAttachmentBar.Visible = false;
            }
            else
            {
                this.currentAttachmentBar.Visible = true;
                this.currentAttachmentBar.NumVisibleLines = this.currentAttachmentBar.Height;
                this.currentAttachmentBar.Max = y - this.currentAttachmentBar.Height;
            }
            this.currentAttachmentScrollArea.invalidate();
            this.currentAttachmentBar.invalidate();
        }

        private void initIconArea()
        {
            this.parentArea.Position = new Point(10, 10);
            this.parentArea.Size = new Size(base.Width - 20, base.Height - 20);
            base.addControl(this.parentArea);
            this.backImage.Image = (Image) GFXLibrary.mail2_new_mail_tab_panel;
            this.backImage.Position = new Point(0, 0);
            this.backImage.setSizeToImage();
            this.backImage.Visible = true;
            this.parentArea.addControl(this.backImage);
            this.changeTabIcons(-1);
            this.playerTabButton.Position = this.backImage.Position;
            this.playerTabButton.setSizeToImage();
            this.playerTabButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playerTabClick), "Attachment_Player_Tab");
            this.playerTabButton.CustomTooltipID = 0x1f9;
            this.parentArea.addControl(this.playerTabButton);
            this.playerSearchArea.Position = new Point(this.backImage.X, this.playerTabButton.Rectangle.Bottom);
            this.playerSearchArea.Size = new Size(this.backImage.Width, this.backImage.Height - this.playerTabButton.Height);
            this.parentArea.addControl(this.playerSearchArea);
            this.playerSearchList.Size = new Size(160, 0xd8);
            this.playerSearchList.Position = new Point((this.playerSearchArea.Width / 2) - (this.playerSearchList.Width / 2), 40);
            this.playerSearchArea.addControl(this.playerSearchList);
            this.playerSearchList.Create(12, 0x12);
            this.playerSearchList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.playerListClick));
            this.playerSearchList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.playerListDoubleClick));
            this.playerAddButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.playerAddButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.playerAddButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.playerAddButton.setSizeToImage();
            this.playerAddButton.Position = new Point((this.playerSearchArea.Width / 2) - (this.playerAddButton.Width / 2), this.playerSearchList.Rectangle.Bottom + 5);
            this.playerAddButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.playerAddButton.TextYOffset = -2;
            this.playerAddButton.Text.Text = SK.Text("MailScreen_Add", "Add");
            this.playerAddButton.Text.Color = ARGBColors.Black;
            this.playerAddButton.Enabled = false;
            this.playerAddButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playerAddClick), "Attachments_Add_Player");
            this.playerSearchArea.addControl(this.playerAddButton);
            this.playerSearchArea.Visible = false;
            this.villageTabButton.Position = new Point(this.playerTabButton.Rectangle.Right, this.playerTabButton.Y);
            this.villageTabButton.setSizeToImage();
            this.villageTabButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageTabClick), "Attachment_Village_Tab");
            this.parentArea.addControl(this.villageTabButton);
            this.villageTabButton.Enabled = true;
            this.villageTabButton.Active = false;
            this.villageTabButton.Alpha = 0.5f;
            this.villageTabButton.CustomTooltipID = 0x206;
            this.villageSearchArea.Position = new Point(this.backImage.X, this.playerTabButton.Rectangle.Bottom);
            this.villageSearchArea.Size = new Size(this.backImage.Width, this.backImage.Height - this.playerTabButton.Height);
            this.parentArea.addControl(this.villageSearchArea);
            this.villageUserLabel.Color = ARGBColors.Black;
            this.villageUserLabel.Position = new Point(1, 1);
            this.villageUserLabel.Size = new Size(this.villageSearchArea.Width - 7, 0x18);
            this.villageUserLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageUserLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.villageSearchArea.addControl(this.villageUserLabel);
            this.villageSearchList.Size = new Size(160, 0xd8);
            this.villageSearchList.Position = new Point((this.villageSearchArea.Width / 2) - (this.villageSearchList.Width / 2), 40);
            this.villageSearchList.Create(12, 0x12);
            this.villageSearchList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.villageListClick));
            this.villageSearchList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.villageListDoubleClick));
            this.villageAddButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.villageAddButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.villageAddButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.villageAddButton.setSizeToImage();
            this.villageAddButton.Position = new Point((this.villageSearchArea.Width / 2) - (this.villageAddButton.Width / 2), this.villageSearchList.Rectangle.Bottom + 5);
            this.villageAddButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.villageAddButton.TextYOffset = -2;
            this.villageAddButton.Text.Text = SK.Text("MailScreen_Add", "Add");
            this.villageAddButton.Text.Color = ARGBColors.Black;
            this.villageAddButton.Enabled = false;
            this.villageAddButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageAddClick), "Attachments_Add_Village");
            this.villageSearchArea.addControl(this.villageAddButton);
            this.villageScrollArea.Position = new Point(5, 0x10);
            this.villageScrollArea.Size = new Size(this.villageSearchArea.Width - 0x27, this.villageSearchArea.Height - 0x54);
            this.villageScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(this.villageSearchArea.Width - 0x27, this.villageSearchArea.Height - 0x54));
            this.villageSearchArea.addControl(this.villageScrollArea);
            this.villageBar.Position = new Point(this.villageScrollArea.Rectangle.Right, this.villageScrollArea.Y);
            this.villageBar.Size = new Size(0x18, this.villageScrollArea.Height);
            this.villageSearchArea.addControl(this.villageBar);
            this.villageBar.Value = 0;
            this.villageBar.Max = 100;
            this.villageBar.NumVisibleLines = 5;
            this.villageBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.villageBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.villageScrollBarMoved));
            this.villageSearchArea.Visible = false;
            this.regionTabButton.Position = new Point(this.villageTabButton.Rectangle.Right, this.villageTabButton.Y);
            this.regionTabButton.setSizeToImage();
            this.regionTabButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionTabClick), "Attachment_Faction_Tab");
            this.regionTabButton.CustomTooltipID = 0x200;
            this.parentArea.addControl(this.regionTabButton);
            this.regionSearchArea.Position = new Point(this.backImage.X, this.playerTabButton.Rectangle.Bottom);
            this.regionSearchArea.Size = new Size(this.backImage.Width, this.backImage.Height - this.playerTabButton.Height);
            this.parentArea.addControl(this.regionSearchArea);
            this.regionSearchList.Size = new Size(160, 0xd8);
            this.regionSearchList.Position = new Point((this.regionSearchArea.Width / 2) - (this.regionSearchList.Width / 2), 40);
            this.regionSearchArea.addControl(this.regionSearchList);
            this.regionSearchList.Create(12, 0x12);
            this.regionSearchList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.regionListClick));
            this.regionSearchList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.regionListDoubleClick));
            this.regionAddButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.regionAddButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.regionAddButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.regionAddButton.setSizeToImage();
            this.regionAddButton.Position = new Point((this.regionSearchArea.Width / 2) - (this.regionAddButton.Width / 2), this.regionSearchList.Rectangle.Bottom + 5);
            this.regionAddButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.regionAddButton.TextYOffset = -2;
            this.regionAddButton.Text.Text = SK.Text("MailScreen_Add", "Add");
            this.regionAddButton.Text.Color = ARGBColors.Black;
            this.regionAddButton.Enabled = false;
            this.regionAddButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.regionAddClick), "Attachments_Add_Region");
            this.regionSearchArea.addControl(this.regionAddButton);
            this.regionSearchArea.Visible = false;
            this.currentTabButton.Position = new Point(this.regionTabButton.Rectangle.Right, this.regionTabButton.Y);
            this.currentTabButton.setSizeToImage();
            this.currentTabButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.listTabclick), "Attachment_List_Tab");
            this.currentTabButton.CustomTooltipID = 0x201;
            this.parentArea.addControl(this.currentTabButton);
            this.currentAttachmentArea.Position = new Point(this.backImage.X, this.playerTabButton.Rectangle.Bottom);
            this.currentAttachmentArea.Size = new Size(this.backImage.Width, this.backImage.Height - this.playerTabButton.Height);
            this.parentArea.addControl(this.currentAttachmentArea);
            this.currentAttachmentScrollArea.Position = new Point(5, 10);
            this.currentAttachmentScrollArea.Size = new Size(this.currentAttachmentArea.Width - 0x27, this.currentAttachmentArea.Height - 60);
            this.currentAttachmentScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(this.currentAttachmentArea.Width - 0x27, this.currentAttachmentArea.Height - 60));
            this.currentAttachmentArea.addControl(this.currentAttachmentScrollArea);
            this.currentAttachmentBar.Position = new Point(this.currentAttachmentScrollArea.Rectangle.Right, this.currentAttachmentScrollArea.Y);
            this.currentAttachmentBar.Size = new Size(0x18, this.currentAttachmentScrollArea.Height);
            this.currentAttachmentArea.addControl(this.currentAttachmentBar);
            this.currentAttachmentBar.Value = 0;
            this.currentAttachmentBar.Max = 100;
            this.currentAttachmentBar.NumVisibleLines = 5;
            this.currentAttachmentBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.currentAttachmentBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.currentScrollBarMoved));
            this.removeLinkButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.removeLinkButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.removeLinkButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.removeLinkButton.setSizeToImage();
            this.removeLinkButton.Position = new Point((this.currentAttachmentArea.Width / 2) - (this.removeLinkButton.Width / 2), this.playerSearchList.Rectangle.Bottom + 5);
            this.removeLinkButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.removeLinkButton.TextYOffset = -2;
            this.removeLinkButton.Text.Text = SK.Text("GENERIC_Remove", "Remove");
            this.removeLinkButton.Text.Color = ARGBColors.Black;
            this.removeLinkButton.Enabled = false;
            this.removeLinkButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.removeLinkLine), "Attachments_Remove_Link");
            this.currentAttachmentArea.addControl(this.removeLinkButton);
        }

        private void listTabclick()
        {
            this.changeTabIcons(3);
            this.initCurrentAttachments();
        }

        private void loadVillageList(string targetUser)
        {
            RemoteServices.Instance.set_GetOtherUserVillageIDList_UserCallBack(new RemoteServices.GetOtherUserVillageIDList_UserCallBack(this.villageUserInfoCallback));
            RemoteServices.Instance.GetOtherUserVillageIDList(targetUser);
        }

        private void playerAddClick()
        {
            if (this.playerSearchList.getSelectedItem() != null)
            {
                this.addPlayer(this.playerSearchList.getSelectedItem());
            }
        }

        private void playerListClick(CustomSelfDrawPanel.CSDListItem item)
        {
            this.playerAddButton.Enabled = this.playerSearchList.getSelectedItem() != null;
            this.villageTabButton.Active = this.playerAddButton.Enabled;
            this.villageTabButton.Alpha = this.playerAddButton.Enabled ? 1f : 0.5f;
            this.villageTabButton.CustomTooltipID = this.playerAddButton.Enabled ? 0x1ff : 0x206;
        }

        private void playerListDoubleClick(CustomSelfDrawPanel.CSDListItem item)
        {
            this.addPlayer(item);
        }

        private void playerTabClick()
        {
            this.changeTabIcons(0);
        }

        private void regionAddClick()
        {
            if (this.regionSearchList.getSelectedItem() != null)
            {
                this.addRegion(this.regionSearchList.getSelectedItem());
            }
        }

        private void regionListClick(CustomSelfDrawPanel.CSDListItem item)
        {
            this.regionAddButton.Enabled = this.regionSearchList.getSelectedItem() != null;
        }

        private void regionListDoubleClick(CustomSelfDrawPanel.CSDListItem item)
        {
            this.addRegion(item);
        }

        private void removeLinkLine()
        {
            if (this.selectedLine != null)
            {
                this.linkList.Remove(this.selectedLine.parentLink);
                this.initCurrentAttachments();
                this.removeLinkButton.Enabled = false;
                CustomSelfDrawPanel.CSDListItem item = null;
                switch (this.selectedLine.parentLink.linkType)
                {
                    case 1:
                        foreach (CustomSelfDrawPanel.CSDListItem item2 in this.playerSearchList.highlightedItems)
                        {
                            if (item2.Text == this.selectedLine.parentLink.objectName)
                            {
                                item = item2;
                            }
                        }
                        if (item != null)
                        {
                            this.playerSearchList.highlightedItems.Remove(item);
                            this.playerSearchList.updateEntries();
                            if (this.playerSearchList.getSelectedItem() != item)
                            {
                                return;
                            }
                            this.playerSearchList.clearSelectedItem();
                            this.villageTabButton.Active = false;
                            this.villageTabButton.Alpha = 0.5f;
                            this.villageTabButton.CustomTooltipID = 0x206;
                        }
                        return;

                    case 2:
                        foreach (VillageLine line in this.villageLines)
                        {
                            if (line.nameLabel.Text.ToLower() == this.selectedLine.parentLink.objectName.ToLower())
                            {
                                line.isAdded = false;
                            }
                        }
                        return;

                    case 3:
                        foreach (CustomSelfDrawPanel.CSDListItem item3 in this.regionSearchList.highlightedItems)
                        {
                            if (item3.Text.ToLower() == this.selectedLine.parentLink.objectName.ToLower())
                            {
                                item = item3;
                            }
                        }
                        if (item != null)
                        {
                            this.regionSearchList.highlightedItems.Remove(item);
                            this.regionSearchList.clearSelectedItem();
                        }
                        return;
                }
            }
        }

        public void searchPlayerUpdateCallback(string textInput)
        {
            switch (this.currentTab)
            {
                case 0:
                    RemoteServices.Instance.set_GetMailUserSearch_UserCallBack(new RemoteServices.GetMailUserSearch_UserCallBack(this.getMailUserSearchCallback));
                    RemoteServices.Instance.GetMailUserSearch(textInput);
                    this.searchText = "";
                    this.searchText = textInput;
                    break;

                case 1:
                case 2:
                    break;

                default:
                    return;
            }
        }

        public void searchRegionUpdateCallback(string textInput)
        {
            if (this.regionNames == null)
            {
                this.regionNames = GameEngine.Instance.World.getParishNameList();
            }
            List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
            this.regionSearchList.highlightedItems.Clear();
            foreach (string str in this.regionNames)
            {
                if (str.ToLower().Contains(textInput.ToLower()))
                {
                    CustomSelfDrawPanel.CSDListItem item = new CustomSelfDrawPanel.CSDListItem {
                        Text = str,
                        Data = GameEngine.Instance.World.getParishIDFromName(str)
                    };
                    items.Add(item);
                    foreach (MailLink link in this.linkList)
                    {
                        if ((link.linkType == 3) && (link.objectName.ToLower() == str.ToLower()))
                        {
                            this.regionSearchList.highlightedItems.Add(item);
                        }
                    }
                }
            }
            this.regionSearchList.populate(items);
            this.regionAddButton.Enabled = this.playerSearchList.getSelectedItem() != null;
        }

        public void setReadOnly(bool value)
        {
            this.readOnly = value;
            if (this.readOnly)
            {
                this.playerTabButton.Enabled = false;
                this.playerSearchArea.Visible = false;
                this.villageTabButton.Active = false;
                this.villageTabButton.Alpha = 0.5f;
                this.villageTabButton.CustomTooltipID = 0x206;
                this.villageSearchArea.Visible = false;
                this.regionTabButton.Enabled = false;
                this.regionSearchArea.Visible = false;
                this.removeLinkButton.Visible = false;
                this.changeTabIcons(3);
            }
            else
            {
                this.playerTabButton.Enabled = true;
                this.playerSearchArea.Visible = true;
                if (this.playerSearchList.getSelectedItem() != null)
                {
                    this.villageTabButton.Active = true;
                    this.villageTabButton.Alpha = 1f;
                    this.villageTabButton.CustomTooltipID = 0x1ff;
                }
                this.villageSearchArea.Visible = true;
                this.regionTabButton.Enabled = true;
                this.regionSearchArea.Visible = true;
                this.removeLinkButton.Visible = true;
                this.changeTabIcons(-1);
            }
        }

        public void setSelectedLine(LinkLine inputLine)
        {
            this.selectedLine = inputLine;
            foreach (LinkLine line in this.lineList)
            {
                line.isSelected(line == inputLine);
                line.invalidate();
            }
            this.removeLinkButton.Enabled = this.selectedLine != null;
        }

        public void setSelectedVillage(VillageLine inputLine)
        {
            this.selectedVillage = inputLine;
            foreach (VillageLine line in this.villageLines)
            {
                line.isSelected(line == inputLine);
                line.invalidate();
            }
            this.villageAddButton.Enabled = this.selectedVillage != null;
        }

        private void villageAddClick()
        {
            if (this.selectedVillage != null)
            {
                this.addVillage(this.selectedVillage);
            }
        }

        private void villageListClick(CustomSelfDrawPanel.CSDListItem item)
        {
            this.villageAddButton.Enabled = this.villageSearchList.getSelectedItem() != null;
        }

        private void villageListDoubleClick(CustomSelfDrawPanel.CSDListItem item)
        {
            this.addVillage(item);
        }

        private void villageScrollBarMoved()
        {
            int y = this.villageBar.Value;
            this.villageScrollArea.Position = new Point(this.villageScrollArea.X, 0x18 - y);
            this.villageScrollArea.ClipRect = new Rectangle(this.villageScrollArea.ClipRect.X, y, this.villageScrollArea.ClipRect.Width, this.villageScrollArea.ClipRect.Height);
            this.villageScrollArea.invalidate();
            this.villageBar.invalidate();
        }

        private void villageTabClick()
        {
            if (this.playerSearchList.getSelectedItem() != null)
            {
                if (this.villageUserLabel.Text == this.playerSearchList.getSelectedItem().Text)
                {
                    this.changeTabIcons(1);
                }
                else
                {
                    this.villageUserLabel.Text = this.playerSearchList.getSelectedItem().Text;
                    this.loadVillageList(this.playerSearchList.getSelectedItem().Text);
                }
            }
        }

        public void villageUserInfoCallback(GetOtherUserVillageIDList_ReturnType returnData)
        {
            if (returnData.Success)
            {
                List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
                this.villageSearchList.populate(items);
                List<WorldMap.VillageData> list2 = new List<WorldMap.VillageData>();
                List<WorldMap.VillageData> list3 = new List<WorldMap.VillageData>();
                List<WorldMap.VillageData> list4 = new List<WorldMap.VillageData>();
                List<WorldMap.VillageData> list5 = new List<WorldMap.VillageData>();
                List<WorldMap.VillageData> list6 = new List<WorldMap.VillageData>();
                foreach (int num in returnData.userVillageList)
                {
                    WorldMap.VillageData item = GameEngine.Instance.World.getVillageData(num);
                    if (item != null)
                    {
                        if (item.regionCapital)
                        {
                            list3.Add(item);
                        }
                        else if (item.countyCapital)
                        {
                            list4.Add(item);
                        }
                        else if (item.provinceCapital)
                        {
                            list5.Add(item);
                        }
                        else if (item.countryCapital)
                        {
                            list6.Add(item);
                        }
                        else
                        {
                            list2.Add(item);
                        }
                    }
                }
                this.villageLines.Clear();
                this.villageScrollArea.clearControls();
                this.villageSearchArea.invalidate();
                int y = 0;
                int position = 0;
                foreach (WorldMap.VillageData data2 in list2)
                {
                    VillageLine control = new VillageLine();
                    if (y != 0)
                    {
                        y += 2;
                    }
                    control.Position = new Point(3, y);
                    control.init(position, this.villageScrollArea.Width - 2, data2, 1, this);
                    this.villageScrollArea.addControl(control);
                    y += control.Height;
                    this.villageLines.Add(control);
                    position++;
                }
                foreach (WorldMap.VillageData data3 in list3)
                {
                    VillageLine line2 = new VillageLine();
                    if (y != 0)
                    {
                        y += 2;
                    }
                    line2.Position = new Point(3, y);
                    line2.init(position, this.villageScrollArea.Width, data3, 2, this);
                    this.villageScrollArea.addControl(line2);
                    y += line2.Height;
                    this.villageLines.Add(line2);
                    position++;
                }
                foreach (WorldMap.VillageData data4 in list4)
                {
                    VillageLine line3 = new VillageLine();
                    if (y != 0)
                    {
                        y += 2;
                    }
                    line3.Position = new Point(3, y);
                    line3.init(position, this.villageScrollArea.Width, data4, 3, this);
                    this.villageScrollArea.addControl(line3);
                    y += line3.Height;
                    this.villageLines.Add(line3);
                    position++;
                }
                foreach (WorldMap.VillageData data5 in list5)
                {
                    VillageLine line4 = new VillageLine();
                    if (y != 0)
                    {
                        y += 2;
                    }
                    line4.Position = new Point(3, y);
                    line4.init(position, this.villageScrollArea.Width, data5, 4, this);
                    this.villageScrollArea.addControl(line4);
                    y += line4.Height;
                    this.villageLines.Add(line4);
                    position++;
                }
                foreach (WorldMap.VillageData data6 in list6)
                {
                    VillageLine line5 = new VillageLine();
                    if (y != 0)
                    {
                        y += 2;
                    }
                    line5.Position = new Point(3, y);
                    line5.init(position, this.villageScrollArea.Width, data6, 5, this);
                    this.villageScrollArea.addControl(line5);
                    y += line5.Height;
                    this.villageLines.Add(line5);
                    position++;
                }
                this.villageAddButton.Enabled = false;
                this.villageScrollArea.Size = new Size(this.villageScrollArea.Width, y);
                if (y < this.villageBar.Height)
                {
                    this.villageBar.Visible = false;
                }
                else
                {
                    this.villageBar.Visible = true;
                    this.villageBar.NumVisibleLines = this.villageBar.Height;
                    this.villageBar.Max = y - this.villageBar.Height;
                }
                this.villageScrollArea.invalidate();
                this.villageBar.invalidate();
                this.changeTabIcons(1);
            }
        }

        public class LinkLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private bool clickable;
            private CustomSelfDrawPanel.CSDImage linkTypeImage = new CustomSelfDrawPanel.CSDImage();
            private MailAttachmentPanel m_parent;
            private CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
            public MailLink parentLink;

            private void getUserIDFromNameCallback(GetUserIDFromName_ReturnType returnData)
            {
                if (returnData.Success && (returnData.userID != -1))
                {
                    InterfaceMgr.Instance.changeTab(0);
                    WorldMap.CachedUserInfo userInfo = new WorldMap.CachedUserInfo {
                        userID = returnData.userID
                    };
                    InterfaceMgr.Instance.showUserInfoScreen(userInfo);
                }
            }

            public void init(MailLink link, int position, int width, bool isClickable, MailAttachmentPanel parent)
            {
                this.m_parent = parent;
                this.parentLink = link;
                this.clickable = isClickable;
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.char_line_01;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.char_line_02;
                }
                this.backgroundImage.Position = new Point(0, 5);
                base.addControl(this.backgroundImage);
                this.Size = new Size(width, 30);
                this.nameLabel.Color = ARGBColors.Black;
                this.nameLabel.RolloverColor = ARGBColors.White;
                this.nameLabel.Position = new Point(1, -10);
                this.nameLabel.Size = new Size(base.Width, this.backgroundImage.Height + 20);
                this.nameLabel.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.nameLabel.Text = this.parentLink.objectName;
                this.backgroundImage.addControl(this.nameLabel);
                switch (this.parentLink.linkType)
                {
                    case 1:
                        this.linkTypeImage.Image = (Image) GFXLibrary.mail2_attach_type_player;
                        this.backgroundImage.CustomTooltipID = 0x203;
                        this.nameLabel.CustomTooltipID = 0x203;
                        this.linkTypeImage.CustomTooltipID = 0x203;
                        break;

                    case 2:
                        this.linkTypeImage.Image = (Image) GFXLibrary.mail2_attach_type_village;
                        this.backgroundImage.CustomTooltipID = 0x204;
                        this.nameLabel.CustomTooltipID = 0x204;
                        this.linkTypeImage.CustomTooltipID = 0x204;
                        break;

                    case 3:
                        this.linkTypeImage.Image = (Image) GFXLibrary.mail2_attach_type_parish;
                        this.backgroundImage.CustomTooltipID = 0x205;
                        this.nameLabel.CustomTooltipID = 0x205;
                        this.linkTypeImage.CustomTooltipID = 0x205;
                        break;
                }
                this.linkTypeImage.setSizeToImage();
                this.linkTypeImage.Position = new Point(base.Width - this.linkTypeImage.Width, 0);
                base.addControl(this.linkTypeImage);
                this.backgroundImage.Width = base.Width - (this.linkTypeImage.Width / 2);
                this.linkTypeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                base.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.nameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
            }

            public void isSelected(bool value)
            {
                this.nameLabel.Font = FontManager.GetFont("Arial", 8.25f, value ? FontStyle.Bold : FontStyle.Regular);
            }

            private void lineClicked()
            {
                this.m_parent.setSelectedLine(this);
                Point point = new Point(-1, -1);
                if (this.clickable)
                {
                    switch (this.parentLink.linkType)
                    {
                        case 1:
                            RemoteServices.Instance.set_GetUserIDFromName_UserCallBack(new RemoteServices.GetUserIDFromName_UserCallBack(this.getUserIDFromNameCallback));
                            RemoteServices.Instance.GetUserIDFromName(this.parentLink.objectName);
                            return;

                        case 2:
                            point = GameEngine.Instance.World.getVillageLocation(this.parentLink.objectID);
                            if (point.X == -1)
                            {
                                MyMessageBox.Show(SK.Text("Attachment_Invalid", "This attachment is invalid"));
                                return;
                            }
                            InterfaceMgr.Instance.changeTab(0);
                            GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                            InterfaceMgr.Instance.displaySelectedVillagePanel(this.parentLink.objectID, false, true, false, false);
                            InterfaceMgr.Instance.reactiveMainWindow();
                            return;

                        case 3:
                        {
                            int villageID = GameEngine.Instance.World.getParishCapital(this.parentLink.objectID);
                            if (villageID != -1)
                            {
                                point = GameEngine.Instance.World.getVillageLocation(villageID);
                                if (point.X != -1)
                                {
                                    InterfaceMgr.Instance.changeTab(0);
                                    GameEngine.Instance.World.startMultiStageZoom(1000.0, (double) point.X, (double) point.Y);
                                    InterfaceMgr.Instance.displaySelectedVillagePanel(villageID, false, true, false, false);
                                    InterfaceMgr.Instance.reactiveMainWindow();
                                    return;
                                }
                                MyMessageBox.Show(SK.Text("Attachment_Invalid", "This attachment is invalid"));
                                break;
                            }
                            MyMessageBox.Show(SK.Text("Attachment_Invalid", "This attachment is invalid"));
                            return;
                        }
                        default:
                            return;
                    }
                }
            }
        }

        public class VillageLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            public bool isAddedFlag;
            private MailAttachmentPanel m_parent;
            public CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDImage sizeImage = new CustomSelfDrawPanel.CSDImage();
            public int villageID = -1;

            public void init(int position, int width, WorldMap.VillageData data, int villageSize, MailAttachmentPanel parent)
            {
                this.m_parent = parent;
                this.villageID = data.id;
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.char_line_01;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.char_line_02;
                }
                this.backgroundImage.Position = new Point(0, 5);
                base.addControl(this.backgroundImage);
                this.Size = new Size(width, 30);
                if (GameEngine.Instance.World.isCapital(this.villageID))
                {
                    int num = 0;
                    if (GameEngine.Instance.World.isRegionCapital(this.villageID))
                    {
                        num = 0;
                    }
                    else if (GameEngine.Instance.World.isCountyCapital(this.villageID))
                    {
                        num = 1;
                    }
                    else if (GameEngine.Instance.World.isProvinceCapital(this.villageID))
                    {
                        num = 2;
                    }
                    else if (GameEngine.Instance.World.isCountryCapital(this.villageID))
                    {
                        num = 3;
                    }
                    this.sizeImage.Image = (Image) GFXLibrary.char_position[num + 4];
                    this.sizeImage.Position = new Point(5, -4);
                    this.backgroundImage.addControl(this.sizeImage);
                }
                else
                {
                    int index = GameEngine.Instance.World.getVillageSize(this.villageID);
                    this.sizeImage.Image = (Image) GFXLibrary.char_village_icons[index];
                    this.sizeImage.Position = new Point(-10, -18);
                    this.backgroundImage.addControl(this.sizeImage);
                }
                this.nameLabel.Color = this.isAddedFlag ? ARGBColors.Chartreuse : ARGBColors.Black;
                this.nameLabel.Position = new Point(0x23, -10);
                this.nameLabel.Size = new Size(base.Width, this.backgroundImage.Height + 20);
                this.nameLabel.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.nameLabel.Text = data.m_villageName;
                this.backgroundImage.addControl(this.nameLabel);
                this.sizeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                base.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.nameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
            }

            public void isSelected(bool value)
            {
                this.nameLabel.Font = FontManager.GetFont("Arial", 8.25f, value ? FontStyle.Bold : FontStyle.Regular);
            }

            private void lineClicked()
            {
                this.m_parent.setSelectedVillage(this);
            }

            public bool isAdded
            {
                set
                {
                    this.isAddedFlag = value;
                    if (value)
                    {
                        this.nameLabel.Color = ARGBColors.Chartreuse;
                    }
                    else
                    {
                        this.nameLabel.Color = ARGBColors.Black;
                    }
                }
            }
        }
    }
}

