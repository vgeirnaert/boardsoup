using System;
using System.Collections.Generic;

namespace BoardSoup
{
    public enum LEVEL {DEBUG = 1, WARNING = 2, ERROR = 3};

    public static class Logger
    {
        // list of our messages
        static private List<KeyValuePair<LEVEL, String>> logEntries = new List<KeyValuePair<LEVEL, String>>();

        static public void log(String entry, LEVEL level)
        {
            // add key value pair
            logEntries.Add(new KeyValuePair<LEVEL, String>(level, entry));
        }

        static public String[] getTenLatestLines(LEVEL level)
        {
            List<String> result = new List<String>();
            int loop = logEntries.Count;
            int items = 0;

            while(items < Math.Min(10, logEntries.Count) && loop > 0)
            {
                loop--;

                if (logEntries[loop].Key >= level)
                {
                    result.Add(logEntries[loop].Value);
                    items++;
                }
            }

            return result.ToArray();
        }
    }
}
