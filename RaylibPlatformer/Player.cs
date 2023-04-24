using System;
using Raylib_cs;
using System.Numerics;


public class Player
{
    Manager m;
    int moveSpeed = 10;
    int jumpForce = 15;
    int fallSpeed = 0;
    int doubleJump = 1;
    int maxWalljumpX = 20;
    int wallJumpForceX = 0;
    int wallJumpForceY = 17;
    int wallDir = 0; 
    public Rectangle rect = new Rectangle(0, 400, 60, 60);
    public Rectangle groundCheck = new Rectangle(0, 0, 58, 10);
    public Rectangle roofCheck = new Rectangle(0, 0, 58, 10);
    public Rectangle rightCheck = new Rectangle(0, 0, 30, 50);
    public Rectangle leftCheck = new Rectangle(0, 0, 30, 50);
    bool isGrounded = false;
    bool isRight = false;
    bool isLeft = false;
    bool hasWalljumpedRight = false;
    bool hasWalljumpedLeft = false;
    bool stopMovement = false;
    bool shouldDie = false;


    public Player(Manager m, int x, int y)
    {
        rect.x = x * 60;
        rect.y = y * 60;
        this.m = m;
    }

    public void Update()
    {
        //Startscreen code
        if(m.state == Manager.State.startScreen)
        {
            if(Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                m.state = Manager.State.playing;
                m.levels.BuildLevel();
            }
        }

        //Checks if the player is tuouching a normal tile
        isGrounded = false;
        isRight = false;
        isLeft = false;
        foreach (Rectangle ob in m.tiles)
        {
            if (Raylib.CheckCollisionRecs(groundCheck, ob))
            {
                rect.y = ob.y - rect.height;
                isGrounded = true;
            }
            if(Raylib.CheckCollisionRecs(roofCheck,ob))
            {
                rect.y = ob.y + rect.height;
                fallSpeed = 0;
            }
            if (Raylib.CheckCollisionRecs(leftCheck, ob))
            {
                isLeft = true;
            }
            if (Raylib.CheckCollisionRecs(rightCheck, ob))
            {
                isRight = true;
            }
        }
        //Checks if the player is touching a deathtile
        foreach(Rectangle ob in m.noNoTiles)
        {
            if (Raylib.CheckCollisionRecs(rect, ob))
            {
                m.state = Manager.State.dead;
            }
        }
        //Checks if the player is touching the goal
        if(Raylib.CheckCollisionRecs(rect, m.goal))
        {
            m.NewLevel();
        }


        //Stops player from going to the right or left
        if (isRight == true && Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            moveSpeed = 0;
        }
        else if (isLeft == true && Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = 10;
        }


        //Checks if player can walljump
        if (isRight == true || isLeft == true)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT) && isRight == true && isGrounded == false && hasWalljumpedRight == false)
            {
                wallDir = -1;
                wallJumpForceX = 5;
                fallSpeed = -wallJumpForceY;
                hasWalljumpedRight = true;
                hasWalljumpedLeft = false;
                stopMovement = true;
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT) && isLeft == true && isGrounded == false && hasWalljumpedLeft == false)
            {
                wallDir = 1;
                wallJumpForceX = 5;
                fallSpeed = -wallJumpForceY;
                hasWalljumpedLeft = true;
                hasWalljumpedRight = false;
                stopMovement = true;
            }
        }
        if(wallJumpForceX != maxWalljumpX)
        {
            wallJumpForceX++;
            rect.x += wallJumpForceX * wallDir;
        }else
        {
            stopMovement = false;
        }
        


        //Right to left movement
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && stopMovement == false)
        {
            rect.x += moveSpeed;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && stopMovement == false)
        {
            rect.x -= moveSpeed;
        }


        //Creates gravity
        if (isGrounded == true)
        {
            fallSpeed = 0;
            doubleJump = 1;
            hasWalljumpedRight = false;
            hasWalljumpedLeft = false;
            //Jumping
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
            {
                fallSpeed = -jumpForce;
            }
        }
        else
        {
            int maxFallspeed = 30;
            if (fallSpeed != maxFallspeed)
            {
                fallSpeed += 1;
            }
            if (doubleJump == 1 && Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
            {
                fallSpeed = -jumpForce;
                doubleJump--;
            }
        }
        rect.y += fallSpeed;

        PlacesChecks();

        
        //If the player has died
        if(m.state == Manager.State.dead)
        {
            if(Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                m.state = Manager.State.playing;
                m.levels.BuildLevel();
            }
        }
    }


    public void PlacesChecks()
    {
        //Places the checking rectangles on the righ pos
        groundCheck.x = rect.x + 1;
        groundCheck.y = rect.y + 51;

        roofCheck.x = rect.x + 1;
        roofCheck.y = rect.y - 1;

        rightCheck.x = rect.x + 40;
        rightCheck.y = rect.y + 5;

        leftCheck.x = rect.x - 10;
        leftCheck.y = rect.y + 5;
    }
}
