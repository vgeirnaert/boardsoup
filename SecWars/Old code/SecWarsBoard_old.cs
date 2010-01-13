using System;
using BoardSoupEngine.Interface;
using System.Collections.Generic;
using System.Drawing;

namespace SecWars.Core
{
    class SecWarsBoard_old : BoardObject
    {
        private long time = 0;
        private SecWarsGameLogic_old logic;
        private List<SecWarsTile_old> deleteQueue;
        private Title statusText;
        private Title timeText;
        private int textTime = 0;
        private long lastUpdate = 0;

        public SecWarsBoard_old() : base("")
        {
            deleteQueue = new List<SecWarsTile_old>();
        }

        public SecWarsBoard_old(String argName) : base(argName)
        {
            this.name = argName;
            deleteQueue = new List<SecWarsTile_old>();
        }

        public override void onBoardChanged()
        {
            if(logic != null)
                logic.evaluateGame(this);

            if(statusText != null)
                statusText.setText("Hexagons left: " + this.getAllBoardActors().Count);
        }

        public void onTurn()
        {
            
        }

        public override void onEngineObjectCreated()
        {
            time = DateTime.Now.Ticks; 
            clearBoard();
            makeBoard();
        }

        public override void onTick()
        {
            if (deleteQueue != null)
            {
                foreach (SecWarsTile_old t in deleteQueue)
                    this.deleteFromBoard(t);

                deleteQueue.Clear();
            }

            long now = DateTime.Now.Ticks;

            if (now - time > 500000000)
            {
                if (logic != null)
                    logic.endGame(this);
            }

            if (now - lastUpdate > 10000000 && timeText != null)
            {
                textTime++;
                timeText.setText("Time left: " + (50 - textTime));
                lastUpdate = now;
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
                            int randT = rand.Next(5); // increase value for more empty spots
                            if (randT > 5)
                                continue;

                            SecWarsTile_old ba;
                            if (randT % 6 == randColor)
                                ba = new SecWarsActiveTile(cX + 100, cY + 150, filesA[randT]);
                            else
                                ba = new SecWarsTile_old(cX + 100, cY + 150, files[randT]);

                            ba.setBoard(this);
                            this.addActor(ba);
                        }
                    }
                }
            }
            statusText = new Title(100, 100, "Hexagons left: " + this.getAllBoardActors().Count);
            timeText = new Title(400, 100, "Time left: 50");
            textTime = 0;
            this.addActor(statusText);
            this.addActor(timeText);
            time = DateTime.Now.Ticks;
            lastUpdate = time;
        }

        public void setLogic(SecWarsGameLogic_old l)
        {
            logic = l;
        }

        public void markForDeletion(SecWarsTile_old tile)
        {
            deleteQueue.Add(tile);
        }

        public void endGame()
        {
            logic.endGame(this);
        }
    }
}
