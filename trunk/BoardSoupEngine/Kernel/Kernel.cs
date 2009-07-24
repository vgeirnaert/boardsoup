using System.Windows.Forms;
using System;
using BoardSoupEngine.Scene;

namespace BoardSoupEngine.Kernel
{
    internal class Kernel : IEventListener, ITickable
    {
        struct eventTime
        {
            public long lastEventTime;
            public int interval; // in ten millionths of a second 
        };

        private EventDispatcher eventDispatcher;
        private eventTime ticks;
        private eventTime renderer; 


        public void setEventDispatcher(IEventDispatcher argDispatcher)
        {
        }

        public Kernel()
        {
            ticks.lastEventTime = 0;
            ticks.interval = 100000; //this value equals 1/100th of a second

            renderer.lastEventTime = 0;
            renderer.interval = 200000;

            eventDispatcher = new EventDispatcher();
            eventDispatcher.registerListener(this);

            eventDispatcher.registerListener(new Renderer.Renderer());
            eventDispatcher.registerListener(new Assets.AssetManager());
            eventDispatcher.registerListener(new Scene.SceneManager());
        }

        public void setRenderSurface(Panel surface)
        {
            Renderer.RenderSurfaceEvent e = new Renderer.RenderSurfaceEvent();
            e.surface = surface;

            eventDispatcher.submitEvent(e);
        }

        public void tick()
        {
            long now = DateTime.Now.Ticks;

            if (now - ticks.lastEventTime > ticks.interval)
            {
                eventDispatcher.submitEvent(new TickEvent());
                ticks.lastEventTime = now;
            }

            if (now - renderer.lastEventTime > renderer.interval)
            {
                eventDispatcher.submitEvent(new SceneRenderEvent());
                renderer.lastEventTime = now;
            }
        }

        public void receiveEvent(Event argEvent)
        {
        }

        public void onTick()
        {
        }

        #region ITickable Members

        void ITickable.onTick()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
