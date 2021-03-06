using System;

namespace BTB.Rendering
{
    /// <summary>
    /// DoubleBuffer2D of generic type.
    /// Responsible for containing the current and next frames in bidimensional
    /// arrays of generic type, being able to swap the next for the current and
    /// clearing the next frame to be written again.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoubleBuffer<T>
    {
        // Stores the current frame in a generic type bidimensional array
        private T[,] current;

        // Stores the next frame in a generic type bidimensional array
        private T[,] next;

        // Dimension of frame in X axis
        public int XDim => next.GetLength(0);

        // Dimension of frame in Y axis
        public int YDim => next.GetLength(1);

        // Indexer to be used in a DoubleBuffer2D instance
        public T this[int x, int y]
        {
            get => current[x, y];
            set => next[x, y] = value;
        }

        /// <summary>
        /// Clears the next frame, to be written over again
        /// </summary>
        public void Clear()
        {
            Array.Clear(next, 0, XDim * YDim - 1);
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="x">X size of the buffer</param>
        /// <param name="y">Y size of the buffer</param>
        public DoubleBuffer(int x, int y)
        {
            // Sets 'current' as a new bidimensional array with specified size
            current = new T[x, y];
            // Sets 'next' as a new bidimensional array with specified size
            next = new T[x, y];
            // Calls method 'Clear'
            Clear();
        }

        /// <summary>
        /// Swaps the current frame for the next frame
        /// </summary>
        public void Swap()
        {
            // Sets new auxiliary frame with the value of the 'current' frame
            T[,] auxFrame = current;
            // Sets 'current' frame with the value of the 'next' frame
            current = next;
            // Sets 'next' frame with the value of 'auxFrame'
            next = auxFrame;
        }
    }
}