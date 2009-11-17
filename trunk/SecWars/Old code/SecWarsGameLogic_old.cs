using System;
using System.Collections.Generic;
using System.Text;
using BoardSoupEngine.Interface;

namespace SecWars.Core
{
    class SecWarsGameLogic_old
    {
        public bool ended = false;

        public SecWarsGameLogic_old()
        {
            MenuBoard menu = new MenuBoard("win board", -1);
            menu.setLogic(this);
        }

        public void evaluateGame(BoardObject o)
        {
            if(isFinished(o))
                endGame(o);
        }

        public void endGame(BoardObject o)
        {
            MenuBoard menu;
            if (isFinished(o))
                menu = new MenuBoard("win board", 0);
            else
                menu = new MenuBoard("win board", 1);

            menu.setLogic(this);
        }

        public void startGame()
        {
            SecWarsBoard_old swb = new SecWarsBoard_old("test board");
            swb.setLogic(this);
        }

        public void exit()
        {
            ended = true;   
        }

        public bool isFinished(BoardObject o)
        {
            bool complete = true;

            foreach (SecWarsTile_old t in o.getAllBoardActors())
            {
                if (!(t is SecWarsActiveTile))
                    complete = false;
            }

            return complete;
        }
    }
}
