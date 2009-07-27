using System;

namespace BoardSoupEngine.Utilities
{
    internal class Dice
    {
        static private Random generator;

        /**
         */
        static public int roll(int numberOfDice, int numberOfSides)
        {
            // make a RNG if it doesnt exist yet
            if(generator == null)
                generator = new Random();

            // if the input isnt valid, we return -1
            int result = -1;

            // test if input is valid
            if(numberOfDice > 0 && numberOfSides > 0)
            {
                result = 0;

                // roll the dice, add their results
                for (int i = 0; i < numberOfDice; i++)
                    result = result + generator.Next(numberOfSides);
            }

            return result;
        }
    }
}
