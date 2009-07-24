using System;
using BoardSoupEngine.Kernel;

namespace BoardSoupEngine.Renderer
{
    class RendererStartSceneEvent : RenderEvent
    {
        public RendererStartSceneEvent()
        {
        }

        public override void execute(IEventListener module)
        {
            if (module is Renderer)
            {
                ((Renderer)module).beginScene();
            }
        }
    }
}
