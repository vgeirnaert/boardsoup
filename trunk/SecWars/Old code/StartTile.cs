using BoardSoupEngine.Interface;

namespace SecWars.Core
{
    class StartTile : MenuTile
    {
        public StartTile(int x, int y, BoardObject argBoard) : base(x, y, "Resources\\menutile_start.png", argBoard)
        {
        }

        public override void onClick()
        {
            board.startGame();
        }
    }
}
