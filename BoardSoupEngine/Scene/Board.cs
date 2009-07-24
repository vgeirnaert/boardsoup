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
            {  
                for (int y = 0; y < 20; y++)
                {
                    int modX = ((y % 2) * 16);
                    int cX = (x * 32) + modX;
                    int cY = (y * 24);
                    BoardActor ba = new BoardActor(cX, cY, "Resources\\Icon1.ico");
                    actors.Add(ba);
                    argDispatcher.submitEvent(new SceneActorLoadAssetEvent(ba));
                }
            }
        }

        public void tick()
        {
            /*int i = 0;
            foreach (BoardActor a in actors)
            {
                if(i % 2 == 0)
                    a.rotation += 2;

                i++;
            }*/

            foreach (BoardActor a in actors)
               a.rotation += 2;
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
