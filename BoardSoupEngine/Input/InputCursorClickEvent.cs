using BoardSoupEngine.Kernel;
using System.Drawing;
using BoardSoupEngine.Scene;
using System;

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
                //Console.WriteLine("checking location: " + location.ToString());
                BoardActor ba = ((SceneManager)module).getActorAt(location);

                if (ba != null)
                {
                    //Console.WriteLine("there is an actor at this location!");
                    if(ba.receivesInput)
                        ba.onClick();
                }
            }
        }
    }
}
