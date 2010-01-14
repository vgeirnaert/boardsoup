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

        public GuiButton(int x, int y) : base(x, y, "D:\\C#\\BoardSoup\\Thud\\Images\\stop.png")
        {
        }

        public override void onMouseIn()
        {
        }

        public override void onMouseOut()
        {
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
