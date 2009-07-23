using BoardSoupEngine.Renderer;
using System.Drawing;
using System;

namespace BoardSoupEngine.Assets
{
    internal class ImageAsset : Asset
    {
        Image file;
        IRenderable renderer;

        public ImageAsset()
        {
        }

        public ImageAsset(String filename)
        {
            renderer = null;
            file = new Bitmap(filename);
        }

        public void setRenderer(IRenderable argRenderer)
        {
            renderer = argRenderer;
        }

        public Image getImage()
        {
            return file;
        }

        public IRenderable getRenderer()
        {
            return renderer;
        }

        public void render(Point location, int rotation)
        {
            renderer.render(location, rotation);
        }
    }
}
