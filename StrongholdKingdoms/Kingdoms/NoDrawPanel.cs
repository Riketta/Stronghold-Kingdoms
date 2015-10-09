namespace Kingdoms
{
    using System;
    using System.Windows.Forms;

    public class NoDrawPanel : Panel
    {
        protected override void OnPaint(PaintEventArgs e)
        {
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }
    }
}

