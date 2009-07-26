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

        abstract public void onClick();

        public void setLocation(Point argLocation)
        {
            actor.location = argLocation;
        }

        public void setRotation(int argRotation)
        {
            actor.rotation = argRotation;
        }

        public void setImage(String filename)
        {
            if (dispatcher != null)
                actor.setImage(filename, dispatcher);
            else
                Logger.log("ActorObject: Warning - no event dispatcher, unable to set actor image.", LEVEL.WARNING);
        }

        internal void setActor(BoardActor ba)
        {
            actor = ba;
            actor.setInterfaceObject(this);
            this.onEngineObjectCreated();
        }

        public void receivesInput(bool input)
        {
            actor.receivesInput = input;
        }
        
    }
}
