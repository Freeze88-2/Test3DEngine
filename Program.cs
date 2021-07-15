using BTB.Engine;
using System;

namespace BTB
{
    internal class Program
    {
        private static void Main()
        {
            TaskScheduler t = new TaskScheduler();
            t.MainGameLoop();
        }
    }
}