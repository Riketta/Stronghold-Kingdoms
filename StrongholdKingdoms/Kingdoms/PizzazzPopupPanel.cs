namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PizzazzPopupPanel : CustomSelfDrawPanel
    {
        private int animCount;
        private CustomSelfDrawPanel.CSDExtendingPanel background = new CustomSelfDrawPanel.CSDExtendingPanel();
        private const int CENTER_X = 0xbf;
        private const int CENTER_Y = 0x29;
        private IContainer components;
        private Firework firework1 = new Firework();
        private Firework firework10 = new Firework();
        private Firework firework10a = new Firework();
        private Firework firework1a = new Firework();
        private Firework firework2 = new Firework();
        private Firework firework2a = new Firework();
        private Firework firework3 = new Firework();
        private Firework firework3a = new Firework();
        private Firework firework4 = new Firework();
        private Firework firework4a = new Firework();
        private Firework firework5 = new Firework();
        private Firework firework5a = new Firework();
        private Firework firework6 = new Firework();
        private Firework firework6a = new Firework();
        private Firework firework7 = new Firework();
        private Firework firework7a = new Firework();
        private Firework firework8 = new Firework();
        private Firework firework8a = new Firework();
        private Firework firework9 = new Firework();
        private Firework firework9a = new Firework();
        public const int PIZZAZZ_IMAGE_APPLES = 1;
        public const int PIZZAZZ_IMAGE_CARD_CASTLE = 10;
        public const int PIZZAZZ_IMAGE_CARD_POINTS = 6;
        public const int PIZZAZZ_IMAGE_CARD_WOOD = 5;
        public const int PIZZAZZ_IMAGE_GOLD = 8;
        public const int PIZZAZZ_IMAGE_HONOUR = 4;
        public const int PIZZAZZ_IMAGE_PREMIUM = 7;
        public const int PIZZAZZ_IMAGE_RESEARCH_POINT = 3;
        public const int PIZZAZZ_IMAGE_WOOD = 9;
        public const int PIZZAZZ_IMAGE_WOOD_STONE = 2;
        private CustomSelfDrawPanel.CSDImage reward2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDArea rewardArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage rewardImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage starImage = new CustomSelfDrawPanel.CSDImage();
        private float starRotate;
        private float starRotateSpeed;
        private int starSpinCount;
        private int starSpinMode;
        private CustomSelfDrawPanel.CSDFill transparentBackground = new CustomSelfDrawPanel.CSDFill();

        public PizzazzPopupPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(int pizzazzImage)
        {
            int num;
            Sound.playVillageEnvironmental(0x2e, false, false);
            Sound.forceFullPlayOfNextEnvironmental();
            base.clearControls();
            this.transparentBackground.Size = base.Size;
            this.transparentBackground.FillColor = Color.FromArgb(0xff, 0, 0xff);
            base.addControl(this.transparentBackground);
            this.background.Position = new Point(0, 0);
            this.background.Size = new Size(0x27e, 0x148);
            base.addControl(this.background);
            this.background.Create((Image) GFXLibrary._9sclice_fancy_top_left, (Image) GFXLibrary._9sclice_fancy_top_mid, (Image) GFXLibrary._9sclice_fancy_top_right, (Image) GFXLibrary._9sclice_fancy_mid_left, (Image) GFXLibrary._9sclice_fancy_mid_mid, (Image) GFXLibrary._9sclice_fancy_mid_right, (Image) GFXLibrary._9sclice_fancy_bottom_left, (Image) GFXLibrary._9sclice_fancy_bottom_mid, (Image) GFXLibrary._9sclice_fancy_bottom_right);
            this.background.ForceTiling();
            this.rewardArea.Position = new Point(0xbf, 0x29);
            this.rewardArea.Size = new Size(0x100, 0x100);
            this.background.addControl(this.rewardArea);
            this.firework1.init(this.rewardArea, 0x24, 0);
            this.firework2.init(this.rewardArea, 0x20, 0);
            this.firework3.init(this.rewardArea, 0x1c, 0);
            this.firework4.init(this.rewardArea, 0x18, 0);
            this.firework5.init(this.rewardArea, 20, 0);
            this.firework6.init(this.rewardArea, 0x10, 0);
            this.firework7.init(this.rewardArea, 12, 0);
            this.firework8.init(this.rewardArea, 8, 0);
            this.firework9.init(this.rewardArea, 4, 0);
            this.firework10.init(this.rewardArea, 0, 0);
            this.firework1a.init(this.rewardArea, 0x24, 1);
            this.firework2a.init(this.rewardArea, 0x20, 1);
            this.firework3a.init(this.rewardArea, 0x1c, 1);
            this.firework4a.init(this.rewardArea, 0x18, 1);
            this.firework5a.init(this.rewardArea, 20, 1);
            this.firework6a.init(this.rewardArea, 0x10, 1);
            this.firework7a.init(this.rewardArea, 12, 1);
            this.firework8a.init(this.rewardArea, 8, 1);
            this.firework9a.init(this.rewardArea, 4, 1);
            this.firework10a.init(this.rewardArea, 0, 1);
            this.starImage.Image = (Image) GFXLibrary.wheel_star[0];
            this.starImage.Position = new Point(0, 0);
            this.starImage.RotateCentre = new PointF(128f, 128f);
            this.starImage.Visible = false;
            this.starSpinMode = 1;
            this.rewardArea.addControl(this.starImage);
            switch (pizzazzImage)
            {
                case 1:
                    this.rewardImage.Image = (Image) GFXLibrary.getCommodity64DSImage(13);
                    this.rewardImage.Position = new Point(0x53, 0x4f);
                    this.rewardArea.addControl(this.rewardImage);
                    return;

                case 2:
                    this.rewardImage.Image = (Image) GFXLibrary.getCommodity64DSImage(6);
                    this.rewardImage.Position = new Point(0x4c, 0x4c);
                    this.rewardArea.addControl(this.rewardImage);
                    this.reward2Image.Image = (Image) GFXLibrary.getCommodity64DSImage(7);
                    this.reward2Image.Position = new Point(0x60, 0x60);
                    this.rewardArea.addControl(this.reward2Image);
                    return;

                case 3:
                case 5:
                case 6:
                case 8:
                case 10:
                    num = -1;
                    switch (pizzazzImage)
                    {
                        case 3:
                            num = 2;
                            goto Label_0542;

                        case 5:
                            num = 15;
                            goto Label_0542;

                        case 6:
                            num = 1;
                            goto Label_0542;

                        case 8:
                            num = 5;
                            goto Label_0542;

                        case 10:
                            num = 14;
                            goto Label_0542;
                    }
                    break;

                case 4:
                    this.rewardImage.Image = (Image) GFXLibrary.com_64_honour_DS;
                    this.rewardImage.Position = new Point(0x54, 0x54);
                    this.rewardArea.addControl(this.rewardImage);
                    return;

                case 7:
                    this.rewardImage.Image = GFXLibrary.PremiumTokens[0x1011][0];
                    this.rewardImage.Position = new Point(0x60, 0x60);
                    this.rewardImage.Size = new Size(this.rewardImage.Image.Width / 2, this.rewardImage.Image.Height / 2);
                    this.rewardArea.addControl(this.rewardImage);
                    return;

                case 9:
                    this.rewardImage.Image = (Image) GFXLibrary.getCommodity64DSImage(6);
                    this.rewardImage.Position = new Point(0x53, 0x4f);
                    this.rewardArea.addControl(this.rewardImage);
                    return;

                default:
                    return;
            }
        Label_0542:
            this.rewardImage.Image = (Image) GFXLibrary.wheel_icons[num];
            this.rewardImage.Size = new Size(0x80, 0x80);
            this.rewardImage.Position = new Point(0x40, 0x40);
            this.rewardArea.addControl(this.rewardImage);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        public void update()
        {
            this.firework1.update();
            this.firework2.update();
            this.firework3.update();
            this.firework4.update();
            this.firework5.update();
            this.firework6.update();
            this.firework7.update();
            this.firework8.update();
            this.firework9.update();
            this.firework10.update();
            this.firework1a.update();
            this.firework2a.update();
            this.firework3a.update();
            this.firework4a.update();
            this.firework5a.update();
            this.firework6a.update();
            this.firework7a.update();
            this.firework8a.update();
            this.firework9a.update();
            this.firework10a.update();
            base.Invalidate();
            this.animCount++;
            if (this.animCount >= 180)
            {
                PizzazzPopupWindow.closePizzazz();
            }
            else if (this.animCount > 150)
            {
                Firework.active = false;
            }
            if (this.starSpinMode > 0)
            {
                switch (this.starSpinMode)
                {
                    case 1:
                        this.starImage.Image = (Image) GFXLibrary.wheel_star[2];
                        this.starImage.Visible = true;
                        this.starImage.Alpha = 0.01f;
                        this.starSpinMode++;
                        this.starRotate = 0f;
                        this.starRotateSpeed = 8f;
                        this.starSpinCount = 200;
                        break;

                    case 2:
                        this.starImage.Alpha += 0.2f;
                        if (this.starImage.Alpha > 1f)
                        {
                            this.starImage.Alpha = 1f;
                            this.starSpinMode++;
                        }
                        break;

                    case 3:
                        this.starSpinCount--;
                        if (this.starSpinCount == 0)
                        {
                            this.starSpinMode++;
                        }
                        break;

                    case 4:
                        this.starImage.Alpha -= 0.1f;
                        if (this.starImage.Alpha < 0f)
                        {
                            this.starImage.Alpha = 0f;
                            this.starImage.Visible = false;
                            this.starSpinMode = 0;
                            this.starRotateSpeed = 0f;
                        }
                        break;
                }
                float starRotate = this.starRotate;
                if ((starRotate >= 179.9f) && (starRotate <= 180f))
                {
                    starRotate = 179.9f;
                }
                else if ((starRotate > 180f) && (starRotate <= 180.1f))
                {
                    starRotate = 180.1f;
                }
                this.starImage.Rotate = starRotate;
                this.starRotate += this.starRotateSpeed;
                if (this.starRotate >= 360f)
                {
                    this.starRotate -= 360f;
                }
                if (this.starRotateSpeed <= 8f)
                {
                    this.starImage.Image = (Image) GFXLibrary.wheel_star[0];
                }
                else if (this.starRotateSpeed < 15f)
                {
                    this.starImage.Image = (Image) GFXLibrary.wheel_star[1];
                }
                else
                {
                    this.starImage.Image = (Image) GFXLibrary.wheel_star[2];
                }
                this.starImage.invalidate();
            }
        }

        public class Firework : CustomSelfDrawPanel.CSDImage
        {
            public static bool active = false;
            private static int baseAngle1 = 0;
            private static int baseAngle2 = 180;
            private int count;
            private float dx;
            private float dy;
            private int m_band;
            private const int max = 40;
            private PointF pos;
            private static Random rand = new Random();
            private const float rate = 4f;

            public void init(CustomSelfDrawPanel.CSDControl parentControl, int initialProgress, int band)
            {
                active = true;
                this.m_band = band;
                base.Image = (Image) GFXLibrary.tutorial_reward_anim[0];
                base.RotateCentre = new PointF(64f, 64f);
                base.Visible = true;
                parentControl.addControl(this);
                this.restart();
                for (int i = 0; i < initialProgress; i++)
                {
                    this.update();
                }
            }

            public void restart()
            {
                if (active)
                {
                    int degrees = baseAngle1;
                    if (this.m_band == 0)
                    {
                        baseAngle1 -= 0x25;
                    }
                    else if (this.m_band == 1)
                    {
                        baseAngle2 -= 0x25;
                        degrees = baseAngle2;
                    }
                    PointF tf = GameEngine.Instance.GFX.rotatePoint(new PointF(1f, 0f), degrees);
                    this.dx = tf.X;
                    this.dy = tf.Y;
                    this.pos = new PointF(64f, 64f);
                    this.Position = new Point(0x40, 0x40);
                    base.Rotate = 0f;
                    this.count = 0;
                    base.Alpha = 1f;
                }
                else
                {
                    base.Visible = false;
                }
            }

            public void update()
            {
                if (this.count > 30)
                {
                    int num = 40 - this.count;
                    if (num > 10)
                    {
                        num = 10;
                    }
                    else if (num < 0)
                    {
                        num = 0;
                    }
                    base.Alpha = ((float) num) / 10f;
                }
                this.pos = new PointF(this.pos.X + (this.dx * 4f), this.pos.Y + (this.dy * 4f));
                this.Position = new Point((int) this.pos.X, (int) this.pos.Y);
                this.count++;
                if (this.count >= 40)
                {
                    this.restart();
                }
                base.Image = (Image) GFXLibrary.tutorial_reward_anim[Math.Min(this.count / 2, 0x13)];
            }
        }
    }
}

