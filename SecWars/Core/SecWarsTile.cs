using System;
using BoardSoupEngine.Interface;

namespace SecWars.Core
{
    class SecWarsTile : ActorObject
    {
        public SecWarsTile(int x, int y, String filename) : base(x, y, filename)
        {
        }

        public override void onClick()
        {
        }

        public override void onEngineObjectCreated()
        {
        }

        public override void onTick()
        {
            this.setRotation(this.getRotation() + 1);
        }
    }
}
