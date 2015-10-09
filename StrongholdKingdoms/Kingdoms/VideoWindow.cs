namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class VideoWindow : MyFormBase
    {
        private IContainer components;
        private static VideoWindow instance;
        protected WebHelpPanel videoPane;
        public static bool vidLoaded;

        public VideoWindow()
        {
            this.InitializeComponent();
        }

        public static void ClosePopup()
        {
            try
            {
                if (instance != null)
                {
                    instance.Close();
                    instance = null;
                }
            }
            catch (Exception)
            {
            }
        }

        public static void closing()
        {
            instance = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.videoPane = new WebHelpPanel();
            base.SuspendLayout();
            this.videoPane.Location = new Point(2, 0x20);
            this.videoPane.Name = "videoPane";
            this.videoPane.Size = new Size(640, 360);
            this.videoPane.TabIndex = 0;
            this.videoPane.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Black;
            base.ClientSize = new Size(0x284, 0x18a);
            base.Controls.Add(this.videoPane);
            base.Name = "VideoWindow";
            base.ShowClose = true;
            this.Text = "VideoWindow";
            base.Load += new EventHandler(this.VideoWindow_Load);
            base.Controls.SetChildIndex(this.videoPane, 0);
            base.ResumeLayout(false);
        }

        public void setMode(bool video)
        {
            if (video)
            {
                this.Text = base.Title = SK.Text("HELP_Help_Video", "Tutorial Video");
            }
            else
            {
                this.Text = base.Title = SK.Text("Admin_Message", "Admin's Message");
                base.Size = new Size(0x356, base.Size.Height);
                this.videoPane.Size = new Size(850, this.videoPane.Size.Height);
            }
        }

        public static void ShowVideo(string url, bool video)
        {
            if (instance != null)
            {
                try
                {
                    instance.Close();
                    instance = null;
                }
                catch (Exception)
                {
                }
            }
            vidLoaded = false;
            VideoWindow window = new VideoWindow();
            window.setMode(video);
            window.closeCallback = new MyFormBase.MFBClose(VideoWindow.closing);
            Form parentForm = InterfaceMgr.Instance.ParentForm;
            if ((parentForm != null) && (parentForm.WindowState != FormWindowState.Minimized))
            {
                Point location = parentForm.Location;
                Size size = parentForm.Size;
                Size size2 = window.Size;
                Point point2 = new Point(((size.Width - size2.Width) / 2) + location.X, ((size.Height - size2.Height) / 2) + location.Y);
                window.Location = point2;
            }
            else
            {
                window.StartPosition = FormStartPosition.CenterScreen;
            }
            window.Show(parentForm);
            instance = window;
            while (!vidLoaded)
            {
                Thread.Sleep(100);
                Application.DoEvents();
            }
            Thread.Sleep(500);
            window.videoPane.Visible = true;
            window.videoPane.openPage(url);
        }

        private void VideoWindow_Load(object sender, EventArgs e)
        {
            vidLoaded = true;
        }
    }
}

