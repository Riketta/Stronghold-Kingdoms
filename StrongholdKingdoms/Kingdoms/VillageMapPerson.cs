namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.Drawing;

    public class VillageMapPerson
    {
        public PointF currentPos;
        public PointF endPos;
        public DateTime endTime = DateTime.Now;
        public int facing;
        public int fadeDir;
        private GraphicsMgr gfx;
        public bool idling;
        public PointF startPos;
        public DateTime startTime = DateTime.Now;
        public VillagePeopleStates state;
        public SpriteWrapper workerSprite;
        public bool working;

        public VillageMapPerson(GraphicsMgr newGfx)
        {
            this.gfx = newGfx;
        }

        public void dispose()
        {
            if (this.workerSprite != null)
            {
                this.workerSprite.RemoveSelfFromParent();
            }
        }

        public void fadeToSolid()
        {
            this.fadeDir = 10;
        }

        public void fadeToTransparent()
        {
            this.fadeDir = -10;
        }

        public PointF getCurrentPos()
        {
            return this.currentPos;
        }

        public PointF getPos()
        {
            return this.currentPos;
        }

        public void initAnim(int texID, int baseFrame, short[] animarray, int animTime)
        {
            this.initWorkerSprite();
            this.workerSprite.Initialize(this.gfx, texID, baseFrame);
            this.workerSprite.clearDirectionality();
            this.workerSprite.initAnim(baseFrame, animarray, animTime);
            Point point = new Point(50, 0x42);
            this.workerSprite.Center = (PointF) point;
            this.workerSprite.Visible = true;
        }

        public void initAnim(int texID, int baseID, int numFrames, int animTime)
        {
            this.initAnim(texID, 0, baseID, numFrames, 1, animTime, true);
        }

        public void initAnim(int texID, int upDir, int baseID, int numFrames, int frameSkip, int animTime, bool clockwise)
        {
            this.initWorkerSprite();
            this.workerSprite.Initialize(this.gfx, texID, baseID);
            if (frameSkip == 1)
            {
                this.workerSprite.clearDirectionality();
                this.workerSprite.initAnim(baseID, numFrames, frameSkip, animTime);
            }
            else
            {
                this.workerSprite.initDirectionality(8, upDir, !clockwise);
                this.workerSprite.initAnim(baseID, numFrames, frameSkip, animTime);
            }
            Point point = new Point(50, 0x42);
            this.workerSprite.Center = (PointF) point;
            this.workerSprite.Visible = true;
        }

        public void initWorkerSprite()
        {
            if (this.workerSprite == null)
            {
                this.workerSprite = new SpriteWrapper();
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.addChildSprite(this.workerSprite, 15);
                }
            }
        }

        public void initWorkerSpriteInBuilding(SpriteWrapper buildingSprite)
        {
            if (this.workerSprite == null)
            {
                this.workerSprite = new SpriteWrapper();
                buildingSprite.AddChild(this.workerSprite, 1);
            }
        }

        public bool isJourneyOver()
        {
            return (this.state != VillagePeopleStates.MOVING);
        }

        public void setPixelPos(Point pos)
        {
            this.currentPos = (PointF) pos;
        }

        public void setPos(Point pos)
        {
            Point point = new Point(pos.X, pos.Y) {
                X = pos.X * 0x20,
                Y = pos.Y * 0x10 + 8,
            };
            this.currentPos = (PointF) point;
        }

        public void startJourney(Point realStart, Point realEnd, double distThroughJourney)
        {
            this.startPos = (PointF) realStart;
            this.endPos = (PointF) realEnd;
            if (distThroughJourney >= 1.0)
            {
                this.currentPos = this.endPos;
                this.state = VillagePeopleStates.STATIONARY;
            }
            else
            {
                TimeSpan span = VillageBuildingsData.calcTravelTime(GameEngine.Instance.LocalWorldData, realStart, realEnd);
                this.startTime = DateTime.Now;
                this.endTime = DateTime.Now.Add(span);
                if (distThroughJourney != 0.0 && !double.IsNaN(distThroughJourney))
                {
                    double num = span.TotalSeconds * distThroughJourney;
                    this.startTime = this.startTime.AddSeconds(0.0 - num); // TODO Ex here
                    this.endTime = this.endTime.AddSeconds(0.0 - num);
                }
                this.state = VillagePeopleStates.MOVING;
                this.facing = SpriteWrapper.getFacing(this.startPos, this.endPos, 8);
                this.updateJourney();
            }
        }

        public void startJourneyTileBased(Point newStartPos, Point newEndPos, double distThroughJourney)
        {
            Point realStart = VillageBuildingsData.tileToPixel(newStartPos);
            Point realEnd = VillageBuildingsData.tileToPixel(newEndPos);
            this.startJourney(realStart, realEnd, distThroughJourney);
        }

        public void update()
        {
            this.updateJourney();
            if (this.workerSprite != null)
            {
                this.workerSprite.PosX = this.currentPos.X;
                this.workerSprite.PosY = this.currentPos.Y;
                this.workerSprite.Facing = this.facing;
                int num = this.workerSprite.ColorToUse.A + this.fadeDir;
                if (num < 160)
                {
                    num = 160;
                }
                else if (num > 0xff)
                {
                    num = 0xff;
                }
                this.workerSprite.ColorToUse = Color.FromArgb((byte) num, 0xff, 0xff, 0xff);
            }
        }

        public void updateJourney()
        {
            if (this.state == VillagePeopleStates.MOVING)
            {
                DateTime now = DateTime.Now;
                if (now >= this.endTime)
                {
                    this.currentPos = this.endPos;
                    this.state = VillagePeopleStates.STATIONARY;
                }
                else
                {
                    TimeSpan span = (TimeSpan) (this.endTime - this.startTime);
                    TimeSpan span2 = (TimeSpan) (now - this.startTime);
                    double num = span2.TotalSeconds / span.TotalSeconds;
                    double num2 = ((this.endPos.X - this.startPos.X) * num) + this.startPos.X;
                    double num3 = ((this.endPos.Y - this.startPos.Y) * num) + this.startPos.Y;
                    this.currentPos = new PointF((float) num2, (float) num3);
                }
            }
        }

        public bool Visible
        {
            set
            {
                if (this.workerSprite != null)
                {
                    this.workerSprite.Visible = value;
                }
            }
        }

        public enum VillagePeopleStates
        {
            STATIONARY,
            MOVING
        }
    }
}

