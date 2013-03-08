using System;
using System.Collections.Generic;
using System.Drawing;
using BoardSoupEngine.Scene;
using BoardSoupEngine.Kernel;
using BoardSoupEngine.Utilities;

namespace BoardSoupEngine.Input
{
    class Cursor
    {
        private Point position;
        private List<BoardActor> collidingActors;
        private IEventDispatcher dispatcher;
		private Size inputPaneSize;

        public Cursor(IEventDispatcher argDispatcher, Size size)
        {
            position = new Point(0,0);
            collidingActors = new List<BoardActor>();
            dispatcher = argDispatcher;
			inputPaneSize = size;
        }

        public void setPosition(Point argPosition)
        {
			position = CoordinateTranslator.screenToSceneLocation(argPosition, inputPaneSize);
            List<BoardActor> deleteList = new List<BoardActor>();

            foreach (BoardActor ba in collidingActors)
            {
                if (!this.isCollidingWithActor(ba))
                    deleteList.Add(ba);
            }

            foreach (BoardActor ba in deleteList)
                removeCollidingActor(ba);

            deleteList.Clear();

			InputCursorMoveEvent e = new InputCursorMoveEvent(position, this);
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
			InputCursorClickEvent e = new InputCursorClickEvent(CoordinateTranslator.screenToSceneLocation(location, inputPaneSize));
            dispatcher.submitEvent(e);
        }
    }
}
