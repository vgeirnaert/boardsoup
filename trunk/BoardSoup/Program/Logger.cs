using System;
using System.Collections.Generic;

namespace BoardSoup
{
    enum LEVEL {DEBUG = 1, WARNING = 2, ERROR = 3};

    public static class Logger
    {
        static private List<String> logEntries = new List<String>();
        static private LEVEL currentLevel = LEVEL.DEBUG;

        static public void log(String entry, LEVEL level)
        {
            if(currentLevel <= level)
                logEntries.Add(entry);
        }

        static public void adjustLevel(LEVEL level)
        {
            currentLevel = level;
        }

        static public String[] getTenLatestLines()
        {
            return logEntries.GetRange(Math.Max(logEntries.Count - 10, 0), Math.Min(10, logEntries.Count)).ToArray();
        }
    }
}
