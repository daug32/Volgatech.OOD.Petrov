using SFML.System;

namespace Lab1.Extensions;

// ReSharper disable once InconsistentNaming
public static class Vector2fExtensions
{
    public static float GetSquareDistance( this Vector2f p0, Vector2f p1 )
    {
        float dx = p1.X - p0.X;
        float dy = p1.Y - p0.Y;
        return dx * dx + dy * dy;
    }
}