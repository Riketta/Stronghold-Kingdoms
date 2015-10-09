namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PostTutorialWindow : Form
    {
        private IContainer components;
        private PostTutorialPanel customPanel;
        private bool inClosedForm;
        private static PostTutorialWindow instance;

        public PostTutorialWindow()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        public static void close()
        {
            try
            {
                if (instance != null)
                {
                    InterfaceMgr.Instance.closeGreyOut();
                    instance.Close();
                    instance = null;
                }
            }
            catch (Exception)
            {
            }
        }

        public static void CreatePostTutorialWindow(bool fromTutorial)
        {
            InterfaceMgr.Instance.openGreyOutWindow(false);
            PostTutorialWindow window = new PostTutorialWindow();
            window.init(fromTutorial);
            window.Show(InterfaceMgr.Instance.getGreyOutWindow());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(bool tutorialOpened)
        {
            instance = this;
            this.customPanel.init(tutorialOpened, this);
            Form parentForm = InterfaceMgr.Instance.ParentForm;
            base.Location = new Point((parentForm.Location.X + (parentForm.Width / 2)) - (base.Width / 2), (parentForm.Location.Y + (parentForm.Height / 2)) - (base.Height / 2));
        }

        private void InitializeComponent()
        {
            this.customPanel = new PostTutorialPanel();
            base.SuspendLayout();
            this.customPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.customPanel.ClickThru = false;
            this.customPanel.Location = new Point(0, 0);
            this.customPanel.Name = "customPanel";
            this.customPanel.PanelActive = true;
            this.customPanel.Size = new Size(0x271, 0x29c);
            this.customPanel.StoredGraphics = null;
            this.customPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Fuchsia;
            base.ClientSize = new Size(0x271, 0x29c);
            base.ControlBox = false;
            base.Controls.Add(this.customPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MinimumSize = new Size(10, 10);
            base.Name = "PostTutorialWindow";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "PostTutorialWindow";
            base.TransparencyKey = ARGBColors.Fuchsia;
            base.FormClosed += new FormClosedEventHandler(this.PostTutorialWindow_FormClosed);
            base.ResumeLayout(false);
        }

        private void PostTutorialWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!this.inClosedForm)
            {
                this.inClosedForm = true;
                InterfaceMgr.Instance.closeGreyOut();
                this.inClosedForm = false;
            }
        }
    }
}

