namespace BoardSoupEngine.Kernel
{
    internal abstract class Event
    {
        abstract public void execute(IEventListener module);
    }
}
