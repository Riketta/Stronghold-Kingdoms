namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class LoadingPanel : Form
    {
        private IContainer components;
        private Panel panel1;

        public LoadingPanel()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init()
        {
            this.Text = SK.Text("LoadingPanel_Loading", "Loading Stronghold Kingdoms");
        }

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.panel1.BackgroundImage = Resources.splash_screen;
            this.panel1.Location = new Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1c6, 0xd4);
            this.panel1.TabIndex = 15;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Black;
            base.ClientSize = new Size(0x1c8, 0xd6);
            base.ControlBox = false;
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            base.Name = "LoadingPanel";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Loading Stronghold Kingdoms";
            base.Load += new EventHandler(this.LoadingPanel_Load);
            base.ResumeLayout(false);
        }

        private void LoadingPanel_Load(object sender, EventArgs e)
        {
            Graphics gfx = base.CreateGraphics();
            FontManager.setDPI(gfx);
            gfx.Dispose();
        }
    }
}

