using BoardSoupEngine.Renderer;
using System.Drawing;

namespace BoardSoupEngine.Assets
{
    internal interface Asset
    {
        void setRenderer(IRenderable Renderer);
        IRenderable getRenderer();
        void render(Point location, int rotation);
    }
}
