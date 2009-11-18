using System.Drawing;
using BoardSoupEngine.Assets;

namespace BoardSoupEngine.Renderer
{
    internal class ImageRenderer : Renderable
    {
        public ImageRenderer()
        {
        }

        public override void render(Point location, int rotation)
        {
            if (this.getRenderer() != null && this.getAsset() != null)
            {
                if(this.getAsset() is ImageAsset)
                    this.getRenderer().drawImage( ((ImageAsset)this.getAsset()).getImage(), location, rotation);
            }
        }

        protected override void onAssetAssigned()
        {
        }
    }
}
