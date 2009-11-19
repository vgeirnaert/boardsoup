using System;
using System.Collections.Generic;
using System.Text;
using BoardSoupEngine.Interface;
using System.Drawing;

namespace SecWars.Core
{
    class Title : TextActor
    {

        public Title(int x, int y, String text) : base (x, y, text)
        {
            receivesInput(false);
            //this.setFont("Calibri");
            //this.setFontColor(Color.Green);
            //this.setFontSize(32);
        }

        public override void onClick()
        {
            
        }

        public override void onMouseIn()
        {
            
        }

        public override void onMouseOut()
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
