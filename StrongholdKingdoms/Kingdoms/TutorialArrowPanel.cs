namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TutorialArrowPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDImage background = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private bool created;
        private bool lastUpArrow;
        private CustomSelfDrawPanel.CSDFill transparentBackground = new CustomSelfDrawPanel.CSDFill();

        public TutorialArrowPanel()
        {
            this.InitializeComponent();
        }

        public void closing()
        {
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.Name = "TutorialArrowPanel";
            base.Size = new Size(600, 0x37);
            base.ResumeLayout(false);
        }

        public void show(bool upArrow, Form parent)
        {
            if (!this.created || (upArrow != this.lastUpArrow))
            {
                this.created = true;
                this.lastUpArrow = upArrow;
                base.clearControls();
                this.transparentBackground.Size = base.Size;
                this.transparentBackground.FillColor = Color.FromArgb(0xff, 0, 0xff);
                base.addControl(this.transparentBackground);
                this.background.Position = new Point(0, 0);
                if (upArrow)
                {
                    this.background.Image = (Image) GFXLibrary.tutorial_arrow_yellow[0];
                }
                else
                {
                    this.background.Image = (Image) GFXLibrary.tutorial_arrow_yellow[1];
                }
                this.background.Size = new Size(this.background.Image.Width, this.background.Image.Height);
                base.addControl(this.background);
                base.Invalidate();
                if (parent != null)
                {
                    parent.Invalidate();
                }
            }
        }

        public void update()
        {
        }
    }
}

