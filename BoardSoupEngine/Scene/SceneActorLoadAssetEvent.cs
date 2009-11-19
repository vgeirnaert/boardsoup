using System;
using BoardSoupEngine.Assets;

namespace BoardSoupEngine.Scene
{
    internal class SceneActorLoadAssetEvent : SceneEvent
    {
        private BoardActor actor;
        private AssetDetails details;

        public SceneActorLoadAssetEvent()
        {
            actor = null;
            details = new AssetDetails(AssetType.IMAGE);
        }

        public SceneActorLoadAssetEvent(BoardActor ba, AssetDetails at)
        {
            actor = ba;
            details = at;
        }

        public override void execute(BoardSoupEngine.Kernel.IEventListener module)
        {
            if (module is AssetManager)
                actor.setAsset(((AssetManager)module).loadAsset(details, actor.name, null));

        }
    }
}
