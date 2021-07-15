using BTB.Rendering;
using BTB.Utility;
using System;

namespace BTB
{
    public class Cube : IGameObject
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Mesh MeshVisuals { get; }
        public Pixel Visuals { get; private set; }


        public Cube()
        {
            Triangle[] tris = new Triangle[12]
            {
                  // SOUTH
                  new Triangle(new Vector3(0.0f, 0.0f, 0.0f),    new Vector3(0.0f, 1.0f, 0.0f),    new Vector3(1.0f, 1.0f, 0.0f)) ,
                  new Triangle(new Vector3(0.0f, 0.0f, 0.0f),    new Vector3(1.0f, 1.0f, 0.0f),    new Vector3(1.0f, 0.0f, 0.0f)) ,

                  // EAST
                  new Triangle(new Vector3(1.0f, 0.0f, 0.0f),    new Vector3(1.0f, 1.0f, 0.0f),    new Vector3(1.0f, 1.0f, 1.0f)) ,
                  new Triangle(new Vector3(1.0f, 0.0f, 0.0f),    new Vector3(1.0f, 1.0f, 1.0f),    new Vector3(1.0f, 0.0f, 1.0f)) ,

                  // NORTH
                  new Triangle(new Vector3(1.0f, 0.0f, 1.0f),    new Vector3(1.0f, 1.0f, 1.0f),    new Vector3(0.0f, 1.0f, 1.0f)) ,
                  new Triangle(new Vector3(1.0f, 0.0f, 1.0f),    new Vector3(0.0f, 1.0f, 1.0f),    new Vector3(0.0f, 0.0f, 1.0f)) ,

                  // WEST
                  new Triangle(new Vector3(0.0f, 0.0f, 1.0f),    new Vector3(0.0f, 1.0f, 1.0f),    new Vector3(0.0f, 1.0f, 0.0f)) ,
                  new Triangle(new Vector3(0.0f, 0.0f, 1.0f),    new Vector3(0.0f, 1.0f, 0.0f),    new Vector3(0.0f, 0.0f, 0.0f)) ,

                  // TOP
                  new Triangle(new Vector3(0.0f, 1.0f, 0.0f),    new Vector3(0.0f, 1.0f, 1.0f),    new Vector3(1.0f, 1.0f, 1.0f)) ,
                  new Triangle(new Vector3(0.0f, 1.0f, 0.0f),    new Vector3(1.0f, 1.0f, 1.0f),    new Vector3(1.0f, 1.0f, 0.0f)) ,

                  // BOTTOM
                  new Triangle(new Vector3(1.0f, 0.0f, 1.0f),    new Vector3(0.0f, 0.0f, 1.0f),    new Vector3(0.0f, 0.0f, 0.0f)) ,
                  new Triangle(new Vector3(1.0f, 0.0f, 1.0f),    new Vector3(0.0f, 0.0f, 0.0f),    new Vector3(1.0f, 0.0f, 0.0f))
            }; 

            Visuals = new Pixel('.', ConsoleColor.White);
            MeshVisuals = new Mesh(tris);
            Position = new Vector3(0,0, 0);
        }
        float timer = 0;
        public void Update(float deltaTime)
        {
            timer += 0.001f * deltaTime;
            Position = new Vector3(0, (MathF.Sin(timer)), 0) * 0.2f;
        }
    }
}