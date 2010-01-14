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
            Point myLocation = square.getBoardPosition();
            Point destination = argPiece.getBoardPosition();

            // early out
            if (!isInLine(myLocation, destination))
                return false;

            GameLogic.NEIGHBOUR direction = getDirection(myLocation, destination);

            EmptyPiece ep = (EmptyPiece)square.getNeighbour(direction);

            while (!ep.isOccupied())
            {
                if (ep == argPiece)
                    return true;

                if(ep.hasNeighbour(direction))
                    ep = (EmptyPiece)ep.getNeighbour(direction);
            }

            return false;   
        }

        private bool isInLine(Point p1, Point p2)
        {
            int xdiff = Math.Abs(p1.X - p2.X);
            int ydiff = Math.Abs(p1.Y - p2.Y);

            // in line vertically, horizontally or diagonally
            if (xdiff == 0 || ydiff == 0 || xdiff == ydiff)
                return true;

            return false;
        }

        private GameLogic.NEIGHBOUR getDirection(Point argLocation, Point argDestination)
        {
            int xdiff = Math.Abs(argLocation.X - argDestination.X);
            int ydiff = Math.Abs(argLocation.Y - argDestination.Y);

            // in line vertically, horizontally or diagonally
            // early out
            if (xdiff == 0 || ydiff == 0 || xdiff == ydiff)
            {
                // vertical
                if (xdiff == 0)
                {
                    if (argLocation.Y > argDestination.Y)
                        return NEIGHBOUR.NORTH;
                    else
                        return NEIGHBOUR.SOUTH;
                }
                else if (ydiff == 0) // horizontal
                {
                    if (argLocation.X > argDestination.X)
                        return NEIGHBOUR.WEST;
                    else
                        return NEIGHBOUR.EAST;
                }
                else // diagonal
                {
                    bool west = false;
                    bool north = false;
                    // are we moving westward or eastward?
                    if (argLocation.X > argDestination.X)
                        west = true;

                    if (argLocation.Y > argDestination.Y)
                        north = true;

                    if (west && north)
                        return NEIGHBOUR.NORTHWEST;
                    else if (west && !north)
                        return NEIGHBOUR.SOUTHWEST;
                    else if (!west && north)
                        return NEIGHBOUR.NORTHEAST;
                    else
                        return NEIGHBOUR.SOUTHEAST;
                }
            }

            // we should not come here!
            Console.WriteLine("Error: DwarfPiece::getDirection - " + argLocation.ToString() + " & " + argDestination.ToString());
            return NEIGHBOUR.NORTH;
        }
    }
}
