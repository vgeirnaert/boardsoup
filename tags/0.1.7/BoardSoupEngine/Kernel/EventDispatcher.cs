using System.Collections.Generic;

namespace BoardSoupEngine.Kernel
{
    internal class EventDispatcher : IEventDispatcher 
    {
        List<IEventListener> listeners;

        public EventDispatcher()
        {
            listeners = new List<IEventListener>();
        }

        public void registerListener(IEventListener argListener)
        {
            argListener.setEventDispatcher(this);
            listeners.Add(argListener);
        }

        public void submitEvent(Event argEvent)
        {
            foreach (IEventListener l in listeners)
                l.receiveEvent(argEvent);

            argEvent = null;
        }
    }
}
