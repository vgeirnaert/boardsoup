using BoardSoupEngine.Kernel;
using System.Drawing;
using System.Collections.Generic;

namespace BoardSoupEngine.Assets
{
    internal class AssetManager : IEventListener, ITickable
    {
        private IEventDispatcher dispatcher;
        private List<ImageAsset> ia;
        int rotate = 0;

        public AssetManager()
        {
            ia = new List<ImageAsset>();
            for(int i = 0; i < 10; i++)
                ia.Add(new ImageAsset("D:\\C#\\BoardSoup\\BoardSoup\\Resources\\Icon1.ico"));
        }

        public void setEventDispatcher(IEventDispatcher argDispatcher)
        {
            dispatcher = argDispatcher;

            foreach (ImageAsset i in ia)
            {
                AssetToRendererEvent atre = new AssetToRendererEvent();
                atre.setAsset(i);
                dispatcher.submitEvent(atre);
            }
        }

        public void receiveEvent(Event argEvent)
        {
            argEvent.execute(this);
        }

        public void onTick()
        {
            dispatcher.submitEvent(new Renderer.RendererStartSceneEvent());
            rotate+=2;
            int x = 0;
            int y = 100;
            
            foreach (ImageAsset i in ia)
            {
                x += 50;
                i.render(new Point(x+rotate, y+rotate), rotate);

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
