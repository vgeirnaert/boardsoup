using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thud.GameLogic
{
    class AIController : PlayerController
    {
        public AIController() 
        {
        }

        public override void doMove(ThudBoard board)
        {
            LinkedList<PawnPiece> pieces = board.getPieces(this.getSide());
            
        }
    }
}
