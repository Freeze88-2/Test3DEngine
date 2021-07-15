using BTB.Rendering;
using BTB.Utility;
using System;

namespace BTB
{
    public class Mask : IGameObject
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Mesh MeshVisuals { get; }
        public Pixel Visuals { get; private set; }

        float timer = 0;
        public Mask()
        {
            Visuals = new Pixel('x', ConsoleColor.DarkRed);
            MeshVisuals = MeshImporter.GetMesh("Residential Buildings 001.obj");
            Position = new Vector3(0, 0, 0);
        }

        //public void Update()
        //{
        //    timer+= 0.02f;
        //    Position = new Vector3(Position.X, -12 + (MathF.Sin(timer)), Position.Z);
        //}
    }
}