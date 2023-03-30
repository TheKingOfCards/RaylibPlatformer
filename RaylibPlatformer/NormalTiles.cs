using System;
using Raylib_cs;
using System.Numerics;



public class NormalTiles
{
    public Rectangle rect = new Rectangle(0, 0, 60, 60);    

    public NormalTiles(int x, int y)
    {
        rect.x = x * 60;
        rect.y = y * 60;
    }
}
