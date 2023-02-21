using System;
using Raylib_cs;
using System.Numerics;


public class Player : GameObject
{
    int moveSpeed = 5;
    int jumpForce = 20;
    public Rectangle player = new Rectangle(500,500,60,60);

    public void Movement()
    {
        //Right to left movement
        if(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            player.x += moveSpeed;
        }else if(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            player.x -= moveSpeed;
        }

        //Jumping
        if(Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
        {
            player.y -= jumpForce;
        }
    }


    public void Gravity()
    {
            player.y += 5;   
    }
}
