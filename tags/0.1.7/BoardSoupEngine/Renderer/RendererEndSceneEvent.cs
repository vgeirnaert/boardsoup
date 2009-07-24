using System;
using BoardSoupEngine.Kernel;

namespace BoardSoupEngine.Renderer
{
    class RendererEndSceneEvent : RenderEvent
    {
        public RendererEndSceneEvent()
        {
        }

        public override void execute(IEventListener module)
        {
            if (module is Renderer)
            {
                ((Renderer)module).endScene();
            }
        }
    }
}
