using System;
using BoardSoup.Interface;

namespace TicTacToe.Core
{
    public class TicTacToe : IBoardGame
    {
        private const String name = "Tic Tac Toe";
        private const String description = "The most difficult game ever invented by man.";
        private const String version = "0.0.1";
        private BoardSoupEngine.Interface.BoardSoupEngine myEngine;

        public TicTacToe()
        {
            myEngine = new BoardSoupEngine.Interface.BoardSoupEngine();
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

        public bool startGame()
        {
            return true;
        }
    }
}
