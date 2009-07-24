using System;
using BoardSoupEngine.Assets;

namespace BoardSoupEngine.Scene
{
    internal class SceneActorLoadAssetEvent : SceneEvent
    {
        private BoardActor actor;

        public SceneActorLoadAssetEvent()
        {
            actor = null;
        }

        public SceneActorLoadAssetEvent(BoardActor ba)
        {
            actor = ba;
        }

        public override void execute(BoardSoupEngine.Kernel.IEventListener module)
        {
            if (module is AssetManager)
                actor.setAsset(((AssetManager)module).loadAsset(actor.name));
        }
    }
}
