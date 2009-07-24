namespace BoardSoupEngine.Kernel
{
    internal interface IEventDispatcher
    {
        void registerListener(IEventListener listener);
        void submitEvent(Event argEvent);
    }
}
