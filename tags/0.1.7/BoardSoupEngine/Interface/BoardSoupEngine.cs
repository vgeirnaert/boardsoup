using System;
using BoardSoupEngine.Kernel;
using System.Windows.Forms;

namespace BoardSoupEngine.Interface
{
    public class BoardSoupEngine
    {
        private Kernel.Kernel myKernel;

        public BoardSoupEngine()
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
    }
}
