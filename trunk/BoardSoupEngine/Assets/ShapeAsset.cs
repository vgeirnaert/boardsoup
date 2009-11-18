using BoardSoupEngine.Renderer;
using BoardSoupEngine.Kernel;
using System.Drawing;
using System;

namespace BoardSoupEngine.Assets
{
    internal class ShapeAsset : Asset
    {
        public ShapeAsset()
        {
            this.setName("");
        }

        public ShapeAsset(String argName)
        {
            this.setName(argName);
        }


        public override Rectangle getBounds()
        {
            return new Rectangle();
        }
    }
}
