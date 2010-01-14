using BoardSoupEngine.Kernel;
using System.Drawing;
using BoardSoupEngine.Scene;
using System.Collections.Generic;

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
                List<BoardActor> list = ((SceneManager)module).getAllActorsAt(location);

                if (list != null)
                {
                    foreach (BoardActor ba in list)
                    {
                        if (ba.receivesInput)
                            cursor.addCollidingActor(ba);
                    }
                }
            }
        }
    }
}