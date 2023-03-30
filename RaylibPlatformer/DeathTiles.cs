using System;
using Raylib_cs;
using System.Numerics;


public class DeathTiles
{
    public Rectangle rect = new Rectangle(1 , 1, 60, 60); 

    public DeathTiles(int x, int y)
    {
        rect.x = x * 60;
        rect.y = y * 60;
    }
}
