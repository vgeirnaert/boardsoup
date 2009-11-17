using BoardSoupEngine.Scene;
using BoardSoupEngine.Kernel;
using System;
using System.Collections.Generic;

namespace BoardSoupEngine.Interface
{
    public abstract class BoardObject : InterfaceObject
    {
        private Board board;
        protected String name;

        abstract public void onBoardChanged();

        public BoardObject(String argName) // TODO: add board dimensions to this, pass board dimensions to event and eventually to board - needed for quad tree
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

        public void deleteFromBoard(ActorObject a)
        {
            if(board != null)
                board.deleteActor(a.getActor());
        }

        public List<ActorObject> getAllBoardActors()
        {
            return board.getAllActorInterfaces();
        }
    }
}
