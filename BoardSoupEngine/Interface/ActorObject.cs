using BoardSoupEngine.Scene;
using System.Drawing;
using System;
using BoardSoupEngine.Kernel;
using BoardSoupEngine.Utilities;
using BoardSoupEngine.Assets;

namespace BoardSoupEngine.Interface
{
    public abstract class ActorObject : InterfaceObject
    {
        internal BoardActor actor;
        private String name;

        public ActorObject(int x, int y, String actorName, AssetType argType)
        {
            name = actorName;
            dispatcher = BoardSoupEngine.getInstance().getInternalDispatcher();
            CreateInternalBoardActorEvent e = new CreateInternalBoardActorEvent(this, x, y, name, new AssetDetails(argType));
            dispatcher.submitEvent(e);
        }

        public void setLocation(Point argLocation)
        {
            actor.location = argLocation;
        }

        public Point getLocation()
        {
            return actor.location;
        }

        public void setRotation(int argRotation)
        {
            actor.rotation = argRotation;
        }

        public int getRotation()
        {
            return actor.rotation;
        }

        public void receivesInput(bool argInput)
        {
            actor.receivesInput = argInput;
        }

        public String getFile()
        {
            return name;
        }

        internal void setActor(BoardActor ba)
        {
            actor = ba;
        }

        internal BoardActor getActor()
        {
            return actor;
        }

        abstract public void onMouseIn();
        abstract public void onMouseOut();
        abstract public void onClick();

    }
}
