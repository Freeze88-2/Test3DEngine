namespace BTB.Rendering
{
    public class Mesh
    {
        public readonly Triangle[] tris;

        public Mesh(Triangle[] triangles)
        {
            tris = triangles;
        }
    }
}