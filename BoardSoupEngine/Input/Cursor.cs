using System;
using System.Collections.Generic;
using System.Drawing;
using BoardSoupEngine.Scene;
using BoardSoupEngine.Kernel;

namespace BoardSoupEngine.Input
{
    class Cursor
    {
        private Point position;
        private List<BoardActor> collidingActors;
        private IEventDispatcher dispatcher;

        public Cursor(IEventDispatcher argDispatcher)
        {
            position = new Point(0,0);
            collidingActors = new List<BoardActor>();
            dispatcher = argDispatcher;
        }

        public void setPosition(Point argPosition)
        {
            position = argPosition;
            List<BoardActor> deleteList = new List<BoardActor>();

            foreach (BoardActor ba in collidingActors)
            {
                if (!this.isCollidingWithActor(ba))
                    deleteList.Add(ba);
            }

            foreach (BoardActor ba in deleteList)
                removeCollidingActor(ba);

            deleteList.Clear();

            InputCursorMoveEvent e = new InputCursorMoveEvent(argPosition, this);
            dispatcher.submitEvent(e);
        }

        public Point getPosition()
        {
            return position;
        }

        public void addCollidingActor(BoardActor argActor)
        {
            if (!collidingActors.Contains(argActor))
            {
                collidingActors.Add(argActor);
                argActor.onMouseIn();
            }
        }

        public void removeCollidingActor(BoardActor argActor)
        {
            collidingActors.Remove(argActor);
            argActor.onMouseOut();
        }

        public bool isCollidingWithActor(BoardActor argActor)
        {
            // TODO: bounding box check
            if (argActor.isAt(position))
                return true;

            return false;
        }

        public void click(int argClicks, Point location)
        {
            InputCursorClickEvent e = new InputCursorClickEvent(location);
            dispatcher.submitEvent(e);
        }
    }
}
