using System;
using Raylib_cs;



public class Manager
{    //A list tha will have every object in the game
    public List<Rectangle> gameObjects = new();
    //New instances of every class
    public Player player = new();
    public DeathTiles deathTiles = new();
    public Goal goal = new();
    public Levels levels;
    public GameObject gameObject = new();


    public Manager()
    {
        levels = new(this);
    }


    public void Playing()
    {

    }


    public void Draw()
    {
        levels.BuildNewLevel();
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.DARKGREEN);
        foreach(Rectangle ob in gameObjects)
        {
            Raylib.DrawRectangleRec(ob, Color.BLACK);
        }

        Raylib.EndDrawing();
    }
}