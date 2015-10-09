namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class RankFacebookPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton facebookShareButton = new CustomSelfDrawPanel.CSDButton();
        private RankFacebookPopup m_parent;
        private CustomSelfDrawPanel.CSDLabel mainLabel = new CustomSelfDrawPanel.CSDLabel();
        public static bool shareClicked;

        public RankFacebookPanel()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            shareClicked = false;
        }

        private void closeClick()
        {
            if (this.m_parent != null)
            {
                this.m_parent.Close();
            }
        }

        private void facebookShareClicked()
        {
            shareClicked = true;
            this.closeClick();
        }

        public void init(RankFacebookPopup parent)
        {
            this.m_parent = parent;
            base.Size = this.m_parent.Size;
            this.BackColor = ARGBColors.Transparent;
            CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage {
                Alpha = 0.1f,
                Image = (Image) GFXLibrary.formations_img,
                Scale = 5.0,
                Position = new Point(0, 0),
                Size = base.Size
            };
            base.addControl(control);
            this.mainLabel.Text = SK.Text("FACEBOOK_SHARE_Info_Body", "Congratulations on Reaching Rank 10 (Thane). Share this achievement on Facebook and receive a free Random Card Pack!");
            this.mainLabel.Color = ARGBColors.Black;
            this.mainLabel.Position = new Point(10, 0);
            this.mainLabel.Size = new Size(430, 0x4b);
            this.mainLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
            this.mainLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            control.addControl(this.mainLabel);
            this.facebookShareButton.ImageNorm = (Image) GFXLibrary.facebookBrownNorm;
            this.facebookShareButton.ImageOver = (Image) GFXLibrary.facebookBrownOver;
            this.facebookShareButton.ImageClick = (Image) GFXLibrary.facebookBrownClick;
            this.facebookShareButton.Position = new Point(20, 80);
            this.facebookShareButton.UseTextSize = true;
            this.facebookShareButton.Text.Text = SK.Text("FACEBOOK_Share", "Share");
            this.facebookShareButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.facebookShareButton.Text.Position = new Point(20, 2);
            this.facebookShareButton.Text.Size = new Size(110, 0x15);
            this.facebookShareButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.facebookShareButton.TextYOffset = 0;
            this.facebookShareButton.Text.Color = ARGBColors.Black;
            this.facebookShareButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.facebookShareClicked));
            control.addControl(this.facebookShareButton);
            this.closeButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.closeButton.Position = new Point(290, 80);
            this.closeButton.Text.Text = SK.Text("GENERIC_Close", "Close");
            this.closeButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.closeButton.TextYOffset = -3;
            this.closeButton.Text.Color = ARGBColors.Black;
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick));
            control.addControl(this.closeButton);
        }
    }
}

