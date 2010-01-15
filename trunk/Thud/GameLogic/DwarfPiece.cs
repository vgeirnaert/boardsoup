using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Thud.GameLogic
{
    class DwarfPiece : PawnPiece
    {
        public DwarfPiece(int x, int y) : base(x, y, "D:\\C#\\BoardSoup\\Thud\\Images\\dwarf.png")
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
            if (logic.getTurn() == TURN.TROLL)
                return;


            foreach (NEIGHBOUR n in Enum.GetValues(typeof(NEIGHBOUR)))
            {
                if (square.hasNeighbour(n))
                {
                    BoardPiece ep = (BoardPiece)square.getNeighbour(n);

                    while (!ep.isOccupied())
                    {
                        if (on)
                            ep.highlight(false);
                        else
                            ep.resetImage();

                        if (ep.hasNeighbour(n))
                            ep = (BoardPiece)ep.getNeighbour(n);
                        else
                            break;
                    }
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
            // early out
            if (logic.getTurn() == TURN.TROLL)
                return false;

            Point myLocation = square.getBoardPosition();
            Point destination = argPiece.getBoardPosition();

            // early out
            if (!isInLine(myLocation, destination))
                return false;

            GameLogic.NEIGHBOUR direction = getDirection(myLocation, destination);

            BoardPiece ep = (BoardPiece)square.getNeighbour(direction);

            // check all squares in the direction of our destination
            while (!ep.isOccupied())
            {
                if (ep == argPiece)
                    return true;

                if (ep.hasNeighbour(direction))
                    ep = (BoardPiece)ep.getNeighbour(direction);
                else
                    break;
            }

            return false;   
        }

        public override bool isLegalAttack(BoardPiece argPiece)
        {
            return isLegalHurl(argPiece);
        }

        private bool isLegalHurl(BoardPiece argPiece)
        {
            // if our target location is occupied
            if (argPiece.isOccupied())
            {
                // ... by a troll
                if (argPiece.getOccupant() is TrollPiece)
                {
                    // and it is in a line with our location
                    if (isInLine(square.getLocation(), argPiece.getLocation()))
                    {

                        int distance = this.getDistance(square.getBoardPosition(), argPiece.getBoardPosition());
                        NEIGHBOUR direction = ThudPiece.getOppositeNeighbour(this.getDirection(square.getLocation(), argPiece.getLocation()));

                        // iterate over the squares in the direction opposite of our destination
                        // equal to the distance we wish to move
                        // if all squares contain a dwarf, the hurl is legal

                        BoardPiece bp = square;
                        for (int i = 0; i < distance; i++)
                        {
                            if (bp.isOccupied())
                            {
                                if (bp.getOccupant() is DwarfPiece)
                                {
                                    if (bp.hasNeighbour(direction))
                                        bp = (BoardPiece)bp.getNeighbour(direction);
                                    else if (i != distance - 1)
                                        return false;
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

        public override bool hasMovesLeft(PHASE argPhase)
        {
            foreach (NEIGHBOUR n in Enum.GetValues(typeof(NEIGHBOUR)))
            {
                if (square.hasNeighbour(n))
                {
                    if (!((BoardPiece)square.getNeighbour(n)).isOccupied())
                        return true;

                    if (((BoardPiece)square.getNeighbour(n)).isOccupied())
                    {
                        if (((BoardPiece)square.getNeighbour(n)).getOccupant() is TrollPiece)
                            return true;
                    }

                }
            }

            return false;
        }
    }
}
