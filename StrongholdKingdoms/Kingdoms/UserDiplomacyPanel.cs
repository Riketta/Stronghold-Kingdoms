namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class UserDiplomacyPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDLabel alliesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea alliesScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar alliesScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private int blockYSize;
        private IContainer components;
        private static List<string> customNotes = new List<string>();
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDLabel enemiesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea enemiesScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar enemiesScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage2 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        public static UserDiplomacyPanel instance = null;
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay1 = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay2 = new CustomSelfDrawPanel.CSDControl();
        public const int PANEL_ID = 60;
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();

        public UserDiplomacyPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void addPlayers()
        {
            this.alliesScrollArea.clearControls();
            this.enemiesScrollArea.clearControls();
            int y = 0;
            int num2 = 2;
            List<UserRelationship> userRelations = GameEngine.Instance.World.UserRelations;
            int position = 0;
            int num4 = 0;
            foreach (UserRelationship relationship in userRelations)
            {
                if (relationship.friendly)
                {
                    UserAllianceLine control = new UserAllianceLine();
                    if (y != 0)
                    {
                        y += 5;
                    }
                    control.Position = new Point(0, y);
                    control.init(relationship.userName, relationship.userID, position, true, this);
                    this.alliesScrollArea.addControl(control);
                    y += control.Height;
                    position++;
                }
                else
                {
                    UserAllianceLine line2 = new UserAllianceLine();
                    if (num2 != 0)
                    {
                        num2 += 5;
                    }
                    line2.Position = new Point(0, num2);
                    line2.init(relationship.userName, relationship.userID, num4, false, this);
                    this.enemiesScrollArea.addControl(line2);
                    num2 += line2.Height;
                    num4++;
                }
            }
            this.alliesScrollArea.Size = new Size(this.alliesScrollArea.Width, y);
            if (y < this.alliesScrollBar.Height)
            {
                this.alliesScrollBar.Visible = false;
            }
            else
            {
                this.alliesScrollBar.Visible = true;
                this.alliesScrollBar.NumVisibleLines = this.alliesScrollBar.Height;
                this.alliesScrollBar.Max = y - this.alliesScrollBar.Height;
            }
            this.alliesScrollArea.invalidate();
            this.alliesScrollBar.invalidate();
            this.enemiesScrollArea.Size = new Size(this.enemiesScrollArea.Width, num2);
            if (num2 < this.enemiesScrollBar.Height)
            {
                this.enemiesScrollBar.Visible = false;
            }
            else
            {
                this.enemiesScrollBar.Visible = true;
                this.enemiesScrollBar.NumVisibleLines = this.enemiesScrollBar.Height;
                this.enemiesScrollBar.Max = num2 - this.enemiesScrollBar.Height;
            }
            this.enemiesScrollArea.invalidate();
            this.enemiesScrollBar.invalidate();
            this.update();
            base.Invalidate();
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
            base.clearControls();
            this.closing();
        }

        public void closing()
        {
            InterfaceMgr.Instance.closeDonatePopup();
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        public void display(ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(parent, x, y);
        }

        public void display(bool asPopup, ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(asPopup, parent, x, y);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void incomingWallScrollBarMoved()
        {
            int y = this.enemiesScrollBar.Value;
            this.enemiesScrollArea.Position = new Point(this.enemiesScrollArea.X, ((0x23 + this.blockYSize) + 5) - y);
            this.enemiesScrollArea.ClipRect = new Rectangle(this.enemiesScrollArea.ClipRect.X, y, this.enemiesScrollArea.ClipRect.Width, this.enemiesScrollArea.ClipRect.Height);
            this.enemiesScrollArea.invalidate();
            this.enemiesScrollBar.invalidate();
        }

        public void init(bool resized)
        {
            int height = base.Height;
            this.blockYSize = height / 2;
            instance = this;
            base.clearControls();
            this.loadDiplomacyData();
            this.mainBackgroundImage.FillColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = new Size(base.Width, height);
            base.addControl(this.mainBackgroundImage);
            this.backgroundFade.Image = (Image) GFXLibrary.background_top;
            this.backgroundFade.Position = new Point(0, 0);
            this.backgroundFade.Size = new Size(base.Width, this.backgroundFade.Image.Height);
            this.mainBackgroundImage.addControl(this.backgroundFade);
            this.headerLabelsImage.Size = new Size((base.Width - 0x19) - 0x17, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 5);
            this.mainBackgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.headerLabelsImage2.Size = new Size((base.Width - 0x19) - 0x17, 0x1c);
            this.headerLabelsImage2.Position = new Point(0x19, this.blockYSize + 5);
            this.mainBackgroundImage.addControl(this.headerLabelsImage2);
            this.headerLabelsImage2.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.alliesLabel.Text = SK.Text("FactionDiplomacy_Allies", "Allies");
            this.alliesLabel.Color = ARGBColors.Black;
            this.alliesLabel.Position = new Point(9, -2);
            this.alliesLabel.Size = new Size(0x143, this.headerLabelsImage.Height);
            this.alliesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.alliesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.alliesLabel);
            this.enemiesLabel.Text = SK.Text("FactionDiplomacy_Enemies", "Enemies");
            this.enemiesLabel.Color = ARGBColors.Black;
            this.enemiesLabel.Position = new Point(9, -2);
            this.enemiesLabel.Size = new Size(0x143, this.headerLabelsImage.Height);
            this.enemiesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.enemiesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage2.addControl(this.enemiesLabel);
            InterfaceMgr.Instance.setVillageHeading(SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy"));
            this.alliesScrollArea.Position = new Point(0x19, 40);
            this.alliesScrollArea.Size = new Size(0x393, (this.blockYSize - 40) - 10);
            this.alliesScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, (this.blockYSize - 40) - 10));
            this.mainBackgroundImage.addControl(this.alliesScrollArea);
            this.mouseWheelOverlay1.Position = this.alliesScrollArea.Position;
            this.mouseWheelOverlay1.Size = this.alliesScrollArea.Size;
            this.mouseWheelOverlay1.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved1));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay1);
            int num1 = this.alliesScrollBar.Value;
            this.alliesScrollBar.Position = new Point(0x3a5, 40);
            this.alliesScrollBar.Size = new Size(0x18, (this.blockYSize - 40) - 10);
            this.mainBackgroundImage.addControl(this.alliesScrollBar);
            this.alliesScrollBar.Value = 0;
            this.alliesScrollBar.Max = 100;
            this.alliesScrollBar.NumVisibleLines = 0x19;
            this.alliesScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.alliesScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            this.enemiesScrollArea.Position = new Point(0x19, (0x23 + this.blockYSize) + 5);
            this.enemiesScrollArea.Size = new Size(0x393, (this.blockYSize - 40) - 10);
            this.enemiesScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, (this.blockYSize - 40) - 10));
            this.mainBackgroundImage.addControl(this.enemiesScrollArea);
            this.mouseWheelOverlay2.Position = this.enemiesScrollArea.Position;
            this.mouseWheelOverlay2.Size = this.enemiesScrollArea.Size;
            this.mouseWheelOverlay2.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved2));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay2);
            int num2 = this.enemiesScrollBar.Value;
            this.enemiesScrollBar.Position = new Point(0x3a5, (0x23 + this.blockYSize) + 5);
            this.enemiesScrollBar.Size = new Size(0x18, (this.blockYSize - 40) - 10);
            this.mainBackgroundImage.addControl(this.enemiesScrollBar);
            this.enemiesScrollBar.Value = 0;
            this.enemiesScrollBar.Max = 100;
            this.enemiesScrollBar.NumVisibleLines = 0x19;
            this.enemiesScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.enemiesScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.incomingWallScrollBarMoved));
            this.addPlayers();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "UserDiplomacyPanel";
            base.Size = new Size(0x3e0, 0x236);
            base.ResumeLayout(false);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        private void loadDiplomacyData()
        {
            string str = GameEngine.getSettingsPath(false);
            FileStream input = null;
            BinaryReader reader = null;
            try
            {
                input = new FileStream(str + @"\DiplomacyDataW" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + "U" + RemoteServices.Instance.UserID.ToString() + ".dat", FileMode.Open);
                reader = new BinaryReader(input);
                byte[] b = new byte[0x10];
                b = reader.ReadBytes(0x10);
                Guid guid = new Guid(b);
                if (RemoteServices.Instance.WorldGUID.CompareTo(guid) != 0)
                {
                    reader.Close();
                    input.Close();
                }
                else
                {
                    customNotes.Clear();
                    for (int i = 0; i < GameEngine.Instance.World.UserRelations.Count; i++)
                    {
                        customNotes.Add(reader.ReadString());
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void logout()
        {
        }

        private void mouseWheelMoved1(int delta)
        {
            if (this.alliesScrollBar.Visible)
            {
                if (delta < 0)
                {
                    this.alliesScrollBar.scrollDown(40);
                }
                else if (delta > 0)
                {
                    this.alliesScrollBar.scrollUp(40);
                }
            }
        }

        private void mouseWheelMoved2(int delta)
        {
            if (this.enemiesScrollBar.Visible)
            {
                if (delta < 0)
                {
                    this.enemiesScrollBar.scrollDown(40);
                }
                else if (delta > 0)
                {
                    this.enemiesScrollBar.scrollUp(40);
                }
            }
        }

        public void update()
        {
        }

        private void wallScrollBarMoved()
        {
            int y = this.alliesScrollBar.Value;
            this.alliesScrollArea.Position = new Point(this.alliesScrollArea.X, 40 - y);
            this.alliesScrollArea.ClipRect = new Rectangle(this.alliesScrollArea.ClipRect.X, y, this.alliesScrollArea.ClipRect.Width, this.alliesScrollArea.ClipRect.Height);
            this.alliesScrollArea.invalidate();
            this.alliesScrollBar.invalidate();
        }

        public class UserAllianceLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLine divider = new CustomSelfDrawPanel.CSDLine();
            private CustomSelfDrawPanel.CSDLabel factionName = new CustomSelfDrawPanel.CSDLabel();
            private UserDiplomacyPanel m_parent;
            private int m_position = -1000;
            private int m_userID = -1;
            private CustomSelfDrawPanel.CSDLabel playerNotes = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDButton playerNotesButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDButton trash = new CustomSelfDrawPanel.CSDButton();

            private void deleteClicked()
            {
                GameEngine.Instance.World.setUserRelationship(this.m_userID, 0, "");
                RemoteServices.Instance.CreateUserRelationship(this.m_userID, 0);
                this.m_parent.init(true);
            }

            public void init(string username, int userID, int position, bool ally, UserDiplomacyPanel parent)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.m_userID = userID;
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_light;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_dark;
                }
                this.backgroundImage.Position = new Point(40, 0);
                this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.userClick));
                base.addControl(this.backgroundImage);
                this.Size = this.backgroundImage.Size;
                this.shieldImage.Image = GameEngine.Instance.World.getWorldShield(userID, 0x19, 0x1c);
                if (this.shieldImage.Image != null)
                {
                    this.shieldImage.Position = new Point(0, 0);
                    this.shieldImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.userClick));
                    base.addControl(this.shieldImage);
                }
                this.factionName.Text = username;
                this.factionName.Color = ARGBColors.Black;
                this.factionName.Position = new Point(9, 0);
                this.factionName.Size = new Size(500, this.backgroundImage.Height);
                this.factionName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.factionName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.factionName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.userClick));
                this.backgroundImage.addControl(this.factionName);
                this.trash.ImageNorm = (Image) GFXLibrary.trashcan_normal;
                this.trash.ImageOver = (Image) GFXLibrary.trashcan_over;
                this.trash.ImageClick = (Image) GFXLibrary.trashcan_clicked;
                this.trash.Position = new Point(830, 4);
                this.trash.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClicked), "FactionNewForumPanel_delete_thread");
                this.trash.CustomTooltipID = 0xc1e;
                this.backgroundImage.addControl(this.trash);
                this.playerNotesButton.ImageNormAndOver = (Image) GFXLibrary.faction_pen;
                this.playerNotesButton.Position = new Point(800, 4);
                this.playerNotesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.setNote), "Set_Player_Notes");
                this.playerNotesButton.CustomTooltipID = 0xc1f;
                this.backgroundImage.addControl(this.playerNotesButton);
                this.playerNotes.Text = "";
                this.playerNotes.Color = ARGBColors.Black;
                this.playerNotes.Position = new Point(this.factionName.Position.X + 240, 4);
                this.playerNotes.Size = new Size(500, this.backgroundImage.Height);
                this.playerNotes.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.playerNotes.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.playerNotes);
                this.divider.Position = new Point(this.playerNotes.Position.X - 5, 5);
                this.divider.Height = this.backgroundImage.Height - 10;
                this.divider.Width = 0;
                this.divider.LineColor = ARGBColors.Black;
                this.backgroundImage.addControl(this.divider);
                for (int i = 0; i < UserDiplomacyPanel.customNotes.Count; i++)
                {
                    string[] strArray = UserDiplomacyPanel.customNotes[i].Split(new char[] { '#' });
                    if ((strArray[0] == username) && (strArray[1] == GameEngine.Instance.World.GetGlobalWorldID().ToString()))
                    {
                        this.playerNotes.Text = strArray[2];
                        if (strArray.Length > 3)
                        {
                            for (int j = 3; j < strArray.Length; j++)
                            {
                                this.playerNotes.Text = this.playerNotes.Text + "#" + strArray[j];
                            }
                        }
                    }
                }
            }

            private void saveDiplomacyData()
            {
                string str = GameEngine.getSettingsPath(true);
                try
                {
                    FileInfo info = new FileInfo(str + @"\DiplomacyDataW" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + "U" + RemoteServices.Instance.UserID.ToString() + ".dat") {
                        IsReadOnly = false
                    };
                }
                catch (Exception)
                {
                }
                try
                {
                    FileStream output = new FileStream(str + @"\DiplomacyDataW" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + "U" + RemoteServices.Instance.UserID.ToString() + ".dat", FileMode.Create);
                    BinaryWriter writer = new BinaryWriter(output);
                    byte[] buffer = RemoteServices.Instance.WorldGUID.ToByteArray();
                    writer.Write(buffer, 0, 0x10);
                    for (int i = 0; i < UserDiplomacyPanel.customNotes.Count; i++)
                    {
                        writer.Write(UserDiplomacyPanel.customNotes[i]);
                    }
                    writer.Close();
                    output.Close();
                }
                catch (Exception)
                {
                }
            }

            private void setNote()
            {
                InterfaceMgr.Instance.setFloatingTextSentDelegate(new InterfaceMgr.FloatingTextSent(this.setText));
                Point point = this.m_parent.PointToScreen(new Point(this.playerNotes.getPanelPosition().X, this.playerNotes.getPanelPosition().Y + 4));
                FloatingInputText.openDisband(point.X, point.Y, this.playerNotes.Text, InterfaceMgr.Instance.ParentForm);
            }

            private void setText(string str)
            {
                this.playerNotes.Text = str;
                bool flag = false;
                string item = this.factionName.Text + "#" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + "#" + str;
                for (int i = 0; i < UserDiplomacyPanel.customNotes.Count; i++)
                {
                    if (UserDiplomacyPanel.customNotes[i].Split(new char[] { '#' })[0] == this.factionName.Text)
                    {
                        UserDiplomacyPanel.customNotes[i] = item;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    UserDiplomacyPanel.customNotes.Add(item);
                }
                this.saveDiplomacyData();
            }

            public void update()
            {
            }

            private void userClick()
            {
                GameEngine.Instance.playInterfaceSound("FactionDiplomacyPanel_faction_clicked");
                InterfaceMgr.Instance.changeTab(0);
                WorldMap.CachedUserInfo userInfo = new WorldMap.CachedUserInfo {
                    userID = this.m_userID
                };
                InterfaceMgr.Instance.showUserInfoScreen(userInfo);
            }
        }
    }
}

