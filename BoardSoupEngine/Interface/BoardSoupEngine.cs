using System;
using BoardSoupEngine.Kernel;
using System.Windows.Forms;

namespace BoardSoupEngine.Interface
{
    public class BoardSoupEngine
    {
        private Kernel.Kernel myKernel;

        private static BoardSoupEngine instance;

        public static BoardSoupEngine getInstance()
        {
            if (instance == null)
                instance = new BoardSoupEngine();

            return instance;
        }

        private BoardSoupEngine()
        {
            myKernel = new Kernel.Kernel();
        }

        public String getVersion()
        {
            return BSEDefines.version; 
        }

        public void tick()
        {
            myKernel.tick();
        }

        public void setRenderSurface(Panel surface)
        {
            myKernel.setRenderSurface(surface);
        }

        internal IEventDispatcher getInternalDispatcher()
        {
            return myKernel.getEventDispatcher();
        }
    }
}
