using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BoardSoupEngine.Utilities {
	class CoordinateTranslator {
		private static float boardSize = 1050.0f;

		public static Point sceneToScreenLocation(Point location, Size screen) {
			// all locations are relative to a 1050x1050 screen (yes they are weird coordinates since a board game board is usually square)
			// so we translate them to new coordinates based on the size of our panel
			return new Point((int)(screen.Width * (location.X / boardSize)), (int)(screen.Height * (location.Y / boardSize)));
		}

		public static Size sceneToScreenSize(Size size, Size screen) {
			return new Size((int)(screen.Width * (size.Width / boardSize)), (int)(screen.Height * (size.Height / boardSize)));
		}

		public static Point screenToSceneLocation(Point location, Size screen) {
			return new Point((int)(location.X * (boardSize / screen.Width)), (int)(location.Y * (boardSize / screen.Height)));	
		}

		public static int sceneToScreenAmount(int amount, int screenSize) {
			return (int)Math.Ceiling(screenSize * (amount / boardSize));
		}

		public static void setBoardSize(float size) {
			boardSize = size;
		}
	}
}
