﻿using BoardSoupEngine.Kernel;

namespace BoardSoupEngine.Assets
{
    internal class AssetManager : IEventListener
    {
        IEventDispatcher dispatcher;

        public void setEventDispatcher(IEventDispatcher argDispatcher)
        {
            dispatcher = argDispatcher;
        }

        public void receiveEvent(Event argEvent)
        {
        }

        public void onTick()
        {
        }

    }
}