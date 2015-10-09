namespace Kingdoms
{
    using System;
    using System.Windows.Forms;

    public class CursorManager
    {
        public static CursorType CurrentCursor = CursorType.Default;

        public static void SetCursor(CursorType type, Form ParentForm)
        {
            if (ParentForm != null)
            {
                switch (type)
                {
                    case CursorType.WaitCursor:
                        ParentForm.Cursor = Cursors.WaitCursor;
                        break;

                    case CursorType.Default:
                        ParentForm.Cursor = Cursors.Default;
                        break;

                    case CursorType.Hand:
                        ParentForm.Cursor = Cursors.Hand;
                        break;

                    case CursorType.SizeWE:
                        ParentForm.Cursor = Cursors.SizeWE;
                        break;

                    case CursorType.Cross:
                        ParentForm.Cursor = Cursors.Cross;
                        break;

                    case CursorType.VSplit:
                        ParentForm.Cursor = Cursors.VSplit;
                        break;
                }
                CurrentCursor = type;
            }
        }

        public enum CursorType
        {
            WaitCursor,
            Default,
            Hand,
            SizeWE,
            Cross,
            VSplit
        }
    }
}

