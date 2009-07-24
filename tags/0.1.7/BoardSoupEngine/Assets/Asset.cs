using BoardSoupEngine.Renderer;
using System.Drawing;
using System;

namespace BoardSoupEngine.Assets
{
    internal interface Asset
    {
        void setRenderer(IRenderable Renderer);
        IRenderable getRenderer();
        void render(Point location, int rotation);
        String getName();
    }
}
