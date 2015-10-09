namespace Kingdoms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class MFBTitlePanel : CustomSelfDrawPanel
    {
        private int lastWidth = -1;

        public MFBTitlePanel()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void init(int width)
        {
            if ((width != this.lastWidth) && (GFXLibrary.messageboxtop_left != null))
            {
                this.lastWidth = width;
                base.SuspendLayout();
                base.AutoScaleMode = AutoScaleMode.None;
                this.BackColor = ARGBColors.Transparent;
                base.Size = new Size(width, 30);
                base.ResumeLayout(false);
                base.clearControls();
                CustomSelfDrawPanel.CSDHorzExtendingPanel control = new CustomSelfDrawPanel.CSDHorzExtendingPanel {
                    Position = new Point(0, 0),
                    Size = new Size(width, 30)
                };
                base.addControl(control);
                control.Create((Image) GFXLibrary.messageboxtop_left, (Image) GFXLibrary.messageboxtop_middle, (Image) GFXLibrary.messageboxtop_right);
            }
        }
    }
}

