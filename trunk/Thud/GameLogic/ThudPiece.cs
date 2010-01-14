using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoardSoupEngine.Interface;

namespace Thud.GameLogic
{
    enum NEIGHBOUR { NORTH, NORTHEAST, EAST, SOUTHEAST, SOUTH, SOUTHWEST, WEST, NORTHWEST };

    abstract class ThudPiece : ImageActor
    {
        protected Dictionary<NEIGHBOUR, ThudPiece> myNeighbours;

        public ThudPiece(int x, int y, String filename) : base(x, y, filename)
        {
            myNeighbours = new Dictionary<NEIGHBOUR, ThudPiece>();
        }

        public ThudPiece getNeighbour(NEIGHBOUR argNeighbour)
        {
            return myNeighbours[argNeighbour];
        }

        public bool hasNeighbour(NEIGHBOUR argNeighbour)
        {
            if (myNeighbours.ContainsKey(argNeighbour))
                if (myNeighbours[argNeighbour] != null)
                    return true;

            return false;
        }

        public void switchWith(ThudPiece argPiece, ThudBoard board)
        {
            foreach (NEIGHBOUR n in myNeighbours.Keys)
            {
                argPiece.setNeighbour(n, myNeighbours[n]);
            }

            board.addActor(argPiece);
            board.deleteFromBoard(this);
        }

        public void setNeighbour(NEIGHBOUR argNeighbour, ThudPiece argPiece)
        {
            this.setNeighbour(argNeighbour, argPiece, true);
        }

        public void setNeighbour(NEIGHBOUR argNeighbour, ThudPiece argPiece, bool recursive)
        {
            if (myNeighbours.ContainsKey(argNeighbour))
                myNeighbours[argNeighbour] = argPiece;
            else
                myNeighbours.Add(argNeighbour, argPiece);

            if (recursive)
                argPiece.setNeighbour(getOppositeNeighbour(argNeighbour), this, false);
        }

        public static NEIGHBOUR getOppositeNeighbour(NEIGHBOUR argNeighbour)
        {
            switch(argNeighbour)
            {
                case NEIGHBOUR.NORTH:
                    return NEIGHBOUR.SOUTH;
                case NEIGHBOUR.NORTHEAST:
                    return NEIGHBOUR.SOUTHWEST;
                case NEIGHBOUR.EAST:
                    return NEIGHBOUR.WEST;
                case NEIGHBOUR.SOUTHEAST:
                    return NEIGHBOUR.NORTHWEST;
                case NEIGHBOUR.SOUTH:
                    return NEIGHBOUR.NORTH;
                case NEIGHBOUR.SOUTHWEST:
                    return NEIGHBOUR.NORTHEAST;
                case NEIGHBOUR.WEST:
                    return NEIGHBOUR.EAST;
                case NEIGHBOUR.NORTHWEST:
                    return NEIGHBOUR.SOUTHEAST;
            }

            // this code should never be reached
            Console.WriteLine("Error: ThudPiece::getOppositeNeighbour - " + argNeighbour);
            return NEIGHBOUR.NORTH;
        }

        public override void onMouseIn()
        {
        }

        public override void onMouseOut()
        {
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
    }
}
