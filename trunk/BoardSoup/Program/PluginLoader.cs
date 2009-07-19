using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoardSoup.Interface;
using System.Reflection;
using System.IO;

namespace BoardSoup
{
    class PluginLoader
    {
        public PluginLoader()
        {
        }

        /**
         * 
         */
        public bool loadPlugins(String argDirectory, out Dictionary<String, IBoardGame> argCollection)
        {
            
            Dictionary<String, IBoardGame> plugins = new Dictionary<String, IBoardGame>();
            bool success = true;

            Logger.log("Loading plugins...", LEVEL.DEBUG);
            
            try
            {
                List<String> paths = getAllPlugins();

                if(paths.Count == 0)
                    Logger.log("Warning: no plugins found", LEVEL.WARNING);
            
                foreach (String path in paths)
                {
                    IBoardGame game = loadGameFromDll(path);

                    if (game != null)
                    {
                        plugins.Add(game.getName(), game);
                        Logger.log("Loaded: " + game.getName() + " [game version " + game.getGameVersion() + ", engine version " + game.getEngineVersion() + "]", LEVEL.DEBUG);
                    }
                }
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                Logger.log("Error: plugin directory not found", LEVEL.ERROR);
                success = false;
            }

            argCollection = plugins;
            return success;
        }

        /**
         */
        private IBoardGame loadGameFromDll(String path)
        {
            IBoardGame myGame = null;

            Assembly myAs = Assembly.LoadFrom(path);
            
            foreach (Type t in myAs.GetTypes())
            {
                if (t.IsClass && !t.IsAbstract)
                {
                    object obj = myAs.CreateInstance(t.FullName);
                    if (obj is IBoardGame)
                    {
                        Logger.log("Loading: " + t.FullName, LEVEL.DEBUG);
                        myGame = (IBoardGame)obj;
                    }
                }
            }

            return myGame;
        }

        /**
         */
        private List<String> getAllPlugins()
        {
            // list of paths to all dll files
            List<String> plugins = new List<String>();

            // we're checking the /plugin/ directory
            try 
            {
                DirectoryInfo directory = new DirectoryInfo("plugins");

                // we're only looking for dll files
                FileInfo[] files = directory.GetFiles("*.dll");

                // for all files in our list, add the full path to the list
                foreach (FileInfo file in files)
                    plugins.Add(file.FullName); 
            } 
            catch(System.IO.DirectoryNotFoundException) 
            {
                 throw new System.IO.DirectoryNotFoundException();
            }

            return plugins;
        }
    }
}
