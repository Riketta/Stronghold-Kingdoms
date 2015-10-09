namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CardBarGDIPatch : CustomSelfDrawPanel
    {
        private CardBarGDI cardbar = new CardBarGDI();
        private IContainer components;

        public CardBarGDIPatch()
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

        public void init(int cardSection)
        {
            base.clearControls();
            this.cardbar.Position = new Point(0, 0);
            base.addControl(this.cardbar);
            this.cardbar.init(cardSection);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.Name = "CardBarGDIPatch";
            base.Size = new Size(600, 0x37);
            base.ResumeLayout(false);
        }

        public void update()
        {
            this.cardbar.update();
        }
    }
}

