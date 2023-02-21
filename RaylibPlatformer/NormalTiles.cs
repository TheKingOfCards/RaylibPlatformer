using System;
using Raylib_cs;
using System.Numerics;



public class NormalTiles : GameObject
{
    public Rectangle tileRectangle = new Rectangle(0, 0, 60, 60);    

    public NormalTiles(int x, int y)
    {
        tileRectangle.x = x * 60;
        tileRectangle.y = y * 60;
    }
}
