using System;
using System.Text;
using System.Drawing;
using BoardSoupEngine.Assets;

namespace BoardSoupEngine.Renderer
{
    class TextRenderer : Renderable
    {
        Font myFont;

        public TextRenderer()
        {
            myFont = null;
        }

        public override void render(Point location, int rotation)
        {
            if (this.getRenderer() != null && this.getAsset() != null)
            {
                if (this.getAsset() is TextAsset)
                    this.getRenderer().drawText(((TextAsset)this.getAsset()).getText(), myFont, location, rotation, ((TextAsset)this.getAsset()).getBounds());
            }
        }

        protected override void onAssetAssigned()
        {
            if (this.getAsset() is TextAsset)
            {
                myFont = new Font(((TextAsset)this.getAsset()).getFontName(), ((TextAsset)this.getAsset()).getFontSize());
                ((TextAsset)this.getAsset()).setBounds(this.getRenderer().getBoundsForString(((TextAsset)this.getAsset()).getText(), myFont));
            }
        }
    }
}
