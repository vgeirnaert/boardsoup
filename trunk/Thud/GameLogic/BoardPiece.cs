using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Thud.GameLogic
{
    class BoardPiece : ThudPiece
    {
        private String image;
        private PawnPiece occupant;
        private ThudLogic logic;
        private Point boardPosition;
        
        public BoardPiece(int x, int y, bool even) : base(x, y, "Images\\empty.png")
        {
            boardPosition = new Point();
            image = "Images\\empty.png";

            if (even)
            {
                image = "Images\\emptylight.png";
                this.setImage(image);
            }
        }

        public override void onMouseIn()
        {
        }

        public override void onMouseOut()
        {
        }

        public override void onClick()
        {
            logic.squareSelected(this);
        }

        public override void onEngineObjectCreated()
        {
            
        }

        public override void onTick()
        {
        }

        public void resetImage()
        {
            this.setImage(image);
        }

        public void setOccupant(PawnPiece argPiece)
        {
            occupant = argPiece;

            if(occupant != null)
                occupant.setSquare(this);
        }

        public PawnPiece getOccupant()
        {
            return occupant;
        }

        public bool isOccupied()
        {
            if(occupant != null)
                return true;

            return false;
        }

        public void setLogic(ThudLogic argLogic)
        {
            logic = argLogic;
        }

        public bool isNeighbour(ThudPiece argPiece)
        {
            return myNeighbours.ContainsValue(argPiece);
        }

        public void setBoardPosition(int x, int y)
        {
            boardPosition.X = x;
            boardPosition.Y = y;
        }

        public Point getBoardPosition()
        {
            return boardPosition;
        }

        public void highlight(bool isRed)
        {
            if (isRed)
                this.setImage(image.Replace(".png", "red.png"));
            else
                this.setImage(image.Replace(".png", "blue.png"));
        }
    }
}
