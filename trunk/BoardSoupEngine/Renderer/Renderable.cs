using System.Drawing;
using BoardSoupEngine.Assets;

namespace BoardSoupEngine.Renderer
{
    internal abstract class Renderable
    {
        private Renderer theRenderer;
        private Asset theAsset;

        abstract public void render(Point location, int rotation);
        abstract protected void onAssetAssigned();
        
        public void setRenderer(Renderer argRenderer)
        {
            theRenderer = argRenderer;
        }

        public void setAsset(Asset argAsset)
        {
            theAsset = argAsset;
            argAsset.setRenderer(this);
            onAssetAssigned();
        }

        public Renderer getRenderer()
        {
            return theRenderer;
        }

        public Asset getAsset()
        {
            return theAsset;
        }
    }
}
