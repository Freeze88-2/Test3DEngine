using BTB.Utility;

namespace BTB.Rendering
{
    public struct Triangle
    {
        public Vector3[] points;

        public Triangle(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            points = new Vector3[3] { p1, p2, p3 };
        }
    }
}