using System;
using System.Text;
using System.Drawing;
using BoardSoupEngine.Assets;

namespace BoardSoupEngine.Renderer {
	class TextRenderer : Renderable {
		Font myFont;

		public TextRenderer() {
			myFont = null;
		}

		public override void render(Point location, int rotation) {
			if (this.getRenderer() != null && this.getAsset() != null) {
				if (this.getAsset() is TextAsset) {
					loadFont();
					this.getRenderer().drawText(((TextAsset)this.getAsset()).getText(), myFont, ((TextAsset)this.getAsset()).getColor(), location, rotation, ((TextAsset)this.getAsset()).getBounds());
				}
			}
		}

		protected override void onAssetAssigned() {
			if (this.getAsset() is TextAsset) {
				loadFont();
				((TextAsset)this.getAsset()).setBounds(this.getRenderer().getBoundsForString(((TextAsset)this.getAsset()).getText(), myFont));
			}
		}

		private void loadFont() {
			if (this.getAsset() is TextAsset) {
				String fontname = ((TextAsset)this.getAsset()).getFontName();
				int fontsize = this.getRenderer().sceneToScreenAmount(((TextAsset)this.getAsset()).getFontSize());

				// only make a new font object if its necessary
				if (myFont == null || (myFont.Name != fontname || myFont.Size != fontsize))
					myFont = new Font(fontname, fontsize);
			}

		}
	}
}
