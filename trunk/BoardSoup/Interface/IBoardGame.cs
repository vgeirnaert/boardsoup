using System;

namespace BoardSoup.Interface
{
    public interface IBoardGame
    {
        String getName();
        String getDescription();
        String getGameVersion();
        String getEngineVersion();
        bool startGame();
        
    }
}
