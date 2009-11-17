using System;
using System.Collections.Generic;

namespace SecWars.Core
{
    enum COLOR { BLUE, GREEN, YELLOW, RED, PURPLE, ORANGE };

    class Player
    {
        private COLOR color;

        public Player(COLOR argColor)
        {
            color = argColor;
        }

        public COLOR getColor()
        {
            return color;
        }

        public bool controls(Territory argTerritory)
        {
            if (argTerritory.getColor() == color)
                return true;

            return false;
        }
    }
}
