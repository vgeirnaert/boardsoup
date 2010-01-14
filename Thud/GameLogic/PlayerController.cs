using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thud.GameLogic
{
    abstract class PlayerController
    {
        private int score = 0;

        public int getScore()
        {
            return score;
        }

        public void incrementScore()
        {
            score++;
        }

        abstract public void doMove(ThudBoard board);
    }
}
