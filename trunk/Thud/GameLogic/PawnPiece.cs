using System;
using System.Collections.Generic;
using System.Text;
using BoardSoupEngine.Interface;
using System.Drawing;

namespace Thud.GameLogic
{
    abstract class PawnPiece : ImageActor
    {
        protected ThudLogic logic;
        protected BoardPiece square;
        private bool wasMoved;

        public PawnPiece(int x, int y, string filename) : base(x, y, filename)
        {
            wasMoved = false;
        }

        public void setLogic(ThudLogic argLogic)
        {
            logic = argLogic;
        }

        public void setSquare(BoardPiece argPiece)
        {
            if(square != null)
                square.setOccupant(null);

            square = argPiece;

            if (square != null)
                this.setLocation(square.getLocation());
        }

        public BoardPiece getSquare()
        {
            return square;
        }

        protected bool isInLine(Point p1, Point p2)
        {
            int xdiff = Math.Abs(p1.X - p2.X);
            int ydiff = Math.Abs(p1.Y - p2.Y);

            // in line vertically, horizontally or diagonally
            if (xdiff == 0 || ydiff == 0 || xdiff == ydiff)
                return true;

            return false;
        }

        protected GameLogic.NEIGHBOUR getDirection(Point argLocation, Point argDestination)
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
            Console.WriteLine("Error: PawnPiece::getDirection - " + argLocation.ToString() + " & " + argDestination.ToString());
            return NEIGHBOUR.NORTH;
        }

        protected int getDistance(Point argLocation, Point argDestination)
        {
            if (isInLine(argLocation, argDestination))
            {
                //vertical
                if (argLocation.X == argDestination.X)
                    return Math.Abs(argLocation.Y - argDestination.Y);
                else //horizontal and diagonal
                    return Math.Abs(argLocation.X - argDestination.X);
            }

            // unable to get a proper distance
            Console.WriteLine("Error: PawnPiece::getDistance - " + argLocation.ToString() + " & " + argDestination.ToString());
            return -1;
        }

        public void move()
        {
            wasMoved = true;
        }

        public void finish()
        {
            wasMoved = false;
        }

        public bool hasMoved()
        {
            return wasMoved;
        }

        abstract public bool isLegalMove(BoardPiece argPiece);
        abstract public bool isLegalAttack(BoardPiece argPiece);
        abstract public bool hasMovesLeft(PHASE argPhase);
    }
}
