using BoardSoupEngine.Kernel;
using System.Collections.Generic;
using System;
using BoardSoupEngine.Utilities;
using System.Drawing;
using BoardSoupEngine.Assets;

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
            /*foreach (String key in boards.Keys)
            {
                Board b;
                boards.TryGetValue(key, out b);
                b.onTick();
            }*/

            Board b;
            boards.TryGetValue(selectedBoard, out b);

            if(b != null)
                b.onTick();
        }

        public void renderActiveBoard()
        {
            Board b;

            boards.TryGetValue(selectedBoard, out b);

            if (b != null)
            {
                dispatcher.submitEvent(EventFactory.createEvent("BoardSoupEngine.Renderer.RendererStartSceneEvent"));
                b.render();
                dispatcher.submitEvent(EventFactory.createEvent("BoardSoupEngine.Renderer.RendererEndSceneEvent"));
            }
        }

        public Board createBoard(String argName)
        {
            Board r = null;

            if(dispatcher != null)
            {
                if (boards.ContainsKey(argName))
                {
                    boards.TryGetValue(argName, out r);
                }
                else
                {
                    r = new Board(dispatcher);
                    boards.Add(argName, r);
                }
                selectedBoard = argName;
            }
            else
                Logger.log("SceneManager: Warning - board (" + argName + ") creation failed, no dispatcher set", LEVEL.WARNING);

            return r;
        }

        public BoardActor createActor(int x, int y, String filename, AssetDetails argDetails)
        {

            BoardActor r = null;

            if (dispatcher != null)
            {
                r = new BoardActor(x, y, filename);
                r.loadAsset(filename, dispatcher, argDetails);
            }
            else
                Logger.log("SceneManager: Warning - board actor creation failed, no dispatcher set", LEVEL.WARNING);

            return r;
        }

        public Board selectBoard(String argName)
        {
            Board b = null;

            if (boards.ContainsKey(argName))
            {
                selectedBoard = argName;
                boards.TryGetValue(selectedBoard, out b);
            }
            
            return b;
        }

        public BoardActor getActorAt(Point p)
        {
            return selectBoard(selectedBoard).getActorAt(p); 
        }

    }
}
