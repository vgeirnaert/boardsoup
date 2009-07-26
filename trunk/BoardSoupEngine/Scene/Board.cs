﻿using System.Collections.Generic;
using BoardSoupEngine.Kernel;
using System;
using System.Drawing;
namespace BoardSoupEngine.Scene
{
    internal class Board : ITickable
    {
        private bool active = true;
        private List<BoardActor> actors;

        //-----
        Bitmap boardmask;

        public Board()
        {          
        }

        public Board(IEventDispatcher argDispatcher)
        {
            actors = new List<BoardActor>();

            //-------------------------
            boardmask = new Bitmap("Resources\\hexagon_board_mask.png");
            Random rand = new Random();
            int randColor = rand.Next(6);
            string[] files = new string[6] {"Resources\\tile_yellow_inactive.png","Resources\\tile_red_inactive.png","Resources\\tile_white_inactive.png",
                                 "Resources\\tile_green_inactive.png","Resources\\tile_blue_inactive.png","Resources\\tile_purple_inactive.png"};

            string[] filesA = new string[6] {"Resources\\tile_yellow_active.png","Resources\\tile_red_active.png","Resources\\tile_white_active.png",
                                 "Resources\\tile_green_active.png","Resources\\tile_blue_active.png","Resources\\tile_purple_active.png"};

            for (int x = 0; x < 21; x++)
            {  
                for (int y = 0; y < 24; y++)
                {
                    int modX = ((y % 2) * 24);
                    int cX = (x * 49) + modX;
                    int cY = (y * 42);

                    if (cX < boardmask.Width - 1 && cY < boardmask.Height - 1)
                    {
                        if (!(boardmask.GetPixel(cX, cY).B == Color.Black.B))
                        {
                            int randT = rand.Next(10);
                            if (randT > 5)
                                continue;

                            BoardActor ba;
                            if (randT % 6 == randColor)
                                ba = new BoardActor(cX+100, cY+150, filesA[randT]);
                            else
                                ba = new BoardActor(cX + 100, cY + 150, files[randT]);

                            ba.setImage(ba.name, argDispatcher);

                            actors.Add(ba);
                        } 
                    }
                }
            }
        }

        public void onTick()
        {
            foreach (BoardActor a in actors)
                a.onTick();
            //------------
            //foreach (BoardActor a in actors)
            //   a.rotation += 2;
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
