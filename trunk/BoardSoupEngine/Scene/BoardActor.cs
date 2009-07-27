using BoardSoupEngine.Assets;
using System.Drawing;
using System;
using BoardSoupEngine.Interface;
using BoardSoupEngine.Kernel;

namespace BoardSoupEngine.Scene
{
    internal class BoardActor : ITickable
    {
        private Asset myAsset;
        private ActorObject myInterface;

        public Point location;
        public int rotation;
        public String name;
        public bool receivesInput;

        public BoardActor()
        {
            myAsset = null;
            myInterface = null;
            
            receivesInput = true;
            location = new Point(0, 0);
            name = "";
            rotation = 0;
        }

        public BoardActor(int x, int y, String filename) : this()
        {
            location.X = x;
            location.Y = y;
            name = filename;
        }

        public void render()
        {
            if (myAsset != null)
                myAsset.render(location, rotation);
        }

        public void loadAsset(String filename, IEventDispatcher dispatcher)
        {
            dispatcher.submitEvent(new SceneActorLoadAssetEvent(this));
        }

        public void setAsset(Asset argAsset)
        {
            myAsset = argAsset;
        }

        public void setInterfaceObject(ActorObject ao)
        {
            myInterface = ao;
            ao.setActor(this);
        }

        public void onTick()
        {
            if (myInterface != null)
                myInterface.onTick();
        }
    }
}
