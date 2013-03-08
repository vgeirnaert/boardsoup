using BoardSoupEngine.Renderer;
using System.Drawing;
using System;

namespace BoardSoupEngine.Assets
{
    internal abstract class Asset
    {
        private Renderable renderer;
        private String name;

        public void setRenderer(Renderable argRenderer)
        {
            renderer = argRenderer;

			if(renderer != null)
				this.onRendererAssigned();
        }

		public virtual void onRendererAssigned() {
		}

        public Renderable getRenderer()
        {
            return renderer;
        }

        public virtual String getName()
        {
            return name;
        }

        public void setName(String argName)
        {
            name = argName;
        }

        public void render(Point location, int rotation)
        {
            if (renderer != null)
                renderer.render(renderer.translateLocationToResolution(location), rotation);
        }

        public abstract Rectangle getBounds();
    }
}
