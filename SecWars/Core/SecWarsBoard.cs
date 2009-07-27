using System;
using BoardSoupEngine.Interface;
using System.Drawing;

namespace SecWars.Core
{
    class SecWarsBoard : BoardObject
    {
        long time = 0;

        public SecWarsBoard() : base("")
        {
        }

        public SecWarsBoard(String argName) : base(argName)
        {
            this.name = argName;
        }

        public override void onBoardChanged()
        {
            
        }

        public void onTurn()
        {
            
        }

        public override void onEngineObjectCreated()
        {
            clearBoard();
            makeBoard();
        }

        public override void onTick()
        {
            long now = DateTime.Now.Ticks;

            if (now - time > 50000000)
            {
                time = now;
                clearBoard();
                makeBoard();
            }
        }

        public void makeBoard()
        {
            //-------------------------
            Bitmap boardmask = new Bitmap("Resources\\hexagon_board_mask.png");
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

                            SecWarsTile ba;
                            if (randT % 6 == randColor)
                                ba = new SecWarsTile(cX + 100, cY + 150, filesA[randT]);
                            else
                                ba = new SecWarsTile(cX + 100, cY + 150, files[randT]);

                            this.addActor(ba);
                        }
                    }
                }
            }
        }
    }
}
