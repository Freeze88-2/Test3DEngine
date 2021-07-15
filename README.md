# BTB

3D engine created only with console, using C#.

# Objective

The main objective of this project was merely to challenge myself to develop
the rendering part of a game engine, implementing things such as culling,
removing unseen faces and view matrix multiplications to show depth.

# How to use

Simply clone this repository and open the `.sln`.  

To make anything with it create a class that inherits from `IGameObject`.  
In the constructor manually create a `Triangle` array, or use the
`MeshImporter` providing the path to the `.Obj` file to get a mesh from a model.

```cs
public Pyramid()
{
    Triangle[] tris = new Triangle[]
    {
        // BOTTOM
        new Triangle(new Vector3(0.0f, 1.0f, 0.0f),  
        new Vector3(0.0f, 1.0f, 1.0f),   
        new Vector3(1.0f, 1.0f, 1.0f)) ,  

        new Triangle(new Vector3(0.0f, 1.0f, 0.0f),   
        new Vector3(1.0f, 1.0f, 1.0f),  
        new Vector3(1.0f, 1.0f, 0.0f)) ,

        // FRONT
        new Triangle(new Vector3(0.0f, 1.0f, 0.0f),  
        new Vector3(1.0f, 1.0f, 0.0f),    
        new Vector3(0.5f, 0.0f, 0.5f)) ,

        // LEFT
        new Triangle(new Vector3(0.0f, 1.0f, 1.0f),    
        new Vector3(0.0f, 1.0f, 0.0f),    
        new Vector3(0.5f, 0.0f, 0.5f)) ,

        // RIGHT
        new Triangle(new Vector3(1.0f, 1.0f, 0.0f),    
        new Vector3(1.0f, 1.0f, 1.0f),    
        new Vector3(0.5f, 0.0f, 0.5f)) ,

        // BACK
        new Triangle(new Vector3(0.0f, 1.0f, 1.0f),    
        new Vector3(0.5f, 0.0f, 0.5f),    
        new Vector3(1.0f, 1.0f, 1.0f))
    };

    Visuals = new Pixel('x', ConsoleColor.Red);
    MeshVisuals = new Mesh(tris); // Or MeshImporter.GetMesh(path);
    Position = new Vector3(0, 0, 5);
}
```

Finally assign the mesh received to the `MeshVisuals` property or create a new
`Mesh` with the `Triangle` array created.


In order to see the object in the world, the object must be added to the 
`IGameObject` array on the `TaskScheduler` class.

```cs
public void MainGameLoop()
{
    IGameObject[] objs = new IGameObject[3];
    objs[0] = new Cube();
    objs[1] = new Pyramid();
    objs[2] = new Plane();

    (...)
}
```

# Creator

This project was developed by André Vitorino (andrevitorino.dev@gmail.com).