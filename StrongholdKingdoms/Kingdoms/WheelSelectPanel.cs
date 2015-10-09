namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class WheelSelectPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private static WheelSelectPanel Instance;
        private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDExtendingPanel MainPanel = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel questLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton questWheelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton treasure1WheelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton treasure2WheelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton treasure3WheelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton treasure4WheelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton treasure5WheelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel treasureLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel treasureTier1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel treasureTier2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel treasureTier3Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel treasureTier4Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel treasureTier5Label = new CustomSelfDrawPanel.CSDLabel();

        public WheelSelectPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.closeWheelSelectPopup();
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

        public void init(bool initialCall)
        {
            CustomSelfDrawPanel.CSDImage image2;
            Instance = this;
            base.clearControls();
            this.mainBackgroundImage.Image = GFXLibrary.dummy;
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = base.Size;
            this.mainBackgroundImage.Tile = true;
            base.addControl(this.mainBackgroundImage);
            this.MainPanel.Size = base.Size;
            this.MainPanel.Position = new Point(0, 0);
            this.mainBackgroundImage.addControl(this.MainPanel);
            this.MainPanel.Create((Image) GFXLibrary.cardpanel_panel_back_top_left, (Image) GFXLibrary.cardpanel_panel_back_top_mid, (Image) GFXLibrary.cardpanel_panel_back_top_right, (Image) GFXLibrary.cardpanel_panel_back_mid_left, (Image) GFXLibrary.cardpanel_panel_back_mid_mid, (Image) GFXLibrary.cardpanel_panel_back_mid_right, (Image) GFXLibrary.cardpanel_panel_back_bottom_left, (Image) GFXLibrary.cardpanel_panel_back_bottom_mid, (Image) GFXLibrary.cardpanel_panel_back_bottom_right);
            CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.cardpanel_panel_gradient_top_left,
                Size = GFXLibrary.cardpanel_panel_gradient_top_left.Size,
                Position = new Point(0, 0)
            };
            this.MainPanel.addControl(control);
            image2 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.cardpanel_panel_gradient_bottom_right,
                Size = GFXLibrary.cardpanel_panel_gradient_bottom_right.Size,
                Position = new Point((this.MainPanel.Width - ((Image)GFXLibrary.cardpanel_panel_gradient_bottom_right).Width) - 6, (this.MainPanel.Height - ((Image) GFXLibrary.cardpanel_panel_gradient_bottom_right).Height) - 6)
            };
            this.MainPanel.addControl(image2);
            this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
            this.closeImage.Size = this.closeImage.Image.Size;
            this.closeImage.setMouseOverDelegate(() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over, () => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal);
            this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
            this.closeImage.Position = new Point((base.Width - 14) - 0x11, 10);
            this.closeImage.CustomTooltipID = 0x2774;
            this.mainBackgroundImage.addControl(this.closeImage);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundImage, 0x20, new Point((base.Width - 40) - 40, 2));
            CustomSelfDrawPanel.CSDFill fill = new CustomSelfDrawPanel.CSDFill {
                FillColor = Color.FromArgb(0xff, 130, 0x81, 0x7e),
                Size = new Size(base.Width - 10, 1),
                Position = new Point(5, 0x22)
            };
            this.mainBackgroundImage.addControl(fill);
            int x = 10;
            int num2 = 0x2d;
            int num3 = 160;
            int y = 110;
            this.questWheelButton.ImageNorm = (Image) GFXLibrary.wheel_spinButton_royal[0];
            this.questWheelButton.ImageOver = (Image) GFXLibrary.wheel_spinButton_royal[1];
            this.questWheelButton.Data = -1;
            this.questWheelButton.MoveOnClick = false;
            this.questWheelButton.Position = new Point(x, y);
            this.questWheelButton.Text.Text = GameEngine.Instance.World.getTickets(this.questWheelButton.Data).ToString();
            this.questWheelButton.TextYOffset = 0x20;
            this.questWheelButton.Text.Color = ARGBColors.Black;
            this.questWheelButton.Text.DropShadowColor = Color.FromArgb(160, 160, 160);
            this.questWheelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.questWheelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openWheel));
            this.mainBackgroundImage.addControl(this.questWheelButton);
            this.questWheelButton.Enabled = GameEngine.Instance.World.getTickets(this.questWheelButton.Data) > 0;
            this.treasure1WheelButton.ImageNorm = (Image) GFXLibrary.wheel_spinButton_royal[0];
            this.treasure1WheelButton.ImageOver = (Image) GFXLibrary.wheel_spinButton_royal[1];
            this.treasure1WheelButton.Data = 0;
            this.treasure1WheelButton.MoveOnClick = false;
            this.treasure1WheelButton.Position = new Point((x + num2) + num3, y);
            this.treasure1WheelButton.Text.Text = GameEngine.Instance.World.getTickets(this.treasure1WheelButton.Data).ToString();
            this.treasure1WheelButton.TextYOffset = 0x20;
            this.treasure1WheelButton.Text.Color = ARGBColors.Black;
            this.treasure1WheelButton.Text.DropShadowColor = Color.FromArgb(160, 160, 160);
            this.treasure1WheelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.treasure1WheelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openWheel));
            this.mainBackgroundImage.addControl(this.treasure1WheelButton);
            this.treasure1WheelButton.Enabled = GameEngine.Instance.World.getTickets(this.treasure1WheelButton.Data) > 0;
            this.treasure2WheelButton.ImageNorm = (Image) GFXLibrary.wheel_spinButton_royal[0];
            this.treasure2WheelButton.ImageOver = (Image) GFXLibrary.wheel_spinButton_royal[1];
            this.treasure2WheelButton.Data = 1;
            this.treasure2WheelButton.MoveOnClick = false;
            this.treasure2WheelButton.Position = new Point((x + num2) + (num3 * 2), y);
            this.treasure2WheelButton.Text.Text = GameEngine.Instance.World.getTickets(this.treasure2WheelButton.Data).ToString();
            this.treasure2WheelButton.TextYOffset = 0x20;
            this.treasure2WheelButton.Text.Color = ARGBColors.Black;
            this.treasure2WheelButton.Text.DropShadowColor = Color.FromArgb(160, 160, 160);
            this.treasure2WheelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.treasure2WheelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openWheel));
            this.mainBackgroundImage.addControl(this.treasure2WheelButton);
            this.treasure2WheelButton.Enabled = GameEngine.Instance.World.getTickets(this.treasure2WheelButton.Data) > 0;
            this.treasure3WheelButton.ImageNorm = (Image) GFXLibrary.wheel_spinButton_royal[0];
            this.treasure3WheelButton.ImageOver = (Image) GFXLibrary.wheel_spinButton_royal[1];
            this.treasure3WheelButton.Data = 2;
            this.treasure3WheelButton.MoveOnClick = false;
            this.treasure3WheelButton.Position = new Point((x + num2) + (num3 * 3), y);
            this.treasure3WheelButton.Text.Text = GameEngine.Instance.World.getTickets(this.treasure3WheelButton.Data).ToString();
            this.treasure3WheelButton.TextYOffset = 0x20;
            this.treasure3WheelButton.Text.Color = ARGBColors.Black;
            this.treasure3WheelButton.Text.DropShadowColor = Color.FromArgb(160, 160, 160);
            this.treasure3WheelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.treasure3WheelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openWheel));
            this.mainBackgroundImage.addControl(this.treasure3WheelButton);
            this.treasure3WheelButton.Enabled = GameEngine.Instance.World.getTickets(this.treasure3WheelButton.Data) > 0;
            this.treasure4WheelButton.ImageNorm = (Image) GFXLibrary.wheel_spinButton_royal[0];
            this.treasure4WheelButton.ImageOver = (Image) GFXLibrary.wheel_spinButton_royal[1];
            this.treasure4WheelButton.Data = 3;
            this.treasure4WheelButton.MoveOnClick = false;
            this.treasure4WheelButton.Position = new Point((x + num2) + (num3 * 4), y);
            this.treasure4WheelButton.Text.Text = GameEngine.Instance.World.getTickets(this.treasure4WheelButton.Data).ToString();
            this.treasure4WheelButton.TextYOffset = 0x20;
            this.treasure4WheelButton.Text.Color = ARGBColors.Black;
            this.treasure4WheelButton.Text.DropShadowColor = Color.FromArgb(160, 160, 160);
            this.treasure4WheelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.treasure4WheelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openWheel));
            this.mainBackgroundImage.addControl(this.treasure4WheelButton);
            this.treasure4WheelButton.Enabled = GameEngine.Instance.World.getTickets(this.treasure4WheelButton.Data) > 0;
            this.treasure5WheelButton.ImageNorm = (Image) GFXLibrary.wheel_spinButton_royal[0];
            this.treasure5WheelButton.ImageOver = (Image) GFXLibrary.wheel_spinButton_royal[1];
            this.treasure5WheelButton.Data = 4;
            this.treasure5WheelButton.MoveOnClick = false;
            this.treasure5WheelButton.Position = new Point((x + num2) + (num3 * 5), y);
            this.treasure5WheelButton.Text.Text = GameEngine.Instance.World.getTickets(this.treasure5WheelButton.Data).ToString();
            this.treasure5WheelButton.TextYOffset = 0x20;
            this.treasure5WheelButton.Text.Color = ARGBColors.Black;
            this.treasure5WheelButton.Text.DropShadowColor = Color.FromArgb(160, 160, 160);
            this.treasure5WheelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.treasure5WheelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openWheel));
            this.mainBackgroundImage.addControl(this.treasure5WheelButton);
            this.treasure5WheelButton.Enabled = GameEngine.Instance.World.getTickets(this.treasure5WheelButton.Data) > 0;
            this.labelTitle.Text = SK.Text("WheelSelectPanel_SelectType", "Select Wheel Type");
            this.labelTitle.Position = new Point(0, 5);
            this.labelTitle.Size = new Size(base.Width, 0x40);
            this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.labelTitle.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
            this.labelTitle.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelTitle);
            this.questLabel.Text = SK.Text("WheelSelectPanel_Quest", "Quest");
            this.questLabel.Position = new Point(this.questWheelButton.X - 8, 50);
            this.questLabel.Size = new Size(150, 0x40);
            this.questLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.questLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.questLabel.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.questLabel);
            this.treasureLabel.Text = SK.Text("WheelSelectPanel_Treasure", "Treasure Castle");
            this.treasureLabel.Position = new Point(0xca, 50);
            this.treasureLabel.Size = new Size(800, 0x40);
            this.treasureLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.treasureLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.treasureLabel.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.treasureLabel);
            this.treasureTier1Label.Text = SK.Text("WheelSelectPanel_Tier1", "Tier 1");
            this.treasureTier1Label.Position = new Point(this.treasure1WheelButton.X - 8, 80);
            this.treasureTier1Label.Size = new Size(150, 0x40);
            this.treasureTier1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.treasureTier1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.treasureTier1Label.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.treasureTier1Label);
            this.treasureTier2Label.Text = SK.Text("WheelSelectPanel_Tier2", "Tier 2");
            this.treasureTier2Label.Position = new Point(this.treasure2WheelButton.X - 8, 80);
            this.treasureTier2Label.Size = new Size(150, 0x40);
            this.treasureTier2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.treasureTier2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.treasureTier2Label.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.treasureTier2Label);
            this.treasureTier3Label.Text = SK.Text("WheelSelectPanel_Tier3", "Tier 3");
            this.treasureTier3Label.Position = new Point(this.treasure3WheelButton.X - 8, 80);
            this.treasureTier3Label.Size = new Size(150, 0x40);
            this.treasureTier3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.treasureTier3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.treasureTier3Label.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.treasureTier3Label);
            this.treasureTier4Label.Text = SK.Text("WheelSelectPanel_Tier4", "Tier 4");
            this.treasureTier4Label.Position = new Point(this.treasure4WheelButton.X - 8, 80);
            this.treasureTier4Label.Size = new Size(150, 0x40);
            this.treasureTier4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.treasureTier4Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.treasureTier4Label.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.treasureTier4Label);
            this.treasureTier5Label.Text = SK.Text("WheelSelectPanel_Tier5", "Tier 5");
            this.treasureTier5Label.Position = new Point(this.treasure5WheelButton.X - 8, 80);
            this.treasureTier5Label.Size = new Size(150, 0x40);
            this.treasureTier5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.treasureTier5Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.treasureTier5Label.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.treasureTier5Label);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void openWheel()
        {
            if (base.ClickedControl != null)
            {
                int data = base.ClickedControl.Data;
                InterfaceMgr.Instance.closeWheelSelectPopup();
                InterfaceMgr.Instance.openWheelPopup(data);
            }
        }
    }
}

