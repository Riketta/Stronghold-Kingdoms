namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AchievementPopup : Form
    {
        private MenuBackground background = new MenuBackground();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel content = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton gotoButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage icon = new CustomSelfDrawPanel.CSDImage();
        private bool isInside;
        private int lifespan;
        private const int MAX_LIFESPAN = 450;
        private int offsetY;
        private CustomSelfDrawPanel.CSDLabel title = new CustomSelfDrawPanel.CSDLabel();

        public AchievementPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.BackColor = Color.FromArgb(0xff, 240, 240, 240);
            this.background.Size = new Size(300, 100);
            this.background.BackColor = Color.FromArgb(0xff, 240, 240, 240);
            base.Controls.Add(this.background);
            this.background.MouseEnter += new EventHandler(this.enterFunction);
            this.background.MouseLeave += new EventHandler(this.exitFunction);
        }

        public void activate(int id)
        {
            base.Location = new Point((InterfaceMgr.Instance.ParentMainWindow.Location.X + (InterfaceMgr.Instance.ParentMainWindow.Width / 2)) - 150, ((InterfaceMgr.Instance.ParentMainWindow.Location.Y + InterfaceMgr.Instance.ParentMainWindow.Height) - 100) - 10);
            base.Width = 300;
            base.Height = 100;
            this.lifespan = 450;
            FontStyle bold = FontStyle.Bold;
            this.title.Size = new Size(300, 20);
            this.title.Text = "";
            this.title.Position = new Point(0, 10);
            this.title.Font = FontManager.GetFont("Microsoft Sans Serif", 12f, bold);
            this.title.Color = ARGBColors.Black;
            this.title.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.title.Visible = true;
            this.background.addControl(this.title);
            bold = FontStyle.Regular;
            this.content.Size = new Size(200, 60);
            this.content.Text = "";
            this.content.Position = new Point(50, 30);
            this.content.Font = FontManager.GetFont("Microsoft Sans Serif", 10f, bold);
            this.content.Color = ARGBColors.Black;
            this.content.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.content.Visible = true;
            this.background.addControl(this.content);
            this.gotoButton.Size = new Size(290, 90);
            this.gotoButton.Position = new Point(5, 5);
            this.gotoButton.FillRectColor = Color.FromArgb(0, 200, 220, 200);
            this.gotoButton.FillRectOverColor = Color.FromArgb(0, 210, 230, 210);
            this.gotoButton.Text.Position = new Point(0, 0);
            this.gotoButton.Text.Size = new Size(140, 20);
            this.gotoButton.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f, bold);
            this.gotoButton.Text.Color = ARGBColors.Black;
            this.gotoButton.TextYOffset = 0;
            this.gotoButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.gotoButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openAchievementsFunction));
            this.background.addControl(this.gotoButton);
            this.closeButton.Size = new Size(150, 30);
            this.closeButton.Position = new Point(90, 70);
            this.closeButton.Text.Text = SK.Text("GENERIC_Close", "Close");
            this.closeButton.Text.Position = new Point(0, 7);
            this.closeButton.Text.Size = new Size(150, 20);
            this.closeButton.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f, bold);
            this.closeButton.Text.Color = ARGBColors.Black;
            this.closeButton.TextYOffset = 0;
            this.closeButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeFunction));
            this.closeButton.ImageNorm = (Image) GFXLibrary.button_blue_01_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.button_blue_01_over;
            this.background.addControl(this.closeButton);
            this.icon.Position = new Point(2, 10);
            this.icon.Image = (Image) GFXLibrary.achievement_ribbons_centre[0];
            this.background.addControl(this.icon);
            base.Opacity = 0.0;
            this.populateControls(id);
            base.Show(InterfaceMgr.Instance.ParentForm);
        }

        private void closeFunction()
        {
            base.Visible = false;
            this.lifespan = 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void enterFunction(object sender, EventArgs e)
        {
            this.isInside = (this.lifespan > 15) && (this.lifespan < 0x1be);
        }

        private void exitFunction(object sender, EventArgs e)
        {
            this.isInside = false;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0xd5, 0x17);
            base.ControlBox = false;
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "AchievementPopup";
            base.Opacity = 0.75;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "AchievementPopup";
            base.ResumeLayout(false);
        }

        public bool isActive()
        {
            return (this.lifespan > 0);
        }

        public bool isMouseInside()
        {
            return this.isInside;
        }

        public void move()
        {
            base.Location = new Point((InterfaceMgr.Instance.ParentMainWindow.Location.X + (InterfaceMgr.Instance.ParentMainWindow.Width / 2)) - 150, (((InterfaceMgr.Instance.ParentMainWindow.Location.Y + InterfaceMgr.Instance.ParentMainWindow.Height) - 100) - 10) + this.offsetY);
        }

        private void openAchievementsFunction()
        {
            this.closeFunction();
            InterfaceMgr.Instance.getMainTabBar().changeTab(4);
        }

        public void populateControls(int id)
        {
            if (id >= 0x3e8)
            {
                this.title.Text = SK.Text("ACHIEVEMENT_OBTAINED", "Achievement Obtained!");
                string str = CustomTooltipManager.getAchievementTitle(id - 0x3e8) + " (" + CustomTooltipManager.getAchievementRank(id - 0x3e8) + ")";
                this.content.Text = str;
                this.icon.Image = (Image) GFXLibrary.medal_images[CustomSelfDrawPanel.MedalImage.getAchievementImage((id - 0x3e8) & 0xfff)];
            }
        }

        public void setClickDelegate(CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate del)
        {
            this.gotoButton.setClickDelegate(del);
        }

        public void update()
        {
            base.Location = new Point((InterfaceMgr.Instance.ParentMainWindow.Location.X + (InterfaceMgr.Instance.ParentMainWindow.Width / 2)) - 150, ((InterfaceMgr.Instance.ParentMainWindow.Location.Y + InterfaceMgr.Instance.ParentMainWindow.Height) - 100) - 10);
            if (!this.isInside)
            {
                if (this.lifespan > 0)
                {
                    this.lifespan--;
                }
                if (this.lifespan > 0x1bd)
                {
                    base.Opacity += 0.15;
                }
                else if (this.lifespan < 0x10)
                {
                    base.Height -= 10;
                    this.offsetY += 10;
                    base.Location = new Point(base.Location.X, base.Location.Y + this.offsetY);
                }
                if (this.lifespan == 4)
                {
                    base.Visible = false;
                }
                base.Invalidate();
            }
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }
    }
}

