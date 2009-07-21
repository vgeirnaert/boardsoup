namespace BoardSoupEngine.Kernel
{
    internal interface IEventListener
    {
        void receiveEvent(Event argEvent);
        void setEventDispatcher(IEventDispatcher argDispatcher);
        void onTick();
    }
}
