using System;

namespace BTB.Utility
{
    public struct Matrix4x4
    {
        public float[,] values;

        public Matrix4x4(int value)
        {
            values = new float[4, 4]
            {
                    { value, value, value, value },
                    { value, value, value, value },
                    { value, value, value, value },
                    { value, value, value, value },
            };
        }

        public Vector3 Multiply(Vector3 vector)
        {
            float x = vector.X * values[0, 0] + vector.Y * values[1, 0] + vector.Z * values[2, 0] + values[3, 0];
            float y = vector.X * values[0, 1] + vector.Y * values[1, 1] + vector.Z * values[2, 1] + values[3, 1];
            float z = vector.X * values[0, 2] + vector.Y * values[1, 2] + vector.Z * values[2, 2] + values[3, 2];
            float w = vector.X * values[0, 3] + vector.Y * values[1, 3] + vector.Z * values[2, 3] + values[3, 3];

            if (w != 0.0f)
            {
                x /= w;
                y /= w;
                z /= w;
            }

            return new Vector3(x, y, z);
        }

        public static Matrix4x4 MultiplyMatrix(Matrix4x4 m1, Matrix4x4 m2)
        {
            Matrix4x4 matrix = new Matrix4x4(0);
            for (int c = 0; c < 4; c++)
            {
                for (int r = 0; r < 4; r++)
                {
                    matrix.values[r, c] = m1.values[r, 0] * m2.values[0, c] + m1.values[r, 1] * m2.values[1, c] + m1.values[r, 2] * m2.values[2, c] + m1.values[r, 3] * m2.values[3, c];
                }
            }

            return matrix;
        }

        public static Matrix4x4 PointAt(Vector3 pos, Vector3 target, Vector3 up)
        {
            // Calculate new forward direction
            Vector3 newForward = (target - pos).Normalized;

            // Calculate new Up direction
            Vector3 a = newForward * Vector3.DotProduct(up, newForward);
            Vector3 newUp = up - a;
            newUp = newUp.Normalized;

            // New Right direction is easy, its just cross product
            Vector3 newRight = Vector3.CrossProduct(newUp, newForward);

            // Construct Dimensioning and Translation Matrix
            Matrix4x4 matrix = new Matrix4x4(0);
            matrix.values[0, 0] = newRight.X; matrix.values[0, 1] = newRight.Y; matrix.values[0, 2] = newRight.Z; matrix.values[0, 3] = 0.0f;
            matrix.values[1, 0] = newUp.X; matrix.values[1, 1] = newUp.Y; matrix.values[1, 2] = newUp.Z; matrix.values[1, 3] = 0.0f;
            matrix.values[2, 0] = newForward.X; matrix.values[2, 1] = newForward.Y; matrix.values[2, 2] = newForward.Z; matrix.values[2, 3] = 0.0f;
            matrix.values[3, 0] = pos.X; matrix.values[3, 1] = pos.Y; matrix.values[3, 2] = pos.Z; matrix.values[3, 3] = 1.0f;
            return matrix;
        }

        public Matrix4x4 Inverse() // Only for Rotation/Translation Matrices
        {
            Matrix4x4 matrix = new Matrix4x4(0);

            matrix.values[0, 0] = values[0, 0];
            matrix.values[0, 1] = values[1, 0];
            matrix.values[0, 2] = values[2, 0];
            matrix.values[0, 3] = 0.0f;

            matrix.values[1, 0] = values[0, 1];
            matrix.values[1, 1] = values[1, 1];
            matrix.values[1, 2] = values[2, 1];
            matrix.values[1, 3] = 0.0f;

            matrix.values[2, 0] = values[0, 2];
            matrix.values[2, 1] = values[1, 2];
            matrix.values[2, 2] = values[2, 2];
            matrix.values[2, 3] = 0.0f;

            matrix.values[3, 0] = -(values[3, 0] * matrix.values[0, 0] + values[3, 1] * matrix.values[1, 0] + values[3, 2] * matrix.values[2, 0]);
            matrix.values[3, 1] = -(values[3, 0] * matrix.values[0, 1] + values[3, 1] * matrix.values[1, 1] + values[3, 2] * matrix.values[2, 1]);
            matrix.values[3, 2] = -(values[3, 0] * matrix.values[0, 2] + values[3, 1] * matrix.values[1, 2] + values[3, 2] * matrix.values[2, 2]);
            matrix.values[3, 3] = 1.0f;
            return matrix;
        }

        public static Matrix4x4 RotateX(float fAngleRad)
        {
            Matrix4x4 matrix = new Matrix4x4(0);
            matrix.values[0, 0] = 1.0f;
            matrix.values[1, 1] = MathF.Cos(fAngleRad);
            matrix.values[1, 2] = MathF.Sin(fAngleRad);
            matrix.values[2, 1] = -MathF.Sin(fAngleRad);
            matrix.values[2, 2] = MathF.Cos(fAngleRad);
            matrix.values[3, 3] = 1.0f;
            return matrix;
        }

        public static Matrix4x4 RotateY(float fAngleRad)
        {
            Matrix4x4 matrix = new Matrix4x4(0);
            matrix.values[0, 0] = MathF.Cos(fAngleRad);
            matrix.values[0, 2] = MathF.Sin(fAngleRad);
            matrix.values[2, 0] = -MathF.Sin(fAngleRad);
            matrix.values[1, 1] = 1.0f;
            matrix.values[2, 2] = MathF.Cos(fAngleRad);
            matrix.values[3, 3] = 1.0f;
            return matrix;
        }

        public static Matrix4x4 RotateZ(float fAngleRad)
        {
            Matrix4x4 matrix = new Matrix4x4(0);
            matrix.values[0, 0] = MathF.Cos(fAngleRad);
            matrix.values[0, 1] = MathF.Sin(fAngleRad);
            matrix.values[1, 0] = -MathF.Sin(fAngleRad);
            matrix.values[1, 1] = MathF.Cos(fAngleRad);
            matrix.values[2, 2] = 1.0f;
            matrix.values[3, 3] = 1.0f;
            return matrix;
        }

        public Matrix4x4 Translate(Vector3 vector)
        {
            values[0, 0] = 1.0f;
            values[1, 1] = 1.0f;
            values[2, 2] = 1.0f;
            values[3, 3] = 1.0f;
            values[3, 0] = vector.X;
            values[3, 1] = vector.Y;
            values[3, 2] = vector.Z;
            return this;
        }
    }
}