using System.Collections.Generic;
using BoardSoupEngine.Kernel;
using System;
using System.Drawing;
using BoardSoupEngine.Interface;
using BoardSoupEngine.Utilities;

namespace BoardSoupEngine.Scene
{
    internal class Board : ITickable
    {
        private bool active = true;
        //private List<BoardActor> actors;

        private QuadTree<BoardActor> actors;
        private BoardObject myInterfaceObject;
        private IEventDispatcher dispatcher;

        public Board()
        {
            //actors = new List<BoardActor>();
            actors = new QuadTree<BoardActor>(new Rectangle(315, 0, 1050, 1050));
            dispatcher = null;
            myInterfaceObject = null;
        }

        public Board(IEventDispatcher argDispatcher) : this()
        {
            dispatcher = argDispatcher;
        }

        public void onTick()
        {
            if (myInterfaceObject != null)
                myInterfaceObject.onTick();

            try
            {
                foreach (BoardActor a in actors)
                    a.onTick();
            }
            catch (InvalidOperationException e) { }
        }

        public void render()
        {
            foreach (BoardActor a in actors)
                a.render();
        }

        public void setActive(bool b)
        {
            active = b;
        }

        public bool isActive()
        {
            return active;
        }

        public void addActor(BoardActor ba)
        {
            if(ba != null)
                actors.Add(ba, ba.getBounds());

            onBoardChanged();
        }

        public void setInterfaceObject(BoardObject bo)
        {
            myInterfaceObject = bo;
            myInterfaceObject.setBoard(this);
        }

        public void clearBoard()
        {
            actors.Clear();
            onBoardChanged();
        }

        public BoardActor getActorAt(Point p)
        {
            BoardActor r = null;

            List<BoardActor> actorlist = actors.getObjectsAt(p);

            if(actorlist != null) {
                try
                {
                    foreach (BoardActor ba in actorlist)
                    {
                        if (ba.isAt(p))
                            r = ba;
                    }
                }
                catch (InvalidOperationException e) { }
            }

            return r;
        }

        public void deleteActor(BoardActor ba)
        {
            actors.Remove(ba, ba.getInterfaceObject().getLocation());
            onBoardChanged();
        }

        private void onBoardChanged()
        {
            if (myInterfaceObject != null)
                myInterfaceObject.onBoardChanged();
        }

        public List<ActorObject> getAllActorInterfaces()
        {
            List<ActorObject> r = new List<ActorObject>();

            foreach (BoardActor ba in actors)
            {
                if (ba.getInterfaceObject() != null)
                    r.Add(ba.getInterfaceObject());
            }

            return r;
        }
    }
}
