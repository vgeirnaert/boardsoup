using System;
using System.Collections.Generic;
using System.Text;

namespace Thud.GameLogic
{
    class ThudLogic
    {
        private bool ended;
        private ThudBoard board;
        private PawnPiece selectedPawn;

        public ThudLogic()
        {
            ended = false;
            //createMenu();
            startGame();
        }

        public void createMenu()
        {
            
        }

        public void startGame()
        {
            selectedPawn = null;
            board = new ThudBoard("thud", this);
        }

        public void startRound()
        {
        }

        public void endRound()
        {
        }

        public void endGame()
        {
            ended = true;
        }

        public bool isEnded()
        {
            return ended;
        }

        public void pawnSelected(PawnPiece argPiece)
        {
            selectedPawn = argPiece;
        }

        public void squareSelected(EmptyPiece argPiece)
        {
            if (selectedPawn != null && !argPiece.isOccupied())
            {
                if (selectedPawn.isLegalMove(argPiece))
                {
                    argPiece.setOccupant(selectedPawn);
                    unselectPawn();
                }
            }
        }

        public void unselectPawn()
        {
            selectedPawn = null;
        }
    }
}
