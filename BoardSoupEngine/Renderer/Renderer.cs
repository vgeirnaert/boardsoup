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
        private Graphics myGraphics;

        private Image pew;
        private int x = 0;
        private int y = 0;
        private int newrotation = 0;

        public void setEventDispatcher(IEventDispatcher argDispatcher)
        {
            dispatcher = argDispatcher;
        }

        public Renderer()
        {
            pew = new Bitmap("D:\\C#\\BoardSoup\\BoardSoup\\Resources\\Icon1.ico");
        }

        public void setSurface(Panel surface)
        {
            renderSurface = surface;
            myGraphics = renderSurface.CreateGraphics();
        }

        public void receiveEvent(Event argEvent)
        {
            argEvent.execute(this);
        }

        public void onTick()
        {
            beginScene();
            drawImage(pew, new Point(x, y), newrotation);

            x++;
            y++;
            newrotation--;
        }

        public void drawImage(Image argImage, Point location, int rotation)
        {
            //create a new empty bitmap to hold rotated image
            Image result = new Bitmap(argImage.Width, argImage.Height);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(result);
            
            //move rotation point to center of image, rotate and return rotation point to normal
            g.TranslateTransform((float)argImage.Width / 2, (float)argImage.Height / 2);
            g.RotateTransform(rotation);
            g.TranslateTransform(-(float)argImage.Width / 2, -(float)argImage.Height / 2);
            
            //draw passed in image onto graphics object
            g.DrawImage(argImage, new Point(0, 0));

            myGraphics.DrawImage(result, location);
            g.Dispose();
        }

        public void beginScene()
        {
            this.clear(renderSurface.BackColor);
        }

        private void clear(Color argColor)
        {
            myGraphics.Clear(argColor);
        }

        public void endScene()
        {
            renderSurface.Update();
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
