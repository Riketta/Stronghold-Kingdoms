namespace Kingdoms
{
    using System;
    using System.Drawing;

    public class TouchInputState : InputState
    {
        public Point dxMousePos;
        private Point lastMouseMovePosition = new Point();
        public DateTime lastMouseMoveTime = DateTime.MaxValue;
        public bool leftdown;
        public bool mousebackward;
        public bool mouseforward;
        public Point mousePos;
        public bool overDXWindow;
        public Point pinchPosition = new Point();
        public bool rightclick;
        public bool scrollDown;
        public bool scrollLeft;
        public bool scrollRight;
        public bool scrollUp;
        public bool wasOverDXWindow;

        public void getInput()
        {
        }

        public bool isMultiTouch()
        {
            return false;
        }

        public bool isScrolling()
        {
            if (this.isMultiTouch())
            {
                return false;
            }
            if ((!this.scrollLeft && !this.scrollRight) && !this.scrollUp)
            {
                return this.scrollDown;
            }
            return true;
        }
    }
}

