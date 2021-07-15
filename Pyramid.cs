using BTB.Rendering;
using BTB.Utility;
using System;

namespace BTB
{
    public class Pyramid : IGameObject
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Mesh MeshVisuals { get; }
        public Pixel Visuals { get; private set; }

        float timer = 0;
        public Pyramid()
        {
            Triangle[] tris = new Triangle[]
            {
		        // BOTTOM
		        new Triangle(new Vector3(0.0f, 1.0f, 0.0f),    new Vector3(0.0f, 1.0f, 1.0f),    new Vector3(1.0f, 1.0f, 1.0f)) ,
                new Triangle(new Vector3(0.0f, 1.0f, 0.0f),    new Vector3(1.0f, 1.0f, 1.0f),    new Vector3(1.0f, 1.0f, 0.0f)) ,

		        // FRONT
		        new Triangle(new Vector3(0.0f, 1.0f, 0.0f),    new Vector3(1.0f, 1.0f, 0.0f),    new Vector3(0.5f, 0.0f, 0.5f)) ,

                // LEFT
                new Triangle(new Vector3(0.0f, 1.0f, 1.0f),    new Vector3(0.0f, 1.0f, 0.0f),    new Vector3(0.5f, 0.0f, 0.5f)) ,

                // RIGHT
                new Triangle(new Vector3(1.0f, 1.0f, 0.0f),    new Vector3(1.0f, 1.0f, 1.0f),    new Vector3(0.5f, 0.0f, 0.5f)) ,

                // BACK
                new Triangle(new Vector3(0.0f, 1.0f, 1.0f),    new Vector3(0.5f, 0.0f, 0.5f),    new Vector3(1.0f, 1.0f, 1.0f))
            };

            Visuals = new Pixel('x', ConsoleColor.Red);
            MeshVisuals = new Mesh(tris);
            Position = new Vector3(0, 0, 5);
        }
        public void Update(float deltaTime)
        {
            timer += 0.001f * deltaTime;
            Position = new Vector3((MathF.Cos(timer)), (MathF.Sin(timer)), (MathF.Cos(timer))) * 1.3f;
        }
    }
}