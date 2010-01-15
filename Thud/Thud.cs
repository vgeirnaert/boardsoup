using System;
using BoardSoup.Interface;
using System.Windows.Forms;
using Thud.GameLogic;

namespace Thud
{
    public class Thud : IBoardGame
    {
        private const String name = "Thud";
        private const String description = "Dwarves and Trolls fight their eternal octagonal war";
        private const String version = "0.7.0";
        private BoardSoupEngine.Interface.BoardSoupEngine myEngine;

        public Thud()
        {
            myEngine = BoardSoupEngine.Interface.BoardSoupEngine.getInstance();
        }

        #region IBoardGame Members

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
            ThudLogic logic = new ThudLogic();

            while (!logic.isEnded())
            {
                myEngine.tick();
            }
        }

        #endregion
    }
}
