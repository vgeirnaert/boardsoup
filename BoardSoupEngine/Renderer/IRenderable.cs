using System.Drawing;
using BoardSoupEngine.Assets;

namespace BoardSoupEngine.Renderer
{
    internal interface IRenderable
    {
        void render(Point location, int rotation);
        void setRenderer(Renderer argRenderer);
        void setAsset(Asset argAsset);
    }
}
