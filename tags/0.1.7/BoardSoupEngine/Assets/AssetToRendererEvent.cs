using System;
using System.Collections.Generic;
using System.Text;
using BoardSoupEngine.Renderer;
using BoardSoupEngine.Kernel;

namespace BoardSoupEngine.Assets
{
    class AssetToRendererEvent : AssetEvent
    {
        private Asset myAsset;

        public AssetToRendererEvent()
        {
        }

        public void setAsset(Asset argAsset)
        {
            myAsset = argAsset;
        }

        override public void execute(IEventListener module)
        {
            if (module is Renderer.Renderer)
                ((Renderer.Renderer)module).makeAssetRenderer(myAsset);
        }
    }
}
