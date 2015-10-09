namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AvatarPanel : UserControl
    {
        private Bitmap _backBuffer;
        public AvatarData avatarData = new AvatarData();
        private IContainer components;
        public bool forceRedraw = true;

        public AvatarPanel()
        {
            this.InitializeComponent();
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
            base.AutoScaleMode = AutoScaleMode.None;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if ((this._backBuffer == null) || this.forceRedraw)
            {
                if (this._backBuffer == null)
                {
                    this._backBuffer = new Bitmap(0x9a, 500);
                }
                this.forceRedraw = false;
                Avatar.CreateAvatar(this.avatarData, this._backBuffer);
            }
            if (e != null)
            {
                e.Graphics.DrawImageUnscaled(this._backBuffer, 0, 0);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        public void update(AvatarData data)
        {
            this.avatarData = data;
            this.forceRedraw = true;
            this.Refresh();
        }
    }
}

