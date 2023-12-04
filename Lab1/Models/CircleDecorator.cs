using SFML.Graphics;
using SFML.System;

namespace Lab1.Models;

public class CircleDecorator : CircleShape, ISurface
{
    public CircleDecorator( Vector2f center, float radius )
        : base( radius )
    {
        Position = center;
    }

    public float GetArea()
    {
        return MathF.PI * Radius * Radius;
    }

    public float GetPerimeter()
    {
        return 2 * MathF.PI * Radius;
    }

    public string GetSurfaceInfo() => $"CIRCLE: P={GetPerimeter()}; S={GetArea()}";
}