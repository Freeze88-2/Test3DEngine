using System;

namespace BTB.Rendering
{
    public struct Pixel
    {
        public ConsoleColor Color { get; }
        public char Visual { get; set; }
        public float ZDepth { get; set; }

        public Pixel(char visual, ConsoleColor color, float depth = float.NegativeInfinity)
        {
            Visual = visual;
            Color = color;
            ZDepth = depth;
        }

        public static bool operator ==(Pixel left, Pixel right)
        {
            return left.Visual == right.Visual && left.Color == right.Color;
        }

        public static bool operator !=(Pixel left, Pixel right)
        {
            return !(left == right);
        }
    }
}