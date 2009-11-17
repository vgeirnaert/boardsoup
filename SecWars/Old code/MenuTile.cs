using System;
using BoardSoupEngine.Interface;

namespace SecWars.Core
{
    class MenuTile : ActorObject
    {
        protected MenuBoard board;

        public MenuTile(int x, int y, String filename, BoardObject argBoard) : base(x, y, filename)
        {
            board = (MenuBoard)argBoard;
        }

        public override void onClick()
        {
            
        }

        public override void onEngineObjectCreated()
        {
            
        }

        public override void onTick()
        {
            
        }

        public override void onMouseIn()
        {
            
        }

        public override void onMouseOut()
        {
            
        }
    }
}
