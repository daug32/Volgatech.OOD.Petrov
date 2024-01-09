using SFML.Graphics;
using SFML.System;

namespace Lab1.Models;

public class Rectangle : RectangleShape, ISurface
{
    public Rectangle( Vector2f leftTop, Vector2f rightBottom )
        : base( new Vector2f(
            Math.Abs( rightBottom.X - leftTop.X ),
            Math.Abs( rightBottom.Y - leftTop.Y ) ) )
    {
        Position = leftTop;
    }

    public float GetArea()
    {
        return Size.X * Size.Y;
    }

    public float GetPerimeter()
    {
        return 2 * ( Size.X + Size.Y );
    }

    public string GetSurfaceInfo()
    {
        return $"RECTANGLE: P={GetPerimeter()}; S={GetArea()}";
    }
}