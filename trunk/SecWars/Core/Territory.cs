using System;
using System.Collections.Generic;

namespace SecWars.Core
{
    class Territory
    {
        private COLOR color;
        private List<Hexagon> hexagons;

        public Territory()
        {
            hexagons = new List<Hexagon>();
        }

        public COLOR getColor()
        {
            return color;
        }

        public void setColor(COLOR argColor)
        {
            color = argColor;
        }

        public void merge(Territory argTerritory)
        {
            if (this.getSize() < argTerritory.getSize())
                argTerritory.merge(this);
            else
            {
                foreach (Hexagon h in argTerritory.getHexagons())
                    this.addHexagon(h);
            }
        }

        public void addHexagon(Hexagon argHexagon)
        {
            hexagons.Add(argHexagon);
            argHexagon.setTerritory(this);
        }

        public int getSize()
        {
            return hexagons.Count;
        }

        public void setCapital()
        {
            // find middle spot in this territory
            // create capital there
        }

        public List<Hexagon> getHexagons()
        {
            return hexagons;
        }
    }
}
