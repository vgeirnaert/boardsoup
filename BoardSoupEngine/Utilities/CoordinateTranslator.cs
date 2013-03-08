using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BoardSoupEngine.Utilities {
	class CoordinateTranslator {
		private static Size boardSize = new Size(1050, 1050);

		public static Point sceneToScreenLocation(Point location, Size screen) {
			// all locations are relative to a 1050x1050 screen (yes they are weird coordinates since a board game board is usually square)
			// so we translate them to new coordinates based on the size of our panel
			return new Point((int)(screen.Width * ((float)location.X / boardSize.Width)), (int)(screen.Height * ((float)location.Y / boardSize.Height)));
		}

		public static Size sceneToScreenSize(Size size, Size screen) {
			return new Size((int)(screen.Width * ((float)size.Width / boardSize.Width)), (int)(screen.Height * ((float)size.Height / boardSize.Height)));
		}

		public static Point screenToSceneLocation(Point location, Size screen) {
			return new Point((int)(location.X * ((float)boardSize.Width / screen.Width)), (int)(location.Y * ((float)boardSize.Height / screen.Height)));	
		}

		public static int sceneToScreenAmount(float amount, int screenSize) {
			return (int)Math.Ceiling(screenSize * (amount / boardSize.Height));
		}

		public static void setBoardSize(Size size) {
			//boardSize = size;
		}
	}
}
