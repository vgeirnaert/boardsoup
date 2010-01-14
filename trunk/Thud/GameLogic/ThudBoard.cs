using System;
using System.Collections.Generic;
using System.Text;
using BoardSoupEngine.Interface;

namespace Thud.GameLogic
{
    class ThudBoard : BoardObject
    {
        private EmptyPiece[,] pieces = new EmptyPiece[15, 15];
        ThudLogic logic;

        public ThudBoard(String argName, ThudLogic argLogic) : base(argName)
        {
            logic = argLogic;
            createBoard();
            setPieces();
        }

        public override void onBoardChanged()
        {
        }

        public override void onEngineObjectCreated()
        {
        }

        public override void onTick()
        {
        }

        private void setPieces()
        {
            //stone
            PawnPiece stone = new StonePiece(7 * 66 + 30, 7 * 66 + 30);
            stone.setLogic(logic);
            pieces[7, 7].setOccupant(stone);
            this.addActor(stone);

            //trolls
            for (int x = 6; x < 9; x++)
            {
                for (int y = 6; y < 9; y++)
                {
                    if (!(x == 7 && y == 7))
                    {
                        PawnPiece troll = new TrollPiece(x * 66 + 30, y * 66 + 30);
                        troll.setLogic(logic);
                        pieces[x, y].setOccupant(troll);
                        this.addActor(troll);
                    }
                }
            }


            //dwarves
            makeDwarves(6, 0, -1, 1);
            makeDwarves(8, 0, 1, 1);
            makeDwarves(6, 14, -1, -1);
            makeDwarves(8, 14, 1, -1);
        }

        private void makeDwarves(int startX, int startY, int dirX, int dirY)
        {
            placeDwarf(startX, startY);

            startX = startX + dirX;
            for (int i = 0; i < 6; i++)
                placeDwarf(startX + (i * dirX), startY + (i * dirY));

            placeDwarf(startX + (5 * dirX), startY + (6 * dirY));
        }

        private void placeDwarf(int x, int y)
        {
            PawnPiece dwarf = new DwarfPiece(x * 66 + 30, y * 66 + 30);
            dwarf.setLogic(logic);
            pieces[x, y].setOccupant(dwarf);
            this.addActor(dwarf);
        }

        private void createBoard()
        {
            //make pieces
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    if (!isCorner(x, y))
                    {
                        bool even = false;
                        if ((x % 2 == 0 && y % 2 == 0) || (x % 2 == 1 && y % 2 == 1))
                            even = true;

                        EmptyPiece tp = new EmptyPiece(x * 66 + 30, y * 66 + 30, even);
                        tp.setLogic(logic);
                        tp.setBoardPosition(x, y);
                        this.addActor(tp);
                        pieces[x, y] = tp;
                    }
                }
            }

            // set neighbours
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    if (pieceExists(x, y))
                    {
                        ThudPiece current = pieces[x, y];

                        if (pieceExists(x, y - 1))
                            current.setNeighbour(NEIGHBOUR.NORTH, pieces[x, y - 1]);

                        if (pieceExists(x + 1, y - 1))
                            current.setNeighbour(NEIGHBOUR.NORTHEAST, pieces[x + 1, y - 1]);

                        if (pieceExists(x + 1, y))
                            current.setNeighbour(NEIGHBOUR.EAST, pieces[x + 1, y]);

                        if (pieceExists(x + 1, y + 1))
                            current.setNeighbour(NEIGHBOUR.SOUTHEAST, pieces[x + 1, y + 1]);

                        if (pieceExists(x, y + 1))
                            current.setNeighbour(NEIGHBOUR.SOUTH, pieces[x, y + 1]);

                        if (pieceExists(x - 1, y + 1))
                            current.setNeighbour(NEIGHBOUR.SOUTHWEST, pieces[x - 1, y + 1]);

                        if (pieceExists(x - 1, y))
                            current.setNeighbour(NEIGHBOUR.WEST, pieces[x - 1, y]);

                        if (pieceExists(x - 1, y - 1))
                            current.setNeighbour(NEIGHBOUR.NORTHWEST, pieces[x - 1, y - 1]);
                    }
                }
            }
        }

        private bool pieceExists(int x, int y)
        {
            if (x < 0 || x > 14)
                return false;

            if (y < 0 || y > 14)
                return false;

            if (isCorner(x, y))
                return false;

            if (pieces[x, y] == null)
                return false;

            return true;
        }

        private bool isCorner(int x, int y)
        {
            int[] rows = new int[15] { 5, 7, 9, 11, 13, 15, 15, 15, 15, 15, 13, 11, 9, 7, 5 };
            int total = 15;

            // lower and higher bounds
            int lower = (total - rows[x]) / 2;
            int higher = rows[x] + lower - 1;

            if (y < lower || y > higher)
                return true;

            // lower and higher bounds
            lower = (total - rows[y]) / 2;
            higher = rows[y] + lower - 1;

            if (x < lower || x > higher)
                return true;

            return false;
        }
    }
}
