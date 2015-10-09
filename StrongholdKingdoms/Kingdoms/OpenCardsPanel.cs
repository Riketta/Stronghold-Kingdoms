namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class OpenCardsPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
    {
        private CustomSelfDrawPanel.CSDExtendingPanel AvailablePanel;
        private CustomSelfDrawPanel.CSDImage AvailablePanelContent = new CustomSelfDrawPanel.CSDImage();
        private int AvailablePanelWidth;
        private static int BorderPadding = 0x10;
        private static Bitmap buttonpic;
        private CustomSelfDrawPanel.CSDImage buybutton = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private int ContentWidth;
        private CustomSelfDrawPanel.CSDImage crownsbutton = new CustomSelfDrawPanel.CSDImage();
        private int currentCardSection = -1;
        private Bitmap greenbar = new Bitmap(0x1d, 3);
        private CustomSelfDrawPanel.CSDImage InplayPanelContent = new CustomSelfDrawPanel.CSDImage();
        private int InplayPanelWidth;
        private CustomSelfDrawPanel.CSDLabel labelFeedback = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
        private DateTime lastRefresh = DateTime.Now;
        private DateTime lastTickCall = DateTime.Now.AddSeconds(-60.0);
        private DateTime lastUpdatedProgressBars = DateTime.Now.AddSeconds(30.0);
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage managebutton = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage playbutton = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage premiumbutton = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDVertScrollBar scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDVertScrollBar scrollbarInplay = new CustomSelfDrawPanel.CSDVertScrollBar();

        public OpenCardsPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.closePlayCardsWindow();
            InterfaceMgr.Instance.ParentForm.TopMost = true;
            InterfaceMgr.Instance.ParentForm.TopMost = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(int cardSection)
        {
            this.currentCardSection = cardSection;
            base.clearControls();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_001;
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = base.Size;
            this.mainBackgroundImage.Tile = true;
            base.addControl(this.mainBackgroundImage);
            this.ContentWidth = base.Width - (2 * BorderPadding);
            this.AvailablePanelWidth = (this.ContentWidth - 150) - 40;
            this.InplayPanelWidth = (this.ContentWidth - BorderPadding) - this.AvailablePanelWidth;
            this.AvailablePanel = new CustomSelfDrawPanel.CSDExtendingPanel();
            this.AvailablePanel.Size = new Size(this.AvailablePanelWidth, (base.Height - 0x10) - BorderPadding);
            this.AvailablePanel.Position = new Point(0x10, 0x10);
            this.mainBackgroundImage.addControl(this.AvailablePanel);
            this.AvailablePanel.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
            int width = (base.Width - (BorderPadding * 3)) - this.AvailablePanel.Width;
            int height = 100;
            if (buttonpic == null)
            {
                buttonpic = new Bitmap(width, height);
                using (Graphics graphics = Graphics.FromImage(buttonpic))
                {
                    Brush green = Brushes.Green;
                    graphics.FillRectangle(green, new Rectangle(new Point(0, 0), buttonpic.Size));
                }
            }
            this.closeButton.Size = new Size(width, 0x26);
            this.closeButton.Text.Text = SK.Text("GENERIC_Close", "Close");
            this.closeButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.closeButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.closeButton.TextYOffset = -1;
            this.closeButton.Text.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.closeButton);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
            this.closeButton.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.closeButton.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.closeButton.Position = new Point((base.Width - this.closeButton.Width) - BorderPadding, BorderPadding);
            this.playbutton.Size = new Size(width, height);
            this.playbutton.Position = new Point((base.Width - this.closeButton.Width) - BorderPadding, (this.closeButton.Y + this.closeButton.Height) + (BorderPadding / 2));
            this.playbutton.Image = buttonpic;
            this.mainBackgroundImage.addControl(this.playbutton);
            CustomSelfDrawPanel.CSDLabel control = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, 0),
                Size = new Size(width, height),
                Text = SK.Text("OpenCardsPanel_Open_Cards", "Open Cards"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Color = ARGBColors.White,
                DropShadowColor = ARGBColors.Black
            };
            this.playbutton.addControl(control);
            this.buybutton.Size = new Size(width, 100);
            this.buybutton.Position = new Point((base.Width - this.closeButton.Width) - BorderPadding, (this.playbutton.Y + this.playbutton.Height) + (BorderPadding / 2));
            this.buybutton.Image = buttonpic;
            this.mainBackgroundImage.addControl(this.buybutton);
            control = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, 0),
                Size = new Size(width, height),
                Text = SK.Text("OpenCardsPanel_Buy_Cards", "Buy Cards"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Color = ARGBColors.White,
                DropShadowColor = ARGBColors.Black
            };
            this.buybutton.addControl(control);
            this.premiumbutton.Size = new Size(width, 100);
            this.premiumbutton.Position = new Point((base.Width - this.closeButton.Width) - BorderPadding, (this.buybutton.Y + this.buybutton.Height) + (BorderPadding / 2));
            this.premiumbutton.Image = buttonpic;
            this.mainBackgroundImage.addControl(this.premiumbutton);
            control = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, 0),
                Size = new Size(width, height),
                Text = SK.Text("OpenCardsPanel_Premium", "Premium"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Color = ARGBColors.White,
                DropShadowColor = ARGBColors.Black
            };
            this.premiumbutton.addControl(control);
            this.managebutton.Size = new Size(width, 100);
            this.managebutton.Position = new Point((base.Width - this.closeButton.Width) - BorderPadding, (this.premiumbutton.Y + this.premiumbutton.Height) + (BorderPadding / 2));
            this.managebutton.Image = buttonpic;
            this.mainBackgroundImage.addControl(this.managebutton);
            control = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, 0),
                Size = new Size(width, height),
                Text = SK.Text("OpenCardsPanel_Manage_Cards", "Manage Cards"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Color = ARGBColors.White,
                DropShadowColor = ARGBColors.Black
            };
            this.managebutton.addControl(control);
            this.crownsbutton.Size = new Size(width, 100);
            this.crownsbutton.Position = new Point((base.Width - this.closeButton.Width) - BorderPadding, (this.managebutton.Y + this.managebutton.Height) + (BorderPadding / 2));
            this.crownsbutton.Image = buttonpic;
            this.mainBackgroundImage.addControl(this.crownsbutton);
            control = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, 0),
                Size = new Size(width, height),
                Text = SK.Text("OpenCardsPanel_Get_Crowns", "Get Crowns"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Color = ARGBColors.White,
                DropShadowColor = ARGBColors.Black
            };
            this.crownsbutton.addControl(control);
            this.labelTitle.Position = new Point(BorderPadding, 2);
            this.labelTitle.Size = new Size(300, 0x40);
            this.labelTitle.Text = SK.Text("OpenCardsPanel_Latest_Offers", "Latest Offers");
            this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelTitle.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.labelTitle.Color = ARGBColors.White;
            this.labelTitle.DropShadowColor = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelTitle);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        public void update()
        {
        }
    }
}

