using BoardSoupEngine.Kernel;
using System.Windows.Forms;
using BoardSoupEngine.Input;
using BoardSoupEngine.Utilities;

namespace BoardSoupEngine.Renderer
{
    class RenderSurfaceEvent : Event
    {
        public Panel surface;

        public override void execute(BoardSoupEngine.Kernel.IEventListener module)
        {
            if (module is Renderer)
                ((Renderer)module).setSurface(surface);

            if(module is InputManager)
                ((InputManager)module).setInputSurface(surface);

			CoordinateTranslator.setBoardSize(surface.Size);
        }

        public RenderSurfaceEvent()
        {
        }
    }
}
