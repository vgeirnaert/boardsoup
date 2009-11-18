using System;
using System.Text;
using System.Drawing;
using BoardSoupEngine.Assets;

namespace BoardSoupEngine.Renderer
{
    class TextRenderer : Renderable
    {
        public TextRenderer()
        {
        }

        public override void render(Point location, int rotation)
        {
            if (this.getRenderer() != null && this.getAsset() != null)
            {
                if (this.getAsset() is TextAsset)
                    this.getRenderer().drawText(((TextAsset)this.getAsset()).getText(), location, rotation);
            }
        }
    }
}
