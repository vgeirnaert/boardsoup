using System;
using System.Collections.Generic;
using System.Text;
using BoardSoupEngine.Interface;

namespace Thud.GameLogic
{
    abstract class PawnPiece : ImageActor
    {
        protected ThudLogic logic;
        protected EmptyPiece square;

        public PawnPiece(int x, int y, string filename) : base(x, y, filename)
        {
            this.receivesInput(false);
        }

        public void setLogic(ThudLogic argLogic)
        {
            logic = argLogic;
        }

        public void setSquare(EmptyPiece argPiece)
        {
            if(square != null)
                square.setOccupant(null);

            square = argPiece;

            if (square != null)
                this.setLocation(square.getLocation());
        }

        abstract public bool isLegalMove(EmptyPiece argPiece);
    }
}
