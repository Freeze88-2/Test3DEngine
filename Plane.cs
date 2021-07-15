using System;
using System.Collections.Generic;
using System.Text;
using BTB.Engine;
using BTB.Rendering;
using BTB.Utility;

namespace BTB
{
    public class Plane : IGameObject
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Mesh MeshVisuals { get; }
        public Pixel Visuals { get; private set; }

        public Plane()
        {
            Triangle[] tris = new Triangle[2]
            {
                  new Triangle(new Vector3(1.0f, 0.0f, 1.0f), new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 0.0f, 0.0f)) ,
                  new Triangle(new Vector3(1.0f, 0.0f, 1.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f)) 
            };

            Visuals = new Pixel('.', ConsoleColor.Blue);
            MeshVisuals = new Mesh(tris);
            Position = new Vector3(0, 0, 0);
        }
    }
}
