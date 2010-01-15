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
        private int round;

        private PlayerController dwarves;
        private PlayerController trolls;
        private PHASE currentPhase;

        public ThudLogic()
        {
            round = 0;
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
            board = new ThudBoard("thud", this);

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
            board.resetBoard();
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
            round++;
            PlayerController temp = dwarves;
            dwarves = trolls;
            trolls = dwarves;

            dwarves.setSide(TURN.TROLL);
            trolls.setSide(TURN.DWARF);

            dwarves.addNewScore();
            trolls.addNewScore();
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

            if (selectedPawn != null)
            {
                switch (this.getTurn())
                {
                    case TURN.DWARF:
                        if (argPiece.isOccupied())
                        {
                            // hurl
                            if (selectedPawn.isLegalAttack(argPiece))
                            {
                                board.removePiece(argPiece.getBoardPosition());
                                movePawn(selectedPawn, argPiece);
                                nextTurn();
                            }
                        }
                        else
                        {
                            // move
                            if (selectedPawn.isLegalMove(argPiece))
                            {
                                movePawn(selectedPawn, argPiece);
                                nextTurn();
                            }
                        }
                        break;
                    case TURN.TROLL:
                        switch (currentPhase)
                        {
                            case PHASE.MOVE:
                                if (selectedPawn.isLegalMove(argPiece))
                                {
                                    movePawn(selectedPawn, argPiece);
                                    currentPhase = PHASE.ATTACK;

                                    if (!selectedPawn.hasMovesLeft(currentPhase))
                                        nextTurn();
                                }
                                break;
                            case PHASE.ATTACK:
                                if (selectedPawn.isLegalAttack(argPiece))
                                    board.removePiece(argPiece.getBoardPosition());

                                if (!selectedPawn.hasMovesLeft(currentPhase))
                                    nextTurn();
                                break;
                        }
                        break;
                }
            }

            evaluate();
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

        public void evaluate()
        {
            if (dwarves.getScore() == 0 || trolls.getScore() == 0)
            {
                if (dwarves.getScore() > trolls.getScore())
                    board.displayWin(TURN.DWARF);
                else
                    board.displayWin(TURN.TROLL);

                endRound();
            }

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

            board.updateTurnButton();
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

        public void pawnRemoved(PawnPiece p)
        {
            if (p is DwarfPiece)
            {
                dwarves.decrementScore();
                board.updateScore(TURN.DWARF, dwarves.getScore());

            }
            else
            {
                trolls.decrementScore();
                trolls.decrementScore();
                trolls.decrementScore();
                trolls.decrementScore();
                board.updateScore(TURN.TROLL, trolls.getScore());
            }
        }
    }
}
