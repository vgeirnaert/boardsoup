using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thud.GameLogic
{
    abstract class PlayerController
    {
        private int score = 0;
        private TURN side;

        public void setSide(TURN argSide)
        {
            side = argSide;
        }

        public TURN getSide()
        {
            return side;
        }

        public int getScore()
        {
            return score;
        }

        public void incrementScore()
        {
            score++;
        }

        public Type getPawnType()
        {
            if (side == TURN.DWARF)
                return typeof(DwarfPiece);

            return typeof(TrollPiece);
        }

        abstract public void doMove(ThudBoard board);
    }
}
