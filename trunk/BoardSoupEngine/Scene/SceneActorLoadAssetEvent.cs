using System;
using BoardSoupEngine.Assets;

namespace BoardSoupEngine.Scene
{
    internal class SceneActorLoadAssetEvent : SceneEvent
    {
        private BoardActor actor;
        private AssetType type;

        public SceneActorLoadAssetEvent()
        {
            actor = null;
            type = AssetType.IMAGE;
        }

        public SceneActorLoadAssetEvent(BoardActor ba, AssetType at)
        {
            actor = ba;
            type = at;
        }

        public override void execute(BoardSoupEngine.Kernel.IEventListener module)
        {
            if (module is AssetManager)
                actor.setAsset(((AssetManager)module).loadAsset(type, actor.name, null));

        }
    }
}
