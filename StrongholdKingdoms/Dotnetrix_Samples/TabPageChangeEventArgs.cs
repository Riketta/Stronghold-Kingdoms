namespace Dotnetrix_Samples
{
    using System;
    using System.Windows.Forms;

    public class TabPageChangeEventArgs : EventArgs
    {
        private TabPage _PreSelected;
        private TabPage _Selected;
        public bool Cancel;

        public TabPageChangeEventArgs(TabPage CurrentTab, TabPage NextTab)
        {
            this._Selected = CurrentTab;
            this._PreSelected = NextTab;
        }

        public TabPage CurrentTab
        {
            get
            {
                return this._Selected;
            }
        }

        public TabPage NextTab
        {
            get
            {
                return this._PreSelected;
            }
        }
    }
}

