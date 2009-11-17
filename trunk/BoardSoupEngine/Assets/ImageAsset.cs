using BoardSoupEngine.Renderer;
using System.Drawing;
using System;

namespace BoardSoupEngine.Assets
{
    internal class ImageAsset : Asset
    {
        private Image file;
        private IRenderable renderer;
        private String name;
        private Rectangle bounds;


        public ImageAsset()
        {
            name = "";
        }

        public ImageAsset(String filename)
        {
            renderer = null;
            file = new Bitmap(filename);
            bounds = new Rectangle(0, 0, file.Width, file.Height);
            name = filename;
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
            if(renderer != null)
                renderer.render(location, rotation);
        }

        public String getName()
        {
            return name;
        }

        public Rectangle getBounds()
        {
            return bounds;
        }
    }
}
