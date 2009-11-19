using BoardSoupEngine.Kernel;
using BoardSoupEngine.Scene;
using System;
using BoardSoupEngine.Assets;

namespace BoardSoupEngine.Interface
{
    class CreateInternalBoardActorEvent : Event
    {
        ActorObject actor;
        int x, y;
        String imageName;
        AssetDetails details;

        public CreateInternalBoardActorEvent()
        {
            x = 0;
            y = 0;
            imageName = "";
            actor = null;
        }

        public CreateInternalBoardActorEvent(ActorObject ao, int argX, int argY, String filename, AssetDetails argDetails) : this()
        {
            actor = ao;
            x = argX;
            y = argY;
            imageName = filename;
            details = argDetails;
        }

        public override void execute(IEventListener module)
        {
            if(module is SceneManager && actor != null)
            {
                BoardActor b = ((SceneManager)module).createActor(x, y, imageName, details);
                b.setInterfaceObject(actor);
                actor.onEngineObjectCreated();
            }
        }
    }
}
