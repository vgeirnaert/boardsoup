using BoardSoupEngine.Kernel;
using System.Windows.Forms;

namespace BoardSoupEngine.Input
{
    internal class InputManager : IEventListener, ITickable
    {
        public enum INPUT { MOUSE, WIIMOTE };

        private IEventDispatcher dispatcher;
        private IInputDevice device;
        private Panel inputPanel;
        private ActionMapper mapper;

        public InputManager()
        {
            device = null;
            dispatcher = null;
            mapper = null;
        }

        public void setEventDispatcher(IEventDispatcher argDispatcher)
        {
            dispatcher = argDispatcher;
            mapper = new ActionMapper(dispatcher);
        }

        public void receiveEvent(Event argEvent)
        {
            argEvent.execute(this);
        }

        public void onTick()
        {
        }

        public void setInputMode(INPUT mode)
        {
            switch (mode)
            {
                case INPUT.MOUSE:
                    device = new MouseDevice(inputPanel, mapper, dispatcher);
                    break;
                case INPUT.WIIMOTE:
                    device = new WiimoteDevice(inputPanel, mapper);
                    break;
            }
        }

        public void setInputSurface(Panel p)
        {
            inputPanel = p;
            setInputMode(INPUT.MOUSE);
        }
    }
}
