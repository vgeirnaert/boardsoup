using System;
using System.Collections.Generic;

namespace SecWars.Core
{
    enum NEIGHBOUR { NORTHEAST, EAST, SOUTHEAST, SOUTHWEST, WEST, NORTHWEST };

    class Hexagon
    {
        private Territory myTerritory;
        private Dictionary<NEIGHBOUR, Hexagon> myNeighbours;
        private GameObject myObject;

        public Hexagon()
        {
            myTerritory = null;
            myNeighbours = new Dictionary<NEIGHBOUR,Hexagon>();
        }

        public void setTerritory(Territory argTerritory)
        {
            myTerritory = argTerritory;
        }

        public Territory getTerritory()
        {
            return myTerritory;
        }

        public void setNeighbour(Hexagon argNeighbour, NEIGHBOUR argArea)
        {
            myNeighbours.Add(argArea, argNeighbour);
        }

        public bool isCapturable()
        {
            // check self
            // we are capturable if we have no defender
            bool capturable = !hasDefender();

            // check neighbours
            foreach (KeyValuePair<NEIGHBOUR, Hexagon> kvp in myNeighbours)
            {
                // we are not capturable if any of our neighbours are defended
                if (kvp.Value.hasDefender())
                    capturable = false;
            }

            return capturable;
        }

        public bool hasDefender()
        {
            if (myObject is DefenceObject)
                return true;

            return false;
        }
    }
}
