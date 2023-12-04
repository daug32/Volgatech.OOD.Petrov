using SFML.Graphics;
using SFML.System;

namespace Lab1.Models;

public class RectangleDecorator : RectangleShape, ISurface
{
    public RectangleDecorator( Vector2f p0, Vector2f p1 )
        : base( new Vector2f(
            Math.Abs( p1.X - p0.X ),
            Math.Abs( p1.Y - p0.Y ) ) )
    {
    }

    public float GetArea()
    {
        return Size.X * Size.Y;
    }

    public float GetPerimeter()
    {
        return 2 * ( Size.X + Size.Y );
    }

    public string GetSurfaceInfo() => $"RECTANGLE: P={GetPerimeter()}; S={GetArea()}";
}