using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thud.GameLogic
{
    abstract class PlayerController
    {
        private int score = 32;
        private SIDE side;

        public void setSide(SIDE argSide)
        {
            side = argSide;
        }

        public SIDE getSide()
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
            if (side == SIDE.DWARF)
                return typeof(DwarfPiece);

            return typeof(TrollPiece);
        }

        abstract public void doMove(ThudBoard board);
    }
}
