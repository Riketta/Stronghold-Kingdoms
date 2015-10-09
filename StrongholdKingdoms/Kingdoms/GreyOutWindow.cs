namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class GreyOutWindow : Form
    {
        private IContainer components;
        private GreyOutPanel greyOutPanel;

        public GreyOutWindow()
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

        public void init(bool showBorder)
        {
            this.greyOutPanel.Visible = false;
        }

        private void InitializeComponent()
        {
            this.greyOutPanel = new GreyOutPanel();
            base.SuspendLayout();
            this.greyOutPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.greyOutPanel.Location = new Point(0, 0);
            this.greyOutPanel.Name = "greyOutPanel";
            this.greyOutPanel.Size = new Size(0x124, 0x10a);
            this.greyOutPanel.TabIndex = 0;
            this.greyOutPanel.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Black;
            base.ClientSize = new Size(0x124, 0x10a);
            base.ControlBox = false;
            base.Controls.Add(this.greyOutPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "GreyOutWindow";
            base.Opacity = 0.4;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "GreyOutWindow";
            base.ResumeLayout(false);
        }

        public void setInnerArea(Rectangle area)
        {
        }
    }
}

