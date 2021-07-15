using System;
using System.Collections.Generic;

namespace BTB.Engine
{
    /// <summary>
    /// Responsible for handlind the user's input during the game's GameLoop.
    /// </summary>
    public static class Input
    {
        // Declare private readonly variable of type
        // BlockingCollection<ConsoleKey> used to save the player's input, used
        // to determine what direction the player chooses
        private static readonly List<ConsoleKey> inputCol
            = new List<ConsoleKey>();

        /// <summary>
        /// Sets Dir and LastDir to Direction.None values and takes everything
        /// saved in the inputCol BlockingCollection, effectively resetting any
        /// and all input given by the player up to that point.
        /// </summary>
        public static void ResetInput()
        {
            inputCol.Clear();
        }

        public static bool GetKey(ConsoleKey key)
        {
            if (inputCol.Contains(key))
            {
                inputCol.Remove(key);
                return true;
            }
            return false;
        }

        ///// <summary>
        ///// Processes the ConsoleKeys given to the inputCol BlockingCollection
        ///// and assigns a direction to Dir based on which key was pressed,
        ///// assigning the LastDir to Dir if not ConsoleKey can be taken from
        ///// inputCol.
        ///// </summary>
        //public void ProcessInput()
        //{
        //    // if statement that checks if it is possible to take an element
        //    // from inputCol
        //    if (inputCol.TryTake(out ConsoleKey key))
        //    {
        //        // Swithes the direction according to WASD or arrow keys
        //        Key = key;
        //    }
        //    // else statement that assigns LastDir's value to Dir if no
        //    // element can be taken from inputCol
        //    else
        //    {
        //        Key = ConsoleKey.NoName;
        //    }
        //}

        /// <summary>
        /// Public method to be ran in it's own thread, constantly accepting
        /// input from the player and adding the ConsoleKey to the inputCol
        /// BlockingCollection, to be processed by the ProcessInput method.
        /// </summary>
        public static void ReadKeys()
        {
            // while loop that runs as long as the IsRunning property is true
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    // Assigns a ConsoleKey value, returned from
                    // Console.Readkey().Key to variable key
                    inputCol.Add(Console.ReadKey(true).Key);
                }
            }
        }
    }
}