using BTB.Utility;
using System;
using System.Threading;

namespace BTB.Rendering
{
    /// <summary>
    /// Class to draw things on screen
    /// </summary>
    public class RenderEngine
    {
        // Instance of the graphics class
        private readonly Graphics graphics;

        // Matrix transformation to turn object space into screen space
        private Matrix4x4 matrix;

        // The list of objects in the app
        private readonly IGameObject[] objs;

        // width / height
        private readonly float aspectRatio;

        // The main renderer
        private readonly Camera mainCamera;

        // Light the triangles according to a light
        private readonly Lighting lighting;

        private Thread thread;
        /// <summary>
        /// Creates a new instance of the render engine
        /// </summary>
        /// <param name="width"> X size of the window </param>
        /// <param name="height"> Y size of the window </param>
        /// <param name="objs"> All the objects to be rendered </param>
        /// <param name="fov"> Field of view of the camera </param>
        /// <param name="nearplane"> how far it should stop rendering </param>
        /// <param name="farplane"> how close it should stop rendering </param>
        public RenderEngine
            (int width, int height, IGameObject[] objs, Camera cam,
            float fov = 90, float nearplane = 0.1f, float farplane = 100f)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(width, height);

            lighting = new Lighting();
            graphics = new Graphics(width, height);
            matrix = new Matrix4x4(0);

            mainCamera = cam;
            aspectRatio = width / height;
            float fovToRad = 1 / MathF.Tan(fov * 0.5f / 180 * MathF.PI);

            matrix.values[0, 0] = aspectRatio * fovToRad;
            matrix.values[1, 1] = fovToRad;
            matrix.values[2, 2] = farplane / (farplane - nearplane);
            matrix.values[3, 2] = (-farplane * nearplane) / (farplane - nearplane);
            matrix.values[2, 3] = -1.0f;
            matrix.values[3, 3] = 0.0f;

            this.objs = objs;
            thread = new Thread(graphics.Render);
            thread.Start();
        }
        public void ConstructObjectVisuals()
        {
            graphics.buffer.Clear();
            Vector3 up = new Vector3(0, 1, 0);
            Vector3 target = new Vector3(0, 0, 1);

            Matrix4x4 cameraMatrix =
                Matrix4x4.RotateY(mainCamera.Rotation.Y);

            cameraMatrix = Matrix4x4.MultiplyMatrix(cameraMatrix,
                (Matrix4x4.RotateX(mainCamera.Rotation.X)));

            mainCamera.Forward = cameraMatrix.Multiply(target);
            target = mainCamera.Position + mainCamera.Forward;

            Matrix4x4 pointedCamera =
                Matrix4x4.PointAt(mainCamera.Position, target, up);


            Matrix4x4 view = pointedCamera.Inverse();

            for (int c = 0; c < objs.Length; c++)
            {
                Triangle[] tris = objs[c].MeshVisuals.tris;

                for (int i = 0; i < tris.Length; i++)
                {
                    // Creates a new Triangle ready to be displayed
                    Triangle? transformedTri = ProjectIntoView
                        (tris[i], view, objs[c].Position);

                    Triangle projectedTri;

                    if (!transformedTri.HasValue) continue;
                    else projectedTri = transformedTri.Value;

                    // Creates 2 of the lines of the triangle
                    Vector3 vecLine1 =
                        projectedTri.points[1] - projectedTri.points[0];

                    Vector3 vecLine2 =
                        projectedTri.points[2] - projectedTri.points[0];

                    // Creates a normal for the triangle being processed,
                    // it looks at the two lines from the triangle and using
                    // the dot product creates a new one 90 degrees from both
                    Vector3 normal =
                        Vector3.CrossProduct(vecLine1, vecLine2).Normalized;

                    // Finds the forward of the camera according to where it's
                    // looking at
                    Vector3 newforward =
                        view.Multiply(mainCamera.Forward).Normalized;

                    newforward = matrix.Multiply(newforward);
                    newforward *= -1;

                    // Finds the angle between the camera and the normal
                    float angle = Vector3.DotProduct(normal, newforward) /
                        normal.Magnitude * newforward.Magnitude;

                    if (angle < 0)
                    {
                        Pixel pixel = objs[c].Visuals;
                        lighting.GetFaceLightLevel(tris[i], ref pixel);

                        for (int b = 0; b < projectedTri.points.Length; b++)
                        {
                            // Temporary Vector to store the vertex
                            Vector3 vertex = projectedTri.points[b];

                            // Where on the screen should the projected
                            // triangle coordinates be
                            float newX =
                                (1 + vertex.X) * 0.5f * graphics.buffer.XDim;

                            float newY =
                                (1 + vertex.Y) * 0.5f * graphics.buffer.YDim;

                            float newZ = vertex.Z;

                            // Alters the vertex of the triangle
                            projectedTri.points[b] =
                                new Vector3(newX, newY, newZ);
                        }

                        projectedTri.points[0] = new Vector3(projectedTri.points[0].X, projectedTri.points[0].Y, Vector3.Distance(mainCamera.Position, tris[i].points[0] + objs[c].Position));
                        projectedTri.points[1] = new Vector3(projectedTri.points[1].X, projectedTri.points[1].Y, Vector3.Distance(mainCamera.Position, tris[i].points[1] + objs[c].Position));
                        projectedTri.points[2] = new Vector3(projectedTri.points[2].X, projectedTri.points[2].Y, Vector3.Distance(mainCamera.Position, tris[i].points[2] + objs[c].Position));

                        //Console.WriteLine(objs[c].ToString() + " " + projectedTri.points[0]);

                        graphics.DrawTriangle
                            (projectedTri, pixel, DrawMode.WireFrameInvisible);
                    }
                }
            }
            graphics.buffer.Swap();


            for (int x = 0; x < graphics.zBuffer.GetLength(0); x++)
            {
                for (int y = 0; y < graphics.zBuffer.GetLength(1); y++)
                {
                    graphics.zBuffer[x, y] = float.NegativeInfinity;
                }
            }
        }

        private Triangle? ProjectIntoView
            (Triangle tri, Matrix4x4 view, Vector3 position)
        {
            // Finds the vertexes of the triangle and subtracts the
            // position of the object
            Vector3 vSpace0 = tri.points[0] + position;
            Vector3 vSpace1 = tri.points[1] + position;
            Vector3 vSpace2 = tri.points[2] + position;

            // Converts the coordinates into the camera space (moves
            // in perspective according to the camera position
            Vector3 vPerspective0 = view.Multiply(vSpace0);
            Vector3 vPerspective1 = view.Multiply(vSpace1);
            Vector3 vPerspective2 = view.Multiply(vSpace2);

            // Projects the coordinates into screen space (3D to 2D)
            Vector3 vProjected0 = matrix.Multiply(vPerspective0);
            Vector3 vProjected1 = matrix.Multiply(vPerspective1);
            Vector3 vProjected2 = matrix.Multiply(vPerspective2);

            vProjected0 = new Vector3((vProjected0.X / aspectRatio) / 1.5f, vProjected0.Y, vProjected0.Z);
            vProjected1 = new Vector3((vProjected1.X / aspectRatio) / 1.5f, vProjected1.Y, vProjected1.Z);
            vProjected2 = new Vector3((vProjected2.X / aspectRatio) / 1.5f, vProjected2.Y, vProjected2.Z);

            // Checks if the plane is clipping
            if (vProjected0.Z < -1 ||
                vProjected1.Z < -1 ||
                vProjected1.Z < -1 ||
                vProjected0.X < -1.5f ||
                vProjected1.X < -1.5f ||
                vProjected2.X < -1.5f ||
                vProjected0.X > 1.5f ||
                vProjected1.X > 1.5f ||
                vProjected2.X > 1.5f ||
                vProjected0.Y < -1.5f ||
                vProjected1.Y < -1.5f ||
                vProjected2.Y < -1.5f ||
                vProjected0.Y > 1.5f ||
                vProjected1.Y > 1.5f ||
                vProjected2.Y > 1.5f)
            {
                return null;
            }

            // Creates a new Triangle ready to be displayed
            return new Triangle(vProjected0, vProjected1, vProjected2);
        }
    }
}