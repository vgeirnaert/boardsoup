using System.Windows.Forms;
using System;

namespace BoardSoupEngine.Kernel
{
    internal class Kernel : IEventListener
    {
        private EventDispatcher eventDispatcher;
        private long timeOfLastTick = 0;
        private int tickInterval = 100000;  // in ten millionths of a second - this value equals 1/100th of a second

        public void setEventDispatcher(IEventDispatcher argDispatcher)
        {
        }

        public Kernel()
        {
            eventDispatcher = new EventDispatcher();
            eventDispatcher.registerListener(this);

            eventDispatcher.registerListener(new Renderer.Renderer());
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

            if (now - timeOfLastTick > tickInterval)
            {
                eventDispatcher.submitEvent(new TickEvent());
                timeOfLastTick = now;
            }
        }

        public void receiveEvent(Event argEvent)
        {
        }

        public void onTick()
        {
        }
    }
}
