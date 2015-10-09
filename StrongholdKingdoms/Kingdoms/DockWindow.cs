namespace Kingdoms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class DockWindow
    {
        private ContainerControl parentControl;

        public DockWindow(ContainerControl parent)
        {
            this.parentControl = parent;
        }

        public void AddControl(UserControl control, int x, int y)
        {
            this.parentControl.SuspendLayout();
            control.Location = new Point(x, y);
            this.parentControl.Controls.Add(control);
            this.parentControl.ResumeLayout(false);
            this.parentControl.PerformLayout();
        }

        public void RemoveControl(UserControl control)
        {
            this.parentControl.Controls.Remove(control);
        }
    }
}

