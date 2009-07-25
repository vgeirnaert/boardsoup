using System.Collections.Generic;
using BoardSoupEngine.Kernel;
using System;
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
            Random rand = new Random();
            int randColor = rand.Next(6);
            string[] files = new string[6] {"Resources\\tile_yellow_inactive.png","Resources\\tile_red_inactive.png","Resources\\tile_white_inactive.png",
                                 "Resources\\tile_green_inactive.png","Resources\\tile_blue_inactive.png","Resources\\tile_purple_inactive.png"};

            string[] filesA = new string[6] {"Resources\\tile_yellow_active.png","Resources\\tile_red_active.png","Resources\\tile_white_active.png",
                                 "Resources\\tile_green_active.png","Resources\\tile_blue_active.png","Resources\\tile_purple_active.png"};
            for (int x = 0; x < 16; x++)
            {  
                for (int y = 0; y < 16; y++)
                {
                    int modX = ((y % 2) * 24);
                    int cX = (x * 49) + modX;
                    int cY = (y * 42);

                    int randT = rand.Next(7);
                    if (randT == 6)
                        continue;

                    BoardActor ba;
                    if (randT % 6 == randColor)
                        ba = new BoardActor(cX, cY, filesA[randT]);
                    else
                        ba = new BoardActor(cX, cY, files[randT]);

                    actors.Add(ba);
                    argDispatcher.submitEvent(new SceneActorLoadAssetEvent(ba));
                }
            }
        }

        public void tick()
        {
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
