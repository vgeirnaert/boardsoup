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
        private String loadingLogo = "D:\\C#\\BoardSoup\\BoardSoup\\Resources\\boardsoup_loading.png";
        private String loadingIdle = "D:\\C#\\BoardSoup\\BoardSoup\\Resources\\boardsoup_menu_idle.png";
        private String loadingBlue = "D:\\C#\\BoardSoup\\BoardSoup\\Resources\\boardsoup_menu_blue.png";
        private String loadingYellow = "D:\\C#\\BoardSoup\\BoardSoup\\Resources\\boardsoup_menu_yellow.png";
        private String loadingGreen = "D:\\C#\\BoardSoup\\BoardSoup\\Resources\\boardsoup_loading_menu_green.png";

        private GamePool gamePool;

        enum STATE { START_LOADING, END_LOADING, READY };

        STATE myState;

        public MainForm()
        {
            InitializeComponent();

            myState = STATE.START_LOADING;

            setPainter();
            
            init();
            myState = STATE.END_LOADING;
            myState = STATE.READY;
        }

        private void setPainter()
        {
            this.Paint += new PaintEventHandler(paintHandler);


        }

        private void init()
        {
            gamePool = new GamePool();

            if (gamePool.pluginsLoaded())
            {
            }
        }

        private void paintHandler(object sender, PaintEventArgs e)
        {
            // Get Graphics Object
            Graphics g = e.Graphics;
            String file = "";

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

            g.DrawImageUnscaled(Image.FromFile(file), new Rectangle(0, 0, 700, 700));
        }

    }
}
