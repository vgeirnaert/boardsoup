using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoardSoupEngine.Utilities
{
    public class Dice
    {
        static private Random generator;

        static public int roll(int numberOfDice, int numberOfSides)
        {
            if(generator == null)
                generator = new Random();

            int result = -1;

            if(numberOfDice > 0 && numberOfSides > 0)
            {
                result = 0;

                for (int i = 0; i < numberOfDice; i++)
                    result = result + generator.Next(numberOfSides);
            }

            return result;
        }
    }
}
