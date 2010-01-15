using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thud.GameLogic
{
    abstract class PlayerController
    {
        private int score = 32;
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

        public void decrementScore()
        {
            score--;
        }

        public void resetScore()
        {
            score = 32;
        }

        public void addNewScore()
        {
            score = score + 32;
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
