using System;
using BoardSoup.Interface;
using System.Windows.Forms;

namespace TicTacToe.Core
{
    public class TicTacToe : IBoardGame
    {
        private const String name = "Tic Tac Toe";
        private const String description = "The most difficult game ever invented by man.";
        private const String version = "0.0.2";
        private BoardSoupEngine.Interface.BoardSoupEngine myEngine;

        public TicTacToe()
        {
            myEngine = BoardSoupEngine.Interface.BoardSoupEngine.getInstance();
        }

        public String getName()
        {
            return name;
        }

        public String getDescription()
        {
            return description;
        }

        public String getEngineVersion()
        {
            return myEngine.getVersion();
        }

        public String getGameVersion()
        {
            return version;
        }

        public void setRenderSurface(Panel renderPanel)
        {
            myEngine.setRenderSurface(renderPanel);
        }

        public void gameLoop()
        {
            TicTacToeLogic logic = new TicTacToeLogic();

            while (logic.running)
            {
                myEngine.tick();
            }
        }
    }
}
