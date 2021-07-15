using System;

namespace BTB.Utility
{
    public struct Vector2
    {
        public float X { get; }
        public float Y { get; }

        public float Magnitude => Lenght();
        public Vector2 Normalized => Normalize();

        public Vector2 Absolute => AbsVector();

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2(Vector3 vector)
        {
            X = vector.X;
            Y = vector.Y;
        }

        public Vector2(int value = 0)
        {
            X = value;
            Y = value;
        }

        private Vector2 Normalize()
        {
            if (Lenght() <= 0)
            {
                return new Vector2(0);
            }
            return this / Lenght();
        }

        private Vector2 AbsVector()
        {
            return new Vector2((int)Math.Round(X), (int)Math.Round(Y));
        }

        private float Lenght()
        {
            return (float)Math.Sqrt((X * X) + (Y * Y));
        }

        public static float Distance(Vector2 right, Vector2 left)
        {
            return MathF.Sqrt(
                ((right.X - left.X) * (right.X - left.X)) +
                ((right.Y - left.Y) * (right.Y - left.Y)));
        }

        public static Vector2 operator +(Vector2 right, Vector2 left)
        {
            return new Vector2(right.X + left.X, right.Y + left.Y);
        }

        public static Vector2 operator +(Vector2 right, Vector3 left)
        {
            return new Vector2(right.X + left.X, right.Y + left.Y);
        }

        public static Vector2 operator -(Vector2 right, Vector2 left)
        {
            return new Vector2(right.X - left.X, right.Y - left.Y);
        }

        public static Vector2 operator /(Vector2 right, float value)
        {
            return new Vector2(right.X / value, right.Y / value);
        }

        public static Vector2 operator /(Vector2 right, Vector2 left)
        {
            return new Vector2(right.X / left.X, right.Y / left.Y);
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return (right.X == left.X && right.Y == left.Y);
        }

        public static bool operator !=(Vector2 right, Vector2 left)
        {
            return !(right == left);
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}