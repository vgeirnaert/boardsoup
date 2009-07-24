using BoardSoupEngine.Assets;
using System.Drawing;
using System;

namespace BoardSoupEngine.Scene
{
    internal class BoardActor
    {
        private Asset myAsset;
        public Point location;
        public int rotation;
        public String name;

        public BoardActor()
        {
            myAsset = null;
            location = new Point(0, 0);
        }

        public BoardActor(int x, int y, String filename) : this()
        {
            location.X = x;
            location.Y = y;
            name = filename;
        }

        public void render()
        {
            if (myAsset != null)
                myAsset.render(location, rotation);
        }

        public void setAsset(Asset argAsset)
        {
            myAsset = argAsset;
        }




    }
}
