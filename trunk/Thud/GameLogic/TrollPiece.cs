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
            // Shove: anywhere there is a straight (orthogonal or diagonal) line of adjacent trolls on the board, 
            // they may shove the endmost troll in the direction continuing the line, up to as many spaces as there 
            // are trolls in the line. 
            // As in a normal move, the troll may not land on an occupied square, and any dwarfs in the eight squares 
            // adjacent to its final position may immediately be captured. 
            // Trolls may only make a shove if by doing so they capture at least one dwarf.

            // if our target location is not occupied
            if (!argPiece.isOccupied())
            {
                // and it is in a line with our location
                if (isInLine(square.getLocation(), argPiece.getLocation()))
                {
                    // if this location neighbours any dwarves
                    if (neighboursDwarves(argPiece))
                    {
                        int distance = this.getDistance(square.getBoardPosition(), argPiece.getBoardPosition());
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
            }
            return false;
        }

        private bool neighboursDwarves(BoardPiece argPiece)
        {
            foreach (NEIGHBOUR n in Enum.GetValues(typeof(NEIGHBOUR)))
            {
                if (argPiece.hasNeighbour(n))
                {
                    BoardPiece p = (BoardPiece)argPiece.getNeighbour(n);

                    if (p.isOccupied())
                    {
                        if (p.getOccupant() is DwarfPiece)
                            return true;
                    }   
                }
            }
            return false;
        }
    }
}
