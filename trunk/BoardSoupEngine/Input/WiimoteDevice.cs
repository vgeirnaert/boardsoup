using System.Windows.Forms;

namespace BoardSoupEngine.Input
{
    internal class WiimoteDevice : IInputDevice
    {
        private ActionMapper myMapper;

        public WiimoteDevice(Panel p, ActionMapper mapper)
        {
            myMapper = mapper;
        }
    }
}
