using System;
using BoardSoupEngine.Kernel;

namespace BoardSoupEngine.Interface
{
    public class BoardSoupEngine
    {
        public BoardSoupEngine()
        {
        }

        public String getVersion()
        {
            return BSEDefines.version; 
        }
    }
}
