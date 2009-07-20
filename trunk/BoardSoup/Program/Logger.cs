using System;
using System.Collections.Generic;

namespace BoardSoup
{
    public enum LEVEL {DEBUG = 1, WARNING = 2, ERROR = 3};

    public static class Logger
    {
        private const int LOG_BATCH = 1;

        // list of our messages
        static private List<KeyValuePair<LEVEL, String>> logEntries = new List<KeyValuePair<LEVEL, String>>();

        /**
         */
        static public void log(String entry, LEVEL level)
        {
            // add key value pair
            logEntries.Add(new KeyValuePair<LEVEL, String>(level, entry));

            if (logEntries.Count % LOG_BATCH == 0)
                writeToFile();
        }

        static private void writeToFile()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("boardsouplog.txt", true);
            String output = "";

            foreach (String s in getLatestLines(LEVEL.DEBUG, LOG_BATCH))
                output = output + "\r" + s;

            file.WriteLine(output);
            file.Close();
        }

        static private String getDateTime()
        {
            return DateTime.Now.ToString("d-M-yyyy H:m:s");
        }

        /**
         */ 
        static public String[] getLatestLines(LEVEL level, int amount)
        {
            List<String> result = new List<String>();
            int loop = logEntries.Count;
            int items = 0;
            amount = Math.Max(amount, 1);

            // loop through the items in the list
            // starting at the last and working 'downwards'
            // end the loop until we have 10 entries or search the end of the list
            while(items < Math.Min(amount, logEntries.Count) && loop > 0)
            {
                // reduce our loop iterator 
                loop--;

                // if the logging level of this entry is the same or higher than our threshold...
                if (logEntries[loop].Key >= level)
                {
                    // add the entry to our result list
                    result.Add(logEntries[loop].Value);
                    items++;
                }
            }

            // reverse the order to avoid our text showing up in the wrong order
            result.Reverse();

            // return
            return result.ToArray();
        }
    }
}
