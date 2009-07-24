using BoardSoupEngine.Kernel;

namespace BoardSoupEngine.Input
{
    internal class InputManager : IEventListener, ITickable
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
