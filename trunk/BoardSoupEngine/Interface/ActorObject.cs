using BoardSoupEngine.Scene;
using System.Drawing;
using System;
using BoardSoupEngine.Kernel;
using BoardSoupEngine.Utilities;

namespace BoardSoupEngine.Interface
{
    public abstract class ActorObject : InterfaceObject
    {
        private BoardActor actor;
        private String name;

        public ActorObject(int x, int y, String imageName)
        {
            name = imageName;
            dispatcher = BoardSoupEngine.getInstance().getInternalDispatcher();
            CreateInternalBoardActorEvent e = new CreateInternalBoardActorEvent(this, x, y, name);
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

        public void setImage(String filename)
        {
            if (dispatcher != null)
                actor.loadAsset(filename, dispatcher);
            else
                Logger.log("ActorObject: Warning - no event dispatcher, unable to set actor image.", LEVEL.WARNING);
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

        public void receivesInput(bool input)
        {
            actor.receivesInput = input;
        }

        abstract public void onMouseIn();
        abstract public void onMouseOut();
        abstract public void onClick();

    }
}
