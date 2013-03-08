using System;
using System.Text;
using System.Drawing;

namespace BoardSoupEngine.Assets {
	class TextAsset : Asset {
		private String text;
		private Rectangle bounds;
		private String font;
		private int fontsize;
		private Color color;

		public TextAsset() {
			font = "Arial";
			fontsize = 16;
			this.setText("");
			this.setBounds(new Rectangle());
			color = Color.White;
		}

		public TextAsset(String argText) {
			font = "Arial";
			fontsize = 16;
			this.setText(argText);
			this.setBounds(new Rectangle());
			color = Color.White;
		}

		public TextAsset(String argText, String argFont, int argSize, Color argColor) {
			font = argFont;
			fontsize = argSize;
			this.setText(argText);
			this.setBounds(new Rectangle());
			color = argColor;
		}

		public String getText() {
			return text;
		}

		public void setText(String argText) {
			text = argText;
			this.setName(text);
		}

		public override Rectangle getBounds() {
			return bounds;
		}

		public void setBounds(Rectangle argBounds) {
			bounds = argBounds;
		}

		public String getFontName() {
			return font;
		}

		public int getFontSize() {
			return fontsize;
		}

		public Color getColor() {
			return color;
		}

		public void setColor(Color argColor) {
			color = argColor;
		}

		public override string getName() {
			return base.getName() + fontsize + font + color.ToString();
		}
	}
}
