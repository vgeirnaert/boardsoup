using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thud.GameLogic
{
    class StonePiece : PawnPiece
    {
        public StonePiece(int x, int y) : base(x, y, "D:\\C#\\BoardSoup\\Thud\\Images\\rock.png")
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
        }

        public override void onEngineObjectCreated()
        {
        }

        public override void onTick()
        {
        }

        public override bool isLegalMove(EmptyPiece argPiece)
        {
            return false;
        }
    }
}
