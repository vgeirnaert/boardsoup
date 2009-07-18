using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoardSoup.Interface;

namespace BoardSoup
{
    class GamePool
    {
        bool loadingSuccess = false;

        // our plugin loader
        PluginLoader pLoader;

        // our collection of games
        Dictionary<String, IBoardGame> gameCollection;

        public GamePool()
        {
            pLoader = new PluginLoader();
            loadingSuccess = pLoader.loadPlugins("games", out gameCollection);
        }

        /**
         */
        public bool pluginsLoaded()
        {
            return loadingSuccess;
        }

        /**
         */
        public bool startGame(String argName)
        {
            IBoardGame bg;
            gameCollection.TryGetValue(argName, out bg);

            if (bg != null)
            {
                bg.startGame();
                return true;
            }

            return false;
        }

        /**
         */
        public List<String> getGameNames()
        {
            List<String> names = new List<String>();

            foreach(String name in gameCollection.Keys)
                names.Add(name);

            return names;
        }
    }
}
