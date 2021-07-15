using BTB.Utility;
using System;

namespace BTB.Rendering
{
    public class Lighting
    {
        private readonly (char, byte)[] lightLevel = new (char, byte)[10] { ('.', 0), (',', 1), (':', 2), ('|', 3), ('I', 4), ('E', 5), ('░', 6), ('▒', 7), ('▓', 7), ('█', 8) };

        private readonly Vector3 globalLight = new Vector3(0, 1, -1f);
        private readonly Vector3 pointLight = new Vector3(0, 1f, 1f);

        public void GetFaceLightLevel(Triangle tri, ref Pixel pixel)
        {
            pixel.Visual = lightLevel[CalculateGlobalLight(tri)].Item1;
            //CalculateGlobalLight(tri);

            //int lightIndexG = 0; 
            //int lightIndexP = 0;

            //if (lightIndexG < 8)
            //{
            //    lightIndexP = CalculatePointLight(tri);
            //}

            //int lightIndex = lightIndexG > lightIndexP ? lightIndexG : lightIndexP;

            //byte current = 0;

            //for (int i = 0; i < lightLevel.Length; i++)
            //{
            //    if (lightLevel[i].Item1 == pixel.Visual)
            //    {
            //        current = lightLevel[i].Item2;
            //        break;
            //    }
            //}
            //if (lightLevel[lightIndex].Item2 > current)
            //{
            //    // Calculates what char to assign the triangle
            //    pixel.Visual = lightLevel[lightIndex].Item1;
            //}
        }
        private int CalculatePointLight(Triangle tri)
        {
            Vector3 triPos = tri.points[0] + tri.points[1] + tri.points[2];

            float dist = Vector3.Distance(triPos, pointLight) / 2;

            // Turns the lightLevel into an index
            return (int)MathF.Round(MathF.Min(
                  dist,8) - 8);

        }
        private int CalculateGlobalLight(Triangle tri)
        {
            // Creates 2 of the lines of the triangle
            Vector3 nLine1 =
                tri.points[1] - tri.points[0];

            Vector3 nLine2 =
                tri.points[2] - tri.points[0];


            // Creates a normal for the triangle being processed,
            // it looks at the two lines from the triangle and using
            // the dot product creates a new one 90 degrees from both
            Vector3 normalN = Vector3.CrossProduct(nLine1, nLine2).Normalized;

            // Calculates how much that face can see the light
            float angleToLight =
                Vector3.DotProduct(normalN, globalLight.Normalized) /
                normalN.Magnitude * globalLight.Normalized.Magnitude;

            int lightIndex = 0;

            if (angleToLight > 0)
            {
                // Turns the lightLevel into an index
                lightIndex = (int)MathF.Round(MathF.Abs(
                    angleToLight * lightLevel.Length - 1));
            }
            return lightIndex;
        }
    }
}
