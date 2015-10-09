namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PizzazzPopupWindow : Form
    {
        private IContainer components;
        private static PizzazzPopupWindow Instance;
        private PizzazzPopupPanel pizzazzPopupPanel;

        public PizzazzPopupWindow()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        public static void closePizzazz()
        {
            if (Instance != null)
            {
                Sound.stopVillageEnvironmentalExceptWorld();
                Instance.Close();
                Instance = null;
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

        public void init(int pizzazzImage)
        {
            this.pizzazzPopupPanel.init(pizzazzImage);
        }

        private void InitializeComponent()
        {
            this.pizzazzPopupPanel = new PizzazzPopupPanel();
            base.SuspendLayout();
            this.pizzazzPopupPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.pizzazzPopupPanel.ClickThru = false;
            this.pizzazzPopupPanel.Location = new Point(0, 0);
            this.pizzazzPopupPanel.Name = "pizzazzPopupPanel";
            this.pizzazzPopupPanel.PanelActive = true;
            this.pizzazzPopupPanel.Size = new Size(0x27e, 0x148);
            this.pizzazzPopupPanel.StoredGraphics = null;
            this.pizzazzPopupPanel.TabIndex = 0;
            this.pizzazzPopupPanel.MouseClick += new MouseEventHandler(this.pizzazzPopupPanel_MouseClick);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Fuchsia;
            base.ClientSize = new Size(0x27e, 0x148);
            base.ControlBox = false;
            base.Controls.Add(this.pizzazzPopupPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            this.MaximumSize = new Size(0x27e, 0x148);
            this.MinimumSize = new Size(0x27e, 0x148);
            base.Name = "PizzazzPopupWindow";
            base.ShowInTaskbar = false;
            base.TransparencyKey = ARGBColors.Fuchsia;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "PizzazzPopupWindow";
            base.ResumeLayout(false);
        }

        private void pizzazzPopupPanel_MouseClick(object sender, MouseEventArgs e)
        {
            closePizzazz();
        }

        public static void showPizzazz(int pizzazzImage)
        {
            if (Instance == null)
            {
                Instance = new PizzazzPopupWindow();
                Instance.init(pizzazzImage);
                Form parentForm = InterfaceMgr.Instance.ParentForm;
                Instance.Location = new Point((parentForm.Location.X + (parentForm.Width / 2)) - (Instance.Width / 2), ((parentForm.Location.Y + (parentForm.Height / 2)) - (Instance.Height / 2)) - 20);
                Instance.Show(parentForm);
            }
        }

        public static void showPizzazzTutorial(int stage)
        {
            int pizzazzImage = -1;
            switch (stage)
            {
                case 2:
                    pizzazzImage = 1;
                    break;

                case 3:
                    pizzazzImage = 2;
                    break;

                case 5:
                    pizzazzImage = 3;
                    break;

                case 6:
                    pizzazzImage = 4;
                    break;

                case 7:
                    pizzazzImage = 5;
                    break;

                case 8:
                    pizzazzImage = 6;
                    break;

                case 10:
                    pizzazzImage = 9;
                    break;

                case 11:
                    pizzazzImage = 10;
                    break;

                case 12:
                    pizzazzImage = 2;
                    break;

                case 0x66:
                    pizzazzImage = 7;
                    break;

                case 0x67:
                    pizzazzImage = 8;
                    break;
            }
            if (pizzazzImage != -1)
            {
                showPizzazz(pizzazzImage);
            }
        }

        public void update()
        {
            this.pizzazzPopupPanel.update();
        }

        public static void updatePizzazz()
        {
            if (Instance != null)
            {
                Instance.update();
            }
        }
    }
}

