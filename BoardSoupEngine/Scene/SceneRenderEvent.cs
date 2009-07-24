using System;
using BoardSoupEngine.Kernel;

namespace BoardSoupEngine.Scene
{
    class SceneRenderEvent : SceneEvent
    {
        public SceneRenderEvent()
        {
        }

        public override void execute(IEventListener module)
        {
            if (module is SceneManager)
                ((SceneManager)module).renderActiveBoard();
        }
    }
}
