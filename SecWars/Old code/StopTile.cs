using BoardSoupEngine.Interface;

namespace SecWars.Core
{
    class StopTile : MenuTile
    {
        public StopTile(int x, int y, BoardObject argBoard) : base(x, y, "Resources\\menutile_stop.png", argBoard)
        {
        }
        public override void onClick()
        {
            board.exit();
        }

    }
}
