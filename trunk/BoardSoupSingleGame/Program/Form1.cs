using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BoardSoup.Interface;
using BoardSoupEngine.Utilities;

namespace BoardSoup.Program
{
    public partial class MainForm : Form
    {
        // version
        private const String version = "0.1.0";

        // collection of our games
        private GamePool gamePool;
        private Thread gamethread;

        public MainForm()
        {
            InitializeComponent();

            init();

            Logger.log("BoardSoup v" + version + " loaded", LEVEL.DEBUG);
        }

        /**
         */
        private void init()
        {
            // make the collection
            gamePool = new GamePool();          
        }

        private void startGame(String name)
        {
            // end any games that may have been running
            endGameThread();

            prepareRenderPanel();

            IBoardGame myGame = gamePool.getGame(name);

            myGame.setRenderSurface(renderPanel);
            // make and start a new thread
            gamethread = new Thread(new ThreadStart(myGame.gameLoop));
            gamethread.Start();
        }

        private void prepareRenderPanel()
        {
            renderPanel.Width = this.Width;
            renderPanel.Height = this.Height;
            renderPanel.Location = new Point(0, 0);
            renderPanel.Visible = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            endGameThread();
        }

        private void endGameThread()
        {
            // end thread
            if (gamethread != null)
                if (gamethread.IsAlive)
                    gamethread.Abort();

            // make panel invisible
            renderPanel.Visible = false;
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
            // if we loaded succesfully
            if (gamePool.pluginsLoaded())
            {
                // if we only have 1 game start it right away
                if (gamePool.getGameNames().Count == 1)
                    startGame(gamePool.getGameNames()[0]);
            }    
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (gamethread != null)
                if (!gamethread.IsAlive)
                    this.Close();
        }

        private void renderPanel_MouseClick(object sender, MouseEventArgs e)
        {
            MainForm_MouseClick(sender, e);
        }

        private void renderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            MainForm_MouseClick(sender, e);
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            MainForm_MouseClick(sender, e);
        }

		private void MainForm_ResizeEnd(object sender, EventArgs e) {
			prepareRenderPanel();
		}

		private void MainForm_SizeChanged(object sender, EventArgs e) {
			prepareRenderPanel();
		}
    }
}
