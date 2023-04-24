using System;
using Raylib_cs;
using System.Numerics;


public class Goal
{
    public Rectangle rect = new(0, 0, 60, 60);

    public Goal(int x, int y)
    {
        rect.x = x * 60;
        rect.y = y * 60;
    }
}
