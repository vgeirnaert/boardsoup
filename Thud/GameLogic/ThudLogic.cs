using System;
using System.Collections.Generic;
using System.Text;

namespace Thud.GameLogic
{
    enum TURN { DWARF, TROLL };
    enum PLAYER { HUMAN, AI };
    enum PHASE { MOVE, ATTACK };

    class ThudLogic
    {
        private bool ended;
        private ThudBoard board;
        private PawnPiece selectedPawn;
        private int turn;

        private PlayerController dwarves;
        private PlayerController trolls;
        private PHASE currentPhase;

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

            dwarves.setSide(TURN.DWARF);
            trolls.setSide(TURN.TROLL);

            startRound();
        }

        public void startRound()
        {
            board = new ThudBoard("thud", this);
            turn = 0;
            selectedPawn = null;
            startTurn();
        }

        private void startTurn()
        {
            currentPhase = PHASE.MOVE;
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

        private void selectPawn(PawnPiece argPawn)
        {
            // if we have selected a 
            if (argPawn.GetType() == getActiveController().getPawnType())
                selectedPawn = argPawn;
        }

        public void squareSelected(BoardPiece argPiece)
        {
            if (argPiece.isOccupied() && currentPhase != PHASE.ATTACK)
                selectPawn(argPiece.getOccupant());

            switch (currentPhase)
            {
                case PHASE.MOVE:
                    if (selectedPawn != null)
                    {
                        if (selectedPawn.isLegalMove(argPiece))
                            movePawn(selectedPawn, argPiece);
                    }
                    break;
                case PHASE.ATTACK:
                    break;
            }
        }

        private void movePawn(PawnPiece pawn, BoardPiece piece)
        {
            piece.setOccupant(pawn);
            pawn.move();
            currentPhase = PHASE.ATTACK;
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

            return TURN.TROLL;
        }

        public void nextTurn()
        {
            if(selectedPawn != null)
                selectedPawn.finish();

            unselectPawn();
            turn++;

            startTurn();

            if (getTurn() == TURN.DWARF)
                dwarves.doMove(board);
            else
                trolls.doMove(board);
        }

        private PlayerController getActiveController()
        {
            if (turn % 2 == 0)
                return dwarves;

            return trolls;
        }
    }
}
