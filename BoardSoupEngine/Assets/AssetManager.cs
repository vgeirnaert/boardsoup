using BoardSoupEngine.Kernel;

namespace BoardSoupEngine.Assets
{
    internal class AssetManager : IEventListener, ITickable
    {
        private IEventDispatcher dispatcher;

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
