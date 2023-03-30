using System;
using Raylib_cs;
using System.Numerics;

//Allt är 60x60
Manager m = new();


//width and height of window
int width = 1200;
int height = 900;

//Create window and set target FPS
Raylib.InitWindow(width, height, "Atle Escapses The Matrix");
Raylib.SetTargetFPS(60);

m.LevelDone();

//play until window is closed
while(!Raylib.WindowShouldClose())
{
    m.Playing();
    m.Draw();
}