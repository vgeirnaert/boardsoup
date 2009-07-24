using System.Collections.Generic;
using BoardSoupEngine.Kernel;
namespace BoardSoupEngine.Scene
{
    internal class Board
    {
        private bool active = true;
        private List<BoardActor> actors;

        public Board()
        {
        }

        public Board(IEventDispatcher argDispatcher)
        {
            actors = new List<BoardActor>();

            //-------------------------
            for (int x = 0; x < 20; x++)
                for (int y = 0; y < 20; y++)
                {
                    BoardActor ba = new BoardActor(x * 50, y * 50, "D:\\C#\\BoardSoup\\BoardSoup\\Resources\\Icon1.ico");
                    actors.Add(ba);
                    argDispatcher.submitEvent(new SceneActorLoadAssetEvent(ba));
                }
        }

        public void tick()
        {
            foreach (BoardActor a in actors)
                a.rotation++;
        }

        public void render()
        {
            foreach (BoardActor a in actors)
                a.render();
        }

        public void setActive(bool b)
        {
            active = b;
        }

        public bool isActive()
        {
            return active;
        }

        public void makeActor()
        {
            actors.Add(new BoardActor());
        }
    }
}
