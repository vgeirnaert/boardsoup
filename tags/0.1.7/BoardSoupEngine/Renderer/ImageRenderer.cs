using System.Drawing;
using BoardSoupEngine.Assets;

namespace BoardSoupEngine.Renderer
{
    internal class ImageRenderer : IRenderable
    {
        private Asset asset;
        private Renderer myRenderer;

        public ImageRenderer()
        {
        }

        public void setAsset(Asset argAsset)
        {
            asset = argAsset;
            asset.setRenderer(this);
        }

        public void render(Point location, int rotation)
        {
            if (myRenderer != null && asset != null && asset is ImageAsset)
            {
                myRenderer.drawImage( ((ImageAsset)asset).getImage(), location, rotation);
            }
        }

        public void setRenderer(Renderer argRenderer)
        {
            myRenderer = argRenderer;
        }
    }
}
