﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoardSoup.Interface;
using System.Reflection;
using System.IO;
using BoardSoupEngine.Utilities;

namespace BoardSoup.Program
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
            // make the collection class we want to set as our out parameter
            Dictionary<String, IBoardGame> plugins = new Dictionary<String, IBoardGame>();
            bool success = true;

            // log it
            Logger.log("PluginLoader: Loading plugins...", LEVEL.DEBUG);
            
            try
            {
                // try to obtain a list of all plugins, this may throw an exception
                List<String> paths = getAllPlugins(argDirectory);

                // if there are no plugins present...
                if(paths.Count == 0)
                    Logger.log("PluginLoader: Warning - no plugins found", LEVEL.WARNING);
            
                // for all plugin paths in the list...
                foreach (String path in paths)
                {
                    // load the game from the path
                    IBoardGame game = loadGameFromDll(path);

                    // check we didnt obtain a null pointer
                    if (game != null)
                    {
                        // add the plugin to our collection
                        plugins.Add(game.getName(), game);

                        // log it
                        Logger.log("PluginLoader: Loaded: " + game.getName() + " [game version " + game.getGameVersion() + ", engine version " + game.getEngineVersion() + "]", LEVEL.DEBUG);
                    }
                }
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                // we cannot find a plugins directory
                // log it and return false
                Logger.log("PluginLoader: Error - plugin directory not found", LEVEL.ERROR);
                success = false;
            }

            // assing the out parameter
            argCollection = plugins;

            return success;
        }

        /**
         */
        private IBoardGame loadGameFromDll(String path)
        {
            IBoardGame myGame = null;

            // load the assembly
            Assembly myAs = Assembly.LoadFrom(path);
            
            // for all the types in this assembly
            foreach (Type t in myAs.GetTypes())
            {
                // make sure the type is a non-abstract class, visible and implements our interface
                if (t.IsClass && !t.IsAbstract && t.IsVisible && (t.GetInterface("BoardSoup.Interface.IBoardGame", true) == typeof(IBoardGame)) )
                {
                    // instantiate the class
                    try
                    {
                        object obj = myAs.CreateInstance(t.FullName);

                        // if it is an implementation of our board game interface...
                        if (obj is IBoardGame)
                        {
                            // log debut output
                            Logger.log("PluginLoader: Loading: " + t.FullName + " from " + t.Module, LEVEL.DEBUG);

                            // cast our class to a board game
                            myGame = (IBoardGame)obj;
                        }
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("-------------------------------");
                        System.Console.WriteLine("Error loading " + t.FullName + " see: " + e.StackTrace);
                        System.Console.WriteLine("-------------------------------");
                    }
                }
            }

            return myGame;
        }

        /**
         */
        private List<String> getAllPlugins(String directoryName)
        {
            // list of paths to all dll files
            List<String> plugins = new List<String>();

            // we're checking the /plugin/ directory
            try 
            {
                DirectoryInfo directory = new DirectoryInfo(directoryName);

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
