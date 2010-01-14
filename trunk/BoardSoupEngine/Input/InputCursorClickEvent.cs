using BoardSoupEngine.Kernel;
using System.Drawing;
using BoardSoupEngine.Scene;
using System;
using System.Collections.Generic;

namespace BoardSoupEngine.Input
{
    internal class InputCursorClickEvent : Event
    {
        private Point location;

        public InputCursorClickEvent(Point spot)
        {
            location = spot;
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
                            ba.onClick();
                    }
                }
            }
        }
    }
}
