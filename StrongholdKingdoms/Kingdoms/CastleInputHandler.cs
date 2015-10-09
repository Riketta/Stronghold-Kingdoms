namespace Kingdoms
{
    using System;

    internal class CastleInputHandler
    {
        private CastleMap castle;
        private GameEngine.GameDisplaySubModes gameDisplayModeSubMode;
        private static CastleMode mode;

        public CastleInputHandler(CastleMap castlemap, GameEngine.GameDisplaySubModes castleSubMode)
        {
            this.castle = castlemap;
            this.gameDisplayModeSubMode = castleSubMode;
        }

        public void handleInput(MouseInputState input)
        {
            if (this.castle != null)
            {
                if (input.leftdown && input.isScrolling())
                {
                    if (input.scrollLeft)
                    {
                        this.castle.moveMap(10, 0);
                    }
                    if (input.scrollRight)
                    {
                        this.castle.moveMap(-10, 0);
                    }
                    if (input.scrollUp)
                    {
                        this.castle.moveMap(0, 10);
                    }
                    if (input.scrollDown)
                    {
                        this.castle.moveMap(0, -10);
                    }
                }
                if (!input.leftdown)
                {
                    this.castle.mouseNotClicked(input.dxMousePos);
                }
                if (input.overDXWindow || this.castle.holdingLeftMouse())
                {
                    if (input.rightclick)
                    {
                        this.castle.rightClick(input.dxMousePos);
                    }
                    else
                    {
                        this.castle.mouseMoveUpdate(input.dxMousePos, input.leftdown);
                    }
                }
                else if (input.wasOverDXWindow)
                {
                    CustomTooltipManager.MouseLeaveTooltipAreaMapSpecial();
                }
                if (((input.mousebackward || input.mouseforward) || (GameEngine.Instance.GFX.keyCode == 0x20)) && (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_DEFAULT))
                {
                    this.castle.mouseWheel();
                }
            }
        }

        private enum CastleMode
        {
            NORMAL,
            DRAGGINGBUILDING,
            DRAGGINGTROOP,
            DRAGGINGHANDLE
        }
    }
}

