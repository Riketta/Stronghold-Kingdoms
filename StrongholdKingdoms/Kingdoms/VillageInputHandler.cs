namespace Kingdoms
{
    using System;

    internal class VillageInputHandler : InputHandler
    {
        private float longTapLength = 1f;
        private float minimumSwipeDistance = 15f;
        private static VillageMode mode;
        private static float swipedDistance;
        private static float touchCounter;
        private VillageMap village;

        public VillageInputHandler(VillageMap villagemap)
        {
            this.village = villagemap;
        }

        public void handleInput(MouseInputState input)
        {
            if (this.village != null)
            {
                if (!input.leftdown && input.isScrolling())
                {
                    if (input.scrollLeft)
                    {
                        this.village.moveMap(10, 0);
                    }
                    if (input.scrollRight)
                    {
                        this.village.moveMap(-10, 0);
                    }
                    if (input.scrollUp)
                    {
                        this.village.moveMap(0, 10);
                    }
                    if (input.scrollDown)
                    {
                        this.village.moveMap(0, -10);
                    }
                }
                if (!input.leftdown)
                {
                    this.village.mouseNotClicked(input.dxMousePos);
                }
                if (input.overDXWindow || this.village.holdingLeftMouse())
                {
                    this.village.mouseMoveUpdate(input.dxMousePos, input.leftdown);
                }
                else if (input.wasOverDXWindow)
                {
                    CustomTooltipManager.MouseLeaveTooltipAreaMapSpecial();
                }
                if (input.rightclick)
                {
                    this.village.stopPlaceBuilding(true);
                }
            }
        }

        private enum VillageMode
        {
            NORMAL,
            DRAGGING
        }
    }
}

