using BoardSoupEngine.Kernel;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using BoardSoupEngine.Assets;

namespace BoardSoupEngine.Renderer
{
    internal class Renderer : IEventListener, ITickable
    {
        private Panel renderSurface;
        private IEventDispatcher dispatcher;
        private BufferedGraphics myBuffer;

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
            BufferedGraphicsContext bgc = BufferedGraphicsManager.Current;
            myBuffer = bgc.Allocate(surface.CreateGraphics(), surface.DisplayRectangle);
        }

        public void receiveEvent(Event argEvent)
        {
            argEvent.execute(this);
        }

        public void onTick()
        {          
        }

        public void drawImage(Image argImage, Point location, int rotation)
        {
            // calculate the location of the center of the image
            int x = location.X + (argImage.Width / 2);
            int y = location.Y + (argImage.Height / 2);
            
            // move the 0,0 point of our graphics to where the center of our image should be
            myBuffer.Graphics.TranslateTransform(x, y);

            // rotate the graphics (inverse to our graphics rotation) to prepare for drawing
            myBuffer.Graphics.RotateTransform(-rotation);
            
            // draw the image onto our buffer, making sure that the center of the graphic is located at point 0,0
            myBuffer.Graphics.DrawImage(argImage, -(argImage.Width / 2), -(argImage.Height / 2));

            // rotate our graphics back
            myBuffer.Graphics.RotateTransform(rotation);

            // translate back to the real 0,0 location
            myBuffer.Graphics.TranslateTransform( -x, -y ); 
        }

        public void beginScene()
        {
            this.clear(renderSurface.BackColor);
        }

        private void clear(Color argColor)
        {
            // clear buffer
            myBuffer.Graphics.Clear(argColor);
        }

        public void endScene()
        {
            // render buffer
            myBuffer.Render();
        }

        public void makeAssetRenderer(Asset argAsset)
        {
            IRenderable result = null;

            if (argAsset is ImageAsset)
                result = new ImageRenderer();
            else if (argAsset is ShapeAsset)
                result = new ShapeRenderer();

            result.setRenderer(this);
            result.setAsset(argAsset);
        }
    }
}
