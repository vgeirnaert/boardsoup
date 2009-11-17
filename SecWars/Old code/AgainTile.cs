using BoardSoupEngine.Interface;
namespace SecWars.Core
{
    class AgainTile : MenuTile
    {
        public AgainTile(int x, int y, BoardObject argBoard) : base(x, y, "Resources\\menutile_again.png", argBoard)
        {
        }

        public override void onClick()
        {
            board.startGame();
        }
    }
}
