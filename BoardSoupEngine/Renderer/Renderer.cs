using BoardSoupEngine.Kernel;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using BoardSoupEngine.Assets;
using BoardSoupEngine.Utilities;

namespace BoardSoupEngine.Renderer {
	internal class Renderer : IEventListener, ITickable {
		private Panel renderSurface;
		private IEventDispatcher dispatcher;
		private BufferedGraphics myBuffer;
		private Graphics helperGraphics;    // we have this so non-render calls do not interfere with our rendering graphics

		public void setEventDispatcher(IEventDispatcher argDispatcher) {
			dispatcher = argDispatcher;
		}

		public Renderer() {
			Logger.log("Renderer: Loading Renderer...", LEVEL.DEBUG);
			Bitmap bm = new Bitmap(1, 1);
			helperGraphics = Graphics.FromImage(bm);
			Logger.log("Renderer: Renderer loaded", LEVEL.DEBUG);
		}

		public void setSurface(Panel surface) {
			renderSurface = surface;
			BufferedGraphicsContext bgc = BufferedGraphicsManager.Current;
			myBuffer = bgc.Allocate(surface.CreateGraphics(), surface.DisplayRectangle);
			Logger.log("Renderer: surface set", LEVEL.DEBUG);
		}

		public void receiveEvent(Event argEvent) {
			argEvent.execute(this);
		}

		public void onTick() {
		}

		private int normalizeRotation(int rotation) {
			return (rotation % 360);
		}

		private void prepareCanvasOrientation(int argRotation, int argX, int argY) {
			// move the 0,0 point of our graphics to where the center of our image should be
			myBuffer.Graphics.TranslateTransform(argX, argY);

			// rotate the graphics (inverse to our graphics rotation) to prepare for drawing
			myBuffer.Graphics.RotateTransform(-argRotation);

		}

		private void resetCanvasOrientation(int argRotation, int argX, int argY) {
			// rotate our graphics back
			myBuffer.Graphics.RotateTransform(argRotation);

			// translate back to the real 0,0 location
			myBuffer.Graphics.TranslateTransform(-argX, -argY);
		}

		public void drawImage(Image argImage, Point location, int rotation) {
			// make sure rotation is in the range 0...359
			rotation = normalizeRotation(rotation);

			Size imageSize = CoordinateTranslator.sceneToScreenSize(argImage.Size, renderSurface.Size);
			Rectangle imageRect = new Rectangle(new Point(-(imageSize.Width / 2), -(imageSize.Height / 2)), imageSize);

			int x = location.X + (imageSize.Width / 2);
			int y = location.Y + (imageSize.Height / 2);

			// move and rotate canvas
			prepareCanvasOrientation(rotation, x, y);
			// draw
			myBuffer.Graphics.DrawImage(argImage, imageRect);
			// reset canvas
			resetCanvasOrientation(rotation, x, y);

		}

		public void drawText(String argText, Font argFont, Color argColor, Point location, int rotation, Rectangle bounds) {
			// make sure rotation is in the range 0...359
			rotation = normalizeRotation(rotation);

			Rectangle imageRect = new Rectangle(new Point(-(bounds.Size.Width / 2), -(bounds.Size.Height / 2)),  bounds.Size);

			int x = location.X + (bounds.Size.Width / 2);
			int y = location.Y + (bounds.Size.Height / 2);

			prepareCanvasOrientation(rotation, x, y);

			// draw the text onto our buffer
			myBuffer.Graphics.DrawString(argText, argFont, new SolidBrush(argColor), imageRect);

			resetCanvasOrientation(rotation, x, y);

		}

		public void beginScene() {
			this.clear(renderSurface.BackColor);
		}

		private void clear(Color argColor) {
			// clear buffer
			myBuffer.Graphics.Clear(argColor);
		}

		public void endScene() {
			// render buffer
			myBuffer.Render();
		}

		public void makeAssetRenderer(Asset argAsset) {
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

		public Rectangle getBoundsForString(String argText, Font argFont) {
			// early out
			if (argText == "")
				return new Rectangle(0, 0, 0, 0);

			SizeF size = helperGraphics.MeasureString(argText, argFont);

			return new Rectangle(0, 0, (int)size.Width + 1, (int)size.Height);
		}

		public Point translateLocationToResolution(Point location) {
			return CoordinateTranslator.sceneToScreenLocation(location, renderSurface.Size);
		}

		public Size translateSizeToResolution(Size size) {
			return CoordinateTranslator.sceneToScreenSize(size, renderSurface.Size);
		}

		public int sceneToScreenAmount(int amount) {
			return CoordinateTranslator.sceneToScreenAmount(amount, renderSurface.Size.Height);
		}
	}
}
