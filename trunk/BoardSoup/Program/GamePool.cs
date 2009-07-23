using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoardSoup.Interface;

namespace BoardSoup.Program
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
        public IBoardGame getGame(String argName)
        {
            IBoardGame bg;
            gameCollection.TryGetValue(argName, out bg);

            // return the game - note: this could be null
            return bg;
        }

        /**
         */
        public List<String> getGameNames()
        {
            List<String> names = new List<String>();

            // for all names in our collection of games
            foreach(String name in gameCollection.Keys)
                names.Add(name); // add them to our list

            // return the list
            return names;
        }
    }
}
