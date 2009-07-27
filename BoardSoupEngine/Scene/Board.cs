using System.Collections.Generic;
using BoardSoupEngine.Kernel;
using System;
using System.Drawing;
using BoardSoupEngine.Interface;
namespace BoardSoupEngine.Scene
{
    internal class Board : ITickable
    {
        private bool active = true;
        private List<BoardActor> actors;
        private BoardObject myInterfaceObject;
        private IEventDispatcher dispatcher;

        public Board()
        {
            actors = new List<BoardActor>();
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

            foreach (BoardActor a in actors)
                a.onTick();
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
                actors.Add(ba);

            if (myInterfaceObject != null)
                myInterfaceObject.onBoardChanged();
        }

        public void setInterfaceObject(BoardObject bo)
        {
            myInterfaceObject = bo;
            myInterfaceObject.setBoard(this);
        }

        public void clearBoard()
        {
            actors.Clear();
        }
    }
}
