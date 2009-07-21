using BoardSoupEngine.Kernel;
using System.Windows.Forms;
using System;

namespace BoardSoupEngine.Renderer
{
    internal class Renderer : IEventListener
    {
        private Panel renderSurface;
        IEventDispatcher dispatcher;
        int x = 0;
        int y = 0;

        public void setEventDispatcher(IEventDispatcher argDispatcher)
        {
            dispatcher = argDispatcher;
        }

        public Renderer()
        {
        }

        public void setSurface(Panel surface)
        {
            renderSurface = surface;
        }

        public void receiveEvent(Event argEvent)
        {
            argEvent.execute(this);
        }

        public void onTick()
        {
            x++;
            y++;
            System.Drawing.Graphics g = renderSurface.CreateGraphics();
            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), 0, 0, x, y);
        }
    }
}
