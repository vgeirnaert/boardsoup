using System;
using System.Collections.Generic;
using System.Text;
using BoardSoupEngine.Interface;

namespace Thud.GUI
{
    class GuiButton : ImageActor
    {
        public delegate void ClickEventHandler();
        public event ClickEventHandler OnClickEvent;
        private String image;

        public GuiButton(int x, int y) : base(x, y, "Images\\stop.png")
        {
            image = "Images\\stop.png";
        }

        public override void setImage(String filename)
        {
            base.setImage(filename);
            image = filename;
        }

        public override void onMouseIn()
        {
            this.setImage(image.Replace(".png", "_a.png"));
        }

        public override void onMouseOut()
        {
            this.setImage(image.Replace("_a.png", ".png"));
        }

        public override void onClick()
        {
            OnClickEvent();
        }

        public override void onEngineObjectCreated()
        {
        }

        public override void onTick()
        {
        }
    }
}
