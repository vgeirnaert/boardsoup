using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thud.GameLogic
{
    class TrollPiece : PawnPiece
    {
        public TrollPiece(int x, int y) : base(x, y, "D:\\C#\\BoardSoup\\Thud\\Images\\troll.png")
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
            logic.pawnSelected(this);
        }

        public override void onEngineObjectCreated()
        {
        }

        public override void onTick()
        {
        }

        public override bool isLegalMove(EmptyPiece argPiece)
        {
            return square.isNeighbour(argPiece);
        }
    }
}
