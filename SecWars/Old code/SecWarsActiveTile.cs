using System;

namespace SecWars.Core
{
    class SecWarsActiveTile : SecWarsTile_old
    {
        public SecWarsActiveTile(int x, int y, String filename) : base(x, y, filename)
        {
        }

        public override void onClick()
        {
            if (board != null)
                board.endGame();
        }
    }

}
