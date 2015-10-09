namespace Kingdoms
{
    using System;
    using System.Drawing;

    public class CardBarDX
    {
        private CardBarGDI cardbar = new CardBarGDI();
        private CustomSelfDrawPanel csd = new CustomSelfDrawPanel();
        private int delayedSection = -1;

        public bool click(Point mousePos)
        {
            return this.csd.baseControl.parentClicked(mousePos);
        }

        public void delayedInit(int cardSection)
        {
            this.delayedSection = cardSection;
        }

        public void init(int cardSection)
        {
            this.delayedSection = -1;
            this.csd.clearControls();
            this.csd.addControl(this.cardbar);
            this.cardbar.init(cardSection);
            this.update();
        }

        public void mouseMove(Point mousePos)
        {
            this.csd.tooltipSet = false;
            CustomTooltipManager.MouseLeaveTooltipArea();
            this.csd.baseControl.parentMouseOver(mousePos);
            TutorialWindow.tooltip(mousePos);
        }

        public void toggleEnabled(bool value)
        {
            this.cardbar.toggleActive(value);
        }

        public void update()
        {
            if (InterfaceMgr.Instance.allowDrawCircles())
            {
                if (this.delayedSection >= 0)
                {
                    this.init(this.delayedSection);
                }
                else
                {
                    this.cardbar.update();
                    if (this.cardbar.Dirty)
                    {
                        Graphics g = GameEngine.Instance.GFX.createDXOverlayTexture(this.cardbar.Size);
                        if (g != null)
                        {
                            if (this.csd.initFromDX(g, this.cardbar))
                            {
                                this.csd.drawControls();
                                this.csd.endPaint();
                            }
                            GameEngine.Instance.GFX.renderDXOverlayTexture(g);
                        }
                        this.cardbar.flagAsRendered();
                    }
                }
            }
        }
    }
}

