using BoardSoupEngine.Kernel;
using BoardSoupEngine.Scene;

namespace BoardSoupEngine.Interface
{
    internal class CreateInternalBoardEvent : Event
    {
        BoardObject bo;

        public CreateInternalBoardEvent()
        {
            bo = null;
        }

        public CreateInternalBoardEvent(BoardObject argBO)
        {
            bo = argBO;
        }

        public override void execute(IEventListener module)
        {
            if (module is SceneManager && bo != null)
            {
                Board b = ((SceneManager)module).createBoard(bo.getName());
                b.setInterfaceObject(bo);
                bo.onEngineObjectCreated();
            }
        }
    }
}
