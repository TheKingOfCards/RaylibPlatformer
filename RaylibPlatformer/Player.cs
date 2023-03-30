using System;
using Raylib_cs;
using System.Numerics;


public class Player
{
    Manager m;
    int moveSpeed = 10;
    int jumpForce = 20;
    int fallSpeed = 0;
    int doubleJump = 1;
    int maxWalljumpX = 20;
    int wallJumpForceX = 10;
    int wallJumpForceY = 20;
    int wallDir = 0; 
    public Rectangle rect = new Rectangle(0, 400, 60, 60);
    public Rectangle groundCheck = new Rectangle(0, 0, 58, 10);
    public Rectangle rightCheck = new Rectangle(0, 0, 30, 50);
    public Rectangle leftCheck = new Rectangle(0, 0, 30, 50);
    bool isGrounded = false;
    bool isRight = false;
    bool isLeft = false;
    bool hasWalljumped = false;
    bool stopMovement = false;


    public Player(Manager m, int x, int y)
    {
        rect.x = x * 60;
        rect.y = y * 60;
        this.m = m;
    }

    public void Update()
    {
        //debug only
        if (Raylib.IsKeyDown(KeyboardKey.KEY_R))
        {
            moveSpeed = 0;
            rect.x = Raylib.GetScreenWidth()/2;
            rect.y = Raylib.GetScreenHeight()/2;
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
            if (Raylib.CheckCollisionRecs(leftCheck, ob))
            {
                isLeft = true;
            }
            if (Raylib.CheckCollisionRecs(rightCheck, ob))
            {
                isRight = true;
            }
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
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT) && isRight == true && isGrounded == false && hasWalljumped == false)
            {
                wallDir = -1;
                wallJumpForceX = 10;
                fallSpeed = -wallJumpForceY;
                hasWalljumped = true;
                stopMovement = true;
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT) && isLeft == true && isGrounded == false && hasWalljumped == false)
            {
                wallDir = 1;
                wallJumpForceX = 10;
                fallSpeed = -wallJumpForceY;
                hasWalljumped = true;
                stopMovement = true;
            }
        }else{
            if(wallJumpForceX != maxWalljumpX)
            {
                wallJumpForceX++;
                rect.x += wallJumpForceX * wallDir;
            }else
            {
                stopMovement = false;
            }
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
            hasWalljumped = false;
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
        // Console.WriteLine(fallSpeed);
        rect.y += fallSpeed;

        PlacesChecks();
    }


    public void PlacesChecks()
    {
        //Places the checking rectangles on the righ pos
        groundCheck.x = rect.x + 1;
        groundCheck.y = rect.y + 51;

        rightCheck.x = rect.x + 40;
        rightCheck.y = rect.y + 5;

        leftCheck.x = rect.x - 10;
        leftCheck.y = rect.y + 5;
    }
}
