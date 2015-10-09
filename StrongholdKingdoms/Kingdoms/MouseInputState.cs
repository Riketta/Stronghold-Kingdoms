namespace Kingdoms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class MouseInputState : InputState
    {
        public Point clientMousePos;
        public Point dxMousePos;
        public bool leftdown;
        public bool mousebackward;
        public bool mouseforward;
        public Point mousePos;
        public bool overDXWindow;
        public bool rightclick;
        public bool scrollDown;
        public bool scrollLeft;
        public bool scrollRight;
        public bool scrollUp;
        public bool wasOverDXWindow;

        public void getInput()
        {
            this.leftdown = GameEngine.Instance.GFX.leftmousedown;
            this.rightclick = GameEngine.Instance.GFX.rightClick;
            this.mousebackward = GameEngine.Instance.GFX.mouseBackward;
            this.mouseforward = GameEngine.Instance.GFX.mouseForward;
            this.mousePos = new Point(Cursor.Position.X, Cursor.Position.Y);
            this.clientMousePos = InterfaceMgr.Instance.ParentForm.PointToClient(this.mousePos);
            this.dxMousePos = InterfaceMgr.Instance.getDXBasePanel().PointToClient(this.mousePos);
            if (((GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE) || (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_WORLD)) || (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE))
            {
                if (InterfaceMgr.Instance.getDXBasePanel().ClientRectangle.Contains(this.dxMousePos))
                {
                    this.overDXWindow = true;
                }
                else
                {
                    this.overDXWindow = false;
                }
            }
            else
            {
                this.overDXWindow = false;
            }
            if (!this.mousePos.Equals(GameEngine.Instance.lastMouseMovePosition))
            {
                GameEngine.Instance.lastMouseMovePosition = this.mousePos;
                GameEngine.Instance.lastMouseMoveTime = DateTime.Now;
            }
        }

        public bool isScrolling()
        {
            if ((!this.scrollLeft && !this.scrollRight) && !this.scrollUp)
            {
                return this.scrollDown;
            }
            return true;
        }
    }
}

