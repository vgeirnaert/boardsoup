using BoardSoupEngine.Kernel;
using System.Drawing;
using BoardSoupEngine.Scene;

namespace BoardSoupEngine.Input
{
    class InputCursorMoveEvent : Event
    {
        private Point location;
        private Cursor cursor;

        public InputCursorMoveEvent(Point spot, Cursor myCursor)
        {
            location = spot;
            cursor = myCursor;
        }

        public override void execute(IEventListener module)
        {
            if (module is SceneManager)
            {
                BoardActor ba = ((SceneManager)module).getActorAt(location);

                if (ba != null)
                {
                    if (ba.receivesInput)
                        cursor.addCollidingActor(ba);
                }
            }
        }
    }
}