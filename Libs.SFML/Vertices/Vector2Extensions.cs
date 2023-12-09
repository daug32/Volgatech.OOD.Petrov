using SFML.System;

namespace Libs.SFML.Vertices;

// ReSharper disable once InconsistentNaming
public static class Vector2Extensions
{
    public static float GetSquareDistance( this Vector2i p0, Vector2i p1 )
    {
        return GetSquareDistance( ( Vector2f )p0, ( Vector2f )p1 );
    }

    public static float GetSquareDistance( this Vector2f p0, Vector2f p1 )
    {
        float dx = p1.X - p0.X;
        float dy = p1.Y - p0.Y;
        return dx * dx + dy * dy;
    }
}