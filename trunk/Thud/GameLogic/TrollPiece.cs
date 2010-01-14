using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thud.GameLogic
{
    class TrollPiece : PawnPiece
    {
        public TrollPiece(int x, int y)
            : base(x, y, "D:\\C#\\BoardSoup\\Thud\\Images\\troll.png")
        {

        }

        public override void onMouseIn()
        {
            setHighlights(true);
        }

        public override void onMouseOut()
        {
            setHighlights(false);
        }

        private void setHighlights(bool on)
        {
            // early out
            if (logic.getTurn() == TURN.DWARF)
                return;

            foreach (NEIGHBOUR n in Enum.GetValues(typeof(NEIGHBOUR)))
            {
                if (square.hasNeighbour(n))
                {
                    if (on)
                        ((BoardPiece)square.getNeighbour(n)).highlight(true);
                    else
                        ((BoardPiece)square.getNeighbour(n)).resetImage();
                }
            }
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

        public override bool isLegalMove(BoardPiece argPiece)
        {
            if (logic.getTurn() == TURN.TROLL)
            {
                if (square.isNeighbour(argPiece))
                    return true;
                else
                    return isLegalShove(argPiece);
            }

            return false;
        }

        public override bool isLegalAttack(BoardPiece argPiece)
        {
            if (hasMoved())
            {
                if (argPiece.isOccupied())
                {
                    if (argPiece.getOccupant() is DwarfPiece)
                    {
                        if(square.isNeighbour(argPiece))
                            return true;
                    }
                }
            }

            return false;
        }

        private bool isLegalShove(BoardPiece argPiece)
        {
            // if our target location is occupied
            if (!argPiece.isOccupied())
            {
                // and it is in a line with our location
                if (isInLine(square.getLocation(), argPiece.getLocation()))
                {
                    int distance = this.getDistance(square.getLocation(), argPiece.getLocation());
                    NEIGHBOUR direction = ThudPiece.getOppositeNeighbour(this.getDirection(square.getLocation(), argPiece.getLocation()));

                    // iterate over the squares in the direction opposite of our destination
                    // equal to the distance we wish to move
                    // if all squares contain a troll, the shove is legal

                    BoardPiece bp = square;
                    for (int i = 0; i < distance; i++)
                    {
                        if (bp.isOccupied())
                        {
                            if (bp.getOccupant() is TrollPiece)
                            {
                                if (bp.hasNeighbour(direction))
                                    bp = (BoardPiece)bp.getNeighbour(direction);
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }

                    return true;
                }
            }


            return false;
        }
    }
}
