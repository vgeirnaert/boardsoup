using BoardSoupEngine.Kernel;

namespace BoardSoupEngine.Interface
{
    public abstract class InterfaceObject
    {
        internal IEventDispatcher dispatcher;

        abstract public void onEngineObjectCreated();
        abstract public void onTick();
    }
}
