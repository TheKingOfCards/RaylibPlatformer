using System;
using Raylib_cs;



public class Manager
{    
    //Lists that will have objects in the game
    public List<Rectangle> tiles = new();
    public List<Rectangle> noNoTiles = new();
    public Player player;
    public Rectangle goal;
    //New instances of levels class
    public Levels levels;
    public State state = State.startScreen;


    public Manager()
    {
        levels = new(this);
    }

    public void NewLevel()
    {
        tiles.Clear();
        noNoTiles.Clear();
        levels.currentLevel++;
        levels.BuildLevel();
        if(levels.currentLevel == 3)
        {
            state = State.finished;
        }
    }


    public void Draw()
    {
        Raylib.BeginDrawing();

        if(state == State.startScreen)
        {
            Raylib.ClearBackground(Color.BLACK);
            Raylib.DrawText("Welcome Too", 500, 300, 30, Color.PURPLE);
            Raylib.DrawText("ATLE ESCAPES THE MATRIX!!", 370, 350, 30 , Color.PURPLE);
            Raylib.DrawText("Press Enter To Start", 430, 400, 30 , Color.PURPLE);
        }else if(state == State.playing)
        {
            if(levels.currentLevel == 0)
            {
                Raylib.DrawText("Use The <- -> Keys To Move", 550, 300, 30, Color.BLACK);
                Raylib.DrawText("Use The Up Arrow Key To Jump", 530, 350, 30, Color.BLACK);
                Raylib.DrawText("Press It Again To Doublejump", 550, 400, 30, Color.BLACK);
            }
            if(levels.currentLevel == 1)
            {
                Raylib.DrawText("If You Are Touching A Wall \nPress <- -> Keys To Walljump", 150, 500, 30, Color.BLACK);
            }
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

            //Draws the goal
            Raylib.DrawRectangleRec(goal, Color.BEIGE);

            //Draws player
            Raylib.DrawRectangleRec(player.rect, Color.PURPLE);
            // Raylib.DrawRectangleRec(player.groundCheck, Color.WHITE);
            // Raylib.DrawRectangleRec(player.rightCheck, Color.RED);
            // Raylib.DrawRectangleRec(player.leftCheck, Color.BLUE);
            // Raylib.DrawRectangleRec(player.roofCheck, Color.WHITE);
        }else if(state == State.dead)
        {
            Raylib.ClearBackground(Color.BLACK);
            Raylib.DrawText("You Died, Try Again?", 450, 300, 30 , Color.PURPLE);
            Raylib.DrawText("Press Enter", 515, 350, 30 , Color.PURPLE);
        }else if(state == State.finished)
        {
            Raylib.ClearBackground(Color.BLACK);
            Raylib.DrawText("You Have Finished The Game", 400, 350, 30, Color.PURPLE);
            Raylib.DrawText("Great Job", 530, 400, 30, Color.PURPLE);
        }
    
        Raylib.EndDrawing();
    }
    

    public void Playing()
    {
        player.Update();
    }
    public enum State
    {
        startScreen, playing, dead, finished
    }
}