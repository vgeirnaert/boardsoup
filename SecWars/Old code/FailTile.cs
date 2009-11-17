using BoardSoupEngine.Interface;
namespace SecWars.Core
{
    class FailTile : MenuTile
    {
        public FailTile(int x, int y, BoardObject argBoard) : base(x, y, "Resources\\menutile_fail.png", argBoard)
        {
        }
    }
}
