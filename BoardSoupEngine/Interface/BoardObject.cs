using BoardSoupEngine.Scene;
using BoardSoupEngine.Kernel;

namespace BoardSoupEngine.Interface
{
    public abstract class BoardObject : InterfaceObject
    {
        private Board board;

        abstract public void onTurn();
        abstract public void onBoardChanged();
    }
}
