using System;
using System.Collections.Generic;
using BoardSoupEngine.Interface;
using System.Threading;

namespace SecWars.Core
{
    class MenuBoard : BoardObject
    {
        private int state = -1;
        private SecWarsGameLogic_old logic;

        public MenuBoard(String argName, int won) : base(argName)
        {
            state = won;
            onEngineObjectCreated();
        }

        public override void onBoardChanged()
        {
            
        }

        public override void onEngineObjectCreated()
        {
            clearBoard();

            switch (state)
            {
                case 0:
                    makeWinTile();
                    break;
                case 1:
                    makeLoseTile();
                    break;
            }

            makeMenu();
        }

        public override void onTick()
        {
            
        }

        public void setLogic(SecWarsGameLogic_old l)
        {
            logic = l;
        }

        private void makeWinTile()
        {
            MenuTile m = new WinTile(450, 225, this);
            this.addActor(m);
        }

        private void makeLoseTile()
        {
            MenuTile m = new FailTile(450, 225, this);
            this.addActor(m);
        }

        private void makeMenu()
        {
            this.addActor(new Title(100, 100, "Sec Wars!"));

            if(state < 0)
                this.addActor(new StartTile(350, 400, this));
            else
                this.addActor(new AgainTile(350, 400, this));

            this.addActor(new StopTile(550, 400, this));
        }

        public void startGame()
        {
            logic.startGame();
        }

        public void exit()
        {
            clearBoard();
            Thread.Sleep(50);
            logic.exit();
        }
    }
}
