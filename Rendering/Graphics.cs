using BTB.Utility;
using System;

namespace BTB.Rendering
{
    public class Graphics
    {
        public readonly DoubleBuffer<Pixel> buffer;
        public readonly float[,] zBuffer;

        public Graphics(int width, int height)
        {
            buffer = new DoubleBuffer<Pixel>(width, height);
            zBuffer = new float[width, height];
        }

        public void DrawPoint(Vector3 position, Pixel pixel)
        {
            if (IsPixelInBounds(position.Absolute))
            {
                int x = (int)position.Absolute.X;
                int y = (int)position.Absolute.Y;
                buffer[x, y] = pixel;
            }
        }

        public void DrawLine(Vector3 start, Vector3 end, Pixel pixel)
        {
            Vector3 direction = (end - start).Normalized;

            float dis = Vector3.Distance(end, start);

            CheckZBuffer(start.Absolute, pixel, start.Z);

            while (dis > 1f && MathF.Abs(dis) < 1000f)
            {
                dis = Vector3.Distance(end, start);
                start += direction / 2;

                CheckZBuffer(start.Absolute, pixel, start.Z);
            }

            CheckZBuffer(end.Absolute, pixel, end.Z);
        }

        private void CheckZBuffer(Vector3 vector, Pixel pixel, float depth)
        {
            if (IsPixelInBounds(vector))
            {
                int x = (int)vector.X;
                int y = (int)vector.Y;

                if (zBuffer[x, y] >= depth ||
                    zBuffer[x, y] == float.NegativeInfinity)
                {
                    pixel.ZDepth = depth;
                    DrawPoint(vector, pixel);
                    zBuffer[x, y] = depth;
                }
            }
        }
        public bool IsPixelInBounds(Vector3 vector)
        {
            return vector.X > -1 && vector.X < buffer.XDim &&
                   vector.Y > -1 && vector.Y < buffer.YDim;
        }

        public void DrawTriangle(Triangle tri, Pixel pixel, DrawMode mode)
        {
            Pixel p = mode == DrawMode.WireFrameInvisible ?
                pixel : new Pixel('?', ConsoleColor.White);

            if (mode != DrawMode.WireFrameOnly)
            {
                RasterTriangle(tri, pixel);
            }
            DrawLine(tri.points[0], tri.points[1], p);
            DrawLine(tri.points[1], tri.points[2], p);
            DrawLine(tri.points[2], tri.points[0], p);
        }

        private Triangle SortByAscending(ref Triangle tri)
        {
            Vector3 lowest = tri.points[0];
            Vector3 highest = new Vector3(-100);
            Vector3 middle = new Vector3(100);

            for (int i = 1; i < tri.points.Length; i++)
            {
                if (lowest.Y > tri.points[i].Y)
                {
                    lowest = tri.points[i];
                }
            }

            for (int i = 0; i < tri.points.Length; i++)
            {
                if (highest.Y < tri.points[i].Y && lowest != tri.points[i])
                {
                    highest = tri.points[i];
                }
            }

            for (int i = 0; i < tri.points.Length; i++)
            {
                if (lowest != tri.points[i] && highest != tri.points[i])
                {
                    middle = tri.points[i];
                }
            }
            tri.points[0] = lowest;
            tri.points[1] = middle;
            tri.points[2] = highest;

            return tri;
        }

        private void FillBottomFlatTriangle(Triangle tri, Pixel p)
        {
            float invslope1 = (tri.points[1].X - tri.points[0].X) / (tri.points[1].Y - tri.points[0].Y);
            float invslope2 = (tri.points[2].X - tri.points[0].X) / (tri.points[2].Y - tri.points[0].Y);

            float zadd1 = (tri.points[0].Z - tri.points[1].Z) / Vector2.Distance(new Vector2(tri.points[1]), new Vector2(tri.points[0]));
            float zadd2 = (tri.points[0].Z - tri.points[2].Z) / Vector2.Distance(new Vector2(tri.points[2]), new Vector2(tri.points[0]));

            //Console.WriteLine(zadd1);

            float curz1 = tri.points[0].Z;
            float curz2 = tri.points[0].Z;

            float curx1 = tri.points[0].X;
            float curx2 = tri.points[0].X;

            for (int scanlineY = (int)Math.Round(tri.points[0].Y); scanlineY <= tri.points[1].Y; scanlineY++)
            {
                Vector3 start = new Vector3(curx1, scanlineY, curz1);
                Vector3 end = new Vector3(curx2, scanlineY, curz2);

                DrawLine(start, end, p);

                curz1 -= zadd1;
                curz2 -= zadd2;

                curx1 += invslope1;
                curx2 += invslope2;
            }
        }

        private void FillTopFlatTriangle(Triangle tri, Pixel p)
        {
            float invslope1 = (tri.points[2].X - tri.points[0].X) / (tri.points[2].Y - tri.points[0].Y);
            float invslope2 = (tri.points[2].X - tri.points[1].X) / (tri.points[2].Y - tri.points[1].Y);

            float zadd1 = (tri.points[2].Z - tri.points[0].Z) / Vector2.Distance(new Vector2(tri.points[0]), new Vector2(tri.points[2]));
            float zadd2 = (tri.points[2].Z - tri.points[1].Z) / Vector2.Distance(new Vector2(tri.points[1]), new Vector2(tri.points[2]));

            float curz1 = tri.points[2].Z;
            float curz2 = tri.points[2].Z;

            float curx1 = tri.points[2].X;
            float curx2 = tri.points[2].X;

            for (int scanlineY = (int)Math.Round(tri.points[2].Y); scanlineY > tri.points[0].Y; scanlineY--)
            {
                Vector3 start = new Vector3(curx1, scanlineY, curz1 + 0.1f);
                Vector3 end = new Vector3(curx2, scanlineY, curz2 + 0.1f);

                DrawLine(start, end, p);

                curz1 -= zadd1;
                curz2 -= zadd2;

                curx1 -= invslope1;
                curx2 -= invslope2;
            }
        }

        private void RasterTriangle(Triangle tri, Pixel p)
        {
            // at first sort the three vertices's by y-coordinate ascending so tri.points[0] is the topmost vertice
            SortByAscending(ref tri);

            // here we know that tri.points[0].y <= tri.points[1].y <= tri.points[2].y
            // heck for trivial case of bottom-flat triangle
            if (tri.points[1].Y == tri.points[2].Y)
            {
                FillBottomFlatTriangle(tri, p);
            }
            // check for trivial case of top-flat triangle
            else if (tri.points[0].Y == tri.points[1].Y)
            {
                FillTopFlatTriangle(tri, p);
            }
            else
            {
                float x =
                    (tri.points[0].X +
                    (tri.points[1].Y - tri.points[0].Y) /
                    (tri.points[2].Y - tri.points[0].Y) *
                    (tri.points[2].X - tri.points[0].X));

                float y = tri.points[1].Y;

                float z = tri.points[2].Z;

                // general case - split the triangle in a top flat and bottom-flat one
                Vector3 v4 = new Vector3(x, y, z);
                FillBottomFlatTriangle(new Triangle(tri.points[0], tri.points[1], v4), p);

                z = tri.points[0].Z;
                v4 = new Vector3(x, y, z);
                FillTopFlatTriangle(new Triangle(tri.points[1], v4, tri.points[2]), p);
            }
        }
        public void Render()
        {
            while (true)
            {
                string line = "";
                for (int y = 0; y < buffer.YDim; y++)
                {
                    for (int x = 0; x < buffer.XDim; x++)
                    {
                        if (buffer[x, y] == default)
                        {
                            line += ' ';
                        }
                        else
                        {
                            if (buffer[x, y].Color != Console.ForegroundColor)
                            {
                                Console.Write(line);
                                Console.ForegroundColor = buffer[x, y].Color;
                                line = "";
                            }
                            line += buffer[x, y].Visual;
                        }
                    }
                    line += '\n';
                }
                Console.Write(line);
                Console.SetCursorPosition(0, 0);

            }
        }
    }
}