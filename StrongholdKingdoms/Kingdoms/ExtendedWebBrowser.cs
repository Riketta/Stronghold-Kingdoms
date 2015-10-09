namespace Kingdoms
{
    using System;
    using System.Windows.Forms;

    public class ExtendedWebBrowser : WebBrowser
    {
        private const int WM_DESTROY = 2;
        private const int WM_PARENTNOTIFY = 0x210;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x210)
            {
                try
                {
                    if (!base.DesignMode && (m.WParam.ToInt32() == 2))
                    {
                        Message message = new Message {
                            Msg = 2
                        };
                        ((Form) base.Parent).Close();
                    }
                }
                catch (Exception)
                {
                }
                this.DefWndProc(ref m);
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}

