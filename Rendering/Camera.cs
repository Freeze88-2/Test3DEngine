using BTB.Engine;
using BTB.Utility;
using System;

namespace BTB.Rendering
{
    public class Camera : IGameObject, IRenderingObject
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Forward { get; set; }
        public Vector3 Right => Vector3.CrossProduct(Forward.Normalized, new Vector3(0, 1, 0)).Normalized;
        public Vector3 Up => Vector3.CrossProduct(Forward, Right).Normalized;

        public Camera(Vector3 pos, Vector3 rot)
        {
            Position = pos;
            Rotation = rot;

        }
        
        public void Update(float deltaTime)
        {
            if (Input.GetKey(ConsoleKey.UpArrow))
            {
                Position += Forward.Normalized * 0.05f * deltaTime;
            }
            else if (Input.GetKey(ConsoleKey.DownArrow))
            {
                Position -= Forward.Normalized * 0.05f * deltaTime;
            }
            if (Input.GetKey(ConsoleKey.LeftArrow))
            {
                Position -= Right.Normalized * 0.05f * deltaTime;
            }
            else if (Input.GetKey(ConsoleKey.RightArrow))
            {
                Position += Right.Normalized * 0.05f * deltaTime;
            }
            if (Input.GetKey(ConsoleKey.Spacebar))
            {
                Position += new Vector3(0, 1, 0) * 0.05f * deltaTime;
            }
            else if (Input.GetKey(ConsoleKey.C))
            {
                Position -= new Vector3(0, 1, 0) * 0.05f * deltaTime;
            }
            if (Input.GetKey(ConsoleKey.A))
            {
                Rotation -= new Vector3(0, 1, 0) * 0.05f * deltaTime;
            }
            else if (Input.GetKey(ConsoleKey.D))
            {
                Rotation += new Vector3(0, 1, 0) * 0.05f * deltaTime;
            }
            if (Input.GetKey(ConsoleKey.W))
            {
                Rotation -= new Vector3(1, 0, 0) * 0.05f * deltaTime;
            }
            else if (Input.GetKey(ConsoleKey.S))
            {
                Rotation += new Vector3(1, 0, 0) * 0.05f * deltaTime;
            }
        }
    }
}
