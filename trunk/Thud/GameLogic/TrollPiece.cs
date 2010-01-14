using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thud.GameLogic
{
    class TrollPiece : PawnPiece
    {
        public TrollPiece(int x, int y) : base(x, y, "D:\\C#\\BoardSoup\\Thud\\Images\\troll.png")
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
            foreach (NEIGHBOUR n in Enum.GetValues(typeof(NEIGHBOUR)))
            {
                if (square.hasNeighbour(n))
                {
                    if (on)
                        ((EmptyPiece)square.getNeighbour(n)).highlight(true);
                    else
                        ((EmptyPiece)square.getNeighbour(n)).resetImage();
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

        public override bool isLegalMove(EmptyPiece argPiece)
        {
            return square.isNeighbour(argPiece);
        }
    }
}
