using BoardSoupEngine.Assets;
using System.Drawing;

namespace BoardSoupEngine.Renderer
{
    internal class ShapeRenderer : IRenderable
    {
        private Asset asset;
        private Renderer myRenderer;

        public ShapeRenderer()
        {
        }

        public void setAsset(Asset argAsset)
        {
            asset = argAsset;
            asset.setRenderer(this);
        }

        public void render(Point location, int rotation)
        {
        }

        public void setRenderer(Renderer argRenderer)
        {
            myRenderer = argRenderer;
        }
    }
}
