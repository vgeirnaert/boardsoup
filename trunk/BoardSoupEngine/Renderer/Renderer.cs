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
        private Graphics backBufferGraphics;
        private Graphics renderGraphics;
        private Bitmap backBuffer;

      /*private Image pew;
        private int x = 0;
        private int y = 0;
        private int newrotation = 0;*/

        public void setEventDispatcher(IEventDispatcher argDispatcher)
        {
            dispatcher = argDispatcher;
        }

        public Renderer()
        {
            //pew = new Bitmap("D:\\C#\\BoardSoup\\BoardSoup\\Resources\\Icon1.ico");
            
        }

        public void setSurface(Panel surface)
        {
            renderSurface = surface;
            backBuffer = new Bitmap(renderSurface.Width, renderSurface.Height);
            //backBuffer = new Bitmap(1050, 200);
            renderGraphics = renderSurface.CreateGraphics();
            backBufferGraphics = Graphics.FromImage(backBuffer);
            
        }

        public void receiveEvent(Event argEvent)
        {
            argEvent.execute(this);
        }

        public void onTick()
        {
            /*endScene();
            beginScene();
            drawImage(pew, new Point(x, y), newrotation);

            x++;
            y++;
            newrotation--;*/
            
        }

        public void drawImage(Image argImage, Point location, int rotation)
        {
            //Console.WriteLine("drawing");

            //create a new empty bitmap to hold rotated image
            /*Image result = new Bitmap(argImage.Width, argImage.Height);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(result);
            
            //move rotation point to center of image, rotate and return rotation point to normal
            g.TranslateTransform((float)argImage.Width / 2, (float)argImage.Height / 2);
            g.RotateTransform(rotation);
            g.TranslateTransform(-(float)argImage.Width / 2, -(float)argImage.Height / 2);
            
            //draw passed in image onto graphics object
            g.DrawImage(argImage, new Point(0, 0));

            backBufferGraphics.DrawImage(result, location);*/

            Image result = new Bitmap(argImage.Width, argImage.Height);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(result);

            //move rotation point to center of image, rotate and return rotation point to normal
            g.TranslateTransform((float)argImage.Width / 2, (float)argImage.Height / 2);
            g.RotateTransform(rotation);
            g.TranslateTransform(-(float)argImage.Width / 2, -(float)argImage.Height / 2);

            //draw passed in image onto graphics object
            g.DrawImage(argImage, new Point(0, 0));

            renderGraphics.DrawImage(result, location);
            g.Dispose();
        }

        public void beginScene()
        {
            this.clear(renderSurface.BackColor);
        }

        private void clear(Color argColor)
        {
            backBufferGraphics.Clear(argColor);
        }

        public void endScene()
        {
            //renderGraphics.DrawImage(backBuffer, 0, 0);
            //renderGraphics = backBufferGraphics;
            renderSurface.Invalidate();

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
