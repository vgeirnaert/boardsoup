using BoardSoupEngine.Kernel;
using System.Collections.Generic;
using System;

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
            selectedBoard = "test";
            boards.Add(selectedBoard, new Board(dispatcher));
            
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
                b.tick();
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

    }
}
