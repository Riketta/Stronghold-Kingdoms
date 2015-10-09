namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FactionDiplomacyPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDLabel alliesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea alliesScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar alliesScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private int blockYSize;
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDLabel enemiesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea enemiesScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar enemiesScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage2 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        public static FactionDiplomacyPanel instance;
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay1 = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay2 = new CustomSelfDrawPanel.CSDControl();
        public const int PANEL_ID = 0x2c;
        private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();

        public FactionDiplomacyPanel()
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
            int position = 0;
            int[] factionAllies = GameEngine.Instance.World.FactionAllies;
            if (factionAllies != null)
            {
                foreach (int num3 in factionAllies)
                {
                    FactionData factionData = GameEngine.Instance.World.getFaction(num3);
                    if ((factionData != null) && factionData.active)
                    {
                        FactionsAllianceLine control = new FactionsAllianceLine();
                        if (y != 0)
                        {
                            y += 5;
                        }
                        control.Position = new Point(0, y);
                        control.init(factionData, position, true, this);
                        this.alliesScrollArea.addControl(control);
                        y += control.Height;
                        position++;
                    }
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
            y = 0;
            int[] factionEnemies = GameEngine.Instance.World.FactionEnemies;
            if (factionEnemies != null)
            {
                foreach (int num4 in factionEnemies)
                {
                    FactionData data2 = GameEngine.Instance.World.getFaction(num4);
                    if ((data2 != null) && data2.active)
                    {
                        FactionsAllianceLine line2 = new FactionsAllianceLine();
                        if (y != 0)
                        {
                            y += 5;
                        }
                        line2.Position = new Point(0, y);
                        line2.init(data2, position, false, this);
                        this.enemiesScrollArea.addControl(line2);
                        y += line2.Height;
                        position++;
                    }
                }
            }
            this.enemiesScrollArea.Size = new Size(this.enemiesScrollArea.Width, y);
            if (y < this.enemiesScrollBar.Height)
            {
                this.enemiesScrollBar.Visible = false;
            }
            else
            {
                this.enemiesScrollBar.Visible = true;
                this.enemiesScrollBar.NumVisibleLines = this.enemiesScrollBar.Height;
                this.enemiesScrollBar.Max = y - this.enemiesScrollBar.Height;
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
            this.sidebar.addSideBar(4, this);
            this.mainBackgroundImage.FillColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = new Size(base.Width - 200, height);
            base.addControl(this.mainBackgroundImage);
            this.backgroundFade.Image = (Image) GFXLibrary.background_top;
            this.backgroundFade.Position = new Point(0, 0);
            this.backgroundFade.Size = new Size(base.Width - 200, this.backgroundFade.Image.Height);
            this.mainBackgroundImage.addControl(this.backgroundFade);
            this.headerLabelsImage.Size = new Size(((base.Width - 0x19) - 0x17) - 200, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 5);
            this.mainBackgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.headerLabelsImage2.Size = new Size(((base.Width - 0x19) - 0x17) - 200, 0x1c);
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
            this.alliesScrollArea.Size = new Size(0x2cb, (this.blockYSize - 40) - 10);
            this.alliesScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, (this.blockYSize - 40) - 10));
            this.mainBackgroundImage.addControl(this.alliesScrollArea);
            this.mouseWheelOverlay1.Position = this.alliesScrollArea.Position;
            this.mouseWheelOverlay1.Size = this.alliesScrollArea.Size;
            this.mouseWheelOverlay1.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved1));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay1);
            int num1 = this.alliesScrollBar.Value;
            this.alliesScrollBar.Position = new Point(0x2dd, 40);
            this.alliesScrollBar.Size = new Size(0x18, (this.blockYSize - 40) - 10);
            this.mainBackgroundImage.addControl(this.alliesScrollBar);
            this.alliesScrollBar.Value = 0;
            this.alliesScrollBar.Max = 100;
            this.alliesScrollBar.NumVisibleLines = 0x19;
            this.alliesScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.alliesScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            this.enemiesScrollArea.Position = new Point(0x19, (0x23 + this.blockYSize) + 5);
            this.enemiesScrollArea.Size = new Size(0x2cb, (this.blockYSize - 40) - 10);
            this.enemiesScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, (this.blockYSize - 40) - 10));
            this.mainBackgroundImage.addControl(this.enemiesScrollArea);
            this.mouseWheelOverlay2.Position = this.enemiesScrollArea.Position;
            this.mouseWheelOverlay2.Size = this.enemiesScrollArea.Size;
            this.mouseWheelOverlay2.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved2));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay2);
            int num2 = this.enemiesScrollBar.Value;
            this.enemiesScrollBar.Position = new Point(0x2dd, (0x23 + this.blockYSize) + 5);
            this.enemiesScrollBar.Size = new Size(0x18, (this.blockYSize - 40) - 10);
            this.mainBackgroundImage.addControl(this.enemiesScrollBar);
            this.enemiesScrollBar.Value = 0;
            this.enemiesScrollBar.Max = 100;
            this.enemiesScrollBar.NumVisibleLines = 0x19;
            this.enemiesScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.enemiesScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.incomingWallScrollBarMoved));
            if (!resized)
            {
                CustomSelfDrawPanel.FactionPanelSideBar.downloadCurrentFactionInfo();
            }
            this.addPlayers();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "FactionDiplomacyPanel";
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
            this.sidebar.update();
        }

        private void wallScrollBarMoved()
        {
            int y = this.alliesScrollBar.Value;
            this.alliesScrollArea.Position = new Point(this.alliesScrollArea.X, 40 - y);
            this.alliesScrollArea.ClipRect = new Rectangle(this.alliesScrollArea.ClipRect.X, y, this.alliesScrollArea.ClipRect.Width, this.alliesScrollArea.ClipRect.Height);
            this.alliesScrollArea.invalidate();
            this.alliesScrollBar.invalidate();
        }

        public class FactionsAllianceLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel factionName = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDFactionFlagImage flagImage = new CustomSelfDrawPanel.CSDFactionFlagImage();
            private FactionData m_factionData;
            private FactionDiplomacyPanel m_parent;
            private int m_position = -1000;

            private void factionClick()
            {
                if (this.m_factionData != null)
                {
                    GameEngine.Instance.playInterfaceSound("FactionDiplomacyPanel_faction_clicked");
                    InterfaceMgr.Instance.showFactionPanel(this.m_factionData.factionID);
                }
            }

            public void init(FactionData factionData, int position, bool ally, FactionDiplomacyPanel parent)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.m_factionData = factionData;
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_light;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_dark;
                }
                this.backgroundImage.Position = new Point(60, 0);
                this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClick));
                base.addControl(this.backgroundImage);
                this.Size = this.backgroundImage.Size;
                this.flagImage.createFromFlagData(factionData.flagData);
                this.flagImage.Position = new Point(0, 0);
                this.flagImage.Scale = 0.25;
                this.flagImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClick));
                base.addControl(this.flagImage);
                this.factionName.Text = factionData.factionName;
                this.factionName.Color = ARGBColors.Black;
                this.factionName.Position = new Point(9, 0);
                this.factionName.Size = new Size(500, this.backgroundImage.Height);
                this.factionName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.factionName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.factionName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClick));
                this.backgroundImage.addControl(this.factionName);
            }

            public void update()
            {
            }
        }
    }
}

