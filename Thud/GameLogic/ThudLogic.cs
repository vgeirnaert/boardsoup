using System;
using System.Collections.Generic;
using System.Text;

namespace Thud.GameLogic
{
    enum TURN { DWARF, TROLL };
    enum PLAYER { HUMAN, AI };

    class ThudLogic
    {
        private bool ended;
        private ThudBoard board;
        private PawnPiece selectedPawn;
        private int turn;

        private PlayerController dwarves;
        private PlayerController trolls;

        public ThudLogic()
        {
            turn = 0;
            ended = false;
            createMenu();
            startGame(PLAYER.HUMAN, PLAYER.AI);
        }

        public void createMenu()
        {
            
        }

        public void startGame(PLAYER argDwarves, PLAYER argTrolls)
        {
            if (argDwarves == PLAYER.HUMAN)
                dwarves = new HumanController();
            else
                dwarves = new AIController();

            if (argTrolls == PLAYER.HUMAN)
                trolls = new HumanController();
            else
                trolls = new AIController();

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
            // if we dont have a previous pawn selected or we select the same type, change the selection
            if (selectedPawn == null || selectedPawn.GetType() == argPiece.GetType())
                selectedPawn = argPiece;
            else // if we click on a different pawn...
            {
                Console.WriteLine("attack!");
                squareSelected(argPiece.getSquare());
                unselectPawn();
            }
        }

        public void squareSelected(BoardPiece argPiece)
        {
            if (selectedPawn != null && !argPiece.isOccupied())
            {
                if (selectedPawn.isLegalMove(argPiece))
                {
                    movePawn(selectedPawn, argPiece);
                }
                else if (selectedPawn.isLegalAttack(argPiece))
                {
                    movePawn(selectedPawn, argPiece);
                    nextTurn();
                }
            }
        }

        private void movePawn(PawnPiece pawn, BoardPiece piece)
        {
            piece.setOccupant(pawn);
            pawn.move();
        }

        private void removePawn(BoardPiece piece)
        {
        }

        public void unselectPawn()
        {
            selectedPawn = null;
        }

        public void evaluate(PawnPiece argPiece)
        {
        }

        public TURN getTurn()
        {
            if (turn % 2 == 0)
                return TURN.DWARF;

            else return TURN.TROLL;
        }

        private void nextTurn()
        {
            selectedPawn.finish();
            unselectPawn();
            turn++;

            if (getTurn() == TURN.DWARF)
                dwarves.doMove(board);
            else
                trolls.doMove(board);
        }
    }
}
