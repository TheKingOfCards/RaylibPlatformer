using System;
using Raylib_cs;



public class Manager
{    //Lists that will have objects in the game
    public List<Rectangle> tiles = new();
    public List<Rectangle> noNoTiles = new();
    public Player player;
    //New instances of levels class
    public Levels levels;


    public Manager()
    {
        levels = new(this);
    }

    public void LevelDone()
    {
        levels.currentLevel++;
        levels.BuildNewLevel();
    }

    public void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);
        Raylib.ClearBackground(Color.DARKGREEN);
        //Draws normal tiles
        foreach(Rectangle ob in tiles)
        {
            Raylib.DrawRectangleRec(ob, Color.BLACK);
        }
        //Draws the tiles that kill you
        foreach(Rectangle ob in noNoTiles)
        {
            Raylib.DrawRectangleRec(ob, Color.RED);
        }
        //Draws player
        Raylib.DrawRectangleRec(player.rect, Color.PURPLE);
        Raylib.DrawRectangleRec(player.groundCheck, Color.WHITE);
        Raylib.DrawRectangleRec(player.rightCheck, Color.RED);
        Raylib.DrawRectangleRec(player.leftCheck, Color.BLUE);

        Raylib.EndDrawing();
    }
    

    public void Playing()
    {
        player.Update();
    }
}