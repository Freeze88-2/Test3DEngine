using BTB.Rendering;
using BTB.Utility;
using System.Threading;
using System;

namespace BTB.Engine
{
    public class TaskScheduler
    {
        private readonly bool taskContinue = true;
        private Thread inputThread;

        public TaskScheduler()
        {
            inputThread = new Thread(Input.ReadKeys)
            {
                Name = "Input only thread"
            };

            inputThread.Start();
        }
        float frames;
        int nFrames;

        public void MainGameLoop()
        {
            IGameObject[] objs = new IGameObject[3];
            objs[0] = new Cube();
            objs[1] = new Pyramid();
            objs[2] = new Plane();

            Camera cam = new Camera(new Vector3(0, 0, -1), new Vector3(0));
            RenderEngine render = new RenderEngine(200, 73, objs, cam);

            long previous = DateTime.Now.Ticks;

            while (taskContinue)
            {
                long current = DateTime.Now.Ticks;
                long elapsed = current - previous;
                previous = current;


                cam.Update(elapsed / 10000f);

                for (int updateN = 0; updateN < objs.Length; updateN++)
                {
                    objs[updateN].Update(elapsed / 10000f);
                }

                render.ConstructObjectVisuals();


                frames += elapsed / 10000f;
                nFrames++;

                if (frames >= 60)
                {
                    frames -= 60f;
                    Console.Title = "FPS: " + nFrames + " elapsed: " + elapsed;
                    nFrames = 0;
                }
            }
        }
    }
}