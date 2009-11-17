using BoardSoupEngine.Kernel;
using System.Drawing;

namespace BoardSoupEngine.Input
{
    internal class ActionMapper
    {
        private IEventDispatcher dispatcher;

        public ActionMapper(IEventDispatcher d)
        {
            dispatcher = d;
        }
    }
}
