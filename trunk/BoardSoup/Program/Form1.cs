using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BoardSoup
{
    public partial class MainForm : Form
    {
        // version
        private const String version = "0.0.3";

        // drawing stuff
        private Image loadingLogo = Properties.Resources.boardsoup_menu_loading;
        private Image loadingIdle = Properties.Resources.boardsoup_menu_idle;
        private Image loadingBlue = Properties.Resources.boardsoup_menu_blue;
        private Image loadingYellow = Properties.Resources.boardsoup_menu_yellow;
        private Image loadingGreen = Properties.Resources.boardsoup_menu_green;
        private Font font = new Font("Verdana", 12, FontStyle.Bold);
        private Brush brush = new SolidBrush(Color.Gray);

        // collection of our games
        private GamePool gamePool;
        
        // drawing state
        enum STATE { START_LOADING, END_LOADING, READY };
        STATE myState;

        public MainForm()
        {
            InitializeComponent();

            myState = STATE.START_LOADING;

            setPainter();

            init();

            Logger.log("BoardSoup v" + version + " loaded", LEVEL.DEBUG);
            myState = STATE.END_LOADING;
            myState = STATE.READY;
        }

        /**
         */
        private void setPainter()
        {
            this.Paint += new PaintEventHandler(paintHandler);


        }

        /**
         */
        private void init()
        {
            // make the collection
            gamePool = new GamePool();

            // if we loaded succesfully
            if (gamePool.pluginsLoaded())
            {
            }
        }

        /**
         */
        private void paintHandler(object sender, PaintEventArgs e)
        {
            Console.WriteLine("painting");
            Console.WriteLine(myState);
            // Get Graphics Object
            Graphics g = e.Graphics;
            Image file = null;

            switch (myState)
            {
                case STATE.START_LOADING:
                    file = loadingLogo;
                    break;
                case STATE.END_LOADING:
                    file = loadingBlue;
                    break;
                case STATE.READY:
                    file = loadingIdle;
                    break;
            }

            g.DrawImageUnscaled(file, new Rectangle(this.Width / 2 - 350, this.Height / 2 - 350, 700, 700));
            
            Point textPoint = new Point(50, 50);
            foreach(String s in Logger.getTenLatestLines(LEVEL.DEBUG))
            {
                Console.WriteLine("drawing");
                textPoint.Y += 16;
                g.DrawString(s, font, brush, textPoint);
            }
        }

    }
}
