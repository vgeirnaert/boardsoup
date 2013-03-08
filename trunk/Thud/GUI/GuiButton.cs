using System;
using System.Collections.Generic;
using System.Text;
using BoardSoupEngine.Interface;

namespace Thud.GUI {
	class GuiButton : ImageActor {
		public delegate void ClickEventHandler();
		public event ClickEventHandler OnClickEvent;
		private String image;
		private String imageActive;

		public GuiButton(int x, int y) : base(x, y, "Images\\stop.png") {
			image = "Images\\stop.png";
			imageActive = "Images\\stop_a.png";
		}

		public override void setImage(String filename) {
			image = filename;
			base.setImage(image);
		}

		public void setImageActive(String filename) {
			this.imageActive = filename;
		}

		public override void onMouseIn() {
			base.setImage(imageActive);
		}

		public override void onMouseOut() {
			base.setImage(image);
		}

		public override void onClick() {
			OnClickEvent();
		}

		public override void onEngineObjectCreated() {
		}

		public override void onTick() {
		}
	}
}
