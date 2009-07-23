using BoardSoupEngine.Kernel;
using System.Windows.Forms;

namespace BoardSoupEngine.Renderer
{
    class RenderSurfaceEvent : Event
    {
        public Panel surface;

        public override void execute(BoardSoupEngine.Kernel.IEventListener module)
        {
            if (module is Renderer)
                ((Renderer)module).setSurface(surface);
        }

        public RenderSurfaceEvent()
        {
        }
    }
}
