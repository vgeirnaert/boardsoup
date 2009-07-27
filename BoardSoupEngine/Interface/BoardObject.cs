using BoardSoupEngine.Scene;
using BoardSoupEngine.Kernel;
using System;

namespace BoardSoupEngine.Interface
{
    public abstract class BoardObject : InterfaceObject
    {
        private Board board;
        protected String name;

        abstract public void onBoardChanged();

        public BoardObject(String argName)
        {
            name = argName;
            CreateInternalBoardEvent e = new CreateInternalBoardEvent(this);
            dispatcher = BoardSoupEngine.getInstance().getInternalDispatcher();
            dispatcher.submitEvent(e);
        }

        public String getName()
        {
            return name;
        }

        public void addActor(ActorObject a)
        {
            if (board != null)
                board.addActor(a.getActor());
        }

        internal void setBoard(Board argBoard)
        {
            board = argBoard;
        }

        public void clearBoard()
        {
            board.clearBoard();
        }
    }
}
