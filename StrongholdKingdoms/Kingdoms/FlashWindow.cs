namespace Kingdoms
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public static class FlashWindow
    {
        public const uint FLASHW_ALL = 3;
        public const uint FLASHW_CAPTION = 1;
        public const uint FLASHW_STOP = 0;
        public const uint FLASHW_TIMER = 4;
        public const uint FLASHW_TIMERNOFG = 12;
        public const uint FLASHW_TRAY = 2;

        private static FLASHWINFO Create_FLASHWINFO(IntPtr handle, uint flags, uint count, uint timeout)
        {
            FLASHWINFO flashwinfo = new FLASHWINFO();
            return new FLASHWINFO { cbSize = Convert.ToUInt32(Marshal.SizeOf(flashwinfo)), hwnd = handle, dwFlags = flags, uCount = count, dwTimeout = timeout };
        }

        public static bool Flash(Form form)
        {
            FLASHWINFO FW = Create_FLASHWINFO(form.Handle, 15, uint.MaxValue, 0);
            return ((Win2000OrLater && (form != null)) && FlashWindowEx(ref FW));
        }

        public static bool Flash(Form form, uint count)
        {
            FLASHWINFO FW = Create_FLASHWINFO(form.Handle, 3, count, 0);
            return ((Win2000OrLater && (form != null)) && FlashWindowEx(ref FW));
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);
        public static bool Start(Form form)
        {
            FLASHWINFO FW = Create_FLASHWINFO(form.Handle, 3, uint.MaxValue, 0);
            return ((Win2000OrLater && (form != null)) && FlashWindowEx(ref FW));
        }

        public static bool Stop(Form form)
        {
            FLASHWINFO FW = Create_FLASHWINFO(form.Handle, 0, uint.MaxValue, 0);
            return ((Win2000OrLater && (form != null)) && FlashWindowEx(ref FW));
        }

        private static bool Win2000OrLater
        {
            get
            {
                return (Environment.OSVersion.Version.Major >= 5);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            public uint cbSize;
            public IntPtr hwnd;
            public uint dwFlags;
            public uint uCount;
            public uint dwTimeout;
        }
    }
}

