using BoardSoupEngine.Kernel;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using BoardSoupEngine.Assets;
using BoardSoupEngine.Utilities;

namespace BoardSoupEngine.Renderer
{
    internal class Renderer : IEventListener, ITickable
    {
        private Panel renderSurface;
        private IEventDispatcher dispatcher;
        private BufferedGraphics myBuffer;
        private Graphics helperGraphics;    // we have this so non-render calls do not interfere with our rendering graphics

        public void setEventDispatcher(IEventDispatcher argDispatcher)
        {
            dispatcher = argDispatcher;
        }

        public Renderer()
        {
            Logger.log("Renderer: Loading Renderer...", LEVEL.DEBUG);
            Bitmap bm = new Bitmap(1, 1);
            helperGraphics = Graphics.FromImage(bm);
            Logger.log("Renderer: Renderer loaded", LEVEL.DEBUG);
        }

        public void setSurface(Panel surface)
        {
            renderSurface = surface;
            BufferedGraphicsContext bgc = BufferedGraphicsManager.Current;
            myBuffer = bgc.Allocate(surface.CreateGraphics(), surface.DisplayRectangle);
            Logger.log("Renderer: surface set", LEVEL.DEBUG);
        }

        public void receiveEvent(Event argEvent)
        {
            argEvent.execute(this);
        }

        public void onTick()
        {          
        }

        private int normalizeRotation(int rotation)
        {
            return (rotation % 360);
        }

        private void prepareCanvasOrientation(int argRotation, int argX, int argY)
        {
            // move the 0,0 point of our graphics to where the center of our image should be
            myBuffer.Graphics.TranslateTransform(argX, argY);

            // rotate the graphics (inverse to our graphics rotation) to prepare for drawing
            myBuffer.Graphics.RotateTransform(-argRotation);

        }

        private void resetCanvasOrientation(int argRotation, int argX, int argY)
        {
            // rotate our graphics back
            myBuffer.Graphics.RotateTransform(argRotation);

            // translate back to the real 0,0 location
            myBuffer.Graphics.TranslateTransform(-argX, -argY);
        }

        public void drawImage(Image argImage, Point location, int rotation)
        {
            // make sure rotation is in the range 0...359
            rotation = normalizeRotation(rotation);
            
            // early outs
            switch (rotation)
            {
                // the image doesnt need to be rotated, so we can just draw it
                case 0:
                    myBuffer.Graphics.DrawImageUnscaled(argImage, location);
                    break;
                /*case 90:    // the image has one of the following 3 rotations, we can use the fast RotateFlip() call
                    argImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    myBuffer.Graphics.DrawImageUnscaled(argImage, location);
                    break;
                /*case 180:
                    argImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    myBuffer.Graphics.DrawImageUnscaled(argImage, location);
                    break;
                /*case 270:
                    argImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    myBuffer.Graphics.DrawImageUnscaled(argImage, location);
                    break;*/
                default: // arbitrary rotation
                    // calculate the location of the center of the image
                    int x = location.X + (argImage.Width / 2);
                    int y = location.Y + (argImage.Height / 2);

                    prepareCanvasOrientation(rotation, x, y);

                    // draw the image onto our buffer, making sure that the center of the graphic is located at point 0,0
                    myBuffer.Graphics.DrawImageUnscaled(argImage, -(argImage.Width / 2), -(argImage.Height / 2));

                    resetCanvasOrientation(rotation, x, y);
                    break;
            }
        }

        public void drawText(String argText, Font argFont, Point location, int rotation, Rectangle bounds)
        {
            // make sure rotation is in the range 0...359
            rotation = normalizeRotation(rotation);
            
            // early outs
            switch (rotation)
            {
                case 0:
                    myBuffer.Graphics.DrawString(argText, argFont, new SolidBrush(Color.LightBlue), location);
                    break;
                default: // arbitrary rotation
                    int x = location.X + (bounds.Width / 2);
                    int y = location.Y + (bounds.Height / 2);

                    prepareCanvasOrientation(rotation, x, y);

                    // draw the text onto our buffer, 
                    myBuffer.Graphics.DrawString(argText, argFont, new SolidBrush(Color.LightBlue), -(bounds.Width / 2), -(bounds.Height / 2));

                    resetCanvasOrientation(rotation, x, y);
                    break;
            }
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
            Renderable result = null;

            if (argAsset is ImageAsset)
                result = new ImageRenderer();
            else if (argAsset is ShapeAsset)
                result = new ShapeRenderer();
            else if (argAsset is TextAsset)
                result = new TextRenderer();

            result.setRenderer(this);
            result.setAsset(argAsset);
        }

        public Rectangle getBoundsForString(String argText, Font argFont)
        {
            // early out
            if (argText == "")
                return new Rectangle(0, 0, 0, 0);

            SizeF size = helperGraphics.MeasureString(argText, argFont);
            
            return new Rectangle(0, 0, (int)size.Width, (int)size.Height);
        }
    }
}
