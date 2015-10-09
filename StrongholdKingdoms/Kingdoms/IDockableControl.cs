namespace Kingdoms
{
    using System;
    using System.Windows.Forms;

    public interface IDockableControl
    {
        void closeControl(bool includePopups);
        void controlDockToggle();
        void display(ContainerControl parent, int x, int y);
        void display(bool asPopup, ContainerControl parent, int x, int y);
        void initProperties(bool dockable, string title, ContainerControl parent);
        bool isPopup();
        bool isVisible();
    }
}

