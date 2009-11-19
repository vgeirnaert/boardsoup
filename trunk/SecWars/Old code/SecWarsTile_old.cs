using System;
using BoardSoupEngine.Interface;

namespace SecWars.Core
{
    class SecWarsTile_old : ImageActor
    {
        protected SecWarsBoard_old board;
        private String image;

        public SecWarsTile_old(int x, int y, String filename) : base(x, y, filename)
        {
            image = filename;    
        }

        public override void onClick()
        {
            if (board != null)
                board.markForDeletion(this);
        }

        public override void onEngineObjectCreated()
        {
        }

        public override void onTick()
        {
            this.setRotation(this.getRotation() + 1);
        }

        public void setBoard(SecWarsBoard_old b)
        {
            board = b;
        }

        public override void onMouseIn()
        {
            this.setImage(image.Replace("_ina", "_a"));
        }

        public override void onMouseOut()
        {
            this.setImage(image);
        }
    }
}
