using BoardSoupEngine.Kernel;
using System.Drawing;
using System.Collections.Generic;

namespace BoardSoupEngine.Assets
{
    internal class AssetManager : IEventListener, ITickable
    {
        private IEventDispatcher dispatcher;


        private ImageAsset ia;
        int rotate = 0;

        public AssetManager()
        {
            ia = new ImageAsset("D:\\C#\\BoardSoup\\BoardSoup\\Resources\\Icon1.ico");
        }

        public void setEventDispatcher(IEventDispatcher argDispatcher)
        {
            dispatcher = argDispatcher;
            AssetToRendererEvent atre = new AssetToRendererEvent();
            atre.setAsset(ia);
            dispatcher.submitEvent(atre);
            
        }

        public void receiveEvent(Event argEvent)
        {
            argEvent.execute(this);
        }

        public void onTick()
        {
            dispatcher.submitEvent(new Renderer.RendererStartSceneEvent());
            rotate++;
            int x = 0;
            int y = 100;
            
            for(int z = 0; z < 100; z++)
            {
                x += 50;
                ia.render(new Point(x, y), rotate);

                if (x > 999)
                {
                    y += 50;
                    x = 0;
                }
            }
            dispatcher.submitEvent(new Renderer.RendererEndSceneEvent());
        }

    }
}
