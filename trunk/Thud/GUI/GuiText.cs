using System;
using System.Collections.Generic;
using System.Text;
using BoardSoupEngine.Interface;

namespace Thud.GUI
{
    class GuiText : TextActor
    {
        public GuiText(int x, int y) : base(x, y, "")
        {
            receivesInput(false);
            this.setFontSize(45);
        }

        public override void onMouseIn()
        {
        }

        public override void onMouseOut()
        {
        }

        public override void onClick()
        {
        }

        public override void onEngineObjectCreated()
        {
        }

        public override void onTick()
        {
        }
    }
}
