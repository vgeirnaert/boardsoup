using BoardSoupEngine.Interface;
namespace SecWars.Core
{
    class WinTile : MenuTile
    {
        public WinTile(int x, int y, BoardObject argBoard) : base(x, y, "Resources\\menutile_win.png", argBoard)
        {
        }
    }
}
