namespace Kingdoms
{
    using System;
    using System.Windows.Forms;

    public interface IDockWindow
    {
        void AddControl(UserControl control, int x, int y);
        void RemoveControl(UserControl control);
    }
}

