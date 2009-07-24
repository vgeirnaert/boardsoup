using System;
using System.Collections.Generic;
using BoardSoupEngine.Utilities;

namespace BoardSoupEngine.Kernel
{
    internal static class EventFactory
    {
        static private Dictionary<String, Event> events = new Dictionary<String,Event>();

        public static Event createEvent(String eventName)
        {
            // our event
            Event e = null;

            // try to find it in our dictionary (and assign it in the out parameter)
            if(!events.TryGetValue(eventName, out e))
            {
                // its not in our dictionary, so we make an obect and add it to the dictionary for future use
                Type t = Type.GetType(eventName);

                // if we found our type
                if (t != null)
                {
                    Logger.log("EventFactory making " + eventName, LEVEL.DEBUG);
                    e = (Event)Activator.CreateInstance(t);
                    events.Add(eventName, e);
                }
            }

            // return it
            return e;
        }
    }
}
