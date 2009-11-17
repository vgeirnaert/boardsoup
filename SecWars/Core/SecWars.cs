using System;
using BoardSoup.Interface;
using System.Windows.Forms;

namespace SecWars.Core
{
    public class SecWars : IBoardGame
    {
        private const String name = "Sec Wars";
        private const String description = "Battle for domination in a hexagonal world gone mad!";
        private const String version = "0.0.1";
        private BoardSoupEngine.Interface.BoardSoupEngine myEngine;

        public SecWars()
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
            SecWarsGameLogic_old logic = new SecWarsGameLogic_old();

            while (!logic.ended)
            {
                myEngine.tick();
            }
        }
    }
}
