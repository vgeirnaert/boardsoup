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
            actors = new QuadTree<BoardActor>(new Rectangle(0, 0, 1050, 1050));
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
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Error Board::onTick"); 
            }
        }

        public void render()
        {
            try
            {
                foreach (BoardActor a in actors)
                    a.render();
            }
            catch (InvalidOperationException e) 
            {
                Console.WriteLine("Error Board::render");
            }
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
            if (ba != null)
            {
                actors.Add(ba, ba.getBounds());
                ba.setBoard(this);
            }

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

        public BoardActor getTopActorAt(Point p)
        {
            BoardActor r = null;

            List<BoardActor> actorlist = actors.getObjectsAt(p);

            if(actorlist != null) 
            {
                try
                {
                    foreach (BoardActor ba in actorlist)
                    {
                        if (ba.isAt(p))
                            r = ba;
                    }
                }
                catch (InvalidOperationException e) 
                {
                    Console.WriteLine("Error Board::getActorAt");
                }
            }

            return r;
        }

        public List<BoardActor> getAllActorsAt(Point p)
        {
            List<BoardActor> actorlist = actors.getObjectsAt(p);
            List<BoardActor> returnlist = new List<BoardActor>();

            if (actorlist != null)
            {
                foreach (BoardActor ba in actorlist)
                {
                    if (ba.isAt(p))
                        returnlist.Add(ba);
                }
            }

            return returnlist;
        }

        public void deleteActor(BoardActor ba)
        {
            actors.Remove(ba, ba.getBounds());
            ba.setBoard(null);
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

        public void onActorLocationChanged(BoardActor boardActor, Point newLocation)
        {
            // give our actor a new position in the quad tree
            actors.Remove(boardActor, boardActor.getBounds());
            boardActor.location = newLocation;
            actors.Add(boardActor, boardActor.getBounds());
        }
    }
}
