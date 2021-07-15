using BTB.Rendering;
using System;
using System.Collections.Generic;
using System.IO;

namespace BTB.Utility
{
    public static class MeshImporter
    {
        public static Mesh GetMesh(string file)
        {
            file = @"C:\Users\andre\Desktop\" + file;

            if (!File.Exists(file))
            {
                throw new Exception("File not found");
            }
            else
            {
                List<Vector3> vertexList = new List<Vector3>();
                List<Triangle> triList = new List<Triangle>();

                using StreamReader sr = new StreamReader(file);

                while (!sr.EndOfStream)
                {
                    //string next = sr.ReadToEnd();
                    string line = sr.ReadLine();

                    if (line.Length > 1 && line[0] == 'v' && line[1] == ' ')
                    {
                        string[] vertexes = line.Split(' ');

                        float x = float.Parse(vertexes[1]);
                        float y = float.Parse(vertexes[2]);
                        float z = float.Parse(vertexes[3]);

                        vertexList.Add(new Vector3(x, y, z));
                    }
                }
                sr.DiscardBufferedData();
                sr.BaseStream.Seek(0, SeekOrigin.Begin);

                while (!sr.EndOfStream)
                {
                    //string next = sr.ReadToEnd();
                    string line = sr.ReadLine();

                    if (line.Length > 1 && line[0] == 'f' && line[1] == ' ')
                    {
                        string[] vertexes = line.Split(' ');

                        int x = int.Parse(vertexes[1].Split('/')[0]) - 1;
                        int y = int.Parse(vertexes[2].Split('/')[0]) - 1;
                        int z = int.Parse(vertexes[3].Split('/')[0]) - 1;

                        triList.Add(new Triangle(vertexList[x], vertexList[y], vertexList[z]));
                    }
                }
                return new Mesh(triList.ToArray());
            }
        }
    }
}