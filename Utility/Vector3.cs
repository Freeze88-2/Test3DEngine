using System;

namespace BTB.Utility
{
    public struct Vector3
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public float Magnitude => Lenght();
        public Vector3 Normalized => Normalize();

        public Vector3 Absolute => AbsVector();

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(Vector2 vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = 0;
        }

        public Vector3(int value = 0)
        {
            X = value;
            Y = value;
            Z = value;
        }

        private Vector3 Normalize()
        {
            if (Lenght() <= 0)
            {
                return this;
            }
            return this / Lenght();
        }

        private float Lenght()
        {
            return (float)Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
        }
        
        public static float Distance(Vector3 right, Vector3 left)
        {
            return MathF.Sqrt(
                ((right.X - left.X) * (right.X - left.X)) +
                ((right.Y - left.Y) * (right.Y - left.Y)) +
                ((right.Z - left.Z) * (right.Z - left.Z)));
        }

        public static float DotProduct(Vector3 right, Vector3 left)
        {
            return ((right.X * left.X) + (right.Y * left.Y) + (right.Z * left.Z));
        }

        public static Vector3 CrossProduct(Vector3 right, Vector3 left)
        {
            float c1 = right.Y * left.Z - right.Z * left.Y;
            float c2 = right.Z * left.X - right.X * left.Z;
            float c3 = right.X * left.Y - right.Y * left.X;

            return new Vector3(c1, c2, c3);
        }

        private Vector3 AbsVector()
        {
            return new Vector3((int)Math.Round(X), (int)Math.Round(Y), (int)Math.Round(Z));
        }

        public static Vector3 operator +(Vector3 right, Vector3 left)
        {
            return new Vector3(right.X + left.X, right.Y + left.Y, right.Z + left.Z);
        }

        public static Vector3 operator +(Vector3 right, Vector2 left)
        {
            return new Vector3(right.X + left.X, right.Y + left.Y, right.Z + 0);
        }

        public static Vector3 operator -(Vector3 right, Vector3 left)
        {
            return new Vector3(right.X - left.X, right.Y - left.Y, right.Z - left.Z);
        }

        public static Vector3 operator /(Vector3 right, float value)
        {
            return new Vector3(right.X / value, right.Y / value, right.Z / value);
        }

        public static Vector3 operator /(Vector3 right, Vector3 left)
        {
            return new Vector3(right.X / left.X, right.Y / left.Y, right.Z / left.Z);
        }

        public static Vector3 operator *(Vector3 right, float value)
        {
            return new Vector3(right.X * value, right.Y * value, right.Z * value);
        }

        public static Vector3 operator *(Vector3 right, Vector3 left)
        {
            return new Vector3(right.X * left.X, right.Y * left.Y, right.Z * left.Z);
        }

        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return (right.X == left.X && right.Y == left.Y && right.Z == left.Z);
        }

        public static bool operator !=(Vector3 right, Vector3 left)
        {
            return !(right == left);
        }

        public override string ToString()
        {
            return $"({X},{Y},{Z})";
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