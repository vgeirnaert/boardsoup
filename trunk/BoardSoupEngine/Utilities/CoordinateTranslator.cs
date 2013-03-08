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
			//return new Point((int)(screen.Width * (location.X / boardSize)), (int)(screen.Height * (location.Y / boardSize)));
			return new Point((int)(boardSize.Width * ((float)location.X / screen.Width)), (int)(boardSize.Height * ((float)location.Y / screen.Height)));
		}

		public static Size sceneToScreenSize(Size size, Size screen) {
			return new Size((int)(boardSize.Width * ((float)size.Width / screen.Width)), (int)(boardSize.Height * ((float)size.Height / screen.Height)));
		}

		public static Point screenToSceneLocation(Point location, Size screen) {
			return new Point((int)(location.X * ((float)boardSize.Width / screen.Width)), (int)(location.Y * ((float)boardSize.Height / screen.Height)));	
		}

		public static int sceneToScreenAmount(float amount, int screenSize) {
			return (int)Math.Ceiling(boardSize.Height * (amount / screenSize));
		}

		public static void setBoardSize(Size size) {
			boardSize = size;
		}
	}
}
