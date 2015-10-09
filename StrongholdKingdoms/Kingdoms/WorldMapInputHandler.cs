namespace Kingdoms
{
    using System;
    using System.Windows.Forms;

    internal class WorldMapInputHandler : InputHandler
    {
        private WorldMap world;

        public WorldMapInputHandler(WorldMap worldMap)
        {
            this.world = worldMap;
        }

        public void handleInput(MouseInputState input)
        {
            if (!input.leftdown && input.isScrolling())
            {
                if (input.scrollLeft)
                {
                    this.world.moveMap(0.0 - (10.0 / this.world.WorldScale), 0.0);
                }
                if (input.scrollRight)
                {
                    this.world.moveMap(10.0 / this.world.WorldScale, 0.0);
                }
                if (input.scrollUp)
                {
                    this.world.moveMap(0.0, 0.0 - (10.0 / this.world.WorldScale));
                }
                if (input.scrollDown)
                {
                    this.world.moveMap(0.0, 10.0 / this.world.WorldScale);
                }
            }
            if (!input.leftdown)
            {
                this.world.mouseNotClicked(input.dxMousePos);
            }
            if (input.overDXWindow || this.world.holdingLeftMouse())
            {
                if (input.leftdown)
                {
                    this.world.leftMouseDown(input.dxMousePos);
                }
                else if (input.rightclick)
                {
                    this.world.zoomOut();
                }
                else if (input.mousebackward)
                {
                    this.world.stopZoom();
                    double num = this.world.getOrigWorldZoom();
                    if (num > 26.899999998509884)
                    {
                        this.world.setMouseWheelZoomOut(14f);
                    }
                    else if (num > 13.899999618530273)
                    {
                        this.world.setMouseWheelZoomOut(9.5f);
                    }
                    else if (num > 9.3999996185302734)
                    {
                        this.world.setMouseWheelZoomOut(6.5f);
                    }
                    else if (num > 6.4000000953674316)
                    {
                        this.world.setMouseWheelZoomOut(3.5f);
                    }
                    else if (num > 3.4000000953674316)
                    {
                        this.world.setMouseWheelZoomOut(2f);
                    }
                    else
                    {
                        this.world.setMouseWheelZoomOut(0f);
                    }
                }
                else if (input.mouseforward)
                {
                    this.world.stopZoom();
                    double num2 = this.world.getOrigWorldZoom();
                    if (num2 < 0.10000000149011612)
                    {
                        this.world.changeZoom(2f, input.dxMousePos);
                    }
                    else if (num2 < 2.0999999046325684)
                    {
                        this.world.changeZoom(3.5f, input.dxMousePos);
                    }
                    else if (num2 < 3.5999999046325684)
                    {
                        this.world.changeZoom(6.5f, input.dxMousePos);
                    }
                    else if (num2 < 6.5999999046325684)
                    {
                        this.world.changeZoom(9.5f, input.dxMousePos);
                    }
                    else if (num2 < 9.6000003814697266)
                    {
                        this.world.changeZoom(14f, input.dxMousePos);
                    }
                    else
                    {
                        this.world.changeZoom(27f, input.dxMousePos);
                    }
                    if (num2 < 26.899999998509884)
                    {
                        GameEngine.Instance.playInterfaceSound("WorldMap_mousewheel_zoomin");
                    }
                    this.world.centreMap(false);
                }
                else
                {
                    this.world.moveMouse(input.dxMousePos);
                }
                InterfaceMgr.Instance.mouseMoveDXCardBar(input.dxMousePos);
                GameEngine.Instance.World.freeCardTooltip(input.dxMousePos);
            }
            else
            {
                if (input.wasOverDXWindow)
                {
                    CustomTooltipManager.MouseLeaveTooltipAreaMapSpecial();
                }
                if (InterfaceMgr.Instance.ParentForm.Cursor == Cursors.Hand)
                {
                    InterfaceMgr.Instance.ParentForm.Cursor = Cursors.Default;
                }
            }
        }

        public void handleInput(TouchInputState input)
        {
        }
    }
}

