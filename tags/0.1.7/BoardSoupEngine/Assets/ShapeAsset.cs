using BoardSoupEngine.Renderer;
using BoardSoupEngine.Kernel;
using System.Drawing;
using System;

namespace BoardSoupEngine.Assets
{
    internal class ShapeAsset : Asset
    {
        private String name;

        public ShapeAsset()
        {
            name = "";
        }

        public ShapeAsset(String argName)
        {
            name = argName;
        }

        public void setRenderer(IRenderable argRenderer)
        {
        }

        public IRenderable getRenderer()
        {
            return null;
        }

        public void render(Point location, int rotation)
        {
        }

        public String getName()
        {
            return name;
        }
    }
}
