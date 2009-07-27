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

        public ActorObject(int x, int y, String imageName)
        {
            dispatcher = BoardSoupEngine.getInstance().getInternalDispatcher();
            CreateInternalBoardActorEvent e = new CreateInternalBoardActorEvent(this, x, y, imageName);
            dispatcher.submitEvent(e);
        }

        abstract public void onClick();

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
            //return actor.name; TODO
            return "";
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
        
    }
}
