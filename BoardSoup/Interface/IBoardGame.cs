using System;
using System.Windows.Forms;

namespace BoardSoup.Interface
{
    public interface IBoardGame
    {
        String getName();
        String getDescription();
        String getGameVersion();
        String getEngineVersion();
        void setRenderSurface(Panel renderPanel);
        void gameLoop();        
    }
}
