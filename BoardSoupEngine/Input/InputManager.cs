using BoardSoupEngine.Kernel;

namespace BoardSoupEngine.Input
{
    internal class InputManager : IEventListener
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
