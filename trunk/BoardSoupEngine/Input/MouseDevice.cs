using System.Windows.Forms;
using System.Drawing;
using BoardSoupEngine.Kernel;

namespace BoardSoupEngine.Input
{
    internal class MouseDevice : IInputDevice
    {
        private ActionMapper myMapper;
        private Cursor myCursor;

        public MouseDevice(Panel p, ActionMapper mapper, IEventDispatcher dispatcher)
        {
            myMapper = mapper;
            p.MouseClick += new System.Windows.Forms.MouseEventHandler(this.onMouseClick);
            p.MouseMove += new System.Windows.Forms.MouseEventHandler(this.onMouseMove);
            myCursor = new Cursor(dispatcher, p.Size);
        }

        private void onMouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            myCursor.click(e.Clicks, e.Location);
        }

        private void onMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            myCursor.setPosition(e.Location);
        }
    }
}
