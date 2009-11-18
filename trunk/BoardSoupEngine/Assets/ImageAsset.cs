using BoardSoupEngine.Renderer;
using System.Drawing;
using System;

namespace BoardSoupEngine.Assets
{
    internal class ImageAsset : Asset
    {
        private Image file;
        private Rectangle bounds;

        public ImageAsset()
        {
            this.setName("");
        }

        public ImageAsset(String filename)
        {
            this.setRenderer(null);
            file = new Bitmap(filename);
            bounds = new Rectangle(0, 0, file.Width, file.Height);
            this.setName(filename);
        }

        public Image getImage()
        {
            return file;
        }

        public override Rectangle getBounds()
        {
            return bounds;
        }
    }
}
