using BoardSoupEngine.Kernel;
using System.Collections.Generic;
using System;
using BoardSoupEngine.Utilities;

namespace BoardSoupEngine.Scene
{
    internal class SceneManager : IEventListener, ITickable
    {
        private IEventDispatcher dispatcher;
        private Dictionary<String, Board> boards;
        private String selectedBoard;

        public SceneManager()
        {
            selectedBoard = "";
            boards = new Dictionary<String, Board>();
        }

        public void setEventDispatcher(IEventDispatcher argDispatcher)
        {
            dispatcher = argDispatcher;

            //-----------------------------
            //selectedBoard = "test";
            //boards.Add(selectedBoard, new Board(dispatcher));
            
        }

        public void receiveEvent(Event argEvent)
        {
            argEvent.execute(this);
        }

        public void onTick()
        {
            foreach (String key in boards.Keys)
            {
                Board b;
                boards.TryGetValue(key, out b);
                b.onTick();
            }
        }

        public void renderActiveBoard()
        {
            Board b;

            boards.TryGetValue(selectedBoard, out b);

            if (b != null)
            {
                dispatcher.submitEvent(new Renderer.RendererStartSceneEvent());
                b.render();
                dispatcher.submitEvent(new Renderer.RendererEndSceneEvent());
            }
        }

        public Board createBoard(String argName)
        {
            Board r = null;

            if(dispatcher != null)
            {
                r =  new Board(dispatcher);
                boards.Add(argName, r);
                selectedBoard = argName;
            }
            else
                Logger.log("SceneManager: Warning - board (" + argName + ") creation failed, no dispatcher set", LEVEL.WARNING);

            return r;
        }

        public BoardActor createActor(int x, int y, String filename)
        {

            BoardActor r = null;

            if (dispatcher != null)
            {
                r = new BoardActor(x, y, filename);
                r.loadAsset(filename, dispatcher);
            }
            else
                Logger.log("SceneManager: Warning - board actor creation failed, no dispatcher set", LEVEL.WARNING);

            return r;
        }

        public Board selectBoard(String argName)
        {
            if (boards.ContainsKey(argName))
                selectedBoard = argName;
            
            return null;
        }

        /*public KeyCollection getAllBoardNames()
        {
            return boards.Keys;
        }*/

    }
}
