using System.Windows.Forms;

namespace BoardSoup.Program
{
    class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
        }

        public void EnableDoubleBuffering()
        {
            // Set the value of the double-buffering style bits to true.
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.GetStyle(ControlStyles.OptimizedDoubleBuffer))
                System.Console.WriteLine("AARG!");
            
        }
    }
}
